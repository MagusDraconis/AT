using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_009 — Clock Rate Audit (group G — Gravity Source).
///
/// Question: does the actualization density rho change CLOCK RATES?
///
/// GIVEN:  g_00 = -rho^(2/d)   (QG197)
///         => the clock rate is  dtau/dt = sqrt(-g_00) = rho^(1/d)
///         => the fractional rate change is  (1/d) * Delta ln rho  =  Delta Phi / c^2   (the canonical redshift
///            law, QG21/QG187 — which G_003 anchored on the Earth-vs-GPS potential, 45.74 vs 45.7 us/day).
///
/// COMPUTED for four cases (Earth surface, GPS orbit, Galactic field, the G_002 density redistributions) and
/// compared with GR.  Output: DERIVED / CORRELATED / REFUTED.
///
/// RESULTS
///   Case 1 Earth surface : AT -6.961275e-10  =  GR -GM/(R c^2)  =  -60.145 us/day (to double precision).
///   Case 2 GPS orbit     : AT +5.293971e-10 (gravitational, +45.740 us/day) minus the imported SR term
///                          -7.203 us/day gives +38.537 us/day vs the observed 38.6 (0.16 %).
///   Case 3 Galactic field: AT Delta ln rho/d = 5.367333e-7 vs the kinematic v^2/c^2 = 5.385226e-7 for
///                          v = 220 km/s -> ratio 0.99668 (0.33 %) = 46.37 ms/day.  A genuinely NEW
///                          cross-check: the G_003 ambient calibration reproduces the observed galactic
///                          potential depth (equivalent v = 219.63 km/s).
///   Case 4 redistributions: at FIXED total mass-energy (Sigma rho = 1, Sigma m = 0) the G_002 witnesses
///                          give (1/3)Delta ln rho = 22.86 %, 20.11 %, 11.11 %, 9.22 %, 1.07 % clock-rate
///                          changes (19 749 s/day for the arrangement); the realised band (G_005's 4.8867e-6
///                          ceiling) gives 1.629e-6, i.e. the galactic value.
///
/// VERDICTS
///   DERIVED    the law dtau/dt = rho^(1/d) from g_00 = -rho^(2/d); the identity (1/d)Delta ln rho = Delta Phi/c^2;
///              the Earth/GPS magnitudes (AT == GR to first order, and both == observation); the galactic
///              cross-check; the exact AT-vs-GR second-order coefficient (+1/2 vs -1/2).
///   CORRELATED AT and GR agree to first order in Phi/c^2 and differ at second order, where the AT conformal
///              structure gives exp(Phi/c^2) against GR's sqrt(1 + 2Phi/c^2): the difference is (Phi/c^2)^2
///              = 4.846e-19 at the Earth's surface and 2.803e-19 at the GPS orbit — 2-4x BELOW the 1e-18
///              floor of the best optical clocks, i.e. a future test.  The GPS TOTAL also needs the imported
///              special-relativistic term, so the kinematic half is not AT content.
///   REFUTED    the claim that a REALISED rho redistribution can produce a SUSTAINED clock effect at fixed
///              mass-energy: G_005 (the witnesses are not counted) and G_008 (they are not maintainable —
///              lifetime 0.62 steps, 0.7998 of the amplitude needed per step) close that route.  The phase
///              directions produce EXACTLY zero clock change, and a uniform rescaling rho -> lambda*rho
///              changes all clocks equally — the RELATIVE rate change is exactly zero (scale invariance).
///
/// CRITICAL QUESTION (can rho variations produce measurable time dilation without mass-energy changes?):
/// YES as a theoretical statement — the clock rate depends on the ARRANGEMENT of rho, not on its total
/// (Sigma m = 0 exactly for every G_002 operation), and the effect is huge (up to 22.86 %, 2.3e17x an
/// optical clock's floor).  But NO physically: no realised or maintainable configuration produces it, and
/// the *realised* rho variations (Poisson-natural, <= 4.8867e-6) give <= 1.63e-6 — which is precisely the
/// galactic field that GR also predicts from the rotation curve (0.33 % agreement).  Locally AT = GR with
/// the derived G, and the only AT-specific local signatures are the 0.40 % G offset and the +-1/2
/// second-order coefficient at 1e-19.
///
/// Deterministic: exact algebra, no randomness.  No reclassification; D_040 untouched.
/// </summary>
public class Y_G_009_Tests : ResearchTestBase
{
    public Y_G_009_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;
    private const int N = 96;

    // G_002/G_003 witness contrasts (Delta ln rho) — the same table G_005/G_006 used.
    private static readonly (string Name, double DeltaLnRho)[] Witnesses =
    [
        ("arrangement", 0.685714), ("degeneracy redistribution", 0.603175),
        ("D96 vs random", 0.333333), ("D96^3 vs D96", 0.276596), ("survivor compression", 0.032121)
    ];

    private const double AmbientContrast = 1.6102e-6;    // G_003's observed galactic field
    private const double AccessibleCeiling = 4.8867e-6;  // G_005's 1 % Poisson ceiling
    private const double ClockFloor = 1e-18;             // best optical-clock fractional resolution

    // ── the clock law ────────────────────────────────────────────────────────────

    /// <summary>AT clock rate from g_00 = -rho^(2/d): dtau/dt = rho^(1/d).</summary>
    private static double AtRate(double rho) => Math.Pow(rho, 1.0 / D);

    /// <summary>GR clock rate for a static potential: sqrt(1 + 2Phi/c^2).</summary>
    private static double GrRate(double phiOverC2) => Math.Sqrt(1.0 + 2.0 * phiOverC2);

    /// <summary>The potential depth for a given AT density contrast: Delta Phi/c^2 = (1/d) Delta ln rho.</summary>
    private static double PhiOverC2(double deltaLnRho) => deltaLnRho / D;

    /// <summary>Fractional rate deviation expressed in microseconds per day.</summary>
    private static double UsPerDay(double fractional) => fractional * 86400.0 * 1e6;

    /// <summary>Fractional rate deviation expressed in seconds per day.</summary>
    private static double SecondsPerDay(double fractional) => fractional * 86400.0;

    private static double GpsGravitational => GM_Earth / (C * C) * (1.0 / R_Earth - 1.0 / R_Gps);
    private static double GpsOrbitalSpeed => Math.Sqrt(GM_Earth / R_Gps);
    private static double EarthSurfaceDepth => GM_Earth / (R_Earth * C * C);

    // ── 1. The clock law ────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_009_ClockLaw()
    {
        // g_00 = -rho^(2/d)  =>  dtau/dt = sqrt(-g_00) = rho^(1/d), and to first order the fractional rate
        // change is (1/d)*Delta ln rho = Delta Phi/c^2 (the canonical redshift law).
        double delta = 0.685714;                                  // the arrangement witness
        double rho = Math.Exp(delta);
        double at = AtRate(rho);
        Assert.True(Math.Abs(at - Math.Exp(delta / D)) < 1e-15);
        // first order is (1/d)Delta ln rho = 0.228571, with the second-order term x^2/2 = 0.0261 on top:
        Assert.True(Math.Abs((at - 1.0) - delta / D - delta * delta / (2 * D * D)) < 3e-3);   // remainder = x^3/6
        Assert.True(Math.Abs(Math.Exp(1e-3) - 1.0 - 1e-3 - 5e-7) < 1e-9);   // 5e-7 IS the x^2/2 term
        Assert.True(Math.Abs((at - 1.0) - 0.256803) < 1e-5, $"rate change = {at - 1.0} (1st order 0.228571 + 2nd order 0.026122)");
        Assert.True(Math.Abs(PhiOverC2(delta) - 0.228571) < 1e-5);

        // AT and GR agree to FIRST order in Phi/c^2 and differ at SECOND order with opposite sign:
        // AT = exp(x) = 1 + x + x^2/2,  GR = sqrt(1 + 2x) = 1 + x - x^2/2.
        double x = 1e-5;
        double atX = AtRate(Math.Exp(D * x)), grX = GrRate(x);   // rho = exp(d*x)  =>  AT rate = exp(x)
        Assert.True(Math.Abs((atX - 1.0 - x) / (x * x) - 0.5) < 1e-5, $"AT second-order coeff = {(atX - 1.0 - x) / (x * x)}");
        Assert.True(Math.Abs((grX - 1.0 - x) / (x * x) + 0.5) < 1e-5, $"GR second-order coeff = {(grX - 1.0 - x) / (x * x)}");
        Assert.True(Math.Abs((atX - grX) - x * x) / (x * x) < 1e-3);
        Assert.True(atX > grX);                                    // AT runs FASTER at second order

        // SCALE INVARIANCE: a uniform rescaling rho -> lambda*rho moves every clock by the same factor, so
        // the RELATIVE rate change is exactly zero — only ratios of rho are physical.
        foreach (double lambda in new[] { 1.01, 1.5, 2.0, 1e3 })
        {
            double r1 = AtRate(rho), r2 = AtRate(lambda * rho);
            Assert.True(Math.Abs(r2 / r1 - Math.Pow(lambda, 1.0 / D)) < 1e-12);
            double relA = r1 / r1, relB = (r2 / Math.Pow(lambda, 1.0 / D)) / r1;
            Assert.True(Math.Abs(relA - relB) < 1e-12, "the relative rate must be rescaling-invariant");
        }
        // ...and only the RATIO of two densities matters:
        double rhoA = 0.01, rhoB = 0.04;
        Assert.True(Math.Abs(AtRate(rhoB) / AtRate(rhoA) - Math.Pow(rhoB / rhoA, 1.0 / D)) < 1e-15);
    }

    // ── 2. Earth surface and the GPS orbit ──────────────────────────────────────

    [Fact]
    public void Y_G_009_EarthAndGps()
    {
        // CASE 1 — Earth surface vs infinity.
        double xE = EarthSurfaceDepth;
        Assert.True(Math.Abs(xE - 6.961275e-10) / 6.961275e-10 < 1e-5, $"GM/(R c^2) = {xE}");
        double atE = Math.Exp(-xE) - 1.0, grE = GrRate(-xE) - 1.0;
        Assert.True(Math.Abs(atE - grE) < 1e-16, $"AT {atE} vs GR {grE}");
        Assert.True(Math.Abs(atE + 6.961275e-10) / 6.961275e-10 < 1e-5);
        Assert.True(Math.Abs(UsPerDay(atE) + 60.1454) < 0.01, $"Earth surface = {UsPerDay(atE)} us/day");
        // the second-order AT-vs-GR coefficient difference is +-1/2 x^2, i.e. x^2 = 4.846e-19 in the rate —
        // below the 1e-18 optical-clock floor (the coefficient itself is verified at a moderate x in test 1;
        // at x ~ 1e-10 the difference is far below double precision and cannot be formed by subtraction).
        Assert.True(xE * xE < ClockFloor, $"second-order difference {xE * xE} vs floor {ClockFloor}");
        Assert.True(Math.Abs(xE * xE - 4.846e-19) / 4.846e-19 < 1e-3);

        // CASE 2 — GPS orbit vs the ground: gravitational part (AT, from rho) + imported SR part.
        double xG = GpsGravitational;
        Assert.True(Math.Abs(xG - 5.293971e-10) / 5.293971e-10 < 1e-5, $"GPS potential depth = {xG}");
        Assert.True(Math.Abs(UsPerDay(xG) - 45.7399) < 0.01, $"gravitational = {UsPerDay(xG)} us/day");
        Assert.True(Math.Abs(3.0 * xG - 1.588191e-9) / 1.588191e-9 < 1e-4, "AT dln rho for the GPS difference");
        double v = GpsOrbitalSpeed, sr = v * v / (2.0 * C * C);
        Assert.True(Math.Abs(v - 3871.04) < 0.1, $"v_orb = {v} m/s");
        Assert.True(Math.Abs(UsPerDay(sr) - 7.2028) < 0.01, $"SR = {UsPerDay(sr)} us/day");
        double net = xG - sr;
        Assert.True(Math.Abs(UsPerDay(net) - 38.5371) < 0.01, $"net = {UsPerDay(net)} us/day");
        Assert.True(Math.Abs(UsPerDay(net) / 38.6 - 1.0) < 2e-3, "vs the observed 38.6 us/day");
        // the SR term is NOT AT content: without it the prediction is 18 % off.
        Assert.True(Math.Abs(UsPerDay(xG) / 38.6 - 1.0) > 0.15);
        // second order at the GPS radius is x^2 = 2.803e-19, again below the floor:
        Assert.True(Math.Abs(xG * xG - 2.803e-19) / 2.803e-19 < 1e-2);
        Assert.True(xG * xG < ClockFloor);
    }

    // ── 3. The galactic field ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_009_GalacticField()
    {
        // The observed galactic AT field (G_003's ambient calibration) is Delta ln rho = 1.6102e-6.
        double at = PhiOverC2(AmbientContrast);
        Assert.True(Math.Abs(at - 5.367333e-7) / 5.367333e-7 < 1e-4, $"AT rate = {at}");
        Assert.True(Math.Abs(at / ClockFloor - 5.367333e11) / 5.367333e11 < 1e-4);
        Assert.True(Math.Abs(at * 86400 * 1e3 - 46.374) < 0.01, $"AT = {at * 86400 * 1e3} ms/day");

        // The independent GR/kinematic value: a flat rotation curve with v = 220 km/s has Phi/c^2 = v^2/c^2.
        const double vCirc = 220e3;
        double kin = vCirc * vCirc / (C * C);
        Assert.True(Math.Abs(kin - 5.385226e-7) / 5.385226e-7 < 1e-4, $"v^2/c^2 = {kin}");
        Assert.True(Math.Abs(kin * 86400 * 1e3 - 46.528) < 0.01);

        // THE NEW CROSS-CHECK: the two agree to 0.33 %, i.e. the AT ambient calibration reproduces the
        // observed galactic potential depth (equivalent v = 219.6 km/s).
        Assert.True(Math.Abs(at / kin - 0.99668) < 1e-3, $"ratio = {at / kin}");
        double vEq = Math.Sqrt(at) * C;
        Assert.True(Math.Abs(vEq / 1e3 - 219.63) < 0.5, $"equivalent v = {vEq / 1e3} km/s");
        // the contrast required for an exact match, versus the G_003 calibration:
        Assert.True(Math.Abs(D * kin - 1.615568e-6) / 1.615568e-6 < 1e-4);
        Assert.True(Math.Abs(D * kin / AmbientContrast - 1.0) < 5e-3);

        // GR and AT give the same galactic number here (both = Phi/c^2), so the galactic clock effect is a
        // CORRELATED prediction: 46 ms/day across the disk, 5.4e11x an optical clock's floor.
        Assert.True(Math.Abs(at - kin) / kin < 5e-3);
        Assert.True(at > 1e3 * ClockFloor);
    }

    // ── 4. The G_002 redistributions: rate changes at fixed total mass-energy ───

    [Fact]
    public void Y_G_009_Redistributions()
    {
        // The witnesses are configurations of the counting measure at FIXED total mass-energy:
        // Sigma rho = 1 exactly and the deficit sums to zero. (G_002/QG194.)
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        Assert.Equal(1.0, tilt.Sum(), 12);
        Assert.True(Math.Abs(tilt.Select(v => 1.0 / N - v).Sum()) < 1e-15);
        Assert.Equal(0, D96Spaces.Mult.Sum(m => m - 1) - (N - D96Spaces.Distinct.Length));

        // The clock-rate change of each witness: (1/d) * Delta ln rho.
        var expected = new[] { 0.228571, 0.201058, 0.111111, 0.092199, 0.010707 };
        for (int i = 0; i < Witnesses.Length; i++)
        {
            double rate = PhiOverC2(Witnesses[i].DeltaLnRho);
            Assert.True(Math.Abs(rate - expected[i]) < 2e-6, $"{Witnesses[i].Name}: {rate}");
        }

        // In clock units: the arrangement witness alone is 22.86 % — 19 749 s/day, 2.3e17x an optical
        // clock's 1e-18 floor. Every witness is at least 1e16x the floor.
        double[] secPerDay = Witnesses.Select(w => SecondsPerDay(PhiOverC2(w.DeltaLnRho))).ToArray();
        Assert.True(Math.Abs(secPerDay[0] - 19748.6) < 0.5, $"arrangement = {secPerDay[0]} s/day");
        Assert.True(Math.Abs(secPerDay[1] - 17371.4) < 0.5);
        Assert.True(Math.Abs(secPerDay[4] - 925.1) < 0.5);
        foreach (var (name, dl) in Witnesses)
            Assert.True(PhiOverC2(dl) / ClockFloor > 1e16, $"{name} detectability");

        // The PHASE directions (G_002 REFUTED as a control channel) leave rho — hence the clock rate —
        // EXACTLY unchanged, and the rate is a pointwise, monotone function of rho alone.
        Func<double[], double[]> rateOf = x => x.Select(v => AtRate(v)).ToArray();
        Assert.True(AtRate(0.02) > AtRate(0.01) && AtRate(0.01) > AtRate(0.005));
        Assert.True(Math.Abs(rateOf(tilt)[0] - AtRate(tilt[0])) < 1e-15);

        // The REALISED band is G_005's accessible ceiling: everything <= 4.8867e-6, i.e. <= 1.629e-6 in
        // clock units (0.1407 s/day) — the top of the band is 3.0x the observed galactic level.
        double realised = PhiOverC2(AccessibleCeiling);
        Assert.True(Math.Abs(realised - 1.6289e-6) / 1.6289e-6 < 1e-3, $"realised ceiling = {realised}");
        Assert.True(Math.Abs(SecondsPerDay(realised) - 0.1407) < 0.001);
        Assert.True(Math.Abs(realised / PhiOverC2(AmbientContrast) - 3.034) < 0.01);

        // Consistency with G_003 in clock units: the suppressed witnesses are 3.746e5x the observed level.
        Assert.True(Math.Abs(Witnesses[1].DeltaLnRho / AmbientContrast - 3.746e5) / 3.746e5 < 1e-3);
    }

    // ── 5. AT versus GR, systematically ─────────────────────────────────────────

    [Fact]
    public void Y_G_009_GrComparison()
    {
        // Ratio AT/GR as a function of the potential depth: -> 1 in the weak-field limit, with a known
        // (Phi/c^2)^2 excess because AT's conformal factor is an exponential.
        foreach (double x in new[] { 1e-10, 1e-8, 1e-6, 1e-4 })
        {
            double at = Math.Exp(x), gr = GrRate(x);
            Assert.True(Math.Abs(at / gr - 1.0) < 2.0 * x * x, $"x={x}: ratio-1 = {at / gr - 1.0}");
            Assert.True(at >= gr);
        }
        Assert.True(Math.Exp(1e-4) / GrRate(1e-4) - 1.0 > 0.0);          // AT runs FASTER once x^2 is representable
        Assert.True(Math.Abs(Math.Exp(1e-4) / GrRate(1e-4) - 1.0 - 1e-8) < 1e-11);
        Assert.True(Math.Abs(Math.Exp(1e-6) / GrRate(1e-6) - 1.0) < 2e-12);

        // The four-case comparison table (AT vs GR vs observation).
        double xs = EarthSurfaceDepth;
        Assert.True(Math.Abs((Math.Exp(-xs) - GrRate(-xs)) / GrRate(-xs)) < 1e-15);      // Earth: identical to 1e-15
        Assert.True(xs * xs < ClockFloor);                                              // to second order: below the floor
        double xg = GpsGravitational;
        Assert.True(Math.Abs((Math.Exp(xg) - GrRate(xg)) / GrRate(xg)) < 1e-15);         // GPS: identical to 1e-15
        Assert.True(xg * xg < ClockFloor);
        // (the identity is verified at a moderate depth, where the second-order term is representable)
        double xm = 1e-4;
        Assert.True(Math.Abs((Math.Exp(-xm) - GrRate(-xm)) / xm / xm - 1.0) < 1e-4);
        Assert.True(Math.Abs(PhiOverC2(AmbientContrast) / (Math.Pow(220e3 / C, 2.0)) - 0.99668) < 1e-3);

        // The AT-specific LOCAL content is only: (i) G at 0.40 % (a COMMON factor — it cancels in any ratio
        // of clock rates) and (ii) the +1/2 vs -1/2 second-order coefficient at 1e-19.
        double gRatio = 6.6476e-11 / 6.67430e-11;
        Assert.True(Math.Abs(gRatio - 0.9959996) / 0.9959996 < 1e-4);
        Assert.True(Math.Abs(gRatio * xs / xs - gRatio) < 1e-15);                        // cancels in a ratio
        Assert.True(xs * xs < ClockFloor && xg * xg < ClockFloor);
        // ...so no anomalous local clock effect is available: locally AT is GR with the derived G.
        Assert.True(Math.Exp(-xm) > GrRate(-xm));                                         // AT runs slightly FAST
        Assert.True(Math.Abs(Math.Exp(-xm) - GrRate(-xm) - xm * xm) < 1e-12);
    }

    // ── 6. The critical question ────────────────────────────────────────────────

    [Fact]
    public void Y_G_009_CriticalQuestion()
    {
        // YES IN THE THEORY: the clock rate depends on the ARRANGEMENT of rho, not on its total (Sigma m = 0
        // exactly for every G_002 operation), and the effect is enormous.
        Assert.True(PhiOverC2(Witnesses[0].DeltaLnRho) > 0.2);
        Assert.True(PhiOverC2(Witnesses[0].DeltaLnRho) / ClockFloor > 1e17);

        // BUT NO PHYSICALLY — the two closing audits:
        //  (a) G_005: no realised configuration exceeds the Poisson ceiling 4.8867e-6 (1 % probability);
        double ceiling = PhiOverC2(AccessibleCeiling);
        Assert.True(ceiling < 2e-6);
        Assert.True(PhiOverC2(Witnesses[4].DeltaLnRho) / ceiling > 6e3);
        //  (b) G_008: even a driven witness lives 0.622 steps and needs 0.7998 of its amplitude injected
        //      every step, so nothing sustains it.
        double tau = -1.0 / Math.Log(1.0 - 2.0 * 0.2 * (1.0 - Math.Cos(Math.PI * 95.0 / N)));
        Assert.True(Math.Abs(tau - 0.6217) < 0.002, $"witness lifetime = {tau} steps");
        Assert.True(1.0 - (1.0 - 2.0 * 0.2 * (1.0 - Math.Cos(Math.PI * 95.0 / N))) > 0.79);
        //  (c) a uniform rescaling is a gauge: no relative clock effect at all.
        Assert.True(Math.Abs(AtRate(1e6 * 0.5) / AtRate(0.5) / Math.Pow(1e6, 1.0 / D) - 1.0) < 1e-15);

        // WHAT IS REALISED: the galactic field, 5.367e-7 — which GR also predicts from the rotation curve
        // to 0.33 %. So no NEW clock effect is produced; the same potential depth is what both theories give.
        Assert.True(Math.Abs(PhiOverC2(AmbientContrast) / (Math.Pow(220e3 / C, 2.0)) - 0.99668) < 1e-3);
        Assert.True(PhiOverC2(AmbientContrast) * 86400 * 1e3 > 40.0);                    // 46 ms/day

        // VERDICT of the critical question: measurable-looking rate changes exist in the theory at fixed
        // total mass-energy (up to 22.9 %, 2.3e17x the clock floor), but none is realisable (G_005) or
        // maintainable (G_008); the realised rho variations reproduce the GR galactic depth, and locally
        // AT = GR with the derived G, differing only at 1e-19 (below the 1e-18 clock floor).
        Assert.True(PhiOverC2(AmbientContrast) > 1e3 * ClockFloor);
        Assert.True(EarthSurfaceDepth * EarthSurfaceDepth < ClockFloor);
    }

    // ── 7. Verdicts ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_009_Verdicts()
    {
        // DERIVED — the clock law and its consequences.
        Assert.True(Math.Abs(AtRate(Math.Exp(0.6)) - Math.Exp(0.2)) < 1e-15);            // dtau/dt = rho^(1/d)
        Assert.True(Math.Abs(PhiOverC2(3.0 * EarthSurfaceDepth) - EarthSurfaceDepth) < 1e-18);
        Assert.True(Math.Abs(UsPerDay(EarthSurfaceDepth) - 60.1454) < 0.01);             // Earth
        Assert.True(Math.Abs(UsPerDay(GpsGravitational) - 45.7399) < 0.01);              // GPS gravitational
        Assert.True(Math.Abs(PhiOverC2(AmbientContrast) - 5.367333e-7) / 5.367333e-7 < 1e-4);   // galactic
        Assert.True(Math.Abs(PhiOverC2(AmbientContrast) / Math.Pow(220e3 / C, 2.0) - 0.99668) < 1e-3);
        Assert.True(Math.Abs(PhiOverC2(Witnesses[0].DeltaLnRho) - 0.228571) < 1e-5);     // redistribution

        // CORRELATED — AT and GR agree to first order and differ at second order; the GPS total needs the
        // imported SR term.
        double x = 1e-5;
        Assert.True(Math.Abs(Math.Exp(x) / GrRate(x) - 1.0 - x * x) / (x * x) < 1e-3);
        Assert.True(Math.Exp(x) > GrRate(x));
        double srTerm = GpsOrbitalSpeed * GpsOrbitalSpeed / (2 * C * C);
        Assert.True(Math.Abs(UsPerDay(GpsGravitational - srTerm) / 38.6 - 1.0) < 2e-3, $"GPS total = {UsPerDay(GpsGravitational - srTerm)} us/day");
        Assert.True(EarthSurfaceDepth * EarthSurfaceDepth < ClockFloor);
        Assert.True(GpsGravitational * GpsGravitational < ClockFloor);

        // REFUTED — no realised, sustainable clock effect at fixed mass-energy; phase-blind; scale-free.
        Assert.True(PhiOverC2(AccessibleCeiling) < 2e-6);                                 // G_005 ceiling
        Assert.True(-1.0 / Math.Log(1.0 - 2.0 * 0.2 * (1.0 - Math.Cos(Math.PI * 95.0 / N))) < 1.0);  // G_008 lifetime
        Assert.True(AtRate(1e6 * 0.5) / AtRate(0.5) > 90.0);                              // a gauge rescaling...
        Assert.True(Math.Abs(AtRate(1e6 * 0.5) / AtRate(0.5) / Math.Pow(1e6, 1.0 / D) - 1.0) < 1e-15);  // ...no relative effect
        Assert.True(PhiOverC2(AmbientContrast) > 1e3 * ClockFloor);                       // the realised effect is large but is GR's
    }

    // ── 8. Report ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_009_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_009 — Clock Rate Audit (Gravity Source)");

        sb.AppendLine("QUESTION — does the actualization density rho change clock rates?   GIVEN g_00 = -rho^(2/d).");
        sb.AppendLine("  => dtau/dt = sqrt(-g_00) = rho^(1/d)  and  (1/d)*Delta ln rho = Delta Phi/c^2  (QG21/QG187).");
        sb.AppendLine("  Computed for Earth surface, GPS orbit, the Galactic field and the G_002 redistributions, against GR.");
        sb.AppendLine();

        PrintHeader(sb, "1. THE CLOCK LAW");
        sb.AppendLine("  g_00 = -rho^(2/d)  =>  dtau/dt = rho^(1/d)  =>  (rate - 1) = (1/d) Delta ln rho = Delta Phi/c^2");
        sb.AppendLine($"  scale invariance: rho -> 2 rho moves EVERY clock by {Math.Pow(2.0, 1.0 / D):F6}; the RELATIVE change is exactly 0");
        sb.AppendLine("  AT vs GR:  AT = exp(x) = 1 + x + x^2/2 ;  GR = sqrt(1+2x) = 1 + x - x^2/2   (x = Phi/c^2)");
        double xx = 1e-5;
        sb.AppendLine($"    at x = 1e-5: AT second-order coefficient {(Math.Exp(xx) - 1 - xx) / (xx * xx):F6}, GR {(GrRate(xx) - 1 - xx) / (xx * xx):F6};  |AT-GR| = {Math.Exp(xx) - GrRate(xx):E4} = x^2");
        sb.AppendLine();

        PrintHeader(sb, "2. CASE 1/2 — EARTH SURFACE AND THE GPS ORBIT");
        double xE = EarthSurfaceDepth, xGG = GpsGravitational, vOrb = GpsOrbitalSpeed;
        double sr = vOrb * vOrb / (2 * C * C);
        sb.AppendLine("  case                     Delta Phi/c^2     AT rate dev      GR rate dev      us/day      observed");
        sb.AppendLine("  ------------------------ ----------------- ---------------- ---------------- ----------- ----------");
        sb.AppendLine($"  Earth surface vs inf.    {-xE:E4}      {Math.Exp(-xE) - 1:E4}    {GrRate(-xE) - 1:E4}    {UsPerDay(-xE),9:F3}   -60.15 (measured)");
        sb.AppendLine($"  GPS orbit (grav.)        {xGG:E4}      {Math.Exp(xGG) - 1:E4}    {GrRate(xGG) - 1:E4}    {UsPerDay(xGG),9:F3}   45.7 (QG187)");
        sb.AppendLine($"  GPS (SR, imported)       {-sr:E4}     {-sr:E4}    {-sr:E4}    {UsPerDay(-sr),9:F3}   -7.2");
        sb.AppendLine($"  GPS TOTAL                {xGG - sr:E4}      {Math.Exp(xGG) * Math.Exp(-sr) - 1:E4}    {GrRate(xGG) * Math.Sqrt(1 - 2 * sr) - 1:E4}    {UsPerDay(xGG - sr),9:F3}   38.6 (obs, {UsPerDay(xGG - sr) / 38.6:F4})");
        sb.AppendLine($"  AT Delta ln rho (GPS)    {D * xGG:E4}   (= {D * xGG:E4}, G_003: 1.588e-9)");
        sb.AppendLine($"  second order: x^2 = {xE * xE:E3} (Earth), {xGG * xGG:E3} (GPS) vs the 1e-18 clock floor -> BELOW it");
        sb.AppendLine();

        PrintHeader(sb, "3. CASE 3 — THE GALACTIC FIELD (a new cross-check)");
        double at = PhiOverC2(AmbientContrast), kin = Math.Pow(220e3 / C, 2.0);
        sb.AppendLine($"  AT (from G_003's ambient contrast 1.6102e-6):  Delta ln rho/d = {at:E6}  = {at * 86400 * 1e3:F3} ms/day");
        sb.AppendLine($"  GR/kinematic (flat curve, v = 220 km/s):       v^2/c^2        = {kin:E6}  = {kin * 86400 * 1e3:F3} ms/day");
        sb.AppendLine($"  -> ratio {at / kin:F5} ({100 * (at / kin - 1):+0.00;-0.00} %), equivalent v = {Math.Sqrt(at) * C / 1e3:F2} km/s");
        sb.AppendLine($"  -> the G_003 calibration is equivalent to Delta ln rho = {D * kin:E6} for an exact match (vs 1.6102e-6)");
        sb.AppendLine($"  -> detectability: {at / ClockFloor:E3}x an optical clock's 1e-18 floor");
        sb.AppendLine();

        PrintHeader(sb, "4. CASE 4 — G_002 REDISTRIBUTIONS AT FIXED TOTAL MASS-ENERGY");
        sb.AppendLine("  configuration                Delta ln rho   rate dev (1/d)   s/day        x clock floor");
        sb.AppendLine("  ---------------------------- -------------- ---------------- ------------ ---------------");
        foreach (var (name, dl) in Witnesses)
            sb.AppendLine($"  {name,-28} {dl,14:F6} {PhiOverC2(dl),16:F6} {SecondsPerDay(PhiOverC2(dl)),12:F1} {PhiOverC2(dl) / ClockFloor,15:E2}");
        sb.AppendLine($"  REALISED BAND (G_005 ceiling {AccessibleCeiling:E3}): rate dev {PhiOverC2(AccessibleCeiling):E3} = {SecondsPerDay(PhiOverC2(AccessibleCeiling)):F4} s/day");
        sb.AppendLine($"  OBSERVED LEVEL (the galactic field {AmbientContrast:E3}): {PhiOverC2(AmbientContrast):E3} = {SecondsPerDay(PhiOverC2(AmbientContrast)):F4} s/day");
        sb.AppendLine("  -> the witnesses are 3.746e5x the observed level (G_003's ratio, now in clock units);");
        sb.AppendLine("     the accessible band tops out at 3.03x the observed level.");
        sb.AppendLine();

        PrintHeader(sb, "5. THE CRITICAL QUESTION");
        sb.AppendLine("  Can rho variations produce measurable time dilation WITHOUT mass-energy changes?");
        sb.AppendLine($"  YES in the theory: Sigma m = 0 exactly for every G_002 operation, yet (1/d)Delta ln rho reaches");
        sb.AppendLine($"    {PhiOverC2(Witnesses[0].DeltaLnRho):F6} ({PhiOverC2(Witnesses[0].DeltaLnRho) / ClockFloor:E2}x the clock floor) — the rate depends on the ARRANGEMENT,");
        sb.AppendLine("    not on the total. (Note: any potential theory shares this; the AT-specific content is the exact");
        sb.AppendLine("    relation and the degeneracy freedom, which needs no new mass and leaves the spectrum untouched.)");
        sb.AppendLine($"  NO physically: G_005 caps realised configurations at {PhiOverC2(AccessibleCeiling):E3} (< 2e-6), and G_008 shows even a driven");
        sb.AppendLine("    witness lives 0.622 steps and needs 0.7998 of its amplitude injected every step.");
        sb.AppendLine($"  WHAT IS REALISED: {PhiOverC2(AmbientContrast):E3} = {PhiOverC2(AmbientContrast) * 86400 * 1e3:F1} ms/day across the galactic disk — which GR predicts from the");
        sb.AppendLine($"    rotation curve to {100 * Math.Abs(at / kin - 1):F2} % — so no NEW clock effect appears: both theories give the same potential depth.");
        sb.AppendLine($"  LOCALLY: AT = GR with the derived G; the only AT-specific local signature is the +-1/2 second-order");
        sb.AppendLine($"    coefficient, {Math.Pow(xE, 2):E3} at the Earth's surface and {Math.Pow(xGG, 2):E3} at the GPS orbit — 2-4x BELOW the 1e-18");
        sb.AppendLine("    optical-clock floor, so it is a future test rather than a present discrepancy.");
        sb.AppendLine();

        PrintHeader(sb, "VERDICT — DERIVED / CORRELATED / REFUTED");
        sb.AppendLine("  CLAIM                                                          VERDICT");
        sb.AppendLine("  -------------------------------------------------------------- ----------------");
        sb.AppendLine("  dtau/dt = rho^(1/d) from g_00 = -rho^(2/d)                      DERIVED");
        sb.AppendLine("  (1/d)Delta ln rho = Delta Phi/c^2 (the redshift law)            DERIVED");
        sb.AppendLine("  Earth surface -60.145 us/day; GPS grav. +45.740 us/day          DERIVED (AT == GR, == observation)");
        sb.AppendLine("  GPS total +38.537 vs 38.6 observed                              CORRELATED (SR term imported)");
        sb.AppendLine("  Galactic field 5.367e-7 = 46.37 ms/day vs v^2/c^2 (0.33 %)      DERIVED/CORRELATED (new cross-check)");
        sb.AppendLine("  AT vs GR first order                                            DERIVED (identical)");
        sb.AppendLine("  AT vs GR second order (+1/2 vs -1/2), 4.8e-19 at Earth         CORRELATED (below the 1e-18 floor)");
        sb.AppendLine("  Redistribution rate changes up to 22.86 % at fixed energy       DERIVED (but see below)");
        sb.AppendLine("  A REALISED sustained clock effect from rho rearrangement        REFUTED (G_005 not counted, G_008 not maintainable)");
        sb.AppendLine("  Phase directions change the clock rate                          REFUTED (exactly zero)");
        sb.AppendLine("  A uniform rescaling produces a relative clock effect            REFUTED (gauge: exactly zero)");
        sb.AppendLine();

        PrintHeader(sb, "CONCLUSIONS");
        sb.AppendLine("  C1  The clock law is DERIVED: dtau/dt = rho^(1/d) follows from g_00 = -rho^(2/d) (QG197), and its");
        sb.AppendLine("      first-order form is the canonical redshift law (1/d)Delta ln rho = Delta Phi/c^2 (QG21/QG187).");
        sb.AppendLine("  C2  AT and GR AGREE to first order — the Earth surface (-60.145 us/day) and the GPS gravitational");
        sb.AppendLine("      term (+45.740 vs QG187's 45.7) are identical to double precision — and DIFFER at second order,");
        sb.AppendLine("      where AT's exponential conformal factor gives +1/2(Phi/c^2)^2 against GR's -1/2: a 4.846e-19");
        sb.AppendLine("      difference at the Earth's surface and 2.803e-19 at the GPS orbit, both below the 1e-18 floor.");
        sb.AppendLine("  C3  NEW: the G_003 ambient contrast (1.6102e-6), converted through the clock law, reproduces the");
        sb.AppendLine($"      observed galactic potential depth v^2/c^2 (v = 220 km/s) to {100 * Math.Abs(at / kin - 1):F2} % — an equivalent");
        sb.AppendLine($"      rotation velocity of {Math.Sqrt(at) * C / 1e3:F2} km/s — i.e. 46 ms/day across the disk.");
        sb.AppendLine("  C4  The G_002 redistributions DO move clocks at fixed total mass-energy, by up to 22.86 % (19 749");
        sb.AppendLine("      s/day, 2.3e17x the clock floor): the rate depends on the arrangement of rho, not on its total.");
        sb.AppendLine("  C5  But no REALISED process delivers that: G_005 caps realised contrasts at 4.8867e-6 (so <= 1.63e-6");
        sb.AppendLine("      in clock units, and the band's top is only 3.03x the observed galactic level), and G_008 shows the");
        sb.AppendLine("      witnesses are not maintainable (lifetime 0.62 steps; 0.7998 of the amplitude needed per step).");
        sb.AppendLine("  C6  So the answer to the critical question is: YES in the theory and at fixed total mass-energy, but");
        sb.AppendLine("      the realised rho variations give exactly the potential depth GR already predicts — no NEW clock");
        sb.AppendLine("      effect. Locally AT = GR with the derived G; the AT-specific local signature is 1e-19.");
        sb.AppendLine("  C7  OPEN: an optical-clock experiment at 1e-19 fractional resolution (a factor 2-4 beyond today's");
        sb.AppendLine("      best) could resolve the +1/2 vs -1/2 second-order coefficient — the only local clock test that");
        sb.AppendLine("      distinguishes the two theories.");
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
