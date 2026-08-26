namespace AT.Core.ResearchXH;

/// <summary>Shared deterministic helpers for building threshold graphs.</summary>
internal static class GraphFactory
{
    /// <summary>
    /// Builds an unweighted, undirected graph connecting pairs within distance epsilon
    /// (an epsilon-threshold / geometric graph). Deterministic: no randomness.
    /// </summary>
    public static double[,] ThresholdGraph(int n, Func<int, int, double> distance, double epsilon)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (distance(i, j) < epsilon)
                {
                    a[i, j] = 1.0;
                    a[j, i] = 1.0;
                }
        return a;
    }

    /// <summary>Breadth-first connectivity check.</summary>
    public static bool Connected(double[,] a, int n)
    {
        var visited = new bool[n];
        var stack = new Stack<int>();
        stack.Push(0);
        visited[0] = true;
        int count = 1;
        while (stack.Count > 0)
        {
            int u = stack.Pop();
            for (int v = 0; v < n; v++)
            {
                if (a[u, v] != 0.0 && !visited[v])
                {
                    visited[v] = true;
                    stack.Push(v);
                    count++;
                }
            }
        }
        return count == n;
    }
}
