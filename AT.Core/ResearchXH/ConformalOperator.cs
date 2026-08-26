namespace AT.Core.ResearchXH;

/// <summary>Conformal (density-weighted) graph operator variants.</summary>
public enum ConformalOperatorKind
{
    /// <summary>Unnormalized Laplacian L = D − A (density-weighted in the continuum).</summary>
    Unnormalized,

    /// <summary>Normalized symmetric Laplacian D^(−1/2) L D^(−1/2) (degree-normalized).</summary>
    Normalized,

    /// <summary>Symmetric ρ^(−1) weighting: ρ^(−1/2) L ρ^(−1/2).</summary>
    RhoInverse,

    /// <summary>Symmetric ρ^(−2) weighting: ρ^(−1) L ρ^(−1) — the conformal operator ≈ Δ_g.</summary>
    RhoInverseSquared
}

/// <summary>
/// Native conformal operators built from a graph and its per-vertex event density ρ.
/// Continuum limits (L ≈ ρ·(−Δ_η), density-weighted):
///   Unnormalized      → ρ·(−Δ_η)      (degree/density-sensitive)
///   Normalized        → −Δ_η          (density-invariant via degree)
///   RhoInverse        → −Δ_η          (density-invariant via analytic ρ)
///   RhoInverseSquared → ρ⁻¹(−Δ_η) = −Δ_g   (conformal Laplace–Beltrami)
/// All returned matrices are symmetric (real spectrum).
/// </summary>
public static class ConformalOperator
{
    public static double[,] Build(GeometricGraph g, ConformalOperatorKind kind)
    {
        int n = g.VertexCount;
        double[] deg = g.Degrees().Select(d => (double)d).ToArray();
        double[] rho = g.VertexDensity();

        double[] w = kind switch
        {
            ConformalOperatorKind.Unnormalized => Enumerable.Repeat(1.0, n).ToArray(),
            ConformalOperatorKind.Normalized => deg.Select(d => Math.Pow(Math.Max(d, 1e-12), -0.5)).ToArray(),
            ConformalOperatorKind.RhoInverse => rho.Select(r => Math.Pow(Math.Max(r, 1e-12), -0.5)).ToArray(),
            ConformalOperatorKind.RhoInverseSquared => rho.Select(r => Math.Pow(Math.Max(r, 1e-12), -1.0)).ToArray(),
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };

        var m = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double degree = 0.0;
            for (int j = 0; j < n; j++)
                if (i != j) degree += g.Adjacency[i, j];
            m[i, i] = degree * w[i] * w[i];
            for (int j = 0; j < n; j++)
                if (i != j) m[i, j] = -g.Adjacency[i, j] * w[i] * w[j];
        }
        return m;
    }

    public static double[] Eigenvalues(GeometricGraph g, ConformalOperatorKind kind)
        => SpectralCurvature.Eigenvalues(Build(g, kind));

    /// <summary>
    /// General two-parameter density-weighted operator ρ^(−a) L ρ^(−b), symmetrized so the
    /// spectrum is real: M = (ρ^(−a) L ρ^(−b) + ρ^(−b) L ρ^(−a))/2. For a=b this reduces to
    /// ρ^(−a) L ρ^(−a); a=b=1 is the conformal operator Lc = ρ⁻¹ L ρ⁻¹.
    /// </summary>
    public static double[,] BuildGeneral(GeometricGraph g, double a, double b)
        => BuildGeneral(g.UnnormalizedLaplacian(), g.VertexDensity(), a, b);

    /// <summary>Symmetrized ρ^(−a) L ρ^(−b) given the unnormalized Laplacian L and density ρ.</summary>
    public static double[,] BuildGeneral(double[,] laplacian, double[] rho, double a, double b)
    {
        int n = laplacian.GetLength(0);
        double[] wa = rho.Select(r => Math.Pow(Math.Max(r, 1e-12), -a)).ToArray();
        double[] wb = rho.Select(r => Math.Pow(Math.Max(r, 1e-12), -b)).ToArray();

        var m = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                m[i, j] = 0.5 * laplacian[i, j] * (wa[i] * wb[j] + wb[i] * wa[j]);
        return m;
    }

    public static double[] EigenvaluesGeneral(GeometricGraph g, double a, double b)
        => SpectralCurvature.Eigenvalues(BuildGeneral(g, a, b));
}
