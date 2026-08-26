using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Fits analytic force-emergence laws to F_net = f(R) data
/// to determine whether net attraction follows a simple
/// functional form of coherence.
///
/// AT-073: Analytic Force Emergence Law
/// </summary>
public static class ForceEmergenceAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// A candidate analytic law for F_net = f(R).
    /// </summary>
    public sealed record ForceLawCandidate(
        string Name,
        string Formula,
        int ParameterCount,
        Func<double, double[], double> Evaluate,  // F(R, params)
        Func<double[], double[], (double[] p, double r2)> Fit);  // (R, F) → (params, R²)

    /// <summary>
    /// Fit result for a single candidate law.
    /// </summary>
    public sealed record ForceLawFit(
        string LawName,
        string Formula,
        double[] Parameters,
        double R2,
        double RMSE,
        double AIC,
        double BIC,
        int DataPoints);

    /// <summary>
    /// Aggregate report comparing all candidate laws.
    /// </summary>
    public sealed record ForceLawReport(
        List<ForceLawFit> Fits,
        string BestLaw,
        string BestFormula,
        double BestR2,
        double BestRMSE,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Data generation (reuses ForceSummationAnalyzer)
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Generates (R, F_net) data points at high resolution.
    /// </summary>
    public static List<(double R, double FNet, double Alignment, double Cancellation)>
    GenerateForceData(
        double rMin, double rMax, double rStep, string lawName,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        var data = new List<(double, double, double, double)>();
        int seedIdx = 0;

        // Get the force law function.
        var forceLaws = new Dictionary<string, Func<double, double>>
        {
            ["cos"] = d => Math.Cos(d),
            ["cos²"] = d => Math.Cos(d) * Math.Cos(d),
            ["exp(-|x|)"] = d => Math.Exp(-Math.Abs(d)),
            ["1/(1+|x|)"] = d => 1.0 / (1.0 + Math.Abs(d)),
        };
        var forceFn = forceLaws[lawName];

        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (double rT in rTargets)
        {
            for (int s = 0; s < seedsPerPoint; s++)
            {
                var profile = ForceSummationAnalyzer.ComputeForces(
                    rT, lawName, forceFn, k, lambda, nPerGroup,
                    baseSeed + seedIdx++ * 7919);
                data.Add((profile.ActualR, profile.NetForceMagnitude,
                    profile.AlignmentScore, profile.CancellationRatio));
            }
        }

        return data;
    }

    // ══════════════════════════════════════════════════════════════════
    // Candidate laws
    // ══════════════════════════════════════════════════════════════════

    private static List<ForceLawCandidate> BuildCandidates()
    {
        var candidates = new List<ForceLawCandidate>();

        // Linear: F = a·R
        candidates.Add(new ForceLawCandidate("Linear", "F = a·R", 1,
            (r, p) => p[0] * r,
            (R, F) => FitLinearOneParam(R, F)));

        // Quadratic: F = a·R²
        candidates.Add(new ForceLawCandidate("Quadratic", "F = a·R²", 1,
            (r, p) => p[0] * r * r,
            (R, F) => { var R2 = R.Select(r => r * r).ToArray(); return FitLinearOneParam(R2, F); }));

        // Cubic: F = a·R³
        candidates.Add(new ForceLawCandidate("Cubic", "F = a·R³", 1,
            (r, p) => p[0] * r * r * r,
            (R, F) => { var R3 = R.Select(r => r * r * r).ToArray(); return FitLinearOneParam(R3, F); }));

        // Power: F = a·Rⁿ
        candidates.Add(new ForceLawCandidate("Power", "F = a·Rⁿ", 2,
            (r, p) => p[0] * Math.Pow(Math.Max(r, 1e-10), p[1]),
            (R, F) => FitPowerLaw(R, F)));

        // Exponential: F = a·(1 - exp(-b·R))
        candidates.Add(new ForceLawCandidate("Exponential", "F = a(1-e^{-bR})", 2,
            (r, p) => p[0] * (1.0 - Math.Exp(-p[1] * r)),
            (R, F) => FitNonlinear(R, F, (r, p) => p[0] * (1.0 - Math.Exp(-p[1] * r)), 2,
                new[] { (0.0, 10.0), (0.1, 20.0) })));

        // Hyperbolic tangent: F = a·tanh(b·R)
        candidates.Add(new ForceLawCandidate("Tanh", "F = a·tanh(bR)", 2,
            (r, p) => p[0] * Math.Tanh(p[1] * r),
            (R, F) => FitNonlinear(R, F, (r, p) => p[0] * Math.Tanh(p[1] * r), 2,
                new[] { (0.0, 10.0), (0.1, 20.0) })));

        // Logistic/sigmoid: F = a/(1+exp(-b(R-c)))
        candidates.Add(new ForceLawCandidate("Logistic", "F = a/(1+e^{-b(R-c)})", 3,
            (r, p) => p[0] / (1.0 + Math.Exp(-p[1] * (r - p[2]))),
            (R, F) => FitNonlinear(R, F, (r, p) => p[0] / (1.0 + Math.Exp(-p[1] * (r - p[2]))), 3,
                new[] { (0.0, 10.0), (0.1, 50.0), (-0.5, 0.5) })));

        // Polynomial degree 3: F = a₀ + a₁R + a₂R² + a₃R³
        candidates.Add(new ForceLawCandidate("Poly3", "F = a₀+a₁R+a₂R²+a₃R³", 4,
            (r, p) => p[0] + p[1] * r + p[2] * r * r + p[3] * r * r * r,
            (R, F) => FitPolynomial(R, F, 3)));

        // Polynomial degree 4
        candidates.Add(new ForceLawCandidate("Poly4", "F = ΣaᵢRⁱ (deg 4)", 5,
            (r, p) => p[0] + p[1] * r + p[2] * r * r + p[3] * r * r * r + p[4] * r * r * r * r,
            (R, F) => FitPolynomial(R, F, 4)));

        // Polynomial degree 5
        candidates.Add(new ForceLawCandidate("Poly5", "F = ΣaᵢRⁱ (deg 5)", 6,
            (r, p) => p[0] + p[1] * r + p[2] * r * r + p[3] * r * r * r +
                     p[4] * r * r * r * r + p[5] * r * r * r * r * r,
            (R, F) => FitPolynomial(R, F, 5)));

        return candidates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Fitting routines
    // ══════════════════════════════════════════════════════════════════

    private static (double[] p, double r2) FitLinearOneParam(double[] X, double[] Y)
    {
        // F = a·X, forced through origin.
        double sumXY = 0, sumX2 = 0;
        for (int i = 0; i < X.Length; i++)
        { sumXY += X[i] * Y[i]; sumX2 += X[i] * X[i]; }
        double a = sumX2 > 1e-15 ? sumXY / sumX2 : 0;
        double ssRes = 0, ssTot = 0;
        double meanY = Y.Average();
        for (int i = 0; i < Y.Length; i++)
        { double pred = a * X[i]; ssRes += (Y[i] - pred) * (Y[i] - pred); ssTot += (Y[i] - meanY) * (Y[i] - meanY); }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (new[] { a }, r2);
    }

    private static (double[] p, double r2) FitPowerLaw(double[] R, double[] F)
    {
        // log(F) = log(a) + n·log(R)  for F > 0.
        var valid = R.Zip(F, (r, f) => (r, f)).Where(x => x.r > 1e-10 && x.f > 1e-10).ToList();
        if (valid.Count < 4) return (new[] { 1.0, 1.0 }, 0);

        int n = valid.Count;
        double sumLr = 0, sumLf = 0, sumLr2 = 0, sumLrLf = 0;
        for (int i = 0; i < n; i++)
        {
            double lr = Math.Log(valid[i].r), lf = Math.Log(valid[i].f);
            sumLr += lr; sumLf += lf; sumLr2 += lr * lr; sumLrLf += lr * lf;
        }
        double slope = (n * sumLrLf - sumLr * sumLf) / Math.Max(n * sumLr2 - sumLr * sumLr, 1e-15);
        double logA = (sumLf - slope * sumLr) / n;
        double a = Math.Exp(logA);

        // Compute R² on original scale.
        double ssRes = 0, ssTot = 0, meanF = F.Average();
        for (int i = 0; i < F.Length; i++)
        {
            double pred = a * Math.Pow(Math.Max(R[i], 1e-10), slope);
            ssRes += (F[i] - pred) * (F[i] - pred);
            ssTot += (F[i] - meanF) * (F[i] - meanF);
        }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (new[] { a, slope }, r2);
    }

    private static (double[] p, double r2) FitNonlinear(
        double[] R, double[] F, Func<double, double[], double> model,
        int nParams, (double min, double max)[] ranges)
    {
        int n = R.Length;
        double[] bestP = new double[nParams];
        double bestR2 = double.MinValue;

        // Grid search over nonlinear parameters, least squares for linear ones.
        int gridSize = 15;
        var grids = ranges.Select(r => Enumerable.Range(0, gridSize)
            .Select(i => r.min + (r.max - r.min) * i / (gridSize - 1)).ToArray()).ToArray();

        void Search(int dim, double[] current)
        {
            if (dim == nParams)
            {
                double ssRes = 0;
                for (int i = 0; i < n; i++)
                {
                    double pred = model(R[i], current);
                    ssRes += (F[i] - pred) * (F[i] - pred);
                }
                double meanF = F.Average();
                double ssTot = 0;
                for (int i = 0; i < n; i++)
                    ssTot += (F[i] - meanF) * (F[i] - meanF);
                double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
                if (r2 > bestR2)
                {
                    bestR2 = r2;
                    Array.Copy(current, bestP, nParams);
                }
                return;
            }

            foreach (double val in grids[dim])
            {
                current[dim] = val;
                Search(dim + 1, current);
            }
        }

        Search(0, new double[nParams]);
        return (bestP, bestR2);
    }

    private static (double[] p, double r2) FitPolynomial(double[] R, double[] F, int degree)
    {
        // Least squares: solve (X^T X) β = X^T Y.
        int n = R.Length;
        int k = degree + 1;
        double[,] XTX = new double[k, k];
        double[] XTY = new double[k];

        for (int i = 0; i < n; i++)
        {
            double[] powers = new double[k];
            powers[0] = 1.0;
            for (int d = 1; d < k; d++)
                powers[d] = powers[d - 1] * R[i];

            for (int a = 0; a < k; a++)
            {
                XTY[a] += powers[a] * F[i];
                for (int b = 0; b < k; b++)
                    XTX[a, b] += powers[a] * powers[b];
            }
        }

        // Solve via Gaussian elimination with partial pivoting.
        double[] beta = SolveLinearSystem(XTX, XTY, k);

        // R².
        double ssRes = 0, ssTot = 0, meanF = F.Average();
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0];
            double rp = 1.0;
            for (int d = 1; d < k; d++)
            { rp *= R[i]; pred += beta[d] * rp; }
            ssRes += (F[i] - pred) * (F[i] - pred);
            ssTot += (F[i] - meanF) * (F[i] - meanF);
        }
        double r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
        return (beta, r2);
    }

    // ══════════════════════════════════════════════════════════════════
    // Numerical helpers
    // ══════════════════════════════════════════════════════════════════

    private static double[] SolveLinearSystem(double[,] A, double[] b, int n)
    {
        // Gaussian elimination with partial pivoting.
        double[,] M = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) M[i, j] = A[i, j];
            M[i, n] = b[i];
        }

        for (int col = 0; col < n; col++)
        {
            // Find pivot.
            int maxRow = col;
            double maxVal = Math.Abs(M[col, col]);
            for (int row = col + 1; row < n; row++)
            {
                if (Math.Abs(M[row, col]) > maxVal)
                { maxVal = Math.Abs(M[row, col]); maxRow = row; }
            }

            // Swap rows.
            for (int j = col; j <= n; j++)
                (M[col, j], M[maxRow, j]) = (M[maxRow, j], M[col, j]);

            if (Math.Abs(M[col, col]) < 1e-15) continue;

            // Eliminate below.
            for (int row = col + 1; row < n; row++)
            {
                double factor = M[row, col] / M[col, col];
                for (int j = col; j <= n; j++)
                    M[row, j] -= factor * M[col, j];
            }
        }

        // Back substitution.
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = M[i, n];
            for (int j = i + 1; j < n; j++)
                sum -= M[i, j] * x[j];
            x[i] = Math.Abs(M[i, i]) > 1e-15 ? sum / M[i, i] : 0;
        }
        return x;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model comparison
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Fits all candidate laws to force data and ranks them.
    /// </summary>
    public static ForceLawReport FitAllLaws(
        List<(double R, double FNet, double Alignment, double Cancellation)> data)
    {
        var R = data.Select(d => d.R).ToArray();
        var F = data.Select(d => d.FNet).ToArray();
        int n = data.Count;
        var candidates = BuildCandidates();
        var fits = new List<ForceLawFit>();

        foreach (var cand in candidates)
        {
            var (p, r2) = cand.Fit(R, F);

            // Compute predictions and RMSE.
            double rss = 0;
            for (int i = 0; i < n; i++)
            {
                double pred = cand.Evaluate(R[i], p);
                rss += (F[i] - pred) * (F[i] - pred);
            }
            double rmse = Math.Sqrt(rss / n);

            // AIC and BIC.
            int k = p.Length;
            double sigma2 = rss / Math.Max(n, 1);
            double aic = n * Math.Log(Math.Max(sigma2, 1e-15)) + 2 * k;
            double bic = n * Math.Log(Math.Max(sigma2, 1e-15)) + k * Math.Log(n);

            fits.Add(new ForceLawFit(cand.Name, cand.Formula, p,
                r2, rmse, aic, bic, n));
        }

        // Rank by R² descending.
        fits = fits.OrderByDescending(f => f.R2).ToList();
        var best = fits[0];

        string classification = best.R2 > 0.95 ? "D: Universal Force Emergence Equation" :
                                best.R2 > 0.85 ? "C: Strong Analytic Law" :
                                best.R2 > 0.70 ? "B: Approximate Empirical Law" :
                                "A: No Simple Law";

        string interpretation = classification switch
        {
            "D: Universal Force Emergence Equation" =>
                $"The {best.LawName} law ({best.Formula}) fits with R²={best.R2:F4}. " +
                "Net attraction follows a precise analytic function of coherence. " +
                "This is a fundamental force-emergence equation.",
            "C: Strong Analytic Law" =>
                $"The {best.LawName} law ({best.Formula}) provides a strong fit " +
                $"(R²={best.R2:F4}). A simple analytic function captures the " +
                "force-coherence relationship with high accuracy.",
            "B: Approximate Empirical Law" =>
                $"The {best.LawName} law ({best.Formula}) approximates the data " +
                $"(R²={best.R2:F4}). A simple law exists but has limited precision.",
            _ => "No simple analytic law captures the force-coherence relationship. " +
                 "The dynamics may require more complex description."
        };

        return new ForceLawReport(fits, best.LawName, best.Formula,
            best.R2, best.RMSE, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Full sweep across multiple laws
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Runs full force-law fitting across all coupling laws.
    /// </summary>
    public static (Dictionary<string, ForceLawReport> Reports,
                  ForceLawReport CombinedReport,
                  string TrueClassification)
    RunFullEmergenceAnalysis(
        double rMin, double rMax, double rStep,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        string[] laws = { "cos", "cos²", "exp(-|x|)", "1/(1+|x|)" };
        var reports = new Dictionary<string, ForceLawReport>();
        var allData = new List<(double, double, double, double)>();

        foreach (string law in laws)
        {
            var data = GenerateForceData(rMin, rMax, rStep, law,
                k, lambda, nPerGroup, seedsPerPoint, baseSeed + laws.ToList().IndexOf(law) * 100000);
            allData.AddRange(data);
            reports[law] = FitAllLaws(data);
        }

        var combined = FitAllLaws(allData);

        // True classification: based on per-law fits, not combined.
        // A universal law exists if a SINGLE analytic form fits all
        // laws well (min per-law R² > threshold).
        double minR2 = reports.Values.Min(r => r.Fits[0].R2);
        string trueClass = minR2 > 0.95 ? "D: Universal Force Emergence Equation" :
                           minR2 > 0.85 ? "C: Strong Analytic Law" :
                           minR2 > 0.70 ? "B: Approximate Empirical Law" :
                           "A: No Simple Universal Law";

        return (reports, combined, trueClass);
    }
}
