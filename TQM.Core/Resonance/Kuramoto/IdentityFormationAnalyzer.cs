using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Generates resonance identity fingerprints from minimal perturbation
/// histories. Determines the smallest historical difference that
/// produces a distinguishable condensate identity.
/// 
/// TQM-044: order matters (AB ≠ BA).
/// TQM-050: identity is local and repulsive.
/// TQM-051: what is the MINIMUM history that creates identity?
/// </summary>
public static class IdentityFormationAnalyzer
{
    /// <summary>
    /// A single perturbation step: apply phase shift, then evolve.
    /// </summary>
    public readonly record struct PerturbationStep(int DelayBefore, double Amplitude, int EvolveAfter);

    /// <summary>
    /// Identity fingerprint: (OrderParameterR, MeanFrequency, PhaseVariance).
    /// </summary>
    public readonly record struct IdentityFingerprint(double R, double MeanFreq, double PhaseVar);

    /// <summary>
    /// A single run result: history name and resulting fingerprint.
    /// </summary>
    public sealed record FormationResult(
        string HistoryName,
        IdentityFingerprint Fingerprint,
        double LocalCoherence,
        double MemoryScore,
        int Seed
    );

    /// <summary>
    /// Aggregate: pairwise identity distances between all history pairs.
    /// </summary>
    public sealed record DistanceMatrix(
        List<string> HistoryNames,
        double[,] MeanDistances,    // [i,j] = mean identity distance history i → history j
        double[,] StdDistances,     // standard deviation
        double NoiseFloor,          // max intra-history distance (same history, different seeds)
        double NoiseFloorStd,
        double MinDistinguishableThreshold // 2σ above noise floor
    );

    // ── Distance ─────────────────────────────────────────────────────

    public static double Distance(IdentityFingerprint a, IdentityFingerprint b)
    {
        const double rS = 1.0, fS = 3.0, vS = 1.0;
        double dr = (a.R - b.R) / rS;
        double df = (a.MeanFreq - b.MeanFreq) / fS;
        double dv = (a.PhaseVar - b.PhaseVar) / vS;
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    // ── Fingerprint generation ───────────────────────────────────────

    /// <summary>
    /// Generates a condensate identity fingerprint using the specified
    /// perturbation history. Each step: optionally delay, then apply
    /// phase shift of given amplitude, then evolve.
    /// </summary>
    public static FormationResult GenerateFingerprint(
        string historyName,
        PerturbationStep[] steps,
        double beta,
        double k,
        double lambda,
        int n,
        int seed,
        int formationIters = 1500)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i,
                rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);

        // Formation.
        sim.Run(formationIters);

        // Apply perturbation history.
        foreach (var step in steps)
        {
            // Optional delay before perturbation.
            if (step.DelayBefore > 0)
                sim.Run(step.DelayBefore);

            // Apply phase shift.
            foreach (var node in network.Nodes)
                node.Phase += step.Amplitude;

            // Evolve after perturbation.
            if (step.EvolveAfter > 0)
                sim.Run(step.EvolveAfter);
        }

        // Measure identity fingerprint.
        var metrics = SynchronizationMetrics.FromNetwork(network, 0);
        double r = metrics.OrderParameterR;
        double freq = network.Nodes.Average(nd => nd.Frequency);
        double var = metrics.PhaseVariance;

        // Local coherence.
        var df = new LocalDensityField(20);
        df.Compute(network, 1);
        double localCoh = df.MaxLocalR();

        // Memory score.
        double memScore = ComputeMemoryScore(network);

        return new FormationResult(historyName,
            new IdentityFingerprint(r, freq, var),
            localCoh, memScore, seed);
    }

    // ── Memory score ─────────────────────────────────────────────────

    private static double ComputeMemoryScore(TemporalNetwork network)
    {
        int n = network.NodeCount;
        if (n < 2) return 0;
        double sum = 0, sumSq = 0;
        int count = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double sinDiff = Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                sum += Math.Abs(sinDiff);
                sumSq += sinDiff * sinDiff;
                count++;
            }
        double mean = sum / count;
        return Math.Sqrt(Math.Max(0, sumSq / count - mean * mean));
    }

    // ── Distance matrix computation ──────────────────────────────────

    /// <summary>
    /// Computes the full pairwise identity distance matrix from a list of
    /// formation results grouped by history name. Each history has multiple
    /// seed runs. Computes noise floor from intra-history distances.
    /// </summary>
    public static DistanceMatrix ComputeDistanceMatrix(
        List<FormationResult> results,
        int seedsPerHistory)
    {
        var historyNames = results.Select(r => r.HistoryName).Distinct().OrderBy(n => n).ToList();
        int h = historyNames.Count;
        var meanDist = new double[h, h];
        var stdDist = new double[h, h];

        // Group results by history.
        var groups = historyNames.ToDictionary(n => n,
            n => results.Where(r => r.HistoryName == n).ToList());

        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < h; j++)
            {
                var gi = groups[historyNames[i]];
                var gj = groups[historyNames[j]];

                if (i == j)
                {
                    // Intra-history: compare different seeds within same history.
                    var dists = new List<double>();
                    for (int si = 0; si < gi.Count; si++)
                        for (int sj = si + 1; sj < gi.Count; sj++)
                            dists.Add(Distance(gi[si].Fingerprint, gi[sj].Fingerprint));
                    meanDist[i, j] = dists.Count > 0 ? dists.Average() : 0;
                    stdDist[i, j] = dists.Count > 1 ? StdDev(dists) : 0;
                }
                else
                {
                    // Cross-history: compare matched seeds.
                    var dists = new List<double>();
                    int maxSeeds = Math.Min(gi.Count, gj.Count);
                    for (int s = 0; s < maxSeeds; s++)
                        dists.Add(Distance(gi[s].Fingerprint, gj[s].Fingerprint));
                    meanDist[i, j] = dists.Average();
                    stdDist[i, j] = dists.Count > 1 ? StdDev(dists) : 0;
                }
            }
        }

        // Noise floor: max intra-history distance.
        double noiseFloor = 0, noiseStd = 0;
        for (int i = 0; i < h; i++)
        {
            noiseFloor = Math.Max(noiseFloor, meanDist[i, i]);
            noiseStd = Math.Max(noiseStd, stdDist[i, i]);
        }

        double minDistinguishable = noiseFloor + 2.0 * noiseStd;

        return new DistanceMatrix(historyNames, meanDist, stdDist,
            noiseFloor, noiseStd, minDistinguishable);
    }

    private static double StdDev(List<double> values)
    {
        if (values.Count < 2) return 0;
        double mean = values.Average();
        return Math.Sqrt(values.Sum(v => (v - mean) * (v - mean)) / (values.Count - 1));
    }
}
