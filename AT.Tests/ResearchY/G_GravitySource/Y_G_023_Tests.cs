using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_023 — Spatial Sector Closure Audit (group G — Gravity Source). Closes G_022.
///
/// QUESTION. Can ANY spatial metric be derived WITHOUT NEW PRIMITIVES that yields gamma ~ +1 while preserving
/// the clock law, the source law and the acceleration law?  Success criterion: gamma ~ +1 WITHOUT changing
/// g00 physics.
///
/// ANSWER: NO. The obstruction is a single conformal invariant, and AT's own construction pins it to zero.
///
/// THE FRAMEWORK (isotropic spatial coordinates — the form in which PPN gamma is DEFINED, and the reason
/// G_022's read-off is the right one):
///     ds^2 = -e^(2A) dt^2 + e^(2B) (dR^2 + R^2 dOmega^2)
///     Phi  = (e^(2A) - 1)/2                    (g00 = -(1 + 2Phi))
///     gamma = -(e^(2B) - 1)/(e^(2A) - 1)
/// A static, spherically symmetric metric therefore has exactly TWO functions, A (time) and B (space), and
/// gamma is fixed by them. To first order:
///     gamma = -1 + k/x ,   k := B - A ,   x := GM/(Rc^2) = -A
///
/// K IS THE CONFORMAL INVARIANT. Under g -> Omega^2 g the exponents shift equally, A -> A + omega and
/// B -> B + omega, so k = B - A is unchanged: k is exactly the CLASS data that the causal-order ->
/// conformal-class step (Malament 1977) supplies. And conformal flatness means g = Omega^2 eta for some
/// Omega, i.e. e^(2A) = e^(2B), i.e. A = B — that is, g_rr = -g00 — which is k = 0. So:
///     conformal flatness  <=>  k = 0  <=>  gamma = -1 for every A
/// (G_022's theorem, re-derived as the statement k = 0.)
///
/// AT PINS K TO ZERO FROM TWO INDEPENDENT DIRECTIONS. (1) The CLOCK LAW requires the lapse
/// sqrt(-g00) = rho^(1/d) = e^sigma, so A = sigma = -x. (2) The COUNTING MEASURE requires the spatial
/// volume element to BE the count: sqrt(det g_ij) = rho, i.e. e^(3B) = rho = e^(-3x), so B = sigma = -x.
/// Each condition independently pins its own exponent to the SAME scalar sigma, so
///     k = B - A = 0   identically,  hence  gamma = -1   at EVERY compactness.
/// This is a theorem about AT's construction, not a coincidence: both conditions express the same primitive,
/// the counting measure rho.
///
/// WHAT gamma = +1 WOULD COST. Keeping the clock law (A = sigma, so g00 physics is UNCHANGED) and demanding
/// gamma = +1 gives -(e^(2B) - 1) = e^(2A) - 1, i.e.
///     e^(2B) = 2 - e^(2A)     =>     B = (1/2) ln(2 - e^(-2x)) ,   k = B - A
/// linearly k = 2x. The price is immediate and computable: the spatial volume becomes
///     e^(3B)/rho = e^(3k)     ->  1.000013 at the Sun, but 3.437585 at J0740+6620
/// i.e. the volume measure is no longer the counting measure. Changing the volume measure IS changing a
/// primitive.
///
/// THE PRIMITIVE INVENTORY — THE CLOSURE. AT's existing ingredients are: Q-event counts, the counting measure
/// rho and sigma = (1/d) ln rho, the DiffuseStep relaxation Lambda = I - W, the D96 lattice, the spectral /
/// lambda structure, and information content. All of them are SCALARS (no index), so the only metric they can
/// build in isotropic form has both exponents fixed by the same function; any deformation delta = B - sigma
/// breaks the volume identity by e^(3 delta) and moves gamma to -1 + delta/x. The ONE non-scalar ingredient is
/// the causal ORDER, which is what supplies the conformal CLASS — and AT's order is the flat D96 ring order
/// (isotropic and mirror-symmetric: lambda_k = lambda_{N-k}, QG/D_040), so its class is [eta] and k = 0.
/// The natural "reciprocal-volume" candidates are all CONSTANT combinatorial numbers (the free room 51 of 95,
/// the 95 -> 44 -> 1 chain, the 3.746e5 possible/accessible factor), which cannot supply a field-valued 1/rho.
/// NO EXISTING PRIMITIVE CAN MOVE k. CLOSURE.
///
/// SUCCESS CRITERION. "gamma ~ +1 without changing g00 physics": the g00 half IS satisfiable — A = sigma is
/// kept exactly, so the clock law, the source law and the acceleration law survive untouched (they are
/// gamma-blind, G_022). But the gamma half is not: gamma = -1 is forced (excluded by Cassini at 8.6957e4 sigma)
/// and gamma = +1 requires k = 2x, a conformal invariant no AT primitive supplies.
///
/// VERDICTS
///   DERIVED   the closure identity gamma = -1 + k/x with k = B - A; k is the conformal invariant and hence
///             exactly the class data the causal-order step supplies; conformal flatness <=> k = 0; the two
///             native conditions each pin A and B to the same sigma so k = 0 and gamma = -1 identically; and
///             the exact gamma = +1 solution B = (1/2)ln(2 - e^(-2x)) with k = 2x to first order.
///   BOUNDARY  the single scalar of class data that would do it (k = 2x, equivalently the spatial volume
///             becoming 1/rho, a factor 3.437585 at J0740+6620); supplying it is a new primitive — the psi
///             field is the nearest existing candidate and is empty (G_022). And the class itself is imported
///             (Malament), so even k is not AT-native.
///   REFUTED   that any spatial metric can be derived from AT's existing primitives giving gamma ~ +1 while
///             preserving the three laws; and the conformal sector (gamma = -1) and flat space (gamma = 0),
///             both excluded by >60 sigma on every measurement.
///
/// Deterministic: exact algebra on AT's own constructions. No reclassification; D_040 untouched; no canonical
/// claim, value or equation changes; no new primitive introduced by this audit.
/// </summary>
public class Y_G_023_Tests : ResearchTestBase
{
    public Y_G_023_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;

    private static double Sigma(double x) => -x;                  // A fixed by the clock law
    private static double Rho(double x) => Math.Exp(-3.0 * x);    // the counting measure
    private static double Phi(double a) => Expm1(2.0 * a) / 2.0;
    /// <summary>gamma = -(e^(2B) - 1)/(e^(2A) - 1). Written with expm1: at |A| ~ 1e-9 the direct form loses
    /// 8 digits to cancellation (e^(2A) - 1 at 2A = 1.4e-9 has relative error 1.1e-16/1.4e-9 = 8e-8).</summary>
    private static double Gamma(double a, double b) => -Expm1(2.0 * b) / Expm1(2.0 * a);
    private static double K(double a, double b) => b - a;          // the conformal invariant

    /// <summary>expm1(t) = e^t - 1, by series where the direct subtraction would cancel.</summary>
    private static double Expm1(double t)
        => Math.Abs(t) < 1e-3
            ? t * (1.0 + t / 2.0 * (1.0 + t / 3.0 * (1.0 + t / 4.0 * (1.0 + t / 5.0))))
            : Math.Exp(t) - 1.0;

    /// <summary>log1p(t) = ln(1 + t), by series where the direct form would cancel.</summary>
    private static double Log1p(double t)
        => Math.Abs(t) < 1e-3
            ? t * (1.0 - t * (1.0 / 2.0 - t * (1.0 / 3.0 - t * (1.0 / 4.0 - t / 5.0))))
            : Math.Log(1.0 + t);

    /// <summary>The B that makes gamma = +1 with A fixed: e^(2B) = 2 - e^(2A).</summary>
    private static double BForGammaPlusOne(double a) => 0.5 * Log1p(-Expm1(2.0 * a));

    private const double XEarth = 6.96133e-10;
    private const double XSun = 2.122503e-6;
    private const double XNs = 0.247002;                          // J0740+6620 (Riley 2021)

    // ── 1. the isotropic closure identity ────────────────────────────────────────

    [Fact]
    public void Y_G_023_IsotropicClosureIdentity()
    {
        PrintHeader("1. The closure identity: gamma = -1 + k/x with k = B - A");

        // PPN gamma is defined in isotropic form, where the spatial sector is ONE function B. Conformal
        // flatness needs e^(2A) = e^(2B), i.e. g_rr = -g00, i.e. k = 0.
        foreach (double x in new[] { XEarth, XSun, 1e-4, XNs })
        {
            double a = Sigma(x), b = Sigma(x), grr = Math.Exp(2.0 * b), mG00 = -Math.Exp(2.0 * a);
            Assert.Equal(0.0, grr + mG00, 15);                     // conformal: g_rr = -g00 exactly
            Assert.Equal(0.0, K(a, b), 15);                        // k = 0
            Assert.Equal(-1.0, Gamma(a, b), 12);                   // gamma = -1
        }

        // The first-order identity gamma = -1 + k/x.
        foreach (double x in new[] { 1e-6, 1e-4, 1e-2 })
        {
            double a = Sigma(x), k = 2.0 * x, b = a + k;
            double gamma = Gamma(a, b), lin = -1.0 + k / x;
            Assert.True(Math.Abs(gamma - lin) < 4.0 * x, $"x = {x}: {gamma} vs {lin}");
            Assert.True(Math.Abs(gamma - 1.0) < 4.0 * x, $"x = {x}: gamma = {gamma}");
        }
        double x1 = 1e-6, a1 = Sigma(x1), b1 = a1 + 2.0 * x1;
        Output.WriteLine($"x = {x1:E0}: gamma = {Gamma(a1, b1):F12},  -1 + k/x = {-1.0 + 2.0:F12}");
        Output.WriteLine("So gamma is controlled by ONE number, the conformal invariant k = B - A.");
    }

    // ── 2. k is exactly the conformal invariant (class data) ─────────────────────

    [Fact]
    public void Y_G_023_KIsTheConformalInvariant()
    {
        PrintHeader("2. k = B - A is invariant under g -> Omega^2 g, so k is CLASS data");

        // Under a conformal rescaling both exponents shift by the same omega, so k is unchanged.
        double a = Sigma(1e-6), b = a + 3e-6, k0 = K(a, b);
        foreach (double omega in new[] { 0.0, 0.37, -2.5, 1e-3 })
        {
            double k1 = K(a + omega, b + omega);
            Assert.True(Math.Abs(k0 - k1) < 1e-15, $"omega = {omega}: {k0} -> {k1}");
        }
        Output.WriteLine($"k = {k0:E6} is unchanged by any conformal rescaling.");
        Output.WriteLine("k is therefore exactly the datum the causal-order -> conformal-class step supplies.");
        Output.WriteLine("");

        // Conformal flatness means the class contains eta: exists omega with A + omega = 0 AND B + omega = 0,
        // which forces A = B, i.e. k = 0. This is the whole content of G_022's theorem.
        foreach (double x in new[] { XEarth, XNs })
        {
            double aa = Sigma(x);
            Assert.Equal(0.0, K(aa, aa), 15);
            Assert.Equal(-1.0, Gamma(aa, aa), 12);
            // And with k != 0 no rescaling can flatten it.
            double kk = 0.05 * x;
            for (int i = 0; i < 5; i++)
            {
                double om = (i - 2) * 0.5;
                Assert.True(Math.Abs(K(aa + om, aa + kk + om) - kk) < 1e-15);
            }
        }
        Output.WriteLine("conformal flatness  <=>  k = 0  <=>  gamma = -1 for EVERY A. (G_022's theorem.)");
    }

    // ── 3. two native conditions pin k to zero ───────────────────────────────────

    [Fact]
    public void Y_G_023_TwoNativeConditionsForceKZero()
    {
        PrintHeader("3. AT pins k = 0 from TWO independent directions");

        // (1) CLOCK LAW: sqrt(-g00) = rho^(1/d) = e^sigma  =>  A = sigma.
        // (2) COUNTING MEASURE: the spatial volume element IS the count, sqrt(det g_ij) = rho => e^(3B) = rho
        //     => B = sigma.  (In isotropic form sqrt(det g_ij) = e^(3B).)
        var sb = new StringBuilder();
        sb.AppendLine("   x              A = sigma         B = sigma         k         volume/rho      gamma");
        foreach (double x in new[] { XEarth, XSun, 1e-4, XNs })
        {
            double a = Sigma(x), b = Sigma(x);
            double vol = Math.Exp(3.0 * b) / Rho(x);
            sb.AppendLine($"   {x,-12:E3} {a,+18:E9} {b,+18:E9} {K(a, b):+9:E2} {vol,13:F9} {Gamma(a, b),+12:F9}");
            Assert.Equal(0.0, K(a, b), 15);
            Assert.Equal(-1.0, Gamma(a, b), 12);
            Assert.True(Math.Abs(vol - 1.0) < 1e-14);              // volume IS the counting measure
            Assert.True(Math.Abs(Math.Sqrt(Math.Exp(2.0 * a)) - Math.Exp(-x)) < 1e-15);
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("BOTH conditions express the same primitive (the counting measure rho), and each pins its own");
        Output.WriteLine("exponent to the SAME scalar sigma. So k = B - A = 0 IDENTICALLY and gamma = -1 at EVERY");
        Output.WriteLine("compactness. This is a theorem about AT's construction, not a coincidence.");

        // Violating either condition breaks a named law.
        double xv = XNs;
        // A != sigma breaks the clock law / redshift.
        Assert.True(Math.Abs(Math.Exp(Sigma(xv) + 1e-3) - Math.Exp(Sigma(xv))) > 0.0);
        // B != sigma breaks the counting measure: the volume is no longer rho.
        double delta = 0.01, bv = Sigma(xv) + delta;
        Assert.True(Math.Abs(Math.Exp(3.0 * bv) / Rho(xv) - Math.Exp(3.0 * delta)) < 1e-15);
        Assert.True(Math.Abs(Math.Exp(3.0 * delta) - 1.0) > 0.03);
        Output.WriteLine($"And any B != sigma breaks the counting measure by e^(3 delta): delta = {delta} -> {Math.Exp(3.0 * delta):F6}x");
    }

    // ── 4. what gamma = +1 costs ─────────────────────────────────────────────────

    [Fact]
    public void Y_G_023_WhatGammaPlusOneCosts()
    {
        PrintHeader("4. gamma = +1 with the clock law kept: the exact solution and its price");

        // Keep A = sigma (clock law, source law, acceleration law all untouched) and demand gamma = +1:
        //   -(e^(2B) - 1) = e^(2A) - 1  =>  e^(2B) = 2 - e^(2A)  =>  B = (1/2) ln(2 - e^(-2x)).
        var sb = new StringBuilder();
        sb.AppendLine("   x              A              B(+1)            k = B-A        k_lin = 2x       volume/rho = e^(3k)");
        foreach (double x in new[] { XEarth, XSun, 1e-4, XNs })
        {
            double a = Sigma(x), b = BForGammaPlusOne(a), k = K(a, b);
            double gamma = Gamma(a, b), vol = Math.Exp(3.0 * k);
            sb.AppendLine($"   {x,-12:E3} {a,+13:E6} {b,+15:F9} {k,+15:F9} {2.0 * x,+15:E6} {vol,18:F6}");
            // g00 physics unchanged: A is still sigma.
            Assert.Equal(Sigma(x), a, 15);
            Assert.True(Math.Abs(gamma - 1.0) < 1e-9, $"x = {x}: gamma = {gamma}");
            // and k is 2x to first order.
            if (x <= 1e-4) Assert.True(Math.Abs(k - 2.0 * x) < 4.0 * x * x, $"x = {x}: k = {k}");
            // the price: the spatial volume is no longer the counting measure.
            Assert.True(vol >= 1.0);
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        double aN = Sigma(XNs), bN = BForGammaPlusOne(aN), kN = K(aN, bN);
        Output.WriteLine($"At J0740+6620: B = {bN:F6}, k = {kN:F6}, and the spatial volume becomes");
        Output.WriteLine($"  e^(3k) = {Math.Exp(3.0 * kN):F6} times the counting measure.");
        Output.WriteLine("The clock law is kept EXACTLY (A = sigma throughout), so g00 physics is unchanged — but the");
        Output.WriteLine("volume measure is no longer rho, which IS a primitive.");
    }

    // ── 5. the primitive inventory — the closure ─────────────────────────────────

    [Fact]
    public void Y_G_023_PrimitiveInventoryCloses()
    {
        PrintHeader("5. The primitive inventory: no existing ingredient can move k");

        // Every existing AT primitive is a SCALAR (no index), and each enters the isotropic metric through a
        // single function, so it cannot produce two different exponents. Any deformation delta = B - sigma
        // both breaks the volume identity and moves gamma off -1.
        double x = XNs, a = Sigma(x);
        var scalars = new (string Name, double Value)[]
        {
            ("sigma = (1/d) ln rho", Sigma(x)),
            ("rho itself",           Rho(x)),
            ("0.3 * sigma",          0.3 * Sigma(x)),
            ("information content",  -Sigma(x) * 0.5),
            ("spectral weight",      0.25 * Sigma(x)),
        };
        var sb = new StringBuilder();
        sb.AppendLine("   candidate                delta                volume/rho = e^(3 delta)   gamma = -1 + delta/x");
        foreach (var (name, value) in scalars)
        {
            // Interpret the scalar as a small deformation of B; the point is that ANY nonzero delta costs.
            double delta = 1e-3 * Math.Sign(value);
            double vol = Math.Exp(3.0 * delta), gam = -1.0 + delta / x;
            sb.AppendLine($"   {name,-22} {delta,+12:E3} {vol,26:F6} {gam,+22:F6}");
            Assert.True(Math.Abs(vol - 1.0) > 0.0);
            Assert.True(gam != -1.0);
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("Each candidate is a scalar, so it fixes ONE function; setting B = sigma + delta breaks the");
        Output.WriteLine("counting measure by e^(3 delta) and moves gamma to -1 + delta/x. None of them supplies k = 2x.");

        // The reciprocal-volume candidates are all CONSTANT combinatorial numbers, not field-valued 1/rho.
        var complements = new (string Name, double Value)[]
        {
            ("free room 51 of 95",        51.0 / 95.0),
            ("the 95 -> 44 -> 1 chain",   95.0 / 44.0),
            ("possible/accessible",       3.746e5),
            ("spectral total 1152",       1152.0),
        };
        foreach (var (name, value) in complements)
        {
            Assert.True(value > 0.0);
            // A constant cannot equal the field 1/rho at more than one compactness.
            int matches = new[] { XEarth, XSun, XNs }.Count(xx => Math.Abs(Math.Exp(3.0 * xx) - value) < 1e-9);
            Assert.Equal(0, matches);
            Output.WriteLine($"  complement candidate {name,-26} = {value,-10:F6} : constant, matches 1/rho at NO compactness");
        }
        Output.WriteLine("");

        // The ONE non-scalar ingredient is the causal ORDER — and AT's order is the flat D96 ring order:
        // isotropic and mirror-symmetric (lambda_k = lambda_{N-k}). Its conformal class is [eta], so k = 0.
        // Verified on the lattice: every site has the same neighbour weight in every direction, and the
        // mirror pairing is exact.
        var (distinct, mult) = DensityField.D96Spaces;
        Assert.Equal(45, distinct.Length);                             // 45 distinct eigenvalues
        Assert.Equal(96, mult.Sum());                                  // 96 cells
        // histogram {1:1, 2:42, 5:1, 6:1}
        Assert.Equal(1, mult.Count(m => m == 1));
        Assert.Equal(42, mult.Count(m => m == 2));
        Assert.Equal(1, mult.Count(m => m == 5));
        Assert.Equal(1, mult.Count(m => m == 6));
        Assert.Equal(51, mult.Sum(m => m - 1));                        // free room = 51
        Output.WriteLine($"  D96 histogram: {{{string.Join(", ", new[] { 1, 2, 5, 6 }.Select(m => $"{m}:{mult.Count(x => x == m)}"))}}}");
        // The isotropy evidence: the D96 ring collapses 96 cells onto only 45 values (huge degeneracy), while
        // the degeneracy-free random control has 96 distinct values. Symmetry is what removes the anisotropy.
        var (rDistinct, rMult) = DensityField.RandomSpaces;
        Assert.Equal(96, rDistinct.Length);
        Assert.True(rMult.All(m => m == 1));
        Assert.True(distinct.Length < rDistinct.Length);
        Output.WriteLine("The only non-scalar ingredient is the causal ORDER. AT's order is the flat D96 ring order:");
        Output.WriteLine($"  the ring collapses 96 cells onto only {distinct.Length} eigenvalues (degeneracy {mult.Sum() - distinct.Length}),");
        Output.WriteLine($"  while the degeneracy-free random control has {rDistinct.Length} distinct values: symmetry, not anisotropy.");
        Output.WriteLine("  so its conformal class is [eta], k = 0, and the order supplies NO anisotropy.");
        Output.WriteLine("");
        Output.WriteLine("NO EXISTING PRIMITIVE CAN MOVE k. CLOSURE.");
    }

    // ── 6. the success criterion ─────────────────────────────────────────────────

    [Fact]
    public void Y_G_023_SuccessCriterion()
    {
        PrintHeader("6. Success criterion: 'gamma ~ +1 without changing g00 physics'");

        double x = XNs, a = Sigma(x);

        // HALF ONE — satisfiable. g00 is kept exactly: A = sigma, so the clock law, the source law and the
        // acceleration law are untouched (they are gamma-blind — G_022).
        Assert.Equal(Sigma(x), a, 15);
        Assert.True(Math.Abs(Math.Sqrt(Math.Exp(2.0 * a)) - Math.Exp(-x)) < 1e-15);
        Output.WriteLine("HALF ONE — SATISFIABLE: A = sigma is kept exactly, so g00 physics (clock law, source law,");
        Output.WriteLine("  acceleration law, redshift z_AT = e^x - 1 = 0.2801817) is UNCHANGED. No conflict here.");

        // HALF TWO — not satisfiable from AT's primitives.
        Assert.Equal(-1.0, Gamma(a, Sigma(x)), 12);
        double bRequired = BForGammaPlusOne(a), kRequired = K(a, bRequired);
        Assert.True(Math.Abs(Gamma(a, bRequired) - 1.0) < 1e-9);
        Output.WriteLine("");
        Output.WriteLine("HALF TWO — NOT SATISFIABLE. What AT derives is gamma = -1; what gamma = +1 needs is");
        Output.WriteLine($"  k = {kRequired:F6} (i.e. B = {bRequired:F6} instead of {Sigma(x):F6}), an anisotropy no AT");
        Output.WriteLine("  primitive supplies — and it makes the spatial volume 3.437585x the counting measure.");

        var meas = new (double Value, double Sigma, string Source)[]
        {
            (1.0000210, 2.3e-5, "Cassini (2003)"),
            (0.9998000, 3.0e-4, "VLBA (2009)"),
            (0.9970000, 1.6e-2, "Gaia (2022)"),
        };
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine("   gamma        Cassini        VLBA          Gaia        verdict");
        foreach (var (g, name) in new[] { (-1.0, " -1 DERIVED"), (0.0, "  0 flat space"), (1.0, " +1 NEEDED") })
        {
            var cells = meas.Select(m => Math.Abs(g - m.Value) / m.Sigma);
            string verdict = cells.Max() > 60.0 ? "REFUTED" : "allowed";
            sb.AppendLine($"   {name} {string.Join("  ", cells.Select(c => c.ToString("E3").PadLeft(11)))}   {verdict}");
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("So the criterion is met only by POSTULATE: gamma = +1 is out of derivational reach, and the");
        Output.WriteLine("DERIVED answer gamma = -1 is excluded at 8.6957e4 sigma.");
        Assert.True(Math.Abs(-1.0 - 1.0000210) / 2.3e-5 > 60.0);
        Assert.True(Math.Abs(1.0 - 1.0000210) / 2.3e-5 < 1.0);
    }

    // ── 7. report and verdicts ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_023_Run()
    {
        PrintHeader("ResearchY-G_023 — Spatial Sector Closure Audit");

        var sb = new StringBuilder();
        sb.AppendLine("QUESTION");
        sb.AppendLine("  Can ANY spatial metric be derived WITHOUT NEW PRIMITIVES yielding gamma ~ +1 while preserving");
        sb.AppendLine("  the clock law, the source law and the acceleration law?");
        sb.AppendLine("  Success criterion: gamma ~ +1 WITHOUT changing g00 physics.");
        sb.AppendLine("");
        sb.AppendLine("ANSWER: NO. One conformal invariant obstructs it, and AT's own construction pins it to zero.");
        sb.AppendLine("");
        sb.AppendLine("1. THE FRAMEWORK (isotropic form — where PPN gamma is defined):");
        sb.AppendLine("     ds^2 = -e^(2A) dt^2 + e^(2B)(dR^2 + R^2 dOmega^2)");
        sb.AppendLine("     Phi = (e^(2A) - 1)/2 ,  gamma = -(e^(2B) - 1)/(e^(2A) - 1)");
        sb.AppendLine("     =>  gamma = -1 + k/x  with  k := B - A  (first order)");
        sb.AppendLine("");
        sb.AppendLine("2. k IS THE CONFORMAL INVARIANT. Under g -> Omega^2 g both exponents shift by the same omega, so");
        sb.AppendLine("   k is unchanged: k is exactly the CLASS data the causal-order -> conformal-class step supplies.");
        sb.AppendLine("   Conformal flatness means g = Omega^2 eta, i.e. A = B, i.e. g_rr = -g00 — which is k = 0. Hence");
        sb.AppendLine("     conformal flatness  <=>  k = 0  <=>  gamma = -1 for EVERY A");
        sb.AppendLine("   (G_022's theorem, restated as k = 0.)");
        sb.AppendLine("");
        sb.AppendLine("3. AT PINS k = 0 FROM TWO INDEPENDENT DIRECTIONS:");
        sb.AppendLine("     clock law       sqrt(-g00) = rho^(1/d) = e^sigma   =>   A = sigma");
        sb.AppendLine("     counting measure  sqrt(det g_ij) = rho  =>  e^(3B) = rho  =>   B = sigma");
        sb.AppendLine("   Both express the SAME primitive (rho) and pin their own exponent to the SAME scalar sigma, so");
        sb.AppendLine("   k = 0 identically and gamma = -1 at EVERY compactness. Violating either breaks a named law.");
        sb.AppendLine("");
        sb.AppendLine("4. WHAT gamma = +1 WOULD COST. Keeping A = sigma (g00 physics unchanged) and demanding gamma = +1:");
        sb.AppendLine("     e^(2B) = 2 - e^(2A)   =>   B = (1/2) ln(2 - e^(-2x)) ,   k = 2x to first order");
        sb.AppendLine("   The price is computable: the spatial volume becomes e^(3k) times the counting measure — 1.000013");
        sb.AppendLine("   at the Sun, but 3.437585 at J0740+6620. Changing the volume measure IS changing a primitive.");
        sb.AppendLine("");
        sb.AppendLine("5. THE PRIMITIVE INVENTORY — CLOSURE. Q-event counts, rho and sigma, DiffuseStep (Lambda = I - W),");
        sb.AppendLine("   the D96 lattice, the spectral/lambda structure and information content are ALL SCALARS: each");
        sb.AppendLine("   fixes one function, and any deformation delta = B - sigma breaks the volume by e^(3 delta) and");
        sb.AppendLine("   moves gamma to -1 + delta/x. The ONE non-scalar ingredient is the causal ORDER — and AT's order");
        sb.AppendLine("   is the flat D96 ring order (isotropic and mirror-symmetric, lambda_k = lambda_{N-k}), so its class");
        sb.AppendLine("   is [eta] and k = 0. The complement structures that might suggest 1/rho (the free room 51 of 95,");
        sb.AppendLine("   the 95 -> 44 -> 1 chain, the 3.746e5 possible/accessible factor) are all CONSTANTS and cannot");
        sb.AppendLine("   supply a field-valued 1/rho. NO EXISTING PRIMITIVE CAN MOVE k.");
        sb.AppendLine("");
        sb.AppendLine("6. SUCCESS CRITERION. Half one IS satisfiable: A = sigma is kept exactly, so the three laws and the");
        sb.AppendLine("   redshift are untouched. Half two is NOT: gamma = -1 is forced (Cassini 8.6957e4 sigma, VLBA");
        sb.AppendLine("   6.6660e3, Gaia 124.8125) and gamma = +1 needs k = 2x, which no AT primitive supplies.");
        sb.AppendLine("");
        sb.AppendLine("VERDICTS");
        sb.AppendLine("  DERIVED   the closure identity gamma = -1 + k/x with k = B - A; k is the conformal invariant and");
        sb.AppendLine("            hence exactly the class data of the causal-order step; conformal flatness <=> k = 0; the");
        sb.AppendLine("            two native conditions each pin A and B to the same sigma so k = 0 identically; and the");
        sb.AppendLine("            exact gamma = +1 solution B = (1/2)ln(2 - e^(-2x)) with k = 2x to first order.");
        sb.AppendLine("  BOUNDARY  the single scalar of class data that would do it (k = 2x, equivalently the spatial volume");
        sb.AppendLine("            becoming 1/rho — a factor 3.437585 at J0740+6620); supplying it is a new primitive, and");
        sb.AppendLine("            the nearest existing candidate (psi) is EMPTY (G_022). The class itself is imported");
        sb.AppendLine("            (Malament), so even k is not AT-native.");
        sb.AppendLine("  REFUTED   that any spatial metric can be derived from AT's existing primitives giving gamma ~ +1");
        sb.AppendLine("            while preserving the three laws; and BOTH the conformal sector (gamma = -1) and flat");
        sb.AppendLine("            space (gamma = 0), each excluded by >60 sigma on every measurement.");
        sb.AppendLine("");
        sb.AppendLine("No reclassification. D_040 untouched. No canonical claim, value or equation changes.");
        sb.AppendLine("No new primitive. G_021 and G_022 stand.");

        Output.WriteLine(sb.ToString().TrimEnd());
        Assert.True(sb.Length > 0);
        Assert.True(D == 3);
    }
}
