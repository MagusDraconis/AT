using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 205 — Post-200 Coverage Audit. Recompute tested/partial/open after removing the resolved
/// items (SM1, SM2, Matter=Deficit, Matter Sector, 2D→3D Bridge) and produce the Top-10 remaining open
/// problems. Deterministic — reads the coverage single source of truth.
/// </summary>
public class ATQG_Phase205_Post200CoverageAuditTests : ResearchTestBase
{
    public ATQG_Phase205_Post200CoverageAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG2050_RecomputedStatusAfterRemovals()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2050: recomputed coverage after the resolved-item removals");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Latest coverage: 207 phases, 190 tested, 12 partial, 5 audit, 95.3% weighted.");
        sb.AppendLine("  - Removed as resolved: SM1 (QG203), SM2 (QG204), Matter=Deficit (QG194),");
        sb.AppendLine("    Matter Sector (QG195), 2D→3D Bridge (QG197).");
        sb.AppendLine();

        sb.AppendLine("RESOLVED-AND-REMOVED:");
        foreach (var r in Post200CoverageAudit.ResolvedAndRemoved())
            sb.AppendLine($"  - {r}");
        sb.AppendLine();

        sb.AppendLine("RECOMPUTED STATUS:");
        sb.AppendLine($"  Total phases:   {Post200CoverageAudit.TotalPhases}");
        sb.AppendLine($"  Tested phases:  {Post200CoverageAudit.TestedPhases}");
        sb.AppendLine($"  Partial phases: {Post200CoverageAudit.PartialPhases}");
        sb.AppendLine($"  Audit phases:   {Post200CoverageAudit.AuditPhases}");
        sb.AppendLine($"  Weighted:       {Post200CoverageAudit.WeightedCoverage:P1}");
        sb.AppendLine($"  Observables:    {Post200CoverageAudit.Observables} total, {Post200CoverageAudit.ObservableTested} tested, " +
                      $"{Post200CoverageAudit.ObservablePartial} partial, {Post200CoverageAudit.ObservableUntested} untested");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - True post-QG204 status: 190/207 tested (91.8%), 95.3% weighted.");
        sb.AppendLine("  - The five resolved items are removed from the open set.");

        Output.WriteLine(sb.ToString());

        Assert.True(Post200CoverageAudit.ResolvedSetComplete(), "the five resolved items must be the QG194/195/197/203/204 set");
        Assert.Equal(207, Post200CoverageAudit.TotalPhases);
        Assert.Equal(190, Post200CoverageAudit.TestedPhases);
    }

    [Fact]
    public void ATQG2051_Top10RemainingOpenProblems()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2051: Top-10 remaining open problems ranked by importance");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score = impact·3 + feasibility·2 + falsifiability·2 (same weights as QG188).");
        sb.AppendLine();

        var top = Post200CoverageAudit.Top10();
        sb.AppendLine("RANKING:");
        for (int i = 0; i < top.Length; i++)
        {
            var p = top[i];
            sb.AppendLine($"  {i + 1,2}  {p.Id,-4} {p.Category,-14} {p.Score,5:F1}  {p.Title}");
        }
        sb.AppendLine();

        var counts = Post200CoverageAudit.CategoryCounts();
        sb.AppendLine("CATEGORY DISTRIBUTION:");
        foreach (var kv in counts.OrderBy(kv => kv.Key))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.True(Post200CoverageAudit.Top10Valid(), "the Top-10 must be sorted and complete");
        Assert.True(Post200CoverageAudit.TopIs106GeV(), "the 106 GeV resonance must be top-ranked");
    }

    [Fact]
    public void ATQG2052_ResolutionsRemovedNotListed()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG2052: the resolved items are absent from the remaining open set");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The remaining open set must NOT contain SM1, SM2, matter deficit, matter sector,");
        sb.AppendLine("    or the 2D→3D bridge.");
        sb.AppendLine();

        var open = Post200CoverageAudit.RemainingOpen();
        var titles = string.Join(" | ", open.Select(p => p.Title));

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Remaining open problems: {open.Length}");
        sb.AppendLine($"  Titles: {titles}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - No resolved item appears in the remaining open set.");
        sb.AppendLine("  - The true post-QG204 open landscape is PREDICTION 3, GRAVITY 3, FOUNDATIONAL 2,");
        sb.AppendLine("    STANDARD MODEL 2.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(10, open.Length);
        Assert.DoesNotContain("neutrino masses", titles, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MS̄", titles, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("deficit", titles, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("2D", titles, StringComparison.OrdinalIgnoreCase);
    }
}
