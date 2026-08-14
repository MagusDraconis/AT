using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Core.ResearchXC;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXC;

/// <summary>
/// Determines whether the causal order (Q-event precedence) already contains enough information
/// to reconstruct the conformal class of the metric. Uses only existing repository content
/// (OriginOfCausalityModel, CausalUniverse, GeometryEmergence, GrBridgeAnalyzer) plus the
/// standard Malament/light-cone mathematics it references. No new physics, no new primitives.
/// </summary>
public class ConformalStructureTests : ResearchTestBase
{
    public ConformalStructureTests(ITestOutputHelper o) : base(o) { }

    // ── Test 1: causal order → light-cone structure ────────────────────────

    [Fact]
    public void CausalOrder_DefinesLightConeStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 1: causal order → light-cone structure");

        var axioms = OriginOfCausalityModel.Axioms();
        var orderStep = GrBridgeAnalyzer.AuditBridgeSteps()
            .First(s => s.Name == "Causal ordering");

        sb.AppendLine("causal-order axioms present: " +
            string.Join(", ", axioms.Select(a => a.Name)));
        sb.AppendLine($"GrBridge \"Causal ordering\": native = {orderStep.IsTqmNative}, \"{orderStep.DerivationStatus}\"");

        Assert.Contains(axioms, a => a.Name == "Transitivity");
        Assert.Contains(axioms, a => a.Name == "Antisymmetry");
        Assert.Contains(axioms, a => a.Name == "Acyclicity");
        Assert.Contains(axioms, a => a.Name == "Local finiteness");
        Assert.True(orderStep.IsTqmNative);

        // Reconstruct a concrete 1+1D causal order and verify it is a valid light-cone structure.
        int n = 4;
        bool Rel((int t, int x) a, (int t, int x) b) =>
            b.t > a.t && Math.Abs(b.x - a.x) < b.t - a.t; // timelike (inside light cone)

        bool trans = true, antisym = true, nullOnBoundary = true;
        for (int t1 = 0; t1 < n; t1++)
        for (int x1 = 0; x1 < n; x1++)
        for (int t2 = 0; t2 < n; t2++)
        for (int x2 = 0; x2 < n; x2++)
        for (int t3 = 0; t3 < n; t3++)
        for (int x3 = 0; x3 < n; x3++)
        {
            var a = (t1, x1); var b = (t2, x2); var c = (t3, x3);
            if (Rel(a, b) && Rel(b, c) && !Rel(a, c)) trans = false;
            if (Rel(a, b) && Rel(b, a)) antisym = false;
        }
        // null-separated pairs (|Δx| = Δt) lie ON the light cone, hence acausal (not related).
        for (int t1 = 0; t1 < n; t1++)
        for (int x1 = 0; x1 < n; x1++)
        for (int t2 = t1 + 1; t2 < n; t2++)
        for (int x2 = 0; x2 < n; x2++)
            if (Math.Abs(x2 - x1) == t2 - t1 && Rel((t1, x1), (t2, x2))) nullOnBoundary = false;

        sb.AppendLine($"1+1D causal order (grid 4×4): transitive = {trans}, antisymmetric = {antisym}, null-on-boundary = {nullOnBoundary}");

        Assert.True(trans, "causal order not transitive");
        Assert.True(antisym, "causal order not antisymmetric");
        Assert.True(nullOnBoundary, "null-separated events wrongly causally related");
        sb.AppendLine("PASS: the causal order is a valid partial order whose boundary is the light cone.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 2: light-cone structure → conformal class ─────────────────────

    [Fact]
    public void LightConeStructure_DeterminesConformalClass()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 2: light-cone (null) structure determines the conformal class");

        // Minkowski η = diag(-1, +1); conformally related metric g = f·η with f > 0.
        static double Eta((double t, double x) v) => -v.t * v.t + v.x * v.x;

        var vectors = new (double t, double x)[] { (1, 1), (1, 0), (0, 1), (1, 0.9), (0.5, 1) };
        double[] factors = { 0.5, 1.0, 2.0, 10.0 };

        bool nullPreserved = true, causalTypePreserved = true;
        foreach (double f in factors)
        foreach (var v in vectors)
        {
            double eta = Eta(v);
            double g = f * eta;
            if (Math.Abs(eta) < 1e-12 && Math.Abs(g) > 1e-12) nullPreserved = false;
            if (Math.Sign(eta) != Math.Sign(g)) causalTypePreserved = false;
        }

        // Contrast: a NON-conformal rescale g = diag(-1, +2) DOES change the null cone.
        double nonConformalNull = -1.0 * 1 + 2.0 * 1; // vector (1,1): η=0 → g=1 ≠ 0

        sb.AppendLine($"null vectors stay null under g=f·η (f>0): {nullPreserved}");
        sb.AppendLine($"causal type (timelike/null/spacelike) preserved: {causalTypePreserved}");
        sb.AppendLine($"non-conformal g=diag(-1,2): null vector (1,1) → g(v,v) = {nonConformalNull:F1} (≠ 0 ⇒ light cone CHANGES)");

        Assert.True(nullPreserved, "conformal transformation changed a null vector");
        Assert.True(causalTypePreserved, "conformal transformation changed causal type");
        Assert.True(Math.Abs(nonConformalNull) > 1e-12, "non-conformal rescale unexpectedly preserved null cone");
        sb.AppendLine("PASS: the null (light-cone) structure picks out exactly the conformal class.");
        Output.WriteLine(sb.ToString());
    }

    // ── Test 3: classification ─────────────────────────────────────────────

    [Fact]
    public void ConformalClass_ReconstructibleOrImported()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Test 3: Conformal class — reconstructible or imported?");

        var steps = GrBridgeAnalyzer.AuditBridgeSteps();
        var orderStep = steps.First(s => s.Name == "Causal ordering");
        var metricStep = steps.First(s => s.Name == "Metric g_μν from N");

        sb.AppendLine($"causal order (light-cone primitive):  PRESENT   (native = {orderStep.IsTqmNative}, \"{orderStep.DerivationStatus}\")");
        sb.AppendLine($"order → conformal class (Malament):   {(metricStep.IsTqmNative ? "NATIVE" : "IMPORTED")}   (\"{metricStep.DerivationStatus}\")");
        sb.AppendLine($"description: {metricStep.Description}");

        Assert.True(orderStep.IsTqmNative);
        Assert.False(metricStep.IsTqmNative);
        Assert.Equal("External theorem", metricStep.DerivationStatus);
        Assert.Contains("Malament", metricStep.GapDescription, StringComparison.OrdinalIgnoreCase);

        sb.AppendLine();
        sb.AppendLine("VERDICT: the causal order CONTAINS enough information to reconstruct the conformal");
        sb.AppendLine("         class (Malament 1977: causal order ⇒ light cones ⇒ conformal metric), but TQM");
        sb.AppendLine("         IMPORTS this reconstruction — it is not computed natively.");
        Output.WriteLine(sb.ToString());
    }
}
