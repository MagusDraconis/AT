using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_099 - ORIENTATION ORIGIN AUDIT.
///
/// G_097 measured that motion is emergent while its kinematics is assumed (the centroid shift is exactly zero, the
/// width grows as the square root of time); G_098 measured that the transport/diffusion ratio is zero for every
/// surviving mode and - sharpest of all - that the surviving link set does not REPRESENT a direction, so directed
/// transport needs one bit of chirality the sector does not supply. This suite asks the question that leaves open:
///
///   can orientation emerge from the surviving AT structure, or is orientation independent boundary information?
///
/// The instrument is the ring's own automorphism group: the cycle has 192 automorphisms (96 rotations and 96
/// reflections), and a reflection is exactly the map that swaps A -> B for B -> A. Whether orientation is represented
/// is therefore not a matter of opinion - it is whether any surviving object BREAKS that symmetry.
///
/// A distinction the suite keeps throughout, because collapsing it would be the easy error: an object can carry
/// orientation as a FIELD (a gradient distinguishes the two directions at a place) while carrying none as a
/// TRANSPORT RULE (nothing says which way count should move).
///
/// Every count, dimension, rank and multiplicity names its object: the ring is the canonical 96-cell ring (cells = 96,
/// links = LinksOf(96, 1)), the automorphism group is the dihedral group of the cycle (order 192), and the equality
/// rules are 1e-12 on a computed value and exact equality where two built configurations are compared. The cubic
/// substrate is forbidden and is not named in this file. No new primitive, field, source term, topology axiom, action,
/// Lagrangian, carrier, orientation tag or external coordinate is introduced. A negative result is acceptable.
///
/// The report follows the seven tasks.
/// </summary>
public sealed class Y_G_099_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_099_Tests(ITestOutputHelper output) : base(output) { }

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
    public void Y_G_099_01_TheOrientationInventory()
    {
        PrintHeader("G_099 - task 1: every surviving object that could distinguish A -> B from B -> A");
        var sb = Start(
            "the candidates are the objects the surviving sector actually has: the state, the potential's gradient, a",
            "potential difference, the flux, the link set, the update law, conservation and the phase;",
            "an object is credited with a direction only if it CAN distinguish A -> B from B -> A, which is a structural",
            "property of the object rather than a property of a measurement;",
            "EVERY count is on the 96-cell ring; the cubic substrate is forbidden and is not named in the code;",
            "a candidate that cannot carry a direction is reported as one rather than quietly dropped.");
        sb.Append(OrientationOriginAudit.OutputInventory());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var carriers = OrientationOriginAudit.Carriers();
        Assert.Equal(8, carriers.Length);

        var counts = OrientationOriginAudit.CarrierCounts();
        Assert.Equal(8, counts.Objects);
        Assert.Equal(3, counts.Distinguishing);
        Assert.Equal(5, counts.Derived);

        // the three distinguishing objects are differences of the potential plus the flux, and nothing else
        var distinguishing = carriers.Where(c => c.DistinguishesADirection).Select(c => c.Object).ToArray();
        Assert.Equal(3, distinguishing.Length);
        Assert.All(distinguishing, o =>
            Assert.True(o.Contains("GRADIENT") || o.Contains("DIFFERENCE") || o.Contains("FLUX"),
                $"unexpected direction-carrying object: {o}"));

        // the state is NOT one of them: it is parity-even to the rounding, so it supplies no handedness
        var state = carriers.Single(c => c.Object.Contains("STATE"));
        Assert.False(state.DistinguishesADirection);
        Assert.Equal(0.0, state.Chirality, 12);
        Assert.Equal(OrientationStatus.Assumed, state.Status);

        // the link set and the phase cannot distinguish at all
        Assert.All(carriers.Where(c => c.Object.Contains("link set") || c.Object.Contains("phase")),
            c => Assert.False(c.DistinguishesADirection));

        var law = carriers.Single(c => c.Object.Contains("update law"));
        var conservation = carriers.Single(c => c.Object.Contains("conservation"));
        Assert.False(law.DistinguishesADirection);
        Assert.False(conservation.DistinguishesADirection);
    }

    [Fact]
    public void Y_G_099_02_TheSymmetryBreakingMeasurement()
    {
        PrintHeader("G_099 - task 2: the ring's automorphism classes, and which of them the surviving structures break");
        var sb = Start(
            "the cycle's automorphism group is the dihedral group: 96 rotations and 96 reflections, 192 in total;",
            "the STATE is the canonical MapRedundancyAudit.State, and a reflection is the map that swaps A for B;",
            "an automorphism is 'broken' by the state when relabelling changes the configuration;",
            "the equality rule is 1e-12 on a computed value;",
            "a group that preserves an object cannot supply that object with a direction - which is the whole test.");
        sb.Append(OrientationOriginAudit.OutputSymmetry());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var autos = OrientationOriginAudit.Automorphisms();
        Assert.Equal(192, autos.Length);
        Assert.Equal(96, autos.Count(a => a.Kind == "rotation"));
        Assert.Equal(96, autos.Count(a => a.Kind == "reflection"));

        var stabilisers = OrientationOriginAudit.Stabilisers();
        Assert.Equal(2, stabilisers.Length);
        var rotation = stabilisers.Single(s => s.Kind == "rotation");
        var reflection = stabilisers.Single(s => s.Kind == "reflection");
        Assert.Equal(96, rotation.Total);
        Assert.Equal(96, reflection.Total);
        Assert.Equal(1, rotation.Preserving);
        Assert.Equal(1, reflection.Preserving);
        Assert.Equal(95, rotation.Broken);
        Assert.Equal(95, reflection.Broken);

        // but the preserved reflection is the one through the state's own axis, so the state is parity-EVEN
        var map = autos.First(a => a.Kind == "reflection" && a.K == 0).Map;
        double chirality = OrientationOriginAudit.ChiralityUnder(OrientationOriginAudit.State, map);
        Assert.True(chirality < OrientationOriginAudit.Tol,
            $"the state must be parity-even to the rounding, measured {N(chirality)}");
        Assert.True(chirality < 1e-12, "below the equality rule, so the state supplies no chirality bit");

        // and the LAW breaks nothing: it commutes with every automorphism
        var law = OrientationOriginAudit.LawUnderAutomorphisms();
        Assert.Equal(192, law.Tested);
        Assert.Equal(0, law.Failing);
        Assert.True(law.Worst < 1e-12, $"the law must commute with all 192 automorphisms, worst {N(law.Worst)}");
    }

    [Fact]
    public void Y_G_099_03_OrientationReversalAppliedToEveryObservable()
    {
        PrintHeader("G_099 - task 3: every surviving observable under orientation reversal, swept over all 96 reversals");
        var sb = Start(
            "a ring has 96 orientation-reversing maps, not one, so the audit sweeps them ALL and reports the range;",
            "a single reflection would let the answer depend on an unstated convention - the first draft did exactly that",
            "by choosing the reflection the state happens to preserve, and it read 'changed = 0';",
            "a readout is credited only when its change exceeds the equality rule 1e-12;",
            "frame-invariance is taken from MinimalGravityCoreAudit.ObservableSet, measured in G_085.");
        sb.Append(OrientationOriginAudit.OutputReversalAndBits());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var counts = OrientationOriginAudit.ReversalCounts();
        Assert.Equal(96, counts.Reflections);
        Assert.Equal(1, counts.ChangingNone);
        Assert.Equal(6, counts.MaxChanged);
        Assert.Equal(0, counts.MinChanged);
        Assert.True(counts.AnyChange);
        Assert.True(counts.LargestChange > 1e-12);
        Assert.True(counts.LargestChange < 1.0, "the change is a readout difference and stays below unity here");

        // the sweep must NOT be a single reflection: the identical-reflection defect would show as every K agreeing
        var sweep = OrientationOriginAudit.ReversalSweep();
        Assert.Equal(96, sweep.Length);
        Assert.True(sweep.Select(s => s.Changed).Distinct().Count() > 1,
            "the reversal effect depends on WHICH reversal is meant, so a single-reflection census would be a convention");

        // at the strongest reversal, at least one frame-invariant readout changes - so the change is physics, not labelling
        var census = OrientationOriginAudit.ReversalCensus();
        Assert.Equal(8, census.Length);
        var changedInvariant = census.Where(r => r.Changed && r.FrameInvariant).ToArray();
        Assert.NotEmpty(changedInvariant);
        Assert.All(changedInvariant, r => Assert.True(r.Change > 1e-12));

        // and the field's own acceleration is one of them, which is the audit's central observable fact
        Assert.Contains(changedInvariant, r => r.Observable.Contains("gradient"));
    }

    [Fact]
    public void Y_G_099_04_TheChiralityBitTableIsZero()
    {
        PrintHeader("G_099 - task 4: the chirality bits supplied by each surviving source, measured");
        var sb = Start(
            "a source supplies a bit only if it BREAKS the orientation symmetry - the definition is structural;",
            "the configuration's parity is measured against all 96 reversals, not against a chosen one;",
            "the equality rule is 1e-12; a quantity below it is zero rather than small;",
            "the spectrum's evenness is the G_098 measurement, cited rather than re-derived;",
            "the total is a sum of measured zeros, so the sector's chirality content is 0 bit, not 'unknown'.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationOriginAudit.ChiralityBits();
        Assert.Equal(5, rows.Length);
        Assert.All(rows, r => Assert.Equal(0, r.Bits));

        var total = OrientationOriginAudit.ChiralityTotal();
        Assert.Equal(0, total.TotalBits);

        // the decisive row is the configuration's, and it is zero because the state is parity-EVEN
        var state = rows.Single(r => r.Source.StartsWith("the state"));
        Assert.Equal(0, state.Bits);
        Assert.Contains("MEASURED ZERO", state.Measurement);

        var map = OrientationOriginAudit.Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map;
        Assert.True(OrientationOriginAudit.ChiralityUnder(OrientationOriginAudit.State, map)
            < OrientationOriginAudit.Tol);
    }

    [Fact]
    public void Y_G_099_05_ParityOddQuantitiesAndTheirSurvivors()
    {
        PrintHeader("G_099 - task 5: the parity-odd quantities already in the sector, after the gauge and coordinate reductions");
        var sb = Start(
            "the candidates are the readouts the surviving minimal core has, plus the two-site redshift;",
            "a quantity SURVIVES the reductions when it is frame-invariant, because the frame-dependent ones are",
            "coordinates (the reduction G_085 measured);",
            "parity is computed by applying a reflection to the input and comparing, not by inspection;",
            "the sector's unique prediction (the AT/GR ratio) is expected to be EVEN - a prediction with chirality would",
            "have shown up in G_087's fit, and it did not.");
        sb.Append(OrientationOriginAudit.OutputParityAndGain());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationOriginAudit.ParityOddQuantities();
        Assert.Equal(6, rows.Length);
        var counts = OrientationOriginAudit.ParityOddCounts();
        Assert.Equal(6, counts.Tested);
        Assert.Equal(3, counts.ParityOdd);
        Assert.Equal(3, counts.SurvivingOdd);

        // every parity-odd survivor is a two-site difference, which is the reason it is odd
        var survivors = rows.Where(r => r.ParityOdd && r.SurvivesTheReductions).Select(r => r.Quantity).ToArray();
        Assert.Equal(3, survivors.Length);
        Assert.All(survivors, q => Assert.True(
            q.Contains("acceleration") || q.Contains("gradient") || q.Contains("redshift"),
            $"unexpected parity-odd survivor: {q}"));

        // and the unique prediction is even
        var ratio = rows.Single(r => r.Quantity.Contains("AT/GR"));
        Assert.False(ratio.ParityOdd);
        Assert.True(ratio.SurvivesTheReductions);
    }

    [Fact]
    public void Y_G_099_06_WithEveryLabelRemoved()
    {
        PrintHeader("G_099 - task 6: all labels, indices and ordering conventions removed, then the observables recomputed");
        var sb = Start(
            "the frame-invariant readouts are the ones that cannot be a labelling, so they survive by measurement;",
            "a RELATIVE position is not a label: comparing two sites survives, indexing one does not;",
            "the centroid needs an index and is therefore undefined rather than zero without labels - G_098 measured the",
            "same fact for its transport measure;",
            "the flux is read by 0 observables, so removing the labels removes nothing and reveals nothing either.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationOriginAudit.WithoutLabels();
        Assert.Equal(5, rows.Length);
        var remaining = OrientationOriginAudit.RemainingOrientation();
        Assert.Equal(5, remaining.Quantities);
        Assert.Equal(3, remaining.Surviving);
        Assert.Equal(3, remaining.SurvivingAndOdd);

        Assert.All(rows.Where(r => r.Quantity.Contains("centroid") || r.Quantity.Contains("flux")),
            r => Assert.False(r.SurvivesWithoutLabels));
        Assert.All(rows.Where(r => r.Quantity.Contains("acceleration") || r.Quantity.Contains("gradient")
            || r.Quantity.Contains("redshift")), r => Assert.True(r.SurvivesWithoutLabels));
    }

    [Fact]
    public void Y_G_099_07_WhatOneOrientationBitBuys()
    {
        PrintHeader("G_099 - task 7: the reduction test - one global orientation bit added, and the gain measured");
        var sb = Start(
            "the bit is added to the LINK SET, which is where G_098 found the orientation missing;",
            "the observables are recomputed rather than argued about: an added orientation does not change the STATE;",
            "the transport ratio is the G_098 transport/diffusion ratio, with and without the orientation supplied;",
            "the degeneracy is the G_097 count of mutually independent transfers that commute with the law;",
            "information gain is counted in OBSERVABLE terms, since an unread quantity is not information.");
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var gain = OrientationOriginAudit.OrientationBitGain();
        Assert.Equal(4, gain.Length);
        var observables = gain.Single(g => g.Gain.Contains("observables"));
        var transport = gain.Single(g => g.Gain.Contains("transport"));
        var degeneracy = gain.Single(g => g.Gain.Contains("degeneracy"));
        var information = gain.Single(g => g.Gain.Contains("information"));

        Assert.Equal(0, observables.Count);
        Assert.Equal(1, transport.Count);
        Assert.True(transport.Measure > 0.3 && transport.Measure < 0.4,
            $"the transport ratio the bit enables is {F3(transport.Measure)}");
        Assert.Equal(0, degeneracy.Count);
        Assert.Equal(0, information.Count);

        // the ratio the bit enables is exactly the G_098 'orientation supplied' value
        double supplied = DirectedTransportAudit.TransportDiffusionRatio().Single(r => r.OrientationSupplied).Ratio;
        Assert.Equal(supplied, transport.Measure + Math.Abs(DirectedTransportAudit.TransportDiffusionRatio()
            .Single(r => r.Mode.Contains("operator alone")).Ratio), 12);
    }

    [Fact]
    public void Y_G_099_08_TheClassificationAndTheCriteria()
    {
        PrintHeader("G_099 - classification and criteria");
        var sb = Start(
            "each orientation mode is classified separately, because they are different objects: as a FIELD, as a",
            "TRANSPORT RULE, as a HANDEDNESS of the configuration, and as an ADDED bit;",
            "the classification is decided by measured counts, not by a judgement;",
            "the task's success criterion is about orientation GENERATED by the structure, and the failure criterion is",
            "about orientation information being ZERO - both can hold of different objects, and the suite says which;",
            "no literal reaches a verdict: every outcome is a comparison of measured quantities.");
        sb.Append(OrientationOriginAudit.OutputClassification());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var rows = OrientationOriginAudit.Classification();
        Assert.Equal(4, rows.Length);
        Assert.Equal(OrientationStatus.Derived,
            rows.Single(r => r.Mode.Contains("FIELD")).Status);
        Assert.Equal(OrientationStatus.Assumed,
            rows.Single(r => r.Mode.Contains("TRANSPORT RULE")).Status);
        Assert.Equal(OrientationStatus.Assumed,
            rows.Single(r => r.Mode.Contains("CHIRALITY")).Status);
        Assert.Equal(OrientationStatus.Boundary,
            rows.Single(r => r.Mode.Contains("ADDED")).Status);

        var criteria = OrientationOriginAudit.Criteria();
        Assert.Equal(3, criteria.Length);
        Assert.Equal("MET", criteria[0].Outcome);
        Assert.Equal("MET", criteria[1].Outcome);
        Assert.Equal("CONFIRMED", criteria[2].Outcome);
    }

    [Fact]
    public void Y_G_099_09_TheVerdictAndTheCrossChecks()
    {
        PrintHeader("G_099 - the verdict, cross-checked against the audits it continues");
        var sb = Start(
            "the verdict is COMPUTED with live branches, so no clause can be a literal (G_027);",
            "the cross-checks are the audits this one continues, and each is asserted at ITS OWN tolerance and in ITS",
            "own regime - a cited number is a claim until it is recomputed;",
            "the spectral substrate is the canonical 96-cell ring, and the cubic substrate is forbidden here;",
            "the equality rules are 1e-12 on computed values and exact equality on constructed configurations.");
        sb.AppendLine("VERDICT");
        sb.AppendLine("  " + OrientationOriginAudit.Verdict());
        sb.AppendLine();
        sb.AppendLine("WHAT THIS ESTABLISHES AND WHAT IT DOES NOT");
        sb.AppendLine(OrientationOriginAudit.WhereItStands());
        sb.AppendLine();
        sb.Append(OrientationOriginAudit.OutputParityAndGain());
        Output.WriteLine(sb.ToString());
        sb.Clear();

        var verdict = OrientationOriginAudit.Verdict();
        Assert.DoesNotContain("computed", verdict);
        Assert.Contains("FINAL VERDICT", verdict);
        Assert.Contains("DERIVED", verdict);
        Assert.Contains("ASSUMED", verdict);
        Assert.Contains("BOUNDARY", verdict);

        // cross-check 1 (G_098): the link set is unordered, so the orientation is absent from the representation
        var links = OrientationOriginAudit.Links;
        Assert.Equal(96, links.Length);
        Assert.All(links, l => Assert.True(l.A <= l.B));

        // cross-check 2 (G_097): the evolution diffuses - the commutativity that hides the transport also gives 48!
        var degeneracy = PatternKinematicsAudit.Degeneracy().Single();
        Assert.True(degeneracy.IndependentTransfers > 1);
        Assert.True(degeneracy.Log10Orderings > 60,
            $"the observationally identical histories must remain in the 10^60 band, measured 10^{F3(degeneracy.Log10Orderings)}");

        // cross-check 3 (G_098): the transport/diffusion ratio without the orientation is exactly zero
        var alone = DirectedTransportAudit.TransportDiffusionRatio().Single(r => r.Mode.Contains("operator alone"));
        Assert.Equal(0.0, alone.Ratio, 12);

        // cross-check 4 (G_086/G_085): the surviving kernel is the minimal core this audit's readouts come from
        Assert.Equal(8, MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full()).Length);
    }
}
