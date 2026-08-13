using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG069_PredictionPriorityAudit:ResearchTestBase{
    public TQM_QG069_PredictionPriorityAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG069_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-069 Prediction Priority Audit");
        var r=PredictionPriorityAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Current Prediction Inventory");sb.AppendLine(r.SA);
        S(sb,"Section B -- Scientific-Risk Analysis (Falsification Power)");sb.AppendLine(r.SB);
        S(sb,"Section C -- Accessibility Analysis (Feasibility x Timeline)");sb.AppendLine(r.SC);
        S(sb,"Section D -- Dependency Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Prediction Ranking");sb.AppendLine(r.SE);
        S(sb,"Section F -- Optimal Testing Roadmap");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Review: Assume TQM Is Wrong");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG069_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
