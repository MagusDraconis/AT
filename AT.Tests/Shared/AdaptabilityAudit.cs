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

    // ── The blind-ring family (shared by D_051 and D_052) ───────────────────
    //
    // Six 96-node circulant rings that appear in NO D_048–D_050 case set, introduced by D_051 and
    // reused verbatim by D_052 so the ring family has ONE definition. All are connected.

    public static readonly (string Name, string Description, (int Offset, double Weight)[] Offsets)[] BlindRings =
    [
        ("S96-123", "sparse ring, offsets ±1..±3, unit weights (degree 6)",
            [(1, 1.0), (2, 1.0), (3, 1.0)]),
        ("S96-135", "sparse ring, offsets ±1,±3,±5, unit weights (degree 6)",
            [(1, 1.0), (3, 1.0), (5, 1.0)]),
        ("D96-24", "dense ring, offsets ±1..±12, unit weights (degree 24)",
            [(1, 1.0), (2, 1.0), (3, 1.0), (4, 1.0), (5, 1.0), (6, 1.0),
             (7, 1.0), (8, 1.0), (9, 1.0), (10, 1.0), (11, 1.0), (12, 1.0)]),
        ("Decay96", "ring ±1..±6 with decaying weights w_d = 1/d (degree 12)",
            [(1, 1.0), (2, 0.5), (3, 1.0 / 3.0), (4, 0.25), (5, 0.2), (6, 1.0 / 6.0)]),
        ("Boost96", "ring ±1..±6 with GROWING weights w_d = d (degree 12)",
            [(1, 1.0), (2, 2.0), (3, 3.0), (4, 4.0), (5, 5.0), (6, 6.0)]),
        ("Ring48", "ring ±1..±6 plus the long-range offsets ±24 and ±48 (degree 12)",
            [(1, 1.0), (2, 1.0), (3, 1.0), (4, 1.0), (5, 1.0), (6, 1.0), (24, 1.0), (48, 1.0)]),
    ];

    /// <summary>Symmetric adjacency matrix of a circulant ring with the given signed offsets.</summary>
    public static double[,] Ring((int Offset, double Weight)[] offsets)
    {
        var a = new double[N, N];
        for (int i = 0; i < N; i++)
            foreach (var (d, w) in offsets)
            {
                a[i, (i + d) % N] += w;
                a[i, ((i - d) % N + N) % N] += w;
            }
        return a;
    }

    /// <summary>Adjacency of a blind ring, or of the canonical ring D96, by name.</summary>
    public static double[,] RingAdjacency(string name)
        => name == "D96" ? Adjacency("D96")
                         : Ring(AllRings.Single(r => r.Name == name).Offsets);

    /// <summary>
    /// D_054's edge-shape rings. Like D_051's set these are connected 96-node circulants absent from
    /// D_048–D_050's case set, but they are chosen for a different purpose: to make the LOW-ENERGY
    /// SPECTRAL EDGE depart from the universal parabolic form λ_k ∝ k² in as many different ways as
    /// possible (high offsets, geometric offset ladders, scale-hierarchy weights, a long-range pair).
    /// </summary>
    public static readonly (string Name, string Description, (int Offset, double Weight)[] Offsets)[] EdgeRings =
    [
        ("E1", "single offset ±1 only (degree 2) — the pure parabolic edge",
            [(1, 1.0)]),
        ("E1-16-32", "offsets ±1, ±16, ±32, unit weights (degree 6) — high offsets bend the edge",
            [(1, 1.0), (16, 1.0), (32, 1.0)]),
        ("G1248", "geometric offsets ±1,±2,±4,±8,±16,±32 (degree 12)",
            [(1, 1.0), (2, 1.0), (4, 1.0), (8, 1.0), (16, 1.0), (32, 1.0)]),
        ("Wscale", "offsets ±1 (w=1), ±2 (w=1/4), ±4 (w=1/16), ±8 (w=1/64) (degree 8) — scale-hierarchy weights",
            [(1, 1.0), (2, 0.25), (4, 0.0625), (8, 0.015625)]),
        ("Two1-12", "offsets ±1 (w=1) and ±12 (w=4) (degree 4) — short edge with heavy long range",
            [(1, 1.0), (12, 4.0)]),
        ("Pair1-47", "offsets ±1 (w=1) and ±47 (w=1) (degree 4) — the near-antipodal pair",
            [(1, 1.0), (47, 1.0)]),
    ];

    /// <summary>The D_054 blind set (six edge-shape rings), by name.</summary>
    public static readonly string[] EdgeRingNames = [.. EdgeRings.Select(r => r.Name)];

    /// <summary>Every named ring the audit family knows: the canonical ring, D_051's set and D_054's set.</summary>
    private static readonly (string Name, string Description, (int Offset, double Weight)[] Offsets)[] AllRings =
        [.. BlindRings, .. EdgeRings];

    /// <summary>The seven-ring family D_052 audits: the canonical ring plus the six blind rings.</summary>
    public static readonly string[] RingFamilyNames =
        ["D96", .. BlindRings.Select(r => r.Name)];

    /// <summary>
    /// Degeneracy-lock scalar of D_047: ΔE_lock = (1/N)·Σ_{m_i>1} m_i·ln m_i — the multiplicity
    /// spectrum collapsed to the entropy it can release. Zero for an all-singleton spectrum.
    /// </summary>
    public static double LockedEntropy(int[] mult, int n)
    {
        double s = 0.0;
        foreach (int m in mult)
            if (m > 1) s += m * Math.Log(m);
        return s / n;
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

    /// <summary>
    /// Near-gap density at k = 2 — the T_014 counting convention: POSITIVE eigenvalues within twice
    /// the algebraic-connectivity gap (the zero mode is excluded). D96 gives 2 (the k = ±1 doublet).
    /// This is D_050's winning single predictor, factored out so both audits count identically.
    /// </summary>
    public static int NearGapDensityK2(double[] spectrum, double lambda2)
        => spectrum.Count(l => l > Tol && l <= 2.0 * lambda2 + Tol);

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

    public static AdaptabilityProfile Study(string name) => Study(name, Adjacency(name));

    /// <summary>
    /// Study an ARBITRARY adjacency matrix under the shared deterministic ensemble. The name is a
    /// label only — no lookup is performed — so a spectrum never seen in D_048/D_049 can be measured
    /// by exactly the same protocol (same perturbation types, doses, seeds and connectivity guard).
    /// </summary>
    public static AdaptabilityProfile Study(string name, double[,] adj)
    {
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
