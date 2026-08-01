namespace TQM.Core.Quantum;

/// <summary>
/// Contains the full eigenmode spectrum of a temporal coupling matrix
/// along with aggregate spectral metrics.
/// </summary>
public sealed class TemporalModeSpectrum
{
    /// <summary>
    /// Total number of eigenmodes computed.
    /// </summary>
    public int ModeCount { get; }

    /// <summary>
    /// The most dominant mode (largest |λ|), or null if no modes.
    /// </summary>
    public TemporalEigenMode? DominantMode { get; }

    /// <summary>
    /// All computed modes ordered by rank (dominant first).
    /// </summary>
    public IReadOnlyList<TemporalEigenMode> AllModes { get; }

    /// <summary>
    /// Spectral radius: the largest absolute eigenvalue.
    /// </summary>
    public double SpectralRadius { get; }

    /// <summary>
    /// Participation ratio: (Σ |λᵢ|)² / (K · Σ |λᵢ|²).
    /// Range: [1/K, 1]. Low values → mode concentration (few dominant modes).
    /// High values (near 1) → equal participation across all modes.
    /// </summary>
    public double ParticipationRatio { get; }

    /// <summary>
    /// Number of modes with stability score above 0.1 (significant modes).
    /// </summary>
    public int SignificantModeCount { get; }

    /// <summary>
    /// Spectral gap: ratio between the dominant and second-dominant eigenvalues.
    /// A large gap (> 2) suggests a well-isolated dominant mode — an emergence signal.
    /// </summary>
    public double SpectralGap { get; }

    /// <summary>
    /// Mean inverse participation ratio across all modes.
    /// High values → localized modes (particle-like).
    /// Low values → delocalized modes (wave-like).
    /// </summary>
    public double MeanIPR { get; }

    private TemporalModeSpectrum(
        int modeCount,
        TemporalEigenMode? dominantMode,
        IReadOnlyList<TemporalEigenMode> allModes,
        double spectralRadius,
        double participationRatio,
        int significantModeCount,
        double spectralGap,
        double meanIPR)
    {
        ModeCount = modeCount;
        DominantMode = dominantMode;
        AllModes = allModes;
        SpectralRadius = spectralRadius;
        ParticipationRatio = participationRatio;
        SignificantModeCount = significantModeCount;
        SpectralGap = spectralGap;
        MeanIPR = meanIPR;
    }

    /// <summary>
    /// Constructs a TemporalModeSpectrum from a list of ranked eigenmodes.
    /// </summary>
    public static TemporalModeSpectrum FromModes(List<TemporalEigenMode> modes)
    {
        if (modes.Count == 0)
        {
            return new TemporalModeSpectrum(0, null, modes.AsReadOnly(),
                0, 0, 0, 0, 0);
        }

        double sumAbs = 0.0, sumSq = 0.0, sumIPR = 0.0;
        int significant = 0;

        foreach (var mode in modes)
        {
            sumAbs += mode.Magnitude;
            sumSq += mode.Magnitude * mode.Magnitude;
            sumIPR += mode.InverseParticipationRatio();

            if (mode.StabilityScore >= 0.1)
                significant++;
        }

        int k = modes.Count;
        double pr = sumSq > 1e-30 ? (sumAbs * sumAbs) / (k * sumSq) : 0;

        double spectralGap = k >= 2 && modes[1].Magnitude > 1e-15
            ? modes[0].Magnitude / modes[1].Magnitude
            : double.PositiveInfinity;

        double meanIPR = sumIPR / k;

        return new TemporalModeSpectrum(
            k,
            modes[0],
            modes.AsReadOnly(),
            modes[0].Magnitude,
            pr,
            significant,
            spectralGap,
            meanIPR);
    }
}
