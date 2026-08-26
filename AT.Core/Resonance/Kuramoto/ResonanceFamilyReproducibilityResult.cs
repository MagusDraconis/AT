namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Reproducibility statistics for a resonance family across multiple independent simulation runs.
/// Tracks how consistently the family reappears and how stable its signatures are.
/// </summary>
public sealed class ResonanceFamilyReproducibilityResult
{
    /// <summary>
    /// Family identifier (matches AT-007 family IDs).
    /// </summary>
    public int FamilyId { get; }

    /// <summary>
    /// Fraction of simulation runs where this family was detected.
    /// 1.0 = appears in every run. 0.0 = never appears.
    /// </summary>
    public double OccurrenceRate { get; }

    /// <summary>
    /// Mean cluster lifetime across all occurrences (iterations).
    /// </summary>
    public double MeanLifetime { get; }

    /// <summary>
    /// Standard deviation of cluster lifetime.
    /// </summary>
    public double LifetimeStd { get; }

    /// <summary>
    /// Mean cluster size (oscillator count).
    /// </summary>
    public double MeanSize { get; }

    /// <summary>
    /// Standard deviation of cluster size.
    /// </summary>
    public double SizeStd { get; }

    /// <summary>
    /// Mean effective frequency.
    /// </summary>
    public double MeanFrequency { get; }

    /// <summary>
    /// Standard deviation of frequency.
    /// </summary>
    public double FrequencyStd { get; }

    /// <summary>
    /// Mean internal synchronization (order parameter).
    /// </summary>
    public double MeanCoherence { get; }

    /// <summary>
    /// Standard deviation of synchronization.
    /// </summary>
    public double CoherenceStd { get; }

    /// <summary>
    /// Total number of simulation runs where this family was detected.
    /// </summary>
    public int TotalOccurrences { get; }

    /// <summary>
    /// Total number of simulation runs performed.
    /// </summary>
    public int TotalRuns { get; }

    /// <summary>
    /// Reproducibility classification:
    /// Universal, Likely Universal, Unstable, Seed Artifact.
    /// </summary>
    public string Classification { get; }

    /// <summary>
    /// Composite reproducibility score: occurrence rate × (1 - normalized variance).
    /// Higher = more reproducible. Range [0, 1].
    /// </summary>
    public double ReproducibilityScore { get; }

    public ResonanceFamilyReproducibilityResult(
        int familyId,
        double occurrenceRate,
        double meanLifetime,
        double lifetimeStd,
        double meanSize,
        double sizeStd,
        double meanFrequency,
        double frequencyStd,
        double meanCoherence,
        double coherenceStd,
        int totalOccurrences,
        int totalRuns,
        string classification,
        double reproducibilityScore)
    {
        FamilyId = familyId;
        OccurrenceRate = occurrenceRate;
        MeanLifetime = meanLifetime;
        LifetimeStd = lifetimeStd;
        MeanSize = meanSize;
        SizeStd = sizeStd;
        MeanFrequency = meanFrequency;
        FrequencyStd = frequencyStd;
        MeanCoherence = meanCoherence;
        CoherenceStd = coherenceStd;
        TotalOccurrences = totalOccurrences;
        TotalRuns = totalRuns;
        Classification = classification;
        ReproducibilityScore = reproducibilityScore;
    }

    public override string ToString() =>
        $"F{FamilyId}: occ={OccurrenceRate:P0} score={ReproducibilityScore:F3} " +
        $"τ={MeanLifetime:F0}±{LifetimeStd:F0} size={MeanSize:F1}±{SizeStd:F1} [{Classification}]";
}
