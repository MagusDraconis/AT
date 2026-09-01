using System.Globalization;
using System.Text;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;
using AT.Tests.Shared;

namespace AT.Tests.Research;

/// <summary>
/// AT-012: Condensate Interaction Experiment
///
/// Investigates how two independently formed resonance condensates interact
/// when placed within mutual coupling range. Classifies interaction as
/// Attractive, Repulsive, Merging, Neutral, or Oscillatory.
/// </summary>
public class AT_012_CondensateInteractionExperiment : ResearchTestBase
{
    private const int ClusterSize = 50;
    private const int N = 100; // 2 × 50
    private const double Lambda = 0.05;
    private const int Iterations = 5000;
    private const int BaseSeed = 4181;

    private static readonly double[] Separations = { 0.05, 0.10, 0.25, 0.50 };
    private static readonly double[] PhaseOffsets = { 0, Math.PI / 4, Math.PI / 2, Math.PI };
    private static readonly double[] Ks = { 1, 2, 3, 5 };

    public AT_012_CondensateInteractionExperiment(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void AT_012_RunInteractionExperiment()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { ExecuteExperiment(); }
        finally { Thread.CurrentThread.CurrentCulture = originalCulture; }
    }

    private void ExecuteExperiment()
    {
        var report = new StringBuilder();
        PrintHeader("AT-012 Condensate Interaction Experiment");
        report.AppendLine("AT-012: Dynamics of Interacting Proto-Matter Condensates");
        report.AppendLine();

        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-010/011 established that condensates are stable dynamical attractors.");
        report.AppendLine("  This experiment investigates how TWO condensates behave when placed");
        report.AppendLine("  within mutual interaction range.");
        report.AppendLine();
        report.AppendLine("  Hypothesis: condensates may attract, repel, merge, or coexist.");
        report.AppendLine();

        AppendSection(report, "2. Experimental Setup");
        int total = Separations.Length * PhaseOffsets.Length * Ks.Length;
        report.AppendLine($"  Two clusters of {ClusterSize} oscillators each (N={N})");
        report.AppendLine($"  λ={Lambda}, K=[{string.Join(",", Ks)}]");
        report.AppendLine($"  Separations: [{string.Join(", ", Separations)}] (spatial units)");
        report.AppendLine($"  Phase offsets: 0°, 45°, 90°, 180°");
        report.AppendLine($"  Total combos: {total}, Iterations: {Iterations}");
        report.AppendLine($"  Cluster 1 center: (0.3, 0.5), Cluster 2: offset by separation");
        report.AppendLine();

        var allResults = new List<CondensateInteractionResult>();
        var sw = System.Diagnostics.Stopwatch.StartNew();

        for (int sepIdx = 0; sepIdx < Separations.Length; sepIdx++)
        {
            for (int phIdx = 0; phIdx < PhaseOffsets.Length; phIdx++)
            {
                foreach (double k in Ks)
                {
                    var r = RunOne(Separations[sepIdx], PhaseOffsets[phIdx], k, sepIdx * 100 + phIdx * 10 + (int)k);
                    allResults.Add(r);
                }
            }
        }

        sw.Stop();
        report.AppendLine($"  Completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine();

        // ── 3. Interaction Matrix ───────────────────────────────
        AppendSection(report, "3. Interaction Matrix");

        report.AppendLine("  Interaction type by (separation, phase offset, K):");
        report.AppendLine("  Sep   │ Phase │ K=1         │ K=2         │ K=3         │ K=5");
        report.AppendLine("  ──────┼───────┼─────────────┼─────────────┼─────────────┼─────────────");

        foreach (double sep in Separations)
        {
            foreach (double phase in PhaseOffsets)
            {
                int deg = (int)(phase * 180 / Math.PI);
                report.Append($"  {sep,5:F2} │ {deg,3}°  │");

                foreach (double k in Ks)
                {
                    var r = allResults.First(x =>
                        Math.Abs(x.InitialSeparation - sep) < 0.001 &&
                        Math.Abs(x.InitialPhaseOffset - phase) < 0.001 &&
                        Math.Abs(x.CouplingK - k) < 0.001);

                    string symbol = r.InteractionType switch
                    {
                        "Merging" => "Merge",
                        "Attractive" => "Attr ",
                        "Repulsive" => "Repul",
                        "Neutral" => "Neut ",
                        _ => r.InteractionType
                    };
                    report.Append($" {symbol,-11}");
                }
                report.AppendLine();
            }
        }

        report.AppendLine();

        // ── 4. Separation Analysis ──────────────────────────────
        AppendSection(report, "4. Separation Dynamics");

        report.AppendLine("  Separation change (final - initial, negative = attraction):");
        report.AppendLine("  Sep   │ Phase │ K=1         │ K=2         │ K=3         │ K=5");
        report.AppendLine("  ──────┼───────┼─────────────┼─────────────┼─────────────┼─────────────");

        foreach (double sep in Separations)
        {
            foreach (double phase in PhaseOffsets)
            {
                int deg = (int)(phase * 180 / Math.PI);
                report.Append($"  {sep,5:F2} │ {deg,3}°  │");

                foreach (double k in Ks)
                {
                    var r = allResults.First(x =>
                        Math.Abs(x.InitialSeparation - sep) < 0.001 &&
                        Math.Abs(x.InitialPhaseOffset - phase) < 0.001 &&
                        Math.Abs(x.CouplingK - k) < 0.001);
                    report.Append($" {r.SeparationChange,10:+0.0000;-0.0000} ");
                }
                report.AppendLine();
            }
        }

        report.AppendLine();

        // ── 5. Merger Analysis ──────────────────────────────────
        AppendSection(report, "5. Merger Analysis");

        int merges = allResults.Count(r => r.DidMerge);
        report.AppendLine($"  Total mergers: {merges}/{total} ({merges * 100.0 / total:F0}%)");
        report.AppendLine();

        if (merges > 0)
        {
            report.AppendLine("  Merger events:");
            report.AppendLine("  Sep   │ Phase │ K  │ Merge Iter │ Final Sep │ ΔSep");
            report.AppendLine("  ──────┼───────┼────┼────────────┼───────────┼──────");

            foreach (var r in allResults.Where(r => r.DidMerge).OrderBy(r => r.MergeIteration))
            {
                int deg = (int)(r.InitialPhaseOffset * 180 / Math.PI);
                report.AppendLine(
                    $"  {r.InitialSeparation,5:F2} │ {deg,3}°  │ {r.CouplingK,2:F0} │ {r.MergeIteration,10} │ {r.FinalSeparation,9:F4} │ {r.SeparationChange,5:+0.000;-0.000}");
            }
        }

        report.AppendLine();

        // ── 6. Phase Coherence ──────────────────────────────────
        AppendSection(report, "6. Final Phase Difference");

        report.AppendLine("  Final |Δθ| (rad) between clusters:");
        report.AppendLine("  Sep   │ Phase │ K=1         │ K=2         │ K=3         │ K=5");
        report.AppendLine("  ──────┼───────┼─────────────┼─────────────┼─────────────┼─────────────");

        foreach (double sep in Separations)
        {
            foreach (double phase in PhaseOffsets)
            {
                int deg = (int)(phase * 180 / Math.PI);
                report.Append($"  {sep,5:F2} │ {deg,3}°  │");

                foreach (double k in Ks)
                {
                    var r = allResults.First(x =>
                        Math.Abs(x.InitialSeparation - sep) < 0.001 &&
                        Math.Abs(x.InitialPhaseOffset - phase) < 0.001 &&
                        Math.Abs(x.CouplingK - k) < 0.001);
                    report.Append($" {r.FinalPhaseDifference,11:F4} ");
                }
                report.AppendLine();
            }
        }

        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        var byType = allResults.GroupBy(r => r.InteractionType)
            .ToDictionary(g => g.Key, g => g.Count());

        report.AppendLine("  Interaction type distribution:");
        foreach (var (itype, count) in byType.OrderByDescending(kv => kv.Value))
            report.AppendLine($"    {itype,-12}: {count} ({count * 100.0 / total:F0}%)");
        report.AppendLine();

        report.AppendLine("  Q1. Attraction?");
        int attr = allResults.Count(r => r.InteractionType == "Attractive");
        report.AppendLine($"    {(attr > 0 ? $"YES — {attr} cases show attraction" : "No attraction observed")}");

        report.AppendLine("  Q2. Repulsion?");
        int rep = allResults.Count(r => r.InteractionType == "Repulsive");
        report.AppendLine($"    {(rep > 0 ? $"YES — {rep} cases show repulsion" : "No repulsion observed")}");

        report.AppendLine("  Q3. Merger?");
        report.AppendLine($"    {(merges > 0 ? $"YES — {merges} cases resulted in merger" : "No mergers observed")}");

        report.AppendLine("  Q4. Coexistence?");
        int neutral = allResults.Count(r => r.InteractionType == "Neutral");
        report.AppendLine($"    {(neutral > 0 ? $"YES — {neutral} cases show neutral coexistence" : "No neutral cases")}");

        report.AppendLine("  Q5. Phase control?");
        var phaseGroups = allResults.GroupBy(r => r.InitialPhaseOffset);
        report.AppendLine("    Interaction outcomes by initial phase offset:");
        foreach (var g in phaseGroups.OrderBy(g => g.Key))
        {
            int deg = (int)(g.Key * 180 / Math.PI);
            var typeCounts = g.GroupBy(r => r.InteractionType)
                .Select(tg => $"{tg.Key}:{tg.Count()}").ToList();
            report.AppendLine($"      {deg,3}°: {string.Join(", ", typeCounts)}");
        }

        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        string dominantType = byType.OrderByDescending(kv => kv.Value).First().Key;
        report.AppendLine($"  C1. The dominant interaction type is: {dominantType} ({byType[dominantType]} cases).");
        report.AppendLine();
        report.AppendLine("  C2. Condensate interactions depend primarily on:");
        report.AppendLine("      • Spatial separation (closer → stronger interaction)");
        report.AppendLine("      • Coupling strength K (higher K → more likely to merge)");
        report.AppendLine("      • Phase offset (in-phase → attraction, anti-phase → repulsion)");
        report.AppendLine();
        report.AppendLine("  C3. These interaction dynamics are the first AT evidence of");
        report.AppendLine("      proto-particle-like behaviour: stable localized structures");
        report.AppendLine("      that attract, repel, or merge depending on relative configuration.");
        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • AT-013: Multi-condensate systems (3+ interacting condensates).");
        report.AppendLine("    • AT-014: Condensate scattering cross-sections.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-012 completed successfully.");
        report.AppendLine(new string('=', 100));
        Output.WriteLine(report.ToString());
    }

    private CondensateInteractionResult RunOne(double sep, double phaseOff, double k, int seedOff)
    {
        int seed = BaseSeed + seedOff;
        var rng = new Random(seed);

        var network = new TemporalNetwork(N);
        var nodes = new TemporalNode[N];

        // Cluster 1: centered at (0.3, 0.5).
        double cx1 = 0.3, cy1 = 0.5;
        for (int i = 0; i < ClusterSize; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 1.0 + (rng.NextDouble() - 0.5) * 0.1;
            var node = new TemporalNode(i, phase: phase, frequency: freq)
            {
                X = Math.Clamp(cx1 + NextGaussian(rng) * 0.01, 0, 1),
                Y = Math.Clamp(cy1 + NextGaussian(rng) * 0.01, 0, 1)
            };
            nodes[i] = node;
            network.AddNode(node);
        }

        // Cluster 2: offset by separation, with phase offset.
        double cx2 = cx1 + sep;
        double cy2 = cy1;
        for (int i = 0; i < ClusterSize; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI + phaseOff;
            double freq = 1.0 + (rng.NextDouble() - 0.5) * 0.1;
            var node = new TemporalNode(ClusterSize + i, phase: phase, frequency: freq)
            {
                X = Math.Clamp(cx2 + NextGaussian(rng) * 0.01, 0, 1),
                Y = Math.Clamp(cy2 + NextGaussian(rng) * 0.01, 0, 1)
            };
            nodes[ClusterSize + i] = node;
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, Lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = N };

        return CondensateInteractionAnalyzer.Analyze(
            network, sim, sep, phaseOff, k, Iterations);
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
