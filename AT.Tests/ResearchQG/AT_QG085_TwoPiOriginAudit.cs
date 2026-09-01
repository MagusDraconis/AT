using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG085_TwoPiOriginAudit : ResearchTestBase
{
    public AT_QG085_TwoPiOriginAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG085_TwoPiOrigin()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-085 — The 2π Origin Audit");

        TwoPiOriginReport r = TwoPiOriginAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "Fundamental2PiRelations.csv", "TwoPiMechanisms.csv",
            "TwoPiOriginRanking.csv", "CoincidenceAnalysis.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Catalog of 2π relations"); sb.AppendLine(r.SA);
        S(sb, "Section B — Mechanism evaluation"); sb.AppendLine(r.SB);
        S(sb, "Section C — Is 1/(2π) actually selected?"); sb.AppendLine(r.SC);
        S(sb, "Section D — Why cH fails"); sb.AppendLine(r.SD);
        S(sb, "Section E — Coincidence model"); sb.AppendLine(r.SE);
        S(sb, "Section F — Ranking"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  a0/(cH) = {CoincidenceAnalyzer.A0OverCH():F3}   vs  1/(2π) = {1.0 / (2.0 * Math.PI):F3}   vs  1/6 = {1.0 / 6.0:F3}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG085_TwoPiOrigin_Report.txt"), sb.ToString());

        Assert.True(r.Relations.Length >= 6);
        Assert.True(File.Exists(Path.Combine(outDir, "Fundamental2PiRelations.csv")));

        // The horizon route (no retained 2π) must be excluded (5.4× too large).
        Assert.All(r.Mechanisms.Where(m => !m.RetainsTwoPi && !double.IsNaN(m.PredictedGdagger) && m.Name.StartsWith("Horizon")),
            m => Assert.True(m.RatioToA0 > 4.0));
        // 1/(2π) is NOT the best match to a0/(cH).
        double best = r.NiceNumbers.OrderBy(n => n.LogMismatch).First().LogMismatch;
        double twoPi = r.NiceNumbers.First(n => n.Candidate == "1/(2π)").LogMismatch;
        Assert.True(best < twoPi, "1/(2π) should not be the best nice-number match");
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
