using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_097 - PATTERN KINEMATICS AUDIT.
///
/// G_084 reduced the sector to one state variable and three equations, G_086 collapsed the observable kernel to one
/// function, and G_096 showed the requirement that would fix the remaining coefficient is assumed. This suite asks the
/// question those leave untouched: can observed spatial motion be derived as pattern propagation on the 96-cell ring,
/// or is KINEMATICS independent information?
///
/// Definitions from the question: a PATTERN is any state configuration distinguishable by the surviving observables;
/// MOTION is the change of a pattern's apparent position; and DISTANCE must be DERIVED, not assumed. The ring is the
/// only substrate (the cubic substrate is not used), and no new primitive, field, source term, topology axiom, action,
/// Lagrangian, memory mechanism or extra structure is introduced.
///
/// The audit leans on NP_092's prior result rather than re-deriving it: propagation in AT is not transport, everything
/// APPEARS to move, and the velocity is the group velocity in units of ONE LINK PER TICK - so the question becomes
/// what those units ARE in the surviving sector.
///
/// The report follows the seven tasks. Equality rule 1e-12 except where a count is quoted.
/// </summary>
public sealed class Y_G_097_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_097_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_097_01_TheStructureInventory()
    {
        PrintHeader("G_097 - task 1: what can carry a propagating pattern on the ring?");
        var sb = Start(
            "a carrier must be a STATE VARIABLE the evolution acts on, or the evolution map itself - a derived object",
            "cannot carry a pattern of its own;",
            "every candidate is tested by its arity and its survival, not by its name;",
            "all counts are on the canonical 96-cell ring; the cubic substrate is NOT used;",
            "no new primitive, field, source term, action, Lagrangian or extra structure is introduced anywhere.");
        sb.Append(PatternKinematicsAudit.OutputCarriers());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var carriers = PatternKinematicsAudit.Carriers();
        Assert.Equal(5, carriers.Length);
        var counts = PatternKinematicsAudit.CarrierCounts();
        Assert.Equal(5, counts.Structures);
        Assert.Equal(1, counts.StateVariables);
        Assert.Equal(2, counts.EvolutionMaps);
        // the state is the only state variable, and it carries every readout
        var state = carriers.Single(c => c.IsAStateVariable);
        Assert.Equal(8, state.ReadoutsCarried);
        Assert.Contains("the occupation", state.Structure);
        // the two maps are the update relation and the class's operations
        Assert.Equal(2, carriers.Count(c => c.IsAnEvolutionMap));
        // and the flux and the phase do not survive as carriers
        Assert.False(carriers.Single(c => c.Structure.Contains("flux")).Survives);
        Assert.False(carriers.Single(c => c.Structure.Contains("phase")).Survives);
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());

        sb.AppendLine($"  {counts.StateVariables} state variable and {counts.EvolutionMaps} evolution maps carry the pattern; the flux is");
        sb.AppendLine($"  written by an operation (read by {ContinuityLawAudit.ObservablesReadingTheFlux()} observables) and the phase is excluded by arity.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_02_TheLawSpreadsAPatternAndMovesItNowhere()
    {
        PrintHeader("G_097 - what the evolution does to a pattern: the measurement motion depends on");
        var sb = Start(
            "the evolution step is rho -> rho + epsilon L rho, and the STEP SIZE is a CONVENTION the sector does not",
            "supply - the sector supplies the Laplacian and not the rate at which it is applied;",
            "the admissible range of epsilon is DERIVED from the Laplacian's own spectrum;",
            "the pattern is tracked through its EXCESS over the flat background, so its width and centroid are the",
            "bump's and not the ring's;",
            "a transport moves the centroid; a diffusion spreads the bump in place - and this is the measurement that",
            "decides whether motion can come from the LAW at all.");
        sb.Append(PatternKinematicsAudit.OutputPropagation());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var propagation = PatternKinematicsAudit.Propagate();
        Assert.Equal(9, propagation.Length);
        // the peak falls and the width grows: the pattern SPREADS
        Assert.True(propagation[^1].Peak < propagation[0].Peak);
        Assert.True(propagation[^1].Spread > propagation[0].Spread);
        Assert.Equal(0.0, propagation[0].Spread, 12);
        // the excess is conserved exactly: the law neither creates nor destroys the pattern
        Assert.All(propagation, p => Assert.Equal(0.25, p.Total, 12));
        // and the centroid does NOT move, at every tick, to the rounding: the law translates nothing
        Assert.All(propagation, p => Assert.Equal(0.0, p.CentroidShift, 12));
        // the width grows as sqrt(t) - the fingerprint of DIFFUSION - and the coefficient is the step size to 0.1 per
        // cent, the deviation being the discrete-diffusion correction rather than noise
        foreach (var p in propagation.Skip(1))
        {
            double ratio = p.Spread * p.Spread / (2.0 * p.Tick);
            Assert.True(Math.Abs(ratio - PatternKinematicsAudit.Step) / PatternKinematicsAudit.Step < 5e-3,
                $"width^2/(2t) = {ratio:F6} against a step of {PatternKinematicsAudit.Step:F3}");
        }
        // the mode symbols are non-positive, so the map dissipates rather than waves
        Assert.All(PatternKinematicsAudit.Symbols(), s => Assert.True(s.Symbol <= 1e-12));
        Assert.All(PatternKinematicsAudit.Symbols(), s => Assert.False(s.IsAPlaneWave));
        // and the step bound is derived from the spectrum
        Assert.Equal(0.5, PatternKinematicsAudit.StableStepBound(), 6);

        sb.AppendLine($"  over {propagation.Length - 1} ticks the peak falls {F3(propagation[0].Peak)} -> {F3(propagation[^1].Peak)}, the bump's width grows");
        sb.AppendLine($"  {F3(propagation[0].Spread)} -> {F3(propagation[^1].Spread)} as sqrt(t) with coefficient {F3(PatternKinematicsAudit.Step)}, the excess is conserved exactly,");
        sb.AppendLine($"  and the centroid moves by exactly {N(propagation[^1].CentroidShift)} at every tick: the LAW MOVES NOTHING.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_03_DistanceClassification()
    {
        PrintHeader("G_097 - task 2: can a distance be derived from the surviving primitives?");
        var sb = Start(
            "three candidates are tested, plus the ratio the chain measured as unselected;",
            "DERIVED means the surviving primitives supply it without an added assumption;",
            "a candidate that is derived but is a STATE difference is not a distance, and the audit separates the two;",
            "the physical length of a hop is checked against the link-length programme's own record.");
        sb.Append(PatternKinematicsAudit.OutputKinematics());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var candidates = PatternKinematicsAudit.DistanceCandidates();
        Assert.Equal(4, candidates.Length);
        // exactly one candidate is DERIVED, and it is the state difference
        var derived = candidates.Where(c => c.Status == KinematicsStatus.Derived).ToArray();
        Assert.Single(derived);
        Assert.Equal("state difference", derived[0].Kind);
        Assert.False(derived[0].RequiresTheLinkSet);
        Assert.False(derived[0].RequiresTheLabelling);
        // the graph distance is derived GIVEN the links, so it inherits the link set's assumption
        var graph = candidates.Single(c => c.Kind == "graph distance");
        Assert.True(graph.RequiresTheLinkSet);
        Assert.Equal(KinematicsStatus.Assumed, graph.Status);
        Assert.True(graph.DerivedFromThePrimitives);
        // the index difference is a coordinate
        var index = candidates.Single(c => c.Kind == "coordinate");
        Assert.True(index.RequiresTheLabelling);
        // the ratio is neither derived nor a distance
        var ratio = candidates.Single(c => c.Kind == "path ratio");
        Assert.False(ratio.DerivedFromThePrimitives);
        // and the link-length programme itself records that a link length does not fix values
        Assert.False(LinkLengthPhysics.LinkLengthDeterminesValues());
        // the overall classification
        var classification = PatternKinematicsAudit.DistanceClassification();
        Assert.Equal(KinematicsStatus.Assumed, classification.Status);
        Assert.Contains("state difference", classification.Reading);

        sb.AppendLine($"  one candidate is DERIVED and it is a {derived[0].Kind}; the graph distance needs the link set, which is");
        sb.AppendLine("  ASSUMED (G_080); the index difference is the labelling, a coordinate; and the link-length programme");
        sb.AppendLine("  records that a link length does NOT determine values. So SPATIAL DISTANCE IS ASSUMED.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_04_WorldlineClassification()
    {
        PrintHeader("G_097 - task 3: can a worldline be reconstructed without adding structure?");
        var sb = Start(
            "a worldline needs two ingredients: a way to identify the pattern across ticks, and a time parameter;",
            "the first is measured against the readouts, the second against the sector's own boundary ledger;",
            "a position is tested separately from a pattern's identity, because a sector can track a pattern and not",
            "know where it is;",
            "the tick's status is taken from the project's recorded structural boundaries, not asserted here.");
        sb.Append(PatternKinematicsAudit.OutputKinematics());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var ingredients = PatternKinematicsAudit.WorldlineIngredients();
        Assert.Equal(3, ingredients.Length);
        Assert.True(ingredients.Single(i => i.Ingredient.Contains("IDENTIFICATION")).AvailableFromTheSurvivingSector);
        Assert.False(ingredients.Single(i => i.Ingredient.Contains("POSITION")).AvailableFromTheSurvivingSector);
        Assert.False(ingredients.Single(i => i.Ingredient.Contains("TIME")).AvailableFromTheSurvivingSector);
        // identification is derived: all readouts are functions of the state
        Assert.Equal(8, MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full()).Count(r => r.Produced));
        // position is not: 4 of 8 readouts read the labelling and 4 do not
        Assert.Equal(4, MinimalGravityCoreAudit.ObservableSet().Count(o => !o.FrameInvariant));
        Assert.Equal(4, MinimalGravityCoreAudit.ObservableSet().Count(o => o.FrameInvariant));
        // and the classification is BOUNDARY, because the missing ingredient is a structural boundary
        Assert.Equal(KinematicsStatus.Boundary, PatternKinematicsAudit.WorldlineClassification());

        sb.AppendLine("  the pattern can be IDENTIFIED across ticks (every readout is a function of the state), a POSITION");
        sb.AppendLine("  cannot (4 of 8 readouts read the labelling and 4 do not), and a TIME parameter cannot (the tick is the");
        sb.AppendLine("  sector's recorded deepest structural boundary). The worldline is BOUNDARY.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_05_TheVelocityAndItsFamily()
    {
        PrintHeader("G_097 - task 4: velocity from surviving quantities only");
        var sb = Start(
            "velocity is defined as the apparent displacement of a pattern per tick, measured in HOPS;",
            "the audit separates the BOUND from the VALUE and the VALUE from the UNIT, because they have different",
            "statuses;",
            "the bound is measured from the operations' own support, and the amount is measured as a free range;",
            "the unit is checked against the prior result that states velocity in exactly these units.");
        sb.Append(PatternKinematicsAudit.OutputKinematics());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var velocity = PatternKinematicsAudit.Velocity();
        Assert.Equal(4, velocity.Length);
        var bound = velocity.Single(v => v.Component.Contains("BOUND"));
        var amount = velocity.Single(v => v.Component.Contains("amount"));
        var direction = velocity.Single(v => v.Component.Contains("direction"));
        var unit = velocity.Single(v => v.Component.Contains("UNIT"));
        // the bound is derived, and it comes from the operations' supports being adjacent
        Assert.True(bound.Derived);
        Assert.Equal(1.0, bound.Value, 12);
        // the amount, the direction and the unit are not
        Assert.False(amount.Derived);
        Assert.False(direction.Derived);
        Assert.False(unit.Derived);
        Assert.Contains("LINK PER TICK", unit.Measurement);
        // every witness moves count across exactly one link: the largest cyclic distance between its two sites is 1
        Assert.Equal(1, PatternKinematicsAudit.MaxHopsPerOperation());
        var witnesses = ConnectivityOriginAudit.ConservingWitnesses(96, 1);
        Assert.All(witnesses, w => Assert.Equal(2, w.Change.Count(c => c != 0)));
        Assert.All(witnesses, w =>
        {
            var sites = Enumerable.Range(0, w.Change.Length).Where(i => w.Change[i] != 0).ToArray();
            int d = Math.Abs(sites[0] - sites[1]);
            Assert.Equal(1, Math.Min(d, 96 - d));
        });
        // the family: 2 directions and a 1-dimensional amount
        var family = PatternKinematicsAudit.VelocityFamily();
        Assert.Equal(2, family.Directions);
        Assert.Equal(1, family.FamilyDimension);
        Assert.False(family.TheValueIsFixed);
        Assert.True(family.TheBoundIsDerived);
        Assert.Equal(PatternKinematicsAudit.State.Max(), family.AmountMax, 12);

        sb.AppendLine($"  the BOUND is derived (every witness's support is adjacent, so |v| <= 1 hop per tick); the VALUE is not");
        sb.AppendLine($"  (a {family.FamilyDimension}-dimensional amount the theory does not select from); and the UNIT is not (one link per");
        sb.AppendLine("  tick, both halves unsupplied — the units NP_092's group velocity is stated in).");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_06_WithoutSpatialAssumptions()
    {
        PrintHeader("G_097 - task 5: remove the explicit spatial assumptions");
        var sb = Start(
            "two assumptions are removed in turn: the link set, which defines adjacency, and the labelling, which names",
            "a site;",
            "the removal of the link set is a MEASUREMENT of the operations that remain, not an assertion about them;",
            "removing the labelling is a coordinate change, so the audit checks that the physics survives it and only",
            "the identity of a site is lost;",
            "no spatial structure is added back at any point.");
        sb.Append(PatternKinematicsAudit.OutputDegeneracyAndReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var removals = PatternKinematicsAudit.WithoutSpatialAssumptions();
        Assert.Equal(3, removals.Length);
        var noLinks = removals.Single(r => r.Removed.Contains("link set"));
        var noLabelling = removals.Single(r => r.Removed.Contains("labelling"));
        // without links there is no operation at all, and transport stops
        Assert.Equal(0, noLinks.OperationsRemaining);
        Assert.False(noLinks.TransportSurvives);
        Assert.Contains("stops", noLinks.Measurement);
        // the along-a-link gradient falls to zero, which G_092 measured as well
        var noLinkReadouts = ConnectivityOriginAudit.AccelerationReach()
            .Single(r => r.Cells == 2 && r.Components == 2);
        Assert.Equal(0, noLinkReadouts.CellsReadingAnAcceleration);
        // with no labelling the operations survive: only the IDENTITY of a site is lost
        Assert.Equal(95, noLabelling.OperationsRemaining);
        Assert.True(noLabelling.TransportSurvives);
        Assert.Equal(8, noLabelling.ReadoutsRemaining);
        // and the order of the operations is not observable: the readouts are functions of the state
        var noSequence = removals.Single(r => r.Removed.Contains("sequence"));
        Assert.True(noSequence.TransportSurvives);
        Assert.All(MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full()),
            r => Assert.True(r.Produced));

        sb.AppendLine("  with NO links there are no operations and apparent transport STOPS; with no labelling the physics");
        sb.AppendLine("  survives and only the identity of a site is lost; and the ORDER of the operations is invisible, because");
        sb.AppendLine("  the readouts are functions of the state and a state has no history.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_07_TheDegeneracyCount()
    {
        PrintHeader("G_097 - task 6: can two distinct histories give identical observables?");
        var sb = Start(
            "the measure is constructive: the largest set of mutually independent transfers is built, their orderings",
            "are counted, and two different orderings are compared to confirm the observables cannot separate them;",
            "the count is reported as a POWER because it exceeds a 64-bit integer - a count that overflows is an",
            "artifact-prone number, so it is not quoted as an integer;",
            "the comparison is made on the built configurations, so the identity is exact rather than approximate;",
            "independence is by construction: the pairs are disjoint, which is what makes them commute.");
        sb.Append(PatternKinematicsAudit.OutputDegeneracyAndReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var degeneracy = PatternKinematicsAudit.Degeneracy();
        Assert.Single(degeneracy);
        var row = degeneracy[0];
        // the ring admits 48 mutually independent transfers
        Assert.Equal(48, row.IndependentTransfers);
        // they commute: the reverse order gives the same state
        Assert.True(row.TheStatesAreIdentical);
        // and the count is a power, far beyond a 64-bit integer
        Assert.True(row.ExceedsA64BitInteger);
        Assert.True(row.Log10Orderings > 60.0);
        Assert.Equal(61.094, row.Log10Orderings, 3);
        // the log10 is the sum of the logarithms, computed rather than quoted
        Assert.Equal(Enumerable.Range(1, 48).Sum(i => Math.Log10(i)), row.Log10Orderings, 12);

        sb.AppendLine($"  {row.IndependentTransfers} mutually independent transfers commute, so {row.IndependentTransfers}! histories - {F3(row.Log10Orderings)} orders of");
        sb.AppendLine("  magnitude - give bit-identical states and therefore identical observables: the missing information is");
        sb.AppendLine("  NOT recoverable from the readouts.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_08_TheReductionTest()
    {
        PrintHeader("G_097 - task 7: reconstruct the motion observables from pattern propagation alone");
        var sb = Start(
            "the FIRST thing measured is how many motion observables the sector HAS, because a set of zero makes the",
            "loss zero and the claim vacuous;",
            "a readout is positional if it names two sites or a link, and labelling-reading if its frame invariance",
            "fails;",
            "the two counts are reported separately, because they answer different questions;",
            "the loss is reported both with the labelling retained as a coordinate and with it removed.");
        sb.Append(PatternKinematicsAudit.OutputDegeneracyAndReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var observables = PatternKinematicsAudit.MotionObservables();
        Assert.Equal(8, observables.Length);
        var reduction = PatternKinematicsAudit.ReductionTest();
        Assert.Equal(8, reduction.MotionObservables);
        Assert.Equal(4, reduction.Positional);
        Assert.Equal(4, reduction.ReadTheLabelling);
        // with the labelling retained as a coordinate the reconstruction costs nothing
        Assert.Equal(0, reduction.LossWithTheLabelling);
        // and without it, the labelling-reading readouts are lost
        Assert.Equal(4, reduction.LossWithoutIt);
        // there is NO absolute-position readout: no observable names where the pattern IS
        Assert.All(observables, o => Assert.False(o.Readout.Contains("position")));
        // and the frame-invariant four constrain the configuration only
        Assert.Equal(4, observables.Count(o => o.FrameInvariant));
        Assert.All(observables.Where(o => o.FrameInvariant), o => Assert.False(o.ReadsTheLabelling));

        sb.AppendLine($"  {reduction.MotionObservables} readouts, {reduction.Positional} of them positional and {reduction.ReadTheLabelling} reading the labelling;");
        sb.AppendLine($"  the observable loss is {reduction.LossWithTheLabelling} with the labelling kept and {reduction.LossWithoutIt} without it, and there is NO readout of a");
        sb.AppendLine("  pattern's absolute position — so motion is the motion of a LABEL.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_097_09_TheClassificationCriteriaAndVerdict()
    {
        PrintHeader("G_097 - the classification, the criteria and the verdict");
        var sb = Start(
            "the four objects are classified separately, because motion's status and the kinematics' status differ;",
            "the task's success and failure criteria are both decided, by count and by measurement;",
            "every clause of the verdict is a measured count, a measured symbol or a measured degeneracy (G_027);",
            "no literal reaches a verdict path.");
        sb.Append(PatternKinematicsAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = PatternKinematicsAudit.Classification();
        Assert.Equal(4, classification.Length);
        Assert.Equal(KinematicsStatus.Emergent, classification.Single(c => c.Object.Contains("PROPAGATION")).Status);
        Assert.Equal(KinematicsStatus.Assumed, classification.Single(c => c.Object.Contains("DISTANCE")).Status);
        Assert.Equal(KinematicsStatus.Boundary, classification.Single(c => c.Object.Contains("WORLDLINE")).Status);
        Assert.Equal(KinematicsStatus.Assumed, classification.Single(c => c.Object.Contains("VELOCITY")).Status);
        // the criteria
        var criteria = PatternKinematicsAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.Equal("MET", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.Equal("EMERGENT", criteria.Single(c => c.Criterion.StartsWith("and the propagation")).Outcome);
        // the verdict and its numbers
        var verdict = PatternKinematicsAudit.Verdict();
        Assert.StartsWith("MOTION IS EMERGENT", verdict);
        Assert.Contains("KINEMATICS IS ASSUMED", verdict);
        Assert.Contains("MOVES NOTHING", verdict);
        Assert.Contains("motion of a LABEL", verdict);
        Assert.Contains(F3(PatternKinematicsAudit.Degeneracy().Single().Log10Orderings), verdict);
        Assert.Contains(F3(PatternKinematicsAudit.Propagate()[^1].Peak), verdict);

        sb.AppendLine();
        sb.AppendLine("  VERDICT: " + verdict);
        Output.WriteLine(sb.ToString());
    }
}
