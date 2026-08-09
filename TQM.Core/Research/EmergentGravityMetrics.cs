namespace TQM.Core.Research;

/// <summary>
/// Data types for X061 Emergent Macroscopic Gravity.
/// </summary>
public static class EmergentGravityMetrics
{
    public enum EmergenceStatus { RequiresGR, WeaklyEmergent, StronglyEmergent, MacroscopicGravityDerived }

    public sealed record GravityTest(
        string Phenomenon, string GRPrediction,
        string TQMPrediction, bool MatchesGR,
        double Deviation, string Notes);

    public sealed record EffectiveEquation(
        string Form, double Coupling,
        double FitQuality, bool RecoverEinstein,
        string Notes);

    public sealed record EmergentGravityReport(
        List<GravityTest> Tests,
        List<EffectiveEquation> Equations,
        int MatchesGR, EmergenceStatus Status,
        string Derivation, string Verdict);
}
