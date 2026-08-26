using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG025_ActualizationDynamicsAudit:ResearchTestBase{
    public AT_QG025_ActualizationDynamicsAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG025_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-025 Actualization Dynamics Audit");
        var r=ActualizationDynamicsAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is Actualization?");sb.AppendLine(r.SA);
        S(sb,"Section B — Actualization Properties");sb.AppendLine(r.SB);
        S(sb,"Section C — Actualization Regimes");sb.AppendLine(r.SC);
        S(sb,"Section D — Amplification Potential");sb.AppendLine(r.SD);
        S(sb,"Section E — Control Layer Hierarchy");sb.AppendLine(r.SE);
        S(sb,"Section F — Has AT Found a Lever?");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Unknowns");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-025 COMPLETE.");
        sb.AppendLine("  Actualization = STATIC PRIMITIVE. Manipulation trilogy: NO.");
        sb.AppendLine("  QG program: 25 experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG025_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
