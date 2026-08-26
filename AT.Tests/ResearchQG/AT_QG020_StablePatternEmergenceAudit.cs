using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG020_StablePatternEmergenceAudit:ResearchTestBase{
    public AT_QG020_StablePatternEmergenceAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG020_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-020 Stable Pattern Emergence Audit");
        var r=StablePatternEmergenceAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why Not Just Noise?");sb.AppendLine(r.SA);
        S(sb,"Section B — Attractor Mechanisms");sb.AppendLine(r.SB);
        S(sb,"Section C — Particle & Atom Persistence");sb.AppendLine(r.SC);
        S(sb,"Section D — Self-Organization Hierarchy");sb.AppendLine(r.SD);
        S(sb,"Section E — Emergence Hierarchy");sb.AppendLine(r.SE);
        S(sb,"Section F — Comparison to Complexity Theory");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-020 COMPLETE.");
        sb.AppendLine("  Matter = stable Q-event attractor. Q + M^2 + Topology.");
        sb.AppendLine("  QG program: 20 experiments COMPLETE.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG020_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
