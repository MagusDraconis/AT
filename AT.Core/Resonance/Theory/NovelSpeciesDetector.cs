namespace AT.Core.Resonance.Theory;

/// <summary>
/// Runs long-term information evolution with mutation-driven pattern drift
/// to detect genuinely novel information species.
///
/// AT-138: Open-Ended Information Innovation
/// </summary>
public static class NovelSpeciesDetector
{
    private const int PatternLength = 20;
    private const double NoveltyThreshold = 0.45; // max similarity to known species to be "novel"
    private const int PersistenceThreshold = 30;   // must survive this many gens to be "persistent"

    // Known species reference patterns.
    private static readonly Dictionary<string, double[]> KnownPatterns = new()
    {
        ["A"] = SpeciesReproductionProfile.SpeciesDefinitions["A"].Pattern,
        ["B"] = SpeciesReproductionProfile.SpeciesDefinitions["B"].Pattern,
        ["C"] = SpeciesReproductionProfile.SpeciesDefinitions["C"].Pattern,
        ["D"] = SpeciesReproductionProfile.SpeciesDefinitions["D"].Pattern,
    };

    // ══════════════════════════════════════════════════════════════════
    // Run a long-term innovation evolution experiment.
    // ══════════════════════════════════════════════════════════════════

    public static (List<InnovationLineage.NovelSpecies> Novelties,
                   List<InnovationLineage.DiversitySnapshot> History,
                   InnovationLineage.InnovationMetrics Metrics)
        RunInnovationExperiment(
            int totalPopulation,
            int totalGenerations,
            double resourceCapacity,
            double mutationStrength = 0.05,
            int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        // Each individual: (ancestorSpecies, pattern, generationBorn).
        var population = new List<(string Ancestor, double[] Pattern, int Born)>();

        // Initialize with equal mix of known species.
        string[] founders = { "A", "B", "C", "D" };
        int perFounder = totalPopulation / founders.Length;
        foreach (string sp in founders)
        {
            var basePattern = KnownPatterns[sp];
            for (int i = 0; i < perFounder; i++)
            {
                var p = new double[PatternLength];
                for (int j = 0; j < PatternLength; j++)
                    p[j] = basePattern[j] + NextGaussian(rng) * 0.02; // tiny initial variation
                population.Add((sp, p, 0));
            }
        }

        // Pad to exact population.
        while (population.Count < totalPopulation)
            population.Add(("A", (double[])KnownPatterns["A"].Clone(), 0));

        // Discovery tracking.
        var discoveredNovel = new Dictionary<string, (double[] Pattern, int FirstSeen, int LastSeen, int Count)>();
        var history = new List<InnovationLineage.DiversitySnapshot>();
        int novelCounter = 0;

        // Main evolution loop.
        for (int gen = 0; gen < totalGenerations; gen++)
        {
            // ── Step 1: Measure diversity ──
            int knownAlive = population.Select(p => p.Ancestor)
                .Where(a => founders.Contains(a)).Distinct().Count();
            int novelAlive = discoveredNovel.Count(kv => kv.Value.Count > 0);

            double meanComplexity = population.Average(ind =>
                (double)CountZeroCrossings(ind.Pattern));
            double meanNovelty = population.Average(ind =>
                ComputeNoveltyScore(ind.Pattern));

            string dominantNovel = discoveredNovel
                .OrderByDescending(kv => kv.Value.Count)
                .FirstOrDefault().Key ?? "None";

            history.Add(new InnovationLineage.DiversitySnapshot(
                gen, knownAlive, discoveredNovel.Count,
                knownAlive + novelAlive, meanComplexity, meanNovelty, dominantNovel));

            // ── Step 2: Resource pressure ──
            double totalConsumption = population.Count * 5.0; // avg consumption
            double pressure = resourceCapacity > 0 ? totalConsumption / resourceCapacity : 0;

            // ── Step 3: Reproduction with mutation ──
            var newPop = new List<(string, double[], int)>();

            foreach (var (ancestor, pattern, born) in population)
            {
                // Fitness: higher novelty can mean lower efficiency (pessimistic).
                double noveltyPenalty = ComputeNoveltyScore(pattern) * 0.5;
                double effectiveRepro = 0.08 * Math.Max(0.1, 1.0 - pressure - noveltyPenalty);
                double effectiveDeath = 0.04 + (pressure > 1.0 ? (pressure - 1.0) * 0.1 : 0);
                effectiveDeath = Math.Min(effectiveDeath, 0.5);

                // Survival check.
                if (rng.NextDouble() < effectiveDeath)
                    continue; // individual dies

                // Reproduction.
                int offspring = 0;
                if (rng.NextDouble() < effectiveRepro) offspring = 1;
                if (effectiveRepro > 0.15 && rng.NextDouble() < effectiveRepro * 0.5) offspring++; // bonus

                // Survivor itself.
                newPop.Add((ancestor, pattern, born));

                // Offspring with mutation.
                for (int o = 0; o < offspring; o++)
                {
                    var child = new double[PatternLength];
                    for (int i = 0; i < PatternLength; i++)
                        child[i] = pattern[i] + NextGaussian(rng) * mutationStrength;

                    // Occasionally larger mutation (innovation jump).
                    if (rng.NextDouble() < 0.01)
                        for (int i = 0; i < PatternLength; i++)
                            child[i] += NextGaussian(rng) * mutationStrength * 5.0;

                    newPop.Add((ancestor, child, gen));
                }
            }

            // ── Step 4: Cap population ──
            if (newPop.Count > totalPopulation)
            {
                // Random culling (simplified: keep random subset).
                newPop = newPop.OrderBy(_ => rng.Next()).Take(totalPopulation).ToList();
            }

            // Fill if below capacity.
            while (newPop.Count < totalPopulation / 2 && newPop.Count > 0)
            {
                var parent = newPop[rng.Next(newPop.Count)];
                var child = new double[PatternLength];
                for (int i = 0; i < PatternLength; i++)
                    child[i] = parent.Item2[i] + NextGaussian(rng) * mutationStrength;
                newPop.Add((parent.Item1, child, gen));
            }

            population = newPop;

            // ── Step 5: Detect novel species ──
            // Cluster population and check for novel clusters.
            if (gen % 25 == 0 && population.Count >= 10)
            {
                var clusters = ClusterPopulation(population, NoveltyThreshold);
                foreach (var cluster in clusters)
                {
                    if (cluster.Count < 3) continue; // too small

                    var proto = AveragePattern(cluster.Select(c => c.Pattern).ToList());
                    double novelty = ComputeNoveltyScore(proto);

                    if (novelty > 0.5) // significantly different from all known species
                    {
                        // Check if already discovered.
                        bool isNew = true;
                        string matchKey = null;
                        foreach (var (key, (pat, first, last, cnt)) in discoveredNovel)
                        {
                            double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(proto, pat));
                            if (sim > 0.8) { isNew = false; matchKey = key; break; }
                        }

                        if (isNew)
                        {
                            string name = $"N{++novelCounter}";
                            discoveredNovel[name] = (proto, gen, gen, cluster.Count);
                        }
                        else if (matchKey != null)
                        {
                            var (pat, first, last, cnt) = discoveredNovel[matchKey];
                            discoveredNovel[matchKey] = (pat, first, gen, cnt + cluster.Count);
                        }
                    }
                }
            }
        }

        // ── Build novel species records ──
        var novelties = new List<InnovationLineage.NovelSpecies>();
        foreach (var (key, (pattern, firstSeen, lastSeen, count)) in discoveredNovel)
        {
            int persistence = lastSeen - firstSeen;
            bool persistent = persistence >= PersistenceThreshold;

            double complexity = CountZeroCrossings(pattern);
            double energy = pattern.Sum(x => x * x);
            double novelty = ComputeNoveltyScore(pattern);

            // Find closest known parent.
            string parent = "Unknown";
            double bestSim = 0;
            foreach (var (sp, pat) in KnownPatterns)
            {
                double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(pattern, pat));
                if (sim > bestSim) { bestSim = sim; parent = sp; }
            }

            novelties.Add(new InnovationLineage.NovelSpecies(
                key, pattern, firstSeen, parent,
                novelty, persistence, complexity, energy, persistent));
        }

        // ── Compute metrics ──
        double innovRate = totalGenerations > 0
            ? (double)novelties.Count / totalGenerations * 1000 : 0;

        // Species saturation: is the discovery curve flattening?
        double satIndex = 0;
        if (history.Count >= 10)
        {
            var early = history.Take(history.Count / 3).ToList();
            var late = history.Skip(2 * history.Count / 3).ToList();
            double earlyRate = early.Count > 1
                ? (double)(early.Last().NovelSpeciesCount - early.First().NovelSpeciesCount)
                  / Math.Max(early.Last().TimeStep - early.First().TimeStep, 1) : 0;
            double lateRate = late.Count > 1
                ? (double)(late.Last().NovelSpeciesCount - late.First().NovelSpeciesCount)
                  / Math.Max(late.Last().TimeStep - late.First().TimeStep, 1) : 0;
            satIndex = earlyRate > 1e-10 ? 1.0 - Math.Min(lateRate / earlyRate, 1.0) : 0;
        }

        double initComplexity = history.FirstOrDefault()?.MeanComplexity ?? 0;
        double finalComplexity = history.LastOrDefault()?.MeanComplexity ?? 0;
        double complexityGrowth = totalGenerations > 0
            ? (finalComplexity - initComplexity) / totalGenerations * 1000 : 0;

        int maxDepth = novelties.Count > 0
            ? novelties.Max(n => n.PersistenceGenerations) : 0;

        double meanNoveltyScore = novelties.Count > 0
            ? novelties.Average(n => n.NoveltyScore) : 0;

        int persistentCount = novelties.Count(n => n.IsPersistent);
        bool innovDetected = novelties.Count > 0;
        bool satObserved = satIndex > 0.5;
        bool complexityInc = complexityGrowth > 0.001;

        string curveShape = satIndex > 0.7 ? "Saturating"
                          : satIndex > 0.3 ? "Logarithmic"
                          : complexityGrowth > 0.01 ? "Exponential"
                          : innovRate > 0.5 ? "Linear"
                          : "Flat";

        var metrics = new InnovationLineage.InnovationMetrics(
            novelties.Count, persistentCount, innovRate, satIndex,
            initComplexity, finalComplexity, complexityGrowth,
            maxDepth, meanNoveltyScore,
            innovDetected, satObserved, complexityInc, curveShape);

        return (novelties, history, metrics);
    }

    // ══════════════════════════════════════════════════════════════════
    // Novelty scoring.
    // ══════════════════════════════════════════════════════════════════

    private static double ComputeNoveltyScore(double[] pattern)
    {
        double maxSim = 0;
        foreach (var (_, known) in KnownPatterns)
        {
            double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(pattern, known));
            if (sim > maxSim) maxSim = sim;
        }
        return 1.0 - maxSim; // higher = more novel
    }

    /// <summary>
    /// Check if a pattern represents a genuinely novel species.
    /// </summary>
    public static bool IsNovel(double[] pattern, out double noveltyScore, out string closestKnown)
    {
        noveltyScore = ComputeNoveltyScore(pattern);
        closestKnown = "Unknown";
        double maxSim = 0;
        foreach (var (name, known) in KnownPatterns)
        {
            double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(pattern, known));
            if (sim > maxSim) { maxSim = sim; closestKnown = name; }
        }
        return noveltyScore > 0.5;
    }

    // ══════════════════════════════════════════════════════════════════
    // Clustering.
    // ══════════════════════════════════════════════════════════════════

    private static List<List<(string Ancestor, double[] Pattern, int Born)>> ClusterPopulation(
        List<(string Ancestor, double[] Pattern, int Born)> population, double threshold)
    {
        var clusters = new List<List<(string Ancestor, double[] Pattern, int Born)>>();
        var used = new bool[population.Count];

        for (int i = 0; i < population.Count; i++)
        {
            if (used[i]) continue;
            var cluster = new List<(string Ancestor, double[] Pattern, int Born)> { population[i] };
            used[i] = true;

            for (int j = i + 1; j < population.Count; j++)
            {
                if (used[j]) continue;
                double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(
                    population[i].Pattern, population[j].Pattern));
                if (sim > threshold)
                {
                    cluster.Add(population[j]);
                    used[j] = true;
                }
            }
            clusters.Add(cluster);
        }
        return clusters;
    }

    private static double[] AveragePattern(List<double[]> patterns)
    {
        if (patterns.Count == 0) return new double[PatternLength];
        var avg = new double[PatternLength];
        foreach (var p in patterns)
            for (int i = 0; i < PatternLength; i++)
                avg[i] += p[i];
        for (int i = 0; i < PatternLength; i++)
            avg[i] /= patterns.Count;
        return avg;
    }

    private static int CountZeroCrossings(double[] p)
    {
        int zc = 0;
        for (int i = 1; i < p.Length; i++)
            if (p[i] * p[i - 1] < 0) zc++;
        return zc;
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
