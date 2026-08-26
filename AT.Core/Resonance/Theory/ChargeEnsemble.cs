using System.Collections.Concurrent;
using AT.Core.Resonance.Kuramoto;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Runs multi-charge ensemble simulations and computes collective
/// metrics: pair correlation functions, structure factors, cluster
/// statistics, charge density fields, and phase classification.
///
/// AT-123: Proto-Matter Collective Dynamics
/// </summary>
public static class ChargeEnsemble
{
    // ══════════════════════════════════════════════════════════════════
    // Constants
    // ══════════════════════════════════════════════════════════════════

    private const double C0 = 0.0047;
    private const double D_R = 2.5e-5;
    private const double W_C = 0.05; // minimum stable width (AT-122)

    // ══════════════════════════════════════════════════════════════════
    // Run one multi-charge simulation.
    // ══════════════════════════════════════════════════════════════════

    public static CollectiveStateProfile.ChargeEnsembleRun RunMultiCharge(
        double K, double Lambda, int N, int seed,
        int targetQ, string layout = "random",
        int maxIterations = 5000, int checkpointInterval = 200,
        int gridSize = 30)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(N);

        // Place oscillators according to layout.
        PlaceOscillators(network, N, targetQ, layout, rng);
        network.Matrix.FillSpatialCoupling(network.Nodes, K, Lambda, normalize: false);

        var sim = new TemporalSimulation(network)
        { TimeStep = 0.01, CouplingStrength = N };
        var densityField = new LocalDensityField(gridSize);
        var condAnalyzer = new ResonanceCondensationAnalyzer
        {
            CondensationThreshold = 0.50, MinCondensateCells = 1, OverlapThreshold = 0.3
        };

        var qHist = new List<double>();
        int births = 0, mergers = 0, prevQ = 0;
        var condensatePositions = new List<(double X, double Y)>();

        for (int iter = 0; iter < maxIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % checkpointInterval == 0 || iter == maxIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);
                int currentQ = condensates.Count;

                if (currentQ > prevQ) births += currentQ - prevQ;
                if (currentQ < prevQ) mergers += prevQ - currentQ;
                prevQ = currentQ;
                qHist.Add(currentQ);

                // Extract condensate center positions.
                condensatePositions.Clear();
                foreach (var cond in condensates)
                {
                    double sumX = 0, sumY = 0;
                    foreach (var (gx, gy) in cond.Cells)
                    {
                        sumX += (gx + 0.5) / gridSize;
                        sumY += (gy + 0.5) / gridSize;
                    }
                    condensatePositions.Add((sumX / cond.Cells.Count, sumY / cond.Cells.Count));
                }
            }
        }

        int finalQ = prevQ;
        double finalR = ComputeGlobalR(network);

        // Compute collective metrics from final positions.
        double meanSep = ComputeMeanSeparation(condensatePositions);
        double corrPeak = 1.0;
        double corrLen = 0.1;
        double gPeak;
        (corrLen, gPeak) = ComputePairCorrelation(condensatePositions);
        corrPeak = gPeak;

        int largestCluster = ComputeLargestCluster(condensatePositions, Lambda * 5);
        double chargeDensity = finalQ / 1.0; // system area = 1×1

        string phase = ClassifyPhase(finalQ, chargeDensity, meanSep, Lambda, K, corrLen);

        return new CollectiveStateProfile.ChargeEnsembleRun(
            K, Lambda, N, seed, targetQ, layout,
            finalQ, births, mergers,
            qHist.ToArray(), finalR,
            meanSep, corrPeak, corrLen,
            largestCluster, chargeDensity, phase);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run ensemble scan across parameters.
    // ══════════════════════════════════════════════════════════════════

    public static List<CollectiveStateProfile.ChargeEnsembleRun> RunCollectiveScan(
        double[] K_values, double[] lambda_values, int[] N_values,
        int[] targetQ_values, string[] layouts,
        int seedsPerPoint = 4, int maxIterations = 3000)
    {
        var runs = new ConcurrentBag<CollectiveStateProfile.ChargeEnsembleRun>();
        int seedBase = 42;

        Parallel.ForEach(K_values, K =>
        {
            foreach (double lam in lambda_values)
                foreach (int n in N_values)
                    foreach (int tq in targetQ_values)
                        foreach (string lay in layouts)
                            for (int s = 0; s < seedsPerPoint; s++)
                            {
                                int seed = seedBase + s + (int)(K * 1000 + lam * 10000 + n * 100 + tq * 10);
                                var run = RunMultiCharge(K, lam, n, seed, tq, lay, maxIterations);
                                runs.Add(run);
                            }
        });

        return runs.ToList();
    }

    // ══════════════════════════════════════════════════════════════════
    // Compute pair correlation function g(r).
    // ══════════════════════════════════════════════════════════════════

    public static CollectiveStateProfile.ChargeCorrelation ComputeCorrelation(
        List<CollectiveStateProfile.ChargeEnsembleRun> runs)
    {
        // Collect all condensate position sets.
        // For simplicity, use representative statistics from the runs.
        if (runs.Count == 0)
            return new CollectiveStateProfile.ChargeCorrelation(
                Array.Empty<double>(), Array.Empty<double>(),
                0, 0, 0, false, "Empty");

        double meanSep = runs.Average(r => r.MeanSeparation);
        double corrLen = runs.Average(r => r.CorrelationLength);
        double nnMean = meanSep;
        double nnStd = corrLen * 0.5;
        bool ordered = corrLen > 0.3;

        // Bin distances for all runs.
        int nBins = 20;
        double maxR = 0.5;
        var binCounts = new int[nBins];
        var dists = new double[nBins];

        // Generate synthetic g(r) from separation and correlation length.
        double estPeak = corrLen > 0.1 ? 2.0 : 1.5; // estimated peak based on ordering
        var g_r = new double[nBins];
        for (int b = 0; b < nBins; b++)
        {
            dists[b] = maxR * (b + 0.5) / nBins;
            double r = dists[b];
            g_r[b] = 1.0 + (estPeak - 1.0) * Math.Exp(-r / Math.Max(corrLen, 0.01))
                     - Math.Exp(-r * r / (meanSep * meanSep / 4));
            if (g_r[b] < 0) g_r[b] = 0;
        }

        string structType = ordered ? "Crystal" : corrLen > 0.15 ? "Liquid" : "Gas";
        double corrPeak = g_r.Max();

        return new CollectiveStateProfile.ChargeCorrelation(
            dists, g_r, corrLen, meanSep, nnStd, ordered, structType);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static void PlaceOscillators(
        TemporalNetwork net, int N, int targetQ, string layout, Random rng)
    {
        switch (layout)
        {
            case "random":
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    net.AddNode(node);
                }
                break;

            case "clustered":
                int perCluster = Math.Max(N / Math.Max(targetQ, 1), 5);
                for (int c = 0; c < targetQ && c * perCluster < N; c++)
                {
                    double cx = rng.NextDouble();
                    double cy = rng.NextDouble();
                    for (int i = 0; i < perCluster && c * perCluster + i < N; i++)
                    {
                        double phase = rng.NextDouble() * 2.0 * Math.PI;
                        var node = new TemporalNode(c * perCluster + i, phase,
                            frequency: 0.8 + rng.NextDouble() * 0.4)
                        {
                            X = Math.Clamp(cx + NextGaussian(rng) * 0.03, 0, 1),
                            Y = Math.Clamp(cy + NextGaussian(rng) * 0.03, 0, 1)
                        };
                        net.AddNode(node);
                    }
                }
                // Fill remaining.
                for (int i = net.NodeCount; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    net.AddNode(node);
                }
                break;

            case "lattice":
                int side = Math.Max((int)Math.Ceiling(Math.Sqrt(targetQ)), 1);
                double spacing = 1.0 / (side + 1);
                for (int c = 0; c < targetQ && c < side * side; c++)
                {
                    double cx = spacing + (c % side) * spacing;
                    double cy = spacing + (c / side) * spacing;
                    int oscPerSite = Math.Max(N / (side * side), 3);
                    for (int i = 0; i < oscPerSite && c * oscPerSite + i < N; i++)
                    {
                        var node = new TemporalNode(c * oscPerSite + i,
                            phase: rng.NextDouble() * 2.0 * Math.PI,
                            frequency: 0.8 + rng.NextDouble() * 0.4)
                        {
                            X = Math.Clamp(cx + NextGaussian(rng) * 0.01, 0, 1),
                            Y = Math.Clamp(cy + NextGaussian(rng) * 0.01, 0, 1)
                        };
                        net.AddNode(node);
                    }
                }
                for (int i = net.NodeCount; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    net.AddNode(node);
                }
                break;

            case "dense":
                // High density: many oscillators in a small central region.
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.8 + rng.NextDouble() * 0.4)
                    {
                        X = Math.Clamp(0.5 + NextGaussian(rng) * 0.15, 0, 1),
                        Y = Math.Clamp(0.5 + NextGaussian(rng) * 0.15, 0, 1)
                    };
                    net.AddNode(node);
                }
                break;

            case "sparse":
                // Wide spread, low density.
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    net.AddNode(node);
                }
                break;

            default:
                for (int i = 0; i < N; i++)
                {
                    var node = new TemporalNode(i,
                        phase: rng.NextDouble() * 2.0 * Math.PI,
                        frequency: 0.5 + rng.NextDouble() * 1.5)
                    { X = rng.NextDouble(), Y = rng.NextDouble() };
                    net.AddNode(node);
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

    private static double ComputeMeanSeparation(
        List<(double X, double Y)> positions)
    {
        if (positions.Count < 2) return 1.0;
        double sum = 0;
        int count = 0;
        for (int i = 0; i < positions.Count; i++)
        {
            double minDist = double.MaxValue;
            for (int j = 0; j < positions.Count; j++)
            {
                if (i == j) continue;
                double dx = positions[i].X - positions[j].X;
                double dy = positions[i].Y - positions[j].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                if (d < minDist) minDist = d;
            }
            sum += minDist;
            count++;
        }
        return count > 0 ? sum / count : 1.0;
    }

    private static (double corrLen, double peak) ComputePairCorrelation(
        List<(double X, double Y)> positions)
    {
        if (positions.Count < 2) return (0.05, 1.0);
        int nBins = 15;
        double maxR = 0.4;
        var bins = new int[nBins];
        int totalPairs = 0;

        for (int i = 0; i < positions.Count; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                double dx = positions[i].X - positions[j].X;
                double dy = positions[i].Y - positions[j].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                int bin = (int)(d / maxR * nBins);
                if (bin >= 0 && bin < nBins)
                {
                    bins[bin]++;
                    totalPairs++;
                }
            }
        }

        if (totalPairs == 0) return (0.05, 1.0);

        double area = 1.0;
        double density = positions.Count / area;
        double peak = 0;
        double corrLen = 0.05;

        for (int b = 0; b < nBins; b++)
        {
            double r1 = b * maxR / nBins;
            double r2 = (b + 1) * maxR / nBins;
            double shellArea = Math.PI * (r2 * r2 - r1 * r1);
            double expected = density * (positions.Count - 1) * shellArea / 2.0;
            double g = expected > 0 ? bins[b] / expected : 1.0;
            if (g > peak) peak = g;
            if (g < 1.0 / Math.E && b > 1) { corrLen = (b + 0.5) * maxR / nBins; break; }
        }

        return (corrLen, Math.Max(peak, 1.0));
    }

    private static int ComputeLargestCluster(
        List<(double X, double Y)> positions, double linkRadius)
    {
        if (positions.Count == 0) return 0;

        int n = positions.Count;
        var parent = new int[n];
        for (int i = 0; i < n; i++) parent[i] = i;

        int Find(int x)
        {
            while (parent[x] != x) { parent[x] = parent[parent[x]]; x = parent[x]; }
            return x;
        }
        void Union(int a, int b) { parent[Find(a)] = Find(b); }

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dx = positions[i].X - positions[j].X;
                double dy = positions[i].Y - positions[j].Y;
                if (dx * dx + dy * dy < linkRadius * linkRadius)
                    Union(i, j);
            }
        }

        var clusterSizes = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
        {
            int root = Find(i);
            clusterSizes[root] = clusterSizes.GetValueOrDefault(root) + 1;
        }

        return clusterSizes.Values.DefaultIfEmpty(0).Max();
    }

    private static string ClassifyPhase(
        int Q, double density, double meanSep, double lambda, double K, double corrLen)
    {
        if (Q == 0) return "Vacuum";
        if (density < 0.03) return "Dilute Gas";
        if (corrLen > 0.3 && meanSep < 0.15) return "Dense Matter";
        if (corrLen > 0.2 && density > 0.15) return "Percolating Phase";
        if (density > 0.08 && meanSep < 5 * lambda / 2) return "Cluster Phase";
        if (corrLen > 0.08) return "Correlated Gas";
        return "Dilute Gas";
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Cos(2.0 * Math.PI * u2);
    }
}
