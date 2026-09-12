using System.Text;
using AT.Tests.Shared;
using static AT.Tests.Shared.PhysicalUnits;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_015 — Rho To Metric Audit (group G — Gravity Source).
///
/// GIVEN: rho = |psi|^2 and g_00 = -rho^(2/d).
/// QUESTION: can a LABORATORY q profile produce any measurable metric effect?
/// Cases: photon occupation, cavity modes, resonator lattice, oscillator lattice.
/// Compute: Delta tau, Delta Phi, equivalent gravity.   Uses G_010, G_011b, G_014.
///
/// TWO CHANNELS (the audit's whole content):
///   ANALOGUE (A) — the density CONTRAST itself: Delta tau/tau = Delta ln q / d is a measurable ratio
///                  (G_014: q is a kappa = 1 observable). The counting-noise floor 1.6102e-6 is 0.046374
///                  s/day of depth, a 4.8:1 contrast is 45 176.138 s/day and a 20:1 contrast 86 277.089
///                  s/day — all trivially above any clock. MEASURABLE, for every case.
///   METRIC (M)  — a REAL time dilation, which needs the lab's q to BE the actualization density. The only
///                  laboratory handle on that is mass-energy, whose depth is Delta Phi/c^2 = G m/(R c^2)
///                  with m = E/c^2, and AT predicts exactly the Newtonian field for it (G_011b).
///
/// RESULTS (measured G; Delta Phi/c^2 = G m/(R c^2), floor = 1e-18)
///   case                                    m [kg]        dPhi/c^2      a [m/s^2]     x floor
///   photon occupation (1 J, 1 cm cavity)    1.1127e-17    1.6525e-42    2.9705e-23    1.65e-24
///   cavity modes (1 kJ, 6 cm SRF)           1.1127e-14    1.3771e-40    2.0628e-22    1.38e-22
///   resonator lattice (1 mJ, 1 cm)          1.1127e-20    8.2627e-46    7.4262e-27    8.26e-28
///   oscillator lattice (1 nJ, 1 cm)         1.1127e-26    8.2627e-52    7.4262e-33    8.26e-34
///   strongest LAB case: an energy density u in a 1 m ball (V = 4.189 m^3)
///     chemical 1e9 J/m^3                    4.6607e-08    3.4611e-35                  3.46e-17
///     capacitor 1e12                        4.6607e-05    3.4611e-32                  3.46e-14
///     magnetic 3e13                         1.3982e-03    1.0383e-30                  1.04e-12
///     nuclear-scale 1e18                    4.6607e+01    3.4611e-26                  3.46e-08
///     neutron-star core 1e34                4.6607e+17    3.4611e-10                  3.46e+08  (ASTROPHYSICAL)
///
/// THE FLOOR ITSELF: 1e-18 needs M/r = 1.3466e9 kg/m — 13 466 tonnes within a CENTIMETRE — i.e. an energy
/// density of 2.8893e25 J/m^3, 2.889e7x the nuclear scale; the G_005 band top (0.1407 s/day) needs
/// 4.7063e37 J/m^3, 4706x a neutron-star core; the observed galactic field (0.0464 s/day) needs
/// M/r = 7.2276e20 kg/m.
///
/// VERDICTS: MEASURABLE = the ANALOGUE contrast channel (all four cases; kappa = 1, G_014) ·
/// ASTROPHYSICAL ONLY = every metric effect that exists (galactic 0.0464 s/day through the band top) ·
/// REFUTED = a laboratory q profile producing a METRIC effect: the best lab-scale case is 3.4611e-26,
/// 3.46e-8 of the measurement floor.
///
/// CRITICAL ANSWER: NO. No physically realizable q produces a clock shift above the measurement floor —
/// the strongest laboratory case is eight orders of magnitude short, and the floor requires nuclear-density
/// matter compressed into a centimetre.
///
/// Deterministic: exact algebra, no randomness.  No reclassification; D_040 untouched; no canonical claim,
/// value or equation changes; no new primitive.
/// </summary>
public class Y_G_015_Tests : ResearchTestBase
{
    public Y_G_015_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const int D = 3;
    private const double ClockFloor = 1e-18;             // best optical-clock fractional resolution
    private const double ObservedContrast = 1.6102e-6;   // G_003's ambient galactic calibration
    private const double BandCeiling = 4.8867e-6;        // G_005's 1 % Poisson ceiling
    private const double CountsPerCell = 3.856917553651e11;

    /// <summary>The clock ratio of a density contrast: f = Delta ln rho / d (G_009).</summary>
    private static double ClockRatio(double deltaLnRho) => deltaLnRho / D;

    /// <summary>The mass-to-radius ratio that produces a potential depth f: M/r = f c^2 / G (G_011b).</summary>
    private static double MassOverRadius(double f) => f * C * C / G_CODATA;

    /// <summary>The potential depth of an energy E confined to a region of size R: m = E/c^2, dPhi/c^2 = G m/(R c^2).</summary>
    private static double DepthOfEnergy(double energy, double size) => G_CODATA * (energy / (C * C)) / (size * C * C);

    /// <summary>The Newtonian acceleration at the boundary of that region.</summary>
    private static double GravityOfEnergy(double energy, double size) => G_CODATA * (energy / (C * C)) / (size * size);

    /// <summary>The potential depth of a uniform energy density u in a sphere of radius R.</summary>
    private static double DepthOfDensity(double density, double radius)
    {
        double volume = 4.0 / 3.0 * Math.PI * radius * radius * radius;
        return DepthOfEnergy(density * volume, radius);
    }

    // ── 1. The clock ladder: what each target costs in mass ──────────────────────

    [Fact]
    public void Y_G_015_ClockLadder()
    {
        // f = Delta ln rho/d (G_009) and M/r = f c^2/G (G_011b/G_004's Earth self-check).
        Assert.True(Math.Abs(ClockRatio(3.0 * ClockFloor) - ClockFloor) < 1e-30);
        Assert.True(Math.Abs(MassOverRadius(ClockFloor) - 1.3466e9) / 1.3466e9 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(1e-9 / 86400.0) - 1.5586e13) / 1.5586e13 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(ObservedContrast / D) - 7.2276e20) / 7.2276e20 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(BandCeiling / D) - 2.1935e21) / 2.1935e21 < 1e-3);

        // Expressed as the mass that must sit within a CENTIMETRE for each target.
        Assert.True(Math.Abs(MassOverRadius(ClockFloor) * 0.01 - 1.3466e7) / 1.3466e7 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(1e-9 / 86400.0) * 0.01 - 1.5586e11) / 1.5586e11 < 1e-3);
        Assert.True(MassOverRadius(ClockFloor) * 0.01 / 1e3 > 1.3e4);        // 13 466 tonnes
        Assert.True(MassOverRadius(BandCeiling / D) * 0.01 > 2.0e19);        // 2.19e19 kg

        // The Earth self-check (G_004): M/R = 9.3740e17 kg/m for 6.9613e-10.
        Assert.True(Math.Abs(MassOverRadius(GM_Earth / (R_Earth * C * C)) - GM_Earth / G_CODATA / R_Earth)
                    / (GM_Earth / G_CODATA / R_Earth) < 1e-9);
    }

    // ── 2. The four cases: the metric effect of stored energy ────────────────────

    [Fact]
    public void Y_G_015_MetricEffectOfEnergy()
    {
        // The only laboratory handle on rho_actualization is mass-energy: m = E/c^2 and
        // dPhi/c^2 = G m/(R c^2). Every case is dozens of orders below the 1e-18 floor.
        foreach (var (name, energy, size, depth, gravity, ratio) in new[]
        {
            ("photon occupation", 1.0, 5e-3, 1.6525e-42, 2.9705e-23, 1.65e-24),
            ("cavity modes", 1e3, 0.06, 1.3771e-40, 2.0628e-22, 1.38e-22),
            ("resonator lattice", 1e-3, 0.01, 8.2627e-46, 7.4262e-27, 8.26e-28),
            ("oscillator lattice", 1e-9, 0.01, 8.2627e-52, 7.4262e-33, 8.26e-34),
        })
        {
            double d = DepthOfEnergy(energy, size);
            double a = GravityOfEnergy(energy, size);
            Assert.True(Math.Abs(d / depth - 1.0) < 1e-3, $"{name}: dPhi/c^2 = {d}");
            Assert.True(Math.Abs(a / gravity - 1.0) < 1e-3, $"{name}: a = {a}");
            Assert.True(Math.Abs(d / ClockFloor / ratio - 1.0) < 1e-2, $"{name}: x floor = {d / ClockFloor}");
            Assert.True(d < 1e-39, $"{name} is far below the floor");
        }

        // The stored energy corresponds to a genuine mass: 1 J is 1.1127e-17 kg (and 5.0341166e18 photons
        // at 1 um), 1 kJ is 1.1127e-14 kg. The metric effect of that mass at a few centimetres is ~1e-40:
        // twenty-two orders below the best optical clock.
        Assert.True(Math.Abs(1.0 / (C * C) - 1.1127e-17) / 1.1127e-17 < 1e-4);
        Assert.True(Math.Abs(1e3 / (C * C) - 1.1127e-14) / 1.1127e-14 < 1e-4);
        Assert.True(DepthOfEnergy(1e3, 0.06) * 1e21 < ClockFloor);
    }

    // ── 3. The strongest possible lab case ───────────────────────────────────────

    [Fact]
    public void Y_G_015_StrongestLabCase()
    {
        // No laboratory configuration beats an ENERGY DENSITY argument, so take the best densities known
        // and put them in a 1 m ball (V = 4.189 m^3).
        Assert.True(Math.Abs(DepthOfDensity(1e9, 1.0) - 3.4611e-35) / 3.4611e-35 < 1e-3);   // chemical
        Assert.True(Math.Abs(DepthOfDensity(1e12, 1.0) - 3.4611e-32) / 3.4611e-32 < 1e-3);  // capacitor
        Assert.True(Math.Abs(DepthOfDensity(3e13, 1.0) - 1.0383e-30) / 1.0383e-30 < 1e-3);  // magnetic
        Assert.True(Math.Abs(DepthOfDensity(1e18, 1.0) - 3.4611e-26) / 3.4611e-26 < 1e-3);  // nuclear scale

        // The nuclear-scale case — the strongest thing a laboratory could ever hold in a metre — is still
        // 3.46e-8 of the measurement floor: eight orders of magnitude short.
        double best = DepthOfDensity(1e18, 1.0);
        Assert.True(Math.Abs(best / ClockFloor - 3.461e-8) / 3.461e-8 < 1e-2, $"best/floor = {best / ClockFloor}");
        Assert.True(best * 1e7 < ClockFloor);
        Assert.True(ClockFloor / best > 2.8e7);

        // What the floor itself needs: 2.8893e25 J/m^3, i.e. 2.889e7x the nuclear scale; and the G_005 band
        // top needs 4.7063e37 J/m^3, 4706x a neutron-star core. Those are not laboratory densities.
        double needFloor = ClockFloor * C * C / G_CODATA * C * C / (4.0 / 3.0 * Math.PI);
        double needBand = (BandCeiling / D) * C * C / G_CODATA * C * C / (4.0 / 3.0 * Math.PI);
        Assert.True(Math.Abs(needFloor / 2.8893e25 - 1.0) < 1e-3, $"u_floor = {needFloor}");
        Assert.True(Math.Abs(needFloor / 1e18 - 2.8893e7) / 2.8893e7 < 1e-3);
        Assert.True(Math.Abs(needBand / 4.7063e37 - 1.0) < 1e-3, $"u_band = {needBand}");
        Assert.True(Math.Abs(needBand / 1e34 - 4706.3) / 4706.3 < 1e-3);

        // A neutron-star core (1e34 J/m^3) WOULD be measurable (3.4611e-10) — which is exactly the
        // ASTROPHYSICAL-ONLY band, not a laboratory one.
        Assert.True(DepthOfDensity(1e34, 1.0) / ClockFloor > 1e8);
        Assert.True(DepthOfDensity(1e34, 1.0) < 1e-8);
    }

    // ── 4. The measurable channel: the density contrast itself ───────────────────

    [Fact]
    public void Y_G_015_AnalogueChannel()
    {
        // G_014 identified the observation: q is a kappa = 1 observable, so Delta tau/tau = Delta ln q/d is a
        // measurable RATIO at any contrast above the counting noise. This is what IS measurable.
        Assert.True(Math.Abs(ObservedContrast / D * 86400.0 - 0.046373) < 1e-5);          // the noise floor
        Assert.True(Math.Abs(Math.Log(4.8) / D * 86400.0 - 45176.138) < 0.01);            // the G_002 witness
        Assert.True(Math.Abs(Math.Log(20.0) / D * 86400.0 - 86277.089) < 0.01);           // a 20:1 contrast
        Assert.True(ObservedContrast / D / ClockFloor > 1e11);                            // 5.4e11x the floor
        Assert.True(Math.Log(20.0) / D / ClockFloor > 1e17);

        // The counting statistics set that floor: delta = 1/sqrt(<N>) with <N> = 3.856917553651e11 (G_014).
        Assert.True(Math.Abs(1.0 / Math.Sqrt(CountsPerCell) / ObservedContrast - 1.0) < 1e-12);

        // BUT the same contrasts demand an equivalent mass that no laboratory can carry: M = f R c^2/G at
        // 1 cm. The noise floor alone needs 7.2276e18 kg, the G_002 witness 7.0409e24 kg (1.18 Earth masses)
        // and a 20:1 contrast 1.3447e25 kg (2.25 Earth masses).
        Assert.True(Math.Abs(MassOverRadius(ObservedContrast / D) * 0.01 - 7.2276e18) / 7.2276e18 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(Math.Log(4.8) / D) * 0.01 - 7.0409e24) / 7.0409e24 < 1e-3);
        Assert.True(Math.Abs(MassOverRadius(Math.Log(20.0) / D) * 0.01 - 1.3447e25) / 1.3447e25 < 1e-3);
        Assert.True(MassOverRadius(Math.Log(20.0) / D) * 0.01 / 5.9722e24 > 2.0);         // > 2 Earth masses

        // So the ANALOGUE clock read is measurable while the METRIC reading is refuted: the profile would
        // have to carry that mass-energy for the reading to be physical (G_011b: the coupling is not borrowed).
        Assert.True(MassOverRadius(ObservedContrast / D) * 0.01 / (1.0 / (C * C)) > 6.4e35);  // joules needed
    }

    // ── 5. The four cases, per-case verdicts ─────────────────────────────────────

    [Fact]
    public void Y_G_015_FourCases()
    {
        // For each laboratory platform: (i) the ANALOGUE read (kappa = 1, directly measurable) and (ii) the
        // METRIC effect (mass-energy only), with the ratio to the floor.
        var cases = new (string Name, double Energy, double Size, double Metric, double Analogue)[]
        {
            // 1 J of 1 um photons in a 1 cm cavity: 5.0341166e18 photons, contrast directly countable.
            ("photon occupation", 1.0, 5e-3, 1.6525e-42, Math.Log(20.0) / D * 86400.0),
            // a 1 kJ superconducting cavity (6 cm), Q = 1e10: the same energy argument.
            ("cavity modes", 1e3, 0.06, 1.3771e-40, Math.Log(20.0) / D * 86400.0),
            // a 96-element resonator lattice holding 1 mJ on 1 cm.
            ("resonator lattice", 1e-3, 0.01, 8.2627e-46, Math.Log(20.0) / D * 86400.0),
            // the G_011b oscillator chain holding 1 nJ on 1 cm.
            ("oscillator lattice", 1e-9, 0.01, 8.2627e-52, Math.Log(20.0) / D * 86400.0),
        };
        foreach (var c in cases)
        {
            Assert.True(Math.Abs(DepthOfEnergy(c.Energy, c.Size) / c.Metric - 1.0) < 1e-3, c.Name);
            Assert.True(c.Metric / ClockFloor < 1e-20, $"{c.Name}: metric {c.Metric}");
            Assert.True(c.Analogue / ClockFloor > 1e17, $"{c.Name}: the analogue read is measurable");
        }
        // Ordered by metric effect: cavity modes > photon occupation > resonator lattice > oscillator lattice.
        Assert.True(cases[1].Metric > cases[0].Metric);
        Assert.True(cases[0].Metric > cases[2].Metric);
        Assert.True(cases[2].Metric > cases[3].Metric);
    }

    // ── 6. The critical answer ───────────────────────────────────────────────────

    [Fact]
    public void Y_G_015_CriticalAnswer()
    {
        // Does any physically realizable q produce a clock shift above the measurement floor?
        // NO — in the METRIC sense: the strongest laboratory case (nuclear-scale energy density in a metre)
        // is 3.4611e-26 against a 1e-18 floor, a factor 2.889e7 short.
        double best = DepthOfDensity(1e18, 1.0);
        Assert.True(best < ClockFloor);
        Assert.True(Math.Abs(ClockFloor / best - 2.8893e7) / 2.8893e7 < 1e-2);
        // And the floor itself is not laboratory physics: 13 466 tonnes inside a centimetre.
        Assert.True(MassOverRadius(ClockFloor) * 0.01 / 1e3 > 1.3e4);
        Assert.True(MassOverRadius(ClockFloor) * 0.01 / 5.9722e24 < 1e-17);

        // YES — in the ANALOGUE sense: the density contrast is a kappa = 1 observable (G_014), so the ratio
        // Delta ln q/d is measurable at 0.046374 s/day at the counting floor and 86 277.089 s/day at 20:1.
        Assert.True(ObservedContrast / D * 86400.0 > 0.046);
        Assert.True(Math.Log(20.0) / D * 86400.0 > 8.6e4);

        // The metric effects that DO exist are astrophysical: the observed galactic field (0.0464 s/day,
        // G_003/G_009) through the G_005 band top (0.1407 s/day) need M/r = 7.2276e20 to 2.1935e21 kg/m.
        Assert.True(MassOverRadius(ObservedContrast / D) > 7.2e20);
        Assert.True(MassOverRadius(BandCeiling / D) > 2.1e21);
        Assert.True(MassOverRadius(ObservedContrast / D) / MassOverRadius(ClockFloor) > 5e11);
    }

    // ── 7. Research report ───────────────────────────────────────────────────────

    [Fact]
    public void Y_G_015_Run()
    {
        var sb = new StringBuilder();

        PrintHeader(sb, "ResearchY-G_015 — RHO TO METRIC AUDIT");
        sb.AppendLine("Given: rho = |psi|^2 and g_00 = -rho^(2/d).");
        sb.AppendLine("Question: can a LABORATORY q profile produce any measurable metric effect?");
        sb.AppendLine("Cases: photon occupation, cavity modes, resonator lattice, oscillator lattice.");
        sb.AppendLine("Compute: Delta tau, Delta Phi, equivalent gravity.  Uses G_010, G_011b, G_014.");
        sb.AppendLine();

        PrintHeader(sb, "ASSUMPTIONS");
        sb.AppendLine("  A1  The metric reading of a density contrast is Delta tau/tau = Delta ln rho/d (G_009).");
        sb.AppendLine("  A2  A laboratory q is a kappa = 1 observable (G_014): the CONTRAST is directly measurable.");
        sb.AppendLine("  A3  The only laboratory handle on rho_actualization is mass-energy: m = E/c^2 and");
        sb.AppendLine("      Delta Phi/c^2 = G m/(R c^2); AT predicts exactly the Newtonian field for it (G_011b).");
        sb.AppendLine("  A4  The measurement floor is 1e-18 (best optical clocks); the counting floor is 1/sqrt(<N>).");
        sb.AppendLine();

        PrintHeader(sb, "1. THE CLOCK LADDER (what each target needs)");
        sb.AppendLine("  target                f = Delta tau/tau   Delta ln rho     M/r [kg/m]        M in 1 cm [kg]");
        foreach (var (name, f) in new[] { ("clock floor 1e-18", ClockFloor), ("1 ns/day", 1e-9 / 86400.0),
                                          ("observed galactic", ObservedContrast / D), ("band top", BandCeiling / D) })
            sb.AppendLine($"  {name,-20} {f,15:E4}   {f * D,13:E4}   {MassOverRadius(f),15:E4}   {MassOverRadius(f) * 0.01,13:E4}");
        sb.AppendLine("  => the FLOOR alone is 13 466 tonnes inside a centimetre.");
        sb.AppendLine();

        PrintHeader(sb, "2. THE FOUR CASES");
        sb.AppendLine("  case                    m [kg]        dPhi/c^2      a [m/s^2]     x floor     metric verdict");
        sb.AppendLine($"  photon occupation       {1.0 / (C * C),12:E4}   {DepthOfEnergy(1.0, 5e-3),12:E4}   {GravityOfEnergy(1.0, 5e-3),12:E4}   {DepthOfEnergy(1.0, 5e-3) / ClockFloor,10:E2}   REFUTED");
        sb.AppendLine($"  cavity modes            {1e3 / (C * C),12:E4}   {DepthOfEnergy(1e3, 0.06),12:E4}   {GravityOfEnergy(1e3, 0.06),12:E4}   {DepthOfEnergy(1e3, 0.06) / ClockFloor,10:E2}   REFUTED");
        sb.AppendLine($"  resonator lattice       {1e-3 / (C * C),12:E4}   {DepthOfEnergy(1e-3, 0.01),12:E4}   {GravityOfEnergy(1e-3, 0.01),12:E4}   {DepthOfEnergy(1e-3, 0.01) / ClockFloor,10:E2}   REFUTED");
        sb.AppendLine($"  oscillator lattice      {1e-9 / (C * C),12:E4}   {DepthOfEnergy(1e-9, 0.01),12:E4}   {GravityOfEnergy(1e-9, 0.01),12:E4}   {DepthOfEnergy(1e-9, 0.01) / ClockFloor,10:E2}   REFUTED");
        sb.AppendLine($"  strongest LAB case      {DepthOfDensity(1e18, 1.0) * C * C,12:E4}   {DepthOfDensity(1e18, 1.0),12:E4}   {"n/a",12}   {DepthOfDensity(1e18, 1.0) / ClockFloor,10:E2}   REFUTED");
        sb.AppendLine("  (strongest LAB case = a nuclear-scale 1e18 J/m^3 held in a 1 m ball.)");
        sb.AppendLine();

        PrintHeader(sb, "3. THE TWO CHANNELS");
        sb.AppendLine("  ANALOGUE (measurable): the contrast Delta ln q/d is a RATIO, kappa = 1 (G_014):");
        sb.AppendLine($"    counting floor 1.6102e-6 -> {ObservedContrast / D * 86400.0:F6} s/day;  4.8:1 -> {Math.Log(4.8) / D * 86400.0:F3} s/day;  20:1 -> {Math.Log(20.0) / D * 86400.0:F3} s/day");
        sb.AppendLine($"    but the equivalent mass at 1 cm: {MassOverRadius(ObservedContrast / D) * 0.01:E3} kg (floor), {MassOverRadius(Math.Log(4.8) / D) * 0.01:E3} kg (4.8:1), {MassOverRadius(Math.Log(20.0) / D) * 0.01:E3} kg (20:1)");
        sb.AppendLine("    i.e. 1.18 and 2.25 EARTH MASSES for the witness and 20:1 contrasts.");
        sb.AppendLine("  METRIC (refuted): only mass-energy moves rho_actualization, and its depth is G m/(R c^2):");
        sb.AppendLine($"    best lab-scale case 3.4611e-26 = {DepthOfDensity(1e18, 1.0) / ClockFloor:E3} of the 1e-18 floor.");
        sb.AppendLine($"    the floor needs u = 2.8893e25 J/m^3 ({2.8893e25 / 1e18:E3}x nuclear scale); the band top 4.7063e37 J/m^3 ({4.7063e37 / 1e34:F1}x a neutron-star core).");
        sb.AppendLine();

        PrintHeader(sb, "4. CONCLUSIONS");
        sb.AppendLine("  C1  MEASURABLE — the ANALOGUE channel, for all four cases: the density contrast is a kappa = 1");
        sb.AppendLine("      observable, so Delta ln q/d is a measurable ratio: 0.046374 s/day at the counting floor and");
        sb.AppendLine("      86 277.089 s/day at a 20:1 contrast. The four laws of the group are all verifiable on it.");
        sb.AppendLine("  C2  REFUTED — a laboratory q profile producing a METRIC effect. The only laboratory handle is");
        sb.AppendLine($"      mass-energy, and the deepest case (1 kJ, 6 cm) gives {DepthOfEnergy(1e3, 0.06):E4}; the strongest");
        sb.AppendLine($"      possible lab-scale configuration (nuclear-scale density in a metre) gives {DepthOfDensity(1e18, 1.0):E4} —");
        sb.AppendLine($"      {DepthOfDensity(1e18, 1.0) / ClockFloor:E3} of the measurement floor.");
        sb.AppendLine("  C3  ASTROPHYSICAL ONLY — every metric effect that exists: the observed galactic field");
        sb.AppendLine($"      ({ObservedContrast / D * 86400.0:F4} s/day) through the G_005 band top ({BandCeiling / D * 86400.0:F4} s/day) needs");
        sb.AppendLine($"      M/r = {MassOverRadius(ObservedContrast / D):E4} to {MassOverRadius(BandCeiling / D):E4} kg/m; a neutron-star core");
        sb.AppendLine($"      ({DepthOfDensity(1e34, 1.0):E3}) would finally be measurable, which is exactly the point.");
        sb.AppendLine("  C4  THE FLOOR ITSELF IS NOT LABORATORY PHYSICS: 1e-18 needs 13 466 tonnes within a centimetre");
        sb.AppendLine("      (M/r = 1.3466e9 kg/m), i.e. an energy density 2.889e7x the nuclear scale.");
        sb.AppendLine("  C5  CRITICAL ANSWER: NO. No physically realizable q produces a clock shift above the");
        sb.AppendLine("      measurement floor — the strongest laboratory case is 2.889e7 times short, and the effects");
        sb.AppendLine("      that do exist are galactic. The laboratory can MEASURE the density (G_014) and HOLD a");
        sb.AppendLine("      pattern (G_012/G_013), but it cannot make the density pull on clocks.");
        sb.AppendLine();

        PrintHeader(sb, "5. CLASSIFICATION");
        sb.AppendLine("  MEASURABLE          the analogue contrast channel (all four cases; kappa = 1).");
        sb.AppendLine("  ASTROPHYSICAL ONLY  every metric effect that exists (0.0464 s/day through 0.1407 s/day).");
        sb.AppendLine("  REFUTED             a laboratory q profile producing a metric effect (8+ orders short).");
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
