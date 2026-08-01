using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Measures effective connectivity metrics at condensate birth locations
/// to determine whether connectivity is a more universal predictor than raw density.
/// </summary>
public static class EffectiveConnectivityAnalyzer
{
    public sealed record ConnectivityMeasurement(
        int N, double K, double Lambda, string Placement,
        double MeanNeighborCount, double MeanWeightedNeighbors,
        double MeanClusteringCoeff, double MeanDensity,
        int CondensateCount);

    /// <summary>
    /// Runs a simulation and records connectivity metrics at condensate birth events.
    /// </summary>
    public static ConnectivityMeasurement Measure(
        int n, double k, double lambda, string placement, Random rng, int iterations = 3000)
    {
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

        var birthConn = new List<(double Neighbors, double Weighted, double Clustering, double Density)>();

        var seenIds = new HashSet<int>();
        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if ((iter + 1) % 500 == 0 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    if (!seenIds.Contains(c.Id) && c.BirthIteration == iter + 1)
                    {
                        seenIds.Add(c.Id);

                        // Find oscillator nearest to condensate center.
                        double ccx = 0, ccy = 0;
                        foreach (var (cellGx, cellGy) in c.Cells)
                        { ccx += cellGx; ccy += cellGy; }
                        ccx = ccx / c.Cells.Count / densityField.GridSize;
                        ccy = ccy / c.Cells.Count / densityField.GridSize;

                        int bestOsc = 0;
                        double bestDist = double.MaxValue;
                        var nodes = network.Nodes;
                        for (int o = 0; o < n; o++)
                        {
                            double dx = nodes[o].X - ccx, dy = nodes[o].Y - ccy;
                            double d = dx * dx + dy * dy;
                            if (d < bestDist) { bestDist = d; bestOsc = o; }
                        }

                        // Compute connectivity metrics for this oscillator.
                        int neighborCount = 0;
                        double weightedSum = 0;
                        var neighbors = new List<int>();
                        for (int j = 0; j < n; j++)
                        {
                            if (j == bestOsc) continue;
                            double dx = nodes[bestOsc].X - nodes[j].X;
                            double dy = nodes[bestOsc].Y - nodes[j].Y;
                            double d = Math.Sqrt(dx * dx + dy * dy);
                            if (d <= lambda)
                            {
                                neighborCount++;
                                weightedSum += k * Math.Exp(-d / lambda);
                                neighbors.Add(j);
                            }
                        }

                        // Clustering coefficient: fraction of neighbor pairs that are neighbors.
                        int triCount = 0, pairCount = 0;
                        for (int a = 0; a < neighbors.Count; a++)
                            for (int b = a + 1; b < neighbors.Count; b++)
                            {
                                pairCount++;
                                double dx = nodes[neighbors[a]].X - nodes[neighbors[b]].X;
                                double dy = nodes[neighbors[a]].Y - nodes[neighbors[b]].Y;
                                if (Math.Sqrt(dx * dx + dy * dy) <= lambda)
                                    triCount++;
                            }
                        double clustering = pairCount > 0 ? (double)triCount / pairCount : 0;

                        // Local density in a cell.
                        int ossGx = (int)(nodes[bestOsc].X * densityField.GridSize);
                        int ossGy = (int)(nodes[bestOsc].Y * densityField.GridSize);
                        double density = densityField.GetLocalDensity(
                            Math.Clamp(ossGx, 0, densityField.GridSize - 1),
                            Math.Clamp(ossGy, 0, densityField.GridSize - 1));

                        birthConn.Add((neighborCount, weightedSum, clustering, density));
                    }
                }
            }
        }

        double meanNC = birthConn.Count > 0 ? birthConn.Average(b => b.Neighbors) : 0;
        double meanW = birthConn.Count > 0 ? birthConn.Average(b => b.Weighted) : 0;
        double meanCC = birthConn.Count > 0 ? birthConn.Average(b => b.Clustering) : 0;
        double meanDens = birthConn.Count > 0 ? birthConn.Average(b => b.Density) : 0;

        return new ConnectivityMeasurement(n, k, lambda, placement,
            meanNC, meanW, meanCC, meanDens, birthConn.Count);
    }

    private static void PlaceNode(TemporalNode node, string placement, Random rng, int idx, int total)
    {
        switch (placement)
        {
            case "Uniform":
                node.X = rng.NextDouble(); node.Y = rng.NextDouble(); break;
            case "GaussianBlobs":
                var bc = new[] { (0.25, 0.25), (0.75, 0.25), (0.5, 0.75) };
                var (bx, by) = bc[idx % 3];
                node.X = Math.Clamp(bx + NextGaussian(rng) * 0.08, 0, 1);
                node.Y = Math.Clamp(by + NextGaussian(rng) * 0.08, 0, 1); break;
            case "MultipleClusters":
                var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
                var (cx, cy) = cc[idx % 5];
                node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
                node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1); break;
            case "Hierarchical":
                double x = 0, y = 0, size = 1.0; int rem = idx;
                for (int l = 0; l < 5 && size > 0.01; l++)
                { int q = rem % 4; rem /= 4; if (q == 1 || q == 3) x += size / 2; if (q == 2 || q == 3) y += size / 2; size /= 2; }
                node.X = Math.Clamp(x + rng.NextDouble() * size, 0, 1);
                node.Y = Math.Clamp(y + rng.NextDouble() * size, 0, 1); break;
        }
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
