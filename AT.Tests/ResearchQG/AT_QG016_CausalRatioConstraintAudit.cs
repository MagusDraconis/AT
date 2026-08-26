using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG016_CausalRatioConstraintAudit:ResearchTestBase{
    public AT_QG016_CausalRatioConstraintAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG016_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-016 Causal Ratio Constraint Audit");
        var r=CausalRatioConstraintAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Ratio Problem");sb.AppendLine(r.SA);
        S(sb,"Section B — Ratio Variation");sb.AppendLine(r.SB);
        S(sb,"Section C — Fixed-c Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D — Observable Effects");sb.AppendLine(r.SD);
        S(sb,"Section E — Hidden Ratio Search");sb.AppendLine(r.SE);
        S(sb,"Section F — Consistency Analysis");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Ambiguities");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-016 COMPLETE.");
        sb.AppendLine("  c = l/tau is DEFINITION, not constraint. No hidden ratio law.");
        sb.AppendLine("  QG program: 17 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG016_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
