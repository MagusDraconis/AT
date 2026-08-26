namespace AT.Core.Resonance.Kuramoto;

/// <summary>
/// Detects and tracks resonance condensates — spatially contiguous regions
/// where local synchronization exceeds a threshold, representing localized
/// coherent structures.
/// </summary>
public sealed class ResonanceCondensationAnalyzer
{
    /// <summary>
    /// Local R threshold for a grid cell to be considered "condensed."
    /// </summary>
    public double CondensationThreshold { get; set; } = 0.80;

    /// <summary>
    /// Minimum number of contiguous grid cells for a valid condensate.
    /// </summary>
    public int MinCondensateCells { get; set; } = 2;

    /// <summary>
    /// Overlap threshold for tracking condensates across time steps.
    /// </summary>
    public double OverlapThreshold { get; set; } = 0.3;

    private readonly Dictionary<int, CondensateRecord> _activeCondensates = new();
    private int _nextId;

    /// <summary>
    /// Represents a single condensation event at a point in time.
    /// </summary>
    public sealed class CondensateRecord
    {
        public int Id { get; set; }
        public List<(int Gx, int Gy)> Cells { get; set; } = new();
        public double MeanLocalR { get; set; }
        public double PeakLocalR { get; set; }
        public int CellCount { get; set; }
        public int BirthIteration { get; set; }
        public int LastSeenIteration { get; set; }
        public int Lifetime => LastSeenIteration - BirthIteration;
        public double PersistenceScore { get; set; }
    }

    /// <summary>
    /// Detects condensates in the local density field and tracks them across iterations.
    /// Returns the current set of condensates.
    /// </summary>
    public List<CondensateRecord> DetectAndTrack(LocalDensityField field, int iteration)
    {
        var current = DetectCondensates(field, iteration);
        TrackCondensates(current);
        return current;
    }

    private List<CondensateRecord> DetectCondensates(LocalDensityField field, int iteration)
    {
        int gs = field.GridSize;
        var visited = new bool[gs, gs];
        var condensates = new List<CondensateRecord>();

        // Flood-fill to find contiguous above-threshold regions.
        for (int gx = 0; gx < gs; gx++)
        {
            for (int gy = 0; gy < gs; gy++)
            {
                if (visited[gx, gy]) continue;
                if (field.GetLocalR(gx, gy) < CondensationThreshold) continue;

                // BFS flood-fill.
                var cells = new List<(int, int)>();
                var queue = new Queue<(int, int)>();
                queue.Enqueue((gx, gy));
                visited[gx, gy] = true;

                double sumR = 0;
                double peakR = 0;

                while (queue.Count > 0)
                {
                    var (cx, cy) = queue.Dequeue();
                    cells.Add((cx, cy));
                    double r = field.GetLocalR(cx, cy);
                    sumR += r;
                    peakR = Math.Max(peakR, r);

                    // 4-neighbor connectivity.
                    foreach (var (nx, ny) in new[] { (cx - 1, cy), (cx + 1, cy), (cx, cy - 1), (cx, cy + 1) })
                    {
                        if (nx >= 0 && nx < gs && ny >= 0 && ny < gs
                            && !visited[nx, ny]
                            && field.GetLocalR(nx, ny) >= CondensationThreshold)
                        {
                            visited[nx, ny] = true;
                            queue.Enqueue((nx, ny));
                        }
                    }
                }

                if (cells.Count >= MinCondensateCells)
                {
                    condensates.Add(new CondensateRecord
                    {
                        Id = -1,
                        Cells = cells,
                        MeanLocalR = sumR / cells.Count,
                        PeakLocalR = peakR,
                        CellCount = cells.Count,
                        BirthIteration = iteration,
                        LastSeenIteration = iteration
                    });
                }
            }
        }

        return condensates;
    }

    private void TrackCondensates(List<CondensateRecord> current)
    {
        var unmatched = new HashSet<int>(_activeCondensates.Keys);

        foreach (var newCond in current)
        {
            int bestMatch = -1;
            double bestOverlap = 0;

            foreach (var (id, oldCond) in _activeCondensates)
            {
                double overlap = ComputeOverlap(oldCond.Cells, newCond.Cells);
                if (overlap > bestOverlap && overlap >= OverlapThreshold)
                {
                    bestOverlap = overlap;
                    bestMatch = id;
                }
            }

            if (bestMatch >= 0)
            {
                var existing = _activeCondensates[bestMatch];
                existing.LastSeenIteration = newCond.BirthIteration;
                existing.Cells = newCond.Cells;
                existing.MeanLocalR = newCond.MeanLocalR;
                existing.PeakLocalR = newCond.PeakLocalR;
                existing.CellCount = newCond.CellCount;

                int window = existing.LastSeenIteration - existing.BirthIteration + 1;
                existing.PersistenceScore = window > 0 ? (double)existing.Lifetime / window : 0;

                newCond.Id = bestMatch;
                unmatched.Remove(bestMatch);
            }
            else
            {
                int newId = _nextId++;
                newCond.Id = newId;
                newCond.PersistenceScore = 0;
                _activeCondensates[newId] = newCond;
            }
        }

        foreach (int id in unmatched)
            _activeCondensates.Remove(id);
    }

    private static double ComputeOverlap(List<(int, int)> a, List<(int, int)> b)
    {
        var setA = new HashSet<(int, int)>(a);
        var setB = new HashSet<(int, int)>(b);
        int intersect = setA.Count(c => setB.Contains(c));
        int union = new HashSet<(int, int)>(setA.Concat(setB)).Count;
        return union > 0 ? (double)intersect / union : 0;
    }

    /// <summary>
    /// Returns all condensates that have ever been tracked.
    /// </summary>
    public List<CondensateRecord> GetAllCondensates()
    {
        return _activeCondensates.Values.ToList();
    }
}
