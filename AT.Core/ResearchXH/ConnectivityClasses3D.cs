namespace AT.Core.ResearchXH;

/// <summary>
/// AT-QG Phase 114 — 3D connectivity classes. QG87 showed higher cells (faces, volumes) are derived composites
/// (no independent d.o.f.). This phase asks: can LOCAL 3D CONNECTIVITY — valence and neighborhood geometry —
/// generate discrete classes of network states?
///
/// Method (computational, fully deterministic): build regular graphs of fixed valence k = 3,4,5,6 (circulant
/// graphs — every node has the same local connectivity) and measure: (1) VALENCE classes — do different
/// valences produce distinct spectral classes (normalized-shape KS &gt; 0.1, distinct family counts)? (2)
/// TETRAHEDRAL STRUCTURES — count K₄ cliques (local 3D volumes: a node with 4 mutually linked neighbors is a
/// tetrahedral cell); (3) LOCAL VOLUME GEOMETRY — mean tetrahedra per node; (4) CONNECTIVITY DEGENERACIES —
/// eigenvalue multiplicities of the Laplacian (degenerate eigenvalues = degenerate connectivity classes); and
/// (5) FAMILY/COLOR ANALOGS — the number of distinct connectivity classes vs the SM 3-family/3-color count
/// (QG79/80).
///
/// Answer (determined by the computed data): PARTIAL RELATION — local 3D connectivity DOES generate discrete
/// classes of network states (each valence gives a distinct spectral class, tetrahedral/volume structure is
/// real and derived from local connectivity, and eigenvalue degeneracies produce discrete connectivity
/// classes). But the connectivity-class count is not uniquely 3 (it tracks valence/size), and connectivity is
/// a LOCAL property that cannot by itself select the internal SM family/color count (consistent with QG83:
/// valence 3 is a graph-theory fact, COINCIDENTAL with color/family 3). Classification: PARTIAL RELATION —
/// real discrete connectivity classes, without determination of the SM counts. No new primitives added here.
/// </summary>
public static class ConnectivityClasses3D
{
    // ── Valence-class networks ─────────────────────────────────────────────────────

    /// <summary>
    /// A k-regular circulant graph on n nodes: node i connects to i±1, i±2, …, i±(k/2) mod n.
    /// Every node has local connectivity k (fixed valence). Deterministic.
    /// </summary>
    public static double[,] ValenceGraph(int n, int valence)
    {
        var a = new double[n, n];
        int half = valence / 2;
        for (int i = 0; i < n; i++)
            for (int d = 1; d <= half; d++)
            {
                int j = (i + d) % n;
                a[i, j] = 1.0; a[j, i] = 1.0;
            }
        // for odd valence, add the "long" chords to keep degree = valence when n is even and large enough
        if (valence % 2 == 1)
        {
            for (int i = 0; i < n; i++)
            {
                int j = (i + n / 2) % n;
                if (i != j) { a[i, j] = 1.0; a[j, i] = 1.0; }
            }
        }
        return a;
    }

    /// <summary>Mean degree of an adjacency (the realized valence).</summary>
    public static double MeanValence(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        double sum = 0.0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++) sum += adjacency[i, j];
        return sum / n;
    }

    // ── 1. Valence spectral classes ───────────────────────────────────────────────

    /// <summary>Normalized spectral shape of an adjacency (scale-free eigenvalue CDF).</summary>
    public static double[] SpectralShape(double[,] adjacency)
        => SpectrumRobustness.NormalizedShape(SpectrumRobustness.LaplacianOf(adjacency));

    /// <summary>KS distance between the spectral shapes of two valences.</summary>
    public static double ValenceClassDistance(int vA, int vB, int n = 120)
        => SpectralCurvature.KolmogorovSmirnov(SpectralShape(ValenceGraph(n, vA)), SpectralShape(ValenceGraph(n, vB)));

    /// <summary>Octave-family count of a valence class (the QG106 family structure).</summary>
    public static int ValenceFamilyCount(int valence, int n = 120)
        => SpectralClasses.OctaveFamilyCount(SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(ValenceGraph(n, valence))));

    /// <summary>Are the valence classes DISTINCT (all pairwise KS &gt; 0.1)?</summary>
    public static bool ValenceClassesDistinct(int[] valences = null, int n = 120)
    {
        valences ??= new[] { 3, 4, 5, 6 };
        for (int i = 0; i < valences.Length; i++)
            for (int j = i + 1; j < valences.Length; j++)
                if (ValenceClassDistance(valences[i], valences[j], n) <= 0.10)
                    return false;
        return true;
    }

    // ── 2. Tetrahedral structures (K4 cliques) ────────────────────────────────────

    /// <summary>Count of K₄ cliques (tetrahedra) in an adjacency — local 3D volume cells.</summary>
    public static int TetrahedronCount(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        int count = 0;
        for (int a = 0; a < n; a++)
            for (int b = a + 1; b < n; b++)
            {
                if (adjacency[a, b] == 0.0) continue;
                for (int c = b + 1; c < n; c++)
                {
                    if (adjacency[a, c] == 0.0 || adjacency[b, c] == 0.0) continue;
                    for (int d = c + 1; d < n; d++)
                        if (adjacency[a, d] != 0.0 && adjacency[b, d] != 0.0 && adjacency[c, d] != 0.0)
                            count++;
                }
            }
        return count;
    }

    /// <summary>Mean tetrahedra per node (local 3D volume geometry).</summary>
    public static double MeanTetrahedraPerNode(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        return n > 0 ? (double)TetrahedronCount(adjacency) / n : 0.0;
    }

    /// <summary>Does a valence class host TETRAHEDRAL (3D volume) structure? (≥ 1 K₄ per node).</summary>
    public static bool HasTetrahedralStructure(double[,] adjacency)
        => MeanTetrahedraPerNode(adjacency) >= 1.0;

    // ── 3. Local volume geometry ──────────────────────────────────────────────────

    /// <summary>
    /// A genuine 3D ε-threshold graph: points on a 3D lattice, connected within Euclidean distance ε. This is
    /// the native 3D-connectivity network (in contrast to the 1+1D causal grid) — the home of tetrahedral
    /// volume structure.
    /// </summary>
    public static double[,] ThresholdGraph3D(int side = 6, double eps = 0.95)
    {
        var pts = new (double x, double y, double z)[side * side * side];
        int idx = 0;
        for (int i = 0; i < side; i++)
            for (int j = 0; j < side; j++)
                for (int k = 0; k < side; k++)
                    pts[idx++] = (-1.0 + 2.0 * i / (side - 1), -1.0 + 2.0 * j / (side - 1), -1.0 + 2.0 * k / (side - 1));
        int n = pts.Length;
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double dx = pts[i].x - pts[j].x, dy = pts[i].y - pts[j].y, dz = pts[i].z - pts[j].z;
                if (Math.Sqrt(dx * dx + dy * dy + dz * dz) < eps)
                {
                    a[i, j] = 1.0;
                    a[j, i] = 1.0;
                }
            }
        return a;
    }

    /// <summary>
    /// Local volume geometry: tetrahedra per node. The causal grid (1+1D) has none; the genuine 3D threshold
    /// graph has dense tetrahedral volume — 3D connectivity is what generates local volume structure.
    /// </summary>
    public static double LocalVolumePerNode(double[,] adjacency)
        => MeanTetrahedraPerNode(adjacency);

    /// <summary>Does the network host 3D volume structure (≥ 1 tetrahedron per node)?</summary>
    public static bool HasLocalVolume(double[,] adjacency)
        => LocalVolumePerNode(adjacency) >= 1.0;

    /// <summary>3D connectivity generates volume structure; 1+1D connectivity does not.</summary>
    public static bool VolumeStructureIs3D()
    {
        double gridVol = LocalVolumePerNode(SpectrumRobustness.LinkAdjacency(CausalSet.BuildGrid(6, 6)));
        double th3dVol = LocalVolumePerNode(ThresholdGraph3D());
        return gridVol == 0.0 && th3dVol >= 1.0;
    }

    // ── 4. Connectivity degeneracies ──────────────────────────────────────────────

    /// <summary>
    /// Connectivity degeneracy: number of DISTINCT Laplacian eigenvalues of a valence class (multiplicity of
    /// the spectrum). Highly symmetric connectivity ⇒ few distinct eigenvalues (degenerate classes).
    /// </summary>
    public static int DistinctEigenvalues(double[,] adjacency, double tol = 1e-8)
    {
        double[] ev = SpectralCurvature.Eigenvalues(SpectrumRobustness.LaplacianOf(adjacency));
        var sorted = ev.OrderBy(x => x).ToArray();
        int distinct = 1;
        for (int i = 1; i < sorted.Length; i++)
            if (Math.Abs(sorted[i] - sorted[i - 1]) > tol) distinct++;
        return distinct;
    }

    /// <summary>Degeneracy ratio = distinct eigenvalues / N (smaller = more degenerate connectivity).</summary>
    public static double DegeneracyRatio(int valence, int n = 120)
    {
        double[,] a = ValenceGraph(n, valence);
        return (double)DistinctEigenvalues(a) / n;
    }

    /// <summary>Are valence classes DEGENERATE (distinct eigenvalues far fewer than N)?</summary>
    public static bool ValenceClassesDegenerate(int valence, int n = 120)
        => DegeneracyRatio(valence, n) < 0.5;

    // ── 5. Family/color analogs ───────────────────────────────────────────────────

    /// <summary>Number of DISTINCT spectral valence classes in {3,4,5,6}.</summary>
    public static int DistinctConnectivityClassCount(int[] valences = null, double ksThreshold = 0.10)
    {
        valences ??= new[] { 3, 4, 5, 6 };
        // cluster valences by spectral-shape KS distance
        var labels = new int[valences.Length];
        Array.Fill(labels, -1);
        int next = 0;
        for (int i = 0; i < valences.Length; i++)
        {
            if (labels[i] != -1) continue;
            labels[i] = next;
            for (int j = 0; j < valences.Length; j++)
                if (labels[j] == -1 && ValenceClassDistance(valences[i], valences[j]) < ksThreshold)
                    labels[j] = next;
            next++;
        }
        return next;
    }

    /// <summary>SM family/color count (QG79/QG80): 3.</summary>
    public static int SmFamilyColorCount() => 3;

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO RELATION             — valence/connectivity generate no discrete classes (all valences collapse);
    ///   CONNECTIVITY CLASS ORIGIN — connectivity determines the SPECIFIC family/color counts (the discrete
    ///                               connectivity class count = 3 uniquely);
    ///   PARTIAL RELATION        — local 3D connectivity generates REAL discrete classes (distinct spectral
    ///                             classes per valence, tetrahedral/volume structure, degeneracies) but the
    ///                             class count tracks valence/size and does not uniquely determine the SM
    ///                             counts (the concrete case).
    /// </summary>
    public static string Classify()
    {
        if (!ValenceClassesDistinct()) return "NO RELATION";

        int classCount = DistinctConnectivityClassCount();
        if (classCount == SmFamilyColorCount()) return "CONNECTIVITY CLASS ORIGIN";

        return "PARTIAL RELATION";
    }
}
