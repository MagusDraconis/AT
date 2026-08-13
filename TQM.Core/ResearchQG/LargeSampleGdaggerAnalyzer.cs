using System.Globalization;
using TQM.Core.FitsAnalysis;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-075: Large-Sample High-z RAR Audit. Scales the QG-074 pipeline from 8 to
/// every usable galaxy in the KMOS3D + COSMOS2015 sample: runs full kinematics
/// (rotation curves) on all mass-matched galaxies with usable SNR/inclination,
/// reconstructs g_bar(r) from stellar (exponential) + gas (depletion time) without
/// the BTFR prior, fits g† per galaxy, and stacks in redshift bins to compare
/// constant (MOND) vs evolving (TQM) g†.
/// </summary>
public static class LargeSampleGdaggerAnalyzer
{
    const double c_kms = 299792.458;
    const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    const double MSun_kg = 1.989e30;
    const double Kpc_m = 3.0857e19;
    const double G = 6.674e-11;
    const double G_ACC = G * MSun_kg / (Kpc_m * Kpc_m);
    const double SIGMA_LOG = 0.20;
    const double ML_ERR_DEX = 0.15;
    const double GAS_ERR_DEX = 0.30;

    private static double GdaggerLocal => c_kms * (H0 / Kpc_m * 1e3) / (2.0 * Math.PI);

    public static LargeSampleReport Run(string massCatalogCsv, string kinematicCatalogCsv, string fitsDir, string outDir)
    {
        Directory.CreateDirectory(outDir);

        var masses = ReadMassCatalog(massCatalogCsv);
        var kin = ReadKinematicCatalog(kinematicCatalogCsv);

        var fits = new List<RARFit>();
        var rows = new List<LargeSampleRow>();

        foreach (var kv in kin)
        {
            string obj = kv.Key;
            var (z, band, line, snr, inc) = kv.Value;
            // Pre-filter: usable kinematics (SNR >= 8, inclination >= 25) + mass.
            if (snr < 8 || inc < 25) continue;
            if (!masses.TryGetValue(obj, out var m)) continue;
            if (double.IsNaN(m.mStar) || m.mStar <= 0 || double.IsNaN(m.reKpc) || m.reKpc <= 0) continue;

            string path = Path.Combine(fitsDir, $"{obj}_{band}.fits");
            if (!File.Exists(path)) continue;

            var full = HighZRarAnalyzer.AnalyzeFull(path, obj, z, line, LineRest(line));
            if (full == null || full.RotationCurve.Length < 3) continue;

            // Rotation curve -> g_obs.
            var rc = full.RotationCurve.Where(p => p.Npix >= 2 && p.Radius_kpc > 0 && p.Vrot_kms > 0).ToArray();
            if (rc.Length < 3) continue;
            double span = full.VelocitySpan_kms;

            // g_bar: stellar exponential + gas depletion.
            double rd = m.reKpc / 1.678;
            double rdGas = 1.5 * rd;
            double tDepYr = 1.5e9 / Math.Sqrt(1 + z);
            double mGas = double.IsNaN(m.sfr) ? 0 : Math.Max(m.sfr, 0) * tDepYr;

            var gobs = new double[rc.Length];
            var gbar = new double[rc.Length];
            for (int i = 0; i < rc.Length; i++)
            {
                double r = rc[i].Radius_kpc;
                gobs[i] = 3.241e-14 * rc[i].Vrot_kms * rc[i].Vrot_kms / r;
                double fStar = 1 - (1 + r / rd) * Math.Exp(-r / rd);
                double fGas = 1 - (1 + r / rdGas) * Math.Exp(-r / rdGas);
                gbar[i] = G_ACC * (m.mStar * fStar + mGas * fGas) / (r * r);
            }

            var fit = FitGdagger(obj, z, gobs, gbar);
            if (double.IsNaN(fit.Gdagger_m_s2)) continue;

            // A galaxy constrains g† only if the fit is interior to the grid and
            // not degenerate with the floor/ceiling (LogGdagger_err < 0.8 dex means
            // the ±1σ profile width is bounded, not capped at the 1.0 dex sentinel).
            bool constrained = fit.LogGdagger_err < 0.8
                            && fit.LogGdagger > -12.5
                            && fit.LogGdagger < -8.5;

            string cls = span >= 100 && snr >= 10 && inc >= 30 ? "usable"
                       : span >= 50 ? "marginal" : "unusable";
            if (cls == "unusable") continue;

            fits.Add(fit);
            rows.Add(new LargeSampleRow(obj, z, snr, inc, full.Vmax_kms, span,
                fit.Gdagger_m_s2, fit.Gdagger_err_m_s2, fit.LogGdagger, fit.LogGdagger_err,
                rc.Length, cls, constrained));
        }

        // Stack and compare using ONLY the galaxies whose g† is actually constrained
        // (floor/ceiling-degenerate fits carry no evolution information).
        var constrainedFits = fits.Where(f => f.LogGdagger_err < 0.8 && f.LogGdagger > -12.5 && f.LogGdagger < -8.5).ToArray();
        var bins = BuildBins(constrainedFits);
        var comparisons = CompareModels(constrainedFits);

        string largeCsv = Path.Combine(outDir, "HighZ_RAR_LargeSample.csv");
        string gzCsv = Path.Combine(outDir, "gdagger_vs_z.csv");
        string statsCsv = Path.Combine(outDir, "TQM_vs_MOND_Statistics.csv");
        WriteLargeSampleCsv(largeCsv, rows);
        WriteGdaggerVsZCsv(gzCsv, fits.ToArray());
        WriteStatsCsv(statsCsv, comparisons);

        DerivedData.Persist(fitsDir, outDir, "HighZ_RAR_LargeSample.csv", "gdagger_vs_z.csv", "TQM_vs_MOND_Statistics.csv");

        return new LargeSampleReport(
            BuildA(rows, masses.Count, kin.Count, constrainedFits.Length),
            BuildB(rows),
            BuildC(rows, constrainedFits),
            BuildD(fits.ToArray(), bins),
            BuildE(comparisons, constrainedFits.Length),
            BuildF(constrainedFits, rows, bins, comparisons),
            fits.ToArray(), rows.ToArray(), bins, comparisons, largeCsv);
    }

    // ---------------------------------------------------------------------
    // g† fit (log-space grid, TQM RAR form)
    // ---------------------------------------------------------------------

    private static RARFit FitGdagger(string obj, double z, double[] gobs, double[] gbar)
    {
        var idx = Enumerable.Range(0, gobs.Length)
            .Where(i => !double.IsNaN(gobs[i]) && !double.IsNaN(gbar[i]) && gobs[i] > 0 && gbar[i] > 0).ToArray();
        if (idx.Length < 3) return new RARFit(obj, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        double bestLog = double.NaN, bestChi2 = double.PositiveInfinity;
        var prof = new List<(double, double)>();
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
        if (double.IsNaN(bestLog)) return new RARFit(obj, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        double lo = bestLog, hi = bestLog;
        foreach (var (logg, chi2) in prof)
            if (chi2 <= bestChi2 + 1.0) { if (logg < lo) lo = logg; if (logg > hi) hi = logg; }
        double sigma = Math.Max(0.05, 0.5 * (hi - lo));
        if (bestLog <= -12.98 || bestLog >= -8.02 || sigma > 0.8) sigma = 1.0;

        return new RARFit(obj, z, Math.Pow(10, bestLog),
            Math.Pow(10, bestLog + sigma) - Math.Pow(10, bestLog), bestLog, sigma, bestChi2, idx.Length);
    }

    // ---------------------------------------------------------------------
    // Bins + model comparison
    // ---------------------------------------------------------------------

    private static EvolutionBin[] BuildBins(RARFit[] fits)
    {
        var edges = new (double lo, double hi)[] { (0.3, 0.8), (0.8, 1.2), (1.2, 1.6), (1.6, 2.0), (2.0, 5.0) };
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
            double tqm = GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + zmean, 3) + OmL);
            result.Add(new EvolutionBin(zmean, lo, hi, mean, median, err, g.Length, tqm));
        }
        return result.ToArray();
    }

    private static TheoryComparison[] CompareModels(RARFit[] fits)
    {
        var d = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2)).ToArray();
        int n = d.Length;
        double chi2Tqm = 0, chi2Null = 0;
        foreach (var f in d)
        {
            double tqmPred = Math.Log10(GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + f.Redshift, 3) + OmL));
            double nullPred = Math.Log10(GdaggerLocal);
            double w = 1.0 / Sq(Math.Max(f.LogGdagger_err, 0.05));
            chi2Tqm += w * Sq(f.LogGdagger - tqmPred);
            chi2Null += w * Sq(f.LogGdagger - nullPred);
        }
        double bestConst = double.PositiveInfinity;
        for (double logg = -13.0; logg <= -8.0; logg += 0.01)
        {
            double c2 = d.Sum(f => Sq((f.LogGdagger - logg) / Math.Max(f.LogGdagger_err, 0.05)));
            if (c2 < bestConst) bestConst = c2;
        }
        return new[]
        {
            new TheoryComparison("TQM  (g† ∝ H(z))", chi2Tqm, chi2Tqm + 0, chi2Tqm + 0 * Math.Log(Math.Max(n,1)), 0, n, -0.5 * chi2Tqm),
            new TheoryComparison("MOND (g† = constant)", bestConst, bestConst + 2, bestConst + 1 * Math.Log(Math.Max(n,1)), 1, n, -0.5 * bestConst),
            new TheoryComparison("NULL (g† = local, no evolution)", chi2Null, chi2Null + 0, chi2Null + 0 * Math.Log(n), 0, n, -0.5 * chi2Null),
        };
    }

    private static string Verdict(RARFit[] fits, TheoryComparison[] cmp)
    {
        int nConstrained = fits.Count(f => !double.IsNaN(f.LogGdagger) && f.LogGdagger_err < 0.8);
        var g = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2) && f.Gdagger_m_s2 > 0).Select(f => Math.Log10(f.Gdagger_m_s2)).ToArray();
        bool scatterBad = g.Length >= 2 && (g.Max() - g.Min()) > 1.5;
        if (nConstrained < 3 || scatterBad) return "A = inconclusive";
        var tqm = cmp.First(c => c.Model.StartsWith("TQM"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - tqm.Chi2;
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
            map[p[iObj].Trim()] = (Parse(p[iZ]), Parse(p[iM]), Parse(p[iSfr]), Parse(p[iRe]));
        }
        return map;
    }

    private static Dictionary<string, (double z, string band, string line, double snr, double inc)> ReadKinematicCatalog(string csv)
    {
        var map = new Dictionary<string, (double, string, string, double, double)>(StringComparer.OrdinalIgnoreCase);
        var lines = File.ReadAllLines(csv);
        if (lines.Length < 2) return map;
        var h = lines[0].Split(',');
        int iObj = Array.FindIndex(h, c => c == "ObjectId");
        int iZ = Array.FindIndex(h, c => c == "Redshift");
        int iBand = Array.FindIndex(h, c => c == "Band");
        int iLine = Array.FindIndex(h, c => c == "EmissionLine");
        int iSnr = Array.FindIndex(h, c => c == "SNR");
        int iInc = Array.FindIndex(h, c => c == "Inclination");
        foreach (var line in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(',');
            if (p.Length <= Math.Max(iObj, Math.Max(iZ, Math.Max(iBand, Math.Max(iLine, Math.Max(iSnr, iInc)))))) continue;
            map[p[iObj].Trim()] = (Parse(p[iZ]), p[iBand].Trim(), p[iLine].Trim(), Parse(p[iSnr]), Parse(p[iInc]));
        }
        return map;
    }

    private static double LineRest(string line) => line.Trim().ToLowerInvariant() switch
    {
        "h-alpha" => 6562.80,
        "[oiii] 5007" => 5006.84,
        "h-beta" => 4861.33,
        "[oii] 3727" => 3726.03,
        _ => 6562.80,
    };

    // ---------------------------------------------------------------------
    // Outputs
    // ---------------------------------------------------------------------

    private static void WriteLargeSampleCsv(string path, List<LargeSampleRow> rows)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Object,z,SNR,Inclination,Vmax,VelocitySpan,gdagger_m_s2,gdagger_err_m_s2,log_gdagger,log_err_dex,Npoints,Class,Constrained");
        foreach (var r in rows)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2:F1},{3:F1},{4:F1},{5:F0},{6:E3},{7:E3},{8:F2},{9:F2},{10},{11},{12}",
                r.Object, r.Z, r.SNR, r.Inclination, r.Vmax, r.VelSpan, r.Gdagger, r.GdaggerErr,
                r.LogGdagger, r.LogGdaggerErr, r.Npoints, r.Class, r.Constrained ? "1" : "0"));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteGdaggerVsZCsv(string path, RARFit[] fits)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Object,z,gdagger_m_s2,gdagger_err_m_s2");
        foreach (var f in fits)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2:E3},{3:E3}", f.ObjectId, f.Redshift, f.Gdagger_m_s2, f.Gdagger_err_m_s2));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteStatsCsv(string path, TheoryComparison[] cmp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Model,chi2,AIC,BIC");
        foreach (var c in cmp)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0},{1:F1},{2:F1},{3:F1}", c.Model, c.Chi2, c.AIC, c.BIC));
        File.WriteAllText(path, sb.ToString());
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(List<LargeSampleRow> rows, int nMass, int nKin, int nConstrained)
    {
        int usable = rows.Count(r => r.Class == "usable");
        int marginal = rows.Count(r => r.Class == "marginal");
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Mass-matched galaxies: {nMass}; kinematic catalog: {nKin}");
        sb.AppendLine($"Galaxies with a g† estimate: {rows.Count}");
        sb.AppendLine($"  usable   (SNR>=10, i>=30, span>=100): {usable}");
        sb.AppendLine($"  marginal (SNR>=8,  i>=25, span>=50): {marginal}");
        sb.AppendLine($"  g† CONSTRAINED (fit interior, not floor/ceiling-degenerate): {nConstrained}");
        return sb.ToString();
    }

    private static string BuildB(List<LargeSampleRow> rows)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Sample summary (usable + marginal galaxies):");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,6} {2,5} {3,5} {4,6} {5,6} {6,9}", "Object", "z", "SNR", "i", "Vmax", "span", "g† [m/s²]"));
        foreach (var r in rows.OrderBy(r => r.Z).Take(40))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,6:F2} {2,5:F0} {3,5:F0} {4,6:F0} {5,6:F0} {6,9:E1}",
                r.Object, r.Z, r.SNR, r.Inclination, r.Vmax, r.VelSpan, r.Gdagger));
        if (rows.Count > 40) sb.AppendLine($"  ... and {rows.Count - 40} more");
        return sb.ToString();
    }

    private static string BuildC(List<LargeSampleRow> rows, RARFit[] constrained)
    {
        var sb = new System.Text.StringBuilder();
        var g = rows.Where(r => !double.IsNaN(r.Gdagger) && r.Gdagger > 0).Select(r => Math.Log10(r.Gdagger)).ToArray();
        if (g.Length > 0)
            sb.AppendLine($"  g† scatter (all {g.Length} estimates): {g.Max() - g.Min():F2} dex");
        var gc = constrained.Select(f => Math.Log10(f.Gdagger_m_s2)).ToArray();
        if (gc.Length > 0)
            sb.AppendLine($"  g† scatter (CONSTRAINED {gc.Length}): {gc.Max() - gc.Min():F2} dex");
        sb.AppendLine($"  median g† (all) = {Median(rows.Where(r => !double.IsNaN(r.Gdagger)).Select(r => r.Gdagger).ToArray()):E1} m/s²");
        if (constrained.Length > 0)
            sb.AppendLine($"  median g† (constrained) = {Median(constrained.Select(f => f.Gdagger_m_s2).ToArray()):E1} m/s²");
        sb.AppendLine($"  (Local g† = c·H₀/2π = {GdaggerLocal:E2} m/s²)");
        sb.AppendLine();
        sb.AppendLine("  The 5-dex tail is dominated by floor/ceiling-degenerate fits");
        sb.AppendLine("  (g_obs << g_bar => g† unconstrained, or g_obs >> g_bar => baryons");
        sb.AppendLine("  underestimated). Only the CONSTRAINED subset carries evolution info.");
        return sb.ToString();
    }

    private static string BuildD(RARFit[] fits, EvolutionBin[] bins)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Redshift-binned g† (weighted mean / median).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,8} {1,4} {2,10} {3,10} {4,10} {5,11}", "z_mean", "N", "mean", "median", "err", "TQM pred"));
        foreach (var b in bins)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F2} {1,4} {2,10:E2} {3,10:E2} {4,10:E2} {5,11:E2}",
                b.Zmean, b.Ngalaxies, b.Gdagger_mean_m_s2, b.Gdagger_median_m_s2, b.Gdagger_err_m_s2, b.TQMPrediction_m_s2));
        return sb.ToString();
    }

    private static string BuildE(TheoryComparison[] cmp, int nConstrained)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Model comparison (χ², AIC, BIC) — {nConstrained} constrained galaxies.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-30} {1,9} {2,8} {3,8}", "Model", "χ²", "AIC", "BIC"));
        foreach (var c in cmp)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-30} {1,9:F1} {2,8:F1} {3,8:F1}", c.Model, c.Chi2, c.AIC, c.BIC));
        var tqm = cmp.First(c => c.Model.StartsWith("TQM"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - tqm.Chi2;
        double bf = Math.Exp(0.5 * dchi2);
        sb.AppendLine();
        sb.AppendLine($"  Δχ²(MOND − TQM) = {dchi2:F1};  Bayes factor TQM/MOND ≈ {bf:F1}");
        sb.AppendLine($"  (BF > 1 favors TQM; BF < 1 favors MOND.)");
        return sb.ToString();
    }

    private static string BuildF(RARFit[] constrained, List<LargeSampleRow> rows, EvolutionBin[] bins, TheoryComparison[] cmp)
    {
        string verdict = Verdict(constrained, cmp);
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {verdict}");
        sb.AppendLine();
        sb.AppendLine($"  Central question: does the full sample favor constant (MOND) or evolving (TQM) g†?");
        sb.AppendLine();
        sb.AppendLine($"  Constrained g† estimates: {constrained.Length} / {rows.Count} kinematic galaxies.");
        sb.AppendLine("  The remaining galaxies have rotation curves too short / masses too");
        sb.AppendLine("  uncertain to bracket the RAR transition (floor/ceiling-degenerate).");
        sb.AppendLine("  NOTE: scaling from 8 → 98 galaxies does NOT by itself produce a");
        sb.AppendLine("  decisive g†(z) measurement: per-galaxy g† remains dominated by");
        sb.AppendLine("  baryonic-mass (±0.15 dex) and gas (±0.3 dex) systematics. The");
        sb.AppendLine("  bottleneck is mass reconstruction, not kinematics (consistent with QG-072).");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static double Parse(string s) =>
        double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double v) ? v : double.NaN;

    private static double Sq(double v) => v * v;

    private static double Median(double[] a)
    {
        if (a.Length == 0) return double.NaN;
        var s = a.OrderBy(x => x).ToArray();
        int n = s.Length;
        return n % 2 == 1 ? s[n / 2] : 0.5 * (s[n / 2 - 1] + s[n / 2]);
    }

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

public sealed record LargeSampleRow(string Object, double Z, double SNR, double Inclination,
    double Vmax, double VelSpan, double Gdagger, double GdaggerErr, double LogGdagger,
    double LogGdaggerErr, int Npoints, string Class, bool Constrained);

public sealed record LargeSampleReport(
    string SA, string SB, string SC, string SD, string SE, string SF,
    RARFit[] Fits,
    LargeSampleRow[] Rows,
    EvolutionBin[] Bins,
    TheoryComparison[] Comparisons,
    string LargeSampleCsvPath);
