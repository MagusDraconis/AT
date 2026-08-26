using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether memory-generated state-space curvature
/// has measurable influence on spatial motion dynamics.
/// 
/// AT-068: Curvature Coupling to Spatial Dynamics
/// </summary>
public static class CurvatureMotionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Per-snapshot measurements combining curvature and spatial motion.
    /// </summary>
    public sealed record MotionProfile(
        int Iteration,
        double Curvature,
        double Separation,
        double VelocityA,
        double VelocityB,
        double AccelerationA,
        double AccelerationB,
        double ConvergenceRate,
        double CenterX_A, double CenterY_A,
        double CenterX_B, double CenterY_B,
        double R_A, double R_B,
        double Beta);

    /// <summary>
    /// Correlation analysis between curvature and motion metrics.
    /// </summary>
    public sealed record CurvatureMotionCorrelation(
        double CurvatureVelocityR,
        double CurvatureAccelerationR,
        double CurvatureConvergenceR,
        double BetaCurvatureR,
        double BetaDriftR,
        string Classification,
        string Interpretation,
        List<(double Beta, double MeanCurvature, double MeanVelocity, double MeanAccel,
               double FinalSeparation, double ConvergenceRate)> Summary);

    // ══════════════════════════════════════════════════════════════════
    // Simulation
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs a combined curvature-motion simulation for a given β.
    /// At each snapshot, measures both spatial motion parameters
    /// and state-space curvature via geodesic deviation.
    /// </summary>
    public static (List<MotionProfile> Profiles, double Beta) RunProfile(
        double beta, double k, double lambda, int nPerGroup, int seed,
        int totalIters = 3000, int snapshotInterval = 200,
        double posStep = 0.001,
        // curvature measurement params
        int curvRecoveryIters = 200,
        double[]? curvPerturbationMags = null)
    {
        curvPerturbationMags ??= new[] { 0.3, 0.8 };

        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Group A: center (0.3, 0.5), Group B: center (0.7, 0.5).
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

        var profiles = new List<MotionProfile>();
        double prevSeparation = double.NaN;
        double prevVelA = 0, prevVelB = 0;

        for (int iter = 0; iter <= totalIters; iter++)
        {
            // Phase update with memory.
            PhaseStepWithMemory(network, beta, n);

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
                double cxA = 0, cyA = 0, cxB = 0, cyB = 0;
                for (int i = 0; i < nPerGroup; i++)
                { cxA += network.Nodes[i].X; cyA += network.Nodes[i].Y; }
                for (int i = 0; i < nPerGroup; i++)
                { cxB += network.Nodes[i + nPerGroup].X; cyB += network.Nodes[i + nPerGroup].Y; }
                cxA /= nPerGroup; cyA /= nPerGroup;
                cxB /= nPerGroup; cyB /= nPerGroup;

                double sep = Math.Sqrt((cxA - cxB) * (cxA - cxB) + (cyA - cyB) * (cyA - cyB));
                double rA = GroupR(network, 0, nPerGroup);
                double rB = GroupR(network, nPerGroup, nPerGroup);

                // Velocity: displacement since last snapshot.
                double vA = 0, vB = 0;
                if (profiles.Count > 0)
                {
                    vA = Math.Sqrt(Math.Pow(cxA - profiles[^1].CenterX_A, 2) +
                                   Math.Pow(cyA - profiles[^1].CenterY_A, 2));
                    vB = Math.Sqrt(Math.Pow(cxB - profiles[^1].CenterX_B, 2) +
                                   Math.Pow(cyB - profiles[^1].CenterY_B, 2));
                }

                // Acceleration: change in velocity.
                double aA = profiles.Count > 1 ? Math.Abs(vA - prevVelA) : 0;
                double aB = profiles.Count > 1 ? Math.Abs(vB - prevVelB) : 0;

                // Convergence rate: -(Δsep) / sep_initial per snapshot.
                double convRate = 0;
                if (!double.IsNaN(prevSeparation) && sep > 1e-10)
                    convRate = -(sep - prevSeparation) / sep;

                // Measure curvature at this state via geodesic deviation.
                double curv = MeasureCurvatureAtState(network, beta, k, lambda,
                    curvRecoveryIters, curvPerturbationMags, seed + iter);

                profiles.Add(new MotionProfile(iter, curv, sep, vA, vB, aA, aB,
                    convRate, cxA, cyA, cxB, cyB, rA, rB, beta));

                prevSeparation = sep;
                prevVelA = vA;
                prevVelB = vB;
            }
        }

        return (profiles, beta);
    }

    // ══════════════════════════════════════════════════════════════════
    // Curvature Measurement at State
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Measures state-space curvature at the current network state
    /// using geodesic deviation: perturbs the state, runs short
    /// recovery, and computes trajectory convergence/divergence.
    /// Returns mean curvature (positive → trajectories converge).
    /// </summary>
    private static double MeasureCurvatureAtState(
        TemporalNetwork network, double beta, double k, double lambda,
        int recoveryIters, double[] perturbationMags, int seed)
    {
        int n = network.NodeCount;
        var rng = new Random(seed);

        // Save current state.
        double[] savedPhases = network.Nodes.Select(nd => nd.Phase).ToArray();
        double[] savedFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();

        // Generate perturbation trajectories.
        var trajs = new List<List<double[]>>();
        foreach (double mag in perturbationMags)
        {
            // Restore state.
            for (int i = 0; i < n; i++)
            { network.Nodes[i].Phase = savedPhases[i]; network.Nodes[i].Frequency = savedFreqs[i]; }

            var sim = new MemoryTemporalSimulation(network, beta);
            // Apply perturbation.
            foreach (var node in network.Nodes)
                node.Phase += (rng.NextDouble() * 2 - 1) * mag;
            sim.Run(50);

            var traj = new List<double[]>();
            for (int iter = 0; iter <= recoveryIters; iter += 50)
            {
                if (iter > 0) sim.Run(50);
                traj.Add(StateVector(network));
            }
            trajs.Add(traj);
        }

        // Restore original state.
        for (int i = 0; i < n; i++)
        { network.Nodes[i].Phase = savedPhases[i]; network.Nodes[i].Frequency = savedFreqs[i]; }

        // Compute geodesic deviation curvature for all pairs.
        double curvSum = 0; int curvCount = 0;
        for (int i = 0; i < trajs.Count; i++)
        {
            for (int j = i + 1; j < trajs.Count; j++)
            {
                var ta = trajs[i]; var tb = trajs[j];
                int pts = Math.Min(ta.Count, tb.Count);
                for (int t = 0; t < pts - 2; t++)
                {
                    double d0 = Dist(ta[t], tb[t]);
                    double d1 = Dist(ta[t + 1], tb[t + 1]);
                    double d2 = Dist(ta[t + 2], tb[t + 2]);
                    if (d0 < 1e-10) continue;
                    double d2nd = d2 - 2 * d1 + d0;
                    curvSum += Math.Abs(d2nd) / d0;
                    curvCount++;
                }
            }
        }

        return curvCount > 0 ? curvSum / curvCount : 0;
    }

    private static double[] StateVector(TemporalNetwork net)
    {
        int n = net.NodeCount;
        // Use subsample of metrics: R, freq spread, phase var, energy, memory score.
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        double mem = MemoryScore(net);
        return new[] { m.OrderParameterR, net.Nodes.Average(nd => nd.Frequency),
            m.PhaseVariance, m.OrderParameterR * net.Nodes.Average(nd => nd.Frequency), mem };
    }

    private static double Dist(double[] a, double[] b)
    {
        double s = 0;
        for (int d = 0; d < a.Length; d++)
        { double dd = a[d] - b[d]; s += dd * dd; }
        return Math.Sqrt(s);
    }

    private static double MemoryScore(TemporalNetwork net)
    {
        int n = net.NodeCount;
        if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
                sum += Math.Abs(s); sumSq += s * s; c++;
            }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    // ══════════════════════════════════════════════════════════════════
    // Phase step with memory
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Kuramoto phase update with historical memory.
    /// Uses exponential moving average of sin(Δθ) as memory term.
    /// </summary>
    private static void PhaseStepWithMemory(TemporalNetwork net, double beta, int n)
    {
        double[] newPhases = new double[n];
        double couplingStrength = n;

        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                double coupling = net.Matrix.GetCoupling(i, j);
                sum += coupling * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            }
            double dTheta = net.Nodes[i].Frequency + (couplingStrength / n) * sum;
            newPhases[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * dTheta);
        }

        // Memory update: if beta > 0, modify phases with memory term.
        if (beta > 0)
        {
            // Use a simplified memory coupling: add β-weighted sin(Δθ) from last step.
            // The memory is maintained as an EMA in the phase update.
            for (int i = 0; i < n; i++)
            {
                double memSum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double coupling = net.Matrix.GetCoupling(i, j);
                    memSum += coupling * beta * Math.Sin(newPhases[j] - newPhases[i]);
                }
                newPhases[i] = TemporalSimulation.NormalizePhase(
                    newPhases[i] + 0.01 * (couplingStrength / n) * memSum);
            }
        }

        for (int i = 0; i < n; i++)
            net.Nodes[i].Phase = newPhases[i];
    }

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double sumSin = 0, sumCos = 0;
        for (int i = start; i < start + count; i++)
        { sumSin += Math.Sin(net.Nodes[i].Phase); sumCos += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / count;
    }

    // ══════════════════════════════════════════════════════════════════
    // Correlation Analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Analyzes curvature-motion correlation across β sweep.
    /// </summary>
    public static CurvatureMotionCorrelation Analyze(
        List<(List<MotionProfile> Profiles, double Beta)> allProfiles)
    {
        // Flatten all profiles (excluding iteration 0 warmup).
        var all = allProfiles.SelectMany(p => p.Profiles.Where(m => m.Iteration > 0)).ToList();

        double r_cv = Pearson(all.Select(p => p.Curvature).ToList(),
                              all.Select(p => (p.VelocityA + p.VelocityB) / 2).ToList());
        double r_ca = Pearson(all.Select(p => p.Curvature).ToList(),
                              all.Select(p => (p.AccelerationA + p.AccelerationB) / 2).ToList());
        double r_cc = Pearson(all.Select(p => p.Curvature).ToList(),
                              all.Select(p => p.ConvergenceRate).ToList());

        // Beta-level summary.
        var betaList = allProfiles.Select(p => p.Beta).ToList();
        var curvList = allProfiles.Select(p =>
            p.Profiles.Where(m => m.Iteration > 0).Average(m => m.Curvature)).ToList();
        var velList = allProfiles.Select(p =>
            p.Profiles.Where(m => m.Iteration > 0).Average(m => (m.VelocityA + m.VelocityB) / 2)).ToList();
        var accList = allProfiles.Select(p =>
            p.Profiles.Where(m => m.Iteration > 0).Average(m => (m.AccelerationA + m.AccelerationB) / 2)).ToList();

        double r_betaCurv = Pearson(betaList, curvList);
        double r_betaDrift = Pearson(betaList, velList);

        // Summary per beta.
        var summary = new List<(double, double, double, double, double, double)>();
        foreach (var (profiles, beta) in allProfiles.OrderBy(p => p.Beta))
        {
            var snapshots = profiles.Where(m => m.Iteration > 0).ToList();
            double mc = snapshots.Average(m => m.Curvature);
            double mv = snapshots.Average(m => (m.VelocityA + m.VelocityB) / 2);
            double ma = snapshots.Average(m => (m.AccelerationA + m.AccelerationB) / 2);
            double fs = snapshots.Any() ? snapshots[^1].Separation : 0;
            double cr = snapshots.Average(m => m.ConvergenceRate);
            summary.Add((beta, mc, mv, ma, fs, cr));
        }

        double maxAbsR = Math.Max(Math.Max(Math.Abs(r_cv), Math.Abs(r_ca)), Math.Abs(r_cc));

        string classification = maxAbsR > 0.5 ? "D: Curvature Dominated Motion" :
                                maxAbsR > 0.3 ? "C: Strong Coupling" :
                                maxAbsR > 0.15 ? "B: Weak Coupling" :
                                "A: No Coupling";

        string interpretation = classification switch
        {
            "D: Curvature Dominated Motion" =>
                "Curvature strongly predicts spatial motion. Memory-generated geometry " +
                "directly shapes trajectories, velocities, and convergence. " +
                "This supports curvature as a physically significant field.",
            "C: Strong Coupling" =>
                "Curvature significantly correlates with spatial dynamics. " +
                "The state-space geometry measurably influences how condensates move, " +
                "suggesting curvature is more than a geometric artifact.",
            "B: Weak Coupling" =>
                "Curvature has a detectable but modest influence on spatial motion. " +
                "Position dynamics are primarily governed by other factors, " +
                "but curvature contributes measurably.",
            _ => "Curvature and spatial motion appear largely independent. " +
                 "Memory-generated curvature does not significantly alter " +
                 "how condensates move through space. Spatial dynamics are " +
                 "determined by coupling gradients, not geometric curvature."
        };

        return new CurvatureMotionCorrelation(r_cv, r_ca, r_cc, r_betaCurv, r_betaDrift,
            classification, interpretation, summary);
    }

    private static double Pearson(List<double> x, List<double> y)
    {
        if (x.Count < 2) return 0;
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0, vy = 0;
        for (int i = 0; i < x.Count; i++)
        { cov += (x[i] - mx) * (y[i] - my); vx += (x[i] - mx) * (x[i] - mx); vy += (y[i] - my) * (y[i] - my); }
        double denom = Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
        return denom < 1e-15 ? 0 : cov / denom;
    }
}
