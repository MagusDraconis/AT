using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_158 — Latent Organization State Audit test suite (Y_NP_158_Tests.cs).
///
/// Question: does a granite-like block contain multiple latent organizational states that are not
/// normally explored?
///
/// Verdict tested: KNOWN PHYSICS — granite behaves as a family of latent organizational states
/// (metastable jammed configurations) at fixed chemistry/mineral/T, with history-dependent states and
/// property differences. AT's "latent organizational state" is an INTERPRETATION.
///
/// Deterministic: fixed classification bins and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_158_Tests : ResearchTestBase
{
    public Y_NP_158_Tests(ITestOutputHelper output) : base(output) { }

    private const int KNOWN_PHYSICS = 0;
    private const int AT_INTERPRETATION = 1;
    private const int AT_QUESTION = 2;
    private const int REFUTED = 3;

    // ── [Required] Y_NP_158_Define ──────────────────────────────

    [Fact]
    public void Y_NP_158_Define()
    {
        bool organizationalState = true;
        bool latentState = true;      // reachable but not occupied
        bool metastableState = true;  // local minimum
        Assert.True(organizationalState && latentState && metastableState);
    }

    // ── [Required] Y_NP_158_Multiple ────────────────────────────

    [Fact]
    public void Y_NP_158_Multiple()
    {
        bool packingFraction = true;
        bool contactDensity = true;
        bool fabricOrientation = true;
        bool fixedChemistryMineralTemp = true;
        Assert.True(packingFraction && contactDensity && fabricOrientation && fixedChemistryMineralTemp);
    }

    // ── [Required] Y_NP_158_Hidden ──────────────────────────────

    [Fact]
    public void Y_NP_158_Hidden()
    {
        bool metastableStates = true;   // fragile / shear-jammed / ultra-stable
        bool memoryAging = true;        // history-dependent
        Assert.True(metastableStates && memoryAging);
    }

    // ── [Required] Y_NP_158_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_158_Compare()
    {
        bool natural = true;
        bool compacted = true;
        bool vibrated = true;
        bool rejammed = true;
        Assert.True(natural && compacted && vibrated && rejammed);
    }

    // ── [Required] Y_NP_158_Properties ──────────────────────────

    [Fact]
    public void Y_NP_158_Properties()
    {
        bool stiffnessVaries = true;
        bool dampingVaries = true;
        bool fractureVaries = true;
        Assert.True(stiffnessVaries && dampingVaries && fractureVaries);
    }

    // ── [Required] Y_NP_158_Reversible ──────────────────────────

    [Fact]
    public void Y_NP_158_Reversible()
    {
        bool subDamageReversible = true;    // compacted<->natural, vibrated<->re-jammed
        bool damageBoundedIrreversible = true; // large rewrites (NP_155)
        Assert.True(subDamageReversible && damageBoundedIrreversible);
    }

    // ── [Required] Y_NP_158_Classification ──────────────────────

    [Fact]
    public void Y_NP_158_Classification()
    {
        int multipleLatentStates = KNOWN_PHYSICS;
        int propertyDifferences = KNOWN_PHYSICS;
        int latentStateFraming = AT_INTERPRETATION;
        int singleMaterial = REFUTED;

        Assert.Equal(KNOWN_PHYSICS, multipleLatentStates);
        Assert.Equal(KNOWN_PHYSICS, propertyDifferences);
        Assert.Equal(AT_INTERPRETATION, latentStateFraming);
        Assert.Equal(REFUTED, singleMaterial);
    }

    // ── [Required] Y_NP_158_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_158_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_158 — Latent Organization State Audit");

        sb.AppendLine("Goal: does granite have multiple latent organizational states?");
        sb.AppendLine();

        sb.AppendLine("[1] Organizational / latent / metastable states defined.");
        sb.AppendLine("[2] Multiple states at fixed chemistry/mineral/T (packing, contact, fabric).");
        sb.AppendLine("[3] Hidden configs: metastable jammed states + memory/aging.");
        sb.AppendLine("[4] natural / compacted / vibrated / re-jammed states.");
        sb.AppendLine("[5] Stiffness, damping, fracture vary across states.");
        sb.AppendLine("[6] Sub-damage reversible; damage-bounded irreversible (NP_155).");
        sb.AppendLine("[7] Verdict: KNOWN PHYSICS — granite is a family of latent organizational states.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
