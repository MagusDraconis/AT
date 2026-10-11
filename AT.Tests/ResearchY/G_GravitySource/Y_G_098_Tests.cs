using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_098 - DIRECTED TRANSPORT AUDIT.
///
/// G_096 left the kernel's coefficient assembled from an assumed principle; G_097 measured that the evolution DIFFUSES
/// (the width grows as the square root of time), that the CENTROID SHIFT IS EXACTLY ZERO, and that 48! histories are
/// observationally identical. This suite asks the narrower question: does the surviving AT structure contain a DERIVED
/// carrier for DIRECTED propagation, or is transport independent information?
///
/// Every count, rank, dimension and multiplicity is computed on the 96-cell ring ALONE; the cubic substrate is
/// forbidden; and no new primitive, field, source term, topology axiom, action, Lagrangian, memory channel, carrier or
/// vector structure is introduced. A negative result is acceptable.
///
/// The sharpest instrument is structural rather than dynamical: whether the surviving link set REPRESENTS a direction
/// at all, since LinksOf stores each link as an ordered pair and therefore discards the orientation.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_098_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_098_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_098_01_TheCarrierInventory()
    {
        PrintHeader("G_098 - tasks 1 and 2: every structure that could carry a propagating quantity");
        var sb = Start(
            "the candidates cover state, excess, phase, label and a conserved quantity, plus the two directed objects the",
            "sector happens to have;",
            "each carries the four measures the task names: excess conservation, centroid motion, directed transport and",
            "diffusion width growth;",
            "EVERY count is on the 96-cell ring; the cubic substrate is forbidden and is not named in the code;",
            "a candidate that cannot be a carrier is reported as one rather than quietly dropped.");
        sb.Append(DirectedTransportAudit.OutputCarriers());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var carriers = DirectedTransportAudit.Carriers();
        Assert.Equal(7, carriers.Length);
        var counts = DirectedTransportAudit.CarrierCounts();
        Assert.Equal(7, counts.Carriers);
        Assert.Equal(3, counts.Directed);
        // the state-like carriers are conserved, unmoved and diffusive
        var stateLike = carriers.Where(c => c.IsAStateLike && c.IsDerived).ToArray();
        Assert.Equal(3, stateLike.Length);
        Assert.All(stateLike, c => Assert.True(c.ExcessConserved));
        Assert.All(stateLike, c => Assert.Equal(0.0, c.CentroidShift, 12));
        Assert.All(stateLike, c => Assert.False(c.DirectedTransport));
        // the phase is not a carrier at all
        var phase = carriers.Single(c => c.Carrier.Contains("phase"));
        Assert.False(phase.IsAStateLike);
        Assert.Equal(0, phase.ReadoutsCarried);
        // and the two directed objects are read by 0 and 2 observables respectively
        Assert.Equal(0, carriers.Single(c => c.Carrier.Contains("flux")).ReadoutsCarried);
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());

        sb.AppendLine($"  {counts.Carriers} candidates, {counts.Derived} derived and {counts.Directed} directed — and every directed one");
        sb.AppendLine("  is a coordinate, a derived output read by nothing, or a static field.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_02_TheSurvivingOperatorDiffusesWithNoShift()
    {
        PrintHeader("G_098 - what the surviving operator does, against what a drift does");
        var sb = Start(
            "the same pattern G_097 tracked is used, so the two audits measure the same object;",
            "the law is run twice: once with the surviving symmetric operator alone, once with a DRIFT supplied from",
            "outside, so the difference between them is exactly what the orientation buys;",
            "the drift step needs an orientation, so it is applied on an index basis and the audit states that this is",
            "SUPPLIED rather than derived;",
            "the step size is the same stated convention G_097 used.");
        sb.Append(DirectedTransportAudit.OutputRatio());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var law = DirectedTransportAudit.Propagate(Directed: false);
        var drift = DirectedTransportAudit.Propagate(Directed: true);
        // the surviving operator: conserved excess, growing width, ZERO shift at every tick
        Assert.All(law, r => Assert.Equal(0.0, r.CentroidShift, 12));
        Assert.All(law, r => Assert.Equal(law[0].Total, r.Total, 12));
        Assert.True(law[^1].Width > law[0].Width);
        // the same law with a drift supplied: the shift appears at once
        Assert.True(Math.Abs(drift[^1].CentroidShift) > 1e-6);
        Assert.All(drift, r => Assert.Equal(law[0].Total, r.Total, 12));
        Assert.True(Math.Abs(drift[^1].CentroidShift) > Math.Abs(law[^1].CentroidShift));
        // and the drift's shift is exactly the drift times the ticks, measured rather than assumed
        Assert.Equal(8 * 0.1, Math.Abs(drift[^1].CentroidShift), 9);

        sb.AppendLine($"  the surviving operator: the width grows {F3(law[0].Width)} -> {F3(law[^1].Width)} while the centroid shift is");
        sb.AppendLine($"  {N(law[^1].CentroidShift)} at every one of {law.Length - 1} ticks. A drift supplied from outside moves it by");
        sb.AppendLine($"  {F3(Math.Abs(drift[^1].CentroidShift))} over the same window — and it is the ORIENTATION that buys that difference.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_03_TheTransportDiffusionRatio()
    {
        PrintHeader("G_098 - task 4: T = |centroid shift| / width growth");
        var sb = Start(
            "the ratio is computed per mode, and the mode that needs an orientation is MARKED as supplied;",
            "a ratio of zero is a structural statement and not a numerical accident: the numerator never leaves zero;",
            "the maximum over ALL modes and the maximum over the SURVIVING modes are reported separately, because the",
            "task's success criterion is about the derived ones;",
            "the symmetric transfer is included so the class's operations are not misrepresented as biased.");
        sb.Append(DirectedTransportAudit.OutputRatio());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = DirectedTransportAudit.TransportDiffusionRatio();
        Assert.Equal(3, rows.Length);
        var law = rows.Single(r => r.Mode.Contains("operator alone"));
        var symmetric = rows.Single(r => r.Mode.Contains("SYMMETRIC"));
        var supplied = rows.Single(r => r.OrientationSupplied);
        // the surviving modes have zero numerator and a growing width
        Assert.Equal(0.0, law.CentroidShift, 12);
        Assert.Equal(0.0, law.Ratio, 12);
        Assert.True(law.WidthGrowth > 1.0);
        Assert.Equal(0.0, symmetric.CentroidShift, 12);
        Assert.Equal(0.0, symmetric.Ratio, 12);
        // the supplied mode has a real ratio
        Assert.True(supplied.CentroidShift > 1e-6);
        Assert.True(supplied.Ratio > 0.1);
        // and the two maxima
        var max = DirectedTransportAudit.MaxRatio();
        Assert.Equal(0.0, max.MaxOverSurviving, 12);
        Assert.True(max.MaxOverAll > 0.1);

        sb.AppendLine($"  T = {F3(law.Ratio)} for the surviving operator and {F3(symmetric.Ratio)} for a symmetric transfer, while the supplied");
        sb.AppendLine($"  drift reaches T = {F3(supplied.Ratio)}. MAX T over the surviving modes is {F3(max.MaxOverSurviving)}; over all modes it is");
        sb.AppendLine($"  {F3(max.MaxOverAll)} — and the difference between them is the orientation.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_04_TheDirectionalBiasIsZero()
    {
        PrintHeader("G_098 - task 5: does anything select a preferred direction?");
        var sb = Start(
            "FOUR independent instruments are used, so the conclusion does not rest on one of them: the operator's",
            "matrix asymmetry, the symbol's evenness under k -> -k, the degree's uniformity, and the orientation the link",
            "set actually STORES;",
            "the last is the sharpest, because it is a property of the representation and not of a simulation;",
            "the missing information is quantified in bits, with the minimum and the maximum stated.",
            "nothing is added to the link set to make it directed.");
        sb.Append(DirectedTransportAudit.OutputBiasAndLabels());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var bias = DirectedTransportAudit.DirectionalBias();
        Assert.Equal(4, bias.Length);
        // NO instrument selects a direction
        Assert.All(bias, b => Assert.False(b.SelectsADirection));
        // the operator is symmetric to the rounding
        Assert.True(bias.Single(b => b.Instrument.Contains("matrix")).Measured < 1e-12);
        // the dispersion is even
        Assert.True(bias.Single(b => b.Instrument.Contains("evenness")).Measured < 1e-12);
        // the degree is uniform and equal to two
        Assert.Equal(2.0, bias.Single(b => b.Instrument.Contains("uniformity")).Measured, 12);
        // and the link set stores no orientation: every pair is written with a <= b
        Assert.Equal(0.0, bias.Single(b => b.Instrument.Contains("orientation")).Measured, 12);
        Assert.All(DirectedTransportAudit.Links, l => Assert.True(l.A <= l.B));
        // the information accounting
        var orientation = DirectedTransportAudit.OrientationInformation();
        Assert.Equal(96, orientation.Links);
        Assert.Equal(1, orientation.GlobalChiralityBits);
        Assert.Equal(96, orientation.IndependentOrientationBits);
        Assert.Equal(0, orientation.SuppliedByTheSector);

        sb.AppendLine($"  all {bias.Length} instruments report NO preferred direction: |L - L^T| = 0, the symbol is even, every cell has");
        sb.AppendLine($"  {F3(bias.Single(b => b.Instrument.Contains("uniformity")).Measured)} links, and every link is stored as an ordered pair with a <= b.");
        sb.AppendLine($"  Directed transport needs {orientation.GlobalChiralityBits} bit of chirality (or {orientation.IndependentOrientationBits} bits link by link); the");
        sb.AppendLine($"  sector supplies {orientation.SuppliedByTheSector}.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_05_WithoutLabelsAndOrdering()
    {
        PrintHeader("G_098 - task 6: remove all labels and external ordering");
        var sb = Start(
            "the transport MEASURE is itself a function of the labelling - a centroid needs an index - so the audit",
            "measures what is left once the labels are gone;",
            "value-only quantities are recomputed as order statistics, which need no metric on the sites;",
            "a quantity that becomes UNDEFINED is reported as undefined and not as zero;",
            "no ordering or index structure is added back.");
        sb.Append(DirectedTransportAudit.OutputBiasAndLabels());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var quantities = DirectedTransportAudit.WithoutLabels();
        Assert.Equal(4, quantities.Length);
        // the value-only quantities survive
        Assert.True(quantities.Single(q => q.Quantity == "the peak").Survives);
        Assert.True(quantities.Single(q => q.Quantity == "the width of the excess").Survives);
        // the transport quantities do not
        Assert.False(quantities.Single(q => q.Quantity.Contains("surviving operator")).Survives);
        Assert.False(quantities.Single(q => q.Quantity.Contains("chosen drift")).Survives);
        Assert.Contains("UNDEFINED", quantities.Single(q => q.Quantity.Contains("surviving operator")).Reading);
        // and the drift's shift is the one that really disappears
        var driftRow = quantities.Single(q => q.Quantity.Contains("chosen drift"));
        Assert.True(driftRow.WithLabels > 1e-6);
        Assert.Equal(0.0, driftRow.WithoutLabels, 12);
        // the remaining transport information is none
        var remaining = DirectedTransportAudit.RemainingTransportInformation();
        Assert.Equal(4, remaining.Quantities);
        Assert.Equal(2, remaining.Surviving);
        Assert.Equal(0, remaining.TransportInformationRemaining);

        sb.AppendLine($"  of {remaining.Quantities} propagation quantities, {remaining.Surviving} survive the removal of the labels and");
        sb.AppendLine($"  {remaining.TransportInformationRemaining} of them is a transport quantity: the measure of transport does not survive its own coordinate.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_06_TheReductionWithDiffusionOnly()
    {
        PrintHeader("G_098 - task 7: reconstruct every apparent-motion observable with diffusion only");
        var sb = Start(
            "the decisive count is how many observables read a DIRECTED object at all, because that is what the",
            "reduction would lose;",
            "the sector's one directed object is the flux, and its reader count is measured rather than assumed;",
            "the loss without the labelling is reported separately, since it is a coordinate loss and not a transport",
            "loss;",
            "no observable is claimed as reconstructed without naming why.");
        sb.Append(DirectedTransportAudit.OutputReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var reduction = DirectedTransportAudit.ReductionCounts();
        Assert.Equal(8, reduction.Readouts);
        // NO readout reads a directed object
        Assert.Equal(0, reduction.ReadingADirectedObject);
        // so the loss with diffusion alone is zero
        Assert.Equal(0, reduction.Loss);
        // and the only loss is the labelling, which is a coordinate
        Assert.Equal(4, reduction.LossWithoutTheLabelling);
        // the flux's reader count is the reason, and it is measured by the owning audit
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());
        Assert.All(DirectedTransportAudit.ReductionTest(), r => Assert.False(r.ReadsADirectedObject));
        Assert.All(DirectedTransportAudit.ReductionTest(), r => Assert.True(r.ReconstructedByDiffusion));

        sb.AppendLine($"  {reduction.Readouts} readouts, {reduction.ReadingADirectedObject} of them reading a directed object, so the loss with diffusion");
        sb.AppendLine($"  alone is {reduction.Loss}; the only loss is {reduction.LossWithoutTheLabelling} when the labelling is removed — a coordinate loss, not a");
        sb.AppendLine("  transport loss. Directed transport costs no observable.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_07_TheClassification()
    {
        PrintHeader("G_098 - the classification of every candidate");
        var sb = Start(
            "each candidate is classified by its own measurement, not by the audit's preference;",
            "a carrier that is directed AND not derived is ASSUMED, because the direction is supplied;",
            "a directed carrier that IS derived but propagates nothing is BOUNDARY, because it is a static output;",
            "a state-like carrier that diffuses is EMERGENT, following G_097's use of the class for a phenomenon.");
        sb.Append(DirectedTransportAudit.OutputReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var classification = DirectedTransportAudit.Classification();
        Assert.Equal(7, classification.Length);
        var byCarrier = classification.ToDictionary(c => c.Carrier, c => c.Status);
        // the state-like carriers are EMERGENT
        Assert.Equal(TransportStatus.Emergent, byCarrier["the state (the occupation)"]);
        Assert.Equal(TransportStatus.Emergent, byCarrier["the excess over the flat background"]);
        Assert.Equal(TransportStatus.Emergent, byCarrier["a conserved quantity (the total count)"]);
        // the label is ASSUMED: the direction it carries is supplied
        Assert.Equal(TransportStatus.Assumed, byCarrier["the label (the cell index)"]);
        // the two derived directed objects propagate nothing, so they are BOUNDARY
        Assert.Equal(TransportStatus.Boundary, byCarrier["the flux (the transport's own variable)"]);
        Assert.Equal(TransportStatus.Boundary, byCarrier["the acceleration (the potential's gradient)"]);
        // and the phase is not a carrier
        Assert.Equal(TransportStatus.Boundary, byCarrier["the phase (the theta sector)"]);

        foreach (var (carrier, status, _) in classification)
            sb.AppendLine($"  {carrier}: {status}");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_08_TheCriteriaAndTheVerdict()
    {
        PrintHeader("G_098 - the criteria, the final verdict and its numbers");
        var sb = Start(
            "the task states a success and a failure criterion, so both are decided;",
            "every clause of the verdict is a measured count, a measured asymmetry or a measured ratio (G_027);",
            "no literal reaches the criteria or the verdict;",
            "a negative result is acceptable, and the verdict says so rather than hedging.");
        sb.Append(DirectedTransportAudit.OutputReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var criteria = DirectedTransportAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria.Single(c => c.Criterion.StartsWith("SUCCESS")).Outcome);
        Assert.Equal("MET", criteria.Single(c => c.Criterion.StartsWith("FAILURE")).Outcome);
        Assert.Equal("CONFIRMED", criteria.Single(c => c.Criterion.StartsWith("and the missing")).Outcome);
        var verdict = DirectedTransportAudit.Verdict();
        Assert.StartsWith("DIRECTED TRANSPORT IS ASSUMED", verdict);
        Assert.Contains("NOT EVEN REPRESENT A DIRECTION", verdict);
        Assert.Contains("ORDERED PAIR", verdict);
        Assert.Contains("ONE BIT", verdict);
        // the verdict's numbers are the audit's own measurements
        Assert.Contains(F3(DirectedTransportAudit.MaxRatio().MaxOverSurviving), verdict);
        Assert.Contains(N(DirectedTransportAudit.Propagate(true)[^1].CentroidShift), verdict);
        Assert.Contains(DirectedTransportAudit.OrientationInformation().GlobalChiralityBits
            .ToString(System.Globalization.CultureInfo.InvariantCulture), verdict);
        Assert.Contains(F3(DirectedTransportAudit.Propagate(false)[^1].Width
            - DirectedTransportAudit.Propagate(false)[0].Width), verdict);

        sb.AppendLine();
        sb.AppendLine("  VERDICT: " + verdict);
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_098_09_TheAuditAgreesWithTheChainItConsumes()
    {
        PrintHeader("G_098 - cross-check against G_080, G_090, G_092, G_093 and G_097");
        var sb = Start(
            "the audit consumes five of the chain's results, so each is cross-checked by computed identity;",
            "G_097 supplies the diffusion and the zero centroid shift, G_090 the reach of the class's operations, G_080",
            "the generation relation's arithmetic, and G_092 the fact that no structure is actuatable and unobservable;",
            "the two audits' shared conventions are checked: the pattern and the step size;",
            "no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.Append(DirectedTransportAudit.OutputReduction());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        // G_097: the same pattern and the same diffusive law
        Assert.Equal(PatternKinematicsAudit.Pattern(), DirectedTransportAudit.Pattern());
        Assert.Equal(PatternKinematicsAudit.Step, DirectedTransportAudit.Step, 12);
        Assert.All(PatternKinematicsAudit.Propagate(), p => Assert.Equal(0.0, p.CentroidShift, 12));
        Assert.Equal(PatternKinematicsAudit.Propagate()[^1].Spread, DirectedTransportAudit.Propagate()[^1].Width, 9);
        // G_090: the class reaches 95 directions and NO operation writes the link set
        Assert.Equal(95, ActuatorVersusObservableAudit.ReachableOperations());
        Assert.Equal(0, ActuatorVersusObservableAudit.Structures()
            .Single(s => s.Name.Contains("link set")).OperationsChangingIt);
        // G_092: no structure is actuatable and unobservable
        Assert.Equal(0, ActuatorVersusObservableAudit.RoleCensus().InvisibleActuator);
        // G_080: the generation relation manufactures the ring's links by arithmetic, and its direction is arithmetic
        Assert.False(ConnectivityOriginAudit.PrimitivesSeparateConnectivity());
        Assert.Equal(96, DirectedTransportAudit.Links.Length);
        // and the link set is an UNORDERED set: the orientation is discarded in the representation
        Assert.All(DirectedTransportAudit.Links, l => Assert.True(l.A <= l.B));
        Assert.Equal(DirectedTransportAudit.Links.Length, DirectedTransportAudit.Links.Distinct().Count());

        sb.AppendLine("  the chain's numbers are reproduced: the same pattern, the same step, the same zero centroid shift and the");
        sb.AppendLine("  same width as G_097; the class's 95 reachable directions with no operation writing the link set; and the");
        sb.AppendLine("  link set's own unordered representation, which is what this audit measures the consequence of.");
        Output.WriteLine(sb.ToString());
    }
}
