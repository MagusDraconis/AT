using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests how competing memory patterns interact: coexistence, interference, overwriting, fusion.
/// </summary>
public static class CompetingMemoryAnalyzer
{
    public sealed record ConflictResult(
        string Sequence, double RecallA, double RecallB, double RecallC,
        string Behavior);

    private static void ApplyPattern(TemporalNetwork network, char pattern, Random rng)
    {
        double shift = pattern switch
        {
            'A' => 0.5,
            'B' => -0.5,
            'C' => (rng.NextDouble() * 2 - 1) * 0.5,
            _ => 0
        };
        foreach (var node in network.Nodes)
            node.Phase += shift;
    }

    private static double ProbeRecall(TemporalNetwork network, char pattern, MemoryTemporalSimulation sim, Random rng)
    {
        ApplyPattern(network, pattern, rng);
        sim.Run(100);
        return SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
    }

    public static ConflictResult Analyze(
        string sequence, double beta, double k, double lambda, int n, Random rng, int baseIter = 4000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);

        // Apply sequence patterns.
        foreach (char p in sequence)
        {
            ApplyPattern(network, p, rng);
            sim.Run(300);
        }

        // Probe recall.
        var probeRng = new Random(rng.Next());
        double recallA = ProbeRecall(network, 'A', sim, probeRng);
        sim.Run(200); // settle between probes.
        double recallB = ProbeRecall(network, 'B', sim, probeRng);
        sim.Run(200);
        double recallC = ProbeRecall(network, 'C', sim, probeRng);

        // Classify behavior.
        string behavior;
        double maxRecall = Math.Max(recallA, Math.Max(recallB, recallC));
        double minRecall = Math.Min(recallA, Math.Min(recallB, recallC));

        if (Math.Abs(recallA - recallB) < 0.05 && Math.Abs(recallA - recallC) < 0.05)
            behavior = "Fusion — all patterns equally recalled";
        else if (maxRecall - minRecall > 0.1)
            behavior = "Overwrite — one pattern dominates";
        else
            behavior = "Coexistence — patterns distinguishable";

        return new ConflictResult(sequence, recallA, recallB, recallC, behavior);
    }
}
