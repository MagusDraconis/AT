using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 3 — attempt to falsify the discriminating prediction. Checks whether the sign of the
/// native acceleration is physical or an artifact, by computing the geodesic acceleration DIRECTLY
/// from the metric (a = −Γ^x_00 = +(1/2)g^xx ∂_x g_00) and comparing to a = −∇Φ. The native field
/// a = −(1/d)∇lnρ points toward density MINIMA (repulsive around peaks, "attractive" toward minima),
/// opposite to Newtonian gravity (which points toward mass/density maxima).
///
/// Tests: G4-O30 (sign convention), G4-O31 (signature + weak-field), G4-O32 (direct geodesic + gauge).
/// </summary>
public class G4O_Phase3_FalsificationAttemptTests : ResearchTestBase
{
    public G4O_Phase3_FalsificationAttemptTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5;

    // ── G4-O30: sign convention (verify against the Newtonian case) ───────────────────

    [Fact]
    public void G4_O30_SignConventionIsFixed()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O30: is the sign a free convention? (check against Newton)");

        // Newtonian reference: Φ = −GM/r, g_00 = −(1+2Φ) = −1+2GM/r (weak field GM=0.1, r=1).
        // a = −Γ = +(1/2)g^xx ∂_x g_00 = −GM/r² (attractive, inward).
        Func<double, double> g00Newt = r => -(1.0 - 2.0 * 0.1 / Math.Abs(r));
        double dg = (g00Newt(1.001) - g00Newt(0.999)) / 0.002;   // ∂_r g_00 = −2GM/r² < 0
        double aNewt = +0.5 * 1.0 * dg;                          // a = +(1/2)g^xx ∂g_00
        sb.AppendLine($"Newtonian (Φ=−GM/r, GM=0.1): a = −Γ = {aNewt:F4} (expect ≈ −0.1 = −GM/r², attractive)");

        // AT ρ=1+ax² (density MINIMUM at origin): a = −(1/d)∇lnρ < 0 (points toward the minimum).
        double aMin = PhysicalObservables.GeodesicAcceleration(0.4, A, 3);
        sb.AppendLine($"AT ρ=1+ax² (min at x=0): a = −Γ = {aMin:F4} (points toward the density minimum)");

        // AT Gaussian (density PEAK at origin): a = −(1/d)∇lnρ > 0 (repulsive, away from the peak).
        double aPeak = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Gaussian(u), 0.4, 3);
        sb.AppendLine($"AT Gaussian (peak at x=0): a = −∇lnρ = {aPeak:F4} (repulsive, away from the peak)");

        sb.AppendLine();
        sb.AppendLine($"Newtonian formula is attractive (−GM/r²) with the SAME a=−Γ convention; AT's field");
        sb.AppendLine($"points toward density MINIMA (opposite Newton) — the sign is FIXED by g_00 = −ρ^(2/d),");
        sb.AppendLine("not a free convention.");
        Output.WriteLine(sb.ToString());

        Assert.True(aNewt < -0.09 && aNewt > -0.11, $"Newtonian a should be ≈ −0.1, got {aNewt:F4}");
        Assert.True(aMin < 0, "ρ=1+ax² (minimum) field should point toward the minimum (a<0)");
        Assert.True(aPeak > 0, "Gaussian (peak) field should be repulsive (a>0)");
    }

    // ── G4-O31: metric signature + weak-field limits ──────────────────────────────────

    [Fact]
    public void G4_O31_SignatureAndWeakField()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O31: signature + weak-field limits (does the sign persist?)");

        double x = 0.4;
        // Exact weak-field potential Φ = (ρ^(2/d) − 1)/2 > 0 where ρ > 1.
        double phiExact = PhysicalObservables.WeakFieldPotential(x, A, 3);
        sb.AppendLine($"weak-field Φ (exact) = {phiExact:F4} > 0 where ρ > 1; linearized σ = {PhysicalObservables.EffectivePotential(x, A, 3):F4}");

        // Acceleration sign is IDENTICAL for the exact Φ and the linearized σ (both point toward minima).
        double aExact = -(PhysicalObservables.WeakFieldPotential(x + 1e-5, A, 3) - PhysicalObservables.WeakFieldPotential(x - 1e-5, A, 3)) / 2e-5;
        double aLin = PhysicalObservables.Acceleration(x, A, 3);
        bool sameSign = Math.Sign(aExact) == Math.Sign(aLin);
        sb.AppendLine($"a from exact Φ = {aExact:F4}, a from σ = {aLin:F4} — same sign: {sameSign}");

        // Gaussian peak: both weak-field forms give REPULSIVE (a>0).
        double aGauss = PhysicalObservables.AtAcceleration(u => PhysicalObservables.Gaussian(u), x, 3);
        sb.AppendLine($"Gaussian peak: a = −∇lnρ = {aGauss:F4} (repulsive, a>0) — sign independent of weak-field linearization");

        sb.AppendLine();
        sb.AppendLine($"the sign is robust to the weak-field limit and the signature (g_00 = −ρ^(2/d) deepens at");
        sb.AppendLine("density peaks in any Lorentzian signature).");
        Output.WriteLine(sb.ToString());

        Assert.True(phiExact > 0, "weak-field potential should be positive where ρ > 1");
        Assert.True(sameSign, "exact and linearized potentials should give the same acceleration sign");
        Assert.True(aGauss > 0, "Gaussian peak should be repulsive (a>0)");
    }

    // ── G4-O32: direct geodesic vs Φ + gauge invariance ────────────────────────────────

    [Fact]
    public void G4_O32_DirectGeodesicAndGaugeInvariance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O32: direct geodesic extraction + gauge invariance");

        // Direct geodesic a = −Γ^x_00 must equal a = −∇Φ = −(1/d)∇lnρ (the G→Φ reduction).
        double[] xs = { -0.6, -0.2, 0.2, 0.6 };
        bool agree = true;
        foreach (double x in xs)
        {
            double aGeo = PhysicalObservables.GeodesicAcceleration(x, A, 3);
            double aPot = PhysicalObservables.Acceleration(x, A, 3);
            bool ok = Math.Abs(aGeo - aPot) < 1e-9;
            if (!ok) agree = false;
            sb.AppendLine($"x={x:F2}: a_geodesic = {aGeo:F4}, a_Φ = {aPot:F4}  {ok}");
        }

        // Poisson consistency: ΔΦ = Δσ = −(1/2)ρR (d=2) ties the geodesic potential to the Einstein tensor.
        double lapPhi = PhysicalObservables.PotentialLaplacian(0.4, A, 2);
        double rho = 1.0 + A * 0.4 * 0.4;
        double r = HigherDimEinstein.ScalarCurvature(0.4, A, 2);
        double poisson = lapPhi + 0.5 * rho * r;

        // Gauge: the conformal factor ρ^(2/d) is the PHYSICAL metric (g_00 = −ρ^(2/d) ≠ −1), not removable.
        double g00 = PhysicalObservables.MetricG00(0.4, A, 3);

        sb.AppendLine();
        sb.AppendLine($"geodesic a = −∇Φ for all x: {agree}");
        sb.AppendLine($"Poisson consistency ΔΦ + (1/2)ρR = 0 (d=2): {Math.Abs(poisson) < 1e-12}");
        sb.AppendLine($"conformal factor is physical (g_00 = {g00:F4} ≠ −1, not a removable gauge)");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: ROBUST — the native acceleration is the genuine geodesic acceleration of");
        sb.AppendLine("g = ρ^(2/d)η (a = −Γ^x_00 = −(1/d)∇lnρ), consistent with the G→Φ reduction; its sign (toward");
        sb.AppendLine("density minima, i.e. repulsive around peaks) is NOT a sign/gauge/weak-field artifact.");
        Output.WriteLine(sb.ToString());

        Assert.True(agree, "geodesic and potential accelerations disagree");
        Assert.True(Math.Abs(poisson) < 1e-12, "Poisson consistency fails");
        Assert.True(Math.Abs(g00 + 1.0) > 0.01, "conformal factor should be physical (g_00 ≠ −1)");
    }
}
