namespace AT.Core.Research;

/// <summary>
/// Data types for X045 Unified Origin of c, G, and ħ.
/// </summary>
public static class FundamentalScaleMetrics
{
    public enum UnificationStatus { Independent, WeakUnification, PartialUnification, FullyUnified }

    public sealed record ConstantDerivation(
        string Constant, string Symbol, string Units,
        string QEventExpression, string Status, string Notes);

    public sealed record PlanckReconstruction(
        string Unit, string Formula,
        string ReducesTo, string Notes);

    public sealed record UnificationReport(
        List<ConstantDerivation> Derivations,
        List<PlanckReconstruction> PlanckUnits,
        int IndependentCount, int DerivedCount,
        UnificationStatus Status, string Verdict);
}
