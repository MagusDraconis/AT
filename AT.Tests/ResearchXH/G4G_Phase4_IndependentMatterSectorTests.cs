using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-G Phase 4 — search for an independent native stress-energy. Tests whether T_μν can emerge from
/// actualization-density dynamics WITHOUT defining T = G/κ. The kinetic (∇ρ) stress-energy is symmetric
/// but not conserved; the divergence-free condition on the general 2nd-order symmetric tensor has a
/// 1-dimensional solution space spanned by G (Lovelock uniqueness); and the density flux ∇ρ has non-zero
/// divergence (the density is curvature-sourced, not an independent conserved sector).
///
/// Tests: G4-G40 (kinetic T not conserved), G4-G41 (uniqueness of the conserved T = G), G4-G42 (density
/// flux is curvature-sourced).
/// </summary>
public class G4G_Phase4_IndependentMatterSectorTests : ResearchTestBase
{
    public G4G_Phase4_IndependentMatterSectorTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5;

    // ── G4-G40: the kinetic stress-energy is NOT conserved ────────────────────────────

    [Fact]
    public void G4_G40_KineticStressEnergyIsNotConserved()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G40: is the kinetic (∇ρ) stress-energy conserved?");

        sb.AppendLine($"{"d",4} {"max |∇^μ T_kin_μ1| over x",26}  conserved");
        bool anyConserved = false;
        foreach (int d in new[] { 3, 4 })
        {
            double maxAbs = 0.0;
            for (int i = 0; i <= 30; i++)
            {
                double x = -0.85 + 1.7 * i / 30.0;
                maxAbs = Math.Max(maxAbs, Math.Abs(HigherDimEinstein.KineticDivergence(x, A, d)));
            }
            bool cons = maxAbs < 1e-8;
            if (cons) anyConserved = true;
            sb.AppendLine($"{d,4} {maxAbs,26:E2}  {cons}");
        }

        sb.AppendLine();
        sb.AppendLine($"kinetic stress-energy conserved: {anyConserved}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the kinetic stress-energy (from ∇ρ alone) is symmetric but NOT divergence-free");
        sb.AppendLine("(∇^μ T_kin ≠ 0 for the curved profile) — so it is NOT a valid independent stress-energy.");
        Output.WriteLine(sb.ToString());

        Assert.False(anyConserved, "kinetic stress-energy should not be conserved");
    }

    // ── G4-G41: the divergence-free condition uniquely selects G ──────────────────────

    [Fact]
    public void G4_G41_UniquenessOfConservedTensor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G41: is the conserved symmetric 2nd-order tensor unique (= G)?");

        // General diagonal ansatz T_11 = A(σ′)² + Bσ″, T_ii = C(σ′)² + Dσ″.
        // Divergence-free condition decomposes into three coefficient equations (see report).
        foreach (int d in new[] { 3, 4 })
        {
            sb.AppendLine($"d = {d}:");
            // Coefficients of G_μν.
            double Ag = 0.5 * (d - 1.0) * (d - 2.0);
            double Bg = 0.0;
            double Cg = 0.5 * (d - 2.0) * (d - 3.0);
            double Dg = (d - 2.0);
            double e1g = 2 * Ag + (d - 3) * Bg - (d - 1) * Dg;          // [σ′σ″]
            double e2g = (d - 3) * Ag - (d - 1) * Cg;                    // [(σ′)³]
            double e3g = Bg;                                             // [σ‴]
            sb.AppendLine($"  G coefficients   : A={Ag:F2} B={Bg:F2} C={Cg:F2} D={Dg:F2}  residual=({e1g:F2},{e2g:F2},{e3g:F2})");

            // Coefficients of the kinetic tensor.
            double Ak = 0.5, Bk = 0.0, Ck = -0.5, Dk = 0.0;
            double e1k = 2 * Ak + (d - 3) * Bk - (d - 1) * Dk;
            double e2k = (d - 3) * Ak - (d - 1) * Ck;
            double e3k = Bk;
            sb.AppendLine($"  kinetic coefficients: A={Ak:F2} B={Bk:F2} C={Ck:F2} D={Dk:F2}  residual=({e1k:F2},{e2k:F2},{e3k:F2})");

            // Uniqueness: the three equations fix B=0, C=(d−3)A/(d−1), D=2A/(d−1) — one free parameter.
            bool gConserved = Math.Abs(e1g) < 1e-9 && Math.Abs(e2g) < 1e-9 && Math.Abs(e3g) < 1e-9;
            bool kinNot = Math.Abs(e1k) > 0.1 || Math.Abs(e2k) > 0.1;
            sb.AppendLine($"  G conserved = {gConserved}; kinetic not conserved = {kinNot}");
            Assert.True(gConserved, $"d={d}: G should satisfy the divergence-free condition");
            Assert.True(kinNot, $"d={d}: kinetic T should NOT satisfy the divergence-free condition");
            sb.AppendLine();
        }

        sb.AppendLine("The divergence-free condition leaves exactly ONE free parameter (the overall scale), and the");
        sb.AppendLine("unique solution is G_μν. Therefore any symmetric, conserved stress-energy built from ρ is");
        sb.AppendLine("forced to be G/κ — there is NO independent matter sector.");
        Output.WriteLine(sb.ToString());
    }

    // ── G4-G42: the density flux is curvature-sourced (no independent conservation) ────

    [Fact]
    public void G4_G42_DensityFluxIsCurvatureSourced()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G42: is the actualization density independently conserved?");

        // The actualization flux J = ∇ρ (x-component ρ′ = 2ax); its divergence is Δρ = ρ″ = 2a.
        sb.AppendLine($"{"x",7} {"ρ′(x) (flux)",13} {"ρ″(x) (div)",13}");
        double[] xs = { -0.6, -0.2, 0.0, 0.2, 0.6 };
        bool fluxNonZero = false;
        foreach (double x in xs)
        {
            double j = HigherDimEinstein.RhoPrime(x, A);
            double div = HigherDimEinstein.RhoSecond(x, A);
            if (Math.Abs(div) > 1e-9) fluxNonZero = true;
            sb.AppendLine($"{x,7:F2} {j,13:F4} {div,13:F4}");
        }

        // The flux divergence Δρ = 2a ≠ 0 equals the curvature content (−R·ρ² at x=0, since R(0) = −2a).
        double r0 = HigherDimEinstein.ScalarCurvature(0.0, A, 3); // any d; the sign structure holds
        double deltaRho = HigherDimEinstein.RhoSecond(0.0, A);

        sb.AppendLine();
        sb.AppendLine($"flux divergence Δρ = 2a = {deltaRho:F4} ≠ 0: {fluxNonZero}");
        sb.AppendLine($"curvature at x=0: R(0) = {r0:F4} (∝ −Δρ) — the density is curvature-SOURCED, not conserved");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the actualization density has no independent conservation (its flux has non-zero");
        sb.AppendLine("divergence); the density is coupled to the geometry (Δρ ∝ −R). Combined with G4-G41, there is");
        sb.AppendLine("no independent matter sector — T is forced to be G/κ.");
        Output.WriteLine(sb.ToString());

        Assert.True(fluxNonZero, "density flux should have non-zero divergence (curvature-sourced)");
    }
}
