namespace AT.Core.Research;

/// <summary>
/// Data types for minimal reality principle analysis.
/// AT-X015: Minimal Reality Principle
/// </summary>
public static class RealityScore
{
    /// <summary>
    /// Reality Score: composite metric for how much "persistent reality" a foundation set produces.
    /// 0-10 scale: persistence(3) + identity(2) + info(2) + species(2) + evolution(1).
    /// </summary>
    public sealed record FoundationTest(
        string Foundations,
        bool HasR, bool HasS, bool HasT, bool HasN,
        double Persistence, double Identity,
        double InfoRetention, double SpeciesFormation,
        double EvolutionaryCapacity,
        double RealityScore,
        string Assessment);

    public sealed record MinimalRealityReport(
        List<FoundationTest> Tests,
        string MinimalSet, double MinimalScore,
        int CombinationsTested, bool RSIsMinimal,
        string Classification, string Verdict);
}
