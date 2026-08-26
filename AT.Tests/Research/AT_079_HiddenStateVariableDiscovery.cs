using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_079_HiddenStateVariableDiscovery : ResearchTestBase
{
    private const double K = 2.0;
    private const double Lambda = 0.05;
    private const int N = 100;
    private const int BaseSeed = 790531847;
    private const int NumStates = 200;

    public AT_079_HiddenStateVariableDiscovery(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_079_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-079 Hidden State Variable Discovery");

        sb.AppendLine("AT-079: What Hidden Variable Determines Coherence Evolution?");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  AT-078: dR/dt not predictable from R and K alone.");
        sb.AppendLine("  Different phase configurations with identical R");
        sb.AppendLine("  evolve differently.");
        sb.AppendLine();
        sb.AppendLine("  Hypothesis: Coherence R compresses phase information.");
        sb.AppendLine("  A hidden state variable contains the missing dynamics.");
        sb.AppendLine();

        // ── Section 2: Setup ─────────────────────────────────────────
        Sec(sb, "2. Experimental Setup");
        sb.AppendLine($"  {NumStates} random phase configurations, N={N}, K={K}");
        sb.AppendLine($"  10-step evolution, measure dR/dt");
        sb.AppendLine();
        sb.AppendLine("  8 candidate hidden variables tested:");
        sb.AppendLine("    H1: Phase variance (1-R, circular)");
        sb.AppendLine("    H2: Phase entropy (20-bin histogram)");
        sb.AppendLine("    H3: Second Fourier mode amplitude");
        sb.AppendLine("    H4: Third Fourier mode amplitude");
        sb.AppendLine("    H5: Cluster count (phase gaps > π/4)");
        sb.AppendLine("    H6: Local coherence variance");
        sb.AppendLine("    H7: Pairwise cos(Δθ) moment variance");
        sb.AppendLine("    H8: Multimodality score");
        sb.AppendLine();

        // ── Run ──────────────────────────────────────────────────────
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var data = HiddenStateAnalyzer.GenerateEnsemble(
            K, Lambda, N, NumStates, BaseSeed);
        var report = HiddenStateAnalyzer.AnalyzeHiddenStates(data);
        sw.Stop();

        sb.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine();

        // ── Section 3: Identical-R Ensembles ─────────────────────────
        Sec(sb, "3. Sample States (Similar R, Different Structures)");

        // Show a few states with similar R.
        var byR = data.OrderBy(d => d.R).ToList();
        sb.AppendLine("  R      │ dR/dt   │ H1      │ H2     │ H3     │ H5     │ H8");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        for (int i = 0; i < 8; i++)
        {
            var d = byR[i * (byR.Count / 8)];
            sb.AppendLine($"  {d.R,5:F3} │ {d.dRdt,6:F4} │ {d.H1_PhaseVariance,6:F4} │ {d.H2_PhaseEntropy,5:F3} │ {d.H3_Fourier2,5:F4} │ {d.H5_ClusterCount,5:F0} │ {d.H8_Multimodality,6:F3}");
        }
        sb.AppendLine();

        // Show that same R can have different dR/dt.
        var midR = data.Where(d => d.R > 0.08 && d.R < 0.12).Take(5).ToList();
        sb.AppendLine("  States with R ≈ 0.10 but different futures:");
        sb.AppendLine("  R      │ dR/dt   │ H3(F2)  │ H5(Clst) │ H7(Mom)");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");
        foreach (var d in midR)
            sb.AppendLine($"  {d.R,5:F3} │ {d.dRdt,6:F4} │ {d.H3_Fourier2,7:F4} │ {d.H5_ClusterCount,8:F0} │ {d.H7_PairwiseMoment,7:F4}");
        sb.AppendLine();

        // ── Section 4: Hidden Variable Analysis ──────────────────────
        Sec(sb, "4. Feature Importance Ranking");

        double r2Base = R2Linear(data.Select(d => d.R).ToArray(),
                                  data.Select(d => d.dRdt).ToArray());
        sb.AppendLine($"  Baseline R² (R only): {r2Base:F4}");
        sb.AppendLine();
        sb.AppendLine("  Rank │ Feature          │ R²(R+H)  │ Gain     │ MI (bits)");
        sb.AppendLine("  \u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500\u2500");

        int rank = 0;
        foreach (var g in report.Gains)
        {
            rank++;
            string star = rank == 1 ? " \u2605" : "  ";
            string sign = g.Gain >= 0 ? "+" : "";
            sb.AppendLine($"  {rank,3}{star} │ {g.Name,-16} │ {g.R2_With,6:F4} │ {sign}{g.Gain,7:F4} │ {g.MutualInfo,8:F4}");
        }
        sb.AppendLine();

        // ── Section 5: Prediction Improvements ───────────────────────
        Sec(sb, "5. Prediction Improvement");

        sb.AppendLine($"  Best hidden variable: {report.BestFeature}");
        sb.AppendLine($"  Gain over R-only: ΔR² = {report.BestGain:+0.0000}");
        sb.AppendLine();

        double improvement = report.BestGain / Math.Max(Math.Abs(r2Base), 1e-10) * 100;
        if (r2Base > 0)
            sb.AppendLine($"  Relative improvement: {improvement:F0}%");
        sb.AppendLine();

        // ── Research Questions ───────────────────────────────────────
        sb.AppendLine("  Q1: Which hidden variable best predicts dR/dt?");
        sb.AppendLine($"    {report.BestFeature} (ΔR² = {report.BestGain:+0.0000})");
        sb.AppendLine();

        sb.AppendLine("  Q2: Can two states with identical R have different futures?");
        int distinctFutures = data.GroupBy(d => Math.Round(d.R, 2))
            .Count(g => g.Select(x => Math.Round(x.dRdt, 5)).Distinct().Count() > 1);
        sb.AppendLine($"    R-bins with multiple dR/dt values: {distinctFutures}");
        sb.AppendLine($"    {(distinctFutures > 0 ? "YES — Same R, different evolution" : "NO — R determines evolution uniquely")}");
        sb.AppendLine();

        sb.AppendLine("  Q3: What information is lost compressing to coherence?");
        var top2 = report.Gains.Take(2).ToList();
        sb.AppendLine($"    Top lost info: {top2[0].Name} (MI={top2[0].MutualInfo:F4} bits)");
        sb.AppendLine($"    2nd: {top2[1].Name} (MI={top2[1].MutualInfo:F4} bits)");
        sb.AppendLine();

        sb.AppendLine("  Q4: Can a low-dimensional state replace full simulation?");
        double bestR2 = report.Gains[0].R2_With;
        sb.AppendLine($"    Best 2-variable R²: {bestR2:F4}");
        sb.AppendLine($"    {(bestR2 > 0.80 ? "YES — Low-dim state nearly replaces simulation" : bestR2 > 0.50 ? "PARTIALLY — Modest replacement" : "NO — Full simulation required")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Is there a second order parameter besides coherence?");
        sb.AppendLine($"    {report.BestFeature} gain = {report.BestGain:+.F4}");
        sb.AppendLine($"    {(report.BestGain > 0.05 ? "YES — A second order parameter exists" : "NO — Coherence is the primary order parameter")}");
        sb.AppendLine();

        // ── Section 6: Interpretation ────────────────────────────────
        Sec(sb, "6. Interpretation");
        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 7: Conclusion ────────────────────────────────────
        Sec(sb, "7. Conclusion");
        sb.AppendLine($"  C1. Classification: {report.Classification}");
        sb.AppendLine($"  C2. Best hidden variable: {report.BestFeature}");
        sb.AppendLine($"  C3. Gain over R-only: {report.BestGain:+0.0000}");
        sb.AppendLine($"  C4. Best 2-var R²: {bestR2:F4}");
        sb.AppendLine($"  C5. Total states analyzed: {data.Count}");
        sb.AppendLine();
        sb.AppendLine($"  C6. {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment AT-079 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
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

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
