namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for inter-charge coherence and phase locking analysis.
/// Defines phase-locking experiments, locking metrics, beat patterns,
/// collective modes, and the inter-charge coherence report.
///
/// TQM-125: Inter-Charge Coherence and Phase Locking
/// </summary>
public static class PhaseLockingProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>Result of a single phase-locking experiment between charges.</summary>
    public sealed record PhaseLockingRun(
        double K, double Lambda, int N, int Seed,
        int NumCharges,                   // 2 or 3
        double Separation,                // distance between charge centers
        double InitialPhaseOffset,        // Δφ at t=0
        double FrequencyDetuning,         // Δω = ω₂−ω₁
        bool PhaseLocked,                 // did phases lock?
        double LockingTime,               // iterations until locking
        double FinalPhaseDiff,            // steady-state Δφ
        double PhaseDiffStd,              // fluctuation in locked state
        double FrequencyRatio,            // ω₂/ω₁ after locking (1.0 = perfect)
        double[] PhaseDiffHistory,
        double[] FreqRatioHistory,
        string LockingType);              // "1:1", "1:2", "None", "Chaotic"

    /// <summary>Aggregated locking results for a parameter combination.</summary>
    public sealed record LockingResult(
        double Separation,
        int NumCharges,
        int TotalTrials,
        int LockedTrials,
        double LockingProbability,
        double MeanLockingTime,
        double MeanFinalPhaseDiff,
        double MeanPhaseDiffStd,
        double CoherenceLength,           // max separation where locking occurs
        bool LongRangeCoherence,          // locking beyond 5λ?
        string LockingRegime);            // "Strong", "Weak", "None"

    /// <summary>A detected beat pattern between two charges.</summary>
    public sealed record BeatPattern(
        double BeatFrequency,             // |ω₂−ω₁|
        double BeatAmplitude,
        bool IsResolved,                  // beat frequency > 0 (not locked)
        double CoherenceTime,             // how long beats stay coherent
        string PatternType);              // "Regular", "Irregular", "None"

    /// <summary>Collective mode of an ensemble of charges.</summary>
    public sealed record CollectiveMode(
        string Name,                      // "Symmetric", "Antisymmetric", "Splay", "Cluster"
        double Frequency,
        int NumChargesParticipating,
        double ParticipationFraction,
        double OrderParameter,           // R_Q for this mode
        bool IsPhaseLocked,
        string Description);

    /// <summary>Complete inter-charge coherence report.</summary>
    public sealed record InterChargeReport(
        List<PhaseLockingRun> Runs,
        List<LockingResult> LockingResults,
        List<BeatPattern> Beats,
        List<CollectiveMode> CollectiveModes,
        double[,] LockingProbabilityGrid,   // separation × detuning
        double[] SeparationAxis,
        double[] DetuningAxis,
        double CoherenceLength,
        bool PhaseLockingObserved,
        bool FrequencyLockingObserved,
        bool CollectiveModesFound,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Locking regime classification
    // ══════════════════════════════════════════════════════════════════

    public static string ClassifyLocking(
        int locked, int total, double meanTime, double meanStd, double separation, double lambda)
    {
        double prob = (double)locked / total;
        if (prob > 0.8 && meanStd < 0.1) return "Strong";
        if (prob > 0.4 && meanStd < 0.3) return "Weak";
        if (prob > 0) return "Marginal";
        return "None";
    }
}
