namespace TQM.Core.Research;

/// <summary>
/// Data types for reality structure principle analysis.
/// TQM-X014: Reality Structure Principle
/// </summary>
public static class RealityStructureMetrics
{
    public sealed record PersistenceQuadrant(
        string Quadrant, string Examples,
        double MeanLifetime, double MeanInfoRetention,
        bool CanFormSpecies, bool CanFormEcologies,
        bool CanEvolve, string RealityClass);

    public sealed record RealityStructureReport(
        List<PersistenceQuadrant> Quadrants,
        bool MaxPersistenceRequiresBoth,
        string RealityPrinciple,
        string Classification, string Verdict);
}
