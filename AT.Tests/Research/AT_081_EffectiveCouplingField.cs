using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_081_EffectiveCouplingField : ResearchTestBase
{
    private const double K = 2.0;
    private const double Lambda = 0.05;
    private const int N = 100;
    private const int BaseSeed = 810473291;
    private const int NumConfigs = 180;

    public AT_081_EffectiveCouplingField(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_081_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-081 Effective Coupling Field");

        sb.AppendLine("AT-081: Can Network Topology Be Compressed into Effective Field Variables?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-080 demonstrated: topology explains 63% of dR/dt variance.");
        sb.AppendLine("  R + MeanCoupling: R² = 0.739.");
        sb.AppendLine("  Topology is the dominant missing factor.");
        sb.AppendLine();
        sb.AppendLine("  This experiment asks: does the FULL network matter, or can");
        sb.AppendLine("  a small set of effective coupling-field variables replace it?");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: A small number of effective variables —");
        sb.AppendLine("  MeanCoupling, CouplingVariance, CouplingEntropy, SpectralGap —");
        sb.AppendLine("  are sufficient. The detailed network is not fundamental.");
        sb.AppendLine();

        // ── Section 2: Experimental Setup ────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  {NumConfigs} configurations, N={N}, K={K}, λ={Lambda}");
        sb.AppendLine("  6 topology types: uniform, clustered, linear, circular,");
        sb.AppendLine("    dense-sparse, random-clusters");
        sb.AppendLine("  10-step Kuramoto evolution, measure dR/dt");
        sb.AppendLine();
        sb.AppendLine("  7 topology metrics measured per configuration:");
        sb.AppendLine("    MeanCoupling, CouplingVariance, MeanDegree, DegreeVariance,");
        sb.AppendLine("    SpectralGap, CouplingEntropy, SpatialClustering");
        sb.AppendLine();
        sb.AppendLine("  5 prediction models (A through E):");
        sb.AppendLine("    A:  R only (null model)");
        sb.AppendLine("    B:  R + MeanCoupling");
        sb.AppendLine("    C:  R + MeanCoupling + CouplingVariance");
        sb.AppendLine("    D:  R + MeanCoupling + CouplingVariance + CouplingEntropy");
        sb.AppendLine("    D': R + MC + Var + Entropy + SpectralGap");
        sb.AppendLine("    E:  R + All 7 topology variables (full model)");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var states = TopologyEvolutionAnalyzer.GenerateTopologyEnsemble(
            K, Lambda, N, NumConfigs, BaseSeed);
        var descriptors = EffectiveCouplingFieldAnalyzer.FromTopologyStates(states);
        var result = EffectiveCouplingFieldAnalyzer.Analyze(descriptors);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Topology Measurements ─────────────────────────
        Sec(sb, "3. Topology Measurements");

        var byType = descriptors.GroupBy(d => d.TopologyType).ToList();
        sb.AppendLine("  Topology Type       │ Count │ Mean R  │ Mean dR/dt │ MeanCoupling │ CouplingEntropy │ SpectralGap");
        sb.AppendLine("  " + new string('─', 120));
        foreach (var g in byType)
            sb.AppendLine($"  {g.Key,-19} │ {g.Count(),4} │ {g.Average(d => d.R),6:F4} │ {g.Average(d => d.dRdt),9:F5} │ {g.Average(d => d.MeanCoupling),11:F4} │ {g.Average(d => d.CouplingEntropy),14:F4} │ {g.Average(d => d.SpectralGap),10:F4}");
        sb.AppendLine();

        // ── Section 4: Effective Variables ───────────────────────────
        Sec(sb, "4. Effective Coupling Field Variables");

        sb.AppendLine("  Variable correlations with dR/dt:");
        sb.AppendLine("  Rank │ Variable           │ r(dR/dt)  │ r(R)     ");
        sb.AppendLine("  " + new string('─', 60));

        var varNames = EffectiveCouplingFieldAnalyzer.TopologyVarNames;
        var stats = new List<(string Name, double CorDR, double CorR)>();
        for (int i = 0; i < varNames.Length; i++)
        {
            double[] vals = descriptors.Select(d => GetVarValue(d, i)).ToArray();
            double[] rs = descriptors.Select(d => d.R).ToArray();
            double[] drs = descriptors.Select(d => d.dRdt).ToArray();
            stats.Add((varNames[i], PearsonR(vals, drs), PearsonR(vals, rs)));
        }
        stats = stats.OrderByDescending(s => Math.Abs(s.CorDR)).ToList();

        int rank = 0;
        foreach (var s in stats)
        {
            rank++;
            string sign = s.CorDR >= 0 ? "+" : "";
            sb.AppendLine($"  {rank,3}  │ {s.Name,-18} │ {sign}{s.CorDR,7:F4}  │ {s.CorR,7:F4}");
        }
        sb.AppendLine();

        // ── Section 5: Reduced Models ────────────────────────────────
        Sec(sb, "5. Reduced Model Analysis");

        sb.AppendLine("  Model │ Variables                           │  P │    R²   │ Adj R²  │ ΔR²(null) │ Info Retention");
        sb.AppendLine("  " + new string('─', 110));
        foreach (var m in result.Models)
        {
            string sign = m.GainVsNull >= 0 ? "+" : "";
            sb.AppendLine($"  {m.ModelLabel,-5} │ {m.Variables,-35} │ {m.NumPredictors,2} │ {m.R2,6:F4} │ {m.AdjustedR2,6:F4} │ {sign}{m.GainVsNull,8:F4} │ {m.InformationRetention,11:P1}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Null model (A) R²:        {result.NullR2:F4}");
        sb.AppendLine($"  Full topology (E) R²:     {result.FullTopologyR2:F4}");
        sb.AppendLine($"  Full topology gain:       {result.FullTopologyR2 - result.NullR2:+0.0000}");
        sb.AppendLine();

        // ── Section 6: Compression Analysis ──────────────────────────
        Sec(sb, "6. Compression Analysis");

        sb.AppendLine("  ── Single-Variable Information Retention ──");
        sb.AppendLine("  Variable           │ Retention");
        sb.AppendLine("  " + new string('─', 45));

        var singleRet = new List<(string Name, double Ret)>();
        for (int i = 0; i < varNames.Length; i++)
        {
            double[] vals = descriptors.Select(d => GetVarValue(d, i)).ToArray();
            double[] rs = descriptors.Select(d => d.R).ToArray();
            double[] drs = descriptors.Select(d => d.dRdt).ToArray();
            double r2Single = FitR2Multi(new[] { rs, vals }, drs);
            double gainFull = result.FullTopologyR2 - result.NullR2;
            double ret = gainFull > 1e-10 ? (r2Single - result.NullR2) / gainFull : 0;
            singleRet.Add((varNames[i], ret));
        }
        singleRet = singleRet.OrderByDescending(x => x.Ret).ToList();

        foreach (var sr in singleRet)
            sb.AppendLine($"  {sr.Name,-18} │ {sr.Ret,6:P1}");

        sb.AppendLine();
        sb.AppendLine($"  Best single variable:     {singleRet[0].Name} ({singleRet[0].Ret:P1})");
        sb.AppendLine($"  Best 2-variable combo:    {result.Best2VarRetention:P1}");
        sb.AppendLine($"  Best 3-variable combo:    {result.Best3VarRetention:P1}");
        sb.AppendLine();

        // ── Correlation matrix ───────────────────────────────────────
        sb.AppendLine("  ── Inter-Variable Correlation Matrix ──");
        sb.Append("  " + new string(' ', 18));
        for (int j = 0; j < result.VariableNames.Length; j++)
            sb.Append($" {result.VariableNames[j][..Math.Min(8, result.VariableNames[j].Length)],8}");
        sb.AppendLine();
        sb.AppendLine("  " + new string('─', 90));

        for (int i = 0; i < result.VariableNames.Length; i++)
        {
            sb.Append($"  {result.VariableNames[i],-16} │");
            for (int j = 0; j < result.VariableNames.Length; j++)
                sb.Append($" {result.CorrelationMatrix[i, j],8:F3}");
            sb.AppendLine();
        }
        sb.AppendLine();

        // ── Section 7: Research Questions ────────────────────────────
        Sec(sb, "7. Research Questions");

        sb.AppendLine("  Q1: How much information is contained in MeanCoupling?");
        double mcRet = singleRet.First(s => s.Name == "MeanCoupling").Ret;
        sb.AppendLine($"    MeanCoupling alone retains {mcRet:P1} of full topology info.");
        sb.AppendLine($"    R²(R+MC) = {result.Models[1].R2:F4} vs R²(full) = {result.FullTopologyR2:F4}");
        sb.AppendLine($"    {(mcRet >= 0.80 ? "MEANCOUPLING IS SUFFICIENT — it is the dominant descriptor." : mcRet >= 0.50 ? "Meancoupling is important but not sufficient alone." : "MeanCoupling is not a strong single descriptor.")}");
        sb.AppendLine();

        sb.AppendLine("  Q2: What is the minimal effective descriptor?");
        // Find the smallest model with >80% retention.
        int bestMinimal = 0;
        for (int i = 1; i < result.Models.Count - 1; i++)
            if (result.Models[i].InformationRetention >= 0.80)
            { bestMinimal = i; break; }
        if (bestMinimal > 0)
            sb.AppendLine($"    Model {result.Models[bestMinimal].ModelLabel}: {result.Models[bestMinimal].Variables}");
        else
            sb.AppendLine("    No reduced model achieves 80% retention — full topology needed.");
        sb.AppendLine($"    Best 3-variable retention: {result.Best3VarRetention:P1}");
        sb.AppendLine();

        sb.AppendLine("  Q3: Can full topology be compressed?");
        double compRatio = result.Best3VarRetention;
        if (compRatio >= 0.90)
            sb.AppendLine($"    YES — 3 variables capture {compRatio:P1} of full topology information.");
        else if (compRatio >= 0.70)
            sb.AppendLine($"    PARTIALLY — 3 variables capture {compRatio:P1}, moderate loss.");
        else
            sb.AppendLine($"    NO — 3 variables only capture {compRatio:P1}, significant loss.");
        sb.AppendLine();

        sb.AppendLine("  Q4: What fraction of variance survives compression?");
        sb.AppendLine($"    Model B (1 var):  {result.Models[1].InformationRetention:P1} retention");
        sb.AppendLine($"    Model C (2 vars): {result.Models[2].InformationRetention:P1} retention");
        sb.AppendLine($"    Model D (3 vars): {result.Models[3].InformationRetention:P1} retention");
        sb.AppendLine($"    Model D' (4 vars):{result.Models[4].InformationRetention:P1} retention");
        sb.AppendLine();

        sb.AppendLine("  Q5: Does an effective coupling field exist?");
        sb.AppendLine($"    Classification: {result.Classification}");
        sb.AppendLine($"    {result.Interpretation}");
        sb.AppendLine();

        // ── Additional detail: Adjusted R² comparison ─────────────────
        sb.AppendLine("  ── Adjusted R² (penalizing model complexity) ──");
        sb.AppendLine("  Model │ Adj R²  │ Penalty");
        sb.AppendLine("  " + new string('─', 40));
        for (int i = 0; i < result.Models.Count; i++)
        {
            double penalty = result.Models[i].R2 - result.Models[i].AdjustedR2;
            sb.AppendLine($"  {result.Models[i].ModelLabel,-5} │ {result.Models[i].AdjustedR2,7:F4} │ {penalty,7:F4}");
        }
        sb.AppendLine();

        // ── Section 8: Interpretation ────────────────────────────────
        Sec(sb, "8. Interpretation");
        sb.AppendLine($"  Classification: {result.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {result.Interpretation}");
        sb.AppendLine();

        // Detailed per-variable breakdown.
        sb.AppendLine("  ── Variable Contribution Breakdown ──");
        sb.AppendLine("  Variable           │ Solo Gain │ Cumulative Retention");
        sb.AppendLine("  " + new string('─', 60));
        foreach (var sr in singleRet)
            sb.AppendLine($"  {sr.Name,-18} │ {sr.Ret,7:P1} │ N/A (solo)");

        sb.AppendLine();

        // ── Section 9: Conclusion ────────────────────────────────────
        Sec(sb, "9. Conclusion");
        sb.AppendLine($"  C1. Classification: {result.Classification}");
        sb.AppendLine($"  C2. Null (R-only) R²: {result.NullR2:F4}");
        sb.AppendLine($"  C3. Full topology R²: {result.FullTopologyR2:F4}");
        sb.AppendLine($"  C4. Topology gain: {result.FullTopologyR2 - result.NullR2:+0.0000}");
        sb.AppendLine($"  C5. Best single variable: {singleRet[0].Name} ({singleRet[0].Ret:P1} retention)");
        sb.AppendLine($"  C6. Best 2-variable: {result.Best2VarRetention:P1} retention");
        sb.AppendLine($"  C7. Best 3-variable: {result.Best3VarRetention:P1} retention");
        sb.AppendLine($"  C8. Model B (R+MC) retention: {result.Models[1].InformationRetention:P1}");
        sb.AppendLine($"  C9. Model C (R+MC+Var) retention: {result.Models[2].InformationRetention:P1}");
        sb.AppendLine($"  C10.Model D (R+MC+Var+Ent) retention: {result.Models[3].InformationRetention:P1}");
        sb.AppendLine($"  C11.Configurations: {descriptors.Count}");
        sb.AppendLine();
        sb.AppendLine($"  C12.{result.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-081 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double GetVarValue(
        EffectiveCouplingFieldAnalyzer.CouplingFieldDescriptor d, int index) => index switch
    {
        0 => d.MeanCoupling,
        1 => d.CouplingVariance,
        2 => d.MeanDegree,
        3 => d.DegreeVariance,
        4 => d.SpectralGap,
        5 => d.CouplingEntropy,
        6 => d.SpatialClustering,
        _ => 0
    };

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

    private static double FitR2Multi(double[][] predictors, double[] Y)
    {
        int n = Y.Length;
        int p = predictors.Length;
        int m = p + 1;

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
        return ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
    }

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

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
