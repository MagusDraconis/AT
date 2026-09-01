using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using AT.Tests.Shared;

namespace AT.Tests.ResearchXH;

/// <summary>
/// AT-QG Phase 102 — Global Network Solution Space. Determines whether SM parameters are properties of globally
/// consistent network solutions. Classify: NO RELATION / PARTIAL RELATION / SOLUTION-SPACE ORIGIN.
///
/// Tests: ATQG1020 (allowed classes + manifolds), ATQG1021 (topology + correlations + uniqueness), ATQG1022 (classification).
/// </summary>
public class ATQG_Phase102_GlobalSolutionSpaceTests : ResearchTestBase
{
    public ATQG_Phase102_GlobalSolutionSpaceTests(ITestOutputHelper o) : base(o) { }

    // ── ATQG1020: allowed network classes, global consistency manifolds ───────────

    [Fact]
    public void ATQG1020_ClassesAndManifolds()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1020: does a global solution space exist?");

        bool classes = GlobalSolutionSpace.AllowedNetworkClassesExist();
        bool manifold = GlobalSolutionSpace.ConsistencyManifoldExists();

        sb.AppendLine($"global consistency carves out allowed network classes: {classes}");
        sb.AppendLine($"global consistency MANIFOLD (solution space) exists: {manifold}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: global consistency (loops, single-valued metric, triangle inequalities) defines an allowed");
        sb.AppendLine("manifold of globally consistent networks — a solution space exists.");
        Output.WriteLine(sb.ToString());

        Assert.True(classes, "allowed classes exist");
        Assert.True(manifold, "manifold exists");
    }

    // ── ATQG1021: topology, correlations, uniqueness ──────────────────────────────

    [Fact]
    public void ATQG1021_TopologyCorrelationsUniqueness()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1021: solution-space topology, correlations, uniqueness");

        bool topology = GlobalSolutionSpace.SolutionSpaceHasTopology();
        bool correlations = GlobalSolutionSpace.InducesParameterCorrelations();
        bool unique = GlobalSolutionSpace.SolutionIsUnique();
        bool determines = GlobalSolutionSpace.SolutionSpaceDeterminesValues();

        sb.AppendLine($"solution space has a topology (components, dimensionality): {topology}");
        sb.AppendLine($"global consistency induces parameter correlations: {correlations}");
        sb.AppendLine($"solution is UNIQUE: {unique}");
        sb.AppendLine($"solution-space properties DETERMINE SM values: {determines}");
        sb.AppendLine();
        sb.AppendLine("CONCLUSION: the solution space is real and correlates parameters, but it is a large non-unique manifold —");
        sb.AppendLine("nothing selects a unique solution whose properties equal the SM parameters.");
        Output.WriteLine(sb.ToString());

        Assert.True(topology, "topology exists");
        Assert.True(correlations, "correlations induced");
        Assert.False(unique, "solution not unique");
        Assert.False(determines, "does not determine values");
    }

    // ── ATQG1022: classification ──────────────────────────────────────────────────

    [Fact]
    public void ATQG1022_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("ATQG1022: NO RELATION / PARTIAL RELATION / SOLUTION-SPACE ORIGIN?");

        sb.AppendLine($"CLASSIFICATION: {GlobalSolutionSpace.Classify()}");
        sb.AppendLine();
        sb.AppendLine("  • NOT NO RELATION: a real solution space exists and it correlates parameters.");
        sb.AppendLine("  • NOT SOLUTION-SPACE ORIGIN: the solution space is non-unique and does not determine specific values.");
        sb.AppendLine("  • PARTIAL RELATION: coherent global organizing principle without value determination.");
        sb.AppendLine();
        sb.AppendLine("So the global solution space gives a PARTIAL RELATION to parameters (organizing, not solution-space origin).");
        Output.WriteLine(sb.ToString());

        Assert.Equal("PARTIAL RELATION", GlobalSolutionSpace.Classify());
        Assert.True(GlobalSolutionSpace.ConsistencyManifoldExists());
        Assert.False(GlobalSolutionSpace.SolutionSpaceDeterminesValues());
    }
}
