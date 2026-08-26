using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether multiple condensates form stable higher-order assemblies.
/// </summary>
public static class CondensateAssemblyAnalyzer
{
    public sealed record AssemblyResult(
        string Layout, int CondensateCount, double FinalGlobalR,
        int FinalDomains, double MeanDomainSize, int Mergers,
        string Classification, int Lifetime);

    /// <summary>
    /// Runs one assembly experiment: Nc condensates arranged in a specific layout.
    /// </summary>
    public static AssemblyResult Analyze(
        int condensateCount, string layout, double separation,
        int clusterOscillators, double k, double lambda, Random rng, int iterations = 4000)
    {
        int totalN = condensateCount * clusterOscillators;
        var network = new TemporalNetwork(totalN);

        // Generate layout positions.
        var positions = GenerateLayout(condensateCount, layout, separation);

        for (int c = 0; c < condensateCount; c++)
        {
            double cx = positions[c].X, cy = positions[c].Y;
            for (int i = 0; i < clusterOscillators; i++)
            {
                double angle = rng.NextDouble() * 2.0 * Math.PI;
                double radius = rng.NextDouble() * lambda * 0.8;
                var node = new TemporalNode(c * clusterOscillators + i,
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

        int domains = condensateCount;
        int mergers = 0;

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if ((iter + 1) % 500 == 0 || iter == iterations - 1)
            {
                df.Compute(network, neighborhoodCells: 1);
                int currentDomains = df.CellsAboveThreshold(0.80);
                if (currentDomains < domains) { mergers += domains - currentDomains; }
                domains = currentDomains;
            }
        }

        double finalR = SynchronizationMetrics.FromNetwork(network, iterations).OrderParameterR;
        df.Compute(network, neighborhoodCells: 1);
        int finalDomains = df.CellsAboveThreshold(0.80);
        double meanDomain = finalDomains > 0 ? (double)totalN / finalDomains : totalN;

        string classification = finalDomains >= condensateCount ? "Stable Assembly" :
                                finalDomains > 1 ? "Partial Assembly" :
                                finalDomains == 1 ? "Merged" : "Disordered";

        return new AssemblyResult(layout, condensateCount, finalR, finalDomains,
            meanDomain, mergers, classification, iterations);
    }

    private static (double X, double Y)[] GenerateLayout(int count, string layout, double sep)
    {
        var positions = new (double, double)[count];
        double cx = 0.5, cy = 0.5;

        for (int i = 0; i < count; i++)
        {
            switch (layout)
            {
                case "Linear":
                    positions[i] = (cx + (i - (count - 1) / 2.0) * sep, cy); break;
                case "Ring":
                    double angle = 2.0 * Math.PI * i / count;
                    positions[i] = (cx + sep * Math.Cos(angle), cy + sep * Math.Sin(angle)); break;
                case "Square":
                    int side = (int)Math.Ceiling(Math.Sqrt(count));
                    positions[i] = (cx + (i % side - (side - 1) / 2.0) * sep,
                                    cy + (i / side - (side - 1) / 2.0) * sep); break;
                case "Random":
                    positions[i] = (cx + (NextGaussian(rng) * sep),
                                    cy + (NextGaussian(rng) * sep)); break;
            }
        }

        return positions;
    }

    private static Random rng = new Random();
    private static double NextGaussian(Random r) =>
        Math.Sqrt(-2.0 * Math.Log(Math.Max(1.0 - r.NextDouble(), 1e-15))) *
        Math.Cos(2.0 * Math.PI * (1.0 - r.NextDouble()));
}
