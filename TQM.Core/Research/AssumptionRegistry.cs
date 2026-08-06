namespace TQM.Core.Research;

/// <summary>
/// Data types for the TQM theory audit.
/// TQM-X001: Alternative Foundations Audit
/// </summary>
public static class AssumptionRegistry
{
    public sealed record TrackedAssumption(
        string Name, string Description, string WhereUsed,
        bool WasTested, string TestResult,
        int ImportanceScore, int DependenceScore,
        int TestCoverage, int NoveltyPotential,
        string Recommendation);

    public sealed record AlternativeOperator(
        string Name, string Definition, string Properties,
        bool TQMSurvives, string WhatBreaks, string WhatSurvives);

    public sealed record AuditReport(
        List<TrackedAssumption> Assumptions,
        List<AlternativeOperator> Alternatives,
        string[] MostCriticalAssumptions,
        string[] MostPromisingDirections,
        string[] PathDependencies,
        bool FrameworkIsBiased,
        string Verdict);
}
