namespace AT.Core.Research;

/// <summary>
/// Simulates graph growth: nodes are added over time,
/// expanding the spectrum and potentially enabling open-ended innovation.
///
/// AT-X004: Graph Growth Physics
/// </summary>
public static class DynamicNodeModel
{
    /// <summary>
    /// Simulate a growing 1D chain: start with initial nodes, add one every addInterval gens.
    /// </summary>
    public static List<GraphGrowthMetrics.GrowthState> SimulateGrowth(
        int initialNodes, int addInterval, int totalGenerations, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var history = new List<GraphGrowthMetrics.GrowthState>();
        int N = initialNodes;
        double range = 0.3;

        for (int t = 0; t < totalGenerations; t++)
        {
            // Add a new node at specified interval (after the first generation).
            if (t > 0 && t % addInterval == 0)
                N++;

            // Build adjacency for a 1D chain of N nodes.
            var A = new double[N, N];
            for (int i = 0; i < N - 1; i++)
                A[i, i + 1] = A[i + 1, i] = 1.0;

            // Graph Laplacian.
            var L = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                double deg = 0;
                for (int j = 0; j < N; j++) deg += A[i, j];
                L[i, i] = deg;
                for (int j = 0; j < N; j++)
                    if (i != j) L[i, j] = -A[i, j];
            }

            // Species count = N (each eigenvalue is a distinct species for 1D chain).
            int species = N;

            // Spectral entropy = -Σ p_k·log(p_k) where p_k = λ_k / Σλ_j.
            double sumEvals = 0;
            for (int k = 1; k <= N; k++)
            {
                double ev = 2.0 - 2.0 * Math.Cos(Math.PI * k / (N + 1));
                sumEvals += Math.Abs(ev);
            }
            double sEntropy = 0;
            for (int k = 1; k <= N; k++)
            {
                double ev = 2.0 - 2.0 * Math.Cos(Math.PI * k / (N + 1));
                double p = Math.Abs(ev) / Math.Max(sumEvals, 1e-10);
                if (p > 0) sEntropy -= p * Math.Log(p);
            }

            // Graph entropy from degree distribution (chain: mostly degree 2).
            double gEntropy = N > 1
                ? -((2.0 / N) * Math.Log(2.0 / N) + ((N - 2.0) / N) * Math.Log((N - 2.0) / N))
                : 0;

            history.Add(new GraphGrowthMetrics.GrowthState(t, N, species, sEntropy, gEntropy));
        }

        return history;
    }
}
