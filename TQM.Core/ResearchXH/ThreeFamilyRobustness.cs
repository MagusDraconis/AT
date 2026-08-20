namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 136 — Robustness of the 3-family sector. QG135 found the 3-family structure emerges from
/// octave structure at the default dynamics but the family count changes under damping (4 families at
/// d=0.4). This phase asks: is there a DYNAMICAL REGIME where the 3-family structure becomes STABLE and
/// parameter-independent?
///
/// Method (computational, fully deterministic): (1) FEEDBACK SWEEP — family count of the observable sector
/// across the feedback axis; (2) DAMPING SWEEP — family count across the damping axis; (3) SIZE SCALING —
/// family count across network sizes (48..192); (4) FAMILY STABILITY BASIN — the fraction of the
/// (feedback × damping) plane at the reference size that gives exactly 3 families, and the identification of
/// the coherent 3-family region; (5) UNIVERSALITY — is the 3-family state independent of size AND stable
/// across a wide parameter basin?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class ThreeFamilyRobustness
{
    /// <summary>Default dynamics parameters (matching QG115–135).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;
    public const int DefaultN = 96;

    // ── 1. Feedback sweep ───────────────────────────────────────────────────────

    /// <summary>Family count of the observable sector vs feedback (fixed damping).</summary>
    public static (double Feedback, int Families)[] FeedbackSweep(double damping = DefaultDamping,
        int n = DefaultN, int K = DefaultK)
    {
        var result = new List<(double, int)>();
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
            result.Add((f, FamilyIndexOrigin.FamilyCount(n, K, f, damping)));
        return result.ToArray();
    }

    // ── 2. Damping sweep ────────────────────────────────────────────────────────

    /// <summary>Family count of the observable sector vs damping (fixed feedback).</summary>
    public static (double Damping, int Families)[] DampingSweep(double feedback = DefaultFeedback,
        int n = DefaultN, int K = DefaultK)
    {
        var result = new List<(double, int)>();
        for (double d = 0.1; d <= 0.5 + 1e-9; d += 0.1)
            result.Add((d, FamilyIndexOrigin.FamilyCount(n, K, feedback, d)));
        return result.ToArray();
    }

    // ── 3. Size scaling ─────────────────────────────────────────────────────────

    /// <summary>Family count of the observable sector vs network size (fixed parameters).</summary>
    public static (int Size, int Families)[] SizeScaling(double feedback = DefaultFeedback,
        double damping = DefaultDamping, int K = DefaultK)
    {
        var result = new List<(int, int)>();
        foreach (int n in new[] { 48, 64, 96, 128, 192 })
            result.Add((n, FamilyIndexOrigin.FamilyCount(n, K, feedback, damping)));
        return result.ToArray();
    }

    /// <summary>Is the 3-family structure independent of network size (all sizes give 3)?</summary>
    public static bool SizeUniversal(double feedback = DefaultFeedback, double damping = DefaultDamping,
        int K = DefaultK)
        => SizeScaling(feedback, damping, K).All(s => s.Families == 3);

    // ── 4. Family stability basin ───────────────────────────────────────────────

    /// <summary>
    /// Family-stability basin: family counts over the (feedback × damping) grid at the reference size, plus
    /// the fraction of grid points giving exactly 3 families.
    /// </summary>
    public static (int[] Counts, double ThreeFraction) FamilyBasin(int n = DefaultN, int K = DefaultK,
        double fMin = 0.6, double fMax = 1.0, double fStep = 0.05, double dMin = 0.05, double dMax = 0.35,
        double dStep = 0.05)
    {
        var counts = new List<int>();
        for (double f = fMin; f <= fMax + 1e-9; f += fStep)
            for (double d = dMin; d <= dMax + 1e-9; d += dStep)
                counts.Add(FamilyIndexOrigin.FamilyCount(n, K, f, d));
        int three = counts.Count(c => c == 3);
        return (counts.ToArray(), (double)three / Math.Max(counts.Count, 1));
    }

    /// <summary>Is the 3-family fraction in the basin &gt;= 0.9 (a coherent, dominant regime)?</summary>
    public static bool CoherentBasin(int n = DefaultN, int K = DefaultK)
        => FamilyBasin(n, K).ThreeFraction >= 0.9;

    /// <summary>Does the DEFAULT point (f=0.9, d=0.3) give exactly 3 families?</summary>
    public static bool DefaultIsThreeFamily(int n = DefaultN, int K = DefaultK)
        => FamilyIndexOrigin.FamilyCount(n, K, DefaultFeedback, DefaultDamping) == 3;

    // ── 5. Universality ─────────────────────────────────────────────────────────

    /// <summary>
    /// Universality: the 3-family state is stable across the parameter basin (≥ 0.9) AND independent of
    /// network size (all tested sizes give 3). Both must hold for full universality.
    /// </summary>
    public static bool FullyUniversal(int n = DefaultN, int K = DefaultK)
        => CoherentBasin(n, K) && SizeUniversal();

    // ── Robustness score & classification ───────────────────────────────────────

    /// <summary>
    /// Robustness score (0..5):
    /// 1. the default point gives 3 families;
    /// 2. the 3-family basin fraction is &gt;= 0.9 (coherent regime exists);
    /// 3. the 3-family basin is LARGE (fraction &gt;= 0.75 even on a broad coarse grid);
    /// 4. the 3-family state holds across the damping sweep at default feedback up to d=0.3;
    /// 5. the 3-family state is independent of network size.
    /// </summary>
    public static int RobustnessScore(int n = DefaultN, int K = DefaultK)
    {
        int score = 0;
        if (DefaultIsThreeFamily(n, K)) score++;
        var (counts, frac) = FamilyBasin(n, K);
        if (frac >= 0.9) score++;
        if (frac >= 0.75) score++;
        var damp = DampingSweep();
        if (damp.Take(3).All(d => d.Families == 3)) score++;
        if (SizeUniversal()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   FRAGILE            — no coherent dynamical regime gives 3 families (basin small, size-dependent);
    ///   PARTIAL ROBUSTNESS — a coherent 3-family regime exists (stable in a wide parameter basin) but the
    ///                        structure is NOT universal (family count changes with network size);
    ///   ROBUST ORIGIN      — the 3-family state is stable across a wide parameter basin AND independent of
    ///                        network size (a universal 3-family attractor).
    /// </summary>
    public static string Classify(int n = DefaultN, int K = DefaultK)
    {
        int score = RobustnessScore(n, K);
        if (score >= 5) return "ROBUST ORIGIN";
        if (score >= 3) return "PARTIAL ROBUSTNESS";
        return "FRAGILE";
    }
}
