using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG100_WhyThisHAudit : ResearchTestBase
{
    public TQM_QG100_WhyThisHAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG100_WhyThisH()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-100 — Why This H Audit");

        WhyThisHReport r = WhyThisHAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "HSelectionLandscape.csv", "AnthropicWindow.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Selection landscape"); sb.AppendLine(r.SA);
        S(sb, "Section B — Anthropic window"); sb.AppendLine(r.SB);
        S(sb, "Section C — Random vs selected"); sb.AppendLine(r.SC);
        S(sb, "Section D — Does Λ ~ H² help?"); sb.AppendLine(r.SD);
        S(sb, "Section E — Final verdict"); sb.AppendLine(r.SE);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  anthropic window: [{r.WindowMin:F1}, {r.WindowMax:F1}] dex   " +
                      $"P(land in window) = {r.AnthropicProbability:P0}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG100_WhyThisH_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "HSelectionLandscape.csv")));
        // Observed H (log H/H0 = 0) must lie inside the anthropic window.
        Assert.InRange(0.0, r.WindowMin, r.WindowMax);
        // H is NOT fine-tuned: the window is broad (≳ 2 decades), probability ≳ 10%.
        Assert.True((r.WindowMax - r.WindowMin) > 2.0);
        Assert.True(r.AnthropicProbability > 0.10);
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
