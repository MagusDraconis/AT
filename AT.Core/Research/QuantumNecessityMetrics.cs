namespace AT.Core.Research;

/// <summary>
/// Data types for quantum necessity analysis.
/// AT-X031: Quantum Reality Necessity Principle
/// </summary>
public static class QuantumNecessityMetrics
{
    public sealed record NecessityTest(
        double R, double S, double MaxComplexityDensity,
        bool ReachesMaximum, string Verdict);

    public sealed record QuantumNecessityReport(
        List<NecessityTest> Tests,
        bool RS1IsNecessary,
        bool QuantumRealityIsInevitable,
        bool ATAndResearchXUnified,
        string Classification, string Verdict);
}
