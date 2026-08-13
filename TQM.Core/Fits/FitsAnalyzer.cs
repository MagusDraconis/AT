using System.Globalization;
using nom.tam.fits;

namespace TQM.Core.FitsAnalysis;

/// <summary>
/// Orchestrates the full FITS analysis: structure, headers, wavelength solution,
/// emission-line detection, kinematic suitability, and QG-070 classification.
/// </summary>
public static class FitsAnalyzer
{
    public static FitsReport Analyze(string path)
    {
        // Keep the FITS stream open during the whole analysis: CSharpFITS reads
        // image data lazily on first DataArray access, which needs the stream.
        var fits = new Fits(path);
        try
        {
            BasicHDU[] hdus = fits.Read();
            return AnalyzeHdus(hdus);
        }
        finally
        {
            fits.Close();
        }
    }

    private static FitsReport AnalyzeHdus(BasicHDU[] hdus)
    {
        var infos = FitsHeaderReport.BuildHduInfos(hdus);
        var primaryHeader = FitsHeaderReport.DumpPrimaryHeader(hdus);

        // Identify the science cube.
        var cube = FitsCubeInspector.FindScienceCube(hdus);
        double[]? wavelength = null;
        double[] spectrum = Array.Empty<double>();
        string lineReport = "No spectral cube found.";
        double redshift = double.NaN;
        string detected = "";

        if (cube is { } c)
        {
            int naxes = (int)FitsHeaderReport.SafeDouble(c.hdu.Header, "NAXIS", 0); // number of axes
            int naxis3 = (int)FitsHeaderReport.SafeDouble(c.hdu.Header, "NAXIS3", 0); // wavelength axis length
            if (naxes >= 3 && naxis3 > 0)
            {
                wavelength = FitsCubeInspector.ComputeWavelength(c.hdu, naxis3);
                spectrum = FitsCubeInspector.CollapseSpectrum(c.data);
                if (wavelength != null)
                {
                    var lr = FitsCubeInspector.DetectLines(wavelength, spectrum);
                    lineReport = lr.report;
                    redshift = lr.redshift;
                    detected = lr.detected;
                }
                else
                {
                    lineReport = "Cube is 3D but no wavelength axis (CTYPE3/CRVAL3/CDELT3) found — emission-line analysis skipped.";
                }
            }
            else
            {
                lineReport = $"Primary image has NAXIS={naxes} (not a spectral cube) — no emission-line analysis.";
            }
        }

        // Kinematic suitability.
        string kinematics = FitsCubeInspector.AssessKinematics(hdus, infos, cube?.hdu, wavelength);

        // Build report sections.
        string sa = BuildSectionA(hdus, infos);
        string sb = BuildSectionB(hdus, primaryHeader);
        string sc = BuildSectionC(wavelength, cube);
        string sd = lineReport;
        string se = kinematics;
        string sf = BuildSectionF(wavelength, redshift);
        string sg = BuildSectionG(infos, cube, wavelength);

        string classification = Classify(infos, cube, wavelength);

        return new FitsReport(sa, sb, sc, sd, se, sf, sg, infos, primaryHeader,
            wavelength ?? Array.Empty<double>(), spectrum, redshift, detected, classification);
    }

    // ---- Section builders ----

    private static string BuildSectionA(BasicHDU[] hdus, HduInfo[] infos)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Total HDUs: {hdus.Length}");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,-20} {2,-14} {3,-10} {4,-12} {5,-22} {6}", "#", "Type", "EXTNAME", "Axes", "Data type", "Shape", "Purpose"));
        sb.AppendLine("  " + new string('-', 100));
        foreach (var i in infos)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,-20} {2,-14} {3,-10} {4,-12} {5,-22} {6}",
                i.Index, i.TypeName, i.ExtName, i.Axes, i.DataType, i.DataShape, i.Purpose));
        }
        return sb.ToString();
    }

    private static string BuildSectionB(BasicHDU[] hdus, HeaderEntry[] primaryHeader)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("All HDU headers (key = value / comment):");
        sb.AppendLine();
        for (int h = 0; h < hdus.Length; h++)
        {
            string extName = FitsHeaderReport.SafeString(hdus[h].Header, "EXTNAME");
            sb.AppendLine($"  --- HDU {h}" + (string.IsNullOrEmpty(extName) ? " (primary)" : $" [{extName}]") + " ---");
            var entries = FitsHeaderReport.DumpHeader(hdus[h].Header);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-14} {1,-24} {2}", "KEY", "VALUE", "COMMENT"));
            foreach (var e in entries)
            {
                string val = e.Value.Length > 24 ? e.Value.Substring(0, 24) : e.Value;
                string com = e.Comment.Length > 40 ? e.Comment.Substring(0, 40) : e.Comment;
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "  {0,-14} {1,-24} {2}", e.Key, val, com));
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    private static string BuildSectionC(double[]? wl, (int hduIndex, ImageHDU hdu, Array data)? cube)
    {
        var sb = new System.Text.StringBuilder();
        if (cube is not { } c)
        {
            sb.AppendLine("No science cube identified.");
            return sb.ToString();
        }
        sb.AppendLine($"Science cube = HDU {c.hduIndex}, shape {DescribeShape(c.data)}");
        if (wl is { Length: > 0 })
        {
            sb.AppendLine($"Wavelength axis: {wl.Length} channels");
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  lambda_min = {0:F2} A", wl[0]));
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  lambda_max = {0:F2} A", wl[wl.Length - 1]));
            double dlambda = wl.Length > 1 ? (wl[wl.Length - 1] - wl[0]) / (wl.Length - 1) : 0;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  delta-lambda = {0:F3} A/channel", dlambda));
            if (wl.Length > 1 && dlambda != 0)
            {
                double R = wl[wl.Length / 2] / dlambda;
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                    "  spectral resolution R ~ {0:F0} (channel-limited)", R));
            }
        }
        else
        {
            sb.AppendLine("No wavelength solution (CTYPE3 not wavelength-like, or missing CRVAL3/CDELT3).");
        }
        return sb.ToString();
    }

    private static string BuildSectionF(double[]? wl, double redshift)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QG-070 relevance: RAR evolution g†(z) = c·H(z)/(2π).");
        sb.AppendLine();
        if (double.IsNaN(redshift))
        {
            sb.AppendLine("  Redshift not yet determined from lines.");
            sb.AppendLine("  -> Cannot yet place this galaxy on the g†(z) curve.");
        }
        else
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  Estimated redshift z = {0:F4}", redshift));
            double ratio = Math.Sqrt(0.315 * Math.Pow(1 + redshift, 3) + 0.685);
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  Predicted g†(z)/g†(0) = sqrt(0.315(1+z)^3 + 0.685) = {0:F3}", ratio));
            sb.AppendLine("  -> If a rotation curve is derivable, its acceleration scale should");
            sb.AppendLine("     scale by this factor relative to local g† = c·H0/(2π).");
        }
        return sb.ToString();
    }

    private static string BuildSectionG(HduInfo[] infos, (int hduIndex, ImageHDU hdu, Array data)? cube, double[]? wl)
    {
        var sb = new System.Text.StringBuilder();
        bool hasCube = cube != null;
        bool hasWavelength = wl is { Length: > 0 };
        bool hasVelocity = infos.Any(i => i.ExtName.Contains("VEL", StringComparison.OrdinalIgnoreCase));

        sb.AppendLine($"Classification inputs: cube={hasCube}, wavelength={hasWavelength}, velocity_field={hasVelocity}");
        sb.AppendLine();

        if (hasVelocity)
            sb.AppendLine("  VERDICT: C/D — kinematic data present; directly usable for rotation-curve analysis.");
        else if (hasCube && hasWavelength)
            sb.AppendLine("  VERDICT: B/C — science cube with wavelength solution; velocity field derivable via line fitting.");
        else if (hasCube)
            sb.AppendLine("  VERDICT: B — science cube present, but wavelength solution missing/not standard.");
        else
            sb.AppendLine("  VERDICT: A — no science cube (calibration/other product only).");

        return sb.ToString();
    }

    private static string Classify(HduInfo[] infos, (int hduIndex, ImageHDU hdu, Array data)? cube, double[]? wl)
    {
        bool hasVelocity = infos.Any(i => i.ExtName.Contains("VEL", StringComparison.OrdinalIgnoreCase));
        bool hasDispersion = infos.Any(i => i.ExtName.Contains("DISP", StringComparison.OrdinalIgnoreCase));
        bool hasCube = cube != null;
        bool hasWavelength = wl is { Length: > 0 };

        if (hasVelocity && hasDispersion) return "D = directly usable for rotation-curve analysis";
        if (hasVelocity) return "C = kinematic cube (velocity field present)";
        if (hasCube && hasWavelength) return "C = kinematic-capable science cube (velocity derivable)";
        if (hasCube) return "B = science cube";
        return "A = calibration / non-science product";
    }

    private static string DescribeShape(Array arr)
    {
        if (arr.Length > 0 && arr.GetValue(0) is Array inner && inner.Rank >= 1)
            return $"{arr.Length}x{DescribeShape(inner)}";
        return arr.Length.ToString();
    }
}
