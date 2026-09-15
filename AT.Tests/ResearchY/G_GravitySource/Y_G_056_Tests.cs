using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.NonScalarSelectionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_056 - Non-Scalar Selection Audit (group G - Gravity Source).
///
/// QUESTION. Can any existing non-scalar AT structure span the 53-dimensional phase sector? Candidates: phase vector
/// field, connection structure, T1/T2 sector coupling, edge-holonomy network, causal-order tensor. Measure the phase
/// rank; require rank > 3.
///
/// ANSWER: **DERIVED - the occupancy-gradient vector field spans the phase sector EXACTLY (53 of 53), and its
/// connection and tensor descendants do too. G_055's ceiling was a ceiling on SCALARS. And spanning is SENSITIVITY, not
/// DETERMINATION: G_054 measured that no AT process runs such a flow.**
/// </summary>
public class Y_G_056_Tests : ResearchTestBase
{
    public Y_G_056_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_056_TheScalarCeilingIsTheBarAndTheGradientStructuresClearIt()
    {
        Assert.Equal(53, PhaseDimension());
        Assert.True(ScalarCeiling() > 0, "G_055's ceiling must be non-zero for the comparison to mean anything");
        Assert.True(TheRequirementIsMet(), $"largest phase rank {LargestPhaseRank()} vs ceiling {ScalarCeiling()}");
        Assert.True(LargestPhaseRank() > ScalarCeiling());
        Assert.True(TheFullSectorIsSpanned(), $"largest phase rank {LargestPhaseRank()}");
        Assert.Equal(PhaseDimension(), LargestPhaseRank());
    }

    [Fact]
    public void Y_G_056_TheGradientBasedCandidatesReachTheFullRank()
    {
        var table = ComputedTable();
        Assert.Equal(5, table.Length);
        Assert.Equal(3, SpanningCandidates().Length);
        Assert.Contains("phase vector field", SpanningCandidates());
        Assert.Contains("connection structure", SpanningCandidates());
        Assert.Contains("T1/T2 sector coupling", SpanningCandidates());
        Assert.All(table.Where(t => t.Verdict == "DERIVED"),
            t => Assert.Equal(PhaseDimension(), t.PhaseRank));
    }

    [Fact]
    public void Y_G_056_TheDecoupledAndPiecewiseConstantCandidatesReachZero()
    {
        Assert.Equal(2, NullCandidates().Length);
        Assert.Contains("edge-holonomy network", NullCandidates());
        Assert.Contains("causal-order tensor", NullCandidates());
        Assert.Equal(0, PhaseRank(EdgeHolonomyNetwork));
        Assert.Equal(0, PhaseRank(CausalOrderTensor));
    }

    [Fact]
    public void Y_G_056_DifferentiationIsInjectiveOnThePhaseSectorAndTheT1HalfVanishes()
    {
        Assert.True(DifferentiationIsInjectiveOnThePhaseSector(),
            $"{PhaseDirectionsWithNonZeroDerivative()} of {PhaseDimension()} directions have a non-zero derivative");
        Assert.Equal(PhaseDimension(), PhaseDirectionsWithNonZeroDerivative());

        // the coupling's antisymmetric half is identically zero because the shifts commute - reported, not hidden
        Assert.True(TheAntisymmetricPartVanishes(), $"T1 norm {AntisymmetricPartNorm():E3}");
    }

    [Fact]
    public void Y_G_056_SpanningIsNotSelectingAndTheVerdictIsDerived()
    {
        Assert.True(NoAtProcessRunsAPhaseFlow());
        Assert.Contains("SENSITIVITY, not DETERMINATION", TheConsistencyClause());
        Assert.Equal("DERIVED", Verdict());
        Assert.Contains("occupancy-gradient", TheFirstSpanningObject());
    }

    [Fact]
    public void Y_G_056_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_056 - Non-Scalar Selection Audit: can a non-scalar AT structure span the phase sector?");

        sb.AppendLine("QUESTION. Can any existing NON-SCALAR AT structure span the 53-dimensional phase sector?");
        sb.AppendLine("GIVEN        G_052 (the interface identity), G_054 (the phases are freely assigned),");
        sb.AppendLine("             G_055 (a scalar constrains at most one phase direction; six measured a rank of 3)");
        sb.AppendLine("CANDIDATES   phase vector field | connection structure | T1/T2 sector coupling |");
        sb.AppendLine("             edge-holonomy network | causal-order tensor");
        sb.AppendLine("REQUIRE      phase rank > 3");
        sb.AppendLine("GOAL         the first AT object whose gradients span more than one phase direction");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The phase rank of a structure is the rank of its derivative along the 53 phase directions, which");
        sb.AppendLine("     generalises G_055's count: a scalar has one output so its rank cannot exceed 1.");
        sb.AppendLine("  2. Each verdict is COMPUTED from the measured rank against G_055's ceiling, not written beside it.");
        sb.AppendLine("  3. Spanning is sensitivity, not determination: G_054's no-process measurement is carried into the");
        sb.AppendLine("     audit explicitly so the series stays consistent.");
        sb.AppendLine("  4. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputTable());
        PrintHeader(OutputWhy());
        PrintHeader(OutputConsistency());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
