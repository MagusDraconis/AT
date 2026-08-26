namespace AT.Core.Research;

/// <summary>
/// Data types for finite vs infinite reality analysis.
/// AT-X027: Finite vs Infinite Reality Principle
/// </summary>
public static class InfiniteLimitMetrics
{
    public sealed record ScalingResult(
        int N, double MaxSpecies, double SaturationTime,
        bool SaturationObserved, string Regime);

    public sealed record FiniteInfiniteReport(
        List<ScalingResult> Results,
        bool AllFiniteSystemsSaturate,
        bool L6RequiresInfinite,
        string Boundary,
        string Classification, string Verdict);
}
