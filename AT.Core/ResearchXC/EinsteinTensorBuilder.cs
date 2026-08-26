using System;

namespace AT.Core.ResearchXC;

/// <summary>
/// Standard Riemannian-geometry chain in local coordinates (dimension n):
/// metric → Christoffel Γ^λ_{μν} → Riemann R^ρ_{σμν} → Ricci R_{μν} → Einstein G_{μν}.
/// Pure differential geometry — no new physics. This is the minimum code required to
/// integrate the tested chain into AT analyzers, which currently describe the chain
/// (see QuantumGravityEmergenceAnalyzer.GeoStep) but never compute it.
/// </summary>
public static class EinsteinTensorBuilder
{
    /// <summary>Metric field: x (length n) → covariant metric g (n×n, symmetric).</summary>
    public delegate double[,] MetricField(double[] x);

    // ── Christoffel symbols Γ^λ_{μν} ─────────────────────────────────────────

    public static double[,,] Christoffel(MetricField g, double[] x, double h)
    {
        int n = x.Length;
        var gi = InverseMetric(g(x));
        var p = new double[n][,];
        for (int mu = 0; mu < n; mu++) p[mu] = PartialMetric(g, x, mu, h);

        var G = new double[n, n, n];
        for (int lam = 0; lam < n; lam++)
        for (int mu = 0; mu < n; mu++)
        for (int nu = 0; nu < n; nu++)
        {
            double s = 0;
            for (int sig = 0; sig < n; sig++)
                s += gi[lam, sig] * (p[mu][sig, nu] + p[nu][sig, mu] - p[sig][mu, nu]);
            G[lam, mu, nu] = 0.5 * s;
        }
        return G;
    }

    // ── Riemann R^ρ_{σμν} ───────────────────────────────────────────────────

    public static double[,,,] Riemann(MetricField g, double[] x, double h)
    {
        int n = x.Length;
        var G = Christoffel(g, x, h);
        var dG = new double[n][,,];
        for (int mu = 0; mu < n; mu++) dG[mu] = PartialChristoffel(g, x, mu, h);

        var R = new double[n, n, n, n];
        for (int rho = 0; rho < n; rho++)
        for (int sig = 0; sig < n; sig++)
        for (int mu = 0; mu < n; mu++)
        for (int nu = 0; nu < n; nu++)
        {
            double val = dG[mu][rho, nu, sig] - dG[nu][rho, mu, sig];
            for (int lam = 0; lam < n; lam++)
            {
                val += G[rho, mu, lam] * G[lam, nu, sig];
                val -= G[rho, nu, lam] * G[lam, mu, sig];
            }
            R[rho, sig, mu, nu] = val;
        }
        return R;
    }

    // ── Ricci R_{μν} and scalar R ────────────────────────────────────────────

    public static double[,] Ricci(MetricField g, double[] x, double h)
    {
        int n = x.Length;
        var R = Riemann(g, x, h);
        var ric = new double[n, n];
        for (int sig = 0; sig < n; sig++)
        for (int nu = 0; nu < n; nu++)
        {
            double s = 0;
            for (int rho = 0; rho < n; rho++) s += R[rho, sig, rho, nu];
            ric[sig, nu] = s;
        }
        return ric;
    }

    public static double RicciScalar(MetricField g, double[] x, double h)
    {
        int n = x.Length;
        var ric = Ricci(g, x, h);
        var gi = InverseMetric(g(x));
        double s = 0;
        for (int a = 0; a < n; a++)
        for (int b = 0; b < n; b++)
            s += gi[a, b] * ric[a, b];
        return s;
    }

    // ── Einstein G_{μν} = R_{μν} − ½R·g_{μν} ────────────────────────────────

    public static double[,] Einstein(MetricField g, double[] x, double h)
    {
        int n = x.Length;
        var ric = Ricci(g, x, h);
        double R = RicciScalar(g, x, h);
        var gm = g(x);
        var G = new double[n, n];
        for (int a = 0; a < n; a++)
        for (int b = 0; b < n; b++)
            G[a, b] = ric[a, b] - 0.5 * R * gm[a, b];
        return G;
    }

    // ── Internal helpers ──────────────────────────────────────────────────────

    private static double[,] PartialMetric(MetricField g, double[] x, int mu, double h)
    {
        int n = x.Length;
        var xp = (double[])x.Clone();
        var xm = (double[])x.Clone();
        xp[mu] += h;
        xm[mu] -= h;
        var gp = g(xp);
        var gm = g(xm);
        var d = new double[n, n];
        for (int a = 0; a < n; a++)
        for (int b = 0; b < n; b++)
            d[a, b] = (gp[a, b] - gm[a, b]) / (2.0 * h);
        return d;
    }

    private static double[,,] PartialChristoffel(MetricField g, double[] x, int mu, double h)
    {
        int n = x.Length;
        var xp = (double[])x.Clone();
        var xm = (double[])x.Clone();
        xp[mu] += h;
        xm[mu] -= h;
        var Gp = Christoffel(g, xp, h);
        var Gm = Christoffel(g, xm, h);
        var d = new double[n, n, n];
        for (int a = 0; a < n; a++)
        for (int b = 0; b < n; b++)
        for (int c = 0; c < n; c++)
            d[a, b, c] = (Gp[a, b, c] - Gm[a, b, c]) / (2.0 * h);
        return d;
    }

    private static double[,] InverseMetric(double[,] g)
    {
        int n = g.GetLength(0);
        var a = new double[n, 2 * n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
        {
            a[i, j] = g[i, j];
            a[i, j + n] = i == j ? 1.0 : 0.0;
        }

        for (int col = 0; col < n; col++)
        {
            int piv = col;
            for (int r = col + 1; r < n; r++)
                if (Math.Abs(a[r, col]) > Math.Abs(a[piv, col])) piv = r;
            if (Math.Abs(a[piv, col]) < 1e-15) throw new InvalidOperationException("Singular metric.");
            if (piv != col)
                for (int j = 0; j < 2 * n; j++)
                    (a[piv, j], a[col, j]) = (a[col, j], a[piv, j]);

            double pv = a[col, col];
            for (int j = 0; j < 2 * n; j++) a[col, j] /= pv;
            for (int r = 0; r < n; r++)
            {
                if (r == col) continue;
                double f = a[r, col];
                if (Math.Abs(f) < 1e-15) continue;
                for (int j = 0; j < 2 * n; j++) a[r, j] -= f * a[col, j];
            }
        }

        var inv = new double[n, n];
        for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            inv[i, j] = a[i, j + n];
        return inv;
    }
}
