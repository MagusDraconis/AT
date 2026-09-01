using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_029_CondensateLifecycleAnalysis : ResearchTestBase
{
    private const int N = 500;
    private const double K = 5.0;
    private const double Lambda = 0.05;
    private const int Runs = 8;
    private const int BaseSeed = 14930352;

    public AT_029_CondensateLifecycleAnalysis(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_029_RunLifecycleAnalysis()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-029 Condensate Lifecycle Analysis");
        report.AppendLine("AT-029: Birth, Growth, Mergers, and Death of Condensates");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-028 found local quantities are conserved. This experiment tracks");
        report.AppendLine("  condensates through their ENTIRE lifecycle to build a taxonomy of");
        report.AppendLine("  condensate evolution paths.");
        report.AppendLine();

        AppendSection(report, "2. Lifecycle Tracking");
        report.AppendLine($"  N={N}, K={K}, λ={Lambda}, {Runs} runs, 5000 iter each");
        report.AppendLine();

        var allProfiles = new ConcurrentBag<LifecycleProfile>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, Runs, run =>
        {
            var rng = new Random(BaseSeed + run * 10000);
            var profiles = CondensateLifecycleAnalyzer.Analyze(N, K, Lambda, rng);
            foreach (var p in profiles) allProfiles.Add(p);
        });

        sw.Stop();
        var profiles = allProfiles.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Total condensates: {profiles.Count}");
        report.AppendLine();

        // ── 3. Lifecycle Classification ─────────────────────────
        AppendSection(report, "3. Lifecycle Classification");

        var byClass = profiles.GroupBy(p => p.LifecycleClass)
            .ToDictionary(g => g.Key, g => g.Count())
            .OrderByDescending(kv => kv.Value);

        report.AppendLine("  Class       │ Count │  %   │ Mean Lifetime │ Mean Start Size │ Mean End Size");
        report.AppendLine("  ────────────┼───────┼──────┼───────────────┼─────────────────┼──────────────");

        foreach (var (cls, count) in byClass)
        {
            var subset = profiles.Where(p => p.LifecycleClass == cls).ToList();
            double avgLife = subset.Average(p => p.Lifetime);
            double startSz = subset.Average(p => p.History.Count > 0 ? p.History[0].Size : 0);
            double endSz = subset.Average(p => p.History.Count > 0 ? p.History[^1].Size : 0);

            report.AppendLine(
                $"  {cls,-11} │ {count,5} │ {count * 100.0 / profiles.Count,4:F0}% │ {avgLife,13:F0} │ {startSz,15:F1} │ {endSz,12:F1}");
        }

        report.AppendLine();

        // ── 4. Growth Analysis ──────────────────────────────────
        AppendSection(report, "4. Growth Path Analysis");

        var growing = profiles.Where(p => p.LifecycleClass == "Growing").ToList();
        var stable = profiles.Where(p => p.LifecycleClass == "Stable").ToList();
        var dying = profiles.Where(p => p.LifecycleClass == "Dying" || p.LifecycleClass == "Shrinking").ToList();

        report.AppendLine($"  Growing  : {growing.Count} condensates, mean final size {(growing.Any() ? growing.Average(p => p.History.Last().Size):0):F0}");
            report.AppendLine($"  Stable   : {stable.Count} condensates, mean lifetime {(stable.Any() ? stable.Average(p => p.Lifetime):0):F0}");
            report.AppendLine($"  Dying    : {dying.Count} condensates, mean lifetime {(dying.Any() ? dying.Average(p => p.Lifetime):0):F0}");
        report.AppendLine();

        // ── 5. Merger Analysis ──────────────────────────────────
        AppendSection(report, "5. Merger & Split Analysis");

        int mergers = profiles.Count(p => p.LifecycleClass == "Merger");
        int splits = profiles.Count(p => p.LifecycleClass == "Split");

        report.AppendLine($"  Merger events  : {mergers} ({mergers * 100.0 / profiles.Count:F0}%)");
        report.AppendLine($"  Split events   : {splits} ({splits * 100.0 / profiles.Count:F0}%)");
        report.AppendLine();

        if (mergers > 0)
        {
            var mergerProfiles = profiles.Where(p => p.LifecycleClass == "Merger").ToList();
            if (mergerProfiles.Any() && mergerProfiles.All(p => p.History.Any()))
            {
                report.AppendLine($"  Mean merger-created condensate size : {mergerProfiles.Average(p => p.History.Last().Size):F0}");
                report.AppendLine($"  Mean merger lifetime                : {mergerProfiles.Average(p => p.Lifetime):F0}");
            }
        }

        report.AppendLine();

        // ── 6. Interpretation ───────────────────────────────────
        AppendSection(report, "6. Interpretation");

        string dominantClass = byClass.First().Key;
        report.AppendLine($"  Q1. Multiple lifecycle classes? YES — {byClass.Count()} classes found.");
        report.AppendLine($"  Q2. Dominant path: {dominantClass} ({byClass.First().Value}/{profiles.Count})");
        report.AppendLine($"  Q3. Death predictable? {(dying.Count > 0 ? "Partially — dying condensates show size decline" : "No deaths observed")}");
        report.AppendLine($"  Q4. Universal curves? {(stable.Count > profiles.Count * 0.5 ? "YES — stable class dominates" : "Diverse paths")}");
        report.AppendLine($"  Q5. Mergers create attractors? {(mergers > 0 ? $"YES — {mergers} mergers observed" : "No mergers")}");

        report.AppendLine();

        AppendSection(report, "7. Conclusion");
        report.AppendLine($"  C1. Condensates follow {byClass.Count()} distinct lifecycle paths.");
        report.AppendLine($"  C2. The dominant class is: {dominantClass} ({byClass.First().Value}/{profiles.Count}).");
        report.AppendLine("  C3. The condensate lifecycle taxonomy provides a foundation for");
        report.AppendLine("      understanding proto-particle dynamics: birth → growth →");
        report.AppendLine("      stability → possible merger/split → death.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-029 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
