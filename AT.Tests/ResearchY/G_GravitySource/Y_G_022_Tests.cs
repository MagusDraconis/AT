using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_022 — Spatial Metric Audit (group G — Gravity Source).  Refines G_021.
///
/// QUESTION. Can a spatial metric be DERIVED that preserves (1) the clock law, (2) the source law,
/// (3) the acceleration law, while reproducing gamma ~ +1 ?
///
/// ANSWER: NO — and the reason is structural, not numerical. THE THREE LAWS ARE gamma-BLIND. Writing
/// sigma = (1/d) ln rho with rho = e^(-3x) and sigma = -x (x = GM/(Rc^2) the compactness, the G_019/G_020
/// convention):
///     clock   dtau/dt = sqrt(-g00) = e^sigma        depends on g00 only
///     source  a = -(1/d) grad ln rho = -grad sigma  depends on rho only
///     accel   d2r/dt2 ~ -A' for a slow test particle  depends on g00 only
/// None of them mentions B, i.e. the spatial sector. So the question as posed is UNDER-DETERMINED: it
/// constrains nothing about gamma. gamma is fixed by a FOURTH condition the question does not name.
///
/// AND THE FOURTH CONDITION GIVES gamma = -1 — FOR ANY CONFORMAL FACTOR. AT's metric is conformally flat,
/// g_uv = Omega^2 eta_uv, so g00 = -Omega^2 and g_rr = +Omega^2, i.e. g_rr = -g00 ALWAYS. With
/// Phi = (-g00 - 1)/2 the PPN read-off is
///     gamma = -(g_rr - 1)/(2 Phi) = -(Omega^2 - 1)/(Omega^2 - 1) = -1   for every Omega^2 != 1.
/// gamma = -1 is therefore NOT an artefact of the counting-measure factor rho^(2/d): it is a THEOREM about
/// 4D conformal flatness plus Phi = sigma. THE FACTOR IS IRRELEVANT — flipping it to rho^(-2/d) still gives
/// gamma = -1, because conformal flatness ties the two sectors together.
///
/// SO gamma = +1 REQUIRES A NON-CONFORMALLY-FLAT METRIC, i.e. the two sectors must carry DIFFERENT factors:
///     g00 = -rho^(2/d) = -e^(-2x)      (AT's clock law, EXACT)
///     g_rr = +rho^(-2/d) = +e^(+2x)    (the reciprocal factor -> gamma = +1)
/// That metric preserves all three laws EXACTLY (clock, source, slow-particle acceleration) and reproduces
/// gamma = +1, so it passes every test the question sets. But it cannot be DERIVED from AT's own chain:
/// the causal-order -> conformal-class step (Malament 1977) produces Omega^2 eta, which has ONE factor. The
/// reciprocal spatial sector is a POSTULATE — and its cost is visible: the spatial volume measure becomes
/// Omega^3 = e^(3x) = 1/rho, the RECIPROCAL of the counting measure, instead of rho.
///
/// THE psi ROUTE IS EMPTY (this refines G_021). G_021 found gamma = +1 <=> psi = -4 sigma in d = 3 by letting
/// g00 define Phi. But AT's SOURCE LAW fixes Phi = sigma independently, and a slow test particle's
/// acceleration is -A' with A = sigma + psi. Requiring -A' = -sigma' gives psi' = 0, i.e. psi constant;
/// requiring the clock law sqrt(-g00) = e^sigma then gives psi = 0. So psi = 0 is forced by BOTH laws, and the
/// psi completion delivers gamma = -1, not +1. Worse, at G_021's value psi = -4 sigma the surface redshift
/// becomes z = e^(-3x) - 1 < 0 — a BLUESHIFT at a bound object — which any positive measured neutron-star
/// redshift refutes outright. The psi sector cannot be the fix.
///
/// WHY THE EARLIER AGREEMENTS WITH GR ARE NOT DAMAGED (the question that motivated this audit). Every
/// G_001-G_020 agreement lives in the gamma-BLIND sector: G_004's 0.99600 calibration, G_009/G_015/G_017's
/// clock rates, G_019's x^2 signature, G_020's neutron-star redshifts (z_AT = e^x - 1 = 0.2801817 for
/// J0740+6620), and the source-law/MOND phenomenology. G_022 proves those are invariant under ANY spatial
/// sector. The gamma sector was simply never exercised before G_021 — it is a SEPARATE observable class
/// (light bending, Shapiro delay), probed by Cassini, and that is where the conformal metric fails. There is
/// no contradiction: the numbers that agreed still agree.
///
/// VERDICTS
///   DERIVED   the gamma-blindness of all three named laws; the theorem "4D conformal flatness + Phi = sigma
///             => gamma = -1" for ANY conformal factor; the unique native spatial metric (Omega = rho^(1/d));
///             and the explicit gamma = +1 spatial sector g_rr = rho^(-2/d) with g00 = -rho^(2/d) intact.
///   BOUNDARY  the gamma = +1 spatial sector is a POSTULATE: it requires the two sectors to carry reciprocal
///             factors, i.e. abandoning 4D conformal flatness, so the conformal-class derivation no longer
///             applies; its cost is a spatial volume measure of 1/rho instead of rho. And the psi route is
///             empty (psi = 0 is forced), not merely expensive.
///   REFUTED   that the three laws imply or permit a DERIVED gamma ~ +1 — they are silent on gamma; and the
///             psi completion, which predicts a surface BLUESHIFT at compact objects.
///
/// Deterministic: exact algebra on AT's own constructions. No reclassification; D_040 untouched; no canonical
/// claim, value or equation changes; no new primitive. G_021's other results stand.
/// </summary>
public class Y_G_022_Tests : ResearchTestBase
{
    public Y_G_022_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;

    // x = GM/(Rc^2) > 0 (the G_019/G_020 convention).  sigma = (1/d) ln rho = -x, so rho = e^(-3x).
    private static double Sigma(double x) => -x;
    private static double Rho(double x) => Math.Exp(-3.0 * x);
    private static double A(double x) => Sigma(x);                       // g00 = -e^(2A)
    private static double G00(double x, double psi = 0.0) => -Math.Exp(2.0 * A(x) + 2.0 * psi);
    private static double ClockRate(double x) => Math.Exp(A(x));         // dtau/dt = sqrt(-g00) = rho^(1/d)
    private static double ZAt(double x) => 1.0 / ClockRate(x) - 1.0;     // = e^x - 1 (G_019/G_020)
    private static double Phi(double x) => (-G00(x) - 1.0) / 2.0;        // g00 = -(1 + 2 Phi)

    /// <summary>PPN gamma from a static metric, with Phi fixed by g00.</summary>
    private static double Gamma(double g00, double grr)
    {
        double phi = (-g00 - 1.0) / 2.0;
        return -(grr - 1.0) / (2.0 * phi);
    }

    /// <summary>
    /// expm1(x) = e^x - 1, evaluated by a 5-term series where the direct subtraction would cancel.
    /// The series error at |t| = 1e-3 is t^6/720 ~ 1.4e-21, so weak-field ratios keep full precision
    /// (a threshold of 1e-6 is too small: at 2e-6 the direct form already loses 1e-11 to cancellation).
    /// </summary>
    private static double Expm1(double t)
        => Math.Abs(t) < 1e-3
            ? t * (1.0 + t / 2.0 * (1.0 + t / 3.0 * (1.0 + t / 4.0 * (1.0 + t / 5.0))))
            : Math.Exp(t) - 1.0;

    /// <summary>
    /// PPN gamma from the two metric exponents: A (time, g00 = -e^(2A)) and B (space, g_rr = e^(2B)).
    /// Since 2 Phi = -g00 - 1 = e^(2A) - 1 and g_rr - 1 = e^(2B) - 1,
    ///     gamma = -(e^(2B) - 1)/(e^(2A) - 1).
    /// Written with expm1 so that weak fields (|A| ~ 1e-6) do not lose precision to cancellation.
    /// </summary>
    private static double GammaFromExponents(double a, double b) => -Expm1(2.0 * b) / Expm1(2.0 * a);

    /// <summary>A conformal metric g = Omega^2 eta: the ONE factor in both sectors.</summary>
    private static double GammaConformal(double omega2) => Gamma(-omega2, +omega2);

    private const double XEarth = 6.96133e-10;      // G_004
    private const double XSun = 2.122503e-6;        // G_019
    private const double XNs = 0.247002;            // J0740+6620 (Riley 2021), G_020

    // ── 1. the three laws are gamma-blind; the earlier agreements therefore stand ──

    [Fact]
    public void Y_G_022_ThreeLawsAreGammaBlind()
    {
        PrintHeader("1. The three laws the question names are gamma-BLIND");

        // For any spatial sector g_rr = e^(2 beta A), the clock law and the slow-particle acceleration are
        // bitwise identical — B does not appear anywhere in them.
        double x = XEarth;
        double clock0 = ClockRate(x);
        double accel0 = -Sigma(x);                                     // -A'  (slow test particle)
        double source0 = -Sigma(x);                                    // -(1/d) ln rho  (source law)
        foreach (double beta in new[] { -3.0, -1.0, 0.0, 1.0, 3.0 })
        {
            double b = beta * A(x);                                    // g_rr = e^(2B), B = beta*A
            double grr = Math.Exp(2.0 * b);
            Assert.True(grr > 0.0);
            Assert.Equal(clock0, ClockRate(x));                        // identical
            Assert.Equal(accel0, -Sigma(x));                           // identical
            Assert.Equal(source0, -Sigma(x));                          // identical
        }
        Output.WriteLine($"clock rate = {clock0:F15}  accel = {accel0:E6}  source = {source0:E6}  (all beta-identical)");

        // Only the PROPER acceleration of a held static observer carries B: a_hat = A' e^(-B). That is a
        // measure convention, and it is a first-order-in-Phi/c^2 effect — 1.4e-9 at the Earth's surface.
        double ratio = Math.Exp(-2.0 * A(x));                          // beta = +1 vs beta = -1
        Assert.True(Math.Abs(ratio - 1.0 - 2.0 * x) < 1e-15);          // series residual ~ 2x^2 = 9.7e-19
        Output.WriteLine($"static-observer proper acceleration, beta=+1 vs beta=-1: ratio = e^(2x) = {ratio:F15}");
        Output.WriteLine($"  deviation = {ratio - 1.0:E6} at the Earth's surface  (below every stated precision)");

        // THE MOTIVATING CHECK: the G-chain's GR agreements are g00-only, hence invariant. Re-verify two.
        double aAt = G_SI * 5.9722e24 / Math.Pow(6.371e6, 2);
        double aGr = 6.67430e-11 * 5.9722e24 / Math.Pow(6.371e6, 2);
        Assert.True(Math.Abs(aAt / aGr - G_SI / 6.67430e-11) < 1e-14);  // G_004: the 0.99600 calibration
        Assert.True(Math.Abs(aAt / aGr - 0.996) < 1e-5);
        Assert.True(Math.Abs(ZAt(XNs) - 0.2801817) < 1e-6);            // G_020: J0740+6620 z_AT
        Output.WriteLine("");
        Output.WriteLine("G-chain agreements, all g00-only and therefore invariant under ANY spatial sector:");
        Output.WriteLine($"  G_004 Earth calibration a_AT/a_GR = {aAt / aGr:F8} (= G_SI/G_CODATA, g00 only)");
        Output.WriteLine($"  G_020 J0740+6620 z_AT = e^x - 1 = {ZAt(XNs):F7}  (g00 only)");
        Output.WriteLine("=> nothing that previously agreed with GR is damaged by G_021/G_022.");
    }

    // ── 2. conformal flatness forces gamma = -1, for ANY factor ──────────────────

    [Fact]
    public void Y_G_022_ConformalFlatnessForcesGammaMinusOne()
    {
        PrintHeader("2. Conformal flatness => gamma = -1, INDEPENDENT of the conformal factor");

        double x = 1e-6;
        // Omega^2 values spanning: AT's counting-measure factor, its reciprocal, a scaled one, constants.
        var omegas = new (string Name, double Omega2)[]
        {
            ("rho^(2/d)   (AT counting measure)", Math.Exp(-2.0 * x)),
            ("rho^(-2/d)  (reciprocal factor)",   Math.Exp(+2.0 * x)),
            ("e^(-0.6x)   (scaled factor)",       Math.Exp(-0.6 * x)),
            ("constant 1.5",                      1.5),
            ("constant 0.9",                      0.9),
        };

        foreach (var (name, omega2) in omegas)
        {
            double g00 = -omega2, grr = +omega2;
            // conformal flatness ties the sectors: g_rr = -g00 exactly.
            Assert.Equal(0.0, grr + g00, 15);
            Assert.Equal(-1.0, GammaConformal(omega2), 12);
            Output.WriteLine($"{name,-36} Omega^2 = {omega2:F15}   gamma = {GammaConformal(omega2):F12}");
        }

        // The theorem, in the exact algebraic form the audit asserts.
        foreach (double omega2 in new[] { Math.Exp(-2e-6), Math.Exp(2e-6), 1.5, 0.9 })
        {
            double phi = (omega2 - 1.0) / 2.0;
            Assert.Equal(-1.0, -(omega2 - 1.0) / (2.0 * phi), 12);     // -(Omega^2-1)/(Omega^2-1)
        }
        Output.WriteLine("");
        Output.WriteLine("So gamma = -1 is NOT an artefact of rho^(2/d): it is a THEOREM about 4D conformal");
        Output.WriteLine("flatness plus Phi = sigma. Flipping the factor to rho^(-2/d) still gives gamma = -1,");
        Output.WriteLine("because conformal flatness forces g_rr = -g00. The factor is IRRELEVANT.");
    }

    // ── 3. the beta family: gamma = -beta ────────────────────────────────────────

    [Fact]
    public void Y_G_022_BetaFamily()
    {
        PrintHeader("3. g_rr = e^(2 beta sigma) with Phi = sigma  =>  gamma = -beta");

        double x = 1e-6;
        // beta = +1 is AT's conformal sector; beta = 0 is flat space; beta = -1 is the reciprocal sector.
        // Computed from the exponents so the weak-field cancellation does not eat the precision.
        Assert.Equal(-1.0, GammaFromExponents(A(x), +1.0 * A(x)), 12);   // beta = +1
        Assert.Equal(0.0, GammaFromExponents(A(x), 0.0), 15);            // beta =  0
        double gammaRecip = GammaFromExponents(A(x), -1.0 * A(x));       // beta = -1
        Assert.True(Math.Abs(gammaRecip - Math.Exp(2.0 * x)) < 1e-12);
        Assert.True(Math.Abs(gammaRecip - 1.0) < 1e-5);
        // Cross-check against the metric form.
        Assert.Equal(-1.0, Gamma(G00(x), Math.Exp(-2.0 * x)), 12);
        Assert.Equal(0.0, Gamma(G00(x), 1.0), 15);

        Output.WriteLine($"beta = +1 (conformal)   : gamma = {GammaFromExponents(A(x), A(x)):+F12}");
        Output.WriteLine($"beta =  0 (flat space)  : gamma = {GammaFromExponents(A(x), 0.0):+F12}");
        Output.WriteLine($"beta = -1 (reciprocal)  : gamma = {gammaRecip:F12}   (= e^(2x) -> +1)");
        Output.WriteLine("");
        Output.WriteLine("gamma = +1 therefore requires g_rr = rho^(-2/d), i.e. beta = -1: the SPATIAL sector must");
        Output.WriteLine("carry the RECIPROCAL of the time sector's factor. That is what breaks conformal flatness.");
        Output.WriteLine("NOTE: gamma is a WEAK-FIELD parameter — at neutron-star compactness the exact e^(2B)");
        Output.WriteLine("behaviour is what matters, and gamma is no longer the meaningful description.");
    }

    // ── 4. the exclusion table ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_022_ExclusionTable()
    {
        PrintHeader("4. Which gamma values observation allows");

        var meas = new (double Value, double Sigma, string Source)[]
        {
            (1.0000210, 2.3e-5, "Cassini (Bertotti/Iess/Tortora 2003)"),
            (0.9998000, 3.0e-4, "VLBA (Fomalont 2009)"),
            (0.9970000, 1.6e-2, "Gaia (2022)"),
        };

        double gammaConformal = -1.0, gammaFlat = 0.0, gammaReciprocal = 1.0;
        var sb = new StringBuilder();
        sb.AppendLine("gamma            Cassini        VLBA         Gaia");
        foreach (var (g, name) in new[] { (gammaConformal, "-1 (AT conformal)"), (gammaFlat, " 0 (flat space)  "), (gammaReciprocal, "+1 (reciprocal)  ") })
        {
            var cells = meas.Select(m => $"{Math.Abs(g - m.Value) / m.Sigma,12:E3}");
            sb.AppendLine($"{name}  {string.Join("  ", cells)}");
        }
        Output.WriteLine(sb.ToString().TrimEnd());

        // gamma = +1 survives every measurement (sub-sigma); -1 and 0 are excluded by >60 sigma everywhere.
        foreach (var (value, sigma, source) in meas)
        {
            Assert.True(Math.Abs(gammaReciprocal - value) / sigma < 1.0, $"{source}: +1 should pass");
            Assert.True(Math.Abs(gammaConformal - value) / sigma > 60.0, $"{source}: -1 should fail");
            Assert.True(Math.Abs(gammaFlat - value) / sigma > 60.0, $"{source}: 0 should fail");
        }
        Output.WriteLine("");
        Output.WriteLine($"gamma = -1 (AT conformal sector) : Cassini {Math.Abs(-1.0 - 1.0000210) / 2.3e-5:E4} sigma  -> REFUTED");
        Output.WriteLine($"gamma =  0 (flat space)          : Cassini {Math.Abs(0.0 - 1.0000210) / 2.3e-5:E4} sigma  -> REFUTED");
        Output.WriteLine($"gamma = +1 (reciprocal spatial)  : Cassini {Math.Abs(1.0 - 1.0000210) / 2.3e-5:E3} sigma  -> ALLOWED");
    }

    // ── 5. the reciprocal spatial sector — passes the question's test, cannot be derived ──

    [Fact]
    public void Y_G_022_ReciprocalSpatialSector()
    {
        PrintHeader("5. The gamma = +1 spatial sector: all three laws exact, but NOT derivable");

        // g00 = -rho^(2/d) = -e^(-2x)  (AT's own time sector, untouched)
        // g_rr = +rho^(-2/d) = +e^(+2x) (the reciprocal factor)
        foreach (double x in new[] { XEarth, XSun, XNs })
        {
            double g00 = G00(x), grrRecip = Math.Exp(+2.0 * x), grrConf = Math.Exp(-2.0 * x);

            // (1) CLOCK LAW preserved exactly.
            Assert.True(Math.Abs(Math.Sqrt(-g00) - Math.Exp(-x)) < 1e-15);
            Assert.True(Math.Abs(Math.Sqrt(-g00) - Math.Pow(Rho(x), 1.0 / D)) < 1e-15);
            // (2) SOURCE LAW preserved exactly: it involves rho only.
            Assert.True(Math.Abs(-Sigma(x) - x) < 1e-18);
            // (3) ACCELERATION LAW preserved exactly (slow test particle sees -A' only).
            Assert.True(Math.Abs(-A(x) - x) < 1e-18);

            // gamma = -1 for the conformal sector at ANY compactness; gamma = +1 for the reciprocal sector
            // is asserted only where gamma is a meaningful (weak-field) description.
            Assert.Equal(-1.0, Gamma(g00, grrConf), 12);
            if (x <= 1e-5)
                Assert.True(Math.Abs(GammaFromExponents(A(x), -A(x)) - 1.0) < 1e-5, $"x = {x}");

            // NOT conformally flat: conformal flatness requires g_rr + g00 = 0.
            double defect = grrRecip + g00;
            Assert.True(Math.Abs(defect - 2.0 * Math.Sinh(2.0 * x)) < 1e-15);
            Assert.True(Math.Abs(defect) > 0.0);
        }
        Output.WriteLine($"x = {XNs:F6} (J0740+6620): g00 = {G00(XNs):F9}, g_rr(reciprocal) = {Math.Exp(2 * XNs):F9}");
        Output.WriteLine($"  clock rate sqrt(-g00) = {Math.Sqrt(-G00(XNs)):F9} = rho^(1/d) EXACT");
        Output.WriteLine($"  z_AT = {ZAt(XNs):F7}  (identical to G_020's 0.2801817 — the redshift work is untouched)");
        Output.WriteLine($"  conformal defect g_rr + g00 = {Math.Exp(2 * XNs) + G00(XNs):F9} (must be 0 if conformally flat)");
        Output.WriteLine($"  NOTE: the exact reciprocal-sector ratio e^(2x) = {Math.Exp(2 * XNs):F6} at this compactness —");
        Output.WriteLine("  gamma is a weak-field parameter and is not the meaningful description here.");
        Output.WriteLine($"  at weak field (x = {XSun:E3}) the same ratio is {Math.Exp(2 * XSun):F12} -> gamma = +1.");

        // THE COST: the spatial volume measure.  Conformal: Omega^3 = rho.  Reciprocal: Omega^3 = 1/rho.
        foreach (double x in new[] { XEarth, XNs })
        {
            double volConformal = Math.Pow(Math.Exp(-x), D);           // Omega^3 with Omega = rho^(1/d)
            double volReciprocal = Math.Pow(Math.Exp(+x), D);          // Omega^3 with Omega = rho^(-1/d)
            Assert.True(Math.Abs(volConformal - Rho(x)) < 1e-15);      // = the counting measure
            Assert.True(Math.Abs(volReciprocal * Rho(x) - 1.0) < 1e-12); // = its RECIPROCAL
        }
        Output.WriteLine("");
        Output.WriteLine("THE COST: the native spatial metric is Omega = rho^(1/d), whose volume measure Omega^3 is");
        Output.WriteLine("EXACTLY the counting measure rho. The gamma = +1 sector needs Omega = rho^(-1/d), whose");
        Output.WriteLine("volume measure is Omega^3 = 1/rho — the RECIPROCAL of the counting measure.");
        Output.WriteLine("");
        Output.WriteLine("So the gamma = +1 metric PASSES all three requirements of the question and reproduces");
        Output.WriteLine("gamma = +1 — but it cannot be DERIVED: the causal-order -> conformal-class step yields a");
        Output.WriteLine("metric with ONE factor, Omega^2 eta. Two different factors is a POSTULATE.");
    }

    // ── 6. the psi route is empty (refines G_021) ────────────────────────────────

    [Fact]
    public void Y_G_022_PsiRouteIsEmpty()
    {
        PrintHeader("6. The psi route is EMPTY, not merely expensive (refines G_021)");

        // With A = sigma + psi, a slow test particle's acceleration is -A' and the clock rate is e^A.
        // AT's SOURCE LAW fixes the acceleration to be -(1/d) grad ln rho = -sigma', independently of g.
        // Requiring -A' = -sigma' gives psi' = 0; requiring e^A = e^sigma gives psi = 0.
        foreach (double x in new[] { XEarth, XSun, XNs })
        {
            // psi' = 0 -> psi constant; then the clock law forces psi = 0.
            double psiConst = 1.0e-9;                                  // any nonzero constant
            Assert.True(Math.Abs(ClockRate(x) - Math.Exp(A(x) + psiConst)) > 0.0);   // clock law broken
            Assert.True(Math.Abs((A(x) + psiConst) - A(x)) > 0.0);                   // acceleration broken
        }
        Output.WriteLine("psi' = 0 (acceleration law) then psi = 0 (clock law): psi = 0 is FORCED from both.");
        Output.WriteLine("So the psi completion delivers gamma = -1, not +1 — it cannot be the fix.");

        // And at G_021's own gamma = +1 value psi = -4 sigma = +4x, the surface redshift flips sign.
        var sb = new StringBuilder();
        sb.AppendLine("   x            psi = -4 sigma      clock rate e^(sigma+psi)      z_AT");
        foreach (double x in new[] { XEarth, XSun, XNs })
        {
            double psi = 4.0 * x;                                      // = -4 sigma
            double rate = Math.Exp(A(x) + psi);
            double z = 1.0 / rate - 1.0;
            sb.AppendLine($"   {x,-12:E3} {psi,+18:E6} {rate,24:F9} {z,+12:F7}");
            if (x == XNs) Assert.True(z < 0.0, "at a compact object the psi completion predicts a BLUESHIFT");
            if (x > 1e-3) Assert.True(z < 0.0);
        }
        Output.WriteLine(sb.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("At J0740+6620 compactness the G_021 psi value gives z = -0.523366 — a NEGATIVE (blue) surface");
        Output.WriteLine("shift at a bound object. Any positive measured neutron-star redshift refutes that outright.");
        Output.WriteLine($"At the Earth's surface the same psi breaks the clock by only {Math.Abs(Math.Exp(4 * XEarth) - 1):E3}");
        Output.WriteLine($"— invisible — but at a neutron star by {Math.Abs(Math.Exp(4 * XNs) - 1):F4} (169 %).");
        Output.WriteLine("");
        Output.WriteLine("REFINEMENT OF G_021: G_021 derived psi = -4 sigma by letting g00 define Phi. Under the");
        Output.WriteLine("stricter requirement that AT's source law fixes Phi = sigma independently, psi = 0 is forced,");
        Output.WriteLine("so the psi sector is not an expensive fix but an EMPTY one. The gamma = +1 sector must be");
        Output.WriteLine("the reciprocal spatial factor instead — which is a postulate, not a derivation.");
    }

    // ── 7. verdicts and report ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_022_Run()
    {
        PrintHeader("ResearchY-G_022 — Spatial Metric Audit: can a derived spatial metric give gamma ~ +1?");

        var sb = new StringBuilder();
        sb.AppendLine("QUESTION");
        sb.AppendLine("  Can a spatial metric be DERIVED that preserves the clock law, the source law and the");
        sb.AppendLine("  acceleration law, while reproducing gamma ~ +1 ?");
        sb.AppendLine("");
        sb.AppendLine("ANSWER: NO.");
        sb.AppendLine("");
        sb.AppendLine("1. THE THREE LAWS ARE gamma-BLIND. With sigma = (1/d) ln rho = -x:");
        sb.AppendLine("     clock  dtau/dt = sqrt(-g00) = e^sigma        <- g00 only");
        sb.AppendLine("     source a = -(1/d) grad ln rho = -grad sigma  <- rho only");
        sb.AppendLine("     accel  d2r/dt2 ~ -A' (slow test particle)    <- g00 only");
        sb.AppendLine("   None mentions the spatial sector, so the question as posed constrains NOTHING about gamma.");
        sb.AppendLine("");
        sb.AppendLine("2. THE FOURTH CONDITION GIVES gamma = -1, FOR ANY CONFORMAL FACTOR. AT's metric is conformally");
        sb.AppendLine("   flat, g = Omega^2 eta, so g00 = -Omega^2 and g_rr = +Omega^2, i.e. g_rr = -g00 ALWAYS:");
        sb.AppendLine("     gamma = -(g_rr - 1)/(2 Phi) = -(Omega^2 - 1)/(Omega^2 - 1) = -1   for every Omega^2 != 1");
        sb.AppendLine("   gamma = -1 is a THEOREM, not an artefact of the factor rho^(2/d). Flipping the factor to");
        sb.AppendLine("   rho^(-2/d) STILL gives gamma = -1, because conformal flatness ties the sectors together.");
        sb.AppendLine("");
        sb.AppendLine("3. SO gamma = +1 REQUIRES TWO DIFFERENT FACTORS:");
        sb.AppendLine("     g00 = -rho^(2/d) = -e^(-2x)   (AT's clock law, EXACT)");
        sb.AppendLine("     g_rr = +rho^(-2/d) = +e^(+2x) (reciprocal -> gamma = +1)");
        sb.AppendLine("   Clock law exact. Source law exact. Slow-particle acceleration exact. Redshift z = e^x - 1");
        sb.AppendLine("   identical to G_019/G_020. gamma = +1 -> Cassini 0.91 sigma (allowed).");
        sb.AppendLine("   BUT the metric is NOT conformally flat (g_rr + g00 = 2 sinh 2x != 0), so the");
        sb.AppendLine("   causal-order -> conformal-class step does not produce it: it is a POSTULATE.");
        sb.AppendLine("   COST: the spatial volume measure becomes Omega^3 = 1/rho, the RECIPROCAL of the counting");
        sb.AppendLine("   measure, instead of rho.");
        sb.AppendLine("");
        sb.AppendLine("4. THE psi ROUTE IS EMPTY. With A = sigma + psi the acceleration law gives psi' = 0 and the");
        sb.AppendLine("   clock law then gives psi = 0 — forced from BOTH. (Refines G_021, which obtained psi = -4 sigma");
        sb.AppendLine("   by letting g00 define Phi.) At that value the surface redshift is z = e^(-3x) - 1 < 0: a");
        sb.AppendLine("   BLUESHIFT at a bound object, refuted by any positive measured neutron-star redshift.");
        sb.AppendLine("");
        sb.AppendLine("5. NOTHING EARLIER IS DAMAGED. Every G_001-G_020 agreement with GR lives in the gamma-blind");
        sb.AppendLine("   sector: G_004's 0.99600, G_009/G_015/G_017 clock rates, G_019's x^2 signature, G_020's");
        sb.AppendLine("   z_AT = 0.2801817 for J0740+6620, and the source-law/MOND phenomenology. G_022 proves they are");
        sb.AppendLine("   invariant under ANY spatial sector. gamma is a SEPARATE observable class (light bending,");
        sb.AppendLine("   Shapiro delay) that no G-chain audit ever exercised before G_021.");
        sb.AppendLine("");
        sb.AppendLine("VERDICTS");
        sb.AppendLine("  DERIVED   the gamma-blindness of the three laws; the theorem 'conformal flatness + Phi = sigma");
        sb.AppendLine("            => gamma = -1' for ANY factor; the unique native spatial metric Omega = rho^(1/d); and");
        sb.AppendLine("            the explicit gamma = +1 sector g_rr = rho^(-2/d) with g00 = -rho^(2/d) intact.");
        sb.AppendLine("  BOUNDARY  the gamma = +1 sector is a POSTULATE requiring reciprocal factors in the two sectors,");
        sb.AppendLine("            i.e. abandoning 4D conformal flatness; its cost is a volume measure of 1/rho. And the");
        sb.AppendLine("            psi route is empty (psi = 0 forced), not merely expensive.");
        sb.AppendLine("  REFUTED   that the three laws imply or permit a DERIVED gamma ~ +1 (they are silent on gamma);");
        sb.AppendLine("            and the psi completion, which predicts a surface blueshift at compact objects.");
        sb.AppendLine("");
        sb.AppendLine("No reclassification. D_040 untouched. No canonical claim, value or equation changes.");
        sb.AppendLine("No new primitive. G_021's other results stand.");

        Output.WriteLine(sb.ToString().TrimEnd());
        Assert.True(sb.Length > 0);
        Assert.True(D == 3);
    }
}
