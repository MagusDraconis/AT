namespace TQM.Core.Research;

/// <summary>
/// Data types for niche construction analysis.
/// TQM-X020: Niche Construction Principle
/// </summary>
public static class NicheConstructionMetrics
{
    public sealed record FeedbackResult(
        string Mechanism, bool NewCarrierClasses,
        bool NewSpeciesClasses, bool NonSaturating,
        string Bottleneck, string Assessment);

    public sealed record NicheConstructionReport(
        List<FeedbackResult> Results,
        bool ClosesTheLoop, bool EnablesL6,
        string Classification, string Verdict);
}
