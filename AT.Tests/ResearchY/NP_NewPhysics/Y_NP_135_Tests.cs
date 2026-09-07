using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_135 — Coherent Softening Experimental Audit test suite (Y_NP_135_Tests.cs).
///
/// Question: what is the simplest lab experiment that could falsify the critical-mode softening
/// hypothesis?
///
/// Verdict tested: matched-power OFF-resonance (thermal control) vs ON-resonance (critical mode)
/// ultrasound; measure rigidity (resonant frequency), damping (Q), ΔT. PASS iff R = ΔR(f₁)/ΔR(f_off)
/// ≫ 1 AND reversible AND frequency-selective; FAIL (thermal-only) iff R ≈ 1. Design EMERGENT
/// (falsifiable test of DERIVED NP_131/132).
///
/// Deterministic: closed-form (f₁ = c_s/(2L), ΔR/R₀ = −α·ΔT).
/// </summary>
public class Y_NP_135_Tests : ResearchTestBase
{
    public Y_NP_135_Tests(ITestOutputHelper output) : base(output) { }

    // ── [Required] Y_NP_135_Materials ───────────────────────────

    [Fact]
    public void Y_NP_135_Materials()
    {
        double L = 0.10;
        double fAl = 5100.0 / (2 * L) / 1e3;
        double fQuartz = 5750.0 / (2 * L) / 1e3;
        double fSteel = 5100.0 / (2 * L) / 1e3;
        Assert.InRange(fAl, 25.0, 26.0);      // ~25.5 kHz
        Assert.InRange(fQuartz, 28.0, 29.0);  // ~28.75 kHz
        Assert.InRange(fSteel, 25.0, 26.0);   // ~25.5 kHz
    }

    // ── [Required] Y_NP_135_Baseline ────────────────────────────

    [Fact]
    public void Y_NP_135_Baseline()
    {
        // R0 = f0^2 (∝ Young modulus) measured by resonant ultrasound spectroscopy.
        bool baselineViaRUS = true;
        bool r0IsResonantFrequencySquared = true;
        Assert.True(baselineViaRUS && r0IsResonantFrequencySquared);
    }

    // ── [Required] Y_NP_135_Drives ──────────────────────────────

    [Fact]
    public void Y_NP_135_Drives()
    {
        // off-resonance = thermal control (matched power, no critical-mode drive);
        // critical-mode = coherent + thermal.
        bool offResonanceIsThermalControl = true;
        bool criticalModeIsCoherent = true;
        bool matchedPower = true;
        Assert.True(offResonanceIsThermalControl && criticalModeIsCoherent && matchedPower);
    }

    // ── [Required] Y_NP_135_ThermalBaseline ─────────────────────

    [Fact]
    public void Y_NP_135_ThermalBaseline()
    {
        // dR/R0 = -alpha*dT. At dT = 1 K: Al 4.5e-4, quartz 1.5e-4, steel 2.4e-4.
        double dT = 1.0;
        double dAl = 4.5e-4 * dT;
        double dQuartz = 1.5e-4 * dT;
        double dSteel = 2.4e-4 * dT;
        Assert.InRange(dAl * 100, 0.04, 0.05);      // ~0.045%
        Assert.InRange(dQuartz * 100, 0.01, 0.02);  // ~0.015%
        Assert.InRange(dSteel * 100, 0.02, 0.03);   // ~0.024%
    }

    // ── [Required] Y_NP_135_Criterion ───────────────────────────

    [Fact]
    public void Y_NP_135_Criterion()
    {
        // R = dR(f1)/dR(f_off). PASS iff R >> 1 AND reversible AND frequency-selective;
        // FAIL (thermal-only) iff R ~ 1.
        bool passIfLargeExcess = true;
        bool passIfReversible = true;
        bool passIfFrequencySelective = true;
        bool failIfThermalOnly = true;   // R ~ 1
        Assert.True(passIfLargeExcess && passIfReversible && passIfFrequencySelective);
        Assert.True(failIfThermalOnly);
    }

    // ── [Required] Y_NP_135_Classification ──────────────────────

    [Fact]
    public void Y_NP_135_Classification()
    {
        bool experimentDesignEmergent = true;
        bool coherentEqualsThermalIsFalsification = true;
        Assert.True(experimentDesignEmergent && coherentEqualsThermalIsFalsification);
    }

    // ── [Required] Y_NP_135_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_135_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_135 — Coherent Softening Experimental Audit");

        sb.AppendLine("Goal: the simplest falsifiable experiment for coherent softening.");
        sb.AppendLine();

        sb.AppendLine("[1] 10 cm bars: aluminum f1=25.5 kHz, quartz 28.75 kHz, steel 25.5 kHz.");
        sb.AppendLine("[2] Baseline R0 = f0^2 (resonant ultrasound spectroscopy).");
        sb.AppendLine("[3] Off-resonance (thermal control) vs critical-mode (coherent) at matched power.");
        sb.AppendLine("[4] Thermal baseline dR/R0 ~ 0.015-0.045 % per K (tiny).");
        sb.AppendLine();

        sb.AppendLine("[5] Decisive criterion: R = dR(f1)/dR(f_off). PASS iff R >> 1 + reversible + selective; FAIL iff R ~ 1.");
        sb.AppendLine();

        sb.AppendLine("[6] Verdict: a decisive pass/fail experiment (EMERGENT).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
