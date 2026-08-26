using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_045_HistoricalIdentityLifetime : ResearchTestBase
{
    private static readonly int[] IterCounts = { 10000, 25000, 50000 };
    private static readonly string[] Sequences = { "AB", "BA" };
    private const double Beta = 0.5;
    private const int N = 200;
    private const int Seeds = 8;
    private const int BaseSeed = 267914296;

    public AT_045_HistoricalIdentityLifetime(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_045_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-045 Historical Identity Lifetime");
        report.AppendLine("AT-045: How Long Do Path-Dependent Identities Survive?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-044 showed AB ≠ BA. This experiment tests whether the difference");
        report.AppendLine("  persists or decays over long time periods.");
        report.AppendLine();

        int total = IterCounts.Length * Sequences.Length * Seeds;
        AppendSection(report, "2. Setup");
        report.AppendLine($"  Iterations: [{string.Join(",", IterCounts)}], AB vs BA, {Seeds} seeds, Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<IdentityLifetimeAnalyzer.LifetimeResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int ii = idx % IterCounts.Length, rem = idx / IterCounts.Length;
            int si = rem % Sequences.Length, seedI = rem / Sequences.Length;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(IdentityLifetimeAnalyzer.Analyze(Sequences[si], Beta, 5.0, 0.05, N, rng, IterCounts[ii]));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Identity Lifetime");

        report.AppendLine("  Iter    │ Seq │ Final R  │ ΔR(AB−BA)");
        report.AppendLine("  ────────┼─────┼──────────┼───────────");

        foreach (int iters in IterCounts)
        {
            var ab = results.Where(r => r.TotalIterations == iters && r.Sequence == "AB").ToList();
            var ba = results.Where(r => r.TotalIterations == iters && r.Sequence == "BA").ToList();
            double rAB = ab.Average(r => r.FinalR), rBA = ba.Average(r => r.FinalR);

            report.AppendLine($"  {iters,7} │ AB  │ {rAB,8:F4} │");
            report.AppendLine($"  {iters,7} │ BA  │ {rBA,8:F4} │ {rAB - rBA,9:F4}");
        }

        report.AppendLine();

        // Check if gap closes.
        var gap10k = results.Where(r => r.TotalIterations == 10000).ToList();
        var gap50k = results.Where(r => r.TotalIterations == 50000).ToList();
        double delta10k = Math.Abs(gap10k.Where(r => r.Sequence == "AB").Average(r => r.FinalR) - gap10k.Where(r => r.Sequence == "BA").Average(r => r.FinalR));
        double delta50k = Math.Abs(gap50k.Where(r => r.Sequence == "AB").Average(r => r.FinalR) - gap50k.Where(r => r.Sequence == "BA").Average(r => r.FinalR));

        bool persistent = delta50k > delta10k * 0.3;

        AppendSection(report, "4. Conclusion");
        report.AppendLine($"  C1. {(persistent ? "PERSISTENT — identity difference survives at 50K iterations" : "DECAYING — identity difference fades with time")}");
        report.AppendLine($"  C2. ΔR at 10K: {delta10k:F4}, at 50K: {delta50k:F4}");
        report.AppendLine("  C3. Historical identity lifetime constrains how long");
        report.AppendLine("      past experiences remain relevant to condensate behavior.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-045 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
