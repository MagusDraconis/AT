namespace AT.Core.Resonance;

/// <summary>
/// Represents a detected resonance cluster — a contiguous region of elevated
/// temporal field density where multiple oscillators may self-organize.
/// </summary>
public sealed class ResonanceCluster
{
    /// <summary>
    /// Unique cluster identifier (assigned during tracking).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Leftmost cell index of the cluster.
    /// </summary>
    public int StartCell { get; }

    /// <summary>
    /// Rightmost cell index of the cluster.
    /// </summary>
    public int EndCell { get; }

    /// <summary>
    /// Center cell index (weighted by density).
    /// </summary>
    public double CenterOfMass { get; set; }

    /// <summary>
    /// Number of cells in the cluster.
    /// </summary>
    public int Size { get; }

    /// <summary>
    /// Total accumulated energy within the cluster region.
    /// </summary>
    public double TotalEnergy { get; }

    /// <summary>
    /// Maximum density value within the cluster.
    /// </summary>
    public double PeakDensity { get; }

    /// <summary>
    /// Iteration at which this cluster was first detected.
    /// </summary>
    public int FirstSeenAt { get; }

    /// <summary>
    /// Iteration at which this cluster was last detected (updated during tracking).
    /// </summary>
    public int LastSeenAt { get; set; }

    /// <summary>
    /// Number of iterations the cluster persisted.
    /// </summary>
    public int Lifetime => LastSeenAt - FirstSeenAt + 1;

    /// <summary>
    /// Stability score: lifetime normalized by observation window.
    /// Higher values indicate more persistent structures.
    /// </summary>
    public double StabilityScore { get; set; }

    /// <summary>
    /// Indices of oscillators positioned within this cluster region.
    /// </summary>
    public List<int> OscillatorIndices { get; }

    /// <summary>
    /// Spatial localization: inverse of cluster size relative to field.
    /// High values → tightly localized. Low values → diffuse.
    /// </summary>
    public double SpatialLocalization { get; set; }

    public ResonanceCluster(
        int id,
        int startCell,
        int endCell,
        double centerOfMass,
        double totalEnergy,
        double peakDensity,
        int firstSeenAt,
        List<int> oscillatorIndices)
    {
        Id = id;
        StartCell = startCell;
        EndCell = endCell;
        CenterOfMass = centerOfMass;
        Size = endCell - startCell + 1;
        TotalEnergy = totalEnergy;
        PeakDensity = peakDensity;
        FirstSeenAt = firstSeenAt;
        LastSeenAt = firstSeenAt;
        OscillatorIndices = oscillatorIndices ?? new List<int>();
    }

    public override string ToString() =>
        $"Cluster[{Id}] cells=[{StartCell}..{EndCell}] size={Size} E={TotalEnergy:F2} " +
        $"peak={PeakDensity:F2} lifetime={Lifetime} osc={OscillatorIndices.Count}";
}
