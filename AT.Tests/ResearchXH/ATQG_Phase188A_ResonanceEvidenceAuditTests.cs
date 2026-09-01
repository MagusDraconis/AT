using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 188A — 106 GeV Resonance Evidence Audit.
/// Audits all existing published experimental evidence (ATLAS, CMS, LEP) against the QG132 prediction of a
/// primary resonance at 106.39 GeV in the 99–114 GeV window. Uses only completed AT results; no new theory,
/// no fitting. Deterministic — all values are published constants.
/// </summary>
public class ATQG_Phase188A_ResonanceEvidenceAuditTests : ResearchTestBase
{
    public ATQG_Phase188A_ResonanceEvidenceAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG188A0_SupportingExcessesAndNullSearches()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG188A0: supporting excesses vs null searches in the 99–114 GeV window");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - QG132 prediction: 106.39 GeV in search window 99–114 GeV (Z-anchor, MODERATE per QG133).");
        sb.AppendLine("  - Evidence is the published experimental record (ATLAS/CMS/LEP), treated as fixed constants.");
        sb.AppendLine();

        sb.AppendLine("SUPPORTING EXCESSES (near ~95 GeV):");
        foreach (var e in ResonanceEvidenceAudit.SupportingExcesses())
            sb.AppendLine($"  {e.Experiment,-6} {e.Channel,-4} {e.MassGeV,6:F1} GeV  local {e.LocalSigma,4:F1}σ  in window? {ResonanceEvidenceAudit.InPredictedWindow(e.MassGeV),-5}  [{e.Reference}]");
        sb.AppendLine($"  Combined γγ (ATLAS+CMS): {ResonanceEvidenceAudit.CombinedGgLocalSigma:F1}σ local, μ = {ResonanceEvidenceAudit.CombinedGgMu:F2}");
        sb.AppendLine();
        sb.AppendLine("NULL SEARCHES:");
        foreach (var n in ResonanceEvidenceAudit.NullSearches())
            sb.AppendLine($"  {n.Experiment,-6} {n.Channel,-4} {n.MassLow,3:F0}–{n.MassHigh,3:F0} GeV  limits {n.LimitLowFb:F0}–{n.LimitHighFb:F0} fb (95% CL)  [{n.Reference}]");
        sb.AppendLine($"  LEP2 hZ: SM-like Higgs excluded below {ResonanceEvidenceAudit.Lep2SmExclusion:F1} GeV (95% CL, SM-strength hZZ only)");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  excesses inside 99–114 GeV window: {ResonanceEvidenceAudit.ExcessesInWindow()}");
        sb.AppendLine($"  lowest excess ({ResonanceEvidenceAudit.SupportingExcesses().Min(e => e.MassGeV):F1} GeV) below window ({ResonanceEvidenceAudit.WindowLow:F0})? {ResonanceEvidenceAudit.ExcessBelowWindow()}");
        sb.AppendLine($"  95.3 GeV vs 106.39 prediction: dev {ResonanceEvidenceAudit.DeviationFromPrediction(95.3) * 100:F1}%");
        sb.AppendLine($"  95.3 GeV vs 91.19 lowest rung:  dev {ResonanceEvidenceAudit.DeviationFromLowestRung(95.3) * 100:F1}%");

        Output.WriteLine(sb.ToString());

        Assert.True(ResonanceEvidenceAudit.SupportingExcesses().Length >= 4, "must collect CMS/ATLAS/LEP excesses");
        Assert.True(ResonanceEvidenceAudit.NullSearches().Length >= 2, "must collect CMS and ATLAS null searches");
        Assert.True(ResonanceEvidenceAudit.ExcessBelowWindow(), "the ~95 GeV excess cluster is below the 99 GeV window");
        Assert.Equal(0, ResonanceEvidenceAudit.ExcessesInWindow());
    }

    [Fact]
    public void ATQG188A1_ExclusionStatusAndDiscoveryPotential()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG188A1: exclusion status of 106 GeV and Run-3 discovery potential");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Exclusion means the prediction is ruled out at 95% CL for ALL allowed couplings.");
        sb.AppendLine("  - Discovery requires ≥ 5σ local significance.");
        sb.AppendLine();

        bool covered = ResonanceEvidenceAudit.PredictedMassCoveredByNullSearch();
        bool excluded = ResonanceEvidenceAudit.PredictionExcluded();
        bool belowDiscovery = ResonanceEvidenceAudit.BelowDiscoveryThreshold();

        sb.AppendLine("EXCLUSION STATUS:");
        sb.AppendLine($"  predicted mass 106.39 GeV covered by a full-Run-2 diphoton null search? {covered}");
        sb.AppendLine($"  → CMS 70–110 GeV and ATLAS 66–110 GeV both cover the predicted mass");
        sb.AppendLine($"  → no excess at 106 GeV in either; limits ≈20–50 fb in 100–110 GeV");
        sb.AppendLine($"  → LEP2 excludes SM-like Higgs < 114.4 GeV, but ONLY for SM-strength hZZ coupling");
        sb.AppendLine($"  prediction excluded? {excluded}");
        sb.AppendLine();
        sb.AppendLine("DISCOVERY POTENTIAL (Run 3 → HL-LHC):");
        sb.AppendLine($"  combined 95 GeV excess {ResonanceEvidenceAudit.CombinedGgLocalSigma:F1}σ < 5σ discovery threshold? {belowDiscovery}");
        sb.AppendLine("  Run 3 (2022–2025): no confirmed increase in the 95 GeV significance to date");
        sb.AppendLine("  HL-LHC (~late 2020s, ~5× luminosity): will probe the 99–114 GeV window with the sensitivity");
        sb.AppendLine("  to confirm or exclude a suppressed-coupling scalar");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The 106 GeV prediction is NOT excluded: the null searches set limits, but a");
        sb.AppendLine("    suppressed-coupling sector-ladder scalar remains allowed.");
        sb.AppendLine("  - Discovery is NOT claimed: the strongest hint (95 GeV γγ, 3.1σ combined) is below 5σ.");
        sb.AppendLine("  - HL-LHC is the decisive experiment for the 99–114 GeV window.");

        Output.WriteLine(sb.ToString());

        Assert.True(covered, "the predicted window must be covered by full-Run-2 null searches");
        Assert.False(excluded, "the prediction is not excluded (coupling-suppressed states allowed)");
        Assert.True(belowDiscovery, "the excess is below the 5σ discovery threshold");
    }

    [Fact]
    public void ATQG188A2_ClassificationInconclusive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG188A2: evidence classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - SUPPORTED requires a confirmed excess at the predicted 106 GeV (≥5σ, inside window).");
        sb.AppendLine("  - DISFAVORED requires the prediction to be excluded for all couplings.");
        sb.AppendLine("  - Otherwise INCONCLUSIVE.");
        sb.AppendLine();

        int score = ResonanceEvidenceAudit.EvidenceScore();
        string classification = ResonanceEvidenceAudit.Classify();
        bool alignsLowRung = ResonanceEvidenceAudit.ExcessAlignsWithLowestRung();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  EvidenceScore (max 3) = {score}");
        sb.AppendLine($"    +1 supporting low-mass scalar excess cluster (3.1σ combined γγ, near 95 GeV)");
        sb.AppendLine($"    +1 predicted window has no confirmed excess AND is not excluded");
        sb.AppendLine($"    +1 excess aligns with the 91.19 GeV ladder rung, NOT the 106 GeV prediction");
        sb.AppendLine($"  excess aligns with lowest rung (91.19 GeV)? {alignsLowRung}");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The experimental record shows a persistent low-mass scalar excess cluster at ~95 GeV");
        sb.AppendLine("  (CMS γγ 2.9σ, ATLAS γγ 1.7σ, combined 3.1σ; CMS ττ 2.6σ; LEP bb̄ 2.3σ), consistent with");
        sb.AppendLine("  the lowest ladder rung 91.19 GeV (dev 4.0%, QG131) — NOT with the predicted 106 GeV rung.");
        sb.AppendLine("  The 99–114 GeV window itself has no confirmed excess (CMS/ATLAS full-Run-2 diphoton)");
        sb.AppendLine("  but is not excluded (limits ≈20–50 fb allow suppressed couplings).");
        sb.AppendLine($"  ⇒ {classification} — evidence for a sector-ladder scalar at ~95 GeV, but the specific");
        sb.AppendLine("    106 GeV prediction is neither confirmed nor excluded; HL-LHC is decisive.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("INCONCLUSIVE", classification);
        Assert.True(score == 3, "all three evidence channels should be present");
    }
}
