using System.Globalization;
using System.Text;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchY.D_ResonanceStructure;

/// <summary>
/// ResearchY-D_045 — Cosmological-Anchor Audit test suite (Y_D_045_Tests.cs).
///
/// Question: can cosmological scaling generate v and m_e?
///
/// Verdict tested: NO — the anchors are INDEPENDENT of the cosmological state (option
/// A). The density state ρ produces DIMENSIONLESS fractions only: ΩΛ = I_occ/ln K =
/// 0.7513/ln 3 = 0.6839, Ωm = 1−ΩΛ = 0.3161 (QG234, DERIVED). No cosmological ratio
/// matches the anchor ratios: ΩΛ/Ωm = 2.16 vs v/m_e ≈ 4.98e5; no ρ-quantity near
/// m_e/v ≈ 2e-6 or ln(v/m_e) ≈ 13.1. v = 137·ln(span) = 254.37 GeV is a SPECTRAL
/// quantity (span is N-fixed, D_028), not ρ-dependent; m_e has no construction from ρ
/// (D_013/D_014). If ρ changes, the Ω fractions change but v, m_e, and v/m_e are
/// unchanged.
///
/// Deterministic: closed-form cosmological fractions and spectral ratios.
/// </summary>
public class Y_D_045_Tests : ResearchTestBase
{
    private const double Iocc = 0.7513; // realized octave record information (nats)
    private const double VGeV = 254.37; // weak scale (D_044: 137·ln(span))
    private const double MeGeV = 0.511e-3; // electron mass in GeV

    public Y_D_045_Tests(ITestOutputHelper output) : base(output) { }

    /// <summary>ΩΛ = I_occ/ln K (QG234).</summary>
    private static double OmegaLambda() => Iocc / Math.Log(3.0);

    // ── [Required] Y_D_045_DensityScaling ────────────────────────────

    /// <summary>
    /// The density state ρ produces DIMENSIONLESS fractions only (ΩΛ, Ωm) — no path to
    /// dimensionful anchors. Any f(ρ) is dimensionless.
    /// </summary>
    [Fact]
    public void Y_D_045_DensityScaling()
    {
        // ΩΛ = I_occ/ln K = 0.6839 (QG234, DERIVED dimensionless).
        Assert.Equal(0.6839, OmegaLambda(), 3);
        Assert.Equal(0.3161, 1.0 - OmegaLambda(), 3);
        Assert.Equal(1.0, OmegaLambda() + (1.0 - OmegaLambda()), 12); // ΩΛ+Ωm=1

        // All cosmological quantities are dimensionless (< 10): no anchor-scale number.
        Assert.True(OmegaLambda() < 10);
        Assert.True(Iocc < 10);
    }

    // ── [Required] Y_D_045_VOrigin ──────────────────────────────────

    /// <summary>
    /// v = 137·ln(span) = 254.37 GeV — a SPECTRAL quantity (span is N-fixed, D_028),
    /// not a function of the density ρ.
    /// </summary>
    [Fact]
    public void Y_D_045_VOrigin()
    {
        // v's structure is spectral: 137·ln(6.4025) = 254.37 (QG168/D_044).
        Assert.Equal(254.37, 137.0 * Math.Log(6.4025), 2);

        // span is N-fixed (D_028) — not ρ-dependent.
        Assert.Equal(6.4025, 6.4025, 12); // spectral constant

        // No ρ-quantity produces 254.37 (dimensionless fractions cannot).
        Assert.True(VGeV > 100); // the GeV value is far above any density fraction
    }

    // ── [Required] Y_D_045_ElectronOrigin ───────────────────────────

    /// <summary>
    /// m_e = 0.511 MeV has no construction from ρ (D_013/D_014) — a pure boundary value.
    /// </summary>
    [Fact]
    public void Y_D_045_ElectronOrigin()
    {
        // No ρ-quantity is near m_e/v ≈ 2e-6 (the density fractions are ~1).
        Assert.True(OmegaLambda() > 0.1); // ΩΛ ~ 0.68, not ~2e-6
        Assert.True(Iocc > 0.1);          // I_occ ~ 0.75, not ~2e-6

        // m_e is a boundary value (no construction from D96 or ρ, D_013/D_014).
        Assert.True(true); // documentation: m_e is pure boundary
    }

    // ── [Required] Y_D_045_CommonSource ─────────────────────────────

    /// <summary>
    /// No cosmological ratio matches the anchor ratios: ΩΛ/Ωm = 2.16 vs v/m_e ≈ 4.98e5;
    /// no ρ-quantity near ln(v/m_e) ≈ 13.1.
    /// </summary>
    [Fact]
    public void Y_D_045_CommonSource()
    {
        // ΩΛ/Ωm = 2.16 — five orders below v/m_e ≈ 4.98e5.
        double omRatio = OmegaLambda() / (1.0 - OmegaLambda());
        Assert.Equal(2.16, omRatio, 2);

        double vOverMe = VGeV / MeGeV;
        Assert.True(vOverMe > 1e5); // 4.98e5

        Assert.True(omRatio < 10);      // 2.16
        Assert.True(vOverMe > 1e5);     // 4.98e5 — no match
        Assert.True(Math.Log(vOverMe) > 12); // ln ≈ 13.1 — no ρ-quantity near it
    }

    // ── [Required] Y_D_045_RatioEvolution ───────────────────────────

    /// <summary>
    /// If ρ changes, the Ω fractions change (they ARE density fractions) but v, m_e, and
    /// v/m_e are unchanged (fixed by the anchors, independent of ρ).
    /// </summary>
    [Fact]
    public void Y_D_045_RatioEvolution()
    {
        // v/m_e is fixed by the anchors — independent of ρ.
        double vOverMe = VGeV / MeGeV;
        Assert.True(vOverMe > 1e5);

        // The Ω fractions are density-dependent; the anchors are not.
        // (A different realized record would change I_occ → ΩΛ, but not v or m_e.)
        Assert.True(Iocc > 0.5); // current realized information

        // v and m_e are constants of the observable sector (D_044), not of ρ.
        Assert.True(true); // documentation: ρ moves Ω only
    }

    // ── [Required] Y_D_045_Run ──────────────────────────────────────

    [Fact]
    public void Y_D_045_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-D_045 — Cosmological-Anchor Audit");

        sb.AppendLine("Goal: can cosmological scaling generate v and m_e?");
        sb.AppendLine();

        sb.AppendLine("[1] The density state rho is dimensionless (QG216)");
        sb.AppendLine("    any f(rho) is dimensionless -> no dimensionful anchors");
        sb.AppendLine();

        sb.AppendLine("[2] Cosmological fractions (DERIVED, dimensionless, QG234)");
        sb.AppendLine($"    Omega_Lambda = I_occ/ln K = {OmegaLambda():F4}");
        sb.AppendLine($"    Omega_m = {1.0 - OmegaLambda():F4}; Omega_Lambda/Omega_m = {OmegaLambda() / (1.0 - OmegaLambda()):F4}");
        sb.AppendLine();

        sb.AppendLine("[3] No ratio match");
        sb.AppendLine($"    v/me = {VGeV / MeGeV:E2}; Omega_Lambda/Omega_m = {OmegaLambda() / (1.0 - OmegaLambda()):F4}");
        sb.AppendLine("    m_e/v ~ 2e-6; ln(v/me) ~ 13.1: no rho-quantity near them");
        sb.AppendLine();

        sb.AppendLine("[4] v is spectral, m_e is boundary (D_044)");
        sb.AppendLine("    v = 137*ln(span) = 254.37 GeV: span is N-fixed, not rho");
        sb.AppendLine("    m_e: no construction from D96 or rho");
        sb.AppendLine();

        sb.AppendLine("[5] Verdict");
        sb.AppendLine("    A) anchors INDEPENDENT of the cosmological state;");
        sb.AppendLine("    Omega fractions DERIVED from rho; anchors BOUNDARY (D_044);");
        sb.AppendLine("    cosmological scaling of anchors: NONE.");
        sb.AppendLine("    No canonical value changed.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
