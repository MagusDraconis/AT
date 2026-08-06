namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Determines whether the Born rule P = |ψ|² can be derived
/// from Q-network dynamics or remains an independent postulate.
///
/// TQM-153: Origin of the Born Rule
/// </summary>
public static class BornRuleAnalyzer
{
    public static string BornTheory()
    {
        return @"
ORIGIN OF THE BORN RULE

1. THE QUESTION:

   TQM-152: Q + reversibility → Schrödinger dynamics.
   But Schrödinger only gives ψ(t). Where does P = |ψ|² come from?

2. GLEASON'S THEOREM (1957):

   Any probability measure on a Hilbert space (dim ≥ 3) that is
   additive for orthogonal projectors MUST be P = Tr(ρ·E).
   For pure states: P = |⟨φ|ψ⟩|².

   This UNIQUELY selects |ψ|² from all possible probability measures.

3. ADDITIVITY TEST:

   For ψ = α|0⟩ + β|1⟩:
   P(|0⟩) + P(|1⟩) = 1 is required for any probability measure.

   P ∝ |ψ|:    |α|+|β| ≠ 1 (for most α,β) — FAILS
   P ∝ |ψ|²:   |α|²+|β|² = 1 (always) — PASSES
   P ∝ |ψ|³:   |α|³+|β|³ ≠ 1 — FAILS
   Only the exponent 2 satisfies additivity.

4. WHAT TQM ADDS:

   TQM does NOT derive the Born rule from Q.
   Gleason's theorem already uniquely selects |ψ|².
   TQM requires the Born rule as an additional postulate.

5. TQM POSTULATES (FINAL COUNT):

   1. Q exists (topological charge)
   2. Dynamics are reversible (→ Schrödinger)
   3. Born rule P = |ψ|² (→ probability interpretation)

   Three postulates for quantum mechanics from TQM.

6. NULL HYPOTHESIS: Born rule is fundamental and cannot be derived.
   H1: Born rule emerges uniquely from additivity constraints.
";
    }

    public static ProbabilityMeasure.BornRuleReport Analyze()
    {
        var candidates = MeasurementModel.EvaluateCandidates();
        var (bornUnique, reason) = MeasurementModel.TestUniqueness();

        bool derived = bornUnique; // Gleason's theorem derives it from additivity
        string motivation = "Additivity (Gleason's theorem)";

        string classification = derived ? "C: Emergent Born Rule" : "A: Born Rule Fundamental";

        string verdict = derived
            ? $"BORN RULE UNIQUELY SELECTED. {reason} "
              + $"Gleason's theorem: additivity for orthogonal projectors ⇒ P=|ψ|². "
              + $"This can be derived from additivity + basis independence. "
              + $"But 'additivity' is an additional postulate beyond Q + reversibility. "
              + $"TQM requires 3 postulates: Q, reversibility, Born rule."
            : "Born rule is fundamental.";

        return new ProbabilityMeasure.BornRuleReport(
            candidates, derived, motivation, classification, verdict);
    }

    public static string HostileReview(ProbabilityMeasure.BornRuleReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Is the Born rule truly derived?");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 1: Gleason's theorem requires additivity.");
        sb.AppendLine("  → 'Additivity for orthogonal projectors' is an ASSUMPTION.");
        sb.AppendLine("  → Why should probabilities add for orthogonal states?");
        sb.AppendLine("  → This is the classical probability axiom, not derived.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 2: Does TQM add anything to Gleason's theorem?");
        sb.AppendLine("  → NO. Gleason (1957) already proved this.");
        sb.AppendLine("  → TQM provides the Hilbert space (L_Q) but not the probability rule.");
        sb.AppendLine("  → The Born rule is mathematically MOTIVATED, not TQM-derived.");
        sb.AppendLine();
        sb.AppendLine("ATTEMPT 3: TQM postulates — final count.");
        sb.AppendLine("  → 1. Q exists (topological charge → L_Q → Hilbert space)");
        sb.AppendLine("  → 2. Dynamics are reversible (→ Schrödinger)");
        sb.AppendLine("  → 3. Born rule P=|ψ|² (→ probability)");
        sb.AppendLine("  → 3 postulates — still fewer than standard QM's ~5.");
        sb.AppendLine();
        return sb.ToString();
    }
}
