using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG042_ParameterVsStructureAudit:ResearchTestBase{
    public AT_QG042_ParameterVsStructureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG042_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-042 Parameter vs Structure Audit");
        var r=ParameterStructureAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- What Is Derivable?");sb.AppendLine(r.SA);
        S(sb,"Section B -- What Resists Derivation?");sb.AppendLine(r.SB);
        S(sb,"Section C -- Structure Versus Parameter");sb.AppendLine(r.SC);
        S(sb,"Section D -- Identity Versus Abundance (QG-065b analogy)");sb.AppendLine(r.SD);
        S(sb,"Section E -- Dimensionless Constants: The True Parameters");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Reduction Attempt: Derive alpha=1/137");sb.AppendLine(r.SF);
        S(sb,"Section G -- The Derivability Boundary");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG042_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
