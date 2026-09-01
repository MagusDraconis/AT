using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 92 — Network consistency constraints. Determines whether consistency conditions restrict link
/// lengths and therefore parameter values. Classify: NO EFFECT / PARTIAL CONSTRAINT / VALUE RELATIONS.
///
/// Tests: ATQG920 (triangle + loop), ATQG921 (neighbor + stability + correlations), ATQG922 (classification).
/// </summary>
public class ATQG_Phase92_NetworkConsistencyParametersTests : ResearchTestBase
{
    public ATQG_Phase92_NetworkConsistencyParametersTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG920: triangle inequalities, loop consistency ──────────────────────────

    [Fact]
    public void ATQG920_TriangleAndLoop()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG920: do triangle/loop conditions restrict lengths?");

        bool triangle = NetworkConsistencyParameters.TriangleInequalityConstrains();
        bool loop = NetworkConsistencyParameters.LoopConsistencyConstrains();

        sb.AppendLine($"triangle inequalities restrict link lengths: {triangle}");
        sb.AppendLine($"loop consistency (holonomy closure) restricts lengths: {loop}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the metric must be a valid distance — triangle inequalities bound triples of lengths, and");
        sb.AppendLine("closed loops impose holonomy consistency. Both restrict allowable link lengths.");
        Output.WriteLine(sb.ToString());

        Assert.True(triangle, "triangle inequality constrains");
        Assert.True(loop, "loop consistency constrains");
    }

    // ── ATQG921: neighbor constraints, global stability, correlations ─────────────

    [Fact]
    public void ATQG921_NeighborStabilityCorrelations()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG921: neighbor/stability constraints → parameter correlations");

        bool neighbor = NetworkConsistencyParameters.NeighborConstraintsApply();
        bool stability = NetworkConsistencyParameters.GlobalStabilityConstrains();
        bool correlate = NetworkConsistencyParameters.InducesParameterCorrelations();
        bool determines = NetworkConsistencyParameters.ConsistencyDeterminesValues();

        sb.AppendLine($"neighbor constraints restrict local configurations: {neighbor}");
        sb.AppendLine($"global stability restricts lengths: {stability}");
        sb.AppendLine($"these induce parameter correlations/relations: {correlate}");
        sb.AppendLine($"consistency DETERMINES specific values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: length restrictions (via QG91 encoding) induce bounds/relations among parameters, but the");
        sb.AppendLine("specific values remain free within the allowed region.");
        Output.WriteLine(sb.ToString());

        Assert.True(neighbor, "neighbor constraints apply");
        Assert.True(stability, "stability constrains");
        Assert.True(correlate, "induces correlations");
        Assert.False(determines, "does not determine values");
    }

    // ── ATQG922: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG922_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG922: NO EFFECT / PARTIAL CONSTRAINT / VALUE RELATIONS?");

        sb.AppendLine($"CLASSIFICATION: {NetworkConsistencyParameters.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO EFFECT: triangle/loop/neighbor/stability conditions DO restrict lengths and hence parameters.");
        sb.AppendLine("  • NOT VALUE RELATIONS alone: the conditions are bounds/inequalities, not equations that fix values.");
        sb.AppendLine("  • PARTIAL CONSTRAINT: consistency conditions induce bounds + correlations among parameters, but do not");
        sb.AppendLine("    determine the specific values.");
        sb.AppendLine();
        sb.AppendLine("So network consistency PARTIALLY constrains parameter values (bounds + correlations).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL CONSTRAINT", NetworkConsistencyParameters.Classify());
        Assert.True(NetworkConsistencyParameters.TriangleInequalityConstrains());
        Assert.False(NetworkConsistencyParameters.ConsistencyDeterminesValues());
    }
}
