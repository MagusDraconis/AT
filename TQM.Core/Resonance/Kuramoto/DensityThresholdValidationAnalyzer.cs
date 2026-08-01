using System.Collections.Concurrent;
using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Validates the universality of the critical local density ρc
/// across different TQM parameter environments.
/// </summary>
public static class DensityThresholdValidationAnalyzer
{
    public sealed record ThresholdMeasurement(
        int N, double K, double Lambda, string Placement,
        double EstimatedRhoC, double MeanBirthDensity, double ThresholdVariance,
        int CondensateCount, double MeanLifetime, double MeanFinalR);

    /// <summary>
    /// Runs a single validation measurement.
    /// </summary>
    public static ThresholdMeasurement Measure(
        int n, double k, double lambda, string placement, Random rng, int iterations = 3000)
    {
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
        {
            CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3
        };

        var records = LocalDensityThresholdAnalyzer.Analyze(
            network, sim, densityField, condAnalyzer, n, k, placement, iterations);

        double estimatedRhoC = records.Count > 0 ? records.Select(r => r.LocalDensity).Median() : 0;
        double meanBirthDensity = records.Count > 0 ? records.Average(r => r.LocalDensity) : 0;
        double variance = records.Count > 1
            ? records.Average(r => (r.LocalDensity - meanBirthDensity) * (r.LocalDensity - meanBirthDensity))
            : 0;
        int condCount = records.Count;
        double meanLifetime = records.Count > 0 ? records.Average(r => r.Lifetime) : 0;
        double meanFinalR = records.Count > 0 ? records.Average(r => r.LocalR) : 0;

        return new ThresholdMeasurement(n, k, lambda, placement,
            estimatedRhoC, meanBirthDensity, variance, condCount, meanLifetime, meanFinalR);
    }

    private static void PlaceNode(TemporalNode node, string placement, Random rng, int idx, int total)
    {
        switch (placement)
        {
            case "Uniform":
                node.X = rng.NextDouble(); node.Y = rng.NextDouble(); break;
            case "GaussianBlobs":
                var bc = new[] { (0.25, 0.25), (0.75, 0.25), (0.5, 0.75) };
                var (bx, by) = bc[idx % 3];
                node.X = Math.Clamp(bx + NextGaussian(rng) * 0.08, 0, 1);
                node.Y = Math.Clamp(by + NextGaussian(rng) * 0.08, 0, 1); break;
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

    private static double Median(this IEnumerable<double> source)
    {
        var sorted = source.OrderBy(x => x).ToList();
        if (sorted.Count == 0) return 0;
        int mid = sorted.Count / 2;
        return sorted.Count % 2 == 0 ? (sorted[mid - 1] + sorted[mid]) / 2.0 : sorted[mid];
    }
}
