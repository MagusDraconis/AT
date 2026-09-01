using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG043_StructureParameterBoundaryAudit:ResearchTestBase{
    public AT_QG043_StructureParameterBoundaryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG043_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-043 Structure-to-Parameter Boundary Audit");
        var r=StructureParameterBoundaryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Structure Versus Parameter Recap");sb.AppendLine(r.SA);
        S(sb,"Section B -- Koide as a Structural Constraint");sb.AppendLine(r.SB);
        S(sb,"Section C -- Parameter Manifold Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Constraint Surfaces (Catalog)");sb.AppendLine(r.SD);
        S(sb,"Section E -- Actualization Selection Model");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Review");sb.AppendLine(r.SF);
        S(sb,"Section G -- The Derivability Boundary: Three Layers");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG043_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
