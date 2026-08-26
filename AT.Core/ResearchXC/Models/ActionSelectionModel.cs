namespace AT.Core.ResearchXC.Models;

/// <summary>
/// Models for BDG action selection analysis (ResearchXC-007).
/// </summary>
public static class ActionSelectionModel
{
    public sealed record ActionCandidate(
        string Name, string DiscreteAction,
        string ContinuumLimit,
        bool ConvergesToEinsteinHilbert,
        double FreeParameters,
        string SelectionPrinciple,
        string Status);

    public sealed record ConstraintTally(
        string Constraint, string Effect,
        int BeforeCount, int AfterCount,
        string Eliminated);
}
