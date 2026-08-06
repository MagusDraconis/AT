namespace TQM.Core.Research;

/// <summary>
/// Data types for graph growth physics.
/// TQM-X004: Graph Growth Physics
/// </summary>
public static class GraphGrowthMetrics
{
    public sealed record GrowthState(
        int TimeStep, int NodeCount, int SpeciesCount,
        double SpectralEntropy, double GraphEntropy);

    public sealed record GrowthReport(
        List<GrowthState> History,
        int InitialNodes, int FinalNodes,
        int InitialSpecies, int FinalSpecies,
        bool SpeciesCountGrows, bool InnovationOpenEnded,
        string Classification, string Verdict);
}
