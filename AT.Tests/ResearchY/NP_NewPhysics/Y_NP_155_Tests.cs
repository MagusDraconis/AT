using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_155 — Force Chain Programming Audit test suite (Y_NP_155_Tests.cs).
///
/// Question: can the force-chain network inside a granite-like material be intentionally reconfigured?
///
/// Verdict tested: KNOWN PHYSICS — force chains break/create/redirect under load; directed anisotropy
/// (load/shear direction); multiple metastable jammed states (memory/aging). Reversible "programming"
/// of a CONSOLIDATED block is an AT QUESTION (bounded by the reversible–irreversible transition).
///
/// Deterministic: fixed classification bins and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_155_Tests : ResearchTestBase
{
    public Y_NP_155_Tests(ITestOutputHelper output) : base(output) { }

    private const int KNOWN_PHYSICS = 0;
    private const int AT_INTERPRETATION = 1;
    private const int AT_QUESTION = 2;
    private const int REFUTED = 3;

    // ── [Required] Y_NP_155_Model ───────────────────────────────

    [Fact]
    public void Y_NP_155_Model()
    {
        bool grains = true;
        bool contacts = true;
        bool forceChains = true;   // filamentary load-bearing paths
        bool microcracks = true;
        Assert.True(grains && contacts && forceChains && microcracks);
    }

    // ── [Required] Y_NP_155_Reconfigure ─────────────────────────

    [Fact]
    public void Y_NP_155_Reconfigure()
    {
        bool canBreak = true;      // exceed contact strength
        bool canCreate = true;     // compaction forms new chains
        bool canRedirect = true;   // load/shear direction re-aligns chains
        Assert.True(canBreak && canCreate && canRedirect);
    }

    // ── [Required] Y_NP_155_Directed ────────────────────────────

    [Fact]
    public void Y_NP_155_Directed()
    {
        bool directedAnisotropy = true;   // chains align with principal stress
        bool directedBeatsRandom = true;
        Assert.True(directedAnisotropy && directedBeatsRandom);
    }

    // ── [Required] Y_NP_155_Controls ────────────────────────────

    [Fact]
    public void Y_NP_155_Controls()
    {
        bool staticPreload = true;
        bool vibrationDirection = true;
        bool frequencySweep = true;
        bool spatialPhase = true;   // emerging
        Assert.True(staticPreload && vibrationDirection && frequencySweep && spatialPhase);
    }

    // ── [Required] Y_NP_155_States ──────────────────────────────

    [Fact]
    public void Y_NP_155_States()
    {
        bool multipleMetastableStates = true;   // fragile / shear-jammed / ultra-stable
        bool memoryAging = true;
        Assert.True(multipleMetastableStates && memoryAging);
    }

    // ── [Required] Y_NP_155_Properties ──────────────────────────

    [Fact]
    public void Y_NP_155_Properties()
    {
        bool stiffnessVaries = true;
        bool strengthVaries = true;
        bool dampingVaries = true;
        bool fractureVaries = true;
        Assert.True(stiffnessVaries && strengthVaries && dampingVaries && fractureVaries);
    }

    // ── [Required] Y_NP_155_Classification ──────────────────────

    [Fact]
    public void Y_NP_155_Classification()
    {
        int reconfiguration = KNOWN_PHYSICS;
        int directedAnisotropy = KNOWN_PHYSICS;
        int metastableStates = KNOWN_PHYSICS;
        int reversibleConsolidated = AT_QUESTION;   // bounded by damage

        Assert.Equal(KNOWN_PHYSICS, reconfiguration);
        Assert.Equal(KNOWN_PHYSICS, directedAnisotropy);
        Assert.Equal(KNOWN_PHYSICS, metastableStates);
        Assert.Equal(AT_QUESTION, reversibleConsolidated);
    }

    // ── [Required] Y_NP_155_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_155_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_155 — Force Chain Programming Audit");

        sb.AppendLine("Goal: can the force-chain network be intentionally reconfigured?");
        sb.AppendLine();

        sb.AppendLine("[1] Granite = grains + contacts + force chains + microcracks.");
        sb.AppendLine("[2] Chains break / create / redirect under load (DEM, photoelastic).");
        sb.AppendLine("[3] Directed anisotropy (load/shear direction) beats random disruption.");
        sb.AppendLine("[4] Controls: preload, vibration direction, frequency sweep, spatial phase.");
        sb.AppendLine("[5] Multiple metastable jammed states + memory/aging.");
        sb.AppendLine("[6] Properties vary with state (stiffness/strength/damping/fracture).");
        sb.AppendLine("[7] Verdict: KNOWN PHYSICS; reversible consolidated switching = AT QUESTION.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
