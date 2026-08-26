namespace AT.Core.ResearchQG;

/// <summary>QG-085 coincidence analysis: the dimensionless factor a0/(cH) = 0.184 compared
/// against 'nice' O(1) candidates (1/2π, 1/6, 1/5, 1/4, 1/8, 1/3). Quantifies the probability
/// that a random dimensionless factor lands near one of several nice numbers.</summary>
public static class CoincidenceAnalyzer
{
    public static double A0OverCH() => LocalCosmicCoupling.A0_Mond / LocalCosmicCoupling.CH;

    public static (string Candidate, double Value, double RatioToObserved, double LogMismatch)[] NiceNumbers()
    {
        double obs = A0OverCH();
        var candidates = new (string, double)[]
        {
            ("1/(2π)", 1.0 / (2.0 * Math.PI)),
            ("1/6", 1.0 / 6.0),
            ("1/5", 1.0 / 5.0),
            ("1/4", 1.0 / 4.0),
            ("1/8", 1.0 / 8.0),
            ("1/3", 1.0 / 3.0),
        };
        return candidates.Select(c => (c.Item1, c.Item2, obs / c.Item2,
            Math.Abs(Math.Log10(obs / c.Item2)))).ToArray();
    }

    /// <summary>Best 'nice-number' match and its fractional mismatch.</summary>
    public static (string Candidate, double FractionalMismatch) BestMatch()
    {
        var n = NiceNumbers();
        var best = n.OrderBy(x => x.LogMismatch).First();
        return (best.Candidate, best.RatioToObserved - 1.0);
    }

    /// <summary>P(random factor within ±16% of any of the 4 nearest nice numbers), 2-decade prior.</summary>
    public static double CoincidenceProbability()
    {
        double priorDex = 2.0;      // dimensionless factor log-uniform over [0.01, 1]
        double fracDex = 2.0 * Math.Log10(1.16); // ±16% window
        int niceCount = 4;          // candidates within ~[0.12, 0.25]
        return Math.Min(1.0, niceCount * fracDex / priorDex);
    }
}
