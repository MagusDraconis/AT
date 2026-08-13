using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG078_GoldSampleKinematicPurityAudit : ResearchTestBase
{
    public TQM_QG078_GoldSampleKinematicPurityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG078_GoldSample()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string dataDir = LocateDir("Data");
        string fitsDir = Path.Combine(dataDir, "FitsData");
        string derived = Path.Combine(dataDir, "derived");
        string kinematicCatalog = Path.Combine(derived, "KMOS3D_KinematicCatalog.csv");
        string massCatalog = Path.Combine(derived, "KMOS3D_MassCatalog.csv");
        string largeSample = Path.Combine(derived, "HighZ_RAR_LargeSample.csv");
        Assert.True(Directory.Exists(fitsDir));
        Assert.True(File.Exists(kinematicCatalog));
        Assert.True(File.Exists(massCatalog));
        Assert.True(File.Exists(largeSample));

        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-078 — Gold Sample Kinematic Purity Audit");

        GoldSampleReport r = GoldSamplePurityAnalyzer.Run(fitsDir, kinematicCatalog, massCatalog, largeSample, outDir);

        S(sb, "Section A — Purity metric + ranking"); sb.AppendLine(r.SA);
        S(sb, "Section B — Scatter vs purity (nested samples)"); sb.AppendLine(r.SB);
        S(sb, "Section C — Intrinsic scatter vs TQM signal"); sb.AppendLine(r.SC);
        S(sb, "Section D — Gold-sample g†(z) refit"); sb.AppendLine(r.SD);
        S(sb, "Section E — MOND vs TQM (gold)"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  ranked: {r.Ranked.Length}   gold: {r.Gold.Length}   " +
                      $"intrinsic scatter: {r.IntrinsicScatterDex:F2} dex");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG078_GoldSample_Report.txt"), sb.ToString());

        Assert.True(r.Ranked.Length > 0, "no galaxies ranked");
        Assert.True(r.Gold.Length > 0, "no gold sample");
        Assert.True(File.Exists(Path.Combine(outDir, "GoldSampleCatalog.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "ScatterVsPurity.csv")));
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
