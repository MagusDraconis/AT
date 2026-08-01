namespace TQM.Core.TemporalField;

/// <summary>
/// Immutable snapshot of the temporal field state at a given iteration,
/// capturing aggregate metrics and the full density profile.
/// </summary>
public sealed class TemporalFieldSnapshot
{
    public int Iteration { get; }

    /// <summary>
    /// Total energy summed across all field cells.
    /// </summary>
    public double TotalEnergy { get; }

    /// <summary>
    /// Mean density averaged across all cells.
    /// </summary>
    public double MeanDensity { get; }

    /// <summary>
    /// Highest density value in the field.
    /// </summary>
    public double PeakDensity { get; }

    /// <summary>
    /// Cell index where peak density occurs.
    /// </summary>
    public int PeakCellIndex { get; }

    /// <summary>
    /// Full density profile across all cells (for visualization/analysis).
    /// </summary>
    public double[] DensityProfile { get; }

    public TemporalFieldSnapshot(
        int iteration,
        double totalEnergy,
        double meanDensity,
        double peakDensity,
        int peakCellIndex,
        double[] densityProfile)
    {
        Iteration = iteration;
        TotalEnergy = totalEnergy;
        MeanDensity = meanDensity;
        PeakDensity = peakDensity;
        PeakCellIndex = peakCellIndex;
        DensityProfile = (double[])densityProfile.Clone();
    }

    public override string ToString() =>
        $"Iter={Iteration} E_total={TotalEnergy:F4} ρ_mean={MeanDensity:F6} ρ_peak={PeakDensity:F6} @ cell {PeakCellIndex}";
}
