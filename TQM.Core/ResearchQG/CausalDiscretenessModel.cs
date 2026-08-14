namespace TQM.Core.ResearchQG;

/// <summary>QG-094 causal-discreteness model: the discreteness scale l_P and the suppression
/// factors governing each observable channel. The fundamental dimensionless suppression is
/// (l_P/λ) ~ 1e-28 for propagation, and 1/√N ~ 1e-122 for cosmological fluctuation effects.</summary>
public sealed record DiscretenessSignal(
    string Channel,
    string Effect,
    double Amplitude,
    double DetectorSensitivity,
    double SignalToNoise,
    bool Observable);

public static class CausalDiscretenessModel
{
    public const double PlanckLength = 1.616255e-35;  // m
    public const double C = 299792458.0;
    public const double H0 = 67.4;
    public const double LambdaObs = 1.1e-52;

    public static double HubbleLength => C / (H0 / 3.0857e19);

    public static double N => Math.Pow(HubbleLength / PlanckLength, 4.0);

    /// <summary>Ever-present Λ: the dark-energy scale itself (observed).</summary>
    public static double LambdaPlanck => 1.0 / Math.Sqrt(N);

    /// <summary>Fluctuation of Λ about its mean: δΛ/Λ ~ 1/√N.</summary>
    public static double LambdaFluctuation => 1.0 / Math.Sqrt(N);

    /// <summary>Propagation suppression (swerving / Lorentz violation): ~ l_P/λ, λ = 500 nm.</summary>
    public static double PropagationSuppression(double wavelength = 5e-7)
        => PlanckLength / wavelength;

    /// <summary>Phase noise for a GW/photon of frequency f: δφ ~ (l_P f/c)^α, α=1 (conservative).</summary>
    public static double PhaseNoise(double freqHz)
        => PlanckLength * freqHz / C;

    /// <summary>CMB horizon-scale cosmic-variance excess from Poisson discreteness: ~ 1/√l at l~2.</summary>
    public static double CmbLowLAnomaly => 1.0 / Math.Sqrt(2.0); // O(0.7) — NOT Planck-suppressed
}
