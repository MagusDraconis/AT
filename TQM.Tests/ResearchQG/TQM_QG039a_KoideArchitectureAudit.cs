using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG039a_KoideArchitectureAudit:ResearchTestBase{
    public TQM_QG039a_KoideArchitectureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG039a_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-039a Koide Architecture Audit");
        var r=KoideArchitectureAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Koide Relation");sb.AppendLine(r.SA);
        S(sb,"Section B -- Frequency -> Amplitude: Why sqrt(m) is Natural");sb.AppendLine(r.SB);
        S(sb,"Section C -- Geometric Interpretation: The 45-degree Mass Vector");sb.AppendLine(r.SC);
        S(sb,"Section D -- Fourth Generation Stress Test");sb.AppendLine(r.SD);
        S(sb,"Section E -- Higgs-Amplitude Correspondence");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Coincidence Audit");sb.AppendLine(r.SF);
        S(sb,"Section G -- Final Verdict");sb.AppendLine(r.SG);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG039a_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
