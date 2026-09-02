using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Executable form of the Curved-Space Bridge finding (Docs/Audits/CurvedSpaceSchrodinger.md)
/// as REFINED by the ResearchXH G4-C program (native conformal operator) and the ResearchXC
/// weighted-Laplacian program:
///
///   * AT now implements Laplace-Beltrami-*like* operators — the native conformal operator
///     Lc = ρ⁻¹ L ρ⁻¹ ≈ −Δ_g (ConformalOperator.RhoInverseSquared, G4C) and the weighted
///     discrete Laplace-Beltrami L_W = D_K − K (TemporalMatrix.BuildWeightedLaplacian).
///   * They are NATIVE: built from density ρ / coupling K, never from an imported metric
///     tensor g_μν as a coefficient.
///   * No curved-space operator is constructed by importing a metric tensor.
///
///   CurvedOperator_NativeNotMetricImported — source scan: no Laplace-Beltrami/curved
///       operator file consumes an imported metric (MetricField).
///   LaplaceBeltrami_ReducesToFlatLaplacian — standard identity: flat metric g=I ⇒ Δ_g = ∇².
///   CurvedSpaceBridge_PresentOrAbsent      — synthesis: bridge PRESENT natively,
///       ABSENT as a metric-tensor import.
/// </summary>
public class CurvedSpaceBridgeTests : ResearchTestBase
{
    public CurvedSpaceBridgeTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: is the curved operator native, or imported from a metric tensor? ──

    [Fact]
    public void CurvedOperator_NativeNotMetricImported()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: is the Laplace-Beltrami/curved operator native, or metric-imported?");

        // The native operators are present in AT.Core (superseding the original audit's
        // "0 occurrences" finding): the conformal operator ρ⁻¹Lρ⁻¹ ≈ −Δ_g and the weighted
        // discrete Laplace-Beltrami L_W = D_K − K.
        int conformal = CountOccurrencesInCore("RhoInverseSquared");
        int weightedLB = CountOccurrencesInCore("BuildWeightedLaplacian");

        // No file that references a Laplace-Beltrami/curved operator consumes an imported
        // metric tensor (MetricField). EinsteinTensorBuilder takes a MetricField but is the
        // pure-differential-geometry Einstein-reconstruction utility, not a curved-space
        // Schrödinger/Laplace-Beltrami operator.
        var curvedFiles = FilesInCoreContaining("Beltrami");
        int curvedWithMetricImport = 0;
        foreach (var f in curvedFiles)
        {
            string text = System.IO.File.ReadAllText(f);
            if (text.Contains("MetricField", StringComparison.Ordinal)) curvedWithMetricImport++;
        }

        sb.AppendLine($"Native conformal operator ρ⁻¹Lρ⁻¹ (RhoInverseSquared) in AT.Core: {conformal} occurrence(s)");
        sb.AppendLine($"Weighted discrete Laplace-Beltrami L_W (BuildWeightedLaplacian) in AT.Core: {weightedLB} occurrence(s)");
        sb.AppendLine($"Files referencing 'Beltrami' that ALSO consume an imported metric tensor: {curvedWithMetricImport}");

        Assert.True(conformal > 0, "native conformal operator ρ⁻¹Lρ⁻¹ ≈ −Δ_g should exist (G4C)");
        Assert.True(weightedLB > 0, "weighted discrete Laplace-Beltrami L_W should exist");
        Assert.True(curvedWithMetricImport == 0,
            $"A Laplace-Beltrami/curved operator imports a metric tensor ({curvedWithMetricImport} file(s))");
        sb.AppendLine("PASS: the curved-space operator is NATIVE (ρ/coupling); no metric tensor is imported as a coefficient.");
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

        // Native conformal Laplace-Beltrami-like operator (G4C): ρ⁻¹Lρ⁻¹ ≈ −Δ_g.
        int conformal = CountOccurrencesInCore("RhoInverseSquared");
        // Weighted discrete Laplace-Beltrami L_W = D_K − K.
        int weightedLB = CountOccurrencesInCore("BuildWeightedLaplacian");

        // The bridge is PRESENT in native form (density/coupling), ABSENT as a metric import.
        bool bridgePresentNatively = conformal > 0 || weightedLB > 0;
        bool bridgePresentViaMetricImport = FilesInCoreContaining("Beltrami")
            .Any(f => System.IO.File.ReadAllText(f).Contains("MetricField", StringComparison.Ordinal));

        sb.AppendLine($"Native conformal operator ρ⁻¹Lρ⁻¹ (RhoInverseSquared): {conformal} occurrence(s)");
        sb.AppendLine($"Weighted discrete Laplace-Beltrami L_W (BuildWeightedLaplacian): {weightedLB} occurrence(s)");
        sb.AppendLine(bridgePresentNatively
            ? "Verdict: PRESENT (native) — AT implements a Laplace-Beltrami-*like* operator built from ρ/coupling (G4C, weighted-Laplacian programs)."
            : "Verdict: ABSENT — no Laplace-Beltrami-like operator.");
        sb.AppendLine(bridgePresentViaMetricImport
            ? "Metric-tensor import: PRESENT (contradiction — a curved operator imports g_μν)."
            : "Metric-tensor import: ABSENT — the curved operator is native (no imported g_μν coefficient).");

        Assert.True(bridgePresentNatively,
            "AT should implement a native Laplace-Beltrami-like operator (conformal ρ⁻¹Lρ⁻¹ and/or weighted L_W)");
        Assert.False(bridgePresentViaMetricImport,
            "A curved-space operator must not be built from an imported metric tensor (MetricField)");
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

    /// <summary>All AT.Core .cs files containing the given substring.</summary>
    private static string[] FilesInCoreContaining(string needle)
        => Directory.GetFiles(Path.Combine(FindRepoRoot(), "AT.Core"), "*.cs", SearchOption.AllDirectories)
            .Where(f => System.IO.File.ReadAllText(f).Contains(needle, StringComparison.Ordinal))
            .ToArray();

    /// <summary>Count occurrences of a substring across all AT.Core .cs files.</summary>
    private static int CountOccurrencesInCore(string needle)
    {
        int count = 0;
        foreach (string file in FilesInCoreContaining(needle))
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
