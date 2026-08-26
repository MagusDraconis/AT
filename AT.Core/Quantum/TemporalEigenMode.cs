namespace AT.Core.Quantum;

/// <summary>
/// Represents a single eigenmode of a temporal coupling matrix.
/// Each mode consists of an eigenvalue, its corresponding eigenvector,
/// and derived stability metrics.
/// </summary>
public sealed class TemporalEigenMode
{
    /// <summary>
    /// The eigenvalue λ associated with this mode.
    /// </summary>
    public double Eigenvalue { get; }

    /// <summary>
    /// The normalized eigenvector v (length = matrix dimension).
    /// </summary>
    public double[] Eigenvector { get; }

    /// <summary>
    /// Absolute magnitude |λ| — indicates the mode's intrinsic strength.
    /// </summary>
    public double Magnitude { get; }

    /// <summary>
    /// Stability score ∈ [0, 1]. 1 = most stable/dominant mode.
    /// Computed as this mode's magnitude relative to the spectral radius.
    /// A high score indicates the mode is a clear, persistent collective pattern.
    /// </summary>
    public double StabilityScore { get; }

    /// <summary>
    /// Rank of this mode: 1 = dominant (largest |λ|), 2 = second dominant, etc.
    /// </summary>
    public int Rank { get; }

    public TemporalEigenMode(
        double eigenvalue,
        double[] eigenvector,
        double magnitude,
        double stabilityScore,
        int rank)
    {
        if (eigenvector == null || eigenvector.Length == 0)
            throw new ArgumentException("Eigenvector must be non-empty.", nameof(eigenvector));

        Eigenvalue = eigenvalue;
        Eigenvector = (double[])eigenvector.Clone();
        Magnitude = magnitude;
        StabilityScore = stabilityScore;
        Rank = rank;
    }

    /// <summary>
    /// Locality measure: the inverse participation ratio of the eigenvector.
    /// High IPR → localized mode. Low IPR → delocalized/extended mode.
    /// </summary>
    public double InverseParticipationRatio()
    {
        double sum2 = 0, sum4 = 0;
        for (int i = 0; i < Eigenvector.Length; i++)
        {
            double v2 = Eigenvector[i] * Eigenvector[i];
            sum2 += v2;
            sum4 += v2 * v2;
        }
        return sum4 / (sum2 * sum2);
    }

    public override string ToString() =>
        $"Mode[{Rank}] λ={Eigenvalue,10:F6} |λ|={Magnitude,10:F6} Stability={StabilityScore:F4} IPR={InverseParticipationRatio():F4}";
}
