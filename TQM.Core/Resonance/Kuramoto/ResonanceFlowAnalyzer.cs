using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tracks dynamic resonance flow at each timestep and measures
/// flow convergence at condensate birth events.
/// </summary>
public static class ResonanceFlowAnalyzer
{
    public sealed record FlowPoint(
        double FlowConvergence, double DRDT, double FreqGradient,
        double Density, double NeighborCount);

    /// <summary>
    /// Runs a simulation and records flow metrics at condensate birth.
    /// </summary>
    public static List<FlowPoint> Analyze(
        int n, double k, double lambda, string placement, Random rng, int iterations = 3000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq);
            if (placement == "Uniform")
            { node.X = rng.NextDouble(); node.Y = rng.NextDouble(); }
            else
            {
                var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
                var (cx, cy) = cc[i % 5];
                node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
                node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
            }
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);
        var condAnalyzer = new ResonanceCondensationAnalyzer
            { CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3 };

        // Track previous local R for dR/dt.
        double[,] prevLocalR = new double[20, 20];
        double[,] currentLocalR = new double[20, 20];
        var points = new List<FlowPoint>();

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();

            // Save previous frame.
            Array.Copy(currentLocalR, prevLocalR, currentLocalR.Length);

            densityField.Compute(network, neighborhoodCells: 1);
            for (int gx = 0; gx < 20; gx++)
                for (int gy = 0; gy < 20; gy++)
                    currentLocalR[gx, gy] = densityField.GetLocalR(gx, gy);

            if ((iter + 1) % 200 == 0 || iter == iterations - 1)
            {
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    // Flow metrics at condensate position.
                    double avgConvergence = 0, avgDRDT = 0, avgFreqGrad = 0, avgDens = 0, avgNc = 0;
                    int cellCount = 0;

                    foreach (var (gx, gy) in c.Cells)
                    {
                        if (gx < 0 || gx >= 20 || gy < 0 || gy >= 20) continue;
                        cellCount++;

                        // dR/dt at this cell.
                        double drdt = currentLocalR[gx, gy] - prevLocalR[gx, gy];
                        avgDRDT += drdt;

                        // Frequency gradient: max frequency difference among oscillators in/around this cell.
                        double maxFG = 0;
                        int countFG = 0;
                        var nodes = network.Nodes;
                        for (int i = 0; i < n; i++)
                        {
                            int ox = (int)(nodes[i].X * 20), oy = (int)(nodes[i].Y * 20);
                            if (ox == gx && oy == gy)
                            {
                                for (int j = 0; j < n; j++)
                                {
                                    if (i == j) continue;
                                    int jx = (int)(nodes[j].X * 20), jy = (int)(nodes[j].Y * 20);
                                    if (Math.Abs(jx - gx) <= 1 && Math.Abs(jy - gy) <= 1)
                                    {
                                        double df = Math.Abs(nodes[i].Frequency - nodes[j].Frequency);
                                        if (df > maxFG) maxFG = df;
                                        countFG++;
                                    }
                                }
                            }
                        }
                        avgFreqGrad += countFG > 0 ? maxFG : 0;

                        // Flow convergence: negative of frequency gradient divergence.
                        // Simplified: -∇·ω ≈ -(sum of (ωⱼ-ωᵢ) for neighbors) / n_neighbors
                        double convergence = 0;
                        int nConv = 0;
                        for (int i = 0; i < n; i++)
                        {
                            int ox = (int)(nodes[i].X * 20), oy = (int)(nodes[i].Y * 20);
                            if (ox != gx || oy != gy) continue;

                            for (int j = 0; j < n; j++)
                            {
                                if (i == j) continue;
                                double dx = nodes[j].X - nodes[i].X, dy = nodes[j].Y - nodes[i].Y;
                                double d = Math.Sqrt(dx * dx + dy * dy);
                                if (d <= lambda && d > 1e-10)
                                {
                                    convergence -= (nodes[j].Frequency - nodes[i].Frequency) / d;
                                    nConv++;
                                }
                            }
                        }
                        avgConvergence += nConv > 0 ? convergence / nConv : 0;

                        avgDens += densityField.GetLocalDensity(gx, gy);

                        // Count neighbors.
                        int nc = 0;
                        for (int i = 0; i < n; i++)
                        {
                            int ox = (int)(nodes[i].X * 20), oy = (int)(nodes[i].Y * 20);
                            if (ox == gx && oy == gy)
                            {
                                for (int j = 0; j < n; j++)
                                {
                                    if (i == j) continue;
                                    double dx = nodes[j].X - nodes[i].X, dy = nodes[j].Y - nodes[i].Y;
                                    if (Math.Sqrt(dx * dx + dy * dy) <= lambda) nc++;
                                }
                                break;
                            }
                        }
                        avgNc += nc;
                    }

                    if (cellCount > 0)
                    {
                        points.Add(new FlowPoint(
                            avgConvergence / cellCount, avgDRDT / cellCount, avgFreqGrad / cellCount,
                            avgDens / cellCount, avgNc / cellCount));
                    }
                }
            }
        }

        return points;
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
