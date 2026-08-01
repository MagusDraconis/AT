using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Creates and analyzes frustrated temporal networks with both positive
/// (synchronizing) and negative (anti-synchronizing) couplings.
/// </summary>
public static class FrustratedNetworkAnalyzer
{
    public sealed record FrustratedResult(
        double FrustrationFraction, double FinalR, int DomainCount,
        double MaxDomainSize, double MeanDomainSize, int Seed);

    /// <summary>
    /// Runs a single frustrated network simulation.
    /// </summary>
    public static FrustratedResult Run(
        int n, double k, double lambda, double frustrationFrac, Random rng, int iterations = 3000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 1.0;
            var node = new TemporalNode(i, phase, freq)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        // Fill coupling: distance-dependent with random sign for frustrated fraction.
        var nodes = network.Nodes;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dx = nodes[i].X - nodes[j].X, dy = nodes[i].Y - nodes[j].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                double coupling = k * Math.Exp(-dist / lambda);

                // Random sign flip for frustrated fraction.
                if (rng.NextDouble() < frustrationFrac)
                    coupling = -coupling;

                network.Matrix[i, j] = coupling;
                network.Matrix[j, i] = coupling;
            }
        }

        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };

        for (int iter = 0; iter < iterations; iter++) sim.Step();

        double globalR = SynchronizationMetrics.FromNetwork(network, iterations).OrderParameterR;

        // Detect phase domains: cluster oscillators by phase proximity.
        var phases = nodes.Select(nd => nd.Phase).ToList();
        double window = 0.5; // rad
        var visited = new bool[n];
        var domains = new List<int>();

        for (int i = 0; i < n; i++)
        {
            if (visited[i]) continue;
            int domainSize = 0;
            var queue = new Queue<int>(); queue.Enqueue(i); visited[i] = true;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue(); domainSize++;
                for (int j = 0; j < n; j++)
                {
                    if (visited[j]) continue;
                    double diff = Math.Abs(TemporalSimulation.NormalizePhase(phases[v] - phases[j] + Math.PI) - Math.PI);
                    if (diff < window) { visited[j] = true; queue.Enqueue(j); }
                }
            }
            domains.Add(domainSize);
        }

        return new FrustratedResult(frustrationFrac, globalR, domains.Count,
            domains.Max(), domains.Average(), rng.Next());
    }
}
