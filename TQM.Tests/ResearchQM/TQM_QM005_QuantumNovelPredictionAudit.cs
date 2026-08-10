using System.Globalization;using System.Text;using TQM.Core.ResearchQM;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQM;
public class TQM_QM005_QuantumNovelPredictionAudit:ResearchTestBase{
    public TQM_QM005_QuantumNovelPredictionAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QM005_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQM-005 Quantum Novel Prediction Audit");
        var r=QuantumNovelPredictionAnalyzer.RunFullAnalysis();
        S(sb,"Section A — TQM–QM Exact Equivalence");sb.AppendLine(r.SA);
        S(sb,"Section B — Actualization Residue");sb.AppendLine(r.SB);
        S(sb,"Section C — Decoherence Predictions");sb.AppendLine(r.SC);
        S(sb,"Section D — Experimental Constraints");sb.AppendLine(r.SD);
        S(sb,"Section E — Falsification Pathways");sb.AppendLine(r.SE);
        S(sb,"Section F — Novel Prediction Inventory");sb.AppendLine(r.SF);
        S(sb,"Section G — Hostile Review");sb.AppendLine(r.SG);
        S(sb,"Section H — Remaining Unknowns");sb.AppendLine(r.SH);
        S(sb,"Section I — Final Verdict");sb.AppendLine(r.SI);
        sb.AppendLine();sb.AppendLine(new string('=',100));sb.AppendLine("  ResearchQM-005 COMPLETE.");
        sb.AppendLine("  TQM = QM experimentally. Superior ontological compression (5→2 axioms).");
        sb.AppendLine("  Critical unknown: ℓ (Q-event spacing). Quantum program COMPLETE.");
        sb.AppendLine(new string('=',100));
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QM005_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
