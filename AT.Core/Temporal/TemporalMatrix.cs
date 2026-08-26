namespace AT.Core.Temporal;

/// <summary>
/// An N×N coupling matrix defining interaction strengths between temporal nodes.
/// </summary>
public sealed class TemporalMatrix
{
    private readonly double[,] _couplings;

    public int Size { get; }

    public TemporalMatrix(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Matrix size must be positive.");

        Size = size;
        _couplings = new double[size, size];
    }

    public double this[int i, int j]
    {
        get
        {
            ValidateIndices(i, j);
            return _couplings[i, j];
        }
        set
        {
            ValidateIndices(i, j);
            _couplings[i, j] = value;
        }
    }

    public double GetCoupling(int i, int j)
    {
        ValidateIndices(i, j);
        return _couplings[i, j];
    }

    public void SetCoupling(int i, int j, double value)
    {
        ValidateIndices(i, j);
        _couplings[i, j] = value;
    }

    /// <summary>
    /// Validates that the matrix is well-formed (size > 0, all entries finite).
    /// </summary>
    public bool IsValid()
    {
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                if (!double.IsFinite(_couplings[i, j]))
                    return false;
        return true;
    }

    /// <summary>
    /// Checks whether the coupling matrix is symmetric within the given tolerance.
    /// </summary>
    public bool IsSymmetric(double tolerance = 1e-10)
    {
        for (int i = 0; i < Size; i++)
            for (int j = i + 1; j < Size; j++)
                if (Math.Abs(_couplings[i, j] - _couplings[j, i]) > tolerance)
                    return false;
        return true;
    }

    /// <summary>
    /// Builds the weighted graph Laplacian L_W = D_K − K from this coupling matrix, where
    /// D_K = diag(Σ_j K_ij) is the weighted degree. This is the discrete Laplace–Beltrami
    /// operator for the weighted graph whose edge weights are the coupling strengths K_ij.
    /// </summary>
    public double[,] BuildWeightedLaplacian()
    {
        var laplacian = new double[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            double degree = 0.0;
            for (int j = 0; j < Size; j++)
            {
                if (i == j) continue;
                degree += _couplings[i, j];
            }
            laplacian[i, i] = degree;
            for (int j = 0; j < Size; j++)
            {
                if (i == j) continue;
                laplacian[i, j] = -_couplings[i, j];
            }
        }
        return laplacian;
    }

    /// <summary>
    /// Fills this matrix with distance-dependent coupling from spatial oscillator positions.
    /// Kᵢⱼ = K · exp(−dᵢⱼ / λ), where dᵢⱼ is the Euclidean distance between oscillators i and j.
    /// </summary>
    public void FillSpatialCoupling(IReadOnlyList<TemporalNode> nodes, double k, double lambda, bool normalize)
    {
        if (nodes.Count != Size)
            throw new ArgumentException($"Node count ({nodes.Count}) must match matrix size ({Size}).");

        for (int i = 0; i < Size; i++)
        {
            for (int j = i + 1; j < Size; j++)
            {
                double dx = nodes[i].X - nodes[j].X;
                double dy = nodes[i].Y - nodes[j].Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                double coupling = k * Math.Exp(-dist / lambda);

                _couplings[i, j] = coupling;
                _couplings[j, i] = coupling;
            }
        }

        // Normalize each row so row sum = 1 (optional).
        if (normalize)
        {
            for (int i = 0; i < Size; i++)
            {
                double rowSum = 0;
                for (int j = 0; j < Size; j++)
                    if (i != j) rowSum += _couplings[i, j];

                if (rowSum > 1e-15)
                    for (int j = 0; j < Size; j++)
                        if (i != j) _couplings[i, j] /= rowSum;
            }
        }
    }

    /// <summary>
    /// Creates a deep copy of the coupling matrix.
    /// </summary>
    public TemporalMatrix Clone()
    {
        var clone = new TemporalMatrix(Size);
        Array.Copy(_couplings, clone._couplings, _couplings.Length);
        return clone;
    }

    private void ValidateIndices(int i, int j)
    {
        if (i < 0 || i >= Size)
            throw new ArgumentOutOfRangeException(nameof(i), $"Row index {i} out of range [0, {Size - 1}].");
        if (j < 0 || j >= Size)
            throw new ArgumentOutOfRangeException(nameof(j), $"Column index {j} out of range [0, {Size - 1}].");
    }
}
