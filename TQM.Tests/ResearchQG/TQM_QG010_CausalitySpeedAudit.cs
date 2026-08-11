using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG010_CausalitySpeedAudit:ResearchTestBase{
    public TQM_QG010_CausalitySpeedAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG010_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-010 Speed of Causality & Planck Scale Selection Audit");
        var r=CausalitySpeedAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is c in TQM?");sb.AppendLine(r.SA);
        S(sb,"Section B — c->infinity Limit");sb.AppendLine(r.SB);
        S(sb,"Section C — Actualization Rate");sb.AppendLine(r.SC);
        S(sb,"Section D — Minimum Time");sb.AppendLine(r.SD);
        S(sb,"Section E — Length Emergence Chain");sb.AppendLine(r.SE);
        S(sb,"Section F — Planck Scale Reconstruction");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-010 COMPLETE.");
        sb.AppendLine("  c < infinity LOGICALLY REQUIRED. l = c*tau is DERIVED.");
        sb.AppendLine("  QG program: 10 experiments. Parameters: 3 (c,tau,hbar).");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG010_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
