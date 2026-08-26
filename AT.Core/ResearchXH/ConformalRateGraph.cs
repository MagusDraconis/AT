namespace AT.Core.ResearchXH;

/// <summary>
/// Flat square [-1,1]² with a deterministic actualization-rate gradient ρ(x) = 1 + a·x²
/// (the counting measure), built as a Euclidean ε-threshold graph. The native conformal
/// factor is f = ρ^(2/d) = ρ (in d=2), so the induced conformally-flat metric g = f·η has
/// scalar curvature R(x) = −(2/f)·(ln ρ)'' = −4a(1−a x²)/(1+a x²)³, i.e. R(0) = −4a.
/// a &gt; 0 ⇒ R &lt; 0; a &lt; 0 ⇒ R &gt; 0.
/// </summary>
public static class ConformalRateGraph
{
    public static GeometricGraph Build(double a, int nPerSide, double epsilon,
        double epsilonGrowth = 1.1, int maxIterations = 400)
    {
        var pts = Grid(a, nPerSide);
        int n = pts.Length;
        double eps = epsilon;
        var adj = GraphFactory.ThresholdGraph(n, (i, j) => Euclid(pts[i], pts[j]), eps);
        while (!GraphFactory.Connected(adj, n) && maxIterations-- > 0)
        {
            eps *= epsilonGrowth;
            adj = GraphFactory.ThresholdGraph(n, (i, j) => Euclid(pts[i], pts[j]), eps);
        }
        double r0 = ConformalCurvature(a, 0.0);
        return new GeometricGraph($"Conformal rate ρ=1+{a:G3}x² (R(0)={r0:F2})", 2, r0, adj,
            VertexDensities(a, nPerSide));
    }

    /// <summary>Per-vertex density ρ(x_i, y_j) = 1 + a·x_i² (row-major, matching Grid).</summary>
    private static double[] VertexDensities(double a, int n)
    {
        double[] xs = XSamples(a, n);
        var rho = new double[n * n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                rho[j * n + i] = 1.0 + a * xs[i] * xs[i];
        return rho;
    }

    /// <summary>Analytic scalar curvature of the conformally-flat metric g = (1+a x²)·η.</summary>
    public static double ConformalCurvature(double a, double x)
    {
        double f = 1.0 + a * x * x;
        double lnfpp = 2.0 * a * (1.0 - a * x * x) / (f * f);
        return -2.0 * lnfpp / f;
    }

    private static (double x, double y)[] Grid(double a, int n)
    {
        double[] xs = XSamples(a, n);
        var pts = new (double, double)[n * n];
        for (int i = 0; i < n; i++)
        {
            double x = xs[i];
            for (int j = 0; j < n; j++)
            {
                double y = -1.0 + 2.0 * j / (n - 1.0);
                pts[j * n + i] = (x, y);
            }
        }
        return pts;
    }

    /// <summary>Deterministic inverse-CDF samples for density ρ(x) = 1 + a x² on [-1,1].</summary>
    private static double[] XSamples(double a, int n)
    {
        double ftot = 2.0 + 2.0 * a / 3.0;
        var xs = new double[n];
        for (int i = 0; i < n; i++)
        {
            double u = (i + 0.5) / n;
            double x = 0.0;
            for (int it = 0; it < 100; it++)
            {
                double g = ((x + 1.0) + a / 3.0 * (x * x * x + 1.0)) / ftot;
                double gp = (1.0 + a * x * x) / ftot;
                x -= (g - u) / gp;
            }
            xs[i] = x;
        }
        return xs;
    }

    private static double Euclid((double x, double y) p, (double x, double y) q)
    {
        double dx = p.x - q.x, dy = p.y - q.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
