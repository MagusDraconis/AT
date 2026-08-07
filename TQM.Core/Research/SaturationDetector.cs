namespace TQM.Core.Research;

/// <summary>
/// Data types for asymptotic L6 verification.
/// TQM-X026: Asymptotic L6 Verification
/// </summary>
public static class SaturationDetector
{
    public sealed record GrowthFit(
        string Model, double R2, double Asymptote,
        bool PredictsSaturation, string Verdict);

    public sealed record AsymptoticL6Report(
        List<L6Metrics.L6Snapshot> LongHistory,
        List<GrowthFit> Fits,
        int MaxGenerations, bool SaturationDetected,
        string BestModel, bool X025WasFalse,
        string Classification, string Verdict);
}
