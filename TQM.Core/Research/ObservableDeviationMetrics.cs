namespace TQM.Core.Research;

/// <summary>
/// Data types for X062 Observable Deviations.
/// </summary>
public static class ObservableDeviationMetrics
{
    public enum TestabilityStatus { NoDeviation, WeakDeviation, StrongDeviation, UniqueFalsifiable }

    public sealed record DeviationSignature(
        string Name, string GRPrediction, string TQMPrediction,
        double SignalStrength, int TestabilityYears,
        string Experiment, bool IsUnique, string Notes);

    public sealed record CosmologyForecast(
        double Redshift, double H_H0_GR, double H_H0_TQM,
        double WLcdm, double WTqm, double DeltaW);

    public sealed record DeviationReport(
        List<DeviationSignature> Signatures,
        List<CosmologyForecast> Forecast,
        DeviationSignature BestTest, TestabilityStatus Status,
        string Verdict);
}
