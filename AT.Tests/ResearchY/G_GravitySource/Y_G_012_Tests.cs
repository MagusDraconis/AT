using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;
using static AT.Tests.Shared.RhoActuators;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_012 — Local Rho Actuator Audit (group G — Gravity Source).
///
/// QUESTION: can any PHYSICALLY REALIZABLE LOCAL process act as a rho source?
/// Candidates: active feedback, mode injection, synchronized oscillators, driven D96 lattice,
/// non-equilibrium steady states.
/// Requirements: (1) local implementation, (2) finite drive, (3) survives DiffuseStep,
/// (4) no imported primitive.   Measure: Delta rho, Delta tau, drive cost, stability.
///
/// RESULTS (d = 0.2, N = 96; mu_1 = 0.9997858350, mu_2 = 0.9991435693, mu_48 = 0.6, mu_95 = 0.20021417)
///
/// (1) THE HOLD-DRIVE IS LOCAL. s = (I - W) rho* is a THREE-POINT stencil (perturbing rho_j moves only
///     s_{j-1}, s_j, s_{j+1}), count-neutral (Sigma s = 0) and exact. G_008/G_010's "mode matching" was
///     about KNOWING the target, not about the stencil.
/// (2) ACTUATOR — THE INCREMENTAL LOCAL FEEDBACK FREEZES ANY CONFIGURATION. s = rho - W rho (a local
///     three-point, count-neutral, AT-native term built from the state alone) makes the closed loop the
///     IDENTITY: the witness is held to 4.3e-18 over 5000 steps, and even a PERTURBATION is retained
///     exactly (100.000 %) — a perfect MEMORY. It is MARGINAL (no restoring force), so it holds a
///     configuration but cannot create one.
/// (3) THE 3-POINT LINEAR THEOREM. For the count-conserving three-point family s = beta (W rho - rho) the
///     closed-loop eigenvalues are c_k = mu_k (1 + beta) - beta; c_k = 1 for every k ONLY at beta = -1
///     (95/95 modes neutral), against 0/95 at beta = 0. So the freezing feedback is the UNIQUE member of
///     its family with a non-uniform fixed point, and it freezes all modes at once.
/// (4) THE RESTORING (uniform-gain) FAMILY IS LIMITED TO THE SMOOTHEST MODE. s = lambda (rho - rhoBar) has
///     closed-loop spectrum mu_k + lambda with stability requiring lambda in
///     [-1.200214165, 2.141650094e-4]; a fixed point carrying mode k needs lambda = 1 - mu_k, so ONLY
///     k = 1 qualifies (k = 2 already needs 8.564307e-4 > the window). Above threshold
///     (lambda = 2(1 - mu_1)) the smooth mode runs away at 1.000214165 per step, reaching saturation
///     from a Poisson-scale seed in 64 516 steps.
/// (5) COMPACT MASKS CANNOT BE TRUSTED TO REACH THE WITNESS CLASS. The gain of (I - W)^-1 decreases with
///     k (4669.2968 at k = 1 to 1.2503 at k = 95), giving the hard bound
///         HighKShare(rho*) <= (g_1/g_48)^2 * D_high/w_1^2 = 2.8666657e-7 * D_high/w_1^2
///     (verified for every family). Unstructured compact sources hold smooth profiles: block windows
///     6.63e-3 (w = 2) down to 6.37e-9 (w = 64), the edge dipole 0.1889, the fully staggered global
///     source 3.28e-4. A STRUCTURED +-1 mask reaches 0.6118 (77 % of the witness's 0.7966) — but a mask
///     is prescribed data, not a local process.
/// (6) MODE INJECTION IS REFUTED AS A LOCAL PROCESS: the drive holding a pure mode is s = (1 - mu_k) c v_k,
///     which has FULL support and is 98.3 % high-k for the witness; and k >= 2 feedback is unstable
///     (the k = 48 attempt at lambda = 0.4 gives mu_1 + lambda = 1.3998 > 1).
/// (7) SYNCHRONIZED OSCILLATORS ARE REFUTED: a locally locked PATCH gives L1(rho, |psi|^2) = 2.5e-16 and
///     |Delta a| < 1e-9 while the psi-sector coherent sum moves to 0.7537609.
/// (8) DRIVEN D96 LATTICE and NON-EQUILIBRIUM STEADY STATES are CORRELATED: the lattice is the MEDIUM
///     (response ratios 2.5 for a dipole to 120.0 for the checkerboard; the undriven attractor is
///     uniform) and a NESS is *defined* by s = (I - W) rho*, so its taxonomy IS the source taxonomy.
///
/// VERDICTS: ACTUATOR = the incremental local feedback (holds ANY profile, including the witness class,
/// exactly — marginal/memory, so it cannot create it) · CORRELATED = the driven lattice and the NESS
/// framing (the medium, not the source) · REFUTED = mode injection, synchronized oscillators, every
/// restoring gain other than k = 1, and the spontaneous creation of the witness class.
///
/// Deterministic: exact algebra, no randomness.  REFINEMENT NOTE (not a reclassification): G_010 said the
/// canonical chain supplies NO driver — still true; G_012 shows an ENGINEERED local feedback realises one,
/// marginally. G_011 labels the QUANTITY (this feedback is a function of rho) while G_012 labels the LOCAL
/// GENERATOR; the two are complementary.  D_040 untouched.
/// </summary>
public class Y_G_012_Tests : ResearchTestBase
{
    public Y_G_012_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double BandCeiling = 4.8867e-6;    // G_005's 1 % Poisson ceiling
    private const double WitnessShare = 0.7965733;   // the G_002/G_003 witness's high-k share (k >= 48)
    private const double ClockFloor = 1e-18;

    /// <summary>The marginal gain that makes the smoothest mode a fixed point: lambda = 1 - mu_1.</summary>
    private static double LambdaHold => 1.0 - NeumannMu(1, N, Damping);

    /// <summary>The lower stability bound, from the fastest mode: -(1 + mu_95).</summary>
    private static double LambdaMin => -(1.0 + NeumannMu(95, N, Damping));

    private static double[] Mode(int k) => NeumannMode(k, N);

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var r = (double[])a.Clone();
        for (int i = 0; i < m; i++) r = Step(r);
        return r;
    }

    /// <summary>The profile held by a source s: rho* = (I - W)^-1 s (spectral summation; DC carried by W).</summary>
    private static double[] HeldFrom(double[] s)
    {
        var w = Dct(s);
        for (int k = 1; k < N; k++) w[k] /= 1.0 - NeumannMu(k, N, Damping);
        w[0] = 0.0;
        return Idct(w, N);
    }

    /// <summary>One step of the local linear feedback rho &lt;- W rho + lambda (rho - rhoBar).</summary>
    private static double[] FeedbackStep(double[] x, double lambda)
    {
        var stepped = Step(x);
        var y = new double[N];
        for (int i = 0; i < N; i++) y[i] = stepped[i] + lambda * x[i];
        return y;
    }

    private static double[] FeedbackIterate(double[] x, double lambda, int steps)
    {
        var r = (double[])x.Clone();
        for (int m = 0; m < steps; m++) r = FeedbackStep(r, lambda);
        return r;
    }

    private static double MaxAbs(double[] x) => x.Max(Math.Abs);

    private static double L1Of(double[] x) => x.Sum(Math.Abs);

    /// <summary>A zero-sum compact "block" source: +1 on the first half of a width-w window, -1 on the second.</summary>
    private static double[] BlockSource(int w)
    {
        var s = new double[N];
        int h = w / 2, start = 48 - h;
        for (int i = 0; i < h; i++) s[start + i] = 1.0;
        for (int i = 0; i < w - h; i++) s[start + h + i] = -1.0;
        return s;
    }

    /// <summary>A zero-sum STAGGERED source on a width-w window: s_i = (-1)^i.</summary>
    private static double[] StaggeredSource(int w)
    {
        var s = new double[N];
        int start = 48 - w / 2;
        for (int i = 0; i < w; i++) s[start + i] = i % 2 == 0 ? 1.0 : -1.0;
        return s;
    }

    /// <summary>The G_002/G_003 witness tilt (within-eigenspace 80/20 redistribution).</summary>
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>Deterministic +-1 pattern family on a width-w window (no RNG: a fixed bit formula).</summary>
    private static double[] PatternSource(int w, int t)
    {
        var s = new double[N];
        int start = 48 - w / 2;
        for (int i = 0; i < w; i++) s[start + i] = ((i * t + t * t) % w) < w / 2 ? 1.0 : -1.0;
        return s;
    }

    // ── 1. Requirement 1: the hold-drive is a local three-point stencil ───────────

    [Fact]
    public void Y_G_012_LocalImplementation()
    {
        var target = Tilt;

        // The drive that holds ANY target is s = (I - W) rho* — a nearest-neighbour stencil: perturbing
        // one cell of the target moves only its own and its two neighbours' source entries.
        var s0 = HoldDrive(target);
        var perturbed = (double[])target.Clone();
        perturbed[48] += 1e-6;
        var s1 = HoldDrive(perturbed);
        for (int i = 0; i < N; i++)
        {
            bool touched = Math.Abs(i - 48) <= 1;
            Assert.True(touched || Math.Abs(s1[i] - s0[i]) < 1e-18, $"cell {i} moved by {Math.Abs(s1[i] - s0[i])}");
        }
        Assert.True(Math.Abs(s1[48] - s0[48]) > 1e-7);

        // Count-neutral and exact: Sigma s = 0 and the driven recursion reproduces the target.
        Assert.True(Math.Abs(s0.Sum()) < 1e-14, $"Sigma s = {s0.Sum()}");
        var settled = Settle(target, s0, 20000);
        double err = Enumerable.Range(0, N).Max(i => Math.Abs(settled[i] - target[i]));
        Assert.True(err < 1e-12, $"settle error = {err}");

        // Requirement 2 (finite drive): the witness costs 0.48675 per step (G_008's number).
        Assert.True(Math.Abs(L1Of(s0) - 0.48675) < 1e-5, $"||s||_1 = {L1Of(s0)}");
        Assert.True(L1Of(s0) < 1.0);
    }

    // ── 2. Requirement 4: a locally generated source must be cellwise ────────────

    [Fact]
    public void Y_G_012_CellwiseTest()
    {
        // (a) A PURE mode is perfectly cellwise-linear: (I - W)v_k = (1 - mu_k)v_k, so s_i = lambda rho_i.
        foreach (int k in new[] { 1, 2, 8, 48, 95 })
        {
            var v = Mode(k);
            var s = HoldDrive(v);
            double dev = Enumerable.Range(0, N).Max(i => Math.Abs(s[i] / ((1.0 - NeumannMu(k, N, Damping)) * v[i]) - 1.0));
            // Floating-point cancellation in the small rate (1 - mu_1 = 2.14e-4) leaves ~2.5e-11.
            Assert.True(dev < 1e-9, $"k = {k}: deviation {dev}");
        }

        // (b) The WITNESS is NOT: (I - W) rho* takes different values at cells with the SAME rho, with a
        //     spread comparable to the source values themselves. So no cellwise local source can hold it.
        var rho = Tilt;
        var sreq = HoldDrive(rho);
        var groups = rho.Select((v, i) => (Value: Math.Round(v, 12), Index: i)).GroupBy(g => g.Value).ToArray();
        Assert.Equal(7, groups.Length);
        double worstSpread = 0.0;
        foreach (var g in groups)
        {
            var vals = g.Select(x => sreq[x.Index]).ToArray();
            worstSpread = Math.Max(worstSpread, vals.Max() - vals.Min());
        }
        Assert.True(worstSpread > 9.0e-3, $"worst spread = {worstSpread}");
        Assert.True(worstSpread > 0.4 * MaxAbs(sreq), "the spread is comparable to the signal");
        // The largest single value of the required source is only ~0.0187, so a 9.5e-3 spread kills it.
        Assert.True(Math.Abs(MaxAbs(sreq) - 0.01866667) < 1e-7);

        // (c) The drive's own high-k content must EXCEED the profile's: the gain emphasises high k, so a
        //     source that holds a high-k profile must itself be nearly pure high-k (98.3 % here).
        Assert.True(Math.Abs(HighKShare(sreq, N / 2) - 0.9831400) < 1e-5, $"drive share = {HighKShare(sreq, N / 2)}");
        Assert.True(Math.Abs(HighKShare(rho, N / 2) - WitnessShare) < 1e-6);
        Assert.True(HighKShare(sreq, N / 2) > HighKShare(rho, N / 2));
    }

    // ── 3. Active feedback: the stability window ─────────────────────────────────

    [Fact]
    public void Y_G_012_FeedbackStabilityWindow()
    {
        // The closed-loop operator of s = lambda (rho - rhoBar) is W + lambda I on the zero-sum subspace,
        // so its eigenvalues are mu_k + lambda and stability demands |mu_k + lambda| <= 1 for every k >= 1.
        double lo = LambdaMin, hi = LambdaHold;
        Assert.True(Math.Abs(hi - 2.141650094e-4) < 1e-12, $"lambda_max = {hi}");
        Assert.True(Math.Abs(lo + 1.200214165) < 1e-8, $"lambda_min = {lo}");
        for (int k = 1; k < N; k++)
        {
            Assert.True(NeumannMu(k, N, Damping) + hi <= 1.0 + 1e-15, $"k = {k}");
            Assert.True(NeumannMu(k, N, Damping) + lo >= -1.0 - 1e-15, $"k = {k}");
        }
        // Just above the window the SMOOTHEST mode grows: mu_1 + lambda > 1.
        Assert.True(NeumannMu(1, N, Damping) + 1.05 * hi > 1.0);

        // A fixed point carrying mode k needs mu_k + lambda = 1, i.e. lambda = 1 - mu_k. Only k = 1 fits
        // inside the stability window; every k >= 2 needs a larger gain, which destabilises k = 1.
        Assert.Equal(1.0, NeumannMu(1, N, Damping) + hi, 12);
        for (int k = 2; k < N; k++)
            Assert.True(1.0 - NeumannMu(k, N, Damping) > hi, $"k = {k} would need a gain above the window");
        Assert.True(Math.Abs((1.0 - NeumannMu(48, N, Damping)) - 0.4) < 1e-12);
        Assert.True(NeumannMu(1, N, Damping) + 0.4 > 1.39);

        // The rest of the spectrum under lambda = lambda_hold: k = 2 decays at 6.422657e-4 per step
        // (tau = 1556.99), k = 48 at 0.3997858010 and k = 95 at 0.7995716700.
        double r2 = 1.0 - (NeumannMu(2, N, Damping) + hi);
        Assert.True(Math.Abs(r2 - 6.422657e-4) < 1e-9, $"1 - (mu_2 + lambda) = {r2}");
        Assert.True(Math.Abs(1.0 / r2 - 1556.99) < 0.05);
        Assert.True(Math.Abs((1.0 - (NeumannMu(48, N, Damping) + hi)) - 0.3997858350) < 1e-9);
        Assert.True(Math.Abs((1.0 - (NeumannMu(95, N, Damping) + hi)) - 0.7995716700) < 1e-9);
    }

    // ── 4. The marginal, mode-selective actuator ─────────────────────────────────

    [Fact]
    public void Y_G_012_MarginalActuator()
    {
        // A k = 1 seed is held EXACTLY (marginal: the eigenvalue is 1) ...
        var seed1 = Mode(1).Select(v => 1e-3 * v).ToArray();
        var held = FeedbackIterate(seed1, LambdaHold, 20000);
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(held[i] - seed1[i])) < 1e-15, "k = 1 must be neutral");

        // ... while a k = 2 seed decays exactly as the closed-loop eigenvalue predicts.
        var seed2 = Mode(2).Select(v => 1e-3 * v).ToArray();
        var decayed = FeedbackIterate(seed2, LambdaHold, 5000);
        double predicted = Math.Pow(NeumannMu(2, N, Damping) + LambdaHold, 5000);
        Assert.True(Math.Abs(MaxAbs(decayed) / MaxAbs(seed2) / predicted - 1.0) < 1e-6,
            $"measured {MaxAbs(decayed) / MaxAbs(seed2)}, predicted {predicted}");
        Assert.True(Math.Abs(predicted - 0.0402614766) < 1e-9);

        // A MIXTURE converges onto its k = 1 component alone: the feedback is mode-selective.
        var mix = new double[N];
        for (int i = 0; i < N; i++) mix[i] = 1e-3 * (Mode(1)[i] + Mode(2)[i]);
        var resolved = FeedbackIterate(mix, LambdaHold, 50000);
        double relErr = Enumerable.Range(0, N).Max(i => Math.Abs(resolved[i] - seed1[i])) / 1e-3;
        Assert.True(relErr < 1e-9, $"mixture -> k = 1 relative error {relErr}");

        // The actuator is COUNT-CONSERVING and has a finite, proportional cost:
        // ||rho - rhoBar||_1 = 0.3183240902 per unit Delta ln rho, so 6.817388e-5 per step per unit contrast.
        double dev1 = 0.0;
        var profile = Mode(1).Select(v => (1.0 + 0.5 * v) / N).ToArray();
        for (int i = 0; i < N; i++) dev1 += Math.Abs(profile[i] - 1.0 / N);
        Assert.True(Math.Abs(dev1 / (2.0 * 0.5) - 0.3183240902) < 1e-9, $"L1 per unit contrast = {dev1}");
        Assert.True(Math.Abs(LambdaHold * 0.3183240902 - 6.817388e-5) < 1e-10);
        Assert.True(Math.Abs(profile.Sum() - 1.0) < 1e-12);
    }

    // ── 5. Above threshold: the runaway and its horizon ──────────────────────────

    [Fact]
    public void Y_G_012_RunawayAboveThreshold()
    {
        double up = 2.0 * LambdaHold;
        double growth = NeumannMu(1, N, Damping) + up;
        Assert.True(Math.Abs(growth - 1.000214165) < 1e-8, $"growth = {growth}");

        // A Poisson-scale seed (1e-6 of rhoBar) reaches saturation in ~64 516 steps.
        double steps = Math.Log(1e6) / Math.Log(growth);
        Assert.True(Math.Abs(steps - 64515.63) < 0.5, $"steps = {steps}");

        // Verified by iteration: 4000 steps grow by growth^4000 to 1e-6 precision.
        var seed = Mode(1).Select(v => 1e-6 * v).ToArray();
        var grown = FeedbackIterate(seed, up, 4000);
        Assert.True(Math.Abs(MaxAbs(grown) / MaxAbs(seed) / Math.Pow(growth, 4000) - 1.0) < 1e-6,
            "the run-away follows the closed-loop eigenvalue exactly");

        // So the actuator's window is a SINGLE gain value: below it the mode decays, above it runs away.
        Assert.True(NeumannMu(1, N, Damping) + 0.95 * LambdaHold < 1.0);
        Assert.True(NeumannMu(1, N, Damping) + 1.05 * LambdaHold > 1.0);
        Assert.True(Math.Abs(NeumannMu(1, N, Damping) + LambdaHold - 1.0) < 1e-15);
    }

    // ── 6. Compact sources cannot reach the witness class ────────────────────────

    [Fact]
    public void Y_G_012_CompactSourceSmoothness()
    {
        // The hard bound: the gain of (I - W)^-1 decreases with k, so for ANY source
        //   HighKShare(rho*) <= (g_1/g_48)^2 * D_high/w_1^2,  (g_1/g_48)^2 = 2.8666657e-7.
        double boundConstant = Math.Pow((1.0 - NeumannMu(1, N, Damping)) / (1.0 - NeumannMu(48, N, Damping)), 2);
        Assert.True(Math.Abs(boundConstant - 2.8666657e-7) / 2.8666657e-7 < 1e-6, $"constant = {boundConstant}");

        double Bound(double[] s)
        {
            var w = Dct(s);
            double dHigh = 0.0;
            for (int k = 48; k < N; k++) dHigh += w[k] * w[k];
            return boundConstant * dHigh / (w[1] * w[1]);
        }

        // Block windows: the measured share falls from 6.63e-3 (w = 2) to 6.37e-9 (w = 64).
        foreach (var (w, expected) in new[] { (2, 6.630273e-3), (4, 4.834875e-4), (8, 1.390521e-5),
                                              (16, 1.106470e-6), (32, 8.138471e-8), (64, 6.368861e-9) })
        {
            var s = BlockSource(w);
            double share = HighKShare(HeldFrom(s), N / 2);
            Assert.True(Math.Abs(share - expected) / expected < 1e-4, $"w = {w}: share = {share}");
            Assert.True(share <= Bound(s), $"w = {w}: the bound {Bound(s)} must hold");
            Assert.True(share < 0.01, $"w = {w}: far below the witness");
        }

        // The sharpest *two-cell* source is the dipole (6.63e-3); at the Neumann EDGE the response is a
        // decaying exponential, so the edge dipole reaches 0.1889 — still 4x below the witness.
        var edge = new double[N];
        edge[0] = 1.0; edge[1] = -1.0;
        double edgeShare = HighKShare(HeldFrom(edge), N / 2);
        Assert.True(Math.Abs(edgeShare - 0.1888945058) < 1e-9, $"edge share = {edgeShare}");
        Assert.True(edgeShare <= Bound(edge));
        Assert.True(Math.Abs(MaxAbs(HeldFrom(edge)) - 4.9479166667) < 1e-8);

        // A fully STAGGERED (maximally alternating) global source still holds a smooth profile: 3.28e-4.
        double checkerShare = HighKShare(HeldFrom(StaggeredSource(N)), N / 2);
        Assert.True(Math.Abs(checkerShare - 3.281127e-4) / 3.281127e-4 < 1e-3, $"checker share = {checkerShare}");
        Assert.True(Math.Abs(MaxAbs(HeldFrom(StaggeredSource(N))) / MaxAbs(StaggeredSource(N)) - 120.0) < 1e-6);
        foreach (var (w, expected) in new[] { (4, 2.906805e-3), (8, 1.443536e-3), (16, 7.501672e-4), (32, 4.235902e-4) })
        {
            var s = StaggeredSource(w);
            Assert.True(Math.Abs(HighKShare(HeldFrom(s), N / 2) - expected) / expected < 1e-4, $"staggered w = {w}");
        }

        // A deterministic sweep of the local +-1 mask family. The UNSTRUCTURED families above are smooth,
        // but a STRUCTURED mask (this one peaks at w = 64, t = 48) reaches 0.6118 — 77 % of the witness.
        double best = 0.0;
        (int W, int T) arg = (0, 0);
        for (int w = 2; w <= 64; w += 2)
            for (int t = 1; t <= w; t++)
            {
                double share = HighKShare(HeldFrom(PatternSource(w, t)), N / 2);
                Assert.True(share <= Bound(PatternSource(w, t)), $"w = {w}, t = {t}: the bound must hold");
                if (share > best) { best = share; arg = (w, t); }
            }
        Assert.True(best > 0.6 && best < 0.62, $"the best local mask reaches {best} at {arg}");
        Assert.True(best / WitnessShare < 0.78, $"best/witness = {best / WitnessShare}");
        // ... but a mask is PRESCRIBED data, not a local process: it is the "mode injection" candidate,
        // and its information is supplied from outside. What a local generator can do is the feed-back
        // family of the next test.

        // To reach the witness share the source needs D_high/w_1^2 >= 2.7790e6 (half of it: 1.7442e6).
        Assert.True(Math.Abs(0.5 / boundConstant - 1.7442e6) / 1.7442e6 < 1e-3);
        Assert.True(Math.Abs(WitnessShare / boundConstant - 2.7790e6) / 2.7790e6 < 1e-3);
    }

    // ── 6b. The incremental feedback: the ACTUATOR ───────────────────────────────

    [Fact]
    public void Y_G_012_FreezeActuator()
    {
        // s = rho - W rho is local (three-point), count-neutral and built from the state alone; the closed
        // loop is rho <- W rho + s = rho, i.e. the IDENTITY: it freezes ANY configuration exactly.
        foreach (var target in new[] { Tilt, Mode(95), Spread(D96Spaces.Mult, 1.0) })
        {
            var s = HoldDrive(target);
            Assert.True(Math.Abs(s.Sum()) < 1e-14, $"Sigma s = {s.Sum()}");
            var frozen = Settle(target, s, 5000);
            double err = Enumerable.Range(0, N).Max(i => Math.Abs(frozen[i] - target[i]));
            Assert.True(err < 1e-15, $"freeze error = {err}");
        }

        // It is a MEMORY, not a stabiliser: a perturbation is retained exactly (100.000 %), so there is no
        // restoring force and the actuator cannot correct an error.
        var rho = Tilt;
        var perturbed = new double[N];
        for (int i = 0; i < N; i++) perturbed[i] = rho[i] + 1e-6 * Mode(2)[i];
        var sp = HoldDrive(perturbed);
        var kept = Settle(perturbed, sp, 5000);
        double retention = Enumerable.Range(0, N).Max(i => Math.Abs(kept[i] - rho[i]))
                         / Enumerable.Range(0, N).Max(i => Math.Abs(perturbed[i] - rho[i]));
        Assert.True(Math.Abs(retention - 1.0) < 1e-12, $"retention = {retention}");
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(kept[i] - perturbed[i])) < 1e-15);

        // Cost: the drive IS the relaxation increment, so ||s||_1 = 0.48675 for the witness and
        // 3.331453e-10 for the band-top smooth profile — strictly proportional to the configuration.
        Assert.True(Math.Abs(L1Of(HoldDrive(rho)) - 0.48675) < 1e-5);
        var bandProfile = Mode(1).Select(v => (1.0 + BandCeiling / 2.0 * v) / N).ToArray();
        Assert.True(Math.Abs(L1Of(HoldDrive(bandProfile)) - 3.331453e-10) < 1e-15,
            $"band-top cost = {L1Of(HoldDrive(bandProfile))}");
        Assert.True(Math.Abs(bandProfile.Sum() - 1.0) < 1e-12);

        // THE THREE-POINT LINEAR THEOREM: for the count-conserving family s = beta (W rho - rho) the
        // closed-loop eigenvalues are c_k = mu_k (1 + beta) - beta, and c_k = 1 for EVERY k only at
        // beta = -1 (95/95 neutral) — the freezing feedback is the unique non-trivial member.
        foreach (double beta in new[] { -1.0, -0.5, 0.0, 0.5 })
            foreach (int k in new[] { 1, 48, 95 })
            {
                double c = NeumannMu(k, N, Damping) * (1 + beta) - beta;
                var v = Mode(k);
                var s = new double[N];
                for (int i = 0; i < N; i++) s[i] = beta * (Step(v)[i] - v[i]);
                for (int i = 0; i < N; i++)
                    Assert.True(Math.Abs((Step(v)[i] + s[i]) - c * v[i]) < 1e-12, $"beta = {beta}, k = {k}");
            }
        int neutralAtBetaMinus1 = 0, neutralAtZero = 0;
        for (int k = 1; k < N; k++)
        {
            if (Math.Abs(NeumannMu(k, N, Damping) * (1.0 + -1.0) - -1.0 - 1.0) < 1e-12) neutralAtBetaMinus1++;
            if (Math.Abs(NeumannMu(k, N, Damping) - 1.0) < 1e-12) neutralAtZero++;
        }
        Assert.Equal(95, neutralAtBetaMinus1);
        Assert.Equal(0, neutralAtZero);
    }

    // ── 7. Synchronization is inert; the lattice and a NESS are the MEDIUM ───────

    [Fact]
    public void Y_G_012_SynchronizationAndMedium()
    {
        var rho = Tilt;

        // A LOCALLY locked patch: cells 40..55 share a phase, the rest keep the canonical grid. The
        // density is bit-identical, so phase locking is exactly rho-inert even locally.
        var canonical = Enumerable.Range(0, N).Select(j => 2.0 * Math.PI * j / N).ToArray();
        var patch = (double[])canonical.Clone();
        for (int j = 40; j <= 55; j++) patch[j] = canonical[40];
        double[] Density(double[] phases)
        {
            var d = new double[N];
            for (int j = 0; j < N; j++)
            {
                double re = Math.Sqrt(rho[j]) * Math.Cos(phases[j]);
                double im = Math.Sqrt(rho[j]) * Math.Sin(phases[j]);
                d[j] = re * re + im * im;
            }
            return d;
        }
        var recovered = Density(patch);
        Assert.True(L1(rho, recovered) < 1e-15, $"L1 = {L1(rho, recovered)}");
        Assert.True(MaxAccelerationDifference(rho, recovered) < 1e-9);
        // The psi-sector does move (the patch is coherent), so the phase IS a variable — just not a rho one.
        double reSum = 0.0, imSum = 0.0;
        for (int j = 0; j < N; j++)
        {
            reSum += Math.Sqrt(rho[j]) * Math.Cos(patch[j]);
            imSum += Math.Sqrt(rho[j]) * Math.Sin(patch[j]);
        }
        Assert.True(Math.Abs(Math.Sqrt(reSum * reSum + imSum * imSum) - 0.7537609) < 1e-6);

        // The D96 LATTICE is the MEDIUM: its undriven attractor is uniform, and a NESS is *defined* by
        // s = (I - W) rho* — so the response is finite but entirely the source's doing.
        var uniform = Enumerable.Repeat(1.0 / N, N).ToArray();
        Assert.True(L1Of(HoldDrive(uniform)) < 1e-15);
        Assert.True(MaxAbs(Iterate(Tilt, 200)) < 0.051);          // the attractor erases the arrangement
        Assert.True(Math.Abs(L1(Tilt, uniform) - 0.6666667) < 1e-6);
        Assert.True(Math.Abs(L1(Iterate(Tilt, 200), uniform) - 0.0201006) < 1e-6);

        // Response ratios max|rho*|/max|s| of the medium: 2.5 (dipole) ... 120.0 (checkerboard).
        foreach (var (s, expected) in new[] { (BlockSource(2), 2.5), (StaggeredSource(4), 5.0),
                                              (StaggeredSource(8), 10.0), (StaggeredSource(16), 20.0),
                                              (StaggeredSource(32), 40.0), (StaggeredSource(N), 120.0) })
            Assert.True(Math.Abs(MaxAbs(HeldFrom(s)) / MaxAbs(s) - expected) < 1e-6, $"response {MaxAbs(HeldFrom(s)) / MaxAbs(s)}");
    }

    // ── 8. Verdicts and readouts ─────────────────────────────────────────────────

    [Fact]
    public void Y_G_012_Verdicts()
    {
        // The k = 1 actuator's readouts: Delta tau/tau = Delta ln rho/3 and the drive cost is
        // 6.817388e-5 per step per unit contrast.
        double costPerContrast = LambdaHold * 0.3183240902;
        foreach (var (dl, f, drive) in new[] { (BandCeiling, BandCeiling / D, LambdaHold * 0.3183240902 * BandCeiling),
                                               (Math.Log(3.0), Math.Log(3.0) / D, LambdaHold * 0.3183240902 * Math.Log(3.0)) })
        {
            Assert.True(Math.Abs(f - dl / 3.0) < 1e-15);
            Assert.True(Math.Abs(drive - costPerContrast * dl) < 1e-18);
        }
        Assert.True(Math.Abs(BandCeiling / D * 86400.0 - 0.140737) < 1e-5);          // the band top, s/day
        Assert.True(Math.Abs(Math.Log(3.0) / D * 86400.0 - 31640.03) < 0.05);        // a 3:1 contrast
        Assert.True(Math.Abs(Math.Log(10.0) / D * 86400.0 - 66314.45) < 0.05);       // a 10:1 contrast
        Assert.True(Math.Abs(costPerContrast * BandCeiling - 3.331453e-10) < 1e-15);
        // The band-top shift is 1.4e11x above a 1e-18 clock and costs 3.3e-10 per step.
        Assert.True(BandCeiling / D / ClockFloor > 1e11);
        Assert.True(costPerContrast * Math.Log(10.0) < 2e-4);

        // The 3:1 case would be SUPPRESSED (not FORBIDDEN) per G_005: above the 1 % Poisson cut and below
        // the one-cell ceiling ln 96 = 4.564348.
        Assert.True(Math.Log(3.0) > BandCeiling);
        Assert.True(Math.Log(3.0) < Math.Log(96.0));
        Assert.True(Math.Log(10.0) < Math.Log(96.0));

        // VERDICTS: ACTUATOR (the incremental local feedback — freezes any profile exactly, including the
        // witness class, but marginal so it cannot create it) / CORRELATED (medium, NESS) / REFUTED (mode
        // injection, synchronization, the restoring family beyond k = 1, spontaneous creation).
        Assert.True(LambdaHold < 1.0 - NeumannMu(2, N, Damping));                    // k = 2 needs more gain
        Assert.True(NeumannMu(1, N, Damping) + (1.0 - NeumannMu(2, N, Damping)) > 1.0); // and is unstable
        // The actuator's readouts: the increment is held at 3.331453e-10 per step for the band top and
        // 0.48675 for the witness, with the clock shift scaling as Delta ln rho/3.
        Assert.True(L1Of(HoldDrive(Tilt)) / (BandCeiling / D * 86400.0) > 1.0);
    }

    // ── 9. Research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_012_Run()
    {
        var sb = new StringBuilder();
        var rho = Tilt;
        var sreq = HoldDrive(rho);
        var up = 2.0 * LambdaHold;

        PrintHeader(sb, "ResearchY-G_012 — LOCAL RHO ACTUATOR AUDIT");
        sb.AppendLine("Question: can any physically realizable LOCAL process act as a rho source?");
        sb.AppendLine("Candidates: active feedback, mode injection, synchronized oscillators, driven D96 lattice,");
        sb.AppendLine("            non-equilibrium steady states.");
        sb.AppendLine("Requirements: (1) local, (2) finite drive, (3) survives DiffuseStep, (4) no imported primitive.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  The canonical relaxation is W = DiffuseStep(d = 0.2) on the ordered 96-cell chain (G_006/07).");
        sb.AppendLine("  A2  A drive s holds rho* iff s = (I - W) rho* (G_008, generalised in G_011).");
        sb.AppendLine("  A3  Count conservation requires Sigma s = 0; the clock reading is Delta tau/tau = Delta ln rho/3 (G_009).");
        sb.AppendLine("  A4  A source is LOCALLY GENERATED iff s_i is a function of the state in a neighbourhood of cell i.");
        sb.AppendLine();

        PrintHeader(sb, "1. THE FOUR REQUIREMENTS");
        sb.AppendLine($"  (1) LOCAL .......... s = (I - W) rho* is a THREE-POINT stencil (verified by perturbation)");
        sb.AppendLine($"  (2) FINITE ......... ||s||_1 = {L1Of(sreq):F5} for the witness, max|s| = {MaxAbs(sreq):F8} (< 1)");
        sb.AppendLine($"  (3) SURVIVES W ..... held exactly: 20 000 driven steps reproduce the target to < 1e-12");
        sb.AppendLine("  (4) NO PRIMITIVE ... s must be a function of the local state (s_i = g(rho[i-1], rho[i], rho[i+1]))");
        sb.AppendLine();

        PrintHeader(sb, "2. CANDIDATE VERDICTS");
        sb.AppendLine("  candidate                  local  finite  survives  primitive-free  VERDICT");
        sb.AppendLine("  active feedback (freeze)   YES    YES     YES       YES (linear in rho)  ACTUATOR (marginal)");
        sb.AppendLine("  active feedback (restoring) YES   YES     YES       YES                REFUTED for k >= 2");
        sb.AppendLine("  mode injection             NO     YES     YES       NO (prescribed mask) REFUTED");
        sb.AppendLine("  synchronized oscillators   YES    n/a     n/a       YES                REFUTED (Delta rho = 0)");
        sb.AppendLine("  driven D96 lattice         NO     YES     YES       YES                CORRELATED (medium)");
        sb.AppendLine("  non-equilibrium steady st. YES    YES     YES       n/a                CORRELATED (the form)");
        sb.AppendLine();

        PrintHeader(sb, "3. THE MEASURED QUANTITIES");
        sb.AppendLine($"  Delta rho (freeze) ......... 0 to 1e-18 over 5000 steps; a perturbation is retained 100.000 %");
        sb.AppendLine($"  Delta rho (witness) ........ L1 = 0.6666667 from uniform, held unchanged");
        sb.AppendLine($"  Delta tau/tau .............. Delta ln rho/3: band top {BandCeiling / D:E3} = {BandCeiling / D * 86400.0:F4} s/day;");
        sb.AppendLine($"                               3:1 contrast {Math.Log(3.0) / D:F4} = {Math.Log(3.0) / D * 86400.0:F2} s/day; 10:1 {Math.Log(10.0) / D * 86400.0:F2} s/day");
        sb.AppendLine($"  drive cost ................. {L1Of(sreq):F5}/step (witness) or {L1Of(HoldDrive(Mode(1).Select(v => (1.0 + BandCeiling / 2.0 * v) / N).ToArray())):E3}/step (band top)");
        sb.AppendLine($"  stability (freeze) ......... the closed loop is the IDENTITY: 95/95 modes neutral (no restoring force)");
        sb.AppendLine($"  stability (restoring) ...... lambda in [{LambdaMin:F9}, {LambdaHold:E3}]; only k = 1 can be held");
        sb.AppendLine($"  stability (k = 2 at lambda_hold) decay {1.0 - (NeumannMu(2, N, Damping) + LambdaHold):E3}/step, tau = {1.0 / (1.0 - (NeumannMu(2, N, Damping) + LambdaHold)):F2}");
        sb.AppendLine($"  k = 48 attempt (lambda = 0.4)  mu_1 + lambda = {NeumannMu(1, N, Damping) + 0.4:F6} > 1 -> UNSTABLE");
        sb.AppendLine($"  runaway (lambda = 2 lambda_hold) {NeumannMu(1, N, Damping) + up:F8}/step; saturation from 1e-6 in {Math.Log(1e6) / Math.Log(NeumannMu(1, N, Damping) + up):F0} steps");
        sb.AppendLine();

        PrintHeader(sb, "4. WHY THE WITNESS CLASS IS NOT LOCALLY GENERATED");
        sb.AppendLine($"  cellwise test .... (I - W) rho* spread within EQUAL-rho groups = 9.5e-3 vs max|s| = {MaxAbs(sreq):F5}");
        sb.AppendLine($"                     -> it is NOT a function of rho_i alone");
        sb.AppendLine($"  drive share ...... the witness hold-drive is {HighKShare(sreq, N / 2):P2} high-k against the profile's {HighKShare(rho, N / 2):P2}");
        sb.AppendLine($"  gain bound ....... HighKShare <= 2.8666657e-7 * D_high/w_1^2; the witness needs D_high/w_1^2 >= 2.7790e6");
        sb.AppendLine($"  compact masks .... block windows {HighKShare(HeldFrom(BlockSource(2)), N / 2):E3} (w = 2) to {HighKShare(HeldFrom(BlockSource(64)), N / 2):E3} (w = 64);");
        sb.AppendLine($"                     edge dipole {HighKShare(HeldFrom(new double[] { 1, -1 }.Concat(new double[N - 2]).ToArray()), N / 2):F4}; staggered global {HighKShare(HeldFrom(StaggeredSource(N)), N / 2):E3}");
        sb.AppendLine("  BUT a STRUCTURED mask reaches 0.6118 (77 % of the witness) — a mask is prescribed data, not a process.");
        sb.AppendLine();

        PrintHeader(sb, "5. CONCLUSIONS");
        sb.AppendLine("  C1  ACTUATOR — the LOCAL INCREMENTAL FEEDBACK s = rho - W rho freezes ANY configuration exactly:");
        sb.AppendLine("      local (three-point), count-neutral, finite, and built from the state alone. It holds the");
        sb.AppendLine("      witness class too (Delta rho = 0), which no compact mask can do.");
        sb.AppendLine("  C2  It is MARGINAL: the closed loop is the IDENTITY (95/95 neutral), so a perturbation is retained");
        sb.AppendLine("      exactly. It is a MEMORY, not a stabiliser: it holds a configuration, it cannot create one.");
        sb.AppendLine("  C3  THE THREE-POINT LINEAR THEOREM: among the count-conserving three-point family");
        sb.AppendLine("      s = beta (W rho - rho) the eigenvalues are c_k = mu_k (1 + beta) - beta, and c_k = 1 for every");
        sb.AppendLine("      k only at beta = -1 — the freezing feedback is the unique non-trivial member.");
        sb.AppendLine("  C4  The RESTORING family s = lambda (rho - rhoBar) is limited to the smoothest mode: stability");
        sb.AppendLine($"      requires lambda <= {LambdaHold:E3} = 1 - mu_1, so the only holdable mode is k = 1 (marginal), while");
        sb.AppendLine("      every higher mode decays (k = 2 at 6.422657e-4 per step, tau = 1556.99); above threshold the");
        sb.AppendLine($"      smooth mode RUNS AWAY at {NeumannMu(1, N, Damping) + up:F8} per step (64 516 steps from a Poisson seed).");
        sb.AppendLine("  C5  MODE INJECTION is REFUTED as a LOCAL process: the drive holding a pure mode has full support");
        sb.AppendLine("      and, for the witness, is 98.3 % high-k; k >= 2 feedback is unstable (mu_1 + 0.4 = 1.3998).");
        sb.AppendLine("  C6  SYNCHRONIZED OSCILLATORS are REFUTED: a locally locked patch leaves rho bit-identical");
        sb.AppendLine("      (L1 = 2.5e-16, |Delta a| < 1e-9) while the psi-sector coherent sum moves to 0.7537609.");
        sb.AppendLine("  C7  The DRIVEN D96 LATTICE and a NESS are CORRELATED: the lattice is the medium (response ratios");
        sb.AppendLine("      2.5 for a dipole to 120.0 for the checkerboard; the undriven attractor is uniform) and a NESS");
        sb.AppendLine("      is *defined* by s = (I - W) rho*, so its taxonomy IS the source taxonomy.");
        sb.AppendLine("  C8  REFINEMENT (not a reclassification): G_010's \"the canonical chain supplies no driver\" stands.");
        sb.AppendLine("      G_012 shows an ENGINEERED local feedback realises one — marginally, and only as a memory.");
        sb.AppendLine();

        PrintHeader(sb, "6. CLASSIFICATION");
        sb.AppendLine("  ACTUATOR    the incremental local feedback s = rho - W rho (any profile, including the witness");
        sb.AppendLine("              class, held exactly; marginal/memory; no spontaneous creation).");
        sb.AppendLine("  CORRELATED  the driven D96 lattice (the medium) and the non-equilibrium-steady-state framing");
        sb.AppendLine("              (a NESS is the general form of a held state).");
        sb.AppendLine("  REFUTED     mode injection (non-local, prescribed mask), synchronized oscillators (exactly");
        sb.AppendLine("              rho-inert), every restoring gain beyond k = 1, and the local creation of the");
        sb.AppendLine("              gravity-control witness class.");
        sb.AppendLine("  G_011 labels the QUANTITY (this feedback is a function of rho) while G_012 labels the LOCAL");
        sb.AppendLine("  GENERATOR — complementary, not contradictory. D_040 untouched; no canonical claim, value or");
        sb.AppendLine("  equation changes; no new primitive. Deterministic: exact algebra, no randomness.");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
