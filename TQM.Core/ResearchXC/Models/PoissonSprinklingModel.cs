namespace TQM.Core.ResearchXC.Models;

/// <summary>
/// Models for Poisson Sprinkling Derivation (ResearchXC-008).
/// </summary>
public static class PoissonSprinklingModel
{
    /// <summary>Each requirement for a Poisson process.</summary>
    public sealed record PoissonRequirement(
        string Requirement, string MathematicalForm,
        string TqmStatus, bool IsSatisfied,
        string ProofStatus);

    /// <summary>A statistical test of Q-event distribution.</summary>
    public sealed record StatisticalTest(
        string Name, string Method,
        double ExpectedValue, double MeasuredValue,
        double Deviation, bool Passes,
        string Interpretation);

    /// <summary>Correlation decay analysis.</summary>
    public sealed record CorrelationDecay(
        string Scale, double Range,
        string DecayLaw, double CorrelationStrength,
        bool IsNegligible, string Verdict);

    /// <summary>A convergence condition for Poisson limit.</summary>
    public sealed record ConvergenceCondition(
        string Condition, string Formula,
        bool IsSatisfied, string Evidence,
        string Gap);

    /// <summary>The complete sprinkling derivation assessment.</summary>
    public sealed record SprinklingAssessment(
        string Title,
        List<PoissonRequirement> Requirements,
        List<StatisticalTest> Tests,
        List<CorrelationDecay> Correlations,
        List<ConvergenceCondition> Conditions,
        int RequirementsSatisfied, int TotalRequirements,
        double SprinklingConfidence,
        string TheoremStatus,
        string Verdict);
}
