using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_033_HierarchicalCondensation : ResearchTestBase
{
    private static readonly int[] CondensateCounts = { 2, 3, 4, 8 };
    private static readonly string[] Layouts = { "Linear", "Ring", "Square" };
    private const double Separation = 0.15; // beyond merger threshold
    private const int ClusterOsc = 30;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int BaseSeed = 102334155;

    public TQM_033_HierarchicalCondensation(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void TQM_033_RunHierarchicalTest()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-033 Hierarchical Condensation");
        report.AppendLine("TQM-033: Can Multiple Condensates Form Stable Assemblies?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-031/032 showed single-attractor landscapes. This experiment tests");
        report.AppendLine("  whether higher-order structures emerge from multi-condensate assemblies.");
        report.AppendLine();

        int total = CondensateCounts.Length * Layouts.Length;
        AppendSection(report, "2. Assembly Construction");
        report.AppendLine($"  Condensates: [{string.Join(",", CondensateCounts)}], Layouts: [{string.Join(", ", Layouts)}]");
        report.AppendLine($"  Separation={Separation}, {ClusterOsc} oscillators/condensate, Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<CondensateAssemblyAnalyzer.AssemblyResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int ci = idx % CondensateCounts.Length, li = (idx / CondensateCounts.Length) % Layouts.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(CondensateAssemblyAnalyzer.Analyze(
                CondensateCounts[ci], Layouts[li], Separation, ClusterOsc, K, Lambda, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Stability Analysis ───────────────────────────────
        AppendSection(report, "3. Assembly Stability");

        report.AppendLine("  Layout │ Nc │ Final R │ Final Domains │ Mergers │ Classification");
        report.AppendLine("  ───────┼────┼─────────┼───────────────┼─────────┼───────────────");

        foreach (var r in results.OrderBy(r => r.Layout).ThenBy(r => r.CondensateCount))
            report.AppendLine(
                $"  {r.Layout,-6} │ {r.CondensateCount,2} │ {r.FinalGlobalR,7:F4} │ {r.FinalDomains,13} │ {r.Mergers,7} │ {r.Classification}");

        report.AppendLine();

        // ── 4. Interpretation ───────────────────────────────────
        AppendSection(report, "4. Assembly Classification");

        int stable = results.Count(r => r.Classification == "Stable Assembly");
        int partial = results.Count(r => r.Classification == "Partial Assembly");
        int merged = results.Count(r => r.Classification == "Merged");

        report.AppendLine($"  Stable Assemblies : {stable}/{total}");
        report.AppendLine($"  Partial Assemblies: {partial}/{total}");
        report.AppendLine($"  Merged           : {merged}/{total}");
        report.AppendLine();

        report.AppendLine("  Q1. Stable assemblies form?");
        report.AppendLine($"    {(stable > 0 ? $"YES — {stable} configurations" : "NO — all assemblies either merge or disorder")}");

        report.AppendLine();
        report.AppendLine("  Q2. Stability increase with count?");
        report.AppendLine("    Analysis from data above.");

        report.AppendLine();
        report.AppendLine("  Q3. Preferred geometries?");
        var bestGeom = results.GroupBy(r => r.Layout)
            .OrderByDescending(g => g.Count(r => r.Classification == "Stable Assembly"))
            .First();
        report.AppendLine($"    {bestGeom.Key} has most stable assemblies.");

        report.AppendLine();
        report.AppendLine("  Q4. Higher-order attractor?");
        report.AppendLine($"    {(stable > 0 ? "Potentially — stable assemblies represent multi-condensate attractors" : "Not observed")}");

        report.AppendLine();

        AppendSection(report, "5. Conclusion");
        string hierarchy = stable > total / 2 ? "MULTI-CONDENSATE STABLE ASSEMBLIES" :
                           stable > 0 ? "SOME STABLE ASSEMBLIES" : "NO ASSEMBLIES — condensates merge or disorder";
        report.AppendLine($"  C1. {hierarchy}");
        report.AppendLine();
        report.AppendLine("  C2. The formation of stable multi-condensate structures represents");
        report.AppendLine("      a second hierarchy level — organization of fundamental condensates");
        report.AppendLine("      into composite structures.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-033 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
