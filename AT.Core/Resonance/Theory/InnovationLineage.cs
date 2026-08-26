namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for open-ended information innovation:
/// novel species detection, innovation metrics, and evolutionary discovery curves.
///
/// AT-138: Open-Ended Information Innovation
/// </summary>
public static class InnovationLineage
{
    /// <summary>
    /// A novel information species discovered during long-term evolution.
    /// </summary>
    public sealed record NovelSpecies(
        string Name,                        // "N1", "N2", etc.
        double[] PrototypePattern,          // representative pattern vector
        int DiscoveryTime,                  // generation when first detected
        string ParentSpecies,               // closest known species at discovery
        double NoveltyScore,                // 1 - max_similarity_to_known (higher = more novel)
        int PersistenceGenerations,         // how many generations it survived
        double MeanComplexity,              // average number of zero crossings
        double MeanEnergy,                  // average pattern energy
        bool IsPersistent);                 // survived > 100 generations?

    /// <summary>
    /// Innovation metrics computed from long-term evolution runs.
    /// </summary>
    public sealed record InnovationMetrics(
        int TotalNovelSpeciesDiscovered,
        int PersistentNovelSpecies,         // survived > 100 gens
        double InnovationRate,              // new species per 1000 generations
        double SpeciesSaturationIndex,      // 0 = still growing, 1 = fully saturated
        double MeanComplexityInitial,       // average complexity at start
        double MeanComplexityFinal,         // average complexity at end
        double ComplexityGrowthRate,        // Δcomplexity / time
        int MaxLineageDepth,                // deepest ancestor chain
        double MeanNoveltyScore,
        bool InnovationDetected,            // any novel species found?
        bool SaturationObserved,            // did discovery curve plateau?
        bool ComplexityIncreased,           // did complexity grow?
        string DiscoveryCurveShape);        // "Linear", "Logarithmic", "Saturating", "Exponential"

    /// <summary>
    /// A temporal snapshot of species diversity during evolution.
    /// </summary>
    public sealed record DiversitySnapshot(
        int TimeStep,
        int KnownSpeciesCount,              // A, B, C, D still alive
        int NovelSpeciesCount,              // new species discovered so far
        int TotalAliveSpecies,              // total distinct species alive
        double MeanComplexity,
        double MeanNoveltyScore,
        string DominantNovelSpecies);

    /// <summary>
    /// Complete innovation report for AT-138.
    /// </summary>
    public sealed record InnovationReport(
        List<NovelSpecies> NovelSpecies,
        List<DiversitySnapshot> DiversityHistory,
        InnovationMetrics Metrics,
        int TotalPopulationSize,
        int TotalGenerations,
        bool OpenEndedEvolution,            // does novelty keep emerging?
        string Classification,              // "A: Fixed Species Catalog" ... "D: Open-Ended Evolution"
        string Verdict);
}
