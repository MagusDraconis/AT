namespace AT.Core.Research;

/// <summary>
/// Data types for the self-consistency depth analysis.
/// AT-X010: Self-Consistency Principle
/// </summary>
public static class SelfConsistencyMetric
{
    public sealed record DeeperCandidate(
        string Name, string MathematicalForm,
        bool ExplainsSelfConsistency,
        bool IsMoreFundamental,
        string Verdict);

    public sealed record SelfConsistencyReport(
        List<DeeperCandidate> Candidates,
        string MinimalForm,
        bool SelfConsistencyIsFundamental,
        string WhatLiesBeneath,
        string Classification, string Verdict);
}
