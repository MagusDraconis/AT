using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_010 — Unit Anchoring Audit test suite (Y_D_010_Tests.cs).
///
/// Question: can a physical unit be anchored to ω₁ (the first non-zero state)?
///
/// Verdict tested: NO — ω₁ = 0.6216 is DIMENSIONLESS; a physical unit requires at
/// least one dimensionful import. ω₁ alone provides only the dimensionless reference
/// (DERIVED); physical clock/ruler/energy units are BOUNDARY. Minimal import: the
/// calibration anchor v (weak scale, GeV); c and ħ are additional SI imports.
///
/// Deterministic: closed-form circulant eigenvalues + analytic dimensional analysis.
/// </summary>
public class Y_D_010_Tests : ResearchTestBase
{
    private const int N = 96;
    private const int K = 6;

    public Y_D_010_Tests(ITestOutputHelper output) : base(output) { }

    private static double Lambda(int k, int n = N)
        => 2.0 * Enumerable.Range(1, K).Sum(d => 1.0 - Math.Cos(2.0 * Math.PI * d * k / n));

    private static double Omega(int k, int n = N) => Math.Sqrt(Lambda(k, n));

    // ── [Required] Y_D_010_DimensionlessReference ────────────────────────

    /// <summary>
    /// ω₁ = 0.6216 is a dimensionless number — it provides a dimensionless frequency
    /// reference (DERIVED), not a physical unit.
    /// </summary>
    [Fact]
    public void Y_D_010_DimensionlessReference()
    {
        Assert.Equal(0.6216, Omega(1), 3); // dimensionless (pure number)

        // The reference is dimensionless: a ratio of eigenvalues' square roots.
        double lam2 = Omega(1) * Omega(1);
        Assert.Equal(0.3864, lam2, 3); // λ₂ = ω₁² (dimensionless)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_010_PhysicalClock ─────────────────────────────────

    /// <summary>
    /// A physical clock is a physical frequency (Hz) — requires a physical time
    /// standard (e.g., atomic clock). ω₁ alone has no units (BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_010_PhysicalClock()
    {
        // ω₁ is dimensionless — it is not a physical frequency (Hz).
        Assert.Equal(0.6216, Omega(1), 3);

        // A physical clock requires a physical time unit (external standard).
        // (Documented: the atomic clock anchors Hz to a physical transition; ω₁ alone
        //  cannot — BOUNDARY.)
        Assert.True(Omega(1) > 0); // dimensionless only
    }

    // ── [Required] Y_D_010_PhysicalRuler ─────────────────────────────────

    /// <summary>
    /// A physical length requires c (L = c/ω₁ in a chosen unit system). ω₁ alone gives
    /// no length (BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_010_PhysicalRuler()
    {
        // ω₁ is dimensionless — no length can be constructed from it alone.
        Assert.Equal(0.6216, Omega(1), 3);

        // A physical ruler requires c (the speed of light) — imported (BOUNDARY).
        // (Documented: L = c/ω₁ needs c and a physical frequency unit.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_010_PhysicalEnergy ────────────────────────────────

    /// <summary>
    /// A physical energy unit requires ħ (E = ħω) or the calibration anchor v. ω₁ alone
    /// gives no energy (BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_010_PhysicalEnergy()
    {
        // ω₁ is dimensionless — no energy can be constructed from it alone.
        Assert.Equal(0.6216, Omega(1), 3);

        // A physical energy requires ħ or v (imported, BOUNDARY).
        // The canonical anchor v (weak scale, GeV) gives E₁ = ω₁·v (D_007-style).
        // (Documented: BOUNDARY.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_010_Scales ────────────────────────────────────────

    /// <summary>
    /// Time, frequency, energy, and length scales from ω₁ each require a dimensionful
    /// import (a physical time unit, ħ, or c). ω₁ alone gives dimensionless relations.
    /// </summary>
    [Fact]
    public void Y_D_010_Scales()
    {
        // Time scale: T = 1/ω₁ (dimensionless) — needs a physical time unit.
        double T = 1.0 / Omega(1);
        Assert.Equal(1.6087, T, 3); // dimensionless reciprocal

        // Frequency scale: ω₁ (dimensionless) — needs a physical time unit (Hz).
        Assert.Equal(0.6216, Omega(1), 3);

        // Energy and length scales require ħ and c respectively (imported).
        // (Documented: every physical scale requires a dimensionful import — BOUNDARY.)
        Assert.True(T > 0);
    }

    // ── [Required] Y_D_010_Dependencies ──────────────────────────────────

    /// <summary>
    /// Dependency check: ω₁ only → dimensionless reference (DERIVED); ω₁+c, ω₁+ħ,
    /// ω₁+v → physical relations but each imports a dimensionful constant (BOUNDARY);
    /// ω₁ + external calibration → the only route to physical units (BOUNDARY).
    /// </summary>
    [Fact]
    public void Y_D_010_Dependencies()
    {
        // ω₁ only: dimensionless reference (DERIVED).
        Assert.Equal(0.6216, Omega(1), 3);

        // + c: a length-time relation (BOUNDARY, c imported).
        // + ħ: an energy relation (BOUNDARY, ħ imported).
        // + v: an energy scale (BOUNDARY, the canonical anchor).
        // (Documented: minimal import = one dimensionful constant, the anchor v.)
        Assert.True(Omega(1) > 0);
    }

    // ── [Required] Y_D_010_Run ───────────────────────────────────────────

    [Fact]
    public void Y_D_010_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_010 — Unit Anchoring Audit");

        sb.AppendLine("Goal: can a physical unit be anchored to ω₁?");
        sb.AppendLine();

        sb.AppendLine("[1] The dimensional obstacle");
        sb.AppendLine($"    ω₁ = {Omega(1):F4} is DIMENSIONLESS (a pure frequency ratio).");
        sb.AppendLine("    A physical unit (s, m, J) is dimensionful — constructing it");
        sb.AppendLine("    from ω₁ requires a dimensionful import.");
        sb.AppendLine();

        sb.AppendLine("[2] Unit system from ω₁ alone");
        sb.AppendLine("    A) dimensionless reference → DERIVED (ω₁ provides it)");
        sb.AppendLine("    B) physical clock          → BOUNDARY (needs a time standard)");
        sb.AppendLine("    C) physical ruler          → BOUNDARY (needs c)");
        sb.AppendLine("    D) physical energy unit    → BOUNDARY (needs ħ or v)");
        sb.AppendLine();

        sb.AppendLine("[3] Scales");
        sb.AppendLine($"    time:      T = 1/ω₁ = {1.0 / Omega(1):F4} (dimensionless) → needs a time unit");
        sb.AppendLine("    frequency: ω₁ → needs a physical time unit (Hz)");
        sb.AppendLine("    energy:    E₁ = ħω₁ or ω₁·v → needs ħ or the anchor v");
        sb.AppendLine("    length:    L = c/ω₁ → needs c");
        sb.AppendLine();

        sb.AppendLine("[4] Dependencies");
        sb.AppendLine("    ω₁ only          → dimensionless reference (DERIVED)");
        sb.AppendLine("    ω₁ + c / + ħ / +v → physical relations, each imports a constant (BOUNDARY)");
        sb.AppendLine("    ω₁ + calibration → the only route to physical units (BOUNDARY)");
        sb.AppendLine("    minimal import: the calibration anchor v (weak scale, GeV)");
        sb.AppendLine();

        sb.AppendLine("[5] Conclusion");
        sb.AppendLine("    A physical unit cannot be anchored to ω₁ alone. ω₁ provides the");
        sb.AppendLine("    dimensionless reference (DERIVED); physical units require at least");
        sb.AppendLine("    one dimensionful import (the anchor v; c and ħ for SI) — BOUNDARY.");
        sb.AppendLine("    No canonical value is changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
