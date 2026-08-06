namespace TQM.Core.Research;

/// <summary>
/// Data types for the universal species principle.
/// TQM-X007: Universal Species Principle
/// </summary>
public static class SpeciesPrinciple
{
    public sealed record SpeciesCriterion(
        string Name, string Description,
        bool EigenmodesMeet, bool SolitonsMeet,
        bool IsNecessary, bool IsSufficient);

    public sealed record UniversalSpeciesReport(
        List<SpeciesCriterion> Criteria,
        string UniversalPrinciple,
        int NecessaryCount, int CommonCount,
        bool PrincipleFound,
        string Classification, string Verdict);
}
