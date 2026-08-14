using System.Globalization;

namespace TQM.Core.ResearchDATA;

/// <summary>
/// Acoustic Oscillator Audit — minimal tight-coupling solver for the first
/// CMB acoustic peak. Implements Theta0 (photon monopole) with baryon loading
/// and a constant gravitational potential Phi (adiabatic, matter era).
/// Theta1 follows from the monopole. No polarization, no lensing, no full C_l.
/// </summary>
public static class AcousticOscillatorAnalyzer
{
    // ── Derived cosmology (comoving, c = 1) ────────────────────────────────
    static readonly double h = RecombinationAnalyzer.H0KmS / 100.0;
    static readonly double H0Mpc = RecombinationAnalyzer.H0KmS / 2.99792458e5; // Mpc^-1
    static readonly double OmegaB = RecombinationAnalyzer.OmegaBh2 / (h * h);
    static readonly double OmegaM = RecombinationAnalyzer.OmegaMh2 / (h * h);
    static readonly double OmegaR = RecombinationAnalyzer.OmegaRh2 / (h * h);
    static readonly double OmegaL = 1.0 - OmegaM - OmegaR;
    static readonly double OmegaG = RecombinationAnalyzer.OmegaGammaH2 / (h * h);
    public static readonly double R0 = 3.0 * OmegaB / (4.0 * OmegaG);

    static double E(double a)
        => Math.Sqrt(OmegaM / (a * a * a) + OmegaR / (a * a * a * a) + OmegaL);

    /// <summary>Conformal Hubble parameter Htilde = a H in Mpc^-1.</summary>
    static double Htilde(double a) => a * H0Mpc * E(a);

    static double R(double a) => R0 * a;   // R = 3 rho_b / 4 rho_gamma ∝ a

    static double Cs2(double a) => 1.0 / (3.0 * (1.0 + R(a)));

    public static double ZStar() => RecombinationAnalyzer.Solve().ZStar;
    public static double AStar() => 1.0 / (1.0 + ZStar());

    /// <summary>Derivatives d/da for the tight-coupling oscillator.
    /// aH = a Htilde = a^2 H0 E(a).</summary>
    static void Derivs(double a, double th0, double th1, double k,
                       out double d0, out double d1)
    {
        double aH = a * Htilde(a);
        double Ra = R(a);
        d0 = th1 / aH;
        d1 = -Ra / (a * (1.0 + Ra)) * th1
           - k * k * Cs2(a) * th0 / aH
           - k * k / (3.0 * aH);
    }

    /// <summary>Integrate the tight-coupling oscillator to a_star.
    /// Returns (Theta0, Theta1) at a_star. Phi = const = 1.</summary>
    public static (double Th0, double Th1) Solve(
        double k, double aStar, double aInit = 1e-6, int steps = 20000)
    {
        double a = aInit;
        double th0 = -0.5;      // adiabatic IC: Theta0 = -Phi/2, Phi = 1
        double th1 = 0.0;       // dTheta0/deta (super-horizon ~ 0)

        double da = (aStar - aInit) / steps;
        for (int i = 0; i < steps; i++)
        {
            Derivs(a, th0, th1, k, out var k1, out var k1p);
            double a2 = a + 0.5 * da;
            Derivs(a2, th0 + 0.5 * da * k1, th1 + 0.5 * da * k1p, k, out var k2, out var k2p);
            Derivs(a2, th0 + 0.5 * da * k2, th1 + 0.5 * da * k2p, k, out var k3, out var k3p);
            double a3 = a + da;
            Derivs(a3, th0 + da * k3, th1 + da * k3p, k, out var k4, out var k4p);

            th0 += da * (k1 + 2 * k2 + 2 * k3 + k4) / 6.0;
            th1 += da * (k1p + 2 * k2p + 2 * k3p + k4p) / 6.0;
            a += da;
        }

        double th1_phys = -3.0 * th1 / k; // Theta1 = -3 Theta0'/k (Phi' = 0)
        return (th0, th1_phys);
    }

    public sealed record PeakResult(
        double KPeak, double LPeak, double SwPeak, double SwPlateau, double Ratio);

    /// <summary>Find the first acoustic peak = first local maximum of |Theta0 + Phi|
    /// (the power spectrum). Returns the SW compression peak.</summary>
    public static PeakResult FindFirstPeak()
    {
        double aStar = AStar();
        double dM = RecombinationAnalyzer.ComovingDistance(ZStar()) / RecombinationAnalyzer.Mpc; // Mpc

        int nk = 4000;
        double kMin = 1e-4, kMax = 0.06;
        double dk = (kMax - kMin) / nk;

        var f = new double[nk + 1];
        for (int i = 0; i <= nk; i++)
        {
            double k = kMin + i * dk;
            f[i] = Math.Abs(Solve(k, aStar).Th0 + 1.0);  // |SW temperature|
        }

        double kPeak = 0, ampPeak = 0;
        bool found = false;
        for (int i = 1; i < nk; i++)
        {
            if (f[i] > f[i - 1] && f[i] >= f[i + 1])
            {
                if (!found) { kPeak = kMin + i * dk; ampPeak = f[i]; found = true; }
            }
        }

        double plateau = 0.5; // |Theta0 + Phi| = Phi/2 on the SW plateau
        return new PeakResult(kPeak, kPeak * dM, ampPeak, plateau, ampPeak / plateau);
    }
}
