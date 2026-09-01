using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG040_HiggsMassSelectionAudit:ResearchTestBase{
    public AT_QG040_HiggsMassSelectionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG040_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-040 Higgs Mass Selection Audit");
        var r=HiggsMassSelectionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why 125 GeV?");sb.AppendLine(r.SA);
        S(sb,"Section B -- Amplitude Resonance Model");sb.AppendLine(r.SB);
        S(sb,"Section C -- Vacuum Stability Model");sb.AppendLine(r.SC);
        S(sb,"Section D -- Higgs Mass Scan");sb.AppendLine(r.SD);
        S(sb,"Section E -- Architecture Survival Audit");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Coincidence Audit");sb.AppendLine(r.SF);
        S(sb,"Section G -- Final Verdict");sb.AppendLine(r.SG);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG040_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
