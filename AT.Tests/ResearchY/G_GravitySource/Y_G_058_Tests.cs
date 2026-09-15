using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.PhaseDynamicsClosureAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_058 - Phase Dynamics Closure Audit (group G - Gravity Source).
///
/// QUESTION. Can any AT update rule generate a non-trivial phase evolution? Tests: single scalar flow, multiple coupled
/// scalar flows, vector-valued flow, connection-driven flow, T1/T2-coupled flow. Measure the phase rank of the evolution
/// operator. Critical: can any existing AT process reach rank 53?
///
/// ANSWER: **BOUNDARY - and the closure's first result is that the requested measure CANNOT DISCRIMINATE. THREE ranks
/// are measured: the linearisation and operator ranks are 53 for EVERY rule, the do-nothing identity included, while the
/// PUSH rank is 1 for any rule that moves and 0 for the process AT actually runs. The question's premise - a hierarchy
/// with vector generators at 53 and scalar rules at 1 - is REFUTED by measurement.**
/// </summary>
public class Y_G_058_Tests : ResearchTestBase
{
    public Y_G_058_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_058_TheRequestedCriterionIsVacuous()
    {
        var table = RankTable();
        Assert.Equal(6, table.Length);

        // the linearisation and the operator are full rank for EVERY rule, the do-nothing control included
        Assert.True(EveryGeneratorLinearisationIsFull());
        Assert.True(EveryOperatorRankIsFull());
        Assert.All(table, t => Assert.Equal(PhaseDimension(), t.OperatorRank));
        Assert.True(TheRequestedCriterionIsVacuous());
        Assert.Contains(RankTable(), t => t.Rule.StartsWith("identity") && t.OperatorRank == PhaseDimension());

        // the warning is stated in the report itself, before any rank from that column is quoted
        Assert.Contains("EVERY rule tested", TheMethodologicalFinding());
        Assert.Contains("PUSH rank", TheMethodologicalFinding());
    }

    [Fact]
    public void Y_G_058_ThePushRankSeparatesRulesAndCouplingScalarsDoesNotHelp()
    {
        // every moving rule pushes exactly one direction; the identity pushes none
        Assert.Equal(1, MaxPushRank());
        Assert.All(RankTable().Where(t => !t.Rule.StartsWith("identity")),
            t => Assert.Equal(1, t.PushRank));
        Assert.Equal(0, RankTable().Single(t => t.Rule.StartsWith("identity")).PushRank);

        // a sum of gradients is still one vector: coupling several scalar flows does not raise the push rank
        Assert.True(CouplingScalarsDoesNotHelp());
        Assert.Equal(5, RulesThatPush().Length);
        Assert.Contains("identity update (CONTROL)", RulesThatDoNotPush());
    }

    [Fact]
    public void Y_G_058_NoRankMeasureSeparatesVectorValuedFromScalarRules()
    {
        // THE WITHDRAWN EXPECTATION: measured, no rank measure separates a vector-valued generator from a scalar one
        Assert.True(VectorValuedAndScalarRulesAreRankIndistinguishable());
        var scalars = RankTable().Where(t => t.Rule.Contains("scalar")).ToArray();
        var vectors = RankTable().Where(t => t.Rule.Contains("vector")).ToArray();
        Assert.Equal(PhaseDimension(), scalars[0].LinearisationRank);
        Assert.Equal(scalars[0].LinearisationRank, vectors[0].LinearisationRank);
        Assert.Equal(scalars[0].PushRank, vectors[0].PushRank);
    }

    [Fact]
    public void Y_G_058_NoAtProcessPushesThePhase()
    {
        Assert.True(TheActualizationHasNoSpatialGenerator());
        Assert.Equal(0, ActualizationPushRank());
        Assert.True(NoAtProcessPushesThePhase());
        Assert.NotEqual(PhaseDimension(), ActualizationPushRank());
    }

    [Fact]
    public void Y_G_058_TheVerdictIsBoundaryAndTheCriticalAnswerHasTwoHalves()
    {
        Assert.Equal("BOUNDARY", Verdict());
        Assert.Contains($"PUSH RANK {ActualizationPushRank()}", TheCriticalAnswer());
        Assert.Contains("no spatial generator", TheCriticalAnswer());
    }

    [Fact]
    public void Y_G_058_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_058 - Phase Dynamics Closure Audit: can an AT update rule generate a phase evolution?");

        sb.AppendLine("QUESTION. Can any AT UPDATE RULE generate a non-trivial phase evolution?");
        sb.AppendLine("GIVEN        G_052 (the interface identity), G_054 (freely assigned), G_056 (structures span),");
        sb.AppendLine("             G_057 (five potentials at rank 1, the running process phase-static)");
        sb.AppendLine("TESTS        single scalar flow | multiple coupled scalar flows | vector-valued flow |");
        sb.AppendLine("             connection-driven flow | T1/T2-coupled flow");
        sb.AppendLine("CRITICAL     can any existing AT process reach rank 53?");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. TWO different ranks are measured and kept apart. The GENERATOR rank counts the phase directions");
        sb.AppendLine("     the rule pushes; the OPERATOR rank counts how phase perturbations propagate under the update.");
        sb.AppendLine("  2. The identity update is carried as an explicit CONTROL, because a criterion that the do-nothing");
        sb.AppendLine("     rule satisfies cannot be evidence of a phase process.");
        sb.AppendLine("  3. A generator is a vector field; a scalar-driven rule's generator is one gradient, so its rank");
        sb.AppendLine("     cannot exceed one however many scalars are summed.");
        sb.AppendLine("  4. THREE RANKS are measured, because the question asks for one: the PUSH rank (the state's");
        sb.AppendLine("     instantaneous phase displacement), the LINEARISATION rank (the generator's Jacobian on the phase");
        sb.AppendLine("     sector) and the OPERATOR rank (how phase perturbations propagate).");
        sb.AppendLine("  5. Deterministic throughout.");
        sb.AppendLine();

        PrintHeader(OutputRanks());
        PrintHeader(OutputPush());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
