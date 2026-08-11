using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG027_FrequencyHierarchyAudit:ResearchTestBase{
    public TQM_QG027_FrequencyHierarchyAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG027_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-027 Frequency Hierarchy Audit");
        var r=FrequencyHierarchyAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Fundamental Frequency");sb.AppendLine(r.SA);
        S(sb,"Section B — Frequency Cascade");sb.AppendLine(r.SB);
        S(sb,"Section C — Particles as Frequencies");sb.AppendLine(r.SC);
        S(sb,"Section D — Atoms as Resonances");sb.AppendLine(r.SD);
        S(sb,"Section E — Multi-Scale Hierarchy");sb.AppendLine(r.SE);
        S(sb,"Section F — Unified Spectrum");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-027 COMPLETE.");
        sb.AppendLine("  All physics = frequency hierarchy from tau.");
        sb.AppendLine("  QG program: 27 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG027_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
