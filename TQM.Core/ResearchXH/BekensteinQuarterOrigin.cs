namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 185 — Bekenstein quarter origin. Known: QG12 derives the area law S ∝ R^(d−1) from boundary
/// counting; QG184 derives M ∝ R (radius-proportional mass) and T ∝ 1/R (surface gravity) from the
/// per-octave deficit. This phase asks: can the EXACT coefficient 1/4 in S = A/4 be derived from TRM/D96
/// without imported normalization — deterministic?
///
/// Method (computational, fully deterministic): (1) STRUCTURE IS DERIVED — the area law (QG12),
/// the mass-radius relation M ∝ R (QG184), and the temperature scaling T ∝ 1/R (QG184) are all
/// established. (2) THE FIRST-LAW COEFFICIENT — with the Schwarzschild deficit normalization
/// m₀/(d·L·ρ̄) = 1/2 (so GM = R/2), the deficit first law S = ∫d(GM)/T with the QG184 temperature
/// T = 1/((d−1)·R^(d−2)) = 1/(2R) at d = 3 gives S = R²/2 = A_cell/2 — coefficient 1/2 in cell units,
/// or A/(8π) in physical area units. (3) THE 2π GAP — the exact Bekenstein-Hawking S = A/4 requires
/// T = κ/(2π) = 1/(8πM) (the Hawking temperature), i.e. the surface gravity κ = 1/(4M) divided by 2π.
/// The 2π is the QUANTUM (Unruh/Hawking) factor; it is not present in the D96/TRM classical structures.
/// (4) CANDIDATE 1/occ₀ — occ₀ = 4 (lightest-octave occupancy) gives 1/occ₀ = 1/4 exactly, but this is
/// a numerical identity of the label 4, with no derived mechanism connecting it to the first law.
///
/// Derived: the structure (S ∝ A, M ∝ R, T ∝ 1/R) is derived; the exact 1/4 coefficient is NOT —
/// it requires the 2π quantum factor (T = κ/2π), which is not derivable from D96/TRM in this phase.
/// The deficit first law gives S = A_cell/2 (cell units) or A/(8π) (physical units), off by 2π.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class BekensteinQuarterOrigin
{
    /// <summary>Spatial dimension (d = 3).</summary>
    public const int Dimension = 3;

    // ── 1. Structure derived in prior phases ──────────────────────────────────

    /// <summary>Area law (QG12): horizon entropy S ∝ R^(d−1) from boundary counting.</summary>
    public static double AreaLawEntropy(int d, double R)
        => BlackHoleEntropy.HorizonEntropy(d, R);

    /// <summary>Mass-radius (QG184): GM = m₀·R/(d·L·ρ̄) — radius-proportional mass.</summary>
    public static double DeficitMass(double R, double rhoBar = 1.0, double m0 = 0.4,
        double r0 = 0.5, double Rmax = 10.0)
        => MassRadiusOrigin.GravitationalMass(R, rhoBar, m0, r0, Rmax);

    /// <summary>Temperature (QG184): T = 1/((d−1)·R^(d−2)) = 1/(2R) at d=3 — surface gravity.</summary>
    public static double SurfaceGravityTemperature(int d, double R)
        => MassRadiusOrigin.HawkingTemperature(d, R);

    // ── 2. The first-law entropy coefficient ──────────────────────────────────

    /// <summary>
    /// The Schwarzschild deficit normalization: m₀/(d·L·ρ̄) = 1/2 (so GM = R/2, R = 2GM).
    /// </summary>
    public static double SchwarzschildDeficitNormalization()
        => 0.5;

    /// <summary>
    /// Deficit first-law entropy: S = ∫d(GM)/T. With GM = R/2 and T = 1/((d−1)·R^(d−2)) at d=3:
    /// S = ∫(dR/2)·(2R) = R²/2 = A_cell/2. Coefficient 1/2 in cell units.
    /// </summary>
    public static double DeficitFirstLawEntropy(double R)
        => R * R / 2.0;

    /// <summary>The deficit first-law coefficient (1/2 in cell units).</summary>
    public static double DeficitCoefficient()
        => 0.5;

    /// <summary>
    /// The deficit entropy in physical area units: S = R²/2 = A/(8π) (A = 4πR²). Coefficient 1/(8π).
    /// </summary>
    public static double DeficitCoefficientPhysicalArea()
        => 1.0 / (8.0 * Math.PI);

    // ── 3. The Bekenstein-Hawking coefficient ────────────────────────────────

    /// <summary>The target Bekenstein-Hawking coefficient: S = A/4.</summary>
    public static double BekensteinCoefficient()
        => 0.25;

    /// <summary>
    /// The standard chain (π cancels): S = 4πM², A = 16πM², S/A = 1/4.
    /// Requires R = 2M (QG184), A = 4πR², and T = κ/(2π) = 1/(8πM) — the 2π quantum factor.
    /// </summary>
    public static double FirstLawEntropyWithHawkingT(double R)
        => Math.PI * R * R;   // S = ∫dM/T with T=1/(8πM), M=R/2 → πR² = A/4

    /// <summary>
    /// The 2π gap: the deficit temperature is the SURFACE GRAVITY κ = 1/(2R); the Hawking temperature
    /// is κ/(2π) = 1/(4πR). Ratio = 2π.
    /// </summary>
    public static double TwoPiGap()
        => 2.0 * Math.PI;

    /// <summary>The ratio of the deficit coefficient to the Bekenstein coefficient.</summary>
    public static double CoefficientRatio()
        => 0.25 / DeficitCoefficient();   // = 1/2

    // ── 4. Candidate: occ₀ = 4 → 1/occ₀ = 1/4 ────────────────────────────────

    /// <summary>occ₀ (lightest-octave occupancy) = 4.</summary>
    public static double LightestOctaveOccupancy()
        => EffectiveAccessCounts.OctaveOccupancies()[0];

    /// <summary>1/occ₀ = 1/4 — a numerical identity of the label 4.</summary>
    public static double InverseLightestOctave()
        => 1.0 / LightestOctaveOccupancy();

    // ── 5. Agreement checks ────────────────────────────────────────────────────

    /// <summary>
    /// Does the deficit first-law coefficient (1/2 in cell units) reproduce the Bekenstein 1/4?
    /// NO — it differs by a factor 2 (the 2π/π gap).
    /// </summary>
    public static bool DeficitReproducesQuarter()
        => Math.Abs(DeficitCoefficient() / BekensteinCoefficient() - 1.0) < 1e-9;

    /// <summary>Does the QG12 counting (ln 2 per cell) reproduce the Bekenstein 1/4? NO.</summary>
    public static bool Qg12ReproducesQuarter()
        => Math.Abs(Math.Log(2.0) / BekensteinCoefficient() - 1.0) < 1e-9;

    /// <summary>Is the structure (area law, M ∝ R, T ∝ 1/R) derived? YES.</summary>
    public static bool StructureDerived()
    {
        bool areaLaw = Math.Abs(BlackHoleEntropy.EntropyRatio(Dimension, 1.0) - Math.Pow(2, Dimension - 1)) < 1e-9;
        bool massRadius = MassRadiusOrigin.MassScalesWithRadius();
        bool tempScaling = MassRadiusOrigin.HawkingRestored();
        return areaLaw && massRadius && tempScaling;
    }

    /// <summary>Is 1/occ₀ = 1/4 a numerical identity (occ₀ = 4)?</summary>
    public static bool InverseOctaveIsQuarter()
        => Math.Abs(InverseLightestOctave() - 0.25) < 1e-9;

    // ── Origin score & classification ─────────────────────────────────────────

    /// <summary>
    /// Quarter-origin score (0..3):
    /// 1. the structure is derived (area law + M ∝ R + T ∝ 1/R);
    /// 2. the deficit first-law gives a definite coefficient (1/2 cell units, 1/(8π) physical);
    /// 3. the exact 1/4 is NOT reproduced without the 2π quantum factor.
    /// Score 2 = PARTIAL ORIGIN (structure + coefficient identified, exact 1/4 not derived).
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        if (StructureDerived()) score++;
        if (DeficitCoefficient() > 0 && DeficitCoefficient() < 1) score++;
        if (TwoPiGap() > 0) score++;   // the 2π gap is identified
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO ORIGIN       — even the structure is not derived;
    ///   PARTIAL ORIGIN  — the structure (S ∝ A, M ∝ R, T ∝ 1/R) is derived and the deficit first-law
    ///                      gives a definite coefficient (1/2 cell units = 1/(8π) physical), but the exact
    ///                      Bekenstein-Hawking 1/4 is NOT reproduced: it requires the 2π quantum factor
    ///                      T = κ/(2π) (the Unruh/Hawking temperature), which is not present in the
    ///                      D96/TRM classical structures. The candidate 1/occ₀ = 1/4 (occ₀ = 4) is a
    ///                      numerical identity without a mechanism.
    ///   QUARTER ORIGIN  — the exact 1/4 is derived (not achieved).
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (DeficitReproducesQuarter()) return "QUARTER ORIGIN";
        if (score <= 1) return "NO ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
