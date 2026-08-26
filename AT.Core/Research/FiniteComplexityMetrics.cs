namespace AT.Core.Research;

/// <summary>
/// Data types for finite universe consequence analysis.
/// AT-X028: Finite Universe Consequences
/// </summary>
public static class FiniteComplexityMetrics
{
    public sealed record ComplexityCeiling(
        string Domain, double EstimatedMaximum,
        string Bound, bool PracticallyReachable,
        string Assessment);

    public sealed record FiniteUniverseReport(
        List<ComplexityCeiling> Ceilings,
        int DomainsAnalyzed, bool AllDomainsHaveCeilings,
        bool CeilingsArePracticallyRelevant,
        string Classification, string Verdict);
}
