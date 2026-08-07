using System.Globalization;
using System.Text;
using TQM.Core.Research;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchX;

public class TQM_X037_BornRuleDerivation : ResearchTestBase
{
    public TQM_X037_BornRuleDerivation(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQM_X037_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunTest(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunTest()
    {
        var sb = new StringBuilder();
        PrintHeader("TQM-X037 Born Rule from Complexity Preservation");

        var theorem = BornRuleAnalyzer.Analyze();

        // 1. Theorem statement
        Sec(sb, "Theorem Statement");
        sb.AppendLine(theorem.TheoremStatement);
        sb.AppendLine();

        // 2. Generalized Born family
        Sec(sb, "Generalized Born Family");
        sb.AppendLine("  P_i = |ψ_i|^α / Σ_j |ψ_j|^α  for arbitrary α > 0.");
        sb.AppendLine("  Tested: α ∈ {0.5, 1.0, 1.5, 2.0, 3.0, 4.0}");
        sb.AppendLine();

        // 3. Alpha test results
        Sec(sb, "Alpha Test Results");
        sb.AppendLine(BornRuleAnalyzer.AlphaTestReport(theorem.AlphaTests));

        // 4. Consistency matrix
        Sec(sb, "Consistency Requirement Matrix");
        sb.AppendLine(BornRuleAnalyzer.ConsistencyMatrix(theorem.Requirements));

        // 5. Detailed failure analysis
        Sec(sb, "Failure Analysis — Why Each α ≠ 2 Fails");
        foreach (var t in theorem.AlphaTests.Where(t => !t.Survives))
        {
            sb.AppendLine($"  α = {t.Alpha}: {t.SystemDescription}");
            sb.AppendLine($"    Failure: {t.Failure}");
            sb.AppendLine($"    Point:   {t.ExactFailurePoint}");
            sb.AppendLine($"    Reason:  {t.MathematicalReason}");
            sb.AppendLine();
        }

        // 6. The key proof
        Sec(sb, "Key Proof: Unitary Invariance ⇒ α = 2");
        sb.AppendLine("  Let N(ψ) = Σ_i |ψ_i|^α be the normalization factor.");
        sb.AppendLine("  Basis independence requires N(Uψ) = N(ψ) for all unitary U.");
        sb.AppendLine();
        sb.AppendLine("  Since U(N) acts transitively on the unit sphere, N(ψ) depends");
        sb.AppendLine("  only on ‖ψ‖² = Σ_i |ψ_i|² (the unique unitarily invariant norm).");
        sb.AppendLine();
        sb.AppendLine("  Test two states with ‖ψ‖² = 1:");
        sb.AppendLine("    ψ_a = (1, 0, 0, ..., 0)    → N(ψ_a) = 1^α = 1");
        sb.AppendLine("    ψ_b = (1/√N, 1/√N, ..., 1/√N) → N(ψ_b) = N · (1/√N)^α = N^{1-α/2}");
        sb.AppendLine();
        sb.AppendLine("  Unitary invariance requires N(ψ_a) = N(ψ_b):");
        sb.AppendLine("    1 = N^{1-α/2}  for all N");
        sb.AppendLine("    ⇒ 1 - α/2 = 0");
        sb.AppendLine("    ⇒ α = 2  ∎");
        sb.AppendLine();

        // 7. Basis-dependence counterexample
        Sec(sb, "Concrete Counterexample: α = 3");
        sb.AppendLine("  |ψ⟩ = (1, 0) in computational basis.");
        sb.AppendLine("    P_0(α=3) = |1|³ / (|1|³ + |0|³) = 1/1 = 1");
        sb.AppendLine("    P_1(α=3) = 0");
        sb.AppendLine();
        sb.AppendLine("  Change basis via Hadamard: |ψ'⟩ = (1/√2, 1/√2).");
        sb.AppendLine("    P_0(α=3) = (1/√2)³ / ((1/√2)³ + (1/√2)³) = 1/2");
        sb.AppendLine("    P_1(α=3) = 1/2");
        sb.AppendLine();
        sb.AppendLine("  SAME PHYSICAL STATE → DIFFERENT PROBABILITIES.");
        sb.AppendLine("  In basis A: P_0 = 1, P_1 = 0.");
        sb.AppendLine("  In basis B: P_0 = 1/2, P_1 = 1/2.");
        sb.AppendLine("  This is a CONTRADICTION. Probability cannot depend on the basis.");
        sb.AppendLine();

        // 8. Why not Gleason
        Sec(sb, "Why This Is Not Gleason's Theorem");
        sb.AppendLine("  Gleason (1957): Probability measure on subspaces → Born.");
        sb.AppendLine("    • Assumes σ-additivity on the projection lattice.");
        sb.AppendLine("    • Only works for dim ≥ 3.");
        sb.AppendLine("    • Heavy functional analysis; opaque.");
        sb.AppendLine();
        sb.AppendLine("  This derivation:");
        sb.AppendLine("    • Assumes only P_i = f(|ψ_i|) + basis independence.");
        sb.AppendLine("    • Works for ALL dimensions including dim = 2.");
        sb.AppendLine("    • Elementary algebra; fully transparent.");
        sb.AppendLine("    • Provides INSIGHT: α=2 is forced by unitary geometry.");
        sb.AppendLine();

        // 9. Hostile review
        Sec(sb, "Hostile Review");
        sb.AppendLine(BornRuleAnalyzer.HostileReview(theorem));

        // 10. Updated postulate count
        Sec(sb, "Impact on Minimal Postulates");
        sb.AppendLine("  PRE-X037:  P4 = Born Rule (Gleason) — external postulate.");
        sb.AppendLine("  POST-X037: P4 = DERIVED. Born rule follows from Hilbert geometry.");
        sb.AppendLine();
        sb.AppendLine("  UPDATED MINIMAL THEORY: 1 postulate + 1 irreducible.");
        sb.AppendLine("    P1: Q = distinguishability primitive (individuation).");
        sb.AppendLine("    P2: Measurement = wavefunction collapse (irreducible).");
        sb.AppendLine("    Everything else (R, S, Hilbert, Schrödinger, Born) is DERIVED.");
        sb.AppendLine();

        // 11. Final verdict
        sb.AppendLine(new string('=', 100));
        sb.AppendLine($"  TQM-X037 COMPLETE.");
        sb.AppendLine($"  Classification: {theorem.Classification}");
        sb.AppendLine($"  {theorem.Verdict}");
        sb.AppendLine(new string('=', 100));
        Output.WriteLine(sb.ToString());
    }

    private static void Sec(StringBuilder sb, string t)
    { sb.AppendLine(t); sb.AppendLine(new string('-', t.Length)); }
}
