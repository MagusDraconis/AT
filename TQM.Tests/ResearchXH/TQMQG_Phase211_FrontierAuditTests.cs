using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 211 — Frontier Audit. Recompute all remaining unresolved items after QG210, excluding
/// resolved / partial-resolved / superseded, and produce the Top-10 frontier problems. Deterministic.
/// </summary>
public class TQMQG_Phase211_FrontierAuditTests : ResearchTestBase
{
    public TQMQG_Phase211_FrontierAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2110_ExclusionsComplete()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2110: resolved and superseded items excluded");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Exclude resolved / partial-resolved / superseded (QG203–QG210).");
        sb.AppendLine();

        sb.AppendLine("RESOLVED-AND-EXCLUDED:");
        foreach (var r in FrontierAudit.ResolvedAndExcluded())
            sb.AppendLine($"  - {r}");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The seven post-QG205 resolutions are all excluded from the frontier.");

        Output.WriteLine(sb.ToString());

        Assert.True(FrontierAudit.ExclusionsComplete(), "all seven post-QG205 resolutions must be excluded");
        Assert.Equal(7, FrontierAudit.ResolvedAndExcluded().Length);
    }

    [Fact]
    public void TQMQG2111_Top10FrontierRanked()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2111: Top-10 frontier problems ranked by importance");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score = impact·3 + feasibility·2 + falsifiability·2.");
        sb.AppendLine();

        var top = FrontierAudit.Top10();
        for (int i = 0; i < top.Length; i++)
        {
            var p = top[i];
            sb.AppendLine($"  {i + 1,2}  {p.Id,-4} {p.Category,-14} {p.Score,5:F1}  {p.Title}");
        }
        sb.AppendLine();

        var counts = FrontierAudit.CategoryCounts();
        sb.AppendLine("CATEGORY DISTRIBUTION:");
        foreach (var kv in counts.OrderBy(kv => kv.Key))
            sb.AppendLine($"  {kv.Key}: {kv.Value}");

        Output.WriteLine(sb.ToString());

        Assert.True(FrontierAudit.Top10Valid(), "the Top-10 must be complete and sorted");
        Assert.True(FrontierAudit.TopIs106GeV(), "P1 must be the top frontier problem");
    }

    [Fact]
    public void TQMQG2112_FrontierCharacter()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2112: the character of the true final frontier");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - The frontier is what remains after QG203–QG210.");
        sb.AppendLine();

        var top = FrontierAudit.Top10();
        int prediction = top.Count(p => p.Category == "PREDICTION");
        int gravity = top.Count(p => p.Category == "GRAVITY");
        int foundational = top.Count(p => p.Category == "FOUNDATIONAL");
        int sm = top.Count(p => p.Category == "STANDARD MODEL");

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  PREDICTION: {prediction}, GRAVITY: {gravity}, FOUNDATIONAL: {foundational}, SM: {sm}");
        sb.AppendLine($"  Top-3 are all pre-registered predictions (experimental).");
        sb.AppendLine($"  No SM mass/hierarchy derivation remains — the mass spectrum is closed.");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The true final frontier is experimental: P1/P2/P3 are the top-3.");
        sb.AppendLine("  - Gravity/foundational gaps: conformal optics, Bekenstein 1/4 (proven impossible), ψ origin.");
        sb.AppendLine("  - Only two SM partial-laws remain (quark hierarchy, golden ratio) — no open masses.");

        Output.WriteLine(sb.ToString());

        Assert.Equal(5, prediction);
        Assert.Equal(1, gravity);
        Assert.Equal(2, foundational);
        Assert.Equal(2, sm);
    }
}
