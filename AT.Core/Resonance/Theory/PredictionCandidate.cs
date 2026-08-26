namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for external physical prediction testing.
///
/// AT-148: External Physical Prediction Test
/// </summary>
public static class PredictionCandidate
{
    public sealed record ExternalPrediction(
        string System, string Observable, string ATPrediction,
        string KnownResult, bool ATMatches, string Category);

    public sealed record ExternalPredictionReport(
        List<ExternalPrediction> Predictions,
        int TotalTests, int Passed, int Failed,
        string[] WhereATWorks, string[] WhereATFails,
        bool HasExternalPredictivePower,
        string Classification, string Verdict);
}
