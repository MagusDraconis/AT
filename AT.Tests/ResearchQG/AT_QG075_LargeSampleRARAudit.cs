using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG075_LargeSampleRARAudit : ResearchTestBase
{
    public AT_QG075_LargeSampleRARAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG075_LargeSampleRAR()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        Assert.True(Directory.Exists(fitsDir));

        string kinematicCatalog = Path.Combine(dataDir, "derived", "KMOS3D_KinematicCatalog.csv");
        string massCatalog = Path.Combine(dataDir, "derived", "KMOS3D_MassCatalog.csv");
        Assert.True(File.Exists(kinematicCatalog), "KMOS3D_KinematicCatalog.csv not found");
        Assert.True(File.Exists(massCatalog), "KMOS3D_MassCatalog.csv not found (run QG-073)");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-075 — Large-Sample High-z RAR Audit");

        LargeSampleReport r = LargeSampleGdaggerAnalyzer.Run(massCatalog, kinematicCatalog, fitsDir, outDir);

        S(sb, "Section A — Galaxy sample"); sb.AppendLine(r.SA);
        S(sb, "Section B — Kinematic extraction (usable + marginal)"); sb.AppendLine(r.SB);
        S(sb, "Section C — g† distribution"); sb.AppendLine(r.SC);
        S(sb, "Section D — Redshift-binned g† evolution"); sb.AppendLine(r.SD);
        S(sb, "Section E — AT vs MOND comparison"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  galaxies with a g† estimate: " + r.Fits.Length);
        sb.AppendLine("  usable: " + r.Rows.Count(x => x.Class == "usable") +
                      "   marginal: " + r.Rows.Count(x => x.Class == "marginal"));
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG075_LargeSampleRAR_Report.txt"), sb.ToString());

        Assert.True(r.Fits.Length > 0, "no large-sample g† estimates produced");
        Assert.True(File.Exists(r.LargeSampleCsvPath));
        Assert.True(File.Exists(Path.Combine(outDir, "gdagger_vs_z.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "AT_vs_MOND_Statistics.csv")));
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
