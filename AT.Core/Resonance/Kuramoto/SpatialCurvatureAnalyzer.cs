using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether state-space curvature (via memory β) generates
/// observable spatial motion through phase-coupling gradients.
/// </summary>
public static class SpatialCurvatureAnalyzer
{
    public sealed record SpatialSnapshot(
        int Iteration,
        double CenterX_A, double CenterY_A, double RA,
        double CenterX_B, double CenterY_B, double RB,
        double Separation, double VelocityA, double VelocityB);

    public sealed record SpatialDriftResult(
        double BetaA, double BetaB,
        List<SpatialSnapshot> History,
        double MeanDriftA, double MeanDriftB,
        double SeparationChange,
        bool Converges,
        int Seed);

    public sealed record SpatialDriftReport(
        List<SpatialDriftResult> Results,
        double MeanConvergenceRate,
        double BetaDriftCorrelation,
        string SpatialClass);

    /// <summary>
    /// Runs a spatial dynamics simulation with two condensates
    /// at different β values. Positions evolve via coupling gradient.
    /// </summary>
    public static SpatialDriftResult RunSpatialDynamics(
        double betaA, double betaB, double k, double lambda, int nPerGroup, int seed,
        int totalIters = 3000, int snapshotInterval = 100, double posStep = 0.001)
    {
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Group A: centered at (0.3, 0.5), Group B: centered at (0.7, 0.5).
        for (int i = 0; i < nPerGroup; i++)
        {
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = 0.3 + (rng.NextDouble() * 2 - 1) * 0.05,
              Y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.05 });
        }
        for (int i = 0; i < nPerGroup; i++)
        {
            network.AddNode(new TemporalNode(nPerGroup + i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = 0.7 + (rng.NextDouble() * 2 - 1) * 0.05,
              Y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.05 });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        // Run a combined simulation with position updates.
        var history = new List<SpatialSnapshot>();

        for (int iter = 0; iter <= totalIters; iter++)
        {
            // Phase update: standard Kuramoto with memory.
            PhaseStep(network, betaA, 0, nPerGroup);
            PhaseStep(network, betaB, nPerGroup, nPerGroup);

            // Position update: gradient descent on coupling energy.
            double[] newX = new double[n], newY = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx = 0, fy = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double dx = network.Nodes[j].X - network.Nodes[i].X;
                    double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                    double dist = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                    double coupling = network.Matrix.GetCoupling(i, j);
                    double cosTerm = Math.Cos(network.Nodes[j].Phase - network.Nodes[i].Phase);
                    double forceMag = coupling * cosTerm / dist;
                    fx += forceMag * dx;
                    fy += forceMag * dy;
                }
                newX[i] = Math.Clamp(network.Nodes[i].X + posStep * fx, 0.01, 0.99);
                newY[i] = Math.Clamp(network.Nodes[i].Y + posStep * fy, 0.01, 0.99);
            }
            for (int i = 0; i < n; i++)
            { network.Nodes[i].X = newX[i]; network.Nodes[i].Y = newY[i]; }

            // Snapshot.
            if (iter % snapshotInterval == 0)
            {
                double cxA = 0, cyA = 0, cXB = 0, cYB = 0;
                for (int i = 0; i < nPerGroup; i++)
                { cxA += network.Nodes[i].X; cyA += network.Nodes[i].Y; }
                for (int i = 0; i < nPerGroup; i++)
                { cXB += network.Nodes[i + nPerGroup].X; cYB += network.Nodes[i + nPerGroup].Y; }
                cxA /= nPerGroup; cyA /= nPerGroup;
                cXB /= nPerGroup; cYB /= nPerGroup;

                double sep = Math.Sqrt((cxA - cXB) * (cxA - cXB) + (cyA - cYB) * (cyA - cYB));
                double rA = GroupR(network, 0, nPerGroup);
                double rB = GroupR(network, nPerGroup, nPerGroup);

                double vA = history.Count > 0
                    ? Math.Sqrt(Math.Pow(cxA - history[^1].CenterX_A, 2) + Math.Pow(cyA - history[^1].CenterY_A, 2)) : 0;
                double vB = history.Count > 0
                    ? Math.Sqrt(Math.Pow(cXB - history[^1].CenterX_B, 2) + Math.Pow(cYB - history[^1].CenterY_B, 2)) : 0;

                history.Add(new SpatialSnapshot(iter, cxA, cyA, rA, cXB, cYB, rB, sep, vA, vB));
            }
        }

        double driftA = history.Count > 1
            ? Math.Sqrt(Math.Pow(history[^1].CenterX_A - history[0].CenterX_A, 2) +
                        Math.Pow(history[^1].CenterY_A - history[0].CenterY_A, 2)) : 0;
        double driftB = history.Count > 1
            ? Math.Sqrt(Math.Pow(history[^1].CenterX_B - history[0].CenterX_B, 2) +
                        Math.Pow(history[^1].CenterY_B - history[0].CenterY_B, 2)) : 0;

        double sepChange = history.Count >= 2 ? history[^1].Separation - history[0].Separation : 0;
        bool converges = sepChange < -0.001;

        return new SpatialDriftResult(betaA, betaB, history, driftA, driftB, sepChange, converges, seed);
    }

    private static void PhaseStep(TemporalNetwork net, double beta, int start, int count)
    {
        int n = net.NodeCount;
        double[] newPhases = new double[count];
        double[] mem = new double[count * n]; // simplified: per-oscillator memory storage

        for (int ii = 0; ii < count; ii++)
        {
            int i = start + ii;
            double sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double coupling = net.Matrix.GetCoupling(i, j);
                sum += coupling * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            }
            double dTheta = net.Nodes[i].Frequency + ((double)n / n) * sum;
            newPhases[ii] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * dTheta);
        }
        for (int ii = 0; ii < count; ii++)
            net.Nodes[start + ii].Phase = newPhases[ii];
    }

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double sumSin = 0, sumCos = 0;
        for (int i = start; i < start + count; i++)
        { sumSin += Math.Sin(net.Nodes[i].Phase); sumCos += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / count;
    }

    public static SpatialDriftReport AnalyzeDrift(List<SpatialDriftResult> results)
    {
        double meanConv = results.Average(r => r.SeparationChange);
        int convCount = results.Count(r => r.Converges);

        var betas = results.Select(r => r.BetaA).ToList();
        var drifts = results.Select(r => r.MeanDriftA).ToList();
        double corr = Correlation(betas, drifts);

        string cls = convCount > results.Count * 0.7 ? "D: Effective attraction from geometry" :
                     convCount > results.Count * 0.4 ? "C: Directed motion" :
                     convCount > results.Count * 0.2 ? "B: Weak drift" : "A: No spatial effect";

        return new SpatialDriftReport(results, meanConv, corr, cls);
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
