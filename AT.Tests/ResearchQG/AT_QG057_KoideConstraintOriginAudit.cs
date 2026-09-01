using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG057_KoideConstraintOriginAudit:ResearchTestBase{
    public AT_QG057_KoideConstraintOriginAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG057_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-057 Koide Constraint Origin Audit");
        var r=KoideConstraintOriginAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Remaining Flavor Mystery");sb.AppendLine(r.SA);
        S(sb,"Section B -- Spectral Analysis: Is Koide a Sum Rule?");sb.AppendLine(r.SB);
        S(sb,"Section C -- Participation-Ratio Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Information-Geometric Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- S3 Texture Analysis");sb.AppendLine(r.SE);
        S(sb,"Section F -- Lepton-Specificity: Why Charged Leptons Only?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Coincidence Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG057_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
