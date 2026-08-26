namespace AT.Core.Research;

/// <summary>
/// Data types for X060f Final Primitive Audit.
/// </summary>
public static class FinalPrimitiveMetrics
{
    public enum ReductionStatus { ThreeRequired, WeakDependency, PartialReduction, PrimitiveEliminated }

    public sealed record ReductionAttempt(
        string Target, string ReducedTo,
        string Argument, bool Succeeds,
        string Why, string Verdict);

    public sealed record DependencyEdge(
        string From, string To, string Relationship,
        bool IsRigorous);

    public sealed record FinalAuditReport(
        List<ReductionAttempt> Attempts,
        List<DependencyEdge> Edges,
        int EliminatedCount, string[] IrreducibleCore,
        ReductionStatus Status, string Verdict);
}
