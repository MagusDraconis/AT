namespace TQM.Core.Research;

/// <summary>
/// Data types for X038b Hostile Audit of Q-Conservation Collapse.
/// </summary>
public static class CollapseAuditMetrics
{
    public enum AuditVerdict { Destroyed, SeriousLoophole, MostlySurvives, FullySurvives }

    public sealed record MwDefense(
        int Number, string DefenseName,
        string ManyWorldsArgument, string TqmResponse,
        bool BreaksCollapse, string ExactFailurePoint);

    public sealed record BranchCountTheorem(
        string Setup, int QBefore, int QAfterIfBranching,
        int QAfterIfCollapse, string Conclusion);

    public sealed record CollapseAuditReport(
        List<MwDefense> Defenses, List<BranchCountTheorem> Theorems,
        int DefensesAttempted, int SuccessfulDefenses,
        AuditVerdict Verdict, string Summary);
}
