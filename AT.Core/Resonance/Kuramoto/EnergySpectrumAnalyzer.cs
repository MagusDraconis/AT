using System.Collections.Concurrent;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Maps the full resonance energy spectrum to determine whether it is
/// continuous, clustered, or possesses discrete band structure.
/// </summary>
public static class EnergySpectrumAnalyzer
{
    public sealed record SpectrumPoint(
        double InjectionLevel, double Beta,
        double FinalR, double FinalEnergy);

    /// <summary>
    /// Runs one spectrum measurement with memory and energy injection.
    /// </summary>
    public static SpectrumPoint? Measure(
        double injection, double beta, double k, double lambda, int n, Random rng, int iterations = 3000)
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
        sim.Run(iterations / 2);

        // Inject energy.
        foreach (var node in network.Nodes)
            node.Frequency *= (1.0 + injection);

        sim.Run(iterations / 2);

        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
        double energy = finalR * network.Nodes.Average(nd => nd.Frequency);

        return new SpectrumPoint(injection, beta, finalR, energy);
    }
}
