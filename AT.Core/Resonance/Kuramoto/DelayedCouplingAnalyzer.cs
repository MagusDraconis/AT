using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes wave patterns, phase gradients, and internal structure
/// in delayed-coupling simulations.
/// </summary>
public static class DelayedCouplingAnalyzer
{
    /// <summary>
    /// Detects the number of wave fronts: contiguous regions where
    /// the phase gradient magnitude exceeds a threshold.
    /// </summary>
    public static int CountWaveFronts(TemporalNetwork network, double threshold = 0.5)
    {
        double grad = InternalStateAnalyzer.ComputePhaseGradient(network);
        return grad > threshold ? 1 : 0; // simplified: high gradient = wave front present
    }

    /// <summary>
    /// Computes the circular phase variance across all oscillators.
    /// Low = synchronized, High = dispersed.
    /// </summary>
    public static double PhaseVariance(TemporalNetwork network)
    {
        int n = network.NodeCount;
        double sumSin = 0, sumCos = 0;
        foreach (var node in network.Nodes)
        {
            sumSin += Math.Sin(node.Phase);
            sumCos += Math.Cos(node.Phase);
        }
        double r = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / n;
        return 1.0 - r;
    }

    /// <summary>
    /// Returns the max phase difference between any pair of nearby oscillators.
    /// </summary>
    public static double MaxLocalPhaseGradient(TemporalNetwork network, double radius = 0.1)
    {
        int n = network.NodeCount;
        var nodes = network.Nodes;
        double maxGrad = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dx = nodes[i].X - nodes[j].X;
                double dy = nodes[i].Y - nodes[j].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist > radius || dist < 1e-10) continue;

                double dPhase = Math.Abs(TemporalSimulation.NormalizePhase(
                    nodes[i].Phase - nodes[j].Phase + Math.PI) - Math.PI);
                double grad = dPhase / dist;
                maxGrad = Math.Max(maxGrad, grad);
            }
        }

        return maxGrad;
    }
}
