using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 284 — Post-Assignment Frontier Audit. Which frontier items remain after the assignment
/// closure (QG283)? Re-classify as OPEN / PARTIAL / BOUNDARY / METHODOLOGY. Audit only.
/// </summary>
public class ATQG_Phase284_PostAssignmentFrontierAuditTests : ResearchTestBase
{
    public ATQG_Phase284_PostAssignmentFrontierAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2840_RemainingFrontier()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2840: the frontier after assignment closure");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the QG280 frontier list re-audited after QG283 ASSIGNMENT CLOSED;");
        sb.AppendLine("  - OPEN = physics question; PARTIAL = partially derived; BOUNDARY = genuine limit;");
        sb.AppendLine("    METHODOLOGY = not a physics frontier (needs external arbitration).");
        sb.AppendLine();

        foreach (var i in PostAssignmentFrontierAudit.Items())
            sb.AppendLine($"  R{i.Rank} [{i.Status,-11}] {i.Name}");
        sb.AppendLine();
        sb.AppendLine($"ASSIGNMENT CLOSED (removed): {PostAssignmentFrontierAudit.AssignmentClosure().Name}");
        var c = PostAssignmentFrontierAudit.StatusCounts();
        sb.AppendLine($"Counts: OPEN={c[PostAssignmentFrontierAudit.Status.Open]}, "
            + $"PARTIAL={c[PostAssignmentFrontierAudit.Status.Partial]}, "
            + $"BOUNDARY={c[PostAssignmentFrontierAudit.Status.Boundary]}, "
            + $"METHODOLOGY={c[PostAssignmentFrontierAudit.Status.Methodology]}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(9, PostAssignmentFrontierAudit.TotalRemaining());
        Assert.True(PostAssignmentFrontierAudit.OpenCount() >= 2, "genuine physics open items remain");
        Assert.True(PostAssignmentFrontierAudit.MethodologyCount() >= 2, "methodology items identified");
    }

    [Fact]
    public void ATQG2841_Reclassification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2841: the reclassification after QG283");

        sb.AppendLine("HYPOTHESIS: the assignment closure (QG283) removed the primary structural question;");
        sb.AppendLine("the remaining items are reclassified appropriately.");
        sb.AppendLine();

        foreach (var i in PostAssignmentFrontierAudit.Items())
        {
            sb.AppendLine($"  R{i.Rank} [{i.Status,-11}] {i.Name}");
            sb.AppendLine($"          {i.Note}");
        }

        Output.WriteLine(sb.ToString());

        // Self-confirmation and publication are METHODOLOGY, not physics.
        Assert.Equal(PostAssignmentFrontierAudit.Status.Methodology,
            PostAssignmentFrontierAudit.Items().Single(i => i.Name.Contains("Self-confirmation")).Status);
        Assert.Equal(PostAssignmentFrontierAudit.Status.Methodology,
            PostAssignmentFrontierAudit.Items().Single(i => i.Name.Contains("Publication")).Status);
        // The 5/4 exception remains OPEN (not touched by the assignment closure).
        Assert.Equal(PostAssignmentFrontierAudit.Status.Open,
            PostAssignmentFrontierAudit.Items().Single(i => i.Name.Contains("5/4")).Status);
        // The ψ primitive remains OPEN.
        Assert.Equal(PostAssignmentFrontierAudit.Status.Open,
            PostAssignmentFrontierAudit.Items().Single(i => i.Name.Contains("ψ")).Status);
    }

    [Fact]
    public void ATQG2842_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2842: the final remaining frontier");

        sb.AppendLine($"SUMMARY: {PostAssignmentFrontierAudit.Summary()}");
        sb.AppendLine();
        sb.AppendLine("THE FINAL REMAINING FRONTIER (after assignment closure):");
        sb.AppendLine("  OPEN (3): independent temporal evidence, the 5/4 exception, the ψ primitive;");
        sb.AppendLine("  PARTIAL (1): SM remaining gaps (Bekenstein, Λ, H);");
        sb.AppendLine("  BOUNDARY (3): me anchor, structural imports, Difference boundary;");
        sb.AppendLine("  METHODOLOGY (2): self-confirmation, publication/arbitration.");
        sb.AppendLine();
        sb.AppendLine("The assignment closure removed the PRIMARY structural question. What remains is");
        sb.AppendLine("external validation (temporal evidence), the 5/4 meta-inconsistency, the ψ");
        sb.AppendLine("primitive, and the documented boundaries + methodology items.");

        Output.WriteLine(sb.ToString());

        Assert.Contains("CLOSED", PostAssignmentFrontierAudit.Summary());
        Assert.Contains("METHODOLOGY", PostAssignmentFrontierAudit.Summary());
    }
}
