using System.Collections.Concurrent;
using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Investigates whether resonance identity and resonance energy represent
/// the same degree of freedom or two independent properties of a condensate.
/// 
/// Hypothesis H0: Identity is fully determined by energy.
/// Hypothesis H1: Identity and energy are independent.
/// </summary>
public static class IdentityEnergyAnalyzer
{
    /// <summary>
    /// A single measurement of a condensate's identity and energy state
    /// after a specific history, memory strength, and energy injection.
    /// </summary>
    public sealed record IdentityEnergyState(
        string History,
        double Beta,
        double InjectionLevel,
        double FinalR,
        double MeanFreq,
        double Energy,
        double LocalCoherence,
        double PhaseVariance,
        double MemoryScore,
        int AttractorClusters,
        int Seed
    );

    /// <summary>
    /// Aggregated correlation metrics between identity and energy
    /// across all measured states.
    /// </summary>
    public sealed record CorrelationMatrix(
        // Pearson correlation between identity distance and energy.
        double PearsonR,

        // Mutual information I(Identity; Energy) in bits.
        double MutualInformationBits,

        // Cross-energy identity stability: for fixed history,
        // how much does identity vary across energy levels?
        // Low = identical identity at different energies (H1 support).
        double CrossEnergyIdentityStability,

        // Cross-identity energy stability: for fixed energy level,
        // how much does energy vary across different histories?
        // Low = identical energy for different identities (H1 support).
        double CrossIdentityEnergyStability,

        // Classification: A=Identity=Energy, B=StrongDependence,
        // C=WeakDependence, D=Independent.
        string RelationshipClassification,

        // Supporting metrics.
        double MeanIdentityDistanceSameEnergy,
        double MeanIdentityDistanceDiffEnergy,
        double MeanEnergyDistanceSameIdentity,
        double MeanEnergyDistanceDiffIdentity,
        int TotalStates
    );

    // ── History application ──────────────────────────────────────────

    private static void ApplyHistory(TemporalNetwork network, char p, Random rng)
    {
        double shift = p switch
        {
            'A' => 0.4,
            'B' => -0.4,
            'C' => (rng.NextDouble() * 2 - 1) * 0.4,
            _ => 0
        };
        foreach (var node in network.Nodes)
            node.Phase += shift;
    }

    // ── Single-run analysis ──────────────────────────────────────────

    /// <summary>
    /// Runs one condensate simulation with the specified history,
    /// memory strength, and energy injection, returning all measurements.
    /// </summary>
    public static IdentityEnergyState Analyze(
        string history,
        double beta,
        double injectionLevel,
        double k,
        double lambda,
        int n,
        int seed,
        int iterations = 4000)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Initialize oscillators with deterministic seeding.
        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i,
                rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        // Record baseline frequencies before injection.
        double[] baselineFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();

        var sim = new MemoryTemporalSimulation(network, beta);

        // Phase 1: formation (1500 iterations).
        sim.Run(1500);

        // Phase 2: apply historical training sequence.
        int historyIters = history.Length * 400;
        foreach (char p in history)
        {
            ApplyHistory(network, p, rng);
            sim.Run(400);
        }

        // Phase 3: energy injection — scale all frequencies.
        foreach (var node in network.Nodes)
            node.Frequency = baselineFreqs[node.Id] * (1.0 + injectionLevel);

        // Phase 4: post-injection evolution.
        int remaining = iterations - 1500 - historyIters;
        if (remaining > 0)
            sim.Run(remaining);

        // ── Measurements ─────────────────────────────────────────────

        var metrics = SynchronizationMetrics.FromNetwork(network, 0);
        double finalR = metrics.OrderParameterR;
        double meanFreq = network.Nodes.Average(nd => nd.Frequency);
        double energy = finalR * meanFreq; // Energy proxy: R × ⟨ω⟩

        // Local coherence via density field.
        var df = new LocalDensityField(20);
        df.Compute(network, neighborhoodCells: 1);
        double localCoherence = df.MaxLocalR();
        int attractorClusters = df.CellsAboveThreshold(0.80);

        // Phase variance.
        double phaseVariance = metrics.PhaseVariance;

        // Memory score: mean absolute phase difference sin across
        // all oscillator pairs (proxy for how much memory is encoded).
        double memoryScore = ComputeMemoryScore(network);

        return new IdentityEnergyState(
            history, beta, injectionLevel,
            finalR, meanFreq, energy,
            localCoherence, phaseVariance,
            memoryScore, attractorClusters, seed);
    }

    /// <summary>
    /// Computes a memory score proxy: the standard deviation of
    /// sin(Δθ) across all oscillator pairs, which captures
    /// how structured phase differences are.
    /// </summary>
    private static double ComputeMemoryScore(TemporalNetwork network)
    {
        int n = network.NodeCount;
        if (n < 2) return 0;

        double sum = 0, sumSq = 0;
        int count = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double sinDiff = Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                sum += Math.Abs(sinDiff);
                sumSq += sinDiff * sinDiff;
                count++;
            }
        }
        double mean = sum / count;
        double variance = sumSq / count - mean * mean;
        return Math.Sqrt(Math.Max(0, variance));
    }

    // ── Identity fingerprint ─────────────────────────────────────────

    /// <summary>
    /// Computes the identity fingerprint as a 3D vector:
    /// (FinalR, MeanFreq, PhaseVariance).
    /// </summary>
    private static (double R, double Freq, double Var) Fingerprint(IdentityEnergyState s)
        => (s.FinalR, s.MeanFreq, s.PhaseVariance);

    /// <summary>
    /// Euclidean distance between two identity fingerprints,
    /// normalized per-dimension by the standard deviation of the dataset.
    /// </summary>
    private static double IdentityDistance(
        IdentityEnergyState a, IdentityEnergyState b,
        double rStd, double fStd, double vStd)
    {
        double dr = (a.FinalR - b.FinalR) / Math.Max(rStd, 1e-10);
        double df = (a.MeanFreq - b.MeanFreq) / Math.Max(fStd, 1e-10);
        double dv = (a.PhaseVariance - b.PhaseVariance) / Math.Max(vStd, 1e-10);
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    // ── Correlation computation ──────────────────────────────────────

    /// <summary>
    /// Computes the full correlation matrix from all measured states.
    /// </summary>
    public static CorrelationMatrix ComputeCorrelations(List<IdentityEnergyState> states)
    {
        int m = states.Count;
        if (m < 10) throw new ArgumentException("Need at least 10 states.");

        // ── Per-dimension standard deviations for normalization ──────
        double rStd = StandardDeviation(states.Select(s => s.FinalR));
        double fStd = StandardDeviation(states.Select(s => s.MeanFreq));
        double vStd = StandardDeviation(states.Select(s => s.PhaseVariance));
        double eStd = StandardDeviation(states.Select(s => s.Energy));

        // ── Pearson r: correlate identity distance with energy diff ──
        var (pearsonR, _) = ComputeIdentityEnergyPearson(states, rStd, fStd, vStd);

        // ── Mutual information ───────────────────────────────────────
        double mi = ComputeMutualInformation(states);

        // ── Cross-energy identity stability ──────────────────────────
        double crossEnergyStability = ComputeCrossEnergyIdentityStability(states, rStd, fStd, vStd);

        // ── Cross-identity energy stability ──────────────────────────
        double crossIdentityStability = ComputeCrossIdentityEnergyStability(states, eStd);

        // ── Mean distances ───────────────────────────────────────────
        double meanIdSameE = MeanIdentityDistanceSameEnergy(states, rStd, fStd, vStd);
        double meanIdDiffE = MeanIdentityDistanceDiffEnergy(states, rStd, fStd, vStd);
        double meanEDiffId = MeanEnergyDistanceSameIdentity(states);
        double meanESameId = MeanEnergyDistanceDiffIdentity(states);

        // ── Classification ───────────────────────────────────────────
        string classification = ClassifyRelationship(
            pearsonR, mi, crossEnergyStability, crossIdentityStability,
            meanIdSameE, meanIdDiffE);

        return new CorrelationMatrix(
            pearsonR, mi,
            crossEnergyStability, crossIdentityStability,
            classification,
            meanIdSameE, meanIdDiffE,
            meanEDiffId, meanESameId,
            m);
    }

    // ── Pearson correlation ──────────────────────────────────────────

    private static (double r, double p) ComputeIdentityEnergyPearson(
        List<IdentityEnergyState> states, double rStd, double fStd, double vStd)
    {
        int m = states.Count;
        // Use identity distance to centroid as "identity coordinate"
        // and energy as the other coordinate.
        var centroidR = states.Average(s => s.FinalR);
        var centroidF = states.Average(s => s.MeanFreq);
        var centroidV = states.Average(s => s.PhaseVariance);
        var centroid = new IdentityEnergyState("", 0, 0,
            centroidR, centroidF, 0, 0, centroidV, 0, 0, 0);

        double[] idDist = new double[m];
        double[] energyVals = new double[m];
        for (int i = 0; i < m; i++)
        {
            idDist[i] = IdentityDistance(states[i], centroid, rStd, fStd, vStd);
            energyVals[i] = states[i].Energy;
        }

        double meanId = idDist.Average();
        double meanE = energyVals.Average();
        double cov = 0, varId = 0, varE = 0;
        for (int i = 0; i < m; i++)
        {
            double di = idDist[i] - meanId;
            double de = energyVals[i] - meanE;
            cov += di * de;
            varId += di * di;
            varE += de * de;
        }

        double r = cov / Math.Sqrt(Math.Max(varId, 1e-15) * Math.Max(varE, 1e-15));
        return (r, 0); // p-value omitted for simplicity
    }

    // ── Mutual information ───────────────────────────────────────────

    private static double ComputeMutualInformation(List<IdentityEnergyState> states)
    {
        const int bins = 5;

        // Discretize identity: use FinalR as 1D proxy.
        double rMin = states.Min(s => s.FinalR), rMax = states.Max(s => s.FinalR);
        double rRange = Math.Max(rMax - rMin, 1e-10);

        // Discretize energy.
        double eMin = states.Min(s => s.Energy), eMax = states.Max(s => s.Energy);
        double eRange = Math.Max(eMax - eMin, 1e-10);

        int m = states.Count;
        double[,] joint = new double[bins, bins];
        double[] idMarginal = new double[bins];
        double[] eMarginal = new double[bins];

        for (int i = 0; i < m; i++)
        {
            int ib = Math.Min(bins - 1, (int)((states[i].FinalR - rMin) / rRange * bins));
            int eb = Math.Min(bins - 1, (int)((states[i].Energy - eMin) / eRange * bins));
            joint[ib, eb] += 1.0 / m;
            idMarginal[ib] += 1.0 / m;
            eMarginal[eb] += 1.0 / m;
        }

        double mi = 0;
        for (int ib = 0; ib < bins; ib++)
        {
            for (int eb = 0; eb < bins; eb++)
            {
                if (joint[ib, eb] > 0 && idMarginal[ib] > 0 && eMarginal[eb] > 0)
                {
                    mi += joint[ib, eb] * Math.Log2(
                        joint[ib, eb] / (idMarginal[ib] * eMarginal[eb]));
                }
            }
        }

        return Math.Max(0, mi);
    }

    // ── Cross-energy identity stability ──────────────────────────────

    /// <summary>
    /// For each history, compute the variance of identity distances
    /// across energy injection levels. Low variance means identity
    /// is stable across energies → identity ≠ energy (H1).
    /// Returns the mean variance across histories (lower = more stable).
    /// </summary>
    private static double ComputeCrossEnergyIdentityStability(
        List<IdentityEnergyState> states, double rStd, double fStd, double vStd)
    {
        var histories = states.Select(s => s.History).Distinct().ToList();
        double totalVar = 0;
        int count = 0;

        foreach (var h in histories)
        {
            var subset = states.Where(s => s.History == h).ToList();
            if (subset.Count < 2) continue;

            // Mean identity fingerprint for this history.
            double meanR = subset.Average(s => s.FinalR);
            double meanF = subset.Average(s => s.MeanFreq);
            double meanV = subset.Average(s => s.PhaseVariance);
            var centroid = new IdentityEnergyState(h, 0, 0, meanR, meanF, 0, 0, meanV, 0, 0, 0);

            double varH = 0;
            foreach (var s in subset)
            {
                double d = IdentityDistance(s, centroid, rStd, fStd, vStd);
                varH += d * d;
            }
            totalVar += varH / subset.Count;
            count++;
        }

        // Stability = 1 / (1 + mean variance). Higher = more stable.
        double meanVar = count > 0 ? totalVar / count : 1e10;
        return 1.0 / (1.0 + meanVar);
    }

    // ── Cross-identity energy stability ──────────────────────────────

    /// <summary>
    /// For each energy injection level, compute the variance of energy
    /// across different histories. Low variance means energy is
    /// stable across identities → identity ≠ energy (H1).
    /// Returns the mean variance across energy levels (lower = more stable).
    /// </summary>
    private static double ComputeCrossIdentityEnergyStability(
        List<IdentityEnergyState> states, double eStd)
    {
        var injectionLevels = states.Select(s => s.InjectionLevel).Distinct().ToList();
        double totalVar = 0;
        int count = 0;

        foreach (var inj in injectionLevels)
        {
            var subset = states.Where(s => Math.Abs(s.InjectionLevel - inj) < 0.001).ToList();
            if (subset.Count < 2) continue;

            double meanE = subset.Average(s => s.Energy);
            double varI = 0;
            foreach (var s in subset)
            {
                double de = (s.Energy - meanE) / Math.Max(eStd, 1e-10);
                varI += de * de;
            }
            totalVar += varI / subset.Count;
            count++;
        }

        double meanVar = count > 0 ? totalVar / count : 1e10;
        return 1.0 / (1.0 + meanVar);
    }

    // ── Mean distances ───────────────────────────────────────────────

    private static double MeanIdentityDistanceSameEnergy(
        List<IdentityEnergyState> states, double rStd, double fStd, double vStd)
    {
        var levels = states.Select(s => s.InjectionLevel).Distinct().ToList();
        double totalDist = 0;
        int pairs = 0;

        foreach (var lvl in levels)
        {
            var subset = states.Where(s => Math.Abs(s.InjectionLevel - lvl) < 0.001).ToList();
            for (int i = 0; i < subset.Count; i++)
                for (int j = i + 1; j < subset.Count; j++)
                {
                    totalDist += IdentityDistance(subset[i], subset[j], rStd, fStd, vStd);
                    pairs++;
                }
        }

        return pairs > 0 ? totalDist / pairs : 0;
    }

    private static double MeanIdentityDistanceDiffEnergy(
        List<IdentityEnergyState> states, double rStd, double fStd, double vStd)
    {
        double totalDist = 0;
        int pairs = 0;

        for (int i = 0; i < states.Count; i++)
            for (int j = i + 1; j < states.Count; j++)
            {
                if (Math.Abs(states[i].InjectionLevel - states[j].InjectionLevel) > 0.001)
                {
                    totalDist += IdentityDistance(states[i], states[j], rStd, fStd, vStd);
                    pairs++;
                }
            }

        return pairs > 0 ? totalDist / pairs : 0;
    }

    private static double MeanEnergyDistanceSameIdentity(
        List<IdentityEnergyState> states)
    {
        var histories = states.Select(s => s.History).Distinct().ToList();
        double eStd = StandardDeviation(states.Select(s => s.Energy));
        double totalDist = 0;
        int pairs = 0;

        foreach (var h in histories)
        {
            var subset = states.Where(s => s.History == h).ToList();
            for (int i = 0; i < subset.Count; i++)
                for (int j = i + 1; j < subset.Count; j++)
                {
                    totalDist += Math.Abs(subset[i].Energy - subset[j].Energy) / Math.Max(eStd, 1e-10);
                    pairs++;
                }
        }

        return pairs > 0 ? totalDist / pairs : 0;
    }

    private static double MeanEnergyDistanceDiffIdentity(
        List<IdentityEnergyState> states)
    {
        double eStd = StandardDeviation(states.Select(s => s.Energy));
        double totalDist = 0;
        int pairs = 0;

        for (int i = 0; i < states.Count; i++)
            for (int j = i + 1; j < states.Count; j++)
            {
                if (states[i].History != states[j].History)
                {
                    totalDist += Math.Abs(states[i].Energy - states[j].Energy) / Math.Max(eStd, 1e-10);
                    pairs++;
                }
            }

        return pairs > 0 ? totalDist / pairs : 0;
    }

    // ── Classification ───────────────────────────────────────────────

    private static string ClassifyRelationship(
        double pearsonR, double mi,
        double crossEnergyStability, double crossIdentityStability,
        double meanIdSameE, double meanIdDiffE)
    {
        double absR = Math.Abs(pearsonR);

        // Decision tree:
        // A: Identity = Energy → |r| > 0.9, MI high, identity varies with energy.
        // B: Strong dependence → |r| > 0.6.
        // C: Weak dependence → 0.3 < |r| <= 0.6.
        // D: Independent → |r| <= 0.3.

        // Additional evidence from stability metrics:
        // If identities are different at same energy (meanIdSameE > meanIdDiffE),
        // that supports independence (H1).
        // If identities are similar at same energy, that supports H0.

        if (absR > 0.9 && mi > 1.5)
            return "A: Identity = Energy (fully determined)";
        if (absR > 0.6)
            return "B: Identity strongly depends on Energy";
        if (absR > 0.3)
            return "C: Identity weakly depends on Energy";
        return "D: Identity and Energy are independent";
    }

    // ── Utility ──────────────────────────────────────────────────────

    private static double StandardDeviation(IEnumerable<double> values)
    {
        var list = values.ToList();
        if (list.Count < 2) return 0;
        double mean = list.Average();
        double sumSq = list.Sum(v => (v - mean) * (v - mean));
        return Math.Sqrt(sumSq / (list.Count - 1));
    }
}
