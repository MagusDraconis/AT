using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether unexplained dR/dt variance arises from
/// network topology rather than hidden phase state variables.
///
/// TQM-080: Network Topology and Coherence Evolution
/// </summary>
public static class TopologyEvolutionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record TopologyState(
        double R,
        double dRdt,
        // Topology metrics
        double MeanCoupling,
        double CouplingVariance,
        double MeanDegree,
        double DegreeVariance,
        double SpectralGap,
        double CouplingEntropy,
        double SpatialClustering,
        double EffectiveDimension,
        string TopologyType,
        int Seed);

    public sealed record TopologyGain(
        string Metric,
        double R2_Base,
        double R2_With,
        double Gain);

    public sealed record TopologyReport(
        List<TopologyState> States,
        List<TopologyGain> Gains,
        string BestMetric,
        double BestGain,
        double TotalR2,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Generate diverse topologies
    // ══════════════════════════════════════════════════════════════════

    public static List<TopologyState> GenerateTopologyEnsemble(
        double k, double lambda, int n, int numConfigs, int baseSeed,
        int evolutionSteps = 10)
    {
        var states = new List<TopologyState>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        for (int c = 0; c < numConfigs; c++)
        {
            int seed = baseSeed + c * 7919;
            var rng = new Random(seed);
            string type = types[c % types.Length];

            var network = new TemporalNetwork(n);
            GeneratePositions(network, n, type, rng);
            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

            // Compute topology metrics.
            var topo = ComputeTopology(network);

            // Generate phases with controlled randomness.
            for (int i = 0; i < n; i++)
                network.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;

            double initR = GlobalR(network);

            // Short evolution.
            for (int iter = 0; iter < evolutionSteps; iter++)
            {
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
            }

            double finalR = GlobalR(network);
            double dr = (finalR - initR) / evolutionSteps;

            states.Add(new TopologyState(initR, dr,
                topo.meanCoupling, topo.couplingVar, topo.meanDegree,
                topo.degreeVar, topo.spectralGap, topo.couplingEntropy,
                topo.spatialClustering, topo.effectiveDim, type, c));
        }

        return states;
    }

    // ══════════════════════════════════════════════════════════════════
    // Position generation for different topologies
    // ══════════════════════════════════════════════════════════════════

    private static void GeneratePositions(
        TemporalNetwork network, int n, string type, Random rng)
    {
        for (int i = 0; i < n; i++)
        {
            double x, y;
            switch (type)
            {
                case "clustered":
                    // 2-3 clusters.
                    int cluster = rng.Next(3);
                    double cx = cluster switch { 0 => 0.2, 1 => 0.5, _ => 0.8 };
                    double cy = cluster switch { 0 => 0.3, 1 => 0.7, _ => 0.5 };
                    x = Math.Clamp(cx + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    y = Math.Clamp(cy + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    break;
                case "linear":
                    // Along a line.
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
                    // Half dense, half sparse.
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
            network.AddNode(new TemporalNode(i, 0, 1.0) { X = x, Y = y });
        }
    }

    // ══════════════════════════════════════════════════════════════════
    // Topology metrics
    // ══════════════════════════════════════════════════════════════════

    private static (double meanCoupling, double couplingVar, double meanDegree,
        double degreeVar, double spectralGap, double couplingEntropy,
        double spatialClustering, double effectiveDim)
        ComputeTopology(TemporalNetwork net)
    {
        int n = net.NodeCount;

        // Mean coupling and variance.
        double sumC = 0, sumC2 = 0; int pairs = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { double c = net.Matrix.GetCoupling(i, j); sumC += c; sumC2 += c * c; pairs++; }
        double meanC = sumC / pairs;
        double varC = sumC2 / pairs - meanC * meanC;

        // Degree (sum of incoming couplings) per node.
        double[] degrees = new double[n];
        for (int i = 0; i < n; i++)
        {
            double deg = 0;
            for (int j = 0; j < n; j++)
                if (i != j) deg += net.Matrix.GetCoupling(i, j);
            degrees[i] = deg;
        }
        double meanDeg = degrees.Average();
        double varDeg = degrees.Average(d => (d - meanDeg) * (d - meanDeg));

        // Spectral gap: ratio of largest to second-largest eigenvalue.
        // Approximate via power iteration on coupling matrix.
        double[] v = new double[n];
        for (int i = 0; i < n; i++) v[i] = 1.0;
        double lambda1 = PowerIteration(net, v, 20);
        // Deflate and find second eigenvalue.
        double lambda2 = 0;
        double sumV2 = 0;
        for (int i = 0; i < n; i++) sumV2 += v[i] * v[i];
        double normV = Math.Sqrt(sumV2);
        if (normV > 1e-10)
        {
            double[] w = new double[n];
            for (int i = 0; i < n; i++) w[i] = i % 2 == 0 ? 1.0 : -1.0; // orthogonal guess
            // Remove projection onto v.
            double dot = 0;
            for (int i = 0; i < n; i++) dot += w[i] * v[i];
            for (int i = 0; i < n; i++) w[i] -= dot / (normV * normV) * v[i];
            lambda2 = PowerIteration(net, w, 20);
        }
        double spectralGap = lambda1 > 1e-10 ? lambda2 / lambda1 : 0;

        // Coupling entropy (histogram of coupling strengths).
        int bins = 20;
        double[] hist = new double[bins];
        double maxC = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { double c = net.Matrix.GetCoupling(i, j); if (c > maxC) maxC = c; }
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                int b = maxC > 1e-10 ? (int)(net.Matrix.GetCoupling(i, j) / maxC * bins) : 0;
                if (b >= bins) b = bins - 1;
                hist[b]++;
            }
        double entropy = 0;
        for (int b = 0; b < bins; b++)
        {
            double p = hist[b] / pairs;
            if (p > 0) entropy -= p * Math.Log(p);
        }

        // Spatial clustering: ratio of near-neighbor coupling to far-neighbor.
        double nearSum = 0, farSum = 0; int nearCount = 0, farCount = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double dx = net.Nodes[i].X - net.Nodes[j].X;
                double dy = net.Nodes[i].Y - net.Nodes[j].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                double c = net.Matrix.GetCoupling(i, j);
                if (d < 0.2) { nearSum += c; nearCount++; }
                else { farSum += c; farCount++; }
            }
        double spatialClustering = farCount > 0 && nearCount > 0
            ? (nearSum / nearCount) / (farSum / farCount) : 0;

        // Effective dimension: log(N_pairs_within_r) vs log(r).
        double effectiveDim = 2.0; // default 2D

        return (meanC, varC, meanDeg, varDeg, spectralGap, entropy,
            spatialClustering, effectiveDim);
    }

    private static double PowerIteration(TemporalNetwork net, double[] v, int iters)
    {
        int n = net.NodeCount;
        for (int iter = 0; iter < iters; iter++)
        {
            double[] w = new double[n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i != j) w[i] += net.Matrix.GetCoupling(i, j) * v[j];
            double norm = 0;
            for (int i = 0; i < n; i++) norm += w[i] * w[i];
            norm = Math.Sqrt(norm);
            if (norm < 1e-15) return 0;
            for (int i = 0; i < n; i++) v[i] = w[i] / norm;
        }
        // Rayleigh quotient.
        double rayleigh = 0, sq = 0;
        for (int i = 0; i < n; i++)
        {
            double av = 0;
            for (int j = 0; j < n; j++)
                if (i != j) av += net.Matrix.GetCoupling(i, j) * v[j];
            rayleigh += v[i] * av;
            sq += v[i] * v[i];
        }
        return sq > 1e-15 ? rayleigh / sq : 0;
    }

    // ══════════════════════════════════════════════════════════════════
    // Analysis
    // ══════════════════════════════════════════════════════════════════

    public static TopologyReport AnalyzeTopology(List<TopologyState> states)
    {
        double[] R = states.Select(s => s.R).ToArray();
        double[] dRdt = states.Select(s => s.dRdt).ToArray();

        double r2Base = R2Linear(R, dRdt);

        var metrics = new (string Name, Func<TopologyState, double> Get)[]
        {
            ("MeanCoupling", s => s.MeanCoupling),
            ("CouplingVar", s => s.CouplingVariance),
            ("MeanDegree", s => s.MeanDegree),
            ("DegreeVar", s => s.DegreeVariance),
            ("SpectralGap", s => s.SpectralGap),
            ("CouplingEntropy", s => s.CouplingEntropy),
            ("SpatialClustering", s => s.SpatialClustering),
        };

        var gains = new List<TopologyGain>();
        foreach (var (name, get) in metrics)
        {
            double[] tVals = states.Select(get).ToArray();
            // Y = β₀ + β₁·R + β₂·T
            var (_, r2With) = Fit3Param(R, tVals, dRdt);
            gains.Add(new TopologyGain(name, r2Base, r2With, r2With - r2Base));
        }

        // Also: topology only (no R).
        foreach (var (name, get) in metrics)
        {
            double[] tVals = states.Select(get).ToArray();
            double r2Topo = R2Linear(tVals, dRdt);
            // Add as separate entry.
        }

        gains = gains.OrderByDescending(g => g.Gain).ToList();
        var best = gains[0];
        double totalR2 = best.R2_With;

        string classification = best.Gain > 0.10 ? "D: Topology-Dominated" :
                                best.Gain > 0.05 ? "C: Strong Topology Dependence" :
                                best.Gain > 0.02 ? "B: Weak Topology Dependence" :
                                "A: Pure Noise / No Topology Effect";

        string interp = classification switch
        {
            "D: Topology-Dominated" =>
                $"Topology explains significant additional variance " +
                $"(ΔR²=+{best.Gain:F3}). Network structure is key to coherence evolution.",
            "C: Strong Topology Dependence" =>
                $"Topology measurably influences dR/dt (ΔR²=+{best.Gain:F3}). " +
                "The network matters alongside phase state.",
            "B: Weak Topology Dependence" =>
                $"Topology has a small but detectable effect (ΔR²=+{best.Gain:F3}).",
            _ => "Network topology does not explain the unexplained dR/dt variance. " +
                 "The remaining variance appears to be intrinsic dynamic noise."
        };

        return new TopologyReport(states, gains, best.Metric,
            best.Gain, totalR2, classification, interp);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static (double[], double) Fit3Param(double[] X1, double[] X2, double[] Y)
    {
        int n = Y.Length;
        double[,] XTX = new double[3, 3];
        double[] XTY = new double[3];
        for (int i = 0; i < n; i++)
        {
            double[] f = { 1, X1[i], X2[i] };
            for (int a = 0; a < 3; a++)
            { XTY[a] += f[a] * Y[i]; for (int b = 0; b < 3; b++) XTX[a, b] += f[a] * f[b]; }
        }
        double[] beta = SolveGauss(XTX, XTY, 3);
        double ssRes = 0, ssTot = 0, mean = Y.Average();
        for (int i = 0; i < n; i++)
        { double p = beta[0] + beta[1] * X1[i] + beta[2] * X2[i]; ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - mean) * (Y[i] - mean); }
        return (beta, ssTot > 1e-15 ? 1 - ssRes / ssTot : 0);
    }

    private static double R2Linear(double[] X, double[] Y)
    {
        double sxy = 0, sx2 = 0;
        for (int i = 0; i < X.Length; i++) { sxy += X[i] * Y[i]; sx2 += X[i] * X[i]; }
        double a = sx2 > 1e-15 ? sxy / sx2 : 0;
        double ssRes = 0, ssTot = 0, m = Y.Average();
        for (int i = 0; i < Y.Length; i++)
        { double p = a * X[i]; ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - m) * (Y[i] - m); }
        return ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
    }

    private static double GlobalR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
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
            for (int j = col; j <= n; j++) (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
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
