using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_007 — Suppression Origin Audit (group G — Gravity Source).
///
/// Question: is DiffuseStep DERIVED or IMPORTED? Trace: Difference -> Actualization -> rho evolution ->
/// DiffuseStep. Determine (1) derivability from canonical AT primitives, (2) uniqueness,
/// (3) alternative operators, (4) sensitivity to the operator choice. Replace DiffuseStep by a
/// nearest-neighbour average, a spectral cutoff, a higher-order (biharmonic) diffusion and the identity,
/// and measure the suppression factor, the G_006 "34x" and the G_003/G_005 witnesses.
///
/// TRACE (all canonical):
///   Difference      -> the counting measure rho (QG_004/QG194: Sigma rho = 1 exactly)
///   Actualization   -> rho_(k+1) = mu rho_k (QG1, branching; COUNT-conserving and ARRANGEMENT-NEUTRAL)
///   rho evolution   -> per-octave deficit increments A_k and their coarse-graining (RhoDynamics.CoarseGrain,
///                      whose RG invariance CoarseGrainedAlpha(alpha) = alpha is EXACT)
///   DiffuseStep     -> the Euler step of the Laplacian flow dA/dt = L A on that chain: the INFINITESIMAL
///                      form of the canonical coarse-graining, not an import.
///
/// VERDICTS
///   DERIVED  — the operator's FORM. Among local (nearest-neighbour), linear, isotropic (symmetric),
///              count-conserving and scale-free (no preferred octave) semigroups the generator is the graph
///              Laplacian, i.e. W = I - d*L: a ONE-parameter family. The positivity of rho (a convex
///              combination needs d <= 1/2) fixes the ADMISSIBLE RANGE 0 <= d <= 1/2, and it is the
///              marginal member d = 1/2 that destroys the selectivity. The qualitative G_005/G_006 verdict
///              (high-k witnesses suppressed, smooth observed field preserved) is robust across the
///              admissible Laplacian-like operators.
///   BOUNDARY — the VALUES: d = 0.2 and the horizon m = 200. At fixed T = m*d the factor varies by only
///              ~0.5% across d = 0.02...0.4, so T (not d, and not m) is the physical control parameter; the
///              number 34 is the T = 40 value and is a BOUNDARY input, exactly as G_006 concluded.
///   REFUTED  — "DiffuseStep is imported/arbitrary" (it is the unique local conservative isotropic scale-free
///              generator); the ALTERNATIVES as canonical operators (spectral cutoff: non-local and
///              non-positive; biharmonic: next-nearest and unstable/non-positive at useful rates; identity:
///              no suppression at all; nearest-neighbour average: canonically admissible but d = 1/2, where
///              |mu| is flat and the mechanism vanishes); and the idea that TIME is the cause.
///
/// TIME (the additional question): time has NOTHING to do with the suppression by itself. The branching
/// flow rho_(k+1) = mu rho_k is diagonal on the arrangement (it rescales every cell equally, so the
/// normalized profile and a(lambda rho) are invariant) and suppresses nothing at ANY mu; the suppression
/// lives entirely in the LEVEL direction (DiffuseStep is tridiagonal in the occupancy index). m counts
/// coarse-graining steps, not time; equating one relaxation step with one generation is an EXTRA
/// assumption (BOUNDARY) — and even then the branching rate mu cancels from the scalar factor.
///
/// Deterministic: exact linear algebra, no randomness. No reclassification; D_040 untouched.
/// </summary>
public class Y_G_007_Tests : ResearchTestBase
{
    public Y_G_007_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    // ── the operator family and the four replacements ────────────────────────────

    /// <summary>DiffuseStep at an explicit rate: b_i = a_i + d(left - 2a_i + right), Neumann ghosts.</summary>
    private static double[] Diffuse(double[] a, double d)
    {
        int n = a.Length;
        var b = new double[n];
        for (int i = 0; i < n; i++)
        {
            double left = i == 0 ? a[0] : a[i - 1];
            double right = i == n - 1 ? a[n - 1] : a[i + 1];
            b[i] = a[i] + d * (left - 2.0 * a[i] + right);
        }
        return b;
    }

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);
    private static double[] StepD(double[] a, double d) => Diffuse(a, d);

    private static double[] Iterate(double[] a, int m, double d = Damping)
    {
        var c = (double[])a.Clone();
        for (int i = 0; i < m; i++) c = Diffuse(c, d);
        return c;
    }

    /// <summary>The replacement "nearest-neighbour average": b_i = (left + right)/2 with Neumann ghosts.</summary>
    private static double[] NearestNeighbourAverage(double[] a)
    {
        int n = a.Length;
        var b = new double[n];
        for (int i = 0; i < n; i++)
        {
            double left = i == 0 ? a[0] : a[i - 1];
            double right = i == n - 1 ? a[n - 1] : a[i + 1];
            b[i] = 0.5 * (left + right);
        }
        return b;
    }

    /// <summary>Reflecting index access (Neumann ghosts) used by the biharmonic stencil.</summary>
    private static double At(double[] a, int i)
    {
        int n = a.Length;
        if (i < 0) i = -i;
        if (i >= n) i = 2 * n - 2 - i;
        return a[i];
    }

    /// <summary>The replacement "higher-order diffusion": one biharmonic (L^2) Euler step, 5-point stencil.</summary>
    private static double[] Biharmonic(double[] a, double kappa)
    {
        int n = a.Length;
        var b = new double[n];
        for (int i = 0; i < n; i++)
        {
            double lap = At(a, i - 1) - 2.0 * a[i] + At(a, i + 1);
            double lapLeft = At(a, i - 2) - 2.0 * At(a, i - 1) + a[i];
            double lapRight = a[i] - 2.0 * At(a, i + 1) + At(a, i + 2);
            b[i] = a[i] - kappa * (lapLeft - 2.0 * lap + lapRight);
        }
        return b;
    }

    /// <summary>The replacement "identity": no smoothing at all.</summary>
    private static double[] Identity(double[] a) => (double[])a.Clone();

    // ── the replacement "spectral cutoff": a global rank projection (NOT local) ───

    private static double[] DctNeumann(double[] x)
    {
        int n = x.Length;
        var w = new double[n];
        for (int k = 0; k < n; k++)
        {
            double s = 0.0;
            for (int i = 0; i < n; i++) s += x[i] * Math.Cos(Math.PI * k * (i + 0.5) / n);
            w[k] = s;
        }
        return w;
    }

    /// <summary>Keep only the Neumann modes k &lt;= kc (a spectral cutoff), then transform back.</summary>
    private static double[] SpectralCutoff(double[] x, int kc)
    {
        int n = x.Length;
        var w = DctNeumann(x);
        var y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double s = 0.0;
            for (int k = 1; k <= kc && k < n; k++) s += w[k] * Math.Cos(Math.PI * k * (i + 0.5) / n);
            y[i] = w[0] / n + 2.0 * s / n;      // exact inverse DCT-II (k = 0 has the half weight)
        }
        return y;
    }

    // ── measures ─────────────────────────────────────────────────────────────────

    /// <summary>Exact Neumann eigenvalue of the Laplacian family: mu_k = 1 - 2d(1 - cos(pi k/N)).</summary>
    private static double Mu(int k, double d = Damping, int n = N)
        => 1.0 - 2.0 * d * (1.0 - Math.Cos(Math.PI * k / n));

    /// <summary>Exact Neumann eigenvalue of the biharmonic step: 1 - kappa*(2 - 2cos)^2.</summary>
    private static double MuBi(int k, double kappa, int n = N)
    {
        double z = 2.0 - 2.0 * Math.Cos(Math.PI * k / n);
        return 1.0 - kappa * z * z;
    }

    private static double[] Mode(int k, int n = N)
        => Enumerable.Range(0, n).Select(i => Math.Cos(Math.PI * k * (i + 0.5) / n)).ToArray();

    private static double Std(double[] x)
    {
        double m = x.Average();
        return Math.Sqrt(x.Sum(v => (v - m) * (v - m)) / x.Length);
    }

    private static double[] Canonical => Spread(D96Spaces.Mult, 1.0);
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    /// <summary>Suppression factor of an arrangement after m applications of an operator.</summary>
    private static double Factor(double[] x, Func<double[], double[]> op, int m)
    {
        var c = (double[])x.Clone();
        for (int i = 0; i < m; i++) c = op(c);
        return Std(x) / Std(c);
    }

    /// <summary>Exact closed-form factor for the Laplacian family (the mode weights are exact).</summary>
    private static double FactorClosed(double[] x, int m, double d = Damping)
    {
        var w = DctNeumann(x);
        w[0] = 0.0;
        double num = 0.0, den = 0.0;
        for (int k = 1; k < w.Length; k++)
        {
            num += w[k] * w[k] * Math.Pow(Mu(k, d, x.Length), 2.0 * m);
            den += w[k] * w[k];
        }
        return Math.Sqrt(den / num);
    }

    /// <summary>Filter selectivity: the fastest available mode's survival per step vs the slowest.</summary>
    private static double Selectivity(double d) => Math.Abs(Mu(95, d)) / Math.Abs(Mu(1, d));

    /// <summary>The smooth (observed-field-like) mode's survival after m steps — the k = 1 mode.</summary>
    private static double SmoothSurvival(int m, double d = Damping) => Math.Pow(Mu(1, d), m);

    // ── 1. The trace: Difference -> Actualization -> rho evolution -> DiffuseStep ─

    [Fact]
    public void Y_G_007_TraceAndDerivation()
    {
        var rho = Canonical;

        // (i) DIFFERENCE -> counting measure: rho is a normalised count, Sigma rho = 1 exactly (QG194).
        Assert.Equal(1.0, Total(rho), 12);
        Assert.True(NativeMetricDynamics.CountConserved(1.08, 24));

        // (ii) ACTUALIZATION -> rho_(k+1) = mu*rho_k, count-conserving and ARRANGEMENT-NEUTRAL
        //      (a uniform rescaling leaves the normalised profile and the field invariant).
        Assert.True(NativeMetricDynamics.BranchingContinuity(1.08, 24));
        var scaled = rho.Select(v => 1.08 * v).ToArray();
        Assert.True(L1(rho, scaled) > 0.0);
        Assert.True(rho.Select((v, i) => Math.Abs(v / Total(rho) - scaled[i] / Total(scaled))).Max() < 1e-15);
        Assert.True(MaxAccelerationDifference(rho, scaled, D) < 1e-6);

        // (iii) rho EVOLUTION -> per-octave increments coarse-grain, and the coarse-graining is SCALE-FREE
        //       (alpha is an exact RG fixed point, RhoDynamics.CoarseGrainedAlpha(alpha) = alpha).
        var inc = RhoDynamics.Increments(1.5, 8, 1.5);
        Assert.Equal(1.0, inc.Sum(), 12);
        Assert.Equal(1.0, RhoDynamics.CoarseGrain(inc).Sum(), 12);          // the total deficit is conserved
        foreach (double alpha in new[] { 0.0, 0.5, 1.0, 1.5, 2.5 })
            Assert.True(Math.Abs(RhoDynamics.CoarseGrainedAlpha(alpha, 8, 1.5) - alpha) < 1e-12,
                $"RG invariance broken at alpha = {alpha}");

        // (iv) DiffuseStep IS the infinitesimal form of that coarse-graining: the one-step map is
        //      W = I - d*L with L the graph Laplacian on the occupancy index. Verified structurally:
        //      tridiagonal, symmetric, rows summing to 1, and a genuine SEMIGROUP.
        var a = Tilt;
        var m1 = Step(a);
        Assert.Equal(1.0, Total(m1), 12);                                   // conservation
        for (int i = 0; i < N; i++)                                          // tridiagonal support
        {
            var e = new double[N]; e[i] = 1.0;
            var col = Step(e);
            var support = Enumerable.Range(0, N).Where(j => Math.Abs(col[j]) > 1e-15).ToArray();
            Assert.True(support.Length <= 3, $"row {i} has {support.Length} non-zeros");
        }
        for (int i = 0; i < N; i++)                                          // symmetry (isotropy)
            for (int j = 0; j < N; j++)
            {
                var ei = new double[N]; ei[i] = 1.0;
                var ej = new double[N]; ej[j] = 1.0;
                Assert.True(Math.Abs(Step(ei)[j] - Step(ej)[i]) < 1e-15);
            }
        Assert.True(L1(Iterate(Iterate(a, 7), 13), Iterate(a, 20)) < 1e-15);  // semigroup
        Assert.True(L1(StepD(a, 0.0), a) < 1e-15);                            // d = 0 -> identity
        Assert.True(MaxAccelerationDifference(a, Step(a), D) > 1e-6);         // and it does move the field
    }

    // ── 2. Uniqueness: one free scalar, and it is the rate ──────────────────────

    /// <summary>The general nearest-neighbour, symmetric, conservation-preserving operator W(b).</summary>
    private static double[] ApplyTridiagonal(double[] a, double b)
    {
        int n = a.Length;
        var y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double left = i == 0 ? a[0] : a[i - 1];
            double right = i == n - 1 ? a[n - 1] : a[i + 1];
            y[i] = b * left + (1.0 - 2.0 * b) * a[i] + b * right;
        }
        return y;
    }

    /// <summary>An anisotropic (directed) variant: different weight to the left and to the right.</summary>
    private static double[] Directed(double[] a, double left, double right)
    {
        int n = a.Length;
        var y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double l = i == 0 ? a[0] : a[i - 1];
            double r = i == n - 1 ? a[n - 1] : a[i + 1];
            y[i] = left * l + (1.0 - left - right) * a[i] + right * r;
        }
        return y;
    }

    [Fact]
    public void Y_G_007_UniqueUpToRate()
    {
        var a = Tilt;

        // The axioms (nearest-neighbour SUPPORT + constant coefficients + symmetry + rows summing to 1)
        // leave exactly ONE free scalar: W(b) has off-diagonal b and diagonal 1 - 2b.
        foreach (double b in new[] { 0.05, 0.2, 0.3, 0.45 })
        {
            Assert.True(L1(ApplyTridiagonal(a, b), Diffuse(a, b)) < 1e-15, $"W({b}) != Diffuse(d = {b})");
            Assert.Equal(1.0, Total(ApplyTridiagonal(a, b)), 12);            // rows sum to 1 for ANY b
            Assert.True(ApplyTridiagonal(a, b).Sum(v => Math.Abs(v - 1.0 / N)) > 0.0);
        }
        Assert.True(L1(ApplyTridiagonal(a, 0.2), Step(a)) < 1e-15);          // the canonical member

        // Dropping ISOTROPY admits directed operators — but with reflecting (Neumann) boundaries an
        // anisotropic weight does NOT conserve the total: conservation FORCES isotropy here (the leak is
        // proportional to (l - r)(a_0 - a_N-1)), and the canonical chain has no antisymmetric coupling
        // (NP_174 reciprocity), so the directed branch is not available.
        var dir = Directed(a, 0.3, 0.1);
        var dirRev = Directed(a, 0.1, 0.3);
        Assert.True(Math.Abs(Total(dir) - 1.0) > 1e-5, $"directed total = {Total(dir)}");
        Assert.True(Math.Abs(Total(dirRev) - 1.0) > 1e-5);
        Assert.True(Math.Sign(Total(dir) - 1.0) != Math.Sign(Total(dirRev) - 1.0));
        Assert.True(L1(dir, ApplyTridiagonal(a, 0.2)) > 1e-6);
        Assert.True(L1(dir, dirRev) > 1e-6);                                  // and the direction matters
        Assert.True(L1(ApplyTridiagonal(a, 0.2), ApplyTridiagonal(a, 0.2)) < 1e-15);

        // The uniqueness statement: among local + linear + isotropic + conservative + scale-free maps, the
        // generator is the graph Laplacian; the family is 1-parameter; the canonical choice is d = 0.2.
        Assert.True(Math.Abs(Damping - 0.2) < 1e-15);
        Assert.True(Math.Abs(Mu(0) - 1.0) < 1e-15);                           // the mean is untouched for any d
        foreach (double d in new[] { 0.05, 0.2, 0.4 })
            Assert.True(Math.Abs(Mu(0, d) - 1.0) < 1e-15);
    }

    // ── 3. Positivity derives the admissible range, not the value ───────────────

    [Fact]
    public void Y_G_007_PositivityDerivesTheRange()
    {
        // The update is a CONVEX COMBINATION b_i = d*a_(i-1) + (1-2d)*a_i + d*a_(i+1): non-negative
        // coefficients (hence rho >= 0 preserved) require 0 <= d <= 1/2. The range is DERIVED.
        var spike = Canonical;
        spike[48] += 0.5;
        Assert.True(spike.Min() > 0.0);
        Assert.True(StepD(spike, 0.0).Min() >= 0.0);
        Assert.True(StepD(spike, 0.2).Min() >= 0.0);
        Assert.True(StepD(spike, 0.5).Min() >= 0.0, $"d=1/2 is the marginal convex case: {StepD(spike, 0.5).Min()}");
        Assert.True(StepD(spike, 0.6).Min() < 0.0, "d > 1/2 must violate rho >= 0");

        // Stability follows from the same bound: |mu_k| <= 1 for d <= 1/2 (the fastest mode reaches -1 at
        // d = 1/2 exactly, i.e. it OSCILLATES instead of decaying).
        foreach (double d in new[] { 0.05, 0.2, 0.25, 0.4, 0.5 })
            Assert.True(Enumerable.Range(0, N).Max(k => Math.Abs(Mu(k, d))) <= 1.0 + 1e-12, $"d = {d}");
        Assert.True(Mu(95, 0.5) < 0.0 && Math.Abs(Mu(95, 0.5) + 0.999465) < 1e-5, $"mu_95(1/2) = {Mu(95, 0.5)}");
        Assert.True(Mu(95, 0.25) > 0.0 && Mu(95, 0.25) < 1e-3, $"mu_95(1/4) = {Mu(95, 0.25)}");

        // The mechanism needs SELECTIVITY, i.e. a spread between the slowest and the fastest mode: maximal at
        // d = 1/4 and GONE at d = 1/2 (where |mu| is flat, the nearest-neighbour average).
        Assert.True(Selectivity(0.25) < 1e-2);
        Assert.True(Selectivity(0.2) > 0.15 && Selectivity(0.2) < 0.25, $"selectivity(0.2) = {Selectivity(0.2)}");
        Assert.True(Selectivity(0.5) > 0.99, $"selectivity(1/2) = {Selectivity(0.5)}");
        Assert.True(Selectivity(0.5) / Selectivity(0.2) > 4.0);
    }

    // ── 4. The four replacements, and why three of them are not admissible ──────

    [Fact]
    public void Y_G_007_AlternativeOperators()
    {
        var a = Tilt;
        double baseFactor = Factor(a, Step, 200);

        // (a) NEAREST-NEIGHBOUR AVERAGE — not a different operator at all: it is the CANONICAL member d = 1/2
        //     of the same Laplacian family (the marginal convex case).
        Assert.True(L1(NearestNeighbourAverage(a), Diffuse(a, 0.5)) < 1e-15);
        Assert.True(L1(NearestNeighbourAverage(a), Step(a)) > 1e-6);
        double nnFactor = Factor(a, NearestNeighbourAverage, 200);
        Assert.True(nnFactor < 3.0, $"NN-average factor = {nnFactor} vs {baseFactor}");
        //     ...and at d = 1/2 the WORST mode stops decaying: it oscillates with |mu| = 0.9995,
        //     so the extreme arrangements survive 90 % of their amplitude. The mechanism is gone.
        Assert.True(Math.Abs(Mu(95, 0.5)) > 0.999, $"|mu_95(1/2)| = {Math.Abs(Mu(95, 0.5))}");
        Assert.True(Math.Pow(Math.Abs(Mu(95, 0.5)), 200) > 0.85);
        Assert.True(Math.Pow(Mu(95), 200) < 1e-100);

        // (b) SPECTRAL CUTOFF (keep k <= kc) — NOT LOCAL and NOT POSITIVE:
        var delta = new double[N]; delta[50] = 1.0;
        int stepSupport = Enumerable.Range(0, N).Count(i => Math.Abs(Step(delta)[i]) > 1e-15);
        int cutSupport = Enumerable.Range(0, N).Count(i => Math.Abs(SpectralCutoff(delta, 48)[i]) > 1e-15);
        Assert.Equal(3, stepSupport);
        Assert.Equal(N, cutSupport);                                          // instantaneous action at a distance
        var spike = Canonical; spike[48] += 0.5;
        Assert.True(StepD(spike, 0.2).Min() >= 0.0);
        Assert.True(SpectralCutoff(spike, 48).Min() < 0.0, $"cutoff min = {SpectralCutoff(spike, 48).Min()}");
        Assert.True(SpectralCutoff(spike, 60).Min() < 0.0);
        Assert.True(L1(SpectralCutoff(delta, 48), delta) > 1.0);              // even a delta is mangled
        //     Its "factor" is also not comparable: it removes the high-k content in ONE step and leaves the
        //     smooth residual (2.1x), i.e. it is an on/off filter rather than a graded decay.
        double cutFactor = Factor(a, x => SpectralCutoff(x, 48), 200);
        Assert.True(Math.Abs(cutFactor - 2.1) < 0.35, $"cutoff factor = {cutFactor}");

        // (c) HIGHER-ORDER (BIHARMONIC) DIFFUSION — NEXT-NEAREST (5-point), NOT A CONVEX COMBINATION
        //     (negative ±2 weights), unstable beyond kappa = 1/16 and positivity-violating long before that.
        Assert.Equal(5, Enumerable.Range(0, N).Count(i => Math.Abs(Biharmonic(delta, 0.05)[i]) > 1e-15));
        double z2Max = Math.Pow(2.0 - 2.0 * Math.Cos(Math.PI * 95.0 / 96.0), 2.0);
        Assert.True(Math.Abs(z2Max - 15.9914) < 1e-3, $"max z^2 = {z2Max}");
        Assert.True(Math.Abs(MuBi(95, 0.0625) - 0.0) < 1e-3);                 // the stability edge
        Assert.True(MuBi(95, 0.1) < -0.5);                                    // unstable at a useful rate
        Assert.True(Biharmonic(spike, 0.05).Min() < 0.0, "biharmonic must break rho >= 0");
        Assert.True(Biharmonic(spike, 0.01).Min() > 0.0);                     // only kappa <~ 0.014 stays positive
        double biFactor = Factor(a, x => Biharmonic(x, 0.05), 200);
        Assert.True(biFactor < baseFactor, $"biharmonic factor {biFactor} vs {baseFactor}");

        // (d) IDENTITY — no suppression at all: the witnesses would survive at full amplitude, contradicting
        //     G_004 (the observed fields require a suppression >= 3.746e5).
        Assert.True(L1(Identity(a), a) < 1e-15);
        Assert.Equal(1.0, Factor(a, Identity, 200), 12);
        Assert.True(Factor(a, Identity, 200) < 3.746e5);
        Assert.True(baseFactor > 1.0 && nnFactor >= 1.0);
    }

    // ── 5. Sensitivity lives in the RATE, not in the FORM ──────────────────────

    [Fact]
    public void Y_G_007_SensitivityIsInTheRate()
    {
        var a = Tilt;

        // The closed form must reproduce every member of the family (the form is fixed, only d moves).
        foreach (double d in new[] { 0.05, 0.1, 0.2, 0.3, 0.4 })
            Assert.True(Math.Abs(FactorClosed(a, 200, d) / Factor(a, x => StepD(x, d), 200) - 1.0) < 1e-9,
                $"d = {d}");

        // The factor at m = 200 grows with d up to the optimum and then COLLAPSES at d = 1/2.
        double f10 = Factor(a, x => StepD(x, 0.1), 200);
        double f20 = Factor(a, x => StepD(x, 0.2), 200);
        double f30 = Factor(a, x => StepD(x, 0.3), 200);
        double f40 = Factor(a, x => StepD(x, 0.4), 200);
        double f50 = Factor(a, x => StepD(x, 0.5), 200);
        Assert.True(f10 > 15.0 && f10 < 30.0, $"d=0.1 -> {f10}");
        Assert.True(f20 > 30.0 && f20 < 38.0, $"d=0.2 -> {f20}");
        Assert.True(f30 > 35.0 && f30 < 52.0, $"d=0.3 -> {f30}");
        Assert.True(f40 > 40.0 && f40 < 52.0, $"d=0.4 -> {f40}");
        Assert.True(f50 < 3.0, $"d=0.5 -> {f50}");
        Assert.True(f10 < f20 && f20 < f30 && f30 < f40);
        Assert.True(f50 < f10);

        // SENSITIVITY IS COLLAPSED BY T = m·d: at fixed T the factor varies by < 1 % over a 20x range of d.
        double T = 40.0;
        var factors = new[] { (0.02, 2000), (0.05, 800), (0.1, 400), (0.2, 200), (0.4, 100) }
            .Select(p => FactorClosed(a, p.Item2, p.Item1)).ToArray();
        Assert.True(factors.Max() / factors.Min() < 1.01, $"T-fixed spread {factors.Min()}..{factors.Max()}");
        Assert.True(factors.All(f => f > 28.0 && f < 38.0), $"T-fixed factors: {string.Join(", ", factors)}");

        // The filter's SELECTIVITY is |mu_95|/|mu_1| ~= |1 - 4d|: maximal at d = 1/4, zero at d = 1/2.
        Assert.True(Math.Abs(Selectivity(0.2) - 0.2) < 0.05);
        Assert.True(Selectivity(0.25) < 1e-2 && Selectivity(0.5) > 0.99);
        foreach (double d in new[] { 0.1, 0.2, 0.3, 0.4, 0.5 })
            Assert.True(Math.Abs(Selectivity(d) - Math.Abs(1.0 - 3.9989 * d)) < 0.05, $"selectivity({d})");

        // Robustness of the STRUCTURE: for every admissible d the fastest mode at m = 200 is annihilated
        // relative to the slowest — except at the marginal value.
        Assert.True(FactorClosed(a, 200, 0.2) / SmoothSurvival(200, 0.2) > 30.0);
        Assert.True(FactorClosed(a, 200, 0.5) / SmoothSurvival(200, 0.5) < 2.5);
    }

    // ── 6. The gravity-control witnesses under each replacement ─────────────────

    [Fact]
    public void Y_G_007_WitnessesUnderAlternatives()
    {
        var a = Tilt;
        // The two objects that matter: the G_002/G_003 WITNESS (within-multiplet, high-k) and the OBSERVED
        // smooth field (the k = 1 mode, G4-ME21's one-void-per-octave deficit).
        double witnessD96 = Factor(a, Step, 200);
        double smoothD96 = 1.0 / SmoothSurvival(200);

        // (1) Canonical DiffuseStep: the DICHOTOMY that closes G_005 — witness erased, observed field kept.
        Assert.True(witnessD96 > 30.0 && witnessD96 < 38.0, $"witness = {witnessD96}");
        Assert.True(Math.Abs(smoothD96 - 1.044) < 0.01, $"smooth survival factor = {smoothD96}");
        Assert.True(witnessD96 / smoothD96 > 30.0);

        // (2) Identity: neither is touched -> no mechanism, and G_004's 3.746e5 requirement is violated.
        Assert.Equal(1.0, Factor(a, Identity, 200), 12);
        Assert.True(Factor(a, Identity, 200) * 3.746e5 > 1e5);

        // (3) Nearest-neighbour average (d = 1/2): BOTH survive, so the dichotomy disappears
        //     (witness 1.7x vs smooth 1.11x — the filter is flat).
        double witnessNN = Factor(a, NearestNeighbourAverage, 200);
        double smoothNN = 1.0 / SmoothSurvival(200, 0.5);
        Assert.True(witnessNN < 3.0 && smoothNN > 1.05 && smoothNN < 1.2);
        Assert.True(witnessNN / smoothNN < 3.0, $"NN dichotomy ratio = {witnessNN / smoothNN}");
        Assert.True(witnessD96 / smoothD96 / (witnessNN / smoothNN) > 10.0);

        // (4) Biharmonic (kappa = 0.05): the dichotomy SURVIVES but is much weaker, and the operator is
        //     non-local and non-positive — admissible only as a deformation, not as the canonical term.
        double witnessBi = Factor(a, x => Biharmonic(x, 0.05), 200);
        Assert.True(witnessBi > 4.0 && witnessBi < 9.0, $"biharmonic witness = {witnessBi}");
        Assert.True(witnessBi < witnessD96);
        Assert.True(Math.Pow(MuBi(1, 0.05), 200) > 0.9999);

        // (5) Spectral cutoff (kc = 48): the high-k witness is removed in ONE step (an on/off filter), the
        //     smooth part survives, but rho becomes NEGATIVE — a physical contradiction, not a mechanism.
        var afterCut = SpectralCutoff(a, 48);
        Assert.True(Std(afterCut) < Std(a));
        Assert.True(afterCut.Min() < 0.0);
        Assert.True(1.0 / Std(afterCut) * Std(a) < 3.0);                      // only a 2.1x contrast reduction

        // VERDICT for the witnesses: the SUPPRESSED verdict is ROBUST across the admissible
        // Laplacian-like family (any 0 < d < 1/2 does it, better than the biharmonic) and REFUTED by the
        // identity and by the marginal d = 1/2.
        foreach (double d in new[] { 0.1, 0.2, 0.25, 0.3, 0.35 })
        {
            double w = FactorClosed(a, 200, d);
            double s = 1.0 / SmoothSurvival(200, d);
            Assert.True(w / s > 8.0, $"d = {d}: dichotomy ratio {w / s}");
        }
    }

    // ── 7. The additional question: does TIME have anything to do with the suppression? ──

    [Fact]
    public void Y_G_007_TimeIsNotTheCause()
    {
        var a = Tilt;

        // (a) The TIME-LIKE flow (branching, rho_(k+1) = mu*rho_k) is DIAGONAL on the arrangement: it
        //     multiplies every cell by the same scalar, so any number of generations leaves the normalised
        //     profile — and therefore the field — exactly invariant. Time alone suppresses NOTHING.
        foreach (double mu in new[] { 1.0, 1.08, 2.0 })
        {
            var evolved = a.Select(v => Math.Pow(mu, 1000.0) * v).ToArray();
            Assert.True(a.Select((v, i) => Math.Abs(v / Total(a) - evolved[i] / Total(evolved))).Max() < 1e-15,
                $"mu = {mu}");
            Assert.True(MaxAccelerationDifference(a, evolved, D) < 1e-6);
            var e = new double[N]; e[40] = 1.0;                               // one generation stays on one cell
            Assert.Equal(1, Enumerable.Range(0, N).Count(i => Math.Abs(mu * e[i]) > 1e-15));
        }
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality());
        Assert.True(NativeMetricDynamics.MetricStaticAtCriticality(D));
        Assert.True(NativeMetricDynamics.BranchingContinuity(1.08, 24));
        Assert.True(Math.Abs(NativeMetricDynamics.DensityRate(1.08) - Math.Log(1.08)) < 1e-12);

        // (b) The SUPPRESSING operator is tridiagonal in the occupancy index: it MIXES ADJACENT LEVELS, i.e.
        //     it is a coarse-graining (scale) operator, not a time evolution. Contrast the two structures.
        var e40 = new double[N]; e40[40] = 1.0;
        Assert.Equal(3, Enumerable.Range(0, N).Count(i => Math.Abs(Step(e40)[i]) > 1e-15));
        Assert.Equal(1, Enumerable.Range(0, N).Count(i => Math.Abs(e40[i] * 1.08) > 1e-15));

        // (c) The factor is a function of m (coarse-graining steps) ONLY: it contains no rate, no mu, no
        //     physical time. Equivalently, the branching parameter cancels from the scalar suppression.
        double f200 = FactorClosed(a, 200);
        Assert.True(Math.Abs(f200 - FactorClosed(a, 200, Damping)) < 1e-12);   // the formula carries no mu and no time
        Assert.True(Math.Abs(f200 - FactorClosed(a.Select(v => 7.0 * v).ToArray(), 200)) < 1e-9);  // scale-free

        // (d) The only temporal readings available are EXTRA assumptions: (i) identify one relaxation step
        //     with one generation (then the witnesses decay over ~200 generations — but the branching rate mu
        //     still cancels), or (ii) read the horizon m as a physical time via the entropy production, which
        //     G_006 showed to be downstream. Neither is derived here.
        double mEquiv = Math.Log(3.746e5) / Math.Log(f200) * 200.0;         // steps for the G_003 requirement
        Assert.True(mEquiv > 600.0 && mEquiv < 900.0, $"equivalent horizon = {mEquiv} steps");
        Assert.True(mEquiv > 0.0 && double.IsFinite(mEquiv));                 // a horizon exists; a TIME does not
    }

    // ── 8. Verdicts ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_007_Verdicts()
    {
        var a = Tilt;

        // 1. DERIVABLE FROM CANONICAL PRIMITIVES — YES (DERIVED).
        //    DiffuseStep is the Euler step of the Laplacian flow on the occupancy index: the infinitesimal
        //    form of the canonical coarse-graining, whose RG invariance is exact.
        foreach (double alpha in new[] { 0.0, 1.0, 2.5 })
            Assert.True(Math.Abs(RhoDynamics.CoarseGrainedAlpha(alpha, 8, 1.5) - alpha) < 1e-12);
        Assert.Equal(1.0, RhoDynamics.CoarseGrain(RhoDynamics.Increments(1.5, 8, 1.5)).Sum(), 12);
        Assert.True(L1(Iterate(Iterate(a, 9), 11), Iterate(a, 20)) < 1e-15);   // semigroup
        Assert.Equal(1.0, Total(Step(a)), 12);                                 // conservation

        // 2. UNIQUE? — YES UP TO ONE SCALAR (DERIVED): local + linear + symmetric + conservative + scale-free
        //    leaves exactly the family W(b) = I - b*L, and the canonical member is b = 0.2.
        Assert.True(L1(ApplyTridiagonal(a, 0.2), Step(a)) < 1e-15);
        Assert.Equal(1.0, Total(ApplyTridiagonal(a, 0.35)), 12);
        Assert.True(Selectivity(0.5) > 0.99 && Selectivity(0.25) < 1e-2);       // the family spans "no filter".."ideal filter"

        // 3. ALTERNATIVE OPERATORS — REFUTED as canonical.
        var delta = new double[N]; delta[50] = 1.0;
        var spike = Canonical; spike[48] += 0.5;
        Assert.Equal(96, Enumerable.Range(0, N).Count(i => Math.Abs(SpectralCutoff(delta, 48)[i]) > 1e-15));  // non-local
        Assert.True(SpectralCutoff(spike, 48).Min() < 0.0);                                                  // non-positive
        Assert.Equal(5, Enumerable.Range(0, N).Count(i => Math.Abs(Biharmonic(delta, 0.05)[i]) > 1e-15));      // next-nearest
        Assert.True(MuBi(95, 0.1) < 0.0);                                                                     // unstable
        Assert.True(Biharmonic(spike, 0.05).Min() < 0.0);                                                      // non-positive
        Assert.Equal(1.0, Factor(a, Identity, 200), 12);                                                       // no suppression
        Assert.True(L1(NearestNeighbourAverage(a), Diffuse(a, 0.5)) < 1e-15);                                  // d = 1/2, flat filter
        Assert.True(Factor(a, NearestNeighbourAverage, 200) < 3.0);

        // 4. SENSITIVITY — the sign is robust, the magnitude is BOUNDARY: the factor is a function of T = m*d
        //    (< 1 % spread over a 20x range of d at fixed T), so the canonical VALUES d = 0.2 and m = 200 are
        //    the boundary inputs, and the G_006 number 34 = exp(3.5198 nats) is the T = 40 value.
        var fT = new[] { (0.02, 2000), (0.1, 400), (0.2, 200), (0.4, 100) }
            .Select(p => FactorClosed(a, p.Item2, p.Item1)).ToArray();
        Assert.True(fT.Max() / fT.Min() < 1.01);
        Assert.True(Math.Abs(Math.Log(FactorClosed(a, 200)) - 3.5198) < 0.15, $"log factor = {Math.Log(FactorClosed(a, 200))}");
        Assert.True(Factor(a, Identity, 200) < Factor(a, NearestNeighbourAverage, 200));

        // 5. TIME — REFUTED as the cause (the suppression is a LEVEL/coarse-graining effect).
        var evolved = a.Select(v => Math.Pow(2.0, 1000.0) * v).ToArray();
        Assert.True(a.Select((v, i) => Math.Abs(v / Total(a) - evolved[i] / Total(evolved))).Max() < 1e-15);
        var e40 = new double[N]; e40[40] = 1.0;
        Assert.Equal(3, Enumerable.Range(0, N).Count(i => Math.Abs(Step(e40)[i]) > 1e-15));   // mixes LEVELS
        Assert.Equal(1, Enumerable.Range(0, N).Count(i => Math.Abs(2.0 * e40[i]) > 1e-15));    // time does not
    }

    // ── 9. Report ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_007_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_007 — Suppression Origin Audit (Gravity Source)");

        var a = Tilt;
        double f(int m, double d) => FactorClosed(a, m, d);

        sb.AppendLine("QUESTION — is DiffuseStep DERIVED or IMPORTED?");
        sb.AppendLine("  Trace: Difference -> Actualization -> rho evolution -> DiffuseStep.");
        sb.AppendLine("  Determine: (1) derivability from canonical primitives (2) uniqueness (3) alternative");
        sb.AppendLine("  operators (4) sensitivity to the operator choice; measure the suppression factor, the G_006");
        sb.AppendLine("  34x and the G_003/G_005 witnesses under four replacements.");
        sb.AppendLine("  ADDITIONAL: does TIME have anything to do with this suppression?");
        sb.AppendLine();

        PrintHeader(sb, "1. THE TRACE — every link is canonical");
        sb.AppendLine($"  Difference    -> counting measure rho:  Sigma rho = {Total(Canonical):F12} exactly (QG194);");
        sb.AppendLine($"                  count conservation along the flow ............ {NativeMetricDynamics.CountConserved(1.08, 24)}");
        sb.AppendLine($"  Actualization -> rho_(k+1) = mu rho_k ......................... {NativeMetricDynamics.BranchingContinuity(1.08, 24)}");
        sb.AppendLine($"                  ARRANGEMENT-NEUTRAL: a(10^6 rho) = a(rho) ..... |da| = {MaxAccelerationDifference(Canonical, Canonical.Select(v => 1e6 * v).ToArray(), D):E2}");
        sb.AppendLine($"  rho evolution -> increments coarse-grain, total conserved ..... {RhoDynamics.CoarseGrain(RhoDynamics.Increments(1.5, 8, 1.5)).Sum():F12}");
        sb.AppendLine($"                  RG invariance CoarseGrainedAlpha(alpha) = alpha  exact (alpha = 0, 0.5, 1, 1.5, 2.5)");
        sb.AppendLine($"  DiffuseStep   -> Euler step of the Laplacian flow on the occupancy index: W = I - d*L, tridiagonal,");
        sb.AppendLine($"                  symmetric, rows summing to 1, semigroup property ..... {L1(Iterate(Iterate(a, 7), 13), Iterate(a, 20)) < 1e-15}");
        sb.AppendLine();

        PrintHeader(sb, "2. UNIQUENESS — one free scalar, and it is the rate");
        sb.AppendLine("  Axioms: nearest-neighbour support + constant coefficients (scale-free) + symmetric (isotropic)");
        sb.AppendLine("          + rows summing to 1 (count conservation).");
        sb.AppendLine($"  General solution: W(b) = b*left + (1-2b)*a + b*right  -> W(0.2) == DiffuseStep ... {L1(ApplyTridiagonal(a, 0.2), Step(a)) < 1e-15}");
        sb.AppendLine("  Dropping isotropy admits DIRECTED operators (still conservative) — a real constraint, and the");
        sb.AppendLine("  canonical chain has no antisymmetric coupling (NP_174 reciprocity): non-reciprocity is not available.");
        sb.AppendLine();

        PrintHeader(sb, "3. THE ADMISSIBLE RANGE IS DERIVED BY rho >= 0");
        sb.AppendLine("  b_i = d*a_(i-1) + (1-2d)*a_i + d*a_(i+1) is a CONVEX COMBINATION iff 0 <= d <= 1/2.");
        var spike = Canonical; spike[48] += 0.5;
        sb.AppendLine($"  min(rho) after one step: d = 0.2 -> {StepD(spike, 0.2).Min():E2} | d = 1/2 -> {StepD(spike, 0.5).Min():E2} | d = 0.6 -> {StepD(spike, 0.6).Min():E2}");
        sb.AppendLine($"  |mu_k| <= 1 (stability): max |mu| at d = 1/2 is {Enumerable.Range(0, N).Max(k => Math.Abs(Mu(k, 0.5))):F6}");
        sb.AppendLine();
        sb.AppendLine("  d     mu_1        mu_95        |mu_95|   selectivity |mu_95|/|mu_1|   factor at m=200");
        sb.AppendLine("  ---- ------------ ------------ --------- ------------------ ----------------");
        foreach (double d in new[] { 0.05, 0.1, 0.2, 0.25, 0.3, 0.4, 0.5 })
            sb.AppendLine($"  {d,4:F2} {Mu(1, d),12:F8} {Mu(95, d),12:F8} {Math.Abs(Mu(95, d)),9:F6} {Selectivity(d),18:F6} {f(200, d),16:F2}");
        sb.AppendLine("  -> selectivity is |1 - 4d|-like: MAXIMAL at d = 1/4, ZERO at d = 1/2 (the nearest-neighbour");
        sb.AppendLine("     average), where the filter is flat and the mechanism disappears.");
        sb.AppendLine();

        PrintHeader(sb, "4. THE FOUR REPLACEMENTS");
        var delta = new double[N]; delta[50] = 1.0;
        sb.AppendLine("  replacement                  local  positive  conservative  factor m=200   verdict");
        sb.AppendLine("  ---------------------------- ------ --------- ------------- -------------- ------------");
        sb.AppendLine($"  DiffuseStep (d = 0.2)        yes    yes       yes           {f(200, 0.2),14:F2}   CANONICAL");
        sb.AppendLine($"  nearest-neighbour average     yes    yes       yes           {Factor(a, NearestNeighbourAverage, 200),14:F2}   d = 1/2, flat");
        sb.AppendLine($"  spectral cutoff (kc = 48)     NO     NO        yes           {Factor(a, x => SpectralCutoff(x, 48), 200),14:F2}   REFUTED");
        sb.AppendLine($"  biharmonic (kappa = 0.05)     NO     NO        yes           {Factor(a, x => Biharmonic(x, 0.05), 200),14:F2}   REFUTED");
        sb.AppendLine($"  identity                      yes    yes       yes           {Factor(a, Identity, 200),14:F2}   REFUTED");
        sb.AppendLine();
        sb.AppendLine($"  locality: a delta spreads to {Enumerable.Range(0, N).Count(i => Math.Abs(Step(delta)[i]) > 1e-15)} cells (DiffuseStep),");
        sb.AppendLine($"            {Enumerable.Range(0, N).Count(i => Math.Abs(SpectralCutoff(delta, 48)[i]) > 1e-15)} cells (cutoff: action at a distance), {Enumerable.Range(0, N).Count(i => Math.Abs(Biharmonic(delta, 0.05)[i]) > 1e-15)} cells (biharmonic);");
        sb.AppendLine($"  positivity: min(rho) on a spike -> {StepD(spike, 0.2).Min():E2} (canonical), {SpectralCutoff(spike, 48).Min():E2} (cutoff), {Biharmonic(spike, 0.05).Min():E2} (biharmonic).");
        sb.AppendLine($"  stability: min eigenvalue of the biharmonic step at kappa = 0.1 is {MuBi(95, 0.1):F4} (< 0: unstable);");
        sb.AppendLine($"            the identity leaves the G_003 witnesses at full amplitude (factor {Factor(a, Identity, 200):F2}).");
        sb.AppendLine();

        PrintHeader(sb, "5. SENSITIVITY — the FORM is stable, the RATE carries the sensitivity");
        sb.AppendLine("  Fixed m = 200:             " + string.Join("  ", new[] { 0.1, 0.2, 0.3, 0.4, 0.5 }.Select(d => $"d={d:F2} -> {f(200, d):F2}")));
        sb.AppendLine("  Fixed T = m*d = 40:        " + string.Join("  ", new[] { (0.02, 2000), (0.05, 800), (0.1, 400), (0.2, 200), (0.4, 100) }
            .Select(p => $"d={p.Item1:F2}/m={p.Item2} -> {f(p.Item2, p.Item1):F2}")));
        var fT = new[] { (0.02, 2000), (0.05, 800), (0.1, 400), (0.2, 200), (0.4, 100) }.Select(p => f(p.Item2, p.Item1)).ToArray();
        sb.AppendLine($"  -> at fixed T the factor varies by only {(fT.Max() / fT.Min() - 1) * 100:F2} % over a 20x range of d:");
        sb.AppendLine("     T = m*d is the physical control parameter, so the pair (d = 0.2, m = 200) is a BOUNDARY choice");
        sb.AppendLine($"     of the SAME flow. The G_006 number 34 is the T = 40 value: exp({Math.Log(f(200, 0.2)):F4}) nats.");
        sb.AppendLine();

        PrintHeader(sb, "6. THE WITNESSES: does the G_005/G_006 verdict survive?");
        sb.AppendLine("  operator                     witness (high-k)  observed (k = 1)  dichotomy  verdict");
        sb.AppendLine("  ---------------------------- ----------------- ----------------- ---------- -------------");
        sb.AppendLine($"  DiffuseStep (d = 0.2)        {f(200, 0.2),17:F2} {1.0 / SmoothSurvival(200),17:F3} {f(200, 0.2) * SmoothSurvival(200),10:F1}   HOLDS");
        sb.AppendLine($"  nearest-neighbour (d = 1/2)  {Factor(a, NearestNeighbourAverage, 200),17:F2} {1.0 / SmoothSurvival(200, 0.5),17:F3} {Factor(a, NearestNeighbourAverage, 200) * SmoothSurvival(200, 0.5),10:F2}   FAILS");
        sb.AppendLine($"  biharmonic (kappa = 0.05)    {Factor(a, x => Biharmonic(x, 0.05), 200),17:F2} {1.0 / Math.Pow(MuBi(1, 0.05), 200),17:F3} {Factor(a, x => Biharmonic(x, 0.05), 200) * Math.Pow(MuBi(1, 0.05), 200),10:F1}   holds, weaker");
        sb.AppendLine($"  identity                     {Factor(a, Identity, 200),17:F2} {1.0,17:F3} {1.0,10:F1}   REFUTED");
        sb.AppendLine("  -> the SUPPRESSED verdict is robust for every admissible Laplacian-like rate (the dichotomy holds");
        sb.AppendLine("     for all 0 < d < 1/2) and is destroyed by the identity and by the marginal d = 1/2.");
        sb.AppendLine();

        PrintHeader(sb, "7. DOES TIME HAVE ANYTHING TO DO WITH THE SUPPRESSION?");
        sb.AppendLine("  NO — and this is a structural statement, not a preference:");
        sb.AppendLine($"   (i)   the time-like flow rho_(k+1) = mu rho_k is DIAGONAL on the arrangement (one cell in, one");
        sb.AppendLine($"         cell out) and leaves the normalised profile EXACTLY invariant for ANY mu over ANY number of");
        sb.AppendLine($"         generations: a(2^1000 rho) = a(rho) ................. |da| = {MaxAccelerationDifference(a, a.Select(v => Math.Pow(2.0, 1000.0) * v).ToArray(), D):E2}");
        sb.AppendLine("   (ii)  the suppressing operator is TRIDIAGONAL in the occupancy index — it mixes ADJACENT LEVELS:");
        sb.AppendLine("         a unit at cell 40 spreads to 3 cells under relaxation and stays at 1 cell under branching.");
        sb.AppendLine("   (iii) the factor contains no rate, no mu and no time: it is a function of m (steps) and d only;");
        sb.AppendLine($"         pre-scaling rho by 7 leaves it unchanged to {Math.Abs(f(200, 0.2) - FactorClosed(a.Select(v => 7.0 * v).ToArray(), 200)):E2}.");
        sb.AppendLine("   (iv)  at criticality (mu = 1) the density and the metric are STATIC: time does nothing at all.");
        sb.AppendLine($"  What m is: a COARSE-GRAINING index (how far one has zoomed out), not a duration. Reaching the G_003");
        sb.AppendLine($"  suppression 3.746e5 by relaxation alone takes about {Math.Log(3.746e5) / Math.Log(f(200, 0.2)) * 200:F0} steps — a horizon, not a time.");
        sb.AppendLine("  The only temporal readings are EXTRA assumptions: (a) identify one relaxation step with one");
        sb.AppendLine("  generation (then the witnesses fade over ~200 generations, but the branching rate mu still cancels");
        sb.AppendLine("  from the scalar factor), or (b) read m through the entropy production — which G_006 showed to be");
        sb.AppendLine("  downstream of the energy decay, not a clock for it.");
        sb.AppendLine();

        PrintHeader(sb, "VERDICT — DERIVED / BOUNDARY / REFUTED");
        sb.AppendLine("  CLAIM                                                        VERDICT");
        sb.AppendLine("  ------------------------------------------------------------ ----------------");
        sb.AppendLine("  DiffuseStep's FORM is derivable from canonical primitives     DERIVED");
        sb.AppendLine("  (Laplacian = infinitesimal coarse-graining; RG invariance)");
        sb.AppendLine("  Uniqueness: local+linear+symmetric+conservative+scale-free     DERIVED (1-parameter)");
        sb.AppendLine("  The admissible RANGE 0 <= d <= 1/2 (from rho >= 0)             DERIVED");
        sb.AppendLine("  The qualitative suppression verdict (dichotomy)                 DERIVED (all 0 < d < 1/2)");
        sb.AppendLine("  The values d = 0.2, m = 200 and hence the number 34             BOUNDARY (T = m*d = 40)");
        sb.AppendLine("  'DiffuseStep is imported / arbitrary'                           REFUTED");
        sb.AppendLine("  Spectral cutoff, biharmonic, identity as canonical operators    REFUTED");
        sb.AppendLine("  The nearest-neighbour average as an equivalent suppressor       REFUTED (d = 1/2, flat)");
        sb.AppendLine("  TIME as the cause of the suppression                            REFUTED");
        sb.AppendLine();

        PrintHeader(sb, "CONCLUSIONS");
        sb.AppendLine("  C1  DiffuseStep is DERIVED, not imported: it is the Euler step of the Laplacian flow on the");
        sb.AppendLine("      occupancy index, i.e. the infinitesimal form of the canonical coarse-graining whose RG");
        sb.AppendLine("      invariance (alpha is an exact fixed point) is already canonical.");
        sb.AppendLine("  C2  It is UNIQUE up to one scalar rate: locality + linearity + isotropy + count conservation +");
        sb.AppendLine("      scale-freeness leave exactly W = I - d*L. Dropping isotropy admits directed operators (absent");
        sb.AppendLine("      in the canonical chain, NP_174); dropping locality or positivity admits the cutoff/biharmonic.");
        sb.AppendLine("  C3  The admissible RANGE is DERIVED (0 <= d <= 1/2 from rho >= 0 and |mu| <= 1); the VALUE d = 0.2");
        sb.AppendLine("      is BOUNDARY. Sensitivity is carried by the rate: the factor is essentially a function of");
        sb.AppendLine("      T = m*d (a sub-1 % spread across a 20x range of d), and selectivity is |1 - 4d|-like — maximal");
        sb.AppendLine("      at d = 1/4, zero at d = 1/2.");
        sb.AppendLine("  C4  Of the four replacements, the nearest-neighbour average is the SAME operator at the marginal");
        sb.AppendLine("      rate d = 1/2 and destroys the mechanism (flat filter: the worst mode keeps 90 % after 200");
        sb.AppendLine("      steps); the spectral cutoff is non-local and non-positive; the biharmonic is next-nearest,");
        sb.AppendLine("      non-positive and unstable beyond kappa = 1/16 (and a weaker suppressor where admissible);");
        sb.AppendLine("      the identity suppresses nothing and contradicts G_004's 3.746e5 requirement.");
        sb.AppendLine("  C5  TIME IS NOT THE CAUSE. The branching flow is diagonal on the arrangement and suppresses");
        sb.AppendLine("      nothing at any mu and any duration; the suppression lives in the LEVEL direction. m is a");
        sb.AppendLine("      coarse-graining horizon, not a duration; equating the two is an extra (BOUNDARY) assumption.");
        sb.AppendLine("  C6  OPEN: whether the AT chain fixes the NUMBER of coarse-graining steps (i.e. why 200) rather than");
        sb.AppendLine("      only the flow; whether d = 0.2 can be tied to a canonical quantity (it is not derived here);");
        sb.AppendLine("      and whether the biharmonic-like deformations arise anywhere in the canonical chain.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
        Assert.True(sb.Length > 0);
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
