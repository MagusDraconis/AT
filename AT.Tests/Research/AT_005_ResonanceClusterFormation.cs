using System.Globalization;
using System.Text;
using AT.Core.Resonance;
using AT.Core.Temporal;
using AT.Core.TemporalField;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

/// <summary>
/// AT-005: Resonance Cluster Formation Experiment
///
/// Investigates whether 100 oscillators interacting exclusively through a shared
/// 1D temporal field self-organize into stable, persistent resonance clusters.
///
/// Central hypothesis:
///   Matter = dynamically stabilized resonance structures inside the temporal field.
/// </summary>
public class AT_005_ResonanceClusterFormation : ResearchTestBase
{
    private const int FieldCells = 200;
    private const int OscillatorCount = 100;
    private const int TotalIterations = 20000;
    private const int DetectionInterval = 500; // check for clusters every N iterations
    private const int RandomSeed = 314;

    public AT_005_ResonanceClusterFormation(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void AT_005_RunResonanceClusterExperiment()
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
        PrintHeader("AT-005 Resonance Cluster Formation");
        report.AppendLine("AT-005: Self-Organizing Resonance Clusters in a Temporal Field");
        report.AppendLine();

        // ── 1. Objective ────────────────────────────────────────
        AppendSection(report, "1. Objective");
        report.AppendLine("  AT-004 showed that two asymmetric oscillators do not synchronize via");
        report.AppendLine("  a shared temporal field — the field amplifies local density asymmetries.");
        report.AppendLine();
        report.AppendLine("  This experiment tests a different hypothesis:");
        report.AppendLine("    The temporal field acts as a RESONANCE medium, not a synchronization medium.");
        report.AppendLine("    With many oscillators, self-reinforcing resonance clusters may form —");
        report.AppendLine("    localized, persistent, high-density structures that trap oscillators.");
        report.AppendLine();
        report.AppendLine("  Research questions:");
        report.AppendLine("    Q1. Do stable high-density regions emerge in the temporal field?");
        report.AppendLine("    Q2. Do these regions persist longer than random density fluctuations?");
        report.AppendLine("    Q3. Do oscillators cluster around these regions?");
        report.AppendLine("    Q4. Does energy become concentrated in persistent structures?");
        report.AppendLine();

        // ── 2. Initial Conditions ───────────────────────────────
        AppendSection(report, "2. Initial Conditions");

        // Create network.
        var network = new TemporalNetwork(OscillatorCount);
        var rng = new Random(RandomSeed);
        for (int i = 0; i < OscillatorCount; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5; // [0.5, 2.0]
            network.AddNode(new TemporalNode(i, phase: phase, frequency: freq));
        }

        // Create field.
        var field = new TemporalField(FieldCells)
        {
            PropagationSpeed = 0.3,
            DiffusionCoefficient = 0.02,
            DampingCoefficient = 0.001,
            EnergyToDensity = 1.0
        };

        // Assign oscillator positions spread evenly across the field.
        int[] oscPositions = new int[OscillatorCount];
        for (int i = 0; i < OscillatorCount; i++)
            oscPositions[i] = (int)((double)i / OscillatorCount * FieldCells) % FieldCells;

        // Create simulation.
        var sim = new TemporalFieldSimulation(network, field, oscPositions)
        {
            FieldCouplingAlpha = 0.2,
            InjectionStrength = 1.5,
            TimeStep = 1.0
        };

        report.AppendLine($"  Field cells              : {FieldCells}");
        report.AppendLine($"  Oscillators              : {OscillatorCount}");
        report.AppendLine($"  Position distribution    : uniform across field");
        report.AppendLine($"  Frequency range          : [0.5, 2.0] (uniform random)");
        report.AppendLine($"  Initial phases           : uniform [0, 2π)");
        report.AppendLine($"  Total iterations         : {TotalIterations}");
        report.AppendLine();
        report.AppendLine($"  Propagation speed c      : {field.PropagationSpeed}");
        report.AppendLine($"  Diffusion D              : {field.DiffusionCoefficient}");
        report.AppendLine($"  Damping γ                : {field.DampingCoefficient}");
        report.AppendLine($"  Coupling α               : {sim.FieldCouplingAlpha}");
        report.AppendLine($"  Injection β              : {sim.InjectionStrength}");
        report.AppendLine($"  Direct coupling          : NONE (field-mediated only)");
        report.AppendLine();

        // ── Cluster analyzer ────────────────────────────────────
        var analyzer = new ResonanceClusterAnalyzer
        {
            ThresholdFactor = 1.5,
            MinClusterSize = 3,
            OverlapThreshold = 0.3
        };

        // ── Run simulation with periodic cluster detection ──────
        var allClusterSnapshots = new List<List<ResonanceCluster>>();
        var snapshotIterations = new List<int>();

        for (int iter = 0; iter < TotalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % DetectionInterval == 0 || iter == 0)
            {
                var clusters = analyzer.DetectAndTrack(
                    field, iter + 1, network, oscPositions);
                allClusterSnapshots.Add(clusters);
                snapshotIterations.Add(iter + 1);
            }
        }

        // Compute final results.
        var results = analyzer.ComputeResults(
            allClusterSnapshots.LastOrDefault() ?? new List<ResonanceCluster>(),
            TotalIterations,
            field.TotalEnergy(),
            OscillatorCount);

        // ── 3. Field Evolution ──────────────────────────────────
        AppendSection(report, "3. Field Evolution");

        int[] showSnapshots = { 1, 10, 20, 30, 40 };
        report.AppendLine("  Snapshot │ Total Energy  │ Mean Density  │ Peak Density  │ Peak Cell");
        report.AppendLine("  ────────┼───────────────┼───────────────┼───────────────┼──────────");

        foreach (int idx in showSnapshots)
        {
            if (idx <= allClusterSnapshots.Count)
            {
                int snapIdx = idx - 1;
                var s = field.TakeSnapshot(snapshotIterations[snapIdx]);
                report.AppendLine(
                    $"  {idx,7} │ {s.TotalEnergy,13:F2} │ {s.MeanDensity,13:F6} │ {s.PeakDensity,13:F6} │ {s.PeakCellIndex,8}");
            }
        }

        report.AppendLine();

        // Density profile at start and end.
        var firstFieldSnap = field.TakeSnapshot(snapshotIterations[0]);
        var lastFieldSnap = field.TakeSnapshot(snapshotIterations[^1]);

        report.AppendLine("  Density profile at iteration 500 (early):");
        PrintDensityProfile(report, firstFieldSnap.DensityProfile, 10);
        report.AppendLine();

        report.AppendLine($"  Density profile at iteration {snapshotIterations[^1]} (final):");
        PrintDensityProfile(report, lastFieldSnap.DensityProfile, 10);
        report.AppendLine();

        // ── 4. Cluster Detection ────────────────────────────────
        AppendSection(report, "4. Cluster Detection");

        report.AppendLine($"  Detection threshold       : mean + {analyzer.ThresholdFactor}×σ");
        report.AppendLine($"  Minimum cluster size      : {analyzer.MinClusterSize} cells");
        report.AppendLine($"  Detection interval        : {DetectionInterval} iterations");
        report.AppendLine($"  Total snapshots analyzed  : {allClusterSnapshots.Count}");
        report.AppendLine();

        report.AppendLine("  Cluster count over time:");
        report.AppendLine("  Snapshot │ Iter   │ Active Clusters │ Total Tracked");
        report.AppendLine("  ────────┼────────┼─────────────────┼──────────────");

        var displayIndices = new[] { 0, 10, 20, 30, 39 };
        foreach (int di in displayIndices)
        {
            if (di < allClusterSnapshots.Count)
            {
                int iter = snapshotIterations[di];
                int active = allClusterSnapshots[di].Count;

                // Count total unique clusters tracked so far.
                var idsSeen = new HashSet<int>();
                for (int s = 0; s <= di; s++)
                    foreach (var c in allClusterSnapshots[s])
                        idsSeen.Add(c.Id);

                report.AppendLine(
                    $"  {di + 1,7} │ {iter,6} │ {active,15} │ {idsSeen.Count,12}");
            }
        }

        report.AppendLine();

        // Show current clusters at final snapshot.
        var finalClusters = allClusterSnapshots[^1];
        report.AppendLine($"  Clusters active at final iteration ({snapshotIterations[^1]}):");
        report.AppendLine("  ID │ Cells [start..end] │ Size │ Energy   │ Lifetime │ Oscillators │ Stability");
        report.AppendLine("  ───┼────────────────────┼──────┼──────────┼──────────┼─────────────┼──────────");

        var sortedFinal = finalClusters.OrderByDescending(c => c.Lifetime).Take(10);
        foreach (var c in sortedFinal)
        {
            report.AppendLine(
                $"  {c.Id,2} │ [{c.StartCell,3}..{c.EndCell,3}]       │ {c.Size,4} │ {c.TotalEnergy,8:F2} │ {c.Lifetime,8} │ {c.OscillatorIndices.Count,11} │ {c.StabilityScore,8:F4}");
        }

        if (finalClusters.Count > 10)
            report.AppendLine($"  ... and {finalClusters.Count - 10} more clusters.");
        report.AppendLine();

        // ── 5. Cluster Stability ────────────────────────────────
        AppendSection(report, "5. Cluster Stability");

        int stabilityThreshold = TotalIterations / 20;
        report.AppendLine($"  Stability threshold       : {stabilityThreshold} iterations (5% of total)");
        report.AppendLine($"  Total clusters detected   : {results.TotalClustersDetected}");
        report.AppendLine($"  Stable clusters (≥{stabilityThreshold} iter) : {results.StableClusterCount}");
        report.AppendLine($"  Active clusters at end    : {results.ActiveClustersAtEnd}");
        report.AppendLine($"  Mean cluster lifetime     : {results.MeanClusterLifetime:F1} iterations");
        report.AppendLine($"  Max cluster lifetime      : {results.MaxClusterLifetime} iterations");
        report.AppendLine($"  Mean stability score      : {results.MeanStabilityScore:F4}");
        report.AppendLine();

        // Show top stable clusters.
        if (results.StableClusters.Count > 0)
        {
            report.AppendLine("  Most stable clusters:");
            report.AppendLine("  ID │ Lifetime │ Size │ Peak Density │ Oscillators │ Center Cell");
            report.AppendLine("  ───┼──────────┼──────┼──────────────┼─────────────┼────────────");

            var topStable = results.StableClusters
                .OrderByDescending(c => c.Lifetime)
                .Take(8);

            foreach (var c in topStable)
            {
                report.AppendLine(
                    $"  {c.Id,2} │ {c.Lifetime,8} │ {c.Size,4} │ {c.PeakDensity,12:F2} │ {c.OscillatorIndices.Count,11} │ {c.CenterOfMass,10:F1}");
            }
        }
        else
        {
            report.AppendLine("  No stable clusters detected — no structure survived the threshold.");
        }

        report.AppendLine();

        // ── 6. Energy Concentration ─────────────────────────────
        AppendSection(report, "6. Energy Concentration");

        double totalFieldEnergy = field.TotalEnergy();
        double clusterEnergy = finalClusters.Sum(c => c.TotalEnergy);
        double nonClusterEnergy = totalFieldEnergy - clusterEnergy;

        report.AppendLine($"  Total field energy        : {totalFieldEnergy:F2}");
        report.AppendLine($"  Energy in clusters        : {clusterEnergy:F2}");
        report.AppendLine($"  Energy outside clusters   : {nonClusterEnergy:F2}");
        report.AppendLine($"  Energy concentration      : {results.EnergyConcentration * 100:F2}%");
        report.AppendLine($"  Oscillator participation  : {results.OscillatorParticipation * 100:F2}%");
        report.AppendLine($"  Mean cluster localization : {results.MeanLocalization:F6} (1/size)");
        report.AppendLine();

        // ── 7. Interpretation ───────────────────────────────────
        AppendSection(report, "7. Interpretation");

        bool hasStableClusters = results.StableClusterCount > 0;
        bool hasHighEnergyConc = results.EnergyConcentration > 0.3;
        bool hasHighOscParticipation = results.OscillatorParticipation > 0.3;
        bool hasLongLivedClusters = results.MaxClusterLifetime > TotalIterations * 0.1;

        int emergenceScore = 0;
        if (hasStableClusters) emergenceScore++;
        if (hasHighEnergyConc) emergenceScore++;
        if (hasHighOscParticipation) emergenceScore++;
        if (hasLongLivedClusters) emergenceScore++;

        report.AppendLine("  Emergence Assessment:");
        report.AppendLine($"    Stable clusters exist          : {(hasStableClusters ? "YES ✓" : "no")}  ({results.StableClusterCount})");
        report.AppendLine($"    Energy concentration > 30%     : {(hasHighEnergyConc ? "YES ✓" : "no")}  ({results.EnergyConcentration * 100:F1}%)");
        report.AppendLine($"    Oscillator participation > 30% : {(hasHighOscParticipation ? "YES ✓" : "no")}  ({results.OscillatorParticipation * 100:F1}%)");
        report.AppendLine($"    Long-lived clusters > 10%      : {(hasLongLivedClusters ? "YES ✓" : "no")}  ({results.MaxClusterLifetime} iter)");
        report.AppendLine($"    ───────────────────────────────");
        report.AppendLine($"    Resonance emergence score      : {emergenceScore} / 4");
        report.AppendLine();

        string verdict = emergenceScore switch
        {
            >= 3 => "Resonance cluster formation CONFIRMED — stable structures emerge.",
            2 => "Partial resonance — some clustering present but not dominant.",
            1 => "Weak resonance — transient structures form but dissipate quickly.",
            _ => "No resonance clusters — the field remains homogeneous."
        };

        report.AppendLine($"  Verdict: {verdict}");
        report.AppendLine();

        // Physical interpretation.
        report.AppendLine("  Physical interpretation:");
        report.AppendLine();

        if (results.StableClusterCount > 0)
        {
            report.AppendLine("    The temporal field supports self-organizing resonance clusters. These");
            report.AppendLine("    structures form when oscillators with similar effective frequencies");
            report.AppendLine("    inject energy into overlapping regions, creating positive feedback:");
            report.AppendLine("    more oscillators → higher density → stronger frequency shift →");
            report.AppendLine("    frequency alignment → more coherent injection → higher density.");
            report.AppendLine();
            report.AppendLine("    This mechanism is analogous to density-wave formation in plasmas and");
            report.AppendLine("    self-trapping in nonlinear optics. The clusters represent the first");
            report.AppendLine("    candidate for proto-particle structures in the AT framework.");
        }
        else
        {
            report.AppendLine("    At N=100 and with the current parameters, the temporal field does not");
            report.AppendLine("    spontaneously break homogeneity. Oscillators inject energy but the");
            report.AppendLine("    diffusion and damping rates prevent density from accumulating into");
            report.AppendLine("    persistent structures. This suggests that either:");
            report.AppendLine("      • Larger N is needed (more oscillators → stronger feedback)");
            report.AppendLine("      • Stronger coupling α is needed");
            report.AppendLine("      • Lower damping γ is needed (energy dissipates too fast)");
            report.AppendLine("      • A nonlinear density-to-frequency coupling may be necessary");
        }

        report.AppendLine();

        // ── 8. Conclusion ───────────────────────────────────────
        AppendSection(report, "8. Conclusion");

        report.AppendLine($"  C1. {verdict}");
        report.AppendLine();
        report.AppendLine($"  C2. Over {TotalIterations} iterations, {results.TotalClustersDetected} clusters were");
        report.AppendLine($"      detected, of which {results.StableClusterCount} survived the stability threshold.");
        report.AppendLine($"      The maximum lifetime was {results.MaxClusterLifetime} iterations.");
        report.AppendLine();
        report.AppendLine($"  C3. Energy concentration: {results.EnergyConcentration * 100:F1}% of total field energy");
        report.AppendLine($"      was concentrated in clusters at the final iteration.");
        report.AppendLine($"      Oscillator participation: {results.OscillatorParticipation * 100:F1}%.");
        report.AppendLine();
        report.AppendLine("  C4. The hypothesis that matter = dynamically stabilized resonance structures");
        report.AppendLine($"      is {(hasStableClusters ? "SUPPORTED" : "NOT YET confirmed")} by this experiment.");
        report.AppendLine();
        report.AppendLine("  Next steps:");
        report.AppendLine("    • AT-006: Parameter sweep (α, γ, N) to find cluster formation thresholds.");
        report.AppendLine("    • AT-007: Cluster-cluster interactions (proto-particle scattering).");
        report.AppendLine("    • AT-008: Nonlinear frequency coupling (density saturation effects).");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment AT-005 completed successfully.");
        report.AppendLine(new string('=', 100));

        Output.WriteLine(report.ToString());
    }

    private void PrintDensityProfile(StringBuilder sb, double[] profile, int step)
    {
        sb.Append("    ");
        for (int i = 0; i < profile.Length; i += step)
        {
            double val = profile[i];
            if (val > 10) sb.Append($"{val,6:F1} ");
            else if (val > 1) sb.Append($"{val,6:F2} ");
            else sb.Append($"{val,6:F3} ");
        }
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
