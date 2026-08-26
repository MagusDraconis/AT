namespace AT.Core.Research;

/// <summary>
/// Data types for X058 Correlation Length Origin.
/// </summary>
public static class CorrelationLengthMetrics
{
    public enum XiStatus { Fundamental, WeaklyEmergent, PartiallyDerived, FullyDerived }

    public sealed record XiModel(
        string Name, string Mechanism,
        double PredictedLogXiOverLP, double ObservedLogXiOverLP,
        bool RequiresTuning, string Flaw, bool Survives);

    public sealed record XiScanPoint(
        double LogXiOverLP, double Stability,
        double InfoCapacity, double ComplexityCost,
        double TotalFitness);

    public sealed record XiReport(
        List<XiModel> Models,
        List<XiScanPoint> Scan,
        double OptimalLogXi, XiStatus Status,
        string Derivation, string Verdict);
}
