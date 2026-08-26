using System.Collections.Concurrent;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Performs parameter sweeps over (N, connection density ρ, coupling strength K)
/// to identify critical thresholds for synchronization cluster formation.
/// </summary>
public sealed class CriticalDensityScanner
{
    public int SimulationIterations { get; set; } = 5000;
    public int RandomSeed { get; set; } = 271;
    public double TimeStep { get; set; } = 0.01;
    public int CheckpointInterval { get; set; } = 500;

    /// <summary>
    /// Represents one point in the parameter space.
    /// </summary>
    public record ScanPoint(int N, double Density, double K);

    /// <summary>
    /// Results for one parameter combination.
    /// </summary>
    public record ScanResult(
        double GlobalR,
        int TotalClustersDetected,
        double MaxClusterSize,
        double MeanClusterSize,
        double MaxClusterLifetime,
        double MeanClusterLifetime,
        double MeanPersistenceScore,
        int LongLivedClusterCount, // clusters with lifetime ≥ 500
        List<SynchronizationCluster> AllClusters);

    /// <summary>
    /// Runs a full parameter sweep. Returns results keyed by scan point.
    /// </summary>
    public Dictionary<ScanPoint, ScanResult> Sweep(
        int[] nodeCounts,
        double[] densities,
        double[] couplingStrengths)
    {
        var points = new List<ScanPoint>();
        foreach (int n in nodeCounts)
            foreach (double rho in densities)
                foreach (double k in couplingStrengths)
                    points.Add(new ScanPoint(n, rho, k));

        var results = new ConcurrentDictionary<ScanPoint, ScanResult>();

        Parallel.ForEach(points, point =>
        {
            var result = SimulateOne(point);
            results[point] = result;
        });

        return new Dictionary<ScanPoint, ScanResult>(results);
    }

    private ScanResult SimulateOne(ScanPoint point)
    {
        var rng = new Random(RandomSeed + point.N * 7919 + (int)(point.Density * 10000) + (int)(point.K * 1000));

        // Build network.
        var network = new TemporalNetwork(point.N);
        for (int i = 0; i < point.N; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            network.AddNode(new TemporalNode(i, phase: phase, frequency: 1.0));
        }

        // Build sparse coupling matrix with density ρ.
        for (int i = 0; i < point.N; i++)
        {
            for (int j = i + 1; j < point.N; j++)
            {
                if (rng.NextDouble() < point.Density)
                {
                    network.Matrix[i, j] = 1.0;
                    network.Matrix[j, i] = 1.0;
                }
            }
        }

        // Run simulation.
        var sim = new TemporalSimulation(network)
        {
            TimeStep = TimeStep,
            CouplingStrength = point.K
        };

        var analyzer = new SynchronizationClusterAnalyzer
        {
            SyncWindow = 0.3,
            MinSyncThreshold = 0.90,
            MinClusterSize = 2,
            OverlapThreshold = 0.5
        };

        double finalR = 0;

        for (int iter = 0; iter < SimulationIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % CheckpointInterval == 0 || iter == SimulationIterations - 1)
            {
                var metrics = SynchronizationMetrics.FromNetwork(network, iter + 1);
                finalR = metrics.OrderParameterR;

                analyzer.DetectAndTrack(network, iter + 1);
            }
        }

        var allClusters = analyzer.GetAllClusters();
        int totalClusters = allClusters.Count;
        double maxSize = allClusters.Count > 0 ? allClusters.Max(c => c.Size) : 0;
        double meanSize = allClusters.Count > 0 ? allClusters.Average(c => c.Size) : 0;
        int maxLifetime = allClusters.Count > 0 ? allClusters.Max(c => c.Lifetime) : 0;
        double meanLifetime = allClusters.Count > 0 ? allClusters.Average(c => c.Lifetime) : 0;
        double meanPersistence = allClusters.Count > 0
            ? allClusters.Average(c => c.PersistenceScore)
            : 0;
        int longLived = allClusters.Count(c => c.Lifetime >= 500);

        return new ScanResult(
            finalR,
            totalClusters,
            maxSize,
            meanSize,
            maxLifetime,
            meanLifetime,
            meanPersistence,
            longLived,
            allClusters);
    }
}
