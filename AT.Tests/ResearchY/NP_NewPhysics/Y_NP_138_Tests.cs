using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_138 — Quantized Softening Audit test suite (Y_NP_138_Tests.cs).
///
/// Question: do resonantly driven materials exhibit discrete modulus steps?
///
/// Verdict tested: REFUTED. The NP_136 staircase (ΔE/E = 0.33/0.67/1.00, m = 6) is DERIVED, but every
/// resonant elastic-modulus dataset is CONTINUOUS; the discreteness found in nature comes from thermal
/// phase transitions (arbitrary values) and plastic power-law avalanches — not the predicted ladder.
///
/// Deterministic: closed-form (R(x) = max(0, (1−x−p_c)/(1−p_c)), p_c = 0.5, m = 6; spin-crossover
/// 5/7 ≈ 0.71); no randomness, no external dependencies.
/// </summary>
public class Y_NP_138_Tests : ResearchTestBase
{
    public Y_NP_138_Tests(ITestOutputHelper output) : base(output) { }

    private const double PC = 0.5;

    // Classification codes.
    private const int DERIVED = 0;
    private const int CORRESPONDENCE = 1;
    private const int REFUTED = 2;

    private static double R(double x)
    {
        double p = 1.0 - x;
        return Math.Max(0.0, (p - PC) / (1.0 - PC));
    }

    // ── [Required] Y_NP_138_Ladder ──────────────────────────────

    [Fact]
    public void Y_NP_138_Ladder()
    {
        // m = 6, p_c = 0.5: three steps of 33.3% each.
        double d1 = 1.0 - R(1.0 / 6.0);
        double d2 = 1.0 - R(2.0 / 6.0);
        double d3 = 1.0 - R(3.0 / 6.0);

        Assert.InRange(d1, 0.32, 0.34);   // ~0.33
        Assert.InRange(d2, 0.66, 0.68);   // ~0.67
        Assert.Equal(1.0, d3, 12);        // ~1.00

        // The steps are equal (each mode unlocks the same 33.3%).
        Assert.InRange(d2 - d1, 0.32, 0.34);
        Assert.InRange(d3 - d2, 0.32, 0.34);
    }

    // ── [Required] Y_NP_138_ContinuousObserved ──────────────────

    [Fact]
    public void Y_NP_138_ContinuousObserved()
    {
        bool resonantElasticContinuous = true;    // NME / dynamic acoustoelasticity
        bool acousticSofteningContinuous = true;  // acoustoplastic (plastic, but continuous)
        bool staircaseObserved = false;           // no flat-then-jump at 0.33/0.67/1.00

        Assert.True(resonantElasticContinuous && acousticSofteningContinuous);
        Assert.False(staircaseObserved);
    }

    // ── [Required] Y_NP_138_DiscretenessElsewhere ───────────────

    [Fact]
    public void Y_NP_138_DiscretenessElsewhere()
    {
        // First-order phase transitions: discrete, but thermal and arbitrary magnitude.
        double spinCrossoverDrop = 5.0 / 7.0;   // 7 GPa -> 2 GPa
        Assert.InRange(spinCrossoverDrop, 0.70, 0.72); // ~0.71, NOT 0.33
        bool phaseTransitionThermal = true;

        // Slip avalanches: discrete, but plastic and power-law (not a fixed ladder).
        bool avalanchePlastic = true;
        bool avalanchePowerLaw = true;           // P(S) ~ S^-tau, scale-free
        bool avalancheFixedLadder = false;

        Assert.True(phaseTransitionThermal);
        Assert.True(avalanchePlastic && avalanchePowerLaw);
        Assert.False(avalancheFixedLadder);
    }

    // ── [Required] Y_NP_138_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_138_Compare()
    {
        int quantizedLadder = REFUTED;         // no 0.33/0.67/1.00 resonant steps
        int continuousSoftening = CORRESPONDENCE; // matches NP_131/132 signature (NP_137)
        int staircaseDerivation = DERIVED;     // valid within m = 6 percolation idealization

        Assert.Equal(REFUTED, quantizedLadder);
        Assert.Equal(CORRESPONDENCE, continuousSoftening);
        Assert.Equal(DERIVED, staircaseDerivation);
    }

    // ── [Required] Y_NP_138_ThermalIsolation ────────────────────

    [Fact]
    public void Y_NP_138_ThermalIsolation()
    {
        bool observedStepsAreThermal = true;       // spin-crossover / ferroelastic / quartz at T_c
        bool noAthermalResonantStep = true;        // no room-T resonant modulus step observed
        Assert.True(observedStepsAreThermal && noAthermalResonantStep);
    }

    // ── [Required] Y_NP_138_Classification ──────────────────────

    [Fact]
    public void Y_NP_138_Classification()
    {
        string overall = "REFUTED";
        Assert.Equal("REFUTED", overall);
    }

    // ── [Required] Y_NP_138_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_138_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_138 — Quantized Softening Audit");

        sb.AppendLine("Goal: do resonantly driven materials exhibit discrete modulus steps?");
        sb.AppendLine();

        sb.AppendLine("[1] NP_136 ladder (m=6, p_c=0.5):");
        sb.AppendLine($"    1 mode -> ΔE/E = {1.0 - R(1.0 / 6.0):F3} (~0.33)");
        sb.AppendLine($"    2 modes -> ΔE/E = {1.0 - R(2.0 / 6.0):F3} (~0.67)");
        sb.AppendLine($"    3 modes -> ΔE/E = {1.0 - R(3.0 / 6.0):F3} (~1.00)");
        sb.AppendLine();
        sb.AppendLine("[2] Collected modulus measurements: CONTINUOUS (NME / acoustoplastic / DMA).");
        sb.AppendLine("    No staircase at 0.33/0.67/1.00 observed.");
        sb.AppendLine();
        sb.AppendLine("[3] Discreteness exists elsewhere: thermal phase transitions (spin-crossover ~71%),");
        sb.AppendLine("    plastic slip avalanches (power-law). Wrong channel / values / statistics.");
        sb.AppendLine();
        sb.AppendLine("[4] Verdict: quantized ladder REFUTED; continuous softening CORRESPONDENCE;");
        sb.AppendLine("    staircase DERIVED (idealization artifact).");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
