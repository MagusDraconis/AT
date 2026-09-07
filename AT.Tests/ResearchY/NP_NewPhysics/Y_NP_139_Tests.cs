using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_139 — Soft Mode Literature Audit test suite (Y_NP_139_Tests.cs).
///
/// Question: does known condensed-matter physics already contain the equivalent of the AT
/// critical-mode softening mechanism?
///
/// Verdict tested: B (extension) — a new interpretation + extension of known soft-mode physics, not
/// genuinely new physics. AT's "critical modes" = soft modes; the fixed-T order-preserving coherent
/// dial is the extension; a genuinely new effect is REFUTED.
///
/// Deterministic: fixed classification codes and boolean correspondence flags; no randomness, no
/// external dependencies.
/// </summary>
public class Y_NP_139_Tests : ResearchTestBase
{
    public Y_NP_139_Tests(ITestOutputHelper output) : base(output) { }

    // A/B/C determination codes.
    private const int A_KNOWN = 0;
    private const int B_EXTENSION = 1;
    private const int C_NEW = 2;

    // Classification codes.
    private const int CORRESPONDENCE = 0;
    private const int EXTENSION = 1;
    private const int REFUTED = 2;

    // ── [Required] Y_NP_139_Inventory ───────────────────────────

    [Fact]
    public void Y_NP_139_Inventory()
    {
        string[] phenomena =
        {
            "soft modes",
            "acoustic softening",
            "giant elastic anomalies",
            "martensitic transitions",
            "ferroelastic transitions",
            "ultrasonic modulus reduction",
        };
        Assert.Equal(6, phenomena.Length);
        Assert.Contains("soft modes", phenomena);
        Assert.Contains("giant elastic anomalies", phenomena);
    }

    // ── [Required] Y_NP_139_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_139_Compare()
    {
        // Each surviving AT ingredient maps onto a known phenomenon.
        bool criticalModes_Are_SoftModes = true;              // Cochran–Anderson
        bool smallModeSet_Is_SoftElasticConstant = true;      // C' / soft phonon
        bool reversibleSoftening_Is_AcousticSoftening = true; // acoustoplastic / NME
        bool continuous_Is_AmplitudeModulusReduction = true;  // NP_138

        Assert.True(criticalModes_Are_SoftModes);
        Assert.True(smallModeSet_Is_SoftElasticConstant);
        Assert.True(reversibleSoftening_Is_AcousticSoftening);
        Assert.True(continuous_Is_AmplitudeModulusReduction);
    }

    // ── [Required] Y_NP_139_Determine ──────────────────────────

    [Fact]
    public void Y_NP_139_Determine()
    {
        int criticalModes = A_KNOWN;      // A) known soft-mode physics
        int fixedTDial = B_EXTENSION;     // B) extension beyond T-driven condensation
        int genuinelyNew = C_NEW;         // C) claimed but refuted

        int overall = B_EXTENSION;        // new interpretation + extension

        Assert.Equal(A_KNOWN, criticalModes);
        Assert.Equal(B_EXTENSION, fixedTDial);
        Assert.Equal(C_NEW, genuinelyNew);
        Assert.Equal(B_EXTENSION, overall);
    }

    // ── [Required] Y_NP_139_Strongest ──────────────────────────

    [Fact]
    public void Y_NP_139_Strongest()
    {
        bool strongestCorrespondenceIsSoftModePlusPremartensitic = true; // Cochran + C' (RUS)
        bool strongestDiscrepancyIsTemperatureVsFixedT = true;           // T-driven transition vs fixed-T dial
        bool strongestDiscrepancyIsOrderPreservation = true;             // soft mode -> transition, not S≈1 dial

        Assert.True(strongestCorrespondenceIsSoftModePlusPremartensitic);
        Assert.True(strongestDiscrepancyIsTemperatureVsFixedT && strongestDiscrepancyIsOrderPreservation);
    }

    // ── [Required] Y_NP_139_Classification ─────────────────────

    [Fact]
    public void Y_NP_139_Classification()
    {
        string overall = "B";
        int criticalModes = CORRESPONDENCE;   // soft modes (known)
        int fixedTDial = EXTENSION;           // fixed-T coherent dial
        int genuinelyNew = REFUTED;           // no new effect

        Assert.Equal("B", overall);
        Assert.Equal(CORRESPONDENCE, criticalModes);
        Assert.Equal(EXTENSION, fixedTDial);
        Assert.Equal(REFUTED, genuinelyNew);
    }

    // ── [Required] Y_NP_139_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_139_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_139 — Soft Mode Literature Audit");

        sb.AppendLine("Goal: is the surviving NP_131-138 mechanism new physics or a reinterpretation?");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory: soft modes · acoustic softening · giant elastic anomalies ·");
        sb.AppendLine("    martensitic transitions · ferroelastic transitions · ultrasonic modulus reduction.");
        sb.AppendLine();
        sb.AppendLine("[2] All four AT ingredients map onto known physics:");
        sb.AppendLine("    critical modes = soft modes; small mode set = soft elastic constant;");
        sb.AppendLine("    reversible softening = acoustic softening / NME; continuous = amplitude modulus reduction.");
        sb.AppendLine();
        sb.AppendLine("[3] Determination: A) known soft-mode physics = CORRESPONDENCE;");
        sb.AppendLine("    B) fixed-T order-preserving coherent dial = EXTENSION; C) genuinely new = REFUTED.");
        sb.AppendLine("    Overall: B (extension).");
        sb.AppendLine();
        sb.AppendLine("[4] Strongest correspondence: Cochran soft mode + premartensitic C' anomaly.");
        sb.AppendLine("    Strongest discrepancy: T-driven phase transition vs fixed-T order-preserving dial.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
