using System.Globalization;
using nom.tam.fits;

namespace TQM.Core.FitsAnalysis;

/// <summary>
/// Scans a directory of KMOS3D IFU cubes and builds a kinematic candidate
/// catalog for the QG-070 RAR-evolution test. For every cube it reads the
/// header, identifies the dominant emission line, estimates the redshift,
/// measures a per-pixel line flux/SNR map, derives the spatial size, axis
/// ratio and inclination, computes a KinematicScore, and ranks all galaxies.
/// </summary>
public static class KinematicCatalogAnalyzer
{
    const double c_kms = 299792.458;

    // Rest-frame emission-line catalogue (Angstrom).
    private static readonly (string Name, double Wave)[] Lines =
    {
        ("[OII] 3727",  3726.03),
        ("H-delta",     4101.73),
        ("H-gamma",     4340.47),
        ("H-beta",      4861.33),
        ("[OIII] 4959", 4958.91),
        ("[OIII] 5007", 5006.84),
        ("[NII] 6548",  6548.05),
        ("H-alpha",     6562.80),
        ("[NII] 6583",  6583.45),
        ("[SII] 6717",  6716.44),
        ("[SII] 6731",  6730.82),
    };

    // Lines that can physically dominate an integrated spectrum. Companions
    // ([NII], [SII], [OIII]4959) are never the primary line, which prevents
    // spurious identifications from the [SII]-doublet / H-alpha degeneracy.
    private static readonly string[] PrimaryLines =
    {
        "H-alpha", "[OIII] 5007", "[OII] 3727", "H-beta",
    };

    public static CatalogReport Run(string fitsDir, string outDir)
    {
        Directory.CreateDirectory(outDir);

        var files = Directory.GetFiles(fitsDir, "*.fits")
            .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var entries = new List<CatalogEntry>();
        var details = new List<CatalogDetails>();

        foreach (string file in files)
        {
            try
            {
                if (AnalyzeFile(file, out var e, out var d))
                {
                    entries.Add(e);
                    details.Add(d);
                }
            }
            catch
            {
                // Skip unreadable/corrupt cubes; keep the catalog robust.
            }
        }

        // Rank by KinematicScore descending.
        var ranked = entries
            .Select((e, idx) => (entry: e, detail: details[idx]))
            .OrderByDescending(x => x.entry.KinematicScore)
            .ToArray();

        var rankedEntries = ranked.Select(x => x.entry).ToArray();
        var rankedDetails = ranked.Select(x => x.detail).ToArray();

        string csvPath = Path.Combine(outDir, "KMOS3D_KinematicCatalog.csv");
        string top20Path = Path.Combine(outDir, "Top20_KinematicCandidates.csv");
        WriteCatalogCsv(csvPath, rankedEntries);
        WriteTop20Csv(top20Path, rankedEntries.Take(20).ToArray(), rankedDetails.Take(20).ToArray());

        string top20Table = BuildTop20Table(rankedEntries.Take(20).ToArray(), rankedDetails.Take(20).ToArray());
        string summary = BuildSummary(rankedEntries, rankedDetails);

        return new CatalogReport(summary, rankedEntries, rankedDetails, top20Table, csvPath, top20Path);
    }

    // ---------------------------------------------------------------------
    // Single-file analysis
    // ---------------------------------------------------------------------

    private static bool AnalyzeFile(string path, out CatalogEntry entry, out CatalogDetails detail)
    {
        entry = null!;
        detail = null!;

        var fits = new Fits(path);
        try
        {
            BasicHDU[] hdus = fits.Read();
            return AnalyzeHdus(hdus, path, out entry, out detail);
        }
        finally
        {
            fits.Close();
        }
    }

    private static bool AnalyzeHdus(BasicHDU[] hdus, string path,
        out CatalogEntry entry, out CatalogDetails detail)
    {
        entry = null!;
        detail = null!;

        // Primary header: target metadata.
        Header prim = hdus[0].Header;
        string objectId = FitsHeaderReport.SafeString(prim, "OBJECT");
        if (string.IsNullOrEmpty(objectId)) objectId = Path.GetFileNameWithoutExtension(path);
        string band = FitsHeaderReport.SafeString(prim, "OBSBAND");
        if (string.IsNullOrEmpty(band)) band = InferBandFromFileName(path);
        double exptimeMin = FitsHeaderReport.SafeDouble(prim, "EXPTIME", double.NaN);
        double ra = FitsHeaderReport.SafeDouble(prim, "RA", double.NaN);
        double dec = FitsHeaderReport.SafeDouble(prim, "DEC", double.NaN);

        // Science (flux) cube.
        var fc = FitsCubeInspector.FindScienceCube(hdus);
        if (fc is not { } c) return false;
        ImageHDU fluxHdu = c.hdu;
        Array jagged = (Array)c.data;

        int nk = jagged.Length;
        int nj = ((Array)jagged.GetValue(0)).Length;
        int ni = ((Array)((Array)jagged.GetValue(0)).GetValue(0)).Length;
        string dims = $"{ni}x{nj}x{nk}";

        double[] wl = FitsCubeInspector.ComputeWavelength(fluxHdu, nk) ?? Array.Empty<double>();

        // Noise cube (3D, same shape as flux).
        ImageHDU? noiseHdu = FindHduByName(hdus, "noise");
        double[,,] cube = LoadCube(jagged, nk, nj, ni);
        double[,,] noise = new double[nk, nj, ni];
        if (noiseHdu != null && noiseHdu.Data is ImageData nd && nd.DataArray is Array njag)
        {
            try { noise = LoadCube(njag, nk, nj, ni); } catch { /* fallback MAD */ }
        }

        // Collapsed spectrum + line identification / redshift.
        string emissionLine = "";
        double z = double.NaN;
        double lambdaObs = double.NaN;
        int kLine = -1;
        if (wl.Length > 0)
        {
            double[] spec = FitsCubeInspector.CollapseSpectrum(jagged);
            var id = IdentifyRedshift(wl, spec);
            if (id is { } line)
            {
                emissionLine = line.name;
                z = line.z;
                lambdaObs = line.name != "" ? wl[NearestIndex(wl, line.lambdaObs)] : double.NaN;
                kLine = NearestIndex(wl, line.lambdaObs);
            }
        }

        if (kLine < 0)
        {
            // No usable emission line -> classification A.
            double ba = 1, inc = 45;
            entry = new CatalogEntry(objectId, double.NaN, band, exptimeMin,
                "(none)", double.NaN, 0, ba, inc, 0);
            detail = new CatalogDetails(objectId, ra, dec, dims, 0, 0, 0, 0, 0,
                0, 0, 0, "A = unusable", "", LambdaRange(wl));
            return true;
        }

        // Per-pixel line flux / SNR / velocity.
        int fitted = 0, good = 0;
        var fluxMap = new double[nj * ni];
        var snrMap = new double[nj * ni];
        var velMap = new double[nj * ni];
        Array.Fill(fluxMap, double.NaN);
        Array.Fill(snrMap, double.NaN);
        Array.Fill(velMap, double.NaN);

        // Robust global noise level from the noise cube (median of valid values),
        // with a per-pixel MAD fallback. Sky-subtracted cubes have ~zero
        // continuum, so an edge-RMS fallback would be ~0 and inflate SNR.
        double globalNoise = GlobalNoise(noise, cube, nk, nj, ni);

        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            var r = FitLinePixel(cube, noise, wl, nk, j, i, kLine, globalNoise);
            if (r == null) continue;
            fitted++;
            int idx = j * ni + i;
            fluxMap[idx] = r.Value.flux;
            snrMap[idx] = r.Value.snr;
            velMap[idx] = r.Value.velocity;
            if (r.Value.snr >= 5) good++;
        }

        if (fitted == 0)
        {
            entry = new CatalogEntry(objectId, z, band, exptimeMin, emissionLine, z, 0, 1, 45, 0);
            detail = new CatalogDetails(objectId, ra, dec, dims, 0, 0, 0, 0, 0, 0, 0, 0,
                "A = unusable", Lambda(wl, kLine), LambdaRange(wl));
            return true;
        }

        // Spatial moments: size, axis ratio, inclination.
        var m = SpatialMoments(fluxMap, ni, nj);
        double arcsecPerPix = Math.Abs(FitsHeaderReport.SafeDouble(fluxHdu.Header, "CDELT2", 0)) * 3600.0;
        double sizeArcsec = m.sizePix * arcsecPerPix;

        // Metrics.
        double snrPeak = RobustPercentile(snrMap, 0.90);   // robust to residual CRs
        double velSpan = RobustSpan(velMap, snrMap);
        double totalFlux = SumPositive(fluxMap);

        double snrScore = SnrScore(snrPeak);
        double incScore = IncScore(m.incDeg);
        double fluxScore = FluxScore(good);
        double kinematicScore = Math.Round(snrScore + incScore + fluxScore, 1);

        string classification = Classify(snrPeak, m.incDeg, good, velSpan, kinematicScore);

        entry = new CatalogEntry(
            objectId,
            Math.Round(z, 4),
            band,
            exptimeMin,
            emissionLine,
            Math.Round(z, 4),
            Math.Round(snrPeak, 1),
            Math.Round(m.axisRatio, 3),
            Math.Round(m.incDeg, 1),
            kinematicScore);

        detail = new CatalogDetails(
            objectId, ra, dec, dims, Math.Round(sizeArcsec, 2),
            good, fitted, totalFlux, Math.Round(velSpan, 0),
            Math.Round(snrScore, 1), Math.Round(incScore, 1), Math.Round(fluxScore, 1),
            classification, Lambda(wl, kLine), LambdaRange(wl));

        return true;
    }

    // ---------------------------------------------------------------------
    // Line identification (cross-correlation over the line catalogue)
    // ---------------------------------------------------------------------

    private static (string name, double z, double lambdaObs)? IdentifyRedshift(double[] wl, double[] spec)
    {
        if (wl.Length == 0 || spec.Length != wl.Length) return null;

        // Median-filter the collapsed spectrum to remove cosmic-ray spikes.
        spec = MedianFilter3(spec);

        double median = Median(spec);
        double mad = 1.4826 * Median(spec.Select(v => Math.Abs(v - median)).ToArray());
        double noiseFloor = mad > 0 ? mad : Math.Abs(median) * 1e-3 + 1e-20;
        double threshold = median + 6.0 * noiseFloor;
        double companionThreshold = median + 5.0 * noiseFloor;

        // Strongest emission peak (must clear the 6-sigma detection threshold).
        int kPeak = -1;
        double peak = double.NegativeInfinity;
        for (int i = 1; i < spec.Length - 1; i++)
            if (spec[i] > threshold && spec[i] >= spec[i - 1] && spec[i] >= spec[i + 1])
                if (spec[i] > peak) { peak = spec[i]; kPeak = i; }
        if (kPeak < 0) return null;

        double wStrong = wl[kPeak];
        double fluxPrimary = peak - median;
        if (fluxPrimary <= 0) return null;

        // Anchor the strongest peak on each physically-dominant line, and score
        // the identification by the flux-weighted detection of companion lines.
        // Restricting anchors to primary lines prevents the [SII]-doublet /
        // H-alpha degeneracy from producing spurious companion-line identities.
        const double zMax = 5.5;
        string bestName = "";
        double bestZ = double.NaN;
        double bestScore = double.NegativeInfinity;

        foreach (var (name, rest) in Lines)
        {
            if (!PrimaryLines.Contains(name)) continue;
            double z = wStrong / rest - 1.0;
            if (z < 0.02 || z > zMax) continue;

            double score = name == "H-alpha" ? 0.3 : 0.0;   // survey is H-alpha selected
            foreach (var (n2, rest2) in Lines)
            {
                if (n2 == name) continue;
                double wObs = rest2 * (1 + z);
                if (wObs < wl[0] || wObs > wl[^1]) continue;
                double f = spec[NearestIndex(wl, wObs)] - median;
                if (f > companionThreshold) score += f / fluxPrimary;
            }

            if (score > bestScore) { bestScore = score; bestZ = z; bestName = name; }
        }

        if (bestName == "") return null;
        return (bestName, bestZ, wStrong);
    }

    // ---------------------------------------------------------------------
    // Per-pixel line fit (robust peak + parabolic centroid; flux + SNR + vel)
    // ---------------------------------------------------------------------

    private static (double flux, double snr, double velocity)? FitLinePixel(
        double[,,] cube, double[,,] noise, double[] wl, int nk, int j, int i, int kLine, double globalNoise)
    {
        int wWide = 20;
        int kLo = Math.Max(0, kLine - wWide);
        int kHi = Math.Min(nk - 1, kLine + wWide);
        int nWide = kHi - kLo + 1;
        if (nWide < 7) return null;

        var win = new double[nWide];
        for (int k = 0; k < nWide; k++) win[k] = cube[kLo + k, j, i];

        // 3-channel median filter removes single-channel cosmic-ray/bad-pixel
        // spikes (which reach ±1e5 in these cubes) before locating the line.
        var clean = MedianFilter3(win);

        double cont = Median(clean);
        var d = new double[nWide];
        for (int k = 0; k < nWide; k++) d[k] = clean[k] - cont;

        int kp = 0;
        double dPeak = double.NegativeInfinity;
        for (int k = 0; k < nWide; k++) if (d[k] > dPeak) { dPeak = d[k]; kp = k; }
        if (dPeak <= 0) return null;

        double noiseLevel = EstimateNoise(noise, nk, j, i, kLo + kp, globalNoise);

        // Reject residual multi-channel cosmic rays (>>500 sigma) that survived
        // the median filter; a real H-alpha peak never exceeds this in one spaxel.
        if (globalNoise > 0 && dPeak > 500.0 * globalNoise) return null;

        double snr = noiseLevel > 0 ? dPeak / noiseLevel : double.NaN;

        // Sub-channel centroid (bounded to +/-1 channel).
        double offset = 0;
        if (kp > 0 && kp < nWide - 1)
        {
            double denom = d[kp - 1] - 2 * d[kp] + d[kp + 1];
            if (Math.Abs(denom) > 1e-30) offset = 0.5 * (d[kp - 1] - d[kp + 1]) / denom;
        }
        offset = Math.Clamp(offset, -1.0, 1.0);
        double dLambda = wl.Length > 1 ? wl[1] - wl[0] : 1;
        double mu = wl[kLo + kp] + offset * dLambda;

        // Flux = sum of positive-signal channels (line integral above continuum).
        double flux = 0;
        for (int k = 0; k < nWide; k++) if (d[k] > 0) flux += d[k];

        double lambdaSys = wl[kLine];
        double velocity = c_kms * (mu - lambdaSys) / lambdaSys;
        if (Math.Abs(velocity) > 2000) velocity = double.NaN;

        return (flux, snr, velocity);
    }

    private static double EstimateNoise(double[,,] noise, int nk, int j, int i, int kPeak, double globalNoise)
    {
        double nv = noise[Math.Min(kPeak, noise.GetLength(0) - 1), j, i];
        // Only trust per-pixel noise within a sane band around the global
        // median; otherwise a bad noise value (NaN, 0, or 1e14) would corrupt SNR.
        if (!double.IsNaN(nv) && nv > 0.1 * globalNoise && nv < 10.0 * globalNoise) return nv;
        return globalNoise;
    }

    /// <summary>
    /// Robust per-cube noise level: median of the positive noise-cube values,
    /// falling back to the median per-pixel MAD of the flux cube.
    /// </summary>
    private static double GlobalNoise(double[,,] noise, double[,,] cube, int nk, int nj, int ni)
    {
        var nv = new List<double>();
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double v = noise[nk / 2, j, i];
            if (!double.IsNaN(v) && v > 0) nv.Add(v);
        }
        if (nv.Count > 0) return Median(nv);

        // Fallback: median per-pixel MAD over a subsample (robust to lines).
        var madList = new List<double>();
        for (int j = 0; j < nj; j += Math.Max(1, nj / 10))
        for (int i = 0; i < ni; i += Math.Max(1, ni / 10))
        {
            var spec = new double[nk];
            for (int k = 0; k < nk; k++) spec[k] = cube[k, j, i];
            double med = Median(spec);
            var dev = new double[nk];
            for (int k = 0; k < nk; k++) dev[k] = Math.Abs(spec[k] - med);
            madList.Add(1.4826 * Median(dev));
        }
        return madList.Count > 0 ? Median(madList) : 0;
    }

    /// <summary>3-channel median filter (removes single-channel cosmic-ray spikes).</summary>
    private static double[] MedianFilter3(double[] x)
    {
        int n = x.Length;
        var y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double a = x[i];
            double b = i > 0 ? x[i - 1] : x[i];
            double c = i < n - 1 ? x[i + 1] : x[i];
            if (a > b) (a, b) = (b, a);
            if (b > c) (b, c) = (c, b);
            if (a > b) (a, b) = (b, a);
            y[i] = b;
        }
        return y;
    }

    // ---------------------------------------------------------------------
    // Spatial moments: size, axis ratio b/a, inclination (thickness corrected)
    // ---------------------------------------------------------------------

    private static (double sizePix, double axisRatio, double incDeg, double xc, double yc) SpatialMoments(
        double[] fluxMap, int ni, int nj)
    {
        double sum = 0, sx = 0, sy = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double f = fluxMap[j * ni + i];
            if (double.IsNaN(f) || f <= 0) continue;
            sum += f; sx += f * i; sy += f * j;
        }
        if (sum == 0) return (0, 1, 45, ni / 2.0, nj / 2.0);

        double xc = sx / sum, yc = sy / sum;
        double Sxx = 0, Syy = 0, Sxy = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double f = fluxMap[j * ni + i];
            if (double.IsNaN(f) || f <= 0) continue;
            double dx = i - xc, dy = j - yc;
            Sxx += f * dx * dx; Syy += f * dy * dy; Sxy += f * dx * dy;
        }
        double mx2 = Sxx / sum, my2 = Syy / sum, mxy = Sxy / sum;
        double tr = mx2 + my2;
        double det = Math.Sqrt((mx2 - my2) * (mx2 - my2) + 4 * mxy * mxy);
        double a2 = (tr + det) / 2, b2 = (tr - det) / 2;

        double sizePix = Math.Sqrt(Math.Max(tr / 2.0, 0));
        double ba = a2 > 0 ? Math.Sqrt(Math.Max(b2, 0) / a2) : 1;

        // Inclination from axis ratio with an intrinsic thickness q0 = 0.15.
        const double q0 = 0.15;
        double cosi2 = Math.Clamp((ba * ba - q0 * q0) / (1 - q0 * q0), 0.0, 1.0);
        double inc = Math.Acos(Math.Sqrt(cosi2)) * 180.0 / Math.PI;
        return (sizePix, ba, inc, xc, yc);
    }

    // ---------------------------------------------------------------------
    // Scoring and classification
    // ---------------------------------------------------------------------

    private static double SnrScore(double snrPeak)
    {
        if (snrPeak >= 20) return 40;
        if (snrPeak >= 10) return 30;
        if (snrPeak >= 5) return 20;
        if (snrPeak >= 3) return 10;
        return 0;
    }

    private static double IncScore(double incDeg)
    {
        if (incDeg < 20) return 0;
        if (incDeg < 45) return 10 + (incDeg - 20) * 0.4;     // 10..20
        if (incDeg <= 80) return 25 + (incDeg - 45) * 0.2;     // 25..32
        return 30;
    }

    private static double FluxScore(int goodPixels) =>
        Math.Min(25, 25.0 * goodPixels / 50.0);

    private static string Classify(double snrPeak, double incDeg, int good, double velSpan, double score)
    {
        if (double.IsNaN(snrPeak) || snrPeak < 3 || good == 0)
            return "A = unusable";
        if (snrPeak >= 12 && incDeg >= 30 && incDeg <= 75 && good >= 20 && velSpan >= 100 && score >= 65)
            return "D = high-priority rotation-curve target";
        if (snrPeak >= 8 && incDeg >= 25 && good >= 8 && velSpan >= 50)
            return "C = velocity field probable";
        return "B = science cube";
    }

    // ---------------------------------------------------------------------
    // Output helpers
    // ---------------------------------------------------------------------

    private static void WriteCatalogCsv(string path, CatalogEntry[] entries)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ObjectId,Redshift,Band,ExposureMinutes,EmissionLine,EstimatedZ,SNR,AxisRatio,Inclination,KinematicScore");
        foreach (var e in entries)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2},{3:F1},{4},{5:F4},{6:F1},{7:F3},{8:F1},{9:F1}",
                e.ObjectId, e.Redshift, e.Band, e.ExposureMinutes, e.EmissionLine,
                e.EstimatedZ, e.SNR, e.AxisRatio, e.InclinationDeg, e.KinematicScore));
        }
        File.WriteAllText(path, sb.ToString());
    }

    private static void WriteTop20Csv(string path, CatalogEntry[] entries, CatalogDetails[] details)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Rank,ObjectId,Redshift,Band,ExposureMinutes,EmissionLine,EstimatedZ,SNR,AxisRatio,Inclination,KinematicScore,Classification,SizeArcsec,GoodPixels,VelSpanKms,RAdeg,DECdeg");
        for (int i = 0; i < entries.Length; i++)
        {
            var e = entries[i];
            var d = details[i];
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1},{2:F4},{3},{4:F1},{5},{6:F4},{7:F1},{8:F3},{9:F1},{10:F1},{11},{12:F2},{13},{14:F0},{15:F4},{16:F4}",
                i + 1, e.ObjectId, e.Redshift, e.Band, e.ExposureMinutes, e.EmissionLine,
                e.EstimatedZ, e.SNR, e.AxisRatio, e.InclinationDeg, e.KinematicScore,
                d.Classification, d.SizeArcsec, d.GoodPixels, d.VelocitySpanKms, d.RAdeg, d.DECdeg));
        }
        File.WriteAllText(path, sb.ToString());
    }

    private static string BuildTop20Table(CatalogEntry[] entries, CatalogDetails[] details)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,-14} {2,6} {3,4} {4,-12} {5,7} {6,6} {7,6} {8,6} {9,5} {10}",
            "#", "Object", "z", "Band", "Line", "SNR", "b/a", "i(deg)", "Score", "Good", "Class"));
        sb.AppendLine("  " + new string('-', 96));
        for (int i = 0; i < entries.Length; i++)
        {
            var e = entries[i];
            var d = details[i];
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,-14} {2,6:F3} {3,4} {4,-12} {5,7:F1} {6,6:F2} {7,6:F0} {8,6:F1} {9,5} {10}",
                i + 1, e.ObjectId, e.Redshift, e.Band, e.EmissionLine,
                e.SNR, e.AxisRatio, e.InclinationDeg, e.KinematicScore, d.GoodPixels,
                d.Classification));
        }
        return sb.ToString();
    }

    private static string BuildSummary(CatalogEntry[] entries, CatalogDetails[] details)
    {
        var sb = new System.Text.StringBuilder();
        int total = entries.Length;
        int a = details.Count(d => d.Classification.StartsWith("A"));
        int b = details.Count(d => d.Classification.StartsWith("B"));
        int c = details.Count(d => d.Classification.StartsWith("C"));
        int dcount = details.Count(d => d.Classification.StartsWith("D"));

        sb.AppendLine($"Cubes scanned: {total}");
        sb.AppendLine($"  A = unusable                          : {a}");
        sb.AppendLine($"  B = science cube                      : {b}");
        sb.AppendLine($"  C = velocity field probable           : {c}");
        sb.AppendLine($"  D = high-priority rotation-curve      : {dcount}");
        sb.AppendLine();

        foreach (var band in new[] { "H", "K", "YJ" })
        {
            var inBand = entries.Where(e => e.Band == band).ToArray();
            if (inBand.Length == 0) continue;
            double zMin = inBand.Where(e => !double.IsNaN(e.Redshift)).Min(e => e.Redshift);
            double zMax = inBand.Where(e => !double.IsNaN(e.Redshift)).Max(e => e.Redshift);
            sb.AppendLine($"  Band {band}: {inBand.Length} cubes, z = {zMin:F2} .. {zMax:F2}");
        }

        var withLine = entries.Where(e => !double.IsNaN(e.Redshift)).ToArray();
        if (withLine.Length > 0)
        {
            var byLine = withLine.GroupBy(e => e.EmissionLine)
                .Select(g => (line: g.Key, n: g.Count()))
                .OrderByDescending(x => x.n);
            sb.AppendLine();
            sb.AppendLine("  Identified emission lines:");
            foreach (var g in byLine)
                sb.AppendLine($"    {g.line,-14} {g.n}");
        }
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Small helpers
    // ---------------------------------------------------------------------

    private static string LambdaRange(double[] wl) =>
        wl.Length > 0
            ? $"{wl[0]:F0}..{wl[^1]:F0} A"
            : "";

    private static string Lambda(double[] wl, int k) =>
        k >= 0 && k < wl.Length ? $"{wl[k]:F1} A" : "";

    private static double LookupRest(string name)
    {
        foreach (var (n, w) in Lines)
            if (n == name) return w;
        return 6562.80;
    }

    private static string InferBandFromFileName(string path)
    {
        string name = Path.GetFileNameWithoutExtension(path);
        if (name.EndsWith("_H", StringComparison.OrdinalIgnoreCase)) return "H";
        if (name.EndsWith("_K", StringComparison.OrdinalIgnoreCase)) return "K";
        if (name.EndsWith("_YJ", StringComparison.OrdinalIgnoreCase)) return "YJ";
        return "";
    }

    private static ImageHDU? FindHduByName(BasicHDU[] hdus, string namePart)
    {
        foreach (var h in hdus)
            if (h is ImageHDU ih)
            {
                string en = FitsHeaderReport.SafeString(ih.Header, "EXTNAME");
                if (en.Contains(namePart, StringComparison.OrdinalIgnoreCase)) return ih;
            }
        return null;
    }

    private static double[,,] LoadCube(Array jagged, int nk, int nj, int ni)
    {
        var cube = new double[nk, nj, ni];
        for (int k = 0; k < nk; k++)
        {
            var plane = (Array)jagged.GetValue(k);
            for (int j = 0; j < nj; j++)
            {
                var row = (Array)plane.GetValue(j);
                for (int i = 0; i < ni; i++)
                    cube[k, j, i] = Convert.ToDouble(row.GetValue(i), CultureInfo.InvariantCulture);
            }
        }
        return cube;
    }

    private static int NearestIndex(double[] wl, double target)
    {
        int lo = 0, hi = wl.Length - 1;
        while (lo < hi) { int mid = (lo + hi) / 2; if (wl[mid] < target) lo = mid + 1; else hi = mid; }
        if (lo > 0 && Math.Abs(wl[lo - 1] - target) < Math.Abs(wl[lo] - target)) return lo - 1;
        return lo;
    }

    private static double Median(IEnumerable<double> vals)
    {
        var a = vals.Where(v => !double.IsNaN(v) && !double.IsInfinity(v)).OrderBy(v => v).ToArray();
        if (a.Length == 0) return 0;
        int n = a.Length;
        return n % 2 == 1 ? a[n / 2] : 0.5 * (a[n / 2 - 1] + a[n / 2]);
    }

    /// <summary>Robust high percentile (ignores NaN), used as the SNR metric.</summary>
    private static double RobustPercentile(double[] map, double q)
    {
        var vals = map.Where(v => !double.IsNaN(v) && v > 0).OrderBy(v => v).ToArray();
        if (vals.Length == 0) return 0;
        int idx = (int)Math.Min(vals.Length - 1, Math.Floor(q * vals.Length));
        return vals[idx];
    }

    private static double SumPositive(double[] map)
    {
        double s = 0;
        foreach (var v in map) if (!double.IsNaN(v) && v > 0) s += v;
        return s;
    }

    private static double RobustSpan(double[] velMap, double[] snrMap)
    {
        var vs = new List<double>();
        for (int i = 0; i < velMap.Length; i++)
            if (!double.IsNaN(velMap[i]) && !double.IsNaN(snrMap[i]) && snrMap[i] >= 5)
                vs.Add(velMap[i]);
        if (vs.Count < 2) return 0;
        vs.Sort();
        double p05 = vs[(int)(0.05 * vs.Count)];
        double p95 = vs[(int)(0.95 * vs.Count)];
        return p95 - p05;
    }
}
