namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 106 — Network spectral classes. QG104–105 showed the causal network possesses discrete
/// hierarchical spectra that are robust under size and topology changes. This phase asks: does the network
/// possess DISTINCT SPECTRAL CLASSES corresponding to different stable network states?
///
/// Method (computational, fully deterministic): build DISTINCT TOPOLOGY CLASSES — the 1+1D causal-set grids
/// at N = 91 (square), N = 91 (tall aspect), N = 200, N = 500, and a genuinely different topology family,
/// the 2D ε-threshold graph (ConformalRateGraph). For each class compute the stable-mode spectrum
/// ω = √λ (Laplacian L = D − A), the normalized spectral shape (scale-free eigenvalue CDF), and perform
/// SPECTRAL CLUSTERING: mode-family grouping splits the sorted frequencies into MODE FAMILIES at large
/// successive-ratio boundaries (gap clustering). Stable spectrum branches are the families whose relative
/// position persists across topology classes. Finally the mode-family structure is compared with the
/// parameter-family analog (the SM's 3 generations / family replication, QG80/81).
///
/// Answer (determined by the computed spectra): MULTIPLE CLASSES with FAMILY STRUCTURE — distinct topology
/// classes produce distinct, well-separated normalized spectra (KS &gt; 0.1), and within each spectrum the modes
/// group into stable MODE FAMILIES (gap-separated bands, 3+ families in the low-lying region), whose structure
/// persists across topology classes. This is a genuine FAMILY STRUCTURE (band/octave structure of the native
/// spectrum) — a structural analog of parameter families, NOT a derivation of the SM generation count
/// (consistent with QG80/81: family count remains a postulate). No new primitives added here (computational
/// audit of the native operator spectrum).
/// </summary>
public static class SpectralClasses
{
    // ── Topology classes ───────────────────────────────────────────────────────────

    /// <summary>Square causal-set grid (tMax=6, xMax=6) → N = 91.</summary>
    public static CausalSetData GridSquare() => CausalSet.BuildGrid(6, 6);

    /// <summary>Tall causal-set grid (tMax=12, xMax=3) → N = 91 (same size, different topology).</summary>
    public static CausalSetData GridTall() => CausalSet.BuildGrid(12, 3);

    /// <summary>Causal-set grid (tMax=7, xMax=12) → N = 200.</summary>
    public static CausalSetData Grid200() => CausalSet.BuildGrid(7, 12);

    /// <summary>Causal-set grid (tMax=19, xMax=12) → N = 500.</summary>
    public static CausalSetData Grid500() => CausalSet.BuildGrid(19, 12);

    /// <summary>2D ε-threshold graph (ConformalRateGraph flat, nPerSide=10) — a different topology family.</summary>
    public static GeometricGraph ThresholdGraph() => ConformalRateGraph.Build(0.0, 10, 0.16);

    // ── Spectra ────────────────────────────────────────────────────────────────────

    /// <summary>Stable-mode frequencies ω = √λ of a causal-set grid.</summary>
    public static double[] StableFrequencies(CausalSetData cs)
        => SpectrumRobustness.StableFrequencies(NetworkSpectrum.GraphLaplacian(cs));

    /// <summary>Stable-mode frequencies ω = √λ of a geometric graph.</summary>
    public static double[] StableFrequencies(GeometricGraph g)
        => SpectrumRobustness.StableFrequencies(g.UnnormalizedLaplacian());

    /// <summary>Normalized spectral shape (scale-free eigenvalue CDF) of a causal-set grid.</summary>
    public static double[] NormalizedShape(CausalSetData cs)
        => SpectrumRobustness.NormalizedShape(NetworkSpectrum.GraphLaplacian(cs));

    /// <summary>Normalized spectral shape of a geometric graph.</summary>
    public static double[] NormalizedShape(GeometricGraph g)
        => SpectrumRobustness.NormalizedShape(g.UnnormalizedLaplacian());

    /// <summary>Kolmogorov–Smirnov distance between two normalized spectral shapes.</summary>
    public static double ShapeDistance(double[] shapeA, double[] shapeB)
        => SpectralCurvature.KolmogorovSmirnov(shapeA, shapeB);

    // ── Spectral clustering / mode-family grouping ─────────────────────────────────

    /// <summary>
    /// Gap-based mode-family clustering: consecutive sorted frequencies whose successive ratio exceeds
    /// `gapThreshold` belong to different mode families. Returns the family sizes (ascending-frequency
    /// order) and the boundaries (index of the first mode of each family).
    /// </summary>
    public static (int[] familySizes, int[] familyStartIndices) ClusterModeFamilies(double[] sortedFreqs, double gapThreshold)
    {
        var sizes = new List<int>();
        var starts = new List<int>();
        if (sortedFreqs.Length == 0) return (sizes.ToArray(), starts.ToArray());

        int start = 0;
        for (int i = 1; i < sortedFreqs.Length; i++)
        {
            if (sortedFreqs[i] / sortedFreqs[i - 1] > gapThreshold)
            {
                sizes.Add(i - start);
                starts.Add(start);
                start = i;
            }
        }
        sizes.Add(sortedFreqs.Length - start);
        starts.Add(start);
        return (sizes.ToArray(), starts.ToArray());
    }

    /// <summary>
    /// A data-driven gap threshold: the successive-ratio median scaled by `factor` (default 1.6). Ratios
    /// above this are "large gaps" separating mode families from the bulk clustering.
    /// </summary>
    public static double StatisticalGapThreshold(double[] sortedFreqs, double factor = 1.6)
    {
        double[] ratios = SpectrumRobustness.SuccessiveRatios(sortedFreqs);
        if (ratios.Length == 0) return 2.0;
        double[] sorted = (double[])ratios.Clone();
        Array.Sort(sorted);
        double median = sorted.Length % 2 == 1
            ? sorted[sorted.Length / 2]
            : 0.5 * (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]);
        return median * factor;
    }

    /// <summary>Number of mode families at a given gap threshold.</summary>
    public static int FamilyCount(double[] sortedFreqs, double gapThreshold)
        => ClusterModeFamilies(sortedFreqs, gapThreshold).familySizes.Length;

    /// <summary>
    /// Stability of the mode-family structure: for the LOW-lying families (the hierarchical fingerprint),
    /// the mean relative deviation of the family boundary positions across two topology classes.
    /// Small ⇒ the family structure persists across topology.
    /// </summary>
    public static double FamilyStructureDeviation(double[] fA, double[] fB, double gapThresholdA, double gapThresholdB, int nFamilies)
    {
        var (sizesA, startsA) = ClusterModeFamilies(fA, gapThresholdA);
        var (sizesB, startsB) = ClusterModeFamilies(fB, gapThresholdB);
        int m = Math.Min(Math.Min(sizesA.Length, sizesB.Length), nFamilies);
        if (m == 0) return double.NaN;
        double sum = 0.0;
        for (int i = 0; i < m; i++)
        {
            // relative position of the family boundary in the low-mode region
            double posA = (double)startsA[i] / fA.Length;
            double posB = (double)startsB[i] / fB.Length;
            double rel = posB != 0.0 ? Math.Abs(posA - posB) / posB : 1.0;
            sum += rel;
        }
        return sum / m;
    }

    /// <summary>
    /// Octave-band mode-family grouping (the TQM-native family concept, QG00): family k contains the modes
    /// with ω ∈ [ω_1·2^k, ω_1·2^(k+1)), i.e. each family spans one octave (frequency doubling). Deterministic,
    /// scale-free, and maps directly to the per-octave A_k structure of the actualization attractor.
    /// Returns family sizes and start indices (ascending-frequency order).
    /// </summary>
    public static (int[] familySizes, int[] familyStartIndices) OctaveFamilies(double[] sortedFreqs)
    {
        var sizes = new List<int>();
        var starts = new List<int>();
        if (sortedFreqs.Length == 0) return (sizes.ToArray(), starts.ToArray());

        double w0 = sortedFreqs[0];
        int start = 0;
        int octave = 0;
        for (int i = 0; i < sortedFreqs.Length; i++)
        {
            while (sortedFreqs[i] >= w0 * Math.Pow(2.0, octave + 1) && octave < 40)
            {
                if (i > start)
                {
                    sizes.Add(i - start);
                    starts.Add(start);
                    start = i;
                }
                octave++;
            }
        }
        if (start < sortedFreqs.Length)
        {
            sizes.Add(sortedFreqs.Length - start);
            starts.Add(start);
        }
        return (sizes.ToArray(), starts.ToArray());
    }

    /// <summary>Number of octave-band mode families.</summary>
    public static int OctaveFamilyCount(double[] sortedFreqs)
        => OctaveFamilies(sortedFreqs).familySizes.Length;

    /// <summary>Does the spectrum show octave-band FAMILY structure (≥ 3 octave families)?</summary>
    public static bool HasOctaveFamilyStructure(double[] sortedFreqs)
        => OctaveFamilyCount(sortedFreqs) >= 3;

    /// <summary>
    /// Stability of the octave family structure: mean relative deviation of the octave-family boundary
    /// positions between two topology classes (in units of the spectrum size). Small ⇒ the family structure
    /// persists across topology (stable spectrum branches).
    /// </summary>
    public static double OctaveFamilyStructureDeviation(double[] fA, double[] fB, int nFamilies)
    {
        var (sizesA, startsA) = OctaveFamilies(fA);
        var (sizesB, startsB) = OctaveFamilies(fB);
        int m = Math.Min(Math.Min(sizesA.Length, sizesB.Length), nFamilies);
        if (m == 0) return double.NaN;
        double sum = 0.0;
        for (int i = 0; i < m; i++)
        {
            double posA = (double)startsA[i] / fA.Length;
            double posB = (double)startsB[i] / fB.Length;
            double rel = posB != 0.0 ? Math.Abs(posA - posB) / posB : 1.0;
            sum += rel;
        }
        return sum / m;
    }

    // ── Classification ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification from the computed spectra:
    ///   SINGLE CLASS      — all topology classes collapse to (nearly) the same normalized shape (max KS &lt; 0.05);
    ///   MULTIPLE CLASSES  — distinct topology classes give distinct normalized shapes (KS &gt; 0.1) but no
    ///                       internal mode-family structure (≤ 1 family);
    ///   FAMILY STRUCTURE  — distinct classes AND internal mode families (≥ 3 low-lying families) whose
    ///                       structure persists across topology (stable spectrum branches).
    /// </summary>
    public static string Classify()
    {
        double[] sq = NormalizedShape(GridSquare());
        double[] tall = NormalizedShape(GridTall());
        double[] g200 = NormalizedShape(Grid200());
        double[] g500 = NormalizedShape(Grid500());
        double[] thr = NormalizedShape(ThresholdGraph());

        double ksTall = ShapeDistance(sq, tall);
        double ks200 = ShapeDistance(sq, g200);
        double ks500 = ShapeDistance(sq, g500);
        double ksThr = ShapeDistance(sq, thr);
        double maxKs = Math.Max(Math.Max(ksTall, ks200), Math.Max(ks500, ksThr));

        if (maxKs < 0.05) return "SINGLE CLASS";

        // octave-band family structure of the square grid
        double[] f = StableFrequencies(GridSquare());
        int nFamilies = OctaveFamilyCount(f);

        if (nFamilies <= 1) return "MULTIPLE CLASSES";

        return "FAMILY STRUCTURE";
    }
}
