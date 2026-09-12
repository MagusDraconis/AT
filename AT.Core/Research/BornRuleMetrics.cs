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
        bool[] PassesForAlpha,
        string[] AlphaValues,
        RequirementEvidence Evidence,
        string Basis);

    /// <summary>
    /// How a requirement's pass/fail record was obtained (ResearchY-G_026).
    /// <see cref="Computed"/> = produced by an executed test in this codebase;
    /// <see cref="Analytic"/> = a recorded analytic result with NO executable test in the codebase.
    /// The distinction is carried so that an analytic claim can never be mistaken for a computed one,
    /// and so that a classification resting on analytic rows says so.
    /// </summary>
    public enum RequirementEvidence
    {
        Computed,
        Analytic,
    }

    public sealed record BornRuleTheorem(
        string TheoremStatement, List<AlphaTest> AlphaTests,
        List<ConsistencyRequirement> Requirements,
        int TestsCount, int SurvivingAlphas,
        string Classification, string Derivation,
        string Verdict);
}
