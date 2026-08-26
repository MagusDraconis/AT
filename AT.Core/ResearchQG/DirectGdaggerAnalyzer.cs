using System.Globalization;

namespace AT.Core.ResearchQG;

/// <summary>
/// QG-074: First Direct High-z g† Measurement. Combines independent COSMOS2015
/// stellar masses with KMOS3D rotation curves to reconstruct g_bar(r) from
/// stellar (exponential profile) + gas (depletion time) components — WITHOUT the
/// circular BTFR prior — and fits the RAR acceleration scale g†(z) per galaxy,
/// stacked in redshift bins, compared against AT (g† ∝ H(z)) vs MOND (constant).
/// </summary>
public static class DirectGdaggerAnalyzer
{
    const double c_kms = 299792.458;
    const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    const double MSun_kg = 1.989e30;
    const double Kpc_m = 3.0857e19;
    const double G = 6.674e-11;
    const double G_ACC = G * MSun_kg / (Kpc_m * Kpc_m);   // m/s^2 per Msun/kpc^2
    const double SIGMA_LOG = 0.20;                          // combined RAR scatter (dex)
    const double ML_ERR_DEX = 0.15;                         // COSMOS2015 stellar-mass error
    const double GAS_ERR_DEX = 0.30;                        // depletion-time / gas-mass error

    // Local acceleration scale (AT): c*H0/(2pi).
    private static double GdaggerLocal =>
        c_kms * (H0 / Kpc_m * 1e3) / (2.0 * Math.PI);

    public static DirectGdaggerReport Run(string massCatalogCsv, string rotationCurvesDir, string outDir)
    {
        Directory.CreateDirectory(outDir);

        var masses = ReadMassCatalog(massCatalogCsv);        // Object -> (z, M*, SFR, Re_kpc)
        var curves = ReadRotationCurves(rotationCurvesDir);  // Object -> (r, vrot, gobs, gobsErr)

        var fits = new List<RARFit>();
        var baryRows = new List<BaryRow>();
        var rarPoints = new List<(string obj, double z, double gbar, double gobs)>();

        foreach (var kv in masses)
        {
            string obj = kv.Key;
            var (z, mStar, sfr, reKpc) = kv.Value;
            if (!curves.TryGetValue(obj, out var rc)) continue;
            if (double.IsNaN(mStar) || mStar <= 0 || double.IsNaN(reKpc) || reKpc <= 0) continue;

            // Stellar exponential disk: half-light radius Re = 1.678 Rd.
            double rd = reKpc / 1.678;
            double rdGas = 1.5 * rd;
            // Gas mass from SFR + depletion time (no BTFR prior).
            double tDepYr = 1.5e9 / Math.Sqrt(1 + z);
            double mGas = double.IsNaN(sfr) ? 0 : Math.Max(sfr, 0) * tDepYr;

            var gbar = new double[rc.gobs.Length];
            for (int i = 0; i < gbar.Length; i++)
            {
                double r = rc.radius[i];
                if (r <= 0) { gbar[i] = double.NaN; continue; }
                double fStar = 1 - (1 + r / rd) * Math.Exp(-r / rd);
                double fGas = 1 - (1 + r / rdGas) * Math.Exp(-r / rdGas);
                double mEnc = mStar * fStar + mGas * fGas;
                gbar[i] = G_ACC * mEnc / (r * r);
            }

            var fit = FitGdagger(obj, z, rc.gobs, gbar);
            fits.Add(fit);
            baryRows.Add(new BaryRow(obj, z, mStar, mGas, reKpc));
            for (int i = 0; i < gbar.Length; i++)
                if (!double.IsNaN(gbar[i]) && !double.IsNaN(rc.gobs[i]))
                    rarPoints.Add((obj, z, gbar[i], rc.gobs[i]));
        }

        var bins = BuildBins(fits.ToArray());
        var comparisons = CompareModels(fits.ToArray());

        return new DirectGdaggerReport(
            BuildA(fits.ToArray(), masses.Count, curves.Count),
            BuildB(baryRows),
            BuildC(fits.ToArray()),
            BuildD(fits.ToArray(), bins),
            BuildE(comparisons),
            BuildF(),
            BuildG(fits.ToArray(), bins, comparisons),
            fits.ToArray(), rarPoints.ToArray(), bins, comparisons);
    }

    // ---------------------------------------------------------------------
    // Per-galaxy g† fit (log-space grid search, AT RAR form)
    // ---------------------------------------------------------------------

    private static RARFit FitGdagger(string obj, double z, double[] gobs, double[] gbar)
    {
        var idx = Enumerable.Range(0, gobs.Length)
            .Where(i => !double.IsNaN(gobs[i]) && !double.IsNaN(gbar[i]) && gobs[i] > 0 && gbar[i] > 0)
            .ToArray();
        if (idx.Length < 3)
            return new RARFit(obj, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        double bestLog = double.NaN, bestChi2 = double.PositiveInfinity;
        var prof = new List<(double logg, double chi2)>();
        for (double logg = -13.0; logg <= -8.0; logg += 0.02)
        {
            double g = Math.Pow(10, logg);
            double chi2 = 0;
            foreach (int i in idx)
            {
                double pred = gbar[i] * Math.Sqrt(1 + g / gbar[i]);
                chi2 += Sq((Math.Log10(gobs[i]) - Math.Log10(pred)) / SIGMA_LOG);
            }
            prof.Add((logg, chi2));
            if (chi2 < bestChi2) { bestChi2 = chi2; bestLog = logg; }
        }
        if (double.IsNaN(bestLog))
            return new RARFit(obj, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        double lo = bestLog, hi = bestLog;
        foreach (var (logg, chi2) in prof)
            if (chi2 <= bestChi2 + 1.0) { if (logg < lo) lo = logg; if (logg > hi) hi = logg; }
        double sigma = Math.Max(0.05, 0.5 * (hi - lo));
        if (bestLog <= -12.98 || bestLog >= -8.02 || sigma > 0.8) sigma = 1.0;

        return new RARFit(obj, z, Math.Pow(10, bestLog),
            Math.Pow(10, bestLog + sigma) - Math.Pow(10, bestLog), bestLog, sigma, bestChi2, idx.Length);
    }

    // ---------------------------------------------------------------------
    // Redshift bins and model comparison
    // ---------------------------------------------------------------------

    private static EvolutionBin[] BuildBins(RARFit[] fits)
    {
        var edges = new (double lo, double hi)[] { (0.5, 1.0), (1.0, 1.5), (1.5, 2.0), (2.0, 5.0) };
        var result = new List<EvolutionBin>();
        foreach (var (lo, hi) in edges)
        {
            var g = fits.Where(f => f.Redshift >= lo && f.Redshift < hi && !double.IsNaN(f.Gdagger_m_s2)).ToArray();
            if (g.Length == 0) continue;
            double zmean = g.Average(f => f.Redshift);
            double mean = g.Average(f => f.Gdagger_m_s2);
            var s = g.Select(f => f.Gdagger_m_s2).OrderBy(x => x).ToArray();
            double median = s[s.Length / 2];
            double err = g.Length >= 2 ? BootstrapStdErr(g.Select(f => f.LogGdagger).ToArray(), g.Select(f => f.LogGdagger_err).ToArray(), 42) : g[0].Gdagger_err_m_s2;
            double at = GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + zmean, 3) + OmL);
            result.Add(new EvolutionBin(zmean, lo, hi, mean, median, err, g.Length, at));
        }
        return result.ToArray();
    }

    private static TheoryComparison[] CompareModels(RARFit[] fits)
    {
        var d = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2)).ToArray();
        int n = d.Length;
        double chi2At = 0, chi2Mond = 0, chi2Null = 0;
        foreach (var f in d)
        {
            double atPred = Math.Log10(GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + f.Redshift, 3) + OmL));
            double nullPred = Math.Log10(GdaggerLocal);
            double w = 1.0 / Sq(Math.Max(f.LogGdagger_err, 0.05));
            chi2At += w * Sq(f.LogGdagger - atPred);
            chi2Null += w * Sq(f.LogGdagger - nullPred);
        }
        // MOND constant: fit the best constant amplitude.
        double bestConst = double.PositiveInfinity;
        for (double logg = -13.0; logg <= -8.0; logg += 0.01)
        {
            double c2 = d.Sum(f => Sq((f.LogGdagger - logg) / Math.Max(f.LogGdagger_err, 0.05)));
            if (c2 < bestConst) bestConst = c2;
        }
        chi2Mond = bestConst;

        return new[]
        {
            new TheoryComparison("AT  (g† ∝ H(z))", chi2At, chi2At + 0, chi2At + 0 * Math.Log(Math.Max(n,1)), 0, n, -0.5 * chi2At),
            new TheoryComparison("MOND (g† = constant)", chi2Mond, chi2Mond + 2, chi2Mond + 1 * Math.Log(Math.Max(n,1)), 1, n, -0.5 * chi2Mond),
            new TheoryComparison("NULL (g† = local, no evolution)", chi2Null, chi2Null + 0, chi2Null + 0 * Math.Log(n), 0, n, -0.5 * chi2Null),
        };
    }

    private static string Verdict(RARFit[] fits, TheoryComparison[] cmp)
    {
        int nConstrained = fits.Count(f => !double.IsNaN(f.LogGdagger) && f.LogGdagger_err < 0.8);
        var g = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2) && f.Gdagger_m_s2 > 0)
                     .Select(f => Math.Log10(f.Gdagger_m_s2)).ToArray();
        bool scatterBad = g.Length >= 2 && (g.Max() - g.Min()) > 1.5;

        if (nConstrained < 3 || scatterBad) return "A = inconclusive";

        var at = cmp.First(c => c.Model.StartsWith("AT"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - at.Chi2;
        double sig = Math.Sqrt(Math.Max(dchi2, 0));
        if (dchi2 > 9 && sig >= 3) return "D = decisive discrimination";
        if (dchi2 > 4 && sig >= 2) return "C = measurable evolution";
        if (dchi2 > 1) return "B = weak evidence";
        return "A = inconclusive";
    }

    // ---------------------------------------------------------------------
    // Input parsing
    // ---------------------------------------------------------------------

    private static Dictionary<string, (double z, double mStar, double sfr, double reKpc)> ReadMassCatalog(string csv)
    {
        var map = new Dictionary<string, (double, double, double, double)>(StringComparer.OrdinalIgnoreCase);
        var lines = File.ReadAllLines(csv);
        if (lines.Length < 2) return map;
        var h = lines[0].Split(',');
        int iObj = Array.FindIndex(h, c => c == "Object");
        int iZ = Array.FindIndex(h, c => c == "z");
        int iM = Array.FindIndex(h, c => c == "StellarMass");
        int iSfr = Array.FindIndex(h, c => c == "SFR");
        int iRe = Array.FindIndex(h, c => c == "Radius");
        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(',');
            if (p.Length <= Math.Max(iObj, Math.Max(iZ, Math.Max(iM, Math.Max(iSfr, iRe))))) continue;
            double z = Parse(p[iZ]), m = Parse(p[iM]), sfr = Parse(p[iSfr]), re = Parse(p[iRe]);
            map[p[iObj].Trim()] = (z, m, sfr, re);
        }
        return map;
    }

    private static Dictionary<string, (double[] radius, double[] vrot, double[] gobs, double[] gobsErr)> ReadRotationCurves(string dir)
    {
        var map = new Dictionary<string, (double[], double[], double[], double[])>(StringComparer.OrdinalIgnoreCase);
        foreach (string file in Directory.GetFiles(dir, "*_rotation.csv"))
        {
            string obj = Path.GetFileName(file).Replace("_rotation.csv", "");
            var lines = File.ReadAllLines(file);
            if (lines.Length < 2) continue;
            var h = lines[0].Split(',');
            int iR = Array.FindIndex(h, c => c == "radius_kpc");
            int iV = Array.FindIndex(h, c => c == "vrot_kms");
            int iG = Array.FindIndex(h, c => c == "gobs_m_s2");
            int iE = Array.FindIndex(h, c => c == "gobs_err_m_s2");
            var r = new List<double>(); var v = new List<double>(); var g = new List<double>(); var e = new List<double>();
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var p = line.Split(',');
                r.Add(Parse(p[iR])); v.Add(Parse(p[iV])); g.Add(Parse(p[iG])); e.Add(Parse(p[iE]));
            }
            map[obj] = (r.ToArray(), v.ToArray(), g.ToArray(), e.ToArray());
        }
        return map;
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(RARFit[] fits, int nMass, int nCurves)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Galaxies with a COSMOS2015 mass: {nMass}");
        sb.AppendLine($"Galaxies with a rotation curve: {nCurves}");
        sb.AppendLine($"Galaxies usable for the direct RAR (both + >= 3 points): {fits.Length}");
        return sb.ToString();
    }

    private static string BuildB(List<BaryRow> rows)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Mass reconstruction (stellar exponential + gas depletion time).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,6} {2,9} {3,9} {4,6}", "Object", "z", "log M*", "log Mgas", "Re kpc"));
        foreach (var r in rows.OrderBy(r => r.Z))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,6:F3} {2,9:F2} {3,9:F2} {4,6:F1}",
                r.Object, r.Z, Math.Log10(Math.Max(r.MStar, 1)), Math.Log10(Math.Max(r.MGas, 1)), r.ReKpc));
        sb.AppendLine();
        sb.AppendLine($"  Stellar-mass uncertainty: ±{ML_ERR_DEX} dex; gas: ±{GAS_ERR_DEX} dex.");
        return sb.ToString();
    }

    private static string BuildC(RARFit[] fits)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Per-galaxy RAR fits (g_obs = g_bar·√(1+g†/g_bar), free g†).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,6} {2,9} {3,10} {4,8} {5,8}", "Object", "z", "log g†", "g† [m/s²]", "σ dex", "χ²"));
        foreach (var f in fits.OrderBy(f => f.Redshift))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,6:F3} {2,9:F2} {3,10:E2} {4,8:F2} {5,8:F1}",
                f.ObjectId, f.Redshift, f.LogGdagger, f.Gdagger_m_s2, f.LogGdagger_err, f.Chi2));
        sb.AppendLine();
        sb.AppendLine($"  Local g† = c·H₀/2π = {GdaggerLocal:E2} m/s².");
        return sb.ToString();
    }

    private static string BuildD(RARFit[] fits, EvolutionBin[] bins)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Redshift-binned g† (weighted mean / median).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,8} {1,4} {2,10} {3,10} {4,10} {5,11}", "z_mean", "N", "mean", "median", "err", "AT pred"));
        foreach (var b in bins)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F2} {1,4} {2,10:E2} {3,10:E2} {4,10:E2} {5,11:E2}",
                b.Zmean, b.Ngalaxies, b.Gdagger_mean_m_s2, b.Gdagger_median_m_s2, b.Gdagger_err_m_s2, b.ATPrediction_m_s2));
        return sb.ToString();
    }

    private static string BuildE(TheoryComparison[] cmp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Model comparison (χ², AIC, BIC).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-30} {1,9} {2,8} {3,8}", "Model", "χ²", "AIC", "BIC"));
        foreach (var c in cmp)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-30} {1,9:F1} {2,8:F1} {3,8:F1}", c.Model, c.Chi2, c.AIC, c.BIC));
        var at = cmp.First(c => c.Model.StartsWith("AT"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - at.Chi2;
        double bf = Math.Exp(0.5 * dchi2);
        sb.AppendLine();
        sb.AppendLine($"  Δχ²(MOND − AT) = {dchi2:F1};  Bayes factor AT/MOND ≈ {bf:F1}");
        sb.AppendLine($"  (BF > 1 favors AT; BF < 1 favors MOND.)");
        return sb.ToString();
    }

    private static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Systematic uncertainties:");
        sb.AppendLine($"  - Stellar mass (COSMOS2015): ±{ML_ERR_DEX} dex");
        sb.AppendLine($"  - Gas mass (depletion time 1.5(1+z)^-0.5 Gyr): ±{GAS_ERR_DEX} dex");
        sb.AppendLine("  - Effective radius: ±0.1 dex");
        sb.AppendLine("  - Inclination deprojection (velocity): factor ~1.2-1.5");
        sb.AppendLine("  - Stellar-profile assumption (exponential disk): ~0.1 dex in g_bar shape");
        sb.AppendLine($"  - Combined g_bar scatter assumed: {SIGMA_LOG} dex (used in the g† fit)");
        sb.AppendLine();
        sb.AppendLine("  The stellar mass is INDEPENDENT of the BTFR prior (QG-071), so the");
        sb.AppendLine("  g†-mass degeneracy is broken at the ~0.15 dex level, not ~1 dex.");
        return sb.ToString();
    }

    private static string BuildG(RARFit[] fits, EvolutionBin[] bins, TheoryComparison[] cmp)
    {
        string verdict = Verdict(fits, cmp);
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {verdict}");
        sb.AppendLine();
        sb.AppendLine("  Central question: does the high-z RAR favor evolving or constant g†?");
        sb.AppendLine();
        var constrained = fits.Where(f => !double.IsNaN(f.LogGdagger) && f.LogGdagger_err < 0.8).ToArray();
        sb.AppendLine($"  Constrained g† estimates: {constrained.Length} / {fits.Length}");
        if (constrained.Length > 0)
            sb.AppendLine($"  g† range: {constrained.Min(f => f.Gdagger_m_s2):E1} .. {constrained.Max(f => f.Gdagger_m_s2):E1} m/s²");
        sb.AppendLine("  NOTE: this is the FIRST direct (non-circular) measurement, on a small");
        sb.AppendLine("  sample. Systematics (gas mass, inclination) still dominate the per-galaxy");
        sb.AppendLine("  scatter; a larger sample and gas-mapping are required for a decisive test.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static double Parse(string s) =>
        double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v) ? v : double.NaN;

    private static double Sq(double v) => v * v;

    private static double BootstrapStdErr(double[] logG, double[] logGerr, int seed)
    {
        int n = logG.Length;
        var rng = new Random(seed);
        var means = new List<double>(1000);
        for (int b = 0; b < 1000; b++)
        {
            double s = 0;
            for (int i = 0; i < n; i++)
            {
                int idx = rng.Next(n);
                s += logG[idx] + Gaussian(rng) * logGerr[idx];
            }
            means.Add(s / n);
        }
        return Math.Sqrt(means.Average(x => Sq(x - means.Average())));
    }

    private static double Gaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
    }
}

public sealed record BaryRow(string Object, double Z, double MStar, double MGas, double ReKpc);

public sealed record DirectGdaggerReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG,
    RARFit[] Fits,
    (string obj, double z, double gbar, double gobs)[] RarPoints,
    EvolutionBin[] Bins,
    TheoryComparison[] Comparisons);
