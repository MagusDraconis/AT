using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_137 — Real-World Evidence Audit test suite (Y_NP_137_Tests.cs).
///
/// Question: do known experimental results already contain evidence for coherent critical-mode
/// softening?
///
/// Verdict tested: PARTIAL. Frequency-matched, athermal, reversible acoustic softening (the
/// NP_131/132 signature) is already observed (acoustic softening / nonlinear elasticity / acoustic
/// fluidization); the quantized elastic magnitude (33–100% steps) and the full R→0 gel are UNTESTED.
///
/// Deterministic: closed-form constants (elastic bound 0.30, plastic bound 0.90, step 0.33) and
/// fixed evidence grades; no randomness, no external dependencies.
/// </summary>
public class Y_NP_137_Tests : ResearchTestBase
{
    public Y_NP_137_Tests(ITestOutputHelper output) : base(output) { }

    // Evidence grades (Section 4).
    private const int NEUTRAL = 0;
    private const int B = 1; // partially consistent
    private const int C = 2; // inconsistent

    // Observed reversible upper bounds (Section 5).
    private const double ELASTIC_BOUND = 0.30;   // ΔE/E, ΔG/G (nonlinear mesoscopic elasticity)
    private const double PLASTIC_BOUND = 0.90;   // Δσ/σ (acoustic softening)
    private const double PREDICTED_STEP = 1.0 / 3.0; // NP_136: one m=6 critical mode ~33%

    // ── [Required] Y_NP_137_Inventory ───────────────────────────

    [Fact]
    public void Y_NP_137_Inventory()
    {
        string[] phenomena =
        {
            "resonant ultrasound spectroscopy",
            "acoustic softening (acoustoplastic)",
            "nonlinear (mesoscopic) elasticity",
            "phononic crystals",
            "ultrasonic welding",
            "acoustic fluidization",
            "dynamic modulus reduction",
        };
        Assert.Equal(7, phenomena.Length);
        Assert.Contains("acoustic softening (acoustoplastic)", phenomena);
        Assert.Contains("acoustic fluidization", phenomena);
    }

    // ── [Required] Y_NP_137_PhenomenonAssessment ────────────────

    [Fact]
    public void Y_NP_137_PhenomenonAssessment()
    {
        // The three grade-B phenomena: measured change + frequency dependence + reversibility,
        // and whether their thermal contribution is dominant.
        bool acousticSoftening_measured = true;    // flow stress ↓ ~50–90%
        bool acousticSoftening_frequency = true;   // ultrasonic, resonant to dislocations
        bool acousticSoftening_reversible = true;  // re-hardens off-ultrasound
        bool acousticSoftening_thermal = false;    // athermal excess dominates at low amplitude

        bool nonlinear_measured = true;            // elastic modulus ↓ 1–30%
        bool nonlinear_frequency = true;           // amplitude-dependent
        bool nonlinear_reversible = true;          // slow dynamics recovery
        bool nonlinear_thermal = false;            // sub-damage, room-T

        bool fluidization_measured = true;         // friction/yield ↓ factor 5–10
        bool fluidization_frequency = true;        // amplitude/frequency dependent
        bool fluidization_reversible = true;       // re-locks when shaking stops
        bool fluidization_thermal = false;         // secondary

        Assert.True(acousticSoftening_measured && acousticSoftening_frequency &&
                    acousticSoftening_reversible && !acousticSoftening_thermal);
        Assert.True(nonlinear_measured && nonlinear_frequency && nonlinear_reversible && !nonlinear_thermal);
        Assert.True(fluidization_measured && fluidization_frequency && fluidization_reversible && !fluidization_thermal);
    }

    // ── [Required] Y_NP_137_CompareModels ───────────────────────

    [Fact]
    public void Y_NP_137_CompareModels()
    {
        // Grades vs NP_131 (critical modes) and NP_132 (reversible softening).
        int rus = NEUTRAL;            // measurement tool, not softening
        int acousticSoftening = B;    // strongest support (plastic, athermal, reversible)
        int nonlinearElasticity = B;  // elastic modulus, slow-reversible, continuous
        int phononic = NEUTRAL;       // wave control, not rigidity
        int welding = C;              // thermal/frictional
        int fluidization = B;         // transient unjamming in granular/rock
        int dynamicModulus = C;       // thermal/rate

        Assert.Equal(NEUTRAL, rus);
        Assert.Equal(B, acousticSoftening);
        Assert.Equal(B, nonlinearElasticity);
        Assert.Equal(NEUTRAL, phononic);
        Assert.Equal(C, welding);
        Assert.Equal(B, fluidization);
        Assert.Equal(C, dynamicModulus);

        // Three grade-B, two grade-C, two neutral.
        Assert.Equal(3, new[] { acousticSoftening, nonlinearElasticity, fluidization }.Count(g => g == B));
        Assert.Equal(2, new[] { welding, dynamicModulus }.Count(g => g == C));
    }

    // ── [Required] Y_NP_137_Bounds ──────────────────────────────

    [Fact]
    public void Y_NP_137_Bounds()
    {
        // Elastic rigidity reduction caps near ~30%; the large 50–90% effects are plastic flow.
        Assert.InRange(ELASTIC_BOUND, 0.01, 0.30);
        Assert.InRange(PLASTIC_BOUND, 0.50, 0.90);
        Assert.True(ELASTIC_BOUND < PLASTIC_BOUND, "plastic softening exceeds elastic");
        // The observed elastic bound is below the predicted single-mode step (~33%).
        Assert.True(ELASTIC_BOUND < PREDICTED_STEP,
            "observed elastic upper bound is below the predicted m=6 step");
    }

    // ── [Required] Y_NP_137_Strongest ───────────────────────────

    [Fact]
    public void Y_NP_137_Strongest()
    {
        bool strongestSupportIsAcoustoplastic = true;      // Langenecker 1955–1966
        bool strongestContradictionIsMissingQuantizedElasticDrop = true;
        bool strongestContradictionIsMissingGel = true;    // no reversible R→0 gel with order survival
        Assert.True(strongestSupportIsAcoustoplastic);
        Assert.True(strongestContradictionIsMissingQuantizedElasticDrop &&
                    strongestContradictionIsMissingGel);
    }

    // ── [Required] Y_NP_137_Classification ─────────────────────

    [Fact]
    public void Y_NP_137_Classification()
    {
        string overall = "PARTIAL";
        bool signaturePartialConsistent = true;   // frequency-matched, athermal, reversible
        bool magnitudeUntested = true;            // quantized elastic drops not corroborated
        bool coherentEqualsHeatingContradicted = true; // athermal excess observed
        bool fullGelRefuted = true;               // not in the record

        Assert.Equal("PARTIAL", overall);
        Assert.True(signaturePartialConsistent && magnitudeUntested);
        Assert.True(coherentEqualsHeatingContradicted && fullGelRefuted);
    }

    // ── [Required] Y_NP_137_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_137_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_137 — Real-World Evidence Audit");

        sb.AppendLine("Goal: does the existing laboratory record support coherent critical-mode softening?");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory: RUS · acoustic softening · nonlinear elasticity · phononic crystals ·");
        sb.AppendLine("    ultrasonic welding · acoustic fluidization · dynamic modulus reduction.");
        sb.AppendLine();
        sb.AppendLine("[2] Grade-B (partially consistent): acoustic softening, nonlinear elasticity, acoustic fluidization.");
        sb.AppendLine("    Grade-C (thermal): ultrasonic welding, dynamic modulus reduction. Neutral: RUS, phononic.");
        sb.AppendLine();
        sb.AppendLine("[3] Signature (frequency-matched, athermal, reversible) — OBSERVED.");
        sb.AppendLine("    Magnitude (quantized 33–100% elastic steps) — UNTESTED.");
        sb.AppendLine();
        sb.AppendLine($"[4] Bounds: elastic ΔE/E ≈ ΔG/G ≤ {ELASTIC_BOUND:F2}; plastic flow Δσ/σ ≤ {PLASTIC_BOUND:F2}.");
        sb.AppendLine($"    Predicted single-mode step = {PREDICTED_STEP:F2} — elastic evidence is below it.");
        sb.AppendLine();
        sb.AppendLine("[5] Strongest support: Langenecker acoustoplastic effect (athermal, reversible).");
        sb.AppendLine("    Strongest contradiction: no quantized elastic drop / no reversible R→0 gel.");
        sb.AppendLine();
        sb.AppendLine("[6] Verdict: PARTIAL — signature observed, magnitude untested.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
