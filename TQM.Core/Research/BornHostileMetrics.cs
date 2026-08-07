namespace TQM.Core.Research;

/// <summary>
/// Data types for X037b Hostile Audit of Born Rule Derivation.
/// </summary>
public static class BornHostileMetrics
{
    public enum Verdict { Destroyed, SeriousLoophole, Survives, Strengthened }

    public sealed record AttackVector(
        string Name, string Strategy,
        bool BreaksX037, string Outcome,
        string Why);

    public sealed record AlternativeReality(
        string Name, double Alpha, string Geometry,
        string Dynamics, int ComplexityScore,
        bool InternallyConsistent, string FatalFlaw);

    public sealed record ComplexityComparison(
        string Reality, int DistinguishableStates,
        int CarrierClasses, int CompositionalDepth,
        int TotalComplexity, string Notes);

    public sealed record HostileAuditReport(
        List<AttackVector> Attacks, List<AlternativeReality> Realities,
        List<ComplexityComparison> ComplexityTable,
        int AttacksAttempted, int SuccessfulAttacks,
        Verdict FinalVerdict, string Summary,
        string StrengthenedTheorem);
}
