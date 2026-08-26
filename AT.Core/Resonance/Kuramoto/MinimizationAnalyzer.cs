using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Searches for a minimization principle in AT dynamics.
/// Tracks candidate scalar potentials over time and measures
/// whether they decrease monotonically across different scenarios.
/// </summary>
public static class MinimizationAnalyzer
{
    // ── Snapshot ─────────────────────────────────────────────────────

    public readonly record struct PotentialSnapshot(
        int Iteration,
        double R, double MeanFreq, double PhaseVar,
        double LocalCoh, double MemScore,
        double[] Potentials  // P1..P10
    );

    // ── Evolution trace ──────────────────────────────────────────────

    public sealed record EvolutionTrace(
        string History,
        string Phase,          // "formation", "energy_inject", "recovery"
        List<PotentialSnapshot> Snapshots,
        int Seed
    );

    // ── Candidate result ─────────────────────────────────────────────

    public sealed record CandidateResult(
        string Name,
        double MeanMonotonicity,      // fraction of steps where value decreased
        double MeanRateOfChange,      // average Δ/step
        double RecoveryConsistency,   // monotonicity during recovery phase
        double CrossSeedRobustness,   // std of monotonicity across seeds
        double InitialValue,
        double FinalValue,
        double TotalDecrease           // initial - final (positive = decreasing)
    );

    // ── Candidate definitions ────────────────────────────────────────

    public static readonly (string Name, Func<TemporalNetwork, double> Compute)[] Candidates =
    {
        ("P1: Sync Deficit (1-R)",        net => { var m = SynchronizationMetrics.FromNetwork(net, 0); return 1.0 - m.OrderParameterR; }),
        ("P2: Phase Variance",            net => { var m = SynchronizationMetrics.FromNetwork(net, 0); return m.PhaseVariance; }),
        ("P3: Neighbor Tension",          net => MeanAbsSinDiff(net)),
        ("P4: Frequency StdDev",          net => FreqStdDev(net)),
        ("P5: LocalCoh Deficit",          net => { var df = new LocalDensityField(20); df.Compute(net, 1); return 1.0 - df.MaxLocalR(); }),
        ("P6: Phase Energy",              net => PhaseEnergy(net)),
        ("P7: Mean |Δθ| (raw)",           net => MeanAbsPhaseDiff(net)),
        ("P8: Coupling-Weighted Tension", net => CouplingWeightedTension(net)),
        ("P9: Identity Drift Rate",       net => 0.0), // placeholder, computed via snapshots
        ("P10: Composite (Var+Sync+Tens)",net => CompositePotential(net)),
    };

    // ── Potential computations ───────────────────────────────────────

    private static double MeanAbsSinDiff(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { sum += Math.Abs(Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase)); c++; }
        return sum / c;
    }

    private static double MeanAbsPhaseDiff(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
        {
            double d = Math.Abs(net.Nodes[j].Phase - net.Nodes[i].Phase);
            d = Math.Min(d, 2 * Math.PI - d);
            sum += d; c++;
        }
        return sum / c;
    }

    private static double FreqStdDev(TemporalNetwork net)
    {
        double mean = net.Nodes.Average(n => n.Frequency);
        double sq = net.Nodes.Average(n => (n.Frequency - mean) * (n.Frequency - mean));
        return Math.Sqrt(sq);
    }

    private static double PhaseEnergy(TemporalNetwork net)
    {
        // Sum over pairs of (1 - cos(Δθ))/2 ≈ coupling potential energy
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { sum += (1.0 - Math.Cos(net.Nodes[j].Phase - net.Nodes[i].Phase)) / 2.0; c++; }
        return sum / c;
    }

    private static double CouplingWeightedTension(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, totalW = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double w = net.Matrix.GetCoupling(i, j);
                double d = Math.Abs(Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase));
                sum += w * d; totalW += w;
            }
        return totalW > 0 ? sum / totalW : 0;
    }

    private static double CompositePotential(TemporalNetwork net)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        double sync = 1.0 - m.OrderParameterR;
        double tens = MeanAbsSinDiff(net);
        var df = new LocalDensityField(20); df.Compute(net, 1);
        double loc = 1.0 - df.MaxLocalR();
        return (sync + tens + loc) / 3.0;
    }

    // ── Snapshot collection ──────────────────────────────────────────

    private static PotentialSnapshot Snapshot(TemporalNetwork net, int iter)
    {
        var pots = new double[Candidates.Length];
        for (int i = 0; i < Candidates.Length; i++)
            pots[i] = Candidates[i].Compute(net);

        var m = SynchronizationMetrics.FromNetwork(net, 0);
        var df = new LocalDensityField(20); df.Compute(net, 1);

        return new PotentialSnapshot(iter, m.OrderParameterR,
            net.Nodes.Average(n => n.Frequency), m.PhaseVariance,
            df.MaxLocalR(), 0, pots);
    }

    // ── Run evolution trace ──────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork nw, string h, Random rng,
        MemoryTemporalSimulation sim, int stepIters = 400)
    {
        foreach (char p in h)
        {
            double shift = p == 'A' ? 0.4 : p == 'B' ? -0.4 : (rng.NextDouble() * 2 - 1) * 0.4;
            foreach (var node in nw.Nodes) node.Phase += shift;
            sim.Run(stepIters);
        }
    }

    public static List<EvolutionTrace> RunEvolution(
        string history, double beta, double k, double lambda, int n, int seed,
        int formationIters = 2000, int injectIters = 1500, int recoveryIters = 1500,
        int snapshotInterval = 100)
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

        var traces = new List<EvolutionTrace>();

        // Phase 1: Formation + training.
        var snaps1 = new List<PotentialSnapshot>();
        for (int iter = 0; iter <= formationIters; iter += snapshotInterval)
        {
            if (iter > 0) sim.Run(snapshotInterval);
            snaps1.Add(Snapshot(network, iter));
        }
        // Train history.
        ApplyHistory(network, history, rng, sim);
        // Capture post-training snapshot.
        snaps1.Add(Snapshot(network, formationIters + history.Length * 400));
        traces.Add(new EvolutionTrace(history, "formation+training", snaps1, seed));

        // Phase 2: Energy injection.
        foreach (var node in network.Nodes) node.Frequency *= 1.5;
        sim.Run(200);
        var snaps2 = new List<PotentialSnapshot>();
        for (int iter = 0; iter <= injectIters; iter += snapshotInterval)
        {
            if (iter > 0) sim.Run(snapshotInterval);
            snaps2.Add(Snapshot(network, iter));
        }
        traces.Add(new EvolutionTrace(history, "energy_inject", snaps2, seed));

        // Phase 3: Recovery.
        for (int i = 0; i < n; i++) network.Nodes[i].Frequency = origFreqs[i];
        sim.Run(200);
        var snaps3 = new List<PotentialSnapshot>();
        for (int iter = 0; iter <= recoveryIters; iter += snapshotInterval)
        {
            if (iter > 0) sim.Run(snapshotInterval);
            snaps3.Add(Snapshot(network, iter));
        }
        traces.Add(new EvolutionTrace(history, "recovery", snaps3, seed));

        return traces;
    }

    // ── Analyze monotonicity ─────────────────────────────────────────

    public static List<CandidateResult> AnalyzeCandidates(List<EvolutionTrace> allTraces)
    {
        var results = new List<CandidateResult>();

        for (int ci = 0; ci < Candidates.Length; ci++)
        {
            var name = Candidates[ci].Name;

            // Collect all monotonicity scores across all traces.
            var monoScores = new List<double>();
            var recScores = new List<double>();
            double totalInit = 0, totalFinal = 0;
            int traceCount = 0;
            var seedMonos = new Dictionary<int, List<double>>();

            foreach (var trace in allTraces)
            {
                var snaps = trace.Snapshots;
                if (snaps.Count < 2) continue;

                // Monotonicity: fraction of consecutive steps where value decreased.
                int decreases = 0, steps = 0;
                for (int s = 1; s < snaps.Count; s++)
                {
                    if (snaps[s].Potentials[ci] < snaps[s - 1].Potentials[ci])
                        decreases++;
                    steps++;
                }
                double mono = steps > 0 ? (double)decreases / steps : 0;
                monoScores.Add(mono);

                if (trace.Phase == "recovery")
                    recScores.Add(mono);

                totalInit += snaps[0].Potentials[ci];
                totalFinal += snaps[^1].Potentials[ci];
                traceCount++;

                if (!seedMonos.ContainsKey(trace.Seed))
                    seedMonos[trace.Seed] = new List<double>();
                seedMonos[trace.Seed].Add(mono);
            }

            double meanMono = monoScores.Average();
            double meanRec = recScores.Count > 0 ? recScores.Average() : 0;

            // Cross-seed robustness: std of seed-mean monotonicity.
            var seedMeans = seedMonos.Values.Select(l => l.Average()).ToList();
            double seedStd = seedMeans.Count > 1 ? StdDev(seedMeans) : 0;

            // Rate of change: average per-step change.
            double totalChange = 0; int changeSteps = 0;
            foreach (var trace in allTraces)
            {
                var s = trace.Snapshots;
                for (int i = 1; i < s.Count; i++)
                {
                    totalChange += Math.Abs(s[i].Potentials[ci] - s[i - 1].Potentials[ci]);
                    changeSteps++;
                }
            }
            double rateOfChange = changeSteps > 0 ? totalChange / changeSteps : 0;

            double initVal = traceCount > 0 ? totalInit / traceCount : 0;
            double finalVal = traceCount > 0 ? totalFinal / traceCount : 0;

            results.Add(new CandidateResult(name, meanMono, rateOfChange, meanRec,
                seedStd, initVal, finalVal, initVal - finalVal));
        }

        return results.OrderByDescending(r => r.MeanMonotonicity).ToList();
    }

    private static double StdDev(List<double> vals)
    {
        if (vals.Count < 2) return 0;
        double m = vals.Average();
        return Math.Sqrt(vals.Sum(v => (v - m) * (v - m)) / (vals.Count - 1));
    }
}
