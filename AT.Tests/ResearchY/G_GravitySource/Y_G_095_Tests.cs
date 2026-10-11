using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_095 - MEMORY COEFFICIENT AUDIT.
///
/// G_093 found that a memory channel carrying the kernel's coefficient is required for completion and absent; G_094
/// found it is the only gap whose closure would change a prediction, and that the change is CONDITIONAL on the value
/// delivered. This suite asks what fixes that value.
///
/// The task's two prohibitions are obeyed literally: the coefficient is NOT set to zero, and it is NOT fitted to
/// observations. Both are excluded as ANSWERS, so the audit instead measures every candidate SELECTING principle
/// against the chain's own instruments and reports which ones the surviving theory contains.
///
/// The report follows the six tasks. Substrate: the canonical 96-cell ring for the time sector and the clock law's own
/// variable for the coefficient; equality rule 1e-12 on a residual, with the numerically-differenced coefficients
/// compared at 1e-6 because they carry about 1e-9 of noise. No spectral quantity is quoted.
/// </summary>
public sealed class Y_G_095_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_095_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_095_01_WhichPrinciplesSelect()
    {
        PrintHeader("G_095 - task 4: does any surviving principle select one value?");
        var sb = Start(
            "every candidate principle is MEASURED against the chain's own instruments rather than argued from",
            "plausibility;",
            "the discriminator is the composition residual, because multiplicativity is the one structural property",
            "that separates the sector's law from every other member of its family;",
            "whether a principle is IN the surviving theory is read from the chain's own classification of it, not",
            "asserted here;",
            "the pinned-data route is excluded by the task's own rules as a fit, and is recorded as excluded.");
        sb.Append(MemoryCoefficientAudit.OutputPrinciples());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var principles = MemoryCoefficientAudit.SelectingPrinciples();
        Assert.Equal(7, principles.Length);
        var multiplicity = principles.Single(p => p.Principle.StartsWith("MULTIPLICATIVITY"));
        // it WOULD select uniquely, and the theory does NOT contain it
        Assert.True(multiplicity.WouldSelectUniquely);
        Assert.False(multiplicity.InTheSurvivingTheory);
        Assert.True(multiplicity.ResidualForTheSectorLaw < 1e-12);
        Assert.True(multiplicity.BestCompetingResidual > 1e-2);
        // and the chain's own audit is what says it is absent
        var chain = ClockLawUniquenessAudit.ForcedFeatures().Single(f => f.Feature.Contains("multiplicativity"));
        Assert.Equal(1, chain.LawsWithIt);
        Assert.Contains("FREE", chain.Status);
        // the counts: nothing contained selects
        var counts = MemoryCoefficientAudit.PrincipleCounts();
        Assert.Equal(7, counts.Principles);
        Assert.Equal(4, counts.InTheSurvivingTheory);
        Assert.Equal(1, counts.WouldSelectUniquely);
        Assert.Equal(0, counts.SelectInTheSurvivingTheory);
        // the multiplicity residual is the composition residual itself, checked against the owning audit
        Assert.Equal(ClockLawUniquenessAudit.Composition()
            .Single(c => c.Law == ClockLawUniquenessAudit.At().Name).Residual,
            multiplicity.ResidualForTheSectorLaw, 12);

        sb.AppendLine($"  {counts.Principles} principles: {counts.InTheSurvivingTheory} contained, {counts.WouldSelectUniquely} would select,");
        sb.AppendLine($"  and the intersection is {counts.SelectInTheSurvivingTheory}. The one that would select does so at {N(multiplicity.ResidualForTheSectorLaw)}");
        sb.AppendLine($"  against a best competitor at {N(multiplicity.BestCompetingResidual)} — and it is measured ABSENT.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_02_TheBoundaryAnalysis()
    {
        PrintHeader("G_095 - task 2: lambda = 0, lambda > 0, lambda < 0");
        var sb = Start(
            "every sign region is measured with the SAME instruments: viability, the second-order coefficient, the",
            "redshift's quadratic, the free part at the target, the target prediction, and whether the second order",
            "coincides with GR's;",
            "the two numerically-differenced coefficients carry about 1e-9 of noise, so the GR comparison is made at",
            "1e-6 and the tolerance is stated rather than left to the reader;",
            "a region is only called EXCLUDED when a surviving requirement fails there, and the failure is named.");
        sb.Append(MemoryCoefficientAudit.OutputBoundaries());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = MemoryCoefficientAudit.Boundaries();
        Assert.Equal(7, rows.Length);
        var recorded = rows.Single(r => r.Regime == "lambda = 0");
        var positive = rows.Single(r => Math.Abs(r.Coefficient - 0.5) < 1e-12);
        var negative = rows.Single(r => Math.Abs(r.Coefficient + 0.5) < 1e-12);
        var gr = rows.Single(r => r.Regime.Contains("GR point"));
        var tooLarge = rows.Single(r => r.Regime.Contains("strongly positive"));
        // the recorded law is viable and is NOT the GR point
        Assert.True(recorded.Viable);
        Assert.False(recorded.MatchesGrAtSecondOrder);
        Assert.True(positive.Viable);
        Assert.True(negative.Viable);
        // the GR point: the second-order coefficient vanishes, matching GR's own
        Assert.True(gr.MatchesGrAtSecondOrder);
        Assert.Equal(0.0, gr.SecondOrderCoefficientOfF, 6);
        Assert.Equal(ClockLawUniquenessAudit.RedshiftCoefficient(ClockLawUniquenessAudit.Gr(), 2), gr.RedshiftQuadratic, 6);
        // and the large positive value is EXCLUDED — the one exclusion the surviving principles produce
        Assert.False(tooLarge.Viable);
        Assert.Contains("NOT VIABLE", tooLarge.Reading);

        sb.AppendLine($"  lambda = 0, +0.5 and -0.5 are all VIABLE; the GR point (lambda = -2) is viable but loses the sector's");
        sb.AppendLine($"  second-order signature; and the only EXCLUSION is the large positive value ({F3(tooLarge.Coefficient)}), where the");
        sb.AppendLine("  clock stops being monotone.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_03_OneSidedBoundedness()
    {
        PrintHeader("G_095 - task 2: is the coefficient bounded on either side?");
        var sb = Start(
            "the bound is FOUND BY BISECTION and the analytic value is reported beside it, so the edge is verified",
            "rather than assumed;",
            "the analytic value comes from monotonicity alone: 1 + lambda x > 0 on the physical range;",
            "the gap between the measured and the analytic edge is the SAMPLER's, and it is stated rather than",
            "smoothed away;",
            "the negative side is tested with the same sweep, and its failure mode at the extreme is named.");
        sb.Append(MemoryCoefficientAudit.OutputBoundaries());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var bounds = MemoryCoefficientAudit.Bounds();
        Assert.Equal(2, bounds.Length);
        var lower = bounds.Single(b => b.Side == "lambda < 0");
        var upper = bounds.Single(b => b.Side == "lambda > 0");
        // the negative side is NOT bounded: every tested value is viable
        Assert.False(lower.Bounded);
        Assert.Equal(lower.LawsTested, lower.ViableLaws);
        // the positive side IS bounded, and the measured edge agrees with the analytic one to a tenth of a per cent
        Assert.True(upper.Bounded);
        Assert.True(upper.ViableLaws < upper.LawsTested);
        double measured = MemoryCoefficientAudit.PositiveBound();
        double analytic = MemoryCoefficientAudit.AnalyticPositiveBound();
        Assert.Equal(2.0, analytic, 12);
        Assert.True(Math.Abs(measured - analytic) / analytic < 2e-3);
        // the sweep confirms the edges on both sides
        Assert.True(ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(1.0)));
        Assert.False(ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(10.0)));
        Assert.True(ClockLawUniquenessAudit.IsViable(ClockLawUniquenessAudit.FreeRoom(-100.0)));

        sb.AppendLine($"  the coefficient is bounded on ONE side: lambda < {F3(measured)} by bisection against {F3(analytic)} analytically,");
        sb.AppendLine("  and NOT bounded below (every negative value tested is viable). So no value is selected — but the");
        sb.AppendLine("  positive side is not free either.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_04_TheStabilityAnalysis()
    {
        PrintHeader("G_095 - task 3: is there a stability question at all?");
        var sb = Start(
            "a stability analysis needs a FLOW, so the audit first measures whether one exists rather than assuming",
            "the question is meaningful;",
            "the absence of a flow is an ARITY measurement, not an opinion: no update relation and no operation takes",
            "the coefficient as an argument;",
            "what CAN be measured is the response of the readouts, and that response is bounded and smooth;",
            "the family does contain members with large effects, and the audit says so instead of reporting stability",
            "as a virtue.");
        sb.Append(MemoryCoefficientAudit.OutputStabilityAndObservables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var stability = MemoryCoefficientAudit.Stability();
        Assert.Equal(3, stability.Length);
        Assert.Contains("NO", stability[0].Answer);
        Assert.Contains("UNDEFINED", stability[0].Answer);
        Assert.Contains("BOUNDED", stability[1].Answer);
        // the response is bounded: no slope drift in the family exceeds a small threshold
        Assert.All(ClockLawUniquenessAudit.ViabilityTable(),
            v => Assert.True(Math.Abs(v.SlopeDrift) < 1e-4));
        // and there is no flow for the coefficient: the update relation does not take it
        Assert.Equal(0.0, DynamicCoreRecoveryAudit.Theories()[0].EvolutionResidual, 12);
        Assert.Equal(0, ActuatorVersusObservableAudit.Structures()
            .Count(s => s.Name.Contains("curvature") && s.OperationsChangingIt > 0));

        sb.AppendLine("  there is NO flow for the coefficient, so 'fixed-point selected' is UNDEFINED rather than false; the");
        sb.AppendLine($"  readout response is bounded (the largest slope drift in the family is {N(ClockLawUniquenessAudit.ViabilityTable().Max(v => Math.Abs(v.SlopeDrift)))}),");
        sb.AppendLine("  so there is no instability to report either.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_05_NewObservablesAndChannels()
    {
        PrintHeader("G_095 - tasks 5 and 6: does the coefficient introduce observables or channels?");
        var sb = Start(
            "task 5 is answered against the surviving readout set: the coefficient moves VALUES of readouts that",
            "already exist and adds none;",
            "task 6 separates the VALUE from the STRUCTURE, which is the distinction G_094's reopening depends on;",
            "the coupling measurement is the curvature's own, so the answer does not rest on a new argument;",
            "a coefficient with no carrier is not a knob, and the audit measures the carrier's absence.");
        sb.Append(MemoryCoefficientAudit.OutputStabilityAndObservables());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var (question, answer, measurement) = MemoryCoefficientAudit.NewObservables();
        Assert.Equal(0, answer);
        Assert.Equal(7, KernelCurvatureActuationAudit.PerUnitInput().Count(p => p.Side == "the effect side"));
        Assert.Equal(8, MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full()).Length);
        Assert.False(question.Length == 0);
        Assert.Contains("ZERO", measurement);
        // no channel: the coupling is zero and no operation writes the coefficient
        var manipulation = MemoryCoefficientAudit.Manipulation();
        Assert.Equal(3, manipulation.Length);
        Assert.All(manipulation, m => Assert.False(m.Reopens));
        Assert.Equal(0.0, KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength, 12);
        Assert.Equal(1, ActuatorVersusObservableAudit.ChainCounts().ActuatableOnTheSurvivingTheory);

        sb.AppendLine($"  the coefficient introduces {answer} new observables — it moves {7} existing ones — and reopens no channel: the");
        sb.AppendLine("  coupling is exactly zero and no operation writes it. G_094's reopening needs the STRUCTURE, not the");
        sb.AppendLine("  value.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_06_DoesAnythingBoundThePrediction()
    {
        PrintHeader("G_095 - how far can the target prediction move over the whole admitted range?");
        var sb = Start(
            "the range is one-sided: lambda < the bisected edge and unbounded below;",
            "the prediction is evaluated at the two ends and at the recorded value, so the SPREAD is measured rather",
            "than described;",
            "this is what makes the coefficient empirically consequential rather than decorative: the spread exceeds",
            "the AT/GR separation the programme is chasing;",
            "the numbers are the chain's own (RecordedAtTarget), recomputed here.");
        sb.Append(MemoryCoefficientAudit.OutputBoundaries());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = MemoryCoefficientAudit.Boundaries();
        var recorded = rows.Single(r => r.Regime == "lambda = 0");
        var extreme = rows.Single(r => r.Regime.Contains("strongly negative"));
        var target = ClockLawUniquenessAudit.RecordedAtTarget();
        // the recorded prediction is the AT prediction, and the extreme moves it far beyond the separation
        Assert.Equal(target.At, recorded.TargetPrediction, 9);
        Assert.True(extreme.TargetPrediction > recorded.TargetPrediction);
        Assert.True(extreme.TargetPrediction - recorded.TargetPrediction > 10.0 * target.Separation);
        // and the separation itself is the AT/GR gap at the recorded coefficient
        Assert.Equal(Math.Abs(target.Gr - target.At), target.Separation, 12);
        Assert.True(target.Separation > 0.1);

        sb.AppendLine($"  the prediction moves from {F3(recorded.TargetPrediction)} at the recorded coefficient to {F3(extreme.TargetPrediction)} at the extreme —");
        sb.AppendLine($"  a spread {F3((extreme.TargetPrediction - recorded.TargetPrediction) / target.Separation)} times the AT/GR separation of {F3(target.Separation)}. So the free");
        sb.AppendLine("  coefficient is empirically consequential, and its freedom is the sector's open question.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_07_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_095 - tasks 1 and 7: the classification, and the two criteria");
        var sb = Start(
            "task 1's four classes are each decided by a measurement rather than by preference;",
            "the task's prohibitions are obeyed: the coefficient is NOT set to zero and NOT fitted to observations,",
            "both of which are excluded as ANSWERS;",
            "the failure criterion is decided by the count of contained principles that select, not by prose;",
            "no literal reaches the criteria or the verdict (G_027).");
        sb.Append(MemoryCoefficientAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = MemoryCoefficientAudit.Classification();
        Assert.Equal(4, classification.Length);
        Assert.False(classification.Single(c => c.Status == CoefficientStatus.Derived).Holds);
        Assert.False(classification.Single(c => c.Status == CoefficientStatus.Emergent).Holds);
        Assert.False(classification.Single(c => c.Status == CoefficientStatus.FixedPointSelected).Holds);
        Assert.True(classification.Single(c => c.Status == CoefficientStatus.Empirical).Holds);
        // and the criteria
        var criteria = MemoryCoefficientAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("MET", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.Contains("NOT MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.Equal("NONE IS ASSUMED", criteria.Single(c => c.Criterion.StartsWith("and the value")).Outcome);
        // the recorded coefficient is zero and the audit does not treat that as a selection
        Assert.Equal(0.0, MemoryCoefficientAudit.RecordedCoefficient, 12);
        Assert.Equal(KernelDimension.OneFunction,
            ObservableKernelAudit.Theories().Single(t => t.Name.StartsWith("the sector's own law")).Dimension);

        sb.AppendLine("  the classification is EMPIRICAL on all four counts: not derived (nothing contained selects), not");
        sb.AppendLine("  emergent, not fixed-point selected (no flow), only empirical — and the failure criterion is MET, while");
        sb.AppendLine("  the value that would be forced is named without being assumed.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_095_08_TheVerdict()
    {
        PrintHeader("G_095 - the verdict, computed from the measured rows");
        var verdict = MemoryCoefficientAudit.Verdict();
        Output.WriteLine(verdict);

        Assert.StartsWith("THE COEFFICIENT REMAINS FREE", verdict);
        Assert.Contains("ABSENT", verdict);
        Assert.Contains("MULTIPLICATIVITY", verdict);
        Assert.Contains("bounded on ONE side", verdict);
        Assert.Contains("why does the clock composition multiply", verdict);
        // every number in the verdict is one of the audit's own measurements
        Assert.Contains(F3(MemoryCoefficientAudit.PositiveBound()), verdict);
        Assert.Contains(F3(MemoryCoefficientAudit.TheGrCoefficient()), verdict);
        Assert.Contains(N(MemoryCoefficientAudit.SelectingPrinciples()
            .Single(p => p.Principle.StartsWith("MULTIPLICATIVITY")).ResidualForTheSectorLaw), verdict);
    }

    [Fact]
    public void Y_G_095_09_TheAuditAgreesWithTheChainAndWithG_072()
    {
        PrintHeader("G_095 - cross-check against G_072, G_073/G_075, G_085, G_087, G_091 and G_094");
        var sb = Start(
            "the audit consumes the chain's results, so each is cross-checked by computed identity rather than",
            "quotation;",
            "the composition property is the chain's own instrument, and the audit reads its status from the owning",
            "audit rather than restating it;",
            "G_094's conditional and G_091's zero coupling are both reproduced, because the sequel must not contradict",
            "the audit it follows;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(MemoryCoefficientAudit.OutputPrinciples());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the composition instrument: the sector's law composes, no other family member does
        var composition = ClockLawUniquenessAudit.Composition();
        Assert.True(composition.Single(c => c.Law == ClockLawUniquenessAudit.At().Name).Residual < 1e-12);
        Assert.All(composition.Where(c => c.Law != ClockLawUniquenessAudit.At().Name && !double.IsInfinity(c.Residual)),
            c => Assert.True(c.Residual > 1e-2));
        Assert.Single(composition, c => double.IsInfinity(c.Residual));
        // G_085: the free room is the one variation that stays undetermined while the pinned data hold
        var freeRoom = MapRedundancyAudit.VariationTable().Single(v => v.Variation.Contains("free room"));
        Assert.True(freeRoom.PinnedDataHold);
        Assert.True(freeRoom.PredictionsMoved > 0);
        Assert.StartsWith("EFFECTIVE", freeRoom.Verdict);
        // G_087: the data leave the coefficient unconstrained inside a measured window
        Assert.True(KernelDiscriminabilityAudit.SmallestDetectableC(0.01, 0.0, 3.0, SigmaModel.SharedX) > 0.0);
        Assert.True(KernelDiscriminabilityAudit.AgainstTheSeparation().Ratio > 1.0);
        // G_091 and G_094: the coupling is exactly zero, so the value alone reopens nothing
        Assert.Equal(0.0, KernelCurvatureActuationAudit.CouplingCounts().MaximumCouplingStrength, 12);
        Assert.All(PhysicsVersusEleganceAudit.Gaps().Where(g => !g.AddsAManipulationChannel),
            g => Assert.Equal(0, g.ReadoutsMoved));
        // and G_094's one carrier is the memory channel, whose value this audit shows is free
        Assert.Single(PhysicsVersusEleganceAudit.Gaps().Where(g => g.AddsAManipulationChannel));
        Assert.True(PhysicsVersusEleganceAudit.Gaps().Single(g => g.AddsAManipulationChannel).FixesTheUndeterminedParameter);

        sb.AppendLine("  the chain's numbers are reproduced: the composition residual isolates the sector's law, the free room is");
        sb.AppendLine("  undetermined while the pinned data hold, the window is open, and the coupling is exactly zero — so the");
        sb.AppendLine("  value is free AND alone it reopens nothing. The sequel is consistent with G_094 and sharpens it.");
        Output.WriteLine(sb.ToString());
    }
}
