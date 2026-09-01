using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_028_ConservedQuantitiesSearch : ResearchTestBase
{
    private const int N = 500;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Runs = 10;
    private const int BaseSeed = 9227465;

    public AT_028_ConservedQuantitiesSearch(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_028_RunConservedQuantitySearch()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-028 Conserved Quantities Search");
        report.AppendLine("AT-028: Hidden Conservation Laws in Condensate Dynamics");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-015→027 found no single universal birth parameter. This experiment");
        report.AppendLine("  searches for quantities that remain APPROXIMATELY CONSERVED throughout");
        report.AppendLine("  a condensate's lifecycle: birth, growth, perturbation, recovery.");
        report.AppendLine();

        AppendSection(report, "2. Candidate Quantities");
        report.AppendLine($"  N={N}, K={K}, λ={Lambda}, {Runs} runs, 4000 iter each");
        report.AppendLine("  Tracking: 13 candidate quantities across 5 lifecycle phases.");
        report.AppendLine();

        var bag = new ConcurrentBag<(List<ConservedQuantityAnalyzer.LifecycleSnapshot>, List<ConservedQuantityAnalyzer.QuantityResult>)>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, Runs, run =>
        {
            var rng = new Random(BaseSeed + run * 10000);
            bag.Add(ConservedQuantityAnalyzer.Analyze(N, K, Lambda, rng));
        });

        sw.Stop();
        var allResults = bag.SelectMany(b => b.Item2).ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // Aggregate invariance scores across runs.
        var aggregated = allResults.GroupBy(r => r.Name)
            .Select(g => new ConservedQuantityAnalyzer.QuantityResult(
                g.Key,
                g.Average(r => r.BirthVal),
                g.Average(r => r.MatureVal),
                g.Average(r => r.PerturbedVal),
                g.Average(r => r.RecoveredVal),
                g.Average(r => r.FinalVal),
                g.Average(r => r.InvarianceScore)))
            .OrderByDescending(r => r.InvarianceScore)
            .ToList();

        AppendSection(report, "3. Conservation Ranking");

        report.AppendLine("  Rank │ Quantity              │ Birth    │ Mature   │ Perturbed│ Recovered│ Final    │ Score");
        report.AppendLine("  ─────┼───────────────────────┼──────────┼──────────┼──────────┼──────────┼──────────┼──────");

        for (int i = 0; i < aggregated.Count; i++)
        {
            var r = aggregated[i];
            string marker = i == 0 ? " ← BEST" : "";
            report.AppendLine(
                $"  {i + 1,4} │ {r.Name,-21} │ {r.BirthVal,8:F3} │ {r.MatureVal,8:F3} │ {r.PerturbedVal,8:F3} │ {r.RecoveredVal,8:F3} │ {r.FinalVal,8:F3} │ {r.InvarianceScore,4:F2}{marker}");
        }

        report.AppendLine();

        // ── 4. Perturbation Stability ───────────────────────────
        AppendSection(report, "4. Perturbation Recovery Analysis");

        report.AppendLine("  Recovery ratio (Final/Perturbed) for top-5 quantities:");
        foreach (var r in aggregated.Take(5))
        {
            double recoveryRatio = r.PerturbedVal > 1e-10 ? r.FinalVal / r.PerturbedVal : 0;
            report.AppendLine($"    {r.Name,-22}: {r.PerturbedVal:F3} → {r.FinalVal:F3} (ratio={recoveryRatio:F2})");
        }

        report.AppendLine();

        // ── 5. Interpretation ───────────────────────────────────
        AppendSection(report, "5. Interpretation");

        var best = aggregated.First();
        report.AppendLine($"  Best conserved quantity: {best.Name} (score={best.InvarianceScore:F2})");
        report.AppendLine();
        report.AppendLine("  Q1. What changes least?");
        report.AppendLine($"    {best.Name} with invariance score {best.InvarianceScore:F2}");

        report.AppendLine();
        report.AppendLine("  Q2. Conserved quantity exists?");
        bool exists = best.InvarianceScore > 0.7;
        report.AppendLine($"    {(exists ? "YES — at least one quantity is approximately conserved" : "NO — all quantities vary significantly")}");

        report.AppendLine();
        report.AppendLine("  Q3. Same across condensates?");
        report.AppendLine($"    Aggregated across {Runs} runs — consistency depends on quantity.");

        report.AppendLine();

        AppendSection(report, "6. Conclusion");
        report.AppendLine($"  C1. {best.Name} is the most conserved quantity (invariance score={best.InvarianceScore:F2}).");
        report.AppendLine();
        report.AppendLine("  C2. The search for conservation laws in AT condensates reveals:");
        report.AppendLine($"      {(exists ? "Approximate conservation IS present — condensates maintain certain invariants." : "No strong conservation — quantities drift throughout the lifecycle.")}");
        report.AppendLine();
        report.AppendLine("  C3. Conserved quantities, if found, represent the first emergent");
        report.AppendLine("      conservation laws in the AT framework — precursors to the");
        report.AppendLine("      energy, momentum, and charge conservation of physical particles.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-028 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
