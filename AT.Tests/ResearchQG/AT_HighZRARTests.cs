using System.Globalization;
using System.Text;
using AT.Core.FitsAnalysis;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_HighZRARTests : ResearchTestBase
{
    public AT_HighZRARTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void HighZ_RARPilotSample()
    {
        FitsDataGate.SkipUnlessFitsData();
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data", "FitsData");
        Assert.True(Directory.Exists(dataDir), $"FITS data directory not found: {dataDir}");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        // Regenerate the kinematic candidate catalog (source of the Top-20 sample).
        var cat = KinematicCatalogAnalyzer.Run(dataDir, outDir);
        Assert.True(File.Exists(cat.Top20CsvPath), "Top-20 catalog CSV missing");

        var sb = new StringBuilder();
        PrintHeader("High-z RAR Pilot Sample — QG-070 (g†(z) = c·H(z)/2π)");

        HighZRarReport r = HighZRarAnalyzer.Run(dataDir, cat.Top20CsvPath, outDir);

        S(sb, "Section A — Acceptance statistics"); sb.AppendLine(r.SA);
        S(sb, "Section B — Best 10 galaxies"); sb.AppendLine(r.SB);
        S(sb, "Section C — Kinematic quality assessment"); sb.AppendLine(r.SC);
        S(sb, "Section D — Readiness for RAR analysis"); sb.AppendLine(r.SD);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  catalog : " + r.CsvPath);
        sb.AppendLine("  accepted galaxies : " + r.Accepted.Length);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "HighZ_RARPilot_Report.txt"), sb.ToString());

        Assert.True(File.Exists(r.CsvPath), "HighZ_RotationCatalog.csv not written");
        Assert.True(r.Accepted.Length > 0, "no galaxies passed acceptance cuts");
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
