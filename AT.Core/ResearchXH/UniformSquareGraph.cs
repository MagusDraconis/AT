namespace AT.Core.ResearchXH;

/// <summary>
/// Uniform-density flat square [-1,1]²: an n×n regular grid connected by a Euclidean
/// ε-threshold graph (same construction as VariableRateGraph, uniform density).
/// Scalar curvature R = 0. Deterministic.
/// </summary>
public static class UniformSquareGraph
{
    public static GeometricGraph Build(int nPerSide, double epsilon, double epsilonGrowth = 1.1, int maxIterations = 400)
    {
        int n = nPerSide;
        var pts = new (double x, double y)[n * n];
        for (int i = 0; i < n; i++)
        {
            double x = -1.0 + 2.0 * i / (n - 1.0);
            for (int j = 0; j < n; j++)
            {
                double y = -1.0 + 2.0 * j / (n - 1.0);
                pts[j * n + i] = (x, y);
            }
        }

        int N = pts.Length;
        double eps = epsilon;
        var a = GraphFactory.ThresholdGraph(N, (i, j) => Euclid(pts[i], pts[j]), eps);
        while (!GraphFactory.Connected(a, N) && maxIterations-- > 0)
        {
            eps *= epsilonGrowth;
            a = GraphFactory.ThresholdGraph(N, (i, j) => Euclid(pts[i], pts[j]), eps);
        }

        return new GeometricGraph($"Uniform flat square ({n}×{n}, ε={eps:F3})", 2, 0.0, a);
    }

    private static double Euclid((double x, double y) p, (double x, double y) q)
    {
        double dx = p.x - q.x, dy = p.y - q.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
