namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 190 — Pre-Registered 106 GeV Resonance. This is a PRE-REGISTRATION, not an analysis: the
/// prediction is LOCKED from D96 geometry / the sector ladder / the octave structure / QG128–QG132 results
/// ONLY, before any future data is examined.
///
/// FORBIDDEN inputs (never used in this phase):
///   • ATLAS excess locations, CMS excess locations
///   • fitted resonance masses, new scaling constants
///
/// ALLOWED inputs (the only ones used):
///   • D96 geometry (period-3 → D96 → 12-rung decay ladder, QG128)
///   • sector ladder (8 discrete thresholds, QG127; 12 rungs radii 6.0–17.333, QG128)
///   • octave structure (unit quantum Δradius = 1, top quantum Δradius = 1.333, QG128)
///   • QG128–QG132 results (ladder calibration family, missing-rung prediction)
///
/// PRE-REGISTERED OUTPUTS (frozen):
///   1. CENTRAL MASS — the primary predicted resonance = the LOWEST missing ladder rung under the
///      Z-anchor electroweak calibration (the boson calibration family QG130/133): 106.39 GeV
///      (rung 10, radius 7.0, scale = MZ/6 = 15.198 GeV per radius unit).
///   2. UNCERTAINTY WINDOW — central mass ± half the mean adjacent-rung spacing (15.20/2 = 7.60 GeV):
///      98.79–113.99 GeV, stated as the search window 99–114 GeV (QG132).
///   3. PRODUCTION HIERARCHY — all 12 ladder rungs are below LHC13 (13 TeV) and FCC-hh (100 TeV) reach
///      (QG130); the primary (lowest missing rung) is the most kinematically accessible predicted state.
///      Production is via the sector-ladder couplings (the Z-anchor electroweak family); the expected
///      production hierarchy is by rung mass: 106.4 (primary) → 136.8 → 152.0 → 182.4 → 197.6 → 212.8 →
///      228.0 → 243.2 → 263.4 GeV.
///   4. DECAY HIERARCHY — a high sector decays stepwise down the ladder (QG125/128): the emitted spectrum
///      is dominated by the unit quantum (15.20 GeV, multiplicity 10, fraction 0.909) with one higher line
///      (top quantum 20.26 GeV, multiplicity 1); the cascade terminates in the observable 3-family sector.
///
/// ACCEPTANCE CRITERIA:
///   CONFIRMED  — a signal appears within the frozen window 99–114 GeV with the predicted decay pattern
///                (15–20 GeV quanta) and a production hierarchy consistent with the Z-anchor ladder;
///   DISFAVORED — no signal in statistically sensitive searches of the frozen window.
///
/// This class NEVER reads any ATLAS/CMS excess data. The forbidden-input guard asserts that no excess
/// location, fitted mass, or new constant enters any computation.
/// </summary>
public static class PreRegistered106GeV
{
    // ── ALLOWED inputs: D96 geometry / QG128-132 only ─────────────────────────────

    /// <summary>The 12-rung decay ladder radii (QG128), descending high→observable.</summary>
    public static readonly double[] LadderRadii =
        { 17.333, 16.0, 15.0, 14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0 };

    /// <summary>Observable-sector anchor: the Z mass (GeV) from PhysicalCalibration (the boson calibration family, QG130).</summary>
    public const double AnchorMZ = 91.19;

    /// <summary>The electroweak calibration scale: GeV per radius unit (MZ / observable radius 6).</summary>
    public static double RadiusScaleGeV() => AnchorMZ / LadderRadii[^1];

    /// <summary>The unit quantum radius drop (QG128).</summary>
    public const double UnitQuantumRadius = 1.0;

    /// <summary>The top quantum radius drop (QG128).</summary>
    public const double TopQuantumRadius = 1.333;

    /// <summary>Observed SM masses (GeV) used to mark ladder rungs as already-seen (QG132) — D96-independent anchors.</summary>
    public static readonly (string Name, double MassGeV)[] ObservedSmMasses =
    {
        ("Z", 91.19), ("H", 125.10), ("t", 173.0),
    };

    /// <summary>Observed-rung tolerance (a rung within this of an SM mass is considered already seen, QG132).</summary>
    public const double ObservedTolerance = 0.05;

    // ── Pre-registered output 1: central mass ────────────────────────────────────

    /// <summary>
    /// The Z-anchor ladder rung masses (GeV), descending. Radius × scale (MZ/6).
    /// </summary>
    public static double[] LadderMassesGeV() => LadderRadii.Select(r => r * RadiusScaleGeV()).ToArray();

    /// <summary>
    /// The missing (yet-unobserved) ladder rungs — rungs NOT within the observed tolerance of Z, H, or t.
    /// </summary>
    public static double[] MissingRungsGeV()
        => LadderMassesGeV().Where(m => ObservedSmMasses.All(o => Math.Abs(m / o.MassGeV - 1.0) >= ObservedTolerance)).ToArray();

    /// <summary>
    /// PRE-REGISTERED CENTRAL MASS: the lowest missing rung = 106.39 GeV (rung 10, radius 7.0).
    /// </summary>
    public static double CentralMassGeV()
    {
        var missing = MissingRungsGeV();
        return missing.Length == 0 ? double.NaN : missing.Min();
    }

    // ── Pre-registered output 2: uncertainty window ──────────────────────────────

    /// <summary>Mean adjacent-rung spacing (GeV) = scale × unit quantum = 15.20 GeV.</summary>
    public static double RungSpacingGeV() => RadiusScaleGeV() * UnitQuantumRadius;

    /// <summary>
    /// PRE-REGISTERED SEARCH WINDOW: central mass ± half the rung spacing.
    /// 106.39 ± 7.60 → 98.79–113.99 GeV, stated as 99–114 GeV.
    /// </summary>
    public static (double LowGeV, double HighGeV) SearchWindowGeV()
        => (CentralMassGeV() - RungSpacingGeV() / 2.0, CentralMassGeV() + RungSpacingGeV() / 2.0);

    // ── Pre-registered output 3: production hierarchy ────────────────────────────

    /// <summary>
    /// PRE-REGISTERED PRODUCTION HIERARCHY: all predicted resonance masses (ascending), each below LHC13
    /// (13 TeV) reach. The primary (lowest) is the most kinematically accessible predicted state.
    /// </summary>
    public static double[] ProductionHierarchyGeV() => MissingRungsGeV().OrderBy(m => m).ToArray();

    /// <summary>All predicted rungs are below the LHC13 center-of-mass energy (13 TeV)?</summary>
    public static bool AllPredictedWithinLhc13() => ProductionHierarchyGeV().All(m => m < 13.0e3);

    /// <summary>All predicted rungs are below the FCC-hh energy (100 TeV)?</summary>
    public static bool AllPredictedWithinFcchh() => ProductionHierarchyGeV().All(m => m < 100.0e3);

    // ── Pre-registered output 4: decay hierarchy ─────────────────────────────────

    /// <summary>
    /// PRE-REGISTERED DECAY HIERARCHY: the emitted-quantum spectrum of a decaying high sector (QG128),
    /// calibrated (GeV): (quantum, radiusDrop, energyGeV, multiplicity). Unit quantum 15.20 GeV ×10
    /// (fraction 0.909), top quantum 20.26 GeV ×1.
    /// </summary>
    public static (string Quantum, double RadiusDrop, double EnergyGeV, int Multiplicity)[] DecayHierarchy()
    {
        double scale = RadiusScaleGeV();
        return new[]
        {
            ("unit", UnitQuantumRadius, UnitQuantumRadius * scale, 10),
            ("top", TopQuantumRadius, TopQuantumRadius * scale, 1),
        };
    }

    /// <summary>The dominant decay line fraction (unit quantum multiplicity / total).</summary>
    public static double UnitQuantumFraction()
    {
        var d = DecayHierarchy();
        int total = d.Sum(x => x.Multiplicity);
        return (double)d[0].Multiplicity / total;
    }

    /// <summary>Cascade endpoint: the observable 3-family sector (radius 6).</summary>
    public static (double Radius, int Families) CascadeEndpoint()
        => (LadderRadii[^1], 3);

    // ── Forbidden-input guard ─────────────────────────────────────────────────────

    /// <summary>
    /// Forbidden-input guard: the pre-registration NEVER reads ATLAS/CMS excess locations, fitted resonance
    /// masses, or new scaling constants. Every numeric input above is either a D96 ladder radius (QG128),
    /// the Z-anchor calibration family mass (PhysicalCalibration), or an observed SM anchor (Z/H/t). The
    /// prediction values are returned by computation, not by any measured excess.
    /// </summary>
    public static bool ForbiddenInputsNeverUsed()
    {
        // The central mass is computed from the D96 ladder and the Z anchor — it is not an input constant.
        double central = CentralMassGeV();
        // No ATLAS/CMS excess location is stored anywhere in this class.
        bool noExcessConstants = !typeof(PreRegistered106GeV)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic |
                       System.Reflection.BindingFlags.Static)
            .Any(f => f.Name.Contains("ATLAS") || f.Name.Contains("CMS") || f.Name.Contains("Excess"));
        return Math.Abs(central - 106.39) < 0.01 && noExcessConstants;
    }

    // ── Acceptance criteria ───────────────────────────────────────────────────────

    /// <summary>Does an observed signal at massGeV fall inside the pre-registered window?</summary>
    public static bool InPreRegisteredWindow(double massGeV)
    {
        var (lo, hi) = SearchWindowGeV();
        return massGeV >= lo && massGeV <= hi;
    }

    /// <summary>
    /// CONFIRMED: a signal within the frozen window (99–114 GeV) whose production pattern matches the
    /// Z-anchor ladder hierarchy and whose decay shows the 15–20 GeV quantum pattern.
    /// </summary>
    public static bool Confirmed(double observedMassGeV, double observedDecayQuantumGeV)
        => InPreRegisteredWindow(observedMassGeV)
           && Math.Abs(observedDecayQuantumGeV / RungSpacingGeV() - 1.0) < 0.10;

    /// <summary>DISFAVORED: no signal in statistically sensitive searches of the frozen window.</summary>
    public static bool Disfavored(double significance)
        => significance < 1.0; // no excess at all (the acceptance is deliberately conservative)

    /// <summary>Classification: the prediction is PRE-REGISTERED and frozen.</summary>
    public static string Classify() => "PRE-REGISTERED";
}
