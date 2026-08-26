namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for information attractors and stable information species.
///
/// AT-133: Information Attractors and Stable Information Species
/// </summary>
public static class InformationSpecies
{
    public sealed record InfoAttractor(
        string Name,
        double[] PrototypePattern,      // representative pattern
        double BasinSize,               // fraction of ICs converging here
        double StabilityLifetime,       // how long it persists
        double Entropy,                 // Shannon entropy
        int Complexity,                 // effective # of modes
        bool IsStable,                  // lifetime > 5000?
        string Morphology);             // "Uniform", "Standing", "AntiPhase", "Composite", "Chaotic"

    public sealed record InfoSpecies(
        string Name,
        string ParentAttractor,
        double OccurrenceFrequency,     // how often seen in ensembles
        double MeanLifetime,
        double MeanEntropy,
        int MeanComplexity,
        bool IsUniversal,               // appears across all densities?
        string Taxonomy);               // e.g. "Uniform/PhaseLocked", "Wave/Standing/n=1"

    public sealed record AttractorConvergence(
        int InitialPatterns,            // # of distinct initial patterns
        int FinalStates,                // # of distinct final states
        double ConvergenceRatio,        // final/initial (small = strong convergence)
        int UniqueAttractors,           // # of unique attractors found
        double MeanConvergenceTime,     // time to reach attractor
        string ConvergenceType);        // "Strong", "Weak", "None"

    public sealed record InfoSpeciesReport(
        List<InfoAttractor> Attractors,
        List<InfoSpecies> Species,
        List<AttractorConvergence> Convergences,
        bool AttractorsFound,
        bool SpeciesIdentified,
        bool ConvergenceObserved,
        int TotalUniqueAttractors,
        int TotalSpecies,
        string Classification,
        string Verdict);
}
