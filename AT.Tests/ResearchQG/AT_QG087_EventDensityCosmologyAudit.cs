using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG087_EventDensityCosmologyAudit : ResearchTestBase
{
    public AT_QG087_EventDensityCosmologyAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG087_EventDensityCosmology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-087 — Event Density Cosmology Audit");

        EventGrowthReport r = EventGrowthAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "EventDensityModels.csv", "EventCosmologyComparison.csv", "A0_FromEvents.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Event formalism"); sb.AppendLine(r.SA);
        S(sb, "Section B — Event-growth models"); sb.AppendLine(r.SB);
        S(sb, "Section C — a₀ from event rate"); sb.AppendLine(r.SC);
        S(sb, "Section D — Redshift from events"); sb.AppendLine(r.SD);
        S(sb, "Section E — Links to Causal Set / entropic"); sb.AppendLine(r.SE);
        S(sb, "Section F — Hostile audit"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  event models: {r.Models.Length}   only N=a reproduces H(z)");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG087_EventDensityCosmology_Report.txt"), sb.ToString());

        Assert.True(r.Models.Length >= 5);
        Assert.True(File.Exists(Path.Combine(outDir, "EventCosmologyComparison.csv")));

        // N = a must match H_ΛCDM exactly; the simple models must deviate at z=2.
        var flrw = r.Comparison.First(c => c.Model.StartsWith("N = a") && Math.Abs(c.Z - 2.0) < 1e-9);
        Assert.Equal(1.0, flrw.Ratio, 6);
        var coasting = r.Comparison.First(c => c.Model.StartsWith("N ∝ t ") && Math.Abs(c.Z - 2.0) < 1e-9);
        Assert.True(Math.Abs(coasting.Ratio - 1.0) > 0.01);
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
