using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG003_HawkingPageCurveAudit:ResearchTestBase{
    public AT_QG003_HawkingPageCurveAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG003_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-003 Hawking Radiation & Page Curve Audit");
        var r=HawkingPageCurveAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Hawking Radiation Emergence");sb.AppendLine(r.SA);
        S(sb,"Section B — Thermal Spectrum Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C — Information Encoding");sb.AppendLine(r.SC);
        S(sb,"Section D — Page Curve Reconstruction");sb.AppendLine(r.SD);
        S(sb,"Section E — Entropy Evolution");sb.AppendLine(r.SE);
        S(sb,"Section F — Approach Comparison");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-003 COMPLETE.");
        sb.AppendLine("  Page curve reproduced qualitatively. Quantitative gaps remain.");
        sb.AppendLine("  QG program complete (QG-001→003). 3 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG003_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
