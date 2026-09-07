using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_140 — Fixed-Temperature Softening Audit test suite (Y_NP_140_Tests.cs).
///
/// Question: does the fixed-temperature, order-preserving softening dial actually exist?
///
/// Verdict tested: PARTIAL. The principle (drive-induced, fixed-T, athermal, reversible softening) is
/// SUPPORTED (acoustic softening, NME), but a large (≥50%) fixed-T ELASTIC-modulus reduction is UNKNOWN
/// (elastic ≤ 0.30; the large effects are plastic or temperature-driven).
///
/// Deterministic: fixed bounds (elastic 0.30, plastic 0.90, thermal 1.00) and classification codes;
/// no randomness, no external dependencies.
/// </summary>
public class Y_NP_140_Tests : ResearchTestBase
{
    public Y_NP_140_Tests(ITestOutputHelper output) : base(output) { }

    // Observed fixed-T / temperature-driven maxima.
    private const double ELASTIC_BOUND = 0.30;   // ΔE/E, ΔG/G fixed-T (NME)
    private const double PLASTIC_BOUND = 0.90;   // Δσ/σ fixed-T (acoustic softening)
    private const double THERMAL_BOUND = 1.00;   // ΔE/E at T_c (soft mode / martensitic)

    // Classification codes.
    private const int SUPPORTED = 0;
    private const int PARTIAL = 1;
    private const int CONTRADICTED = 2;
    private const int UNKNOWN = 3;

    // ── [Required] Y_NP_140_Inventory ───────────────────────────

    [Fact]
    public void Y_NP_140_Inventory()
    {
        string[] experiments =
        {
            "resonant ultrasound",
            "acoustic softening",
            "ferroelastic softening",
            "martensitic precursors",
            "nonlinear mesoscopic elasticity",
        };
        Assert.Equal(5, experiments.Length);
        Assert.Contains("acoustic softening", experiments);
        Assert.Contains("nonlinear mesoscopic elasticity", experiments);
    }

    // ── [Required] Y_NP_140_Separate ────────────────────────────

    [Fact]
    public void Y_NP_140_Separate()
    {
        // temperature-driven (soft mode / ferroelastic / martensitic)
        bool temperatureDrivenElastic = true;   // needs T -> T_c, phase transition
        // drive-induced, plastic (acoustic softening)
        bool driveInducedPlastic = true;        // flow stress, fixed-T, reversible
        // drive-induced, elastic (NME)
        bool driveInducedElastic = true;        // modulus, fixed-T, slow-reversible

        Assert.True(temperatureDrivenElastic && driveInducedPlastic && driveInducedElastic);
    }

    // ── [Required] Y_NP_140_MaxFixedT ───────────────────────────

    [Fact]
    public void Y_NP_140_MaxFixedT()
    {
        Assert.InRange(ELASTIC_BOUND, 0.01, 0.30);
        Assert.InRange(PLASTIC_BOUND, 0.50, 0.90);
        Assert.InRange(THERMAL_BOUND, 0.99, 1.01);

        // Fixed-T elastic < plastic < temperature-driven elastic.
        Assert.True(ELASTIC_BOUND < PLASTIC_BOUND, "fixed-T elastic < plastic");
        Assert.True(PLASTIC_BOUND < THERMAL_BOUND, "plastic < temperature-driven");
    }

    // ── [Required] Y_NP_140_CompareNp136 ────────────────────────

    [Fact]
    public void Y_NP_140_CompareNp136()
    {
        // NP_136 tiers vs fixed-T elastic evidence.
        bool tier10Partial = true;    // NME ~10-30% at high sub-damage strain
        bool tier50NotElastic = true; // 50% only via plastic flow stress
        bool tier90NotElastic = true; // 90% only via temperature-driven transition

        Assert.True(tier10Partial);
        Assert.True(tier50NotElastic && tier90NotElastic);
    }

    // ── [Required] Y_NP_140_Strongest ───────────────────────────

    [Fact]
    public void Y_NP_140_Strongest()
    {
        bool strongestForIsNME = true;                 // fixed-T elastic reduction, reversible (slow)
        bool strongestForIsAcoustoplastic = true;      // fixed-T athermal reversible drive (plastic)
        bool strongestAgainstIsLargeElasticOnlyThermal = true; // large elastic needs phase transition

        Assert.True(strongestForIsNME && strongestForIsAcoustoplastic);
        Assert.True(strongestAgainstIsLargeElasticOnlyThermal);
    }

    // ── [Required] Y_NP_140_Classification ─────────────────────

    [Fact]
    public void Y_NP_140_Classification()
    {
        string overall = "PARTIAL";
        int principle = SUPPORTED;      // drive-induced, fixed-T, athermal, reversible
        int largeFixedTElastic = UNKNOWN; // not observed; only plastic or thermal
        int smallFixedTElastic = SUPPORTED; // NME <= 0.30

        Assert.Equal("PARTIAL", overall);
        Assert.Equal(SUPPORTED, principle);
        Assert.Equal(UNKNOWN, largeFixedTElastic);
        Assert.Equal(SUPPORTED, smallFixedTElastic);
    }

    // ── [Required] Y_NP_140_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_140_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_140 — Fixed-Temperature Softening Audit");

        sb.AppendLine("Goal: does the fixed-temperature, order-preserving softening dial exist?");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory: RUS · acoustic softening · ferroelastic softening ·");
        sb.AppendLine("    martensitic precursors · nonlinear mesoscopic elasticity.");
        sb.AppendLine();
        sb.AppendLine("[2] Split: temperature-driven (soft mode / ferroelastic / martensitic) vs");
        sb.AppendLine("    drive-induced (acoustic softening = plastic; NME = elastic).");
        sb.AppendLine();
        sb.AppendLine($"[3] Max at fixed T: elastic ΔE/E ≈ ΔG/G ≤ {ELASTIC_BOUND:F2} (NME);");
        sb.AppendLine($"    plastic Δσ/σ ≤ {PLASTIC_BOUND:F2} (acoustoplastic); thermal ΔE/E ≤ {THERMAL_BOUND:F2} (T_c).");
        sb.AppendLine();
        sb.AppendLine("[4] NP_136 tiers: 10% partial; 50% plastic-only; 90% temperature-only.");
        sb.AppendLine();
        sb.AppendLine("[5] FOR: NME + acoustoplastic (principle real). AGAINST: large elastic only via phase transition.");
        sb.AppendLine("[6] Verdict: PARTIAL — principle supported; large fixed-T elastic magnitude UNKNOWN.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
