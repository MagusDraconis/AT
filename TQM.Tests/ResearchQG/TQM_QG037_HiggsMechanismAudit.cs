using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG037_HiggsMechanismAudit:ResearchTestBase{
    public TQM_QG037_HiggsMechanismAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG037_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-037 Higgs Mechanism Reinterpretation Audit");
        var r=HiggsMechanismAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- What Is Mass in TQM?");sb.AppendLine(r.SA);
        S(sb,"Section B -- The Standard Model Higgs Mechanism");sb.AppendLine(r.SB);
        S(sb,"Section C -- Architecture Versus Higgs");sb.AppendLine(r.SC);
        S(sb,"Section D -- Amplitude-Mode Interpretation");sb.AppendLine(r.SD);
        S(sb,"Section E -- Particle Spectrum: TQM + Higgs");sb.AppendLine(r.SE);
        S(sb,"Section F -- Collider Compatibility");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG037_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
