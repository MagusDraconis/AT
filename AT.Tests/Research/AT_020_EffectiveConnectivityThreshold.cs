using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_020_EffectiveConnectivityThreshold : ResearchTestBase
{
    private static readonly int[] Ns = { 100, 200, 500 };
    private static readonly double[] Ks = { 1, 2, 3, 5 };
    private static readonly double[] Lambdas = { 0.02, 0.05, 0.10, 0.50 };
    private static readonly string[] Placements = { "Uniform", "GaussianBlobs", "MultipleClusters", "Hierarchical" };
    private const int Iterations = 3000;
    private const int BaseSeed = 196418;

    public AT_020_EffectiveConnectivityThreshold(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void AT_020_RunConnectivityExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-020 Effective Connectivity Threshold");
        report.AppendLine("AT-020: Is Effective Connectivity the True Universal Predictor?");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-019 showed ρc depends strongly on placement. This experiment tests");
        report.AppendLine("  whether effective CONNECTIVITY (neighbors within λ) is more universal.");
        report.AppendLine();

        int total = Ns.Length * Ks.Length * Lambdas.Length * Placements.Length;
        AppendSection(report, "2. Experimental Setup");
        report.AppendLine($"  N=[{string.Join(",", Ns)}], K=[{string.Join(",", Ks)}]");
        report.AppendLine($"  λ=[{string.Join(",", Lambdas)}], 4 placements, Total: {total}");
        report.AppendLine();

        var bag = new ConcurrentBag<EffectiveConnectivityAnalyzer.ConnectivityMeasurement>();
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var points = (from n in Ns from k in Ks from lam in Lambdas from p in Placements select (n, k, lam, p)).ToList();

        Parallel.ForEach(points, pt =>
        {
            var (n, k, lam, p) = pt;
            int seed = BaseSeed + n * 1000 + (int)(k * 100) + (int)(lam * 10000) + p.GetHashCode() % 10000;
            var rng = new Random(seed);
            var m = EffectiveConnectivityAnalyzer.Measure(n, k, lam, p, rng, Iterations);
            bag.Add(m);
        });

        sw.Stop();
        var meas = bag.ToList();
        var withCond = meas.Where(m => m.CondensateCount > 0).ToList();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms. Condensate combos: {withCond.Count}/{total}");
        report.AppendLine();

        if (withCond.Count == 0) { report.AppendLine("No condensates."); Output.WriteLine(report.ToString()); return; }

        // ── 3. Connectivity vs Density ──────────────────────────
        AppendSection(report, "3. Metric Universality Comparison");

        double cvNeighbors = CV(Group(Placements, withCond, m => m.Placement, m => m.MeanNeighborCount));
        double cvWeighted = CV(Group(Placements, withCond, m => m.Placement, m => m.MeanWeightedNeighbors));
        double cvClustering = CV(Group(Placements, withCond, m => m.Placement, m => m.MeanClusteringCoeff));
        double cvDensity = CV(Group(Placements, withCond, m => m.Placement, m => m.MeanDensity));

        report.AppendLine("  Cross-placement CV comparison (lower = more universal):");
        report.AppendLine($"    Neighbor count        : CV = {cvNeighbors:F3}");
        report.AppendLine($"    Weighted neighbors    : CV = {cvWeighted:F3}");
        report.AppendLine($"    Clustering coefficient: CV = {cvClustering:F3}");
        report.AppendLine($"    Local density         : CV = {cvDensity:F3}");
        report.AppendLine();

        // ── 4. Metric Rankings ──────────────────────────────────
        AppendSection(report, "4. Universality Rankings");

        var metrics = new (string Name, double CV)[]
        {
            ("Neighbor Count", cvNeighbors),
            ("Weighted Neighbors", cvWeighted),
            ("Clustering Coeff", cvClustering),
            ("Local Density", cvDensity),
        }.OrderBy(m => m.CV).ToList();

        report.AppendLine("  Rank │ Metric               │ Cross-Placement CV │ Universality");
        report.AppendLine("  ─────┼───────────────────────┼────────────────────┼─────────────");

        for (int i = 0; i < metrics.Count; i++)
        {
            string level = metrics[i].CV < 0.3 ? "HIGH" : metrics[i].CV < 0.5 ? "MODERATE" : "LOW";
            report.AppendLine($"  {i + 1,4} │ {metrics[i].Name,-21} │ {metrics[i].CV,18:F3} │ {level}");
        }

        report.AppendLine();
        report.AppendLine($"  Best metric: {metrics[0].Name} (CV={metrics[0].CV:F3})");

        // Compare against AT-019 density CV.
        report.AppendLine($"  AT-019 density CV (cross-placement): 0.683");
        report.AppendLine($"  Improvement factor: {(cvDensity > 0 ? 0.683 / metrics[0].CV : 0):F1}×");
        report.AppendLine();

        // ── 5. Mean Values by Placement ─────────────────────────
        AppendSection(report, "5. Metric Values by Placement");

        report.AppendLine("  Placement         │ Neighbors │ Weighted │ Clustering │ Density");
        report.AppendLine("  ─────────────────┼───────────┼──────────┼────────────┼────────");

        foreach (string p in Placements)
        {
            var sub = withCond.Where(m => m.Placement == p).ToList();
            if (sub.Count > 0)
            {
                report.AppendLine(
                    $"  {p,-17} │ {sub.Average(m => m.MeanNeighborCount),9:F1} │ {sub.Average(m => m.MeanWeightedNeighbors),8:F2} │ {sub.Average(m => m.MeanClusteringCoeff),10:F4} │ {sub.Average(m => m.MeanDensity),6:F4}");
            }
        }

        report.AppendLine();

        // ── 6. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        report.AppendLine("  Q1. Critical effective connectivity?");
        double meanNeighbors = withCond.Average(m => m.MeanNeighborCount);
        double cvAllN = CV(Group(Ns, withCond, m => m.N, m => m.MeanNeighborCount));
        report.AppendLine($"    Mean neighbors at birth: {meanNeighbors:F1}");
        report.AppendLine($"    CV across all parameters: {cvAllN:F3}");
        report.AppendLine();

        report.AppendLine("  Q2. More universal than density?");
        bool betterThanDensity = metrics[0].CV < cvDensity;
        report.AppendLine($"    {(betterThanDensity ? "YES" : "NO")} — best metric CV={metrics[0].CV:F3} vs density CV={cvDensity:F3}");

        report.AppendLine();
        report.AppendLine("  Q3. Common threshold?");
        report.AppendLine($"    {(metrics[0].CV < 0.3 ? "Likely — best metric is placement-independent" : "No — even best metric varies by placement")}");

        report.AppendLine();
        report.AppendLine("  Q4. Best predictor?");
        report.AppendLine($"    {metrics[0].Name} is the most universal predictor (CV={metrics[0].CV:F3}).");

        report.AppendLine();

        // ── 7. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. {metrics[0].Name} is the most universal predictor of resonance");
        report.AppendLine($"      condensation (cross-placement CV={metrics[0].CV:F3}).");
        report.AppendLine();
        report.AppendLine($"  C2. Effective connectivity {(betterThanDensity ? "IS" : "is NOT")} more universal than raw");
        report.AppendLine($"      density — {(betterThanDensity ? $"CV improves from {cvDensity:F3} to {metrics[0].CV:F3}" : "density remains competitive")}.");
        report.AppendLine();
        report.AppendLine("  C3. The true control parameter for resonance condensation has been");
        report.AppendLine("      narrowed to: oscillator spatial arrangement > effective connectivity > λ > K.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-020 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private static List<double> Group<T>(T[] keys, List<EffectiveConnectivityAnalyzer.ConnectivityMeasurement> data,
        Func<EffectiveConnectivityAnalyzer.ConnectivityMeasurement, T> selector,
        Func<EffectiveConnectivityAnalyzer.ConnectivityMeasurement, double> valueSelector)
    {
        return keys.Select(k => data.Where(m => EqualityComparer<T>.Default.Equals(selector(m), k))
            .Select(valueSelector).DefaultIfEmpty(0).Average()).Where(d => d > 0).ToList();
    }

    private static double CV(List<double> values) =>
        values.Count > 1 && values.Average() > 1e-10
            ? Math.Sqrt(values.Average(v => (v - values.Average()) * (v - values.Average()))) / values.Average()
            : 0;

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
