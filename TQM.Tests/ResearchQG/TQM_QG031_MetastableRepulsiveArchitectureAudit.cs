using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG031_MetastableRepulsiveArchitectureAudit:ResearchTestBase{
    public TQM_QG031_MetastableRepulsiveArchitectureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG031_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-031 Metastable Repulsive Architecture Audit");
        var r=MetastableRepulsiveArchitectureAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why Repulsive Architectures Fail");sb.AppendLine(r.SA);
        S(sb,"Section B — Candidate Metastable Architectures");sb.AppendLine(r.SB);
        S(sb,"Section C — Lifetime Estimates");sb.AppendLine(r.SC);
        S(sb,"Section D — Domain-Wall Behavior");sb.AppendLine(r.SD);
        S(sb,"Section E — Topological Protection");sb.AppendLine(r.SE);
        S(sb,"Section F — Observable Signatures");sb.AppendLine(r.SF);
        S(sb,"Section G — Reality Constraints");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));
        sb.AppendLine("  ResearchQG-031 COMPLETE.");
        sb.AppendLine("  Metastable repulsive = Dark Energy at cosmological scale.");
        sb.AppendLine("  Trilogy: QG-029 (attraction dominates), QG-030 (no counter-structure),");
        sb.AppendLine("  QG-031 (metastable = DE only). Gravity manipulation program finished.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG031_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
