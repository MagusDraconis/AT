using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 280 — Final Frontier Inventory. Review QG223-QG279, classify every remaining issue, and
/// produce the definitive post-QG279 frontier list ranked by importance. Inventory only, no derivations.
/// </summary>
public class TQMQG_Phase280_FinalFrontierInventoryTests : ResearchTestBase
{
    public TQMQG_Phase280_FinalFrontierInventoryTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2800_Inventory()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2800: the definitive post-QG279 frontier list");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - RESOLVED = fully addressed; REFRAMED = interpretation changed;");
        sb.AppendLine("  - BOUNDARY = genuine limit (documented); PARTIAL = partially derived;");
        sb.AppendLine("  - OPEN = genuine remaining research question.");
        sb.AppendLine();

        foreach (var i in FinalFrontierInventory.Items())
            sb.AppendLine($"  R{i.Rank} [{i.Status,-8}] {i.Name}");
        sb.AppendLine();
        var c = FinalFrontierInventory.StatusCounts();
        sb.AppendLine($"Counts: OPEN={c[FinalFrontierInventory.Status.Open]}, "
            + $"PARTIAL={c[FinalFrontierInventory.Status.Partial]}, "
            + $"BOUNDARY={c[FinalFrontierInventory.Status.Boundary]}, "
            + $"REFRAMED={c[FinalFrontierInventory.Status.Reframed]}, "
            + $"RESOLVED={c[FinalFrontierInventory.Status.Resolved]}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, FinalFrontierInventory.Items().Length);
        Assert.True(FinalFrontierInventory.OpenCount() >= 4, "genuine remaining research questions");
        Assert.True(FinalFrontierInventory.BoundaryCount() >= 3, "documented boundaries");
        Assert.True(FinalFrontierInventory.ResolvedCount() == 0, "inventory of the remaining frontier only");
    }

    [Fact]
    public void TQMQG2801_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2801: the classification of each remaining issue");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - the classification reflects the QG223-279 reduction state.");
        sb.AppendLine();

        foreach (var i in FinalFrontierInventory.Items())
        {
            sb.AppendLine($"  R{i.Rank} [{i.Status,-8}] {i.Name}");
            sb.AppendLine($"          {i.Note}");
        }
        sb.AppendLine();
        sb.AppendLine("The frontier is dominated by: OPEN items (assignment, temporal evidence,");
        sb.AppendLine("self-confirmation, 5/4 exception, ψ, publication) and BOUNDARY items");
        sb.AppendLine("(me anchor, structural imports, Difference). No item is RESOLVED in the");
        sb.AppendLine("remaining frontier — the inventory lists what is still to be addressed.");

        Output.WriteLine(sb.ToString());

        // The primary frontier is the assignment step.
        Assert.Equal("Assignment frontier (operator → physics labels)", FinalFrontierInventory.TopItem().Name);
        Assert.Equal(FinalFrontierInventory.Status.Open, FinalFrontierInventory.TopItem().Status);
        // The Difference boundary is a genuine boundary, not an open problem.
        var diff = FinalFrontierInventory.Items().Single(i => i.Name.Contains("Difference boundary"));
        Assert.Equal(FinalFrontierInventory.Status.Boundary, diff.Status);
    }

    [Fact]
    public void TQMQG2802_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2802: the definitive frontier summary");

        sb.AppendLine($"SUMMARY: {FinalFrontierInventory.Summary()}");
        sb.AppendLine();
        sb.AppendLine("THE TOP REMAINING RESEARCH QUESTIONS (ranked):");
        sb.AppendLine("  R1 the ASSIGNMENT frontier (operator → physics labels; the relational-subclass role);");
        sb.AppendLine("  R2 independent temporal evidence (the 6.7% binding constraint — future measurement);");
        sb.AppendLine("  R3 self-confirmation (external arbitration of the validation architecture);");
        sb.AppendLine("  R4 the 5/4 exception (the meta-level inconsistency in the selection rules);");
        sb.AppendLine("  R5 the me anchor (derive 0.511 from D96, or document as a permanent boundary);");
        sb.AppendLine("  R6 the ψ primitive (derive the tensor sector, or document as a boundary);");
        sb.AppendLine("  R7 the structural imports (conformal η, Bekenstein π, RG, 3+1);");
        sb.AppendLine("  R8 the SM remaining gaps (Bekenstein coefficient, Λ magnitude, H).");
        sb.AppendLine();
        sb.AppendLine("The Difference boundary (R9) is a genuine BOUNDARY, not an open problem — the");
        sb.AppendLine("theory's first concept, irreducible by design (QG278-279).");

        Output.WriteLine(sb.ToString());

        Assert.Contains("ASSIGNMENT", FinalFrontierInventory.Summary());
        Assert.Contains("Difference", FinalFrontierInventory.Summary());
    }
}
