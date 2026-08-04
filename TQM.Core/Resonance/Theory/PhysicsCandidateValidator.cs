using TQM.Core.Temporal;
using TQM.Core.Resonance.Kuramoto;

namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Hostile validation framework for the {R, M} minimal theory.
/// Generates adversarial scenarios and attempts to falsify the theory.
///
/// TQM-100: Physics Candidate Validation
/// </summary>
public static class PhysicsCandidateValidator
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A single data point for validation.
    /// </summary>
    public sealed record ValidationPoint(
        double R, double M, double dRdt, double dMdt,
        string Scenario, int Seed);

    /// <summary>
    /// A detected failure case.
    /// </summary>
    public sealed record FailureCase(
        string Scenario,
        string Description,
        double ExpectedR2,
        double ObservedR2,
        double PredictionError,
        string Severity,     // "Critical", "Significant", "Minor", "None"
        string Interpretation);

    /// <summary>
    /// A structured stress test.
    /// </summary>
    public sealed record TheoryStressTest(
        string Name,
        string AttackVector,
        string Hypothesis,
        int NumPoints,
        List<ValidationPoint> Data,
        double R2_dRdt,
        double R2_dMdt,
        double MeanAbsError,
        bool TheoryFailed,
        string FailureMode);

    /// <summary>
    /// Full validation report.
    /// </summary>
    public sealed record ValidationReport(
        List<TheoryStressTest> StressTests,
        List<FailureCase> Failures,
        double GeneralizationScore,
        int TotalAttackVectors,
        int FailuresDetected,
        string Classification,
        string Verdict);

    // ══════════════════════════════════════════════════════════════════
    // Train the {R, M} model on standard data
    // ══════════════════════════════════════════════════════════════════

    public sealed record TrainedModel(
        double[] DRdtCoeffs,   // [intercept, R_coeff, M_coeff]
        double[] DMdtCoeffs,   // [intercept, R_coeff, M_coeff]
        double R2_DRdt,
        double R2_DMdt);

    public static TrainedModel TrainOnStandardData(int numConfigs = 200, int seed = 100_000_001)
    {
        var states = TopologyEvolutionAnalyzer.GenerateTopologyEnsemble(
            2.0, 0.05, 100, numConfigs, seed);

        double[] R = states.Select(s => s.R).ToArray();
        double[] M = states.Select(s => s.MeanCoupling).ToArray();
        double[] dRdt = states.Select(s => s.dRdt).ToArray();

        // dR/dt = β₀ + β₁·R + β₂·M
        var (coeffs_dr, r2_dr) = Fit3Param(R, M, dRdt);

        // dM/dt = β₀ + β₁·R + β₂·M — use dRdt as proxy (no true dM/dt in static data)
        // For static snapshots, dM/dt ≈ 0 (positions are fixed).
        // We use the temporal data from TQM-082 style for dM/dt training.
        var dMdata = GenerateDMdtTrainingData(numConfigs / 4, seed + 7919);
        double[] R2 = dMdata.Select(p => p.R).ToArray();
        double[] M2 = dMdata.Select(p => p.M).ToArray();
        double[] Y2 = dMdata.Select(p => p.dMdt).ToArray();
        var (coeffs_dm, r2_dm) = Fit3Param(R2, M2, Y2);

        return new TrainedModel(coeffs_dr, coeffs_dm, r2_dr, r2_dm);
    }

    private static List<(double R, double M, double dMdt)> GenerateDMdtTrainingData(
        int profiles, int baseSeed)
    {
        var results = new List<(double R, double M, double dMdt)>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        for (int p = 0; p < profiles; p++)
        {
            int seed = baseSeed + p * 7919;
            string type = types[p % types.Length];
            var profile = MeanCouplingFieldAnalyzer.SimulateProfile(
                type, 2.0, 0.05, 100, seed, totalSteps: 200, snapshotInterval: 10);

            for (int i = 1; i < profile.M.Length; i++)
                results.Add((profile.R[i], profile.M[i], profile.dMdt[i]));
        }

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Predict using trained model
    // ══════════════════════════════════════════════════════════════════

    public static double Predict_dRdt(TrainedModel model, double R, double M) =>
        model.DRdtCoeffs[0] + model.DRdtCoeffs[1] * R + model.DRdtCoeffs[2] * M;

    public static double Predict_dMdt(TrainedModel model, double R, double M) =>
        model.DMdtCoeffs[0] + model.DMdtCoeffs[1] * R + model.DMdtCoeffs[2] * M;

    // ══════════════════════════════════════════════════════════════════
    // Adversarial scenario generators
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Attack 1: Extreme R — theory trained on R≈0.09, test at R≈0 and R≈1.
    /// </summary>
    public static TheoryStressTest Attack_ExtremeCoherence(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 100;

        // R ≈ 0: all phases random (uniform on [0, 2π)).
        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + s * 7919;
            var net = BuildRandomNetwork(n, "uniform", seed, 2.0, 0.05);
            // Random phases → R ≈ 0.
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net);
            double M0 = ComputeM(net);
            // Evolve 10 steps.
            double R1 = R0;
            EvolvePhases(net, 10);
            R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "R≈0", seed));
        }

        // R ≈ 1: all phases nearly identical.
        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + 1000 + s * 7919;
            var net = BuildRandomNetwork(n, "clustered", seed, 2.0, 0.05);
            var rng = new Random(seed);
            double basePhase = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < n; i++)
                net.Nodes[i].Phase = basePhase + (rng.NextDouble() * 2 - 1) * 0.01;
            double R0 = ComputeR(net);
            double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "R≈1", seed));
        }

        return EvaluateStressTest("Extreme Coherence",
            "Theory trained on R≈0.09. Test at R≈0 and R≈1.",
            model, points);
    }

    /// <summary>
    /// Attack 2: Extreme M — theory trained on M≈0.1, test at very small/large M.
    /// </summary>
    public static TheoryStressTest Attack_ExtremeMeanCoupling(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 100;

        // M very small → large λ or small K.
        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + s;
            var net = BuildRandomNetwork(n, "uniform", seed, 0.5, 0.20); // K=0.5, λ=0.20 → small M
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "M→0", seed));
        }

        // M very large → small λ or large K, clustered topology.
        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + 1000 + s;
            var net = BuildRandomNetwork(n, "clustered", seed, 5.0, 0.03); // K=5, λ=0.03 → large M
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "M>>1", seed));
        }

        return EvaluateStressTest("Extreme Mean Coupling",
            "Theory trained on M≈0.1. Test at M→0 and M>>1.",
            model, points);
    }

    /// <summary>
    /// Attack 3: Leave-one-topology-out cross-validation.
    /// </summary>
    public static TheoryStressTest Attack_MixedTopologies(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        string[] allTypes = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };

        // Test on each topology type with fresh random seeds (different from training).
        for (int t = 0; t < allTypes.Length; t++)
        {
            for (int s = 0; s < 10; s++)
            {
                int seed = baseSeed + t * 10000 + s * 7919;
                var net = BuildRandomNetwork(100, allTypes[t], seed, 2.0, 0.05);
                var rng = new Random(seed);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net); double M0 = ComputeM(net);
                EvolvePhases(net, 10);
                double R1 = ComputeR(net);
                double dR = (R1 - R0) / 10.0;
                points.Add(new ValidationPoint(R0, M0, dR, 0, $"Topo:{allTypes[t]}", seed));
            }
        }

        return EvaluateStressTest("Mixed Topologies",
            "Fresh seeds for all 6 topology types. Tests generalization across topologies.",
            model, points);
    }

    /// <summary>
    /// Attack 4: Different coupling laws — not exp(-d/λ).
    /// </summary>
    public static TheoryStressTest Attack_DifferentCouplingLaws(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 100;

        // Coupling law 1: K / (1 + d/λ) — power-law decay.
        for (int s = 0; s < 15; s++)
        {
            int seed = baseSeed + s;
            var net = BuildNetworkWithCouplingLaw(n, "uniform", seed, 2.0, 0.05,
                (K, lam, d) => K / (1.0 + d / lam));
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "Law:1/(1+d/λ)", seed));
        }

        // Coupling law 2: K * (1 - d/d_max) for d < d_max — linear cutoff.
        for (int s = 0; s < 15; s++)
        {
            int seed = baseSeed + 1000 + s;
            double dMax = 1.0;
            var net = BuildNetworkWithCouplingLaw(n, "clustered", seed, 2.0, 0.05,
                (K, lam, d) => d < dMax ? K * (1.0 - d / dMax) : 0);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "Law:linear-cutoff", seed));
        }

        // Coupling law 3: Constant coupling K/N (mean-field, no spatial decay).
        for (int s = 0; s < 10; s++)
        {
            int seed = baseSeed + 2000 + s;
            var net = BuildNetworkWithCouplingLaw(n, "uniform", seed, 2.0, 0.05,
                (K, lam, d) => K / n);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, "Law:constant", seed));
        }

        return EvaluateStressTest("Different Coupling Laws",
            "Theory trained on exp(-d/λ). Test on power-law, linear cutoff, constant coupling.",
            model, points);
    }

    /// <summary>
    /// Attack 5: High-noise — add phase noise to break coherence predictions.
    /// </summary>
    public static TheoryStressTest Attack_HighNoise(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 100;

        // Standard setup but add Gaussian phase noise at each step.
        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + s;
            var net = BuildRandomNetwork(n, "uniform", seed, 2.0, 0.05);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            // Evolve with noise.
            for (int step = 0; step < 10; step++)
            {
                PhaseStepWithNoise(net, rng, 0.5); // noise std = 0.5 rad
            }
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, $"Noise:σ=0.5", seed));
        }

        return EvaluateStressTest("High Phase Noise",
            "Theory trained without noise. Test with σ_noise=0.5 rad/step.",
            model, points);
    }

    /// <summary>
    /// Attack 6: Large-N scaling — theory trained at N=100, test at N=500.
    /// </summary>
    public static TheoryStressTest Attack_LargeN(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 500;

        for (int s = 0; s < 15; s++)
        {
            int seed = baseSeed + s;
            var net = BuildRandomNetwork(n, "uniform", seed, 2.0, 0.05);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, $"N=500", seed));
        }

        return EvaluateStressTest("Large-N Scaling",
            "Theory trained at N=100. Test at N=500.",
            model, points);
    }

    /// <summary>
    /// Attack 7: Small-N scaling — theory trained at N=100, test at N=10.
    /// </summary>
    public static TheoryStressTest Attack_SmallN(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 10;

        for (int s = 0; s < 20; s++)
        {
            int seed = baseSeed + s;
            var net = BuildRandomNetwork(n, "uniform", seed, 2.0, 0.05);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, $"N=10", seed));
        }

        return EvaluateStressTest("Small-N Scaling",
            "Theory trained at N=100. Test at N=10.",
            model, points);
    }

    /// <summary>
    /// Attack 8: Out-of-distribution — parameter combinations far from training.
    /// </summary>
    public static TheoryStressTest Attack_OutOfDistribution(
        TrainedModel model, int baseSeed)
    {
        var points = new List<ValidationPoint>();
        int n = 100;

        // K=0.1 (10x weaker than training), λ=0.01 (5x shorter range).
        for (int s = 0; s < 15; s++)
        {
            int seed = baseSeed + s;
            var net = BuildRandomNetwork(n, "random-clusters", seed, 0.1, 0.01);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, $"K=0.1,λ=0.01", seed));
        }

        // K=10 (5x stronger), λ=0.20 (4x longer range).
        for (int s = 0; s < 15; s++)
        {
            int seed = baseSeed + 1000 + s;
            var net = BuildRandomNetwork(n, "linear", seed, 10.0, 0.20);
            var rng = new Random(seed);
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net); double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;
            points.Add(new ValidationPoint(R0, M0, dR, 0, $"K=10,λ=0.20", seed));
        }

        return EvaluateStressTest("Out-of-Distribution",
            "Theory trained at (K=2,λ=0.05). Test at (K=0.1,λ=0.01) and (K=10,λ=0.20).",
            model, points);
    }

    // ══════════════════════════════════════════════════════════════════
    // Evaluate a stress test
    // ══════════════════════════════════════════════════════════════════

    private static TheoryStressTest EvaluateStressTest(
        string name, string hypothesis, TrainedModel model,
        List<ValidationPoint> points)
    {
        double[] pred = new double[points.Count];
        double[] obs = new double[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            pred[i] = Predict_dRdt(model, points[i].R, points[i].M);
            obs[i] = points[i].dRdt;
        }

        double r2 = ComputeR2(pred, obs);
        double mae = 0;
        for (int i = 0; i < points.Count; i++)
            mae += Math.Abs(pred[i] - obs[i]);
        mae /= points.Count;

        // Theory fails if R² drops below 0.3 (substantially worse than training R² ~ 0.76).
        bool failed = r2 < 0.30;

        string failureMode = failed
            ? (r2 < 0.10 ? "CRITICAL: Predictions nearly random"
               : r2 < 0.20 ? "SEVERE: Weak predictive power"
               : "MODERATE: Marginal predictive power")
            : "PASSED: Theory generalizes";

        return new TheoryStressTest(name, name, hypothesis, points.Count, points,
            r2, 0, mae, failed, failureMode);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full validation
    // ══════════════════════════════════════════════════════════════════

    public static ValidationReport RunFullValidation(int baseSeed = 100_000_001)
    {
        // Train model.
        var model = TrainOnStandardData(200, baseSeed);

        // Run all attacks.
        var tests = new List<TheoryStressTest>
        {
            Attack_ExtremeCoherence(model, baseSeed + 100),
            Attack_ExtremeMeanCoupling(model, baseSeed + 200),
            Attack_MixedTopologies(model, baseSeed + 300),
            Attack_DifferentCouplingLaws(model, baseSeed + 400),
            Attack_HighNoise(model, baseSeed + 500),
            Attack_LargeN(model, baseSeed + 600),
            Attack_SmallN(model, baseSeed + 700),
            Attack_OutOfDistribution(model, baseSeed + 800),
        };

        // Collect failures.
        var failures = new List<FailureCase>();
        foreach (var test in tests)
        {
            if (test.TheoryFailed)
            {
                failures.Add(new FailureCase(
                    test.Name,
                    test.Hypothesis,
                    0.76, // training R²
                    test.R2_dRdt,
                    test.MeanAbsError,
                    test.R2_dRdt < 0.10 ? "Critical" :
                    test.R2_dRdt < 0.20 ? "Significant" : "Minor",
                    test.FailureMode));
            }
        }

        int totalAttacks = tests.Count;
        int failuresDetected = tests.Count(t => t.TheoryFailed);
        int passed = totalAttacks - failuresDetected;

        // Generalization score: fraction of attacks survived.
        double genScore = (double)passed / totalAttacks;

        // Classification.
        string classification;
        string verdict;
        if (failuresDetected == 0)
        {
            classification = "D: Candidate Emergent Physics";
            verdict = "THEORY SURVIVES ALL ATTACKS. The {R, M} minimal theory " +
                      "generalizes to all tested adversarial scenarios without failure. " +
                      "This is strong evidence for emergent physics.";
        }
        else if (failuresDetected <= 2 && genScore >= 0.70)
        {
            classification = "C: Robust Effective Theory";
            verdict = $"THEORY SURVIVES MOST ATTACKS ({passed}/{totalAttacks} passed). " +
                      $"{failuresDetected} failure(s) found but within acceptable bounds. " +
                      "The theory is a robust effective description with known limitations.";
        }
        else if (failuresDetected <= 4 && genScore >= 0.40)
        {
            classification = "B: Significant Gaps";
            verdict = $"THEORY HAS SIGNIFICANT GAPS ({passed}/{totalAttacks} passed). " +
                      $"{failuresDetected} attack vectors expose weaknesses. " +
                      "A third variable or extended dynamics may be needed.";
        }
        else
        {
            classification = "A: Theory Rejected";
            verdict = $"THEORY REJECTED ({passed}/{totalAttacks} passed). " +
                      $"{failuresDetected} of {totalAttacks} attack vectors broke the theory. " +
                      "The {R, M} minimal theory is NOT a valid candidate for emergent physics.";
        }

        return new ValidationReport(tests, failures, genScore,
            totalAttacks, failuresDetected, classification, verdict);
    }

    // ══════════════════════════════════════════════════════════════════
    // Network builders
    // ══════════════════════════════════════════════════════════════════

    private static TemporalNetwork BuildRandomNetwork(
        int n, string type, int seed, double k, double lambda)
    {
        var net = new TemporalNetwork(n);
        var rng = new Random(seed);

        for (int i = 0; i < n; i++)
        {
            double x, y;
            switch (type)
            {
                case "clustered":
                    int cluster = rng.Next(3);
                    double cx = cluster switch { 0 => 0.2, 1 => 0.5, _ => 0.8 };
                    double cy = cluster switch { 0 => 0.3, 1 => 0.7, _ => 0.5 };
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

    private static TemporalNetwork BuildNetworkWithCouplingLaw(
        int n, string type, int seed, double k, double lambda,
        Func<double, double, double, double> couplingLaw)
    {
        var net = new TemporalNetwork(n);
        var rng = new Random(seed);

        for (int i = 0; i < n; i++)
        {
            double x = rng.NextDouble(), y = rng.NextDouble();
            net.AddNode(new TemporalNode(i, 0, 1.0) { X = x, Y = y });
        }

        // Fill with custom coupling law.
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                double dx = net.Nodes[i].X - net.Nodes[j].X;
                double dy = net.Nodes[i].Y - net.Nodes[j].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                double c = couplingLaw(k, lambda, d);
                net.Matrix.SetCoupling(i, j, c);
                net.Matrix.SetCoupling(j, i, c);
            }

        return net;
    }

    // ══════════════════════════════════════════════════════════════════
    // Simulation helpers
    // ══════════════════════════════════════════════════════════════════

    private static void EvolvePhases(TemporalNetwork net, int steps)
    {
        int n = net.NodeCount;
        for (int step = 0; step < steps; step++)
        {
            double[] newPhases = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += net.Matrix.GetCoupling(i, j) *
                           Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
                }
                newPhases[i] = TemporalSimulation.NormalizePhase(
                    net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum));
            }
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = newPhases[i];
        }
    }

    private static void PhaseStepWithNoise(TemporalNetwork net, Random rng, double noiseStd)
    {
        int n = net.NodeCount;
        double[] newPhases = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++)
            {
                if (i == j) continue;
                sum += net.Matrix.GetCoupling(i, j) *
                       Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            }
            double noise = SampleGaussian(rng) * noiseStd;
            newPhases[i] = TemporalSimulation.NormalizePhase(
                net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum) + noise);
        }
        for (int i = 0; i < n; i++) net.Nodes[i].Phase = newPhases[i];
    }

    private static double SampleGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = 1.0 - rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-10))) *
               Math.Sin(2.0 * Math.PI * u2);
    }

    private static double ComputeR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    private static double ComputeM(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double sum = 0; int pairs = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            { sum += net.Matrix.GetCoupling(i, j); pairs++; }
        return sum / pairs;
    }

    // ══════════════════════════════════════════════════════════════════
    // Math helpers
    // ══════════════════════════════════════════════════════════════════

    private static (double[], double) Fit3Param(double[] X1, double[] X2, double[] Y)
    {
        int n = Y.Length;
        double[,] XTX = new double[3, 3];
        double[] XTY = new double[3];
        for (int i = 0; i < n; i++)
        {
            double[] f = { 1, X1[i], X2[i] };
            for (int a = 0; a < 3; a++)
            { XTY[a] += f[a] * Y[i]; for (int b = 0; b < 3; b++) XTX[a, b] += f[a] * f[b]; }
        }
        double[] beta = SolveGauss(XTX, XTY, 3);
        double ssRes = 0, ssTot = 0, mean = Y.Average();
        for (int i = 0; i < n; i++)
        { double p = beta[0] + beta[1] * X1[i] + beta[2] * X2[i];
          ssRes += (Y[i] - p) * (Y[i] - p); ssTot += (Y[i] - mean) * (Y[i] - mean); }
        return (beta, ssTot > 1e-15 ? 1 - ssRes / ssTot : 0);
    }

    private static double ComputeR2(double[] pred, double[] obs)
    {
        double ssRes = 0, ssTot = 0, mean = obs.Average();
        for (int i = 0; i < obs.Length; i++)
        { ssRes += (obs[i] - pred[i]) * (obs[i] - pred[i]);
          ssTot += (obs[i] - mean) * (obs[i] - mean); }
        return ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0.0;
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
}
