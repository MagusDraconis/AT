using AT.Core.Quantum;
using AT.Core.Temporal;

namespace AT.Tests.Unit.Quantum;

public class TemporalEigenAnalysisTests
{
    private readonly TemporalEigenAnalysis _analysis = new(maxIterations: 2000, tolerance: 1e-12, randomSeed: 42);

    [Fact]
    public void IdentityMatrix_AllEigenvaluesAreOne()
    {
        var matrix = new TemporalMatrix(5);
        for (int i = 0; i < 5; i++)
            matrix[i, i] = 1.0;

        var modes = _analysis.ComputeTopModes(matrix, 3);

        Assert.Equal(3, modes.Count);

        for (int i = 0; i < modes.Count; i++)
            Assert.Equal(1.0, modes[i].Eigenvalue, precision: 6);
    }

    [Fact]
    public void IdentityMatrix_StabilityScoresEqual()
    {
        var matrix = new TemporalMatrix(4);
        for (int i = 0; i < 4; i++)
            matrix[i, i] = 1.0;

        var modes = _analysis.ComputeTopModes(matrix, 3);

        // All eigenvalues are 1, so all stability scores should be ~1.
        for (int i = 0; i < modes.Count; i++)
            Assert.Equal(1.0, modes[i].StabilityScore, precision: 6);
    }

    [Fact]
    public void DiagonalMatrix_EigenvaluesMatchDiagonalEntries()
    {
        var matrix = new TemporalMatrix(4);
        matrix[0, 0] = 3.0;
        matrix[1, 1] = 2.0;
        matrix[2, 2] = 1.0;
        matrix[3, 3] = 0.5;

        var modes = _analysis.ComputeTopModes(matrix, 4);

        Assert.Equal(4, modes.Count);
        Assert.Equal(3.0, modes[0].Eigenvalue, precision: 5);
        Assert.Equal(2.0, modes[1].Eigenvalue, precision: 5);
        Assert.Equal(1.0, modes[2].Eigenvalue, precision: 5);
        Assert.Equal(0.5, modes[3].Eigenvalue, precision: 5);
    }

    [Fact]
    public void DiagonalMatrix_ModesAreRankedByMagnitude()
    {
        var matrix = new TemporalMatrix(3);
        matrix[0, 0] = -5.0;
        matrix[1, 1] = 3.0;
        matrix[2, 2] = 1.0;

        var modes = _analysis.ComputeTopModes(matrix, 3);

        Assert.Equal(3, modes.Count);
        Assert.True(modes[0].Magnitude > modes[1].Magnitude);
        Assert.True(modes[1].Magnitude > modes[2].Magnitude);
    }

    [Fact]
    public void SymmetricMatrix_EigenvectorsAreOrthogonal()
    {
        var rng = new Random(99);
        var matrix = new TemporalMatrix(6);

        // Build a random symmetric matrix.
        for (int i = 0; i < 6; i++)
            for (int j = i; j < 6; j++)
            {
                double val = rng.NextDouble() * 2 - 1;
                matrix[i, j] = val;
                matrix[j, i] = val;
            }

        var modes = _analysis.ComputeTopModes(matrix, 4);
        Assert.Equal(4, modes.Count);

        // Check pairwise orthogonality: vᵢ · vⱼ ≈ 0 for i ≠ j.
        for (int a = 0; a < modes.Count; a++)
        {
            for (int b = a + 1; b < modes.Count; b++)
            {
                double dot = Dot(modes[a].Eigenvector, modes[b].Eigenvector);
                Assert.True(Math.Abs(dot) < 1e-5,
                    $"Modes {a} and {b} not orthogonal: dot = {dot:E6}");
            }
        }
    }

    [Fact]
    public void Spectrum_ParticipationRatio_ReflectsModeConcentration()
    {
        var matrix = new TemporalMatrix(3);
        matrix[0, 0] = 10.0;
        matrix[1, 1] = 1.0;
        matrix[2, 2] = 0.5;

        var modes = _analysis.ComputeTopModes(matrix, 3);
        var spectrum = TemporalModeSpectrum.FromModes(modes);

        // PR = (Σ|λ|)² / (K · Σ|λ|²). For [10, 1, 0.5]: PR ≈ 0.435.
        // Low PR → mode concentration. Equal modes → PR = 1.
        Assert.Equal(0.435, spectrum.ParticipationRatio, precision: 2);
    }

    [Fact]
    public void ComputeTopModes_RequestMoreThanSize_ReturnsAllModes()
    {
        var matrix = new TemporalMatrix(3);
        for (int i = 0; i < 3; i++)
            matrix[i, i] = 1.0;

        var modes = _analysis.ComputeTopModes(matrix, 100);

        Assert.Equal(3, modes.Count);
    }

    [Fact]
    public void TemporalEigenMode_InverseParticipationRatio_LocalizedVector()
    {
        double[] localized = { 1.0, 0.0, 0.0, 0.0 };
        var mode = new TemporalEigenMode(5.0, localized, 5.0, 1.0, 1);

        double ipr = mode.InverseParticipationRatio();
        Assert.Equal(1.0, ipr, precision: 6); // Fully localized → IPR = 1.
    }

    [Fact]
    public void TemporalEigenMode_InverseParticipationRatio_UniformVector()
    {
        double[] uniform = { 1.0, 1.0, 1.0, 1.0 };
        var mode = new TemporalEigenMode(3.0, uniform, 3.0, 1.0, 1);

        double ipr = mode.InverseParticipationRatio();
        Assert.Equal(0.25, ipr, precision: 6); // Fully delocalized → IPR = 1/N = 0.25.
    }

    private static double Dot(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < a.Length; i++)
            sum += a[i] * b[i];
        return sum;
    }
}
