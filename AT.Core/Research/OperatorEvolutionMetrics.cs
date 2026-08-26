namespace AT.Core.Research;

/// <summary>
/// Data types for operator evolution analysis.
/// AT-X021: Operator Evolution Principle
/// </summary>
public static class OperatorEvolutionMetrics
{
    public sealed record OperatorFamily(
        string Name, string Operator,
        string CarrierClass, int SpeciesCapacity,
        bool IsReachableFromLQ);

    public sealed record OperatorEvolutionReport(
        List<OperatorFamily> Families,
        int TotalFamilies, int ReachableFamilies,
        bool OperatorSpaceIsBounded,
        bool OperatorEvolutionNecessary,
        string MissingMechanism,
        string Classification, string Verdict);
}
