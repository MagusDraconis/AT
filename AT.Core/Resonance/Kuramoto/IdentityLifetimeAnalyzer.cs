using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Measures how long path-dependent historical identities persist.
/// </summary>
public static class IdentityLifetimeAnalyzer
{
    public sealed record LifetimeResult(
        int TotalIterations, string Sequence, double FinalR, double MeanFreq);

    public static LifetimeResult Analyze(
        string sequence, double beta, double k, double lambda, int n, Random rng, int totalIter = 10000)
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

        // Apply training sequence.
        foreach (char p in sequence)
        {
            double shift = p == 'A' ? 0.4 : -0.4;
            foreach (var node in network.Nodes) node.Phase += shift;
            sim.Run(300);
        }

        // Long-term evolution.
        sim.Run(totalIter - 1500 - sequence.Length * 300);

        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
        double meanFreq = network.Nodes.Average(nd => nd.Frequency);

        return new LifetimeResult(totalIter, sequence, finalR, meanFreq);
    }
}
