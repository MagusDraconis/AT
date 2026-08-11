using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG014_QuantumOfActionAudit:ResearchTestBase{
    public TQM_QG014_QuantumOfActionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG014_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-014 Quantum of Action (hbar) Emergence Audit");
        var r=QuantumOfActionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is hbar?");sb.AppendLine(r.SA);
        S(sb,"Section B — hbar->0 Limit Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C — Event Counting Approach");sb.AppendLine(r.SC);
        S(sb,"Section D — Phase Structure");sb.AppendLine(r.SD);
        S(sb,"Section E — Information Interpretation");sb.AppendLine(r.SE);
        S(sb,"Section F — Dependency Graph");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Assumptions");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-014 COMPLETE.");
        sb.AppendLine("  hbar NOT derived. Irreducible triple: (l,tau,hbar).");
        sb.AppendLine("  QG program: 15 experiments COMPLETE.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG014_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
