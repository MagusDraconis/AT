using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Core.ResearchXC;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXC;

/// <summary>
/// Determines whether the existing causal-distance structure (CausalUniverse +
/// GeometryEmergence) can generate a metric tensor candidate. Reconstructs the standard
/// conformal-metric recipe ("interval volume → conformal factor") with existing content only
/// and verifies it with the standard-geometry builder (EinsteinTensorBuilder).
/// No new physics, no new primitives — only reconstruction of existing repository content.
/// </summary>
public class MetricEmergenceTests : ResearchTestBase
{
    public MetricEmergenceTests(ITestOutputHelper o) : base(o) { }

    private const double H = 1e-4;

    // ── Test 1: the causal distance matrix is a metric ──────────────────────

    [Fact]
    public void DistanceMatrix_IsMetric()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: distance matrix from causal distance is a metric");

        int n = 8;
        double dim = CausalUniverse.EmergentDimension; // 4
        var dMat = new double[n, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            double depth = Math.Abs(i - j);                       // number of causal links
            double vol = CausalUniverse.CausalVolume(depth, dim); // interval volume N ∝ D^d
            dMat[i, j] = GeometryEmergence.CausalDistance(vol, dim); // N^(1/d) = depth
        }

        bool nonNeg = true, identity = true, sym = true, triangle = true;
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            if (dMat[i, j] < -1e-12) nonNeg = false;
            if ((i == j && Math.Abs(dMat[i, j]) > 1e-12) ||
                (i != j && dMat[i, j] <= 1e-12)) identity = false;
            if (Math.Abs(dMat[i, j] - dMat[j, i]) > 1e-12) sym = false;
            for (int k = 0; k < n; k++)
                if (dMat[i, k] > dMat[i, j] + dMat[j, k] + 1e-9) triangle = false;
        }

        sb.AppendLine($"8×8 distance matrix (d={dim}): D[i,j] = |i−j| links");
        sb.AppendLine($"non-negativity = {nonNeg}, identity = {identity}, symmetry = {sym}, triangle = {triangle}");

        Assert.True(nonNeg, "negative distance");
        Assert.True(identity, "identity of indiscernibles violated");
        Assert.True(sym, "distance not symmetric");
        Assert.True(triangle, "triangle inequality violated");
        sb.AppendLine("PASS: the causal distance matrix satisfies all four metric axioms.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: causal volume defines the conformal factor ──────────────────

    [Fact]
    public void CausalVolume_DefinesConformalFactor()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: causal volume (counting measure) → conformal factor");

        bool ok = true;
        foreach (double dim in new[] { 2.0, 3.0, 4.0 })
        foreach (double rho in new[] { 0.5, 1.0, 2.0, 8.0 })
        {
            double f = ConformalFactor(rho, dim);          // f = ρ^(2/d)
            double volElement = Math.Pow(f, dim / 2.0);    // √|g| = f^(d/2)
            if (Math.Abs(volElement - rho) > 1e-12) ok = false;
        }

        double f1 = ConformalFactor(1.0, 4.0); // unit density → trivial conformal factor
        sb.AppendLine($"round-trip √|g| = f^(d/2) = ρ holds for ρ ∈ {{0.5,1,2,8}}, d ∈ {{2,3,4}}");
        sb.AppendLine($"unit density ρ=1 (d=4) → conformal factor f = {f1:F6} (expect 1)");

        Assert.True(ok, "conformal factor round-trip failed");
        Assert.Equal(1.0, f1, 12);
        sb.AppendLine("PASS: the counting measure fixes the conformal factor f = ρ^(2/d).");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: conformal metric candidate is constructible ─────────────────

    [Fact]
    public void ConformalMetricCandidate_IsConstructible()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: conformal metric candidate g = f·η is constructible");

        // Constant conformal factor → conformally flat → zero curvature.
        EinsteinTensorBuilder.MetricField flat = p => ConformalMetric(p, 1.0);
        double rFlat = EinsteinTensorBuilder.RicciScalar(flat, new[] { 0.5, 0.0 }, H);

        // Non-constant conformal factor f(x) = 1 + 0.5·x² → curvature emerges.
        EinsteinTensorBuilder.MetricField curved = p => ConformalMetric(p, 1.0 + 0.5 * p[0] * p[0]);
        double rCurved = EinsteinTensorBuilder.RicciScalar(curved, new[] { 0.5, 0.0 }, H);

        sb.AppendLine($"constant f=1:            R = {rFlat:E3}   (conformally flat ⇒ 0)");
        sb.AppendLine($"f = 1 + 0.5·x² at x=0.5: R = {rCurved:F4}   (curvature from conformal factor)");

        Assert.True(Math.Abs(rFlat) < 1e-6, $"constant conformal factor gave R = {rFlat:E3} != 0");
        Assert.True(rCurved < -0.1, $"non-constant conformal factor gave R = {rCurved:F4}, expected curvature");
        sb.AppendLine("PASS: g = f·η is a valid symmetric metric; flat for constant f, curved for varying f.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 4: classification ─────────────────────────────────────────────

    [Fact]
    public void MetricEmergence_PresentOrMissing()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 4: Metric emergence — Present / Partial / Missing");

        var metricStep = GrBridgeAnalyzer.AuditBridgeSteps()
            .First(s => s.Name.Contains("Metric", StringComparison.OrdinalIgnoreCase));

        sb.AppendLine($"distance matrix (metric in distance sense):   PRESENT      (CausalUniverse + GeometryEmergence)");
        sb.AppendLine($"conformal factor from causal volume:           PRESENT      (f = ρ^(2/d), reconstructed)");
        sb.AppendLine($"conformally-flat candidate g = f·η:            CONSTRUCTIBLE (standard, verified R=0/≠0)");
        sb.AppendLine($"full g_μν from Q-events (incl. conformal class): {(!metricStep.IsAtNative ? "MISSING" : "PRESENT")}     (\"{metricStep.DerivationStatus}\")");

        Assert.False(metricStep.IsAtNative, "g_μν unexpectedly marked AT-native");

        sb.AppendLine();
        sb.AppendLine("VERDICT: the causal-distance structure CAN generate a metric tensor candidate");
        sb.AppendLine("         UP TO CONFORMAL FACTOR (a conformally-flat g = f·η). The conformal");
        sb.AppendLine("         structure itself (light cones from the full causal order) is imported.");
        Output.WriteLine(sb.ToString());
    }

    // ── Helpers (standard reconstruction, no new primitives) ────────────────

    /// <summary>Conformal factor from counting density: √|g| = f^(d/2) = ρ ⇒ f = ρ^(2/d).</summary>
    private static double ConformalFactor(double density, double dim) => Math.Pow(density, 2.0 / dim);

    /// <summary>Conformally-flat metric candidate g = f·η with η = diag(1,1) (2D).</summary>
    private static double[,] ConformalMetric(double[] x, double f)
        => new[,] { { f, 0.0 }, { 0.0, f } };
}
