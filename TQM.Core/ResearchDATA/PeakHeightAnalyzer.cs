using System.Globalization;

namespace TQM.Core.ResearchDATA;

/// <summary>
/// CMB Peak Height Audit — first acoustic peak amplitude.
/// Adds radiation driving (evolving Phi via the Poisson equation) and Silk
/// damping (photon diffusion) to the acoustic oscillator, then estimates the
/// first-peak amplitude D_l1. No polarization, no lensing, no full C_l.
/// </summary>
public static class PeakHeightAnalyzer
{
    public const double As = 2.105e-9;               // Planck scalar amplitude
    public const double TcmbMicroK = 2.7255e6;       // K -> micro-K
    const double Mpc = 3.08567758149e22;             // m
    const double Gsi = 6.67430e-11;                  // m^3 kg^-1 s^-2
    const double Mh = 1.6735575e-27;                 // kg (hydrogen atom)

    static readonly double h = RecombinationAnalyzer.H0KmS / 100.0;
    static readonly double H0Mpc = RecombinationAnalyzer.H0KmS / 2.99792458e5; // Mpc^-1
    static readonly double Ob = RecombinationAnalyzer.OmegaBh2 / (h * h);
    static readonly double Om = RecombinationAnalyzer.OmegaMh2 / (h * h);
    static readonly double Or = RecombinationAnalyzer.OmegaRh2 / (h * h);
    static readonly double Ol = 1.0 - Om - Or;
    static readonly double Og = RecombinationAnalyzer.OmegaGammaH2 / (h * h);
    static readonly double Onu = Or - Og;   // neutrino density (N_eff ~ 3.04)
    static readonly double R0 = 3.0 * Ob / (4.0 * Og);

    // Baryon number density today (m^-3), hydrogen-only.
    static readonly double H0s = RecombinationAnalyzer.H0KmS * 1e3 / Mpc;
    static readonly double RhoCrit = 3.0 * H0s * H0s / (8.0 * Math.PI * Gsi);
    static readonly double NH0 = Ob * RhoCrit / Mh;

    static double E(double a) => Math.Sqrt(Om / (a * a * a) + Or / (a * a * a * a) + Ol);
    static double Htilde(double a) => a * H0Mpc * E(a);   // Mpc^-1
    static double R(double a) => R0 * a;

    public static double ZStar() => RecombinationAnalyzer.Solve().ZStar;
    public static double AStar() => 1.0 / (1.0 + ZStar());
    public static double DM() => RecombinationAnalyzer.ComovingDistance(ZStar()) / Mpc;

    /// <summary>Full tight-coupling system with evolving Phi (radiation driving).
    /// 5 ODEs: Theta0, Theta1, delta_matter, v_matter, Phi (0i Einstein eq.).</summary>
    public static (double Th0, double Th1, double Phi, double Dm) FullSolve(
        double k, double aStar, double aInit = 1e-6, int steps = 20000)
    {
        double a = aInit;
        double th0 = -0.5, th1 = 0.0, dm = -1.5, vm = 0.0, phi = 1.0;  // adiabatic IC

        double da = (aStar - aInit) / steps;
        for (int i = 0; i < steps; i++)
        {
            void RHS(double aa, double t0, double t1, double d, double v, double p,
                     out double dt0, out double dt1, out double dd, out double dv, out double dp)
            {
                double aH = aa * Htilde(aa);
                double Ra = R(aa);
                double h2 = H0Mpc * H0Mpc;
                // 0i Einstein: Phi' = (4piG a^2)[rho_m v_m + (4/3)rho_g Theta1]/k - H Phi
                double pdot = (1.5 * h2 * Om * v / aa + 2.0 * h2 * Og * t1 / (aa * aa)) / k
                            - Htilde(aa) * p;
                dt0 = (-k * t1 / 3.0 - pdot) / aH;
                dt1 = (k * t0 + k * (1.0 + Ra) * p - Ra * Htilde(aa) * t1) / ((1.0 + Ra) * aH);
                dd = (-k * v - 3.0 * pdot) / aH;
                dv = (-Htilde(aa) * v + k * p) / aH;
                dp = pdot / aH;
            }

            RHS(a, th0, th1, dm, vm, phi, out var k1t0, out var k1t1, out var k1d, out var k1v, out var k1p);
            RHS(a + 0.5 * da, th0 + 0.5 * da * k1t0, th1 + 0.5 * da * k1t1, dm + 0.5 * da * k1d, vm + 0.5 * da * k1v, phi + 0.5 * da * k1p, out var k2t0, out var k2t1, out var k2d, out var k2v, out var k2p);
            RHS(a + 0.5 * da, th0 + 0.5 * da * k2t0, th1 + 0.5 * da * k2t1, dm + 0.5 * da * k2d, vm + 0.5 * da * k2v, phi + 0.5 * da * k2p, out var k3t0, out var k3t1, out var k3d, out var k3v, out var k3p);
            RHS(a + da, th0 + da * k3t0, th1 + da * k3t1, dm + da * k3d, vm + da * k3v, phi + da * k3p, out var k4t0, out var k4t1, out var k4d, out var k4v, out var k4p);

            th0 += da * (k1t0 + 2 * k2t0 + 2 * k3t0 + k4t0) / 6.0;
            th1 += da * (k1t1 + 2 * k2t1 + 2 * k3t1 + k4t1) / 6.0;
            dm  += da * (k1d + 2 * k2d + 2 * k3d + k4d) / 6.0;
            vm  += da * (k1v + 2 * k2v + 2 * k3v + k4v) / 6.0;
            phi += da * (k1p + 2 * k2p + 2 * k3p + k4p) / 6.0;
            a += da;
        }
        return (th0, th1, phi, dm);
    }

    /// <summary>Full system with free-streaming neutrinos (7 ODEs). Neutrino
    /// density is damped by the free-streaming factor 3 j1(x)/x.</summary>
    public static (double Th0, double Th1, double Phi) FullSolveNu(
        double k, double aStar, double aInit = 1e-6, int steps = 20000)
    {
        double a = aInit;
        double th0 = -0.5, th1 = 0.0, dm = -1.5, vm = 0.0, phi = 1.0;
        double dnu = -2.0, vnu = 0.0;   // adiabatic: dnu = dgamma = 4 Th0 = -2 Phi

        double da = (aStar - aInit) / steps;
        for (int i = 0; i < steps; i++)
        {
            void RHS(double aa, double t0, double t1, double d, double v, double p,
                     double dn, double vn,
                     out double dt0, out double dt1, out double dd, out double dv,
                     out double dp, out double ddn, out double dvn)
            {
                double aH = aa * Htilde(aa);
                double Ra = R(aa);
                double h2 = H0Mpc * H0Mpc;
                // conformal time (Mpc), matter+radiation closed form (Lambda negligible early)
                double eta = 2.0 / (H0Mpc * Om) * (Math.Sqrt(Om * aa + Or) - Math.Sqrt(Or));
                double x = k * eta;
                double fs = x < 1e-6 ? 1.0 : 3.0 * (Math.Sin(x) - x * Math.Cos(x)) / (x * x * x);
                // 0i Einstein with photon + matter + (free-streaming-damped) neutrino
                double pdot = (1.5 * h2 * Om * v / aa + 2.0 * h2 * Og * t1 / (aa * aa)
                             + 2.0 * h2 * Onu * vn * fs / (aa * aa)) / k - Htilde(aa) * p;
                dt0 = (-k * t1 / 3.0 - pdot) / aH;
                dt1 = (k * t0 + k * (1.0 + Ra) * p - Ra * Htilde(aa) * t1) / ((1.0 + Ra) * aH);
                dd = (-k * v - 3.0 * pdot) / aH;
                dv = (-Htilde(aa) * v + k * p) / aH;
                dp = pdot / aH;
                ddn = (-4.0 / 3.0 * k * vn - 4.0 * pdot) / aH;
                dvn = (k / 4.0 * dn + k * p) / aH;
            }

            RHS(a, th0, th1, dm, vm, phi, dnu, vnu, out var k1t0, out var k1t1, out var k1d, out var k1v, out var k1p, out var k1n, out var k1vn);
            RHS(a + 0.5 * da, th0 + 0.5 * da * k1t0, th1 + 0.5 * da * k1t1, dm + 0.5 * da * k1d, vm + 0.5 * da * k1v, phi + 0.5 * da * k1p, dnu + 0.5 * da * k1n, vnu + 0.5 * da * k1vn, out var k2t0, out var k2t1, out var k2d, out var k2v, out var k2p, out var k2n, out var k2vn);
            RHS(a + 0.5 * da, th0 + 0.5 * da * k2t0, th1 + 0.5 * da * k2t1, dm + 0.5 * da * k2d, vm + 0.5 * da * k2v, phi + 0.5 * da * k2p, dnu + 0.5 * da * k2n, vnu + 0.5 * da * k2vn, out var k3t0, out var k3t1, out var k3d, out var k3v, out var k3p, out var k3n, out var k3vn);
            RHS(a + da, th0 + da * k3t0, th1 + da * k3t1, dm + da * k3d, vm + da * k3v, phi + da * k3p, dnu + da * k3n, vnu + da * k3vn, out var k4t0, out var k4t1, out var k4d, out var k4v, out var k4p, out var k4n, out var k4vn);

            th0 += da * (k1t0 + 2 * k2t0 + 2 * k3t0 + k4t0) / 6.0;
            th1 += da * (k1t1 + 2 * k2t1 + 2 * k3t1 + k4t1) / 6.0;
            dm  += da * (k1d + 2 * k2d + 2 * k3d + k4d) / 6.0;
            vm  += da * (k1v + 2 * k2v + 2 * k3v + k4v) / 6.0;
            phi += da * (k1p + 2 * k2p + 2 * k3p + k4p) / 6.0;
            dnu += da * (k1n + 2 * k2n + 2 * k3n + k4n) / 6.0;
            vnu += da * (k1vn + 2 * k2vn + 2 * k3vn + k4vn) / 6.0;
            a += da;
        }
        return (th0, th1, phi);
    }

    /// <summary>Silk damping scale k_D (Mpc^-1) and factor exp(-k^2/k_D^2).
    /// X_e = 1 before recombination (the dominant contribution).</summary>
    public static (double kD, double silk) SilkDamping(double k, double aStar)
    {
        int steps = 20000;
        double aInit = 1e-6;
        double da = (aStar - aInit) / steps;
        double sum = 0.0;
        for (int i = 0; i <= steps; i++)
        {
            double a = aInit + i * da;
            double Ra = R(a);
            double tauDot = NH0 * RecombinationAnalyzer.SigmaT * Mpc / (a * a); // Mpc^-1
            double f = (1.0 / 6.0) * (Ra * Ra + 16.0 * (1.0 + Ra) / 15.0)
                     / ((1.0 + Ra) * (1.0 + Ra) * tauDot);
            double dEtaDa = 1.0 / (a * Htilde(a));   // Mpc
            double w = (i == 0 || i == steps) ? 1.0 : (i % 2 == 0 ? 2.0 : 4.0);
            sum += w * f * dEtaDa;
        }
        double kD2inv = da * sum / 3.0;
        double kD = 1.0 / Math.Sqrt(kD2inv);
        return (kD, Math.Exp(-k * k / (kD * kD)));
    }

    /// <summary>First-peak amplitude D_l1 (micro-K^2) = (9/25) A_s T_cmb^2 (S^2+v_b^2) Silk.</summary>
    public static (double lPeak, double dPeak, double s2, double vb2, double silk,
                   double phi, double kD) FirstPeakAmplitude(
        int lMin = 40, int lMax = 500, int dl = 2)
    {
        double aStar = AStar();
        double dM = DM();

        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double s2 = 0, vb2 = 0, lPeak = 0, phiAtPeak = 0;

        for (int l = lMin; l <= lMax; l += dl)
        {
            double k = l / dM;
            var (th0, th1, phi, _) = FullSolve(k, aStar);
            double p = (th0 + phi) * (th0 + phi) + th1 * th1;
            if (prev1 > prev2 && prev1 >= p && l - dl > lMin)
            {
                lPeak = l - dl; phiAtPeak = phi; break;
            }
            prev2 = prev1; prev1 = p;
            s2 = (th0 + phi) * (th0 + phi);
            vb2 = th1 * th1;
        }

        double kPeak = lPeak / dM;
        var (kd, silk) = SilkDamping(kPeak, aStar);
        double norm = (9.0 / 25.0) * As * TcmbMicroK * TcmbMicroK;
        double dPeak = norm * (s2 + vb2) * silk;
        return (lPeak, dPeak, s2, vb2, silk, phiAtPeak, kd);
    }

    /// <summary>First-peak amplitude WITH free-streaming neutrinos.</summary>
    public static (double lPeak, double dPeak, double phi) FirstPeakAmplitudeNu(
        int lMin = 40, int lMax = 500, int dl = 2)
    {
        double aStar = AStar();
        double dM = DM();

        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double s2 = 0, vb2 = 0, lPeak = 0, phiAtPeak = 0;

        for (int l = lMin; l <= lMax; l += dl)
        {
            double k = l / dM;
            var (th0, th1, phi) = FullSolveNu(k, aStar);
            double p = (th0 + phi) * (th0 + phi) + th1 * th1;
            if (prev1 > prev2 && prev1 >= p && l - dl > lMin)
            {
                lPeak = l - dl; phiAtPeak = phi; break;
            }
            prev2 = prev1; prev1 = p;
            s2 = (th0 + phi) * (th0 + phi);
            vb2 = th1 * th1;
        }

        double kPeak = lPeak / dM;
        var (kd, silk) = SilkDamping(kPeak, aStar);
        double norm = (9.0 / 25.0) * As * TcmbMicroK * TcmbMicroK;
        double dPeak = norm * (s2 + vb2) * silk;
        return (lPeak, dPeak, phiAtPeak);
    }

    /// <summary>First N local maxima of D_l = norm (S^2 + v_b^2) Silk.</summary>
    public static List<(double l, double d, double s2, double vb2)> FindPeaks(
        int count, int lMin = 40, int lMax = 900, int dl = 2)
    {
        double aStar = AStar();
        double dM = DM();
        double norm = (9.0 / 25.0) * As * TcmbMicroK * TcmbMicroK;
        var peaks = new List<(double, double, double, double)>();
        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double ps2 = 0, pvb2 = 0, ps2prev = 0, pvb2prev = 0;

        for (int l = lMin; l <= lMax; l += dl)
        {
            double k = l / dM;
            var (th0, th1, phi) = FullSolveNu(k, aStar);
            double s2 = (th0 + phi) * (th0 + phi);
            double vb2 = th1 * th1;
            var (kd, silk) = SilkDamping(k, aStar);
            double d = norm * (s2 + vb2) * silk;

            if (prev1 > prev2 && prev1 >= d && l - dl > lMin && peaks.Count < count)
                peaks.Add((l - dl, prev1, ps2prev, pvb2prev));

            prev2 = prev1; prev1 = d;
            ps2prev = ps2; pvb2prev = pvb2;
            ps2 = s2; pvb2 = vb2;
        }
        return peaks;
    }

    /// <summary>Correct Doppler projection weight.
    /// Under the LOS projection with measure d(ln k):
    ///   w_D = Int d(ln k) j_l'^2 / Int d(ln k) j_l^2 = 1/3
    /// (the dipole/monopole angular-average ratio). This replaces the naive
    /// weight 1 used in the original quadrature.</summary>
    public static double DopplerProjectionWeight() => 1.0 / 3.0;

    /// <summary>SW-Doppler cross-term weight.
    /// The monopole (SW) and dipole (Doppler) transfer functions enter the LOS
    /// integral with a relative phase -i (e^{ik.mu.D} -> i^-l vs i^-(l-1)), so
    /// the interference 2 S v_b Re[-i j_l j_l'] is exactly zero. The cross term
    /// cannot fill the rarefaction peak.</summary>
    public static double CrossTermWeight() => 0.0;

    /// <summary>Doppler visibility damping D_v(k) = exp(-k^2 c_s^2 sigma_eta^2/2).
    /// The finite recombination width averages the velocity v_b(eta) over the
    /// visibility function g(eta), suppressing the Doppler term relative to the
    /// SW offset (which does not oscillate and survives the averaging).</summary>
    public static double DopplerVisibilityDamping(double k)
    {
        var (sigmaEta, _) = RecombinationAnalyzer.VisibilityWidth();
        double cs = 1.0 / Math.Sqrt(3.0 * (1.0 + R(AStar())));   // sound speed / c at z*
        return Math.Exp(-0.5 * k * k * cs * cs * sigmaEta * sigmaEta);
    }

    /// <summary>Density extrema (acoustic peaks) with the full projection:
    /// D_l = S^2 + (1/3) D_v(k)^2 v_b^2, where D_v is the Doppler visibility
    /// damping. Returns (l, T^2, S^2, v_b^2, D_v) at each extremum.</summary>
    public static List<(double l, double t2, double s2, double vb2, double dv)> FindAcousticPeaksVisible(
        int count, int lMin = 60, int lMax = 960, int dl = 2)
    {
        double aStar = AStar();
        double dM = DM();
        var peaks = new List<(double, double, double, double, double)>();
        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double ps2 = 0, pvb2 = 0, pt2 = 0, pdv = 0;

        for (int l = lMin; l <= lMax; l += dl)
        {
            double k = l / dM;
            var (th0, th1, phi) = FullSolveNu(k, aStar);
            double s2 = (th0 + phi) * (th0 + phi);
            double vb2 = th1 * th1;
            double dv = DopplerVisibilityDamping(k);
            double t2 = s2 + (1.0 / 3.0) * dv * dv * vb2;
            if (prev1 > prev2 && prev1 >= s2 && l - dl > lMin && peaks.Count < count)
                peaks.Add((l - dl, pt2, ps2, pvb2, pdv));
            prev2 = prev1; prev1 = s2;
            ps2 = s2; pvb2 = vb2; pt2 = t2; pdv = dv;
        }
        return peaks;
    }

    /// <summary>Acoustic peaks = local maxima of |S| = |Theta0 + Phi| (density extrema).
    /// Returns (l, T^2 = S^2 + w*v_b^2, S^2, v_b^2) at each extremum, where w is
    /// the Doppler projection weight (1/3 correct, 1 = naive quadrature).</summary>
    public static List<(double l, double t2, double s2, double vb2)> FindAcousticPeaks(
        int count, int lMin = 60, int lMax = 960, int dl = 2, double dopplerWeight = 1.0)
    {
        double aStar = AStar();
        double dM = DM();
        var peaks = new List<(double, double, double, double)>();
        double prev2 = double.NegativeInfinity, prev1 = double.NegativeInfinity;
        double prevS2 = 0, prevVb2 = 0, prevT2 = 0;

        for (int l = lMin; l <= lMax; l += dl)
        {
            double k = l / dM;
            var (th0, th1, phi) = FullSolveNu(k, aStar);
            double s2 = (th0 + phi) * (th0 + phi);
            double vb2 = th1 * th1;
            if (prev1 > prev2 && prev1 >= s2 && l - dl > lMin && peaks.Count < count)
                peaks.Add((l - dl, prevT2, prevS2, prevVb2));
            prev2 = prev1; prev1 = s2;
            prevS2 = s2; prevVb2 = vb2; prevT2 = s2 + dopplerWeight * vb2;
        }
        return peaks;
    }
}
