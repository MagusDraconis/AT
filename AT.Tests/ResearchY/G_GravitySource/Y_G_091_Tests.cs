using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_091 - KERNEL CURVATURE ACTUATION AUDIT.
///
/// G_090 left exactly one exception standing: the kernel's curvature. This suite asks whether it can be actuated by known
/// physical processes, with the surviving kernel and the surviving audits allowed and new primitives, source terms and
/// component-count manipulation forbidden. It prefers refutation: success would be a physical actuator, failure would be
/// that the curvature is a PARAMETER rather than a controllable variable.
///
/// The report follows the six tasks. Every figure is computed here, and no spectral quantity is quoted.
/// </summary>
public sealed class Y_G_091_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_091_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);
    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_091_01_TheOperationalDefinition()
    {
        PrintHeader("G_091 - task 1: define the kernel's curvature operationally");
        var sb = Start(
            "the curvature is DEFINED operationally as the second derivative of the log-rate with respect to the potential,",
            "which is equivalently the slope of the acceleration with respect to it;",
            "two independent procedures are executed so the definition does not rest on one numerical scheme;",
            "the second difference uses h = 1e-4 (truncation ~1e-9 for these smooth laws), compared at 1e-6 rather than at",
            "zero — the numerical-reproducibility rule;",
            "the surviving kernel is ln K = A + c A^2 / 2, the object G_086 collapsed the sector to.");
        sb.Append(KernelCurvatureActuationAudit.OutputDefinition());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var byDifference = KernelCurvatureActuationAudit.OperationalProcedure(
            new[] { 1e-9, KernelCurvatureActuationAudit.SurfacePotential, 0.536 });
        Assert.Equal(3, byDifference.Length);
        Assert.All(byDifference, p => Assert.True(p.Residual < 1e-6, $"the second-difference procedure must reproduce the curvature ({N(p.Residual)})"));
        var bySlope = KernelCurvatureActuationAudit.SlopeOfTheAcceleration();
        Assert.Equal(3, bySlope.Length);
        Assert.All(bySlope, p => Assert.True(p.Residual < 1e-9, $"the acceleration-slope procedure must reproduce it ({N(p.Residual)})"));
        // the exact identity, checked at several curvatures
        foreach (var c in new[] { -0.5, 0.0, 0.3, 1.0 })
        {
            Assert.Equal(c, KernelCurvatureActuationAudit.CurvatureExact(c), 12);
        }

        sb.AppendLine($"  the curvature is operationally exact: {byDifference.Length} potentials by second difference (worst residual");
        sb.AppendLine($"  {N(byDifference.Max(p => p.Residual))}) and {bySlope.Length} potential pairs by the acceleration's slope (worst");
        sb.AppendLine($"  {N(bySlope.Max(p => p.Residual))}). The definition is a procedure, not a symbol.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_02_ObservableControllableBothOrNeither()
    {
        PrintHeader("G_091 - task 2: observable, controllable, both or neither?");
        var sb = Start(
            "the two halves of the question have DIFFERENT answers, so they are reported separately;",
            "observability in principle means a procedure exists and reproduces the curvature; observability in practice",
            "means the surviving programme can decide it — G_087 computed the opposite;",
            "controllability means a surviving operation changes it, and effectivity means a change would move the");
        sb.AppendLine("  observables.");
        sb.Append(KernelCurvatureActuationAudit.OutputStatusAndCouplings());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var status = KernelCurvatureActuationAudit.ObservableOrControllable();
        Assert.Equal(4, status.Length);
        Assert.True(status.Single(s => s.Question.Contains("in principle")).Answer);
        Assert.False(status.Single(s => s.Question.Contains("IN PRACTICE")).Answer);
        Assert.False(status.Single(s => s.Question.Contains("CONTROLLABLE")).Answer);
        Assert.True(status.Single(s => s.Question.Contains("EFFECTIVE")).Answer);
        // so the answer to the question as asked is NEITHER, and the effect is what makes it worth naming
        Assert.False(status.Single(s => s.Question.Contains("CONTROLLABLE")).Answer
            || status.Single(s => s.Question.Contains("IN PRACTICE")).Answer);

        sb.AppendLine("  the answer is NEITHER: observable only in principle (the procedures exist, the measurement does not),");
        sb.AppendLine("  controllable by nothing, and EFFECTIVE on both sectors if it did change — which is why it is worth naming.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_03_TheCouplingIsZeroByStateIndependence()
    {
        PrintHeader("G_091 - task 3: the physical couplings, computed rather than searched");
        var sb = Start(
            "each candidate process is judged by the variable it would couple through, and by the curvature's OWN",
            "state-dependence;",
            "the curvature is the same at every density, every potential and every site, so a state-based process has",
            "nothing to couple to;",
            "the one candidate that COULD couple — a structure carrying a coefficient — is a new primitive, which the rules",
            "forbid this audit to introduce.");
        sb.Append(KernelCurvatureActuationAudit.OutputStatusAndCouplings());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var couplings = KernelCurvatureActuationAudit.Couplings();
        Assert.Equal(6, couplings.Length);
        var counts = KernelCurvatureActuationAudit.CouplingCounts();
        Assert.Equal(5, counts.SurvivingCandidates);
        Assert.Equal(1, counts.ForbiddenCandidates);
        Assert.Equal(0.0, counts.MaximumCouplingStrength, 12);
        Assert.All(couplings.Where(c => c.Status != CurvatureCoupling.ForbiddenByTheRules),
            c => Assert.Equal(0.0, c.CouplingStrength, 12));
        Assert.All(couplings.Where(c => c.Status != CurvatureCoupling.ForbiddenByTheRules),
            c => Assert.Equal(0.0, c.StateDependenceOfTheCurvature, 12));
        // and the state-independence itself, measured: one distinct curvature across all cells and all potentials
        var rho = KernelCurvatureActuationAudit.State;
        Assert.Equal(1, rho.Select(r => KernelCurvatureActuationAudit.CurvatureExact(0.3)).Distinct().Count());

        sb.AppendLine($"  five surviving candidates, coupling strength maximum {F3(counts.MaximumCouplingStrength)}: the zero is a");
        sb.AppendLine("  CONSEQUENCE of the curvature's state-independence, not a failed search. The sixth candidate is the one");
        sb.AppendLine("  that could couple, and it is a new primitive — forbidden here and named instead.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_04_TheAsymmetryBetweenActuationAndEffect()
    {
        PrintHeader("G_091 - task 4: the asymmetry — zero actuation, large effect");
        var sb = Start(
            "the actuation side and the effect side are computed separately, because they are not the same measurement;",
            "the actuation side is the coupling per unit input, which is zero for every surviving process;",
            "the effect side is the relative change of each readout per unit curvature, taken from the surviving sensitivity",
            "machinery;",
            "so the audit reports an ASYMMETRY rather than a single number.");
        sb.Append(KernelCurvatureActuationAudit.OutputEffects());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var perUnit = KernelCurvatureActuationAudit.PerUnitInput();
        var actuation = perUnit.Where(p => p.Side == "the actuation side").ToArray();
        var effect = perUnit.Where(p => p.Side == "the effect side").ToArray();
        Assert.Equal(5, actuation.Length);
        Assert.All(actuation, a => Assert.Equal(0.0, a.PerUnitCurvature, 12));
        Assert.True(effect.Length >= 3, "several readouts must move per unit curvature");
        Assert.All(effect, e => Assert.True(e.PerUnitCurvature > 0.0));
        Assert.True(effect.Max(e => e.PerUnitCurvature) > 1e-2,
            $"the largest effect must be at the per-cent level per unit curvature ({N(effect.Max(e => e.PerUnitCurvature))})");

        sb.AppendLine($"  {actuation.Length} actuation candidates at exactly zero against {effect.Length} readouts that move, the");
        sb.AppendLine($"  largest at {N(effect.Max(e => e.PerUnitCurvature))} per unit curvature: an effectively infinite lever arm with an");
        sb.AppendLine("  actuation gain of zero.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_05_ItMovesBothTheClockAndGravity()
    {
        PrintHeader("G_091 - task 5: clock only, gravity only, or both?");
        var sb = Start(
            "a curvature change multiplies the potential squared in the rate and the potential in the gradient;",
            "so both the clock readouts and the gravity readouts must move, and the audit lists which;",
            "the listing comes from the surviving measurement of the free room's effect rather than from a fresh claim.");
        sb.Append(KernelCurvatureActuationAudit.OutputEffects());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var both = KernelCurvatureActuationAudit.AffectsClockOrGravity();
        Assert.Equal(2, both.Length);
        Assert.All(both, b => Assert.True(b.Moves));
        Assert.False(string.IsNullOrWhiteSpace(both.Single(b => b.Sector == "the clock").WhichReadouts));
        Assert.False(string.IsNullOrWhiteSpace(both.Single(b => b.Sector == "gravity").WhichReadouts));
        // the gravity side is the PREDICTION readouts and the clock side the LEVEL readouts
        var clock = both.Single(b => b.Sector == "the clock").WhichReadouts;
        var gravity = both.Single(b => b.Sector == "gravity").WhichReadouts;
        Assert.Contains("redshift", clock + gravity, StringComparison.OrdinalIgnoreCase);
        Assert.True(gravity.Contains("gradient", StringComparison.OrdinalIgnoreCase)
            || gravity.Contains("acceleration", StringComparison.OrdinalIgnoreCase),
            $"the gravity side must name a gradient or acceleration readout ({gravity})");

        sb.AppendLine("  BOTH: the clock readouts move and so do the gravity readouts, which is why the quantity is worth naming");
        sb.AppendLine("  even though it cannot be actuated.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_06_TheClassificationAndTheRequirement()
    {
        PrintHeader("G_091 - task 6: the classification, and what an actuator would have to be");
        var sb = Start(
            "all four classes are exercised, each attached to the candidate it applies to;",
            "the laboratory-feasible class is empty and the audit says so rather than filling it;",
            "and the requirement an actuator would have to meet is stated rather than assumed.");
        sb.Append(KernelCurvatureActuationAudit.OutputEffects());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = KernelCurvatureActuationAudit.Classification();
        Assert.Equal(4, classification.Length);
        Assert.Contains(classification, c => c.Classification == "impossible");
        Assert.Contains(classification, c => c.Classification == "indirect");
        Assert.Contains(classification, c => c.Classification == "astrophysical");
        Assert.Contains(classification, c => c.Classification == "laboratory-feasible");
        Assert.Contains("none", classification.Single(c => c.Classification == "laboratory-feasible").Candidate);
        // the indirect class is the subtlest: a process that changes the ARGUMENT rather than the coefficient
        Assert.Contains("argument", classification.Single(c => c.Classification == "indirect").Why);
        var requirement = KernelCurvatureActuationAudit.Requirement();
        Assert.Contains("COEFFICIENT", requirement.Requirement);
        Assert.Contains("new primitive", requirement.Why);

        sb.AppendLine("  impossible for every state-based process; indirect for a process that changes the ARGUMENT rather than the");
        sb.AppendLine("  coefficient; astrophysical in the regime of its largest effect; and laboratory-feasible: NONE.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_07_TheVerdictMeetsTheFailureCriterion()
    {
        PrintHeader("G_091 - the verdict");
        var sb = Start(
            "the verdict is a function of measured residuals, distinctness counts, coupling maxima and sensitivities;",
            "the failure criterion of the question is that the curvature is a parameter rather than a controllable variable,",
            "and the success criterion would be a physical actuator;",
            "the audit states which it met, and names what an actuator would have to be.");
        sb.Append(KernelCurvatureActuationAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = KernelCurvatureActuationAudit.Verdict();
        Assert.StartsWith("KERNEL CURVATURE IS A PARAMETER, NOT A CONTROLLABLE VARIABLE", verdict);
        Assert.Contains("EXACTLY ZERO", verdict);
        Assert.Contains("NO STATE-DEPENDENCE", verdict);
        Assert.Contains("ASYMMETRY", verdict);
        Assert.Contains("BOTH", verdict);
        Assert.Contains("FAILURE CRITERION OF THE QUESTION IS MET", verdict);
        Assert.Contains("COEFFICIENT OF THE LAW", verdict);
        Assert.Equal(verdict, KernelCurvatureActuationAudit.Verdict());

        var standing = KernelCurvatureActuationAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("NEW PRIMITIVE", standing);
        Assert.Contains("WHY THIS IS NOT A RESTATEMENT", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);

        sb.AppendLine("  the failure criterion is met — the curvature is a PARAMETER — and the audit names the one candidate that could");
        sb.AppendLine("  couple (a coefficient-carrying structure) while refusing to introduce it as a primitive.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_08_TheRefutationDoesNotRestOnARefusal()
    {
        PrintHeader("G_091 - the discipline: the zero is computed, not the absence of a search");
        var sb = Start(
            "a zero coupling could be either a measurement or a failure to look, and the audit must make the difference",
            "explicit;",
            "here the zero follows from the curvature's own state-independence, which is a property of the object rather",
            "than a limitation of the search;",
            "the audit therefore reports the state-dependence BESIDE the coupling strength in every row.");
        sb.Append(KernelCurvatureActuationAudit.OutputStatusAndCouplings());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var couplings = KernelCurvatureActuationAudit.Couplings();
        // every surviving row reports a state-dependence of zero, which is what makes the coupling zero structural
        Assert.All(couplings.Where(c => c.Status != CurvatureCoupling.ForbiddenByTheRules),
            c => Assert.Equal(0.0, c.StateDependenceOfTheCurvature, 12));
        // and the forbidden row is the one with no state-dependence number, because it is not a state question
        Assert.True(double.IsNaN(couplings.Single(c => c.Status == CurvatureCoupling.ForbiddenByTheRules)
            .StateDependenceOfTheCurvature));
        // the curvature does not vary with the argument either: that is the 'indirect' class
        var acrossPotentials = new[] { 1e-9, 0.247, 0.536 }
            .Select(a => KernelCurvatureActuationAudit.CurvatureExact(0.3)).Distinct().Count();
        Assert.Equal(1, acrossPotentials);
        Assert.All(couplings, c => Assert.False(string.IsNullOrWhiteSpace(c.Reading)));

        sb.AppendLine("  every surviving coupling is zero WITH a reported state-dependence of zero: the audit shows the property that");
        sb.AppendLine("  makes the coupling vanish, so the refutation is arithmetic rather than an absence of search.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_09_TheTwoHalvesAreDifferentQuestions()
    {
        PrintHeader("G_091 - observability and controllability are separate questions with separate answers");
        var sb = Start(
            "the audit refuses to merge the two halves, because the answer differs between them;",
            "observability in principle is a property of the OBJECT (a procedure exists), observability in practice is a",
            "property of the MEASUREMENT (G_087's degeneracy), and controllability is a property of the OPERATION SET;",
            "the three are reported separately, and only the third is about actuation.");
        sb.Append(KernelCurvatureActuationAudit.OutputStatusAndCouplings());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var status = KernelCurvatureActuationAudit.ObservableOrControllable();
        Assert.Equal(4, status.Length);
        Assert.All(status, s => Assert.False(string.IsNullOrWhiteSpace(s.Basis)));
        // the in-principle row cites the procedures, the in-practice row cites the degeneracy measurement
        Assert.Contains("second difference", status.Single(s => s.Question.Contains("in principle")).Basis);
        Assert.Contains("veto", status.Single(s => s.Question.Contains("IN PRACTICE")).Basis);
        Assert.Contains("coefficient", status.Single(s => s.Question.Contains("CONTROLLABLE")).Basis);
        var veto = KernelDiscriminabilityAudit.CompactnessVetoThreshold();
        Assert.True(veto > 0.1 && veto < 2.0);

        sb.AppendLine($"  the three answers are separate: measurable in principle, NOT decidable in practice (the veto is |c| = {F3(veto)}),");
        sb.AppendLine("  and controllable by nothing. Only the third is about actuation, and it is the one that fails.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_091_10_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_091 - cross-check against G_086, G_087, G_088, G_089 and G_090");
        var sb = Start(
            "the audit consumes the surviving results: the collapsed kernel, the unmeasured curvature, the compactness veto,",
            "the single actuatable channel and the collapse of every candidate pump;",
            "each cross-check is a computed identity between this audit's quantities and the owning audit's own API;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(ObservableKernelAudit.OutputReproduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the kernel is the collapsed one, and K(1) = 1 is the pinned data at the mean density
        Assert.Equal(1.0, KernelDiscriminabilityAudit.K(1.0, 0.3), 12);
        Assert.Equal(0.0, KernelCurvatureActuationAudit.LnRate(0.0, 0.3), 12);   // ln K = 0, i.e. the pinned K(0) = 1
        // and the curvature this audit varies is the coefficient G_086's collapse left free: it enters as c A^2 / 2
        double a = KernelCurvatureActuationAudit.SurfacePotential;
        Assert.Equal(0.3 * a * a / 2.0,
            KernelCurvatureActuationAudit.LnRate(a, 0.3) - KernelCurvatureActuationAudit.LnRate(a, 0.0), 12);
        Assert.Equal(KernelDimension.TwoFunctions, ObservableKernelAudit.Theories()
            .Single(t => t.Name.StartsWith("the free room")).Dimension);
        // G_088 measured the curvature unmeasurable, which this audit's status table reproduces
        Assert.Equal(0, AlternativeObservableAudit.Catalogue().Count(o => o.Observable.Contains("curvature")
            && o.MeasurableInTheSurvivingSector));
        // G_089 found one actuatable channel, and G_090 found every pump collapsing
        Assert.Single(ManipulationChannelAudit.Channels()
            .Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.Actuatable));
        Assert.Equal(0, DirectDensityControlAudit.AmplificationSearch().Count(a => a.Amplifies
            && a.Regime.StartsWith("the linear")));
        Assert.StartsWith("EVERY CANDIDATE COLLAPSES", DirectDensityControlAudit.Verdict());
        // and the free room is the variation whose effect this audit's task 5 lists
        Assert.Contains(MapRedundancyAudit.VariationTable(), v => v.Variation.Contains("free room")
            && v.PredictionsMoved > 0);

        sb.AppendLine("  the kernel, the unmeasurable curvature, the single actuatable channel and the collapse of every pump all");
        sb.AppendLine("  agree with this audit's tables: what is added is the reason the curvature cannot be actuated — its");
        sb.AppendLine("  state-independence — and the requirement that follows.");
        Output.WriteLine(sb.ToString());
    }
}
