using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Computes geometric resonance metrics for oscillator neighborhoods
/// at condensate birth events.
/// </summary>
public static class GeometricResonanceAnalyzer
{
    public sealed record GeometricPoint(
        double Density, double NeighborCount,
        double MeanDistance, double DistVariance, double RadialSymmetry,
        double Anisotropy, double Compactness, double ConvexArea, double NeighborEntropy,
        int N, double K, double Lambda, string Placement);

    /// <summary>
    /// Collects all metrics for condensates in one simulation.
    /// </summary>
    public static List<GeometricPoint> Collect(
        int n, double k, double lambda, string placement, Random rng, int iterations = 2000)
    {
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
        {
            double phase = rng.NextDouble() * 2.0 * Math.PI;
            double freq = 0.5 + rng.NextDouble() * 1.5;
            var node = new TemporalNode(i, phase, freq);
            Place(node, placement, rng, i, n);
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new TemporalSimulation(network) { TimeStep = 0.01, CouplingStrength = n };
        var densityField = new LocalDensityField(20);
        var condAnalyzer = new ResonanceCondensationAnalyzer
            { CondensationThreshold = 0.80, MinCondensateCells = 2, OverlapThreshold = 0.3 };

        var points = new List<GeometricPoint>();

        for (int iter = 0; iter < iterations; iter++)
        {
            sim.Step();
            if (iter == iterations / 2 || iter == iterations - 1)
            {
                densityField.Compute(network, neighborhoodCells: 1);
                var condensates = condAnalyzer.DetectAndTrack(densityField, iter + 1);

                foreach (var c in condensates)
                {
                    int bo = Math.Clamp(c.Cells.Count > 0 ? c.Cells[0].Item1 * n / 400 : 0, 0, n - 1);
                    var nodes = network.Nodes;
                    double ox = nodes[bo].X, oy = nodes[bo].Y;

                    var nbrDistances = new List<double>();
                    var nbrAngles = new List<double>();

                    for (int j = 0; j < n; j++)
                    {
                        if (j == bo) continue;
                        double dx = nodes[j].X - ox, dy = nodes[j].Y - oy;
                        double d = Math.Sqrt(dx * dx + dy * dy);
                        if (d <= lambda)
                        {
                            nbrDistances.Add(d);
                            nbrAngles.Add(Math.Atan2(dy, dx));
                        }
                    }

                    int nc = nbrDistances.Count;
                    if (nc < 2) continue;

                    // Mean distance.
                    double meanDist = nbrDistances.Average();
                    double distVar = nbrDistances.Average(d => (d - meanDist) * (d - meanDist));

                    // Radial symmetry: how uniform are angular positions?
                    // Sort angles, compute gap uniformity.
                    nbrAngles.Sort();
                    double maxGap = 0, sumGapSq = 0;
                    for (int i = 0; i < nc; i++)
                    {
                        double gap = (i == nc - 1)
                            ? (2 * Math.PI + nbrAngles[0] - nbrAngles[i])
                            : (nbrAngles[i + 1] - nbrAngles[i]);
                        maxGap = Math.Max(maxGap, gap);
                        sumGapSq += gap * gap;
                    }
                    double expectedGap = 2 * Math.PI / nc;
                    double radialSym = 1.0 - (maxGap - expectedGap) / (2 * Math.PI - expectedGap);

                    // Anisotropy: ratio of principal axes of neighbor positions.
                    double sumX2 = 0, sumY2 = 0, sumXY = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == bo) continue;
                        double dx = nodes[j].X - ox, dy = nodes[j].Y - oy;
                        double d2 = dx * dx + dy * dy;
                        if (d2 <= lambda * lambda)
                        {
                            sumX2 += dx * dx;
                            sumY2 += dy * dy;
                            sumXY += dx * dy;
                        }
                    }
                    double trace = sumX2 + sumY2;
                    double det = sumX2 * sumY2 - sumXY * sumXY;
                    double disc = Math.Sqrt(Math.Max(0, trace * trace - 4 * det));
                    double lambda1 = (trace + disc) / 2;
                    double lambda2 = (trace - disc) / 2;
                    double anisotropy = lambda2 > 1e-10 ? Math.Sqrt(lambda1 / lambda2) : 1;

                    // Compactness: fraction of neighbors within λ/2.
                    int compact = nbrDistances.Count(d => d < lambda / 2);
                    double compactness = (double)compact / nc;

                    // Convex hull area (approximate by bounding box area).
                    double minDist = nbrDistances.Min(), maxDist = nbrDistances.Max();
                    double hullArea = Math.PI * maxDist * maxDist - Math.PI * minDist * minDist;

                    // Neighbor entropy of angular distribution.
                    double entropy = 0;
                    for (int i = 0; i < nc; i++)
                    {
                        double gap = (i == nc - 1)
                            ? (2 * Math.PI + nbrAngles[0] - nbrAngles[i])
                            : (nbrAngles[i + 1] - nbrAngles[i]);
                        double p = gap / (2 * Math.PI);
                        if (p > 1e-15) entropy -= p * Math.Log(p);
                    }

                    int gx = (int)(ox * densityField.GridSize), gy = (int)(oy * densityField.GridSize);
                    double dens = densityField.GetLocalDensity(Math.Clamp(gx, 0, 19), Math.Clamp(gy, 0, 19));

                    points.Add(new GeometricPoint(dens, nc, meanDist, distVar, radialSym,
                        anisotropy, compactness, hullArea, entropy, n, k, lambda, placement));
                }
            }
        }

        return points;
    }

    private static void Place(TemporalNode node, string p, Random rng, int idx, int total)
    {
        if (p == "Uniform") { node.X = rng.NextDouble(); node.Y = rng.NextDouble(); return; }
        var cc = new[] { (0.1, 0.1), (0.9, 0.1), (0.5, 0.5), (0.1, 0.9), (0.9, 0.9) };
        var (cx, cy) = cc[idx % 5];
        node.X = Math.Clamp(cx + NextGaussian(rng) * 0.02, 0, 1);
        node.Y = Math.Clamp(cy + NextGaussian(rng) * 0.02, 0, 1);
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) * Math.Cos(2.0 * Math.PI * u2);
    }
}
