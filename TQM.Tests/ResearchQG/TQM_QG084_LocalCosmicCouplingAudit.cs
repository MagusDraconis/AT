using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG084_LocalCosmicCouplingAudit : ResearchTestBase
{
    public TQM_QG084_LocalCosmicCouplingAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG084_LocalCosmicCoupling()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-084 — Local–Cosmic Coupling Audit");

        GdaggerOriginReport r = GdaggerOriginAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "NaturalAccelerationScales.csv", "CouplingModelComparison.csv",
            "CoincidenceProbability.csv", "LocalCosmicMechanisms.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Natural acceleration scales"); sb.AppendLine(r.SA);
        S(sb, "Section B — Coincidence probability"); sb.AppendLine(r.SB);
        S(sb, "Section C — Mechanism evaluation"); sb.AppendLine(r.SC);
        S(sb, "Section D — The 2π discriminator"); sb.AppendLine(r.SD);
        S(sb, "Section E — Ranking"); sb.AppendLine(r.SE);
        S(sb, "Section F — Distinguishing observables"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  g† = cH0/2π = {LocalCosmicCoupling.Gdagger:E2} m/s²   a0(MOND) = {LocalCosmicCoupling.A0_Mond:E2} m/s²");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG084_LocalCosmicCoupling_Report.txt"), sb.ToString());

        Assert.True(r.Mechanisms.Length >= 5);
        Assert.True(File.Exists(Path.Combine(outDir, "NaturalAccelerationScales.csv")));

        // Mach/boundary/causal must be excluded (5.4× too large, no 2π).
        Assert.All(r.Mechanisms.Where(m => !m.HasExactTwoPi && !double.IsNaN(m.PredictedGdagger)),
            m => Assert.True(m.RatioToA0 > 4.0));
        // Information + time-scale must have the exact 2π and match a0.
        Assert.All(r.Mechanisms.Where(m => m.HasExactTwoPi),
            m => Assert.InRange(m.RatioToA0, 0.7, 1.3));
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
