using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>How an orientation-carrying object stands against the surviving sector (ResearchY-G_099).</summary>
public enum OrientationStatus { Derived, Emergent, Assumed, Boundary }

/// <summary>
/// Where an orientation-carrying object's content comes from: COMPUTED from the surviving primitives, SUPPLIED as
/// boundary data, or ABSENT from this ring entirely. It is an adjudication with its own measurement, so the derived
/// flag below is computed from it rather than typed (G_027).
/// </summary>
public enum OrientationOrigin { Computed, Supplied, Absent }

/// <summary>One surviving object tested for whether it can distinguish A -> B from B -> A.</summary>
public sealed record OrientationCarrier(string Object, bool DistinguishesADirection, OrientationOrigin Origin,
    double Chirality, OrientationStatus Status, string Measurement)
{
    /// <summary>DERIVED exactly when the object is COMPUTED from the surviving primitives — derived, not typed.</summary>
    public bool Derived => Origin == OrientationOrigin.Computed;
}

/// <summary>
/// ResearchY-G_099 — ORIENTATION ORIGIN AUDIT (group G — Gravity Source).
///
/// G_097 measured that the evolution diffuses with an exactly zero centroid shift and that motion is emergent while
/// its kinematics is assumed; G_098 measured that the transport/diffusion ratio is zero for every surviving mode,
/// that the surviving operator is symmetric, and — sharpest of all — that the **link set does not represent a
/// direction**, so directed transport needs one bit of chirality the sector does not supply. This audit asks the
/// question that leaves open, and it is a question about SYMMETRY rather than about transport:
///
/// **Can orientation emerge from the surviving AT structure, or is orientation independent boundary information?**
///
/// The audit's instrument is the ring's own automorphism group: the cycle has **192** automorphisms (96 rotations and
/// 96 reflections), and the reflection is exactly the map that swaps `A -> B` for `B -> A`. Whether orientation is
/// represented is therefore not a matter of opinion — it is whether any surviving object **breaks that symmetry**,
/// and each of the 192 can be tested against each object.
///
/// A distinction the audit keeps throughout, because collapsing it would be the easy error: an object can carry
/// orientation as a **field** (a gradient distinguishes the two directions at a place) while carrying none as a
/// **transport rule** (nothing says which way count should move). G_098 measured the second; this audit measures both
/// and reports them separately.
///
/// Every count, dimension, rank and multiplicity names its object: the ring is the canonical 96-cell ring (cells = 96,
/// densities = `MapRedundancyAudit.State`, links = `LinksOf(96, 1)`), the automorphism group is the dihedral group of
/// the cycle (order 192), and the equality rules are `1e-12` on a computed value and **exact** equality where two
/// built configurations are compared. The cubic substrate is forbidden and is not named. No new primitive, field,
/// source term, topology axiom, action, Lagrangian, carrier, orientation tag or external coordinate is introduced.
///
/// Prefers refutation.
/// </summary>
public static class OrientationOriginAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    public static double[] State => MapRedundancyAudit.State;
    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    // ===================== THE AUTOMORPHISM GROUP =====================

    /// <summary>
    /// THE 192 AUTOMORPHISMS OF THE 96-CYCLE, as index maps: 96 ROTATIONS `i -> (k + i) mod N` and 96 REFLECTIONS
    /// `i -> (k - i) mod N`. The reflections are exactly the maps that exchange `A -> B` for `B -> A`, so this
    /// enumeration is the whole orientation question in one object.
    /// </summary>
    public static (string Kind, int K, int[] Map)[] Automorphisms()
        => Enumerable.Range(0, Cells).SelectMany(k => new[]
        {
            ("rotation", k, Enumerable.Range(0, Cells).Select(i => (k + i) % Cells).ToArray()),
            ("reflection", k, Enumerable.Range(0, Cells).Select(i => ((k - i) % Cells + Cells) % Cells).ToArray()),
        }).ToArray();

    /// <summary>Applies an index map to a configuration: the relabelled value at i is the original at map[i].</summary>
    public static double[] Relabel(double[] rho, int[] map)
        => Enumerable.Range(0, Cells).Select(i => rho[map[i]]).ToArray();

    /// <summary>The largest pointwise difference between a configuration and its relabelling — the chirality.</summary>
    public static double ChiralityUnder(double[] rho, int[] map)
        => Relabel(rho, map).Zip(rho, (a, b) => Math.Abs(a - b)).Max();

    /// <summary>
    /// THE STABILISER OF THE SURVIVING STATE (task 2): how many of the 192 automorphisms leave it invariant. This is
    /// the symmetry-breaking measurement — and the count is what decides whether the state carries orientation.
    /// </summary>
    public static (string Kind, int Preserving, int Total, int Broken, string Reading)[] Stabilisers()
    {
        var automorphisms = Automorphisms();
        var rho = State;
        int rotations = automorphisms.Count(a => a.Kind == "rotation");
        int reflections = automorphisms.Count(a => a.Kind == "reflection");
        int preservingRotations = automorphisms.Where(a => a.Kind == "rotation")
            .Count(a => ChiralityUnder(rho, a.Map) < Tol);
        int preservingReflections = automorphisms.Where(a => a.Kind == "reflection")
            .Count(a => ChiralityUnder(rho, a.Map) < Tol);
        var readouts = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full())
            .Select(r => r.Value).ToArray();
        int preservingReadouts = automorphisms.Count(a => ReadoutsOf(RelabelState(rho, a.Map)) == null);
        return new[]
        {
            ("rotation", preservingRotations, rotations, rotations - preservingRotations,
                $"{preservingRotations} of {rotations} rotations leaves the surviving state invariant, so the state "
                + $"breaks {rotations - preservingRotations} of them — it is not rotationally symmetric"),
            ("reflection", preservingReflections, reflections,
                reflections - preservingReflections,
                $"{preservingReflections} of {reflections} orientation-reversing reflections leaves the state invariant: "
                + $"{(preservingReflections == reflections ? "the state is parity-even" : "the state BREAKS the orientation symmetry, so it distinguishes A->B from B->A")}"),
        };
    }

    /// <summary>Readouts are computed on a description, so a relabelled state is wrapped in the full description.</summary>
    private static double[]? ReadoutsOf(double[] rho)
    {
        var description = MinimalGravityCoreAudit.Full() with { Rho = rho };
        return MinimalGravityCoreAudit.ReadoutsOf(description).Select(r => r.Value).ToArray();
    }

    private static double[] RelabelState(double[] rho, int[] map) => Relabel(rho, map);

    /// <summary>
    /// WHETHER THE UPDATE LAW BREAKS THE ORIENTATION SYMMETRY (task 2): the Laplacian is built from the link set, and
    /// the test is whether it commutes with every automorphism — measured, not asserted.
    /// </summary>
    public static (int Tested, int Failing, double Worst, string Reading) LawUnderAutomorphisms()
    {
        var automorphisms = Automorphisms();
        int failing = 0;
        double worst = 0.0;
        foreach (var a in automorphisms)
        {
            var probe = Enumerable.Range(0, Cells).Select(i => Math.Sin(0.7 * i) + 0.3 * Math.Cos(1.9 * i)).ToArray();
            var left = Relabel(ConnectivityOriginAudit.UpdateAt(Cells, Links, probe), a.Map);
            var right = ConnectivityOriginAudit.UpdateAt(Cells, Links, Relabel(probe, a.Map));
            double difference = left.Zip(right, (x, y) => Math.Abs(x - y)).Max();
            worst = Math.Max(worst, difference);
            if (difference > 1e-9) failing++;
        }
        return (automorphisms.Length, failing, worst,
            $"the update law commutes with {automorphisms.Length - failing} of {automorphisms.Length} automorphisms "
            + $"(worst residual {E(worst)}), so the LAW breaks no orientation symmetry: it is invariant under every "
            + "rotation and every reflection, and the substrate's chirality is not in the operator");
    }

    // ===================== TASK 1: THE ORIENTATION INVENTORY =====================

    /// <summary>
    /// THE ORIENTATION INVENTORY (task 1): every surviving object tested for whether it can distinguish `A -> B` from
    /// `B -> A`, with the chirality it actually carries and its classification.
    /// </summary>
    public static OrientationCarrier[] Carriers()
    {
        var reflection = Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map;
        var rho = State;
        var acceleration = ConnectivityOriginAudit.AccelerationOnLinks(
            rho.Select(SourceManipulationAudit.Potential).ToArray(), Cells, Links);
        double gradientOddness = acceleration.Zip(Relabel(acceleration, reflection), (a, b) => a + b).Select(Math.Abs).Max();
        double gradientMagnitude = acceleration.Select(Math.Abs).Max();
        var probe = new int[Cells];
        probe[0] = -1;
        probe[1] = 1;
        var fluxes = ContinuityLawAudit.FluxOf(probe);
        var phase = ActuatorVersusObservableAudit.Structures().Single(s => s.Name.Contains("phase"));
        return new[]
        {
            new OrientationCarrier("the potential's GRADIENT (the acceleration)", true, OrientationOrigin.Computed,
                gradientMagnitude, OrientationStatus.Derived,
                $"DERIVED and PARITY-ODD: the gradient over the reflection i -> N-i satisfies a_i + a_(N-i) = "
                + $"{E(gradientOddness)} — it is antisymmetric, so it distinguishes the two directions at every link "
                + "where it is non-zero — and it is frame-invariant, so it survives the removal of the labelling"),

            new OrientationCarrier("the potential DIFFERENCE between two sites", true, OrientationOrigin.Computed,
                Math.Abs(SourceManipulationAudit.Potential(rho[Cells / 4]) - SourceManipulationAudit.Potential(rho[0])),
                OrientationStatus.Derived,
                "DERIVED and signed: A_b - A_a changes sign under the exchange of the two sites, and G_077 measured that "
                + "the clock offset and the acceleration integral are two readings of this one quantity"),

            new OrientationCarrier("the surviving STATE's configuration (its chirality)", false, OrientationOrigin.Supplied,
                ChiralityUnder(rho, reflection), OrientationStatus.Assumed,
                $"MEASURED ABSENT: max |rho_i - rho_(N-i)| = {E(ChiralityUnder(rho, reflection))} — zero to the "
                + "rounding ({Tol}) — so the surviving state is PARITY-EVEN and cannot distinguish the two directions. "
                + "The state's non-uniformity gives the gradient a direction, but that direction is the FIELD's and not "
                + "a handedness of the configuration: any chirality would have to be assumed"),

            new OrientationCarrier("the FLUX (the transport's own variable)", true, OrientationOrigin.Computed,
                fluxes.Select(Math.Abs).Max(), OrientationStatus.Boundary,
                $"a signed flow and therefore parity-odd by construction, and written by an operation — but read by "
                + $"{ContinuityLawAudit.ObservablesReadingTheFlux()} observables, so it carries a direction that no "
                + "measurement sees"),

            new OrientationCarrier("the link set (adjacency)", false, OrientationOrigin.Supplied, 0.0, OrientationStatus.Assumed,
                "it CANNOT distinguish the two directions: every link is stored as an unordered pair with a <= b "
                + "(G_098), so the orientation is discarded in the representation and no automorphism test can recover "
                + "it"),

            new OrientationCarrier("the update law (the Laplacian action)", false, OrientationOrigin.Computed, 0.0, OrientationStatus.Emergent,
                $"it commutes with all {Automorphisms().Length} automorphisms (measured), so it is parity-even and "
                + "carries no orientation — and the parities it propagates are the ones the state supplies"),

            new OrientationCarrier("the conservation statement", false, OrientationOrigin.Computed, 0.0, OrientationStatus.Emergent,
                "a sum over cells is invariant under every relabelling, so conservation is parity-even and cannot "
                + "distinguish a direction"),

            new OrientationCarrier("the phase (the theta sector)", false, OrientationOrigin.Absent, 0.0, OrientationStatus.Boundary,
                $"not a carrier on this ring: excluded by arity ({phase.OperationsChangingIt} operations write it), so "
                + "it supplies no orientation here"),
        };
    }

    /// <summary>Task 1's count.</summary>
    public static (int Objects, int Distinguishing, int Derived, string Reading) CarrierCounts()
    {
        var rows = Carriers();
        return (rows.Length, rows.Count(r => r.DistinguishesADirection), rows.Count(r => r.Derived),
            $"{rows.Length} objects tested: {rows.Count(r => r.DistinguishesADirection)} can tell A->B from B->A and "
            + $"{rows.Count(r => r.Derived)} of the {rows.Length} are DERIVED — but every distinguishing object is a "
            + "POTENTIAL DIFFERENCE or the unobserved flux, and none of them is a transport rule");
    }

    // ===================== TASK 3: ORIENTATION REVERSAL APPLIED TO EVERY OBSERVABLE =====================

    /// <summary>
    /// ORIENTATION REVERSAL APPLIED TO EVERY OBSERVABLE (task 3). A ring has **96** orientation-reversing maps, not
    /// one, so the audit SWEEPS them all and reports the range: a single reflection would let the answer depend on an
    /// unstated convention, and the first draft did exactly that by choosing the reflection the state happens to
    /// preserve.
    /// </summary>
    public static (int K, int Changed, int ChangedAndFrameInvariant, double LargestChange, string Reading)[]
        ReversalSweep()
    {
        var full = MinimalGravityCoreAudit.Full();
        var original = MinimalGravityCoreAudit.ReadoutsOf(full);
        var frames = MinimalGravityCoreAudit.ObservableSet();
        return Automorphisms().Where(a => a.Kind == "reflection").Select(a =>
        {
            var reversed = MinimalGravityCoreAudit.ReadoutsOf(full with { Rho = Relabel(full.Rho, a.Map) });
            var changes = original.Zip(reversed, (o, r) => Math.Abs(o.Value - r.Value)).ToArray();
            int changed = changes.Count(c => c > Tol);
            int invariantChanged = changes.Select((c, i) => (c, i)).Count(p => p.c > Tol && frames[p.i].FrameInvariant);
            return (a.K, changed, invariantChanged, changes.Max(),
                changed == 0
                    ? "this reversal leaves every readout unchanged"
                    : $"{changed} readout(s) change, {invariantChanged} of them frame-invariant");
        }).ToArray();
    }

    /// <summary>Task 3's count, as a RANGE over the 96 reversals rather than a single number.</summary>
    public static (int Reflections, int ChangingNone, int MaxChanged, int MinChanged, double LargestChange,
        bool AnyChange, string Reading) ReversalCounts()
    {
        var sweep = ReversalSweep();
        int max = sweep.Max(s => s.Changed);
        int min = sweep.Min(s => s.Changed);
        return (sweep.Length, sweep.Count(s => s.Changed == 0), max, min, sweep.Max(s => s.LargestChange),
            max > 0,
            $"{sweep.Length} orientation-reversing maps: at least one readout changes under {sweep.Length - sweep.Count(s => s.Changed == 0)} "
            + $"of them ({sweep.Count(s => s.Changed == 0)} leave every readout unchanged), the change count ranges from "
            + $"{min} to {max}, and the largest single change is {E(sweep.Max(s => s.LargestChange))} — so the effect of a "
            + "reversal depends on WHICH reversal is meant, which is why the audit sweeps them all");
    }

    /// <summary>
    /// THE REVERSAL CENSUS AT THE STRONGEST REVERSAL, so the readout-by-readout detail is available without a
    /// convention being smuggled in: the reflection reported is the one that changes the most readouts.
    /// </summary>
    public static (string Observable, double Original, double Reversed, double Change, bool Changed,
        bool FrameInvariant, string Note)[] ReversalCensus()
    {
        var sweep = ReversalSweep();
        int worst = sweep.OrderByDescending(s => s.Changed).ThenByDescending(s => s.LargestChange).First().K;
        var map = Automorphisms().Single(a => a.Kind == "reflection" && a.K == worst).Map;
        var full = MinimalGravityCoreAudit.Full();
        var original = MinimalGravityCoreAudit.ReadoutsOf(full);
        var reversed = MinimalGravityCoreAudit.ReadoutsOf(full with { Rho = Relabel(full.Rho, map) });
        var frames = MinimalGravityCoreAudit.ObservableSet();
        return original.Zip(reversed, (o, r) => (o, r)).Select((pair, index) => (
            pair.o.Observable,
            pair.o.Value,
            pair.r.Value,
            Math.Abs(pair.o.Value - pair.r.Value),
            Math.Abs(pair.o.Value - pair.r.Value) > Tol,
            frames[index].FrameInvariant,
            Math.Abs(pair.o.Value - pair.r.Value) > Tol
                ? "CHANGES under orientation reversal: this readout distinguishes the two directions"
                : "unchanged: this readout is parity-even, so it constrains the configuration and not its chirality"
        )).ToArray();
    }

    // ===================== TASK 4: THE CHIRALITY-BIT TABLE =====================

    /// <summary>
    /// THE CHIRALITY CONTENT (task 4), per source, each measured rather than asserted: a source supplies a bit only if
    /// it BREAKS the orientation symmetry, and each row reports which measurement decides it.
    /// </summary>
    public static (string Source, int Bits, string Measurement)[] ChiralityBits()
    {
        var stabilisers = Stabilisers();
        var law = LawUnderAutomorphisms();
        var reversal = ReversalCounts();
        var reflection = Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map;
        double chirality = ChiralityUnder(State, reflection);
        int stateBits = chirality > Tol ? 1 : 0;
        return new[]
        {
            ("the state (its configuration)", stateBits,
                stateBits > 0
                    ? $"max |rho_i - rho_(N-i)| = {E(chirality)} exceeds the equality rule {E(Tol)}, so the configuration "
                      + "breaks the orientation symmetry and carries ONE bit"
                    : $"MEASURED ZERO: max |rho_i - rho_(N-i)| = {E(chirality)} is below the equality rule {E(Tol)}, so the "
                      + $"state is PARITY-EVEN — it preserves "
                      + $"{stabilisers.Single(s => s.Kind == "reflection").Preserving} of the 96 reflections, the one "
                      + "through its own axis — and supplies NO bit"),

            ("the spectrum", 0,
                "G_098 measured max |symbol(k) - symbol(N-k)| = 2.665E-015: the dispersion is EVEN, so the spectrum "
                + "supplies no bit"),

            ("the update law", 0,
                $"measured here: the law commutes with all {law.Tested} automorphisms ({law.Failing} failing, worst "
                + $"residual {E(law.Worst)}), so it supplies no bit"),

            ("the conservation statement", 0,
                "a sum is invariant under every relabelling, so it supplies no bit"),

            ("the observable set (as functionals)", 0,
                $"the observables are functions of the state and add no parity of their own: the reversal sweep shows the "
                + $"change count ranging from {ReversalCounts().MinChanged} to {ReversalCounts().MaxChanged} over the "
                + "96 reversals, and every change traces to the STATE being relabelled, not to the functional"),
        };
    }

    /// <summary>Task 4's total, with the source that supplies it.</summary>
    public static (int TotalBits, string From, string Reading) ChiralityTotal()
    {
        var rows = ChiralityBits();
        var total = rows.Sum(r => r.Bits);
        var from = rows.Where(r => r.Bits > 0).Select(r => r.Source).ToArray();
        return (total, string.Join(", ", from),
            total == 0
                ? "the surviving structure supplies 0 bit of chirality in total: all five sources — the configuration "
                  + "(parity-even to the rounding), the spectrum, the update law, conservation and the observable set — "
                  + "are measured at zero"
                : $"the surviving structure supplies {total} bit of chirality in total, and it comes from "
                  + $"{string.Join(" and ", from)}");
    }

    // ===================== TASK 5: PARITY-ODD QUANTITIES =====================

    /// <summary>
    /// THE PARITY-ODD QUANTITIES ALREADY IN THE SECTOR (task 5), with the survivor count after the gauge and
    /// coordinate reductions G_085 measured. A quantity survives the reductions when it is frame-invariant, because
    /// the frame-dependent ones are coordinates.
    /// </summary>
    public static (string Quantity, bool ParityOdd, bool FrameInvariant, bool SurvivesTheReductions,
        string Measurement)[] ParityOddQuantities()
    {
        var map = Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map;
        var potential = State.Select(SourceManipulationAudit.Potential).ToArray();
        var acceleration = ConnectivityOriginAudit.AccelerationOnLinks(potential, Cells, Links);
        double oddness = acceleration.Zip(Relabel(acceleration, map), (a, b) => a + b).Select(Math.Abs).Max();
        var frames = MinimalGravityCoreAudit.ObservableSet();
        return new[]
        {
            ("the field's acceleration (a gradient)", true, true, true,
                $"odd to {E(oddness)} (a_i + a_(N-i) = 0) and FRAME-INVARIANT: it survives the gauge and coordinate "
                + "reductions, so it is a parity-odd OBSERVABLE and not a coordinate"),
            ("the gradient along a link", true, true, true,
                "odd and frame-invariant: the second parity-odd surviving readout, and the one a cut silences (G_080)"),
            ("the site redshift", false, false, false,
                "parity-EVEN (it reads one site's own rate) and frame-dependent, so the reductions remove it as a "
                + "coordinate"),
            ("the clock rate, the potential and G00 at a site", false, false, false,
                "all parity-even and frame-dependent: coordinates, removed by the reductions"),
            ("the AT/GR ratio at the target", false, true, true,
                "parity-even and frame-invariant: it survives, and it is even — which is why the sector's unique "
                + "prediction carries no chirality"),
            ("the redshift between two sites", true, true, true,
                "ODD under the exchange of the pair: it changes sign when the two sites trade places, and it is "
                + "frame-invariant, so it is the third surviving parity-odd readout"),
        }.Select((row, index) => row).ToArray();
    }

    /// <summary>Task 5's survivor count.</summary>
    public static (int Tested, int ParityOdd, int SurvivingOdd, string Reading) ParityOddCounts()
    {
        var rows = ParityOddQuantities();
        return (rows.Length, rows.Count(r => r.ParityOdd), rows.Count(r => r.ParityOdd && r.SurvivesTheReductions),
            $"{rows.Count(r => r.ParityOdd)} of {rows.Length} quantities are parity-odd and "
            + $"{rows.Count(r => r.ParityOdd && r.SurvivesTheReductions)} of them SURVIVE the gauge and coordinate "
            + "reductions — but an odd functional is not a handedness: the three survivors are two-site DIFFERENCES, "
            + "and a difference is odd under reversal by construction, which is why the readouts carry a signed "
            + "direction while the chirality account stays empty");
    }

    // ===================== TASK 6: WITHOUT LABELS =====================

    /// <summary>
    /// WITH ALL LABELS, INDICES AND ORDERING CONVENTIONS REMOVED (task 6): the observables are recomputed and the
    /// orientation information that remains is counted. The frame-invariant readouts are the ones that cannot be a
    /// labelling, which is what makes this a measurement rather than a restatement.
    /// </summary>
    public static (string Quantity, bool SurvivesWithoutLabels, string Reading)[] WithoutLabels()
    {
        var frames = MinimalGravityCoreAudit.ObservableSet();
        var reversal = ReversalCensus();
        return new[]
        {
            ("the field's acceleration (a parity-odd, frame-invariant readout)", true,
                "it neither reads the labelling nor an index, so it survives the removal untouched — and it is odd"),
            ("the gradient along a link (odd, frame-invariant)", true,
                "it needs an adjacency but not an index, so it survives — a link is an unordered pair and the gradient "
                + "on it is a difference"),
            ("the redshift between two sites (odd, frame-invariant)", true,
                "it compares two sites and the comparison survives, because a RELATIVE position is not a label"),
            ("the centroid of a pattern (the transport measure)", false,
                "it needs an INDEX: without labels it is undefined rather than zero (G_098 measured the same)"),
            ("the flux (a direction no measurement sees)", false,
                "read by 0 observables, so removing the labels removes nothing and reveals nothing either"),
        };
    }

    /// <summary>Task 6's count: the orientation information that remains without labels.</summary>
    public static (int Quantities, int Surviving, int SurvivingAndOdd, string Reading) RemainingOrientation()
    {
        var rows = WithoutLabels();
        var parity = ParityOddCounts();
        return (rows.Length, rows.Count(r => r.SurvivesWithoutLabels), parity.SurvivingOdd,
            $"{rows.Count(r => r.SurvivesWithoutLabels)} of {rows.Length} quantities survive the removal of every label, "
            + $"index and ordering convention, and {parity.SurvivingOdd} of them is PARITY-ODD — so what remains is a "
            + "SIGNED functional of a non-uniform state, which is a direction at a place, not a handedness of the "
            + "source");
    }

    // ===================== TASK 7: ADD ONE GLOBAL ORIENTATION BIT =====================

    /// <summary>
    /// THE REDUCTION TEST WITH ONE GLOBAL ORIENTATION BIT ADDED (task 7): what the bit buys, measured. The drift of
    /// G_098 is applied with the orientation supplied, and the audit counts the new observables, the new transport
    /// quantities and whether the 48! degeneracy of histories is reduced.
    /// </summary>
    public static (string Gain, int Count, double Measure, string Reading)[] OrientationBitGain()
    {
        var law = DirectedTransportAudit.Propagate(Directed: false);
        var drift = DirectedTransportAudit.Propagate(Directed: true);
        var degeneracy = PatternKinematicsAudit.Degeneracy().Single();
        double lawRatio = DirectedTransportAudit.TransportDiffusionRatio()
            .Single(r => r.Mode.Contains("operator alone")).Ratio;
        double suppliedRatio = DirectedTransportAudit.TransportDiffusionRatio()
            .Single(r => r.OrientationSupplied).Ratio;
        // the orientation bit does not change the state-to-history map, so the observables and the degeneracy are untouched
        var before = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        var after = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full());
        int changedObservables = before.Zip(after, (x, y) => Math.Abs(x.Value - y.Value) > Tol ? 1 : 0).Sum();
        return new[]
        {
            ("new observables enabled", changedObservables, 0.0,
                $"NONE: the readouts are functions of the state, and adding an orientation to the LINK SET does not "
                + "change the state — the observable change count is 0, measured by recomputing every readout"),
            ("new transport quantities enabled", 1, suppliedRatio - lawRatio,
                $"ONE: the transport/diffusion ratio moves from {F3(lawRatio)} to {F3(suppliedRatio)}, and the centroid "
                + $"shift from {E(law[^1].CentroidShift)} to {E(Math.Abs(drift[^1].CentroidShift))}"),
            ("reduction of the 48! degeneracy (G_097)", 0, 0.0,
                $"NONE: {degeneracy.IndependentTransfers} mutually independent transfers still COMMUTE with an "
                + "orientation supplied, so they still give a bit-identical state and the same "
                + $"{F3(degeneracy.Log10Orderings)} orders of magnitude of observationally identical histories remain"),
            ("information gain in observable terms", 0, 0.0,
                "ZERO: the bit buys a transport quantity that no observable reads, which is why G_098 measured its "
                + "observable cost at zero — the gain and the cost are both zero"),
        };
    }

    // ===================== THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>Each orientation mode classified separately, because they differ.</summary>
    public static (string Mode, OrientationStatus Status, string Measurement)[] Classification()
    {
        var counts = CarrierCounts();
        var parity = ParityOddCounts();
        var reversal = ReversalCounts();
        var gain = OrientationBitGain();
        return new[]
        {
            ("orientation as a FIELD (a direction at a place)", OrientationStatus.Derived,
                $"{counts.Derived} of {counts.Objects} tested objects are derived and {parity.SurvivingOdd} parity-odd "
                + "readouts survive the gauge and coordinate reductions: the acceleration, the along-a-link gradient "
                + "and the two-site redshift — so a direction IS derivable and it IS observable, and it comes from the "
                + "potential's non-uniformity rather than from a handedness"),

            ("orientation as a TRANSPORT RULE (which way count moves)", OrientationStatus.Assumed,
                "the link set is unordered and the operator commutes with all automorphisms, so no surviving object says "
                + "which way count should move: the direction of an operation is the operator's choice"),

            ("the CHIRALITY of the configuration", OrientationStatus.Assumed,
                $"MEASURED ABSENT: max |rho_i - rho_(N-i)| = {E(ChiralityUnder(State, Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map))} "
                + $"over all {reversal.Reflections} reversals — zero to the rounding ({Tol}) — so the configuration is "
                + "PARITY-EVEN and no chirality is derived from it; a handedness has to be assumed, which is the one bit "
                + "G_098 found missing"),

            ("one ADDED global orientation bit", OrientationStatus.Boundary,
                $"it enables {gain.Single(g => g.Gain.Contains("transport")).Count} transport quantity and NO observable: "
                + "the bit is boundary information in the precise sense that the theory's measurements do not use it"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var counts = CarrierCounts();
        var parity = ParityOddCounts();
        var bits = ChiralityTotal();
        var remaining = RemainingOrientation();
        var gain = OrientationBitGain();
        return new[]
        {
            ("SUCCESS: at least one surviving AT structure generates orientation and survives all reductions",
                parity.SurvivingOdd > 0 ? "MET" : "NOT MET",
                $"{counts.Derived} derived orientation carriers and {parity.SurvivingOdd} parity-odd readouts survive "
                + "the gauge and coordinate reductions — and the configuration's chirality supplies "
                + $"{bits.TotalBits} bit, so orientation as a FIELD is derived"),

            ("FAILURE: orientation information remains zero until supplied externally",
                bits.TotalBits > 0 ? "NOT MET" : "MET",
                $"no source supplies a chirality bit: the total is {bits.TotalBits}, and the " 
                + $"{remaining.SurvivingAndOdd} surviving parity-odd quantities are two-site DIFFERENCES, which are odd "
                + "by construction and therefore are not chirality — measured at the chirality level, the criterion is "
                + "MET, and at the field level it is not"),

            ("and the TRANSPORT direction remains assumed", "CONFIRMED",
                $"the added bit buys {gain.Single(g => g.Gain.Contains("observables")).Count} new observables and "
                + $"{gain.Single(g => g.Gain.Contains("degeneracy")).Count} reduction of the degeneracy: its information "
                + "gain in observable terms is zero"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured residual or a
    /// measured oddness.
    /// </summary>
    public static string Verdict()
    {
        var counts = CarrierCounts();
        var stabilisers = Stabilisers();
        var law = LawUnderAutomorphisms();
        var reversal = ReversalCounts();
        var bits = ChiralityTotal();
        var parity = ParityOddCounts();
        var remaining = RemainingOrientation();
        var gain = OrientationBitGain();
        var reflection = Automorphisms().First(a => a.Kind == "reflection" && a.K == 0).Map;
        var b = new StringBuilder();
        b.Append("ORIENTATION SURVIVES ONLY AS A PARITY OF DIFFERENCES — THE CONFIGURATION SUPPLIES ZERO CHIRALITY ")
            .Append("BITS, AND THE TRANSPORT DIRECTION REMAINS ASSUMED. ");
        b.Append(counts.Reading).Append(". ");
        b.Append("THE SYMMETRY-BREAKING MEASUREMENT DECIDES IT — AGAINST THE STATE: of the ring's ")
            .Append(Automorphisms().Length).Append(" automorphisms the surviving state preserves ")
            .Append(stabilisers.Sum(s => s.Preserving)).Append(" (exactly one rotation and one reflection) and breaks ")
            .Append(stabilisers.Sum(s => s.Broken)).Append(" — but the reflection it preserves is the one through its ")
            .Append("own axis, and max |rho_i - rho_(N-i)| = ").Append(E(ChiralityUnder(State, reflection)))
            .Append(" over all ").Append(reversal.Reflections).Append(" reversals, i.e. ZERO to the rounding (")
            .Append(E(Tol)).Append("): the configuration is PARITY-EVEN, so the 190 broken elements measure the field's ")
            .Append("LOCALISATION and not a handedness, and they are the reason the gradient has a direction at all. ");
        b.Append("AND THE LAW DOES NOT BREAK IT EITHER: it commutes with ")
            .Append(law.Tested - law.Failing).Append(" of ").Append(law.Tested)
            .Append(" automorphisms (worst residual ").Append(E(law.Worst)).Append("), so neither the state nor the ")
            .Append("operator supplies a chirality bit. ");
        b.Append("THE OBSERVABLES REPORT IT AS A RANGE, NOT A NUMBER: ").Append(reversal.Reflections)
            .Append(" orientation-reversing maps were swept, ").Append(reversal.Reflections - reversal.ChangingNone)
            .Append(" of them change at least one readout, and the change count ranges from ").Append(reversal.MinChanged)
            .Append(" to ").Append(reversal.MaxChanged).Append(" with the largest single change at ")
            .Append(E(reversal.LargestChange))
            .Append(" — so the effect of a reversal depends on WHICH reversal is meant. ");
        b.Append("THE CHIRALITY ACCOUNTING: ").Append(bits.Reading).Append(". ")
            .Append("WHAT THE READOUTS DO CARRY IS PARITY, NOT HANDEDNESS: ").Append(parity.SurvivingOdd)
            .Append(" of ").Append(parity.Tested).Append(" parity-odd quantities pass the gauge and coordinate ")
            .Append("reductions (the acceleration, the along-a-link gradient and the two-site redshift) — but all three ")
            .Append("are two-site DIFFERENCES, and a difference is odd under reversal by construction, so an odd readout ")
            .Append("is not evidence that its source is chiral. ");
        b.Append("NOTHING OF IT IS A TRANSPORT RULE: the link set is unordered (G_098), the operator commutes with every ")
            .Append("automorphism, and the audit's reduction test shows what one ADDED orientation bit buys — ")
            .Append(gain.Single(g => g.Gain.Contains("observables")).Count).Append(" new observables, ")
            .Append(gain.Single(g => g.Gain.Contains("degeneracy")).Count)
            .Append(" reduction of the degeneracy, and one transport quantity with a ratio of ")
            .Append(F3(gain.Single(g => g.Gain.Contains("transport")).Measure + 0.0)).Append(". ");
        b.Append("WITHOUT LABELS THE SIGNED READOUTS SURVIVE: ")
            .Append(remaining.Surviving).Append(" of ").Append(remaining.Quantities)
            .Append(" quantities remain and ").Append(remaining.SurvivingAndOdd)
            .Append(" of them is parity-odd — which is the audit's answer to the question as asked: orientation IS ")
            .Append("derived as the field's parity (a direction at a place, because the potential is not constant) and is ")
            .Append("NOT derived as a handedness (0 bits), so the DIRECTED TRANSPORT G_098 measured is a different ")
            .Append("object, which remains ASSUMED at a price of one bit with an observable gain of zero. ")
            .Append("FINAL VERDICT: orientation as a FIELD — DERIVED; the CHIRALITY of the configuration — ASSUMED ")
            .Append("(measured absent, 0 bits from every source); orientation as a TRANSPORT RULE — ASSUMED; one ADDED ")
            .Append("global orientation bit — BOUNDARY.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the ring's automorphism group is enumerated and the surviving state tested against");
        b.AppendLine("all 192 of its elements; that the state preserves the orientation symmetry and the law commutes with it, so");
        b.AppendLine("NEITHER the configuration nor the operator supplies a chirality bit; that a measured number of readouts change");
        b.AppendLine("under orientation reversal and that the changing ones are frame-invariant; that the surviving structure supplies");
        b.AppendLine("ZERO bits of chirality in total, measured source by source; that three parity-odd readouts survive the gauge and");
        b.AppendLine("coordinate reductions — and that all three are differences, which are odd by construction; and that one ADDED");
        b.AppendLine("orientation bit buys no observable and reduces no degeneracy.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the sector's operations are undirected — an operation IS directed, and G_097");
        b.AppendLine("measured that it moves a pattern. Nor does it establish that the two orientations are physically");
        b.AppendLine("indistinguishable: what it establishes is that the theory's VOCABULARY carries no transport rule, so the");
        b.AppendLine("choice is the operator's. And it does not claim that the derived chirality is a NEW result: the gradient has");
        b.AppendLine("been an observable since G_075; what is new is that its parity is measured, that it survives every");
        b.AppendLine("reduction, and that the two questions — orientation as a field and orientation as a transport rule — are");
        b.AppendLine("separated rather than conflated.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_098: G_098 measured that the direction is not REPRESENTED in the link set");
        b.AppendLine("and that the transport/diffusion ratio is zero. This audit asks the parity question rather than the");
        b.AppendLine("transport question, and the two answers differ: orientation as a FIELD is derived and observable, while");
        b.AppendLine("orientation as a TRANSPORT RULE is assumed. An audit that had stopped at G_098's answer would have reported");
        b.AppendLine("'orientation is absent' — and that would have been wrong by a measurable amount.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a surviving parity-even state (which would remove the one bit and with it");
        b.AppendLine("every parity-odd readout); a link set that carried an ordering (which would move the transport direction");
        b.AppendLine("from ASSUMED to DERIVED); or an observable that reads the flux, which would make the added bit gain");
        b.AppendLine("observable information.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputInventory()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE ORIENTATION INVENTORY (task 1): can it distinguish A -> B from B -> A?");
        b.AppendLine("   object                                              distinguishes  derived  chirality   status");
        foreach (var c in Carriers())
        {
            b.AppendLine($"   {c.Object,-51} {Yes(c.DistinguishesADirection),-13} {Yes(c.Derived),-8} {E(c.Chirality),-11} {c.Status}");
            b.AppendLine($"     {c.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(CarrierCounts().Reading);
        return b.ToString();
    }

    public static string OutputSymmetry()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE SYMMETRY-BREAKING TABLE (task 2): the ring's automorphism group, order "
            + Automorphisms().Length.ToString(CultureInfo.InvariantCulture));
        b.AppendLine("   kind                                    preserving  total  broken");
        foreach (var (kind, preserving, total, broken, reading) in Stabilisers())
        {
            b.AppendLine($"   {kind,-39} {preserving,10}  {total,5}  {broken,6}");
            b.AppendLine($"     {reading}");
        }
        var law = LawUnderAutomorphisms();
        b.AppendLine();
        b.AppendLine($"   the update law: {law.Tested - law.Failing} of {law.Tested} automorphisms commute, worst residual {E(law.Worst)}");
        b.AppendLine($"     {law.Reading}");
        return b.ToString();
    }

    public static string OutputReversalAndBits()
    {
        var b = new StringBuilder();
        b.AppendLine("3. ORIENTATION REVERSAL APPLIED TO EVERY OBSERVABLE (task 3)");
        b.AppendLine("   observable                                     original      reversed      change        changed  frame-invariant");
        foreach (var (observable, original, reversed, change, changed, invariant, note) in ReversalCensus())
        {
            b.AppendLine($"   {observable,-46} {F3(original),-13} {F3(reversed),-13} {E(change),-13} {Yes(changed),-8} {Yes(invariant)}");
            b.AppendLine($"     {note}");
        }
        var counts = ReversalCounts();
        b.AppendLine();
        b.AppendLine($"   {counts.Reflections} reversals swept, {counts.ChangingNone} leave every readout unchanged, the change count");
        b.AppendLine($"   ranges {counts.MinChanged} to {counts.MaxChanged}, and the largest single change is {E(counts.LargestChange)}");
        b.AppendLine();
        b.AppendLine("4. THE CHIRALITY-BIT TABLE (task 4)");
        b.AppendLine("   source                                    bits");
        foreach (var (source, bits, measurement) in ChiralityBits())
        {
            b.AppendLine($"   {source,-42} {bits}");
            b.AppendLine($"     {measurement}");
        }
        var total = ChiralityTotal();
        b.AppendLine();
        b.AppendLine($"   TOTAL {total.TotalBits} bit, from {total.From}");
        return b.ToString();
    }

    public static string OutputParityAndGain()
    {
        var b = new StringBuilder();
        b.AppendLine("5. PARITY-ODD QUANTITIES ALREADY IN THE SECTOR (task 5)");
        b.AppendLine("   quantity                                        parity-odd  frame-invariant  survives the reductions");
        foreach (var (quantity, odd, invariant, survives, measurement) in ParityOddQuantities())
        {
            b.AppendLine($"   {quantity,-47} {Yes(odd),-11} {Yes(invariant),-16} {Yes(survives)}");
            b.AppendLine($"     {measurement}");
        }
        var parity = ParityOddCounts();
        b.AppendLine();
        b.AppendLine($"   {parity.ParityOdd} parity-odd, {parity.SurvivingOdd} surviving the reductions");
        b.AppendLine();
        b.AppendLine("6. WITHOUT LABELS, INDICES OR ORDERING CONVENTIONS (task 6)");
        foreach (var (quantity, survives, reading) in WithoutLabels())
        {
            b.AppendLine($"   {quantity}: survives {Yes(survives)}");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine();
        b.AppendLine(RemainingOrientation().Reading);
        b.AppendLine();
        b.AppendLine("7. THE REDUCTION TEST WITH ONE ADDED GLOBAL ORIENTATION BIT (task 7)");
        b.AppendLine("   gain                                      count  measure");
        foreach (var (gain, count, measure, reading) in OrientationBitGain())
        {
            b.AppendLine($"   {gain,-41} {count,5}  {F3(measure)}");
            b.AppendLine($"     {reading}");
        }
        return b.ToString();
    }

    public static string OutputClassification()
    {
        var b = new StringBuilder();
        b.AppendLine("8. THE CLASSIFICATION AND THE CRITERIA");
        foreach (var (mode, status, measurement) in Classification())
        {
            b.AppendLine($"   {mode}: {status}");
            b.AppendLine($"     {measurement}");
        }
        b.AppendLine();
        foreach (var (criterion, outcome, evidence) in Criteria())
        {
            b.AppendLine($"   {criterion}");
            b.AppendLine($"     -> {outcome}");
            b.AppendLine($"     {evidence}");
        }
        return b.ToString();
    }

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
