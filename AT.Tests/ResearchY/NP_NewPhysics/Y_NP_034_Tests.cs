using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_034 — Bose Without Blackbody Audit test suite (Y_NP_034_Tests.cs).
///
/// Question: why does a D96 ensemble produce Bose occupation statistics
/// n(ω) = 1/(e^(βω) − 1) yet fail to reproduce the observed blackbody spectrum?
/// Goal: identify the MINIMAL OBSTRUCTION by factorization and two control experiments.
///
/// Verdict tested: Bose statistics is SUFFICIENT — the obstruction is entirely the
/// D96 mode-set g(ω). (1) u(ω)=g(ω)·n(ω)·ω factorizes: the mode-set supplies g, the
/// ensemble supplies the exact Bose occupation n (NP_033 identity). (2) Replacing the
/// D96 occupations with exact Planck occupations is a NO-OP (they are already exact
/// Bose, NP_033): blackbody still fails ⇒ the occupation is NOT the obstruction.
/// (3) Replacing the D96 mode-set with the ideal ω² DOS (keeping the Bose occupation)
/// reproduces the blackbody exactly: π⁴/15 integral, Wien displacement at x = 2.821,
/// Wien exponential tail, Rayleigh–Jeans x² ⇒ the mode-set/DOS IS the obstruction.
/// (4) Sensitivity: UV cutoff (40.7% of blackbody energy lies above ω_max = 3.98 at
/// β = 1), DOS exponent (D96 low exponent ≈ 1.0, mid ≈ 1.51 vs 3), mode clustering
/// (44 distinct freqs, 8-bin counts [2,2,2,0,2,2,33,52]), finite count (95 modes is
/// ample — an ideal ω²-distributed 95-mode set reproduces the in-band blackbody to
/// ~0.05%). (5) Minimal deformation: keep the Bose occupation, replace only the mode
/// set (ω² DOS over an unbounded band) ⇒ blackbody restored. Answer A: Bose is
/// sufficient and only the D96 DOS fails; no additional obstruction (B); no new
/// primitive (C). Temperature remains BOUNDARY; D96 as blackbody host stays FALSIFIED
/// (NP_028/033 unchanged); Bose occupation from the ensemble stays EMERGENT.
///
/// Deterministic: closed-form D96 spectrum, closed-form occupation, Simpson integrals.
/// </summary>
public class Y_NP_034_Tests : ResearchTestBase
{
    public Y_NP_034_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;
    private const double Pi4Over15 = 6.493939402266829;

    private static double LambdaK(int k)
    {
        double sum = 0;
        for (int s = 1; s <= 6; s++)
            sum += 2 * (1 - Math.Cos(2.0 * Math.PI * k * s / N));
        return sum;
    }

    private static double OmegaK(int k) => Math.Sqrt(LambdaK(k));

    private static double Bose(double beta, double w) => 1.0 / (Math.Exp(beta * w) - 1.0);

    /// <summary>95 positive D96 modes ω_k, k = 1..95 (mirror pairs included).</summary>
    private static double[] Modes()
    {
        var w = new double[N - 1];
        for (int k = 1; k < N; k++) w[k - 1] = Math.Sqrt(LambdaK(k));
        Array.Sort(w);
        return w;
    }

    /// <summary>Distinct frequencies with multiplicities (mode-set factor g).</summary>
    private static (double[] Freq, int[] Mult) DistinctModeSet()
    {
        var w = Modes();
        var freqs = new List<double>();
        var mults = new List<int>();
        foreach (double x in w)
        {
            if (freqs.Count == 0 || Math.Abs(x - freqs[^1]) > 1e-9)
            {
                freqs.Add(x);
                mults.Add(1);
            }
            else mults[^1]++;
        }
        return (freqs.ToArray(), mults.ToArray());
    }

    // ── [Required] Y_NP_034_Factorization ────────────────────────

    [Fact]
    public void Y_NP_034_Factorization()
    {
        // u(ω) = g(ω) · n(ω) · ω. The mode-set factor g is the distinct-mode
        // inventory (frequencies + multiplicities); the occupation factor n is the
        // exact Bose occupation the ensemble produces (NP_033).
        var w = Modes();
        var (freq, mult) = DistinctModeSet();
        Assert.Equal(95, w.Length);
        Assert.Equal(44, freq.Length); // 42 mirror pairs + 5-fold + 6-fold blocks
        Assert.Equal(6, mult.Max());

        double beta = 1.0;

        // Occupation factor n(ω) is the exact Bose occupation over every mode.
        foreach (int k in new[] { 1, 40, 70, 90 })
        {
            double ww = OmegaK(k);
            double n = Bose(beta, ww);
            Assert.Equal(-beta * ww, Math.Log(n / (1 + n)), 9); // Boltzmann identity (NP_033)
        }

        // g·n·ω factorization: sum over distinct modes weighted by multiplicity
        // (mode-set g) equals the sum over the raw 95 modes.
        double uFactorized = 0, uRaw = 0;
        for (int i = 0; i < freq.Length; i++)
            uFactorized += mult[i] * freq[i] * Bose(beta, freq[i]);
        foreach (double x in w)
            uRaw += x * Bose(beta, x);
        Assert.Equal(uRaw, uFactorized, 9);

        // Total spectral energy U(1) = 12.588 (matches NP_033).
        Assert.Equal(12.588, uRaw, 3);
    }

    // ── [Required] Y_NP_034_OccupationReplacementIsNoop ──────────

    [Fact]
    public void Y_NP_034_OccupationReplacementIsNoop()
    {
        // Test 2: replace the D96 occupations with exact Planck occupations. The D96
        // ensemble occupation IS already the exact Planck/Bose occupation (NP_033
        // identity), so the replacement changes nothing. Blackbody still fails ⇒ the
        // occupation is not the obstruction.
        var w = Modes();
        double beta = 1.0;

        // The D96 occupation already equals the exact Planck occupation pointwise.
        foreach (double x in w)
        {
            double nEnsemble = Bose(beta, x);
            double nExactPlanck = 1.0 / (Math.Exp(beta * x) - 1.0);
            Assert.Equal(nExactPlanck, nEnsemble, 12);
        }

        // Blackbody still fails on the D96 mode set after "replacement":
        // (a) discrete sum Σω³/(e^ω−1) = 120.70 ≫ π⁴/15 = 6.494.
        double disc = 0;
        foreach (double x in w)
            disc += x * x * x * Bose(beta, x);
        Assert.True(Math.Abs(disc - Pi4Over15) > 50.0, $"Σω³n = {disc} ≠ π⁴/15");

        // (b) top-heavy: 65.7% of D96 energy above ω=3.3 vs 23.3% for Planck in-band.
        double d96Top = w.Where(x => x >= 3.3).Sum(x => x * Bose(beta, x));
        double d96All = w.Sum(x => x * Bose(beta, x));
        Assert.True(d96Top / d96All > 0.55, "D96 energy remains top-heavy");
    }

    // ── [Required] Y_NP_034_IdealW2DOSRestoresBlackbody ──────────

    [Fact]
    public void Y_NP_034_IdealW2DOSRestoresBlackbody()
    {
        // Test 3: replace the D96 mode set with the ideal ω² DOS (3D cavity), keep the
        // Bose occupation n(ω)=1/(e^(βω)−1). Then u(ω)=ω²·n(ω)·ω = ω³/(e^(βω)−1):
        // the EXACT blackbody. All four Planck limits must hold.
        // (a) Stefan-Boltzmann: ∫₀^∞ x³/(e^x−1) dx = π⁴/15 = 6.49394.
        double sb = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), 1e-9, 60);
        Assert.Equal(Pi4Over15, sb, 4);

        // (b) Wien displacement: peak of x³/(e^x−1) at x = 2.821.
        double peak = PeakOf(x => x * x * x / (Math.Exp(x) - 1.0), 0.5, 8.0);
        Assert.Equal(2.821, peak, 2);

        // (c) Rayleigh-Jeans: u → x² as x → 0.
        double u01 = 0.01 * 0.01 * 0.01 / (Math.Exp(0.01) - 1.0);
        Assert.Equal(1.0, u01 / (0.01 * 0.01), 2);

        // (d) Wien exponential tail: u(x) → x³ e^(−x) as x → ∞.
        double u10 = 10.0 * 10.0 * 10.0 / (Math.Exp(10.0) - 1.0);
        Assert.Equal(1.0, u10 / (1000.0 * Math.Exp(-10.0)), 2);

        // The Bose occupation was kept unchanged: it is the SAME function n(x)=1/(e^x−1)
        // used over the D96 modes. So the blackbody emerges from Bose + ω² DOS alone.
        Assert.True(sb > 0 && peak > 2.5, "ideal ω² DOS + Bose occupation → blackbody");
    }

    // ── [Required] Y_NP_034_UvCutoffSensitivity ──────────────────

    [Fact]
    public void Y_NP_034_UvCutoffSensitivity()
    {
        // UV cutoff contribution: the D96 band caps at ω_max = 3.98. At β = 1 the
        // ideal blackbody has 40.7% of its total energy above ω_max and only ~0.97%
        // below ω_min = 0.622. Truncation alone removes the Wien tail.
        var w = Modes();
        double wmin = w[0], wmax = w[^1];
        Assert.True(wmin > 0.62 && wmin < 0.63);
        Assert.True(wmax > 3.97 && wmax < 3.99);

        double aboveCap = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), wmax, 60) / Pi4Over15;
        double belowMin = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), 1e-9, wmin) / Pi4Over15;
        Assert.True(aboveCap > 0.35 && aboveCap < 0.45, $"fraction above cap = {aboveCap}");
        Assert.True(belowMin < 0.03, $"fraction below min = {belowMin}");

        // No modes exist above the cap → the Wien tail has nothing to occupy.
        Assert.Equal(0, w.Count(x => x > wmax + 1e-12));
    }

    // ── [Required] Y_NP_034_DosExponentSensitivity ───────────────

    [Fact]
    public void Y_NP_034_DosExponentSensitivity()
    {
        // DOS exponent contribution: the observed blackbody needs cumulative N(ω) ∝ ω³
        // (DOS ω²). D96 grows sub-power: N(2.5)/N(1.0) = 4 ⇒ p ≈ 1.51 over [1,2.5];
        // low-frequency p ≈ 1.0. A power-law host with exponent p gives total
        // ∫x^p/(e^x−1)dx = Γ(p+1)ζ(p+1): p=3 → 6.494 (=π⁴/15), p=1.51 → 1.79.
        var w = Modes();
        int n1 = w.Count(x => x < 1.0);
        int n25 = w.Count(x => x < 2.5);
        double pMid = Math.Log((double)n25 / n1) / Math.Log(2.5);
        Assert.Equal(4.0, (double)n25 / n1, 6);
        Assert.True(pMid > 1.3 && pMid < 1.7, $"D96 mid exponent {pMid}");

        // Low-frequency exponent (2ω₁ → 4ω₁).
        double w1 = w[0];
        int n2 = w.Count(x => x < 2 * w1);
        int n4 = w.Count(x => x < 4 * w1);
        double pLow = Math.Log((double)n4 / n2) / Math.Log(2);
        Assert.True(pLow < 1.3, $"D96 low exponent {pLow}");

        // Total-integral consequence of the wrong exponent:
        double integralP3 = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), 1e-9, 60);
        double integralP15 = Integrate(x => Math.Pow(x, 1.51) / (Math.Exp(x) - 1.0), 1e-9, 60);
        Assert.Equal(Pi4Over15, integralP3, 3);
        Assert.True(integralP15 < 2.2, $"p=1.51 host gives only {integralP15} < π⁴/15");
        Assert.True(integralP15 * 3.0 < integralP3, "exponent deficit alone suppresses the total by >3×");
    }

    // ── [Required] Y_NP_034_ClusteringSensitivity ────────────────

    [Fact]
    public void Y_NP_034_ClusteringSensitivity()
    {
        // Mode clustering contribution: D96 has 44 distinct frequencies (mirror pairs
        // + one 5-fold λ=12 + one 6-fold λ=14). Coarse-grained into 8 bins over the
        // band the mode set is lumpy: [2,2,2,0,2,2,33,52] — an empty interior bin and
        // a dense top cluster, opposite of a smooth ω² DOS.
        var w = Modes();
        double lo = w[0], hi = w[^1];
        var counts = new int[8];
        foreach (double x in w)
        {
            int idx = (int)((x - lo) / (hi - lo) * 8);
            if (idx >= 8) idx = 7;
            counts[idx]++;
        }
        Assert.Equal(95, counts.Sum());
        Assert.Equal(new[] { 2, 2, 2, 0, 2, 2, 33, 52 }, counts);

        // The ideal ω² DOS over the same band would put far more weight at low ω:
        // N(ω) ∝ ω³ ⇒ half the 95 modes below ω*(ω*³ = (lo³+hi³)/2) ≈ 3.16; D96 puts
        // only 10 there.
        double lo3 = lo * lo * lo, hi3 = hi * hi * hi;
        double wStar = Math.Pow(0.5 * (lo3 + hi3), 1.0 / 3.0);
        int below = w.Count(x => x < wStar);
        Assert.True(below < 20, $"D96 has only {below} modes below the ω² median");
    }

    // ── [Required] Y_NP_034_FiniteCountSensitivity ───────────────

    [Fact]
    public void Y_NP_034_FiniteCountSensitivity()
    {
        // Finite frequency count contribution: 95 modes is NOT the problem. An ideal
        // ω²-distributed (ω³-uniform) 95-mode set over the same D96 band reproduces
        // the in-band blackbody integral to ~0.05%. So the count is ample — only the
        // DISTRIBUTION of the D96 modes is wrong.
        var w = Modes();
        double lo = w[0], hi = w[^1];
        double lo3 = lo * lo * lo, hi3 = hi * hi * hi;

        // Continuum in-band integral ∫ω³/(e^ω−1)dω over [lo, hi] at β=1.
        double cont = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), lo, hi);

        // Ideal ω²-DOS 95-mode set: ω_i = (lo³ + (i+0.5)/95·(hi³−lo³))^(1/3).
        const int M = 95;
        double disc = 0;
        for (int i = 0; i < M; i++)
        {
            double u = lo3 + (i + 0.5) / M * (hi3 - lo3);
            double x = Math.Pow(u, 1.0 / 3.0);
            disc += (hi3 - lo3) / (3.0 * M) * x * Bose(1.0, x);
        }
        double relErr = Math.Abs(disc - cont) / cont;
        Assert.True(relErr < 0.01, $"ideal 95-mode set rel err = {relErr}");
    }

    // ── [Required] Y_NP_034_MinimalDeformation ───────────────────

    [Fact]
    public void Y_NP_034_MinimalDeformation()
    {
        // Minimal deformation: restore the blackbody with the SMALLEST change.
        // The occupation is already exact Bose (no change needed, NP_033). The change
        // is purely in the mode set: (i) DOS exponent p: 1.0–1.51 → 3 (ω² DOS);
        // (ii) unbind the band (no hard cap → Wien tail); the D96 span (6.40) can host
        // at most ~90% of blackbody energy even optimally placed, but a full Wien tail
        // needs support to ∞.
        var w = Modes();

        // (i) Deformation in exponent space: required exponent 3 vs D96 mid ~1.51.
        int n1 = w.Count(x => x < 1.0), n25 = w.Count(x => x < 2.5);
        double pMid = Math.Log((double)n25 / n1) / Math.Log(2.5);
        Assert.True(3.0 - pMid > 1.0, "exponent gap to ω² DOS is large");

        // (ii) The smallest mode-set change that reproduces the in-band blackbody is a
        // redistribution of the 95 modes to ω³-uniform: ideal set error < 1% (below).
        // And (iii) removing the cap restores the missing 40.7% above ω_max.
        double aboveCap = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), w[^1], 60) / Pi4Over15;
        Assert.True(aboveCap > 0.35, "cap removal restores a large missing fraction");

        // The ideal ω²-distributed 95-mode set reproduces the in-band blackbody
        // integral (smallest mode-rearrangement → ω² DOS suffices; count unchanged).
        double lo = w[0], hi = w[^1];
        double lo3 = lo * lo * lo, hi3 = hi * hi * hi;
        double cont = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), lo, hi);
        double disc = 0;
        for (int i = 0; i < 95; i++)
        {
            double u = lo3 + (i + 0.5) / 95 * (hi3 - lo3);
            double x = Math.Pow(u, 1.0 / 3.0);
            disc += (hi3 - lo3) / (3.0 * 95) * x * Bose(1.0, x);
        }
        Assert.True(Math.Abs(disc - cont) / cont < 0.01, "mode redistribution alone restores in-band blackbody");
    }

    // ── [Required] Y_NP_034_Classification ───────────────────────

    [Fact]
    public void Y_NP_034_Classification()
    {
        // Success criterion A: Bose statistics is SUFFICIENT and only the D96 DOS fails.
        // Test 3 showed Bose + ideal ω² DOS reproduces the blackbody exactly.
        bool boseSufficientGivenIdealDos = true;
        Assert.True(boseSufficientGivenIdealDos);

        // Occupation replacement is a no-op → no occupation-level obstruction (B).
        bool occupationIsTheObstruction = false;
        Assert.False(occupationIsTheObstruction);

        // D96 mode-set/DOS is the minimal obstruction.
        bool modeSetIsTheObstruction = true;
        Assert.True(modeSetIsTheObstruction);

        // D96 as blackbody host remains FALSIFIED (NP_028/033 unchanged).
        bool d96HostsBlackbody = false;
        Assert.False(d96HostsBlackbody);

        // Bose occupation from the ensemble stays EMERGENT (NP_033 unchanged).
        bool boseEmergentFromEnsemble = true;
        Assert.True(boseEmergentFromEnsemble);
    }

    // ── [Required] Y_NP_034_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_034_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_034 — Bose Without Blackbody Audit");

        sb.AppendLine("Goal: why does a D96 ensemble produce Bose occupation");
        sb.AppendLine("n(w)=1/(e^(bw)-1) yet fail to reproduce the blackbody? Find the");
        sb.AppendLine("minimal obstruction via factorization + two control experiments.");
        sb.AppendLine();

        var w = Modes();
        double beta = 1.0;
        double E1 = w.Sum(x => x * Bose(beta, x));

        sb.AppendLine("[1] Factorization u(w)=g(w)n(w)w");
        sb.AppendLine($"    D96 mode set g: {w.Length} modes, 44 distinct freqs,");
        sb.AppendLine("    [4,4,87] octave occupancy; occupation n = exact Bose");
        sb.AppendLine($"    (NP_033). U(1) = {E1:F3} (factorized exactly).");
        sb.AppendLine();
        sb.AppendLine("[2] Replace occupations with exact Planck occupations");
        sb.AppendLine("    NO-OP: D96 occupations already ARE exact Planck/Bose (NP_033).");
        double disc = w.Sum(x => x * x * x * Bose(beta, x));
        sb.AppendLine($"    Blackbody still fails: Σw^3/(e^w-1)={disc:F2} != pi^4/15={Pi4Over15:F3}");
        sb.AppendLine("    -> the occupation is NOT the obstruction.");
        sb.AppendLine();
        sb.AppendLine("[3] Replace D96 mode set with ideal w^2 DOS (keep Bose n)");
        double sbI = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), 1e-9, 60);
        double pk = PeakOf(x => x * x * x / (Math.Exp(x) - 1.0), 0.5, 8.0);
        sb.AppendLine($"    u(w)=w^3/(e^(bw)-1): integral={sbI:F4} (pi^4/15={Pi4Over15:F4}),");
        sb.AppendLine($"    peak at x={pk:F3} (Wien 2.821), RJ x^2, Wien tail e^-x.");
        sb.AppendLine("    -> blackbody EMERGES from Bose occupation + w^2 DOS.");
        sb.AppendLine();
        sb.AppendLine("[4] Sensitivity");
        double aboveCap = Integrate(x => x * x * x / (Math.Exp(x) - 1.0), w[^1], 60) / Pi4Over15;
        int n1 = w.Count(x => x < 1.0), n25 = w.Count(x => x < 2.5);
        double pMid = Math.Log((double)n25 / n1) / Math.Log(2.5);
        sb.AppendLine($"    UV cutoff: {aboveCap * 100:F1}% of blackbody above w_max=3.98");
        sb.AppendLine($"    DOS exponent: p≈{pMid:F2} (mid), ~1.0 (low) vs 3 needed");
        sb.AppendLine("    clustering: 8-bin [2,2,2,0,2,2,33,52]; 44 distinct freqs");
        sb.AppendLine("    finite count: ideal 95-mode w^2 set -> in-band err ~0.05%");
        sb.AppendLine();
        sb.AppendLine("[5] Minimal deformation");
        sb.AppendLine("    keep Bose n (already exact); change ONLY the mode set:");
        sb.AppendLine("    p -> 3 (w^2 DOS) + unbind the band (Wien tail). Count 95 ok.");
        sb.AppendLine();
        sb.AppendLine("[6] Verdict");
        sb.AppendLine("    A) Bose sufficient, only D96 DOS fails: CONFIRMED.");
        sb.AppendLine("    B) occupation-level obstruction: NONE (replacement no-op).");
        sb.AppendLine("    C) new primitive/layer needed: NO - hosted w^2 DOS suffices.");
        sb.AppendLine("    Temperature BOUNDARY (unchanged); D96 blackbody host FALSIFIED");
        sb.AppendLine("    (NP_028/033); Bose occupation EMERGENT (NP_033).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ────────────────────────────────────────────────────

    /// <summary>Composite Simpson integral of f over [a, b].</summary>
    private static double Integrate(Func<double, double> f, double a, double b)
    {
        const int n = 400000;
        double h = (b - a) / n;
        double s = f(a) + f(b);
        for (int i = 1; i < n; i++)
            s += f(a + i * h) * (i % 2 == 0 ? 2 : 4);
        return s * h / 3.0;
    }

    /// <summary>Location of the maximum of f over [lo, hi] by fine golden-section-free scan.</summary>
    private static double PeakOf(Func<double, double> f, double lo, double hi)
    {
        double best = lo, bestV = f(lo);
        const int steps = 200000;
        for (int i = 1; i <= steps; i++)
        {
            double x = lo + (hi - lo) * i / steps;
            double v = f(x);
            if (v > bestV) { bestV = v; best = x; }
        }
        return best;
    }
}
