using AT.Core.Temporal;

namespace AT.Tests.Unit.Temporal;

public class TemporalNodeTests
{
    [Fact]
    public void Constructor_WithValidId_AssignsCorrectId()
    {
        var node = new TemporalNode(42);
        Assert.Equal(42, node.Id);
    }

    [Fact]
    public void Constructor_WithNegativeId_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TemporalNode(-1));
    }

    [Fact]
    public void Constructor_WithDefaultParameters_SetsDefaults()
    {
        var node = new TemporalNode(0);

        Assert.Equal(0.0, node.Phase);
        Assert.Equal(1.0, node.Frequency);
        Assert.Equal(0.0, node.Energy);
    }

    [Fact]
    public void Constructor_WithCustomParameters_AssignsAllValues()
    {
        var node = new TemporalNode(7, phase: 2.5, frequency: 3.1, energy: 12.0);

        Assert.Equal(7, node.Id);
        Assert.Equal(2.5, node.Phase);
        Assert.Equal(3.1, node.Frequency);
        Assert.Equal(12.0, node.Energy);
    }

    [Fact]
    public void PhaseSetter_UpdatesValue()
    {
        var node = new TemporalNode(1, phase: 0.0);
        node.Phase = Math.PI;

        Assert.Equal(Math.PI, node.Phase);
    }

    [Fact]
    public void FrequencySetter_UpdatesValue()
    {
        var node = new TemporalNode(1, frequency: 1.0);
        node.Frequency = 5.5;

        Assert.Equal(5.5, node.Frequency);
    }

    [Fact]
    public void EnergySetter_UpdatesValue()
    {
        var node = new TemporalNode(1, energy: 0.0);
        node.Energy = 25.0;

        Assert.Equal(25.0, node.Energy);
    }

    [Fact]
    public void ToString_ReturnsFormattedRepresentation()
    {
        var node = new TemporalNode(3, phase: 1.0, frequency: 2.0, energy: 0.5);
        var str = node.ToString();

        Assert.Contains("Node[003]", str);
        Assert.Contains("1.000000", str);
        Assert.Contains("2.000000", str);
    }
}
