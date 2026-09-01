using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG063_ArchitectureShapeOriginAudit:ResearchTestBase{
    public AT_QG063_ArchitectureShapeOriginAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG063_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-063 Architecture Shape Origin Audit");
        var r=ArchitectureShapeOriginAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Architecture-Shape Question");sb.AppendLine(r.SA);
        S(sb,"Section B -- Architecture Definition: What Is the Shape?");sb.AppendLine(r.SB);
        S(sb,"Section C -- Attractor Shape: Stability Determines Form, Not Frequencies");sb.AppendLine(r.SC);
        S(sb,"Section D -- Overlap Formation: How Shapes Produce Hierarchy");sb.AppendLine(r.SD);
        S(sb,"Section E -- Lepton Architecture: The Cleanest Case");sb.AppendLine(r.SE);
        S(sb,"Section F -- Quark Comparison: Why Different Shapes?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Arbitrary-Shape Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG063_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
