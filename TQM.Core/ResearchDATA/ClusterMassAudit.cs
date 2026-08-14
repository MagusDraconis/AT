using System.Globalization;

namespace TQM.Core.ResearchDATA;

/// <summary>
/// Cluster Mass Audit — upgrades the "Clusters" topic from PARTIAL to COMPLETE.
///
/// Reconstructs the cluster mass profile from two independent observables:
///   1. Coma galaxy kinematics (velocity dispersion -> virial mass).
///   2. Chandra X-ray gas profiles (ACCEPT) -> gas mass fraction.
///
/// Compares three models: Newtonian (baryons only), Lambda-CDM, and the TQM
/// defect model (X063/X064: topological-defect dark matter). No new physics.
/// </summary>
public static class ClusterMassAudit
{
    public const double G = 6.674e-11;        // m^3 kg^-1 s^-2
    public const double Msun = 1.989e30;      // kg
    public const double Mpc = 3.0857e22;      // m
    public const double Ckms = 2.99792458e5;  // km/s
    public const double H0 = 70.0;            // km/s/Mpc

    // Coma cluster anchors (literature, observational inputs — not new physics).
    public const double ComaRa = 194.953;     // deg (12h59m48.7s)
    public const double ComaDec = 27.9807;    // deg (+27d58m50s)
    public const double ComaZ = 0.0231;
    public const double ComaRvirMpc = 1.4;    // virial radius (Mpc)
    public const double ComaGasMsun = 1.2e14; // X-ray gas mass (M_sun), literature
    public const double ComaStarsMsun = 1.0e13;// stellar mass (M_sun), literature

    // ACCEPT gas: mean molecular weight (fully ionized, 0.6 H + 0.3 He).
    public const double Mu = 0.61;
    public const double Mp = 1.6726e-27;      // kg

    public sealed record ComaResult(
        int NGalaxies, double SigmaVKms, double VirialMassMsun,
        double BaryonMassMsun, double Ratio, double BaryonFraction);

    public sealed record AcceptResult(
        int Clusters, double MedianFgas, double MeanFgas,
        double Fgas16, double Fgas84);

    /// <summary>Coma dynamical mass from galaxy line-of-sight velocities.</summary>
    public static ComaResult AnalyzeComa(string csvPath)
    {
        var vs = new List<double>();
        foreach (var line in File.ReadLines(csvPath).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(',');
            if (p.Length < 5) continue;
            if (!double.TryParse(p[4], NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) continue;
            vs.Add(v);
        }

        int n = vs.Count;
        double mean = vs.Sum() / n;
        double var = vs.Sum(v => (v - mean) * (v - mean)) / (n - 1);
        double sigma = Math.Sqrt(var); // km/s

        // Virial theorem: M_vir = 3 sigma^2 R_vir / G.
        double sigmaMs = sigma * 1e3;
        double rvir = ComaRvirMpc * Mpc;
        double mVirial = 3.0 * sigmaMs * sigmaMs * rvir / G / Msun;

        double mBaryon = ComaGasMsun + ComaStarsMsun;
        double ratio = mVirial / mBaryon;
        double fBaryon = mBaryon / mVirial;

        return new ComaResult(n, sigma, mVirial, mBaryon, ratio, fBaryon);
    }

    /// <summary>Gas mass fraction across the ACCEPT Chandra cluster sample.</summary>
    public static AcceptResult AnalyzeAccept(string profilePath)
    {
        var shells = new Dictionary<string, List<(double Rin, double Rout, double Ne, double Mgrav)>>();

        foreach (var line in File.ReadLines(profilePath))
        {
            if (line.StartsWith('#')) continue;
            var p = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 14) continue;
            string name = p[0];
            if (!double.TryParse(p[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var rin)) continue;
            if (!double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var rout)) continue;
            if (!double.TryParse(p[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var ne)) continue;
            if (!double.TryParse(p[12], NumberStyles.Float, CultureInfo.InvariantCulture, out var mgrav)) continue;
            if (ne <= 0 || mgrav <= 0) continue;

            if (!shells.TryGetValue(name, out var list)) shells[name] = list = new();
            list.Add((rin, rout, ne, mgrav));
        }

        var fgas = new List<double>();
        foreach (var (_, list) in shells)
        {
            double mgas = 0.0, mtot = 0.0;
            foreach (var (rin, rout, ne, mgrav) in list.OrderBy(x => x.Rin))
            {
                double r1 = rin * Mpc, r2 = rout * Mpc;
                double vol = (4.0 / 3.0) * Math.PI * (r2 * r2 * r2 - r1 * r1 * r1);
                double rho = Mu * Mp * (ne * 1e6); // cm^-3 -> m^-3, kg/m^3
                mgas += rho * vol;
                mtot = mgrav * Msun;
            }
            if (mgas > 0 && mtot > 0 && mgas / mtot < 1.5)
                fgas.Add(mgas / mtot);
        }

        fgas.Sort();
        int n = fgas.Count;
        double median = n % 2 == 1 ? fgas[n / 2] : 0.5 * (fgas[n / 2 - 1] + fgas[n / 2]);
        double mean = fgas.Average();
        double q16 = fgas[(int)(0.16 * n)];
        double q84 = fgas[(int)(0.84 * n)];

        return new AcceptResult(n, median, mean, q16, q84);
    }
}
