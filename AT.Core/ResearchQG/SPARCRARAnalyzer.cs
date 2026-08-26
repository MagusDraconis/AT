using System.Globalization;

namespace AT.Core.ResearchQG;

/// <summary>Local SPARC reference analysis: recovers the local RAR acceleration
/// scale g†(0) and the baryonic Tully-Fisher relation (baryonic scaling prior).</summary>
public static class SPARCRARAnalyzer
{
    const double MSun = 1.989e30;
    const double Kpc_m = 3.0857e19;
    const double G = 6.674e-11;
    // g [m/s^2] = G_ACC * M[Msun] / r[kpc]^2
    const double G_ACC = G * MSun / (Kpc_m * Kpc_m);
    // Canonical SPARC stellar mass-to-light ratio at [3.6] micron.
    const double ML_36 = 0.5;

    public static SPARCReport Run(string dataDir)
    {
        string massPath = Path.Combine(dataDir, "MassModels_Lelli2016c.mrt");
        string samplePath = Path.Combine(dataDir, "SPARC_Lelli2016c.mrt");
        if (!File.Exists(massPath) || !File.Exists(samplePath))
            throw new FileNotFoundException($"SPARC data not found in {dataDir}");

        var mass = ReadMassModels(massPath);
        var sample = ReadSample(samplePath);

        // g_obs and g_bar per radius.
        var gobs = new List<double>();
        var gbar = new List<double>();
        foreach (var m in mass)
        {
            double gb = (m.Vgas * m.Vgas + ML_36 * m.Vdisk * m.Vdisk + ML_36 * m.Vbul * m.Vbul) / m.R;
            double go = m.Vobs * m.Vobs / m.R;
            gobs.Add(G_ACC * go);
            gbar.Add(G_ACC * gb);
        }

        double gdagMcGaugh = FitRar(gobs.ToArray(), gbar.ToArray(), form: "mcGaugh");
        double gdagAt = FitRar(gobs.ToArray(), gbar.ToArray(), form: "at");

        var btfr = FitBTFR(sample);

        return new SPARCReport(
            mass.Count, sample.Count,
            gdagMcGaugh, gdagAt,
            btfr.a, btfr.b, btfr.scatter,
            gobs.ToArray(), gbar.ToArray(),
            btfr.logVflat, btfr.logMbar);
    }

    // ---------------------------------------------------------------------
    // RAR fit (local): recover g†(0)
    // ---------------------------------------------------------------------

    private static double FitRar(double[] gobs, double[] gbar, string form)
    {
        double bestLog = -10.0, bestChi2 = double.PositiveInfinity;
        for (double logg = -11.5; logg <= -8.5; logg += 0.005)
        {
            double g = Math.Pow(10, logg);
            double chi2 = 0;
            int n = 0;
            for (int i = 0; i < gobs.Length; i++)
            {
                if (gbar[i] <= 1e-14 || gobs[i] <= 0) continue;
                double pred = form == "at"
                    ? gbar[i] * Math.Sqrt(1 + g / gbar[i])
                    : gbar[i] / (1 - Math.Exp(-Math.Sqrt(gbar[i] / g)));
                double lp = Math.Log10(pred);
                double lo = Math.Log10(gobs[i]);
                chi2 += (lo - lp) * (lo - lp);
                n++;
            }
            chi2 /= Math.Max(1, n);
            if (chi2 < bestChi2) { bestChi2 = chi2; bestLog = logg; }
        }
        return Math.Pow(10, bestLog);
    }

    // ---------------------------------------------------------------------
    // Baryonic Tully-Fisher relation (baryonic scaling prior)
    // ---------------------------------------------------------------------

    private static (double a, double b, double scatter, double[] logVflat, double[] logMbar) FitBTFR(
        List<(string gal, double vflat, double l36, double mhi)> sample)
    {
        var logV = new List<double>();
        var logM = new List<double>();
        foreach (var s in sample)
        {
            if (s.vflat <= 20 || s.l36 <= 0 || s.mhi <= 0) continue;
            double mStar = ML_36 * s.l36 * 1e9;      // Msun
            double mGas = 1.33 * s.mhi * 1e9;        // Msun (1.33 = helium correction)
            double mBar = mStar + mGas;
            logV.Add(Math.Log10(s.vflat));
            logM.Add(Math.Log10(mBar));
        }
        // Linear least squares: logM = a + b * logV.
        double xm = logV.Average(), ym = logM.Average();
        double sxx = 0, sxy = 0;
        for (int i = 0; i < logV.Count; i++)
        {
            sxx += (logV[i] - xm) * (logV[i] - xm);
            sxy += (logV[i] - xm) * (logM[i] - ym);
        }
        double b = sxx > 0 ? sxy / sxx : 4.0;
        double a = ym - b * xm;
        double scatter = 0;
        for (int i = 0; i < logV.Count; i++)
            scatter += (logM[i] - (a + b * logV[i])) * (logM[i] - (a + b * logV[i]));
        scatter = Math.Sqrt(scatter / Math.Max(1, logV.Count));
        return (a, b, scatter, logV.ToArray(), logM.ToArray());
    }

    /// <summary>Baryonic mass predicted by the fitted BTFR for a flat rotation velocity.</summary>
    public static double BaryonicMassFromBTFR(double vflat_kms, double a, double b) =>
        Math.Pow(10, a + b * Math.Log10(Math.Max(vflat_kms, 20)));

    // ---------------------------------------------------------------------
    // Fixed-width parsing
    // ---------------------------------------------------------------------

    private static List<(string gal, double R, double Vobs, double eVobs, double Vgas, double Vdisk, double Vbul)>
        ReadMassModels(string path)
    {
        var rows = new List<(string, double, double, double, double, double, double)>();
        foreach (var line in File.ReadAllLines(path))
        {
            if (line.Length < 76) continue;
            if (!line.Substring(0, 11).Trim().Any(char.IsLetterOrDigit)) continue;
            string gal = line.Substring(0, 11).Trim();
            double R = Parse(line, 19, 6);
            double Vobs = Parse(line, 26, 6);
            double eVobs = Parse(line, 33, 5);
            double Vgas = Parse(line, 39, 6);
            double Vdisk = Parse(line, 46, 6);
            double Vbul = Parse(line, 53, 6);
            if (gal.Length == 0 || R <= 0 || Vobs <= 0) continue;
            rows.Add((gal, R, Vobs, eVobs, Vgas, Vdisk, Vbul));
        }
        return rows;
    }

    private static List<(string gal, double vflat, double l36, double mhi)> ReadSample(string path)
    {
        var rows = new List<(string, double, double, double)>();
        foreach (var line in File.ReadAllLines(path))
        {
            // Data lines have 19 whitespace-separated tokens. Token indices
            // (0-based): 0=Galaxy, 7=L[3.6], 13=MHI, 15=Vflat.
            var tok = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            if (tok.Length < 19) continue;
            if (!tok[0].Any(char.IsLetterOrDigit)) continue;
            if (!double.TryParse(tok[15], NumberStyles.Float, CultureInfo.InvariantCulture, out double vflat)) continue;
            if (!double.TryParse(tok[7], NumberStyles.Float, CultureInfo.InvariantCulture, out double l36)) continue;
            if (!double.TryParse(tok[13], NumberStyles.Float, CultureInfo.InvariantCulture, out double mhi)) continue;
            if (vflat <= 0) continue;
            rows.Add((tok[0], vflat, l36, mhi));
        }
        return rows;
    }

    private static double Parse(string line, int start0, int len)
    {
        if (start0 + len > line.Length) return double.NaN;
        string s = line.Substring(start0, len);
        return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v) ? v : double.NaN;
    }
}

public sealed record SPARCReport(
    int NRadiusPoints,
    int NGalaxies,
    double GdaggerLocalMcGaugh_m_s2,
    double GdaggerLocalAt_m_s2,
    double BTFR_a,
    double BTFR_b,
    double BTFR_scatter_dex,
    double[] Gobs_m_s2,
    double[] Gbar_m_s2,
    double[] BTFR_logVflat,
    double[] BTFR_logMbar);
