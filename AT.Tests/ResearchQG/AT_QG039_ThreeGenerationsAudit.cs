using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;using Xunit.Abstractions;

namespace AT.Tests.ResearchQG;
public class AT_QG039_ThreeGenerationsAudit:ResearchTestBase{
    public AT_QG039_ThreeGenerationsAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG039_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-039 Three Generations Quantitative Audit");
        var r=GenerationSpectrumAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- The Three-Generation Mystery");sb.AppendLine(r.SA);
        S(sb,"Section B -- Lepton Excitation Spectrum");sb.AppendLine(r.SB);
        S(sb,"Section C -- Frequency Quantization");sb.AppendLine(r.SC);
        S(sb,"Section D -- Fourth Generation: Excluded");sb.AppendLine(r.SD);
        S(sb,"Section E -- Neutrino Generation Mapping");sb.AppendLine(r.SE);
        S(sb,"Section F -- Hostile Spectrum Audit");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H -- Final Verdict");sb.AppendLine(r.SH);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG039_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
