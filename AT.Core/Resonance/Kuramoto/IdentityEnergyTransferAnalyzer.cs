using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether resonance identity survives energy-state transitions.
/// Measures identity before and after controlled energy injection/removal
/// within the same condensate to determine if identity is preserved
/// when energy changes.
/// 
/// AT-047 showed identity and energy are independent across runs.
/// AT-048 tests whether identity survives WITHIN a single run
/// when energy is deliberately changed.
/// </summary>
public static class IdentityEnergyTransferAnalyzer
{
    /// <summary>
    /// A single energy transfer measurement: before/after comparison
    /// within one condensate.
    /// </summary>
    public sealed record TransferProfile(
        string History,
        double Beta,
        double TransferFraction,
        // Before transfer
        double BeforeR,
        double BeforeFreq,
        double BeforeEnergy,
        double BeforePhaseVar,
        double BeforeLocalCoherence,
        double BeforeMemoryScore,
        // After transfer
        double AfterR,
        double AfterFreq,
        double AfterEnergy,
        double AfterPhaseVar,
        double AfterLocalCoherence,
        double AfterMemoryScore,
        // Differences
        double IdentityDistance,
        double EnergyChange,
        bool IdentityPreserved,
        double PreservationScore,
        int Seed
    );

    /// <summary>
    /// Aggregate preservation result across all transfer profiles.
    /// </summary>
    public sealed record PreservationResult(
        // Overall stats
        double MeanIdentityDistance,
        double MeanEnergyChange,
        double PreservationRate,
        int TotalTransfers,
        int PreservedCount,

        // By transfer level
        List<(double TransferFraction, double MeanIdDist, double MeanEDist, double PresRate)> ByTransfer,

        // By history
        List<(string History, double MeanIdDist, double PresRate)> ByHistory,

        // By beta
        List<(double Beta, double MeanIdDist, double PresRate)> ByBeta,

        // Critical threshold: highest transfer where >50% identities preserved
        double CriticalTransferThreshold,

        // Classification
        string TransferClassification
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

    // ── Identity fingerprint helpers ─────────────────────────────────

    private static (double R, double Freq, double Var) Fingerprint(TemporalNetwork network)
    {
        var m = SynchronizationMetrics.FromNetwork(network, 0);
        return (m.OrderParameterR,
                network.Nodes.Average(nd => nd.Frequency),
                m.PhaseVariance);
    }

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

    /// <summary>
    /// Normalized identity distance between two fingerprints.
    /// Uses a fixed normalization scale rather than dataset statistics
    /// since we compare within a single run.
    /// </summary>
    private static double IdentityDistance(
        (double R, double Freq, double Var) a,
        (double R, double Freq, double Var) b)
    {
        // Scale factors: R ∈ [0,1], Freq ∈ [0.5, 3.5], Var ∈ [0,1]
        const double rScale = 1.0;
        const double fScale = 3.0;
        const double vScale = 1.0;

        double dr = (a.R - b.R) / rScale;
        double df = (a.Freq - b.Freq) / fScale;
        double dv = (a.Var - b.Var) / vScale;
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    // ── Main analysis ────────────────────────────────────────────────

    /// <summary>
    /// Runs a single energy transfer experiment on a condensate
    /// with the given history, memory strength, and transfer fraction.
    /// </summary>
    public static TransferProfile AnalyzeTransfer(
        string history,
        double beta,
        double transferFraction, // positive = injection, negative = removal
        double k,
        double lambda,
        int n,
        int seed,
        int formationIters = 1500,
        int historyItersPerChar = 400,
        int recoveryIters = 1500)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Initialize oscillators.
        for (int i = 0; i < n; i++)
        {
            var node = new TemporalNode(i,
                rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            { X = rng.NextDouble(), Y = rng.NextDouble() };
            network.AddNode(node);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        // Record baseline frequencies (before training).
        double[] originalFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();

        var sim = new MemoryTemporalSimulation(network, beta);

        // Phase 1: Formation.
        sim.Run(formationIters);

        // Phase 2: Apply historical training.
        foreach (char p in history)
        {
            ApplyHistory(network, p, rng);
            sim.Run(historyItersPerChar);
        }

        // ── Measure BEFORE ───────────────────────────────────────────
        var fpBefore = Fingerprint(network);
        double beforeE = fpBefore.R * fpBefore.Freq;
        double beforeMem = ComputeMemoryScore(network);
        var dfBefore = new LocalDensityField(20);
        dfBefore.Compute(network, neighborhoodCells: 1);
        double beforeLocal = dfBefore.MaxLocalR();

        // Phase 3: Apply energy transfer — scale all frequencies.
        // Negative fractions = removal (e.g. -0.75 → multiply by 0.25).
        double scaleFactor = 1.0 + transferFraction;
        if (scaleFactor <= 0) scaleFactor = 0.01; // floor at 1%
        foreach (var node in network.Nodes)
            node.Frequency = originalFreqs[node.Id] * scaleFactor;

        // Phase 4: Post-transfer evolution (recovery).
        sim.Run(recoveryIters);

        // ── Measure AFTER ────────────────────────────────────────────
        var fpAfter = Fingerprint(network);
        double afterE = fpAfter.R * fpAfter.Freq;
        double afterMem = ComputeMemoryScore(network);
        var dfAfter = new LocalDensityField(20);
        dfAfter.Compute(network, neighborhoodCells: 1);
        double afterLocal = dfAfter.MaxLocalR();

        // ── Metrics ──────────────────────────────────────────────────
        double idDist = IdentityDistance(fpBefore, fpAfter);
        double eChange = afterE - beforeE;

        // Preservation: identity distance < 0.15 (normalized) = preserved.
        bool preserved = idDist < 0.15;
        double preservationScore = 1.0 / (1.0 + idDist * 10); // [0, 1], higher = better

        return new TransferProfile(
            history, beta, transferFraction,
            fpBefore.R, fpBefore.Freq, beforeE, fpBefore.Var, beforeLocal, beforeMem,
            fpAfter.R, fpAfter.Freq, afterE, fpAfter.Var, afterLocal, afterMem,
            idDist, eChange, preserved, preservationScore, seed);
    }

    // ── Aggregate analysis ───────────────────────────────────────────

    public static PreservationResult AnalyzePreservation(List<TransferProfile> profiles)
    {
        int total = profiles.Count;

        // ── Overall stats ────────────────────────────────────────────
        double meanIdDist = profiles.Average(p => p.IdentityDistance);
        double meanEDist = profiles.Average(p => Math.Abs(p.EnergyChange));
        int preserved = profiles.Count(p => p.IdentityPreserved);
        double preservationRate = (double)preserved / total;

        // ── By transfer level ────────────────────────────────────────
        var transfers = profiles.Select(p => p.TransferFraction).Distinct().OrderBy(t => t).ToList();
        var byTransfer = new List<(double, double, double, double)>();
        foreach (var tf in transfers)
        {
            var sub = profiles.Where(p => Math.Abs(p.TransferFraction - tf) < 0.001).ToList();
            if (sub.Count == 0) continue;
            double mid = sub.Average(p => p.IdentityDistance);
            double med = sub.Average(p => Math.Abs(p.EnergyChange));
            double pr = (double)sub.Count(p => p.IdentityPreserved) / sub.Count;
            byTransfer.Add((tf, mid, med, pr));
        }

        // ── By history ───────────────────────────────────────────────
        var histories = profiles.Select(p => p.History).Distinct().ToList();
        var byHistory = new List<(string, double, double)>();
        foreach (var h in histories)
        {
            var sub = profiles.Where(p => p.History == h).ToList();
            double mid = sub.Average(p => p.IdentityDistance);
            double pr = (double)sub.Count(p => p.IdentityPreserved) / sub.Count;
            byHistory.Add((h, mid, pr));
        }

        // ── By beta ──────────────────────────────────────────────────
        var betas = profiles.Select(p => p.Beta).Distinct().OrderBy(b => b).ToList();
        var byBeta = new List<(double, double, double)>();
        foreach (var b in betas)
        {
            var sub = profiles.Where(p => Math.Abs(p.Beta - b) < 0.001).ToList();
            double mid = sub.Average(p => p.IdentityDistance);
            double pr = (double)sub.Count(p => p.IdentityPreserved) / sub.Count;
            byBeta.Add((b, mid, pr));
        }

        // ── Critical threshold ───────────────────────────────────────
        // Find the highest transfer fraction where >50% identities preserved.
        double criticalThreshold = 0;
        foreach (var tf in transfers.OrderBy(t => t))
        {
            var sub = profiles.Where(p => Math.Abs(p.TransferFraction - tf) < 0.001).ToList();
            double pr = sub.Count > 0 ? (double)sub.Count(p => p.IdentityPreserved) / sub.Count : 0;
            if (pr >= 0.5)
                criticalThreshold = Math.Max(criticalThreshold, tf);
        }

        // ── Classification ───────────────────────────────────────────
        string classification = ClassifyTransfer(preservationRate, meanIdDist, criticalThreshold);

        return new PreservationResult(
            meanIdDist, meanEDist, preservationRate, total, preserved,
            byTransfer, byHistory, byBeta, criticalThreshold, classification);
    }

    private static string ClassifyTransfer(double preservationRate, double meanIdDist, double criticalThreshold)
    {
        if (preservationRate >= 0.90)
            return "D: Identity is fully independent of energy transfer";
        if (preservationRate >= 0.70)
            return "C: Identity survives moderate energy changes";
        if (preservationRate >= 0.40)
            return "B: Identity partially follows energy";
        return "A: Identity fully follows energy";
    }
}
