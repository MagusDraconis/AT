using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG062_YukawaOperatorOriginAudit:ResearchTestBase{
    public TQM_QG062_YukawaOperatorOriginAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG062_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-062 Yukawa Operator Origin Audit");
        var r=YukawaOperatorOriginAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why the Yukawa Operator Matters");sb.AppendLine(r.SA);
        S(sb,"Section B -- Fundamental-Operator Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- Emergent-Operator Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Generation-Geometry Contribution");sb.AppendLine(r.SD);
        S(sb,"Section E -- Unified Flavor Operator: Is There One F?");sb.AppendLine(r.SE);
        S(sb,"Section F -- Koide Implications: Does 45-Degree Constrain Y?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Elimination Review: Can We Remove Y?");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG062_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
