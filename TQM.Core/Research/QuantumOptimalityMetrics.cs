namespace TQM.Core.Research;

/// <summary>
/// Data types for quantum optimality analysis.
/// TQM-X030: Quantum Optimality Principle
/// </summary>
public static class QuantumOptimalityMetrics
{
    public sealed record OptimalityTest(
        string Architecture, double R, double S,
        int CarrierClasses, double ComplexityDensity,
        bool BeatsQuantum, string Assessment);

    public sealed record QuantumOptimalityReport(
        List<OptimalityTest> Tests,
        bool QuantumIsLocallyOptimal,
        bool QuantumIsGloballyOptimal,
        bool AnyBeatsQuantum,
        string Classification, string Verdict);
}
