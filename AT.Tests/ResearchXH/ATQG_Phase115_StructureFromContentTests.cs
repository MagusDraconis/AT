using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 115 — Does content determine structure? Previous phases assumed network → physics. This phase
/// tests the alternative: actualization patterns determine network geometry — can the network emerge dynamically
/// from its own activity (feedback between Q-events and links)?
/// Classify: FIXED NETWORK / PARTIAL FEEDBACK / FULL SELF-ORGANIZATION.
///
/// Tests: ATQG1150 (feedback + activity-driven connectivity), ATQG1151 (self-organized geometry +
/// structure-from-content), ATQG1152 (fixed vs adaptive + classification).
/// </summary>
public class ATQG_Phase115_StructureFromContentTests : ResearchTestBase
{
    public ATQG_Phase115_StructureFromContentTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1150: feedback + activity-driven connectivity ────────────────────────

    [Fact]
    public void ATQG1150_FeedbackAndActivityDrivenConnectivity()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1150: feedback between Q-events (activity) and links (structure)");

        double[] conc = StructureFromContent.ConcentratedActivity(96);
        double[,] fixedNet = StructureFromContent.FixedNetwork(conc);
        double[,] adaptive = StructureFromContent.AdaptiveNetwork(conc);

        int fixedLinks = StructureFromContent.LinkCount(fixedNet);
        int adaptiveLinks = StructureFromContent.LinkCount(adaptive);
        int fixedFamilies = StructureFromContent.FamilyCount(fixedNet);
        int adaptiveFamilies = StructureFromContent.FamilyCount(adaptive);
        double fixedSpan = StructureFromContent.HierarchySpan(fixedNet);
        double adaptiveSpan = StructureFromContent.HierarchySpan(adaptive);
        bool changes = StructureFromContent.FeedbackChangesGeometry(conc);

        sb.AppendLine("ACTIVITY-DRIVEN CONNECTIVITY (concentrated activity, N=96):");
        sb.AppendLine($"  fixed (one round, no feedback) : {fixedLinks} links, {fixedFamilies} families, span {fixedSpan:F2}");
        sb.AppendLine($"  adaptive (feedback loop)       : {adaptiveLinks} links, {adaptiveFamilies} families, span {adaptiveSpan:F2}");
        sb.AppendLine($"  feedback changes geometry      : {changes}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the feedback loop DOES change the geometry — active nodes create links and the");
        sb.AppendLine("degree feeds back into activity, so Q-events and links are genuinely coupled. Activity-driven");
        sb.AppendLine("connectivity exists.");
        Output.WriteLine(sb.ToString());

        Assert.True(adaptiveLinks > fixedLinks, "feedback grows the network (activity-driven connectivity)");
        Assert.True(changes, "feedback changes the geometry");
        Assert.True(adaptiveSpan > 1.0, "adaptive loop builds a structured (non-degenerate) network");
    }

    // ── ATQG1151: self-organized geometry + structure-from-content ───────────────

    [Fact]
    public void ATQG1151_SelfOrganizationAndStructureFromContent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1151: self-organized geometry and structure-from-content");

        double[] conc = StructureFromContent.ConcentratedActivity(96);
        double[] spread = StructureFromContent.SpreadActivity(96);
        double[] uniform = StructureFromContent.UniformActivity(96);

        double[,] cNet = StructureFromContent.AdaptiveNetwork(conc);
        double[,] sNet = StructureFromContent.AdaptiveNetwork(spread);
        double[,] uNet = StructureFromContent.AdaptiveNetwork(uniform);

        bool structured = StructureFromContent.LoopBuildsStructuredNetwork(conc);
        bool contentDependent = StructureFromContent.StructureDependsOnContent();
        bool uniformSelfOrg = StructureFromContent.UniformActivitySelfOrganizes();

        sb.AppendLine("SELF-ORGANIZED GEOMETRY (adaptive loop):");
        sb.AppendLine($"  concentrated content: {StructureFromContent.LinkCount(cNet)} links, {StructureFromContent.FamilyCount(cNet)} families, span {StructureFromContent.HierarchySpan(cNet):F2}");
        sb.AppendLine($"  spread content      : {StructureFromContent.LinkCount(sNet)} links, {StructureFromContent.FamilyCount(sNet)} families, span {StructureFromContent.HierarchySpan(sNet):F2}");
        sb.AppendLine($"  uniform content     : {StructureFromContent.LinkCount(uNet)} links, {StructureFromContent.FamilyCount(uNet)} families, span {StructureFromContent.HierarchySpan(uNet):F2}");
        sb.AppendLine($"  loop builds a bounded structured network: {structured}");
        sb.AppendLine($"  structure depends on content: {contentDependent}");
        sb.AppendLine($"  uniform content self-organizes a rich hierarchy: {uniformSelfOrg}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the loop builds a bounded structured network and DIFFERENT content gives DIFFERENT");
        sb.AppendLine("geometry (structure-from-content in the weak sense). BUT uniform (featureless) content does");
        sb.AppendLine("NOT self-organize a rich hierarchy — structure is content-driven, not emergent from nothing.");
        Output.WriteLine(sb.ToString());

        Assert.True(structured, "the feedback loop builds a bounded structured network");
        Assert.True(contentDependent, "different content gives different geometry (structure-from-content)");
        Assert.False(uniformSelfOrg, "uniform content does NOT self-organize a rich hierarchy (not full emergence)");
    }

    // ── ATQG1152: fixed vs adaptive + classification ─────────────────────────────

    [Fact]
    public void ATQG1152_FixedVsAdaptiveAndClassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1152: fixed vs adaptive networks → FIXED / PARTIAL FEEDBACK / FULL SELF-ORGANIZATION");

        double[] conc = StructureFromContent.ConcentratedActivity(96);
        double[,] fixedNet = StructureFromContent.FixedNetwork(conc);
        double[,] adaptive = StructureFromContent.AdaptiveNetwork(conc);
        string cls = StructureFromContent.Classify();

        sb.AppendLine("FIXED vs ADAPTIVE:");
        sb.AppendLine($"  fixed network    : {StructureFromContent.LinkCount(fixedNet)} links, {StructureFromContent.FamilyCount(fixedNet)} families");
        sb.AppendLine($"  adaptive network : {StructureFromContent.LinkCount(adaptive)} links, {StructureFromContent.FamilyCount(adaptive)} families");
        sb.AppendLine();
        sb.AppendLine($"CLASSIFICATION: {cls}");
        sb.AppendLine();
        sb.AppendLine("  • NOT FIXED NETWORK: the adaptive loop changes the geometry — activity drives connectivity.");
        sb.AppendLine("  • NOT FULL SELF-ORGANIZATION: uniform (featureless) content does NOT build a rich hierarchy");
        sb.AppendLine("    — the geometry is content- and seed-constrained, not emergent from nothing.");
        sb.AppendLine("  • PARTIAL FEEDBACK: content (actualization patterns) shapes structure via the feedback loop,");
        sb.AppendLine("    but the network does not fully self-organize from its own activity alone.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL FEEDBACK", cls);
        Assert.True(StructureFromContent.LinkCount(adaptive) > StructureFromContent.LinkCount(fixedNet),
            "adaptive network differs from fixed");
        Assert.True(StructureFromContent.FeedbackChangesGeometry(conc), "feedback changes geometry");
    }
}
