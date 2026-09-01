using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG065_OntologyCompletionAudit:ResearchTestBase{
    public AT_QG065_OntologyCompletionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG065_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-065 Ontology Completion Audit");
        var r=OntologyCompletionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Ontology Recap: The Primitives");sb.AppendLine(r.SA);
        S(sb,"Section B -- Reduction Chain Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- Irreducible Residues");sb.AppendLine(r.SC);
        S(sb,"Section D -- Structure vs Content: The Universal Split");sb.AppendLine(r.SD);
        S(sb,"Section E -- Actualization as Bedrock: Logical, Not Physical");sb.AppendLine(r.SE);
        S(sb,"Section F -- Alternative Deeper Ontologies");sb.AppendLine(r.SF);
        S(sb,"Section G -- Infinite-Regression Analysis");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG065_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
