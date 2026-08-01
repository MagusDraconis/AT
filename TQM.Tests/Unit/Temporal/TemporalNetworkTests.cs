using TQM.Core.Temporal;

namespace TQM.Tests.Unit.Temporal;

public class TemporalNetworkTests
{
    [Fact]
    public void Constructor_WithPositiveCount_CreatesEmptyNetwork()
    {
        var network = new TemporalNetwork(10);

        Assert.Equal(0, network.NodeCount);
        Assert.Equal(10, network.Matrix.Size);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_WithNonPositiveCount_ThrowsArgumentOutOfRangeException(int count)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TemporalNetwork(count));
    }

    [Fact]
    public void AddNode_AddsNodeToNetwork()
    {
        var network = new TemporalNetwork(5);
        var node = new TemporalNode(0, phase: 1.0);

        network.AddNode(node);

        Assert.Equal(1, network.NodeCount);
        Assert.Same(node, network.Nodes[0]);
    }

    [Fact]
    public void AddNode_ExceedingCapacity_ThrowsInvalidOperationException()
    {
        var network = new TemporalNetwork(1);
        network.AddNode(new TemporalNode(0));

        Assert.Throws<InvalidOperationException>(() => network.AddNode(new TemporalNode(1)));
    }

    [Fact]
    public void AddNode_WithNull_ThrowsArgumentNullException()
    {
        var network = new TemporalNetwork(5);

        Assert.Throws<ArgumentNullException>(() => network.AddNode(null!));
    }

    [Fact]
    public void AddNodes_Bulk_AddsAllNodes()
    {
        var network = new TemporalNetwork(10);
        var nodes = Enumerable.Range(0, 5).Select(i => new TemporalNode(i)).ToList();

        network.AddNodes(nodes);

        Assert.Equal(5, network.NodeCount);
    }

    [Fact]
    public void AveragePhase_WithUniformPhases_ReturnsCorrectMean()
    {
        var network = new TemporalNetwork(3);
        network.AddNodes(new[]
        {
            new TemporalNode(0, phase: 0.0),
            new TemporalNode(1, phase: 0.0),
            new TemporalNode(2, phase: 0.0),
        });

        double avg = network.AveragePhase();

        Assert.Equal(0.0, avg, precision: 6);
    }

    [Fact]
    public void PhaseVariance_WithUniformPhases_ReturnsZero()
    {
        var network = new TemporalNetwork(3);
        network.AddNodes(new[]
        {
            new TemporalNode(0, phase: 1.0),
            new TemporalNode(1, phase: 1.0),
            new TemporalNode(2, phase: 1.0),
        });

        double variance = network.PhaseVariance();

        Assert.Equal(0.0, variance, precision: 6);
    }

    [Fact]
    public void PhaseVariance_WithRandomPhases_ReturnsPositiveValue()
    {
        var network = new TemporalNetwork(4);
        network.AddNodes(new[]
        {
            new TemporalNode(0, phase: 0.0),
            new TemporalNode(1, phase: Math.PI / 2),
            new TemporalNode(2, phase: Math.PI),
            new TemporalNode(3, phase: 3 * Math.PI / 2),
        });

        double variance = network.PhaseVariance();

        Assert.True(variance > 0.0);
    }
}
