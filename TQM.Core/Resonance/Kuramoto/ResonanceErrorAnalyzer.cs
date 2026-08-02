using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether effective attraction emerges from resonance error
/// reduction rather than explicit forces. Compares error dynamics
/// with and without spatial position updates.
/// </summary>
public static class ResonanceErrorAnalyzer
{
    public sealed record ErrorSnapshot(
        int Iteration,
        double E1, double E2, double E3, double E4, double E5, double E6, double E7,
        double Separation, double RA, double RB);

    public sealed record ErrorProfile(
        string Mode, double SeparationLambda, string HistoryA, string HistoryB,
        List<ErrorSnapshot> History,
        double InitialError, double FinalError, double ErrorReductionRate,
        double MeanSeparation, double SeparationChange);

    public sealed record ErrorReport(
        List<ErrorProfile> Profiles,
        double FixedMeanReduction, double MovingMeanReduction,
        double ErrorMotionCorrelation,
        string Classification);

    // ── 7 Error metrics ──────────────────────────────────────────────

    private static double[] ComputeErrors(TemporalNetwork net, int nA, int nB)
    {
        int n = net.NodeCount;
        // Group A: 0..nA-1, Group B: nA..nA+nB-1.

        // E1: 1 - global R.
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        double e1 = 1.0 - m.OrderParameterR;

        // E2: local coherence deficit.
        var df = new LocalDensityField(20); df.Compute(net, 1);
        double e2 = 1.0 - df.MaxLocalR();

        // E3: mean |sin(Δθ)| between groups.
        double sum3 = 0; int c3 = 0;
        for (int i = 0; i < nA; i++)
            for (int j = nA; j < nA + nB; j++)
            { sum3 += Math.Abs(Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase)); c3++; }
        double e3 = c3 > 0 ? sum3 / c3 : 0;

        // E4: coupling-weighted tension.
        double sum4 = 0, tw = 0;
        for (int i = 0; i < nA; i++)
            for (int j = nA; j < nA + nB; j++)
            {
                double w = net.Matrix.GetCoupling(i, j);
                sum4 += w * Math.Abs(Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase));
                tw += w;
            }
        double e4 = tw > 0 ? sum4 / tw : 0;

        // E5: identity mismatch (fingerprint distance between groups).
        double sA = 0, cA = 0, sB = 0, cB = 0, fA = 0, fB = 0;
        for (int i = 0; i < nA; i++)
        { sA += Math.Sin(net.Nodes[i].Phase); cA += Math.Cos(net.Nodes[i].Phase); fA += net.Nodes[i].Frequency; }
        for (int i = nA; i < nA + nB; i++)
        { sB += Math.Sin(net.Nodes[i].Phase); cB += Math.Cos(net.Nodes[i].Phase); fB += net.Nodes[i].Frequency; }
        double rA = Math.Sqrt(sA * sA + cA * cA) / nA;
        double rB = Math.Sqrt(sB * sB + cB * cB) / nB;
        fA /= nA; fB /= nB;
        double dr = rA - rB, dFreq = (fA - fB) / 3.0;
        double e5 = Math.Sqrt(dr * dr + dFreq * dFreq);

        // E6: frequency mismatch.
        double e6 = Math.Abs(fA - fB);

        // E7: composite.
        double e7 = (e1 + e2 + e3 + e4 + e5 + e6) / 6.0;

        return new[] { e1, e2, e3, e4, e5, e6, e7 };
    }

    // ── Run ──────────────────────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork nw, int start, int count, string h, Random rng,
        MemoryTemporalSimulation sim)
    {
        foreach (char p in h)
        {
            double shift = p == 'A' ? 0.4 : p == 'B' ? -0.4 : (rng.NextDouble() * 2 - 1) * 0.4;
            for (int i = start; i < start + count; i++) nw.Nodes[i].Phase += shift;
            sim.Run(200);
        }
    }

    public static ErrorProfile RunErrorEvolution(
        string mode, double sepLambda, string histA, string histB,
        double beta, double k, double lambda, int nPerGroup, int seed,
        int totalIters = 2000, int interval = 100)
    {
        bool movePositions = mode == "moving";
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        double sep = sepLambda * lambda;
        double ax = 0.35, ay = 0.5;
        double bx = 0.35 + sep, by = 0.5;

        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = Math.Clamp(ax + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(ay + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });
        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(nPerGroup + i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = Math.Clamp(bx + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(by + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);
        sim.Run(1500);
        ApplyHistory(network, 0, nPerGroup, histA, rng, sim);
        ApplyHistory(network, nPerGroup, nPerGroup, histB, rng, sim);

        var history = new List<ErrorSnapshot>();

        for (int iter = 0; iter <= totalIters; iter++)
        {
            // Phase step.
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                double dTheta = network.Nodes[i].Frequency + ((double)n / n) * sum;
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * dTheta);
            }

            // Optional position update.
            if (movePositions)
            {
                double[] nx = new double[n], ny = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double fx = 0, fy = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;
                        double dx = network.Nodes[j].X - network.Nodes[i].X;
                        double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                        double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                        double w = network.Matrix.GetCoupling(i, j);
                        fx += w * Math.Cos(network.Nodes[j].Phase - network.Nodes[i].Phase) * dx / d;
                        fy += w * Math.Cos(network.Nodes[j].Phase - network.Nodes[i].Phase) * dy / d;
                    }
                    nx[i] = Math.Clamp(network.Nodes[i].X + 0.001 * fx, 0.01, 0.99);
                    ny[i] = Math.Clamp(network.Nodes[i].Y + 0.001 * fy, 0.01, 0.99);
                }
                for (int i = 0; i < n; i++)
                { network.Nodes[i].X = nx[i]; network.Nodes[i].Y = ny[i]; }
            }

            if (iter % interval == 0)
            {
                var errors = ComputeErrors(network, nPerGroup, nPerGroup);
                double cxA = 0, cyA = 0, cXB = 0, cYB = 0;
                for (int i = 0; i < nPerGroup; i++) { cxA += network.Nodes[i].X; cyA += network.Nodes[i].Y; }
                for (int i = 0; i < nPerGroup; i++) { cXB += network.Nodes[i + nPerGroup].X; cYB += network.Nodes[i + nPerGroup].Y; }
                double currentSep = Math.Sqrt(Math.Pow(cxA / nPerGroup - cXB / nPerGroup, 2) +
                                              Math.Pow(cyA / nPerGroup - cYB / nPerGroup, 2));
                double rA = GroupR(network, 0, nPerGroup);
                double rB = GroupR(network, nPerGroup, nPerGroup);

                history.Add(new ErrorSnapshot(iter, errors[0], errors[1], errors[2],
                    errors[3], errors[4], errors[5], errors[6], currentSep, rA, rB));
            }
        }

        double initErr = history[0].E7;
        double finalErr = history[^1].E7;
        double errRate = history.Count > 1 ? (initErr - finalErr) / (history.Count - 1) : 0;
        double meanSep = history.Average(h => h.Separation);
        double sepChange = history[^1].Separation - history[0].Separation;

        return new ErrorProfile(mode, sepLambda, histA, histB, history,
            initErr, finalErr, errRate, meanSep, sepChange);
    }

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double ss = 0, sc = 0;
        for (int i = start; i < start + count; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / count;
    }

    // ── Aggregate ────────────────────────────────────────────────────

    public static ErrorReport AnalyzeErrors(List<ErrorProfile> profiles)
    {
        var fixedP = profiles.Where(p => p.Mode == "fixed").ToList();
        var movingP = profiles.Where(p => p.Mode == "moving").ToList();

        double fixedReduction = fixedP.Average(p => p.ErrorReductionRate);
        double movingReduction = movingP.Average(p => p.ErrorReductionRate);

        // Correlation: error reduction vs separation change.
        var errRates = profiles.Select(p => p.ErrorReductionRate).ToList();
        var sepChanges = profiles.Select(p => p.SeparationChange).ToList();
        double corr = Correlation(errRates, sepChanges);

        string cls = Math.Abs(corr) > 0.7 ? "D: Unified Resonance Error Dynamics" :
                     Math.Abs(corr) > 0.4 ? "C: Error Reduction Driven" :
                     Math.Abs(corr) > 0.2 ? "B: Synchronization Driven" : "A: Force Driven";

        return new ErrorReport(profiles, fixedReduction, movingReduction, corr, cls);
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
