using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests how many distinct patterns a condensate can store before memory saturates.
/// </summary>
public static class MemoryCapacityAnalyzer
{
    public sealed record CapacityResult(
        int PatternCount, double FirstRecallR, double LastRecallR,
        double Drift, bool Saturated);

    /// <summary>
    /// Trains on N patterns sequentially, then tests recall.
    /// </summary>
    public static CapacityResult Analyze(
        int patternCount, double beta, double k, double lambda, int n, Random rng, int baseIter = 5000)
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
        double baselineR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Generate and apply patterns sequentially.
        var patternSeeds = Enumerable.Range(0, patternCount).Select(i => new Random(rng.Next())).ToList();
        int recoveryIter = Math.Max(30, 2000 / Math.Max(1, patternCount));

        foreach (var ps in patternSeeds)
        {
            // Apply pattern: phase perturbation specific to this pattern.
            foreach (var node in network.Nodes)
                node.Phase += (ps.NextDouble() * 2 - 1) * 0.3;
            sim.Run(recoveryIter);
        }

        // Recall test: probe with first pattern's perturbation.
        // (Re-apply the phase shift from the first pattern to see if the system 'remembers' it.)
        var firstPs = patternSeeds[0];
        foreach (var node in network.Nodes)
            node.Phase += (firstPs.NextDouble() * 2 - 1) * 0.3;

        sim.Run(500);
        double firstRecallR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Test with last pattern.
        sim.Run(200); // settle
        var lastPs = patternSeeds[^1];
        foreach (var node in network.Nodes)
            node.Phase += (lastPs.NextDouble() * 2 - 1) * 0.3;

        sim.Run(500);
        double lastRecallR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        double drift = Math.Abs(baselineR - lastRecallR);
        bool saturated = drift > 0.2;

        return new CapacityResult(patternCount, firstRecallR, lastRecallR, drift, saturated);
    }
}
