using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_150_OriginOfImaginaryUnit : ResearchTestBase
{
    public TQM_150_OriginOfImaginaryUnit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_150_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-150 Origin of the Imaginary Unit");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. L_Q supports Schrödinger form i∂ψ/∂t = L_Q ψ (TQM-149).");
        sb.AppendLine("  2. i was manually introduced. Can it emerge from real dynamics?");
        sb.AppendLine("  3. Assume i is fundamental until emergence is demonstrated.");
        sb.AppendLine();

        Sec(sb, "1. Origin Theory");
        sb.AppendLine(ImaginaryUnitOriginAnalyzer.OriginTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = ImaginaryUnitOriginAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Real Coupled Systems on L_Q");
        sb.AppendLine("  System                  │ Equations                    │ ≡Schröd? │ Norm? │ Mechanism");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var s in report.Systems)
            sb.AppendLine($"  {s.Name,-23} │ {s.Equations,-29} │ {(s.EquivalentToSchrodinger ? "✓" : "✗"),-9} │ {(s.NormConserved ? "✓" : "✗"),-5} │ {s.Mechanism}");
        sb.AppendLine();

        Sec(sb, "3. The Real-Form Equivalence");
        sb.AppendLine("  ψ = u + iv, with u,v REAL.");
        sb.AppendLine("  ∂u/∂t = L_Q v  +  ∂v/∂t = -L_Q u  ⇔  i∂ψ/∂t = L_Q ψ");
        sb.AppendLine();
        sb.AppendLine("  Proof:");
        sb.AppendLine("    i∂ψ/∂t = i(∂u/∂t + i∂v/∂t) = i∂u/∂t - ∂v/∂t");
        sb.AppendLine("           = i(L_Q v) - (-L_Q u) = L_Q u + iL_Q v = L_Q ψ ✓");
        sb.AppendLine();
        sb.AppendLine("  The imaginary unit i = J = [[0,1],[-1,0]] (90° rotation).");
        sb.AppendLine("  Complex structure = antisymmetric coupling of two real fields.");
        sb.AppendLine();

        Sec(sb, "4. What Is Derived and What Is Not");
        sb.AppendLine("  DERIVED:    i AS a matrix representation from antisymmetric coupling.");
        sb.AppendLine("  NOT DERIVED: WHY the coupling is antisymmetric.");
        sb.AppendLine();
        sb.AppendLine("  L_Q is symmetric (L_Q^T = L_Q).");
        sb.AppendLine("  Coupling type (symmetric vs antisymmetric) is an INDEPENDENT choice.");
        sb.AppendLine("  Symmetric → diffusion. Antisymmetric → Schrödinger/wave.");
        sb.AppendLine("  TQM provides Hilbert space + Hamiltonian, not the dynamical postulate.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(ImaginaryUnitOriginAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-150 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
