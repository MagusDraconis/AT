using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether repeated stimulation trains condensates to respond differently.
/// </summary>
public static class LearningAnalyzer
{
    public sealed record LearningResult(
        int PulseCount, double TrainingStrength,
        double ProbeResponse, double RecoveryTime,
        double FinalR, bool Learned);

    /// <summary>
    /// Trains a condensate with repeated pulses, then probes it.
    /// </summary>
    public static LearningResult Analyze(
        int pulseCount, double trainingStrength,
        double beta, double k, double lambda, int n, Random rng, int baseIter = 6000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);

        // Baseline formation.
        int formIter = 2000;
        sim.Run(formIter);
        double baselineR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;

        // Training phase: repeated pulses with recovery between them.
        int intervalBetweenPulses = Math.Max(50, baseIter / Math.Max(1, pulseCount * 2));
        for (int p = 0; p < pulseCount; p++)
        {
            // Apply training pulse.
            foreach (var node in network.Nodes)
                node.Phase += (rng.NextDouble() * 2 - 1) * trainingStrength;
            // Recover.
            sim.Run(intervalBetweenPulses);
        }

        // Probe: standard test pulse.
        foreach (var node in network.Nodes)
            node.Phase += (rng.NextDouble() * 2 - 1) * 0.5;

        // Measure recovery.
        int recoveryIter = -1;
        for (int iter = 0; iter < 500; iter++)
        {
            sim.Step();
            double r = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
            if (r > baselineR * 0.9 && recoveryIter < 0)
                recoveryIter = iter;
        }

        double finalR = SynchronizationMetrics.FromNetwork(network, 0).OrderParameterR;
        double probeResponse = Math.Abs(finalR - baselineR);
        bool learned = finalR > baselineR * 0.95 && recoveryIter >= 0;

        return new LearningResult(pulseCount, trainingStrength, probeResponse,
            recoveryIter >= 0 ? recoveryIter : 500, finalR, learned);
    }
}
