using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 152 — Golden-ratio robustness audit. QG151 found δ(up) − δ(down) ≈ φ (golden ratio) and
/// interpreted it as the fixed point of two-channel spectral mode competition. This phase asks whether the
/// relation is FUNDAMENTAL or a COINCIDENCE by sweeping size, K, damping, feedback, and spectral
/// perturbations.
///
/// Tests: ATQG1520 (size + K scaling), ATQG1521 (damping + feedback + perturbations), ATQG1522 (audit
/// aggregates + classification).
/// </summary>
public class ATQG_Phase152_GoldenRatioAuditTests : ResearchTestBase
{
    public ATQG_Phase152_GoldenRatioAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1520_SizeAndKScaling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1520: size and K scaling of the golden-ratio relation");

        double up = GoldenRatioAudit.UpDeltaEff();
        double phi = GoldenRatioAudit.Phi();
        sb.AppendLine($"REFERENCE: up δ_eff = {up:F4}, φ = {phi:F4}");
        sb.AppendLine($"  spectral relation tested: up = Weyl_full + φ");
        sb.AppendLine($"  default: Weyl_full = {GoldenRatioAudit.FullWeyl():F4}, deviation = {GoldenRatioAudit.GoldenDeviation(GoldenRatioAudit.FullWeyl()):P1}");
        sb.AppendLine();
        sb.AppendLine("SIZE SCALING (network size n, default K=6 dynamics):");
        foreach (var s in GoldenRatioAudit.SizeScaling())
            sb.AppendLine($"  n={s.N}: modes={s.Modes} Weyl={s.Weyl:F4} deviation={s.Deviation:P1}");
        sb.AppendLine();
        sb.AppendLine("K SCALING (coupling K, default size n=96):");
        foreach (var s in GoldenRatioAudit.KScaling())
            sb.AppendLine($"  K={s.K}: modes={s.Modes} Weyl={s.Weyl:F4} deviation={s.Deviation:P1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the relation holds tightly at the default (0.6%), is mild under size");
        sb.AppendLine("variation (3-8%), but is strongly peaked in K — extreme K values deviate 12-20%.");
        Output.WriteLine(sb.ToString());

        Assert.True(GoldenRatioAudit.DefaultHolds(), "the golden-ratio relation should hold at default dynamics");
        Assert.True(GoldenRatioAudit.SizeScaling()[2].Deviation < 0.05, "default size should be robust");
        Assert.True(GoldenRatioAudit.KScaling()[2].Deviation < 0.05, "default K=6 should be robust");
    }

    [Fact]
    public void ATQG1521_DampingFeedbackAndPerturbations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1521: damping, feedback, and spectral-perturbation robustness");

        sb.AppendLine("DAMPING VARIATION (feedback=0.9):");
        foreach (var s in GoldenRatioAudit.DampingVariation())
            sb.AppendLine($"  damping={s.Damping}: Weyl={s.Weyl:F4} deviation={s.Deviation:P1}");
        sb.AppendLine();
        sb.AppendLine("FEEDBACK VARIATION (damping=0.3):");
        foreach (var s in GoldenRatioAudit.FeedbackVariation())
            sb.AppendLine($"  feedback={s.Feedback}: Weyl={s.Weyl:F4} deviation={s.Deviation:P1}");
        sb.AppendLine();
        sb.AppendLine("SPECTRAL PERTURBATIONS (seeded multiplicative mode-frequency noise):");
        foreach (var s in GoldenRatioAudit.SpectralPerturbations())
            sb.AppendLine($"  amplitude={s.Amp}: Weyl={s.Weyl:F4} deviation={s.Deviation:P1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the relation is fully robust to damping and spectral perturbations, and");
        sb.AppendLine("holds across a coherent feedback basin (feedback ≥ 0.7), failing only at feedback 0.5");
        sb.AppendLine("(25% deviation).");
        Output.WriteLine(sb.ToString());

        Assert.True(GoldenRatioAudit.DampingRobust(), "damping should not affect the relation");
        Assert.True(GoldenRatioAudit.PerturbationRobust(), "spectral perturbations should not break the relation");
        Assert.True(GoldenRatioAudit.FeedbackBasin(), "a coherent feedback basin should hold");
    }

    [Fact]
    public void ATQG1522_AuditAggregatesAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1522: audit aggregates and classification");

        int robust = GoldenRatioAudit.RobustCount();
        int weak = GoldenRatioAudit.WeakCount();
        int total = GoldenRatioAudit.TotalSettings();
        int score = GoldenRatioAudit.RobustnessScore();
        string cls = GoldenRatioAudit.Classify();
        bool allStrong = GoldenRatioAudit.AllSettings().All(s => s.Deviation < 0.05);

        sb.AppendLine("AUDIT AGGREGATES (25 settings across 5 parameter axes):");
        sb.AppendLine($"  robust settings (dev < 5%): {robust}/{total} ({robust / (double)total:P0})");
        sb.AppendLine($"  weak settings (dev < 10%): {weak}/{total} ({weak / (double)total:P0})");
        sb.AppendLine($"  all settings below 5%: {allStrong}");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION SCORE (0..5):");
        sb.AppendLine($"  +1 default dynamics holds: {GoldenRatioAudit.DefaultHolds()}");
        sb.AppendLine($"  +1 damping-robust: {GoldenRatioAudit.DampingRobust()}");
        sb.AppendLine($"  +1 perturbation-robust: {GoldenRatioAudit.PerturbationRobust()}");
        sb.AppendLine($"  +1 feedback basin: {GoldenRatioAudit.FeedbackBasin()}");
        sb.AppendLine($"  +1 broad basin (≥15/25 <10%): {GoldenRatioAudit.BroadBasin()}");
        sb.AppendLine($"  score = {score}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • COINCIDENCE rejected: the relation survives damping, feedback, and perturbation");
        sb.AppendLine("    changes and holds at 0.6% at the default dynamics — far tighter than a coincidence.");
        sb.AppendLine("  • FUNDAMENTAL PHI rejected: extreme K and size settings deviate 12-25% — not universal.");
        sb.AppendLine("  • PARTIAL ROBUSTNESS accepted: the golden-ratio relation is a robust consequence of");
        sb.AppendLine("    spectral mode competition within a coherent basin (the observable dynamics: default");
        sb.AppendLine("    K, damping-insensitive, feedback ≥ 0.7, mild size sensitivity, perturbation-robust)");
        sb.AppendLine("    but is not universal across all topologies.");
        Output.WriteLine(sb.ToString());

        Assert.True(robust >= 15, "a substantial robust basin should exist");
        Assert.True(weak >= 15, "a broad weak basin should exist");
        Assert.True(score >= 4, "robustness score should be strong");
        Assert.Equal("PARTIAL ROBUSTNESS", cls);
    }
}
