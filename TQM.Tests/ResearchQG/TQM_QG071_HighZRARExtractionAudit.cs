using System.Globalization;
using System.Text;
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
        string dataDir = LocateDir("Data", "FitsData");
        Assert.True(Directory.Exists(dataDir), $"FITS data directory not found: {dataDir}");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        // Reproduce the catalog + pilot sample (source inputs).
        var cat = KinematicCatalogAnalyzer.Run(dataDir, outDir);
        Assert.True(File.Exists(cat.Top20CsvPath));
        var pilot = HighZRarAnalyzer.Run(dataDir, cat.Top20CsvPath, outDir);
        string rotationCsv = Path.Combine(outDir, "HighZ_RotationCatalog.csv");
        Assert.True(File.Exists(rotationCsv), "HighZ_RotationCatalog.csv missing");

        var sb = new StringBuilder();
        PrintHeader("QG-071 — High-z RAR Extraction Audit");

        HighZRARExtractionReport r = HighZRARExtractionAnalyzer.Run(dataDir, cat.Top20CsvPath, rotationCsv, outDir);

        S(sb, "Section A — Accepted galaxy sample"); sb.AppendLine(r.SA);
        S(sb, "Section B — Rotation-curve extraction"); sb.AppendLine(r.SB);
        S(sb, "Section C — Baryonic acceleration estimates"); sb.AppendLine(r.SC);
        S(sb, "Section D — Individual g† measurements"); sb.AppendLine(r.SD);
        S(sb, "Section E — Redshift-binned g† evolution"); sb.AppendLine(r.SE);
        S(sb, "Section F — TQM vs MOND comparison"); sb.AppendLine(r.SF);
        S(sb, "Section G — Statistical significance"); sb.AppendLine(r.SG);
        S(sb, "Section H — Falsification analysis"); sb.AppendLine(r.SH);
        S(sb, "Section I — Final verdict"); sb.AppendLine(r.SI);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  VERDICT: " + r.VerdictClass);
        sb.AppendLine("  galaxies with g† estimate: " + r.Fits.Length);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG071_HighZRAR_Report.txt"), sb.ToString());

        Assert.True(r.Fits.Length > 0, "no g† estimates produced");
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
