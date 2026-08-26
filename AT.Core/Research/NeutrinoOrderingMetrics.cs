namespace AT.Core.Research;

/// <summary>
/// Data types for X060 Neutrino Ordering.
/// </summary>
public static class NeutrinoOrderingMetrics
{
    public enum OrderingStatus { NoPreference, WeakPreference, StrongPreference, OrderingDerived }

    public sealed record OrderingModel(
        string Name, string Mechanism,
        string PredictedOrdering, double PredictedDm21,
        double PredictedDm31, string Notes, bool Survives);

    public sealed record OscillationData(
        string Parameter, double PredictedValue,
        double ObservedValue, double ObservedError,
        bool WithinErrors);

    public sealed record OrderingReport(
        List<OrderingModel> Models,
        List<OscillationData> OscData,
        OrderingStatus Status, string Derivation,
        string Verdict);
}
