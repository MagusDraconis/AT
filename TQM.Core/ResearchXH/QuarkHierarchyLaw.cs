namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 146 — Quark mass hierarchy law. QG145 established that the quark amplification arises from
/// spectral structure × charge-isospin interaction (UP-SECTOR ORIGIN). This phase asks: can the FULL up/down
/// quark mass hierarchy be reproduced from ONE spectral-interaction law?
///
/// Method (computational, fully deterministic): the spectral law (QG140/141) fixes the within-sector octave
/// ratios {1, 2^5.88, 4^5.88} = {1, 58.9, 3468}. If ONE law (one spectral exponent set + one charge×isospin
/// interaction) applies to BOTH quark sectors, the within-sector ratios must be the SAME for up and down.
/// We measure: (1) UP-QUARK SECTOR — the up within-sector ratios (u/c/t); (2) DOWN-QUARK SECTOR — the down
/// within-sector ratios (d/s/b); (3) SPECTRAL DENSITY — the octave spectral baseline and the mode-density
/// input; (4) CHARGE×ISOSPIN AMPLIFICATION — the deviation factor per sector and its dependence on the
/// charge×isospin cross term; (5) HIERARCHY RECONSTRUCTION — whether a single law (shared exponents +
/// cross-term interaction) reproduces both quark sectors' hierarchies (consistency of the implied exponent
/// and cross-term coefficient across sectors and generations).
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class QuarkHierarchyLaw
{
    /// <summary>The octave-predicted within-sector ratios {1, 2^p, 4^p} with p = net mass exponent 5.88.</summary>
    public static readonly double[] OctaveRatios = { 1.0, Math.Pow(2.0, 5.88), Math.Pow(4.0, 5.88) };

    /// <summary>Documented quark masses (MeV).</summary>
    public static readonly double[] UpQuarkMasses = { 2.2, 1270.0, 173000.0 };
    public static readonly double[] DownQuarkMasses = { 4.7, 95.0, 4180.0 };

    // ── 1. Up-quark sector ─────────────────────────────────────────────────────

    /// <summary>Up-quark within-sector ratios (c/u, t/u).</summary>
    public static (double R21, double R31) UpSectorRatios()
        => (UpQuarkMasses[1] / UpQuarkMasses[0], UpQuarkMasses[2] / UpQuarkMasses[0]);

    /// <summary>Up-sector deviation factors (r21/r21_octave, r31/r31_octave).</summary>
    public static (double R21Factor, double R31Factor) UpDeviation()
    {
        var (r21, r31) = UpSectorRatios();
        return (r21 / OctaveRatios[1], r31 / OctaveRatios[2]);
    }

    // ── 2. Down-quark sector ───────────────────────────────────────────────────

    /// <summary>Down-quark within-sector ratios (s/d, b/d).</summary>
    public static (double R21, double R31) DownSectorRatios()
        => (DownQuarkMasses[1] / DownQuarkMasses[0], DownQuarkMasses[2] / DownQuarkMasses[0]);

    /// <summary>Down-sector deviation factors (r21/r21_octave, r31/r31_octave).</summary>
    public static (double R21Factor, double R31Factor) DownDeviation()
    {
        var (r21, r31) = DownSectorRatios();
        return (r21 / OctaveRatios[1], r31 / OctaveRatios[2]);
    }

    // ── 3. Spectral density ────────────────────────────────────────────────────

    /// <summary>The spectral-density input (Weyl exponent of the observable sector, QG141).</summary>
    public static double SpectralDensityExponent()
        => HierarchyExponentOrigin.WeylExponent();

    /// <summary>The octave spectral occupancy (top-octave density).</summary>
    public static double SpectralOccupancy()
        => EffectiveSizeLaw.TopOctaveCrowding();

    // ── 4. Charge×isospin amplification ────────────────────────────────────────

    /// <summary>
    /// Effective within-sector exponent of each quark sector: p_eff = log2(r31)/log2(4) = log(r31)/log(4).
    /// A universal law requires the SAME p_eff for up and down; a sector-dependent law has different values.
    /// </summary>
    public static double UpEffectiveExponent()
        => Math.Log(UpSectorRatios().R31) / Math.Log(4.0);

    /// <summary>Down effective within-sector exponent.</summary>
    public static double DownEffectiveExponent()
        => Math.Log(DownSectorRatios().R31) / Math.Log(4.0);

    /// <summary>
    /// Cross-term amplification: the deviation factor per sector should correlate with the charge×isospin
    /// cross term. Returns the Pearson correlation of log2(factor) with Q·(1+T3) across all fermion sectors
    /// (leptons, up, down, neutrino).
    /// </summary>
    public static double CrossTermCorrelation()
    {
        var data = UpSectorEnhancement.SectorData();
        return EffectiveSizeFamilies.Pearson(
            data.Select(s => s.Q * (1.0 + s.T3)).ToArray(),
            data.Select(s => Math.Log2(s.Factor)).ToArray());
    }

    // ── 5. Hierarchy reconstruction ────────────────────────────────────────────

    /// <summary>
    /// Universal-law consistency: ONE law (shared exponents) reproduces both sectors ONLY if the within-sector
    /// ratios are identical. Returns the relative difference |p_up − p_down|/|p_up|.
    /// </summary>
    public static double ExponentSplit()
        => Math.Abs(UpEffectiveExponent() - DownEffectiveExponent()) / Math.Abs(UpEffectiveExponent());

    /// <summary>Do both quark sectors share the same within-sector exponent (universal law)?</summary>
    public static bool UniversalLaw()
        => ExponentSplit() < 0.15;

    /// <summary>
    /// Single-law reproduction: can one set of spectral exponents reproduce the up hierarchy AND the down
    /// hierarchy simultaneously? False if the within-sector ratios differ (universal (p,q) fails).
    /// </summary>
    public static bool SingleLawReproducesBoth()
    {
        var up = UpSectorRatios();
        var down = DownSectorRatios();
        return Math.Abs(up.R21 / down.R21 - 1.0) < 0.15
            && Math.Abs(up.R31 / down.R31 - 1.0) < 0.15;
    }

    // ── Law score & classification ─────────────────────────────────────────────

    /// <summary>
    /// Quark-hierarchy-law score (0..5):
    /// 1. the up sector deviates strongly from the octave law (amplified);
    /// 2. the down sector deviates (suppressed or different direction);
    /// 3. the spectral-density input is well-defined;
    /// 4. the charge×isospin cross term correlates with the deviations;
    /// 5. a single law reproduces BOTH quark hierarchies (same within-sector ratios).
    /// </summary>
    public static int LawScore()
    {
        int score = 0;
        if (UpDeviation().R31Factor > 2.0) score++;
        if (DownDeviation().R31Factor < 0.9 || DownDeviation().R31Factor > 2.0) score++;
        if (!double.IsNaN(SpectralDensityExponent()) && SpectralDensityExponent() > 1.0) score++;
        if (Math.Abs(CrossTermCorrelation()) > 0.4) score++;
        if (SingleLawReproducesBoth()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO LAW               — no spectral-interaction structure reproduces either quark sector;
    ///   PARTIAL LAW          — the up and down sectors each deviate from the octave law in a charge×isospin
    ///                          signed way, but ONE universal law does NOT reproduce both (the within-sector
    ///                          ratios differ) — consistent with QG142's PARTIAL LAW;
    ///   QUARK HIERARCHY ORIGIN — one spectral-interaction law reproduces the full up/down quark hierarchy
    ///                          (both sectors share the same within-sector structure).
    /// </summary>
    public static string Classify()
    {
        int score = LawScore();
        if (score <= 2) return "NO LAW";
        if (score == 5) return "QUARK HIERARCHY ORIGIN";
        return "PARTIAL LAW";
    }
}
