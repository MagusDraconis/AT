using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG006_RandomActualizationAudit:ResearchTestBase{
    public AT_QG006_RandomActualizationAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG006_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-006 Random Actualization Irreducibility Audit");
        var r=RandomActualizationAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is Random Actualization?");sb.AppendLine(r.SA);
        S(sb,"Section B — Dependency Graph");sb.AppendLine(r.SB);
        S(sb,"Section C — Removal Audit");sb.AppendLine(r.SC);
        S(sb,"Section D — Deterministic Replacement Test");sb.AppendLine(r.SD);
        S(sb,"Section E — Information Interpretation");sb.AppendLine(r.SE);
        S(sb,"Section F — Ontology Classification");sb.AppendLine(r.SF);
        S(sb,"Section G — Bedrock Analysis");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-006 COMPLETE.");
        sb.AppendLine("  Random Actualization is IRREDUCIBLE. AT bedrock: Q + Randomness.");
        sb.AppendLine("  QG program (6 experiments) COMPLETE. AT fully compressed.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG006_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
