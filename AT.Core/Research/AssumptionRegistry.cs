namespace AT.Core.Research;

/// <summary>
/// Data types for the AT theory audit.
/// AT-X001: Alternative Foundations Audit
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
        bool ATSurvives, string WhatBreaks, string WhatSurvives);

    public sealed record AuditReport(
        List<TrackedAssumption> Assumptions,
        List<AlternativeOperator> Alternatives,
        string[] MostCriticalAssumptions,
        string[] MostPromisingDirections,
        string[] PathDependencies,
        bool FrameworkIsBiased,
        string Verdict);
}
