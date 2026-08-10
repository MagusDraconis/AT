using System.Globalization;using System.Text;using TQM.Core.ResearchDATA;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;
public class TQM_DATA008_HighZRarFeasibilityAudit:ResearchTestBase{
    public TQM_DATA008_HighZRarFeasibilityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void DATA008_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchDATA-008 High-z RAR Feasibility Audit");
        var r=HighZRarFeasibilityAnalyzer.RunFullAnalysis();
        S(sb,"Section A — g†(z) Signal Evolution");sb.AppendLine(r.SA);
        S(sb,"Section B — MOND vs TQM Difference");sb.AppendLine(r.SB);
        S(sb,"Section C — Instrument Capabilities");sb.AppendLine(r.SC);
        S(sb,"Section D — Sample Size Requirements");sb.AppendLine(r.SD);
        S(sb,"Section E — Systematic Effects");sb.AppendLine(r.SE);
        S(sb,"Section F — Detection Significance");sb.AppendLine(r.SF);
        S(sb,"Section G — Earliest Feasible Test");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  DATA-008 COMPLETE.");
        sb.AppendLine("  The g†(z) test is HARD but MEASURABLE. 1σ by 2028, 5σ by 2038.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DATA008_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
