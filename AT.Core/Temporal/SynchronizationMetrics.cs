namespace AT.Core.Temporal;

/// <summary>
/// Computes and stores synchronization metrics for a temporal network snapshot.
/// </summary>
public sealed class SynchronizationMetrics
{
    /// <summary>
    /// The iteration at which these metrics were captured.
    /// </summary>
    public int Iteration { get; }

    /// <summary>
    /// Kuramoto order parameter R ∈ [0, 1].
    /// R = 0 → fully incoherent. R = 1 → fully synchronized.
    /// </summary>
    public double OrderParameterR { get; }

    /// <summary>
    /// Average phase ψ (circular mean).
    /// </summary>
    public double AveragePhase { get; }

    /// <summary>
    /// Phase variance σ² (circular variance).
    /// </summary>
    public double PhaseVariance { get; }

    /// <summary>
    /// Mean energy across all nodes.
    /// </summary>
    public double MeanEnergy { get; }

    /// <summary>
    /// Total energy across all nodes.
    /// </summary>
    public double TotalEnergy { get; }

    public SynchronizationMetrics(
        int iteration,
        double orderParameterR,
        double averagePhase,
        double phaseVariance,
        double meanEnergy,
        double totalEnergy)
    {
        Iteration = iteration;
        OrderParameterR = orderParameterR;
        AveragePhase = averagePhase;
        PhaseVariance = phaseVariance;
        MeanEnergy = meanEnergy;
        TotalEnergy = totalEnergy;
    }

    /// <summary>
    /// Computes synchronization metrics from a network snapshot.
    /// </summary>
    public static SynchronizationMetrics FromNetwork(TemporalNetwork network, int iteration)
    {
        int n = network.NodeCount;
        if (n == 0)
            return new SynchronizationMetrics(iteration, 0, 0, 0, 0, 0);

        double sumSin = 0.0, sumCos = 0.0, sumEnergy = 0.0;

        foreach (var node in network.Nodes)
        {
            sumSin += Math.Sin(node.Phase);
            sumCos += Math.Cos(node.Phase);
            sumEnergy += node.Energy;
        }

        double r = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / n;
        double averagePhase = Math.Atan2(sumSin / n, sumCos / n);
        double phaseVariance = 1.0 - r;
        double meanEnergy = sumEnergy / n;

        return new SynchronizationMetrics(iteration, r, averagePhase, phaseVariance, meanEnergy, sumEnergy);
    }
}
