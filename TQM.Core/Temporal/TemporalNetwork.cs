namespace TQM.Core.Temporal;

/// <summary>
/// A network of temporal nodes interconnected via a coupling matrix.
/// </summary>
public sealed class TemporalNetwork
{
    private readonly List<TemporalNode> _nodes;
    private readonly TemporalMatrix _matrix;

    public IReadOnlyList<TemporalNode> Nodes => _nodes;
    public TemporalMatrix Matrix => _matrix;
    public int NodeCount => _nodes.Count;

    public TemporalNetwork(int nodeCount)
    {
        if (nodeCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(nodeCount), "Node count must be positive.");

        _nodes = new List<TemporalNode>(nodeCount);
        _matrix = new TemporalMatrix(nodeCount);
    }

    public void AddNode(TemporalNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (_nodes.Count >= _matrix.Size)
            throw new InvalidOperationException(
                $"Cannot add more nodes. Network capacity is {_matrix.Size}.");

        _nodes.Add(node);
    }

    public void AddNodes(IEnumerable<TemporalNode> nodes)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        foreach (var node in nodes)
            AddNode(node);
    }

    /// <summary>
    /// Computes the arithmetic mean of all node phases.
    /// </summary>
    public double AveragePhase()
    {
        if (_nodes.Count == 0)
            return 0.0;

        double sumSin = 0.0, sumCos = 0.0;
        foreach (var node in _nodes)
        {
            sumSin += Math.Sin(node.Phase);
            sumCos += Math.Cos(node.Phase);
        }

        return Math.Atan2(sumSin / _nodes.Count, sumCos / _nodes.Count);
    }

    /// <summary>
    /// Returns the phase variance (circular variance) of the node ensemble.
    /// </summary>
    public double PhaseVariance()
    {
        if (_nodes.Count == 0)
            return 0.0;

        double sumSin = 0.0, sumCos = 0.0;
        foreach (var node in _nodes)
        {
            sumSin += Math.Sin(node.Phase);
            sumCos += Math.Cos(node.Phase);
        }

        double r = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / _nodes.Count;
        return 1.0 - r;
    }
}
