namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 133 — Robustness of the 106 GeV prediction. QG132 predicts a primary resonance near 106 GeV
/// (Z-anchor). This phase asks: how SENSITIVE is the prediction to calibration assumptions?
///
/// Method (computational, fully deterministic): recompute the primary predicted resonance (lowest ladder
/// rung not within 5% of an observed SM state) under each of the four electroweak calibration anchors
/// (Z, H, W, t — each anchoring the observable radius-6 sector on that mass). Then measure: (1) Z-ANCHOR
/// CALIBRATION — the reference prediction (QG132); (2) H-ANCHOR CALIBRATION — primary under the Higgs
/// anchor; (3) W-ANCHOR CALIBRATION — primary under the W anchor; (4) TOP-ANCHOR CALIBRATION — primary
/// under the top anchor; (5) PARAMETER UNCERTAINTY — the shift of the primary under the documented
/// experimental mass uncertainties of each anchor, and the sensitivity to the observed-tolerance parameter.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class PredictionRobustness
{
    /// <summary>Observed-tolerance: a rung within this of an observed SM mass counts as observed.</summary>
    public const double ObservedTolerance = 0.05;

    /// <summary>All observed electroweak states used to mark rungs as observed.</summary>
    public static readonly (string Name, double MassGeV)[] ObservedMasses =
    {
        ("Z", PhysicalCalibration.MZGeV),
        ("W", PhysicalCalibration.MWGeV),
        ("H", PhysicalCalibration.MHGeV),
        ("t", PhysicalCalibration.MTopGeV),
    };

    /// <summary>Documented experimental mass uncertainties (GeV) for each anchor.</summary>
    public static readonly (string Name, double UncertaintyGeV)[] AnchorUncertainties =
    {
        ("Z", 0.0021),
        ("W", 0.009),
        ("H", 0.17),
        ("t", 0.40),
    };

    /// <summary>The four electroweak calibration anchors (name, mass GeV).</summary>
    public static (string Name, double MassGeV)[] CalibrationAnchors()
        => new[] { ("Z", PhysicalCalibration.MZGeV), ("W", PhysicalCalibration.MWGeV),
                   ("H", PhysicalCalibration.MHGeV), ("t", PhysicalCalibration.MTopGeV) };

    // ── Primary predicted resonance under a given anchor ────────────────────────

    /// <summary>
    /// Primary predicted resonance (GeV) for an anchor mass: the lowest ladder rung (linear radius→mass
    /// calibration) not within the observed tolerance of any observed SM state.
    /// </summary>
    public static double PrimaryPrediction(double anchorMassGeV, double tolerance = ObservedTolerance)
    {
        double scale = anchorMassGeV / 6.0;   // observable radius 6 → anchor mass
        var rungs = ColliderSectorPredictions.LadderRadii.Select(r => r * scale).OrderBy(r => r).ToArray();
        foreach (double r in rungs)
        {
            if (ObservedMasses.All(o => Math.Abs(r / o.MassGeV - 1.0) >= tolerance))
                return r;
        }
        return double.NaN;
    }

    /// <summary>Primary predicted resonance under a named anchor.</summary>
    public static double PrimaryPrediction(string anchorName, double tolerance = ObservedTolerance)
    {
        var a = CalibrationAnchors().First(x => x.Name == anchorName);
        return PrimaryPrediction(a.MassGeV, tolerance);
    }

    /// <summary>Primary prediction under each of the four anchors.</summary>
    public static (string Anchor, double PrimaryGeV)[] AllAnchorPredictions(double tolerance = ObservedTolerance)
        => CalibrationAnchors().Select(a => (a.Name, PrimaryPrediction(a.Name, tolerance))).ToArray();

    // ── 1–4. Anchor calibrations ────────────────────────────────────────────────

    /// <summary>Z-anchor primary prediction (the QG132 reference).</summary>
    public static double ZAnchorPrediction() => PrimaryPrediction("Z");

    /// <summary>W-anchor primary prediction.</summary>
    public static double WAnchorPrediction() => PrimaryPrediction("W");

    /// <summary>H-anchor primary prediction.</summary>
    public static double HAnchorPrediction() => PrimaryPrediction("H");

    /// <summary>t-anchor primary prediction.</summary>
    public static double TopAnchorPrediction() => PrimaryPrediction("t");

    // ── 5. Parameter uncertainty ────────────────────────────────────────────────

    /// <summary>
    /// Uncertainty width of the primary prediction for an anchor: the shift produced by ± the documented
    /// experimental mass uncertainty of that anchor.
    /// </summary>
    public static double UncertaintyWidth(string anchorName)
    {
        var a = CalibrationAnchors().First(x => x.Name == anchorName);
        var u = AnchorUncertainties.First(x => x.Name == anchorName);
        double lo = PrimaryPrediction(a.MassGeV - u.UncertaintyGeV);
        double hi = PrimaryPrediction(a.MassGeV + u.UncertaintyGeV);
        return Math.Abs(hi - lo);
    }

    /// <summary>Maximum uncertainty width over all anchors (GeV).</summary>
    public static double MaxUncertaintyWidth()
        => CalibrationAnchors().Select(a => UncertaintyWidth(a.Name)).Max();

    /// <summary>
    /// Tolerance sensitivity: the primary prediction under the Z anchor for each observed-tolerance value.
    /// If unchanged across the tolerance sweep, the prediction is parameter-robust.
    /// </summary>
    public static double[] ToleranceSensitivity()
    {
        var tols = new[] { 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.10 };
        return tols.Select(t => PrimaryPrediction("Z", t)).ToArray();
    }

    /// <summary>Is the Z-anchor primary unchanged across the tolerance sweep?</summary>
    public static bool ToleranceInsensitive()
        => ToleranceSensitivity().All(v => Math.Abs(v - ZAnchorPrediction()) < 1e-6);

    // ── Robustness score & classification ───────────────────────────────────────

    /// <summary>
    /// Robustness score (0..5):
    /// 1. the Z and W (boson) anchors agree within 5% (the two most precise EW bosons);
    /// 2. the Z and H anchors agree within 25% (some cross-check);
    /// 3. the uncertainty width from experimental mass errors is &lt; 2% of the Z prediction;
    /// 4. the prediction is insensitive to the observed-tolerance parameter;
    /// 5. the overall spread across all four anchors is &lt; 50% of the smallest prediction.
    /// </summary>
    public static int RobustnessScore()
    {
        double z = ZAnchorPrediction(), w = WAnchorPrediction(), h = HAnchorPrediction(), t = TopAnchorPrediction();
        int score = 0;
        if (Math.Abs(w / z - 1.0) < 0.05) score++;
        if (Math.Abs(h / z - 1.0) < 0.25) score++;
        if (MaxUncertaintyWidth() / z < 0.02) score++;
        if (ToleranceInsensitive()) score++;
        double min = new[] { z, w, h, t }.Min();
        double max = new[] { z, w, h, t }.Max();
        if ((max - min) / min < 0.5) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FRAGILE  — the primary prediction shifts substantially across plausible calibrations (boson anchors
    ///              disagree, or experimental uncertainty changes the value);
    ///   MODERATE — the prediction is stable within a subset of the calibration family (the boson anchors Z
    ///              and W agree closely) and is insensitive to experimental/parameter uncertainty, but it
    ///              shifts if one anchors on the fermion-sector states (H, t);
    ///   ROBUST   — the primary prediction is stable across ALL plausible calibrations (anchors and
    ///              parameters) within a narrow window.
    /// </summary>
    public static string Classify()
    {
        int score = RobustnessScore();
        if (score >= 4) return "ROBUST";
        if (score >= 2) return "MODERATE";
        return "FRAGILE";
    }
}
