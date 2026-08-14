using System.Globalization;
using System.Text;
using TQM.Core.ResearchXC;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Determines whether the standard tested chain (g → Γ → Riemann → Ricci → G) can be
/// integrated into existing TQM analyzers. Tests the minimal builder
/// <see cref="EinsteinTensorBuilder"/> (pure differential geometry) that would be inserted.
/// No new physics — only standard Riemannian geometry in local coordinates.
/// </summary>
public class EinsteinTensorIntegrationTests : ResearchTestBase
{
    public EinsteinTensorIntegrationTests(ITestOutputHelper o) : base(o) { }

    private const double H = 1e-4;

    // ── Test 1: ChristoffelBuilder — flat metric ───────────────────────────

    [Fact]
    public void ChristoffelBuilder_ComputesFlatMetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: ChristoffelBuilder — flat metric (g = diag(1,1))");

        double[] x = { 1.3, 0.7 };
        EinsteinTensorBuilder.MetricField flat = p => new[,] { { 1.0, 0.0 }, { 0.0, 1.0 } };

        var G = EinsteinTensorBuilder.Christoffel(flat, x, H);
        double max = MaxAbs3(G);

        sb.AppendLine($"flat:  max |Γ^λ_μν| = {max:E3}");

        Assert.True(max < 1e-9, $"flat metric has nonzero Christoffels ({max:E3})");
        sb.AppendLine("PASS: flat metric → all Christoffel symbols vanish (standard).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: ChristoffelBuilder — unit 2-sphere ─────────────────────────

    [Fact]
    public void ChristoffelBuilder_ComputesSphereMetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: ChristoffelBuilder — unit 2-sphere (g = diag(1, sin²θ))");

        double theta = Math.PI / 4;
        double[] x = { theta, 0.0 };
        EinsteinTensorBuilder.MetricField sphere = p => new[,]
        {
            { 1.0, 0.0 },
            { 0.0, Math.Sin(p[0]) * Math.Sin(p[0]) },
        };

        var G = EinsteinTensorBuilder.Christoffel(sphere, x, H);
        // Γ^θ_φφ = −sinθ·cosθ = −0.5 at θ=π/4.
        double Gtpp = G[0, 1, 1];

        sb.AppendLine($"sphere: Γ^θ_φφ = {Gtpp:F4}  (expect −sinθcosθ = −0.5000)");

        Assert.True(Math.Abs(Gtpp + 0.5) < 1e-3, $"sphere Γ^θ_φφ = {Gtpp:F4} != −0.5");
        sb.AppendLine("PASS: sphere metric → correct Christoffel symbol (standard).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: RicciTensor — known examples ───────────────────────────────

    [Fact]
    public void RicciTensor_ComputesKnownExamples()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: RicciTensor — flat vs unit 2-sphere (R_μν = K·g_μν)");

        double theta = Math.PI / 4;
        double[] x = { theta, 0.0 };
        EinsteinTensorBuilder.MetricField flat = p => new[,] { { 1.0, 0.0 }, { 0.0, 1.0 } };
        EinsteinTensorBuilder.MetricField sphere = p => new[,]
        {
            { 1.0, 0.0 },
            { 0.0, Math.Sin(p[0]) * Math.Sin(p[0]) },
        };

        var flatRicci = EinsteinTensorBuilder.Ricci(flat, x, H);
        var sphRicci = EinsteinTensorBuilder.Ricci(sphere, x, H);
        double flatR = EinsteinTensorBuilder.RicciScalar(flat, x, H);
        double sphR = EinsteinTensorBuilder.RicciScalar(sphere, x, H);

        sb.AppendLine($"flat:  R_μν max = {MaxAbs2(flatRicci):E3}, R = {flatR:E3}");
        sb.AppendLine($"sphere: R_θθ = {sphRicci[0, 0]:F4} (expect 1), R_φφ = {sphRicci[1, 1]:F4} (expect 0.5), R = {sphR:F4} (expect 2)");

        Assert.True(MaxAbs2(flatRicci) < 1e-8, $"flat metric has nonzero Ricci ({MaxAbs2(flatRicci):E3})");
        Assert.True(Math.Abs(sphRicci[0, 0] - 1.0) < 1e-3, $"sphere R_θθ = {sphRicci[0, 0]:F4} != 1");
        Assert.True(Math.Abs(sphRicci[1, 1] - 0.5) < 1e-3, $"sphere R_φφ = {sphRicci[1, 1]:F4} != 0.5");
        Assert.True(Math.Abs(sphR - 2.0) < 2e-3, $"sphere R = {sphR:F4} != 2");
        sb.AppendLine("PASS: Ricci tensor follows from the metric (flat ⇒ 0, sphere ⇒ K·g_μν).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 4: EinsteinTensor — known examples ────────────────────────────

    [Fact]
    public void EinsteinTensor_ComputesKnownExamples()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 4: EinsteinTensor — 2D (vanishes) vs unit 3-sphere (G = −g)");

        double t = Math.PI / 4;
        double[] x2 = { t, 0.0 };
        EinsteinTensorBuilder.MetricField sphere2 = p => new[,]
        {
            { 1.0, 0.0 },
            { 0.0, Math.Sin(p[0]) * Math.Sin(p[0]) },
        };

        // Unit 3-sphere: g = diag(1, sin²χ, sin²χ·sin²θ); at χ=θ=π/4 → (1, 0.5, 0.25).
        // R_μν = 2·g_μν, R = 6 ⇒ G_μν = −g_μν = diag(−1, −0.5, −0.25).
        double[] x3 = { t, t, 0.0 };
        EinsteinTensorBuilder.MetricField sphere3 = p => new[,]
        {
            { 1.0, 0.0, 0.0 },
            { 0.0, Math.Sin(p[0]) * Math.Sin(p[0]), 0.0 },
            { 0.0, 0.0, Math.Sin(p[0]) * Math.Sin(p[0]) * Math.Sin(p[1]) * Math.Sin(p[1]) },
        };

        var G2 = EinsteinTensorBuilder.Einstein(sphere2, x2, H);
        var G3 = EinsteinTensorBuilder.Einstein(sphere3, x3, H);

        sb.AppendLine($"2-sphere: max |G_μν| = {MaxAbs2(G2):E3}  (2D ⇒ identically zero)");
        sb.AppendLine($"3-sphere: G_χχ = {G3[0, 0]:F4} (expect −1), G_θθ = {G3[1, 1]:F4} (expect −0.5), G_φφ = {G3[2, 2]:F4} (expect −0.25)");

        Assert.True(MaxAbs2(G2) < 1e-3, $"2D Einstein tensor is nonzero ({MaxAbs2(G2):E3})");
        Assert.True(Math.Abs(G3[0, 0] + 1.0) < 2e-2, $"3-sphere G_χχ = {G3[0, 0]:F4} != −1");
        Assert.True(Math.Abs(G3[1, 1] + 0.5) < 2e-2, $"3-sphere G_θθ = {G3[1, 1]:F4} != −0.5");
        Assert.True(Math.Abs(G3[2, 2] + 0.25) < 2e-2, $"3-sphere G_φφ = {G3[2, 2]:F4} != −0.25");
        sb.AppendLine("PASS: Einstein tensor computed correctly — 2D vanishes, 3-sphere gives G = −g (non-trivial).");
        sb.AppendLine("NOTE: a NON-trivial G_μν requires dimension ≥ 3 — this is the minimal working chain.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static double MaxAbs3(double[,,] a)
    {
        double m = 0;
        for (int i = 0; i < a.GetLength(0); i++)
        for (int j = 0; j < a.GetLength(1); j++)
        for (int k = 0; k < a.GetLength(2); k++)
            m = Math.Max(m, Math.Abs(a[i, j, k]));
        return m;
    }

    private static double MaxAbs2(double[,] a)
    {
        double m = 0;
        for (int i = 0; i < a.GetLength(0); i++)
        for (int j = 0; j < a.GetLength(1); j++)
            m = Math.Max(m, Math.Abs(a[i, j]));
        return m;
    }
}
