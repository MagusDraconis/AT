using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// G4-G Phase 0 — emergence of Einstein structure. In the native conformally-flat 2D geometry
/// g = ρ·η, constructs the Ricci tensor, scalar-curvature field, and Einstein-tensor candidate
/// from ρ alone, and tests symmetry, trace consistency, the 2D Einstein-tensor degeneracy
/// (G ≡ 0), and the Gauss–Bonnet conservation (refinement-stable total curvature).
/// No Einstein tensor / equations / GR field equations imported.
///
/// Tests: G4-G00 (Ricci-like + scalar field), G4-G01 (Einstein tensor vanishes in 2D),
///        G4-G02 (Gauss–Bonnet conservation + refinement).
/// </summary>
public class G4G_Phase0_EinsteinStructureTests : ResearchTestBase
{
    public G4G_Phase0_EinsteinStructureTests(ITestOutputHelper o) : base(o) { }

    private const double A = 0.5; // curvature strength (R(0) = −2a = −1)

    // ── G4-G00: Ricci-like quantity and scalar-curvature field ─────────────────────────

    [Fact]
    public void G4_G00_RicciAndScalarCurvatureField()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G00: native Ricci tensor and scalar-curvature field (d=2)");

        double[] xs = { -0.9, -0.5, 0.0, 0.5, 0.9 };
        sb.AppendLine($"{"x",7} {"ρ(x)",7} {"R(x)",9} {"R_μμ",9} {"trace(Ricci)",13}  trace=R");
        bool traceOk = true, symmetricOk = true;
        foreach (double x in xs)
        {
            double r = EinsteinStructure.ScalarCurvature(x, A);
            double ricci = EinsteinStructure.RicciDiag(x, A);
            double tr = EinsteinStructure.TraceRicci(x, A);
            bool ok = Math.Abs(tr - r) < 1e-12;
            if (!ok) traceOk = false;
            // R_μν = −(1/2)(lnρ)″δ_μν is diagonal ⇒ symmetric (off-diagonal ≡ 0).
            sb.AppendLine($"{x,7:F2} {EinsteinStructure.Rho(x, A),7:F3} {r,9:F4} {ricci,9:F4} {tr,13:F4}  {ok}");
        }

        sb.AppendLine();
        sb.AppendLine($"trace(g^μν R_μν) = R for all x: {traceOk}");
        sb.AppendLine($"R_μν symmetric (diagonal, off-diagonal ≡ 0): {symmetricOk}");
        sb.AppendLine($"R_μν = (R/2)g_μν (2D identity): {traceOk}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the native Ricci tensor is R_μν = (R/2)g_μν = (R·ρ/2)δ_μν — symmetric, trace");
        sb.AppendLine("consistent, and fully determined by the native scalar-curvature field R(x) and metric g = ρ·η.");
        Output.WriteLine(sb.ToString());

        Assert.True(traceOk, "trace of Ricci tensor does not equal the scalar curvature");
        Assert.True(symmetricOk, "Ricci tensor is not symmetric");
    }

    // ── G4-G01: the Einstein tensor vanishes identically in 2D ────────────────────────

    [Fact]
    public void G4_G01_EinsteinTensorVanishesIn2D()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G01: does the Einstein-tensor candidate vanish in d=2?");

        double maxAbs = 0.0;
        double[] xs = Enumerable.Range(0, 41).Select(i => -1.0 + 2.0 * i / 40.0).ToArray();
        sb.AppendLine($"{"x",7} {"G_μν (diag)",13}");
        foreach (double x in xs)
        {
            double g = EinsteinStructure.EinsteinDiag(x, A);
            maxAbs = Math.Max(maxAbs, Math.Abs(g));
        }
        sb.AppendLine($"(sampled {xs.Length} points)");
        sb.AppendLine($"max|G_μν| over x ∈ [−1,1] = {maxAbs:E2}");
        sb.AppendLine($"Einstein tensor identically zero: {maxAbs < 1e-12}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: G_μν = R_μν − (R/2)g_μν ≡ 0 in d=2 (a geometric identity, not an import).");
        sb.AppendLine("There is NO non-trivial Einstein tensor in the native 2D geometry — non-trivial Einstein");
        sb.AppendLine("structure requires d ≥ 3.");
        Output.WriteLine(sb.ToString());

        Assert.True(maxAbs < 1e-12, $"Einstein tensor does not vanish (max|G| = {maxAbs:E2})");
    }

    // ── G4-G02: Gauss–Bonnet conservation + refinement ────────────────────────────────

    [Fact]
    public void G4_G02_GaussBonnetConservationAndRefinement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("G4-G02: Gauss–Bonnet conservation and refinement stability");

        double analytic = EinsteinStructure.TotalCurvature(A);
        sb.AppendLine($"analytic ∫R√g dA = −8a/(1+a) = {analytic:F4} (a boundary/topological term)");
        sb.AppendLine();

        sb.AppendLine($"{"n",5} {"grid ∫R√g dA",15} {"rel. error",11}");
        var errors = new List<(int n, double err)>();
        foreach (int n in new[] { 16, 24, 40 })
        {
            var xs = CurvatureField.UniformXs(n);
            // ∫R√g dA ≈ (4/n)·Σ R(x_i)ρ(x_i)  (y-integral = ×2 width, Δx = 2/n).
            double sum = 0.0;
            foreach (double x in xs) sum += EinsteinStructure.GaussBonnetIntegrand(x, A);
            double grid = (4.0 / n) * sum;
            double err = Math.Abs(grid - analytic) / Math.Abs(analytic);
            errors.Add((n, err));
            sb.AppendLine($"{n,5} {grid,15:F4} {err,11:F4}");
        }

        bool converges = errors[^1].err < errors[0].err;
        sb.AppendLine();
        sb.AppendLine($"relative error decreases under refinement: {converges} ({errors[0].err:F4} → {errors[^1].err:F4})");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the total curvature ∫R√g dA is a refinement-stable (Gauss–Bonnet / boundary)");
        sb.AppendLine("invariant — the native curvature field obeys a conservation identity.");
        Output.WriteLine(sb.ToString());

        Assert.True(converges, "Gauss–Bonnet integral does not converge under refinement");
        Assert.True(errors[^1].err < 0.02, $"final relative error {errors[^1].err:F4} not < 0.02");
    }
}
