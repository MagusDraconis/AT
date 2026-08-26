namespace AT.Core.Research;

/// <summary>
/// Data types for meta-operator evolution analysis.
/// AT-X024: Meta-Operator Evolution Principle
/// </summary>
public static class OperatorLineage
{
    public sealed record OperatorGeneration(
        int Level, string Operator, string CarrierClass,
        bool IsNewFamily, int SpeciesCount);

    public sealed record MetaOperatorReport(
        List<OperatorGeneration> Tower,
        int MaxDepth, bool GeneratesNewFamilies,
        bool IsUnbounded, bool FirstL6Mechanism,
        string Classification, string Verdict);
}
