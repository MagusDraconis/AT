namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for blind prediction validation.
///
/// TQM-147: Predictive Physical Correspondence
/// </summary>
public static class PhysicalPrediction
{
    public sealed record BlindPrediction(
        string Geometry, string Observable, double PredictedValue,
        double KnownValue, double Error, bool WithinTolerance);

    public sealed record PredictionReport(
        List<BlindPrediction> Predictions,
        int TotalPredictions, int AccuratePredictions,
        double MeanError, int NovelPredictions,
        bool PredictsKnownPhysics, bool PredictsNewPhysics,
        string Classification, string Verdict);
}
