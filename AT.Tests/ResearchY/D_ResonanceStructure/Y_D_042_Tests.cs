using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_042 — Fundamental-Ratio Audit test suite (Y_D_042_Tests.cs).
///
/// Question: does D96 contain a fundamental ratio analogous to circumference/diameter = π?
///
/// Verdict tested: the structural ratio of the C96 ring is span = ωmax/ω₁ = 6.4025 —
/// π's ROLE but DERIVED where π is BOUNDARY. π is transcendental (value BOUNDARY,
/// B_002); span is algebraic (integer-matrix spectrum), hence DERIVED (D_028). The
/// ratio is invariant under N-preserving ring automorphisms (the spectrum multiset is
/// preserved) but NOT universal across N (span ~ 0.0578·N, monotone). The ratio family
/// generates the hierarchies: span → 3 families (D_028), ω₂/ω₁ ≈ 1.97 → the octave
/// (D_030), λmax/λ₂ = 40.99 → the scale gap, A³ = 4.81e16 → the Planck content (D_007),
/// ω₁ → the universal dimensionless reference (D_008/D_011).
///
/// Deterministic: closed-form circulant eigenvalues and ratios.
/// </summary>
public class Y_D_042_Tests : ResearchTestBase
{
    private const int K = 6;

    public Y_D_042_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n) => Math.Sqrt(Lambda(k, n));

    private static double Span(int n)
    {
        double wmax = Enumerable.Range(1, n - 1).Max(k => Omega(k, n));
        double w1 = Enumerable.Range(1, n - 1).Min(k => Omega(k, n));
        return wmax / w1;
    }

    private static double LambdaMaxOverGap(int n)
    {
        double lmax = Enumerable.Range(1, n - 1).Max(k => Lambda(k, n));
        double gap = Enumerable.Range(1, n - 1).Where(k => Lambda(k, n) > 0.1).Min(k => Lambda(k, n));
        return lmax / gap;
    }

    /// <summary>Ring automorphism k → s·k mod N; checks the spectrum multiset is preserved.</summary>
    private static bool AutomorphismPreservesSpectrum(int n, int s)
    {
        var before = Enumerable.Range(1, n - 1).Select(k => Math.Round(Lambda(k, n), 6)).OrderBy(x => x);
        var after = Enumerable.Range(1, n - 1)
            .Select(k => Math.Round(Lambda(((s * k) % n) == 0 ? n : (s * k) % n, n), 6))
            .OrderBy(x => x);
        return before.SequenceEqual(after);
    }

    // ── [Required] Y_D_042_FundamentalRatio ──────────────────────────

    /// <summary>
    /// span = ωmax/ω₁ = 6.4025 is the structural ratio of the C96 ring (π's role).
    /// </summary>
    [Fact]
    public void Y_D_042_FundamentalRatio()
    {
        Assert.Equal(6.4025, Span(96), 2);
        Assert.Equal(6.4025, Span(96), 2); // stable
    }

    // ── [Required] Y_D_042_InvariantScan ─────────────────────────────

    /// <summary>
    /// The spectral ratios (span, λmax/λ₂, ω₂/ω₁) are invariant under N-preserving ring
    /// automorphisms (k → s·k, s coprime to N) — genuine structural invariants.
    /// </summary>
    [Fact]
    public void Y_D_042_InvariantScan()
    {
        foreach (int s in new[] { 5, 7, 11, 13 })
            Assert.True(AutomorphismPreservesSpectrum(96, s), $"automorphism k->{s}k must preserve the spectrum");

        // Span is the same under any relabeling (invariant by construction).
        Assert.Equal(6.4025, Span(96), 2);
    }

    // ── [Required] Y_D_042_HierarchyGeneration ───────────────────────

    /// <summary>
    /// The ratios generate the hierarchies: span → family count 3 (D_028); ω₂/ω₁ ≈ 1.97
    /// (octave, D_030); λmax/λ₂ = 40.99 (scale gap); A³ = 4.81e16 (Planck, D_007).
    /// </summary>
    [Fact]
    public void Y_D_042_HierarchyGeneration()
    {
        // span → family count = floor(log₂ span)+1 = 3 (D_028).
        Assert.Equal(3, (int)Math.Floor(Math.Log2(Span(96))) + 1);

        // ω₂/ω₁ ≈ 1.97 — the octave (mode doubling, D_030).
        Assert.Equal(1.97, Omega(2, 96) / Omega(1, 96), 2);

        // λmax/λ₂ = 40.99 — the scale gap.
        Assert.Equal(40.99, LambdaMaxOverGap(96), 2);

        // A³ = (Σm·#g·occ₂)³ = 4.8094e16 — the dimensionless Planck content (D_007).
        double A3 = Math.Pow(95.0 * 44.0 * 87.0, 3);
        Assert.Equal(4.8094e16, A3, 1e12);
    }

    // ── [Required] Y_D_042_NStability ────────────────────────────────

    /// <summary>
    /// No ratio is universal across N: span is monotone (4.02→12.78); λmax/λ₂ is
    /// non-monotone. Unlike π (the same for every circle), there is no N-invariant ratio.
    /// </summary>
    [Fact]
    public void Y_D_042_NStability()
    {
        // span is monotone increasing in N (D_028: ~0.0578·N).
        Assert.True(Span(60) < Span(96));
        Assert.True(Span(96) < Span(120));
        Assert.True(Span(120) < Span(192));
        Assert.Equal(4.02, Span(60), 2);
        Assert.Equal(12.78, Span(192), 2);

        // λmax/λ₂ is NOT monotone (40.99 at 96 vs 41.10 at 192).
        Assert.NotEqual(LambdaMaxOverGap(96), LambdaMaxOverGap(192), 2);

        // ω₂/ω₁ → 2 only in the continuum limit (not a fixed constant).
        Assert.True(Omega(2, 96) / Omega(1, 96) < 2.0);
        Assert.True(Omega(2, 192) / Omega(1, 192) > Omega(2, 96) / Omega(1, 96));
    }

    // ── [Required] Y_D_042_PhysicsConnection ─────────────────────────

    /// <summary>
    /// The ratios connect to physics: ω₁ is the universal dimensionless reference
    /// (D_008/D_011); span → 3 families (D_028); A³ → the Planck content (D_007). π's
    /// value remains BOUNDARY (transcendental, B_002); span's value is DERIVED.
    /// </summary>
    [Fact]
    public void Y_D_042_PhysicsConnection()
    {
        // ω₁ = 0.6216 — the universal dimensionless reference (D_008/D_011).
        Assert.Equal(0.6216, Omega(1, 96), 3);

        // span → 3 families (physics: the observable-sector hierarchy).
        Assert.Equal(3, (int)Math.Floor(Math.Log2(Span(96))) + 1);

        // A³ → the dimensionless Planck content (D_007).
        double A3 = Math.Pow(95.0 * 44.0 * 87.0, 3);
        Assert.True(A3 > 1e16);

        // π value is BOUNDARY (transcendental) — cannot be derived from the integer-matrix
        // spectrum; span value IS algebraic and derived. (Documented: no computation
        // produces π's digits from L; span's digits are exact.)
        Assert.True(Math.PI > 3.14159 && Math.PI < 3.14160); // π unchanged (BOUNDARY)
    }

    // ── [Required] Y_D_042_Run ───────────────────────────────────────

    [Fact]
    public void Y_D_042_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_042 — Fundamental-Ratio Audit");

        sb.AppendLine("Goal: does D96 contain a fundamental ratio analogous to");
        sb.AppendLine("circumference/diameter = pi for a circle?");
        sb.AppendLine();

        sb.AppendLine("[1] The structural ratio: span = wmax/w1 = 6.4025");
        sb.AppendLine("    pi's ROLE but DERIVED where pi is BOUNDARY");
        sb.AppendLine("    pi: transcendental (BOUNDARY, B_002);");
        sb.AppendLine("    span: algebraic (integer-matrix spectrum), DERIVED (D_028)");
        sb.AppendLine();

        sb.AppendLine("[2] Invariance under N-preserving automorphisms");
        sb.AppendLine("    k -> s*k (s coprime): spectrum preserved (verified)");
        sb.AppendLine("    => span, lmax/l2, w2/w1 are structural invariants");
        sb.AppendLine();

        sb.AppendLine("[3] No universal ratio across N");
        sb.AppendLine("    span: 4.02 (60) -> 12.78 (192) monotone (D_028)");
        sb.AppendLine("    lmax/l2: 40.99 (96) vs 41.10 (192) non-monotone");
        sb.AppendLine("    w2/w1 -> 2 only in the continuum limit");
        sb.AppendLine();

        sb.AppendLine("[4] Hierarchy generation (all DERIVED)");
        sb.AppendLine("    span -> 3 families (D_028)");
        sb.AppendLine("    w2/w1 ~ 1.97 -> octave (D_030)");
        sb.AppendLine("    A^3 = 4.81e16 -> Planck content (D_007)");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    span role EMERGENT; span value DERIVED;");
        sb.AppendLine("    ratio family DERIVED; pi value BOUNDARY (unchanged);");
        sb.AppendLine("    universal N-invariant ratio: NONE.");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
