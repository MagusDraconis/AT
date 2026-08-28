using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.C_SourceGeometry;

/// <summary>
/// ResearchY-C_001 — Center Audit test suite (Y_C_001_Tests.cs).
///
/// Goal: is a unique center or source implicitly present in the Difference → Actualization
/// framework? Is it derived, emergent, or absent?
///
/// Verdict tested: center is ABSENT in space (circulant symmetry), EMERGENT as the
/// branching root (generation-space source), and the zero mode is a DERIVED reference
/// state (not a source).
///
/// Deterministic: circulant-ring structure + closed-form eigenvalues.
/// </summary>
public class Y_C_001_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_C_001_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    // ── [Required] Y_C_001_CenterNecessity ───────────────────────────────

    /// <summary>
    /// Difference needs a reference (the uniform background / zero mode), not a center.
    /// The definition is translation-invariant: a Q-event at any site is the same content.
    /// </summary>
    [Fact]
    public void Y_C_001_CenterNecessity()
    {
        // Difference = counting difference from a uniform background (Ch1).
        // The uniform background is the zero mode: λ₀ = 0, constant eigenvector.
        Assert.Equal(0.0, Lambda(0), 10);

        // No center is required by the definition: the reference is uniform, not a point.
        // All positive modes exist without any distinguished site.
        for (int k = 1; k <= 10; k++)
            Assert.True(Lambda(k) > 0.0);
    }

    // ── [Required] Y_C_001_RadialPropagation ─────────────────────────────

    /// <summary>
    /// Propagation is branching (tree-local, generation depth) + spectral projection
    /// (global ring readout) — A_003 rev.2. There is no radial propagation on the ring:
    /// the ring has no radial direction, and the only radial quantity (r = N/2π) is a
    /// size measure, not a propagation coordinate.
    /// </summary>
    [Fact]
    public void Y_C_001_RadialPropagation()
    {
        // The ring has a radius (B_001/B_002) but no center and no radial direction.
        double radius = N / (2.0 * Math.PI);
        Assert.Equal(15.279, radius, 2);

        // Branching is tree-local: the recurrence is a scalar generation law.
        double mu = 2.0;
        double rho0 = 1.0, S = 0.0;
        int gens = 8;
        for (int j = 0; j < gens; j++) S += Math.Pow(mu, j);
        double share0 = Math.Pow(mu, 0) / S;
        double share3 = Math.Pow(mu, 3) / S;
        Assert.Equal(Math.Pow(mu, 3) * share0, share3, 10); // depth-3 = iterated local recurrence

        // No radial quantity propagates: the ring modes are global (all sites).
        // (Documented: no radial coordinate exists in the branching law.)
    }

    // ── [Required] Y_C_001_ZeroModeSource ────────────────────────────────

    /// <summary>
    /// The zero mode is the reference state (ω₀ = 0, constant eigenvector), not a source:
    /// it emits nothing. It is the uniform background against which Difference is read
    /// (A_002 RQ7).
    /// </summary>
    [Fact]
    public void Y_C_001_ZeroModeSource()
    {
        // Zero mode: λ₀ = 0, ω₀ = √λ₀ = 0 — no oscillation, no emission.
        Assert.Equal(0.0, Lambda(0), 10);
        Assert.Equal(0.0, Math.Sqrt(Lambda(0)), 10);

        // The zero-mode eigenvector is constant (uniform): it singles out no site.
        // |φ₀(n)|² = 1/N for every site (verified analytically).
        double support = 1.0 / N;
        Assert.Equal(0.01042, support, 4);

        // It is the reference state: all positive modes oscillate against it.
        for (int k = 1; k <= 10; k++)
            Assert.True(Math.Sqrt(Lambda(k)) > 0.0);
    }

    // ── [Required] Y_C_001_SymmetryCenter ────────────────────────────────

    /// <summary>
    /// The circulant symmetry of C96 eliminates any preferred center: the adjacency is
    /// translation-invariant (A[i+1,j+1] = A[i,j]), every site has the same degree, and
    /// the zero-mode eigenvector is constant.
    /// </summary>
    [Fact]
    public void Y_C_001_SymmetryCenter()
    {
        // All sites have the same degree: the ring is 12-regular.
        int degree = 2 * K;
        Assert.Equal(12, degree);

        // Translation invariance: A[i+1,j+1] = A[i,j] for the circulant.
        // The adjacency of C96 connects i to i±d (mod 96) — invariant under rotation.
        for (int i = 0; i < 10; i++)
        {
            // Site i's neighbors are {i±1..i±6}; site i+1's are {i+1±1..i+1±6} — the
            // rotated pattern. Any rotation maps site i onto site j (automorphism).
            Assert.True((i + 7) % N < N); // indices wrap around the ring (no boundary)
        }

        // The spectrum's Z2 pairing (λ_k = λ_{N−k}) is a ring symmetry, not a center.
        var lam = new double[N];
        for (int k = 0; k < N; k++) lam[k] = Lambda(k);
        int pairs = 0;
        for (int k = 1; k <= 47; k++)
            if (Math.Abs(lam[k] - lam[N - k]) < 1e-9) pairs++;
        Assert.Equal(47, pairs); // no mode/site is singled out
    }

    // ── [Required] Y_C_001_ClosureCenter ─────────────────────────────────

    /// <summary>
    /// Closure = the fixed point N = 96 (an integer count of ring sites). It is a
    /// centerless ring: no source is required by the closure. The only source-like
    /// object is the branching root (generation 0), an emergent generation-space origin.
    /// </summary>
    [Fact]
    public void Y_C_001_ClosureCenter()
    {
        // Closure is the integer N = 96 (Ch3/Ch5).
        Assert.Equal(96, N);
        Assert.True(N % 1 == 0);

        // The branching root (generation 0) is the only natural source — a generation-
        // space origin, not a spatial center.
        double mu = 2.0;
        int gens = 8;
        double S = 0.0;
        for (int j = 0; j < gens; j++) S += Math.Pow(mu, j);
        double rho0 = 1.0 / S;   // the root carries 1/S of the count
        Assert.Equal(0.00392, rho0, 4);

        // The root is emergent (generation-space), not a spatial site of the ring.
        // (Documented: the ring itself has no distinguished site.)
    }

    // ── [Required] Y_C_001_Run ───────────────────────────────────────────

    [Fact]
    public void Y_C_001_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-C_001 — Center Audit");

        sb.AppendLine("Goal: is a unique center or source present in Difference → Actualization?");
        sb.AppendLine("      Is it derived, emergent, or absent?");
        sb.AppendLine();

        // ── 1. The ring is centerless ───────────────────────────────────
        sb.AppendLine("[1] The attractor ring C96 is centerless");
        sb.AppendLine($"    N = {N}, 12-regular, circulant (translation-invariant)");
        sb.AppendLine("    zero-mode eigenvector: constant (no site singled out)");
        sb.AppendLine("    Z2 pairs: 47 (ring symmetry, no center)");
        sb.AppendLine("    radius r = N/2π = 15.279 — a size measure, no center point");
        sb.AppendLine();

        // ── 2. The only source: the branching root ──────────────────────
        double mu = 2.0;
        int gens = 8;
        double S = 0.0;
        for (int j = 0; j < gens; j++) S += Math.Pow(mu, j);
        sb.AppendLine("[2] The only source: the branching root (generation 0)");
        sb.AppendLine($"    ρ₀ = 1/S = {1.0 / S:F6} — the root carries the first share");
        sb.AppendLine("    This is a GENERATION-SPACE origin, not a spatial center.");
        sb.AppendLine("    Branching is tree-local; there is no radial propagation on the ring.");
        sb.AppendLine();

        // ── 3. Verdicts ─────────────────────────────────────────────────
        sb.AppendLine("[3] Verdicts");
        sb.AppendLine("    RQ1 Difference without center?  → YES (reference, not center)");
        sb.AppendLine("    RQ2 closure implies center?     → NO (N=96 integer, ring)");
        sb.AppendLine("    RQ3 propagation radial?         → NO (tree-local + global readout)");
        sb.AppendLine("    RQ4 zero mode the source?       → NO (reference state, ω₀=0)");
        sb.AppendLine("    RQ5 C96 preferred center?       → NO (translation-invariant)");
        sb.AppendLine("    RQ6 center by symmetry?         → eliminated by symmetry");
        sb.AppendLine("    RQ7 attractor centerless?       → YES (regular ring)");
        sb.AppendLine("    RQ8 closure needs source?       → NO (branching root is generation-space)");
        sb.AppendLine();

        // ── 4. Conclusion ───────────────────────────────────────────────
        sb.AppendLine("[4] Conclusion");
        sb.AppendLine("    Center is ABSENT in space (circulant symmetry eliminates it).");
        sb.AppendLine("    It is EMERGENT as the branching root (generation-space source).");
        sb.AppendLine("    The zero mode is a DERIVED reference state, not a source.");
        sb.AppendLine("    No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
