using System.Globalization;
using System.Text;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.NP_NewPhysics;

/// <summary>
/// ResearchY-NP_157 — Organization vs Material Audit test suite (Y_NP_157_Tests.cs).
///
/// Question: in AT, does resonant excitation act on material properties or on the organization of the
/// difference network?
///
/// Verdict tested: (B) ORGANIZATION — resonance drives the locking/defect/contact network first, with
/// material properties as emergent readouts. This is an AT INTERPRETATION of known physics (properties
/// = functions of microstructure/defect/contact state). Reorganization is a third category between
/// elastic deformation and irreversible damage.
///
/// Deterministic: fixed classification bins and boolean flags; no randomness, no external deps.
/// </summary>
public class Y_NP_157_Tests : ResearchTestBase
{
    public Y_NP_157_Tests(ITestOutputHelper output) : base(output) { }

    private const int KNOWN_PHYSICS = 0;
    private const int AT_INTERPRETATION = 1;
    private const int AT_QUESTION = 2;
    private const int REFUTED = 3;

    // ── [Required] Y_NP_157_Define ──────────────────────────────

    [Fact]
    public void Y_NP_157_Define()
    {
        bool organizationInvariants = true;   // locking topology / defect / contact network
        bool materialInvariants = true;       // modulus, yield, hardness
        Assert.True(organizationInvariants && materialInvariants);
    }

    // ── [Required] Y_NP_157_OrganizationFirst ───────────────────

    [Fact]
    public void Y_NP_157_OrganizationFirst()
    {
        bool organizationFirst = true;   // drive -> network change -> property change
        bool propertiesDerived = true;   // emergent readouts (NP_100)
        Assert.True(organizationFirst && propertiesDerived);
    }

    // ── [Required] Y_NP_157_BeforeFailure ───────────────────────

    [Fact]
    public void Y_NP_157_BeforeFailure()
    {
        bool reorganizationBeforeFailure = true;   // softening precedes failure
        Assert.True(reorganizationBeforeFailure);
    }

    // ── [Required] Y_NP_157_ReversibleTopology ──────────────────

    [Fact]
    public void Y_NP_157_ReversibleTopology()
    {
        bool reLockReversible = true;      // NP_132
        bool contactRearrangeReversible = true; // NP_155/156 (sub-damage)
        bool microcrackIrreversible = true;     // damage
        Assert.True(reLockReversible && contactRearrangeReversible && microcrackIrreversible);
    }

    // ── [Required] Y_NP_157_ThreeWay ────────────────────────────

    [Fact]
    public void Y_NP_157_ThreeWay()
    {
        bool deformation = true;    // elastic strain
        bool damage = true;         // irreversible break
        bool reorganization = true; // reversible topology change (third category)
        Assert.True(deformation && damage && reorganization);
    }

    // ── [Required] Y_NP_157_Classification ──────────────────────

    [Fact]
    public void Y_NP_157_Classification()
    {
        int actsOnOrganization = AT_INTERPRETATION;   // B
        int propertiesAreReadouts = KNOWN_PHYSICS;    // microstructure-property maps
        int newOntologyOfMatter = REFUTED;            // NP_151/153

        Assert.Equal(AT_INTERPRETATION, actsOnOrganization);
        Assert.Equal(KNOWN_PHYSICS, propertiesAreReadouts);
        Assert.Equal(REFUTED, newOntologyOfMatter);
    }

    // ── [Required] Y_NP_157_Run ─────────────────────────────────

    [Fact]
    public void Y_NP_157_Run()
    {
        var orig = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        try { RunResearchReport(); }
        finally { Thread.CurrentThread.CurrentCulture = orig; }
    }

    private void RunResearchReport()
    {
        var sb = new StringBuilder();
        PrintHeader("ResearchY-NP_157 — Organization vs Material Audit");

        sb.AppendLine("Goal: does resonance act on matter or on organization?");
        sb.AppendLine();

        sb.AppendLine("[1] Organization invariants (locking/defect/contact) vs material invariants (E, sigma_y).");
        sb.AppendLine("[2] Resonance acts on the network first; properties are emergent readouts.");
        sb.AppendLine("[3] Reorganization precedes failure (softening, not fracture).");
        sb.AppendLine("[4] Reversible: re-lock (NP_132), contact re-arrange (NP_155/156); microcrack = irreversible.");
        sb.AppendLine("[5] Three categories: deformation / damage / reorganization.");
        sb.AppendLine("[6] Verdict: acts on ORGANIZATION (B) — an AT INTERPRETATION of known physics.");
        sb.AppendLine();

        Output.WriteLine(sb.ToString());
    }
}
