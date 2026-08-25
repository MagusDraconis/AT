using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 319 (reissue) — Competing Predictor Audit. QG317: lock coherence predicted the future
/// HIGH class. Do the locks OUTPERFORM the standard complexity measures [entropy, gini, power-law
/// exponent, spectral gap] at predicting the future HIGH class? All five predictors use the SAME
/// direction-aware top-third protocol over the 12-system evolving cohort. Deterministic, no observables,
/// no target values.
/// </summary>
public class TQMQG_Phase319_CompetingPredictorAuditTests : ResearchTestBase
{
    public TQMQG_Phase319_CompetingPredictorAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3190_TheFivePredictors()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3190: the five competing predictors over the 12-system cohort");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - entropy, gini, exponent, spectral gap, and lock coherence are each evaluated");
        sb.AppendLine("    from the EARLY stage spectrum;");
        sb.AppendLine("  - all use the same direction-aware top-third protocol;");
        sb.AppendLine("  - the future HIGH class is the top third by stage-8 maturity.");
        sb.AppendLine();

        foreach (var p in CompetingPredictorAudit.Predictors())
        {
            sb.AppendLine($"  {p.Name.PadRight(10)}: correlation={p.Correlation:F3} accuracy={p.Accuracy:P0} " +
                          $"precision={p.Precision:P0} recall={p.Recall:P0}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, CompetingPredictorAudit.Predictors().Length);
        Assert.Contains("lock", CompetingPredictorAudit.Predictors().Select(p => p.Name));
        Assert.All(CompetingPredictorAudit.Predictors(), p =>
        {
            Assert.InRange(p.Accuracy, 0.0, 1.0);
            Assert.InRange(p.Correlation, -1.0, 1.0);
        });
    }

    [Fact]
    public void TQMQG3191_StandardMeasuresMatchOrBeatLocks()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3191: the standard measures predict the future HIGH class at least as well as the locks");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the standard complexity measures [entropy, gini, exponent, gap] predict the");
        sb.AppendLine("    future HIGH class at least as well as the lock coherence.");
        sb.AppendLine();

        double lockAcc = CompetingPredictorAudit.LockAccuracy();
        double bestStd = CompetingPredictorAudit.BestStandardAccuracy();
        sb.AppendLine($"lock accuracy: {lockAcc:P0}");
        sb.AppendLine($"best standard accuracy: {bestStd:P0}");
        foreach (var p in CompetingPredictorAudit.Predictors().Where(p => p.Name != "lock"))
        {
            sb.AppendLine($"  {p.Name}: accuracy {p.Accuracy:P0} >= lock {lockAcc:P0}: {p.Accuracy >= lockAcc}");
        }
        sb.AppendLine();
        sb.AppendLine("On the evolving power-law cohort, the standard complexity measures are at least as");
        sb.AppendLine("good as the lock coherence at predicting the future HIGH class.");

        Output.WriteLine(sb.ToString());

        Assert.True(bestStd >= lockAcc,
            "the best standard measure must predict the future HIGH class at least as well as the locks");
        Assert.True(CompetingPredictorAudit.Predictors().Count(p => p.Name != "lock" && p.Accuracy >= lockAcc) >= 4,
            "at least four standard measures must match or beat the lock accuracy");
    }

    [Fact]
    public void TQMQG3192_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3192: the advantage determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - NO ADVANTAGE: the locks provide no predictive advantage over the standard");
        sb.AppendLine("    complexity measures on this cohort.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {CompetingPredictorAudit.Summary()}");
        sb.AppendLine($"Advantage score: {CompetingPredictorAudit.AdvantageScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {CompetingPredictorAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - entropy, gini, exponent, and spectral gap all reach 100% accuracy on the");
        sb.AppendLine("    evolving power-law cohort — the same cohort where QG317 reported 8/8 for locks;");
        sb.AppendLine("  - the lock coherence reaches only 83% — the standard measures match or beat it;");
        sb.AppendLine("  - QG317's 8/8 was NOT a lock-specific advantage: the standard complexity measures");
        sb.AppendLine("    predict the future HIGH class at least as well on this cohort.");
        sb.AppendLine("  - the lock coherence is a valid predictor, but it offers NO ADVANTAGE over the");
        sb.AppendLine("    standard measures here.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("NO ADVANTAGE", CompetingPredictorAudit.Classify());
        Assert.True(CompetingPredictorAudit.LockAccuracy() < CompetingPredictorAudit.BestStandardAccuracy());
        Assert.Contains("NO ADVANTAGE", CompetingPredictorAudit.Summary());
    }
}
