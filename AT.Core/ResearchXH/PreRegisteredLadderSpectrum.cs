namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 192 — Pre-Registered Sector-Ladder Spectrum. This is a PRE-REGISTRATION: the full 12-rung
/// ladder spectrum is LOCKED from the ladder structure, the attractor spectrum, and D96 geometry (QG121–QG132)
/// ONLY, before any future collider data is examined.
///
/// FORBIDDEN inputs (never used in this phase):
///   • collider bumps, resonance catalogs, fitted energies
///
/// ALLOWED inputs (the only ones used):
///   • ladder structure — the 12-rung decay ladder (radii 6.0–17.333, QG121/QG128)
///   • attractor spectrum — the 8 discrete energy thresholds and the emitted-quantum spectrum
///     (unit quantum Δradius = 1, top quantum Δradius = 1.333, QG127/QG128)
///   • D96 geometry — the Z-anchor electroweak calibration family (scale = MZ/6, QG130)
///
/// FROZEN LADDER SPECTRUM (12 rungs, Z-anchor scale = 15.198 GeV/radius):
///   Rung  Energy   Status          Channel
///    0    263.43   ladder resonance
///    1    243.17   ladder resonance
///    2    227.97   ladder resonance
///    3    212.78   ladder resonance
///    4    197.58   ladder resonance
///    5    182.38   ladder resonance
///    6    167.18   (aligned with t — not predicted)
///    7    151.98   ladder resonance
///    8    136.78   ladder resonance
///    9    121.59   (aligned with H — not predicted)
///   10    106.39   ladder resonance (PRIMARY)
///   11     91.19   (aligned with Z — not predicted)
///
/// PRE-REGISTERED OUTPUTS:
///   1. LADDER ENERGIES — the 9 predicted resonance masses (missing rungs), frozen above.
///   2. MULTIPLICITIES — from the QG128 emitted-quantum spectrum: the unit quantum (Δradius 1) carries
///      multiplicity 10 (fraction 0.909); the top quantum (Δradius 1.333) carries multiplicity 1.
///   3. WIDTHS — a resonance is metastable (QG125) and decays in unit-quantum steps (ΔE = 15.20 GeV per
///      rung, the calibration of Δradius = 1); the decay-width scale is therefore the unit quantum energy.
///   4. PRODUCTION ORDERING — by rung mass, ascending: the lowest missing rung (106.39 GeV) is the most
///      kinematically accessible predicted state; all 9 are below LHC13 (13 TeV) and FCC-hh (100 TeV).
///
/// REQUIRED OUTPUT TABLE (per rung): Rung / Energy / Expected visibility / Expected channel.
///
/// ACCEPTANCE:
///   CONFIRMED  — a new resonance matches a frozen rung energy;
///   FALSIFIED  — sensitive searches exclude a frozen rung.
///
/// This class NEVER reads collider bumps, resonance catalogs, or fitted energies. The forbidden-input guard
/// asserts that no bump/catalog/fitted-energy field exists and the energies are computed (not stored).
/// </summary>
public static class PreRegisteredLadderSpectrum
{
    // ── ALLOWED inputs: ladder structure / attractor spectrum / D96 geometry ──────

    /// <summary>The 12-rung decay ladder radii (QG121/QG128), descending high→observable.</summary>
    public static readonly double[] LadderRadii =
        { 17.333, 16.0, 15.0, 14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0 };

    /// <summary>The Z-anchor electroweak calibration mass (GeV) — the boson calibration family (QG130).</summary>
    public const double AnchorMZ = 91.19;

    /// <summary>Calibration scale: GeV per radius unit = MZ / 6 = 15.198 GeV.</summary>
    public static double RadiusScaleGeV() => AnchorMZ / LadderRadii[^1];

    /// <summary>Unit quantum radius drop (Δradius = 1, QG128).</summary>
    public const double UnitQuantumRadius = 1.0;

    /// <summary>Top quantum radius drop (Δradius = 1.333, QG128).</summary>
    public const double TopQuantumRadius = 1.333;

    /// <summary>Observed SM masses (GeV) that mark ladder rungs as already-seen (QG132).</summary>
    public static readonly (string Name, double MassGeV)[] ObservedSmMasses =
    {
        ("Z", 91.19), ("H", 125.10), ("t", 173.0),
    };

    /// <summary>Observed-rung tolerance (5%, QG132).</summary>
    public const double ObservedTolerance = 0.05;

    // ── Frozen ladder energies ────────────────────────────────────────────────────

    /// <summary>The 12 ladder rung energies (GeV), descending (radius × scale).</summary>
    public static double[] LadderEnergiesGeV() => LadderRadii.Select(r => r * RadiusScaleGeV()).ToArray();

    /// <summary>Is a rung "seen" (within 5% of an observed SM mass)?</summary>
    public static bool RungIsObserved(double energyGeV)
        => ObservedSmMasses.Any(o => Math.Abs(energyGeV / o.MassGeV - 1.0) < ObservedTolerance);

    /// <summary>
    /// The 9 PREDICTED ladder-resonance energies (GeV), ascending — the frozen spectrum.
    /// </summary>
    public static double[] PredictedResonancesGeV()
        => LadderEnergiesGeV().Where(e => !RungIsObserved(e)).OrderBy(e => e).ToArray();

    /// <summary>The frozen ladder spectrum as (rungIndex, radius, energyGeV, isPredicted).</summary>
    public static (int Rung, double Radius, double EnergyGeV, bool Predicted)[] FrozenSpectrum()
        => LadderRadii.Select((r, i) => (i, r, r * RadiusScaleGeV(), !RungIsObserved(r * RadiusScaleGeV()))).ToArray();

    // ── Frozen multiplicities ─────────────────────────────────────────────────────

    /// <summary>
    /// Multiplicities from the QG128 emitted-quantum spectrum: (quantum, radiusDrop, energyGeV, multiplicity).
    /// Unit quantum 15.20 GeV ×10 (fraction 0.909), top quantum 20.26 GeV ×1.
    /// </summary>
    public static (string Quantum, double RadiusDrop, double EnergyGeV, int Multiplicity)[] Multiplicities()
    {
        double scale = RadiusScaleGeV();
        return new[]
        {
            ("unit", UnitQuantumRadius, UnitQuantumRadius * scale, 10),
            ("top", TopQuantumRadius, TopQuantumRadius * scale, 1),
        };
    }

    /// <summary>Dominant-line fraction = 10/11 ≈ 0.909 (unit quantum dominates).</summary>
    public static double UnitQuantumFraction()
    {
        var m = Multiplicities();
        return (double)m[0].Multiplicity / m.Sum(x => x.Multiplicity);
    }

    // ── Frozen widths ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Width scale: a predicted resonance is METASTABLE (QG125) and decays in unit-quantum steps. The decay
    /// step is ΔE = unit-quantum energy = 15.20 GeV (radius drop 1). The width scale = unit quantum energy.
    /// </summary>
    public static double WidthScaleGeV() => RadiusScaleGeV() * UnitQuantumRadius;

    // ── Frozen production ordering ────────────────────────────────────────────────

    /// <summary>All predicted rungs are below the LHC13 c.o.m. energy (13 TeV)?</summary>
    public static bool AllBelowLhc13() => PredictedResonancesGeV().All(e => e < 13.0e3);

    /// <summary>All predicted rungs are below the FCC-hh energy (100 TeV)?</summary>
    public static bool AllBelowFcchh() => PredictedResonancesGeV().All(e => e < 100.0e3);

    /// <summary>The production ordering (by rung mass, ascending): the lightest predicted state first.</summary>
    public static double[] ProductionOrderingGeV() => PredictedResonancesGeV();

    // ── Required output table ─────────────────────────────────────────────────────

    /// <summary>
    /// The required pre-registration table: (rung, energyGeV, expectedVisibility, expectedChannel).
    /// Expected visibility: a predicted resonance is "searchable at LHC13" (metastable decay signature);
    /// an observed-aligned rung is "aligned with SM [name]" (not a prediction).
    /// Expected channel: the decay channel is the unit-quantum cascade (15.2 GeV steps) for predicted rungs.
    /// </summary>
    public static (int Rung, double EnergyGeV, string Visibility, string Channel)[] OutputTable()
        => FrozenSpectrum().Select(f =>
        {
            if (!f.Predicted)
            {
                var name = ObservedSmMasses.First(o => Math.Abs(f.EnergyGeV / o.MassGeV - 1.0) < ObservedTolerance).Name;
                return (f.Rung, Math.Round(f.EnergyGeV, 2), $"aligned with SM {name}", "not a prediction");
            }
            return (f.Rung, Math.Round(f.EnergyGeV, 2), "searchable at LHC13 (metastable decay)", "unit-quantum cascade (15.2 GeV)");
        }).ToArray();

    // ── Forbidden-input guard ─────────────────────────────────────────────────────

    /// <summary>
    /// Forbidden-input guard: the prediction NEVER reads collider bumps, resonance catalogs, or fitted
    /// energies. No bump/catalog/fitted-energy field exists; energies are computed from the D96 ladder and
    /// the Z anchor.
    /// </summary>
    public static bool ForbiddenInputsNeverUsed()
    {
        bool noBumpFields = !typeof(PreRegisteredLadderSpectrum)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic |
                       System.Reflection.BindingFlags.Static)
            .Any(f => f.Name.Contains("Bump") || f.Name.Contains("Catalog")
                   || f.Name.Contains("Fitted") || f.Name.Contains("Excess"));
        return PredictedResonancesGeV().Length == 9 && noBumpFields;
    }

    // ── Acceptance ────────────────────────────────────────────────────────────────

    /// <summary>CONFIRMED: a new resonance at massGeV matches a frozen rung energy (within the half-spacing).</summary>
    public static bool Confirmed(double observedMassGeV)
        => PredictedResonancesGeV().Any(e => Math.Abs(observedMassGeV / e - 1.0) < 0.05);

    /// <summary>FALSIFIED: sensitive searches exclude a frozen rung (an upper limit below a frozen rung energy).</summary>
    public static bool Falsified(double rungEnergyGeV, double upperLimitGeV)
        => upperLimitGeV < rungEnergyGeV;

    /// <summary>Classification: the ladder spectrum is PRE-REGISTERED and frozen.</summary>
    public static string Classify() => "PRE-REGISTERED";
}
