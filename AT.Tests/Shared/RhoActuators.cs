using AT.Core.ResearchXH;

namespace AT.Tests.Shared;

/// <summary>
/// Shared machinery for the ResearchY-G actuator audits (G_011 Rho Actuator, G_011b Labor Rho):
/// the spectral coordinates of the counting measure (orthonormal DCT-II), the global information
/// functionals, the general hold-drive of the relaxation operator (G_008: <c>s = (I − W)rho*</c>),
/// and the lattice families a laboratory realisation can supply with their admissible (CFL)
/// damping bounds. Extracted so the two actuator audits do not duplicate it.
/// </summary>
public static class RhoActuators
{
    // ── Spectral coordinates of the counting measure ─────────────────────────────

    /// <summary>Orthonormal DCT-II matrix: C[k,i] = c_k·cos(pi k (i+1/2)/n), c_0 = 1/sqrt(n), c_k = sqrt(2/n).</summary>
    public static double[,] DctMatrix(int n)
    {
        var c = new double[n, n];
        for (int k = 0; k < n; k++)
        {
            double norm = k == 0 ? 1.0 / Math.Sqrt(n) : Math.Sqrt(2.0 / n);
            for (int i = 0; i < n; i++) c[k, i] = norm * Math.Cos(Math.PI * k * (i + 0.5) / n);
        }
        return c;
    }

    /// <summary>Unnormalised DCT-II in the AT convention (basis norm sum cos^2 = n/2 for k >= 1).</summary>
    public static double[] Dct(double[] x)
    {
        int n = x.Length;
        var w = new double[n];
        for (int k = 0; k < n; k++)
        {
            double s = 0.0;
            for (int i = 0; i < n; i++) s += x[i] * Math.Cos(Math.PI * k * (i + 0.5) / n);
            w[k] = s;
        }
        return w;
    }

    /// <summary>Inverse of <see cref="Dct"/> (the AC modes carry the n/2 basis norm).</summary>
    public static double[] Idct(double[] w, int n)
    {
        var x = new double[n];
        double dc = w[0] / n;
        for (int i = 0; i < n; i++) x[i] = dc;
        for (int k = 1; k < n; k++)
        {
            double amp = w[k] / (n / 2.0);
            for (int i = 0; i < n; i++) x[i] += amp * Math.Cos(Math.PI * k * (i + 0.5) / n);
        }
        return x;
    }

    /// <summary>High-k (k &gt;= kc) share of the AC spectral energy of a profile.</summary>
    public static double HighKShare(double[] x, int kc)
    {
        var w = Dct(x);
        double hi = 0.0, tot = 0.0;
        for (int k = 1; k < x.Length; k++) { double e = w[k] * w[k]; tot += e; if (k >= kc) hi += e; }
        return tot > 0.0 ? hi / tot : 0.0;
    }

    /// <summary>KL(rho || uniform) = sum rho_i ln(n rho_i) — the AT information density I_occ (QG008 chain).</summary>
    public static double Kl(double[] rho)
    {
        int n = rho.Length;
        double s = 0.0;
        foreach (double r in rho) if (r > 0.0) s += r * Math.Log(n * r);
        return s;
    }

    /// <summary>Shannon entropy of an occupancy vector normalised to fractions.</summary>
    public static double EntropyOf(double[] rho) => RhoDynamics.EntropyOf(rho);

    // ── Actuator algebra: the general hold-drive of the relaxation operator ──────

    /// <summary>
    /// The drive that holds an arbitrary target profile against the relaxation: s = (I − W)rho* per
    /// step (G_008's law, generalised to any rho*). Its sum is exactly zero, i.e. it is count-neutral.
    /// </summary>
    public static double[] HoldDrive(double[] target, double damp = 0.2)
    {
        var stepped = RhoDynamics.DiffuseStep(target, damp);
        var s = new double[target.Length];
        for (int i = 0; i < target.Length; i++) s[i] = target[i] - stepped[i];
        return s;
    }

    /// <summary>Iterate rho &lt;- W rho + s for a given number of steps.</summary>
    public static double[] Settle(double[] start, double[] drive, int steps, double damp = 0.2)
    {
        var r = (double[])start.Clone();
        for (int m = 0; m < steps; m++) r = Add(RhoDynamics.DiffuseStep(r, damp), drive);
        return r;
    }

    private static double[] Add(double[] a, double[] b)
    {
        var s = new double[a.Length];
        for (int i = 0; i < a.Length; i++) s[i] = a[i] + b[i];
        return s;
    }

    // ── The canonical (Neumann chain) mode family ────────────────────────────────

    /// <summary>The k-th Neumann mode of the occupancy index: cos(pi k (i+1/2)/N).</summary>
    public static double[] NeumannMode(int k, int n) => Enumerable.Range(0, n)
        .Select(i => Math.Cos(Math.PI * k * (i + 0.5) / n)).ToArray();

    /// <summary>Exact eigenvalue of DiffuseStep on the k-th Neumann mode: mu_k = 1 - 2d(1 - cos(pi k/n)).</summary>
    public static double NeumannMu(int k, int n, double damp = 0.2)
        => 1.0 - 2.0 * damp * (1.0 - Math.Cos(Math.PI * k / n));

    // ── Laboratory operator families (analytic Laplacian spectra) ────────────────

    /// <summary>Neumann chain (the DiffuseStep occupancy index): lambda_k = 2(1 - cos(pi k/n)).</summary>
    public static double[] ChainSpectrum(int n) => Enumerable.Range(0, n)
        .Select(k => 2.0 * (1.0 - Math.Cos(Math.PI * k / n))).ToArray();

    /// <summary>Nearest-neighbour ring (periodic chain): lambda_k = 2(1 - cos(2 pi k/n)).</summary>
    public static double[] RingSpectrum(int n) => Enumerable.Range(0, n)
        .Select(k => 2.0 * (1.0 - Math.Cos(2.0 * Math.PI * k / n))).ToArray();

    /// <summary>D96 circulant C_n(±1..±k): lambda_r = 2·sum_{s=1..k}(1 - cos(2 pi s r/n)).</summary>
    public static double[] CirculantSpectrum(int n = 96, int k = 6) => Enumerable.Range(0, n)
        .Select(r => 2.0 * Enumerable.Range(1, k).Sum(s => 1.0 - Math.Cos(2.0 * Math.PI * s * r / n))).ToArray();

    /// <summary>Star (hub) graph: {0} ∪ {1}^(n-2) ∪ {n}.</summary>
    public static double[] StarSpectrum(int n)
    {
        var l = new double[n];
        for (int i = 1; i < n - 1; i++) l[i] = 1.0;
        l[n - 1] = n;
        return l;
    }

    /// <summary>One explicit (ring) relaxation step: x + d(x_{i-1} - 2x_i + x_{i+1}) with periodic wrap.</summary>
    public static double[] RingStep(double[] x, double damp)
    {
        int n = x.Length;
        var b = new double[n];
        for (int i = 0; i < n; i++) b[i] = x[i] + damp * (x[(i - 1 + n) % n] - 2.0 * x[i] + x[(i + 1) % n]);
        return b;
    }

    /// <summary>Largest damping for which 1 - d·L keeps the spectrum inside [-1, 1] (the CFL bound).</summary>
    public static double AdmissibleDamping(double[] laplacianSpectrum) => 2.0 / laplacianSpectrum.Max();

    /// <summary>Largest / smallest NON-ZERO rate of 1 - d·L (the selectivity of the suppression).</summary>
    public static double RateSelectivity(double[] laplacianSpectrum, double damp)
    {
        double lo = laplacianSpectrum.Where(l => l > 1e-9).Min() * damp;
        return damp * laplacianSpectrum.Max() / lo;
    }
}
