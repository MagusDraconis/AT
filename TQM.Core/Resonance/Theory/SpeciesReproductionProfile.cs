namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Simulates information species reproduction, interaction, inheritance,
/// mutation, and long-term evolution in the Theta information layer.
///
/// TQM-134: Information Species Reproduction and Inheritance
/// </summary>
public static class SpeciesReproductionProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Species definitions with characteristic patterns.
    // ══════════════════════════════════════════════════════════════════

    public static readonly IReadOnlyDictionary<string, (string Taxonomy, double[] Pattern, double[] Genotype)> SpeciesDefinitions = new Dictionary<string, (string, double[], double[])>
    {
        ["A"] = ("Uniform Phase-Locked",
            BuildUniformPattern(20),
            ExtractGenotype(BuildUniformPattern(20))),
        ["B"] = ("Standing Wave (n=1)",
            BuildStandingWavePattern(20),
            ExtractGenotype(BuildStandingWavePattern(20))),
        ["C"] = ("Anti-Phase Domain",
            BuildAntiPhasePattern(20),
            ExtractGenotype(BuildAntiPhasePattern(20))),
        ["D"] = ("Composite Memory",
            BuildCompositePattern(20),
            ExtractGenotype(BuildCompositePattern(20))),
    };

    private static double[] BuildUniformPattern(int n)
        => Enumerable.Range(0, n).Select(_ => 1.0).ToArray();

    private static double[] BuildStandingWavePattern(int n)
        => Enumerable.Range(0, n).Select(i => Math.Sin(2 * Math.PI * i / n)).ToArray();

    private static double[] BuildAntiPhasePattern(int n)
        => Enumerable.Range(0, n).Select(i => i < n / 2 ? 1.0 : -1.0).ToArray();

    private static double[] BuildCompositePattern(int n)
    {
        var p = new double[n];
        for (int i = 0; i < n; i++)
            p[i] = Math.Sin(2 * Math.PI * i / n) + 0.5 * Math.Cos(6 * Math.PI * i / n);
        return p;
    }

    /// <summary>
    /// Extract a feature-vector "genotype" from a pattern.
    /// Genotype = [mean, std, skew, kurtosis, zero_crossings, dominant_freq, energy].
    /// </summary>
    public static double[] ExtractGenotype(double[] pattern)
    {
        if (pattern.Length == 0) return new double[7];

        int n = pattern.Length;
        double mean = pattern.Average();
        double std = Math.Sqrt(pattern.Average(x => (x - mean) * (x - mean)));

        // Skewness.
        double skew = std > 1e-10
            ? pattern.Average(x => Math.Pow((x - mean) / std, 3)) : 0;

        // Kurtosis (excess).
        double kurt = std > 1e-10
            ? pattern.Average(x => Math.Pow((x - mean) / std, 4)) - 3 : 0;

        // Zero crossings.
        int zc = 0;
        for (int i = 1; i < n; i++)
            if (pattern[i] * pattern[i - 1] < 0) zc++;

        // Dominant frequency (simplified: energy in first Fourier mode ratio).
        double re = 0, im = 0;
        for (int i = 0; i < n; i++)
        {
            double angle = 2 * Math.PI * i / n;
            re += pattern[i] * Math.Cos(angle);
            im += pattern[i] * Math.Sin(angle);
        }
        double domFreq = Math.Sqrt(re * re + im * im) / n;

        // Energy (norm squared).
        double energy = pattern.Sum(x => x * x);

        return new[] { mean, std, skew, kurt, (double)zc, domFreq, energy };
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulate reproduction between two species.
    // ══════════════════════════════════════════════════════════════════

    public static InformationLineage.ReproductionEvent SimulateReproduction(
        string speciesA, string speciesB,
        double density, double damping = 0.1, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);

        var defA = SpeciesDefinitions[speciesA];
        var defB = SpeciesDefinitions[speciesB];

        double overlap = InformationInteractionProfile.PatternOverlap(defA.Pattern, defB.Pattern);
        double genSim = GenotypeSimilarity(defA.Genotype, defB.Genotype);

        // Interaction strength depends on density and pattern overlap.
        double interactionStrength = Math.Abs(overlap) * density;

        string outcome;
        double[] child = null;
        double childSimA = 0, childSimB = 0;
        double inheritCoeff = 0;
        bool childSurvived = false;
        string desc;

        if (speciesA == speciesB)
        {
            // Same species: self-reproduction attempt.
            // Higher density enables self-copying; damping limits fidelity.
            double fidelity = 1.0 - damping * 0.5 / (density + 0.1);
            double mutationMagnitude = (1.0 - fidelity) * 0.5;

            if (density > 0.3 && fidelity > 0.6)
            {
                // Reproduce: child = parent pattern + mutation.
                child = new double[defA.Pattern.Length];
                for (int i = 0; i < child.Length; i++)
                    child[i] = defA.Pattern[i] * fidelity + NextGaussian(rng) * mutationMagnitude * Math.Abs(defA.Pattern[i]);

                childSimA = InformationInteractionProfile.PatternOverlap(defA.Pattern, child);
                childSimB = childSimA; // same parent
                inheritCoeff = childSimA; // inheritance = pattern similarity
                childSurvived = childSimA > 0.7 && density > 0.3;
                outcome = childSurvived ? "Reproduce" : "Extinct";
                desc = $"Self-reproduction: fidelity={fidelity:F2}, child similarity={childSimA:F3}";
            }
            else
            {
                outcome = "Extinct";
                desc = $"Self-reproduction failed: insufficient density ({density:F2}) or fidelity ({fidelity:F2})";
            }
        }
        else
        {
            // Different species: interaction.
            if (overlap > 0.8)
            {
                // High overlap: merge into a composite child.
                outcome = "Merge";
                child = new double[defA.Pattern.Length];
                for (int i = 0; i < child.Length; i++)
                    child[i] = (defA.Pattern[i] + defB.Pattern[i]) * 0.5;

                childSimA = InformationInteractionProfile.PatternOverlap(defA.Pattern, child);
                childSimB = InformationInteractionProfile.PatternOverlap(defB.Pattern, child);
                inheritCoeff = (childSimA + childSimB) * 0.5;
                childSurvived = inheritCoeff > 0.5;
                desc = $"Merge: overlap={overlap:F2}, composite child, inheritance={inheritCoeff:F3}";
            }
            else if (overlap < -0.5)
            {
                // Anti-correlated: competition — one dominates.
                // The species with higher "competitive advantage" (based on pattern energy) survives.
                double energyA = defA.Pattern.Sum(x => x * x);
                double energyB = defB.Pattern.Sum(x => x * x);

                if (energyA > energyB)
                {
                    outcome = "Compete";
                    child = (double[])defA.Pattern.Clone();
                    childSimA = 1.0;
                    childSimB = overlap;
                    inheritCoeff = childSimA;
                    childSurvived = true;
                    desc = $"Competition: {speciesA} dominates (E_A={energyA:F1} > E_B={energyB:F1})";
                }
                else
                {
                    outcome = "Compete";
                    child = (double[])defB.Pattern.Clone();
                    childSimA = overlap;
                    childSimB = 1.0;
                    inheritCoeff = childSimB;
                    childSurvived = true;
                    desc = $"Competition: {speciesB} dominates (E_B={energyB:F1} > E_A={energyA:F1})";
                }
            }
            else if (Math.Abs(overlap) < 0.2 && density > 0.5)
            {
                // Orthogonal + high density: potential reproduction.
                // Child = weighted blend of parent genotypes + mutation.
                double mutationMag = (1.0 - density) * 0.1;
                child = new double[defA.Pattern.Length];
                for (int i = 0; i < child.Length; i++)
                    child[i] = defA.Pattern[i] * 0.5 + defB.Pattern[i] * 0.5 + NextGaussian(rng) * mutationMag;

                childSimA = InformationInteractionProfile.PatternOverlap(defA.Pattern, child);
                childSimB = InformationInteractionProfile.PatternOverlap(defB.Pattern, child);
                inheritCoeff = (Math.Abs(childSimA) + Math.Abs(childSimB)) * 0.5;
                childSurvived = inheritCoeff > 0.4;
                outcome = childSurvived ? "Reproduce" : "Extinct";
                desc = $"Cross-species reproduction: blend + mutation, inheritance={inheritCoeff:F3}";
            }
            else
            {
                // Low overlap: coexist independently.
                outcome = "Coexist";
                child = null;
                childSimA = 0;
                childSimB = 0;
                inheritCoeff = 0;
                childSurvived = false;
                desc = $"Coexistence: overlap={overlap:F2}, species remain distinct";
            }
        }

        return new InformationLineage.ReproductionEvent(
            speciesA, speciesB,
            defA.Pattern, defB.Pattern,
            outcome, child,
            childSimA, childSimB,
            inheritCoeff, childSurvived, desc);
    }

    // ══════════════════════════════════════════════════════════════════
    // Long-term evolution simulation: track species populations over time.
    // ══════════════════════════════════════════════════════════════════

    public static (List<InformationLineage.SpeciesLineage> Lineages,
                   List<InformationLineage.ReproductionEvent> AllEvents,
                   int TotalExtinctions)
        SimulateEvolution(
            List<string> initialSpecies,
            int totalTimeUnits,
            double density,
            double damping = 0.1,
            int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var lineages = new List<InformationLineage.SpeciesLineage>();
        var allEvents = new List<InformationLineage.ReproductionEvent>();
        var speciesPool = new List<(string Name, double[] Pattern, int Generation, string Ancestor)>();

        // Initialize population.
        foreach (string sp in initialSpecies)
        {
            var def = SpeciesDefinitions[sp];
            speciesPool.Add((sp, (double[])def.Pattern.Clone(), 0, sp));
        }

        // Create initial lineages.
        foreach (string sp in initialSpecies.Distinct())
        {
            var def = SpeciesDefinitions[sp];
            lineages.Add(new InformationLineage.SpeciesLineage(
                sp, sp, 0, new List<string>(),
                (double[])def.Pattern.Clone(), (double[])def.Pattern.Clone(),
                1.0, 1, 0.0, false));
        }

        int extinctions = 0;
        int interactionInterval = Math.Max(totalTimeUnits / 100, 10);
        int mutationInterval = Math.Max(totalTimeUnits / 20, 50);
        int generationCounter = 1;

        // Evolution loop.
        for (int t = 0; t < totalTimeUnits; t++)
        {
            // Interactions at fixed intervals.
            if (t % interactionInterval == 0 && speciesPool.Count >= 2)
            {
                // Pick two random species from the pool.
                int i = rng.Next(speciesPool.Count);
                int j = rng.Next(speciesPool.Count);
                while (j == i) j = rng.Next(speciesPool.Count);

                var spA = speciesPool[i];
                var spB = speciesPool[j];

                // Simulate reproduction using their current patterns.
                double overlap = InformationInteractionProfile.PatternOverlap(spA.Pattern, spB.Pattern);
                double genSim = GenotypeSimilarity(
                    ExtractGenotype(spA.Pattern), ExtractGenotype(spB.Pattern));

                double interactionStr = Math.Abs(overlap) * density;

                string outcome;
                double[] child = null;
                bool childSurvived = false;
                double childSimA = 0, childSimB = 0, inheritCoeff = 0;
                string desc;

                if (spA.Name == spB.Name)
                {
                    double fidelity = 1.0 - damping * 0.5 / (density + 0.1);
                    double mutMag = (1.0 - fidelity) * 0.5;
                    if (density > 0.3 && fidelity > 0.6)
                    {
                        child = new double[spA.Pattern.Length];
                        for (int k = 0; k < child.Length; k++)
                            child[k] = spA.Pattern[k] * fidelity + NextGaussian(rng) * mutMag * Math.Abs(spA.Pattern[k]);
                        childSimA = InformationInteractionProfile.PatternOverlap(spA.Pattern, child);
                        childSimB = childSimA;
                        inheritCoeff = childSimA;
                        childSurvived = childSimA > 0.7 && density > 0.3;
                        outcome = childSurvived ? "Reproduce" : "Extinct";
                        desc = $"Evol: self-reproduction at t={t}";
                    }
                    else { outcome = "Extinct"; desc = $"Evol: self-reproduction failed t={t}"; }
                }
                else if (overlap > 0.8)
                {
                    outcome = "Merge";
                    child = new double[spA.Pattern.Length];
                    for (int k = 0; k < child.Length; k++)
                        child[k] = (spA.Pattern[k] + spB.Pattern[k]) * 0.5;
                    childSimA = InformationInteractionProfile.PatternOverlap(spA.Pattern, child);
                    childSimB = InformationInteractionProfile.PatternOverlap(spB.Pattern, child);
                    inheritCoeff = (childSimA + childSimB) * 0.5;
                    childSurvived = inheritCoeff > 0.5;
                    desc = $"Evol: merge at t={t}";
                }
                else if (overlap < -0.5)
                {
                    outcome = "Compete";
                    double eA = spA.Pattern.Sum(x => x * x);
                    double eB = spB.Pattern.Sum(x => x * x);
                    if (eA > eB)
                    {
                        child = (double[])spA.Pattern.Clone();
                        childSimA = 1.0; childSimB = overlap; inheritCoeff = 1.0;
                        childSurvived = true;
                        // Remove B from pool.
                        speciesPool.RemoveAt(j);
                        extinctions++;
                        desc = $"Evol: {spB.Name} extinct (competition) at t={t}";
                    }
                    else
                    {
                        child = (double[])spB.Pattern.Clone();
                        childSimA = overlap; childSimB = 1.0; inheritCoeff = 1.0;
                        childSurvived = true;
                        speciesPool.RemoveAt(i);
                        extinctions++;
                        desc = $"Evol: {spA.Name} extinct (competition) at t={t}";
                    }
                }
                else if (Math.Abs(overlap) < 0.2 && density > 0.5)
                {
                    outcome = "Reproduce";
                    double mutMag = (1.0 - density) * 0.1;
                    child = new double[spA.Pattern.Length];
                    for (int k = 0; k < child.Length; k++)
                        child[k] = spA.Pattern[k] * 0.5 + spB.Pattern[k] * 0.5 + NextGaussian(rng) * mutMag;
                    childSimA = InformationInteractionProfile.PatternOverlap(spA.Pattern, child);
                    childSimB = InformationInteractionProfile.PatternOverlap(spB.Pattern, child);
                    inheritCoeff = (Math.Abs(childSimA) + Math.Abs(childSimB)) * 0.5;
                    childSurvived = inheritCoeff > 0.4;
                    desc = $"Evol: cross-reproduction at t={t}";
                }
                else
                {
                    outcome = "Coexist";
                    child = null;
                    desc = $"Evol: coexistence at t={t}";
                }

                var ev = new InformationLineage.ReproductionEvent(
                    spA.Name, spB.Name, spA.Pattern, spB.Pattern,
                    outcome, child, childSimA, childSimB,
                    inheritCoeff, childSurvived, desc);
                allEvents.Add(ev);

                // If reproduction produced a child, add to pool and update lineages.
                if (childSurvived && child != null)
                {
                    string childName = GenerateChildName(spA.Name, spB.Name, generationCounter++);
                    speciesPool.Add((childName, child, Math.Max(spA.Generation, spB.Generation) + 1, spA.Ancestor));

                    var existingLineage = lineages.FirstOrDefault(l => l.SpeciesName == spA.Ancestor);
                    if (existingLineage != null)
                    {
                        double linSim = InformationInteractionProfile.PatternOverlap(
                            existingLineage.AncestorPattern, child);
                        double drift = 1.0 - linSim;
                        int idx = lineages.IndexOf(existingLineage);
                        lineages[idx] = existingLineage with
                        {
                            Descendants = existingLineage.Descendants.Append(childName).ToList(),
                            CurrentPattern = (double[])child.Clone(),
                            LineageSimilarity = linSim,
                            LineageLength = Math.Max(existingLineage.Generation, spA.Generation) + 1,
                            MutationDrift = existingLineage.MutationDrift + drift,
                        };
                    }
                    else
                    {
                        // Start a new lineage.
                        lineages.Add(new InformationLineage.SpeciesLineage(
                            spA.Ancestor, spA.Ancestor,
                            Math.Max(spA.Generation, spB.Generation) + 1,
                            new List<string> { childName },
                            (double[])SpeciesDefinitions[spA.Ancestor].Pattern.Clone(),
                            (double[])child.Clone(),
                            1.0,
                            Math.Max(spA.Generation, spB.Generation) + 1,
                            0.0, false));
                    }
                }
            }

            // Mutations at fixed intervals: apply small drift to all species patterns.
            if (t % mutationInterval == 0 && t > 0)
            {
                for (int i = 0; i < speciesPool.Count; i++)
                {
                    var sp = speciesPool[i];
                    double mutMag = damping * 0.05 / (density + 0.1);
                    var newPattern = new double[sp.Pattern.Length];
                    for (int k = 0; k < newPattern.Length; k++)
                        newPattern[k] = sp.Pattern[k] + NextGaussian(rng) * mutMag;
                    speciesPool[i] = (sp.Name, newPattern, sp.Generation, sp.Ancestor);
                }
            }
        }

        // Mark lineages that went extinct.
        for (int i = 0; i < lineages.Count; i++)
        {
            var lin = lineages[i];
            bool hasLiving = speciesPool.Any(sp => sp.Ancestor == lin.AncestorName);
            if (!hasLiving)
                lineages[i] = lin with { IsExtinct = true };
        }

        return (lineages, allEvents, extinctions);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute species transition matrix T_ij.
    // ══════════════════════════════════════════════════════════════════

    public static List<InformationLineage.SpeciesTransition> ComputeTransitionMatrix(
        string[] species, double density, int trialsPerPair = 20, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var transitions = new List<InformationLineage.SpeciesTransition>();

        foreach (string fromSp in species)
        foreach (string toSp in species)
        {
            int transitions_i_to_j = 0;
            double totalDrift = 0;

            for (int t = 0; t < trialsPerPair; t++)
            {
                // Start with species fromSp's pattern.
                var startPattern = (double[])SpeciesDefinitions[fromSp].Pattern.Clone();

                // Expose to environment dominated by toSp via attractor dynamics.
                // Simulate: pattern relaxes toward toSp's attractor basin.
                var targetPattern = SpeciesDefinitions[toSp].Pattern;
                double attractorPull = density * 0.5;

                var evolved = new double[startPattern.Length];
                for (int i = 0; i < evolved.Length; i++)
                    evolved[i] = startPattern[i] * (1 - attractorPull) + targetPattern[i] * attractorPull;

                // Classify the evolved pattern.
                string classified = ClassifyPattern(evolved);

                if (classified == toSp)
                {
                    transitions_i_to_j++;
                    double drift = 1.0 - InformationInteractionProfile.PatternOverlap(startPattern, evolved);
                    totalDrift += drift;
                }
            }

            double prob = (double)transitions_i_to_j / trialsPerPair;
            double meanDrift = transitions_i_to_j > 0 ? totalDrift / transitions_i_to_j : 0;

            string mechanism = prob > 0.8 ? "AttractorCapture"
                             : prob > 0.3 ? "Mutation" : "Competition";

            transitions.Add(new InformationLineage.SpeciesTransition(
                fromSp, toSp, prob, meanDrift, mechanism));
        }

        return transitions;
    }

    // ══════════════════════════════════════════════════════════════════
    // Classify a pattern to the nearest species.
    // ══════════════════════════════════════════════════════════════════

    public static string ClassifyPattern(double[] pattern)
    {
        string best = "Unknown";
        double bestSim = -1;

        foreach (var (name, (taxonomy, spPattern, _)) in SpeciesDefinitions)
        {
            double sim = Math.Abs(InformationInteractionProfile.PatternOverlap(pattern, spPattern));
            if (sim > bestSim) { bestSim = sim; best = name; }
        }
        return best;
    }

    // ══════════════════════════════════════════════════════════════════
    // Build species reproduction profile from evolution data.
    // ══════════════════════════════════════════════════════════════════

    public static InformationLineage.SpeciesReproductionProfile BuildProfile(
        string speciesName,
        List<InformationLineage.ReproductionEvent> events,
        List<InformationLineage.SpeciesLineage> lineages)
    {
        var speciesEvents = events
            .Where(e => e.ParentA == speciesName || e.ParentB == speciesName)
            .ToList();

        var reproduceEvents = speciesEvents
            .Where(e => e.Outcome == "Reproduce" && e.ChildSurvived).ToList();

        double reproRate = speciesEvents.Count > 0
            ? (double)reproduceEvents.Count / speciesEvents.Count : 0;

        double survival = speciesEvents.Count > 0
            ? (double)speciesEvents.Count(e => e.ChildSurvived) / speciesEvents.Count : 0;

        double fidelity = reproduceEvents.Count > 0
            ? reproduceEvents.Average(e => e.ParentChildSimilarityA) : 0;

        double mutation = 1.0 - fidelity;

        var lin = lineages.FirstOrDefault(l => l.AncestorName == speciesName);
        double drift = lin?.MutationDrift ?? 0;

        double advantage = ComputeCompetitiveAdvantage(speciesName, events);

        var def = SpeciesDefinitions[speciesName];

        return new InformationLineage.SpeciesReproductionProfile(
            speciesName, reproRate, survival, fidelity, mutation,
            advantage, def.Pattern, speciesEvents);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double GenotypeSimilarity(double[] a, double[] b)
    {
        if (a.Length == 0 || b.Length == 0) return 0;
        int n = Math.Min(a.Length, b.Length);
        double dot = 0, normA = 0, normB = 0;
        for (int i = 0; i < n; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }
        double denom = Math.Sqrt(normA * normB);
        return denom > 1e-10 ? Math.Abs(dot / denom) : 0;
    }

    private static double ComputeCompetitiveAdvantage(
        string species, List<InformationLineage.ReproductionEvent> events)
    {
        int wins = events.Count(e =>
            e.Outcome == "Compete" &&
            ((e.ParentA == species && e.ParentChildSimilarityA > 0.8) ||
             (e.ParentB == species && e.ParentChildSimilarityB > 0.8)));

        int total = events.Count(e =>
            e.Outcome == "Compete" &&
            (e.ParentA == species || e.ParentB == species));

        return total > 0 ? (double)wins / total : 0.5;
    }

    private static string GenerateChildName(string parentA, string parentB, int gen)
    {
        if (parentA == parentB)
            return $"{parentA}_{gen}";
        return $"{parentA}x{parentB}_{gen}";
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
