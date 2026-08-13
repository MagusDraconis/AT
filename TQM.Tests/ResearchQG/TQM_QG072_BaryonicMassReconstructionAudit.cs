using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG072_BaryonicMassReconstructionAudit : ResearchTestBase
{
    public TQM_QG072_BaryonicMassReconstructionAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG072_BaryonicMassAudit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        Assert.True(Directory.Exists(dataDir));
        Assert.True(Directory.Exists(fitsDir));

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-072 — High-z Baryonic Mass Reconstruction Audit");

        DataInventoryReport r = DataInventoryAnalyzer.Run(dataDir, fitsDir, outDir);

        S(sb, "Section A — File inventory"); sb.AppendLine(r.SA);
        S(sb, "Section B — Candidate mass catalogs"); sb.AppendLine(r.SB);
        S(sb, "Section C — Cross-match results"); sb.AppendLine(r.SC);
        S(sb, "Section D — Independent mass availability"); sb.AppendLine(r.SD);
        S(sb, "Section E — RAR readiness assessment"); sb.AppendLine(r.SE);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  CLASSIFICATION: " + r.AssessmentClass);
        sb.AppendLine("  FileInventory.csv: " + r.InventoryCsvPath);
        sb.AppendLine("  cross-matches: " + r.Matches.Length);
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG072_BaryonicMass_Report.txt"), sb.ToString());

        Assert.True(File.Exists(r.InventoryCsvPath), "FileInventory.csv not written");
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
