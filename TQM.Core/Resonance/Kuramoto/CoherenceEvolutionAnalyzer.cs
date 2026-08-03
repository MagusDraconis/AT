using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether coherence evolution dR/dt follows
/// a closed analytic equation f(R, K, law).
///
/// TQM-078: Coherence Evolution Law
/// </summary>
public static class CoherenceEvolutionAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record CoherenceGrowthProfile(
        double K,
        double InitialR,
        double R,
        double dRdt,
        double Timestep,
        string LawName);

    public sealed record EvolutionFit(
        string Name,
        string Formula,
        double R2,
        double RMSE,
        double AIC,
        double BIC,
        double[] Parameters);

    public sealed record EvolutionLawReport(
        List<EvolutionFit> Fits,
        string BestModel,
        double BestR2,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Run coherence evolution
    // ══════════════════════════════════════════════════════════════════

    public static List<CoherenceGrowthProfile> RunEvolution(
        double k, double lambda, int n, int seed,
        int totalSteps = 500, int recordEvery = 1)
    {
        var rng = new Random(seed);
        var network = new TemporalNetwork(n);

        // Random positions, random initial phases (controlled by seed only).
        for (int i = 0; i < n; i++)
            network.AddNode(new TemporalNode(i, rng.NextDouble() * 2 * Math.PI, 1.0)
            { X = rng.NextDouble(), Y = rng.NextDouble() });

        network.Matrix.FillSpatialCoupling(network.Nodes, k, lambda, normalize: false);

        var profiles = new List<CoherenceGrowthProfile>();
        double prevR = GlobalR(network);
        double initR = prevR;

        for (int iter = 0; iter < totalSteps; iter++)
        {
            // Pure Kuramoto phase update (no position dynamics).
            int nCount = network.NodeCount;
            for (int i = 0; i < nCount; i++)
            {
                double sum = 0;
                for (int j = 0; j < nCount; j++)
                {
                    if (i == j) continue;
                    sum += network.Matrix.GetCoupling(i, j) *
                           Math.Sin(network.Nodes[j].Phase - network.Nodes[i].Phase);
                }
                network.Nodes[i].Phase = TemporalSimulation.NormalizePhase(
                    network.Nodes[i].Phase + 0.01 * (network.Nodes[i].Frequency + sum));
            }

            if (iter % recordEvery == 0)
            {
                double r = GlobalR(network);
                double dr = iter > 0 ? (r - prevR) / recordEvery : 0;
                profiles.Add(new CoherenceGrowthProfile(k, initR, r, dr, iter, ""));
                prevR = r;
            }
        }

        return profiles;
    }

    // ══════════════════════════════════════════════════════════════════
    // Batch data generation
    // ══════════════════════════════════════════════════════════════════

    public static List<CoherenceGrowthProfile> GenerateEvolutionData(
        double[] kValues, double lambda, int n, int seedsPerK, int baseSeed,
        int totalSteps = 500, int recordEvery = 2)
    {
        var all = new List<CoherenceGrowthProfile>();
        int seedIdx = 0;

        foreach (double k in kValues)
        {
            for (int s = 0; s < seedsPerK; s++)
            {
                all.AddRange(RunEvolution(k, lambda, n,
                    baseSeed + seedIdx++ * 7919, totalSteps, recordEvery));
            }
        }

        return all;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model fitting
    // ══════════════════════════════════════════════════════════════════

    public static EvolutionLawReport FitEvolutionLaws(
        List<CoherenceGrowthProfile> data, double kValue)
    {
        int n = data.Count;
        double[] R = data.Select(d => d.R).ToArray();
        double[] dR = data.Select(d => d.dRdt).ToArray();

        // Filter to R > 0.01 and dR > 1e-10 (meaningful range).
        var valid = Enumerable.Range(0, n)
            .Where(i => R[i] > 0.01 && R[i] < 0.99)
            .Select(i => (R[i], dR[i]))
            .ToList();
        if (valid.Count < 10)
            return new EvolutionLawReport(new List<EvolutionFit>(), "", 0,
                "A: No Evolution Law", "Insufficient data.");

        double[] rV = valid.Select(v => v.Item1).ToArray();
        double[] drV = valid.Select(v => v.Item2).ToArray();

        var fits = new List<EvolutionFit>();

        // Model A: dR/dt = a·R
        fits.Add(FitLinearOne("A: Linear", "dR/dt = a·R", rV, drV, r => r));

        // Model B: dR/dt = a·R·(1-R)  (logistic)
        fits.Add(FitLinearOne("B: Logistic", "dR/dt = a·R(1-R)",
            rV, drV, r => r * (1 - r)));

        // Model C: dR/dt = a·Rⁿ·(1-R)
        fits.Add(FitPowerLogistic("C: Gen Logistic", "dR/dt = a·Rⁿ(1-R)", rV, drV));

        // Model D: dR/dt = a·K·R·(1-R)
        fits.Add(FitLinearOne("D: K-Logistic", "dR/dt = a·K·R(1-R)",
            rV, drV, r => kValue * r * (1 - r)));

        // Model E: dR/dt = a·K·Rⁿ·(1-R)
        fits.Add(FitKPowerLogistic("E: Full Model", "dR/dt = a·K·Rⁿ(1-R)", rV, drV, kValue));

        fits = fits.OrderByDescending(f => f.R2).ToList();
        var best = fits[0];

        string classification = best.R2 > 0.80 ? "D: Universal Evolution Eq" :
                                best.R2 > 0.60 ? "C: Strong Analytic Law" :
                                best.R2 > 0.40 ? "B: Empirical Growth Law" :
                                "A: No Evolution Law";

        string interp = classification switch
        {
            "D: Universal Evolution Eq" =>
                $"Coherence evolution follows {best.Formula} (R²={best.R2:F3}). " +
                "The growth law is deterministic and universal.",
            "C: Strong Analytic Law" =>
                $"A strong analytic law exists: {best.Formula} (R²={best.R2:F3}). " +
                "Coherence evolution is largely predictable.",
            "B: Empirical Growth Law" =>
                $"An approximate growth law fits (R²={best.R2:F3}). " +
                "Some predictability exists.",
            _ => "No simple law captures coherence evolution. " +
                 "The growth process may be more complex."
        };

        return new EvolutionLawReport(fits, best.Name, best.R2,
            classification, interp);
    }

    // ══════════════════════════════════════════════════════════════════
    // Fitting helpers
    // ══════════════════════════════════════════════════════════════════

    private static EvolutionFit FitLinearOne(string name, string formula,
        double[] R, double[] dR, Func<double, double> feature)
    {
        double[] X = R.Select(feature).ToArray();
        double sumXY = 0, sumX2 = 0;
        for (int i = 0; i < X.Length; i++)
        { sumXY += X[i] * dR[i]; sumX2 += X[i] * X[i]; }
        double a = sumX2 > 1e-15 ? sumXY / sumX2 : 0;

        double ssRes = 0, ssTot = 0, mean = dR.Average();
        for (int i = 0; i < dR.Length; i++)
        { double pred = a * X[i]; ssRes += (dR[i] - pred) * (dR[i] - pred); ssTot += (dR[i] - mean) * (dR[i] - mean); }
        double r2 = ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
        double rmse = Math.Sqrt(ssRes / dR.Length);
        int n = dR.Length, k = 1;
        double s2 = rmse * rmse;
        double aic = n * Math.Log(Math.Max(s2, 1e-15)) + 2 * k;
        double bic = n * Math.Log(Math.Max(s2, 1e-15)) + k * Math.Log(n);

        return new EvolutionFit(name, formula, r2, rmse, aic, bic, new[] { a });
    }

    private static EvolutionFit FitPowerLogistic(string name, string formula,
        double[] R, double[] dR)
    {
        // dR/dt = a·Rⁿ·(1-R). Fit via log-log: log(dR/(1-R)) = log(a) + n·log(R).
        var valid = Enumerable.Range(0, R.Length)
            .Where(i => R[i] > 0.02 && R[i] < 0.98 && dR[i] > 1e-12)
            .Select(i => (R[i], dR[i]))
            .ToList();
        if (valid.Count < 5)
            return new EvolutionFit(name, formula, 0, 1e6, 0, 0, new[] { 0.0, 1.0 });

        double sumLr = 0, sumLy = 0, sumLr2 = 0, sumLrLy = 0;
        int vn = valid.Count;
        for (int i = 0; i < vn; i++)
        {
            double lr = Math.Log(valid[i].Item1);
            double ly = Math.Log(valid[i].Item2 / (1 - valid[i].Item1));
            sumLr += lr; sumLy += ly; sumLr2 += lr * lr; sumLrLy += lr * ly;
        }
        double nExp = (vn * sumLrLy - sumLr * sumLy) /
                      Math.Max(vn * sumLr2 - sumLr * sumLr, 1e-15);
        double logA = (sumLy - nExp * sumLr) / vn;
        double aVal = Math.Exp(logA);

        double ssRes = 0, ssTot = 0, mean = dR.Average();
        for (int i = 0; i < dR.Length; i++)
        {
            double pred = aVal * Math.Pow(R[i], nExp) * (1 - R[i]);
            ssRes += (dR[i] - pred) * (dR[i] - pred);
            ssTot += (dR[i] - mean) * (dR[i] - mean);
        }
        double r2 = ssTot > 1e-15 ? 1 - ssRes / ssTot : 0;
        double rmse = Math.Sqrt(ssRes / dR.Length);
        int k = 2, nn = dR.Length;
        double s2 = rmse * rmse;
        double aic = nn * Math.Log(Math.Max(s2, 1e-15)) + 2 * k;
        double bic = nn * Math.Log(Math.Max(s2, 1e-15)) + k * Math.Log(nn);

        return new EvolutionFit(name, formula, r2, rmse, aic, bic, new[] { aVal, nExp });
    }

    private static EvolutionFit FitKPowerLogistic(string name, string formula,
        double[] R, double[] dR, double kVal)
    {
        // dR/dt = a·K·Rⁿ·(1-R). Fix K, fit a and n.
        // Equivalent to: dR/dt = (a·K)·Rⁿ·(1-R). Let A = a·K.
        var fit = FitPowerLogistic(name, formula, R, dR);
        if (fit.Parameters.Length >= 2)
        {
            double A = fit.Parameters[0], nVal = fit.Parameters[1];
            double aVal = kVal > 1e-10 ? A / kVal : A;
            return new EvolutionFit(name, formula, fit.R2, fit.RMSE,
                fit.AIC, fit.BIC, new[] { aVal, nVal, kVal });
        }
        return fit;
    }

    // ══════════════════════════════════════════════════════════════════
    // K-sweep analysis
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<double, EvolutionLawReport> AnalyzeKSweep(
        List<CoherenceGrowthProfile> data, double[] kValues)
    {
        var results = new Dictionary<double, EvolutionLawReport>();
        foreach (double k in kValues)
        {
            var sub = data.Where(d => Math.Abs(d.K - k) < 0.001).ToList();
            if (sub.Count > 10)
                results[k] = FitEvolutionLaws(sub, k);
        }
        return results;
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
}
