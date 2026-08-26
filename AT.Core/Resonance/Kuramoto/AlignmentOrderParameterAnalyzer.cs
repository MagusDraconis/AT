using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether force alignment (not coherence R) is the
/// true order parameter for macroscopic attraction.
///
/// AT-074: Alignment Order Parameter
/// </summary>
public static class AlignmentOrderParameterAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record AlignmentProfile(
        double R,
        double Alignment,
        double MeanLocalForce,
        double NetForce,
        double Cancellation,
        double PositiveArea,
        string LawName,
        int Seed);

    public sealed record ModelComparison(
        string ModelName,
        string Formula,
        double R2,
        double RMSE,
        double AIC,
        double BIC,
        double[] Coefficients,
        int ParamCount);

    public sealed record AlignmentOrderReport(
        List<AlignmentProfile> Profiles,
        List<ModelComparison> Comparisons,
        string BestModel,
        double BestR2,
        double AlignmentVsR_R2,     // R² when using Alignment vs when using R
        double Improvement,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Data generation
    // ══════════════════════════════════════════════════════════════════

    public static List<AlignmentProfile> GenerateProfiles(
        double rMin, double rMax, double rStep,
        string[] lawNames, double k, double lambda,
        int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        var forceLaws = new Dictionary<string, Func<double, double>>
        {
            ["cos"] = d => Math.Cos(d),
            ["cos²"] = d => Math.Cos(d) * Math.Cos(d),
            ["exp(-|x|)"] = d => Math.Exp(-Math.Abs(d)),
            ["1/(1+|x|)"] = d => 1.0 / (1.0 + Math.Abs(d)),
        };

        var profiles = new List<AlignmentProfile>();
        int seedIdx = 0;

        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (string law in lawNames)
        {
            var forceFn = forceLaws[law];
            // Compute PositiveArea for this law.
            double posArea = ComputePositiveArea(forceFn);

            foreach (double rT in rTargets)
            {
                for (int s = 0; s < seedsPerPoint; s++)
                {
                    var fp = ForceSummationAnalyzer.ComputeForces(
                        rT, law, forceFn, k, lambda, nPerGroup,
                        baseSeed + seedIdx++ * 7919);

                    profiles.Add(new AlignmentProfile(
                        fp.ActualR, fp.AlignmentScore,
                        fp.MeanPairMagnitude, fp.NetForceMagnitude,
                        fp.CancellationRatio, posArea, law, baseSeed + s));
                }
            }
        }

        return profiles;
    }

    private static double ComputePositiveArea(Func<double, double> fn, int samples = 500)
    {
        double dx = 2 * Math.PI / samples;
        double area = 0;
        for (int i = 0; i < samples; i++)
        {
            double x = -Math.PI + i * dx;
            double v = fn(x);
            if (v > 0) area += v;
        }
        return area * dx;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model fitting & comparison
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Compares coherence-based vs alignment-based models.
    /// </summary>
    public static AlignmentOrderReport Analyze(
        List<AlignmentProfile> profiles)
    {
        int n = profiles.Count;
        double[] F = profiles.Select(p => p.NetForce).ToArray();
        double[] R = profiles.Select(p => p.R).ToArray();
        double[] A = profiles.Select(p => p.Alignment).ToArray();
        double[] M = profiles.Select(p => p.MeanLocalForce).ToArray();
        double[] P = profiles.Select(p => p.PositiveArea).ToArray();

        var comparisons = new List<ModelComparison>();

        // ── Model A: F = f(R) — coherence-based (Poly5) ──────────────
        {
            int deg = 5;
            var (beta, r2) = FitPolynomial(R, F, deg);
            double rmse = RMSE(R, F, (r, p) => PolyEval(r, p, deg), beta, n);
            double aic = AIC(n, rmse, beta.Length);
            double bic = BIC(n, rmse, beta.Length);
            comparisons.Add(new ModelComparison("Coherence Poly5",
                "F = ΣaᵢRⁱ (deg 5)", r2, rmse, aic, bic, beta, beta.Length));
        }

        // ── Model B: F = a · Alignment ──────────────────────────────
        {
            var beta = FitLinearThroughOrigin(A, F);
            double r2 = R2Linear(A, F, beta[0]);
            double rmse = RMSE(A, F, (a, p) => p[0] * a, beta, n);
            double aic = AIC(n, rmse, 1);
            double bic = BIC(n, rmse, 1);
            comparisons.Add(new ModelComparison("Alignment Direct",
                "F = a · A", r2, rmse, aic, bic, beta, 1));
        }

        // ── Model C: F = a · Alignment × MeanLocalForce ─────────────
        {
            double[] AM = A.Zip(M, (a, m) => a * m).ToArray();
            var beta = FitLinearThroughOrigin(AM, F);
            double r2 = R2Linear(AM, F, beta[0]);
            double rmse = RMSE(AM, F, (am, p) => p[0] * am, beta, n);
            double aic = AIC(n, rmse, 1);
            double bic = BIC(n, rmse, 1);
            comparisons.Add(new ModelComparison("Align × MeanForce",
                "F = a · A · ⟨f⟩", r2, rmse, aic, bic, beta, 1));
        }

        // ── Model D: F = a · Alignment × PositiveArea ───────────────
        {
            double[] AP = A.Zip(P, (a, pv) => a * pv).ToArray();
            var beta = FitLinearThroughOrigin(AP, F);
            double r2 = R2Linear(AP, F, beta[0]);
            double rmse = RMSE(AP, F, (ap, p) => p[0] * ap, beta, n);
            double aic = AIC(n, rmse, 1);
            double bic = BIC(n, rmse, 1);
            comparisons.Add(new ModelComparison("Align × PosArea",
                "F = a · A · Area⁺", r2, rmse, aic, bic, beta, 1));
        }

        // ── Model E: F = a · Alignment + b (offset) ─────────────────
        {
            var beta = FitPolynomial(A, F, 1);
            double r2 = beta.Item2;
            double rmse = RMSE(A, F, (a, p) => p[0] + p[1] * a, beta.Item1, n);
            int k = 2;
            double aic = AIC(n, rmse, k);
            double bic = BIC(n, rmse, k);
            comparisons.Add(new ModelComparison("Alignment + offset",
                "F = a₀ + a₁·A", r2, rmse, aic, bic, beta.Item1, k));
        }

        // Sort by R².
        comparisons = comparisons.OrderByDescending(c => c.R2).ToList();
        var best = comparisons[0];
        var coherenceModel = comparisons.First(c => c.ModelName.Contains("Coherence"));
        double improvement = best.R2 - coherenceModel.R2;

        string classification = improvement > 0.15 ? "D: Universal Force Generator" :
                                improvement > 0.05 ? "C: Primary Order Parameter" :
                                improvement > 0.01 ? "B: Strong Predictor" :
                                "A: Secondary Variable";

        string interpretation = classification switch
        {
            "D: Universal Force Generator" =>
                $"Alignment is the true order parameter for attraction. " +
                $"Model '{best.ModelName}' achieves R²={best.R2:F4}, far exceeding " +
                $"coherence-based models (R²={coherenceModel.R2:F4}). Coherence is merely " +
                "a precursor that enables alignment.",
            "C: Primary Order Parameter" =>
                $"Alignment significantly outperforms coherence as a predictor " +
                $"(ΔR²=+{improvement:F4}). The causal chain R → Alignment → F_net " +
                "is confirmed: alignment is the direct force-generating variable.",
            "B: Strong Predictor" =>
                $"Alignment is a strong predictor of net force (R²={best.R2:F4}) " +
                "but coherence-based models are comparable. Both variables carry " +
                "significant predictive power.",
            _ => "Alignment and coherence are similarly predictive. Neither is " +
                 "clearly superior as an order parameter."
        };

        return new AlignmentOrderReport(profiles, comparisons,
            best.ModelName, best.R2, comparisons.First(c => c.ModelName == "Alignment Direct").R2,
            improvement, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Per-law analysis
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, AlignmentOrderReport> AnalyzePerLaw(
        List<AlignmentProfile> profiles)
    {
        return profiles.GroupBy(p => p.LawName)
            .ToDictionary(g => g.Key, g => Analyze(g.ToList()));
    }

    // ══════════════════════════════════════════════════════════════════
    // Numerical helpers
    // ══════════════════════════════════════════════════════════════════

    private static double[] FitLinearThroughOrigin(double[] X, double[] Y)
    {
        double sumXY = 0, sumX2 = 0;
        for (int i = 0; i < X.Length; i++)
        { sumXY += X[i] * Y[i]; sumX2 += X[i] * X[i]; }
        double a = sumX2 > 1e-15 ? sumXY / sumX2 : 0;
        return new[] { a };
    }

    private static double R2Linear(double[] X, double[] Y, double a)
    {
        double ssRes = 0, ssTot = 0, meanY = Y.Average();
        for (int i = 0; i < Y.Length; i++)
        { double pred = a * X[i]; ssRes += (Y[i] - pred) * (Y[i] - pred); ssTot += (Y[i] - meanY) * (Y[i] - meanY); }
        return ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
    }

    private static (double[], double) FitPolynomial(double[] X, double[] Y, int degree)
    {
        int n = X.Length, k = degree + 1;
        double[,] XTX = new double[k, k];
        double[] XTY = new double[k];
        for (int i = 0; i < n; i++)
        {
            double[] pw = new double[k]; pw[0] = 1.0;
            for (int d = 1; d < k; d++) pw[d] = pw[d - 1] * X[i];
            for (int a = 0; a < k; a++)
            {
                XTY[a] += pw[a] * Y[i];
                for (int b = 0; b < k; b++) XTX[a, b] += pw[a] * pw[b];
            }
        }
        double[] beta = SolveGauss(XTX, XTY, k);
        double ssRes = 0, ssTot = 0, meanY = Y.Average();
        for (int i = 0; i < n; i++)
        { double pred = PolyEval(X[i], beta, degree); ssRes += (Y[i] - pred) * (Y[i] - pred); ssTot += (Y[i] - meanY) * (Y[i] - meanY); }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (beta, r2);
    }

    private static double PolyEval(double x, double[] coeffs, int degree)
    {
        double result = coeffs[0], xp = 1.0;
        for (int d = 1; d <= degree; d++)
        { xp *= x; result += coeffs[d] * xp; }
        return result;
    }

    private static double RMSE(double[] X, double[] Y,
        Func<double, double[], double> model, double[] p, int n)
    {
        double rss = 0;
        for (int i = 0; i < n; i++)
        { double diff = Y[i] - model(X[i], p); rss += diff * diff; }
        return Math.Sqrt(rss / n);
    }

    private static double AIC(int n, double rmse, int k)
    {
        double sigma2 = rmse * rmse;
        return n * Math.Log(Math.Max(sigma2, 1e-15)) + 2 * k;
    }

    private static double BIC(int n, double rmse, int k)
    {
        double sigma2 = rmse * rmse;
        return n * Math.Log(Math.Max(sigma2, 1e-15)) + k * Math.Log(n);
    }

    private static double[] SolveGauss(double[,] A, double[] b, int n)
    {
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        { for (int j = 0; j < n; j++) M[i, j] = A[i, j]; M[i, n] = b[i]; }
        for (int col = 0; col < n; col++)
        {
            int maxRow = col;
            for (int row = col + 1; row < n; row++)
                if (Math.Abs(M[row, col]) > Math.Abs(M[maxRow, col])) maxRow = row;
            for (int j = col; j <= n; j++)
                (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);
            if (Math.Abs(M[col, col]) < 1e-15) continue;
            for (int row = col + 1; row < n; row++)
            {
                double factor = M[row, col] / M[col, col];
                for (int j = col; j <= n; j++) M[row, j] -= factor * M[col, j];
            }
        }
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = M[i, n];
            for (int j = i + 1; j < n; j++) sum -= M[i, j] * x[j];
            x[i] = Math.Abs(M[i, i]) > 1e-15 ? sum / M[i, i] : 0;
        }
        return x;
    }
}
