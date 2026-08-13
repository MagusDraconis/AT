using System.Globalization;
using System.Text;
using SixLabors.ImageSharp.PixelFormats;
using TQM.Core.FitsAnalysis;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG071_HighZRARExtractionAudit : ResearchTestBase
{
    public TQM_QG071_HighZRARExtractionAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG071_HighZRARExtraction()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        Assert.True(Directory.Exists(fitsDir), $"FITS data directory not found: {fitsDir}");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);
        string plotDir = Path.Combine(outDir, "Plots");
        Directory.CreateDirectory(plotDir);

        var sb = new StringBuilder();
        PrintHeader("QG-071 — High-z RAR Extraction Audit (SPARC-calibrated)");

        // ---- Local SPARC calibration ----
        var sparc = SPARCRARAnalyzer.Run(dataDir);
        S(sb, "Section 0 — SPARC local calibration");
        sb.AppendLine($"  SPARC radius points: {sparc.NRadiusPoints}; galaxies: {sparc.NGalaxies}");
        sb.AppendLine($"  Local g† (McGaugh form) = {sparc.GdaggerLocalMcGaugh_m_s2:E2} m/s²");
        sb.AppendLine($"  Local g† (TQM form)     = {sparc.GdaggerLocalTqm_m_s2:E2} m/s²");
        sb.AppendLine($"  BTFR: log M_bar = {sparc.BTFR_a:F3} + {sparc.BTFR_b:F3} log Vflat  (scatter {sparc.BTFR_scatter_dex:F2} dex)");

        // ---- Catalog + pilot ----
        var cat = KinematicCatalogAnalyzer.Run(fitsDir, outDir);
        Assert.True(File.Exists(cat.Top20CsvPath));
        var pilot = HighZRarAnalyzer.Run(fitsDir, cat.Top20CsvPath, outDir);
        string rotationCsv = Path.Combine(outDir, "HighZ_RotationCatalog.csv");
        Assert.True(File.Exists(rotationCsv));

        WriteGalaxyCatalog(Path.Combine(outDir, "HighZGalaxyCatalog.csv"), cat);

        // ---- High-z RAR extraction (BTFR prior) ----
        HighZRARExtractionReport r = HighZRARExtractionAnalyzer.Run(
            fitsDir, cat.Top20CsvPath, rotationCsv, outDir, sparc.BTFR_a, sparc.BTFR_b);

        S(sb, "Section A — Accepted galaxy sample"); sb.AppendLine(r.SA);
        S(sb, "Section B — Rotation-curve extraction"); sb.AppendLine(r.SB);
        S(sb, "Section C — Baryonic acceleration estimates"); sb.AppendLine(r.SC);
        S(sb, "Section D — Individual g† measurements"); sb.AppendLine(r.SD);
        S(sb, "Section E — Redshift-binned g† evolution"); sb.AppendLine(r.SE);
        S(sb, "Section F — TQM vs MOND comparison"); sb.AppendLine(r.SF);
        S(sb, "Section G — Statistical significance"); sb.AppendLine(r.SG);
        S(sb, "Section H — Falsification analysis"); sb.AppendLine(r.SH);
        S(sb, "Section I — Final verdict"); sb.AppendLine(r.SI);

        S(sb, "Section J — Generated plots");
        foreach (string p in GeneratePlots(plotDir, sparc, r))
            sb.AppendLine("  " + p);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  VERDICT: " + r.VerdictClass);
        sb.AppendLine("  galaxies with g† estimate: " + r.Fits.Length);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG071_HighZRAR_Report.txt"), sb.ToString());

        Assert.True(r.Fits.Length > 0, "no g† estimates produced");
        Assert.True(File.Exists(Path.Combine(plotDir, "Local_RAR.png")), "Local_RAR.png missing");
    }

    private static void WriteGalaxyCatalog(string path, CatalogReport cat)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Object,Redshift,Band,Exposure,SNR,Inclination,VelocitySpan,KinematicScore");
        for (int i = 0; i < cat.Entries.Length; i++)
        {
            var e = cat.Entries[i];
            var d = cat.Details[i];
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "{0},{1:F4},{2},{3:F1},{4:F1},{5:F1},{6:F0},{7:F1}",
                e.ObjectId, e.Redshift, e.Band, e.ExposureMinutes, e.SNR, e.InclinationDeg,
                d.VelocitySpanKms, e.KinematicScore));
        }
        File.WriteAllText(path, sb.ToString());
    }

    private static string[] GeneratePlots(string plotDir, SPARCReport sparc, HighZRARExtractionReport r)
    {
        var blue = new Rgb24(40, 80, 220);
        var red = new Rgb24(220, 60, 60);
        var green = new Rgb24(40, 160, 60);
        var orange = new Rgb24(230, 140, 40);

        double g0 = 299792.458 * (67.4 / 3.0857e19 * 1e3) / (2.0 * Math.PI);

        {
            var gb = LogRange(-14, -8, 120);
            var mc = gb.Select(g => g / (1 - Math.Exp(-Math.Sqrt(g / sparc.GdaggerLocalMcGaugh_m_s2)))).ToArray();
            var tq = gb.Select(g => g * Math.Sqrt(1 + sparc.GdaggerLocalTqm_m_s2 / g)).ToArray();
            var series = new[]
            {
                new RARPlotter.Series(sparc.Gbar_m_s2, sparc.Gobs_m_s2, blue, false, 1),
                new RARPlotter.Series(gb, mc, red, true, 1),
                new RARPlotter.Series(gb, tq, green, true, 1),
            };
            RARPlotter.PlotLogLog(Path.Combine(plotDir, "Local_RAR.png"), series, 1e-14, 1e-8, 1e-13, 1e-8);
        }

        {
            var gobs = r.Curves.SelectMany(c => c.Gobs_m_s2).Where(g => !double.IsNaN(g) && g > 0).ToArray();
            var gbar = r.Baryons.SelectMany(b => b.Gbar_m_s2).Where(g => !double.IsNaN(g) && g > 0).ToArray();
            int n = Math.Min(gobs.Length, gbar.Length);
            var gb = LogRange(-14, -8, 120);
            var mc = gb.Select(g => g / (1 - Math.Exp(-Math.Sqrt(g / sparc.GdaggerLocalMcGaugh_m_s2)))).ToArray();
            var series = new[]
            {
                new RARPlotter.Series(gbar.Take(n).ToArray(), gobs.Take(n).ToArray(), orange, false, 2),
                new RARPlotter.Series(gb, mc, red, true, 1),
            };
            RARPlotter.PlotLogLog(Path.Combine(plotDir, "HighZ_RAR.png"), series, 1e-14, 1e-8, 1e-13, 1e-8);
        }

        {
            var zArr = r.Fits.Where(f => !double.IsNaN(f.Gdagger_m_s2)).Select(f => f.Redshift).ToArray();
            var gArr = r.Fits.Where(f => !double.IsNaN(f.Gdagger_m_s2)).Select(f => f.Gdagger_m_s2).ToArray();
            double[] zGrid = { 0.0, 0.5, 1.0, 1.5, 2.0, 2.5, 3.0 };
            var tqmCurve = zGrid.Select(z => g0 * Math.Sqrt(0.315 * Math.Pow(1 + z, 3) + 0.685)).ToArray();
            var mondCurve = zGrid.Select(_ => g0).ToArray();
            var series = new[]
            {
                new RARPlotter.Series(zArr, gArr, orange, false, 3),
                new RARPlotter.Series(zGrid, tqmCurve, red, true, 1),
                new RARPlotter.Series(zGrid, mondCurve, green, true, 1),
            };
            RARPlotter.PlotSemiLogY(Path.Combine(plotDir, "gdagger_vs_z.png"), series, 0.0, 3.0, 1e-13, 1e-8);
        }

        {
            var labels = r.Comparisons.Select(c => c.Model).ToArray();
            var chi2 = r.Comparisons.Select(c => c.Chi2).ToArray();
            RARPlotter.PlotBars(Path.Combine(plotDir, "TQM_vs_MOND.png"), labels, chi2, blue);
        }

        return new[]
        {
            Path.Combine(plotDir, "Local_RAR.png"),
            Path.Combine(plotDir, "HighZ_RAR.png"),
            Path.Combine(plotDir, "gdagger_vs_z.png"),
            Path.Combine(plotDir, "TQM_vs_MOND.png"),
        };
    }

    private static double[] LogRange(double lmin, double lmax, int n)
    {
        var a = new double[n];
        for (int i = 0; i < n; i++)
            a[i] = Math.Pow(10, lmin + (lmax - lmin) * i / (n - 1));
        return a;
    }

    private static void S(StringBuilder sb, string title)
    {
        sb.AppendLine();
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));
    }

    private static string LocateDir(params string[] segments)
    {
        string combined = Path.Combine(segments);
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, combined);
            if (Directory.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine(@"D:\Coding\Test\TQM", combined);
    }
}
