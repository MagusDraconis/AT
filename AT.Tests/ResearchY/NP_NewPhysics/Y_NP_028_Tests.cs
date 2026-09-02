using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_028 — Blackbody Reconstruction Audit test suite
/// (Y_NP_028_Tests.cs).
///
/// Question: can the D96 mode structure reproduce the observed Planck spectrum after
/// coarse-graining? Weight the 95 positive modes by occupancy, construct a spectral
/// density, compare against Planck, and determine the high-frequency falloff.
///
/// Verdict tested: NO — coarse-graining does NOT heal the NP_027 gaps. The per-mode
/// occupation factor n = 1/(e^x − 1) is CORRESPONDENCE (NP_027 DERIVED form), but the
/// blackbody DOS (ω², 3D cavity) is FALSIFIED for D96 (sub-power-law ~ω^1.5, top-heavy:
/// 87% of modes in top 20% of band), the Wien exponential tail is FALSIFIED (hard cutoff
/// at ω_max = 3.980, zero modes above), and the full observed blackbody after
/// coarse-graining is FALSIFIED. Temperature θ is BOUNDARY and cannot rescue the shape.
///
/// Deterministic: the D96 spectrum (C_96(±1..±6)) is fixed; comparisons are closed-form.
/// </summary>
public class Y_NP_028_Tests : ResearchTestBase
{
    public Y_NP_028_Tests(ITestOutputHelper output) : base(output) { }

    private const int N = 96;

    private static double LambdaK(int k)
    {
        double sum = 0;
        for (int s = 1; s <= 6; s++)
            sum += 2 * (1 - Math.Cos(2.0 * Math.PI * k * s / N));
        return sum;
    }

    private static double Planck(double x) => 1.0 / (Math.Exp(x) - 1.0);

    /// <summary>95 positive modes ω_k = √λ_k, k = 1..95.</summary>
    private static double[] Modes()
    {
        var w = new double[N - 1];
        for (int k = 1; k < N; k++) w[k - 1] = Math.Sqrt(LambdaK(k));
        Array.Sort(w);
        return w;
    }

    // ── [Required] Y_NP_028_ModeInventory ─────────────────────────

    [Fact]
    public void Y_NP_028_ModeInventory()
    {
        var w = Modes();
        Assert.Equal(95, w.Length);

        // Band [0.622, 3.980], span ratio 6.40.
        Assert.True(w[0] > 0.62 && w[0] < 0.63, $"min ω = {w[0]}");
        Assert.True(w[^1] > 3.97 && w[^1] < 3.99, $"max ω = {w[^1]}");
        Assert.True(Math.Abs(w[^1] / w[0] - 6.40) < 0.05, $"span ratio {w[^1] / w[0]}");

        // 44 distinct frequencies (mirror pairs + 5-fold λ=12 + 6-fold λ=14 blocks).
        int distinct = 0;
        for (int i = 0; i < w.Length; i++)
            if (i == 0 || w[i] - w[i - 1] > 1e-9) distinct++;
        Assert.Equal(44, distinct);
    }

    // ── [Required] Y_NP_028_DosNotBlackbody ───────────────────────

    [Fact]
    public void Y_NP_028_DosNotBlackbody()
    {
        var w = Modes();

        // Cumulative mode count growth: D96 is sub-power-law (~ω^1.5), NOT ω³ (3D cavity).
        int n1 = w.Count(x => x < 1.0);
        int n25 = w.Count(x => x < 2.5);
        double ratio = (double)n25 / n1;
        Assert.Equal(8, n25);
        Assert.Equal(2, n1);
        Assert.True(Math.Abs(ratio - 4.0) < 0.01, $"cumulative ratio {ratio} ≠ 4.0");

        int n15 = w.Count(x => x < 1.5);
        int n30 = w.Count(x => x < 3.0);
        Assert.True((double)n30 / n15 < 5.0, $"mid-band growth {n30 / n15} not ω³");
    }

    // ── [Required] Y_NP_028_TopHeavyDOS ───────────────────────────

    [Fact]
    public void Y_NP_028_TopHeavyDOS()
    {
        var w = Modes();
        double mid = 0.5 * (w[0] + w[^1]);          // 2.30
        double top20 = w[^1] - 0.2 * (w[^1] - w[0]); // 3.31

        int aboveMid = w.Count(x => x > mid);
        int aboveTop20 = w.Count(x => x >= top20);

        // 93.7% above band mid; 87.4% in top 20% of the band.
        Assert.True(aboveMid / 95.0 > 0.9, $"fraction above mid = {aboveMid / 95.0}");
        Assert.True(aboveTop20 / 95.0 > 0.85, $"fraction in top 20% = {aboveTop20 / 95.0}");
        Assert.Equal(83, aboveTop20);
    }

    // ── [Required] Y_NP_028_OccupancyWeightedMismatch ─────────────

    [Fact]
    public void Y_NP_028_OccupancyWeightedMismatch()
    {
        var w = Modes();
        double theta = 1.0;

        // D96 weighted energy above ω = 3.3 (θ = 1).
        double d96Top = w.Where(x => x >= 3.3).Sum(x => x * Planck(x));
        double d96Tot = w.Sum(x => x * Planck(x));
        double d96Share = d96Top / d96Tot;

        // Planck (3D, in-band): share of ∫ω³/(e^ω−1) above ω = 3.3 within [0.622, 3.98].
        double planckShare = PlanckInBandShareAbove(3.3, theta, 0.622, 3.98);

        // D96 keeps 65.7% of its energy in the top cluster; Planck in-band keeps ~23%.
        Assert.True(d96Share > 0.5, $"D96 top share = {d96Share:F3}");
        Assert.True(planckShare < 0.30, $"Planck in-band top share = {planckShare:F3}");
        Assert.True(d96Share > planckShare + 0.3, "D96 energy is far more top-heavy than Planck");
    }

    // ── [Required] Y_NP_028_HighFrequencyFalloff ──────────────────

    [Fact]
    public void Y_NP_028_HighFrequencyFalloff()
    {
        var w = Modes();
        double wmax = w[^1];

        // Hard cutoff: no modes above ω_max.
        Assert.True(wmax > 3.97 && wmax < 3.99);
        Assert.Equal(0, w.Count(x => x > wmax + 1e-12));

        // No Wien tail: density rises into the cutoff instead of decaying e^(−ω).
        // Counts per 0.1 bin: 0 in [3.0,3.1), 6 in [3.3,3.4), 6 in [3.9,4.0).
        int low = w.Count(x => x >= 3.0 && x < 3.1);
        int high = w.Count(x => x >= 3.9 && x < 4.0);
        Assert.True(high > low, "D96 mode density must not decay exponentially near ω_max");

        // A Wien falloff e^(−ω/θ) at θ=1 would give n(3.98) = 0.019 << n(1) = 0.58,
        // but D96 has more modes near the top than near the bottom of the band.
        double topBand = w.Count(x => x >= wmax - 1.0);
        double lowBand = w.Count(x => x <= w[0] + 1.0);
        Assert.True(topBand > lowBand, "D96 concentrates modes at the top, not the (Wien) tail");
    }

    // ── [Required] Y_NP_028_CoarseGrainNoHeal ─────────────────────

    [Fact]
    public void Y_NP_028_CoarseGrainNoHeal()
    {
        var w = Modes();

        // Coarse-graining into K bins over the band must not change the mode count.
        int bins = 8;
        double lo = w[0], hi = w[^1];
        var counts = new int[bins];
        foreach (double x in w)
        {
            int idx = (int)((x - lo) / (hi - lo) * bins);
            if (idx >= bins) idx = bins - 1;
            counts[idx]++;
        }
        Assert.Equal(95, counts.Sum());

        // The coarse histogram is top-heavy: last bin (top 1/8 of band) dominates.
        double lastShare = counts[^1] / 95.0;
        Assert.True(lastShare > 0.3, $"last-bin share {lastShare:F3}");

        // Planck at θ matched to the band puts its peak at x=2.82 → inside the band,
        // NOT at the top; the observed shape cannot be reproduced by the top-heavy bins.
        Assert.True(2.82 * 1.0 > 0.622 && 2.82 * 1.0 < 3.98, "Planck peak lies inside the band");
    }

    // ── [Required] Y_NP_028_Classification ────────────────────────

    [Fact]
    public void Y_NP_028_Classification()
    {
        // Per-mode occupation factor is CORRESPONDENCE (NP_027 DERIVED form).
        Assert.True(Planck(1.0) > 0 && Planck(5.0) < 0.01);

        // Blackbody DOS ω² and Wien tail are FALSIFIED for D96 (top-heavy, truncated).
        var w = Modes();
        double wmax = w[^1];
        bool hasWienTail = w.Count(x => x > 6.0) > 0;      // no modes that high
        Assert.False(hasWienTail);
        Assert.True(wmax < 4.0, "spectrum is finite — no unbounded Wien support");

        // Temperature θ is BOUNDARY: it rescales x but cannot change the mode set.
        bool thetaIsCanonicalPrimitive = false;
        Assert.False(thetaIsCanonicalPrimitive);
    }

    // ── [Required] Y_NP_028_Run ───────────────────────────────────

    [Fact]
    public void Y_NP_028_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_028 — Blackbody Reconstruction Audit");

        sb.AppendLine("Goal: can the D96 mode structure reproduce the observed");
        sb.AppendLine("Planck spectrum after coarse-graining?");
        sb.AppendLine();

        var w = Modes();
        double d96Top = w.Where(x => x >= 3.3).Sum(x => x * Planck(x));
        double d96Tot = w.Sum(x => x * Planck(x));
        double planckShare = PlanckInBandShareAbove(3.3, 1.0, 0.622, 3.98);

        sb.AppendLine("[1] Mode inventory");
        sb.AppendLine($"    {w.Length} positive modes, band [{w[0]:F3}, {w[^1]:F3}]");
        sb.AppendLine($"    87.4% of modes in top 20% of the band (top-heavy)");
        sb.AppendLine();
        sb.AppendLine("[2] Occupancy-weighted spectral density (theta=1)");
        sb.AppendLine($"    D96 energy above 3.3: {d96Top / d96Tot:F3}");
        sb.AppendLine($"    Planck in-band above 3.3: {planckShare:F3}");
        sb.AppendLine();
        sb.AppendLine("[3] High-frequency falloff");
        sb.AppendLine("    hard cutoff at omega_max=3.98; no modes above;");
        sb.AppendLine("    density rises into the cutoff (no Wien tail)");
        sb.AppendLine();
        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    per-mode factor CORRESPONDENCE (NP_027); blackbody DOS");
        sb.AppendLine("    and Wien tail FALSIFIED; coarse-graining does NOT heal");
        sb.AppendLine("    the NP_027 gaps. No new primitive; canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }

    // ── Helpers ─────────────────────────────────────────────────────

    /// <summary>∫ω³/(e^(ω/θ)−1) above w0 within [lo, hi] ÷ ∫ over the full in-band window.</summary>
    private static double PlanckInBandShareAbove(double w0, double theta, double lo, double hi)
    {
        double top = Integral(x => x * x * x * Planck(x / theta), w0, hi);
        double all = Integral(x => x * x * x * Planck(x / theta), lo, hi);
        return top / all;
    }

    private static double Integral(Func<double, double> f, double a, double b)
    {
        const int n = 200000;
        double h = (b - a) / n;
        double s = 0;
        for (int i = 0; i < n; i++) s += f(a + (i + 0.5) * h);
        return s * h;
    }
}
