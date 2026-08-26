using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Core.ResearchXC;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Determines whether AT already contains enough information to generate g_μν from Q-events.
/// Uses only existing analyzers (CausalUniverse, GeometryEmergence, GrBridgeAnalyzer) plus the
/// standard-geometry builder (EinsteinTensorBuilder) for the coordinate-invariance criterion.
/// No new physics, no new primitives.
/// </summary>
public class MetricGenerationTests : ResearchTestBase
{
    public MetricGenerationTests(ITestOutputHelper o) : base(o) { }

    private const double H = 1e-4;

    // ── Test 1: Q-events → distance structure ──────────────────────────────

    [Fact]
    public void QEvents_DefineDistanceStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: Q-events → distance structure (causal interval count)");

        bool ok = true;
        foreach (double d in new[] { 2.0, 3.0, 4.0 })
        foreach (double depth in new[] { 1.0, 2.0, 4.0, 8.0 })
        {
            double vol = CausalUniverse.CausalVolume(depth, d);
            double dist = GeometryEmergence.CausalDistance(vol, d);
            if (Math.Abs(dist - depth) > 1e-9) ok = false;
        }

        double recDim = CausalUniverse.RecoverDimension(
            CausalUniverse.CausalVolume(4.0, 4.0),
            CausalUniverse.CausalVolume(8.0, 4.0), 4.0, 8.0);

        sb.AppendLine($"causal distance = N^(1/d) recovers depth D exactly (all d=2,3,4; D=1..8)");
        sb.AppendLine($"recovered dimension from volume growth = {recDim:F6} (expect 4)");

        Assert.True(ok, "causal distance failed to recover depth");
        Assert.Equal(4.0, recDim, 6);
        sb.AppendLine("PASS: Q-events (causal interval count) define a distance structure.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: distance structure → metric candidate ──────────────────────

    [Fact]
    public void DistanceStructure_DefinesMetricCandidate()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: distance structure → metric candidate g_μν");

        string recipe = GeometryEmergence.MetricRecovery;
        var metricStep = GrBridgeAnalyzer.AuditBridgeSteps()
            .First(s => s.Name.Contains("Metric", StringComparison.OrdinalIgnoreCase));

        sb.AppendLine($"recipe (GeometryEmergence.MetricRecovery): \"{recipe}\"");
        sb.AppendLine($"GrBridge step \"{metricStep.Name}\": status = \"{metricStep.DerivationStatus}\", native = {metricStep.IsAtNative}");

        Assert.False(string.IsNullOrWhiteSpace(recipe), "metric-recovery recipe is empty");
        Assert.Contains("conformal", recipe, StringComparison.OrdinalIgnoreCase);
        Assert.Equal("External theorem", metricStep.DerivationStatus);
        Assert.False(metricStep.IsAtNative);

        sb.AppendLine("PASS: the candidate is a CONFORMAL class (light cone) + conformal factor (volume),");
        sb.AppendLine("      stated as text only — the full tensor g_μν is NOT computed natively (external theorem).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: metric candidate → coordinate invariance (standard criterion) ─

    [Fact]
    public void MetricCandidate_IsCoordinateInvariant()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: metric candidate is coordinate-invariant (standard criterion)");

        // Same unit 2-sphere in two charts; the Ricci scalar R is a coordinate-invariant
        // scalar, so it must be equal (and = 2) in both charts even though components change.
        double theta = Math.PI / 4;

        EinsteinTensorBuilder.MetricField chartA = p => new[,]
        {
            { 1.0, 0.0 },
            { 0.0, Math.Sin(p[0]) * Math.Sin(p[0]) },
        };
        // Chart B: θ = 2·θ' ⇒ g = diag(4, sin²(2θ')); same sphere, θ' = π/8 ⇔ θ = π/4.
        EinsteinTensorBuilder.MetricField chartB = p => new[,]
        {
            { 4.0, 0.0 },
            { 0.0, Math.Sin(2.0 * p[0]) * Math.Sin(2.0 * p[0]) },
        };

        double rA = EinsteinTensorBuilder.RicciScalar(chartA, new[] { theta, 0.0 }, H);
        double rB = EinsteinTensorBuilder.RicciScalar(chartB, new[] { theta / 2.0, 0.0 }, H);

        sb.AppendLine($"chart A (θ):  R = {rA:F4}   (metric diag(1, sin²θ))");
        sb.AppendLine($"chart B (θ'=θ/2): R = {rB:F4}   (metric diag(4, sin²2θ'))");
        sb.AppendLine($"|R_A − R_B| = {Math.Abs(rA - rB):E3}  (must be ~0, and both ≈ 2)");

        Assert.True(Math.Abs(rA - 2.0) < 2e-3, $"chart A R = {rA:F4} != 2");
        Assert.True(Math.Abs(rB - 2.0) < 2e-3, $"chart B R = {rB:F4} != 2");
        Assert.True(Math.Abs(rA - rB) < 5e-3, $"R not coordinate-invariant: |ΔR| = {Math.Abs(rA - rB):E3}");
        sb.AppendLine("PASS: scalar invariants of a metric candidate are coordinate-invariant (standard).");
        sb.AppendLine("      AT's candidate is a conformal class — invariant by construction, but no full tensor exists to transform.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 4: classification ─────────────────────────────────────────────

    [Fact]
    public void MetricGeneration_PresentOrMissing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 4: Metric generation — Present / Partial / Missing");

        var metricStep = GrBridgeAnalyzer.AuditBridgeSteps()
            .First(s => s.Name.Contains("Metric", StringComparison.OrdinalIgnoreCase));
        bool distancePresent = !string.IsNullOrWhiteSpace(GeometryEmergence.MetricRecovery);
        bool tensorComputed = metricStep.IsAtNative;

        sb.AppendLine($"distance structure (Q-events → length):   PRESENT   (CausalUniverse + GeometryEmergence, numeric)");
        sb.AppendLine($"metric candidate g_μν (conformal recipe):  PARTIAL   (text only, {(!distancePresent ? "absent" : "present")})");
        sb.AppendLine($"full g_μν generated from Q-events:         {(tensorComputed ? "PRESENT" : "MISSING")}   (\"{metricStep.DerivationStatus}\")");

        Assert.True(distancePresent, "distance structure unexpectedly absent");
        Assert.False(tensorComputed, "g_μν unexpectedly marked AT-native");

        sb.AppendLine();
        sb.AppendLine("VERDICT: AT does NOT generate g_μν from Q-events.");
        sb.AppendLine("         It defines a distance (present) and a conformal metric recipe (text),");
        sb.AppendLine("         but imports the full metric via the external Malament/HKM theorem.");
        Output.WriteLine(sb.ToString());
    }
}
