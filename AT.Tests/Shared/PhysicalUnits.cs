namespace AT.Tests.Shared;

/// <summary>
/// Physical (SI / astronomical) constants used by the ResearchY-G gravity audits when a dimensionless
/// AT quantity is translated into physical units. Every constant is a canonical AT value or a defining
/// SI/IAU value; no fitted quantity is introduced here. Shared by Y_G_001 and Y_G_003.
/// </summary>
public static class PhysicalUnits
{
    // ── Defining / measured constants ─────────────────────────────────────────────
    public const double C = 2.99792458e8;            // m/s (exact)
    public const double C_Kms = 299792.458;          // km/s (exact)
    public const double G_N = 9.80665;               // m/s^2 (standard gravity)
    public const double H0 = 67.4;                   // km/s/Mpc (Planck 2018)
    public const double Kms2PerKpcTo1e10 = 0.000324077929;   // km^2/s^2/kpc -> 1e-10 m/s^2
    public const double HbarCJm = 1.054571817e-34 * 2.99792458e8;   // hbar*c in J*m
    public const double GeVPerKg = 1.0 / 1.78266192e-27;
    public const double G_CODATA = 6.67430e-11;      // m^3 kg^-1 s^-2
    public const double G_SI = 6.6476e-11;           // AT value (QG181/QG182, 0.40% from CODATA)
    public const double MPlGeV = 1.223339e19;        // QG181

    // ── Astronomical lengths (IAU) and masses ────────────────────────────────────
    public const double Au = 1.495978707e11;         // m
    public const double Pc = 3.0856775814913673e16;  // m
    public const double Kpc = 1e3 * Pc;
    public const double Mpc = 1e6 * Pc;
    public const double Gpc = 1e9 * Pc;
    public const double MSun = 1.98892e30;           // kg
    public const double Year = 3.15576e7;            // s (Julian)

    /// <summary>AT's acceleration scale g† = c·H0/(2π) (QG080 / the RAR program) in m/s^2.</summary>
    public static double GDagger => (C_Kms * H0 / 1000.0) * Kms2PerKpcTo1e10 * 1e-10 / (2.0 * Math.PI);

    /// <summary>Hubble length c/H0 in metres (1/H0 with H0 in SI).</summary>
    public static double HubbleLength => C / (H0 * 1000.0 / Mpc);

    /// <summary>Cosmic circumference 2πc/H0 in metres (the closed-ring scale).</summary>
    public static double CosmicCircumference => 2.0 * Math.PI * HubbleLength;

    /// <summary>An acceleration expressed in units of standard gravity g.</summary>
    public static double InG(double acceleration) => acceleration / G_N;

    /// <summary>The mass whose Newtonian field at distance L equals deltaA: dM = deltaA*L^2/G (AT G).</summary>
    public static double EquivalentMass(double deltaA, double length) => deltaA * length * length / G_SI;
    // ── Gravitational parameters of the two calibration systems and MOND/RAR anchors ──
    public const double GM_Sun = 1.32712440018e20;   // m^3/s^2 (IAU)
    public const double GM_Earth = 3.986004418e14;   // m^3/s^2
    public const double R_Earth = 6.371e6;           // m (mean)
    public const double R_Gps = 2.66e7;              // m (GPS orbital radius)

    /// <summary>MOND/RAR acceleration scale, the project's literature set mean (Data/derived/A0OverCH_Distribution.csv).</summary>
    public const double A0_MOND_Literature = 1.200e-10;

    /// <summary>The project's COMBINED determination a0/cH0 = 0.1725 (Data/derived/A0OverCH_Distribution.csv).</summary>
    public const double A0OverCH_Combined = 0.1725;

    /// <summary>AT's parameter-free value of the same dimensionless ratio: g†/cH0 = 1/(2π).</summary>
    public static double A0OverCH_AT => 1.0 / (2.0 * Math.PI);

    /// <summary>Speed of light squared (m^2/s^2).</summary>
    public static double C2 => C * C;
}
