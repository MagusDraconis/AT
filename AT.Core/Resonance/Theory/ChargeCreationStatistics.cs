using System.Collections.Concurrent;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Runs massive ensembles of charge creation experiments to determine
/// the statistical law governing P(Q) — the probability distribution
/// of topological charge creation.
///
/// AT-119: Topological Charge Creation Statistics
/// </summary>
public static class ChargeCreationStatistics
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>Result of a single simulation run.</summary>
    public sealed record ChargeCreationRun(
        double K, double Lambda, int N, int Seed,
        string InitialCondition,
        int Q_final,
        int Q_creation_time,
        int NumberOfBirths,
        int NumberOfMergers,
        double PeakR,
        double PeakM,
        double FinalGlobalR,
        double CreationIteration,
        List<int> Q_history);

    /// <summary>Ensemble statistics for one parameter point.</summary>
    public sealed record ParameterPointStats(
        double K, double Lambda, int N, string InitialCondition,
        int TotalRuns,
        double MeanQ,
        double VarianceQ,
        double P_Q0,
        double P_Q1,
        double P_Q2,
        double P_Q3plus,
        int[] Q_histogram,
        double MeanCreationTime,
        double MeanBirths,
        double MeanMergers,
        double MeanPeakR,
        double MeanPeakM,
        double MeanFinalGlobalR,
        double CreationProbability,
        string BestDistribution,
        double BestDistributionPValue,
        Dictionary<string, double> DistributionScores);

    /// <summary>Full ensemble report.</summary>
    public sealed record ChargeEnsembleReport(
        List<ParameterPointStats> PointStats,
        List<ChargeCreationRun> AllRuns,
        string OverallBestDistribution,
        double OverallBestScore,
        bool UniversalLawFound,
        string CriticalScalingAnalysis,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Run a single simulation and track charge creation.
    // ══════════════════════════════════════════════════════════════════

    public static ChargeCreationRun RunSingle(
        double K, double Lambda, int N, int seed,
        string initialCondition = "random",
        int maxIterations = 5000,
        int checkpointInterval = 100,
        int gridSize = 20)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);

        CreateInitialCondition(network, N, initialCondition, rng);
        network.Matrix.FillSpatialCoupling(network.Nodes, K, Lambda, normalize: false);

        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = N };
        var densityField = new LocalDensityField(gridSize);
        var condAnalyzer = new ResonanceCondensationAnalyzer
        {
            CondensationThreshold = 0.80,
            MinCondensateCells = 2,
            OverlapThreshold = 0.3
        };

        int q_final = 0;
        int q_creation_time = -1; // iteration when Q first becomes > 0
        int numberOfBirths = 0;
        int numberOfMergers = 0;
        double peakR = 0;
        double peakM = 0;
        double finalGlobalR = 0;
        double creationIteration = -1;
        var qHistory = new List<int>();

        int prevQ = 0;
        bool chargeCreated = false;

        for (int iter = 0; iter < maxIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == 0 || iter == maxIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);
                int currentQ = condensates.Count;

                // Track charge creation.
                if (!chargeCreated && currentQ > 0)
                {
                    chargeCreated = true;
                    q_creation_time = iter + 1;
                    creationIteration = iter + 1;
                }

                // Count births (Q increases).
                if (currentQ > prevQ)
                    numberOfBirths += currentQ - prevQ;

                // Count mergers (Q decreases).
                if (currentQ < prevQ)
                    numberOfMergers += prevQ - currentQ;

                // Track peaks.
                double localR = densityField.MaxLocalR();
                if (localR > peakR) peakR = localR;

                double localM = ComputeMeanCoupling(network);
                if (localM > peakM) peakM = localM;

                prevQ = currentQ;
                qHistory.Add(currentQ);
            }
        }

        q_final = prevQ;
        finalGlobalR = ComputeGlobalR(network);
        peakM = Math.Max(peakM, ComputeMeanCoupling(network));

        return new ChargeCreationRun(
            K, Lambda, N, seed, initialCondition,
            q_final, q_creation_time, numberOfBirths, numberOfMergers,
            peakR, peakM, finalGlobalR, creationIteration, qHistory);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run ensemble for one parameter point.
    // ══════════════════════════════════════════════════════════════════

    public static ParameterPointStats RunPointEnsemble(
        double K, double Lambda, int N,
        string initialCondition = "random",
        int seeds = 1000,
        int maxIterations = 5000,
        int checkpointInterval = 100)
    {
        var results = new ConcurrentBag<ChargeCreationRun>();

        Parallel.For(0, seeds, seed =>
        {
            var run = RunSingle(K, Lambda, N, seed + 1000 * (int)(K * 100 + Lambda * 1000),
                initialCondition, maxIterations, checkpointInterval);
            results.Add(run);
        });

        var runs = results.ToList();
        int total = runs.Count;

        double meanQ = runs.Average(r => r.Q_final);
        double varQ = runs.Average(r => r.Q_final * r.Q_final) - meanQ * meanQ;

        int maxQ = runs.Max(r => r.Q_final);
        int[] qHist = new int[maxQ + 4]; // +4 for safety
        foreach (var r in runs) qHist[Math.Min(r.Q_final, qHist.Length - 1)]++;

        double pQ0 = (double)runs.Count(r => r.Q_final == 0) / total;
        double pQ1 = (double)runs.Count(r => r.Q_final == 1) / total;
        double pQ2 = (double)runs.Count(r => r.Q_final == 2) / total;
        double pQ3plus = (double)runs.Count(r => r.Q_final >= 3) / total;

        double meanCreationTime = runs.Where(r => r.Q_creation_time > 0)
            .DefaultIfEmpty(new ChargeCreationRun(0, 0, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, new List<int>()))
            .Average(r => r.Q_creation_time > 0 ? r.Q_creation_time : 0.0);
        double meanBirths = runs.Average(r => r.NumberOfBirths);
        double meanMergers = runs.Average(r => r.NumberOfMergers);
        double meanPeakR = runs.Average(r => r.PeakR);
        double meanPeakM = runs.Average(r => r.PeakM);
        double meanFinalR = runs.Average(r => r.FinalGlobalR);
        double creationProb = (double)runs.Count(r => r.Q_final > 0) / total;

        // Fit distributions and find best.
        var scores = ChargeDistributionModel.FitAllDistributions(qHist, total, meanQ);
        string bestDist = scores.OrderByDescending(kv => kv.Value).First().Key;
        double bestScore = scores[bestDist];

        return new ParameterPointStats(
            K, Lambda, N, initialCondition,
            total, meanQ, varQ, pQ0, pQ1, pQ2, pQ3plus,
            qHist,
            meanCreationTime, meanBirths, meanMergers,
            meanPeakR, meanPeakM, meanFinalR, creationProb,
            bestDist, bestScore, scores);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full parameter scan.
    // ══════════════════════════════════════════════════════════════════

    public static ChargeEnsembleReport RunFullScan(
        double[] K_values,
        double[] lambda_values,
        int[] N_values,
        string[] initialConditions,
        int seedsPerPoint = 1000,
        int maxIterations = 5000)
    {
        var allStats = new ConcurrentBag<ParameterPointStats>();
        var allRuns = new ConcurrentBag<ChargeCreationRun>();

        // Use a subset for manageable runtime; full scan would be huge.
        int totalCombos = K_values.Length * lambda_values.Length * N_values.Length * initialConditions.Length;
        int maxCombos = Math.Min(totalCombos, 60); // limit to 60 parameter combos

        var combos = new List<(double K, double L, int N, string IC)>();
        foreach (double k in K_values)
            foreach (double lam in lambda_values)
                foreach (int n in N_values)
                    foreach (string ic in initialConditions)
                        combos.Add((k, lam, n, ic));

        // Downsample if needed.
        if (combos.Count > maxCombos)
        {
            var rng = new Random(42);
            combos = combos.OrderBy(_ => rng.Next()).Take(maxCombos).ToList();
        }

        var progressLock = new object();
        int completed = 0;

        Parallel.ForEach(combos, combo =>
        {
            var stats = RunPointEnsemble(combo.K, combo.L, combo.N, combo.IC,
                seedsPerPoint, maxIterations);
            allStats.Add(stats);

            lock (progressLock)
            {
                completed++;
                // Progress is tracked but not output during parallel execution.
            }
        });

        var statsList = allStats.OrderBy(s => s.K).ThenBy(s => s.Lambda).ThenBy(s => s.N).ToList();

        // Determine overall best distribution.
        var distWins = new Dictionary<string, int>();
        foreach (var s in statsList)
        {
            if (!distWins.ContainsKey(s.BestDistribution))
                distWins[s.BestDistribution] = 0;
            distWins[s.BestDistribution]++;
        }
        string overallBest = distWins.OrderByDescending(kv => kv.Value).First().Key;
        double overallScore = (double)distWins[overallBest] / statsList.Count;

        bool universalLawFound = overallScore > 0.80;

        string criticalScaling = ChargeStatisticsAnalyzer.AnalyzeCriticalScaling(statsList);

        string classification = universalLawFound
            ? "D: Universal Nucleation Statistics"
            : (overallScore > 0.50 ? "C: Strong Candidate Distribution" : "B: Empirical Distribution");

        string verdict = universalLawFound
            ? $"UNIVERSAL LAW FOUND: {overallBest} distribution governs P(Q) across " +
              $"{overallScore * 100:F0}% of parameter space. Charge creation is a " +
              $"STATISTICAL process obeying {overallBest} statistics."
            : $"NO UNIVERSAL LAW: best candidate {overallBest} wins only " +
              $"{overallScore * 100:F0}% of the time. Charge creation statistics " +
              $"are parameter-dependent.";

        return new ChargeEnsembleReport(
            statsList, allRuns.ToList(),
            overallBest, overallScore, universalLawFound,
            criticalScaling, classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static void CreateInitialCondition(
        TemporalNetwork network, int N, string condition, Random rng)
    {
        switch (condition)
        {
            case "random":
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    network.AddNode(node);
                }
                break;

            case "noise-only":
                // Slightly coherent noise: phases drawn from von Mises with small κ.
                double kappa = 0.5;
                for (int i = 0; i < N; i++)
                {
                    double phase = SampleVonMises(rng, 0, kappa);
                    var node = new TemporalNode(i,
                        phase: phase,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    network.AddNode(node);
                }
                break;

            case "clustered-noise":
                // Positions clustered in 2–4 groups; phases partially coherent within clusters.
                int nClusters = 2 + rng.Next(3);
                var clusterCenters = new (double cx, double cy, double phase)[nClusters];
                for (int c = 0; c < nClusters; c++)
                    clusterCenters[c] = (rng.NextDouble(), rng.NextDouble(),
                        rng.NextDouble() * 2.0 * Math.PI);

                for (int i = 0; i < N; i++)
                {
                    var center = clusterCenters[i % nClusters];
                    double phase = SampleVonMises(rng, center.phase, 2.0);
                    var node = new TemporalNode(i, phase: phase,
                        frequency: 0.8 + rng.NextDouble() * 0.4)
                    {
                        X = Math.Clamp(center.cx + NextGaussian(rng) * 0.05, 0, 1),
                        Y = Math.Clamp(center.cy + NextGaussian(rng) * 0.05, 0, 1)
                    };
                    network.AddNode(node);
                }
                break;

            case "near-uniform":
                // Almost uniform phases with tiny noise.
                double basePhase = rng.NextDouble() * 2.0 * Math.PI;
                for (int i = 0; i < N; i++)
                {
                    double phase = basePhase + (rng.NextDouble() - 0.5) * 0.1;
                    var node = new TemporalNode(i,
                        phase: NormalizePhase(phase),
                        frequency: 0.95 + rng.NextDouble() * 0.1)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    network.AddNode(node);
                }
                break;

            default: // fallback to random
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    network.AddNode(node);
                }
                break;
        }
    }

    private static double ComputeGlobalR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        {
            ss += Math.Sin(net.Nodes[i].Phase);
            sc += Math.Cos(net.Nodes[i].Phase);
        }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    private static double ComputeMeanCoupling(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double sum = 0;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                sum += net.Matrix.GetCoupling(i, j);
                count++;
            }
        }
        return count > 0 ? sum / count : 0;
    }

    private static double SampleVonMises(Random rng, double mu, double kappa)
    {
        if (kappa < 1e-8) return rng.NextDouble() * 2.0 * Math.PI;
        double a = 1.0 + Math.Sqrt(1.0 + 4.0 * kappa * kappa);
        double b = (a - Math.Sqrt(2.0 * a)) / (2.0 * kappa);
        double r = (1.0 + b * b) / (2.0 * b);

        while (true)
        {
            double u1 = rng.NextDouble();
            double z = Math.Cos(Math.PI * u1);
            double f = (1.0 + r * z) / (r + z);
            double c = kappa * (r - f);
            double u2 = rng.NextDouble();
            if (c * (2.0 - c) - u2 > 0 || Math.Log(c / u2) + 1.0 - c >= 0)
                return NormalizePhase(mu + Math.Acos(f) * (rng.NextDouble() < 0.5 ? -1 : 1));
        }
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }

    private static double NormalizePhase(double phase)
    {
        phase %= 2.0 * Math.PI;
        if (phase < 0) phase += 2.0 * Math.PI;
        return phase;
    }
}
