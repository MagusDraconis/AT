using System.Globalization;
using System.Text;

namespace AT.Core.ResearchQG;

/// <summary>
/// Neutrino-Koide audit: assume Koide Q=2/3 is a flavor constraint, derive the neutrino-mass
/// implications, and compute the likelihood shift if neutrino-Koide holds. Solves for the lightest
/// neutrino mass (both orderings) such that Q_ν = 2/3, given the measured mass-squared differences.
/// </summary>
public static class NeutrinoKoideAnalyzer
{
    // Mass-squared differences (eV²).
    public const double Dm21 = 7.53e-5;    // solar
    public const double Dm32 = 2.453e-3;   // atmospheric (|Δm²_3l|)
    public const double Dm31 = Dm21 + Dm32; // 2.528e-3

    /// <summary>Koide Q for three masses (eV).</summary>
    public static double KoideQ(double m1, double m2, double m3)
    {
        double s = Math.Sqrt(m1) + Math.Sqrt(m2) + Math.Sqrt(m3);
        return (m1 + m2 + m3) / (s * s);
    }

    /// <summary>Masses for a given lightest mass and ordering.</summary>
    public static (double m1, double m2, double m3) Masses(double mLight, bool normalOrdering)
    {
        if (normalOrdering)
            return (mLight, Math.Sqrt(mLight * mLight + Dm21), Math.Sqrt(mLight * mLight + Dm31));
        else // inverted: m3 lightest, m1 middle, m2 heaviest
            return (Math.Sqrt(mLight * mLight + Dm31), Math.Sqrt(mLight * mLight + Dm31 + Dm21), mLight);
    }

    /// <summary>Residual Q − 2/3 for a given lightest mass.</summary>
    public static double Residual(double mLight, bool normalOrdering)
    {
        var (m1, m2, m3) = Masses(mLight, normalOrdering);
        return KoideQ(m1, m2, m3) - 2.0 / 3.0;
    }

    /// <summary>Bisection solve for the lightest mass giving Q=2/3 (or null if none in range).</summary>
    public static double? SolveLightestMass(bool normalOrdering, double lo = 1e-6, double hi = 0.5)
    {
        double flo = Residual(lo, normalOrdering);
        double fhi = Residual(hi, normalOrdering);
        if (flo * fhi > 0) return null; // no sign change → no solution in range
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            double fm = Residual(mid, normalOrdering);
            if (Math.Abs(fm) < 1e-12) return mid;
            if (flo * fm < 0) { hi = mid; fhi = fm; } else { lo = mid; flo = fm; }
        }
        return 0.5 * (lo + hi);
    }

    /// <summary>Maximum Q achievable as m_light → 0 (the most hierarchical limit).</summary>
    public static double MaxQ(bool normalOrdering)
    {
        var (m1, m2, m3) = Masses(1e-9, normalOrdering);
        return KoideQ(m1, m2, m3);
    }

    /// <summary>Likelihood shift (Bayes factor) if neutrino-Koide holds:
    /// BF(all-lepton : charged-only) = 1 / P(second coincidence) ≈ 1/precision.</summary>
    public static double LikelihoodShift(double precision = 1e-5) => 1.0 / precision;
}
