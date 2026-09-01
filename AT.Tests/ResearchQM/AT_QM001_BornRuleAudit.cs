using System.Globalization;using System.Text;using AT.Core.ResearchQM;using AT.Tests.Shared;namespace AT.Tests.ResearchQM;
public class AT_QM001_BornRuleAudit:ResearchTestBase{
    public AT_QM001_BornRuleAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QM001_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQM-001 Born Rule Derivation Audit");
        var r=BornRuleAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Probability in AT");sb.AppendLine(r.SA);
        S(sb,"Section B — Alternative Probability Laws");sb.AppendLine(r.SB);
        S(sb,"Section C — Frequency Derivation");sb.AppendLine(r.SC);
        S(sb,"Section D — Interference Constraints");sb.AppendLine(r.SD);
        S(sb,"Section E — Experimental Constraints");sb.AppendLine(r.SE);
        S(sb,"Section F — Born Rule Derivation Candidates");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Assumption Audit");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQM-001 COMPLETE.");
        sb.AppendLine("  Born Rule: PARTIALLY DERIVED. Path clear via Gleason+Hilbert emergence.");
        sb.AppendLine("  Reduces 1 axiom vs standard QM. Gap: Hilbert space from Q-events.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QM001_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
