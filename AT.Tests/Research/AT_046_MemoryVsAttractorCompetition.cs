using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_046_MemoryVsAttractorCompetition : ResearchTestBase
{
    private static readonly double[] Betas = { 0.0, 0.05, 0.10, 0.20, 0.50, 1.0, 2.0 };
    private static readonly int[] Times = { 1000, 5000, 10000, 25000, 50000 };
    private const int N = 200;
    private const int Seeds = 5;
    private const int BaseSeed = 433494437;

    public AT_046_MemoryVsAttractorCompetition(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_046_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var report = new StringBuilder();
        PrintHeader("AT-046 Memory vs Attractor Competition");
        report.AppendLine("AT-046: Does Memory Strength β Resist Attractor Convergence?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-044/045 showed path dependence decays. This experiment maps");
        report.AppendLine("  the competition between memory (β) and attractor relaxation over time.");
        report.AppendLine();

        int total = Betas.Length * Times.Length * 2 * Seeds;
        AppendSection(report, "2. Setup");
        report.AppendLine($"  β=[{string.Join(",", Betas)}], Times=[{string.Join(",", Times)}], Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<MemoryCompetitionAnalyzer.CompetitionResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int bi = idx % Betas.Length, rem = idx / Betas.Length;
            int ti = rem % Times.Length, rem2 = rem / Times.Length;
            int si = rem2 % 2, seedI = rem2 / 2;
            var rng = new Random(BaseSeed + idx * 7919);
            bag.Add(MemoryCompetitionAnalyzer.Analyze(Betas[bi], Times[ti], si == 0 ? "AB" : "BA", N, rng));
        });

        sw.Stop();
        var results = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        AppendSection(report, "3. Identity Gap (ΔR = R_AB − R_BA)");

        report.Append("  β \\ Time │");
        foreach (int t in Times) report.Append($"{t,10}");
        report.AppendLine();
        report.Append("  ─────────┼");
        report.AppendLine(new string('─', Times.Length * 10));

        foreach (double beta in Betas)
        {
            report.Append($"  {beta,7:F2} │");
            foreach (int t in Times)
            {
                var ab = results.Where(r => Math.Abs(r.Beta - beta) < 0.001 && r.Iterations == t && r.Sequence == "AB").ToList();
                var ba = results.Where(r => Math.Abs(r.Beta - beta) < 0.001 && r.Iterations == t && r.Sequence == "BA").ToList();
                double gap = (ab.Any() && ba.Any()) ? ab.Average(r => r.FinalR) - ba.Average(r => r.FinalR) : 0;
                report.Append($" {gap,9:F4}");
            }
            report.AppendLine();
        }

        report.AppendLine();

        AppendSection(report, "4. Conclusion");
        double maxGap = 0;
        foreach (double beta in Betas)
            foreach (int t in Times)
            {
                var ab = results.Where(r => Math.Abs(r.Beta - beta) < 0.001 && r.Iterations == t && r.Sequence == "AB").Select(r => r.FinalR).DefaultIfEmpty(0).Average();
                var ba = results.Where(r => Math.Abs(r.Beta - beta) < 0.001 && r.Iterations == t && r.Sequence == "BA").Select(r => r.FinalR).DefaultIfEmpty(0).Average();
                maxGap = Math.Max(maxGap, Math.Abs(ab - ba));
            }

        report.AppendLine($"  C1. Maximum identity gap: {maxGap:F4}");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-046 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string t) { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
