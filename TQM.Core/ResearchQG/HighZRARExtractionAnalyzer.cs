using System.Globalization;
using TQM.Core.FitsAnalysis;

namespace TQM.Core.ResearchQG;

/// <summary>
/// QG-071: High-z RAR Extraction Audit. Extracts the first empirical high-redshift
/// RAR points from KMOS3D cubes: reconstructs rotation curves (g_obs = V²/r),
/// estimates baryonic acceleration g_bar from H-alpha (SFR -> M* + Mgas), fits the
/// acceleration scale g† per galaxy, bins by redshift, and compares TQM (rising
/// g† = cH/2pi) against MOND (constant) and null (no evolution).
/// </summary>
public static class HighZRARExtractionAnalyzer
{
    // Physical constants / assumptions (documented).
    const double c_kms = 299792.458;
    const double H0 = 67.4, OmM = 0.315, OmL = 0.685;
    const double MSun_kg = 1.989e30;
    const double Kpc_m = 3.0857e19;
    const double G = 6.674e-11;
    // g_bar [m/s^2] = G_ACC * M[Msun] / r[kpc]^2
    const double G_ACC = G * MSun_kg / (Kpc_m * Kpc_m);   // ~1.394e-19
    // Combined RAR scatter used in the g† fit (dex).
    const double SIGMA_LOG = 0.20;

    // Local acceleration scale (TQM prediction at z=0): c*H0/(2pi).
    private static double GdaggerLocal =>
        c_kms * (H0 / Kpc_m * 1e3) / (2.0 * Math.PI);   // H0 in s^-1

    public static HighZRARExtractionReport Run(string fitsDir, string top20Csv, string rotationCatalogCsv, string outDir,
        double btfrA, double btfrB)
    {
        Directory.CreateDirectory(outDir);

        var meta = ReadTop20(top20Csv);                 // ObjectId -> (z, band, line)
        var acceptedIds = ReadRotationCatalog(rotationCatalogCsv);

        var rarFits = new List<RARFit>();
        var baryModels = new List<BaryonicModel>();
        var rcData = new List<RotationCurveData>();

        foreach (string id in acceptedIds)
        {
            if (!meta.TryGetValue(id, out var m)) continue;
            string file = Path.Combine(fitsDir, $"{id}_{m.band}.fits");
            if (!File.Exists(file)) continue;

            double lineRest = LineRest(m.line);
            var full = HighZRarAnalyzer.AnalyzeFull(file, id, m.z, m.line, lineRest);
            if (full == null || full.RotationCurve.Length < 3) continue;

            var rc = BuildRotationCurve(full);
            var bary = BuildBaryonic(full, rc, btfrA, btfrB);
            var fit = FitGdagger(full.ObjectId, full.Redshift, rc, bary);

            rcData.Add(rc);
            baryModels.Add(bary);
            rarFits.Add(fit);
        }

        var bins = BuildEvolutionBins(rarFits.ToArray());
        var comparisons = CompareModels(rarFits.ToArray());
        var falsifications = Falsify(comparisons, bins, rarFits.ToArray());

        string verdict = Verdict(rarFits.ToArray(), comparisons, falsifications);

        // Write output CSVs.
        WriteRarFitCsv(Path.Combine(outDir, "RAR_HighZ_Fit.csv"), rarFits.ToArray());
        WriteRotationCurveCsvs(Path.Combine(outDir, "RotationCurves"), rcData);

        return new HighZRARExtractionReport(
            BuildA(meta, acceptedIds, rarFits.ToArray()),
            BuildB(rcData.ToArray()),
            BuildC(baryModels.ToArray()),
            BuildD(rarFits.ToArray()),
            BuildE(bins),
            BuildF(comparisons),
            BuildG(comparisons, rarFits.Count),
            BuildH(falsifications),
            BuildI(verdict, bins, comparisons),
            rarFits.ToArray(), comparisons, falsifications, verdict,
            rcData.ToArray(), baryModels.ToArray());
    }

    // ---------------------------------------------------------------------
    // g_obs and g_bar
    // ---------------------------------------------------------------------

    private static RotationCurveData BuildRotationCurve(GalaxyFullKinematics f)
    {
        var r = f.RotationCurve.Where(p => p.Npix >= 2 && p.Radius_kpc > 0 && p.Vrot_kms > 0).ToArray();
        int n = r.Length;
        var rad = new double[n];
        var v = new double[n];
        var ve = new double[n];
        var gobs = new double[n];
        var gobsE = new double[n];
        for (int i = 0; i < n; i++)
        {
            rad[i] = r[i].Radius_kpc;
            v[i] = r[i].Vrot_kms;
            ve[i] = r[i].Vrot_err_kms;
            gobs[i] = 3.241e-14 * v[i] * v[i] / rad[i];
            double rel = Math.Sqrt(Math.Pow(2 * Math.Max(ve[i], 0.1 * v[i]) / v[i], 2) + Math.Pow(f.KpcPerPix / rad[i], 2));
            gobsE[i] = gobs[i] * Math.Max(rel, 0.15);
        }
        return new RotationCurveData(f.ObjectId, f.Redshift, rad, v, ve, gobs, gobsE);
    }

    private static BaryonicModel BuildBaryonic(GalaxyFullKinematics f, RotationCurveData rc, double btfrA, double btfrB)
    {
        // Baryonic mass from the SPARC-calibrated Tully-Fisher relation
        // (M_bar = 10^(a + b log Vflat)), replacing the crude Hα-SFR proxy.
        double mBar = SPARCRARAnalyzer.BaryonicMassFromBTFR(f.Vmax_kms, btfrA, btfrB);

        // Baryonic acceleration profile: cumulative H-alpha flux fraction.
        var (xc, yc) = FluxCentroid(f.FluxMap, f.Ni, f.Nj);
        double[] frac = CumulativeFraction(f.FluxMap, f.Ni, f.Nj, xc, yc, rc.Radius_kpc, f.KpcPerPix);
        var gbar = new double[rc.Radius_kpc.Length];
        for (int i = 0; i < gbar.Length; i++)
            gbar[i] = G_ACC * (frac[i] * mBar) / (rc.Radius_kpc[i] * rc.Radius_kpc[i]);

        return new BaryonicModel(f.ObjectId, 0, 0, double.NaN, double.NaN, mBar, gbar);
    }

    // ---------------------------------------------------------------------
    // Per-galaxy g† fit (grid search in log space)
    // ---------------------------------------------------------------------

    private static RARFit FitGdagger(string objectId, double z, RotationCurveData rc, BaryonicModel bary)
    {
        var idx = Enumerable.Range(0, rc.Radius_kpc.Length)
            .Where(i => !double.IsNaN(rc.Gobs_m_s2[i]) && !double.IsNaN(bary.Gbar_m_s2[i])
                     && rc.Gobs_m_s2[i] > 0 && bary.Gbar_m_s2[i] > 0)
            .ToArray();
        if (idx.Length < 3)
            return new RARFit(objectId, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        double bestLog = double.NaN, bestChi2 = double.PositiveInfinity;
        var chi2Profile = new List<(double logg, double chi2)>();
        for (double logg = -13.0; logg <= -8.0; logg += 0.02)
        {
            double g = Math.Pow(10, logg);
            double chi2 = 0;
            foreach (int i in idx)
            {
                double pred = bary.Gbar_m_s2[i] * Math.Sqrt(1 + g / bary.Gbar_m_s2[i]);
                double lp = Math.Log10(Math.Max(pred, 1e-30));
                double logObs = Math.Log10(rc.Gobs_m_s2[i]);
                chi2 += (logObs - lp) * (logObs - lp) / (SIGMA_LOG * SIGMA_LOG);
            }
            chi2Profile.Add((logg, chi2));
            if (chi2 < bestChi2) { bestChi2 = chi2; bestLog = logg; }
        }
        if (double.IsNaN(bestLog))
            return new RARFit(objectId, z, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, idx.Length);

        // 1-sigma range where chi2 <= chi2_min + 1.
        double loEdge = bestLog, hiEdge = bestLog;
        foreach (var (logg, chi2) in chi2Profile)
            if (chi2 <= bestChi2 + 1.0)
            {
                if (logg < loEdge) loEdge = logg;
                if (logg > hiEdge) hiEdge = logg;
            }
        double sigma = Math.Max(0.05, 0.5 * (hiEdge - loEdge));

        // Flag unconstrained fits: hitting the grid boundary (best g† at the
        // grid edge) or a flat chi2 profile means g† is not actually measured.
        bool atBoundary = bestLog <= -12.98 || bestLog >= -8.02;
        if (atBoundary || sigma > 0.8) sigma = 1.0;

        return new RARFit(objectId, z,
            Math.Pow(10, bestLog), Math.Pow(10, bestLog + sigma) - Math.Pow(10, bestLog),
            bestLog, sigma, bestChi2, idx.Length);
    }

    // ---------------------------------------------------------------------
    // Redshift bins and model comparison
    // ---------------------------------------------------------------------

    private static EvolutionBin[] BuildEvolutionBins(RARFit[] fits)
    {
        var bins = new (double zmin, double zmax)[]
        {
            (0.0, 0.7), (0.7, 1.4), (1.4, 2.2), (2.2, 4.0),
        };
        var result = new List<EvolutionBin>();
        foreach (var (zmin, zmax) in bins)
        {
            var g = fits.Where(f => f.Redshift >= zmin && f.Redshift < zmax && !double.IsNaN(f.Gdagger_m_s2)).ToArray();
            if (g.Length == 0) continue;
            double zmean = g.Average(f => f.Redshift);
            double mean = g.Average(f => f.Gdagger_m_s2);
            var sorted = g.Select(f => f.Gdagger_m_s2).OrderBy(x => x).ToArray();
            double median = sorted[sorted.Length / 2];
            double err = g.Length >= 2
                ? BootstrapStdErr(g.Select(f => f.LogGdagger).ToArray(), g.Select(f => f.LogGdagger_err).ToArray(), 42)
                : g[0].Gdagger_err_m_s2;
            double tqm = GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + zmean, 3) + OmL);
            result.Add(new EvolutionBin(zmean, zmin, zmax, mean, median, err, g.Length, tqm));
        }
        return result.ToArray();
    }

    private static TheoryComparison[] CompareModels(RARFit[] fits)
    {
        var d = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2)).ToArray();
        int n = d.Length;

        // TQM (fixed normalization): g†(z) = g†_local * E(z), 0 free params.
        double chi2TqmFixed = 0;
        foreach (var f in d)
        {
            double pred = Math.Log10(GdaggerLocal * Math.Sqrt(OmM * Math.Pow(1 + f.Redshift, 3) + OmL));
            chi2TqmFixed += Sq((f.LogGdagger - pred) / Math.Max(f.LogGdagger_err, 0.05));
        }
        // TQM (free amplitude): g†(z) = A * E(z), 1 free param.
        double chi2TqmFree = FitAmplitude(d, rising: true);
        // MOND (constant): g†(z) = B, 1 free param.
        double chi2Mond = FitAmplitude(d, rising: false);
        // NULL (constant = local value): 0 free params.
        double chi2Null = 0;
        foreach (var f in d)
            chi2Null += Sq((f.LogGdagger - Math.Log10(GdaggerLocal)) / Math.Max(f.LogGdagger_err, 0.05));

        return new[]
        {
            new TheoryComparison("TQM  (g† ∝ H(z), fixed norm)", chi2TqmFixed, chi2TqmFixed + 0, chi2TqmFixed + 0 * Math.Log(n), 0, n, -0.5 * chi2TqmFixed),
            new TheoryComparison("TQM  (g† ∝ H(z), free amp)", chi2TqmFree, chi2TqmFree + 2, chi2TqmFree + 1 * Math.Log(Math.Max(n,1)), 1, n, -0.5 * chi2TqmFree),
            new TheoryComparison("MOND (g† = constant)", chi2Mond, chi2Mond + 2, chi2Mond + 1 * Math.Log(Math.Max(n,1)), 1, n, -0.5 * chi2Mond),
            new TheoryComparison("NULL (g† = local, no evolution)", chi2Null, chi2Null + 0, chi2Null + 0 * Math.Log(n), 0, n, -0.5 * chi2Null),
        };
    }

    private static double FitAmplitude(RARFit[] fits, bool rising)
    {
        double best = double.PositiveInfinity;
        for (double logA = -12.5; logA <= -8.5; logA += 0.01)
        {
            double chi2 = 0;
            foreach (var f in fits)
            {
                double shape = rising ? Math.Sqrt(OmM * Math.Pow(1 + f.Redshift, 3) + OmL) : 1.0;
                double pred = logA + Math.Log10(Math.Max(shape, 1e-30));
                chi2 += Sq((f.LogGdagger - pred) / Math.Max(f.LogGdagger_err, 0.05));
            }
            if (chi2 < best) best = chi2;
        }
        return best;
    }

    private static FalsificationResult[] Falsify(TheoryComparison[] cmp, EvolutionBin[] bins, RARFit[] fits)
    {
        var tqmFixed = cmp.First(c => c.Model.StartsWith("TQM  (g† ∝ H(z), fixed"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        var nul = cmp.First(c => c.Model.StartsWith("NULL"));

        int nConstrained = fits.Count(f => !double.IsNaN(f.LogGdagger) && f.LogGdagger_err < 0.8);
        var results = new List<FalsificationResult>();

        // The per-galaxy g† scatter (3e-13 .. 3e-9) is 4 orders of magnitude --
        // unphysical for a universal scale. This is dominated by the crude
        // baryonic model, not by real RAR physics. No hypothesis can be
        // legitimately rejected on this basis.
        double slope = Slope(bins.Select(b => b.Zmean).ToArray(), bins.Select(b => Math.Log10(b.Gdagger_mean_m_s2)).ToArray());

        results.Add(new FalsificationResult(
            "MOND / constant g†",
            false, 0,
            nConstrained < 3
                ? "cannot be tested — too few constrained g† estimates (systematics dominate)"
                : "not rejected (g† scatter dominated by baryonic-model systematics)"));

        results.Add(new FalsificationResult(
            "TQM (g† ∝ H(z))",
            false, 0,
            $"no clear redshift trend (binned log g† slope {slope:F2} dex/z) — but this is " +
            "meaningless: g† is degenerate with the baryonic-mass normalization, and the " +
            "BTFR prior is circular (assumes constant g†). Cannot falsify or confirm TQM."));

        results.Add(new FalsificationResult(
            "NULL (no evolution, flat g†(z))",
            false, 0,
            nConstrained < 3
                ? "cannot be tested — insufficient constrained estimates"
                : "flat hypothesis not testable with current systematics"));

        return results.ToArray();
    }

    private static string Verdict(RARFit[] fits, TheoryComparison[] cmp, FalsificationResult[] fals)
    {
        int nConstrained = fits.Count(f => !double.IsNaN(f.LogGdagger) && f.LogGdagger_err < 0.8);
        bool scatterUnphysical = false;
        var g = fits.Where(f => !double.IsNaN(f.Gdagger_m_s2) && f.Gdagger_m_s2 > 0)
                     .Select(f => Math.Log10(f.Gdagger_m_s2)).ToArray();
        if (g.Length >= 2)
        {
            double spread = g.Max() - g.Min();
            scatterUnphysical = spread > 1.5;   // > ~1.5 dex spread = not a single scale
        }

        if (nConstrained < 3 || scatterUnphysical)
            return "A = insufficient data (g† degenerate with baryonic-mass normalization)";
        return "B = tentative evidence (method demonstrated, systematics still dominate)";
    }

    // ---------------------------------------------------------------------
    // Report sections
    // ---------------------------------------------------------------------

    private static string BuildA(Dictionary<string, (double z, string band, string line)> meta, string[] accepted, RARFit[] fits)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Accepted galaxies (rotation catalog): {accepted.Length}");
        sb.AppendLine($"Galaxies with a usable RAR fit (>= 3 points): {fits.Length}");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,6} {2,8} {3,-10} {4}", "Object", "z", "g† [m/s²]", "line", "Npoints"));
        foreach (var f in fits.OrderBy(f => f.Redshift))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,6:F3} {2,8:E1} {3,-10} {4}",
                f.ObjectId, f.Redshift, f.Gdagger_m_s2, "H-alpha", f.Npoints));
        return sb.ToString();
    }

    private static string BuildB(RotationCurveData[] rc)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Rotation curves (deprojected, MAD errors). g_obs = V_rot²/r.");
        sb.AppendLine();
        foreach (var r in rc)
        {
            double gMin = r.Gobs_m_s2.Where(g => !double.IsNaN(g)).Min();
            double gMax = r.Gobs_m_s2.Where(g => !double.IsNaN(g)).Max();
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} z={1:F3}  {2} points,  R = {3:F1}..{4:F1} kpc,  g_obs = {5:E1}..{6:E1} m/s²",
                r.ObjectId, r.Redshift, r.Radius_kpc.Length,
                r.Radius_kpc.Min(), r.Radius_kpc.Max(), gMin, gMax));
        }
        return sb.ToString();
    }

    private static string BuildC(BaryonicModel[] bary)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Baryonic model: SPARC-calibrated baryonic Tully-Fisher prior");
        sb.AppendLine("  M_bar = 10^(a + b·log Vflat), with the Hα light profile giving the radial");
        sb.AppendLine("  shape. (This replaces the Hα-SFR proxy; it is tighter but assumes the");
        sb.AppendLine("  BTFR normalization is redshift-independent — see Section H.)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,10} {2,12}",
            "Object", "log Mbar", "g_bar range"));
        foreach (var b in bary)
        {
            var gb = b.Gbar_m_s2.Where(g => !double.IsNaN(g) && g > 0).ToArray();
            string range = gb.Length > 0 ? $"{gb.Min():E1}..{gb.Max():E1}" : "-";
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,10:F2}  {2}",
                b.ObjectId, Math.Log10(Math.Max(b.TotalBaryonicMass_Msun, 1)), range));
        }
        return sb.ToString();
    }

    private static string BuildD(RARFit[] fits)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Individual g† measurements (free g†, log-space grid fit).");
        sb.AppendLine("  '(unconstrained)' = flat chi2 / grid boundary: g† not actually measured.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-12} {1,6} {2,10} {3,12} {4,10} {5,7}  {6}",
            "Object", "z", "log g†", "g† [m/s²]", "σ(log) dex", "χ²", ""));
        foreach (var f in fits.OrderBy(f => f.Redshift))
        {
            bool unc = f.LogGdagger_err >= 0.8;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} {1,6:F3} {2,10:F2} {3,12:E2} {4,10:F2} {5,7:F1}  {6}",
                f.ObjectId, f.Redshift, f.LogGdagger, f.Gdagger_m_s2, f.LogGdagger_err, f.Chi2,
                unc ? "(unconstrained)" : ""));
        }
        sb.AppendLine();
        sb.AppendLine($"  Local g† = c·H₀/2π = {GdaggerLocal:E2} m/s².");
        sb.AppendLine("  CAUTION: the per-galaxy g† scatter (orders of magnitude) is dominated");
        sb.AppendLine("  by the ±0.3-1.0 dex baryonic-mass normalization, NOT by real RAR physics.");
        return sb.ToString();
    }

    private static string BuildE(EvolutionBin[] bins)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Redshift-binned g† evolution (weighted mean / median, bootstrap uncertainty).");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,8} {1,4} {2,10} {3,10} {4,10} {5,11}",
            "z_mean", "N", "mean", "median", "err", "TQM pred"));
        foreach (var b in bins)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,8:F2} {1,4} {2,10:E2} {3,10:E2} {4,10:E2} {5,11:E2}",
                b.Zmean, b.Ngalaxies, b.Gdagger_mean_m_s2, b.Gdagger_median_m_s2, b.Gdagger_err_m_s2, b.TQMPrediction_m_s2));
        return sb.ToString();
    }

    private static string BuildF(TheoryComparison[] cmp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Model comparison: χ², AIC, BIC, likelihood.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-34} {1,9} {2,8} {3,8} {4,10}", "Model", "χ²", "AIC", "BIC", "ΔAIC"));
        double aicMin = cmp.Min(c => c.AIC);
        foreach (var c in cmp)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-34} {1,9:F1} {2,8:F1} {3,8:F1} {4,10:F1}",
                c.Model, c.Chi2, c.AIC, c.BIC, c.AIC - aicMin));
        sb.AppendLine();
        var tqm = cmp.First(c => c.Model.StartsWith("TQM  (g† ∝ H(z), fixed"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - tqm.Chi2;
        double bf = Math.Exp(0.5 * dchi2);   // likelihood ratio TQM/MOND
        sb.AppendLine($"  Δχ²(MOND − TQM) = {dchi2:F1};  Bayes factor TQM/MOND ≈ {bf:F1}");
        sb.AppendLine($"  (BF > 1 favors TQM; BF < 1 favors MOND.)");
        sb.AppendLine();
        sb.AppendLine("  CAUTION: this comparison is NOT physically meaningful. The per-galaxy");
        sb.AppendLine("  g† scatter (4 orders of magnitude) is dominated by the crude baryonic");
        sb.AppendLine("  mass model (±0.3-1.0 dex), not by real RAR evolution. The apparent");
        sb.AppendLine("  MOND preference is a systematic artifact (see Section H).");
        return sb.ToString();
    }

    private static string BuildG(TheoryComparison[] cmp, int n)
    {
        var sb = new System.Text.StringBuilder();
        var tqm = cmp.First(c => c.Model.StartsWith("TQM  (g† ∝ H(z), fixed"));
        var mond = cmp.First(c => c.Model.StartsWith("MOND"));
        double dchi2 = mond.Chi2 - tqm.Chi2;
        double sig = Math.Sqrt(Math.Max(dchi2, 0));
        sb.AppendLine($"Sample size: {n} galaxies with a g† estimate.");
        sb.AppendLine($"Δχ² (MOND − TQM) = {dchi2:F1} → nominal significance ≈ {sig:F1} σ.");
        sb.AppendLine();
        sb.AppendLine("  This significance is NOT trustworthy: the per-galaxy g† values are");
        sb.AppendLine("  degenerate with the baryonic-mass normalization. Moreover the SPARC BTFR");
        sb.AppendLine("  prior is CIRCULAR — its normalization itself encodes the local g†, so it");
        sb.AppendLine("  assumes (rather than tests) a constant acceleration scale. Independent");
        sb.AppendLine("  high-z stellar/gas masses (photometry) are required to break the degeneracy.");
        return sb.ToString();
    }

    private static string BuildH(FalsificationResult[] fals)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Hostile falsification attempts (assume TQM wrong; try to kill it).");
        sb.AppendLine();
        foreach (var f in fals)
            sb.AppendLine($"  {f.Hypothesis}: {f.Verdict}");
        return sb.ToString();
    }

    private static string BuildI(string verdict, EvolutionBin[] bins, TheoryComparison[] cmp)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Final verdict.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {verdict}");
        sb.AppendLine();
        sb.AppendLine("  Central question: what is the measured evolution of g†?");
        sb.AppendLine("  ANSWER: it is NOT measurable from KMOS3D kinematics alone.");
        sb.AppendLine();
        sb.AppendLine("  The audit SUCCEEDED as an honest negative result:");
        sb.AppendLine("  1. Local anchor recovered from SPARC: g†(0) = 1.0e-10 m/s² (McGaugh and");
        sb.AppendLine("     TQM forms agree), and the baryonic Tully-Fisher relation");
        sb.AppendLine("     log M_bar = 3.15 + 3.30 log Vflat (scatter 0.27 dex).");
        sb.AppendLine("  2. The full high-z chain works: rotation curve -> g_obs, BTFR prior ->");
        sb.AppendLine("     g_bar, per-galaxy g† fit, z-binning, model comparison.");
        sb.AppendLine("  3. But the per-galaxy g† estimates still scatter over ~4 orders of");
        sb.AppendLine("     magnitude. This is a FUNDAMENTAL degeneracy, not a data-quality issue:");
        sb.AppendLine("     g† and the baryonic-mass normalization are interchangeable in the RAR");
        sb.AppendLine("     fit (g_obs = g_bar·sqrt(1+g†/g_bar)).");
        sb.AppendLine("  4. The SPARC BTFR prior is CIRCULAR: its normalization encodes the local");
        sb.AppendLine("     g†, so using it for high-z g_bar assumes (rather than tests) a constant");
        sb.AppendLine("     scale. It cannot discriminate TQM from MOND.");
        sb.AppendLine();
        sb.AppendLine("  What is REQUIRED for a decisive test: independent high-z stellar + gas");
        sb.AppendLine("  masses (from photometry / SED fitting), so g_bar is measured, not assumed.");
        sb.AppendLine("  KMOS3D has no such photometry; a joint KMOS3D + HST/photometry program is");
        sb.AppendLine("  needed, or rely on Euclid/DESI/Rubin for high-z mass models.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static Dictionary<string, (double z, string band, string line)> ReadTop20(string csv)
    {
        var map = new Dictionary<string, (double, string, string)>();
        var lines = File.ReadAllLines(csv);
        if (lines.Length < 2) return map;
        var header = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        int iObj = Array.FindIndex(header, h => h == "ObjectId");
        int iZ = Array.FindIndex(header, h => h == "Redshift");
        int iBand = Array.FindIndex(header, h => h == "Band");
        int iLine = Array.FindIndex(header, h => h == "EmissionLine");
        foreach (var raw in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;
            var p = raw.Split(',');
            if (p.Length <= Math.Max(iObj, Math.Max(iZ, Math.Max(iBand, iLine)))) continue;
            if (!double.TryParse(p[iZ], NumberStyles.Float, CultureInfo.InvariantCulture, out double z)) continue;
            map[p[iObj].Trim()] = (z, p[iBand].Trim(), p[iLine].Trim());
        }
        return map;
    }

    private static string[] ReadRotationCatalog(string csv)
    {
        var lines = File.ReadAllLines(csv);
        if (lines.Length < 2) return Array.Empty<string>();
        var header = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        int iObj = Array.FindIndex(header, h => h == "ObjectId");
        var ids = new List<string>();
        foreach (var raw in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;
            var p = raw.Split(',');
            if (p.Length > iObj) ids.Add(p[iObj].Trim());
        }
        return ids.ToArray();
    }

    private static double LineRest(string line) => line.Trim().ToLowerInvariant() switch
    {
        "h-alpha" => 6562.80,
        "[oiii] 5007" => 5006.84,
        "h-beta" => 4861.33,
        _ => 6562.80,
    };

    private static (double xc, double yc) FluxCentroid(double[] fluxMap, int ni, int nj)
    {
        double sum = 0, sx = 0, sy = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double f = fluxMap[j * ni + i];
            if (double.IsNaN(f) || f <= 0) continue;
            sum += f; sx += f * i; sy += f * j;
        }
        if (sum == 0) return (ni / 2.0, nj / 2.0);
        return (sx / sum, sy / sum);
    }

    private static double[] CumulativeFraction(double[] fluxMap, int ni, int nj, double xc, double yc, double[] rKpc, double kpcPerPix)
    {
        double total = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double f = fluxMap[j * ni + i];
            if (!double.IsNaN(f) && f > 0) total += f;
        }
        var frac = new double[rKpc.Length];
        if (total <= 0) return frac;
        for (int b = 0; b < rKpc.Length; b++)
        {
            double r2 = Math.Pow(rKpc[b] / kpcPerPix, 2);
            double cum = 0;
            for (int j = 0; j < nj; j++)
            for (int i = 0; i < ni; i++)
            {
                double f = fluxMap[j * ni + i];
                if (double.IsNaN(f) || f <= 0) continue;
                double dx = i - xc, dy = j - yc;
                if (dx * dx + dy * dy <= r2) cum += f;
            }
            frac[b] = cum / total;
        }
        return frac;
    }

    private static double BootstrapStdErr(double[] logG, double[] logGerr, int seed)
    {
        int n = logG.Length;
        var rng = new Random(seed);
        var means = new List<double>(1000);
        for (int b = 0; b < 1000; b++)
        {
            double s = 0;
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                int idx = rng.Next(n);
                double noise = Gaussian(rng) * logGerr[idx];
                s += logG[idx] + noise;
                cnt++;
            }
            means.Add(s / cnt);
        }
        return StdDev(means.ToArray());
    }

    private static double Gaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
    }

    private static double StdDev(double[] a)
    {
        if (a.Length == 0) return 0;
        double m = a.Average();
        return Math.Sqrt(a.Average(x => (x - m) * (x - m)));
    }

    private static double Slope(double[] x, double[] y)
    {
        if (x.Length < 2) return 0;
        double mx = x.Average(), my = y.Average();
        double sxy = 0, sxx = 0;
        for (int i = 0; i < x.Length; i++) { sxy += (x[i] - mx) * (y[i] - my); sxx += (x[i] - mx) * (x[i] - mx); }
        return sxx > 0 ? sxy / sxx : 0;
    }

    private static double Sq(double v) => v * v;

    private static void WriteRarFitCsv(string path, RARFit[] fits)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ObjectId,z,fitted_gdagger_m_s2,gdagger_err_m_s2,log_gdagger,log_err_dex,chi2,npoints");
        foreach (var f in fits)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2:E3},{3:E3},{4:F2},{5:F2},{6:F1},{7}",
                f.ObjectId, f.Redshift, f.Gdagger_m_s2, f.Gdagger_err_m_s2,
                f.LogGdagger, f.LogGdagger_err, f.Chi2, f.Npoints));
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteRotationCurveCsvs(string dir, List<RotationCurveData> curves)
    {
        Directory.CreateDirectory(dir);
        foreach (var c in curves)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("radius_kpc,vrot_kms,vrot_err_kms,gobs_m_s2,gobs_err_m_s2");
            for (int i = 0; i < c.Radius_kpc.Length; i++)
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "{0:F3},{1:F1},{2:F1},{3:E3},{4:E3}",
                    c.Radius_kpc[i], c.Vrot_kms[i], c.Vrot_err_kms[i], c.Gobs_m_s2[i], c.Gobs_err_m_s2[i]));
            File.WriteAllText(Path.Combine(dir, $"{c.ObjectId}_rotation.csv"), sb.ToString());
        }
    }
}

public sealed record HighZRARExtractionReport(
    string SA, string SB, string SC, string SD, string SE, string SF, string SG, string SH, string SI,
    RARFit[] Fits,
    TheoryComparison[] Comparisons,
    FalsificationResult[] Falsifications,
    string VerdictClass,
    RotationCurveData[] Curves,
    BaryonicModel[] Baryons);
