namespace AT.Tests.Shared;

/// <summary>
/// Shared machinery for the ResearchY-G gravity-source audits (G_001 Gravitation Source, G_002
/// Density Control): the counting measure (actualization density) field and its two native reads —
/// the conformal acceleration a = -(1/d) grad ln rho (G4-O3) and the conformal scalar curvature
/// R = -2(d-1) rho^(-2/d)[sigma'' + ((d-2)/2)(sigma')^2], sigma = (1/d) ln rho (G4-G2/QG197).
/// Extracted from Y_G_001 so the G-series does not duplicate it.
/// </summary>
public static class DensityField
{
    /// <summary>Piecewise-linear density on an explicit ordered node set (binary search; O(log n) per probe).</summary>
    public static Func<double, double> PiecewiseLinear(double[] values, double[] xs) => x =>
    {
        if (x <= xs[0]) return values[0];
        if (x >= xs[^1]) return values[^1];
        int i = Array.BinarySearch(xs, x);
        if (i >= 0) return values[i];
        i = ~i;                                  // first node above x
        int lo = i - 1;
        double t = (x - xs[lo]) / (xs[i] - xs[lo]);
        return values[lo] * (1.0 - t) + values[i] * t;
    };

    /// <summary>Piecewise-linear density on the node lattice x = 1..n (the mode/cell lattice).</summary>
    public static Func<double, double> PiecewiseLinear(double[] values)
    {
        var xs = new double[values.Length];
        for (int i = 0; i < values.Length; i++) xs[i] = i + 1;
        return PiecewiseLinear(values, xs);
    }

    /// <summary>Native conformal acceleration a = -(1/d) rho'/rho on a continuous density profile.</summary>
    public static double Acceleration(Func<double, double> rho, double x, int d = 3, double h = 1e-6)
        => -(rho(x + h) - rho(x - h)) / (2.0 * h * d * rho(x));

    /// <summary>Conformal scalar curvature R = F(rho) for an ARBITRARY profile (central differences).</summary>
    public static double ScalarCurvatureOf(Func<double, double> rho, double x, int d = 3, double h = 1e-3)
    {
        double r = rho(x);
        double rp = (rho(x + h) - rho(x - h)) / (2.0 * h);
        double rpp = (rho(x + h) - 2.0 * r + rho(x - h)) / (h * h);
        double sp = rp / (d * r);
        double spp = rpp / (d * r) - rp * rp / (d * r * r);
        return -2.0 * (d - 1.0) * Math.Pow(r, -2.0 / d) * (spp + 0.5 * (d - 2.0) * sp * sp);
    }

    /// <summary>A source term of a LOCAL field equation must respond to position; a global scalar cannot.</summary>
    public static double FieldSensitivity(Func<double, double> field, double x, double h = 1e-3)
        => Math.Abs((field(x + h) - field(x - h)) / (2.0 * h));

    /// <summary>L1 difference of two density arrays (the "how much did rho move" measure).</summary>
    public static double L1(double[] a, double[] b)
    {
        double s = 0.0;
        for (int i = 0; i < a.Length; i++) s += Math.Abs(a[i] - b[i]);
        return s;
    }

    /// <summary>Sum of an occupancy (counting-measure) array — the total actualization rate (QG89).</summary>
    public static double Total(double[] rho) => rho.Sum();

    /// <summary>max |a(rho)| over the whole profile, probed at cell midpoints.</summary>
    public static double MaxAbsAcceleration(double[] rho, int d = 3)
    {
        var f = PiecewiseLinear(rho);
        double m = 0.0;
        for (int i = 1; i < rho.Length; i++) m = Math.Max(m, Math.Abs(Acceleration(f, i + 0.5, d)));
        return m;
    }

    /// <summary>max |R(rho)| over the whole profile, probed at cell midpoints.</summary>
    public static double MaxAbsCurvature(double[] rho, int d = 3)
    {
        var f = PiecewiseLinear(rho);
        double m = 0.0;
        for (int i = 1; i < rho.Length; i++) m = Math.Max(m, Math.Abs(ScalarCurvatureOf(f, i + 0.5, d)));
        return m;
    }

    /// <summary>max |Δa| between two density arrays over the shared cell lattice.</summary>
    public static double MaxAccelerationDifference(double[] a, double[] b, int d = 3)
    {
        var fa = PiecewiseLinear(a);
        var fb = PiecewiseLinear(b);
        double m = 0.0;
        for (int i = 1; i < a.Length; i++)
            m = Math.Max(m, Math.Abs(Acceleration(fa, i + 0.5, d) - Acceleration(fb, i + 0.5, d)));
        return m;
    }

    /// <summary>max |ΔR| between two density arrays over the shared cell lattice.</summary>
    public static double MaxCurvatureDifference(double[] a, double[] b, int d = 3)
    {
        var fa = PiecewiseLinear(a);
        var fb = PiecewiseLinear(b);
        double m = 0.0;
        for (int i = 1; i < a.Length; i++)
            m = Math.Max(m, Math.Abs(ScalarCurvatureOf(fa, i + 0.5, d) - ScalarCurvatureOf(fb, i + 0.5, d)));
        return m;
    }

    // ── Counting-measure configurations on the real lattices (shared by G_002 and G_003) ─────────

    /// <summary>D96 eigenspaces (distinct eigenvalues, multiplicities) — A0 = 45, L = 0.53125 (D_048).</summary>
    public static (double[] Distinct, int[] Mult) D96Spaces => D96Lazy.Value;

    /// <summary>Sparse random-graph eigenspaces (degeneracy-free control) — A0 = 96, L = 0 (D_048).</summary>
    public static (double[] Distinct, int[] Mult) RandomSpaces => RandomLazy.Value;

    /// <summary>D96⊗D96⊗D96 tensor-cube eigenspaces (884 736 modes, 20 812 eigenspaces).</summary>
    public static (double[] Distinct, int[] Mult) CubeSpaces => CubeLazy.Value;

    private static readonly Lazy<(double[] Distinct, int[] Mult)> D96Lazy = new(() =>
    {
        var c = SpectralCaseCatalog.D96();
        return (c.Distinct, c.Multiplicities);
    });

    private static readonly Lazy<(double[] Distinct, int[] Mult)> RandomLazy = new(() =>
    {
        var c = SpectralCaseCatalog.Random();
        return (c.Distinct, c.Multiplicities);
    });

    private static readonly Lazy<(double[] Distinct, int[] Mult)> CubeLazy = new(
        () => SpectralCaseCatalog.TensorProductSpectrum96());

    /// <summary>
    /// Spread each eigenspace total occupancy share (m_i/N of the total) over its m_i cells with the
    /// given per-cell fractions. Uniform fractions give the canonical counting measure (uniform rho);
    /// any other fractions are a redistribution INSIDE degenerate multiplets only.
    /// </summary>
    public static double[] Spread(int[] mult, double total, Func<int, double[]>? fractions = null)
    {
        int n = mult.Sum();
        var rho = new double[n];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
        {
            int m = mult[i];
            double share = total * m / n;
            double[] fr = fractions?.Invoke(m) ?? Enumerable.Repeat(1.0 / m, m).ToArray();
            for (int j = 0; j < m; j++) rho[k++] = share * fr[j];
        }
        return rho;
    }

    /// <summary>The witness tilt: 80% of each multiplet share on its first cell, the rest spread equally.</summary>
    public static double[] TiltFractions(int m)
    {
        if (m == 1) return new[] { 1.0 };
        var fr = new double[m];
        fr[0] = 0.8;
        for (int j = 1; j < m; j++) fr[j] = 0.2 / (m - 1);
        return fr;
    }

    /// <summary>Per-multiplet occupancy totals — invariant under any WITHIN-multiplet redistribution.</summary>
    public static double[] BlockSums(int[] mult, double[] rho)
    {
        var blocks = new double[mult.Length];
        int k = 0;
        for (int i = 0; i < mult.Length; i++)
        {
            double s = 0.0;
            for (int j = 0; j < mult[i]; j++) s += rho[k++];
            blocks[i] = s;
        }
        return blocks;
    }

    /// <summary>Survivor compaction at FIXED total: keep the k largest cells, replace the rest by their mean.</summary>
    public static double[] Compaction(double[] rho, int k)
    {
        var keep = Enumerable.Range(0, rho.Length).OrderByDescending(i => rho[i]).Take(k).ToHashSet();
        double mean = Enumerable.Range(0, rho.Length).Where(i => !keep.Contains(i)).Sum(i => rho[i])
                      / (rho.Length - k);
        return Enumerable.Range(0, rho.Length).Select(i => keep.Contains(i) ? rho[i] : mean).ToArray();
    }
}
