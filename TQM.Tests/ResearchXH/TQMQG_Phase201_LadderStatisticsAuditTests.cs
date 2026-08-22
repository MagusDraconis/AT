using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 201 — Ladder Statistics Audit. Is the 152 GeV ↔ 151.98 GeV alignment statistically
/// significant? Computed from the frozen QG192 rungs and the observed 152 GeV excess mass only.
/// No new theory, no fitting. Deterministic.
/// </summary>
public class TQMQG_Phase201_LadderStatisticsAuditTests : ResearchTestBase
{
    public TQMQG_Phase201_LadderStatisticsAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2010_DeviationAndNearestRung()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2010: observed deviation and nearest-rung geometry");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Frozen QG192 ladder (9 predicted rungs): " + string.Join(", ", LadderStatisticsAudit.FrozenRungs));
        sb.AppendLine("  - Observed excess central mass: 152.0 GeV (arXiv:2503.16245).");
        sb.AppendLine();

        double tol = LadderStatisticsAudit.Tolerance();
        double nearest = LadderStatisticsAudit.NearestRung();
        double dist = LadderStatisticsAudit.NearestRungDistance();
        double meanSpacing = LadderStatisticsAudit.MeanSpacing();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Observed excess: 152.0 GeV");
        sb.AppendLine($"  Nearest frozen rung: {nearest:F2} GeV");
        sb.AppendLine($"  Nearest-rung distance: {dist:F3} GeV");
        sb.AppendLine($"  Tolerance τ = |152.0/151.98 − 1| = {tol:P4}");
        sb.AppendLine($"  Mean nearest-neighbour spacing of the 9 rungs: {meanSpacing:F2} GeV");
        sb.AppendLine($"  Nearest-neighbour distance from 151.98: {Math.Min(Math.Abs(151.98-136.78), Math.Abs(151.98-182.38)):F2} GeV");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The 152 GeV excess sits {dist:F3} GeV from the frozen 151.98 rung — a {tol:P4} deviation.");
        sb.AppendLine("  - The ladder's typical spacing is ~15.2 GeV; the excess is ~760× closer to its rung.");
        sb.AppendLine("  - The stated '0.01%' is the rounded figure; the exact deviation is 0.0132%.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(151.98, nearest, 2);
        Assert.True(tol < 0.0002, "the deviation must be below 0.02%");
        Assert.True(dist < 0.1, "the excess must sit within 0.1 GeV of the rung");
    }

    [Fact]
    public void TQMQG2011_CoincidenceRateAndLookElsewhere()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2011: random coincidence rate and look-elsewhere correction");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Null hypothesis: the excess mass is uniform over the search range [95, 270] GeV.");
        sb.AppendLine("  - Tolerance τ = 0.0132% (exact observed deviation).");
        sb.AppendLine();

        double window = LadderStatisticsAudit.TotalWindowGeV();
        double span = LadderStatisticsAudit.SearchSpan();
        double pAny = LadderStatisticsAudit.ProbabilityAnyRung();
        double pOne = LadderStatisticsAudit.ProbabilitySingleRung151_98();
        double trial = LadderStatisticsAudit.TrialFactor();
        double zAny = LadderStatisticsAudit.ZAnyRung();
        double zOne = LadderStatisticsAudit.ZSingleRung();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Per-rung window 2·τ·E: Σ = {window:F4} GeV");
        sb.AppendLine($"  Search span: {span:F0} GeV");
        sb.AppendLine($"  p(any rung) = window/span = {pAny:P4}  →  1 in {1.0/pAny:F0}");
        sb.AppendLine($"  p(151.98 alone) = {pOne:P5}  →  1 in {1.0/pOne:F0}");
        sb.AppendLine($"  LEE trial factor (any-rung / single-rung) = {trial:F1}");
        sb.AppendLine($"  z(any rung) = {zAny:F2}σ   (look-elsewhere corrected — p_any already counts all 9 rungs)");
        sb.AppendLine($"  z(151.98 alone) = {zOne:F2}σ  (single pre-registered rung)");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The any-rung coincidence probability (0.26%, 1-in-386) is ALREADY look-elsewhere");
        sb.AppendLine("    corrected: the covered window sums all 9 rungs, so no extra trial factor applies.");
        sb.AppendLine("  - z = 2.80σ for the ladder-wide alignment; 3.50σ for the 151.98 rung in isolation.");

        Output.WriteLine(sb.ToString());

        Assert.True(pAny > 0.001 && pAny < 0.01, "any-rung probability must be in (0.001, 0.01)");
        Assert.True(pOne < 0.001, "single-rung probability must be below 0.1%");
        Assert.True(zAny >= 2.0 && zAny < 3.5, "any-rung z must be between 2 and 3.5 sigma");
        Assert.True(zOne >= 3.0, "single-rung z must be at least 3 sigma");
    }

    [Fact]
    public void TQMQG2012_ClassificationModerateSupport()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2012: classification of the 152 GeV alignment");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Bands: COINCIDENCE p>5%, WEAK 1–5%, MODERATE 0.1–1%, STRONG <0.1%.");
        sb.AppendLine("  - p_any = 0.26% → MODERATE SUPPORT (2.80σ).");
        sb.AppendLine();

        string classification = LadderStatisticsAudit.Classify();
        int score = LadderStatisticsAudit.EvidenceScore();
        double pAny = LadderStatisticsAudit.ProbabilityAnyRung();
        double zAny = LadderStatisticsAudit.ZAnyRung();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  p(any rung) = {pAny:P4}");
        sb.AppendLine($"  z(any rung) = {zAny:F2}σ");
        sb.AppendLine($"  Evidence score (max 4) = {score}");
        sb.AppendLine($"    +1 τ < 0.05% ({LadderStatisticsAudit.Tolerance():P4})");
        sb.AppendLine($"    +1 p_any < 1% ({pAny:P4})");
        sb.AppendLine($"    +1 z_any ≥ 2σ ({zAny:F2}σ)");
        sb.AppendLine($"    +1 single-rung p < 0.1% ({LadderStatisticsAudit.ProbabilitySingleRung151_98():P5})");
        sb.AppendLine($"  Classification = {classification}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The 152 GeV alignment is unlikely by chance: 0.26% (1 in 386) after look-elsewhere");
        sb.AppendLine("    over all 9 rungs, equivalent to 2.80σ.");
        sb.AppendLine("  - If the 151.98 rung had been the ONLY prediction, the coincidence would be 1 in 4375");
        sb.AppendLine("    (3.50σ); the 9-rung ladder reduces this to MODERATE.");
        sb.AppendLine("  - The alignment alone does NOT reach 5σ; it is MODERATE SUPPORT, consistent with the");
        sb.AppendLine("    ~152 GeV excess's own global significance (up to 5.4σ in the independent combination).");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("MODERATE SUPPORT", classification);
        Assert.True(score >= 3, "at least three evidence channels must hold");
    }
}
