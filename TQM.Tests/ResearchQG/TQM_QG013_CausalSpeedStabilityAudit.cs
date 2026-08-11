using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG013_CausalSpeedStabilityAudit:ResearchTestBase{
    public TQM_QG013_CausalSpeedStabilityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG013_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-013 Causal Speed Stability Audit");
        var r=CausalSpeedStabilityAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why c Matters");sb.AppendLine(r.SA);
        S(sb,"Section B — c Extrema Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C — Scale Sweep");sb.AppendLine(r.SC);
        S(sb,"Section D — Stability Landscape");sb.AppendLine(r.SD);
        S(sb,"Section E — Information Throughput");sb.AppendLine(r.SE);
        S(sb,"Section F — Selection Analysis");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-013 COMPLETE.");
        sb.AppendLine("  c = 299792458 m/s is UNIT CONVENTION, not physical parameter.");
        sb.AppendLine("  c = l/tau. In natural units, c = 1. QG program: 13 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG013_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
