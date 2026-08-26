using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG096_CausalSetAccelerationAudit : ResearchTestBase
{
    public AT_QG096_CausalSetAccelerationAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG096_CausalSetAcceleration()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-096 — Can Causal Discreteness Generate g† Audit");

        CausalScaleReport r = CausalScaleAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "CausalSetAccelerationScales.csv", "GdaggerFromCausalSets.csv",
            "LambdaVsGdaggerOrigin.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Constructible acceleration scales"); sb.AppendLine(r.SA);
        S(sb, "Section B — Candidate derivations"); sb.AppendLine(r.SB);
        S(sb, "Section C — Predicted or inserted?"); sb.AppendLine(r.SC);
        S(sb, "Section D — Λ vs g† derivation"); sb.AppendLine(r.SD);
        S(sb, "Section E — Hostile audit"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  causal-depth a_eff = cH = {CausalGdaggerOrigin.AEffFromCausalDepth():E2} m/s²   " +
                      $"generates 2π? {CausalGdaggerOrigin.GeneratesTwoPi}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG096_CausalSetAcceleration_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "CausalSetAccelerationScales.csv")));
        // Causal discreteness gives cH (not the 2π).
        Assert.InRange(CausalGdaggerOrigin.AEffFromCausalDepth(), 6e-10, 7e-10);
        Assert.False(CausalGdaggerOrigin.GeneratesTwoPi);
        // The only 2π-bearing scale is g† = cH/2π.
        Assert.Single(r.Scales.Where(s => s.HasTwoPi));
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
