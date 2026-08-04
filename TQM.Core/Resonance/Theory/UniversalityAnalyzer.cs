using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Theory;

/// <summary>
/// Searches for scale transformations that make the {R, M} theory
/// universal across N, K, λ regimes. Tests whether missing physics
/// is scale dependence rather than missing variables.
///
/// TQM-103: Universality and Scale Invariance
/// </summary>
public static class UniversalityAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record DataPoint(
        double R, double M, double dRdt, int N, double K, double Lam, string Topo);

    public sealed record RenormalizedState(
        string Name,
        string Formula,
        double BetaN, double GammaK, double DeltaLam,
        Func<double, int, double, double, double> TransformM,  // (M, N, K, lam) -> M*
        Func<double, int, double, double, double> TransformR); // (R, N, K, lam) -> R*

    public sealed record ScalingResult(
        RenormalizedState State,
        double CollapseR2,
        double RawR2,
        double CollapseQuality,
        double[] Coefficients,  // [intercept, R_coeff, M_coeff]
        int Rank);

    public sealed record UniversalityReport(
        List<RenormalizedState> Candidates,
        List<ScalingResult> Results,
        ScalingResult BestResult,
        Dictionary<string, (double R2, bool Passed)> Validation,
        double SurvivalRate,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Generate scaling data
    // ══════════════════════════════════════════════════════════════════

    public static List<DataPoint> GenerateScalingData(int baseSeed = 103_000_001)
    {
        var points = new List<DataPoint>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };
        int[] Ns = { 10, 20, 50, 100, 200, 500, 1000 };
        double[] Ks = { 0.01, 0.1, 0.5, 1.0, 2.0, 5.0, 10.0 };
        double[] Lams = { 0.005, 0.01, 0.05, 0.1, 0.2, 0.5 };

        int counter = 0;
        var rng = new Random(baseSeed);

        // Dense stratified sampling.
        for (int i = 0; i < 500; i++)
        {
            int nTry = Ns[rng.Next(Ns.Length)];
            double kTry = Ks[rng.Next(Ks.Length)];
            double lTry = Lams[rng.Next(Lams.Length)];
            string topo = types[rng.Next(types.Length)];
            int seed = baseSeed + Interlocked.Increment(ref counter) * 7919;

            var net = BuildNetwork(topo, nTry, seed, kTry, lTry);
            var rng2 = new Random(seed);
            for (int j = 0; j < nTry; j++)
                net.Nodes[j].Phase = rng2.NextDouble() * 2 * Math.PI;

            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10);
            double R1 = ComputeR(net);
            points.Add(new DataPoint(R0, M0, (R1 - R0) / 10.0, nTry, kTry, lTry, topo));
        }

        // Add extreme R samples for better coverage.
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
                for (int j = 0; j < nTry; j++)
                    net.Nodes[j].Phase = bp + (rng2.NextDouble() * 2 - 1) * 0.01;
            }
            else
                for (int j = 0; j < nTry; j++)
                    net.Nodes[j].Phase = rng2.NextDouble() * 2 * Math.PI;

            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10);
            double R1 = ComputeR(net);
            points.Add(new DataPoint(R0, M0, (R1 - R0) / 10.0, nTry, kTry, lTry, "extreme"));
        }

        return points;
    }

    // ══════════════════════════════════════════════════════════════════
    // Candidate renormalizations
    // ══════════════════════════════════════════════════════════════════

    public static List<RenormalizedState> GenerateCandidates()
    {
        var candidates = new List<RenormalizedState>();

        // Raw (no renormalization).
        candidates.Add(new RenormalizedState("Raw", "M*=M, R*=R", 0, 0, 0,
            (m, n, k, lam) => m,
            (r, n, k, lam) => r));

        // M/N — mean coupling per oscillator (natural Kuramoto scaling).
        candidates.Add(new RenormalizedState("M/N", "M* = M/N", -1, 0, 0,
            (m, n, k, lam) => m / Math.Max(n, 1),
            (r, n, k, lam) => r));

        // N·M — total coupling.
        candidates.Add(new RenormalizedState("N·M", "M* = N·M", 1, 0, 0,
            (m, n, k, lam) => n * m,
            (r, n, k, lam) => r));

        // M·K — coupling scaled by global strength.
        candidates.Add(new RenormalizedState("M·K", "M* = M·K", 0, 1, 0,
            (m, n, k, lam) => m * k,
            (r, n, k, lam) => r));

        // M/K.
        candidates.Add(new RenormalizedState("M/K", "M* = M/K", 0, -1, 0,
            (m, n, k, lam) => m / Math.Max(k, 1e-10),
            (r, n, k, lam) => r));

        // M·λ.
        candidates.Add(new RenormalizedState("M·λ", "M* = M·λ", 0, 0, 1,
            (m, n, k, lam) => m * lam,
            (r, n, k, lam) => r));

        // M/λ.
        candidates.Add(new RenormalizedState("M/λ", "M* = M/λ", 0, 0, -1,
            (m, n, k, lam) => m / Math.Max(lam, 1e-10),
            (r, n, k, lam) => r));

        // N·M·K — total coupling × global strength.
        candidates.Add(new RenormalizedState("N·M·K", "M* = N·M·K", 1, 1, 0,
            (m, n, k, lam) => n * m * k,
            (r, n, k, lam) => r));

        // M/(N·λ).
        candidates.Add(new RenormalizedState("M/(N·λ)", "M* = M/(N·λ)", -1, 0, -1,
            (m, n, k, lam) => m / (Math.Max(n, 1) * Math.Max(lam, 1e-10)),
            (r, n, k, lam) => r));

        // R/√N — finite-size coherence scaling.
        candidates.Add(new RenormalizedState("R·√N", "M*=M, R*=R·√N", 0, 0, 0,
            (m, n, k, lam) => m,
            (r, n, k, lam) => r * Math.Sqrt(n)));

        return candidates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Search for optimal scaling via grid search
    // ══════════════════════════════════════════════════════════════════

    public static UniversalityReport SearchUniversality(
        List<DataPoint> data, int baseSeed = 103_000_001)
    {
        int n = data.Count;
        double[] Y = data.Select(d => d.dRdt).ToArray();
        double ssTot = 0;
        double meanY = Y.Average();
        for (int i = 0; i < n; i++) ssTot += (Y[i] - meanY) * (Y[i] - meanY);

        // Raw baseline.
        double rawR2 = FitR2Raw(data, Y, ssTot);

        // Test candidate renormalizations.
        var candidates = GenerateCandidates();
        var results = new List<ScalingResult>();

        foreach (var cand in candidates)
        {
            double[] Rstar = new double[n];
            double[] Mstar = new double[n];
            for (int i = 0; i < n; i++)
            {
                var dp = data[i];
                Rstar[i] = cand.TransformR(dp.R, dp.N, dp.K, dp.Lam);
                Mstar[i] = cand.TransformM(dp.M, dp.N, dp.K, dp.Lam);
            }

            var (coeffs, r2) = Fit3Param(Rstar, Mstar, Y);
            double quality = rawR2 > 1e-10 ? r2 / rawR2 : 0;
            results.Add(new ScalingResult(cand, r2, rawR2, quality, coeffs, 0));
        }

        // Grid search for optimal exponents.
        double bestR2 = rawR2;
        double bestBeta = 0, bestGamma = 0, bestDelta = 0;

        double[] betas = { -2.0, -1.5, -1.0, -0.5, -0.25, 0, 0.25, 0.5, 1.0, 1.5, 2.0 };
        double[] gammas = { -2.0, -1.0, -0.5, 0, 0.5, 1.0, 2.0 };
        double[] deltas = { -2.0, -1.0, -0.5, 0, 0.5, 1.0, 2.0 };

        foreach (double beta in betas)
            foreach (double gamma in gammas)
                foreach (double delta in deltas)
                {
                    double[] Mstar = new double[n];
                    for (int i = 0; i < n; i++)
                    {
                        var dp = data[i];
                        Mstar[i] = dp.M * Math.Pow(Math.Max(dp.N, 1), beta)
                                        * Math.Pow(Math.Max(dp.K, 1e-10), gamma)
                                        * Math.Pow(Math.Max(dp.Lam, 1e-10), delta);
                    }

                    double[] R_raw = data.Select(d => d.R).ToArray();
                    var (_, r2) = Fit3Param(R_raw, Mstar, Y);
                    if (r2 > bestR2) { bestR2 = r2; bestBeta = beta; bestGamma = gamma; bestDelta = delta; }
                }

        double betaF = bestBeta, gammaF = bestGamma, deltaF = bestDelta;
        var bestState = new RenormalizedState(
            "OPTIMAL", $"M* = M·N^{betaF:F2}·K^{gammaF:F2}·λ^{deltaF:F2}",
            betaF, gammaF, deltaF,
            (m, nn, k, lam) => m * Math.Pow(Math.Max(nn, 1), betaF)
                                 * Math.Pow(Math.Max(k, 1e-10), gammaF)
                                 * Math.Pow(Math.Max(lam, 1e-10), deltaF),
            (r, nn, k, lam) => r);

        // Fit optimal renormalization.
        double[] Ropt = data.Select(d => d.R).ToArray();
        double[] Mopt = new double[n];
        for (int i = 0; i < n; i++)
        {
            var dp = data[i];
            Mopt[i] = bestState.TransformM(dp.M, dp.N, dp.K, dp.Lam);
        }
        var (optCoeffs, optR2) = Fit3Param(Ropt, Mopt, Y);

        double optQuality = rawR2 > 1e-10 ? optR2 / rawR2 : 0;
        var bestResult = new ScalingResult(bestState, optR2, rawR2, optQuality, optCoeffs, 1);

        // Rank candidates.
        results.Add(bestResult);
        results = results.OrderByDescending(r => r.CollapseR2).ToList();
        for (int i = 0; i < results.Count; i++)
            results[i] = results[i] with { Rank = i + 1 };

        // Validate best renormalization against TQM-100 attacks.
        var validation = ValidateRenormalized(bestState, optCoeffs, baseSeed);

        int passed = validation.Count(v => v.Value.Passed);
        double survival = (double)passed / validation.Count;

        string classification = survival >= 0.875 ? "D: Scale-Invariant Effective Theory" :
                                survival >= 0.625 ? "C: Strong Universality" :
                                survival >= 0.375 ? "B: Weak Scaling" :
                                "A: No Universality";

        string interp = bestResult.CollapseQuality >= 2.0
            ? $"Renormalization {bestState.Formula} dramatically improves collapse " +
              $"(quality={bestResult.CollapseQuality:F2}×). Strong evidence for scale invariance."
            : bestResult.CollapseQuality >= 1.2
            ? $"Renormalization {bestState.Formula} measurably improves collapse " +
              $"(quality={bestResult.CollapseQuality:F2}×). Moderate evidence for scaling."
            : $"Best renormalization {bestState.Formula} provides weak improvement " +
              $"(quality={bestResult.CollapseQuality:F2}×). Scale invariance is weak or absent.";

        return new UniversalityReport(candidates, results, bestResult,
            validation, survival, classification, interp);
    }

    // ══════════════════════════════════════════════════════════════════
    // Validation against TQM-100 attacks (using renormalized variables)
    // ══════════════════════════════════════════════════════════════════

    private static Dictionary<string, (double R2, bool Passed)> ValidateRenormalized(
        RenormalizedState state, double[] coeffs, int seed)
    {
        var results = new Dictionary<string, (double, bool)>();

        results["Extreme Coherence"] = TestExtremeR(state, coeffs, seed + 100);
        results["Extreme M"] = TestExtremeM(state, coeffs, seed + 200);
        results["Mixed Topologies"] = TestTopologies(state, coeffs, seed + 300);
        results["Coupling Laws"] = TestCouplingLaws(state, coeffs, seed + 400);
        results["Phase Noise"] = TestNoise(state, coeffs, seed + 500);
        results["Large-N N=500"] = TestLargeN(state, coeffs, seed + 600);
        results["Small-N N=10"] = TestSmallN(state, coeffs, seed + 700);
        results["Out-of-Distribution"] = TestOOD(state, coeffs, seed + 800);

        return results;
    }

    private static double Predict(RenormalizedState state, double[] coeffs,
        double R, double M, int N, double K, double lam)
    {
        double Rs = state.TransformR(R, N, K, lam);
        double Ms = state.TransformM(M, N, K, lam);
        return coeffs[0] + coeffs[1] * Rs + coeffs[2] * Ms;
    }

    // ══════════════════════════════════════════════════════════════════
    // Individual attack tests
    // ══════════════════════════════════════════════════════════════════

    private static (double, bool) TestExtremeR(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        for (int si = 0; si < 15; si++)
        {
            var net = BuildNetwork("uniform", 100, seed + si, 2.0, 0.05);
            var rng = new Random(seed + si);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        for (int si = 0; si < 10; si++)
        {
            var net = BuildNetwork("clustered", 100, seed + 1000 + si, 2.0, 0.05);
            var rng = new Random(seed + 1000 + si);
            double bp = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = bp + (rng.NextDouble() * 2 - 1) * 0.005;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        return Score(p, o);
    }

    private static (double, bool) TestExtremeM(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        foreach (var (k, lam) in new[] { (0.5, 0.20), (5.0, 0.03) })
            for (int si = 0; si < 15; si++)
            {
                var net = BuildNetwork("uniform", 100, seed + si, k, lam);
                var rng = new Random(seed + si);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                p.Add(Predict(s, c, R0, M0, 100, k, lam)); o.Add((R1 - R0) / 10.0);
            }
        return Score(p, o);
    }

    private static (double, bool) TestTopologies(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        string[] types = { "uniform", "clustered", "linear", "circular", "dense-sparse", "random-clusters" };
        foreach (var t in types)
            for (int si = 0; si < 8; si++)
            {
                var net = BuildNetwork(t, 100, seed + si, 2.0, 0.05);
                var rng = new Random(seed + si);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
            }
        return Score(p, o);
    }

    private static (double, bool) TestCouplingLaws(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        for (int si = 0; si < 10; si++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + si, 2.0, 0.05,
                (k, lam, d) => k / (1.0 + d / lam));
            var rng = new Random(seed + si);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        for (int si = 0; si < 10; si++)
        {
            var net = BuildNetworkWithLaw("uniform", 100, seed + 1000 + si, 2.0, 0.05,
                (k, lam, d) => k / 100.0);
            var rng = new Random(seed + 1000 + si);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        return Score(p, o);
    }

    private static (double, bool) TestNoise(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        for (int si = 0; si < 15; si++)
        {
            var net = BuildNetwork("uniform", 100, seed + si, 2.0, 0.05);
            var rng = new Random(seed + si);
            for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            for (int step = 0; step < 10; step++) PhaseStepNoise(net, rng, 0.3);
            double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 100, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        return Score(p, o);
    }

    private static (double, bool) TestLargeN(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        for (int si = 0; si < 10; si++)
        {
            var net = BuildNetwork("uniform", 500, seed + si, 2.0, 0.05);
            var rng = new Random(seed + si);
            for (int i = 0; i < 500; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 500, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        return Score(p, o);
    }

    private static (double, bool) TestSmallN(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        for (int si = 0; si < 15; si++)
        {
            var net = BuildNetwork("uniform", 10, seed + si, 2.0, 0.05);
            var rng = new Random(seed + si);
            for (int i = 0; i < 10; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
            double R0 = ComputeR(net), M0 = ComputeM(net);
            Evolve(net, 10); double R1 = ComputeR(net);
            p.Add(Predict(s, c, R0, M0, 10, 2.0, 0.05)); o.Add((R1 - R0) / 10.0);
        }
        return Score(p, o);
    }

    private static (double, bool) TestOOD(RenormalizedState s, double[] c, int seed)
    {
        var p = new List<double>(); var o = new List<double>();
        foreach (var (k, lam) in new[] { (0.1, 0.01), (10.0, 0.20) })
            for (int si = 0; si < 10; si++)
            {
                var net = BuildNetwork("random-clusters", 100, seed + si, k, lam);
                var rng = new Random(seed + si);
                for (int i = 0; i < 100; i++) net.Nodes[i].Phase = rng.NextDouble() * 2 * Math.PI;
                double R0 = ComputeR(net), M0 = ComputeM(net);
                Evolve(net, 10); double R1 = ComputeR(net);
                p.Add(Predict(s, c, R0, M0, 100, k, lam)); o.Add((R1 - R0) / 10.0);
            }
        return Score(p, o);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double FitR2Raw(List<DataPoint> data, double[] Y, double ssTot)
    {
        double[] R = data.Select(d => d.R).ToArray();
        double[] M = data.Select(d => d.M).ToArray();
        var (_, r2) = Fit3Param(R, M, Y);
        return r2;
    }

    private static (double[], double) Fit3Param(double[] X1, double[] X2, double[] Y)
    {
        int n = Y.Length;
        double[,] XTX = new double[3, 3]; double[] XTY = new double[3];
        for (int i = 0; i < n; i++)
        {
            double[] f = { 1, X1[i], X2[i] };
            for (int a = 0; a < 3; a++)
            { XTY[a] += f[a] * Y[i]; for (int b = 0; b < 3; b++) XTX[a, b] += f[a] * f[b]; }
        }
        double[] beta = SolveGauss(XTX, XTY, 3);
        double ssRes = 0, mean = Y.Average();
        for (int i = 0; i < n; i++)
        { double pred = beta[0] + beta[1] * X1[i] + beta[2] * X2[i]; ssRes += (Y[i] - pred) * (Y[i] - pred); }
        double ssTot = 0;
        for (int i = 0; i < n; i++) ssTot += (Y[i] - mean) * (Y[i] - mean);
        return (beta, ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0);
    }

    private static (double, bool) Score(List<double> p, List<double> o)
    {
        double ssRes = 0, ssTot = 0, mean = o.Average();
        for (int i = 0; i < o.Count; i++)
        { ssRes += (o[i] - p[i]) * (o[i] - p[i]); ssTot += (o[i] - mean) * (o[i] - mean); }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (r2, r2 > 0.10);
    }

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++) { for (int j = 0; j < n; j++) M[i, j] = A[i, j]; M[i, n] = b[i]; }
        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col])) maxRow = row;
            for (int j = col; j <= n; j++) (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            { double f = M[row, col] / M[col, col]; for (int j = col; j <= n; j++) M[row, j] -= f * M[col, j]; }
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
                    y = Math.Clamp((cl switch { 0 => 0.3, 1 => 0.7, _ => 0.5 }) + (rng.NextDouble() * 2 - 1) * 0.1, 0.01, 0.99); break;
                case "linear": x = 0.1 + (double)i / n * 0.8; y = 0.5 + (rng.NextDouble() * 2 - 1) * 0.02; break;
                case "circular": double a = 2 * Math.PI * i / n; x = 0.5 + 0.3 * Math.Cos(a); y = 0.5 + 0.3 * Math.Sin(a); break;
                case "dense-sparse": if (i < n / 2) { x = rng.NextDouble() * 0.4; y = rng.NextDouble(); } else { x = 0.6 + rng.NextDouble() * 0.4; y = rng.NextDouble(); } break;
                case "random-clusters": int rc = rng.Next(4); x = Math.Clamp((rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.8, _ => 0.35 }) + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99); y = Math.Clamp((rc switch { 0 => 0.2, 1 => 0.5, 2 => 0.5, _ => 0.8 }) + (rng.NextDouble() * 2 - 1) * 0.08, 0.01, 0.99); break;
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
            { double dx = net.Nodes[i].X - net.Nodes[j].X, dy = net.Nodes[i].Y - net.Nodes[j].Y; double c = law(k, lam, Math.Sqrt(dx * dx + dy * dy)); net.Matrix.SetCoupling(i, j, c); net.Matrix.SetCoupling(j, i, c); }
        return net;
    }

    private static void Evolve(TemporalNetwork net, int steps)
    {
        int n = net.NodeCount;
        for (int s = 0; s < steps; s++)
        {
            double[] np = new double[n];
            for (int i = 0; i < n; i++) { double sum = 0; for (int j = 0; j < n; j++) if (i != j) sum += net.Matrix.GetCoupling(i, j) * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); np[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum)); }
            for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
        }
    }

    private static void PhaseStepNoise(TemporalNetwork net, Random rng, double sigma)
    {
        int n = net.NodeCount; double[] np = new double[n];
        for (int i = 0; i < n; i++) { double sum = 0; for (int j = 0; j < n; j++) if (i != j) sum += net.Matrix.GetCoupling(i, j) * Math.Sin(net.Nodes[j].Phase - net.Nodes[i].Phase); double noise = Math.Sqrt(-2 * Math.Log(Math.Max(1 - rng.NextDouble(), 1e-10))) * Math.Sin(2 * Math.PI * (1 - rng.NextDouble())) * sigma; np[i] = TemporalSimulation.NormalizePhase(net.Nodes[i].Phase + 0.01 * (net.Nodes[i].Frequency + sum) + noise); }
        for (int i = 0; i < n; i++) net.Nodes[i].Phase = np[i];
    }

    private static double ComputeR(TemporalNetwork net)
    { double ss = 0, sc = 0; int n = net.NodeCount; for (int i = 0; i < n; i++) { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); } return Math.Sqrt(ss * ss + sc * sc) / n; }

    private static double ComputeM(TemporalNetwork net)
    { int n = net.NodeCount; double s = 0; int p = 0; for (int i = 0; i < n; i++) for (int j = i + 1; j < n; j++) { s += net.Matrix.GetCoupling(i, j); p++; } return s / p; }
}
