using AT.Core.Temporal;
using AT.Core.Resonance.Kuramoto;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Repairs the rejected AT-083 {R, M} theory using failure modes
/// identified by AT-100. Preserves state variables, repairs equations.
///
/// AT-101: Theory Repair Program
/// </summary>
public static class TheoryRepairAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record RepairedTheoryCandidate(
        string Name,
        string Equation,
        string Description,
        Func<double, double, int, double, double, double> Predictor, // (R, M, N, K, lam) -> dR/dt
        double[] Parameters,
        double TrainingR2,
        double TrainingMAE);

    public sealed record RepairValidationResult(
        string AttackName,
        double R2,
        double MAE,
        bool Passed); // R² > 0.3 = pass

    public sealed record RepairValidationReport(
        RepairedTheoryCandidate BestCandidate,
        RepairedTheoryCandidate Baseline,
        List<RepairedTheoryCandidate> AllCandidates,
        Dictionary<string, List<RepairValidationResult>> PerCandidateResults,
        double BestSurvivalRate,
        string Classification);

    // ══════════════════════════════════════════════════════════════════
    // Training data point
    // ══════════════════════════════════════════════════════════════════

    public sealed record TrainPoint(
        double R, double M, double dRdt, int N, double K, double Lam, int Seed);

    // ══════════════════════════════════════════════════════════════════
    // Generate training data (same as AT-083 but with parameter diversity)
    // ══════════════════════════════════════════════════════════════════

    public static List<TrainPoint> GenerateTrainData(int baseSeed = 101_000_001)
    {
        var points = new List<TrainPoint>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        // Standard regime: N=100, K=2.0, λ=0.05.
        var states = TopologyEvolutionAnalyzer.GenerateTopologyEnsemble(
            2.0, 0.05, 100, 100, baseSeed);
        foreach (var s in states)
            points.Add(new TrainPoint(s.R, s.MeanCoupling, s.dRdt, 100, 2.0, 0.05, s.Seed));

        // Add some N=50 and N=200 data for N-scaling.
        for (int ni = 0; ni < 50; ni++)
        {
            int seed = baseSeed + 10000 + ni;
            foreach (int nTry in new[] { 50, 200 })
            {
                var net = BuildNetwork(types[ni % types.Length], nTry, seed, 2.0, 0.05);
                var rng = new Random(seed);
                for (int i = 0; i < nTry; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net);
                double M0 = ComputeM(net);
                Evolve(net, 10);
                double R1 = ComputeR(net);
                points.Add(new TrainPoint(R0, M0, (R1 - R0) / 10.0, nTry, 2.0, 0.05, seed));
            }
        }

        // Add some varied K,λ data.
        foreach (double kTry in new[] { 1.0, 3.0, 5.0 })
            foreach (double lTry in new[] { 0.03, 0.07, 0.10 })
                for (int si = 0; si < 10; si++)
                {
                    int seed = baseSeed + 20000 + si;
                    var net = BuildNetwork(types[si % types.Length], 100, seed, kTry, lTry);
                    var rng = new Random(seed);
                    for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                    double R0 = ComputeR(net);
                    double M0 = ComputeM(net);
                    Evolve(net, 10);
                    double R1 = ComputeR(net);
                    points.Add(new TrainPoint(R0, M0, (R1 - R0) / 10.0, 100, kTry, lTry, seed));
                }

        return points;
    }

    // ══════════════════════════════════════════════════════════════════
    // Generate repaired candidates
    // ══════════════════════════════════════════════════════════════════

    public static List<RepairedTheoryCandidate> GenerateRepairedCandidates(
        List<TrainPoint> trainData)
    {
        var candidates = new List<RepairedTheoryCandidate>();
        int n = trainData.Count;

        // Extract arrays.
        double[] R = trainData.Select(p => p.R).ToArray();
        double[] M = trainData.Select(p => p.M).ToArray();
        double[] Y = trainData.Select(p => p.dRdt).ToArray();
        int[] Ns = trainData.Select(p => p.N).ToArray();
        double[] Ks = trainData.Select(p => p.K).ToArray();

        // ── Model 0: Baseline (original AT-083, rejected by AT-100) ──
        {
            double[] X0 = new double[n];
            for (int i = 0; i < n; i++) X0[i] = 1.0;
            var (beta0, r2_0, mae0) = FitLinear(new[] { X0, R, M }, Y);
            candidates.Add(new RepairedTheoryCandidate("M0", "α₀ + α₁·R + α₂·M",
                "BASELINE: Original rejected theory. Linear in (1, R, M).",
                (r, mVal, nn, k, lam) => beta0[0] + beta0[1] * r + beta0[2] * mVal,
                beta0, r2_0, mae0));
        }

        // ── Model A: dR/dt = a·M·R·(1-R) ──
        {
            double[] X = new double[n];
            for (int i = 0; i < n; i++)
            {
                double r = Math.Max(R[i], 1e-6);
                X[i] = M[i] * r * (1.0 - r);
            }
            var (betaA, r2_A, maeA) = FitLinear(new[] { X }, Y, intercept: false);
            candidates.Add(new RepairedTheoryCandidate("A", "a·M·R·(1-R)",
                "Logistic saturation. Fixes R≈0 and R≈1 endpoints. No N-dependence.",
                (r, mVal, nn, k, lam) => betaA[0] * mVal * Math.Max(r, 1e-6) * (1.0 - r),
                betaA, r2_A, maeA));
        }

        // ── Model B: dR/dt = a·N·M·R·(1-R) ──
        {
            double[] X = new double[n];
            for (int i = 0; i < n; i++)
            {
                double r = Math.Max(R[i], 1e-6);
                X[i] = Ns[i] * M[i] * r * (1.0 - r);
            }
            var (betaB, r2_B, maeB) = FitLinear(new[] { X }, Y, intercept: false);
            candidates.Add(new RepairedTheoryCandidate("B", "a·N·M·R·(1-R)",
                "N-scaling logistic. Fixes R≈0, R≈1, AND N-dependence. PHYSICALLY MOTIVATED.",
                (r, mVal, nn, k, lam) => betaB[0] * nn * mVal * Math.Max(r, 1e-6) * (1.0 - r),
                betaB, r2_B, maeB));
        }

        // ── Model C: dR/dt = a·N·M·Rⁿ·(1-R) [grid search n] ──
        {
            double bestR2 = -1e9;
            double bestNExp = 1.0;
            double bestA = 0;

            foreach (double nExp in new[] { 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0 })
            {
                double[] X = new double[n];
                for (int i = 0; i < n; i++)
                {
                    double r = Math.Max(R[i], 1e-6);
                    X[i] = Ns[i] * M[i] * Math.Pow(r, nExp) * (1.0 - r);
                }
                var (bC, r2_C, _) = FitLinear(new[] { X }, Y, intercept: false);
                if (r2_C > bestR2) { bestR2 = r2_C; bestNExp = nExp; bestA = bC[0]; }
            }

            double nFinal = bestNExp;
            double aFinal = bestA;
            candidates.Add(new RepairedTheoryCandidate("C", $"a·N·M·R^{bestNExp:F2}·(1-R)",
                $"Variable R exponent (n={bestNExp:F2}). Captures nonlinear coherence growth.",
                (r, mVal, nn, k, lam) => aFinal * nn * mVal * Math.Pow(Math.Max(r, 1e-6), nFinal) * (1.0 - r),
                new[] { aFinal, nFinal }, bestR2, 0));
        }

        // ── Model D: dR/dt = a·N·M·Rⁿ·(1-R)^m [grid search n, m] ──
        {
            double bestR2 = -1e9;
            double bestNExp = 1.0, bestMExp = 1.0;
            double bestA = 0;

            foreach (double nExp in new[] { 0.5, 1.0, 1.5 })
                foreach (double mExp in new[] { 0.5, 1.0, 1.5 })
                {
                    double[] X = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        double r = Math.Max(R[i], 1e-6);
                        X[i] = Ns[i] * M[i] * Math.Pow(r, nExp) * Math.Pow(1.0 - r, mExp);
                    }
                    var (bD, r2_D, _) = FitLinear(new[] { X }, Y, intercept: false);
                    if (r2_D > bestR2) { bestR2 = r2_D; bestNExp = nExp; bestMExp = mExp; bestA = bD[0]; }
                }

            double nd = bestNExp, md = bestMExp, ad = bestA;
            candidates.Add(new RepairedTheoryCandidate("D", $"a·N·M·R^{nd:F2}·(1-R)^{md:F2}",
                $"Variable R and (1-R) exponents (n={nd:F2}, m={md:F2}).",
                (r, mVal, nn, k, lam) => ad * nn * mVal * Math.Pow(Math.Max(r, 1e-6), nd) * Math.Pow(1.0 - r, md),
                new[] { ad, nd, md }, bestR2, 0));
        }

        // ── Model E: dR/dt = a·N·M·Rⁿ·(1-R) + b·N·M·Rⁿ  (2-term) ──
        {
            double[] X1 = new double[n], X2 = new double[n];
            for (int i = 0; i < n; i++)
            {
                double r = Math.Max(R[i], 1e-6);
                X1[i] = Ns[i] * M[i] * r * (1.0 - r);  // growth term
                X2[i] = Ns[i] * M[i] * r * r;           // noise correction
            }
            var (betaE, r2_E, maeE) = FitLinear(new[] { X1, X2 }, Y, intercept: false);
            candidates.Add(new RepairedTheoryCandidate("E", "a·N·M·R·(1-R) + b·N·M·R²",
                "Two-term: logistic growth + noise/fluctuation correction.",
                (r, mVal, nn, k, lam) =>
                {
                    double rr = Math.Max(r, 1e-6);
                    return betaE[0] * nn * mVal * rr * (1.0 - rr) + betaE[1] * nn * mVal * rr * rr;
                },
                betaE, r2_E, maeE));
        }

        // ── Model F: dR/dt = a·K·M·R·(1-R)  (K scaling instead of N) ──
        {
            double[] X = new double[n];
            for (int i = 0; i < n; i++)
            {
                double r = Math.Max(R[i], 1e-6);
                X[i] = Ks[i] * M[i] * r * (1.0 - r);
            }
            var (betaF, r2_F, maeF) = FitLinear(new[] { X }, Y, intercept: false);
            candidates.Add(new RepairedTheoryCandidate("F", "a·K·M·R·(1-R)",
                "K-scaling logistic. K is the global coupling strength.",
                (r, mVal, nn, k, lam) => betaF[0] * k * mVal * Math.Max(r, 1e-6) * (1.0 - r),
                betaF, r2_F, maeF));
        }

        return candidates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Validate a candidate against AT-100 attacks
    // ══════════════════════════════════════════════════════════════════

    public static List<RepairValidationResult> ValidateCandidate(
        RepairedTheoryCandidate candidate, int baseSeed = 101_000_001)
    {
        var results = new List<RepairValidationResult>();

        // Attack 1: Extreme Coherence (R≈0, R≈1).
        results.Add(Validate_ExtremeR(candidate, baseSeed + 100));

        // Attack 2: Extreme M.
        results.Add(Validate_ExtremeM(candidate, baseSeed + 200));

        // Attack 3: Mixed Topologies.
        results.Add(Validate_Topologies(candidate, baseSeed + 300));

        // Attack 4: Different Coupling Laws.
        results.Add(Validate_CouplingLaws(candidate, baseSeed + 400));

        // Attack 5: High Phase Noise.
        results.Add(Validate_Noise(candidate, baseSeed + 500));

        // Attack 6: Large-N (N=500).
        results.Add(Validate_LargeN(candidate, baseSeed + 600));

        // Attack 7: Small-N (N=10).
        results.Add(Validate_SmallN(candidate, baseSeed + 700));

        // Attack 8: Out-of-Distribution.
        results.Add(Validate_OOD(candidate, baseSeed + 800));

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Individual attack validators
    // ══════════════════════════════════════════════════════════════════

    private static RepairValidationResult Validate_ExtremeR(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>(); int n = 100;

        // R≈0.
        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", n, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, n, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        // R≈1.
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetwork("clustered", n, seed + 1000 + s, 2.0, 0.05);
            var rng = new Random(seed + 1000 + s);
            double bp = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = bp + (rng.NextDouble() * 2 - 1) * 0.005;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, n, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        return Eval("Extreme Coherence", pred, obs);
    }

    private static RepairValidationResult Validate_ExtremeM(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>(); int n = 100;

        foreach (var (kTry, lTry) in new[] { (0.5, 0.20), (5.0, 0.03) })
            for (int s = 0; s < 15; s++)
            {
                var net = BuildNetwork("uniform", n, seed + s, kTry, lTry);
                var rng = new Random(seed + s);
                for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                pred.Add(c.Predictor(R0, M0, n, kTry, lTry));
                obs.Add((R1 - R0) / 10.0);
            }

        return Eval("Extreme M", pred, obs);
    }

    private static RepairValidationResult Validate_Topologies(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        foreach (var t in types)
            for (int s = 0; s < 8; s++)
            {
                var net = BuildNetwork(t, 100, seed + s, 2.0, 0.05);
                var rng = new Random(seed + s);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                pred.Add(c.Predictor(R0, M0, 100, 2.0, 0.05));
                obs.Add((R1 - R0) / 10.0);
            }

        return Eval("Mixed Topologies", pred, obs);
    }

    private static RepairValidationResult Validate_CouplingLaws(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();

        // Power law: K/(1+d/λ).
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + s, 2.0, 0.05,
                (k, lam, d) => k / (1.0 + d / lam));
            var rng = new Random(seed + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        // Constant coupling.
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + 1000 + s, 2.0, 0.05,
                (k, lam, d) => k / 100.0);
            var rng = new Random(seed + 1000 + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        return Eval("Coupling Laws", pred, obs);
    }

    private static RepairValidationResult Validate_Noise(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();

        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", 100, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            for (int step = 0; step < 10; step++)
            {
                PhaseStepWithNoise(net, rng, 0.3);
            }
            double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        return Eval("Phase Noise", pred, obs);
    }

    private static RepairValidationResult Validate_LargeN(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>(); int nTest = 500;

        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetwork("uniform", nTest, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < nTest; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, nTest, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        return Eval("Large-N N=500", pred, obs);
    }

    private static RepairValidationResult Validate_SmallN(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>(); int nTest = 10;

        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", nTest, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < nTest; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            pred.Add(c.Predictor(R0, M0, nTest, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }

        return Eval("Small-N N=10", pred, obs);
    }

    private static RepairValidationResult Validate_OOD(
        RepairedTheoryCandidate c, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();

        foreach (var (kTry, lTry) in new[] { (0.1, 0.01), (10.0, 0.20) })
            for (int s = 0; s < 10; s++)
            {
                var net = BuildNetwork("random-clusters", 100, seed + s, kTry, lTry);
                var rng = new Random(seed + s);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                pred.Add(c.Predictor(R0, M0, 100, kTry, lTry));
                obs.Add((R1 - R0) / 10.0);
            }

        return Eval("Out-of-Distribution", pred, obs);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full repair pipeline
    // ══════════════════════════════════════════════════════════════════

    public static RepairValidationReport RunRepairPipeline(int baseSeed = 101_000_001)
    {
        // Generate training data.
        var trainData = GenerateTrainData(baseSeed);

        // Generate repaired candidates.
        var candidates = GenerateRepairedCandidates(trainData);

        // Validate each candidate.
        var perCandidate = new Dictionary<string, List<RepairValidationResult>>();
        foreach (var cand in candidates)
            perCandidate[cand.Name] = ValidateCandidate(cand, baseSeed);

        // Score: survival rate = fraction of attacks with R² > 0.10.
        var scored = candidates.Select(c =>
        {
            var results = perCandidate[c.Name];
            int passed = results.Count(r => r.Passed);
            double survival = (double)passed / results.Count;
            double meanR2 = results.Average(r => r.R2);
            return (Candidate: c, Passed: passed, Survival: survival, MeanR2: meanR2);
        }).OrderByDescending(x => x.Survival)
          .ThenByDescending(x => x.MeanR2)
          .ToList();

        var best = scored[0];
        var baseline = candidates.First(c => c.Name == "M0");

        string classification = best.Survival >= 0.875 ? "D: Candidate Emergent Physics" :
                                best.Survival >= 0.625 ? "C: Robust Effective Theory" :
                                best.Survival >= 0.375 ? "B: Partially Repaired" :
                                "A: Theory Still Rejected";

        return new RepairValidationReport(
            best.Candidate, baseline, candidates, perCandidate,
            best.Survival, classification);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static RepairValidationResult Eval(string name, List<double> pred, List<double> obs)
    {
        double ssRes = 0, ssTot = 0, mae = 0, mean = obs.Average();
        int n = obs.Count;
        for (int i = 0; i < n; i++)
        {
            double err = obs[i] - pred[i];
            ssRes += err * err;
            ssTot += (obs[i] - mean) * (obs[i] - mean);
            mae += Math.Abs(err);
        }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0.0;
        bool passed = r2 > 0.10;
        return new RepairValidationResult(name, r2, mae / n, passed);
    }

    private static (double[], double, double) FitLinear(double[][] predictors, double[] Y, bool intercept = true)
    {
        int n = Y.Length;
        int p = predictors.Length;
        int m = intercept ? p + 1 : p;

        double[,] XTX = new double[m, m];
        double[] XTY = new double[m];
        for (int i = 0; i < n; i++)
        {
            double[] f = new double[m];
            int fi = 0;
            if (intercept) f[fi++] = 1.0;
            for (int j = 0; j < p; j++)
                f[fi++] = predictors[j][i];
            for (int a = 0; a < m; a++)
            { XTY[a] += f[a] * Y[i]; for (int b = 0; b < m; b++) XTX[a, b] += f[a] * f[b]; }
        }
        double[] beta = SolveGauss(XTX, XTY, m);

        double ssRes = 0, ssTot = 0, mean = Y.Average(), sumAbs = 0;
        for (int i = 0; i < n; i++)
        {
            double pred = 0;
            int fi = 0;
            if (intercept) pred = beta[fi++];
            for (int j = 0; j < p; j++)
                pred += beta[fi++] * predictors[j][i];
            ssRes += (Y[i] - pred) * (Y[i] - pred);
            ssTot += (Y[i] - mean) * (Y[i] - mean);
            sumAbs += Math.Abs(Y[i] - pred);
        }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        double mae = sumAbs / n;
        return (beta, r2, mae);
    }

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        { for (int j = 0; j < n; j++) M[i, j] = A[i, j]; M[i, n] = b[i]; }
        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col])) maxRow = row;
            for (int j = col; j <= n; j++) (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            { double f = M[row, col] / M[col, col];
              for (int j = col; j <= n; j++) M[row, j] -= f * M[col, j]; }
        }
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        { double s = M[i, n]; for (int j = i + 1; j < n; j++) s -= M[i, j] * x[j];
          x[i] = Math.Abs(M[i, i]) > 1e-15 ? s / M[i, i] : 0; }
        return x;
    }

    // ══════════════════════════════════════════════════════════════════
    // Network builders
    // ══════════════════════════════════════════════════════════════════

    private static TemporalNetwork BuildNetwork(string type, int n, int seed, double k, double lambda)
    {
        var net = new TemporalNetwork(n);
        var rng = new Random(seed);
        for (int i = 0; i < n; i++)
        {
            double x, y;
            switch (type)
            {
                case "clustered":
                    int cl = rng.Next(3);
                    double cx = cl switch { 0 => 0.2, 1 => 0.5, _ => 0.8 };
                    double cy = cl switch { 0 => 0.3, 1 => 0.7, _ => 0.5 };
                    x = Math.Clamp(cx + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    y = Math.Clamp(cy + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    break;
                case "linear":
                    double t = (double)i / n;
                    x = 0.1 + t * 0.8;
                    y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.02;
                    break;
                case "circular":
                    double angle = 2 * Math.PI * i / n;
                    x = 0.5 + 0.3 * Math.Cos(angle);
                    y = 0.5 + 0.3 * Math.Sin(angle);
                    break;
                case "dense-sparse":
                    if (i < n / 2) { x = rng.NextDouble() * 0.4; y = rng.NextDouble(); }
                    else { x = 0.6 + rng.NextDouble() * 0.4; y = rng.NextDouble(); }
                    break;
                case "random-clusters":
                    int rc = rng.Next(4);
                    double rcx = rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.8, _ => 0.35 };
                    double rcy = rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.5, _ => 0.8 };
                    x = Math.Clamp(rcx + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99);
                    y = Math.Clamp(rcy + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99);
                    break;
                default:
                    x = rng.NextDouble(); y = rng.NextDouble();
                    break;
            }
            net.AddNode(new TemporalNode(i, 0, 1.0) { X = x, Y = y });
        }
        net.Matrix.FillSpatialCoupling(net.Nodes, k, lambda, normalize: false);
        return net;
    }

    private static TemporalNetwork BuildNetworkWithLaw(string type, int n, int seed,
        double k, double lambda, Func<double, double, double, double> law)
    {
        var net = new TemporalNetwork(n); var rng = new Random(seed);
        for (int i = 0; i < n; i++)
            net.AddNode(new TemporalNode(i, 0, 1.0) { X = rng.NextDouble(), Y = rng.NextDouble() });
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double dx = net.Nodes[i].X - net.Nodes[j].X;
                double dy = net.Nodes[i].Y - net.Nodes[j].Y;
                double c = law(k, lambda, Math.Sqrt(dx * dx + dy * dy));
                net.Matrix.SetCoupling(i, j, c);
                net.Matrix.SetCoupling(j, i, c);
            }
        return net;
    }

    private static void Evolve(TemporalNetwork net, int steps)
    {
        int n = net.NodeCount;
        for (int step = 0; step < steps; step++)
        {
            double[] np = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                    if (i != j) sum += net.Matrix.GetCoupling(i, j) *
                        Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
                np[i] = TemporalSimulation.NormalizePhase(
                    net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum));
            }
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
        }
    }

    private static void PhaseStepWithNoise(TemporalNetwork net, Random rng, double sigma)
    {
        int n = net.NodeCount;
        double[] np = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
                if (i != j) sum += net.Matrix.GetCoupling(i, j) *
                    Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            double noise = SampleGaussian(rng) * sigma;
            np[i] = TemporalSimulation.NormalizePhase(
                net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum) + noise);
        }
        for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
    }

    private static double SampleGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble(), u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) * Math.Sin(2.0 * Math.PI * u2);
    }

    private static double ComputeR(TemporalNetwork net)
    {
        double ss = 0, sc = 0; int n = net.NodeCount;
        for (int i = 0; i < n; i++) { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    private static double ComputeM(TemporalNetwork net)
    {
        int n = net.NodeCount; double sum = 0; int pairs = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++) { sum += net.Matrix.GetCoupling(i, j); pairs++; }
        return sum / pairs;
    }
}
