namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for information fitness law discovery.
/// Evaluates candidate fitness functions against observed selection outcomes.
///
/// AT-136: Information Fitness Law
/// </summary>
public static class FitnessCandidate
{
    /// <summary>
    /// A candidate fitness function with computed values for each species.
    /// </summary>
    public sealed record FitnessFunction(
        string Name,                        // e.g., "Resource Efficiency", "Coherence × Reproduction"
        string Formula,                     // e.g., "w = r / c", "w = r · coherence"
        int ParameterCount,                 // number of fitted parameters (0 = parameter-free)
        Dictionary<string, double> SpeciesValues,  // species → fitness value
        double[] ObservedTarget,            // ground-truth selection outcomes (same order as species)
        double PearsonR,                    // Pearson correlation with observed
        double SpearmanRho,                 // Spearman rank correlation
        double R2,                          // coefficient of determination
        double AICC,                        // corrected AIC (penalizes complexity)
        string PredictiveRank,              // "Excellent", "Good", "Moderate", "Weak", "None"
        bool IsSignificant);                // p < 0.05 equivalent (|r| > 0.7 with 4 data points)

    /// <summary>
    /// Species-level measurements for fitness analysis.
    /// </summary>
    public sealed record SpeciesMeasurements(
        string SpeciesName,
        double PatternEnergy,               // Σ(pattern²)
        double ShannonEntropy,              // information content
        double Coherence,                   // phase alignment metric
        double DominantFrequency,           // primary Fourier mode
        double ZeroCrossings,              // pattern complexity
        double ResourceConsumption,         // total consumption (from AT-135)
        double ReproductionRate,            // intrinsic growth (from AT-135)
        double DeathRate,                   // baseline mortality (from AT-135)
        double MutationRobustness,          // 1 / mutation rate
        double MemoryPersistence,           // pattern stability over time
        double InformationDensity,          // entropy per resource unit
        double ObservedSelectionCoefficient, // s_i from AT-135
        double ObservedDeltaFrequency,      // Δf from AT-135
        string ObservedDominance);          // "Dominant", "Intermediate", "Marginal"

    /// <summary>
    /// A fitness landscape: maps a 2-variable space to fitness.
    /// </summary>
    public sealed record FitnessLandscape2D(
        string VariableX,
        string VariableY,
        double[] XValues,
        double[] YValues,
        double[] FitnessValues,             // predicted fitness at (x, y)
        double OptimalX,
        double OptimalY,
        double MaxFitness,
        string LandscapeShape);             // "Single Peak", "Ridge", "Multi-Peak", "Flat"

    /// <summary>
    /// Multivariate fitness model.
    /// </summary>
    public sealed record MultivariateModel(
        string[] Variables,
        double[] Coefficients,
        double Intercept,
        double R2,
        double AdjustedR2,
        double AICC,
        string Formula);                    // e.g., "w = 0.12·E + 0.34·C - 0.05·H"

    /// <summary>
    /// Complete fitness law report for AT-136.
    /// </summary>
    public sealed record FitnessLawReport(
        List<SpeciesMeasurements> Measurements,
        List<FitnessFunction> Candidates,
        FitnessFunction BestSingleVariable,
        MultivariateModel BestMultivariate,
        FitnessLandscape2D Landscape,
        string BestFormula,
        double PredictionAccuracy,          // fraction of AT-135 rankings correctly predicted
        bool SingleVariableFound,
        bool MultivariateFound,
        bool PredictivePowerDemonstrated,
        string Classification,              // "A: No Fitness Law" ... "D: Fundamental Information Fitness Law"
        string Verdict);
}
