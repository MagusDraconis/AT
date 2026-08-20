namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 134 — Boson-Fermion calibration split. QG133 established that the electroweak BOSON anchors
/// (Z, W) calibrate the ladder consistently (~106 GeV, 0.74% agreement) while the FERMION anchors (H, t)
/// shift the prediction (145, 202 GeV). This phase asks: WHY does the attractor ladder calibrate
/// consistently to bosons but not to fermions?
///
/// Method (computational, fully deterministic): (1) BOSON SECTOR MAPPING — the ratio of each bosonic state
/// mass to the Z mass (W/Z, H/Z, t/Z) and whether it lies within the ladder radius span; (2) FERMION SECTOR
/// MAPPING — the lepton mass ratios (mu/e, tau/e) and whether they lie within the ladder span; (3) FAMILY-
/// INDEX EFFECTS — the observable sector (radius 6) carries a 3-FAMILY structure (QG126), so fermion
/// generations are distinguished by a family index WITHIN the observable sector, whereas bosons are single
/// family-index states at each rung; (4) GENERATION HIERARCHY GAP — quantify the gap between the ladder
/// radius span (2.889) and the generation spans needed for the lepton ratios (207, 3477); (5) CALIBRATION
/// UNIVERSALITY — boson-anchor agreement (Z vs W) vs fermion-anchor spread (H vs t).
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class BosonFermionSplit
{
    /// <summary>Ladder radius span (max rung / observable rung).</summary>
    public const double LadderRadiusSpan = 17.333 / 6.0;   // 2.889

    // ── 1. Boson sector mapping ────────────────────────────────────────────────

    /// <summary>
    /// Bosonic-state mass ratios to the Z mass. Bosons are single family-index states; their ratios to Z
    /// are O(1)-few.
    /// </summary>
    public static (string Name, double Ratio)[] BosonRatios()
        => new[]
        {
            ("W/Z", PhysicalCalibration.MWGeV / PhysicalCalibration.MZGeV),
            ("H/Z", PhysicalCalibration.MHGeV / PhysicalCalibration.MZGeV),
            ("t/Z", PhysicalCalibration.MTopGeV / PhysicalCalibration.MZGeV),
        };

    /// <summary>Do ALL bosonic mass ratios lie within the ladder radius span?</summary>
    public static bool BosonsWithinLadderSpan()
        => BosonRatios().All(r => r.Ratio <= LadderRadiusSpan + 1e-9);

    /// <summary>Are the boson ratios on the O(1)-few scale (all within ~2× of the Z)?</summary>
    public static bool BosonsSingleIndexScale()
        => BosonRatios().All(r => r.Ratio <= 2.0 + 1e-9);

    // ── 2. Fermion sector mapping ──────────────────────────────────────────────

    /// <summary>
    /// Lepton mass ratios (charged leptons, MeV). Fermions come in 3 generations.
    /// </summary>
    public static (string Name, double Ratio)[] FermionRatios()
        => new[]
        {
            ("mu/e", PhysicalCalibration.MMuon / PhysicalCalibration.MElectron),
            ("tau/e", PhysicalCalibration.MTau / PhysicalCalibration.MElectron),
            ("tau/mu", PhysicalCalibration.MTau / PhysicalCalibration.MMuon),
        };

    /// <summary>Are any lepton (generation) ratios BEYOND the ladder radius span?</summary>
    public static bool FermionsBeyondLadderSpan()
        => FermionRatios().Any(r => r.Ratio > LadderRadiusSpan + 1e-9);

    // ── 3. Family-index effects ────────────────────────────────────────────────

    /// <summary>
    /// Family-index effect: the observable sector (radius 6) carries a 3-FAMILY structure (QG126), so the
    /// fermion generations are distinguished by a family index WITHIN the observable sector — not by
    /// separate ladder rungs. Bosons are single family-index states at each rung. Returns the observable
    /// sector's family count.
    /// </summary>
    public static int ObservableFamilyCount()
        => ParticleSectorMapping.LowEnergySector().Families;

    /// <summary>Is the observable sector a 3-family sector (the fermion generations live inside it)?</summary>
    public static bool ObservableIsThreeFamily()
        => ObservableFamilyCount() == 3;

    /// <summary>
    /// Family-index resolution: the number of distinct family classes carried by the observable sector.
    /// Fermion generations are resolved by this index; bosons are not (single index).
    /// </summary>
    public static int FamilyIndexClasses()
        => FamilyStructureRobustness.FamilyCount(
            HighEnergySectorStability.ObservableSector().Adjacency);

    // ── 4. Generation hierarchy gap ────────────────────────────────────────────

    /// <summary>
    /// Generation gap: the ratio of the largest lepton ratio to the ladder radius span. A value &gt;&gt; 1 means
    /// the ladder CANNOT host the generation hierarchy under a linear calibration.
    /// </summary>
    public static double GenerationGapFactor()
        => FermionRatios().Max(r => r.Ratio) / LadderRadiusSpan;

    /// <summary>Is the generation gap a genuine hierarchy beyond the ladder span (gap &gt; 10)?</summary>
    public static bool GenerationGapLarge()
        => GenerationGapFactor() > 10.0;

    // ── 5. Calibration universality ────────────────────────────────────────────

    /// <summary>Boson-anchor calibration agreement (Z vs W primary prediction, relative deviation).</summary>
    public static double BosonAnchorAgreement()
        => Math.Abs(PredictionRobustness.WAnchorPrediction() / PredictionRobustness.ZAnchorPrediction() - 1.0);

    /// <summary>Fermion-anchor calibration spread (H vs t primary prediction, relative deviation).</summary>
    public static double FermionAnchorSpread()
        => Math.Abs(PredictionRobustness.TopAnchorPrediction() / PredictionRobustness.HAnchorPrediction() - 1.0);

    /// <summary>Is the boson-anchor agreement much tighter than the fermion-anchor spread (agreement &lt; 0.1 × spread)?</summary>
    public static bool BosonsCalibrateUniversally()
        => BosonAnchorAgreement() < 0.1 * FermionAnchorSpread();

    // ── Split score & classification ───────────────────────────────────────────

    /// <summary>
    /// Split score (0..5):
    /// 1. all boson ratios lie within the ladder span (bosons map to rungs);
    /// 2. boson ratios are on the single-index O(1)-few scale;
    /// 3. fermion (generation) ratios are beyond the ladder span;
    /// 4. the observable sector is a 3-family sector (family index carries the generations);
    /// 5. boson anchors calibrate universally while fermion anchors do not.
    /// </summary>
    public static int SplitScore()
    {
        int score = 0;
        if (BosonsWithinLadderSpan()) score++;
        if (BosonsSingleIndexScale()) score++;
        if (FermionsBeyondLadderSpan()) score++;
        if (ObservableIsThreeFamily()) score++;
        if (BosonsCalibrateUniversally()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO SPLIT         — bosons and fermions calibrate the same way (no structural difference);
    ///   PARTIAL SPLIT    — some differences exist (e.g. ratios differ) but no systematic family-index
    ///                      mechanism;
    ///   FUNDAMENTAL SPLIT — bosons and fermions calibrate DIFFERENTLY BY STRUCTURE: bosons are single
    ///                      family-index states on ladder rungs (ratios within the ladder span, anchors
    ///                      agree), while fermions are 3-family states whose generations are resolved by a
    ///                      family index WITHIN the observable sector (ratios beyond the ladder span,
    ///                      anchors spread) — the concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = SplitScore();
        if (score <= 2) return "NO SPLIT";
        if (score == 5) return "FUNDAMENTAL SPLIT";
        return "PARTIAL SPLIT";
    }
}
