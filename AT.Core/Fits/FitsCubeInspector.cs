using System.Globalization;
using nom.tam.fits;

namespace AT.Core.FitsAnalysis;

/// <summary>
/// Inspects an IFU spectral cube: wavelength solution, collapsed spectrum,
/// emission-line detection, redshift estimate, and kinematic suitability.
/// </summary>
public static class FitsCubeInspector
{
    // Rest-frame line catalogue (Angstrom).
    private static readonly (string Name, double Wave)[] Lines =
    {
        ("H-beta", 4861.33),
        ("[OIII] 4959", 4958.91),
        ("[OIII] 5007", 5006.84),
        ("[NII] 6548", 6548.05),
        ("H-alpha", 6562.80),
        ("[NII] 6583", 6583.45),
        ("[SII] 6717", 6716.44),
        ("[SII] 6731", 6730.82),
    };

    /// <summary>Find the most likely science cube (largest-rank image HDU).</summary>
    public static (int hduIndex, ImageHDU hdu, Array data)? FindScienceCube(BasicHDU[] hdus)
    {
        int best = -1;
        ImageHDU bestHdu = null!;
        Array bestData = null!;
        int bestSize = -1;
        for (int i = 0; i < hdus.Length; i++)
        {
            if (hdus[i] is ImageHDU ih && ih.Data is ImageData img && img.DataArray is Array arr)
            {
                int size = arr.Length;
                if (size > bestSize) { bestSize = size; best = i; bestHdu = ih; bestData = arr; }
            }
        }
        if (best < 0) return null;
        return (best, bestHdu, bestData);
    }

    /// <summary>Compute the wavelength array for a cube's third axis (Angstrom).</summary>
    public static double[]? ComputeWavelength(ImageHDU hdu, int naxis3)
    {
        Header h = hdu.Header;
        string ctype3 = FitsHeaderReport.SafeString(h, "CTYPE3");
        bool isWave = ctype3.Contains("WAVE", StringComparison.OrdinalIgnoreCase) ||
                      ctype3.Contains("AWAV", StringComparison.OrdinalIgnoreCase);
        bool isFreq = ctype3.Contains("FREQ", StringComparison.OrdinalIgnoreCase);

        double crval3 = FitsHeaderReport.SafeDouble(h, "CRVAL3", double.NaN);
        double cdelt3 = FitsHeaderReport.SafeDouble(h, "CDELT3", FitsHeaderReport.SafeDouble(h, "CD3_3", 0.0));
        double crpix3 = FitsHeaderReport.SafeDouble(h, "CRPIX3", 1.0);
        string cunit3 = FitsHeaderReport.SafeString(h, "CUNIT3");

        if (double.IsNaN(crval3) || cdelt3 == 0.0 || !(isWave || isFreq)) return null;

        var wl = new double[naxis3];
        if (isFreq)
        {
            // Frequency axis (Hz): lambda = c / nu (Angstrom).
            const double c = 2.99792458e18;
            for (int k = 0; k < naxis3; k++)
            {
                double nu = crval3 + (k + 1 - crpix3) * cdelt3;
                wl[k] = nu > 0 ? c / nu : double.NaN;
            }
        }
        else
        {
            double toAng = UnitToAngstrom(cunit3);
            crval3 *= toAng;
            cdelt3 *= toAng;
            for (int k = 0; k < naxis3; k++)
            {
                wl[k] = crval3 + (k + 1 - crpix3) * cdelt3;
            }
        }
        return wl;
    }

    private static double UnitToAngstrom(string cunit)
    {
        string u = (cunit ?? "").Trim().ToLowerInvariant();
        if (u.Contains("um") || u.Contains("micron")) return 1e4;
        if (u.Contains("nm")) return 10;
        if (u == "m") return 1e10;
        if (u.Contains("angstrom") || u == "a") return 1;
        return 1;
    }

    /// <summary>Collapse a spectral cube (jagged array; outermost index = wavelength) into a 1D spectrum.</summary>
    public static double[] CollapseSpectrum(Array cube)
    {
        int nz = cube.Length;               // outer length = NAXIS3 (wavelength axis)
        var spec = new double[nz];
        for (int k = 0; k < nz; k++)
        {
            object plane = cube.GetValue(k);
            spec[k] = SumAll(plane);
        }
        return spec;
    }

    private static double SumAll(object node)
    {
        if (node is Array arr)
        {
            double s = 0;
            foreach (object? child in arr) s += SumAll(child!);
            return s;
        }
        double v = Convert.ToDouble(node, CultureInfo.InvariantCulture);
        return double.IsNaN(v) || double.IsInfinity(v) ? 0 : v;
    }

    /// <summary>Detect strong emission lines and estimate redshift via the strongest line (H-alpha default).</summary>
    public static (string report, double redshift, string detected) DetectLines(double[] wl, double[] spec)
    {
        if (wl.Length == 0 || spec.Length != wl.Length)
            return ("No wavelength solution or empty spectrum — line detection skipped.", double.NaN, "");

        // Find the continuum baseline (median) and noise (robust MAD).
        double median = Median(spec);
        double mad = 1.4826 * Median(spec.Select(v => Math.Abs(v - median)).ToArray());
        double threshold = median + 6.0 * (mad > 0 ? mad : Math.Abs(median) * 1e-3 + 1e-20);

        // Detect local maxima above threshold.
        var peaks = new List<(double wl, double flux)>();
        for (int i = 1; i < spec.Length - 1; i++)
        {
            if (spec[i] > threshold && spec[i] >= spec[i - 1] && spec[i] >= spec[i + 1])
                peaks.Add((wl[i], spec[i]));
        }

        var top = peaks.OrderByDescending(p => p.flux).Take(10).ToArray();

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  Continuum (median) = {median:E3};  MAD = {mad:E3};  6-sigma threshold = {threshold:E3}");
        sb.AppendLine($"  Peaks detected above threshold: {peaks.Count}");

        if (top.Length == 0)
        {
            sb.AppendLine("  No significant emission lines detected (continuum or absorption object).");
            return (sb.ToString(), double.NaN, "");
        }

        // Primary line = strongest peak. Candidate identifications (common KMOS3D lines).
        double wlStrong = top[0].wl;
        double fluxStrong = top[0].flux;
        sb.AppendLine();
        sb.AppendLine($"  STRONGEST peak: lambda_obs = {wlStrong:F1} A, flux = {fluxStrong:E3}");
        sb.AppendLine("  Candidate identifications for the strongest line:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "    {0,-16} {1,-12} {2}", "Line", "rest (A)", "implied z"));
        var zTable = new List<(string line, double z)>();
        foreach (var (name, rest) in Lines)
        {
            double z = wlStrong / rest - 1.0;
            if (z >= 0 && z <= 10) { zTable.Add((name, z)); }
        }
        foreach (var (line, z) in zTable.OrderBy(x => x.z))
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "    {0,-16} {1,-12:F1} z = {2:F4}", line, wlStrong / (1 + z), z));

        // Primary estimate: H-alpha is the dominant line in KMOS3D H-band (z ~ 1.2-1.8).
        double zHa = wlStrong / 6562.80 - 1.0;
        sb.AppendLine();
        sb.AppendLine($"  PRIMARY redshift estimate (strongest line = H-alpha): z = {zHa:F4}");
        sb.AppendLine("  (If the strongest line is instead [OIII]5007 / H-beta / [NII], use the");
        sb.AppendLine("   alternative z values from the table above.)");

        // Consistency check: at z_Ha, do we also see [NII] 6548/6583 (H-alpha complex)?
        var detected = new List<string> { "H-alpha" };
        foreach (var (name, rest) in Lines)
        {
            double expected = rest * (1 + zHa);
            int idx = NearestIndex(wl, expected);
            double tol = wl.Length > 1 ? Math.Abs(wl[1] - wl[0]) * 3 : 3; // 3 channels
            if (Math.Abs(wl[idx] - expected) < tol && spec[idx] > threshold)
            {
                detected.Add(name);
            }
        }

        if (detected.Count > 1)
            sb.AppendLine($"  At z={zHa:F4}, additional lines detected: {string.Join(", ", detected.Skip(1))}");
        else
            sb.AppendLine($"  At z={zHa:F4}, no clear companion lines ([NII]/[SII]) above threshold.");

        return (sb.ToString(), zHa, string.Join(", ", detected.Distinct()));
    }

    private static int NearestIndex(double[] wl, double target)
    {
        int lo = 0, hi = wl.Length - 1;
        while (lo < hi)
        {
            int mid = (lo + hi) / 2;
            if (wl[mid] < target) lo = mid + 1; else hi = mid;
        }
        if (lo > 0 && Math.Abs(wl[lo - 1] - target) < Math.Abs(wl[lo] - target)) return lo - 1;
        return lo;
    }

    private static double Median(IEnumerable<double> values)
    {
        var arr = values.Where(v => !double.IsNaN(v) && !double.IsInfinity(v)).OrderBy(v => v).ToArray();
        if (arr.Length == 0) return 0;
        int n = arr.Length;
        return n % 2 == 1 ? arr[n / 2] : 0.5 * (arr[n / 2 - 1] + arr[n / 2]);
    }

    /// <summary>Assess whether a velocity field / rotation curve can be extracted.</summary>
    public static string AssessKinematics(BasicHDU[] hdus, HduInfo[] infos, ImageHDU? cube, double[]? wl)
    {
        var sb = new System.Text.StringBuilder();
        bool hasVelocity = infos.Any(i => i.ExtName.Contains("VEL", StringComparison.OrdinalIgnoreCase));
        bool hasDispersion = infos.Any(i => i.ExtName.Contains("DISP", StringComparison.OrdinalIgnoreCase));
        bool hasCube = cube != null;
        bool hasWavelength = wl != null && wl.Length > 0;

        sb.AppendLine($"  Spectral cube present: {hasCube}");
        sb.AppendLine($"  Wavelength solution present: {hasWavelength}");
        sb.AppendLine($"  Pre-computed velocity field: {hasVelocity}");
        sb.AppendLine($"  Pre-computed dispersion map: {hasDispersion}");

        if (hasVelocity) sb.AppendLine("  -> Velocity field AVAILABLE: rotation curve derivable directly.");
        else if (hasCube && hasWavelength)
        {
            sb.AppendLine("  -> No pre-computed velocity field, BUT a spectral cube + wavelength");
            sb.AppendLine("     solution means a velocity field can be DERIVED (fit emission lines");
            sb.AppendLine("     per spaxel). Rotation-curve analysis is POSSIBLE.");
        }
        else
        {
            sb.AppendLine("  -> Neither a velocity field nor a wavelength-solved cube is available.");
            sb.AppendLine("     Rotation-curve analysis NOT directly possible from this file alone.");
        }
        return sb.ToString();
    }
}
