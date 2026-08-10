using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG005_QEventGrowthLawAudit:ResearchTestBase{
    public TQM_QG005_QEventGrowthLawAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG005_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-005 Q-Event Growth Law Audit");
        var r=QEventGrowthLawAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Why Q-Events Grow");sb.AppendLine(r.SA);
        S(sb,"Section B — Growth Law Candidates");sb.AppendLine(r.SB);
        S(sb,"Section C — Derived N(t) — Cosmological Eras");sb.AppendLine(r.SC);
        S(sb,"Section D — Hubble Emergence");sb.AppendLine(r.SD);
        S(sb,"Section E — Cosmological Era Audit");sb.AppendLine(r.SE);
        S(sb,"Section F — Dark Energy & w(z)");sb.AppendLine(r.SF);
        S(sb,"Section G — Future Cosmic Evolution");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-005 COMPLETE.");
        sb.AppendLine("  QG program complete (5 experiments). Growth law not derived from first principles.");
        sb.AppendLine("  TQM reduces ΛCDM (6 params) to 2 primitives + 3 params.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG005_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
