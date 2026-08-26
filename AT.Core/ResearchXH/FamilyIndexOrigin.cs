namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 135 — Origin of the family index. QG134 established that bosons are rung states while
/// fermions carry a family index inside an observable sector (FUNDAMENTAL SPLIT). This phase asks: can the
/// family index EMERGE from internal attractor structure WITHIN a single sector?
///
/// Method (computational, fully deterministic): the observable sector is the converged attractor of the
/// QG115/125 dynamics (radius 6, family count 3 per QG126). Its internal structure is its graph-Laplacian
/// spectrum. We measure: (1) INTRA-SECTOR MODES — the observable sector's Laplacian eigenvalue spectrum
/// (positive modes); (2) FAMILY SPLITTING — the octave-band family decomposition (QG00) of the SINGLE
/// sector's spectrum: each octave (frequency doubling) is a family; (3) FAMILY STABILITY — is the family
/// count stable across the observable dynamics parameters (damping, feedback, network size); (4) HIERARCHY
/// FORMATION — do the octave families form a genuine frequency-doubling hierarchy with non-empty families;
/// (5) GENERATION COUNT — does the intra-sector structure naturally produce the generation count 3.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class FamilyIndexOrigin
{
    /// <summary>Default dynamics parameters (matching QG115–134).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;

    // ── 1. Intra-sector modes ───────────────────────────────────────────────────

    /// <summary>
    /// The observable sector's internal Laplacian spectrum: positive (nonzero) eigenvalues in ascending
    /// order. These are the intra-sector modes.
    /// </summary>
    public static double[] IntraSectorModes(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        var (_, adj) = HighEnergySectorStability.ObservableSector(n, K, feedback, damping);
        return SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adj));
    }

    /// <summary>Number of positive intra-sector modes.</summary>
    public static int ModeCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
        => IntraSectorModes(n, K, feedback, damping).Length;

    // ── 2. Family splitting ─────────────────────────────────────────────────────

    /// <summary>
    /// Family splitting: the octave-band decomposition (QG00) of the SINGLE sector's spectrum. Each octave
    /// (frequency doubling from the fundamental mode) is one family. Returns (familySizes, familyCount).
    /// </summary>
    public static (int[] Sizes, int Count) FamilySplit(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var modes = IntraSectorModes(n, K, feedback, damping);
        var (sizes, _) = SpectralClasses.OctaveFamilies(modes);
        return (sizes, sizes.Length);
    }

    /// <summary>Family count from the intra-sector octave decomposition.</summary>
    public static int FamilyCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
        => FamilySplit(n, K, feedback, damping).Count;

    /// <summary>
    /// Generation count: the natural family count produced by the intra-sector structure. For the observable
    /// sector this should be 3 (the observed generation count).
    /// </summary>
    public static int GenerationCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
        => FamilyCount(n, K, feedback, damping);

    /// <summary>Does the intra-sector structure produce exactly 3 families (the observed generation count)?</summary>
    public static bool ThreeGenerationsEmerge(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
        => GenerationCount(n, K, feedback, damping) == 3;

    // ── 3. Family stability ─────────────────────────────────────────────────────

    /// <summary>
    /// Family stability: family counts of the observable sector across the dynamics parameter grid
    /// (feedback 0.3..0.9, damping 0.2..0.4). Returns the distinct family counts and whether ALL are 3.
    /// </summary>
    public static (int[] DistinctCounts, bool AllThree) FamilyStability(int n = 96, int K = DefaultK)
    {
        var counts = new List<int>();
        foreach (double f in new[] { 0.5, 0.7, 0.9 })
            foreach (double d in new[] { 0.2, 0.3, 0.4 })
                counts.Add(FamilyCount(n, K, f, d));
        return (counts.Distinct().OrderBy(c => c).ToArray(), counts.All(c => c == 3));
    }

    // ── 4. Hierarchy formation ──────────────────────────────────────────────────

    /// <summary>
    /// Hierarchy formation: do the octave families form a genuine frequency-doubling hierarchy? True if the
    /// family count is &gt;= 3 and every family is non-empty (each octave band is populated).
    /// </summary>
    public static bool HierarchyFormed(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        var (sizes, count) = FamilySplit(n, K, feedback, damping);
        return count >= 3 && sizes.All(s => s > 0);
    }

    // ── 5. Mode-band separation ─────────────────────────────────────────────────

    /// <summary>
    /// The octave band boundaries of the family structure: the first eigenvalue of each octave family
    /// (family start frequencies). A clean doubling hierarchy shows starts near ω₁·2^k.
    /// </summary>
    public static double[] FamilyStartFrequencies(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping)
    {
        var modes = IntraSectorModes(n, K, feedback, damping);
        var (_, starts) = SpectralClasses.OctaveFamilies(modes);
        return starts.Select(i => modes[i]).ToArray();
    }

    // ── Origin score & classification ───────────────────────────────────────────

    /// <summary>
    /// Family-origin score (0..5):
    /// 1. the single observable sector has positive intra-sector modes;
    /// 2. the octave decomposition splits the sector spectrum into &gt;= 3 families;
    /// 3. the family structure is stable across the dynamics parameter grid (all 3);
    /// 4. the octave hierarchy is fully formed (all bands populated);
    /// 5. the intra-sector structure produces EXACTLY 3 generations.
    /// </summary>
    public static int OriginScore(int n = 96, int K = DefaultK)
    {
        int score = 0;
        if (ModeCount(n, K) > 1) score++;
        if (FamilyCount(n, K) >= 3) score++;
        if (FamilyStability(n, K).AllThree) score++;
        if (HierarchyFormed(n, K)) score++;
        if (ThreeGenerationsEmerge(n, K)) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   POSTULATED    — the family index is not derivable from intra-sector structure (few/no internal
    ///                   modes, no stable 3-family octave splitting);
    ///   PARTIAL ORIGIN — some intra-sector family structure exists but it is not stable or not exactly 3
    ///                   generations;
    ///   FAMILY ORIGIN — the family index EMERGES from the internal attractor structure of a single sector:
    ///                   the observable sector's Laplacian spectrum splits into 3 stable octave families
    ///                   (generations), robust across the dynamics parameters — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK)
    {
        int score = OriginScore(n, K);
        if (score <= 2) return "POSTULATED";
        if (score == 5) return "FAMILY ORIGIN";
        return "PARTIAL ORIGIN";
    }
}
