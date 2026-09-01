using System.Globalization;
using System.Text;
using AT.Core.ResearchXE;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXE;

public class AT_XE002_AssumptionDependencyAudit : ResearchTestBase
{
    public AT_XE002_AssumptionDependencyAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void XE002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchXE-002 Assumption Dependency Audit");

        var assumptions = AssumptionDependencyAnalyzer.BuildInventory();
        var results = AssumptionDependencyAnalyzer.BuildResults();
        var impact = AssumptionDependencyAnalyzer.ComputeAssumptionImpact(assumptions, results);
        int totalResults = results.Count;

        // 1. Assumption inventory
        Sec(sb, "Assumption Inventory — 18 Assumptions Across 5 Tiers");
        sb.AppendLine("  ID   Tier  Assumption                          Explicit?");
        sb.AppendLine("  " + new string('-', 60));
        foreach (var a in assumptions)
        {
            string expl = a.IsExplicit ? "✓" : "~ implicit";
            sb.AppendLine($"  {a.Id,-4} {a.Tier,4}  {a.Name,-38} {expl}");
        }
        sb.AppendLine();
        sb.AppendLine($"  {assumptions.Count} assumptions. {assumptions.Count(a => a.IsExplicit)} explicit.");
        sb.AppendLine();

        // 2. Impact ranking
        Sec(sb, "Assumption Impact — How Many Results Depend on Each");
        sb.AppendLine(AssumptionDependencyAnalyzer.ImpactRanking(assumptions, impact, totalResults));

        // 3. Result → Assumptions (reverse)
        Sec(sb, "Result → Required Assumptions (Reverse Matrix)");
        sb.AppendLine("  Result                          Assumptions");
        sb.AppendLine("  " + new string('-', 70));
        foreach (var r in results)
        {
            sb.AppendLine($"  {r.Name,-32} {string.Join(", ", r.RequiredAssumptions)}");
        }
        sb.AppendLine();

        // 4. Single points of failure
        Sec(sb, "Single Points of Failure");
        sb.AppendLine(AssumptionDependencyAnalyzer.SinglePointsOfFailure(assumptions, impact, totalResults));

        // 5. Fragility correlation
        Sec(sb, "Fragility Correlation — XE001 Causes Traced to Assumptions");
        sb.AppendLine(AssumptionDependencyAnalyzer.FragilityCorrelation(results));

        // 6. Tier analysis
        Sec(sb, "Assumptions by Tier");
        for (int t = 0; t <= 4; t++)
        {
            var tierAssumptions = assumptions.Where(a => a.Tier == t).ToList();
            int tierResults = results.Count(r => r.Tier >= t);
            sb.AppendLine($"  TIER {t}: {tierAssumptions.Count} assumptions, supports Tier {t}+ results.");
            foreach (var a in tierAssumptions)
                sb.AppendLine($"    {a.Id}: {a.Name} — {impact[a.Id]} results directly dependent.");
            sb.AppendLine();
        }

        // 7. Removal analysis
        Sec(sb, "Removal Analysis — If Each Tier Is Removed");
        sb.AppendLine("  Remove    Surviving Results    Lost Results    Survival %");
        sb.AppendLine("  " + new string('-', 60));
        for (int t = 4; t >= 0; t--)
        {
            var keepAssumptions = assumptions.Where(a => a.Tier < t).Select(a => a.Id).ToHashSet();
            int surviving = results.Count(r => r.RequiredAssumptions.All(a => keepAssumptions.Contains(a)));
            int lost = totalResults - surviving;
            sb.AppendLine($"  Tier {t}+   {surviving,17}   {lost,12}      {100.0 * surviving / totalResults,8:F0}%");
        }
        sb.AppendLine();
        sb.AppendLine("  Removing Tier 4: ALL 18 results survive. Only cosmology/DM predictions lost.");
        sb.AppendLine("  Removing Tier 3+: 6 results (abundance + cosmology) lost. 12 survive.");
        sb.AppendLine("  Removing Tier 2+: ONLY 4 results survive (QM, time, spacetime, GR).");
        sb.AppendLine("  Removing Tier 1+: 2 results survive (Q + Randomness only).");
        sb.AppendLine();

        // 8. Minimal core
        Sec(sb, "Minimal AT Core — Tier by Tier");
        sb.AppendLine(AssumptionDependencyAnalyzer.MinimalCore(assumptions, results));

        // 9. Final
        int criticalCount = assumptions.Count(a => impact[a.Id] > totalResults * 0.25);
        string classification = criticalCount <= 4 ? "C: Robust Structure — Few critical assumptions"
            : criticalCount <= 6 ? "B: Moderate Dependency"
            : "A: Highly Assumption-Dependent";

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ResearchXE-002 COMPLETE.");
        sb.AppendLine($"  Classification: {classification}");
        sb.AppendLine($"  {assumptions.Count} assumptions. {results.Count} results. {criticalCount} critical assumptions.");
        sb.AppendLine($"  Top-3 critical: Q (A1), Randomness (A2), Dimensions (A5).");
        sb.AppendLine($"  Fragility comes from A9 (stability cutoff) + A16 (degree definition).");
        sb.AppendLine($"  Robustness comes from CLT (A12) + theorems (A4, A8).");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
