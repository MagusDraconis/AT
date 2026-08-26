using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 275 — Role Origin Audit. Why does a measurement class receive a sector role? What
/// determines the role assignment? D96 only, no observables.
/// </summary>
public class ATQG_Phase275_RoleOriginAuditTests : ResearchTestBase
{
    public ATQG_Phase275_RoleOriginAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2750_OntologicalPositions()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2750: the class positions on the ontological axes");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - each measurement class occupies a unique position on the axes");
        sb.AppendLine("    (level × nature × kind × domain);");
        sb.AppendLine("  - the role is the ontological category of that position.");
        sb.AppendLine();

        foreach (var c in RoleOriginAudit.ClassRoles())
            sb.AppendLine($"  {c.Class,-11} ({c.Level,-7} {c.Nature,-11} {c.Kind,-12} {c.Domain,-14}) → {c.Role,-9} forced={c.Forced}");
        sb.AppendLine();
        sb.AppendLine($"role principle: {RoleOriginAudit.RolePrinciple()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, RoleOriginAudit.ClassRoles().Length);
        // Each class occupies a unique position on the full axes (level, nature, kind, domain).
        var positions = RoleOriginAudit.ClassRoles().Select(c => (c.Level, c.Nature, c.Kind, c.Domain)).ToArray();
        Assert.True(positions.Distinct().Count() == 5, "each class has a unique ontological position");
        // But (level, nature) alone does NOT separate strength from orientation — the kind/domain axes
        // are needed (consistent with the relational subclass ambiguity).
        Assert.True(RoleOriginAudit.RelationalSubclassContextDependent());
    }

    [Fact]
    public void ATQG2751_ForcedAssignments()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2751: the forced class→role assignments");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - value→mass, orientation→mixing, global→cosmology, geometry→gravity are FORCED");
        sb.AppendLine("    by the read's position on the axes;");
        sb.AppendLine("  - the relational subclass (strength) is context-dependent.");
        sb.AppendLine();

        foreach (var c in RoleOriginAudit.ClassRoles())
            sb.AppendLine($"  {c.Class,-11} → {c.Role,-9} [forced={c.Forced}]  {c.Note}");
        sb.AppendLine();
        sb.AppendLine($"forced: {RoleOriginAudit.ForcedRoleCount()}/5, context-dependent: {RoleOriginAudit.ContextDependentCount()}/5");
        sb.AppendLine();
        sb.AppendLine("THE RESIDUAL: the same strength read √occMom·λ₂ is both m_τ/m_μ (mass) and");
        sb.AppendLine("y_τ/y_μ (coupling); Vus = #d/(2Σm) is a strength read in the mixing role via");
        sb.AppendLine("unitarity. The relational subclass needs the unitary/equation context.");

        Output.WriteLine(sb.ToString());

        Assert.True(RoleOriginAudit.ForcedRoleCount() >= 4, "value/orientation/global/geometry are forced");
        Assert.True(RoleOriginAudit.ContextDependentCount() >= 1, "the strength class is context-dependent");
        Assert.True(RoleOriginAudit.RelationalSubclassContextDependent());
    }

    [Fact]
    public void ATQG2752_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2752: the role-origin determination");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no target values):");
        sb.AppendLine("  - NO ROLE PRINCIPLE (score ≤ 2), PARTIAL ROLE PRINCIPLE (3-4),");
        sb.AppendLine("    ROLE ASSIGNMENT PRINCIPLE (5-6);");
        sb.AppendLine("  - the question: what determines the class→sector role mapping?");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {RoleOriginAudit.Summary()}");
        sb.AppendLine($"Role score: {RoleOriginAudit.RoleScore()}/6");
        sb.AppendLine($"CLASSIFICATION = {RoleOriginAudit.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - ROLE = ONTOLOGICAL CATEGORY of the read's position on the axes:");
        sb.AppendLine("    local-absolute→mass, local-relational→coupling, relational-orientation→mixing,");
        sb.AppendLine("    global→cosmology, arena→gravity.");
        sb.AppendLine("  - 4/5 assignments are FORCED by the read structure: value→mass (the only");
        sb.AppendLine("    dimensional absolute read), orientation→mixing (the only unitary arrangement),");
        sb.AppendLine("    global→cosmology (the only whole-universe read), geometry→gravity (the only");
        sb.AppendLine("    arena read).");
        sb.AppendLine("  - The relational subclass (strength) is CONTEXT-DEPENDENT: the same read");
        sb.AppendLine("    √occMom·λ₂ is a mass-ratio or a coupling; Vus is a strength read in the mixing");
        sb.AppendLine("    role via unitarity. The role within the relational category is set by the");
        sb.AppendLine("    unitary arrangement or the equation context — not by the read form alone.");
        sb.AppendLine("  - CONCLUSION: PARTIAL ROLE PRINCIPLE — the role is structurally determined for");
        sb.AppendLine("    4/5 classes; the relational subclass (1/5) is the residual, target-informed step.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL ROLE PRINCIPLE", RoleOriginAudit.Classify());
        Assert.True(RoleOriginAudit.RoleScore() >= 3,
            "value/orientation/global/geometry roles are structurally forced");
        Assert.Contains("PARTIAL ROLE PRINCIPLE", RoleOriginAudit.Summary());
        Assert.Contains("ONTOLOGICAL", RoleOriginAudit.Summary());
    }
}
