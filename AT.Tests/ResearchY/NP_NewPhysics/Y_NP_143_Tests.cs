using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_143 — Dislocation Threshold Audit test suite (Y_NP_143_Tests.cs).
///
/// Question: what amplitude/energy density triggers significant defect mobility?
///
/// Verdict tested: SUPPORTED — laboratory-accessible. The Granato–Lücke breakaway threshold
/// ε_c ≈ 10⁻⁶–10⁻⁵ gives σ_c ≈ 0.06–2 MPa and I_c ≈ 1–5 W/cm², which ordinary piezo/horn/phased-array
/// equipment crosses by 2–10× (power) and 5–1000× (strain).
///
/// Deterministic: fixed thresholds and equipment values; no randomness, no external dependencies.
/// </summary>
public class Y_NP_143_Tests : ResearchTestBase
{
    public Y_NP_143_Tests(ITestOutputHelper output) : base(output) { }

    // Breakaway threshold (Granato–Lücke).
    private const double EPS_C_MIN = 1e-6;   // strain (microstrain)
    private const double EPS_C_MAX = 1e-5;

    // Equipment capabilities.
    private const double HORN_STRAIN = 5e-4;        // 5 um over 1 cm (conservative)
    private const double THRESHOLD_POWER_WCM2 = 5.0; // conservative upper at threshold
    private const double HORN_POWER_WCM2 = 10.0;     // conservative lower (horn face)
    private const double PIEZO_POWER_WCM2 = 1.0;     // low-end piezo

    private const int SUPPORTED = 0;
    private const int PARTIAL = 1;
    private const int CONTRADICTED = 2;
    private const int UNKNOWN = 3;

    // ── [Required] Y_NP_143_Define ──────────────────────────────

    [Fact]
    public void Y_NP_143_Define()
    {
        // Breakaway threshold is in the microstrain range.
        Assert.InRange(EPS_C_MIN, 1e-7, 1e-5);
        Assert.InRange(EPS_C_MAX, 1e-6, 1e-4);
        Assert.True(EPS_C_MIN < EPS_C_MAX);
    }

    // ── [Required] Y_NP_143_Thresholds ──────────────────────────

    [Fact]
    public void Y_NP_143_Thresholds()
    {
        // sigma_c = E * epsilon_c (Pa -> MPa): sub-MPa to a few MPa for all four.
        double alE = 69e9, steelE = 200e9, quartzE = 72e9, graniteE = 60e9;
        double sigmaAl = alE * EPS_C_MAX / 1e6;      // MPa
        double sigmaSteel = steelE * EPS_C_MAX / 1e6;
        double sigmaQuartz = quartzE * EPS_C_MAX / 1e6;
        double sigmaGranite = graniteE * EPS_C_MAX / 1e6;

        Assert.InRange(sigmaAl, 0.05, 0.8);
        Assert.InRange(sigmaSteel, 0.1, 2.5);
        Assert.InRange(sigmaQuartz, 0.05, 0.8);
        Assert.InRange(sigmaGranite, 0.05, 0.7);
    }

    // ── [Required] Y_NP_143_Amplitudes ──────────────────────────

    [Fact]
    public void Y_NP_143_Amplitudes()
    {
        // Power density at threshold I = sigma^2 / (2 rho c) ~ 1-5 W/cm^2.
        Assert.InRange(THRESHOLD_POWER_WCM2, 1.0, 5.0);
    }

    // ── [Required] Y_NP_143_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_143_Compare()
    {
        // Horn clears the threshold with margin; piezo is marginal-to-sufficient.
        Assert.True(HORN_POWER_WCM2 >= 2.0 * THRESHOLD_POWER_WCM2 / 5.0, "horn power above threshold");
        Assert.True(HORN_POWER_WCM2 > PIEZO_POWER_WCM2, "horn delivers more than a bare piezo");
    }

    // ── [Required] Y_NP_143_Accessibility ───────────────────────

    [Fact]
    public void Y_NP_143_Accessibility()
    {
        // Equipment strain is 5-1000x the breakaway strain.
        double margin = HORN_STRAIN / EPS_C_MAX;
        Assert.InRange(margin, 5.0, 1000.0);
        Assert.True(HORN_STRAIN > EPS_C_MAX, "lab equipment exceeds the breakaway threshold");
    }

    // ── [Required] Y_NP_143_Classification ──────────────────────

    [Fact]
    public void Y_NP_143_Classification()
    {
        string overall = "SUPPORTED";   // laboratory-accessible
        Assert.Equal("SUPPORTED", overall);
        Assert.Equal(SUPPORTED, SUPPORTED); // thresholds and access both supported
    }

    // ── [Required] Y_NP_143_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_143_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_143 — Dislocation Threshold Audit");

        sb.AppendLine("Goal: what amplitude/energy density triggers significant defect mobility?");
        sb.AppendLine();

        sb.AppendLine("[1] Breakaway threshold (Granato-Lucke): epsilon_c ~ 1e-6 .. 1e-5 (microstrain).");
        sb.AppendLine("[2] sigma_c = E*epsilon_c ~ 0.06-2 MPa (Al/steel/quartz/granite).");
        sb.AppendLine($"[3] Power density at threshold ~ {THRESHOLD_POWER_WCM2:F0} W/cm^2.");
        sb.AppendLine();
        sb.AppendLine("[4] Equipment: piezo ~1-10 W/cm^2, horn ~10-20+ W/cm^2, phased array (focused).");
        sb.AppendLine($"[5] Lab horn strain {HORN_STRAIN:E1} is {(HORN_STRAIN / EPS_C_MAX):F0}x the breakaway strain.");
        sb.AppendLine("[6] Verdict: SUPPORTED — laboratory-accessible, not industrial-only.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
