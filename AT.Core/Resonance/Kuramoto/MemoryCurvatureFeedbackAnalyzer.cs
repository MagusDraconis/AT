using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether memory-generated curvature feeds back into future
/// memory formation, creating a self-reinforcing resonance geometry.
/// </summary>
public static class MemoryCurvatureFeedbackAnalyzer
{
    public sealed record CycleMeasurement(
        int Cycle,
        double MemoryScore,
        double Curvature,
        double ConvergenceRate,
        double R,
        double Energy);

    public sealed record FeedbackProfile(
        double Beta,
        List<CycleMeasurement> History,
        double MemoryGrowthRate,
        double CurvatureGrowthRate,
        double FeedbackCoefficient,
        bool Saturated,
        int SaturationCycle);

    public sealed record FeedbackReport(
        List<FeedbackProfile> Profiles,
        double MeanFeedbackCoefficient,
        double BetaFeedbackCorrelation,
        string FeedbackClass,
        string Description);

    // ── Helpers ──────────────────────────────────────────────────────

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

    /// <summary>
    /// Runs a multi-cycle experience loop and tracks memory and curvature evolution.
    /// </summary>
    public static FeedbackProfile RunFeedbackLoop(
        double beta, double k, double lambda, int n, int seed,
        int totalCycles = 50, int measureInterval = 5, int cycleIters = 500)
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
        ApplyHistory(network, "AB", rng, sim);

        var history = new List<CycleMeasurement>();

        for (int cycle = 0; cycle <= totalCycles; cycle++)
        {
            // Measure at intervals.
            if (cycle % measureInterval != 0 && cycle > 0 && cycle < totalCycles)
            {
                // Apply cycle perturbation and continue.
                foreach (var node in network.Nodes)
                    node.Phase += (rng.NextDouble() * 2 - 1) * 1.5;
                sim.Run(cycleIters);
                continue;
            }

            // Measure memory and curvature at this cycle.
            double memScore = Mem(network);
            var m = SynchronizationMetrics.FromNetwork(network, 0);
            double r = m.OrderParameterR;
            double energy = r * network.Nodes.Average(nd => nd.Frequency);

            // Estimate curvature via geodesic deviation.
            double[] baselinePhases = network.Nodes.Select(nd => nd.Phase).ToArray();
            double curvature = 0, convRate = 0;

            // Create 2 small perturbation trajectories.
            var trajs = new List<List<(double r, double f, double pv, double e, double mem)>>();
            foreach (double mag in new[] { 0.3, 0.6 })
            {
                for (int i = 0; i < n; i++) network.Nodes[i].Phase = baselinePhases[i];
                var sim2 = new MemoryTemporalSimulation(network, beta);
                foreach (var node in network.Nodes) node.Phase += (rng.NextDouble() * 2 - 1) * mag;
                sim2.Run(50);

                var traj = new List<(double, double, double, double, double)>();
                for (int iter = 0; iter <= 300; iter += 30)
                {
                    if (iter > 0) sim2.Run(30);
                    var f = SynchronizationMetrics.FromNetwork(network, 0);
                    traj.Add((f.OrderParameterR, network.Nodes.Average(nd => nd.Frequency),
                        f.PhaseVariance, f.OrderParameterR * network.Nodes.Average(nd => nd.Frequency),
                        Mem(network)));
                }
                trajs.Add(traj);
            }

            // Compute geodesic deviation.
            if (trajs.Count >= 2)
            {
                var ta = trajs[0]; var tb = trajs[1];
                int pts = Math.Min(ta.Count, tb.Count);
                double curvSum = 0; int curvCount = 0;
                for (int t = 0; t < pts - 2; t++)
                {
                    double d0 = Math.Sqrt(Math.Pow(ta[t].r - tb[t].r, 2) + Math.Pow(ta[t].f - tb[t].f, 2) +
                                           Math.Pow(ta[t].pv - tb[t].pv, 2));
                    double d1 = Math.Sqrt(Math.Pow(ta[t + 1].r - tb[t + 1].r, 2) + Math.Pow(ta[t + 1].f - tb[t + 1].f, 2) +
                                           Math.Pow(ta[t + 1].pv - tb[t + 1].pv, 2));
                    double d2 = Math.Sqrt(Math.Pow(ta[t + 2].r - tb[t + 2].r, 2) + Math.Pow(ta[t + 2].f - tb[t + 2].f, 2) +
                                           Math.Pow(ta[t + 2].pv - tb[t + 2].pv, 2));
                    if (d0 < 1e-10) continue;
                    curvSum += Math.Abs(d2 - 2 * d1 + d0) / d0;
                    curvCount++;
                }
                curvature = curvCount > 0 ? curvSum / curvCount : 0;

                double crSum = 0; int crCount = 0;
                for (int t = 0; t < pts - 1; t++)
                {
                    double d0 = Math.Sqrt(Math.Pow(ta[t].r - tb[t].r, 2) + Math.Pow(ta[t].f - tb[t].f, 2) +
                                           Math.Pow(ta[t].pv - tb[t].pv, 2));
                    double d1 = Math.Sqrt(Math.Pow(ta[t + 1].r - tb[t + 1].r, 2) + Math.Pow(ta[t + 1].f - tb[t + 1].f, 2) +
                                           Math.Pow(ta[t + 1].pv - tb[t + 1].pv, 2));
                    if (d0 < 1e-10) continue;
                    crSum += -(d1 - d0) / d0; crCount++;
                }
                convRate = crCount > 0 ? crSum / crCount : 0;
            }

            // Restore and continue.
            for (int i = 0; i < n; i++) network.Nodes[i].Phase = baselinePhases[i];

            history.Add(new CycleMeasurement(cycle, memScore, curvature, convRate, r, energy));

            if (cycle >= totalCycles) break;

            // Apply cycle perturbation.
            foreach (var node in network.Nodes)
                node.Phase += (rng.NextDouble() * 2 - 1) * 1.5;
            sim.Run(cycleIters);
        }

        // Compute growth rates.
        double memGrowth = 0, curvGrowth = 0;
        int validPairs = 0;
        for (int i = 1; i < history.Count; i++)
        {
            double dc = history[i].Cycle - history[i - 1].Cycle;
            if (dc > 0)
            {
                memGrowth += (history[i].MemoryScore - history[i - 1].MemoryScore) / dc;
                curvGrowth += (history[i].Curvature - history[i - 1].Curvature) / dc;
                validPairs++;
            }
        }
        double memRate = validPairs > 0 ? memGrowth / validPairs : 0;
        double curvRate = validPairs > 0 ? curvGrowth / validPairs : 0;

        // Feedback coefficient: correlation between memory and curvature evolutions.
        double memVals = 0, curvVals = 0, cov = 0, vMem = 0, vCurv = 0;
        for (int i = 1; i < history.Count; i++)
        {
            double dm = history[i].MemoryScore - history[i - 1].MemoryScore;
            double dc = history[i].Curvature - history[i - 1].Curvature;
            memVals += dm; curvVals += dc;
        }
        double meanDm = memVals / Math.Max(history.Count - 1, 1);
        double meanDc = curvVals / Math.Max(history.Count - 1, 1);
        for (int i = 1; i < history.Count; i++)
        {
            double dm = history[i].MemoryScore - history[i - 1].MemoryScore;
            double dc = history[i].Curvature - history[i - 1].Curvature;
            cov += (dm - meanDm) * (dc - meanDc);
            vMem += (dm - meanDm) * (dm - meanDm);
            vCurv += (dc - meanDc) * (dc - meanDc);
        }
        double feedbackCoeff = Math.Sqrt(Math.Max(vMem, 1e-15) * Math.Max(vCurv, 1e-15)) > 1e-10
            ? cov / Math.Sqrt(Math.Max(vMem, 1e-15) * Math.Max(vCurv, 1e-15)) : 0;

        // Saturation detection: does curvature growth flatten?
        bool saturated = false;
        int satCycle = 0;
        for (int i = history.Count - 1; i >= 3; i--)
        {
            double earlyCurv = history.Take(i / 2).Average(h => h.Curvature);
            double lateCurv = history.Skip(i / 2).Take(i - i / 2).Average(h => h.Curvature);
            if (Math.Abs(lateCurv - earlyCurv) < 0.01)
            { saturated = true; satCycle = i; break; }
        }

        return new FeedbackProfile(beta, history, memRate, curvRate, feedbackCoeff, saturated, satCycle);
    }

    public static FeedbackReport AnalyzeFeedback(List<FeedbackProfile> profiles)
    {
        double meanFC = profiles.Average(p => p.FeedbackCoefficient);

        var betas = profiles.Select(p => p.Beta).ToList();
        var fcs = profiles.Select(p => p.FeedbackCoefficient).ToList();
        double betaFC = Correlation(betas, fcs);

        string fcClass = meanFC > 0.7 ? "D: Self-Organizing Feedback Loop" :
                         meanFC > 0.4 ? "C: Positive Feedback" :
                         meanFC > 0.2 ? "B: Weak Feedback" :
                         "A: No Feedback";

        string desc = meanFC > 0.5
            ? $"Strong feedback: memory and curvature co-evolve (r={meanFC:F3})"
            : $"Weak/no feedback: memory and curvature evolve independently (r={meanFC:F3})";

        return new FeedbackReport(profiles, meanFC, betaFC, fcClass, desc);
    }

    private static double Correlation(List<double> x, List<double> y)
    {
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0, vy = 0;
        for (int i = 0; i < x.Count; i++)
        { cov += (x[i] - mx) * (y[i] - my); vx += (x[i] - mx) * (x[i] - mx); vy += (y[i] - my) * (y[i] - my); }
        return cov / Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
    }
}
