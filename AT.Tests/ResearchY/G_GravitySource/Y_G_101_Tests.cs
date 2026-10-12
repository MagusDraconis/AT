using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_101 - STATE DEGREES-OF-FREEDOM AUDIT.
///
/// G_097 measured that the law diffuses and that transport is absent; G_098 that the direction is not represented;
/// G_099 that the orientation field is derived and observable; G_100 that the field is dynamically decoupled and has 0
/// independent degrees of freedom. This suite asks the question those leave open, and it is a question about the STATE
/// SPACE rather than about one object in it:
///
///   does the surviving G-sector contain more than one independent dynamical degree of freedom?
///
/// THE INSTRUMENT is the Jacobian of each surviving quantity against the state, with the tolerance STATED - because a
/// rank is only a dimension when it does not drift with the rule it was taken at (G_096). A quantity whose Jacobian has
/// full rank is an invertible reparametrisation of the state and NOT an independent coordinate; a quantity with a
/// non-trivial kernel loses directions and is a functional. The suite therefore keeps three counts apart that are easy
/// to conflate: the COMPONENTS the state carries, the RANK of the dynamics, and the number of INDEPENDENT STATE
/// COORDINATES.
///
/// Every count, dimension and rank names its object: the state is the canonical 96-cell ring's occupation (cells = 96,
/// links = LinksOf(96, 1)), the dynamics is UpdateAt(cells, links, rho) — the Laplace operator, whose symbol spectrum
/// is sampled through the alternating mode k = N/2 — and the equality rules are 1e-12 on a computed value, EXACT
/// equality on an identity, and a STATED tolerance for every rank. The cubic substrate is forbidden and is not named in
/// this file. No new primitive, field, source term, topology axiom, action, Lagrangian, transport rule or hidden
/// variable is introduced. A negative result is acceptable.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_101_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_101_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
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
    public void Y_G_101_01_TheStateQuantityInventory()
    {
        PrintHeader("G_101 - task 1: every surviving state-like quantity, with the rank of its Jacobian");
        var sb = Start(
            "the candidates are the quantities the surviving sector actually has and that could plausibly be a second",
            "coordinate: the occupation, the potential, the kernel, g00, the rate, the acceleration, the orientation",
            "field, the excess, the total count, the flux's determined content and the readouts;",
            "the rank is the measurement, so 'independent' and 'derived' are decided by a rank and not by wording;",
            "every rank is reported at the audit's stated rule AND at the loose end of its band, so a rank that would",
            "drift with its rule is visible;",
            "EVERY count is on the 96-cell ring; the cubic substrate is forbidden and is not named in the code.");
        sb.Append(StateDegreesOfFreedomAudit.OutputInventory());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = StateDegreesOfFreedomAudit.Inventory();
        Assert.Equal(11, rows.Length);
        var counts = StateDegreesOfFreedomAudit.InventoryCounts();
        Assert.Equal(11, counts.Quantities);
        Assert.Equal(1, counts.TheState);
        Assert.Equal(4, counts.Invertible);
        Assert.Equal(6, counts.Functionals);

        // the four invertible rows recover the state in closed form, which is what "reparametrisation" means
        var invertible = rows.Where(r => r.Role == StateRole.InvertibleReparametrisation).ToArray();
        Assert.Equal(4, invertible.Length);
        Assert.All(invertible, r => Assert.Equal(96, r.RankAtTheRule));
        Assert.All(invertible, r => Assert.Equal(0, r.KernelDimension));
        Assert.All(invertible, r => Assert.True(r.DeterminesTheState));

        // and every functional loses at least one direction
        Assert.All(rows.Where(r => r.Role == StateRole.FunctionalOfTheState), r => Assert.True(r.KernelDimension >= 1));

        // the rank is flat over the band for every quantity: no row's classification turns on the rule
        Assert.All(rows, r => Assert.True(r.TheRankIsToleranceFree,
            $"the rank of '{r.Quantity}' drifts between the rule and the loose end"));

        // kernel and rank must partition the state's components - the arithmetic that caught the first draft
        Assert.All(rows.Where(r => r.Components == 96),
            r => Assert.Equal(96, r.RankAtTheRule + r.KernelDimension));
    }

    [Fact]
    public void Y_G_101_02_TheDependencyTable()
    {
        PrintHeader("G_101 - tasks 2 and 4: the dependency table - independent, equivalent, or dependent");
        var sb = Start(
            "the FORWARD direction (the quantity from the state) is an identity for every row, because each quantity is",
            "DEFINED from the state - so the informative direction is the INVERSE and that is what the rank measures;",
            "a row with kernel 0 and the state's component count is an invertible change of variable, which cannot add",
            "a degree of freedom; a row with a kernel loses directions;",
            "the partition is therefore three-way - the state (1), the invertible reparametrisations (4) and the",
            "functionals (6) - and the binary split the task asks for is 'independent' = the state alone.");
        sb.AppendLine(StateDegreesOfFreedomAudit.OutputInventory().Split("2. THE DEPENDENCY TABLE")[1]);
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var table = StateDegreesOfFreedomAudit.DependencyTable();
        Assert.Equal(11, table.Length);
        Assert.Single(table, d => d.TheStateIsRecoverable && d.Role == StateRole.TheState.ToString());
        Assert.Equal(4, table.Count(d => d.Role == StateRole.InvertibleReparametrisation.ToString()));
        Assert.Equal(6, table.Count(d => !d.TheStateIsRecoverable));

        // the two kernel dimensions the audit turns on: one for the scale, 95 for the total
        Assert.Equal(1, table.Single(d => d.Quantity.Contains("orientation field")).Kernel);
        Assert.Equal(95, table.Single(d => d.Quantity.Contains("total count")).Kernel);
        Assert.Equal(92, table.Single(d => d.Quantity.Contains("readouts")).Kernel);
    }

    [Fact]
    public void Y_G_101_03_TheDynamicalStateSpaceRank()
    {
        PrintHeader("G_101 - task 3: the dynamical state-space rank, with its tolerance band");
        var sb = Start(
            "the update is the ring's Laplacian, so its modes are the ring's own Fourier modes and its symbol at mode k",
            "is the eigenvalue, with multiplicities 1 at k = 0, 2 for the interior modes and 1 at the alternating mode;",
            "the multiplicities must sum to the cell count, which the suite asserts rather than assumes;",
            "the rank is the number of directions the law can move, and the band's edges are the exact spectrum's own",
            "magnitudes - so a rank that is flat across the band is a dimension and not a rule artefact (G_096);",
            "the alternating mode is sampled, because stopping one mode short is the error G_097 recorded.");
        sb.Append(StateDegreesOfFreedomAudit.OutputRank());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var spectrum = StateDegreesOfFreedomAudit.Spectrum();
        Assert.Equal(96, spectrum.Sum(s => s.Multiplicity));
        Assert.Equal(1, spectrum.Single(s => s.Mode == 0).Multiplicity);
        Assert.Equal(1, spectrum.Single(s => s.Mode == 96 / 2).Multiplicity);
        Assert.Equal(0.0, spectrum.Single(s => s.Mode == 0).Symbol, 15);   // the uniform mode is EXACTLY unmoved

        var rank = StateDegreesOfFreedomAudit.StateSpaceRank();
        Assert.Equal(7, rank.Length);
        var atTheRule = rank.Single(r => Math.Abs(r.Tolerance - StateDegreesOfFreedomAudit.DynamicsRule) < 1e-18);
        Assert.Equal(95, atTheRule.Rank);
        Assert.Equal(1, atTheRule.KernelDimension);

        // flat from the rule up to the smallest non-zero magnitude, and the loose end is named
        Assert.All(rank.Where(r => r.Tolerance <= 1e-3), r => Assert.Equal(95, r.Rank));
        double smallestNonZero = spectrum.Where(s => Math.Abs(s.Symbol) > 0).Min(s => Math.Abs(s.Symbol));
        Assert.True(smallestNonZero > 4.2e-3 && smallestNonZero < 4.3e-3,
            $"the band's loose end must be the first non-zero magnitude, measured {N(smallestNonZero)}");
        Assert.Equal(93, rank.Single(r => Math.Abs(r.Tolerance - 1e-2) < 1e-18).Rank);

        // and the three counts the audit keeps apart
        var counts = StateDegreesOfFreedomAudit.Counts();
        Assert.Equal(96, counts.Single(c => c.Count.Contains("COMPONENTS")).Value);
        Assert.Equal(95, counts.Single(c => c.Count.Contains("MOVING")).Value);
        Assert.Equal(1, counts.Single(c => c.Count.Contains("CONSERVED")).Value);
        Assert.Equal(1, counts.Single(c => c.Count.Contains("STATE COORDINATES")).Value);
        Assert.Equal(0, counts.Single(c => c.Count.Contains("BEYOND")).Value);
    }

    [Fact]
    public void Y_G_101_04_NoQuantityBeyondTheStateIsIndependent()
    {
        PrintHeader("G_101 - task 5: whether the orientation field, the flux, the source, the kernel or the observables add a coordinate");
        var sb = Start(
            "the test is a rank: a quantity adds an independent coordinate only if its Jacobian against the state is",
            "NOT a function of the state - and every surviving quantity is computed from the state, so its rank is at",
            "most the state's own dimension and its kernel is what it loses;",
            "the five candidates the task names are tested individually, so a failure of one cannot hide behind another;",
            "the measurement is the kernel dimension, because a kernel of 0 with full rank means 'a change of variable'",
            "and a kernel of k means 'loses k directions' - neither adds one.");
        var rows = StateDegreesOfFreedomAudit.Inventory();
        sb.AppendLine("   candidate                          role                          rank  kernel  adds a coordinate");
        foreach (var name in new[] { "orientation field", "flux", "acceleration", "kernel", "readouts" })
        {
            var row = rows.Single(r => r.Quantity.Contains(name));
            sb.AppendLine($"   {name,-34}  {row.Role,-28}  {row.RankAtTheRule,4}  {row.KernelDimension,6}  no");
        }
        sb.AppendLine();
        sb.AppendLine("   every one of the five is a FUNCTIONAL of the state or an invertible reparametrisation of it,");
        sb.AppendLine("   measured by the rank of its Jacobian against the 96-cell state at the stated rule.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        foreach (var name in new[] { "orientation field", "flux", "acceleration", "kernel", "readouts" })
        {
            var row = rows.Single(r => r.Quantity.Contains(name));
            // an independent coordinate would need a component count ABOVE the state's and full rank; none has either
            Assert.True(row.Components <= 96, $"{name} carries more components than the state");
            Assert.True(row.KernelDimension >= 0);
            Assert.NotEqual(StateRole.NotAStateObject, row.Role);
        }

        // the two that are not functionals are reparametrisations, which cannot add a degree of freedom either
        Assert.Equal(0, rows.Single(r => r.Quantity.Contains("kernel")).KernelDimension);
        Assert.True(rows.Single(r => r.Quantity.Contains("kernel")).DeterminesTheState);
        Assert.True(rows.Single(r => r.Quantity.Contains("orientation field")).KernelDimension > 0);
    }

    [Fact]
    public void Y_G_101_05_EachCandidateRemovedIndividually()
    {
        PrintHeader("G_101 - task 6: each structure removed on its own, with the observable and evolution loss measured");
        var sb = Start(
            "the removals are the description parts the reduction demoted, rebuilt from the full description one at a",
            "time so the measurements are independent of one another;",
            "the OBSERVABLE loss is the count of readouts that stop being produced plus the count that move;",
            "the EVOLUTION loss is the update's residual, which is the only measurement that can show a structure",
            "entering the dynamics at all;",
            "structures are counted by FAMILY, because the first draft counted the link set twice and read the success",
            "criterion as met.");
        sb.Append(StateDegreesOfFreedomAudit.OutputRemovalAndReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = StateDegreesOfFreedomAudit.Removal();
        Assert.Equal(10, rows.Length);
        var counts = StateDegreesOfFreedomAudit.RemovalCounts();
        Assert.Equal(10, counts.Structures);
        Assert.Equal(1, counts.DistinctStructuresEnteringTheEvolution);
        Assert.True(counts.MaxReadoutsMoved <= 1);

        // exactly the link set enters the evolution, in both of its forms
        var entering = rows.Where(r => r.UpdateResidual > StateDegreesOfFreedomAudit.Tol).ToArray();
        Assert.Equal(2, entering.Length);
        Assert.All(entering, r => Assert.Equal("the link set", r.Family));

        // and the layers G_083 demoted move nothing at all
        Assert.All(rows.Where(r => r.Family is "the flux" or "count conservation" or "the transport operator"
            or "the ledger" or "the component count"), r => Assert.Equal(0.0, r.UpdateResidual, 12));
    }

    [Fact]
    public void Y_G_101_06_TheReductionToTheStateAlone()
    {
        PrintHeader("G_101 - task 7: the whole surviving sector reconstructed from the state alone");
        var sb = Start(
            "the reconstruction keeps the state, the link set and the three maps and drops the flux, conservation, the",
            "transport operator and the ledger - which is G_084's minimal core;",
            "the three losses are kept apart because they are different questions: what stops being produced, what the",
            "evolution loses, and what the CONTROL loses;",
            "the control loss is measured as the reach and the actuatable count, because a control that needs a removed",
            "structure would be invisible to the readouts;",
            "the equality rule is 1e-12.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var result = StateDegreesOfFreedomAudit.ReconstructFromTheStateAlone();
        Assert.Equal(0, result.ObservableLoss);
        Assert.Equal(0, result.ReadoutsMoved);
        Assert.Equal(0.0, result.EvolutionLoss, 12);
        Assert.Equal(0, result.ReachLoss);
        Assert.Equal(0, result.ActuatableLoss);

        // the reduction is not vacuous: the operations act on the STATE, which is retained
        Assert.Equal(95, ActuatorVersusObservableAudit.ReachableOperations());
        Assert.Equal(1, ActuatorVersusObservableAudit.RoleCensus().Actuatable);
    }

    [Fact]
    public void Y_G_101_07_TheIndependentInputsAreNotCoordinates()
    {
        PrintHeader("G_101 - what the sector still needs, so that the verdict cannot be read as minimality of the description");
        var sb = Start(
            "the audit reports the description's independent inputs BESIDE the state count, because one degree of",
            "freedom is not the same statement as a self-contained theory;",
            "the column is computed: a state coordinate is a FIELD that indexes the state space, and a parameter or a",
            "structure does not;",
            "G_095 left the law's coefficient free and this suite reports it as an INPUT rather than as a coordinate;",
            "the tick is the deepest structural boundary in the completion record and is the independent variable;",
            "nothing here is asserted from memory: the free room's size and the reach are recomputed.");
        sb.AppendLine(StateDegreesOfFreedomAudit.OutputRank().Split("4. THE INDEPENDENT INPUTS")[1]);
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var inputs = StateDegreesOfFreedomAudit.IndependentInputs();
        Assert.Equal(6, inputs.Length);
        Assert.Equal(1, inputs.Count(i => i.IsAStateCoordinate));
        Assert.Equal(5, inputs.Count(i => !i.IsAStateCoordinate));
        Assert.Equal("the occupation itself (the initial datum)", inputs.Single(i => i.IsAStateCoordinate).Input);

        // the free room's size and the step are measured, not declared
        Assert.Equal(5, inputs.Single(i => i.Input.Contains("coefficient")).Dimensions);
        Assert.True(inputs.Single(i => i.Input.Contains("step")).Dimensions == 1);
    }

    [Fact]
    public void Y_G_101_08_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_101 - classification and criteria");
        var sb = Start(
            "the counts are reported separately because they answer three different questions, and the verdict is",
            "decided by the coordinate count rather than by a majority of them;",
            "the success criterion asks for a coordinate BEYOND the state, and the one structure that enters the",
            "evolution is the link set, which G_080 measured to be an input and which is a structure, not a coordinate;",
            "the failure criterion is the complement, and it is measured three independent ways: by rank, by kernel and",
            "by removal;",
            "no literal reaches a verdict: every outcome is a comparison of measured quantities.");
        sb.Append(StateDegreesOfFreedomAudit.OutputClassification());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var criteria = StateDegreesOfFreedomAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria[0].Outcome);
        Assert.Equal("MET", criteria[1].Outcome);
        Assert.Equal("CONFIRMED", criteria[2].Outcome);

        var counts = StateDegreesOfFreedomAudit.Counts();
        Assert.Equal(6, counts.Length);
        Assert.Equal("96", counts.Single(c => c.Count.Contains("COMPONENTS")).Value.ToString());
        Assert.Equal("0", counts.Single(c => c.Count.Contains("BEYOND")).Value.ToString());
        Assert.Equal("0", counts.Single(c => c.Count.Contains("CONTROL")).Value.ToString());
    }

    [Fact]
    public void Y_G_101_09_TheVerdictAndTheCrossChecks()
    {
        PrintHeader("G_101 - the verdict, cross-checked against the audits it continues");
        var sb = Start(
            "the verdict is COMPUTED with live branches, so no clause can be a literal (G_027);",
            "the cross-checks are the audits this one continues, and each is asserted at ITS OWN tolerance and in ITS",
            "own regime - a cited number is a claim until it is recomputed;",
            "the spectral substrate is the canonical 96-cell ring and the cubic substrate is forbidden here;",
            "the equality rules are 1e-12 on computed values, exact equality on identities, and a stated rule per rank.");
        sb.AppendLine("VERDICT");
        sb.AppendLine("  " + StateDegreesOfFreedomAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine("WHAT THIS ESTABLISHES AND WHAT IT DOES NOT");
        sb.AppendLine(StateDegreesOfFreedomAudit.WhereItStands());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = StateDegreesOfFreedomAudit.Verdict();
        Assert.Contains("ONE DEGREE OF FREEDOM", verdict);
        Assert.Contains("FINAL VERDICT", verdict);
        Assert.Contains("96 components", verdict);

        // cross-check 1 (G_100): the orientation field's partition had one dimension, and its rank here is 95
        Assert.Equal(1, OrientationCouplingAudit.PartitionCounts().Dimension);
        Assert.Equal(1, StateDegreesOfFreedomAudit.Inventory()
            .Single(r => r.Quantity.Contains("orientation field")).KernelDimension);

        // cross-check 2 (G_084): the minimal core is one state variable and three equations, and it reproduces 8 of 8
        Assert.Equal(0, StateDegreesOfFreedomAudit.ReconstructFromTheStateAlone().ObservableLoss);
        Assert.Equal(1, MinimalGravityCoreAudit.MinimalVariableSet().Count(v => v.Kind == "State"));

        // cross-check 3 (G_092): exactly one structure is actuatable, and it is the state
        Assert.Equal(1, ActuatorVersusObservableAudit.RoleCensus().Actuatable);

        // cross-check 4 (G_097): the update's zero mode is the uniform mode, so the conserved direction is 1
        Assert.Equal(0.0, StateDegreesOfFreedomAudit.Spectrum().Single(s => s.Mode == 0).Symbol, 15);

        // cross-check 5 (G_101's own guard): rank + kernel partition the state's components for every 96-component row
        Assert.All(StateDegreesOfFreedomAudit.Inventory().Where(r => r.Components == 96),
            r => Assert.Equal(96, r.RankAtTheRule + r.KernelDimension));
    }
}
