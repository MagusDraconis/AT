namespace AT.Core.Research;

/// <summary>
/// Data types for reality flow theory.
/// AT-X017: Reality Flow Theory
/// </summary>
public static class RealityFlowMetrics
{
    public sealed record RealityTrajectory(
        string System, string Domain,
        double R_initial, double S_initial,
        double R_final, double S_final,
        string FlowDirection, bool HasAttractor,
        string Mechanism);

    public sealed record RealityFlowReport(
        List<RealityTrajectory> Trajectories,
        bool UniversalFlowExists,
        string DominantFlow,
        string[] Attractors,
        string Classification, string Verdict);
}
