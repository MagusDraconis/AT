namespace TQM.Core.Resonance;

/// <summary>
/// Aggregate results from resonance cluster detection and tracking
/// over an entire simulation run.
/// </summary>
public sealed class ResonanceClusterResult
{
    /// <summary>
    /// Total number of clusters ever detected (including transient ones).
    /// </summary>
    public int TotalClustersDetected { get; }

    /// <summary>
    /// Number of clusters that survived longer than the stability threshold.
    /// </summary>
    public int StableClusterCount { get; }

    /// <summary>
    /// Number of clusters active at the final iteration.
    /// </summary>
    public int ActiveClustersAtEnd { get; }

    /// <summary>
    /// Mean cluster size (in cells).
    /// </summary>
    public double MeanClusterSize { get; }

    /// <summary>
    /// Mean cluster lifetime (in iterations).
    /// </summary>
    public double MeanClusterLifetime { get; }

    /// <summary>
    /// Maximum observed cluster lifetime.
    /// </summary>
    public int MaxClusterLifetime { get; }

    /// <summary>
    /// Fraction of total field energy concentrated in clusters.
    /// Range [0, 1]. High values → energy is clustered.
    /// </summary>
    public double EnergyConcentration { get; }

    /// <summary>
    /// Fraction of oscillators located within any cluster region.
    /// Range [0, 1]. High values → oscillators grouped in clusters.
    /// </summary>
    public double OscillatorParticipation { get; }

    /// <summary>
    /// Mean spatial localization score (1/size) of current clusters.
    /// High values → tight, localized clusters.
    /// </summary>
    public double MeanLocalization { get; }

    /// <summary>
    /// All clusters ever detected.
    /// </summary>
    public IReadOnlyList<ResonanceCluster> AllClusters { get; }

    /// <summary>
    /// Stable clusters (lifetime ≥ threshold).
    /// </summary>
    public IReadOnlyList<ResonanceCluster> StableClusters { get; }

    /// <summary>
    /// Clusters active at the final iteration.
    /// </summary>
    public IReadOnlyList<ResonanceCluster> FinalClusters { get; }

    /// <summary>
    /// Average stability score across stable clusters.
    /// </summary>
    public double MeanStabilityScore { get; }

    public ResonanceClusterResult(
        int totalClustersDetected,
        int stableClusterCount,
        int activeClustersAtEnd,
        double meanClusterSize,
        double meanClusterLifetime,
        int maxClusterLifetime,
        double energyConcentration,
        double oscillatorParticipation,
        double meanLocalization,
        List<ResonanceCluster> allClusters,
        List<ResonanceCluster> stableClusters,
        List<ResonanceCluster> finalClusters)
    {
        TotalClustersDetected = totalClustersDetected;
        StableClusterCount = stableClusterCount;
        ActiveClustersAtEnd = activeClustersAtEnd;
        MeanClusterSize = meanClusterSize;
        MeanClusterLifetime = meanClusterLifetime;
        MaxClusterLifetime = maxClusterLifetime;
        EnergyConcentration = energyConcentration;
        OscillatorParticipation = oscillatorParticipation;
        MeanLocalization = meanLocalization;
        AllClusters = allClusters.AsReadOnly();
        StableClusters = stableClusters.AsReadOnly();
        FinalClusters = finalClusters.AsReadOnly();

        MeanStabilityScore = stableClusters.Count > 0
            ? stableClusters.Average(c => c.StabilityScore)
            : 0;
    }
}
