namespace AT.Core.Research;

/// <summary>
/// Data types for reversibility vs self-consistency comparison.
/// AT-X011: Reversibility vs Self-Consistency
/// </summary>
public static class ReversibilityVsSelfConsistency
{
    public sealed record SystemClassification(
        string System, bool IsReversible,
        bool IsSelfConsistent, string Category);

    public sealed record FoundationComparisonReport(
        List<SystemClassification> Systems,
        int BothCount, int ReversibleOnly,
        int SelfConsistentOnly, int NeitherCount,
        bool AreIndependent, string Relationship,
        string Classification, string Verdict);
}
