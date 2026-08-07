namespace TQM.Core.Research;

/// <summary>
/// Data types for X041b Correlation Geometry.
/// </summary>
public static class CorrelationGeometryMetrics
{
    public enum ReconstructionStatus { NoGeometry, WeakSignal, PartialEmergence, FullyReconstructed }

    public sealed record CorrelationResult(
        string GraphType, int EventCount, double DimensionEstimate,
        double ActualDimension, double DistanceCorrelation,
        double MetricReconstructionError, string Notes);

    public sealed record GeometryReconstruction(
        double[,] TrueDistances, double[,] ReconstructedDistances,
        double RankCorrelation, double MeanRelativeError,
        int EstimatedDimension, int ActualDimension,
        bool MetricRecovered, string Summary);

    public sealed record CorrelationGeometryReport(
        List<CorrelationResult> Results,
        List<GeometryReconstruction> Reconstructions,
        ReconstructionStatus Status, string Verdict);
}
