using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_093 - DYNAMIC CORE RECOVERY AUDIT.
///
/// G_084-G_091 reduced the observable sector to rho -> K(rho) and G_092 found no hidden actuator. This suite takes up
/// the concern G_092 did NOT test: an observable minimization may have removed structure the OBSERVABLES never needed
/// - structure required for the evolution, for control, or for first-principles COMPLETION. The three are kept apart,
/// and the minimal core is NOT assumed complete: an absent structure is reported as absent rather than as inert.
///
/// Two theories are built and compared on four axes, then every structure is removed one at a time and the four
/// losses are measured. The report follows the eight tasks. The substrate is the canonical 96-cell ring (the cubic
/// substrate only where the vector sector is named, by E_004's own measurement), the equality rule is 1e-12, and no
/// spectral quantity is quoted.
/// </summary>
public sealed class Y_G_093_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_093_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_093_01_TheTwoTheoriesAreBuiltAndCompared()
    {
        PrintHeader("G_093 - tasks 1 and 2: theory A, theory B, and the four axes");
        var sb = Start(
            "A is the observable-minimal core and B the full pre-reduction theory, both built from the chain's own",
            "factory methods so that neither is a paraphrase;",
            "the four axes are observables, evolution, controllability and closure;",
            "each axis is decided by a MEASURED difference, not by a reading of the descriptions;",
            "closure is measured as the count of structures the theory must be GIVEN (this audit's new axis).");
        sb.Append(DynamicCoreRecoveryAudit.OutputTheories());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var theories = DynamicCoreRecoveryAudit.Theories();
        Assert.Equal(2, theories.Length);
        var a = theories[0];
        var b = theories[1];
        Assert.StartsWith("A, the observable-minimal core", a.Theory);
        Assert.StartsWith("B, the full pre-reduction theory", b.Theory);
        // the observables: A reproduces every readout of B
        Assert.Equal(b.ReadoutsProduced, a.ReadoutsProduced);
        Assert.Equal(0, a.ReadoutsLost);
        Assert.All(MinimalGravityCoreAudit.Reproduction(), r => Assert.Equal(0.0, r.Difference, 12));
        // the evolution: identical, measured as an exact zero rather than argued
        Assert.Equal(0.0, a.EvolutionResidual, 12);
        Assert.Equal(0.0, DynamicCoreRecoveryAudit.EvolutionResidual(
            MinimalGravityCoreAudit.Minimal(), MinimalGravityCoreAudit.Full()), 12);
        // control and closure: the same on both
        Assert.Equal(b.ReachableDirections, a.ReachableDirections);
        Assert.Equal(b.ClosureInputs, a.ClosureInputs);
        // and NO axis differs - which is the audit's answer to the question's own concern
        Assert.All(DynamicCoreRecoveryAudit.Comparison(), c => Assert.False(c.TheyDiffer));

        sb.AppendLine($"  A produces {a.ReadoutsProduced} of {a.ReadoutsProduced + a.ReadoutsLost} readouts, moves the evolution by");
        sb.AppendLine($"  exactly {N(a.EvolutionResidual)}, reaches {a.ReachableDirections} operations and is given {a.ClosureInputs} inputs -");
        sb.AppendLine("  identical to B on every axis. No measured axis distinguishes the two theories.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_02_TheMatrixHasFourMeasuredColumns()
    {
        PrintHeader("G_093 - tasks 3 and 4: the four necessities, per structure, one at a time");
        var sb = Start(
            "the first three columns are READ from G_092's measured rows rather than restated, so the two audits",
            "cannot drift apart;",
            "the fourth column is this audit's closure measurement, and every true row carries its witness;",
            "the closure loss of a removal is kept apart from the closure necessity: a structure can be an input, or",
            "can be what DEFINES an object that would otherwise have to be supplied;",
            "whether the theory HAS the structure is a third thing again, and is reported separately.");
        sb.Append(DynamicCoreRecoveryAudit.OutputMatrix());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var matrix = DynamicCoreRecoveryAudit.Structures();
        Assert.Equal(19, matrix.Length);
        // the three base columns agree with G_092's own rows, name for name
        var fromG092 = ActuatorVersusObservableAudit.Structures().ToDictionary(s => s.Name);
        Assert.All(matrix.Where(m => fromG092.ContainsKey(m.Name)), m =>
        {
            Assert.Equal(fromG092[m.Name].Observable, m.ObservableNecessary);
            Assert.Equal(fromG092[m.Name].Dynamical, m.DynamicNecessary);
            Assert.Equal(fromG092[m.Name].Actuatable, m.ActuatorNecessary);
        });
        // the four columns' totals
        Assert.Equal(7, matrix.Count(m => m.ObservableNecessary));
        Assert.Equal(5, matrix.Count(m => m.DynamicNecessary));
        Assert.Equal(1, matrix.Count(m => m.ActuatorNecessary));
        Assert.Equal(10, matrix.Count(m => m.ClosureNecessary));
        // removal costs closure in exactly two places, both of them definitional
        var withClosureLoss = matrix.Where(m => m.ClosureLossOnRemoval > 0).ToArray();
        Assert.Equal(2, withClosureLoss.Length);
        Assert.Contains(withClosureLoss, m => m.Name.Contains("transport operator"));
        Assert.Contains(withClosureLoss, m => m.Name == "the continuity law");
        // and three structures are absent from the surviving theory
        Assert.Equal(3, matrix.Count(m => !m.PresentInTheTheory));
        // every closure row has a witness, and no closure row is witness-free
        Assert.All(matrix.Where(m => m.ClosureNecessary), m => Assert.False(string.IsNullOrWhiteSpace(m.ClosureWitness)));

        sb.AppendLine($"  the matrix's totals: observable {7}, dynamic {5}, actuator {1}, closure {10}; removal costs closure in");
        sb.AppendLine($"  {withClosureLoss.Length} places (a definitional removal turns the defined object into an input); and");
        sb.AppendLine($"  {matrix.Count(m => !m.PresentInTheTheory)} structures are ABSENT from the surviving theory.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_03_TheSearchForDynamicallyEssentialYetObservablyRedundant()
    {
        PrintHeader("G_093 - task 5: observable necessity no, dynamic necessity yes");
        var sb = Start(
            "the task asks for this combination specifically, so it is computed as a set intersection over the matrix;",
            "the result is cross-checked against G_092's own search, which asked the same question on its own columns;",
            "G_092's further finding - that none of them is actuatable and their effect is restrictive - is carried",
            "here rather than restated, and is asserted against G_092's own API.");
        sb.Append(DynamicCoreRecoveryAudit.OutputSearch());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var search = DynamicCoreRecoveryAudit.DynamicallyEssentialAndObservablyRedundant();
        Assert.Equal(3, search.Length);
        Assert.Contains(search, s => s.Name == "the component count");
        Assert.Contains(search, s => s.Name == "density topology");
        Assert.Contains(search, s => s.Name.Contains("transport operator"));
        Assert.All(search, s => Assert.False(s.ObservableNecessary));
        Assert.All(search, s => Assert.True(s.DynamicNecessary));
        // the same set G_092 found, so the two searches agree
        Assert.Equal(ActuatorVersusObservableAudit.DynamicalWithoutObservables().Length, search.Length);
        Assert.Equal(ActuatorVersusObservableAudit.DynamicalWithoutObservables().Select(s => s.Name).OrderBy(n => n),
            search.Select(s => s.Name).OrderBy(n => n));
        // and none of them is actuatable - the success criterion is met in the LETTER, and G_092 says why it is narrow
        Assert.All(search, s => Assert.False(s.ActuatorNecessary));
        Assert.Equal(0, ActuatorVersusObservableAudit.RoleCensus().InvisibleActuator);

        sb.AppendLine($"  {search.Length} structures are dynamically essential and observably redundant, and the set is the one");
        sb.AppendLine("  G_092 found - so the success criterion is MET. What is NOT added is a channel: none is actuatable,");
        sb.AppendLine("  and G_092 measured that their reach effect is restrictive (n - c can only fall).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_04_TheFirstPrinciplesGapAudit()
    {
        PrintHeader("G_093 - task 6: the four paths, under the criterion 'required for closure?'");
        var sb = Start(
            "the task names four paths, so all four are judged by the same criterion and none is skipped;",
            "the criterion is closure, not observability: a path is required when the theory cannot supply what it",
            "carries from its own vocabulary;",
            "mode locking is judged by G_090's measurement that it is a function of the distribution, which is what",
            "makes it unable to close anything;",
            "the unified action is judged by E_004's substrate assignment, which is computed rather than quoted.");
        sb.Append(DynamicCoreRecoveryAudit.OutputSearch());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var gaps = DynamicCoreRecoveryAudit.ClosureGaps();
        Assert.Equal(4, gaps.Length);
        Assert.True(gaps.Single(g => g.Path.Contains("memory-channel")).RequiredForClosure);
        Assert.False(gaps.Single(g => g.Path.Contains("mode-locking")).RequiredForClosure);
        Assert.True(gaps.Single(g => g.Path.Contains("theta")).RequiredForClosure);
        Assert.True(gaps.Single(g => g.Path.Contains("unified action")).RequiredForClosure);
        Assert.Equal(3, gaps.Count(g => g.RequiredForClosure));

        sb.AppendLine($"  of the four paths, {gaps.Count(g => g.RequiredForClosure)} are REQUIRED for closure and {gaps.Count(g => !g.RequiredForClosure)} is closed.");
        sb.AppendLine("  The closed one is mode locking, because a locked mode IS the occupancy, renamed.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_05_TheThreeCoresAndTheirDimensions()
    {
        PrintHeader("G_093 - tasks 7 and 8: ObservableCore, DynamicCore, ClosureCore");
        var sb = Start(
            "the three cores are named sets of structures, computed from the matrix rather than listed;",
            "the surplus of a core is the members it holds ALONE - in neither of the other two - which is what makes",
            "a comparison a comparison;",
            "dimensions alone would not settle coincidence, so each pair's intersection and each core's surplus are",
            "computed as well;",
            "the failure criterion is 'the three cores coincide', which requires equal dimensions AND containment.");
        sb.Append(DynamicCoreRecoveryAudit.OutputCores());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var cores = DynamicCoreRecoveryAudit.Cores();
        Assert.Equal(3, cores.Length);
        Assert.Equal(7, cores.Single(c => c.Core == CoreName.Observable).Dimension);
        Assert.Equal(5, cores.Single(c => c.Core == CoreName.Dynamic).Dimension);
        Assert.Equal(10, cores.Single(c => c.Core == CoreName.Closure).Dimension);
        // the dimensions are three different numbers, so no two cores coincide
        var dimensions = DynamicCoreRecoveryAudit.Dimensions();
        Assert.Equal(3, dimensions.Select(d => d.Dimension).Distinct().Count());
        // and each core holds at least one member the other two do not: the comparison is not a coincidence of sizes
        Assert.All(dimensions, d => Assert.True(d.Surplus > 0));
        Assert.Equal(1, dimensions.Single(d => d.Core == CoreName.Observable).Surplus);
        Assert.Equal(1, dimensions.Single(d => d.Core == CoreName.Dynamic).Surplus);
        Assert.Equal(3, dimensions.Single(d => d.Core == CoreName.Closure).Surplus);
        // the closure core is the largest, and its surplus is exactly the absent three
        Assert.True(dimensions.Single(d => d.Core == CoreName.Closure).Dimension
            > dimensions.Single(d => d.Core == CoreName.Observable).Dimension);

        sb.AppendLine($"  the cores measure {string.Join(", ", dimensions.Select(d => d.Dimension))} - three different numbers -");
        sb.AppendLine("  and each holds a member the other two do not (surpluses 1, 1, 3). The failure criterion is refuted.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_06_TheClosureSurplusIsAbsent()
    {
        PrintHeader("G_093 - task 8: the closure surplus, and whether the theory has it");
        var sb = Start(
            "the surplus is the set required for closure that is neither observable nor dynamical: the only place the",
            "question's success criterion could hide a genuinely recovered structure;",
            "for each member the audit reports whether the surviving theory HAS it, because the question forbids",
            "assuming the minimal core is complete;",
            "absent is reported as absent, not as inert - the distinction the question's third rule demands.");
        sb.Append(DynamicCoreRecoveryAudit.OutputCores());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var surplus = DynamicCoreRecoveryAudit.ClosureSurplus();
        Assert.Equal(3, surplus.Length);
        Assert.Contains(surplus, s => s.Name == "memory channels");
        Assert.Contains(surplus, s => s.Name == "the vector sector");
        Assert.Contains(surplus, s => s.Name == "the unified action");
        // every one of them is required for closure, and NONE of them is observable or dynamical or actuatable
        Assert.All(surplus, s => Assert.True(s.ClosureNecessary));
        Assert.All(surplus, s => Assert.False(s.ObservableNecessary));
        Assert.All(surplus, s => Assert.False(s.DynamicNecessary));
        Assert.All(surplus, s => Assert.False(s.ActuatorNecessary));
        // and the surviving theory has none of them
        Assert.All(surplus, s => Assert.False(s.PresentInTheTheory));
        Assert.Equal(3, DynamicCoreRecoveryAudit.Structures().Count(s => !s.PresentInTheTheory));

        sb.AppendLine($"  the closure surplus is {surplus.Length} structures - the memory channel, the vector sector and the unified");
        sb.AppendLine("  action - and the surviving theory has NONE of them. The reduction did not remove them: they were");
        sb.AppendLine("  never there, which is why the audit reports them as ABSENT rather than as inert.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_07_TheReductionCostsTheClosureLedgerNothing()
    {
        PrintHeader("G_093 - the concern itself: did observable minimization remove a necessity?");
        var sb = Start(
            "the question's concern is taken at face value and answered on each axis, not summarised;",
            "the reduction's own strip list is the experiment: flux, displaced flux, conservation statement,",
            "transport operator and ledger;",
            "what the reduction removed must be shown to be DERIVED from the operations rather than merely unread;",
            "the demonstration is the chain's own measurements, not a re-reading of them.");
        sb.Append(DynamicCoreRecoveryAudit.OutputTheories());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var full = MinimalGravityCoreAudit.Full();
        var minimal = MinimalGravityCoreAudit.Minimal();
        // what the reduction removed, measured against what it kept
        Assert.NotEmpty(full.Flux);
        Assert.Empty(minimal.Flux);
        Assert.Empty(minimal.DisplacedFlux);
        Assert.True(full.Conservation);
        Assert.False(minimal.Conservation);
        Assert.True(full.TransportOperator);
        Assert.False(minimal.TransportOperator);
        Assert.True(full.Ledger);
        Assert.False(minimal.Ledger);
        // and every one of them is DERIVED: the layer's identity holds exactly, so the operations imply the statement
        Assert.True(MinimalGravityCoreAudit.TransportLayerContribution().Single().Holds);
        Assert.Equal(0, MinimalGravityCoreAudit.TransportLayerContribution().Single().Residual);
        // the state and the links - the two things the evolution needs - are untouched
        Assert.Equal(full.Rho.Length, minimal.Rho.Length);
        Assert.Equal(full.Links.Length, minimal.Links.Length);
        // and the closure ledger does not move: the inputs are the same set before and after
        Assert.Equal(DynamicCoreRecoveryAudit.Theories()[0].ClosureInputs,
            DynamicCoreRecoveryAudit.Theories()[1].ClosureInputs);

        sb.AppendLine("  the reduction removed the flux, the conservation statement and the ledger - and the layer's identity");
        sb.AppendLine("  holds EXACTLY, so the operations imply every one of them. The state and the links are untouched,");
        sb.AppendLine("  the closure ledger is unmoved, and no axis distinguishes A from B.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_08_TheCriteriaAndTheVerdict()
    {
        PrintHeader("G_093 - the criteria, and the verdict computed from the matrix");
        var sb = Start(
            "the task states a success and a failure criterion, so both are decided;",
            "every clause of the verdict is a measured dimension, a measured residual or a measured count (G_027);",
            "the concern is answered on all three axes rather than on the one that is easiest to check;",
            "no literal reaches the verdict.");
        sb.Append(DynamicCoreRecoveryAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var criteria = DynamicCoreRecoveryAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.Equal("REFUTED", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.StartsWith("NO, on all three axes", criteria.Single(c => c.Criterion.StartsWith("and the question")).Outcome);

        var verdict = DynamicCoreRecoveryAudit.Verdict();
        Assert.StartsWith("NOTHING NECESSARY WAS REMOVED, AND NOTHING ABSENT WAS CLOSED", verdict);
        Assert.Contains("THE THREE CORES ARE THREE DIFFERENT SETS", verdict);
        Assert.Contains("AND THE CLOSURE SURPLUS IS ABSENT", verdict);
        Assert.Contains("OF THE FOUR PATHS", verdict);
        var dimensions = DynamicCoreRecoveryAudit.Cores();
        Assert.Contains(dimensions.Single(c => c.Core == CoreName.Closure).Dimension
            .ToString(System.Globalization.CultureInfo.InvariantCulture), verdict);

        sb.AppendLine();
        sb.AppendLine("  VERDICT: " + verdict);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_093_09_TheAuditAgreesWithTheChainAndWithE_004()
    {
        PrintHeader("G_093 - cross-check against G_084, G_085, G_092 and E_004");
        var sb = Start(
            "the audit consumes the chain's results and one from group E, so both are cross-checked by computed",
            "identity rather than by quotation;",
            "the first three columns are G_092's, so the two censuses must agree exactly;",
            "the closure column's two non-trivial rows are G_085's free room and E_004's substrate assignment;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(ActuatorVersusObservableAudit.OutputCensus());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // G_092: the censuses must agree, because the columns are read rather than restated
        var census = ActuatorVersusObservableAudit.RoleCensus();
        var matrix = DynamicCoreRecoveryAudit.Structures();
        Assert.Equal(census.Observable, matrix.Count(m => m.ObservableNecessary));
        Assert.Equal(census.Dynamical, matrix.Count(m => m.DynamicNecessary));
        Assert.Equal(census.Actuatable, matrix.Count(m => m.ActuatorNecessary));
        Assert.Equal(census.InvisibleActuator, 0);
        // G_084: the minimal core reproduces 8 of 8 and the layer's identity holds exactly
        Assert.Equal(8, MinimalGravityCoreAudit.Reproduction().Length);
        Assert.All(MinimalGravityCoreAudit.Reproduction(), r => Assert.True(r.ProducedInBoth));
        Assert.True(MinimalGravityCoreAudit.TransportLayerContribution().Single().Holds);
        // G_085: the clock law's shape is genuinely free - more than one member fits the pinned data
        Assert.True(MinimalGravityCoreAudit.ClockLawFamily().Alternatives.Length > 1);
        Assert.Contains(MapRedundancyAudit.VariationTable(),
            v => v.Variation.Contains("free room") && v.PredictionsMoved > 0);
        // G_087: the curvature is undecidable in situ, which is why it is an input — the ratio there is
        // separation / signature, so a ratio ABOVE one means the separation exceeds the signature
        Assert.True(KernelDiscriminabilityAudit.AgainstTheSeparation().Ratio > 1.0);
        // E_004: the vector sector is not on the time sector's substrate, and the 4-vector splits across both
        Assert.False(VectorSectorAudit.SingleRingHasVectorSector());
        Assert.True(VectorSectorAudit.CubicHasVectorSector());
        Assert.Equal(2, VectorSectorAudit.SubstrateAssignment().Select(p => p.SuppliedBy).Distinct().Count());
        Assert.Equal(VectorSectorAudit.VectorDimension,
            VectorSectorAudit.LorentzVectorSplit().Single(p => p.Part.Contains("spatial")).Dimension);

        sb.AppendLine("  the chain's own numbers are reproduced: the censuses agree with G_092 (7 observable, 5 dynamical,");
        sb.AppendLine("  1 actuatable, 0 invisible actuators), the minimal core reproduces all 8 readouts with the layer's");
        sb.AppendLine("  identity exact, the clock law's shape is genuinely free, the curvature is undecidable, and E_004's");
        sb.AppendLine("  vector sector needs the cubic substrate while the Lorentz vector splits across both.");
        Output.WriteLine(sb.ToString());
    }
}
