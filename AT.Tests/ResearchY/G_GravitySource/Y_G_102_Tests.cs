using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_102 - FREEDOM CLOSURE AUDIT.
///
/// G_101 measured the sector's state space: 96 components, 95 moving directions, 1 conserved direction and ONE
/// INDEPENDENT STATE COORDINATE, with every surviving quantity either an invertible reparametrisation of the
/// occupation or a many-to-one functional of it. This suite asks the sharper question that leaves open:
///
///   can the surviving G-sector generate an independent degree of freedom internally, or is the one-degree-of-freedom
///   reduction exact?
///
/// THE INSTRUMENT HAS TWO ARMS, because two different claims are in play. A quantity that IS a function of the state
/// cannot add a direction - its Jacobian against the state has 96 columns, so STACKING it with the state leaves the
/// rank where it was, and that is measured. A quantity that is NOT a function of the state CAN be varied while the
/// state is held fixed, and then the only question that makes it a DYNAMICAL degree of freedom is whether the
/// EVOLUTION reads it: a variation the update does not read is a gauge, a coordinate or a parameter. The suite
/// separates "generated internally" from "supplied to the description", because a supplied structure is not a degree
/// of freedom the sector produces.
///
/// Every count, dimension and rank names its object: the state is the canonical 96-cell ring's occupation (cells = 96,
/// links = LinksOf(96, 1)), the evolution is UpdateAt(cells, links, rho), the rank rule is 1e-8 for the
/// finite-difference Jacobians (floor ~1e-12 at the audit's step of 1e-4, truncation ~1e-8) and 1e-12 for the
/// dynamics, where the uniform mode's symbol is exactly 0.0. The cubic substrate is forbidden and is not named in
/// this file. No new primitive, field, source term, topology axiom, action, Lagrangian or hidden variable is
/// introduced. A negative result is acceptable.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_102_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_102_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string Y(bool v) => v ? "yes" : "no";

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_102_01_TheCandidateTable()
    {
        PrintHeader("G_102 - task 1: every surviving quantity NOT classified by G_101 as a reparametrisation");
        var sb = Start(
            "the candidates are the six functionals G_101 left (those with a kernel), the seven supplied structures the",
            "reduction demoted or the description needs, and the three closure requirements G_093 named;",
            "each carries the rank of its Jacobian against the state and the MAGNITUDE of what a variation of it moves,",
            "so a count is never printed where a magnitude is meant;",
            "the relabelling test uses the audit's SKEWED working state, because the canonical state is",
            "reflection-symmetric (G_099) and reflecting it is the identity - the first draft measured 0 for that reason;",
            "EVERY count is on the 96-cell ring; the cubic substrate is forbidden and is not named in the code.");
        sb.Append(FreedomClosureAudit.OutputCandidates());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = FreedomClosureAudit.Candidates();
        Assert.Equal(16, rows.Length);
        var counts = FreedomClosureAudit.CandidateCounts();
        Assert.Equal(16, counts.Candidates);
        Assert.Equal(6, counts.FunctionsOfTheState);
        Assert.Equal(7, counts.Supplied);
        Assert.Equal(3, counts.Absent);
        Assert.Equal(1, counts.StructuresTheEvolutionReads);

        // the three roles of "how the evolution sees it" are distinguished, not bundled
        Assert.Equal(1, rows.Count(r => r.Role == EvolutionRole.Argument));
        Assert.Equal(1, rows.Count(r => r.Role == EvolutionRole.Coefficient));
        Assert.Equal(1, rows.Count(r => r.Role == EvolutionRole.Index));
        Assert.Equal(EvolutionRole.Argument, rows.Single(r => r.Candidate.Contains("link set")).Role);
        Assert.Equal(EvolutionRole.Coefficient, rows.Single(r => r.Candidate.Contains("step")).Role);
        Assert.Equal(EvolutionRole.Index, rows.Single(r => r.Candidate.Contains("tick")).Role);

        // every functional loses at least one direction, measured
        Assert.All(rows.Where(r => r.IsAFunctionOfTheState), r => Assert.True(r.KernelAgainstTheState >= 1));
        // the scales: the orientation field loses exactly 1, the total count 95, the readouts 92
        Assert.Equal(1, rows.Single(r => r.Candidate.Contains("orientation field")).KernelAgainstTheState);
        Assert.Equal(95, rows.Single(r => r.Candidate.Contains("total count")).KernelAgainstTheState);
        Assert.Equal(92, rows.Single(r => r.Candidate.Contains("readouts")).KernelAgainstTheState);

        // the gauges move nothing and the coordinates/parameters move readouts but never the update
        Assert.Equal(0.0, rows.Single(r => r.Candidate.Contains("circulation")).ReadoutEffect, 12);
        Assert.Equal(0.0, rows.Single(r => r.Candidate.Contains("component count")).ReadoutEffect, 12);
        Assert.True(rows.Single(r => r.Candidate.Contains("labelling")).ReadoutEffect > 0.1);
        Assert.True(rows.Single(r => r.Candidate.Contains("coefficient")).ReadoutEffect > 1e-2);
        Assert.All(rows.Where(r => !r.Candidate.Contains("link set") && !r.Candidate.Contains("step")
            && !r.Candidate.Contains("tick")), r => Assert.Equal(0.0, r.UpdateEffect, 12));
    }

    [Fact]
    public void Y_G_102_02_TheRankContributionTable()
    {
        PrintHeader("G_102 - task 2: each candidate's rank contribution, measured by STACKING");
        var sb = Start(
            "the measurement is the rank of the candidate's rows STACKED with the state's own identity, against the",
            "state's rank alone — a quantity that is a function of the state contributes columns it does not own;",
            "a SUPPLIED structure has no Jacobian against the state, so what is stacked is its measured EFFECT;",
            "an effect is a VECTOR IN the state space, so stacking it cannot raise the space's rank either — it adds a",
            "PARAMETER dimension to the description, which is a different count and is reported separately;",
            "the increase is reported rather than the rank, so that a zero is visible as a zero.");
        sb.Append(FreedomClosureAudit.OutputRankContributions());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = FreedomClosureAudit.RankContributions();
        Assert.Equal(13, rows.Length);
        Assert.All(rows, r => Assert.Equal(96, r.RankStacked));
        Assert.All(rows, r => Assert.Equal(0, r.RankIncrease));
        Assert.All(rows, r => Assert.False(r.ContributesABeyondTheStateDirection));

        // the state's own rank is the maximum any stacking can reach
        var maximum = FreedomClosureAudit.MaximumRank();
        Assert.Equal(96, maximum.MaxRankDerived);
        Assert.Equal(0, maximum.IncreaseBeyondTheState);
        Assert.Equal(5, maximum.SuppliedInputs);

        // and the stack really contains every derived group, so the zero is not vacuous
        Assert.Equal(6, rows.Count(r => r.Origin == CandidateOrigin.DerivedFromTheState));
        Assert.Equal(7, rows.Count(r => r.Origin == CandidateOrigin.SuppliedToTheDescription));
    }

    [Fact]
    public void Y_G_102_03_TheMaximumStateSpaceRank()
    {
        PrintHeader("G_102 - task 3: the maximum state-space rank obtainable from all surviving structures together");
        var sb = Start(
            "stacking EVERY surviving quantity's Jacobian with the state's own gives the maximum rank the derived",
            "sector can span — and the state's own rank is the ceiling, because every derived quantity's columns are",
            "functions of its columns;",
            "the rank helper zero-pads short rows, because stacking quantities with different component counts is",
            "exactly how this audit measures a contribution (the first draft's helper assumed a rectangle and threw);",
            "the description's rank is reported BESIDE the derived rank, with the supplied inputs counted explicitly;",
            "the evolution's own rank is reported separately because it is where a supplied structure enters.");
        sb.Append(FreedomClosureAudit.OutputRankContributions().Split("4. THE THREE GROUPS")[1]);
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var maximum = FreedomClosureAudit.MaximumRank();
        Assert.Equal(96, maximum.MaxRankDerived);
        Assert.Equal(0, maximum.IncreaseBeyondTheState);
        Assert.Equal(95, FreedomClosureAudit.EvolutionRank());

        // the evolution's rank is the state's rank, measured through the same spectrum G_101 used
        var rankAtTheRule = StateDegreesOfFreedomAudit.StateSpaceRank()
            .Single(r => Math.Abs(r.Tolerance - StateDegreesOfFreedomAudit.DynamicsRule) < 1e-18);
        Assert.Equal(95, rankAtTheRule.Rank);
        Assert.Equal(1, rankAtTheRule.KernelDimension);
    }

    [Fact]
    public void Y_G_102_04_TheThreeGroupsContributeNothing()
    {
        PrintHeader("G_102 - tasks 4, 5 and 6: the orientation, observable and evolution groups, each measured");
        var sb = Start(
            "the task names three groups and each gets its own instrument: the orientation quantities' rank",
            "contribution, the observables' and the evolution's;",
            "the evolution group is special because it is the ONLY place a supplied structure can enter, so its own",
            "rank is reported as the state's rank rather than as an increase;",
            "a group's numeric column is its maximum rank increase, and the evolution's rank appears in its READING",
            "rather than in that column, so a rank is never printed where an increase is meant.");
        foreach (var g in FreedomClosureAudit.GroupContributions())
            sb.AppendLine($"   {g.Group}: {g.Members} member(s), max rank increase {g.MaxRankIncrease} — {g.Reading}");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var groups = FreedomClosureAudit.GroupContributions();
        Assert.Equal(6, groups.Length);
        Assert.All(groups, g => Assert.Equal(0, g.MaxRankIncrease));
        Assert.All(groups, g => Assert.Equal(0.0, g.MaxIndependentEffect, 12));

        // the three groups the task names are present and each is zero
        var orientation = groups.Single(g => g.Group.Contains("task 4"));
        var observable = groups.Single(g => g.Group.Contains("task 5"));
        var evolution = groups.Single(g => g.Group.Contains("task 6"));
        Assert.Equal(2, orientation.Members);
        Assert.Equal(1, observable.Members);
        Assert.Equal(1, evolution.Members);
        Assert.Contains("95", evolution.Reading);
    }

    [Fact]
    public void Y_G_102_05_TheReconstructionResidualTable()
    {
        PrintHeader("G_102 - task 7: every surviving quantity reconstructed from the state, with the obstruction measured");
        var sb = Start(
            "for a derived quantity the forward direction is an identity, so the informative direction is the INVERSE and",
            "the audit rebuilds the state in closed form where an inverse exists;",
            "where it does not, the OBSTRUCTION is measured and its KIND is named: a scale, a kernel dimension, a",
            "movement, or an absence — and no count is averaged with a magnitude;",
            "the relabelling and the free room are tested on the skewed working state, and the link set's obstruction is",
            "the update's own residual under a cut;",
            "the closure requirements are reported as ABSENT rather than as reconstructed.");
        sb.Append(FreedomClosureAudit.OutputReconstruction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = FreedomClosureAudit.Reconstruction();
        Assert.Equal(11, rows.Length);
        var counts = FreedomClosureAudit.ReconstructionCounts();
        Assert.Equal(11, counts.Quantities);
        Assert.Equal(4, counts.Closing);
        Assert.Equal(1, counts.ObstructedByAScale);
        Assert.Equal(1, counts.ObstructedByAKernel);
        Assert.Equal(4, counts.ObstructedByAMovement);
        Assert.Equal(1, counts.Absent);
        Assert.True(counts.WorstClosingResidual < 1e-12,
            $"the closed-form inverses must close at the rule, measured {N(counts.WorstClosingResidual)}");
        Assert.True(counts.MaxMovementObstruction > 1.0);

        // the four closed-form inverses are the four reparametrisations G_101 identified
        var closing = rows.Where(r => r.Closes).Select(r => r.Quantity).ToArray();
        Assert.Contains("the potential map A", closing);
        Assert.Contains("the rate kernel K", closing);
        Assert.Contains("g00", closing);
        Assert.Contains("the clock rate", closing);

        // and the scale obstruction is the orientation field's own one-dimensional kernel
        var field = rows.Single(r => r.Quantity.Contains("orientation field"));
        Assert.Equal(ObstructionKind.Scale, field.Kind);
        Assert.True(field.Obstruction < 1e-15);
    }

    [Fact]
    public void Y_G_102_06_NothingSuppliedIsGenerated()
    {
        PrintHeader("G_102 - the supplied inputs, and why a supplied structure is not a generated degree of freedom");
        var sb = Start(
            "the audit separates 'generated internally' from 'supplied to the description', because only the first is a",
            "degree of freedom the sector produces;",
            "the test is the evolution's own argument list: the update reads (cells, links, rho), so the link set is the",
            "only non-state structure it takes as an argument;",
            "the step is the update's coefficient and the tick its index — neither is a structure the law reads;",
            "the gauges and the coordinates move no update at all, measured;",
            "the five non-coordinate inputs are reported so that the verdict cannot be read as completeness.");
        foreach (var i in StateDegreesOfFreedomAudit.IndependentInputs())
            sb.AppendLine($"   {i.Input}: dimensions {i.Dimensions}, a state coordinate: {Y(i.IsAStateCoordinate)}");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the evolution's argument list is a fact of the sector's own operator
        var probe = FreedomClosureAudit.Update(
            Enumerable.Range(0, 96).Select(i => 1.0 + 0.1 * Math.Cos(2 * Math.PI * i / 96)).ToArray());
        Assert.Equal(96, probe.Length);

        // the only argument is the link set: a cut moves the update, and every other candidate does not
        var rows = FreedomClosureAudit.Candidates();
        var moving = rows.Where(r => r.UpdateEffect > 1e-12).ToArray();
        Assert.Equal(3, moving.Length);
        Assert.All(moving, r => Assert.True(r.Role != EvolutionRole.NotRead));
        Assert.Equal(1, moving.Count(r => r.Role == EvolutionRole.Argument));

        // and the supplied inputs are five beyond the state coordinate
        Assert.Equal(5, StateDegreesOfFreedomAudit.IndependentInputs().Count(i => !i.IsAStateCoordinate));
    }

    [Fact]
    public void Y_G_102_07_TheGaugesAndCoordinatesMoveNothing()
    {
        PrintHeader("G_102 - the gauge and coordinate candidates, each measured against the update");
        var sb = Start(
            "a candidate that can be varied while the state is fixed, and which the update does not read, is a gauge or",
            "a coordinate — and neither is a dynamical direction;",
            "the circulation is G_083's freedom (the same divergence, a different flux), the component count is G_082's",
            "gauge-like datum, the labelling is the coordinate system and the free room is G_075's map coefficient;",
            "each is measured twice: what it moves in the READOUTS and what it moves in the UPDATE;",
            "the verdict needs the second number, and it is zero for all of them.");
        var rows = FreedomClosureAudit.Candidates();
        sb.AppendLine("   candidate                                    readout effect   update effect   role");
        foreach (var name in new[] { "circulation", "component count", "labelling", "coefficient (the free room)" })
        {
            var r = rows.Single(x => x.Candidate.Contains(name));
            sb.AppendLine($"   {name,-42}  {N(r.ReadoutEffect),15}  {N(r.UpdateEffect),13}  {r.Role}");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        foreach (var name in new[] { "circulation", "component count", "labelling", "coefficient (the free room)" })
        {
            var r = rows.Single(x => x.Candidate.Contains(name));
            Assert.Equal(0.0, r.UpdateEffect, 12);
            Assert.Equal(EvolutionRole.NotRead, r.Role);
        }

        // and two of the four move readouts, so the zero update effect is a measurement and not a vacuous one
        Assert.True(rows.Single(r => r.Candidate.Contains("labelling")).ReadoutEffect > 0.1);
        Assert.True(rows.Single(r => r.Candidate.Contains("coefficient")).ReadoutEffect > 1e-2);
    }

    [Fact]
    public void Y_G_102_08_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_102 - classification and criteria");
        var sb = Start(
            "the classification is CLOSED or OPEN, decided by the rank increase rather than by a judgement;",
            "the success criterion asks for a positive rank beyond the state and the failure criterion for the exactness",
            "of the reduction, and both are reported with their counts;",
            "two further criteria are stated so that the verdict cannot be read as completeness: the supplied inputs and",
            "the absent closures;",
            "no literal reaches a verdict: every outcome is a comparison of measured quantities.");
        sb.Append(FreedomClosureAudit.OutputClassification());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var criteria = FreedomClosureAudit.Criteria();
        Assert.Equal(4, criteria.Length);
        Assert.Equal("NOT MET", criteria[0].Outcome);
        Assert.Equal("MET", criteria[1].Outcome);
        Assert.Equal("CONFIRMED", criteria[2].Outcome);
        Assert.Equal("CONFIRMED", criteria[3].Outcome);
    }

    [Fact]
    public void Y_G_102_09_TheVerdictAndTheCrossChecks()
    {
        PrintHeader("G_102 - the verdict, cross-checked against the audits it continues");
        var sb = Start(
            "the verdict is COMPUTED with live branches, so no clause can be a literal (G_027);",
            "the cross-checks are the audits this one continues, and each is asserted at ITS OWN tolerance and in ITS",
            "own regime - a cited number is a claim until it is recomputed;",
            "the spectral substrate is the canonical 96-cell ring and the cubic substrate is forbidden here;",
            "the equality rules are 1e-12 on computed values, exact equality on identities, and a stated rule per rank.");
        sb.AppendLine("VERDICT");
        sb.AppendLine("  " + FreedomClosureAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine("WHAT THIS ESTABLISHES AND WHAT IT DOES NOT");
        sb.AppendLine(FreedomClosureAudit.WhereItStands());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = FreedomClosureAudit.Verdict();
        Assert.Contains("CLOSED", verdict);
        Assert.Contains("FINAL VERDICT", verdict);
        Assert.Contains("rank 1", verdict);

        // cross-check 1 (G_101): one independent state coordinate, 95 moving directions and 1 conserved
        var counts = StateDegreesOfFreedomAudit.Counts();
        Assert.Equal(1, counts.Single(c => c.Count.Contains("STATE COORDINATES")).Value);
        Assert.Equal(95, counts.Single(c => c.Count.Contains("MOVING")).Value);
        Assert.Equal(0, counts.Single(c => c.Count.Contains("BEYOND")).Value);

        // cross-check 2 (G_100): the orientation field has ZERO independent dimensions, reaching here as a one-dimensional kernel
        Assert.Equal(0, OrientationCouplingAudit.InventoryCounts().Independent);
        Assert.Equal(1, OrientationCouplingAudit.PartitionCounts().Dimension);
        Assert.Equal(1, FreedomClosureAudit.Candidates()
            .Single(r => r.Candidate.Contains("orientation field")).KernelAgainstTheState);

        // cross-check 3 (G_093): exactly three closure requirements are absent, and they are the ones G_093 named
        var absent = FreedomClosureAudit.Candidates()
            .Where(r => r.Origin == CandidateOrigin.AbsentFromTheSector).Select(r => r.Candidate).ToArray();
        Assert.Equal(3, absent.Length);
        Assert.Contains(absent, a => a.Contains("memory channel"));
        Assert.Contains(absent, a => a.Contains("vector sector"));
        Assert.Contains(absent, a => a.Contains("unified action"));

        // cross-check 4 (G_080): the link set is an input, and it is the only structure the evolution takes as one
        Assert.Equal(1, FreedomClosureAudit.Candidates().Count(r => r.IsAnArgumentOfTheUpdate));
        Assert.Equal(0.0, FreedomClosureAudit.MaximumRank().IncreaseBeyondTheState);
    }
}
