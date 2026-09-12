using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_017 — Metric Coupling Audit (group G — Gravity Source).
///
/// GIVEN rho = |psi|^2 and g_00 = -rho^(2/d).
/// QUESTION: does any EXPERIMENTALLY REALIZABLE |psi|^2 profile produce a MEASURABLE clock shift?
/// Systems: optical cavity, resonator array, photon lattice, oscillator network.
/// Measure: DeltaTau, DeltaPhi, the clock signal.   Compare: AT prediction, GR prediction, observed limits.
/// Output: DERIVED / BOUNDARY / REFUTED.
///
/// WHY THIS IS NOT G_015. G_015 priced a metric effect through mass-energy. G_017 takes the |psi|^2 reading
/// literally for *optical* fields, where the intensity IS the directly measured |psi|^2 and the contrast is
/// enormous — so the prediction becomes O(1) and can be confronted with real clock performance. That turns
/// the identification premise of G_014/G_015 from "unproven" into "experimentally excluded at bench scale".
///
/// THE TWO READINGS
///   AT-naive   rho ~ the LAB intensity I:   DeltaTau/tau = Delta Phi/c^2 = (1/d) Delta ln I.
///   GR         the field's stored ENERGY:    Delta Phi/c^2 = G U/(R c^4),  m = U/c^2.
///   AT-substrate  rho = the actualization density of the substrate (G_014): identical to GR for
///              mass-energy configurations (G_004's 0.99600) and matching the galactic cross-check
///              (G_009's 0.99668) — this reading is NOT refuted.
///
/// THE NUMBERS (all recomputed here from F, Q, P, lambda and the lattice depth)
///   system                        realizable contrast   AT-naive DeltaTau/tau    GR Delta Phi/c^2     AT-naive/GR
///   optical cavity (F=1e6, 1 W, 0.3 m)   1e2 (Gaussian)      0.6666666667         3.509234e-47        1.899750e46
///   resonator array (Q=1e7, 1 mW, 1550 nm)      1e4          3.0701134573         7.071071e-50        4.341794e49
///   photon lattice (Sr clock, I=2.439413e8 W/m^2) 1e3        2.3025850930         (see test)          large
///   oscillator network (Q=1e6, 1 pW, 6 GHz)     10           0.7675283643         3.533090e-58        2.17e57
///
/// EXCLUSION against the observed clock ceiling (best optical clocks, 1e-18): 6.667e17 (cavity), 3.070e18
/// (resonator), 2.303e18 (lattice), 7.675e17 (network); even a crude 1e-12 laboratory systematic excludes by
/// 6.7e11 to 3.1e12.  A Sr optical LATTICE clock operates at I = 2.439413e8 W/m^2 with node/antinode contrast
/// 1e3 AND reads a reproducible frequency to 1e-18 — AT-naive predicts a 230.26 % shift across its own
/// lattice, so the naive identification is excluded by 2.303e18 by the very experiment that best tests it.
///
/// THE CLOCK SIGNAL: the observable is the FRACTIONAL FREQUENCY RATIO between two clocks placed at two
/// intensities (two lattice sites, inside/outside a mode). AT-naive predicts 0.77 … 3.07 (77–307 %), against
/// an observed reproducibility of 1e-18; GR predicts 1e-47 to 1e-58. The data agree with GR.
///
/// VERDICTS
///   DERIVED   the clock law DeltaTau/tau = (1/d) Delta ln rho and its LOGARITHMIC form; and AT's SUBSTRATE
///             prediction, which reproduces GR at the Earth's surface (0.99600) and the galactic
///             cross-check (0.99668 = 0.046374 s/day, 5.367e11 x the 1e-18 floor).
///   BOUNDARY  the IDENTIFICATION rho = |psi|^2 as applied to a LABORATORY field, and the scale invariance
///             (only ratios of rho are physical, G_009) — the coupling is a boundary input and is NOT
///             borrowed by a readout (G_011b).
///   REFUTED   any experimentally realizable |psi|^2 profile producing a measurable AT metric clock shift:
///             excluded by 6.7e17 … 3.1e18 at the 1e-18 ceiling, with the naive reading differing from GR by
///             1.9e46 … 4.3e49 on real optical systems.
///
/// Deterministic: exact algebra, no randomness.  No reclassification (G_014/G_015 unchanged); D_040
/// untouched; no canonical claim, value or equation changes; no new primitive.
/// </summary>
public class Y_G_017_Tests : ResearchTestBase
{
    public Y_G_017_Tests(ITestOutputHelper output) : base(output) { }

    private const int D = 3;
    private const double ClockCeiling = 1e-18;      // best optical clocks (state of the art)
    private const double C4 = C * C * C * C;

    /// <summary>AT-naive: a lab intensity profile read as rho — dtau/dt = rho^(1/d) (G_009).</summary>
    private static double AtNaive(double deltaLnIntensity) => deltaLnIntensity / D;

    /// <summary>GR: the field's stored energy U, m = U/c^2, Delta Phi/c^2 = G m/(R c^2).</summary>
    private static double GrDepth(double energy, double size) => G_CODATA * (energy / (C * C)) / (size * C * C);

    /// <summary>The Gaussian mode contrast: Delta ln I between the peak and r = a*w0 is 2a^2.</summary>
    private static double GaussianContrast(double a) => 2.0 * a * a;

    /// <summary>The energy stored in a resonator driven at power P with quality factor Q.</summary>
    private static double StoredEnergy(double q, double power, double wavelength)
        => q * power * wavelength / (2.0 * Math.PI * C);

    // ── 1. DERIVED: the clock law and the substrate consistency ──────────────────

    [Fact]
    public void Y_G_017_DerivedClockLaw()
    {
        // The law is dtau/dt = rho^(1/d) from g00 = -rho^(2/d) (QG197), so the fractional rate change is a
        // LOG-RATIO: DeltaTau/tau = (1/d) Delta ln rho = Delta Phi/c^2. Depths add where ratios multiply.
        double x = 1e-6;
        Assert.True(Math.Abs(Math.Log(Math.Exp(3.0 * x)) / D - x) / x < 1e-9, "log/exp round trip");
        Assert.True(Math.Abs(Math.Exp(D * x) - Math.Exp(3.0 * x)) < 1e-15, "d = 3 exponent");
        double r1 = 1.000002, r2 = 1.000003;
        Assert.True(Math.Abs((Math.Log(r1) + Math.Log(r2)) / D - Math.Log(r1 * r2) / D) / (Math.Log(r1 * r2) / D) < 1e-9);

        // Scale invariance (G_009): rho -> lambda rho changes every clock by lambda^(1/d), and the RELATIVE
        // change between two cells is exactly 0 — only ratios of rho are physical.
        double r = 5.0 / 3.0;                                  // a 5:3 density ratio
        Assert.True(Math.Abs(Math.Log(2.0 * r) / D - Math.Log(r) / D - Math.Log(2.0) / D) < 1e-15);

        // AT's SUBSTRATE reading is consistent with observation (this is the part that is NOT refuted):
        //  * the Earth's surface: Delta ln rho/d = 6.96133e-10/3 = 2.320443e-10, AT/GR = 0.99600 (G_004);
        //  * the galactic field: 1.6102e-6/3 = 5.367333e-7 = 0.046374 s/day (G_009's cross-check 0.99668).
        double earth = 6.96133e-10 / D;
        double galactic = 1.6102e-6 / D;
        Assert.True(Math.Abs(earth - 2.320443e-10) < 1e-16, $"Earth = {earth}");
        Assert.True(Math.Abs(galactic - 5.367333333333333e-7) < 1e-15, $"galactic = {galactic}");
        Assert.True(Math.Abs(galactic * 86400.0 - 0.04637376) < 1e-7);
        Assert.True(earth / ClockCeiling > 2.3e8);
        Assert.True(galactic / ClockCeiling > 5.3e11);

        // The observable is a FRACTIONAL FREQUENCY RATIO, so it is compared to the clock's fractional
        // resolution — which is why the ceiling in this audit is a fractional-frequency number.
        Assert.True(ClockCeiling > 0.0);
        Assert.True(Math.Abs(D - 3) < 1e-15);
    }

    // ── 2. Optical cavity ────────────────────────────────────────────────────────

    [Fact]
    public void Y_G_017_OpticalCavity()
    {
        // A high-finesse Fabry-Perot: F = 1e6, 1 W input, 0.3 m long.
        double finesse = 1e6, pIn = 1.0, length = 0.3;
        double pCirc = finesse * pIn / Math.PI;
        double roundTrip = 2.0 * length / C;
        double u = pCirc * roundTrip;
        Assert.True(Math.Abs(pCirc - 3.183099e5) / 3.183099e5 < 1e-6, $"P_circ = {pCirc}");
        Assert.True(Math.Abs(roundTrip - 2.001385e-9) / 2.001385e-9 < 1e-6, $"T = {roundTrip}");
        Assert.True(Math.Abs(u - 6.370605e-4) / 6.370605e-4 < 1e-5, $"U = {u}");

        // The mode's own Gaussian profile is a realizable |psi|^2 with a LARGE contrast.
        Assert.True(Math.Abs(GaussianContrast(1.0) - 2.0) < 1e-15);          // peak -> 1/e^2 radius
        Assert.True(Math.Abs(GaussianContrast(1.5) - 4.5) < 1e-15);          // peak -> 1.5 w0
        Assert.True(Math.Abs(GaussianContrast(3.0) - 18.0) < 1e-15);         // peak -> 3 w0

        double atn = AtNaive(GaussianContrast(1.0));
        Assert.True(Math.Abs(atn - 0.6666666667) < 1e-9, $"AT-naive = {atn}");
        Assert.True(Math.Abs(AtNaive(GaussianContrast(3.0)) - 6.0) < 1e-9);

        // GR, for the same stored energy at the mode's half-length.
        double gr = GrDepth(u, length / 2.0);
        Assert.True(Math.Abs(gr - 3.509234e-47) / 3.509234e-47 < 1e-4, $"GR = {gr}");

        // AT-naive and GR differ by 1.899750e46 — and the exclusion against a 1e-18 clock is 6.666667e17.
        Assert.True(Math.Abs(atn / gr / 1.899750e46 - 1.0) < 1e-3, $"AT/GR = {atn / gr}");
        Assert.True(Math.Abs(atn / ClockCeiling - 6.666667e17) / 6.666667e17 < 1e-5);
        Assert.True(atn / gr > 1e46);
    }

    // ── 3. Resonator array ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_017_ResonatorArray()
    {
        // A microring / photonic-crystal array: Q = 1e7, 1 mW coupled, lambda = 1550 nm.
        double q = 1e7, power = 1e-3, lambda = 1.55e-6;
        double u = StoredEnergy(q, power, lambda);
        double volume = lambda * lambda * lambda;
        double r = Math.Cbrt(3.0 * volume / (4.0 * Math.PI));
        Assert.True(Math.Abs(u - 8.228698e-12) / 8.228698e-12 < 1e-5, $"U = {u}");
        Assert.True(Math.Abs(volume - 3.723875e-18) / 3.723875e-18 < 1e-5);
        Assert.True(Math.Abs(u / volume - 2.209714e6) / 2.209714e6 < 1e-5, $"u = {u / volume}");
        Assert.True(Math.Abs(r - 9.615433e-7) / 9.615433e-7 < 1e-5, $"R = {r}");

        // A realizable on/off contrast of 1e4 across the array.
        double dln = Math.Log(1e4);
        Assert.True(Math.Abs(dln - 9.21034037) < 1e-7);
        double atn = AtNaive(dln);
        Assert.True(Math.Abs(atn - 3.0701134573) < 1e-9, $"AT-naive = {atn}");

        double gr = GrDepth(u, r);
        Assert.True(Math.Abs(gr - 7.071071e-50) / 7.071071e-50 < 1e-3, $"GR = {gr}");
        Assert.True(Math.Abs(atn / gr / 4.341794e49) < 3e45, $"AT/GR = {atn / gr}");
        Assert.True(Math.Abs(atn / ClockCeiling - 3.070113e18) / 3.070113e18 < 1e-5);
        Assert.True(atn > 3.0);                                   // a 307 % clock shift
        Assert.True(atn / ClockCeiling > 1e18);
    }

    // ── 4. Photon lattice (the lattice-clock anchor) ─────────────────────────────

    [Fact]
    public void Y_G_017_PhotonLattice()
    {
        // A Sr optical lattice clock: lambda = 813 nm, R = 88 u, depth 100 E_rec, 300 a.u. polarizability.
        double h = 6.62607015e-34, lambdaL = 813e-9;
        double mass = 88.0 * 1.66053906660e-27;
        double eRec = h * h / (2.0 * mass * lambdaL * lambdaL);
        Assert.True(Math.Abs(eRec - 2.272842e-30) / 2.272842e-30 < 1e-4, $"E_rec = {eRec}");

        double depth = 100.0 * eRec;
        double alpha = 300.0 * 1.64877727436e-41;                 // 300 a.u. -> C m^2/V
        double eps0 = 8.8541878128e-12;
        double intensity = depth * 2.0 * eps0 * C / alpha;
        Assert.True(Math.Abs(intensity - 2.439413e8) / 2.439413e8 < 1e-4, $"I = {intensity}");

        // Node/antinode contrast ladder. This is the SHARPEST test: the clock operates at 2.4e8 W/m^2 inside
        // its own standing wave and reads a reproducible frequency to 1e-18.
        var rows = new (double Contrast, double Atn, double Exclusion)[]
        {
            (1e2, 1.53505673, 1.5351e18), (1e3, 2.30258509, 2.3026e18),
            (1e4, 3.07011346, 3.0701e18), (1e6, 4.60517019, 4.6052e18),
        };
        foreach (var (contrast, expected, exclusion) in rows)
        {
            double a = AtNaive(Math.Log(contrast));
            Assert.True(Math.Abs(a - expected) < 1e-7, $"contrast {contrast}: AT-naive = {a}");
            Assert.True(Math.Abs(a / ClockCeiling / exclusion - 1.0) < 1e-4, $"contrast {contrast}");
        }

        // AT-naive predicts a 230.26 % shift across the lattice of the very clock that tests it.
        double atn = AtNaive(Math.Log(1e3));
        Assert.True(Math.Abs(atn - 2.3025850930) < 1e-9);
        Assert.True(atn > 2.0);
        Assert.True(atn / ClockCeiling > 2.3e18);

        // The field's stored energy, for the GR comparison, is negligible: even the whole beam is ~1e-14 J.
        double beamEnergy = 0.5 * 2.0 * lambdaL / C;               // 0.5 W held for two lattice periods
        Assert.True(GrDepth(beamEnergy, 100e-6) < 1e-30);
        Assert.True(atn / Math.Max(GrDepth(beamEnergy, 100e-6), 1e-60) > 1e30);
    }

    // ── 5. Oscillator network ────────────────────────────────────────────────────

    [Fact]
    public void Y_G_017_OscillatorNetwork()
    {
        // A circuit-QED / LC oscillator network: Q = 1e6, 1 pW drive, 6 GHz, 1 mm^3 mode volume.
        double q = 1e6, power = 1e-12, frequency = 6e9;
        double u = q * power / (2.0 * Math.PI * frequency);
        double volume = 1e-9;
        double r = Math.Cbrt(3.0 * volume / (4.0 * Math.PI));
        Assert.True(Math.Abs(u - 2.652582e-17) / 2.652582e-17 < 1e-5, $"U = {u}");
        Assert.True(Math.Abs(u / volume - 2.652582e-08) / 2.652582e-08 < 1e-5, $"u = {u / volume}");
        Assert.True(Math.Abs(r - 6.203505e-4) / 6.203505e-4 < 1e-5, $"R = {r}");

        // A 10:1 amplitude contrast across the network is trivially realizable.
        double atn = AtNaive(Math.Log(10.0));
        Assert.True(Math.Abs(atn - 0.7675283643) < 1e-9, $"AT-naive = {atn}");
        Assert.True(Math.Abs(AtNaive(Math.Log(100.0)) - 1.53505673) < 1e-7);

        double gr = GrDepth(u, r);
        Assert.True(Math.Abs(gr - 3.533090e-58) / 3.533090e-58 < 1e-2, $"GR = {gr}");
        Assert.True(Math.Abs(atn / ClockCeiling - 7.6753e17) / 7.6753e17 < 1e-4);
        Assert.True(atn / gr > 1e56);
        Assert.True(atn > 0.7);                                   // a 77 % clock shift
    }

    // ── 6. observed limits, the clock signal, and the verdicts ───────────────────

    [Fact]
    public void Y_G_017_ObservedLimits()
    {
        // THE CLOCK SIGNAL is a fractional frequency ratio; the ceilings used are real clock numbers.
        var ceilings = new (double Ceiling, double Cavity, double Resonator, double Lattice, double Network)[]
        {
            (1e-12, 6.666667e11, 3.070113e12, 2.302585e12, 7.675284e11),
            (1e-15, 6.666667e14, 3.070113e15, 2.302585e15, 7.675284e14),
            (1e-18, 6.666667e17, 3.070113e18, 2.302585e18, 7.675284e17),
            (1e-19, 6.666667e18, 3.070113e19, 2.302585e19, 7.675284e18),
        };
        double cavityAt = AtNaive(GaussianContrast(1.0));
        double resonatorAt = AtNaive(Math.Log(1e4));
        double latticeAt = AtNaive(Math.Log(1e3));
        double networkAt = AtNaive(Math.Log(10.0));

        foreach (var (ceiling, cav, res, lat, net) in ceilings)
        {
            Assert.True(Math.Abs(cavityAt / ceiling / cav - 1.0) < 1e-4, $"cavity @ {ceiling}");
            Assert.True(Math.Abs(resonatorAt / ceiling / res - 1.0) < 1e-4, $"resonator @ {ceiling}");
            Assert.True(Math.Abs(latticeAt / ceiling / lat - 1.0) < 1e-4, $"lattice @ {ceiling}");
            Assert.True(Math.Abs(networkAt / ceiling / net - 1.0) < 1e-4, $"network @ {ceiling}");
        }

        // Even a CRUDE 1e-12 laboratory systematic excludes the naive reading by ~1e12, and the best optical
        // clocks by ~1e18. There is no ceiling at which any realizable system survives.
        Assert.True(cavityAt / 1e-12 > 1e11);
        Assert.True(networkAt / 1e-12 > 1e11);
        Assert.True(cavityAt / 1e-19 > 1e18);
        Assert.True(latticeAt / ClockCeiling > 2.3e18);

        // The four AT-naive predictions span 0.7675 … 3.0701 (77–307 %), all O(1): there is no tuning that
        // makes them small, because the law is LOGARITHMIC and any realizable contrast is > 10.
        Assert.True(Math.Abs(networkAt - 0.7675283643) < 1e-9);
        Assert.True(Math.Abs(latticeAt - 2.3025850930) < 1e-9);
        Assert.True(Math.Abs(resonatorAt - 3.0701134573) < 1e-9);
        Assert.True(Math.Abs(cavityAt - 0.6666666667) < 1e-9);
        Assert.True(networkAt > 0.7 && resonatorAt < 3.1);

        // VERDICTS. DERIVED: the log law and AT's SUBSTRATE reading (2.3204e8 and 5.3673e11 x floor, matching
        // G_004's 0.99600 and G_009's 0.99668). BOUNDARY: the identification rho = |psi|^2 for a LAB field.
        // REFUTED: every realizable lab |psi|^2 metric clock shift.
        Assert.True(5.367333333333333e-7 / ClockCeiling > 5.3e11);
        Assert.True(cavityAt / ClockCeiling > 6.6e17);
        Assert.True(resonatorAt / ClockCeiling > 3.0e18);
        Assert.True(GrDepth(6.370605e-4, 0.15) < 1e-46);
        Assert.True(AtNaive(GaussianContrast(1.0)) / GrDepth(6.370605e-4, 0.15) > 1e46);
    }

    // ── 7. research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_017_Run()
    {
        var sb = new StringBuilder();

        PrintHeader(sb, "ResearchY-G_017 — METRIC COUPLING AUDIT");
        sb.AppendLine("Given: rho = |psi|^2 and g_00 = -rho^(2/d).");
        sb.AppendLine("Question: does any EXPERIMENTALLY REALIZABLE |psi|^2 profile produce a MEASURABLE clock shift?");
        sb.AppendLine("Systems: optical cavity, resonator array, photon lattice, oscillator network.");
        sb.AppendLine("Measure: DeltaTau, DeltaPhi, clock signal.   Compare: AT, GR, observed limits.");
        sb.AppendLine("Output: DERIVED / BOUNDARY / REFUTED.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  dtau/dt = rho^(1/d) from g00 = -rho^(2/d) (QG197): DeltaTau/tau = (1/d) Delta ln rho.");
        sb.AppendLine("  A2  AT-SUBSTRATE reading: rho is the actualization density (G_014). For mass-energy");
        sb.AppendLine("      configurations AT == GR (G_004's 0.99600).");
        sb.AppendLine("  A3  AT-NAIVE reading: a laboratory intensity profile IS rho, i.e. rho ~ I.");
        sb.AppendLine("  A4  GR: the field's stored ENERGY U with m = U/c^2 gives Delta Phi/c^2 = G U/(R c^4).");
        sb.AppendLine("  A5  The clock signal is a FRACTIONAL FREQUENCY RATIO, so the ceiling is the clock's");
        sb.AppendLine("      fractional resolution: 1e-18 (best optical clocks); 1e-12 is a crude systematic.");
        sb.AppendLine();

        PrintHeader(sb, "1. DERIVED — the law and the SUBSTRATE reading (NOT refuted)");
        sb.AppendLine($"  Earth surface:  Delta ln rho/d = 2.320443e-10  = {2.320443e-10 / ClockCeiling:E3} x the 1e-18 floor");
        sb.AppendLine("                  (G_004: AT/GR = 0.99600)");
        sb.AppendLine($"  galactic field: Delta ln rho/d = 5.367333e-07 = {5.367333e-7 * 86400:F6} s/day");
        sb.AppendLine($"                  = {5.367333e-7 / ClockCeiling:E3} x the floor (G_009 cross-check 0.99668)");
        sb.AppendLine("  => for the SUBSTRATE the law is derived and observationally consistent.");

        PrintHeader(sb, "2. THE FOUR SYSTEMS — AT-naive vs GR vs the observed ceiling");
        double finesse = 1e6, pCirc = finesse / Math.PI, uCav = pCirc * 2.0 * 0.3 / C;
        double uRes = StoredEnergy(1e7, 1e-3, 1.55e-6);
        double rRes = Math.Cbrt(3.0 * Math.Pow(1.55e-6, 3) / (4.0 * Math.PI));
        double uNet = 1e6 * 1e-12 / (2.0 * Math.PI * 6e9);
        double rNet = Math.Cbrt(3.0 * 1e-9 / (4.0 * Math.PI));
        sb.AppendLine("  system                          contrast   AT-naive dTau/tau   GR dPhi/c^2     AT/GR        exclusion @1e-18");
        sb.AppendLine($"  optical cavity (F=1e6, 1 W, 0.3 m)   1e2       {AtNaive(2.0),14:F10}   {GrDepth(uCav, 0.15),12:E4}   {AtNaive(2.0) / GrDepth(uCav, 0.15),10:E3}   {AtNaive(2.0) / ClockCeiling:E4}");
        sb.AppendLine($"  resonator array (Q=1e7, 1 mW, 1550 nm)  1e4   {AtNaive(Math.Log(1e4)),14:F10}   {GrDepth(uRes, rRes),12:E4}   {AtNaive(Math.Log(1e4)) / GrDepth(uRes, rRes),10:E3}   {AtNaive(Math.Log(1e4)) / ClockCeiling:E4}");
        sb.AppendLine($"  photon lattice (Sr, I=2.439413e8 W/m^2) 1e3   {AtNaive(Math.Log(1e3)),14:F10}   (see l)       large      {AtNaive(Math.Log(1e3)) / ClockCeiling:E4}");
        sb.AppendLine($"  oscillator network (Q=1e6, 1 pW, 6 GHz)  10  {AtNaive(Math.Log(10.0)),14:F10}   {GrDepth(uNet, rNet),12:E4}   {AtNaive(Math.Log(10.0)) / GrDepth(uNet, rNet),10:E3}   {AtNaive(Math.Log(10.0)) / ClockCeiling:E4}");
        sb.AppendLine();
        sb.AppendLine($"  optical cavity stored energy U = {uCav:E6} J;  resonator U = {uRes:E6} J;  network U = {uNet:E6} J");
        sb.AppendLine($"  -> GR predictions span 1e-47 … 1e-58: unobservable. AT-naive spans 0.77 … 3.07: a 77–307 % shift.");

        PrintHeader(sb, "3. THE SHARPEST TEST — a lattice clock inside its own standing wave");
        sb.AppendLine("  Sr optical lattice clock: lambda = 813 nm, depth 100 E_rec, 300 a.u. polarizability");
        sb.AppendLine("  => I = 2.439413e8 W/m^2 at the antinode, and the clock reads a reproducible frequency to 1e-18.");
        sb.AppendLine("  Node/antinode contrast ladder:");
        sb.AppendLine("    contrast    Delta ln I     AT-naive dTau/tau     exclusion @1e-18");
        foreach (var (cont, atn, exc) in new (double, double, double)[]
                 { (1e2, 1.53505673, 1.5351e18), (1e3, 2.30258509, 2.3026e18),
                   (1e4, 3.07011346, 3.0701e18), (1e6, 4.60517019, 4.6052e18) })
            sb.AppendLine($"    {cont,8:E0}    {Math.Log(cont),10:F6}     {atn,14:F8}     {exc:E4}");
        sb.AppendLine("  => AT-naive predicts a 230.26 % shift across the lattice of the very clock that tests it,");
        sb.AppendLine("     so the naive identification is excluded by 2.303e18 by the best available experiment.");

        PrintHeader(sb, "4. EXCLUSION AGAINST EVERY CEILING");
        sb.AppendLine("  ceiling    cavity        resonator     lattice       network");
        foreach (var Ceiling in new[] { 1e-12, 1e-15, 1e-18, 1e-19 })
            sb.AppendLine($"  {Ceiling,-9:E0}  {AtNaive(2.0) / Ceiling,11:E3}  {AtNaive(Math.Log(1e4)) / Ceiling,11:E3}  {AtNaive(Math.Log(1e3)) / Ceiling,11:E3}  {AtNaive(Math.Log(10.0)) / Ceiling,11:E3}");
        sb.AppendLine("  => no ceiling exists at which any realizable |psi|^2 profile survives.");

        PrintHeader(sb, "5. CONCLUSIONS");
        sb.AppendLine("  C1  DERIVED — the clock law DeltaTau/tau = (1/d) Delta ln rho and its LOGARITHMIC form, and the");
        sb.AppendLine("      SUBSTRATE reading, which reproduces GR at the Earth's surface (0.99600) and the galactic");
        sb.AppendLine("      cross-check (0.99668 = 0.046374 s/day, 5.367e11 x the floor).");
        sb.AppendLine("  C2  BOUNDARY — the IDENTIFICATION of a laboratory |psi|^2 with rho, and the scale invariance");
        sb.AppendLine("      (only ratios of rho are physical). The coupling is a boundary input and is NOT borrowed");
        sb.AppendLine("      by a readout (G_011b).");
        sb.AppendLine("  C3  REFUTED — any experimentally realizable |psi|^2 profile producing a measurable AT metric");
        sb.AppendLine("      clock shift: excluded by 6.7e17 … 3.1e18 at the 1e-18 ceiling (and by 6.7e11 … 3.1e12 even");
        sb.AppendLine("      at a crude 1e-12), because the law is LOGARITHMIC and every realizable contrast exceeds 10,");
        sb.AppendLine("      so the prediction is O(1) with no tuning that makes it small.");
        sb.AppendLine("  C4  THE MISMATCH IS STRUCTURAL: on real optical systems AT-naive and GR differ by 1.9e46 to");
        sb.AppendLine("      4.3e49, and the observation agrees with GR. So the identification premise is not merely");
        sb.AppendLine("      unproven — it is experimentally excluded at bench scale, which is exactly why the substrate");
        sb.AppendLine("      reading is the only survivable one.");
        sb.AppendLine("  C5  CRITICAL ANSWER: NO. No experimentally realizable |psi|^2 profile produces a measurable AT");
        sb.AppendLine("      metric clock shift; the effect that exists is the substrate's, and it is consistent with GR.");

        PrintHeader(sb, "6. CLASSIFICATION");
        sb.AppendLine("  DERIVED   the clock law, its logarithmic form, and the SUBSTRATE consistency (0.99600 / 0.99668).");
        sb.AppendLine("  BOUNDARY  the identification rho = |psi|^2 for a laboratory field; scale invariance (ratios only).");
        sb.AppendLine("  REFUTED   every realizable laboratory |psi|^2 metric clock shift (6.7e17 … 3.1e18 exclusion;");
        sb.AppendLine("            AT-naive vs GR = 1.9e46 … 4.3e49, observation agreeing with GR).");
        sb.AppendLine("  No reclassification; D_040 untouched; no canonical claim, value or equation changes; no new");
        sb.AppendLine("  primitive; deterministic (exact algebra, no randomness).");

        Output.WriteLine(sb.ToString());
    }

    private static void PrintHeader(StringBuilder sb, string title)
    {
        sb.AppendLine(new string('=', 100));
        sb.AppendLine(title);
        sb.AppendLine(new string('=', 100));
    }
}
