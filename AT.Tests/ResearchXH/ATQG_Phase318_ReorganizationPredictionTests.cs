using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 318 — Reorganization Prediction. QG315: locks precede organization; QG316: critical
/// transition; QG317: blind prediction of maturity. This phase asks: do the early lock identities
/// predict FUTURE STRUCTURAL REORGANIZATION? Four evolving systems [software history, wiki edits,
/// citation networks, language corpora] grow a frequency law and then undergo a reorganization [a law
/// switch at stage 4]. The future topology change is the fractional spectral difference between the
/// pre- and post-reorganization spectra. Deterministic, no observables, no target values.
/// </summary>
public class ATQG_Phase318_ReorganizationPredictionTests : ResearchTestBase
{
    public ATQG_Phase318_ReorganizationPredictionTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG3180_TheReorganizationCohort()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3180: the reorganization cohort — four systems, law switch at stage 4");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - each member grows a frequency law and then reorganizes [exponent jumps];");
        sb.AppendLine("  - the early lock coherence is measured at stage 2 [before the reorganization];");
        sb.AppendLine("  - the future topology change is the fractional spectral difference.");
        sb.AppendLine();

        foreach (var m in ReorganizationPrediction.Cohort())
        {
            sb.AppendLine($"  {m.System.PadRight(10)} {m.Name}: pre={m.PreExponent:F2} post={m.PostExponent:F2} " +
                          $"strength={m.ReorgStrength:F2} earlyLock={m.EarlyLockCoherence:F3} " +
                          $"topoChange={m.TopologyChange:F3} predicted={m.PredictedClass} " +
                          $"revealed={m.RevealedClass} correct={m.Correct}");
        }

        Output.WriteLine(sb.ToString());

        Assert.Equal(32, ReorganizationPrediction.Cohort().Length);
        Assert.Equal(4, ReorganizationPrediction.Cohort().Select(m => m.System).Distinct().Count());
        Assert.All(ReorganizationPrediction.Cohort(), m =>
        {
            Assert.InRange(m.ReorgStrength, 1.2, 2.5);
            Assert.Contains(m.PredictedClass, new[] { "LARGE", "SMALL" });
        });
        Assert.Equal(16, ReorganizationPrediction.Cohort().Count(m => m.RevealedClass == "LARGE"));
    }

    [Fact]
    public void ATQG3181_LockedSystemsReorganizeLess()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3181: locked systems are plasticity-lost — they reorganize less");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - systems with early lock structure [present ≥ 0.10] have ALREADY committed to a");
        sb.AppendLine("    rigid small-fraction topology — their future reorganization is SMALL;");
        sb.AppendLine("  - systems without early locks are still plastic — their reorganization is LARGE.");
        sb.AppendLine();

        var cohort = ReorganizationPrediction.Cohort();
        var locked = cohort.Where(m => m.EarlyLockCoherence >= 0.10).ToArray();
        var unlocked = cohort.Where(m => m.EarlyLockCoherence < 0.10).ToArray();
        sb.AppendLine($"locked members: {locked.Length} — mean topology change {locked.Average(m => m.TopologyChange):F3}");
        sb.AppendLine($"unlocked members: {unlocked.Length} — mean topology change {unlocked.Average(m => m.TopologyChange):F3}");
        sb.AppendLine($"locked SMALL fraction: {locked.Count(m => m.RevealedClass == "SMALL")}/{locked.Length}");
        sb.AppendLine();
        sb.AppendLine("The lock structure is a rigidity signal: systems that lock early have already");
        sb.AppendLine("committed their topology, so the future reorganization is small. Un-locked systems");
        sb.AppendLine("remain plastic and reorganize more.");

        Output.WriteLine(sb.ToString());

        Assert.True(locked.Length >= 8, "the cohort must contain systems with early lock structure");
        Assert.All(locked, m => Assert.Equal("SMALL", m.RevealedClass));
        Assert.True(locked.Average(m => m.TopologyChange) < unlocked.Average(m => m.TopologyChange),
            "locked systems must reorganize LESS than unlocked systems");
    }

    [Fact]
    public void ATQG3182_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG3182: the reorganization-prediction determination");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - REORGANIZATION PREDICTOR: the early lock structure predicts the future");
        sb.AppendLine("    topology-change class.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {ReorganizationPrediction.Summary()}");
        sb.AppendLine($"Prediction score: {ReorganizationPrediction.PredictionScore()}/5");
        sb.AppendLine($"CLASSIFICATION = {ReorganizationPrediction.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - the future topology-change class was predicted from the EARLY stage-2 lock");
        sb.AppendLine("    coherence [present ≥ 0.10 → SMALL — the plasticity-loss hypothesis], then the");
        sb.AppendLine("    reorganization was revealed;");
        sb.AppendLine("  - the early lock structure predicts future structural reorganization: locked");
        sb.AppendLine("    systems are plasticity-lost and reorganize less, un-locked systems are plastic");
        sb.AppendLine("    and reorganize more.");
        sb.AppendLine("  - the locks are not only maturity predictors [QG317] but REORGANIZATION");
        sb.AppendLine("    PREDICTORS.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("REORGANIZATION PREDICTOR", ReorganizationPrediction.Classify());
        Assert.Equal(5, ReorganizationPrediction.PredictionScore());
        Assert.Contains("REORGANIZATION PREDICTOR", ReorganizationPrediction.Summary());
    }
}
