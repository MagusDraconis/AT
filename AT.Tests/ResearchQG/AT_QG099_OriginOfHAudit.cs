using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG099_OriginOfHAudit : ResearchTestBase
{
    public AT_QG099_OriginOfHAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG099_OriginOfH()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-099 — Origin Of H Audit");

        OriginOfHReport r = OriginOfHAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "OriginOfH.csv", "HDependencyGraph.csv", "DerivedVsInputH.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Catalog of H origins"); sb.AppendLine(r.SA);
        S(sb, "Section B — Can H be calculated?"); sb.AppendLine(r.SB);
        S(sb, "Section C — Ranking"); sb.AppendLine(r.SC);
        S(sb, "Section D — Dependency graph"); sb.AppendLine(r.SD);
        S(sb, "Section E — Final verdict"); sb.AppendLine(r.SE);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Planck hierarchy: {OriginOfHModel.PlanckHierarchy:F0} decades   " +
                      $"H from Λ: {OriginOfHModel.HRateFromLambdaRatio:F1}× (circular)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG099_OriginOfH_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "OriginOfH.csv")));
        // The Planck rate is astronomically larger than H0.
        Assert.True(OriginOfHModel.PlanckHierarchy > 40.0);
        // H from Λ is circular (Λ ~ H²/c² is H-dependent).
        Assert.True(OriginOfHModel.Origins().First(o => o.Model == "de Sitter").Circular);
        Assert.True(OriginOfHModel.Origins().First(o => o.Model == "Causal set").Circular);
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
