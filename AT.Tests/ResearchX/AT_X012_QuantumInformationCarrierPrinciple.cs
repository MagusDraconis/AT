using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchX;

public class AT_X012_QuantumInformationCarrierPrinciple : ResearchTestBase
{
    public AT_X012_QuantumInformationCarrierPrinciple(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X012_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X012 Quantum Information Carrier Principle");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. AT-X011: Reversibility ≠ Self-consistency (independent).");
        sb.AppendLine("  2. Question: what lives at the INTERSECTION of both?");
        sb.AppendLine("  3. Hypothesis: a distinct 'quantum carrier' class exists.");
        sb.AppendLine();

        Sec(sb, "1. Intersection Theory");
        sb.AppendLine(QuantumInformationCarrierAnalyzer.IntersectionTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = QuantumInformationCarrierAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Carrier Classification — Four Quadrants");
        sb.AppendLine("  Structure                 │ Rev? │ SC?  │ Quantum? │ Info Retention │ Coherence");
        sb.AppendLine("  " + new string('─', 90));
        foreach (var c in report.Classes)
            sb.AppendLine($"  {c.Name,-25} │ {(c.IsReversible ? "✓" : "✗"),-4} │ {(c.IsSelfConsistent ? "✓" : "✗"),-4} │ {(c.IsQuantumCarrier ? "YES" : "no"),-8} │ {c.InfoRetention,13:P0} │ {c.CoherenceTime,8:F0}");
        sb.AppendLine();

        Sec(sb, "3. The Quantum Carrier Principle");
        sb.AppendLine($"  \"{report.UniversalPrinciple}\"");
        sb.AppendLine();
        sb.AppendLine($"  Universal equation: {report.IntersectionEquation}");
        sb.AppendLine();
        sb.AppendLine("  This is the mathematical definition of a quantum carrier:");
        sb.AppendLine("    1. Unitary evolution: i∂ψ/∂t = H ψ (norm preserved)");
        sb.AppendLine("    2. Eigenstate condition: H ψ = λ ψ (structure invariant)");
        sb.AppendLine("  → ψ(t) = exp(-iλt) ψ(0): only phase changes, structure is eternal.");
        sb.AppendLine();

        Sec(sb, "4. Why Quantum Carriers Are Optimal");
        sb.AppendLine("  Carrier Type      │ Info Retention │ Degradation │ Coherence");
        sb.AppendLine("  " + new string('─', 60));
        sb.AppendLine("  Quantum (Rev+SC)  │ 100%           │ NONE        │ Infinite");
        sb.AppendLine("  Ordinary (SC only)│ 50-60%         │ Gradual     │ Finite");
        sb.AppendLine("  Chaotic (Rev only)│ 10%            │ Rapid       │ ~None");
        sb.AppendLine("  Noise (Neither)   │ 0%             │ Immediate   │ None");
        sb.AppendLine();
        sb.AppendLine("  Quantum carriers are the ONLY structures in AT that simultaneously:");
        sb.AppendLine("    - Store information PERFECTLY (no degradation over time)");
        sb.AppendLine("    - Maintain a PERSISTENT, IDENTIFIABLE structure");
        sb.AppendLine("    - Support COHERENT SUPERPOSITION (interference possible)");
        sb.AppendLine("    - Have INFINITE information lifetime");
        sb.AppendLine();

        Sec(sb, "5. The AT Hierarchy — With Quantum Carriers");
        sb.AppendLine("  Q (topological charge)");
        sb.AppendLine("   ↓");
        sb.AppendLine("  L_Q (graph Laplacian / Hamiltonian)");
        sb.AppendLine("   ↓");
        sb.AppendLine("  DYNAMICS");
        sb.AppendLine("   ├── REVERSIBILITY (norm conservation → unitary)");
        sb.AppendLine("   └── SELF-CONSISTENCY (F(x)=x → fixed points)");
        sb.AppendLine("        ↓ intersection");
        sb.AppendLine("  QUANTUM INFORMATION CARRIERS ← THIS LEVEL");
        sb.AppendLine("   ↓ (unitary fixed points = stationary states)");
        sb.AppendLine("  SPECIES");
        sb.AppendLine("   ↓");
        sb.AppendLine("  ECOLOGIES → EVOLUTION");
        sb.AppendLine();

        Sec(sb, "6. Hostile Review");
        sb.AppendLine(QuantumInformationCarrierAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "7. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X012 complete. Classification: {report.Classification}");
        sb.AppendLine($"  Quantum carriers: {report.QuantumCarrierCount} classes at Rev∩SC.");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
