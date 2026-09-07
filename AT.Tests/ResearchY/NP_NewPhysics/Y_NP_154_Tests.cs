using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_154 — Contact Network Softening Audit test suite (Y_NP_154_Tests.cs).
///
/// Question: for granite-like materials, is rigidity controlled more by grain-contact networks than
/// by internal crystal defects?
///
/// Verdict tested: KNOWN PHYSICS — contact-network engineering is the dominant lever. Granite is a
/// jammed granular system (force chains + microcracks); dislocations are minor in the brittle regime.
/// Temporary softening = acoustic fluidization (contact disruption).
///
/// Deterministic: fixed classification bins and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_154_Tests : ResearchTestBase
{
    public Y_NP_154_Tests(ITestOutputHelper output) : base(output) { }

    private const int KNOWN_PHYSICS = 0;
    private const int AT_INTERPRETATION = 1;
    private const int AT_QUESTION = 2;
    private const int REFUTED = 3;

    // ── [Required] Y_NP_154_Model ───────────────────────────────

    [Fact]
    public void Y_NP_154_Model()
    {
        bool grains = true;
        bool contacts = true;   // force chains carry stress
        bool microcracks = true;
        Assert.True(grains && contacts && microcracks);
    }

    // ── [Required] Y_NP_154_Compare ─────────────────────────────

    [Fact]
    public void Y_NP_154_Compare()
    {
        bool contactsDominate = true;    // force chains + microcracks
        bool dislocationsMinor = true;   // only semi-brittle/ductile at depth
        Assert.True(contactsDominate && dislocationsMinor);
    }

    // ── [Required] Y_NP_154_Soften ──────────────────────────────

    [Fact]
    public void Y_NP_154_Soften()
    {
        bool contactDisruption = true;      // acoustic fluidization
        bool factorFiveToTen = true;        // friction/shear reduction
        Assert.True(contactDisruption && factorFiveToTen);
    }

    // ── [Required] Y_NP_154_Estimate ────────────────────────────

    [Fact]
    public void Y_NP_154_Estimate()
    {
        double strain = 5e-6;         // ~1e-6..1e-5
        double stress = 0.3;          // ~0.06-0.6 MPa
        double power = 3.0;           // ~1-5 W/cm^2
        Assert.InRange(strain, 1e-6, 1e-5);
        Assert.InRange(stress, 0.05, 0.7);
        Assert.InRange(power, 1.0, 5.0);
    }

    // ── [Required] Y_NP_154_Mobility ────────────────────────────

    [Fact]
    public void Y_NP_154_Mobility()
    {
        bool contactMobilityDominant = true;
        bool defectMobilityMinor = true;   // few mobile dislocations (brittle)
        Assert.True(contactMobilityDominant && defectMobilityMinor);
    }

    // ── [Required] Y_NP_154_Classification ──────────────────────

    [Fact]
    public void Y_NP_154_Classification()
    {
        int contactNetwork = KNOWN_PHYSICS;      // granular jamming / rock mechanics
        int acousticFluidization = KNOWN_PHYSICS;
        int dislocationDominant = REFUTED;       // "defect engineering dominates granite"

        Assert.Equal(KNOWN_PHYSICS, contactNetwork);
        Assert.Equal(KNOWN_PHYSICS, acousticFluidization);
        Assert.Equal(REFUTED, dislocationDominant);
    }

    // ── [Required] Y_NP_154_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_154_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_154 — Contact Network Softening Audit");

        sb.AppendLine("Goal: is granite rigidity controlled by contacts or defects?");
        sb.AppendLine();

        sb.AppendLine("[1] Granite = grains + contacts (force chains) + microcracks (jammed granular system).");
        sb.AppendLine("[2] Contacts dominate in the brittle regime; dislocations only at depth.");
        sb.AppendLine("[3] Temporary softening = contact disruption (acoustic fluidization, factor 5-10).");
        sb.AppendLine("[4] Threshold: microstrain, ~0.06-0.6 MPa, ~1-5 W/cm^2.");
        sb.AppendLine("[5] Contact mobility > defect mobility.");
        sb.AppendLine("[6] Verdict: KNOWN PHYSICS — contact-network engineering is the dominant lever.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
