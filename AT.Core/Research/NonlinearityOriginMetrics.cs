namespace AT.Core.Research;

/// <summary>
/// Data types for X060d Origin of M².
/// </summary>
public static class NonlinearityOriginMetrics
{
    public enum M2Status { Fundamental, WeaklyConstrained, PartiallyDerived, FullyDerived }

    public sealed record M2Model(
        string Name, string Mechanism,
        double PredictedM2, double ObservedM2,
        bool EliminatesM2, string Flaw, bool Survives);

    public sealed record M2ScanPoint(
        double M2, double SolitonStability,
        double DefectDiversity, double InfoCapacity,
        double TotalFitness);

    public sealed record M2Report(
        List<M2Model> Models,
        List<M2ScanPoint> Scan,
        double OptimalM2, M2Status Status,
        string Derivation, string Verdict);
}
