namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for information reproduction, inheritance, and evolutionary lineage tracking.
///
/// AT-134: Information Species Reproduction and Inheritance
/// </summary>
public static class InformationLineage
{
    /// <summary>
    /// Represents a single reproduction event: two species interact and may produce offspring.
    /// </summary>
    public sealed record ReproductionEvent(
        string ParentA,
        string ParentB,
        double[] ParentAPattern,
        double[] ParentBPattern,
        string Outcome,                     // "Merge", "Compete", "Coexist", "Reproduce", "Extinct"
        double[] ChildPattern,              // null if no reproduction
        double ParentChildSimilarityA,      // child vs parent A pattern similarity
        double ParentChildSimilarityB,      // child vs parent B pattern similarity
        double InheritanceCoefficient,      // H(parent, child) - overall heritability score
        bool ChildSurvived,                 // does the child persist?
        string Description);

    /// <summary>
    /// Tracks a lineage: ancestor → descendants over time.
    /// </summary>
    public sealed record SpeciesLineage(
        string SpeciesName,
        string AncestorName,
        int Generation,
        List<string> Descendants,
        double[] AncestorPattern,
        double[] CurrentPattern,
        double LineageSimilarity,           // current vs ancestor similarity
        int LineageLength,                  // number of generations survived
        double MutationDrift,               // accumulated pattern drift per generation
        bool IsExtinct);

    /// <summary>
    /// Full reproduction profile for one species.
    /// </summary>
    public sealed record SpeciesReproductionProfile(
        string SpeciesName,
        double ReproductionRate,            // prob of producing offspring per interaction
        double SurvivalProbability,         // prob of surviving one generation
        double Fidelity,                    // how faithfully offspring match parent (1.0 = perfect)
        double MutationRate,                // drift per generation
        double CompetitiveAdvantage,        // relative fitness vs other species
        double[] BaselinePattern,
        List<ReproductionEvent> ReproductionHistory);

    /// <summary>
    /// Species transition matrix entry: T_ij = probability i -> j.
    /// </summary>
    public sealed record SpeciesTransition(
        string FromSpecies,
        string ToSpecies,
        double TransitionProbability,
        double MeanDrift,
        string Mechanism);                  // "AttractorCapture", "Mutation", "Competition"

    /// <summary>
    /// Complete evolution report for AT-134.
    /// </summary>
    public sealed record InformationEvolutionReport(
        List<SpeciesReproductionProfile> SpeciesProfiles,
        List<SpeciesLineage> Lineages,
        List<SpeciesTransition> TransitionMatrix,
        List<ReproductionEvent> AllReproductionEvents,
        int TotalReproductionEvents,
        int TotalExtinctions,
        int TotalLineages,
        int LongestLineageLength,
        double MeanInheritanceCoefficient,
        double MeanFidelity,
        double MeanSurvivalRate,
        bool ReproductionDetected,
        bool LineagesFormed,
        bool MutationsObserved,
        bool CompetitionDetected,
        string Classification,              // "A: Attractors Only" ... "D: Information Evolution Layer"
        string Verdict);
}
