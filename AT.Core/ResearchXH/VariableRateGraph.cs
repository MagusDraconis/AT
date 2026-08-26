namespace AT.Core.ResearchXH;

/// <summary>
/// Flat square [-1,1]² with deterministic NON-UNIFORM actualization rate (event density):
/// Chebyshev-node placement (dense near the boundary, sparse in the centre), connected by a
/// Euclidean ε-threshold graph. The underlying manifold is flat (R = 0); only the counting
/// measure (event density) varies — the "variable event-rate regions" of G4-T.
/// </summary>
public static class VariableRateGraph
{
    public static GeometricGraph Build(int nPerSide, double epsilon, double epsilonGrowth = 1.1, int maxIterations = 400)
    {
        var pts = ChebyshevGrid(nPerSide);
        int n = pts.Length;
        double eps = epsilon;
        var a = GraphFactory.ThresholdGraph(n, (i, j) => Euclid(pts[i], pts[j]), eps);
        while (!GraphFactory.Connected(a, n) && maxIterations-- > 0)
        {
            eps *= epsilonGrowth;
            a = GraphFactory.ThresholdGraph(n, (i, j) => Euclid(pts[i], pts[j]), eps);
        }

        return new GeometricGraph($"Flat variable-rate grid (Chebyshev, N={n}, ε={eps:F3})", 2, 0.0, a);
    }

    /// <summary>Chebyshev nodes in each axis: x_i = cos(π(i+0.5)/n), dense near ±1.</summary>
    private static (double x, double y)[] ChebyshevGrid(int n)
    {
        var pts = new (double, double)[n * n];
        for (int i = 0; i < n; i++)
        {
            double x = Math.Cos(Math.PI * (i + 0.5) / n);
            for (int j = 0; j < n; j++)
            {
                double y = Math.Cos(Math.PI * (j + 0.5) / n);
                pts[j * n + i] = (x, y);
            }
        }
        return pts;
    }

    private static double Euclid((double x, double y) p, (double x, double y) q)
    {
        double dx = p.x - q.x, dy = p.y - q.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
