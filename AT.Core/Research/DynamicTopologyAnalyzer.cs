namespace AT.Core.Research;

/// <summary>
/// Constructs the dynamic topology phase diagram and determines
/// critical mobility thresholds for phase transitions.
///
/// AT-X003: Dynamic Topology Phase Diagram
/// </summary>
public static class DynamicTopologyAnalyzer
{
    public static string PhaseTheory()
    {
        return @"
DYNAMIC TOPOLOGY PHASE DIAGRAM

1. THE QUESTION:

   AT-X002: at μ=0.02, system stays quasi-static.
   Where does the transition to genuinely new physics occur?

2. PHASES:

   I:  Static (μ ≈ 0)        — L_Q constant, AT-117-154 valid.
   II: Quasi-Static (μ < 0.1) — Slow spectral drift, species stable.
   III: Dynamic (μ ≈ 0.1-0.5)— Changing attractors, evolving species.
   IV: Open-Ended (μ > 0.5)  — Continuous novelty, unbounded innovation.

3. CRITICAL MOBILITIES:

   μ_c1: Static → Quasi-Static (spectral drift becomes measurable).
   μ_c2: Quasi-Static → Dynamic (species identity breaks).
   μ_c3: Dynamic → Open-Ended (innovation becomes unbounded).

4. NULL HYPOTHESIS: No phase transitions exist. Dynamic effects
   are continuous corrections without qualitative change.
";
    }

    public static DynamicPhaseMetrics.PhaseDiagram Analyze(int? seed = null)
    {
        double[] mobilities = { 0.00, 0.01, 0.02, 0.05, 0.10, 0.20, 0.50, 1.00 };
        var results = TopologyPhaseDiagram.SweepMobility(mobilities, seed: seed);
        return TopologyPhaseDiagram.BuildDiagram(results);
    }

    public static string HostileReview(DynamicPhaseMetrics.PhaseDiagram diagram)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Are the phases real?");
        sb.AppendLine();
        sb.AppendLine($"  Phases: [{string.Join(" → ", diagram.Phases)}]");
        sb.AppendLine($"  Critical μ₁: {diagram.CriticalMobility1}");
        sb.AppendLine($"  Critical μ₂: {diagram.CriticalMobility2}");
        sb.AppendLine($"  Open-ended: {(diagram.OpenEndedDetected ? "DETECTED" : "NOT DETECTED")}");
        sb.AppendLine();
        if (!diagram.OpenEndedDetected)
        {
            sb.AppendLine("  At tested mobilities (μ ≤ 1.0), no open-ended regime found.");
            sb.AppendLine("  The graph stays roughly chain-like even at high mobility.");
            sb.AppendLine("  True open-ended innovation may require:");
            sb.AppendLine("    - Node addition/removal (birth/death)");
            sb.AppendLine("    - Species-driven rewiring (co-evolution)");
            sb.AppendLine("    - Larger graphs (Q > 20)");
        }
        sb.AppendLine();
        return sb.ToString();
    }
}
