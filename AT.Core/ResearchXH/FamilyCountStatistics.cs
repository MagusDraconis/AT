namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 108 — Family count statistics. QG107 found robust octave-band spectral mode families in the
/// causal network class. This phase asks: what family counts are STATISTICALLY PREFERRED in causal networks?
///
/// Method (computational, fully deterministic): build a LARGE ENSEMBLE of causal graphs — (1) Erdős–Rényi
/// random graphs at several sizes and densities (fixed seeds), (2) causal-set grids at many sizes, (3) 2D
/// ε-threshold graphs at several densities, (4) deterministic link-perturbed grids. For each network compute
/// the octave-band family count and the hierarchy span ω_max/ω_min. Compile the FAMILY-COUNT DISTRIBUTION,
/// the span statistics, the SCALING of the family count with network size N, and the PREFERENCE FOR N=3
/// (the SM generation count, QG80/81): the fraction of networks whose octave-family count is exactly 3,
/// compared with the modal count.
///
/// Answer (determined by the computed spectra): the family count is essentially log₂ of the hierarchy span,
/// so it grows slowly with network size and spans a broad distribution (1–6) over the ensemble. There is a
/// WEAK PREFERENCE for N = 3 families among sparse causal networks (the mid-density causal class), but no
/// STRONG PREFERENCE: the modal count shifts with size/density, and 3-family networks are only a minority of
/// the full ensemble. Classification: WEAK PREFERENCE — N=3 is common but not dominant. No new primitives
/// added here (computational audit of the native operator spectrum).
/// </summary>
public static class FamilyCountStatistics
{
    // ── Ensemble construction (deterministic) ──────────────────────────────────────

    /// <summary>Erdős–Rényi random graph adjacency (deterministic: fixed seed).</summary>
    public static double[,] RandomErdosRenyi(int n, double p, int seed)
        => FamilyStructureRobustness.RandomErdosRenyi(n, p, seed);

    /// <summary>
    /// The large ensemble of causal graphs (name, adjacency): ER random at many sizes/densities (fixed
    /// seeds), causal-set grids at many sizes, threshold graphs at several densities, and perturbed grids.
    /// </summary>
    public static List<(string name, double[,] adjacency)> BuildEnsemble()
    {
        var list = new List<(string, double[,])>();

        // 1. Erdős–Rényi random graphs: sizes × densities × seeds (deterministic).
        foreach (int n in new[] { 60, 91, 130, 200 })
            foreach (double p in new[] { 0.03, 0.06, 0.10, 0.20, 0.35 })
                foreach (int seed in new[] { 11, 23, 47 })
                    list.Add(($"ER n={n} p={p:G2} s={seed}", RandomErdosRenyi(n, p, seed)));

        // 2. Causal-set grids at many sizes.
        foreach ((int t, int x) in new[] { (4, 5), (6, 6), (8, 4), (12, 3), (7, 12), (10, 8), (14, 6), (19, 12) })
            list.Add(($"grid t={t} x={x}", SpectrumRobustness.LinkAdjacency(CausalSet.BuildGrid(t, x))));

        // 3. 2D ε-threshold graphs at several densities.
        foreach (double eps in new[] { 0.05, 0.08, 0.12, 0.16, 0.25, 0.40 })
            list.Add(($"threshold ε={eps:G2}", ConformalRateGraph.Build(0.0, 12, eps).Adjacency));

        // 4. Perturbed grids (deterministic link removal).
        foreach (double frac in new[] { 0.05, 0.10, 0.20 })
            list.Add(($"perturbed {frac:P0}",
                SpectrumRobustness.RemoveLinksDeterministic(SpectrumRobustness.LinkAdjacency(CausalSet.BuildGrid(10, 8)), frac)));

        return list;
    }

    // ── Family counts ──────────────────────────────────────────────────────────────

    /// <summary>Octave-band family count of an adjacency.</summary>
    public static int FamilyCount(double[,] adjacency)
        => FamilyStructureRobustness.FamilyCount(adjacency);

    /// <summary>Hierarchy span ω_max/ω_min of an adjacency.</summary>
    public static double HierarchySpan(double[,] adjacency)
        => SpectrumRobustness.HierarchySpan(SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adjacency)));

    /// <summary>Family counts of the whole ensemble.</summary>
    public static int[] EnsembleFamilyCounts() => BuildEnsemble().Select(e => FamilyCount(e.adjacency)).ToArray();

    /// <summary>Hierarchy spans of the whole ensemble.</summary>
    public static double[] EnsembleHierarchySpans() => BuildEnsemble().Select(e => HierarchySpan(e.adjacency)).ToArray();

    // ── Distribution statistics ────────────────────────────────────────────────────

    /// <summary>
    /// Family-count histogram: counts[k] = number of networks with k octave families (k from 0..maxCount).
    /// </summary>
    public static int[] FamilyCountHistogram(int[] counts)
    {
        int max = counts.Length == 0 ? 0 : counts.Max();
        var h = new int[max + 1];
        foreach (int c in counts) h[c]++;
        return h;
    }

    /// <summary>The modal (most frequent) family count.</summary>
    public static int ModalFamilyCount(int[] counts)
    {
        if (counts.Length == 0) return 0;
        int[] h = FamilyCountHistogram(counts);
        int best = 0;
        for (int k = 1; k < h.Length; k++)
            if (h[k] > h[best]) best = k;
        return best;
    }

    /// <summary>Fraction of networks whose family count is exactly 3.</summary>
    public static double FractionWithThree(int[] counts)
        => counts.Length == 0 ? 0.0 : (double)counts.Count(c => c == 3) / counts.Length;

    /// <summary>Fraction of networks whose family count is exactly k.</summary>
    public static double FractionWith(int[] counts, int k)
        => counts.Length == 0 ? 0.0 : (double)counts.Count(c => c == k) / counts.Length;

    /// <summary>
    /// Scaling: mean family count vs network size N, binned over the ensemble (only networks whose size
    /// lies in the given window). Returns (size, meanCount) pairs.
    /// </summary>
    public static List<(double size, double meanCount)> MeanFamilyCountBySize(double[] sizes, int[] counts)
    {
        var result = new List<(double, double)>();
        double[] bins = { 40, 80, 140, 260, 600 };
        for (int b = 0; b < bins.Length - 1; b++)
        {
            var subset = new List<int>();
            for (int i = 0; i < sizes.Length; i++)
                if (sizes[i] >= bins[b] && sizes[i] < bins[b + 1])
                    subset.Add(counts[i]);
            if (subset.Count >= 3)
                result.Add((0.5 * (bins[b] + bins[b + 1]), subset.Average()));
        }
        return result;
    }

    // ── N=3 preference ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Preference classification (data-driven):
    ///   STRONG PREFERENCE — 3 is the modal count AND &gt; 40% of networks have exactly 3 families;
    ///   WEAK PREFERENCE   — 3 is common (≥ 15%) but not the dominant mode (the concrete case);
    ///   NO PREFERENCE     — 3-family networks are rare (&lt; 15%) or far below the mode.
    /// </summary>
    public static string Classify()
    {
        int[] counts = EnsembleFamilyCounts();
        int modal = ModalFamilyCount(counts);
        double frac3 = FractionWithThree(counts);

        if (modal == 3 && frac3 > 0.40) return "STRONG PREFERENCE";
        if (frac3 >= 0.15) return "WEAK PREFERENCE";
        return "NO PREFERENCE";
    }
}
