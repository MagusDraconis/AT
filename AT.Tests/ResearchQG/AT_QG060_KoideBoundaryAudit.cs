using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG060_KoideBoundaryAudit:ResearchTestBase{
    public AT_QG060_KoideBoundaryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG060_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-060 Koide Boundary Condition Audit");
        var r=KoideBoundaryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Boundary Question");sb.AppendLine(r.SA);
        S(sb,"Section B -- Flavor-Manifold: The Koide Surface");sb.AppendLine(r.SB);
        S(sb,"Section C -- Fixed-Point Audit: Is Q=2/3 an Attractor?");sb.AppendLine(r.SC);
        S(sb,"Section D -- Boundary-State: Democracy vs Hierarchy Divider");sb.AppendLine(r.SD);
        S(sb,"Section E -- Random Flavor: Distance to the Surface");sb.AppendLine(r.SE);
        S(sb,"Section F -- Lepton Localization: Why Charged Leptons on the Surface?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Null Review: Predictive Power");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG060_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
