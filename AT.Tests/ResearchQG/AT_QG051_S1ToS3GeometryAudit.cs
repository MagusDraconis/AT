using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG051_S1ToS3GeometryAudit:ResearchTestBase{
    public AT_QG051_S1ToS3GeometryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG051_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-051 S1-to-S3 Geometry Emergence Audit");
        var r=S1ToS3GeometryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The S1-to-S3 Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- S1 Excitation Spectrum");sb.AppendLine(r.SB);
        S(sb,"Section C -- Generation Emergence: Where Could 3 Come From?");sb.AppendLine(r.SC);
        S(sb,"Section D -- S3 Emergence: Does 3 Modes Give S3?");sb.AppendLine(r.SD);
        S(sb,"Section E -- Singlet-Doublet Decomposition");sb.AppendLine(r.SE);
        S(sb,"Section F -- Koide-Angle Derivation Attempt");sb.AppendLine(r.SF);
        S(sb,"Section G -- Alternative Generation Structures");sb.AppendLine(r.SG);
        S(sb,"Section H -- Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG051_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
