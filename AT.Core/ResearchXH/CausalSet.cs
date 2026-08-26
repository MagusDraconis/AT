namespace AT.Core.ResearchXH;

/// <summary>
/// A deterministic finite causal set in 1+1D Minkowski space: events on a (t, x) grid with
/// causal order i ≺ j ⟺ t_j &gt; t_i and |x_j − x_i| ≤ t_j − t_i. Exposes the order relation,
/// Hasse links, interval sizes, and past/future degree — the native inputs for Lorentzian
/// operators (no BDG weights, no metric tensor, no d'Alembertian formula).
/// </summary>
public sealed record CausalSetData(
    int Count,
    int[] Time,
    int[] Space,
    bool[,] Order,      // Order[i, j] = i ≺ j (strict partial order)
    bool[,] Link,       // Hasse links: i ⋖ j (immediate causal relation)
    int[,] Interval,    // |[i, j]| = # events strictly between i and j (−1 if incomparable)
    int[] PastDegree,   // # links into i
    int[] FutureDegree) // # links out of i
{
    /// <summary>True if the relation is a strict partial order with Time as a topological order.</summary>
    public bool IsDag()
    {
        for (int i = 0; i < Count; i++)
            for (int j = 0; j < Count; j++)
            {
                if (i == j && Order[i, j]) return false;          // not irreflexive
                if (Order[i, j] && Order[j, i]) return false;     // not antisymmetric
                if (Order[i, j] && Time[i] >= Time[j]) return false; // time not topological
            }
        return true;
    }

    /// <summary>True if the directed link relation is genuinely directed (A ≠ Aᵀ).</summary>
    public bool IsDirected()
    {
        for (int i = 0; i < Count; i++)
            for (int j = 0; j < Count; j++)
                if (Link[i, j] != Link[j, i]) return true;
        return false;
    }
}

/// <summary>Deterministic 1+1D Minkowski causal-set builder.</summary>
public static class CausalSet
{
    /// <summary>
    /// Build a grid causal set: t ∈ [0..tMax], x ∈ [−xMax..xMax]. Row-major by time.
    /// </summary>
    public static CausalSetData BuildGrid(int tMax, int xMax)
    {
        var ts = new List<int>();
        var xs = new List<int>();
        for (int t = 0; t <= tMax; t++)
            for (int x = -xMax; x <= xMax; x++)
            {
                ts.Add(t);
                xs.Add(x);
            }
        int n = ts.Count;

        var order = new bool[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                int dt = ts[j] - ts[i];
                int dx = Math.Abs(xs[j] - xs[i]);
                order[i, j] = dt > 0 && dx <= dt;
            }

        var link = new bool[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (!order[i, j]) continue;
                bool hasMiddle = false;
                for (int k = 0; k < n && !hasMiddle; k++)
                    if (order[i, k] && order[k, j]) hasMiddle = true;
                link[i, j] = !hasMiddle;
            }

        var interval = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                if (!order[i, j]) { interval[i, j] = -1; continue; }
                int c = 0;
                for (int k = 0; k < n; k++)
                    if (order[i, k] && order[k, j]) c++;
                interval[i, j] = c;
            }

        var past = new int[n];
        var future = new int[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (link[i, j]) { future[i]++; past[j]++; }

        return new CausalSetData(n, ts.ToArray(), xs.ToArray(), order, link, interval, past, future);
    }
}
