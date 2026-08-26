namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for alternative fitness models, universality metrics,
/// and evolution robustness analysis.
///
/// AT-137: Universality of Information Evolution
/// </summary>
public static class AlternativeFitnessModel
{
    /// <summary>
    /// A fitness model specification.
    /// </summary>
    public sealed record FitnessModelSpec(
        string Name,                        // "Baseline (r/c)", "Quadratic Repro (r²/c)", etc.
        string Formula,                     // "w = r/c", "w = r²/c"
        string Category,                    // "Rational", "Polynomial", "Logarithmic", "Random", "Emergent"
        bool IsEmergent,                    // true if fitness not explicitly defined
        Func<double, double, double> Compute); // (r, c) → w

    /// <summary>
    /// A resource regime specification.
    /// </summary>
    public sealed record ResourceRegime(
        string Name,                        // "Global", "Local", "Dynamic", etc.
        string Description,
        double CapacityScale,               // relative to baseline
        bool IsDynamic,                     // capacity changes over time?
        Func<int, double, double> CapacityFn); // (timeStep, baseCapacity) → effectiveCapacity

    /// <summary>
    /// Results from running one fitness model under one resource regime.
    /// </summary>
    public sealed record ModelRunResult(
        string FitnessModel,
        string ResourceRegime,
        int Extinctions,
        bool SelectionDetected,
        bool CompetitionObserved,
        bool CoexistenceObserved,
        string DominantSpecies,
        double MeanFitnessDifferential,     // max(w)/min(w)
        double RankStability,               // Kendall tau vs baseline ranking
        double PopulationChange,            // (final - initial) / initial
        bool EvolutionPersisted,            // did any Darwinian signature survive?
        string Notes);

    /// <summary>
    /// Aggregate universality metrics across all models and regimes.
    /// </summary>
    public sealed record UniversalityMetrics(
        int TotalRuns,
        int RunsWithSelection,              // selection detected
        int RunsWithExtinctions,            // extinctions occurred
        int RunsWithCompetition,            // competition observed
        int RunsWithCoexistence,            // coexistence observed
        double SelectionRobustnessIndex,    // fraction of runs with selection
        double EvolutionPersistenceScore,   // weighted composite of all Darwinian signatures
        double RankStabilityGlobal,         // mean Kendall tau across all models
        string MostUniversalFitnessModel,   // model that works across most regimes
        string MostRobustResourceRegime,    // regime that supports evolution across most models
        bool IsEvolutionUniversal,          // does evolution persist across most models/regimes?
        string Classification,              // "A: Evolution Artifact" ... "D: Universal Evolution Principle"
        string Verdict);

    /// <summary>
    /// Complete universality report for AT-137.
    /// </summary>
    public sealed record UniversalityReport(
        List<FitnessModelSpec> Models,
        List<ResourceRegime> Regimes,
        List<ModelRunResult> RunResults,
        UniversalityMetrics Metrics,
        Dictionary<string, double> SpeciesRankStability, // species → mean rank position across models
        string HiddenInvariant,             // what survives all modifications?
        string Classification,
        string Verdict);
}
