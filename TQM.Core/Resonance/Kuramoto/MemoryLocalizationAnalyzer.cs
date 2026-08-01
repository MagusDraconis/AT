using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests where resonance memory is stored by selectively deleting
/// portions of the memory matrix and measuring identity persistence.
/// </summary>
public static class MemoryLocalizationAnalyzer
{
    public sealed record LocalizationResult(
        string DeletionType, double DeletionFraction,
        double FinalR, double IdentityShift, bool IdentityPreserved);

    /// <summary>
    /// Forms a condensate with memory, selectively deletes memory, and measures recovery.
    /// </summary>
    public static LocalizationResult Analyze(
        string deletionType, double deletionFrac,
        double beta, double k, double lambda, int n, Random rng, int iterations = 4000)
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
        int halfIter = iterations / 2;

        // Form identity.
        sim.Run(halfIter);
        double initialR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Delete memory: set M[i,j] = 0 for selected nodes.
        int toDelete = (int)(n * deletionFrac);
        var deleteSet = new HashSet<int>();

        switch (deletionType)
        {
            case "Single":
                deleteSet.Add(rng.Next(n)); break;
            case "Random":
                while (deleteSet.Count < toDelete) deleteSet.Add(rng.Next(n)); break;
            case "HighConnectivity":
                // Select nodes with most neighbors within λ.
                var degrees = Enumerable.Range(0, n).Select(i =>
                {
                    int deg = 0; var nds = network.Nodes;
                    for (int j = 0; j < n; j++)
                    { if (i == j) continue; double dx = nds[i].X - nds[j].X, dy = nds[i].Y - nds[j].Y; if (Math.Sqrt(dx * dx + dy * dy) <= lambda) deg++; }
                    return (i, deg);
                }).OrderByDescending(d => d.deg).Take(toDelete).Select(d => d.i);
                foreach (int i in degrees) deleteSet.Add(i);
                break;
            case "Cluster":
                // Delete a spatial cluster of nearby oscillators.
                int seed = rng.Next(n);
                double sx = network.Nodes[seed].X, sy = network.Nodes[seed].Y;
                for (int i = 0; i < n; i++)
                { double dx = network.Nodes[i].X - sx, dy = network.Nodes[i].Y - sy; if (Math.Sqrt(dx * dx + dy * dy) <= lambda * 2) deleteSet.Add(i); }
                break;
        }

        // Apply deletion: zero out memory for deleted nodes.
        foreach (int i in deleteSet)
            for (int j = 0; j < n; j++)
                sim.ZeroMemory(i, j);

        // Continue evolution.
        sim.Run(iterations - halfIter);
        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
        double shift = Math.Abs(finalR - initialR);

        return new LocalizationResult(deletionType, deletionFrac, finalR, shift, shift < 0.1);
    }
}
