namespace TQM.Core.Research;

/// <summary>
/// Data types for finite complexity optimization.
/// TQM-X029: Finite Complexity Optimization Principle
/// </summary>
public static class ComplexityEfficiencyMetrics
{
    public sealed record ArchitectureScore(
        string Architecture, int CarrierClasses,
        int MaxSpecies, double ComplexityScore,
        double Efficiency, bool IsOptimal);

    public sealed record OptimizationReport(
        List<ArchitectureScore> Architectures,
        string BestArchitecture, double BestEfficiency,
        bool HybridIsOptimal,
        string Classification, string Verdict);
}
