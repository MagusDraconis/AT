namespace AT.Core.Research;

/// <summary>
/// Data types for X040 Emergence of Time.
/// </summary>
public static class TimeEmergenceMetrics
{
    public enum TimeStatus { Fundamental, WeakEmergence, PartialEmergence, FullyDerived }

    public sealed record TimeMechanism(
        int Number, string Name, string Construction,
        bool GeneratesOrdering, bool GeneratesMetric,
        bool GeneratesArrow, string Gap, bool Survives);

    public sealed record QEventModel(
        string Description, int QBefore, int QAfter,
        bool HasLogicalDependence, string Ordering);

    public sealed record TimeEmergenceReport(
        List<TimeMechanism> Mechanisms,
        List<QEventModel> EventModels,
        int Attempted, int Successful,
        TimeStatus Status, string Derivation,
        string Verdict);
}
