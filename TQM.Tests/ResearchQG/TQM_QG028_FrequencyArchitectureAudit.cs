using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG028_FrequencyArchitectureAudit:ResearchTestBase{
    public TQM_QG028_FrequencyArchitectureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG028_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-028 Frequency Architecture Audit");
        var r=FrequencyArchitectureAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Frequency Architecture Taxonomy");sb.AppendLine(r.SA);
        S(sb,"Section B — Same Energy, Different Architecture");sb.AppendLine(r.SB);
        S(sb,"Section C — Particle Architectures");sb.AppendLine(r.SC);
        S(sb,"Section D — Frequency -> Mass");sb.AppendLine(r.SD);
        S(sb,"Section E — Frequency -> Geometry");sb.AppendLine(r.SEct);
        S(sb,"Section F — Architecture Taxonomy — Physical Consequences");sb.AppendLine(r.SF);
        S(sb,"Section G — Gravity Implications");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-028 COMPLETE.");
        sb.AppendLine("  Architecture > Energy. Reality = organized frequency.");
        sb.AppendLine("  QG program: 28 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG028_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
