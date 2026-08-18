using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 83 — Network Valence Audit. Determines whether preferred link valence (branching degree) can
/// generate a natural multiplicity of 3. Classify: COINCIDENCE / PARTIAL RELATION / COMMON ORIGIN.
///
/// Tests: TQMQG830 (minimal branching + directed connectivity), TQMQG831 (3D embedding + valence + color/family),
/// TQMQG832 (classification).
/// </summary>
public class TQMQG_Phase83_NetworkValenceThreeTests : ResearchTestBase
{
    public TQMQG_Phase83_NetworkValenceThreeTests(ITestOutputHelper o) : base(o) { }

    // ── TQMQG830: minimal stable branching, directed connectivity ──────────────────

    [Fact]
    public void TQMQG830_MinimalBranching()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG830: is there a natural minimal branching degree?");

        int degree = NetworkValenceThree.MinimalBranchingDegree();
        bool isThree = NetworkValenceThree.MinimalBranchingIsThree();

        sb.AppendLine($"minimal NON-TRIVIAL branching degree = {degree}");
        sb.AppendLine($"degree 3 is the minimal genuine branching degree: {isThree}");
        sb.AppendLine();
        sb.AppendLine("CONSIDER: degree 0 = isolated, 1 = leaf, 2 = contractible pass-through (topologically trivial).");
        sb.AppendLine("Degree 3 is where a node first GENUINELY branches (a Y-junction).");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: graph theory DOES single out 3 as the minimal true branching degree — but this is a");
        sb.AppendLine("graph-topology fact, unrelated to gauge/flavor structure.");
        Output.WriteLine(sb.ToString());

        Assert.Equal(3, degree);
        Assert.True(isThree, "minimal branching degree is 3");
    }

    // ── TQMQG831: 3D embedding, valence distributions, color/family relation ───────

    [Fact]
    public void TQMQG831_EmbeddingAndValence()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG831: does valence/dimension determine color or family count?");

        bool valenceColor = NetworkValenceThree.ValenceDeterminesColorCount();
        bool valenceFamily = NetworkValenceThree.ValenceDeterminesFamilyCount();
        bool dimThree = NetworkValenceThree.SpatialDimensionIsThree();
        bool dimColor = NetworkValenceThree.DimensionDeterminesColorCount();
        bool dimFamily = NetworkValenceThree.DimensionDeterminesFamilyCount();
        bool common = NetworkValenceThree.CommonOriginWithColorFamily();

        sb.AppendLine($"valence determines color count N=3: {valenceColor}");
        sb.AppendLine($"valence determines family count N=3: {valenceFamily}");
        sb.AppendLine($"spatial dimension is d=3: {dimThree}");
        sb.AppendLine($"dimension d=3 determines color count: {dimColor}");
        sb.AppendLine($"dimension d=3 determines family count: {dimFamily}");
        sb.AppendLine($"common origin linking valence/dimension to color/family: {common}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: color and generations are INTERNAL (gauge/flavor) structure, independent of valence and");
        sb.AppendLine("spatial embedding. Neither valence 3 nor dimension 3 determines N_color or N_family.");
        Output.WriteLine(sb.ToString());

        Assert.False(valenceColor, "valence does not determine color");
        Assert.False(valenceFamily, "valence does not determine family");
        Assert.True(dimThree, "spatial dimension is 3");
        Assert.False(dimColor, "dimension does not determine color");
        Assert.False(dimFamily, "dimension does not determine family");
        Assert.False(common, "no common origin");
    }

    // ── TQMQG832: classification ───────────────────────────────────────────────────

    [Fact]
    public void TQMQG832_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG832: COINCIDENCE / PARTIAL RELATION / COMMON ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {NetworkValenceThree.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • COINCIDENCE: the number 3 appears in minimal branching degree (graph), spatial dimension (embedding),");
        sb.AppendLine("    color count (gauge), and family count (flavor) — but with NO causal link between them.");
        sb.AppendLine("  • NOT PARTIAL RELATION: there is no partial mechanism connecting valence/dimension to gauge/flavor 3.");
        sb.AppendLine("  • NOT COMMON ORIGIN: color and family counts are not derivable from valence or dimension.");
        sb.AppendLine();
        sb.AppendLine("So the shared number 3 is a numerical COINCIDENCE with no common origin.");
        Output.WriteLine(sb.ToString());

        Assert.Equal("COINCIDENCE", NetworkValenceThree.Classify());
        Assert.False(NetworkValenceThree.CommonOriginWithColorFamily());
        Assert.False(NetworkValenceThree.ValenceDeterminesColorCount());
    }
}
