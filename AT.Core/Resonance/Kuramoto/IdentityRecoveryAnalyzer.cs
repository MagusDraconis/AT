using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether resonance identity destroyed by large energy transfers
/// is truly erased or merely suppressed, and whether it can be recovered
/// by gradually restoring energy to its original level.
/// 
/// AT-048: identity survives ±25% energy change, destroyed beyond.
/// AT-049: after destruction, can identity return if energy is restored?
/// </summary>
public static class IdentityRecoveryAnalyzer
{
    // ── Recovery schedule definitions ───────────────────────────────

    public sealed record RecoverySchedule(string Name, int Steps, int ItersPerStep, int FinalRecoveryIters);

    public static readonly RecoverySchedule Fast = new("Fast", 1, 0, 1000);
    public static readonly RecoverySchedule Medium = new("Medium", 5, 400, 500);
    public static readonly RecoverySchedule Slow = new("Slow", 10, 400, 500);

    public static readonly RecoverySchedule[] Schedules = { Fast, Medium, Slow };

    // ── Result types ─────────────────────────────────────────────────

    /// <summary>
    /// A single collapse-and-recovery measurement for one condensate.
    /// </summary>
    public sealed record RecoveryProfile(
        string History,
        double Beta,
        double TransferFraction,
        string ScheduleName,
        // Baseline (before collapse)
        double BaselineR, double BaselineFreq, double BaselineEnergy,
        double BaselinePhaseVar, double BaselineCoherence, double BaselineMemScore,
        // After collapse
        double CollapseR, double CollapseFreq, double CollapseEnergy,
        double CollapsePhaseVar, double CollapseCoherence, double CollapseMemScore,
        double CollapseIdDistance,
        // After recovery
        double RecoveryR, double RecoveryFreq, double RecoveryEnergy,
        double RecoveryPhaseVar, double RecoveryCoherence, double RecoveryMemScore,
        double RecoveryIdDistance,
        // Scores
        double IdentityRecoveryScore,  // 1 = perfect recovery, 0 = no recovery
        double MemorySurvivalScore,    // how much memory survived collapse
        double HistoricalRecoveryIndex,// recovery relative to collapse damage
        string CollapseClassification, // A/B/C/D
        int Seed
    );

    /// <summary>
    /// Aggregate result across all recovery profiles.
    /// </summary>
    public sealed record AggregateRecoveryResult(
        // Overall
        double MeanRecoveryScore,
        double MeanCollapseDistance,
        double MeanRecoveryDistance,
        string OverallClassification,
        int TotalRuns,
        int RecoveredCount,    // recovery score > 0.5
        int FullRecoveryCount, // recovery score > 0.85

        // Classification distribution
        List<(string Class, int Count, double Pct)> ClassificationDistribution,

        // By transfer magnitude
        List<(double Transfer, double MeanRecovery, double MeanCollapseDist)> ByTransfer,

        // By schedule
        List<(string Schedule, double MeanRecovery, double MeanCollapseDist)> BySchedule,

        // By beta
        List<(double Beta, double MeanRecovery, double MeanCollapseDist)> ByBeta,

        // By history
        List<(string History, double MeanRecovery, double MeanCollapseDist)> ByHistory
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

    // ── Identity fingerprint ─────────────────────────────────────────

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
            for (int j = i + 1; j < n; j++)
            {
                double sinDiff = Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                sum += Math.Abs(sinDiff);
                sumSq += sinDiff * sinDiff;
                count++;
            }
        double mean = sum / count;
        double variance = sumSq / count - mean * mean;
        return Math.Sqrt(Math.Max(0, variance));
    }

    private static double IdDistance((double R, double Freq, double Var) a,
                                      (double R, double Freq, double Var) b)
    {
        const double rS = 1.0, fS = 3.0, vS = 1.0;
        double dr = (a.R - b.R) / rS, df = (a.Freq - b.Freq) / fS, dv = (a.Var - b.Var) / vS;
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    // ── Main analysis: three-phase collapse-recovery ─────────────────

    /// <summary>
    /// Runs a full collapse-and-recovery experiment.
    /// Phase 1: Create condensate → measure baseline → apply destructive transfer → measure collapse.
    /// Phase 2: Gradually restore energy to original level according to schedule.
    /// Phase 3: Measure recovery and compare to baseline.
    /// </summary>
    public static RecoveryProfile AnalyzeRecovery(
        string history,
        double beta,
        double transferFraction,
        RecoverySchedule schedule,
        double k,
        double lambda,
        int n,
        int seed,
        int formationIters = 1500,
        int historyItersPerChar = 400,
        int collapseIters = 1000)
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
        double[] originalFreqs = network.Nodes.Select(nd => nd.Frequency).ToArray();
        var sim = new MemoryTemporalSimulation(network, beta);

        // ── Formation + training ─────────────────────────────────────
        sim.Run(formationIters);
        foreach (char p in history)
        {
            ApplyHistory(network, p, rng);
            sim.Run(historyItersPerChar);
        }

        // ── Phase 1a: Measure BASELINE ───────────────────────────────
        var fpBaseline = Fingerprint(network);
        double blEnergy = fpBaseline.R * fpBaseline.Freq;
        double blMem = ComputeMemoryScore(network);
        var dfBl = new LocalDensityField(20); dfBl.Compute(network, 1);
        double blCoh = dfBl.MaxLocalR();

        // ── Phase 1b: Apply destructive transfer ─────────────────────
        double scaleFactor = 1.0 + transferFraction;
        if (scaleFactor <= 0) scaleFactor = 0.01;
        foreach (var node in network.Nodes)
            node.Frequency = originalFreqs[node.Id] * scaleFactor;
        sim.Run(collapseIters);

        // ── Phase 1c: Measure COLLAPSE ───────────────────────────────
        var fpCollapse = Fingerprint(network);
        double coEnergy = fpCollapse.R * fpCollapse.Freq;
        double coMem = ComputeMemoryScore(network);
        var dfCo = new LocalDensityField(20); dfCo.Compute(network, 1);
        double coCoh = dfCo.MaxLocalR();
        double collapseDist = IdDistance(fpBaseline, fpCollapse);

        // ── Phase 2: Gradual energy restoration ──────────────────────
        // Linearly interpolate frequencies from collapsed back to original.
        // At step i of N steps, fraction = (i+1)/N of the way back.
        if (schedule.Steps > 0 && schedule.ItersPerStep > 0)
        {
            for (int step = 1; step <= schedule.Steps; step++)
            {
                double frac = (double)step / schedule.Steps;
                foreach (var node in network.Nodes)
                {
                    double collapsed = originalFreqs[node.Id] * scaleFactor;
                    node.Frequency = collapsed - frac * (collapsed - originalFreqs[node.Id]);
                }
                sim.Run(schedule.ItersPerStep);
            }
        }
        else if (schedule.Steps == 1)
        {
            // Fast: restore instantly.
            foreach (var node in network.Nodes)
                node.Frequency = originalFreqs[node.Id];
        }

        // Phase 2b: Final recovery evolution.
        sim.Run(schedule.FinalRecoveryIters);

        // ── Phase 3: Measure RECOVERY ────────────────────────────────
        var fpRecovery = Fingerprint(network);
        double reEnergy = fpRecovery.R * fpRecovery.Freq;
        double reMem = ComputeMemoryScore(network);
        var dfRe = new LocalDensityField(20); dfRe.Compute(network, 1);
        double reCoh = dfRe.MaxLocalR();
        double recoveryDist = IdDistance(fpBaseline, fpRecovery);

        // ── Scores ───────────────────────────────────────────────────
        // Identity recovery: how close is post-recovery to baseline?
        // 1 = perfect, 0 = no recovery (same as collapse or worse).
        double identityRecoveryScore = collapseDist > 1e-10
            ? Math.Max(0, 1.0 - recoveryDist / Math.Max(collapseDist, 1e-10))
            : 1.0;
        identityRecoveryScore = Math.Clamp(identityRecoveryScore, 0, 1);

        // Memory survival: did the memory score survive collapse?
        double memorySurvival = blMem > 1e-10
            ? Math.Clamp(coMem / Math.Max(blMem, 1e-10), 0, 1)
            : 1.0;

        // Historical recovery: recovery relative to collapse damage.
        double historicalRecovery = collapseDist > 1e-10
            ? Math.Clamp((collapseDist - recoveryDist) / collapseDist, 0, 1)
            : 1.0;

        string classification = ClassifyCollapse(identityRecoveryScore, recoveryDist, collapseDist);

        return new RecoveryProfile(
            history, beta, transferFraction, schedule.Name,
            fpBaseline.R, fpBaseline.Freq, blEnergy, fpBaseline.Var, blCoh, blMem,
            fpCollapse.R, fpCollapse.Freq, coEnergy, fpCollapse.Var, coCoh, coMem,
            collapseDist,
            fpRecovery.R, fpRecovery.Freq, reEnergy, fpRecovery.Var, reCoh, reMem,
            recoveryDist,
            identityRecoveryScore, memorySurvival, historicalRecovery,
            classification, seed);
    }

    // ── Classification ───────────────────────────────────────────────

    private static string ClassifyCollapse(double recoveryScore, double recoveryDist, double collapseDist)
    {
        if (recoveryDist > collapseDist * 0.9)
            return "A: Permanent Destruction";
        if (recoveryScore >= 0.85)
            return "D: Fully Recoverable";
        if (recoveryScore >= 0.50)
            return "C: Temporary Suppression";
        if (recoveryScore >= 0.20)
            return "B: Partial Destruction";
        return "A: Permanent Destruction";
    }

    // ── Aggregate analysis ───────────────────────────────────────────

    public static AggregateRecoveryResult Aggregate(List<RecoveryProfile> profiles)
    {
        int total = profiles.Count;
        double meanRecovery = profiles.Average(p => p.IdentityRecoveryScore);
        double meanColDist = profiles.Average(p => p.CollapseIdDistance);
        double meanRecDist = profiles.Average(p => p.RecoveryIdDistance);

        int recovered = profiles.Count(p => p.IdentityRecoveryScore > 0.5);
        int fullyRecovered = profiles.Count(p => p.IdentityRecoveryScore > 0.85);

        // Overall classification based on mean recovery.
        string overallClass = ClassifyCollapse(meanRecovery, meanRecDist, meanColDist);

        // Classification distribution.
        var classDist = profiles.GroupBy(p => p.CollapseClassification[..1])
            .Select(g => (g.Key + g.First().CollapseClassification[1..], g.Count(),
                          (double)g.Count() / total * 100))
            .OrderBy(c => c.Item1)
            .ToList();

        // By transfer.
        var transfers = profiles.Select(p => p.TransferFraction).Distinct().OrderBy(t => t).ToList();
        var byTransfer = transfers.Select(tf =>
        {
            var sub = profiles.Where(p => Math.Abs(p.TransferFraction - tf) < 0.001).ToList();
            return (tf, sub.Average(p => p.IdentityRecoveryScore),
                    sub.Average(p => p.CollapseIdDistance));
        }).ToList();

        // By schedule.
        var scheds = profiles.Select(p => p.ScheduleName).Distinct().ToList();
        var bySchedule = scheds.Select(s =>
        {
            var sub = profiles.Where(p => p.ScheduleName == s).ToList();
            return (s, sub.Average(p => p.IdentityRecoveryScore),
                    sub.Average(p => p.CollapseIdDistance));
        }).ToList();

        // By beta.
        var betas = profiles.Select(p => p.Beta).Distinct().OrderBy(b => b).ToList();
        var byBeta = betas.Select(b =>
        {
            var sub = profiles.Where(p => Math.Abs(p.Beta - b) < 0.001).ToList();
            return (b, sub.Average(p => p.IdentityRecoveryScore),
                    sub.Average(p => p.CollapseIdDistance));
        }).ToList();

        // By history.
        var histories = profiles.Select(p => p.History).Distinct().ToList();
        var byHistory = histories.Select(h =>
        {
            var sub = profiles.Where(p => p.History == h).ToList();
            return (h, sub.Average(p => p.IdentityRecoveryScore),
                    sub.Average(p => p.CollapseIdDistance));
        }).ToList();

        return new AggregateRecoveryResult(
            meanRecovery, meanColDist, meanRecDist,
            overallClass, total, recovered, fullyRecovered,
            classDist, byTransfer, bySchedule, byBeta, byHistory);
    }
}
