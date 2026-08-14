namespace TQM.Core.ResearchQG;

/// <summary>QG-097 literature estimates of the RAR acceleration scale a₀ (MOND a₀ / g†), with
/// their uncertainties. These feed the Bayesian comparison of the dimensionless factor x = a₀/(cH)
/// against the candidate O(1) factors 1/(2π), 1/5, 1/6, 1/7.</summary>
public sealed record A0Estimate(string Source, double A0_e10_m_s2, double Sigma_e10_m_s2);

public sealed record FactorCandidate(string Name, double Value);

public sealed record FactorComparisonRow(string Candidate, double Value, double Chi2, double Likelihood,
    double BayesFactorVsBest, double NSigmaFromObserved);

public static class FactorComparisonModel
{
    public const double C = 299792458.0;
    public const double H0 = 67.4;
    public const double Kpc_m = 3.0857e19;
    public static double CH => C * (H0 / Kpc_m); // 6.55e-10

    /// <summary>Historical a₀ estimates (×1e-10 m/s²).</summary>
    public static A0Estimate[] Estimates() => new[]
    {
        new A0Estimate("Begeman+1991", 1.21, 0.14),
        new A0Estimate("McGaugh+2016", 1.20, 0.20),
        new A0Estimate("Lelli+2017", 1.20, 0.15),
        new A0Estimate("Rodrigues+2018", 1.00, 0.10),
        new A0Estimate("Li+2018 (RAR)", 1.20, 0.15),
        new A0Estimate("Chae 2023 (binaries)", 1.20, 0.30),
    };

    /// <summary>Candidate O(1) factors.</summary>
    public static FactorCandidate[] Candidates() => new[]
    {
        new FactorCandidate("1/(2π)", 1.0 / (2.0 * Math.PI)),
        new FactorCandidate("1/5", 0.20),
        new FactorCandidate("1/6", 1.0 / 6.0),
        new FactorCandidate("1/7", 1.0 / 7.0),
        new FactorCandidate("1/4", 0.25),
        new FactorCandidate("1/8", 0.125),
        new FactorCandidate("1/3", 1.0 / 3.0),
    };

    /// <summary>Observed x = a₀/(cH) and its 1σ (from the combined estimates, weighted).</summary>
    public static (double X, double Sigma) ObservedX()
    {
        // Weighted mean of x_i = a0_i/(cH).
        double sw = 0, swx = 0;
        foreach (var e in Estimates())
        {
            double x = e.A0_e10_m_s2 * 1e-10 / CH;
            double sx = e.Sigma_e10_m_s2 * 1e-10 / CH;
            double w = 1.0 / (sx * sx);
            sw += w;
            swx += w * x;
        }
        double xbar = swx / sw;
        double sigma = 1.0 / Math.Sqrt(sw);
        return (xbar, sigma);
    }

    /// <summary>Chi² of each candidate against the observed x (Gaussian).</summary>
    public static FactorComparisonRow[] Comparison()
    {
        var (x, sigma) = ObservedX();
        var rows = new List<FactorComparisonRow>();
        foreach (var c in Candidates())
        {
            double chi2 = Math.Pow((x - c.Value) / sigma, 2);
            double like = Math.Exp(-0.5 * chi2);
            rows.Add(new FactorComparisonRow(c.Name, c.Value, chi2, like, 0, (x - c.Value) / sigma));
        }
        double bestLike = rows.Max(r => r.Likelihood);
        return rows.Select(r => r with { BayesFactorVsBest = r.Likelihood / bestLike }).ToArray();
    }
}
