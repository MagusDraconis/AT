using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG036_InertiaAndGForceAudit:ResearchTestBase{
    public TQM_QG036_InertiaAndGForceAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG036_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-036 Inertia and G-Force Emergence Audit");
        var r=InertiaGForceAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- What Is Inertia in TQM?");sb.AppendLine(r.SA);
        S(sb,"Section B -- Attractor Resistance Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- Phase Reconfiguration Cost");sb.AppendLine(r.SC);
        S(sb,"Section D -- Topological Contribution to Inertia");sb.AppendLine(r.SD);
        S(sb,"Section E -- Frequency Architecture Contribution");sb.AppendLine(r.SE);
        S(sb,"Section F -- G-Force Emergence Mechanism");sb.AppendLine(r.SF);
        S(sb,"Section G -- Equivalence Principle: Derived");sb.AppendLine(r.SG);
        S(sb,"Section H -- Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG036_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
