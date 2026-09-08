using MathNet.Numerics.LinearAlgebra;

namespace AT.Core.ResearchT;

/// <summary>Per-model attractor-dominance summary.</summary>
public sealed record AttractorDominance(
    string Model,
    int N,
    int AttractorCount,          // A = number of distinct eigenspaces (attractors)
    double LargestBasin,         // D = largest basin fraction
    double SecondBasin,          // second-largest basin fraction
    double DominanceRatio,       // R = largest / second-largest (∞ if A=1)
    double BasinEntropyNats,     // E = Shannon entropy of the basin distribution
    double NormalizedEntropy,    // E / ln(N)
    double DominanceIndex,       // DI = D / (1/A) = D·A  (1 → uniform, ≫1 → dominated)
    double SpectralGap,          // λ₂ — convergence-speed proxy
    double MeanAttractorLifetime);

/// <summary>
/// Attractor dominance audit. Tests the AT principle that a small number of stable
/// attractors (eigenmodes of the graph Laplacian) capture most accessible trajectories.
///
/// Model: under damped mode-locking a trajectory locks onto the eigenmode it most
/// resembles; degenerate (identical-eigenvalue) modes are indistinguishable, so the
/// attractors are the DISTINCT eigenspaces of the Laplacian, and the basin of an
/// eigenspace is its multiplicity fraction (the exact measure over uniform initial
/// states). Deterministic throughout.
/// </summary>
public static class AttractorDominanceAnalyzer
{
    // ── Graph models ────────────────────────────────────────────────────────

    /// <summary>D96 ring: circulant C_n(±1..±k). Default n=96, k=6.</summary>
    public static double[,] D96Ring(int n = 96, int k = 6)
        => GeneralInverseSpectrumAnalyzer.D96DerivedGraph(n, k);

    /// <summary>3D D96 analogue: a×b×c periodic torus (nearest-neighbor in 3 dimensions).</summary>
    public static double[,] D963D(int a, int b, int c)
    {
        int n = a * b * c;
        int Id(int x, int y, int z) => (x * b + y) * c + z;
        var adj = new double[n, n];
        var dirs = new[] { (1, 0, 0), (0, 1, 0), (0, 0, 1) };
        for (int x = 0; x < a; x++)
            for (int y = 0; y < b; y++)
                for (int z = 0; z < c; z++)
                {
                    int v = Id(x, y, z);
                    foreach (var (dx, dy, dz) in dirs)
                    {
                        int u = Id((x + dx) % a, (y + dy) % b, (z + dz) % c);
                        adj[v, u] = 1.0;
                        adj[u, v] = 1.0;
                    }
                }
        return adj;
    }

    // ── Eigenspaces ─────────────────────────────────────────────────────────

    /// <summary>Distinct eigenvalues and their multiplicities (attractors and basin weights).</summary>
    public static (double[] Distinct, int[] Multiplicities) Eigenspaces(double[,] adjacency)
    {
        var spec = GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(adjacency));
        return GroupSpectrum(spec);
    }

    public static (double[] Distinct, int[] Multiplicities) GroupSpectrum(double[] sortedSpectrum)
    {
        var distinct = new List<double>();
        var mult = new List<int>();
        foreach (double e in sortedSpectrum)
        {
            if (distinct.Count == 0 || Math.Abs(e - distinct[^1]) > 1e-6)
            {
                distinct.Add(e);
                mult.Add(1);
            }
            else mult[^1]++;
        }
        return (distinct.ToArray(), mult.ToArray());
    }

    /// <summary>
    /// Empirical basins: sample random states, assign each to its dominant eigenspace
    /// (the eigenspace capturing the largest projection energy). Verifies the analytic
    /// multiplicity-based basins (≈ multiplicity/N).
    /// </summary>
    public static double[] SampleBasins(double[,] adjacency, int samples, int seed)
    {
        int n = adjacency.GetLength(0);
        var lap = GeneralInverseSpectrumAnalyzer.Laplacian(adjacency);
        var mat = Matrix<double>.Build.DenseOfArray(lap);
        var evd = mat.Evd(Symmetricity.Symmetric);
        var evals = evd.EigenValues.Select(c => c.Real).ToArray();
        var evecs = evd.EigenVectors; // columns are eigenvectors

        var order = Enumerable.Range(0, n).OrderBy(i => evals[i]).ToArray();
        var groups = new List<List<int>>();
        foreach (int i in order)
        {
            if (groups.Count == 0 || Math.Abs(evals[i] - evals[order[groups[^1][0]]]) > 1e-6)
                groups.Add(new List<int> { i });
            else
                groups[^1].Add(i);
        }

        var rng = new Random(seed);
        var basin = new double[groups.Count];
        for (int s = 0; s < samples; s++)
        {
            var x = new double[n];
            for (int i = 0; i < n; i++) x[i] = NextGaussian(rng);
            double norm = Math.Sqrt(x.Sum(v => v * v));
            for (int i = 0; i < n; i++) x[i] /= norm;

            int best = 0;
            double bestE = -1.0;
            for (int g = 0; g < groups.Count; g++)
            {
                double e = 0.0;
                foreach (int k in groups[g])
                {
                    double dot = 0.0;
                    for (int i = 0; i < n; i++) dot += evecs[i, k] * x[i];
                    e += dot * dot;
                }
                if (e > bestE) { bestE = e; best = g; }
            }
            basin[best]++;
        }
        for (int g = 0; g < basin.Length; g++) basin[g] /= samples;
        return basin;
    }

    private static double NextGaussian(Random rng)
    {
        double u1 = 1.0 - rng.NextDouble();
        double u2 = rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
    }

    // ── Metrics ─────────────────────────────────────────────────────────────

    public static AttractorDominance AnalyzeGraph(string model, double[,] adjacency)
    {
        var (distinct, mult) = Eigenspaces(adjacency);
        return BuildMetrics(model, adjacency.GetLength(0), distinct, mult);
    }

    public static AttractorDominance AnalyzeSpectrum(string model, double[] spectrum)
    {
        var sorted = spectrum.OrderBy(x => x).ToArray();
        var (distinct, mult) = GroupSpectrum(sorted);
        return BuildMetrics(model, spectrum.Length, distinct, mult);
    }

    private static AttractorDominance BuildMetrics(string model, int n, double[] distinct, int[] mult)
    {
        int A = distinct.Length;
        var p = mult.Select(m => (double)m / n).OrderByDescending(x => x).ToArray();
        double D = p.Length > 0 ? p[0] : 0.0;
        double second = p.Length > 1 ? p[1] : 0.0;
        double R = p.Length > 1 && second > 0 ? p[0] / second : double.PositiveInfinity;
        double E = p.Sum(x => x > 0 ? -x * Math.Log(x) : 0.0);
        double normE = E / Math.Log(n);
        double DI = D * A;

        double gap = double.NaN;
        foreach (double e in distinct)
            if (e > 1e-9) { gap = e; break; }

        double lifetime = 0.0, wsum = 0.0;
        for (int i = 0; i < distinct.Length; i++)
            if (distinct[i] > 1e-9)
            {
                lifetime += mult[i] / distinct[i];
                wsum += mult[i];
            }
        lifetime = wsum > 0 ? lifetime / wsum : double.NaN;

        return new AttractorDominance(model, n, A, D, second, R, E, normE, DI, gap, lifetime);
    }

    /// <summary>Classification from the success criteria: D&gt;0.5 dominated, else R&gt;2 weak.</summary>
    public static string Classify(double largestBasin, double dominanceRatio)
        => largestBasin > 0.5 ? "ATTRACTOR DOMINATED"
        : dominanceRatio > 2.0 ? "WEAKLY DOMINATED"
        : "NOT DOMINATED";
}
