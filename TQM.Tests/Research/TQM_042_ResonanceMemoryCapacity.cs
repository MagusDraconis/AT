using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_042_ResonanceMemoryCapacity : ResearchTestBase
{
    private static readonly int[] PatternCounts = { 1, 2, 5, 10, 20, 50 };
    private const double Beta = 0.5;
    private const int N = 200;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Seeds = 10;
    private const int BaseSeed = 701408733;

    public TQM_042_ResonanceMemoryCapacity(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_042_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-042 Resonance Memory Capacity");
        report.AppendLine("TQM-042: How Much Can a Condensate Remember?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-041 showed condensates can learn. This experiment tests memory");
        report.AppendLine("  CAPACITY — how many distinct patterns can be stored before saturation.");
        report.AppendLine();

        int total = PatternCounts.Length * Seeds;
        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  Patterns: [{string.Join(",", PatternCounts)}], β={Beta}, {Seeds} seeds, Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<MemoryCapacityAnalyzer.CapacityResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int pi = idx % PatternCounts.Length, si = idx / PatternCounts.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(MemoryCapacityAnalyzer.Analyze(PatternCounts[pi], Beta, K, Lambda, N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Capacity Results");

        report.AppendLine("  Patterns │ First Recall R │ Last Recall R │ Drift  │ Saturated");
        report.AppendLine("  ────────┼────────────────┼───────────────┼────────┼──────────");

        foreach (int pc in PatternCounts)
        {
            var sub = results.Where(r => r.PatternCount == pc).ToList();
            report.AppendLine($"  {pc,7} │ {sub.Average(r => r.FirstRecallR),14:F4} │ {sub.Average(r => r.LastRecallR),13:F4} │ {sub.Average(r => r.Drift),6:F4} │ {sub.Count(r => r.Saturated),8}/{sub.Count}");
        }

        report.AppendLine();

        int sat = results.Count(r => r.Saturated);

        AppendSection(report, "4. Conclusion");
        report.AppendLine($"  C1. {(sat > total / 3 ? "FINITE CAPACITY — saturation detected" : "HIGH CAPACITY — minimal saturation")} ({sat}/{total} saturated)");
        report.AppendLine("  C2. Memory capacity constrains the number of distinguishable");
        report.AppendLine("      identities a condensate can maintain.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-042 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
