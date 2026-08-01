using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether memory-generated resonance identities persist through perturbations.
/// </summary>
public static class IdentityPersistenceAnalyzer
{
    public sealed record IdentityResult(
        double Beta, double InitialR, double PerturbedR, double FinalR,
        double IdentityShift, bool IdentityPreserved);

    /// <summary>
    /// Creates a condensate with memory, perturbs it, and measures identity persistence.
    /// </summary>
    public static IdentityResult Analyze(
        double beta, double k, double lambda, int n, Random rng, int totalIter = 10000)
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
        int third = totalIter / 3;

        // Phase 1: formation.
        sim.Run(third);
        double initialR = SynchronizationMetrics.FromNetwork(network, third).OrderParameterR;

        // Phase 2: perturbation.
        foreach (var node in network.Nodes)
            node.Phase += (rng.NextDouble() * 2 - 1) * 1.0;
        double perturbedR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Phase 3: recovery.
        sim.Run(totalIter - third);
        double finalR = SynchronizationMetrics.FromNetwork(network, totalIter - third).OrderParameterR;

        double shift = Math.Abs(finalR - initialR);
        bool preserved = shift < 0.1;

        return new IdentityResult(beta, initialR, perturbedR, finalR, shift, preserved);
    }
}
