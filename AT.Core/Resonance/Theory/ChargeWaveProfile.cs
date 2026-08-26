namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for emergent collective charge wave analysis.
/// Defines charge-wave experiments at varying density, coherence
/// transitions, and the collective wave phase diagram.
///
/// AT-127: Emergent Collective Charge Waves
/// </summary>
public static class ChargeWaveProfile
{
    /// <summary>Result of a many-charge wave experiment.</summary>
    public sealed record ChargeWaveRun(
        double K, double Lambda, int N, int Seed,
        int TargetQ, double ChargeDensity,
        string Layout,
        int FinalQ,
        double R_Q,                      // charge-mode Kuramoto order parameter
        double MeanAmplitude,            // ⟨|Θ(x)|⟩
        double AmplitudeStd,             // spatial variation of |Θ|
        double CoherenceLength,          // correlation length of Θ(x)
        double StructureFactorPeak,      // S(k) at dominant k
        double DominantWaveNumber,       // k where S(k) peaks
        double WaveVelocity,             // estimated from phase gradient
        bool StandingWaveDetected,
        bool TravelingWaveDetected,
        bool CollectiveWavePhase,        // coherent wave medium?
        string Regime);                  // "Dilute", "Correlated", "CoherentWave"

    public sealed record WaveSpectrum(
        double[] WaveNumbers,
        double[] StructureFactor,        // S(k) = ⟨|Θ̃(k)|²⟩
        double[] TemporalPower,          // frequency spectrum
        double DominantK,
        double DominantOmega,
        double DispersionRelation,       // ω/k
        string SpectrumType);            // "Flat", "Peaked", "Dispersive"

    public sealed record CoherenceTransition(
        bool TransitionFound,
        double CriticalDensity,
        double CriticalCoupling,
        double OrderParameterJump,       // ΔR_Q at transition
        double CoherenceLengthDivergence,// ξ near critical point
        string TransitionType,           // "Continuous", "FirstOrder", "Crossover"
        string ScalingAnalysis);

    public sealed record ChargeWavePhaseDiagram(
        double[] DensityAxis,
        double[] CouplingAxis,
        double[,] RQGrid,
        string[,] RegimeGrid,
        CoherenceTransition Transition,
        string Description);

    public sealed record CollectiveWaveReport(
        List<ChargeWaveRun> Runs,
        List<WaveSpectrum> Spectra,
        ChargeWavePhaseDiagram PhaseDiagram,
        CoherenceTransition Transition,
        bool CollectiveWavesFound,
        bool StandingWavesFound,
        bool TravelingWavesFound,
        bool CoherenceTransitionFound,
        string Classification,
        string Verdict);
}
