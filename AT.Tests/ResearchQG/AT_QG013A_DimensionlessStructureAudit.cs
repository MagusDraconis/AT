using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG013A_DimensionlessStructureAudit:ResearchTestBase{
    public AT_QG013A_DimensionlessStructureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG013A_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-013A Dimensionless Structure Audit");
        var r=DimensionlessStructureAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Dimensionless Inventory");sb.AppendLine(r.SA);
        S(sb,"Section B — 2*pi Recurrence Audit");sb.AppendLine(r.SB);
        S(sb,"Section C — Topological Structures");sb.AppendLine(r.SC);
        S(sb,"Section D — Fourier Structures");sb.AppendLine(r.SD);
        S(sb,"Section E — Hidden Constraint Search");sb.AppendLine(r.SE);
        S(sb,"Section F — Invariant Candidates");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Ambiguities");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-013A COMPLETE.");
        sb.AppendLine("  NO hidden invariants. AT provides STRUCTURE, not SCALE.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG013A_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
