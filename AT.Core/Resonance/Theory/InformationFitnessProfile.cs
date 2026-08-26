namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for information fitness, selection dynamics,
/// resource-constrained population evolution, and Darwinian ecology.
///
/// AT-135: Information Selection Under Resource Constraints
/// </summary>
public static class InformationFitnessProfile
{
    /// <summary>
    /// A resource budget that constrains the total information ecology.
    /// </summary>
    public sealed record ResourceBudget(
        string Name,                        // "Amplitude", "Memory", "Coherence", "Lifetime", "Spatial", "Bandwidth"
        double TotalCapacity,               // total units available
        double CurrentUsage,                // currently consumed
        double RegenerationRate,            // how fast capacity regenerates per time step
        bool IsExhaustible);                // true if finite and depletable

    /// <summary>
    /// Per-species resource consumption profile.
    /// </summary>
    public sealed record ResourceConsumption(
        string SpeciesName,
        double AmplitudeConsumption,        // Θ amplitude units per individual
        double MemoryConsumption,           // memory capacity units per individual
        double CoherenceConsumption,        // coherence units per individual
        double LifetimeConsumption,         // lifetime units per individual
        double SpatialConsumption,          // spatial occupancy units per individual
        double BandwidthConsumption);       // information bandwidth units per individual

    /// <summary>
    /// Fitness metrics for one species in a resource-constrained environment.
    /// </summary>
    public sealed record SpeciesFitness(
        string SpeciesName,
        double IntrinsicGrowthRate,         // r_i: reproduction rate without constraints
        double CarryingCapacity,            // K_i: max sustainable population
        double ResourceEfficiency,          // offspring per resource unit consumed
        double CompetitiveCoefficient,      // α_ii: intra-species competition
        double SelectionCoefficient,        // s_i: relative fitness advantage
        double MeanPopulationAtEquilibrium, // steady-state population size
        double ExtinctionProbability,       // chance of going extinct under constraints
        bool IsDominant,                    // highest fitness in current ecology?
        string FitnessRank);                // "Dominant", "Intermediate", "Marginal"

    /// <summary>
    /// A single temporal snapshot of a multi-species population.
    /// </summary>
    public sealed record PopulationSnapshot(
        int TimeStep,
        Dictionary<string, int> Populations,     // species → count
        Dictionary<string, double> Frequencies,  // species → fraction of total
        double TotalPopulation,
        double TotalResourceUsage,
        double ResourcePressure,                 // usage / capacity ( > 1 = overshoot)
        string DominantSpecies);

    /// <summary>
    /// Selection metrics derived from population dynamics.
    /// </summary>
    public sealed record SelectionMetrics(
        string SpeciesName,
        double DeltaFrequency,              // change in relative abundance over time
        double MeanGrowthRate,              // average dN/dt
        double FitnessRelativeToMean,       // w_i / ⟨w⟩
        double SelectionDifferential,       // S = change in mean trait due to selection
        bool FrequencyIncreased,            // did this species grow its share?
        bool WentExtinct,                   // population reached zero?
        bool IsSignificant);                // did selection exceed noise threshold?

    /// <summary>
    /// Complete selection report for AT-135.
    /// </summary>
    public sealed record SelectionReport(
        List<ResourceBudget> Budgets,
        List<ResourceConsumption> ConsumptionProfiles,
        List<SpeciesFitness> FitnessProfiles,
        List<PopulationSnapshot> PopulationHistory,
        List<SelectionMetrics> SelectionMetrics,
        int TotalGenerations,
        int InitialTotalPopulation,
        int FinalTotalPopulation,
        int ExtinctionEvents,
        double MeanSelectionCoefficient,
        double MaxFitnessDifferential,      // max(w_i) / min(w_i)
        bool SelectionDetected,
        bool ExtinctionsObserved,
        bool DominanceShiftObserved,        // did the dominant species change?
        bool CoexistenceObserved,
        string ReplicatorEquationFit,       // "Strong", "Moderate", "Weak", "None"
        string Classification,              // "A: No Selection" ... "D: Darwinian Information Ecology"
        string Verdict);
}
