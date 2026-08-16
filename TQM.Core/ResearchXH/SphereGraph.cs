namespace TQM.Core.ResearchXH;

/// <summary>
/// Unit 2-sphere S²: deterministic Fibonacci point set connected by geodesic-distance
/// threshold (an epsilon-graph). Scalar curvature R = 2. Deterministic (no randomness);
/// epsilon is grown in fixed steps only to guarantee connectivity.
/// </summary>
public static class SphereGraph
{
    public static GeometricGraph Build(int n, double epsilon, double epsilonGrowth = 1.1, int maxIterations = 400)
    {
        var pts = FibonacciSphere(n);
        double eps = epsilon;
        var a = GraphFactory.ThresholdGraph(n, (i, j) => Geodesic(pts[i], pts[j]), eps);

        while (!GraphFactory.Connected(a, n) && maxIterations-- > 0)
        {
            eps *= epsilonGrowth;
            a = GraphFactory.ThresholdGraph(n, (i, j) => Geodesic(pts[i], pts[j]), eps);
        }

        return new GeometricGraph($"Unit 2-sphere S² (N={n}, ε={eps:F3})", 2, 2.0, a);
    }

    /// <summary>Deterministic quasi-uniform points on the unit sphere (Fibonacci lattice).</summary>
    private static (double x, double y, double z)[] FibonacciSphere(int n)
    {
        var pts = new (double, double, double)[n];
        double goldenAngle = 2.0 * Math.PI / ((1.0 + Math.Sqrt(5.0)) / 2.0);
        for (int i = 0; i < n; i++)
        {
            double y = 1.0 - 2.0 * (i + 0.5) / n;          // cos(theta) from +1 to -1
            double r = Math.Sqrt(Math.Max(0.0, 1.0 - y * y));
            double th = goldenAngle * i;
            pts[i] = (Math.Cos(th) * r, y, Math.Sin(th) * r);
        }
        return pts;
    }

    private static double Geodesic((double x, double y, double z) p, (double x, double y, double z) q)
    {
        double dot = p.x * q.x + p.y * q.y + p.z * q.z;
        return Math.Acos(Math.Clamp(dot, -1.0, 1.0));
    }
}
