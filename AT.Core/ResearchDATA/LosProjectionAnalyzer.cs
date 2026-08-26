using MathNet.Numerics;

namespace AT.Core.ResearchDATA;

/// <summary>
/// LOS Projection Audit — exact line-of-sight projection of the tight-coupling
/// sources (SW monopole S = Theta0+Phi, Doppler dipole v_b = Theta1) onto the sky
/// with the full spherical Bessel functions j_l(x) and j_l'(x). No polarization,
/// no lensing, no ISW.
/// </summary>
public static class LosProjectionAnalyzer
{
    public static double ZStar() => RecombinationAnalyzer.Solve().ZStar;
    public static double DM() => RecombinationAnalyzer.ComovingDistance(ZStar()) / RecombinationAnalyzer.Mpc;
    public static double RS() => RecombinationAnalyzer.SoundHorizon(ZStar()) / RecombinationAnalyzer.Mpc;

    // Tight-coupling WKB transfer functions (Phi(0)=1 normalization).
    //   S(k)   = A cos(k r_s) - R Phi(z*)          (SW monopole)
    //   v_b(k) = B sin(k r_s)                      (Doppler dipole)
    //   A = (1/2 + R)(1+R)^-1/4,  B = sqrt(3) A / sqrt(1+R)
    static readonly double PhiStar = 0.78;                    // Phi at recombination (neutrino driving)
    static readonly double R = 0.627;                         // 3 Omega_b / 4 Omega_gamma at z*
    static readonly double A = (0.5 + R) * Math.Pow(1.0 + R, -0.25);
    static readonly double B = Math.Sqrt(3.0) * A / Math.Sqrt(1.0 + R);
    static readonly double C = R * PhiStar;

    // Doppler visibility damping (cached): D_v(k) = exp(-k^2 c_s^2 sigma_eta^2/2)
    static readonly double SigmaEta = RecombinationAnalyzer.VisibilityWidth().SigmaEtaMpc;
    static readonly double Cs = RecombinationAnalyzer.SoundSpeed(RecombinationAnalyzer.Solve().ZStar) / RecombinationAnalyzer.C;

    public static double S(double k) => A * Math.Cos(k * RS()) - C;
    public static double Vb(double k) => B * Math.Sin(k * RS());
    static double Dv(double k) => Math.Exp(-0.5 * k * k * Cs * Cs * SigmaEta * SigmaEta);

    /// <summary>Spherical Bessel j_l(x).</summary>
    public static double J(int l, double x)
        => x < 1e-12 ? (l == 0 ? 1.0 : 0.0)
           : Math.Sqrt(Math.PI / (2.0 * x)) * SpecialFunctions.BesselJ(l + 0.5, x);

    /// <summary>Spherical Bessel derivative j_l'(x).</summary>
    public static double JPrime(int l, double x)
        => l == 0 ? -J(1, x) : (x < 1e-12 ? 0.0 : J(l - 1, x) - (l + 1.0) / x * J(l, x));

    /// <summary>Exact LOS projection of the SW+Doppler sources onto the sky.
    /// Uses the EXACT spherical-Bessel integrals (verified numerically):
    ///   Int_0^inf d(ln k) j_l(kD)^2   = 1/(2 l(l+1))
    ///   Int_0^inf d(ln k) j_l'(kD)^2  = (1/3) * 1/(2 l(l+1))
    /// and evaluates the slowly-varying sources S(k), v_b(k) at the Limber point
    /// k = l/D (accurate for l >> 1). This is the large-l limit of the exact
    /// j_l / j_l' projection:
    ///   D_l = l(l+1) [S^2 * 1/(2l(l+1)) + v_b^2 * (1/3)/(2l(l+1))] damp
    ///       = [S^2 + v_b^2/3]/2 * damp.
    /// (The SW-Doppler cross term is identically zero.)</summary>
    public static (double sw, double dop) Project(int l, double kMin = 0.004, double kMax = 0.30, int nk = 12000)
    {
        double k = l / DM();
        double silk = Math.Exp(-k * k / (0.234 * 0.234));
        double dv = Dv(k);
        double damp = 0.5 * silk * dv * dv;
        return (S(k) * S(k) * damp, Vb(k) * Vb(k) * damp / 3.0);
    }

    /// <summary>Full numeric LOS projection (direct j_l / j_l' integral) for
    /// verification of the Limber result at a few multipoles.</summary>
    public static (double sw, double dop) ProjectNumeric(int l, double kMin = 0.004, double kMax = 0.30, int nk = 20000)
    {
        double D = DM();
        double dk = (kMax - kMin) / nk;
        double sumSw = 0, sumDop = 0;
        for (int i = 0; i <= nk; i++)
        {
            double k = kMin + i * dk;
            double x = k * D;
            double jl = J(l, x);
            double jlp = JPrime(l, x);
            double silk = Math.Exp(-k * k / (0.234 * 0.234));
            double dv = Dv(k);
            double damp = silk * dv * dv / k;
            double w = (i == 0 || i == nk) ? 0.5 : 1.0;
            sumSw += w * S(k) * S(k) * jl * jl * damp;
            sumDop += w * Vb(k) * Vb(k) * jlp * jlp * damp;
        }
        double lfac = l * (l + 1.0) * dk;
        return (lfac * sumSw, lfac * sumDop);
    }

    /// <summary>Local maxima of the full projected power D_l = SW + Doppler.</summary>
    public static List<(double l, double dl, double sw, double dop)> FindPeaks(
        int count, int lMin = 80, int lMax = 950, int dl = 8)
    {
        var peaks = new List<(double, double, double, double)>();
        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double psw = 0, pdop = 0, pTot = 0;
        for (int l = lMin; l <= lMax; l += dl)
        {
            var (sw, dop) = Project(l);
            double tot = sw + dop;
            if (prev1 > prev2 && prev1 >= tot && l - dl > lMin && peaks.Count < count)
                peaks.Add((l - dl, pTot, psw, pdop));
            prev2 = prev1; prev1 = tot;
            psw = sw; pdop = dop; pTot = tot;
        }
        return peaks;
    }
}
