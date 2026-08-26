using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG049_QCDYukawaScramblingAudit:ResearchTestBase{
    public AT_QG049_QCDYukawaScramblingAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG049_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-049 QCD Yukawa Scrambling Audit");
        var r=QCDYukawaScramblingAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Lepton 45-Degree Baseline");sb.AppendLine(r.SA);
        S(sb,"Section B -- Quark Deviation Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- QCD Distortion Mechanism: Common-Factor Result");sb.AppendLine(r.SC);
        S(sb,"Section D -- RG Evolution Effects");sb.AppendLine(r.SD);
        S(sb,"Section E -- CKM Interpretation");sb.AppendLine(r.SE);
        S(sb,"Section F -- Null-Model Comparison");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for AT");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG049_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
