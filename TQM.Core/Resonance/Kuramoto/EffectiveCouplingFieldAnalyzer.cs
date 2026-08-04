namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether network topology can be compressed into a
/// small set of effective coupling-field variables.
///
/// TQM-081: Effective Coupling Field
/// </summary>
public static class EffectiveCouplingFieldAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A single data point: R, dR/dt, and all effective coupling
    /// field variables measured from one topology configuration.
    /// </summary>
    public sealed record CouplingFieldDescriptor(
        string TopologyType,
        int Seed,
        double R,
        double dRdt,
        double MeanCoupling,
        double CouplingVariance,
        double CouplingEntropy,
        double SpectralGap,
        double MeanDegree,
        double DegreeVariance,
        double SpatialClustering);

    /// <summary>
    /// Report for a single reduced prediction model.
    /// </summary>
    public sealed record ReducedModelReport(
        string ModelLabel,
        string Variables,
        int NumPredictors,
        double R2,
        double AdjustedR2,
        double GainVsNull,
        double InformationRetention);

    /// <summary>
    /// Full result of the compression analysis.
    /// </summary>
    public sealed record CompressionAnalysisResult(
        string Classification,
        double BestSingleVarRetention,
        double Best2VarRetention,
        double Best3VarRetention,
        List<ReducedModelReport> Models,
        double[,] CorrelationMatrix,
        string[] VariableNames,
        double FullTopologyR2,
        double NullR2,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Variable names (ordered)
    // ══════════════════════════════════════════════════════════════════

    public static readonly string[] TopologyVarNames =
    {
        "MeanCoupling", "CouplingVar", "MeanDegree",
        "DegreeVar", "SpectralGap", "CouplingEntropy", "SpatialClustering"
    };

    // ══════════════════════════════════════════════════════════════════
    // Convert TopologyState to CouplingFieldDescriptor
    // ══════════════════════════════════════════════════════════════════

    public static List<CouplingFieldDescriptor> FromTopologyStates(
        List<TopologyEvolutionAnalyzer.TopologyState> states)
    {
        return states.Select(s => new CouplingFieldDescriptor(
            s.TopologyType, s.Seed, s.R, s.dRdt,
            s.MeanCoupling, s.CouplingVariance, s.CouplingEntropy,
            s.SpectralGap, s.MeanDegree, s.DegreeVariance, s.SpatialClustering))
            .ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Main analysis
    // ══════════════════════════════════════════════════════════════════

    public static CompressionAnalysisResult Analyze(
        List<CouplingFieldDescriptor> descriptors)
    {
        int n = descriptors.Count;
        double[] dRdt = descriptors.Select(d => d.dRdt).ToArray();
        double[] R = descriptors.Select(d => d.R).ToArray();

        // Extract topology variables.
        var topoVars = new (string Name, double[] Values)[]
        {
            ("MeanCoupling",     descriptors.Select(d => d.MeanCoupling).ToArray()),
            ("CouplingVar",      descriptors.Select(d => d.CouplingVariance).ToArray()),
            ("MeanDegree",       descriptors.Select(d => d.MeanDegree).ToArray()),
            ("DegreeVar",        descriptors.Select(d => d.DegreeVariance).ToArray()),
            ("SpectralGap",      descriptors.Select(d => d.SpectralGap).ToArray()),
            ("CouplingEntropy",  descriptors.Select(d => d.CouplingEntropy).ToArray()),
            ("SpatialClustering",descriptors.Select(d => d.SpatialClustering).ToArray()),
        };

        // ── Model A: R only ──────────────────────────────────────
        double r2A = FitAndR2(new[] { R }, dRdt);
        double adjR2A = AdjustedR2(r2A, n, 1);

        // ── Model B: R + MeanCoupling ────────────────────────────
        double[] mc = topoVars[0].Values;
        double r2B = FitAndR2(new[] { R, mc }, dRdt);
        double adjR2B = AdjustedR2(r2B, n, 2);

        // ── Model C: R + MeanCoupling + CouplingVariance ─────────
        double[] cv = topoVars[1].Values;
        double r2C = FitAndR2(new[] { R, mc, cv }, dRdt);
        double adjR2C = AdjustedR2(r2C, n, 3);

        // ── Model D: R + MeanCoupling + Variance + Entropy ───────
        double[] ce = topoVars[5].Values;
        double r2D = FitAndR2(new[] { R, mc, cv, ce }, dRdt);
        double adjR2D = AdjustedR2(r2D, n, 4);

        // ── Model D': R + MeanCoupling + Variance + Entropy + SpectralGap
        double[] sg = topoVars[4].Values;
        double r2Dp = FitAndR2(new[] { R, mc, cv, ce, sg }, dRdt);
        double adjR2Dp = AdjustedR2(r2Dp, n, 5);

        // ── Model E: Full topology (R + all 7 topology vars) ─────
        var allPredictors = new List<double[]> { R };
        foreach (var tv in topoVars) allPredictors.Add(tv.Values);
        double r2E = FitAndR2(allPredictors.ToArray(), dRdt);
        double adjR2E = AdjustedR2(r2E, n, 8);

        // Gains.
        double gainFull = r2E - r2A;

        // Information retention per model.
        double retainB = gainFull > 1e-10 ? (r2B - r2A) / gainFull : 0;
        double retainC = gainFull > 1e-10 ? (r2C - r2A) / gainFull : 0;
        double retainD = gainFull > 1e-10 ? (r2D - r2A) / gainFull : 0;
        double retainDp = gainFull > 1e-10 ? (r2Dp - r2A) / gainFull : 0;
        double retainE = 1.0; // full model by definition

        var models = new List<ReducedModelReport>
        {
            new("A",  "R",                                           1, r2A,  adjR2A,  0,         0),
            new("B",  "R + MeanCoupling",                            2, r2B,  adjR2B,  r2B - r2A, retainB),
            new("C",  "R + MeanCoupling + CouplingVar",              3, r2C,  adjR2C,  r2C - r2A, retainC),
            new("D",  "R + MeanCoupling + CouplingVar + Entropy",    4, r2D,  adjR2D,  r2D - r2A, retainD),
            new("D'", "R + MC + Var + Entropy + SpectralGap",        5, r2Dp, adjR2Dp, r2Dp - r2A,retainDp),
            new("E",  "R + All 7 topology variables",                8, r2E,  adjR2E,  gainFull,   retainE),
        };

        // ── Single-variable analysis ─────────────────────────────
        var singleVarRetentions = new List<(string Name, double Retention)>();
        foreach (var tv in topoVars)
        {
            double r2Single = FitAndR2(new[] { R, tv.Values }, dRdt);
            double ret = gainFull > 1e-10 ? (r2Single - r2A) / gainFull : 0;
            singleVarRetentions.Add((tv.Name, ret));
        }
        singleVarRetentions = singleVarRetentions.OrderByDescending(x => x.Retention).ToList();
        double best1 = singleVarRetentions[0].Retention;

        // ── Best 2-variable retention ────────────────────────────
        double best2 = 0;
        for (int i = 0; i < topoVars.Length; i++)
            for (int j = i + 1; j < topoVars.Length; j++)
            {
                double r2ij = FitAndR2(new[] { R, topoVars[i].Values, topoVars[j].Values }, dRdt);
                double ret = gainFull > 1e-10 ? (r2ij - r2A) / gainFull : 0;
                if (ret > best2) best2 = ret;
            }

        // ── Best 3-variable retention ────────────────────────────
        double best3 = 0;
        for (int i = 0; i < topoVars.Length; i++)
            for (int j = i + 1; j < topoVars.Length; j++)
                for (int k = j + 1; k < topoVars.Length; k++)
                {
                    double r2ijk = FitAndR2(new[] { R, topoVars[i].Values, topoVars[j].Values, topoVars[k].Values }, dRdt);
                    double ret = gainFull > 1e-10 ? (r2ijk - r2A) / gainFull : 0;
                    if (ret > best3) best3 = ret;
                }

        // ── Correlation matrix ───────────────────────────────────
        int nVars = topoVars.Length;
        double[,] corr = new double[nVars, nVars];
        for (int i = 0; i < nVars; i++)
            for (int j = 0; j < nVars; j++)
                corr[i, j] = PearsonR(topoVars[i].Values, topoVars[j].Values);

        string[] varNames = topoVars.Select(v => v.Name).ToArray();

        // ── Classification ───────────────────────────────────────
        string classification;
        string interpretation;

        if (retainB >= 0.85)
        {
            classification = "D: Effective Coupling Field";
            interpretation =
                "MEAN COUPLING ALONE captures ≥85% of topology information. " +
                "The detailed network is NOT fundamental — a SINGLE effective " +
                $"field variable (MeanCoupling, {retainB * 100:F0}% retention) " +
                "is sufficient. An effective coupling field EXISTS and replaces " +
                "the full network with negligible loss.";
        }
        else if (retainB >= 0.55 || retainC >= 0.80)
        {
            classification = "C: Strong Compression";
            interpretation =
                $"Topology compresses STRONGLY: {retainC * 100:F0}% of full " +
                "topology information is captured by 2-3 effective variables. " +
                "The network structure can be largely replaced by a compact " +
                "coupling-field descriptor without losing most predictive power.";
        }
        else if (retainD >= 0.65 || best3 >= 0.70)
        {
            classification = "B: Weak Compression";
            interpretation =
                $"Topology shows WEAK compressibility: 3-4 effective " +
                $"variables capture {best3 * 100:F0}% of information. " +
                "A reduced descriptor helps but significant information " +
                "resides in the detailed network structure.";
        }
        else
        {
            classification = "A: Topology Irreducible";
            interpretation =
                "TOPOLOGY IS IRREDUCIBLE. Even with 4+ effective variables, " +
                $"information retention is only {retainD * 100:F0}%. " +
                "The detailed network structure carries unique information " +
                "that cannot be compressed into a small set of field variables. " +
                "No effective coupling field exists — the full coupling matrix matters.";
        }

        return new CompressionAnalysisResult(
            classification, best1, best2, best3,
            models, corr, varNames, r2E, r2A, interpretation);
    }

    // ══════════════════════════════════════════════════════════════
    // Multi-variable linear regression
    // ══════════════════════════════════════════════════════════════

    /// <summary>
    /// Fits Y = β₀ + Σᵢ βᵢ₊₁·Xᵢ and returns R².
    /// </summary>
    private static double FitAndR2(double[][] predictors, double[] Y)
    {
        int n = Y.Length;
        int p = predictors.Length;     // number of predictor variables
        int m = p + 1;                  // +1 for intercept

        double[,] XTX = new double[m, m];
        double[] XTY = new double[m];

        for (int i = 0; i < n; i++)
        {
            double[] f = new double[m];
            f[0] = 1.0;
            for (int k = 0; k < p; k++)
                f[k + 1] = predictors[k][i];

            for (int a = 0; a < m; a++)
            {
                XTY[a] += f[a] * Y[i];
                for (int b = 0; b < m; b++)
                    XTX[a, b] += f[a] * f[b];
            }
        }

        double[] beta = SolveGauss(XTX, XTY, m);

        double ssRes = 0, ssTot = 0, mean = Y.Average();
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            for (int k = 0; k < p; k++)
                pred += beta[k + 1] * predictors[k][i];
            ssRes += (Y[i] - pred) * (Y[i] - pred);
            ssTot += (Y[i] - mean) * (Y[i] - mean);
        }

        return ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0.0;
    }

    private static double AdjustedR2(double r2, int n, int p)
    {
        // p = number of predictor variables (excluding intercept)
        if (n <= p + 1) return r2;
        return 1.0 - (1.0 - r2) * (n - 1) / (n - p - 1);
    }

    // ══════════════════════════════════════════════════════════════
    // Pearson correlation
    // ══════════════════════════════════════════════════════════════

    private static double PearsonR(double[] X, double[] Y)
    {
        int n = X.Length;
        double mx = X.Average(), my = Y.Average();
        double sxy = 0, sx2 = 0, sy2 = 0;
        for (int i = 0; i < n; i++)
        {
            double dx = X[i] - mx, dy = Y[i] - my;
            sxy += dx * dy;
            sx2 += dx * dx;
            sy2 += dy * dy;
        }
        double denom = Math.Sqrt(sx2 * sy2);
        return denom > 1e-15 ? sxy / denom : 0;
    }

    // ══════════════════════════════════════════════════════════════
    // Gaussian elimination solver
    // ══════════════════════════════════════════════════════════════

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
