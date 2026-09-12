using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_021 — Light-Propagation Audit (group G — Gravity Source).  This audit CORRECTS G_019.
///
/// THE CORRECTION. G_019 stated that "AT supplies no spatial metric, so no light bending, Shapiro delay or
/// shadow size follows from it — the theory's most distinctive consequence is its least derivable one."
/// THAT IS WRONG, and the project's own earlier programme (G4, AT-QG Phases 21/26/186/207/212) already has
/// the right answer. AT DOES supply the full g_uv: the metric is CONFORMALLY FLAT,
///
///     g_uv = rho^(2/d) eta_uv ,     so  g00 = -rho^(2/d)  and  g_rr = +rho^(2/d) ,
///
/// and the project classifies the chain as CLOSED (Docs/Audits/MetricOriginClosure.md:
/// "Full g_uv | determined | class x factor - closed"), with
///     causal order -> conformal class   (IMPORTED: Malament 1977 / Hawking-King-McCarthy - a PROVEN theorem)
///     counting measure -> conformal factor rho^(2/d)   (NATIVE)
/// (and a native reconstruction of the same class without Malament: G4-M Phase 0).
///
/// SO LIGHT BENDING IS DERIVABLE — AND THE DERIVED ANSWER IS ZERO. With sigma = (1/d) ln rho (so Phi = sigma)
/// the weak field of the conformal metric is g00 = -(1 + 2 sigma) and g_rr = +(1 + 2 sigma); in PPN form
/// g00 = -(1 + 2 Phi), g_rr = 1 - 2 gamma Phi, this gives the EXACT result
///
///     gamma = -1 ,     (1 + gamma)/2 = 0 ,
///
/// so deflection = 0, convergence kappa = 0, shear = 0, magnification mu = 1 and Shapiro delay = 0 — while the
/// REDSHIFT survives, because it is governed by g00 alone. AT therefore predicts "redshift WITHOUT lensing".
///
/// AND THAT IS OBSERVATIONALLY EXCLUDED, at very high significance:
///   Cassini (Bertotti, Iess & Tortora 2003)   gamma = 1.0000210 +- 2.3e-5   ->  8.6957e4 sigma
///   VLBA solar deflection (Fomalont 2009)     gamma = 0.9998000 +- 3.0e-4   ->  6.6660e3 sigma
///   Gaia solar deflection (2022)              gamma = 0.9970000 +- 1.6e-2   ->  1.2481e2 sigma
/// plus the qualitative fact that gravitational lensing is observed in thousands of systems.
///
/// THE TWO SECTORS (project result, AT-QG Phase 212): the theory escapes only through the psi != 0 tensor
/// completion, g00 = -rho^(2/d) e^(2 psi), g_ii = rho^(2/d) e^(-2 psi/(d-1)) (Fierz-Pauli, QG44, whose
/// linearized limit is GR: gamma = +1). THIS AUDIT DERIVES THE REQUIREMENT EXPLICITLY:
///
///     gamma(psi) = -[ sigma - psi/(d-1) ] / ( sigma + psi ) ,   so   gamma = -1 at psi = 0
///     gamma = +1   <=>   psi = -2 sigma (d-1)/(d-2)   ->   psi = -4 sigma in d = 3   (no solution in d = 2)
///
/// AND THAT REQUIREMENT IS NOT A FREE FIX. Because a timelike geodesic's acceleration is fixed by g00 alone,
/// the potential becomes Phi = sigma + psi; with psi = -4 sigma that gives Phi = -3 sigma, which CONTRADICTS
/// AT's native source law a = -(1/d) grad ln rho = -grad sigma (the very relation behind G_004's 0.99600).
/// So restoring gamma = +1 necessarily changes the SOURCING relation, and the psi sector is classified by the
/// project itself as a MINIMAL NEW PRIMITIVE (QG24; QG43: lensing/Shapiro/gamma need at least a 1-d.o.f. psi).
///
/// WHY THE WHOLE G-CHAIN WAS BLIND TO THIS: every audit G_001-G_020 used g00 only — the source law, the clock
/// law, the calibration (0.99600), the galactic cross-check (0.99668), the second-order signature (G_019) and
/// the neutron-star redshifts (G_020). The conformal spatial part never entered. The G-chain is therefore
/// correct within its scope and silent on optics. One further consequence: the psi-completed clock law is
/// dtau/dt = rho^(1/d) e^(psi), and e^(psi) = 1 + psi + ... is FIRST order in psi, unlike G_019's x^2
/// signature — so any nonzero psi enters the clock at LOWER order and could mask that signature.
///
/// VERDICTS
///   DERIVED   the conformal metric and its spatial part; gamma = -1 exactly and (1+gamma)/2 = 0; the
///             resulting zero deflection/convergence/shear/Shapiro with a surviving redshift; and the
///             gamma(psi) formula with gamma = +1 <=> psi = -4 sigma in d = 3.
///   BOUNDARY  the conformal CLASS (imported Malament theorem) and the psi sector (a NEW PRIMITIVE, QG24);
///             and the tension that psi = -4 sigma also shifts Phi to -3 sigma.
///   REFUTED   the psi = 0 conformal sector's optics: 8.6957e4 sigma (Cassini), 6.6660e3 sigma (VLBA),
///             1.2481e2 sigma (Gaia), and the existence of observed lensing.
///
/// Deterministic: exact algebra on imported published values.  No reclassification of G_001-G_020's own
/// results (they are g00-only and stand); D_040 untouched; no canonical claim, value or equation changes;
/// no new primitive.
///
/// ⚠ CORRECTED BY ResearchY-G_024 (Optics Reconciliation Audit). This suite's statements that psi is a
/// "MINIMAL NEW PRIMITIVE" and that gamma = +1 is out of derivational reach are SUPERSEDED and WITHDRAWN.
/// The AT-QG optics resolution QG212 (Docs/Research/ATQG_ConformalOpticsResolution.md — Status COMPLETE,
/// OPTICS RESOLVED; tests ATQG2120/2121/2122) classes the psi = 0 conformal slice as a RESTRICTED SECTOR:
/// the PHYSICAL sector is psi != 0, which gives gamma = +1 with lensing, Shapiro and frame dragging at full GR
/// — with NO new primitives, since psi is the TRACELESS FACE of the one Difference read against eta
/// (minimal primitive set {Difference, eta}; QG285/QG286/QG292). What STANDS here is the quantitative part:
/// the psi = 0 slice is Cassini-excluded at 8.6957e4 sigma, and the invariant split is
/// psi = 0 <=> A = B <=> gamma = -1, gamma = +1 <=> A + B = 0.
/// </summary>
public class Y_G_021_Tests : ResearchTestBase
{
    public Y_G_021_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;
    private const double C2 = C * C;

    /// <summary>The conformal metric: g00 = -rho^(2/d) = -e^(2 sigma), g_rr = +rho^(2/d) = +e^(2 sigma).</summary>
    private static double G00(double sigma) => -Math.Exp(2.0 * sigma);
    private static double Grr(double sigma) => Math.Exp(2.0 * sigma);

    /// <summary>
    /// PPN gamma, evaluated exactly (not by finite difference) along the ray sigma = a t, psi = b t.
    /// Phi is defined by g00 = -(1 + 2 Phi) with no expansion:  Phi = (1 - g00)/2 = (e^(2 sigma + 2 psi) - 1)/2.
    /// Then gamma = -(g_rr - 1)/(2 Phi).
    /// </summary>
    private static double GammaAlong(double a, double b, double t)
    {
        double sigma = a * t, psi = b * t;
        double grr = Math.Exp(2.0 * sigma - 2.0 * psi / (D - 1));
        double phi = 0.5 * (Math.Exp(2.0 * sigma + 2.0 * psi) - 1.0);
        return -(grr - 1.0) / (2.0 * phi);
    }

    /// <summary>The first-order PPN formula gamma = -(a - b/(d-1))/(a + b), for general d.</summary>
    private static double GammaFirstOrder(double a, double b, int d = D)
        => -(a - b / (d - 1)) / (a + b);

    /// <summary>expm1(x) = e^x - 1, evaluated by series where the direct subtraction would cancel.</summary>
    private static double Expm1(double x)
        => Math.Abs(x) < 1e-6 ? x + x * x / 2.0 + x * x * x / 6.0 : Math.Exp(x) - 1.0;

    // ── 1. the conformal closure: AT DOES have a spatial metric ──────────────────

    [Fact]
    public void Y_G_021_ConformalClosure()
    {
        // The metric is conformally flat: every spatial component equals rho^(2/d), so g_rr = -g00.
        foreach (double sigma in new[] { -1e-9, -1e-6, -1e-3, -0.2 })
        {
            Assert.True(Math.Abs(Grr(sigma) + G00(sigma)) < 1e-15 * Math.Abs(G00(sigma)),
                $"sigma = {sigma}: g_rr != -g00");
            Assert.True(Grr(sigma) > 0.0 && G00(sigma) < 0.0);
        }

        // The closure chain the project states (MetricOriginClosure.md): class x factor = full g_uv, CLOSED.
        // The class is IMPORTED (Malament 1977; Hawking-King-McCarthy 1976 - a proven theorem); the factor
        // rho^(2/d) is NATIVE (the counting measure, uniquely selected by counting-measure preservation).
        Assert.True(Math.Abs(Math.Pow(1.0, 2.0 / D) - 1.0) < 1e-15);          // rho = 1 -> flat
        Assert.True(Math.Abs(Math.Pow(2.0, 2.0 / D) - Math.Pow(2.0, 2.0 / 3.0)) < 1e-15);

        // So G_019's claim that AT "supplies no spatial metric" is FALSE: the spatial part is g_ij = rho^(2/d) d_ij.
        // This test exists to make that correction explicit and executable.
        double sigma0 = -6.96133e-10;                                    // the Earth's surface (G_004)
        Assert.True(Math.Abs(Grr(sigma0) - (1.0 + 2.0 * sigma0)) < 1e-17);
        Assert.True(Math.Abs(G00(sigma0) + (1.0 + 2.0 * sigma0)) < 1e-17);
    }

    // ── 2. gamma = -1 exactly ────────────────────────────────────────────────────

    [Fact]
    public void Y_G_021_GammaMinusOne()
    {
        // With psi = 0 the PPN read-off is EXACTLY -1 at any amplitude, not merely to first order:
        //   g00 = -e^(2 sigma)  =>  Phi = (e^(2 sigma) - 1)/2  and  g_rr = e^(2 sigma),
        //   so gamma = -(g_rr - 1)/(2 Phi) = -(e^(2 sigma) - 1)/(e^(2 sigma) - 1) = -1.
        foreach (double t in new[] { 1e-12, 1e-6, 1e-3, 0.2 })
            Assert.Equal(-1.0, GammaAlong(1.0, 0.0, t), 12);

        // Same statement in the first-order (standard PPN) form: Phi = sigma and g_rr = 1 + 2 sigma,
        // i.e. -2 gamma sigma = +2 sigma -> gamma = -1.
        Assert.Equal(-1.0, GammaFirstOrder(1.0, 0.0), 15);

        // The discriminating fact: gamma = -1 is a CONSEQUENCE of the conformal choice g_rr = -g00.
        // The opposite spatial sign, g_rr = e^(-2 sigma), would give gamma = +1.
        double alt = -(Math.Exp(-2.0 * 1e-9) - 1.0) / (Math.Exp(2.0 * 1e-9) - 1.0);
        Assert.True(Math.Abs(alt - 1.0) < 1e-6, $"alt gamma = {alt}");
    }

    // ── 3. no optics: zero deflection, zero Shapiro, redshift survives ───────────

    [Fact]
    public void Y_G_021_NoOptics()
    {
        double gamma = -1.0;
        double factor = (1.0 + gamma) / 2.0;
        Assert.True(factor == 0.0);

        // Every lensing observable is proportional to (1+gamma)/2, so all of them vanish.
        double deflection = factor * 4.0;                       // in units of GM/(bc^2)
        double convergence = factor;
        double shear = factor;
        double magnification = factor > 0.0 ? factor + 1.0 : 1.0;
        double shapiro = factor * 2.0;                          // in units of GM/c^3
        Assert.True(deflection == 0.0 && convergence == 0.0 && shear == 0.0 && shapiro == 0.0);
        Assert.Equal(1.0, magnification);

        // The REDSHIFT survives: it is governed by g00 alone, so it is untouched by the spatial sector.
        // AT: z = rho^(1/d) - 1 at the same location; GR: z ~ Phi/c^2. Both are nonzero.
        double sigma = -6.96133e-10;
        double zAt = Expm1(-sigma);                        // deeper -> bluer incoming light
        Assert.True(Math.Abs(zAt + sigma) < 1e-18);
        Assert.True(zAt > 0.0);
        // And the redshift agrees with GR to first order (G_004's 0.99600 is a g00-only statement, untouched).
        Assert.True(Math.Abs(G00(sigma) - (-(1.0 + 2.0 * sigma))) < 1e-17);
    }

    // ── 4. the exclusion: Cassini, VLBA, Gaia ────────────────────────────────────

    [Fact]
    public void Y_G_021_Exclusion()
    {
        // (value, sigma, source) — published, imported.
        var measurements = new (double Value, double Sigma, string Source)[]
        {
            (1.0000210, 2.3e-5, "Cassini (Bertotti/Iess/Tortora 2003)  Shapiro delay"),
            (0.9998000, 3.0e-4, "VLBA (Fomalont 2009)               solar deflection"),
            (0.9970000, 1.6e-2, "Gaia (2022)                        solar deflection"),
        };

        double gammaAt = -1.0;
        var line = new StringBuilder();
        PrintHeader("4. The conformal sector's optics are observationally excluded");
        line.AppendLine("observable                                   gamma_obs     sigma_g      |gamma_AT-g_obs|/sg");
        foreach (var (value, sigmaObs, source) in measurements)
        {
            double sep = Math.Abs(gammaAt - value) / sigmaObs;
            line.AppendLine($"{source,-44} {value,10:F7}  {sigmaObs,9:E2}  {sep,14:E4}");
            Assert.True(sep > 100.0, $"{source}: only {sep:E3} sigma");
        }
        Output.WriteLine(line.ToString().TrimEnd());
        Output.WriteLine("");
        Output.WriteLine("AT (conformal, psi = 0): gamma = -1.0000000");
        Output.WriteLine("Cassini  -> 8.6957e4 sigma      VLBA -> 6.6660e3 sigma      Gaia -> 1.2481e2 sigma");
        Output.WriteLine("Plus the qualitative fact: gravitational lensing IS observed in thousands of systems.");
        Output.WriteLine("=> AT's psi = 0 conformal sector is REFUTED as a description of light propagation.");

        // The Cassini number, computed.
        Assert.True(Math.Abs(Math.Abs(-1.0 - 1.0000210) / 2.3e-5 - 8.69574347826087e4)
            < 1e-9 * 8.69574347826087e4);
        Assert.True(Math.Abs(Math.Abs(-1.0 - 0.9998) / 3.0e-4 - 6.6660e3) < 1e-9);
        Assert.True(Math.Abs(Math.Abs(-1.0 - 0.997) / 1.6e-2 - 124.8125) < 1e-9);
    }

    // ── 5. the tensor completion: gamma = +1 requires psi = -4 sigma ─────────────

    [Fact]
    public void Y_G_021_TensorCompletion()
    {
        PrintHeader("5. The psi completion: what psi is needed for gamma = +1");

        // gamma(psi) = -[ sigma - psi/(d-1) ] / ( sigma + psi ) to first order; gamma = -1 at psi = 0.
        foreach (double b in new[] { 0.0, -0.5, 2.0 })
        {
            double gamma = GammaAlong(1.0, b, -1e-6);
            Assert.True(Math.Abs(gamma - GammaFirstOrder(1.0, b)) < 1e-5, $"b = {b}: {gamma}");
        }
        Assert.Equal(-1.0, GammaFirstOrder(1.0, 0.0), 15);
        Assert.Equal(-1.0, GammaAlong(1.0, 0.0, 1e-6), 12);

        // gamma = +1  <=>  psi = -2 sigma (d-1)/(d-2)  ->  psi = -4 sigma in d = 3.
        Assert.Equal(+1.0, GammaFirstOrder(1.0, -4.0), 15);
        Assert.True(Math.Abs(-2.0 * (D - 1) / (D - 2) + 4.0) < 1e-12);
        Assert.Equal(+1.0, GammaFirstOrder(1.0, -3.0, 4), 15);        // d = 4 -> psi = -3 sigma
        // d = 2 is degenerate: the requirement diverges (consistent with G_uv == 0 in d = 2, QG180).
        Assert.True(double.IsPositiveInfinity(2.0 * (2 - 1) / (2 - 2)));

        // Along the required direction the exact gamma is e^(6t) = 1 + 6t + ..., i.e. +1 to first order with a
        // residual that is second order — exactly the order at which PPN gamma is defined.
        double t = 1e-6;
        Assert.True(Math.Abs(GammaAlong(1.0, -4.0, t) - Math.Exp(6.0 * t)) < 1e-10);
        Assert.True(Math.Abs(GammaAlong(1.0, -4.0, t) - 1.0 - 6.0 * t) < 1e-10);
        Output.WriteLine($"gamma along psi = -4 sigma: exact = e^(6 sigma), = {GammaAlong(1.0, -4.0, t):F10} at sigma = {t:E0}");
        Output.WriteLine("so gamma = +1 to first order (the order at which PPN gamma is defined).");

        // AND THE REQUIREMENT IS NOT FREE. A timelike geodesic's acceleration is fixed by g00 alone, so the
        // potential is Phi = sigma + psi = -3 sigma, not sigma.
        double phiRequired = 0.5 * (Math.Exp(2.0 * t + 2.0 * (-4.0 * t)) - 1.0);
        Assert.True(Math.Abs(phiRequired / t + 3.0) < 1e-4);
        Output.WriteLine("");
        Output.WriteLine("CONFLICT: restoring gamma = +1 via psi = -4 sigma forces Phi = sigma + psi = -3 sigma,");
        Output.WriteLine("which contradicts AT's native source law a = -(1/d) grad ln rho = -grad sigma");
        Output.WriteLine("(the relation behind G_004's 0.99600 agreement). The psi sector is therefore NOT a");
        Output.WriteLine("free optics patch: it necessarily changes the SOURCING relation, and the project");
        Output.WriteLine("classifies psi itself as a MINIMAL NEW PRIMITIVE (QG24; QG43: a 1-d.o.f. scalar suffices).");
    }

    // ── 6. why the G-chain was blind, and the verdicts ───────────────────────────

    [Fact]
    public void Y_G_021_ChainBlindnessAndVerdicts()
    {
        PrintHeader("6. Why G_001-G_020 never saw this, and the verdicts");

        // Every G-chain audit used g00 only. Re-verify one calibration number that IS g00-only, so the point is
        // not that the chain is wrong but that it is silent on optics.
        double aAt = G_SI * 5.9722e24 / Math.Pow(6.371e6, 2);
        double aGr = 6.67430e-11 * 5.9722e24 / Math.Pow(6.371e6, 2);
        Assert.True(Math.Abs(aAt / aGr - G_SI / 6.67430e-11) < 1e-14);
        Assert.True(Math.Abs(aAt / aGr - 0.996) < 1e-5);
        Output.WriteLine("g00-only results that stand unchanged: G_004 calibration 0.99600; G_009 clock law;");
        Output.WriteLine("G_015/G_016b dTau = 86 277.089 s/day at fixed energy; G_019 x^2 signature; G_020 redshifts.");

        // The psi-completed clock law is dtau/dt = rho^(1/d) e^(psi). e^(psi) is FIRST order in psi -
        // LOWER order than G_019's x^2 - so a nonzero psi could swamp the second-order signature.
        double psiProbe = 1e-3;
        double clockPsiBlind = Expm1(psiProbe);                     // first order
        double clockX2Signature = Math.Pow(1e-3, 2);                     // second order (G_019)
        Assert.True(clockPsiBlind > clockX2Signature);
        Assert.True(Math.Abs(clockPsiBlind / clockX2Signature - 1000.5001667083847) < 1e-6);
        Output.WriteLine($"psi enters the clock at FIRST order: at psi = {psiProbe:E0} the shift is");
        Output.WriteLine($"  {clockPsiBlind:E4}, i.e. {clockPsiBlind / clockX2Signature:F1}x the x^2 signature of G_019.");

        Output.WriteLine("");
        PrintHeader("VERDICTS");
        Output.WriteLine("DERIVED   AT's metric has a spatial part g_ij = rho^(2/d) d_ij (conformal closure, class");
        Output.WriteLine("          imported Malament / factor native); gamma = -1 EXACTLY, so (1+gamma)/2 = 0 and");
        Output.WriteLine("          deflection, kappa, shear, mu-1 and Shapiro delay all vanish while the redshift");
        Output.WriteLine("          survives; and gamma(psi) = +1 <=> psi = -4 sigma in d = 3 (no d = 2 solution).");
        Output.WriteLine("BOUNDARY  the conformal CLASS (imported theorem) and the psi sector (MINIMAL NEW PRIMITIVE,");
        Output.WriteLine("          QG24); and the conflict that psi = -4 sigma also moves Phi from sigma to -3 sigma.");
        Output.WriteLine("REFUTED   the psi = 0 conformal optics: 8.6957e4 sigma (Cassini), 6.6660e3 sigma (VLBA),");
        Output.WriteLine("          1.2481e2 sigma (Gaia), plus thousands of observed lensing systems.");
        Output.WriteLine("");
        Output.WriteLine("CORRECTION TO G_019: 'AT supplies no spatial metric' is FALSE. The spatial metric exists,");
        Output.WriteLine("it is derivable, and its consequence (gamma = -1) is a falsification - not a gap.");
        Output.WriteLine("G_019's other results are g00-only and stand.");

        // No reclassification, no new primitive, no canonical claim touched.
        Assert.True(D == 3);
    }

    // ── 7. full report ───────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_021_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_021 — Light-Propagation Audit  (corrects G_019)");

        sb.AppendLine("QUESTION");
        sb.AppendLine("  G_019 claimed AT 'supplies no spatial metric', so that no light bending, Shapiro delay or");
        sb.AppendLine("  shadow size follows from it. Is that true?");
        sb.AppendLine("");
        sb.AppendLine("ANSWER: NO. AT supplies the full metric, and the derived optics are gamma = -1 (zero");
        sb.AppendLine("deflection, zero Shapiro delay, redshift only) - which is EXCLUDED by Cassini at 8.7e4 sigma.");
        sb.AppendLine("");
        sb.AppendLine("1. THE METRIC (project result, MetricOriginClosure.md): g_uv = rho^(2/d) eta_uv, CLOSED.");
        sb.AppendLine("     g00 = -rho^(2/d)      g_rr = +rho^(2/d)      (conformally flat)");
        sb.AppendLine("     causal order -> conformal class (IMPORTED: Malament 1977, proven)");
        sb.AppendLine("     counting measure -> conformal factor rho^(2/d) (NATIVE)");
        sb.AppendLine("");
        sb.AppendLine("2. THE OPTICS. sigma = (1/d) ln rho, Phi = sigma:");
        sb.AppendLine("     g00 ~ -(1 + 2 Phi)     g_rr ~ 1 - 2 gamma Phi     ->     gamma = -1 exactly");
        sb.AppendLine("     every lensing observable ~ (1 + gamma)/2 = 0:  deflection = kappa = shear = Shapiro = 0, mu = 1");
        sb.AppendLine("     the REDSHIFT survives (it is g00 only). AT predicts redshift WITHOUT lensing.");
        sb.AppendLine("");
        sb.AppendLine("3. THE EXCLUSION");
        sb.AppendLine("     Cassini (Bertotti/Iess/Tortora 2003)  gamma = 1.0000210 +- 2.3e-5  ->  8.6957e4 sigma");
        sb.AppendLine("     VLBA (Fomalont 2009)                  gamma = 0.9998000 +- 3.0e-4  ->  6.6660e3 sigma");
        sb.AppendLine("     Gaia (2022)                           gamma = 0.9970000 +- 1.6e-2  ->  1.2481e2 sigma");
        sb.AppendLine("");
        sb.AppendLine("4. THE ESCAPE (QG207/QG212) AND ITS PRICE");
        sb.AppendLine("     g00 = -rho^(2/d) e^(2 psi)      g_ii = rho^(2/d) e^(-2 psi/(d-1))   (Fierz-Pauli, QG44)");
        sb.AppendLine("     gamma(psi) = -[sigma - psi/(d-1)]/(sigma + psi)      gamma = -1 at psi = 0");
        sb.AppendLine("     gamma = +1  <=>  psi = -2 sigma (d-1)/(d-2)  =  -4 sigma in d = 3   (no d = 2 solution)");
        sb.AppendLine("     BUT Phi = sigma + psi = -3 sigma then contradicts AT's native a = -grad sigma.");
        sb.AppendLine("     So psi is not a free patch; it changes the SOURCING relation. psi is classified by the");
        sb.AppendLine("     project as a MINIMAL NEW PRIMITIVE (QG24); QG43: a 1-d.o.f. scalar suffices for gamma.");
        sb.AppendLine("");
        sb.AppendLine("5. WHY THE G-CHAIN WAS BLIND. Every audit G_001-G_020 used g00 only (source law, clock law,");
        sb.AppendLine("   calibration 0.99600, galactic 0.99668, x^2 signature, neutron-star redshifts). The conformal");
        sb.AppendLine("   spatial part never entered. Those results stand; the chain is silent on optics.");
        sb.AppendLine("   Note: the psi-completed clock is dtau/dt = rho^(1/d) e^(psi), and e^(psi) enters at FIRST");
        sb.AppendLine("   order - lower than G_019's x^2 - so nonzero psi could mask the second-order signature.");
        sb.AppendLine("");
        sb.AppendLine("VERDICTS:  DERIVED (conformal spatial metric, gamma = -1, gamma = +1 <=> psi = -4 sigma)");
        sb.AppendLine("           BOUNDARY (conformal class imported; psi a new primitive; the Phi conflict)");
        sb.AppendLine("           REFUTED (psi = 0 optics: 8.7e4 sigma Cassini; lensing is observed)");
        sb.AppendLine("");
        sb.AppendLine("CORRECTION: G_019's 'no spatial metric' sentence is withdrawn.");

        Output.WriteLine(sb.ToString().TrimEnd());
        Assert.True(sb.Length > 0);
    }
}
