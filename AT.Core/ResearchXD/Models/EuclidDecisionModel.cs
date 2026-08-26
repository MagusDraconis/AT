namespace AT.Core.ResearchXD.Models;

/// <summary>
/// Models for Euclid Decision Tree (ResearchXD-004).
/// </summary>
public static class EuclidDecisionModel
{
    /// <summary>A Euclid/Roman/DESI observational scenario.</summary>
    public sealed record ObservationalScenario(
        string Id, string Name,
        string Measurement, double WOscillator, double Sigma,
        string Timescale,
        string AtVerdict, string ActionClass,
        string[] KilledSectors, string[] SurvivingSectors,
        int KilledCount, int SurvivingCount);

    /// <summary>A sector's survival under a scenario.</summary>
    public sealed record SectorSurvival(
        string Sector, string Description,
        double PriorConfidence,
        bool DependsOnWEz,
        string WorstCaseStatus,
        string BestCaseStatus);

    /// <summary>Confidence update for a research branch.</summary>
    public sealed record ConfidenceUpdate(
        string Branch, string Experiment,
        double Prior, double PosteriorWEqMinus1,
        double PosteriorWeakDev, double PosteriorStrongDev,
        double PosteriorWrongSign);

    /// <summary>Revision action for a specific sector and scenario.</summary>
    public sealed record RevisionAction(
        string Scenario, string Sector,
        string Action, // Preserve, Revise, Replace, Delete
        string Rationale,
        string Timeline);

    /// <summary>The complete decision tree.</summary>
    public sealed record DecisionTree(
        string Title,
        List<ObservationalScenario> Scenarios,
        List<SectorSurvival> Sectors,
        List<ConfidenceUpdate> ConfidenceUpdates,
        List<RevisionAction> RevisionActions,
        int TotalSectors, int MinimumSurviving,
        string ReadinessClass,
        string Verdict);
}
