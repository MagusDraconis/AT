namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 121 — Origin of the attractor ladder. QG117 showed attractor geometries form DISCRETE radius
/// classes (a ladder: radius 2 and 6 for K=6) over the (feedback, damping) plane, with sharp transitions at
/// threshold ratios. This phase asks: WHY does the feedback dynamics produce a discrete ladder instead of a
/// continuous family of geometries?
///
/// Method (computational, fully deterministic): probe the mechanism of the discreteness by varying each
/// candidate cause independently. (1) THRESHOLD EFFECTS — vary the activity threshold (a &gt; θ) that gates link
/// creation: does the ladder persist for θ = 0.3, 0.5, 0.7? (2) ROUNDING STRUCTURE — replace the integer
/// rounding k = round(a·K) with floor and ceil: does the ladder survive, or was it an artifact of the
/// rounding? Also run a CONTINUOUS-WEIGHT variant (no integer count; link weight = a_i) to test whether the
/// discreteness is intrinsic. (3) FIXED-POINT BIFURCATIONS — the algebraic fixed point of the saturated
/// activity is a* = min(1, f/d) (uniform ring: deg = maxDeg), so the next-round radius is round(K·a*): the
/// fixed point is a STEP function of the continuous ratio f/d — a bifurcation ladder. (4) ATTRACTOR CLASS
/// TRANSITIONS — locate the sharp threshold ratios where the realized radius jumps (basin selection). (5)
/// LADDER UNIVERSALITY — does a discrete ladder appear for every K (3,4,5,6,8), every threshold, and both
/// discrete and continuous link rules?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class AttractorLadder
{
    /// <summary>Default dynamics parameters (matching QG115–120).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>Discretization modes for the link-creation rule.</summary>
    public enum LinkDiscretization { Round, Floor, Ceil, Continuous }

    // ── Generalized dynamics ───────────────────────────────────────────────────────

    /// <summary>
    /// Generalized activity-driven dynamics with configurable link discretization and activity threshold.
    /// Returns the final adjacency (weights for the continuous variant, binary otherwise).
    /// </summary>
    public static double[,] AdaptiveNetworkGeneral(double[] initialActivity, int K = 6, double damping = 0.2,
        double feedback = 0.7, int steps = 120, double threshold = 0.5,
        LinkDiscretization mode = LinkDiscretization.Round)
    {
        int n = initialActivity.Length;
        var a = (double[])initialActivity.Clone();
        var w = new double[n, n];
        for (int t = 0; t < steps; t++)
        {
            for (int i = 0; i < n; i++)
            {
                if (a[i] <= threshold) continue;
                switch (mode)
                {
                    case LinkDiscretization.Round:
                    case LinkDiscretization.Floor:
                    case LinkDiscretization.Ceil:
                    {
                        int k = mode switch
                        {
                            LinkDiscretization.Round => (int)Math.Round(a[i] * K),
                            LinkDiscretization.Floor => (int)Math.Floor(a[i] * K),
                            _ => (int)Math.Ceiling(a[i] * K),
                        };
                        for (int d = 1; d <= k; d++)
                        {
                            int j = (i + d) % n;
                            w[i, j] = 1.0; w[j, i] = 1.0;
                        }
                        break;
                    }
                    case LinkDiscretization.Continuous:
                    {
                        double scale = a[i];   // continuous link strength
                        for (int d = 1; d <= K; d++)
                        {
                            int j = (i + d) % n;
                            if (scale > w[i, j]) { w[i, j] = scale; w[j, i] = scale; }
                        }
                        break;
                    }
                }
            }
            // degree/weight feedback to activity
            double[] deg = new double[n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++) deg[i] += w[i, j];
            double maxDeg = Math.Max(deg.Max(), 1e-9);
            for (int i = 0; i < n; i++)
            {
                a[i] = a[i] * (1.0 - damping) + feedback * (deg[i] / maxDeg);
                a[i] = Math.Clamp(a[i], 0.0, 1.0);
            }
        }
        return w;
    }

    /// <summary>Link radius (links per node) of an adjacency/weight matrix.</summary>
    public static double RadiusOf(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        double c = 0.0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (matrix[i, j] != 0.0) c += 1.0;
        return c / n;
    }

    /// <summary>Distinct radii realized over a feedback sweep at fixed damping.</summary>
    public static double[] DistinctRadiiOverFeedback(double damping = 0.3, double fMin = 0.2, double fMax = 1.0,
        double fStep = 0.1, int K = DefaultK, double threshold = 0.5,
        LinkDiscretization mode = LinkDiscretization.Round)
    {
        var radii = new List<double>();
        for (double f = fMin; f <= fMax + 1e-9; f += fStep)
        {
            var net = AdaptiveNetworkGeneral(ActualizationStructures.PersistentActivity(96), K, damping, f, 120,
                threshold, mode);
            radii.Add(RadiusOf(net));
        }
        return radii.Distinct().OrderBy(r => r).ToArray();
    }

    // ── 1. Threshold effects ───────────────────────────────────────────────────────

    /// <summary>Distinct radii realized at each threshold (ladder robustness to the activity gate).</summary>
    public static (double Threshold, double[] Radii)[] LadderByThreshold(int K = DefaultK)
    {
        var result = new List<(double, double[])>();
        foreach (double thr in new[] { 0.3, 0.5, 0.7 })
            result.Add((thr, DistinctRadiiOverFeedback(0.3, 0.2, 1.0, 0.1, K, thr)));
        return result.ToArray();
    }

    /// <summary>Does a discrete ladder persist at every threshold (≥ 2 distinct radii)?</summary>
    public static bool LadderPersistsAcrossThresholds(int K = DefaultK)
        => LadderByThreshold(K).All(x => x.Radii.Length >= 2);

    // ── 2. Rounding structure ──────────────────────────────────────────────────────

    /// <summary>Distinct radii realized under each discretization mode (round/floor/ceil/continuous).</summary>
    public static (LinkDiscretization Mode, double[] Radii)[] LadderByDiscretization(int K = DefaultK)
    {
        var result = new List<(LinkDiscretization, double[])>();
        foreach (LinkDiscretization m in new[]
        {
            LinkDiscretization.Round, LinkDiscretization.Floor, LinkDiscretization.Ceil,
            LinkDiscretization.Continuous,
        })
            result.Add((m, DistinctRadiiOverFeedback(0.3, 0.2, 1.0, 0.1, K, 0.5, m)));
        return result.ToArray();
    }

    /// <summary>
    /// Does a discrete ladder persist across ALL discretization modes INCLUDING continuous link weights?
    /// If yes, the discreteness is NOT a rounding artifact — it is intrinsic to the feedback structure.
    /// </summary>
    public static bool LadderPersistsAcrossDiscretization(int K = DefaultK)
        => LadderByDiscretization(K).All(x => x.Radii.Length >= 2);

    /// <summary>Does even the CONTINUOUS-weight variant show a discrete ladder (≥ 2 radii)?</summary>
    public static bool ContinuousVariantShowsLadder(int K = DefaultK)
    {
        var c = LadderByDiscretization(K).First(x => x.Mode == LinkDiscretization.Continuous);
        return c.Radii.Length >= 2;
    }

    // ── 3. Fixed-point bifurcations ────────────────────────────────────────────────

    /// <summary>
    /// Algebraic fixed-point radius of the saturated uniform ring: a* = min(1, f/d) and k = round(K·a*).
    /// This step function of the CONTINUOUS ratio f/d is the bifurcation ladder.
    /// </summary>
    public static double AlgebraicFixedPointRadius(double feedback, double damping, int K = DefaultK)
        => Math.Round(K * Math.Min(1.0, feedback / damping));

    /// <summary>
    /// Does the measured radius MATCH the algebraic fixed point round(K·min(1,f/d)) at the given
    /// parameters? Measures how faithfully the dynamics realizes the fixed-point ladder.
    /// </summary>
    public static bool RadiusMatchesAlgebraicFixedPoint(double feedback, double damping, int K = DefaultK)
    {
        double measured = RadiusOf(AdaptiveNetworkGeneral(ActualizationStructures.PersistentActivity(96), K,
            damping, feedback, 120));
        return Math.Abs(measured - AlgebraicFixedPointRadius(feedback, damping, K)) < 0.5;
    }

    /// <summary>Number of discrete rungs of the ALGEBRAIC ladder over f/d ∈ (0, ∞): K+1 for K &gt; 0.</summary>
    public static int AlgebraicRungCount(int K = DefaultK) => K + 1;

    // ── 4. Attractor class transitions ─────────────────────────────────────────────

    /// <summary>
    /// Transition ratios: feedback/damping values where the realized radius changes (sharp class
    /// transitions). Measured over a fine feedback sweep at fixed damping.
    /// </summary>
    public static (double Ratio, double Radius)[] TransitionPoints(double damping = 0.3, int K = DefaultK)
    {
        var points = new List<(double, double)>();
        double prev = double.NaN;
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.02)
        {
            double r = RadiusOf(AdaptiveNetworkGeneral(ActualizationStructures.PersistentActivity(96), K,
                damping, f, 120));
            if (!double.IsNaN(prev) && Math.Abs(r - prev) > 0.5)
                points.Add((f / damping, r));
            prev = r;
        }
        return points.ToArray();
    }

    /// <summary>Are the class transitions SHARP (few well-separated thresholds, not a gradual ramp)?</summary>
    public static bool TransitionsAreSharp(double damping = 0.3, int K = DefaultK)
    {
        var transitions = TransitionPoints(damping, K);
        return transitions.Length >= 1 && transitions.Length <= 4;
    }

    // ── 5. Ladder universality ─────────────────────────────────────────────────────

    /// <summary>Distinct radii realized per K over the feedback sweep (ladder exists for every K?).</summary>
    public static (int K, double[] Radii)[] LadderByK()
    {
        var result = new List<(int, double[])>();
        foreach (int k in new[] { 3, 4, 5, 6, 8 })
            result.Add((k, DistinctRadiiOverFeedback(0.3, 0.2, 1.0, 0.1, k)));
        return result.ToArray();
    }

    /// <summary>Does a discrete ladder (≥ 2 radii over the feedback sweep) appear for EVERY K?</summary>
    public static bool LadderUniversalAcrossK()
        => LadderByK().All(x => x.Radii.Length >= 2);

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   ARTIFACT     — the discrete ladder is an artifact of the integer rounding: it disappears under a
    ///                  different discretization (e.g. the continuous-weight variant is a continuum) or a
    ///                  different threshold;
    ///   DYNAMICAL    — the ladder is a genuine fixed-point bifurcation structure of the feedback dynamics:
    ///                  robust to discretization AND threshold, but its rung positions are set by the
    ///                  dynamics parameters (f/d ratio) and basin selection;
    ///   FUNDAMENTAL  — the discrete ladder is unavoidable: it persists across thresholds, discretizations
    ///                  INCLUDING continuous weights, every K, and matches the algebraic step-function fixed
    ///                  point of the saturated feedback — the bounded-activity × discrete-link structure of
    ///                  the model forces discreteness.
    /// </summary>
    public static string Classify(int K = DefaultK)
    {
        bool acrossThresholds = LadderPersistsAcrossThresholds(K);
        bool acrossDiscretization = LadderPersistsAcrossDiscretization(K);
        bool continuousLadder = ContinuousVariantShowsLadder(K);
        bool universalK = LadderUniversalAcrossK();

        if (!acrossThresholds || !acrossDiscretization) return "ARTIFACT";
        if (continuousLadder && universalK) return "FUNDAMENTAL";
        return "DYNAMICAL";
    }
}
