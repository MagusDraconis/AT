namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Computes species measurements, evaluates candidate fitness functions,
/// performs correlation/regression analysis, and constructs the fitness landscape.
///
/// TQM-136: Information Fitness Law
/// </summary>
public static class FitnessLandscape
{
    // ══════════════════════════════════════════════════════════════════
    // Compute all species measurements from patterns.
    // ══════════════════════════════════════════════════════════════════

    public static List<FitnessCandidate.SpeciesMeasurements> MeasureAllSpecies()
    {
        var measurements = new List<FitnessCandidate.SpeciesMeasurements>();
        string[] species = { "A", "B", "C", "D" };

        // TQM-135 observed values (ground truth).
        var observedSC = new Dictionary<string, double>
        { ["A"] = 0.009, ["D"] = -0.041, ["B"] = -0.082, ["C"] = -0.100 };
        var observedDF = new Dictionary<string, double>
        { ["A"] = 0.01, ["B"] = 0.08, ["D"] = -0.09, ["C"] = -0.09 };
        var observedDom = new Dictionary<string, string>
        { ["A"] = "Dominant", ["B"] = "Dominant", ["C"] = "Marginal", ["D"] = "Intermediate" };

        // TQM-135 rates.
        var reproRates = new Dictionary<string, double>
        { ["A"] = 0.08, ["B"] = 0.06, ["C"] = 0.05, ["D"] = 0.12 };
        var deathRates = new Dictionary<string, double>
        { ["A"] = 0.03, ["B"] = 0.05, ["C"] = 0.06, ["D"] = 0.08 };

        // TQM-134 mutation rates.
        var mutationRates = new Dictionary<string, double>
        { ["A"] = 0.23, ["B"] = 0.27, ["C"] = 0.27, ["D"] = 0.09 };

        foreach (string sp in species)
        {
            var def = SpeciesReproductionProfile.SpeciesDefinitions[sp];
            var pattern = def.Pattern;

            // Pattern energy.
            double energy = pattern.Sum(x => x * x);

            // Shannon entropy.
            double entropy = ComputeShannonEntropy(pattern);

            // Coherence (simplified: 1 / variance).
            double mean = pattern.Average();
            double variance = pattern.Average(x => (x - mean) * (x - mean));
            double coherence = variance > 1e-10 ? 1.0 / (1.0 + Math.Sqrt(variance)) : 1.0;

            // Dominant frequency (Fourier).
            int n = pattern.Length;
            double re = 0, im = 0;
            for (int i = 0; i < n; i++)
            {
                double angle = 2 * Math.PI * i / n;
                re += pattern[i] * Math.Cos(angle);
                im += pattern[i] * Math.Sin(angle);
            }
            double domFreq = Math.Sqrt(re * re + im * im) / n;

            // Zero crossings.
            int zc = 0;
            for (int i = 1; i < n; i++)
                if (pattern[i] * pattern[i - 1] < 0) zc++;

            // Resource consumption.
            var cons = ResourceConstraintModel.GetConsumption(sp);
            double totalCons = cons.AmplitudeConsumption + cons.MemoryConsumption
                + cons.CoherenceConsumption + cons.LifetimeConsumption
                + cons.SpatialConsumption + cons.BandwidthConsumption;

            double repro = reproRates[sp];
            double death = deathRates[sp];
            double mutRate = mutationRates[sp];
            double mutRobustness = mutRate > 0 ? 1.0 / mutRate : 10;

            // Memory persistence: related to death rate (lower death = more persistent).
            double persistence = death > 0 ? 1.0 / death : 100;

            // Information density: entropy per resource unit.
            double infoDensity = totalCons > 0 ? entropy / totalCons : 0;

            measurements.Add(new FitnessCandidate.SpeciesMeasurements(
                sp, energy, entropy, coherence, domFreq, zc,
                totalCons, repro, death, mutRobustness, persistence,
                infoDensity,
                observedSC.GetValueOrDefault(sp, 0),
                observedDF.GetValueOrDefault(sp, 0),
                observedDom.GetValueOrDefault(sp, "Unknown")));
        }

        return measurements;
    }

    // ══════════════════════════════════════════════════════════════════
    // Evaluate all candidate fitness functions.
    // ══════════════════════════════════════════════════════════════════

    public static List<FitnessCandidate.FitnessFunction> EvaluateCandidates(
        List<FitnessCandidate.SpeciesMeasurements> measurements)
    {
        var candidates = new List<FitnessCandidate.FitnessFunction>();
        string[] speciesOrder = { "A", "B", "C", "D" };

        // Build lookup tables.
        var mDict = measurements.ToDictionary(m => m.SpeciesName);
        var observed = speciesOrder.Select(s => mDict[s].ObservedSelectionCoefficient).ToArray();

        // Generate all candidate functions.
        foreach (string sp in speciesOrder)
        {
            var m = mDict[sp];
            // Pre-compute common values.
        }

        // ── Parameter-free candidates ──

        // 1. Reproduction rate alone.
        candidates.Add(EvaluateFormula("Reproduction Rate", "w = r",
            speciesOrder.Select(s => mDict[s].ReproductionRate).ToArray(),
            observed, 0));

        // 2. Inverse consumption.
        candidates.Add(EvaluateFormula("Inverse Consumption", "w = 1/c",
            speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray(),
            observed, 0));

        // 3. Resource efficiency (r/c) — TQM-135 default.
        candidates.Add(EvaluateFormula("Resource Efficiency", "w = r / c",
            speciesOrder.Select(s => mDict[s].ReproductionRate / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray(),
            observed, 0));

        // 4. Coherence.
        candidates.Add(EvaluateFormula("Coherence", "w = C",
            speciesOrder.Select(s => mDict[s].Coherence).ToArray(),
            observed, 0));

        // 5. Inverse entropy (order preference).
        candidates.Add(EvaluateFormula("Order (1/Entropy)", "w = 1/H",
            speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ShannonEntropy, 0.01)).ToArray(),
            observed, 0));

        // 6. Pattern energy.
        candidates.Add(EvaluateFormula("Pattern Energy", "w = E",
            speciesOrder.Select(s => mDict[s].PatternEnergy).ToArray(),
            observed, 0));

        // 7. Mutation robustness.
        candidates.Add(EvaluateFormula("Mutation Robustness", "w = 1/μ",
            speciesOrder.Select(s => mDict[s].MutationRobustness).ToArray(),
            observed, 0));

        // 8. Reproduction × Coherence.
        candidates.Add(EvaluateFormula("Repro × Coherence", "w = r · C",
            speciesOrder.Select(s => mDict[s].ReproductionRate * mDict[s].Coherence).ToArray(),
            observed, 0));

        // 9. Reproduction × Energy.
        candidates.Add(EvaluateFormula("Repro × Energy", "w = r · E",
            speciesOrder.Select(s => mDict[s].ReproductionRate * mDict[s].PatternEnergy).ToArray(),
            observed, 0));

        // 10. Efficiency × Coherence.
        candidates.Add(EvaluateFormula("Efficiency × Coherence", "w = (r/c) · C",
            speciesOrder.Select(s => (mDict[s].ReproductionRate / Math.Max(mDict[s].ResourceConsumption, 0.01)) * mDict[s].Coherence).ToArray(),
            observed, 0));

        // 11. Information density.
        candidates.Add(EvaluateFormula("Information Density", "w = H/c",
            speciesOrder.Select(s => mDict[s].InformationDensity).ToArray(),
            observed, 0));

        // 12. Reproduction × Information Density.
        candidates.Add(EvaluateFormula("Repro × Info Density", "w = r · H/c",
            speciesOrder.Select(s => mDict[s].ReproductionRate * mDict[s].InformationDensity).ToArray(),
            observed, 0));

        // 13. Memory persistence.
        candidates.Add(EvaluateFormula("Memory Persistence", "w = 1/d",
            speciesOrder.Select(s => mDict[s].MemoryPersistence).ToArray(),
            observed, 0));

        // 14. Dominant frequency.
        candidates.Add(EvaluateFormula("Dominant Frequency", "w = f_dom",
            speciesOrder.Select(s => mDict[s].DominantFrequency).ToArray(),
            observed, 0));

        // 15. Zero crossings (complexity).
        candidates.Add(EvaluateFormula("Complexity", "w = ZC",
            speciesOrder.Select(s => (double)mDict[s].ZeroCrossings).ToArray(),
            observed, 0));

        // ── 1-parameter fitted candidates ──

        // 16. Linear fit to reproduction rate.
        candidates.Add(FitLinearModel("Reproduction (fitted)", "w = a·r + b",
            speciesOrder.Select(s => mDict[s].ReproductionRate).ToArray(),
            observed));

        // 17. Linear fit to inverse consumption.
        candidates.Add(FitLinearModel("1/Consumption (fitted)", "w = a/c + b",
            speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray(),
            observed));

        // 18. Linear fit to efficiency.
        candidates.Add(FitLinearModel("Efficiency (fitted)", "w = a·r/c + b",
            speciesOrder.Select(s => mDict[s].ReproductionRate / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray(),
            observed));

        // 19. Linear fit to coherence.
        candidates.Add(FitLinearModel("Coherence (fitted)", "w = a·C + b",
            speciesOrder.Select(s => mDict[s].Coherence).ToArray(),
            observed));

        return candidates.OrderByDescending(c => Math.Abs(c.SpearmanRho)).ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Find best multivariate model via stepwise regression.
    // ══════════════════════════════════════════════════════════════════

    public static FitnessCandidate.MultivariateModel FindBestMultivariate(
        List<FitnessCandidate.SpeciesMeasurements> measurements)
    {
        string[] speciesOrder = { "A", "B", "C", "D" };
        var mDict = measurements.ToDictionary(m => m.SpeciesName);

        // Candidate variables.
        var variables = new (string Name, double[] Values)[]
        {
            ("r", speciesOrder.Select(s => mDict[s].ReproductionRate).ToArray()),
            ("1/c", speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray()),
            ("C", speciesOrder.Select(s => mDict[s].Coherence).ToArray()),
            ("1/H", speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ShannonEntropy, 0.01)).ToArray()),
            ("E", speciesOrder.Select(s => mDict[s].PatternEnergy).ToArray()),
            ("1/μ", speciesOrder.Select(s => mDict[s].MutationRobustness).ToArray()),
        };

        double[] observed = speciesOrder.Select(s => mDict[s].ObservedSelectionCoefficient).ToArray();
        int n = speciesOrder.Length;

        // Try all single-variable models.
        var bestModel = FindBestSingleVar(variables, observed, n);

        // Try all 2-variable combinations.
        for (int i = 0; i < variables.Length; i++)
        for (int j = i + 1; j < variables.Length; j++)
        {
            var model = FitTwoVar(variables[i], variables[j], observed, n);
            if (model.AdjustedR2 > bestModel.AdjustedR2)
                bestModel = model;
        }

        // Try 3-variable combination with best 2.
        var topVars = variables
            .Select((v, idx) => (v, Math.Abs(Correlation(v.Values, observed).pearson)))
            .OrderByDescending(x => x.Item2)
            .Take(3)
            .Select(x => x.v)
            .ToArray();

        if (topVars.Length >= 3)
        {
            var model3 = FitThreeVar(topVars[0], topVars[1], topVars[2], observed, n);
            if (model3.AdjustedR2 > bestModel.AdjustedR2)
                bestModel = model3;
        }

        return bestModel;
    }

    // ══════════════════════════════════════════════════════════════════
    // Build 2D fitness landscape.
    // ══════════════════════════════════════════════════════════════════

    public static FitnessCandidate.FitnessLandscape2D BuildLandscape(
        List<FitnessCandidate.SpeciesMeasurements> measurements,
        FitnessCandidate.FitnessFunction bestCandidate)
    {
        // Use the two variables most correlated with fitness.
        string[] speciesOrder = { "A", "B", "C", "D" };
        var mDict = measurements.ToDictionary(m => m.SpeciesName);

        // Find the top 2 candidate variables by correlation with observed.
        var candidateVars = new (string Name, double[] Values)[]
        {
            ("Efficiency (r/c)", speciesOrder.Select(s => mDict[s].ReproductionRate
                / Math.Max(mDict[s].ResourceConsumption, 0.01)).ToArray()),
            ("Coherence (C)", speciesOrder.Select(s => mDict[s].Coherence).ToArray()),
            ("Reproduction (r)", speciesOrder.Select(s => mDict[s].ReproductionRate).ToArray()),
            ("Order (1/H)", speciesOrder.Select(s => 1.0 / Math.Max(mDict[s].ShannonEntropy, 0.01)).ToArray()),
            ("Energy (E)", speciesOrder.Select(s => mDict[s].PatternEnergy).ToArray()),
        };

        double[] observed = speciesOrder.Select(s => mDict[s].ObservedSelectionCoefficient).ToArray();

        var sorted = candidateVars
            .Select(v => (v.Name, v.Values, corr: Math.Abs(Correlation(v.Values, observed).pearson)))
            .OrderByDescending(x => x.corr)
            .Take(2)
            .ToArray();

        string varX = sorted[0].Name;
        string varY = sorted[1].Name;
        var xVals = sorted[0].Values;
        var yVals = sorted[1].Values;

        // Create a grid of fitness values using linear interpolation.
        int gridSize = 10;
        double xMin = xVals.Min(), xMax = xVals.Max();
        double yMin = yVals.Min(), yMax = yVals.Max();
        double xPad = (xMax - xMin) * 0.1, yPad = (yMax - yMin) * 0.1;

        var gridX = new List<double>();
        var gridY = new List<double>();
        var gridF = new List<double>();

        for (int i = 0; i < gridSize; i++)
        for (int j = 0; j < gridSize; j++)
        {
            double x = xMin - xPad + (xMax - xMin + 2 * xPad) * i / (gridSize - 1);
            double y = yMin - yPad + (yMax - yMin + 2 * yPad) * j / (gridSize - 1);

            // Fitness = weighted combination based on best candidate.
            double f = PredictFitness(x, y, xVals, yVals, observed);

            gridX.Add(x);
            gridY.Add(y);
            gridF.Add(f);
        }

        var fArr = gridF.ToArray();
        double maxF = fArr.Max();
        int maxIdx = Array.IndexOf(fArr, maxF);

        string shape = fArr.Count(v => v > maxF * 0.8) < gridSize ? "Single Peak"
                     : fArr.Count(v => v > maxF * 0.5) < gridSize * 3 ? "Ridge"
                     : "Flat";

        return new FitnessCandidate.FitnessLandscape2D(
            varX, varY,
            gridX.ToArray(), gridY.ToArray(), fArr,
            gridX[maxIdx], gridY[maxIdx], maxF, shape);
    }

    // ══════════════════════════════════════════════════════════════════
    // Validate predictions against TQM-135 outcomes.
    // ══════════════════════════════════════════════════════════════════

    public static double ValidatePredictions(
        FitnessCandidate.FitnessFunction bestCandidate,
        List<FitnessCandidate.SpeciesMeasurements> measurements)
    {
        // Compare predicted ranking vs observed ranking.
        var mDict = measurements.ToDictionary(m => m.SpeciesName);
        string[] species = { "A", "B", "C", "D" };

        // Observed ranking: by SelectionCoefficient.
        var observedRank = species
            .OrderByDescending(s => mDict[s].ObservedSelectionCoefficient)
            .ToList();

        // Predicted ranking: by best candidate's fitness values.
        var predictedRank = bestCandidate.SpeciesValues
            .OrderByDescending(kv => kv.Value)
            .Select(kv => kv.Key)
            .ToList();

        // Also validate against dominance classification.
        int correctDominance = 0;
        foreach (string sp in species)
        {
            bool isDominant = mDict[sp].ObservedDominance == "Dominant";
            bool predictedHigh = predictedRank.IndexOf(sp) < 2; // top half
            if (isDominant == predictedHigh)
                correctDominance++;
        }

        // Kendall tau for ranking agreement.
        int concordant = 0, discordant = 0;
        for (int i = 0; i < species.Length; i++)
        for (int j = i + 1; j < species.Length; j++)
        {
            int obsI = observedRank.IndexOf(species[i]);
            int obsJ = observedRank.IndexOf(species[j]);
            int predI = predictedRank.IndexOf(species[i]);
            int predJ = predictedRank.IndexOf(species[j]);

            if ((obsI < obsJ && predI < predJ) || (obsI > obsJ && predI > predJ))
                concordant++;
            else
                discordant++;
        }

        double tau = (double)(concordant - discordant) / (concordant + discordant);

        // Weighted accuracy: Kendall tau (0.6) + dominance accuracy (0.4).
        double dominanceAcc = (double)correctDominance / species.Length;
        double accuracy = tau * 0.6 + dominanceAcc * 0.4;

        return accuracy;
    }

    // ══════════════════════════════════════════════════════════════════
    // Statistical helpers.
    // ══════════════════════════════════════════════════════════════════

    private static FitnessCandidate.FitnessFunction EvaluateFormula(
        string name, string formula, double[] values, double[] observed, int paramCount)
    {
        var (pearson, spearman) = Correlation(values, observed);

        double r2 = pearson * pearson;

        // AICc with n=4.
        int n = values.Length;
        int k = paramCount + 1; // +1 for intercept (implicit in param-free)
        double rss = 0;
        for (int i = 0; i < n; i++)
            rss += (values[i] - observed[i]) * (values[i] - observed[i]);
        double aicc = n * Math.Log(Math.Max(rss / n, 1e-10)) + 2 * k + 2 * k * (k + 1) / Math.Max(n - k - 1, 1);

        string rank = Math.Abs(spearman) > 0.8 ? "Excellent"
                    : Math.Abs(spearman) > 0.6 ? "Good"
                    : Math.Abs(spearman) > 0.4 ? "Moderate"
                    : Math.Abs(spearman) > 0.2 ? "Weak"
                    : "None";

        bool significant = Math.Abs(spearman) >= 0.7;

        var speciesDict = new Dictionary<string, double>();
        string[] species = { "A", "B", "C", "D" };
        for (int i = 0; i < n && i < species.Length; i++)
            speciesDict[species[i]] = values[i];

        return new FitnessCandidate.FitnessFunction(
            name, formula, paramCount, speciesDict, observed,
            pearson, spearman, r2, aicc, rank, significant);
    }

    private static FitnessCandidate.FitnessFunction FitLinearModel(
        string name, string formula, double[] x, double[] y)
    {
        int n = x.Length;
        double meanX = x.Average(), meanY = y.Average();
        double cov = 0, varX = 0;
        for (int i = 0; i < n; i++) { cov += (x[i] - meanX) * (y[i] - meanY); varX += (x[i] - meanX) * (x[i] - meanX); }
        double a = varX > 1e-10 ? cov / varX : 0;
        double b = meanY - a * meanX;

        var predicted = x.Select(xi => a * xi + b).ToArray();
        var (pearson, spearman) = Correlation(predicted, y);
        double r2 = pearson * pearson;

        int k = 2; // a + b
        double rss = 0;
        for (int i = 0; i < n; i++)
            rss += (predicted[i] - y[i]) * (predicted[i] - y[i]);
        double aicc = n * Math.Log(Math.Max(rss / n, 1e-10)) + 2 * k + 2 * k * (k + 1) / Math.Max(n - k - 1, 1);

        string rank = Math.Abs(spearman) > 0.8 ? "Excellent"
                    : Math.Abs(spearman) > 0.6 ? "Good"
                    : Math.Abs(spearman) > 0.4 ? "Moderate"
                    : Math.Abs(spearman) > 0.2 ? "Weak" : "None";

        bool significant = Math.Abs(spearman) >= 0.7;

        var speciesDict = new Dictionary<string, double>();
        string[] species = { "A", "B", "C", "D" };
        for (int i = 0; i < n && i < species.Length; i++)
            speciesDict[species[i]] = predicted[i];

        return new FitnessCandidate.FitnessFunction(
            name, formula, 1, speciesDict, y,
            pearson, spearman, r2, aicc, rank, significant);
    }

    private static (double pearson, double spearman) Correlation(double[] a, double[] b)
    {
        int n = Math.Min(a.Length, b.Length);
        if (n < 2) return (0, 0);

        double ma = a.Take(n).Average(), mb = b.Take(n).Average();
        double cov = 0, va = 0, vb = 0;
        for (int i = 0; i < n; i++) { cov += (a[i] - ma) * (b[i] - mb); va += (a[i] - ma) * (a[i] - ma); vb += (b[i] - mb) * (b[i] - mb); }
        double pearson = (va > 1e-10 && vb > 1e-10) ? cov / Math.Sqrt(va * vb) : 0;

        // Spearman: rank correlation.
        var rankA = GetRanks(a.Take(n).ToArray());
        var rankB = GetRanks(b.Take(n).ToArray());
        double mrA = rankA.Average(), mrB = rankB.Average();
        double covR = 0, vrA = 0, vrB = 0;
        for (int i = 0; i < n; i++) { covR += (rankA[i] - mrA) * (rankB[i] - mrB); vrA += (rankA[i] - mrA) * (rankA[i] - mrA); vrB += (rankB[i] - mrB) * (rankB[i] - mrB); }
        double spearman = (vrA > 1e-10 && vrB > 1e-10) ? covR / Math.Sqrt(vrA * vrB) : 0;

        return (pearson, spearman);
    }

    private static double[] GetRanks(double[] values)
    {
        int n = values.Length;
        var indexed = values.Select((v, i) => (v, i)).OrderBy(x => x.v).ToArray();
        var ranks = new double[n];
        for (int i = 0; i < n; i++)
            ranks[indexed[i].i] = i + 1;
        return ranks;
    }

    private static FitnessCandidate.MultivariateModel FindBestSingleVar(
        (string Name, double[] Values)[] variables, double[] observed, int n)
    {
        FitnessCandidate.MultivariateModel best = new(
            Array.Empty<string>(), Array.Empty<double>(), 0, 0, 0, double.MaxValue, "none");

        foreach (var v in variables)
        {
            var model = FitOneVar(v, observed, n);
            if (model.AICC < best.AICC && model.AdjustedR2 > best.AdjustedR2)
                best = model;
        }
        if (best.Variables.Length == 0)
            best = FitOneVar(variables[0], observed, n);
        return best;
    }

    private static FitnessCandidate.MultivariateModel FitOneVar(
        (string Name, double[] Values) v, double[] y, int n)
    {
        double[] x = v.Values;
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0;
        for (int i = 0; i < n; i++) { cov += (x[i] - mx) * (y[i] - my); vx += (x[i] - mx) * (x[i] - mx); }
        double a = vx > 1e-10 ? cov / vx : 0;
        double b = my - a * mx;

        double rss = 0, tss = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = a * x[i] + b;
            rss += (pred - y[i]) * (pred - y[i]);
            tss += (y[i] - my) * (y[i] - my);
        }
        double r2 = tss > 1e-10 ? 1 - rss / tss : 0;
        double adjR2 = n > 3 ? 1 - (1 - r2) * (n - 1) / (n - 3) : r2;
        int k = 2;
        double aicc = n * Math.Log(Math.Max(rss / n, 1e-10)) + 2 * k + 2 * k * (k + 1) / Math.Max(n - k - 1, 1);

        string formula = $"w = {a:F4}·{v.Name} + {b:F4}";

        return new FitnessCandidate.MultivariateModel(
            new[] { v.Name }, new[] { a }, b, r2, adjR2, aicc, formula);
    }

    private static FitnessCandidate.MultivariateModel FitTwoVar(
        (string Name, double[] Values) v1, (string Name, double[] Values) v2,
        double[] y, int n)
    {
        // Solve normal equations for w = a1·x1 + a2·x2 + b.
        double[] x1 = v1.Values, x2 = v2.Values;
        double s11 = 0, s12 = 0, s22 = 0, s1y = 0, s2y = 0, sy = 0;
        for (int i = 0; i < n; i++)
        {
            s11 += x1[i] * x1[i]; s12 += x1[i] * x2[i]; s22 += x2[i] * x2[i];
            s1y += x1[i] * y[i]; s2y += x2[i] * y[i]; sy += y[i];
        }
        double sx1 = x1.Sum(), sx2 = x2.Sum();

        // Center variables.
        double mx1 = sx1 / n, mx2 = sx2 / n, my = sy / n;
        double c11 = 0, c12 = 0, c22 = 0, c1y = 0, c2y = 0;
        for (int i = 0; i < n; i++)
        {
            c11 += (x1[i] - mx1) * (x1[i] - mx1);
            c12 += (x1[i] - mx1) * (x2[i] - mx2);
            c22 += (x2[i] - mx2) * (x2[i] - mx2);
            c1y += (x1[i] - mx1) * (y[i] - my);
            c2y += (x2[i] - mx2) * (y[i] - my);
        }

        double det = c11 * c22 - c12 * c12;
        double a1 = det > 1e-10 ? (c1y * c22 - c2y * c12) / det : 0;
        double a2 = det > 1e-10 ? (c2y * c11 - c1y * c12) / det : 0;
        double b = my - a1 * mx1 - a2 * mx2;

        double rss = 0, tss = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = a1 * x1[i] + a2 * x2[i] + b;
            rss += (pred - y[i]) * (pred - y[i]);
            tss += (y[i] - my) * (y[i] - my);
        }
        double r2 = tss > 1e-10 ? 1 - rss / tss : 0;
        double adjR2 = n > 4 ? 1 - (1 - r2) * (n - 1) / (n - 4) : r2;
        int k = 3;
        double aicc = n * Math.Log(Math.Max(rss / n, 1e-10)) + 2 * k + 2 * k * (k + 1) / Math.Max(n - k - 1, 1);

        string formula = $"w = {a1:F4}·{v1.Name} + {a2:F4}·{v2.Name} + {b:F4}";

        return new FitnessCandidate.MultivariateModel(
            new[] { v1.Name, v2.Name }, new[] { a1, a2 }, b, r2, adjR2, aicc, formula);
    }

    private static FitnessCandidate.MultivariateModel FitThreeVar(
        (string Name, double[] Values) v1, (string Name, double[] Values) v2,
        (string Name, double[] Values) v3, double[] y, int n)
    {
        double[] x1 = v1.Values, x2 = v2.Values, x3 = v3.Values;
        double mx1 = x1.Average(), mx2 = x2.Average(), mx3 = x3.Average(), my = y.Average();

        double c11 = 0, c12 = 0, c13 = 0, c22 = 0, c23 = 0, c33 = 0, c1y = 0, c2y = 0, c3y = 0;
        for (int i = 0; i < n; i++)
        {
            c11 += (x1[i] - mx1) * (x1[i] - mx1);
            c12 += (x1[i] - mx1) * (x2[i] - mx2);
            c13 += (x1[i] - mx1) * (x3[i] - mx3);
            c22 += (x2[i] - mx2) * (x2[i] - mx2);
            c23 += (x2[i] - mx2) * (x3[i] - mx3);
            c33 += (x3[i] - mx3) * (x3[i] - mx3);
            c1y += (x1[i] - mx1) * (y[i] - my);
            c2y += (x2[i] - mx2) * (y[i] - my);
            c3y += (x3[i] - mx3) * (y[i] - my);
        }

        // Solve 3×3 system via Cramer's rule.
        double det = c11 * (c22 * c33 - c23 * c23)
                   - c12 * (c12 * c33 - c23 * c13)
                   + c13 * (c12 * c23 - c22 * c13);

        double a1 = det > 1e-10 ? (c1y * (c22 * c33 - c23 * c23)
                                 - c12 * (c2y * c33 - c23 * c3y)
                                 + c13 * (c2y * c23 - c22 * c3y)) / det : 0;
        double a2 = det > 1e-10 ? (c11 * (c2y * c33 - c23 * c3y)
                                 - c1y * (c12 * c33 - c23 * c13)
                                 + c13 * (c12 * c3y - c2y * c13)) / det : 0;
        double a3 = det > 1e-10 ? (c11 * (c22 * c3y - c2y * c23)
                                 - c12 * (c12 * c3y - c2y * c13)
                                 + c1y * (c12 * c23 - c22 * c13)) / det : 0;
        double b = my - a1 * mx1 - a2 * mx2 - a3 * mx3;

        double rss = 0, tss = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = a1 * x1[i] + a2 * x2[i] + a3 * x3[i] + b;
            rss += (pred - y[i]) * (pred - y[i]);
            tss += (y[i] - my) * (y[i] - my);
        }
        double r2 = tss > 1e-10 ? 1 - rss / tss : 0;
        double adjR2 = n > 5 ? 1 - (1 - r2) * (n - 1) / (n - 5) : r2;
        int k = 4;
        double aicc = n * Math.Log(Math.Max(rss / n, 1e-10)) + 2 * k + 2 * k * (k + 1) / Math.Max(n - k - 1, 1);

        string formula = $"w = {a1:F4}·{v1.Name} + {a2:F4}·{v2.Name} + {a3:F4}·{v3.Name} + {b:F4}";

        return new FitnessCandidate.MultivariateModel(
            new[] { v1.Name, v2.Name, v3.Name }, new[] { a1, a2, a3 }, b, r2, adjR2, aicc, formula);
    }

    private static double PredictFitness(double x, double y, double[] xVals, double[] yVals, double[] fVals)
    {
        // Simple distance-weighted interpolation.
        double totalWeight = 0, weightedSum = 0;
        for (int i = 0; i < xVals.Length; i++)
        {
            double dx = (x - xVals[i]) / Math.Max(Math.Abs(xVals.Max() - xVals.Min()), 0.01);
            double dy = (y - yVals[i]) / Math.Max(Math.Abs(yVals.Max() - yVals.Min()), 0.01);
            double dist = Math.Sqrt(dx * dx + dy * dy);
            double weight = Math.Exp(-dist * dist * 10);
            weightedSum += fVals[i] * weight;
            totalWeight += weight;
        }
        return totalWeight > 1e-10 ? weightedSum / totalWeight : 0;
    }

    /// <summary>
    /// Shannon entropy from histogram.
    /// </summary>
    private static double ComputeShannonEntropy(double[] pattern)
    {
        int nBins = 8;
        var hist = new int[nBins];
        double min = pattern.Min(), max = pattern.Max();
        double range = max - min;
        if (range < 1e-10) return 0;

        foreach (double v in pattern)
        {
            int b = (int)((v - min) / range * nBins);
            b = Math.Clamp(b, 0, nBins - 1);
            hist[b]++;
        }

        double h = 0;
        int total = pattern.Length;
        foreach (int c in hist)
            if (c > 0) { double p = (double)c / total; h -= p * Math.Log(p); }
        return h;
    }
}
