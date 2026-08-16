using MathNet.Numerics.LinearAlgebra;

namespace TQM.Core.ResearchXH;

/// <summary>
/// Spectral observables of a graph Laplacian, used to probe whether curvature is encoded
/// in spectra. Deterministic: closed-form graph constructions + symmetric EVD only.
/// </summary>
public static class SpectralCurvature
{
    /// <summary>Sorted (ascending) real eigenvalues of a symmetric matrix.</summary>
    public static double[] Eigenvalues(double[,] matrix)
    {
        var mat = Matrix<double>.Build.DenseOfArray(matrix);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] evals = evd.EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }

    /// <summary>
    /// Sorted real parts of the eigenvalues of a general (possibly non-symmetric) matrix.
    /// For a strictly triangular (retarded/advanced) operator the spectrum is the diagonal
    /// (zero for a zero diagonal → nilpotent).
    /// </summary>
    public static double[] GeneralEigenvalues(double[,] matrix)
    {
        var mat = Matrix<double>.Build.DenseOfArray(matrix);
        var evd = mat.Evd();
        double[] evals = evd.EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }

    /// <summary>Heat trace Z(t) = Σ_k exp(−t λ_k).</summary>
    public static double HeatTrace(double[] evals, double t)
    {
        double sum = 0.0;
        foreach (double l in evals) sum += Math.Exp(-t * l);
        return sum;
    }

    /// <summary>Heat trace derivative Z'(t) = −Σ_k λ_k exp(−t λ_k) (negative for all t &gt; 0).</summary>
    public static double HeatTraceDerivative(double[] evals, double t)
    {
        double sum = 0.0;
        foreach (double l in evals) sum += l * Math.Exp(-t * l);
        return -sum;
    }

    /// <summary>Mean eigenvalue under the heat-kernel measure: ⟨λ⟩(t) = Σ λ e^(−tλ) / Σ e^(−tλ) = −Z'/Z.</summary>
    public static double MeanEigenvalue(double[] evals, double t)
    {
        double z = 0.0, zl = 0.0;
        foreach (double l in evals)
        {
            double w = Math.Exp(-t * l);
            z += w;
            zl += l * w;
        }
        return z == 0.0 ? double.NaN : zl / z;
    }

    /// <summary>
    /// Spectral (heat-kernel) entropy S(t) = −Σ p_k ln p_k with p_k = e^(−tλ_k)/Z(t).
    /// A uniform measure (t→0) has maximal entropy ln N; a single surviving mode (t→∞) has entropy 0.
    /// </summary>
    public static double SpectralEntropy(double[] evals, double t)
    {
        double z = 0.0;
        foreach (double l in evals) z += Math.Exp(-t * l);
        double s = 0.0;
        foreach (double l in evals)
        {
            double p = Math.Exp(-t * l) / z;
            if (p > 1e-300) s -= p * Math.Log(p);
        }
        return s;
    }

    /// <summary>
    /// Spectral Curvature Indicator: the deviation of the effective spectral dimension
    /// d_s(t) = 2t·⟨λ⟩(t) from the ambient dimension d = 2.
    /// SCI(t) = d_s − 2 = 2t·⟨λ⟩(t) − 2.
    /// The heat-kernel spectral dimension carries a curvature correction, so the sign of SCI
    /// tracks the scalar-curvature sign: flat ⇒ SCI ≈ 0, positive ⇒ SCI &gt; 0, negative ⇒ SCI &lt; 0.
    /// </summary>
    public static double SpectralCurvatureIndicator(double[] evals, double t)
        => 2.0 * t * MeanEigenvalue(evals, t) - 2.0;

    /// <summary>Spectral zeta ζ(s) = Σ_{λ_k>0} λ_k^(−s) (zero mode excluded).</summary>
    public static double SpectralZeta(double[] evals, double s)
    {
        double sum = 0.0;
        foreach (double l in evals)
            if (l > 1e-12) sum += Math.Pow(l, -s);
        return sum;
    }

    /// <summary>Smallest positive eigenvalue (spectral gap); NaN if none.</summary>
    public static double SpectralGap(double[] evals)
    {
        foreach (double l in evals)
            if (l > 1e-10) return l;
        return double.NaN;
    }

    /// <summary>
    /// Weyl dimension estimate from the UNNORMALIZED Laplacian spectrum (which mirrors the
    /// continuum −Δ). Uses the cumulative counting function N(λ) = #{λ_k ≤ λ} and fits
    /// log N(λ) = (d/2)·log λ + c over the LOW-λ regime (λ ≤ 0.25·λ_max), where the graph
    /// best approximates the continuum Weyl law N(λ) ∝ λ^(d/2). Returns d = 2·slope.
    /// </summary>
    public static double WeylDimension(double[] evals)
    {
        var pos = evals.Where(l => l > 1e-8).ToArray(); // exclude the zero mode
        if (pos.Length < 16) return double.NaN;

        double lamMax = pos[^1] * 0.25;
        var unique = pos.Where(l => l <= lamMax).Distinct().OrderBy(l => l).ToArray();
        if (unique.Length < 8) return double.NaN;

        var xs = new List<double>();
        var ys = new List<double>();
        foreach (double l in unique)
        {
            if (l <= 1e-8) continue;
            double count = 0.0;
            foreach (double p in pos) if (p <= l) count++;
            xs.Add(Math.Log(l));
            ys.Add(Math.Log(count));
        }

        double sx = 0.0, sy = 0.0, sxx = 0.0, sxy = 0.0;
        int m = xs.Count;
        for (int i = 0; i < m; i++)
        {
            sx += xs[i]; sy += ys[i]; sxx += xs[i] * xs[i]; sxy += xs[i] * ys[i];
        }
        double denom = m * sxx - sx * sx;
        double slope = denom == 0.0 ? double.NaN : (m * sxy - sx * sy) / denom;
        return 2.0 * slope; // slope = d/2
    }

    /// <summary>
    /// Two-sample Kolmogorov–Smirnov distance between two empirical eigenvalue CDFs:
    /// D = max_t |F_x(t) − F_y(t)|. Scale-free spectral distinguishability measure.
    /// </summary>
    public static double KolmogorovSmirnov(double[] x, double[] y)
    {
        int n = x.Length, m = y.Length;
        int i = 0, j = 0;
        double d = 0.0;
        while (i < n && j < m)
        {
            if (x[i] <= y[j]) i++; else j++;
            double fx = (double)i / n;
            double fy = (double)j / m;
            d = Math.Max(d, Math.Abs(fx - fy));
        }
        return d;
    }
}
