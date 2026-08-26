namespace AT.Core.Research;

/// <summary>
/// Data types for complexity emergence analysis.
/// AT-X018: Complexity Emergence Principle
/// </summary>
public static class ComplexityMetric
{
    public sealed record ComplexityLevel(
        string Level, string Requirements,
        double ComplexityScore, bool HasDiversity,
        bool HasInteractions, bool HasSelection,
        bool HasInnovation, string Examples);

    public sealed record ComplexityReport(
        List<ComplexityLevel> Levels,
        bool ComplexityIsGradual,
        string MinimalIngredients,
        string Classification, string Verdict);
}
