using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tracks candidate conserved quantities throughout a condensate's lifecycle.
/// </summary>
public static class ConservedQuantityAnalyzer
{
    public sealed record LifecycleSnapshot(
        int Iteration, string Phase,
        double TotalPhaseVar, double LocalPhaseVar, double GlobalR, double LocalR,
        double NeighborCount, double WeightedNeighbors, double Coherence,
        double ClusterSize, double Density, double FreqStd);

    public sealed record QuantityResult(
        string Name, double BirthVal, double MatureVal, double PerturbedVal,
        double RecoveredVal, double FinalVal, double InvarianceScore);

    public static (List<LifecycleSnapshot> Snapshots, List<QuantityResult> Results) Analyze(
        int n, double k, double lambda, Random rng, int totalIter = 4000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 1.0 + (rng.NextDouble() - 0.5) * 0.2;
            var node = new TemporalNode(i, phase, freq);
            var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
            var (cx, cy) = cc[i % 5];
            node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
            node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);
        var snapshots = new List<LifecycleSnapshot>();

        for (int iter = 0; iter < totalIter; iter++)
        {
            sim.Step();

            if (iter == totalIter / 2 - 1)
            {
                // Apply perturbation: add phase noise.
                foreach (var node in network.Nodes)
                    node.Phase += (rng.NextDouble() * 2 - 1) * 0.5;
            }

            if (iter % 200 == 0 || iter == totalIter - 1 || iter == totalIter / 2)
            {
                densityField.Compute(network, neighborhoodCells: 1);

                string phase = iter < totalIter / 2 ? (iter < 500 ? "Birth" : "Growth") :
                               iter == totalIter / 2 ? "Perturbed" :
                               iter < totalIter * 3 / 4 ? "Recovery" : "Final";

                double globalR = SynchronizationMetrics.FromNetwork(network, iter + 1).OrderParameterR;
                double localR = densityField.MaxLocalR();
                double phaseVar = 1.0 - globalR;

                var nodes = network.Nodes;
                double sumFreq = 0, sumFreqSq = 0;
                int nc = 0;
                for (int i = 0; i < n; i++)
                {
                    sumFreq += nodes[i].Frequency;
                    sumFreqSq += nodes[i].Frequency * nodes[i].Frequency;
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;
                        double dx = nodes[j].X - nodes[i].X, dy = nodes[j].Y - nodes[i].Y;
                        if (Math.Sqrt(dx * dx + dy * dy) <= lambda) nc++;
                    }
                }
                double freqStd = Math.Sqrt(sumFreqSq / n - (sumFreq / n) * (sumFreq / n));
                double avgNc = (double)nc / n;
                int clusterSize = densityField.CellsAboveThreshold(0.80);

                // Find a representative oscillator for density/neighbors.
                int bo = n / 4;
                double density = densityField.GetLocalDensity(
                    Math.Clamp((int)(nodes[bo].X * 20), 0, 19),
                    Math.Clamp((int)(nodes[bo].Y * 20), 0, 19));
                int localNc = 0; double weightedN = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == bo) continue;
                    double dx = nodes[bo].X - nodes[j].X, dy = nodes[bo].Y - nodes[j].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy);
                    if (d <= lambda) { localNc++; weightedN += k * Math.Exp(-d / lambda); }
                }

                snapshots.Add(new LifecycleSnapshot(iter, phase, phaseVar, 0, globalR, localR,
                    localNc, weightedN, localR, clusterSize, density, freqStd));
            }
        }

        // Compute invariance scores.
        var quantities = new List<(string Name, Func<LifecycleSnapshot, double> Sel)>
        {
            ("Total Phase Var", s => s.TotalPhaseVar),
            ("Global R", s => s.GlobalR),
            ("Local R", s => s.LocalR),
            ("Neighbor Count", s => s.NeighborCount),
            ("Weighted Neighbors", s => s.WeightedNeighbors),
            ("Cluster Size", s => s.ClusterSize),
            ("Density", s => s.Density),
            ("Freq Std", s => s.FreqStd),
            ("Density×Coherence", s => s.Density * s.Coherence),
            ("ClusterSize×Coherence", s => s.ClusterSize * s.Coherence),
            ("Neighbors×Coherence", s => s.NeighborCount * s.Coherence),
            ("Density×ClusterSize", s => s.Density * s.ClusterSize),
            ("Density×Neighbors", s => s.Density * s.NeighborCount),
        };

        var results = new List<QuantityResult>();
        foreach (var (name, sel) in quantities)
        {
            var vals = snapshots.Select(sel).ToList();
            double birth = vals.First(), mature = vals[vals.Count / 4],
                   perturbed = vals[vals.Count / 2], recovered = vals[vals.Count * 3 / 4],
                   final = vals.Last();
            double mean = vals.Average();
            double cv = mean > 1e-10 ? Math.Sqrt(vals.Average(v => (v - mean) * (v - mean))) / mean : 1;
            double score = 1.0 - Math.Min(1.0, cv);

            results.Add(new QuantityResult(name, birth, mature, perturbed, recovered, final, score));
        }

        return (snapshots, results);
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
