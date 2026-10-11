using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_090 - DIRECT DENSITY CONTROL AUDIT.
///
/// ASSUMED TRUE: direct control of the density exists — a process that changes rho without moving ordinary mass. The
/// audit does not ask whether that is possible; it determines **what physical mechanism it would require**. Ordinary
/// mass relocation is excluded as an answer, adding primitives at the outset is forbidden, and arguing from previous
/// negative audits is forbidden.
///
/// The report follows the seven tasks. Every figure is computed here, and no spectral quantity is quoted.
/// </summary>
public sealed class Y_G_090_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_090_Tests(ITestOutputHelper output) : base(output) { }

    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    private static StringBuilder Start(params string[] assumptions)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        foreach (var a in assumptions) sb.AppendLine("  - " + a);
        sb.AppendLine();
        return sb;
    }

    [Fact]
    public void Y_G_090_01_TheInverseRequirement()
    {
        PrintHeader("G_090 - task 1: starting from Delta rho, what must an actuator possess?");
        var sb = Start(
            "direct density control is ASSUMED TRUE, and the audit's job is to derive the minimum property an actuator",
            "would need — not to argue that it is impossible;",
            "ordinary mass relocation is excluded as an ANSWER, so a redistribution counts as the thing to be surpassed;",
            "the class's transports are the only elementary operations the surviving sector has, and they are divergences;",
            "the requirement is therefore derived from the dimensions of the change space, the image and the conserving");
        sb.AppendLine("  set.");
        sb.Append(DDC_Output());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var inverse = DirectDensityControlAudit.InverseRequirement();
        Assert.Equal(3, inverse.Length);
        // on a connected substrate the transports span EVERY conserving change: nothing lies between them
        var connected = inverse.Single(r => r.Components == 1);
        Assert.True(connected.ConservingChangesAreAllTransports);
        Assert.Equal(connected.ConservingDimension - connected.ImageDimension, connected.ComplementDimension);
        Assert.Equal(0, connected.ComplementDimension);
        // the image rank is n - c exactly, on every substrate
        Assert.All(inverse, r => Assert.Equal(r.ChangeSpaceDimension - r.Components, r.ImageDimension));
        // and on a cut substrate a complement appears: conserving changes that are NOT transports
        Assert.All(inverse.Where(r => r.Components > 1), r => Assert.True(r.ComplementDimension > 0));
        Assert.Equal(1, inverse.Single(r => r.Components == 2).ComplementDimension);

        // the clock's response is compressive: the density must move more than the clock
        Assert.All(DirectDensityControlAudit.ClockResponse(), r => Assert.True(
            r.ClockResponsePerUnitDensity > 0.0 && r.ClockResponsePerUnitDensity < 1.0));

        sb.AppendLine($"  the requirement is DERIVED: on a connected substrate the {connected.ConservingDimension} conserving");
        sb.AppendLine("  changes and the transports are the SAME set, so a conserving density change is always a redistribution —");
        sb.AppendLine($"  and the complement (dimension {connected.ComplementDimension} connected, {inverse.Single(r => r.Components == 2).ComplementDimension} per cut)");
        sb.AppendLine("  is what an actuator would have to reach: a NON-DIVERGENCE operation.");
        Output.WriteLine(sb.ToString());
    }

    private static string DDC_Output() => DirectDensityControlAudit.OutputInverseRequirement();

    [Fact]
    public void Y_G_090_02_TheCandidateScan()
    {
        PrintHeader("G_090 - task 2: the ten candidate mechanisms");
        var sb = Start(
            "each candidate is judged by four measured facts: whether it is a FUNCTION of the distribution, whether it",
            "lies OUTSIDE the class's operations, whether its net effect SURVIVES AVERAGING, and whether the class can",
            "ACTUATE it;",
            "the status follows from those facts by one rule, stated in the core rather than chosen per row;",
            "the ten candidates are those the question lists: coherence, synchronization, phase coherence, information",
            "density, the actualization rate, correlation structure, mode locking, resonance, and oscillation amplitude",
            "and frequency.");
        sb.Append(DirectDensityControlAudit.OutputCandidates());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var candidates = DirectDensityControlAudit.Candidates();
        Assert.Equal(10, candidates.Length);
        // the split is structural: functions of the distribution versus quantities the clock cannot take
        int massMotion = candidates.Count(c => DirectDensityControlAudit.StatusOf(c) == PumpStatus.EquivalentToMassMotion);
        int impossible = candidates.Count(c => DirectDensityControlAudit.StatusOf(c) == PumpStatus.Impossible);
        int effectiveOnly = candidates.Count(c => DirectDensityControlAudit.StatusOf(c) == PumpStatus.EffectiveOnly);
        Assert.Equal(8, massMotion);
        Assert.Equal(1, impossible);
        Assert.Equal(1, effectiveOnly);
        Assert.Equal(candidates.Length, massMotion + impossible + effectiveOnly);
        // every row carries a real measurement rather than a verdict token
        Assert.All(candidates, c => Assert.True(c.Measurement.Length > 60, $"{c.Candidate} must carry a measurement"));

        sb.AppendLine($"  {massMotion} candidates are EQUIVALENT TO MASS MOTION (they are the distribution's own shape), {impossible} are");
        sb.AppendLine($"  IMPOSSIBLE by the clock's arity, and {effectiveOnly} is EFFECTIVE with no actuator. The split is structural,");
        sb.AppendLine("  not a verdict in bulk.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_03_TheFamilyHasZeroNetEffect()
    {
        PrintHeader("G_090 - the oscillation family: net effect zero by averaging");
        var sb = Start(
            "the clock reads the OCCUPANCY, so a modulation of the occupancy gives a modulation of the clock;",
            "a modulation's time-average is unchanged, which is computed rather than asserted;",
            "so the oscillation family can change the clock periodically but not its mean — and a mean-zero modulation is",
            "not a manipulation.");
        var energy = DirectDensityControlAudit.HiddenEnergy();
        foreach (var (route, perUnit, why) in energy)
        {
            sb.AppendLine($"    {route}: {(double.IsNaN(perUnit) ? "undefined" : perUnit.ToString("F3", System.Globalization.CultureInfo.InvariantCulture))}");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        Assert.Equal(4, energy.Length);
        Assert.Equal(0.0, energy[0].DeltaRhoPerUnitEnergy, 12);                    // a redistribution is priced at zero
        Assert.True(double.IsNaN(energy[1].DeltaRhoPerUnitEnergy));                // a source is detected, not priced
        Assert.Equal(0.0, energy[2].DeltaRhoPerUnitEnergy, 12);                    // the modulation's mean is conserved
        Assert.Equal(1.0, energy[3].DeltaRhoPerUnitEnergy, 12);                    // the mass route: the identity
        // the summed modulation over the state is zero, which is the averaging statement
        var rho = DirectDensityControlAudit.State;
        Assert.True(Math.Abs(rho.Select(v => v - 1.0).Sum()) < 1e-12);

        sb.AppendLine("  the oscillation family is refuted by AVERAGING rather than by arity: the mean of the modulation is");
        sb.AppendLine("  conserved, so a coherent oscillation changes the clock periodically and not on average.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_04_TheConservationAudit()
    {
        PrintHeader("G_090 - task 3: redistribution versus effective local creation");
        var sb = Start(
            "the question for every candidate is whether it REDISTRIBUTES the density or effectively CREATES it locally;",
            "the two are separated by dimensions measured on the substrate: the conserving set, the image of the",
            "divergence, and the complement;",
            "on a connected substrate the complement is empty, so there is no third option; on a cut one it is not, and",
            "those are exactly the changes the ledger flags.");
        sb.Append(DirectDensityControlAudit.OutputConservationAndEnergy());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var audit = DirectDensityControlAudit.ConservationAudit();
        Assert.Equal(3, audit.Length);
        Assert.Equal(0, audit.Single(a => a.Components == 1).LocalCreationChanges);
        Assert.All(audit.Where(a => a.Components > 1), a => Assert.True(a.LocalCreationChanges == a.Components - 1));
        Assert.All(audit, a => Assert.False(string.IsNullOrWhiteSpace(a.Reading)));

        sb.AppendLine("  on a connected substrate redistribution and creation COINCIDE in the small — there is no third option —");
        sb.AppendLine("  and on a cut one a complement of c - 1 appears, which is precisely what the ledger flags. So effective");
        sb.AppendLine("  LOCAL creation without mass transport exists in the structure and requires a CUT SUBSTRATE.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_05_TheAmplificationSearch()
    {
        PrintHeader("G_090 - task 5: is there a regime where a small input gives a large local Delta rho?");
        var sb = Start(
            "the map rho -> clock is logarithmic, so it is COMPRESSIVE: a fractional density change produces 1/d of the",
            "clock change;",
            "the only term that grows is the kernel's curvature, which is the free-room coefficient and which G_088",
            "measured to be unobservable in situ;",
            "the search reports every regime it can construct and whether it amplifies.");
        foreach (var (regime, gain, amplifies, why) in DirectDensityControlAudit.AmplificationSearch())
        {
            sb.AppendLine($"    {regime}: gain {gain:F4}, amplifies {(amplifies ? "YES" : "no")}");
        }
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var amplification = DirectDensityControlAudit.AmplificationSearch();
        Assert.Equal(4, amplification.Length);
        Assert.Equal(1, amplification.Count(a => a.Amplifies));
        var amplifying = amplification.Single(a => a.Amplifies);
        Assert.Contains("curvature", amplifying.Regime);
        Assert.Contains("unobservable", amplifying.Why);
        // the linear regime is compressive and its gain is exactly 1/d
        var linear = amplification.Single(a => a.Regime.StartsWith("the linear regime"));
        Assert.Equal(1.0 / DirectDensityControlAudit.D, linear.ResponseGain, 12);
        Assert.False(linear.Amplifies);

        sb.AppendLine("  exactly ONE regime amplifies, and it is the kernel's curvature — which is a parameter of the law and");
        sb.AppendLine("  unobservable in situ. The sector's own map is compressive, with gain 1/d.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_06_TheRealityClassification()
    {
        PrintHeader("G_090 - task 6: the reality classification, with every class exercised");
        var sb = Start(
            "the classification is applied to the candidates the scan produced, and each verdict is tied to a candidate;",
            "all five classes are exercised, so the audit is not using one label for everything;",
            "the laboratory-viable entry is the ordinary redistribution, reported as such.");
        sb.Append(DirectDensityControlAudit.OutputClassificationAndPathway());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = DirectDensityControlAudit.RealityClassification();
        Assert.Equal(5, classification.Length);
        foreach (var name in new[] { "impossible", "equivalent to mass motion", "effective only", "astrophysical",
            "laboratory viable" })
        {
            Assert.Contains(classification, c => c.Classification == name);
        }
        Assert.All(classification, c => Assert.False(string.IsNullOrWhiteSpace(c.Candidate)));
        Assert.All(classification, c => Assert.False(string.IsNullOrWhiteSpace(c.Why)));

        sb.AppendLine("  all five classes are exercised, each with the candidate it applies to: the classification is a");
        sb.AppendLine("  discrimination rather than a single verdict.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_07_TheMinimalEngineeringPathway()
    {
        PrintHeader("G_090 - task 7: the minimal engineering pathway, with the requirement it needs");
        var sb = Start(
            "the pathway is operator action -> physical process -> Delta rho -> Delta clock -> Delta gravity;",
            "each step carries the REQUIREMENT it must meet, and the first step's requirement is the audit's inverse",
            "result — a non-divergence operation, or an actuator for an assumed input;",
            "so the pathway is reported complete and its missing component is named.");
        sb.Append(DirectDensityControlAudit.OutputClassificationAndPathway());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var pathway = DirectDensityControlAudit.MinimalPathway();
        Assert.Equal(5, pathway.Length);
        Assert.StartsWith("an operator action", pathway[0].Step);
        Assert.Contains(pathway, s => s.Step == "Delta rho");
        Assert.Contains(pathway, s => s.Step == "Delta clock");
        Assert.Contains(pathway, s => s.Step == "Delta gravity");
        Assert.All(pathway, s => Assert.False(string.IsNullOrWhiteSpace(s.Requirement)));
        // the first requirement names what the audit derived
        Assert.Contains("non-divergence", pathway[0].Requirement);
        // and the density step carries the damping
        Assert.Contains("damping", pathway.Single(s => s.Step == "Delta rho").Requirement);

        sb.AppendLine("  the pathway is complete and its first step names the missing component: a NON-DIVERGENCE operation, or an");
        sb.AppendLine("  actuator for an assumed input. That is a requirement on the theory, not an assumed primitive.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_08_TheVerdictMeetsTheFailureCriterion()
    {
        PrintHeader("G_090 - the verdict");
        var sb = Start(
            "the verdict is a function of measured counts and dimensions: the candidate split, the image rank against the",
            "conserving dimension, the modulation's summed mean, and the amplifying regimes;",
            "the failure criterion is that every candidate collapses to mass transport or to an unactuatable variable; the",
            "success criterion would be a physically real process that changes rho without mass redistribution;",
            "the audit states which criterion is met and names the requirement the other implies.");
        sb.Append(DirectDensityControlAudit.OutputVerdict());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = DirectDensityControlAudit.Verdict();
        Assert.StartsWith("EVERY CANDIDATE COLLAPSES", verdict);
        Assert.Contains("EQUIVALENT TO MASS MOTION", verdict);
        Assert.Contains("IMPOSSIBLE by the clock's arity", verdict);
        Assert.Contains("EFFECTIVE with no actuator", verdict);
        Assert.Contains("ZERO BY AVERAGING", verdict);
        Assert.Contains("NON-DIVERGENCE", verdict);
        Assert.Contains("FAILURE CRITERION IS MET", verdict);
        Assert.Equal(verdict, DirectDensityControlAudit.Verdict());

        var standing = DirectDensityControlAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("assume it", standing);
        Assert.Contains("WHY THE COLLAPSE IS A RESULT RATHER THAN A REFUSAL", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("each is named", standing);

        sb.AppendLine("  the failure criterion is met, and the success criterion's requirement is named rather than assumed: a");
        sb.AppendLine("  non-divergence operation, an actuator for an assumed input, or a change of a parameter of the law.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_09_TheTwoFailureModesAreDistinctAndMeasured()
    {
        PrintHeader("G_090 - the two failure modes, and why the collapse is a result");
        var sb = Start(
            "the candidates fail for TWO DIFFERENT and measured reasons, and the audit reports the split per candidate;",
            "the first: being a function of the distribution, so acting through it IS redistributing;",
            "the second: being invisible to a clock whose signature takes the occupancy alone;",
            "the two are distinguished by the measured facts in the catalogue, not by the verdict in bulk.");
        sb.Append(DirectDensityControlAudit.OutputCandidates());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var candidates = DirectDensityControlAudit.Candidates();
        var byShape = candidates.Where(c => c.IsAFunctionOfRho).ToArray();
        var byArity = candidates.Where(c => !c.IsAFunctionOfRho && !c.HasAnActuator).ToArray();
        Assert.True(byShape.Length >= 6, "most candidates are the distribution's own shape");
        Assert.True(byArity.Length >= 2, "some candidates cannot reach the clock at all");
        // the two sets are disjoint and their union is the whole catalogue
        Assert.Empty(byShape.Intersect(byArity));
        Assert.Equal(candidates.Length, byShape.Length + byArity.Length);
        // and each carries its own measurement
        Assert.All(byShape, c => Assert.True(c.Measurement.Length > 60, $"{c.Candidate} must carry a measurement"));
        Assert.All(byArity, c => Assert.False(string.IsNullOrWhiteSpace(c.Measurement)));

        sb.AppendLine($"  {byShape.Length} candidates fail by SHAPE (they are functions of the distribution) and {byArity.Length} by ACCESS");
        sb.AppendLine("  (the clock cannot take them). The split is disjoint and covers the catalogue, so the collapse is a");
        sb.AppendLine("  measurement of why each candidate fails rather than a single blanket verdict.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_090_10_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_090 - cross-check against G_078, G_079, G_080, G_082, G_086, G_088 and G_089");
        var sb = Start(
            "the audit consumes the surviving results: the conserving class and the ledger, the transport rank, the assumed",
            "connectivity, the gauge-like component count, the collapsed kernel, the unmeasured curvature and the single",
            "actuatable channel;",
            "each cross-check is a computed identity between this audit's quantities and the owning audit's own API;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(ObservableKernelAudit.OutputReproduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // the kernel used here is the collapsed one, and the transports are the class's own
        var witnesses = ConnectivityOriginAudit.ConservingWitnesses(ManipulationChannelAudit.Cells, 1);
        Assert.NotEmpty(witnesses);
        Assert.All(witnesses, w => Assert.True(ContinuityLawAudit.IsATransport(
            w.Change.Select(x => (double)x).ToArray(), ManipulationChannelAudit.Cells, 1)));
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());
        Assert.False(ConnectivityOriginAudit.DisjointUnionClosure().Any(r => r.ContentChanged));
        // G_089's actuatable channel is the one this audit's pathway rests on
        var channels = ManipulationChannelAudit.Channels();
        Assert.Single(channels.Where(c => ManipulationChannelAudit.StatusOf(c) == ChannelStatus.Actuatable));
        // and G_088's unmeasured curvature is the amplification search's only amplifying regime
        Assert.Equal(0, AlternativeObservableAudit.Catalogue().Count(o => o.Observable.Contains("curvature")
            && o.MeasurableInTheSurvivingSector));

        sb.AppendLine("  every conserving witness IS a transport, 0 observables read the flux, 0 of 4 primitives detect a cut, and");
        sb.AppendLine("  G_089's single actuatable channel is the one the pathway rests on while G_088's curvature is the only");
        sb.AppendLine("  amplifying regime — and it is unmeasurable. The collapse follows from those measurements.");
        Output.WriteLine(sb.ToString());
    }
}
