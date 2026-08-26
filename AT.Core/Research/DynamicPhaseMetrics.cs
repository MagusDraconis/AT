namespace AT.Core.Research;

/// <summary>
/// Data types for dynamic topology phase diagram.
/// AT-X003: Dynamic Topology Phase Diagram
/// </summary>
public static class DynamicPhaseMetrics
{
    public sealed record MobilityResult(
        double Mobility, int InitialSpecies, int FinalSpecies,
        double InnovationRate, double SpectralDrift,
        double GraphEntropyChange, string Phase);

    public sealed record PhaseDiagram(
        List<MobilityResult> Results,
        double CriticalMobility1, double CriticalMobility2,
        bool OpenEndedDetected, string[] Phases,
        string Classification, string Verdict);
}
