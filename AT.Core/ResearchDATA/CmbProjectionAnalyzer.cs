using MathNet.Numerics;

namespace AT.Core.ResearchDATA;

/// <summary>
/// CMB Projection Audit — minimal line-of-sight projection to the first peak.
/// Projects the acoustic oscillator source (SW + Doppler) onto the sky with
/// spherical Bessel functions. No polarization, no lensing, no ISW.
/// </summary>
public static class CmbProjectionAnalyzer
{
    public static double ZStar() => RecombinationAnalyzer.Solve().ZStar;
    public static double DM() => RecombinationAnalyzer.ComovingDistance(ZStar()) / RecombinationAnalyzer.Mpc; // Mpc

    /// <summary>Spherical Bessel j_l(x).</summary>
    public static double SphericalBesselJ(int l, double x)
    {
        if (x < 1e-12)
            return l == 0 ? 1.0 : 0.0;
        return Math.Sqrt(Math.PI / (2.0 * x)) * SpecialFunctions.BesselJ(l + 0.5, x);
    }

    /// <summary>Spherical Bessel derivative j_l'(x) via recurrence.</summary>
    public static double SphericalBesselJPrime(int l, double x)
    {
        if (l == 0)
            return -SphericalBesselJ(1, x);
        if (x < 1e-12) return 0.0;
        return SphericalBesselJ(l - 1, x) - (l + 1.0) / x * SphericalBesselJ(l, x);
    }

    /// <summary>Combined transfer power at multipole l via the Limber
    /// approximation (l = k D_M): T^2 = S^2 + v_b^2 (Doppler optional).</summary>
    public static double TransferPower(double l, bool includeDoppler)
    {
        double k = l / DM();                      // Limber: k = l / D_M
        double aStar = 1.0 / (1.0 + ZStar());
        var (th0, th1) = AcousticOscillatorAnalyzer.Solve(k, aStar);
        double s = th0 + 1.0;                     // SW source S = Theta0 + Psi
        double vb = th1;                          // Doppler velocity v_b = Theta1
        double p = s * s;
        if (includeDoppler) p += vb * vb;
        return p;
    }

    /// <summary>First local maximum of the transfer power T^2 over [lMin, lMax].</summary>
    public static (double lPeak, double dPeak) FirstPeak(
        bool includeDoppler, int lMin = 40, int lMax = 500, int dl = 2)
    {
        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        for (int l = lMin; l <= lMax; l += dl)
        {
            double d = TransferPower(l, includeDoppler);
            if (prev1 > prev2 && prev1 >= d && l - dl > lMin)
                return (l - dl, prev1);   // prev1 is the first local max
            prev2 = prev1; prev1 = d;
        }
        return (lMax, prev1);
    }
}
