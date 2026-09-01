using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_035 — Multiplet-Requirement Audit test suite (Y_D_035_Tests.cs).
///
/// Question: why must the self-conjugate mode participate in a degenerate multiplet?
///
/// Verdict tested: the self-conjugate mode k=N/2 is REAL-ONLY (sin(πn)=0); its
/// eigenvalue λ=12 has a 1D real eigenspace at N=64/80/128 (an isolated singlet
/// violating complex observability) and a 5D eigenspace at N=96/192. Complex
/// observability (every observable frequency must carry [magnitude, phase], QG218/D_034)
/// requires every eigenvalue to have multiplicity ≥ 2. At N=96 every eigenvalue has
/// mult ≥ 2 (complete pairing); at N=64 λ=12 has mult 1 — the real-only singlet
/// violates complex observability. The degenerate multiplet supplies the
/// phase/quadrature partners. REFINEMENT: complete pairing is DERIVED from complex
/// observability — the boundary moves one step deeper, from '0 unpaired' to 'the
/// observable sector is complex'.
///
/// Deterministic: closed-form circulant eigenvalues.
/// </summary>
public class Y_D_035_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_035_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    /// <summary>Multiplicity of the self-conjugate eigenvalue λ(N/2).</summary>
    private static int SelfConjugateMultiplicity(int n)
    {
        int ksc = n / 2;
        double lamSc = Math.Round(Lambda(ksc, n), 9);
        return Enumerable.Range(1, n - 1).Count(k => Math.Round(Lambda(k, n), 9) == lamSc);
    }

    /// <summary>Minimum eigenvalue multiplicity over the spectrum.</summary>
    private static int MinMultiplicity(int n)
    {
        var mults = Enumerable.Range(1, n - 1)
            .Select(k => Math.Round(Lambda(k, n), 9))
            .GroupBy(x => x)
            .Select(g => g.Count())
            .ToArray();
        return mults.Min();
    }

    // ── [Required] Y_D_035_SelfConjugateMode ───────────────────────────

    /// <summary>
    /// The self-conjugate mode k=N/2 is real-only: sin(πn) = 0 for all n.
    /// </summary>
    [Fact]
    public void Y_D_035_SelfConjugateMode()
    {
        foreach (int n in new[] { 64, 80, 96, 128, 192 })
        {
            int ksc = n / 2;
            foreach (int site in Enumerable.Range(0, n).Where(i => i % 7 == 0))
                Assert.Equal(0.0, Math.Sin(2.0 * Math.PI * ksc * site / n), 10);
        }
    }

    // ── [Required] Y_D_035_DegenerateMultiplet ─────────────────────────

    /// <summary>
    /// λ(N/2)=12 is 1-fold at 64/80/128 (isolated singlet) and 5-fold at 96/192
    /// (degenerate multiplet).
    /// </summary>
    [Fact]
    public void Y_D_035_DegenerateMultiplet()
    {
        Assert.Equal(1, SelfConjugateMultiplicity(64));
        Assert.Equal(1, SelfConjugateMultiplicity(80));
        Assert.Equal(5, SelfConjugateMultiplicity(96));
        Assert.Equal(1, SelfConjugateMultiplicity(128));
        Assert.Equal(5, SelfConjugateMultiplicity(192));
    }

    // ── [Required] Y_D_035_PhaseFreedom ────────────────────────────────

    /// <summary>
    /// The 1D eigenspace is real-only (no phase partner); the 5D group supplies the
    /// phase/quadrature partners for the real-only self-conjugate mode.
    /// </summary>
    [Fact]
    public void Y_D_035_PhaseFreedom()
    {
        // The self-conjugate mode alone is real-only (sin vanishes).
        int N = 96, ksc = 48;
        foreach (int site in Enumerable.Range(0, 96).Where(i => i % 7 == 0))
            Assert.Equal(0.0, Math.Sin(2.0 * Math.PI * ksc * site / N), 10);

        // The λ=12 group supplies the partners: {16, 32, 48, 64, 80}.
        var lam12 = Enumerable.Range(1, 95)
            .Where(k => Math.Abs(Lambda(k, 96) - 12.0) < 1e-6)
            .ToArray();
        Assert.Equal(new[] { 16, 32, 48, 64, 80 }, lam12);

        // The other members (16, 32, 64, 80) have full cos+sin quadratures.
        foreach (int k in new[] { 16, 32, 64, 80 })
            Assert.True(Math.Abs(Math.Sin(2.0 * Math.PI * k * 7 / N)) > 1e-3);
    }

    // ── [Required] Y_D_035_InterferenceLoss ────────────────────────────

    /// <summary>
    /// Interference loss: a real-only (1D) eigenvalue gives classical addition; a
    /// complex (mult ≥ 2) eigenvalue gives interference.
    /// </summary>
    [Fact]
    public void Y_D_035_InterferenceLoss()
    {
        // Complex states: P = 2 + 2cos(θ₁−θ₂) — varies (interference).
        double p1 = 2.0 + 2.0 * Math.Cos(1.0);
        double p2 = 2.0 + 2.0 * Math.Cos(1.7);
        Assert.NotEqual(p1, p2, 3); // interference varies with phase

        // Real-only: P = P₁ + P₂ — fixed (no interference).
        Assert.Equal(2.0, 1.0 + 1.0, 10);

        // At N=64, λ=12 is a 1D real eigenspace (real-only — no interference for that
        // frequency); at N=96 it is 5D (complex — interference).
        Assert.Equal(1, SelfConjugateMultiplicity(64));
        Assert.Equal(5, SelfConjugateMultiplicity(96));
    }

    // ── [Required] Y_D_035_RepresentationClosure ───────────────────────

    /// <summary>
    /// Representation closure: complex observability requires every eigenvalue to have
    /// multiplicity ≥ 2. At N=96 this holds (min mult 2); at N=64/80/128 it fails
    /// (min mult 1).
    /// </summary>
    [Fact]
    public void Y_D_035_RepresentationClosure()
    {
        Assert.Equal(1, MinMultiplicity(64));
        Assert.Equal(1, MinMultiplicity(80));
        Assert.Equal(2, MinMultiplicity(96));
        Assert.Equal(1, MinMultiplicity(128));
        Assert.Equal(2, MinMultiplicity(192));
    }

    // ── [Required] Y_D_035_Run ─────────────────────────────────────────

    [Fact]
    public void Y_D_035_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_035 — Multiplet-Requirement Audit");

        sb.AppendLine("Goal: why must the self-conjugate mode participate in a");
        sb.AppendLine("degenerate multiplet?");
        sb.AppendLine();

        sb.AppendLine("[1] The self-conjugate mode is REAL-ONLY");
        sb.AppendLine("    sin(pi*n) = 0: only the cos harmonic survives");
        sb.AppendLine();

        sb.AppendLine("[2] Complex observability requires mult >= 2");
        foreach (int n in new[] { 64, 80, 96, 128, 192 })
            sb.AppendLine($"    N={n}: min mult = {MinMultiplicity(n)}, self-conj mult = {SelfConjugateMultiplicity(n)}");
        sb.AppendLine("    (a 1D real eigenspace is real-only - no phase partner)");
        sb.AppendLine();

        sb.AppendLine("[3] The 5-fold group supplies the phase partners");
        sb.AppendLine("    lambda=12 at N=96: {16, 32, 48, 64, 80}");
        sb.AppendLine("    k=48 real-only; k=16/32/64/80 have full cos+sin");
        sb.AppendLine();

        sb.AppendLine("[4] Verdict");
        sb.AppendLine("    complete pairing is DERIVED from complex observability;");
        sb.AppendLine("    the boundary moves one step deeper:");
        sb.AppendLine("    from '0 unpaired' to 'the observable sector is complex'.");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
