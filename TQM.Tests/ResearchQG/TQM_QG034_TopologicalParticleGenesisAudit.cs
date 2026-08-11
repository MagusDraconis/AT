using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG034_TopologicalParticleGenesisAudit:ResearchTestBase{
    public TQM_QG034_TopologicalParticleGenesisAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG034_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-034 Topological Particle Genesis Audit");
        var r=TopologicalParticleGenesisAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why Topology Matters");sb.AppendLine(r.SA);
        S(sb,"Section B — Allowed Winding Sectors");sb.AppendLine(r.SB);
        S(sb,"Section C — Stability Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D — Particle Mapping");sb.AppendLine(r.SD);
        S(sb,"Section E — Mass Generation from Topology");sb.AppendLine(r.SE);
        S(sb,"Section F — Charge and Spin from Topology");sb.AppendLine(r.SF);
        S(sb,"Section G — Spectrum Selection");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG034_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
