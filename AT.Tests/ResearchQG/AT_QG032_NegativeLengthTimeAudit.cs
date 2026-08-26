using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG032_NegativeLengthTimeAudit:ResearchTestBase{
    public AT_QG032_NegativeLengthTimeAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG032_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-032 Negative Length and Time Audit");
        var r=NegativeLengthTimeAnalyzer.RunFullAnalysis();
        S(sb,"Section A — The Logical Status of ℓ and τ");sb.AppendLine(r.SA);
        S(sb,"Section B — Negative Length: Coordinate vs Metric");sb.AppendLine(r.SB);
        S(sb,"Section C — Negative Time: The Arrow of Becoming");sb.AppendLine(r.SC);
        S(sb,"Section D — Causality Under ℓ<0 and τ<0");sb.AppendLine(r.SD);
        S(sb,"Section E — Oscillation and Phase with τ<0");sb.AppendLine(r.SE);
        S(sb,"Section F — Domain-by-Domain Audit & Verdict");sb.AppendLine(r.SF);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG032_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
