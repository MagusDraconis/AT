using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG018_LocalTimeVsActualizationTimeAudit:ResearchTestBase{
    public AT_QG018_LocalTimeVsActualizationTimeAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG018_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-018 Local Time vs Actualization Time Audit");
        var r=LocalTimeActualizationTimeAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Layers of Time");sb.AppendLine(r.SA);
        S(sb,"Section B — Time Ontology");sb.AppendLine(r.SB);
        S(sb,"Section C — Time in Different Environments");sb.AppendLine(r.SC);
        S(sb,"Section D — Local c Consistency");sb.AppendLine(r.SD);
        S(sb,"Section E — Dual-Time Structure");sb.AppendLine(r.SE);
        S(sb,"Section F — Gravity-Time Interaction");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Ambiguities");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-018 COMPLETE.");
        sb.AppendLine("  DUAL-TIME: tau (fundamental, invariant) + proper time (emergent, dilates).");
        sb.AppendLine("  QG program: 19 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG018_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
