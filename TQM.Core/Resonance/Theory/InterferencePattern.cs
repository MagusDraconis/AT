namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Data types for charge mode interference analysis.
/// Defines interference experiments, beat spectra, collective
/// wave reconstruction, and visibility metrics.
///
/// TQM-126: Charge Mode Interference
/// </summary>
public static class InterferencePattern
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    public sealed record InterferenceRun(
        double K, double Lambda, int N, int Seed,
        int NumCharges, double Separation,
        double PhaseOffset,              // controlled Δφ
        double ObservedAmplitude,        // |Θ_total| measured
        double PredictedAmplitude,       // |Σ A_i exp(iθ_i)| from superposition
        double Visibility,               // (max−min)/(max+min) of amplitude
        double BeatFrequency,
        bool ConstructiveObserved,       // amplitude > sum of individuals
        bool DestructiveObserved,        // amplitude < individual amplitudes
        bool PhaseNodeDetected,          // spatial node in Θ(x,t)
        double CoherenceLifetime,
        string InterferenceClass);       // "Constructive", "Destructive", "Neutral", "Beat"

    public sealed record BeatSpectrum(
        double[] Frequencies,
        double[] Power,
        double DominantBeat,             // |ω₂−ω₁|
        double BeatVisibility,           // how clean the beat is
        int HarmonicBeats,               // 2|ω₂−ω₁|, 3|ω₂−ω₁|, etc.
        string BeatQuality);             // "Clean", "Modulated", "Irregular"

    public sealed record CollectiveWave(
        double[] X,                      // spatial positions
        double[] ThetaTotal,             // Θ(x) = Σ A_i exp(iθ_i)
        double[] AmplitudeEnvelope,      // |Θ(x)|
        double[] PhaseProfile,           // arg(Θ(x))
        int NodeCount,                   // # of points where |Θ| ≈ 0
        double Wavelength,               // spatial period of interference
        bool MatchesSuperposition,       // does observation match linear sum?
        double SuperpositionError);      // rms deviation from prediction

    public sealed record ModeVisibility(
        double Separation,
        double PhaseOffset,
        double Visibility,
        double Contrast,
        double InterferenceDepth,       // 1 − min(|Θ|)/max(|Θ|)
        string FringePattern);           // "Uniform", "Fringes", "Nodes"

    public sealed record ModeInterferenceReport(
        List<InterferenceRun> Runs,
        List<BeatSpectrum> BeatSpectra,
        List<CollectiveWave> CollectiveWaves,
        List<ModeVisibility> VisibilityData,
        bool InterferenceObserved,
        bool ConstructiveConfirmed,
        bool DestructiveConfirmed,
        bool BeatPhenomenaObserved,
        bool PhaseNodesFound,
        string Classification,
        string Verdict);
}
