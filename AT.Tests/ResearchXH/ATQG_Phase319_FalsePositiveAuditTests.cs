using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 319 — False Positive Audit. QG317: the early lock structure predicted the future
/// maturity class 8/8. This phase stress-tests that predictor: can the lock identity be FAKED [locks
/// present while organization is absent] or MISSED [organization present while locks are absent]?
/// Generate 1000 deterministic synthetic systems attempting BOTH failure modes and measure the honest
/// false-positive and false-negative rates of the QG317 lock rule [coherence ≥ 0.10 → organized].
/// Deterministic, no observables, no target values.
/// </summary>
public class ATQG_Phase319_FalsePositiveAuditTests : ResearchTestBase
{
    public ATQG_Phase319_FalsePositiveAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3190_TheContingencyTable()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3190: the 1000-system contingency table");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - 500 systems attempt to FAKE locks [locks present, organization absent];");
        sb.AppendLine("  - 500 systems attempt to MISS locks [organization present, locks absent];");
        sb.AppendLine("  - the QG317 rule [lock coherence ≥ 0.10 → organized] is scored against the truth.");
        sb.AppendLine();

        sb.AppendLine($"TP={FalsePositiveAudit.TruePositives()} " +
                      $"FP={FalsePositiveAudit.FalsePositives()}");
        sb.AppendLine($"FN={FalsePositiveAudit.FalseNegatives()} " +
                      $"TN={FalsePositiveAudit.TrueNegatives()}");
        sb.AppendLine($"false positive rate: {FalsePositiveAudit.FalsePositiveRate():P1}");
        sb.AppendLine($"false negative rate: {FalsePositiveAudit.FalseNegativeRate():P1}");
        sb.AppendLine($"precision: {FalsePositiveAudit.Precision():P1} " +
                      $"recall: {FalsePositiveAudit.Recall():P1}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(1000, FalsePositiveAudit.Generate().Length);
        Assert.Equal(500, FalsePositiveAudit.Generate().Count(s => s.Group == "A-lock-fake"));
        Assert.Equal(500, FalsePositiveAudit.Generate().Count(s => s.Group == "B-org-miss"));
    }

    [Fact]
    public void ATQG3191_BothFailureModesSucceed()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3191: both failure modes are real");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the lock-fake systems [tiny rational-ratio spectra] produce locks WITHOUT");
        sb.AppendLine("    organization [false positives];");
        sb.AppendLine("  - the org-miss systems [large-numerator power laws, finance-like] are organized");
        sb.AppendLine("    but carry no locks [false negatives].");
        sb.AppendLine();

        var fake = FalsePositiveAudit.Generate().Where(s => s.Group == "A-lock-fake").ToArray();
        var miss = FalsePositiveAudit.Generate().Where(s => s.Group == "B-org-miss").ToArray();
        sb.AppendLine($"A-lock-fake: {fake.Count(s => s.LockPresent)}/{fake.Length} lock present, " +
                      $"{fake.Count(s => s.OrgPresent)}/{fake.Length} org present");
        sb.AppendLine($"B-org-miss: {miss.Count(s => s.OrgPresent)}/{miss.Length} org present, " +
                      $"{miss.Count(s => s.LockPresent)}/{miss.Length} lock present");
        sb.AppendLine();
        sb.AppendLine($"false positive rate {FalsePositiveAudit.FalsePositiveRate():P1} — most truly");
        sb.AppendLine($"unorganized systems are falsely flagged as organized by the lock rule.");
        sb.AppendLine($"false negative rate {FalsePositiveAudit.FalseNegativeRate():P1} — most truly");
        sb.AppendLine($"organized systems are missed by the lock rule.");

        Output.WriteLine(sb.ToString());

        Assert.True(fake.Count(s => s.LockPresent && !s.OrgPresent) >= fake.Length * 0.8,
            "most lock-fake attempts must succeed [locks without organization]");
        int organizedInMiss = miss.Count(s => s.OrgPresent);
        Assert.True(organizedInMiss > 0 && miss.Count(s => !s.LockPresent && s.OrgPresent) >= organizedInMiss * 0.5,
            "at least half of the organized systems in the org-miss group must be missed by the lock rule");
    }

    [Fact]
    public void ATQG3192_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3192: the robustness determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - WEAK: the QG317 lock rule is frequently wrong under adversarial synthetic");
        sb.AppendLine("    systems — locks can be faked and real organizations can be missed.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {FalsePositiveAudit.Summary()}");
        sb.AppendLine($"Robustness score: {FalsePositiveAudit.RobustnessScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {FalsePositiveAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the lock rule [coherence ≥ 0.10 → organized] is WEAK as a standalone detector:");
        sb.AppendLine($"    false positive rate {FalsePositiveAudit.FalsePositiveRate():P1} — tiny engineered");
        sb.AppendLine("    rational-ratio spectra trivially fake locks without any organization;");
        sb.AppendLine($"    false negative rate {FalsePositiveAudit.FalseNegativeRate():P1} — finance-like");
        sb.AppendLine("    large-numerator power laws are organized but carry no small-fraction locks;");
        sb.AppendLine("  - QG317's 8/8 held for the SPECIFIC evolving power-law cohort, not as a universal");
        sb.AppendLine("    detector — the lock identity is fake-able and miss-able in general.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("WEAK", FalsePositiveAudit.Classify());
        Assert.True(FalsePositiveAudit.FalsePositiveRate() >= 0.60 || FalsePositiveAudit.FalseNegativeRate() >= 0.60);
        Assert.Contains("WEAK", FalsePositiveAudit.Summary());
    }
}
