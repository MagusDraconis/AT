using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether MeanCoupling behaves as a true dynamical field
/// variable with a simple governing equation dM/dt = f(M, R).
///
/// AT-082: Mean Coupling Field Equation
/// </summary>
public static class MeanCouplingFieldAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Time series of M(t), R(t) and their derivatives for one run.
    /// </summary>
    public sealed record MeanCouplingProfile(
        string TopologyType,
        double K,
        double Lambda,
        int Seed,
        double[] M,
        double[] R,
        double[] dMdt,
        double[] dRdt,
        int Steps);

    /// <summary>
    /// Fit result for one candidate field equation.
    /// </summary>
    public sealed record FieldEquationFit(
        string ModelLabel,
        string Equation,
        int NumParams,
        double R2,
        double AdjustedR2,
        double AIC,
        double[] Parameters,
        string[] ParamNames);

    /// <summary>
    /// Full report with all profiles and equation fits.
    /// </summary>
    public sealed record FieldEquationReport(
        List<MeanCouplingProfile> Profiles,
        List<FieldEquationFit> Fits,
        string BestModel,
        double BestR2,
        double NullR2,
        string Classification,
        string Interpretation,
        double MRange,
        double dMdtRange,
        double RRange);

    // ══════════════════════════════════════════════════════════════════
    // Simulation
    // ══════════════════════════════════════════════════════════════════

    public static MeanCouplingProfile SimulateProfile(
        string topologyType, double k, double lambda, int n, int seed,
        int totalSteps = 1000, int snapshotInterval = 20, double posStep = 0.001)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Generate positions for this topology.
        GeneratePositions(network, n, topologyType, rng);

        // Initialize phases randomly.
        for (int i = 0; i < n; i++)
            network.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;

        // Initialize coupling.
        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        int numSnapshots = totalSteps / snapshotInterval + 1;
        var M = new double[numSnapshots];
        var R = new double[numSnapshots];

        int rec = 0;
        for (int step = 0; step <= totalSteps; step++)
        {
            if (step % snapshotInterval == 0)
            {
                M[rec] = ComputeMeanCoupling(network);
                R[rec] = ComputeOrderParameter(network);
                rec++;
            }

            if (step == totalSteps) break;

            // Phase update: standard Kuramoto.
            PhaseStep(network);

            // Position update: gradient descent on coupling energy.
            PositionStep(network, posStep);

            // Recompute coupling from new positions.
            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        }

        // Compute finite-difference derivatives.
        var dMdt = new double[numSnapshots];
        var dRdt = new double[numSnapshots];
        for (int i = 1; i < numSnapshots; i++)
        {
            double dt = snapshotInterval;
            dMdt[i] = (M[i] - M[i - 1]) / dt;
            dRdt[i] = (R[i] - R[i - 1]) / dt;
        }

        return new MeanCouplingProfile(topologyType, k, lambda, seed, M, R, dMdt, dRdt, totalSteps);
    }

    // ══════════════════════════════════════════════════════════════════
    // Position generation
    // ══════════════════════════════════════════════════════════════════

    private static void GeneratePositions(TemporalNetwork net, int n, string type, Random rng)
    {
        for (int i = 0; i < n; i++)
        {
            double x, y;
            switch (type)
            {
                case "clustered":
                    int cluster = rng.Next(3);
                    double cx = cluster switch { 0 => 0.2, 1 => 0.5, _ => 0.8 };
                    double cy = cluster switch { 0 => 0.3, 1 => 0.7, _ => 0.5 };
                    x = Math.Clamp(cx + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    y = Math.Clamp(cy + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    break;
                case "linear":
                    double t = (double)i / n;
                    x = 0.1 + t * 0.8;
                    y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.02;
                    break;
                case "circular":
                    double angle = 2 * Math.PI * i / n;
                    x = 0.5 + 0.3 * Math.Cos(angle);
                    y = 0.5 + 0.3 * Math.Sin(angle);
                    break;
                case "dense-sparse":
                    if (i < n / 2)
                    { x = rng.NextDouble() * 0.4; y = rng.NextDouble(); }
                    else
                    { x = 0.6 + rng.NextDouble() * 0.4; y = rng.NextDouble(); }
                    break;
                case "random-clusters":
                    int rc = rng.Next(4);
                    double rcx = rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.8, _ => 0.35 };
                    double rcy = rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.5, _ => 0.8 };
                    x = Math.Clamp(rcx + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99);
                    y = Math.Clamp(rcy + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99);
                    break;
                default: // uniform
                    x = rng.NextDouble();
                    y = rng.NextDouble();
                    break;
            }
            net.AddNode(new TemporalNode(i, 0, 1.0) { X = x, Y = y });
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // Phase step: Kuramoto dynamics
    // ══════════════════════════════════════════════════════════════════

    private static void PhaseStep(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double[] newPhases = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                sum += net.Matrix.GetCoupling(i, j) *
                       Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            }
            newPhases[i] = TemporalSimulation.NormalizePhase(
                net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum));
        }

        for (int i = 0; i < n; i++)
            net.Nodes[i].Phase = newPhases[i];
    }

    // ══════════════════════════════════════════════════════════════════
    // Position step: coupling-energy gradient descent
    // ══════════════════════════════════════════════════════════════════

    private static void PositionStep(TemporalNetwork net, double posStep)
    {
        int n = net.NodeCount;
        double[] newX = new double[n], newY = new double[n];

        for (int i = 0; i < n; i++)
        {
            double fx = 0, fy = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double dx = net.Nodes[j].X - net.Nodes[i].X;
                double dy = net.Nodes[j].Y - net.Nodes[i].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                double coupling = net.Matrix.GetCoupling(i, j);
                double cosTerm = Math.Cos(net.Nodes[j].Phase - net.Nodes[i].Phase);
                double forceMag = coupling * cosTerm / dist;
                fx += forceMag * dx;
                fy += forceMag * dy;
            }
            newX[i] = Math.Clamp(net.Nodes[i].X + posStep * fx, 0.01, 0.99);
            newY[i] = Math.Clamp(net.Nodes[i].Y + posStep * fy, 0.01, 0.99);
        }

        for (int i = 0; i < n; i++)
        { net.Nodes[i].X = newX[i]; net.Nodes[i].Y = newY[i]; }
    }

    // ══════════════════════════════════════════════════════════════════
    // Metrics
    // ══════════════════════════════════════════════════════════════════

    private static double ComputeMeanCoupling(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double sum = 0;
        int pairs = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { sum += net.Matrix.GetCoupling(i, j); pairs++; }
        return sum / pairs;
    }

    private static double ComputeOrderParameter(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    // ══════════════════════════════════════════════════════════════════
    // Equation fitting
    // ══════════════════════════════════════════════════════════════════

    public static FieldEquationReport Analyze(List<MeanCouplingProfile> profiles)
    {
        // Pool all non-zero-derivative data points.
        var M_all = new List<double>();
        var R_all = new List<double>();
        var dM_all = new List<double>();

        foreach (var p in profiles)
            for (int i = 1; i < p.M.Length; i++) // skip t=0 (no derivative)
            {
                M_all.Add(p.M[i]);
                R_all.Add(p.R[i]);
                dM_all.Add(p.dMdt[i]);
            }

        int nPts = dM_all.Count;
        double[] M = M_all.ToArray();
        double[] R = R_all.ToArray();
        double[] Y = dM_all.ToArray();

        double mRange = M.Max() - M.Min();
        double rRange = R.Max() - R.Min();
        double yRange = Y.Max() - Y.Min();

        // Baseline: intercept-only model.
        double meanY = Y.Average();
        double ssTot = 0;
        for (int i = 0; i < nPts; i++) ssTot += (Y[i] - meanY) * (Y[i] - meanY);
        double nullR2 = 0.0; // intercept-only gives R²=0 by definition

        // Candidate models.
        var fits = new List<FieldEquationFit>();

        // Model A: dM/dt = a₀ + a₁·M
        fits.Add(FitModel("A", "a₀ + a₁·M", nPts, new[] { M }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·M" }));

        // Model B: dM/dt = a₀ + a₁·M + a₂·M²
        double[] M2 = M.Select(m => m * m).ToArray();
        fits.Add(FitModel("B", "a₀ + a₁·M + a₂·M²", nPts, new[] { M, M2 }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·M", "a₂·M²" }));

        // Model C: dM/dt = a₀ + a₁·R
        fits.Add(FitModel("C", "a₀ + a₁·R", nPts, new[] { R }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·R" }));

        // Model D: dM/dt = a₀ + a₁·M + a₂·R
        fits.Add(FitModel("D", "a₀ + a₁·M + a₂·R", nPts, new[] { M, R }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·M", "a₂·R" }));

        // Model E: dM/dt = a₀ + a₁·M·R
        double[] MR = new double[nPts];
        for (int i = 0; i < nPts; i++) MR[i] = M[i] * R[i];
        fits.Add(FitModel("E", "a₀ + a₁·M·R", nPts, new[] { MR }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·M·R" }));

        // Model F: full quadratic dM/dt = a₀ + a₁·M + a₂·R + a₃·M² + a₄·R² + a₅·M·R
        double[] R2 = R.Select(r => r * r).ToArray();
        fits.Add(FitModel("F", "a₀ + a₁·M + a₂·R + a₃·M² + a₄·R² + a₅·M·R",
            nPts, new[] { M, R, M2, R2, MR }, Y, ssTot, meanY,
            new[] { "a₀", "a₁·M", "a₂·R", "a₃·M²", "a₄·R²", "a₅·M·R" }));

        fits = fits.OrderByDescending(f => f.AdjustedR2).ToList();

        var best = fits[0];
        string classification;
        string interpretation;

        if (best.AdjustedR2 >= 0.40)
        {
            classification = "D: Fundamental State Variable";
            interpretation =
                $"MeanCoupling obeys a STRONG dynamical law ({best.Equation}, " +
                $"Adj R² = {best.AdjustedR2:F3}). M is not merely a compressed " +
                "topology descriptor — it is a genuine dynamical field variable " +
                "whose evolution is predictable from (M, R) alone. " +
                "This CLOSES the effective theory: R(t) determines M(t) determines dR/dt.";
        }
        else if (best.AdjustedR2 >= 0.15)
        {
            classification = "C: Effective Field";
            interpretation =
                $"MeanCoupling shows MODERATE predictability ({best.Equation}, " +
                $"Adj R² = {best.AdjustedR2:F3}). M behaves as an effective field — " +
                "its evolution is partially captured by a simple equation, but " +
                "significant stochastic or higher-order effects remain.";
        }
        else if (best.AdjustedR2 >= 0.03)
        {
            classification = "B: Weak Dynamics";
            interpretation =
                $"MeanCoupling has WEAK dynamical content (best Adj R² = {best.AdjustedR2:F3}). " +
                "M changes measurably but the evolution is dominated by noise " +
                "rather than a deterministic field equation.";
        }
        else
        {
            classification = "A: Static Descriptor";
            interpretation =
                $"MeanCoupling is essentially STATIC (best Adj R² = {best.AdjustedR2:F3}). " +
                "M does not change appreciably during the simulation — it is a " +
                "compressed topology descriptor, NOT a dynamical field variable. " +
                "The effective coupling field does NOT have its own dynamics.";
        }

        return new FieldEquationReport(profiles, fits, best.ModelLabel,
            best.R2, nullR2, classification, interpretation, mRange, yRange, rRange);
    }

    // ══════════════════════════════════════════════════════════════════
    // Single model fit
    // ══════════════════════════════════════════════════════════════════

    private static FieldEquationFit FitModel(
        string label, string equation, int n, double[][] predictors,
        double[] Y, double ssTot, double meanY, string[] paramNames)
    {
        int p = predictors.Length;  // predictor variables
        int m = p + 1;              // + intercept

        // Build normal equations.
        double[,] XTX = new double[m, m];
        double[] XTY = new double[m];
        for (int i = 0; i < n; i++)
        {
            double[] f = new double[m];
            f[0] = 1.0;
            for (int j = 0; j < p; j++)
                f[j + 1] = predictors[j][i];
            for (int a = 0; a < m; a++)
            {
                XTY[a] += f[a] * Y[i];
                for (int b = 0; b < m; b++)
                    XTX[a, b] += f[a] * f[b];
            }
        }

        double[] beta = SolveGauss(XTX, XTY, m);

        double ssRes = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            for (int j = 0; j < p; j++)
                pred += beta[j + 1] * predictors[j][i];
            double err = Y[i] - pred;
            ssRes += err * err;
        }

        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0.0;
        double adjR2 = n > m ? 1.0 - (1.0 - r2) * (n - 1) / (n - m) : r2;

        // AIC = n * ln(RSS/n) + 2*k where k = numParams + 1 (for σ²)
        int nParamsAic = m + 1;
        double sigma2 = ssRes / n;
        double aic = sigma2 > 1e-15
            ? n * Math.Log(sigma2) + 2.0 * nParamsAic
            : double.MaxValue;

        return new FieldEquationFit(label, equation, m, r2, adjR2, aic, beta, paramNames);
    }

    // ══════════════════════════════════════════════════════════════════
    // Gaussian elimination (same as AT-080/081)
    // ══════════════════════════════════════════════════════════════════

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = A[i, j];
            M[i, n] = b[i];
        }

        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col]))
                    maxRow = row;
            for (int j = col; j <= n; j++)
                (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            {
                double f = M[row, col] / M[col, col];
                for (int j = col; j <= n; j++)
                    M[row, j] -= f * M[col, j];
            }
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double s = M[i, n];
            for (int j = i + 1; j < n; j++)
                s -= M[i, j] * x[j];
            x[i] = Math.Abs(M[i, i]) > 1e-15 ? s / M[i, i] : 0;
        }
        return x;
    }
}
