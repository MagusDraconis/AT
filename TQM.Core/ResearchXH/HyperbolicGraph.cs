namespace TQM.Core.ResearchXH;

/// <summary>
/// Hyperbolic plane H² in the Poincaré disk model: deterministic concentric-ring point set
/// connected by hyperbolic-distance threshold. Scalar curvature R = −2 (Gaussian curvature −1).
/// Deterministic (no randomness); epsilon is grown in fixed steps only to guarantee connectivity.
/// </summary>
public static class HyperbolicGraph
{
    public static GeometricGraph Build(int targetN, double epsilon, double epsilonGrowth = 1.1, int maxIterations = 400)
    {
        var pts = PoincarePoints(targetN);
        int n = pts.Length;
        double eps = epsilon;
        var a = GraphFactory.ThresholdGraph(n, (i, j) => HyperbolicDistance(pts[i], pts[j]), eps);

        while (!GraphFactory.Connected(a, n) && maxIterations-- > 0)
        {
            eps *= epsilonGrowth;
            a = GraphFactory.ThresholdGraph(n, (i, j) => HyperbolicDistance(pts[i], pts[j]), eps);
        }

        return new GeometricGraph($"Hyperbolic plane H² (N={n}, ε={eps:F3})", 2, -2.0, a);
    }

    /// <summary>
    /// Deterministic points in the unit disk: the origin plus concentric rings whose point
    /// counts are proportional to the hyperbolic circumference (quasi-uniform in H²).
    /// </summary>
    private static (double x, double y)[] PoincarePoints(int targetN)
    {
        var list = new List<(double, double)> { (0.0, 0.0) };
        const int rings = 8;
        const double rMax = 0.9;

        double[] weights = new double[rings];
        for (int m = 1; m <= rings; m++)
        {
            double r = rMax * m / rings;
            weights[m - 1] = 2.0 * Math.PI * r * 2.0 / (1.0 - r * r); // hyperbolic circumference
        }
        double total = weights.Sum();

        for (int m = 1; m <= rings; m++)
        {
            double r = rMax * m / rings;
            int nPerRing = Math.Max(4, (int)Math.Round(targetN * weights[m - 1] / total));
            for (int j = 0; j < nPerRing; j++)
            {
                double th = 2.0 * Math.PI * j / nPerRing;
                list.Add((r * Math.Cos(th), r * Math.Sin(th)));
            }
        }

        return list.ToArray();
    }

    private static double HyperbolicDistance((double x, double y) p, (double x, double y) q)
    {
        double dx = p.x - q.x;
        double dy = p.y - q.y;
        double num = 2.0 * (dx * dx + dy * dy);
        double den = (1.0 - (p.x * p.x + p.y * p.y)) * (1.0 - (q.x * q.x + q.y * q.y));
        double arg = 1.0 + num / Math.Max(den, 1e-12);
        return Math.Acosh(Math.Max(1.0, arg));
    }
}
