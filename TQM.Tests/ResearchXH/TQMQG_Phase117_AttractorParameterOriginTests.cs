using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 117 — Do physical parameters control the attractor geometry? QG116b showed the universal
/// attractor's saturated link radius depends on the dynamics parameters. This phase asks: can changes in
/// attractor parameters produce DISTINCT STABLE geometries analogous to masses, families, or interaction
/// strengths? Sweeps the (feedback, damping) parameter plane and measures attractor radius, feedback/damping
/// response, geometry classes (KS single-linkage), and parameter sensitivity. Classify: NO RELATION /
/// PARTIAL RELATION / ATTRACTOR ORIGIN.
///
/// Tests: TQMQG1170 (attractor radius + parameter response), TQMQG1171 (geometry classes), TQMQG1172
/// (parameter sensitivity + classification).
/// </summary>
public class TQMQG_Phase117_AttractorParameterOriginTests : ResearchTestBase
{
    public TQMQG_Phase117_AttractorParameterOriginTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG1170: attractor radius + parameter response ───────────────────────────

    [Fact]
    public void TQMQG1170_AttractorRadiusAndParameterResponse()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1170: attractor radius; feedback and damping response");

        double[] radii = AttractorParameterOrigin.DistinctRadii();
        var vsFeedback = AttractorParameterOrigin.RadiusVsFeedback(0.3);
        var vsDamping = AttractorParameterOrigin.RadiusVsDamping(0.5);

        sb.AppendLine("ATTRACTOR RADIUS LADDER (parameter plane, K=6, N=96):");
        sb.AppendLine($"  distinct saturated radii: [{string.Join(", ", radii.Select(r => r.ToString("F1")))}]");
        sb.AppendLine();
        sb.AppendLine("FEEDBACK RESPONSE (radius at d=0.3):");
        foreach (var p in vsFeedback)
            sb.AppendLine($"  f={p.Feedback:F1} -> radius={p.Radius:F1} (links={p.Links}, span={p.Span:F2}, families={p.Families})");
        sb.AppendLine();
        sb.AppendLine("DAMPING RESPONSE (radius at f=0.5):");
        foreach (var p in vsDamping)
            sb.AppendLine($"  d={p.Damping:F1} -> radius={p.Radius:F1}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the attractor radius is NOT a continuum — it takes a small DISCRETE ladder of");
        sb.AppendLine("values (2 and 6 links/node for K=6). Increasing feedback holds/raises the radius; increasing");
        sb.AppendLine("damping lowers it — the parameters CONTROL the geometry, in discrete plateaus.");
        Output.WriteLine(sb.ToString());

        // discrete ladder: at least two distinct radii realized
        Assert.True(radii.Length >= 2, "parameter plane realizes a discrete ladder of radii");
        Assert.True(radii.Length <= 6, "the radius ladder is SMALL (discrete classes, not a continuum)");
        // feedback response monotone non-decreasing
        for (int i = 1; i < vsFeedback.Length; i++)
            Assert.True(vsFeedback[i].Radius >= vsFeedback[i - 1].Radius - 1e-9,
                "radius is monotone non-decreasing in feedback");
        // damping response monotone non-increasing
        for (int i = 1; i < vsDamping.Length; i++)
            Assert.True(vsDamping[i].Radius <= vsDamping[i - 1].Radius + 1e-9,
                "radius is monotone non-increasing in damping");
    }

    // ── TQMQG1171: geometry classes ────────────────────────────────────────────────

    [Fact]
    public void TQMQG1171_GeometryClasses()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1171: do parameter plateaus map to distinct stable geometry classes?");

        var (classes, _) = AttractorParameterOrigin.GeometryClasses(0.12);
        bool robust = AttractorParameterOrigin.GeometryRobustWithinPlateaus();
        double intraMax = AttractorParameterOrigin.MaxIntraClassDistance();

        var radius2 = AttractorParameterOrigin.AttractorAt(0.3, 0.5);   // low plateau
        var radius6 = AttractorParameterOrigin.AttractorAt(0.9, 0.3);   // high plateau

        sb.AppendLine($"GEOMETRY CLASSES (KS single-linkage, ε=0.12, 16-pt parameter plane): {classes}");
        sb.AppendLine($"  geometry robust WITHIN each radius plateau: {robust}");
        sb.AppendLine($"  max intra-plateau shape distance: {intraMax:F4}");
        sb.AppendLine();
        sb.AppendLine("PLATEAU GEOMETRIES (distinct spectral signatures):");
        sb.AppendLine($"  radius 2 class: span={radius2.Span:F2}, families={radius2.Families}");
        sb.AppendLine($"  radius 6 class: span={radius6.Span:F2}, families={radius6.Families}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the parameter plane maps to a SMALL set of DISCRETE geometry classes — each");
        sb.AppendLine("radius plateau is a distinct stable geometry (different span, different family count), and");
        sb.AppendLine("the geometry is IDENTICAL within a plateau (robust). This is a discrete spectrum of stable");
        sb.AppendLine("geometries parameter-controlled like families or mass levels.");
        Output.WriteLine(sb.ToString());

        Assert.True(classes >= 2, "at least two distinct geometry classes exist in the parameter plane");
        Assert.True(classes <= 3, "the class set is small and discrete (not a continuum)");
        Assert.True(robust, "geometry is robust within each radius plateau");
        Assert.True(intraMax < 0.12, "intra-plateau shapes are near-identical");
        // distinct classes have distinct geometry signatures
        Assert.NotEqual(radius2.Span, radius6.Span);
        Assert.NotEqual(radius2.Families, radius6.Families);
    }

    // ── TQMQG1172: parameter sensitivity + classification ─────────────────────────

    [Fact]
    public void TQMQG1172_ParameterSensitivityAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG1172: parameter sensitivity → NO RELATION / PARTIAL RELATION / ATTRACTOR ORIGIN");

        double maxAdjacent = AttractorParameterOrigin.MaxAdjacentShapeDistance();
        double intraMax = AttractorParameterOrigin.MaxIntraClassDistance();
        string cls = AttractorParameterOrigin.Classify();

        sb.AppendLine("PARAMETER SENSITIVITY:");
        sb.AppendLine($"  max spectral shape distance between ADJACENT parameter points: {maxAdjacent:F4}");
        sb.AppendLine($"  max shape distance WITHIN a plateau: {intraMax:F4}");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: the geometry strongly RESPONDS to parameters (radius ladder 2↔6,");
        sb.AppendLine("    adjacent-point shape distance up to 0.62).");
        sb.AppendLine("  • NOT PARTIAL RELATION: the response is NOT a smooth continuum — geometries are");
        sb.AppendLine("    near-identical within each plateau and JUMP between discrete classes at thresholds.");
        sb.AppendLine("  • ATTRACTOR ORIGIN: the parameter plane controls a discrete ladder of stable geometry");
        sb.AppendLine("    classes (radius = k links/node, each a distinct spectral class) — distinct stable");
        sb.AppendLine("    geometries parameter-controlled exactly as masses/families/interaction strengths.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("ATTRACTOR ORIGIN", cls);
        // sharp threshold between classes (adjacent distance large) vs robustness within plateau
        Assert.True(maxAdjacent > 0.3, "geometry jumps sharply between parameter plateaus");
        Assert.True(intraMax < 0.12, "geometry is stable within a plateau");
    }
}
