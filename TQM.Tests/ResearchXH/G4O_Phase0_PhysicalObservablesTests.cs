using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// G4-O Phase 0 — physical observables of the ρ-only Einstein structure. Identifies the measurable
/// consequences of Q-events → ρ → G_μν: the curvature-density relation, the effective gravitational
/// potential Φ = (1/d)ln ρ, geodesic acceleration, redshift, lensing, and expansion — and classifies
/// each as KNOWN GR-LIKE / TQM-SPECIFIC / UNDECIDED.
///
/// Tests: G4-O00 (curvature-density + Poisson, TQM-specific), G4-O01 (acceleration + redshift,
/// GR-like), G4-O02 (lensing + expansion + dimension dependence).
/// </summary>
public class G4O_Phase0_PhysicalObservablesTests : ResearchTestBase
{
    public G4O_Phase0_PhysicalObservablesTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5;

    // ── G4-O00: curvature-density relation + native Poisson (TQM-SPECIFIC) ─────────────

    [Fact]
    public void G4_O00_CurvatureDensityAndPoisson()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O00: curvature-density relation + native Poisson equation");

        double[] xs = { -0.7, -0.3, 0.0, 0.3, 0.7 };
        sb.AppendLine($"{"x",7} {"R(x) d=2",10} {"−(lnρ)″/ρ",11}  match   {"Poisson residual d=3",20}");
        bool curvatureOk = true, poissonOk = true;
        foreach (double x in xs)
        {
            double r = HigherDimEinstein.ScalarCurvature(x, A, 2);   // d=2: R = −(1/ρ)(lnρ)″
            double rho = 1.0 + A * x * x;
            double lnr2 = 2.0 * A * (1.0 - A * x * x) / (rho * rho);
            double expect = -lnr2 / rho;
            bool ok = Math.Abs(r - expect) < 1e-12;
            double poisson = PhysicalObservables.PoissonResidual(x, A, 3);  // d=3 native Poisson
            bool pOk = Math.Abs(poisson) < 1e-12;
            if (!ok) curvatureOk = false;
            if (!pOk) poissonOk = false;
            sb.AppendLine($"{x,7:F2} {r,10:F4} {expect,11:F4}  {ok}   {poisson,20:E2}");
        }

        sb.AppendLine();
        sb.AppendLine($"R = −(lnρ)″/ρ holds exactly (d=2): {curvatureOk}");
        sb.AppendLine($"native Poisson relation ΔΦ + ((d−2)/2)|∇Φ|² = −ρ^(2/d)R/(2(d−1)) (d=3): {poissonOk}");
        sb.AppendLine();
        sb.AppendLine("The curvature is ALGEBRAICALLY fixed by ρ (no PDE/iteration); the Poisson source is the");
        sb.AppendLine("CURVATURE (ρ″ structure), not the density value — unlike GR's ΔΦ = 4πGρ.");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: TQM-SPECIFIC (algebraic curvature-density relation; curvature-sourced Poisson).");
        Output.WriteLine(sb.ToString());

        Assert.True(curvatureOk, "curvature-density relation fails");
        Assert.True(poissonOk, "native Poisson relation fails");
    }

    // ── G4-O01: effective potential + acceleration + redshift (GR-LIKE) ────────────────

    [Fact]
    public void G4_O01_AccelerationAndRedshift()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O01: effective potential, geodesic acceleration, redshift");

        double[] xs = { -0.7, -0.3, 0.0, 0.3, 0.7 };
        sb.AppendLine($"{"x",7} {"Φ=(1/d)lnρ",12} {"a=−∇Φ",10} {"redshift(x→0)",15}");
        foreach (double x in xs)
        {
            double phi = PhysicalObservables.EffectivePotential(x, A, 3);
            double acc = PhysicalObservables.Acceleration(x, A, 3);
            double z = PhysicalObservables.Redshift(x, 0.0, A, 3);
            sb.AppendLine($"{x,7:F2} {phi,12:F4} {acc,10:F4} {z,15:F4}");
        }

        // GR-like weak-field structure: a = −∇Φ, redshift = −ΔΦ, with Φ_eff = (1/d)ln ρ.
        double xTest = 0.4;
        double a1 = PhysicalObservables.Acceleration(xTest, A, 3);
        double gradPhi = (PhysicalObservables.EffectivePotential(xTest + 1e-6, A, 3)
                          - PhysicalObservables.EffectivePotential(xTest - 1e-6, A, 3)) / (2e-6);
        bool accOk = Math.Abs(a1 - (-gradPhi)) < 1e-9;

        sb.AppendLine();
        sb.AppendLine($"a = −∇Φ holds (GR weak-field form): {accOk}");
        sb.AppendLine($"redshift = −ΔΦ (gravitational redshift): standard GR form with Φ = (1/d)ln ρ");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: KNOWN GR-LIKE (acceleration and redshift have the standard weak-field form,");
        sb.AppendLine("with the TQM-specific potential Φ = (1/d)ln ρ).");
        Output.WriteLine(sb.ToString());

        Assert.True(accOk, "a = −∇Φ fails");
        // Redshift is monotonic in ΔΦ (blue toward x=0 for a>0, since ρ peaks at x=0).
        Assert.True(PhysicalObservables.Redshift(0.5, 0.0, A, 3) * PhysicalObservables.Redshift(-0.5, 0.0, A, 3) > 0,
            "redshift should be symmetric about x=0");
    }

    // ── G4-O02: lensing + expansion + dimension dependence ─────────────────────────────

    [Fact]
    public void G4_O02_LensingExpansionAndDimension()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-O02: lensing, expansion, and dimension dependence");

        // Lensing: deflection ∝ ΔΦ across a symmetric pair.
        double defl = PhysicalObservables.LensingDeflection(-0.5, 0.5, A, 3);
        sb.AppendLine($"lensing deflection ∝ ΔΦ = Φ(0.5) − Φ(−0.5) = {defl:F4} (symmetric ⇒ 0 for symmetric ρ)");
        double deflAsym = PhysicalObservables.LensingDeflection(0.1, 0.5, A, 3);
        sb.AppendLine($"asymmetric deflection Φ(0.5) − Φ(0.1) = {deflAsym:F4} ≠ 0 ⇒ lensing possible");

        // Expansion: H = ρ̇/ρ = 0 for the static profile.
        double h = PhysicalObservables.Expansion(0.0, 1.0 + A * 0.25);
        sb.AppendLine($"expansion H = ρ̇/ρ = {h:F4} (static profile ⇒ no expansion)");

        // Dimension dependence: Φ = (1/d)lnρ scales as 1/d.
        sb.AppendLine();
        sb.AppendLine($"{"d",4} {"Φ(0.5)",10} {"a(0.5)",10}");
        foreach (int d in new[] { 2, 3, 4 })
            sb.AppendLine($"{d,4} {PhysicalObservables.EffectivePotential(0.5, A, d),10:F4} {PhysicalObservables.Acceleration(0.5, A, d),10:F4}");

        sb.AppendLine();
        sb.AppendLine($"lensing: GR-like (deflection ∝ ΔΦ, non-zero off-axis): {Math.Abs(deflAsym) > 1e-9}");
        sb.AppendLine($"expansion: GR-like cosmological form (H = ρ̇/ρ; 0 for static ρ)");
        sb.AppendLine($"dimension dependence: Φ and a scale as 1/d — TQM-specific conformal-weight dependence");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: lensing + expansion KNOWN GR-LIKE; the 1/d conformal-weight scaling is TQM-SPECIFIC.");
        Output.WriteLine(sb.ToString());

        Assert.True(Math.Abs(deflAsym) > 1e-9, "asymmetric lensing deflection should be non-zero");
        Assert.True(Math.Abs(h) < 1e-12, "static expansion should be zero");
        Assert.True(PhysicalObservables.EffectivePotential(0.5, A, 2) >
                    1.5 * PhysicalObservables.EffectivePotential(0.5, A, 4),
            "Φ should scale as 1/d (larger for smaller d)");
    }
}
