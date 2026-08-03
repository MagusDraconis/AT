using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether effective attraction emerges from
/// the vector summation of many local coupling contributions.
/// Analyzes force alignment and cancellation at controlled R.
///
/// TQM-072: Coherent Force Summation
/// </summary>
public static class ForceSummationAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A single pair-wise force contribution between two oscillators.
    /// </summary>
    public readonly record struct LocalForceContribution(
        int SourceId, int TargetId,
        double Magnitude,
        double Direction,       // angle in [0, 2π) of force vector
        double Fx, double Fy,   // force components
        double PhaseDiff,       // Δθ = θ_target - θ_source
        double CouplingStrength,
        double ForceSign);       // +1 = attractive (toward target), -1 = repulsive

    /// <summary>
    /// Force alignment profile at a single R value.
    /// </summary>
    public sealed record ForceAlignmentProfile(
        double TargetR,
        double ActualR,
        string LawName,
        int Seed,
        // Net force
        double NetForceX,
        double NetForceY,
        double NetForceMagnitude,
        double NetForceDirection,
        // Per-pair statistics
        int TotalPairs,
        double MeanPairMagnitude,
        double StdPairMagnitude,
        double SumPairMagnitudes,   // Σ|f_ij|
        double CancellationRatio,   // |Σ f_ij| / Σ|f_ij|
        // Alignment
        double AlignmentScore,      // mean cos(angle_pair - angle_net)
        double AlignmentStd,
        double AlignedFraction,     // fraction with cos > 0.5
        // Attractive vs repulsive
        int AttractivePairs,        // force toward other group
        int RepulsivePairs,
        double AttractiveFraction,
        // Force histogram
        double[] DirectionHistogram, // 36 bins (10° each)
        double[] MagnitudeHistogram, // 20 bins
        // Raw data (sampled)
        List<LocalForceContribution> SampleForces);

    /// <summary>
    /// Aggregate force summation report.
    /// </summary>
    public sealed record ForceSummationReport(
        List<ForceAlignmentProfile> Profiles,
        double AlignmentAttenuationR, // correlation: alignment score vs R
        double CancellationGrowthR,   // correlation: cancellation ratio vs R
        double NetForceAlignmentR,   // correlation: net force vs alignment
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Coupling laws
    // ══════════════════════════════════════════════════════════════════

    private static readonly Dictionary<string, Func<double, double>> ForceLaws = new()
    {
        ["cos"]        = d => Math.Cos(d),
        ["sin"]        = d => Math.Sin(d),
        ["cos²"]       = d => Math.Cos(d) * Math.Cos(d),
        ["exp(-|x|)"]  = d => Math.Exp(-Math.Abs(d)),
    };

    // ══════════════════════════════════════════════════════════════════
    // State preparation (reuses CriticalCoherenceAnalyzer logic)
    // ══════════════════════════════════════════════════════════════════

    private static TemporalNetwork PrepareState(
        double targetR, double k, double lambda, int nPerGroup, int seed)
    {
        double kappa = CriticalCoherenceAnalyzer.KappaFromR(targetR);
        var rng = new Random(seed);
        int n = nPerGroup * 2;
        var network = new TemporalNetwork(n);

        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(i, phase, 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }
        for (int i = 0; i < nPerGroup; i++)
        {
            double phase = VonMises(rng, kappa);
            network.AddNode(new TemporalNode(nPerGroup + i, phase, 1.0)
            { X = Math.Clamp(0.7 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * 0.05, 0.01, 0.99) });
        }

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);
        return network;
    }

    // ══════════════════════════════════════════════════════════════════
    // Von Mises generator (same as CriticalCoherenceAnalyzer)
    // ══════════════════════════════════════════════════════════════════

    private static double VonMises(Random rng, double kappa)
    {
        if (kappa < 0.01)
            return rng.NextDouble() * 2 * Math.PI;

        if (kappa > 5.0)
        {
            double u1 = rng.NextDouble(), u2 = rng.NextDouble();
            double sigma = 1.0 / Math.Sqrt(kappa);
            double z = Math.Sqrt(-2.0 * Math.Log(Math.Max(u1, 1e-15))) *
                       Math.Cos(2.0 * Math.PI * u2);
            double theta = z * sigma;
            theta %= 2.0 * Math.PI;
            if (theta < 0) theta += 2.0 * Math.PI;
            return theta;
        }

        double tau = 1.0 + Math.Sqrt(1.0 + 4.0 * kappa * kappa);
        double rho = (tau - Math.Sqrt(2.0 * tau)) / (2.0 * kappa);
        double r = (1.0 + rho * rho) / (2.0 * rho);

        for (int attempt = 0; attempt < 1000; attempt++)
        {
            double u1 = rng.NextDouble(), u2 = rng.NextDouble(), u3 = rng.NextDouble();
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
        return rng.NextDouble() * 2.0 * Math.PI;
    }

    // ══════════════════════════════════════════════════════════════════
    // Force computation for a single R point
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Computes all pair-wise force contributions and alignment
    /// statistics at a given target R.
    /// </summary>
    public static ForceAlignmentProfile ComputeForces(
        double targetR, string lawName, Func<double, double> forceFn,
        double k, double lambda, int nPerGroup, int seed)
    {
        var network = PrepareState(targetR, k, lambda, nPerGroup, seed);
        int n = network.NodeCount;
        double actualR = GlobalR(network);

        var allForces = new List<LocalForceContribution>();
        int totalPairs = 0;
        double sumMag = 0, sumMagSq = 0;
        double netFx = 0, netFy = 0;

        // Direction histogram: 36 bins of 10° each.
        int nDirBins = 36;
        int[] dirHist = new int[nDirBins];
        int nMagBins = 20;
        int[] magHist = new int[nMagBins];
        double maxMag = 0;

        int attractivePairs = 0, repulsivePairs = 0;

        // Compute forces from each oscillator in A toward each in B.
        // Force on oscillator i from j: F_ij * (r_j - r_i) / d_ij
        // Net force on group A: sum of all F_ij pointing toward group B.
        for (int i = 0; i < nPerGroup; i++)
        {
            for (int j = nPerGroup; j < n; j++)
            {
                double dx = network.Nodes[j].X - network.Nodes[i].X;
                double dy = network.Nodes[j].Y - network.Nodes[i].Y;
                double d = Math.Sqrt(dx * dx + dy * dy) + 1e-10;
                double w = network.Matrix.GetCoupling(i, j);

                double pd = TemporalSimulation.NormalizePhase(
                    network.Nodes[j].Phase - network.Nodes[i].Phase);
                if (pd > Math.PI) pd -= 2 * Math.PI;
                double fVal = forceFn(pd);

                // Force components (force on i from j).
                double fx = w * fVal * dx / d;
                double fy = w * fVal * dy / d;
                double mag = Math.Sqrt(fx * fx + fy * fy);
                double dir = Math.Atan2(fy, fx);
                if (dir < 0) dir += 2 * Math.PI;

                netFx += fx;
                netFy += fy;
                sumMag += mag;
                sumMagSq += mag * mag;
                totalPairs++;

                if (mag > maxMag) maxMag = mag;

                // Determine if attractive (forces moving A toward B).
                // dx, dy point from A(i) toward B(j). Force positive in
                // that direction = attractive (moves A toward B).
                double forceSign = Math.Sign(fx * dx + fy * dy);
                if (forceSign > 0) attractivePairs++;
                else if (forceSign < 0) repulsivePairs++;

                int dirBin = (int)(dir / (2 * Math.PI) * nDirBins);
                if (dirBin >= nDirBins) dirBin = nDirBins - 1;
                dirHist[dirBin]++;

                int magBin = maxMag > 0
                    ? (int)(mag / maxMag * nMagBins)
                    : 0;
                if (magBin >= nMagBins) magBin = nMagBins - 1;
                magHist[magBin]++;

                allForces.Add(new LocalForceContribution(
                    i, j, mag, dir, fx, fy, pd, w, forceSign));
            }
        }

        double netMag = Math.Sqrt(netFx * netFx + netFy * netFy);
        double netDir = Math.Atan2(netFy, netFx);
        if (netDir < 0) netDir += 2 * Math.PI;
        double cancRatio = sumMag > 1e-15 ? netMag / sumMag : 0;
        double meanMag = totalPairs > 0 ? sumMag / totalPairs : 0;
        double stdMag = totalPairs > 1
            ? Math.Sqrt(Math.Max(0, sumMagSq / totalPairs - meanMag * meanMag)) : 0;

        // Alignment: cos between each force direction and net force direction.
        double alignSum = 0, alignSumSq = 0;
        int alignedCount = 0;
        foreach (var f in allForces)
        {
            double cosA = Math.Cos(f.Direction - netDir);
            alignSum += cosA; alignSumSq += cosA * cosA;
            if (cosA > 0.5) alignedCount++;
        }
        double alignScore = totalPairs > 0 ? alignSum / totalPairs : 0;
        double alignStd = totalPairs > 1
            ? Math.Sqrt(Math.Max(0, alignSumSq / totalPairs - alignScore * alignScore)) : 0;

        double attFrac = totalPairs > 0 ? (double)attractivePairs / totalPairs : 0;

        // Sample: take first 200 forces.
        var sample = allForces.Take(200).ToList();

        return new ForceAlignmentProfile(targetR, actualR, lawName, seed,
            netFx, netFy, netMag, netDir,
            totalPairs, meanMag, stdMag, sumMag, cancRatio,
            alignScore, alignStd, (double)alignedCount / Math.Max(totalPairs, 1),
            attractivePairs, repulsivePairs, attFrac,
            dirHist.Select(c => (double)c).ToArray(),
            magHist.Select(c => (double)c).ToArray(),
            sample);
    }

    // ══════════════════════════════════════════════════════════════════
    // Aggregate analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Analyzes force alignment and cancellation across the R sweep.
    /// </summary>
    public static ForceSummationReport AnalyzeSummation(
        List<ForceAlignmentProfile> profiles)
    {
        var byR = profiles.GroupBy(p => p.TargetR).OrderBy(g => g.Key).ToList();

        // Extract per-R averages.
        var rVals = byR.Select(g => g.Key).ToList();
        var alignScores = byR.Select(g => g.Average(p => p.AlignmentScore)).ToList();
        var cancRatios = byR.Select(g => g.Average(p => p.CancellationRatio)).ToList();
        var netMags = byR.Select(g => g.Average(p => p.NetForceMagnitude)).ToList();

        double rAlign = Pearson(rVals, alignScores);
        double rCanc = Pearson(rVals, cancRatios);
        double rNetAlign = Pearson(alignScores, netMags);

        // Classification.
        string classification;
        if (rAlign > 0.7 && rCanc > 0.7)
            classification = "D: Coherent Summation Dominated";
        else if (rAlign > 0.5)
            classification = "C: Alignment Driven";
        else if (rCanc > 0.3)
            classification = "B: Partial Summation Effect";
        else
            classification = "A: Intrinsic Force (No Summation Effect)";

        string interpretation = classification switch
        {
            "D: Coherent Summation Dominated" =>
                "Attraction is almost entirely explained by force alignment. " +
                "Local forces exist at all R but cancel at low coherence. " +
                "As coherence increases, forces align and net attraction emerges. " +
                "Attraction is a COLLECTIVE COHERENCE PHENOMENON.",
            "C: Alignment Driven" =>
                "Force alignment is the dominant mechanism for attraction growth. " +
                "Cancellation at low R is the primary reason attraction is weak. " +
                "Coherence acts as a force-alignment mechanism.",
            "B: Partial Summation Effect" =>
                "Force alignment contributes to attraction but is not the full story. " +
                "Some intrinsic force scaling also occurs with coherence.",
            _ => "Net attraction does not primarily emerge from vector summation. " +
                 "The intrinsic force magnitude changes with coherence, or other " +
                 "mechanisms dominate the attraction growth."
        };

        return new ForceSummationReport(profiles, rAlign, rCanc, rNetAlign,
            classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full sweep
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs the full R sweep across all coupling laws.
    /// </summary>
    public static (List<ForceAlignmentProfile> Profiles, ForceSummationReport Report)
    RunFullForceAnalysis(
        double rMin, double rMax, double rStep,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        var profiles = new List<ForceAlignmentProfile>();
        int seedIdx = 0;

        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (var (lawName, fn) in ForceLaws)
        {
            foreach (double rT in rTargets)
            {
                for (int s = 0; s < seedsPerPoint; s++)
                {
                    profiles.Add(ComputeForces(rT, lawName, fn, k, lambda,
                        nPerGroup, baseSeed + seedIdx++ * 7919));
                }
            }
        }

        var report = AnalyzeSummation(profiles);
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

    private static double Pearson(List<double> x, List<double> y)
    {
        if (x.Count < 2) return 0;
        double mx = x.Average(), my = y.Average();
        double cov = 0, vx = 0, vy = 0;
        for (int i = 0; i < x.Count; i++)
        {
            double dx = x[i] - mx, dy = y[i] - my;
            cov += dx * dy; vx += dx * dx; vy += dy * dy;
        }
        double denom = Math.Sqrt(Math.Max(vx, 1e-15) * Math.Max(vy, 1e-15));
        return denom < 1e-15 ? 0 : cov / denom;
    }
}
