namespace AT.Core.Research;

/// <summary>
/// Data types for operator evolution mechanism analysis.
/// AT-X022: Operator Evolution Mechanism
/// </summary>
public static class OperatorTransitionMetrics
{
    public sealed record TransitionMechanism(
        string Name, string FromFamily, string ToFamily,
        bool IsInternal, bool IsContinuous,
        bool EnablesL6, string Limitation);

    public sealed record OperatorMechanismReport(
        List<TransitionMechanism> Mechanisms,
        bool InternalMechanismExists,
        bool BoundedOperatorSpace,
        string BestMechanism,
        string Classification, string Verdict);
}
