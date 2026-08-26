namespace AT.Core.Research;

/// <summary>
/// Data types for X043 Origin of Newton's Constant.
/// </summary>
public static class NewtonConstantMetrics
{
    public enum GStatus { Fundamental, WeaklyEmergent, PartiallyDerived, FullyDerived }

    public sealed record GCandidate(
        string Model, string Origin,
        bool ProducesLengthDimension, bool ProducesCorrectCoupling,
        string Formula, string FatalFlaw, bool Survives);

    public sealed record ScalingTest(
        string Parameter, double Value, double EffectOnG,
        string Scaling, string Implication);

    public sealed record GReport(
        List<GCandidate> Candidates, List<ScalingTest> ScalingTests,
        int Surviving, GStatus Status, string Derivation,
        string Verdict);
}
