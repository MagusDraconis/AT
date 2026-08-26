using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG079_DetectabilityLimitAudit : ResearchTestBase
{
    public AT_QG079_DetectabilityLimitAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG079_DetectabilityLimit()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string largeSample = Path.Combine(dataDir, "derived", "HighZ_RAR_LargeSample.csv");
        Assert.True(File.Exists(largeSample));

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-079 — Detectability Limit Audit");

        DetectabilityReport r = DetectabilityLimitAnalyzer.Run(largeSample, outDir);

        S(sb, "Section A — AT signal amplitude"); sb.AppendLine(r.SA);
        S(sb, "Section B — Signal vs noise"); sb.AppendLine(r.SB);
        S(sb, "Section C — Detection thresholds"); sb.AppendLine(r.SC);
        S(sb, "Section D — Required precision"); sb.AppendLine(r.SD);
        S(sb, "Section E — Facility forecast"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  signal leverage: {r.StdDelta:F3} dex   observed S/N: {r.SnrObs:F2}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG079_DetectabilityLimit_Report.txt"), sb.ToString());

        Assert.True(r.Thresholds.Length > 0);
        Assert.True(File.Exists(Path.Combine(outDir, "SignalBudget.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "DetectabilityThresholds.csv")));
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
