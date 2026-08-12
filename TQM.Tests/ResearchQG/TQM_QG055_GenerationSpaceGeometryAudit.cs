using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG055_GenerationSpaceGeometryAudit:ResearchTestBase{
    public TQM_QG055_GenerationSpaceGeometryAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG055_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-055 Generation Space Geometry Audit");
        var r=GenerationSpaceGeometryAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Why the Geometry of G Matters");sb.AppendLine(r.SA);
        S(sb,"Section B -- Euclidean R^3 Analysis");sb.AppendLine(r.SB);
        S(sb,"Section C -- Simplex Analysis: Generations as Triangle Vertices");sb.AppendLine(r.SC);
        S(sb,"Section D -- Spherical S^2 Analysis");sb.AppendLine(r.SD);
        S(sb,"Section E -- Mixing Interpretation: U(3) Rotations");sb.AppendLine(r.SE);
        S(sb,"Section F -- Koide Interpretation: 45-Degree and Participation Ratio");sb.AppendLine(r.SF);
        S(sb,"Section G -- Geometry Comparison Matrix");sb.AppendLine(r.SG);
        S(sb,"Section H -- Hostile Review");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG055_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
