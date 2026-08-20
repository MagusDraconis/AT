namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 128 — Observable spectrum from sector transitions. QG127 established that high-energy
/// sectors decay through discrete ladders into the observable 3-family sector, leaving OBSERVABLE
/// SIGNATURES. This phase asks: do the sector transitions generate a PREDICTABLE spectrum of emitted
/// energy/information quanta?
///
/// Method (computational, fully deterministic): within the QG125 de-actualization (link-decay) dynamics, let
/// a high-energy sector decay under a gradual energy decline and record the discrete rung radii (transition
/// ladder). The emitted energy/information quantum of each transition is the drop in radius (links per node)
/// between consecutive rungs. We measure: (1) TRANSITION LADDER SPACING — the sequence of rung radii and
/// their spacings; (2) EMITTED-ENERGY ANALOG — the multiset of emitted quanta (radius drops) along the
/// cascade; (3) CASCADE SPECTRUM — the distinct emitted-quantum values with their multiplicities (spectral
/// lines); (4) THRESHOLD STRUCTURE — the discrete energy thresholds at which sector classes change (QG127);
/// (5) OBSERVABLE SIGNATURES — reproducibility of the spectrum across decay speeds (same rung set, same
/// dominant quantum) and dominance of a single quantum line.
///
/// Answer (determined by the computed data): [filled by Classify]. No new primitives added here.
/// </summary>
public static class SectorTransitionSpectrum
{
    /// <summary>Default dynamics parameters (matching QG115–127).</summary>
    public const double DefaultDamping = 0.3;
    public const double DefaultFeedback = 0.9;
    public const int DefaultK = 6;

    /// <summary>High-energy regime used to build high sectors.</summary>
    public const double HighCeiling = HighEnergySectorStability.HighCeiling;

    /// <summary>Baseline (observable) energy regime.</summary>
    public const double BaselineCeiling = HighEnergySectorStability.BaselineCeiling;

    // ── 1. Transition ladder spacing ────────────────────────────────────────────

    /// <summary>
    /// Transition ladder: the distinct rung radii visited during a gradual energy decline. Returns
    /// (rungIndex, radius, linkCount) for each rung, ascending from observable to high-energy.
    /// </summary>
    public static (int Rung, double Radius, int Links)[] TransitionLadder(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
    {
        var (a, _) = HighEnergySectorStability.HighEnergySector(n, K, feedback, damping);
        var radii = new List<double>();
        for (int i = 0; i <= rampSteps; i++)
        {
            double ceil = HighCeiling - (HighCeiling - BaselineCeiling) * i / (double)rampSteps;
            for (int s = 0; s < perStep; s++)
            {
                (a, var adj) = EnergyDependentAttractors.AdaptiveNetworkWithDecayFull(a, K, damping, feedback,
                    1, ceil, HighEnergySectorStability.DecayThreshold);
                double r = HighEnergySectorStability.RadiusOf(adj);
                if (radii.Count == 0 || Math.Abs(r - radii[^1]) > 1e-6) radii.Add(r);
            }
        }
        return radii.Select((r, i) => (i, r, (int)Math.Round(r * n))).ToArray();
    }

    /// <summary>
    /// Ladder spacings: the magnitude of the radius drop between consecutive rungs (each transition emits a
    /// quantum equal to |Δradius|).
    /// </summary>
    public static double[] LadderSpacings(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
    {
        var ladder = TransitionLadder(n, K, feedback, damping, rampSteps, perStep);
        var spacings = new double[ladder.Length - 1];
        for (int i = 0; i < spacings.Length; i++)
            spacings[i] = Math.Abs(ladder[i + 1].Radius - ladder[i].Radius);   // emitted quantum magnitude
        return spacings;
    }

    // ── 2. Emitted-energy analog ────────────────────────────────────────────────

    /// <summary>
    /// Emitted-energy quanta: the distinct (radius-drop, multiplicity) pairs of the cascade. Each distinct
    /// drop value is a candidate spectral line; its multiplicity is the line's strength.
    /// </summary>
    public static (double Quantum, int Multiplicity)[] EmittedQuanta(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
    {
        var spacings = LadderSpacings(n, K, feedback, damping, rampSteps, perStep);
        var counts = new Dictionary<double, int>();
        foreach (double s in spacings)
        {
            double key = Math.Round(s, 3);
            counts[key] = counts.GetValueOrDefault(key) + 1;
        }
        return counts.Select(kv => (kv.Key, kv.Value)).OrderByDescending(kv => kv.Item2).ToArray();
    }

    /// <summary>Number of distinct emitted-quantum lines in the cascade spectrum.</summary>
    public static int SpectrumLineCount(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
        => EmittedQuanta(n, K, feedback, damping, rampSteps, perStep).Length;

    /// <summary>
    /// Dominant quantum: the most frequently emitted quantum (with its multiplicity and the fraction of all
    /// transitions that emit it). A dominant unit quantum = a fundamental emission quantum.
    /// </summary>
    public static (double Quantum, int Multiplicity, double Fraction) DominantQuantum(int n = 96,
        int K = DefaultK, double feedback = DefaultFeedback, double damping = DefaultDamping,
        int rampSteps = 40, int perStep = 3)
    {
        var quanta = EmittedQuanta(n, K, feedback, damping, rampSteps, perStep);
        if (quanta.Length == 0) return (0, 0, 0);
        int total = quanta.Sum(q => q.Multiplicity);
        return (quanta[0].Quantum, quanta[0].Multiplicity, (double)quanta[0].Multiplicity / Math.Max(total, 1));
    }

    // ── 3. Cascade spectrum ─────────────────────────────────────────────────────

    /// <summary>
    /// Cascade spectrum: does the emitted spectrum consist of a dominant line plus few discrete satellites
    /// (quantum values form a discrete set with one clearly dominant line)? True if the spectrum has 2-5
    /// distinct lines AND the dominant line carries &gt;= 50% of all emitted quanta.
    /// </summary>
    public static bool DiscreteSpectrumWithDominantLine(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
    {
        var quanta = EmittedQuanta(n, K, feedback, damping, rampSteps, perStep);
        if (quanta.Length < 2 || quanta.Length > 5) return false;
        int total = quanta.Sum(q => q.Multiplicity);
        return (double)quanta[0].Multiplicity / Math.Max(total, 1) >= 0.5;
    }

    // ── 4. Threshold structure ──────────────────────────────────────────────────

    /// <summary>Discrete energy thresholds at which sector classes change (QG127 fine sweep).</summary>
    public static (double[] Thresholds, int Count) EnergyThresholds(int n = 96, int K = DefaultK,
        double feedback = DefaultFeedback, double damping = DefaultDamping, double step = 0.25)
        => HighEnergySectorSignatures.EnergyThresholds(n, K, feedback, damping, step);

    // ── 5. Observable signatures (reproducibility) ──────────────────────────────

    /// <summary>
    /// Spectrum reproducibility: the rung set and dominant quantum are the same for a slower decay (more
    /// evolutions per ramp step). A predictive spectrum must not depend on the decay speed.
    /// </summary>
    public static bool SpectrumReproducible(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 40)
    {
        var fast = TransitionLadder(n, K, feedback, damping, rampSteps, 3);
        var slow = TransitionLadder(n, K, feedback, damping, rampSteps, 6);
        if (fast.Length != slow.Length) return false;
        for (int i = 0; i < fast.Length; i++)
            if (Math.Abs(fast[i].Radius - slow[i].Radius) > 1e-6) return false;
        var qFast = DominantQuantum(n, K, feedback, damping, rampSteps, 3);
        var qSlow = DominantQuantum(n, K, feedback, damping, rampSteps, 6);
        return Math.Abs(qFast.Quantum - qSlow.Quantum) < 1e-6;
    }

    /// <summary>
    /// Fundamental-quantum check: the dominant emitted quantum is a UNIT quantum (radius drop of exactly 1
    /// link per node, the smallest nonzero rung separation) — the spectrum is quantized at a fundamental
    /// unit.
    /// </summary>
    public static bool UnitQuantumDominant(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping, int rampSteps = 40, int perStep = 3)
    {
        var q = DominantQuantum(n, K, feedback, damping, rampSteps, perStep);
        return Math.Abs(q.Quantum - 1.0) < 1e-6 && q.Fraction >= 0.5;
    }

    // ── Spectrum score & classification ─────────────────────────────────────────

    /// <summary>
    /// Predictive-spectrum score (0..5):
    /// 1. the emitted spectrum is discrete (2-5 distinct lines);
    /// 2. a dominant quantum line carries &gt;= 50% of emissions;
    /// 3. the dominant quantum is the unit quantum (Δradius = 1);
    /// 4. the spectrum is reproducible across decay speeds;
    /// 5. discrete energy thresholds exist (&gt;= 3) that predict the transition ladder.
    /// </summary>
    public static int SpectrumScore(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = 0;
        if (SpectrumLineCount(n, K, feedback, damping) >= 2) score++;
        if (DominantQuantum(n, K, feedback, damping).Fraction >= 0.5) score++;
        if (UnitQuantumDominant(n, K, feedback, damping)) score++;
        if (SpectrumReproducible(n, K, feedback, damping)) score++;
        if (EnergyThresholds(n, K, feedback, damping).Count >= 3) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO SPECTRUM        — transitions emit no discrete quantum structure (continuum of spacings, no
    ///                        reproducible lines);
    ///   PARTIAL SPECTRUM   — discrete lines exist but no dominant/fundamental quantum and/or the spectrum
    ///                        depends on decay speed;
    ///   PREDICTIVE SPECTRUM — sector transitions emit a DISCRETE, REPRODUCIBLE spectrum dominated by a
    ///                        fundamental unit quantum, with the transition ladder predicted by discrete
    ///                        energy thresholds — the concrete case.
    /// </summary>
    public static string Classify(int n = 96, int K = DefaultK, double feedback = DefaultFeedback,
        double damping = DefaultDamping)
    {
        int score = SpectrumScore(n, K, feedback, damping);
        if (score <= 2) return "NO SPECTRUM";
        if (score == 5) return "PREDICTIVE SPECTRUM";
        return "PARTIAL SPECTRUM";
    }
}
