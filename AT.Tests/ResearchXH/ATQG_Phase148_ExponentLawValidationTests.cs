using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 148 — Independent validation of the exponent law. QG147 constructed p = 6.760 − 1.473·Q +
/// 4.706·T3 from lepton/up/down. This phase tests whether the law predicts fermion sectors NOT used to
/// construct it (the neutrino sector) and checks for overfitting.
///
/// Tests: ATQG1480 (neutrino sector prediction), ATQG1481 (leave-one-out validation), ATQG1482
/// (overfitting check + classification).
/// </summary>
public class ATQG_Phase148_ExponentLawValidationTests : ResearchTestBase
{
    public ATQG_Phase148_ExponentLawValidationTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1480_NeutrinoSectorPrediction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1480: neutrino sector (unseen) prediction");

        var law = ExponentLawValidation.Law();
        var nu = ExponentLawValidation.NeutrinoPrediction();

        sb.AppendLine($"LAW (fitted on leptons, up, down):");
        sb.AppendLine($"  p = {law.P0:F3} + {law.A:F3}·Q + {law.B:F3}·T3");
        sb.AppendLine();
        sb.AppendLine("NEUTRINO SECTOR (Q=0, T3=+1/2) — the only fully UNSEEN fermion sector:");
        sb.AppendLine($"  predicted exponent = {nu.Predicted:F3}");
        sb.AppendLine($"  observed exponent (ν3/ν1 = 500) = {nu.Observed:F3}");
        sb.AppendLine($"  relative deviation = {nu.Deviation:P2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the neutrino prediction deviates substantially — the law does NOT");
        sb.AppendLine("generalize to the unseen sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(nu.Predicted > 0, "neutrino prediction should be well-defined");
        Assert.True(nu.Deviation > 0.5, "neutrino deviation should be large (out-of-sample failure)");
    }

    [Fact]
    public void ATQG1481_LeaveOneOutValidation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1481: leave-one-out validation");

        sb.AppendLine("LEAVE-ONE-OUT (2-parameter reduced model p = p0 + k·T3):");
        foreach (var (h, d) in ExponentLawValidation.LeaveOneOut("T3"))
            sb.AppendLine($"  held-out {h}: deviation = {d:P2}");
        double meanT3 = ExponentLawValidation.MeanLooDeviation("T3");
        sb.AppendLine($"  mean = {meanT3:P2}");
        sb.AppendLine();
        sb.AppendLine("LEAVE-ONE-OUT (p = p0 + k·Q):");
        foreach (var (h, d) in ExponentLawValidation.LeaveOneOut("Q"))
            sb.AppendLine($"  held-out {h}: deviation = {d:P2}");
        double meanQ = ExponentLawValidation.MeanLooDeviation("Q");
        sb.AppendLine($"  mean = {meanQ:P2}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the reduced T3 model generalizes partially (~21% mean) but the Q-only");
        sb.AppendLine("model performs worse (~50% mean) — the law's generalization is limited.");
        Output.WriteLine(sb.ToString());

        Assert.True(meanT3 > 0.15, "T3-only LOO should show non-trivial deviation");
        Assert.True(meanQ > meanT3, "Q-only LOO should be worse than T3-only");
    }

    [Fact]
    public void ATQG1482_OverfittingCheckAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1482: overfitting check and classification");

        bool saturated = ExponentLawValidation.SaturatedFit();
        double overall = ExponentLawValidation.OverallDeviation();
        int score = ExponentLawValidation.ValidationScore();
        string cls = ExponentLawValidation.Classify();

        sb.AppendLine($"OVERFITTING CHECK:");
        sb.AppendLine($"  3-parameter law is a saturated fit (3 params, 3 points): {saturated}");
        sb.AppendLine($"  overall deviation (neutrino + best LOO) = {overall:P2}");
        sb.AppendLine();
        sb.AppendLine($"validation score (0..5): {score}");
        sb.AppendLine($"  +1 reproduces training: {SectorExponentLaw.LawReproducesSectors()}");
        sb.AppendLine($"  +1 neutrino within 50%: {ExponentLawValidation.NeutrinoPrediction().Deviation < 0.50}");
        sb.AppendLine($"  +1 T3 LOO < 35%: {ExponentLawValidation.MeanLooDeviation("T3") < 0.35}");
        sb.AppendLine($"  +1 Q LOO < 35%: {ExponentLawValidation.MeanLooDeviation("Q") < 0.35}");
        sb.AppendLine($"  +1 overall < 25%: {overall < 0.25}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • PARTIAL VALIDATION rejected: the unseen neutrino prediction fails badly (103%).");
        sb.AppendLine("  • OVERFIT accepted: the 3-parameter law reproduces its training sectors exactly");
        sb.AppendLine("    (saturated interpolation) but does NOT predict the unseen neutrino sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(saturated, "the law should be a saturated 3-param/3-point fit");
        Assert.True(overall > 0.25, "overall out-of-sample deviation should be large");
        Assert.Equal("OVERFIT", cls);
    }
}
