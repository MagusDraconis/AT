using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether condensates occupy stable minima of a resonance potential
/// by applying perturbations and measuring recovery.
/// </summary>
public static class ResonancePotentialAnalyzer
{
    public sealed record PerturbationResult(
        double Magnitude, double Displacement, int RecoveryIter,
        double FinalDistance, double RestoringRate);

    /// <summary>
    /// Measures condensate state in a reduced feature space: [density, localR, neighborCount].
    /// </summary>
    private static (double D, double R, double N) MeasureState(TemporalNetwork network, LocalDensityField df, int n, double lambda, double k)
    {
        df.Compute(network, neighborhoodCells: 1);
        double r = df.MaxLocalR();
        double d = df.MeanLocalR();
        var nodes = network.Nodes;
        int nc = 0; int bo = n / 4;
        for (int j = 0; j < n; j++)
        { if (j == bo) continue; double dx = nodes[bo].X - nodes[j].X, dy = nodes[bo].Y - nodes[j].Y; if (Math.Sqrt(dx * dx + dy * dy) <= lambda) nc++; }
        return (d, r, nc);
    }

    private static double Distance((double D, double R, double N) a, (double D, double R, double N) b)
        => Math.Sqrt((a.D - b.D) * (a.D - b.D) + (a.R - b.R) * (a.R - b.R) + (a.N - b.N) * (a.N - b.N) / 10000.0);

    public static List<PerturbationResult> Analyze(
        int n, double k, double lambda, Random rng, double magnitude, int recoveryIterations = 1000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 1.0 + (rng.NextDouble() - 0.5) * 0.1;
            var node = new TemporalNode(i, phase, freq);
            var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
            var (cx, cy) = cc[i % 5];
            node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
            node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var df = new LocalDensityField(20);

        // Form condensate.
        for (int iter = 0; iter < 1500; iter++) sim.Step();

        // Measure baseline.
        var baseline = MeasureState(network, df, n, lambda, k);

        // Apply perturbation.
        foreach (var node in network.Nodes)
            node.Phase += (rng.NextDouble() * 2 - 1) * magnitude;

        // Measure displacement immediately after perturbation.
        var perturbed = MeasureState(network, df, n, lambda, k);
        double displacement = Distance(baseline, perturbed);

        // Recovery: track return to baseline.
        int recoveryIter = -1;
        double finalDist = displacement;
        for (int iter = 0; iter < recoveryIterations; iter++)
        {
            sim.Step();
            if (iter % 50 == 0 || iter == recoveryIterations - 1)
            {
                var current = MeasureState(network, df, n, lambda, k);
                double dist = Distance(baseline, current);
                if (dist < displacement * 0.1 && recoveryIter < 0)
                    recoveryIter = iter;
                finalDist = dist;
            }
        }

        double restoringRate = displacement / Math.Max(1, recoveryIter > 0 ? recoveryIter : recoveryIterations);

        return new List<PerturbationResult>
        {
            new(magnitude, displacement, recoveryIter, finalDist, restoringRate)
        };
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
