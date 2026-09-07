using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_151 — AT Contribution Audit test suite (Y_NP_151_Tests.cs).
///
/// Question: which conclusions of NP_131–150 require Actualization Theory?
///
/// Verdict tested: essentially none of the experimentally supported results require AT — they are
/// known physics. AT contributed an INTERPRETATION (critical modes = soft modes), a REFUTED prediction
/// set (quantized ladder, phase-coherence resource), and one UNTESTED unique prediction (fixed-T
/// order-preserving elastic dial).
///
/// Deterministic: fixed classification bins and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_151_Tests : ResearchTestBase
{
    public Y_NP_151_Tests(ITestOutputHelper output) : base(output) { }

    private const int KNOWN_PHYSICS = 0;
    private const int AT_INTERPRETATION = 1;
    private const int AT_PREDICTION = 2;
    private const int AT_UNIQUE_PREDICTION = 3;

    // ── [Required] Y_NP_151_Inventory ───────────────────────────

    [Fact]
    public void Y_NP_151_Inventory()
    {
        // 20 audits, NP_131..NP_150.
        int[] ids = Enumerable.Range(131, 20).ToArray();
        Assert.Equal(20, ids.Length);
        Assert.Equal(131, ids[0]);
        Assert.Equal(150, ids[19]);
    }

    // ── [Required] Y_NP_151_Classify ────────────────────────────

    [Fact]
    public void Y_NP_151_Classify()
    {
        int acousticSoftening = KNOWN_PHYSICS;      // Langenecker
        int softModes = KNOWN_PHYSICS;              // Cochran
        int peeningDefectEngineering = KNOWN_PHYSICS; // UNSM/USRP
        int masterKeyFraming = AT_INTERPRETATION;   // critical modes = soft modes
        int quantizedLadder = AT_PREDICTION;        // refuted (NP_138)
        int phaseCoherenceResource = AT_PREDICTION; // contradicted (NP_142)
        int fixedTDial = AT_UNIQUE_PREDICTION;      // untested (NP_140)

        Assert.Equal(KNOWN_PHYSICS, acousticSoftening);
        Assert.Equal(KNOWN_PHYSICS, softModes);
        Assert.Equal(KNOWN_PHYSICS, peeningDefectEngineering);
        Assert.Equal(AT_INTERPRETATION, masterKeyFraming);
        Assert.Equal(AT_PREDICTION, quantizedLadder);
        Assert.Equal(AT_PREDICTION, phaseCoherenceResource);
        Assert.Equal(AT_UNIQUE_PREDICTION, fixedTDial);
    }

    // ── [Required] Y_NP_151_RemoveAT ────────────────────────────

    [Fact]
    public void Y_NP_151_RemoveAT()
    {
        bool acousticSofteningSurvives = true;    // yes (Langenecker)
        bool softModesSurvive = true;             // yes (Cochran)
        bool peeningSurvives = true;              // yes (manufacturing)
        bool masterKeySurvives = false;           // no (AT label)
        bool phaseCoherenceSurvives = false;      // no (contradicted)
        bool quantizedLadderSurvives = false;     // no (refuted)

        Assert.True(acousticSofteningSurvives && softModesSurvive && peeningSurvives);
        Assert.False(masterKeySurvives || phaseCoherenceSurvives || quantizedLadderSurvives);
    }

    // ── [Required] Y_NP_151_Unique ──────────────────────────────

    [Fact]
    public void Y_NP_151_Unique()
    {
        bool fixedTDialUnique = true;      // the only surviving AT-unique content
        bool fixedTDialUntested = true;    // UNKNOWN, not confirmed
        Assert.True(fixedTDialUnique && fixedTDialUntested);
    }

    // ── [Required] Y_NP_151_Classification ──────────────────────

    [Fact]
    public void Y_NP_151_Classification()
    {
        // No confirmed new physics emerged; AT = reinterpretation + refuted/untested predictions.
        bool confirmedNewPhysics = false;
        bool knownPhysicsDominates = true;
        Assert.False(confirmedNewPhysics);
        Assert.True(knownPhysicsDominates);
    }

    // ── [Required] Y_NP_151_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_151_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_151 — AT Contribution Audit");

        sb.AppendLine("Goal: which NP_131-150 conclusions require Actualization Theory?");
        sb.AppendLine();

        sb.AppendLine("[1] Inventory: 20 audits (NP_131..NP_150).");
        sb.AppendLine("[2] Classification:");
        sb.AppendLine("    KNOWN PHYSICS: acoustic softening, soft modes, dislocation/defect engineering, peening.");
        sb.AppendLine("    AT INTERPRETATION: 'critical mode / master key' = soft modes re-labeled.");
        sb.AppendLine("    AT PREDICTION: quantized ladder (REFUTED), phase-coherence resource (CONTRADICTED).");
        sb.AppendLine("    AT UNIQUE PREDICTION: fixed-T order-preserving elastic dial (UNTESTED).");
        sb.AppendLine();
        sb.AppendLine("[3] Remove AT: supported results survive; AT-specific content does not.");
        sb.AppendLine("[4] No confirmed new physics emerged from this chain.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
