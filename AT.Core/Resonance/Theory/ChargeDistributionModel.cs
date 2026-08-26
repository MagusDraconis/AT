namespace AT.Core.Resonance.Theory;

/// <summary>
/// Distribution models for fitting P(Q) — the probability distribution
/// of topological charge Q = condensate count.
///
/// Tests models: Poisson, Binomial, Exponential, Power Law, Critical Scaling,
/// and automatically discovers the best fit.
///
/// AT-119: Topological Charge Creation Statistics
/// </summary>
public static class ChargeDistributionModel
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record DistributionFit(
        string ModelName,
        double PValue,
        double LogLikelihood,
        double AIC,
        double BIC,
        int NumParameters,
        double[] PredictedProbabilities);

    // ══════════════════════════════════════════════════════════════════
    // Fit all candidate distributions.
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Fits all candidate distributions and returns scores (higher = better fit).
    /// Score is 1 - normalized AIC (closer to 1 is better).
    /// </summary>
    public static Dictionary<string, double> FitAllDistributions(
        int[] histogram, int totalSamples, double mean)
    {
        var scores = new Dictionary<string, double>();

        // Fit each model.
        var poissonFit = FitPoisson(histogram, totalSamples, mean);
        var binomialFit = FitBinomial(histogram, totalSamples, mean);
        var exponentialFit = FitExponential(histogram, totalSamples, mean);
        var powerLawFit = FitPowerLaw(histogram, totalSamples, mean);
        var criticalFit = FitCriticalScaling(histogram, totalSamples, mean);
        var autoFit = AutoDiscoverDistribution(histogram, totalSamples, mean);

        // Collect AICs.
        var fits = new List<DistributionFit>
        {
            poissonFit, binomialFit, exponentialFit, powerLawFit, criticalFit, autoFit
        };

        double minAIC = fits.Min(f => f.AIC);
        double maxAIC = fits.Max(f => f.AIC);
        double range = maxAIC - minAIC;

        foreach (var fit in fits)
        {
            if (range < 1e-10)
                scores[fit.ModelName] = 1.0;
            else
                scores[fit.ModelName] = 1.0 - (fit.AIC - minAIC) / range;
        }

        return scores;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model A: Poisson  P(Q=k) = λ^k e^{-λ} / k!
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit FitPoisson(int[] histogram, int total, double mean)
    {
        double lambda = Math.Max(mean, 0.01);
        int maxK = histogram.Length - 1;

        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        for (int k = 0; k <= maxK; k++)
        {
            predicted[k] = PoissonPMF(k, lambda);
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);
        }

        // Normalize to handle truncation.
        double sum = predicted.Sum();
        for (int k = 0; k <= maxK; k++) predicted[k] /= sum;
        logLik -= total * Math.Log(sum);

        // Chi-squared test.
        double chi2 = 0;
        int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5)
            {
                chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected;
                dof++;
            }
        }
        dof = Math.Max(dof - 2, 1); // subtract 2: 1 for parameter + 1 for normalization

        double pValue = ChiSquareSurvival(chi2, dof);
        double aic = 2.0 * 1 - 2.0 * logLik; // 1 = lambda parameter
        double bic = Math.Log(total) * 1 - 2.0 * logLik;

        return new DistributionFit("Poisson", pValue, logLik, aic, bic, 1, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Model B: Binomial  P(Q=k) = C(n,k) p^k (1-p)^{n-k}
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit FitBinomial(int[] histogram, int total, double mean)
    {
        // Estimate n from variance: Var = n p (1-p), Mean = n p
        // → p = 1 - Var/Mean, n = Mean/p
        double variance = 0;
        double m2 = 0;
        for (int k = 0; k < histogram.Length; k++) m2 += histogram[k] * k * k;
        m2 /= total;
        variance = m2 - mean * mean;

        double p = variance < mean * 0.999 ? 1.0 - variance / mean : 0.5;
        p = Math.Clamp(p, 1e-6, 1 - 1e-6);
        int n = Math.Max((int)Math.Round(mean / p), (int)Math.Ceiling(mean) + 1);
        n = Math.Max(n, 1);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        for (int k = 0; k <= Math.Min(maxK, n); k++)
        {
            predicted[k] = BinomialPMF(k, n, p);
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);
        }

        double sum = predicted.Sum();
        for (int k = 0; k <= maxK; k++) predicted[k] /= sum;
        logLik -= total * Math.Log(sum);

        double chi2 = 0;
        int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5)
            {
                chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected;
                dof++;
            }
        }
        dof = Math.Max(dof - 3, 1); // 2 parameters + normalization

        double pValue = ChiSquareSurvival(chi2, dof);
        double aic = 2.0 * 2 - 2.0 * logLik;
        double bic = Math.Log(total) * 2 - 2.0 * logLik;

        return new DistributionFit("Binomial", pValue, logLik, aic, bic, 2, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Model C: Exponential / Geometric  P(Q=k) = (1-p) p^k
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit FitExponential(int[] histogram, int total, double mean)
    {
        double p = mean / (1.0 + mean); // Geometric parameter
        p = Math.Clamp(p, 1e-6, 0.999999);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        for (int k = 0; k <= maxK; k++)
        {
            predicted[k] = GeometricPMF(k, p);
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);
        }

        double sum = predicted.Sum();
        for (int k = 0; k <= maxK; k++) predicted[k] /= sum;
        logLik -= total * Math.Log(sum);

        double chi2 = 0;
        int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5)
            {
                chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected;
                dof++;
            }
        }
        dof = Math.Max(dof - 2, 1);

        double pValue = ChiSquareSurvival(chi2, dof);
        double aic = 2.0 * 1 - 2.0 * logLik;
        double bic = Math.Log(total) * 1 - 2.0 * logLik;

        return new DistributionFit("Exponential/Geometric", pValue, logLik, aic, bic, 1, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Model D: Power Law  P(Q=k) ∝ k^{-α}
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit FitPowerLaw(int[] histogram, int total, double mean)
    {
        // Estimate α from non-zero bins using MLE for discrete power law.
        double alpha = 2.0; // default
        double num = 0, den = 0;
        for (int k = 1; k < histogram.Length - 1; k++)
        {
            if (histogram[k] > 0)
            {
                num += histogram[k];
                den += histogram[k] * Math.Log(k);
            }
        }
        if (den > 0)
            alpha = 1.0 + num / den;
        alpha = Math.Clamp(alpha, 1.1, 5.0);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];

        // P(k=0) handled separately.
        predicted[0] = histogram[0] / (double)total;

        for (int k = 1; k <= maxK; k++)
            predicted[k] = Math.Pow(k, -alpha);

        double sumNonZero = predicted.Skip(1).Sum();
        for (int k = 1; k <= maxK; k++)
            predicted[k] = predicted[k] / sumNonZero * (1.0 - predicted[0]);

        double logLik = 0;
        for (int k = 0; k <= maxK; k++)
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);

        double chi2 = 0;
        int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5)
            {
                chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected;
                dof++;
            }
        }
        dof = Math.Max(dof - 2, 1); // 2 parameters: α + P(Q=0)

        double pValue = ChiSquareSurvival(chi2, dof);
        double aic = 2.0 * 2 - 2.0 * logLik;
        double bic = Math.Log(total) * 2 - 2.0 * logLik;

        return new DistributionFit("Power Law", pValue, logLik, aic, bic, 2, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Model E: Critical Scaling  P(Q) ∝ exp(-(Q-Qc)²/2σ²) for Q>0
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit FitCriticalScaling(int[] histogram, int total, double mean)
    {
        // Critical scaling: mixture of delta at Q=0 + Gaussian-like tail.
        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];

        double pQ0 = histogram[0] / (double)total;
        predicted[0] = pQ0;

        if (maxK >= 1)
        {
            // Fit a Gaussian-like tail for Q ≥ 1.
            double meanNonZero = 0, varNonZero = 0;
            double countNonZero = total - histogram[0];
            if (countNonZero > 0)
            {
                for (int k = 1; k <= maxK; k++)
                    meanNonZero += k * histogram[k];
                meanNonZero /= countNonZero;

                for (int k = 1; k <= maxK; k++)
                    varNonZero += histogram[k] * (k - meanNonZero) * (k - meanNonZero);
                varNonZero /= countNonZero;
            }

            double sigma = Math.Max(Math.Sqrt(varNonZero), 0.5);
            double mu = Math.Max(meanNonZero, 1.0);

            for (int k = 1; k <= maxK; k++)
            {
                double z = (k - mu) / sigma;
                predicted[k] = Math.Exp(-z * z / 2.0);
            }

            double tailSum = predicted.Skip(1).Sum();
            if (tailSum > 0)
                for (int k = 1; k <= maxK; k++)
                    predicted[k] = predicted[k] / tailSum * (1.0 - pQ0);
        }

        double logLik = 0;
        for (int k = 0; k <= maxK; k++)
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);

        double chi2 = 0;
        int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5)
            {
                chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected;
                dof++;
            }
        }
        dof = Math.Max(dof - 4, 1); // 4 parameters: pQ0, mu, sigma, tail normalization

        double pValue = ChiSquareSurvival(chi2, dof);
        double aic = 2.0 * 4 - 2.0 * logLik;
        double bic = Math.Log(total) * 4 - 2.0 * logLik;

        return new DistributionFit("Critical Scaling", pValue, logLik, aic, bic, 4, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Model F: Auto-Discovered (best of compound models).
    // ══════════════════════════════════════════════════════════════════

    public static DistributionFit AutoDiscoverDistribution(int[] histogram, int total, double mean)
    {
        // Try negative binomial (overdispersed count data).
        var nbFit = FitNegativeBinomial(histogram, total, mean);

        // Try zero-inflated Poisson.
        var zipFit = FitZeroInflatedPoisson(histogram, total, mean);

        // Try discrete Weibull-like.
        var dwFit = FitDiscreteWeibullLike(histogram, total, mean);

        // Return best by AIC.
        var candidates = new[] { nbFit, zipFit, dwFit };
        return candidates.OrderBy(f => f.AIC).First();
    }

    private static DistributionFit FitNegativeBinomial(int[] histogram, int total, double mean)
    {
        double variance = 0;
        double m2 = 0;
        for (int k = 0; k < histogram.Length; k++) m2 += histogram[k] * k * k;
        m2 /= total;
        variance = m2 - mean * mean;

        // NB: mean = r(1-p)/p, var = r(1-p)/p²
        // → p = mean/var, r = mean²/(var-mean) if var > mean
        double p, r;
        if (variance > mean * 1.1)
        {
            p = mean / variance;
            r = mean * mean / (variance - mean);
        }
        else
        {
            p = 0.5;
            r = mean;
        }
        p = Math.Clamp(p, 1e-6, 0.999999);
        r = Math.Max(r, 0.1);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        for (int k = 0; k <= maxK; k++)
        {
            predicted[k] = NB_PMF(k, r, p);
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);
        }

        double sum = predicted.Sum();
        for (int k = 0; k <= maxK; k++) predicted[k] /= sum;
        logLik -= total * Math.Log(sum);

        double aic = 2.0 * 2 - 2.0 * logLik;
        double bic = Math.Log(total) * 2 - 2.0 * logLik;

        double chi2 = 0; int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5) { chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected; dof++; }
        }
        dof = Math.Max(dof - 2, 1);
        double pValue = ChiSquareSurvival(chi2, dof);

        return new DistributionFit("Negative Binomial", pValue, logLik, aic, bic, 2, predicted);
    }

    private static DistributionFit FitZeroInflatedPoisson(int[] histogram, int total, double mean)
    {
        double pZero = histogram[0] / (double)total;
        double poissonMean = mean / Math.Max(1.0 - pZero, 0.01);
        poissonMean = Math.Max(poissonMean, 0.01);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        predicted[0] = pZero + (1.0 - pZero) * PoissonPMF(0, poissonMean);
        for (int k = 1; k <= maxK; k++)
            predicted[k] = (1.0 - pZero) * PoissonPMF(k, poissonMean);

        for (int k = 0; k <= maxK; k++)
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);

        double aic = 2.0 * 2 - 2.0 * logLik;
        double bic = Math.Log(total) * 2 - 2.0 * logLik;

        double chi2 = 0; int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5) { chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected; dof++; }
        }
        dof = Math.Max(dof - 2, 1);
        double pValue = ChiSquareSurvival(chi2, dof);

        return new DistributionFit("Zero-Inflated Poisson", pValue, logLik, aic, bic, 2, predicted);
    }

    private static DistributionFit FitDiscreteWeibullLike(int[] histogram, int total, double mean)
    {
        // Discrete Weibull-type: P(Q=k) ∝ exp(-(k/λ)^β) - exp(-((k+1)/λ)^β)
        double beta = 1.0;
        double lambda = mean / Gamma(1.0 + 1.0 / beta);
        lambda = Math.Max(lambda, 0.5);

        int maxK = histogram.Length - 1;
        double[] predicted = new double[maxK + 1];
        double logLik = 0;

        predicted[0] = histogram[0] / (double)total;
        for (int k = 1; k <= maxK; k++)
        {
            predicted[k] = Math.Exp(-Math.Pow(k / lambda, beta)) -
                           Math.Exp(-Math.Pow((k + 1) / lambda, beta));
            if (predicted[k] < 0) predicted[k] = 0;
        }

        double tailSum = predicted.Skip(1).Sum();
        if (tailSum > 0)
            for (int k = 1; k <= maxK; k++)
                predicted[k] = predicted[k] / tailSum * (1.0 - predicted[0]);

        for (int k = 0; k <= maxK; k++)
            if (histogram[k] > 0 && predicted[k] > 1e-15)
                logLik += histogram[k] * Math.Log(predicted[k]);

        double aic = 2.0 * 3 - 2.0 * logLik;
        double bic = Math.Log(total) * 3 - 2.0 * logLik;

        double chi2 = 0; int dof = 0;
        for (int k = 0; k <= maxK; k++)
        {
            double expected = predicted[k] * total;
            if (expected > 5) { chi2 += (histogram[k] - expected) * (histogram[k] - expected) / expected; dof++; }
        }
        dof = Math.Max(dof - 3, 1);
        double pValue = ChiSquareSurvival(chi2, dof);

        return new DistributionFit("Discrete Weibull-like", pValue, logLik, aic, bic, 3, predicted);
    }

    // ══════════════════════════════════════════════════════════════════
    // Probability mass functions.
    // ══════════════════════════════════════════════════════════════════

    private static double PoissonPMF(int k, double lambda)
    {
        if (k < 0) return 0;
        if (k > 170) return 0; // overflow protection
        double logP = -lambda + k * Math.Log(lambda) - LogFactorial(k);
        return Math.Exp(logP);
    }

    private static double BinomialPMF(int k, int n, double p)
    {
        if (k < 0 || k > n) return 0;
        if (p <= 0) return k == 0 ? 1.0 : 0.0;
        if (p >= 1) return k == n ? 1.0 : 0.0;
        double logP = LogCombination(n, k) + k * Math.Log(p) + (n - k) * Math.Log(1 - p);
        return Math.Exp(logP);
    }

    private static double GeometricPMF(int k, double p)
    {
        if (k < 0) return 0;
        return (1.0 - p) * Math.Pow(p, k);
    }

    private static double NB_PMF(int k, double r, double p)
    {
        if (k < 0) return 0;
        if (k > 170) return 0;
        double logP = LogGamma(k + r) - LogGamma(r) - LogFactorial(k)
                      + r * Math.Log(1.0 - p) + k * Math.Log(p);
        return Math.Exp(logP);
    }

    // ══════════════════════════════════════════════════════════════════
    // Statistical helpers.
    // ══════════════════════════════════════════════════════════════════

    private static double LogFactorial(int n)
    {
        if (n <= 1) return 0;
        // Stirling approximation for n > 10.
        if (n > 10)
            return n * Math.Log(n) - n + 0.5 * Math.Log(2.0 * Math.PI * n) +
                   1.0 / (12.0 * n) - 1.0 / (360.0 * n * n * n);

        double sum = 0;
        for (int i = 2; i <= n; i++) sum += Math.Log(i);
        return sum;
    }

    private static double LogCombination(int n, int k)
    {
        if (k < 0 || k > n) return double.NegativeInfinity;
        if (k == 0 || k == n) return 0;
        return LogFactorial(n) - LogFactorial(k) - LogFactorial(n - k);
    }

    private static double LogGamma(double x)
    {
        // Lanczos approximation for log Gamma.
        double[] coef = { 76.18009172947146, -86.50532032941677,
                          24.01409824083091, -1.231739572450155,
                          0.1208650973866179e-2, -0.5395239384953e-5 };
        double y = x;
        double tmp = x + 5.5;
        tmp -= (x + 0.5) * Math.Log(tmp);
        double ser = 1.000000000190015;
        for (int j = 0; j < 6; j++) ser += coef[j] / ++y;
        return -tmp + Math.Log(2.5066282746310005 * ser / x);
    }

    private static double Gamma(double x)
    {
        return Math.Exp(LogGamma(x));
    }

    /// <summary>
    /// Chi-squared survival function (approximate).
    /// </summary>
    private static double ChiSquareSurvival(double chi2, int dof)
    {
        if (dof <= 0) return 0;
        // Wilson-Hilferty approximation.
        double x = chi2 / dof;
        double z = (Math.Pow(x, 1.0 / 3.0) - 1.0 + 2.0 / (9.0 * dof)) /
                   Math.Sqrt(2.0 / (9.0 * dof));
        return 0.5 * (1.0 - Erf(z / Math.Sqrt(2.0)));
    }

    private static double Erf(double x)
    {
        // Abramowitz and Stegun approximation.
        double t = 1.0 / (1.0 + 0.47047 * Math.Abs(x));
        double poly = t * (0.3480242 + t * (-0.0958798 + t * 0.7478556));
        double result = 1.0 - poly * Math.Exp(-x * x);
        return x >= 0 ? result : -result;
    }
}
