namespace TQM.Core.ResearchXC.Models;

/// <summary>
/// Models for Beta Coupling Derivation (ResearchXC-011).
/// </summary>
public static class BetaModel
{
    /// <summary>A derivation approach for β.</summary>
    public sealed record BetaApproach(
        string Name, string Derivation,
        double BetaEstimate, double Uncertainty,
        bool IsAnalytical, string Status);

    /// <summary>Connectivity response to density perturbation.</summary>
    public sealed record ConnectivityResponse(
        string Quantity, string Formula,
        double Value, string Dependence,
        string Notes);

    /// <summary>A universality check for β.</summary>
    public sealed record UniversalityCheck(
        string DefectType, double Mass,
        double EstimatedBeta, bool IsUniversal,
        string Verdict);

    /// <summary>The complete β derivation assessment.</summary>
    public sealed record BetaAssessment(
        string Title,
        List<BetaApproach> Approaches,
        List<ConnectivityResponse> Responses,
        List<UniversalityCheck> Universality,
        double BestEstimate, double Uncertainty,
        string DerivationStatus,
        string Verdict);
}
