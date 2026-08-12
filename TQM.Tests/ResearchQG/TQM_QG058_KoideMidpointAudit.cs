using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG058_KoideMidpointAudit:ResearchTestBase{
    public TQM_QG058_KoideMidpointAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG058_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-058 Koide Midpoint Principle Audit");
        var r=KoideMidpointAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Midpoint Observation");sb.AppendLine(r.SA);
        S(sb,"Section B -- Participation-Ratio Analysis: The N-Dependence");sb.AppendLine(r.SB);
        S(sb,"Section C -- Information-Theoretic Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Attractor Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Cross-System Analogies");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Review: Is the Midpoint Numerology?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Implications for Flavor Physics");sb.AppendLine(r.SG);
        S(sb,"Section H -- Remaining Open Problems");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG058_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
