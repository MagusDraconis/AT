using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Maps the global resonance state space topology: attractor basins,
/// transition regions, stability plateaus, and recovery corridors.
/// </summary>
public static class ResonanceLandscapeAnalyzer
{
    // ── State point ──────────────────────────────────────────────────

    public readonly record struct LandscapePoint(
        // 6D state vector (normalized per dimension in analysis)
        double R, double MeanFreq, double PhaseVar,
        double Energy, double MemScore, double LocalCoh,
        // Metadata
        string History, double Beta, double EnergyScale, int Seed,
        int Label  // basin label assigned during clustering
    );

    // ── Attractor basin ──────────────────────────────────────────────

    public sealed record AttractorBasin(
        int Id,
        int PointCount,
        double[] Centroid,      // 6D mean
        double Radius,          // max distance from centroid
        double Density,         // points / volume
        double StabilityMean,    // mean R of basin members
        List<string> DominantHistories
    );

    // ── Topology report ──────────────────────────────────────────────

    public sealed record TopologyReport(
        int TotalPoints,
        int BasinCount,
        List<AttractorBasin> Basins,
        double[,] BasinDistances,     // centroid-to-centroid distances
        double MeanInterBasinDistance,
        double MeanIntraBasinDistance,
        double SilhouetteScore,
        string TopologyClassification
    );

    // ── State generation ─────────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork nw, string h, Random rng,
        MemoryTemporalSimulation sim, int stepIters = 400)
    {
        foreach (char p in h)
        {
            double shift = p == 'A' ? 0.4 : p == 'B' ? -0.4 : (rng.NextDouble() * 2 - 1) * 0.4;
            foreach (var node in nw.Nodes) node.Phase += shift;
            sim.Run(stepIters);
        }
    }

    public static LandscapePoint GenerateState(
        string history, double beta, double energyScale,
        double k, double lambda, int n, int seed, int formationIters = 1500)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(formationIters);
        ApplyHistory(network, history, rng, sim);

        // Apply energy scaling.
        foreach (var node in network.Nodes) node.Frequency *= energyScale;
        sim.Run(500);

        // Measure.
        var m = SynchronizationMetrics.FromNetwork(network, 0);
        double memScore = ComputeMemScore(network);
        var df = new LocalDensityField(20); df.Compute(network, 1);

        return new LandscapePoint(
            m.OrderParameterR, network.Nodes.Average(nd => nd.Frequency),
            m.PhaseVariance, m.OrderParameterR * network.Nodes.Average(nd => nd.Frequency),
            memScore, df.MaxLocalR(),
            history, beta, energyScale, seed, -1);
    }

    private static double ComputeMemScore(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); sum += Math.Abs(s); sumSq += s * s; c++; }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    // ── 6D vector operations ─────────────────────────────────────────

    private static double[] Vector(LandscapePoint p) =>
        new[] { p.R, p.MeanFreq, p.PhaseVar, p.Energy, p.MemScore, p.LocalCoh };

    private static double Distance(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
        { double d = a[i] - b[i]; sum += d * d; }
        return Math.Sqrt(sum);
    }

    // ── Basin detection (agglomerative clustering) ───────────────────

    public static TopologyReport AnalyzeTopology(
        List<LandscapePoint> points, double clusterThreshold = 0.3)
    {
        int m = points.Count;
        if (m < 2) throw new ArgumentException("Need at least 2 points.");

        // ── Normalize coordinates ────────────────────────────────────
        double[] means = new double[6], stds = new double[6];
        for (int d = 0; d < 6; d++)
        {
            var vals = points.Select(p => Vector(p)[d]).ToList();
            means[d] = vals.Average();
            stds[d] = Math.Sqrt(vals.Average(v => (v - means[d]) * (v - means[d])));
            if (stds[d] < 1e-10) stds[d] = 1.0;
        }

        var vectors = points.Select(p =>
        {
            var v = Vector(p);
            for (int d = 0; d < 6; d++) v[d] = (v[d] - means[d]) / stds[d];
            return v;
        }).ToArray();

        // ── Agglomerative clustering ─────────────────────────────────
        int[] labels = new int[m];
        for (int i = 0; i < m; i++) labels[i] = i;

        // Distance threshold clustering: connect points within threshold.
        for (int i = 0; i < m; i++)
            for (int j = i + 1; j < m; j++)
                if (Distance(vectors[i], vectors[j]) < clusterThreshold)
                    Union(labels, i, j);

        // Assign sequential basin IDs.
        var rootMap = new Dictionary<int, int>();
        int nextId = 0;
        var labeled = new int[m];
        for (int i = 0; i < m; i++)
        {
            int root = Find(labels, i);
            if (!rootMap.ContainsKey(root)) rootMap[root] = nextId++;
            labeled[i] = rootMap[root];
        }

        // Update point labels.
        var result = points.Select((p, i) => p with { Label = labeled[i] }).ToList();

        // ── Basin statistics ─────────────────────────────────────────
        int basinCount = nextId;
        var basins = new List<AttractorBasin>();
        for (int b = 0; b < basinCount; b++)
        {
            var indices = Enumerable.Range(0, m).Where(i => labeled[i] == b).ToList();
            if (indices.Count == 0) continue;

            double[] centroid = new double[6];
            for (int d = 0; d < 6; d++)
                centroid[d] = indices.Average(i => vectors[i][d]);

            double maxDist = indices.Max(i => Distance(vectors[i], centroid));
            double stability = indices.Average(i => result[i].R);
            double volume = maxDist > 0 ? indices.Count / Math.Pow(maxDist, 6) : 0;
            var domHist = indices.GroupBy(i => result[i].History)
                .OrderByDescending(g => g.Count()).Take(3)
                .Select(g => g.Key).ToList();

            basins.Add(new AttractorBasin(b, indices.Count, centroid, maxDist,
                volume, stability, domHist));
        }

        // ── Inter/intra basin distances ──────────────────────────────
        var basinCentroids = basins.Select(b => b.Centroid).ToArray();
        var basinDists = new double[basinCount, basinCount];
        double interSum = 0; int interCount = 0;
        for (int i = 0; i < basinCount; i++)
            for (int j = i + 1; j < basinCount; j++)
            {
                double d = Distance(basinCentroids[i], basinCentroids[j]);
                basinDists[i, j] = basinDists[j, i] = d;
                interSum += d; interCount++;
            }
        double meanInter = interCount > 0 ? interSum / interCount : 0;

        // Intra-basin mean distance.
        double intraSum = 0; int intraCount = 0;
        for (int b = 0; b < basinCount; b++)
        {
            var indices = Enumerable.Range(0, m).Where(i => labeled[i] == b).ToList();
            for (int i = 0; i < indices.Count; i++)
                for (int j = i + 1; j < indices.Count; j++)
                { intraSum += Distance(vectors[indices[i]], vectors[indices[j]]); intraCount++; }
        }
        double meanIntra = intraCount > 0 ? intraSum / intraCount : 0;

        // Silhouette score.
        double silSum = 0;
        for (int i = 0; i < m; i++)
        {
            int bi = labeled[i];
            var same = Enumerable.Range(0, m).Where(j => labeled[j] == bi && j != i).ToList();
            double a = same.Count > 0 ? same.Average(j => Distance(vectors[i], vectors[j])) : 0;
            double bMin = double.MaxValue;
            for (int bj = 0; bj < basinCount; bj++)
            {
                if (bj == bi) continue;
                var other = Enumerable.Range(0, m).Where(j => labeled[j] == bj).ToList();
                double b = other.Average(j => Distance(vectors[i], vectors[j]));
                if (b < bMin) bMin = b;
            }
            double sil = Math.Abs(a - bMin) > 1e-10 ? (bMin - a) / Math.Max(a, bMin) : 0;
            silSum += sil;
        }
        double silhouette = silSum / m;

        // Classification.
        string classification = basinCount == 1 ? "A: Single attractor basin" :
            silhouette > 0.7 ? "D: Hierarchical basin structure" :
            silhouette > 0.4 ? "C: Multiple attractor basins" :
            "B: Multi-region single basin";

        return new TopologyReport(m, basinCount, basins, basinDists,
            meanInter, meanIntra, silhouette, classification);
    }

    private static int Find(int[] parent, int x)
    { while (parent[x] != x) { parent[x] = parent[parent[x]]; x = parent[x]; } return x; }
    private static void Union(int[] parent, int a, int b)
    { int ra = Find(parent, a), rb = Find(parent, b); if (ra != rb) parent[rb] = ra; }
}
