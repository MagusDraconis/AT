namespace TQM.Core.Research;

/// <summary>
/// Data types for X052 Particle Mass Hierarchies.
/// </summary>
public static class MassHierarchyMetrics
{
    public enum HierarchyStatus { MassesArbitrary, WeakHierarchy, HierarchyEmerges, FullyDerived }

    public sealed record MassModel(
        string Name, string Mechanism,
        double PredictedRatio21, double PredictedRatio31,
        double ObservedRatio21, double ObservedRatio31,
        double AccuracyLog, string Notes, bool Survives);

    public sealed record DefectEnergyLevel(
        int Level, string Label, double Energy,
        double Stability, double ObservableMass,
        string Status);

    public sealed record MassHierarchyReport(
        List<MassModel> Models,
        List<DefectEnergyLevel> Spectrum,
        int SurvivingModels, HierarchyStatus Status,
        string Derivation, string Verdict);
}
