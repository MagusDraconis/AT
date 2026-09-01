using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG090_OriginOfChangeAudit : ResearchTestBase
{
    public AT_QG090_OriginOfChangeAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG090_OriginOfChange()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-090 — Origin Of Change Audit");

        FundamentalChangeReport r = FundamentalChangeAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "OriginOfChangeModels.csv", "ChangeHierarchy.csv",
            "TimeVsChangeAnalysis.csv", "FundamentalChangeRanking.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Primitive realities"); sb.AppendLine(r.SA);
        S(sb, "Section B — Time vs change"); sb.AppendLine(r.SB);
        S(sb, "Section C — Change hierarchy"); sb.AppendLine(r.SC);
        S(sb, "Section D — Causal-set Λ prediction"); sb.AppendLine(r.SD);
        S(sb, "Section E — a₀ as minimum rate of change"); sb.AppendLine(r.SE);
        S(sb, "Section F — Hostile audit"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  predicted Λ·l_P² = {ChangeAnalyzer.PredictedLambdaPlanck():E2}   " +
                      $"observed = {ChangeAnalyzer.ObservedLambdaPlanck():E2}   ratio = {ChangeAnalyzer.PredictionRatio():F2}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG090_OriginOfChange_Report.txt"), sb.ToString());

        Assert.True(r.Realities.Length >= 5);
        Assert.True(File.Exists(Path.Combine(outDir, "OriginOfChangeModels.csv")));
        // The causal-set Λ prediction must be within an order of magnitude of observation.
        double ratio = ChangeAnalyzer.PredictionRatio();
        Assert.InRange(ratio, 0.1, 10.0);
        // Time is not fundamental (emergent in quantum gravity).
        Assert.False(EmergentTimeModel.TimeIsFundamental);
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
