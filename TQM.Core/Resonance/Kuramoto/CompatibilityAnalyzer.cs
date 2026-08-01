using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether condensate compatibility (signature similarity) predicts
/// assembly formation success beyond spatial proximity.
/// </summary>
public static class CompatibilityAnalyzer
{
    public sealed record CompatResult(
        double FreqDifference, double PhaseDifference,
        bool StableAssembly, double FinalR);

    /// <summary>
    /// Tests two-condensate assembly with controlled parameter differences.
    /// </summary>
    public static CompatResult TestPair(
        double freq1, double freq2, double phaseOff,
        double k, double lambda, int oscPerCond, double separation, Random rng, int iterations = 3000)
    {
        int totalN = 2 * oscPerCond;
        var network = new TemporalNetwork(totalN);

        // Condensate 1.
        for (int i = 0; i < oscPerCond; i++)
        {
            double angle = rng.NextDouble() * 2.0 * Math.PI;
            double radius = rng.NextDouble() * lambda * 0.8;
            network.AddNode(new TemporalNode(i, phase: rng.NextDouble() * 2.0 * Math.PI, frequency: freq1)
            { X = Math.Clamp(0.4 + radius * Math.Cos(angle), 0, 1), Y = Math.Clamp(0.5 + radius * Math.Sin(angle), 0, 1) });
        }

        // Condensate 2.
        for (int i = 0; i < oscPerCond; i++)
        {
            double angle = rng.NextDouble() * 2.0 * Math.PI;
            double radius = rng.NextDouble() * lambda * 0.8;
            network.AddNode(new TemporalNode(oscPerCond + i, phase: rng.NextDouble() * 2.0 * Math.PI + phaseOff, frequency: freq2)
            { X = Math.Clamp(0.4 + separation + radius * Math.Cos(angle), 0, 1), Y = Math.Clamp(0.5 + radius * Math.Sin(angle), 0, 1) });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = totalN };
        var df = new LocalDensityField(20);

        for (int iter = 0; iter < iterations; iter++) sim.Step();

        df.Compute(network, neighborhoodCells: 1);
        int domains = df.CellsAboveThreshold(0.80);
        double globalR = SynchronizationMetrics.FromNetwork(network, iterations).OrderParameterR;

        // Stable assembly = both condensates preserve identity (2+ domains) AND global R > 0.8.
        bool stable = domains > 10 && globalR > 0.8;

        return new CompatResult(Math.Abs(freq1 - freq2), Math.Abs(phaseOff), stable, globalR);
    }
}
