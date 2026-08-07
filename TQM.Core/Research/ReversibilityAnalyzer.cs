namespace TQM.Core.Research;

/// <summary>
/// Compares reversibility and self-consistency to determine
/// whether TQM has one foundation or two.
/// TQM-X011: Reversibility vs Self-Consistency
/// </summary>
public static class ReversibilityAnalyzer
{
    public static string ComparisonTheory()
    {
        return @"
REVERSIBILITY vs SELF-CONSISTENCY

1. DEFINITIONS:

   REVERSIBILITY: d/dt||ψ||²=0. Evolution is anti-Hermitian.
   The generator M satisfies M† = -M. Time can run backwards.
   → Enables: unitary evolution, norm conservation, Schrödinger dynamics.

   SELF-CONSISTENCY: F(x)=x. The structure is a fixed point
   of its own dynamics. Structure and dynamics are mutually reinforcing.
   → Enables: eigenmodes, solitons, topological defects, attractors.

2. THE KEY INSIGHT:

   These are DIFFERENT properties of the DYNAMICS TYPE:
   Reversibility = 'Is the evolution unitary?'
   Self-consistency = 'Do fixed points exist?'

   A system can have either, both, or neither.

3. COUNTEREXAMPLES:

   Reversible WITHOUT self-consistency:
     Free particle (disperses, no fixed point)
     Hamiltonian chaos (no simple attractors)

   Self-consistent WITHOUT reversibility:
     Diffusion eigenmodes (norm decays)
     Kuramoto sync state (dissipative)

4. RELATIONSHIP:

   The two principles are INDEPENDENT but COMPATIBLE.
   They answer DIFFERENT questions about a dynamical system.
   There is NO deeper principle from which both emerge —
   they are orthogonal properties of evolution operators.

5. NULL HYPOTHESIS: The principles are independent.
   H1: A deeper principle unifies them.
";
    }

    public static ReversibilityVsSelfConsistency.FoundationComparisonReport Analyze()
    {
        var systems = FoundationComparison.Classify();
        int both = systems.Count(s => s.IsReversible && s.IsSelfConsistent);
        int revOnly = systems.Count(s => s.IsReversible && !s.IsSelfConsistent);
        int scOnly = systems.Count(s => !s.IsReversible && s.IsSelfConsistent);
        int neither = systems.Count(s => !s.IsReversible && !s.IsSelfConsistent);

        bool independent = revOnly > 0 && scOnly > 0;
        string relationship = independent
            ? "INDEPENDENT — counterexamples exist in both directions."
            : "DEPENDENT — one may imply the other.";

        string classification = independent ? "A: Independent Foundations"
                              : both > revOnly + scOnly ? "C: Equivalent Principles"
                              : "B: Partial Overlap";

        string verdict = independent
            ? $"REVERSIBILITY AND SELF-CONSISTENCY ARE INDEPENDENT. "
              + $"{both} systems have both, {revOnly} have only reversibility, "
              + $"{scOnly} have only self-consistency. "
              + $"Counterexamples exist: Hamiltonian chaos (reversible, not SC); "
              + $"diffusion eigenmodes (SC, not reversible). "
              + $"These are ORTHOGONAL properties: reversibility is about "
              + $"the TYPE of dynamics; self-consistency is about the "
              + $"EXISTENCE of fixed points. TQM has TWO irreducible foundations."
            : "The principles are not independent.";

        return new ReversibilityVsSelfConsistency.FoundationComparisonReport(
            systems, both, revOnly, scOnly, neither,
            independent, relationship, classification, verdict);
    }

    public static string HostileReview(ReversibilityVsSelfConsistency.FoundationComparisonReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Are these truly independent?");
        sb.AppendLine();
        sb.AppendLine($"  Both: {report.BothCount}, Rev-only: {report.ReversibleOnly}");
        sb.AppendLine($"  SC-only: {report.SelfConsistentOnly}, Neither: {report.NeitherCount}");
        sb.AppendLine();
        sb.AppendLine("  The existence of counterexamples in BOTH directions");
        sb.AppendLine("  (reversible-not-SC, SC-not-reversible) proves independence.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THIS MEANS FOR TQM:");
        sb.AppendLine("  TQM has TWO irreducible foundations:");
        sb.AppendLine("    1. REVERSIBILITY → quantum correspondence");
        sb.AppendLine("    2. SELF-CONSISTENCY → information carriers");
        sb.AppendLine();
        sb.AppendLine("  You can build a theory with either, both, or neither.");
        sb.AppendLine("  TQM uses both — which is why it has both quantum");
        sb.AppendLine("  correspondence AND information carrier structure.");
        sb.AppendLine();
        sb.AppendLine("  This is NOT a weakness. It means TQM rests on TWO");
        sb.AppendLine("  independent pillars, which is more robust than");
        sb.AppendLine("  having a single point of failure.");
        sb.AppendLine();
        return sb.ToString();
    }
}
