namespace AT.Core.Research;

/// <summary>
/// Data types for X035 Origin of Q Principle.
/// </summary>
public static class QNecessityMetrics
{
    public enum ReductionStatus { Irreducible, PartiallyDerived, FullyDerived, Collapses }

    public sealed record QReductionAttempt(
        string CandidateOrigin, string DerivationPath,
        ReductionStatus Status, string WhatSurvives,
        string WhatCollapses, string Verdict);

    public sealed record QNecessityAudit(
        string Concept, bool SurvivesWithoutQ,
        string Impact, string Notes);

    public sealed record OriginOfQReport(
        List<QReductionAttempt> ReductionAttempts,
        List<QNecessityAudit> NecessityAudits,
        int AttemptsCount, int IrreducibleCount,
        int DerivedCount, string Classification,
        string[] WhatQReallyIs,
        string Verdict);
}
