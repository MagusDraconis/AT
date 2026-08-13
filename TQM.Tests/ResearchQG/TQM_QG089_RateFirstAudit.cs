using System.Globalization;
using System.Text;
using TQM.Core.ResearchQG;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;

public class TQM_QG089_RateFirstAudit : ResearchTestBase
{
    public TQM_QG089_RateFirstAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG089_RateFirst()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-089 — Cosmic Rate First Audit");

        RateFirstReport r = RateFirstAnalyzer.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "RateFirstModels.csv", "EmergentTimeFromRate.csv",
            "EmergentExpansionFromRate.csv", "AccelerationFromRate.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Rate-first formalism"); sb.AppendLine(r.SA);
        S(sb, "Section B — Information content of H vs a"); sb.AppendLine(r.SB);
        S(sb, "Section C — Emergent time"); sb.AppendLine(r.SC);
        S(sb, "Section D — Redshift from rate"); sb.AppendLine(r.SD);
        S(sb, "Section E — a₀ from rate"); sb.AppendLine(r.SE);
        S(sb, "Section F — Links to relational time"); sb.AppendLine(r.SF);
        S(sb, "Section G — Final verdict"); sb.AppendLine(r.SG);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  H0 = {CosmicRateModel.H0PerS:E2} s⁻¹   a₀ = cH = {RateDerivedAcceleration.CH():E2} m/s²");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG089_RateFirst_Report.txt"), sb.ToString());

        Assert.True(r.Points.Length >= 4);
        Assert.True(File.Exists(Path.Combine(outDir, "RateFirstModels.csv")));
        // Rate-first redshift must be exactly equivalent to FLRW.
        Assert.True(RateDrivenRedshift.EquivalentToFlrw(2.0));
        // Time reconstruction is exact (tautological).
        Assert.True(EmergentTime.ReconstructionIsExact(2.0));
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
