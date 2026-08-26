using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether trajectory prediction becomes accurate
/// when dynamical evolution terms (dR/dt, dA/dt, dF/dt)
/// are included alongside initial state variables.
///
/// AT-077: Dynamical Closure Theory
/// </summary>
public static class DynamicalClosureAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record TrajectoryState(
        double R,
        double Alignment,
        double MeanForce,
        double NetForce,
        double dR,          // dR/dt (finite difference)
        double dA,          // dA/dt
        double dF,          // dF/dt
        double Separation,
        double Velocity);

    public sealed record ModelResult(
        string Name,
        string Description,
        double R2,
        double RMSE,
        double[] Coefficients);

    public sealed record ClosureReport(
        string LawName,
        List<ModelResult> Models,
        double StaticR2,         // Model A R²
        double BestR2,           // Best model R²
        double DynamicalGain,    // Best - Static
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Run dynamical trajectory
    // ══════════════════════════════════════════════════════════════════

    public static List<TrajectoryState> RunDynamicalTrajectory(
        double targetR, string lawName, Func<double, double> forceFn,
        double k, double lambda, int nPerGroup, int seed,
        int steps = 100, int recordEvery = 2)
    {
        var network = PrepareState(targetR, k, lambda, nPerGroup, seed);
        var states = new List<TrajectoryState>();
        int n = network.NodeCount;

        double prevR = double.NaN, prevA = double.NaN, prevF = double.NaN;

        for (int iter = 0; iter <= steps; iter++)
        {
            // Phase update.
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }

            // Position update.
            double[] nx = new double[n], ny = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx = 0, fy = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double dx = network.Nodes[j].X - network.Nodes[i].X;
                    double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                    double w = network.Matrix.GetCoupling(i, j);
                    double pd = TemporalSimulation.NormalizePhase(
                        network.Nodes[j].Phase - network.Nodes[i].Phase);
                    if (pd > Math.PI) pd -= 2 * Math.PI;
                    fx += w * forceFn(pd) * dx / d;
                    fy += w * forceFn(pd) * dy / d;
                }
                nx[i] = Math.Clamp(network.Nodes[i].X + 0.001 * fx, 0.01, 0.99);
                ny[i] = Math.Clamp(network.Nodes[i].Y + 0.001 * fy, 0.01, 0.99);
            }
            for (int i = 0; i < n; i++)
            { network.Nodes[i].X = nx[i]; network.Nodes[i].Y = ny[i]; }

            if (iter % recordEvery == 0)
            {
                double r = GlobalR(network);
                var fp = ForceSummationAnalyzer.ComputeForces(
                    r, lawName, forceFn, k, lambda, nPerGroup, seed + iter);
                double a = fp.AlignmentScore;
                double mf = fp.MeanPairMagnitude;
                double nf = fp.NetForceMagnitude;
                double sep = GroupSeparation(network, nPerGroup);

                double dr = double.IsNaN(prevR) ? 0 : (r - prevR) / recordEvery;
                double da = double.IsNaN(prevA) ? 0 : (a - prevA) / recordEvery;
                double df = double.IsNaN(prevF) ? 0 : (nf - prevF) / recordEvery;
                double vel = states.Count > 0
                    ? Math.Abs(sep - states[^1].Separation) / recordEvery : 0;

                states.Add(new TrajectoryState(r, a, mf, nf,
                    dr, da, df, sep, vel));

                prevR = r; prevA = a; prevF = nf;
            }
        }

        return states;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model fitting
    // ══════════════════════════════════════════════════════════════════

    public static ClosureReport AnalyzeClosure(
        List<List<TrajectoryState>> allTrajectories, string lawName)
    {
        // Flatten: use state at t to predict Δsep over next H steps.
        var samples = new List<(double[] features, double target)>();
        int horizon = 10; // predict separation change over next 10 rec intervals

        foreach (var traj in allTrajectories)
        {
            for (int i = 0; i < traj.Count - horizon / 2; i++)
            {
                var s = traj[i];
                int j = Math.Min(i + horizon / 2, traj.Count - 1);
                double sepChange = traj[j].Separation - s.Separation;

                // Features.
                double r = s.R, a = s.Alignment, mf = s.MeanForce;
                double nf = s.NetForce;
                double dr = s.dR, da = s.dA, df = s.dF;

                // Model A: initial state only (A·⟨f⟩)
                samples.Add((new[] { a * mf }, sepChange));
                // Also store for multi-feature models via indexing.
            }
        }

        // Build feature matrices for each model.
        int n = allTrajectories.Sum(t => Math.Max(0, t.Count - horizon / 2));
        var allStates = new List<TrajectoryState>();
        foreach (var traj in allTrajectories)
            for (int i = 0; i < traj.Count - horizon / 2; i++)
                allStates.Add(traj[i]);

        var targets = allStates.Select((s, idx) =>
        {
            var traj = allTrajectories.First(t => t.Contains(s));
            int i = traj.IndexOf(s);
            int j = Math.Min(i + horizon / 2, traj.Count - 1);
            return traj[j].Separation - s.Separation;
        }).ToArray();

        var models = new List<ModelResult>();

        // Model A: F = β₀ + β₁·A·⟨f⟩
        models.Add(FitModel("Static (A·⟨f⟩)", "F = β₀ + β₁·A·⟨f⟩",
            allStates.Select(s => new[] { 1.0, s.Alignment * s.MeanForce }).ToArray(),
            targets));

        // Model B: F = β₀ + β₁·A·⟨f⟩ + β₂·R
        models.Add(FitModel("Static + R", "F = β₀ + β₁·A·⟨f⟩ + β₂·R",
            allStates.Select(s => new[] { 1.0, s.Alignment * s.MeanForce, s.R }).ToArray(),
            targets));

        // Model C: + dR/dt
        models.Add(FitModel("Static + dR/dt", "F + β₃·dR/dt",
            allStates.Select(s => new[] { 1.0, s.Alignment * s.MeanForce, s.R, s.dR }).ToArray(),
            targets));

        // Model D: + dA/dt
        models.Add(FitModel("Static + dR/dt + dA/dt", "F + β₄·dA/dt",
            allStates.Select(s => new[] { 1.0, s.Alignment * s.MeanForce, s.R, s.dR, s.dA }).ToArray(),
            targets));

        // Model E: full dynamic closure
        models.Add(FitModel("Full Dynamic", "F + dR/dt + dA/dt + dF/dt",
            allStates.Select(s => new[] { 1.0, s.Alignment * s.MeanForce, s.R, s.dR, s.dA, s.dF }).ToArray(),
            targets));

        models = models.OrderByDescending(m => m.R2).ToList();
        double staticR2 = models.First(m => m.Name == "Static (A·⟨f⟩)").R2;
        double bestR2 = models[0].R2;
        double gain = bestR2 - staticR2;

        string classification = gain > 0.20 ? "D: Fully Predictive" :
                                gain > 0.10 ? "C: Strong Dynamic Closure" :
                                gain > 0.03 ? "B: Partial Dynamic Closure" :
                                "A: Static Only";

        string interp = classification switch
        {
            "D: Fully Predictive" =>
                $"Dynamic terms dramatically improve prediction " +
                $"(ΔR²=+{gain:F3}). The theory is CLOSED — trajectories " +
                "are predictable from state + derivatives.",
            "C: Strong Dynamic Closure" =>
                $"Dynamic terms significantly improve prediction " +
                $"(ΔR²=+{gain:F3}). Phase evolution is essential for " +
                "accurate trajectory forecasting.",
            "B: Partial Dynamic Closure" =>
                $"Dynamic terms provide modest improvement " +
                $"(ΔR²=+{gain:F3}). Some predictability is gained.",
            _ => "Dynamic terms do not improve prediction. " +
                 "Trajectories remain unpredictable from local information."
        };

        return new ClosureReport(lawName, models, staticR2, bestR2,
            gain, classification, interp);
    }

    // ══════════════════════════════════════════════════════════════════
    // Linear model fit
    // ══════════════════════════════════════════════════════════════════

    private static ModelResult FitModel(string name, string desc,
        double[][] features, double[] targets)
    {
        int m = features.Length, k = features[0].Length;

        // Normal equations: (X^T X) β = X^T y.
        double[,] XTX = new double[k, k];
        double[] XTy = new double[k];
        for (int i = 0; i < m; i++)
        {
            for (int a = 0; a < k; a++)
            {
                XTy[a] += features[i][a] * targets[i];
                for (int b = 0; b < k; b++)
                    XTX[a, b] += features[i][a] * features[i][b];
            }
        }

        double[] beta = SolveGauss(XTX, XTy, k);

        double ssRes = 0, ssTot = 0, meanY = targets.Average();
        for (int i = 0; i < m; i++)
        {
            double pred = 0;
            for (int a = 0; a < k; a++) pred += beta[a] * features[i][a];
            ssRes += (targets[i] - pred) * (targets[i] - pred);
            ssTot += (targets[i] - meanY) * (targets[i] - meanY);
        }
        double r2 = ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
        double rmse = Math.Sqrt(ssRes / m);

        return new ModelResult(name, desc, r2, rmse, beta);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full sweep
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, ClosureReport> RunClosureAnalysis(
        double[] rTargets, string[] lawNames,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        var forceLaws = new Dictionary<string, Func<double, double>>
        {
            ["cos"] = d => Math.Cos(d),
            ["cos²"] = d => Math.Cos(d) * Math.Cos(d),
            ["exp(-|x|)"] = d => Math.Exp(-Math.Abs(d)),
            ["1/(1+|x|)"] = d => 1.0 / (1.0 + Math.Abs(d)),
        };

        var results = new Dictionary<string, ClosureReport>();
        int seedIdx = 0;

        foreach (string law in lawNames)
        {
            var fn = forceLaws[law];
            var trajectories = new List<List<TrajectoryState>>();

            foreach (double rT in rTargets)
            {
                for (int s = 0; s < seedsPerPoint; s++)
                {
                    trajectories.Add(RunDynamicalTrajectory(
                        rT, law, fn, k, lambda, nPerGroup,
                        baseSeed + seedIdx++ * 7919));
                }
            }

            results[law] = AnalyzeClosure(trajectories, law);
        }

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static TemporalNetwork PrepareState(
        double targetR, double k, double lambda, int nPerGroup, int seed)
    {
        double kappa = CriticalCoherenceAnalyzer.KappaFromR(targetR);
        var rng = new Random(seed);
        int n = nPerGroup * 2;
        var network = new TemporalNetwork(n);

        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(i, phase, 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }
        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(nPerGroup + i, phase, 1.0)
            { X = Math.Clamp(0.7 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        return network;
    }

    private static double GlobalR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    private static double GroupSeparation(TemporalNetwork net, int np)
    {
        double ax = 0, ay = 0, bx = 0, by = 0;
        for (int i = 0; i < np; i++)
        { ax += net.Nodes[i].X; ay += net.Nodes[i].Y; bx += net.Nodes[i + np].X; by += net.Nodes[i + np].Y; }
        ax /= np; ay /= np; bx /= np; by /= np;
        return Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));
    }

    private static double VonMises(Random rng, double kappa)
    {
        if (kappa < 0.01) return rng.NextDouble() * 2 * Math.PI;
        if (kappa > 5.0)
        {
            double u1 = rng.NextDouble(), u2 = rng.NextDouble();
            double s = 1.0 / Math.Sqrt(kappa);
            double z = Math.Sqrt(-2 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2 * Math.PI * u2);
            double t = z * s; t %= 2 * Math.PI; if (t < 0) t += 2 * Math.PI;
            return t;
        }
        double tau = 1 + Math.Sqrt(1 + 4 * kappa * kappa);
        double rho = (tau - Math.Sqrt(2 * tau)) / (2 * kappa);
        double r = (1 + rho * rho) / (2 * rho);
        for (int a = 0; a < 1000; a++)
        {
            double u1 = rng.NextDouble(), u2 = rng.NextDouble(), u3 = rng.NextDouble();
            double zz = Math.Cos(Math.PI * u1), f = (1 + r * zz) / (r + zz), c = kappa * (r - f);
            if (c * (2 - c) - u2 > 0 || (c > 0 && Math.Log(c / Math.Max(u2, 1e-15)) + 1 - c >= 0))
            { double th = Math.Acos(Math.Clamp(f, -1, 1)); if (u3 > 0.5) th = 2 * Math.PI - th; return th; }
        }
        return rng.NextDouble() * 2 * Math.PI;
    }

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        { for (int j = 0; j < n; j++) M[i, j] = A[i, j]; M[i, n] = b[i]; }
        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col])) maxRow = row;
            for (int j = col; j <= n; j++)
                (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            { double f = M[row, col] / M[col, col]; for (int j = col; j <= n; j++) M[row, j] -= f * M[col, j]; }
        }
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        { double s = M[i, n]; for (int j = i + 1; j < n; j++) s -= M[i, j] * x[j]; x[i] = Math.Abs(M[i, i]) > 1e-15 ? s / M[i, i] : 0; }
        return x;
    }
}
