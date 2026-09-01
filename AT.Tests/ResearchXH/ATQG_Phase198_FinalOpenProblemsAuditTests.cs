using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 198 — Final Open Problems Audit. Enumerate ALL unresolved physics questions from the
/// physics-coverage single source of truth and the prediction registry, excluding resolved / partial-resolved /
/// audit entries. Classify each (FOUNDATIONAL / GRAVITY / STANDARD MODEL / PREDICTION) with why-open,
/// blocking impact, and priority. Output: Top-20 open problems ranked by importance. Deterministic.
/// </summary>
public class ATQG_Phase198_FinalOpenProblemsAuditTests : ResearchTestBase
{
    public ATQG_Phase198_FinalOpenProblemsAuditTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void ATQG1980_CatalogIsCompleteAndExclusionsRespected()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1980: the complete unresolved-problem catalog (exclusions respected)");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Sources: Docs/ATQG_PhysicsCoverage.json (open_questions, observables, gr_topics)");
        sb.AppendLine("    and Docs/ATQG_Predictions.json (registered predictions, outcome = null ⇒ PENDING).");
        sb.AppendLine("  - Excluded: RESOLVED (QG194/195/197), PARTIALLY-SOLVED (ψ/Weyl register), and audit-only entries.");
        sb.AppendLine("  - Classification: FOUNDATIONAL, GRAVITY, STANDARD MODEL, PREDICTION.");
        sb.AppendLine();

        var all = OpenProblemsFinalAudit.All();
        var counts = OpenProblemsFinalAudit.CategoryCounts();
        var valid = OpenProblemsFinalAudit.CatalogValid();

        sb.AppendLine("INTERMEDIATE CALCULATIONS:");
        sb.AppendLine($"  Total open problems: {all.Length}");
        sb.AppendLine($"  Categories present (must be 4): {counts.Count}");
        foreach (var kv in counts.OrderBy(kv => kv.Key))
            sb.AppendLine($"    {kv.Key}: {kv.Value}");
        sb.AppendLine($"  Exclusions respected (no RESOLVED / no partial-resolved / no empty fields)? {OpenProblemsFinalAudit.NoResolvedOrPartialResolved()}");
        sb.AppendLine();

        sb.AppendLine("THE CATALOG:");
        foreach (var p in all.OrderBy(p => p.Category).ThenBy(p => p.Id))
            sb.AppendLine($"  [{p.Category,-14}] {p.Id} {p.Title}  ({p.Phase})");
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - The audit enumerates every unresolved physics question the framework leaves open.");
        sb.AppendLine("  - All four categories are populated: the open problems span foundations, gravity,");
        sb.AppendLine("    the standard model, and the pre-registered predictions.");
        sb.AppendLine($"  - Catalog valid (20 entries, all categories, exclusions respected)? {valid}");

        Output.WriteLine(sb.ToString());

        Assert.Equal(20, all.Length);
        Assert.True(valid, "catalog must have 20 entries, all four categories, and no excluded statuses");
    }

    [Fact]
    public void ATQG1981_Top20RankedByImportance()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1981: Top-20 open problems ranked by importance");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - score = impact·3 + feasibility·2 + falsifiability·2 (deterministic weights, same as QG188).");
        sb.AppendLine("  - Priority bands: HIGH ≥ 30, MEDIUM 18–29, LOW < 18.");
        sb.AppendLine();

        var top = OpenProblemsFinalAudit.Top20();
        sb.AppendLine("RANKING (descending importance):");
        sb.AppendLine("  #  Id   Category        Priority  Score  Title");
        for (int i = 0; i < top.Length; i++)
        {
            var p = top[i];
            sb.AppendLine($"  {i + 1,2}  {p.Id,-4} {p.Category,-14} {p.Priority,-6} {p.Score,5:F1}  {p.Title}");
        }
        sb.AppendLine();

        var rec = OpenProblemsFinalAudit.RecommendedNextTarget();
        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine($"  - Top-ranked: {rec.Id} {rec.Title} (score {rec.Score:F1}).");
        sb.AppendLine($"  - The 106 GeV resonance is the single most important open problem? {rec.Id == "P1"}");
        sb.AppendLine($"  - Priority counts: {string.Join(", ", OpenProblemsFinalAudit.PriorityCounts().Select(kv => $"{kv.Key}={kv.Value}"))}");

        Output.WriteLine(sb.ToString());

        Assert.Equal("P1", rec.Id, ignoreCase: true);
        Assert.True(OpenProblemsFinalAudit.TopIs106GeV(), "the 106 GeV resonance must be the top-ranked open problem");
        Assert.True(OpenProblemsFinalAudit.CatalogValid(), "the ranking must be sorted and complete");
    }

    [Fact]
    public void ATQG1982_EveryProblemHasReasonBlockingAndPriority()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1982: why-open, blocking impact, and priority for every problem");

        sb.AppendLine("ASSUMPTIONS:");
        sb.AppendLine("  - Every entry must state WHY it is open and WHAT it blocks (no empty fields).");
        sb.AppendLine("  - Priority is derived from the deterministic score (impact·3 + feasibility·2 + falsifiability·2).");
        sb.AppendLine();

        var all = OpenProblemsFinalAudit.All();
        sb.AppendLine("DETAIL (why open → blocking impact):");
        foreach (var p in all.OrderBy(p => p.Score).Reverse())
        {
            sb.AppendLine($"  {p.Id} [{p.Priority}] {p.Title}");
            sb.AppendLine($"      why open : {p.WhyOpen}");
            sb.AppendLine($"      blocks   : {p.BlockingImpact}");
        }
        sb.AppendLine();

        sb.AppendLine("CONCLUSIONS:");
        sb.AppendLine("  - Every open problem has a documented reason, a blocking impact, and a priority.");
        sb.AppendLine("  - The blocking impacts cluster on: electroweak validation (P1), neutrino sector");
        sb.AppendLine("    closure (SM1/SM3/P2), gravitational tensor sector (G1/G3/G6), and the geometric");
        sb.AppendLine("    foundation (F1/F2).");

        Output.WriteLine(sb.ToString());

        Assert.True(all.All(p => !string.IsNullOrWhiteSpace(p.WhyOpen)), "every problem must state why it is open");
        Assert.True(all.All(p => !string.IsNullOrWhiteSpace(p.BlockingImpact)), "every problem must state its blocking impact");
        Assert.Contains("HIGH", all.Select(p => p.Priority));
        Assert.Contains("MEDIUM", all.Select(p => p.Priority));
    }
}
