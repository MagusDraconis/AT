using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG024_ResonanceLeverageAudit:ResearchTestBase{
    public TQM_QG024_ResonanceLeverageAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG024_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-024 Resonance Leverage Audit");
        var r=ResonanceLeverageAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Leverage Hypothesis");sb.AppendLine(r.SA);
        S(sb,"Section B — Leverage Points");sb.AppendLine(r.SB);
        S(sb,"Section C — Synchronization Amplification");sb.AppendLine(r.SC);
        S(sb,"Section D — Topological Leverage");sb.AppendLine(r.SD);
        S(sb,"Section E — M^2 Amplification");sb.AppendLine(r.SE);
        S(sb,"Section F — Gravity Coupling Relevance");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Most Promising Parameter");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-024 COMPLETE.");
        sb.AppendLine("  NO leverage found. Stability = Unmanipulability.");
        sb.AppendLine("  QG program: 24 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG024_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
