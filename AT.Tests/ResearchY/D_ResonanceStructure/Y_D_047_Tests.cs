using System.Globalization;
using System.Text;
using AT.Core.ResearchT;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_047 — Degeneracy-Splitting Amplification Audit.
///
/// Question: can D96 degeneracy splitting AMPLIFY weak perturbations — i.e. is the
/// amplification factor G = Δ(observable)/ε unbounded as the perturbation strength ε → 0?
///
/// Model: L(ε) = L + ε·P, where L is the graph Laplacian and P is a deterministic
/// symmetry-breaking ±1 edge-sign Laplacian (an LCG-signed pattern on the graph's edge set,
/// zero row sums, so L(ε) stays a valid weighted Laplacian for ε &lt; 1).
///
/// Observables: eigenvalue splitting (ΔA = shift in the number of distinct eigenspaces =
/// attractors), attractor count shift, spectral entropy shift ΔE (nats of the basin
/// distribution p_i = m_i/N), and the relative RMS spectral shift.
///
/// Cases: D96 (circulant C96(±1..±6)), Random (seeded sparse p=0.3), Complete (K96),
/// D96³ (D96⊗D96⊗D96 tensor product, 96³ = 884 736 modes; the ε-pattern is applied to each
/// factor ring, so its first-order spectral response is up to 3× the single-factor cases).
///
/// VERDICT FOUND: YES — for structural observables only, and the amplification is DIVERGENT.
///   · DEGENERACY-LOCK THEOREM: the first infinitesimal symmetry-breaking perturbation releases
///     the FULL degeneracy-locked entropy ΔE_lock = (1/N)·Σ_{m_i>1} m_i·ln m_i, a closed form
///     in the multiplicity structure alone. D96: 0.80235 nats; K96: 4.50644 nats. ΔE(ε) is
///     ε-INDEPENDENT for every resolvable ε > 0 → G_E = ΔE_lock/ε → ∞.
///   · The attractor count saturates at ΔA = N − A₀ (D96: 51, K96: 94) — again ε-independent
///     → G_A → ∞. The response is a STEP function at ε = 0, not a smooth one.
///   · RANDOM is a perfect NULL: A ≡ 96 distinct levels, ΔA ≡ ΔE ≡ 0 for every ε up to 1.0 → G ≡ 0.
///   · The CONTINUOUS observable is NOT amplified: G_λ = Δλ_rms/ε is finite and ε-independent
///     (≈ 0.056 for D96), i.e. the amplification is purely a degeneracy effect.
///   · D96³ amplifies hardest (peak G_A ≈ 1.3e11, ≳ 2000× D96) but is capped at ~16 % of its
///     headroom by the residual factor-permutation symmetry of the tensor product.
///
/// Deterministic: fixed seed, no unseeded randomness, no external dependencies.
/// </summary>
public class Y_D_047_Tests : ResearchTestBase
{
    public Y_D_047_Tests(ITestOutputHelper output) : base(output) { }

    private const uint PatternSeed = 12345u;
    private const double Tol1D = 1e-9;    // 1-factor resolution floor
    private const double TolCube = 1e-8;  // 3-factor sums carry ~1e-14 noise; tol above it

    private static readonly double[] Eps1D = [1e-9, 1e-8, 1e-7, 1e-6, 1e-5, 1e-4, 1e-3, 1e-2, 1e-1];
    private static readonly double[] EpsCube = [1e-8, 1e-7, 1e-6, 1e-5, 1e-4, 1e-3, 1e-2];

    // ── Perturbation ─────────────────────────────────────────────────────────

    /// <summary>Deterministic LCG bit stream (numerically identical in any runtime/language).</summary>
    private static int[] SignBits(int count, uint seed)
    {
        var bits = new int[count];
        uint x = seed;
        for (int i = 0; i < count; i++)
        {
            x = unchecked(1664525u * x + 1013904223u);
            bits[i] = (int)((x >> 16) & 1u);
        }
        return bits;
    }

    /// <summary>
    /// Symmetry-breaking ±1 edge-sign Laplacian on the graph's edge set. Row sums vanish, so
    /// L + ε·P is the Laplacian of the weighted graph w_ij → w_ij·(1 + ε·s_ij).
    /// </summary>
    private static double[,] SignPerturbation(double[,] adjacency, uint seed)
    {
        int n = adjacency.GetLength(0);
        var sign = new double[n, n];
        var bits = SignBits(n * n, seed);
        int c = 0;
        for (int i = 0; i < n; i++)
            for (int j = i + 1; j < n; j++)
            {
                if (adjacency[i, j] != 0.0) sign[i, j] = sign[j, i] = bits[c] == 1 ? 1.0 : -1.0;
                c++;
            }

        var p = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            double s = 0.0;
            for (int j = 0; j < n; j++)
                if (i != j) { s += sign[i, j]; p[i, j] = -sign[i, j]; }
            p[i, i] = s;
        }
        return p;
    }

    // ── Spectral observables ─────────────────────────────────────────────────

    /// <summary>Distinct-eigenspace (attractor) count, Shannon entropy of the basin distribution, multiplicities.</summary>
    private static (int A, double E, int[] Mult) Buckets(double[] spectrum, double tol)
    {
        var s = (double[])spectrum.Clone();
        Array.Sort(s);
        var mult = new List<int>();
        int i = 0;
        while (i < s.Length)
        {
            int j = i;
            while (j < s.Length && Math.Abs(s[j] - s[i]) <= tol) j++;
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

    /// <summary>Relative RMS spectral shift ‖λ(ε) − λ(0)‖ / ‖λ(0)‖ (the continuous observable).</summary>
    private static double RmsShift(double[] a, double[] b)
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

    /// <summary>ΔE_lock = (1/N)·Σ_{m&gt;1} m·ln m — the entropy locked inside the degeneracies.</summary>
    private static double LockedEntropy(int[] mult)
    {
        long n = 0;
        foreach (int m in mult) n += m;
        double s = 0.0;
        foreach (int m in mult) if (m > 1) s += m * Math.Log(m);
        return n > 0 ? s / n : 0.0;
    }

    // ── Case study ───────────────────────────────────────────────────────────

    private sealed record Study(
        string Model, int N, int A0, double E0, int DegGroups, int Doublets, int MaxMult,
        double Locked, int Headroom, double[] Eps, int[] A, double[] E, double[] Rms)
    {
        public int DeltaA(int k) => A[k] - A0;
        public double DeltaE(int k) => E[k] - E0;
        public double GA(int k) => DeltaA(k) / Eps[k];
        public double GE(int k) => DeltaE(k) / Eps[k];
        public double GLambda(int k) => Rms[k] / Eps[k];
        public int IndexOf(double eps) => Array.IndexOf(Eps, eps);
    }

    private static Study Run(string model, double[,] laplacian, double tol, double[] eps, bool cube)
    {
        int n = laplacian.GetLength(0);
        var pert = SignPerturbation(AdjacencyOf(laplacian), PatternSeed);

        double[] baseSpec = GeneralInverseSpectrumAnalyzer.Spectrum(laplacian);
        if (cube) baseSpec = CubeSpectrum(baseSpec);
        var (a0, e0, m0) = Buckets(baseSpec, tol);

        int deg = 0, dbl = 0, maxMult = 0;
        foreach (int m in m0)
        {
            if (m > 1) deg++;
            if (m == 2) dbl++;
            if (m > maxMult) maxMult = m;
        }

        var a = new int[eps.Length];
        var e = new double[eps.Length];
        var rms = new double[eps.Length];

        for (int k = 0; k < eps.Length; k++)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    laplacian[i, j] += eps[k] * pert[i, j];

            double[] spec = cube
                ? CubeSpectrum(GeneralInverseSpectrumAnalyzer.Spectrum(laplacian))
                : GeneralInverseSpectrumAnalyzer.Spectrum(laplacian);

            var (ak, ek, _) = Buckets(spec, tol);
            a[k] = ak;
            e[k] = ek;
            rms[k] = RmsShift(spec, baseSpec);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    laplacian[i, j] -= eps[k] * pert[i, j];
        }

        return new Study(model, cube ? n * n * n : n, a0, e0, deg, dbl, maxMult,
            LockedEntropy(m0), (cube ? n * n * n : n) - a0, eps, a, e, rms);
    }

    private static double[,] AdjacencyOf(double[,] laplacian)
    {
        int n = laplacian.GetLength(0);
        var adj = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (i != j && Math.Abs(laplacian[i, j]) > 1e-9) adj[i, j] = 1.0;
        return adj;
    }

    /// <summary>D96⊗D96⊗D96 spectrum: the Minkowski sum Λ = λ_i + λ_j + λ_k (884 736 modes).</summary>
    private static double[] CubeSpectrum(double[] oneD)
    {
        int n = oneD.Length;
        var s = new double[n * n * n];
        int k = 0;
        for (int i = 0; i < n; i++)
        {
            double a = oneD[i];
            for (int j = 0; j < n; j++)
            {
                double b = a + oneD[j];
                for (int l = 0; l < n; l++) s[k++] = b + oneD[l];
            }
        }
        Array.Sort(s);
        return s;
    }

    // ── Cached study set (one build per test class) ──────────────────────────

    private static readonly Lazy<Study[]> Studies = new(() =>
    [
        Run("D96",
            GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.D96DerivedGraph(96, 6)),
            Tol1D, Eps1D, false),
        Run("random",
            GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42)),
            Tol1D, Eps1D, false),
        Run("complete",
            GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.CompleteGraph(96)),
            Tol1D, Eps1D, false),
        Run("D96^3",
            GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.D96DerivedGraph(96, 6)),
            TolCube, EpsCube, true),
    ]);

    private static Study Get(string model) => Studies.Value.Single(s => s.Model == model);

    // ── 1. The degeneracy structure (the amplifier's reservoir) ──────────────

    [Fact]
    public void Y_D_047_DegeneracyStructure()
    {
        var d96 = Get("D96");
        var rnd = Get("random");
        var cmp = Get("complete");
        var cub = Get("D96^3");

        // D96: 44 degenerate groups ([42×2, 5, 6]) → 45 distinct eigenspaces incl. λ₀ = 0.
        Assert.Equal(45, d96.A0);
        Assert.Equal(44, d96.DegGroups);
        Assert.Equal(42, d96.Doublets);
        Assert.Equal(6, d96.MaxMult);
        Assert.Equal(51, d96.Headroom);              // N − A₀ = Σ(m−1) = 42 + 4 + 5
        Assert.Equal(95, d96.A0 - 1 + d96.Headroom); // Σm = 95 positive modes

        // Complete K96: {0 (×1), 96 (×95)} — one 95-fold locked manifold.
        Assert.Equal(2, cmp.A0);
        Assert.Equal(1, cmp.DegGroups);
        Assert.Equal(95, cmp.MaxMult);
        Assert.Equal(94, cmp.Headroom);

        // Random: all-singleton spectrum — no degeneracy reservoir at all.
        Assert.Equal(96, rnd.A0);
        Assert.Equal(0, rnd.DegGroups);
        Assert.Equal(0, rnd.Headroom);

        // D96³: degeneracy is enormous — A₀ ≫ 96 and the reservoir ≫ 96.
        Assert.True(cub.A0 > 15_000, $"D96³ A₀ = {cub.A0}");
        Assert.Equal(884_736, cub.N);
        Assert.True(cub.Headroom > 800_000, $"D96³ headroom = {cub.Headroom}");
    }

    // ── 2. Attractor-count shift: a STEP, so G_A diverges as ε → 0 ───────────

    [Fact]
    public void Y_D_047_AttractorCountGain()
    {
        var d96 = Get("D96");
        var cmp = Get("complete");
        var rnd = Get("random");

        // The shift JUMPS to the full headroom (N − A₀) at the resolution floor and then stays
        // there: ΔA is independent of ε over 5 decades — the response is a step function.
        foreach (double eps in new[] { 1e-6, 1e-5, 1e-4, 1e-3, 1e-2, 1e-1 })
        {
            Assert.Equal(d96.Headroom, d96.DeltaA(d96.IndexOf(eps)));
            Assert.Equal(cmp.Headroom, cmp.DeltaA(cmp.IndexOf(eps)));
        }

        // Partial resolution strictly increases towards the saturation ceiling (monotone step).
        Assert.True(d96.DeltaA(d96.IndexOf(1e-8)) < d96.DeltaA(d96.IndexOf(1e-7)));
        Assert.True(d96.DeltaA(d96.IndexOf(1e-7)) <= d96.Headroom);

        // G_A = ΔA/ε is therefore ∝ 1/ε: 5+ orders of magnitude of gain over the sweep.
        double gHigh = d96.GA(d96.IndexOf(1e-3));
        double gLow = d96.GA(d96.IndexOf(1e-9));
        Assert.True(gLow > 5e9, $"G_A(D96, 1e-9) = {gLow:E3}");
        Assert.True(gLow / gHigh > 1e5, $"G_A grows by {gLow / gHigh:E3}× as ε falls 6 decades");
        Assert.True(gHigh > 1e4, $"G_A(D96, 1e-3) = {gHigh:E3}");

        // Complete amplifies harder still: 94 attractors unlocked vs 51.
        Assert.True(cmp.GA(cmp.IndexOf(1e-3)) > d96.GA(d96.IndexOf(1e-3)));

        // Random: exactly zero — the null control.
        foreach (double e in rnd.Eps) Assert.Equal(0, rnd.DeltaA(rnd.IndexOf(e)));
    }

    // ── 3. Spectral entropy: the DEGENERACY-LOCK closed form ────────────────

    [Fact]
    public void Y_D_047_DegeneracyLockEntropy()
    {
        var d96 = Get("D96");
        var cmp = Get("complete");
        var rnd = Get("random");

        // Closed form: ΔE_lock = (1/N)·Σ_{m_i>1} m_i·ln m_i.
        Assert.Equal(0.8023, d96.Locked, 4);                         // 42 doublets + [5] + [6] over N=96
        Assert.Equal(4.5064, cmp.Locked, 4);                         // (95/96)·ln 95
        Assert.Equal(0.0, rnd.Locked, 12);                           // no degeneracy → nothing locked

        // The perturbation RELEASES EXACTLY the locked entropy — and the released amount does
        // not depend on how weak the perturbation is (ε-independent over 5 decades).
        foreach (double eps in new[] { 1e-6, 1e-5, 1e-4, 1e-3, 1e-2, 1e-1 })
        {
            Assert.Equal(d96.Locked, d96.DeltaE(d96.IndexOf(eps)), 5);
            Assert.Equal(cmp.Locked, cmp.DeltaE(cmp.IndexOf(eps)), 5);
        }

        // Hence G_E = ΔE_lock/ε → ∞ (divergent), while the random control is identically 0.
        Assert.True(d96.GE(d96.IndexOf(1e-9)) > 1e8, $"G_E(D96, 1e-9) = {d96.GE(d96.IndexOf(1e-9)):E3}");
        Assert.True(d96.GE(d96.IndexOf(1e-6)) / d96.GE(d96.IndexOf(1e-2)) > 5e3);
        foreach (double e in rnd.Eps) Assert.Equal(0.0, rnd.DeltaE(rnd.IndexOf(e)), 12);
    }

    // ── 4. Random is a perfect null (no degeneracy → no amplification) ───────

    [Fact]
    public void Y_D_047_RandomNullResponse()
    {
        var rnd = Get("random");

        // A ≡ 96, E ≡ E₀, shifted spectrum stays all-singleton for EVERY ε in the grid.
        foreach (double eps in rnd.Eps)
        {
            int k = rnd.IndexOf(eps);
            Assert.Equal(rnd.A0, rnd.A[k]);
            Assert.Equal(0.0, rnd.DeltaE(k), 12);
            Assert.Equal(0.0, rnd.GA(k), 12);
            Assert.Equal(0.0, rnd.GE(k), 12);
        }

        // …and even far outside the grid (ε = 1.0, where the operator is strongly perturbed).
        var lap = GeneralInverseSpectrumAnalyzer.Laplacian(GeneralInverseSpectrumAnalyzer.RandomSparseGraph(96, 0.3, 42));
        double[] baseSpec = GeneralInverseSpectrumAnalyzer.Spectrum(lap);
        var pert = SignPerturbation(AdjacencyOf(lap), PatternSeed);
        for (int i = 0; i < 96; i++)
            for (int j = 0; j < 96; j++)
                lap[i, j] += 1.0 * pert[i, j];
        var (aBig, eBig, _) = Buckets(GeneralInverseSpectrumAnalyzer.Spectrum(lap), Tol1D);
        Assert.Equal(96, aBig);
        Assert.Equal(rnd.E0, eBig, 6);
    }

    // ── 5. The continuous observable is NOT amplified ────────────────────────

    [Fact]
    public void Y_D_047_SpectralGainFinite()
    {
        var d96 = Get("D96");
        var rnd = Get("random");
        var cmp = Get("complete");

        // G_λ = Δλ_rms/ε is finite and ε-INDEPENDENT (linear response): the same gain at the
        // weakest and the strongest perturbation, in sharp contrast with G_A/G_E.
        double gLamLow = d96.GLambda(d96.IndexOf(1e-9));
        double gLamHigh = d96.GLambda(d96.IndexOf(1e-3));
        Assert.True(gLamLow > 1e-3 && gLamLow < 10.0, $"G_λ(D96, 1e-9) = {gLamLow}");
        Assert.True(gLamLow / gLamHigh < 1.5, $"G_λ ratio = {gLamLow / gLamHigh}");
        Assert.True(gLamLow / gLamHigh > 0.5, $"G_λ ratio = {gLamLow / gLamHigh}");

        // The structural gain over the same interval grows by ≥ 1e5 — four orders more.
        Assert.True(d96.GA(d96.IndexOf(1e-9)) / d96.GA(d96.IndexOf(1e-3)) > 1e5);
        Assert.True(d96.GE(d96.IndexOf(1e-9)) / d96.GE(d96.IndexOf(1e-3)) > 1e5);

        // D96's continuous response is the SAME ORDER as the degenerate-free random control:
        // the amplification is purely a degeneracy effect, not a spectral one.
        double ratio = gLamLow / rnd.GLambda(rnd.IndexOf(1e-9));
        Assert.True(ratio > 0.2 && ratio < 5.0, $"G_λ(D96)/G_λ(random) = {ratio}");

        // Complete (one 95-fold block) is the opposite extreme: a large relative shift.
        Assert.True(cmp.GLambda(cmp.IndexOf(1e-3)) > 0.0);
    }

    // ── 6. D96³ — hardest amplifier, capped by permutation protection ───────

    [Fact]
    public void Y_D_047_CubeAmplificationAndCeiling()
    {
        var d96 = Get("D96");
        var cmp = Get("complete");
        var rnd = Get("random");
        var cub = Get("D96^3");

        // Peak gain at the weakest resolvable strength: D96³ ≫ complete > D96 ≫ random = 0.
        int kCube = cub.IndexOf(1e-6);
        int kD96 = d96.IndexOf(1e-6);
        int kCmp = cmp.IndexOf(1e-6);
        int kRnd = rnd.IndexOf(1e-6);

        Assert.True(cub.DeltaA(kCube) > 100_000, $"ΔA(D96³, 1e-6) = {cub.DeltaA(kCube)}");
        Assert.True(cub.GA(kCube) > 1e10, $"G_A(D96³, 1e-6) = {cub.GA(kCube):E3}");
        Assert.True(cub.GA(kCube) > cmp.GA(kCmp));
        Assert.True(cmp.GA(kCmp) > d96.GA(kD96));
        Assert.True(d96.GA(kD96) > 0);
        Assert.Equal(0.0, rnd.GA(kRnd), 12);

        // Peak of G_A is attained at the WEAKEST ε that still resolves the splitting — the
        // divergence signature (below it the splittings drop under the resolution floor).
        int peak = 0;
        for (int k = 1; k < cub.Eps.Length; k++) if (cub.GA(k) > cub.GA(peak)) peak = k;
        Assert.True(cub.Eps[peak] <= 1e-6, $"peak at ε = {cub.Eps[peak]:g}");
        Assert.True(cub.GA(peak) > 1e11, $"peak G_A = {cub.GA(peak):E3}");
        Assert.True(cub.GA(peak) / cub.GA(cub.Eps.Length - 1) > 1e3,
            $"G_A(peak)/G_A(1e-2) = {cub.GA(peak) / cub.GA(cub.Eps.Length - 1):E3}");

        // PROTECTION CEILING: unlike the 1-factor cases, D96³ never reaches N − A₀ — the
        // factor-permutation symmetry of the tensor product is untouched by a factor-wise
        // perturbation, so the permuted triple sums stay exactly degenerate.
        double frac = (double)cub.DeltaA(cub.IndexOf(1e-2)) / cub.Headroom;
        Assert.True(frac < 0.20, $"D96³ unlocked only {frac:P1} of its headroom");

        // Entropy is likewise only PARTIALLY unlocked (it weights the large permutation orbits,
        // so it recovers more of its ceiling than the count does).
        double lockFrac = cub.DeltaE(cub.IndexOf(1e-2)) / cub.Locked;
        Assert.True(lockFrac > 0.3 && lockFrac < 0.8, $"D96³ entropy unlocked = {lockFrac:P1}");
    }

    // ── 7. Report ────────────────────────────────────────────────────────────

    [Fact]
    public void Y_D_047_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_047 — Degeneracy-Splitting Amplification Audit");

        sb.AppendLine("Question: can D96 degeneracy splitting amplify weak perturbations?");
        sb.AppendLine("Measure:  G = Δ(observable)/ε   for   L(ε) = L + ε·P,  P = ±1 edge-sign Laplacian.");
        sb.AppendLine("Observables: ΔA (attractor count), ΔE (spectral entropy, nats), Δλ_rms (spectrum).");
        sb.AppendLine();

        var all = Studies.Value;
        sb.AppendLine("[1] Amplifier reservoirs");
        sb.AppendLine("     case        N          A0      degen-groups  max-mult  ΔE_lock(nats)  headroom N−A0");
        foreach (var s in all)
            sb.AppendLine($"     {s.Model,-10} {s.N,-10} {s.A0,-7} {s.DegGroups,-13} {s.MaxMult,-9} " +
                          $"{s.Locked,-14:F5} {s.Headroom}");
        sb.AppendLine();

        sb.AppendLine("[2] Amplification factor G_A = Δ(attractor count)/ε");
        sb.AppendLine("     eps        " + string.Join("  ", all.Select(s => s.Model.PadLeft(12))));
        foreach (double eps in Eps1D)
        {
            var cells = all.Select(s =>
            {
                int k = Array.IndexOf(s.Eps, eps);
                return k >= 0 ? s.GA(k).ToString("0.000E+00").PadLeft(12) : new string('-', 12);
            });
            sb.AppendLine($"     {eps,-10:g} " + string.Join("  ", cells));
        }
        sb.AppendLine();

        sb.AppendLine("[3] Spectral-entropy shift ΔE and its gain G_E = ΔE/ε (D96)");
        var d96 = Get("D96");
        foreach (double eps in d96.Eps)
        {
            int k = d96.IndexOf(eps);
            sb.AppendLine($"     eps={eps,-8:g} ΔA={d96.DeltaA(k),3}  G_A={d96.GA(k),11:E3}   " +
                          $"ΔE={d96.DeltaE(k):F6}  G_E={d96.GE(k),11:E3}   G_λ={d96.GLambda(k):F4}");
        }
        sb.AppendLine();

        sb.AppendLine("[4] Degeneracy-lock closed form  ΔE_lock = (1/N)·Σ_{m>1} m·ln m");
        sb.AppendLine($"     D96      (42×2,[5],[6]) : {d96.Locked:F6} nats   (measured {d96.DeltaE(d96.IndexOf(1e-2)):F6})");
        var cmp = Get("complete");
        sb.AppendLine($"     complete (95×1)        : {cmp.Locked:F6} nats   (measured {cmp.DeltaE(cmp.IndexOf(1e-2)):F6})");
        var rnd = Get("random");
        sb.AppendLine($"     random   (all singlet) : {rnd.Locked:F6} nats   (measured {rnd.DeltaE(rnd.IndexOf(1e-2)):F6})");
        sb.AppendLine();

        sb.AppendLine("[5] D96³ (96³ = 884 736 modes) — hard amplifier, permutation ceiling");
        var cub = Get("D96^3");
        foreach (double eps in cub.Eps)
        {
            int k = cub.IndexOf(eps);
            double frac = (double)cub.DeltaA(k) / cub.Headroom;
            sb.AppendLine($"     eps={eps,-8:g} ΔA={cub.DeltaA(k),8}  G_A={cub.GA(k),11:E3}  " +
                          $"ΔE={cub.DeltaE(k):F6}  unlocked={frac:P1} of headroom");
        }
        sb.AppendLine();

        sb.AppendLine("[6] Verdict");
        sb.AppendLine("  YES — D96 degeneracy splitting amplifies weak perturbations without limit,");
        sb.AppendLine("  but ONLY on structural (degeneracy-indexed) observables:");
        sb.AppendLine($"    · G_A(D96) = {d96.GA(d96.IndexOf(1e-9)):E3} at ε=1e-9 and {d96.GA(d96.IndexOf(1e-3)):E3} at ε=1e-3");
        sb.AppendLine($"      → the response is a STEP, G ∝ 1/ε, divergent as ε → 0.");
        sb.AppendLine($"    · ΔE_lock = {d96.Locked:F5} nats (D96) is released EXACTLY by any ε > 0;");
        sb.AppendLine("      the same ε-independence holds for complete.");
        sb.AppendLine("    · G_λ = Δλ_rms/ε is FINITE and ε-independent — the continuous spectrum is");
        sb.AppendLine("      not amplified; D96's G_λ matches the degeneracy-free random control.");
        sb.AppendLine("    · random (all-singleton) is an exact NULL: ΔA ≡ ΔE ≡ 0, G ≡ 0 up to ε = 1.0.");
        sb.AppendLine("    · D96³ is the hardest amplifier (G_A ≳ 1e11) but saturates near 16 % of its");
        sb.AppendLine("      headroom: factor-permutation symmetry protects the remaining degeneracy.");
        sb.AppendLine();

        sb.AppendLine("[7] Classification");
        sb.AppendLine("  DERIVED : ΔE_lock = (1/N)·Σ_{m>1} m·ln m  (closed form in the multiplicity structure)");
        sb.AppendLine("  DERIVED : ΔA_max = N − A0 = Σ(m−1)  (degeneracy headroom)");
        sb.AppendLine("  EMERGENT: the ε-independence of the release (step response at ε = 0)");
        sb.AppendLine("  REFUTED : 'D96 amplifies weak perturbations in its continuous spectrum' (G_λ finite)");
        sb.AppendLine("  BOUNDARY: the numerical resolution floor sets where full splitting is observed,");
        sb.AppendLine("            not any intrinsic physical scale.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
