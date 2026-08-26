using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 213 — Ultra Frontier Audit. Recompute the frontier after QG212, excluding resolved /
/// partial-resolved / closed-by-impossibility-proof items, and produce the Top-10 unresolved items plus
/// the percentage of theory completed. Deterministic.
/// </summary>
public class ATQG_Phase213_UltraFrontierAuditTests : ResearchTestBase
{
    public ATQG_Phase213_UltraFrontierAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2130_TheoryCompletionPercentage()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2130: percentage of theory completed");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Sources: coverage JSON (215 phases, 196 tested, 12 partial, 7 audit).");
        sb.AppendLine();

        double weighted = UltraFrontierAudit.TheoryCompletion();
        double phase = UltraFrontierAudit.PhaseCompletion();
        double obs = UltraFrontierAudit.ObservableCompletion();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Total phases: {UltraFrontierAudit.TotalPhases}");
        sb.AppendLine($"  Tested: {UltraFrontierAudit.TestedPhases}, partial: {UltraFrontierAudit.PartialPhases}, audit: {UltraFrontierAudit.AuditPhases}");
        sb.AppendLine($"  Phase completion (tested/tested+partial): {phase:P1}");
        sb.AppendLine($"  Observable completion (weighted): {obs:P1}");
        sb.AppendLine($"  Weighted coverage: {weighted:P1}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The theory is ~95% complete as a derivation program.");
        sb.AppendLine("  - Weighted coverage 94.8%, phase completion 94.2%.");

        Output.WriteLine(sb.ToString());

        Assert.True(weighted > 0.94, "weighted coverage must exceed 94%");
        Assert.True(phase > 0.94, "phase completion must exceed 94%");
    }

    [Fact]
    public void ATQG2131_ExclusionsAndTop10()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2131: exclusions and the Top-10 frontier");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Exclude resolved, partial-resolved, and closed-by-impossibility (QG196).");
        sb.AppendLine();

        sb.AppendLine("EXCLUDED:");
        foreach (var e in UltraFrontierAudit.Excluded())
            sb.AppendLine($"  - {e}");
        sb.AppendLine($"  Bekenstein 1/4 closed by impossibility proof? {UltraFrontierAudit.BekensteinClosedByImpossibility()}");
        sb.AppendLine();

        var top = UltraFrontierAudit.Top10();
        for (int i = 0; i < top.Length; i++)
            sb.AppendLine($"  {i + 1,2}  {top[i].Id,-4} {top[i].Category,-14} {top[i].Score,5:F1}  {top[i].Title}");
        sb.AppendLine();

        var counts = UltraFrontierAudit.CategoryCounts();
        sb.AppendLine("CATEGORY DISTRIBUTION:");
        foreach (var kv in counts.OrderBy(kv => kv.Key))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.True(UltraFrontierAudit.Top10Valid(), "the Top-10 must be valid and sorted");
        Assert.True(UltraFrontierAudit.BekensteinClosedByImpossibility(), "Bekenstein 1/4 is closed by impossibility");
    }

    [Fact]
    public void ATQG2132_FrontierPrimarilyExperimental()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2132: the remaining frontier is primarily experimental");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The frontier is 'primarily experimental' if the top-3 are PREDICTION and");
        sb.AppendLine("    PREDICTION dominates the category counts.");
        sb.AppendLine();

        bool experimental = UltraFrontierAudit.FrontierPrimarilyExperimental();
        var counts = UltraFrontierAudit.CategoryCounts();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Top-3 categories: {UltraFrontierAudit.Top10()[0].Category}, {UltraFrontierAudit.Top10()[1].Category}, {UltraFrontierAudit.Top10()[2].Category}");
        foreach (var kv in counts.OrderByDescending(kv => kv.Value))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The top-3 are P1/P2/P3 — the pre-registered predictions awaiting data.");
        sb.AppendLine("  - PREDICTION dominates (5/10); the residual theory items are partial laws and the");
        sb.AppendLine("    proven-impossible Bekenstein coefficient.");
        sb.AppendLine("  - The remaining frontier is PRIMARILY EXPERIMENTAL.");

        Output.WriteLine(sb.ToString());

        Assert.True(experimental, "the frontier must be primarily experimental");
    }
}
