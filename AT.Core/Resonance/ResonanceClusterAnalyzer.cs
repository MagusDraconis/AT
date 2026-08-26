using AT.Core.Temporal;
using AT.Core.TemporalField;
using TemporalFieldClass = AT.Core.TemporalField.TemporalField;

namespace AT.Core.Resonance;

/// <summary>
/// Detects and tracks resonance clusters in a temporal field.
///
/// A resonance cluster is a contiguous region of elevated field density
/// that persists longer than random fluctuations, potentially hosting
/// multiple oscillators in a self-reinforcing structure.
/// </summary>
public sealed class ResonanceClusterAnalyzer
{
    /// <summary>
    /// Density threshold: mean + factor × standard deviation.
    /// </summary>
    public double ThresholdFactor { get; set; } = 1.5;

    /// <summary>
    /// Minimum cluster size (in cells) to be considered significant.
    /// </summary>
    public int MinClusterSize { get; set; } = 3;

    /// <summary>
    /// Overlap fraction required to consider two clusters as the same entity across timesteps.
    /// </summary>
    public double OverlapThreshold { get; set; } = 0.3;

    private readonly Dictionary<int, ResonanceCluster> _activeClusters = new();
    private int _nextClusterId;

    /// <summary>
    /// Detects clusters in the current field snapshot and tracks them against
    /// previously detected clusters.
    /// </summary>
    public List<ResonanceCluster> DetectAndTrack(
        TemporalFieldClass field,
        int iteration,
        TemporalNetwork network,
        int[] oscillatorPositions)
    {
        var current = DetectClusters(field, iteration, network, oscillatorPositions);
        var tracked = TrackClusters(current);
        return tracked;
    }

    /// <summary>
    /// Scans the field for contiguous regions where density exceeds the threshold.
    /// </summary>
    private List<ResonanceCluster> DetectClusters(
        TemporalFieldClass field,
        int iteration,
        TemporalNetwork network,
        int[] oscillatorPositions)
    {
        int n = field.CellCount;

        // Compute threshold.
        double sum = 0, sumSq = 0;
        for (int i = 0; i < n; i++)
        {
            double d = field[i].TemporalDensity;
            sum += d;
            sumSq += d * d;
        }

        double mean = sum / n;
        double std = Math.Sqrt(Math.Max(0, sumSq / n - mean * mean));
        double threshold = mean + ThresholdFactor * std;

        // Find contiguous above-threshold regions.
        bool[] above = new bool[n];
        for (int i = 0; i < n; i++)
            above[i] = field[i].TemporalDensity > threshold;

        var clusters = new List<ResonanceCluster>();

        int scanIdx = 0;
        while (scanIdx < n)
        {
            // Skip below-threshold cells.
            while (scanIdx < n && !above[scanIdx])
                scanIdx++;

            if (scanIdx >= n) break;

            // Found start of a cluster.
            int start = scanIdx;
            double totalE = 0, weightedSum = 0;

            while (scanIdx < n && above[scanIdx])
            {
                double d = field[scanIdx].TemporalDensity;
                totalE += field[scanIdx].TemporalEnergy;
                weightedSum += d * scanIdx;
                scanIdx++;
            }

            int end = scanIdx - 1;
            int size = end - start + 1;

            if (size >= MinClusterSize)
            {
                double totalDensity = 0;
                for (int i = start; i <= end; i++)
                    totalDensity += field[i].TemporalDensity;

                double centerOfMass = totalDensity > 1e-15 ? weightedSum / totalDensity : (start + end) / 2.0;
                double peakDensity = 0;
                for (int i = start; i <= end; i++)
                    peakDensity = Math.Max(peakDensity, field[i].TemporalDensity);

                // Find oscillators in this region.
                var oscInCluster = new List<int>();
                for (int o = 0; o < oscillatorPositions.Length; o++)
                {
                    int pos = oscillatorPositions[o];
                    if (pos >= start && pos <= end)
                        oscInCluster.Add(o);
                }

                clusters.Add(new ResonanceCluster(
                    -1, // id assigned during tracking
                    start, end, centerOfMass, totalE, peakDensity,
                    iteration, oscInCluster));
            }
        }

        return clusters;
    }

    /// <summary>
    /// Matches newly detected clusters to previously tracked clusters based on
    /// spatial overlap. Surviving clusters get their lifetime extended.
    /// New clusters get fresh IDs. Expired clusters are removed.
    /// </summary>
    private List<ResonanceCluster> TrackClusters(List<ResonanceCluster> current)
    {
        // Mark all active clusters as not-yet-matched.
        var unmatched = new HashSet<int>(_activeClusters.Keys);

        foreach (var newCluster in current)
        {
            int bestMatch = -1;
            double bestOverlap = 0;

            foreach (var (id, oldCluster) in _activeClusters)
            {
                double overlap = ComputeOverlap(oldCluster, newCluster);
                if (overlap > bestOverlap && overlap >= OverlapThreshold)
                {
                    bestOverlap = overlap;
                    bestMatch = id;
                }
            }

            if (bestMatch >= 0)
            {
                // Extend existing cluster.
                var existing = _activeClusters[bestMatch];
                existing.LastSeenAt = newCluster.FirstSeenAt;
                existing.StabilityScore = (double)existing.Lifetime / (existing.LastSeenAt - existing.FirstSeenAt + 1);

                // Update spatial properties (moving average).
                existing.CenterOfMass = newCluster.CenterOfMass;
                existing.SpatialLocalization = 1.0 / existing.Size;

                newCluster.Id = bestMatch;
                unmatched.Remove(bestMatch);
            }
            else
            {
                // New cluster.
                int newId = _nextClusterId++;
                newCluster.Id = newId;
                newCluster.StabilityScore = 0;
                newCluster.SpatialLocalization = 1.0 / newCluster.Size;
                _activeClusters[newId] = newCluster;
            }
        }

        // Remove expired clusters.
        foreach (int id in unmatched)
            _activeClusters.Remove(id);

        // Return all active clusters for this iteration.
        var result = new List<ResonanceCluster>();
        foreach (var cluster in current)
            result.Add(_activeClusters.ContainsKey(cluster.Id)
                ? _activeClusters[cluster.Id]
                : cluster);

        return result;
    }

    /// <summary>
    /// Computes the Jaccard-like overlap between two cluster cell ranges.
    /// overlap = |intersection| / |union|
    /// </summary>
    private static double ComputeOverlap(ResonanceCluster a, ResonanceCluster b)
    {
        int intersectStart = Math.Max(a.StartCell, b.StartCell);
        int intersectEnd = Math.Min(a.EndCell, b.EndCell);
        int intersectSize = Math.Max(0, intersectEnd - intersectStart + 1);

        int unionStart = Math.Min(a.StartCell, b.StartCell);
        int unionEnd = Math.Max(a.EndCell, b.EndCell);
        int unionSize = unionEnd - unionStart + 1;

        return unionSize > 0 ? (double)intersectSize / unionSize : 0;
    }

    /// <summary>
    /// Computes aggregate results from all tracked clusters.
    /// </summary>
    public ResonanceClusterResult ComputeResults(
        List<ResonanceCluster> currentClusters,
        int totalIterations,
        double totalFieldEnergy,
        int totalOscillators)
    {
        // Collect all clusters that have ever been tracked.
        var allClusters = _activeClusters.Values.ToList();

        int stableThreshold = totalIterations / 20; // 5% of total iterations
        var stableClusters = allClusters.Where(c => c.Lifetime >= stableThreshold).ToList();

        double meanSize = allClusters.Count > 0 ? allClusters.Average(c => c.Size) : 0;
        double meanLifetime = allClusters.Count > 0 ? allClusters.Average(c => c.Lifetime) : 0;
        int maxLifetime = allClusters.Count > 0 ? allClusters.Max(c => c.Lifetime) : 0;

        // Energy concentration: fraction of total field energy in clusters.
        double clusterEnergy = currentClusters.Sum(c => c.TotalEnergy);
        double energyConcentration = totalFieldEnergy > 1e-15 ? clusterEnergy / totalFieldEnergy : 0;

        // Oscillator participation: fraction of oscillators in any cluster.
        var oscInClusters = new HashSet<int>();
        foreach (var c in currentClusters)
            foreach (int oi in c.OscillatorIndices)
                oscInClusters.Add(oi);
        double oscParticipation = totalOscillators > 0
            ? (double)oscInClusters.Count / totalOscillators
            : 0;

        // Spatial localization of clusters (mean).
        double meanLocalization = currentClusters.Count > 0
            ? currentClusters.Average(c => c.SpatialLocalization)
            : 0;

        return new ResonanceClusterResult(
            allClusters.Count,
            stableClusters.Count,
            currentClusters.Count,
            meanSize,
            meanLifetime,
            maxLifetime,
            energyConcentration,
            oscParticipation,
            meanLocalization,
            allClusters,
            stableClusters,
            currentClusters);
    }
}
