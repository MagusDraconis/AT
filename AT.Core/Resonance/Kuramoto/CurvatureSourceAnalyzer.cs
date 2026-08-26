using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Systematically varies state variables (energy, memory, identity)
/// and measures their contribution to resonance state-space curvature.
/// </summary>
public static class CurvatureSourceAnalyzer
{
    public enum ScanVariable { EnergyScale, MemoryBeta, IdentityHistory }

    public sealed record ScanPoint(
        ScanVariable Variable,
        double VariableValue,      // energy scale or beta
        string History,
        double MeanCurvature,
        double ConvergentFraction,
        double MeanConvergenceRate,
        double BaselineR,
        double BaselineEnergy,
        double BaselineMem);

    public sealed record SourceAttribution(
        List<ScanPoint> AllPoints,
        // Per-variable correlation
        double EnergyCurvatureCorrelation,
        double MemoryCurvatureCorrelation,
        double IdentityCurvatureVariance,
        // Multi-factor
        string DominantSource,
        string Classification);

    // ── Helpers ──────────────────────────────────────────────────────

    private static double[] Vec(double r, double freq, double pvar, double energy, double mem, double lc)
        => new[] { r, freq, pvar, energy, mem, lc };

    private static double Dist(double[] a, double[] b)
    {
        double s = 0; for (int d = 0; d < a.Length; d++)
        { double dd = a[d] - b[d]; s += dd * dd; }
        return Math.Sqrt(s);
    }

    private static (double r, double freq, double pvar) MeasureFP(TemporalNetwork net)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        return (m.OrderParameterR, net.Nodes.Average(n => n.Frequency), m.PhaseVariance);
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

    // ── Single scan point ────────────────────────────────────────────

    public static ScanPoint MeasureScanPoint(
        ScanVariable variable, double varValue, string history,
        double k, double lambda, int n, int seed,
        double[] perturbationMagnitudes = null)
    {
        perturbationMagnitudes ??= new[] { 0.5, 1.0, 1.5, 2.0 };
        double beta = variable == ScanVariable.MemoryBeta ? varValue : 0.5;
        double energyScale = variable == ScanVariable.EnergyScale ? varValue : 1.0;
        string hist = variable == ScanVariable.IdentityHistory ? history : history;

        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        for (int i = 0; i < n; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });
        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);
        ApplyHistory(network, hist, rng, sim);
        foreach (var node in network.Nodes) node.Frequency *= energyScale;
        sim.Run(300);

        // Baseline measurement.
        var fp = MeasureFP(network);
        double blMem = Mem(network);
        double blEnergy = fp.r * fp.freq;
        double[] baselinePhases = network.Nodes.Select(nd => nd.Phase).ToArray();
        double[] baselineFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();

        // Generate trajectory bundle.
        var trajs = new List<List<(double r, double f, double pv, double e, double m, double lc)>>();
        foreach (double mag in perturbationMagnitudes)
        {
            for (int i = 0; i < n; i++)
            { network.Nodes[i].Phase = baselinePhases[i]; network.Nodes[i].Frequency = baselineFreqs[i]; }
            sim = new MemoryTemporalSimulation(network, beta);
            foreach (var node in network.Nodes) node.Phase += (rng.NextDouble() * 2 - 1) * mag;
            sim.Run(100);

            var traj = new List<(double, double, double, double, double, double)>();
            for (int iter = 0; iter <= 1500; iter += 50)
            {
                if (iter > 0) sim.Run(50);
                var f = MeasureFP(network);
                double m = Mem(network);
                var df = new LocalDensityField(20); df.Compute(network, 1);
                traj.Add((f.r, f.freq, f.pvar, f.r * f.freq, m, df.MaxLocalR()));
            }
            trajs.Add(traj);
        }

        // Compute geodesic deviation.
        double curvSum = 0; int curvCount = 0;
        int convergent = 0;
        double convRateSum = 0; int convRateCount = 0;

        for (int i = 0; i < trajs.Count; i++)
        {
            for (int j = i + 1; j < trajs.Count; j++)
            {
                var ta = trajs[i]; var tb = trajs[j];
                int pts = Math.Min(ta.Count, tb.Count);

                double d0 = Dist(Vec(ta[0].r, ta[0].f, ta[0].pv, ta[0].e, ta[0].m, ta[0].lc),
                                 Vec(tb[0].r, tb[0].f, tb[0].pv, tb[0].e, tb[0].m, tb[0].lc));
                double dEnd = Dist(Vec(ta[pts - 1].r, ta[pts - 1].f, ta[pts - 1].pv, ta[pts - 1].e, ta[pts - 1].m, ta[pts - 1].lc),
                                   Vec(tb[pts - 1].r, tb[pts - 1].f, tb[pts - 1].pv, tb[pts - 1].e, tb[pts - 1].m, tb[pts - 1].lc));
                if (dEnd < d0) convergent++;

                for (int t = 0; t < pts - 2; t++)
                {
                    double dt0 = Dist(Vec(ta[t].r, ta[t].f, ta[t].pv, ta[t].e, ta[t].m, ta[t].lc),
                                      Vec(tb[t].r, tb[t].f, tb[t].pv, tb[t].e, tb[t].m, tb[t].lc));
                    double dt1 = Dist(Vec(ta[t + 1].r, ta[t + 1].f, ta[t + 1].pv, ta[t + 1].e, ta[t + 1].m, ta[t + 1].lc),
                                      Vec(tb[t + 1].r, tb[t + 1].f, tb[t + 1].pv, tb[t + 1].e, tb[t + 1].m, tb[t + 1].lc));
                    double dt2 = Dist(Vec(ta[t + 2].r, ta[t + 2].f, ta[t + 2].pv, ta[t + 2].e, ta[t + 2].m, ta[t + 2].lc),
                                      Vec(tb[t + 2].r, tb[t + 2].f, tb[t + 2].pv, tb[t + 2].e, tb[t + 2].m, tb[t + 2].lc));
                    if (dt0 < 1e-10) continue;
                    double d2 = dt2 - 2 * dt1 + dt0;
                    curvSum += Math.Abs(d2) / dt0;
                    curvCount++;
                }
                for (int t = 0; t < pts - 1; t++)
                {
                    double d1 = Dist(Vec(ta[t].r, ta[t].f, ta[t].pv, ta[t].e, ta[t].m, ta[t].lc),
                                      Vec(tb[t].r, tb[t].f, tb[t].pv, tb[t].e, tb[t].m, tb[t].lc));
                    double d2 = Dist(Vec(ta[t + 1].r, ta[t + 1].f, ta[t + 1].pv, ta[t + 1].e, ta[t + 1].m, ta[t + 1].lc),
                                      Vec(tb[t + 1].r, tb[t + 1].f, tb[t + 1].pv, tb[t + 1].e, tb[t + 1].m, tb[t + 1].lc));
                    if (d1 < 1e-10) continue;
                    convRateSum += -(d2 - d1) / d1; convRateCount++;
                }
            }
        }

        double meanCurv = curvCount > 0 ? curvSum / curvCount : 0;
        double totalPairs = perturbationMagnitudes.Length * (perturbationMagnitudes.Length - 1) / 2;
        double convFrac = totalPairs > 0 ? convergent / totalPairs : 0;
        double meanCR = convRateCount > 0 ? convRateSum / convRateCount : 0;

        return new ScanPoint(variable, varValue, hist, Math.Abs(meanCurv), convFrac, meanCR,
            fp.r, blEnergy, blMem);
    }

    // ── Source attribution ───────────────────────────────────────────

    public static SourceAttribution AnalyzeSources(List<ScanPoint> points)
    {
        // Correlation: curvature vs variable value for Energy and Memory scans.
        double Pearson(List<double> x, List<double> y)
        {
            double mx = x.Average(), my = y.Average();
            double cov = 0, vx = 0, vy = 0;
            for (int i = 0; i < x.Count; i++)
            { cov += (x[i] - mx) * (y[i] - my); vx += (x[i] - mx) * (x[i] - mx); vy += (y[i] - my) * (y[i] - my); }
            return cov / Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
        }

        var energyPts = points.Where(p => p.Variable == ScanVariable.EnergyScale).ToList();
        var memoryPts = points.Where(p => p.Variable == ScanVariable.MemoryBeta).ToList();
        var identityPts = points.Where(p => p.Variable == ScanVariable.IdentityHistory).ToList();

        double eCorr = energyPts.Count > 1
            ? Pearson(energyPts.Select(p => p.VariableValue).ToList(),
                      energyPts.Select(p => p.MeanCurvature).ToList()) : 0;
        double mCorr = memoryPts.Count > 1
            ? Pearson(memoryPts.Select(p => p.VariableValue).ToList(),
                      memoryPts.Select(p => p.MeanCurvature).ToList()) : 0;

        // Identity variance: std of mean curvature across histories.
        double idMean = identityPts.Any() ? identityPts.Average(p => p.MeanCurvature) : 0;
        double idVar = identityPts.Count > 1
            ? Math.Sqrt(identityPts.Average(p => (p.MeanCurvature - idMean) * (p.MeanCurvature - idMean)))
            : 0;

        // Also check curvature correlation with baseline state variables.
        double rCorr = points.Count > 1
            ? Pearson(points.Select(p => p.BaselineR).ToList(), points.Select(p => p.MeanCurvature).ToList()) : 0;
        double eCorr2 = points.Count > 1
            ? Pearson(points.Select(p => p.BaselineEnergy).ToList(), points.Select(p => p.MeanCurvature).ToList()) : 0;
        double memCorr = points.Count > 1
            ? Pearson(points.Select(p => p.BaselineMem).ToList(), points.Select(p => p.MeanCurvature).ToList()) : 0;

        double maxAbs = Math.Max(Math.Max(Math.Abs(eCorr), Math.Abs(mCorr)), Math.Abs(rCorr));
        string dominant = maxAbs < 0.3 ? "Multi-Factor Geometry" :
                          Math.Abs(eCorr) == maxAbs ? "Energy Dominated" :
                          Math.Abs(mCorr) == maxAbs ? "Memory Dominated" :
                          "Coherence Dominated";

        string classification = maxAbs > 0.7 ? $"E: {dominant}" :
                                maxAbs > 0.4 ? $"D: {dominant}" :
                                maxAbs > 0.2 ? $"C: {dominant}" :
                                "B: Weak source dependence";

        return new SourceAttribution(points, eCorr, mCorr, idVar, dominant, classification);
    }
}
