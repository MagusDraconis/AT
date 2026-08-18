using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 80 — Why three generations? Determines whether the 3-generation count is related to the network
/// structure that hosts color. Classify: DERIVED / PREFERRED / NEW POSTULATE.
///
/// Tests: TQMQG800 (spin replication + topological families), TQMQG801 (link sectors + color connection),
/// TQMQG802 (classification).
/// </summary>
public class TQMQG_Phase80_WhyThreeGenerationsTests : ResearchTestBase
{
    public TQMQG_Phase80_WhyThreeGenerationsTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG800: replication of spin structures, topological families ─────────────

    [Fact]
    public void TQMQG800_ReplicationAndTopology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG800: do spin structures replicate / do topological families give 3?");

        bool replicates = WhyThreeGenerations.SpinStructureReplicates();
        bool topological = WhyThreeGenerations.TopologicalFamilyCountDerived();

        sb.AppendLine($"spin structure S replicates into multiple generations: {replicates}");
        sb.AppendLine($"family count derivable from a topological invariant: {topological}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the SU(2) spin structure produces a SINGLE spin-1/2 representation; it does NOT replicate");
        sb.AppendLine("into three copies. No topological invariant of the network yields three families.");
        Output.WriteLine(sb.ToString());

        Assert.False(replicates, "spin structure should not replicate");
        Assert.False(topological, "no topological family count");
    }

    // ── TQMQG801: link sectors and color-generation connection ─────────────────────

    [Fact]
    public void TQMQG801_SectorsAndColorConnection()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG801: link-sector multiplicity and the color-generation connection");

        int sectors = WhyThreeGenerations.LinkSectorCount();
        bool map = WhyThreeGenerations.LinkSectorsMapToGenerations();
        bool linked = WhyThreeGenerations.ColorAndGenerationLinked();
        bool forced = WhyThreeGenerations.MinimalFamilyCountForced();
        int generations = WhyThreeGenerations.GenerationCount();
        int colors = WhyThreeGenerations.ColorCount();

        sb.AppendLine($"link irreducible sector count (ρ, ψ, θ, S, J) = {sectors}");
        sb.AppendLine($"5 sectors map to 3 generations: {map}");
        sb.AppendLine($"color N={colors} and generation N={generations} causally linked: {linked}");
        sb.AppendLine($"minimal family count FORCED by the network: {forced}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the link has 5 sectors, not 3; they do not map to generations. Color's 3 is a GAUGE");
        sb.AppendLine("(horizontal) symmetry; generations are a FLAVOR multiplicity (3 vertical mass replicas). The two 3s");
        sb.AppendLine("are COINCIDENTAL, not causally linked. Nothing forces a minimal family count.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(5, sectors);
        Assert.False(map, "link sectors should not map to generations");
        Assert.False(linked, "color and generation counts are coincidental");
        Assert.False(forced, "no minimal family count is forced");
        Assert.Equal(3, generations);
        Assert.Equal(3, colors);
    }

    // ── TQMQG802: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG802_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG802: DERIVED / PREFERRED / NEW POSTULATE?");

        sb.AppendLine($"CLASSIFICATION: {WhyThreeGenerations.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: nothing in the network (spin structure, topology, link sectors) yields 3 generations.");
        sb.AppendLine("  • NOT PREFERRED: there is no structural reason the family count should equal 3 (unlike color, where");
        sb.AppendLine("    N=3 colors uniquely force SU(3)); the generation count has no such selection.");
        sb.AppendLine("  • NEW POSTULATE: the 3-generation count is a new postulate, COINCIDENTAL with the 3-color postulate");
        sb.AppendLine("    but not derived from it.");
        sb.AppendLine();
        sb.AppendLine("So Why three generations? The count 3 is postulated; it is not the same network structure that hosts color.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("NEW POSTULATE", WhyThreeGenerations.Classify());
        Assert.False(WhyThreeGenerations.ColorAndGenerationLinked());
        Assert.False(WhyThreeGenerations.SpinStructureReplicates());
    }
}
