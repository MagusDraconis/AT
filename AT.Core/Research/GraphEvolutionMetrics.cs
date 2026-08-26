namespace AT.Core.Research;

/// <summary>
/// Data types for dynamic graph physics.
/// AT-X002: Dynamic Graph Physics
/// </summary>
public static class GraphEvolutionMetrics
{
    public sealed record DynamicState(
        int TimeStep,
        double[] Positions, double[,] Laplacian,
        double[] Eigenvalues, int UniqueSpeciesCount,
        double SpectralDrift, double GraphEntropy);

    public sealed record DynamicGraphReport(
        List<DynamicState> History,
        int InitialSpeciesCount, int FinalSpeciesCount,
        double InnovationRate, bool InnovationSaturated,
        bool OpenEndedDetected, bool SpectrumStable,
        string Classification, string Verdict);
}
