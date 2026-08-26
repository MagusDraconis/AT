using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG047_Koide45DegreeSelectionAudit:ResearchTestBase{
    public AT_QG047_Koide45DegreeSelectionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG047_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-047 Koide 45-Degree Selection Audit");
        var r=KoideAngleSelectionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The 45-Degree Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- Geometric Interpretation");sb.AppendLine(r.SB);
        S(sb,"Section C -- S3 Symmetry-Breaking Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Attractor-Balance Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Yukawa-Space Interpretation");sb.AppendLine(r.SE);
        S(sb,"Section F -- Robustness Scan: Q(theta) and Balance");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Coincidence Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG047_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
