namespace AT.Tests.Shared;

/// <summary>
/// Spectral-blueprint utilities shared by the ResearchY-T (Spectral Blueprint) program.
///
/// A circulant (ring) graph's Laplacian is a circulant matrix: its eigenvalues λ_k are
/// the discrete Fourier transform (DFT) of the coupling row c_d, and the coupling row is
/// the inverse DFT (IDFT) of the eigenvalues:
///   forward   λ_k = Σ_{d=1..n−1} w_d · (1 − cos 2πdk/n)      (weights → spectrum)
///   inverse   w_d = −(1/n) Σ_{k=0..n−1} λ_k · cos 2πdk/n     (spectrum → weights)
/// Edge weights w_d = −c_d ≥ 0 are physical (attractive) coupling. The inverse spectral
/// design problem is therefore closed-form for ring topologies. Deterministic only.
/// </summary>
public static class SpectralBlueprint
{
    public const double DefaultTolerance = 1e-6;

    /// <summary>Build a symmetric spectrum λ_0 = 0, λ_k = λ_{n−k} from a profile of m = min(k, n−k).</summary>
    public static double[] BuildSymmetric(int n, Func<int, double> valueAtM)
    {
        var lam = new double[n];
        lam[0] = 0.0;
        for (int k = 1; k < n; k++)
        {
            int m = Math.Min(k, n - k);
            lam[k] = valueAtM(m);
        }
        return lam;
    }

    /// <summary>Circulant C_n(±1..±k) spectrum: λ = 2Σ_{d=1..k}(1−cos 2πdj/n). D96 = (n=96, k=6).</summary>
    public static double[] CirculantSpectrum(int n, int k)
    {
        var lam = new double[n];
        for (int j = 0; j < n; j++)
        {
            double s = 0.0;
            for (int d = 1; d <= k; d++)
                s += 1.0 - Math.Cos(2.0 * Math.PI * d * j / n);
            lam[j] = 2.0 * s;
        }
        return lam;
    }

    /// <summary>Forward map: coupling weights w[d] (d=1..n−1) → Laplacian spectrum λ[k].</summary>
    public static double[] ForwardSpectrum(double[] w, int n)
    {
        var lam = new double[n];
        for (int k = 0; k < n; k++)
        {
            double s = 0.0;
            for (int d = 1; d < n; d++)
                s += w[d] * (1.0 - Math.Cos(2.0 * Math.PI * d * k / n));
            lam[k] = s;
        }
        return lam;
    }

    /// <summary>Inverse map: target spectrum λ[k] → coupling weights w[d] (d=1..n−1).</summary>
    public static double[] ReconstructWeights(double[] lambda, int n)
    {
        var w = new double[n]; // index 0 unused (degree); 1..n−1 are edge weights
        for (int d = 1; d < n; d++)
        {
            double s = 0.0;
            for (int k = 0; k < n; k++)
                s += lambda[k] * Math.Cos(2.0 * Math.PI * d * k / n);
            w[d] = -s / n;
        }
        return w;
    }

    public static double MaxAbs(double[] a)
    {
        double m = 0.0;
        foreach (double x in a) m = Math.Max(m, Math.Abs(x));
        return m;
    }

    /// <summary>Absolute RMS error between two same-length arrays.</summary>
    public static double Rms(double[] a, double[] b)
    {
        double s = 0.0;
        for (int i = 0; i < a.Length; i++)
        {
            double e = a[i] - b[i];
            s += e * e;
        }
        return Math.Sqrt(s / a.Length);
    }

    /// <summary>Distinct unordered edges (distances 1..n/2) with |w_d| above threshold.</summary>
    public static int EdgeCount(double[] w, int n, double eps = DefaultTolerance)
    {
        int c = 0;
        for (int d = 1; d <= n / 2; d++)
            if (Math.Abs(w[d]) > eps) c++;
        return c;
    }

    /// <summary>Total coupling mass Σ_d |w_d| (a graph-complexity proxy).</summary>
    public static double CouplingMass(double[] w)
    {
        double s = 0.0;
        for (int d = 1; d < w.Length; d++) s += Math.Abs(w[d]);
        return s;
    }

    /// <summary>Total attractive (positive) coupling mass Σ_d max(w_d, 0).</summary>
    public static double PositiveMass(double[] w)
    {
        double s = 0.0;
        for (int d = 1; d < w.Length; d++) s += Math.Max(w[d], 0.0);
        return s;
    }

    /// <summary>Negative (repulsive) coupling statistics: count, most-negative weight, total mass.</summary>
    public static (int Count, double Min, double Mass) NegativeWeights(double[] w, double eps = DefaultTolerance)
    {
        int c = 0;
        double mn = 0.0, mass = 0.0;
        for (int d = 1; d < w.Length; d++)
            if (w[d] < -eps)
            {
                c++;
                mn = Math.Min(mn, w[d]);
                mass += -w[d];
            }
        return (c, mn, mass);
    }
}
