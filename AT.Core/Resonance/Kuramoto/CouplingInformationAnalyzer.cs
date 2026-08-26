using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines which mathematical property of a coupling function
/// predicts effective spatial attraction.
///
/// AT-069: Coupling Information Principle
/// </summary>
public static class CouplingInformationAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Mathematical properties of a coupling function f: [-π,π] → R.
    /// </summary>
    public sealed record FunctionDescriptor(
        string Name,
        // Basic statistics
        double MeanValue,
        double Variance,
        double Entropy,
        // Symmetry
        double SymmetryScore,       // 0 = perfectly even, 1 = perfectly odd
        double EvenEnergyFraction,
        double OddEnergyFraction,
        // Shape
        int ZeroCrossings,
        double PositiveArea,
        double NegativeArea,
        double AreaRatio,           // positive / (|positive| + |negative|)
        // Derivatives
        double AvgGradient,         // mean |f'(x)|
        double AvgCurvature,        // mean |f''(x)|
        // Fourier
        double[] FourierSpectrum,   // energy per harmonic k=0..9
        double HighFreqEnergy,      // energy in k >= 3
        // Integral properties
        double TotalVariation,      // ∫|f'(x)|dx (same as AvgGradient * 2π)
        double L2Norm,              // sqrt(∫ f² dx)
        double L1Norm,              // ∫|f| dx
        // Info-theoretic
        double DifferentialEntropy);

    /// <summary>
    /// A coupling function paired with its measured attraction results.
    /// </summary>
    public sealed record AttractionPredictor(
        FunctionDescriptor Descriptor,
        // Measured outcomes
        double AttractionScore,
        double ConvergenceProbability,
        double SyncProbability,
        double FinalSeparation,
        double DriftVelocity);

    /// <summary>
    /// Feature importance ranking result.
    /// </summary>
    public sealed record FeatureRanking(
        string FeatureName,
        double PearsonR,
        double MutualInformation,
        double SpearmanR);

    /// <summary>
    /// Full analysis report.
    /// </summary>
    public sealed record InformationPrincipleReport(
        List<AttractionPredictor> Predictors,
        List<FeatureRanking> Rankings,
        string TopPredictor,
        double TopPredictorR,
        string Classification,
        string Interpretation,
        double BestSinglePredictorR);

    // ══════════════════════════════════════════════════════════════════
    // Constants
    // ══════════════════════════════════════════════════════════════════

    private const int SamplePoints = 200;
    private const int FourierHarmonics = 10;
    private const int EntropyBins = 50;
    private const int RandomFuncModes = 8;

    // ══════════════════════════════════════════════════════════════════
    // Named Coupling Functions
    // ══════════════════════════════════════════════════════════════════

    public static readonly Dictionary<string, Func<double, double>> NamedFunctions = new()
    {
        ["cos(x)"]              = x => Math.Cos(x),
        ["sin(x)"]              = x => Math.Sin(x),
        ["cos²(x)"]             = x => Math.Cos(x) * Math.Cos(x),
        ["exp(-|x|)"]           = x => Math.Exp(-Math.Abs(x)),
        ["1/(1+|x|)"]           = x => 1.0 / (1.0 + Math.Abs(x)),
        ["cos(x)*exp(-|x|)"]    = x => Math.Cos(x) * Math.Exp(-Math.Abs(x)),
        ["sign(cos(x))"]        = x => Math.Sign(Math.Cos(x)),
        ["1-|x|/π"]             = x => 1.0 - Math.Abs(x) / Math.PI,
    };

    // ══════════════════════════════════════════════════════════════════
    // Random Smooth Function Generation
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generates a random smooth function as a Fourier series
    /// with randomly weighted cos and sin terms.
    /// Coefficients decay as 1/k² for C² smoothness.
    /// </summary>
    public static Func<double, double> GenerateRandomSmoothFunction(int seed)
    {
        var rng = new Random(seed);
        int nModes = 3 + rng.Next(RandomFuncModes - 2); // 3..7 modes

        // Random coefficients with 1/k² decay.
        double[] a = new double[nModes + 1]; // cos coefficients (a₀..aₙ)
        double[] b = new double[nModes + 1]; // sin coefficients (b₀..bₙ)
        a[0] = (rng.NextDouble() * 2 - 1) * 0.3; // DC offset

        for (int k = 1; k <= nModes; k++)
        {
            double decay = 1.0 / (k * k);
            a[k] = (rng.NextDouble() * 2 - 1) * decay;
            b[k] = (rng.NextDouble() * 2 - 1) * decay;
        }

        // Normalize to unit L2 norm.
        double norm = Math.Sqrt(a[0] * a[0] * 2 * Math.PI +
            Enumerable.Range(1, nModes).Sum(k => Math.PI * (a[k] * a[k] + b[k] * b[k])));
        if (norm > 1e-10)
        {
            for (int k = 0; k <= nModes; k++) { a[k] /= norm; b[k] /= norm; }
        }

        return x =>
        {
            double sum = a[0];
            for (int k = 1; k <= nModes; k++)
                sum += a[k] * Math.Cos(k * x) + b[k] * Math.Sin(k * x);
            return sum;
        };
    }

    /// <summary>
    /// Generates n random smooth coupling functions.
    /// </summary>
    public static List<(string Name, Func<double, double> Fn)> GenerateRandomFunctions(
        int count, int baseSeed)
    {
        var result = new List<(string, Func<double, double>)>();
        for (int i = 0; i < count; i++)
        {
            var fn = GenerateRandomSmoothFunction(baseSeed + i * 7919);
            result.Add(($"R{i:D3}", fn));
        }
        return result;
    }

    // ══════════════════════════════════════════════════════════════════
    // Function Metrics Computation
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Computes all mathematical metrics for a coupling function.
    /// </summary>
    public static FunctionDescriptor ComputeMetrics(
        string name, Func<double, double> fn)
    {
        // Sample function on [-π, π].
        double[] xs = new double[SamplePoints];
        double[] ys = new double[SamplePoints];
        double dx = 2 * Math.PI / (SamplePoints - 1);

        for (int i = 0; i < SamplePoints; i++)
        {
            xs[i] = -Math.PI + i * dx;
            ys[i] = fn(xs[i]);
        }

        // ── Basic statistics ─────────────────────────────────────────
        double mean = 0;
        for (int i = 0; i < SamplePoints; i++) mean += ys[i];
        mean /= SamplePoints;

        double variance = 0;
        for (int i = 0; i < SamplePoints; i++)
        { double d = ys[i] - mean; variance += d * d; }
        variance /= SamplePoints;

        // L1, L2 norms (using trapezoidal integration).
        double l1 = 0, l2 = 0;
        for (int i = 0; i < SamplePoints; i++)
        { l1 += Math.Abs(ys[i]); l2 += ys[i] * ys[i]; }
        l1 *= dx; l2 = Math.Sqrt(l2 * dx);

        // ── Entropy (histogram-based) ────────────────────────────────
        double yMin = ys.Min(), yMax = ys.Max();
        double binWidth = (yMax - yMin + 1e-10) / EntropyBins;
        int[] hist = new int[EntropyBins];
        for (int i = 0; i < SamplePoints; i++)
        {
            int bin = (int)((ys[i] - yMin) / binWidth);
            if (bin >= EntropyBins) bin = EntropyBins - 1;
            if (bin < 0) bin = 0;
            hist[bin]++;
        }
        double entropy = 0;
        for (int b = 0; b < EntropyBins; b++)
        {
            double p = (double)hist[b] / SamplePoints;
            if (p > 0) entropy -= p * Math.Log(p);
        }

        // ── Symmetry ─────────────────────────────────────────────────
        double evenEnergy = 0, oddEnergy = 0;
        for (int i = 0; i < SamplePoints; i++)
        {
            int j = SamplePoints - 1 - i;
            double evenPart = (ys[i] + ys[j]) / 2;
            double oddPart = (ys[i] - ys[j]) / 2;
            evenEnergy += evenPart * evenPart;
            oddEnergy += oddPart * oddPart;
        }
        evenEnergy /= SamplePoints; oddEnergy /= SamplePoints;
        double totalEnergy = evenEnergy + oddEnergy + 1e-15;
        double evenFrac = evenEnergy / totalEnergy;
        double oddFrac = oddEnergy / totalEnergy;
        // SymmetryScore: 0 = perfectly even, 1 = perfectly odd.
        double symScore = oddFrac;

        // ── Zero crossings ───────────────────────────────────────────
        int zeroCrossings = 0;
        for (int i = 1; i < SamplePoints; i++)
            if (ys[i - 1] * ys[i] < 0) zeroCrossings++;

        // ── Positive / Negative area ─────────────────────────────────
        double posArea = 0, negArea = 0;
        for (int i = 0; i < SamplePoints; i++)
        {
            if (ys[i] > 0) posArea += ys[i];
            else negArea -= ys[i];
        }
        posArea *= dx; negArea *= dx;
        double areaRatio = posArea / (posArea + negArea + 1e-15);

        // ── Derivatives ──────────────────────────────────────────────
        double avgGrad = 0, avgCurv = 0;
        for (int i = 1; i < SamplePoints; i++)
            avgGrad += Math.Abs(ys[i] - ys[i - 1]);
        avgGrad /= (SamplePoints - 1);
        avgGrad /= dx; // scale to actual derivative magnitude

        for (int i = 2; i < SamplePoints; i++)
            avgCurv += Math.Abs(ys[i] - 2 * ys[i - 1] + ys[i - 2]);
        avgCurv /= (SamplePoints - 2);
        avgCurv /= (dx * dx);

        // ── Fourier spectrum ─────────────────────────────────────────
        double[] spectrum = new double[FourierHarmonics];
        // DC component.
        spectrum[0] = mean * mean;
        for (int k = 1; k < FourierHarmonics; k++)
        {
            double ak = 0, bk = 0;
            for (int i = 0; i < SamplePoints; i++)
            {
                ak += ys[i] * Math.Cos(k * xs[i]);
                bk += ys[i] * Math.Sin(k * xs[i]);
            }
            ak *= dx / (2 * Math.PI); bk *= dx / (2 * Math.PI);
            spectrum[k] = (ak * ak + bk * bk) / 2;
        }

        double highFreq = 0;
        for (int k = 3; k < FourierHarmonics; k++)
            highFreq += spectrum[k];

        // ── Differential entropy ─────────────────────────────────────
        // -∫ f(x) log f(x) with histogram.
        double diffEnt = 0;
        for (int b = 0; b < EntropyBins; b++)
        {
            double p = (double)hist[b] / SamplePoints;
            if (p > 0) diffEnt -= p * Math.Log(p / binWidth);
        }

        return new FunctionDescriptor(name, mean, variance, entropy,
            symScore, evenFrac, oddFrac, zeroCrossings, posArea, negArea,
            areaRatio, avgGrad, avgCurv, spectrum, highFreq,
            avgGrad * 2 * Math.PI, l2, l1, diffEnt);
    }

    // ══════════════════════════════════════════════════════════════════
    // Attraction Test
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs the spatial attraction simulation using the given coupling
    /// function as the position-dynamics force law.
    /// Phase dynamics always use sin(Δθ) (standard Kuramoto).
    /// </summary>
    public static AttractionPredictor RunAttractionTest(
        string name, Func<double, double> forceFn, double k, double lambda,
        int nPerGroup, int seed, int totalIters = 2000, double sepLambda = 0.5)
    {
        int n = nPerGroup * 2;
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);
        double sep = sepLambda * lambda;

        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = Math.Clamp(0.3 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });
        for (int i = 0; i < nPerGroup; i++)
            network.AddNode(new TemporalNode(nPerGroup + i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = Math.Clamp(0.3 + sep + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99),
              Y = Math.Clamp(0.5 + (rng.NextDouble() * 2 - 1) * lambda * 0.8, 0.01, 0.99) });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        double initSep = GroupSeparation(network, nPerGroup);

        for (int iter = 0; iter < totalIters; iter++)
        {
            // Phase update (standard Kuramoto with sin).
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

            // Position update using the test coupling function.
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
        double rA = GroupR(network, 0, nPerGroup);
        double rB = GroupR(network, nPerGroup, nPerGroup);

        double attrScore = Math.Clamp((initSep - finalSep) / Math.Max(initSep, 1e-10), -1, 1);
        bool syncs = rA > 0.8 && rB > 0.8;
        double driftVel = Math.Abs(initSep - finalSep) / totalIters;

        var descriptor = ComputeMetrics(name, forceFn);

        return new AttractionPredictor(descriptor, attrScore,
            attrScore > 0.5 ? 1.0 : 0.0, syncs ? 1.0 : 0.0, finalSep, driftVel);
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

    private static double GroupR(TemporalNetwork net, int start, int count)
    {
        double ss = 0, sc = 0;
        for (int i = start; i < start + count; i++)
        { ss += Math.Sin(net.Nodes[i].Phase); sc += Math.Cos(net.Nodes[i].Phase); }
        return Math.Sqrt(ss * ss + sc * sc) / count;
    }

    // ══════════════════════════════════════════════════════════════════
    // Feature Importance Analysis
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Performs feature importance ranking: Pearson r, mutual information,
    /// and Spearman rank correlation for each mathematical metric
    /// against attraction score.
    /// </summary>
    public static InformationPrincipleReport Analyze(
        List<AttractionPredictor> predictors)
    {
        // ── Extract feature vectors ──────────────────────────────────
        var featureNames = new (string Name, Func<AttractionPredictor, double> Extractor)[]
        {
            ("SymmetryScore (odd→0 even→1)", p => 1 - p.Descriptor.SymmetryScore),
            ("OddEnergyFraction", p => p.Descriptor.OddEnergyFraction),
            ("EvenEnergyFraction", p => p.Descriptor.EvenEnergyFraction),
            ("MeanValue", p => p.Descriptor.MeanValue),
            ("Variance", p => p.Descriptor.Variance),
            ("Entropy", p => p.Descriptor.Entropy),
            ("ZeroCrossings", p => p.Descriptor.ZeroCrossings),
            ("PositiveArea", p => p.Descriptor.PositiveArea),
            ("NegativeArea", p => p.Descriptor.NegativeArea),
            ("AreaRatio", p => p.Descriptor.AreaRatio),
            ("AvgGradient", p => p.Descriptor.AvgGradient),
            ("AvgCurvature", p => p.Descriptor.AvgCurvature),
            ("L2Norm", p => p.Descriptor.L2Norm),
            ("L1Norm", p => p.Descriptor.L1Norm),
            ("TotalVariation", p => p.Descriptor.TotalVariation),
            ("HighFreqEnergy", p => p.Descriptor.HighFreqEnergy),
            ("DifferentialEntropy", p => p.Descriptor.DifferentialEntropy),
            ("FourierDC", p => p.Descriptor.FourierSpectrum[0]),
            ("FourierK1", p => p.Descriptor.FourierSpectrum[1]),
            ("FourierK2", p => p.Descriptor.FourierSpectrum[2]),
        };

        var attrs = predictors.Select(p => p.AttractionScore).ToList();
        var rankings = new List<FeatureRanking>();

        foreach (var (fname, extract) in featureNames)
        {
            var fvals = predictors.Select(extract).ToList();
            double pearsonR = Pearson(fvals, attrs);
            double spearmanR = Spearman(fvals, attrs);
            double mi = MutualInfo(fvals, attrs);

            rankings.Add(new FeatureRanking(fname, pearsonR, mi, spearmanR));
        }

        // Sort by absolute Pearson r descending.
        rankings = rankings.OrderByDescending(r => Math.Abs(r.PearsonR)).ToList();
        var top = rankings[0];

        double maxAbs = rankings.Max(r => Math.Abs(r.PearsonR));

        string classification = maxAbs > 0.7 ? "D: Universal Coupling Principle" :
                                maxAbs > 0.5 ? "C: Strong Predictor" :
                                maxAbs > 0.3 ? "B: Weak Predictor" :
                                "A: No Predictor";

        string interpretation = classification switch
        {
            "D: Universal Coupling Principle" =>
                $"A single mathematical property ({top.FeatureName}) " +
                $"strongly predicts attraction (r={top.PearsonR:F3}). " +
                "This property constitutes a universal coupling principle.",
            "C: Strong Predictor" =>
                $"{top.FeatureName} is the strongest predictor of attraction " +
                $"(r={top.PearsonR:F3}). Attraction can be predicted from " +
                "function properties with good accuracy.",
            "B: Weak Predictor" =>
                $"{top.FeatureName} has a detectable but modest correlation " +
                $"with attraction (r={top.PearsonR:F3}). Multiple properties " +
                "may jointly determine attraction.",
            _ => "No single mathematical property strongly predicts attraction. " +
                 "Attraction may emerge from the interaction of multiple properties, " +
                 "or may be a dynamical rather than static property of coupling functions."
        };

        return new InformationPrincipleReport(predictors, rankings,
            top.FeatureName, top.PearsonR, classification, interpretation, maxAbs);
    }

    // ══════════════════════════════════════════════════════════════════
    // Statistical helpers
    // ══════════════════════════════════════════════════════════════════

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

    private static double Spearman(List<double> x, List<double> y)
    {
        int n = x.Count;
        if (n < 2) return 0;

        int[] rankX = Rank(x), rankY = Rank(y);
        double sumD2 = 0;
        for (int i = 0; i < n; i++)
        { double d = rankX[i] - rankY[i]; sumD2 += d * d; }
        return 1.0 - 6.0 * sumD2 / (n * (n * n - 1.0));
    }

    private static int[] Rank(List<double> vals)
    {
        int n = vals.Count;
        var indexed = vals.Select((v, i) => (v, i)).OrderBy(t => t.v).ToList();
        int[] ranks = new int[n];
        for (int i = 0; i < n; i++) ranks[indexed[i].i] = i + 1;
        return ranks;
    }

    /// <summary>
    /// Approximate mutual information via discretized joint histogram.
    /// </summary>
    private static double MutualInfo(List<double> x, List<double> y, int bins = 20)
    {
        int n = x.Count;
        if (n < 2) return 0;

        double xMin = x.Min(), xMax = x.Max(), xRange = xMax - xMin + 1e-15;
        double yMin = y.Min(), yMax = y.Max(), yRange = yMax - yMin + 1e-15;

        int[,] joint = new int[bins, bins];
        int[] margX = new int[bins], margY = new int[bins];

        for (int i = 0; i < n; i++)
        {
            int bx = Math.Clamp((int)((x[i] - xMin) / xRange * bins), 0, bins - 1);
            int by = Math.Clamp((int)((y[i] - yMin) / yRange * bins), 0, bins - 1);
            joint[bx, by]++; margX[bx]++; margY[by]++;
        }

        double mi = 0;
        for (int bx = 0; bx < bins; bx++)
        {
            for (int by = 0; by < bins; by++)
            {
                if (joint[bx, by] == 0) continue;
                double pxy = (double)joint[bx, by] / n;
                double px = (double)margX[bx] / n;
                double py = (double)margY[by] / n;
                mi += pxy * Math.Log(pxy / (px * py));
            }
        }
        return mi / Math.Log(2); // bits
    }

    /// <summary>
    /// Runs the full coupling-function analysis pipeline: named + random functions.
    /// </summary>
    public static (
        List<AttractionPredictor> NamedResults,
        List<AttractionPredictor> RandomResults)
    RunFullAnalysis(
        double k, double lambda, int nPerGroup, int seedsPerFunc,
        int randomFuncCount, int baseSeed)
    {
        var namedResults = new List<AttractionPredictor>();
        var randomResults = new List<AttractionPredictor>();

        int seedIdx = 0;

        // Named functions.
        foreach (var (name, fn) in NamedFunctions)
        {
            for (int s = 0; s < seedsPerFunc; s++)
            {
                namedResults.Add(RunAttractionTest(name, fn, k, lambda, nPerGroup,
                    baseSeed + seedIdx++ * 7919));
            }
        }

        // Random functions.
        var randomFns = GenerateRandomFunctions(randomFuncCount, baseSeed + 100000);
        foreach (var (name, fn) in randomFns)
        {
            for (int s = 0; s < seedsPerFunc; s++)
            {
                randomResults.Add(RunAttractionTest(name, fn, k, lambda, nPerGroup,
                    baseSeed + seedIdx++ * 7919));
            }
        }

        return (namedResults, randomResults);
    }
}
