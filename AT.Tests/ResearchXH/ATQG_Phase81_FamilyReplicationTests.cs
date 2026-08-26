using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;
using Xunit.Abstractions;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 81 — Origin of family replication. Determines whether the EXISTENCE of multiple fermion families
/// can emerge from network structure at all. Classify: DERIVED / COMPATIBLE / FUNDAMENTALLY POSTULATED.
///
/// Tests: ATQG810 (spin replication + topology), ATQG811 (link degeneracies + family symmetry + count),
/// ATQG812 (classification).
/// </summary>
public class ATQG_Phase81_FamilyReplicationTests : ResearchTestBase
{
    public ATQG_Phase81_FamilyReplicationTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG810: replicated spin structures, topological sectors ──────────────────

    [Fact]
    public void ATQG810_SpinAndTopology()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG810: does replication emerge from spin structure or topology?");

        bool spin = FamilyReplication.SpinStructureReplicatesFamilies();
        bool topo = FamilyReplication.TopologicalFamiliesEmergent();

        sb.AppendLine($"spin structure S replicates families on its own: {spin}");
        sb.AppendLine($"multiple families emerge from topological sectors: {topo}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: S yields a SINGLE spin-1/2 rep; no topological invariant produces families. Replication");
        sb.AppendLine("does NOT emerge from these mechanisms.");
        Output.WriteLine(sb.ToString());

        Assert.False(spin, "spin structure does not replicate");
        Assert.False(topo, "no topological family emergence");
    }

    // ── ATQG811: link degeneracies, family symmetry, generation count ─────────────

    [Fact]
    public void ATQG811_DegeneraciesAndSymmetry()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG811: can the link host replication (degenerate family index)?");

        bool hostIndex = FamilyReplication.LinkCanHostFamilyIndex();
        bool symAdditional = FamilyReplication.FamilySymmetryIsAdditional();
        bool spontaneous = FamilyReplication.ReplicationEmergesSpontaneously();
        bool countForced = FamilyReplication.FamilyCountForced();

        sb.AppendLine($"link/node can host a degenerate family index: {hostIndex}");
        sb.AppendLine($"family symmetry is ADDITIONAL structure: {symAdditional}");
        sb.AppendLine($"replication emerges SPONTANEOUSLY from (V,E)+sectors: {spontaneous}");
        sb.AppendLine($"family COUNT forced by the network: {countForced}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the network CAN host replication via a discrete internal label (a family index), exactly as");
        sb.AppendLine("it hosts the SU(3) connection. The horizontal family symmetry is additional structure; the count stays");
        sb.AppendLine("free. Replication is ACCOMMODATED, not generated.");
        Output.WriteLine(sb.ToString());

        Assert.True(hostIndex, "link can host a family index");
        Assert.True(symAdditional, "family symmetry is additional");
        Assert.False(spontaneous, "replication is not spontaneous");
        Assert.False(countForced, "count is not forced");
    }

    // ── ATQG812: classification ───────────────────────────────────────────────────

    [Fact]
    public void ATQG812_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG812: DERIVED / COMPATIBLE / FUNDAMENTALLY POSTULATED?");

        sb.AppendLine($"CLASSIFICATION: {FamilyReplication.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT DERIVED: no mechanism (spin, topology, sector count) spontaneously generates multiple families.");
        sb.AppendLine("  • COMPATIBLE: the network can HOST replication — a degenerate family index attaches to the node/link");
        sb.AppendLine("    (as SU(3) attaches to the link), with an additional horizontal family symmetry; no contradiction.");
        sb.AppendLine("  • NOT FUNDAMENTALLY POSTULATED at the level of existence: replication needs no new primitive beyond a");
        sb.AppendLine("    discrete index — though the specific COUNT (3) remains postulatory (QG80).");
        sb.AppendLine();
        sb.AppendLine("So the existence of multiple families is COMPATIBLE with the network (accommodated via a family index),");
        sb.AppendLine("but not DERIVED from it.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("COMPATIBLE", FamilyReplication.Classify());
        Assert.True(FamilyReplication.LinkCanHostFamilyIndex());
        Assert.False(FamilyReplication.ReplicationEmergesSpontaneously());
    }
}
