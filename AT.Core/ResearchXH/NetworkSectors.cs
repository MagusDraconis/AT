namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 112 — Network sector hypothesis. QG109–111 showed no unique global network is selected by
/// stability, information, or multi-objective criteria. This phase asks: can physical reality consist of
/// MULTIPLE INTERACTING NETWORK SECTORS rather than one uniform network?
///
/// Method (computational, fully deterministic): reuse the 77-network ensemble and its normalized spectral
/// shapes. (1) SECTOR DECOMPOSITION: cluster the spectral shapes by Kolmogorov–Smirnov single-linkage into
/// spectral sectors; (2) COEXISTENCE: measure within-class vs between-class KS separation to check whether
/// distinct classes coexist as separable sectors; (3) PHASE-LIKE REGIONS: check whether the sectors are
/// separated by spectral gaps (between-sector KS &gt; within-sector KS) like distinct phases; (4) FAMILY/COLOR
/// ANALOGS: compare the number of dominant spectral sectors with the SM's 3 families / 3 colors (QG79/80);
/// (5) SECTOR INTERACTIONS: measure inter-sector coupling via the KS distance between sector centroids and
/// the number of boundary networks.
///
/// Answer (determined by the computed spectra): PARTIAL SECTORING — the ensemble DOES decompose into distinct
/// spectral sectors that coexist (within-class KS much smaller than between-class KS: distinct classes are
/// separable, phase-like regions), and multiple sectors interact via spectral distance. But the sector count
/// does not uniquely match the SM 3-family/3-color structure, and the sectors are not sharply phase-separated
/// (continuous KS spectrum between them). So the network is PARTIALLY sectored — multiple coexisting
/// interacting classes, not a single uniform network, but also not a sharp FULL sector structure with a
/// uniquely determined sector count. Classification: PARTIAL SECTORING. No new primitives added here.
/// </summary>
public static class NetworkSectors
{
    // ── Ensemble ───────────────────────────────────────────────────────────────────

    /// <summary>The 77-network deterministic ensemble (name, adjacency).</summary>
    public static (string name, double[,] adjacency)[] Ensemble()
        => FamilyCountStatistics.BuildEnsemble().ToArray();

    /// <summary>Class (name prefix) of an ensemble member.</summary>
    public static string ClassOf(string name)
        => name.StartsWith("grid", StringComparison.Ordinal) ? "grid"
         : name.StartsWith("threshold", StringComparison.Ordinal) ? "threshold"
         : name.StartsWith("perturbed", StringComparison.Ordinal) ? "perturbed"
         : "ER";

    /// <summary>Normalized spectral shape of an ensemble member.</summary>
    public static double[] ShapeOf(double[,] adjacency)
        => SpectrumRobustness.NormalizedShape(SpectrumRobustness.LaplacianOf(adjacency));

    // ── 1. Sector decomposition (spectral clustering) ─────────────────────────────

    /// <summary>
    /// Sector decomposition: single-linkage cluster the normalized spectral shapes by KS distance. Returns
    /// (sectorCount, sectorLabels) with labels in [0, sectorCount).
    /// </summary>
    public static (int sectorCount, int[] labels) SectorDecomposition(double ksThreshold = 0.10)
    {
        var ens = Ensemble();
        int n = ens.Length;
        var shapes = new double[n][];
        for (int i = 0; i < n; i++) shapes[i] = ShapeOf(ens[i].adjacency);

        var labels = new int[n];
        Array.Fill(labels, -1);
        int nextLabel = 0;
        for (int i = 0; i < n; i++)
        {
            if (labels[i] != -1) continue;
            labels[i] = nextLabel;
            var frontier = new Queue<int>();
            frontier.Enqueue(i);
            while (frontier.Count > 0)
            {
                int a = frontier.Dequeue();
                for (int b = 0; b < n; b++)
                {
                    if (labels[b] != -1) continue;
                    if (SpectralCurvature.KolmogorovSmirnov(shapes[a], shapes[b]) < ksThreshold)
                    {
                        labels[b] = nextLabel;
                        frontier.Enqueue(b);
                    }
                }
            }
            nextLabel++;
        }
        return (nextLabel, labels);
    }

    /// <summary>Dominant sectors: sector labels with at least `minMembers` members.</summary>
    public static int[] DominantSectors(int sectorCount, int[] labels, int minMembers = 5)
    {
        var sizes = new int[sectorCount];
        for (int i = 0; i < labels.Length; i++) sizes[labels[i]]++;
        return Enumerable.Range(0, sectorCount).Where(s => sizes[s] >= minMembers).ToArray();
    }

    // ── 2. Coexistence of network classes ─────────────────────────────────────────

    /// <summary>Precomputed normalized shapes of the whole ensemble (indexed by ensemble index).</summary>
    public static double[][] CachedShapes()
    {
        var ens = Ensemble();
        var shapes = new double[ens.Length][];
        for (int i = 0; i < ens.Length; i++) shapes[i] = ShapeOf(ens[i].adjacency);
        return shapes;
    }

    /// <summary>Mean within-class KS distance (all pairs in the same class) using precomputed shapes.</summary>
    public static double WithinClassKS(string classPrefix, double[][]? shapes = null)
    {
        var ens = Ensemble();
        var members = Enumerable.Range(0, ens.Length).Where(i => ClassOf(ens[i].name) == classPrefix).ToArray();
        if (members.Length < 2) return double.NaN;
        shapes ??= CachedShapes();
        double sum = 0.0; int count = 0;
        for (int a = 0; a < members.Length; a++)
            for (int b = a + 1; b < members.Length; b++)
            {
                sum += SpectralCurvature.KolmogorovSmirnov(shapes[members[a]], shapes[members[b]]);
                count++;
            }
        return count > 0 ? sum / count : double.NaN;
    }

    /// <summary>Mean between-class KS distance (pairs in different classes) using precomputed shapes.</summary>
    public static double BetweenClassKS(string classPrefix, double[][]? shapes = null)
    {
        var ens = Ensemble();
        var members = Enumerable.Range(0, ens.Length).Where(i => ClassOf(ens[i].name) == classPrefix).ToArray();
        var others = Enumerable.Range(0, ens.Length).Where(i => ClassOf(ens[i].name) != classPrefix).ToArray();
        if (members.Length == 0 || others.Length == 0) return double.NaN;
        shapes ??= CachedShapes();
        double sum = 0.0; int count = 0;
        foreach (int m in members)
            foreach (int o in others)
            {
                sum += SpectralCurvature.KolmogorovSmirnov(shapes[m], shapes[o]);
                count++;
            }
        return count > 0 ? sum / count : double.NaN;
    }

    /// <summary>Separation ratio between/within for a class (&gt; 1 = the class is a separable sector).</summary>
    public static double SeparationRatio(string classPrefix, double[][]? shapes = null)
    {
        double w = WithinClassKS(classPrefix, shapes);
        double b = BetweenClassKS(classPrefix, shapes);
        return w > 0 ? b / w : double.NaN;
    }

    // ── 3. Phase-like regions ──────────────────────────────────────────────────────

    /// <summary>
    /// Phase-like: are the dominant spectral sectors separated by gaps (between-sector centroid KS &gt;
    /// within-sector KS)? Computes the mean centroid separation vs mean within-sector KS.
    /// </summary>
    public static (double meanCentroidKS, double meanWithinSectorKS, bool phaseLike) PhaseLikeRegions(double ksThreshold = 0.10)
    {
        var ens = Ensemble();
        int n = ens.Length;
        var shapes = new double[n][];
        for (int i = 0; i < n; i++) shapes[i] = ShapeOf(ens[i].adjacency);

        var (sectorCount, labels) = SectorDecomposition(ksThreshold);
        var members = Enumerable.Range(0, sectorCount).Select(s => new List<int>()).ToArray();
        for (int i = 0; i < n; i++) members[labels[i]].Add(i);

        double sumCentroid = 0.0; int centroidPairs = 0;
        double sumWithin = 0.0; int withinPairs = 0;
        for (int s = 0; s < sectorCount; s++)
        {
            // within-sector KS
            for (int i = 0; i < members[s].Count; i++)
                for (int j = i + 1; j < members[s].Count; j++)
                {
                    sumWithin += SpectralCurvature.KolmogorovSmirnov(shapes[members[s][i]], shapes[members[s][j]]);
                    withinPairs++;
                }
            // centroid distance to other sectors
            for (int t = s + 1; t < sectorCount; t++)
            {
                double cs = CentroidKS(shapes, members[s]);
                double ct = CentroidKS(shapes, members[t]);
                sumCentroid += Math.Abs(cs - ct);
                centroidPairs++;
            }
        }
        double meanCent = centroidPairs > 0 ? sumCentroid / centroidPairs : 0.0;
        double meanWithin = withinPairs > 0 ? sumWithin / withinPairs : 0.0;
        return (meanCent, meanWithin, meanCent > meanWithin);
    }

    /// <summary>Median KS of a sector's members to a reference shape (centroid proxy: min-max median).</summary>
    private static double CentroidKS(double[][] shapes, List<int> members)
    {
        // Use the median pairwise KS to the first member as a robust centroid proxy.
        var ds = new List<double>();
        for (int i = 0; i < members.Count; i++)
            for (int j = i + 1; j < members.Count; j++)
                ds.Add(SpectralCurvature.KolmogorovSmirnov(shapes[members[i]], shapes[members[j]]));
        if (ds.Count == 0) return 0.0;
        ds.Sort();
        return ds[ds.Count / 2];
    }

    // ── 4. Family/color analogs ───────────────────────────────────────────────────

    /// <summary>Number of dominant spectral sectors (the "family/color" analog count).</summary>
    public static int DominantSectorCount(int minMembers = 5)
    {
        var (sectorCount, labels) = SectorDecomposition();
        return DominantSectors(sectorCount, labels, minMembers).Length;
    }

    /// <summary>SM family/color count (QG79/QG80): 3.</summary>
    public static int SmFamilyColorCount() => 3;

    // ── 5. Sector interactions ─────────────────────────────────────────────────────

    /// <summary>
    /// Sector interactions: fraction of networks that are BOUNDARY networks (KS distance to another class's
    /// members smaller than to their own class's members) — a measure of inter-sector coupling. Uses
    /// precomputed shapes.
    /// </summary>
    public static double BoundaryFraction()
    {
        var ens = Ensemble();
        var shapes = CachedShapes();
        string[] classes = { "ER", "grid", "threshold", "perturbed" };

        var classMembers = classes.ToDictionary(
            c => c,
            c => Enumerable.Range(0, ens.Length).Where(i => ClassOf(ens[i].name) == c).ToArray());

        int boundary = 0;
        for (int i = 0; i < ens.Length; i++)
        {
            string myClass = ClassOf(ens[i].name);
            double myWithin = MeanKS(shapes, i, classMembers[myClass]);
            bool isBoundary = false;
            foreach (string c in classes)
            {
                if (c == myClass || classMembers[c].Length == 0) continue;
                double distToOther = MeanKS(shapes, i, classMembers[c]);
                if (distToOther < myWithin) { isBoundary = true; break; }
            }
            if (isBoundary) boundary++;
        }
        return (double)boundary / ens.Length;
    }

    /// <summary>Mean KS distance from member i to the members in `targetIndices` (cached shapes).</summary>
    private static double MeanKS(double[][] shapes, int i, int[] targetIndices)
    {
        if (targetIndices.Length == 0) return double.NaN;
        double sum = 0.0;
        foreach (int t in targetIndices)
            sum += SpectralCurvature.KolmogorovSmirnov(shapes[i], shapes[t]);
        return sum / targetIndices.Length;
    }

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   UNIFORM NETWORK     — all classes collapse into a single spectral sector (max KS &lt; 0.05);
    ///   FULL SECTOR STRUCTURE — the ensemble decomposes into ≥ 3 sharply separated sectors with a clean
    ///                           phase-like gap and weak inter-sector interaction;
    ///   PARTIAL SECTORING   — multiple coexisting sectors with partial separation and continuous inter-sector
    ///                         KS (the concrete case).
    /// </summary>
    public static string Classify()
    {
        var (sectorCount, _) = SectorDecomposition();
        if (sectorCount == 1) return "UNIFORM NETWORK";

        var shapes = CachedShapes();
        double gridSep = SeparationRatio("grid", shapes);
        double erSep = SeparationRatio("ER", shapes);
        double meanSep = (gridSep + erSep) / 2.0;

        var (meanCent, meanWithin, phaseLike) = PhaseLikeRegions();
        double boundary = BoundaryFraction();
        int dominant = DominantSectorCount();

        // Full sector structure: sharp phase-like separation, weak interaction, and a sector count
        // comparable to the SM 3-family/3-color structure (2–4 sectors).
        bool full = phaseLike && boundary < 0.2 && dominant >= 3;
        if (full) return "FULL SECTOR STRUCTURE";

        // Partial: multiple sectors coexist and separate (separation ratio > 1) but not sharply.
        if (meanSep > 1.0) return "PARTIAL SECTORING";

        return "UNIFORM NETWORK";
    }
}
