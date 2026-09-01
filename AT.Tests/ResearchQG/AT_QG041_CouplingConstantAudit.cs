using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG041_CouplingConstantAudit:ResearchTestBase{
    public AT_QG041_CouplingConstantAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG041_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-041 Coupling Constant Origin Audit");
        var r=CouplingConstantAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Coupling Constants: The Unexplained Numbers");sb.AppendLine(r.SA);
        S(sb,"Section B -- Fine Structure Constant: Why 1/137?");sb.AppendLine(r.SB);
        S(sb,"Section C -- Strong and Weak Couplings");sb.AppendLine(r.SC);
        S(sb,"Section D -- Yukawa Couplings: Architectural Overlap");sb.AppendLine(r.SD);
        S(sb,"Section E -- Coupling Unification: The One Hint");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Randomness Audit: The Numerology Graveyard");sb.AppendLine(r.SF);
        S(sb,"Section G -- Final Verdict");sb.AppendLine(r.SG);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG041_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
