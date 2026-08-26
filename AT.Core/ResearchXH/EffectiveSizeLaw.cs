namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 138 — Origin of the effective-size law. QG137 established that the family count follows the
/// effective size N/K (Pearson r = 0.950). This phase asks WHY N/K controls the family count: is it an
/// artifact, a dynamical coincidence, or a fundamental spectral/combinatorial law?
///
/// Method (computational, fully deterministic): the family count is the number of OCTAVE BANDS (QG00) of the
/// observable sector's Laplacian spectrum: family k contains the modes with ω ∈ [ω₁·2^k, ω₁·2^(k+1)). The
/// number of octave bands is therefore floor(log2(ω_max/ω_min)) + 1, i.e. the family count is set by the
/// SPECTRAL SPAN. For a K-neighbor circulant-like network the eigenvalues give ω_min ~ K^{3/2}/N (longest
/// wavelength) and ω_max ~ √K (shortest), so the spectral span ω_max/ω_min ∝ N/K and the family count ~
/// log2(N/K). We verify: (1) MODE DENSITY — total modes and the mode distribution across octave bands;
/// (2) OCTAVE SPACING — the octave band boundaries at ω₁·2^k; (3) SPECTRAL CROWDING — the skew of mode
/// density (most modes in the top octave); (4) EFFECTIVE HORIZON — the fundamental mode ω_min is set by the
/// longest wavelength (the effective horizon N/K); (5) FAMILY-BAND FORMATION — the exact identity
/// familyCount = floor(log2(span)) + 1 holds.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class EffectiveSizeLaw
{
    /// <summary>Default dynamics parameters (matching QG115–137).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;
    public const int DefaultN = 96;

    // ── 1. Mode density ─────────────────────────────────────────────────────────

    /// <summary>Total positive intra-sector modes (N−1 for a connected network).</summary>
    public static int ModeDensity(int n = DefaultN, int K = DefaultK)
        => FamilyIndexOrigin.IntraSectorModes(n, K).Length;

    /// <summary>Modes per octave band: (octaveIndex, lowBound, highBound, modeCount).</summary>
    public static (int Octave, double Low, double High, int Count)[] ModeDensityPerOctave(int n = DefaultN,
        int K = DefaultK)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K);
        if (w.Length == 0) return Array.Empty<(int, double, double, int)>();
        double w0 = w[0];
        var result = new List<(int, double, double, int)>();
        for (int oct = 0; oct < 40; oct++)
        {
            double lo = w0 * Math.Pow(2.0, oct);
            double hi = w0 * Math.Pow(2.0, oct + 1);
            int cnt = w.Count(x => x >= lo - 1e-12 && x < hi);
            if (cnt == 0) break;
            result.Add((oct, lo, hi, cnt));
        }
        return result.ToArray();
    }

    // ── 2. Octave spacing ───────────────────────────────────────────────────────

    /// <summary>
    /// Octave band boundaries: the first mode frequency of each octave band, and its ratio to ω₁·2^octave
    /// (ideal doubling). Ratios near 1 mean the band boundaries follow the exact octave doubling.
    /// </summary>
    public static (int Octave, double Start, double Ideal, double Ratio)[] OctaveSpacing(int n = DefaultN,
        int K = DefaultK)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K);
        if (w.Length == 0) return Array.Empty<(int, double, double, double)>();
        double w0 = w[0];
        var result = new List<(int, double, double, double)>();
        int oct = 0;
        for (int i = 0; i < w.Length; i++)
        {
            double ideal = w0 * Math.Pow(2.0, oct);
            if (w[i] >= ideal * 2.0) { oct++; i--; continue; }
            if (result.Count == 0 || result[^1].Item1 != oct)
                result.Add((oct, w[i], ideal, w[i] / ideal));
        }
        return result.ToArray();
    }

    /// <summary>Mean ratio of actual octave starts to the ideal ω₁·2^k (should be ≈ 1).</summary>
    public static double MeanOctaveRatio(int n = DefaultN, int K = DefaultK)
    {
        var sp = OctaveSpacing(n, K);
        return sp.Length == 0 ? double.NaN : sp.Average(x => x.Ratio);
    }

    // ── 3. Spectral crowding ────────────────────────────────────────────────────

    /// <summary>
    /// Spectral crowding: the fraction of modes in the TOP octave band. A large fraction (the familiar
    /// "most modes sit at high frequency") is the crowding signature of the octave hierarchy.
    /// </summary>
    public static double TopOctaveCrowding(int n = DefaultN, int K = DefaultK)
    {
        var bands = ModeDensityPerOctave(n, K);
        if (bands.Length == 0) return 0;
        int total = bands.Sum(b => b.Count);
        return (double)bands[^1].Count / Math.Max(total, 1);
    }

    // ── 4. Effective horizon ────────────────────────────────────────────────────

    /// <summary>
    /// Fundamental-mode wavelength: the longest wavelength mode ω_min scales like K^{3/2}/N (for the
    /// K-neighbor circulant), i.e. the effective horizon is N/K link-length steps. Returns the fundamental
    /// frequency and the effective size N/K.
    /// </summary>
    public static (double Fundamental, double EffectiveSize) EffectiveHorizon(int n = DefaultN, int K = DefaultK)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K);
        double fund = w.Length > 0 ? w[0] : double.NaN;
        return (fund, (double)n / K);
    }

    /// <summary>
    /// Spectral-span law: the log2 of the spectral span (ω_max/ω_min) should grow ~ log2(N/K) across the
    /// (N, K) grid. Returns the Pearson correlation of log2(span) with log2(N/K).
    /// </summary>
    public static double SpanEffectiveSizeCorrelation(double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        var pts = new List<(double, double)>();
        foreach (int n in new[] { 48, 64, 96, 128, 192 })
            foreach (int K in new[] { 3, 4, 5, 6, 8, 10 })
            {
                if (n / K < 6) continue;
                var w = FamilyIndexOrigin.IntraSectorModes(n, K, feedback, damping);
                if (w.Length < 2) continue;
                pts.Add((Math.Log2((double)n / K), Math.Log2(w[^1] / w[0])));
            }
        return EffectiveSizeFamilies.Pearson(pts.Select(p => p.Item1).ToArray(),
            pts.Select(p => p.Item2).ToArray());
    }

    // ── 5. Family-band formation ────────────────────────────────────────────────

    /// <summary>
    /// The fundamental identity: familyCount = floor(log2(ω_max/ω_min)) + 1, i.e. the family count is
    /// exactly the number of octave bands spanned by the spectrum. Returns the identity check.
    /// </summary>
    public static bool FamilyBandIdentity(int n = DefaultN, int K = DefaultK)
    {
        var w = FamilyIndexOrigin.IntraSectorModes(n, K);
        if (w.Length == 0) return false;
        int fromSpan = (int)Math.Floor(Math.Log2(w[^1] / w[0])) + 1;
        int actual = FamilyIndexOrigin.FamilyCount(n, K);
        return fromSpan == actual;
    }

    /// <summary>Does the family-count = octave-band-count identity hold across the (N, K) grid?</summary>
    public static bool IdentityHoldsAcrossGrid(double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        foreach (int n in new[] { 48, 64, 96, 128, 192 })
            foreach (int K in new[] { 3, 4, 5, 6, 8, 10 })
            {
                if (n / K < 6) continue;
                if (!FamilyBandIdentity(n, K)) return false;
            }
        return true;
    }

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Origin score (0..5):
    /// 1. the octave band boundaries follow the ideal doubling (mean ratio ≈ 1);
    /// 2. spectral crowding is strong (top octave holds &gt; 50% of modes);
    /// 3. log2(spectral span) correlates strongly (r &gt; 0.8) with log2(N/K);
    /// 4. the familyCount = floor(log2(span)) + 1 identity holds at the default point;
    /// 5. the identity holds across the whole (N, K) grid.
    /// </summary>
    public static int OriginScore()
    {
        int score = 0;
        double ratio = MeanOctaveRatio();
        if (!double.IsNaN(ratio) && Math.Abs(ratio - 1.0) < 0.3) score++;
        if (TopOctaveCrowding() > 0.5) score++;
        if (SpanEffectiveSizeCorrelation() > 0.8) score++;
        if (FamilyBandIdentity()) score++;
        if (IdentityHoldsAcrossGrid()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   ARTIFACT     — the N/K law is a numerical artifact (identity fails, no octave doubling, no span
    ///                  correlation);
    ///   DYNAMICAL    — the law arises from the specific dynamics parameters but is not a general spectral
    ///                  identity;
    ///   FUNDAMENTAL  — the family count IS the octave-band count, and the octave-band count is
    ///                  floor(log2(spectral span)) + 1 with spectral span ∝ N/K for the K-neighbor network:
    ///                  a spectral/combinatorial law — the concrete case.
    /// </summary>
    public static string Classify()
    {
        int score = OriginScore();
        if (score <= 2) return "ARTIFACT";
        if (score == 5) return "FUNDAMENTAL";
        return "DYNAMICAL";
    }
}
