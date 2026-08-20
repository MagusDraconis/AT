namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 127 — Observable signatures of high-energy sectors. QG124-126 established that higher-energy
/// attractor sectors exist, decay toward the observable 3-family sector, and map onto particle sectors
/// (SECTOR-PARTICLE MAPPING). This phase asks: can the METASTABLE high-energy sectors leave OBSERVABLE
/// remnants — signatures a low-energy observer could detect?
///
/// Method (computational, fully deterministic): within the QG125 de-actualization (link-decay) dynamics,
/// let a high-energy sector decay under a gradual energy decline and measure: (1) DECAY SIGNATURES — the
/// distinct sector classes (radius classes and their family structure) visited along the decay trajectory;
/// (2) CASCADE SPECTRA — whether the decay cascade consists of spectrally distinct states (distinct radius
/// classes, distinct family counts) rather than a smooth slide; (3) TRANSIENT SECTOR OCCUPATION — how long
/// the system dwells in each intermediate (non-endpoint) class, i.e. whether transients are measurable;
/// (4) ENERGY THRESHOLDS — the discrete energy levels at which new sector classes appear (a fine ceiling
/// sweep); (5) OBSERVABLE LOW-ENERGY REMNANTS — after full decay the system lands in the observable 3-family
/// sector.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here (reuses the
/// QG125 link-decay dynamics).
/// </summary>
public static class HighEnergySectorSignatures
{
    /// <summary>Default dynamics parameters (matching QG115–126).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;

    /// <summary>High-energy regime used to build high sectors.</summary>
    public const double HighCeiling = HighEnergySectorStability.HighCeiling;

    /// <summary>Baseline (observable) energy regime.</summary>
    public const double BaselineCeiling = HighEnergySectorStability.BaselineCeiling;

    // ── 1. Decay signatures ─────────────────────────────────────────────────────

    /// <summary>
    /// Decay trajectory under a GRADUAL energy decline (the physically natural decay channel): the high-
    /// energy sector is built at the high ceiling, then the ceiling is ramped down over R ramp steps with
    /// S evolutions per step. Returns (rampIndex, ceiling, radius, familyCount) at every evolution step.
    /// </summary>
    public static (int Step, double Ceiling, double Radius, int Families)[] DecayTrajectory(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30,
        int perStep = 3)
    {
        var (a, _) = HighEnergySectorStability.HighEnergySector(n, K, feedback, damping);
        var result = new List<(int, double, double, int)>();
        int step = 0;
        for (int i = 0; i <= rampSteps; i++)
        {
            double ceil = HighCeiling - (HighCeiling - BaselineCeiling) * i / (double)rampSteps;
            for (int s = 0; s < perStep; s++)
            {
                (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(a, K, damping, feedback,
                    1, ceil, HighEnergySectorStability.DecayThreshold);
                result.Add((step, ceil, HighEnergySectorStability.RadiusOf(adj),
                    StructureFromContent.FamilyCount(adj)));
                step++;
            }
        }
        return result.ToArray();
    }

    /// <summary>
    /// Decay-signature classes: the DISTINCT (radius, families) combinations visited along the decay
    /// trajectory. Each is a candidate observable signature of the decaying sector.
    /// </summary>
    public static (double Radius, int Families, int DwellSteps)[] DecaySignatureClasses(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30,
        int perStep = 3)
    {
        var traj = DecayTrajectory(n, K, feedback, damping, rampSteps, perStep);
        var counts = new Dictionary<(double, int), int>();
        foreach (var (_, _, r, f) in traj)
        {
            var key = (Math.Round(r, 3), f);
            counts[key] = counts.GetValueOrDefault(key) + 1;
        }
        return counts.Select(kv => (kv.Key.Item1, kv.Key.Item2, kv.Value))
            .OrderByDescending(c => c.Item3)
            .ToArray();
    }

    /// <summary>Number of distinct decay-signature classes (radius+families states) in the trajectory.</summary>
    public static int DecaySignatureCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
        => DecaySignatureClasses(n, K, feedback, damping, rampSteps, perStep).Length;

    // ── 2. Cascade spectra ──────────────────────────────────────────────────────

    /// <summary>Number of DISTINCT family counts visited along the decay cascade.</summary>
    public static int CascadeFamilyStates(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
        => DecayTrajectory(n, K, feedback, damping, rampSteps, perStep).Select(t => t.Families)
            .Distinct().Count();

    /// <summary>Number of DISTINCT radius classes visited along the decay cascade.</summary>
    public static int CascadeRadiusClasses(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
        => DecayTrajectory(n, K, feedback, damping, rampSteps, perStep).Select(t => Math.Round(t.Radius, 3))
            .Distinct().Count();

    /// <summary>
    /// Cascade spectra: does the decay pass through spectrally distinct intermediate states? True if the
    /// cascade visits at least 3 distinct radius classes AND at least 2 distinct family structures — a
    /// spectrally structured cascade rather than a smooth slide or a single jump.
    /// </summary>
    public static bool SpectrallyStructuredCascade(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
        => CascadeRadiusClasses(n, K, feedback, damping, rampSteps, perStep) >= 3
            && CascadeFamilyStates(n, K, feedback, damping, rampSteps, perStep) >= 2;

    // ── 3. Transient sector occupation ──────────────────────────────────────────

    /// <summary>
    /// Transient occupation: the total dwell time (in dynamics steps) spent in INTERMEDIATE classes — i.e.
    /// all decay-signature classes except the initial high-energy class and the final observable class.
    /// Returns (transientSteps, totalSteps, transientFraction, maxIntermediateDwell).
    /// </summary>
    public static (int TransientSteps, int TotalSteps, double TransientFraction, int MaxIntermediateDwell)
        TransientOccupation(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
            double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
    {
        var classes = DecaySignatureClasses(n, K, feedback, damping, rampSteps, perStep)
            .OrderBy(c => c.Radius).ToArray();
        int total = classes.Sum(c => c.DwellSteps);
        double obsRadius = ParticleSectorMapping.LowEnergySector(n, K, feedback, damping).Radius;
        double highRadius = classes.Select(c => c.Radius).Max();
        int transient = 0, maxDwell = 0;
        foreach (var c in classes)
        {
            bool isEndpoint = Math.Abs(c.Radius - highRadius) < 1e-6
                || Math.Abs(c.Radius - obsRadius) < 1e-6;
            if (!isEndpoint) { transient += c.DwellSteps; maxDwell = Math.Max(maxDwell, c.DwellSteps); }
        }
        return (transient, total, (double)transient / Math.Max(total, 1), maxDwell);
    }

    // ── 4. Energy thresholds ────────────────────────────────────────────────────

    /// <summary>
    /// Energy thresholds: fine sweep of the ceiling axis; each ceiling at which the fresh-attractor radius
    /// class changes is a discrete energy threshold where a new sector appears.
    /// </summary>
    public static (double[] Thresholds, int Count) EnergyThresholds(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, double step = 0.25)
    {
        var thresholds = new List<double>();
        double last = double.NaN;
        for (double c = BaselineCeiling; c <= HighCeiling + 1e-9; c += step)
        {
            var (_, adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(
                EnergyDependentAttractors.EnergyScaledSeed(n, 1.0), K, damping, feedback, 200, c,
                HighEnergySectorStability.DecayThreshold);
            double r = HighEnergySectorStability.RadiusOf(adj);
            if (!double.IsNaN(last) && Math.Abs(r - last) > 1e-6) thresholds.Add(c);
            last = r;
        }
        return (thresholds.ToArray(), thresholds.Count);
    }

    // ── 5. Observable low-energy remnants ───────────────────────────────────────

    /// <summary>
    /// After a full gradual decay to baseline, the system must settle in the observable sector: final radius
    /// equals the observable radius AND final family count equals the observable family count.
    /// </summary>
    public static bool ObservableRemnant(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 30, int perStep = 3)
    {
        var traj = DecayTrajectory(n, K, feedback, damping, rampSteps, perStep);
        var final = traj[^1];
        var obs = ParticleSectorMapping.LowEnergySector(n, K, feedback, damping);
        return Math.Abs(final.Radius - obs.Radius) < 1e-6 && final.Families == obs.Families;
    }

    // ── Signature score & classification ────────────────────────────────────────

    /// <summary>
    /// Observable-signature score (0..5):
    /// 1. the decay visits at least 3 distinct radius classes (a cascade exists);
    /// 2. the cascade passes through at least 2 distinct family structures (spectrally structured);
    /// 3. transient occupation is measurable (transient fraction &gt; 0.05);
    /// 4. at least 3 discrete energy thresholds exist;
    /// 5. the decay settles in the observable low-energy remnant.
    /// </summary>
    public static int SignatureScore(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = 0;
        if (CascadeRadiusClasses(n, K, feedback, damping) >= 3) score++;
        if (CascadeFamilyStates(n, K, feedback, damping) >= 2) score++;
        if (TransientOccupation(n, K, feedback, damping).TransientFraction > 0.05) score++;
        if (EnergyThresholds(n, K, feedback, damping).Count >= 3) score++;
        if (ObservableRemnant(n, K, feedback, damping)) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO SIGNATURE        — the decay leaves nothing observable: a single jump straight to the observable
    ///                         sector with no intermediate classes, no transient occupation, no energy
    ///                         thresholds;
    ///   PARTIAL SIGNATURE   — some structure exists (a few classes or thresholds) but transients are too
    ///                         brief or the cascade is not spectrally distinct;
    ///   OBSERVABLE SIGNATURE — the decay produces a spectrally structured multi-class cascade with measurable
    ///                         transient occupation and discrete energy thresholds, settling in the observable
    ///                         3-family remnant — a detectable signature of past high-energy sectors.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = SignatureScore(n, K, feedback, damping);
        if (score <= 2) return "NO SIGNATURE";
        if (score == 5) return "OBSERVABLE SIGNATURE";
        return "PARTIAL SIGNATURE";
    }
}
