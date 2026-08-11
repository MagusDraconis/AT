using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG015_ScalePairDegeneracyAudit:ResearchTestBase{
    public TQM_QG015_ScalePairDegeneracyAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG015_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-015 Scale Pair Degeneracy Audit");
        var r=ScalePairDegeneracyAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why l and tau Matter");sb.AppendLine(r.SA);
        S(sb,"Section B — Joint Scaling Results");sb.AppendLine(r.SB);
        S(sb,"Section C — Independent Variation");sb.AppendLine(r.SC);
        S(sb,"Section D — Observable Sensitivity");sb.AppendLine(r.SD);
        S(sb,"Section E — Degeneracy Analysis");sb.AppendLine(r.SE);
        S(sb,"Section F — Constraint Discovery");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Freedom");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-015 COMPLETE.");
        sb.AppendLine("  (l,tau,hbar) = 3 independent parameters. Scaling degeneracy exists but broken by measurement.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG015_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
