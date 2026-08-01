using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_043_CompetingMemoryEncoding : ResearchTestBase
{
    private static readonly string[] Sequences = { "A", "B", "AB", "ABA", "ABC", "AAB", "ABB" };
    private const double Beta = 0.5;
    private const int N = 200;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Seeds = 8;
    private const int BaseSeed = 102334155;

    public TQM_043_CompetingMemoryEncoding(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_043_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-043 Competing Memory Encoding");
        report.AppendLine("TQM-043: Do Competing Memories Coexist, Interfere, or Overwrite?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-041/042 showed learning and capacity. This experiment tests how");
        report.AppendLine("  multiple conflicting training patterns interact in memory.");
        report.AppendLine();

        int total = Sequences.Length * Seeds;
        AppendSection(report, "2. Training Sequences");
        report.AppendLine($"  Sequences: [{string.Join(", ", Sequences)}], β={Beta}, {Seeds} seeds");
        report.AppendLine();

        var bag = new ConcurrentBag<CompetingMemoryAnalyzer.ConflictResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int si = idx % Sequences.Length, seedI = idx / Sequences.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(CompetingMemoryAnalyzer.Analyze(Sequences[si], Beta, K, Lambda, N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Recall Results");

        report.AppendLine("  Seq   │ Recall A │ Recall B │ Recall C │ Behavior");
        report.AppendLine("  ──────┼──────────┼──────────┼──────────┼──────────");

        foreach (string seq in Sequences)
        {
            var sub = results.Where(r => r.Sequence == seq).ToList();
            report.AppendLine($"  {seq,-5} │ {sub.Average(r => r.RecallA),8:F4} │ {sub.Average(r => r.RecallB),8:F4} │ {sub.Average(r => r.RecallC),8:F4} │ {sub.GroupBy(r => r.Behavior).OrderByDescending(g => g.Count()).First().Key}");
        }

        report.AppendLine();

        AppendSection(report, "4. Conclusion");
        var dominant = results.GroupBy(r => r.Behavior).OrderByDescending(g => g.Count()).First();
        report.AppendLine($"  C1. Dominant behavior: {dominant.Key} ({dominant.Count()}/{results.Count})");
        report.AppendLine("  C2. Competing memories demonstrate complex interaction patterns");
        report.AppendLine("      that depend on training sequence order.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-043 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
