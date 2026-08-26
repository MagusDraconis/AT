namespace AT.Core.ResearchQG;

/// <summary>QG-093 Monte Carlo: sample H0 (and hence N) over a realistic range to measure the
/// robustness of the Λ ~ 1/√N amplitude α = O(1). Deterministic (fixed seed).</summary>
public static class LambdaMonteCarloAnalyzer
{
    public static (double H0, double Alpha)[] Run(int nReal = 100000, int seed = 42)
    {
        var rng = new Random(seed);
        var result = new (double, double)[nReal];
        for (int i = 0; i < nReal; i++)
        {
            // H0 ∈ [65, 75] km/s/Mpc (Gaussian-ish box; realistic range).
            double h0 = 65.0 + 10.0 * rng.NextDouble();
            double alpha = CausalSetLambdaModel.AmplitudeAlpha(h0);
            result[i] = (h0, alpha);
        }
        return result;
    }

    /// <summary>Summary statistics of α over the Monte Carlo sample.</summary>
    public static (double Min, double Max, double Mean, double Std) Summary((double H0, double Alpha)[] samples)
    {
        var a = samples.Select(s => s.Alpha).ToArray();
        return (a.Min(), a.Max(), a.Average(), Std(a));
    }

    private static double Std(double[] v)
    {
        double m = v.Average();
        return Math.Sqrt(v.Average(x => (x - m) * (x - m)));
    }
}
