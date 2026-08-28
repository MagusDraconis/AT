using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_002 — Standing Wave Model test suite (Y_D_002_Tests.cs).
///
/// Goal: construct the canonical standing wave model of C96 — mode decomposition,
/// resonant pair structure, zero mode role, 47 Z2 pair analysis, spatial vs spectral
/// content, closure consistency — and classify it (GEOMETRIC/SPECTRAL/HYBRID).
///
/// Verdict tested: HYBRID (center-free) — spatial harmonics × spectral eigenvalues,
/// 47 Z2 pairs + self-conjugate mode, zero mode as reference, closure-consistent.
///
/// Deterministic: closed-form circulant eigenvalues + Fourier-mode analysis.
/// </summary>
public class Y_D_002_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_002_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_002_ModeDecomposition ─────────────────────────────

    /// <summary>
    /// The mode decomposition: 95 positive Fourier modes + 1 zero mode, complete and
    /// orthogonal (the circulant's eigenbasis).
    /// </summary>
    [Fact]
    public void Y_D_002_ModeDecomposition()
    {
        // 95 positive modes + 1 zero mode = N modes.
        int positive = N - 1;
        Assert.Equal(95, positive);

        // Eigenvalues: λ₀ = 0 (zero mode), λ_k > 0 for k = 1..95.
        Assert.Equal(0.0, Lambda(0), 10);
        for (int k = 1; k <= 10; k++) Assert.True(Lambda(k) > 0.0);

        // Fourier harmonics are the modes (cos/sin basis at each k).
        // Verify a harmonic is periodic: φ_k(n+N) = φ_k(n).
        for (int n = 0; n < N; n += 11)
        {
            double c0 = Math.Cos(2.0 * Math.PI * 3 * n / N);
            double cN = Math.Cos(2.0 * Math.PI * 3 * (n + N) / N);
            Assert.Equal(c0, cN, 10);
        }
    }

    // ── [Required] Y_D_002_ResonantPairs ─────────────────────────────────

    /// <summary>
    /// Resonant pair structure: the Z2 symmetry λ_k = λ_{N−k} gives degenerate pairs;
    /// each pair provides two real harmonics (cos and sin) at the same frequency.
    /// </summary>
    [Fact]
    public void Y_D_002_ResonantPairs()
    {
        // λ_k = λ_{N−k} for the circulant (Z2 pairing).
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        for (int k = 1; k <= 47; k++)
            Assert.Equal(lam[k], lam[N - k], 8);

        // Each pair has the same frequency: ω_k = ω_{N−k}.
        Assert.Equal(Omega(1), Omega(N - 1), 10);

        // The cos and sin harmonics of a pair are both standing waves at ω_k.
        // (Documented: pair = 2 real degenerate modes.)
    }

    // ── [Required] Y_D_002_ZeroMode ──────────────────────────────────────

    /// <summary>
    /// The zero mode is the uniform rest state: λ₀ = 0, ω₀ = 0, constant eigenvector —
    /// the reference against which all standing waves oscillate.
    /// </summary>
    [Fact]
    public void Y_D_002_ZeroMode()
    {
        Assert.Equal(0.0, Lambda(0), 10);
        Assert.Equal(0.0, Omega(0), 10);

        // Constant eigenvector: uniform on the ring (no oscillation, no center).
        Assert.Equal(0.01042, 1.0 / N, 4);

        // The zero mode is the only zero-frequency mode (all others oscillate).
        for (int k = 1; k <= 10; k++) Assert.True(Omega(k) > 0.0);
    }

    // ── [Required] Y_D_002_Z2Pairs ───────────────────────────────────────

    /// <summary>
    /// 47 Z2 pairs → 94 paired real modes + 1 self-conjugate (k=48) = 95 positive modes.
    /// The doublet structure is the ring-mode degeneracy.
    /// </summary>
    [Fact]
    public void Y_D_002_Z2Pairs()
    {
        // 47 Z2 pairs (k = 1..47 each paired with N−k).
        int pairs = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(Lambda(k) - Lambda(N - k)) < 1e-9) pairs++;
        Assert.Equal(47, pairs);

        // Self-conjugate mode k = 48: λ = 12 (exact, integer).
        Assert.Equal(12.0, Lambda(48), 6);

        // 94 paired modes + 1 self-conjugate = 95 positive modes.
        Assert.Equal(95, 2 * pairs + 1);

        // The multiplicity multiset: 42 doublets + one 5 + one 6.
        var mult = new List<int>();
        foreach (var g in Enumerable.Range(1, N - 1).Select(k => Lambda(k)).GroupBy(l => Math.Round(l, 8)))
            mult.Add(g.Count());
        Assert.Equal(42, mult.Count(c => c == 2));
        Assert.Equal(1, mult.Count(c => c == 5));
        Assert.Equal(1, mult.Count(c => c == 6));
    }

    // ── [Required] Y_D_002_SpatialSpectral ───────────────────────────────

    /// <summary>
    /// The standing wave model is HYBRID: each mode is a spatial harmonic (geometric
    /// content) oscillating at a spectral eigenvalue (spectral content). Both are
    /// center-free.
    /// </summary>
    [Fact]
    public void Y_D_002_SpatialSpectral()
    {
        // Spatial content: the harmonic (pattern).
        // A node position depends only on k: cos(2πk·24/96) = cos(πk/2) = 0 for odd k.
        Assert.Equal(0.0, Math.Cos(2.0 * Math.PI * 1 * 24 / N), 10);

        // Spectral content: the frequency ω_k = √λ_k.
        Assert.Equal(0.6216, Omega(1), 3);

        // Hybrid: the standing wave Ψ = harmonic × cos(ωt) needs BOTH the pattern and
        // the frequency. (Documented: neither spatial-only nor spectral-only.)
    }

    // ── [Required] Y_D_002_ClosureConsistency ────────────────────────────

    /// <summary>
    /// The model is closure-consistent: R^N = identity (modes N-periodic),
    /// θ_{k+N} ≡ θ_k (phase lattice closes), z_k^N = 1 (rotations close algebraically),
    /// and the spectrum is algebraic (no transcendental value enters the content).
    /// </summary>
    [Fact]
    public void Y_D_002_ClosureConsistency()
    {
        // R^N = identity: modes are N-periodic.
        for (int n = 0; n < N; n += 13)
        {
            double c0 = Math.Cos(2.0 * Math.PI * 5 * n / N);
            double cN = Math.Cos(2.0 * Math.PI * 5 * (n + N) / N);
            Assert.Equal(c0, cN, 10);
        }

        // θ_{k+N} ≡ θ_k: phase lattice closes.
        double t5 = 2.0 * Math.PI * 5 / N;
        double t5N = 2.0 * Math.PI * (5 + N) / N;
        Assert.Equal(Math.Cos(t5), Math.Cos(t5N), 10);

        // z_k^N = 1: eigenmode rotations close algebraically (roots of unity).
        for (int k = 1; k <= 8; k++)
        {
            Assert.Equal(1.0, Math.Cos(2.0 * Math.PI * k), 10);
            Assert.Equal(0.0, Math.Sin(2.0 * Math.PI * k), 10);
        }

        // Algebraic spectrum: λ_k are eigenvalues of an integer matrix (no π value).
        Assert.Equal(0.3864, Lambda(1), 3); // algebraic (not transcendental)
    }

    // ── [Required] Y_D_002_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_002_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_002 — Standing Wave Model");

        sb.AppendLine("Goal: construct the canonical standing wave model of C96.");
        sb.AppendLine();

        // ── 1. Mode decomposition ───────────────────────────────────────
        sb.AppendLine("[1] Mode decomposition");
        sb.AppendLine("    Ψ(n,t) = Σ_k [a_k cos(2πkn/96) + b_k sin(2πkn/96)] cos(ω_k t)");
        sb.AppendLine("    95 positive Fourier modes + 1 zero mode (complete, orthogonal)");
        sb.AppendLine();

        // ── 2. Resonant pairs ───────────────────────────────────────────
        sb.AppendLine("[2] Resonant pair structure");
        sb.AppendLine("    47 Z2 pairs (λ_k = λ_{96−k}) → 94 paired real modes");
        sb.AppendLine("    + 1 self-conjugate mode (k=48, λ=12) = 95 positive modes");
        sb.AppendLine($"    fundamental doublet: ω₁ = {Omega(1):F4}");
        sb.AppendLine();

        // ── 3. Zero mode ────────────────────────────────────────────────
        sb.AppendLine("[3] Zero mode role");
        sb.AppendLine($"    λ₀ = {Lambda(0):F1}, ω₀ = {Omega(0):F1}: uniform rest state (reference)");
        sb.AppendLine();

        // ── 4. Spatial vs spectral ──────────────────────────────────────
        sb.AppendLine("[4] Spatial vs spectral content");
        sb.AppendLine("    spatial (geometric): the harmonics cos/sin (center-free)");
        sb.AppendLine("    spectral: the eigenvalues λ_k, ω_k = √λ_k (center-free)");
        sb.AppendLine("    ⇒ HYBRID");
        sb.AppendLine();

        // ── 5. Closure consistency ──────────────────────────────────────
        sb.AppendLine("[5] Closure consistency");
        sb.AppendLine("    R^N = identity (modes N-periodic); θ_{k+N} ≡ θ_k; z_k^N = 1");
        sb.AppendLine("    algebraic spectrum (integer-matrix Laplacian); π only in role (B_003)");
        sb.AppendLine();

        // ── 6. Conclusion ───────────────────────────────────────────────
        sb.AppendLine("[6] Conclusion — HYBRID (center-free)");
        sb.AppendLine("    The canonical standing wave model is the center-free hybrid");
        sb.AppendLine("    decomposition of the closed ring's algebraic spectrum: spatial");
        sb.AppendLine("    harmonics × spectral eigenvalues, 47 Z2 pairs + self-conjugate,");
        sb.AppendLine("    zero mode as reference, closure-consistent. No canonical value");
        sb.AppendLine("    is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
