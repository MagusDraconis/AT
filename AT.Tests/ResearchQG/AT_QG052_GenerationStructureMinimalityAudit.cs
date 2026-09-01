using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG052_GenerationStructureMinimalityAudit:ResearchTestBase{
    public AT_QG052_GenerationStructureMinimalityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG052_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-052 Generation Structure Minimality Audit");
        var r=GenerationMinimalityAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why Generations Are Still Unexplained");sb.AppendLine(r.SA);
        S(sb,"Section B -- Minimal Structure Analysis: What Fails");sb.AppendLine(r.SB);
        S(sb,"Section C -- Generation-Space Construction");sb.AppendLine(r.SC);
        S(sb,"Section D -- Attractor-Family Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Mixing Interpretation: Rotations in Generation Space");sb.AppendLine(r.SE);
        S(sb,"Section F -- Dimension Selection: Why 3D?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Reduction Review: Eliminate G?");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG052_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
