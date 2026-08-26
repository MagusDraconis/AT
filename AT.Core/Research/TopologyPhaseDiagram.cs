namespace AT.Core.Research;

/// <summary>
/// Sweeps mobility parameter to construct the dynamic topology phase diagram.
///
/// AT-X003: Dynamic Topology Phase Diagram
/// </summary>
public static class TopologyPhaseDiagram
{
    public static List<DynamicPhaseMetrics.MobilityResult> SweepMobility(
        double[] mobilities, int Q = 20, int generations = 300, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var results = new List<DynamicPhaseMetrics.MobilityResult>();

        foreach (double mu in mobilities)
        {
            // Run 3 seeds per mobility for robustness.
            var innovRates = new List<double>();
            var drifts = new List<double>();
            var entropies = new List<double>();
            var initSps = new List<int>();
            var finalSps = new List<int>();

            for (int s = 0; s < 3; s++)
            {
                var history = DynamicGraphModel.Simulate(Q, generations, mu, 0.25, rng.Next());
                initSps.Add(history.First().UniqueSpeciesCount);
                finalSps.Add(history.Last().UniqueSpeciesCount);
                innovRates.Add(generations > 0
                    ? (double)(history.Last().UniqueSpeciesCount - history.First().UniqueSpeciesCount) / generations : 0);
                drifts.Add(history.Skip(1).Average(h => h.SpectralDrift));
                entropies.Add(history.Last().GraphEntropy - history.First().GraphEntropy);
            }

            double avgInnov = innovRates.Average();
            double avgDrift = drifts.Average();
            double avgEntropy = entropies.Average();

            // Classify phase.
            string phase;
            if (mu < 0.02) phase = "I: Static";
            else if (mu < 0.10) phase = "II: Quasi-Static";
            else if (mu < 0.50 && avgInnov > 0.001) phase = "III: Dynamic";
            else if (mu >= 0.50 && avgInnov > 0.005) phase = "IV: Open-Ended?";
            else phase = "III: Dynamic";

            results.Add(new DynamicPhaseMetrics.MobilityResult(
                mu, (int)initSps.Average(), (int)finalSps.Average(),
                avgInnov, avgDrift, avgEntropy, phase));
        }

        return results;
    }

    public static DynamicPhaseMetrics.PhaseDiagram BuildDiagram(List<DynamicPhaseMetrics.MobilityResult> results)
    {
        // Find critical mobilities where phase changes.
        double mu1 = 0, mu2 = 0;
        string lastPhase = "";
        foreach (var r in results)
        {
            if (lastPhase != "" && r.Phase != lastPhase)
            {
                if (mu1 == 0) mu1 = r.Mobility;
                else mu2 = r.Mobility;
            }
            lastPhase = r.Phase;
        }

        bool openEnded = results.Any(r => r.Phase.StartsWith("IV"));
        var phases = results.Select(r => r.Phase).Distinct().ToArray();

        string classification = openEnded ? "D: Open-Ended Topological Universe"
                              : results.Count(r => r.Phase == "III: Dynamic") >= 2 ? "C: Dynamic Graph Physics"
                              : "B: Dynamic Corrections Only";

        string verdict = openEnded
            ? $"OPEN-ENDED REGIME DETECTED at μ ≥ {mu2}. "
              + $"Phases: [{string.Join(" → ", phases)}]. "
              + $"Critical mobilities: μ_c1={mu1}, μ_c2={mu2}. "
              + $"Dynamic graph topology produces a phase transition to open-ended innovation."
            : $"No open-ended regime at tested mobilities. "
              + $"Phases: [{string.Join(" → ", phases)}]. "
              + $"Dynamic graph effects are corrections to static AT.";

        return new DynamicPhaseMetrics.PhaseDiagram(
            results, mu1, mu2, openEnded, phases, classification, verdict);
    }
}
