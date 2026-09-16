using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.FlowSelectionAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_067 - Flow Selection Audit (group G - Gravity Source).
///
/// QUESTION. Can any existing AT quantity select between the dissipative and the unitary flows? Candidates: the clock
/// law, the acceleration law, the field law, occupancy conservation, the phase sector, the free room. Measure whether any
/// quantity changes under admissible flow replacement. Goal: find the first principle that selects a physical flow.
///
/// ANSWER: **REFUTED - no existing AT quantity selects. The flows are distinguishable by almost every quantity the
/// theory names, and none of those quantities is required by anything AT states; the one pattern that WOULD select
/// (norm conservation) appears only in the audit's own control, and the entropy's monotonicity is a principle AT does
/// not state.**
/// </summary>
public class Y_G_067_Tests : ResearchTestBase
{
    public Y_G_067_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_067_TheQuestionsCandidatesDoNotSelect()
    {
        var table = Table();
        Assert.Equal(8, table.Length);
        Assert.Equal(6, QuestionCandidates().Length);
        Assert.Equal(2, ControlQuantities().Length);

        // two of the six are invariant under both flows, four change under both - and NONE selects
        Assert.Equal(new[] { "Q4", "Q6" }, InvariantUnderBoth());
        Assert.Equal(new[] { "Q1", "Q2", "Q3", "Q5", "Q8" }, ChangingUnderBoth());
        Assert.Empty(Selectors());
        Assert.True(NoCandidateSelects());

        // the invariance of the total is exact, not approximate
        var total = table.Single(t => t.Id == "Q4");
        Assert.Equal(96.0, total.Start, 9);
        Assert.Equal(total.Start, total.Dissipative, 9);
        Assert.Equal(total.Start, total.Unitary, 9);
    }

    [Fact]
    public void Y_G_067_TheFreeRoomCannotSelectAnything()
    {
        Assert.True(TheFreeRoomIsStateIndependent());

        double freeRoom = Candidates().Single(c => c.Id == "Q6").Value(Canonical());
        Assert.Equal(51, freeRoom, 9);
        // it is a property of the SPECTRUM, so no state and no flow can move it
        Assert.Equal(96 - 45, freeRoom, 9);
        Assert.Equal(freeRoom, Candidates().Single(c => c.Id == "Q6").Value(PhaseBearing()), 9);
        Assert.Equal(freeRoom, Candidates().Single(c => c.Id == "Q6").Value(
            PhaseEvolutionAudit.Orbit(Unitary, PhaseBearing(), Eps, Steps)), 9);
    }

    [Fact]
    public void Y_G_067_TheLawsSelectNeitherFlow()
    {
        var under = LawUnderEachFlow();
        Assert.Equal(2, under.Length);
        Assert.All(under, t => Assert.InRange(t.WorstLawResidual, 0.0, 1e-8));
        Assert.All(under, t => Assert.InRange(t.TotalResidual, 0.0, 1e-8));

        Assert.True(EveryLawHoldsUnderBothFlows());
        Assert.True(TheLawsSelectNeitherFlow());
        Assert.True(NoAtLawRequiresMonotonicity());
    }

    [Fact]
    public void Y_G_067_BothFlowsCreatePhaseContentAndDifferInTransience()
    {
        // THE CORRECTED EXPECTATION: both flows create phase content from the phase-free state
        var fate = PhaseFate(new[] { 0, 1000, Steps });
        Assert.InRange(fate[0].PhaseFreeStartDissipative, 0.0, 1e-12);
        Assert.True(fate[1].PhaseFreeStartDissipative > 0.1);
        Assert.True(fate[1].PhaseFreeStartUnitary > 0.1);
        Assert.True(TheUnitaryFlowCreatesPhaseContent());

        // and they differ in TRANSIENCE: the dissipative content peaks and decays, the unitary content persists
        var trajectory = DissipativePhaseTrajectory(new[] { 0, 4000, 20000, 50000 });
        Assert.True(trajectory[1].PhaseNorm > trajectory[2].PhaseNorm);
        Assert.True(trajectory[2].PhaseNorm > trajectory[3].PhaseNorm);
        Assert.True(TheDifferenceFlowGeneratesPhaseContentTransiently());
        Assert.True(TheAttractorIsStillTheUniformState());

        // the unitary flow holds its phase content at three quarters of the state's norm
        Assert.True(PhaseFate(new[] { Steps }).Single().PhaseFreeStartUnitary > 0.7);
    }

    [Fact]
    public void Y_G_067_TheSelectorPatternAppearsOnlyInTheControl()
    {
        // the deviation norm is fixed by the unitary flow and decays under the dissipative one
        Assert.Equal(new[] { "Q7" }, ControlQuantitiesWithTheSelectorPattern());
        var deviation = Table().Single(t => t.Id == "Q7");
        Assert.InRange(Math.Abs(deviation.Unitary - deviation.Start) / deviation.Start, 0.0, 1e-6);
        Assert.InRange(deviation.Dissipative / deviation.Start, 0.0, 0.5);
        Assert.StartsWith("SELECTS", deviation.Class);

        // and it is NOT one of the question's candidates, so it does not answer the question
        Assert.DoesNotContain("Q7", QuestionCandidates());
        Assert.Empty(Selectors());
    }

    [Fact]
    public void Y_G_067_TheUnmadeSelectionIsAMonotonicityPrinciple()
    {
        // the dissipative flow is entropy-monotone and the unitary one is not - so an arrow of time WOULD select
        Assert.True(TheDissipativeFlowIsEntropyMonotone());
        Assert.True(TheUnitaryFlowIsNotEntropyMonotone());
        var fate = EntropyFate(new[] { 0, Steps });
        Assert.True(fate[1].EntropyDissipative > fate[0].EntropyDissipative);
        Assert.InRange(Math.Abs(fate[1].EntropyUnitary - fate[0].EntropyUnitary), 0.0, 1e-4);

        // and AT states no such principle, which is why the selection is available but unmade
        Assert.Equal("REFUTED", Verdict());
        Assert.Contains("no existing AT quantity selects", TheAnswer());
    }

    [Fact]
    public void Y_G_067_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_067 - Flow Selection Audit: can any AT quantity select a flow?");

        sb.AppendLine("QUESTION. Can any EXISTING AT quantity select between the DISSIPATIVE and the UNITARY flows?");
        sb.AppendLine("GIVEN        G_060 (the multipliers and ranks), G_065 (no law requires phase-freeness), G_066 (four");
        sb.AppendLine("             of five forms are admissible and they split two-and-two)");
        sb.AppendLine("CANDIDATES   clock law | acceleration law | field law | occupancy conservation | phase sector | free room");
        sb.AppendLine("MEASURE      whether any quantity changes under admissible flow replacement");
        sb.AppendLine("GOAL         the first principle that selects a physical flow");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The question's six candidates are kept apart from the two controls the audit adds, and the verdict is");
        sb.AppendLine("     ruled on the QUESTION\'S list: a control that shows the pattern does not answer the question.");
        sb.AppendLine("  2. A quantity can only select in one of three ways: invariant under both (it cannot select), changing");
        sb.AppendLine("     under both (it distinguishes them but states no preference), or invariant under exactly one - which is");
        sb.AppendLine("     the only pattern a requirement could turn into a selection.");
        sb.AppendLine("  3. The requirement test is measured on the EVOLVED states rather than cited: a law that holds under both");
        sb.AppendLine("     flows cannot prefer either, whatever its wording.");
        sb.AppendLine("  4. Deterministic throughout; eps = 1E-3.");
        sb.AppendLine();

        PrintHeader(OutputCandidates());
        PrintHeader(OutputRequirement());
        PrintHeader(OutputDisagreement());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
