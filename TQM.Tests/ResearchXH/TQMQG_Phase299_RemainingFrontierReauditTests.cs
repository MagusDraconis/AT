using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 299 — Remaining Frontier Re-Audit. Reclassify every QG280 frontier item through the
/// QG281-298 lens (OPEN / PARTIAL / BOUNDARY / METHODOLOGY / CLOSED) and produce the final post-QG298
/// frontier. Focus: 5/4, ψ, temporal evidence, self-confirmation, publication, me anchor, structural
/// imports. Audit only.
/// </summary>
public class TQMQG_Phase299_RemainingFrontierReauditTests : ResearchTestBase
{
    public TQMQG_Phase299_RemainingFrontierReauditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2990_ClosedAndBoundaryItems()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2990: 5/4, me, and structural imports are CLOSED; ψ is BOUNDARY");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - 5/4 is CLOSED by QG298 (the first-peak boundary projection, not a free constant);");
        sb.AppendLine("  - me is CLOSED/reframed by QG289 (one of two calibration scales, replaceable);");
        sb.AppendLine("  - the structural imports are CLOSED by QG289-292 (η framework, π redundant, RG");
        sb.AppendLine("    removable, 3+1 derived);");
        sb.AppendLine("  - ψ is reclassified BOUNDARY (QG285/286 located it as the anisotropic face of");
        sb.AppendLine("    Difference — its fundamental status is a documented boundary).");
        sb.AppendLine();

        foreach (var i in RemainingFrontierReaudit.Items())
        {
            if (i.Status is RemainingFrontierReaudit.Status.Closed or RemainingFrontierReaudit.Status.Boundary)
                sb.AppendLine($"  [{i.Status.ToString().PadRight(11)}] {i.Name} — {i.ReauditNote}");
        }
        sb.AppendLine();
        sb.AppendLine($"closed: {RemainingFrontierReaudit.ClosedCount()}  boundary: {RemainingFrontierReaudit.BoundaryCount()}");
        sb.AppendLine($"changed by QG281-298: {RemainingFrontierReaudit.ChangedCount()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(3, RemainingFrontierReaudit.ClosedCount());   // 5/4, me, structural imports
        Assert.Equal(2, RemainingFrontierReaudit.BoundaryCount());  // ψ, Difference
        Assert.Equal(4, RemainingFrontierReaudit.ChangedCount());   // 5/4, me, imports, ψ
    }

    [Fact]
    public void TQMQG2991_RemainingOpenPartialMethodology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2991: the remaining exact issues");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - OPEN (1): independent temporal evidence (the 6.7% binding constraint);");
        sb.AppendLine("  - PARTIAL (1): SM value gaps (Bekenstein 2π, Λ value, H epoch);");
        sb.AppendLine("  - METHODOLOGY (2): self-confirmation, publication.");
        sb.AppendLine();

        sb.AppendLine($"OPEN: {RemainingFrontierReaudit.OpenCount()}");
        sb.AppendLine($"PARTIAL: {RemainingFrontierReaudit.PartialCount()}");
        sb.AppendLine($"METHODOLOGY: {RemainingFrontierReaudit.MethodologyCount()}");
        sb.AppendLine();
        sb.AppendLine("REMAINING EXACT ISSUES:");
        foreach (var r in RemainingFrontierReaudit.RemainingExactIssues())
            sb.AppendLine($"  - {r}");
        sb.AppendLine();
        sb.AppendLine($"frontier closure verified: {RemainingFrontierReaudit.FrontierClosureVerified()}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(1, RemainingFrontierReaudit.OpenCount());       // temporal evidence
        Assert.Equal(1, RemainingFrontierReaudit.PartialCount());    // SM gaps
        Assert.Equal(2, RemainingFrontierReaudit.MethodologyCount()); // self-confirmation, publication
        Assert.True(RemainingFrontierReaudit.FrontierClosureVerified());
        Assert.Equal(6, RemainingFrontierReaudit.RemainingExactIssues().Length);
    }

    [Fact]
    public void TQMQG2992_Summary()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2992: the final post-QG298 frontier");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the QG281-298 program closed/reframed the physics frontier items it could");
        sb.AppendLine("    resolve; the remaining exact issues are external validation, SM value gaps,");
        sb.AppendLine("    the documented boundaries, and the methodology items.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {RemainingFrontierReaudit.Summary()}");
        sb.AppendLine($"Re-audit score: {RemainingFrontierReaudit.ReauditScore()}/5");
        sb.AppendLine($"open={RemainingFrontierReaudit.OpenCount()} partial={RemainingFrontierReaudit.PartialCount()} boundary={RemainingFrontierReaudit.BoundaryCount()} methodology={RemainingFrontierReaudit.MethodologyCount()} closed={RemainingFrontierReaudit.ClosedCount()}");
        sb.AppendLine();
        sb.AppendLine("THE FINAL POST-QG298 FRONTIER:");
        foreach (var i in RemainingFrontierReaudit.Items())
            sb.AppendLine($"  [{i.Status.ToString().PadRight(11)}] R{i.Rank} {i.Name}");

        Output.WriteLine(sb.ToString());

        Assert.True(RemainingFrontierReaudit.ReauditScore() >= 5);
        Assert.Contains("OPEN 1", RemainingFrontierReaudit.Summary());
        Assert.Contains("CLOSED 3", RemainingFrontierReaudit.Summary());
    }
}
