using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_103_UniversalityAndScaleInvariance : ResearchTestBase
{
    private const int BaseSeed = 103_000_001;

    public TQM_103_UniversalityAndScaleInvariance(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_103_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-103 Universality and Scale Invariance");

        sb.AppendLine("TQM-103: Can scale transformations make {R, M} universal?");
        sb.AppendLine("         Searching for M* = M·N^β·K^γ·λ^δ");
        sb.AppendLine();

        // ── Section 1: Objective ─────────────────────────────────────
        Sec(sb, "1. Objective");
        sb.AppendLine("  TQM-100/101/102: {R, M} theory fails at extreme N, K, λ.");
        sb.AppendLine("  Hypothesis: The failure is NOT missing variables —");
        sb.AppendLine("  it's missing SCALE TRANSFORMATIONS.");
        sb.AppendLine();
        sb.AppendLine("  If raw {R, M} fails but renormalized {R*, M*} succeeds,");
        sb.AppendLine("  then the theory IS correct — it just needs proper scaling.");
        sb.AppendLine();
        sb.AppendLine("  This experiment searches for:");
        sb.AppendLine("    M* = M · N^β · K^γ · λ^δ");
        sb.AppendLine("  via grid search over β,γ,δ ∈ [-2, 2].");
        sb.AppendLine();

        // ── Section 2: Data Generation ───────────────────────────────
        Sec(sb, "2. Scaling Data");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var data = UniversalityAnalyzer.GenerateScalingData(BaseSeed);
        sw.Stop();

        sb.AppendLine($"  {data.Count} points in {sw.ElapsedMilliseconds} ms.");
        sb.AppendLine($"  N ∈ [10..1000], K ∈ [0.01..10], λ ∈ [0.005..0.5]");
        sb.AppendLine($"  R ∈ [{data.Min(d => d.R):F4}, {data.Max(d => d.R):F4}]");
        sb.AppendLine($"  M ∈ [{data.Min(d => d.M):F6}, {data.Max(d => d.M):F3}]");
        sb.AppendLine($"  dR/dt ∈ [{data.Min(d => d.dRdt):F6}, {data.Max(d => d.dRdt):F6}]");
        sb.AppendLine();

        // ── Section 3: Universality Search ───────────────────────────
        Sec(sb, "3. Universality Search");

        var report = UniversalityAnalyzer.SearchUniversality(data, BaseSeed);

        sb.AppendLine("  Rank │ Renormalization                    │ Collapse R² │ Quality  │ Assessment");
        sb.AppendLine("  " + new string('─', 95));
        foreach (var r in report.Results)
        {
            string assess = r.CollapseQuality >= 2.0 ? "★ STRONG" :
                            r.CollapseQuality >= 1.2 ? "↑ Measurable" :
                            r.CollapseQuality >= 1.05 ? "≈ Marginal" : "↓ Worse";
            sb.AppendLine($"  {r.Rank,3}   │ {r.State.Formula,-35} │ {r.CollapseR2,10:F4} │ {r.CollapseQuality,6:F2}× │ {assess}");
        }
        sb.AppendLine();

        sb.AppendLine($"  Raw (unscaled) R²: {report.BestResult.RawR2:F4}");
        sb.AppendLine($"  Best collapse R²: {report.BestResult.CollapseR2:F4}");
        sb.AppendLine($"  Collapse quality: {report.BestResult.CollapseQuality:F2}×");
        sb.AppendLine();
        sb.AppendLine($"  Optimal exponents: β={report.BestResult.State.BetaN:F2}, γ={report.BestResult.State.GammaK:F2}, δ={report.BestResult.State.DeltaLam:F2}");
        sb.AppendLine($"  Optimal formula: {report.BestResult.State.Formula}");
        sb.AppendLine();

        // ── Section 4: Validation ────────────────────────────────────
        Sec(sb, "4. Validation with Renormalized Variables");

        sb.AppendLine("  Attack                  │ R²       │ Pass?");
        sb.AppendLine("  " + new string('─', 50));
        foreach (var (name, (r2, passedTest)) in report.Validation)
            sb.AppendLine($"  {name,-22} │ {r2,7:F4} │ {(passedTest ? "✓" : "✗ FAIL")}");
        sb.AppendLine();

        int nPassed = report.Validation.Count(v => v.Value.Passed);
        sb.AppendLine($"  Survival: {nPassed}/8 ({report.SurvivalRate:P0})");
        sb.AppendLine();

        // ── Comparison vs TQM-101 ────────────────────────────────────
        sb.AppendLine("  ── Comparison: TQM-101(D) vs TQM-103(renormalized) ──");
        sb.AppendLine("  Attack                  │ TQM-101   │ TQM-103   │ Δ");
        sb.AppendLine("  " + new string('─', 65));
        var t101 = new Dictionary<string, double>
        {
            ["Extreme Coherence"] = 0.279, ["Extreme M"] = 0.035,
            ["Mixed Topologies"] = 0.478, ["Coupling Laws"] = 0.903,
            ["Phase Noise"] = -0.154, ["Large-N N=500"] = -3.825,
            ["Small-N N=10"] = 0.114, ["Out-of-Distribution"] = -4.003
        };
        foreach (var (name, (r2, _)) in report.Validation)
        {
            double t101r2 = t101.GetValueOrDefault(name, 0);
            double delta = r2 - t101r2;
            string marker = delta > 0.05 ? "↑" : delta < -0.05 ? "↓" : "≈";
            sb.AppendLine($"  {name,-22} │ {t101r2,8:F3}  │ {r2,7:F3}  │ {delta,8:F3} {marker}");
        }
        sb.AppendLine();

        // ── Section 5: Research Questions ────────────────────────────
        Sec(sb, "5. Research Questions");

        sb.AppendLine("  Q1: Is N-dependence removable by rescaling?");
        double betaOpt = report.BestResult.State.BetaN;
        if (Math.Abs(betaOpt) > 0.1)
            sb.AppendLine($"    YES — optimal β = {betaOpt:F2}. N-dependence is a scaling artifact.");
        else
            sb.AppendLine("    NO — β ≈ 0, N-dependence is not removable by scaling M alone.");
        sb.AppendLine();

        sb.AppendLine("  Q2: Is K-dependence removable by rescaling?");
        double gammaOpt = report.BestResult.State.GammaK;
        if (Math.Abs(gammaOpt) > 0.1)
            sb.AppendLine($"    YES — optimal γ = {gammaOpt:F2}. K-dependence is a scaling artifact.");
        else
            sb.AppendLine("    NO — γ ≈ 0, K-dependence is not removable by scaling M.");
        sb.AppendLine();

        sb.AppendLine("  Q3: Can a scale-invariant description be found?");
        sb.AppendLine($"    Collapse quality: {report.BestResult.CollapseQuality:F2}×");
        if (report.BestResult.CollapseQuality >= 1.5)
            sb.AppendLine("    STRONG EVIDENCE — data from all regimes collapses onto a single curve.");
        else if (report.BestResult.CollapseQuality >= 1.1)
            sb.AppendLine("    MODERATE — some collapse, but significant residual scatter.");
        else
            sb.AppendLine("    WEAK/NONE — scaling does not substantially improve collapse.");
        sb.AppendLine();

        sb.AppendLine("  Q4: Do all topologies collapse onto the same curve?");
        sb.AppendLine("    Topologies are NOT included in the renormalization (only N,K,λ).");
        sb.AppendLine("    The Mixed Topologies test provides the answer:");
        double topoR2 = report.Validation["Mixed Topologies"].R2;
        sb.AppendLine($"    {(topoR2 > 0.3 ? $"YES — topology independence confirmed (R²={topoR2:F3})." : "NO — topologies differ even after renormalization.")}");
        sb.AppendLine();

        sb.AppendLine("  Q5: Are R and M the correct variables after renormalization?");
        sb.AppendLine($"    Best formula: {report.BestResult.State.Formula}");
        sb.AppendLine($"    Best equation: dR/dt = {report.BestResult.Coefficients[0]:F4} " +
                       $"+ {report.BestResult.Coefficients[1]:F4}·R " +
                       $"+ {report.BestResult.Coefficients[2]:F4}·M*");
        sb.AppendLine($"    {(report.SurvivalRate >= 0.625 ? "YES — renormalized {R, M} works across regimes." : "PARTIALLY — renormalization helps but doesn't fully resolve issues.")}");
        sb.AppendLine();

        sb.AppendLine("  Q6: Can the TQM-100 failures be explained by scale effects?");
        int stillFailed = report.Validation.Count(v => !v.Value.Passed);
        sb.AppendLine($"    Failures before renormalization: 7/8 (12% survival)");
        sb.AppendLine($"    Failures after renormalization:  {stillFailed}/8 ({report.SurvivalRate:P0} survival)");
        if (stillFailed < 5)
            sb.AppendLine("    PARTIALLY — renormalization fixes some failures but not all.");
        else
            sb.AppendLine("    NO — renormalization does not materially change the failure pattern.");
        sb.AppendLine();

        // ── Section 6: Renormalized Theory ───────────────────────────
        Sec(sb, "6. Renormalized Effective Theory");

        sb.AppendLine("  ┌─────────────────────────────────────────────────────┐");
        sb.AppendLine("  │  RENORMALIZED EFFECTIVE THEORY                      │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  State: {{R, M*}}                                    │");
        sb.AppendLine($"  │  M* = {report.BestResult.State.Formula,-42} │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  dR/dt = {report.BestResult.Coefficients[0],8:F4} " +
                       $"+ {report.BestResult.Coefficients[1],8:F4}·R " +
                       $"+ {report.BestResult.Coefficients[2],8:F4}·M* │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────┤");
        sb.AppendLine($"  │  Collapse quality: {report.BestResult.CollapseQuality:F2}×                            │");
        sb.AppendLine($"  │  Validation: {nPassed}/8 ({report.SurvivalRate:P0})                                  │");
        sb.AppendLine("  └─────────────────────────────────────────────────────┘");
        sb.AppendLine();

        // ── Section 7: Classification ────────────────────────────────
        Sec(sb, "7. Classification");

        sb.AppendLine($"  Classification: {report.Classification}");
        sb.AppendLine();
        sb.AppendLine($"  {report.Interpretation}");
        sb.AppendLine();

        // ── Section 8: Conclusion ────────────────────────────────────
        Sec(sb, "8. Conclusion");
        sb.AppendLine($"  C1.  Best scaling: {report.BestResult.State.Formula}");
        sb.AppendLine($"  C2.  Exponents: β={report.BestResult.State.BetaN:F2}, γ={report.BestResult.State.GammaK:F2}, δ={report.BestResult.State.DeltaLam:F2}");
        sb.AppendLine($"  C3.  Collapse R²: {report.BestResult.CollapseR2:F4} (raw: {report.BestResult.RawR2:F4}, quality: {report.BestResult.CollapseQuality:F2}×)");
        sb.AppendLine($"  C4.  Validation: {nPassed}/8 ({report.SurvivalRate:P0})");
        sb.AppendLine($"  C5.  Classification: {report.Classification}");
        sb.AppendLine($"  C6.  Data: {data.Count} points, N∈[10..1000], K∈[0.01..10], λ∈[0.005..0.5]");
        sb.AppendLine();
        sb.AppendLine($"  C7.  {report.Interpretation}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  Experiment TQM-103 completed successfully.");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    {
        sb.AppendLine(t);
        sb.AppendLine(new string('-', t.Length));
    }
}
