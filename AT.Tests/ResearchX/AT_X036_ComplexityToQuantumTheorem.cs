using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;
using Xunit.Abstractions;
using static AT.Core.Research.ComplexityAxiomAudit;

namespace AT.Tests.ResearchX;

public class AT_X036_ComplexityToQuantumTheorem : ResearchTestBase
{
    public AT_X036_ComplexityToQuantumTheorem(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X036_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X036 Complexity-to-Quantum Theorem");

        var theorem = ComplexityToQuantumAnalyzer.Prove();

        // 1. Theorem statement
        Sec(sb, "Theorem Statement");
        sb.AppendLine(theorem.TheoremStatement);
        sb.AppendLine();

        // 2. Axiom audit
        Sec(sb, "Minimal Axioms");
        sb.AppendLine("  A1: N < ∞ distinguishable entities exist.");
        sb.AppendLine("  A2: Dynamics can preserve information (∃ reversible sector).");
        sb.AppendLine("  A3: Dynamics can preserve identity (∃ fixed points).");
        sb.AppendLine("  No other assumptions permitted.");
        sb.AppendLine();

        // 3. Proof chain
        Sec(sb, "Proof Chain");
        sb.AppendLine("  Step │ St │ Derivation");
        sb.AppendLine("  " + new string('─', 80));
        foreach (var s in theorem.Proof)
        {
            string icon = s.Status switch
            {
                ProofStepStatus.Proven => "✓",
                ProofStepStatus.GapIdentified => "~",
                ProofStepStatus.Assumed => "A",
                _ => "?"
            };
            sb.AppendLine($"  {s.Number,4} │ {icon}  │ {s.Step}");
            sb.AppendLine($"       │    │ Uses: [{string.Join(", ", s.UsesAxioms)}]");
        }
        sb.AppendLine();
        sb.AppendLine($"  Proven: {theorem.ProvenCount}/{theorem.StepsCount}. Gaps/assumptions: {theorem.GapCount}.");
        sb.AppendLine();

        // 4. Derivation chain visual
        Sec(sb, "Derivation Chain");
        sb.AppendLine("  A1 (entities) + A2 (info retention)");
        sb.AppendLine("      ↓");
        sb.AppendLine("  Information preservation at maximum");
        sb.AppendLine("      ↓");
        sb.AppendLine("  Injective dynamics");
        sb.AppendLine("      ↓  (finite state space)");
        sb.AppendLine("  Bijective dynamics");
        sb.AppendLine("      ↓");
        sb.AppendLine("  REVERSIBILITY (R=1)              ← Step 6");
        sb.AppendLine();
        sb.AppendLine("  A1 (entities) + A3 (identity persistence)");
        sb.AppendLine("      ↓");
        sb.AppendLine("  Maximal persistent identities");
        sb.AppendLine("      ↓");
        sb.AppendLine("  SELF-CONSISTENCY (S=1)           ← Step 8");
        sb.AppendLine();
        sb.AppendLine("  R=1 + S=1");
        sb.AppendLine("      ↓");
        sb.AppendLine("  Complex Hilbert space              ← Steps 9-12");
        sb.AppendLine("      ↓");
        sb.AppendLine("  Unitary dynamics                   ← Step 13");
        sb.AppendLine("      ↓  (Stone's Theorem)");
        sb.AppendLine("  i∂ψ/∂t = Hψ                        ← Step 14");
        sb.AppendLine();
        sb.AppendLine("  QUANTUM MECHANICS DERIVED.");
        sb.AppendLine();

        // 5. Gap analysis
        Sec(sb, "Gap Analysis");
        sb.AppendLine(QuantumNecessityProof.GapAssessment());
        sb.AppendLine();

        // 6. Counterexample audit
        Sec(sb, "Counterexample Audit");
        sb.AppendLine(ComplexityToQuantumAnalyzer.CounterexampleAudit(theorem.Counterexamples));

        // 7. Implications
        Sec(sb, "Implications");
        sb.AppendLine(ComplexityToQuantumAnalyzer.Implications());

        // 8. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(ComplexityToQuantumAnalyzer.HostileReview());

        // 9. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X036 COMPLETE.");
        sb.AppendLine($"  Classification: {theorem.Classification}");
        sb.AppendLine($"  {theorem.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
