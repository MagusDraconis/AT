using System.Globalization;
using System.Text;
using AT.Core.Resonance.Theory;
using AT.Tests.Shared;

namespace AT.Tests.Research;

public class AT_149_SchrodingerCorrespondence : ResearchTestBase
{
    public AT_149_SchrodingerCorrespondence(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_149_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-149 Emergence of Schrödinger Dynamics from Q Networks");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. L_Q = graph Laplacian of Q interactions (AT-142).");
        sb.AppendLine("  2. Assume Schrödinger dynamics do NOT emerge until demonstrated.");
        sb.AppendLine("  3. The factor 'i' in i∂ψ/∂t is a manual choice, not derived.");
        sb.AppendLine();

        Sec(sb, "1. Quantum Correspondence Theory");
        sb.AppendLine(QuantumCorrespondenceAnalyzer.QuantumTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = QuantumCorrespondenceAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Dynamical Models on L_Q");
        sb.AppendLine("  Model         │ Equation          │ Norm │ Phase │ Interf. │ Stat. States │ Class");
        sb.AppendLine("  " + new string('─', 85));
        foreach (var m in report.Models)
            sb.AppendLine($"  {m.Model,-13} │ {m.Equation,-17} │ {(m.NormConserved ? "✓" : "✗"),-4} │ {(m.PhaseEvolves ? "✓" : "✗"),-5} │ {(m.InterferencePossible ? "✓" : "✗"),-8} │ {(m.StationaryStatesExist ? "✓" : "✗"),-12} │ {m.DynamicsClass}");
        sb.AppendLine();

        Sec(sb, "3. Unitary Evolution Test");
        sb.AppendLine($"  Unitary evolution demonstrated: {(report.UnitaryEvolutionPossible ? "YES" : "NO")}");
        sb.AppendLine($"  Continuum limit → Schrödinger: {(report.ContinuumLimitIsSchrodinger ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine("  L_Q eigenmodes = stationary states with energy λ_k.");
        sb.AppendLine("  Superposition → interference (quantum-like behavior).");
        sb.AppendLine("  BUT: i is manual. L_Q also supports ∂u/∂t = -L_Q u (diffusion).");
        sb.AppendLine();

        Sec(sb, "4. Honest Assessment");
        sb.AppendLine("  AT provides the HILBERT SPACE (L_Q eigenmodes as basis).");
        sb.AppendLine("  AT provides the HAMILTONIAN (H = L_Q).");
        sb.AppendLine("  AT does NOT derive i — the Schrödinger form is a CHOICE.");
        sb.AppendLine("  Without i: classical diffusion or waves.");
        sb.AppendLine("  With i: unitary quantum-like evolution.");
        sb.AppendLine("  The STRUCTURE for quantum mechanics exists in L_Q.");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(QuantumCorrespondenceAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-149 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
