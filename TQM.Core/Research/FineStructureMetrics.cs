namespace TQM.Core.Research;

/// <summary>
/// Data types for X055 Fine-Structure Constant.
/// </summary>
public static class FineStructureMetrics
{
    public enum AlphaStatus { Fundamental, WeakPreference, PartialEmergence, FullyDerived }

    public sealed record AlphaModel(
        string Name, string Origin,
        double PredictedAlpha, double LogError,
        string Mechanism, bool Survives);

    public sealed record AlphaScanPoint(
        double Alpha, double BoundStateEnergy,
        double InteractionRange, double DefectStability,
        double InfoCapacity, double Fitness);

    public sealed record AlphaReport(
        List<AlphaModel> Models,
        List<AlphaScanPoint> Scan,
        double OptimalAlpha, AlphaStatus Status,
        string Derivation, string Verdict);
}
