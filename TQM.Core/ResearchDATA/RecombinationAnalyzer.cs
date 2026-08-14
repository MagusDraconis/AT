using System.Globalization;

namespace TQM.Core.ResearchDATA;

/// <summary>
/// Recombination Audit — minimal z* solver.
///
/// Computes the free-electron fraction X_e(z) and the decoupling redshift z*
/// using the standard Saha equation (equilibrium) and the Peebles correction
/// (non-equilibrium). Hydrogen-only; helium recombination is omitted.
///
/// This is standard Lambda-CDM recombination physics — IMPORTED, not derived
/// from TQM primitives. It closes the "smallest missing module" identified in
/// the Acoustic Gap Audit (the recombination redshift z*).
/// </summary>
public static class RecombinationAnalyzer
{
    // ── Physical constants (SI) ────────────────────────────────────────────
    public const double Tcmb0 = 2.7255;           // K
    public const double H0KmS = 67.36;            // km/s/Mpc (Planck 2018)
    public const double C = 2.99792458e8;         // m/s
    public const double G = 6.67430e-11;          // m^3 kg^-1 s^-2
    public const double KB = 1.380649e-23;        // J/K
    public const double Hbar = 1.054571817e-34;   // J s
    public const double MeV = 9.10938370e-31;     // kg (electron)
    public const double Mp = 1.67262192e-27;      // kg (proton)
    public const double Mh = 1.6735575e-27;       // kg (hydrogen atom)
    public const double Mpc = 3.08567758149e22;   // m (megaparsec)
    public const double Ev = 1.602176634e-19;     // J (1 eV)
    public const double SigmaT = 6.6524587321e-29;// m^2 (Thomson)
    public const double Lambda2s1s = 8.22458;     // s^-1 (two-photon decay)
    public const double LambdaAlpha = 1.215668e-7;// m (Ly-alpha)

    // ── Cosmological parameters (Planck 2018) ─────────────────────────────
    public const double OmegaBh2 = 0.02237;
    public const double OmegaMh2 = 0.1430;
    public const double OmegaRh2 = 4.183e-5;
    public const double OmegaGammaH2 = 2.469e-5; // photon density (T_CMB = 2.7255 K)

    // ── Hydrogen energies ──────────────────────────────────────────────────
    public const double EIon = 13.6057 * Ev;      // J (1s ionization)
    public const double E2s = 3.3995 * Ev;        // J (2s ionization, for beta^(2))

    // ── Derived quantities ─────────────────────────────────────────────────
    static readonly double H0 = H0KmS * 1e3 / 3.08567758149e22; // s^-1
    static readonly double H = H0KmS / 100.0;
    static readonly double OmegaB = OmegaBh2 / (H * H);
    static readonly double OmegaM = OmegaMh2 / (H * H);
    static readonly double OmegaR = OmegaRh2 / (H * H);
    static readonly double OmegaL = 1.0 - OmegaM - OmegaR;
    static readonly double RhoCrit = 3.0 * H0 * H0 / (8.0 * Math.PI * G); // kg/m^3
    static readonly double NH0 = OmegaB * RhoCrit / Mh;                   // m^-3 (H-only)

    public sealed record RecombinationResult(
        double ZStar, double XeAtZstar, double TauAtZstar, int Steps);

    /// <summary>Hubble function H(z) in s^-1.</summary>
    static double Hubble(double z)
    {
        double e2 = OmegaM * Math.Pow(1 + z, 3)
                  + OmegaR * Math.Pow(1 + z, 4)
                  + OmegaL;
        return H0 * Math.Sqrt(Math.Max(e2, 0.0));
    }

    /// <summary>Saha equilibrium ionization fraction X_e at redshift z.</summary>
    public static double Saha(double z)
    {
        double T = Tcmb0 * (1 + z);
        double nH = NH0 * Math.Pow(1 + z, 3);
        double q = Thermal(T) / nH * Math.Exp(-EIon / (KB * T));
        // Solve X^2/(1-X) = q. Rationalized form avoids catastrophic cancellation
        // when X ~ 1 (q large): X = 2q / (q + sqrt(q^2 + 4q)).
        double disc = Math.Sqrt(q * q + 4.0 * q);
        return 2.0 * q / (q + disc);
    }

    /// <summary>Case-B recombination coefficient alpha^(2)(T) in m^3/s.</summary>
    static double Alpha2(double T)
    {
        double t4 = T / 1e4;
        return 2.84e-19 * Math.Pow(t4, -0.7); // 2.84e-13 cm^3/s * (T/1e4)^-0.7
    }

    /// <summary>Thermal de Broglie density (m_e kT / 2pi hbar^2)^(3/2) in m^-3.</summary>
    static double Thermal(double T)
        => Math.Pow(MeV * KB * T / (2 * Math.PI * Hbar * Hbar), 1.5);

    /// <summary>Photoionization rate beta^(2)(T) in s^-1 (2s ionization energy).</summary>
    static double Beta2(double T)
        => Alpha2(T) * Thermal(T) * Math.Exp(-E2s / (KB * T));

    /// <summary>Peebles suppression factor C(T, X_e, z).</summary>
    static double PeeblesC(double T, double Xe, double z)
    {
        double nH = NH0 * Math.Pow(1 + z, 3);
        double n1s = (1.0 - Xe) * nH;
        double beta = Beta2(T);
        double K = LambdaAlpha * LambdaAlpha * LambdaAlpha
                 / (8.0 * Math.PI * Hubble(z));
        return (1.0 + K * Lambda2s1s * n1s)
             / (1.0 + K * (Lambda2s1s + beta) * n1s);
    }

    /// <summary>Peebles ODE: dX_e/dz (positive as X_e falls with decreasing z).
    /// Equilibrium of this bracket is the Saha equation (E_ion), so the Saha IC
    /// is the exact equilibrium and the ODE is non-stiff.</summary>
    static double Dxdz(double z, double Xe)
    {
        double T = Tcmb0 * (1 + z);
        double nH = NH0 * Math.Pow(1 + z, 3);
        double alpha = Alpha2(T);
        double g = alpha * (nH * Xe * Xe
                          - Thermal(T) * (1.0 - Xe) * Math.Exp(-EIon / (KB * T)));
        double C = PeeblesC(T, Xe, z);
        return C * g / (Hubble(z) * (1.0 + z));
    }

    /// <summary>Solve the full recombination history and locate z* (tau = 1).</summary>
    public static RecombinationResult Solve(
        double zHi = 1800.0, double zLo = 100.0, int steps = 80000)
    {
        double dz = (zLo - zHi) / steps;  // negative

        // ── Pass 1: integrate X_e(z) from high z down to low z (Saha IC) ──
        var Z = new double[steps + 1];
        var XeArr = new double[steps + 1];
        double z = zHi, xe = Saha(zHi);
        Z[0] = z; XeArr[0] = xe;

        for (int i = 0; i < steps; i++)
        {
            double zNext = z + dz;
            double k1 = Dxdz(z, xe);
            double k2 = Dxdz(z + 0.5 * dz, xe + 0.5 * dz * k1);
            double k3 = Dxdz(z + 0.5 * dz, xe + 0.5 * dz * k2);
            double k4 = Dxdz(zNext, xe + dz * k3);
            xe += dz * (k1 + 2 * k2 + 2 * k3 + k4) / 6.0;
            if (xe < 0.0) xe = 0.0;
            if (xe > 1.0) xe = 1.0;
            z = zNext;
            Z[i + 1] = z; XeArr[i + 1] = xe;
        }

        // ── Pass 2: integrate tau(z) from low z upward; locate tau = 1 ──
        double tau = 0.0, zStar = 0.0, xeStar = 0.0, tauStar = 0.0;
        bool found = false;

        for (int i = steps; i >= 1; i--)
        {
            double zLow = Z[i], zHigh = Z[i - 1];
            double zMid = 0.5 * (zLow + zHigh);
            double xeMid = 0.5 * (XeArr[i] + XeArr[i - 1]);
            double ne = xeMid * NH0 * Math.Pow(1 + zMid, 3);
            double dTau = SigmaT * ne * C / (Hubble(zMid) * (1.0 + zMid)) * (zHigh - zLow);
            tau += dTau;

            if (!found && tau >= 1.0)
            {
                zStar = zMid;
                xeStar = xeMid;
                tauStar = tau;
                found = true;
            }
        }

        return new RecombinationResult(zStar, xeStar, tauStar, steps);
    }

    /// <summary>Conformal time (comoving Mpc) at scale factor a (matter+radiation
    /// closed form, Lambda negligible early).</summary>
    public static double ConformalTimeMpc(double a)
    {
        double h0m = H0KmS / 2.99792458e5;   // H0 in Mpc^-1
        return 2.0 / (h0m * OmegaM) * (Math.Sqrt(OmegaM * a + OmegaR) - Math.Sqrt(OmegaR));
    }

    /// <summary>Visibility function g(z) = sigma_T n_e c/(H(1+z)) e^{-tau(z)} over
    /// the recombination epoch, and its conformal-time RMS width sigma_eta (Mpc).
    /// Returns (sigma_eta, z_peak).</summary>
    public static (double SigmaEtaMpc, double ZPeak) VisibilityWidth(
        double zLo = 700.0, double zHi = 1600.0, int steps = 40000)
    {
        // pass 1: X_e(z) from Saha IC
        double dz = (zLo - zHi) / steps;
        var Z = new double[steps + 1];
        var Xe = new double[steps + 1];
        double z = zHi, xe = Saha(zHi);
        Z[0] = z; Xe[0] = xe;
        for (int i = 0; i < steps; i++)
        {
            double zNext = z + dz;
            double k1 = Dxdz(z, xe);
            double k2 = Dxdz(z + 0.5 * dz, xe + 0.5 * dz * k1);
            double k3 = Dxdz(z + 0.5 * dz, xe + 0.5 * dz * k2);
            double k4 = Dxdz(zNext, xe + dz * k3);
            xe += dz * (k1 + 2 * k2 + 2 * k3 + k4) / 6.0;
            if (xe < 0.0) xe = 0.0;
            if (xe > 1.0) xe = 1.0;
            z = zNext; Z[i + 1] = z; Xe[i + 1] = xe;
        }

        // pass 2: tau(z) from zLo (tau ~ 0) upward, then g(z) and its moments
        double tau = 0.0;
        double sumG = 0.0, sumEta = 0.0, sumEta2 = 0.0;
        double gMax = 0.0, zPeak = 0.0;
        for (int i = steps; i >= 1; i--)
        {
            double zLo2 = Z[i], zHi2 = Z[i - 1];
            double zMid = 0.5 * (zLo2 + zHi2);
            double xeMid = 0.5 * (Xe[i] + Xe[i - 1]);
            double ne = xeMid * NH0 * Math.Pow(1.0 + zMid, 3);
            double dTaudz = SigmaT * ne * C / (Hubble(zMid) * (1.0 + zMid));
            double dTau = dTaudz * (zHi2 - zLo2);   // positive, integrating upward in z
            double g = dTaudz * Math.Exp(-(tau + 0.5 * dTau));
            double eta = ConformalTimeMpc(1.0 / (1.0 + zMid));
            double w = g * (zHi2 - zLo2);
            sumG += w; sumEta += w * eta; sumEta2 += w * eta * eta;
            if (g > gMax) { gMax = g; zPeak = zMid; }
            tau += dTau;
        }
        double mean = sumEta / sumG;
        double variance = sumEta2 / sumG - mean * mean;
        return (Math.Sqrt(variance), zPeak);
    }

    // ── Sound horizon & θ* (background only; no perturbation theory) ──────

    public sealed record ThetaStarResult(
        double ZStar, double RsMpc, double DmMpc,
        double ThetaStar, double ThetaStar100);

    static double OmegaG => OmegaGammaH2 / (H * H);

    /// <summary>Sound speed c_s(z) = c / sqrt(3(1+R)), R = 3 rho_b / 4 rho_gamma.</summary>
    public static double SoundSpeed(double z)
    {
        double R = 3.0 * OmegaB / (4.0 * OmegaG) / (1.0 + z);
        return C / Math.Sqrt(3.0 * (1.0 + R));
    }

    /// <summary>Comoving sound horizon r_s = int_{z*}^{zMax} c_s / H dz (meters).</summary>
    public static double SoundHorizon(double zStar, double zMax = 1e5)
    {
        int steps = 40000;
        double dz = (zMax - zStar) / steps;
        double sum = 0.0;
        for (int i = 0; i <= steps; i++)
        {
            double zp = zStar + i * dz;
            double f = SoundSpeed(zp) / Hubble(zp);
            double w = (i == 0 || i == steps) ? 1.0 : (i % 2 == 0 ? 2.0 : 4.0);
            sum += w * f;
        }
        return dz * sum / 3.0;
    }

    /// <summary>Comoving distance D_M(z) = int_0^z c / H dz (meters).</summary>
    public static double ComovingDistance(double z)
    {
        int steps = 20000;
        double dz = z / steps;
        double sum = 0.0;
        for (int i = 0; i <= steps; i++)
        {
            double zp = i * dz;
            double f = C / Hubble(zp);
            double w = (i == 0 || i == steps) ? 1.0 : (i % 2 == 0 ? 2.0 : 4.0);
            sum += w * f;
        }
        return dz * sum / 3.0;
    }

    /// <summary>θ* = r_s / D_M(z*) (comoving ratio; equals physical r_s / angular D_A).</summary>
    public static ThetaStarResult ComputeThetaStar()
    {
        var rec = Solve();
        double zStar = rec.ZStar;
        double rs = SoundHorizon(zStar);
        double dM = ComovingDistance(zStar);
        double theta = rs / dM;
        return new ThetaStarResult(zStar, rs / Mpc, dM / Mpc, theta, theta * 100.0);
    }
}
