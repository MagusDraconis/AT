using System.Collections.Concurrent;
using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Searches for multiple stable attractor basins in the TQM resonance landscape.
/// </summary>
public static class AttractorAnalyzer
{
    public sealed record AttractorPoint(
        double LocalR, double Density, double NeighborCount,
        double RadialSymmetry, double ClusterSize, double FreqStd,
        int N, double K);

    /// <summary>
    /// Runs one simulation and returns attractor characteristics at the final state.
    /// </summary>
    public static AttractorPoint? RunOne(int n, double k, double lambda, Random rng, int iterations = 3000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var df = new LocalDensityField(20);

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if (iter == iterations - 1)
            {
                df.Compute(network, neighborhoodCells: 1);
                double localR = df.MaxLocalR();
                double density = df.MeanLocalR();
                int clusterSize = df.CellsAboveThreshold(0.80);

                var nodes = network.Nodes;
                double sumFreq = 0, sumFreqSq = 0;
                int bo = n / 4, nc = 0; double radSym = 0;
                for (int j = 0; j < n; j++)
                {
                    sumFreq += nodes[j].Frequency; sumFreqSq += nodes[j].Frequency * nodes[j].Frequency;
                    if (j == bo) continue;
                    double dx = nodes[bo].X - nodes[j].X, dy = nodes[bo].Y - nodes[j].Y;
                    if (Math.Sqrt(dx * dx + dy * dy) <= lambda) nc++;
                }
                double freqStd = Math.Sqrt(Math.Max(0, sumFreqSq / n - (sumFreq / n) * (sumFreq / n)));

                // Simplified radial symmetry proxy.
                radSym = density > 0.05 ? 0.7 : 0.3;

                if (localR > 0.5)
                    return new AttractorPoint(localR, density, nc, radSym, clusterSize, freqStd, n, k);
            }
        }
        return null;
    }

    /// <summary>
    /// Simple agglomerative clustering of attractor points.
    /// </summary>
    public static List<List<AttractorPoint>> Cluster(List<AttractorPoint> points, double threshold = 0.3)
    {
        int m = points.Count;
        var parent = new int[m];
        for (int i = 0; i < m; i++) parent[i] = i;

        for (int i = 0; i < m; i++)
        {
            for (int j = i + 1; j < m; j++)
            {
                double dist = FeatureDistance(points[i], points[j]);
                if (dist < threshold) Union(parent, i, j);
            }
        }

        var groups = new Dictionary<int, List<AttractorPoint>>();
        for (int i = 0; i < m; i++)
        {
            int root = Find(parent, i);
            if (!groups.ContainsKey(root)) groups[root] = new List<AttractorPoint>();
            groups[root].Add(points[i]);
        }

        return groups.Values.Where(g => g.Count >= 2).ToList();
    }

    private static double FeatureDistance(AttractorPoint a, AttractorPoint b)
    {
        double dr = a.LocalR - b.LocalR, dd = (a.Density - b.Density) * 5,
               dn = (a.NeighborCount - b.NeighborCount) / 100.0;
        return Math.Sqrt(dr * dr + dd * dd + dn * dn);
    }

    private static int Find(int[] p, int x) { while (p[x] != x) { p[x] = p[p[x]]; x = p[x]; } return x; }
    private static void Union(int[] p, int a, int b) { int ra = Find(p, a), rb = Find(p, b); if (ra != rb) p[rb] = ra; }
}
