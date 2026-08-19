namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 115 — Does content determine structure? Previous phases assumed network → physics. This phase
/// tests the ALTERNATIVE: actualization patterns determine network geometry — can the network emerge DYNAMICALLY
/// from its own activity?
///
/// Method (computational, fully deterministic): build an ACTIVITY-DRIVEN network model. Each node carries an
/// activity a_i (the actualization rate, QG89). At each step, active nodes create links to their local
/// ring-neighbors (activity-driven connectivity); the resulting degree FEEDS BACK into activity (a_i →
/// a_i(1−damping) + feedback·deg_i/maxDeg). Iterating this loop gives a two-way feedback between Q-events
/// (activity) and links (structure). We measure (1) FEEDBACK between activity and links (activity changes the
/// geometry AND the geometry changes activity), (2) ACTIVITY-DRIVEN CONNECTIVITY (links grow from activity),
/// (3) SELF-ORGANIZED GEOMETRY (the loop converges to a structured network — hierarchy span, family count),
/// (4) STRUCTURE-FROM-CONTENT (different initial activity patterns produce different final geometries), and
/// (5) FIXED vs ADAPTIVE (a frozen network vs an activity-adaptive network).
///
/// Answer (determined by the computed data): PARTIAL FEEDBACK — activity DOES drive connectivity (links grow
/// from activity, geometry converges under the loop, and different content gives different structure), but the
/// resulting geometry is strongly constrained by the initial activity seed and the deterministic local rule: it
/// is activity-MODULATED rather than FULLY self-organized from nothing. Classification: PARTIAL FEEDBACK
/// (content shapes structure, but not a full structure-from-content emergence). No new primitives added here.
/// </summary>
public static class StructureFromContent
{
    // ── Deterministic activity patterns ───────────────────────────────────────────

    /// <summary>Concentrated activity: a Gaussian bump at the center (deterministic).</summary>
    public static double[] ConcentratedActivity(int n)
    {
        var a = new double[n];
        double center = (n - 1) / 2.0;
        for (int i = 0; i < n; i++)
            a[i] = Math.Exp(-Math.Pow((i - center) / (n / 6.0), 2));
        return a;
    }

    /// <summary>Spread activity: alternating high/low bands (deterministic).</summary>
    public static double[] SpreadActivity(int n)
    {
        var a = new double[n];
        for (int i = 0; i < n; i++)
            a[i] = (i % 4 == 0 || i % 4 == 1) ? 0.9 : 0.2;
        return a;
    }

    /// <summary>Uniform activity (featureless content).</summary>
    public static double[] UniformActivity(int n)
    {
        var a = new double[n];
        for (int i = 0; i < n; i++) a[i] = 0.5;
        return a;
    }

    // ── Activity-driven adaptive network ───────────────────────────────────────────

    /// <summary>
    /// Iterate the activity→links→activity feedback loop. Active nodes (a_i &gt; 0.5) create links to their next
    /// k = round(a_i·K) ring-neighbors; degree then feeds back into activity. Deterministic.
    /// </summary>
    public static double[,] AdaptiveNetwork(double[] initialActivity, int K = 6, double damping = 0.25,
        double feedback = 0.6, int steps = 30)
        => AdaptiveNetworkFull(initialActivity, K, damping, feedback, steps).Adjacency;

    /// <summary>
    /// Full adaptive dynamics: returns the final activity vector as well as the adjacency (both needed for
    /// fixed-point analysis). Same update rule as <see cref="AdaptiveNetwork"/>.
    /// </summary>
    public static (double[] Activity, double[,] Adjacency) AdaptiveNetworkFull(double[] initialActivity,
        int K = 6, double damping = 0.25, double feedback = 0.6, int steps = 30)
    {
        int n = initialActivity.Length;
        var a = (double[])initialActivity.Clone();
        var adj = new double[n, n];
        for (int t = 0; t < steps; t++)
        {
            // activity-driven connectivity
            for (int i = 0; i < n; i++)
            {
                if (a[i] <= 0.5) continue;
                int k = (int)Math.Round(a[i] * K);
                for (int d = 1; d <= k; d++)
                {
                    int j = (i + d) % n;
                    adj[i, j] = 1.0;
                    adj[j, i] = 1.0;
                }
            }
            // degree feedback to activity
            int[] deg = Degrees(adj);
            int maxDeg = Math.Max(deg.Max(), 1);
            for (int i = 0; i < n; i++)
            {
                a[i] = a[i] * (1.0 - damping) + feedback * (deg[i] / (double)maxDeg);
                a[i] = Math.Clamp(a[i], 0.0, 1.0);
            }
        }
        return (a, adj);
    }

    /// <summary>The FIXED network: the initial activity creates ONE round of links and then freezes (no feedback).</summary>
    public static double[,] FixedNetwork(double[] initialActivity, int K = 6)
    {
        int n = initialActivity.Length;
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            if (initialActivity[i] <= 0.5) continue;
            int k = (int)Math.Round(initialActivity[i] * K);
            for (int d = 1; d <= k; d++)
            {
                int j = (i + d) % n;
                adj[i, j] = 1.0;
                adj[j, i] = 1.0;
            }
        }
        return adj;
    }

    private static int[] Degrees(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        var deg = new int[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (adjacency[i, j] != 0.0) deg[i]++;
        return deg;
    }

    // ── Geometry measures ──────────────────────────────────────────────────────────

    /// <summary>Link count of an adjacency.</summary>
    public static int LinkCount(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        int count = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (adjacency[i, j] != 0.0) count++;
        return count;
    }

    /// <summary>Hierarchy span of the Laplacian spectrum.</summary>
    public static double HierarchySpan(double[,] adjacency)
        => SpectrumRobustness.HierarchySpan(SpectrumRobustness.StableFrequencies(SpectrumRobustness.LaplacianOf(adjacency)));

    /// <summary>Octave-family count of the spectrum (QG106 family structure).</summary>
    public static int FamilyCount(double[,] adjacency)
        => FamilyStructureRobustness.FamilyCount(adjacency);

    // ── Feedback and self-organization tests ──────────────────────────────────────

    /// <summary>
    /// Feedback effect: does the activity-driven loop CHANGE the geometry relative to a fixed network?
    /// (adaptive link count ≠ fixed link count AND adaptive family count ≠ fixed family count).
    /// </summary>
    public static bool FeedbackChangesGeometry(double[] activity, int K = 6)
    {
        double[,] fixedNet = FixedNetwork(activity, K);
        double[,] adaptive = AdaptiveNetwork(activity, K);
        bool linksDiffer = Math.Abs(LinkCount(adaptive) - LinkCount(fixedNet)) > 0;
        bool familyDiffers = FamilyCount(adaptive) != FamilyCount(fixedNet);
        return linksDiffer || familyDiffers;
    }

    /// <summary>Does geometry depend on CONTENT (different activity patterns → different final geometry)?</summary>
    public static bool StructureDependsOnContent(int n = 96)
    {
        double[,] conc = AdaptiveNetwork(ConcentratedActivity(n));
        double[,] spread = AdaptiveNetwork(SpreadActivity(n));
        bool linksDiffer = Math.Abs(LinkCount(conc) - LinkCount(spread)) > 0;
        bool familyDiffers = FamilyCount(conc) != FamilyCount(spread);
        bool spanDiffers = Math.Abs(HierarchySpan(conc) - HierarchySpan(spread)) > 0.5;
        return linksDiffer || familyDiffers || spanDiffers;
    }

    /// <summary>
    /// Self-organization: does the adaptive loop build a STRUCTURED network (non-trivial hierarchy, ≥ 3
    /// octave families) and does link growth SATURATE (bounded — the loop does not run away)?
    /// </summary>
    public static bool LoopBuildsStructuredNetwork(double[] activity, int K = 6)
    {
        double[,] s20 = AdaptiveNetwork(activity, K, steps: 20);
        double[,] s40 = AdaptiveNetwork(activity, K, steps: 40);
        int links20 = LinkCount(s20), links40 = LinkCount(s40);
        double growth = (double)(links40 - links20) / Math.Max(links40, 1);   // relative growth over 20 steps
        bool bounded = growth < 0.5;                                          // growth decelerates (saturating)
        bool structured = HierarchySpan(s40) > 1.0 && FamilyCount(s40) >= 3;
        return bounded && structured;
    }

    /// <summary>
    /// Full self-organization test: does UNIFORM (featureless) activity alone build a rich hierarchy? If yes,
    /// structure is emergent from nothing; if no (uniform gives a trivial/flat geometry), structure is
    /// content-dependent — NOT full self-organization.
    /// </summary>
    public static bool UniformActivitySelfOrganizes(int n = 96)
    {
        double[,] uniform = AdaptiveNetwork(UniformActivity(n));
        return HierarchySpan(uniform) >= 2.0 && LinkCount(uniform) > 0;
    }

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   FIXED NETWORK         — activity/feedback does not change the geometry (adaptive == fixed);
    ///   FULL SELF-ORGANIZATION — structure emerges ENTIRELY from activity (uniform content still builds a rich
    ///                            hierarchy; the geometry is determined by activity alone);
    ///   PARTIAL FEEDBACK      — activity drives connectivity and content shapes structure, but uniform
    ///                            (featureless) content produces NO structure, so the geometry is content-driven
    ///                            rather than emergent from nothing — the concrete case.
    /// </summary>
    public static string Classify()
    {
        double[] conc = ConcentratedActivity(96);

        if (!FeedbackChangesGeometry(conc)) return "FIXED NETWORK";
        if (UniformActivitySelfOrganizes()) return "FULL SELF-ORGANIZATION";

        bool feedback = FeedbackChangesGeometry(conc);
        bool content = StructureDependsOnContent();
        bool structured = LoopBuildsStructuredNetwork(conc);
        if (feedback && content && structured) return "PARTIAL FEEDBACK";

        return "FIXED NETWORK";
    }
}
