namespace TQM.Core.Research;

/// <summary>
/// Data types for unbounded operator space analysis.
/// TQM-X023: Unbounded Operator Space Principle
/// </summary>
public static class OperatorSpaceMetric
{
    public sealed record GenerationMethod(
        string Name, string Example,
        bool Bounded, int MaxDepth,
        bool CreatesNewFamilies, string Limitation);

    public sealed record OperatorSpaceReport(
        List<GenerationMethod> Methods,
        int TotalMethods, int UnboundedMethods,
        bool UnboundedSpaceExists,
        string BestRoute,
        string Classification, string Verdict);
}
