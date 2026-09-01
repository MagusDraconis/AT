using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG091_CausalUniverseAudit : ResearchTestBase
{
    public AT_QG091_CausalUniverseAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG091_CausalUniverse()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-091 — Is Causality More Fundamental Than Time Audit");

        CausalOrderReport r = CausalOrderAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "CausalHierarchy.csv", "TimeFromCausality.csv",
            "GeometryFromCausality.csv", "FundamentalityRanking.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Pure causal framework"); sb.AppendLine(r.SA);
        S(sb, "Section B — Time from causality"); sb.AppendLine(r.SB);
        S(sb, "Section C — Geometry from causality"); sb.AppendLine(r.SC);
        S(sb, "Section D — Cosmology from causality"); sb.AppendLine(r.SD);
        S(sb, "Section E — a₀ from causality"); sb.AppendLine(r.SE);
        S(sb, "Section F — Hostile audit (precedence)"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  recovered dimension = {r.RecoveredDimension:F3} (input 4)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG091_CausalUniverse_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "CausalHierarchy.csv")));
        // Dimension recovery from N ∝ D^d must return d = 4.
        Assert.Equal(4.0, r.RecoveredDimension, 3);
        // a₀ = cH (cH class).
        Assert.InRange(CausalRateModel.A0FromCausalRate(), 6e-10, 7e-10);
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
