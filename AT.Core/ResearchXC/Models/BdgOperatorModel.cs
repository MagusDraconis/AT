namespace AT.Core.ResearchXC.Models;

/// <summary>
/// Models for BDG Uniqueness Audit (ResearchXC-007).
/// </summary>
public static class BdgOperatorModel
{
    /// <summary>Each assumption in the BDG construction.</summary>
    public sealed record BdgAssumption(
        string Name, string Description,
        bool IsNecessary, bool IsSufficient,
        string IfRelaxed,
        string Classification);

    /// <summary>A candidate alternative discrete d'Alembertian.</summary>
    public sealed record AlternativeOperator(
        string Name, string Definition,
        double NumLayers, string LayerWeights,
        bool ConvergesToBox, bool IsLocal,
        bool IsLorentzInvariant, bool IsAdditive,
        string FailureMode,
        string Status);

    /// <summary>A constraint on discrete d'Alembertians.</summary>
    public sealed record Constraint(
        string Name, string Description,
        int NumEliminated, int NumSurviving,
        bool IsEssential,
        string Verdict);

    /// <summary>The complete uniqueness assessment.</summary>
    public sealed record UniquenessAssessment(
        string Title,
        List<BdgAssumption> Assumptions,
        List<AlternativeOperator> Alternatives,
        List<Constraint> Constraints,
        int TotalCandidates, int SurvivingCandidates,
        double BdgNecessityScore,
        string UniquenessClass,
        string Verdict);

    /// <summary>BDG necessity score breakdown.</summary>
    public sealed record NecessityBreakdown(
        string Component, double Score, string Rationale);
}
