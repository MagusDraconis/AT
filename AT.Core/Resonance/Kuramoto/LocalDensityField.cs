using AT.Core.Temporal;

namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Computes a spatial map of local synchronization by partitioning
/// the domain [0,1]×[0,1] into a grid and computing the order parameter
/// within each cell from nearby oscillators.
/// </summary>
public sealed class LocalDensityField
{
    private readonly int _gridSize;
    private readonly double[,] _localR;
    private readonly double[,] _localDensity;

    public int GridSize => _gridSize;
    public double CellSize => 1.0 / _gridSize;

    public LocalDensityField(int gridSize)
    {
        if (gridSize < 2)
            throw new ArgumentOutOfRangeException(nameof(gridSize), "Grid size must be at least 2.");
        _gridSize = gridSize;
        _localR = new double[gridSize, gridSize];
        _localDensity = new double[gridSize, gridSize];
    }

    /// <summary>
    /// Computes the local order parameter and oscillator density for each grid cell.
    /// Uses a neighborhood radius in cell units.
    /// </summary>
    public void Compute(TemporalNetwork network, int neighborhoodCells = 1)
    {
        int n = network.NodeCount;
        var nodes = network.Nodes;

        // Clear.
        for (int gx = 0; gx < _gridSize; gx++)
            for (int gy = 0; gy < _gridSize; gy++)
            {
                _localR[gx, gy] = 0;
                _localDensity[gx, gy] = 0;
            }

        // For each grid cell, find oscillators in neighborhood.
        for (int gx = 0; gx < _gridSize; gx++)
        {
            for (int gy = 0; gy < _gridSize; gy++)
            {
                double sumSin = 0, sumCos = 0;
                int count = 0;

                int xMin = Math.Max(0, gx - neighborhoodCells);
                int xMax = Math.Min(_gridSize - 1, gx + neighborhoodCells);
                int yMin = Math.Max(0, gy - neighborhoodCells);
                int yMax = Math.Min(_gridSize - 1, gy + neighborhoodCells);

                for (int i = 0; i < n; i++)
                {
                    int ox = (int)(nodes[i].X * _gridSize);
                    int oy = (int)(nodes[i].Y * _gridSize);
                    ox = Math.Clamp(ox, 0, _gridSize - 1);
                    oy = Math.Clamp(oy, 0, _gridSize - 1);

                    if (ox >= xMin && ox <= xMax && oy >= yMin && oy <= yMax)
                    {
                        sumSin += Math.Sin(nodes[i].Phase);
                        sumCos += Math.Cos(nodes[i].Phase);
                        count++;
                    }
                }

                _localDensity[gx, gy] = (double)count / n;

                if (count > 0)
                    _localR[gx, gy] = Math.Sqrt(sumSin * sumSin + sumCos * sumCos) / count;
            }
        }
    }

    /// <summary>
    /// Returns the local order parameter at the given grid cell.
    /// </summary>
    public double GetLocalR(int gx, int gy)
    {
        if (gx < 0 || gx >= _gridSize || gy < 0 || gy >= _gridSize)
            return 0;
        return _localR[gx, gy];
    }

    /// <summary>
    /// Returns the local oscillator density at the given grid cell.
    /// </summary>
    public double GetLocalDensity(int gx, int gy)
    {
        if (gx < 0 || gx >= _gridSize || gy < 0 || gy >= _gridSize)
            return 0;
        return _localDensity[gx, gy];
    }

    /// <summary>
    /// Returns the max local R across the entire grid.
    /// </summary>
    public double MaxLocalR()
    {
        double max = 0;
        for (int gx = 0; gx < _gridSize; gx++)
            for (int gy = 0; gy < _gridSize; gy++)
                max = Math.Max(max, _localR[gx, gy]);
        return max;
    }

    /// <summary>
    /// Returns the mean local R across the grid.
    /// </summary>
    public double MeanLocalR()
    {
        double sum = 0;
        for (int gx = 0; gx < _gridSize; gx++)
            for (int gy = 0; gy < _gridSize; gy++)
                sum += _localR[gx, gy];
        return sum / (_gridSize * _gridSize);
    }

    /// <summary>
    /// Returns the number of grid cells where local R exceeds the threshold.
    /// </summary>
    public int CellsAboveThreshold(double threshold)
    {
        int count = 0;
        for (int gx = 0; gx < _gridSize; gx++)
            for (int gy = 0; gy < _gridSize; gy++)
                if (_localR[gx, gy] > threshold)
                    count++;
        return count;
    }
}
