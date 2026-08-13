using System.Globalization;
using System.Text;
using TQM.Core.FitsAnalysis;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG074_DirectGdaggerMeasurementAudit : ResearchTestBase
{
    public TQM_QG074_DirectGdaggerMeasurementAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG074_DirectGdaggerMeasurement()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        Assert.True(Directory.Exists(fitsDir));

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        // Regenerate the rotation curves (catalog -> pilot -> RAR extraction).
        var cat = KinematicCatalogAnalyzer.Run(fitsDir, outDir);
        var pilot = HighZRarAnalyzer.Run(fitsDir, cat.Top20CsvPath, outDir);
        string rotationCatalog = Path.Combine(outDir, "HighZ_RotationCatalog.csv");
        Assert.True(File.Exists(rotationCatalog));
        var sparc = SPARCRARAnalyzer.Run(dataDir);
        HighZRARExtractionAnalyzer.Run(fitsDir, cat.Top20CsvPath, rotationCatalog, outDir, sparc.BTFR_a, sparc.BTFR_b);

        // Independent masses (persistent derived data).
        string massCatalog = Path.Combine(dataDir, "derived", "KMOS3D_MassCatalog.csv");
        Assert.True(File.Exists(massCatalog), "KMOS3D_MassCatalog.csv not found (run QG-073)");
        string rotDir = Path.Combine(outDir, "RotationCurves");
        Assert.True(Directory.Exists(rotDir) && Directory.GetFiles(rotDir).Length > 0);

        var sb = new StringBuilder();
        PrintHeader("QG-074 — First Direct High-z g† Measurement");

        DirectGdaggerReport r = DirectGdaggerAnalyzer.Run(massCatalog, rotDir, outDir);

        S(sb, "Section A — Galaxy sample"); sb.AppendLine(r.SA);
        S(sb, "Section B — Mass reconstruction"); sb.AppendLine(r.SB);
        S(sb, "Section C — RAR fits"); sb.AppendLine(r.SC);
        S(sb, "Section D — g† measurements"); sb.AppendLine(r.SD);
        S(sb, "Section E — TQM vs MOND comparison"); sb.AppendLine(r.SE);
        S(sb, "Section F — Systematic uncertainties"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  galaxies with g† estimate: " + r.Fits.Length);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG074_DirectGdagger_Report.txt"), sb.ToString());

        Assert.True(r.Fits.Length > 0, "no direct g† estimates produced");
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
