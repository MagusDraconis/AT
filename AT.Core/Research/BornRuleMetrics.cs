namespace AT.Core.Research;

/// <summary>
/// Data types for X037 Born Rule Derivation.
/// </summary>
public static class BornRuleMetrics
{
    public enum FailureMode
    {
        None,
        BasisDependence,
        CompositionFailure,
        EntanglementIncompatibility,
        AdditivityFailure,
        ComplexityLoss,
        HilbertGeometryViolation,
        NonlinearityInduced
    }

    public sealed record AlphaTest(
        double Alpha, string SystemDescription,
        bool Survives, FailureMode Failure,
        string ExactFailurePoint, string MathematicalReason);

    public sealed record ConsistencyRequirement(
        string Name, string Statement,
        bool PassesForAlpha2, bool[] PassesForOtherAlphas,
        string[] AlphaValues);

    public sealed record BornRuleTheorem(
        string TheoremStatement, List<AlphaTest> AlphaTests,
        List<ConsistencyRequirement> Requirements,
        int TestsCount, int SurvivingAlphas,
        string Classification, string Derivation,
        string Verdict);
}
