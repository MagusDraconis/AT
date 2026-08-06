namespace TQM.Core.Research;

/// <summary>
/// Data types for nonlinear operator physics.
/// TQM-X005: Nonlinear Operator Physics
/// </summary>
public static class NonlinearPhaseMetrics
{
    public sealed record NonlinearResult(
        double Alpha, string Regime,
        bool EigenmodesSurvive, bool SuperpositionSurvives,
        bool HilbertSpaceSurvives, bool NewStructuresEmerge,
        int SolitonCount);

    public sealed record NonlinearReport(
        List<NonlinearResult> Results,
        int RegimeCount, bool LinearityIsEssential,
        bool NewPhysicsDetected,
        string Classification, string Verdict);
}
