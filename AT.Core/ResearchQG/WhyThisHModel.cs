namespace AT.Core.ResearchQG;

/// <summary>QG-100 why-this-H model: the selection landscape. The cosmic age scales as t ~ 1/H,
/// and the processes (chemistry, stars, galaxies, complex life) each need a minimum age. This maps
/// the anthropic window in H/H0.</summary>
public sealed record HSelectionRow(double LogHH0, double AgeGyr, bool Chemistry, bool Stars, bool Galaxies, bool ComplexLife);

public static class WhyThisHModel
{
    public const double H0PerS = 67.4 / 3.0857e19; // 2.184e-18 s^-1
    public const double AgeGyrAtH0 = 13.8;

    // Minimum ages (Gyr) required for each process.
    public const double AgeChemistryGyr = 0.01;   // first stars ~ 1e8 yr; chemistry earlier
    public const double AgeStarsGyr = 0.1;        // first stars ~ 1e8 yr
    public const double AgeGalaxiesGyr = 0.5;     // galaxy assembly ~ 5e8 yr
    public const double AgeComplexLifeGyr = 3.0;  // ~ 3-4 Gyr (Earth-like timescale)

    /// <summary>Cosmic age for a given H (t ∝ 1/H, flat universe).</summary>
    public static double AgeGyr(double hPerS) => AgeGyrAtH0 * (H0PerS / hPerS);

    /// <summary>Selection landscape over log10(H/H0) ∈ [−6, +6].</summary>
    public static HSelectionRow[] Landscape()
    {
        var rows = new List<HSelectionRow>();
        for (int i = -60; i <= 60; i++)
        {
            double logHH0 = i / 10.0; // step 0.1 dex
            double h = H0PerS * Math.Pow(10, logHH0);
            double age = AgeGyr(h);
            rows.Add(new HSelectionRow(logHH0, age,
                age >= AgeChemistryGyr, age >= AgeStarsGyr, age >= AgeGalaxiesGyr, age >= AgeComplexLifeGyr));
        }
        return rows.ToArray();
    }

    /// <summary>The anthropic window (log10 H/H0 range) where ALL processes are possible.</summary>
    public static (double LogHH0Min, double LogHH0Max) AnthropicWindow()
    {
        // Complex life is the most restrictive: age >= 3 Gyr.
        // age = 13.8 * 10^-logHH0 >= 3 → logHH0 <= log10(13.8/3) = 0.66.
        // Also H not too small (no lower bound from age; the lower bound is set by structure formation,
        // but here we take the age-based bound: no upper limit on age).
        // Upper bound on H (min age): age >= 0.1 Gyr → logHH0 <= log10(13.8/0.1) = 2.14.
        double logMin = -6.0; // prior lower bound (structure formation, not age)
        double logMaxStars = Math.Log10(AgeGyrAtH0 / AgeStarsGyr);   // 2.14
        double logMaxLife = Math.Log10(AgeGyrAtH0 / AgeComplexLifeGyr); // 0.66
        return (logMin, logMaxLife); // complex-life window is the binding constraint
    }

    /// <summary>Probability a log-uniform H (over 12 decades) lands in the complex-life window.</summary>
    public static double AnthropicProbability()
    {
        var (lo, hi) = AnthropicWindow();
        double priorDex = 12.0; // log-uniform prior 1e-6 .. 1e6
        return (hi - lo) / priorDex;
    }
}
