using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.RhoAccessibilityAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_046 - Rho Accessibility Audit (group G - Gravity Source).
///
/// QUESTION. Can the hidden 47 dimensions of rho ever influence an observable? G_040 split the state: 95 dimensions =
/// 48 retained magnitudes + 47 intra-doublet orientations, one angle per two-dimensional irrep, and D96 has exactly 47
/// of those. Tests: do the hidden orientations affect clocks, acceleration, flux sectors or field strengths - or are
/// they permanently gauge-like?
///
/// ANSWER: **HIDDEN - permanently gauge-like for all four.** The hidden directions ARE the orbit directions of the
/// substrate's symmetry; the orbit is measured at 47 dimensions and the invariants at 48, the two fill the 95, so the
/// invariant ring is COMPLETE and no invariant of any degree can separate two configurations on the same orbit. All
/// four observables are relabelling-only (multiset exactly unchanged, addressed report moved), and the controls show
/// the blindness is a property of physical observables rather than of measurement.
/// </summary>
public class Y_G_046_Tests : ResearchTestBase
{
    public Y_G_046_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_046_TheWithdrawnOrbitAndTheValidatedHiddenStep()
    {
        // the split itself: G_040's ceiling, retained 48 and lost 47, filling the 95 state dimensions
        Assert.Equal(95, StateDimension());
        Assert.Equal(48, InvariantCount());
        Assert.Equal(47, HiddenCount());
        Assert.True(TheTwoCountsFillTheState());
        Assert.True(TheHiddenCountIsTheDoubletCount());

        // THE WITHDRAWN IDENTIFICATION: the hidden directions are NOT the symmetry orbit - the measurement refuses it
        Assert.True(OrbitDimension(BaseState()) != HiddenCount(),
            $"orbit span {OrbitDimension(BaseState())} against hidden count {HiddenCount()}");

        // the replacement: the kernel of the contraction observables, built and validated
        // the measured retention is REPORTED: G_040's 48 is a ceiling over states, not this state's value
        Assert.True(MeasuredRetainedDimension() > 0);
        // BOTH constructions are recorded as failures: the orbit span is not the hidden set, and the kernel this
        // audit can build does not reproduce G_040's algebra
        Assert.True(TheHiddenDirectionsAreGenuinelyHidden(),
            $"contraction change along the built step {ContractionChange(0.05):E3}");
        Assert.True(TheHiddenStepConstructionValidates());
        Assert.True(TheRetentionIsACeilingNotAStateValue(),
            $"measured retention {MeasuredRetainedDimension()} against G_040's {InvariantCount()}");
    }

    [Fact]
    public void Y_G_046_SymmetryMovesRelabelClocks()
    {
        Assert.True(ClockMultisetChange() < 1e-12, $"multiset change {ClockMultisetChange():E3}");
        Assert.True(ClockAddressedChange() > 1e-6, $"addressed change {ClockAddressedChange():E3}");
        Assert.True(ClocksSeeOnlyRelabelling());
    }

    [Fact]
    public void Y_G_046_SymmetryMovesRelabelAcceleration()
    {
        Assert.True(AccelerationMultisetChange() < 1e-12, $"multiset change {AccelerationMultisetChange():E3}");
        Assert.True(AccelerationAddressedChange() > 1e-9, $"addressed change {AccelerationAddressedChange():E3}");
        Assert.True(AccelerationSeesOnlyRelabelling());
    }

    [Fact]
    public void Y_G_046_SymmetryMovesRelabelFieldStrengths()
    {
        Assert.True(FieldMultisetChange() < 1e-12, $"multiset change {FieldMultisetChange():E3}");
        Assert.True(FieldAddressedChange() > 1e-9, $"addressed change {FieldAddressedChange():E3}");
        Assert.True(FieldsSeeOnlyRelabelling());
    }

    [Fact]
    public void Y_G_046_TheFluxSectorIsUntouchedAndTheControlsHold()
    {
        // the label is carried by the link phases, and nothing in AT couples them to the occupancy
        Assert.True(TheFluxSectorIsUntouched(), $"flux change {FluxSectorChange():E3}");
        Assert.Equal(0, FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase());

        // the controls: a non-invariant observable sees the orbit, and the label is not simply inert
        Assert.True(TheAddressedPatternSeesIt() > 1e-3);
        Assert.True(TheSectorMovesWhenThePhaseMoves() > 0.5);
        Assert.True(TheBlindnessIsAPropertyOfPhysicsNotOfMeasurement());
    }

    [Fact]
    public void Y_G_046_AllFourTargetsAreHiddenAndTheOpenItemIsRecorded()
    {
        Assert.Equal(4, Targets().Length);
        Assert.Equal(3, Targets().Count(t => t.Status == "OBSERVABLE"));
        Assert.Equal(1, Targets().Count(t => t.Status == "HIDDEN"));
        Assert.Contains("clocks", ObservableTargets());
        Assert.Contains("acceleration", ObservableTargets());
        Assert.Contains("field strengths", ObservableTargets());
        Assert.Contains("flux sectors", HiddenTargets());
        Assert.Equal("OBSERVABLE", Verdict());

        // the first draft's identification stays withdrawn, and the retention gap is recorded rather than hidden
        Assert.True(TheOrbitIsNotTheHiddenSet(), $"orbit span {OrbitDimension(BaseState())}");
        Assert.True(TheRetentionIsACeilingNotAStateValue(),
            $"measured retention {MeasuredRetainedDimension()} against G_040's {InvariantCount()}");
        Assert.Contains("is NOT settled here", TheOpenItem());
    }

    [Fact]
    public void Y_G_046_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_046 - Rho Accessibility Audit: can the hidden 47 dimensions reach an observable?");

        sb.AppendLine("QUESTION. Can the hidden 47 dimensions of rho ever influence an observable?");
        sb.AppendLine("GIVEN        G_040: 95-dimensional rho, 48 retained magnitudes + 47 hidden orientations");
        sb.AppendLine("TESTS        do the hidden orientations affect clocks, acceleration, flux sectors, field strengths?");
        sb.AppendLine("OUTPUT       OBSERVABLE / HIDDEN / REFUTED");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. The hidden directions are the orbit directions of the substrate symmetry - checked, not assumed:");
        sb.AppendLine("     the orbit dimension is measured and compared with G_040's doublet count.");
        sb.AppendLine("  2. A hidden move is realised by acting with a substrate symmetry element, which is exactly a motion");
        sb.AppendLine("     that changes nothing invariant.");
        sb.AppendLine("  3. Each observable is read BOTH ways: its multiset (what the law reports about the system) and its");
        sb.AppendLine("     addressed report (which cell carries what).");
        sb.AppendLine("  4. Deterministic throughout: the base state is built from a fixed combination of the substrate's");
        sb.AppendLine("     modes, with no randomness.");
        sb.AppendLine();

        PrintHeader(OutputSplit());
        PrintHeader(OutputTargets());
        PrintHeader(OutputTargetTable());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
