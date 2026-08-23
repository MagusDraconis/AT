using System.Globalization;
using System.Text;
using TQM.Core.ResearchXH;
using TQM.Tests.Shared;
using Xunit.Abstractions;

namespace TQM.Tests.ResearchXH;

/// <summary>
/// TQM-QG Phase 268 — Count Conservation Origin. Why is the actualization count conserved? D96 only,
/// no observables.
/// </summary>
public class TQMQG_Phase268_CountConservationOriginTests : ResearchTestBase
{
    public TQMQG_Phase268_CountConservationOriginTests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void TQMQG2680_ActualizationAndIndividuation()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2680: actualization and individuation — the primitive is a countable unit");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - a Q-event is a REAL-UNDERIVED primitive (a network transition, not emergent);");
        sb.AppendLine("  - ρ counts individual Q-events — individuation makes the primitive countable.");
        sb.AppendLine();

        sb.AppendLine($"Q-event is a real-underived primitive: {CountConservationOrigin.QEventIsPrimitive()}");
        sb.AppendLine($"Q-event is a network transition (tick): {CountConservationOrigin.QEventIsTransition()}");
        sb.AppendLine($"actualization content required (bare point insufficient): {CountConservationOrigin.ActualizationContentRequired()}");
        sb.AppendLine($"ρ counts individual events: {CountConservationOrigin.RhoCountsIndividualEvents()}");
        sb.AppendLine($"actualization is a discrete projection: {CountConservationOrigin.ActualizationIsDiscrete()}");

        Output.WriteLine(sb.ToString());

        Assert.True(CountConservationOrigin.QEventIsPrimitive(), "the Q-event is a real-underived primitive");
        Assert.True(CountConservationOrigin.RhoCountsIndividualEvents(), "ρ counts individual Q-events");
        Assert.True(CountConservationOrigin.ActualizationContentRequired());
    }

    [Fact]
    public void TQMQG2681_NetworkClosure()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2681: network closure — the attractor fixes the event count");

        sb.AppendLine("HYPOTHESES:");
        sb.AppendLine("  - the actualization dynamics converges to a fixed topology;");
        sb.AppendLine("  - the N=96 attractor fixes the link count → the network's event count is fixed.");
        sb.AppendLine();

        sb.AppendLine($"Born rule exact (ρ = normalized share): {CountConservationOrigin.BornRuleExact()}");
        sb.AppendLine($"branching conserves total population: {CountConservationOrigin.BranchingConservesPopulation()}");
        sb.AppendLine($"topology converged (0% residual link growth): {CountConservationOrigin.TopologyConverged()}");
        sb.AppendLine($"link count fixed (trace = 2·edges = 1152): {CountConservationOrigin.LinkCountFixed()}");
        sb.AppendLine($"constant vector in ker L (total-mass): {CountConservationOrigin.ConstantVectorInKernel()}");

        Output.WriteLine(sb.ToString());

        Assert.True(CountConservationOrigin.BornRuleExact());
        Assert.True(CountConservationOrigin.BranchingConservesPopulation());
        Assert.True(CountConservationOrigin.TopologyConverged());
        Assert.True(CountConservationOrigin.LinkCountFixed());
    }

    [Fact]
    public void TQMQG2682_Classification()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("TQMQG2682: the origin determination — why count is conserved");

        sb.AppendLine("ASSUMPTIONS (structure only — no observables, no formulas):");
        sb.AppendLine("  - COUNT FUNDAMENTAL (score ≤ 2), COUNT DERIVED (3-4),");
        sb.AppendLine("    UNIVERSAL SELF-CONSISTENCY (5-6);");
        sb.AppendLine("  - the terminal question of the QG260-268 reduction chain.");
        sb.AppendLine();

        sb.AppendLine($"SUMMARY: {CountConservationOrigin.Summary()}");
        sb.AppendLine($"Origin score: {CountConservationOrigin.OriginScore()}/6");
        sb.AppendLine($"Self-consistency (a Q-event IS a unit): {CountConservationOrigin.SelfConsistency()}");
        sb.AppendLine($"CLASSIFICATION = {CountConservationOrigin.Classify()}");
        sb.AppendLine();
        sb.AppendLine("INTERPRETATION:");
        sb.AppendLine("  - ACTUALIZATION: the Q-event is a real-underived primitive whose minimal content");
        sb.AppendLine("    is a network transition (a tick) — the primitive IS an actualization.");
        sb.AppendLine("  - INDIVIDUATION: each event is an individual, indivisible unit; ρ counts them.");
        sb.AppendLine("  - NETWORK CLOSURE: the actualization dynamics converges to the fixed N=96");
        sb.AppendLine("    attractor (0% residual link growth) — the event count is fixed by closure.");
        sb.AppendLine("  - SELF-CONSISTENCY: a Q-event IS a unit. 'Conservation of the count' is the");
        sb.AppendLine("    DEFINITIONAL IDENTITY of the primitive — a unit is self-identical, so the");
        sb.AppendLine("    number of units cannot change without the primitive ceasing to be a unit.");
        sb.AppendLine("  - The count is conserved because the primitive IS a countable unit. This is not");
        sb.AppendLine("    an unexplained axiom and not a dynamical law — it is what it means for the");
        sb.AppendLine("    primitive to be a unit (self-consistency). Every deeper conservation law");
        sb.AppendLine("    (norm, trace, unitarity, Bianchi, Noether, QG267) is a projection of this.");

        Output.WriteLine(sb.ToString());

        Assert.Equal("UNIVERSAL SELF-CONSISTENCY", CountConservationOrigin.Classify());
        Assert.True(CountConservationOrigin.OriginScore() >= 5);
        Assert.True(CountConservationOrigin.SelfConsistency());
        Assert.Contains("UNIVERSAL SELF-CONSISTENCY", CountConservationOrigin.Summary());
    }
}
