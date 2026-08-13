namespace TQM.Core.ResearchQG;

/// <summary>Shared RAR/TQM constants and closed-form predictions used by the
/// QG-076 gas-mass systematics audit. Deterministic and reproducible.</summary>
public static class RARPhysics
{
    public const double C_KMS = 299792.458;
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double G_ACC = 6.674e-11 * 1.989e30 / (3.0857e19 * 3.0857e19); // m/s² per Msun/kpc²

    // Per-source 1σ dex scatter (log10 space) used in the error budget.
    public const double SigmaStellarDex = 0.15;   // COSMOS2015 stellar mass
    public const double SigmaInclDex = 0.10;      // inclination deprojection
    public const double SigmaRcDex = 0.07;        // rotation-curve measurement
    public const double SigmaRadiusDex = 0.10;    // effective radius -> g_bar shape
    public const double SigmaIntrinsicDex = 0.10; // residual RAR intrinsic scatter

    public static double GdaggerLocal() => C_KMS * (H0 / 3.0857e19 * 1e3) / (2.0 * Math.PI);

    /// <summary>TQM prediction: g†(z) = c·H(z)/2π = g†(0)·sqrt(Ωm(1+z)³+ΩΛ).</summary>
    public static double GdaggerTqm(double z) =>
        GdaggerLocal() * Math.Sqrt(OmM * Math.Pow(1 + z, 3) + OmL);

    public static double LogGdaggerTqm(double z) => Math.Log10(GdaggerTqm(z));
}

/// <summary>Per-galaxy gas-mass systematics: the sensitivity factor S = |1 + 2·g_bar/g†|
/// and the gas fraction at the transition radius, together with the full error budget.</summary>
public sealed record GalaxyGasSystematics(
    string Object,
    double Z,
    double MStar,
    double MGas,
    double ReKpc,
    double LogGdagger,
    double GbarOut,
    double GobsOut,
    double FGasLocal,
    double SFactor,
    double SigmaStellar,
    double SigmaGasAt03,
    double SigmaIncl,
    double SigmaRc,
    double SigmaRadius,
    double SigmaIntrinsic,
    bool Constrained);

/// <summary>σ(log g†) as a function of the gas-mass uncertainty σ(log Mgas).</summary>
public sealed record GdaggerSensitivityPoint(
    double SigmaGasDex,
    double MedianSigmaGdaggerDex,
    double MeanSigmaGdaggerDex,
    double P16SigmaGdaggerDex,
    double P84SigmaGdaggerDex);

/// <summary>Synthetic-truth recovery/false-positive result at one gas precision.</summary>
public sealed record RecoveryPoint(
    double SigmaGasDex,
    double RecoveryRateTqm,
    double FalsePositiveRateMond,
    double MeanDeltaChi2Tqm,
    double Snr2);

/// <summary>Model-discrimination summary (χ²/AIC/BIC/Bayes factor).</summary>
public sealed record DiscriminationRow(string Model, double Chi2, double AIC, double BIC, double BayesFactor);

/// <summary>One row of the per-galaxy error budget CSV.</summary>
public sealed record GasErrorBudgetRow(
    string Object, double Z, double FGasLocal, double SFactor,
    double SigmaStellar, double SigmaGas, double SigmaIncl, double SigmaRc,
    double SigmaRadius, double SigmaIntrinsic, double SigmaTotal);

/// <summary>Aggregate report returned by GasMassSystematicsAnalyzer.</summary>
public sealed record GasMassSystematicsReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG, string SH,
    GdaggerSensitivityPoint[] Sensitivity,
    RecoveryPoint[] Recovery,
    DiscriminationRow[] Discrimination,
    GalaxyGasSystematics[] Galaxies,
    string CsvDir);
