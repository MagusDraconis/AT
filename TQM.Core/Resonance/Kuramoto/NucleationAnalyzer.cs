using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether a pre-coherent nucleus of a given size can survive
/// and grow when embedded in an incoherent oscillator background.
/// </summary>
public static class NucleationAnalyzer
{
    /// <summary>
    /// Runs a single nucleation experiment.
    /// </summary>
    public static NucleationResult TestNucleus(
        int totalN,
        int nucleusSize,
        double k,
        double lambda,
        int totalIterations,
        Random rng)
    {
        int backgroundN = totalN - nucleusSize;
        var network = new TemporalNetwork(totalN);

        double cx = 0.5, cy = 0.5;
        double nucleusRadius = 0.02;

        // Create coherent nucleus.
        double nucleusPhase = rng.NextDouble() * 2.0 * Math.PI;
        for (int i = 0; i < nucleusSize; i++)
        {
            double angle = rng.NextDouble() * 2.0 * Math.PI;
            double radius = rng.NextDouble() * nucleusRadius;
            var node = new TemporalNode(i, phase: nucleusPhase, frequency: 1.0)
            {
                X = Math.Clamp(cx + radius * Math.Cos(angle), 0, 1),
                Y = Math.Clamp(cy + radius * Math.Sin(angle), 0, 1)
            };
            network.AddNode(node);
        }

        // Create incoherent background spread across the domain.
        for (int i = nucleusSize; i < totalN; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase: phase, frequency: freq)
            {
                X = rng.NextDouble(),
                Y = rng.NextDouble()
            };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = totalN };
        var densityField = new LocalDensityField(20);

        bool survived = false;
        bool grew = false;
        int finalSize = nucleusSize;
        double finalR = 0;
        int lifetime = totalIterations;

        for (int iter = 0; iter < totalIterations; iter++)
        {
            sim.Step();

            if ((iter + 1) % 100 == 0 || iter == totalIterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                finalR = densityField.MaxLocalR();
                finalSize = densityField.CellsAboveThreshold(0.80);

                if (finalR < 0.3 && !survived)
                {
                    lifetime = iter + 1;
                }

                if (finalR >= 0.8)
                    survived = true;

                if (finalSize > nucleusSize)
                    grew = true;
            }
        }

        double growthRate = (finalSize - nucleusSize) / (double)totalIterations;

        return new NucleationResult(nucleusSize, survived, grew, finalSize, finalR, lifetime, growthRate);
    }
}
