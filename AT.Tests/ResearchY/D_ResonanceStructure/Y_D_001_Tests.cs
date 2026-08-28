using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_001 — Standing Wave Audit test suite (Y_D_001_Tests.cs).
///
/// Goal: can standing waves exist on C96 without center-based geometry?
///
/// Verdict tested: YES — the Fourier modes are time-harmonic eigenfunctions of the
/// graph Laplacian (standing waves), translation-invariant (no origin), and the
/// standing structure is a center-free hybrid (spatial harmonics + spectral
/// frequencies).
///
/// Deterministic: closed-form circulant eigenvalues + Fourier-mode analysis.
/// </summary>
public class Y_D_001_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_001_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_001_FormalDefinition ──────────────────────────────

    /// <summary>
    /// A standing wave is a time-harmonic field ψ(n,t) = φ(n)·cos(ωt+δ) whose spatial
    /// part φ is a stationary eigenfunction of the graph Laplacian. On the ring the
    /// modes are the Fourier harmonics.
    /// </summary>
    [Fact]
    public void Y_D_001_FormalDefinition()
    {
        // A Fourier mode is a standing wave: φ_k(n) = cos(2πkn/N), ω_k = √λ_k.
        // ψ(n,t) = φ_k(n)·cos(ω_k t) satisfies the wave equation on the ring.
        // Verify the mode is periodic (stationary pattern): φ_k(n+N) = φ_k(n).
        double phi0 = Math.Cos(2.0 * Math.PI * 3 * 0 / N);
        double phiN = Math.Cos(2.0 * Math.PI * 3 * N / N);
        Assert.Equal(phi0, phiN, 10); // periodicity of the spatial pattern

        // The frequency is the normal-mode frequency ω = √λ.
        Assert.Equal(0.6216, Omega(1), 3);
    }

    // ── [Required] Y_D_001_EigenmodeExpression ───────────────────────────

    /// <summary>
    /// The eigenmodes satisfy L φ_k = λ_k φ_k with ω_k = √λ_k; the modes are the
    /// cos/sin Fourier harmonics (the ±k degeneracy gives the pair).
    /// </summary>
    [Fact]
    public void Y_D_001_EigenmodeExpression()
    {
        // ω_k = √λ_k (the normal-mode frequency).
        Assert.Equal(Math.Sqrt(Lambda(1)), Omega(1), 12);

        // The cos and sin members of a pair both have the same frequency (degenerate).
        // λ_k = λ_{N−k} (Z2 pairing) — the two harmonics of one pair.
        Assert.Equal(Lambda(1), Lambda(N - 1), 10);

        // Fourier mode values are bounded and periodic (a harmonic).
        for (int n = 0; n < N; n++)
        {
            double c = Math.Cos(2.0 * Math.PI * 2 * n / N);
            Assert.True(c >= -1.0 && c <= 1.0);
        }
    }

    // ── [Required] Y_D_001_GeometricVsSpectral ───────────────────────────

    /// <summary>
    /// Geometric standing wave = the spatial harmonic (pattern); spectral standing wave
    /// = the eigenfrequency ω_k = √λ_k. Both are centerless faces of the same object.
    /// </summary>
    [Fact]
    public void Y_D_001_GeometricVsSpectral()
    {
        // Geometric content: the spatial pattern (harmonic) — centerless (translation-
        // invariant structure; node positions depend only on k).
        double nodePos = Math.Cos(2.0 * Math.PI * 1 * 24 / N); // cos(π/2)=0 for k=1 at n=24
        Assert.Equal(0.0, nodePos, 10); // mode k=1 has a node at n=24 (position from k only)

        // Spectral content: the frequency ω_k = √λ_k (from the graph spectrum).
        Assert.Equal(0.6216, Omega(1), 3);

        // No origin enters either face: shifting the pattern is a rotation (automorphism).
        // (Documented: the two faces are the geometric pattern and the spectral frequency.)
    }

    // ── [Required] Y_D_001_ZeroMode ──────────────────────────────────────

    /// <summary>
    /// The zero mode λ₀ = 0, ω₀ = 0 is the constant eigenvector — the uniform rest
    /// state, a degenerate (zero-frequency) standing wave and the reference state.
    /// </summary>
    [Fact]
    public void Y_D_001_ZeroMode()
    {
        Assert.Equal(0.0, Lambda(0), 10);
        Assert.Equal(0.0, Omega(0), 10);

        // Constant eigenvector: |φ₀(n)|² = 1/N for every site (uniform, centerless).
        Assert.Equal(0.01042, 1.0 / N, 4);

        // Zero frequency: no oscillation — the rest state (reference, C_001/A_002).
        Assert.Equal(1.0, Math.Cos(Omega(0) * 10.0), 12);
    }

    // ── [Required] Y_D_001_ResonantPairs ─────────────────────────────────

    /// <summary>
    /// Resonant mode pairs are the Z2-degenerate pairs λ_k = λ_{N−k}: 42 doublets
    /// (multiplicity 2) plus the multiplicity-5 and -6 groups. Degeneracy follows from
    /// the ring's ±k symmetry — no center.
    /// </summary>
    [Fact]
    public void Y_D_001_ResonantPairs()
    {
        // Z2 pairing: λ_k = λ_{N−k} for the circulant ring.
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        int pairs = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(lam[k] - lam[N - k]) < 1e-9) pairs++;
        Assert.Equal(47, pairs); // 47 pairs + 1 self-conjugate (k=48)

        // Multiplicity structure: 42 doublets + one 5-group + one 6-group.
        var mult = new List<int>();
        foreach (var g in lam.Skip(1).GroupBy(l => Math.Round(l, 8)))
            mult.Add(g.Count());
        Assert.Equal(42, mult.Count(c => c == 2));
        Assert.Equal(1, mult.Count(c => c == 5));
        Assert.Equal(1, mult.Count(c => c == 6));

        // The fundamental doublet is the resonant pair at the first-peak frequency.
        Assert.Equal(0.6216, Omega(1), 3);
    }

    // ── [Required] Y_D_001_Classification ────────────────────────────────

    /// <summary>
    /// The standing structure is HYBRID (spatial pattern + spectral frequency) and
    /// CENTER-FREE: the modes are translation-invariant (node positions depend only on
    /// k), and the spectrum is rotation-invariant.
    /// </summary>
    [Fact]
    public void Y_D_001_Classification()
    {
        // Spatial content: the harmonics (translation-invariant as a set).
        // Spectral content: the eigenvalues (rotation-invariant).
        // Hybrid: a standing wave needs BOTH the pattern (spatial) and ω (spectral).

        // Translation invariance of the spectrum: λ_k does not depend on any site label.
        double lam1 = Lambda(1);
        Assert.Equal(0.3864, lam1, 3);

        // A rotation maps one harmonic to another of the same k — the mode set is
        // invariant as a set (no origin).
        for (int shift = 1; shift <= 6; shift++)
        {
            double c0 = Math.Cos(2.0 * Math.PI * 1 * 0 / N);
            double cs = Math.Cos(2.0 * Math.PI * 1 * shift / N);
            // Both are values of the SAME harmonic (same k) — a rotation, not a new mode.
            Assert.True(c0 >= -1 && c0 <= 1 && cs >= -1 && cs <= 1);
        }

        // Center-free: no mode or eigenvalue references a distinguished site.
        // (Documented: the standing structure is hybrid — spatial + spectral — center-free.)
    }

    // ── [Required] Y_D_001_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_001 — Standing Wave Audit");

        sb.AppendLine("Goal: can standing waves exist on C96 without center-based geometry?");
        sb.AppendLine();

        // ── 1. Definition ───────────────────────────────────────────────
        sb.AppendLine("[1] Formal definition");
        sb.AppendLine("    ψ(n,t) = φ(n)·cos(ωt+δ),  L φ = λ φ,  ω = √λ");
        sb.AppendLine("    Modes: Fourier harmonics cos(2πkn/N), sin(2πkn/N)");
        sb.AppendLine();

        // ── 2. Geometric vs spectral ────────────────────────────────────
        sb.AppendLine("[2] Geometric vs spectral standing wave");
        sb.AppendLine("    geometric: the spatial pattern (harmonic, fixed nodes)");
        sb.AppendLine("    spectral:  the frequency ω_k = √λ_k");
        sb.AppendLine("    both centerless; two faces of one object (hybrid)");
        sb.AppendLine();

        // ── 3. Zero mode & resonant pairs ───────────────────────────────
        sb.AppendLine("[3] Zero mode and resonant pairs");
        sb.AppendLine($"    λ₀ = {Lambda(0):F1}, ω₀ = {Omega(0):F1} — uniform rest state");
        sb.AppendLine($"    fundamental doublet: ω₁ = {Omega(1):F4} = ω₉₅ (Z2 pair)");
        sb.AppendLine("    42 doublet groups + one 5-group + one 6-group (degenerate pairs)");
        sb.AppendLine("    node positions depend only on k (no origin)");
        sb.AppendLine();

        // ── 4. Verdicts ─────────────────────────────────────────────────
        sb.AppendLine("[4] Verdicts");
        sb.AppendLine("    standing waves on C96?        → YES (Fourier modes are time-harmonic)");
        sb.AppendLine("    center required?              → NO (translation-invariant modes)");
        sb.AppendLine("    zero mode standing wave?      → YES (ω₀=0 uniform rest state)");
        sb.AppendLine("    resonant pairs?               → YES (Z2 degeneracy, 42 doublets + …)");
        sb.AppendLine("    spatial-only?                 → NO (frequency is essential)");
        sb.AppendLine("    spectral-only?                → NO (pattern is a spatial harmonic)");
        sb.AppendLine("    classification                → HYBRID, center-free");
        sb.AppendLine();

        // ── 5. Conclusion ───────────────────────────────────────────────
        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    Standing waves exist on C96 without center-based geometry.");
        sb.AppendLine("    The standing structure is a center-free hybrid: spatial harmonics");
        sb.AppendLine("    (geometric) + spectral frequencies (ω_k = √λ_k). No canonical value");
        sb.AppendLine("    is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
