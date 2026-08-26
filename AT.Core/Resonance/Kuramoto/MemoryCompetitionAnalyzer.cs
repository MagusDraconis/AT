using System.Collections.Concurrent;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Measures identity decay as a function of memory strength β and time,
/// testing the competition between memory formation and attractor relaxation.
/// </summary>
public static class MemoryCompetitionAnalyzer
{
    public sealed record CompetitionResult(
        double Beta, int Iterations, string Sequence,
        double FinalR, double MeanFreq);

    public static CompetitionResult Analyze(
        double beta, int iterations, string sequence, int n, Random rng)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, 5.0, 0.05, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);

        foreach (char p in sequence)
        {
            double shift = p == 'A' ? 0.4 : -0.4;
            foreach (var node in network.Nodes) node.Phase += shift;
            sim.Run(300);
        }

        sim.Run(iterations - 1500 - sequence.Length * 300);

        return new CompetitionResult(beta, iterations, sequence,
            SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR,
            network.Nodes.Average(nd => nd.Frequency));
    }
}
