using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 63 — physical location of the quantum phase. Determines where the U(1) phase lives.
/// Classify: NODES / LINKS / LOOPS / new object.
///
/// Tests: ATQG630 (the three homes), ATQG631 (links canonical, loops derived), ATQG632 (classification).
/// </summary>
public class ATQG_Phase63_PhaseLocationTests : ResearchTestBase
{
    public ATQG_Phase63_PhaseLocationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG630: the three natural homes ────────────────────────────────────────────

    [Fact]
    public void ATQG630_ThreeHomes()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG630: nodes (matter), links (gauge), loops (derived)");

        bool matterOnNodes = PhaseLocation.MatterPhaseOnNodes();
        bool gaugeOnLinks = PhaseLocation.GaugePhaseOnLinks();
        bool loopDerived = PhaseLocation.LoopHolonomyDerived();
        bool newObject = PhaseLocation.RequiresNewObject();

        sb.AppendLine($"matter wavefunction phase on NODES: {matterOnNodes}");
        sb.AppendLine($"gauge connection phase on LINKS:    {gaugeOnLinks}");
        sb.AppendLine($"loop holonomies DERIVED from links:  {loopDerived}");
        sb.AppendLine($"a NEW object is required:            {newObject}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the phase has three natural homes — matter phases on nodes, gauge phases on links, and loop");
        sb.AppendLine("holonomies derived from links. No new object is needed.");
        Output.WriteLine(sb.ToString());

        Assert.True(matterOnNodes, "matter phase should be on nodes");
        Assert.True(gaugeOnLinks, "gauge phase should be on links");
        Assert.True(loopDerived, "loop holonomy should be derived");
        Assert.False(newObject, "no new object should be needed");
    }

    // ── ATQG631: links are canonical, loops derived ─────────────────────────────────

    [Fact]
    public void ATQG631_LinksCanonicalLoopsDerived()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG631: the gauge connection is canonically a link variable");

        sb.AppendLine("In lattice gauge theory:");
        sb.AppendLine("  • the gauge connection A_ij = e^(i θ_ij) is a LINK (edge) variable;");
        sb.AppendLine("  • the Wilson loop (loop holonomy) is the PRODUCT of link phases around a closed loop — derived,");
        sb.AppendLine("    gauge-invariant, and the physical observable (interference/Aharonov-Bohm phase);");
        sb.AppendLine("  • matter wavefunction phases sit on NODES (vertices).");
        sb.AppendLine();
        sb.AppendLine("So the FUNDAMENTAL phase lives on LINKS; loops are its derived observables; nodes carry matter phases.");
        Output.WriteLine(sb.ToString());

        Assert.True(PhaseLocation.GaugePhaseOnLinks());
        Assert.True(PhaseLocation.LoopHolonomyDerived());
    }

    // ── ATQG632: classification ───────────────────────────────────────────────────────

    [Fact]
    public void ATQG632_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG632: NODES / LINKS / LOOPS / new object?");

        sb.AppendLine($"CLASSIFICATION: {PhaseLocation.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • LINKS (canonical): the U(1) gauge connection is a link variable — the gauge phase naturally lives on the");
        sb.AppendLine("    links, consistent with QG60 (gauge fields on links).");
        sb.AppendLine("  • NODES (matter): matter wavefunction phases live on the nodes.");
        sb.AppendLine("  • LOOPS (derived): loop holonomies are the gauge-invariant observables (interference phases), derived from");
        sb.AppendLine("    link phases, not independent objects.");
        sb.AppendLine("  • NO new object: the existing network (nodes + links) suffices to host the phase.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("LINKS", PhaseLocation.Classify());
        Assert.True(PhaseLocation.GaugePhaseOnLinks());
        Assert.False(PhaseLocation.RequiresNewObject());
    }
}
