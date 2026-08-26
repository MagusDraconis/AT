namespace AT.Core.Research;

/// <summary>
/// Data types for X039 Origin of Quantum Randomness.
/// </summary>
public static class RandomnessMetrics
{
    public enum RandomnessStatus { Fundamental, WeakReduction, PartiallyDerived, FullyDerived }

    public sealed record SelectionMechanism(
        int Number, string Name, string DerivationAttempt,
        bool DerivesBornWeights, string FatalFlaw,
        string Status);

    public sealed record RandomnessReport(
        List<SelectionMechanism> Mechanisms,
        int Attempted, int Successful,
        RandomnessStatus Status, string Conclusion,
        string Verdict);
}
