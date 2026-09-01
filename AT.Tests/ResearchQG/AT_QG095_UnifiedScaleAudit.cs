using System.Globalization;
using System.Text;
using AT.Core.ResearchQG;
using AT.Tests.Shared;

namespace AT.Tests.ResearchQG;

public class AT_QG095_UnifiedScaleAudit : ResearchTestBase
{
    public AT_QG095_UnifiedScaleAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void QG095_UnifiedScale()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string outDir = Path.Combine(AppContext.BaseDirectory, "catalog_out");
        Directory.CreateDirectory(outDir);

        var sb = new StringBuilder();
        PrintHeader("QG-095 — Unified Cosmological Scale Audit");

        UnifiedScaleReport r = ScaleUnificationAudit.Run(outDir);

        // Persist CSVs to Data/derived (consistency with prior audits).
        string derivedDir = Path.Combine(LocateDir("Data"), "derived");
        Directory.CreateDirectory(derivedDir);
        foreach (var f in new[] { "UnifiedScaleRelations.csv", "LambdaGdaggerComparison.csv",
            "ScaleUnificationRanking.csv" })
            File.Copy(Path.Combine(outDir, f), Path.Combine(derivedDir, f), overwrite: true);

        S(sb, "Section A — Scales & dimensionless combinations"); sb.AppendLine(r.SA);
        S(sb, "Section B — g† linked to H or √Λ?"); sb.AppendLine(r.SB);
        S(sb, "Section C — Three hypotheses"); sb.AppendLine(r.SC);
        S(sb, "Section D — Can one derive the other?"); sb.AppendLine(r.SD);
        S(sb, "Section E — Tuning quantification"); sb.AppendLine(r.SE);
        S(sb, "Section F — Final verdict"); sb.AppendLine(r.SF);

        sb.AppendLine();
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  H²/Λc² = {UnifiedScaleAnalyzer.H2OverLambda:F2}   " +
                      $"Λ_pred/Λ_obs = {UnifiedScaleAnalyzer.LambdaPredictionRatio:F2}   " +
                      $"g†/(cH) = {UnifiedScaleAnalyzer.Gdagger / UnifiedScaleAnalyzer.CH:F3}");
        sb.AppendLine(new string('=', 100));

        Output.WriteLine(sb.ToString());
        File.WriteAllText(Path.Combine(outDir, "QG095_UnifiedScale_Report.txt"), sb.ToString());

        Assert.True(File.Exists(Path.Combine(outDir, "UnifiedScaleRelations.csv")));
        // The 'why now' coincidence: H²/Λc² is O(1).
        Assert.InRange(UnifiedScaleAnalyzer.H2OverLambda, 0.1, 10.0);
        // g† is closer to cH (1/2π ≈ 0.159) than to c²√Λ.
        double dH = Math.Abs(UnifiedScaleAnalyzer.Gdagger - UnifiedScaleAnalyzer.CH);
        double dL = Math.Abs(UnifiedScaleAnalyzer.Gdagger - UnifiedScaleAnalyzer.C2SqrtLambda);
        Assert.True(dH < dL);
        // Neither Λ nor g† can derive the other.
        Assert.False(LambdaOriginModel.CanDeriveH);
        Assert.False(GdaggerOriginModel.CanDeriveLambda);
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
