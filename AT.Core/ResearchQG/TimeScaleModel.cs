namespace AT.Core.ResearchQG;

/// <summary>
/// QG-080 Time-Scale Cosmology: core model. Introduces two times — coordinate time t
/// and physical/emergent time τ with clock rate γ(t) = dτ/dt — and establishes the
/// central equivalence: with γ(t) = a(t) (the FLRW scale factor), Time-Scale Cosmology
/// (TSC) is FLRW expressed in conformal time. All quantities below are analytic flat
/// ΛCDM (Ωm + ΩΛ = 1) with H0 = 67.4 km/s/Mpc.
/// </summary>
public static class Cosmology
{
    public const double H0 = 67.4;           // km/s/Mpc
    public const double OmM = 0.315, OmL = 0.685;
    public const double C_KMS = 299792.458;  // km/s
    public const double Kpc_m = 3.0857e19;

    /// <summary>Hubble parameter (coordinate time) at redshift z, in km/s/Mpc.</summary>
    public static double H(double z) => H0 * Math.Sqrt(OmM * Math.Pow(1 + z, 3) + OmL);

    /// <summary>FLRW scale factor at redshift z (a(0)=1).</summary>
    public static double ScaleFactor(double z) => 1.0 / (1.0 + z);

    /// <summary>Cosmic age t(a) for flat ΛCDM: t = (2/3H0√ΩΛ)·asinh(√(ΩΛ/Ωm) a^{3/2}).</summary>
    public static double AgeAtScaleFactor(double a)
    {
        double arg = Math.Sqrt(OmL / OmM) * Math.Pow(a, 1.5);
        return 2.0 / (3.0 * H0 * Math.Sqrt(OmL)) * Math.Asinh(arg); // in 1/(H0) units × (Mpc/km)
    }

    /// <summary>Cosmic age in Gyr (1/H0 in Gyr × dimensionless).</summary>
    public static double AgeGyratScaleFactor(double a)
    {
        double hInvGyr = 978.0; // 1/(H0 in km/s/Mpc) in Gyr for H0=67.4 (≈14.5 Gyr · 67.4)
        return AgeAtScaleFactor(a) * 978.0 / 1.0; // age in (Mpc/km) × conversion
    }

    /// <summary>Conformal time η(a) = ∫ da/(a² H(a)) from a→0 to a (numerical).</summary>
    public static double ConformalTimeAtScaleFactor(double a, int n = 20000)
    {
        double aMin = 1e-4;
        double result = 0;
        double da = (a - aMin) / n;
        for (int i = 0; i < n; i++)
        {
            double a1 = aMin + i * da, a2 = aMin + (i + 1) * da;
            double f1 = 1.0 / (a1 * a1 * HOfA(a1));
            double f2 = 1.0 / (a2 * a2 * HOfA(a2));
            result += 0.5 * (f1 + f2) * da;
        }
        return result; // units of 1/H0 (Mpc/km)
    }

    private static double HOfA(double a) =>
        H0 * Math.Sqrt(OmM / (a * a * a) + OmL);
}

/// <summary>Record carrying the time-scale quantities at a given redshift z.</summary>
public sealed record TimeScalePoint(
    double Z,
    double ScaleFactor,
    double H,
    double Gamma,          // γ = dτ/dt = a
    double HLnGamma,       // d(ln γ)/dt = H
    double Gdagger_TSC,    // c·γ̇/(2πγ) = c·H/2π  [m/s²]
    double Gdagger_AT);   // c·H(z)/2π            [m/s²]

/// <summary>Aggregate QG-080 report.</summary>
public sealed record TimeScaleReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    TimeScalePoint[] Points,
    double SnIaDiscriminationSigma,
    string OutDir);
