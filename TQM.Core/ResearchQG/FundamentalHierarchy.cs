namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-086 fundamental-scale hierarchy and dimensional reduction. Determines whether the
/// ~1e-10 m/s² scale can be formed from the fundamental constants alone (it cannot — those
/// give the Planck acceleration a_P ~ 1e51), and therefore that a₀ requires a cosmological
/// input (H, Λ or 1/t).
/// </summary>
public static class FundamentalHierarchy
{
    public const double C = 299792458.0;
    public const double G = 6.674e-11;
    public const double Hbar = 1.054571817e-34;
    public const double Lambda = 1.1e-52;   // m^-2
    public const double H0 = 67.4, OmL = 0.685;
    public const double Kpc_m = 3.0857e19;
    public const double Msun_kg = 1.989e30;

    public static double HubbleRatePerS => H0 / 3.0857e19; // H0[km/s/Mpc]/(Mpc in km)

    // Fundamental derived scales.
    public static double PlanckLength => Math.Sqrt(Hbar * G / (C * C * C));
    public static double PlanckAcceleration => C * C / PlanckLength;
    public static double PlanckTime => PlanckLength / C;

    // Cosmological rates (s^-1).
    public static double CosmicRateH => HubbleRatePerS;
    public static double CosmicRateSqrtLambda => C * Math.Sqrt(Lambda);
    public static double CosmicRateAge => 1.0 / (13.8e9 * 3.15576e7);

    // Cosmological accelerations (m/s²).
    public static double CH => C * HubbleRatePerS;
    public static double C2SqrtLambda => C * C * Math.Sqrt(Lambda);
    public static double COverAge => C / (13.8e9 * 3.15576e7);

    /// <summary>Typical galactic acceleration GM/R² (M=1e11 Msun, R=10 kpc).</summary>
    public static double GalacticAcceleration => G * 1e11 * Msun_kg / Math.Pow(10.0 * Kpc_m, 2);

    /// <summary>Cluster acceleration (M=1e14 Msun, R=1 Mpc).</summary>
    public static double ClusterAcceleration => G * 1e14 * Msun_kg / Math.Pow(1e3 * Kpc_m, 2);

    /// <summary>Earth-orbit acceleration (Sun, 1 AU).</summary>
    public static double EarthOrbitAcceleration => G * 1.989e30 / Math.Pow(1.496e11, 2);

    /// <summary>Full landscape (name, log10 value).</summary>
    public static (string Name, double Log10A)[] Landscape() => new[]
    {
        ("Planck a_P = c²/l_P", Math.Log10(PlanckAcceleration)),
        ("Earth orbit GM/R²", Math.Log10(EarthOrbitAcceleration)),
        ("galactic GM/R²", Math.Log10(GalacticAcceleration)),
        ("cluster GM/R²", Math.Log10(ClusterAcceleration)),
        ("a0 (MOND)", Math.Log10(1.2e-10)),
        ("g† = cH/2π", Math.Log10(LocalCosmicCoupling.Gdagger)),
        ("cH0", Math.Log10(CH)),
        ("c²√Λ", Math.Log10(C2SqrtLambda)),
        ("c/t0", Math.Log10(COverAge)),
    };

    /// <summary>Can a₀ be formed from {c, G, ħ} alone? Returns the only such acceleration (a_P).</summary>
    public static bool RequiresCosmologicalInput(double a0 = 1.2e-10)
        => Math.Abs(Math.Log10(PlanckAcceleration / a0)) > 30; // a_P is ~61 decades above a₀

    /// <summary>The simplest dimensionless formulation: a₀ ≈ c × (cosmic rate), all ~1e-18 s^-1.</summary>
    public static (string Name, double RatePerS)[] CosmicRates() => new[]
    {
        ("H0", CosmicRateH),
        ("√Λ·c", CosmicRateSqrtLambda),
        ("1/t_universe", CosmicRateAge),
    };
}
