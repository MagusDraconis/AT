using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG066_GenerationSpaceOriginAudit:ResearchTestBase{
    public TQM_QG066_GenerationSpaceOriginAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG066_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-066 Generation Space Origin Audit");
        var r=GenerationSpaceOriginAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The G Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- Fundamental-Space Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- Attractor-Emergence: G = the Basin Structure");sb.AppendLine(r.SC);
        S(sb,"Section D -- Actualization-Emergence: Can Randomness Partition?");sb.AppendLine(r.SD);
        S(sb,"Section E -- Architecture-Sector: e, mu, tau as Attractor Branches");sb.AppendLine(r.SE);
        S(sb,"Section F -- Dimension-Selection: Can dim(G)=3 Be Derived?");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Elimination Review: Can We Remove G?");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG066_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
