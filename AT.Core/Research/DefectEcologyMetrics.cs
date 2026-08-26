namespace AT.Core.Research;

/// <summary>
/// Data types for X049b Defect Ecology.
/// </summary>
public static class DefectEcologyMetrics
{
    public enum EcologyStatus { NoPreference, WeakPreference, StrongPreference, UniquelySelected }

    public sealed record GaugeEcology(
        string Group, int Dimension, int Rank,
        double SpeciesDiversity, double InteractionRichness,
        double Stability, double InfoCapacity,
        double Cost, double Fitness,
        bool IsAbelian, string Notes);

    public sealed record EcologyReport(
        List<GaugeEcology> Ecologies,
        string BestEcology, double BestFitness,
        EcologyStatus Status, string Verdict);
}
