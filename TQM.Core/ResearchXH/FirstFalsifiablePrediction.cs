namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 132 — First falsifiable collider prediction. QG131 established that existing collider data
/// are CONSISTENT with the sector ladder (95 GeV excess, Z/H/t masses, pair thresholds on rungs). This phase
/// asks: does the sector hierarchy predict a SPECIFIC yet-UNOBSERVED energy region or decay signature?
///
/// Method (computational, fully deterministic): within the Z-anchor ladder (12 rungs, 91.19–263.43 GeV):
/// (1) MISSING LADDER RUNGS — rungs NOT within 5% of an observed SM state (Z, H, t) are predicted but
/// unobserved resonances; (2) PREDICTED RESONANCES — the specific unobserved rung masses; (3) DECAY-CASCADE
/// ENDPOINTS — the QG128 emitted-quantum signature (unit quantum 15.2 GeV × 10, top quantum 20.3 GeV × 1)
/// and the cascade endpoint (observable 3-family sector); (4) THRESHOLD REGIONS — the QG127 discrete energy
/// thresholds at which new sectors appear; (5) COLLIDER REACH — every predicted rung is within LHC13/FCC
/// reach (QG130), making the prediction TESTABLE (falsifiable).
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class FirstFalsifiablePrediction
{
    /// <summary>Tolerance for "observed" (a rung within this of an SM mass is considered already seen).</summary>
    public const double ObservedTolerance = 0.05;

    /// <summary>The observed SM masses used to mark rungs as already-seen (GeV).</summary>
    public static readonly (string Name, double MassGeV)[] ObservedMasses =
    {
        ("Z", PhysicalCalibration.MZGeV),
        ("H", PhysicalCalibration.MHGeV),
        ("t", PhysicalCalibration.MTopGeV),
    };

    // ── 1. Missing ladder rungs ─────────────────────────────────────────────────

    /// <summary>
    /// Ladder rungs (GeV) not within the observed tolerance of any observed SM mass — the missing,
    /// yet-unobserved rungs.
    /// </summary>
    public static double[] MissingRungs(double tolerance = ObservedTolerance)
    {
        var rungs = ColliderDataAudit.LadderRungs();
        return rungs.Where(r => ObservedMasses.All(o => Math.Abs(r / o.MassGeV - 1.0) >= tolerance)).ToArray();
    }

    /// <summary>Number of missing (yet-unobserved) ladder rungs.</summary>
    public static int MissingRungCount(double tolerance = ObservedTolerance)
        => MissingRungs(tolerance).Length;

    /// <summary>Are there any missing ladder rungs (a nontrivial prediction exists)?</summary>
    public static bool HasMissingRungs(double tolerance = ObservedTolerance)
        => MissingRungCount(tolerance) >= 1;

    // ── 2. Predicted resonances ─────────────────────────────────────────────────

    /// <summary>The LOWEST missing rung — the most discoverable predicted resonance.</summary>
    public static double PrimaryPredictedResonance(double tolerance = ObservedTolerance)
    {
        var missing = MissingRungs(tolerance);
        return missing.Length == 0 ? double.NaN : missing.Min();
    }

    /// <summary>
    /// All predicted-but-unobserved resonance masses (GeV), ascending. These are the falsifiable prediction.
    /// </summary>
    public static double[] PredictedResonances(double tolerance = ObservedTolerance)
        => MissingRungs(tolerance).OrderBy(r => r).ToArray();

    /// <summary>
    /// The energy window of the primary prediction: the lowest missing rung plus/minus the half-rung spacing
    /// (half the average adjacent-rung gap) — the search window for a discovery.
    /// </summary>
    public static (double CenterGeV, double HalfWidthGeV) PrimarySearchWindow(double tolerance = ObservedTolerance)
    {
        double center = PrimaryPredictedResonance(tolerance);
        var rungs = ColliderDataAudit.LadderRungs().OrderBy(r => r).ToArray();
        double spacing = rungs.Zip(rungs.Skip(1), (a, b) => b - a).Average();
        return (center, spacing / 2.0);
    }

    // ── 3. Decay-cascade endpoints ──────────────────────────────────────────────

    /// <summary>
    /// Decay-cascade signature: the emitted quanta of a decaying high sector (QG128), calibrated (GeV):
    /// (quantumName, radiusDrop, energyGeV, multiplicity).
    /// </summary>
    public static (string Quantum, double RadiusDrop, double EnergyGeV, int Multiplicity)[] CascadeEndpoints()
    {
        var unit = ColliderSectorPredictions.DecaySpectrum("Z")[0];
        var top = ColliderSectorPredictions.DecaySpectrum("Z")[1];
        return new[]
        {
            ("unit", unit.RadiusDrop, unit.EnergyGeV, 10),
            ("top", top.RadiusDrop, top.EnergyGeV, 1),
        };
    }

    /// <summary>
    /// The cascade endpoint: the final state a decaying high sector settles into — the observable sector
    /// (radius 6, family count 3). Returns the observable radius and family count.
    /// </summary>
    public static (double Radius, int Families) CascadeEndpointSector()
    {
        var obs = ParticleSectorMapping.LowEnergySector();
        return (obs.Radius, obs.Families);
    }

    // ── 4. Threshold regions ────────────────────────────────────────────────────

    /// <summary>The discrete sector energy thresholds (dimensionless ceiling units, QG127).</summary>
    public static double[] ThresholdRegions() => ColliderDataAudit.LadderRungs().Length > 0
        ? HighEnergySectorSignatures.EnergyThresholds().Thresholds
        : Array.Empty<double>();

    // ── 5. Collider reach ───────────────────────────────────────────────────────

    /// <summary>
    /// Are ALL predicted (missing) resonances within LHC13 and FCC-hh reach? If yes the prediction is
    /// TESTABLE (falsifiable) at existing and next-generation colliders.
    /// </summary>
    public static (bool Lhc13, bool Fcchh, bool AllBelowLhc) ColliderReach(double tolerance = ObservedTolerance)
    {
        var pred = PredictedResonances(tolerance);
        double lhc = 13.0e3, fcc = 100.0e3;   // GeV c.o.m.
        bool allLhc = pred.Length > 0 && pred.All(p => p < lhc);
        return (allLhc, pred.All(p => p < fcc), allLhc);
    }

    // ── Prediction score & classification ───────────────────────────────────────

    /// <summary>
    /// Falsifiable-prediction score (0..5):
    /// 1. missing ladder rungs exist;
    /// 2. at least 3 predicted resonance masses;
    /// 3. a primary predicted resonance in a clean discovery window (between Z and H);
    /// 4. the decay cascade has a well-defined quantum signature (unit + top quanta);
    /// 5. all predicted resonances are within LHC13/FCC reach (testable).
    /// </summary>
    public static int PredictionScore()
    {
        int score = 0;
        if (HasMissingRungs()) score++;
        if (PredictedResonances().Length >= 3) score++;
        double primary = PrimaryPredictedResonance();
        bool cleanWindow = !double.IsNaN(primary) && primary > PhysicalCalibration.MZGeV
            && primary < PhysicalCalibration.MHGeV;
        if (cleanWindow) score++;
        if (CascadeEndpoints().Length >= 2) score++;
        if (ColliderReach().AllBelowLhc) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO PREDICTION          — the ladder has no unobserved structure (all rungs already seen, no testable
    ///                            feature);
    ///   PARTIAL PREDICTION     — some unobserved rungs exist but the prediction is not specific/testable
    ///                            (no clean primary window, or reach incomplete);
    ///   FALSIFIABLE PREDICTION — the hierarchy predicts SPECIFIC yet-unobserved resonances (including a
    ///                            primary in a clean discovery window) that are TESTABLE at LHC/FCC — the
    ///                            concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = PredictionScore();
        if (score <= 2) return "NO PREDICTION";
        if (score == 5) return "FALSIFIABLE PREDICTION";
        return "PARTIAL PREDICTION";
    }
}
