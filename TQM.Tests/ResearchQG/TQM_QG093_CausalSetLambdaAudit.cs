using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG093_CausalSetLambdaAudit : ResearchTestBase
{
    public TQM_QG093_CausalSetLambdaAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG093_CausalSetLambda()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-093 — Causal Set Cosmological Constant Audit");

        CausalSetLambdaReport r = NumerologyAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "LambdaScalingAudit.csv", "LambdaExponentScan.csv",
            "LambdaMonteCarlo.csv", "NumerologyComparison.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Dimensions & derivation"); sb.AppendLine(r.SA);
        S(sb, "Section B — Uncertainty propagation"); sb.AppendLine(r.SB);
        S(sb, "Section C — Exponent scan"); sb.AppendLine(r.SC);
        S(sb, "Section D — Monte Carlo"); sb.AppendLine(r.SD);
        S(sb, "Section E — Hostile numerology audit"); sb.AppendLine(r.SE);
        S(sb, "Section F — Observational consequences"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  α = Λ_obs/Λ_pred = {r.Alpha:F2}   (O(1) ⇒ no tuning)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG093_CausalSetLambda_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "LambdaExponentScan.csv")));
        // α = O(1): within [0.1, 10].
        Assert.InRange(r.Alpha, 0.1, 10.0);
        // Exponent −1/2 gives the smallest |log α| (the most natural, least-tuned).
        double minAbsLogAlpha = r.ExponentRows.Min(x => Math.Abs(Math.Log10(x.Alpha)));
        var best = r.ExponentRows.First(x => Math.Abs(Math.Log10(x.Alpha)) == minAbsLogAlpha);
        Assert.Equal(-0.5, best.Exponent, 2);
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
