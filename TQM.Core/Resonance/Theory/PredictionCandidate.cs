namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for external physical prediction testing.
///
/// TQM-148: External Physical Prediction Test
/// </summary>
public static class PredictionCandidate
{
    public sealed record ExternalPrediction(
        string System, string Observable, string TQMPrediction,
        string KnownResult, bool TQMMatches, string Category);

    public sealed record ExternalPredictionReport(
        List<ExternalPrediction> Predictions,
        int TotalTests, int Passed, int Failed,
        string[] WhereTQMWorks, string[] WhereTQMFails,
        bool HasExternalPredictivePower,
        string Classification, string Verdict);
}
