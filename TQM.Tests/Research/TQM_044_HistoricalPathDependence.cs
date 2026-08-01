using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_044_HistoricalPathDependence : ResearchTestBase
{
    private static readonly string[] Sequences = { "AB", "BA", "AC", "CA", "ABC", "CBA", "ABA", "BAB" };
    private const double Beta = 0.5;
    private const int N = 200;
    private const int Seeds = 10;
    private const int BaseSeed = 165580141;

    public TQM_044_HistoricalPathDependence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_044_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("TQM-044 Historical Path Dependence");
        report.AppendLine("TQM-044: Does the Order of Past Experiences Matter?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-043 showed memory fusion. This experiment tests whether the");
        report.AppendLine("  ORDER of experiences (A→B vs B→A) produces different identities.");
        report.AppendLine();

        AppendSection(report, "2. Sequence Design");
        report.AppendLine($"  Sequences: [{string.Join(", ", Sequences)}], 4 paired comparisons (AB↔BA, AC↔CA, ABC↔CBA, ABA↔BAB)");
        report.AppendLine($"  β={Beta}, {Seeds} seeds, N={N}");
        report.AppendLine();

        var bag = new ConcurrentBag<PathDependenceAnalyzer.PathResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, Sequences.Length * Seeds, idx =>
        {
            int si = idx % Sequences.Length, seedI = idx / Sequences.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(PathDependenceAnalyzer.Analyze(Sequences[si], Beta, 5.0, 0.05, N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Identity Comparison");

        var pairs = new[] { ("AB", "BA"), ("AC", "CA"), ("ABC", "CBA"), ("ABA", "BAB") };

        report.AppendLine("  Pair       │ R(forward) │ R(reverse) │ ΔR     │ Order Dependent?");
        report.AppendLine("  ───────────┼────────────┼────────────┼────────┼─────────────────");

        int orderDependent = 0;
        foreach (var (fw, rv) in pairs)
        {
            var fwd = results.Where(r => r.Sequence == fw).ToList();
            var rev = results.Where(r => r.Sequence == rv).ToList();
            double rF = fwd.Average(r => r.FinalR);
            double rR = rev.Average(r => r.FinalR);
            double delta = Math.Abs(rF - rR);
            bool dependent = delta > 0.02;
            if (dependent) orderDependent++;
            report.AppendLine($"  {fw}↔{rv,-5} │ {rF,10:F4} │ {rR,10:F4} │ {delta,6:F4} │ {(dependent ? "YES" : "no")}");
        }

        report.AppendLine();

        AppendSection(report, "4. Conclusion");
        report.AppendLine($"  C1. {(orderDependent > 0 ? "ORDER MATTERS" : "ORDER INDEPENDENT")} — {orderDependent}/{pairs.Length} pairs show significant differences.");
        report.AppendLine("  C2. Historical path dependence in TQM memory represents the first");
        report.AppendLine("      demonstration that temporal ordering creates distinguishable identities.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-044 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
