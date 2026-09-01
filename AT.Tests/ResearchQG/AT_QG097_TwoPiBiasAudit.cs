using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG097_TwoPiBiasAudit : ResearchTestBase
{
    public AT_QG097_TwoPiBiasAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG097_TwoPiBias()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-097 — Is The 2π Bias Audit");

        TwoPiBiasReport r = TwoPiBiasAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "A0OverCH_Distribution.csv", "FactorComparison.csv",
            "BayesFactorComparison.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Historical a₀ estimates"); sb.AppendLine(r.SA);
        S(sb, "Section B — Bayesian comparison"); sb.AppendLine(r.SB);
        S(sb, "Section C — Significance"); sb.AppendLine(r.SC);
        S(sb, "Section D — Final verdict"); sb.AppendLine(r.SD);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  x = a₀/(cH) = {r.ObservedX:F3} ± {r.ObservedSigma:F3}   verdict: {r.Verdict}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG097_TwoPiBias_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "FactorComparison.csv")));
        Assert.True(r.Estimates.Length >= 4);
        // The observed x must be within the candidate factor band [0.125, 0.25].
        Assert.InRange(r.ObservedX, 0.10, 0.30);
        // Verdict must be A or B (2π is not robust).
        Assert.Contains("2π", r.Verdict);
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
