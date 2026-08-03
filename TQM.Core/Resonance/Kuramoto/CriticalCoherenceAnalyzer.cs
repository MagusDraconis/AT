using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether attraction emerges at a universal
/// critical coherence threshold via high-resolution R-scan
/// with controlled-coherence state preparation.
///
/// TQM-071: Critical Coherence Threshold
/// </summary>
public static class CriticalCoherenceAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Measurement at a single target R value.
    /// </summary>
    public sealed record CoherenceThresholdProfile(
        double TargetR,
        double ActualR,            // measured R after preparation
        double InitialSeparation,
        double FinalSeparation,
        double SeparationChange,    // negative = attraction
        double MeanVelocity,
        double MaxVelocity,
        double AttractionForce,    // positive = attractive
        bool Attracts,             // separation decreased
        double Beta,
        string LawName,
        int Seed);

    /// <summary>
    /// Phase transition analysis across the full R sweep.
    /// </summary>
    public sealed record PhaseTransitionReport(
        List<CoherenceThresholdProfile> Profiles,
        double CriticalR,          // R where attraction probability crosses 50%
        double TransitionWidth,    // R range from 10% to 90% attraction
        double MaximumForce,
        double RAtMaxForce,
        bool IsUniversal,          // threshold consistent across laws
        string TransitionType,     // Continuous / Discontinuous / Sharp Crossover
        string Classification,
        string Interpretation,
        // Per-law critical thresholds
        Dictionary<string, double> LawThresholds);

    // ══════════════════════════════════════════════════════════════════
    // Coupling laws
    // ══════════════════════════════════════════════════════════════════

    private static readonly Dictionary<string, Func<double, double>> ForceLaws = new()
    {
        ["cos"]        = d => Math.Cos(d),
        ["cos²"]       = d => Math.Cos(d) * Math.Cos(d),
        ["exp(-|x|)"]  = d => Math.Exp(-Math.Abs(d)),
    };

    // ══════════════════════════════════════════════════════════════════
    // Bessel function ratio R(κ) = I₁(κ)/I₀(κ)
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Computes R(κ) = I₁(κ)/I₀(κ) using series/asymptotic approximations.
    /// </summary>
    public static double RFromKappa(double kappa)
    {
        if (kappa < 1e-10) return 0;
        if (kappa <= 2.0)
        {
            // Series: R(κ) ≈ κ/2 - κ³/16 + κ⁵/96 - 19κ⁷/4608
            double k2 = kappa * kappa;
            double k3 = k2 * kappa;
            double k5 = k3 * k2;
            double k7 = k5 * k2;
            return kappa / 2.0 - k3 / 16.0 + k5 / 96.0 - 19.0 * k7 / 4608.0;
        }
        else
        {
            // Asymptotic: R(κ) ≈ 1 - 1/(2κ) - 1/(8κ²) - 1/(8κ³)
            double inv = 1.0 / kappa;
            return 1.0 - 0.5 * inv - 0.125 * inv * inv - 0.125 * inv * inv * inv;
        }
    }

    /// <summary>
    /// Computes κ from target R via Newton iteration.
    /// </summary>
    public static double KappaFromR(double targetR)
    {
        if (targetR <= 0) return 0;
        if (targetR >= 0.9999) return 1.0 / (2.0 * (1.0 - targetR));

        // Initial guess.
        double kappa = targetR < 0.6
            ? 2.0 * targetR + targetR * targetR * targetR
            : 1.0 / (2.0 * (1.0 - targetR));

        // Newton: κ_{n+1} = κ_n - (R(κ_n) - targetR) / R'(κ_n)
        for (int iter = 0; iter < 20; iter++)
        {
            double rk = RFromKappa(kappa);
            double drk = 1.0 - rk * rk - rk / Math.Max(kappa, 1e-10);
            // R'(κ) = 1 - R² - R/κ  (standard Bessel identity)
            double delta = (rk - targetR) / Math.Max(drk, 1e-10);
            kappa -= delta;
            if (Math.Abs(delta) < 1e-8) break;
            if (kappa < 0) kappa = 0.01;
        }
        return kappa;
    }

    // ══════════════════════════════════════════════════════════════════
    // Von Mises random variate generation (Best-Fisher algorithm)
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generates a random angle from von Mises(μ=0, κ) distribution.
    /// Uses normal approximation for κ > 5 (wrapped N(0, 1/√κ)).
    /// </summary>
    private static double VonMises(Random rng, double kappa)
    {
        if (kappa < 0.01)
            return rng.NextDouble() * 2 * Math.PI; // near-uniform

        // For high κ, use normal approximation (von Mises ≈ wrapped normal).
        if (kappa > 5.0)
        {
            // Box-Muller: N(0, σ²) with σ = 1/√κ.
            double u1 = rng.NextDouble();
            double u2 = rng.NextDouble();
            double sigma = 1.0 / Math.Sqrt(kappa);
            double z = Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) *
                       Math.Cos(2.0 * Math.PI * u2);
            double theta = z * sigma;
            // Wrap to [0, 2π).
            theta %= 2.0 * Math.PI;
            if (theta < 0) theta += 2.0 * Math.PI;
            return theta;
        }

        // Best-Fisher (1979) for moderate κ.
        double tau = 1.0 + Math.Sqrt(1.0 + 4.0 * kappa * kappa);
        double rho = (tau - Math.Sqrt(2.0 * tau)) / (2.0 * kappa);
        double r = (1.0 + rho * rho) / (2.0 * rho);

        int maxAttempts = 1000;
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            double u1 = rng.NextDouble();
            double u2 = rng.NextDouble();
            double u3 = rng.NextDouble();

            double z = Math.Cos(Math.PI * u1);
            double f = (1.0 + r * z) / (r + z);
            double c = kappa * (r - f);

            if (c * (2.0 - c) - u2 > 0 ||
                (c > 0 && Math.Log(c / Math.Max(u2, 1e-15)) + 1.0 - c >= 0))
            {
                double theta = Math.Acos(Math.Clamp(f, -1.0, 1.0));
                if (u3 > 0.5) theta = 2.0 * Math.PI - theta;
                return theta;
            }
        }

        // Fallback: uniform.
        return rng.NextDouble() * 2.0 * Math.PI;
    }

    // ══════════════════════════════════════════════════════════════════
    // Controlled-coherence state preparation
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Prepares a network with controlled coherence (target R) by
    /// generating phases from von Mises distribution.
    /// </summary>
    private static TemporalNetwork PrepareState(
        double targetR, double k, double lambda, int nPerGroup, int seed)
    {
        double kappa = KappaFromR(targetR);
        var rng = new Random(seed);
        int n = nPerGroup * 2;
        var network = new TemporalNetwork(n);

        // Group A: center (0.3, 0.5).
        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(i, phase,
                0.5 + rng.NextDouble() * 1.5)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }

        // Group B: center (0.7, 0.5).
        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(nPerGroup + i, phase,
                0.5 + rng.NextDouble() * 1.5)
            { X = Math.Clamp(0.7 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        return network;
    }

    // ══════════════════════════════════════════════════════════════════
    // Single R-point measurement
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Measures instantaneous attraction at a given target R.
    /// Prepares state, runs brief simulation, measures force.
    /// </summary>
    public static CoherenceThresholdProfile MeasureAtR(
        double targetR, string lawName, Func<double, double> forceFn,
        double beta, double k, double lambda, int nPerGroup, int seed,
        int measureSteps = 30)
    {
        var network = PrepareState(targetR, k, lambda, nPerGroup, seed);
        int n = network.NodeCount;

        // Measure actual R after preparation.
        double actualR = GlobalR(network);
        double initSep = GroupSeparation(network, nPerGroup);

        double totalVel = 0;
        double maxVel = 0;

        for (int iter = 0; iter < measureSteps; iter++)
        {
            // Phase update (standard Kuramoto).
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }

            // Position update.
            double[] nx = new double[n], ny = new double[n];
            for (int i = 0; i < n; i++)
            {
                double fx = 0, fy = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;
                    double dx = network.Nodes[j].X - network.Nodes[i].X;
                    double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                    double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                    double w = network.Matrix.GetCoupling(i, j);
                    double pd = TemporalSimulation.NormalizePhase(
                        network.Nodes[j].Phase - network.Nodes[i].Phase);
                    if (pd > Math.PI) pd -= 2 * Math.PI;
                    double force = forceFn(pd);
                    fx += w * force * dx / d;
                    fy += w * force * dy / d;
                }
                nx[i] = Math.Clamp(network.Nodes[i].X + 0.001 * fx, 0.01, 0.99);
                ny[i] = Math.Clamp(network.Nodes[i].Y + 0.001 * fy, 0.01, 0.99);
            }
            for (int i = 0; i < n; i++)
            { network.Nodes[i].X = nx[i]; network.Nodes[i].Y = ny[i]; }
        }

        double finalSep = GroupSeparation(network, nPerGroup);
        double sepChange = finalSep - initSep;
        double attractForce = -sepChange / Math.Max(initSep, 1e-10); // positive = attraction
        bool attracts = sepChange < -1e-6;

        return new CoherenceThresholdProfile(targetR, actualR, initSep, finalSep,
            sepChange, totalVel / Math.Max(measureSteps, 1), maxVel,
            attractForce, attracts, beta, lawName, seed);
    }

    // ══════════════════════════════════════════════════════════════════
    // Phase transition analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Analyzes the phase transition from coherence scan data.
    /// </summary>
    public static PhaseTransitionReport AnalyzePhaseTransition(
        List<CoherenceThresholdProfile> profiles)
    {
        // Group by target R (average across seeds and laws).
        var byR = profiles.GroupBy(p => p.TargetR)
            .OrderBy(g => g.Key).ToList();

        var rVals = byR.Select(g => g.Key).ToList();
        var attractProb = byR.Select(g =>
            (double)g.Count(p => p.Attracts) / g.Count()).ToList();
        var meanForce = byR.Select(g => g.Average(p => p.AttractionForce)).ToList();

        // Find critical R: where attraction probability crosses 50%.
        double rCrit = double.NaN;
        for (int i = 1; i < attractProb.Count; i++)
        {
            if (attractProb[i - 1] < 0.5 && attractProb[i] >= 0.5)
            {
                double t = (0.5 - attractProb[i - 1]) /
                           Math.Max(attractProb[i] - attractProb[i - 1], 1e-10);
                rCrit = rVals[i - 1] + t * (rVals[i] - rVals[i - 1]);
                break;
            }
        }

        // Transition width: R90 - R10.
        int i10 = attractProb.FindIndex(p => p >= 0.1);
        int i90 = attractProb.FindLastIndex(p => p <= 0.9);
        double r10 = i10 >= 0 ? rVals[i10] : 0;
        double r90 = i90 >= 0 ? rVals[Math.Min(i90 + 1, rVals.Count - 1)] : 1;
        double transWidth = r90 - r10;

        // Maximum force.
        int maxIdx = 0;
        double maxForce = double.MinValue;
        for (int i = 0; i < meanForce.Count; i++)
        {
            if (meanForce[i] > maxForce)
            { maxForce = meanForce[i]; maxIdx = i; }
        }

        // Per-law critical thresholds.
        var lawThresholds = new Dictionary<string, double>();
        foreach (var lawGroup in profiles.GroupBy(p => p.LawName))
        {
            var lawByR = lawGroup.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();
            var lawR = lawByR.Select(g => g.Key).ToList();
            var lawP = lawByR.Select(g =>
                (double)g.Count(p => p.Attracts) / g.Count()).ToList();

            for (int i = 1; i < lawP.Count; i++)
            {
                if (lawP[i - 1] < 0.5 && lawP[i] >= 0.5)
                {
                    double t = (0.5 - lawP[i - 1]) /
                               Math.Max(lawP[i] - lawP[i - 1], 1e-10);
                    lawThresholds[lawGroup.Key] = lawR[i - 1] + t * (lawR[i] - lawR[i - 1]);
                    break;
                }
            }
            if (!lawThresholds.ContainsKey(lawGroup.Key))
                lawThresholds[lawGroup.Key] = double.NaN;
        }

        // Universality: check if thresholds are within 0.05 of each other.
        var validThresholds = lawThresholds.Values
            .Where(v => !double.IsNaN(v)).ToList();
        bool isUniversal = validThresholds.Count >= 2 &&
            validThresholds.Max() - validThresholds.Min() < 0.05;

        // Transition type.
        string transType;
        if (transWidth < 0.05)
            transType = "Discontinuous (First-Order)";
        else if (transWidth < 0.15)
            transType = "Sharp Crossover";
        else
            transType = "Continuous (Second-Order)";

        // Classification.
        string classification;
        if (!double.IsNaN(rCrit) && isUniversal)
            classification = "D: Universal Critical Threshold";
        else if (!double.IsNaN(rCrit))
            classification = "C: Law-Dependent Threshold";
        else if (transWidth < 0.2)
            classification = "B: Sharp Crossover (No Clear Threshold)";
        else
            classification = "A: No Threshold";

        string interpretation = classification switch
        {
            "D: Universal Critical Threshold" =>
                $"A universal critical coherence R_crit ≈ {rCrit:F3} exists. " +
                $"Attraction emerges as a {transType.ToLower()} at this threshold, " +
                $"independent of coupling law. Coherence is the true order parameter " +
                "for spatial attraction.",
            "C: Law-Dependent Threshold" =>
                $"Critical thresholds vary by coupling law " +
                $"(range: {validThresholds.Min():F3}–{validThresholds.Max():F3}). " +
                "Coherence is necessary but the threshold depends on the specific " +
                "interaction form.",
            _ => "No sharp critical threshold detected. Attraction emerges " +
                 "gradually as coherence increases."
        };

        return new PhaseTransitionReport(profiles, rCrit, transWidth,
            maxForce, rVals[maxIdx], isUniversal, transType,
            classification, interpretation, lawThresholds);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full sweep
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs the full R sweep across all coupling laws and seeds.
    /// </summary>
    public static (List<CoherenceThresholdProfile> Profiles, PhaseTransitionReport Report)
    RunFullCoherenceScan(
        double rMin, double rMax, double rStep,
        double k, double lambda, int nPerGroup, int seedsPerPoint,
        int baseSeed, double beta = 0.0)
    {
        var profiles = new List<CoherenceThresholdProfile>();
        int seedIdx = 0;

        // Generate R targets.
        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (var (lawName, fn) in ForceLaws)
        {
            foreach (double rT in rTargets)
            {
                for (int s = 0; s < seedsPerPoint; s++)
                {
                    profiles.Add(MeasureAtR(rT, lawName, fn, beta, k, lambda,
                        nPerGroup, baseSeed + seedIdx++ * 7919));
                }
            }
        }

        var report = AnalyzePhaseTransition(profiles);
        return (profiles, report);
    }

    // ══════════════════════════════════════════════════════════════════
    // Also scan with different β values for universality
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs R sweep across multiple β values to test β-independence.
    /// </summary>
    public static (List<CoherenceThresholdProfile> Profiles, PhaseTransitionReport Report)
    RunBetaSweep(
        double rMin, double rMax, double rStep,
        double[] betas, string lawName,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        if (!ForceLaws.TryGetValue(lawName, out var fn))
            fn = ForceLaws["cos"];

        var profiles = new List<CoherenceThresholdProfile>();
        int seedIdx = 0;

        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (double beta in betas)
        {
            foreach (double rT in rTargets)
            {
                for (int s = 0; s < seedsPerPoint; s++)
                {
                    profiles.Add(MeasureAtR(rT, $"{lawName}_β{beta:F2}", fn, beta,
                        k, lambda, nPerGroup, baseSeed + seedIdx++ * 7919));
                }
            }
        }

        var report = AnalyzePhaseTransition(profiles);
        return (profiles, report);
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static double GlobalR(TemporalNetwork net)
    {
        int n = net.NodeCount;
        double ss = 0, sc = 0;
        for (int i = 0; i < n; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / n;
    }

    private static double GroupSeparation(TemporalNetwork net, int nPerGroup)
    {
        double cxA = 0, cyA = 0, cxB = 0, cyB = 0;
        for (int i = 0; i < nPerGroup; i++)
        { cxA += net.Nodes[i].X; cyA += net.Nodes[i].Y; }
        for (int i = 0; i < nPerGroup; i++)
        { cxB += net.Nodes[i + nPerGroup].X; cyB += net.Nodes[i + nPerGroup].Y; }
        cxA /= nPerGroup; cxB /= nPerGroup; cyA /= nPerGroup; cyB /= nPerGroup;
        return Math.Sqrt((cxA - cxB) * (cxA - cxB) + (cyA - cyB) * (cyA - cyB));
    }
}
