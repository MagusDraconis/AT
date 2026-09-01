using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 199 — P1 Evidence Update. Re-audits the published experimental record for the P1
/// pre-registered prediction (106.39 GeV, window 99–114 GeV) as of the search cut-off. Evidence only,
/// no theory, no fitting, cited sources. Classification: PENDING / SUPPORTED / DISFAVORED / CONFIRMED /
/// FALSIFIED. Deterministic.
/// </summary>
public class ATQG_Phase199_P1EvidenceUpdateTests : ResearchTestBase
{
    public ATQG_Phase199_P1EvidenceUpdateTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1990_SupportiveAndNullEvidenceCatalogued()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1990: supportive evidence and null searches (cited)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Evidence-only audit; every number is a published constant with a citation.");
        sb.AppendLine("  - P1 prediction: 106.39 GeV central, window 99–114 GeV (QG132/QG190).");
        sb.AppendLine();

        var excesses = P1EvidenceUpdate.SupportingExcesses();
        sb.AppendLine("SUPPORTIVE EXCESSES (classic low-mass cluster, ~95 GeV):");
        foreach (var e in excesses)
            sb.AppendLine($"  {e.Experiment,-7} {e.Channel,-4} {e.MassGeV,7:F1} GeV  local {e.LocalSigma,4:F1}σ   [{e.Reference}]");
        sb.AppendLine($"  Combined γγ (ATLAS+CMS, neglecting correlations): {P1EvidenceUpdate.CombinedGgLocalSigma:F1}σ local, μ = {P1EvidenceUpdate.CombinedGgMu:F2}");
        sb.AppendLine($"  NEW ~152 GeV narrow diphoton excess (multi-channel): local {P1EvidenceUpdate.Excess152LocalSigma:F1}σ, global {P1EvidenceUpdate.Excess152GlobalSigma:F1}σ  [arXiv:2503.16245]");
        sb.AppendLine();

        var nulls = P1EvidenceUpdate.NullSearches();
        sb.AppendLine("NULL SEARCHES IN THE P1 WINDOW (99–114 GeV):");
        foreach (var n in nulls)
            sb.AppendLine($"  {n.Experiment,-6} {n.Channel,-3} {n.MassLow,3:F0}–{n.MassHigh,3:F0} GeV  limits {n.LimitLowFb,3:F0}–{n.LimitHighFb,3:F0} fb  [{n.Reference}]");
        sb.AppendLine($"  LEP2 SM-like hZ exclusion: m_H &lt; {P1EvidenceUpdate.Lep2SmExclusion:F1} GeV (95% CL, SM coupling only)");
        sb.AppendLine();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Excesses inside the P1 window: {P1EvidenceUpdate.ExcessesInP1Window()}");
        sb.AppendLine($"  All classic excesses below the window: {P1EvidenceUpdate.ExcessesBelowWindow()}");
        sb.AppendLine($"  95 GeV ↔ 91.19 rung dev: {P1EvidenceUpdate.DeviationFromLowestRung(excesses[0].MassGeV):P2}");
        sb.AppendLine($"  95 GeV ↔ P1 106.39 dev:   {P1EvidenceUpdate.DeviationFromPrediction(excesses[0].MassGeV):P2}");
        sb.AppendLine($"  152 GeV ↔ 151.98 rung dev: {P1EvidenceUpdate.DeviationFromRung152(P1EvidenceUpdate.Excess152MassGeV):P4}");
        sb.AppendLine($"  P1 covered by null searches: {P1EvidenceUpdate.P1CoveredByNullSearches()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Supporting low-mass scalar evidence exists, but at ~95 GeV (the 91.19 GeV rung),");
        sb.AppendLine("    NOT inside the P1 window.");
        sb.AppendLine("  - A new ~152 GeV excess aligns with the NEXT ladder rung 151.98 GeV (0.01% dev).");
        sb.AppendLine("  - No confirmed signal appears in the 99–114 GeV window.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(4, excesses.Length);
        Assert.True(P1EvidenceUpdate.ExcessesInP1Window() == 0, "no supporting excess is inside the P1 window");
        Assert.True(P1EvidenceUpdate.ExcessesBelowWindow(), "all classic excesses are below 99 GeV");
        Assert.True(P1EvidenceUpdate.P1CoveredByNullSearches(), "the 106.39 GeV central mass is covered by null searches");
    }

    [Fact]
    public void ATQG1991_ExclusionStatusAndHlLhcProspect()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1991: exclusion status and HL-LHC discovery potential");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Exclusion status is read from the published 95% CL upper limits.");
        sb.AppendLine("  - HL-LHC projection: 3000 fb⁻¹ reaches ~1–3 fb in the 100–106 GeV range (central 2 fb).");
        sb.AppendLine();

        sb.AppendLine("EXCLUSION STATUS:");
        sb.AppendLine($"  CMS diphoton limit 70–110 GeV: 15–73 fb (95% CL)   [CMS-HIG-20-002]");
        sb.AppendLine($"  ATLAS diphoton limit 66–110 GeV: 19–102 fb (95% CL) [arXiv:2407.07546]");
        sb.AppendLine($"  LEP2 hZ exclusion: < {P1EvidenceUpdate.Lep2SmExclusion:F1} GeV at SM coupling only");
        sb.AppendLine($"  P1 excluded? {P1EvidenceUpdate.P1Excluded()}");
        sb.AppendLine($"  → The 106 GeV prediction is NOT excluded: limits ≈15–102 fb allow a suppressed-coupling scalar.");
        sb.AppendLine();

        sb.AppendLine("HL-LHC PROSPECT:");
        sb.AppendLine($"  Projected σ×BR(γγ) sensitivity at 100–106 GeV: {P1EvidenceUpdate.HlLhcProjectedSensitivityFb:F1} fb (3000 fb⁻¹)");
        sb.AppendLine($"  Min current limit: {P1EvidenceUpdate.NullSearches().Min(n => n.LimitLowFb):F1} fb");
        sb.AppendLine($"  HL-LHC decisive for the window? {P1EvidenceUpdate.HlLhcIsDecisive()}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The P1 window is currently NOT excluded; suppressed couplings remain allowed.");
        sb.AppendLine("  - HL-LHC sensitivity (~2 fb) is an order of magnitude below the current limits,");
        sb.AppendLine("    so the 99–114 GeV window becomes decisive at 3000 fb⁻¹.");

        Output.WriteLine(sb.ToString());

        Assert.False(P1EvidenceUpdate.P1Excluded(), "P1 must not be excluded by current data");
        Assert.True(P1EvidenceUpdate.HlLhcIsDecisive(), "HL-LHC sensitivity must be below current limits");
    }

    [Fact]
    public void ATQG1992_ClassificationStaysPending()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1992: P1 classification — PENDING");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Registry rule (QG193): P1 is PENDING until CONFIRMED / DISFAVORED / FALSIFIED.");
        sb.AppendLine("  - CONFIRMED: a signal inside 99–114 GeV at ≥5σ — none exists.");
        sb.AppendLine("  - DISFAVORED / FALSIFIED: an exclusion in the window — none exists (LEP2 is SM-coupling only).");
        sb.AppendLine();

        string classification = P1EvidenceUpdate.Classify();
        int score = P1EvidenceUpdate.EvidenceScore();
        bool pending = P1EvidenceUpdate.P1StillPending();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Evidence score (0..4) = {score}");
        sb.AppendLine($"    +1 no excess inside the P1 window ({P1EvidenceUpdate.ExcessesInP1Window()})");
        sb.AppendLine($"    +1 P1 not excluded ({P1EvidenceUpdate.P1Excluded()})");
        sb.AppendLine($"    +1 95 GeV aligns with the 91.19 rung ({P1EvidenceUpdate.ExcessAlignsWithLowestRung()})");
        sb.AppendLine($"    +1 152 GeV aligns with the 151.98 rung ({P1EvidenceUpdate.ExcessAlignsWithRung152()})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - P1 remains PENDING: the 99–114 GeV window is neither confirmed nor excluded.");
        sb.AppendLine("  - Supporting scalar evidence exists at OTHER ladder rungs (95 GeV ↔ 91.19 rung,");
        sb.AppendLine("    152 GeV ↔ 151.98 rung) but none inside the P1 window.");
        sb.AppendLine("  - The frozen prediction and its registry outcome are unchanged (no CONFIRMED /");
        sb.AppendLine("    DISFAVORED / FALSIFIED may be written yet).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PENDING", classification);
        Assert.True(pending, "P1 must remain PENDING");
        Assert.True(score >= 3, "at least three evidence channels must hold (empty window, not excluded, rung alignment)");
    }
}
