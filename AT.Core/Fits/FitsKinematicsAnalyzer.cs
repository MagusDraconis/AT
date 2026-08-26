using System.Globalization;
using nom.tam.fits;

namespace AT.Core.FitsAnalysis;

/// <summary>
/// Extracts a galaxy velocity field from a KMOS3D IFU cube: per-pixel H-alpha
/// Gaussian fits, velocity/dispersion/flux maps, a rotating-disk model, and a
/// rotation curve, in service of the QG-070 RAR-evolution test.
/// </summary>
public static class FitsKinematicsAnalyzer
{
    const double c_kms = 299792.458;
    const double HA_REST = 6562.80;   // Angstrom
    const double Z = 1.800;           // redshift (from H-alpha, QG-068 FITS analysis)

    public static KinematicsReport Run(string path, string outDir)
    {
        Directory.CreateDirectory(outDir);
        var fits = new Fits(path);
        try
        {
            BasicHDU[] hdus = fits.Read();
            return RunOnHdus(hdus, outDir, path);
        }
        finally
        {
            fits.Close();
        }
    }

    private static KinematicsReport RunOnHdus(BasicHDU[] hdus, string outDir, string path)
    {
        // Locate flux and noise cubes.
        var flux = FitsCubeInspector.FindScienceCube(hdus);
        if (flux is not { } fc)
            throw new InvalidDataException("No science (flux) cube found in " + path);

        ImageHDU noiseHdu = FindHduByName(hdus, "noise");
        ImageHDU fluxHdu = fc.hdu;

        // Geometry.
        int nk = ((Array)fc.data).Length;                                   // wavelength
        int nj = ((Array)((Array)fc.data).GetValue(0)).Length;              // DEC
        int ni = ((Array)((Array)((Array)fc.data).GetValue(0)).GetValue(0)).Length; // RA

        double[] wl = FitsCubeInspector.ComputeWavelength(fluxHdu, nk)
            ?? throw new InvalidDataException("No wavelength solution in flux cube.");

        double[,,] cube = LoadCube(fc.data, nk, nj, ni);
        double[,,] noise = new double[nk, nj, ni];
        if (noiseHdu != null && noiseHdu.Data is ImageData nd && nd.DataArray is Array njag)
        {
            try { noise = LoadCube(njag, nk, nj, ni); } catch { /* keep zero noise -> fallback RMS */ }
        }

        // Header geometry for RA/DEC per pixel.
        Header h = fluxHdu.Header;
        double crval1 = FitsHeaderReport.SafeDouble(h, "CRVAL1", 0);
        double crval2 = FitsHeaderReport.SafeDouble(h, "CRVAL2", 0);
        double cdelt1 = FitsHeaderReport.SafeDouble(h, "CDELT1", 0);
        double cdelt2 = FitsHeaderReport.SafeDouble(h, "CDELT2", 0);
        double crpix1 = FitsHeaderReport.SafeDouble(h, "CRPIX1", 1);
        double crpix2 = FitsHeaderReport.SafeDouble(h, "CRPIX2", 1);

        double lambdaSys = HA_REST * (1 + Z);   // 18375.84 A
        int kSys = NearestIndex(wl, lambdaSys);

        // Per-pixel Gaussian fits.
        var fluxMap = new double[nj * ni];
        var velMap = new double[nj * ni];
        var dispMap = new double[nj * ni];
        var snrMap = new double[nj * ni];
        Array.Fill(fluxMap, double.NaN);
        Array.Fill(velMap, double.NaN);
        Array.Fill(dispMap, double.NaN);
        Array.Fill(snrMap, double.NaN);

        int fitted = 0, good = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            var r = FitPixel(cube, noise, wl, nk, j, i, kSys, lambdaSys);
            if (r == null) continue;
            fitted++;
            int idx = j * ni + i;
            fluxMap[idx] = r.Value.flux;
            velMap[idx] = r.Value.velocity;
            dispMap[idx] = r.Value.dispersion;
            snrMap[idx] = r.Value.snr;
            if (r.Value.snr >= 5) good++;
        }

        // Galaxy center = flux-weighted centroid.
        (double xc, double yc) = FluxCentroid(fluxMap, ni, nj);

        // Estimate PA from velocity gradient; inclination from flux axial ratio.
        double pa = EstimatePA(velMap, fluxMap, ni, nj, xc, yc);
        double inc = EstimateInclination(fluxMap, ni, nj, xc, yc);

        // Disk model grid fit.
        var disk = GridDiskFit(velMap, snrMap, ni, nj, xc, yc, pa, inc);

        // Rotation curve along the major axis (deprojected).
        var rot = ExtractRotationCurve(velMap, snrMap, ni, nj, xc, yc, disk, ni, nj);

        // Distance scale.
        double kpcPerArcsec = KpcPerArcsec(Z);
        double arcsecPerPix = Math.Abs(cdelt2) * 3600.0;
        double kpcPerPix = kpcPerArcsec * arcsecPerPix;

        // Convert rotation curve radii from pixels to kpc.
        var rotKpc = rot.Select(p => new RotationPoint(
            p.Radius_kpc * kpcPerPix, p.Vrot_kms, p.Vrot_err_kms, p.Npix)).ToArray();

        // Export.
        double vmaxScale = MaxAbs(velMap);
        string velPng = Path.Combine(outDir, "velocity_map.png");
        string fluxPng = Path.Combine(outDir, "flux_map.png");
        string rotCsv = Path.Combine(outDir, "rotation_curve.csv");
        ImageMapExporter.SaveDiverging(velPng, velMap, ni, nj, -vmaxScale, vmaxScale);
        ImageMapExporter.SaveSequential(fluxPng, fluxMap, ni, nj, 0, MaxAbs(fluxMap));
        WriteRotationCsv(rotCsv, rotKpc);

        string classification = Classify(fitted, good, rotKpc, disk.Inclination_deg);

        var report = new KinematicsReport(
            BuildA(lambdaSys, Z, kSys, fitted, good),
            BuildB(velMap, ni, nj),
            BuildC(disk, pa, inc),
            BuildD(rotKpc, kpcPerPix, kpcPerArcsec, arcsecPerPix),
            BuildE(Z, kpcPerArcsec, classification),
            lambdaSys, lambdaSys, Z, arcsecPerPix, kpcPerArcsec,
            ni, nj, fitted, good,
            fluxMap, velMap, dispMap, snrMap,
            disk, rotKpc, classification, velPng, fluxPng, rotCsv);

        return report;
    }

    // ---- per-pixel Gaussian fit (robust peak + parabolic centroid) ----

    private static (double flux, double velocity, double dispersion, double snr)? FitPixel(
        double[,,] cube, double[,,] noise, double[] wl, int nk, int j, int i, int kSys, double lambdaSys)
    {
        // Search a wide window for the H-alpha peak.
        int wWide = 25;
        int kLo = Math.Max(0, kSys - wWide);
        int kHi = Math.Min(nk - 1, kSys + wWide);
        int kPeak = kLo;
        double peak = double.NegativeInfinity;
        for (int k = kLo; k <= kHi; k++)
            if (cube[k, j, i] > peak) { peak = cube[k, j, i]; kPeak = k; }

        double noiseLevel = EstimateNoise(noise, cube, nk, j, i, kPeak);

        // Fit window around the peak; robust median continuum.
        int wFit = 8;
        int fLo = Math.Max(0, kPeak - wFit);
        int fHi = Math.Min(nk - 1, kPeak + wFit);
        int n = fHi - fLo + 1;
        if (n < 5) return null;

        var raw = new double[n];
        for (int k = fLo; k <= fHi; k++) raw[k - fLo] = cube[k, j, i];
        double cont = Median(raw);
        var d = new double[n];
        for (int k = 0; k < n; k++) d[k] = raw[k] - cont;

        // Peak of the continuum-subtracted profile.
        int kp = 0;
        double dPeak = double.NegativeInfinity;
        for (int k = 0; k < n; k++) if (d[k] > dPeak) { dPeak = d[k]; kp = k; }
        if (dPeak <= 0) return null;

        double snr = noiseLevel > 0 ? dPeak / noiseLevel : double.NaN;

        // Sub-channel centroid via parabolic interpolation (bounded to +/-1 channel).
        double offset = 0;
        if (kp > 0 && kp < n - 1)
        {
            double denom = d[kp - 1] - 2 * d[kp] + d[kp + 1];
            if (Math.Abs(denom) > 1e-30) offset = 0.5 * (d[kp - 1] - d[kp + 1]) / denom;
        }
        offset = Math.Clamp(offset, -1.0, 1.0);
        double dLambda = wl.Length > 1 ? wl[1] - wl[0] : 1;
        double mu = wl[fLo + kp] + offset * dLambda;

        // Flux = sum of positive-signal channels (line integral above continuum).
        double flux = 0;
        for (int k = 0; k < n; k++) if (d[k] > 0) flux += d[k];

        // Dispersion from second moment around the centroid (positive-signal channels).
        double s2num = 0, s2den = 0;
        for (int k = 0; k < n; k++)
        {
            if (d[k] <= 0) continue;
            double w = wl[fLo + k];
            s2num += d[k] * (w - mu) * (w - mu);
            s2den += d[k];
        }
        double sigma = s2den > 0 ? Math.Sqrt(s2num / s2den) : dLambda;

        double velocity = c_kms * (mu - lambdaSys) / lambdaSys;
        double dispersion = c_kms * sigma / lambdaSys;

        // Physical sanity clamp: H-alpha velocities beyond ~2000 km/s are spurious.
        if (Math.Abs(velocity) > 2000) return null;

        return (flux, velocity, dispersion, snr);
    }

    private static double EstimateNoise(double[,,] noise, double[,,] cube, int nk, int j, int i, int kPeak)
    {
        // Prefer the noise cube value at the peak channel (positive => valid).
        double nv = noise[Math.Min(kPeak, noise.GetLength(0) - 1), j, i];
        if (nv > 0) return nv;
        // Fallback: RMS of the spectrum edges (continuum-only region).
        var vals = new List<double>();
        for (int k = 0; k < Math.Min(20, nk); k++) vals.Add(cube[k, j, i]);
        for (int k = Math.Max(0, nk - 20); k < nk; k++) vals.Add(cube[k, j, i]);
        if (vals.Count == 0) return 0;
        double mean = vals.Average();
        double rms = Math.Sqrt(vals.Average(v => (v - mean) * (v - mean)));
        return rms;
    }

    // ---- geometry helpers ----

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

    private static double EstimatePA(double[] velMap, double[] fluxMap, int ni, int nj, double xc, double yc)
    {
        // Fit V_los = a + b*(x-xc) + c*(y-yc); PA = atan2(b, c).
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
        double pa = Math.Atan2(b, c) * 180.0 / Math.PI; // measured E of N approx
        if (pa < 0) pa += 180.0;
        return pa;
    }

    private static double EstimateInclination(double[] fluxMap, int ni, int nj, double xc, double yc)
    {
        // Axial ratio b/a from second moments -> cos(i) ~ b/a.
        double sxx = 0, syy = 0, sxy = 0, sum = 0;
        for (int j = 0; j < nj; j++)
        for (int i = 0; i < ni; i++)
        {
            double f = fluxMap[j * ni + i];
            if (double.IsNaN(f) || f <= 0) continue;
            double dx = i - xc, dy = j - yc;
            sxx += f * dx * dx; syy += f * dy * dy; sxy += f * dx * dy; sum += f;
        }
        if (sum == 0) return 45;
        double mx2 = sxx / sum, my2 = syy / sum, mxy = sxy / sum;
        double tr = mx2 + my2;
        double det = Math.Sqrt((mx2 - my2) * (mx2 - my2) + 4 * mxy * mxy);
        double a2 = (tr + det) / 2, b2 = (tr - det) / 2;
        if (a2 <= 0) return 45;
        double ba = Math.Sqrt(Math.Max(b2, 0) / a2);
        double inc = Math.Acos(Math.Clamp(ba, 0.0, 1.0)) * 180.0 / Math.PI;
        return Math.Clamp(inc, 5, 85);
    }

    private static DiskFit GridDiskFit(double[] velMap, double[] snrMap, int ni, int nj, double xc, double yc, double paInit, double incInit)
    {
        double[] paGrid = { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165 };
        double[] incGrid = { 20, 35, 50, 65, 80 };
        double[] rtGrid = { 1, 2, 3, 4, 6, 8 };  // pixels (turnover radius)
        double bestChi2 = double.PositiveInfinity;
        var best = new DiskFit(0, incInit, paInit, 0, 3, double.NaN);

        foreach (double pa in paGrid)
        foreach (double inc in incGrid)
        foreach (double rt in rtGrid)
        {
            // Build linear system for V_los = Vsys + Vmax * f(R).
            double sinI = Math.Sin(inc * Math.PI / 180.0);
            double paR = pa * Math.PI / 180.0;
            double sA = 0, sB = 0, sC = 0, sD = 0, sE = 0, chi2 = 0; int cnt = 0;
            for (int j = 0; j < nj; j++)
            for (int i = 0; i < ni; i++)
            {
                double v = velMap[j * ni + i];
                double snr = double.IsNaN(snrMap[j * ni + i]) ? 0 : snrMap[j * ni + i];
                if (double.IsNaN(v) || snr < 3) continue;
                double dx = i - xc, dy = j - yc;
                double R = Math.Sqrt(dx * dx + dy * dy);
                if (R < 0.2) continue;
                double s = dx * Math.Sin(paR) + dy * Math.Cos(paR);
                double f = (2.0 / Math.PI) * Math.Atan(R / rt) * sinI * (s / R);
                double w = 1.0;
                sA += w; sB += w * f; sC += w * f * f; sD += w * v; sE += w * f * v; cnt++;
            }
            if (cnt < 8) continue;
            double det = sA * sC - sB * sB;
            if (Math.Abs(det) < 1e-12) continue;
            double Vsys = (sD * sC - sE * sB) / det;
            double Vmax = (sA * sE - sB * sD) / det;
            if (Vmax < 0) continue;
            // chi2
            chi2 = 0; int c2 = 0;
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
            chi2 /= c2;
            if (chi2 < bestChi2) { bestChi2 = chi2; best = new DiskFit(Vsys, inc, pa, Vmax, rt, chi2); }
        }
        return best;
    }

    private static RotationPoint[] ExtractRotationCurve(double[] velMap, double[] snrMap, int ni, int nj, double xc, double yc, DiskFit disk, int ni2, int nj2)
    {
        double sinI = Math.Sin(disk.Inclination_deg * Math.PI / 180.0);
        double paR = disk.PA_deg * Math.PI / 180.0;
        var bins = new Dictionary<int, List<double>>();
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
            double cosTheta = s / R;
            if (Math.Abs(cosTheta) < 0.3) continue;   // near minor axis
            double vrot = (v - disk.Vsys_kms) / (sinI * cosTheta);
            int bin = (int)Math.Round(R);
            if (!bins.ContainsKey(bin)) bins[bin] = new List<double>();
            bins[bin].Add(vrot);
        }
        var result = new List<RotationPoint>();
        foreach (var kv in bins.OrderBy(k => k.Key))
        {
            var vals = kv.Value.OrderBy(x => x).ToArray();
            double med = Median(vals);
            double mad = 1.4826 * Median(vals.Select(x => Math.Abs(x - med)).ToArray());
            result.Add(new RotationPoint(kv.Key, Math.Abs(med), mad, vals.Length));
        }
        return result.ToArray();
    }

    private static double KpcPerArcsec(double z)
    {
        // Comoving distance D_C (Mpc), then D_A = D_C/(1+z); kpc/arcsec = D_A_Mpc * 4.848e-3.
        const double H0 = 67.4;
        const double OmM = 0.315, OmL = 0.685;
        double Dc = 0; int n = 4000; double dz = z / n;
        for (int k = 0; k < n; k++)
        {
            double zz = (k + 0.5) * dz;
            double E = Math.Sqrt(OmM * Math.Pow(1 + zz, 3) + OmL);
            Dc += 299792.458 / H0 / E * dz;
        }
        double DaMpc = Dc / (1 + z);
        return DaMpc * 4.848e-3;   // kpc per arcsec
    }

    // ---- helpers ----

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
        {
            if (h is ImageHDU ih)
            {
                string en = FitsHeaderReport.SafeString(ih.Header, "EXTNAME");
                if (en.Contains(namePart, StringComparison.OrdinalIgnoreCase)) return ih;
            }
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
        var a = vals.Where(v => !double.IsNaN(v)).OrderBy(v => v).ToArray();
        if (a.Length == 0) return 0;
        int n = a.Length;
        return n % 2 == 1 ? a[n / 2] : 0.5 * (a[n / 2 - 1] + a[n / 2]);
    }

    private static double MaxAbs(double[] map)
    {
        double m = 0;
        foreach (var v in map) if (!double.IsNaN(v)) m = Math.Max(m, Math.Abs(v));
        return m == 0 ? 1 : m;
    }

    private static void WriteRotationCsv(string path, RotationPoint[] rot)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("radius_kpc,vrot_kms,vrot_err_kms,npix");
        foreach (var p in rot)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0:F3},{1:F1},{2:F1},{3}", p.Radius_kpc, p.Vrot_kms, p.Vrot_err_kms, p.Npix));
        File.WriteAllText(path, sb.ToString());
    }

    private static string Classify(int fitted, int good, RotationPoint[] rot, double inclination)
    {
        if (fitted == 0) return "A = no velocity field";
        if (inclination < 25)
            return "B = velocity field visible (near face-on; rotation-curve deprojection unreliable)";
        if (rot.Length < 3) return "B = velocity field visible";
        double vmax = rot.Length > 0 ? rot.Max(p => p.Vrot_kms) : 0;
        if (vmax < 30) return "B = velocity field visible (weak)";
        if (good > fitted * 0.3) return "D = high-quality kinematic galaxy";
        return "C = rotation curve measurable";
    }

    // ---- report sections ----

    private static string BuildA(double lambdaSys, double z, int kSys, int fitted, int good)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"H-alpha rest wavelength: {HA_REST:F1} A");
        sb.AppendLine($"Redshift: z = {z:F4}");
        sb.AppendLine($"Expected observed wavelength: lambda_obs = {HA_REST:F1} * (1+z) = {lambdaSys:F2} A");
        sb.AppendLine($"Nearest cube channel: {kSys}");
        sb.AppendLine();
        sb.AppendLine($"Pixels with a successful H-alpha fit: {fitted}");
        sb.AppendLine($"Pixels with SNR >= 5 (high quality): {good}");
        return sb.ToString();
    }

    private static string BuildB(double[] velMap, int ni, int nj)
    {
        var sb = new System.Text.StringBuilder();
        var vs = velMap.Where(v => !double.IsNaN(v)).ToArray();
        if (vs.Length == 0) { sb.AppendLine("No velocity measurements."); return sb.ToString(); }
        double vmin = vs.Min(), vmax = vs.Max(), vmean = vs.Average();
        double spread = vmax - vmin;
        sb.AppendLine($"Velocity range: {vmin:F0} .. {vmax:F0} km/s (span {spread:F0} km/s)");
        sb.AppendLine($"Mean velocity: {vmean:F0} km/s");
        sb.AppendLine($"Velocity spread {spread:F0} km/s " +
            (spread > 60 ? "=> significant rotation / velocity gradient detected" :
             spread > 20 ? "=> modest velocity gradient (rotation possible)" :
                           "=> weak velocity structure (face-on, compact, or low signal)"));
        return sb.ToString();
    }

    private static string BuildC(DiskFit d, double paInit, double incInit)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Systemic velocity: Vsys = {d.Vsys_kms:F0} km/s");
        sb.AppendLine($"Inclination: i = {d.Inclination_deg:F0} deg");
        sb.AppendLine($"Position angle: PA = {d.PA_deg:F0} deg");
        sb.AppendLine($"Max rotation: Vmax = {d.Vmax_kms:F0} km/s");
        sb.AppendLine($"Turnover radius: r_t = {d.TurnoverRadius_kpc:F1} pixels (approx)");
        sb.AppendLine($"Reduced chi^2: {d.Chi2:F1}");
        sb.AppendLine();
        sb.AppendLine($"Disk-fit quality: " +
            (d.Inclination_deg < 25 ? "UNCERTAIN — near face-on, deprojection unreliable" :
             d.Vmax_kms > 100 ? "GOOD — clear rotation, well-constrained disk" :
             d.Vmax_kms > 40 ? "MODERATE — rotation detected, disk parameters uncertain" :
                               "WEAK — low-amplitude rotation or poor signal"));
        if (d.Inclination_deg < 25)
            sb.AppendLine("  CAVEAT: inclination estimate (from flux axial ratio) is near face-on");
        sb.AppendLine("  (i < 25 deg). For clumpy high-z galaxies this estimate is unreliable and");
        sb.AppendLine("  the rotation-curve deprojection Vrot = (V-Vsys)/(sin i cos theta) blows up.");
        sb.AppendLine("  Treat Vmax and the deprojected rotation curve as UPPER limits / tentative.");
        return sb.ToString();
    }

    private static string BuildD(RotationPoint[] rot, double kpcPerPix, double kpcPerArcsec, double arcsecPerPix)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Angular scale: {arcsecPerPix:F2} arcsec/pixel; {kpcPerArcsec:F2} kpc/arcsec; {kpcPerPix:F2} kpc/pixel (z=1.800)");
        sb.AppendLine();
        sb.AppendLine($"Rotation curve ({rot.Length} radial bins):");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,10} {1,10} {2,12}", "R (kpc)", "Vrot (km/s)", "err (km/s)"));
        foreach (var p in rot)
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,10:F2} {1,10:F1} {2,12:F1}", p.Radius_kpc, p.Vrot_kms, p.Vrot_err_kms));
        return sb.ToString();
    }

    private static string BuildE(double z, double kpcPerArcsec, string classification)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QG-070 RAR-evolution test.");
        sb.AppendLine($"  At z = {z:F3}, predicted g†(z)/g†(0) = sqrt(0.315(1+z)^3 + 0.685) = {Math.Sqrt(0.315*Math.Pow(1+z,3)+0.685):F3}.");
        sb.AppendLine($"  Spatial scale: {kpcPerArcsec:F2} kpc/arcsec.");
        sb.AppendLine();
        sb.AppendLine($"  CLASSIFICATION: {classification}");
        sb.AppendLine("  -> If Vmax is well measured, the flat part of the rotation curve");
        sb.AppendLine("     constrains g†(z) via the RAR (g_obs vs g_bar relation),");
        sb.AppendLine("     placing this galaxy on the high-z g† evolution curve.");
        return sb.ToString();
    }
}
