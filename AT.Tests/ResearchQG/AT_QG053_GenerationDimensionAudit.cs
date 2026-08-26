using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG053_GenerationDimensionAudit:ResearchTestBase{
    public AT_QG053_GenerationDimensionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG053_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-053 Generation Space Dimension Selection Audit");
        var r=GenerationDimensionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Dimension Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- Dimension Counting: CP Phases and Mixing Angles");sb.AppendLine(r.SB);
        S(sb,"Section C -- CP Violation Minimality: The Lower Bound");sb.AppendLine(r.SC);
        S(sb,"Section D -- Higher-Dimension Stress Test: Why Not N=4,5?");sb.AppendLine(r.SD);
        S(sb,"Section E -- Generation Geometry: What Changes with N");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Dimension Audit: Assume Arbitrary N");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG053_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
