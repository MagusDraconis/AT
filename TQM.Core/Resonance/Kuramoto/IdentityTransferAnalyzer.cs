using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Tests whether resonance identity can be transferred between condensates
/// through spatial coupling interaction.
/// 
/// TQM-049: identity is latent and recoverable.
/// TQM-050: can identity PROPAGATE from one condensate to another?
/// </summary>
public static class IdentityTransferAnalyzer
{
    // ── Result types ─────────────────────────────────────────────────

    /// <summary>
    /// A single two-condensate interaction measurement.
    /// </summary>
    public sealed record TransferProfile(
        double DistanceLambda,   // separation in units of λ
        int InteractionDuration,
        double Beta,
        // Condensate A (history AB) — before
        double A_BeforeR, double A_BeforeFreq, double A_BeforeVar,
        // Condensate A — after
        double A_AfterR, double A_AfterFreq, double A_AfterVar,
        // Condensate B (history BA) — before
        double B_BeforeR, double B_BeforeFreq, double B_BeforeVar,
        // Condensate B — after
        double B_AfterR, double B_AfterFreq, double B_AfterVar,
        // Cross-distances
        double InitialCrossDist,   // identity distance A_before ↔ B_before
        double FinalCrossDist,     // identity distance A_after ↔ B_after
        // Transfer scores
        double Transfer_A_To_B,    // how much B moved toward A (signed, + = B absorbed A)
        double Transfer_B_To_A,    // how much A moved toward B
        double IdentitySurvivalA,  // how much of A's identity survived
        double IdentitySurvivalB,  // how much of B's identity survived
        double SharedIdentityScore,// 1 = identical after, 0 = unchanged
        double DominanceScore,     // positive = A dominates, negative = B dominates
        string InteractionClass,   // Isolation/Transfer/Hybridization/Synchronization
        int Seed
    );

    public sealed record AggregateTransferResult(
        double MeanTransfer_A_To_B,
        double MeanTransfer_B_To_A,
        double MeanSurvivalA, double MeanSurvivalB,
        double MeanSharedIdentity,
        string OverallClass,
        int TotalRuns,
        List<(string Class, int Count, double Pct)> ClassDistribution,
        List<(double Dist, double TransferAB, double TransferBA, double SharedId)> ByDistance,
        List<(int Duration, double TransferAB, double TransferBA, double SharedId)> ByDuration,
        List<(double Beta, double TransferAB, double TransferBA, double SharedId)> ByBeta
    );

    // ── Identity fingerprint (per-condensate subset) ────────────────

    private static (double R, double Freq, double Var) CondensateFingerprint(
        TemporalNetwork network, int startIdx, int count)
    {
        double sumSin = 0, sumCos = 0, sumFreq = 0;
        for (int i = startIdx; i < startIdx + count; i++)
        {
            var n = network.Nodes[i];
            sumSin += Math.Sin(n.Phase);
            sumCos += Math.Cos(n.Phase);
            sumFreq += n.Frequency;
        }
        double r = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / count;
        double var = 1.0 - r;
        return (r, sumFreq / count, var);
    }

    private static double IdDist((double R, double Freq, double Var) a,
                                  (double R, double Freq, double Var) b)
    {
        const double rS = 1.0, fS = 3.0, vS = 1.0;
        double dr = (a.R - b.R) / rS, df = (a.Freq - b.Freq) / fS, dv = (a.Var - b.Var) / vS;
        return Math.Sqrt(dr * dr + df * df + dv * dv);
    }

    // ── Phase shift for history ─────────────────────────────────────

    private static void ShiftOscillators(TemporalNetwork net, int start, int count, double shift)
    {
        for (int i = start; i < start + count; i++)
            net.Nodes[i].Phase += shift;
    }

    // ── Main analysis ────────────────────────────────────────────────

    /// <summary>
    /// Runs a two-condensate identity transfer experiment.
    /// </summary>
    public static TransferProfile AnalyzeTransfer(
        double distanceLambda, // separation in units of λ
        int interactionDuration,
        double beta,
        double k,
        double lambda,
        int nPerCondensate,
        int seed,
        int formationIters = 1500,
        int trainItersPerStep = 400)
    {
        int n = nPerCondensate * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        double spread = lambda * 0.8;  // oscillator spread within condensate
        double spatialOffset = distanceLambda * lambda;  // actual spatial separation

        // Condensate A center: (0.3, 0.5), B center: (0.3 + offset, 0.5)
        double ax = 0.3, ay = 0.5;
        double bx = 0.3 + spatialOffset, by = 0.5;

        for (int i = 0; i < nPerCondensate; i++)
        {
            // Condensate A
            var nodeA = new TemporalNode(i,
                rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            {
                X = Math.Clamp(ax + (rng.NextDouble() * 2 - 1) * spread, 0, 1),
                Y = Math.Clamp(ay + (rng.NextDouble() * 2 - 1) * spread, 0, 1)
            };
            network.AddNode(nodeA);
        }

        for (int i = 0; i < nPerCondensate; i++)
        {
            // Condensate B
            var nodeB = new TemporalNode(nPerCondensate + i,
                rng.NextDouble() * 2 * Math.PI,
                0.5 + rng.NextDouble() * 1.5)
            {
                X = Math.Clamp(bx + (rng.NextDouble() * 2 - 1) * spread, 0, 1),
                Y = Math.Clamp(by + (rng.NextDouble() * 2 - 1) * spread, 0, 1)
            };
            network.AddNode(nodeB);
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        var sim = new MemoryTemporalSimulation(network, beta);

        // Phase 1: Formation.
        sim.Run(formationIters);

        // Phase 2: Train A with AB history.
        ShiftOscillators(network, 0, nPerCondensate, 0.4);  // A
        sim.Run(trainItersPerStep);
        ShiftOscillators(network, 0, nPerCondensate, -0.4); // B
        sim.Run(trainItersPerStep);

        // Phase 3: Train B with BA history.
        ShiftOscillators(network, nPerCondensate, nPerCondensate, -0.4); // B
        sim.Run(trainItersPerStep);
        ShiftOscillators(network, nPerCondensate, nPerCondensate, 0.4);  // A
        sim.Run(trainItersPerStep);

        // ── Measure BEFORE ───────────────────────────────────────────
        var fpAb = CondensateFingerprint(network, 0, nPerCondensate);
        var fpBb = CondensateFingerprint(network, nPerCondensate, nPerCondensate);
        double initCrossDist = IdDist(fpAb, fpBb);

        // Phase 4: Interaction (natural dynamics, no perturbations).
        sim.Run(interactionDuration);

        // ── Measure AFTER ────────────────────────────────────────────
        var fpAa = CondensateFingerprint(network, 0, nPerCondensate);
        var fpBa = CondensateFingerprint(network, nPerCondensate, nPerCondensate);
        double finalCrossDist = IdDist(fpAa, fpBa);

        // ── Transfer scores ──────────────────────────────────────────
        // How much did B move toward A's original identity?
        double distBbToAb = IdDist(fpBb, fpAb); // B_before vs A_before
        double distBaToAb = IdDist(fpBa, fpAb); // B_after vs A_before
        double transferAToB = distBbToAb > 1e-10
            ? Math.Clamp((distBbToAb - distBaToAb) / distBbToAb, -1, 1)
            : 0;

        // How much did A move toward B's original identity?
        double distAbToBb = IdDist(fpAb, fpBb);
        double distAaToBb = IdDist(fpAa, fpBb);
        double transferBToA = distAbToBb > 1e-10
            ? Math.Clamp((distAbToBb - distAaToBb) / distAbToBb, -1, 1)
            : 0;

        // Identity survival.
        double distAbToAa = IdDist(fpAb, fpAa);
        double survivalA = distBbToAb > 1e-10
            ? Math.Clamp(1.0 - distAbToAa / distBbToAb, 0, 1)
            : 1.0;

        double distBbToBa = IdDist(fpBb, fpBa);
        double survivalB = distBbToAb > 1e-10
            ? Math.Clamp(1.0 - distBbToBa / distBbToAb, 0, 1)
            : 1.0;

        // Shared identity: how similar are they after?
        double sharedScore = initCrossDist > 1e-10
            ? Math.Clamp(1.0 - finalCrossDist / initCrossDist, 0, 1)
            : 0;

        // Dominance: positive = A dominates (B moved toward A more than A toward B).
        double dominance = transferAToB - transferBToA;

        // Classification.
        string classification = ClassifyInteraction(transferAToB, transferBToA,
            sharedScore, survivalA, survivalB);

        return new TransferProfile(
            distanceLambda, interactionDuration, beta,
            fpAb.R, fpAb.Freq, fpAb.Var,
            fpAa.R, fpAa.Freq, fpAa.Var,
            fpBb.R, fpBb.Freq, fpBb.Var,
            fpBa.R, fpBa.Freq, fpBa.Var,
            initCrossDist, finalCrossDist,
            transferAToB, transferBToA, survivalA, survivalB,
            sharedScore, dominance, classification, seed);
    }

    // ── Classification ───────────────────────────────────────────────

    private static string ClassifyInteraction(
        double tAB, double tBA, double shared, double survA, double survB)
    {
        double maxTransfer = Math.Max(Math.Abs(tAB), Math.Abs(tBA));

        if (shared > 0.80)
            return "D: Identity Synchronization (merged)";

        if (maxTransfer > 0.60)
        {
            if (tAB > tBA)
                return "B: Transfer (A dominates B)";
            else
                return "B: Transfer (B dominates A)";
        }

        if (shared > 0.30 && maxTransfer > 0.15)
            return "C: Hybridization (partial mixing)";

        return "A: Isolation (identities remain distinct)";
    }

    // ── Aggregate ────────────────────────────────────────────────────

    public static AggregateTransferResult Aggregate(List<TransferProfile> profiles)
    {
        int total = profiles.Count;
        double mtAB = profiles.Average(p => p.Transfer_A_To_B);
        double mtBA = profiles.Average(p => p.Transfer_B_To_A);
        double msA = profiles.Average(p => p.IdentitySurvivalA);
        double msB = profiles.Average(p => p.IdentitySurvivalB);
        double msi = profiles.Average(p => p.SharedIdentityScore);

        // Overall class based on shared identity.
        string overallClass = msi > 0.80 ? "D: Synchronization" :
                              msi > 0.30 ? "C: Hybridization" :
                              msi > 0.10 ? "B: Transfer" :
                              "A: Isolation";

        // Class distribution.
        var classDist = profiles.GroupBy(p => p.InteractionClass[..1])
            .Select(g => (g.First().InteractionClass, g.Count(),
                          (double)g.Count() / total * 100))
            .OrderBy(c => c.Item1)
            .ToList();

        // By distance.
        var dists = profiles.Select(p => p.DistanceLambda).Distinct().OrderBy(d => d).ToList();
        var byDist = dists.Select(d =>
        {
            var sub = profiles.Where(p => Math.Abs(p.DistanceLambda - d) < 0.001).ToList();
            return (d, sub.Average(p => p.Transfer_A_To_B),
                    sub.Average(p => p.Transfer_B_To_A),
                    sub.Average(p => p.SharedIdentityScore));
        }).ToList();

        // By duration.
        var durs = profiles.Select(p => p.InteractionDuration).Distinct().OrderBy(d => d).ToList();
        var byDur = durs.Select(d =>
        {
            var sub = profiles.Where(p => p.InteractionDuration == d).ToList();
            return (d, sub.Average(p => p.Transfer_A_To_B),
                    sub.Average(p => p.Transfer_B_To_A),
                    sub.Average(p => p.SharedIdentityScore));
        }).ToList();

        // By beta.
        var betas = profiles.Select(p => p.Beta).Distinct().OrderBy(b => b).ToList();
        var byBeta = betas.Select(b =>
        {
            var sub = profiles.Where(p => Math.Abs(p.Beta - b) < 0.001).ToList();
            return (b, sub.Average(p => p.Transfer_A_To_B),
                    sub.Average(p => p.Transfer_B_To_A),
                    sub.Average(p => p.SharedIdentityScore));
        }).ToList();

        return new AggregateTransferResult(
            mtAB, mtBA, msA, msB, msi, overallClass, total,
            classDist, byDist, byDur, byBeta);
    }
}
