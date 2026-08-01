using System.Collections.Concurrent;
using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Validates the universality of the effective connectivity threshold Nc_eff ≈ 42
/// across broad parameter sweeps with multiple seeds per configuration.
/// </summary>
public static class EffectiveConnectivityUniversalityAnalyzer
{
    public sealed record UniversalityPoint(
        int N, double K, double Lambda, string Placement, int Seed,
        double NeighborCount);

    /// <summary>
    /// Runs a simulation and returns the mean neighbor count at condensate birth.
    /// </summary>
    public static UniversalityPoint? Measure(int n, double k, double lambda, string placement, int seed, int iterations = 2000)
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

        var neighborCounts = new List<double>();

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if (iter == iterations / 2 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    // Count neighbors for a representative oscillator in each condensate.
                    int bestOsc = c.Cells.Count > 0 ? (int)(c.Cells[0].Item1 * n / 400.0) : 0;
                    bestOsc = Math.Clamp(bestOsc, 0, n - 1);

                    int nc = 0;
                    var nodes = network.Nodes;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == bestOsc) continue;
                        double dx = nodes[bestOsc].X - nodes[j].X;
                        double dy = nodes[bestOsc].Y - nodes[j].Y;
                        if (Math.Sqrt(dx * dx + dy * dy) <= lambda)
                            nc++;
                    }
                    neighborCounts.Add(nc);
                }
            }
        }

        if (neighborCounts.Count == 0) return null;
        return new UniversalityPoint(n, k, lambda, placement, seed, neighborCounts.Average());
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
