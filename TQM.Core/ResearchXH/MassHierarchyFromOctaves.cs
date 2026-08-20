namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 139 — Mass hierarchy from octave structure. QG138 established that the family count follows
/// the octave quantization of the spectrum (family count = octave-band count = floor(log2(span)) + 1, span
/// ∝ N/K). This phase asks: can the FERMION MASS HIERARCHIES emerge from the octave-band structure?
///
/// Method (computational, fully deterministic): the observable sector's intra-sector spectrum splits into
/// octave bands (families). Each band is a candidate GENERATION, and its position (start frequency, center,
/// or mode count) is a candidate MASS analog. We measure: (1) BAND POSITIONS — the octave band start
/// frequencies and centers; (2) SPECTRAL GAPS — the gap between consecutive octave bands; (3) OCTAVE
/// SCALING — the ratio of band centers (ideal factor-2 geometric ladder); (4) MASS-RATIO ANALOGS — the
/// implied generation mass ratios from the octave structure vs the documented lepton mass ratios (mu/e,
/// tau/mu, tau/e); (5) FAMILY HIERARCHY — does the octave structure reproduce the family COUNT (3) and a
/// MONOTONE hierarchy even if not the exact ratios.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class MassHierarchyFromOctaves
{
    /// <summary>Default dynamics parameters (matching QG115–138).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;
    public const int DefaultN = 96;

    /// <summary>Documented lepton masses (MeV).</summary>
    public const double MElectron = PhysicalCalibration.MElectron;
    public const double MMuon = PhysicalCalibration.MMuon;
    public const double MTau = PhysicalCalibration.MTau;

    // ── 1. Band positions ───────────────────────────────────────────────────────

    /// <summary>
    /// Octave band positions of the observable sector: (bandIndex, startFrequency, geometricCenter,
    /// modeCount).
    /// </summary>
    public static (int Band, double Start, double Center, int Modes)[] BandPositions(int n = DefaultN,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K, feedback, damping);
        if (w.Length == 0) return Array.Empty<(int, double, double, int)>();
        double w0 = w[0];
        var result = new List<(int, double, double, int)>();
        for (int b = 0; b < 40; b++)
        {
            double lo = w0 * Math.Pow(2.0, b);
            double hi = w0 * Math.Pow(2.0, b + 1);
            int cnt = w.Count(x => x >= lo - 1e-12 && x < hi);
            if (cnt == 0) break;
            double start = w.First(x => x >= lo - 1e-12);
            double center = Math.Sqrt(lo * hi);
            result.Add((b, start, center, cnt));
        }
        return result.ToArray();
    }

    // ── 2. Spectral gaps ────────────────────────────────────────────────────────

    /// <summary>
    /// Spectral gaps between consecutive octave bands: the ratio of the next band start to the current band
    /// end (band spacing). Values near 1 mean the bands are contiguous; near 2 mean the bands are separated.
    /// </summary>
    public static (int GapIndex, double GapRatio)[] SpectralGaps(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var bands = BandPositions(n, K, feedback, damping);
        var result = new List<(int, double)>();
        for (int i = 0; i < bands.Length - 1; i++)
        {
            double bandEnd = bands[i].Start * 2.0;   // octave band i spans [start, 2*start)
            double gap = bands[i + 1].Start / bandEnd;
            result.Add((i, gap));
        }
        return result.ToArray();
    }

    // ── 3. Octave scaling ───────────────────────────────────────────────────────

    /// <summary>Band-center ratios (center_k / center_0) — the octave geometric ladder.</summary>
    public static double[] OctaveCenterRatios(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var bands = BandPositions(n, K, feedback, damping);
        if (bands.Length == 0) return Array.Empty<double>();
        double c0 = bands[0].Center;
        return bands.Select(b => b.Center / c0).ToArray();
    }

    /// <summary>
    /// Octave scaling is geometric if the center ratios follow powers of 2 (each octave doubles the
    /// frequency): ratios ≈ 1, 2, 4, ...
    /// </summary>
    public static bool GeometricOctaveScaling(int n = DefaultN, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var ratios = OctaveCenterRatios(n, K, feedback, damping);
        if (ratios.Length < 2) return false;
        for (int i = 1; i < ratios.Length; i++)
            if (Math.Abs(ratios[i] / Math.Pow(2.0, i) - 1.0) > 0.25) return false;
        return true;
    }

    // ── 4. Mass-ratio analogs ───────────────────────────────────────────────────

    /// <summary>
    /// Octave-implied generation mass ratios: if mass ∝ band center, the octave structure implies ratios
    /// 1 : 2 : 4 ... The lepton ratios are (mu/e, tau/mu, tau/e). Returns (octaveImplied, leptonObserved,
    /// nearestOctaveLine, maxDeviation).
    /// </summary>
    public static (double[] OctaveImplied, double[] LeptonObserved, int MatchingLines, double MaxDeviation)
        MassRatioAnalogs(int n = DefaultN, int K = DefaultK, double feedback = DefaultFeedback,
            double damping = DefaultDamping)
    {
        var octave = OctaveCenterRatios(n, K, feedback, damping);
        double[] lepton = { MMuon / MElectron, MTau / MMuon, MTau / MElectron };
        // each octave ratio 2^k must be within tolerance of a lepton ratio (or vice versa)
        int matching = 0;
        double maxDev = 0;
        foreach (double o in octave)
        {
            double best = double.MaxValue;
            foreach (double l in lepton) best = Math.Min(best, Math.Abs(l / o - 1.0));
            maxDev = Math.Max(maxDev, best);
            if (best < 0.25) matching++;
        }
        return (octave, lepton, matching, maxDev);
    }

    // ── 5. Family hierarchy ─────────────────────────────────────────────────────

    /// <summary>
    /// Family hierarchy: does the octave structure reproduce the family COUNT (= generation count) and a
    /// MONOTONE hierarchy (band positions increasing)? Returns (bandCount, isMonotone, matchesThree).
    /// </summary>
    public static (int BandCount, bool IsMonotone, bool MatchesThree) FamilyHierarchy(int n = DefaultN,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var bands = BandPositions(n, K, feedback, damping);
        bool monotone = true;
        for (int i = 1; i < bands.Length; i++)
            if (bands[i].Start <= bands[i - 1].Start) monotone = false;
        return (bands.Length, monotone, bands.Length == 3);
    }

    // ── Hierarchy score & classification ────────────────────────────────────────

    /// <summary>
    /// Hierarchy-origin score (0..5):
    /// 1. the octave structure has ≥ 3 bands (families);
    /// 2. the band positions form a monotone hierarchy;
    /// 3. the family count is exactly 3 (matches the generation count);
    /// 4. the octave scaling is geometric (factor-2 ladder);
    /// 5. at least one octave ratio reproduces a lepton ratio within 25% (mass-ratio correspondence).
    /// </summary>
    public static int HierarchyScore(int n = DefaultN, int K = DefaultK)
    {
        int score = 0;
        var bands = BandPositions(n, K);
        if (bands.Length >= 3) score++;
        if (FamilyHierarchy(n, K).IsMonotone) score++;
        if (FamilyHierarchy(n, K).MatchesThree) score++;
        if (GeometricOctaveScaling(n, K)) score++;
        if (MassRatioAnalogs(n, K).MatchingLines >= 1) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO RELATION      — the octave structure does not reproduce any fermion hierarchy feature (band
    ///                      count, monotonicity, or ratios all fail);
    ///   PARTIAL RELATION — the octave structure reproduces the family COUNT (3 = generation count) and a
    ///                      monotone geometric hierarchy, but the implied mass ratios (1:2:4) do NOT match
    ///                      the observed lepton ratios (1:17:207) — the count and ordering emerge, the
    ///                      numerical hierarchy does not;
    ///   HIERARCHY ORIGIN — the octave structure reproduces both the family count AND the observed mass
    ///                      ratios (a full fermion-hierarchy origin).
    /// </summary>
    public static string Classify(int n = DefaultN, int K = DefaultK)
    {
        int score = HierarchyScore(n, K);
        if (score <= 2) return "NO RELATION";
        if (score == 5) return "HIERARCHY ORIGIN";
        return "PARTIAL RELATION";
    }
}
