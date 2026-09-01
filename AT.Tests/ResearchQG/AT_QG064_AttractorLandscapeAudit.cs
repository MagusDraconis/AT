using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG064_AttractorLandscapeAudit:ResearchTestBase{
    public AT_QG064_AttractorLandscapeAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG064_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-064 Attractor Landscape Audit");
        var r=AttractorLandscapeAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Attractor Landscape Question");sb.AppendLine(r.SA);
        S(sb,"Section B -- Attractor Landscape: Form Derived, Content Contingent");sb.AppendLine(r.SB);
        S(sb,"Section C -- Frequency Basin: Why Discrete Architectures?");sb.AppendLine(r.SC);
        S(sb,"Section D -- Generation Emergence: G from the Landscape?");sb.AppendLine(r.SD);
        S(sb,"Section E -- Koide Landscape: Is 45-Degree a Landscape Property?");sb.AppendLine(r.SE);
        S(sb,"Section F -- Resonance Structure: Do Basins Have Resonant Depths?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Arbitrary Landscape Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG064_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
