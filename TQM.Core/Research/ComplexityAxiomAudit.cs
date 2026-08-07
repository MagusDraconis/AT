namespace TQM.Core.Research;

/// <summary>
/// Data types for X036 Complexity-to-Quantum Theorem.
/// </summary>
public static class ComplexityAxiomAudit
{
    public enum ProofStepStatus { Proven, GapIdentified, Assumed, CounterexampleFound }

    public sealed record ProofStep(
        int Number, string Step, string Derivation,
        ProofStepStatus Status, string[] UsesAxioms,
        string GapOrNote);

    public sealed record CounterexampleAttempt(
        string System, string Description,
        bool Survives, string WhyItFails);

    public sealed record ComplexityQuantumTheorem(
        string TheoremStatement, List<ProofStep> Proof,
        List<CounterexampleAttempt> Counterexamples,
        int StepsCount, int ProvenCount, int GapCount,
        string Classification, string Verdict);
}
