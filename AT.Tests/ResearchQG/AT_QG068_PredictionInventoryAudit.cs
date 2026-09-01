using System.Globalization;using System.Text;using AT.Core.ResearchQG;using AT.Tests.Shared;namespace AT.Tests.ResearchQG;
public class AT_QG068_PredictionInventoryAudit:ResearchTestBase{
    public AT_QG068_PredictionInventoryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG068_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-068 AT Prediction Inventory Audit");
        var r=PredictionInventoryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Inventory Methodology (Strict Criteria)");sb.AppendLine(r.SA);
        S(sb,"Section B -- Derived Results (Structure, Not Prediction)");sb.AppendLine(r.SB);
        S(sb,"Section C -- Reinterpretations (Not Predictions)");sb.AppendLine(r.SC);
        S(sb,"Section D -- Predictions (Genuine, Risky, Specific)");sb.AppendLine(r.SD);
        S(sb,"Section E -- Negative Predictions (Prohibited Phenomena)");sb.AppendLine(r.SE);
        S(sb,"Section F -- Falsification Opportunities (Direct Tests)");sb.AppendLine(r.SF);
        S(sb,"Section G -- Open Problems (The Unresolved Residue)");sb.AppendLine(r.SG);
        S(sb,"Section H -- Scientific Scorecard");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG068_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
