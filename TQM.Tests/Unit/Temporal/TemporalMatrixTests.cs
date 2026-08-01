using TQM.Core.Temporal;

namespace TQM.Tests.Unit.Temporal;

public class TemporalMatrixTests
{
    [Fact]
    public void Constructor_WithPositiveSize_CreatesMatrix()
    {
        var matrix = new TemporalMatrix(10);

        Assert.Equal(10, matrix.Size);
        Assert.True(matrix.IsValid());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_WithNonPositiveSize_ThrowsArgumentOutOfRangeException(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TemporalMatrix(size));
    }

    [Fact]
    public void Indexer_GetDefault_ReturnsZero()
    {
        var matrix = new TemporalMatrix(5);

        Assert.Equal(0.0, matrix[2, 3]);
    }

    [Fact]
    public void Indexer_SetAndGet_ReturnsStoredValue()
    {
        var matrix = new TemporalMatrix(3);
        matrix[0, 1] = 0.75;

        Assert.Equal(0.75, matrix[0, 1]);
    }

    [Fact]
    public void Indexer_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var matrix = new TemporalMatrix(3);

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = matrix[5, 0]);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = matrix[0, 5]);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = matrix[-1, 0]);
    }

    [Fact]
    public void SetCoupling_And_GetCoupling_WorkCorrectly()
    {
        var matrix = new TemporalMatrix(4);
        matrix.SetCoupling(1, 2, 0.42);

        Assert.Equal(0.42, matrix.GetCoupling(1, 2));
    }

    [Fact]
    public void IsSymmetric_SymmetricMatrix_ReturnsTrue()
    {
        var matrix = new TemporalMatrix(3);
        matrix[0, 1] = 0.5; matrix[1, 0] = 0.5;
        matrix[0, 2] = 0.3; matrix[2, 0] = 0.3;
        matrix[1, 2] = 0.8; matrix[2, 1] = 0.8;

        Assert.True(matrix.IsSymmetric());
    }

    [Fact]
    public void IsSymmetric_AsymmetricMatrix_ReturnsFalse()
    {
        var matrix = new TemporalMatrix(3);
        matrix[0, 1] = 0.5; matrix[1, 0] = 0.9;

        Assert.False(matrix.IsSymmetric());
    }

    [Fact]
    public void IsValid_AllZeros_ReturnsTrue()
    {
        var matrix = new TemporalMatrix(4);
        Assert.True(matrix.IsValid());
    }

    [Fact]
    public void Clone_ProducesIndependentCopy()
    {
        var original = new TemporalMatrix(2);
        original[0, 1] = 0.99;

        var clone = original.Clone();
        clone[0, 1] = 0.11;

        Assert.Equal(0.99, original[0, 1]);
        Assert.Equal(0.11, clone[0, 1]);
    }
}
