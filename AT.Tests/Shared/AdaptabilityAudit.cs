using AT.Core.ResearchT;

namespace AT.Tests.Shared;

/// <summary>
/// Per-case adaptability/robustness profile: the spectral baselines plus the response to a
/// deterministic perturbation ensemble (4 types × 5 doses × 3 seeds, connectivity-guarded).
/// </summary>
public sealed record AdaptabilityProfile(
    string Name,
    int A0,
    double E0,
    double Lambda2,
    double L,                    // latent fraction (N − A₀)/N
    int Edges,
    int DegenerateGroups,        // eigenspaces with multiplicity > 1
    int MaxMultiplicity,
    int Excluded,                // perturbations dropped by the connectivity guard
    double Capacity,             // mean ΔA/(N − A₀) — the adaptive capacity
    double MeanDeltaA,
    double MeanDeltaE,
    double MeanRecovery,         // mean 1 − ‖λ′−λ‖₂/‖λ‖₂
    double[] CapacityByDose,
    double[] CapacityByKind,
    double[] RecoveryByKind)
{
    /// <summary>Adaptability × robustness product, the candidate conserved quantity.</summary>
    public double Product => Capacity * MeanRecovery;

    /// <summary>Damage = 1 − recovery (a positive spectral displacement measure).</summary>
    public double Damage => 1.0 - MeanRecovery;
}

/// <summary>
/// Shared adaptability/robustness audit machinery for the ResearchY-D_048 (latent-degeneracy
/// adaptability) and D_049 (adaptability–robustness frontier) audits.
///
/// Six 96-node cases are perturbed by four deterministic perturbation types (edge deletion,
/// edge addition, rewiring, weight perturbation) at five dose fractions of the graph's own edge
/// count, with three fixed seeds each, and a connectivity guard. Everything is deterministic;
/// no unseeded randomness and no AT theoretical assumptions are used anywhere here.
/// </summary>
public static class AdaptabilityAudit
{
    public const int N = 96;
    public const double Tol = 1e-9;

    public static readonly double[] Doses = [0.005, 0.01, 0.02, 0.05, 0.10];
    public static readonly uint[] Seeds = [11u, 22u, 33u];
    public static readonly string[] Kinds = ["delete", "add", "rewire", "weight"];
    public static readonly string[] CaseNames = ["D96", "D96-3D", "random", "physical", "unphysical", "complete"];

    // ── Deterministic RNG (LCG; identical stream in any runtime) ─────────────

    private sealed class Lcg
    {
        private uint _x;
        public Lcg(uint seed) => _x = seed;
        public uint Next() => _x = unchecked(1664525u * _x + 1013904223u);
        public int Pick(int m) => m <= 0 ? 0 : (int)(Next() % (uint)m);
    }

    // ── Case constructions ──────────────────────────────────────────────────

    private static double[,] Symmetrize(double[,] a)
    {
        for (int i = 0; i < N; i++)
            for (int j = i + 1; j < N; j++)
            {
                double s = 0.5 * (a[i, j] + a[j, i]);
                a[i, j] = s;
                a[j, i] = s;
            }
        return a;
    }

    public static double[,] CirculantFromSpectrum(double[] spectrum)
    {
        double[] w = SpectralBlueprint.ReconstructWeights(spectrum, N);
        var a = new double[N, N];
        for (int i = 0; i < N; i++)
            for (int d = 1; d < N; d++)
                a[i, (i + d) % N] += w[d];
        return Symmetrize(a);
    }

    public static double[,] Adjacency(string name) => name switch
    {
        "D96" => GeneralInverseSpectrumAnalyzer.D96DerivedGraph(96, 6),
        "D96-3D" => AttractorDominanceAnalyzer.D963D(4, 4, 6),
        "random" => GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42),
        "physical" => CirculantFromSpectrum(SpectralBlueprint.BuildSymmetric(96, m => m)),
        "unphysical" => CirculantFromSpectrum(SpectralBlueprint.BuildSymmetric(96,
            m => m <= 16 ? 5.0 : m <= 32 ? 25.0 : 60.0)),
        "complete" => GeneralInverseSpectrumAnalyzer.CompleteGraph(96),
        _ => throw new ArgumentOutOfRangeException(nameof(name)),
    };

    // ── Perturbations ───────────────────────────────────────────────────────

    public static List<(int I, int J)> Edges(double[,] a)
    {
        var list = new List<(int, int)>();
        for (int i = 0; i < N; i++)
            for (int j = i + 1; j < N; j++)
                if (a[i, j] != 0.0) list.Add((i, j));
        return list;
    }

    private static List<(int I, int J)> NonEdges(double[,] a)
    {
        var list = new List<(int, int)>();
        for (int i = 0; i < N; i++)
            for (int j = i + 1; j < N; j++)
                if (a[i, j] == 0.0) list.Add((i, j));
        return list;
    }

    private static double[,] Clone(double[,] a)
    {
        var c = new double[N, N];
        Array.Copy(a, c, a.Length);
        return c;
    }

    public static double[,] Perturb(double[,] baseAdj, string kind, int k, uint seed, double eps)
    {
        var a = Clone(baseAdj);
        var rng = new Lcg(seed);
        switch (kind)
        {
            case "delete":
            {
                var e = Edges(a);
                for (int t = 0; t < k && e.Count > 0; t++)
                {
                    var (i, j) = e[rng.Pick(e.Count)];
                    e.Remove((i, j));
                    a[i, j] = a[j, i] = 0.0;
                }
                break;
            }
            case "add":
            {
                var e = NonEdges(a);
                for (int t = 0; t < k && e.Count > 0; t++)
                {
                    var (i, j) = e[rng.Pick(e.Count)];
                    e.Remove((i, j));
                    a[i, j] = a[j, i] = 1.0;
                }
                break;
            }
            case "rewire":
            {
                for (int t = 0; t < k; t++)
                {
                    var e = Edges(a);
                    if (e.Count == 0) break;
                    var (i, j) = e[rng.Pick(e.Count)];
                    a[i, j] = a[j, i] = 0.0;
                    var f = NonEdges(a);
                    if (f.Count == 0) break;
                    var (p, q) = f[rng.Pick(f.Count)];
                    a[p, q] = a[q, p] = 1.0;
                }
                break;
            }
            case "weight":
            {
                foreach (var (i, j) in Edges(a))
                {
                    double s = rng.Pick(2) == 1 ? 1.0 : -1.0;
                    a[i, j] = a[j, i] = baseAdj[i, j] * (1.0 + eps * s);
                }
                break;
            }
        }
        return a;
    }

    public static bool Connected(double[,] a)
    {
        var seen = new bool[N];
        var stack = new Stack<int>();
        seen[0] = true;
        stack.Push(0);
        int count = 1;
        while (stack.Count > 0)
        {
            int u = stack.Pop();
            for (int v = 0; v < N; v++)
                if (a[u, v] != 0.0 && !seen[v]) { seen[v] = true; count++; stack.Push(v); }
        }
        return count == N;
    }

    // ── Spectral measures ───────────────────────────────────────────────────

    public static double[] SpectrumOf(double[,] adj)
        => GeneralInverseSpectrumAnalyzer.Spectrum(GeneralInverseSpectrumAnalyzer.Laplacian(adj));

    /// <summary>Distinct-eigenspace (attractor) count and basin-distribution Shannon entropy (nats).</summary>
    public static (int A, double E, int[] Mult) Buckets(double[] spectrum)
    {
        var s = (double[])spectrum.Clone();
        Array.Sort(s);
        var mult = new List<int>();
        int i = 0;
        while (i < s.Length)
        {
            int j = i;
            while (j < s.Length && Math.Abs(s[j] - s[i]) <= Tol) j++;
            mult.Add(j - i);
            i = j;
        }
        double e = 0.0;
        foreach (int m in mult)
        {
            double p = (double)m / s.Length;
            e -= p * Math.Log(p);
        }
        return (mult.Count, e, mult.ToArray());
    }

    /// <summary>Relative RMS spectral shift ‖λ′−λ‖₂/‖λ‖₂ — the damage / inverse-recovery measure.</summary>
    public static double RelativeRms(double[] a, double[] b)
    {
        double num = 0.0, den = 0.0;
        for (int i = 0; i < a.Length; i++)
        {
            double d = a[i] - b[i];
            num += d * d;
            den += b[i] * b[i];
        }
        return den > 0 ? Math.Sqrt(num / a.Length) / Math.Sqrt(den / a.Length) : 0.0;
    }

    // ── Case study ──────────────────────────────────────────────────────────

    public static AdaptabilityProfile Study(string name)
    {
        var adj = Adjacency(name);
        var baseSpec = SpectrumOf(adj);
        var (a0, e0, mult) = Buckets(baseSpec);
        double lam2 = double.PositiveInfinity;
        foreach (double x in baseSpec) if (x > Tol && x < lam2) lam2 = x;
        double l = (double)(N - a0) / N;
        int head = N - a0;
        int nEdges = Edges(adj).Count;
        int degGroups = mult.Count(m => m > 1);
        int maxMult = mult.Max();

        double[] byDose = new double[Doses.Length];
        double[] byKind = new double[Kinds.Length];
        double[] recByKind = new double[Kinds.Length];
        var doseSum = new double[Doses.Length];
        var doseCount = new int[Doses.Length];
        var daList = new List<double>();
        var deList = new List<double>();
        var recList = new List<double>();
        var gainList = new List<double>();
        int excluded = 0;

        for (int ki = 0; ki < Kinds.Length; ki++)
        {
            double kindSum = 0.0, recSum = 0.0;
            int kindCount = 0;
            for (int di = 0; di < Doses.Length; di++)
            {
                int k = Math.Max(1, (int)Math.Round(Doses[di] * nEdges));
                foreach (uint seed in Seeds)
                {
                    uint s = unchecked(seed * 1000u + (uint)(Kinds[ki].Length * 7) + (uint)(Doses[di] * 10000));
                    var p = Perturb(adj, Kinds[ki], k, s, Doses[di] * 2.0);
                    if (!Connected(p)) { excluded++; continue; }
                    var pertSpec = SpectrumOf(p);
                    var (a1, e1, _) = Buckets(pertSpec);
                    double gain = head > 0 ? (double)(a1 - a0) / head : 0.0;
                    double rec = 1.0 - RelativeRms(pertSpec, baseSpec);
                    doseSum[di] += gain;
                    doseCount[di]++;
                    kindSum += gain;
                    recSum += rec;
                    kindCount++;
                    daList.Add(a1 - a0);
                    deList.Add(e1 - e0);
                    recList.Add(rec);
                    gainList.Add(gain);
                }
            }
            byKind[ki] = kindCount > 0 ? kindSum / kindCount : 0.0;
            recByKind[ki] = kindCount > 0 ? recSum / kindCount : 0.0;
        }

        for (int di = 0; di < Doses.Length; di++)
            byDose[di] = doseCount[di] > 0 ? doseSum[di] / doseCount[di] : 0.0;

        return new AdaptabilityProfile(name, a0, e0, lam2, l, nEdges, degGroups, maxMult, excluded,
            gainList.Count > 0 ? gainList.Average() : 0.0,
            daList.Count > 0 ? daList.Average() : 0.0,
            deList.Count > 0 ? deList.Average() : 0.0,
            recList.Count > 0 ? recList.Average() : 0.0,
            byDose, byKind, recByKind);
    }

    private static readonly Lazy<AdaptabilityProfile[]> ProfilesLazy =
        new(() => CaseNames.Select(Study).ToArray());

    public static IReadOnlyList<AdaptabilityProfile> Profiles => ProfilesLazy.Value;

    public static AdaptabilityProfile P(string name) => Profiles.Single(p => p.Name == name);

    /// <summary>Retained spectrum for one perturbation family at one dose (best case over seeds).</summary>
    public static double RetainedAt(string name, double dose, string kind)
    {
        var adj = Adjacency(name);
        var baseSpec = SpectrumOf(adj);
        int k = Math.Max(1, (int)Math.Round(dose * Edges(adj).Count));
        double best = 0.0;
        foreach (uint seed in Seeds)
        {
            var p = Perturb(adj, kind, k, seed, dose * 2.0);
            if (!Connected(p)) continue;
            best = Math.Max(best, 1.0 - RelativeRms(SpectrumOf(p), baseSpec));
        }
        return best;
    }

    // ── Rank statistics (tie-averaged Spearman) ─────────────────────────────

    /// <summary>Tie-averaged ranks. Values agreeing to 6 decimals are treated as tied, so the
    /// ranking is stable against last-bit differences between algebraically equal quantities.</summary>
    public static double[] Ranks(double[] a)
    {
        int n = a.Length;
        var key = a.Select(v => Math.Round(v, 6)).ToArray();
        var order = Enumerable.Range(0, n).OrderBy(i => key[i]).ToArray();
        var r = new double[n];
        int i0 = 0;
        while (i0 < n)
        {
            int j = i0;
            while (j + 1 < n && key[order[j + 1]] == key[order[i0]]) j++;
            double avg = (i0 + j) / 2.0 + 1.0;
            for (int t = i0; t <= j; t++) r[order[t]] = avg;
            i0 = j + 1;
        }
        return r;
    }

    public static double Pearson(double[] x, double[] y)
    {
        double mx = x.Average(), my = y.Average();
        double sxy = 0, sxx = 0, syy = 0;
        for (int i = 0; i < x.Length; i++)
        {
            double dx = x[i] - mx, dy = y[i] - my;
            sxy += dx * dy; sxx += dx * dx; syy += dy * dy;
        }
        return sxx > 0 && syy > 0 ? sxy / Math.Sqrt(sxx * syy) : 0.0;
    }

    public static double Spearman(double[] x, double[] y) => Pearson(Ranks(x), Ranks(y));

    // ── Ordinary least squares ──────────────────────────────────────────────

    /// <summary>OLS fit y = a + b·x. Returns the slope, intercept, R² and residual sum of squares.</summary>
    public static (double Slope, double Intercept, double R2, double Sse) Fit(double[] x, double[] y)
    {
        int n = x.Length;
        double mx = x.Average(), my = y.Average();
        double sxy = 0, sxx = 0;
        for (int i = 0; i < n; i++) { sxy += (x[i] - mx) * (y[i] - my); sxx += (x[i] - mx) * (x[i] - mx); }
        double b = sxx > 0 ? sxy / sxx : 0.0;
        double a = my - b * mx;
        double sse = 0, sst = 0;
        for (int i = 0; i < n; i++)
        {
            double r = y[i] - (a + b * x[i]);
            sse += r * r;
            sst += (y[i] - my) * (y[i] - my);
        }
        return (b, a, sst > 0 ? 1.0 - sse / sst : 0.0, sse);
    }

    /// <summary>OLS through the origin with a forced slope (for the β = 1 constant-product model).</summary>
    public static (double Offset, double R2, double Sse) FitForcedSlope(double[] x, double[] y, double slope)
    {
        // y_i = offset + slope·x_i, offset chosen to minimize SSE.
        int n = x.Length;
        double offset = 0.0;
        for (int i = 0; i < n; i++) offset += y[i] - slope * x[i];
        offset /= n;
        double my = y.Average(), sse = 0, sst = 0;
        for (int i = 0; i < n; i++)
        {
            double r = y[i] - (offset + slope * x[i]);
            sse += r * r;
            sst += (y[i] - my) * (y[i] - my);
        }
        return (offset, sst > 0 ? 1.0 - sse / sst : 0.0, sse);
    }
}
