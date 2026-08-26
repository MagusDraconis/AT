using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG046_GenerationSymmetryAudit:ResearchTestBase{
    public AT_QG046_GenerationSymmetryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG046_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-046 Generation Symmetry Architecture Audit");
        var r=GenerationSymmetryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Generation Problem");sb.AppendLine(r.SA);
        S(sb,"Section B -- Three-Generation Necessity: What Fails at N!=3");sb.AppendLine(r.SB);
        S(sb,"Section C -- S3 Symmetry Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Koide Geometry = S3 Decomposition");sb.AppendLine(r.SD);
        S(sb,"Section E -- Architecture Excitation Model");sb.AppendLine(r.SE);
        S(sb,"Section F -- Neutrino Correspondence");sb.AppendLine(r.SF);
        S(sb,"Section G -- Fourth Generation Stress Test");sb.AppendLine(r.SG);
        S(sb,"Section H -- Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG046_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
