namespace AT.Core.Research;

/// <summary>
/// Data types for X060g Final Core Consistency Audit.
/// </summary>
public static class FinalCoreMetrics
{
    public enum ConsistencyStatus { CoreInsufficient, SignificantGaps, MostlyConsistent, FullySelfConsistent }

    public enum RigorLevel { Rigorous, Heuristic, GapIdentified, HiddenAssumption }

    public sealed record DerivationStep(
        int Stage, string Result, string[] Requires,
        RigorLevel Rigor, string MissingAssumption,
        string Status);

    public sealed record PrimitiveRemovalTest(
        string Removed, string[] Collapses,
        string[] Survives, string Verdict);

    public sealed record ConsistencyReport(
        List<DerivationStep> Steps,
        List<PrimitiveRemovalTest> RemovalTests,
        int TotalSteps, int RigorousSteps,
        int GapSteps, ConsistencyStatus Status,
        string Verdict);
}
