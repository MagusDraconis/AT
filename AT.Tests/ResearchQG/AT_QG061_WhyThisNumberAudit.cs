using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG061_WhyThisNumberAudit:ResearchTestBase{
    public AT_QG061_WhyThisNumberAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG061_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-061 Why This Number Audit");
        var r=WhyThisNumberAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why This Number?");sb.AppendLine(r.SA);
        S(sb,"Section B -- Rational Number Audit");sb.AppendLine(r.SB);
        S(sb,"Section C -- Geometric Ratio Audit: 2/3 = 1/(3 cos^2 45 deg)");sb.AppendLine(r.SC);
        S(sb,"Section D -- Simplex and Projection Ratio Audit");sb.AppendLine(r.SD);
        S(sb,"Section E -- Boundary-Value Stability Audit");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Nearby-Value Scan (0.60 - 0.75)");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG061_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
