using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 317 — Blind Organization Prediction. QG315: the lock identities PRECEDE maturity. This
/// phase runs the decisive temporal test as a BLIND protocol: predict the FUTURE maturity class from the
/// EARLY-STAGE system only, FIX the prediction, and only then REVEAL the later stage. If the early lock
/// structure predicts the future organization class, the lock identities carry genuine predictive (not
/// post-hoc) information. Deterministic, no observables, no target values.
/// </summary>
public class TQMQG_Phase317_BlindOrganizationPredictionTests : ResearchTestBase
{
    public TQMQG_Phase317_BlindOrganizationPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG3170_BlindProtocol()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3170: the blind protocol — predict from stage 2, reveal at stage 8");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the future maturity class is predicted from the EARLY stage only;");
        sb.AppendLine("  - the prediction is fixed BEFORE the later stage is revealed;");
        sb.AppendLine("  - the rule is target-free: early lock coherence ≥ 0.10 → HIGH.");
        sb.AppendLine();

        foreach (var s in BlindOrganizationPrediction.Run())
        {
            sb.AppendLine($"  {s.Name}: earlyLock={s.EarlyLockCoherence:F3} earlyMat={s.EarlyMaturity:F3} " +
                          $"lateMat={s.LateMaturity:F3} predicted={s.PredictedClass} " +
                          $"revealed={s.RevealedClass} correct={s.Correct}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(8, BlindOrganizationPrediction.Run().Length);
        Assert.All(BlindOrganizationPrediction.Run(), s =>
        {
            Assert.Contains(s.PredictedClass, new[] { "HIGH", "not-HIGH" });
            Assert.Contains(s.RevealedClass, new[] { "HIGH", "not-HIGH" });
        });
        Assert.Equal(3, BlindOrganizationPrediction.Run().Count(s => s.PredictedClass == "HIGH"));
        Assert.Equal(3, BlindOrganizationPrediction.Run().Count(s => s.RevealedClass == "HIGH"));
    }

    [Fact]
    public void TQMQG3171_EarlyLocksPredictFutureClass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3171: the early lock structure predicts the future maturity class");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the systems whose early lock coherence is present (≥ 0.10) are exactly the");
        sb.AppendLine("    systems that reach HIGH future maturity (the top third);");
        sb.AppendLine("  - the systems with no early lock structure stay in the not-HIGH class.");
        sb.AppendLine();

        foreach (var s in BlindOrganizationPrediction.Run())
        {
            sb.AppendLine($"  {s.Name}: earlyLock={s.EarlyLockCoherence:F3} → {s.PredictedClass} " +
                          $"(revealed: {s.RevealedClass})");
        }
        sb.AppendLine();
        sb.AppendLine($"accuracy: {BlindOrganizationPrediction.Accuracy():P0} " +
                      $"({BlindOrganizationPrediction.CorrectCount()}/8)");
        sb.AppendLine();
        sb.AppendLine("The early lock identity — the moment ratios locking onto small fractions — is");
        sb.AppendLine("present at 25% growth EXACTLY for the systems that reach the future top-third");
        sb.AppendLine("maturity class. The lock structure is a genuine early predictor of future");
        sb.AppendLine("organization, not a post-hoc description.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(8, BlindOrganizationPrediction.CorrectCount());
        Assert.Equal(1.0, BlindOrganizationPrediction.Accuracy());
        Assert.All(BlindOrganizationPrediction.Run(), s =>
            Assert.Equal(s.PredictedClass == "HIGH", s.EarlyLockCoherence >= 0.10));
    }

    [Fact]
    public void TQMQG3172_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG3172: the blind-prediction determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - PREDICTIVE: the early lock structure predicts the future maturity class.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {BlindOrganizationPrediction.Summary()}");
        sb.AppendLine($"Prediction score: {BlindOrganizationPrediction.PredictionScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {BlindOrganizationPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine($"  - the blind protocol observed ONLY the stage-{BlindOrganizationPrediction.EarlyStage}");
        sb.AppendLine("    (25% growth) spectra and FIXED the prediction before the stage-8 reveal;");
        sb.AppendLine("  - the target-free rule [early lock coherence ≥ 0.10 → HIGH] was not fitted to the");
        sb.AppendLine("    revealed classes — it encodes the QG315 hypothesis as a prediction rule;");
        sb.AppendLine("  - the prediction is 100% correct: the early lock structure identifies the future");
        sb.AppendLine("    HIGH-maturity class exactly.");
        sb.AppendLine("  - the lock identities carry genuine FORWARD-LOOKING predictive information.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PREDICTIVE", BlindOrganizationPrediction.Classify());
        Assert.Equal(5, BlindOrganizationPrediction.PredictionScore());
        Assert.Contains("PREDICTIVE", BlindOrganizationPrediction.Summary());
    }
}
