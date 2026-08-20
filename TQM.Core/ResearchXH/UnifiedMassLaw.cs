namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 142 — Unified fermion mass law. QG138 derived the family count from octave quantization and
/// QG141 derived the hierarchy amplification exponents from the spectral (Weyl/mode-density) scaling. This
/// phase asks: can a SINGLE spectral law reproduce ALL fermion generations simultaneously — leptons, up
/// quarks, down quarks, neutrinos?
///
/// Method (computational, fully deterministic): the QG140/141 law is mass_k = A·center_k^p·modes_k^q with
/// the net mass exponent p_net = p + q·δ = log(leptonSpan)/log(octaveSpan) = 5.88, so within-sector ratios
/// are mass_k/mass_0 = (center_k/center_0)^p_net = {1, 2^5.88, 4^5.88} = {1, 59, 3468}. We test whether
/// each fermion sector (leptons e/μ/τ, up u/c/t, down d/s/b, neutrinos in normal ordering) reproduces
/// these octave-predicted ratios: (1) LEPTONS — e/μ/τ vs the octave law; (2) UP QUARKS — u/c/t;
/// (3) DOWN QUARKS — d/s/b; (4) NEUTRINOS — ν1/ν2/ν3; (5) UNIVERSAL SCALING — since a universal law with
/// shared exponents and octave structure fixes the WITHIN-SECTOR ratios, the ratios must be identical across
/// all sectors; we measure the ratio spread and how many sectors match the octave prediction.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class UnifiedMassLaw
{
    /// <summary>Octave-predicted within-sector ratios {1, 2^p, 4^p} with p = net mass exponent 5.88.</summary>
    public static readonly double[] OctavePredictedRatios =
        { 1.0, Math.Pow(2.0, 5.88), Math.Pow(4.0, 5.88) };   // {1, 58.9, 3468}

    /// <summary>Documented fermion masses (MeV) per sector (neutrinos in normal ordering).</summary>
    public static (string Sector, double[] MassesMeV)[] FermionSectors()
        => new[]
        {
            ("leptons", new[] { 0.511, 105.66, 1776.86 }),
            ("up", new[] { 2.2, 1270.0, 173000.0 }),
            ("down", new[] { 4.7, 95.0, 4180.0 }),
            ("neutrino", new[] { 0.0001, 0.0009, 0.05 }),
        };

    // ── 1–4. Sector ratio reproduction ──────────────────────────────────────────

    /// <summary>
    /// Within-sector mass ratios (m2/m1, m3/m1) for a sector, and the relative deviation of the highest
    /// ratio from the octave prediction (r31_pred = 4^p).
    /// </summary>
    public static (string Sector, double R21, double R31, double Deviation)[] SectorRatios()
    {
        var result = new List<(string, double, double, double)>();
        foreach (var (name, m) in FermionSectors())
        {
            double r21 = m[1] / m[0], r31 = m[2] / m[0];
            double dev = Math.Abs(r31 / OctavePredictedRatios[2] - 1.0);
            result.Add((name, r21, r31, dev));
        }
        return result.ToArray();
    }

    /// <summary>How many sectors reproduce the octave-predicted highest ratio within 30%?</summary>
    public static int SectorsMatchingOctave(double tolerance = 0.30)
        => SectorRatios().Count(s => s.Deviation < tolerance);

    /// <summary>Does the LEPTON sector reproduce the octave law (τ/e within 30%)?</summary>
    public static bool LeptonsMatch(double tolerance = 0.30)
        => SectorRatios().First(s => s.Sector == "leptons").Deviation < tolerance;

    // ── 5. Universal scaling ────────────────────────────────────────────────────

    /// <summary>
    /// Universal-scaling consistency: a single law with shared exponents and octave structure fixes the
    /// within-sector RATIOS to be identical across sectors. We measure the spread (max/min) of the highest
    /// ratio r31 across sectors. Small spread ⇒ universal; large spread ⇒ sector-dependent.
    /// </summary>
    public static double R31Spread()
    {
        var ratios = SectorRatios().Select(s => s.R31).ToArray();
        return ratios.Max() / ratios.Min();
    }

    /// <summary>Are the highest ratios universal across sectors (spread &lt; 5×)?</summary>
    public static bool UniversalRatios()
        => R31Spread() < 5.0;

    /// <summary>
    /// Consistency of the log2 ratio pattern: if a universal law held, log2(r21) and log2(r31) would be
    /// the SAME across sectors (the octave structure is shared). We measure the standard deviation of
    /// log2(r31) across sectors.
    /// </summary>
    public static double LogRatioSpread()
    {
        var log31 = SectorRatios().Select(s => Math.Log2(s.R31)).ToArray();
        double mean = log31.Average();
        double var = log31.Average(x => (x - mean) * (x - mean));
        return Math.Sqrt(var);
    }

    // ── Law score & classification ──────────────────────────────────────────────

    /// <summary>
    /// Unified-law score (0..5):
    /// 1. the lepton sector reproduces the octave law (τ/e within 30%);
    /// 2. at least 2 sectors reproduce the octave-predicted ratio within 50%;
    /// 3. the log-ratio spread across sectors is &lt; 2 (a shared pattern);
    /// 4. the highest-ratio spread across sectors is &lt; 20 (moderate universality);
    /// 5. the highest-ratio spread is &lt; 5 (full universality).
    /// </summary>
    public static int LawScore()
    {
        int score = 0;
        if (LeptonsMatch()) score++;
        if (SectorsMatchingOctave(0.50) >= 2) score++;
        if (LogRatioSpread() < 2.0) score++;
        if (R31Spread() < 20.0) score++;
        if (R31Spread() < 5.0) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO LAW        — NO sector reproduces the octave law (no ratio correspondence at all);
    ///   PARTIAL LAW   — at least one sector (the leptons) reproduces the octave-predicted hierarchy, but
    ///                   the sectors do NOT share a universal ratio pattern (each sector has a different
    ///                   effective exponent) — a unified law fails;
    ///   UNIFIED MASS LAW — a single spectral law reproduces all fermion sectors simultaneously (leptons,
    ///                   up, down, neutrinos share the octave ratio pattern).
    /// </summary>
    public static string Classify()
    {
        int score = LawScore();
        if (score == 0) return "NO LAW";
        if (score == 5) return "UNIFIED MASS LAW";
        return "PARTIAL LAW";
    }
}
