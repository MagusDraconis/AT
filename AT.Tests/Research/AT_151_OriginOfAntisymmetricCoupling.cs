using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.Research;

public class AT_151_OriginOfAntisymmetricCoupling : ResearchTestBase
{
    public AT_151_OriginOfAntisymmetricCoupling(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_151_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-151 Origin of the Antisymmetric Coupling");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. J = [[0,1],[-1,0]] was assumed in AT-150.");
        sb.AppendLine("  2. Attempt to derive J from deeper principles.");
        sb.AppendLine("  3. Assume J is fundamental until derived.");
        sb.AppendLine();

        Sec(sb, "1. Coupling Origin Theory");
        sb.AppendLine(AntisymmetricCouplingAnalyzer.CouplingTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = AntisymmetricCouplingAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Candidate Origins of J");
        sb.AppendLine("  Hypothesis              │ Produces J? │ Conserves Norm? │ Assessment");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var o in report.Origins)
            sb.AppendLine($"  {o.Hypothesis,-23} │ {(o.ProducesJ ? "✓" : "✗"),-11} │ {(o.ConservesNorm ? "✓" : "✗"),-15} │ {o.Assessment}");
        sb.AppendLine();

        Sec(sb, "3. The Norm Conservation Derivation");
        sb.AppendLine("  d/dt(u²+v²) = 0 ⇒ M^T = -M (antisymmetry)");
        sb.AppendLine("  Simplest 2×2 antisymmetric = J = [[0,1],[-1,0]]");
        sb.AppendLine("  Combined with L_Q: M = J ⊗ L_Q → Schrödinger.");
        sb.AppendLine();
        sb.AppendLine($"  Best derivation: {report.BestDerivation}");
        sb.AppendLine($"  J derived: {(report.JDerived ? "YES" : "NO")}");
        sb.AppendLine();

        Sec(sb, "4. The Minimal Postulate Chain");
        sb.AppendLine("  Q (topological charge) → GRAPH TOPOLOGY → L_Q (Hilbert space)");
        sb.AppendLine("  + NORM CONSERVATION → J (antisymmetric coupling) → i → Schrödinger");
        sb.AppendLine();
        sb.AppendLine("  TWO postulates remain irreducible:");
        sb.AppendLine("    1. Q exists (topological charge)");
        sb.AppendLine("    2. Norm is conserved (probability interpretation)");
        sb.AppendLine("  Everything else follows: L_Q, J, i, Schrödinger, quantum mechanics.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(AntisymmetricCouplingAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-151 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
