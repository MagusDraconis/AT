using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG004_CosmicExpansionAudit:ResearchTestBase{
    public AT_QG004_CosmicExpansionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG004_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-004 Cosmic Expansion Emergence Audit");
        var r=CosmicExpansionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Q-Event Growth");sb.AppendLine(r.SA);
        S(sb,"Section B — Connectivity Evolution");sb.AppendLine(r.SB);
        S(sb,"Section C — Distance Emergence");sb.AppendLine(r.SC);
        S(sb,"Section D — Scale Factor Reconstruction");sb.AppendLine(r.SD);
        S(sb,"Section E — Hubble Emergence");sb.AppendLine(r.SE);
        S(sb,"Section F — Dark Energy Implications");sb.AppendLine(r.SF);
        S(sb,"Section G — Cosmology Comparison");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-004 COMPLETE.");
        sb.AppendLine("  Expansion = Q-event network growth. Λ(t) emerges from volume.");
        sb.AppendLine("  Unique prediction: w(z) = -1 + 0.015(1+z)^(3/2).");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG004_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
