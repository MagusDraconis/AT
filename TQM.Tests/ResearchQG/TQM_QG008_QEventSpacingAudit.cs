using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG008_QEventSpacingAudit:ResearchTestBase{
    public TQM_QG008_QEventSpacingAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG008_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-008 Q-Event Spacing Determination Audit");
        var r=QEventSpacingAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is l?");sb.AppendLine(r.SA);
        S(sb,"Section B — Candidate Origins");sb.AppendLine(r.SB);
        S(sb,"Section C — What Depends on l");sb.AppendLine(r.SC);
        S(sb,"Section D — Constraints on l");sb.AppendLine(r.SD);
        S(sb,"Section E — Parameter Elimination");sb.AppendLine(r.SE);
        S(sb,"Section F — Consistency Audit");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-008 COMPLETE.");
        sb.AppendLine("  l is NOT derived. Final frontier of TQM.");
        sb.AppendLine("  26 params (SM+GR) -> 2 primitives + 1-3 params (TQM).");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG008_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
