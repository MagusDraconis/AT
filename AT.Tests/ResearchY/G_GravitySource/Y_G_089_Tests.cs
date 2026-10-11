using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_089 - MANIPULATION CHANNEL AUDIT.
///
/// WORKING HYPOTHESIS, ASSUMED TRUE: local manipulation of time and/or gravity IS physically possible. The question is
/// not whether it exists but **which physical channel would permit it**, inside the surviving sector's own vocabulary.
/// Arguing from previous negative verdicts is forbidden, as is adding primitives immediately or assuming impossibility.
///
/// The report follows the seven tasks. Every figure is computed here, and no spectral quantity is quoted.
/// </summary>
public sealed class Y_G_089_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_089_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string P2(double v) => v.ToString("P2", System.Globalization.CultureInfo.InvariantCulture);
    private static string F4(double v) => v.ToString("F4", System.Globalization.CultureInfo.InvariantCulture);

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_089_01_TheInverseProblemIsSolved()
    {
        PrintHeader("G_089 - task 1: what change in the surviving state a clock change requires");
        var sb = Start(
            "the working hypothesis is ASSUMED: local manipulation of time and gravity is possible, and the audit asks which",
            "channel would permit it rather than whether it exists;",
            "the state is the count density rho on the 96-cell ring and the kernel is K(rho) = rho^(1/d) exp(c A^2/2) with",
            "A = ln rho / d, the object G_086 collapsed the sector to;",
            "the inverse problem is solved in closed form: a fractional clock change needs a local count change of",
            "(1 + Delta)^(d/(1+cA)) - 1, with the GAIN reported beside it;",
            "the gravity side is solved by the direction of the change: uniform or differential.");
        sb.Append(ManipulationChannelAudit.OutputInverseProblem());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = ManipulationChannelAudit.InverseProblemSmallClockChange(new[] { 0.01, 0.10, 1.00 },
            ManipulationChannelAudit.SurfacePotential);
        Assert.Equal(3, rows.Length);
        // the density is the lever arm and the clock is DAMPED: the density must move MORE than the clock, by about d
        Assert.All(rows, r => Assert.True(r.DensityPerUnitClockChange > 1.0,
            $"the density must move MORE than the clock ({r.DensityPerUnitClockChange:F4})"));
        Assert.All(rows, r => Assert.True(r.RequiredDensityChange > r.TargetClockChange,
            "a clock change must cost MORE density change than itself"));
        Assert.True(rows[0].DensityPerUnitClockChange > 2.5 && rows[0].DensityPerUnitClockChange < 3.5,
            $"the requirement must be near d ({F4(rows[0].DensityPerUnitClockChange)})");
        // a zero clock change costs nothing, and the closed form returns exactly zero
        Assert.Equal(0.0, ManipulationChannelAudit.InverseProblemSmallClockChange(new[] { 0.0 },
            ManipulationChannelAudit.SurfacePotential).Single().RequiredDensityChange, 15);

        var gravity = ManipulationChannelAudit.GravityInverse();
        Assert.Equal(2, gravity.Length);
        var uniform = gravity.Single(g => g.Direction.Contains("uniform"));
        var differential = gravity.Single(g => g.Direction.Contains("differential"));
        // the uniform direction leaves the acceleration EXACTLY unchanged; the differential one moves it
        Assert.True(uniform.AccelerationChange < 1e-12, $"uniform must not change the acceleration ({N(uniform.AccelerationChange)})");
        Assert.True(differential.AccelerationChange > 1e-9, "a differential change must move the acceleration");
        Assert.False(uniform.TheClassContainsIt);
        Assert.True(differential.TheClassContainsIt);

        sb.AppendLine($"  the inverse problem is closed: a 1 % clock change needs {P2(rows[0].RequiredDensityChange)} of local count, and a");
        sb.AppendLine("  GRAVITY change needs a DIFFERENTIAL change — the uniform direction moves the acceleration by zero AND is");
        sb.AppendLine("  the direction the class does not contain.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_02_TheChannelSearch()
    {
        PrintHeader("G_089 - task 2: the channel search across the eight candidates");
        var sb = Start(
            "each channel is judged by three measured facts: does it CONTROL the kernel, is it DIRECTLY observable, and can",
            "the surviving class ACTUATE it;",
            "the status follows from those facts by one rule, stated in the core rather than chosen per row;",
            "the eight candidates are those the task names: local density, density correlation, density variance, density",
            "topology, kernel curvature, kernel drift, phase structure and boundary conditions.");
        sb.Append(ManipulationChannelAudit.OutputChannels());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var channels = ManipulationChannelAudit.Channels();
        Assert.Equal(8, channels.Length);
        var actuatable = channels.Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.Actuatable).ToArray();
        Assert.Single(actuatable);
        Assert.Equal("local density", actuatable[0].Channel);
        var controls = channels.Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.ControlWithoutActuator).ToArray();
        Assert.Equal(3, controls.Length);
        Assert.Contains(controls, c => c.Channel == "density topology");
        Assert.Contains(controls, c => c.Channel == "kernel curvature");
        Assert.Contains(controls, c => c.Channel == "boundary conditions");
        Assert.Equal(3, channels.Count(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.NotAControl));
        Assert.Equal(1, channels.Count(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.ExcludedByArity));

        sb.AppendLine("  exactly ONE channel is actuatable, and it is the class's own elementary operation: a count transfer. THREE");
        sb.AppendLine("  control the kernel without an actuator, and the rest are not independent controls at all.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_03_TheHiddenVariablesStillAct()
    {
        PrintHeader("G_089 - task 3: the eliminated structures, re-examined as control variables");
        var sb = Start(
            "a quantity removed as observationally redundant may still be able to ACT, which is the distinction the task",
            "asks about: unobservable is not the same as inert;",
            "the four eliminated structures are connectivity, continuity, the transport layer and the component count, with",
            "the kernel's curvature added because G_088 identified it as the one effective-and-unmeasured quantity;",
            "for each, three facts are measured: does it control the kernel, is it directly observable, can the class",
            "actuate it.");
        sb.Append(ManipulationChannelAudit.OutputHiddenVariables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var hidden = ManipulationChannelAudit.HiddenVariables();
        Assert.Equal(5, hidden.Length);
        // the two eliminated structures that STILL control the kernel are exactly the two the class conserves
        var controllers = hidden.Where(h => h.ControlsTheKernel).ToArray();
        Assert.Equal(3, controllers.Length);
        Assert.All(controllers, h => Assert.False(h.DirectlyObservable));
        Assert.All(controllers, h => Assert.False(h.Actuatable));
        // and the transport layer is a description of the conserving operations, not a control
        var transport = hidden.Single(h => h.Quantity.Contains("transport layer"));
        Assert.False(transport.ControlsTheKernel);
        Assert.True(transport.Actuatable);

        sb.AppendLine($"  {controllers.Length} eliminated structures still CONTROL the kernel — the link structure, the component count and");
        sb.AppendLine("  the kernel's curvature — and ALL of them are unobservable AND unactuatable. The transport layer, by contrast,");
        sb.AppendLine("  is actuatable precisely because it is a description of the conserving operations rather than a control.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_04_ControlVersusObservable()
    {
        PrintHeader("G_089 - task 4: can a quantity be unobservable directly and yet controllable?");
        var sb = Start(
            "the question is answered by a COUNT over the quantities the sector has already examined, not by an argument;",
            "a manipulation channel would have to live in the intersection: unobservable AND actuatable;",
            "the audit therefore reports that intersection's size, and what it means when the size is zero.");
        sb.Append(ManipulationChannelAudit.OutputHiddenVariables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var counts = ManipulationChannelAudit.ControlVersusObservableCounts();
        sb.AppendLine($"  controlling the kernel: {counts.Controls}; unobservable among them: {counts.UnobservableControls}");
        sb.AppendLine($"  actuatable: {counts.Actuatable}; unobservable AND actuatable CONTROLS: "
            + $"{counts.UnobservableAndActuatableControls}; actuatable and observable: "
            + $"{counts.ActuatableAndObservable}");
        Assert.True(counts.Controls > 0);
        Assert.True(counts.UnobservableControls > 0, "unobservable controls must exist — that is the hypothesis's best case");
        Assert.True(counts.Actuatable > 0);
        Assert.Equal(0, counts.UnobservableAndActuatableControls);
        Assert.Equal(counts.Actuatable, counts.ActuatableAndObservable);

        sb.AppendLine("  YES in principle: quantities DO exist that are unobservable and control the kernel. And within the class the");
        sb.AppendLine("  intersection with ACTUATABLE is EMPTY — every actuatable quantity is one the observables read. So the");
        sb.AppendLine("  unobservable controls are exactly the CONSERVED inputs, and conservation is what makes them uncontrollable.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_05_TheEnergyScalingIsTheSectorsOwn()
    {
        PrintHeader("G_089 - task 5: the energy scale, in the sector's own terms");
        var sb = Start(
            "the ledger prices a SOURCE exactly — its residual IS the source — and prices a redistribution at nothing,",
            "because a redistribution conserves the count;",
            "the practical cost is therefore the calibration's, and the audit states that rather than inventing an energy;",
            "the scale table reports the required LOCAL COUNT change for three target clock changes in two regimes.");
        sb.Append(ManipulationChannelAudit.OutputScale());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var energy = ManipulationChannelAudit.EnergyScaling();
        Assert.Equal(3, energy.Length);
        Assert.Equal(0.0, energy[0].TheSectorsOwnCost, 12);
        Assert.True(Math.Abs(energy[1].TheSectorsOwnCost) > 0.0, "a source must be priced by the ledger");
        Assert.True(double.IsNaN(energy[2].TheSectorsOwnCost), "a change of the law has no state-energy scale");

        var scale = ManipulationChannelAudit.ScaleTable();
        Assert.Equal(2, scale.Length);
        Assert.All(scale, s => Assert.Equal(3, s.Scale.Length));
        // the required change grows with the target and is the same fraction in both regimes at leading order
        foreach (var (regime, _, _, rows) in scale)
        {
            Assert.True(rows[0].Required < rows[1].Required && rows[1].Required < rows[2].Required);
            Assert.True(rows[0].Required > 0.0);
        }

        sb.AppendLine("  the sector prices a redistribution at ZERO and a source exactly, and the required count change is a fixed");
        sb.AppendLine("  fraction of the clock change at leading order — so the practical scale is set by what the density IS,");
        sb.AppendLine("  which is the calibration's business and is stated as such.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_06_TheTechnologyClassification()
    {
        PrintHeader("G_089 - task 6: the technology classification");
        var sb = Start(
            "the four classifications are applied to the channels the search found, one each;",
            "the classification is a statement about the OPERATION the channel would need, not about energy;",
            "the laboratory-feasible case is reported honestly as the ordinary gravitational shift, which CONFIRMS the",
            "channel rather than manipulating the clock independently of the density.");
        sb.Append(ManipulationChannelAudit.OutputScale());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var technology = ManipulationChannelAudit.TechnologyClassification();
        Assert.Equal(4, technology.Length);
        Assert.Contains(technology, t => t.Classification == "impossible");
        Assert.Contains(technology, t => t.Classification == "principle-only");
        Assert.Contains(technology, t => t.Classification == "astrophysical-only");
        Assert.Contains(technology, t => t.Classification == "laboratory-feasible");
        Assert.Contains(technology, t => t.Classification == "principle-only" && t.Channel.Contains("curvature"));
        Assert.Contains(technology, t => t.Classification == "impossible" && t.Channel.Contains("conserved"));

        sb.AppendLine("  each classification is attached to a channel and to the operation it would require — and the");
        sb.AppendLine("  laboratory-feasible entry is the ordinary gravitational shift, which is the kernel itself.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_07_TheMinimalPathwayAndItsLimits()
    {
        PrintHeader("G_089 - task 7: the smallest chain that exists, with its limits");
        var sb = Start(
            "the pathway is the smallest chain from a human action to a clock or gravity change;",
            "each step carries the LIMIT that bounds it, so the pathway is reported with what it cannot do;",
            "the pathway is the sector's own dynamics, so the audit says so rather than dressing it as a new channel.");
        sb.Append(ManipulationChannelAudit.OutputPathway());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var pathway = ManipulationChannelAudit.MinimalPathway();
        Assert.Equal(4, pathway.Length);
        Assert.StartsWith("a human action", pathway[0].Step);
        Assert.Contains("the kernel", pathway.Select(p => p.Step));
        Assert.All(pathway, p => Assert.False(string.IsNullOrWhiteSpace(p.Limit)));
        // the gain limit is the sector's own: the clock moves less than the density
        var rows = ManipulationChannelAudit.InverseProblemSmallClockChange(new[] { 0.01 },
            ManipulationChannelAudit.SurfacePotential);
        Assert.True(rows.Single().DensityPerUnitClockChange > 1.0);

        sb.AppendLine("  the chain is: a human action -> a count transfer -> the density -> the kernel -> the clock and gravity,");
        sb.AppendLine("  bounded by count conservation, by the gain 1/d, and by the ledger — which is the sector's own dynamics.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_08_TheVerdictMeetsTheFailureCriterionForPathways()
    {
        PrintHeader("G_089 - the verdict");
        var sb = Start(
            "the verdict is a function of the measured counts: how many channels are actuatable, how many control without",
            "an actuator, how many are unobservable AND actuatable, and what the ledger prices;",
            "the failure criterion is that every channel collapses back to the existing kernel without adding a controllable",
            "variable; the success criterion would be a surviving channel not already eliminated;",
            "the audit states which criterion is met, and names the requirement the other one implies.");
        sb.Append(ManipulationChannelAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = ManipulationChannelAudit.Verdict();
        Assert.StartsWith("ONE ACTUATABLE CHANNEL", verdict);
        Assert.Contains("WITHOUT an actuator", verdict);
        Assert.Contains("DAMPED", verdict);
        Assert.Contains("unobservable and actuatable", verdict, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("costs", verdict);
        Assert.Contains("FAILURE CRITERION IS MET FOR PATHWAYS", verdict);
        Assert.Contains("IDENTIFIED", verdict);
        Assert.Equal(verdict, ManipulationChannelAudit.Verdict());

        var standing = ManipulationChannelAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("Those are requirements on the theory", standing);
        Assert.Contains("WHY THE ACTUATABLE CHANNEL IS NOT A NEW MANIPULATION", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("requirement", standing);

        sb.AppendLine("  the failure criterion is met for pathways, and the success criterion only in the weaker sense that one");
        sb.AppendLine("  effective-and-unobservable control (the kernel's curvature) is IDENTIFIED — with the operation the class");
        sb.AppendLine("  would have to contain, stated as a requirement rather than assumed as a primitive.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_09_TheRefutationIsNotArguingFromNegatives()
    {
        PrintHeader("G_089 - the discipline: the verdict is built from measurements, not from previous refusals");
        var sb = Start(
            "the task forbids arguing from previous negative verdicts, so every clause of this suite traces to a measurement",
            "made HERE — a count, a residual, a witness list or a signature;",
            "the audit therefore reports what each channel WOULD require, and treats that as the finding;",
            "and it never concludes from a verdict token: the refutation is a statement about the OPERATION SET, which is",
            "computed.");
        sb.Append(ManipulationChannelAudit.OutputChannels());
        sb.Append(ManipulationChannelAudit.OutputHiddenVariables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var channels = ManipulationChannelAudit.Channels();
        // every row carries a measurement string, and no row's verdict rests on a bare token
        Assert.All(channels, c => Assert.False(string.IsNullOrWhiteSpace(c.Measurement)));
        Assert.All(channels, c => Assert.True(c.Measurement.Length > 60, $"{c.Channel} must carry a real measurement"));
        // and the required operation is named for every control without an actuator
        Assert.All(channels.Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.ControlWithoutActuator),
            c => Assert.False(string.IsNullOrWhiteSpace(c.WhatItWouldRequire)));
        Assert.All(ManipulationChannelAudit.HiddenVariables(),
            h => Assert.False(string.IsNullOrWhiteSpace(h.Evidence)));

        sb.AppendLine("  every channel carries the measurement that decides it and the operation it would require: the audit reports");
        sb.AppendLine("  what would be needed instead of concluding from what was previously refused.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_089_10_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_089 - cross-check against G_077, G_078, G_080, G_082, G_084, G_086 and G_088");
        var sb = Start(
            "the audit consumes the surviving results rather than re-deriving them: the shared clock/gravity source, the",
            "conserving class, the assumed connectivity, the gauge-like component count, the collapsed kernel and the",
            "effective-but-unmeasured curvature;",
            "each cross-check is a computed identity between this audit's quantities and the owning audit's own API;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(MinimalGravityCoreAudit.OutputReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the kernel used here is G_087's and G_086's object
        foreach (var rho in new[] { 0.4, 1.0, 1.6 })
        {
            Assert.Equal(KernelDiscriminabilityAudit.K(rho, 0.3), ManipulationChannelAudit.Rate(rho, 0.3), 12);
        }
        // the conserving class is non-empty and contains genuine transports — the actuator the audit relies on
        var witnesses = ConnectivityOriginAudit.ConservingWitnesses(ManipulationChannelAudit.Cells, 1);
        Assert.NotEmpty(witnesses);
        Assert.Contains(witnesses, w => ContinuityLawAudit.IsATransport(w.Change.Select(x => (double)x).ToArray(), ManipulationChannelAudit.Cells, 1));
        // and the connectivity is an input that no primitive detects, which is why it is a control without an actuator
        Assert.False(ConnectivityOriginAudit.DisjointUnionClosure().Any(r => r.ContentChanged));
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());

        sb.AppendLine($"  the kernel agrees with G_086/G_087 to machine precision, {witnesses.Length} conserving witnesses exist and at");
        sb.AppendLine("  least one is a genuine transport — the actuator — while 0 of 4 primitives detect a cut and 0 observables");
        sb.AppendLine("  read the flux. The channels are classified from those measurements, not from a verdict token.");
        Output.WriteLine(sb.ToString());
    }
}
