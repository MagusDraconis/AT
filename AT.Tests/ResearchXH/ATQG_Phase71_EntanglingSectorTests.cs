using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 71 — origin of the entangling sector. Determines the minimal additional link content for entanglement.
/// Classify: DERIVED / COMPATIBLE / NEW SECTOR.
///
/// Tests: ATQG710 (phase ≠ non-separability), ATQG711 (joint link state), ATQG712 (classification).
/// </summary>
public class ATQG_Phase71_EntanglingSectorTests : ResearchTestBase
{
    public ATQG_Phase71_EntanglingSectorTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG710: a single phase does not produce non-separability ──────────────────

    [Fact]
    public void ATQG710_PhaseVsNonSeparability()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG710: a single-DOF phase gives interference, not non-separability");

        bool phase = EntanglingSector.PhaseGivesNonSeparability();

        sb.AppendLine($"phase θ produces non-separability (entanglement): {phase}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: e^{iθ} is a SINGLE-degree-of-freedom amplitude — it gives interference (QG65) but is SEPARABLE.");
        sb.AppendLine("Non-separability requires a JOINT state across TWO degrees of freedom, which a single phase cannot supply.");
        Output.WriteLine(sb.ToString());

        Assert.False(phase, "a single phase should not give non-separability");
    }

    // ── ATQG711: the joint link state ──────────────────────────────────────────────

    [Fact]
    public void ATQG711_JointLinkState()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG711: the minimal addition is a joint (2-qubit) state on the link");

        bool joint = EntanglingSector.RequiresJointLinkState();
        bool compatible = EntanglingSector.Compatible();
        bool newSector = EntanglingSector.NewSector();

        sb.AppendLine($"entanglement requires a JOINT (2-qubit) link state: {joint}");
        sb.AppendLine($"the joint state is COMPATIBLE with the link (a pair): {compatible}");
        sb.AppendLine($"the joint state is a NEW SECTOR:                     {newSector}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the link (connecting exactly two nodes) is the natural home for a JOINT state — e.g. a Bell pair");
        sb.AppendLine("(|00⟩+|11⟩)/√2. This joint state is the minimal additional content, compatible but new.");
        Output.WriteLine(sb.ToString());

        Assert.True(joint, "entanglement should require a joint link state");
        Assert.True(compatible, "the joint state should be compatible");
        Assert.True(newSector, "the joint state should be a new sector");
    }

    // ── ATQG712: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG712_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG712: DERIVED / COMPATIBLE / NEW SECTOR?");

        sb.AppendLine($"CLASSIFICATION: {EntanglingSector.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the joint (non-separable) state is not derivable from the single-DOF phase θ or the spin");
        sb.AppendLine("    structure S.");
        sb.AppendLine("  • COMPATIBLE: the link is the natural home for a joint (pair) state.");
        sb.AppendLine("  • NEW SECTOR: the entangling (joint link state) sector is new content beyond θ + S.");
        sb.AppendLine();
        sb.AppendLine("So the minimal entangling content is a JOINT LINK STATE — a new sector, compatible with the link, that");
        sb.AppendLine("produces Bell-type non-separability.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW SECTOR", EntanglingSector.Classify());
        Assert.True(EntanglingSector.RequiresJointLinkState());
        Assert.True(EntanglingSector.NewSector());
    }
}
