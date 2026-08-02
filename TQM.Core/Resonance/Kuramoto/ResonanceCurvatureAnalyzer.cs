using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Measures intrinsic curvature of the resonance state-space manifold
/// via geodesic deviation and determines whether curvature creates
/// effective directed motion (analogous to gravity in phase space).
/// </summary>
public static class ResonanceCurvatureAnalyzer
{
    // ── Types ────────────────────────────────────────────────────────

    public readonly record struct GeodesicPoint(
        int Iteration, double R, double Freq, double PhaseVar,
        double Energy, double MemScore, double LocalCoh);

    public sealed record TrajectoryBundle(
        string History, int Seed,
        List<List<GeodesicPoint>> Trajectories, // multiple trajectories from same baseline
        double[] PerturbationMagnitudes);

    public sealed record DeviationPair(
        double InitialSeparation,
        double FinalSeparation,
        double SeparationChange,      // final - initial (negative = convergence)
        double CurvatureEstimate,     // positive = trajectories converge
        double ConvergenceRate,       // how fast they converge per iteration
        bool Converges);

    public sealed record CurvatureReport(
        double MeanCurvature,
        double CurvatureStd,
        double ConvergentFraction,    // fraction of pairs that converge
        double MeanConvergenceRate,
        double MeanSeparationChange,
        string CurvatureClass,        // Flat/Weakly/Strongly Curved/Geometrically Dominated
        string EffectiveGravity,      // description of curvature-driven motion
        List<DeviationPair> AllPairs,
        int TotalPairs);

    // ── Helpers ──────────────────────────────────────────────────────

    private static double[] Vec(GeodesicPoint p) =>
        new[] { p.R, p.Freq, p.PhaseVar, p.Energy, p.MemScore, p.LocalCoh };

    private static double Dist(double[] a, double[] b)
    {
        double s = 0; for (int d = 0; d < a.Length; d++)
        { double dd = a[d] - b[d]; s += dd * dd; }
        return Math.Sqrt(s);
    }

    private static GeodesicPoint Measure(TemporalNetwork net, int iter)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        double mem = Mem(net);
        var df = new LocalDensityField(20); df.Compute(net, 1);
        return new GeodesicPoint(iter, m.OrderParameterR,
            net.Nodes.Average(n => n.Frequency), m.PhaseVariance,
            m.OrderParameterR * net.Nodes.Average(n => n.Frequency), mem, df.MaxLocalR());
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

    // ── Normalize ────────────────────────────────────────────────────

    private static void Normalize(List<double[]> vecs)
    {
        double[] means = new double[6], stds = new double[6];
        for (int d = 0; d < 6; d++)
        {
            var vals = vecs.Select(v => v[d]).ToList();
            means[d] = vals.Average();
            stds[d] = Math.Sqrt(vals.Average(x => (x - means[d]) * (x - means[d])));
            if (stds[d] < 1e-10) stds[d] = 1.0;
        }
        foreach (var v in vecs)
            for (int d = 0; d < 6; d++)
                v[d] = (v[d] - means[d]) / stds[d];
    }

    // ── Main: collect geodesic deviation data ────────────────────────

    public static CurvatureReport AnalyzeCurvature(
        string history, double beta, double k, double lambda, int n, int[] seeds,
        double[] perturbationMagnitudes, int recoveryIters = 2000, int interval = 50)
    {
        var allPairs = new List<DeviationPair>();

        foreach (int seed in seeds)
        {
            // Build and train baseline condensate.
            var rng = new Random(seed);
            var network = new TemporalNetwork(n);
            for (int i = 0; i < n; i++)
                network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                    0.5 + rng.NextDouble() * 1.5)
                { X = rng.NextDouble(), Y = rng.NextDouble() });
            network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
            var sim = new MemoryTemporalSimulation(network, beta);
            sim.Run(1500);
            ApplyHistory(network, history, rng, sim);

            // Save baseline state (phases, frequencies).
            double[] baselinePhases = network.Nodes.Select(nd => nd.Phase).ToArray();
            double[] baselineFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();

            // Generate trajectory bundle: perturb baseline, then track recovery.
            var bundleTrajs = new List<List<GeodesicPoint>>();
            foreach (double mag in perturbationMagnitudes)
            {
                // Restore baseline.
                for (int i = 0; i < n; i++)
                { network.Nodes[i].Phase = baselinePhases[i]; network.Nodes[i].Frequency = baselineFreqs[i]; }
                sim = new MemoryTemporalSimulation(network, beta);

                // Apply perturbation: phase noise at given magnitude.
                foreach (var node in network.Nodes)
                    node.Phase += (rng.NextDouble() * 2 - 1) * mag;
                sim.Run(100);

                // Track recovery.
                var traj = new List<GeodesicPoint>();
                for (int iter = 0; iter <= recoveryIters; iter += interval)
                {
                    if (iter > 0) sim.Run(interval);
                    traj.Add(Measure(network, iter));
                }
                bundleTrajs.Add(traj);
            }

            // Compute geodesic deviation for all pairs.
            for (int i = 0; i < bundleTrajs.Count; i++)
            {
                for (int j = i + 1; j < bundleTrajs.Count; j++)
                {
                    var ta = bundleTrajs[i];
                    var tb = bundleTrajs[j];
                    int pts = Math.Min(ta.Count, tb.Count);

                    // Separation at start and end.
                    double d0 = Dist(Vec(ta[0]), Vec(tb[0]));
                    double dEnd = Dist(Vec(ta[pts - 1]), Vec(tb[pts - 1]));
                    if (d0 < 1e-10) continue;

                    double sepChange = dEnd - d0;

                    // Curvature estimate: average of -d''(t)/d(t) over trajectory.
                    // d''(t) ≈ d(t+2δ) - 2d(t+δ) + d(t) / δ²
                    double curvSum = 0; int curvCount = 0;
                    for (int t = 0; t < pts - 2; t++)
                    {
                        double dt0 = Dist(Vec(ta[t]), Vec(tb[t]));
                        double dt1 = Dist(Vec(ta[t + 1]), Vec(tb[t + 1]));
                        double dt2 = Dist(Vec(ta[t + 2]), Vec(tb[t + 2]));
                        if (dt0 < 1e-10) continue;
                        double d2 = dt2 - 2 * dt1 + dt0;
                        double curv = -d2 / dt0;
                        curvSum += curv; curvCount++;
                    }
                    double curvEstimate = curvCount > 0 ? curvSum / curvCount : 0;

                    // Convergence rate: average -Δd/Δiter.
                    double convRate = 0; int convCount = 0;
                    for (int t = 0; t < pts - 1; t++)
                    {
                        double d1 = Dist(Vec(ta[t]), Vec(tb[t]));
                        double d2 = Dist(Vec(ta[t + 1]), Vec(tb[t + 1]));
                        if (d1 < 1e-10) continue;
                        convRate += -(d2 - d1) / d1;
                        convCount++;
                    }
                    double meanConvRate = convCount > 0 ? convRate / convCount : 0;

                    allPairs.Add(new DeviationPair(d0, dEnd, sepChange,
                        curvEstimate, meanConvRate, sepChange < 0));
                }
            }
        }

        double meanCurv = allPairs.Average(p => p.CurvatureEstimate);
        double stdCurv = allPairs.Count > 1 ?
            Math.Sqrt(allPairs.Average(p => (p.CurvatureEstimate - meanCurv) * (p.CurvatureEstimate - meanCurv))) : 0;
        double convFrac = (double)allPairs.Count(p => p.Converges) / allPairs.Count;
        double meanCR = allPairs.Average(p => p.ConvergenceRate);
        double meanSC = allPairs.Average(p => p.SeparationChange);

        string curvClass = Math.Abs(meanCurv) > 0.01 ? "D: Geometrically Dominated" :
                           Math.Abs(meanCurv) > 0.005 ? "C: Strongly Curved" :
                           Math.Abs(meanCurv) > 0.001 ? "B: Weakly Curved" :
                           "A: Flat";

        string gravDesc = convFrac > 0.7
            ? $"Effective attraction: {convFrac:P0} of trajectories CONVERGE (curvature-driven focusing)"
            : convFrac > 0.5
            ? $"Mild convergence: {convFrac:P0} of trajectories converge"
            : "No effective attraction";

        return new CurvatureReport(meanCurv, stdCurv, convFrac, meanCR, meanSC,
            curvClass, gravDesc, allPairs, allPairs.Count);
    }
}
