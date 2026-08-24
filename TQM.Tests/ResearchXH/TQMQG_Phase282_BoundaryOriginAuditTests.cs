using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 282 — Boundary Origin Audit. What is the origin of the boundary (N=96 closure)? Is it
/// derived or primitive? D96 only, no formulas.
/// </summary>
public class TQMQG_Phase282_BoundaryOriginAuditTests : ResearchTestBase
{
    public TQMQG_Phase282_BoundaryOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2820_AttractorClosure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2820: the N=96 attractor as the closure");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the actualization dynamics converges to a FIXED POINT (0% residual link growth);");
        sb.AppendLine("  - the attractor is UNIQUE (every initial pattern → identical geometry).");
        sb.AppendLine();

        sb.AppendLine($"topology converges: {BoundaryOriginAudit.TopologyConverges()}");
        sb.AppendLine($"attractor is unique (content-independent): {BoundaryOriginAudit.AttractorIsUnique()}");
        sb.AppendLine($"dynamics has a fixed point: {BoundaryOriginAudit.HasFixedPoint()}");
        sb.AppendLine($"boundary is the fixed point: {BoundaryOriginAudit.BoundaryIsFixedPoint()}");
        sb.AppendLine($"attractor is stable: {BoundaryOriginAudit.AttractorIsStable()}");

        Output.WriteLine(sb.ToString());

        Assert.True(BoundaryOriginAudit.TopologyConverges(),
            "the topology saturates (link growth → 0)");
        Assert.True(BoundaryOriginAudit.AttractorIsUnique(),
            "every initial pattern converges to the same N=96 geometry (QG116)");
        Assert.True(BoundaryOriginAudit.BoundaryIsFixedPoint());
    }

    [Fact]
    public void TQMQG2821_NotAMenuChoice()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2821: the D96 selection is the attractor, not a menu");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - N=96 is the attractor the dynamics converges to, not a chosen input;");
        sb.AppendLine("  - the symmetries are attractor properties, not inputs.");
        sb.AppendLine();

        sb.AppendLine($"N=96 is the attractor, not a choice: {BoundaryOriginAudit.N96IsAttractorNotChoice()}");
        sb.AppendLine($"symmetries are attractor properties: {BoundaryOriginAudit.SymmetriesAreAttractorProperties()}");
        sb.AppendLine($"boundary is the closure: {BoundaryOriginAudit.BoundaryIsClosure()}");
        sb.AppendLine();
        sb.AppendLine("N=96 is not picked from (64, 96, 128, 192) — it is what the dynamics converges to");
        sb.AppendLine("from every initial pattern. The Z2 symmetry, octave families, and degree-12");
        sb.AppendLine("regularity are PROPERTIES OF THE ATTRACTOR, not inputs (QG159/161/266).");

        Output.WriteLine(sb.ToString());

        Assert.True(BoundaryOriginAudit.N96IsAttractorNotChoice(),
            "the D96 selection is the attractor (QG159/160 INEVITABLE), not a menu choice");
        Assert.True(BoundaryOriginAudit.SymmetriesAreAttractorProperties());
        Assert.True(BoundaryOriginAudit.BoundaryIsClosure());
    }

    [Fact]
    public void TQMQG2822_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2822: the boundary-origin determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - BOUNDARY FUNDAMENTAL (score ≤ 2), BOUNDARY DERIVED (3-4),");
        sb.AppendLine("    CLOSURE PRINCIPLE (5-6);");
        sb.AppendLine("  - the question: is the boundary derived or primitive?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {BoundaryOriginAudit.Summary()}");
        sb.AppendLine($"Origin score: {BoundaryOriginAudit.OriginScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {BoundaryOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - The boundary (N=96) is NOT a primitive input: it is the STABLE FIXED POINT");
        sb.AppendLine("    (closure) of the actualization dynamics.");
        sb.AppendLine("  - Every initial activity pattern converges to the same N=96 geometry");
        sb.AppendLine("    (content-independent attractor, QG116).");
        sb.AppendLine("  - The Z2 symmetry, octave families, and degree-12 regularity are attractor");
        sb.AppendLine("    PROPERTIES — derived, not assumed.");
        sb.AppendLine("  - The primitive is the PROCESS (actualization); the boundary is its fixed point.");
        sb.AppendLine("  - CONCLUSION: the boundary is DERIVED — it is the CLOSURE of the actualization");
        sb.AppendLine("    dynamics (the closure principle).");

        Output.WriteLine(sb.ToString());

        Assert.Equal("CLOSURE PRINCIPLE", BoundaryOriginAudit.Classify());
        Assert.True(BoundaryOriginAudit.OriginScore() >= 5);
        Assert.Contains("CLOSURE PRINCIPLE", BoundaryOriginAudit.Summary());
        Assert.Contains("fixed point", BoundaryOriginAudit.Summary());
    }
}
