using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG081_ModelDependenceAudit : ResearchTestBase
{
    public AT_QG081_ModelDependenceAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG081_ModelDependence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-081 — Model Dependence Audit");

        ModelDependenceReport r = ModelDependenceAnalyzer.Run(outDir);

        // Persist the two CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "Observables_vs_Inferences.csv", "FLRW_Assumption_Map.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Observation vs inference"); sb.AppendLine(r.SA);
        S(sb, "Section B — Probe-by-probe audit"); sb.AppendLine(r.SB);
        S(sb, "Section C — Hidden FLRW assumptions"); sb.AppendLine(r.SC);
        S(sb, "Section D — Dependency graph"); sb.AppendLine(r.SD);
        S(sb, "Section E — Static-space + evolving-time"); sb.AppendLine(r.SE);
        S(sb, "Section F — Invariant vs model-dependent"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  observables: {r.Observables.Length}   inferences: {r.Inferences.Length}   assumptions: {r.Assumptions.Length}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG081_ModelDependence_Report.txt"), sb.ToString());

        Assert.True(r.Observables.Length > 0);
        Assert.True(File.Exists(Path.Combine(outDir, "Observables_vs_Inferences.csv")));
        Assert.True(File.Exists(Path.Combine(outDir, "FLRW_Assumption_Map.csv")));
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
