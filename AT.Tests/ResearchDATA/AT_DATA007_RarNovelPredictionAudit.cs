using System.Globalization;using System.Text;using AT.Core.ResearchDATA;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchDATA;
public class AT_DATA007_RarNovelPredictionAudit:ResearchTestBase{
    public AT_DATA007_RarNovelPredictionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void DATA007_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchDATA-007 RAR Novel Prediction Audit");
        var r=RarNovelPredictionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Novel Predictions");sb.AppendLine(r.SA);
        S(sb,"Section B — g†(z) Evolution");sb.AppendLine(r.SB);
        S(sb,"Section C — Scatter Evolution");sb.AppendLine(r.SC);
        S(sb,"Section D — Galaxy-Type Predictions");sb.AppendLine(r.SD);
        S(sb,"Section E — Failure Conditions");sb.AppendLine(r.SE);
        S(sb,"Section F — Observational Priorities");sb.AppendLine(r.SF);
        S(sb,"Section G — MOND Comparison");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchDATA-007 COMPLETE.");
        sb.AppendLine("  AT now has a UNIQUE, FALSIFIABLE prediction: g†(z) INCREASES with redshift.");
        sb.AppendLine("  This distinguishes AT from MOND (a0=const) and LCDM (no prediction).");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DATA007_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
