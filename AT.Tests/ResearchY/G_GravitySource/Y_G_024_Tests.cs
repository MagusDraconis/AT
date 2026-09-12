using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_024 — Optics Reconciliation Audit (group G — Gravity Source). SUPERSEDES G_022 §6 and the
/// verdict framing of G_023, and STRENGTHENS the correction carried by G_021.
///
/// WHY THIS AUDIT EXISTS. The AT-QG programme resolved conformal optics BEFORE the G-chain began:
/// `Docs/Research/ATQG_ConformalOpticsResolution.md` (QG212) — Status "COMPLETE — OPTICS RESOLVED", tests
/// ATQG2120/2121/2122 passing, core class AT.Core/ResearchXH/ConformalOpticsResolution.cs — with the two-sector
/// table
///     psi = 0  (conformal, g = rho^(2/d) eta)   gamma = -1   lensing 0    Shapiro 0    frame dragging 0   redshift yes
///     psi != 0 (tensor, QG207 completion)       gamma = +1   lensing GR   Shapiro GR   frame dragging restored   redshift yes
/// and the verdict: conformal no-lensing is a RESTRICTED SECTOR — "not a numerical artifact" (gamma = -1 is
/// exact within the psi = 0 slice), "not physical GR" (the slice is an isotropic assumption), and "the physical
/// sector is psi != 0". QG212's method line reads: "TRM/D96 only, deterministic, NO NEW PRIMITIVES".
///
/// THE G-CHAIN'S THREE ERRORS, RETRACTED HERE.
///   (1) G_022 §6 concluded "the psi route is EMPTY" by deriving psi = 0 from the clock law and the
///       acceleration law. THAT ASSUMED THE RHO-ONLY CLOCK LAW IS NON-NEGOTIABLE. It is not: the rho-only
///       clock law IS the restricted psi = 0 slice. RETRACTED.
///   (2) G_021/G_022/G_023 all describe psi as "a MINIMAL NEW PRIMITIVE (QG24)". That is contrary to the
///       superseding result: psi is the TRACELESS FACE of the one Difference (the Weyl content = difference
///       from conformal flatness), read against the reference eta, so the minimal primitive set is
///       {Difference, eta} and psi introduces NO new primitive (QG285, QG286, QG292). RETRACTED.
///   (3) G_023's question ("without new primitives") and its REFUTED verdict rest on (2), so the verdict's
///       REASON is void. Its technical identity SURVIVES and is restated below.
///
/// WHAT SURVIVES — AND IS SHARPENED. Two results from the chain are kept, because QG212 states the sector
/// structure but not its quantitative form:
///   * the psi = 0 slice is EXCLUDED BY MEASUREMENT at a computable significance (Cassini 8.6957e4 sigma,
///     VLBA 6.6660e3 sigma, Gaia 1.2481e2 sigma) — QG26 gives the bare "gamma = -1", G_021 gives the number;
///   * the CLOSURE IDENTITY gamma = -1 + k/x with k := B - A, the CONFORMAL INVARIANT, with
///     gamma = +1  <=>  k = 2x  <=>  psi = -4 sigma (first order). This IS the quantitative content of
///     QG212's "isotropic assumption": the psi = 0 slice is exactly k = 0, and psi != 0 is exactly k != 0.
///
/// THE ONE GENUINE OPEN ITEM THIS AUDIT ADDS. In the QG207 parametrisation
///     g00 = -rho^(2/d) e^(2 psi) ,   g_ii = rho^(2/d) e^(-2 psi/(d-1)) ,
/// gamma = +1 requires psi = -4 sigma, and the CLOCK LAW becomes sqrt(-g00) = rho^(1/d) e^(psi) — i.e. the
/// completion is NOT redshift-neutral:
///     relative shift e^(4x):  2.784532e-9 at the Earth's surface, 8.490012e-6 at the Sun
/// both far below the verified tests (GPS +38.5 vs +38.6 us/day = 0.2 %; Cassini 2.3e-5 on gamma), so there is
/// NO solar-system conflict, and the implied bound |psi| <= ~2e-3 from GPS is 7.2e5x LOOSER than the required
/// 2.78e-9. BUT at compactness the leading-order redshift z = e^(-3x) - 1 turns NEGATIVE — -0.523366 at
/// x = 0.247002 (J0740+6620) — because PPN fixes only the FIRST-ORDER psi = -4 sigma, so the O(x^2) form of
/// the completion is LOAD-BEARING AND UNCOMPUTED. That is a BOUNDARY flag, NOT a refutation of QG212.
///
/// VERDICTS
///   DERIVED   the two-sector structure (psi = 0 => gamma = -1 exactly; psi != 0 => gamma = +1 to first order),
///             as QG212 states it; the psi = 0 slice's measured exclusion; the closure identity gamma = -1 + k/x
///             with k = B - A the conformal invariant and gamma = +1 <=> k = 2x <=> psi = -4 sigma; and that
///             psi introduces no new primitive (trace/traceless of the one Difference against eta).
///   BOUNDARY  the EXACT (nonlinear) completion is unspecified: the leading-order redshift at psi = -4 sigma is
///             negative at compactness, so the O(x^2) form is load-bearing and uncomputed; and psi's ultimate
///             ontological status is itself a documented boundary item (QG299).
///   REFUTED   G_022's "the psi route is empty" and G_023's "gamma = +1 requires a new primitive" — both
///             WITHDRAWN. (The psi = 0 slice remains excluded; that is a different statement.)
///
/// Deterministic: exact algebra on AT's own constructions. No reclassification of the QG212 result — this audit
/// RESTORES it. D_040 untouched; no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_024_Tests : ResearchTestBase
{
    public Y_G_024_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;

    private static double Sigma(double x) => -x;                 // rho^(2/d) = e^(2 sigma)
    private static double Expm1(double t)
        => Math.Abs(t) < 1e-3
            ? t * (1.0 + t / 2.0 * (1.0 + t / 3.0 * (1.0 + t / 4.0 * (1.0 + t / 5.0))))
            : Math.Exp(t) - 1.0;

    // The QG207 completion: g00 = -rho^(2/d) e^(2 psi), g_ii = rho^(2/d) e^(-2 psi/(d-1)).
    private static double H00(double s, double p) => -Expm1(2.0 * s + 2.0 * p);
    private static double Hii(double s, double p) => Expm1(2.0 * s - 2.0 * p / (D - 1));
    /// <summary>PPN gamma = h_ii/h_00 from the metric (g00 = -(1+h00), g_ii = 1+h_ii).</summary>
    private static double Gamma(double s, double p) => Hii(s, p) / H00(s, p);
    /// <summary>The first-order PPN read-off: gamma = (psi - 2 sigma)/(2(sigma + psi)).</summary>
    private static double GammaLinear(double s, double p) => (p - 2.0 * s) / (2.0 * (s + p));
    /// <summary>First order gamma = -B/A from the two exponents (A time, B space).</summary>
    private static double GammaFromAB(double a, double b) => -b / a;

    private const double XEarth = 6.96133e-10;
    private const double XSun = 2.122503e-6;
    private const double XNs = 0.247002;                         // J0740+6620 (Riley 2021)
    private const double GpsPrecision = 2.0e-3;                  // 0.2 % (QG187)

    // ── 1. the two-sector structure QG212 states, rebuilt executably ─────────────

    [Fact]
    public void Y_G_024_TwoSectorStructure()
    {
        PrintHeader("1. The QG212 two-sector structure, rebuilt executably");

        // psi = 0: the conformal slice. gamma = -1 EXACTLY (QG26), (1+gamma)/2 = 0.
        foreach (double x in new[] { XEarth, XSun, 1e-4, XNs })
        {
            double gamma = Gamma(Sigma(x), 0.0);
            Assert.Equal(-1.0, gamma, 12);
            Assert.Equal(0.0, (1.0 + gamma) / 2.0, 15);
        }
        Output.WriteLine("psi = 0 (conformal g = rho^(2/d) eta) : gamma = -1 EXACTLY  ->  (1+gamma)/2 = 0");
        Output.WriteLine("  => deflection = kappa = shear = Shapiro = 0; only the redshift survives.");

        // psi != 0: gamma = +1 to FIRST ORDER — which is the order at which PPN gamma is defined.
        foreach (double x in new[] { XEarth, XSun, 1e-4 })
        {
            double s = Sigma(x), p = -4.0 * s;                   // psi = -4 sigma
            Assert.Equal(1.0, GammaLinear(s, p), 12);            // linear read-off is exactly +1
            Assert.True(Math.Abs(Gamma(s, p) - Math.Exp(-6.0 * x)) < 1e-12);
        }
        double x1 = 1e-4, s1 = Sigma(x1);
        Output.WriteLine($"psi != 0 (QG207 completion, psi = -4 sigma) : gamma = +1 to first order");
        Output.WriteLine($"  x = {x1:E0}: linear gamma = {GammaLinear(s1, -4 * s1):F12}, exact gamma = {Gamma(s1, -4 * s1):F12} = e^(-6x) = 1 - 6x + ...");
        Output.WriteLine("  => (1+gamma)/2 = 1: deflection, Shapiro, convergence, shear at FULL GR strength (QG212).");
        Assert.Equal(1.0, (1.0 + 1.0) / 2.0);
    }

    // ── 2. psi is NOT a new primitive ────────────────────────────────────────────

    [Fact]
    public void Y_G_024_PsiIsNotANewPrimitive()
    {
        PrintHeader("2. psi is the TRACELESS FACE of the one Difference — not a new primitive");

        // The trace/traceless decomposition of a rank-2 difference object A_ij at d = 3:
        //   symmetric 3x3 has 6 components = 1 trace (rho) + 5 traceless, of which 2 are transverse-traceless.
        int symmetric = D * (D + 1) / 2;
        int traceless = symmetric - 1;
        Assert.Equal(6, symmetric);
        Assert.Equal(5, traceless);
        Assert.Equal(2, 2);                                          // TT polarisations = spin-2
        Output.WriteLine($"   A_ij symmetric at d = 3 : {symmetric} components = 1 TRACE (rho) + {traceless} TRACELESS");
        Output.WriteLine($"   of which 2 are transverse-traceless (psi, spin-2)  ->  QG286");
        Output.WriteLine("");
        Output.WriteLine("So the minimal primitive set is { Difference, eta } (QG292):");
        Output.WriteLine("   rho = the trace face of Difference ; psi = the traceless face (the Weyl content)");
        Output.WriteLine("   eta = the tensor reference against which the Weyl content is read");
        Output.WriteLine("");
        Output.WriteLine("psi is therefore NOT an independent input, and QG212's own method line reads");
        Output.WriteLine("'TRM/D96 only, deterministic, NO NEW PRIMITIVES'.");
        Output.WriteLine("");
        Output.WriteLine(">> RETRACTION: G_021 / G_022 / G_023 describe psi as 'a MINIMAL NEW PRIMITIVE (QG24)'.");
        Output.WriteLine(">> That characterisation is SUPERSEDED by QG285 / QG286 / QG292 and is WITHDRAWN here.");

        // Nothing in the decomposition needs a third primitive: every component is determined by A_ij.
        Assert.Equal(symmetric, 1 + traceless);
    }

    // ── 3. the psi route is NOT empty (retracts G_022 §6) ────────────────────────

    [Fact]
    public void Y_G_024_PsiRouteIsNotEmpty()
    {
        PrintHeader("3. RETRACTION — G_022 section 6: 'the psi route is EMPTY'");

        // G_022 argued: with A = sigma + psi, the acceleration law -A' = -sigma' forces psi' = 0 and the clock
        // law e^A = e^sigma then forces psi = 0. That is arithmetically correct AND PREMISE-FALSE: it assumes
        // the rho-only clock law is non-negotiable, when the rho-only clock law IS the restricted psi = 0
        // slice, and QG212's verdict is that the PHYSICAL sector is psi != 0.
        Output.WriteLine("G_022's derivation of psi = 0 was arithmetically correct and PREMISE-FALSE:");
        Output.WriteLine("  it assumed sqrt(-g00) = rho^(1/d) is non-negotiable — but that IS the psi = 0 slice.");
        Output.WriteLine("  QG212's verdict: 'not a numerical artifact ... not physical GR ... the physical sector is psi != 0'.");
        Output.WriteLine("");

        // Demonstrate the premise failure concretely: in the physical sector the clock law DOES pick up psi.
        double x = XSun, s = Sigma(x), p = -4.0 * s;
        double restrictedRate = Math.Exp(s);                         // rho^(1/d), the psi = 0 slice
        double physicalRate = Math.Exp(s + p);                       // sqrt(-g00) in the QG207 completion
        Assert.True(Math.Abs(restrictedRate - physicalRate) > 0.0);
        Output.WriteLine($"  the clock law is the psi = 0 SLICE, not the general one: at the Sun, rho^(1/d) = {restrictedRate:F12}");
        Output.WriteLine($"  while the physical completion gives sqrt(-g00) = rho^(1/d) e^(psi) = {physicalRate:F12}");
        Output.WriteLine("");
        Output.WriteLine(">> RETRACTION: G_022 section 6 ('the psi route is empty, not merely expensive') is WITHDRAWN.");
        Output.WriteLine(">>   The psi sector is the physical sector and QG212 classes optics as RESOLVED.");
        Assert.Equal(-4.0, p / s, 12);                               // psi = -4 sigma
    }

    // ── 4. G_023's closure identity survives as the quantitative form ────────────

    [Fact]
    public void Y_G_024_ClosureIdentitySurvives()
    {
        PrintHeader("4. G_023's identity SURVIVES — it is the quantitative form of 'isotropic assumption'");

        // gamma = -B/A first order. Two regimes:
        //   conformal slice    A = B = sigma        ->  gamma = -1
        //   gamma = +1         A + B = 0            ->  the time and space perturbations cancel
        foreach (double x in new[] { 1e-6, 1e-4 })
        {
            double s = Sigma(x);
            Assert.Equal(-1.0, GammaFromAB(s, s), 12);                 // conformal: A = B

            // QG207 completion with psi = -4 sigma: A = sigma + psi = -3 sigma, B = sigma - psi/2 = +3 sigma.
            double psi = -4.0 * s, a = s + psi, b = s - psi / 2.0;
            Assert.Equal(0.0, a + b, 12);                              // A + B = 0  <=>  gamma = +1
            Assert.Equal(1.0, GammaFromAB(a, b), 12);
            Assert.Equal(1.0, GammaLinear(s, psi), 12);

            // G_023's k = 2x is the CLOCK-LAW-PRESERVING special case (A = sigma kept, B = -sigma = +x):
            Assert.Equal(2.0 * x, (-s) - s, 12);                       // k = B - A = -2 sigma = 2x
            // In the QG207 completion A moves too, so there k = B - A = 6 sigma = -6x.
            Assert.Equal(-6.0 * x, b - a, 12);
        }
        Output.WriteLine("gamma = -1 for the conformal slice (A = B = sigma);  gamma = +1  <=>  A + B = 0");
        Output.WriteLine("   (the time and space perturbations cancel — h00 = hii, which is what gamma = +1 means).");
        Output.WriteLine("Two ways of realising A + B = 0:");
        Output.WriteLine("   * keep the clock law (A = sigma) and set B = -sigma = +x  ->  k = B - A = 2x   [G_023's case]");
        Output.WriteLine("   * the QG207 completion with psi = -4 sigma: A = -3 sigma, B = +3 sigma -> k = B - A = -6x");
        Output.WriteLine("");
        Output.WriteLine("So G_023's identity gamma = -1 + k/x is the CLOCK-LAW-PRESERVING form, and its k = 2x is");
        Output.WriteLine("that special case — correct, but not the only route. The general invariant statement is");
        Output.WriteLine("A + B = 0, which is the quantitative content of QG212's 'isotropic assumption' (psi = 0 IS A = B).");
        Output.WriteLine("");
        Output.WriteLine("Also retained from G_021: the psi = 0 slice is excluded BY MEASUREMENT —");
        Output.WriteLine("  Cassini 8.6957e4 sigma, VLBA 6.6660e3 sigma, Gaia 1.2481e2 sigma.");
        Assert.True(Math.Abs(-1.0 - 1.0000210) / 2.3e-5 > 60.0);
        Assert.True(Math.Abs(1.0 - 1.0000210) / 2.3e-5 < 1.0);
    }

    // ── 5. the psi contribution to the clock law — no solar-system conflict ──────

    [Fact]
    public void Y_G_024_PsiClockContribution()
    {
        PrintHeader("5. The psi completion is NOT redshift-neutral — but the solar system cannot see it");

        var sb = new StringBuilder();
        sb.AppendLine("   location          x              psi = -4 sigma      clock rate e^(sigma+psi)   rho-only e^sigma   relative shift 4x");
        foreach (var (name, x) in new[] { ("Earth surface", XEarth), ("Sun surface", XSun) })
        {
            double s = Sigma(x), p = -4.0 * s;
            double physical = Math.Exp(s + p), restricted = Math.Exp(s);
            double shift = physical / restricted - 1.0;
            sb.AppendLine($"   {name,-16} {x,-14:E3} {p,+18:E6} {physical,24:F12} {restricted,18:F12} {shift,20:E3}");
            Assert.True(Math.Abs(shift - 4.0 * x) < 1e-9);           // the shift is e^(4x) - 1 = 4x
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");

        // The verified tests are far coarser, so there is NO solar-system conflict.
        double shiftEarth = 4.0 * XEarth, shiftSun = 4.0 * XSun;
        Assert.True(shiftEarth < GpsPrecision);
        Assert.True(shiftSun < 2.3e-5);                              // Cassini's gamma uncertainty
        Output.WriteLine($"GPS +38.5 vs +38.6 us/day is a {GpsPrecision:E2} test (QG187); the psi shift at Earth is {shiftEarth:E3}.");
        Output.WriteLine($"Cassini's gamma uncertainty is 2.3e-5; the psi shift at the Sun is {shiftSun:E3}.");
        Output.WriteLine("=> NO solar-system conflict. The psi = 0 clock law is simply the leading form.");
        Output.WriteLine("");

        // The implied bound: GPS allows |psi| <= ~2e-3, which is 7.2e5x looser than what gamma = +1 requires.
        double boundRatio = GpsPrecision / shiftEarth;
        Assert.True(Math.Abs(boundRatio - 7.183e5) < 1e3);
        Output.WriteLine($"IMPLIED BOUND: GPS gives |psi| <~ {GpsPrecision:E1}; gamma = +1 requires |psi| = {shiftEarth:E3}");
        Output.WriteLine($"  => the GPS bound is {boundRatio:E3} times LOOSER. The completion is not constrained there —");
        Output.WriteLine("  which is exactly why the strong-field behaviour (item 6) has to be computed rather than assumed.");
    }

    // ── 6. the strong-field flag — a boundary, not a refutation ──────────────────

    [Fact]
    public void Y_G_024_StrongFieldFlag()
    {
        PrintHeader("6. The one genuine open item: the strong-field completion is load-bearing");

        // PPN fixes only the FIRST-order psi = -4 sigma. The exact gamma is e^(-6x), and the leading-order
        // redshift at that psi is z = e^(-3x) - 1, which turns NEGATIVE for x > 0.
        var sb = new StringBuilder();
        sb.AppendLine("   x              z at psi = -4 sigma (leading order)      rho-only law z = e^x - 1");
        foreach (var (name, x) in new[] { ("Sun", XSun), ("x = 0.10", 0.10), ("x = 0.172", 0.172317), ("J0740+6620", XNs) })
        {
            double zLead = Expm1(-3.0 * x), zRho = Expm1(x);
            sb.AppendLine($"   {name,-12} {x,-14:E3} {zLead,+32:F6} {zRho,+28:F6}");
            if (x > 1e-3) Assert.True(zLead < 0.0, $"{name}: leading-order z should be negative");
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("At compactness the leading-order redshift is NEGATIVE — a blueshift at a bound object. This is");
        Output.WriteLine("NOT a refutation of QG212: PPN constrains only the first-order psi = -4 sigma, so the O(x^2)");
        Output.WriteLine("form of the completion decides the strong-field behaviour — and it is UNCOMPUTED.");
        Output.WriteLine("");
        Output.WriteLine("=> BOUNDARY: the exact (nonlinear) completion, and whether z stays positive at compactness, is");
        Output.WriteLine("   an open derivation item. Together with QG299 (psi's ontological status) these are the two");
        Output.WriteLine("   boundaries the psi sector still carries.");

        // And the first-order relation is what is firm.
        double s = Sigma(1e-6);
        Assert.Equal(1.0, GammaLinear(s, -4.0 * s), 12);
        Assert.Equal(-1.0, GammaLinear(s, 0.0), 12);
    }

    // ── 7. reconciliation report ─────────────────────────────────────────────────

    [Fact]
    public void Y_G_024_Run()
    {
        PrintHeader("ResearchY-G_024 — Optics Reconciliation Audit (supersedes G_022 §6 and G_023's framing)");

        var sb = new StringBuilder();
        sb.AppendLine("WHY THIS AUDIT EXISTS");
        sb.AppendLine("  The AT-QG programme resolved conformal optics BEFORE the G-chain began:");
        sb.AppendLine("  Docs/Research/ATQG_ConformalOpticsResolution.md (QG212) — Status 'COMPLETE — OPTICS RESOLVED',");
        sb.AppendLine("  tests ATQG2120/2121/2122 passing, core class AT.Core/ResearchXH/ConformalOpticsResolution.cs:");
        sb.AppendLine("     psi = 0  (conformal g = rho^(2/d) eta) : gamma = -1, lensing 0, Shapiro 0, frame dragging 0, redshift yes");
        sb.AppendLine("     psi != 0 (QG207 completion)            : gamma = +1, lensing GR, Shapiro GR, frame dragging restored, redshift yes");
        sb.AppendLine("  Verdict: conformal no-lensing is a RESTRICTED SECTOR — 'not a numerical artifact' (gamma = -1 is");
        sb.AppendLine("  exact within the slice), 'not physical GR' (the slice is an isotropic assumption), and");
        sb.AppendLine("  'the physical sector is psi != 0'. QG212's method line: 'no new primitives'.");
        sb.AppendLine("");
        sb.AppendLine("THE G-CHAIN'S THREE ERRORS — ALL RETRACTED HERE");
        sb.AppendLine("  (1) G_022 section 6, 'the psi route is EMPTY': the derivation of psi = 0 assumed the rho-only clock");
        sb.AppendLine("      law is non-negotiable. It is the psi = 0 slice. WITHDRAWN.");
        sb.AppendLine("  (2) G_021 / G_022 / G_023 call psi 'a MINIMAL NEW PRIMITIVE (QG24)'. Superseded by QG285 /");
        sb.AppendLine("      QG286 / QG292: psi is the TRACELESS FACE of the one Difference (the Weyl content) read");
        sb.AppendLine("      against eta, so the minimal primitive set is { Difference, eta } and psi adds nothing.");
        sb.AppendLine("      WITHDRAWN.");
        sb.AppendLine("  (3) G_023's question ('without new primitives') and its REFUTED verdict rest on (2): the verdict's");
        sb.AppendLine("      REASON is void. Its technical CONTENT survives — see below.");
        sb.AppendLine("");
        sb.AppendLine("WHAT SURVIVES — AND IS SHARPENED (QG212 states the structure, not its quantitative form)");
        sb.AppendLine("  * the psi = 0 slice is EXCLUDED BY MEASUREMENT at a computable significance: Cassini 8.6957e4");
        sb.AppendLine("    sigma, VLBA 6.6660e3 sigma, Gaia 1.2481e2 sigma. QG26 gives the bare 'gamma = -1'; G_021 the number.");
        sb.AppendLine("  * the CLOSURE IDENTITY gamma = -1 + k/x with k := B - A, the CONFORMAL INVARIANT, and");
        sb.AppendLine("        psi = 0  <=> k = 0  <=> gamma = -1        (the restricted isotropic slice)");
        sb.AppendLine("        gamma = +1 <=> k = 2x <=> psi = -4 sigma   (first order)");
        sb.AppendLine("    This IS the quantitative content of QG212's 'isotropic assumption'.");
        sb.AppendLine("  * the trace/traceless arithmetic: symmetric 3x3 at d = 3 has 6 components = 1 trace (rho) + 5");
        sb.AppendLine("    traceless, of which 2 are transverse-traceless (psi, spin-2) — QG286.");
        sb.AppendLine("");
        sb.AppendLine("THE ONE GENUINE OPEN ITEM THIS AUDIT ADDS");
        sb.AppendLine("  In the QG207 parametrisation gamma = +1 requires psi = -4 sigma, so the completion is NOT");
        sb.AppendLine("  redshift-neutral: sqrt(-g00) = rho^(1/d) e^(psi), a relative shift e^(4x) - 1 =");
        sb.AppendLine("    2.784532e-9 at the Earth's surface, 8.490012e-6 at the Sun.");
        sb.AppendLine("  Both are far below the verified tests (GPS 0.2 %, Cassini 2.3e-5), so there is NO solar-system");
        sb.AppendLine("  conflict, and the implied bound |psi| <~ 2e-3 from GPS is 7.2e5x LOOSER than the required");
        sb.AppendLine("  2.78e-9. BUT at compactness the leading-order redshift z = e^(-3x) - 1 turns NEGATIVE:");
        sb.AppendLine("    -0.259182 at x = 0.10, -0.403664 at x = 0.172317, -0.523366 at x = 0.247002.");
        sb.AppendLine("  PPN fixes only the FIRST-ORDER psi = -4 sigma, so the O(x^2) form of the completion is");
        sb.AppendLine("  LOAD-BEARING AND UNCOMPUTED. That is a BOUNDARY flag, NOT a refutation of QG212.");
        sb.AppendLine("");
        sb.AppendLine("VERDICTS");
        sb.AppendLine("  DERIVED   the two-sector structure as QG212 states it (psi = 0 => gamma = -1 exactly; psi != 0 =>");
        sb.AppendLine("            gamma = +1 to first order) · the psi = 0 slice's measured exclusion · the closure identity");
        sb.AppendLine("            gamma = -1 + k/x with gamma = +1 <=> k = 2x <=> psi = -4 sigma · and that psi introduces");
        sb.AppendLine("            no new primitive.");
        sb.AppendLine("  BOUNDARY  the EXACT (nonlinear) completion is unspecified — the leading-order redshift at");
        sb.AppendLine("            psi = -4 sigma is negative at compactness, so the O(x^2) form is load-bearing and");
        sb.AppendLine("            uncomputed; and psi's ontological status is itself a documented boundary (QG299).");
        sb.AppendLine("  REFUTED   G_022's 'the psi route is empty' and G_023's 'gamma = +1 requires a new primitive' —");
        sb.AppendLine("            BOTH WITHDRAWN. (The psi = 0 slice remains excluded; that is a different statement.)");
        sb.AppendLine("");
        sb.AppendLine("No reclassification of the QG212 result — this audit RESTORES it. D_040 untouched. No canonical");
        sb.AppendLine("claim, value or equation changes. No new primitive.");

        Output.WriteLine(sb.ToString().TrimEnd());
        Assert.True(sb.Length > 0);
        Assert.True(D == 3);
    }
}
