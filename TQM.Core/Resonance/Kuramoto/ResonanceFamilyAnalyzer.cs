using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Classifies synchronization clusters into resonance families based on
/// statistical signature similarity in feature space.
///
/// Features: synchronization, effective frequency, energy, lifetime, cluster size.
/// </summary>
public sealed class ResonanceFamilyAnalyzer
{
    /// <summary>
    /// Maximum Euclidean distance in normalized feature space for two clusters
    /// to be considered members of the same family.
    /// </summary>
    public double FeatureDistanceThreshold { get; set; } = 0.4;

    /// <summary>
    /// Minimum number of clusters required to form a distinct family.
    /// </summary>
    public int MinFamilySize { get; set; } = 2;

    /// <summary>
    /// Classifies a collection of synchronization clusters into resonance families
    /// using agglomerative clustering in normalized feature space.
    /// </summary>
    public List<ResonanceFamily> ClassifyFamilies(
        List<SynchronizationCluster> allClusters)
    {
        if (allClusters.Count == 0)
            return new List<ResonanceFamily>();

        // Extract feature vectors.
        var features = new List<double[]>();
        foreach (var cluster in allClusters)
            features.Add(ExtractFeatures(cluster));

        // Normalize features to [0, 1].
        NormalizeFeatures(features);

        // Agglomerative clustering: merge clusters within threshold distance.
        int m = features.Count;
        var parent = new int[m];
        for (int i = 0; i < m; i++)
            parent[i] = i;

        // Compute all pairwise distances and merge below threshold.
        for (int i = 0; i < m; i++)
        {
            for (int j = i + 1; j < m; j++)
            {
                double dist = EuclideanDistance(features[i], features[j]);
                if (dist < FeatureDistanceThreshold)
                    Union(parent, i, j);
            }
        }

        // Group by root.
        var groups = new Dictionary<int, List<int>>();
        for (int i = 0; i < m; i++)
        {
            int root = Find(parent, i);
            if (!groups.ContainsKey(root))
                groups[root] = new List<int>();
            groups[root].Add(i);
        }

        // Build families from groups that meet minimum size.
        var families = new List<ResonanceFamily>();
        int familyId = 0;

        foreach (var (_, indices) in groups.OrderByDescending(g => g.Value.Count))
        {
            if (indices.Count < MinFamilySize)
                continue;

            var members = indices.Select(i => allClusters[i]).ToList();

            double meanSync = members.Average(c => c.Synchronization);
            double meanFreq = members.Average(c => c.MeanFrequency);
            double meanEnergy = members.Average(c => c.MeanEnergy);
            double meanLifetime = members.Average(c => c.Lifetime);
            double meanSize = members.Average(c => c.Size);

            // Coherence: 1 - (mean intra-family distance / max possible distance).
            double intraDist = 0;
            int pairs = 0;
            foreach (int a in indices)
            {
                foreach (int b in indices)
                {
                    if (a >= b) continue;
                    intraDist += EuclideanDistance(features[a], features[b]);
                    pairs++;
                }
            }

            double avgIntraDist = pairs > 0 ? intraDist / pairs : 0;
            double coherenceScore = 1.0 - Math.Min(1.0, avgIntraDist / FeatureDistanceThreshold);

            families.Add(new ResonanceFamily(
                familyId++,
                meanSync,
                meanFreq,
                meanEnergy,
                meanLifetime,
                meanSize,
                coherenceScore,
                members));
        }

        return families;
    }

    /// <summary>
    /// Extracts a feature vector from a synchronization cluster.
    /// Features: [sync, mean_freq, mean_energy, lifetime, size].
    /// </summary>
    private static double[] ExtractFeatures(SynchronizationCluster cluster)
    {
        return new[]
        {
            cluster.Synchronization,
            cluster.MeanFrequency,
            cluster.MeanEnergy,
            (double)cluster.Lifetime,
            (double)cluster.Size
        };
    }

    /// <summary>
    /// Normalizes feature vectors column-wise to [0, 1] using min-max scaling.
    /// </summary>
    private static void NormalizeFeatures(List<double[]> features)
    {
        if (features.Count == 0) return;

        int dims = features[0].Length;
        var mins = new double[dims];
        var maxs = new double[dims];

        for (int d = 0; d < dims; d++)
        {
            mins[d] = double.MaxValue;
            maxs[d] = double.MinValue;
        }

        foreach (var f in features)
        {
            for (int d = 0; d < dims; d++)
            {
                if (f[d] < mins[d]) mins[d] = f[d];
                if (f[d] > maxs[d]) maxs[d] = f[d];
            }
        }

        foreach (var f in features)
        {
            for (int d = 0; d < dims; d++)
            {
                double range = maxs[d] - mins[d];
                if (range > 1e-10)
                    f[d] = (f[d] - mins[d]) / range;
                else
                    f[d] = 0.5;
            }
        }
    }

    private static double EuclideanDistance(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += (a[i] - b[i]) * (a[i] - b[i]);
        return Math.Sqrt(sum);
    }

    // Union-Find for agglomerative clustering.
    private static int Find(int[] parent, int x)
    {
        while (parent[x] != x)
        {
            parent[x] = parent[parent[x]];
            x = parent[x];
        }
        return x;
    }

    private static void Union(int[] parent, int a, int b)
    {
        int ra = Find(parent, a);
        int rb = Find(parent, b);
        if (ra != rb)
            parent[rb] = ra;
    }
}
