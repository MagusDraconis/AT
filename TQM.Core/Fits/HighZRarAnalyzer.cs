using System.Globalization;
using nom.tam.fits;

namespace TQM.Core.FitsAnalysis;

/// <summary>
/// Builds the first high-z RAR pilot sample: for each Class-D KMOS3D cube it
/// derives the H-alpha velocity field, fits a rotating-disk model, applies
/// acceptance cuts, ranks by kinematic reliability, and estimates g_obs(r).
/// </summary>
public static class HighZRarAnalyzer
{
    const double c_kms = 299792.458;
    const double H0 = 67.4, OmM = 0.315, OmL = 0.685;

    public static HighZRarReport Run(string fitsDir, string catalogCsv, string outDir)
    {
        Directory.CreateDirectory(outDir);

        var rows = ReadCatalog(catalogCsv);
        var all = new List<GalaxyKinematics>();

        foreach (var (objectId, z, band, line) in rows)
        {
            string file = Path.Combine(fitsDir, $"{objectId}_{band}.fits");
            if (!File.Exists(file)) continue;
            double lineRest = LineRest(line);
            if (lineRest <= 0) continue;

            var k = AnalyzeGalaxy(file, objectId, z, line, lineRest);
            if (k != null) all.Add(k);
        }

        // Rank non-rejected galaxies by kinematic reliability (FitQuality).
        var accepted = all
            .Where(k => !k.Classification.StartsWith("A"))
            .OrderByDescending(k => k.FitQuality)
            .ToArray();

        string csvPath = Path.Combine(outDir, "HighZ_RotationCatalog.csv");
        WriteCsv(csvPath, accepted);

        return new HighZRarReport(
            BuildA(all.ToArray()),
            BuildB(accepted.Take(10).ToArray()),
            BuildC(accepted),
            BuildD(accepted),
            accepted, all.ToArray(), csvPath);
    }

    // ---------------------------------------------------------------------
    // Per-galaxy kinematics
    // ---------------------------------------------------------------------

    /// <summary>Full per-galaxy kinematics (velocity field, disk fit, rotation curve,
    /// maps). Public so the QG-071 RAR extraction audit can reuse it.</summary>
    public static GalaxyFullKinematics? AnalyzeFull(string path, string objectId, double z, string line, double lineRest)
    {
        var fits = new Fits(path);
        try
        {
            return AnalyzeHdusFull(fits.Read(), objectId, z, line, lineRest);
        }
        catch
        {
            return null;
        }
        finally
        {
            fits.Close();
        }
    }

    private static GalaxyKinematics? AnalyzeGalaxy(string path, string objectId, double z, string line, double lineRest)
    {
        var f = AnalyzeFull(path, objectId, z, line, lineRest);
        if (f == null) return null;
        double fitQuality = FitQuality(f.Vmax_kms, f.Rms_kms, f.SNR, f.GoodPixels);
        string classification = Classify(f.InclinationDeg, f.VelocitySpan_kms, f.SNR, f.Vmax_kms, fitQuality);
        var (gobs, rlast) = Gobslast(f.RotationCurve);
        return new GalaxyKinematics(
            f.ObjectId, f.Redshift, f.EmissionLine,
            f.InclinationDeg, f.Vmax_kms, f.TurnoverRadius_kpc,
            f.VelocitySpan_kms, fitQuality, f.SNR, f.GoodPixels,
            f.Rms_kms, f.Vsys_kms, gobs, rlast, classification);
    }

    private static GalaxyFullKinematics? AnalyzeHdusFull(BasicHDU[] hdus, string objectId, double z, string line, double lineRest)
    {
        var fc = FitsCubeInspector.FindScienceCube(hdus);
        if (fc is not { } c) return null;
        ImageHDU fluxHdu = c.hdu;
        Array jagged = (Array)c.data;

        int nk = jagged.Length;
        int nj = ((Array)jagged.GetValue(0)).Length;
        int ni = ((Array)((Array)jagged.GetValue(0)).GetValue(0)).Length;

        double[] wl = FitsCubeInspector.ComputeWavelength(fluxHdu, nk) ?? Array.Empty<double>();
        if (wl.Length == 0) return null;

        ImageHDU? noiseHdu = FindHduByName(hdus, "noise");
        double[,,] cube = LoadCube(jagged, nk, nj, ni);
        double[,,] noise = new double[nk, nj, ni];
        if (noiseHdu != null && noiseHdu.Data is ImageData nd && nd.DataArray is Array njag)
        {
            try { noise = LoadCube(njag, nk, nj, ni); } catch { }
        }
        double globalNoise = GlobalNoise(noise, cube, nk, nj, ni);

        double lambdaSys = lineRest * (1 + z);
        int kLine = NearestIndex(wl, lambdaSys);
        double dLambda = wl.Length > 1 ? wl[1] - wl[0] : 1;

        // Per-pixel line fits -> velocity / dispersion / flux / SNR maps.
        int fitted = 0, good = 0;
        var velMap = new double[nj * ni];
        var dispMap = new double[nj * ni];
        var fluxMap = new double[nj * ni];
        var snrMap = new double[nj * ni];
        Array.Fill(velMap, double.NaN);
        Array.Fill(dispMap, double.NaN);
        Array.Fill(fluxMap, double.NaN);
        Array.Fill(snrMap, double.NaN);

        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            var r = FitPixel(cube, noise, wl, nk, j, i, kLine, lambdaSys, globalNoise);
            if (r == null) continue;
            fitted++;
            int idx = j * ni + i;
            velMap[idx] = r.Value.velocity;
            dispMap[idx] = r.Value.dispersion;
            fluxMap[idx] = r.Value.flux;
            snrMap[idx] = r.Value.snr;
            if (r.Value.snr >= 5) good++;
        }

        if (fitted < 8)
            return new GalaxyFullKinematics(objectId, z, line, 0, 0, 0, 45, 0, double.NaN,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                Array.Empty<RotationCurvePoint>(), fluxMap, velMap, snrMap, ni, nj);

        // Flux-weighted centre; PA from velocity gradient; disk fit.
        var (xc, yc) = FluxCentroid(fluxMap, ni, nj);
        double paInit = EstimatePA(velMap, ni, nj, xc, yc);
        var disk = GridDiskFit(velMap, snrMap, ni, nj, xc, yc, paInit);

        // Velocity span (robust p95-p05 over SNR>=5 pixels).
        double velSpan = RobustSpan(velMap, snrMap);
        double snr90 = RobustPercentile(snrMap, 0.90);
        double rms = Math.Sqrt(Math.Max(disk.chi2, 0));

        // Spatial scale.
        double arcsecPerPix = Math.Abs(FitsHeaderReport.SafeDouble(fluxHdu.Header, "CDELT2", 0)) * 3600.0;
        double kpcPerPix = KpcPerArcsec(z) * arcsecPerPix;
        double rturnKpc = disk.rturn_pix * kpcPerPix;
        double deltaLambdaUm = Math.Abs(FitsHeaderReport.SafeDouble(fluxHdu.Header, "CDELT3", 0));

        // Rotation curve (deprojected, with MAD errors) for g_obs.
        var rotPix = ExtractRotationCurve(velMap, snrMap, ni, nj, xc, yc, disk);
        var rot = rotPix.Select(p => new RotationCurvePoint(
            Math.Round(p.r_pix * kpcPerPix, 3),
            Math.Round(p.vrot_kms, 1),
            Math.Round(p.err_kms, 1),
            p.npix)).ToArray();

        double totalFlux = SumPositive(fluxMap);

        return new GalaxyFullKinematics(
            objectId, z, line,
            Math.Round(disk.vsys_kms, 1),
            Math.Round(disk.vmax_kms, 1),
            Math.Round(rturnKpc, 2),
            Math.Round(disk.inclination_deg, 1),
            Math.Round(disk.pa_deg, 1),
            disk.chi2,
            Math.Round(rms, 1),
            Math.Round(velSpan, 0),
            Math.Round(snr90, 1),
            good, fitted,
            totalFlux,
            kpcPerPix, arcsecPerPix, deltaLambdaUm,
            rot, fluxMap, velMap, snrMap, ni, nj);
    }

    // ---------------------------------------------------------------------
    // Per-pixel fit (robust peak + parabolic centroid + dispersion)
    // ---------------------------------------------------------------------

    private static (double flux, double velocity, double dispersion, double snr)? FitPixel(
        double[,,] cube, double[,,] noise, double[] wl, int nk, int j, int i,
        int kLine, double lambdaSys, double globalNoise)
    {
        int wWide = 20;
        int kLo = Math.Max(0, kLine - wWide);
        int kHi = Math.Min(nk - 1, kLine + wWide);
        int nWide = kHi - kLo + 1;
        if (nWide < 7) return null;

        var win = new double[nWide];
        for (int k = 0; k < nWide; k++) win[k] = cube[kLo + k, j, i];
        var clean = MedianFilter3(win);

        double cont = Median(clean);
        var d = new double[nWide];
        for (int k = 0; k < nWide; k++) d[k] = clean[k] - cont;

        int kp = 0;
        double dPeak = double.NegativeInfinity;
        for (int k = 0; k < nWide; k++) if (d[k] > dPeak) { dPeak = d[k]; kp = k; }
        if (dPeak <= 0) return null;

        double noiseLevel = EstimateNoise(noise, nk, j, i, kLo + kp, globalNoise);
        if (globalNoise > 0 && dPeak > 500.0 * globalNoise) return null;   // residual CR
        double snr = noiseLevel > 0 ? dPeak / noiseLevel : double.NaN;

        double offset = 0;
        if (kp > 0 && kp < nWide - 1)
        {
            double denom = d[kp - 1] - 2 * d[kp] + d[kp + 1];
            if (Math.Abs(denom) > 1e-30) offset = 0.5 * (d[kp - 1] - d[kp + 1]) / denom;
        }
        offset = Math.Clamp(offset, -1.0, 1.0);
        double dLambda = wl.Length > 1 ? wl[1] - wl[0] : 1;
        double mu = wl[kLo + kp] + offset * dLambda;

        double flux = 0;
        for (int k = 0; k < nWide; k++) if (d[k] > 0) flux += d[k];

        // Dispersion (second moment around the centroid, positive channels).
        double s2num = 0, s2den = 0;
        for (int k = 0; k < nWide; k++)
        {
            if (d[k] <= 0) continue;
            double w = wl[kLo + k];
            s2num += d[k] * (w - mu) * (w - mu);
            s2den += d[k];
        }
        double sigma = s2den > 0 ? Math.Sqrt(s2num / s2den) : dLambda;

        double velocity = c_kms * (mu - lambdaSys) / lambdaSys;
        double dispersion = c_kms * sigma / lambdaSys;
        if (Math.Abs(velocity) > 2000) velocity = double.NaN;

        return (flux, velocity, dispersion, snr);
    }

    // ---------------------------------------------------------------------
    // Disk model
    // ---------------------------------------------------------------------

    private sealed record DiskFit(double vsys_kms, double inclination_deg, double pa_deg,
        double vmax_kms, double rturn_pix, double chi2);

    private static DiskFit GridDiskFit(double[] velMap, double[] snrMap, int ni, int nj,
        double xc, double yc, double paInit)
    {
        double[] paGrid = { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165 };
        double[] incGrid = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 };
        double[] rtGrid = { 1, 2, 3, 4, 5, 6, 8, 10, 12 };
        double bestChi2 = double.PositiveInfinity;
        var best = new DiskFit(0, 45, paInit, 0, 4, double.NaN);

        foreach (double pa in paGrid)
        foreach (double inc in incGrid)
        foreach (double rt in rtGrid)
        {
            double sinI = Math.Sin(inc * Math.PI / 180.0);
            double paR = pa * Math.PI / 180.0;
            double sA = 0, sB = 0, sC = 0, sD = 0, sE = 0;
            int cnt = 0;
            for (int j = 0; j < nj; j++)
            for (int i = 0; i < ni; i++)
            {
                double v = velMap[j * ni + i];
                double snr = double.IsNaN(snrMap[j * ni + i]) ? 0 : snrMap[j * ni + i];
                if (double.IsNaN(v) || snr < 3) continue;
                double dx = i - xc, dy = j - yc;
                double R = Math.Sqrt(dx * dx + dy * dy);
                if (R < 0.3) continue;
                double s = dx * Math.Sin(paR) + dy * Math.Cos(paR);
                double f = (2.0 / Math.PI) * Math.Atan(R / rt) * sinI * (s / R);
                sA += 1; sB += f; sC += f * f; sD += v; sE += f * v; cnt++;
            }
            if (cnt < 8) continue;
            double det = sA * sC - sB * sB;
            if (Math.Abs(det) < 1e-12) continue;
            double Vsys = (sD * sC - sE * sB) / det;
            double Vmax = (sA * sE - sB * sD) / det;
            if (Vmax < 0) continue;

            double chi2 = 0;
            int c2 = 0;
            for (int j = 0; j < nj; j++)
            for (int i = 0; i < ni; i++)
            {
                double v = velMap[j * ni + i];
                double snr = double.IsNaN(snrMap[j * ni + i]) ? 0 : snrMap[j * ni + i];
                if (double.IsNaN(v) || snr < 3) continue;
                double dx = i - xc, dy = j - yc;
                double R = Math.Sqrt(dx * dx + dy * dy);
                double s = dx * Math.Sin(paR) + dy * Math.Cos(paR);
                double model = Vsys + (2.0 / Math.PI) * Vmax * Math.Atan(R / rt) * sinI * (s / R);
                chi2 += (v - model) * (v - model); c2++;
            }
            chi2 /= Math.Max(1, c2);
            if (chi2 < bestChi2)
            {
                bestChi2 = chi2;
                best = new DiskFit(Vsys, inc, pa, Vmax, rt, chi2);
            }
        }
        return best;
    }

    private static (double gobs_m_s2, double rlast_kpc) Gobslast(RotationCurvePoint[] rot)
    {
        // Outermost reliable bin (npix >= 2): g_obs = V_rot^2 / r.
        var last = rot.Where(p => p.Npix >= 2).OrderByDescending(p => p.Radius_kpc).FirstOrDefault();
        if (last.Npix < 2) return (double.NaN, double.NaN);
        double rKpc = last.Radius_kpc;
        double v = last.Vrot_kms;
        double gobs = 3.241e-14 * v * v / rKpc;   // km/s, kpc -> m/s^2
        return (gobs, rKpc);
    }

    /// <summary>Coherent-rotation fit-quality score (0..1).</summary>
    private static double FitQuality(double vmax, double rms, double snr, int good)
    {
        return Math.Clamp(
            0.35 * Math.Min(1, vmax / 250.0)
          + 0.20 * Math.Min(1, snr / 50.0)
          + 0.25 * Math.Min(1, good / 100.0)
          + 0.20 * Math.Min(1, vmax / Math.Max(rms, 20.0)), 0, 1);
    }

    // ---------------------------------------------------------------------
    // Acceptance / classification
    // ---------------------------------------------------------------------

    private static string Classify(double incDeg, double velSpan, double snr, double vmax, double fitQuality)
    {
        if (incDeg < 35 || velSpan < 150 || snr < 10)
            return "A = reject";
        if (fitQuality >= 0.7 && vmax >= 150 && incDeg <= 80)
            return "D = immediate RAR candidate";
        if (fitQuality >= 0.5)
            return "C = high-quality";
        return "B = usable";
    }

    private static (double r_pix, double vrot_kms, double err_kms, int npix)[] ExtractRotationCurve(
        double[] velMap, double[] snrMap, int ni, int nj, double xc, double yc, DiskFit disk)
    {
        double sinI = Math.Sin(disk.inclination_deg * Math.PI / 180.0);
        double paR = disk.pa_deg * Math.PI / 180.0;
        var bins = new Dictionary<int, List<double>>();
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double v = velMap[j * ni + i];
            double snr = double.IsNaN(snrMap[j * ni + i]) ? 0 : snrMap[j * ni + i];
            if (double.IsNaN(v) || snr < 3) continue;
            double dx = i - xc, dy = j - yc;
            double R = Math.Sqrt(dx * dx + dy * dy);
            if (R < 0.5) continue;
            double s = dx * Math.Sin(paR) + dy * Math.Cos(paR);
            double cosTheta = s / R;
            if (Math.Abs(cosTheta) < 0.3) continue;
            double vrot = Math.Abs((v - disk.vsys_kms) / (sinI * cosTheta));
            int bin = (int)Math.Round(R);
            if (!bins.ContainsKey(bin)) bins[bin] = new List<double>();
            bins[bin].Add(vrot);
        }
        var result = new List<(double, double, double, int)>();
        foreach (var kv in bins.OrderBy(k => k.Key))
        {
            var vals = kv.Value.OrderBy(x => x).ToArray();
            double med = Median(vals);
            double mad = 1.4826 * Median(vals.Select(x => Math.Abs(x - med)).ToArray());
            result.Add((kv.Key, med, mad, vals.Length));
        }
        return result.ToArray();
    }

    // ---------------------------------------------------------------------
    // Catalog input
    // ---------------------------------------------------------------------

    private static List<(string objectId, double z, string band, string line)> ReadCatalog(string csv)
    {
        var rows = new List<(string, double, string, string)>();
        var lines = File.ReadAllLines(csv);
        if (lines.Length < 2) return rows;

        var header = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        int iObj = Array.FindIndex(header, h => h == "ObjectId");
        int iZ = Array.FindIndex(header, h => h == "Redshift");
        int iBand = Array.FindIndex(header, h => h == "Band");
        int iLine = Array.FindIndex(header, h => h == "EmissionLine");
        if (iObj < 0 || iZ < 0 || iBand < 0 || iLine < 0) return rows;

        foreach (var raw in lines.Skip(1))
        {
            if (string.IsNullOrWhiteSpace(raw)) continue;
            var parts = raw.Split(',');
            if (parts.Length <= Math.Max(iObj, Math.Max(iZ, Math.Max(iBand, iLine)))) continue;
            if (!double.TryParse(parts[iZ], NumberStyles.Float, CultureInfo.InvariantCulture, out double z)) continue;
            rows.Add((parts[iObj].Trim(), z, parts[iBand].Trim(), parts[iLine].Trim()));
        }
        return rows;
    }

    private static double LineRest(string line)
    {
        return line.Trim().ToLowerInvariant() switch
        {
            "h-alpha" => 6562.80,
            "[oiii] 5007" => 5006.84,
            "h-beta" => 4861.33,
            "[oii] 3727" => 3726.03,
            _ => 0,
        };
    }

    // ---------------------------------------------------------------------
    // Output
    // ---------------------------------------------------------------------

    private static void WriteCsv(string path, GalaxyKinematics[] accepted)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ObjectId,Redshift,Inclination,Vmax,TurnoverRadius,VelocitySpan,FitQuality");
        foreach (var k in accepted)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2:F1},{3:F1},{4:F2},{5:F0},{6:F3}",
                k.ObjectId, k.Redshift, k.InclinationDeg, k.Vmax_kms,
                k.TurnoverRadius_kpc, k.VelocitySpan_kms, k.FitQuality));
        }
        File.WriteAllText(path, sb.ToString());
    }

    private static string BuildA(GalaxyKinematics[] all)
    {
        var sb = new System.Text.StringBuilder();
        int total = all.Length;
        int rejected = all.Count(k => k.Classification.StartsWith("A"));
        int usable = all.Count(k => k.Classification.StartsWith("B"));
        int high = all.Count(k => k.Classification.StartsWith("C"));
        int imm = all.Count(k => k.Classification.StartsWith("D"));
        sb.AppendLine($"Galaxies processed: {total}");
        sb.AppendLine($"  A = reject (inc<35, span<150 km/s, or SNR<10): {rejected}");
        sb.AppendLine($"  B = usable                         : {usable}");
        sb.AppendLine($"  C = high-quality                   : {high}");
        sb.AppendLine($"  D = immediate RAR candidate        : {imm}");
        return sb.ToString();
    }

    private static string BuildB(GalaxyKinematics[] best)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,-12} {2,6} {3,6} {4,7} {5,6} {6,7} {7,5} {8,8}",
            "#", "Object", "z", "i(deg)", "Vmax", "Rturn", "span", "Qual", "gobs"));
        sb.AppendLine("  " + new string('-', 80));
        for (int i = 0; i < best.Length; i++)
        {
            var k = best[i];
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,-12} {2,6:F2} {3,6:F0} {4,7:F0} {5,6:F1} {6,7:F0} {7,5:F2} {8,8:E1}",
                i + 1, k.ObjectId, k.Redshift, k.InclinationDeg, k.Vmax_kms,
                k.TurnoverRadius_kpc, k.VelocitySpan_kms, k.FitQuality, k.Gobslast_m_s2));
        }
        return sb.ToString();
    }

    private static string BuildC(GalaxyKinematics[] accepted)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Kinematic quality of the accepted sample:");
        sb.AppendLine();
        foreach (var k in accepted)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} z={1:F3} i={2:F0} Vmax={3:F0} Rturn={4:F2} kpc span={5:F0} km/s " +
                "SNR={6:F0} Ngood={7} rms={8:F0} km/s -> {9}",
                k.ObjectId, k.Redshift, k.InclinationDeg, k.Vmax_kms, k.TurnoverRadius_kpc,
                k.VelocitySpan_kms, k.SNR, k.GoodPixels, k.Rms_kms, k.Classification));
        }
        return sb.ToString();
    }

    private static string BuildD(GalaxyKinematics[] accepted)
    {
        var sb = new System.Text.StringBuilder();
        int imm = accepted.Count(k => k.Classification.StartsWith("D"));
        var withGobs = accepted.Where(k => !double.IsNaN(k.Gobslast_m_s2)).ToArray();
        sb.AppendLine("Readiness for RAR analysis:");
        sb.AppendLine();
        sb.AppendLine($"  Accepted galaxies: {accepted.Length}");
        sb.AppendLine($"  Immediate RAR candidates (D): {imm}");
        sb.AppendLine($"  Galaxies with a g_obs(r) estimate: {withGobs.Length}");
        sb.AppendLine();
        sb.AppendLine("  g_obs = V_rot^2 / r at the outermost reliable radius.");
        sb.AppendLine("  TQM prediction at z: g†(z) = c·H(z)/2π (rising with z);");
        sb.AppendLine("  MOND: a₀ = constant. A rising g_obs(Rlast) trend with z would");
        sb.AppendLine("  favor TQM; a constant trend favors MOND.");
        sb.AppendLine();
        foreach (var k in withGobs.OrderBy(k => k.Redshift))
        {
            double ratio = Math.Sqrt(OmM * Math.Pow(1 + k.Redshift, 3) + OmL);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-12} z={1:F3}  g_obs({2:F1} kpc) = {3:E2} m/s^2   (g†(z)/g†(0) = {4:F2})",
                k.ObjectId, k.Redshift, k.Rlast_kpc, k.Gobslast_m_s2, ratio));
        }
        sb.AppendLine();
        sb.AppendLine("  CAVEATS (pilot): the disk fit uses a coarse (PA, i, r_t) grid and a");
        sb.AppendLine("  simple arctan model with no turbulent-dispersion term. Vmax is the");
        sb.AppendLine("  COHERENT rotation amplitude (turbulence and centroid noise are left in");
        sb.AppendLine("  the residual), and g_obs at the last bin carries deprojection");
        sb.AppendLine("  uncertainty. Treat Vmax / g_obs as first-cut estimates for follow-up.");
        return sb.ToString();
    }

    // ---------------------------------------------------------------------
    // Helpers (shared algorithms)
    // ---------------------------------------------------------------------

    private static double KpcPerArcsec(double z)
    {
        double Dc = 0;
        int n = 4000;
        double dz = z / n;
        for (int k = 0; k < n; k++)
        {
            double zz = (k + 0.5) * dz;
            double E = Math.Sqrt(OmM * Math.Pow(1 + zz, 3) + OmL);
            Dc += 299792.458 / H0 / E * dz;
        }
        double DaMpc = Dc / (1 + z);
        return DaMpc * 4.848e-3;   // kpc per arcsec
    }

    private static double EstimatePA(double[] velMap, int ni, int nj, double xc, double yc)
    {
        double sxx = 0, syy = 0, sxy = 0, sxv = 0, syv = 0; int cnt = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double v = velMap[j * ni + i];
            if (double.IsNaN(v)) continue;
            double dx = i - xc, dy = j - yc;
            sxx += dx * dx; syy += dy * dy; sxy += dx * dy; sxv += dx * v; syv += dy * v; cnt++;
        }
        if (cnt < 3) return 0;
        double det = sxx * syy - sxy * sxy;
        if (Math.Abs(det) < 1e-12) return 0;
        double b = (sxv * syy - syv * sxy) / det;
        double c = (syv * sxx - sxv * sxy) / det;
        double pa = Math.Atan2(b, c) * 180.0 / Math.PI;
        if (pa < 0) pa += 180.0;
        return pa;
    }

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

    private static double EstimateNoise(double[,,] noise, int nk, int j, int i, int kPeak, double globalNoise)
    {
        double nv = noise[Math.Min(kPeak, noise.GetLength(0) - 1), j, i];
        if (!double.IsNaN(nv) && nv > 0.1 * globalNoise && nv < 10.0 * globalNoise) return nv;
        return globalNoise;
    }

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

    private static double RobustPercentile(double[] map, double q)
    {
        var vals = map.Where(v => !double.IsNaN(v) && v > 0).OrderBy(v => v).ToArray();
        if (vals.Length == 0) return 0;
        int idx = (int)Math.Min(vals.Length - 1, Math.Floor(q * vals.Length));
        return vals[idx];
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

    private static double SumPositive(double[] map)
    {
        double s = 0;
        foreach (var v in map) if (!double.IsNaN(v) && v > 0) s += v;
        return s;
    }
}
