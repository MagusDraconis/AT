using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_009 — Minimum Excitation Audit test suite (Y_D_009_Tests.cs).
///
/// Question: is ω₁ the minimum non-zero actualization / the first possible difference
/// above the zero mode?
///
/// Verdict tested: YES — ω₁ = 0.6216 is the smallest positive frequency (DERIVED),
/// isolated from the zero mode by the spectral gap λ₂ = ω₁² = 0.3864 (no state in
/// (0, ω₁), verified). "First actualization" is an EMERGENT interpretation; as a
/// physical clock ω₁ is BOUNDARY (D_008, dimensionless only).
///
/// Deterministic: closed-form circulant eigenvalues + analytic frequencies.
/// </summary>
public class Y_D_009_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_009_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_009_ZeroMode ──────────────────────────────────────

    /// <summary>
    /// The zero mode: ω₀ = 0 (reference state, no oscillation).
    /// </summary>
    [Fact]
    public void Y_D_009_ZeroMode()
    {
        Assert.Equal(0.0, Lambda(0), 10);
        Assert.Equal(0.0, Omega(0), 10);
        Assert.Equal(0.01042, 1.0 / N, 4); // constant eigenvector (uniform)
    }

    // ── [Required] Y_D_009_MinimumExcitation ─────────────────────────────

    /// <summary>
    /// ω₁ = 0.6216 > 0 is the smallest positive frequency (the first excitation).
    /// </summary>
    [Fact]
    public void Y_D_009_MinimumExcitation()
    {
        Assert.True(Omega(1) > 0);
        Assert.Equal(0.6216, Omega(1), 3);

        // ω₁ is the minimum of the positive spectrum.
        double minW = double.PositiveInfinity;
        for (int k = 1; k < N; k++) minW = Math.Min(minW, Omega(k));
        Assert.Equal(Omega(1), minW, 6); // the first frequency is the minimum
    }

    // ── [Required] Y_D_009_MinimumDifference ─────────────────────────────

    /// <summary>
    /// ω₁ is the first (minimum non-zero) difference above the zero mode:
    /// the smallest positive separation from ω₀ = 0.
    /// </summary>
    [Fact]
    public void Y_D_009_MinimumDifference()
    {
        // The difference ω₁ − ω₀ = ω₁ is the smallest positive spectral separation.
        Assert.Equal(Omega(1), Omega(1) - Omega(0), 10);

        // No positive frequency is smaller than ω₁.
        for (int k = 1; k < N; k++)
            Assert.True(Omega(k) >= Omega(1) - 1e-9);
    }

    // ── [Required] Y_D_009_ActualizationEvent ────────────────────────────

    /// <summary>
    /// The minimum excitation as the "first actualization event" is an interpretation:
    /// the structure (first frequency) is DERIVED; the count-event identification is
    /// EMERGENT.
    /// </summary>
    [Fact]
    public void Y_D_009_ActualizationEvent()
    {
        // The spectral fact: ω₁ is the first excitation (DERIVED).
        Assert.Equal(0.6216, Omega(1), 3);

        // The actualization reading (the first count event) is interpretive.
        // (Documented: structure DERIVED, count-event identification EMERGENT.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_009_NoStateBetween ────────────────────────────────

    /// <summary>
    /// No spectral state exists between ω₀ and ω₁: the spectral gap λ₂ = ω₁² = 0.3864
    /// is the smallest positive eigenvalue; zero positive states lie below ω₁.
    /// </summary>
    [Fact]
    public void Y_D_009_NoStateBetween()
    {
        // The spectral gap λ₂ = ω₁² = 0.3864 (the smallest positive eigenvalue).
        double lam2 = 0.0;
        for (int k = 1; k < N; k++)
        {
            double l = Lambda(k);
            if (lam2 == 0.0 || l < lam2) lam2 = l;
        }
        Assert.Equal(0.3864, lam2, 3);
        Assert.Equal(lam2, Omega(1) * Omega(1), 4); // ω₁² = λ₂

        // Zero positive frequencies strictly below ω₁.
        int below = 0;
        for (int k = 1; k < N; k++)
            if (Omega(k) < Omega(1) - 1e-9) below++;
        Assert.Equal(0, below); // no state in (0, ω₁)

        // Multiplicity of the first excitation (fundamental doublet): 2.
        int mult = 0;
        for (int k = 1; k < N; k++)
            if (Math.Abs(Omega(k) - Omega(1)) < 1e-9) mult++;
        Assert.Equal(2, mult); // k=1 and k=N−1
    }

    // ── [Required] Y_D_009_Classification ────────────────────────────────

    /// <summary>
    /// ω₁ is the first frequency and first difference (DERIVED); "first actualization"
    /// is EMERGENT (interpretive); as a physical clock it is BOUNDARY (D_008).
    /// </summary>
    [Fact]
    public void Y_D_009_Classification()
    {
        // First frequency: DERIVED (smallest positive ω).
        Assert.Equal(0.6216, Omega(1), 3);

        // First difference: DERIVED (minimum non-zero separation from ω₀).
        Assert.Equal(Omega(1), Omega(1) - Omega(0), 10);

        // First actualization: EMERGENT (interpretive reading of the minimum excitation).
        // Physical clock: BOUNDARY (dimensionless only, D_008).
        // (Documented: A DERIVED, B DERIVED, C EMERGENT, D BOUNDARY-as-physical.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_009_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_009_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_009 — Minimum Excitation Audit");

        sb.AppendLine("Goal: is ω₁ the minimum non-zero actualization?");
        sb.AppendLine();

        double lam2 = 0.0;
        for (int k = 1; k < N; k++)
        {
            double l = Lambda(k);
            if (lam2 == 0.0 || l < lam2) lam2 = l;
        }
        int below = 0;
        for (int k = 1; k < N; k++)
            if (Omega(k) < Omega(1) - 1e-9) below++;
        int mult = 0;
        for (int k = 1; k < N; k++)
            if (Math.Abs(Omega(k) - Omega(1)) < 1e-9) mult++;

        sb.AppendLine("[1] Definitions");
        sb.AppendLine("    zero mode: λ₀ = 0, ω₀ = 0 (uniform reference)");
        sb.AppendLine("    minimum excitation: ω₁ = min{ω_k : k=1..95}");
        sb.AppendLine("    minimum difference: ω₁ − ω₀ = ω₁ (smallest non-zero separation)");
        sb.AppendLine("    actualization event: a unit of Difference (Q-event)");
        sb.AppendLine();

        sb.AppendLine("[2] Spectral facts");
        sb.AppendLine($"    ω₀ = {Omega(0):F1}, ω₁ = {Omega(1):F4} > 0");
        sb.AppendLine($"    spectral gap λ₂ = ω₁² = {lam2:F4} (smallest positive eigenvalue)");
        sb.AppendLine($"    positive states below ω₁: {below}");
        sb.AppendLine($"    multiplicity of ω₁: {mult} (fundamental doublet)");
        sb.AppendLine();

        sb.AppendLine("[3] Proof: no state between ω₀ and ω₁");
        sb.AppendLine("    The positive spectrum is discrete; λ₂ is the smallest positive");
        sb.AppendLine("    eigenvalue, so ω₁ = √λ₂ is the minimum frequency. The interval");
        sb.AppendLine("    (0, ω₁) contains no spectral state (verified: 0 below).");
        sb.AppendLine();

        sb.AppendLine("[4] Verdicts");
        sb.AppendLine("    A) first frequency  → DERIVED (ω₁ = min positive ω)");
        sb.AppendLine("    B) first difference → DERIVED (min non-zero separation from ω₀)");
        sb.AppendLine("    C) first actualization → EMERGENT (interpretive reading)");
        sb.AppendLine("    D) natural clock only → NO (more than a clock); physical clock BOUNDARY");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    ω₁ IS the minimum non-zero excitation — the first frequency and");
        sb.AppendLine("    first difference above the zero mode, isolated by the spectral gap");
        sb.AppendLine("    λ₂ = ω₁². 'First actualization' is interpretive (EMERGENT); as a");
        sb.AppendLine("    physical clock it is BOUNDARY. No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
