using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;

public class AT_QG083_ClockNonUniversalityAudit : ResearchTestBase
{
    public AT_QG083_ClockNonUniversalityAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG083_ClockNonUniversality()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-083 — Cosmic Clock Non-Universality Audit");

        ClockNonUniversalityReport r = ClockConsistencyAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "ClockFamilyConstraints.csv", "ClockConsistencyMatrix.csv",
            "AllowedClockDrift.csv", "Gdagger_ClockFamilySensitivity.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Clock families"); sb.AppendLine(r.SA);
        S(sb, "Section B — Experimental constraints"); sb.AppendLine(r.SB);
        S(sb, "Section C — Consistency matrix"); sb.AppendLine(r.SC);
        S(sb, "Section D — Max allowed drift"); sb.AppendLine(r.SD);
        S(sb, "Section E — g† per clock family"); sb.AppendLine(r.SE);
        S(sb, "Section F — Amplifying observables"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  families: {r.Families.Length}   max g† correction: {r.Sensitivity.Max(s => s.DEpsilonDLnA):E1}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG083_ClockNonUniversality_Report.txt"), sb.ToString());

        Assert.True(r.Families.Length >= 4);
        Assert.True(r.Sensitivity.Length > 0);
        Assert.True(File.Exists(Path.Combine(outDir, "ClockConsistencyMatrix.csv")));

        // g† must be insensitive: max correction factor within 1% of unity.
        Assert.All(r.Sensitivity, s => Assert.InRange(s.CorrectionFactor, 0.995, 1.005));
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
