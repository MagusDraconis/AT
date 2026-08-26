using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG019_QuantumScaleAmplificationAudit:ResearchTestBase{
    public AT_QG019_QuantumScaleAmplificationAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG019_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-019 Quantum Scale Amplification Audit");
        var r=QuantumScaleAmplificationAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Spatial Scale Hierarchy");sb.AppendLine(r.SA);
        S(sb,"Section B — Temporal Scale Hierarchy");sb.AppendLine(r.SB);
        S(sb,"Section C — Amplification Factors");sb.AppendLine(r.SC);
        S(sb,"Section D — Emergence Layers");sb.AppendLine(r.SD);
        S(sb,"Section E — Continuum Validity");sb.AppendLine(r.SE);
        S(sb,"Section F — Observable Consequences");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-019 COMPLETE.");
        sb.AppendLine("  Atom~10^24 l. Atomic time~10^27 tau. No grain signature at current expt.");
        sb.AppendLine("  QG program: 20 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG019_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
