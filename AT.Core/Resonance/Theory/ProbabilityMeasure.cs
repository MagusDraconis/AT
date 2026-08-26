namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for Born rule origin analysis.
///
/// AT-153: Origin of the Born Rule
/// </summary>
public static class ProbabilityMeasure
{
    public sealed record ProbabilityCandidate(
        string Name, string Formula,
        bool Normalized, bool Additive,
        bool BasisIndependent, bool Unique);

    public sealed record BornRuleReport(
        List<ProbabilityCandidate> Candidates,
        bool BornRuleDerived, string BestMotivation,
        string Classification, string Verdict);
}
