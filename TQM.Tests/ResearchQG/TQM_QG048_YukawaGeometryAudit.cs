using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG048_YukawaGeometryAudit:ResearchTestBase{
    public TQM_QG048_YukawaGeometryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG048_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-048 Yukawa Geometry Audit");
        var r=YukawaGeometryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- What Yukawas Represent in TQM");sb.AppendLine(r.SA);
        S(sb,"Section B -- Lepton Yukawa Geometry");sb.AppendLine(r.SB);
        S(sb,"Section C -- Koide in Yukawa Space: The VEV Cancellation");sb.AppendLine(r.SC);
        S(sb,"Section D -- S3 Alignment Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Quark-Sector Extension: Does Koide Generalize?");sb.AppendLine(r.SE);
        S(sb,"Section F -- Mixing-Matrix Geometry");sb.AppendLine(r.SF);
        S(sb,"Section G -- Random-Yukawa Stress Test");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG048_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
