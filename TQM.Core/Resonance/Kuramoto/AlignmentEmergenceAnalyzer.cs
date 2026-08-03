using TQM.Core.Temporal;

namespace TQM.Core.Resonance.Kuramoto;

/// <summary>
/// Determines whether force alignment can be derived analytically
/// from the statistical phase distribution of oscillators.
///
/// TQM-075: Analytic Alignment Emergence Law
/// </summary>
public static class AlignmentEmergenceAnalyzer
{
    // ══════════════════════════════════════════════════════════════════
    // Types
    // ══════════════════════════════════════════════════════════════════

    public sealed record AlignmentCandidate(
        string Name,
        string Formula,
        int ParamCount,
        Func<double, double[], double> Predict); // A(R, params)

    public sealed record AlignmentFit(
        string Name,
        string Formula,
        double[] Parameters,
        double R2,
        double RMSE,
        double AIC,
        double BIC,
        int DataPoints);

    public sealed record AlignmentLawReport(
        List<AlignmentFit> Fits,
        string BestModel,
        double BestR2,
        bool VonMisesWins,
        double AnalyticImprovement,
        string Classification,
        string Interpretation);

    // ══════════════════════════════════════════════════════════════════
    // Von Mises utilities
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// von Mises PDF: exp(κ cos θ) / (2π I₀(κ)).
    /// Unnormalized: exp(κ cos θ). Normalized later.
    /// </summary>
    private static double VonMisesPdf(double theta, double kappa)
        => Math.Exp(kappa * Math.Cos(theta));

    /// <summary>
    /// Numerically integrates ∫ f(θ) · vonMises(θ|κ) dθ over [-π, π].
    /// </summary>
    private static double VonMisesExpectation(
        double kappa, Func<double, double> f, int samples = 1000)
    {
        double dx = 2 * Math.PI / samples;
        double sum = 0, norm = 0;
        for (int i = 0; i < samples; i++)
        {
            double theta = -Math.PI + (i + 0.5) * dx;
            double w = VonMisesPdf(theta, kappa);
            sum += f(theta) * w;
            norm += w;
        }
        return norm > 1e-15 ? sum / norm : 0;
    }

    /// <summary>
    /// Computes alignment analytically from von Mises distribution:
    /// A(κ) = E[sign(F(θ)) | κ] for a coupling function F.
    /// </summary>
    public static double AnalyticAlignment(double kappa, Func<double, double> couplingFn,
        int samples = 1000)
    {
        return VonMisesExpectation(kappa, theta => Math.Sign(couplingFn(theta)), samples);
    }

    /// <summary>
    /// Computes mean local force from von Mises:
    /// ⟨f⟩(κ) = E[|F(θ)| | κ].
    /// </summary>
    public static double AnalyticMeanForce(double kappa, Func<double, double> couplingFn,
        int samples = 1000)
    {
        return VonMisesExpectation(kappa, theta => Math.Abs(couplingFn(theta)), samples);
    }

    /// <summary>
    /// Computes cancellation ratio from von Mises:
    /// C(κ) = |E[F(θ)]| / E[|F(θ)|].
    /// </summary>
    public static double AnalyticCancellation(double kappa, Func<double, double> couplingFn,
        int samples = 1000)
    {
        double meanF = VonMisesExpectation(kappa, couplingFn, samples);
        double meanAbs = VonMisesExpectation(kappa, theta => Math.Abs(couplingFn(theta)), samples);
        return meanAbs > 1e-15 ? Math.Abs(meanF) / meanAbs : 0;
    }

    // ══════════════════════════════════════════════════════════════════
    // Data generation: measure alignment at controlled R
    // ══════════════════════════════════════════════════════════════════

    public static List<(double R, double Alignment, double Cancellation, double MeanForce, double NetForce)>
    GenerateAlignmentData(
        double rMin, double rMax, double rStep,
        string lawName, double k, double lambda, int nPerGroup,
        int seedsPerPoint, int baseSeed)
    {
        var forceLaws = new Dictionary<string, Func<double, double>>
        {
            ["cos"] = d => Math.Cos(d),
            ["cos²"] = d => Math.Cos(d) * Math.Cos(d),
            ["exp(-|x|)"] = d => Math.Exp(-Math.Abs(d)),
            ["1/(1+|x|)"] = d => 1.0 / (1.0 + Math.Abs(d)),
        };
        var fn = forceLaws[lawName];

        var data = new List<(double, double, double, double, double)>();
        int seedIdx = 0;

        var rTargets = new List<double>();
        for (double r = rMin; r <= rMax + 1e-10; r += rStep)
            rTargets.Add(Math.Round(r, 4));

        foreach (double rT in rTargets)
        {
            for (int s = 0; s < seedsPerPoint; s++)
            {
                var fp = ForceSummationAnalyzer.ComputeForces(
                    rT, lawName, fn, k, lambda, nPerGroup,
                    baseSeed + seedIdx++ * 7919);
                data.Add((fp.ActualR, fp.AlignmentScore,
                    fp.CancellationRatio, fp.MeanPairMagnitude,
                    fp.NetForceMagnitude));
            }
        }

        return data;
    }

    // ══════════════════════════════════════════════════════════════════
    // Analytic candidate models
    // ══════════════════════════════════════════════════════════════════

    private static List<AlignmentCandidate> BuildCandidates(
        Func<double, double> couplingFn)
    {
        var candidates = new List<AlignmentCandidate>();

        // 1. A = R (alignment equals coherence)
        candidates.Add(new AlignmentCandidate("A = R", "A(R) = R", 0,
            (r, _) => r));

        // 2. A = R²
        candidates.Add(new AlignmentCandidate("A = R²", "A(R) = R²", 0,
            (r, _) => r * r));

        // 3. A = R³
        candidates.Add(new AlignmentCandidate("A = R³", "A(R) = R³", 0,
            (r, _) => r * r * r));

        // 4. A = a·Rⁿ (power law fitted)
        candidates.Add(new AlignmentCandidate("Power Law", "A = a·Rⁿ", 2,
            null!)); // fitted separately

        // 5. A = R^5 (from TQM-073 cos power law)
        candidates.Add(new AlignmentCandidate("A = R⁵", "A(R) = R⁵", 0,
            (r, _) => Math.Pow(r, 5)));

        // 6. Von Mises analytic: A(κ) with κ = κ(R)
        candidates.Add(new AlignmentCandidate("von Mises", "A = E[sign(F(θ))|κ(R)]", 0,
            (r, p) =>
            {
                double kappa = CriticalCoherenceAnalyzer.KappaFromR(r);
                return AnalyticAlignment(kappa, couplingFn);
            }));

        // 7. von Mises alignment squared (some theories predict A ~ R² from phase stats)
        candidates.Add(new AlignmentCandidate("von Mises²", "A = [E[sign(F)|κ]]²", 0,
            (r, p) =>
            {
                double kappa = CriticalCoherenceAnalyzer.KappaFromR(r);
                double a = AnalyticAlignment(kappa, couplingFn);
                return a * a;
            }));

        // 8. Linear in R with offset: a·R + b
        candidates.Add(new AlignmentCandidate("Linear+offset", "A = a·R + b", 2,
            null!)); // fitted

        return candidates;
    }

    // ══════════════════════════════════════════════════════════════════
    // Model fitting & comparison
    // ══════════════════════════════════════════════════════════════════

    /// <summary>
    /// Fits all candidate alignment models to measured data.
    /// </summary>
    public static AlignmentLawReport FitAlignmentLaws(
        List<(double R, double A, double C, double Mf, double Nf)> data,
        Func<double, double> couplingFn)
    {
        double[] R = data.Select(d => d.R).ToArray();
        double[] A = data.Select(d => d.A).ToArray();
        int n = data.Count;
        var candidates = BuildCandidates(couplingFn);
        var fits = new List<AlignmentFit>();

        foreach (var cand in candidates)
        {
            double[] @params;
            double r2;

            if (cand.ParamCount == 0)
            {
                // Zero-parameter model: compute R² directly.
                @params = Array.Empty<double>();
                double ssRes = 0, ssTot = 0, meanA = A.Average();
                for (int i = 0; i < n; i++)
                {
                    double pred = cand.Predict(R[i], @params);
                    ssRes += (A[i] - pred) * (A[i] - pred);
                    ssTot += (A[i] - meanA) * (A[i] - meanA);
                }
                r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
            }
            else if (cand.Name == "Power Law")
            {
                // Fit power law: log(A) = log(a) + n·log(R).
                var valid = R.Zip(A, (r, a) => (r, a))
                    .Where(x => x.r > 1e-10 && x.a > 1e-10).ToList();
                if (valid.Count < 4)
                {
                    @params = new[] { 1.0, 1.0 }; r2 = 0;
                }
                else
                {
                    double sumLr = 0, sumLa = 0, sumLr2 = 0, sumLrLa = 0;
                    int vn = valid.Count;
                    for (int i = 0; i < vn; i++)
                    {
                        double lr = Math.Log(valid[i].r), la = Math.Log(valid[i].a);
                        sumLr += lr; sumLa += la; sumLr2 += lr * lr; sumLrLa += lr * la;
                    }
                    double slope = (vn * sumLrLa - sumLr * sumLa) /
                                   Math.Max(vn * sumLr2 - sumLr * sumLr, 1e-15);
                    double logA0 = (sumLa - slope * sumLr) / vn;
                    double a0 = Math.Exp(logA0);
                    @params = new[] { a0, slope };

                    double ssRes = 0, ssTot = 0, mA = A.Average();
                    for (int i = 0; i < n; i++)
                    {
                        double pred = a0 * Math.Pow(Math.Max(R[i], 1e-10), slope);
                        ssRes += (A[i] - pred) * (A[i] - pred);
                        ssTot += (A[i] - mA) * (A[i] - mA);
                    }
                    r2 = ssTot > 1e-15 ? 1.0 - ssRes / ssTot : 0;
                }
            }
            else if (cand.Name == "Linear+offset")
            {
                // Fit A = a·R + b via linear least squares.
                var (beta, r2f) = FitPoly(R, A, 1);
                @params = beta; r2 = r2f;
            }
            else
            {
                @params = Array.Empty<double>(); r2 = 0;
            }

            double rmse = 0;
            for (int i = 0; i < n; i++)
            {
                double pred = cand.ParamCount > 0
                    ? (cand.Name == "Power Law"
                        ? @params[0] * Math.Pow(Math.Max(R[i], 1e-10), @params[1])
                        : @params[0] * R[i] + @params[1])
                    : cand.Predict(R[i], @params);
                double diff = A[i] - pred;
                if (double.IsNaN(diff)) diff = 0;
                rmse += diff * diff;
            }
            rmse = Math.Sqrt(rmse / n);
            if (double.IsNaN(rmse)) rmse = 1e6;
            if (double.IsNaN(r2)) r2 = -1; // NaN → treat as worst fit
            int k = @params.Length;
            double sigma2 = rmse * rmse;
            double aic = n * Math.Log(Math.Max(sigma2, 1e-15)) + 2 * k;
            double bic = n * Math.Log(Math.Max(sigma2, 1e-15)) + k * Math.Log(n);

            fits.Add(new AlignmentFit(cand.Name, cand.Formula,
                @params, r2, rmse, aic, bic, n));
        }

        fits = fits.OrderByDescending(f => f.R2).ToList();
        var best = fits[0];
        var vm = fits.First(f => f.Name == "von Mises");
        double vmImprove = vm.R2 - best.R2;

        bool vmWins = vm.R2 >= best.R2 - 0.01 || (double.IsNaN(vm.R2) && best.R2 < 0);

        double safeVmR2 = double.IsNaN(vm.R2) ? -1 : vm.R2;
        string classification = safeVmR2 > 0.90 ? "D: Universal Alignment Equation" :
                                safeVmR2 > 0.70 ? "C: Strong Analytic Law" :
                                safeVmR2 > 0.50 ? "B: Approximate Empirical Law" :
                                "A: No Analytic Law";

        string interpretation = safeVmR2 > 0.90 ? $"Alignment is analytically derivable from the von Mises " +
                $"phase distribution (R²={safeVmR2:F4}). A(R) = E[sign(F(θ))|κ(R)] " +
                "closes the force-emergence theory." :
                safeVmR2 > 0.70 ? $"Von Mises theory strongly predicts alignment (R²={safeVmR2:F4}). " +
                "The phase distribution → alignment → force chain is verified." :
                safeVmR2 > 0.50 ? $"Von Mises theory approximates alignment (R²={safeVmR2:F4}). " +
                "Additional corrections may be needed for full accuracy." :
                safeVmR2 < 0 ? "The von Mises analytic prediction fails for this law " +
                $"(alignment is near-constant, making R² ill-defined). " +
                "The power law A = a·Rⁿ fits best (R²={best.R2:F4})." :
                "Alignment cannot be derived from the phase distribution alone. " +
                "Finite-N effects or other factors may dominate.";

        return new AlignmentLawReport(fits, best.Name, best.R2,
            vmWins, vmImprove, classification, interpretation);
    }

    // ══════════════════════════════════════════════════════════════════
    // Run full analysis across all laws
    // ══════════════════════════════════════════════════════════════════

    public static Dictionary<string, AlignmentLawReport> RunFullAlignmentAnalysis(
        double rMin, double rMax, double rStep,
        double k, double lambda, int nPerGroup, int seedsPerPoint, int baseSeed)
    {
        var forceLaws = new Dictionary<string, Func<double, double>>
        {
            ["cos"] = d => Math.Cos(d),
            ["cos²"] = d => Math.Cos(d) * Math.Cos(d),
            ["exp(-|x|)"] = d => Math.Exp(-Math.Abs(d)),
            ["1/(1+|x|)"] = d => 1.0 / (1.0 + Math.Abs(d)),
        };

        var results = new Dictionary<string, AlignmentLawReport>();
        int offset = 0;
        foreach (var (law, fn) in forceLaws)
        {
            var data = GenerateAlignmentData(rMin, rMax, rStep,
                law, k, lambda, nPerGroup, seedsPerPoint, baseSeed + offset);
            results[law] = FitAlignmentLaws(data, fn);
            offset += 100000;
        }

        return results;
    }

    // ══════════════════════════════════════════════════════════════════
    // Helpers
    // ══════════════════════════════════════════════════════════════════

    private static (double[], double) FitPoly(double[] X, double[] Y, int degree)
    {
        int n = X.Length, k = degree + 1;
        double[,] XTX = new double[k, k];
        double[] XTY = new double[k];
        for (int i = 0; i < n; i++)
        {
            double[] pw = new double[k]; pw[0] = 1;
            for (int d = 1; d < k; d++) pw[d] = pw[d - 1] * X[i];
            for (int a = 0; a < k; a++)
            { XTY[a] += pw[a] * Y[i]; for (int b = 0; b < k; b++) XTX[a, b] += pw[a] * pw[b]; }
        }
        double[] beta = SolveGauss(XTX, XTY, k);
        double ssRes = 0, ssTot = 0, meanY = Y.Average();
        for (int i = 0; i < n; i++)
        {
            double pred = beta[0], xp = 1;
            for (int d = 1; d <= degree; d++) { xp *= X[i]; pred += beta[d] * xp; }
            ssRes += (Y[i] - pred) * (Y[i] - pred);
            ssTot += (Y[i] - meanY) * (Y[i] - meanY);
        }
        return (beta, ssTot > 1e-15 ? 1 - ssRes / ssTot : 0);
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
                double f = M[row, col] / M[col, col];
                for (int j = col; j <= n; j++) M[row, j] -= f * M[col, j];
            }
        }
        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double s = M[i, n];
            for (int j = i + 1; j < n; j++) s -= M[i, j] * x[j];
            x[i] = Math.Abs(M[i, i]) > 1e-15 ? s / M[i, i] : 0;
        }
        return x;
    }
}
