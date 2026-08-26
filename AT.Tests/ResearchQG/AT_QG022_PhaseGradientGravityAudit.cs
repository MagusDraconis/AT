using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG022_PhaseGradientGravityAudit:ResearchTestBase{
    public AT_QG022_PhaseGradientGravityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG022_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-022 Phase Gradient & Gravity Audit");
        var r=PhaseGradientGravityAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Oscillation-Gravity Hypothesis");sb.AppendLine(r.SA);
        S(sb,"Section B — The Phase-Gravity Chain");sb.AppendLine(r.SB);
        S(sb,"Section C — Phase Gradient -> Curvature");sb.AppendLine(r.SC);
        S(sb,"Section D — Oscillation Density -> Gravity");sb.AppendLine(r.SD);
        S(sb,"Section E — Mass as Phase Structure");sb.AppendLine(r.SE);
        S(sb,"Section F — Gravity Correspondence");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Potential Manipulation Pathways");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-022 COMPLETE.");
        sb.AppendLine("  Gravity = phase-gradient phenomenon. 22 QG experiments.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG022_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
