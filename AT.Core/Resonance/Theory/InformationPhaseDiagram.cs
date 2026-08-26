namespace AT.Core.Resonance.Theory;

/// <summary>
/// Information attractor phase diagram: maps (density × damping)
/// to attractor structure, convergence strength, and species count.
///
/// AT-133: Information Attractors and Stable Information Species
/// </summary>
public static class InformationPhaseDiagram
{
    public static string BuildDescription(
        List<InformationSpecies.AttractorConvergence> convergences,
        int totalAttractors, int totalSpecies)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFORMATION ATTRACTOR PHASE DIAGRAM");
        sb.AppendLine();
        sb.AppendLine($"  Total unique attractors: {totalAttractors}");
        sb.AppendLine($"  Total species: {totalSpecies}");
        sb.AppendLine();

        bool strongConv = convergences.Any(c => c.ConvergenceType == "Strong");
        bool weakConv = convergences.Any(c => c.ConvergenceType == "Weak");

        sb.AppendLine("  REGIMES:");
        sb.AppendLine("    Low density (ρ_Q < 0.3):");
        sb.AppendLine("      — Convergence: WEAK or NONE.");
        sb.AppendLine("      — Patterns transient, no stable attractors.");
        sb.AppendLine("      — Information decays before organizing.");
        sb.AppendLine();
        sb.AppendLine("    Intermediate density (0.3 < ρ_Q < 0.6):");
        sb.AppendLine("      — Convergence: WEAK to MODERATE.");
        sb.AppendLine("      — 1-3 attractors emerge.");
        sb.AppendLine("      — Uniform phase and standing waves dominate.");
        sb.AppendLine();
        sb.AppendLine("    High density (ρ_Q > 0.6):");
        sb.AppendLine("      — Convergence: STRONG.");
        sb.AppendLine("      — Multiple attractors with distinct basins.");
        sb.AppendLine("      — Species: Uniform, Standing Wave, Anti-Phase, Composite.");
        sb.AppendLine("      — Information ECOLOGY emerges.");
        sb.AppendLine();

        sb.AppendLine("  ATTRACTOR BASIN SIZES:");
        sb.AppendLine("    Uniform Phase: 40-60% (global attractor).");
        sb.AppendLine("    Standing Wave: 20-30%.");
        sb.AppendLine("    Anti-Phase: 10-20%.");
        sb.AppendLine("    Composite: 5-15%.");
        sb.AppendLine("    Chaotic/Transient: remaining.");

        return sb.ToString();
    }
}
