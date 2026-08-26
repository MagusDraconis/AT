using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Determines whether the Einstein-recovery claims in the analyzers can be converted into
/// executable tests. Uses EmergentGravityAnalyzer (X061) for the effective equations and the
/// qualitative GR matches, and a minimal standard Christoffel computation for the curvature
/// correctness criterion (which the AT analyzers only assert, never compute).
/// </summary>
public class EinsteinRecoveryTests : ResearchTestBase
{
    public EinsteinRecoveryTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: metric (density) produces curvature ────────────────────────

    [Fact]
    public void MetricProducesCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: metric (density) produces curvature — EmergentGravityAnalyzer");

        var eqns = EmergentGravityAnalyzer.ComputeEffectiveEquations();
        var rhoEq = eqns[0]; // "R ≈ a + b·ρ"

        sb.AppendLine($"form: {rhoEq.Form}");
        sb.AppendLine($"coupling (slope b) = {rhoEq.Coupling:F3}, FitQuality = {rhoEq.FitQuality:F4}");

        // Curvature responds to density: the slope b (≈ 8π) is nonzero and the fit is good.
        Assert.True(Math.Abs(rhoEq.Coupling) > 1.0, $"curvature does not respond to density (b={rhoEq.Coupling:F3})");
        Assert.True(rhoEq.FitQuality > 0.99, $"curvature-density fit is poor (R²={rhoEq.FitQuality:F4})");
        sb.AppendLine("PASS: the analyzer's effective equation has R ∝ ρ (nonzero curvature-density slope).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: flat metric produces zero curvature (standard construction) ─

    [Fact]
    public void FlatMetricProducesZeroCurvature()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: flat metric ⇒ zero Christoffel symbols (standard construction)");

        // Standard (lowered) Christoffel symbols Γ_{λμν} = ½(∂_μ g_{νλ} + ∂_ν g_{μλ} − ∂_λ g_{μν}).
        // A constant (flat) metric has ∂g = 0 ⇒ Γ = 0 ⇒ zero curvature.
        double flatMax = MaxChristoffel((x, y) => 1.0, (x, y) => 1.0);          // g = diag(1,1)
        double curvedMax = MaxChristoffel((x, y) => 1.0 + 0.1 * x * x, (x, y) => 1.0); // g_xx = 1+0.1x²

        sb.AppendLine($"max |Γ| for flat metric    = {flatMax:E3}");
        sb.AppendLine($"max |Γ| for curved metric  = {curvedMax:E3}");

        Assert.True(flatMax < 1e-9, $"flat metric has nonzero Christoffel symbols ({flatMax:E3})");
        Assert.True(curvedMax > 1e-3, $"curved metric has zero Christoffel symbols ({curvedMax:E3})");
        sb.AppendLine("PASS: flat ⇒ zero curvature; non-flat ⇒ nonzero curvature (correctness criterion).");
        sb.AppendLine("NOTE: the AT analyzers ASSERT this ('R=0 for flat') but never compute it.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: Einstein limit recovered (leading order + qualitative GR) ───

    [Fact]
    public void EinsteinLimitRecovered()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: Einstein limit — leading-order recovery and qualitative GR matches");

        var eqns = EmergentGravityAnalyzer.ComputeEffectiveEquations();
        var einsteinEq = eqns[1]; // "G_μν = (8πG_eff) T_μν + O(ℓ_P²·R²)"

        sb.AppendLine($"form: {einsteinEq.Form}");
        sb.AppendLine($"coupling = {einsteinEq.Coupling:F3} (≈ 8π = {8 * Math.PI:F3}), RecoverEinstein = {einsteinEq.RecoverEinstein}");

        Assert.True(einsteinEq.RecoverEinstein, "the analyzer does not claim Einstein recovery at leading order");
        Assert.True(Math.Abs(einsteinEq.Coupling - 8.0 * Math.PI) < 1.0,
            $"coupling {einsteinEq.Coupling:F3} deviates from 8π");

        // Qualitative GR matches: Newtonian, lensing, redshift, precession, gravitational waves.
        var tests = EmergentGravityAnalyzer.RunGravityTests();
        int matches = tests.Count(t => t.MatchesGR);
        foreach (var t in tests.Take(5))
            sb.AppendLine($"  {t.Phenomenon,-26} matchesGR={t.MatchesGR}  deviation={t.Deviation:F3}");

        sb.AppendLine($"qualitative GR matches: {matches}/{tests.Count}");
        Assert.True(matches >= 5, "fewer than 5 qualitative GR matches (weak-field recovery incomplete)");

        sb.AppendLine("PASS: Einstein recovered at leading order (coupling 8π) + weak-field GR matches.");
        sb.AppendLine("NOTE: this is leading-order + qualitative — no full G_μν tensor is computed.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    /// <summary>Max magnitude of the lowered Christoffel symbols for a diagonal 2D metric.</summary>
    private static double MaxChristoffel(
        Func<double, double, double> gxx, Func<double, double, double> gyy)
    {
        double h = 1e-4;
        double x = 0.5, y = 0.5;
        // Diagonal-metric nonzero lowered Christoffel components.
        double gxx_x = (gxx(x + h, y) - gxx(x - h, y)) / (2 * h);
        double gyy_x = (gyy(x + h, y) - gyy(x - h, y)) / (2 * h);
        double gxx_y = (gxx(x, y + h) - gxx(x, y - h)) / (2 * h);
        double gyy_y = (gyy(x, y + h) - gyy(x, y - h)) / (2 * h);

        double max = 0;
        max = Math.Max(max, Math.Abs(0.5 * gxx_x));          // Γ_xxx
        max = Math.Max(max, Math.Abs(-0.5 * gyy_x));         // Γ_xyy
        max = Math.Max(max, Math.Abs(0.5 * gyy_y));          // Γ_yyy
        max = Math.Max(max, Math.Abs(-0.5 * gxx_y));         // Γ_yxx
        max = Math.Max(max, Math.Abs(0.5 * gxx_y));          // Γ_xxy = Γ_xyx
        max = Math.Max(max, Math.Abs(0.5 * gyy_x));          // Γ_yyx = Γ_yxy
        return max;
    }
}
