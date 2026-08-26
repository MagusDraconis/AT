using AT.Core.Temporal;

namespace AT.Core.Quantum;

/// <summary>
/// Performs eigen-decomposition of a temporal coupling matrix
/// using power iteration with Hotelling deflation.
///
/// Suitable for symmetric matrices. Computes the top-K dominant
/// eigenmodes ranked by absolute eigenvalue magnitude.
/// </summary>
public sealed class TemporalEigenAnalysis
{
    private readonly int _maxIterations;
    private readonly double _tolerance;
    private readonly int _randomSeed;

    public TemporalEigenAnalysis(int maxIterations = 2000, double tolerance = 1e-12, int randomSeed = 137)
    {
        if (maxIterations <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxIterations), "Max iterations must be positive.");
        if (tolerance <= 0)
            throw new ArgumentOutOfRangeException(nameof(tolerance), "Tolerance must be positive.");

        _maxIterations = maxIterations;
        _tolerance = tolerance;
        _randomSeed = randomSeed;
    }

    /// <summary>
    /// Computes the top-K dominant eigenmodes of the coupling matrix.
    /// Modes are ranked by absolute eigenvalue magnitude (dominant first).
    /// </summary>
    public List<TemporalEigenMode> ComputeTopModes(TemporalMatrix matrix, int topK)
    {
        int n = matrix.Size;
        topK = Math.Min(topK, n);

        // Deep-copy the matrix for successive deflation.
        double[,] A = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                A[i, j] = matrix[i, j];

        var modes = new List<TemporalEigenMode>(topK);
        double dominantMagnitude = double.NaN;

        for (int k = 0; k < topK; k++)
        {
            var (eigenvalue, eigenvector) = PowerIteration(A, n, k);

            // Stop if the residual matrix is exhausted (near-zero).
            if (eigenvector == null)
                break;

            double magnitude = Math.Abs(eigenvalue);

            // First mode defines the spectral radius.
            if (k == 0)
                dominantMagnitude = magnitude;

            double stabilityScore = dominantMagnitude > 1e-15
                ? magnitude / dominantMagnitude
                : 1.0;

            modes.Add(new TemporalEigenMode(eigenvalue, eigenvector, magnitude, stabilityScore, k + 1));

            // Hotelling deflation: A ← A − λ·v·vᵀ
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    A[i, j] -= eigenvalue * eigenvector[i] * eigenvector[j];
        }

        return modes;
    }

    /// <summary>
    /// Power iteration on matrix A to find the dominant eigenvalue/eigenvector pair.
    /// </summary>
    private (double eigenvalue, double[]? eigenvector) PowerIteration(double[,] A, int n, int modeIndex)
    {
        var rng = new Random(_randomSeed + modeIndex * 7919);

        double[] v = new double[n];
        for (int i = 0; i < n; i++)
            v[i] = rng.NextDouble() * 2.0 - 1.0; // [-1, 1]

        Normalize(v);
        double[] vNew = new double[n];

        for (int iter = 0; iter < _maxIterations; iter++)
        {
            // v_new = A · v
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int j = 0; j < n; j++)
                    sum += A[i, j] * v[j];
                vNew[i] = sum;
            }

            Normalize(vNew);

            // Compute change for convergence check.
            double diff = 0.0;
            for (int i = 0; i < n; i++)
            {
                double d = vNew[i] - v[i];
                diff += d * d;
            }

            // Swap buffers for next iteration.
            (v, vNew) = (vNew, v);

            if (Math.Sqrt(diff) < _tolerance)
            {
                double lambda = RayleighQuotient(A, v, n);

                // Reject near-zero eigenvalues (residual noise after deflation).
                if (Math.Abs(lambda) < 1e-14)
                    return (lambda, null);

                return (lambda, (double[])v.Clone());
            }
        }

        // Final Rayleigh quotient even if convergence tolerance not met.
        double finalLambda = RayleighQuotient(A, v, n);
        if (Math.Abs(finalLambda) < 1e-14)
            return (finalLambda, null);

        return (finalLambda, (double[])v.Clone());
    }

    /// <summary>
    /// Rayleigh quotient: λ = (vᵀ·A·v) / (vᵀ·v).
    /// </summary>
    private static double RayleighQuotient(double[,] A, double[] v, int n)
    {
        double numerator = 0.0, denominator = 0.0;
        for (int i = 0; i < n; i++)
        {
            double av = 0.0;
            for (int j = 0; j < n; j++)
                av += A[i, j] * v[j];
            numerator += v[i] * av;
            denominator += v[i] * v[i];
        }

        return denominator > 1e-30 ? numerator / denominator : 0.0;
    }

    /// <summary>
    /// Normalizes a vector to unit L2 norm in-place.
    /// </summary>
    internal static void Normalize(double[] v)
    {
        double norm = 0.0;
        for (int i = 0; i < v.Length; i++)
            norm += v[i] * v[i];
        norm = Math.Sqrt(norm);

        if (norm < 1e-15)
            return;

        for (int i = 0; i < v.Length; i++)
            v[i] /= norm;
    }
}
