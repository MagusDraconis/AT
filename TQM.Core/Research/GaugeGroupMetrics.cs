namespace TQM.Core.Research;

/// <summary>
/// Data types for X049 Gauge Group Selection.
/// </summary>
public static class GaugeGroupMetrics
{
    public enum SelectionStatus { NoPreference, WeakPreference, StrongPreference, UniquelySelected }

    public sealed record GaugeGroupCandidate(
        string Group, int Dimension, int Rank,
        bool IsAnomalyFree, string DefectOrigin,
        double ComplexityScore, string Notes);

    public sealed record SelectionReport(
        List<GaugeGroupCandidate> Candidates,
        string BestGroup, SelectionStatus Status,
        string Derivation, string Verdict);
}
