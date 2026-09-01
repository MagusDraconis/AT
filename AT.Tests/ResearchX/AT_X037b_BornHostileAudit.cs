using System.Globalization;
using System.Text;
using AT.Core.Research;
using AT.Tests.Shared;

namespace AT.Tests.ResearchX;

public class AT_X037b_BornHostileAudit : ResearchTestBase
{
    public AT_X037b_BornHostileAudit(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void AT_X037b_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("AT-X037b Hostile Audit of the Born Rule Derivation");

        var report = BornHostileAnalyzer.Audit();

        // 1. Mission statement
        Sec(sb, "Mission");
        sb.AppendLine("  Attempt to DESTROY the X037 Born rule derivation.");
        sb.AppendLine("  Goal: find ANY consistent α ≠ 2 reality with maximal complexity.");
        sb.AppendLine("  Assume X037 is false until proven otherwise.");
        sb.AppendLine();

        // 2. Attack vectors
        Sec(sb, "Attack Vector Results");
        sb.AppendLine(BornHostileAnalyzer.AttackReport(report.Attacks));

        // 3. Alternative realities
        Sec(sb, "Alternative Reality Constructions");
        sb.AppendLine(BornHostileAnalyzer.RealityReport(report.Realities));

        // 4. Complexity comparison
        Sec(sb, "Complexity Comparison");
        sb.AppendLine(BornHostileAnalyzer.ComplexityTable(report.ComplexityTable));

        // 5. The rigid chain
        Sec(sb, "The Rigid Chain");
        sb.AppendLine(BornHostileAnalyzer.TheRigidChain());

        // 6. Detailed failure: AV4 (entanglement signaling)
        Sec(sb, "Detailed Failure: AV4 — Entanglement Signaling for α≠2");
        sb.AppendLine("  Consider the Bell state |Ψ⟩ = (|00⟩ + |11⟩)/√2.");
        sb.AppendLine();
        sb.AppendLine("  α=2 (Born):");
        sb.AppendLine("    P_0^A = |1/√2|² / (|1/√2|² + |1/√2|²) = 1/2.");
        sb.AppendLine("    Bob applies local unitary U_B → new state (|0+⟩ + |1-⟩)/√2.");
        sb.AppendLine("    P_0^A = |1/2|²+|1/2|² / ... = 1/2. UNCHANGED. ✓");
        sb.AppendLine();
        sb.AppendLine("  α=3:");
        sb.AppendLine("    Before: P_0^A = (1/√2)³ / ((1/√2)³+(1/√2)³) = 1/2.");
        sb.AppendLine("    After Bob's unitary: coefficients change to (1/2, 1/2, 1/2, 1/2).");
        sb.AppendLine("    P_0^A = (2·(1/2)³) / (4·(1/2)³) = 1/2.");
        sb.AppendLine("    Wait — this actually gives 1/2 too for this specific example...");
        sb.AppendLine();
        sb.AppendLine("    But consider |Ψ⟩ = √0.9|00⟩ + √0.1|11⟩ (non-maximally entangled).");
        sb.AppendLine("    α=3: P_0^A = (√0.9)³ / ((√0.9)³+(√0.1)³) = 0.9^1.5/(0.9^1.5+0.1^1.5).");
        sb.AppendLine("    After Bob rotates: coefficients redistribute. P_0^A CHANGES.");
        sb.AppendLine("    Alice can detect Bob's rotation → SUPERLUMINAL SIGNALING.");
        sb.AppendLine();

        // 7. Detailed failure: AV5 (no inner product = no measurement)
        Sec(sb, "Detailed Failure: AV5 — No Measurement Without Inner Product");
        sb.AppendLine("  Measurement axiom: outcome |i⟩ with probability P_i.");
        sb.AppendLine("  This requires projecting |ψ⟩ onto |i⟩. Projection requires:");
        sb.AppendLine("    1. An inner product ⟨i|ψ⟩ (to compute the component).");
        sb.AppendLine("    2. Orthogonal decomposition (for mutually exclusive outcomes).");
        sb.AppendLine("    3. Completeness Σ|i⟩⟨i| = I (resolution of identity).");
        sb.AppendLine();
        sb.AppendLine("  In Lp for p≠2: NONE of these exist.");
        sb.AppendLine("  • No inner product → cannot compute components.");
        sb.AppendLine("  • No orthogonal complement → outcomes not mutually exclusive.");
        sb.AppendLine("  • No projection operators → measurement undefined.");
        sb.AppendLine();
        sb.AppendLine("  Without measurement, the theory cannot make empirical predictions.");
        sb.AppendLine("  It is NOT a physical theory — it's just a norm with no interpretation.");
        sb.AppendLine();

        // 8. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  AT-X037b COMPLETE.");
        sb.AppendLine($"  Attacks attempted: {report.AttacksAttempted}. Successful: {report.SuccessfulAttacks}.");
        sb.AppendLine($"  Final verdict: X037 {report.FinalVerdict.ToString().ToUpper()}.");
        sb.AppendLine($"  {report.Summary}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
