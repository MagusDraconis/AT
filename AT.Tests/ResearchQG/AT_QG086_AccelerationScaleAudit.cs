using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG086_AccelerationScaleAudit : ResearchTestBase
{
    public AT_QG086_AccelerationScaleAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG086_AccelerationScale()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-086 — Fundamental Acceleration Scale Audit");

        AccelerationScaleReport r = AccelerationScaleAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "AccelerationLandscape.csv", "AccelerationOriginRanking.csv",
            "FundamentalScaleHierarchy.csv", "AccelerationCoincidenceAnalysis.csv", "UniquePredictions.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Acceleration landscape"); sb.AppendLine(r.SA);
        S(sb, "Section B — Dimensional reduction"); sb.AppendLine(r.SB);
        S(sb, "Section C — Origin hypotheses"); sb.AppendLine(r.SC);
        S(sb, "Section D — Unique predictions"); sb.AppendLine(r.SD);
        S(sb, "Section E — Coincidence hypothesis"); sb.AppendLine(r.SE);
        S(sb, "Section F — Ranking"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  a_P = {FundamentalHierarchy.PlanckAcceleration:E1} m/s²   a0 = 1.2E-10   ratio ≈ 10^-61");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG086_AccelerationScale_Report.txt"), sb.ToString());

        Assert.True(r.Landscape.Length >= 8);
        Assert.True(File.Exists(Path.Combine(outDir, "AccelerationLandscape.csv")));
        // a₀ cannot be formed from fundamental constants alone (needs cosmological input).
        Assert.True(FundamentalHierarchy.RequiresCosmologicalInput());
        // The cosmological cH origin should outrank coincidence.
        Assert.True(r.Ranking[0].Name.StartsWith("Cosmological"));
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
