using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether the ORDER of training experiences affects final condensate identity.
/// </summary>
public static class PathDependenceAnalyzer
{
    public sealed record PathResult(
        string Sequence, double FinalR, double MeanFreq,
        (double R, double Freq) Identity);

    private static void Apply(TemporalNetwork network, char p, Random rng)
    {
        double shift = p switch { 'A' => 0.4, 'B' => -0.4, 'C' => (rng.NextDouble() * 2 - 1) * 0.4, _ => 0 };
        foreach (var node in network.Nodes) node.Phase += shift;
    }

    public static PathResult Analyze(
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

        foreach (char p in sequence)
        {
            Apply(network, p, rng);
            sim.Run(400);
        }

        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
        double meanFreq = network.Nodes.Average(nd => nd.Frequency);

        return new PathResult(sequence, finalR, meanFreq, (finalR, meanFreq));
    }
}
