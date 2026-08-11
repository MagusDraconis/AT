using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG033_NegativePhaseAntiPhaseArchitectureAudit:ResearchTestBase{
    public TQM_QG033_NegativePhaseAntiPhaseArchitectureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG033_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-033 Negative Phase and Anti-Phase Architecture Audit");
        var r=NegativePhaseAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Phase Circle: S¹ Geometry");sb.AppendLine(r.SA);
        S(sb,"Section B — Anti-Phase Architectures");sb.AppendLine(r.SB);
        S(sb,"Section C — Phase Winding and Topological Architectures");sb.AppendLine(r.SC);
        S(sb,"Section D — Repulsive Geometry from Phase Inversion");sb.AppendLine(r.SD);
        S(sb,"Section E — Global Structure: The Complete Phase Sector");sb.AppendLine(r.SE);
        S(sb,"Section F — Final Verdict");sb.AppendLine(r.SF);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG033_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
