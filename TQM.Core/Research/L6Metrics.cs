namespace TQM.Core.Research;

/// <summary>
/// Data types for L6 simulation.
/// TQM-X025: First L6 Simulation
/// </summary>
public static class L6Metrics
{
    public sealed record L6Snapshot(
        int Generation, int OperatorFamilies,
        int CarrierClasses, int SpeciesCount,
        double InnovationRate, bool IsSaturating);

    public sealed record L6SimulationReport(
        List<L6Snapshot> History,
        int InitialFamilies, int FinalFamilies,
        int TotalGenerations, bool SaturationObserved,
        bool EvidenceForL6,
        string Classification, string Verdict);
}
