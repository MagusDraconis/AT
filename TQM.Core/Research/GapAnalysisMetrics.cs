namespace TQM.Core.Research;

/// <summary>
/// Data types for completeness audit.
/// TQM-X032: Completeness Audit
/// </summary>
public static class GapAnalysisMetrics
{
    public sealed record EquivalenceEntry(
        string Concept, string MainTQMStatus,
        string ResearchXStatus, bool IsEquivalent,
        string Notes);

    public sealed record CompletenessAuditReport(
        List<EquivalenceEntry> Entries,
        int TotalConcepts, int EquivalentConcepts,
        int GapsRemaining, bool TheoriesAreUnified,
        string[] Gaps,
        string Classification, string Verdict);
}
