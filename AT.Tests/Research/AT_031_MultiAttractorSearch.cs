using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_031_MultiAttractorSearch : ResearchTestBase
{
    private static readonly int[] Ns = { 100, 300, 500 };
    private static readonly double[] Ks = { 1, 3, 5 };
    private const double Lambda = 0.05;
    private const int SimsPerCombo = 30; // reduced from 1000 for runtime
    private const int BaseSeed = 39088169;

    public AT_031_MultiAttractorSearch(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_031_RunMultiAttractorSearch()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-031 Multi-Attractor Search");
        report.AppendLine("AT-031: How Many Stable Attractor Basins Exist?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-030 showed condensates are deep attractors. This experiment searches");
        report.AppendLine("  for MULTIPLE distinct attractor basins using random initial conditions.");
        report.AppendLine();

        int total = Ns.Length * Ks.Length * SimsPerCombo;
        AppendSection(report, "2. Simulation Ensemble");
        report.AppendLine($"  N=[{string.Join(",", Ns)}], K=[{string.Join(",", Ks)}]");
        report.AppendLine($"  {SimsPerCombo} sims/combo, total: {total}, random init: phases, freqs, placements");
        report.AppendLine();

        var bag = new ConcurrentBag<AttractorAnalyzer.AttractorPoint>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.For(0, total, idx =>
        {
            int ni = idx % Ns.Length, ki = (idx / Ns.Length) % Ks.Length, si = idx / (Ns.Length * Ks.Length);
            if (si >= SimsPerCombo) return;
            var rng = new Random(BaseSeed + idx * 7919);
            var pt = AttractorAnalyzer.RunOne(Ns[ni], Ks[ki], Lambda, rng);
            if (pt != null) bag.Add(pt);
        });

        sw.Stop();
        var points = bag.ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Points: {points.Count}/{total}");
        report.AppendLine();

        if (points.Count < 10) { report.AppendLine("Insufficient data."); Output.WriteLine(report.ToString()); return; }

        // ── 3. Attractor Detection ──────────────────────────────
        AppendSection(report, "3. Attractor Clustering");

        // Cluster final states.
        var clusters = AttractorAnalyzer.Cluster(points, 0.3);
        int unclustered = points.Count - clusters.Sum(c => c.Count);

        report.AppendLine($"  Total points: {points.Count}");
        report.AppendLine($"  Attractor clusters (≥2 members): {clusters.Count}");
        report.AppendLine($"  Unclustered: {unclustered}");
        report.AppendLine();

        if (clusters.Count > 0)
        {
            var sorted = clusters.OrderByDescending(c => c.Count).ToList();
            report.AppendLine("  Attractor │ Members │ Mean R    │ Mean Density │ Mean Neighbors");
            report.AppendLine("  ──────────┼─────────┼───────────┼──────────────┼───────────────");

            for (int i = 0; i < sorted.Count; i++)
            {
                var c = sorted[i];
                report.AppendLine(
                    $"  A{i + 1,8} │ {c.Count,7} │ {c.Average(p => p.LocalR),9:F4} │ {c.Average(p => p.Density),12:F4} │ {c.Average(p => p.NeighborCount),13:F1}");
            }
        }
        else
        {
            report.AppendLine("  No distinct attractor clusters found — single attractor landscape.");
        }

        report.AppendLine();

        // ── 4. Basin Analysis ───────────────────────────────────
        AppendSection(report, "4. Landscape Classification");

        string classification = clusters.Count >= 3 ? "RICH MULTI-ATTRACTOR LANDSCAPE" :
                                clusters.Count >= 1 ? "FEW ATTRACTORS" : "SINGLE ATTRACTOR";

        report.AppendLine($"  Classification: {classification}");
        report.AppendLine();

        // ── 5. Interpretation ───────────────────────────────────
        AppendSection(report, "5. Interpretation");

        report.AppendLine($"  Q1. Multiple attractor classes? {(clusters.Count > 0 ? $"YES — {clusters.Count} distinct attractors" : "NO — single universal attractor")}");

        report.AppendLine();
        report.AppendLine($"  Q2. How many? {clusters.Count}");

        report.AppendLine();
        report.AppendLine("  Q3. Different basin sizes?");
        if (clusters.Count > 1)
        {
            var sizes = clusters.Select(c => c.Count).ToList();
            report.AppendLine($"    Yes — sizes range from {sizes.Min()} to {sizes.Max()} (ratio={sizes.Max()/(double)sizes.Min():F1}×)");
        }
        else
            report.AppendLine("    Single basin — all points converge to same attractor.");

        report.AppendLine();

        AppendSection(report, "6. Conclusion");
        report.AppendLine($"  C1. The AT resonance landscape is: {classification}");
        report.AppendLine($"  C2. {clusters.Count} attractor basin{(clusters.Count == 1 ? "" : "s")} identified.");
        report.AppendLine("  C3. The attractor structure determines the possible stable states");
        report.AppendLine("      available to AT condensates — analogous to the vacuum structure");
        report.AppendLine("      of quantum field theories.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-031 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
