namespace TQM.Core.Temporal;

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
