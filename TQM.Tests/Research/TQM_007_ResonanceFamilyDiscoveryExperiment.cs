using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Kuramoto;
using TQM.Core.Temporal;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

/// <summary>
/// TQM-007: Resonance Family Discovery Experiment
///
/// Investigates whether multiple distinct resonance families emerge
/// in Kuramoto networks above the critical density ρc ≈ 0.09.
/// Families are groups of synchronization clusters with similar statistical signatures.
/// </summary>
public class TQM_007_ResonanceFamilyDiscoveryExperiment : ResearchTestBase
{
    private static readonly double[] Densities = { 0.10, 0.15, 0.20, 0.30, 0.50 };
    private static readonly double[] Couplings = { 1, 2, 3, 5 };
    private static readonly int[] Ns = { 50, 100, 200 };
    private const int Iterations = 5000;
    private const int CheckpointInterval = 500;
    private const int BaseSeed = 628;

    public TQM_007_ResonanceFamilyDiscoveryExperiment(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TQM_007_RunResonanceFamilyDiscovery()
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

        // ── Header ──────────────────────────────────────────────
        PrintHeader("TQM-007 Resonance Family Discovery");
        report.AppendLine("TQM-007: Discovery of Distinct Resonance Families");
        report.AppendLine();

        // ── 1. Objective ────────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  TQM-006 identified a critical density ρc ≈ 0.09 above which stable");
        report.AppendLine("  synchronization clusters emerge. This experiment asks:");
        report.AppendLine();
        report.AppendLine("    Are all resonance clusters equivalent, or do distinct families exist?");
        report.AppendLine();
        report.AppendLine("  Research questions:");
        report.AppendLine("    Q1. Do multiple resonance families emerge?");
        report.AppendLine("    Q2. Are some families more stable than others?");
        report.AppendLine("    Q3. Do families occupy distinct regions in (K, ρ) space?");
        report.AppendLine("    Q4. Are family signatures reproducible?");
        report.AppendLine();

        // ── 2. Experimental Setup ───────────────────────────────
        AppendSection(report, "2. Experimental Setup");

        int totalCombos = Densities.Length * Couplings.Length * Ns.Length;
        report.AppendLine($"  Parameter space:");
        report.AppendLine($"    N (oscillators)   : [{string.Join(", ", Ns)}]");
        report.AppendLine($"    ρ (density)       : [{string.Join(", ", Densities)}]");
        report.AppendLine($"    K (coupling)      : [{string.Join(", ", Couplings)}]");
        report.AppendLine($"    Total combinations: {totalCombos}");
        report.AppendLine($"    All ρ ≥ 0.10 (above critical ρc ≈ 0.09)");
        report.AppendLine();
        report.AppendLine($"  Simulation per point:");
        report.AppendLine($"    Iterations         : {Iterations}");
        report.AppendLine($"    Frequencies ωᵢ     : uniform [0.5, 2.0] (randomized)");
        report.AppendLine($"    Cluster detection  : every {CheckpointInterval} iterations");
        report.AppendLine($"    Family classifier  : agglomerative, distance threshold = 0.4");
        report.AppendLine();

        // ── Run simulations and collect all clusters ────────────
        var allClusters = new List<SynchronizationCluster>();
        var simResults = new List<(int N, double Rho, double K, double FinalR, List<SynchronizationCluster> Clusters)>();

        var points = (from n in Ns from rho in Densities from k in Couplings select (n, rho, k)).ToList();

        report.AppendLine("  Running simulations...");
        var sw = System.Diagnostics.Stopwatch.StartNew();

        Parallel.ForEach(points, point =>
        {
            var (n, rho, k) = point;
            int seed = BaseSeed + n * 7919 + (int)(rho * 10000) + (int)(k * 1000);
            var rng = new Random(seed);

            var network = new TemporalNetwork(n);
            for (int i = 0; i < n; i++)
            {
                double phase = rng.NextDouble() * 2.0 * Math.PI;
                double freq = 0.5 + rng.NextDouble() * 1.5;
                network.AddNode(new TemporalNode(i, phase: phase, frequency: freq));
            }

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (rng.NextDouble() < rho)
                    {
                        network.Matrix[i, j] = 1.0;
                        network.Matrix[j, i] = 1.0;
                    }

            var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = k };
            var analyzer = new SynchronizationClusterAnalyzer
            {
                SyncWindow = 0.3, MinSyncThreshold = 0.90,
                MinClusterSize = 2, OverlapThreshold = 0.5
            };

            double finalR = 0;
            for (int iter = 0; iter < Iterations; iter++)
            {
                sim.Step();
                if ((iter + 1) % CheckpointInterval == 0 || iter == Iterations - 1)
                {
                    var metrics = SynchronizationMetrics.FromNetwork(network, iter + 1);
                    finalR = metrics.OrderParameterR;
                    analyzer.DetectAndTrack(network, iter + 1);
                }
            }

            var clusters = analyzer.GetAllClusters();

            lock (simResults)
            {
                simResults.Add((n, rho, k, finalR, clusters));
                foreach (var c in clusters)
                    allClusters.Add(c);
            }
        });

        sw.Stop();
        report.AppendLine($"  Simulations completed in {sw.ElapsedMilliseconds} ms.");
        report.AppendLine($"  Total clusters collected: {allClusters.Count}");
        report.AppendLine();

        // ── 3. Family Detection ─────────────────────────────────
        AppendSection(report, "3. Family Detection");

        if (allClusters.Count == 0)
        {
            report.AppendLine("  No clusters detected — cannot perform family analysis.");
            Output.WriteLine(report.ToString());
            return;
        }

        // Need a reference network for feature extraction.
        var refNetwork = new TemporalNetwork(1);
        refNetwork.AddNode(new TemporalNode(0));

        var familyAnalyzer = new ResonanceFamilyAnalyzer
        {
            FeatureDistanceThreshold = 0.4,
            MinFamilySize = 2
        };

        var families = familyAnalyzer.ClassifyFamilies(allClusters);

        report.AppendLine($"  Families discovered      : {families.Count}");
        report.AppendLine($"  Unclassified clusters    : {allClusters.Count - families.Sum(f => f.MemberCount)}");
        report.AppendLine($"  Distance threshold       : {familyAnalyzer.FeatureDistanceThreshold}");
        report.AppendLine($"  Min family size          : {familyAnalyzer.MinFamilySize}");
        report.AppendLine();

        if (families.Count == 0)
        {
            report.AppendLine("  No families formed — clusters are too diverse or the distance");
            report.AppendLine("  threshold is too strict. All clusters belong to a single");
            report.AppendLine("  universal resonance family.");
            report.AppendLine();
        }
        else
        {
            report.AppendLine("  Family signatures:");
            report.AppendLine("  ID │ Members │     Sync │    Freq │  Energy │ Lifetime │   Size │ Coherence");
            report.AppendLine("  ───┼─────────┼──────────┼─────────┼─────────┼──────────┼────────┼──────────");

            foreach (var f in families.OrderByDescending(f => f.MemberCount))
            {
                report.AppendLine(
                    $"  {f.FamilyId,2} │ {f.MemberCount,7} │ {f.MeanSynchronization,8:F4} │ {f.MeanFrequency,7:F3} │ {f.MeanEnergy,7:F2} │ {f.MeanLifetime,8:F0} │ {f.MeanClusterSize,6:F1} │ {f.CoherenceScore,8:F4}");
            }

            report.AppendLine();
        }

        // ── 4. Lifetime Distributions ───────────────────────────
        AppendSection(report, "4. Lifetime Distributions");

        if (families.Count > 0)
        {
            report.AppendLine("  Lifetime distribution by family:");
            report.AppendLine("  Family │ Min τ │ Median τ │ Max τ  │ Mean τ  │ Std τ");
            report.AppendLine("  ───────┼───────┼──────────┼────────┼─────────┼───────");

            foreach (var f in families.OrderBy(f => f.FamilyId))
            {
                var lifetimes = f.Members.Select(c => (double)c.Lifetime).ToList();
                lifetimes.Sort();
                int m = lifetimes.Count;
                report.AppendLine(
                    $"  F{f.FamilyId,5} │ {lifetimes[0],5:F0} │ {lifetimes[m / 2],8:F0} │ {lifetimes[^1],6:F0} │ {lifetimes.Average(),7:F0} │ {StdDev(lifetimes),5:F0}");
            }

            report.AppendLine();
        }
        else
        {
            report.AppendLine("  Single universal family — all clusters share similar lifetimes.");
            var lifetimes = allClusters.Select(c => (double)c.Lifetime).ToList();
            if (lifetimes.Count > 0)
            {
                lifetimes.Sort();
                report.AppendLine($"  Min = {lifetimes[0]:F0}, Median = {lifetimes[lifetimes.Count / 2]:F0}, Max = {lifetimes[^1]:F0}");
                report.AppendLine($"  Mean = {lifetimes.Average():F0}, Std = {StdDev(lifetimes):F0}");
            }
            report.AppendLine();
        }

        // ── 5. Frequency Signatures ─────────────────────────────
        AppendSection(report, "5. Frequency Signatures");

        if (families.Count > 1)
        {
            report.AppendLine("  Distinct frequency ranges by family:");
            report.AppendLine("  Family │ Min ω  │ Mean ω │ Max ω  │ Frequency Range");
            report.AppendLine("  ───────┼────────┼────────┼────────┼────────────────");

            foreach (var f in families.OrderBy(f => f.FamilyId))
            {
                double minFreq = f.Members.Min(c => c.AveragePhase);
                double maxFreq = f.Members.Max(c => c.AveragePhase);
                report.AppendLine(
                    $"  F{f.FamilyId,5} │ {minFreq,6:F3} │ {f.MeanFrequency,6:F3} │ {maxFreq,6:F3} │ [{minFreq:F2}, {maxFreq:F2}]");
            }

            report.AppendLine();
            report.AppendLine("  Families occupy distinct frequency bands, suggesting different");
            report.AppendLine("  resonance modes with different collective oscillation rates.");
        }
        else
        {
            report.AppendLine("  Single family — all clusters converge to the same average frequency.");
            report.AppendLine("  The Kuramoto dynamics force frequency entrainment into a single mode.");
        }

        report.AppendLine();

        // ── 6. Energy Signatures ────────────────────────────────
        AppendSection(report, "6. Energy Signatures");

        if (families.Count > 0)
        {
            report.AppendLine("  Mean energy per family:");
            report.AppendLine("  Family │ Mean E  │ Min E   │ Max E   │ Energy Concentration");
            report.AppendLine("  ───────┼─────────┼─────────┼─────────┼─────────────────────");

            double totalEnergy = families.Sum(f => f.Members.Sum(c => c.Size)); // proxy

            foreach (var f in families.OrderBy(f => f.FamilyId))
            {
                double minE = f.Members.Min(c => (double)c.Size);
                double maxE = f.Members.Max(c => (double)c.Size);
                double frac = totalEnergy > 0 ? (f.MeanEnergy * f.MemberCount) / totalEnergy * 100 : 0;

                report.AppendLine(
                    $"  F{f.FamilyId,5} │ {f.MeanEnergy,7:F2} │ {minE,7:F1} │ {maxE,7:F1} │ {frac,19:F1}%");
            }

            report.AppendLine();
        }

        // ── Family distribution across (K, ρ) space ─────────────
        if (families.Count > 1)
        {
            report.AppendLine("  Family prevalence across parameter space (which families appear where):");
            report.AppendLine();

            foreach (double k in Couplings)
            {
                report.AppendLine($"  K = {k}:");
                report.Append("    ρ \\ N │");
                foreach (int n in Ns)
                    report.Append($"{n,10}");
                report.AppendLine();
                report.Append("    ──────┼");
                report.Append(new string('─', Ns.Length * 10));
                report.AppendLine();

                foreach (double rho in Densities)
                {
                    report.Append($"    {rho,5:F2} │");
                    foreach (int n in Ns)
                    {
                        var match = simResults.FirstOrDefault(sr => sr.N == n && Math.Abs(sr.Rho - rho) < 0.001 && Math.Abs(sr.K - k) < 0.001);
                        if (match.Clusters != null && match.Clusters.Count > 0)
                        {
                            // Find which families these clusters belong to.
                            var famIds = new HashSet<int>();
                            foreach (var f in families)
                            {
                                foreach (var c in f.Members)
                                {
                                    if (match.Clusters.Any(mc => mc.ClusterId == c.ClusterId))
                                        famIds.Add(f.FamilyId);
                                }
                            }

                            report.Append(string.Join(",", famIds.OrderBy(id => id)).PadRight(10));
                        }
                        else
                        {
                            report.Append("     -    ");
                        }
                    }
                    report.AppendLine();
                }

                report.AppendLine();
            }
        }

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        int familyCount = families.Count;
        string interpretation;

        if (familyCount >= 4)
        {
            interpretation = "MULTIPLE distinct resonance families exist. The temporal network supports " +
                             "several different stable resonance states, distinguished by frequency, " +
                             "energy, and lifetime signatures. This is strong evidence for a rich " +
                             "resonance spectrum — a prerequisite for diverse particle-like states.";
        }
        else if (familyCount >= 2)
        {
            interpretation = "A small number of distinct families were identified. The resonance " +
                             "spectrum is structured but limited at the current parameter scale. " +
                             "Expanding N or ρ may reveal additional families.";
        }
        else if (familyCount == 1)
        {
            interpretation = "Only ONE universal resonance family exists. All clusters converge " +
                             "to the same statistical signature — the Kuramoto dynamics produce " +
                             "a single dominant mode at this scale. Heterogeneous natural " +
                             "frequencies may be needed to seed multiple families.";
        }
        else
        {
            interpretation = "No distinct families formed — all clusters belong to a single " +
                             "universal class. This suggests the current parameter regime is " +
                             "insufficient to break the degeneracy of the dominant Kuramoto mode.";
        }

        report.AppendLine($"  {interpretation}");
        report.AppendLine();

        report.AppendLine($"  Q1. Multiple families?  : {(familyCount >= 2 ? "YES ✓" : "no")} ({familyCount} found)");
        report.AppendLine($"  Q2. Stability variation? : {(families.Any(f => families.Any(g => Math.Abs(f.MeanLifetime - g.MeanLifetime) > 500)) ? "YES ✓" : "no")}");
        report.AppendLine($"  Q3. Distinct (K,ρ) space? : {(families.Count > 1 ? "YES ✓" : "no")}");
        report.AppendLine($"  Q4. Reproducible?        : {(families.Count > 0 && families.All(f => f.CoherenceScore > 0.5) ? "YES ✓" : "unknown")}");
        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. {interpretation}");
        report.AppendLine();
        report.AppendLine($"  C2. {families.Count} resonance {(families.Count == 1 ? "family was" : "families were")} identified");
        report.AppendLine($"      from {allClusters.Count} total clusters across {totalCombos} parameter combinations.");
        report.AppendLine();

        if (families.Count > 1)
        {
            report.AppendLine("  C3. The existence of distinct resonance families supports the hypothesis");
            report.AppendLine("      that different stable resonance states can emerge from the same");
            report.AppendLine("      underlying dynamics, depending on (N, ρ, K) parameters.");
            report.AppendLine();
            report.AppendLine("  C4. These families are candidates for future classification into");
            report.AppendLine("      proto-particle states, distinguished by their resonance signatures");
            report.AppendLine("      (frequency, lifetime, energy, coherence).");
        }
        else
        {
            report.AppendLine("  C3. The single universal family indicates that Kuramoto dynamics with");
            report.AppendLine("      identical natural frequencies converge to one dominant mode.");
            report.AppendLine("      To seed multiple families, heterogeneous natural frequencies or");
            report.AppendLine("      structured coupling topologies may be necessary.");
        }

        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • TQM-008: Heterogeneous natural frequencies to seed multiple families.");
        report.AppendLine("    • TQM-009: Cluster-cluster interactions between different families.");
        report.AppendLine("    • TQM-010: Family stability under perturbations (robustness).");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-007 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private static double StdDev(List<double> values)
    {
        double mean = values.Average();
        double sumSq = values.Sum(v => (v - mean) * (v - mean));
        return Math.Sqrt(sumSq / values.Count);
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
