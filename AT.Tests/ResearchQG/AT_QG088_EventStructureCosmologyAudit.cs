using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG088_EventStructureCosmologyAudit : ResearchTestBase
{
    public AT_QG088_EventStructureCosmologyAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG088_EventStructureCosmology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-088 — Event Structure Cosmology Audit");

        StructuralReport r = StructuralEvolutionAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "EventStructureModels.csv", "StructuralEvolutionRates.csv",
            "EmergentCosmologyComparison.csv", "A0_FromStructure.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Structural variables"); sb.AppendLine(r.SA);
        S(sb, "Section B — Toy universes"); sb.AppendLine(r.SB);
        S(sb, "Section C — Structural evolution rates"); sb.AppendLine(r.SC);
        S(sb, "Section D — Redshift from structure"); sb.AppendLine(r.SD);
        S(sb, "Section E — a₀ from structure"); sb.AppendLine(r.SE);
        S(sb, "Section F — Links to emergent space-time"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  structural variables: {r.Variables.Length}   only S=a gives H_S=H");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG088_EventStructureCosmology_Report.txt"), sb.ToString());

        Assert.True(r.Variables.Length >= 6);
        Assert.True(File.Exists(Path.Combine(outDir, "StructuralEvolutionRates.csv")));
        // No structural variable (other than S=a) gives p=1.
        Assert.Single(r.Variables.Where(v => Math.Abs(v.PowerP - 1.0) < 1e-3));
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
