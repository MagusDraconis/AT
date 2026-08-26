using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG098_CosmologicalRateEmergenceAudit : ResearchTestBase
{
    public AT_QG098_CosmologicalRateEmergenceAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG098_CosmologicalRateEmergence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-098 — Cosmological Rate Emergence Audit");

        CosmologicalRateEmergenceReport r = CosmologicalRateEmergenceAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "EmergentRateModels.csv", "LambdaRateConnections.csv",
            "AccelerationRateConnections.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Λ and a₀ from N (no FLRW)"); sb.AppendLine(r.SA);
        S(sb, "Section B — Does R emerge from N?"); sb.AppendLine(r.SB);
        S(sb, "Section C — Natural scales from R"); sb.AppendLine(r.SC);
        S(sb, "Section D — Hostile audit (independent)"); sb.AppendLine(r.SD);
        S(sb, "Section E — Final verdict"); sb.AppendLine(r.SE);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  N = {r.N:E2}   Λ_pred = {r.LambdaPred:E2}   a₀_pred = {r.A0Pred:E2}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG098_CosmologicalRateEmergence_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "EmergentRateModels.csv")));
        // H is NOT emergent from N (it is the input).
        Assert.False(EmergentRateModel.HEmergesFromN);
        // Λ and a₀ are recovered at O(1)/order-of-magnitude.
        double lambdaRatio = r.LambdaPred / EmergentRateModel.Lambda;
        Assert.InRange(lambdaRatio, 0.1, 10.0);
        double a0Ratio = r.A0Pred / EmergentRateModel.A0;
        Assert.InRange(a0Ratio, 1.0, 10.0); // cH is ~5.5× a₀ (order of magnitude)
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
