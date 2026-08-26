using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tracks whether hierarchical condensate assemblies emerge spontaneously
/// from randomly placed independent condensates.
/// </summary>
public static class SpontaneousAssemblyAnalyzer
{
    public sealed record SpontaneousResult(
        int InitCondensates, int FinalDomains, int Mergers,
        double MeanDomainSize, double GlobalR, int AssemblyCount);

    /// <summary>
    /// Places Nc small condensates randomly and tracks their evolution.
    /// </summary>
    public static SpontaneousResult Analyze(
        int condensateCount, int oscPerCondensate,
        double k, double lambda, Random rng, int iterations = 4000)
    {
        int totalN = condensateCount * oscPerCondensate;
        var network = new TemporalNetwork(totalN);

        for (int c = 0; c < condensateCount; c++)
        {
            double cx = rng.NextDouble(), cy = rng.NextDouble();
            for (int i = 0; i < oscPerCondensate; i++)
            {
                double angle = rng.NextDouble() * 2.0 * Math.PI;
                double radius = rng.NextDouble() * lambda * 0.5;
                var node = new TemporalNode(c * oscPerCondensate + i,
                    phase: rng.NextDouble() * 2.0 * Math.PI, frequency: 1.0)
                {
                    X = Math.Clamp(cx + radius * Math.Cos(angle), 0, 1),
                    Y = Math.Clamp(cy + radius * Math.Sin(angle), 0, 1)
                };
                network.AddNode(node);
            }
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = totalN };
        var df = new LocalDensityField(20);

        int domains = condensateCount, mergers = 0;

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if ((iter + 1) % 500 == 0 || iter == iterations - 1)
            {
                df.Compute(network, neighborhoodCells: 1);
                int current = df.CellsAboveThreshold(0.80);
                if (current < domains) mergers += domains - current;
                domains = current;
            }
        }

        double globalR = SynchronizationMetrics.FromNetwork(network, iterations).OrderParameterR;
        df.Compute(network, neighborhoodCells: 1);
        int finalDomains = df.CellsAboveThreshold(0.80);
        double meanDomain = finalDomains > 0 ? (double)totalN / finalDomains : totalN;

        // Assembly count: estimate from condensate positions.
        // Condensates within 2λ of each other form an assembly.
        var nodes = network.Nodes;
        var visited = new bool[condensateCount];
        int assemblies = 0;

        for (int c = 0; c < condensateCount; c++)
        {
            if (visited[c]) continue;
            assemblies++;
            var queue = new Queue<int>(); queue.Enqueue(c); visited[c] = true;
            double cx = nodes[c * oscPerCondensate].X, cy = nodes[c * oscPerCondensate].Y;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                double vx = nodes[v * oscPerCondensate].X, vy = nodes[v * oscPerCondensate].Y;
                for (int w = 0; w < condensateCount; w++)
                {
                    if (visited[w]) continue;
                    double wx = nodes[w * oscPerCondensate].X, wy = nodes[w * oscPerCondensate].Y;
                    double dx = vx - wx, dy = vy - wy;
                    if (Math.Sqrt(dx * dx + dy * dy) <= lambda * 3)
                    { visited[w] = true; queue.Enqueue(w); }
                }
            }
        }

        return new SpontaneousResult(condensateCount, finalDomains, mergers,
            meanDomain, globalR, assemblies);
    }
}
