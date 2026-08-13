namespace TQM.Core.ResearchQG;

/// <summary>QG-084 natural acceleration scales and the central coincidence.
/// All values in m/s².</summary>
public static class LocalCosmicCoupling
{
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double C = 299792458.0;          // m/s
    public const double Kpc_m = 3.0857e19;
    public const double Msun_kg = 1.989e30;
    public const double G = 6.674e-11;

    public static double HubbleRatePerS => H0 / Kpc_m; // H0[km/s/Mpc]/[Mpc in km]=s^-1 (Kpc_m=3.0857e19=1 Mpc in km)

    /// <summary>g† = cH0/2π (TQM / time-scale).</summary>
    public static double Gdagger => C * HubbleRatePerS / (2.0 * Math.PI);

    /// <summary>c·H0.</summary>
    public static double CH => C * HubbleRatePerS;

    /// <summary>c²√Λ = c·√(3ΩΛ)·H0.</summary>
    public static double C2SqrtLambda => C * Math.Sqrt(3.0 * OmL) * HubbleRatePerS;

    /// <summary>c/t_universe (t0 = 13.8 Gyr).</summary>
    public static double COverTUniverse => C / (13.8e9 * 3.15576e7);

    /// <summary>c²/R_H = c·H0 (causal horizon).</summary>
    public static double C2OverRH => CH;

    /// <summary>Typical galactic acceleration GM/R² (M=1e11 Msun, R=10 kpc).</summary>
    public static double GalacticAcceleration =>
        G * 1e11 * Msun_kg / Math.Pow(10.0 * Kpc_m, 2);

    /// <summary>MOND's a0 (measured).</summary>
    public const double A0_Mond = 1.2e-10;

    /// <summary>All natural scales for the landscape.</summary>
    public static (string Name, double Value)[] NaturalScales() => new[]
    {
        ("g† = cH0/2π", Gdagger),
        ("a0 (MOND)", A0_Mond),
        ("galactic GM/R²", GalacticAcceleration),
        ("cH0", CH),
        ("c²√Λ", C2SqrtLambda),
        ("c/t0", COverTUniverse),
    };
}
