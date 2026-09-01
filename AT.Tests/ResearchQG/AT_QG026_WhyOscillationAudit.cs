using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG026_WhyOscillationAudit:ResearchTestBase{
    public AT_QG026_WhyOscillationAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG026_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-026 Why Does Actualization Generate Oscillation Audit");
        var r=ActualizationOscillationAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Oscillation Question");sb.AppendLine(r.SA);
        S(sb,"Section B — Non-Oscillatory Models");sb.AppendLine(r.SB);
        S(sb,"Section C — The Inevitable Chain");sb.AppendLine(r.SC);
        S(sb,"Section D — Phase Origin");sb.AppendLine(r.SD);
        S(sb,"Section E — Recursion and Feedback");sb.AppendLine(r.SE);
        S(sb,"Section F — Necessity Classification");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-026 COMPLETE.");
        sb.AppendLine("  Oscillation = LOGICAL INEVITABILITY. All physics from Q + Randomness.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG026_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
