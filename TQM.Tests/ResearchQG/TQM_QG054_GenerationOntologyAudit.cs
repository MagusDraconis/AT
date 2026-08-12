using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG054_GenerationOntologyAudit:ResearchTestBase{
    public TQM_QG054_GenerationOntologyAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG054_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-054 Generation Space Ontology Audit");
        var r=GenerationOntologyAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- What Is G Ontologically?");sb.AppendLine(r.SA);
        S(sb,"Section B -- What G Stores (and What It Doesn't)");sb.AppendLine(r.SB);
        S(sb,"Section C -- Can G Emerge from Existing TQM Primitives?");sb.AppendLine(r.SC);
        S(sb,"Section D -- Mixing: The Decisive Evidence G Is Real");sb.AppendLine(r.SD);
        S(sb,"Section E -- Ontology Interpretations (Evaluated)");sb.AppendLine(r.SE);
        S(sb,"Section F -- Actualization and Architecture Interpretations");sb.AppendLine(r.SF);
        S(sb,"Section G -- Hostile Elimination Audit");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG054_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
