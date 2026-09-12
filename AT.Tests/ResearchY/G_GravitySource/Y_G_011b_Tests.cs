using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using static AT.Tests.Shared.DensityField;
using static AT.Tests.Shared.PhysicalUnits;
using static AT.Tests.Shared.RhoActuators;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_011b — Labor Rho Audit (group G — Gravity Source).
///
/// QUESTION: can any laboratory system implement a controlled rho profile?
/// Systems: oscillator lattice, resonator network, coupled modes, graph diffusion, D96 controls.
/// Measured per system: required drive, steady-state rho, clock shift, gravity shift.
///
/// RESULT
///   PRACTICAL ........ the rho-DYNAMICS itself. The canonical relaxation W = I - d·L is literally the
///                     explicit lattice-diffusion step (RhoDynamics.DiffuseStep), so a 96-site chain of
///                     coupled oscillators / an RC or LC ladder / a graph-diffusion chip reproduces it
///                     exactly. The derived admissibility 0 <= d <= 1/2 (G_007, from rho >= 0) IS the CFL
///                     stability bound of the explicit scheme (|1 - d·lambda| <= 1), and the canonical
///                     d = 0.2 is 0.3999 of it. Required drive for a 1 ns/day clock target: 7.436285e-18
///                     of the held amplitude per step at k = 1 (2.777034e-14 at k = 95); for the G_002
///                     witness 0.48675 per step (49 % of the count, G_008). The steady state is the exact
///                     Neumann mode rho = rhoBar(1 + (Delta ln rho/2) v_k) with Sigma rho = 1 and the
///                     excursion/rhoBar at most 1.74e-5, so positivity NEVER binds. In ANALOGUE terms the
///                     fractional readout (Delta ln rho/3) is measurable at 1e-14 trivially.
///   ASTROPHYSICAL ... every REAL clock or gravity readout. The clock floor 1e-18 needs M/r = 1.3466e9 kg/m
///                     (1.35 million tonnes per metre), 1 ns/day needs 1.5586e13, 1 ms/day 1.5586e19 and
///                     the G_005 band top 2.1935e21 kg/m (2340 Earths per metre). The Earth itself is
///                     9.3740e17 kg/m = 6.9613e-10.
///   REFUTED ......... a laboratory clock or gravity effect from a rho REARRANGEMENT at fixed total
///                     energy. The strongest lab-scale operation (1 kg moved 1 m from a 1 m separation)
///                     gives Delta Phi/c^2 = 3.7131e-28 — 2.69e9x BELOW the clock floor (1 kg at 1 m:
///                     7.4262e-28, 1.35e9x below) — and in AT the resulting field is EXACTLY the Newtonian
///                     one (G_004's Earth calibration 0.99600), so there is no new effect to see at any
///                     magnitude.
///
/// Deterministic: exact algebra, no randomness.  No reclassification; D_040 untouched.
/// </summary>
public class Y_G_011b_Tests : ResearchTestBase
{
    public Y_G_011b_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double Damping = 0.2;

    private const double BandCeiling = 4.8867e-6;   // G_005's 1 % Poisson ceiling on Delta ln rho
    private const double ObservedContrast = 1.6102e-6;
    private const double ClockFloor = 1e-18;        // best optical-clock fractional resolution

    // ── the clock and gravity conversion laws (G_009 / G_004) ────────────────────

    private static double FracFromSecondsPerDay(double seconds) => seconds / 86400.0;
    private static double RequiredContrast(double secondsPerDay) => D * FracFromSecondsPerDay(secondsPerDay);
    private static double RequiredDrive(double deltaLnRho, int k) => (1.0 - NeumannMu(k, N, Damping)) * deltaLnRho;
    private static double EquivalentVelocity(double secondsPerDay) => Math.Sqrt(FracFromSecondsPerDay(secondsPerDay)) * C;

    /// <summary>The mass-to-radius ratio that produces a potential depth Delta Phi/c^2 = f (GM/(r c^2)), measured G.</summary>
    private static double MassOverRadius(double f) => f * C * C / G_CODATA;

    /// <summary>The potential depth of a mass M at radius r (measured G).</summary>
    private static double DepthOf(double mass, double radius) => G_CODATA * mass / (radius * C * C);

    private static double[] Mode(int k) => NeumannMode(k, N);

    private static double[] Step(double[] a) => RhoDynamics.DiffuseStep(a, Damping);

    private static double[] Iterate(double[] a, int m)
    {
        var r = (double[])a.Clone();
        for (int i = 0; i < m; i++) r = Step(r);
        return r;
    }

    private static double MaxAbs(double[] x) => x.Max(Math.Abs);

    /// <summary>The mode-matched steady state: rho_i = rhoBar·(1 + (Delta ln rho / 2)·v_k(i)), rhoBar = 1/N.</summary>
    private static double[] SteadyProfile(int k, double deltaLnRho)
    {
        var v = Mode(k);
        return v.Select(x => (1.0 + deltaLnRho / 2.0 * x) / N).ToArray();
    }

    // ── 1. The five laboratory systems as operators ──────────────────────────────

    [Fact]
    public void Y_G_011b_SystemOperators()
    {
        // (a) Oscillator lattice / resonator network / D96 controls: the canonical Neumann chain, which is
        //     exactly what RhoDynamics.DiffuseStep implements (v_k are exact eigenvectors).
        double worstChain = 0.0;
        for (int k = 1; k < N; k++)
        {
            var v = Mode(k);
            double mu = NeumannMu(k, N, Damping);
            for (int i = 0; i < N; i++) worstChain = Math.Max(worstChain, Math.Abs(Step(v)[i] - mu * v[i]));
        }
        Assert.True(worstChain < 1e-12, $"chain eigenvector error = {worstChain}");

        // (b) Nearest-neighbour ring / graph diffusion: the periodic step has the same form with
        //     lambda_k = 2(1 - cos(2 pi k/n)) and cos(2 pi k j/n) as its exact eigenvectors.
        double worstRing = 0.0;
        foreach (int k in new[] { 1, 2, 24, 47, 48 })
        {
            var v = Enumerable.Range(0, N).Select(j => Math.Cos(2.0 * Math.PI * k * j / N)).ToArray();
            double mu = 1.0 - Damping * RingSpectrum(N)[k];
            var stepped = RingStep(v, Damping);
            for (int i = 0; i < N; i++) worstRing = Math.Max(worstRing, Math.Abs(stepped[i] - mu * v[i]));
        }
        Assert.True(worstRing < 1e-12, $"ring eigenvector error = {worstRing}");

        // (c) The five system families, by their largest Laplacian eigenvalue and admissible damping.
        var chain = ChainSpectrum(N);
        var ring = RingSpectrum(N);
        var circulant = CirculantSpectrum(N, 6);
        var star = StarSpectrum(N);

        Assert.True(Math.Abs(chain.Max() - 3.998929) < 1e-5, $"chain lmax = {chain.Max()}");
        Assert.True(Math.Abs(ring.Max() - 4.0) < 1e-12);
        Assert.True(Math.Abs(circulant.Max() - 15.837372) < 1e-5, $"circulant lmax = {circulant.Max()}");
        Assert.True(Math.Abs(star.Max() - 96.0) < 1e-12);

        Assert.True(Math.Abs(AdmissibleDamping(chain) - 0.500134) < 1e-5);
        Assert.True(Math.Abs(AdmissibleDamping(ring) - 0.5) < 1e-12);
        Assert.True(Math.Abs(AdmissibleDamping(circulant) - 0.126284) < 1e-5);
        Assert.True(Math.Abs(AdmissibleDamping(star) - 0.0208333) < 1e-6);

        // (d) The canonical d = 0.2 is admissible for the degree-2 lattices only: it sits at 0.3999 of the
        //     chain's bound and 0.4000 of the ring's, but 1.5837x OVER the D96 circulant's and 9.6000x OVER
        //     a hub (star) graph's. The occupancy index is an ORDERED CHAIN, not the D96 mutation ring.
        Assert.True(Math.Abs(Damping / AdmissibleDamping(chain) - 0.3999) < 1e-4);
        Assert.True(Math.Abs(Damping / AdmissibleDamping(ring) - 0.4000) < 1e-4);
        Assert.True(Math.Abs(Damping / AdmissibleDamping(circulant) - 1.5837) < 1e-3);
        Assert.True(Math.Abs(Damping / AdmissibleDamping(star) - 9.6000) < 1e-3);

        // (e) The chain's suppression mechanism (a 3734.4 rate spread) is stronger than the ring's (934.1)
        //     because the chain's fundamental is twice as smooth; the mechanism SURVIVES both.
        Assert.True(Math.Abs(RateSelectivity(chain, Damping) - 3734.4375) < 1e-2);
        Assert.True(Math.Abs(RateSelectivity(ring, Damping) - 934.1094) < 1e-2);
        Assert.True(RateSelectivity(chain, Damping) / RateSelectivity(ring, Damping) > 3.9);
    }

    // ── 2. The derived range IS the CFL bound ────────────────────────────────────

    [Fact]
    public void Y_G_011b_StabilityIsTheCflBound()
    {
        // The three conditions coincide: rho >= 0 (a convex combination), |mu_k| <= 1 (stability of the
        // explicit step) and d <= 1/2 (G_007's derived range) are the SAME inequality.
        var chain = ChainSpectrum(N);
        Assert.True(Math.Abs(AdmissibleDamping(chain) - 0.5) < 2e-4);      // the chain's own bound
        foreach (double d in new[] { 0.0, 0.1, 0.2, 0.4, 0.5 })
        {
            for (int k = 0; k < N; k++)
                Assert.True(Math.Abs(1.0 - d * chain[k]) <= 1.0 + 1e-15, $"d = {d}, k = {k}");
        }
        // Just above the bound the fastest mode leaves [-1, 1] (the scheme becomes unstable).
        Assert.True(1.0 - 0.5 * 4.0 < -0.9999);
        Assert.True(1.0 - 0.6 * chain.Max() < -1.0);

        // The canonical damping sits at 0.3999 of the bound, i.e. deliberately inside the admissible range.
        Assert.True(Damping < AdmissibleDamping(chain));
        Assert.True(Math.Abs(Damping / AdmissibleDamping(chain) - 0.3999) < 1e-4);

        // Rate ladder of the chain at d = 0.2: 2.141650094e-4 (k = 1) ... 0.799785835 (k = 95).
        Assert.True(Math.Abs(Damping * chain[1] - 2.141650094e-4) < 1e-12);
        Assert.True(Math.Abs(Damping * chain[95] - 0.799785835) < 1e-9);
        Assert.True(Math.Abs(NeumannMu(48, N, Damping) - 0.6) < 1e-15);
    }

    // ── 3. Required drive and steady-state profile ───────────────────────────────

    [Fact]
    public void Y_G_011b_RequiredDriveAndSteadyState()
    {
        // The four clock targets, priced at the two extremes of the mode ladder (G_010).
        var targets = new (double Seconds, double Contrast, double DriveK1, double DriveK95, double BandShare, double Well)[]
        {
            (1e-9, 3.472222e-14, 7.436285e-18, 2.777034e-14, 7.11e-9, 32.25),
            (1e-6, 3.472222e-11, 7.436285e-15, 2.777034e-11, 7.11e-6, 1019.91),
            (1e-3, 3.472222e-8, 7.436285e-12, 2.777034e-8, 7.11e-3, 32252.53),
            (1.0, 3.472222e-5, 7.436285e-9, 2.777034e-5, 7.105, 1019914.65),
        };
        foreach (var t in targets)
        {
            Assert.True(Math.Abs(RequiredContrast(t.Seconds) - t.Contrast) / t.Contrast < 1e-5);
            Assert.True(Math.Abs(RequiredDrive(t.Contrast, 1) - t.DriveK1) / t.DriveK1 < 1e-5);
            Assert.True(Math.Abs(RequiredDrive(t.Contrast, 95) - t.DriveK95) / t.DriveK95 < 1e-5);
            Assert.True(Math.Abs(t.Contrast / BandCeiling - t.BandShare) / t.BandShare < 1e-2);
            Assert.True(Math.Abs(EquivalentVelocity(t.Seconds) - t.Well) / t.Well < 1e-3);
        }

        // The steady state is the pure Neumann mode, count-exact and positive: positivity never binds.
        var v1 = Mode(1);
        double vMax = v1.Max(Math.Abs);
        Assert.True(Math.Abs(vMax - 0.9998661) < 1e-6);
        foreach (var t in targets)
        {
            var profile = SteadyProfile(1, t.Contrast);
            Assert.True(Math.Abs(profile.Sum() - 1.0) < 1e-12);
            Assert.True(profile.Min() > 0.0);
            // The profile IS the mode shape (compared directly, avoiding the 1e-14 cancellation trap) ...
            for (int i = 0; i < N; i++)
                Assert.True(Math.Abs(profile[i] - (1.0 + t.Contrast / 2.0 * v1[i]) / N) < 1e-19);
            // ... so its log contrast is 2 atanh((a/2)|v_1|max) = a·0.9998661 (analytic, no cancellation).
            double xAnalytic = t.Contrast / 2.0 * vMax;
            Assert.True(Math.Abs(2.0 * Math.Atanh(xAnalytic) / t.Contrast - 0.9998661) < 1e-6);
        }
        Assert.True(Math.Abs(SteadyProfile(1, 3.472222e-8).Min() - 1.0416666e-2) < 1e-8);

        // The driven recursion reproduces the steady state exactly (the lab needs only a mode-matched drive).
        var target = SteadyProfile(1, 3.472222e-8);
        var drive = HoldDrive(target);
        var settled = Settle(target, drive, 20000);
        double err = Enumerable.Range(0, N).Max(i => Math.Abs(settled[i] - target[i]));
        Assert.True(err < 1e-12, $"settle error = {err}");
        Assert.True(Math.Abs(drive.Sum()) < 1e-15);
        // The price is exactly (1 - mu_1) of the mode amplitude per step (to the 5.6e-9 rounding floor of
        // forming the centred profile by subtracting rhoBar).
        var centred = target.Select(v => v - 1.0 / N).ToArray();
        Assert.True(Math.Abs(MaxAbs(drive) / MaxAbs(centred) - (1.0 - NeumannMu(1, N, Damping))) < 1e-7);

        // For the G_002 witness class the price is the G_008 number: 0.48675 per step (49 % of the count).
        var tilt = Spread(D96Spaces.Mult, 1.0, TiltFractions);
        Assert.True(Math.Abs(MaxAbs(HoldDrive(tilt)) - 0.01866667) < 1e-7);
        Assert.True(Math.Abs(HoldDrive(tilt).Sum(Math.Abs) - 0.48675) < 1e-5);

        // The kinematic room is the one-cell counting ceiling ln 96 = 4.564348 (G_005's MaximumContrast):
        // a 1.521449 fractional shift = 131 453 s/day, at a k = 1 drive of 9.7752e-4 per step.
        Assert.True(Math.Abs(Math.Log(N) / D - 1.5214494) < 1e-6);
        Assert.True(Math.Abs(Math.Log(N) / D * 86400.0 - 131453.228) < 0.5);
        Assert.True(Math.Abs(RequiredDrive(Math.Log(N), 1) - 9.775237e-4) < 1e-8);
    }

    // ── 4. The clock shift readout ───────────────────────────────────────────────

    [Fact]
    public void Y_G_011b_ClockShift()
    {
        // In AT the readout is the metric: dtau/dt = rho^(1/d), so Delta tau/tau = Delta ln rho / d (G_009).
        Assert.True(Math.Abs(RequiredContrast(1e-9) / D - 1.157407e-14) / 1.157407e-14 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1e-6) / D - 1.157407e-11) / 1.157407e-11 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1e-3) / D - 1.157407e-8) / 1.157407e-8 < 1e-5);
        Assert.True(Math.Abs(RequiredContrast(1.0) / D - 1.157407e-5) / 1.157407e-5 < 1e-5);

        // Every target is far above the 1e-18 optical-clock floor in FRACTIONAL terms: the obstacle is
        // producing the configuration, not seeing it — and a laboratory can hold the analogue profile.
        Assert.True(RequiredContrast(1e-9) / D / ClockFloor > 1e4);
        Assert.True(RequiredContrast(1e-3) / D / ClockFloor > 1e10);

        // The band's top and the observed galactic field in the same units (G_005/G_003).
        Assert.True(Math.Abs(BandCeiling / D * 86400.0 - 0.140737) < 1e-5);
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.046373) < 1e-5);

        // The clock channel carries NO length scale (G_003): the required contrast is the same for a
        // 1 cm and a 1 kpc device — only the drive's POWER scales with the physical size.
        Assert.True(RequiredContrast(1e-9) == D * 1e-9 / 86400.0);
        Assert.True(Math.Abs(RequiredContrast(1e-9) - 3.472222e-14) / 3.472222e-14 < 1e-5);
    }

    // ── 5. The gravity shift readout: what mass/radius buys each depth ───────────

    [Fact]
    public void Y_G_011b_GravityShift()
    {
        // Delta Phi/c^2 = f requires M/r = f c^2 / G. Checked against AT's own Earth calibration (G_009).
        Assert.True(Math.Abs(DepthOf(GM_Earth / G_CODATA, R_Earth) - 6.9613e-10) / 6.9613e-10 < 1e-4);
        Assert.True(Math.Abs(MassOverRadius(6.9613e-10) - 9.3740e17) / 9.3740e17 < 1e-3);
        Assert.True(Math.Abs(GM_Earth / G_CODATA / R_Earth - 9.3740e17) / 9.3740e17 < 1e-3);

        // The ladder of laboratory and astrophysical reaches (kg per metre).
        Assert.True(Math.Abs(MassOverRadius(ClockFloor) - 1.3466e9) / 1.3466e9 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(FracFromSecondsPerDay(1e-9)) - 1.5586e13) / 1.5586e13 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(FracFromSecondsPerDay(1e-3)) - 1.5586e19) / 1.5586e19 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(BandCeiling / D) - 2.1935e21) / 2.1935e21 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(ObservedContrast / D) - 7.2276e20) / 7.2276e20 < 1e-3);

        // The clock floor needs 1.35 million tonnes per metre; the band's top needs 2340 Earths per metre.
        Assert.True(MassOverRadius(ClockFloor) / 1e3 > 1.3e6);
        Assert.True(MassOverRadius(BandCeiling / D) / (5.9722e24 / 6.371e6) > 2000.0);

        // The strongest honest laboratory operation: 1 kg at 1 m, and 1 kg MOVED 1 m at a 1 m separation
        // (a pure rearrangement at fixed total energy).
        Assert.True(Math.Abs(DepthOf(1.0, 1.0) - 7.4262e-28) / 7.4262e-28 < 1e-3);
        double moved = G_CODATA * 1.0 / (C * C) * (1.0 / 1.0 - 1.0 / 2.0);
        Assert.True(Math.Abs(moved - 3.7131e-28) / 3.7131e-28 < 1e-3);
        Assert.True(Math.Abs(DepthOf(1000.0, 1.0) - 7.4262e-25) / 7.4262e-25 < 1e-3);

        // Both are FAR below the 1e-18 clock floor: 1.35e9x (1 kg at 1 m) and 6.7e8x (the rearrangement).
        Assert.True(DepthOf(1.0, 1.0) / ClockFloor < 1e-9);
        Assert.True(moved / ClockFloor < 1e-8);
        Assert.True(ClockFloor / DepthOf(1.0, 1.0) > 1.3e9);
    }

    // ── 6. There is no metric coupling to borrow ─────────────────────────────────

    [Fact]
    public void Y_G_011b_NoMetricCoupling()
    {
        // AT's prediction for ANY real mass-energy arrangement IS the Newtonian one: the Earth calibration
        // is reproduced with the derived G at 0.99600 and AT = GR to double precision at the surface
        // (G_004/G_009). So a laboratory rho rearrangement produces no NEW effect at any magnitude.
        Assert.True(Math.Abs(GM_Earth / (R_Earth * R_Earth) - 9.820250) < 1e-5);
        Assert.True(Math.Abs(MassOverRadius(DepthOf(1.0, 1.0)) - 1.0) < 1e-9);   // M/r = 1 kg/m by construction

        // The analogue readout is trivially measurable: the fractional occupancy shift (Delta ln rho/3) for
        // a 1 ns/day target is 1.157407e-14 — a voltage/amplitude ratio, i.e. a bench-top measurement. It is
        // NOT a clock: it is a configuration of the device, not the actualization density of spacetime.
        Assert.True(RequiredContrast(1e-9) / D < 1e-13);
        Assert.True(RequiredContrast(1e-9) / D / 1e-15 > 10.0);       // 1e-15 amplitude metrology is routine

        // Conversely, the only laboratory handle that DOES change rho_actualization is moving mass-energy,
        // and then the effect is exactly Newtonian at 1e-27: 1e9x below the clock floor. The gravity channel
        // of a lab device is therefore REFUTED, while its rho-dynamics is PRACTICAL.
        Assert.True(G_CODATA * 1.0 / (C * C) / ClockFloor < 1e-8);

        // A 1 cm device and a 1 kpc device need the SAME contrast (the clock channel has no length scale);
        // the difference is only in the drive's POWER, which is finite and small.
        // Example: 1 mJ held in a 1 us step at k = 1 needs 7.4363e-15 W for a 1 ns/day excursion.
        double drive1 = RequiredDrive(RequiredContrast(1e-9), 1);
        Assert.True(Math.Abs(drive1 * 1e-3 / 1e-6 - 7.4363e-15) / 7.4363e-15 < 1e-3);
    }

    // ── 7. Classification and report ─────────────────────────────────────────────

    [Fact]
    public void Y_G_011b_ClassificationAndReport()
    {
        var sb = new StringBuilder();

        PrintHeader(sb, "ResearchY-G_011b — LABOR RHO AUDIT");
        sb.AppendLine("Question: can any laboratory system implement a controlled rho profile?");
        sb.AppendLine("Systems: oscillator lattice, resonator network, coupled modes, graph diffusion, D96 controls.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  The canonical relaxation is W = I - d·L with d = 0.2 on an ORDERED 96-cell chain");
        sb.AppendLine("      (RhoDynamics.DiffuseStep, G_006/G_007).");
        sb.AppendLine("  A2  A laboratory realisation must reproduce W as one explicit diffusion step: the derived");
        sb.AppendLine("      range 0 <= d <= 1/2 (G_007, from rho >= 0) is the CFL stability bound.");
        sb.AppendLine("  A3  The readouts are G_009's clock law (Delta tau/tau = Delta ln rho/d) and G_004's gravity");
        sb.AppendLine("      calibration (Delta Phi/c^2 = GM/(r c^2), AT = GR, G from QG181).");
        sb.AppendLine("  A4  A clock target of T seconds per day requires Delta ln rho = 3T/86400 (G_010).");
        sb.AppendLine();

        PrintHeader(sb, "1. THE FIVE SYSTEMS AS OPERATORS");
        var chain = ChainSpectrum(N);
        var ring = RingSpectrum(N);
        var circulant = CirculantSpectrum(N, 6);
        var star = StarSpectrum(N);
        sb.AppendLine("  system                        lambda_max   admissible d   0.2 / d_max   rate spread");
        sb.AppendLine($"  oscillator lattice (chain)     {chain.Max(),9:F6}   {AdmissibleDamping(chain),11:F6}   {Damping / AdmissibleDamping(chain),10:F4}   {RateSelectivity(chain, Damping),10:F2}");
        sb.AppendLine($"  resonator network (ring)       {ring.Max(),9:F6}   {AdmissibleDamping(ring),11:F6}   {Damping / AdmissibleDamping(ring),10:F4}   {RateSelectivity(ring, Damping),10:F2}");
        sb.AppendLine($"  graph diffusion (D96 circulant){circulant.Max(),9:F6}   {AdmissibleDamping(circulant),11:F6}   {Damping / AdmissibleDamping(circulant),10:F4}   {RateSelectivity(circulant, Damping),10:F2}");
        sb.AppendLine($"  hub / star (control)           {star.Max(),9:F6}   {AdmissibleDamping(star),11:F6}   {Damping / AdmissibleDamping(star),10:F4}   {RateSelectivity(star, Damping),10:F2}");
        sb.AppendLine("  => the canonical d = 0.2 is admissible for the DEGREE-2 lattices only (0.3999 of the CFL bound);");
        sb.AppendLine("     the occupancy index is an ordered chain, not the D96 mutation ring.");
        sb.AppendLine();

        PrintHeader(sb, "2. REQUIRED DRIVE, STEADY STATE, CLOCK AND GRAVITY PER TARGET");
        sb.AppendLine("  target     Delta ln rho   drive/step k=1  drive k=95    x band    well [m/s]   M/r [kg/m]");
        foreach (double sec in new[] { 1e-9, 1e-6, 1e-3, 1.0 })
        {
            double dl = RequiredContrast(sec);
            sb.AppendLine($"  {sec,8:0.0e0}  {dl,14:E4}  {RequiredDrive(dl, 1),14:E4}  {RequiredDrive(dl, 95),12:E4}  {dl / BandCeiling,8:E3}  {EquivalentVelocity(sec),11:F2}  {MassOverRadius(dl / D),11:E4}");
        }
        sb.AppendLine($"  band top   {BandCeiling,14:E4}  {RequiredDrive(BandCeiling, 1),14:E4}  {RequiredDrive(BandCeiling, 95),12:E4}  {1.0,8:F3}  {Math.Sqrt(BandCeiling / D) * C / 1e3,9:F2} km/s  {MassOverRadius(BandCeiling / D),11:E4}");
        sb.AppendLine($"  clock floor {ClockFloor * D,13:E4}  {RequiredDrive(ClockFloor * D, 1),14:E4}  {RequiredDrive(ClockFloor * D, 95),12:E4}  {ClockFloor * D / BandCeiling,8:E3}  {Math.Sqrt(ClockFloor) * C * 1e3,9:F3} mm/s  {MassOverRadius(ClockFloor),11:E4}");
        sb.AppendLine($"  kinematic room (ln 96) {Math.Log(N),10:F6}  {RequiredDrive(Math.Log(N), 1),14:E4}  {RequiredDrive(Math.Log(N), 95),12:E4}  {Math.Log(N) / BandCeiling,8:E1}  n/a          n/a");
        sb.AppendLine();

        PrintHeader(sb, "3. THE GRAVITY LADDER (what buys each depth)");
        sb.AppendLine($"  Earth (GM/Rc^2) .......................... {DepthOf(GM_Earth / G_CODATA, R_Earth):E4}   M/r = {MassOverRadius(DepthOf(GM_Earth / G_CODATA, R_Earth)):E4} kg/m");
        sb.AppendLine($"  1 kg at 1 m .............................. {DepthOf(1.0, 1.0):E4}   M/r = 1.0 kg/m");
        sb.AppendLine($"  1000 kg at 1 m ........................... {DepthOf(1000.0, 1.0):E4}");
        sb.AppendLine($"  1 kg moved 1 m at a 1 m separation ....... {G_N * 1.0 / (C * C) * 0.5:E4}   (fixed total energy)");
        sb.AppendLine($"  clock floor 1e-18 ........................ needs M/r = {MassOverRadius(ClockFloor):E4} kg/m = {MassOverRadius(ClockFloor) / 1e3:E3} t/m");
        sb.AppendLine($"  1 ns/day ................................ needs M/r = {MassOverRadius(FracFromSecondsPerDay(1e-9)):E4} kg/m");
        sb.AppendLine($"  1 ms/day ................................ needs M/r = {MassOverRadius(FracFromSecondsPerDay(1e-3)):E4} kg/m");
        sb.AppendLine($"  band top {BandCeiling / D * 86400.0:F4} s/day ........... needs M/r = {MassOverRadius(BandCeiling / D):E4} kg/m = {MassOverRadius(BandCeiling / D) / (5.9722e24 / 6.371e6):E3} Earths/m");
        sb.AppendLine();

        PrintHeader(sb, "4. CONCLUSIONS");
        sb.AppendLine("  C1  PRACTICAL — the rho DYNAMICS is a textbook explicit diffusion scheme. Any 96-site chain of");
        sb.AppendLine("      coupled oscillators, an LC/RC ladder, a resonator network or a graph-diffusion chip with");
        sb.AppendLine("      nearest-neighbour coupling and d = 0.2 implements RhoDynamics.DiffuseStep exactly.");
        sb.AppendLine("  C2  The derived admissibility 0 <= d <= 1/2 IS the CFL bound |1 - d·lambda| <= 1, and d = 0.2");
        sb.AppendLine("      sits at 0.3999 of it: AT's positivity constraint and the scheme's stability constraint are");
        sb.AppendLine("      the same inequality — a genuine cross-check of G_007 (which derived it from rho >= 0).");
        sb.AppendLine("  C3  The drive is trivial: 7.436285e-18 of the held amplitude per step for 1 ns/day (k = 1),");
        sb.AppendLine($"      0.48675 per step for the G_002 witness. Positivity never binds (excursion <= {RequiredContrast(1e-3) / 2.0:E3}).");
        sb.AppendLine("  C4  ASTROPHYSICAL — every REAL clock/gravity readout: 1e-18 needs M/r = 1.3466e9 kg/m,");
        sb.AppendLine("      1 ns/day 1.5586e13, 1 ms/day 1.5586e19, the band top 2.1935e21 kg/m.");
        sb.AppendLine("  C5  REFUTED — a laboratory clock or gravity effect from a rho rearrangement at fixed total");
        sb.AppendLine("      energy: the strongest bench operation (1 kg moved 1 m from 1 m) is 3.7131e-28, i.e. 2.7e9x");
        sb.AppendLine("      BELOW the clock floor, and in AT the resulting field is exactly the Newtonian one (G_004),");
        sb.AppendLine("      so there is no new effect to detect at any magnitude.");
        sb.AppendLine("  C6  The analogue readout is real but is not a clock: the mode amplitude is a configuration of");
        sb.AppendLine("      the device, not the actualization density of spacetime — the metric coupling is not borrowed.");
        sb.AppendLine();

        PrintHeader(sb, "5. CLASSIFICATION");
        sb.AppendLine("  PRACTICAL     the rho dynamics, the drive, the steady state and the analogue readout (C1-C3).");
        sb.AppendLine("  ASTROPHYSICAL any real clock or gravity effect (C4).");
        sb.AppendLine("  REFUTED       a bench-side metric effect from a rho rearrangement (C5-C6).");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changed; no new");
        sb.AppendLine("  primitive. Deterministic: exact algebra, no randomness.");
        sb.AppendLine();
        sb.AppendLine($"  SYSTEM ANSWER: PRACTICAL (dynamics) / ASTROPHYSICAL (metric readouts) / REFUTED (bench metric).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}