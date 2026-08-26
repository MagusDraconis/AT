using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Detects and tracks phase-synchronized oscillator clusters in a Kuramoto network.
///
/// A synchronization cluster is a set of oscillators whose phases are tightly grouped
/// (within a configurable window) and whose internal order parameter exceeds a threshold.
/// </summary>
public sealed class SynchronizationClusterAnalyzer
{
    /// <summary>
    /// Maximum phase difference (in radians) for two oscillators to be considered synchronized.
    /// </summary>
    public double SyncWindow { get; set; } = 0.3; // rad

    /// <summary>
    /// Minimum internal order parameter for a group to qualify as a cluster.
    /// </summary>
    public double MinSyncThreshold { get; set; } = 0.90;

    /// <summary>
    /// Minimum cluster size (number of oscillators) to be considered significant.
    /// </summary>
    public int MinClusterSize { get; set; } = 2;

    /// <summary>
    /// Jaccard overlap threshold for tracking clusters across iterations.
    /// </summary>
    public double OverlapThreshold { get; set; } = 0.5;

    private readonly Dictionary<int, SynchronizationCluster> _activeClusters = new();
    private int _nextClusterId;

    /// <summary>
    /// Detects clusters in the current network state and tracks them against previous detections.
    /// </summary>
    public List<SynchronizationCluster> DetectAndTrack(TemporalNetwork network, int iteration)
    {
        var current = DetectClusters(network, iteration);
        TrackClusters(current);
        return current;
    }

    /// <summary>
    /// Finds groups of oscillators with tightly clustered phases (connected components
    /// in the phase-proximity graph).
    /// </summary>
    private List<SynchronizationCluster> DetectClusters(TemporalNetwork network, int iteration)
    {
        int n = network.NodeCount;
        var nodes = network.Nodes;

        // Build adjacency: edge if phase difference < syncWindow.
        var adjacency = new List<int>[n];
        for (int i = 0; i < n; i++)
            adjacency[i] = new List<int>();

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double diff = Math.Abs(TemporalSimulation.NormalizePhase(
                    nodes[i].Phase - nodes[j].Phase + Math.PI) - Math.PI);

                if (diff < SyncWindow)
                {
                    adjacency[i].Add(j);
                    adjacency[j].Add(i);
                }
            }
        }

        // Find connected components via BFS.
        var visited = new bool[n];
        var clusters = new List<SynchronizationCluster>();

        for (int i = 0; i < n; i++)
        {
            if (visited[i]) continue;

            var component = new List<int>();
            var queue = new Queue<int>();
            queue.Enqueue(i);
            visited[i] = true;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                component.Add(v);

                foreach (int neighbor in adjacency[v])
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if (component.Count >= MinClusterSize)
            {
                // Compute internal order parameter.
                double sumSin = 0, sumCos = 0;
                foreach (int idx in component)
                {
                    sumSin += Math.Sin(nodes[idx].Phase);
                    sumCos += Math.Cos(nodes[idx].Phase);
                }

                int m = component.Count;
                double r = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / m;
                double avgPhase = Math.Atan2(sumSin, sumCos);

                if (r >= MinSyncThreshold)
                {
                    // Compute mean frequency and energy for the cluster.
                    double meanFreq = 0, meanEnergy = 0;
                    foreach (int idx in component)
                    {
                        meanFreq += nodes[idx].Frequency;
                        meanEnergy += nodes[idx].Energy;
                    }
                    meanFreq /= m;
                    meanEnergy /= m;

                    var cluster = new SynchronizationCluster(
                        -1, component, r, iteration, avgPhase)
                    {
                        MeanFrequency = meanFreq,
                        MeanEnergy = meanEnergy
                    };

                    clusters.Add(cluster);
                }
            }
        }

        return clusters;
    }

    /// <summary>
    /// Matches newly detected clusters to previously tracked ones using Jaccard overlap.
    /// </summary>
    private void TrackClusters(List<SynchronizationCluster> current)
    {
        var unmatched = new HashSet<int>(_activeClusters.Keys);

        foreach (var newCluster in current)
        {
            int bestMatch = -1;
            double bestOverlap = 0;

            foreach (var (id, oldCluster) in _activeClusters)
            {
                double overlap = JaccardOverlap(oldCluster.NodeIds, newCluster.NodeIds);
                if (overlap > bestOverlap && overlap >= OverlapThreshold)
                {
                    bestOverlap = overlap;
                    bestMatch = id;
                }
            }

            if (bestMatch >= 0)
            {
                var existing = _activeClusters[bestMatch];
                existing.DeathIteration = newCluster.BirthIteration;

                int totalWindow = existing.DeathIteration - existing.BirthIteration + 1;
                existing.PersistenceScore = totalWindow > 0
                    ? (double)existing.Lifetime / totalWindow
                    : 0;

                newCluster.ClusterId = bestMatch;
                unmatched.Remove(bestMatch);
            }
            else
            {
                int newId = _nextClusterId++;
                newCluster.ClusterId = newId;
                newCluster.PersistenceScore = 0;
                _activeClusters[newId] = newCluster;
            }
        }

        foreach (int id in unmatched)
            _activeClusters.Remove(id);
    }

    /// <summary>
    /// Computes Jaccard similarity: |A ∩ B| / |A ∪ B|.
    /// </summary>
    private static double JaccardOverlap(List<int> a, List<int> b)
    {
        var setA = new HashSet<int>(a);
        var setB = new HashSet<int>(b);

        int intersect = setA.Count(id => setB.Contains(id));
        int union = new HashSet<int>(setA.Concat(setB)).Count;

        return union > 0 ? (double)intersect / union : 0;
    }

    /// <summary>
    /// Returns all clusters that have ever been tracked, with updated statistics.
    /// </summary>
    public List<SynchronizationCluster> GetAllClusters()
    {
        return _activeClusters.Values.ToList();
    }

    /// <summary>
    /// Resets the tracker state for a fresh scan.
    /// </summary>
    public void Reset()
    {
        _activeClusters.Clear();
        _nextClusterId = 0;
    }
}
