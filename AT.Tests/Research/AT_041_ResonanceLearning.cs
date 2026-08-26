using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_041_ResonanceLearning : ResearchTestBase
{
    private static readonly int[] PulseCounts = { 1, 5, 10, 25, 50 };
    private static readonly double[] Strengths = { 0.2, 0.5, 1.0 }; // low, medium, high
    private const double Beta = 0.5;
    private const int N = 200;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Seeds = 10;
    private const int BaseSeed = 433494437;

    public AT_041_ResonanceLearning(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_041_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-041 Resonance Learning");
        report.AppendLine("AT-041: Can Condensates Learn from Repeated Experience?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-036-040 established memory, identity, and distributed storage.");
        report.AppendLine("  This experiment tests whether repeated stimulation TRAINS condensates");
        report.AppendLine("  to respond differently — the hallmark of learning.");
        report.AppendLine();

        int total = PulseCounts.Length * Strengths.Length * Seeds;
        AppendSection(report, "2. Training Protocol");
        report.AppendLine($"  Pulses: [{string.Join(",", PulseCounts)}], Strength: [{string.Join(",", Strengths)}]");
        report.AppendLine($"  β={Beta}, {Seeds} seeds, Total: {total}. Baseline→Training→Probe→Recovery");
        report.AppendLine();

        var bag = new ConcurrentBag<LearningAnalyzer.LearningResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int pi = idx % PulseCounts.Length, rem = idx / PulseCounts.Length;
            int si = rem % Strengths.Length, seedI = rem / Strengths.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(LearningAnalyzer.Analyze(PulseCounts[pi], Strengths[si], Beta, K, Lambda, N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Learning Results");

        report.AppendLine("  Pulses │ Strength │ Learned % │ Recovery τ │ Probe Resp │ Final R");
        report.AppendLine("  ───────┼──────────┼───────────┼────────────┼────────────┼────────");

        foreach (int pc in PulseCounts)
        {
            foreach (double st in Strengths)
            {
                var sub = results.Where(r => r.PulseCount == pc && Math.Abs(r.TrainingStrength - st) < 0.001).ToList();
                report.AppendLine($"  {pc,6} │ {st,8:F1} │ {sub.Count(r => r.Learned) * 100.0 / sub.Count,8:F0}% │ {sub.Average(r => r.RecoveryTime),10:F0} │ {sub.Average(r => r.ProbeResponse),10:F4} │ {sub.Average(r => r.FinalR),6:F4}");
            }
        }

        report.AppendLine();

        int learned = results.Count(r => r.Learned);

        AppendSection(report, "4. Conclusion");
        report.AppendLine($"  C1. {(learned > total / 2 ? "LEARNING DETECTED" : "NO LEARNING")} — {learned}/{total} ({learned * 100 / total}%) trained condensates showed adaptation.");
        report.AppendLine("  C2. Repeated stimulation represents the first AT mechanism for");
        report.AppendLine("      information encoding through temporal dynamics.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-041 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
