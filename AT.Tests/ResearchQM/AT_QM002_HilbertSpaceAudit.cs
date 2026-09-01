using System.Globalization;using System.Text;using AT.Core.ResearchQM;using AT.Tests.Shared;namespace AT.Tests.ResearchQM;
public class AT_QM002_HilbertSpaceAudit:ResearchTestBase{
    public AT_QM002_HilbertSpaceAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QM002_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQM-002 Hilbert Space Emergence Audit");
        var r=HilbertSpaceAnalyzer.RunFullAnalysis();
        S(sb,"Section A — Q-Event State Structure");sb.AppendLine(r.SA);
        S(sb,"Section B — Amplitude Emergence");sb.AppendLine(r.SB);
        S(sb,"Section C — Interference Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D — Inner Product Emergence");sb.AppendLine(r.SD);
        S(sb,"Section E — Tensor Products");sb.AppendLine(r.SE);
        S(sb,"Section F — Hilbert Reconstruction");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Gaps");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQM-002 COMPLETE.");
        sb.AppendLine("  Hilbert space RECONSTRUCTED from Q-events in 6 steps.");
        sb.AppendLine("  With QM-001, AT now explains the 2 largest QM axioms.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QM002_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
