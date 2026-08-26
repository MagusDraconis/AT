using System.Collections.Concurrent;
using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether radial symmetry becomes causal near the critical condensation threshold.
/// </summary>
public static class CriticalSymmetryAnalyzer
{
    public sealed record CriticalPoint(
        double K, int NeighborCount, double Symmetry,
        bool Formed, int BirthIter, double FinalR, int FinalSize, int Lifetime);

    public static List<CriticalPoint> Sweep(
        double k, int neighborCount, double symmetry, int totalN,
        double lambda, int seeds, int baseSeed, int iterations = 2000)
    {
        var results = new ConcurrentBag<CriticalPoint>();

        Parallel.For(0, seeds, run =>
        {
            var rng = new Random(baseSeed + run * 7919);
            var network = new TemporalNetwork(totalN);
            double cx = 0.5, cy = 0.5;
            double clusterRadius = lambda * 0.8;

            // Controlled cluster with specified symmetry and neighbor count.
            for (int i = 0; i < neighborCount; i++)
            {
                double angle = rng.NextDouble() * symmetry * 2.0 * Math.PI;
                double radius = rng.NextDouble() * clusterRadius;
                var node = new TemporalNode(i, phase: rng.NextDouble() * 2.0 * Math.PI, frequency: 1.0)
                {
                    X = Math.Clamp(cx + radius * Math.Cos(angle), 0, 1),
                    Y = Math.Clamp(cy + radius * Math.Sin(angle), 0, 1)
                };
                network.AddNode(node);
            }

            // Background.
            for (int i = neighborCount; i < totalN; i++)
            {
                var node = new TemporalNode(i, phase: rng.NextDouble() * 2.0 * Math.PI,
                    frequency: 0.5 + rng.NextDouble() * 1.5)
                { X = rng.NextDouble(), Y = rng.NextDouble() };
                network.AddNode(node);
            }

            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
            var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = totalN };
            var densityField = new LocalDensityField(20);

            bool formed = false; int birth = -1; int lifetime = 0;

            for (int iter = 0; iter < iterations; iter++)
            {
                sim.Step();
                if ((iter + 1) % 200 == 0 || iter == iterations - 1)
                {
                    densityField.Compute(network, neighborhoodCells: 1);
                    double maxR = densityField.MaxLocalR();
                    if (maxR >= 0.80 && !formed) { formed = true; birth = iter + 1; }
                    if (formed) lifetime = iter + 1 - birth;
                    if (iter == iterations - 1)
                    {
                        results.Add(new CriticalPoint(k, neighborCount, symmetry, formed, birth,
                            maxR, densityField.CellsAboveThreshold(0.80), lifetime));
                    }
                }
            }
        });

        return results.ToList();
    }
}
