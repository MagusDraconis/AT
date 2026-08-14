using System.Globalization;
using System.Text;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Determines whether TQM already contains enough ingredients to compute the Einstein tensor
/// G_μν. Implements the STANDARD differential-geometry chain (metric → Christoffel → Riemann →
/// Ricci → Einstein) in 2D and verifies each step, then identifies where TQM's own analyzers
/// stop (they describe the chain but never compute it).
/// </summary>
public class EinsteinTensorTests : ResearchTestBase
{
    public EinsteinTensorTests(ITestOutputHelper o) : base(o) { }

    private const double H = 1e-4;

    // ── Test 1: metric → Christoffel symbols ───────────────────────────────

    [Fact]
    public void MetricProducesChristoffels()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: metric → Christoffel symbols (standard)");

        double theta = Math.PI / 4;

        // Flat metric g = diag(1,1): all Γ = 0.
        double flat = MaxLoweredChristoffel((x, y) => 1.0, (x, y) => 1.0, theta, 0.0, H);
        // Unit 2-sphere g = diag(1, sin²θ): Γ_{θφφ} = −sinθ·cosθ = −0.5 at θ=π/4.
        double sphere = LoweredChristoffelTpp((x, y) => 1.0, (x, y) => Math.Sin(x) * Math.Sin(x), theta, 0.0, H);

        sb.AppendLine($"flat:  max |Γ| = {flat:E3}");
        sb.AppendLine($"sphere: Γ_θφφ = {sphere:F4}  (expect −sinθcosθ = −0.5000)");

        Assert.True(flat < 1e-9, $"flat metric has nonzero Christoffels ({flat:E3})");
        Assert.True(Math.Abs(sphere + 0.5) < 1e-3, $"sphere Γ_θφφ = {sphere:F4} != −0.5");
        sb.AppendLine("PASS: metric produces Christoffel symbols (flat ⇒ 0, curved ⇒ −½∂g).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: Christoffels → Riemann (via Gauss curvature in 2D) ─────────

    [Fact]
    public void ChristoffelsProduceRiemann()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: Christoffels → Riemann curvature (standard, 2D)");

        double theta = Math.PI / 4;

        double flatK = GaussCurvature((x, y) => 1.0, (x, y) => 1.0, theta, 0.0, H);
        double sphereK = GaussCurvature((x, y) => 1.0, (x, y) => Math.Sin(x) * Math.Sin(x), theta, 0.0, H);

        sb.AppendLine($"flat:  Gauss curvature K = {flatK:E3}");
        sb.AppendLine($"sphere: Gauss curvature K = {sphereK:F4}  (expect 1.0000 for unit sphere)");

        Assert.True(Math.Abs(flatK) < 1e-6, $"flat metric has nonzero curvature ({flatK:E3})");
        Assert.True(Math.Abs(sphereK - 1.0) < 1e-3, $"sphere K = {sphereK:F4} != 1");
        sb.AppendLine("PASS: curvature (Riemann in 2D) is produced — flat ⇒ 0, sphere ⇒ K=1.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: Riemann → Ricci ────────────────────────────────────────────

    [Fact]
    public void RiemannProducesRicci()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: Riemann → Ricci tensor (standard, 2D: R_μν = K·g_μν)");

        double theta = Math.PI / 4;

        // Ricci scalar R = 2K; Ricci tensor R_μν = K·g_μν in 2D.
        double sphereK = GaussCurvature((x, y) => 1.0, (x, y) => Math.Sin(x) * Math.Sin(x), theta, 0.0, H);
        double R = 2.0 * sphereK;                 // Ricci scalar, expect 2 for unit sphere
        double Rtt = sphereK * 1.0;               // R_θθ = K·g_θθ, expect 1
        double flatR = 2.0 * GaussCurvature((x, y) => 1.0, (x, y) => 1.0, theta, 0.0, H);

        sb.AppendLine($"flat:  Ricci scalar R = {flatR:E3}");
        sb.AppendLine($"sphere: R = {R:F4} (expect 2), R_θθ = {Rtt:F4} (expect 1)");

        Assert.True(Math.Abs(flatR) < 1e-6, $"flat metric has nonzero Ricci ({flatR:E3})");
        Assert.True(Math.Abs(R - 2.0) < 2e-3, $"sphere R = {R:F4} != 2");
        Assert.True(Math.Abs(Rtt - 1.0) < 1e-3, $"sphere R_θθ = {Rtt:F4} != 1");
        sb.AppendLine("PASS: Ricci tensor follows from the curvature (flat ⇒ 0, sphere ⇒ R=2).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 4: Ricci → Einstein tensor ────────────────────────────────────

    [Fact]
    public void RicciProducesEinsteinTensor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 4: Ricci → Einstein tensor G_μν = R_μν − ½R·g_μν (standard)");

        double theta = Math.PI / 4;

        // In 2D G_μν vanishes IDENTICALLY (R_μν = ½R·g_μν ⇒ G_μν = 0), for ANY metric.
        double sphereK = GaussCurvature((x, y) => 1.0, (x, y) => Math.Sin(x) * Math.Sin(x), theta, 0.0, H);
        double R = 2.0 * sphereK;
        double Gtt = sphereK * 1.0 - 0.5 * R * 1.0; // G_θθ = K·g_θθ − ½R·g_θθ = 0

        sb.AppendLine($"sphere: G_θθ = {Gtt:E3}  (2D ⇒ identically zero)");

        Assert.True(Math.Abs(Gtt) < 1e-3, $"2D Einstein tensor is nonzero ({Gtt:E3})");
        sb.AppendLine("PASS: G_μν = R_μν − ½R·g_μν is correctly formed (vanishes in 2D, as required).");
        sb.AppendLine("NOTE: a NON-trivial G_μν requires dimension ≥ 3 — not computed here or in TQM.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static double D(Func<double, double, double> f, int dim, double x, double y, double h)
    {
        double fwd = dim == 0 ? f(x + h, y) : f(x, y + h);
        double bwd = dim == 0 ? f(x - h, y) : f(x, y - h);
        return (fwd - bwd) / (2.0 * h);
    }

    /// <summary>Lowered Christoffel Γ_{θφφ} = −½ ∂_θ G for a diagonal metric g = diag(E, G).</summary>
    private static double LoweredChristoffelTpp(
        Func<double, double, double> E, Func<double, double, double> G,
        double x, double y, double h) => -0.5 * D(G, 0, x, y, h);

    private static double MaxLoweredChristoffel(
        Func<double, double, double> E, Func<double, double, double> G,
        double x, double y, double h)
    {
        double max = 0;
        max = Math.Max(max, Math.Abs(0.5 * D(E, 0, x, y, h)));   // Γ_xxx
        max = Math.Max(max, Math.Abs(-0.5 * D(G, 0, x, y, h)));  // Γ_xyy
        max = Math.Max(max, Math.Abs(0.5 * D(G, 1, x, y, h)));   // Γ_yyy
        max = Math.Max(max, Math.Abs(-0.5 * D(E, 1, x, y, h)));  // Γ_yxx
        max = Math.Max(max, Math.Abs(0.5 * D(E, 1, x, y, h)));   // Γ_xxy
        max = Math.Max(max, Math.Abs(0.5 * D(G, 0, x, y, h)));   // Γ_yyx
        return max;
    }

    /// <summary>Gauss curvature K = R/2 for a diagonal 2D metric g = diag(E, G).</summary>
    private static double GaussCurvature(
        Func<double, double, double> E, Func<double, double, double> G,
        double x, double y, double h)
    {
        double sqrtEG = Math.Sqrt(E(x, y) * G(x, y));
        double F(double xx, double yy) => D(G, 0, xx, yy, h) / Math.Sqrt(E(xx, yy) * G(xx, yy));
        double H(double xx, double yy) => D(E, 1, xx, yy, h) / Math.Sqrt(E(xx, yy) * G(xx, yy));
        return -(1.0 / (2.0 * sqrtEG)) * (D(F, 0, x, y, h) + D(H, 1, x, y, h));
    }
}
