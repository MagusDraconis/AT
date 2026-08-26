namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Evaluates composite order parameter candidates for resonance condensation
/// and ranks them by predictive universality.
/// </summary>
public static class OrderParameterAnalyzer
{
    public sealed record CandidateResult(
        string Name, double GlobalCV, double LambdaCV, double PlacementCV, double NCV, double KCV,
        double MeanValue, double SEM);

    public static List<CandidateResult> EvaluateAll(
        List<(double Density, double Neighbors, double WeightedNeighbors, double Clustering, double K,
               int N, double Lambda, string Placement)> data)
    {
        var candidates = new List<(string Name, Func<double, double, double, double, double, double> Formula)>
        {
            ("P1 Density",              (d, n, w, c, k) => d),
            ("P2 NeighborCount",        (d, n, w, c, k) => n),
            ("P3 Density×N",            (d, n, w, c, k) => d * n),
            ("P4 Density×N×K",          (d, n, w, c, k) => d * n * k),
            ("P5 Density×√N",           (d, n, w, c, k) => d * Math.Sqrt(n)),
            ("P6 Density×N / Cluster",  (d, n, w, c, k) => c > 0.01 ? d * n / c : d * n),
            ("P7 WeightedN",            (d, n, w, c, k) => w),
            ("P8 WeightedN×Density",    (d, n, w, c, k) => w * d),
        };

        var results = new List<CandidateResult>();

        foreach (var (name, formula) in candidates)
        {
            var values = data.Select(d => formula(d.Density, d.Neighbors, d.WeightedNeighbors, d.Clustering, d.K)).ToList();
            if (values.Count < 2) continue;

            double mean = values.Average();
            double std = Math.Sqrt(values.Average(v => (v - mean) * (v - mean)));
            double globalCV = mean > 1e-10 ? std / mean : 0;
            double sem = std / Math.Sqrt(values.Count);

            double lambdaCV = GroupCV(data, values, d => d.Lambda);
            double placementCV = GroupCV(data, values, d => d.Placement);
            double nCV = GroupCV(data, values, d => d.N);
            double kCV = GroupCV(data, values, d => d.K);

            results.Add(new CandidateResult(name, globalCV, lambdaCV, placementCV, nCV, kCV, mean, sem));
        }

        return results;
    }

    private static double GroupCV<T>(
        List<(double Density, double Neighbors, double WeightedNeighbors,
        double Clustering, double K, int N, double Lambda, string Placement)> data,
        List<double> values, Func<(double Density, double Neighbors, double WeightedNeighbors,
        double Clustering, double K, int N, double Lambda, string Placement), T> keySelector)
    {
        var groups = data.Zip(values, (d, v) => (Key: keySelector(d), Value: v))
            .GroupBy(x => x.Key)
            .Select(g => g.Select(x => x.Value).Average())
            .Where(m => m > 1e-10)
            .ToList();

        if (groups.Count < 2) return 0;
        double mean = groups.Average();
        double std = Math.Sqrt(groups.Average(g => (g - mean) * (g - mean)));
        return mean > 1e-10 ? std / mean : 0;
    }
}
