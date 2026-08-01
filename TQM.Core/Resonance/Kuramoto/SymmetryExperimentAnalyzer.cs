using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Creates oscillator neighborhoods with controlled radial symmetry
/// to test whether symmetry is causal for condensation.
/// </summary>
public static class SymmetryControlledPlacementFactory
{
    /// <summary>
    /// Places oscillators around a center with controlled radial symmetry.
    /// Symmetry = 1.0 means uniform angular distribution (perfect symmetry).
    /// Symmetry < 1.0 means oscillators are confined to a wedge of angle symmetry × 2π.
    /// </summary>
    public static TemporalNetwork CreateNetwork(
        int totalN, double symmetry, double lambda, double k, Random rng)
    {
        int clusterN = 50; // fixed cluster size
        int backgroundN = totalN - clusterN;
        var network = new TemporalNetwork(totalN);

        double cx = 0.5, cy = 0.5;
        double clusterRadius = lambda * 0.8;

        // Place cluster oscillators within a controlled angular wedge.
        for (int i = 0; i < clusterN; i++)
        {
            // Angle confined to [0, symmetry × 2π].
            double angle = rng.NextDouble() * symmetry * 2.0 * Math.PI;
            double radius = rng.NextDouble() * clusterRadius;
            var node = new TemporalNode(i, phase: rng.NextDouble() * 2.0 * Math.PI, frequency: 1.0)
            {
                X = Math.Clamp(cx + radius * Math.Cos(angle), 0, 1),
                Y = Math.Clamp(cy + radius * Math.Sin(angle), 0, 1)
            };
            network.AddNode(node);
        }

        // Background: uniform random.
        for (int i = clusterN; i < totalN; i++)
        {
            var node = new TemporalNode(i, phase: rng.NextDouble() * 2.0 * Math.PI,
                frequency: 0.5 + rng.NextDouble() * 1.5)
            {
                X = rng.NextDouble(),
                Y = rng.NextDouble()
            };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        return network;
    }
}

/// <summary>
/// Result of a single symmetry-controlled condensation experiment.
/// </summary>
public sealed class SymmetryExperimentResult
{
    public double Symmetry { get; }
    public bool CondensateFormed { get; }
    public int BirthIteration { get; }
    public double FinalLocalR { get; }
    public int FinalCondensateSize { get; }
    public int Lifetime { get; }

    public SymmetryExperimentResult(double symmetry, bool formed, int birth, double r, int size, int lifetime)
    {
        Symmetry = symmetry; CondensateFormed = formed; BirthIteration = birth;
        FinalLocalR = r; FinalCondensateSize = size; Lifetime = lifetime;
    }
}

/// <summary>
/// Runs symmetry-controlled condensation experiments.
/// </summary>
public static class SymmetryExperimentAnalyzer
{
    public static SymmetryExperimentResult Run(
        double symmetry, int n, double lambda, double k, Random rng, int iterations = 3000)
    {
        var network = SymmetryControlledPlacementFactory.CreateNetwork(n, symmetry, lambda, k, rng);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);

        bool formed = false;
        int birthIter = -1;
        double finalR = 0;
        int finalSize = 0;
        int lifetime = 0;

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if ((iter + 1) % 200 == 0 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                double maxR = densityField.MaxLocalR();
                int size = densityField.CellsAboveThreshold(0.80);
                finalR = maxR;
                finalSize = size;

                if (maxR >= 0.80)
                {
                    if (!formed) { formed = true; birthIter = iter + 1; }
                    lifetime = iter + 1 - birthIter;
                }
            }
        }

        return new SymmetryExperimentResult(symmetry, formed, birthIter, finalR, finalSize, lifetime);
    }
}
