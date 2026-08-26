namespace AT.Core.Research;

/// <summary>
/// Simulates dynamic Q-charge graphs where nodes move over time,
/// changing the graph Laplacian L_Q(t) and its spectrum.
///
/// AT-X002: Dynamic Graph Physics
/// </summary>
public static class DynamicGraphModel
{
    /// <summary>
    /// Run dynamic graph evolution for Q charges on a 1D interval.
    /// </summary>
    public static List<GraphEvolutionMetrics.DynamicState> Simulate(
        int Q, int generations, double mobility = 0.01, double range = 0.3, int? seed = null)
    {
        var rng = seed.HasValue ? new Random(seed.Value) : new Random(42);
        var history = new List<GraphEvolutionMetrics.DynamicState>();

        // Initialize Q charges uniformly on [0, 1].
        var positions = new double[Q];
        for (int i = 0; i < Q; i++)
            positions[i] = (double)i / (Q - 1 + 1e-10);

        for (int t = 0; t < generations; t++)
        {
            // Build adjacency based on distance.
            var A = new double[Q, Q];
            for (int i = 0; i < Q; i++)
            for (int j = i + 1; j < Q; j++)
                if (Math.Abs(positions[i] - positions[j]) < range)
                    A[i, j] = A[j, i] = 1.0;

            // Graph Laplacian.
            var L = new double[Q, Q];
            for (int i = 0; i < Q; i++)
            {
                double deg = 0;
                for (int j = 0; j < Q; j++) deg += A[i, j];
                L[i, i] = deg;
                for (int j = 0; j < Q; j++)
                    if (i != j) L[i, j] = -A[i, j];
            }

            // Compute eigenvalues (analytic for this simple case).
            var evals = EstimateEigenvalues(L, Q);

            // Count distinct eigenvalues (species proxy).
            int speciesCount = evals.Distinct().Count();

            // Spectral drift from previous step.
            double drift = 0;
            if (history.Count > 0)
            {
                var prev = history.Last().Eigenvalues;
                int n = Math.Min(evals.Length, prev.Length);
                for (int i = 0; i < n; i++)
                    drift += Math.Abs(evals[i] - prev[i]);
                drift /= n;
            }

            // Graph entropy (from degree distribution).
            var degrees = new int[Q];
            for (int i = 0; i < Q; i++)
                for (int j = 0; j < Q; j++)
                    if (A[i, j] > 0) degrees[i]++;
            double entropy = 0;
            foreach (var g in degrees.GroupBy(d => d))
            {
                double p = (double)g.Count() / Q;
                if (p > 0) entropy -= p * Math.Log(p);
            }

            history.Add(new GraphEvolutionMetrics.DynamicState(
                t, (double[])positions.Clone(), L,
                evals, speciesCount, drift, entropy));

            // Move charges (Brownian motion with reflective boundaries).
            for (int i = 0; i < Q; i++)
            {
                positions[i] += (rng.NextDouble() - 0.5) * mobility;
                positions[i] = Math.Clamp(positions[i], 0.0, 1.0);
            }

            // Sort positions to maintain ordering (1D chain identity).
            Array.Sort(positions);
        }

        return history;
    }

    private static double[] EstimateEigenvalues(double[,] L, int n)
    {
        var evals = new double[n];
        // For graphs that are roughly chain-like, eigenvalues ≈ 2-2cos(πk/(n+1)) scaled by mean degree.
        double meanDeg = 0;
        for (int i = 0; i < n; i++) meanDeg += L[i, i];
        meanDeg /= n;

        for (int k = 0; k < n; k++)
            evals[k] = meanDeg * (1.0 - Math.Cos(Math.PI * (k + 1) / (n + 1)));

        return evals;
    }
}
