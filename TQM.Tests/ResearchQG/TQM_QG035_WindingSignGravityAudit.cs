using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG035_WindingSignGravityAudit:ResearchTestBase{
    public TQM_QG035_WindingSignGravityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG035_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-035 Winding Sign and Gravity Coupling Audit");
        var r=WindingSignGravityAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why Winding Sign Matters (or Doesn't)");sb.AppendLine(r.SA);
        S(sb,"Section B -- Winding Reversal: n -> -n");sb.AppendLine(r.SB);
        S(sb,"Section C -- Curvature Comparison: R(n) vs R(-n)");sb.AppendLine(r.SC);
        S(sb,"Section D -- Anti-Matter Interpretation");sb.AppendLine(r.SD);
        S(sb,"Section E -- Repulsive-Sector Candidates from Winding Sign");sb.AppendLine(r.SE);
        S(sb,"Section F -- Experimental Consistency");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG035_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
