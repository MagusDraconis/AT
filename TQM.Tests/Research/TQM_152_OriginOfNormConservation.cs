using System.Globalization;
using System.Text;
using TQM.Core.Resonance.Theory;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.Research;

public class TQM_152_OriginOfNormConservation : ResearchTestBase
{
    public TQM_152_OriginOfNormConservation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_152_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-152 Origin of Norm Conservation");

        Sec(sb, "0. Assumptions");
        sb.AppendLine("  1. TQM-151: Q + norm conservation → Schrödinger.");
        sb.AppendLine("  2. Attempt to derive norm conservation from deeper principles.");
        sb.AppendLine("  3. Assume norm conservation is fundamental until derived.");
        sb.AppendLine();

        Sec(sb, "1. Norm Conservation Theory");
        sb.AppendLine(NormConservationAnalyzer.NormTheory());
        sb.AppendLine();

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = NormConservationAnalyzer.Analyze();
        sw.Stop();
        sb.AppendLine($"  Analysis: {sw.Elapsed.TotalMilliseconds:F0}ms");

        Sec(sb, "2. Candidate Origins of Norm Conservation");
        sb.AppendLine("  Hypothesis                │ Conserves? │ Reducible? │ Assessment");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var o in report.Origins)
            sb.AppendLine($"  {o.Hypothesis,-25} │ {(o.PredictsConservation ? "✓" : "✗"),-10} │ {(o.Reducible ? "✓" : "✗"),-10} │ {o.Assessment}");
        sb.AppendLine();

        Sec(sb, "3. The Mathematical Equivalence");
        sb.AppendLine("  Reversible ⇔ Unitary ⇔ Norm-conserving ⇔ Anti-Hermitian");
        sb.AppendLine("  These four statements ARE each other. None can derive another.");
        sb.AppendLine();
        sb.AppendLine("  Diffusion (∂u/∂t = -L_Q u): norm DECAYS — NOT reversible.");
        sb.AppendLine("  Schrödinger (i∂ψ/∂t = L_Q ψ): norm CONSERVED — reversible.");
        sb.AppendLine("  The difference is the dynamics type, not the Hilbert space.");
        sb.AppendLine();

        Sec(sb, "4. TQM vs Standard Quantum Mechanics");
        sb.AppendLine("  Standard QM postulates:");
        sb.AppendLine("    1. Hilbert space");
        sb.AppendLine("    2. Observables = Hermitian operators");
        sb.AppendLine("    3. Schrödinger equation");
        sb.AppendLine("    4. Born rule (probability = |ψ|²)");
        sb.AppendLine("    5. Measurement postulate");
        sb.AppendLine();
        sb.AppendLine("  TQM postulates:");
        sb.AppendLine("    1. Q exists (topological charge)");
        sb.AppendLine("    2. Dynamics are reversible");
        sb.AppendLine();
        sb.AppendLine("  TQM DERIVES: Hilbert space (L_Q), operators (L_Q), Schrödinger (from reversibility).");
        sb.AppendLine("  Born rule and measurement remain external.");
        sb.AppendLine();
        sb.AppendLine($"  Irreducible postulates: {report.IrreduciblePostulates}");
        sb.AppendLine();

        Sec(sb, "5. Hostile Review");
        sb.AppendLine(NormConservationAnalyzer.HostileReview(report));
        sb.AppendLine();

        Sec(sb, "6. Classification");
        sb.AppendLine($"  {report.Classification}");
        sb.AppendLine($"  {report.Verdict}");
        sb.AppendLine();

        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-152 complete. Classification: {report.Classification}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
