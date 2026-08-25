using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 314 — Organization Predictor Audit. QG313: the lock LAW is universal, the lock VALUES are
/// domain-specific. Can the lock VALUES predict ORGANIZATION STRENGTH? Compute an organization score from
/// the lock structure ONLY (the four normalized lock identities: moment/span, compression/count,
/// higher-moment, √moment/span) and test whether stronger organizations show stronger lock coherence.
/// Deterministic, no observables, no target values.
/// </summary>
public class TQMQG_Phase314_OrganizationPredictorAuditTests : ResearchTestBase
{
    public TQMQG_Phase314_OrganizationPredictorAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3140_LockCoherenceScores()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3140: the lock-coherence organization score across the eight domains");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the organization score is computed from the lock structure ONLY;");
        sb.AppendLine("  - the score is the mean coherence of the four lock identities onto small-fraction");
        sb.AppendLine("    rationals p/q (q ≤ 5, p ≤ 120);");
        sb.AppendLine("  - organized systems should lock more coherently than unorganized systems.");
        sb.AppendLine();

        foreach (var d in OrganizationPredictorAudit.Domains())
        {
            sb.AppendLine($"  {d.Name.PadRight(12)}: score={d.OrganizationScore:F3} " +
                          $"c[M/S={d.CoherenceMomentSpan:F3} C/C={d.CoherenceCompressionCount:F3} " +
                          $"H-M={d.CoherenceHigherMoment:F3} √M/S={d.CoherenceSqrtMomentSpan:F3}] " +
                          $"locks={d.StableLocks} org={d.IsOrganized}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(8, OrganizationPredictorAudit.Domains().Length);
        Assert.All(OrganizationPredictorAudit.Domains(), d => Assert.InRange(d.OrganizationScore, 0.0, 1.0));
    }

    [Fact]
    public void TQMQG3141_ClassSeparationNotStrengthRanking()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3141: the score separates the CLASS but not the STRENGTH");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the lock-coherence score separates organized from unorganized at the class level;");
        sb.AppendLine("  - the score does NOT rank organization strength within the organized class.");
        sb.AppendLine();

        sb.AppendLine($"mean organized: {OrganizationPredictorAudit.MeanOrganized():F3}");
        sb.AppendLine($"mean unorganized: {OrganizationPredictorAudit.MeanUnorganized():F3}");
        sb.AppendLine($"class separates: {OrganizationPredictorAudit.ClassSeparates()}");
        sb.AppendLine($"organized above both unorganized: {OrganizationPredictorAudit.OrganizedAboveUnorganized()}/6");
        sb.AppendLine($"strictly separates: {OrganizationPredictorAudit.StrictlySeparates()}");
        sb.AppendLine($"class ranking (heavy-tailed ≥ Zipf ≥ unorganized): {OrganizationPredictorAudit.ClassRankingHolds()}");
        sb.AppendLine($"stable locks organized: {OrganizationPredictorAudit.MeanStableLocksOrganized():F1} " +
                      $"vs unorganized: {OrganizationPredictorAudit.MeanStableLocksUnorganized():F1}");
        sb.AppendLine();
        sb.AppendLine("finance (heavy-tailed, QG310's strongest organization) locks at 0.000: its lock");
        sb.AppendLine("identities [M/S=1.618, C/C≈334, H-M≈470] have LARGE numerators that never lock onto");
        sb.AppendLine("a small fraction. The lock VALUES predict the organized/unorganized CLASS, not the");
        sb.AppendLine("organization STRENGTH within the class.");

        Output.WriteLine(sb.ToString());

        Assert.True(OrganizationPredictorAudit.ClassSeparates(),
            "the lock-coherence score must separate the organized class from the unorganized class");
        Assert.True(OrganizationPredictorAudit.MeanStableLocksOrganized() >= 2.0,
            "organized systems must carry substantially more stable locks than unorganized systems");
        Assert.True(OrganizationPredictorAudit.MeanStableLocksUnorganized() <= 0.5,
            "unorganized systems must carry few stable locks");
        Assert.True(OrganizationPredictorAudit.OrganizedAboveUnorganized() >= 4,
            "most organized systems must lock above both unorganized systems");
        Assert.False(OrganizationPredictorAudit.ClassRankingHolds(),
            "the QG310 operator-strength ranking (heavy-tailed ≥ Zipf) must NOT be reproduced by lock coherence");
    }

    [Fact]
    public void TQMQG3142_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3142: the lock-prediction determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PARTIAL PREDICTION: the lock values separate the organized/unorganized CLASS but");
        sb.AppendLine("    do not rank organization STRENGTH.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {OrganizationPredictorAudit.Summary()}");
        sb.AppendLine($"Prediction score: {OrganizationPredictorAudit.PredictionScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {OrganizationPredictorAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the lock-coherence score predicts the organized/unorganized class: organized");
        sb.AppendLine("    systems lock onto small-fraction rationals more coherently [mean 0.484 vs 0.108];");
        sb.AppendLine("  - the score does NOT predict organization strength: heavy-tailed finance [C/C≈334,");
        sb.AppendLine("    H-M≈470 — large numerators, no small-fraction locks] scores 0.000 below the Zipf");
        sb.AppendLine("    systems despite being QG310's strongest organization;");
        sb.AppendLine("  - the lock VALUES (domain fingerprints per QG313) separate CLASS but not STRENGTH.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL PREDICTION", OrganizationPredictorAudit.Classify());
        Assert.InRange(OrganizationPredictorAudit.PredictionScore(), 3, 4);
        Assert.Contains("PARTIAL PREDICTION", OrganizationPredictorAudit.Summary());
    }
}
