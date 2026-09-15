using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseFlowAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_057 - Phase Flow Audit (group G - Gravity Source).
///
/// QUESTION. Can any existing AT PROCESS change the phase coordinates? Candidates: clock flow, acceleration flow, field
/// flow, connection flow, T1/T2 coupling. Measure phase velocity, phase rank, selection power. Goal: the first AT
/// process that generates a phase flow.
///
/// ANSWER: **BOUNDARY - no AT process the theory RUNS changes the phase coordinates, while five POTENTIALS it defines
/// would move them; and a flow needs a potential, a potential is a scalar, so every candidate has phase rank 1 and
/// selection power 1/53.**
/// </summary>
public class Y_G_057_Tests : ResearchTestBase
{
    public Y_G_057_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_057_TheRunningProcessIsPhaseStatic()
    {
        // the actualization supplies no direction with phase content - G_054's measurement restated as a velocity
        Assert.Equal(0.0, ActualizationPhaseVelocity());
        Assert.True(TheRunningProcessIsPhaseStatic());
        Assert.True(UpdateRuleSectors().Spatial < 1e-15, $"spatial part {UpdateRuleSectors().Spatial:E3}");
        Assert.Equal(0, CouplingCensus());
    }

    [Fact]
    public void Y_G_057_TheFivePotentialsDoMoveThePhase()
    {
        var table = FlowTable();
        Assert.Equal(5, table.Length);
        Assert.Equal(5, FlowingCandidates().Length);
        Assert.Empty(StaticCandidates());
        Assert.All(table, t => Assert.True(t.Velocity > VelocityFloor,
            $"{t.Candidate} velocity {t.Velocity:E3}"));
        Assert.All(table, t => Assert.True(t.Fraction > 1e-9, $"{t.Candidate} fraction {t.Fraction:E3}"));
    }

    [Fact]
    public void Y_G_057_TheFieldAndConnectionFlowsAreOneFlow()
    {
        // the derived connection IS the field strength, so two candidate names describe one potential
        Assert.True(TheFieldAndConnectionFlowsCoincide(), $"gap {FieldVersusConnectionGap():E3}");
        Assert.Equal(4, DistinctFlows());
        Assert.Equal(FlowTable().Single(t => t.Candidate == "field flow").Velocity,
                     FlowTable().Single(t => t.Candidate == "connection flow").Velocity, 12);
    }

    [Fact]
    public void Y_G_057_EveryFlowHasPhaseRankOneAndSelectionPowerOneFiftyThird()
    {
        // a flow is ONE vector field, so its rank cannot exceed 1 - measured rather than asserted
        Assert.All(FlowTable(), t => Assert.Equal(1, t.Rank));
        Assert.All(FlowTable(), t => Assert.Equal(1.0 / 53.0, t.SelectionPower, 12));
        Assert.Equal(1.0 / 53.0, SelectionPower(FlowingCandidates()[0]), 12);
    }

    [Fact]
    public void Y_G_057_TheUnionStillFallsShortOfTheSector()
    {
        Assert.True(UnionConstraintRank() > 0);
        Assert.True(UnionConstraintRank() <= Candidates().Length, $"{UnionConstraintRank()} > 5");
        Assert.True(UnionDeficiency() > 40, $"deficiency {UnionDeficiency()}");
        Assert.Equal(53 - UnionConstraintRank(), UnionDeficiency());
    }

    [Fact]
    public void Y_G_057_TheVerdictIsBoundaryAndTheFirstFlowingProcessIsAPotential()
    {
        Assert.Equal("BOUNDARY", Verdict());
        Assert.Contains("POTENTIAL", TheFirstFlowingProcess());
        Assert.Contains("does not run", TheFirstFlowingProcess());
    }

    [Fact]
    public void Y_G_057_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_057 - Phase Flow Audit: can any existing AT process change the phase coordinates?");

        sb.AppendLine("QUESTION. Can any existing AT PROCESS change the PHASE COORDINATES?");
        sb.AppendLine("GIVEN        G_054 (no AT process runs a phase flow), G_055 (a scalar constrains one phase direction),");
        sb.AppendLine("             G_056 (non-scalar STRUCTURES span the sector - sensitivity, not selection)");
        sb.AppendLine("CANDIDATES   clock flow | acceleration flow | field flow | connection flow | T1/T2 coupling");
        sb.AppendLine("MEASURE      phase velocity | phase rank | selection power");
        sb.AppendLine("GOAL         the first AT process that generates a phase flow");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A PROCESS is driven by a POTENTIAL, and a potential is a scalar, so its action on the state is one");
        sb.AppendLine("     gradient vector: a flow's phase rank is therefore at most 1, which the audit measures per candidate.");
        sb.AppendLine("  2. Phase velocity is the norm of the gradient's phase component - a flow's actual speed in the phase");
        sb.AppendLine("     coordinates - and the fraction is reported beside it so a fast flow in a shallow direction is not");
        sb.AppendLine("     confused with a phase-specific one.");
        sb.AppendLine("  3. Selection power is the phase rank over the 53 phase dimensions: how much of the sector a single");
        sb.AppendLine("     process could constrain.");
        sb.AppendLine("  4. The clock potential's gradient is analytic and the rest are differenced at step 1E-4, whose floor");
        sb.AppendLine("     G_055 established as about 1E-12.");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputFlows());
        PrintHeader(OutputProcess());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
