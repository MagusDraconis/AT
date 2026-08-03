using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Discovers which hidden state variable, beyond coherence R,
/// is required to predict coherence evolution dR/dt.
///
/// TQM-079: Hidden State Variable Discovery
/// </summary>
public static class HiddenStateAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record StateDescriptor(
        double R,
        double dRdt,
        double H1_PhaseVariance,
        double H2_PhaseEntropy,
        double H3_Fourier2,
        double H4_Fourier3,
        double H5_ClusterCount,
        double H6_LocalCohVariance,
        double H7_PairwiseMoment,
        double H8_Multimodality,
        int Seed);

    public sealed record FeatureGain(
        string Name,
        double R2_Base,       // R² with R only
        double R2_With,       // R² with R + feature
        double Gain,          // improvement
        double MutualInfo);   // mutual information with dR/dt

    public sealed record HiddenStateReport(
        List<StateDescriptor> Descriptors,
        List<FeatureGain> Gains,
        string BestFeature,
        double BestGain,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Generate random states and measure dR/dt
    // ══════════════════════════════════════════════════════════════════

    public static List<StateDescriptor> GenerateEnsemble(
        double k, double lambda, int n, int numStates, int baseSeed,
        int evolutionSteps = 10)
    {
        var descriptors = new List<StateDescriptor>();

        for (int s = 0; s < numStates; s++)
        {
            int seed = baseSeed + s * 7919;
            var rng = new Random(seed);
            var network = new TemporalNetwork(n);

            // Random positions in [0,1]².
            for (int i = 0; i < n; i++)
                network.AddNode(new TemporalNode(i,
                    rng.NextDouble() * 2 * Math.PI, 1.0)
                { X = rng.NextDouble(), Y = rng.NextDouble() });

            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

            // Measure initial state.
            double initR = GlobalR(network);
            var h = ComputeHiddenVariables(network);

            // Run short evolution.
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

            descriptors.Add(new StateDescriptor(initR, dr,
                h.h1, h.h2, h.h3, h.h4, h.h5, h.h6, h.h7, h.h8, s));
        }

        return descriptors;
    }

    // ══════════════════════════════════════════════════════════════════
    // Hidden variable computation
    // ══════════════════════════════════════════════════════════════════

    private static (double h1, double h2, double h3, double h4,
        double h5, double h6, double h7, double h8)
        ComputeHiddenVariables(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double[] phases = net.Nodes.Select(nd => nd.Phase).ToArray();

        // H1: Phase variance (circular variance = 1 - R).
        double r = GlobalR(net);
        double h1 = 1.0 - r;

        // H2: Phase entropy (histogram-based, 20 bins).
        int bins = 20;
        int[] hist = new int[bins];
        for (int i = 0; i < n; i++)
        {
            int b = (int)(phases[i] / (2 * Math.PI) * bins);
            if (b >= bins) b = bins - 1;
            hist[b]++;
        }
        double h2 = 0;
        for (int b = 0; b < bins; b++)
        {
            double p = (double)hist[b] / n;
            if (p > 0) h2 -= p * Math.Log(p);
        }

        // H3, H4: Second and third Fourier mode amplitudes.
        double sumCos2 = 0, sumSin2 = 0, sumCos3 = 0, sumSin3 = 0;
        for (int i = 0; i < n; i++)
        {
            sumCos2 += Math.Cos(2 * phases[i]); sumSin2 += Math.Sin(2 * phases[i]);
            sumCos3 += Math.Cos(3 * phases[i]); sumSin3 += Math.Sin(3 * phases[i]);
        }
        double h3 = Math.Sqrt(sumCos2 * sumCos2 + sumSin2 * sumSin2) / n;
        double h4 = Math.Sqrt(sumCos3 * sumCos3 + sumSin3 * sumSin3) / n;

        // H5: Cluster count (number of phase clusters using DBSCAN-like threshold).
        double threshold = Math.PI / 4;
        var sorted = phases.OrderBy(p => p).ToList();
        int clusters = 1;
        for (int i = 1; i < n; i++)
        {
            double gap = sorted[i] - sorted[i - 1];
            if (gap > threshold) clusters++;
        }
        double h5 = clusters;

        // H6: Local coherence variance (std of per-oscillator R).
        double[] localRs = new double[n];
        for (int i = 0; i < n; i++)
        {
            double ss = 0, sc = 0; int count = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double pd = TemporalSimulation.NormalizePhase(phases[j] - phases[i]);
                ss += Math.Sin(pd); sc += Math.Cos(pd); count++;
            }
            localRs[i] = Math.Sqrt(ss * ss + sc * sc) / count;
        }
        double meanLR = localRs.Average();
        double h6 = Math.Sqrt(localRs.Average(lr => (lr - meanLR) * (lr - meanLR)));

        // H7: Pairwise phase-distance moment (variance of cos(Δθ)).
        double sumCos = 0, sumCosSq = 0; int pairs = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double cd = Math.Cos(phases[j] - phases[i]);
                sumCos += cd; sumCosSq += cd * cd; pairs++;
            }
        double meanCos = sumCos / pairs;
        double h7 = Math.Sqrt(Math.Max(0, sumCosSq / pairs - meanCos * meanCos));

        // H8: Multimodality score (Hartigan's dip test approximation).
        // Ratio of variance of sorted differences to total variance.
        var diffs = new List<double>();
        for (int i = 1; i < n; i++)
            diffs.Add(sorted[i] - sorted[i - 1]);
        diffs.Add(2 * Math.PI - sorted[^1] + sorted[0]); // wrap-around
        double meanDiff = diffs.Average();
        double h8 = Math.Sqrt(diffs.Average(d => (d - meanDiff) * (d - meanDiff))) /
                    Math.Max(meanDiff, 1e-10);

        return (h1, h2, h3, h4, h5, h6, h7, h8);
    }

    // ══════════════════════════════════════════════════════════════════
    // Feature importance analysis
    // ══════════════════════════════════════════════════════════════════

    public static HiddenStateReport AnalyzeHiddenStates(
        List<StateDescriptor> data)
    {
        double[] R = data.Select(d => d.R).ToArray();
        double[] dRdt = data.Select(d => d.dRdt).ToArray();
        int n = data.Count;

        // Baseline: predict dR/dt from R only.
        double r2Base = R2Linear(R, dRdt);

        // Feature names and extractors.
        var features = new (string Name, Func<StateDescriptor, double> Get)[]
        {
            ("H1 PhaseVar", d => d.H1_PhaseVariance),
            ("H2 Entropy", d => d.H2_PhaseEntropy),
            ("H3 Fourier2", d => d.H3_Fourier2),
            ("H4 Fourier3", d => d.H4_Fourier3),
            ("H5 Clusters", d => d.H5_ClusterCount),
            ("H6 LocalCohVar", d => d.H6_LocalCohVariance),
            ("H7 PairMoment", d => d.H7_PairwiseMoment),
            ("H8 Multimod", d => d.H8_Multimodality),
        };

        var gains = new List<FeatureGain>();
        foreach (var (fname, get) in features)
        {
            // Predict dR/dt from [R, feature].
            double[] fVals = data.Select(get).ToArray();
            var (_, r2With) = FitPoly2(R, fVals, dRdt);
            double mi = MutualInfo(fVals, dRdt);
            gains.Add(new FeatureGain(fname, r2Base, r2With,
                r2With - r2Base, mi));
        }

        gains = gains.OrderByDescending(g => g.Gain).ToList();
        var best = gains[0];

        string classification = best.Gain > 0.10 ? "D: Hidden Variable Found" :
                                best.Gain > 0.05 ? "C: Significant Hidden State" :
                                best.Gain > 0.02 ? "B: Weak Hidden State" :
                                "A: No Hidden State";

        string interp = classification switch
        {
            "D: Hidden Variable Found" =>
                $"{best.Name} carries significant additional information " +
                $"(ΔR²=+{best.Gain:F3}). A two-parameter state description " +
                "(R, {best.Name}) substantially improves predictions.",
            "C: Significant Hidden State" =>
                $"{best.Name} improves dR/dt prediction (ΔR²=+{best.Gain:F3}). " +
                "Coherence R compresses but does not lose essential dynamics.",
            "B: Weak Hidden State" =>
                "A weak hidden state exists but adds limited predictive power.",
            _ => "No hidden state variable significantly improves dR/dt prediction. " +
                 "Coherence evolution may be genuinely stochastic at this level."
        };

        return new HiddenStateReport(data, gains, best.Name,
            best.Gain, classification, interp);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double R2Linear(double[] X, double[] Y)
    {
        double sumXY = 0, sumX2 = 0;
        for (int i = 0; i < X.Length; i++) { sumXY += X[i] * Y[i]; sumX2 += X[i] * X[i]; }
        double a = sumX2 > 1e-15 ? sumXY / sumX2 : 0;
        double ssRes = 0, ssTot = 0, mean = Y.Average();
        for (int i = 0; i < Y.Length; i++)
        { double p = a * X[i]; ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - mean) * (Y[i] - mean); }
        return ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
    }

    private static (double[] beta, double r2) FitPoly2(
        double[] X1, double[] X2, double[] Y)
    {
        // Y = β₀ + β₁·X1 + β₂·X2 via normal equations.
        int n = Y.Length;
        double[,] XTX = new double[3, 3];
        double[] XTY = new double[3];
        for (int i = 0; i < n; i++)
        {
            double[] f = { 1, X1[i], X2[i] };
            for (int a = 0; a < 3; a++)
            { XTY[a] += f[a] * Y[i]; for (int b = 0; b < 3; b++) XTX[a, b] += f[a] * f[b]; }
        }
        var beta = SolveGauss(XTX, XTY, 3);
        double ssRes = 0, ssTot = 0, mean = Y.Average();
        for (int i = 0; i < n; i++)
        { double p = beta[0] + beta[1] * X1[i] + beta[2] * X2[i]; ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - mean) * (Y[i] - mean); }
        return (beta, ssTot > 1e-15 ? 1 - ssRes / ssTot : 0);
    }

    private static double MutualInfo(double[] X, double[] Y, int bins = 15)
    {
        int n = X.Length;
        if (n < 2) return 0;
        double xMin = X.Min(), xMax = X.Max(), xR = xMax - xMin + 1e-15;
        double yMin = Y.Min(), yMax = Y.Max(), yR = yMax - yMin + 1e-15;
        int[,] joint = new int[bins, bins];
        int[] mx = new int[bins], my = new int[bins];
        for (int i = 0; i < n; i++)
        {
            int bx = Math.Clamp((int)((X[i] - xMin) / xR * bins), 0, bins - 1);
            int by = Math.Clamp((int)((Y[i] - yMin) / yR * bins), 0, bins - 1);
            joint[bx, by]++; mx[bx]++; my[by]++;
        }
        double mi = 0;
        for (int bx = 0; bx < bins; bx++)
            for (int by = 0; by < bins; by++)
            {
                if (joint[bx, by] == 0) continue;
                double pxy = (double)joint[bx, by] / n;
                double px = (double)mx[bx] / n, py = (double)my[by] / n;
                mi += pxy * Math.Log(pxy / (px * py));
            }
        return mi / Math.Log(2);
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
