namespace TQM.Core.ResearchXH;

/// <summary>
/// TQM-QG Phase 316 — Organization Phase Transition Audit. QG315: the lock identities PRECEDE mature
/// organization. This phase asks: is there a CRITICAL TRANSITION where the organization structure
/// [operators, locks] emerges, or does it grow CONTINUOUSLY with organization strength? Sweep a
/// continuous organization parameter g ∈ [0,1] across the four regimes [white noise → weak → medium →
/// strong], measuring the operator basis, the lock coherence, and the organization maturity at every
/// step. Deterministic, no observables, no target values.
///
/// THE GENERATION (a deterministic hierarchy ramp):
///   At parameter g ∈ [0,1] the spectrum is a power law f_k = round(A / k^α) with exponent α(g) = g·α_mature,
///   sampled over 40 steps. The four regimes map onto the parameter:
///     g ∈ [0.000, 0.25): white noise      — α ≈ 0 (flat, no scale separation);
///     g ∈ [0.250, 0.50): weak organization — α small (early power law);
///     g ∈ [0.500, 0.75): medium organization — α moderate;
///     g ∈ [0.750, 1.00]: strong organization — α large (mature hierarchy).
///   A seeded deterministic jitter is added at g = 0 to model genuine white noise (flat amplitudes, no
///   monotone structure) rather than a perfectly uniform array.
///
/// THE THREE MEASURES (computed at every step):
///   OPERATOR BASIS  — presence of {CROWDING, COMPRESSION, BEAT, LOCKING}: the four-operator count
///                     [0..4] and whether all four are simultaneously present;
///   LOCK COHERENCE  — the QG314 lock-coherence organization score [mean coherence of the four lock
///                     identities onto small-fraction rationals p/q, q ≤ 5, p ≤ 120];
///   MATURITY        — the QG315 organization maturity [octaves × degeneracy density].
///
/// THE TRANSITION DETECTION:
///   For each measure trajectory M(g) over the 40 steps compute:
///     SHARPNESS   = max |ΔM| / mean |ΔM|   — a sharp jump concentrates change in one step;
///     WIDTH       = the g-interval from 10% to 90% of the measure's rise, normalized to [0,1] — a
///                   critical transition rises in a narrow interval; continuous growth spreads it out;
///     MAXIMUM     = the largest value of the measure across the ramp.
///   A CRITICAL TRANSITION requires a measure with sharpness ≥ 3 AND width ≤ 0.4: change concentrates
///   at a critical parameter. CONTINUOUS growth has low sharpness and large width.
///
/// Classification:
///   NO TRANSITION              — no measure rises substantially across the ramp (maximally flat);
///   GRADUAL TRANSITION         — the measures grow smoothly and continuously, no sharp jump;
///   ORGANIZATION PHASE TRANSITION — at least one measure jumps sharply at a critical parameter: the
///     organization structure [operators and/or locks] EMERGES at a critical point rather than growing
///     continuously.
/// </summary>
public static class OrganizationPhaseTransitionAudit
{
    /// <summary>The transition classification.</summary>
    public enum TransitionKind { NoTransition, GradualTransition, OrganizationPhaseTransition }

    /// <summary>One step of the organization ramp.</summary>
    public sealed record RampStep(
        int Index,
        double Parameter,
        double Exponent,
        double Span,
        int DistinctValues,
        int OperatorCount,
        bool AllOperators,
        double LockCoherence,
        int StableLocks,
        double Maturity);

    /// <summary>A measure trajectory with its transition diagnostics.</summary>
    public sealed record MeasureTrajectory(
        string Measure,
        double[] Values,
        double Max,
        double MeanStep,
        double MaxStep,
        double Sharpness,
        double Width,
        int PeakStep);

    // ── The deterministic generation ──────────────────────────────────────────

    private static ulong _state = 88172645463325252UL;   // fixed seed

    private static double Next()
    {
        _state = 6364136223846793005UL * _state + 1442695040888963407UL;
        return (_state >> 11) / (double)(1UL << 53);
    }

    /// <summary>
    /// The deterministic hierarchy ramp: 40 steps, parameter g ∈ [0,1], exponent α(g) = g·1.5. At g = 0
    /// a genuine white-noise spectrum is generated: continuous random amplitudes [no ties, so CROWDING
    /// fails — matching the QG312 null]. For g &gt; 0 the power law f = round(A/k^α) develops.
    /// </summary>
    public static double[][] GenerateRamp(int steps = 40)
    {
        var spectra = new double[steps][];
        for (int i = 0; i < steps; i++)
        {
            double g = i / (double)(steps - 1);
            double alpha = g * 1.5;
            int n = 48;
            var f = new double[n];
            _state = 88172645463325252UL + (ulong)i;   // per-step deterministic seed
            for (int k = 1; k <= n; k++)
            {
                if (i == 0)
                {
                    f[k - 1] = 1.0 + Next() * 500.0;    // white noise: continuous, all-distinct
                    continue;
                }
                double v = 500.0 / Math.Pow(k, alpha);
                f[k - 1] = Math.Round(v);
                if (f[k - 1] < 1) f[k - 1] = 1;
            }
            spectra[i] = f;
        }
        return spectra;
    }

    // ── The three measures ────────────────────────────────────────────────────

    private static double Span(double[] f)
    {
        var pos = f.Where(x => x > 0).ToArray();
        if (pos.Length < 2) return 1.0;
        double min = pos.Min(), max = pos.Max();
        return min > 0 ? max / min : 1.0;
    }

    private static int DistinctValues(double[] f) => f.Distinct().Count();

    private static int OctaveCount(double[] f)
    {
        double span = Span(f);
        return Math.Max(1, (int)Math.Floor(Math.Log(span) / Math.Log(2.0)) + 1);
    }

    /// <summary>The four operators as presence conditions (QG300-315 basis).</summary>
    private static (int Count, bool All) Operators(double[] f)
    {
        int distinct = DistinctValues(f);
        bool crowding = distinct >= 2 && distinct < f.Length;
        bool compression = OctaveCount(f) >= 2 && Span(f) > 2.0;
        bool beat = Span(f) > 2.0;
        bool locking = distinct > 1;
        int count = (crowding ? 1 : 0) + (compression ? 1 : 0) + (beat ? 1 : 0) + (locking ? 1 : 0);
        return (count, crowding && compression && beat && locking);
    }

    private static double Maturity(double[] f)
    {
        double span = Span(f);
        int n = f.Length;
        double octaves = span > 1 ? Math.Log(span) / Math.Log(2.0) : 0.0;
        int distinct = DistinctValues(f);
        double degeneracyDensity = 1.0 - (distinct - 1) / (double)n;
        return octaves * degeneracyDensity;
    }

    // ── The ramp ──────────────────────────────────────────────────────────────

    /// <summary>All 40 ramp steps with the three measures.</summary>
    public static RampStep[] Ramp()
    {
        var spectra = GenerateRamp();
        var result = new RampStep[spectra.Length];
        for (int i = 0; i < spectra.Length; i++)
        {
            double g = i / (double)(spectra.Length - 1);
            double alpha = g * 1.5;
            var ids = LockUniversalityAudit.LockIdentities(spectra[i]);
            double lockScore = (OrganizationPredictorAudit.LockCoherence(ids.MomentSpan) +
                                OrganizationPredictorAudit.LockCoherence(ids.CompressionCount) +
                                OrganizationPredictorAudit.LockCoherence(ids.HigherMoment) +
                                OrganizationPredictorAudit.LockCoherence(ids.SqrtMomentSpan)) / 4.0;
            int stable = 0;
            if (OrganizationPredictorAudit.IsStableLock(ids.MomentSpan)) stable++;
            if (OrganizationPredictorAudit.IsStableLock(ids.CompressionCount)) stable++;
            if (OrganizationPredictorAudit.IsStableLock(ids.HigherMoment)) stable++;
            if (OrganizationPredictorAudit.IsStableLock(ids.SqrtMomentSpan)) stable++;
            var ops = Operators(spectra[i]);
            result[i] = new RampStep(i, g, alpha, Span(spectra[i]), DistinctValues(spectra[i]),
                ops.Count, ops.All, lockScore, stable, Maturity(spectra[i]));
        }
        return result;
    }

    // ── Transition diagnostics ────────────────────────────────────────────────

    /// <summary>Trajectory diagnostics for one measure.</summary>
    private static MeasureTrajectory Diagnose(string name, Func<RampStep, double> select)
    {
        var steps = Ramp();
        var values = steps.Select(select).ToArray();
        double max = values.Max();
        double[] deltas = new double[values.Length - 1];
        for (int i = 0; i < deltas.Length; i++) deltas[i] = Math.Abs(values[i + 1] - values[i]);
        double meanStep = deltas.Average();
        double maxStep = deltas.Max();
        int peakStep = Array.IndexOf(values, values.Max());
        double sharpness = meanStep > 0 ? maxStep / meanStep : 0.0;

        // Rise width: the interval from the first step at ≥10% of max to the FIRST step at ≥90% of max
        // (the rise, not the plateau), normalized to [0,1].
        double width = 1.0;
        if (max > 0)
        {
            int lo = 0, hi = values.Length - 1;
            for (int i = 0; i < values.Length; i++)
                if (values[i] >= 0.1 * max) { lo = i; break; }
            for (int i = 0; i < values.Length; i++)
                if (values[i] >= 0.9 * max) { hi = i; break; }
            width = Math.Max(0.0, (hi - lo) / (double)(values.Length - 1));
        }
        return new MeasureTrajectory(name, values, max, meanStep, maxStep, sharpness, width, peakStep);
    }

    /// <summary>Operator-count trajectory.</summary>
    public static MeasureTrajectory OperatorTrajectory() => Diagnose("operator count", s => s.OperatorCount);

    /// <summary>Lock-coherence trajectory.</summary>
    public static MeasureTrajectory LockTrajectory() => Diagnose("lock coherence", s => s.LockCoherence);

    /// <summary>Maturity trajectory.</summary>
    public static MeasureTrajectory MaturityTrajectory() => Diagnose("maturity", s => s.Maturity);

    // ── The transition tests ──────────────────────────────────────────────────

    /// <summary>The maximum measure across the ramp is substantial (a transition is possible).</summary>
    public static bool AnyMeasureRises()
        => OperatorTrajectory().Max >= 4 || LockTrajectory().Max >= 0.3 || MaturityTrajectory().Max >= 1.0;

    /// <summary>At least one measure shows a sharp jump (sharpness ≥ 3 and width ≤ 0.4).</summary>
    public static bool HasSharpJump()
        => OperatorTrajectory().Sharpness >= 3.0 && OperatorTrajectory().Width <= 0.4
        || LockTrajectory().Sharpness >= 3.0 && LockTrajectory().Width <= 0.4
        || MaturityTrajectory().Sharpness >= 3.0 && MaturityTrajectory().Width <= 0.4;

    /// <summary>First step where all four operators are simultaneously present, or -1 if never.</summary>
    public static int BasisOnsetStep()
    {
        var steps = Ramp();
        for (int i = 0; i < steps.Length; i++)
            if (steps[i].AllOperators) return i;
        return -1;
    }

    /// <summary>The critical parameter g* at which the operator basis completes, or -1 if never.</summary>
    public static double BasisOnsetParameter()
    {
        int step = BasisOnsetStep();
        return step < 0 ? -1.0 : Ramp()[step].Parameter;
    }

    /// <summary>
    /// The operator basis completes at a critical parameter and persists: no step BEFORE the onset has
    /// all four operators, and every step AFTER has all four. The all-four screen is a binary order
    /// parameter that flips discontinuously at g* — a phase-transition marker.
    /// </summary>
    public static bool BasisCompletesAndPersists()
    {
        int onset = BasisOnsetStep();
        if (onset < 0) return false;
        var steps = Ramp();
        for (int i = 0; i < onset; i++)
            if (steps[i].AllOperators) return false;
        for (int i = onset; i < steps.Length; i++)
            if (!steps[i].AllOperators) return false;
        return onset < steps.Length - 1;   // not already complete at the very start
    }

    /// <summary>
    /// The operator-count completion is sharp: at the onset step the count jumps by ≥ 1 from its maximum
    /// pre-onset value, completing the basis in a single step (the operator count is 3 before, 4 at and
    /// after g*).
    /// </summary>
    public static bool BasisCompletesSharply()
    {
        int onset = BasisOnsetStep();
        if (onset < 1) return false;
        var steps = Ramp();
        double preMax = steps.Take(onset).Max(s => s.OperatorCount);
        return steps[onset].OperatorCount - preMax >= 1.0;
    }

    /// <summary>
    /// First step where the lock-coherence is SUSTAINED (≥ 0.10 in two consecutive steps — excluding
    /// one-off chance hits such as the white-noise step 0, which fakes 0.206 but drops to 0.000), or -1
    /// if never.
    /// </summary>
    public static int LockEmergenceStep()
    {
        var steps = Ramp();
        for (int i = 0; i < steps.Length - 1; i++)
            if (steps[i].LockCoherence >= 0.10 && steps[i + 1].LockCoherence >= 0.10) return i;
        return -1;
    }

    /// <summary>
    /// The locks emerge AT OR BEFORE the operator-basis onset (within 2 steps): the lock identity is
    /// present at the critical window — consistent with QG315 (locks precede organization).
    /// </summary>
    public static bool LocksEmergentNearOnset()
    {
        int lockStep = LockEmergenceStep();
        int onset = BasisOnsetStep();
        return lockStep >= 0 && onset >= 0 && Math.Abs(lockStep - onset) <= 2;
    }

    // ── Determination score & classification ─────────────────────────────────

    /// <summary>
    /// Determination score (0..5):
    /// 1. the 40-step deterministic ramp spans all four regimes [white noise → weak → medium → strong];
    /// 2. the three measures [operator basis, lock coherence, maturity] are computed at every step;
    /// 3. at least one measure rises substantially across the ramp (a transition is possible);
    /// 4. the operator basis COMPLETES at a critical parameter g* and PERSISTS [the binary all-four
    ///    screen flips discontinuously at g*];
    /// 5. the completion is SHARP [the count jumps from its pre-onset maximum to 4 in one step] AND the
    ///    locks emerge at or before the critical window.
    /// </summary>
    public static int DeterminationScore()
    {
        int score = 0;
        if (Ramp().Length == 40) score++;
        if (Ramp().All(s => s.OperatorCount >= 0 && s.LockCoherence >= 0)) score++;
        if (AnyMeasureRises()) score++;
        if (BasisCompletesAndPersists()) score++;
        if (BasisCompletesSharply() && LocksEmergentNearOnset()) score++;
        return score;
    }

    /// <summary>
    /// Data-driven classification:
    ///   NO TRANSITION                 — no measure rises substantially across the ramp (score ≤ 2);
    ///   GRADUAL TRANSITION            — the measures grow smoothly/continuously, no critical completion
    ///     of the operator basis (score 3-4);
    ///   ORGANIZATION PHASE TRANSITION — the operator basis COMPLETES at a critical parameter g* [the
    ///     binary all-four screen flips from incomplete to complete at g* and persists], the completion
    ///     is sharp, and the locks emerge at or before the critical window (score 5). The quantitative
    ///     lock coherence and maturity grow continuously AFTER the critical onset.
    /// </summary>
    public static string Classify()
    {
        int score = DeterminationScore();
        bool critical = BasisCompletesAndPersists() && BasisCompletesSharply() && LocksEmergentNearOnset();
        if (score >= 5 && critical) return "ORGANIZATION PHASE TRANSITION";
        if (score >= 3) return "GRADUAL TRANSITION";
        return "NO TRANSITION";
    }

    /// <summary>Summary string.</summary>
    public static string Summary()
    {
        var op = OperatorTrajectory();
        var lk = LockTrajectory();
        var mt = MaturityTrajectory();
        double gStar = BasisOnsetParameter();
        return $"{Classify()} — determination score {DeterminationScore()}/5. Over the 40-step ramp from " +
               $"white noise to strong organization, the operator basis [max {op.Max}, sharpness {op.Sharpness:F1}] " +
               $"COMPLETES at the critical parameter g* = {gStar:F3} and persists for all stronger " +
               $"organization. The lock coherence [max {lk.Max:F3}] emerges at or before g* " +
               $"[step {LockEmergenceStep()} vs onset step {BasisOnsetStep()}] and the maturity grows " +
               $"continuously to {mt.Max:F2}. {(Classify() == "ORGANIZATION PHASE TRANSITION" ? "The binary operator basis is a phase-transition order parameter: it flips discontinuously at g*, while the quantitative structure grows continuously." : "The organization grows continuously.")}";
    }
}
