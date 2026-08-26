using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Searches for emergent conserved quantities that remain approximately
/// invariant across all known AT transformations.
/// 
/// Applies a sequence of transformations to each condensate and tracks
/// candidate invariants before/after each transformation.
/// </summary>
public static class EmergentConservationAnalyzer
{
    // ── Measurement ──────────────────────────────────────────────────

    public readonly record struct Measurement(
        double R, double MeanFreq, double PhaseVar,
        double LocalCoherence, double MemScore, double MeanPhase
    );

    // ── Transformation type ──────────────────────────────────────────

    public enum TransformType
    {
        Evolve,         // just evolve 500 iterations (control)
        PhaseNoise,     // add random phase noise ±0.5 rad, evolve 500
        EnergyInject,   // scale frequencies ×1.5, evolve 500
        EnergyRemove,   // scale frequencies ×0.5, evolve 500
        CollapseRecover,// scale ×3.0 → evolve 800 → restore → evolve 800
        MemoryDisrupt   // strong phase noise as memory disruption proxy
    }

    // ── Single transformation result ─────────────────────────────────

    public sealed record TransformStep(
        TransformType Type,
        Measurement Before,
        Measurement After
    );

    // ── Candidate invariant definition ───────────────────────────────

    public sealed record CandidateInvariant(
        string Name,
        Func<Measurement, double> Compute
    );

    // ── Invariant drift result ───────────────────────────────────────

    public sealed record InvariantDrift(
        string Name,
        double MeanRelativeDrift,
        double MaxRelativeDrift,
        double DriftStdDev,
        Dictionary<TransformType, double> DriftByTransform
    );

    // ── Aggregate result ─────────────────────────────────────────────

    public sealed record AggregateConservationResult(
        List<InvariantDrift> RankedInvariants,
        string BestInvariant,
        double BestMeanDrift,
        string ConservationClassification
    );

    // ── Candidate definitions ────────────────────────────────────────

    public static readonly CandidateInvariant[] Candidates =
    {
        new("Q1: Energy × Coherence",   m => m.R * m.MeanFreq * m.R),
        new("Q2: Energy × Memory",      m => m.R * m.MeanFreq * m.MemScore),
        new("Q3: Coherence × Memory",   m => m.R * m.MemScore),
        new("Q4: R (global coherence)", m => m.R),
        new("Q5: Phase Variance",       m => m.PhaseVar),
        new("Q6: Mean Phase",           m => m.MeanPhase),
        new("Q7: Local Coherence",      m => m.LocalCoherence),
        new("Q8: Energy / PhaseVar",    m => m.PhaseVar > 1e-10 ? m.R * m.MeanFreq / m.PhaseVar : 0),
        new("Q9: R × LocalCoh",        m => m.R * m.LocalCoherence),
        new("Q10: Freq × Memory",       m => m.MeanFreq * m.MemScore),
        new("Q11: R / PhaseVar",       m => m.PhaseVar > 1e-10 ? m.R / m.PhaseVar : 0),
        new("Q12: E×M×Coh (product)",   m => m.R * m.MeanFreq * m.MemScore * m.LocalCoherence),
    };

    // ── Measurement ──────────────────────────────────────────────────

    private static Measurement Measure(TemporalNetwork network)
    {
        var m = SynchronizationMetrics.FromNetwork(network, 0);
        double ms = ComputeMemScore(network);
        var df = new LocalDensityField(20); df.Compute(network, 1);
        return new Measurement(m.OrderParameterR, network.Nodes.Average(n => n.Frequency),
            m.PhaseVariance, df.MaxLocalR(), ms, m.AveragePhase);
    }

    private static double ComputeMemScore(TemporalNetwork network)
    {
        int n = network.NodeCount; if (n < 2) return 0;
        double sum = 0, sumSq = 0; int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double s = Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                sum += Math.Abs(s); sumSq += s * s; c++;
            }
        double mean = sum / c;
        return Math.Sqrt(Math.Max(0, sumSq / c - mean * mean));
    }

    // ── Transformation application ───────────────────────────────────

    private static Measurement ApplyTransform(TransformType t, TemporalNetwork nw,
        MemoryTemporalSimulation sim, double[] origFreqs, Random rng)
    {
        switch (t)
        {
            case TransformType.Evolve:
                sim.Run(500); break;

            case TransformType.PhaseNoise:
                foreach (var n in nw.Nodes) n.Phase += (rng.NextDouble() * 2 - 1) * 1.0;
                sim.Run(500); break;

            case TransformType.EnergyInject:
                foreach (var n in nw.Nodes) n.Frequency *= 1.5;
                sim.Run(500); break;

            case TransformType.EnergyRemove:
                foreach (var n in nw.Nodes) n.Frequency *= 0.5;
                sim.Run(500); break;

            case TransformType.CollapseRecover:
                foreach (var n in nw.Nodes) n.Frequency *= 3.0;
                sim.Run(800);
                for (int i = 0; i < nw.NodeCount; i++) nw.Nodes[i].Frequency = origFreqs[i];
                sim.Run(800); break;

            case TransformType.MemoryDisrupt:
                foreach (var n in nw.Nodes) n.Phase += (rng.NextDouble() * 2 - 1) * 2.0;
                sim.Run(500);
                sim.Run(300); break;
        }
        return Measure(nw);
    }

    // ── Run sequence ─────────────────────────────────────────────────

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

    public static List<TransformStep> RunSequence(
        string history, double beta, double k, double lambda, int n, int seed)
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
        double[] origFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();
        var sim = new MemoryTemporalSimulation(network, beta);

        sim.Run(1500);
        ApplyHistory(network, history, rng, sim);

        var transforms = new[] { TransformType.Evolve, TransformType.PhaseNoise,
            TransformType.EnergyInject, TransformType.EnergyRemove,
            TransformType.CollapseRecover, TransformType.MemoryDisrupt };

        var steps = new List<TransformStep>();
        foreach (var t in transforms)
        {
            var before = Measure(network);
            var after = ApplyTransform(t, network, sim, origFreqs, rng);
            steps.Add(new TransformStep(t, before, after));
        }
        return steps;
    }

    // ── Compute drifts ───────────────────────────────────────────────

    public static List<InvariantDrift> ComputeDrifts(List<TransformStep> allSteps)
    {
        var result = new List<InvariantDrift>();
        foreach (var cand in Candidates)
        {
            var drifts = new List<double>();
            var byT = new Dictionary<TransformType, List<double>>();

            foreach (var step in allSteps)
            {
                double b = cand.Compute(step.Before), a = cand.Compute(step.After);
                double d = Math.Abs(b) > 1e-10 ? Math.Abs(a - b) / Math.Abs(b) : Math.Abs(a - b);
                drifts.Add(d);
                if (!byT.ContainsKey(step.Type)) byT[step.Type] = new List<double>();
                byT[step.Type].Add(d);
            }

            double meanD = drifts.Average(), maxD = drifts.Max();
            double stdD = drifts.Count > 1 ? StdDev(drifts) : 0;
            var dbt = byT.ToDictionary(kv => kv.Key, kv => kv.Value.Average());
            result.Add(new InvariantDrift(cand.Name, meanD, maxD, stdD, dbt));
        }
        return result.OrderBy(d => d.MeanRelativeDrift).ToList();
    }

    public static AggregateConservationResult Aggregate(List<InvariantDrift> ranked)
    {
        var best = ranked.First();
        string cls = best.MeanRelativeDrift < 0.05 ? "D: Emergent conservation law" :
                     best.MeanRelativeDrift < 0.15 ? "C: Strong invariant" :
                     best.MeanRelativeDrift < 0.30 ? "B: Weak invariant" : "A: No invariant";
        return new AggregateConservationResult(ranked, best.Name, best.MeanRelativeDrift, cls);
    }

    private static double StdDev(List<double> vals)
    {
        if (vals.Count < 2) return 0;
        double m = vals.Average();
        return Math.Sqrt(vals.Sum(v => (v - m) * (v - m)) / (vals.Count - 1));
    }
}
