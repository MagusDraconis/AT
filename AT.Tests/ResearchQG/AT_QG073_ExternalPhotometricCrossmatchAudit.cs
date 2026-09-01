using System.Globalization;
using System.Text;
using AT.Core.FitsAnalysis;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG073_ExternalPhotometricCrossmatchAudit : ResearchTestBase
{
    public AT_QG073_ExternalPhotometricCrossmatchAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG073_ExternalPhotometricCrossmatch()
    {
        FitsDataGate.SkipUnlessFitsData();
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        string cosmosCsv = Path.Combine(dataDir, "COSMOS2015_KMOS3D_field.csv");
        Assert.True(Directory.Exists(fitsDir));
        Assert.True(File.Exists(cosmosCsv),
            "COSMOS2015_KMOS3D_field.csv not found. Download it first: " +
            "TAP query on VizieR J/ApJS/224/24/cosmos2015 over RA 150.02-150.23, DEC 2.16-2.50.");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        // Regenerate the kinematic catalog to obtain H-alpha redshifts.
        var cat = KinematicCatalogAnalyzer.Run(fitsDir, outDir);
        string kmosCatalog = Path.Combine(outDir, "KMOS3D_KinematicCatalog.csv");
        Assert.True(File.Exists(kmosCatalog));

        var sb = new StringBuilder();
        PrintHeader("QG-073 — External Photometric Crossmatch Audit");

        PhotometricCrossmatchReport r = PhotometricCrossmatchAnalyzer.Run(fitsDir, cosmosCsv, kmosCatalog, outDir);

        S(sb, "Section A — Crossmatch statistics"); sb.AppendLine(r.SA);
        S(sb, "Section B — Mass-recovery statistics"); sb.AppendLine(r.SB);
        S(sb, "Section C — RAR-ready sample"); sb.AppendLine(r.SC);
        S(sb, "Section D — Expected precision on g†(z)"); sb.AppendLine(r.SD);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  CLASSIFICATION: " + r.ClassificationClass);
        sb.AppendLine("  KMOS3D_MassCatalog.csv: " + r.CsvPath);
        sb.AppendLine("  matches: " + r.Matches.Count(m => !double.IsNaN(m.MassMed)));
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG073_PhotometricCrossmatch_Report.txt"), sb.ToString());

        Assert.True(File.Exists(r.CsvPath), "KMOS3D_MassCatalog.csv not written");
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
        return Path.Combine(@"D:\Coding\Test\AT", combined);
    }
}
