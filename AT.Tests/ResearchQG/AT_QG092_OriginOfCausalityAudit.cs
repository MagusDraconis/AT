using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG092_OriginOfCausalityAudit : ResearchTestBase
{
    public AT_QG092_OriginOfCausalityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG092_OriginOfCausality()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-092 — Origin Of Causality Audit");

        CausalityDependencyReport r = CausalityDependencyGraph.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "OriginOfCausalityModels.csv", "CausalityDependencyGraph.csv",
            "PrimitiveHierarchy.csv", "EmergentVsFundamentalCausality.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Irreducible axioms"); sb.AppendLine(r.SA);
        S(sb, "Section B — Deeper primitive candidates"); sb.AppendLine(r.SB);
        S(sb, "Section C — Dependency graph"); sb.AppendLine(r.SC);
        S(sb, "Section D — Universe with no causal order"); sb.AppendLine(r.SD);
        S(sb, "Section E — Consistency and causality"); sb.AppendLine(r.SE);
        S(sb, "Section F — Does Λ ~ 1/√N survive?"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  deepest primitive: {CausalityHierarchy.DeepestPrimitive}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG092_OriginOfCausality_Report.txt"), sb.ToString());

        Assert.True(r.Axioms.Length >= 4);
        Assert.True(File.Exists(Path.Combine(outDir, "OriginOfCausalityModels.csv")));
        // Causality is not derivable (all deeper candidates fail).
        Assert.All(r.Deeper, d => Assert.DoesNotContain("YES", d.CanReconstruct));
        // Without causality, no observation is meaningful.
        Assert.False(PrimitiveStructureAudit.MeaningfulWithoutCausality);
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
