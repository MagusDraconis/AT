namespace TQM.Core.ResearchXB.Models;

/// <summary>
/// Models abundance as arising from multiplicative actualization cascades.
/// ResearchXB-002
/// </summary>
public static class MultiplicativeCascade
{
    /// <summary>
    /// Simulate a multiplicative actualization cascade.
    /// Each step: X_{n+1} = X_n * exp(ε_n) where ε_n ~ N(0,σ²).
    /// After N steps: log(X_N) ~ N(μ, N·σ²) → X_N is LOG-NORMAL.
    /// </summary>
    public static double[] GenerateAbundanceValues(int count, int cascadeSteps, double sigma)
    {
        var rng = new Random(42);
        var values = new double[count];

        for (int i = 0; i < count; i++)
        {
            double logX = 0;
            for (int step = 0; step < cascadeSteps; step++)
                logX += sigma * (rng.NextDouble() - 0.5) * 2.0 * Math.Sqrt(3); // approx N(0,σ²)
            values[i] = Math.Exp(logX);
        }

        return values;
    }

    /// <summary>
    /// Check if a dataset is consistent with log-normal.
    /// </summary>
    public static (double mean, double std, bool isLogNormal) TestLogNormality(double[] values)
    {
        double[] logValues = values.Select(v => Math.Log(v)).ToArray();
        double mean = logValues.Average();
        double std = Math.Sqrt(logValues.Select(lv => (lv - mean) * (lv - mean)).Average());

        // Shapiro-Wilk simplified: check skewness and kurtosis
        double skew = logValues.Select(lv => Math.Pow((lv - mean) / std, 3)).Average();
        double kurt = logValues.Select(lv => Math.Pow((lv - mean) / std, 4)).Average();

        bool isLogNormal = Math.Abs(skew) < 0.5 && Math.Abs(kurt - 3.0) < 1.0;

        return (mean, std, isLogNormal);
    }

    public static string CascadeAnalysis(int steps, double sigma, int samples)
    {
        var values = GenerateAbundanceValues(samples, steps, sigma);
        var (mean, std, isLogNormal) = TestLogNormality(values);

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"MULTIPLICATIVE CASCADE: {steps} steps, σ={sigma:F2}, {samples} samples");
        sb.AppendLine($"  log(X) mean: {mean:F3}, std: {std:F3}");
        sb.AppendLine($"  Distribution: {(isLogNormal ? "LOG-NORMAL ✓" : "not log-normal")}");
        sb.AppendLine($"  Percentiles: P10={Math.Exp(mean - 1.28 * std):F3}, "
                     + $"P50={Math.Exp(mean):F3}, P90={Math.Exp(mean + 1.28 * std):F3}");
        sb.AppendLine();
        sb.AppendLine("  INTERPRETATION: If abundance = accumulated actualization history,");
        sb.AppendLine("  and each actualization multiplies by a random factor ~O(1),");
        sb.AppendLine("  then the CENTRAL LIMIT THEOREM (in log-space) guarantees");
        sb.AppendLine("  log-normal distributions for all abundance quantities.");
        return sb.ToString();
    }
}
