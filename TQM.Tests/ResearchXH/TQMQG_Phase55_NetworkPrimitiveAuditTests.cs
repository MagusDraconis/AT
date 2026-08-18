using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 55 — are Q-events and ψ truly independent? Determines whether (nodes, links) can be one primitive.
/// Classify: INDEPENDENT / DUAL / UNIFIED.
///
/// Tests: TQMQG550 (node/link completeness), TQMQG551 (one network primitive), TQMQG552 (classification).
/// </summary>
public class TQMQG_Phase55_NetworkPrimitiveAuditTests : ResearchTestBase
{
    public TQMQG_Phase55_NetworkPrimitiveAuditTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG550: node-only / link-only / node+link ─────────────────────────────────

    [Fact]
    public void TQMQG550_Completeness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG550: nodes alone and links alone are both incomplete");

        bool nodeOnly = NetworkPrimitiveAudit.NodeOnlySufficient();
        bool linkOnly = NetworkPrimitiveAudit.LinkOnlySufficient();
        bool complete = NetworkPrimitiveAudit.NodeLinkComplete();

        sb.AppendLine($"node-only (no links) sufficient:  {nodeOnly}  (no structure)");
        sb.AppendLine($"link-only (no nodes) sufficient:  {linkOnly}  (no endpoints)");
        sb.AppendLine($"nodes + links complete:           {complete}  (a network)");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a network is intrinsically the pair (V, E). Nodes alone carry no structure; links alone are");
        sb.AppendLine("undefined. Only nodes + links form a complete network.");
        Output.WriteLine(sb.ToString());

        Assert.False(nodeOnly, "node-only should be incomplete");
        Assert.False(linkOnly, "link-only should be incomplete");
        Assert.True(complete, "node+link should be complete");
    }

    // ── TQMQG551: one network primitive ──────────────────────────────────────────────

    [Fact]
    public void TQMQG551_OneNetworkPrimitive()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG551: (nodes, links) = one primitive");

        bool one = NetworkPrimitiveAudit.OneNetworkPrimitive();
        bool newDof = NetworkPrimitiveAudit.PsiStillNewDof();
        bool dual = NetworkPrimitiveAudit.DualInternalStructure();

        sb.AppendLine($"(nodes, links) is ONE network primitive: {one}");
        sb.AppendLine($"ψ (Weyl content) remains a NEW d.o.f.:  {newDof}");
        sb.AppendLine($"nodes/links are two IRREDUCIBLE aspects: {dual}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the causal network (V, E) is one primitive, with Q-events (nodes) and ψ (links' Weyl content) as");
        sb.AppendLine("two irreducible aspects. The scalar sector froze Weyl=0; ψ is the unfrozen link content — a new d.o.f. within");
        sb.AppendLine("the single network primitive.");
        Output.WriteLine(sb.ToString());

        Assert.True(one, "nodes+links should be one primitive");
        Assert.True(newDof, "psi should remain a new d.o.f.");
        Assert.True(dual, "nodes and links should be irreducible aspects");
    }

    // ── TQMQG552: classification ───────────────────────────────────────────────────────

    [Fact]
    public void TQMQG552_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG552: INDEPENDENT / DUAL / UNIFIED?");

        sb.AppendLine($"CLASSIFICATION: {NetworkPrimitiveAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT INDEPENDENT: Q-events (nodes) and ψ (links' Weyl content) are not two separate primitives — they");
        sb.AppendLine("    are two aspects of ONE network (a network is the single object (V, E)).");
        sb.AppendLine("  • UNIFIED: the primitive count reduces from two to ONE — the causal network primitive.");
        sb.AppendLine("  • WITH A DUAL INTERIOR: nodes (spin-0 actualization) and links (spin-2 Weyl) remain two irreducible aspects");
        sb.AppendLine("    (QG51). The scalar sector was the restricted case Weyl = 0; ψ is the unfrozen Weyl content.");
        sb.AppendLine();
        sb.AppendLine("So the theory is ONE network primitive with a dual (node/link) interior — not two independent primitives.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIFIED", NetworkPrimitiveAudit.Classify());
        Assert.True(NetworkPrimitiveAudit.OneNetworkPrimitive());
        Assert.True(NetworkPrimitiveAudit.DualInternalStructure());
    }
}
