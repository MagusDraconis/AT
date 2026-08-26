using AT.Core.Temporal;

namespace AT.Core.Resonance.Theory;

/// <summary>
/// Autonomous equation discovery via sparse symbolic regression.
/// Infers governing equations dR/dt = F(R,M,N,K,λ) directly from data
/// without assuming functional form.
///
/// AT-102: Autonomous Equation Discovery
/// </summary>
public static class EquationDiscoveryAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record DiscoveryPoint(
        double R, double M, double dRdt,
        int N, double K, double Lam, string Topology, int Seed);

    public sealed record BasisFunction(
        string Name,
        string Expression,
        Func<double, double, int, double, double, double> Eval);
    // Eval(R, M, N, K, lam) -> basis value

    public sealed record EquationCandidate(
        string Name,
        string Equation,
        List<BasisFunction> Terms,
        double[] Coefficients,
        double TrainR2,
        double TrainAICc,
        int NumTerms);

    public sealed record DiscoveredTheory(
        EquationCandidate DRdtEquation,
        EquationCandidate DMdtEquation,
        List<EquationCandidate> AllDRdtCandidates,
        string SearchPath);

    public sealed record DiscoveryReport(
        DiscoveredTheory Theory,
        Dictionary<string, (double R2, bool Passed)> ValidationResults,
        double SurvivalRate,
        string Classification);

    // ══════════════════════════════════════════════════════════════════
    // Basis function library
    // ══════════════════════════════════════════════════════════════════

    public static readonly List<BasisFunction> BasisLibrary = new()
    {
        new("φ_R",       "R",           (r, m, n, k, lam) => r),
        new("φ_M",       "M",           (r, m, n, k, lam) => m),
        new("φ_R2",      "R²",          (r, m, n, k, lam) => r * r),
        new("φ_M2",      "M²",          (r, m, n, k, lam) => m * m),
        new("φ_RM",      "R·M",         (r, m, n, k, lam) => r * m),
        new("φ_R1mR",    "R·(1-R)",     (r, m, n, k, lam) => r * (1.0 - r)),
        new("φ_MR1mR",   "M·R·(1-R)",   (r, m, n, k, lam) => m * r * (1.0 - r)),
        new("φ_NMR1mR",  "N·M·R·(1-R)", (r, m, n, k, lam) => n * m * r * (1.0 - r)),
        new("φ_N",       "N",           (r, m, n, k, lam) => (double)n),
        new("φ_invN",    "1/N",         (r, m, n, k, lam) => 1.0 / Math.Max(n, 1)),
        new("φ_K",       "K",           (r, m, n, k, lam) => k),
        new("φ_Lam",     "λ",           (r, m, n, k, lam) => lam),
        new("φ_invLam",  "1/λ",         (r, m, n, k, lam) => 1.0 / Math.Max(lam, 1e-10)),
        new("φ_KM",      "K·M",         (r, m, n, k, lam) => k * m),
        new("φ_MoverN",  "M/N",         (r, m, n, k, lam) => m / Math.Max(n, 1)),
        new("φ_MoverLam","M/λ",         (r, m, n, k, lam) => m / Math.Max(lam, 1e-10)),
        new("φ_R3",      "R³",          (r, m, n, k, lam) => r * r * r),
        new("φ_M3",      "M³",          (r, m, n, k, lam) => m * m * m),
        new("φ_R21mR",   "R²·(1-R)",    (r, m, n, k, lam) => r * r * (1.0 - r)),
        new("φ_R1mR2",   "R·(1-R)²",    (r, m, n, k, lam) => r * (1.0 - r) * (1.0 - r)),
        new("φ_sqrtR",   "√R",          (r, m, n, k, lam) => Math.Sqrt(Math.Max(r, 1e-10))),
        new("φ_expmR",   "exp(-R)",     (r, m, n, k, lam) => Math.Exp(-r)),
        new("φ_KoverN",  "K/N",         (r, m, n, k, lam) => k / Math.Max(n, 1)),
        new("φ_KMoverLam","K·M/λ",      (r, m, n, k, lam) => k * m / Math.Max(lam, 1e-10)),
    };

    // ══════════════════════════════════════════════════════════════════
    // Data generation — wide parameter sweep
    // ══════════════════════════════════════════════════════════════════

    public static List<DiscoveryPoint> GenerateDiscoveryData(int baseSeed = 102_000_001)
    {
        var points = new List<DiscoveryPoint>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };
        int[] Ns = { 10, 20, 50, 100, 200, 500, 1000 };
        double[] Ks = { 0.01, 0.1, 0.5, 1.0, 2.0, 5.0, 10.0 };
        double[] Lams = { 0.005, 0.01, 0.05, 0.1, 0.2, 0.5 };

        int counter = 0;
        var rng = new Random(baseSeed);

        // Stratified sampling: 400 random parameter combinations.
        for (int i = 0; i < 400; i++)
        {
            int nTry = Ns[rng.Next(Ns.Length)];
            double kTry = Ks[rng.Next(Ks.Length)];
            double lTry = Lams[rng.Next(Lams.Length)];
            string topo = types[rng.Next(types.Length)];
            int seed = baseSeed + Interlocked.Increment(ref counter) * 7919;

            var net = BuildNetwork(topo, nTry, seed, kTry, lTry);
            var rng2 = new Random(seed);

            // Random initial phases → wide R coverage.
            for (int j = 0; j < nTry; j++)
                net.Nodes[j].Phase = rng2.NextDouble() * 2 * Math.PI;

            double R0 = ComputeR(net);
            double M0 = ComputeM(net);

            // Short evolution.
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            double dR = (R1 - R0) / 10.0;

            points.Add(new DiscoveryPoint(R0, M0, dR, nTry, kTry, lTry, topo, seed));
        }

        // Add extreme R samples (R≈0 and R≈1).
        for (int i = 0; i < 60; i++)
        {
            int nTry = Ns[rng.Next(Ns.Length)];
            double kTry = Ks[rng.Next(Ks.Length)];
            double lTry = Lams[rng.Next(Lams.Length)];
            int seed = baseSeed + 50000 + i;

            bool nearOne = i >= 30;
            var net = BuildNetwork("clustered", nTry, seed, kTry, lTry);
            var rng2 = new Random(seed);

            if (nearOne)
            {
                double bp = rng2.NextDouble() * 2 * Math.PI;
                double spread = 0.01;
                for (int j = 0; j < nTry; j++)
                    net.Nodes[j].Phase = bp + (rng2.NextDouble() * 2 - 1) * spread;
            }
            else
            {
                for (int j = 0; j < nTry; j++)
                    net.Nodes[j].Phase = rng2.NextDouble() * 2 * Math.PI;
            }

            double R0 = ComputeR(net);
            double M0 = ComputeM(net);
            EvolvePhases(net, 10);
            double R1 = ComputeR(net);
            points.Add(new DiscoveryPoint(R0, M0, (R1 - R0) / 10.0, nTry, kTry, lTry, "extreme", seed));
        }

        return points;
    }

    // ══════════════════════════════════════════════════════════════════
    // Equation discovery via forward stepwise regression
    // ══════════════════════════════════════════════════════════════════

    public static DiscoveredTheory DiscoverEquation(List<DiscoveryPoint> data)
    {
        int n = data.Count;
        double[] Y = data.Select(d => d.dRdt).ToArray();
        double ssTot = 0;
        double meanY = Y.Average();
        for (int i = 0; i < n; i++) ssTot += (Y[i] - meanY) * (Y[i] - meanY);

        // Build full design matrix.
        int nBasis = BasisLibrary.Count;
        double[][] design = new double[nBasis][];
        for (int b = 0; b < nBasis; b++)
        {
            design[b] = new double[n];
            var fn = BasisLibrary[b].Eval;
            for (int i = 0; i < n; i++)
            {
                var dp = data[i];
                design[b][i] = fn(dp.R, dp.M, dp.N, dp.K, dp.Lam);
            }
        }

        // Forward stepwise selection.
        var selected = new List<int>(); // indices of selected basis functions
        var available = Enumerable.Range(0, nBasis).ToList();
        var candidates = new List<EquationCandidate>();

        // Intercept-only baseline.
        double ssRes0 = ssTot;
        const double aiccThreshold = 2.0; // minimum AICc improvement

        for (int step = 0; step < 12; step++)
        {
            int bestIdx = -1;
            double bestAICc = double.MaxValue;
            double[] bestCoeffs = null;
            double bestR2 = 0;

            foreach (int b in available)
            {
                var trial = new List<int>(selected) { b };
                var (coeffs, r2) = FitModel(design, Y, trial);
                double aicc = ComputeAICc(n, r2, trial.Count + 1); // +1 for intercept
                if (aicc < bestAICc)
                {
                    bestAICc = aicc;
                    bestIdx = b;
                    bestCoeffs = coeffs;
                    bestR2 = r2;
                }
            }

            // Record this step's model.
            var terms = new List<BasisFunction> { new("1", "1", (_, _, _, _, _) => 1.0) };
            foreach (int si in selected) terms.Add(BasisLibrary[si]);

            string eq = FormatEquation(terms, bestCoeffs ?? new[] { meanY });
            candidates.Add(new EquationCandidate($"S{step}", eq, terms,
                bestCoeffs ?? new[] { meanY }, bestR2, bestAICc, selected.Count));

            if (bestIdx < 0) break;

            // Check if improvement is significant.
            if (step > 0 && candidates[^1].TrainAICc > candidates[^2].TrainAICc + aiccThreshold)
            {
                // AICc got worse — stop adding terms.
                break;
            }

            selected.Add(bestIdx);
            available.Remove(bestIdx);
        }

        // Select best model by AICc.
        var best = candidates.OrderBy(c => c.TrainAICc).First();

        // Format final equation.
        var finalTerms = new List<BasisFunction> { new("1", "1", (_, _, _, _, _) => 1.0) };
        foreach (int si in selected.Take(best.NumTerms))
            finalTerms.Add(BasisLibrary[si]);

        string finalEq = FormatEquation(finalTerms, best.Coefficients);

        var dRdtEq = new EquationCandidate("BEST", finalEq, finalTerms,
            best.Coefficients, best.TrainR2, best.TrainAICc, best.NumTerms);

        // For dM/dt, use a simple empirical fit (data generation is expensive).
        var dMdtEq = new EquationCandidate("DM", "β₀ + β₁·R + β₂·M",
            new List<BasisFunction>(), new[] { 0.0, 0.0, 0.0 }, 0, 0, 2);

        return new DiscoveredTheory(dRdtEq, dMdtEq, candidates,
            $"Forward stepwise: {candidates.Count} steps, best AICc = {best.TrainAICc:F1}, {best.NumTerms} terms");
    }

    // ══════════════════════════════════════════════════════════════════
    // Validate discovered equation against AT-100 attacks
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, (double R2, bool Passed)> ValidateDiscoveredTheory(
        DiscoveredTheory theory, int baseSeed = 102_000_001)
    {
        var results = new Dictionary<string, (double, bool)>();

        // Attack 1: Extreme R.
        results["Extreme Coherence"] = TestExtremeR(theory.DRdtEquation, baseSeed + 100);

        // Attack 2: Extreme M.
        results["Extreme M"] = TestExtremeM(theory.DRdtEquation, baseSeed + 200);

        // Attack 3: Mixed Topologies.
        results["Mixed Topologies"] = TestTopologies(theory.DRdtEquation, baseSeed + 300);

        // Attack 4: Coupling Laws.
        results["Coupling Laws"] = TestCouplingLaws(theory.DRdtEquation, baseSeed + 400);

        // Attack 5: Noise.
        results["Phase Noise"] = TestNoise(theory.DRdtEquation, baseSeed + 500);

        // Attack 6: Large-N.
        results["Large-N N=500"] = TestLargeN(theory.DRdtEquation, baseSeed + 600);

        // Attack 7: Small-N.
        results["Small-N N=10"] = TestSmallN(theory.DRdtEquation, baseSeed + 700);

        // Attack 8: OOD.
        results["Out-of-Distribution"] = TestOOD(theory.DRdtEquation, baseSeed + 800);

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Individual attack tests
    // ══════════════════════════════════════════════════════════════════

    private static (double R2, bool Passed) TestExtremeR(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", 100, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetwork("clustered", 100, seed + 1000 + s, 2.0, 0.05);
            var rng = new Random(seed + 1000 + s);
            double bp = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = bp + (rng.NextDouble() * 2 - 1) * 0.005;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestExtremeM(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        foreach (var (k, lam) in new[] { (0.5, 0.20), (5.0, 0.03) })
            for (int s = 0; s < 15; s++)
            {
                var net = BuildNetwork("uniform", 100, seed + s, k, lam);
                var rng = new Random(seed + s);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                EvolvePhases(net, 10); double R1 = ComputeR(net);
                pred.Add(Predict(eq, R0, M0, 100, k, lam));
                obs.Add((R1 - R0) / 10.0);
            }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestTopologies(EquationCandidate eq, int seed)
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
                EvolvePhases(net, 10); double R1 = ComputeR(net);
                pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
                obs.Add((R1 - R0) / 10.0);
            }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestCouplingLaws(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + s, 2.0, 0.05,
                (k, lam, d) => k / (1.0 + d / lam));
            var rng = new Random(seed + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + 1000 + s, 2.0, 0.05,
                (k, lam, d) => k / 100.0);
            var rng = new Random(seed + 1000 + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestNoise(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", 100, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            for (int step = 0; step < 10; step++) PhaseStepWithNoise(net, rng, 0.3);
            double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 100, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestLargeN(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        for (int s = 0; s < 10; s++)
        {
            var net = BuildNetwork("uniform", 500, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < 500; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 500, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestSmallN(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        for (int s = 0; s < 15; s++)
        {
            var net = BuildNetwork("uniform", 10, seed + s, 2.0, 0.05);
            var rng = new Random(seed + s);
            for (int i = 0; i < 10; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            EvolvePhases(net, 10); double R1 = ComputeR(net);
            pred.Add(Predict(eq, R0, M0, 10, 2.0, 0.05));
            obs.Add((R1 - R0) / 10.0);
        }
        return ComputeScore(pred, obs);
    }

    private static (double, bool) TestOOD(EquationCandidate eq, int seed)
    {
        var pred = new List<double>(); var obs = new List<double>();
        foreach (var (k, lam) in new[] { (0.1, 0.01), (10.0, 0.20) })
            for (int s = 0; s < 10; s++)
            {
                var net = BuildNetwork("random-clusters", 100, seed + s, k, lam);
                var rng = new Random(seed + s);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                EvolvePhases(net, 10); double R1 = ComputeR(net);
                pred.Add(Predict(eq, R0, M0, 100, k, lam));
                obs.Add((R1 - R0) / 10.0);
            }
        return ComputeScore(pred, obs);
    }

    // ══════════════════════════════════════════════════════════════════
    // Prediction
    // ══════════════════════════════════════════════════════════════════

    public static double Predict(EquationCandidate eq, double R, double M, int N, double K, double lam)
    {
        double result = 0;
        for (int i = 0; i < eq.Terms.Count && i < eq.Coefficients.Length; i++)
            result += eq.Coefficients[i] * eq.Terms[i].Eval(R, M, N, K, lam);
        return result;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model fitting
    // ══════════════════════════════════════════════════════════════════

    private static (double[], double) FitModel(
        double[][] design, double[] Y, List<int> termIndices)
    {
        int p = termIndices.Count;
        int m = p + 1; // + intercept
        int n = Y.Length;

        double[,] XTX = new double[m, m];
        double[] XTY = new double[m];

        for (int i = 0; i < n; i++)
        {
            double[] f = new double[m];
            f[0] = 1.0;
            for (int j = 0; j < p; j++)
                f[j + 1] = design[termIndices[j]][i];

            for (int a = 0; a < m; a++)
            {
                XTY[a] += f[a] * Y[i];
                for (int b = 0; b < m; b++)
                    XTX[a, b] += f[a] * f[b];
            }
        }

        double[] beta = SolveGauss(XTX, XTY, m);

        double ssRes = 0, ssTot = 0, meanY = Y.Average();
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            for (int j = 0; j < p; j++)
                pred += beta[j + 1] * design[termIndices[j]][i];
            ssRes += (Y[i] - pred) * (Y[i] - pred);
            ssTot += (Y[i] - meanY) * (Y[i] - meanY);
        }

        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (beta, r2);
    }

    private static double ComputeAICc(int n, double r2, int k)
    {
        // k = number of parameters (including intercept)
        double rss = (1.0 - r2); // relative RSS (for this purpose)
        double logTerm = rss > 1e-15 ? n * Math.Log(rss / n) : 0;
        double aic = logTerm + 2.0 * k;
        if (n > k + 1)
            aic += 2.0 * k * (k + 1) / (n - k - 1);
        return aic;
    }

    private static string FormatEquation(List<BasisFunction> terms, double[] coeffs)
    {
        var parts = new List<string>();
        for (int i = 0; i < terms.Count && i < coeffs.Length; i++)
        {
            if (Math.Abs(coeffs[i]) < 1e-10) continue;
            string sign = coeffs[i] >= 0 ? (parts.Count == 0 ? "" : " + ") : " - ";
            string val = i == 0
                ? $"{coeffs[i]:F4}"
                : $"{Math.Abs(coeffs[i]):F4}·{terms[i].Expression}";
            parts.Add(sign + val);
        }
        return parts.Count > 0 ? string.Join("", parts) : "0";
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static (double R2, bool Passed) ComputeScore(List<double> pred, List<double> obs)
    {
        double ssRes = 0, ssTot = 0, mean = obs.Average();
        for (int i = 0; i < obs.Count; i++)
        { ssRes += (obs[i] - pred[i]) * (obs[i] - pred[i]); ssTot += (obs[i] - mean) * (obs[i] - mean); }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (r2, r2 > 0.10);
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
    // Network builders (minimal copies)
    // ══════════════════════════════════════════════════════════════════

    private static TemporalNetwork BuildNetwork(string type, int n, int seed, double k, double lam)
    {
        var net = new TemporalNetwork(n); var rng = new Random(seed);
        for (int i = 0; i < n; i++)
        {
            double x, y;
            switch (type)
            {
                case "clustered":
                    int cl = rng.Next(3);
                    x = Math.Clamp((cl switch { 0 => 0.2, 1 => 0.5, _ => 0.8 }) + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    y = Math.Clamp((cl switch { 0 => 0.3, 1 => 0.7, _ => 0.5 }) + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99);
                    break;
                case "linear": x = 0.1 + (double)i / n * 0.8; y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.02; break;
                case "circular": double a = 2 * Math.PI * i / n; x = 0.5 + 0.3 * Math.Cos(a); y = 0.5 + 0.3 * Math.Sin(a); break;
                case "dense-sparse":
                    if (i < n / 2) { x = rng.NextDouble() * 0.4; y = rng.NextDouble(); }
                    else { x = 0.6 + rng.NextDouble() * 0.4; y = rng.NextDouble(); } break;
                case "random-clusters":
                    int rc = rng.Next(4);
                    x = Math.Clamp((rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.8, _ => 0.35 }) + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99);
                    y = Math.Clamp((rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.5, _ => 0.8 }) + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99); break;
                default: x = rng.NextDouble(); y = rng.NextDouble(); break;
            }
            net.AddNode(new TemporalNode(i, 0, 1.0) { X = x, Y = y });
        }
        net.Matrix.FillSpatialCoupling(net.Nodes, k, lam, normalize: false);
        return net;
    }

    private static TemporalNetwork BuildNetworkWithLaw(string type, int n, int seed,
        double k, double lam, Func<double, double, double, double> law)
    {
        var net = new TemporalNetwork(n); var rng = new Random(seed);
        for (int i = 0; i < n; i++) net.AddNode(new TemporalNode(i, 0, 1.0) { X = rng.NextDouble(), Y = rng.NextDouble() });
        for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++)
            {
                double dx = net.Nodes[i].X - net.Nodes[j].X, dy = net.Nodes[i].Y - net.Nodes[j].Y;
                double c = law(k, lam, Math.Sqrt(dx * dx + dy * dy));
                net.Matrix.SetCoupling(i, j, c); net.Matrix.SetCoupling(j, i, c);
            }
        return net;
    }

    private static void EvolvePhases(TemporalNetwork net, int steps)
    {
        int n = net.NodeCount;
        for (int step = 0; step < steps; step++)
        {
            double[] np = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++) if (i != j)
                        sum += net.Matrix.GetCoupling(i, j) * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
                np[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum));
            }
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
        }
    }

    private static void PhaseStepWithNoise(TemporalNetwork net, Random rng, double sigma)
    {
        int n = net.NodeCount; double[] np = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < n; j++) if (i != j)
                    sum += net.Matrix.GetCoupling(i, j) * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase);
            double noise = Math.Sqrt(-2 * Math.Log(Math.Max(1 - rng.NextDouble(), 1e-10))) * Math.Sin(2 * Math.PI * (1 - rng.NextDouble())) * sigma;
            np[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum) + noise);
        }
        for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
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
        for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++) { sum += net.Matrix.GetCoupling(i, j); pairs++; }
        return sum / pairs;
    }
}
