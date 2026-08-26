namespace AT.Core.Research;

/// <summary>
/// Data types for X060b Hidden Dependency Audit.
/// </summary>
public static class HiddenDependencyMetrics
{
    public enum DependencyStatus { TrulyIndependent, WeakDependencies, StrongDependencies, MajorRedundancyFound }

    public sealed record ParameterEntry(
        string Name, string Symbol, string Origin,
        string CurrentStatus, string[] DependsOn,
        bool IsReducible);

    public sealed record DependencyLink(
        string From, string To, string Relationship,
        bool IsExact, string Derivation, bool Survives);

    public sealed record ReductionProposal(
        string Name, string Reduces,
        int OldCount, int NewCount,
        string Mechanism, bool IsRigorous);

    public sealed record DependencyReport(
        List<ParameterEntry> Parameters,
        List<DependencyLink> Links,
        List<ReductionProposal> Reductions,
        int OriginalCount, int MinimalCount,
        DependencyStatus Status, string Verdict);
}
