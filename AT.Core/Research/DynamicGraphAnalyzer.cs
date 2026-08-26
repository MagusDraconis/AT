namespace AT.Core.Research;

/// <summary>
/// Analyzes dynamic Q-charge graphs to determine whether
/// the AT framework survives time-dependent L_Q(t).
///
/// AT-X002: Dynamic Graph Physics
/// </summary>
public static class DynamicGraphAnalyzer
{
    public static string DynamicTheory()
    {
        return @"
DYNAMIC GRAPH PHYSICS

1. THE QUESTION:

   All AT (117-154) assumed L_Q = constant.
   AT-X001 identified this as the highest-value unexplored direction.
   What happens when L_Q(t) changes over time?

2. MECHANISM:

   Q charges undergo Brownian motion.
   Graph edges form/dissolve based on proximity.
   L_Q(t) changes → spectrum drifts → eigenmodes evolve.
   Species are no longer fixed — they emerge, drift, and die.

3. PREDICTIONS:

   If species count SATURATES → static graph AT survives.
   If species count GROWS → OPEN-ENDED innovation possible.
   If spectrum is UNSTABLE → quantum correspondence breaks.

4. NULL HYPOTHESIS:

   H0: Dynamic graphs produce no qualitatively new phenomena.
       Innovation remains bounded, spectrum is quasi-static.

   H1: Dynamic graphs enable open-ended innovation,
       qualitatively new species, and spectral evolution.
";
    }

    public static GraphEvolutionMetrics.DynamicGraphReport Analyze(int? seed = null)
    {
        int Q = 20;
        int gens = 500;
        double mobility = 0.02;
        double range = 0.25;

        var history = DynamicGraphModel.Simulate(Q, gens, mobility, range, seed);

        int initSpecies = history.First().UniqueSpeciesCount;
        int finalSpecies = history.Last().UniqueSpeciesCount;

        // Innovation rate: new species per generation.
        double innovRate = gens > 0 ? (double)(finalSpecies - initSpecies) / gens : 0;

        // Saturation check: compare early vs late species discovery rate.
        var early = history.Take(gens / 3).ToList();
        var late = history.Skip(2 * gens / 3).ToList();
        double earlyRate = early.Count > 1
            ? (double)(early.Last().UniqueSpeciesCount - early.First().UniqueSpeciesCount)
              / (early.Last().TimeStep - early.First().TimeStep + 1) : 0;
        double lateRate = late.Count > 1
            ? (double)(late.Last().UniqueSpeciesCount - late.First().UniqueSpeciesCount)
              / (late.Last().TimeStep - late.First().TimeStep + 1) : 0;

        bool saturated = lateRate < earlyRate * 0.3 && lateRate >= 0;
        bool openEnded = !saturated && innovRate > 0.001;

        // Spectrum stability: mean drift over time.
        double meanDrift = history.Skip(1).Average(h => h.SpectralDrift);
        bool spectrumStable = meanDrift < 0.01;

        string classification = openEnded ? "D: Open-Ended Dynamic Graph Universe"
                              : !saturated ? "C: Dynamic Evolution Physics"
                              : spectrumStable ? "B: Limited Dynamic Effects"
                              : "A: Static Graph Special Case";

        string verdict = openEnded
            ? $"OPEN-ENDED INNOVATION DETECTED. Species: {initSpecies} → {finalSpecies}. "
              + $"Innovation rate: {innovRate:F4}/gen. Saturation: {(saturated ? "YES" : "NO")}. "
              + $"Dynamic graphs enable CONTINUOUS innovation beyond the static eigenmode spectrum."
            : saturated
                ? $"INNOVATION SATURATES. Species: {initSpecies} → {finalSpecies}. "
                  + $"Saturation: YES. Dynamic graphs do NOT enable open-ended innovation "
                  + $"at tested mobility/range. The eigenmode spectrum is quasi-static."
                : $"INTERMEDIATE. Species: {initSpecies} → {finalSpecies}. Further study needed.";

        return new GraphEvolutionMetrics.DynamicGraphReport(
            history, initSpecies, finalSpecies,
            innovRate, saturated, openEnded, spectrumStable,
            classification, verdict);
    }

    public static string HostileReview(GraphEvolutionMetrics.DynamicGraphReport report)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW: Does dynamic graph matter?");
        sb.AppendLine();
        sb.AppendLine($"  Species: {report.InitialSpeciesCount} → {report.FinalSpeciesCount}");
        sb.AppendLine($"  Open-ended: {(report.OpenEndedDetected ? "YES" : "NO")}");
        sb.AppendLine($"  Saturation: {(report.InnovationSaturated ? "YES" : "NO")}");
        sb.AppendLine();
        sb.AppendLine("  If mobility is small, L_Q(t) ≈ L_Q(0) — quasi-static.");
        sb.AppendLine("  The graph structure must CHANGE SIGNIFICANTLY to produce");
        sb.AppendLine("  qualitatively new eigenmodes. Small fluctuations in");
        sb.AppendLine("  node positions don't change the spectrum enough.");
        sb.AppendLine();
        sb.AppendLine("  For truly open-ended innovation, we need:");
        sb.AppendLine("  - Node addition/removal (changing Q)");
        sb.AppendLine("  - Large topology changes (not just small perturbations)");
        sb.AppendLine("  - Graph rewiring beyond nearest-neighbor");
        sb.AppendLine();
        return sb.ToString();
    }
}
