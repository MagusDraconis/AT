namespace TQM.Core.Research;

/// <summary>
/// Data types for information preservation depth analysis.
/// TQM-X013: Information Preservation Principle
/// </summary>
public static class InformationPreservationMetric
{
    public sealed record RetentionProfile(
        string Structure, double ReversibilityScore,
        double SelfConsistencyScore, double InfoRetention,
        double InfoLifetime, string Depth);

    public sealed record PreservationReport(
        List<RetentionProfile> Profiles,
        double ReversibilityCorrelation, double SelfConsistencyCorrelation,
        bool InfoPreservationIsCause, bool InfoPreservationIsConsequence,
        string DeepestInvariant,
        string Classification, string Verdict);
}
