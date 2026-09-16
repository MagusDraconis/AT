using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_060 - PHASE EVOLUTION AUDIT (group G - Gravity Source).
///
/// QUESTION. Can the difference-generated phase push (G_059) be PROMOTED to an actual UPDATE RULE? Test
/// rho(t+1) = rho(t) + eps*D*rho and other AT-native updates. Measure the PHASE EVOLUTION RANK, the AMPLITUDE
/// EVOLUTION RANK, STABILITY and FIXED POINTS. Critical: does any AT-native update generate non-trivial phase dynamics?
///
/// ANSWER: **DERIVED - the promotion exists, and the audit also measures precisely which form fails: the one the
/// question names.**
///
///  (1) THE WHOLE AUDIT IS EXACT, BECAUSE EVERY CANDIDATE IS CIRCULANT. The difference is a function of the shift, so
///      each update is diagonal in the Fourier basis with a COMPLEX MULTIPLIER per channel; the audit drives both its
///      analysis and its iteration from that one number, so no sign convention is left to guess and nothing is
///      estimated. The reachable rank is computed twice - per channel, and by a Krylov scan over the state space - and
///      the two routes agree.
///
///  (2) THE QUESTION'S OWN UPDATE IS STABLE, AND THAT IS WHY IT FAILS. rho + eps*D*rho has |mu|^2 = 1 - 2 eps x (1-eps)
///      with x = 1 - cos delta_c, so it CONTRACTS for 0 < eps < 1: the difference's symmetric part is negative
///      semi-definite, which makes this a DISSIPATIVE step and not the skew step the name "difference" suggests. Its
///      attractor is the kernel of D - the CONSTANT - so measured over 20000 steps the deviation norm falls from
///      1.005E+000 to 2.424E-001 and the state is driven to the PHASE-FREE point. That is the closure with G_059: THE
///      STABLE DIFFERENCE FLOW IS ATTRACTED EXACTLY TO THE STATE WITH NO PHASE CONTENT, which is why the canonical
///      state has none. The phase content itself oscillates on the way down rather than decaying monotonically - a
///      first draft asserted a plain fall and the measurement corrected it - so the audit reports the DEVIATION NORM as
///      the envelope and the phase content as the oscillation.
///
///  (3) THE PHASE SURVIVES ONLY WHERE |mu| = 1, AND EXACTLY ONE FORM HAS IT. The backward difference and the centred
///      difference have |mu| > 1 - they amplify every non-constant mode and leave the simplex in a measured number of
///      steps - and the difference's own exact flow exp(eps*D) contracts like the forward step. The UNITARY form, the
///      exact flow of the CENTRED difference, has |mu| = 1 exactly: it sustains all 42 phase directions, conserves the
///      deviation norm exactly (1.005011E+000 at every horizon tested) and the sum exactly, and keeps every cell
///      positive over 20000 steps. So the promotion is real and it is the norm-preserving one.
///
///  (4) THE RANK IS BOUNDED BY THE SUBSTRATE AT 42 OF 53, AND THE ELEVEN THAT REMAIN ARE STRUCTURALLY UNREACHABLE. A
///      circulant update cannot mix a quadrature pair with the constant, so from a phase-free state it can only reach a
///      channel's OTHER quadrature: the 42 half-hidden channels give 42, and the 11 left are the DOUBLY-HIDDEN channels
///      (10 directions - channels carrying no visible quadrature at all, so a phase-free state has nothing there to
///      act on) plus the ALTERNATING MODE (1 - a channel with no partner quadrature, which every circulant merely
///      rescales). No update of this family can ever reach them.
///
///  (5) THE AT-NATIVE POSITIVITY GUARD IS MEASURED AND IT NEVER FIRES. rho is a density, so the audit carries a
///      positivity-clipped variant - and finds it numerically IDENTICAL to the unclipped step, because dissipation
///      keeps the state inside the simplex (minimum cell 7.4E-001 at the worst point tested). A guard that never fires
///      is reported rather than counted as a mechanism.
/// </summary>
public static class PhaseEvolutionAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;

    public static double[] Base() => RhoAccessibilityAudit.BaseState();
    public static double[] Uniform() => Enumerable.Repeat(1.0, Cells).ToArray();
    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));
    public static double PhaseNorm(double[] v) => Norm(AmplitudePhaseAudit.PhasePart(v));
    public static double AmplitudeNorm(double[] v) => Norm(AmplitudePhaseAudit.AmplitudePart(v));
    public const double PhaseFloor = 1e-9;
    public static int PhaseDimension() => AmplitudePhaseAudit.PhaseDimension();

    // ===================== 1. THE CHANNEL ALGEBRA (EXACT) =====================

    /// <summary>Memoised: the step is called tens of thousands of times and the basis never changes.</summary>
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<int, double[][]> BasisCache = new();
    public static double[][] Basis(int channel)
    {
        return BasisCache.GetOrAdd(channel, KernelStructureAudit.ChannelBasis);
    }

    public static int[] Channels() => Enumerable.Range(1, Cells / 2).ToArray();
    public static double Delta(int channel) => 2.0 * Math.PI * channel / Cells;

    /// <summary>The difference D = S - 1, with S the forward shift, as the complex multiplier on channel c.</summary>
    public static System.Numerics.Complex DifferenceMultiplier(int c) => System.Numerics.Complex.Exp(new System.Numerics.Complex(0, Delta(c))) - 1.0;

    /// <summary>The centred (skew) difference: a bare rotation generator, with no symmetric part at all.</summary>
    public static System.Numerics.Complex SkewMultiplier(int c) => new System.Numerics.Complex(0, Math.Sin(Delta(c)));

    /// <summary>Every candidate's multiplier: one complex number per channel, which is all a circulant update needs.</summary>
    /// <summary>
    /// Memoised: the multiplier is a pure function of (update, channel, eps), and the exact flow and the unitary form each
    /// evaluate a complex exponential per channel per step otherwise - which is where a 20000-step scan spent its time.
    /// </summary>
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<(string, int, double), System.Numerics.Complex> MultiplierCache = new();

    public static System.Numerics.Complex Multiplier(string update, int c, double eps)
    {
        return MultiplierCache.GetOrAdd((update, c, eps), key => MultiplierUncached(key.Item1, key.Item2, key.Item3));
    }

    private static System.Numerics.Complex MultiplierUncached(string update, int c, double eps) => update switch
    {
        "forward difference" => 1.0 + eps * DifferenceMultiplier(c),
        "backward difference" => 1.0 - eps * DifferenceMultiplier(c),
        "centred (skew) difference" => 1.0 + eps * SkewMultiplier(c),
        "exact flow exp(eps D)" => System.Numerics.Complex.Exp(eps * DifferenceMultiplier(c)),
        "unitary (Cayley of the skew part)" => (1.0 + eps * SkewMultiplier(c)) / (1.0 - eps * SkewMultiplier(c)),
        "positivity-clipped difference" => 1.0 + eps * DifferenceMultiplier(c),
        "actualization (CONTROL)" => System.Numerics.Complex.One,
        _ => throw new ArgumentException(update),
    };

    public static (string Update, string Kind, bool IsControl)[] Updates() => new (string, string, bool)[]
    {
        ("forward difference", "the question's own test", false),
        ("backward difference", "anti-dissipative", false),
        ("centred (skew) difference", "a bare rotation generator", false),
        ("exact flow exp(eps D)", "the difference's own flow", false),
        ("unitary (Cayley of the skew part)", "norm-preserving", false),
        ("positivity-clipped difference", "AT-native: rho is a density", false),
        ("actualization (CONTROL)", "the running rule, supplied for contrast", true),
    };

    public static double[] Epsilons() => new[] { 1e-3, 1e-2, 1e-1 };

    // ===================== 2. THE REAL 2x2 BLOCK, APPLIED EXACTLY =====================

    /// <summary>
    /// A circulant operator acts on each quadrature pair (cos_c, sin_c) as a real 2x2 matrix. The audit builds it from
    /// the complex multiplier rather than from a sign convention, so the same multiplier drives both the analysis and
    /// the iteration.
    /// </summary>
    public static (double A11, double A12, double A21, double A22) BlockOf(System.Numerics.Complex mu)
    {
        double re = mu.Real, im = mu.Imaginary;
        return (re, -im, im, re);
    }

    public static double[] Compose(double mean, double[][] coeffs)
    {
        var result = Enumerable.Repeat(mean, Cells).ToArray();
        for (int c = 1; c <= Cells / 2; c++)
        {
            var basis = Basis(c);
            for (int k = 0; k < basis.Length; k++)
                for (int i = 0; i < Cells; i++) result[i] += coeffs[c][k] * basis[k][i];
        }
        return result;
    }

    public static double[][] Coefficients(double[] state)
    {
        var coeffs = new double[Cells / 2 + 1][];
        for (int c = 1; c <= Cells / 2; c++)
        {
            var basis = Basis(c);
            coeffs[c] = basis.Select(b => state.Zip(b, (x, y) => x * y).Sum()).ToArray();
        }
        return coeffs;
    }

    /// <summary>One exact step of a circulant update, or of the clipped (nonlinear, AT-native) variant.</summary>
    public static double[] Step(string update, double[] state, double eps)
    {
        if (update == "positivity-clipped difference")
        {
            var raw = Step("forward difference", state, eps);
            var clipped = raw.Select(x => Math.Max(x, 0.0)).ToArray();
            // rho is a density, so the guard must restore the TOTAL MASS, and it must do so STABLY. Dividing by the
            // clipped mean does not: where the clip removes most of the state that mean is near zero, so the division
            // explodes (a first version produced rows scaled by 1E6, which overflowed the rank scan's own subspace) and
            // on a zero-mean deviation it annihilates the state outright. A uniform SHIFT restores the mean exactly, is
            // bounded, and reduces to the unclipped step wherever the clip is inactive - which is everywhere measured.
            double target = state.Average(), survived = clipped.Average();
            return clipped.Select(x => x + (target - survived)).ToArray();
        }
        double centre = state.Average();
        var coeffs = Coefficients(state);
        for (int c = 1; c <= Cells / 2; c++)
        {
            if (coeffs[c].Length == 0) continue;
            var (a11, a12, a21, a22) = BlockOf(Multiplier(update, c, eps));
            double a = coeffs[c][0], b = coeffs[c].Length > 1 ? coeffs[c][1] : 0.0;
            coeffs[c][0] = a11 * a + a12 * b;
            if (coeffs[c].Length > 1) coeffs[c][1] = a21 * a + a22 * b;
        }
        return Compose(centre, coeffs);
    }

    public static double[] Orbit(string update, double[] start, double eps, int steps)
    {
        var state = (double[])start.Clone();
        for (int k = 0; k < steps; k++) state = Step(update, state, eps);
        return state;
    }

    // ===================== 3. THE RANKS =====================

    /// <summary>
    /// The update whose LINEAR response defines reachability. For the linear forms it is the update itself; for the
    /// AT-native clipped form it is its linear part, because the guard is measured to be INACTIVE on densities
    /// (TheClippedUpdateNeverFires) so the two coincide there. Feeding a density guard a zero-mean DEVIATION - which a
    /// first version did - makes the guard fire on an object that is not a state: that is a defect in the probe, not a
    /// finding about AT, and it is why this indirection exists rather than a probe on an arbitrary vector.
    /// </summary>
    public static string RankProbeUpdate(string update)
        => update == "positivity-clipped difference" ? "forward difference" : update;

    /// <summary>
    /// The ONE-STEP LINEAR phase response to each visible perturbation. Measured as a linear response and NOT as a
    /// difference of two orbits: that construction subtracts two vectors of magnitude 1 to obtain a response of order
    /// 1E-6, and the cancellation leaves a MEASURED relative leak of 2.329E-008 into the eleven unreachable directions -
    /// identical for the linear and the clipped update, which is how the audit knows the leak is its own arithmetic and
    /// not a property of either. The linear response has no such floor.
    /// </summary>
    public static (int Channel, string VisibleKind, double CoupledPhaseComponent)[] ChannelCoupling(string update, double eps)
        => KernelStructureAudit.VisibleModeVectors().Select(m =>
            (m.Channel, m.Kind, PhaseNorm(Step(RankProbeUpdate(update), m.Mode, eps)))).ToArray();

    public static int ReachablePhaseRank(string update, double eps = 1e-3)
        => ChannelCoupling(update, eps).Count(t => t.CoupledPhaseComponent > PhaseFloor);

    /// <summary>
    /// The independent cross-check: the same number from a Krylov scan of the linear responses, with the rank taken in
    /// the 53-DIMENSIONAL coordinate space of the phase sector. Measuring it in the 96-dimensional state space let the
    /// count exceed the space it lived in - 63, then 54, inside 53 dimensions - because a Gram-Schmidt residual on
    /// nearly dependent vectors is not a reliable rank; in coefficient space the bound is structural.
    /// </summary>
    public static int ReachablePhaseRankByKrylov(string update, double eps = 1e-3, int horizon = 12)
    {
        var phaseBasis = AmplitudePhaseAudit.PhaseModes().Select(m => m.Mode).ToArray();
        var rows = new List<double[]>();
        foreach (var mode in KernelStructureAudit.VisibleModeVectors().Select(m => m.Mode))
        {
            var state = mode;
            for (int k = 0; k <= horizon; k++)
            {
                rows.Add(phaseBasis.Select(b => state.Zip(b, (x, y) => x * y).Sum()).ToArray());
                state = Step(RankProbeUpdate(update), state, eps);
            }
        }
        return Rank(rows);
    }

    /// <summary>
    /// Rank by Gram-Schmidt in the space the vectors actually live in, rows unit-scaled first: one absolute gate is then
    /// scale-free, and a nearly dependent row cannot be mistaken for a new direction.
    /// </summary>
    private static int Rank(IEnumerable<double[]> vectors)
    {
        var basis = new List<double[]>();
        foreach (var v0 in vectors)
        {
            var v = (double[])v0.Clone();
            double original = Norm(v);
            if (original < PhaseFloor) continue;
            for (int i = 0; i < v.Length; i++) v[i] /= original;
            for (int pass = 0; pass < 3; pass++)
                foreach (var b in basis)
                {
                    double dot = v.Zip(b, (x, y) => x * y).Sum();
                    for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
                }
            double norm = Norm(v);
            if (norm > 1e-9) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    public static int ReachableAmplitudeRank(string update, double eps = 1e-3)
    {
        var amplitudeBasis = AmplitudePhaseAudit.AmplitudeModes().Select(m => m.Mode).ToArray();
        var rows = KernelStructureAudit.VisibleModeVectors().Select(m =>
        {
            var image = Step(RankProbeUpdate(update), m.Mode, eps);
            return amplitudeBasis.Select(a => image.Zip(a, (x, y) => x * y).Sum()).ToArray();
        }).ToArray();
        return Rank(rows);
    }

    /// <summary>The measured arithmetic floor of the difference-of-orbits construction, and why it is not used.</summary>
    public static double DifferenceOfOrbitsLeak(string update, double eps = 1e-3)
    {
        var phaseBasis = AmplitudePhaseAudit.PhaseModes();
        var visibleChannels = KernelStructureAudit.VisibleModeVectors().Select(v => v.Channel).Distinct().ToHashSet();
        var baseline = Step(RankProbeUpdate(update), Base(), eps);
        var mode = KernelStructureAudit.VisibleModeVectors().First().Mode;
        var probe = Base().Zip(mode, (r, m) => r + 1e-3 * m).ToArray();
        var response = Step(RankProbeUpdate(update), probe, eps).Zip(baseline, (a, b) => a - b).ToArray();
        double signal = 0, leak = 0;
        foreach (var m in phaseBasis)
        {
            double dot = Math.Abs(response.Zip(m.Mode, (x, y) => x * y).Sum());
            if (visibleChannels.Contains(m.Channel)) signal = Math.Max(signal, dot);
            else leak = Math.Max(leak, dot);
        }
        return signal < 1e-30 ? 0.0 : leak / signal;
    }

    /// <summary>
    /// The directions the difference cannot reach at all: the doubly-hidden channels (where the phase-free subspace has
    /// no component) and the alternating channel, which is one real direction the difference merely rescales.
    /// </summary>
    public static string[] UnreachablePhaseDirections()
    {
        int coupled = ReachablePhaseRank(SustainingForm);
        var emptyChannels = KernelStructureAudit.ModeTable().GroupBy(t => t.Channel)
            .Where(g => g.Key != Cells / 2 && g.Count() > 1 && g.All(x => x.Class == "HIDDEN"))
            .Select(g => g.Key).OrderBy(x => x).ToArray();
        var names = emptyChannels.Select(c => $"empty channel {c} (both quadratures)").ToList();
        names.Add($"the alternating mode (channel {Cells / 2})");
        return new[] { $"{PhaseDimension() - coupled} unreachable phase directions" }.Concat(names).ToArray();
    }

    // ===================== 4. STABILITY AND THE FIXED POINTS =====================

    public static (int Decaying, int Sustained, int Growing) StabilitySplit(string update, double eps)
    {
        int decaying = 0, sustained = 0, growing = 0;
        foreach (var c in PhaseBearingChannels())
        {
            if (PhaseCouplingOfChannel(update, c, eps) < 1e-9) continue;
            double modulus = Multiplier(update, c, eps).Magnitude;
            if (Math.Abs(modulus - 1.0) < 1e-12) sustained++;
            else if (modulus < 1.0) decaying++;
            else growing++;
        }
        return (decaying, sustained, growing);
    }

    /// <summary>
    /// The channels that carry a VISIBLE mode - the only ones a phase-free state can leave from. Measured from the mode
    /// table rather than assumed: the five doubly-hidden channels carry no visible quadrature at all, which is exactly
    /// why they are unreachable.
    /// </summary>
    public static int[] PhaseBearingChannels()
        => KernelStructureAudit.VisibleModeVectors().Select(m => m.Channel).Distinct().OrderBy(c => c).ToArray();

    private static double PhaseCouplingOfChannel(string update, int c, double eps)
    {
        var cos = Basis(c)[0];
        return PhaseNorm(Step(update, cos, eps));
    }

    public static double SpectralRadius(string update, double eps)
        => Channels().Max(c => Multiplier(update, c, eps).Magnitude);

    /// <summary>The fixed points: the modes a step leaves exactly where they are.</summary>
    /// <summary>
    /// The fixed-point set, counted in MODES. A doublet channel has two quadratures and channel 48 has one, so the
    /// count is 2 per doublet and 1 for the alternating channel, plus the constant. A first version counted CHANNELS and
    /// reported 49 fixed points for the identity, which moves nothing and fixes all 96.
    /// </summary>
    public static (string Update, int FixedPointDimension, string NeutrallyStable)[] FixedPointTable(double eps = 1e-3)
        => Updates().Select(u => (u.Update, FixedPointDimension(u.Update, eps),
            u.Update == SustainingForm ? "every mode" : "none")).ToArray();

    public static int FixedPointDimension(string update, double eps = 1e-3)
    {
        int fixedModes = 1;                                   // the constant
        foreach (var c in Channels())
        {
            if ((Multiplier(update, c, eps) - 1.0).Magnitude >= 1e-12) continue;
            fixedModes += c == Cells / 2 ? 1 : 2;
        }
        return fixedModes;
    }

    /// <summary>
    /// The CENTRED family additionally fixes the ALTERNATING MODE, because the centred difference annihilates it:
    /// (S - S^-1) e_48 = 0. So the alternating direction - one of the eleven the phase cannot reach from a phase-free
    /// state - is in the centred difference's KERNEL. Measured, and it is why those forms have a 2-dimensional
    /// fixed-point set rather than 1.
    /// </summary>
    public static bool TheCentredFamilyAlsoFixesTheAlternatingMode()
        => FixedPointDimension("centred (skew) difference") == 2
        && FixedPointDimension(SustainingForm) == 2
        && FixedPointDimension("forward difference") == 1;

    public static bool TheUniformStateIsTheFixedPoint(string update, double eps = 1e-3)
        => PhaseNorm(Step(update, Uniform(), eps)) < 1e-12 && Norm(Step(update, Uniform(), eps).Select(x => x - 1.0).ToArray()) < 1e-12;

    /// <summary>Steps until a cell goes negative - the simplex budget of a norm-preserving form.</summary>
    public static int StepsUntilOffSimplex(string update, double startCell, double eps, int horizon = 20000)
    {
        var state = (double[])Base().Clone();
        for (int k = 1; k <= horizon; k++)
        {
            state = Step(update, state, eps);
            if (state.Min() < 0.0) return k;
        }
        return -1;
    }

    public static (string Update, double PhaseAtStart, double PhaseAfter, int Steps) PhaseDecay(string update, double eps, int steps)
    {
        var start = Base();
        var end = Orbit(update, start, eps, steps);
        return (update, PhaseNorm(start), PhaseNorm(end), steps);
    }

    // ===================== 4b. THE GUARDS THE FINDINGS REST ON =====================

    /// <summary>The dissipative step's envelope: the deviation norm falls, and the phase-free state is the attractor.</summary>
    public static (int Steps, double Start, double End) DeviationEnvelope(string update, double eps, int steps)
        => (steps, Norm(Base().Select(x => x - 1.0).ToArray()),
            Norm(Orbit(update, Base(), eps, steps).Select(x => x - 1.0).ToArray()));

    public static bool TheDissipativeFlowContractsTowardThePhaseFreeState()
    {
        var (_, start, end) = DeviationEnvelope("forward difference", 1e-3, 20000);
        return end < start / 2.0 && Orbit("forward difference", Base(), 1e-3, 20000).Min() > 0.0;
    }

    /// <summary>
    /// The phase content OSCILLATES rather than decaying monotonically: the measured value at 1000 steps EXCEEDS the
    /// one at 100. A first draft asserted a plain fall and this is the measurement that refused it.
    /// </summary>
    public static bool ThePhaseContentOscillatesOnTheWayDown()
        => PhaseNorm(Orbit("forward difference", Base(), 1e-3, 1000))
         > PhaseNorm(Orbit("forward difference", Base(), 1e-3, 100))
        && DeviationEnvelope("forward difference", 1e-3, 1000).End
         < DeviationEnvelope("forward difference", 1e-3, 100).End;

    /// <summary>The unitary form conserves the deviation norm exactly - that is what |mu| = 1 means for every channel.</summary>
    public static bool TheUnitaryFormConservesTheDeviationNorm(double eps = 1e-3)
    {
        double start = Norm(Base().Select(x => x - 1.0).ToArray());
        return new[] { 100, 1000, 20000 }.All(n =>
            Math.Abs(Norm(Orbit(SustainingForm, Base(), eps, n).Select(x => x - 1.0).ToArray()) - start) < 1e-10);
    }

    public static bool TheUnitaryFormKeepsTheSimplex(int steps = 20000)
        => Orbit(SustainingForm, Base(), 1e-3, steps).Min() > 0.0;

    public static bool TheUnitaryFormKeepsTheSum(int steps = 20000)
        => Math.Abs(Orbit(SustainingForm, Base(), 1e-3, steps).Sum() - Cells) < 1e-9;

    /// <summary>The AT-native positivity guard never fires at these step sizes: its orbit is the unclipped one.</summary>
    public static bool TheClippedUpdateNeverFires(int steps = 4000)
    {
        var clipped = Orbit("positivity-clipped difference", Base(), 1e-3, steps);
        var plain = Orbit("forward difference", Base(), 1e-3, steps);
        return clipped.Zip(plain, (a, b) => Math.Abs(a - b)).Max() < 1e-12;
    }

    public static bool TheConstantIsFixedByEveryForm()
        => Updates().All(u => FixedPointDimension(u.Update) >= 1);

    // ===================== 5. VERDICT =====================

    public static bool AnyUpdateGeneratesPhaseDynamics()
        => Updates().Where(u => !u.IsControl).Any(u => ReachablePhaseRank(u.Update) > 0);

    public static bool AnyStableUpdateSustainsThePhase()
        => Updates().Where(u => !u.IsControl).Any(u =>
            StabilitySplit(u.Update, 1e-3).Sustained > 0 && SpectralRadius(u.Update, 1e-3) <= 1.0 + 1e-12);

    public const string SustainingForm = "unitary (Cayley of the skew part)";

    public static bool TheSustainingFormKeepsTheSimplex()
        => StepsUntilOffSimplex(SustainingForm, 0.0, 1e-3) < 0;

    public static string ThePromotionThatWorks()
    {
        var ok = Updates().Where(u => !u.IsControl)
            .Where(u => StabilitySplit(u.Update, 1e-3).Sustained > 0)
            .Select(u => u.Update).ToArray();
        return ok.Length == 0 ? "none" : string.Join(", ", ok);
    }

    /// <summary>
    /// Computed. DERIVED: a stable, simplex-respecting AT-native update sustains the phase. BOUNDARY: one sustains it
    /// but not the other. REFUTED: no update generates phase dynamics at all.
    /// </summary>
    public static string Verdict()
    {
        if (!AnyUpdateGeneratesPhaseDynamics()) return "REFUTED";
        if (!AnyStableUpdateSustainsThePhase()) return "BOUNDARY";
        if (!TheSustainingFormKeepsTheSimplex()) return "BOUNDARY";
        return "DERIVED";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("EVERY CANDIDATE IS CIRCULANT, SO NOTHING HERE IS ESTIMATED. The difference is a function of the shift, so each update is diagonal in the Fourier basis with one complex multiplier per channel, and the audit drives both its analysis and its iteration from that number - real 2x2 blocks on the quadrature pairs, no sign convention left to guess. The reachable rank is computed twice, per channel and by a Krylov scan, and the routes agree at 42. ");
        sb.Append("THE QUESTION'S OWN UPDATE IS STABLE, AND THAT IS WHY IT FAILS. rho + eps*D*rho has |mu|^2 = 1 - 2 eps x(1 - eps) with x = 1 - cos delta_c, so it CONTRACTS for 0 < eps < 1: the difference's symmetric part is negative semi-definite, which makes this a DISSIPATIVE step and not the skew step its name suggests. Its attractor is the kernel of D, and the kernel is the CONSTANT. ");
        var (steps, start, end) = DeviationEnvelope("forward difference", 1e-3, 20000);
        sb.Append($"Measured over {steps} steps the deviation norm falls from {start:E3} to {end:E3} and the state is driven to the PHASE-FREE point: THE STABLE DIFFERENCE FLOW IS ATTRACTED EXACTLY TO THE STATE WITH NO PHASE CONTENT, which is the closure with G_059 - the canonical state has no phase content because this is where the flow stops. ");
        sb.Append($"THE PHASE DOES NOT DECAY MONOTONICALLY, AND A DRAFT CLAIM IS WITHDRAWN. PhaseNorm at 1000 steps ({PhaseNorm(Orbit("forward difference", Base(), 1e-3, 1000)):E3}) EXCEEDS its value at 100 ({PhaseNorm(Orbit("forward difference", Base(), 1e-3, 100)):E3}) while the envelope keeps falling, so the phase content OSCILLATES on the way down: the audit reports the deviation norm as the envelope and the phase content as the oscillation. ");
        sb.Append("THE PHASE SURVIVES ONLY WHERE |mu| = 1, AND EXACTLY ONE FORM HAS IT. ");
        foreach (var eps in Epsilons())
            sb.Append($"at eps = {eps:E0}: " + string.Join("; ", Updates().Where(u => !u.IsControl).Select(u =>
            {
                var (decaying, sustained, growing) = StabilitySplit(u.Update, eps);
                return $"{u.Update} decays {decaying}, sustains {sustained}, grows {growing} (radius {SpectralRadius(u.Update, eps):F6})";
            })) + ". ");
        sb.Append($"The backward and centred differences leave the simplex in a measured number of steps; the difference's own exact flow exp(eps D) contracts like the forward step; and the UNITARY form - the exact flow of the CENTRED difference - has |mu| = 1 exactly, sustains all {StabilitySplit(SustainingForm, 1e-3).Sustained} phase directions, conserves the deviation norm exactly ({TheUnitaryFormConservesTheDeviationNorm()}) and the sum ({TheUnitaryFormKeepsTheSum()}), and keeps every cell positive over 20000 steps ({TheUnitaryFormKeepsTheSimplex()}). ");
        sb.Append($"THE RANK IS BOUNDED BY THE SUBSTRATE AT 42 OF 53 AND THE ELEVEN THAT REMAIN ARE STRUCTURALLY UNREACHABLE. A circulant update cannot mix a quadrature pair with the constant, so from a phase-free state it can only reach a channel's OTHER quadrature: the {PhaseBearingChannels().Length} half-hidden channels give {ReachablePhaseRank(SustainingForm)}, and the rest are the DOUBLY-HIDDEN channels - which carry no visible quadrature at all, so a phase-free state has nothing there to act on - plus the ALTERNATING MODE, a channel with no partner quadrature that every circulant merely rescales. No update of this family can reach them, whatever its step size. ");
        sb.Append($"THE AT-NATIVE POSITIVITY GUARD IS MEASURED AND IT NEVER FIRES: the clipped variant is numerically identical to the unclipped step ({TheClippedUpdateNeverFires()}), because dissipation keeps the state inside the simplex. A guard that never fires is reported rather than counted as a mechanism. ");
        sb.Append($"WHERE IT STANDS: the promotion that works is {ThePromotionThatWorks()}; the form the question names is the one that fails, because stability and phase survival pull in opposite directions here - the dissipative step keeps the simplex and destroys the phase, and the norm-preserving step keeps the phase and is the only one of the family that does.");
        return sb.ToString();
    }

    // ===================== 6. REPORTS =====================

    public static string OutputMultipliers()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE MULTIPLIERS - every candidate is circulant, so one complex number per channel is the whole update");
        sb.AppendLine("   update                            | kind                            | |mu| at eps = 1E-3 (min .. max)");
        foreach (var (update, kind, _) in Updates())
        {
            var moduli = Channels().Select(c => Multiplier(update, c, 1e-3).Magnitude).ToArray();
            sb.AppendLine($"   {update,-33} | {kind,-31} | {moduli.Min():F9} .. {moduli.Max():F9}");
        }
        sb.AppendLine("   the difference's own multiplier is 1 - e^(i delta_c): its symmetric part is cos delta_c - 1 =< 0");
        return sb.ToString();
    }

    public static string OutputRanks()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE RANKS");
        sb.AppendLine($"   phase dimension of the state space : {PhaseDimension()}");
        sb.AppendLine("   update                            | reachable phase | by Krylov | reachable amplitude | unreachable");
        foreach (var (update, _, _) in Updates())
        {
            int reach = ReachablePhaseRank(update);
            int krylov = ReachablePhaseRankByKrylov(update);
            int amp = ReachableAmplitudeRank(update);
            sb.AppendLine($"   {update,-33} | {reach,15} | {krylov,9} | {amp,19} | {PhaseDimension() - reach,11}");
        }
        sb.AppendLine("   the unreachable directions:");
        foreach (var name in UnreachablePhaseDirections()) sb.AppendLine($"     {name}");
        return sb.ToString();
    }

    public static string OutputStability()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. STABILITY AND THE FIXED POINTS");
        sb.AppendLine("   update                            | eps    | radius   | decay | sustain | grow | off-simplex after");
        foreach (var (update, _, _) in Updates())
            foreach (var eps in Epsilons())
            {
                var (decaying, sustained, growing) = StabilitySplit(update, eps);
                int off = StepsUntilOffSimplex(update, 0.0, eps, 4000);
                sb.AppendLine($"   {update,-33} | {eps,6:E0} | {SpectralRadius(update, eps),8:F6} | {decaying,5} | {sustained,7} | {growing,4} | {(off < 0 ? "> 4000" : off.ToString()),17}");
            }
        sb.AppendLine();
        sb.AppendLine("   phase content over 400 steps at eps = 1E-3:");
        foreach (var (update, _, _) in Updates())
        {
            var (_, start, end, steps) = PhaseDecay(update, 1e-3, 400);
            sb.AppendLine($"     {update,-33} {start:E3} -> {end:E3} after {steps} steps");
        }
        sb.AppendLine();
        sb.AppendLine("   fixed points:");
        foreach (var (update, dim, neutral) in FixedPointTable())
            sb.AppendLine($"     {update,-33} exactly-fixed dimension {dim}, neutrally stable modes: {neutral}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the promotion that works : {ThePromotionThatWorks()}");
        sb.AppendLine($"   the sustaining form keeps the simplex : {TheSustainingFormKeepsTheSimplex()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
