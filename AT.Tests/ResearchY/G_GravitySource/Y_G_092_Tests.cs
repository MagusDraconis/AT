using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_092 - ACTUATOR VERSUS OBSERVABLE AUDIT.
///
/// G_084-G_091 reduced the observable sector, each time on the ground that the surviving observables did not move.
/// This suite asks the converse question: did any eliminated structure remain necessary for DYNAMICS or for CONTROL?
/// Observable-reduction arguments and "not observed therefore irrelevant" are both forbidden, so the three roles are
/// measured on three different objects - the readouts, the update relation with the class's reach, and the class's
/// operations - and the question "is it REQUIRED for control?" is kept apart from "can an operator SET it?".
///
/// The report follows the six tasks. Every figure is computed here; the substrate is the canonical 96-cell ring with
/// the counterfactual cuts c = 1, 2, 4, the equality rule is 1e-12, and no spectral quantity is quoted.
/// </summary>
public sealed class Y_G_092_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_092_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_092_01_ObservableRoleVersusDynamicalRole()
    {
        PrintHeader("G_092 - task 1: separate the observable role from the dynamical role");
        var sb = Start(
            "the three roles are PROCEDURES, each measured on a different object, so that a structure may hold any",
            "combination of them;",
            "observable-reduction arguments are forbidden, so 'the reduction kept the readouts' is never used as",
            "evidence about dynamics;",
            "'not observed, therefore irrelevant' is forbidden, so relevance is never read off observability;",
            "the three objects are the readouts (the description), the update relation and the class's reach (the",
            "substrate and the class), and the operation set;",
            "the substrate is the canonical 96-cell ring; the equality rule is 1e-12 on a computed value.");
        sb.Append(ActuatorVersusObservableAudit.OutputRoles());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var definitions = ActuatorVersusObservableAudit.RoleDefinitions();
        Assert.Equal(4, definitions.Length);
        Assert.Equal(3, definitions.Count(d => d.Role != "REQUIRED FOR CONTROL"));
        Assert.Contains(definitions, d => d.Role == "REQUIRED FOR CONTROL");

        // the roles are separable IN THE INVENTORY, measured: structures exist that are read and not evolved, and
        // structures that are evolved and not read
        var rows = ActuatorVersusObservableAudit.Structures();
        Assert.NotEmpty(rows.Where(r => r.Observable && !r.Dynamical));
        Assert.NotEmpty(rows.Where(r => r.Dynamical && !r.Observable));
        // and the two measurements are genuinely independent: a map is read and does not touch the update
        var map = rows.Single(r => r.Name == "the potential map");
        Assert.True(map.Observable);
        Assert.False(map.Dynamical);
        Assert.Equal(0.0, map.UpdateMoved, 12);

        sb.AppendLine($"  the roles separate in the inventory: {rows.Count(r => r.Observable && !r.Dynamical)} structures are");
        sb.AppendLine($"  read and not evolved, {rows.Count(r => r.Dynamical && !r.Observable)} are evolved and not read, and");
        sb.AppendLine($"  {rows.Count(r => r.Observable && r.Dynamical)} are both. The two roles are not two names for one thing.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_02_TheNecessityTableAndTheFourWayCensus()
    {
        PrintHeader("G_092 - task 2 and task 6: the table, and observable / dynamical / actuator / neither");
        var sb = Start(
            "the table's three columns are the three measured roles, and the outcome is a pure function of them;",
            "a structure is OBSERVABLE when removing it loses a readout or moves one beyond the equality rule;",
            "a structure is DYNAMICAL when it moves the update relation, the reach, or the operation set;",
            "a structure is ACTUATABLE when an operation of the class writes it - the strong sense of a knob;",
            "REMOVAL IS ONE STRUCTURE AT A TIME, so the rows are independent measurements.");
        sb.Append(ActuatorVersusObservableAudit.OutputTable());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var table = ActuatorVersusObservableAudit.NecessityTable();
        Assert.Equal(17, table.Length);
        // every row's role is exactly what the role rule computes from its three measured columns
        foreach (var s in ActuatorVersusObservableAudit.Structures())
        {
            Assert.Equal(s.Role, ActuatorVersusObservableAudit.RoleOf(
                s.Observable, s.Dynamical, s.Actuatable, s.InTheVocabulary));
        }
        // the four classes the task names, each read off the table
        var census = ActuatorVersusObservableAudit.RoleCensus();
        Assert.Equal(17, census.Total);
        Assert.Equal(7, census.Observable);
        Assert.Equal(5, census.Dynamical);
        Assert.Equal(1, census.Actuatable);
        Assert.Equal(1, census.ObservableAndActuatable);
        Assert.Equal(1, census.BoundaryCondition);
        Assert.Equal(3, census.DynamicalOnly);
        Assert.Equal(5, census.ObservableOnly);
        Assert.Equal(6, census.Inert);
        Assert.Equal(1, census.Absent);
        // and the success criterion's class is EMPTY, which is the audit's refutation
        Assert.Equal(0, census.InvisibleActuator);

        sb.AppendLine($"  {census.Total} structures; observable {census.Observable}, dynamical {census.Dynamical}, actuatable");
        sb.AppendLine($"  {census.Actuatable}; boundary conditions {census.BoundaryCondition}, dynamical-only");
        sb.AppendLine($"  {census.DynamicalOnly}, observable-only {census.ObservableOnly}, inert {census.Inert}, absent");
        sb.AppendLine($"  {census.Absent} - and INVISIBLE ACTUATORS {census.InvisibleActuator}.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_03_TheCountRowIsMeasuredWithItsOwnInstrument()
    {
        PrintHeader("G_092 - task 2: the cut is not a count-only intervention (a defect found while building this audit)");
        var sb = Start(
            "a cut changes the LINKS as well as the component count, so a readout that moves under a cut is evidence",
            "about the links, not about the count;",
            "the count's own instrument is G_082's union construction, which holds the component-internal link set",
            "fixed and varies only c;",
            "the audit therefore reports the count's row from that instrument and attributes the cut's readout",
            "movement to the LINKS row, where it is measured.");
        sb.Append(ActuatorVersusObservableAudit.OutputTable());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var invariance = ActuatorVersusObservableAudit.ComponentCountInvariance();
        Assert.Equal(0, invariance.ShapeChanges);
        Assert.Equal(0, invariance.ValueChanges);
        Assert.Equal(0, invariance.RatioChanges);
        Assert.Equal(0, invariance.LocalObservablesReadingTheCount);

        var rows = ActuatorVersusObservableAudit.Structures();
        var count = rows.Single(r => r.Name == "the component count");
        var links = rows.Single(r => r.Name == "the link set (connectivity)");
        // the count's own columns are the invariant ones, and the links row carries the cut's readout movement
        Assert.Equal(0, count.ReadoutsLost);
        Assert.Equal(0.0, count.ReadoutsMoved, 12);
        Assert.Equal(0, count.ReadoutsThatMove);
        Assert.False(count.Observable);
        Assert.True(links.Observable);
        Assert.True(links.ReadoutsThatMove >= 1);
        Assert.True(links.ReadoutsMoved > ActuatorVersusObservableAudit.Tol);
        // and the count is dynamical all the same - through the reach, which the cut DOES change
        Assert.True(count.Dynamical);
        Assert.True(count.ReachMoved > 0);

        sb.AppendLine($"  the count's own instrument is invariant: shape {invariance.ShapeChanges}, values");
        sb.AppendLine($"  {invariance.ValueChanges}, ratio {invariance.RatioChanges}, local observables reading the count");
        sb.AppendLine($"  {invariance.LocalObservablesReadingTheCount} - while the LINKS row carries {links.ReadoutsThatMove}");
        sb.AppendLine($"  readout(s) moving by {N(links.ReadoutsMoved)}. The count is dynamical without being observable.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_04_TheChainOnTheFullPreReductionTheory()
    {
        PrintHeader("G_092 - task 3: re-run the manipulation chain with the FULL pre-reduction theory");
        var sb = Start(
            "G_089 ran the channel search over the surviving vocabulary; the task asks for the same search with every",
            "pre-reduction structure available as a candidate channel;",
            "the only question that decides actuatability is whether an operation of the class writes the structure,",
            "so the added rows are judged by the operation set and by nothing else;",
            "the reduction changed no operation - it removed bookkeeping - so the chain is expected to gain nothing,",
            "and that is measured rather than assumed.");
        sb.Append(ActuatorVersusObservableAudit.OutputChain());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var counts = ActuatorVersusObservableAudit.ChainCounts();
        Assert.Equal(1, counts.ActuatableOnTheSurvivingTheory);
        Assert.Equal(1, counts.ActuatableOnTheFullTheory);
        Assert.Equal(0, counts.AddedByTheFullTheory);
        var chain = ActuatorVersusObservableAudit.ChainOnTheFullTheory();
        Assert.Equal(14, chain.Length);
        // exactly one row adds an actuator, and it is the surviving class's own operation on the state
        Assert.Single(chain.Where(r => r.AddsAnActuator));
        Assert.Equal("local density", chain.Single(r => r.AddsAnActuator).Structure);
        // every pre-reduction-only structure adds nothing
        Assert.All(chain.Where(r => r.Structure.Contains("pre-reduction only")), r => Assert.False(r.AddsAnActuator));
        Assert.False(chain.Single(r => r.Structure == "the transport's own rule").AddsAnActuator);
        Assert.False(chain.Single(r => r.Structure == "memory channels").AddsAnActuator);

        sb.AppendLine($"  {chain.Length} channels judged: {counts.ActuatableOnTheFullTheory} actuatable on the full theory against");
        sb.AppendLine($"  {counts.ActuatableOnTheSurvivingTheory} on the reduced one, a gain of {counts.AddedByTheFullTheory}. The full theory's extra");
        sb.AppendLine("  structures are controls without actuators, not channels.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_05_TheTwoFailureCores()
    {
        PrintHeader("G_092 - task 4: FailureCore(observables) versus FailureCore(actuation)");
        var sb = Start(
            "FailureCore(observables) is measured by REMOVAL: the structures whose removal loses or moves a readout;",
            "FailureCore(actuation) is measured in the task's sense - the structures whose removal kills the",
            "OPERATIONS: the update's operands and the rule itself;",
            "the two are deliberately different notions, because a PRECONDITION is not a knob, and the audit's",
            "criterion is about knobs;",
            "the comparison reports the direction of each difference, so a coincidence is not assumed either way.");
        sb.Append(ActuatorVersusObservableAudit.OutputCores());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var cores = ActuatorVersusObservableAudit.CoreComparison();
        Assert.Equal(7, cores.Observables);
        Assert.Equal(3, cores.Actuation);
        Assert.Equal(2, cores.Intersection);
        Assert.Equal(1, cores.ActuationOnly);
        Assert.Equal(5, cores.ObservablesOnly);
        // the cores differ in BOTH directions: the coincidence is refuted, not asserted
        Assert.False(cores.TheCoresCoincide);
        Assert.False(cores.ActuationIsASubsetOfObservables);
        Assert.False(cores.ObservablesIsASubsetOfActuation);
        // what the actuation core holds outside the observable core is the operation's own RULE
        var actuationOnly = ActuatorVersusObservableAudit.FailureCoreActuation()
            .Except(ActuatorVersusObservableAudit.FailureCoreObservables(), StringComparer.Ordinal).ToArray();
        Assert.Single(actuationOnly);
        Assert.Contains("transport operator", actuationOnly[0]);
        // and the observable core's extra members are the maps, the gradient definition, the curvature and the phase
        Assert.Equal(5, ActuatorVersusObservableAudit.FailureCoreObservables()
            .Except(ActuatorVersusObservableAudit.FailureCoreActuation(), StringComparer.Ordinal).Count());

        sb.AppendLine($"  observables {cores.Observables}, actuation {cores.Actuation}, intersection {cores.Intersection},");
        sb.AppendLine($"  actuation-only {cores.ActuationOnly} (the operation's own rule), observables-only {cores.ObservablesOnly}.");
        sb.AppendLine("  The cores differ both ways. What matters is which way the ACTUATABLE members lie:");
        sb.AppendLine("  the single actuatable structure is in the observable core.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_06_EveryOperationWritesTheObservableState()
    {
        PrintHeader("G_092 - task 4: the one-sidedness, measured on the operation set itself");
        var sb = Start(
            "the failure criterion is about the direction of the difference, so the audit measures what an operation",
            "CAN write rather than what it is expected to write;",
            "on the canonical frame the class's operations span the whole conserving change space, so every operation",
            "writes the state;",
            "and the state is the object the readouts read - which is why no actuation can be observation-free;",
            "the counts are the reach table of G_090's inverse requirement, recomputed here.");
        sb.Append(ActuatorVersusObservableAudit.OutputTaskFive());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the operation set, measured: the reach IS the number of elementary operations
        Assert.Equal(95, ActuatorVersusObservableAudit.ReachableOperations());
        var reach = ActuatorVersusObservableAudit.Reach();
        Assert.Equal(3, reach.Length);
        Assert.All(reach, r => Assert.Equal(95, r.Conserving));
        // on the canonical frame the operations span every conserving direction
        Assert.Equal(0, reach.Single(r => r.Components == 1).Complement);
        // every operation writes the state, and the state is the actuatable row
        var state = ActuatorVersusObservableAudit.Structures().Single(r => r.Name == "the state (the occupation)");
        Assert.Equal(95, state.OperationsChangingIt);
        Assert.True(state.Observable);
        Assert.True(state.Actuatable);
        // and no other structure is written by any operation
        Assert.All(ActuatorVersusObservableAudit.Structures().Where(r => r.Name != "the state (the occupation)"),
            r => Assert.Equal(0, r.OperationsChangingIt));

        sb.AppendLine($"  the class has {ActuatorVersusObservableAudit.ReachableOperations()} elementary operations on the canonical frame, and");
        sb.AppendLine("  every one of them writes the state - the only structure any operation writes. The state is");
        sb.AppendLine("  observable. So an actuation that left the observables unchanged does not exist in this class.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_07_DynamicsWithoutObservablesIsNotAChannel()
    {
        PrintHeader("G_092 - task 5: structures that move the evolution and no readout");
        var sb = Start(
            "the task's search is for a structure removed by the reduction and still required for control, which is",
            "exactly a structure that is dynamical without being observable plus actuatable;",
            "the search is run over the whole inventory, and the direction of the difference is measured against the",
            "counterfactual cuts c = 1, 2, 4;",
            "a structure that can only REMOVE reach is not a channel, whatever its dynamical role.");
        sb.Append(ActuatorVersusObservableAudit.OutputTaskFive());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var dynamics = ActuatorVersusObservableAudit.DynamicalWithoutObservables();
        Assert.Equal(3, dynamics.Length);
        Assert.Contains(dynamics, d => d.Name == "the component count");
        Assert.Contains(dynamics, d => d.Name == "density topology");
        Assert.Contains(dynamics, d => d.Name.Contains("transport operator"));
        // none of them is actuatable - the success criterion's structure does not exist
        Assert.All(dynamics, d => Assert.False(d.Actuatable));
        // and the direction is restrictive: the reach can only fall
        var ceiling = ActuatorVersusObservableAudit.ReachCeiling();
        Assert.All(ceiling, r => Assert.True(r.ReachableDirections <= r.ConservingDirections));
        Assert.Equal(0, ceiling.Single(r => r.Components == 1).Unreachable);
        Assert.True(ceiling.Single(r => r.Components == 2).Unreachable > 0);
        Assert.True(ceiling.Single(r => r.Components == 4).Unreachable
            > ceiling.Single(r => r.Components == 2).Unreachable);
        // the rank is n - c, and never more
        Assert.All(ceiling, r => Assert.Equal(96 - r.Components, r.ReachableDirections));

        sb.AppendLine($"  {dynamics.Length} structures move the evolution and no readout, and none is actuatable. The reach is");
        sb.AppendLine($"  {ceiling.Single(r => r.Components == 1).ReachableDirections} of {ceiling.Single(r => r.Components == 1).ConservingDirections} on the canonical frame and");
        sb.AppendLine($"  {ceiling.Single(r => r.Components == 4).ReachableDirections} at c = 4: a dynamical structure without observables can only");
        sb.AppendLine("  TAKE control away. The direction is what refutes it as a channel.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_08_TheReductionWasObservationSafeWhereItWasControlFatal()
    {
        PrintHeader("G_092 - the substance of the failure: the operation is invisible, its effect is not");
        var sb = Start(
            "the reduction's strip list contained the transport operator, and stripping it was measurably harmless",
            "to the readouts;",
            "the same structure is the whole operation set, so no operation exists without it - which is the success",
            "criterion's letter;",
            "the criterion's content is about a settable structure, and the rule is not settable by any operation;",
            "so the audit reports both readings rather than choosing the convenient one.");
        sb.Append(ActuatorVersusObservableAudit.OutputCores());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the operation's invisibility, measured: the layer's gauge residual is exactly zero
        var layer = MinimalGravityCoreAudit.TransportLayerContribution().Single();
        Assert.Equal(0, layer.Residual);
        Assert.True(layer.Holds);
        var transport = ActuatorVersusObservableAudit.Structures()
            .Single(r => r.Name.Contains("transport operator"));
        Assert.Equal(0, transport.ReadoutsLost);
        Assert.Equal(0.0, transport.ReadoutsMoved, 12);
        Assert.False(transport.Observable);
        // its necessity for control, and its non-settability
        Assert.True(transport.Dynamical);
        Assert.True(transport.RequiredForControl);
        Assert.False(transport.Actuatable);
        // and the reduction's own reproduction test: the minimal description reads exactly like the full one
        Assert.All(MinimalGravityCoreAudit.Reproduction(), r => Assert.Equal(0.0, r.Difference, 12));

        sb.AppendLine($"  the transport operator: readouts lost {transport.ReadoutsLost}, worst readout movement");
        sb.AppendLine($"  {N(transport.ReadoutsMoved)} (the gauge residual is exactly {layer.Residual}), required for control");
        sb.AppendLine($"  {Y(transport.RequiredForControl)}, settable by an operation {Y(transport.Actuatable)}. The reduction was");
        sb.AppendLine("  observation-safe exactly where it was control-fatal - and the thing it removed is a RULE, not a knob.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_09_TheVerdictAndTheCriteria()
    {
        PrintHeader("G_092 - task 7: the criteria, and the verdict computed from the inventory");
        var sb = Start(
            "the task states two criteria, so both are decided and reported, not one;",
            "every clause of the verdict is a function of a measured count or a measured residual (G_027);",
            "the success criterion is decided by the SIZE of the invisible-actuator class, which is the only honest",
            "way to decide it;",
            "no literal reaches the verdict.");
        sb.Append(ActuatorVersusObservableAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var criteria = ActuatorVersusObservableAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.StartsWith("NOT MET IN THE LETTER", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.Equal("CONFIRMED", criteria.Single(c => c.Criterion.StartsWith("and the chain")).Outcome);

        var verdict = ActuatorVersusObservableAudit.Verdict();
        Assert.StartsWith("NO ACTUATABLE STRUCTURE IS UNOBSERVABLE", verdict);
        Assert.Contains("CONTROL NEEDS THREE AND CAN SET ONE", verdict);
        Assert.Contains("TAKE REACH AWAY", verdict);
        Assert.Contains("invisible RULE", verdict);
        // and every number in the verdict is the inventory's own
        var census = ActuatorVersusObservableAudit.RoleCensus();
        Assert.Contains(census.Total.ToString(System.Globalization.CultureInfo.InvariantCulture), verdict);

        sb.AppendLine();
        sb.AppendLine("  VERDICT: " + verdict);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_092_10_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_092 - cross-check against G_080, G_082, G_084, G_089, G_090 and G_091");
        var sb = Start(
            "the audit consumes the surviving results: connectivity assumed, the count gauge-like, the reduced core,",
            "the single actuatable channel, the collapse of every pump and the curvature as a parameter;",
            "each cross-check is a computed identity between this audit's quantities and the owning audit's own API;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(ConnectivityOriginAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // G_080: connectivity is assumed, and a read that needs a link is a read a cut silences
        Assert.False(ConnectivityOriginAudit.PrimitivesSeparateConnectivity());
        var reach = ConnectivityOriginAudit.AccelerationReach();
        Assert.Equal(0, reach.Single(r => r.Cells == 2 && r.Components == 2).CellsReadingAnAcceleration);
        Assert.True(reach.Single(r => r.Substrate.Contains("canonical")).CellsReadingAnAcceleration > 0);
        // G_082: no local observable reads the absolute count
        Assert.Equal(0, ComponentCountAudit.ObservablesReadingAbsoluteC());
        // G_084: the minimal description reproduces every readout
        Assert.Equal(8, MinimalGravityCoreAudit.Reproduction().Length);
        Assert.All(MinimalGravityCoreAudit.Reproduction(), r => Assert.True(r.ProducedInBoth));
        // G_089: one actuatable channel, and the unobservable controls are none of them actuatable
        Assert.Single(ManipulationChannelAudit.Channels()
            .Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.Actuatable));
        Assert.Equal(0, ManipulationChannelAudit.ControlVersusObservableCounts().UnobservableAndActuatableControls);
        // G_090: the complements at the cuts, which this audit's reach ceiling reproduces
        Assert.All(DirectDensityControlAudit.ConservationAudit(), r => Assert.Equal(r.Components - 1, r.LocalCreationChanges));
        // G_091: the curvature's coupling is exactly zero
        Assert.Equal(0.0, KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength, 12);
        Assert.StartsWith("KERNEL CURVATURE IS A PARAMETER", KernelCurvatureActuationAudit.Verdict());
        // and the one row where read and decidable come apart is the curvature, alone
        var rows = ActuatorVersusObservableAudit.Structures();
        var undecidable = rows.Where(r => r.Observable && !r.TheReadIsDecidableInSitu).ToArray();
        Assert.Single(undecidable);
        Assert.Contains("curvature", undecidable[0].Name);

        sb.AppendLine("  the chain's own verdicts are reproduced: connectivity assumed and a cut silences the acceleration");
        sb.AppendLine("  read, the count read by no local observable, the minimal core reproducing all 8 readouts, one");
        sb.AppendLine("  actuatable channel with no unobservable actuatable control, the complements c - 1 at the cuts, and");
        sb.AppendLine("  the curvature's coupling exactly zero. What is added is the DYNAMICAL axis and the attribution of");
        sb.AppendLine("  each measurement to the structure that owns it.");
        Output.WriteLine(sb.ToString());
    }
}
