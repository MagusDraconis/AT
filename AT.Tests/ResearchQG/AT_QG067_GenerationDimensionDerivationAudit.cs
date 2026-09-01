using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG067_GenerationDimensionDerivationAudit:ResearchTestBase{
    public AT_QG067_GenerationDimensionDerivationAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG067_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-067 Generation Dimension Derivation Audit");
        var r=GenerationDimensionDerivationAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Dimension Problem (Derivation Required)");sb.AppendLine(r.SA);
        S(sb,"Section B -- Attractor Branching: Can the Landscape Force 3?");sb.AppendLine(r.SB);
        S(sb,"Section C -- Landscape Topology: Why No 3 from the Landscape");sb.AppendLine(r.SC);
        S(sb,"Section D -- Bifurcation Analysis: Catastrophe Theory");sb.AppendLine(r.SD);
        S(sb,"Section E -- Architecture-Family: Why the Count Is Selected");sb.AppendLine(r.SE);
        S(sb,"Section F -- Selection Versus Derivation");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG067_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
