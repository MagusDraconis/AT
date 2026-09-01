using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG038_GaugeGroupSelectionAudit:ResearchTestBase{
    public AT_QG038_GaugeGroupSelectionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG038_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-038 SU(3)xSU(2)xU(1) Gauge Group Selection Audit");
        var r=GaugeGroupSelectionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- U(1) Derivation: The Complete Success");sb.AppendLine(r.SB);
        S(sb,"Section C -- SU(2) Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- SU(3) Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Alternative Group Comparison");sb.AppendLine(r.SE);
        S(sb,"Section F -- Complexity and Stability Selection");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG038_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
