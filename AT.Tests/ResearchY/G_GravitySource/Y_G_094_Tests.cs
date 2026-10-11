using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_094 - PHYSICS VERSUS ELEGANCE AUDIT.
///
/// G_093 measured the closure surplus: three structures are required for first-principles completion while being
/// neither observable nor dynamical - a memory channel carrying the kernel's free coefficient, the vector sector and
/// the unified action - and the theory has none of them. This suite asks whether that matters: if the gaps were
/// closed, would any physical prediction change, or would only the mathematical description become more complete?
///
/// Neither easy answer is allowed: closure is not assumed to change physics, and it is not assumed to be only
/// elegance. So each gap is closed HYPOTHETICALLY in the strongest form the surviving audits support, and the effect
/// is measured on the surviving readouts. Every figure is CONDITIONAL on the value the closure delivers, which is
/// stated in the rows rather than in a footnote.
///
/// The report follows the seven tasks. Substrate: the canonical 96-cell ring (the cubic substrate only where the
/// vector sector is named, by E_004's own measurement); equality rule 1e-12 on a readout value; no spectral quantity
/// is quoted.
/// </summary>
public sealed class Y_G_094_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_094_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_094_01_TheFourHypotheticalClosedForms()
    {
        PrintHeader("G_094 - task 1: the strongest surviving candidate form of each gap");
        var sb = Start(
            "the closed forms are HYPOTHETICAL, and the audit says so in every row rather than in a footnote;",
            "the strongest currently surviving candidate is used for each: a coefficient-carrying structure for the",
            "memory channel (the very actuator G_091 named), E_004's T1(3) with the curl form for the vector sector,",
            "the derived grid for the theta sector, and the join that would derive the map and the kernel for the",
            "unified action;",
            "the operating point is the WINDOW'S EDGE, so every figure is the smallest the closure could matter by.",
            "no closure is assumed to change physics, and none is assumed to be only elegance.");
        sb.Append(PhysicsVersusEleganceAudit.OutputForms());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var forms = PhysicsVersusEleganceAudit.CandidateForms();
        Assert.Equal(4, forms.Length);
        Assert.Contains(forms, f => f.Gap.Contains("memory channel") && f.CandidateForm.Contains("CARRIES THE COEFFICIENT"));
        Assert.Contains(forms, f => f.Gap.Contains("vector sector") && f.CandidateForm.Contains("T1(3)"));
        Assert.Contains(forms, f => f.Gap.Contains("theta") && f.CandidateForm.Contains("DERIVED"));
        Assert.Contains(forms, f => f.Gap.Contains("unified action") && f.CandidateForm.Contains("BOTH sectors"));
        // the operating point is measured from the recorded instrument classes rather than chosen
        double window = PhysicsVersusEleganceAudit.WindowEdge();
        Assert.True(window > 0.0);
        Assert.True(window < KernelDiscriminabilityAudit.ThresholdsByClass().Max(c => c.SmallestDetectableC));
        // the four gaps are exactly the three G_093 measured absent, plus the theta sector the question adds
        Assert.Equal(3, DynamicCoreRecoveryAudit.ClosureSurplus().Count(s =>
            PhysicsVersusEleganceAudit.Gaps().Any(g => s.Name.Contains(g.Gap.Replace("the ", "").Split(' ')[0]))));

        sb.AppendLine($"  four forms, and the operating point is the window's edge |c| = {F3(window)}: the SMALLEST coefficient the");
        sb.AppendLine("  best recorded class can see, so the measured effects below are lower bounds.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_02_TheSixReadoutSetsRecomputed()
    {
        PrintHeader("G_094 - task 2: recompute the six readout sets under the strongest closure");
        var sb = Start(
            "the six sets the task names are recomputed: redshift, clock rate, acceleration, the neutron-star",
            "comparison, cosmology, and the manipulation-channel results;",
            "each is a measured quantity before and after the closure, so the change is a difference rather than a",
            "judgement;",
            "the clock rate is computed at the MEAN density, which is where the kernel is exactly 1 for every",
            "coefficient - the reason the closure is invisible to cosmology;",
            "the coefficient used is the window's edge, the smallest the data could still hide.");
        sb.Append(PhysicsVersusEleganceAudit.OutputRecomputed());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = PhysicsVersusEleganceAudit.Recomputed();
        Assert.Equal(6, rows.Length);
        var redshift = rows.Single(r => r.ReadoutSet.Contains("redshift at the target"));
        var clock = rows.Single(r => r.ReadoutSet.Contains("clock rate"));
        var acceleration = rows.Single(r => r.ReadoutSet.Contains("acceleration"));
        var star = rows.Single(r => r.ReadoutSet.Contains("neutron-star"));
        var cosmology = rows.Single(r => r.ReadoutSet.Contains("cosmology"));
        var manipulation = rows.Single(r => r.ReadoutSet.Contains("manipulation"));
        // four of the six move, and four of them are the memory channel's
        Assert.Equal(4, rows.Count(r => r.Moves));
        Assert.True(redshift.Moves);
        Assert.True(acceleration.Moves);
        Assert.True(star.Moves);
        Assert.True(manipulation.Moves);
        // the clock rate does NOT move: at the mean density every kernel gives exactly 1
        Assert.False(clock.Moves);
        Assert.Equal(0.0, clock.Change, 12);
        Assert.Equal(1.0, clock.UnderClosure, 12);
        Assert.Equal(KernelDiscriminabilityAudit.K(1.0, 7.5), KernelDiscriminabilityAudit.K(1.0, -3.0), 12);
        // and the cosmology set does not move either, by eight orders of magnitude
        Assert.False(cosmology.Moves);
        Assert.True(redshift.Change / cosmology.Change > 1e6);
        // the manipulation set moves by ONE CHANNEL, which is the only set that moves by a count
        Assert.Equal(1.0, manipulation.Change, 12);

        sb.AppendLine($"  four of the six sets move: the redshift by {N(redshift.Change)}, the acceleration by {N(acceleration.Change)},");
        sb.AppendLine($"  the neutron-star comparison reaches {N(star.UnderClosure)} (decisive only above |c| = {F3(PhysicsVersusEleganceAudit.DecisiveCoefficient())}),");
        sb.AppendLine($"  and the manipulation results gain {F3(manipulation.Change)} channel. The clock rate and the cosmology set do not");
        sb.AppendLine("  move at all - the kernel is exactly 1 at the mean density, for every coefficient.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_03_TheFourGapsAndTheirEffects()
    {
        PrintHeader("G_094 - tasks 1 and 3: classify every closure effect");
        var sb = Start(
            "each gap's effect is classified as changes observable, changes dynamics only, changes interpretation",
            "only, or no change at all;",
            "the classification is a function of the MEASURED readout movement, not of the gap's importance;",
            "the memory channel is the only form that delivers a value the theory does not already pin, and that is",
            "why it is the only one that moves a readout;",
            "a gap that is a physics gap for ANOTHER group is reported as such rather than as elegance.");
        sb.Append(PhysicsVersusEleganceAudit.OutputGaps());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var gaps = PhysicsVersusEleganceAudit.Gaps();
        Assert.Equal(4, gaps.Length);
        var memory = gaps.Single(g => g.Gap.Contains("memory"));
        var vector = gaps.Single(g => g.Gap.Contains("vector"));
        var theta = gaps.Single(g => g.Gap.Contains("theta"));
        var action = gaps.Single(g => g.Gap.Contains("unified"));
        // the memory channel is the only measurable one
        Assert.Equal(7, memory.ReadoutsMoved);
        Assert.True(memory.LargestRelativeChangePerUnit > 0.3);
        Assert.True(memory.ChangesTheNeutronStarVerdict);
        Assert.True(memory.AddsAManipulationChannel);
        Assert.False(memory.ChangesTheMeanDensityReadout);
        Assert.Equal(ClosureEffect.ChangesObservable, memory.Effect);
        // the other three move NO readout at all
        Assert.All(new[] { vector, theta, action }, g => Assert.Equal(0, g.ReadoutsMoved));
        Assert.All(new[] { vector, theta, action }, g => Assert.False(g.AddsAManipulationChannel));
        Assert.All(new[] { vector, theta, action }, g => Assert.Equal(0.0, g.LargestRelativeChangePerUnit, 12));
        // and their kinds differ: the vector sector is another group's PHYSICS gap
        Assert.Equal(GapKind.PhysicsGap, vector.Kind);
        Assert.False(vector.IsOnTheTimeSectorsSubstrate);
        Assert.Equal(GapKind.MathematicalGap, theta.Kind);
        Assert.Equal(GapKind.MathematicalGap, action.Kind);
        // the identified G_093 surplus is the two absences, and the theta sector is the question's addition
        Assert.Equal(2, gaps.Count(g => !g.IsOnTheTimeSectorsSubstrate));

        sb.AppendLine($"  the memory channel moves {memory.ReadoutsMoved} readouts and adds a channel; the vector sector, the theta");
        sb.AppendLine("  sector and the unified action move NONE. The vector sector's kind is a PhysicsGap all the same - for");
        sb.AppendLine("  the group that owns it - which is why the audit does not call it elegance.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_04_TheGapKindsAndTheEmptyDynamicalGap()
    {
        PrintHeader("G_094 - task 4: physics, dynamical, mathematical or documentation");
        var sb = Start(
            "the four kinds are counted over the closed gaps, with one kind MEASURED as empty;",
            "the dynamical kind is empty for a measured reason rather than a definitional one: none of the four forms",
            "is an argument of the update relation, and the reduction's own residual is exactly zero;",
            "the documentation kind is empty because each gap names a STRUCTURE rather than an unwritten paragraph;",
            "a gap is never filed under a weaker kind than its measurement supports.");
        sb.Append(PhysicsVersusEleganceAudit.OutputGaps());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var kinds = PhysicsVersusEleganceAudit.GapKinds();
        Assert.Equal(4, kinds.Length);
        Assert.Equal(2, kinds.Single(k => k.Kind == GapKind.PhysicsGap).Gaps);
        Assert.Equal(0, kinds.Single(k => k.Kind == GapKind.DynamicalGap).Gaps);
        Assert.Equal(2, kinds.Single(k => k.Kind == GapKind.MathematicalGap).Gaps);
        Assert.Equal(0, kinds.Single(k => k.Kind == GapKind.DocumentationGap).Gaps);
        // the empty dynamical kind is measured: the update relation reads neither the forms nor the map
        Assert.Equal(0.0, DynamicCoreRecoveryAudit.EvolutionResidual(
            MinimalGravityCoreAudit.Minimal(), MinimalGravityCoreAudit.Full()), 12);
        Assert.Equal(0.0, DynamicCoreRecoveryAudit.Theories()[0].EvolutionResidual, 12);
        // and the map's own freedom is excluded, which is why the unified action's kind is mathematical
        var mapVariations = MapRedundancyAudit.VariationTable().Where(v => v.Variation.Contains("potential")).ToArray();
        Assert.Equal(2, mapVariations.Count(v => v.Verdict.StartsWith("OBSERVABLE")));
        Assert.Contains(mapVariations, v => Math.Abs(v.SectorValueResidual - 0.5) < 1e-12);

        sb.AppendLine($"  kinds: physics {kinds.Single(k => k.Kind == GapKind.PhysicsGap).Gaps}, dynamical 0, mathematical");
        sb.AppendLine($"  {kinds.Single(k => k.Kind == GapKind.MathematicalGap).Gaps}, documentation 0. The empty dynamical kind is measured:");
        sb.AppendLine("  the update relation takes the state and the links, and the residual is exactly zero.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_05_TheThreeGains()
    {
        PrintHeader("G_094 - task 5: ObservableGain, DynamicGain, ManipulationGain");
        var sb = Start(
            "the three gains are measured counts rather than judgements: readouts that move, structures that enter",
            "the evolution, and channels an operator would gain;",
            "the ObservableGain counts DISTINCT readouts once each, so the sum cannot double-count the same",
            "sensitivity;",
            "the DynamicGain is the reduction's own measured residual, which is the honest instrument for 'does the",
            "evolution change';",
            "the ManipulationGain is the rise in the actuatable channel count.");
        sb.Append(PhysicsVersusEleganceAudit.OutputGaps());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var gains = PhysicsVersusEleganceAudit.Gains().ToDictionary(g => g.Gain);
        Assert.Equal(3, gains.Count);
        // ObservableGain: seven readouts, all through one gap, and the largest sensitivity is the AT/GR comparison
        Assert.Equal(7, gains["ObservableGain"].Count);
        Assert.Equal("the memory channel", gains["ObservableGain"].Which);
        Assert.Equal(KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").Max(p => p.PerUnitCurvature), gains["ObservableGain"].Measure, 12);
        // DynamicGain: zero, measured
        Assert.Equal(0, gains["DynamicGain"].Count);
        Assert.Equal(0.0, gains["DynamicGain"].Measure, 12);
        // ManipulationGain: one channel, and the curvature's coupling is what it replaces
        Assert.Equal(1, gains["ManipulationGain"].Count);
        Assert.Equal(0.0, KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength, 12);
        Assert.Equal(1, ActuatorVersusObservableAudit.ChainCounts().ActuatableOnTheSurvivingTheory);
        // so the observable gain and the manipulation gain have the SAME carrier
        Assert.Equal(gains["ObservableGain"].Which, gains["ManipulationGain"].Which);

        sb.AppendLine($"  ObservableGain {gains["ObservableGain"].Count} readouts (largest {N(gains["ObservableGain"].Measure)} relative per unit");
        sb.AppendLine($"  coefficient), DynamicGain {gains["DynamicGain"].Count}, ManipulationGain {gains["ManipulationGain"].Count} channel - and the observable and");
        sb.AppendLine("  manipulation gains have the SAME carrier, the memory channel.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_06_TheCounterfactualTest()
    {
        PrintHeader("G_094 - task 6: assume all four gaps are solved - what appears first?");
        var sb = Start(
            "the consequences are ordered by the THRESHOLD each needs, not by the order in which they were imagined;",
            "a capability needs no instrument, so it comes first and is not a numerical prediction at all;",
            "the measurements are then ordered by the coefficient they can see, using the recorded instrument classes;",
            "and one consequence is ruled out entirely rather than merely deferred: a cosmology consequence cannot",
            "appear, because the closure is invisible at the mean density.");
        sb.Append(PhysicsVersusEleganceAudit.OutputCounterfactual());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var first = PhysicsVersusEleganceAudit.FirstConsequences();
        Assert.Equal(4, first.Length);
        Assert.StartsWith("a MANIPULATION consequence", first.Single(f => f.Order == 0).Consequence);
        Assert.Contains("NONE", first.Single(f => f.Order == 0).Threshold);
        // the first MEASUREMENT is the compact-object redshift, at the window's edge
        var measurable = first.Single(f => f.Order == 1);
        Assert.Contains(F3(PhysicsVersusEleganceAudit.WindowEdge()), measurable.Threshold);
        // the decisive comparison comes later, and its threshold is the measured crossover
        var decisive = first.Single(f => f.Order == 2);
        Assert.Contains(F3(PhysicsVersusEleganceAudit.DecisiveCoefficient()), decisive.Threshold);
        Assert.True(PhysicsVersusEleganceAudit.DecisiveCoefficient() > PhysicsVersusEleganceAudit.WindowEdge());
        // and the cosmology consequence never appears
        Assert.Contains("INVISIBLE", first.Single(f => f.Order == 3).Threshold);
        // the decisive threshold is consistent with the sweep: the signature reaches the separation there, to the
        // bisection's own residual
        double decisiveC = PhysicsVersusEleganceAudit.DecisiveCoefficient();
        Assert.True(Math.Abs(KernelDiscriminabilityAudit.Signature(PhysicsVersusEleganceAudit.TargetCompactness, decisiveC))
            >= KernelDiscriminabilityAudit.AgainstTheSeparation().AtGrSeparation);
        Assert.True(PhysicsVersusEleganceAudit.DecisiveResidual()
            < 1e-6 * KernelDiscriminabilityAudit.AgainstTheSeparation().AtGrSeparation);

        sb.AppendLine($"  first is the CAPABILITY (no instrument at all), then the redshift at |c| = {F3(PhysicsVersusEleganceAudit.WindowEdge())}, then");
        sb.AppendLine($"  the decisive comparison at |c| = {F3(decisiveC)} - and a cosmology consequence NEVER.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_07_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_094 - task 7: physics, dynamics, or only elegance");
        var sb = Start(
            "the task's three-way classification is decided by counts over the four gaps;",
            "the conditionality is not hidden: the theory currently carries coefficient ZERO, so a closure returning",
            "the same value would change nothing and only improve the derivation;",
            "the figures are at the window's edge, which is the SMALLEST the closure could matter by;",
            "no literal reaches the classification (G_027).");
        sb.Append(PhysicsVersusEleganceAudit.OutputClassification());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = PhysicsVersusEleganceAudit.Classification();
        Assert.Equal(5, classification.Length);
        Assert.Equal("YES", classification.Single(c => c.Question.Contains("change PHYSICS")).Answer);
        Assert.Equal("NO", classification.Single(c => c.Question.Contains("change DYNAMICS")).Answer);
        Assert.Equal("NO", classification.Single(c => c.Question.Contains("only ELEGANCE")).Answer);
        Assert.Equal("MET", classification.Single(c => c.Question.StartsWith("SUCCESS")).Answer);
        Assert.Equal("REFUTED", classification.Single(c => c.Question.StartsWith("FAILURE")).Answer);
        // the conditionality, measured: the theory's own law carries coefficient zero, so its signature is exactly 0
        Assert.Equal(0.0, KernelDiscriminabilityAudit.Signature(PhysicsVersusEleganceAudit.TargetCompactness, 0.0), 12);
        Assert.Equal(KernelDimension.OneFunction,
            ObservableKernelAudit.Theories().Single(t => t.Name.StartsWith("the sector's own law")).Dimension);
        Assert.Equal(KernelDimension.TwoFunctions,
            ObservableKernelAudit.Theories().Single(t => t.Name.StartsWith("the free room")).Dimension);

        sb.AppendLine();
        sb.AppendLine("  the closure changes PHYSICS, does not change dynamics, and is not only elegance: one of the four gaps");
        sb.AppendLine("  carries every observable and every manipulation consequence the closure has.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_094_08_TheVerdict()
    {
        PrintHeader("G_094 - the verdict, computed from the measured rows");
        var verdict = PhysicsVersusEleganceAudit.Verdict();
        Output.WriteLine(verdict);

        Assert.StartsWith("PHYSICS, NOT ELEGANCE", verdict);
        Assert.Contains("ONLY THROUGH ONE GAP", verdict);
        Assert.Contains("REOPENS", verdict);
        Assert.Contains("INHERITED", verdict);
        Assert.Contains("invisible at the mean density", verdict);
        // every number in the verdict is one of the audit's own measurements
        Assert.Contains(F3(KernelCurvatureActuationAudit.PerUnitInput()
            .Where(p => p.Side == "the effect side").Max(p => p.PerUnitCurvature)), verdict);
        Assert.Contains(F3(PhysicsVersusEleganceAudit.DecisiveCoefficient()), verdict);
        Assert.Contains(F3(PhysicsVersusEleganceAudit.WindowEdge()), verdict);
    }

    [Fact]
    public void Y_G_094_09_TheAuditAgreesWithTheChainAndWithE_004()
    {
        PrintHeader("G_094 - cross-check against G_085, G_086, G_091, G_093 and E_004");
        var sb = Start(
            "the audit consumes four of the chain's results and one from group E, so each is cross-checked by",
            "computed identity rather than by quotation;",
            "G_093 supplies the four gaps and their absence, G_085 the free room, G_086 the mean-density agreement,",
            "G_091 the effect side and the zero coupling, and E_004 the vector sector;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(DynamicCoreRecoveryAudit.OutputCores());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // G_093: the three absent structures are exactly the closure surplus
        var surplus = DynamicCoreRecoveryAudit.ClosureSurplus();
        Assert.Equal(3, surplus.Length);
        Assert.All(surplus, s => Assert.False(s.PresentInTheTheory));
        // G_085: the free room is the one variation that stays undetermined while the pinned data hold
        var freeRoom = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room"));
        Assert.True(freeRoom.PredictionsMoved > 0);
        Assert.True(freeRoom.PinnedDataHold);
        Assert.StartsWith("EFFECTIVE", freeRoom.Verdict);
        Assert.True(MinimalGravityCoreAudit.ClockLawFamily().Alternatives.Length > 1);
        // G_086: at the mean density every kernel agrees, which is why the closure is invisible to cosmology
        Assert.All(ObservableKernelAudit.DimensionTable(), k => Assert.True(k.MismatchAtTheMeanDensity < 1e-8));
        Assert.True(ObservableKernelAudit.DimensionTable()
            .Single(k => k.Theory.StartsWith("the free room")).MismatchElsewhere > 1e-2);
        // G_091: seven effect readouts and exactly zero coupling on the actuation side
        Assert.Equal(7, KernelCurvatureActuationAudit.PerUnitInput().Count(p => p.Side == "the effect side"));
        Assert.Equal(0.0, KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength, 12);
        Assert.StartsWith("KERNEL CURVATURE IS A PARAMETER", KernelCurvatureActuationAudit.Verdict());
        // E_004: the vector sector is another substrate's, and the curl form is what makes it two-state
        Assert.False(VectorSectorAudit.SingleRingHasVectorSector());
        Assert.True(VectorSectorAudit.CubicHasVectorSector());
        var modes = VectorSectorAudit.FormsOnModes();
        Assert.Equal(0, modes.CurlLong);
        Assert.Equal(2, modes.CurlTrans);

        sb.AppendLine("  the chain's own numbers are reproduced: the three absent structures G_093 measured, the free room as");
        sb.AppendLine("  the one undetermined variation, the mean-density agreement that blinds cosmology, the seven effect");
        sb.AppendLine("  readouts with zero coupling, and E_004's vector sector on the cubic substrate alone.");
        Output.WriteLine(sb.ToString());
    }
}
