using System.Collections.Concurrent;
using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes the reproducibility of resonance families across multiple independent
/// simulation runs with different random seeds.
///
/// Matches detected clusters to reference family signatures from TQM-007
/// and computes occurrence rates, statistical stability, and reproducibility scores.
/// </summary>
public sealed class ResonanceFamilyReproducibilityAnalyzer
{
    /// <summary>
    /// Reference family signatures from TQM-007 (centroids in 5D feature space).
    /// Features: [sync, freq, energy, lifetime/5000, size/200].
    /// </summary>
    private static readonly (int Id, double[] Centroid, string Name)[] ReferenceFamilies =
    {
        (0, new[] { 0.9723, 1.289,  0.0,    12.0/5000,   7.5/200 }, "F0 Transient"),
        (1, new[] { 0.9596, 1.269,  0.0,  4167.0/5000, 199.7/200 }, "F1 Stable Large"),
        (2, new[] { 0.9025, 1.201,  0.0,  4500.0/5000,  49.0/200 }, "F2 Ultra-Stable Compact"),
        (3, new[] { 0.9831, 1.227,  0.0,  4500.0/5000,  75.0/200 }, "F3 Stable Medium"),
        (4, new[] { 0.9421, 1.231,  0.0,  4000.0/5000, 100.0/200 }, "F4 Stable Coherent"),
    };

    /// <summary>
    /// Maximum normalized distance for a cluster to match a reference family.
    /// </summary>
    public double MatchThreshold { get; set; } = 0.5;

    /// <summary>
    /// Minimum family size (across runs) for reproducibility classification.
    /// </summary>
    public int MinReproducibilityOccurrences { get; set; } = 5;

    /// <summary>
    /// Runs a batch of reproducibility simulations for the given parameter set.
    /// </summary>
    public Dictionary<int, ResonanceFamilyReproducibilityResult> AnalyzeReproducibility(
        int n,
        double rho,
        double k,
        int iterations,
        int seedStart,
        int seedCount)
    {
        var familyOccurrences = new ConcurrentDictionary<int, List<SynchronizationCluster>>();
        for (int fid = 0; fid < ReferenceFamilies.Length; fid++)
            familyOccurrences[fid] = new List<SynchronizationCluster>();

        int totalRuns = 0;

        Parallel.For(seedStart, seedStart + seedCount, seed =>
        {
            var clusters = RunOneSimulation(n, rho, k, iterations, seed);

            foreach (var cluster in clusters)
            {
                int matchedFamily = MatchToFamily(cluster);
                if (matchedFamily >= 0)
                {
                    familyOccurrences[matchedFamily].Add(cluster);
                }
            }

            Interlocked.Increment(ref totalRuns);
        });

        // Compute reproducibility statistics for each family.
        var results = new Dictionary<int, ResonanceFamilyReproducibilityResult>();

        for (int fid = 0; fid < ReferenceFamilies.Length; fid++)
        {
            var occurrences = familyOccurrences[fid];

            if (occurrences.Count == 0)
            {
                results[fid] = new ResonanceFamilyReproducibilityResult(
                    fid, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, totalRuns, "Not Detected", 0);
                continue;
            }

            double occRate = (double)occurrences.Count / totalRuns;

            var lifetimes = occurrences.Select(c => (double)c.Lifetime).ToList();
            var sizes = occurrences.Select(c => (double)c.Size).ToList();
            var freqs = occurrences.Select(c => c.MeanFrequency).ToList();
            var syncs = occurrences.Select(c => c.Synchronization).ToList();

            double meanLifetime = lifetimes.Average();
            double stdLifetime = StdDev(lifetimes, meanLifetime);
            double meanSize = sizes.Average();
            double stdSize = StdDev(sizes, meanSize);
            double meanFreq = freqs.Average();
            double stdFreq = StdDev(freqs, meanFreq);
            double meanSync = syncs.Average();
            double stdSync = StdDev(syncs, meanSync);

            // Coefficient of variation (normalized variance).
            double cvLifetime = meanLifetime > 0 ? stdLifetime / meanLifetime : 1;
            double cvSize = meanSize > 0 ? stdSize / meanSize : 1;
            double cvFreq = meanFreq > 0 ? stdFreq / meanFreq : 1;
            double cvSync = meanSync > 0 ? stdSync / meanSync : 1;

            double avgCV = (cvLifetime + cvSize + cvFreq + cvSync) / 4.0;
            double reproducibilityScore = occRate * (1.0 - Math.Min(1.0, avgCV));

            string classification = ClassifyFamily(occRate, reproducibilityScore, occurrences.Count);

            results[fid] = new ResonanceFamilyReproducibilityResult(
                fid,
                occRate,
                meanLifetime,
                stdLifetime,
                meanSize,
                stdSize,
                meanFreq,
                stdFreq,
                meanSync,
                stdSync,
                occurrences.Count,
                totalRuns,
                classification,
                reproducibilityScore);
        }

        return results;
    }

    /// <summary>
    /// Runs a single simulation and returns all detected synchronization clusters.
    /// </summary>
    private List<SynchronizationCluster> RunOneSimulation(
        int n, double rho, double k, int iterations, int seed)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Use heterogeneous frequencies for family diversity.
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            network.AddNode(new TemporalNode(i, phase: phase, frequency: freq));
        }

        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (rng.NextDouble() < rho)
                {
                    network.Matrix[i, j] = 1.0;
                    network.Matrix[j, i] = 1.0;
                }

        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = k };

        var analyzer = new SynchronizationClusterAnalyzer
        {
            SyncWindow = 0.3,
            MinSyncThreshold = 0.90,
            MinClusterSize = 2,
            OverlapThreshold = 0.5
        };

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if ((iter + 1) % 500 == 0 || iter == iterations - 1)
                analyzer.DetectAndTrack(network, iter + 1);
        }

        return analyzer.GetAllClusters();
    }

    /// <summary>
    /// Matches a cluster to the closest reference family by Euclidean distance.
    /// Returns the family ID, or -1 if no match.
    /// </summary>
    private int MatchToFamily(SynchronizationCluster cluster)
    {
        // Normalize cluster features to match reference scale.
        double[] features =
        {
            cluster.Synchronization,
            cluster.MeanFrequency,
            cluster.MeanEnergy,
            cluster.Lifetime / 5000.0,
            cluster.Size / 200.0
        };

        int bestMatch = -1;
        double bestDist = double.MaxValue;

        for (int fid = 0; fid < ReferenceFamilies.Length; fid++)
        {
            double dist = EuclideanDistance(features, ReferenceFamilies[fid].Centroid);
            if (dist < bestDist && dist < MatchThreshold)
            {
                bestDist = dist;
                bestMatch = fid;
            }
        }

        return bestMatch;
    }

    private static string ClassifyFamily(double occRate, double reproScore, int occurrences)
    {
        if (occRate >= 0.70 && reproScore >= 0.6)
            return "Universal";
        if (occRate >= 0.40 && reproScore >= 0.3)
            return "Likely Universal";
        if (occRate >= 0.10)
            return "Unstable";
        if (occurrences > 0)
            return "Seed Artifact";
        return "Not Detected";
    }

    private static double EuclideanDistance(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += (a[i] - b[i]) * (a[i] - b[i]);
        return Math.Sqrt(sum);
    }

    private static double StdDev(List<double> values, double mean)
    {
        if (values.Count < 2) return 0;
        double sumSq = values.Sum(v => (v - mean) * (v - mean));
        return Math.Sqrt(sumSq / values.Count);
    }
}
