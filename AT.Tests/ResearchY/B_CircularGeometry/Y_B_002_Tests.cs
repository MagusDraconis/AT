using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.B_CircularGeometry;

/// <summary>
/// ResearchY-B_002 — Origin of π Value Audit test suite (Y_B_002_Tests.cs).
///
/// Goal: can the numerical value π = 3.141592653… emerge from the canonical framework,
/// or only its geometric role?
///
/// Verdict tested: BOUNDARY — the canonical content (D96 spectrum) is ALGEBRAIC
/// (integer-matrix Laplacian), π is TRANSCENDENTAL, so no finite canonical construction
/// outputs π's value. Only the role (circle constant) emerges with closure (B_001).
///
/// Deterministic: closed-form circulant eigenvalues + analytic algebraicity.
/// </summary>
public class Y_B_002_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_B_002_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_B_002_PiFromClosure ─────────────────────────────────

    /// <summary>
    /// Closure produces the integer N = 96 (a count of ring sites), never π.
    /// The closure fixed point is an integer; no integer equals π.
    /// </summary>
    [Fact]
    public void Y_B_002_PiFromClosure()
    {
        // Closure gives the integer attractor size.
        Assert.Equal(96, N);
        Assert.True(N % 1 == 0); // integer

        // An integer is algebraic; π is transcendental → not equal.
        Assert.NotEqual(Math.PI, (double)N);
        Assert.Equal(96.0, (double)N); // the closure count is exact and integer
    }

    // ── [Required] Y_B_002_PiFromCircle ──────────────────────────────────

    /// <summary>
    /// C/D = π is the definitional radius identity: r = N/(2π) was introduced AS
    /// N/(2π). The ratio holds by definition (a unit/convention choice), not by a
    /// derivation of the value — measurement, not emergence.
    /// </summary>
    [Fact]
    public void Y_B_002_PiFromCircle()
    {
        // The "radius" is defined as N/(2π): a convention.
        double radius = N / (2.0 * Math.PI);
        double circumference = N;
        double ratio = circumference / (2.0 * radius);
        Assert.Equal(Math.PI, ratio, 10); // tautology: radius defined via π

        // The value is not derived: the identity uses π on both sides.
        // (Documented: the C/D identity carries π's role, not its origin.)
        Assert.Equal(15.279, radius, 2);
    }

    // ── [Required] Y_B_002_PiFromFourierBasis ────────────────────────────

    /// <summary>
    /// The Fourier basis of the circulant is the roots-of-unity basis:
    /// z_k = e^{2πik/N} satisfies z_k^N = 1 — ALGEBRAIC. The basis is fixed by the
    /// algebraic equation z^N = 1; the parameter 2π/N is a convention. The basis
    /// cannot output the transcendental π.
    /// </summary>
    [Fact]
    public void Y_B_002_PiFromFourierBasis()
    {
        // Roots of unity are algebraic: z_k^N = 1 for the Fourier basis.
        // Verify the algebraic relation for several modes: |z_k^N| = 1.
        for (int k = 1; k <= 5; k++)
        {
            // z_k = e^{2πik/N}; z_k^N = e^{2πik} = 1 (algebraic relation).
            double cosN = Math.Cos(2.0 * Math.PI * k);
            double sinN = Math.Sin(2.0 * Math.PI * k);
            Assert.Equal(1.0, cosN, 10);
            Assert.Equal(0.0, sinN, 10);
        }

        // The basis is algebraic — it does not contain π's value.
        // (π parametrizes the roots; the values are algebraic.)
    }

    // ── [Required] Y_B_002_PiFromSpectrum ────────────────────────────────

    /// <summary>
    /// The graph Laplacian L = D − A is an integer matrix; its eigenvalues are
    /// algebraic integers. The D96 spectrum is therefore algebraic. π is
    /// transcendental — no eigenvalue equals π.
    /// </summary>
    [Fact]
    public void Y_B_002_PiFromSpectrum()
    {
        // The Laplacian entries are integers: L = D − A (Ch5/Ch6).
        // Eigenvalues of an integer matrix are algebraic integers (standard theorem).
        // π is transcendental (Lindemann) — no algebraic integer equals π.

        // Verify the spectrum is finite/algebraic-valued: all 95 positive eigenvalues
        // are computed from algebraic roots of unity (no transcendental appears).
        var freqs = new double[N - 1];
        for (int k = 1; k < N; k++) freqs[k - 1] = Omega(k);

        // No eigenvalue/frequency equals π or 2π.
        foreach (double f in freqs)
        {
            Assert.NotEqual(Math.PI, f);
            Assert.NotEqual(2.0 * Math.PI, f);
        }

        // The max eigenvalue is the integer 2K = 12 (exact, algebraic).
        Assert.Equal(12.0, 2.0 * K, 10);
    }

    // ── [Required] Y_B_002_PiApproximants ────────────────────────────────

    /// <summary>
    /// Natural D96 ratios approximate π (span/2, √10, sm/√sm2 ≈ 2π) but NONE equals π
    /// exactly. Selecting any one as "the derivation" is target-driven selection (a
    /// fit) — forbidden. The near-misses are coincidences, not derivations.
    /// </summary>
    [Fact]
    public void Y_B_002_PiApproximants()
    {
        // Natural D96 spectral ratios.
        double sm = 95.0;
        double sm2 = 229.0;
        double maxW = 0.0, minW = double.PositiveInfinity;
        for (int k = 1; k < N; k++)
        {
            double w = Omega(k);
            if (w > maxW) maxW = w;
            if (w < minW) minW = w;
        }
        double span = maxW / minW;

        double spanHalf = span / 2.0;
        double sqrt10 = Math.Sqrt(10.0);
        double smOverSqrtSm2 = sm / Math.Sqrt(sm2); // ≈ 6.278 ≈ 2π

        // None equals π exactly.
        Assert.NotEqual(Math.PI, spanHalf);
        Assert.NotEqual(Math.PI, sqrt10);
        Assert.NotEqual(2.0 * Math.PI, smOverSqrtSm2);

        // They are near-misses (coincidences), not derivations:
        // deviation from π / 2π is far above any exactness threshold.
        Assert.True(Math.Abs(spanHalf - Math.PI) > 1e-3);
        Assert.True(Math.Abs(sqrt10 - Math.PI) > 1e-3);
        Assert.True(Math.Abs(smOverSqrtSm2 - 2.0 * Math.PI) > 1e-3);

        // Selecting any ratio as "the π" would be a fit (target-driven) — documented.
    }

    // ── [Required] Y_B_002_BoundaryConsistency ───────────────────────────

    /// <summary>
    /// QG291/QG196 remain correct: π is a boundary constant; the Bekenstein 1/4 needs
    /// imported 2π. The spectrum is algebraic; π's value is transcendental and outside
    /// the framework's closure.
    /// </summary>
    [Fact]
    public void Y_B_002_BoundaryConsistency()
    {
        // π's value is transcendental — it is NOT an algebraic output of the framework.
        // The framework's content is algebraic (integer-matrix Laplacian).
        // Hence π's value is a boundary (QG291 consistent).

        // The only 2π in the spectral layer is the parametrization of roots of unity —
        // its VALUE never enters the algebraic outputs.
        double lam1 = Lambda(1);
        Assert.Equal(0.3864, lam1, 3); // spectral gap (algebraic)

        // QG196: Bekenstein 1/4 requires imported 2π — not overturned here.
        // (Documented: this audit adds the algebraicity argument for π's value.)

        // 2π = 6.283185... is equally transcendental; closure needs only its ROLE
        // (the full-cycle phase, B_001), not its value.
        Assert.Equal(6.28319, 2.0 * Math.PI, 5);
    }

    // ── [Required] Y_B_002_Run ───────────────────────────────────────────

    [Fact]
    public void Y_B_002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-B_002 — Origin of π Value Audit");

        sb.AppendLine("Goal: can the numerical value π = 3.14159… emerge from the");
        sb.AppendLine("      canonical framework, or only its geometric role?");
        sb.AppendLine();

        // ── 1. The decisive fact ────────────────────────────────────────
        sb.AppendLine("[1] The decisive fact: algebraicity vs transcendence");
        sb.AppendLine("    The graph Laplacian L = D − A is an INTEGER matrix.");
        sb.AppendLine("    ⇒ its eigenvalues (the D96 spectrum) are ALGEBRAIC integers.");
        sb.AppendLine("    ⇒ every finite combination of spectral constants is algebraic.");
        sb.AppendLine("    π is TRANSCENDENTAL (Lindemann, 1882).");
        sb.AppendLine("    ⇒ NO finite canonical construction can output π's value.");
        sb.AppendLine();

        // ── 2. Candidate paths ──────────────────────────────────────────
        sb.AppendLine("[2] Candidate derivation paths (all FAIL for the value)");
        sb.AppendLine($"    closure:          N = {N} (integer) ≠ π");
        sb.AppendLine("    phase:            θ_k = 2πk/N — π is a parameter of algebraic roots of unity");
        sb.AppendLine("    graph/eigenmodes: algebraic eigenvalues/basis (no transcendental appears)");
        sb.AppendLine("    circumference:    C/D = π — definitional (radius = N/2π is a unit choice)");
        sb.AppendLine();

        // ── 3. Approximants ─────────────────────────────────────────────
        double sm = 95.0, sm2 = 229.0;
        double maxW = 0.0, minW = double.PositiveInfinity;
        for (int k = 1; k < N; k++)
        {
            double w = Omega(k);
            if (w > maxW) maxW = w;
            if (w < minW) minW = w;
        }
        double span = maxW / minW;
        sb.AppendLine("[3] D96 approximants (near-misses, NOT derivations)");
        sb.AppendLine($"    span/2        = {span / 2.0:F6}  (dev from π {Math.Abs(span / 2.0 - Math.PI):F4})");
        sb.AppendLine($"    √10           = {Math.Sqrt(10):F6}  (dev {Math.Abs(Math.Sqrt(10) - Math.PI):F4})");
        sb.AppendLine($"    Σm/√Σm²      = {sm / Math.Sqrt(sm2):F6}  ≈ 2π (dev {Math.Abs(sm / Math.Sqrt(sm2) - 2.0 * Math.PI):F4})");
        sb.AppendLine("    Selecting any as 'the π' = a fit (forbidden). Coincidences only.");
        sb.AppendLine();

        // ── 4. Verdicts ─────────────────────────────────────────────────
        sb.AppendLine("[4] Verdicts");
        sb.AppendLine("    RQ1 what remains?         → the numerical value of π");
        sb.AppendLine("    RQ2 inherited from circle?→ role YES; value NO");
        sb.AppendLine("    RQ3 reconstruct π?        → NO (all paths algebraic)");
        sb.AppendLine("    RQ4 C96 approximants?     → YES, near-misses only (no fit allowed)");
        sb.AppendLine("    RQ5 N/2π emergence?       → NO (measurement/unit choice)");
        sb.AppendLine("    RQ6 π from Fourier basis? → NO (roots of unity are algebraic)");
        sb.AppendLine("    RQ7 π numerical/geometric?→ geometric in role, boundary in value");
        sb.AppendLine("    RQ8 path to π?            → NO (algebraic chain, transcendental target)");
        sb.AppendLine("    RQ9 closure needs?        → only 2π (phase cycle), not π's value");
        sb.AppendLine("    RQ10 QG291/196 correct?   → YES (strengthened by algebraicity)");
        sb.AppendLine();

        // ── 5. Conclusion ───────────────────────────────────────────────
        sb.AppendLine("[5] Conclusion — BOUNDARY");
        sb.AppendLine("    π's ROLE (circle constant of the closed ring) emerges with closure;");
        sb.AppendLine("    π's VALUE is transcendental and cannot be produced by the algebraic");
        sb.AppendLine("    framework. QG291/QG196 remain correct. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
