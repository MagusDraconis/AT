using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG045_SymmetryConstraintAudit:ResearchTestBase{
    public AT_QG045_SymmetryConstraintAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG045_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-045 Symmetry as Constraint Generator Audit");
        var r=SymmetryConstraintAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Symmetry Versus Stability");sb.AppendLine(r.SA);
        S(sb,"Section B -- Koide Symmetry Audit (S3)");sb.AppendLine(r.SB);
        S(sb,"Section C -- Gauge Symmetry Contribution");sb.AppendLine(r.SC);
        S(sb,"Section D -- Quantization: Symmetry or Topology?");sb.AppendLine(r.SD);
        S(sb,"Section E -- Constraint Classification");sb.AppendLine(r.SE);
        S(sb,"Section F -- Unified-Origin Investigation");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG045_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
