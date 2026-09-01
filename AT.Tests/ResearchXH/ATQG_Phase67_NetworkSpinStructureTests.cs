using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 67 — network spin structure. Determines whether the causal network naturally carries a spin structure.
/// Classify: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE.
///
/// Tests: ATQG670 (orientation ≠ spin structure), ATQG671 (compatible but not present), ATQG672 (classification).
/// </summary>
public class ATQG_Phase67_NetworkSpinStructureTests : ResearchTestBase
{
    public ATQG_Phase67_NetworkSpinStructureTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG670: orientation is not a spin structure ───────────────────────────────

    [Fact]
    public void ATQG670_OrientationVsSpinStructure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG670: graph orientation (Z2) is not a spin structure (double cover)");

        bool orientation = NetworkSpinStructure.OrientationGivesSpinStructure();

        sb.AppendLine($"graph orientation gives a spin structure: {orientation}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: a graph orientation assigns a direction (Z2) to each link; a spin structure is a DOUBLE COVER");
        sb.AppendLine("with a consistent sign on each cycle — a richer (SU(2)/Z2^cycles) object. Orientation alone is insufficient.");
        Output.WriteLine(sb.ToString());

        Assert.False(orientation, "orientation should not give a spin structure");
    }

    // ── ATQG671: compatible but not naturally present ──────────────────────────────

    [Fact]
    public void ATQG671_CompatibleButNotPresent()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG671: the network CAN carry a spin structure, but it is new data");

        bool canCarry = NetworkSpinStructure.CanCarrySpinStructure();
        bool present = NetworkSpinStructure.NaturallyPresent();

        sb.AppendLine($"network CAN carry a spin structure (double cover): {canCarry}");
        sb.AppendLine($"spin structure is NATURALLY present in (V, E):      {present}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network naturally has orientation (Z2) and a U(1) phase, but NOT the double-cover/SU(2) data.");
        sb.AppendLine("A spin structure can be added (compatible), but it is new data, not derivable from the network.");
        Output.WriteLine(sb.ToString());

        Assert.True(canCarry, "the network should be able to carry a spin structure");
        Assert.False(present, "the spin structure should not be naturally present");
    }

    // ── ATQG672: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG672_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG672: DERIVED / COMPATIBLE / REQUIRES NEW PRIMITIVE?");

        sb.AppendLine($"CLASSIFICATION: {NetworkSpinStructure.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: the spin structure (double cover / SU(2)) is not derivable from the scalar + rank-2 + U(1)");
        sb.AppendLine("    content of the network.");
        sb.AppendLine("  • COMPATIBLE: a spin structure (double cover) can be added to the network to host fermions.");
        sb.AppendLine("  • REQUIRES NEW PRIMITIVE: the spin structure (SU(2) spin connection) is new data — fermions need it.");
        sb.AppendLine();
        sb.AppendLine("So the causal network CAN carry a spin structure (compatible), but it is not naturally present — confirming");
        sb.AppendLine("QG66: fermions require a new spin-1/2 (spin structure) primitive.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("REQUIRES NEW PRIMITIVE", NetworkSpinStructure.Classify());
        Assert.True(NetworkSpinStructure.RequiresNewPrimitive());
        Assert.True(NetworkSpinStructure.CanCarrySpinStructure());
    }
}
