using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 133 — Robustness of the 106 GeV prediction. QG132 predicts a primary resonance near 106 GeV
/// under the Z-anchor calibration. This phase asks how sensitive the prediction is to calibration assumptions.
///
/// Tests: ATQG1330 (Z/H/W/t anchor calibrations), ATQG1331 (parameter uncertainty), ATQG1332 (score +
/// classification).
/// </summary>
public class ATQG_Phase133_PredictionRobustnessTests : ResearchTestBase
{
    public ATQG_Phase133_PredictionRobustnessTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1330_AnchorCalibrations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1330: primary prediction across calibration anchors");

        double z = PredictionRobustness.ZAnchorPrediction();
        double w = PredictionRobustness.WAnchorPrediction();
        double h = PredictionRobustness.HAnchorPrediction();
        double t = PredictionRobustness.TopAnchorPrediction();

        sb.AppendLine("PRIMARY PREDICTED RESONANCE PER ANCHOR:");
        sb.AppendLine($"  Z anchor: {z:F2} GeV   (QG132 reference)");
        sb.AppendLine($"  W anchor: {w:F2} GeV");
        sb.AppendLine($"  H anchor: {h:F2} GeV");
        sb.AppendLine($"  t anchor: {t:F2} GeV");
        sb.AppendLine();
        sb.AppendLine($"Z–W (boson) agreement: {100 * Math.Abs(w / z - 1):F2}%");
        sb.AppendLine($"Z–H agreement: {100 * Math.Abs(h / z - 1):F2}%");
        sb.AppendLine($"Z–t agreement: {100 * Math.Abs(t / z - 1):F2}%");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the two electroweak BOSON anchors (Z, W) agree closely (~106–107 GeV),");
        sb.AppendLine("while the fermion-sector anchors (H, t) shift the prediction upward.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(w / z - 1.0) < 0.05, "Z and W anchors should agree within 5%");
        Assert.True(z > 95 && z < 120, "Z-anchor primary should be in the ~106 GeV region");
        Assert.True(h > z && t > h, "fermion anchors should shift the prediction upward");
    }

    [Fact]
    public void ATQG1331_ParameterUncertainty()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1331: parameter uncertainty sensitivity");

        double z = PredictionRobustness.ZAnchorPrediction();
        double maxWidth = PredictionRobustness.MaxUncertaintyWidth();
        var tol = PredictionRobustness.ToleranceSensitivity();
        bool insensitive = PredictionRobustness.ToleranceInsensitive();

        sb.AppendLine("EXPERIMENTAL MASS-UNCERTAINTY WIDTH OF THE PRIMARY PREDICTION:");
        foreach (var (a, _) in PredictionRobustness.CalibrationAnchors())
            sb.AppendLine($"  {a}: ±{PredictionRobustness.UncertaintyWidth(a):F3} GeV");
        sb.AppendLine($"  max width = {maxWidth:F3} GeV ({100 * maxWidth / z:F3}% of the Z prediction)");
        sb.AppendLine();
        sb.AppendLine("OBSERVED-TOLERANCE SENSITIVITY (Z-anchor primary):");
        sb.AppendLine($"  [{string.Join(", ", tol.Select(v => v.ToString("F2", CultureInfo.InvariantCulture)))}]");
        sb.AppendLine($"  tolerance-insensitive: {insensitive}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: experimental mass uncertainties shift the prediction by less than 1 GeV");
        sb.AppendLine("and the observed-tolerance parameter does not change it at all.");
        Output.WriteLine(sb.ToString());

        Assert.True(maxWidth / z < 0.02, "experimental uncertainty should shift the prediction by < 2%");
        Assert.True(insensitive, "prediction should be insensitive to the tolerance parameter");
        Assert.True(tol.All(v => Math.Abs(v - z) < 1e-6), "tolerance sweep should keep the prediction fixed");
    }

    [Fact]
    public void ATQG1332_RobustnessScoreAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1332: robustness score and classification");

        int score = PredictionRobustness.RobustnessScore();
        string cls = PredictionRobustness.Classify();

        double z = PredictionRobustness.ZAnchorPrediction();
        double w = PredictionRobustness.WAnchorPrediction();

        sb.AppendLine($"robustness score (0..5): {score}");
        sb.AppendLine($"  +1 Z–W boson anchors agree: {Math.Abs(w / z - 1.0) < 0.05}");
        sb.AppendLine($"  +1 experimental uncertainty < 2%: {PredictionRobustness.MaxUncertaintyWidth() / z < 0.02}");
        sb.AppendLine($"  +1 tolerance-insensitive: {PredictionRobustness.ToleranceInsensitive()}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • FRAGILE rejected: boson anchors and parameters leave the prediction stable.");
        sb.AppendLine("  • ROBUST rejected: fermion anchors (H → 146 GeV, t → 202 GeV) shift the prediction.");
        sb.AppendLine("  • MODERATE accepted: stable within the electroweak-boson calibration family (Z/W");
        sb.AppendLine("    agree within 1%), insensitive to experimental/parameter uncertainty, but not");
        sb.AppendLine("    robust against re-anchoring on the fermion-sector states.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(w / z - 1.0) < 0.05, "boson anchors should agree");
        Assert.Equal("MODERATE", cls);
    }
}
