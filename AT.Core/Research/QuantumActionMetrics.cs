namespace AT.Core.Research;

/// <summary>
/// Data types for X044 Origin of the Quantum of Action.
/// </summary>
public static class QuantumActionMetrics
{
    public enum HBarStatus { Fundamental, WeaklyEmergent, PartiallyDerived, FullyDerived }

    public sealed record ActionMechanism(
        string Model, string Origin,
        bool ProducesActionDimensions, bool ConnectsToDiscreteness,
        string Formula, string Verdict, bool Survives);

    public sealed record UncertaintyDerivation(
        string Relation, string QEventOrigin,
        double MinimumProduct, string Notes);

    public sealed record ActionReport(
        List<ActionMechanism> Mechanisms,
        List<UncertaintyDerivation> UncertaintyTests,
        int Surviving, HBarStatus Status,
        string Derivation, string Verdict);
}
