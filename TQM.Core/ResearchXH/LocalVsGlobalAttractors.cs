namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 119 — Local vs Global Attractor Classes. QG118 showed the octave-family count of each
/// attractor geometry class SCALES with the network (radius-2 class: 3→4→5 families as N=48→96→192). This
/// phase asks: do LOCAL observers — who can only sample a finite subregion (horizon) of the network — see a
/// subset of the network's attractor classes?
///
/// Method (computational, fully deterministic): (1) LOCAL ATTRACTOR ACCESSIBILITY — a local observer with a
/// horizon of n_local events runs the QG115/116 dynamics within a ring of that size; which geometry classes
/// (saturated radii) are reachable from the full parameter plane? (2) GLOBAL ATTRACTOR SPECTRUM — the full
/// class set realized over the parameter plane at global sizes N=48/96/192. (3) HIDDEN STABLE CLASSES — classes
/// in the global spectrum that a local horizon cannot reach. (4) SUPPRESSION OF HIGHER CLASSES — whether the
/// locally observable family count saturates below the total family count as the global network grows (the
/// QG118 scaling suppressed beyond the horizon). (5) OBSERVABLE vs TOTAL FAMILIES — family count of a FIXED
/// local window embedded in growing networks vs the whole network.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class LocalVsGlobalAttractors
{
    /// <summary>Default dynamics parameters (matching QG115–118).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>Global sizes used for the observable-vs-total comparison.</summary>
    public static readonly int[] GlobalSizes = { 48, 96, 192 };

    /// <summary>Local horizons sampled.</summary>
    public static readonly int[] LocalHorizons = { 16, 24, 32 };

    // ── 2. Global attractor spectrum ───────────────────────────────────────────────

    /// <summary>Rung tolerance for class comparison (finite-size distortion of the radius ladder).</summary>
    public const double RadiusTolerance = 0.5;

    /// <summary>Distinct saturated radii (geometry classes) reachable over the parameter plane at size N,
    /// grouped into rungs with tolerance.</summary>
    public static double[] GlobalRadii(int n = 96, int K = DefaultK)
        => DistinctRungs(AttractorParameterOrigin.ParameterPlane(n, K).Select(p => p.Radius));

    // ── Helpers ───────────────────────────────────────────────────────────────────

    /// <summary>Group radii into discrete rungs with tolerance (finite-size distortion of the radius ladder).</summary>
    private static double[] DistinctRungs(IEnumerable<double> radii)
    {
        var sorted = radii.Distinct().OrderBy(r => r).ToList();
        var rungs = new List<double>();
        foreach (double r in sorted)
        {
            if (!rungs.Any(existing => Math.Abs(existing - r) < RadiusTolerance))
                rungs.Add(r);
        }
        return rungs.ToArray();
    }

    /// <summary>Octave-family count of the whole network at size N.</summary>
    public static int TotalFamilies(int n, double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(n), K,
            damping, feedback, 120);
        return StructureFromContent.FamilyCount(net);
    }

    // ── 3. Local accessibility / hidden classes ────────────────────────────────────

    /// <summary>
    /// Local reachable radii: distinct geometry classes a local observer with horizon n_local can reach by
    /// running the dynamics (full parameter plane) on a ring of that size.
    /// </summary>
    public static double[] LocalReachableRadii(int nLocal, int K = DefaultK)
        => DistinctRungs(AttractorParameterOrigin.ParameterPlane(nLocal, K).Select(p => p.Radius));

    /// <summary>Classes in the global spectrum that a local horizon CANNOT reach (hidden stable classes).</summary>
    public static double[] HiddenClasses(int nLocal, int nGlobal = 96, int K = DefaultK)
    {
        var global = GlobalRadii(nGlobal, K);
        var local = LocalReachableRadii(nLocal, K);
        return global.Where(g => !local.Any(l => Math.Abs(l - g) < RadiusTolerance)).ToArray();
    }

    /// <summary>Are any global classes hidden from the local horizon?</summary>
    public static bool HasHiddenClasses(int nLocal = 24, int nGlobal = 96, int K = DefaultK)
        => HiddenClasses(nLocal, nGlobal, K).Length > 0;

    // ── Local window extraction ────────────────────────────────────────────────────

    /// <summary>
    /// Induced subgraph of a contiguous window [start, start+nLocal) of a converged network — the patch a
    /// local observer actually samples.
    /// </summary>
    public static double[,] LocalWindowPatch(double[,] fullNetwork, int start, int nLocal)
    {
        int n = fullNetwork.GetLength(0);
        var patch = new double[nLocal, nLocal];
        for (int i = 0; i < nLocal; i++)
            for (int j = 0; j < nLocal; j++)
                patch[i, j] = fullNetwork[(start + i) % n, (start + j) % n];
        return patch;
    }

    /// <summary>Octave-family count of a fixed local window embedded in a converged network of size N.</summary>
    public static int LocalWindowFamilies(int globalN, int nLocal, double feedback = 0.7,
        double damping = 0.3, int K = DefaultK, int start = 0)
    {
        var net = UniversalAttractor.ConvergedNetwork(ActualizationStructures.PersistentActivity(globalN), K,
            damping, feedback, 120);
        var patch = LocalWindowPatch(net, start, Math.Min(nLocal, globalN));
        return StructureFromContent.FamilyCount(patch);
    }

    // ── 5. Observable vs total families ────────────────────────────────────────────

    /// <summary>
    /// Observable vs total: for each global size, the total family count of the whole network vs the family
    /// count of a FIXED local window (horizon). Shows whether local observers saturate below the total.
    /// </summary>
    public static (int GlobalN, int TotalFamilies, int LocalFamilies)[] ObservableVsTotal(int nLocal = 24,
        double feedback = 0.7, double damping = 0.3, int K = DefaultK)
    {
        var result = new List<(int, int, int)>();
        foreach (int n in GlobalSizes)
        {
            int total = TotalFamilies(n, feedback, damping, K);
            int local = LocalWindowFamilies(n, nLocal, feedback, damping, K);
            result.Add((n, total, local));
        }
        return result.ToArray();
    }

    // ── 4. Suppression of higher classes ───────────────────────────────────────────

    /// <summary>
    /// Is the locally observable family count SUPPRESSED below the total (local families saturate while the
    /// global network keeps adding families)? True if the local window family count stays constant as the
    /// global family count grows.
    /// </summary>
    public static bool HigherFamiliesSuppressed(int nLocal = 24, int K = DefaultK)
    {
        var data = ObservableVsTotal(nLocal, K: K);
        if (data.Length < 2) return false;
        // total grows with global size
        bool totalGrows = data[^1].TotalFamilies > data[0].TotalFamilies;
        // local stays (nearly) constant
        bool localConstant = data.Max(d => d.LocalFamilies) == data.Min(d => d.LocalFamilies);
        return totalGrows && localConstant;
    }

    /// <summary>Local observers see a STRICT subset of the total family count (observable &lt; total at large N).</summary>
    public static bool LocalIsStrictSubset(int nLocal = 24, int K = DefaultK)
    {
        var data = ObservableVsTotal(nLocal, K: K);
        return data.Any(d => d.LocalFamilies < d.TotalFamilies);
    }

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   EXACT MATCH   — local observers see exactly the global class/family spectrum (local reachable
    ///                   radii == global radii AND local family counts == total for every size);
    ///   LOCAL SUBSET  — local observers reach a strict SUBSET: fewer families than total (higher families
    ///                   suppressed beyond the horizon) but no globally-stable class is missing entirely;
    ///   HIDDEN CLASSES — some globally-stable geometry classes are entirely INACCESSIBLE to the local
    ///                   horizon (HiddenClasses non-empty) — classes hidden, not merely reduced in count.
    /// </summary>
    public static string Classify(int nLocal = 24, int nGlobal = 96, int K = DefaultK)
    {
        bool hasHidden = HasHiddenClasses(nLocal, nGlobal, K);
        bool suppressed = HigherFamiliesSuppressed(nLocal, K);
        bool subset = LocalIsStrictSubset(nLocal, K);

        if (hasHidden) return "HIDDEN CLASSES";
        if (suppressed || subset) return "LOCAL SUBSET";
        return "EXACT MATCH";
    }
}
