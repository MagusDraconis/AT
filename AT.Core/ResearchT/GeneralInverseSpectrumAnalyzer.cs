using System.Globalization;
using MathNet.Numerics.LinearAlgebra;

namespace AT.Core.ResearchT;

/// <summary>
/// A non-isomorphic, isospectral pair: two graphs with identical Laplacian spectrum
/// but different degree sequences (hence provably non-isomorphic).
/// </summary>
public sealed record IsospectralPair(
    double[] Spectrum,
    double[,] GraphA,
    double[,] GraphB,
    int[] DegreesA,
    int[] DegreesB,
    int Edges);

/// <summary>
/// General inverse spectral graph audit. Computes Laplacian spectra of concrete graph
/// families, reconstructs a graph from a full eigendecomposition (spectrum + eigenbasis),
/// measures reconstruction quality, and searches for isospectral (spectrum-sharing)
/// non-isomorphic graphs — the standard obstacle to inverting the spectrum of a general
/// (non-circulant) graph. Deterministic throughout (no randomness except a seeded RNG).
/// </summary>
public static class GeneralInverseSpectrumAnalyzer
{
    // ── Graph generators (adjacency as double[,], symmetric, 0/1) ───────────

    public static double[,] PathGraph(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i + 1 < n; i++) { a[i, i + 1] = 1.0; a[i + 1, i] = 1.0; }
        return a;
    }

    public static double[,] CycleGraph(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            int j = (i + 1) % n;
            a[i, j] = 1.0;
            a[j, i] = 1.0;
        }
        return a;
    }

    public static double[,] GridGraph(int rows, int cols)
    {
        int n = rows * cols;
        int Id(int r, int c) => r * cols + c;
        var a = new double[n, n];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                int v = Id(r, c);
                if (r + 1 < rows) { int u = Id(r + 1, c); a[v, u] = a[u, v] = 1.0; }
                if (c + 1 < cols) { int u = Id(r, c + 1); a[v, u] = a[u, v] = 1.0; }
            }
        return a;
    }

    public static double[,] CompleteGraph(int n)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                a[i, j] = 1.0;
                a[j, i] = 1.0;
            }
        return a;
    }

    /// <summary>Deterministic sparse random graph: each edge present i.i.d. with probability `density`.</summary>
    public static double[,] RandomSparseGraph(int n, double density, int seed)
    {
        var rng = new Random(seed);
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (rng.NextDouble() < density)
                {
                    a[i, j] = 1.0;
                    a[j, i] = 1.0;
                }
        return a;
    }

    /// <summary>The D96-derived graph: circulant C_n(±1..±k). Default n=96, k=6 (canonical D96).</summary>
    public static double[,] D96DerivedGraph(int n = 96, int k = 6)
    {
        var a = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int d = 1; d <= k; d++)
            {
                int j1 = (i + d) % n;
                int j2 = (i - d + n) % n;
                a[i, j1] = 1.0;
                a[i, j2] = 1.0;
            }
        return a;
    }

    /// <summary>Labeled graph on n vertices from an upper-triangle bitmask (bit order: (0,1),(0,2),...,(n-2,n-1)).</summary>
    public static double[,] GraphFromMask(int n, int mask)
    {
        var a = new double[n, n];
        int b = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                if ((mask & (1 << b)) != 0) { a[i, j] = 1.0; a[j, i] = 1.0; }
                b++;
            }
        return a;
    }

    // ── Graph quantities ────────────────────────────────────────────────────

    public static double[,] Laplacian(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        var l = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double deg = 0.0;
            for (int j = 0; j < n; j++)
                if (i != j) deg += adjacency[i, j];
            l[i, i] = deg;
            for (int j = 0; j < n; j++)
                if (i != j) l[i, j] = -adjacency[i, j];
        }
        return l;
    }

    public static double[] Spectrum(double[,] laplacian)
    {
        var mat = Matrix<double>.Build.DenseOfArray(laplacian);
        var evd = mat.Evd(Symmetricity.Symmetric);
        double[] evals = evd.EigenValues.Select(c => c.Real).ToArray();
        Array.Sort(evals);
        return evals;
    }

    public static int[] DegreeSequence(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        var deg = new int[n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (adjacency[i, j] != 0.0) deg[i]++;
        Array.Sort(deg);
        return deg;
    }

    public static int EdgeCount(double[,] adjacency)
    {
        int n = adjacency.GetLength(0), c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
                if (adjacency[i, j] != 0.0) c++;
        return c;
    }

    /// <summary>Sparsity = 1 − edges/maxEdges ∈ [0,1] (1 = empty, 0 = complete).</summary>
    public static double Sparsity(double[,] adjacency)
    {
        int n = adjacency.GetLength(0);
        int maxEdges = n * (n - 1) / 2;
        return 1.0 - (double)EdgeCount(adjacency) / maxEdges;
    }

    /// <summary>Fraction of off-diagonal Laplacian entries that are non-negative (non-edges recovered as 0).</summary>
    public static double PositiveWeightRatio(double[,] laplacian)
    {
        int n = laplacian.GetLength(0);
        int total = 0, nonneg = 0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (i != j)
                {
                    total++;
                    if (laplacian[i, j] >= -1e-9) nonneg++;
                }
        return total == 0 ? 1.0 : (double)nonneg / total;
    }

    // ── Reconstruction ──────────────────────────────────────────────────────

    /// <summary>
    /// Reconstruct the Laplacian from its full eigendecomposition L = V Λ Vᵀ (spectrum
    /// AND eigenbasis). This is exact: it shows that spectrum+eigenvectors loses nothing,
    /// whereas spectrum ALONE does not determine a general graph (see isospectral search).
    /// </summary>
    public static double[,] ReconstructFromEigendecomposition(double[,] laplacian)
    {
        int n = laplacian.GetLength(0);
        var mat = Matrix<double>.Build.DenseOfArray(laplacian);
        var evd = mat.Evd(Symmetricity.Symmetric);
        var v = evd.EigenVectors;      // columns are eigenvectors
        var lam = evd.EigenValues;     // real eigenvalues
        var result = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                double s = 0.0;
                for (int k = 0; k < n; k++)
                    s += v[i, k] * lam[k].Real * v[j, k]; // V Λ Vᵀ
                result[i, j] = s;
            }
        return result;
    }

    // ── Metrics ─────────────────────────────────────────────────────────────

    public static double SpectralError(double[] a, double[] b)
    {
        double s = 0.0;
        for (int i = 0; i < a.Length; i++)
        {
            double e = a[i] - b[i];
            s += e * e;
        }
        return Math.Sqrt(s / a.Length);
    }

    /// <summary>Jaccard similarity of the edge sets of two adjacency matrices.</summary>
    public static double GraphSimilarity(double[,] a, double[,] b)
    {
        int n = a.GetLength(0);
        int inter = 0, union = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                bool ea = a[i, j] > 0.5, eb = b[i, j] > 0.5;
                if (ea || eb) union++;
                if (ea && eb) inter++;
            }
        return union == 0 ? 1.0 : (double)inter / union;
    }

    private static double MaxAbsDiff(double[] a, double[] b)
    {
        double m = 0.0;
        for (int i = 0; i < a.Length; i++) m = Math.Max(m, Math.Abs(a[i] - b[i]));
        return m;
    }

    // ── Isospectral search ──────────────────────────────────────────────────

    /// <summary>
    /// Enumerate ALL labeled graphs on n vertices, group them by Laplacian spectrum, and
    /// return the non-isomorphic isospectral pairs (identical spectrum, different degree
    /// sequence). Deterministic and exhaustive.
    /// </summary>
    public static List<IsospectralPair> FindIsospectralPairs(int n)
    {
        int maxEdges = n * (n - 1) / 2;
        int maxMask = 1 << maxEdges;
        var groups = new Dictionary<string, List<(double[,] Adj, int[] Deg, int Edges)>>();

        for (int mask = 0; mask < maxMask; mask++)
        {
            var adj = GraphFromMask(n, mask);
            var spec = Spectrum(Laplacian(adj));
            string key = string.Join(",", spec.Select(x => x.ToString("F6", CultureInfo.InvariantCulture)));
            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<(double[,], int[], int)>();
                groups[key] = list;
            }
            list.Add((adj, DegreeSequence(adj), EdgeCount(adj)));
        }

        var result = new List<IsospectralPair>();
        var seen = new HashSet<string>();
        foreach (var g in groups.Values)
        {
            if (g.Count < 2) continue;
            // Distinct degree sequences within this spectrum class.
            var distinct = g
                .GroupBy(x => string.Join(",", x.Deg))
                .Select(grp => grp.First())
                .ToList();
            if (distinct.Count < 2) continue;

            var s0 = Spectrum(Laplacian(distinct[0].Adj));
            foreach (var other in distinct.Skip(1))
            {
                var s1 = Spectrum(Laplacian(other.Adj));
                if (MaxAbsDiff(s0, s1) < 1e-8) // exact isospectrality (to numerical precision)
                {
                    // Canonical key: spectrum + unordered degree-sequence pair, to dedupe.
                    string sa = string.Join(",", distinct[0].Deg);
                    string sb = string.Join(",", other.Deg);
                    string degPair = string.CompareOrdinal(sa, sb) <= 0 ? sa + "|" + sb : sb + "|" + sa;
                    string key = string.Join(",", s0.Select(x => x.ToString("F4", CultureInfo.InvariantCulture))) + "|" + degPair;
                    if (seen.Add(key))
                        result.Add(new IsospectralPair(s0, distinct[0].Adj, other.Adj,
                            distinct[0].Deg, other.Deg, distinct[0].Edges));
                }
            }
        }
        return result;
    }
}
