using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG012_PlanckScaleSelectionAudit:ResearchTestBase{
    public AT_QG012_PlanckScaleSelectionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG012_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-012 Planck Scale Selection Audit");
        var r=PlanckScaleSelectionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Planck-Scale Problem");sb.AppendLine(r.SA);
        S(sb,"Section B — Scale Variation Audit");sb.AppendLine(r.SB);
        S(sb,"Section C — Emergence Stability");sb.AppendLine(r.SC);
        S(sb,"Section D — Selection Mechanisms");sb.AppendLine(r.SD);
        S(sb,"Section E — Information Density");sb.AppendLine(r.SE);
        S(sb,"Section F — Fixed-Point Analysis");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Assumptions");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-012 COMPLETE.");
        sb.AppendLine("  Planck scale NOT derived. ~26 params -> 3-5. QG program COMPLETE.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG012_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
