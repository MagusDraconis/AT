using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tracks state transitions through resonance space and measures
/// whether recovery follows preferred geodesic trajectories.
/// </summary>
public static class StateSpaceGeodesicAnalyzer
{
    // ── Types ────────────────────────────────────────────────────────

    public enum PerturbType { EnergyCollapse, PhaseNoise, MemoryDisrupt }

    public readonly record struct StatePoint(
        int Iteration, double R, double Freq, double PhaseVar,
        double Energy, double MemScore, double LocalCoh);

    public sealed record Trajectory(
        string History, PerturbType Perturbation, int Seed,
        List<StatePoint> Points,
        double PathLength, double Curvature, double ConvergenceSpeed);

    public sealed record GeodesicReport(
        double MeanPathLength,
        double MeanCurvature,
        double RepeatabilityScore,  // how consistent are paths from same perturbation
        double ConvergenceScore,   // how fast states converge to baseline
        double ShortestPathRatio,  // actual path / direct distance
        string Classification,
        int TotalTrajectories);

    // ── Measurement ──────────────────────────────────────────────────

    private static StatePoint Measure(TemporalNetwork net, int iter)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        double mem = Mem(net);
        var df = new LocalDensityField(20); df.Compute(net, 1);
        return new StatePoint(iter, m.OrderParameterR,
            net.Nodes.Average(n => n.Frequency), m.PhaseVariance,
            m.OrderParameterR * net.Nodes.Average(n => n.Frequency),
            mem, df.MaxLocalR());
    }

    private static double Mem(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++)
            { double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); sum += Math.Abs(s); sumSq += s * s; c++; }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    // ── 6D distance ──────────────────────────────────────────────────

    private static double[] Vec(StatePoint p) =>
        new[] { p.R, p.Freq, p.PhaseVar, p.Energy, p.MemScore, p.LocalCoh };

    private static double Dist(double[] a, double[] b)
    {
        double s = 0; for (int i = 0; i < a.Length; i++) { double d = a[i] - b[i]; s += d * d; }
        return Math.Sqrt(s);
    }

    // ── History ──────────────────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork nw, string h, Random rng,
        MemoryTemporalSimulation sim)
    {
        foreach (char p in h)
        {
            double shift = p == 'A' ? 0.4 : p == 'B' ? -0.4 : (rng.NextDouble() * 2 - 1) * 0.4;
            foreach (var n in nw.Nodes) n.Phase += shift;
            sim.Run(400);
        }
    }

    // ── Trajectory collection ────────────────────────────────────────

    public static List<Trajectory> CollectTrajectories(
        string history, double beta, double k, double lambda, int n, int seed,
        int recoveryIters = 2000, int interval = 50)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });
        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        double[] origFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);
        ApplyHistory(network, history, rng, sim);

        var trajectories = new List<Trajectory>();

        // Test each perturbation type.
        foreach (PerturbType pt in Enum.GetValues<PerturbType>())
        {
            // Reset to baseline.
            // Rebuild from scratch to ensure clean start.
            network = new TemporalNetwork(n);
            var rng2 = new Random(seed + (int)pt * 10007);
            for (int i = 0; i < n; i++)
                network.AddNode(new TemporalNode(i, rng2.NextDouble() * 2 * Math.PI,
                    0.5 + rng2.NextDouble() * 1.5)
                { X = rng2.NextDouble(), Y = rng2.NextDouble() });
            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
            origFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();
            sim = new MemoryTemporalSimulation(network, beta);
            sim.Run(1500);
            ApplyHistory(network, history, rng2, sim);

            // Apply perturbation.
            switch (pt)
            {
                case PerturbType.EnergyCollapse:
                    foreach (var node in network.Nodes) node.Frequency *= 3.0;
                    sim.Run(200);
                    for (int i = 0; i < n; i++) network.Nodes[i].Frequency = origFreqs[i];
                    break;
                case PerturbType.PhaseNoise:
                    foreach (var node in network.Nodes) node.Phase += (rng2.NextDouble() * 2 - 1) * 2.0;
                    sim.Run(100);
                    break;
                case PerturbType.MemoryDisrupt:
                    foreach (var node in network.Nodes) node.Phase += (rng2.NextDouble() * 2 - 1) * 3.0;
                    sim.Run(300);
                    break;
            }

            // Track recovery trajectory.
            var points = new List<StatePoint>();
            for (int iter = 0; iter <= recoveryIters; iter += interval)
            {
                if (iter > 0) sim.Run(interval);
                points.Add(Measure(network, iter));
            }

            // Compute path metrics.
            double pathLen = 0, curvature = 0;
            for (int i = 1; i < points.Count; i++)
                pathLen += Dist(Vec(points[i]), Vec(points[i - 1]));

            // Curvature: sum of angle changes.
            for (int i = 2; i < points.Count; i++)
            {
                var v1 = Vec(points[i - 1]); var v0 = Vec(points[i - 2]); var v2 = Vec(points[i]);
                double d01 = Dist(v0, v1), d12 = Dist(v1, v2), d02 = Dist(v0, v2);
                if (d01 > 1e-10 && d12 > 1e-10)
                {
                    double cosAngle = (d01 * d01 + d12 * d12 - d02 * d02) / (2 * d01 * d12);
                    cosAngle = Math.Clamp(cosAngle, -1, 1);
                    curvature += Math.Acos(cosAngle);
                }
            }

            // Convergence speed: iterations to reach <10% of initial distance.
            double initDist = points.Count > 1 ? Dist(Vec(points[0]), Vec(points[^1])) : 1;
            int convIter = recoveryIters;
            for (int i = 1; i < points.Count; i++)
                if (Dist(Vec(points[i]), Vec(points[^1])) < initDist * 0.1)
                { convIter = points[i].Iteration; break; }

            trajectories.Add(new Trajectory(history, pt, seed, points,
                pathLen, curvature, convIter));
        }

        return trajectories;
    }

    // ── Aggregate ────────────────────────────────────────────────────

    public static GeodesicReport Analyze(List<Trajectory> trajectories)
    {
        double meanLen = trajectories.Average(t => t.PathLength);
        double meanCurv = trajectories.Average(t => t.Curvature);
        double meanConv = trajectories.Average(t => t.ConvergenceSpeed);

        // Repeatability: for same perturbation type, how consistent are paths?
        var groups = trajectories.GroupBy(t => t.Perturbation);
        double repeatSum = 0; int repeatCount = 0;
        foreach (var g in groups)
        {
            var trajs = g.ToList();
            for (int i = 0; i < trajs.Count; i++)
                for (int j = i + 1; j < trajs.Count; j++)
                {
                    // Path similarity: average point-to-point distance.
                    double sim = 0; int minPts = Math.Min(trajs[i].Points.Count, trajs[j].Points.Count);
                    for (int p = 0; p < minPts; p++)
                        sim += Dist(Vec(trajs[i].Points[p]), Vec(trajs[j].Points[p]));
                    repeatSum += 1.0 / (1.0 + sim / minPts);
                    repeatCount++;
                }
        }
        double repeatability = repeatCount > 0 ? repeatSum / repeatCount : 0;

        // Shortest path ratio: actual path / direct start-to-end distance.
        double sprSum = 0; int sprCount = 0;
        foreach (var t in trajectories)
        {
            if (t.Points.Count < 2) continue;
            double direct = Dist(Vec(t.Points[0]), Vec(t.Points[^1]));
            if (direct > 1e-10) { sprSum += t.PathLength / direct; sprCount++; }
        }
        double spr = sprCount > 0 ? sprSum / sprCount : 1;

        string classification = repeatability > 0.8 ? "D: Deterministic geodesic paths" :
                                repeatability > 0.5 ? "C: Preferred corridors" :
                                repeatability > 0.3 ? "B: Weakly structured" :
                                "A: Random transitions";

        return new GeodesicReport(meanLen, meanCurv, repeatability, meanConv, spr,
            classification, trajectories.Count);
    }
}
