using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG082_BeyondConformalTimeAudit : ResearchTestBase
{
    public TQM_QG082_BeyondConformalTimeAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG082_BeyondConformalTime()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-082 — Beyond Conformal Time Audit");

        BeyondConformalReport r = BeyondConformalTimeAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "ClockFamilies.csv", "ViableTimeDynamics.csv", "SN_TimeDilation_Constraints.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Core relation"); sb.AppendLine(r.SA);
        S(sb, "Section B — Candidate clock families"); sb.AppendLine(r.SB);
        S(sb, "Section C — SN Ia time-dilation constraints"); sb.AppendLine(r.SC);
        S(sb, "Section D — Viable time dynamics"); sb.AppendLine(r.SD);
        S(sb, "Section E — g† factor for survivors"); sb.AppendLine(r.SE);
        S(sb, "Section F — Distinct predictions"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  families tested: {r.Rows.Length}   viable (all reduce to γ=a): {r.Viable.Length}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG082_BeyondConformalTime_Report.txt"), sb.ToString());

        Assert.True(r.Rows.Length > 0);
        Assert.True(r.Viable.Length > 0);
        // Every surviving family must reduce to γ = a (g† factor f = 1).
        Assert.All(r.Viable, v => Assert.Equal(1.0, v.GdaggerFactor, 2));
        Assert.True(File.Exists(Path.Combine(outDir, "ClockFamilies.csv")));
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
