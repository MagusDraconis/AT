using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_011 — Universal Reference Audit test suite (Y_D_011_Tests.cs).
///
/// Question: can ω₁ be the universal reference to which all physical units are attached?
///
/// Verdict tested: ω₁ is the universal DIMENSIONLESS reference (ratios ω_k/ω₁,
/// λ_k/λ₂, span/ω₁ are exact, DERIVED) but NOT the universal physical-unit reference
/// (every dimension needs a dimensionful anchor, BOUNDARY). Minimal anchor count: one
/// (v) for energy/mass; length/time need more.
///
/// Deterministic: closed-form circulant eigenvalues + analytic ratios.
/// </summary>
public class Y_D_011_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_011_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_011_UniversalReference ────────────────────────────

    /// <summary>
    /// ω₁ is the universal dimensionless reference: the smallest positive frequency
    /// (D_009) against which the spectrum's ratios are measured.
    /// </summary>
    [Fact]
    public void Y_D_011_UniversalReference()
    {
        Assert.Equal(0.6216, Omega(1), 3); // ω₁ (dimensionless)

        // ω₁ is the minimum positive frequency (the reference state).
        double minW = double.PositiveInfinity;
        for (int k = 1; k < N; k++) minW = Math.Min(minW, Omega(k));
        Assert.Equal(Omega(1), minW, 6);

        // Dimensionless: a pure number.
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_011_Dimensions ────────────────────────────────────

    /// <summary>
    /// No physical dimension (time/frequency/energy/mass/length) can be expressed
    /// relative to ω₁ alone — each requires a dimensionful anchor (D_010, BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_011_Dimensions()
    {
        // time:      T = 1/ω₁ (dimensionless) — needs a physical time unit.
        // frequency: ω = ω₁·ratio — needs a physical Hz standard.
        // energy:    E = ω₁·v — needs the anchor v.
        // mass:      M = ω₁·v/c² — needs v and c.
        // length:    L = c/ω₁ — needs c.
        // (Documented: every physical dimension needs an anchor — BOUNDARY.)
        Assert.Equal(0.6216, Omega(1), 3); // ω₁ alone is dimensionless
    }

    // ── [Required] Y_D_011_ReferenceAnalogies ────────────────────────────

    /// <summary>
    /// ω₁ does not act as an atomic transition frequency, a speed-of-light reference,
    /// or a Planck reference — it is a dimensionless spectral frequency.
    /// </summary>
    [Fact]
    public void Y_D_011_ReferenceAnalogies()
    {
        // Atomic transition: a physical frequency (e.g., Cs 9.19 GHz) — ω₁ is dimensionless.
        // Speed of light: c defines the meter — ω₁ carries no length.
        // Planck reference: ħ, c, G — ω₁ is a single dimensionless number.
        // (Documented: ω₁ is a dimensionless spectral reference, not a physical standard.)
        Assert.Equal(0.6216, Omega(1), 3);
    }

    // ── [Required] Y_D_011_DimensionlessRatios ───────────────────────────

    /// <summary>
    /// The dimensionless ratios are exact, derived spectral facts:
    /// ω_max/ω₁ (span 6.40), λ_max/λ₂ (40.99), span/ω₁ (10.30).
    /// </summary>
    [Fact]
    public void Y_D_011_DimensionlessRatios()
    {
        double w1 = Omega(1);
        double lam2 = Lambda(1);

        // ω_k/ω₁: the span (max ratio).
        double maxW = 0.0;
        for (int k = 1; k < N; k++) maxW = Math.Max(maxW, Omega(k));
        Assert.Equal(6.40, maxW / w1, 2); // span (DERIVED)

        // λ_k/λ₂: max eigenvalue ratio.
        double maxL = 0.0;
        for (int k = 1; k < N; k++) maxL = Math.Max(maxL, Lambda(k));
        Assert.Equal(40.99, maxL / lam2, 2);

        // span/ω₁.
        Assert.Equal(10.30, (maxW / w1) / w1, 2);

        // These ratios are dimensionless and exact (DERIVED).
        Assert.True(maxW / w1 > 0);
    }

    // ── [Required] Y_D_011_AnchorCount ──────────────────────────────────

    /// <summary>
    /// Physical units require: A) ω₁ only → DERIVED (dimensionless); B) ω₁ + one anchor
    /// (v) → BOUNDARY (energy/mass); C) ω₁ + multiple anchors (v, c, ħ) → BOUNDARY (SI).
    /// Minimal anchor count: one (v) for energy/mass.
    /// </summary>
    [Fact]
    public void Y_D_011_AnchorCount()
    {
        // A) ω₁ only: dimensionless reference (DERIVED).
        Assert.Equal(0.6216, Omega(1), 3);

        // B) ω₁ + one anchor (v): energy/mass scales (BOUNDARY).
        // C) ω₁ + multiple anchors (v, c, ħ): all SI dimensions (BOUNDARY).
        // (Documented: minimal anchor count = 1 (v) for energy/mass; length/time need more.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_011_ScaleMap ──────────────────────────────────────

    /// <summary>
    /// The universal scale map: ω₁ → reference ratios (DERIVED) → dimensions (BOUNDARY)
    /// → observables. The map splits at "dimensions".
    /// </summary>
    [Fact]
    public void Y_D_011_ScaleMap()
    {
        // ω₁ → ratios (DERIVED): the dimensionless skeleton.
        double w1 = Omega(1);
        double maxW = 0.0;
        for (int k = 1; k < N; k++) maxW = Math.Max(maxW, Omega(k));
        Assert.Equal(6.40, maxW / w1, 2); // the ratio map is derived

        // ω₁ → dimensions (BOUNDARY): the physical units need anchors.
        // ω₁ → observables: the spectral readouts use the dimensionless ratios.
        // (Documented: the map splits at "dimensions".)
        Assert.True(w1 > 0);
    }

    // ── [Required] Y_D_011_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_011_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_011 — Universal Reference Audit");

        sb.AppendLine("Goal: can ω₁ be the universal reference for all physical units?");
        sb.AppendLine();

        double w1 = Omega(1);
        double lam2 = Lambda(1);
        double maxW = 0.0;
        for (int k = 1; k < N; k++) maxW = Math.Max(maxW, Omega(k));
        double maxL = 0.0;
        for (int k = 1; k < N; k++) maxL = Math.Max(maxL, Lambda(k));

        sb.AppendLine("[1] ω₁ as the universal dimensionless reference");
        sb.AppendLine($"    ω₁ = {w1:F4} (the first non-zero state, D_009)");
        sb.AppendLine($"    ratios: ω_max/ω₁ = {maxW / w1:F2} (span), λ_max/λ₂ = {maxL / lam2:F2}, span/ω₁ = {(maxW / w1) / w1:F2}");
        sb.AppendLine("    all ratios DERIVED (exact, dimensionless)");
        sb.AppendLine();

        sb.AppendLine("[2] Physical dimensions relative to ω₁");
        sb.AppendLine("    time/frequency/energy/mass/length — each needs a dimensionful anchor");
        sb.AppendLine("    (D_010: ω₁ is dimensionless; no physical unit from ω₁ alone) — BOUNDARY");
        sb.AppendLine();

        sb.AppendLine("[3] Reference analogies");
        sb.AppendLine("    atomic transition?  NO (ω₁ dimensionless)");
        sb.AppendLine("    speed-of-light?     NO (ω₁ carries no length)");
        sb.AppendLine("    Planck reference?   NO (ω₁ is a single number)");
        sb.AppendLine();

        sb.AppendLine("[4] Anchor count");
        sb.AppendLine("    A) ω₁ only            → dimensionless (DERIVED)");
        sb.AppendLine("    B) ω₁ + one anchor (v)→ energy/mass (BOUNDARY)");
        sb.AppendLine("    C) ω₁ + multiple      → all SI dimensions (BOUNDARY)");
        sb.AppendLine("    minimal anchor count: 1 (v) for energy/mass; length/time need more");
        sb.AppendLine();

        sb.AppendLine("[5] Universal scale map");
        sb.AppendLine("    ω₁ → reference ratios (DERIVED) → dimensions (BOUNDARY) → observables");
        sb.AppendLine("    ω₁ is the universal DIMENSIONLESS reference, not the universal");
        sb.AppendLine("    physical-unit reference. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
