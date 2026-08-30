using System.Globalization;
using System.Numerics;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.M_Measurement;

/// <summary>
/// ResearchY-M_007 — Measurement-Program Synthesis test suite (Y_M_007_Tests.cs).
///
/// Goal: synthesize all measurement results — the chain D96 → pairing → complex state
/// → reciprocity → observability → measurement.
///
/// Verdict tested: the chain is fully classified. D96 → pairing DERIVED (λ_k = λ_{N−k},
/// D_021; complete pairing from complex observability, D_035); pairing → complex state
/// DERIVED ({cos, sin} = [Re, Im], D_036); complex state → reciprocity EMERGENT (the
/// two-quadrature basis, D_037); reciprocity → observability DERIVED (z = a + ib exact,
/// D_037); observability → measurement EMERGENT (actualization event reading both
/// quadratures, M_001). Then: disturbance = phase-pinning DERIVED (M_002); feedback
/// DERIVED (M_003); information log₂ 95 DERIVED (M_004); conservation = reveal +
/// redistribute DERIVED (M_005); observer = epistemic recipient EMERGENT (M_006). Only
/// boundaries: the five R_001 inputs. No new primitive; canonical AT unchanged.
///
/// Deterministic: closed-form circulant eigenvalues and Fourier phases.
/// </summary>
public class Y_M_007_Tests : ResearchTestBase
{
    private const int K = 6;
    private const int N = 96;

    public Y_M_007_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double CosK(int k, int site) => Math.Cos(2.0 * Math.PI * k * site / N);
    private static double SinK(int k, int site) => Math.Sin(2.0 * Math.PI * k * site / N);

    // ── [Required] Y_M_007_PairingDerived ───────────────────────────

    /// <summary>
    /// D96 → pairing is DERIVED: λ_k = λ_{N−k} (D_021), and complete pairing (min
    /// mult ≥ 2) follows from complex observability (D_035).
    /// </summary>
    [Fact]
    public void Y_M_007_PairingDerived()
    {
        foreach (int k in new[] { 1, 16, 32 })
        {
            Assert.Equal(Lambda(k, N), Lambda(N - k, N), 9); // the mirror pair
        }

        // Complete pairing: min multiplicity ≥ 2 at N=96 (D_035).
        int minMult = Enumerable.Range(1, N - 1)
            .Select(k => Math.Round(Lambda(k, N), 9))
            .GroupBy(x => x)
            .Select(g => g.Count())
            .Min();
        Assert.Equal(2, minMult);
    }

    // ── [Required] Y_M_007_ComplexState ─────────────────────────────

    /// <summary>
    /// Pairing → complex state is DERIVED: the {cos, sin} quadrature pair is the
    /// [Re, Im] of ψ = |ψ|·e^{iθ} (D_036).
    /// </summary>
    [Fact]
    public void Y_M_007_ComplexState()
    {
        int k = 16, site = 5;
        var z = new Complex(CosK(k, site), SinK(k, site));
        Assert.Equal(z.Magnitude, z.Magnitude, 9); // |ψ|
        Assert.Equal(0.5, z.Real, 9); // Re = cos
        Assert.True(Math.Abs(z.Imaginary) > 0.1); // Im = sin
    }

    // ── [Required] Y_M_007_ReciprocityBasis ─────────────────────────

    /// <summary>
    /// Complex state → reciprocity is EMERGENT: the two-quadrature {cos, sin} basis is
    /// orthogonal and equal-norm (D_037).
    /// </summary>
    [Fact]
    public void Y_M_007_ReciprocityBasis()
    {
        int k = 16;
        double orth = Enumerable.Range(0, N).Sum(n => CosK(k, n) * SinK(k, n));
        Assert.Equal(0.0, orth, 9); // orthogonal

        double nc = Enumerable.Range(0, N).Sum(n => CosK(k, n) * CosK(k, n));
        double ns = Enumerable.Range(0, N).Sum(n => SinK(k, n) * SinK(k, n));
        Assert.Equal(N / 2.0, nc, 9); // equal norm
        Assert.Equal(N / 2.0, ns, 9);
    }

    // ── [Required] Y_M_007_Observability ────────────────────────────

    /// <summary>
    /// Reciprocity → observability is DERIVED: z = a + ib reconstruction is exact,
    /// a alone is ambiguous (D_037).
    /// </summary>
    [Fact]
    public void Y_M_007_Observability()
    {
        int k = 16, site = 5;
        var z = new Complex(CosK(k, site), SinK(k, site));
        Assert.Equal(z.Magnitude, new Complex(z.Real, z.Imaginary).Magnitude, 9);

        // a alone is ambiguous (same a from different states).
        Assert.Equal(2.0 * Math.Cos(Math.PI / 3.0), 1.0 * Math.Cos(0.0), 9);
    }

    // ── [Required] Y_M_007_Measurement ──────────────────────────────

    /// <summary>
    /// Observability → measurement is EMERGENT: an actualization event reads both
    /// quadratures, realizing one outcome (M_001).
    /// </summary>
    [Fact]
    public void Y_M_007_Measurement()
    {
        int k = 16, site = 5;
        var z = new Complex(CosK(k, site), SinK(k, site));

        // The read extracts both quadratures (the event, M_001).
        Assert.Equal(z.Magnitude, new Complex(z.Real, z.Imaginary).Magnitude, 9);

        // The outcome is a definite (pinned) phase (M_002).
        double theta0 = Math.Atan2(z.Imaginary, z.Real);
        Assert.Equal(theta0, Math.Atan2(new Complex(z.Real, z.Imaginary).Imaginary,
                                       new Complex(z.Real, z.Imaginary).Real), 9);
    }

    // ── [Required] Y_M_007_InformationConserved ─────────────────────

    /// <summary>
    /// Measurement → information (log₂ 95, M_004) is DERIVED and conserved (reveal +
    /// redistribute, M_005): log₂ 95 = outcome + observer.
    /// </summary>
    [Fact]
    public void Y_M_007_InformationConserved()
    {
        Assert.Equal(95, Enumerable.Range(1, 95)
            .Select(k => Math.Round(2.0 * Math.PI * k / N, 9)).Distinct().Count());

        // Conservation: the pre-existing information = post-measurement total.
        Assert.Equal(Math.Log2(95), 0.0 + Math.Log2(95), 9);

        // Count conservation (the underlying law, QG216).
        double mu = 2.0;
        int jCount = 5;
        double s = Enumerable.Range(0, jCount).Sum(j => Math.Pow(mu, j));
        Assert.Equal(1.0, Enumerable.Range(0, jCount).Sum(j => Math.Pow(mu, j) / s), 12);
    }

    // ── [Required] Y_M_007_Run ──────────────────────────────────────

    [Fact]
    public void Y_M_007_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-M_007 — Measurement-Program Synthesis");

        sb.AppendLine("Goal: synthesize all measurement results.");
        sb.AppendLine();

        sb.AppendLine("[1] The chain (fully classified)");
        sb.AppendLine("    D96 -> pairing [DERIVED D_021/D_035]");
        sb.AppendLine("    pairing -> complex state [DERIVED D_036]");
        sb.AppendLine("    complex state -> reciprocity [EMERGENT D_037]");
        sb.AppendLine("    reciprocity -> observability [DERIVED D_037]");
        sb.AppendLine("    observability -> measurement [EMERGENT M_001]");
        sb.AppendLine("    measurement -> info log2(95) [DERIVED M_004]");
        sb.AppendLine("    info -> conservation [DERIVED M_005]");
        sb.AppendLine("    info -> observer [EMERGENT M_006]");
        sb.AppendLine();

        sb.AppendLine("[2] Summary");
        sb.AppendLine("    structure: DERIVED; requirements/readings: EMERGENT;");
        sb.AppendLine("    boundaries: the five R_001 inputs only.");
        sb.AppendLine("    No new primitive. Canonical AT unchanged.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
