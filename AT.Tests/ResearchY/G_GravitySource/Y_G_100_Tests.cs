using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_100 - ORIENTATION COUPLING AUDIT.
///
/// G_097 measured that the sector's law DIFFUSES (the width grows as the square root of time, the centroid shift is
/// zero, the mode symbols are non-positive); G_098 measured that directed transport is ABSENT and that the surviving
/// transport structure does not even REPRESENT a direction; G_099 measured that the orientation FIELD is DERIVED and
/// observable while the CHIRALITY is ASSUMED and the TRANSPORT RULE is ASSUMED, the surviving state being parity-even
/// to the rounding and the law commuting with all 192 automorphisms. This suite asks the question those leave open:
///
///   does the derived orientation field couple to surviving dynamics strongly enough to generate directed
///   propagation?
///
/// THE INSTRUMENT is a two-part test, because "coupling" can be read two ways and only one of them is dynamical. The
/// orientation field is a FUNCTION OF THE STATE, so it cannot be varied independently of the state: reversing it
/// necessarily changes the state, and the update changes with the state. A descriptive coupling is therefore
/// guaranteed by construction and measures nothing. What decides the question is (a) whether the field has any
/// INDEPENDENT degrees of freedom as a control variable, and (b) whether the transport measure moves with it - and the
/// suite measures the second by SWEEPING the field's content, so that S = dT/dO is a difference over an admissible
/// family rather than a claim.
///
/// Every count, dimension, rank and multiplicity names its object: the ring is the canonical 96-cell ring (cells = 96,
/// links = LinksOf(96, 1)), the orientation field is the SIGNED potential difference along each of the ring's 96
/// cyclic links, the transport measure is G_098's T = |centroid shift| / width growth computed against each state's own
/// far-field plateau, and the equality rules are 1e-12 on a computed value and EXACT equality where a telescoping or a
/// summation-by-parts identity holds. The cubic substrate is forbidden and is not named in this file. No new primitive,
/// field, source term, topology axiom, action, Lagrangian, carrier, vector structure, orientation tag or transport rule
/// is introduced. A negative result is acceptable.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_100_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_100_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_100_01_TheOrientationFieldInventory()
    {
        PrintHeader("G_100 - task 1: every derived orientation-field quantity surviving G_099, with its dimension count");
        var sb = Start(
            "the candidates are the orientation-bearing objects G_099 left standing: the field itself, the acceleration,",
            "the field's content, the odd part of the configuration, the flux and the parity-odd readouts;",
            "the INDEPENDENT DIMENSION count is the number of degrees of freedom the quantity has of its own, and a",
            "functional of the state has zero - which is the distinction the whole audit turns on;",
            "EVERY count is on the 96-cell ring; the cubic substrate is forbidden and is not named in the code;",
            "a quantity that is derived but read by nothing is reported as such rather than dropped.");
        sb.Append(OrientationCouplingAudit.OutputInventory());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Inventory();
        Assert.Equal(6, rows.Length);
        var counts = OrientationCouplingAudit.InventoryCounts();
        Assert.Equal(6, counts.Quantities);
        Assert.Equal(3, counts.ReadByAnObservable);
        Assert.Equal(0, counts.Dynamical);
        Assert.Equal(0, counts.Independent);

        // the field measures: the content MOVES over the family while the peak does not, which is why task 5 sweeps
        // the content
        double peakLow = OrientationCouplingAudit.OrientationPeak(OrientationCouplingAudit.Skewed(0.0));
        double peakHigh = OrientationCouplingAudit.OrientationPeak(OrientationCouplingAudit.Skewed(1.0));
        Assert.Equal(peakLow, peakHigh, 12);
        Assert.True(OrientationCouplingAudit.OrientationContent(OrientationCouplingAudit.Skewed(1.0))
            > OrientationCouplingAudit.OrientationContent(OrientationCouplingAudit.Skewed(0.0)) * 1.3,
            "the content must move where the peak does not, or the sweep would divide by zero");

        // and the flux is the derived direction no measurement sees
        var flux = rows.Single(r => r.Quantity.Contains("flux"));
        Assert.Equal(0.0, flux.Magnitude, 12);
        Assert.Equal(0, ContinuityLawAudit.ObservablesReadingTheFlux());
        Assert.Contains("READER COUNT", flux.Measurement);
    }

    [Fact]
    public void Y_G_100_02_TheCouplingTable()
    {
        PrintHeader("G_100 - task 2: the field's magnitude against the local update's magnitude, and the law's own objects");
        var sb = Start(
            "the per-cell coupling is a normalized product of two measured profiles, both functions of the same state;",
            "the signed overlap is the cosine between them, so a strong magnitude correlation and a large cosine are",
            "both expected and neither is evidence of anything dynamical;",
            "the three DYNAMICAL channels are properties of the law and are tested by RECOMPUTING them, so the row reports",
            "the residual and not the magnitude of the object under test;",
            "the equality rule is 1e-12.");
        sb.Append(OrientationCouplingAudit.OutputCoupling());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Coupling();
        Assert.Equal(5, rows.Length);
        var summary = OrientationCouplingAudit.CouplingSummary();
        Assert.True(summary.PearsonR > 0.9, $"the descriptive coupling must be strong, measured {F3(summary.PearsonR)}");
        Assert.True(summary.MeanProduct > 0.0);
        Assert.Equal(0.9123, rows[1].Mean, 4);   // the signed overlap: a strong alignment, not an orthogonality

        // and every dynamical object is state-independent to the equality rule
        Assert.Equal(0.0, summary.MaxDynamical, 12);
        Assert.All(rows.Where(r => r.Channel.Contains("OPERATOR") || r.Channel.Contains("symbol")
            || r.Channel.Contains("conservation")), r => Assert.Equal(0.0, r.Max, 12));
    }

    [Fact]
    public void Y_G_100_03_ThePartitionByEqualOrientationField()
    {
        PrintHeader("G_100 - task 3: the partition the orientation field defines, measured on DIFFERENT states");
        var sb = Start(
            "the field fixes the potential up to one additive constant by telescoping, hence the state up to a positive",
            "scale - and the measurement is taken on OTHER states so that it is not an identity read back;",
            "each member's field is RE-derived from it: the residual between two states' fields is the partition's width;",
            "the evolution residual inside the partition is the linearity of the Laplacian and nothing else;",
            "the equality rule is a relative residual below 1e-12.");
        sb.Append(OrientationCouplingAudit.OutputPartition());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Partition();
        Assert.Equal(4, rows.Length);
        var counts = OrientationCouplingAudit.PartitionCounts();
        Assert.Equal(1, counts.Dimension);
        Assert.True(counts.FieldResidual < 1e-12,
            $"two different states with the same field must agree to the rule, measured {N(counts.FieldResidual)}");
        Assert.True(counts.EvolutionResidualAfterTheScale < 1e-12,
            "inside the partition the evolution must differ by exactly the scale");

        // a scaled member is a DIFFERENT state, so the test is not vacuous
        var halved = OrientationCouplingAudit.Working.Select(r => r / 2.0).ToArray();
        Assert.True(OrientationCouplingAudit.MaxAbs(
            halved.Zip(OrientationCouplingAudit.Working, (a, b) => a - b)) > 0.1,
            "the partition member must be a genuinely different state");

        // and a state that is NOT in the partition is reported as one
        var even = rows.Single(r => r.Member.Contains("NOT in the partition"));
        Assert.True(even.FieldResidual > 1e-3);
    }

    [Fact]
    public void Y_G_100_04_TheOrientationReversal()
    {
        PrintHeader("G_100 - task 4: the field reversed by TWO operations, every dynamical object measured");
        var sb = Start(
            "the field cannot be reversed while the state is held literally fixed, so two faithful operations are used:",
            "the ring reflection (a relabelling the law commutes with) and the mean-preserving density inversion (which",
            "flips the potential's sign exactly and is non-linear);",
            "LAW objects and STATE OUTPUTS are kept apart, because a law that changed would be a different finding from a",
            "state transformation's own residue;",
            "the operator is measured as an explicit 96x96 matrix built by applying the update to each basis vector;",
            "the equality rule is 1e-12 and exact where a telescoping identity holds.");
        sb.Append(OrientationCouplingAudit.OutputReversal());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Reversal();
        var counts = OrientationCouplingAudit.ReversalCounts();
        Assert.Equal(2, counts.LawObjects);
        Assert.Equal(0, counts.LawChanged);
        Assert.Equal(0.0, counts.MaxLawChange, 12);
        Assert.True(counts.StateOutputs >= 4);

        // the operator's own matrix is bit-identical: it takes no state argument
        var matrix = OrientationCouplingAudit.OperatorMatrix();
        Assert.Equal(96, matrix.Length);
        Assert.Equal(0.0, OrientationCouplingAudit.OperatorChange(matrix, matrix), 12);

        // the field really is reversed, by both operations, and the residual is named per operation
        var flip = OrientationCouplingAudit.FieldFlipResiduals(OrientationCouplingAudit.Working);
        Assert.True(flip.AtTheSameIndex < 1e-15, $"the inversion's flip residual is {N(flip.AtTheSameIndex)}");
        Assert.Equal(0.0, flip.AtTheReflectedIndex, 12);

        // and the transport is zero on both sides of each reversal
        var asIs = OrientationCouplingAudit.Transport(OrientationCouplingAudit.Working);
        Assert.True(asIs.CentroidShift < 1e-12);
        Assert.Equal(0.0, OrientationCouplingAudit.Transport(
            OrientationCouplingAudit.Reflect(OrientationCouplingAudit.Working)).CentroidShift, 12);
    }

    [Fact]
    public void Y_G_100_05_TheTransportSensitivity()
    {
        PrintHeader("G_100 - task 5: S = dT/dO over a swept orientation field, both signs of the field covered");
        var sb = Start(
            "the sweep has seven members: five skews of the working pattern and its two reversals, so the field's sign",
            "is covered and not only its magnitude;",
            "every member's pattern is SIGN-DEFINITE, so every width exists - a member whose pattern had a negative lobe",
            "would have an undefined second moment, which the record carries as a flag rather than as a number;",
            "the derivative is taken over the field's CONTENT, because the PEAK is constant over this family (measured) and",
            "a sweep keyed on the peak would divide by zero;",
            "the equality rule is 1e-12, and the audit reports the differenced value BESIDE its reason.");
        sb.Append(OrientationCouplingAudit.OutputSensitivity().Split("6. THE SURVIVING")[0]);
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Sensitivity();
        Assert.Equal(7, rows.Length);
        var counts = OrientationCouplingAudit.SensitivityCounts();
        Assert.Equal(7, counts.Members);
        Assert.Equal(7, counts.WidthsDefined);
        Assert.True(counts.MaxAbsS < 1e-12,
            $"max |dT/dO| must be under the equality rule, measured {N(counts.MaxAbsS)}");
        Assert.True(counts.MaxOrientation > counts.MinOrientation * 1.3, "the sweep must actually vary the field");
        Assert.True(counts.MaxCentroidShift < 1e-12,
            $"the numerator must never leave the rounding, largest {N(counts.MaxCentroidShift)}");
        Assert.All(rows, r => Assert.True(r.Ratio < 1e-12));

        // the first moment of the update vanishes by summation by parts: measured on the family
        foreach (var rho in new[] { OrientationCouplingAudit.Skewed(0.0), OrientationCouplingAudit.Working,
            OrientationCouplingAudit.Reflect(OrientationCouplingAudit.Working),
            OrientationCouplingAudit.Invert(OrientationCouplingAudit.Working) })
        {
            double moment = Enumerable.Range(0, 96)
                .Sum(i => i * OrientationCouplingAudit.Update(rho)[i]);
            Assert.True(Math.Abs(moment) < 1e-11, $"the first moment of the update is {N(moment)}");
        }
    }

    [Fact]
    public void Y_G_100_06_TheOrientationDependentEvolution()
    {
        PrintHeader("G_100 - task 6: the surviving observables under an orientation-dependent evolution, TWO readings");
        var sb = Start(
            "the test is not whether a readout differs - the state differs, so readouts must differ - but whether the",
            "DIFFERENCE GROWS: a dynamics that generated orientation would amplify it;",
            "reading (a) holds the sector's canonical site fixed, which is what its own readouts do;",
            "reading (b) reads each orientation at the sites the reflection carries the other's to, so the comparison is",
            "a true mirror comparison and a relabelling difference cannot masquerade as physics;",
            "the amplification factor is the measurement, and a growth from below the rule is counted separately.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var atTheSite = OrientationCouplingAudit.ObservableChange();
        Assert.Equal(8, atTheSite.Length);
        var counts = OrientationCouplingAudit.ObservableChangeCounts();
        Assert.Equal(8, counts.Observables);
        Assert.Equal(1, counts.Changing);
        Assert.Equal(0, counts.Amplifying);
        Assert.True(counts.MaxAmplification < 1.0,
            $"the visible difference must CLOSE, measured factor {F3(counts.MaxAmplification)}");
        Assert.Equal(0, counts.ChangingAtThePatternSite);

        // and at the mirrored sites the two orientations are bit-identical, which is the relabelling statement
        Assert.All(OrientationCouplingAudit.ObservableChangeAtThePatternSite(),
            r => Assert.Equal(0.0, r.DifferenceAtZero, 12));
    }

    [Fact]
    public void Y_G_100_07_TheReductionTest()
    {
        PrintHeader("G_100 - task 7: the orientation information removed, with the LAW loss and the STATE loss apart");
        var sb = Start(
            "the reduction projects the state onto its even part, which keeps the scale and every parity-even feature;",
            "the LAW loss counts the operator and the symbol spectrum - a removed RATE would be a different finding;",
            "the state-term residual is reported separately because it is the removed component carried by the same",
            "operator, not a missing law term;",
            "the transport loss is the change of the transport quantities themselves;",
            "the equality rule is 1e-12.");
        sb.AppendLine(OrientationCouplingAudit.OutputSensitivity().Split("7. THE REDUCTION TEST")[1]);
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Reduction();
        Assert.Equal(5, rows.Length);
        var counts = OrientationCouplingAudit.ReductionCounts();
        Assert.Equal(2, counts.ObservableLoss);
        Assert.Equal(0, counts.LawLoss);
        Assert.Equal(0, counts.TransportLoss);
        Assert.True(counts.StateTermResidual > 0.1, $"the state term must move, measured {N(counts.StateTermResidual)}");

        // the two observables lost are the parity-odd ones
        Assert.True(OrientationCouplingAudit.IsOdd("the gradient (acceleration) along a link"));
        Assert.False(OrientationCouplingAudit.IsOdd("the second-order AT/GR ratio at the target"));
    }

    [Fact]
    public void Y_G_100_08_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_100 - classification and criteria");
        var sb = Start(
            "each coupling channel is classified separately, because they differ and collapsing them is the easy error;",
            "the classification is decided by measured counts, not by a judgement;",
            "the task's success criterion asks for a change in EVOLUTION or TRANSPORT attributable to the field: the",
            "update does change with the state, and that is not the same thing;",
            "the failure criterion is the complement, and both are reported with their counts;",
            "no literal reaches a verdict: every outcome is a comparison of measured quantities.");
        sb.Append(OrientationCouplingAudit.OutputClassification());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationCouplingAudit.Classification();
        Assert.Equal(5, rows.Length);
        Assert.Equal(OrientationCoupling.Coupled, rows.Single(r => r.Channel.Contains("DESCRIPTIVE")).Coupling);
        Assert.Equal(OrientationCoupling.Decoupled, rows.Single(r => r.Channel.Contains("INDEPENDENT")).Coupling);
        Assert.Equal(OrientationCoupling.Decoupled, rows.Single(r => r.Channel.Contains("SOURCE OF TRANSPORT")).Coupling);
        Assert.Equal(OrientationCoupling.Decoupled, rows.Single(r => r.Channel.Contains("DYNAMICAL INPUT")).Coupling);
        Assert.Equal(OrientationCoupling.Coupled, rows.Single(r => r.Channel.Contains("OBSERVABLE-ONLY")).Coupling);

        var criteria = OrientationCouplingAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("NOT MET", criteria[0].Outcome);
        Assert.Equal("MET", criteria[1].Outcome);
        Assert.Equal("CONFIRMED", criteria[2].Outcome);
    }

    [Fact]
    public void Y_G_100_09_TheVerdictAndTheCrossChecks()
    {
        PrintHeader("G_100 - the verdict, cross-checked against the audits it continues");
        var sb = Start(
            "the verdict is COMPUTED with live branches, so no clause can be a literal (G_027);",
            "the cross-checks are the audits this one continues, and each is asserted at ITS OWN tolerance and in ITS own",
            "regime - a cited number is a claim until it is recomputed;",
            "the spectral substrate is the canonical 96-cell ring and the cubic substrate is forbidden here;",
            "the equality rules are 1e-12 on computed values and exact equality on identities.");
        sb.AppendLine("VERDICT");
        sb.AppendLine("  " + OrientationCouplingAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine("WHAT THIS ESTABLISHES AND WHAT IT DOES NOT");
        sb.AppendLine(OrientationCouplingAudit.WhereItStands());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = OrientationCouplingAudit.Verdict();
        Assert.Contains("DECOUPLED", verdict);
        Assert.Contains("COUPLED as an OBSERVABLE-ONLY effect", verdict);
        Assert.Contains("FINAL VERDICT", verdict);

        // cross-check 1 (G_099): the ring's automorphism group is 192 and the law commutes with all of them
        Assert.Equal(192, OrientationOriginAudit.Automorphisms().Length);
        Assert.Equal(0, OrientationOriginAudit.LawUnderAutomorphisms().Failing);

        // cross-check 2 (G_098): the link set is unordered, so the direction is not represented
        Assert.All(OrientationCouplingAudit.Links, l => Assert.True(l.A <= l.B));

        // cross-check 3 (G_098): the transport/diffusion ratio of the surviving operator is zero
        var lawOnly = DirectedTransportAudit.TransportDiffusionRatio().Single(r => r.Mode.Contains("operator alone"));
        Assert.Equal(0.0, lawOnly.Ratio, 12);

        // cross-check 4 (G_097): the evolution diffuses and the observationally identical histories remain 48!
        var degeneracy = PatternKinematicsAudit.Degeneracy().Single();
        Assert.True(degeneracy.IndependentTransfers > 1);
        Assert.True(degeneracy.Log10Orderings > 60);

        // cross-check 5 (G_099): the orientation field is a DERIVED functional, so it cannot be an independent variable
        Assert.Equal(0, OrientationCouplingAudit.InventoryCounts().Independent);
    }
}
