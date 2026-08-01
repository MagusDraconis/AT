using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Searches for multiple stable resonance energy levels by applying
/// controlled energy injections and tracking final states.
/// </summary>
public static class EnergyLevelAnalyzer
{
    public sealed record EnergyResult(
        double InjectionLevel, double Beta, double FinalR,
        double FinalEnergy, int EnergyBand);

    /// <summary>
    /// Runs one energy injection experiment with memory.
    /// </summary>
    public static EnergyResult Analyze(
        double injectionLevel, double beta, double k, double lambda, int n, Random rng, int iterations = 5000)
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

        // Form baseline.
        sim.Run(iterations / 2);

        // Inject energy: scale oscillator frequencies by injection level.
        foreach (var node in network.Nodes)
            node.Frequency *= (1.0 + injectionLevel);

        // Continue evolution.
        sim.Run(iterations / 2);

        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Energy proxy: R × mean frequency.
        double meanFreq = network.Nodes.Average(nd => nd.Frequency);
        double energy = finalR * meanFreq;

        // Band: discretize energy into bands.
        int band = (int)(energy * 10); // 0-10 bands

        return new EnergyResult(injectionLevel, beta, finalR, energy, band);
    }
}
