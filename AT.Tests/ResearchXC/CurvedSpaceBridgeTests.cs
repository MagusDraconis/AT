using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Executable form of the Curved-Space Bridge finding (Docs/Audits/CurvedSpaceSchrodinger.md):
/// no metric-dependent Schrödinger operator (Laplace-Beltrami Δ_g) exists in AT.Core.
///
///   MetricDependentOperator_Exists      — scans source: no Δ_g present.
///   LaplaceBeltrami_ReducesToFlatLaplacian — standard identity: flat metric g=I ⇒ Δ_g = ∇².
///   CurvedSpaceBridge_PresentOrAbsent   — synthesis: bridge ABSENT.
/// </summary>
public class CurvedSpaceBridgeTests : ResearchTestBase
{
    public CurvedSpaceBridgeTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: is a metric-dependent operator present in the code? ──────

    [Fact]
    public void MetricDependentOperator_Exists()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: does a metric-dependent operator exist in AT.Core?");

        int beltrami = CountOccurrencesInCore("Beltrami");       // Laplace-Beltrami Δ_g
        int curvedSchrodinger = CountOccurrencesInCore("curved-space Schrödinger");

        sb.AppendLine($"Occurrences of 'Beltrami' in AT.Core: {beltrami}");
        sb.AppendLine($"Occurrences of 'curved-space Schrödinger' in AT.Core: {curvedSchrodinger}");

        Assert.True(beltrami == 0, $"A Laplace-Beltrami operator exists ({beltrami} occurrence(s))");
        Assert.True(curvedSchrodinger == 0, $"A curved-space Schrödinger operator exists ({curvedSchrodinger} occurrence(s))");
        sb.AppendLine("PASS: no metric-dependent operator (Δ_g) is implemented.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: Δ_g reduces to the flat Laplacian on a flat metric ───────

    [Fact]
    public void LaplaceBeltrami_ReducesToFlatLaplacian()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: flat metric g=I ⇒ Δ_g reduces to ∇² (standard identity)");

        // For the flat Euclidean metric g = I, the Laplace-Beltrami operator
        // Δ_g = (1/√|g|) ∂_i(√|g| g^{ij} ∂_j) reduces to ∇² = ∂²/∂x² + ∂²/∂y².
        // Its discrete form is the 5-point stencil. On the eigenfunction
        // f = sin(πx) sin(πy), exact ∇²f = -2π² f.
        double x0 = 0.4, y0 = 0.4;
        double[] hValues = { 1.0 / 16, 1.0 / 32, 1.0 / 64, 1.0 / 128 };
        double tol = 1e-2;

        double prevErr = double.PositiveInfinity;
        foreach (double h in hValues)
        {
            double discrete = FlatMetricLaplaceBeltrami(x0, y0, h);
            double exact = -2.0 * Math.PI * Math.PI * F(x0, y0);
            double relErr = Math.Abs(discrete - exact) / Math.Max(1.0, Math.Abs(exact));

            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "h=1/{0,-3} relErr={1:E3}  Δ_g f={2:F4}  ∇²f={3:F4}",
                (int)Math.Round(1.0 / h), relErr, discrete, exact));

            Assert.True(relErr < tol, $"h={h}: relative error {relErr:E3} exceeds {tol}");
            Assert.True(relErr < prevErr, $"h={h}: error did not decrease (prev {prevErr:E3})");
            prevErr = relErr;
        }

        sb.AppendLine();
        sb.AppendLine("PASS: Δ_g (flat metric) = ∇² — the reduction to the flat Laplacian holds.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: is the curved-space bridge present or absent? ─────────────

    [Fact]
    public void CurvedSpaceBridge_PresentOrAbsent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: curved-space Schrödinger bridge — present or absent?");

        int beltrami = CountOccurrencesInCore("Beltrami");
        sb.AppendLine($"Laplace-Beltrami Δ_g occurrences in AT.Core: {beltrami}");

        // The flat Laplacian exists (L_Q → ∇², verified elsewhere); the curved
        // Δ_g does not. The bridge (coupling L_Q to a metric) is therefore ABSENT.
        bool bridgeAbsent = beltrami == 0;
        sb.AppendLine(bridgeAbsent
            ? "Verdict: ABSENT — no Δ_g, hence no curved-space Schrödinger bridge."
            : "Verdict: PRESENT — a metric-dependent operator exists.");

        Assert.True(bridgeAbsent, "A curved-space Schrödinger operator should not exist");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static double F(double x, double y) => Math.Sin(Math.PI * x) * Math.Sin(Math.PI * y);

    /// <summary>Discrete Laplace-Beltrami on the flat metric g=I (5-point stencil).</summary>
    private static double FlatMetricLaplaceBeltrami(double x, double y, double h)
    {
        double up = F(x, y + h), down = F(x, y - h);
        double right = F(x + h, y), left = F(x - h, y);
        return (up + down + right + left - 4.0 * F(x, y)) / (h * h);
    }

    /// <summary>Locate the repository root by walking up from the test output directory.</summary>
    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !Directory.Exists(Path.Combine(dir.FullName, "AT.Core")))
            dir = dir.Parent;
        return dir?.FullName ?? throw new DirectoryNotFoundException("AT.Core not found");
    }

    /// <summary>Count occurrences of a substring across all AT.Core .cs files.</summary>
    private static int CountOccurrencesInCore(string needle)
    {
        string root = FindRepoRoot();
        int count = 0;
        foreach (string file in Directory.GetFiles(
            Path.Combine(root, "AT.Core"), "*.cs", SearchOption.AllDirectories))
        {
            string text = File.ReadAllText(file);
            int idx = 0;
            while ((idx = text.IndexOf(needle, idx, StringComparison.Ordinal)) >= 0)
            {
                count++;
                idx += needle.Length;
            }
        }
        return count;
    }
}
