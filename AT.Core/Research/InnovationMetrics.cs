namespace AT.Core.Research;

/// <summary>
/// Data types for open-ended evolution analysis.
/// AT-X019: Open-Ended Evolution Principle
/// </summary>
public static class InnovationMetrics
{
    public sealed record L6Requirement(
        string Requirement, bool SatisfiedInAT,
        bool IsBottleneck, string Why);

    public sealed record OpenEndedEvoReport(
        List<L6Requirement> Requirements,
        bool L6IsAchievable, string MissingIngredient,
        string FailureRootCause,
        string Classification, string Verdict);
}
