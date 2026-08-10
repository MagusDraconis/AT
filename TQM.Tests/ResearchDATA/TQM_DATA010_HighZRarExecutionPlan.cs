using System.Globalization;using System.Text;using TQM.Core.ResearchDATA;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchDATA;
public class TQM_DATA010_HighZRarExecutionPlan:ResearchTestBase{
    public TQM_DATA010_HighZRarExecutionPlan(ITestOutputHelper o):base(o){}
    [Fact]public void DATA010_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchDATA-010 JWST/Euclid High-z RAR Execution Plan");
        var r=HighZRarExecutionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Available Datasets");sb.AppendLine(r.SA);
        S(sb,"Section B — Instrument Comparison");sb.AppendLine(r.SB);
        S(sb,"Section C — Target Galaxy Selection");sb.AppendLine(r.SC);
        S(sb,"Section D — Analysis Pipeline");sb.AppendLine(r.SD);
        S(sb,"Section E — Falsification Pathway");sb.AppendLine(r.SE);
        S(sb,"Section F — Sample Size Requirements");sb.AppendLine(r.SF);
        S(sb,"Section G — Execution Roadmap 2025-2035");sb.AppendLine(r.SG);
        S(sb,"Section H — Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  DATA-010 COMPLETE.");
        sb.AppendLine("  A publishable g†(z) test exists TODAY using archival KMOS3D+MUSE data.");
        sb.AppendLine("  Phase 1 (archival): 2025-2026. Phase 2 (JWST+Euclid): 2027. Phase 3 (combined): 2031.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DATA010_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
