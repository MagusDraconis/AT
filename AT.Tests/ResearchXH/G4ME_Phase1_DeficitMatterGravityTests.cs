using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-ME Phase 1 — does the deficit matter reproduce Newton-like attraction? Tests the derived matter
/// m = ρ̄−ρ across Gaussian / spherical / multiple / extended deficits, measuring acceleration sign,
/// radial falloff, effective mass profile, and superposition, compared to Newtonian expectation.
///
/// Tests: G4-ME10 (Gaussian deficit sign + falloff), G4-ME11 (spherical + superposition),
///        G4-ME12 (extended halo + classification).
/// </summary>
public class G4ME_Phase1_DeficitMatterGravityTests : ResearchTestBase
{
    public G4ME_Phase1_DeficitMatterGravityTests(ITestOutputHelper o) : base(o) { }

    private const int D = 3;

    private static double AtAcc(Func<double, double> rho, double x) => PhysicalObservables.AtAcceleration(rho, x, D);

    // ── G4-ME10: Gaussian deficit — attractive sign, falloff vs Newton ─────────────────

    [Fact]
    public void G4_ME10_GaussianDeficitAttractiveFalloff()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME10: Gaussian deficit — attractive sign and radial falloff");

        sb.AppendLine($"{"x",7} {"a_AT",9} {"a_Newton",9}");
        double[] xs = { 0.2, 0.4, 0.6, 0.8, 1.0 };
        bool attractive = true;
        var ratios = new List<double>();
        foreach (double x in xs)
        {
            double aT = AtAcc(u => PhysicalObservables.Void(u), x);
            double aN = PhysicalObservables.NewtonianDeficitAcceleration(u => PhysicalObservables.Void(u), x);
            if (!(aT < 0)) attractive = false;
            ratios.Add(Math.Abs(aT / aN));
            sb.AppendLine($"{x,7:F2} {aT,9:F4} {aN,9:F4}");
        }

        // Falloff: AT is localized (∝ ∇m, exponential), Newton is long-range (∝ −∫m, → constant).
        bool localized = ratios[^1] < 0.5 * ratios[0]; // ratio |a_AT/a_Newton| shrinks outward

        sb.AppendLine();
        sb.AppendLine($"attractive (a_AT < 0) everywhere: {attractive}");
        sb.AppendLine($"AT falloff localized (|a_AT/a_Newton| shrinks outward): {localized}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deficit matter has the correct ATTRACTIVE sign, but its field is LOCALIZED");
        sb.AppendLine("(∝ ∇m, exponential) — it does NOT reproduce Newton's long-range 1/r² falloff.");
        Output.WriteLine(sb.ToString());

        Assert.True(attractive, "Gaussian deficit should be attractive");
        Assert.True(localized, "AT deficit field should fall off faster than Newton");
    }

    // ── G4-ME11: spherical deficit + superposition ────────────────────────────────────

    [Fact]
    public void G4_ME11_SphericalDeficitAndSuperposition()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME11: spherical deficit + multiple-deficit superposition");

        // Spherical deficit: no field inside (∇m=0) or outside (∇m=0), only a boundary kick.
        double xIn = 0.2, xOut = 0.8;
        double aIn = AtAcc(u => PhysicalObservables.SphericalDeficit(u), xIn);
        double aOut = AtAcc(u => PhysicalObservables.SphericalDeficit(u), xOut);
        sb.AppendLine($"spherical deficit: a_inside(x={xIn}) = {aIn:E2}, a_outside(x={xOut}) = {aOut:E2} (both ≈ 0)");

        // Multiple deficits: two voids attract test particles toward their nearest void (localized).
        double aMulti1 = AtAcc(u => 1.0 - 0.4 * Math.Exp(-Math.Pow(u + 0.4, 2) / 0.09) - 0.4 * Math.Exp(-Math.Pow(u - 0.4, 2) / 0.09), 0.7);
        double aMulti2 = AtAcc(u => 1.0 - 0.4 * Math.Exp(-Math.Pow(u + 0.4, 2) / 0.09) - 0.4 * Math.Exp(-Math.Pow(u - 0.4, 2) / 0.09), 0.2);
        sb.AppendLine($"two deficits: a(0.7) = {aMulti1:F4} (attracted left toward void at +0.4), a(0.2) = {aMulti2:F4} (attracted right toward void at +0.4)");

        bool sphericalLocalized = Math.Abs(aIn) < 1e-3 && Math.Abs(aOut) < 1e-3;
        bool superposes = aMulti1 < 0 && aMulti2 > 0; // each test point attracted toward its nearest void

        sb.AppendLine();
        sb.AppendLine($"spherical deficit localized (no 1/r² exterior): {sphericalLocalized}");
        sb.AppendLine($"multiple deficits attract locally (superposition): {superposes}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the deficit field is LOCALIZED (no long-range exterior), and multiple deficits");
        sb.AppendLine("superimpose locally — attractive, but NOT Newtonian 1/r².");
        Output.WriteLine(sb.ToString());

        Assert.True(sphericalLocalized, "spherical deficit should have no interior/exterior field");
        Assert.True(superposes, "multiple deficits should attract locally");
    }

    // ── G4-ME12: extended halo + classification ───────────────────────────────────────

    [Fact]
    public void G4_ME12_ExtendedHaloAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-ME12: extended deficit halo + classification");

        // Extended (exponential) deficit halo ρ = 1 − A e^(−|x|/r_d).
        double x1 = 0.3, x2 = 1.0;
        double a1 = AtAcc(u => 1.0 - 0.4 * Math.Exp(-Math.Abs(u) / 0.4), x1);
        double a2 = AtAcc(u => 1.0 - 0.4 * Math.Exp(-Math.Abs(u) / 0.4), x2);
        double aN1 = PhysicalObservables.NewtonianDeficitAcceleration(u => 1.0 - 0.4 * Math.Exp(-Math.Abs(u) / 0.4), x1);
        double aN2 = PhysicalObservables.NewtonianDeficitAcceleration(u => 1.0 - 0.4 * Math.Exp(-Math.Abs(u) / 0.4), x2);
        sb.AppendLine($"extended halo: a_AT(0.3)={a1:F4}, a_AT(1.0)={a2:F4}; a_Newton(0.3)={aN1:F4}, a_Newton(1.0)={aN2:F4}");
        sb.AppendLine($"attractive (a_AT<0): {a1 < 0 && a2 < 0}");

        // The sign is correct but the effective-mass profile differs: AT ∝ ∇m (local), Newton ∝ ∫m (global).
        bool signOk = a1 < 0 && a2 < 0;

        sb.AppendLine();
        sb.AppendLine($"effective mass profile: AT ∝ ∇m (local gradient), Newton ∝ ∫m (enclosed) — different");
        sb.AppendLine($"superposition: local (correct sign) but not long-range");
        sb.AppendLine();
        sb.AppendLine("CLASSIFICATION: PARTIAL MATCH — the derived deficit matter is ATTRACTIVE (correct sign) and");
        sb.AppendLine("superposes locally, but it does NOT reproduce Newton's long-range 1/r² falloff (the field is");
        sb.AppendLine("localized ∝ ∇m). Full Newtonian gravity would require an additional non-conformal sector.");
        Output.WriteLine(sb.ToString());

        Assert.True(signOk, "extended halo should be attractive");
    }
}
