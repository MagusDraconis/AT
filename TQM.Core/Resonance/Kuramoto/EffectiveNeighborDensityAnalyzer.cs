using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Analyzes multiple candidate order parameters for resonance condensation
/// and ranks them by universality (cross-parameter CV).
/// </summary>
public static class EffectiveNeighborDensityAnalyzer
{
    public sealed record MultiMetricPoint(
        int N, double K, double Lambda, string Placement,
        double NeighborCount, double LocalDensity,
        double EffectiveNeighborDensity, double ClusteringCoeff);

    public static MultiMetricPoint? Measure(int n, double k, double lambda, string placement, int seed, int iterations = 2000)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq);
            PlaceNode(node, placement, rng, i, n);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);
        var condAnalyzer = new ResonanceCondensationAnalyzer
            { CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3 };

        var metrics = new List<(double Neighbors, double Density, double EffDensity, double Clustering)>();

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if (iter == iterations / 2 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    int bestOsc = Math.Clamp((c.Cells.Count > 0 ? c.Cells[0].Item1 * n / 400 : 0), 0, n - 1);
                    var nodes = network.Nodes;

                    int nc = 0;
                    var neighborList = new List<int>();
                    for (int j = 0; j < n; j++)
                    {
                        if (j == bestOsc) continue;
                        double dx = nodes[bestOsc].X - nodes[j].X;
                        double dy = nodes[bestOsc].Y - nodes[j].Y;
                        if (Math.Sqrt(dx * dx + dy * dy) <= lambda)
                        { nc++; neighborList.Add(j); }
                    }

                    // Clustering.
                    int tri = 0, pairs = 0;
                    for (int a = 0; a < neighborList.Count; a++)
                        for (int b = a + 1; b < neighborList.Count; b++)
                        {
                            pairs++;
                            double dx = nodes[neighborList[a]].X - nodes[neighborList[b]].X;
                            double dy = nodes[neighborList[a]].Y - nodes[neighborList[b]].Y;
                            if (Math.Sqrt(dx * dx + dy * dy) <= lambda) tri++;
                        }
                    double clustering = pairs > 0 ? (double)tri / pairs : 0;

                    int gx = (int)(nodes[bestOsc].X * densityField.GridSize);
                    int gy = (int)(nodes[bestOsc].Y * densityField.GridSize);
                    double density = densityField.GetLocalDensity(
                        Math.Clamp(gx, 0, densityField.GridSize - 1),
                        Math.Clamp(gy, 0, densityField.GridSize - 1));

                    double effDensity = lambda > 1e-10 ? nc / (lambda * lambda) : 0;

                    metrics.Add((nc, density, effDensity, clustering));
                }
            }
        }

        if (metrics.Count == 0) return null;
        return new MultiMetricPoint(n, k, lambda, placement,
            metrics.Average(m => m.Neighbors),
            metrics.Average(m => m.Density),
            metrics.Average(m => m.EffDensity),
            metrics.Average(m => m.Clustering));
    }

    private static void PlaceNode(TemporalNode node, string placement, Random rng, int idx, int total)
    {
        switch (placement)
        {
            case "Uniform":
                node.X = rng.NextDouble(); node.Y = rng.NextDouble(); break;
            case "MultipleClusters":
                var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
                var (cx, cy) = cc[idx % 5];
                node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
                node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1); break;
        }
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
