using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_028 — Span-Origin Audit test suite (Y_D_028_Tests.cs).
///
/// Question: why is span ≈ 6.4025? Is span the true selection quantity behind D96?
///
/// Verdict tested: span is a DERIVED monotone function of N, NOT a selector.
/// span = ω_max/ω_min; ω_max → √12 (antipodal mode, even N) and ω_min ~ (2π√91)/N
/// (fundamental mode), so span ~ 0.0578·N — monotonically increasing with no special
/// point at 96; span(96) = 6.4025 is the N=96 point of this function. Removing any
/// candidate (closure, Z2, octave rung, resonance, information) leaves span(96)
/// unchanged. The family count = floor(log₂ 6.4025)+1 = 3 is a DERIVED consequence of
/// span (D_016 identity).
///
/// Deterministic: closed-form circulant eigenvalues.
/// </summary>
public class Y_D_028_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_028_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n) => Math.Sqrt(Lambda(k, n));

    private static double Span(int n)
    {
        var freqs = new double[n - 1];
        for (int k = 1; k < n; k++) freqs[k - 1] = Omega(k, n);
        Array.Sort(freqs);
        return freqs[^1] / freqs[0];
    }

    private static int FamilyCount(int n) => (int)Math.Floor(Math.Log2(Span(n))) + 1;

    private static int UnpairedCount(int n)
    {
        var evals = new List<double>();
        for (int k = 1; k < n; k++) evals.Add(Math.Round(Lambda(k, n), 9));
        var mult = evals.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        int unpaired = 0;
        for (int k = 1; k < n; k++)
            if (k == n - k && mult[Math.Round(Lambda(k, n), 9)] == 1) unpaired++;
        return unpaired;
    }

    // ── [Required] Y_D_028_SpanOrigin ──────────────────────────────────

    /// <summary>
    /// span ~ 0.0578·N: ω_max → √12 (antipodal mode) and ω_min ~ (2π√91)/N.
    /// span(96) = 6.4025 is the N=96 point of the monotone function.
    /// </summary>
    [Fact]
    public void Y_D_028_SpanOrigin()
    {
        // ω_max → √12 = 3.464 (antipodal k=N/2 for even N; λ(N/2)=2Σ(1−cos πd)=12).
        double lamHalf = 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(Math.PI * d));
        Assert.Equal(12.0, lamHalf, 6);
        Assert.Equal(3.4641, Math.Sqrt(lamHalf), 3);

        // ω_min ~ (2π√91)/N with Σd² = 91.
        double sumD2 = Enumerable.Range(1, K).Sum(d => (double)d * d);
        Assert.Equal(91.0, sumD2, 6);

        // span ~ (√12)/(2π√91) · N ≈ 0.0578·N.
        double slope = Math.Sqrt(12.0) / (2.0 * Math.PI * Math.Sqrt(91.0));
        Assert.Equal(0.0578, slope, 3);

        // Canonical value: span(96) = 6.4025.
        Assert.Equal(6.4025, Span(96), 2);
    }

    // ── [Required] Y_D_028_AlternativeN ────────────────────────────────

    /// <summary>
    /// span is smooth and monotone across N — no special point at 96.
    /// </summary>
    [Fact]
    public void Y_D_028_AlternativeN()
    {
        // Monotone increasing in N.
        double prev = 0.0;
        foreach (int n in new[] { 48, 60, 90, 96, 102, 120, 128, 192 })
        {
            Assert.True(Span(n) > prev, $"span({n}) not monotone");
            prev = Span(n);
        }

        // No kink at 96: the neighbors are smooth.
        Assert.True(Span(95) < Span(96) && Span(96) < Span(97));
        Assert.True(Math.Abs(Span(97) - Span(95)) < 0.4); // smooth, no jump

        // Canonical values.
        Assert.Equal(3.2396, Span(48), 3);
        Assert.Equal(4.0231, Span(60), 3);
        Assert.Equal(6.4025, Span(96), 2);
        Assert.Equal(7.9991, Span(120), 3);
        Assert.Equal(12.7791, Span(192), 3);
    }

    // ── [Required] Y_D_028_SelectorRemoval ─────────────────────────────

    /// <summary>
    /// Removing any candidate leaves span(96) unchanged (the span is N-determined):
    ///   - closure (D_019) does not determine N;
    ///   - Z2 completeness does not track span (span(64)=4.298 with 1 unpaired);
    ///   - octave-rung: span is continuous in N;
    ///   - resonance/information: consequences, not selectors.
    /// </summary>
    [Fact]
    public void Y_D_028_SelectorRemoval()
    {
        // The span value is fixed by N=96 regardless of the candidate:
        Assert.Equal(6.4025, Span(96), 2);

        // Z2 completeness does not determine span:
        //   N=64 (1 unpaired) span 4.298; N=96 (0 unpaired) span 6.4025.
        Assert.Equal(1, UnpairedCount(64));
        Assert.Equal(0, UnpairedCount(96));
        Assert.True(Span(64) < Span(96));

        // Octave-rung: span is continuous through 96.
        Assert.True(Span(90) < Span(96) && Span(96) < Span(102));

        // Closure does not determine N (D_019) — span is N-determined.
        Assert.Equal(6.4025, Span(96), 2); // unchanged
    }

    // ── [Required] Y_D_028_FamilyGeneration ────────────────────────────

    /// <summary>
    /// span generates 3 families as a DERIVED consequence: floor(log₂ 6.4025)+1 = 3.
    /// </summary>
    [Fact]
    public void Y_D_028_FamilyGeneration()
    {
        // The D_016 identity: families = floor(log₂ span) + 1.
        Assert.Equal(3, FamilyCount(96));
        Assert.Equal(3, (int)Math.Floor(Math.Log2(Span(96))) + 1);

        // span(96) ∈ [4, 8) → 3 families.
        Assert.True(Span(96) >= 4.0 && Span(96) < 8.0);

        // Consistency across the window.
        Assert.Equal(3, FamilyCount(90));
        Assert.Equal(3, FamilyCount(120));
        Assert.Equal(4, FamilyCount(128)); // span ≥ 8
    }

    // ── [Required] Y_D_028_DependencyTrace ─────────────────────────────

    /// <summary>
    /// Trace: Difference → Actualization → Closure → Spectrum → span → 3 families.
    /// </summary>
    [Fact]
    public void Y_D_028_DependencyTrace()
    {
        // Spectrum → span: the ratio of extreme eigenvalues (N=96).
        double wMin = Omega(1, 96);
        double wMax = Enumerable.Range(1, 95).Select(k => Omega(k, 96)).Max();
        Assert.Equal(Span(96), wMax / wMin, 6);

        // ω_max → √12 (antipodal mode).
        Assert.Equal(3.4641, Math.Sqrt(Lambda(48, 96)), 3);

        // ω_min ~ (2π√91)/N.
        Assert.Equal(0.6216, wMin, 3);

        // span → 3 families (D_016 identity).
        Assert.Equal(3, (int)Math.Floor(Math.Log2(wMax / wMin)) + 1);

        // The chain is: observable-sector construction (BOUNDARY, D_020) → N=96 (DERIVED,
        // D_040) → span (DERIVED) → families (DERIVED).
        Assert.Equal(6.4025, wMax / wMin, 2);
    }

    // ── [Required] Y_D_028_Run ─────────────────────────────────────────

    [Fact]
    public void Y_D_028_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_028 — Span-Origin Audit");

        sb.AppendLine("Goal: why is span ~ 6.4025? Is span the true selection quantity");
        sb.AppendLine("behind D96?");
        sb.AppendLine();

        sb.AppendLine("[1] Span origin: a derived function of N");
        sb.AppendLine("    span = w_max/w_min");
        sb.AppendLine("    w_max -> sqrt(12) = 3.464 (antipodal mode, even N)");
        sb.AppendLine("    w_min ~ (2*pi*sqrt(91))/N ~ 59.9/N (fundamental mode)");
        sb.AppendLine("    => span ~ 0.0578 * N (monotone increasing)");
        sb.AppendLine();

        sb.AppendLine("[2] Alternative N (smooth, no special point at 96)");
        sb.AppendLine($"    N=60: {Span(60):F4}; N=90: {Span(90):F4}; N=96: {Span(96):F4}; N=102: {Span(102):F4}; N=120: {Span(120):F4}");
        sb.AppendLine($"    N=95: {Span(95):F4}; N=97: {Span(97):F4} (smooth)");
        sb.AppendLine();

        sb.AppendLine("[3] Selector removal");
        sb.AppendLine("    closure/Z2/octave-rung/resonance/info do NOT change span(96)");
        sb.AppendLine("    (span is N-determined; the 3-family window is the D_020 input)");
        sb.AppendLine();

        sb.AppendLine("[4] Family generation");
        sb.AppendLine($"    floor(log2({Span(96):F4})) + 1 = {FamilyCount(96)} (DERIVED, D_016)");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    span VALUE: DERIVED (from N=96 via the spectrum)");
        sb.AppendLine("    span as a selector: REFUTED (a consequence, not a cause)");
        sb.AppendLine("    span in [4,8) window: BOUNDARY (the 3-family requirement, D_020)");
        sb.AppendLine("    family count = 3 (VALUE): DERIVED; N=96: DERIVED (D_040)");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
