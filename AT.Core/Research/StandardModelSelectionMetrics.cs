namespace AT.Core.Research;

/// <summary>
/// Data types for X056 Standard Model Selection.
/// </summary>
public static class StandardModelSelectionMetrics
{
    public enum SelectionStatus { NoPreference, WeakPreference, StrongPreference, UniquelySelected }

    public sealed record GaugeCandidate(
        string Group, int Dim, int Rank,
        int SimpleFactors, bool IsAnomalyFree,
        bool SupportsConfinement, double SpeciesDiversity,
        double InteractionRichness, double Stability,
        double InfoCapacity, double StructuralCost,
        double TotalFitness, string Notes);

    public sealed record FactorRemovalTest(
        string RemovedFactor, string RemainingGroup,
        double FitnessLoss, string Consequences);

    public sealed record SelectionReport(
        List<GaugeCandidate> Candidates,
        List<FactorRemovalTest> RemovalTests,
        GaugeCandidate Best, SelectionStatus Status,
        string Verdict);
}
