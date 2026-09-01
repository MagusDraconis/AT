using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG094_CausalDiscretenessAudit : ResearchTestBase
{
    public AT_QG094_CausalDiscretenessAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG094_CausalDiscreteness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-094 — Observable Consequences Of Causal Discreteness Audit");

        DiscretenessObservableReport r = DiscretenessObservableScanner.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "CausalDiscretenessSignals.csv", "ObservableRanking.csv",
            "LambdaFluctuationAnalysis.csv", "DiscretenessVsContinuum.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Discreteness scale & effects"); sb.AppendLine(r.SA);
        S(sb, "Section B — Signal amplitudes"); sb.AppendLine(r.SB);
        S(sb, "Section C — Detectability ranking"); sb.AppendLine(r.SC);
        S(sb, "Section D — Detection feasibility"); sb.AppendLine(r.SD);
        S(sb, "Section E — Hostile continuum audit"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  Planck suppression l_P/λ = {CausalDiscretenessModel.PropagationSuppression():E1}   " +
                      $"cosmological 1/√N = {CausalDiscretenessModel.LambdaFluctuation:E1}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG094_CausalDiscreteness_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "CausalDiscretenessSignals.csv")));
        // Every Planck-suppressed channel (except Λ itself and CMB) must be unobservable (S/N ≪ 1).
        var planckSuppressed = r.Signals.Where(s =>
            !s.Channel.StartsWith("dark") && !s.Channel.StartsWith("CMB"));
        Assert.All(planckSuppressed, s => Assert.True(s.SignalToNoise < 1.0));
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
