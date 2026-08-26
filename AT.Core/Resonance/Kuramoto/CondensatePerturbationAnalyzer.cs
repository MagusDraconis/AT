using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Applies perturbations to a running simulation and analyzes whether
/// resonance condensates survive, recover, or are destroyed.
/// </summary>
public sealed class CondensatePerturbationAnalyzer
{
    /// <summary>
    /// Applies a phase perturbation: adds random noise to each oscillator's phase.
    /// </summary>
    public static void ApplyPhasePerturbation(TemporalNetwork network, double level, Random rng)
    {
        foreach (var node in network.Nodes)
            node.Phase += (rng.NextDouble() * 2 - 1) * level * Math.PI;
    }

    /// <summary>
    /// Applies a frequency perturbation: adds random noise to each oscillator's frequency.
    /// </summary>
    public static void ApplyFrequencyPerturbation(TemporalNetwork network, double level, Random rng)
    {
        foreach (var node in network.Nodes)
        {
            double noise = (rng.NextDouble() * 2 - 1) * level * node.Frequency;
            node.Frequency = Math.Max(0.1, node.Frequency + noise);
        }
    }

    /// <summary>
    /// Removes a fraction of oscillators by zeroing their coupling entries.
    /// </summary>
    public static void ApplyOscillatorRemoval(TemporalNetwork network, double level, Random rng)
    {
        int n = network.NodeCount;
        int toRemove = (int)(n * level);
        var removed = new HashSet<int>();

        while (removed.Count < toRemove)
            removed.Add(rng.Next(n));

        foreach (int i in removed)
            for (int j = 0; j < n; j++)
            {
                network.Matrix[i, j] = 0;
                network.Matrix[j, i] = 0;
            }
    }

    /// <summary>
    /// Removes oscillators from the highest-density spatial regions.
    /// </summary>
    public static void ApplyDensityReduction(TemporalNetwork network, double level, Random rng)
    {
        int n = network.NodeCount;
        int toRemove = (int)(n * level);

        // Find highest-density regions: rank oscillators by local neighbor count (proxy for density).
        var densityScores = new (int Index, double Score)[n];
        var nodes = network.Nodes;

        for (int i = 0; i < n; i++)
        {
            int neighbors = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double dx = nodes[i].X - nodes[j].X;
                double dy = nodes[i].Y - nodes[j].Y;
                if (Math.Sqrt(dx * dx + dy * dy) < 0.05) neighbors++;
            }
            densityScores[i] = (i, neighbors);
        }

        var removed = densityScores.OrderByDescending(d => d.Score).Take(toRemove)
            .Select(d => d.Index).ToHashSet();

        foreach (int i in removed)
            for (int j = 0; j < n; j++)
            {
                network.Matrix[i, j] = 0;
                network.Matrix[j, i] = 0;
            }
    }

    /// <summary>
    /// Reduces coupling strength by scaling all matrix entries by (1 - level).
    /// </summary>
    public static void ApplyCouplingReduction(TemporalNetwork network, double level)
    {
        int n = network.NodeCount;
        double scale = 1.0 - level;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (i != j)
                    network.Matrix[i, j] *= scale;
    }

    /// <summary>
    /// Runs a full perturbation experiment:
    /// 1. Form condensates (first half of iterations)
    /// 2. Record pre-perturbation state
    /// 3. Apply perturbation
    /// 4. Run recovery (second half)
    /// 5. Measure outcomes
    /// </summary>
    public static CondensateStabilityResult RunPerturbation(
        TemporalNetwork network,
        TemporalSimulation sim,
        LocalDensityField densityField,
        ResonanceCondensationAnalyzer condAnalyzer,
        int totalIterations,
        string perturbationType,
        double perturbationLevel,
        Random rng,
        Action<TemporalNetwork, double, Random> perturbation)
    {
        int halfIter = totalIterations / 2;
        int checkpointInterval = 250;
        int condensatesBefore = 0;
        double localRBefore = 0;

        // Phase 1: form condensates.
        for (int iter = 0; iter < halfIter; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == halfIter - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                if (iter == halfIter - 1)
                {
                    condensatesBefore = condAnalyzer.GetAllCondensates().Count;
                    localRBefore = densityField.MaxLocalR();
                }
            }
        }

        // Record condensate IDs before perturbation for lifetime tracking.
        var prePerturbationIds = condAnalyzer.GetAllCondensates()
            .Select(c => c.Id).ToHashSet();

        // Apply perturbation.
        perturbation(network, perturbationLevel, rng);

        // Phase 2: recovery.
        int recoveryIterations = -1;
        int condensatesAfter = 0;
        double localRAfter = 0;

        for (int iter = halfIter; iter < totalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == totalIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                if (recoveryIterations < 0 && condensates.Count > 0)
                    recoveryIterations = iter - halfIter;

                if (iter == totalIterations - 1)
                {
                    var allCond = condAnalyzer.GetAllCondensates();
                    condensatesAfter = allCond.Count;
                    localRAfter = densityField.MaxLocalR();
                }
            }
        }

        // Determine survival and fragmentation.
        var postCondensates = condAnalyzer.GetAllCondensates();
        bool survived = postCondensates.Any(c => prePerturbationIds.Contains(c.Id));
        bool fragmented = condensatesAfter > condensatesBefore;
        bool merged = condensatesAfter > 0 && condensatesAfter < condensatesBefore;

        // Lifetime reduction: fraction of pre-perturbation condensates that died.
        int died = prePerturbationIds.Count(id => !postCondensates.Any(c => c.Id == id));
        double lifetimeReduction = prePerturbationIds.Count > 0
            ? (double)died / prePerturbationIds.Count
            : 0;

        return new CondensateStabilityResult(
            perturbationType,
            perturbationLevel,
            survived,
            condensatesBefore,
            condensatesAfter,
            localRBefore,
            localRAfter,
            recoveryIterations,
            lifetimeReduction,
            fragmented,
            merged);
    }
}
