using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG059_FlavorExceptionalismAudit:ResearchTestBase{
    public AT_QG059_FlavorExceptionalismAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG059_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-059 Flavor Exceptionalism Audit");
        var r=FlavorExceptionalismAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Flavor Exceptionalism Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- Inventory of Flavor Relations");sb.AppendLine(r.SB);
        S(sb,"Section C -- Exceptional vs Generic Structure");sb.AppendLine(r.SC);
        S(sb,"Section D -- Hidden Symmetry Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Low-Energy Remnant Analysis");sb.AppendLine(r.SE);
        S(sb,"Section F -- Generation-Space Implications");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Coincidence Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG059_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
