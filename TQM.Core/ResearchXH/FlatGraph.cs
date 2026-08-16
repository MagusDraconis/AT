namespace TQM.Core.ResearchXH;

/// <summary>
/// Flat 2-torus: an n×n grid with periodic boundary conditions (N = n², degree 4).
/// Scalar curvature R = 0. Deterministic.
/// </summary>
public static class FlatGraph
{
    public static GeometricGraph Build(int nPerSide)
    {
        int n = nPerSide;
        int N = n * n;
        var a = new double[N, N];

        for (int x = 0; x < n; x++)
        for (int y = 0; y < n; y++)
        {
            int i = y * n + x;
            int xr = (x + 1) % n, xl = (x - 1 + n) % n;
            int yu = (y + 1) % n, yd = (y - 1 + n) % n;
            a[i, y * n + xr] = 1.0;
            a[i, y * n + xl] = 1.0;
            a[i, yu * n + x] = 1.0;
            a[i, yd * n + x] = 1.0;
        }

        return new GeometricGraph($"Flat 2-torus grid ({n}×{n})", 2, 0.0, a);
    }
}
