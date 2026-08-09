namespace TQM.Core.Research;

/// <summary>
/// Data types for X065 Defect Relic Abundance.
/// </summary>
public static class DefectRelicAbundanceMetrics
{
    public enum AbundanceStatus { Contingent, WeaklyConstrained, StronglyConstrained, FullyDerived }

    public sealed record AbundanceModel(
        string Name, string Mechanism,
        double PredictedOmegaDM, double ObservedOmegaDM,
        double PredictedRatio, bool Survives,
        string Verdict);

    public sealed record FreezeoutPoint(
        double Temperature, double DefectDensity,
        double AnnihilationRate, double HubbleRate,
        string Regime);

    public sealed record AbundanceReport(
        List<AbundanceModel> Models,
        List<FreezeoutPoint> Freezeout,
        int SurvivingModels, AbundanceStatus Status,
        string Verdict);
}
