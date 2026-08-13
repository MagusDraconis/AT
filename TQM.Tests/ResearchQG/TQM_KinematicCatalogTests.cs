using System.Globalization;
using System.Text;
using TQM.Core.FitsAnalysis;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_KinematicCatalogTests : ResearchTestBase
{
    public TQM_KinematicCatalogTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Kmos3D_KinematicCandidateCatalog()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data", "FitsData");
        Assert.True(Directory.Exists(dataDir), $"FITS data directory not found: {dataDir}");

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");

        var sb = new StringBuilder();
        PrintHeader("KMOS3D Kinematic Candidate Catalog — QG-070 RAR evolution");

        CatalogReport r = KinematicCatalogAnalyzer.Run(dataDir, outDir);

        S(sb, "Section A — Catalog summary"); sb.AppendLine(r.Summary);
        S(sb, "Section B — Top 20 kinematic candidates"); sb.AppendLine(r.Top20Table);
        S(sb, "Section C — Outputs");
        sb.AppendLine("  full catalog : " + r.CsvPath);
        sb.AppendLine("  top 20       : " + r.Top20CsvPath);
        sb.AppendLine();

        // Classification histogram.
        var hist = r.Details
            .GroupBy(d => d.Classification.Substring(0, 1))
            .OrderBy(g => g.Key)
            .Select(g => $"{g.Key}: {g.Count()}");
        sb.AppendLine("  Classification histogram: " + string.Join(", ", hist));
        sb.AppendLine("  Total entries: " + r.Entries.Length);

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "KMOS3D_KinematicCatalog_Report.txt"), sb.ToString());

        Assert.True(File.Exists(r.CsvPath), "catalog CSV not written");
        Assert.True(File.Exists(r.Top20CsvPath), "Top-20 CSV not written");
        Assert.True(r.Entries.Length > 0, "no entries produced");
        Assert.Contains(r.Details, d => d.Classification.StartsWith("C") || d.Classification.StartsWith("D"));
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
