using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG050_LeptonSpecificSymmetryAudit:ResearchTestBase{
    public AT_QG050_LeptonSpecificSymmetryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG050_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-050 Lepton-Specific Symmetry Audit");
        var r=LeptonSpecificSymmetryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Lepton-Specific Mystery");sb.AppendLine(r.SA);
        S(sb,"Section B -- Lepton-Quark Comparison");sb.AppendLine(r.SB);
        S(sb,"Section C -- Color Versus No-Color Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Generation Symmetry Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Neutrino Correspondence");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hidden Architecture Investigation");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG050_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
