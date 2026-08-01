namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Represents a cluster of phase-synchronized oscillators detected
/// in a Kuramoto-coupled network. Distinct from field-based ResonanceCluster.
/// </summary>
public sealed class SynchronizationCluster
{
    /// <summary>
    /// Unique cluster identifier (assigned during tracking).
    /// </summary>
    public int ClusterId { get; set; }

    /// <summary>
    /// Indices of oscillators belonging to this cluster.
    /// </summary>
    public List<int> NodeIds { get; }

    /// <summary>
    /// Mean order parameter within the cluster. ≥ 0.90 indicates strong synchronization.
    /// </summary>
    public double Synchronization { get; }

    /// <summary>
    /// Iteration at which this cluster was first detected.
    /// </summary>
    public int BirthIteration { get; }

    /// <summary>
    /// Iteration at which this cluster was last seen (updated during tracking).
    /// </summary>
    public int DeathIteration { get; set; }

    /// <summary>
    /// Number of iterations the cluster persisted.
    /// </summary>
    public int Lifetime => DeathIteration - BirthIteration;

    /// <summary>
    /// Number of oscillators in the cluster.
    /// </summary>
    public int Size => NodeIds.Count;

    /// <summary>
    /// Persistence score: fraction of observation window the cluster survived.
    /// 1.0 = survived the entire simulation after birth.
    /// </summary>
    public double PersistenceScore { get; set; }

    /// <summary>
    /// Average phase of the cluster members (circular mean).
    /// </summary>
    public double AveragePhase { get; }

    /// <summary>
    /// Mean natural frequency of oscillators in this cluster (computed at detection).
    /// </summary>
    public double MeanFrequency { get; set; }

    /// <summary>
    /// Mean accumulated energy of oscillators in this cluster (computed at detection).
    /// </summary>
    public double MeanEnergy { get; set; }

    public SynchronizationCluster(
        int clusterId,
        List<int> nodeIds,
        double synchronization,
        int birthIteration,
        double averagePhase)
    {
        ClusterId = clusterId;
        NodeIds = nodeIds ?? new List<int>();
        Synchronization = synchronization;
        BirthIteration = birthIteration;
        DeathIteration = birthIteration;
        AveragePhase = averagePhase;
    }

    public override string ToString() =>
        $"SyncCluster[{ClusterId}] size={Size} R={Synchronization:F4} " +
        $"lifetime={Lifetime} birth={BirthIteration}";
}
