namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Simulates resource-constrained population dynamics of information species.
/// Implements Lotka-Volterra-like competition, carrying capacity limits,
/// birth/death processes, and selection pressure.
///
/// TQM-135: Information Selection Under Resource Constraints
/// </summary>
public static class InformationPopulationDynamics
{
    // ══════════════════════════════════════════════════════════════════
    // Run a resource-constrained population simulation.
    // ══════════════════════════════════════════════════════════════════

    public static (List<InformationFitnessProfile.PopulationSnapshot> History,
                   List<InformationFitnessProfile.SelectionMetrics> Metrics,
                   List<InformationFitnessProfile.SpeciesFitness> Fitnesses,
                   int Extinctions)
        SimulateConstrained(
            string[] species,
            int initialPopulationPerSpecies,
            int totalGenerations,
            double resourceCapacity,
            int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var history = new List<InformationFitnessProfile.PopulationSnapshot>();
        var budgets = ResourceConstraintModel.CreateBudgets(resourceCapacity);

        // Initialize populations.
        var populations = new Dictionary<string, int>();
        var initialFreqs = new Dictionary<string, double>();
        foreach (string sp in species)
        {
            populations[sp] = initialPopulationPerSpecies;
            initialFreqs[sp] = 1.0 / species.Length;
        }

        var consumptions = ResourceConstraintModel.GetConsumptionProfiles();

        // Base reproduction rates per species.
        var baseReproRates = new Dictionary<string, double>
        {
            ["A"] = 0.08,  // Uniform: moderate reproduction
            ["B"] = 0.06,  // Standing wave: slower reproduction
            ["C"] = 0.05,  // Anti-phase: slow reproduction
            ["D"] = 0.12,  // Composite: fast reproduction (confirmed by TQM-134)
        };

        // Death rates per species (inverse of stability).
        var baseDeathRates = new Dictionary<string, double>
        {
            ["A"] = 0.03,  // Uniform: very stable
            ["B"] = 0.05,  // Standing wave: moderately stable
            ["C"] = 0.06,  // Anti-phase: less stable
            ["D"] = 0.08,  // Composite: least stable individually
        };

        // Initial snapshot.
        int totalPop = populations.Values.Sum();
        history.Add(new InformationFitnessProfile.PopulationSnapshot(
            0,
            new Dictionary<string, int>(populations),
            populations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalPop),
            totalPop,
            ResourceConstraintModel.ComputeAggregatePressure(populations, budgets),
            ResourceConstraintModel.ComputeAggregatePressure(populations, budgets),
            GetDominant(populations)));

        int extinctions = 0;
        var extinctSet = new HashSet<string>();

        // Main simulation loop.
        for (int gen = 1; gen <= totalGenerations; gen++)
        {
            var newPopulations = new Dictionary<string, int>(populations);

            foreach (string sp in species)
            {
                if (extinctSet.Contains(sp)) continue;
                if (!populations.ContainsKey(sp)) continue;

                int currentPop = populations[sp];
                if (currentPop <= 0) { extinctSet.Add(sp); extinctions++; continue; }

                // Compute resource pressure for this species.
                double pressure = ResourceConstraintModel.ComputeAggregatePressure(
                    populations, budgets);

                // Carrying capacity effect: reproduction suppressed by resource pressure.
                double effectiveReproRate = baseReproRates[sp] * Math.Max(0, 1.0 - pressure);

                // Births: each individual has some probability of reproducing.
                int births = 0;
                for (int i = 0; i < currentPop; i++)
                {
                    if (rng.NextDouble() < effectiveReproRate)
                        births++;
                }

                // Deaths: baseline + resource-stress mortality.
                double stressMortality = pressure > 1.0 ? (pressure - 1.0) * 0.1 : 0;
                double effectiveDeathRate = baseDeathRates[sp] + stressMortality;
                effectiveDeathRate = Math.Min(effectiveDeathRate, 0.5); // cap at 50%

                int deaths = 0;
                for (int i = 0; i < currentPop; i++)
                {
                    if (rng.NextDouble() < effectiveDeathRate)
                        deaths++;
                }

                newPopulations[sp] = Math.Max(0, currentPop + births - deaths);

                // Hard capacity ceiling: if total population exceeds resource capacity * 10, cull.
                int hardCap = (int)(resourceCapacity * 5);
                if (newPopulations.Values.Sum() > hardCap)
                {
                    // Proportional culling — each species loses proportionally.
                    double excessRatio = (double)hardCap / newPopulations.Values.Sum();
                    foreach (string s in species)
                    {
                        if (newPopulations.ContainsKey(s) && newPopulations[s] > 0)
                            newPopulations[s] = Math.Max(0, (int)(newPopulations[s] * excessRatio));
                    }
                }

                // Minimum population threshold for extinction.
                if (newPopulations[sp] < 2 && currentPop > 2)
                {
                    // Stochastic extinction risk increases at low population.
                    if (rng.NextDouble() < 1.0 / Math.Max(newPopulations[sp], 1))
                    {
                        newPopulations[sp] = 0;
                        extinctSet.Add(sp);
                        extinctions++;
                    }
                }
            }

            populations = newPopulations;
            totalPop = populations.Values.Sum();

            // Regenerate resources.
            foreach (var budget in budgets)
            {
                // Resources regenerate slightly each generation.
                // We model this implicitly through the pressure calculation.
            }

            double aggPressure = ResourceConstraintModel.ComputeAggregatePressure(populations, budgets);

            history.Add(new InformationFitnessProfile.PopulationSnapshot(
                gen,
                new Dictionary<string, int>(populations),
                totalPop > 0
                    ? populations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalPop)
                    : new Dictionary<string, double>(),
                totalPop,
                aggPressure,
                aggPressure,
                GetDominant(populations)));
        }

        // Compute fitness profiles.
        var fitnesses = ComputeFitness(species, history, baseReproRates, consumptions, resourceCapacity);

        // Compute selection metrics.
        var metrics = ComputeSelectionMetrics(species, history, initialFreqs);

        return (history, metrics, fitnesses, extinctions);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run an UNCONSTRAINED population simulation (control).
    // ══════════════════════════════════════════════════════════════════

    public static (List<InformationFitnessProfile.PopulationSnapshot> History,
                   List<InformationFitnessProfile.SelectionMetrics> Metrics,
                   int Extinctions)
        SimulateUnconstrained(
            string[] species,
            int initialPopulationPerSpecies,
            int totalGenerations,
            int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var history = new List<InformationFitnessProfile.PopulationSnapshot>();

        var populations = new Dictionary<string, int>();
        var initialFreqs = new Dictionary<string, double>();
        foreach (string sp in species)
        {
            populations[sp] = initialPopulationPerSpecies;
            initialFreqs[sp] = 1.0 / species.Length;
        }

        var baseReproRates = new Dictionary<string, double>
        { ["A"] = 0.08, ["B"] = 0.06, ["C"] = 0.05, ["D"] = 0.12 };

        var baseDeathRates = new Dictionary<string, double>
        { ["A"] = 0.03, ["B"] = 0.05, ["C"] = 0.06, ["D"] = 0.08 };

        int totalPop = populations.Values.Sum();
        history.Add(new InformationFitnessProfile.PopulationSnapshot(
            0, new Dictionary<string, int>(populations),
            populations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalPop),
            totalPop, 0, 0, GetDominant(populations)));

        int extinctions = 0;
        var extinctSet = new HashSet<string>();

        for (int gen = 1; gen <= totalGenerations; gen++)
        {
            var newPop = new Dictionary<string, int>(populations);
            foreach (string sp in species)
            {
                if (extinctSet.Contains(sp)) continue;
                int cur = populations.GetValueOrDefault(sp, 0);
                if (cur <= 0) { extinctSet.Add(sp); extinctions++; continue; }

                int births = 0, deaths = 0;
                for (int i = 0; i < cur; i++)
                {
                    if (rng.NextDouble() < baseReproRates[sp]) births++;
                    if (rng.NextDouble() < baseDeathRates[sp]) deaths++;
                }
                newPop[sp] = Math.Max(0, cur + births - deaths);
            }
            populations = newPop;
            totalPop = populations.Values.Sum();
            history.Add(new InformationFitnessProfile.PopulationSnapshot(
                gen, new Dictionary<string, int>(populations),
                totalPop > 0 ? populations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalPop)
                    : new Dictionary<string, double>(),
                totalPop, 0, 0, GetDominant(populations)));
        }

        var metrics = ComputeSelectionMetrics(species, history, initialFreqs);
        return (history, metrics, extinctions);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compare constrained vs unconstrained to isolate selection effects.
    // ══════════════════════════════════════════════════════════════════

    public static (double SelectionEffect, double Confidence)
        CompareConstrainedVsUnconstrained(
            string species,
            List<InformationFitnessProfile.PopulationSnapshot> constrainedHistory,
            List<InformationFitnessProfile.PopulationSnapshot> unconstrainedHistory)
    {
        if (constrainedHistory.Count == 0 || unconstrainedHistory.Count == 0)
            return (0, 0);

        // Compare final frequencies.
        double constrainedFinalFreq = 0, unconstrainedFinalFreq = 0;

        var cLast = constrainedHistory.Last();
        var uLast = unconstrainedHistory.Last();

        if (cLast.Frequencies.TryGetValue(species, out double cf))
            constrainedFinalFreq = cf;
        if (uLast.Frequencies.TryGetValue(species, out double uf))
            unconstrainedFinalFreq = uf;

        double effect = constrainedFinalFreq - unconstrainedFinalFreq;
        double confidence = Math.Abs(effect) > 0.05 ? effect / 0.05 : effect / 0.01;
        confidence = Math.Min(confidence, 1.0);

        return (effect, Math.Max(confidence, 0));
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute fitness profiles.
    // ══════════════════════════════════════════════════════════════════

    private static List<InformationFitnessProfile.SpeciesFitness> ComputeFitness(
        string[] species,
        List<InformationFitnessProfile.PopulationSnapshot> history,
        Dictionary<string, double> reproRates,
        List<InformationFitnessProfile.ResourceConsumption> consumptions,
        double resourceCapacity)
    {
        var fitnesses = new List<InformationFitnessProfile.SpeciesFitness>();

        if (history.Count < 2) return fitnesses;

        var final = history.Last();
        var initial = history.First();
        double totalResourcePerIndividual = resourceCapacity / Math.Max(final.TotalPopulation, 1);

        foreach (string sp in species)
        {
            int initPop = initial.Populations.GetValueOrDefault(sp, 0);
            int finalPop = final.Populations.GetValueOrDefault(sp, 0);

            double growthRate = initPop > 0 && history.Count > 1
                ? (double)(finalPop - initPop) / initPop / history.Count
                : 0;

            var c = consumptions.FirstOrDefault(x => x.SpeciesName == sp);
            double totalConsumption = c != null
                ? c.AmplitudeConsumption + c.MemoryConsumption + c.CoherenceConsumption
                  + c.LifetimeConsumption + c.SpatialConsumption + c.BandwidthConsumption
                : 6.0;

            double efficiency = totalConsumption > 0
                ? reproRates.GetValueOrDefault(sp, 0.05) / totalConsumption : 0;

            double carryingCap = resourceCapacity / Math.Max(totalConsumption, 0.1);

            bool isDominant = final.Frequencies.TryGetValue(sp, out double freq)
                && final.Frequencies.Values.All(f => freq >= f);

            string rank = isDominant ? "Dominant"
                : freq > 0.15 ? "Intermediate" : "Marginal";

            double extProb = finalPop <= 0 ? 1.0
                : finalPop < 5 ? 0.5
                : finalPop < 20 ? 0.1 : 0.01;

            fitnesses.Add(new InformationFitnessProfile.SpeciesFitness(
                sp, reproRates.GetValueOrDefault(sp, 0.05),
                carryingCap, efficiency,
                1.0 / Math.Max(carryingCap, 1), // α_ii = 1/K
                growthRate / Math.Max(reproRates.GetValueOrDefault(sp, 0.01), 0.01), // selection coefficient
                finalPop, extProb, isDominant, rank));
        }

        return fitnesses;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute selection metrics from population history.
    // ══════════════════════════════════════════════════════════════════

    private static List<InformationFitnessProfile.SelectionMetrics> ComputeSelectionMetrics(
        string[] species,
        List<InformationFitnessProfile.PopulationSnapshot> history,
        Dictionary<string, double> initialFreqs)
    {
        var metrics = new List<InformationFitnessProfile.SelectionMetrics>();

        if (history.Count < 2) return metrics;

        var first = history.First();
        var last = history.Last();

        double meanGrowth = 0;
        var growthRates = new Dictionary<string, double>();
        foreach (string sp in species)
        {
            int n0 = first.Populations.GetValueOrDefault(sp, 0);
            int nT = last.Populations.GetValueOrDefault(sp, 0);
            double g = n0 > 0 ? (double)(nT - n0) / n0 / history.Count : 0;
            growthRates[sp] = g;
            meanGrowth += g;
        }
        meanGrowth /= Math.Max(species.Length, 1);

        foreach (string sp in species)
        {
            double initFreq = initialFreqs.GetValueOrDefault(sp, 0);
            double finalFreq = last.Frequencies.GetValueOrDefault(sp, 0);
            double deltaFreq = finalFreq - initFreq;

            double g = growthRates.GetValueOrDefault(sp, 0);
            double relFitness = meanGrowth != 0 ? g / meanGrowth : 1.0;

            double selDiff = deltaFreq; // selection differential = Δ frequency
            bool freqIncreased = deltaFreq > 0.01;
            bool wentExtinct = last.Populations.GetValueOrDefault(sp, 0) <= 0;
            bool significant = Math.Abs(deltaFreq) > 0.05;

            metrics.Add(new InformationFitnessProfile.SelectionMetrics(
                sp, deltaFreq, g, relFitness, selDiff,
                freqIncreased, wentExtinct, significant));
        }

        return metrics;
    }

    // ══════════════════════════════════════════════════════════════════
    // Fit a replicator equation to population dynamics.
    // ══════════════════════════════════════════════════════════════════

    public static (string FitQuality, double R2, double[] Coefficients)
        FitReplicatorEquation(List<InformationFitnessProfile.PopulationSnapshot> history, string[] species)
    {
        if (history.Count < 5) return ("None", 0, Array.Empty<double>());

        // Replicator: dx_i/dt = x_i * (f_i - ⟨f⟩)
        // Where x_i = N_i / N_total, f_i = fitness of species i.
        // We compute dN_i/dt from successive snapshots and check if
        // it correlates with N_i * (something).

        var dNi = new List<double>();
        var predicted = new List<double>();

        // Average growth rate per species.
        var avgGrowth = new Dictionary<string, double>();
        foreach (string sp in species)
        {
            var popSeries = history
                .Where(h => h.Populations.ContainsKey(sp))
                .Select(h => (double)h.Populations[sp])
                .ToList();
            if (popSeries.Count >= 2)
            {
                double totalGrowth = 0;
                for (int i = 1; i < popSeries.Count; i++)
                    totalGrowth += popSeries[i] - popSeries[i - 1];
                avgGrowth[sp] = totalGrowth / (popSeries.Count - 1);
            }
            else avgGrowth[sp] = 0;
        }

        double meanGrowth = avgGrowth.Values.Average();

        // For each time step, compute: dN_i and N_i * (r_i - mean_r).
        for (int t = 1; t < history.Count; t++)
        {
            var prev = history[t - 1];
            var curr = history[t];

            foreach (string sp in species)
            {
                if (!prev.Populations.TryGetValue(sp, out int prevPop)) continue;
                if (!curr.Populations.TryGetValue(sp, out int currPop)) continue;

                dNi.Add(currPop - prevPop);
                predicted.Add(prevPop * (avgGrowth.GetValueOrDefault(sp, 0) - meanGrowth));
            }
        }

        // Compute R² between dNi and predicted.
        if (dNi.Count < 3) return ("None", 0, Array.Empty<double>());

        double meanD = dNi.Average();
        double meanP = predicted.Average();
        double cov = 0, varD = 0, varP = 0;
        for (int i = 0; i < dNi.Count; i++)
        {
            cov += (dNi[i] - meanD) * (predicted[i] - meanP);
            varD += (dNi[i] - meanD) * (dNi[i] - meanD);
            varP += (predicted[i] - meanP) * (predicted[i] - meanP);
        }

        double r2 = varD > 1e-10 && varP > 1e-10 ? cov * cov / (varD * varP) : 0;

        string quality = r2 > 0.5 ? "Strong" : r2 > 0.2 ? "Moderate" : r2 > 0.05 ? "Weak" : "None";

        return (quality, r2, avgGrowth.Values.ToArray());
    }

    // ══════════════════════════════════════════════════════════════════
    // Helper.
    // ══════════════════════════════════════════════════════════════════

    private static string GetDominant(Dictionary<string, int> populations)
    {
        if (populations.Count == 0) return "None";
        return populations.OrderByDescending(kv => kv.Value).First().Key;
    }
}
