using AT.Core.ResearchT;

namespace AT.Tests.Shared;

/// <summary>
/// Analytical laws for the surviving-mode count N_fit of the replicator–mutator
/// (BoundedInnovationAnalyzer), shared by the T-program (T_009/T_010).
///
/// The model has two exactly-solvable limits:
///   · μ = 0 (pure selection + crowding): S∞ = #{k : w_k > Z*(β)}, where Z* is the unique
///     root of Σ_{w_k>Z}(w_k − Z) = β·Z.
///   · β = 0 (mutation + selection, no crowding): S∞ = #{k : Perron(M·diag(w))_k > ε},
///     M the ring mutation matrix.
/// plus a small-μ uniform-gap reach N_fit ≈ 1 + log(1/ε)/log(2δ/μ), δ = Δw/w*.
/// Deterministic throughout.
/// </summary>
public static class FitnessReachLaw
{
    public const double Eps = BoundedInnovationAnalyzer.DefaultExtinctionThreshold;

    /// <summary>Unique root of Σ_{w_k&gt;Z}(w_k − Z) = β·Z in (0, w_max) — the μ=0 crowding threshold.</summary>
    public static double ThresholdZ(double[] w, double beta)
    {
        double lo = 0.0, hi = w.Max();
        for (int i = 0; i < 200; i++)
        {
            double mid = 0.5 * (lo + hi);
            double g = -beta * mid;
            foreach (double x in w) if (x > mid) g += x - mid;
            if (g > 0.0) lo = mid; else hi = mid;
        }
        return 0.5 * (lo + hi);
    }

    /// <summary>Exact μ=0 species count: modes whose abundance (w_k/Z − 1)/β exceeds ε.</summary>
    public static int PredictedZeroMutation(double[] w, double beta)
    {
        double z = ThresholdZ(w, beta);
        return w.Count(x => x > z * (1.0 + beta * Eps));
    }

    /// <summary>Perron (dominant) eigenvector of the linear operator M·diag(w) for the β=0 case.</summary>
    public static double[] PerronVector(double[] w, double mu)
    {
        int a = w.Length;
        var x = Enumerable.Repeat(1.0 / a, a).ToArray();
        for (int iter = 0; iter < 200000; iter++)
        {
            var y = new double[a];
            for (int i = 0; i < a; i++)
            {
                double s = (1.0 - mu) * w[i] * x[i];
                if (a > 1)
                {
                    s += (mu / 2.0) * w[(i - 1 + a) % a] * x[(i - 1 + a) % a];
                    s += (mu / 2.0) * w[(i + 1) % a] * x[(i + 1) % a];
                }
                else s += mu * w[i] * x[i];
                y[i] = s;
            }
            double norm = y.Sum();
            if (norm <= 0.0) break;
            double diff = 0.0;
            for (int i = 0; i < a; i++) { y[i] /= norm; diff += Math.Abs(y[i] - x[i]); }
            x = y;
            if (diff < 1e-15) break;
        }
        return x;
    }

    /// <summary>Exact β=0 species count: components of the Perron vector above the extinction threshold.</summary>
    public static int PredictedLinearEquilibrium(double[] w, double mu)
        => PerronVector(w, mu).Count(x => x > Eps);

    /// <summary>Top fitness w*, second-distinct fitness, and the relative gap δ = Δw/w*.</summary>
    public static (double wStar, double wSecond, double delta) TopGap(double[] w)
    {
        double[] distinct = w.Distinct().OrderByDescending(x => x).ToArray();
        double star = distinct[0];
        double second = distinct.Length > 1 ? distinct[1] : star;
        return (star, second, star > 0.0 ? (star - second) / star : 0.0);
    }

    /// <summary>Small-μ uniform-gap reach: N_fit ≈ 1 + log(1/ε)/log(2δ/μ).</summary>
    public static int PredictedUniformGap(double[] w, double mu)
    {
        double delta = TopGap(w).delta;
        if (delta <= 0.0) return w.Length;
        double reach = Math.Log(1.0 / Eps) / Math.Log(2.0 * delta / mu);
        return Math.Max(1, 1 + (int)Math.Floor(reach));
    }
}
