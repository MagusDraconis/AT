namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Runs population dynamics under alternative fitness models and resource regimes
/// to determine whether Darwinian evolution is universal or model-dependent.
///
/// TQM-137: Universality of Information Evolution
/// </summary>
public static class EvolutionRobustnessProfile
{
    // ══════════════════════════════════════════════════════════════════
    // Define all alternative fitness models.
    // ══════════════════════════════════════════════════════════════════

    public static List<AlternativeFitnessModel.FitnessModelSpec> GetAllFitnessModels()
    {
        return new List<AlternativeFitnessModel.FitnessModelSpec>
        {
            new("Baseline (r/c)", "w = r/c", "Rational", false,
                (r, c) => c > 0.01 ? r / c : r / 0.01),

            new("Quadratic Repro (r²/c)", "w = r²/c", "Polynomial", false,
                (r, c) => c > 0.01 ? r * r / c : r * r / 0.01),

            new("Inverse Square Cons (r/c²)", "w = r/c²", "Rational", false,
                (r, c) => { double cc = Math.Max(c, 0.01); return r / (cc * cc); }),

            new("Linear Difference (r-c)", "w = r - c", "Polynomial", false,
                (r, c) => Math.Max(r - c * 0.01, 0.001)),

            new("Logarithmic (log(r+1)/(c+1))", "w = log(r+1)/(c+1)", "Logarithmic", false,
                (r, c) => Math.Log(r + 1) / (c + 1)),

            new("Random Fitness", "w = random", "Random", false,
                (r, c) => 0), // set per-species via randomness

            new("Equal Fitness", "w = 1 (all equal)", "Emergent", true,
                (r, c) => 1.0),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Define all resource regimes.
    // ══════════════════════════════════════════════════════════════════

    public static List<AlternativeFitnessModel.ResourceRegime> GetAllResourceRegimes()
    {
        return new List<AlternativeFitnessModel.ResourceRegime>
        {
            new("Global Resources", "Single shared resource pool", 1.0, false,
                (t, cap) => cap),

            new("Local Resources", "Per-species separate budgets", 0.5, false,
                (t, cap) => cap),

            new("Dynamic Resources", "Capacity grows over time", 0.3, true,
                (t, cap) => cap * (1.0 + t * 0.002)),

            new("Fluctuating Resources", "Sinusoidal capacity variation", 1.0, true,
                (t, cap) => cap * (1.0 + 0.3 * Math.Sin(t * 0.05))),

            new("Scarcity Cycles", "Alternating scarcity/abundance", 0.8, true,
                (t, cap) => (t / 50) % 2 == 0 ? cap * 0.3 : cap * 1.5),

            new("Resource Shocks", "Sudden capacity drops", 1.0, true,
                (t, cap) => t % 100 == 50 ? cap * 0.2 : cap),

            new("No Resource Limits", "Unconstrained growth (control)", 9999.0, false,
                (t, cap) => cap),
        };
    }

    // ══════════════════════════════════════════════════════════════════
    // Run a single model × regime combination.
    // ══════════════════════════════════════════════════════════════════

    public static AlternativeFitnessModel.ModelRunResult RunModel(
        AlternativeFitnessModel.FitnessModelSpec model,
        AlternativeFitnessModel.ResourceRegime regime,
        int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        string[] species = { "A", "B", "C", "D" };

        // Baseline rates.
        var reproRates = new Dictionary<string, double>
        { ["A"] = 0.08, ["B"] = 0.06, ["C"] = 0.05, ["D"] = 0.12 };
        var deathRates = new Dictionary<string, double>
        { ["A"] = 0.03, ["B"] = 0.05, ["C"] = 0.06, ["D"] = 0.08 };
        var consumptions = ResourceConstraintModel.GetConsumptionProfiles();
        var consDict = consumptions.ToDictionary(c => c.SpeciesName,
            c => c.AmplitudeConsumption + c.MemoryConsumption + c.CoherenceConsumption
               + c.LifetimeConsumption + c.SpatialConsumption + c.BandwidthConsumption);

        // Compute fitness values for each species.
        var fitnessValues = new Dictionary<string, double>();
        if (model.Name == "Random Fitness")
        {
            // Assign random fitness values to each species.
            fitnessValues["A"] = 0.2 + rng.NextDouble() * 0.6;
            fitnessValues["B"] = 0.2 + rng.NextDouble() * 0.6;
            fitnessValues["C"] = 0.2 + rng.NextDouble() * 0.6;
            fitnessValues["D"] = 0.2 + rng.NextDouble() * 0.6;
        }
        else
        {
            foreach (string sp in species)
            {
                double r = reproRates[sp];
                double c = consDict[sp];
                fitnessValues[sp] = model.Compute(r, c);
            }
        }

        // Run simulation.
        int generations = 150;
        int initialPop = 50;
        double baseCapacity = 200.0;
        var populations = species.ToDictionary(s => s, _ => initialPop);
        var initialFreqs = species.ToDictionary(s => s, s => 1.0 / species.Length);
        var extinctSet = new HashSet<string>();
        int extinctions = 0;

        for (int gen = 0; gen < generations; gen++)
        {
            double effectiveCapacity = regime.CapacityFn(gen, baseCapacity) * regime.CapacityScale;
            double totalConsumption = populations.Sum(kv =>
                (double)kv.Value * consDict.GetValueOrDefault(kv.Key, 5.0));
            double pressure = effectiveCapacity > 0 ? totalConsumption / effectiveCapacity : 0;

            var newPop = new Dictionary<string, int>(populations);

            foreach (string sp in species)
            {
                if (extinctSet.Contains(sp)) continue;

                int cur = populations.GetValueOrDefault(sp, 0);
                if (cur <= 0) { extinctSet.Add(sp); extinctions++; continue; }

                // Fitness modulates reproduction and death rates.
                double w = fitnessValues.GetValueOrDefault(sp, 0.01);
                double meanW = fitnessValues.Values.Average();
                double relativeFitness = meanW > 0 ? w / meanW : 1.0;

                // Higher fitness → higher effective reproduction, lower effective death.
                double fitnessModRepro = Math.Clamp(relativeFitness, 0.1, 3.0);
                double fitnessModDeath = Math.Clamp(2.0 - relativeFitness, 0.1, 3.0);

                double effectiveRepro = reproRates[sp] * fitnessModRepro
                    * Math.Max(0, 1.0 - pressure);
                double effectiveDeath = deathRates[sp] * fitnessModDeath
                    + (pressure > 1.0 ? (pressure - 1.0) * 0.15 : 0);
                effectiveDeath = Math.Min(effectiveDeath, 0.5);

                int births = 0, deaths = 0;
                for (int i = 0; i < cur; i++)
                {
                    if (rng.NextDouble() < effectiveRepro) births++;
                    if (rng.NextDouble() < effectiveDeath) deaths++;
                }

                newPop[sp] = Math.Max(0, cur + births - deaths);

                // Hard capacity ceiling.
                int hardCap = Math.Max(10, (int)(effectiveCapacity * 3));
                if (newPop.Values.Sum() > hardCap)
                {
                    double ratio = (double)hardCap / newPop.Values.Sum();
                    foreach (string s in species)
                        if (newPop.ContainsKey(s) && newPop[s] > 0)
                            newPop[s] = Math.Max(0, (int)(newPop[s] * ratio));
                }

                // Stochastic extinction at low population.
                if (newPop[sp] < 2 && cur > 2 && rng.NextDouble() < 1.0 / Math.Max(newPop[sp], 1))
                {
                    newPop[sp] = 0;
                    extinctSet.Add(sp);
                    extinctions++;
                }
            }

            populations = newPop;
        }

        // Analyze results.
        var finalFreqs = populations.Values.Sum() > 0
            ? populations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / populations.Values.Sum())
            : new Dictionary<string, double>();

        string dominant = populations.OrderByDescending(kv => kv.Value).First().Key;
        bool selectionDetected = false;
        foreach (string sp in species)
        {
            double initF = initialFreqs.GetValueOrDefault(sp, 0.25);
            double finalF = finalFreqs.GetValueOrDefault(sp, 0);
            if (Math.Abs(finalF - initF) > 0.05)
                selectionDetected = true;
        }

        bool competitionObserved = extinctions > 0;
        bool coexistenceObserved = populations.Values.Count(v => v > 5) >= 3;

        // Rank stability: compare ranking (by w) vs baseline (A>D>B>C by r/c).
        var baselineRank = new Dictionary<string, int> { ["A"] = 0, ["D"] = 1, ["B"] = 2, ["C"] = 3 };
        var modelRank = fitnessValues.OrderByDescending(kv => kv.Value)
            .Select((kv, idx) => (kv.Key, idx))
            .ToDictionary(x => x.Key, x => x.idx);

        int conc = 0, disc = 0;
        string[] spList = { "A", "B", "C", "D" };
        for (int i = 0; i < 4; i++)
        for (int j = i + 1; j < 4; j++)
        {
            int obI = baselineRank[spList[i]], obJ = baselineRank[spList[j]];
            int prI = modelRank.GetValueOrDefault(spList[i], 0), prJ = modelRank.GetValueOrDefault(spList[j], 0);
            if ((obI < obJ && prI < prJ) || (obI > obJ && prI > prJ)) conc++;
            else disc++;
        }
        double tau = (conc + disc) > 0 ? (double)(conc - disc) / (conc + disc) : 0;

        double maxF = fitnessValues.Values.Max();
        double minF = fitnessValues.Values.Max() > 0 ? fitnessValues.Values.Min() / Math.Max(fitnessValues.Values.Max(), 0.001) : 1;
        double fitDiff = minF > 0 ? maxF / Math.Max(fitnessValues.Values.Min(), 0.000001) : 1;

        int initTotal = initialPop * species.Length;
        int finalTotal = populations.Values.Sum();
        double popChange = initTotal > 0 ? (double)(finalTotal - initTotal) / initTotal : 0;

        bool evolutionPersisted = selectionDetected || competitionObserved || coexistenceObserved;

        string notes = "";
        if (!selectionDetected && !competitionObserved)
            notes = "No Darwinian signature — evolution absent";
        else if (selectionDetected && competitionObserved)
            notes = "Full Darwinian dynamics — selection + competition";
        else
            notes = "Partial Darwinian signature";

        return new AlternativeFitnessModel.ModelRunResult(
            model.Name, regime.Name, extinctions,
            selectionDetected, competitionObserved, coexistenceObserved,
            dominant, fitDiff, tau, popChange,
            evolutionPersisted, notes);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run all model × regime combinations.
    // ══════════════════════════════════════════════════════════════════

    public static List<AlternativeFitnessModel.ModelRunResult> RunAll(
        List<AlternativeFitnessModel.FitnessModelSpec> models,
        List<AlternativeFitnessModel.ResourceRegime> regimes,
        int seedsPerConfig = 3)
    {
        var results = new List<AlternativeFitnessModel.ModelRunResult>();
        var rng = new Random(42);

        foreach (var model in models)
        foreach (var regime in regimes)
        for (int s = 0; s < seedsPerConfig; s++)
        {
            results.Add(RunModel(model, regime, rng.Next()));
        }

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute universality metrics from all results.
    // ══════════════════════════════════════════════════════════════════

    public static AlternativeFitnessModel.UniversalityMetrics ComputeMetrics(
        List<AlternativeFitnessModel.ModelRunResult> results,
        List<AlternativeFitnessModel.FitnessModelSpec> models,
        List<AlternativeFitnessModel.ResourceRegime> regimes)
    {
        int total = results.Count;
        int sel = results.Count(r => r.SelectionDetected);
        int ext = results.Count(r => r.Extinctions > 0);
        int comp = results.Count(r => r.CompetitionObserved);
        int coex = results.Count(r => r.CoexistenceObserved);

        double selIdx = total > 0 ? (double)sel / total : 0;
        double evolScore = total > 0
            ? results.Average(r => (r.SelectionDetected ? 0.4 : 0)
                                 + (r.CompetitionObserved ? 0.3 : 0)
                                 + (r.CoexistenceObserved ? 0.15 : 0)
                                 + (r.EvolutionPersisted ? 0.15 : 0))
            : 0;

        double rankStab = results.Average(r => r.RankStability);

        // Find most universal model.
        var modelScores = models.Select(m =>
        {
            var runs = results.Where(r => r.FitnessModel == m.Name).ToList();
            double score = runs.Count > 0
                ? runs.Average(r => (r.EvolutionPersisted ? 1.0 : 0)) : 0;
            return (m.Name, score);
        }).OrderByDescending(x => x.score).ToList();

        string bestModel = modelScores.FirstOrDefault().Name ?? "None";

        // Find most robust regime.
        var regimeScores = regimes.Select(r =>
        {
            var runs = results.Where(rr => rr.ResourceRegime == r.Name).ToList();
            double score = runs.Count > 0
                ? runs.Average(rr => (rr.EvolutionPersisted ? 1.0 : 0)) : 0;
            return (r.Name, score);
        }).OrderByDescending(x => x.score).ToList();

        string bestRegime = regimeScores.FirstOrDefault().Name ?? "None";

        bool isUniversal = selIdx > 0.5 && evolScore > 0.5;

        string classification;
        if (!isUniversal && selIdx < 0.2)
            classification = "A: Evolution Artifact — evolution disappears under model changes";
        else if (!isUniversal && selIdx < 0.5)
            classification = "B: Model-Dependent Evolution — evolution exists but is fragile";
        else if (isUniversal && evolScore < 0.8)
            classification = "C: Robust Evolution — evolution persists across most models and regimes";
        else
            classification = "D: Universal Evolution Principle — evolution is an inevitable emergent phenomenon";

        string verdict = isUniversal
            ? $"EVOLUTION IS ROBUST. Selection robustness index: {selIdx:P0}. "
              + $"Evolution persistence score: {evolScore:P0}. "
              + $"Most universal fitness model: {bestModel}. "
              + $"Most robust resource regime: {bestRegime}. "
              + $"Information evolution is NOT an artifact of the r/c fitness model — "
              + $"it persists across {sel}/{total} alternative configurations."
            : $"EVOLUTION IS MODEL-DEPENDENT. Selection robustness index: only {selIdx:P0}. "
              + $"Evolution does not survive radical changes to fitness models and resource regimes.";

        return new AlternativeFitnessModel.UniversalityMetrics(
            total, sel, ext, comp, coex,
            selIdx, evolScore, rankStab,
            bestModel, bestRegime,
            isUniversal, classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute species rank stability across models.
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, double> ComputeRankStability(
        List<AlternativeFitnessModel.ModelRunResult> results)
    {
        var stability = new Dictionary<string, double>();
        string[] species = { "A", "B", "C", "D" };

        foreach (string sp in species)
        {
            int dominantCount = results.Count(r => r.DominantSpecies == sp && r.EvolutionPersisted);
            double domFrac = results.Count > 0 ? (double)dominantCount / results.Count : 0;
            stability[sp] = domFrac;
        }

        return stability;
    }

    // ══════════════════════════════════════════════════════════════════
    // Discover the hidden invariant — what survives all modifications?
    // ══════════════════════════════════════════════════════════════════

    public static string DiscoverHiddenInvariant(
        List<AlternativeFitnessModel.ModelRunResult> results)
    {
        var sb = new System.Text.StringBuilder();

        double selFraction = results.Count(r => r.SelectionDetected) / (double)Math.Max(results.Count, 1);
        double extFraction = results.Count(r => r.Extinctions > 0) / (double)Math.Max(results.Count, 1);
        double coexFraction = results.Count(r => r.CoexistenceObserved) / (double)Math.Max(results.Count, 1);
        double evolFraction = results.Count(r => r.EvolutionPersisted) / (double)Math.Max(results.Count, 1);

        sb.Append("The invariant across all models and regimes is: ");

        if (evolFraction > 0.7)
            sb.Append("EVOLUTION ITSELF. Darwinian dynamics emerge regardless of how fitness is defined. ");
        else if (extFraction > 0.5)
            sb.Append("RESOURCE PRESSURE. Extinctions occur whenever resources are constrained. ");
        else if (coexFraction > 0.5)
            sb.Append("COEXISTENCE. Species find ways to coexist across diverse conditions. ");
        else if (selFraction > 0.3)
            sb.Append("WEAK SELECTION. Differential survival exists but is context-dependent. ");
        else
            sb.Append("NONE. No invariant survives all modifications. Evolution is model-dependent. ");

        sb.Append($"Selection: {selFraction:P0}, Extinction: {extFraction:P0}, "
                + $"Coexistence: {coexFraction:P0}, Evolution: {evolFraction:P0}.");

        return sb.ToString();
    }
}
