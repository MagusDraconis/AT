namespace TQM.Core.ResearchXH;

/// <summary>
/// G4-D Phase 1 — local curvature fields. Builds a graph from a per-vertex density field on a
/// FIXED uniform grid (so the adjacency is density-independent and only the counting measure ρ
/// varies), reconstructs the LOCAL curvature via the diagonal heat kernel of Lc = ρ⁻¹ L ρ⁻¹
/// (K_t(x) = Σ e^(−tλ) φ(x)², whose deviation from flat encodes R(x) through the heat-kernel
/// expansion), and compares against the analytic conformal curvature R = −(2/ρ)(ln ρ)″.
/// No new primitives: only ρ, L, and the spectral decomposition.
/// </summary>
public static class CurvatureField
{
    /// <summary>Uniform x-samples on [−1,1].</summary>
    public static double[] UniformXs(int n)
    {
        var xs = new double[n];
        for (int i = 0; i < n; i++) xs[i] = -1.0 + 2.0 * i / (n - 1.0);
        return xs;
    }

    /// <summary>
    /// Build a uniform-grid graph on [−1,1]² whose per-vertex density is the x-only profile
    /// ρ(x_i) = rhoX[i] (expanded over the y direction). Vertices are row-major (index = j·n + i).
    /// </summary>
    public static GeometricGraph Build(double[] rhoX, int n, double epsilon, double growth = 1.1)
    {
        var xs = UniformXs(n);
        var pts = new (double x, double y)[n * n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                pts[j * n + i] = (xs[i], -1.0 + 2.0 * j / (n - 1.0));

        var rho = new double[n * n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                rho[j * n + i] = rhoX[i];

        double eps = epsilon;
        int maxIter = 400;
        var adj = GraphFactory.ThresholdGraph(n * n, (a, b) => Dist(pts[a], pts[b]), eps);
        while (!GraphFactory.Connected(adj, n * n) && maxIter-- > 0)
        {
            eps *= growth;
            adj = GraphFactory.ThresholdGraph(n * n, (a, b) => Dist(pts[a], pts[b]), eps);
        }
        return new GeometricGraph("Local density field", 2, 0.0, adj, rho);
    }

    /// <summary>
    /// Local curvature map from the diagonal heat kernel: R̂(x_i) = (K_geo(x_i) − K_flat(x_i)) /
    /// K_flat(x_i). Both graphs share the same uniform adjacency, so boundary effects cancel.
    /// </summary>
    public static double[] Reconstruct(GeometricGraph flat, GeometricGraph geo, double t)
    {
        var lcFlat = ConformalOperator.Build(flat, ConformalOperatorKind.RhoInverseSquared);
        var lcGeo = ConformalOperator.Build(geo, ConformalOperatorKind.RhoInverseSquared);
        double[] kFlat = SpectralCurvature.LocalHeatKernel(lcFlat, t);
        double[] kGeo = SpectralCurvature.LocalHeatKernel(lcGeo, t);
        var r = new double[geo.VertexCount];
        for (int i = 0; i < r.Length; i++)
            r[i] = (kGeo[i] - kFlat[i]) / kFlat[i];
        return r;
    }

    /// <summary>Collapse a full vertex array to its x-profile by averaging over the y direction.</summary>
    public static double[] XProfile(double[] full, int n)
    {
        var r = new double[n];
        for (int i = 0; i < n; i++)
        {
            double s = 0.0;
            for (int j = 0; j < n; j++) s += full[j * n + i];
            r[i] = s / n;
        }
        return r;
    }

    /// <summary>Analytic conformal curvature R(x) = −(2/ρ)(ln ρ)″ for an x-only profile, via central differences.</summary>
    public static double[] AnalyticCurvature(double[] rhoX, int n)
    {
        var xs = UniformXs(n);
        var r = new double[n];
        for (int i = 0; i < n; i++)
        {
            double h = xs[1] - xs[0];
            int im = Math.Max(0, i - 1), ip = Math.Min(n - 1, i + 1);
            double lnm = Math.Log(rhoX[im]), ln0 = Math.Log(rhoX[i]), lnp = Math.Log(rhoX[ip]);
            double d2 = (lnp - 2.0 * ln0 + lnm) / (h * h);
            r[i] = -2.0 * d2 / rhoX[i];
        }
        return r;
    }

    private static double Dist((double x, double y) a, (double x, double y) b)
    {
        double dx = a.x - b.x, dy = a.y - b.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
