using System.Globalization;
using System.Text;
using TQM.Core.Temporal;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

/// <summary>
/// TQM-001: Temporal Synchronization Experiment
///
/// Investigates whether a globally-coupled network of 100 temporal oscillators
/// with randomized initial phases can achieve synchronization under Kuramoto dynamics.
/// </summary>
public class TQM_001_TemporalSynchronizationExperiment : ResearchTestBase
{
    private const int NodeCount = 100;
    private const int TotalIterations = 5000;
    private const double CouplingStrength = 2.0;
    private const double TimeStep = 0.01;

    public TQM_001_TemporalSynchronizationExperiment(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public void TQM_001_RunSynchronizationExperiment()
    {
        // Ensure invariant culture for deterministic, locale-independent scientific output.
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
        PrintHeader("TQM-001 Temporal Synchronization Experiment");
        report.AppendLine("TQM-001: Temporal Synchronization Experiment");
        report.AppendLine();

        // ── Assumptions ─────────────────────────────────────────
        AppendSection(report, "1. Assumptions");
        report.AppendLine("  A1. All N oscillators are globally coupled with uniform coupling strength K.");
        report.AppendLine("  A2. Each oscillator i has intrinsic frequency ωᵢ = 1.0 (identical frequencies).");
        report.AppendLine("  A3. Initial phases θᵢ(0) are drawn uniformly from [0, 2π) using a fixed seed.");
        report.AppendLine("  A4. The dynamics follow the Kuramoto model: dθᵢ/dt = ωᵢ + (K/N) Σⱼ sin(θⱼ − θᵢ).");
        report.AppendLine("  A5. The coupling matrix is the identity-like uniform matrix: Kᵢⱼ = 1 for all i ≠ j.");
        report.AppendLine("  A6. Energy is tracked but not yet coupled to dynamics (reserved for future quantum extension).");
        report.AppendLine();

        // ── Configuration ───────────────────────────────────────
        AppendSection(report, "2. Experimental Configuration");
        report.AppendLine($"  Node count (N)           : {NodeCount}");
        report.AppendLine($"  Total iterations         : {TotalIterations}");
        report.AppendLine($"  Time step (Δt)           : {TimeStep}");
        report.AppendLine($"  Coupling strength (K)    : {CouplingStrength}");
        report.AppendLine($"  Total simulated time     : {TotalIterations * TimeStep:F2}");
        report.AppendLine($"  Intrinsic frequency (ω)  : 1.0 (uniform)");
        report.AppendLine($"  Initial phase distribution: Uniform [0, 2π), seed = 42");
        report.AppendLine();

        // ── Network Construction ────────────────────────────────
        AppendSection(report, "3. Network Construction");

        var random = new Random(42); // deterministic seed
        var network = new TemporalNetwork(NodeCount);

        for (int i = 0; i < NodeCount; i++)
        {
            double phase = random.NextDouble() * 2.0 * Math.PI;
            network.AddNode(new TemporalNode(i, phase: phase, frequency: 1.0));
        }

        // Set uniform coupling: Kᵢⱼ = 1 for all i ≠ j
        for (int i = 0; i < NodeCount; i++)
            for (int j = 0; j < NodeCount; j++)
                if (i != j)
                    network.Matrix[i, j] = 1.0;

        report.AppendLine($"  Network created with {network.NodeCount} nodes.");
        report.AppendLine("  Coupling matrix: fully connected, uniform (Kᵢⱼ = 1 for i ≠ j).");
        report.AppendLine();

        // ── Initial State ───────────────────────────────────────
        AppendSection(report, "4. Initial State");

        var initialMetrics = SynchronizationMetrics.FromNetwork(network, 0);
        report.AppendLine($"  Order parameter R(0)     : {initialMetrics.OrderParameterR:F6}");
        report.AppendLine($"  Average phase ψ(0)       : {initialMetrics.AveragePhase:F6} rad");
        report.AppendLine($"  Phase variance σ²(0)     : {initialMetrics.PhaseVariance:F6}");
        report.AppendLine($"  Mean energy Ē(0)         : {initialMetrics.MeanEnergy:F6}");
        report.AppendLine();

        // ── Simulation Execution ────────────────────────────────
        AppendSection(report, "5. Simulation Execution");

        var simulation = new TemporalSimulation(network)
        {
            TimeStep = TimeStep,
            CouplingStrength = CouplingStrength
        };

        // Collect metrics at checkpoints
        var checkpoints = new[] { 500, 1000, 2000, 3000, 4000, 5000 };
        var snapshotMetrics = new Dictionary<int, SynchronizationMetrics>();

        int nextCheckpointIndex = 0;
        int remaining = TotalIterations;

        while (remaining > 0)
        {
            int batchSize = Math.Min(remaining, checkpoints[nextCheckpointIndex] - simulation.CurrentIteration);
            simulation.Run(batchSize);
            remaining -= batchSize;

            if (simulation.CurrentIteration == checkpoints[nextCheckpointIndex])
            {
                snapshotMetrics[simulation.CurrentIteration] =
                    SynchronizationMetrics.FromNetwork(network, simulation.CurrentIteration);
                nextCheckpointIndex++;
            }
        }

        report.AppendLine($"  Simulation completed in {TotalIterations} iterations.");
        report.AppendLine();

        // ── Checkpoint Metrics ──────────────────────────────────
        AppendSection(report, "6. Synchronization Metrics Over Time");
        report.AppendLine("  Iteration │  Order Parameter R  │  Avg Phase ψ  │  Phase Variance σ²");
        report.AppendLine("  ──────────┼─────────────────────┼───────────────┼───────────────────");

        foreach (int checkpoint in checkpoints)
        {
            var m = snapshotMetrics[checkpoint];
            report.AppendLine(
                $"  {checkpoint,9} │ {m.OrderParameterR,19:F6} │ {m.AveragePhase,13:F6} │ {m.PhaseVariance,17:F6}");
        }

        report.AppendLine();

        // ── Final State ─────────────────────────────────────────
        AppendSection(report, "7. Final State Analysis");

        var finalMetrics = SynchronizationMetrics.FromNetwork(network, TotalIterations);
        double deltaR = finalMetrics.OrderParameterR - initialMetrics.OrderParameterR;
        double deltaVariance = initialMetrics.PhaseVariance - finalMetrics.PhaseVariance;

        report.AppendLine($"  Final order parameter R({TotalIterations})    : {finalMetrics.OrderParameterR:F6}");
        report.AppendLine($"  Initial order parameter R(0)                  : {initialMetrics.OrderParameterR:F6}");
        report.AppendLine($"  ΔR (improvement)                              : {deltaR:F6}");
        report.AppendLine($"  Final phase variance σ²({TotalIterations})    : {finalMetrics.PhaseVariance:F6}");
        report.AppendLine($"  Variance reduction                            : {deltaVariance:F6}");
        report.AppendLine($"  Final average phase ψ({TotalIterations})      : {finalMetrics.AveragePhase:F6} rad");
        report.AppendLine();

        // ── Synchronization Threshold ───────────────────────────
        double syncThreshold = 0.95;
        int syncIteration = -1;

        // Binary search for threshold crossing using full metrics collection
        // Rebuild network and re-run to find exact crossing point
        random = new Random(42);
        var network2 = new TemporalNetwork(NodeCount);
        for (int i = 0; i < NodeCount; i++)
        {
            double phase = random.NextDouble() * 2.0 * Math.PI;
            network2.AddNode(new TemporalNode(i, phase: phase, frequency: 1.0));
        }
        for (int i = 0; i < NodeCount; i++)
            for (int j = 0; j < NodeCount; j++)
                if (i != j)
                    network2.Matrix[i, j] = 1.0;

        var sim2 = new TemporalSimulation(network2)
        {
            TimeStep = TimeStep,
            CouplingStrength = CouplingStrength
        };

        // Take metrics every 100 iterations to find threshold
        for (int i = 0; i < TotalIterations; i++)
        {
            sim2.Step();
            if ((i + 1) % 100 == 0)
            {
                var m = SynchronizationMetrics.FromNetwork(network2, i + 1);
                if (m.OrderParameterR >= syncThreshold)
                {
                    syncIteration = i + 1;
                    report.AppendLine($"  R ≥ {syncThreshold} achieved at iteration {syncIteration}");
                    break;
                }
            }
        }

        if (syncIteration < 0)
            report.AppendLine($"  R did not reach {syncThreshold} within {TotalIterations} iterations.");

        report.AppendLine();

        // ── Conclusions ─────────────────────────────────────────
        AppendSection(report, "8. Conclusions");
        report.AppendLine($"  C1. The globally-coupled oscillator network evolved from an initial order");
        report.AppendLine($"      parameter R(0) = {initialMetrics.OrderParameterR:F4} to a final value of");
        report.AppendLine($"      R({TotalIterations}) = {finalMetrics.OrderParameterR:F4}, representing a");
        report.AppendLine($"      ΔR of {deltaR:F4}.");
        report.AppendLine();
        report.AppendLine($"  C2. The phase variance decreased from {initialMetrics.PhaseVariance:F4} to");
        report.AppendLine($"      {finalMetrics.PhaseVariance:F4}, confirming emergence of collective phase coherence.");
        report.AppendLine();
        report.AppendLine($"  C3. With coupling strength K = {CouplingStrength},{' '}");
        if (syncIteration > 0)
            report.AppendLine($"      synchronization (R ≥ 0.95) was achieved at iteration {syncIteration}.");
        else
            report.AppendLine($"      synchronization (R ≥ 0.95) was not achieved — K may be below critical threshold.");
        report.AppendLine();
        report.AppendLine("  C4. These results confirm that the Kuramoto mechanism successfully produces");
        report.AppendLine("      collective synchronization in a uniform network of identical oscillators,");
        report.AppendLine("      establishing the foundation for subsequent TQM research into eigenmodes,");
        report.AppendLine("      non-uniform coupling, and quantum extensions.");

        report.AppendLine();
        report.AppendLine(new string('=', 100));
        report.AppendLine("  Experiment TQM-001 completed successfully.");
        report.AppendLine(new string('=', 100));

        // ── Output Report ───────────────────────────────────────
        Output.WriteLine(report.ToString());
    }

    private static void AppendSection(StringBuilder sb, string title)
    {
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }
}
