namespace AT.Core.Resonance.Theory;

/// <summary>
/// Data types for coherent field excitation analysis of topological
/// charge quanta. Defines excitation modes, resonance spectra,
/// coherent excitation events, and the excitation dynamics report.
///
/// AT-124: Coherent Field Excitations of Topological Charge
/// </summary>
public static class CoherentModeProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Core types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>An excitation mode of a Q=1 condensate.</summary>
    public sealed record ExcitationMode(
        string Name,
        string Description,
        double Frequency,           // dominant angular frequency
        double Amplitude,           // peak amplitude
        double QualityFactor,       // f/Δf (sharpness)
        double DecayTime,           // e-folding decay time
        bool IsStable,              // persists without driving?
        string Observable,          // "R(x)", "M(x)", "phase(x)", "width"
        string SpatialProfile);     // "Uniform", "Breathing", "Standing", "Traveling"

    /// <summary>A single coherent excitation event.</summary>
    public sealed record CoherentExcitation(
        string PerturbationType,    // "PhaseKick", "EnergyInject", "SpatialSqueeze", "FrequencyChirp"
        double PerturbationAmplitude,
        List<ExcitationMode> ModesFound,
        double TotalSpectralPower,  // integrated power across all modes
        double CoherenceTime,       // how long modes stay coherent
        bool ModesDetected,
        string Interpretation);

    /// <summary>A peak in the Fourier spectrum.</summary>
    public sealed record ResonancePeak(
        double Frequency,
        double Power,
        double Width,              // FWHM
        double QualityFactor,       // f/FWHM
        int HarmonicOrder,          // 1=fundamental, 2, 3, ...
        bool IsSignificant,         // above noise floor?
        string ModeType);           // "Breathing", "Oscillation", "Shape", "Unknown"

    /// <summary>Complete excitation spectrum for a condensate.</summary>
    public sealed record ExcitationSpectrum(
        double[] Frequencies,
        double[] PowerSpectrum,
        List<ResonancePeak> Peaks,
        double NoiseFloor,
        double TotalPower,
        int SignificantPeaks,
        string SpectrumType);      // "Discrete", "Continuous", "Noise"

    /// <summary>Complete excitation dynamics report.</summary>
    public sealed record ExcitationDynamicsReport(
        List<CoherentExcitation> Excitations,
        List<ExcitationSpectrum> Spectra,
        List<ExcitationMode> AllModes,
        bool CoherentModesFound,
        bool BreathingModeFound,
        bool StandingWaveFound,
        double FundamentalFrequency,
        int TotalModesIdentified,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Mode catalog
    // ══════════════════════════════════════════════════════════════════

    public static List<ExcitationMode> GetTheoreticalModes()
    {
        return new List<ExcitationMode>
        {
            new("Breathing Mode",
                "Periodic expansion and contraction of condensate width. " +
                "The soliton breathes — widens and narrows at characteristic frequency.",
                0, 0, 0, 0, false, "width(t)", "Breathing (spherically symmetric)"),

            new("Phase Oscillation",
                "Collective oscillation of the internal phase of all oscillators " +
                "within the condensate. θ(t) = θ₀ + A·sin(ωt). " +
                "This is the Kuramoto limit-cycle mode.",
                0, 0, 0, 0, false, "phase(x,t) inside condensate", "Uniform"),

            new("Standing Wave (1st harmonic)",
                "Spatial standing wave within the condensate: " +
                "R(x,t) = R₀(x) + A·sin(πx/L_cond)·cos(ωt). " +
                "One node at center.",
                0, 0, 0, 0, false, "R(x,t) inside condensate", "Standing (1 node)"),

            new("Standing Wave (2nd harmonic)",
                "Two-node standing wave: " +
                "R(x,t) = R₀(x) + A·sin(2πx/L_cond)·cos(ωt).",
                0, 0, 0, 0, false, "R(x,t) inside condensate", "Standing (2 nodes)"),

            new("Shape Oscillation",
                "Non-spherical deformation of condensate boundary. " +
                "The elliptical mode: condensate oscillates between prolate and oblate.",
                0, 0, 0, 0, false, "R boundary shape", "Quadrupolar"),

            new("Bound Mode (2-condensate)",
                "Coherent oscillation of two coupled condensates. " +
                "In-phase (symmetric) and out-of-phase (antisymmetric) modes. " +
                "Frequency splitting proportional to coupling strength.",
                0, 0, 0, 0, false, "phase difference between condensates", "Symmetric/Antisymmetric"),
        };
    }
}
