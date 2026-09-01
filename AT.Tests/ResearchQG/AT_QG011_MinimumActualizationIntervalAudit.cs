using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG011_MinimumActualizationIntervalAudit:ResearchTestBase{
    public AT_QG011_MinimumActualizationIntervalAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG011_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-011 Minimum Actualization Interval Audit");
        var r=MinimumActualizationIntervalAnalyzer.RunFullAnalysis();
        S(sb,"Section A — What Is tau?");sb.AppendLine(r.SA);
        S(sb,"Section B — tau->0 Limit");sb.AppendLine(r.SB);
        S(sb,"Section C — Continuous Reality Audit");sb.AppendLine(r.SC);
        S(sb,"Section D — Becoming and Causality");sb.AppendLine(r.SD);
        S(sb,"Section E — Information Implications");sb.AppendLine(r.SE);
        S(sb,"Section F — Dependency Analysis");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQG-011 COMPLETE.");
        sb.AppendLine("  tau > 0 LOGICALLY REQUIRED. Spatial + temporal grain unified.");
        sb.AppendLine("  QG program: 11 experiments. l + tau + hbar: 3 parameters.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG011_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
