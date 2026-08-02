using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether the attractor basins discovered in TQM-055 are
/// genuine emergent structures or sampling artifacts. Performs
/// a high-resolution energy scan with multi-threshold clustering
/// to measure basin persistence and stability.
/// </summary>
public static class EmergentBasinSpectrumAnalyzer
{
    // ── Types ────────────────────────────────────────────────────────

    public readonly record struct SpectrumPoint(
        double R, double MeanFreq, double PhaseVar,
        double Energy, double MemScore, double LocalCoh,
        string History, double Beta, double EnergyScale, int Seed);

    public sealed record BasinPersistence(
        double Threshold,
        int BasinCount,
        int LargeBasinCount,   // basins with ≥5% of points
        double Silhouette,
        double InterIntraRatio);

    public sealed record SpectrumTopologyReport(
        int TotalPoints,
        int EnergyLevels,
        List<BasinPersistence> PersistenceByThreshold,
        // Stats at optimal threshold
        int OptimalBasinCount,
        int OptimalLargeBasinCount,
        double OptimalSilhouette,
        string Classification,
        bool DiscreteBasinsConfirmed);

    // ── State generation ─────────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork nw, string h, Random rng,
        MemoryTemporalSimulation sim)
    {
        foreach (char p in h)
        {
            double shift = p == 'A' ? 0.4 : p == 'B' ? -0.4 : (rng.NextDouble() * 2 - 1) * 0.4;
            foreach (var node in nw.Nodes) node.Phase += shift;
            sim.Run(400);
        }
    }

    public static SpectrumPoint GenerateState(
        string history, double beta, double energyScale,
        double k, double lambda, int n, int seed)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });
        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);
        ApplyHistory(network, history, rng, sim);
        foreach (var node in network.Nodes) node.Frequency *= energyScale;
        sim.Run(400);

        var m = SynchronizationMetrics.FromNetwork(network, 0);
        double mem = MemScore(network);
        var df = new LocalDensityField(20); df.Compute(network, 1);

        return new SpectrumPoint(m.OrderParameterR, network.Nodes.Average(nd => nd.Frequency),
            m.PhaseVariance, m.OrderParameterR * network.Nodes.Average(nd => nd.Frequency),
            mem, df.MaxLocalR(), history, beta, energyScale, seed);
    }

    private static double MemScore(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); sum += Math.Abs(s); sumSq += s * s; c++; }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    // ── Clustering (same as TQM-055) ─────────────────────────────────

    private static double[] Vector(SpectrumPoint p) =>
        new[] { p.R, p.MeanFreq, p.PhaseVar, p.Energy, p.MemScore, p.LocalCoh };

    private static double Dist(double[] a, double[] b)
    {
        double s = 0; for (int i = 0; i < a.Length; i++) { double d = a[i] - b[i]; s += d * d; }
        return Math.Sqrt(s);
    }

    private static (int[] Labels, int BasinCount) Cluster(List<SpectrumPoint> pts, double threshold)
    {
        int m = pts.Count;
        // Normalize.
        double[] means = new double[6], stds = new double[6];
        for (int d = 0; d < 6; d++)
        {
            var vals = pts.Select(p => Vector(p)[d]).ToList();
            means[d] = vals.Average();
            stds[d] = Math.Sqrt(vals.Average(v => (v - means[d]) * (v - means[d])));
            if (stds[d] < 1e-10) stds[d] = 1.0;
        }
        var vecs = pts.Select(p => { var v = Vector(p); for (int d = 0; d < 6; d++) v[d] = (v[d] - means[d]) / stds[d]; return v; }).ToArray();

        int[] labels = new int[m];
        for (int i = 0; i < m; i++) labels[i] = i;
        for (int i = 0; i < m; i++)
            for (int j = i + 1; j < m; j++)
                if (Dist(vecs[i], vecs[j]) < threshold)
                    Union(labels, i, j);

        var rootMap = new Dictionary<int, int>();
        int nextId = 0;
        int[] result = new int[m];
        for (int i = 0; i < m; i++)
        {
            int root = Find(labels, i);
            if (!rootMap.ContainsKey(root)) rootMap[root] = nextId++;
            result[i] = rootMap[root];
        }
        return (result, nextId);
    }

    private static double Silhouette(List<SpectrumPoint> pts, int[] labels, int bc, double threshold)
    {
        int m = pts.Count;
        // Normalize vectors same as in Cluster.
        double[] means = new double[6], stds = new double[6];
        for (int d = 0; d < 6; d++)
        {
            var vals = pts.Select(p => Vector(p)[d]).ToList();
            means[d] = vals.Average();
            stds[d] = Math.Sqrt(vals.Average(v => (v - means[d]) * (v - means[d])));
            if (stds[d] < 1e-10) stds[d] = 1.0;
        }
        var vecs = pts.Select(p => { var v = Vector(p); for (int d = 0; d < 6; d++) v[d] = (v[d] - means[d]) / stds[d]; return v; }).ToArray();

        double sum = 0;
        for (int i = 0; i < m; i++)
        {
            int bi = labels[i];
            var same = Enumerable.Range(0, m).Where(j => labels[j] == bi && j != i).ToList();
            double a = same.Count > 0 ? same.Average(j => Dist(vecs[i], vecs[j])) : 0;
            double bMin = double.MaxValue;
            for (int bj = 0; bj < bc; bj++)
            {
                if (bj == bi) continue;
                var other = Enumerable.Range(0, m).Where(j => labels[j] == bj).ToList();
                if (other.Count == 0) continue;
                double b = other.Average(j => Dist(vecs[i], vecs[j]));
                if (b < bMin) bMin = b;
            }
            double sil = bMin < double.MaxValue && Math.Abs(a - bMin) > 1e-10 ? (bMin - a) / Math.Max(a, bMin) : 0;
            sum += sil;
        }
        return sum / m;
    }

    // ── Multi-threshold analysis ─────────────────────────────────────

    public static SpectrumTopologyReport AnalyzeSpectrum(
        List<SpectrumPoint> points,
        double[] thresholds)
    {
        var persistence = new List<BasinPersistence>();

        foreach (var thresh in thresholds)
        {
            var (labels, bc) = Cluster(points, thresh);
            int largeCount = 0;
            var basinSizes = new int[bc];
            for (int i = 0; i < points.Count; i++) basinSizes[labels[i]]++;
            largeCount = basinSizes.Count(s => s >= points.Count * 0.05);
            double sil = Silhouette(points, labels, bc, thresh);

            // Inter/intra ratio approximation.
            double interSum = 0, intraSum = 0;
            int interC = 0, intraC = 0;
            // Use centroids.
            var centroids = new double[bc][];
            var vecs2 = points.Select(p => Vector(p)).ToArray();
            double[] m2 = new double[6], s2 = new double[6];
            for (int d = 0; d < 6; d++)
            { var v = points.Select(p => Vector(p)[d]).ToList(); m2[d] = v.Average(); s2[d] = Math.Sqrt(v.Average(x => (x - m2[d]) * (x - m2[d]))); if (s2[d] < 1e-10) s2[d] = 1.0; }
            for (int b = 0; b < bc; b++)
            {
                centroids[b] = new double[6];
                var idx = Enumerable.Range(0, points.Count).Where(i => labels[i] == b).ToList();
                if (idx.Count == 0) continue;
                for (int d = 0; d < 6; d++)
                    centroids[b][d] = idx.Average(i => (vecs2[i][d] - m2[d]) / s2[d]);
            }
            for (int i = 0; i < bc; i++)
            {
                if (basinSizes[i] < 2) continue;
                for (int j = i + 1; j < bc; j++)
                {
                    if (basinSizes[j] < 2) continue;
                    interSum += Dist(centroids[i], centroids[j]); interC++;
                }
            }
            double ratio = intraC > 0 && interC > 0 ? (interSum / interC) / (intraSum / Math.Max(intraC, 1)) : 0;

            persistence.Add(new BasinPersistence(thresh, bc, largeCount, sil, ratio));
        }

        // Optimal threshold: maximize silhouette.
        var best = persistence.OrderByDescending(p => p.Silhouette).First();
        bool discreteConfirmed = best.LargeBasinCount > 1 && best.Silhouette > 0.30;

        string classification = best.LargeBasinCount > 3 && best.Silhouette > 0.5
            ? "D: Hierarchical basin spectrum"
            : best.LargeBasinCount > 1 && best.Silhouette > 0.3
            ? "C: Stable emergent basins"
            : best.LargeBasinCount > 1 ? "B: Weak basin structure"
            : "A: Continuous landscape";

        return new SpectrumTopologyReport(points.Count,
            points.Select(p => p.EnergyScale).Distinct().Count(),
            persistence, best.BasinCount, best.LargeBasinCount,
            best.Silhouette, classification, discreteConfirmed);
    }

    private static int Find(int[] p, int x) { while (p[x] != x) { p[x] = p[p[x]]; x = p[x]; } return x; }
    private static void Union(int[] p, int a, int b) { int ra = Find(p, a), rb = Find(p, b); if (ra != rb) p[rb] = ra; }
}
