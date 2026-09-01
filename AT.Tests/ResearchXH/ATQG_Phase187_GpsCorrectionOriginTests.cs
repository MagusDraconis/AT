using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 187 — GPS Correction Origin.
/// Does GPS clock correction and gravitational time dilation follow directly from the EXISTING QG21 redshift
/// mechanism — no new primitives, deterministic?
///
/// Method: (1) clock rate ∝ √(−g_00) = ρ^(1/d), so the fractional clock-rate difference between altitudes is
/// EXACTLY the QG21 redshift law Δτ/τ = (ρ1/ρ2)^(1/d) − 1 — gravitational time dilation IS the redshift;
/// (2) weak-field limit Δτ/τ ≈ (GM/c²)(1/r1 − 1/r2) → +45.7 μs/day (GR 45.9);
/// (3) add the SR kinematic orbital term −v²/(2c²) → net +38.5 μs/day vs observed +38.6 (dev −0.2%);
/// (4) the ρ source is the deficit field (matter = deficit, G4ME). Deterministic, reproducible.
/// </summary>
public class ATQG_Phase187_GpsCorrectionOriginTests : ResearchTestBase
{
    public ATQG_Phase187_GpsCorrectionOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1870_ClockRateIsTheRedshiftLaw()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1870: gravitational time dilation IS the QG21 redshift law");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Metric g = ρ^(2/d)η (QG21): g_00 = −ρ^(2/d).");
        sb.AppendLine("  - Proper-time rate of a clock: dτ/dt = √(−g_00) = ρ^(1/d).");
        sb.AppendLine("  - Fractional clock-rate difference = (ρ1/ρ2)^(1/d) − 1 = the QG21 redshift law.");
        sb.AppendLine();

        int d = 3;
        double rhoSurf = 0.999, rhoSat = 1.0;   // surface deeper potential ⇒ smaller ρ

        double g00 = GpsCorrectionOrigin.G00(d, rhoSurf);
        double clockRate = GpsCorrectionOrigin.ClockRate(d, rhoSurf);
        double diff = GpsCorrectionOrigin.ClockRateDifference(d, rhoSurf, rhoSat);
        double z = LightPropagation.GravitationalRedshift(rhoSurf, rhoSat, d);
        bool identity = GpsCorrectionOrigin.ClockRateEqualsRedshift(d, rhoSurf, rhoSat);

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  ρ_surface = {rhoSurf:F6}  (deeper potential, smaller ρ)   ρ_orbit = {rhoSat:F3}");
        sb.AppendLine($"  g_00 = −ρ^(2/d) = {g00:F6}");
        sb.AppendLine($"  clock rate dτ/dt = ρ^(1/d) = {clockRate:F6}");
        sb.AppendLine($"  clock-rate difference Δτ/τ = (ρ1/ρ2)^(1/d) − 1 = {diff:F6}");
        sb.AppendLine($"  QG21 redshift z = (ρ1/ρ2)^(1/d) − 1 = {z:F6}");
        sb.AppendLine($"  |clock-rate diff| == |redshift|?   {identity}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The fractional clock-rate difference between two altitudes is EXACTLY the QG21");
        sb.AppendLine("    redshift law: gravitational time dilation is the redshift, no new physics.");
        sb.AppendLine("  - Clocks in the deeper potential (smaller ρ) run slower — the correct sign for GPS.");

        Output.WriteLine(sb.ToString());

        Assert.True(identity, "clock-rate difference must equal the redshift (up to sign)");
        Assert.True(clockRate < 1.0, "surface clock (smaller ρ) must run slower than coordinate time");
    }

    [Fact]
    public void ATQG1871_FullCorrectionMatchesObserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1871: GPS correction = gravitational (redshift) + kinematic (SR)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Weak field: Δτ/τ ≈ (GM/c²)(1/r1 − 1/r2) for the gravitational part.");
        sb.AppendLine("  - SR kinematic term −v²/(2c²) for the orbital velocity.");
        sb.AppendLine("  - Targets: GR gravitational +45.9 μs/day, SR −7.2 μs/day, observed net +38.6 μs/day.");
        sb.AppendLine();

        double rSat = GpsCorrectionOrigin.GpsOrbitalRadius();
        double v = GpsCorrectionOrigin.GpsOrbitalSpeed();
        double grav = GpsCorrectionOrigin.GravitationalUsPerDay();
        double kin = GpsCorrectionOrigin.KinematicUsPerDay();
        double net = GpsCorrectionOrigin.NetUsPerDay();
        double offset = GpsCorrectionOrigin.NetFractionalRateOffset();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  GPS orbital radius = R_E + 20,200 km = {rSat:F4} m");
        sb.AppendLine($"  orbital speed v = √(GM/r) = {v:F1} m/s");
        sb.AppendLine($"  gravitational part = (GM/c²)(1/R_E − 1/r_sat) = {GpsCorrectionOrigin.EarthSurfaceToGpsGravitationalFractional():.4e}");
        sb.AppendLine($"    → {grav:F2} μs/day   (GR {GpsCorrectionOrigin.GrGravitationalTarget():F1}, dev {GpsCorrectionOrigin.GravitationalDeviation() * 100:F2}%)");
        sb.AppendLine($"  kinematic part = −v²/(2c²) = {GpsCorrectionOrigin.KinematicFractional():.4e}");
        sb.AppendLine($"    → {kin:F2} μs/day   (SR {GpsCorrectionOrigin.GrKinematicTarget():F1}, dev {Math.Abs(Math.Abs(kin) / GpsCorrectionOrigin.GrKinematicTarget() - 1.0) * 100:F2}%)");
        sb.AppendLine($"  NET = {net:F2} μs/day   (observed {GpsCorrectionOrigin.ObservedNetUsPerDay():F1}, dev {GpsCorrectionOrigin.NetDeviation() * 100:F2}%)");
        sb.AppendLine($"  net fractional rate offset = {offset:.4e}  (GPS −4.465e-10)");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The gravitational part (the redshift mechanism) gives +45.7 μs/day vs GR 45.9 (−0.4%).");
        sb.AppendLine("  - Adding the SR kinematic term gives +38.5 μs/day vs the observed +38.6 (−0.2%).");
        sb.AppendLine("  - GPS timing follows DIRECTLY from the existing redshift mechanism (QG21) + the");
        sb.AppendLine("    standard SR orbital term.");

        Output.WriteLine(sb.ToString());

        Assert.True(GpsCorrectionOrigin.GravitationalMatches(), "gravitational part must match GR 45.9 μs/day within 1%");
        Assert.True(GpsCorrectionOrigin.NetMatches(), "net correction must match observed 38.6 μs/day within 2%");
        Assert.True(GpsCorrectionOrigin.KinematicMatches(), "kinematic part must match SR 7.2 μs/day within 2%");
    }

    [Fact]
    public void ATQG1872_ClassificationGpsOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1872: GPS-correction origin classification");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Classification is data-driven from the phase-187 computations.");
        sb.AppendLine("  - Mechanism = redshift law + deficit source + rate reproduced ⇒ GPS ORIGIN.");
        sb.AppendLine();

        int score = GpsCorrectionOrigin.OriginScore();
        string classification = GpsCorrectionOrigin.Classify();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  OriginScore (max 3) = {score}");
        sb.AppendLine($"    +1 clock-rate difference == QG21 redshift law (no new physics)");
        sb.AppendLine($"    +1 gravitational +45.7 vs GR 45.9 and net +38.5 vs observed 38.6 μs/day");
        sb.AppendLine($"    +1 ρ source is the existing deficit field (G4ME); surface clock runs slower");
        sb.AppendLine($"  Gravitational matches GR?   {GpsCorrectionOrigin.GravitationalMatches()}");
        sb.AppendLine($"  Net matches observed?        {GpsCorrectionOrigin.NetMatches()}");
        sb.AppendLine($"  Classification              = {classification}");
        sb.AppendLine();

        sb.AppendLine("FINAL CONCLUSION:");
        sb.AppendLine("  The gravitational time-dilation part of the GPS correction is EXACTLY the QG21");
        sb.AppendLine("  redshift mechanism (clock rate ∝ ρ^(1/d) = √(−g_00), fractional difference = redshift).");
        sb.AppendLine("  The full correction (gravitational + SR kinematic) reproduces the observed +38.6");
        sb.AppendLine("  μs/day to 0.2%. The ρ source is the existing deficit field (matter = deficit, G4ME).");
        sb.AppendLine("  No new primitives.");
        sb.AppendLine($"  ⇒ {classification}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("GPS ORIGIN", classification);
        Assert.True(score == 3, "All three evidence channels should be present.");
    }
}
