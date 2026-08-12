using System.Globalization;using System.Text;using TQM.Core.ResearchQG;using TQM.Tests.Shared;using Xunit.Abstractions;

namespace TQM.Tests.ResearchQG;
public class TQM_QG056_YukawaEigenstructureAudit:ResearchTestBase{
    public TQM_QG056_YukawaEigenstructureAudit(ITestOutputHelper o):base(o){}
    [Fact]public void QG056_Run(){
        Thread.CurrentThread.CurrentCulture=CultureInfo.InvariantCulture;
        var sb=new StringBuilder();PrintHeader("ResearchQG-056 Yukawa Eigenstructure Audit");
        var r=YukawaEigenstructureAnalyzer.RunFullAnalysis();
        S(sb,"Section A -- Yukawa Matrices as Operators on G");sb.AppendLine(r.SA);
        S(sb,"Section B -- Generation-Space Spectral Structure");sb.AppendLine(r.SB);
        S(sb,"Section C -- Eigenvalue Hierarchy Analysis");sb.AppendLine(r.SC);
        S(sb,"Section D -- Eigenvector Geometry (Mixing Bases)");sb.AppendLine(r.SD);
        S(sb,"Section E -- Koide Eigenstructure: Eigenvalues, Not Eigenvectors");sb.AppendLine(r.SE);
        S(sb,"Section F -- CKM/PMNS as Misalignment");sb.AppendLine(r.SF);
        S(sb,"Section G -- Random-Spectrum Stress Test");sb.AppendLine(r.SG);
        S(sb,"Section H -- Implications for TQM");sb.AppendLine(r.SH);
        S(sb,"Section I -- Final Verdict");sb.AppendLine(r.SI);
        Output.WriteLine(sb.ToString());
        File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"QG056_Report.txt"),sb.ToString());
    }
    static void S(StringBuilder sb,string t){sb.AppendLine();sb.AppendLine(t);sb.AppendLine(new string('-',t.Length));}
}
