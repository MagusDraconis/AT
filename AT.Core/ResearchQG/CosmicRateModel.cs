namespace AT.Core.ResearchQG;

/// <summary>QG-089 rate-first cosmology: the fundamental cosmic rate R(t) and the
/// H ↔ a bijection. H(z) and a(z) = 1/(1+z) carry identical information (up to the
/// a(0)=1 normalization), so "rate-first" and "scale-factor-first" are equivalent.</summary>
public sealed record CosmicRatePoint(double Z, double Rate, double ScaleFactor, double TimeGyr);

public static class CosmicRateModel
{
    public const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    public const double C = 299792458.0;
    public static double H0PerS => H0 / 3.0857e19; // 2.184e-18 s^-1

    public static double Rate(double z) => H0PerS * Math.Sqrt(OmM * Math.Pow(1 + z, 3) + OmL);

    public static double ScaleFactor(double z) => 1.0 / (1.0 + z);

    /// <summary>Age t(z) in Gyr (numerical: t = ∫ dz'/((1+z')H(z'))).</summary>
    public static double AgeGyr(double z, int n = 20000)
    {
        double result = 0;
        double dz = z / n;
        for (int i = 0; i < n; i++)
        {
            double z1 = i * dz, z2 = (i + 1) * dz;
            double f1 = 1.0 / ((1 + z1) * Rate(z1));
            double f2 = 1.0 / ((1 + z2) * Rate(z2));
            result += 0.5 * (f1 + f2) * dz;
        }
        return result / (3.15576e7 * 1e9); // s → Gyr
    }

    public static CosmicRatePoint[] Points(double[] zs)
        => zs.Select(z => new CosmicRatePoint(z, Rate(z), ScaleFactor(z), AgeGyr(z))).ToArray();
}
