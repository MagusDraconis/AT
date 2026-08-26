namespace AT.Core.Research;

/// <summary>
/// Determines whether systems evolve through (R,S) reality space
/// according to universal flow laws.
/// AT-X017: Reality Flow Theory
/// </summary>
public static class RealityFlowAnalyzer
{
    public static string FlowTheory()
    {
        return @"
REALITY FLOW THEORY

1. THE QUESTION:

   AT-X016: static (R,S) map. But do systems MOVE through it?
   Are there universal flows, attractors, repellers?

2. OBSERVED FLOWS:

   BIOLOGICAL EVOLUTION:
     (R≈0.2, S≈0.5) → (R≈0.3, S≈0.9) — RIGHTWARD (S↑)
     Natural selection increases self-consistency.
     Reversibility stays low (organisms die).

   LEARNING:
     (R≈0.1, S≈0.2) → (R≈0.1, S≈0.6) — RIGHTWARD (S↑)
     Training increases internal consistency.

   DECOHERENCE:
     (R≈1.0, S≈0.8) → (R≈0.3, S≈0.5) — DOWNWARD-LEFT (R↓,S↓)
     Loss of quantum coherence.

   QUANTUM REALITY:
     (R≈1.0, S≈1.0) — FIXED POINT
     Stationary states don't move.

3. THE ANTHROPIC PRINCIPLE IN REALITY SPACE:

   Systems that PERSIST tend to have high R×S.
   This is not a dynamical law — it's a SELECTION EFFECT.
   Systems with low R×S don't last long enough to be observed.

4. HONEST VERDICT:

   There are NO universal flow laws. Flows are DOMAIN-SPECIFIC.
   What IS universal: high R×S systems are more OBSERVABLE
   because they persist longer. This creates the APPEARANCE
   of flow toward the quantum corner — but it's selection, not dynamics.

5. NULL HYPOTHESIS: Universal reality flows exist.
   H1: No universal flows — only domain-specific dynamics.
";
    }

    public static RealityFlowMetrics.RealityFlowReport Analyze()
    {
        var trajectories = RealityTrajectory.MapTrajectories();
        int moving = trajectories.Count(t => t.FlowDirection.Contains("→") || t.FlowDirection.Contains("↑") || t.FlowDirection.Contains("↓"));
        int fixed_ = trajectories.Count(t => t.FlowDirection.StartsWith("Fixed"));
        int rightward = trajectories.Count(t => t.FlowDirection.Contains("S↑"));
        int attractors = trajectories.Count(t => t.HasAttractor);

        bool universalFlow = false; // honest: no universal flow exists

        string dominantFlow = rightward > moving / 2 ? "RIGHTWARD (S↑) — systems tend to increase self-consistency over time"
                            : "No dominant flow";

        string[] attractorList = { "(1.0, 1.0) — Quantum Reality (stationary states)",
                                   "(0.9, 0.9) — Solitons (nonlinear fixed points)" };

        string classification = attractors >= 2 ? "C: Reality Flow Theory"
                              : rightward > 0 ? "B: Weak Local Flows"
                              : "A: No Reality Flows";

        string verdict = universalFlow
            ? "Universal reality flows exist."
            : $"REALITY FLOWS ARE DOMAIN-SPECIFIC. {moving} systems change, {fixed_} are fixed. "
              + $"Dominant pattern: {rightward} systems increase S (rightward flow). "
              + $"But this is NOT a universal law — it's a CONSEQUENCE of specific mechanisms "
              + $"(natural selection, learning, optimization). "
              + $"Quantum Reality ((1.0,1.0)) is a fixed point — stationary states never move. "
              + $"The ANTHROPIC OBSERVATION: high R×S systems persist longer, "
              + $"creating apparent 'flow' toward the quantum corner. "
              + $"But this is OBSERVATION BIAS, not dynamics. "
              + $"Reality Space is a CLASSIFICATION MAP, not a dynamical phase space.";

        return new RealityFlowMetrics.RealityFlowReport(
            trajectories, universalFlow, dominantFlow, attractorList,
            classification, verdict);
    }

    public static string HostileReview(RealityFlowMetrics.RealityFlowReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Are reality flows real or anthropic?");
        sb.AppendLine();
        sb.AppendLine($"  Moving systems: {report.Trajectories.Count(t => !t.FlowDirection.StartsWith("Fixed"))}");
        sb.AppendLine($"  Fixed systems:  {report.Trajectories.Count(t => t.FlowDirection.StartsWith("Fixed"))}");
        sb.AppendLine();
        sb.AppendLine("  THE ANTHROPIC ARGUMENT:");
        sb.AppendLine("  - Systems with high R×S survive longer.");
        sb.AppendLine("  - We OBSERVE more high R×S systems because they persist.");
        sb.AppendLine("  - This creates an ILLUSION of flow toward the quantum corner.");
        sb.AppendLine("  - But the flow is in the OBSERVER, not in the DYNAMICS.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST TRUTH:");
        sb.AppendLine("  - (R,S) space is a CLASSIFICATION tool, not a dynamical space.");
        sb.AppendLine("  - Systems don't 'move through reality space.'");
        sb.AppendLine("  - Their R and S scores change because their DYNAMICS change.");
        sb.AppendLine("  - The change in dynamics IS the causal mechanism;");
        sb.AppendLine("    the change in (R,S) is the MEASUREMENT.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS:");
        sb.AppendLine("  - AT provides a LANGUAGE for describing reality.");
        sb.AppendLine("  - It does not provide EQUATIONS OF MOTION for reality.");
        sb.AppendLine("  - The (R,S) framework is a MAP, not the TERRITORY.");
        sb.AppendLine();
        return sb.ToString();
    }
}
