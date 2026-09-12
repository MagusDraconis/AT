using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_010 — Time Control Feasibility Audit (group G — Gravity Source).
///
/// GIVEN: dtau/dt = rho^(1/d) (G_009, from g_00 = -rho^(2/d)) and the G_008 suppression law
///        (a mode-matched drive costs (1 - mu_k) of the amplitude PER STEP; nothing is self-sustaining).
///
/// QUESTION: what sustained clock shift can exist under ALLOWED driving?
/// Targets: 1 ns/day, 1 us/day, 1 ms/day, 1 s/day.  For each: required Delta rho, required drive,
/// steady-state profile, power scaling.
///
/// RESULTS (d = 3; mu_1 = 0.9997858350, mu_95 = 0.20021417; G_005 ceiling Delta ln rho <= 4.8867e-6)
///
///   target     f = T/86400    Delta ln rho    drive k=1      drive k=95     x ceiling   equiv. well
///   1 ns/day   1.157407e-14   3.472222e-14    7.4363e-18     2.7770e-14     7.10e-9     32 m/s
///   1 us/day   1.157407e-11   3.472222e-11    7.4363e-15     2.7770e-11     7.10e-6     1.02 km/s
///   1 ms/day   1.157407e-08   3.472222e-08    7.4363e-12     2.7770e-08     7.10e-3     32.3 km/s
///   1 s/day    1.157407e-05   3.472222e-05    7.4363e-09     2.7770e-05     7.10 (>1)   1020 km/s
///   ceiling    1.628900e-06   4.886700e-06    1.0466e-09     3.9083e-06     1           383 km/s
///   observed   5.367333e-07   1.610200e-06    3.4485e-10     1.2878e-06     0.330       220 km/s
///
/// POWER SCALING: the drive per unit clock shift is d*(1 - mu_k) = 6.4250e-4 for the smoothest mode and
/// 2.3994 for a cell-scale mode (ratio 3734.4, G_008's number); 1 - mu_k ~ d*(pi k/N)^2, so
/// P ~ f * (k/N)^2 — LINEAR in the target shift, QUADRATIC in the relative spatial frequency, and
/// N^-2 cheaper in a larger system at fixed physical wavelength.
///
/// VERDICTS
///   PRACTICAL         1 ns/day, 1 us/day and 1 ms/day: all inside the accessible band (7.10e-9, 7.10e-6
///                     and 7.10e-3 of it), needing density excursions of 1.8e-16, 1.8e-13 and 1.8e-10 count
///                     units and per-step drives of 7.4e-18 ... 2.8e-8.  Positivity and count conservation
///                     never bind; the required potential wells are small (32 m/s, 1.0 km/s, 32 km/s).
///                     PRACTICAL AS NUMBERS — but see REFUTED below: none of them happens on its own.
///   ASTROPHYSICAL ONLY  the band's top is 0.1407 s/day and the OBSERVED galactic field is already 0.0464
///                     s/day (33 % of the band, a 220 km/s well), so shifts above ~10 ms/day live at the
///                     galactic/cluster scale; 1 s/day needs a 1020 km/s (cluster-depth) well, 16626x the
///                     Earth's surface depth.
///   REFUTED            1 s/day as a spontaneously realised or sustained state (Delta ln rho = 3.47e-5 is
///                     7.10x the Poisson ceiling); and — the sharper statement — ANY sustained clock shift
///                     whatsoever without an external mode-matched driver, because the canonical chain
///                     supplies none (G_005 caps spontaneous contrasts; G_008 shows a local state lives
///                     0.622 steps and the branching flow is arrangement-neutral).
///
/// Deterministic: exact algebra, no randomness.  No reclassification; D_040 untouched.
/// </summary>
public class Y_G_010_Tests : ResearchTestBase
{
    public Y_G_010_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double CeilingContrast = 4.8867e-6;   // G_005 Poisson ceiling on Delta ln rho
    private const double ObservedContrast = 1.6102e-6;  // the observed galactic field (G_003)
    private const double ClockFloor = 1e-18;            // best optical-clock fractional resolution

    private static double Mu(int k) => 1.0 - 2.0 * Damping * (1.0 - Math.Cos(Math.PI * k / N));

    /// <summary>Fractional clock shift for a target expressed in seconds per day.</summary>
    private static double FracFromSecondsPerDay(double seconds) => seconds / 86400.0;

    /// <summary>Required density contrast for a fractional clock shift: Delta ln rho = d * f (G_009).</summary>
    private static double RequiredContrast(double secondsPerDay) => D * FracFromSecondsPerDay(secondsPerDay);

    /// <summary>Required drive per step for a mode-k steady state of relative amplitude Delta ln rho.</summary>
    private static double RequiredDrive(double deltaLnRho, int k) => (1.0 - Mu(k)) * deltaLnRho;

    /// <summary>The largest sustained shift the accessible band allows.</summary>
    private static double CeilingSecondsPerDay => CeilingContrast / D * 86400.0;

    /// <summary>Equivalent circular velocity of a potential well with a given fractional depth.</summary>
    private static double EquivalentVelocity(double secondsPerDay) => Math.Sqrt(FracFromSecondsPerDay(secondsPerDay)) * C;

    /// <summary>The canonical witness tilt (a within-multiplet, high-k configuration).</summary>
    private static double[] Tilt => Spread(D96Spaces.Mult, 1.0, TiltFractions);

    private static double[] Mode(int k) => Enumerable.Range(0, N)
        .Select(i => Math.Cos(Math.PI * k * (i + 0.5) / N)).ToArray();

    /// <summary>Steady state of a mode-matched drive s = c*v_k: c*v_k/(1 - mu_k) (G_008).</summary>
    private static double[] SteadyProfile(int k, double c, out double[] drive)
    {
        var v = Mode(k);
        drive = v.Select(x => c * x).ToArray();
        return v.Select(x => c * x / (1.0 - Mu(k))).ToArray();
    }

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double L1Norm(double[] x) => x.Sum(Math.Abs);

    private static readonly (string Name, double SecondsPerDay)[] Targets =
    [
        ("1 ns/day", 1e-9), ("1 us/day", 1e-6), ("1 ms/day", 1e-3), ("1 s/day", 1.0)
    ];

    // ── 1. The four targets ────────────────────────────────────────────────────

    [Fact]
    public void Y_G_010_TargetInventory()
    {
        // f = T/86400 and the required contrast Delta ln rho = d*f (G_009's clock law, inverted).
        foreach (var (name, sec) in Targets)
            Assert.True(Math.Abs(RequiredContrast(sec) - 3.0 * sec / 86400.0) < 1e-18, name);
        Assert.True(Math.Abs(RequiredContrast(1e-9) - 3.472222e-14) / 3.472222e-14 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1e-6) - 3.472222e-11) / 3.472222e-11 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1e-3) - 3.472222e-8) / 3.472222e-8 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1.0) - 3.472222e-5) / 3.472222e-5 < 1e-5);

        // Equivalent potential wells: a fractional depth f is a well whose circular velocity is c*sqrt(f).
        Assert.True(Math.Abs(EquivalentVelocity(1e-9) - 32.25) < 0.05, $"1 ns/day -> {EquivalentVelocity(1e-9)} m/s");
        Assert.True(Math.Abs(EquivalentVelocity(1e-6) - 1019.91) < 0.5);
        Assert.True(Math.Abs(EquivalentVelocity(1e-3) - 32252.53) < 5.0);
        Assert.True(Math.Abs(EquivalentVelocity(1.0) - 1019914.65) < 100.0);
        // For scale: the Earth's surface is 7.91 km/s (G_009's 60.145 us/day).
        double earth = Math.Sqrt(60.1454 / 86400.0 / 1e6) * C;
        Assert.True(Math.Abs(earth - 7910.0) < 10.0, $"Earth surface v_eq = {earth} m/s");
        Assert.True(EquivalentVelocity(1e-3) / earth > 4.0 && EquivalentVelocity(1e-3) / earth < 5.0);

        // The G_005 ceiling and the observed galactic field, in the same units.
        Assert.True(Math.Abs(CeilingSecondsPerDay - 0.140737) < 1e-5, $"ceiling = {CeilingSecondsPerDay} s/day");
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.046373) < 1e-5);
        Assert.True(Math.Abs(ObservedContrast / CeilingContrast - 0.32951) < 1e-4, "the observed field is 33 % of the band");
        Assert.True(Math.Abs(EquivalentVelocity(CeilingSecondsPerDay) - 3.826e5) < 1e2);
        Assert.True(Math.Abs(EquivalentVelocity(ObservedContrast / D * 86400.0) - 2.1963e5) < 1e2);
    }

    // ── 2. Required density change, profile and positivity ─────────────────────

    [Fact]
    public void Y_G_010_RequiredDensity()
    {
        double rhoBar = 1.0 / N;
        var v1 = Mode(1);
        var v95 = Mode(95);

        foreach (var (name, sec) in Targets)
        {
            double dl = RequiredContrast(sec);
            // The steady profile is a pure Neumann mode: rho = rhoBar + (rhoBar*dl/2)*v_k, i.e. a density
            // excursion of (rhoBar*dl/2).  Positivity and the count never bind at these levels.
            double m1 = Math.Max(Math.Abs(v1.Max()), Math.Abs(v1.Min()));
            var profile = v1.Select(x => rhoBar + rhoBar * dl / 2.0 * (x / m1)).ToArray();
            Assert.True(profile.Min() > 0.0, $"{name}: min rho = {profile.Min()}");
            Assert.True(Math.Abs(profile.Sum() - 1.0) < 1e-12, $"{name}: Sigma rho = {profile.Sum()}");
            double amp = rhoBar * dl / 2.0;
            Assert.True(amp / rhoBar < 1e-4, $"{name}: relative excursion {amp / rhoBar}");
            // the log-contrast reproduces Delta ln rho (analytically exact; numerically only above the fp floor
            // of rhoBar ~ 1.7e-18, which the 1 ns/day excursion of 1.8e-16 is just 100x above):
            // Delta ln rho = 2*atanh(dl/2) = dl + dl^3/12 + ... (the stable form: log((1+a)/(1-a)) loses
            // precision near a = 0 because the 1e-16 floor of double is 1 % of the 1 ns/day excursion)
            Assert.True(Math.Abs(2.0 * Math.Atanh(dl / 2.0) / dl - 1.0) < 1e-9, $"{name}");
            if (sec >= 1e-3) Assert.True(Math.Abs(Math.Log(profile.Max() / profile.Min()) / dl - 1.0) < 1e-6, $"{name}");
        }
        Assert.True(Math.Abs(1.0 / N * RequiredContrast(1e-9) / 2.0 - 1.808449e-16) / 1.808449e-16 < 1e-4);
        Assert.True(Math.Abs(1.0 / N * RequiredContrast(1.0) / 2.0 - 1.808449e-7) / 1.808449e-7 < 1e-4);

        // Inside or outside the Poisson-accessible band?
        Assert.True(RequiredContrast(1e-9) / CeilingContrast < 1e-8);
        Assert.True(RequiredContrast(1e-6) / CeilingContrast < 1e-5);
        Assert.True(RequiredContrast(1e-3) / CeilingContrast < 1e-2);
        Assert.True(RequiredContrast(1.0) / CeilingContrast > 7.0, "1 s/day EXCEEDS the accessible band");
        Assert.True(Math.Abs(RequiredContrast(1.0) / CeilingContrast - 7.1055) < 1e-3);
        // So the band's top is between 1 ms/day and 1 s/day:
        Assert.True(CeilingSecondsPerDay > 1e-3 && CeilingSecondsPerDay < 1.0);
        // ...and the observed galactic field already uses a third of it.
        Assert.True(ObservedContrast / CeilingContrast > 0.3);
    }

    // ── 3. Required drive and the steady-state profile ─────────────────────────

    [Fact]
    public void Y_G_010_RequiredDriveAndProfile()
    {
        // A mode-matched drive s = c*v_k holds the exact steady state c*v_k/(1 - mu_k) (G_008): the drive
        // per unit PROFILE amplitude is (1 - mu_k), and the drive per unit CLOCK shift is d*(1 - mu_k).
        var v1 = Mode(1);
        var drive = v1.Select(x => 1e-3 * x).ToArray();
        var steady = SteadyProfile(1, 1e-3, out var drv);
        Assert.True(steady.All(x => Math.Abs(x) < 10.0));    // the k = 1 gain is 4669x, so the amplitude is 4.67
        Assert.True(Math.Abs(steady.Max() / (v1.Max() * 1e-3 / (1.0 - Mu(1))) - 1.0) < 1e-12);
        Assert.True(L1Norm(drv) > 0.0);

        // Iterating the driven recursion reproduces it (the G_008 verification, repeated at this amplitude).
        // 150 000 steps is enough for the k = 1 transient (mu_1^150000 = 1e-14).
        var x = new double[N];
        for (int t = 0; t < 150000; t++) x = Step(x).Select((val, i) => val + drive[i]).ToArray();
        double coef = x.Zip(v1, (a, b) => a * b).Sum() / v1.Sum(b => b * b);
        Assert.True(Math.Abs(coef / (1e-3 / (1.0 - Mu(1))) - 1.0) < 1e-9, $"driven fixed point {coef}");

        // The four targets' per-step drives, for the smoothest and the most local mode.
        foreach (var (name, sec) in Targets)
        {
            double dl = RequiredContrast(sec);
            double d1 = RequiredDrive(dl, 1), d95 = RequiredDrive(dl, 95);
            Assert.True(Math.Abs(d1 / dl - 2.141650e-4) / 2.141650e-4 < 1e-4, $"{name}: 1-mu_1");
            Assert.True(Math.Abs(d95 / dl - 0.799786) / 0.799786 < 1e-4, $"{name}: 1-mu_95");
            Assert.True(d1 < 1e-8 && d95 < 1e-4, $"{name}: drives {d1} / {d95}");
        }
        Assert.True(Math.Abs(RequiredDrive(RequiredContrast(1e-9), 1) - 7.436285e-18) / 7.436285e-18 < 1e-4);
        Assert.True(Math.Abs(RequiredDrive(RequiredContrast(1.0), 1) - 7.436285e-9) / 7.436285e-9 < 1e-4);
        Assert.True(Math.Abs(RequiredDrive(RequiredContrast(1.0), 95) - 2.777034e-5) / 2.777034e-5 < 1e-4);

        // Maintaining it costs that drive EVERY step (G_008: nothing is self-sustaining).
        double perStep = RequiredDrive(RequiredContrast(1e-3), 95);
        double accumulated = 200 * perStep;
        Assert.True(Math.Abs(accumulated - 5.554e-6) / 5.554e-6 < 1e-2, $"accumulated = {accumulated}");
        // the smoothest mode is 3734x cheaper per unit shift:
        Assert.True(Math.Abs((1.0 - Mu(95)) / (1.0 - Mu(1)) - 3734.4) < 1.0);
    }

    // ── 4. Power scaling ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_010_PowerScaling()
    {
        // P is LINEAR in the target shift: the required drive per step is proportional to Delta ln rho = d*f.
        double pNs = RequiredDrive(RequiredContrast(1e-9), 1);
        double pUs = RequiredDrive(RequiredContrast(1e-6), 1);
        double pMs = RequiredDrive(RequiredContrast(1e-3), 1);
        double pS = RequiredDrive(RequiredContrast(1.0), 1);
        Assert.True(Math.Abs(pUs / pNs - 1e3) < 1.0, $"us/ns = {pUs / pNs}");
        Assert.True(Math.Abs(pMs / pNs - 1e6) / 1e6 < 1e-3);
        Assert.True(Math.Abs(pS / pNs - 1e9) / 1e9 < 1e-3);

        // P is QUADRATIC in the relative spatial frequency: 1 - mu_k = 2d(1 - cos(pi k/N)) ~ d(pi k/N)^2.
        foreach (int k in new[] { 1, 2, 4, 8 })
        {
            double exact = 1.0 - Mu(k), approx = Damping * Math.Pow(Math.PI * k / N, 2.0);
            Assert.True(Math.Abs(exact / approx - 1.0) < 0.02, $"k={k}: exact {exact} vs approx {approx}");
        }
        // the continuum form OVERESTIMATES at high k (exact 0.4000 vs 0.4935 at k = 48): the quadratic law is
        // the long-wavelength limit, which is where the smooth (cheap) modes live.
        Assert.True(1.0 - Mu(48) < Damping * Math.Pow(Math.PI * 48.0 / N, 2.0));
        // ...so doubling k quadruples the cost, and the (k/N)^2 law makes a bigger system cheaper:
        double c2 = 1.0 - Mu(2), c4 = 1.0 - Mu(4);
        Assert.True(Math.Abs(c4 / c2 - 4.0) < 0.1, $"cost(4)/cost(2) = {c4 / c2}");
        double big = 1.0 - (1.0 - 2.0 * Damping * (1.0 - Math.Cos(Math.PI * 2.0 / 1024.0)));
        Assert.True(Math.Abs(c2 / big - Math.Pow(1024.0 / 96.0, 2.0)) < 0.2, $"size scaling {c2 / big}");

        // The cost per unit clock shift: d*(1 - mu_k) = 6.4250e-4 (smoothest) vs 2.3994 (cell-scale).
        Assert.True(Math.Abs(D * (1.0 - Mu(1)) - 6.424950e-4) / 6.424950e-4 < 1e-4, $"k=1: {D * (1.0 - Mu(1))}");
        Assert.True(Math.Abs(D * (1.0 - Mu(95)) - 2.3994) / 2.3994 < 1e-4, $"k=95: {D * (1.0 - Mu(95))}");
        // and the observed galactic field costs, per step, only 3.45e-10 in the smoothest mode:
        Assert.True(Math.Abs(RequiredDrive(ObservedContrast, 1) - 3.448485e-10) / 3.448485e-10 < 1e-4);

        // Absolute drive magnitudes as fractions of the total count (Sigma rho = 1):
        foreach (var (name, sec) in Targets)
            Assert.True(RequiredDrive(RequiredContrast(sec), 1) < 1e-7, name);
    }

    // ── 5. Where the practical boundaries lie ──────────────────────────────────

    [Fact]
    public void Y_G_010_PracticalBounds()
    {
        // The accessible band's top: 0.1407 s/day.  The observed galactic field is already 33 % of it
        // (0.0464 s/day) — so the "astrophysical" region starts at the tens-of-ms/day level.
        Assert.True(Math.Abs(CeilingSecondsPerDay - 0.140737) < 1e-5);
        Assert.True(ObservedContrast / CeilingContrast > 0.32 && ObservedContrast / CeilingContrast < 0.34);
        Assert.True(CeilingSecondsPerDay / 1e-3 > 100.0);          // 140x above the 1 ms/day target
        Assert.True(CeilingSecondsPerDay / 1.0 < 1.0);             // and 7x below 1 s/day

        // Classification of the four targets against the band:
        Assert.True(RequiredContrast(1e-9) / CeilingContrast < 1e-8);       // PRACTICAL
        Assert.True(RequiredContrast(1e-6) / CeilingContrast < 1e-5);       // PRACTICAL
        Assert.True(RequiredContrast(1e-3) / CeilingContrast < 1e-2);       // PRACTICAL (as numbers)
        Assert.True(RequiredContrast(1.0) / CeilingContrast > 7.0);         // REFUTED

        // Detectability of the targets with an optical clock (1e-18): all four are far above the floor,
        // so the shifts themselves are measurable — the obstacle is producing them.
        foreach (var (name, sec) in Targets)
        {
            double f = FracFromSecondsPerDay(sec);
            Assert.True(f / ClockFloor > 1e4, $"{name}: {f / ClockFloor}");
        }
        Assert.True(FracFromSecondsPerDay(1e-9) / ClockFloor > 1e4);
        Assert.True(FracFromSecondsPerDay(1.0) / ClockFloor > 1e13);

        // For the 1 ms/day target the required well is a 32 km/s one — deeper than the Earth's surface but
        // shallower than a galaxy: 16.6x the Earth's surface depth (60.145 us/day).
        Assert.True(Math.Abs(1e-3 / 60.1454e-6 - 16.63) < 0.01, "1 ms/day is 16.63x the Earth-surface shift");
        Assert.True(EquivalentVelocity(1e-3) > 30e3 && EquivalentVelocity(1e-3) < 35e3);
        Assert.True(EquivalentVelocity(1.0) > 1.0e6);                        // 1020 km/s: cluster depth
    }

    // ── 6. No driver exists in the canonical chain ────────────────────────────

    [Fact]
    public void Y_G_010_NoDriver()
    {
        // The drives above are mode-matched and STRUCTURED.  The canonical chain supplies none:
        //  (a) the branching flow is arrangement-neutral (it rescales every cell equally);
        var tilt = Tilt;
        Assert.True(MaxAccelerationDifference(tilt, tilt.Select(v => 1e6 * v).ToArray(), D) < 1e-6);
        Assert.True(NativeMetricDynamics.BranchingContinuity(1.08, 24));
        Assert.True(NativeMetricDynamics.CountConserved(1.08, 24));
        Assert.True(NativeMetricDynamics.DensityStaticAtCriticality());
        //  (b) the attractor ERASES arrangements (basin 1);
        Assert.True(UniversalAttractor.BasinFraction(N, 8) >= 0.9);
        //  (c) an undriven local state dies in 0.622 steps (G_008) — so it must be re-created every step;
        Assert.True(Math.Abs(-1.0 / Math.Log(Mu(95)) - 0.6217) < 0.002);
        //  (d) and the Poisson ceiling (G_005) caps anything SPONTANEOUS at Delta ln rho <= 4.8867e-6.
        Assert.True(CeilingContrast / D * 86400.0 < 0.15);

        // A uniform drive is not allowed either (it would change the count): a steady state needs Sigma s = 0,
        // and the phase directions change nothing at all (G_002/G_009).
        var v1 = Mode(1);
        var holdDrive = v1.Select(x => 1e-3 * x).ToArray();
        Assert.True(Math.Abs(holdDrive.Sum()) < 1e-15);
        // ...and it must be MODE-MATCHED: here the drive IS the mode shape, to machine precision.
        Assert.True(Math.Abs(holdDrive.Zip(v1, (a, b) => a * b).Sum() / v1.Sum(x => x * x) - 1e-3) < 1e-18);
        Assert.True(UniversalAttractor.IsExactFixedPoint(ActualizationStructures.PersistentActivity(N)));

        // VERDICT: every target above is "achievable" only with an external, mode-matched, structured agent
        // that injects (1 - mu_k) of the amplitude every step — nothing in the theory provides it.
        Assert.True((1.0 - Mu(95)) > 0.79 && (1.0 - Mu(1)) < 3e-4);
    }

    // ── 7. Verdicts ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_010_Verdicts()
    {
        // PRACTICAL — as NUMBERS, given a driver: 1 ns/day, 1 us/day and 1 ms/day sit far inside the
        // accessible band with negligible density excursions and per-step drives.
        foreach (var (name, sec) in Targets.Take(3))
        {
            Assert.True(RequiredContrast(sec) < 1e-6, name);                       // inside the 4.8867e-6 band
            Assert.True(RequiredDrive(RequiredContrast(sec), 1) < 1e-8, name);     // negligible per step
            Assert.True(1.0 / N * RequiredContrast(sec) / 2.0 < 1e-9, name);       // negligible excursion
            Assert.True(FracFromSecondsPerDay(sec) / ClockFloor > 1e4, name);      // and measurable once produced
        }
        Assert.True(RequiredContrast(1e-9) / CeilingContrast < 1e-8);
        Assert.True(RequiredContrast(1e-3) / CeilingContrast < 1e-2);

        // ASTROPHYSICAL ONLY — the band's top and the observed galactic field.
        Assert.True(Math.Abs(CeilingSecondsPerDay - 0.140737) < 1e-5);
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.046373) < 1e-5);
        Assert.True(ObservedContrast / CeilingContrast > 0.3);
        Assert.True(EquivalentVelocity(ObservedContrast / D * 86400.0) > 2e5);     // 220 km/s well

        // REFUTED — 1 s/day spontaneously, and any sustained shift without an external driver.
        Assert.True(RequiredContrast(1.0) / CeilingContrast > 7.0);
        Assert.True(EquivalentVelocity(1.0) > 1e6);                               // 1020 km/s: cluster depth
        Assert.True(-1.0 / Math.Log(Mu(95)) < 1.0);                               // no undriven local state
        Assert.True(MaxAccelerationDifference(Tilt, Tilt.Select(v => 1e6 * v).ToArray(), D) < 1e-6);
        Assert.True(UniversalAttractor.BasinFraction(N, 8) >= 0.9);

        // The classification is ordered: the required contrast grows 1e9x from the first to the fourth
        // target while the ceiling does not move.
        double[] needed = Targets.Select(t => RequiredContrast(t.SecondsPerDay)).ToArray();
        for (int i = 1; i < needed.Length; i++) Assert.True(needed[i] > needed[i - 1]);
        Assert.True(Math.Abs(needed[3] / needed[0] - 1e9) / 1e9 < 1e-3);
        Assert.True(needed[0] < CeilingContrast && needed[3] > CeilingContrast);
    }

    // ── 8. Report ──────────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_010_Run()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-G_010 — Time Control Feasibility Audit (Gravity Source)");

        sb.AppendLine("GIVEN dtau/dt = rho^(1/d) (G_009) and the G_008 suppression law (a mode-matched drive costs");
        sb.AppendLine("  (1 - mu_k) of the amplitude PER STEP; nothing is self-sustaining).");
        sb.AppendLine("QUESTION: what sustained clock shift can exist under ALLOWED driving?");
        sb.AppendLine();

        PrintHeader(sb, "THE FOUR TARGETS");
        sb.AppendLine("  target      f = T/86400    Delta ln rho    drive/step k=1   drive/step k=95   x band   equiv. well");
        sb.AppendLine("  ----------- -------------- --------------- ---------------- ----------------- -------- -------------");
        foreach (var (name, sec) in Targets)
        {
            double f = FracFromSecondsPerDay(sec), dl = RequiredContrast(sec);
            sb.AppendLine($"  {name,-11} {f,14:E6} {dl,15:E6} {RequiredDrive(dl, 1),16:E6} {RequiredDrive(dl, 95),17:E6} {dl / CeilingContrast,8:E2} {EquivalentVelocity(sec),10:F2} m/s");
        }
        sb.AppendLine($"  {"band top",-11} {CeilingContrast / D,14:E6} {CeilingContrast,15:E6} {RequiredDrive(CeilingContrast, 1),16:E6} {RequiredDrive(CeilingContrast, 95),17:E6} {1.0,8:F2} {EquivalentVelocity(CeilingSecondsPerDay),10:F0} m/s");
        sb.AppendLine($"  {"observed",-11} {ObservedContrast / D,14:E6} {ObservedContrast,15:E6} {RequiredDrive(ObservedContrast, 1),16:E6} {RequiredDrive(ObservedContrast, 95),17:E6} {ObservedContrast / CeilingContrast,8:F2} {EquivalentVelocity(ObservedContrast / D * 86400.0),10:F0} m/s");
        sb.AppendLine($"  -> the accessible band tops out at {CeilingSecondsPerDay:F4} s/day ({CeilingSecondsPerDay * 1e3:F1} ms/day), and the OBSERVED");
        sb.AppendLine($"     galactic field already uses {ObservedContrast / CeilingContrast * 100:F1} % of it ({ObservedContrast / D * 86400.0 * 1e3:F1} ms/day, a 220 km/s well).");
        sb.AppendLine();

        PrintHeader(sb, "REQUIRED DENSITY, DRIVE AND STEADY-STATE PROFILE");
        sb.AppendLine("  A sustained shift is a pure Neumann-mode steady state rho = rhoBar + (rhoBar*Delta ln rho/2)*v_k,");
        sb.AppendLine("  held by the mode-matched drive s = c*v_k with c = (1 - mu_k)*amplitude (G_008, exact: the driven");
        sb.AppendLine("  recursion reproduces it to 1e-9 after 150 000 steps at k = 1).");
        sb.AppendLine("  target      Sigma rho    min rho        excursion/rhoBar   drive/step k=1   drive/step k=95  accumulated 200 steps (k=95)");
        sb.AppendLine("  ----------- ------------ -------------- ------------------ ---------------- ---------------- -----------------------------");
        double rhoBar = 1.0 / N;
        foreach (var (name, sec) in Targets)
        {
            double dl = RequiredContrast(sec);
            var v1 = Mode(1);
            var prof = v1.Select(x => rhoBar + rhoBar * dl / 2.0 * x).ToArray();
            double d95 = RequiredDrive(dl, 95);
            sb.AppendLine($"  {name,-11} {prof.Sum(),12:F10} {prof.Min(),14:E6} {dl / 2.0,18:E6} {RequiredDrive(dl, 1),16:E6} {d95,16:E6} {200 * d95,29:E6}");
        }
        sb.AppendLine("  -> positivity and the count NEVER bind (the largest excursion is 1.7e-5 of rhoBar); the binding");
        sb.AppendLine("     constraint is the Poisson band above and the missing mode-matched driver below.");
        sb.AppendLine();

        PrintHeader(sb, "POWER SCALING");
        sb.AppendLine("  drive per unit clock shift = d*(1 - mu_k):  k=1 -> 6.4250e-04  |  k=95 -> 2.3994  |  ratio 3734.4");
        sb.AppendLine("  P ~ f * (k/N)^2  with 1 - mu_k = 2d(1 - cos(pi k/N)) ~ d(pi k/N)^2:");
        sb.AppendLine($"    LINEAR in the target: 1 ns/day -> {RequiredDrive(RequiredContrast(1e-9), 1):E3}, 1 us/day -> {RequiredDrive(RequiredContrast(1e-6), 1):E3}, 1 ms/day -> {RequiredDrive(RequiredContrast(1e-3), 1):E3}, 1 s/day -> {RequiredDrive(RequiredContrast(1.0), 1):E3}");
        sb.AppendLine($"    QUADRATIC in frequency: cost(2)/cost(1) = {(1.0 - Mu(2)) / (1.0 - Mu(1)):F2}, cost(4)/cost(2) = {(1.0 - Mu(4)) / (1.0 - Mu(2)):F2}");
        double sizeGain = (1.0 - Mu(2)) / (2.0 * Damping * (1.0 - Math.Cos(Math.PI * 2.0 / 1024.0)));
        sb.AppendLine($"    N^-2 at fixed physical wavelength: a 1024-site lattice is {sizeGain:F1} times cheaper than a 96-site one");
        sb.AppendLine("  Scalings verified for k = 1, 2, 4, 8 (|exact/approx - 1| < 2 %); at high k the continuum form overestimates.");
        sb.AppendLine();

        PrintHeader(sb, "VERDICT — PRACTICAL / ASTROPHYSICAL ONLY / REFUTED");
        sb.AppendLine("  target      required Delta ln rho   drive/step (smooth)   x band      equivalent well   verdict");
        sb.AppendLine("  ----------- ---------------------- --------------------- ----------- ----------------- --------------------");
        sb.AppendLine($"  1 ns/day    {RequiredContrast(1e-9),22:E6} {RequiredDrive(RequiredContrast(1e-9), 1),21:E6} {RequiredContrast(1e-9) / CeilingContrast,11:E2} {EquivalentVelocity(1e-9),10:F1} m/s      PRACTICAL");
        sb.AppendLine($"  1 us/day    {RequiredContrast(1e-6),22:E6} {RequiredDrive(RequiredContrast(1e-6), 1),21:E6} {RequiredContrast(1e-6) / CeilingContrast,11:E2} {EquivalentVelocity(1e-6),10:F0} m/s      PRACTICAL");
        sb.AppendLine($"  1 ms/day    {RequiredContrast(1e-3),22:E6} {RequiredDrive(RequiredContrast(1e-3), 1),21:E6} {RequiredContrast(1e-3) / CeilingContrast,11:E2} {EquivalentVelocity(1e-3),10:F0} m/s     PRACTICAL (as numbers)");
        sb.AppendLine($"  1 s/day     {RequiredContrast(1.0),22:E6} {RequiredDrive(RequiredContrast(1.0), 1),21:E6} {RequiredContrast(1.0) / CeilingContrast,11:F1} {EquivalentVelocity(1.0),10:F0} m/s     REFUTED (7.1x the band)");
        sb.AppendLine();
        sb.AppendLine("  ASTROPHYSICAL ONLY: the band's top (0.1407 s/day) and the observed galactic field (0.0464 s/day,");
        sb.AppendLine("  33 % of the band, a 220 km/s well) — sustained shifts above ~10 ms/day live at galactic/cluster scale.");
        sb.AppendLine();

        PrintHeader(sb, "CONCLUSIONS");
        sb.AppendLine("  C1  INVERSION: a sustained fractional shift f needs Delta ln rho = d*f, i.e. 3.472e-14, 3.472e-11,");
        sb.AppendLine("      3.472e-8 and 3.472e-5 for 1 ns, 1 us, 1 ms and 1 s per day. The equivalent potential wells are");
        sb.AppendLine("      32 m/s, 1.02 km/s, 32.3 km/s and 1020 km/s — the Earth's surface (60.145 us/day) is 7.91 km/s.");
        sb.AppendLine("  C2  DRIVE: holding the state costs (1 - mu_k) of the amplitude EVERY STEP — 7.44e-18, 7.44e-15,");
        sb.AppendLine("      7.44e-12 and 7.44e-9 for the smoothest mode (2.78e-14 ... 2.78e-5 for a cell-scale mode).");
        sb.AppendLine("      Nothing is self-sustaining (G_008): the drive IS the state, re-created each step.");
        sb.AppendLine("  C3  PROFILE: the steady state is a pure Neumann mode at an excursion of rhoBar*Delta ln rho/2 —");
        sb.AppendLine("      1.8e-16 to 1.8e-7 count units. Positivity and count conservation never bind; the smoothest mode");
        sb.AppendLine("      is 3734x cheaper per unit shift than a cell-scale one.");
        sb.AppendLine("  C4  SCALING: P is linear in the target shift, quadratic in the relative spatial frequency, and N^-2");
        sb.AppendLine("      at fixed physical wavelength: 6.4250e-04 per unit shift at k = 1 against 2.3994 at k = 95.");
        sb.AppendLine("  C5  VERDICTS: 1 ns/day, 1 us/day and 1 ms/day are PRACTICAL AS NUMBERS (7.10e-9, 7.10e-6 and");
        sb.AppendLine("      7.10e-3 of the accessible band, negligible excursions, and far above a 1e-18 clock floor); the");
        sb.AppendLine("      band's top (0.1407 s/day) and the observed galactic field (0.0464 s/day) are ASTROPHYSICAL ONLY;");
        sb.AppendLine("      and 1 s/day is REFUTED spontaneously (7.105x the Poisson band, a 1020 km/s well).");
        sb.AppendLine("  C6  THE SHARP STATEMENT: every target above requires an external, mode-matched, structured driver —");
        sb.AppendLine("      and the canonical chain supplies none (branching is arrangement-neutral, the attractor erases");
        sb.AppendLine("      arrangements, an undriven local state lives 0.622 steps, and the Poisson band caps spontaneous");
        sb.AppendLine("      contrasts at 4.8867e-6). So: time control is feasible as an ENGINEERING statement about");
        sb.AppendLine("      numbers and refuted as a PHYSICS statement about what the theory produces on its own.");
        sb.AppendLine("  C7  OPEN: whether any external agent can be mode-matched to the AT occupancy index at all (the same");
        sb.AppendLine("      gap G_008 OP1 identified); and whether the N^-2 scaling makes a larger effective lattice a");
        sb.AppendLine("      cheaper route to a given shift.");
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
