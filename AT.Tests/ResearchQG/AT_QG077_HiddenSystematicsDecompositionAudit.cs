using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG077_HiddenSystematicsDecompositionAudit : ResearchTestBase
{
    public AT_QG077_HiddenSystematicsDecompositionAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG077_HiddenSystematics()
    {
        FitsDataGate.SkipUnlessFitsData();
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
        PrintHeader("QG-077 — Hidden Systematics Decomposition Audit");

        HiddenSystematicsReport r = HiddenSystematicsAnalyzer.Run(fitsDir, kinematicCatalog, massCatalog, largeSample, outDir);

        S(sb, "Section A — Residual definition + sample"); sb.AppendLine(r.SA);
        S(sb, "Section B — Univariate residual correlations"); sb.AppendLine(r.SB);
        S(sb, "Section C — Hierarchical variance decomposition"); sb.AppendLine(r.SC);
        S(sb, "Section D — Dominant hidden systematic"); sb.AppendLine(r.SD);
        S(sb, "Section E — Remaining scatter vs AT signal"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  galaxies: {r.Galaxies.Length}   residual std: {r.ResidualStdDex:F2} dex   " +
                      $"explained: {r.ExplainedVarianceFraction:P0}   remaining: {r.RemainingScatterDex:F2} dex");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG077_HiddenSystematics_Report.txt"), sb.ToString());

        Assert.True(r.Galaxies.Length > 0, "no galaxies analyzed");
        Assert.True(File.Exists(Path.Combine(outDir, "ResidualCorrelationTable.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "VarianceBreakdown.csv")));
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
