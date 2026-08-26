namespace AT.Core.Research;

/// <summary>
/// Data types for X057 Absolute Mass Scales.
/// </summary>
public static class AbsoluteMassScaleMetrics
{
    public enum MassScaleStatus { Fundamental, WeaklyEmergent, PartiallyDerived, FullyDerived }

    public sealed record MassScaleModel(
        string Name, string Mechanism,
        double PredictedElectronMassMeV, double Log10Error,
        bool RequiresNewParameter, string Verdict);

    public sealed record MassScaleReport(
        List<MassScaleModel> Models,
        double ObservedElectronMass, double BestPrediction,
        MassScaleStatus Status, string Derivation, string FinalVerdict);
}
