using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG076_GasMassSystematicsAudit : ResearchTestBase
{
    public AT_QG076_GasMassSystematicsAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG076_GasMassSystematics()
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
        PrintHeader("QG-076 — Gas Mass Systematics Audit");

        GasMassSystematicsReport r = GasMassSystematicsAnalyzer.Run(fitsDir, kinematicCatalog, massCatalog, largeSample, outDir);

        S(sb, "Section A — Error budget audit"); sb.AppendLine(r.SA);
        S(sb, "Section B — Gas fraction + depletion-time audit"); sb.AppendLine(r.SB);
        S(sb, "Section C — σ(g†) vs σ(Mgas) sensitivity"); sb.AppendLine(r.SC);
        S(sb, "Section D — Required gas precision"); sb.AppendLine(r.SD);
        S(sb, "Section E — Synthetic recovery (10,000 realizations)"); sb.AppendLine(r.SE);
        S(sb, "Section F — AT vs MOND discrimination"); sb.AppendLine(r.SF);
        S(sb, "Section G — Hostile audit"); sb.AppendLine(r.SG);
        S(sb, "Section H — Final verdict"); sb.AppendLine(r.SH);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine("  constrained galaxies in audit: " + r.Galaxies.Count(g => g.Constrained));
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG076_GasMassSystematics_Report.txt"), sb.ToString());

        Assert.True(r.Galaxies.Length > 0, "no systematics built");
        Assert.True(r.Sensitivity.Length > 0, "no sensitivity curve");
        Assert.True(File.Exists(Path.Combine(outDir, "GasMassErrorBudget.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "MonteCarloRecovery.csv")));
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
