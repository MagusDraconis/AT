namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 122 — Energy dependence of attractor classes. QG89 derived energy = actualization rate
/// (Q-event activity). QG117 showed the (feedback, damping) plane maps to a discrete ladder of attractor
/// geometry classes (radius ≤ K = 6 links/node). This phase asks: can HIGHER actualization-energy regimes
/// generate NEW attractor classes not accessible in the current parameter range?
///
/// Method (computational, fully deterministic): the QG115/116 dynamics clamps activity to a ceiling
/// (a ≤ ceiling, default 1.0), so the saturated activity fixed point a* = min(ceiling, f/d) bounds the link
/// radius k = round(a*·K) ≤ K. We (1) ENERGY SCALING — multiply the seed activity by an energy scale E and
/// record the attractor radius; (2) ACTUALIZATION-RATE REGIMES — raise the activity ceiling (the energy
/// regime) and re-sweep the parameter plane; (3) ATTRACTOR PHASE TRANSITIONS — count the distinct spectral
/// classes (KS single-linkage) realized at each ceiling; (4) FAMILY-COUNT EVOLUTION — octave-family count and
/// hierarchy span vs energy regime; (5) HIGH-ENERGY CLASSES — does the realized radius EXCEED the current
/// range cap (K), i.e. are there classes that only appear above the baseline energy regime?
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class EnergyDependentAttractors
{
    /// <summary>Default dynamics parameters (matching QG115–121).</summary>
    public const double DefaultDamping = 0.2;
    public const double DefaultFeedback = 0.7;
    public const int DefaultK = 6;

    /// <summary>Baseline activity ceiling (current parameter range).</summary>
    public const double BaselineCeiling = 1.0;

    /// <summary>Energy ceilings swept (higher = higher actualization-rate regime).</summary>
    public static readonly double[] EnergyCeilings = { 1.0, 1.5, 2.0, 4.0, 8.0 };

    /// <summary>Seed energy scales for the energy-scaling study.</summary>
    public static readonly double[] EnergyScales = { 0.25, 0.5, 1.0, 2.0, 4.0, 8.0 };

    // ── Generalized dynamics with configurable energy ceiling ──────────────────────

    /// <summary>
    /// Activity-driven dynamics with a configurable ACTIVITY CEILING (the energy regime). The saturated
    /// activity a* = min(ceiling, f/d) bounds the link radius k = round(a*·K). Higher ceilings allow higher
    /// actualization rates → potentially new classes.
    /// </summary>
    public static double[,] AdaptiveNetworkWithCeiling(double[] initialActivity, int K = 6, double damping = 0.2,
        double feedback = 0.7, int steps = 120, double ceiling = 1.0)
    {
        int n = initialActivity.Length;
        var a = (double[])initialActivity.Clone();
        var adj = new double[n, n];
        for (int t = 0; t < steps; t++)
        {
            for (int i = 0; i < n; i++)
            {
                if (a[i] <= 0.5) continue;
                int k = (int)Math.Round(a[i] * K);
                for (int d = 1; d <= k; d++)
                {
                    int j = (i + d) % n;
                    adj[i, j] = 1.0; adj[j, i] = 1.0;
                }
            }
            int[] deg = new int[n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++) if (adj[i, j] != 0.0) deg[i]++;
            int maxDeg = Math.Max(deg.Max(), 1);
            for (int i = 0; i < n; i++)
            {
                a[i] = a[i] * (1.0 - damping) + feedback * (deg[i] / (double)maxDeg);
                a[i] = Math.Clamp(a[i], 0.0, ceiling);
            }
        }
        return adj;
    }

    /// <summary>Link radius (links per node) of an adjacency.</summary>
    public static double RadiusOf(double[,] adjacency) => StructureFromContent.LinkCount(adjacency) /
        (double)adjacency.GetLength(0);

    /// <summary>
    /// Activity-driven dynamics with LINK DECAY (de-actualization): links are created by active nodes as in
    /// <see cref="AdaptiveNetworkWithCeiling"/>, but a link is REMOVED when BOTH endpoints' activity falls below
    /// the decay threshold (a link de-actualizes when neither endpoint sustains it). The geometry is therefore
    /// energy-SUPPORTED: sectors persist only while actualization energy maintains the activity. Deterministic.
    /// </summary>
    public static (double[] Activity, double[,] Adjacency) AdaptiveNetworkWithDecayFull(double[] initialActivity,
        int K = 6, double damping = 0.2, double feedback = 0.7, int steps = 120, double ceiling = 1.0,
        double decayThreshold = 0.5)
    {
        int n = initialActivity.Length;
        var a = (double[])initialActivity.Clone();
        var adj = new double[n, n];
        for (int t = 0; t < steps; t++)
        {
            for (int i = 0; i < n; i++)
            {
                if (a[i] <= 0.5) continue;
                int k = (int)Math.Round(a[i] * K);
                for (int d = 1; d <= k; d++)
                {
                    int j = (i + d) % n;
                    adj[i, j] = 1.0; adj[j, i] = 1.0;
                }
            }
            // de-actualization: remove links where BOTH endpoints have fallen below the decay threshold
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (adj[i, j] != 0.0 && a[i] <= decayThreshold && a[j] <= decayThreshold)
                    {
                        adj[i, j] = 0.0; adj[j, i] = 0.0;
                    }
            int[] deg = new int[n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++) if (adj[i, j] != 0.0) deg[i]++;
            int maxDeg = Math.Max(deg.Max(), 1);
            for (int i = 0; i < n; i++)
            {
                a[i] = a[i] * (1.0 - damping) + feedback * (deg[i] / (double)maxDeg);
                a[i] = Math.Clamp(a[i], 0.0, ceiling);
            }
        }
        return (a, adj);
    }

    /// <summary>Seed activity pattern scaled by an energy scale E.</summary>
    public static double[] EnergyScaledSeed(int n, double energyScale)
    {
        var a = new double[n];
        for (int i = 0; i < n; i++)
            a[i] = (i % 3 == 0) ? 0.95 * energyScale : 0.2 * energyScale;
        return a;
    }

    // ── 1. Energy scaling ──────────────────────────────────────────────────────────

    /// <summary>Attractor radius vs seed energy scale E (baseline ceiling 1.0).</summary>
    public static (double EnergyScale, double Radius)[] RadiusVsEnergyScale(int n = 96, int K = DefaultK)
    {
        var result = new List<(double, double)>();
        foreach (double e in EnergyScales)
        {
            var net = AdaptiveNetworkWithCeiling(EnergyScaledSeed(n, e), K, 0.3, 0.7, 120, BaselineCeiling);
            result.Add((e, RadiusOf(net)));
        }
        return result.ToArray();
    }

    /// <summary>Does the attractor radius respond to the seed energy scale (radius grows with E)?</summary>
    public static bool RadiusRespondsToEnergyScale(int n = 96, int K = DefaultK)
    {
        var data = RadiusVsEnergyScale(n, K);
        return data[^1].Radius > data[0].Radius + 0.5;
    }

    // ── 2. Actualization-rate regimes ──────────────────────────────────────────────

    /// <summary>Distinct radii realized over the feedback sweep at each energy ceiling.</summary>
    public static (double Ceiling, double[] Radii)[] LadderByCeiling(int n = 96, int K = DefaultK)
    {
        var result = new List<(double, double[])>();
        foreach (double ceil in EnergyCeilings)
        {
            var radii = new List<double>();
            for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
            {
                var net = AdaptiveNetworkWithCeiling(EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, ceil);
                radii.Add(RadiusOf(net));
            }
            result.Add((ceil, radii.Distinct().OrderBy(r => r).ToArray()));
        }
        return result.ToArray();
    }

    /// <summary>Maximum realized radius at a given energy ceiling.</summary>
    public static double MaxRadiusAtCeiling(double ceiling, int n = 96, int K = DefaultK)
    {
        double max = 0.0;
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
        {
            var net = AdaptiveNetworkWithCeiling(EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, ceiling);
            max = Math.Max(max, RadiusOf(net));
        }
        return max;
    }

    // ── 3. Attractor phase transitions ─────────────────────────────────────────────

    /// <summary>
    /// Distinct spectral classes (KS single-linkage, ε=0.12) realized over the feedback sweep at a given
    /// energy ceiling — the count of attractor phases accessible in that regime.
    /// </summary>
    public static int SpectralClassCount(double ceiling, int n = 96, int K = DefaultK, double ks = 0.12)
    {
        var shapes = new List<double[]>();
        for (double f = 0.2; f <= 1.0 + 1e-9; f += 0.1)
        {
            var net = AdaptiveNetworkWithCeiling(EnergyScaledSeed(n, 1.0), K, 0.3, f, 120, ceiling);
            var sh = SpectrumRobustness.NormalizedShape(SpectrumRobustness.LaplacianOf(net));
            if (sh.Length > 0) shapes.Add(sh);
        }
        if (shapes.Count == 0) return 0;
        var labels = new int[shapes.Count];
        Array.Fill(labels, -1);
        int next = 0;
        for (int i = 0; i < shapes.Count; i++)
        {
            if (labels[i] != -1) continue;
            labels[i] = next;
            for (int j = 0; j < shapes.Count; j++)
                if (labels[j] == -1 && SpectralCurvature.KolmogorovSmirnov(shapes[i], shapes[j]) < ks)
                    labels[j] = next;
            next++;
        }
        return next;
    }

    /// <summary>Does the number of accessible spectral classes GROW with the energy ceiling?</summary>
    public static bool SpectralClassesGrowWithEnergy(int n = 96, int K = DefaultK)
        => SpectralClassCount(EnergyCeilings[^1], n, K) > SpectralClassCount(EnergyCeilings[0], n, K);

    // ── 4. Family-count evolution ──────────────────────────────────────────────────

    /// <summary>Octave-family count and hierarchy span vs energy ceiling (at fixed parameters).</summary>
    public static (double Ceiling, int Families, double Span, double Radius)[] FamilyEvolution(
        double feedback = 0.7, double damping = 0.3, int n = 96, int K = DefaultK)
    {
        var result = new List<(double, int, double, double)>();
        foreach (double ceil in EnergyCeilings)
        {
            var net = AdaptiveNetworkWithCeiling(EnergyScaledSeed(n, 1.0), K, damping, feedback, 120, ceil);
            result.Add((ceil, StructureFromContent.FamilyCount(net),
                StructureFromContent.HierarchySpan(net), RadiusOf(net)));
        }
        return result.ToArray();
    }

    // ── 5. High-energy classes ─────────────────────────────────────────────────────

    /// <summary>
    /// Are there attractor classes reachable ONLY above the baseline energy regime? True if the maximum
    /// radius at a high ceiling EXCEEDS the maximum at the baseline ceiling (K for the standard ladder).
    /// </summary>
    public static bool HighEnergyClassesExist(int n = 96, int K = DefaultK)
    {
        double baseline = MaxRadiusAtCeiling(BaselineCeiling, n, K);
        double high = MaxRadiusAtCeiling(EnergyCeilings[^1], n, K);
        return high > baseline + 0.5;
    }

    /// <summary>Maximum radius realized across the baseline ceiling (the current parameter-range cap).</summary>
    public static double BaselineMaxRadius(int n = 96, int K = DefaultK)
        => MaxRadiusAtCeiling(BaselineCeiling, n, K);

    // ── Classification ────────────────────────────────────────────────────────────

    /// <summary>
    /// Data-driven classification:
    ///   NO EFFECT     — the energy regime changes nothing: same radius ladder, same class count at every
    ///                   ceiling, no response to seed energy;
    ///   PARTIAL EFFECT — energy shifts the geometry (radii/classes move) but no NEW class appears beyond
    ///                   the baseline range cap;
    ///   NEW CLASSES   — higher actualization-energy regimes open NEW attractor classes: the radius ladder
    ///                   extends beyond the baseline cap (K), the spectral class count grows with the
    ///                   ceiling, and the seed energy scale raises the realized radius — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK)
    {
        bool responds = RadiusRespondsToEnergyScale(n, K);
        bool grows = SpectralClassesGrowWithEnergy(n, K);
        bool newClasses = HighEnergyClassesExist(n, K);

        if (!responds && !grows && !newClasses) return "NO EFFECT";
        if (newClasses && grows) return "NEW CLASSES";
        return "PARTIAL EFFECT";
    }
}
