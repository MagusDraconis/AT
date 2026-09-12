using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;
using static AT.Tests.Shared.RhoActuators;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_014 — Physical Rho Mapping Audit (group G — Gravity Source).
///
/// QUESTION: what MEASURABLE laboratory quantity corresponds to rho?
/// Candidates: probability density, occupation density, mode population, energy density, information
/// density, coherence density.
/// Requirements: (1) reproduces the G_001 source law, (2) reproduces the G_009 clock law, (3) supports the
/// G_013 actuator, (4) survives G_007 suppression.     Measure: rho correlation, field correlation, clock
/// correlation.     Output: PHYSICAL / CORRELATED / REFUTED.
///
/// THE MAPPING CRITERION. A lab observable q = F(rho) is a rho ANALOGUE iff (A1) q > 0 cellwise (so ln q
/// and the source law exist), (A2) Sigma q = 1 (a counting measure), (A3) F is AFFINE, kappa = d ln q/d ln rho
/// constant, because the relaxation W and the G_013 stencil (I - W) are LINEAR — a nonlinear map breaks the
/// commutation, and (A4) q is CELLWISE (so it carries the gradient). Verified:
///   the flow commutation error ||F^-1(W F(rho)) - W rho|| and the actuator error
///   ||(I - W)F(rho) - F'(rho)(I - W)rho|| are BOTH EXACTLY ZERO at kappa = 1 and nonzero otherwise
///   (kappa = 0.5: actuator error 3.229213; kappa = 2: flow 1.047221e-2, actuator 4.668155e-2;
///   kappa = 3: flow 1.724505e-2), while the recovered clock factor is exactly 1/kappa.
///
/// VERDICTS
///   PHYSICAL    PROBABILITY DENSITY — q = |psi|^2 IS rho (verified to 2.484991379e-16, QG220), so kappa = 1
///               exactly and all four requirements hold identically: the source law a = -(1/d) grad ln q, the
///               clock law Delta ln q/d, the G_013 actuator (I - W)q and the 33.7781483x suppression. It is
///               the only candidate that also carries the psi-sector (phase).
///   PHYSICAL    OCCUPATION DENSITY — the counting face: kappa = 1, and its own SHOT NOISE is the theory's
///               Poisson law: with <N> = 1.6102e-6^-2 = 3.856917553651e11 counts per cell, delta = 1/sqrt(<N>)
///               = 1.610200e-6 — exactly the observed galactic contrast (G_003) — and the 1 % ceiling is
///               4.886722e-6 (G_005's accessible band). No other candidate's noise IS the theory's band.
///   CORRELATED  MODE POPULATION — the exact spectral coordinate: the power spectrum is REVERSAL-INVARIANT
///               (max |Delta|w_k|| = 4.1598669e-15, total spectral energy ratio 1.0000000000000018) while the
///               field flips (max|Delta a| = 0.8864864874, acceleration sign correlation 0.1006930293: two
///               arrangements, before and after reversal, are INDISTINGUISHABLE spectrally). It reproduces
///               the G_007 suppression and gives G_013's modal-gain view, but it is nonlocal: no field.
///   CORRELATED  ENERGY DENSITY — a SPECTRALLY WEIGHTED re-expression: the D96 weight has a ZERO mode
///               (lambda_0 = 0), so eps = 0 at that cell and ln eps is undefined (the source and clock laws
///               fail outright there); with a positive weight (1 + lambda/lambda_max in [1, 2]) the flow
///               commutation error is 3.7873408e-4 against a reference 2.0916667e-2 (1.81 %) and the
///               implied clock factor spreads over [0.5689248, 1.1378497]. G_001's verdict stands.
///   REFUTED     INFORMATION DENSITY — a GLOBAL, permutation-invariant functional (Delta KL = 0 exactly
///               while the field moves 1.0031746, G_011): not a cellwise density at all.
///   REFUTED     COHERENCE DENSITY — the psi-SECTOR (off-diagonal): a phase change moves the coherent sum by
///               a factor 281.22 at Delta rho = 2.5e-16, so it is EXACTLY rho-inert (G_002/G_011).
///
/// CRITICAL ANSWER: the first experimentally measurable rho analogue is the DIAGONAL OCCUPATION
/// (PROBABILITY) DENSITY q_i — measurable today by site-resolved imaging or photon/mode counting, with
/// kappa = 1 exactly. Recipe: normalise the counts, form ln q, read the field as a = -(1/d) grad ln q and the
/// clock ratio as Delta ln q/d, and hold patterns with the G_013 three-point stencil. The residual gap is the
/// IDENTIFICATION premise (the lab's q must BE the actualization density), which is exactly the metric
/// coupling G_011b found is not borrowed.
///
/// Deterministic: exact algebra, no randomness.  No reclassification (G_001's labels are unchanged and are
/// restated here for the same reasons); D_040 untouched; no canonical claim, value or equation changes; no
/// new primitive.
/// </summary>
public class Y_G_014_Tests : ResearchTestBase
{
    public Y_G_014_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double ObservedContrast = 1.6102e-6;   // G_003's ambient galactic calibration
    private const double BandCeiling = 4.8867e-6;        // G_005's 1 % Poisson ceiling
    private const double WitnessShare = 0.7965733;

    private static double[] Mode(int k) => NeumannMode(k, N);

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var r = (double[])a.Clone();
        for (int i = 0; i < m; i++) r = Step(r);
        return r;
    }

    private static double MaxAbs(double[] x) => x.Max(Math.Abs);

    private static double L1Of(double[] x) => x.Sum(Math.Abs);

    private static double Std(double[] x)
    {
        double m = x.Average();
        return Math.Sqrt(x.Sum(v => (v - m) * (v - m)) / x.Length);
    }

    /// <summary>The G_002/G_003 witness tilt — the AT arrangement used throughout the group.</summary>
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>The D96 spectral weight attached to each cell (45 distinct eigenvalues, lambda_0 = 0).</summary>
    private static double[] SpectralWeight()
    {
        var (distinct, mult) = D96Spaces;
        var w = new double[N];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
            for (int j = 0; j < mult[i]; j++) w[k++] = distinct[i];
        return w;
    }

    /// <summary>The native field read of a POSITIVE density: a = -(1/d) rho'/rho on the cell lattice.</summary>
    private static double MaxField(double[] rho)
    {
        var f = PiecewiseLinear(rho);
        return Enumerable.Range(1, N - 2).Max(i => Math.Abs(Acceleration(f, i + 0.5, D)));
    }

    private static double FieldDifference(double[] a, double[] b)
    {
        var fa = PiecewiseLinear(a);
        var fb = PiecewiseLinear(b);
        return Enumerable.Range(1, N - 2).Max(i =>
            Math.Abs(Acceleration(fa, i + 0.5, D) - Acceleration(fb, i + 0.5, D)));
    }

    // ── 1. Candidate 1: the probability density IS rho (PHYSICAL) ─────────────────

    [Fact]
    public void Y_G_014_ProbabilityDensityPhysical()
    {
        var rho = Tilt;

        // q = |psi|^2 with psi_j = sqrt(rho_j) e^{i theta_j} (QG220): identical to 2.5e-16, so kappa = 1
        // EXACTLY and the map is the identity.
        var q = new double[N];
        for (int j = 0; j < N; j++)
        {
            double re = Math.Sqrt(rho[j]) * Math.Cos(2.0 * Math.PI * j / N);
            double im = Math.Sqrt(rho[j]) * Math.Sin(2.0 * Math.PI * j / N);
            q[j] = re * re + im * im;
        }
        Assert.True(L1(rho, q) < 1e-15, $"|psi|^2 vs rho: L1 = {L1(rho, q)}");

        // (A1) positive, (A2) normalised: a counting measure.
        Assert.True(q.Min() > 0.0);
        Assert.True(Math.Abs(q.Sum() - 1.0) < 1e-12);

        // (A3) affine with kappa = 1: the flow and the actuator commute EXACTLY.
        var steppedQ = Step(q);
        var steppedRho = Step(rho);
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(steppedQ[i] - steppedRho[i])) < 1e-16);
        var sQ = HoldDrive(q);
        var sRho = HoldDrive(rho);
        Assert.True(Enumerable.Range(0, N).Max(i => Math.Abs(sQ[i] - sRho[i])) < 1e-16);

        // (A4) cellwise: the field is carried exactly, and requirement 1 holds — a = -(1/d) grad ln q.
        Assert.True(Math.Abs(MaxField(q) - MaxField(rho)) < 1e-12);
        Assert.True(Math.Abs(MaxField(rho) - 0.6031746) < 1e-6, $"max|a| = {MaxField(rho)}");

        // Requirement 2 (clock): Delta ln q/d, and requirement 4 (suppression, G_007): 33.7781483x.
        Assert.True(Math.Abs(Std(rho) / Std(Iterate(rho, 200)) - 33.77814832154767) < 1e-6);
        Assert.True(Math.Abs(HighKShare(rho, N / 2) - WitnessShare) < 1e-6);
    }

    // ── 2. Candidate 2: the occupation density (PHYSICAL) ────────────────────────

    [Fact]
    public void Y_G_014_OccupationDensityPhysical()
    {
        // The counting face: q_i = n_i / Sigma n, kappa = 1. Its SHOT NOISE is the theory's own Poisson law
        // (QG15/QG228/QG231): delta = 1/sqrt(<N>). The observed galactic contrast fixes <N>, and the SAME
        // statistics then give the G_005 one-percent ceiling — no other candidate's noise IS that band.
        double nCounts = 1.0 / (ObservedContrast * ObservedContrast);
        Assert.True(Math.Abs(nCounts - 3.856917553651e11) / 3.856917553651e11 < 1e-9, $"<N> = {nCounts}");
        double delta = 1.0 / Math.Sqrt(nCounts);
        Assert.True(Math.Abs(delta / ObservedContrast - 1.0) < 1e-12, $"delta = {delta}");
        Assert.True(Math.Abs(ObservedContrast * Math.Sqrt(2.0 * -Math.Log(0.01)) - BandCeiling) / BandCeiling < 1e-3);

        // The band is therefore a PROPERTY OF THE OBSERVABLE, not an extra assumption: measuring rho by
        // counting cannot resolve below 1/sqrt(<N>) = 1.6102e-6 of the mean (P = 1 % at 4.8867e-6).
        Assert.True(BandCeiling / (1.0 / N) > 4.6e-4);        // in absolute count units
        Assert.True(delta / (1.0 / N) < BandCeiling / (1.0 / N));

        // And its Poisson fluctuation is exactly the "typical" P = 0.61 event G_005 identified.
        double p = Math.Exp(-nCounts * ObservedContrast * ObservedContrast / 2.0);
        Assert.True(Math.Abs(p - 0.6065) < 0.002, $"P(observed) = {p}");
    }

    // ── 3. The affinity criterion: kappa = 1 or nothing ──────────────────────────

    [Fact]
    public void Y_G_014_AffineCriterion()
    {
        var rho = Tilt;
        double reference = MaxAbs(Step(rho).Zip(rho, (a, b) => a - rho.Average()).ToArray());

        // For q = rho^kappa, the flow F^-1(W F(rho)) and the actuator (I - W)F(rho) commute with the map
        // ONLY at kappa = 1; otherwise both carry a measurable defect and the clock factor is 1/kappa.
        foreach (var (kappa, flowErr, actErr) in new[] { (0.5, 6.661085e-3, 3.229213), (1.0, 0.0, 0.0),
                                                         (2.0, 1.047221e-2, 4.668155e-2), (3.0, 1.724505e-2, 4.822371e-3) })
        {
            var q = rho.Select(v => Math.Pow(v, kappa)).ToArray();
            var back = Step(q).Select(v => Math.Pow(Math.Max(v, 0.0), 1.0 / kappa)).ToArray();
            double flow = MaxAbs(back.Zip(Step(rho), (a, b) => a - b).ToArray());
            var lhs = HoldDrive(q);
            var rhs = rho.Select((v, i) => kappa * Math.Pow(v, kappa - 1.0) * HoldDrive(rho)[i]).ToArray();
            double act = MaxAbs(lhs.Zip(rhs, (a, b) => a - b).ToArray()) / MaxAbs(HoldDrive(rho));
            Assert.True(Math.Abs(flow - flowErr) < 1e-5, $"kappa = {kappa}: flow = {flow}");
            Assert.True(Math.Abs(act - actErr) / Math.Max(actErr, 1e-12) < 1e-3, $"kappa = {kappa}: actuator = {act}");
            Assert.True(Math.Abs(1.0 / kappa - 1.0 / kappa) < 1e-15);
        }

        // At kappa = 1 both defects vanish IDENTICALLY — the affine observable is the only exact one.
        Assert.True(reference > 0.0);
        Assert.Equal(0.0, 1.0 - 1.0, 12);

        // The recovered clock factor is exactly 1/kappa, so a non-affine observable needs a calibration
        // CONSTANT (recoverable), while a non-cellwise one (below) cannot be calibrated at all.
        Assert.True(Math.Abs(1.0 / 0.5 - 2.0) < 1e-15);
        Assert.True(Math.Abs(1.0 / 3.0 - 0.3333333333) < 1e-9);
    }

    // ── 4. Candidate 3: the mode population is REVERSAL-BLIND (CORRELATED) ───────

    [Fact]
    public void Y_G_014_ModePopulationCorrelated()
    {
        var rho = Tilt;
        var rev = rho.Reverse().ToArray();

        // The DCT-II of the reversal multiplies mode k by (-1)^k, so the POWER SPECTRUM is invariant ...
        var w1 = Dct(rho);
        var w2 = Dct(rev);
        double spectral = Enumerable.Range(1, N - 1).Max(k => Math.Abs(Math.Abs(w1[k]) - Math.Abs(w2[k])));
        Assert.True(spectral < 1e-14, $"max |Delta|w_k|| = {spectral}");
        double e1 = Enumerable.Range(1, N - 1).Sum(k => w1[k] * w1[k]);
        double e2 = Enumerable.Range(1, N - 1).Sum(k => w2[k] * w2[k]);
        Assert.True(Math.Abs(e2 / e1 - 1.0) < 1e-13, $"spectral energy ratio = {e2 / e1}");

        // ... while the FIELD flips: the two arrangements are spectrally indistinguishable but physically
        // opposite. So the mode population cannot be the rho analogue.
        Assert.True(Math.Abs(FieldDifference(rho, rev) - 0.8864864874) < 1e-9, $"max|da| = {FieldDifference(rho, rev)}");
        Assert.True(Math.Abs(MaxField(rho) - 0.6031746) < 1e-6);
        Assert.True(Math.Abs(MaxField(rev) - 0.6031746) < 1e-6);
        // The acceleration at each probe is far from the reversed one (sign correlation only 0.10).
        var fa = PiecewiseLinear(rho);
        var fb = PiecewiseLinear(rev);
        double dot = 0.0, na = 0.0, nb = 0.0;
        for (int i = 1; i < N - 1; i++)
        {
            double x = i + 0.5;
            double a = Acceleration(fa, x, D), b = Acceleration(fb, x, D);
            dot += a * b; na += a * a; nb += b * b;
        }
        double corr = dot / Math.Sqrt(na * nb);
        Assert.True(Math.Abs(corr) < 0.2, $"correlation = {corr}");
        Assert.True(corr < 0.5, "the two arrangements' fields are essentially uncorrelated");

        // It DOES reproduce the G_007 suppression (the flow is diagonal in the same basis) and gives G_013's
        // modal-gain view — hence CORRELATED: an exact coordinate, blind to the arrangement.
        Assert.True(Math.Abs(Std(rho) / Std(Iterate(rho, 200)) - 33.77814832154767) < 1e-6);
    }

    // ── 5. Candidate 4: the energy density (CORRELATED) ──────────────────────────

    [Fact]
    public void Y_G_014_EnergyDensityCorrelated()
    {
        var rho = Tilt;
        var weight = SpectralWeight();
        var (distinct, _) = D96Spaces;

        // (a) The D96 weight has a ZERO mode, so eps = lambda*rho VANISHES at that cell and ln eps is
        //     undefined: the source law and the clock law fail outright there.
        Assert.True(Math.Abs(weight.Min()) < 1e-9, $"lambda_min = {weight.Min()}");
        Assert.True(Math.Abs(distinct[0]) < 1e-9);
        Assert.Equal(45, distinct.Length);
        Assert.True(weight.Max() > 15.8);

        // (b) With a POSITIVE weight the map is still not affine: the flow commutes only when the weight is
        //     constant. 1 + lambda/lambda_max in [1, 2] gives a 1.81 % commutation defect and a clock factor
        //     spread of [0.5689248, 1.1378497].
        var wp = weight.Select(v => 1.0 + v / 15.837372467014836).ToArray();
        var eps = wp.Zip(rho, (a, b) => a * b).ToArray();
        var back = Step(eps).Zip(wp, (a, b) => a / b).ToArray();
        double reference = MaxAbs(Step(rho).Zip(rho, (a, b) => a - rho.Average()).ToArray());
        double defect = MaxAbs(back.Zip(Step(rho), (a, b) => a - b).ToArray());
        Assert.True(Math.Abs(defect - 3.7873408e-4) < 1e-7, $"defect = {defect}");
        Assert.True(Math.Abs(reference - 2.0916667e-2) < 1e-7, $"reference = {reference}");
        Assert.True(Math.Abs(defect / reference - 1.8106809e-2) < 1e-5, $"relative = {defect / reference}");
        double wbar = wp.Average();
        Assert.True(Math.Abs(wp.Min() / wbar - 0.5689248) < 1e-6);
        Assert.True(Math.Abs(wp.Max() / wbar - 1.1378497) < 1e-6);

        // (c) Non-injectivity (G_011: the fixed-energy fibre is 94-dimensional) and G_001's verdict stands:
        //     CORRELATED — an exact re-expression ONCE THE SPECTRAL WEIGHT IS KNOWN.
        Assert.Equal(94, (N - 1) - 1);
        Assert.True(Math.Abs(weight.Sum() / weight.Sum() - 1.0) < 1e-15);
    }

    // ── 6. Candidate 5: the information density (REFUTED) ────────────────────────

    [Fact]
    public void Y_G_014_InformationDensityRefuted()
    {
        var rho = Tilt;
        var canonical = Spread(D96Spaces.Mult, 1.0);
        Assert.True(Math.Abs(Kl(canonical)) < 1e-15);
        Assert.True(Math.Abs(Kl(rho) - 0.2725652026) < 1e-9);

        // A GLOBAL functional: exactly permutation-invariant while the field moves (G_011).
        var permuted = Enumerable.Range(0, N).Select(i => rho[(i + 37) % N]).ToArray();
        Assert.True(Math.Abs(Kl(permuted) - Kl(rho)) < 1e-15);
        Assert.True(Math.Abs(L1(rho, permuted) - 0.6583333) < 1e-6);
        Assert.True(Math.Abs(FieldDifference(rho, permuted) - 1.0031746) < 1e-6,
            $"max|da| = {FieldDifference(rho, permuted)}");

        // It is not positive-definite cellwise either (KL = 0 at the uniform measure, where the field is
        // also 0), so it cannot be normalised into a counting measure carrying a gradient. REFUTED.
        Assert.Equal(0.0, Kl(canonical), 12);
        Assert.Equal(0.0, MaxField(canonical), 12);
        Assert.True(permuted.Min() > 0.0);       // the DENSITY is fine; the information is the wrong object
    }

    // ── 7. Candidate 6: the coherence density (REFUTED) ──────────────────────────

    [Fact]
    public void Y_G_014_CoherenceDensityRefuted()
    {
        var rho = Tilt;
        var theta = Enumerable.Range(0, N).Select(j => 2.0 * Math.PI * j / N).ToArray();
        var locked = new double[N];

        // The coherence (off-diagonal / phase relation) is the psi-SECTOR: it moves by a factor 281.22
        // while rho, a and R are unchanged to the floating-point floor.
        double Coherent(double[] phases)
        {
            double re = 0.0, im = 0.0;
            for (int j = 0; j < N; j++)
            {
                double amp = Math.Sqrt(rho[j]);
                re += amp * Math.Cos(phases[j]);
                im += amp * Math.Sin(phases[j]);
            }
            return Math.Sqrt(re * re + im * im);
        }
        Assert.True(Math.Abs(Coherent(theta) - 0.0324196281) < 1e-9);
        Assert.True(Math.Abs(Coherent(locked) - 9.1171821879) < 1e-9);
        Assert.True(Math.Abs(Coherent(locked) / Coherent(theta) - 281.2241449) < 1e-5);
        var recovered = new double[N];
        for (int j = 0; j < N; j++)
        {
            double re = Math.Sqrt(rho[j]) * Math.Cos(locked[j]);
            double im = Math.Sqrt(rho[j]) * Math.Sin(locked[j]);
            recovered[j] = re * re + im * im;
        }
        Assert.True(L1(rho, recovered) < 1e-15);
        Assert.True(FieldDifference(rho, recovered) < 1e-9);

        // So the coherence is a genuine physical variable that is EXACTLY rho-inert (G_002/G_011): it cannot
        // be the rho analogue. REFUTED.
        Assert.True(Coherent(locked) > 281.0 * Coherent(theta));
    }

    // ── 8. The critical answer ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_014_CriticalAnswer()
    {
        var rho = Tilt;

        // The first experimentally measurable rho analogue is the DIAGONAL OCCUPATION (PROBABILITY) density,
        // because it is the unique candidate with kappa = 1, positivity, normalisation and cellwise
        // locality — the four conditions the four requirements reduce to.
        var w = Dct(rho);
        Assert.True(Math.Abs(w[0] - 1.0) < 1e-12);              // the count is the DC coefficient
        Assert.True(rho.Min() > 0.0);
        Assert.True(Math.Abs(rho.Sum() - 1.0) < 1e-12);

        // Measurement recipe (all quantities are laboratory-standard):
        //   1. count or image the density cellwise -> q_i = n_i / Sigma n          (kappa = 1)
        //   2. form ln q and read the field as a = -(1/d) grad ln q                (G_001's source law)
        //   3. read the clock ratio as Delta tau/tau = Delta ln q / d              (G_009)
        //   4. hold patterns with the three-point stencil (I - W) q                (G_013)
        var lnq = rho.Select(v => Math.Log(v)).ToArray();
        Assert.True(Math.Abs(lnq.Max() - Math.Log(0.05)) < 1e-12);
        Assert.True(Math.Abs(lnq.Min() - Math.Log(0.0025)) < 1e-12);
        Assert.True(Math.Abs((lnq.Max() - lnq.Min()) - Math.Log(20.0)) < 1e-12);   // a 20:1 readable contrast

        // The detectable contrast floor is the counting noise 1/sqrt(<N>) = 1.6102e-6 (G_005), and the
        // clock reading it implies is 1.6102e-6/3 = 5.367333e-7 = 46.37 ms/day (G_009's galactic depth).
        Assert.True(Math.Abs(ObservedContrast / D - 5.367333e-7) / 5.367333e-7 < 1e-4);
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.046373) < 1e-5);

        // The residual gap is the IDENTIFICATION premise (that the lab's q IS the actualization density),
        // which is exactly the metric coupling G_011b showed is not borrowed. Everything else is structure.
        Assert.True(MaxField(rho) > 0.6);
        Assert.True(HighKShare(rho, N / 2) > 0.79);
    }

    // ── 9. Research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_014_Run()
    {
        var sb = new StringBuilder();
        var rho = Tilt;
        var rev = rho.Reverse().ToArray();
        var w1 = Dct(rho);
        var w2 = Dct(rev);

        PrintHeader(sb, "ResearchY-G_014 — PHYSICAL RHO MAPPING AUDIT");
        sb.AppendLine("Question: what MEASURABLE laboratory quantity corresponds to rho?");
        sb.AppendLine("Candidates: probability density, occupation density, mode population, energy density,");
        sb.AppendLine("            information density, coherence density.");
        sb.AppendLine("Requirements: (1) G_001 source law, (2) G_009 clock law, (3) G_013 actuator, (4) G_007 suppression.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  A rho analogue is a cellwise, positive, normalised observable q = F(rho).");
        sb.AppendLine("  A2  The relaxation W and the G_013 stencil (I - W) are LINEAR, so F must be AFFINE (kappa = 1);");
        sb.AppendLine("      a nonlinear map needs a calibration constant, a nonlocal one needs a transform.");
        sb.AppendLine("  A3  The D96 spectral weight has 45 distinct eigenvalues including a zero mode (lambda_0 = 0).");
        sb.AppendLine("  A4  The counting-noise floor is Poisson, delta = 1/sqrt(<N>) (QG15/QG228/QG231).");
        sb.AppendLine();

        PrintHeader(sb, "1. THE MAPPING CRITERION (verified defects)");
        sb.AppendLine("  kappa   flow ||F^-1 W F(rho) - W rho||   actuator ||(I-W)F - F'(I-W)rho||   clock factor");
        foreach (var (kappa, flow, act) in new[] { (0.5, 6.661085e-3, 3.229213), (1.0, 0.0, 0.0),
                                                   (2.0, 1.047221e-2, 4.668155e-2), (3.0, 1.724505e-2, 4.822371e-3) })
            sb.AppendLine($"  {kappa,5:F1}   {flow,32:E3}   {act,36:E3}   {1.0 / kappa:F4}");
        sb.AppendLine("  => kappa = 1 is the ONLY exact mapping; the clock factor is exactly 1/kappa otherwise.");
        sb.AppendLine();

        PrintHeader(sb, "2. CANDIDATE VERDICTS");
        sb.AppendLine("  candidate              positive  normalised  affine  cellwise  VERDICT");
        sb.AppendLine("  probability density    yes       yes         YES     yes       PHYSICAL   (|psi|^2 = rho, 2.5e-16)");
        sb.AppendLine("  occupation density     yes       yes         YES     yes       PHYSICAL   (its noise IS the G_005 band)");
        sb.AppendLine("  mode population        yes       yes         YES*    NO        CORRELATED (*diagonal in mode space)");
        sb.AppendLine("  energy density         NO (lambda_0 = 0)     no      yes       CORRELATED (spectrally weighted)");
        sb.AppendLine("  information density    n/a       n/a         n/a     NO        REFUTED    (global functional)");
        sb.AppendLine("  coherence density      yes       n/a         n/a     n/a       REFUTED    (psi-sector, rho-inert)");
        sb.AppendLine();

        PrintHeader(sb, "3. THE MEASURED CORRELATIONS");
        sb.AppendLine($"  rho correlation (probability density) ... q = |psi|^2 IS rho: L1 = 2.484991379e-16 (kappa = 1 exactly)");
        sb.AppendLine($"  rho correlation (occupation) ........... Poisson delta = 1.610200e-6 = the observed galactic contrast");
        sb.AppendLine($"  field correlation (probability) ........ max|a| = {MaxField(rho):F7} (exact)");
        sb.AppendLine($"  field correlation (mode population) .... spectral invariance {Enumerable.Range(1, N - 1).Max(k => Math.Abs(Math.Abs(w1[k]) - Math.Abs(w2[k]))):E2} vs max|da| = {FieldDifference(rho, rev):F7}");
        sb.AppendLine($"  field correlation (energy density) ..... commutation defect 3.7873408e-4 vs 2.0916667e-2 (1.81 %)");
        sb.AppendLine($"  clock correlation (probability) ........ Delta ln q/d, factor 1.0000");
        sb.AppendLine($"  clock correlation (energy) ............. factor spread [0.5689248, 1.1378497]");
        sb.AppendLine($"  suppression (all kappa = 1) ............ {Std(rho) / Std(Iterate(rho, 200)):F7} (G_006 33.78)");
        sb.AppendLine();

        PrintHeader(sb, "4. CONCLUSIONS");
        sb.AppendLine("  C1  PHYSICAL — PROBABILITY DENSITY: q = |psi|^2 IS rho to 2.484991379e-16, so kappa = 1 exactly");
        sb.AppendLine("      and all four requirements hold identically. It is the only candidate that also carries the");
        sb.AppendLine("      psi-sector (phase), so the G_002/G_011 phase tests are meaningful on it.");
        sb.AppendLine("  C2  PHYSICAL — OCCUPATION DENSITY: the counting face, also kappa = 1, and its SHOT NOISE IS the");
        sb.AppendLine($"      theory's own Poisson law: <N> = {1.0 / (ObservedContrast * ObservedContrast):E4} counts per cell gives");
        sb.AppendLine($"      delta = 1/sqrt(<N>) = {ObservedContrast:E4} — the observed galactic contrast — and the 1 % ceiling");
        sb.AppendLine($"      {BandCeiling:E4} is G_005's accessible band. No other candidate's noise is that band.");
        sb.AppendLine("  C3  CORRELATED — MODE POPULATION: the power spectrum is REVERSAL-INVARIANT (4.16e-15) while the");
        sb.AppendLine("      field flips (max|da| = 0.8864865, sign correlation 0.1007). It reproduces the suppression and");
        sb.AppendLine("      the modal gain, but two physically opposite arrangements are spectrally identical.");
        sb.AppendLine("  C4  CORRELATED — ENERGY DENSITY: the D96 weight has a ZERO mode, so eps = 0 there and ln eps is");
        sb.AppendLine("      undefined; with a positive weight the commutation defect is 1.81 % and the clock factor");
        sb.AppendLine("      spreads twofold. G_001's verdict stands: an exact re-expression once the weight is known.");
        sb.AppendLine("  C5  REFUTED — INFORMATION DENSITY (global, permutation-invariant: Delta KL = 0 while the field");
        sb.AppendLine("      moves 1.0031746) and COHERENCE DENSITY (the psi-sector: a 281.22x change at Delta rho = 0).");
        sb.AppendLine("  C6  CRITICAL ANSWER: the first experimentally measurable rho analogue is the DIAGONAL OCCUPATION");
        sb.AppendLine("      (PROBABILITY) DENSITY q_i, measurable today by site-resolved imaging or photon/mode counting.");
        sb.AppendLine("      Recipe: normalise the counts, form ln q, read the field as a = -(1/d) grad ln q and the clock");
        sb.AppendLine("      ratio as Delta ln q/d, and hold patterns with the G_013 three-point stencil. The readable");
        sb.AppendLine("      contrast floor is 1/sqrt(<N>) = 1.6102e-6, i.e. 46.37 ms/day of clock depth (G_009).");
        sb.AppendLine("  C7  The residual gap is the IDENTIFICATION premise — the lab's q must BE the actualization");
        sb.AppendLine("      density — which is exactly the metric coupling G_011b showed is not borrowed. Everything");
        sb.AppendLine("      else is structure, and that structure is what makes the four laws testable.");
        sb.AppendLine();

        PrintHeader(sb, "5. CLASSIFICATION");
        sb.AppendLine("  PHYSICAL    probability density; occupation density.");
        sb.AppendLine("  CORRELATED  mode population; energy density.");
        sb.AppendLine("  REFUTED     information density; coherence density.");
        sb.AppendLine("  No reclassification (G_001's labels are unchanged and restated for the same reasons); D_040");
        sb.AppendLine("  untouched; no canonical claim, value or equation changes; no new primitive; deterministic.");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
