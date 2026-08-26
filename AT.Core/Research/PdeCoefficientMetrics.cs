namespace AT.Core.Research;

/// <summary>
/// Data types for X060c PDE Coefficient Audit.
/// </summary>
public static class PdeCoefficientMetrics
{
    public enum ReductionStatus { ThreeIndependent, TwoIndependent, OneIndependent, NoParameters }

    public sealed record NondimensionalResult(
        string Quantity, string Dimension,
        string ScaledBy, string DimensionlessValue);

    public sealed record ReductionStep(
        int Step, string Action, string Eliminates,
        int RemainingCount, string Notes);

    public sealed record PdeAuditReport(
        List<NondimensionalResult> Nondimensionalization,
        List<ReductionStep> Steps,
        int InitialCount, int FinalCount,
        string[] SurvivingInvariants,
        ReductionStatus Status, string Verdict);
}
