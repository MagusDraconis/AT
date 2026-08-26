using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether coherence is the CAUSAL root variable behind identity,
/// memory, and condensate stability, or merely a correlated byproduct.
/// 
/// Actively disrupts coherence to target levels and measures the
/// causal impact on identity survival, memory retention, and recovery.
/// </summary>
public static class CoherenceDisruptionAnalyzer
{
    // ── Result types ─────────────────────────────────────────────────

    public sealed record DisruptionProfile(
        string History,
        double TargetR,
        double Beta,
        // Baseline
        double BaseR, double BaseFreq, double BaseVar,
        double BaseMem, double BaseEnergy,
        // After disruption
        double DisruptedR, double DisruptedFreq, double DisruptedVar,
        double DisruptedMem, double DisruptedEnergy,
        double ActualTargetError,    // |achieved_R - target_R|
        // After recovery
        double RecoveredR, double RecoveredFreq, double RecoveredVar,
        double RecoveredMem, double RecoveredEnergy,
        // Scores
        double IdentityPreservation,  // 1 = identity survived disruption
        double MemoryPreservation,    // 1 = memory survived disruption
        double RecoveryScore,         // 1 = fully recovered
        int Seed
    );

    public sealed record AggregateCausalResult(
        double MeanIdentityPreservation,
        double MeanMemoryPreservation,
        double MeanRecoveryScore,
        double IdentityCollapseThreshold,  // R_target below which identity fails
        double MemoryCollapseThreshold,
        double RecoveryThreshold,
        string CausalClassification,
        List<(double TargetR, double IdPres, double MemPres, double RecScore)> ByTarget
    );

    // ── Fingerprint & distance ───────────────────────────────────────

    private static (double R, double Freq, double Var) Fingerprint(TemporalNetwork net)
    {
        var m = SynchronizationMetrics.FromNetwork(net, 0);
        return (m.OrderParameterR, net.Nodes.Average(n => n.Frequency), m.PhaseVariance);
    }

    private static double IdDist((double R, double Freq, double Var) a,
                                  (double R, double Freq, double Var) b)
    {
        const double rS = 1.0, fS = 3.0, vS = 1.0;
        double dr = (a.R - b.R) / rS, df = (a.Freq - b.Freq) / fS, dv = (a.Var - b.Var) / vS;
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    private static double MemScore(TemporalNetwork net)
    {
        int n = net.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double s = Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
                sum += Math.Abs(s); sumSq += s * s; c++;
            }
        double m = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - m * m));
    }

    // ── Coherence disruption ─────────────────────────────────────────

    /// <summary>
    /// Applies Gaussian phase noise to reduce coherence to approximately
    /// the target R level. Uses σ = sqrt(-2 * ln(target_R)).
    /// </summary>
    private static double DisruptCoherence(TemporalNetwork net, double targetR, Random rng)
    {
        if (targetR >= 0.99) return SynchronizationMetrics.FromNetwork(net, 0).OrderParameterR;

        double sigma = Math.Sqrt(-2.0 * Math.Log(Math.Max(targetR, 0.01)));

        // Compute current mean phase for centering.
        double sumSin = 0, sumCos = 0;
        foreach (var n in net.Nodes) { sumSin += Math.Sin(n.Phase); sumCos += Math.Cos(n.Phase); }
        double meanPhase = Math.Atan2(sumSin, sumCos);

        // Apply Gaussian noise around mean phase.
        foreach (var n in net.Nodes)
        {
            double noise = SampleGaussian(rng) * sigma;
            n.Phase = meanPhase + noise;
        }

        return SynchronizationMetrics.FromNetwork(net, 0).OrderParameterR;
    }

    private static double SampleGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) * Math.Sin(2.0 * Math.PI * u2);
    }

    // ── History application ──────────────────────────────────────────

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

    // ── Main analysis ────────────────────────────────────────────────

    public static DisruptionProfile AnalyzeDisruption(
        string history, double targetR, double beta,
        double k, double lambda, int n, int seed,
        int formationIters = 1500, int disruptionSettle = 200, int recoveryIters = 1500)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        for (int i = 0; i < n; i++)
        {
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);

        // Formation + training.
        sim.Run(formationIters);
        ApplyHistory(network, history, rng, sim);

        // ── Baseline ─────────────────────────────────────────────────
        var fpBase = Fingerprint(network);
        double baseMem = MemScore(network);
        double baseEnergy = fpBase.R * fpBase.Freq;

        // ── Disruption ───────────────────────────────────────────────
        double achievedR = DisruptCoherence(network, targetR, rng);
        sim.Run(disruptionSettle);

        var fpDisrupted = Fingerprint(network);
        double disMem = MemScore(network);
        double disEnergy = fpDisrupted.R * fpDisrupted.Freq;
        double targetError = Math.Abs(fpDisrupted.R - targetR);

        double idPreservation = IdDist(fpBase, fpDisrupted) < 1e-10 ? 1.0
            : Math.Clamp(1.0 / (1.0 + IdDist(fpBase, fpDisrupted) * 5), 0, 1);

        double memPreservation = baseMem > 1e-10
            ? Math.Clamp(disMem / baseMem, 0, 1) : 1.0;

        // ── Recovery ─────────────────────────────────────────────────
        sim.Run(recoveryIters);

        var fpRecovered = Fingerprint(network);
        double recMem = MemScore(network);
        double recEnergy = fpRecovered.R * fpRecovered.Freq;

        double recoveryScore = IdDist(fpBase, fpDisrupted) < 1e-10 ? 1.0
            : Math.Clamp(1.0 - IdDist(fpBase, fpRecovered) / Math.Max(IdDist(fpBase, fpDisrupted), 1e-10), 0, 1);

        return new DisruptionProfile(history, targetR, beta,
            fpBase.R, fpBase.Freq, fpBase.Var, baseMem, baseEnergy,
            fpDisrupted.R, fpDisrupted.Freq, fpDisrupted.Var, disMem, disEnergy,
            targetError,
            fpRecovered.R, fpRecovered.Freq, fpRecovered.Var, recMem, recEnergy,
            idPreservation, memPreservation, recoveryScore, seed);
    }

    // ── Aggregate ────────────────────────────────────────────────────

    public static AggregateCausalResult Aggregate(List<DisruptionProfile> profiles)
    {
        double meanIp = profiles.Average(p => p.IdentityPreservation);
        double meanMp = profiles.Average(p => p.MemoryPreservation);
        double meanRs = profiles.Average(p => p.RecoveryScore);

        // Thresholds: highest targetR where preservation < 0.5.
        var targets = profiles.Select(p => p.TargetR).Distinct().OrderByDescending(t => t).ToList();
        double idThresh = 0, memThresh = 0, recThresh = 0;

        foreach (var t in targets)
        {
            var sub = profiles.Where(p => Math.Abs(p.TargetR - t) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double ip = sub.Average(p => p.IdentityPreservation);
            double mp = sub.Average(p => p.MemoryPreservation);
            double rs = sub.Average(p => p.RecoveryScore);
            if (ip < 0.5 && idThresh == 0) idThresh = t;
            if (mp < 0.5 && memThresh == 0) memThresh = t;
            if (rs < 0.5 && recThresh == 0) recThresh = t;
        }

        var byTarget = targets.Select(t =>
        {
            var sub = profiles.Where(p => Math.Abs(p.TargetR - t) < 0.001).ToList();
            return (t, sub.Average(p => p.IdentityPreservation),
                    sub.Average(p => p.MemoryPreservation),
                    sub.Average(p => p.RecoveryScore));
        }).ToList();

        string classification = ClassifyCausal(meanIp, idThresh);

        return new AggregateCausalResult(meanIp, meanMp, meanRs,
            idThresh, memThresh, recThresh, classification, byTarget);
    }

    private static string ClassifyCausal(double meanIdPres, double idThreshold)
    {
        if (meanIdPres < 0.3)
            return "A: Fundamental Cause — destroying coherence destroys identity";
        if (idThreshold > 0.5)
            return "A: Fundamental Cause — identity collapses at high coherence";
        if (idThreshold > 0.2)
            return "B: Necessary but not sufficient — coherence supports identity";
        if (meanIdPres > 0.6)
            return "C: Emergent consequence — identity survives low coherence";
        return "D: Correlated only — coherence and identity are independent";
    }
}
