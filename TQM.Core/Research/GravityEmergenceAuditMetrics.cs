namespace TQM.Core.Research;

/// <summary>
/// Data types for X060h Gravity Emergence Audit.
/// </summary>
public static class GravityEmergenceAuditMetrics
{
    public enum GravityStatus { Fundamental, WeaklyEmergent, StronglyEmergent, FullyEmergent }

    public sealed record DependencyNode(
        string Concept, bool ExistsBeforeGravity,
        string[] Requires, string Notes);

    public sealed record ReconstructionPath(
        string Name, string[] Steps,
        bool RecoversGR, string WeakestLink, bool Survives);

    public sealed record GravityAuditReport(
        List<DependencyNode> Nodes,
        List<ReconstructionPath> Paths,
        int BeforeGravityCount, GravityStatus Status,
        string Verdict);
}
