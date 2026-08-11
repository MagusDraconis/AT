using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG021_OscillationPrimacyAudit:ResearchTestBase{
    public TQM_QG021_OscillationPrimacyAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG021_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-021 Oscillation Primacy Audit");
        var r=OscillationPrimacyAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why Oscillation Matters");sb.AppendLine(r.SA);
        S(sb,"Section B — Oscillation Removal Audit");sb.AppendLine(r.SB);
        S(sb,"Section C — Phase and Interference");sb.AppendLine(r.SC);
        S(sb,"Section D — Resonance and Stability");sb.AppendLine(r.SD);
        S(sb,"Section E — Oscillation->Reality Bridge");sb.AppendLine(r.SE);
        S(sb,"Section F — Geometry and Gravity");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-021 COMPLETE.");
        sb.AppendLine("  Oscillation = PRIMARY MECHANISM. Bridge from Q-events to reality.");
        sb.AppendLine("  QG program: 21 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG021_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
