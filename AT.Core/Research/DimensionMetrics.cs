namespace AT.Core.Research;

/// <summary>
/// Data types for X042 Dimension Emergence.
/// </summary>
public static class DimensionMetrics
{
    public sealed record DimensionResult(
        int SpatialDim, int TemporalDim, int TotalDim,
        double CorrelationAccuracy, double IdentityStability,
        double InformationCapacity, double ComplexityIndex,
        double GravityScore, bool SupportsStableOrbits,
        bool SupportsPropagatingWaves, string Notes);

    public sealed record DimensionReport(
        List<DimensionResult> Results,
        int BestDimension, string BestReason,
        string Classification, string Verdict);
}
