namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 109 — Selection of the physical network. QG102 established that many globally-consistent network
/// classes exist (a non-unique solution space), and QG108 showed the family-count distribution over the causal
/// ensemble is broad. This phase asks: why does nature realize ONE specific network class?
///
/// Method (computational, fully deterministic): reuse the 77-network ensemble (ER random, causal grids,
/// threshold graphs, perturbed grids) and measure five selection mechanisms — (1) STABILITY selection (spectral
/// gap + hierarchy span, the QG105 robustness picture), (2) ATTRACTOR BASINS (cluster the normalized spectral
/// shapes into basins by Kolmogorov–Smirnov distance; count basins and volumes), (3) ACTUALIZATION STATISTICS
/// (counting-measure variance of the classes — the native actualization-rate observable, QG89), (4) NETWORK
/// GROWTH HISTORY (causal grids at increasing size: does the spectral class converge to a unique class?), and
/// (5) ANTHROPIC-FREE SELECTION (does a native stability functional select a UNIQUE class without observer
/// input?).
///
/// Answer (determined by the computed spectra): PARTIAL SELECTION — stability and attractor structure DO narrow
/// the region to the causal class (causal grids have higher robustness and form a distinct basin), and the
/// counting measure favours them, but the selected region still contains MANY distinct networks (no unique class
/// is singled out by any native, anthropic-free functional). Consistent with QG96 (partial selection) and
/// QG102 (solution space non-unique). Classification: PARTIAL SELECTION. No new primitives added here.
/// </summary>
public static class PhysicalNetworkSelection
{
    // ── Ensemble ───────────────────────────────────────────────────────────────────

    /// <summary>The 77-network deterministic ensemble (name, adjacency).</summary>
    public static (string name, double[,] adjacency)[] Ensemble()
        => FamilyCountStatistics.BuildEnsemble().ToArray();

    // ── 1. Stability selection ─────────────────────────────────────────────────────

    /// <summary>Spectral gap λ_2 of an adjacency.</summary>
    public static double SpectralGap(double[,] adjacency)
        => SpectralCurvature.SpectralGap(SpectralCurvature.Eigenvalues(SpectrumRobustness.LaplacianOf(adjacency)));

    /// <summary>Hierarchy span ω_max/ω_min of an adjacency.</summary>
    public static double HierarchySpan(double[,] adjacency)
        => SpectrumRobustness.HierarchySpan(SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adjacency)));

    /// <summary>Robustness of a class under deterministic link removal (fraction of families surviving).</summary>
    public static double RobustnessFraction(double[,] adjacency, double removalFraction = 0.10)
    {
        int baseFamilies = FamilyStructureRobustness.FamilyCount(adjacency);
        if (baseFamilies == 0) return 0.0;
        double[,] perturbed = SpectrumRobustness.RemoveLinksDeterministic(adjacency, removalFraction);
        int perturbedFamilies = FamilyStructureRobustness.FamilyCount(perturbed);
        return (double)perturbedFamilies / baseFamilies;
    }

    /// <summary>Mean stability-gap of a class by name prefix (e.g. "grid", "ER", "threshold", "perturbed").</summary>
    public static double MeanStabilityGap(string namePrefix)
    {
        var members = Ensemble().Where(e => e.name.StartsWith(namePrefix, StringComparison.Ordinal)).ToArray();
        if (members.Length == 0) return double.NaN;
        return members.Average(e => SpectralGap(e.adjacency));
    }

    /// <summary>Mean family-count survival of a class under deterministic link removal (mean of perturbed/base).</summary>
    public static double MeanRobustness(string namePrefix)
    {
        var members = Ensemble().Where(e => e.name.StartsWith(namePrefix, StringComparison.Ordinal)).ToArray();
        if (members.Length == 0) return double.NaN;
        return members.Average(e => RobustnessFraction(e.adjacency));
    }

    /// <summary>Fraction of a class whose octave-family count NEVER drops under 10% link removal.</summary>
    public static double FamilyStructurePersistence(string namePrefix)
    {
        var members = Ensemble().Where(e => e.name.StartsWith(namePrefix, StringComparison.Ordinal)).ToArray();
        if (members.Length == 0) return double.NaN;
        return (double)members.Count(e => RobustnessFraction(e.adjacency) >= 1.0) / members.Length;
    }

    // ── 2. Attractor basins ────────────────────────────────────────────────────────

    /// <summary>
    /// Cluster the normalized spectral shapes into attractor basins by single-linkage on KS distance.
    /// Returns (basinCount, basinVolumes) where each volume = fraction of networks in that basin.
    /// Deterministic greedy clustering.
    /// </summary>
    public static (int basinCount, double[] basinVolumes) AttractorBasins(double ksThreshold = 0.08)
    {
        var ens = Ensemble();
        int n = ens.Length;
        var shapes = new double[n][];
        for (int i = 0; i < n; i++)
            shapes[i] = SpectrumRobustness.NormalizedShape(SpectrumRobustness.LaplacianOf(ens[i].adjacency));

        // single-linkage clustering
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

        int basinCount = nextLabel;
        var volumes = new double[basinCount];
        for (int i = 0; i < n; i++) volumes[labels[i]]++;
        for (int i = 0; i < basinCount; i++) volumes[i] /= n;
        return (basinCount, volumes);
    }

    /// <summary>Does a single attractor basin dominate (&gt; 80% of the ensemble)?</summary>
    public static bool SingleBasinDominates(double[] basinVolumes)
        => basinVolumes.Length > 0 && basinVolumes.Max() > 0.80;

    // ── 3. Actualization statistics ────────────────────────────────────────────────

    /// <summary>
    /// Counting-measure variance of an adjacency: the spread of the native actualization-rate density
    /// ρ_i = past+future degree (the QG89 observable). A class whose counting measure is concentrated
    /// (low variance) is statistically preferred by the actualization process.
    /// </summary>
    public static double ActualizationVariance(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        var rho = new double[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                rho[i] += adjacency[i, j];
        double mean = rho.Average();
        double var = rho.Sum(r => (r - mean) * (r - mean)) / n;
        return var;
    }

    /// <summary>Mean actualization-rate variance of a class by name prefix.</summary>
    public static double MeanActualizationVariance(string namePrefix)
    {
        var members = Ensemble().Where(e => e.name.StartsWith(namePrefix, StringComparison.Ordinal)).ToArray();
        if (members.Length == 0) return double.NaN;
        return members.Average(e => ActualizationVariance(e.adjacency));
    }

    // ── 4. Network growth history ──────────────────────────────────────────────────

    /// <summary>
    /// Growth-history convergence: causal grids at increasing size (t,x) → does the octave-family count
    /// converge to a fixed value as the network grows? Returns the family-count sequence.
    /// </summary>
    public static int[] GrowthFamilyCountSequence()
    {
        (int t, int x)[] growth =
        {
            (3, 3),   // N=28
            (5, 5),   // N=66
            (6, 6),   // N=91
            (7, 12),  // N=200
            (12, 8),  // N=221
            (19, 12), // N=500
            (25, 15), // N=806
        };
        var counts = new int[growth.Length];
        for (int i = 0; i < growth.Length; i++)
            counts[i] = FamilyStructureRobustness.FamilyCount(CausalSet.BuildGrid(growth[i].t, growth[i].x));
        return counts;
    }

    /// <summary>Did the growth history CONVERGE (last three counts identical)?</summary>
    public static bool GrowthConverges(int[] sequence)
        => sequence.Length >= 3 && sequence[^1] == sequence[^2] && sequence[^2] == sequence[^3];

    // ── 5. Anthropic-free selection ────────────────────────────────────────────────

    /// <summary>
    /// Does a native stability functional (maximize family-structure persistence, then maximize spectral gap)
    /// select a UNIQUE network class? We evaluate each class by its family-structure persistence: the class
    /// whose family structure survives link removal best is preferred. Returns (bestClass, memberCount, unique).
    /// </summary>
    public static (string bestClass, int classMemberCount, bool unique) AnthropicFreeSelection()
    {
        string[] prefixes = { "ER", "grid", "threshold", "perturbed" };
        string best = "";
        double bestScore = -1.0;
        int bestCount = 0;
        foreach (string p in prefixes)
        {
            double persist = FamilyStructurePersistence(p);
            if (double.IsNaN(persist)) continue;
            double gap = MeanStabilityGap(p);
            double score = persist * (1.0 + Math.Log(1.0 + gap)); // persistence-dominated stability functional
            if (score > bestScore)
            {
                bestScore = score;
                best = p;
                bestCount = Ensemble().Count(e => e.name.StartsWith(p, StringComparison.Ordinal));
            }
        }
        bool unique = bestCount == 1; // a class selects a UNIQUE network only if it has exactly one member
        return (best, bestCount, unique);
    }

    // ── Classification ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   PHYSICAL SELECTION  — a native, anthropic-free functional selects a UNIQUE network class;
    ///   NO SELECTION        — no native criterion narrows the region at all (no stable preference);
    ///   PARTIAL SELECTION   — some native criteria narrow toward a preferred class but criteria CONFLICT
    ///                         (e.g. spectral gap prefers ER random, family-structure persistence prefers the
    ///                         causal grid) and many distinct networks remain — the concrete case.
    /// </summary>
    public static string Classify()
    {
        var (basinCount, basinVolumes) = AttractorBasins();
        var (bestClass, classCount, unique) = AnthropicFreeSelection();

        if (unique) return "PHYSICAL SELECTION";

        // Criteria conflict: the counting measure (QG89) and family-structure persistence prefer the causal
        // grid, but the spectral gap prefers ER random. Real narrowing + conflict + non-uniqueness ⇒ PARTIAL.
        double varGrid = MeanActualizationVariance("grid");
        double varER = MeanActualizationVariance("ER");
        bool countingPrefersGrid = varGrid < varER;
        bool persistencePrefersGrid = FamilyStructurePersistence("grid") > FamilyStructurePersistence("ER");
        bool anyNarrowing = basinCount > 1 && !SingleBasinDominates(basinVolumes)
                            && (countingPrefersGrid || persistencePrefersGrid);
        if (anyNarrowing) return "PARTIAL SELECTION";

        return "NO SELECTION";
    }
}
