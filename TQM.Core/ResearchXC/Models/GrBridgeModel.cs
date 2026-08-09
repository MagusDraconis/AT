namespace TQM.Core.ResearchXC.Models;

/// <summary>
/// Models for GR Bridge Completion Program (ResearchXC-006).
/// </summary>
public static class GrBridgeModel
{
    /// <summary>Each step in the Q → GR derivation chain.</summary>
    public sealed record BridgeStep(
        string Name, string Description,
        string DerivationStatus, // "TQM-derived", "External theorem", "Heuristic", "Missing"
        bool IsTqmNative,
        string GapDescription,
        string Priority); // "Critical", "High", "Medium", "Low"

    /// <summary>A candidate TQM-native gravitational action.</summary>
    public sealed record CandidateAction(
        string Name, string Lagrangian,
        bool RecoversEinstein,
        bool RecoversNewtonianLimit,
        bool HasCorrectSign,
        string Viability,
        string Verdict);

    /// <summary>Curvature interpretation from Q-event connectivity.</summary>
    public sealed record CurvatureInterpretation(
        string Approach, string Definition,
        bool RecoverRicci, bool RecoverRiemann,
        string ContinuumLimit,
        string Status);

    /// <summary>A theorem gap — what's needed for full derivation.</summary>
    public sealed record TheoremGap(
        string Gap, string WhatIsNeeded,
        string Difficulty, // "Moderate", "Hard", "Very Hard", "Open Problem"
        string CurrentBestApproach,
        bool BlocksFullDerivation);

    /// <summary>The complete bridge audit.</summary>
    public sealed record BridgeAudit(
        string Title,
        List<BridgeStep> Steps,
        List<CandidateAction> Actions,
        List<CurvatureInterpretation> CurvatureViews,
        List<TheoremGap> Gaps,
        double NativeFraction,
        double ExternalFraction,
        double MissingFraction,
        string Roadmap,
        string Verdict);
}
