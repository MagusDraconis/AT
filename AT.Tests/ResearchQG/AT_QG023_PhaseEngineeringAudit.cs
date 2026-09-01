using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG023_PhaseEngineeringAudit:ResearchTestBase{
    public AT_QG023_PhaseEngineeringAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG023_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-023 Phase Engineering Audit");
        var r=PhaseEngineeringAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Phase Engineering Hypothesis");sb.AppendLine(r.SA);
        S(sb,"Section B — Phase-Control Variables");sb.AppendLine(r.SB);
        S(sb,"Section C — Coherence Requirements");sb.AppendLine(r.SC);
        S(sb,"Section D — Gravity Response");sb.AppendLine(r.SD);
        S(sb,"Section E — Energy Cost Audit");sb.AppendLine(r.SE);
        S(sb,"Section F — Experimental Constraints");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Manipulation Pathways");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-023 COMPLETE.");
        sb.AppendLine("  Gravity manipulation: theoretically possible, practically impossible.");
        sb.AppendLine("  Coupling G/c^4 = 8e-45 m/J. Need ~10^21 J for detection.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG023_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
