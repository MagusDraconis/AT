using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

/// <summary>
/// AT-010: Local Resonance Condensation
///
/// Tests whether stable localized condensates emerge from local density enhancement,
/// rather than global synchronization. Uses 2D spatial embedding with different
/// oscillator placement models and a local synchronization grid.
/// </summary>
public class AT_010_LocalResonanceCondensation : ResearchTestBase
{
    private static readonly int[] Ns = { 200, 500 };
    private static readonly double[] Lambdas = { 0.05, 0.10, 0.20, 0.50 };
    private static readonly double[] Ks = { 2.0, 5.0 };
    private const int Iterations = 5000;
    private const int GridSize = 20;
    private const int CheckpointInterval = 500;
    private const int BaseSeed = 1597;

    private enum PlacementModel { Uniform, GaussianBlobs, MultipleClusters, Hierarchical }

    public AT_010_LocalResonanceCondensation(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void AT_010_RunLocalCondensationExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
            ExecuteExperiment();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-010 Local Resonance Condensation");
        report.AppendLine("AT-010: Emergence of Localized Resonance Condensates");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-009 showed that distance-dependent coupling weakens synchronization.");
        report.AppendLine("  This experiment tests whether localized resonance CONDENSATION — regions");
        report.AppendLine("  of high local synchronization embedded in a globally incoherent background —");
        report.AppendLine("  can emerge under different spatial oscillator distributions.");
        report.AppendLine();
        report.AppendLine("  Hypothesis: Stable matter-like structures emerge through LOCAL resonance");
        report.AppendLine("  condensation, not global synchronization.");
        report.AppendLine();

        AppendSection(report, "2. Experimental Setup");
        int total = Ns.Length * Lambdas.Length * Ks.Length * 4;
        report.AppendLine($"  Parameters: N=[{string.Join(",", Ns)}], λ=[{string.Join(",", Lambdas)}], K=[{string.Join(",", Ks)}]");
        report.AppendLine($"  Placement models: Uniform, Gaussian Blobs, Multiple Clusters, Hierarchical");
        report.AppendLine($"  Total combos: {total}, Grid: {GridSize}×{GridSize}, Iterations: {Iterations}");
        report.AppendLine($"  Condensation threshold: R_local ≥ 0.80, min cells: 2");
        report.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        report.AppendLine("  Running condensation simulations...");

        var results = new ConcurrentBag<SimResult>();
        var points = (from n in Ns from lam in Lambdas from k in Ks from pm in Enum.GetValues<PlacementModel>()
                      select (n, lam, k, pm)).ToList();

        Parallel.ForEach(points, point =>
        {
            var (n, lam, k, pm) = point;
            var r = RunOne(n, lam, k, pm);
            results.Add(r);
        });

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Density Maps (show best condensate example) ──────
        AppendSection(report, "3. Condensation Overview");

        var withCondensates = results.Where(r => r.CondensateCount > 0).ToList();
        report.AppendLine($"  Parameter combos with condensates: {withCondensates.Count}/{total}");
        report.AppendLine();

        if (withCondensates.Count > 0)
        {
            report.AppendLine("  Top condensate-forming parameters:");
            report.AppendLine("  N   │ λ    │ K  │ Placement       │ Condensates │ Max Cells │ Max τ");
            report.AppendLine("  ────┼──────┼────┼─────────────────┼─────────────┼───────────┼───────");

            foreach (var r in withCondensates.OrderByDescending(r => r.CondensateCount).Take(10))
                report.AppendLine($"  {r.N,4} │ {r.Lambda,4:F2} │ {r.K,3:F0} │ {r.Placement,-15} │ {r.CondensateCount,11} │ {r.MaxCondensateCells,9} │ {r.MaxCondensateLifetime,5}");
        }
        else
        {
            report.AppendLine("  No condensates detected — local R never exceeds 0.80 threshold.");
        }
        report.AppendLine();

        // ── 4. Condensation Events ──────────────────────────────
        AppendSection(report, "4. Condensation by Placement Model");

        foreach (var pm in Enum.GetValues<PlacementModel>())
        {
            var subset = results.Where(r => r.Placement == pm.ToString()).ToList();
            int totalPM = subset.Count;
            int withCond = subset.Count(r => r.CondensateCount > 0);
            double avgCond = subset.Average(r => r.CondensateCount);
            double avgMaxR = subset.Average(r => r.MaxLocalR);
            double avgMeanR = subset.Average(r => r.MeanLocalR);

            report.AppendLine($"  {pm}:");
            report.AppendLine($"    Combos with condensates : {withCond}/{totalPM}");
            report.AppendLine($"    Mean condensates/run    : {avgCond:F2}");
            report.AppendLine($"    Mean max local R        : {avgMaxR:F4}");
            report.AppendLine($"    Mean mean local R       : {avgMeanR:F4}");
            report.AppendLine();
        }

        // ── 5. Local vs Global ──────────────────────────────────
        AppendSection(report, "5. Local vs Global Synchronization");

        report.AppendLine("  Mean values by (N, λ, K):");
        report.AppendLine("  N   │ λ    │ K  │ Global R │ Max Local R │ Mean Local R │ Condensates");
        report.AppendLine("  ────┼──────┼────┼──────────┼─────────────┼──────────────┼────────────");

        foreach (var g in results.GroupBy(r => (r.N, r.Lambda, r.K)).OrderBy(g => g.Key.N).ThenBy(g => g.Key.Lambda))
        {
            double avgGR = g.Average(r => r.GlobalR);
            double avgMLR = g.Average(r => r.MaxLocalR);
            double avgMeanLR = g.Average(r => r.MeanLocalR);
            double avgCond = g.Average(r => r.CondensateCount);

            report.AppendLine($"  {g.Key.N,4} │ {g.Key.Lambda,4:F2} │ {g.Key.K,2:F0} │ {avgGR,8:F4} │ {avgMLR,11:F4} │ {avgMeanLR,12:F4} │ {avgCond,10:F1}");
        }
        report.AppendLine();

        // ── 6. Lifetime Analysis ────────────────────────────────
        AppendSection(report, "6. Condensate Lifetime Analysis");

        var longLived = results.Where(r => r.MaxCondensateLifetime >= 1000).ToList();

        if (longLived.Count > 0)
        {
            report.AppendLine($"  Long-lived condensates (τ ≥ 1000): {longLived.Count} parameter combos");
            report.AppendLine("  N   │ λ    │ K  │ Placement       │ τ_max │ Cells │ Max R_local │ Global R");
            report.AppendLine("  ────┼──────┼────┼─────────────────┼───────┼───────┼─────────────┼─────────");

            foreach (var r in longLived.OrderByDescending(r => r.MaxCondensateLifetime).Take(10))
                report.AppendLine($"  {r.N,4} │ {r.Lambda,4:F2} │ {r.K,2:F0} │ {r.Placement,-15} │ {r.MaxCondensateLifetime,5} │ {r.MaxCondensateCells,5} │ {r.MaxLocalR,11:F4} │ {r.GlobalR,7:F4}");
        }
        else
        {
            report.AppendLine("  No condensates survived ≥ 1000 iterations.");
        }
        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        bool hasProtoMatter = results.Any(r =>
            r.GlobalR < 0.5 && r.MaxLocalR > 0.80 && r.MaxCondensateLifetime >= 1000);

        report.AppendLine($"  Proto-matter states (global R<0.5, local R>0.80, τ≥1000):");
        report.AppendLine($"    {(hasProtoMatter ? "DETECTED ✓" : "Not detected")}");
        report.AppendLine();

        report.AppendLine("  Q1. Localized condensates?");
        if (withCondensates.Count > 0)
            report.AppendLine($"    YES — {withCondensates.Count} combos produced condensates (max local R up to {results.Max(r => r.MaxLocalR):F3})");
        else
            report.AppendLine("    NO — local R never exceeded 0.80 threshold");
        report.AppendLine();

        report.AppendLine("  Q2. Critical local density?");
        var denseCond = results.Where(r => r.CondensateCount > 0)
            .GroupBy(r => (int)(r.MaxLocalR * 10))
            .OrderBy(g => g.Key);
        if (denseCond.Any())
            report.AppendLine($"    Condensates appear above R_local ≈ {denseCond.First().Average(r => r.MaxLocalR):F3}");
        else
            report.AppendLine("    No threshold identified");
        report.AppendLine();

        report.AppendLine("  Q3. Survive while global is low?");
        var surviving = results.Where(r => r.GlobalR < 0.5 && r.CondensateCount > 0).ToList();
        report.AppendLine($"    {(surviving.Count > 0 ? $"YES — {surviving.Count} combos have condensates despite low global sync" : "NO")}");
        report.AppendLine();

        report.AppendLine("  Q5. Multiple coexisting condensates?");
        int multi = results.Count(r => r.CondensateCount >= 2);
        report.AppendLine($"    {(multi > 0 ? $"YES — {multi} combos have ≥2 coexisting condensates" : "NO")}");
        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. Local resonance condensates {(withCondensates.Count > 0 ? "DO" : "do NOT")} emerge under");
        report.AppendLine("      distance-dependent coupling with heterogeneous spatial distributions.");
        report.AppendLine();

        if (hasProtoMatter)
        {
            report.AppendLine("  C2. Proto-matter states — where local coherent structures persist");
            report.AppendLine("      despite global incoherence — ARE detected. This is the first AT");
            report.AppendLine("      experiment to demonstrate localized resonance condensation.");
            report.AppendLine();
            report.AppendLine("  C3. These condensates are the strongest candidates yet for matter-like");
            report.AppendLine("      structures in the AT framework: spatially localized, internally");
            report.AppendLine("      coherent, and persistent against the surrounding incoherent background.");
        }
        else
        {
            report.AppendLine("  C2. Proto-matter states were not achieved — either condensates failed");
            report.AppendLine("      to form, or when they formed, global synchronization was also high.");
            report.AppendLine("      This suggests that stronger localization (smaller λ, clustered placement)");
            report.AppendLine("      or reduced coupling may be needed to isolate condensates.");
        }

        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • AT-011: Condensate-condensate interactions (proto-particle collisions).");
        report.AppendLine("    • AT-012: External perturbation response (condensate robustness).");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-010 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private SimResult RunOne(int n, double lambda, double k, PlacementModel pm)
    {
        int seed = BaseSeed + n * 1000 + (int)(lambda * 10000) + (int)(k * 100) + (int)pm * 7919;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Place oscillators according to model.
        PlaceOscillators(network, n, pm, rng);

        // Fill spatial coupling.
        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };

        var densityField = new LocalDensityField(GridSize);
        var condAnalyzer = new ResonanceCondensationAnalyzer
        {
            CondensationThreshold = 0.80,
            MinCondensateCells = 2,
            OverlapThreshold = 0.3
        };

        double globalR = 0;
        double maxLocalR = 0;
        double meanLocalR = 0;
        int condensateCount = 0;
        int maxCondCells = 0;
        int maxCondLifetime = 0;

        for (int iter = 0; iter < Iterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % CheckpointInterval == 0 || iter == Iterations - 1)
            {
                var metrics = SynchronizationMetrics.FromNetwork(network, iter + 1);
                globalR = metrics.OrderParameterR;

                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                maxLocalR = Math.Max(maxLocalR, densityField.MaxLocalR());
                meanLocalR = densityField.MeanLocalR();

                if (iter == Iterations - 1)
                {
                    var allCond = condAnalyzer.GetAllCondensates();
                    condensateCount = allCond.Count;
                    maxCondCells = allCond.Count > 0 ? allCond.Max(c => c.CellCount) : 0;
                    maxCondLifetime = allCond.Count > 0 ? allCond.Max(c => c.Lifetime) : 0;
                }
            }
        }

        return new SimResult(n, lambda, k, pm.ToString(), globalR, maxLocalR, meanLocalR,
            condensateCount, maxCondCells, maxCondLifetime);
    }

    private static void PlaceOscillators(TemporalNetwork network, int n, PlacementModel pm, Random rng)
    {
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase: phase, frequency: freq);
            PlaceOne(node, pm, rng, i, n);
            network.AddNode(node);
        }
    }

    private static void PlaceOne(TemporalNode node, PlacementModel pm, Random rng, int idx, int total)
    {
        switch (pm)
        {
            case PlacementModel.Uniform:
                node.X = rng.NextDouble();
                node.Y = rng.NextDouble();
                break;

            case PlacementModel.GaussianBlobs:
                // 3 Gaussian blobs at (0.25,0.25), (0.75,0.25), (0.5,0.75).
                var blobCenters = new[] { (0.25, 0.25), (0.75, 0.25), (0.5, 0.75) };
                var (cx, cy) = blobCenters[idx % 3];
                node.X = Clamp01(cx + NextGaussian(rng) * 0.08);
                node.Y = Clamp01(cy + NextGaussian(rng) * 0.08);
                break;

            case PlacementModel.MultipleClusters:
                // 5 tight clusters.
                var clusterCenters = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
                var (ccx, ccy) = clusterCenters[idx % 5];
                node.X = Clamp01(ccx + NextGaussian(rng) * 0.02);
                node.Y = Clamp01(ccy + NextGaussian(rng) * 0.02);
                break;

            case PlacementModel.Hierarchical:
                // Recursive subdivision: each level halves the region.
                double x = 0, y = 0, size = 1.0;
                int remaining = idx;
                int levels = (int)Math.Log(total, 4) + 1;
                for (int l = 0; l < levels && size > 0.01; l++)
                {
                    int quadrant = remaining % 4;
                    remaining /= 4;
                    if (quadrant == 1 || quadrant == 3) x += size / 2;
                    if (quadrant == 2 || quadrant == 3) y += size / 2;
                    size /= 2;
                }
                node.X = Math.Clamp(x + rng.NextDouble() * size, 0, 1);
                node.Y = Math.Clamp(y + rng.NextDouble() * size, 0, 1);
                break;
        }
    }

    private static double Clamp01(double v) => Math.Clamp(v, 0, 1);
    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }

    private sealed record SimResult(
        int N, double Lambda, double K, string Placement,
        double GlobalR, double MaxLocalR, double MeanLocalR,
        int CondensateCount, int MaxCondensateCells, int MaxCondensateLifetime);

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
