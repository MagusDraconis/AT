using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG070_RAREvolutionPrecisionAudit:ResearchTestBase{
    public TQM_QG070_RAREvolutionPrecisionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG070_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-070 RAR Evolution Precision Audit");
        var r=RAREvolutionPrecisionAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- RAR Prediction Recap");sb.AppendLine(r.SA);
        S(sb,"Section B -- Cosmological Evolution Model (H(z))");sb.AppendLine(r.SB);
        S(sb,"Section C -- Predicted g-dagger(z) Table (Numerical)");sb.AppendLine(r.SC);
        S(sb,"Section D -- MOND Comparison: Constant a0 vs Evolving g-dagger");sb.AppendLine(r.SD);
        S(sb,"Section E -- LCDM Comparison: No RAR at All");sb.AppendLine(r.SE);
        S(sb,"Section F -- Observational Feasibility");sb.AppendLine(r.SF);
        S(sb,"Section G -- Falsification Threshold Analysis");sb.AppendLine(r.SG);
        S(sb,"Section H -- Experimental Roadmap (Precision)");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG070_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
