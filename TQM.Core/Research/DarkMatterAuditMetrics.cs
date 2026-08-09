namespace TQM.Core.Research;

/// <summary>
/// Data types for X063 Correlation Dark Matter.
/// </summary>
public static class DarkMatterAuditMetrics
{
    public enum DMStatus { ParticleDMRequired, CorrelationWeak, CorrelationSignificant, DMFullyEmergent }

    public sealed record DMTest(
        string Observation, string LCDMExplanation,
        string TQMExplanation, bool TQMExplains,
        double Confidence, string Verdict);

    public sealed record RotationCurveFit(
        string Galaxy, double VFlatObs, double VFlatTQM,
        double MOND_A0, double TQM_A0, double Agreement);

    public sealed record DMAuditReport(
        List<DMTest> Tests,
        List<RotationCurveFit> Fits,
        int ExplainedCount, DMStatus Status,
        string Derivation, string Verdict);
}
