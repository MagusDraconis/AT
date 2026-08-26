namespace AT.Core.Research;

/// <summary>
/// Data types for universal reality classification.
/// AT-X016: Reality Classification Theory
/// </summary>
public static class RealityCoordinates
{
    public sealed record SystemPlacement(
        string System, string Domain,
        double R, double S,
        bool HasSpecies, bool HasEvolution,
        string Region, string RealityClass);

    public sealed record RealityClassificationReport(
        List<SystemPlacement> Systems,
        int TotalSystems, int DomainsCovered,
        string[] Regions, bool ClassificationIsUniversal,
        string PhaseDiagram,
        string Classification, string Verdict);
}
