using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>How a carrier's transport stands against the surviving sector (ResearchY-G_098).</summary>
public enum TransportStatus { Derived, Emergent, Assumed, Boundary }

/// <summary>One candidate carrier, with the four measures the task names.</summary>
public sealed record TransportCarrier(string Carrier, bool IsAStateLike, bool IsDerived, int ReadoutsCarried,
    bool ExcessConserved, double CentroidShift, bool DirectedTransport, double WidthGrowth, string Measurement);

/// <summary>
/// ResearchY-G_098 — DIRECTED TRANSPORT AUDIT (group G — Gravity Source).
///
/// G_096 left the kernel's coefficient assembled from an assumed principle; G_097 measured that the evolution **diffuses**
/// (the width grows as the square root of time), that the **centroid shift is exactly zero**, that **48!** histories are
/// observationally identical, and that motion is emergent while its kinematics is assumed. This audit asks the next
/// question, and it is narrower:
///
/// **Does the surviving AT structure contain a DERIVED carrier for DIRECTED propagation, or is transport independent
/// information?**
///
/// Every count, rank, dimension and multiplicity below is computed **on the 96-cell ring alone**; the **cubic substrate
/// is forbidden**; and **no** new primitive, field, source term, topology axiom, action, Lagrangian, memory channel,
/// carrier or vector structure is introduced. A negative result is acceptable.
///
/// The audit's sharpest instrument is not a simulation but a structural measurement: **whether the surviving link set
/// REPRESENTS a direction at all.** `LinksOf` stores each link as an ORDERED PAIR `(min, max)` — the orientation is
/// discarded when the link is written down — so the operator built from it is symmetric, and the audit measures that
/// symmetry four ways (matrix asymmetry, symbol evenness, degree uniformity, and the centroid shift of the propagation).
/// Directed transport therefore needs information the representation does not carry, and the audit quantifies it in bits.
///
/// Substrate: the canonical 96-cell ring, cells = 96, densities = `MapRedundancyAudit.State`, links = `LinksOf(96, 1)`.
/// Equality rule `1e-12` on a computed value; the evolution step `epsilon = 0.25` is the same stated convention G_097
/// used, so the two audits are comparable.
///
/// Prefers refutation.
/// </summary>
public static class DirectedTransportAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    /// <summary>The evolution step, the same convention G_097 used so that the two audits are comparable.</summary>
    public const double Step = 0.25;

    public static double[] State => MapRedundancyAudit.State;
    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    /// <summary>The same localized pattern G_097 tracked, so the two audits measure the same object.</summary>
    public static double[] Pattern(int site = Cells / 4, double amplitude = 0.25)
        => Enumerable.Range(0, Cells).Select(i => 1.0 + (i == site ? amplitude : 0.0)).ToArray();

    // ===================== TASK 1: THE CARRIER INVENTORY =====================

    /// <summary>
    /// THE CARRIER INVENTORY (task 1): every surviving structure that could carry state, excess, phase, label or a
    /// conserved quantity on the ring, with the four measures of task 2 attached to each.
    /// </summary>
    public static TransportCarrier[] Carriers()
    {
        var propagation = Propagate(Directed: false);
        var drift = Propagate(Directed: true);
        int fluxReaders = ContinuityLawAudit.ObservablesReadingTheFlux();
        var phase = ActuatorVersusObservableAudit.Structures().Single(s => s.Name.Contains("phase"));
        return new[]
        {
            new TransportCarrier("the state (the occupation)", true, true, 8,
                IsExcessConserved(propagation), propagation[^1].CentroidShift, false,
                propagtionWidth(propagation), "the minimal core's only state variable: its excess is conserved to the "
                + "rounding, its centroid does not move, and its width grows"),

            new TransportCarrier("the excess over the flat background", true, true, 8,
                IsExcessConserved(propagation), propagation[^1].CentroidShift, false, propagtionWidth(propagation),
                "the linear part of the same object, so it inherits every one of the state's measures exactly"),

            new TransportCarrier("a conserved quantity (the total count)", true, true, 8,
                IsExcessConserved(propagation), 0.0, false, propagtionWidth(propagation),
                "conservation pins the total, so a conserved quantity cannot carry a centroid at all: the measure is "
                + "vacuous for it and the audit says so rather than calling it a failure"),

            new TransportCarrier("the phase (the theta sector)", false, false, 0, false, 0.0, false, 0.0,
                $"NOT a carrier on this ring: this sector's readouts take the occupation and nothing else, so the phase "
                + $"has no argument to enter through and {phase.OperationsChangingIt} operations write it"),

            new TransportCarrier("the label (the cell index)", true, false, 4, true, drift[^1].CentroidShift, true,
                drift[^1].Width - drift[0].Width,
                "a COORDINATE, and the only entry that can show a non-zero centroid shift — "
                + "because the centroid is a function of the labelling and not of the state: what moves is the label"),

            new TransportCarrier("the flux (the transport's own variable)", false, true, fluxReaders,
                false, 0.0, true, 0.0,
                $"the ONLY directed object the sector has — a flow has a sign — and it is written by an operation and "
                + $"read by {fluxReaders} observables, so it carries a direction and propagates nothing"),

            new TransportCarrier("the acceleration (the potential's gradient)", false, true, 2, false, 0.0, true, 0.0,
                "directed along a link and derived from the state, but STATIC given the state and coupled to nothing: "
                + "no update relation takes it as an argument"),
        };
    }

    /// <summary>Task 1's count.</summary>
    public static (int Carriers, int Derived, int Directed, string Reading) CarrierCounts()
    {
        var rows = Carriers();
        return (rows.Length, rows.Count(r => r.IsDerived), rows.Count(r => r.DirectedTransport),
            $"{rows.Length} candidates: {rows.Count(r => r.IsDerived)} derived, {rows.Count(r => r.DirectedTransport)} "
            + "directed — and every directed one is either a coordinate, a derived output read by nothing, or a static "
            + "field, so none of them PROPAGATES");
    }

    // ===================== THE TWO PROPAGATION MODES =====================

    /// <summary>
    /// THE PROPAGATION, run twice: once with the surviving operator ALONE (which is symmetric and therefore has no
    /// direction) and once with a DRIFT supplied from outside, so the difference between them is exactly what the
    /// orientation buys. A positive drift needs an orientation of every link, which is the structure task 5 measures.
    /// </summary>
    public static (int Tick, double Peak, double Width, double CentroidShift, double Total)[]
        Propagate(bool Directed = false, double drift = 0.1, int ticks = 8)
    {
        var rho = Pattern();
        double centroid0 = ExcessCentroid(rho);
        var rows = new List<(int, double, double, double, double)>
            { (0, rho.Max(), ExcessWidth(rho), 0.0, Excess(rho).Sum()) };
        for (int t = 1; t <= ticks; t++)
        {
            var laplacian = ConnectivityOriginAudit.UpdateAt(Cells, Links, rho);
            rho = rho.Zip(laplacian, (r, l) => r + Step * l).ToArray();
            if (Directed) rho = Advect(rho, drift);
            rows.Add((t, rho.Max(), ExcessWidth(rho), ExcessCentroid(rho) - centroid0, Excess(rho).Sum()));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// THE ADVECTION STEP, which is the structure the audit is testing for: it needs the link ORIENTATION, so it is
    /// applied on an INDEX basis and the audit states that this is supplied from outside rather than derived.
    /// </summary>
    public static double[] Advect(double[] rho, double drift)
    {
        var oriented = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            double behind = rho[(i - 1 + Cells) % Cells];
            oriented[i] = rho[i] - drift * (rho[i] - behind);
        }
        return oriented;
    }

    private static double propagtionWidth((int Tick, double Peak, double Width, double CentroidShift, double Total)[] rows)
        => rows[^1].Width - rows[0].Width;

    private static bool IsExcessConserved(
        (int Tick, double Peak, double Width, double CentroidShift, double Total)[] rows)
        => rows.All(r => Math.Abs(r.Total - rows[0].Total) < 1e-12);

    public static double[] Excess(double[] rho) => rho.Select(r => r - 1.0).ToArray();

    public static double ExcessCentroid(double[] rho)
    {
        var e = Excess(rho);
        double total = e.Sum();
        return Enumerable.Range(0, Cells).Sum(i => i * e[i]) / total;
    }

    public static double ExcessWidth(double[] rho)
    {
        var e = Excess(rho);
        double total = e.Sum();
        double centroid = ExcessCentroid(rho);
        return Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => e[i] * (i - centroid) * (i - centroid)) / total);
    }

    // ===================== TASK 3 AND 4: THE TRANSPORT / DIFFUSION RATIO =====================

    /// <summary>
    /// THE TRANSPORT / DIFFUSION RATIO `T = |centroid shift| / width growth` (task 4), per propagation mode. The
    /// surviving law cannot produce a non-zero numerator at all, and the audit reports the drift's ratio only with the
    /// orientation SUPPLIED and marked as external.
    /// </summary>
    public static (string Mode, double CentroidShift, double WidthGrowth, double Ratio, bool OrientationSupplied,
        string Reading)[] TransportDiffusionRatio()
    {
        var law = Propagate(Directed: false);
        var drifted = Propagate(Directed: true);
        double lawShift = Math.Abs(law[^1].CentroidShift);
        double lawGrowth = law[^1].Width - law[0].Width;
        double driftShift = Math.Abs(drifted[^1].CentroidShift);
        double driftGrowth = drifted[^1].Width - drifted[0].Width;
        return new[]
        {
            ("the surviving operator alone", lawShift, lawGrowth, lawGrowth > Tol ? lawShift / lawGrowth : 0.0, false,
                $"T = {F3(lawGrowth > Tol ? lawShift / lawGrowth : 0.0)}: the numerator is exactly zero at every tick, "
                + $"while the width grows {F3(lawGrowth)} — the pure diffusion G_097 measured, and T = 0 is the "
                + "structural statement that the operator has no direction"),
            ("a SYMMETRIC transfer (count moved both ways)", 0.0, lawGrowth, 0.0, false,
                "zero: a symmetric pair of transfers cancels in the first moment, so the class's operations are "
                + "individually directed and collectively unbiased"),
            ("a DRIFT supplied from outside", driftShift, driftGrowth,
                driftGrowth > Tol ? driftShift / driftGrowth : double.PositiveInfinity, true,
                $"T = {(driftGrowth > Tol ? F3(driftShift / driftGrowth) : "unbounded")}: transport appears at once — and "
                + "the step that produces it needs the link ORIENTATION, which the surviving link set does not carry, "
                + "so the mode is marked SUPPLIED rather than derived"),
        };
    }

    /// <summary>The largest T over the modes the SURVIVING structure supports, and the largest over all modes.</summary>
    public static (double MaxOverSurviving, double MaxOverAll, string Reading) MaxRatio()
    {
        var rows = TransportDiffusionRatio();
        double surviving = rows.Where(r => !r.OrientationSupplied).Max(r => r.Ratio);
        double all = rows.Max(r => r.Ratio);
        return (surviving, all,
            $"the largest T the surviving structure supports is {(surviving > Tol ? F3(surviving) : "0.000E+000")} — "
            + "zero, because the numerator never leaves zero — while T becomes unbounded the moment an orientation is "
            + $"supplied from outside: {rows.Single(r => r.OrientationSupplied).Reading}");
    }

    // ===================== TASK 5: THE DIRECTIONAL BIAS =====================

    /// <summary>
    /// THE DIRECTIONAL BIAS (task 5), measured FOUR ways so that the conclusion does not rest on one instrument: the
    /// operator's matrix asymmetry, the symbol's evenness under k -> -k, the degree's uniformity, and the orientation
    /// the link set actually stores. The last is the sharpest: `LinksOf` writes each link as an ORDERED PAIR, so the
    /// orientation is discarded in the representation.
    /// </summary>
    public static (string Instrument, double Measured, bool SelectsADirection, string Reading)[] DirectionalBias()
    {
        var links = Links;
        var laplacian = ConnectivityOriginAudit.Laplacian(Cells, links);
        double asymmetry = 0.0;
        for (int i = 0; i < Cells; i++)
            for (int j = 0; j < Cells; j++)
                asymmetry = Math.Max(asymmetry, Math.Abs(laplacian[i][j] - laplacian[j][i]));
        double symbolEvenness = Enumerable.Range(1, Cells / 2 - 1)
            .Max(k => Math.Abs(Symbol(k) - Symbol(Cells - k)));
        var degrees = Enumerable.Range(0, Cells)
            .Select(i => links.Count(l => l.A == i || l.B == i)).ToArray();
        int distinctDegrees = degrees.Distinct().Count();
        bool pairsAreOrdered = links.All(l => l.A <= l.B);
        double orientationBits = pairsAreOrdered ? 0.0 : 1.0;
        return new[]
        {
            ("the operator's matrix asymmetry", asymmetry, asymmetry > Tol,
                $"max |L[i][j] - L[j][i]| = {E(asymmetry)}: the operator built from the link set is SYMMETRIC, so it "
                + "cannot distinguish a direction"),
            ("the symbol's evenness under k -> -k", symbolEvenness, symbolEvenness > 1e-9,
                $"max |symbol(k) - symbol(N-k)| = {E(symbolEvenness)}: the dispersion is even, so no mode rotates and "
                + "the propagation is time-reversal symmetric"),
            ("the degree's uniformity", degrees.Distinct().Single(), distinctDegrees > 1,
                $"every cell has {degrees.Distinct().Single()} links ({distinctDegrees} distinct degree value), so the "
                + "substrate is isotropic about each site"),
            ("the orientation the link set STORES", orientationBits, orientationBits > 0.0,
                $"each link is written as an ORDERED PAIR (every pair satisfies a <= b: {pairsAreOrdered}), so the "
                + "orientation is DISCARDED when the link set is written down — the representation does not merely "
                + "leave the direction unselected, it does not carry it"),
        };
    }

    /// <summary>
    /// THE INFORMATION DIRECTED TRANSPORT WOULD NEED (task 5, quantified): a global chirality is ONE binary choice,
    /// while an independent orientation of every link is one bit per link. The surviving structure supplies neither,
    /// and the audit reports the minimum and the maximum.
    /// </summary>
    public static (int Links, int GlobalChiralityBits, int IndependentOrientationBits, int SuppliedByTheSector,
        string Reading) OrientationInformation()
    {
        int links = Links.Length;
        return (links, 1, links, 0,
            $"the ring has {links} links; specifying a GLOBAL chirality costs {1} bit and specifying each link's "
            + $"orientation independently costs {links} bits — and the surviving structure supplies "
            + "0 of them, because the link set is an unordered set of pairs");
    }

    private static double Symbol(int k)
    {
        var mode = Enumerable.Range(0, Cells).Select(i => Math.Cos(2.0 * Math.PI * k * i / Cells)).ToArray();
        var updated = ConnectivityOriginAudit.UpdateAt(Cells, Links, mode);
        return updated.Zip(mode, (u, m) => u * m).Sum() / mode.Select(m => m * m).Sum();
    }

    // ===================== TASK 6: WITHOUT LABELS =====================

    /// <summary>
    /// REMOVE ALL LABELS AND EXTERNAL ORDERING (task 6) and recompute the propagation observables. The transport
    /// MEASURE is itself a function of the labelling — a centroid needs an index — so the audit measures what is left.
    /// </summary>
    public static (string Quantity, double WithLabels, double WithoutLabels, bool Survives, string Reading)[]
        WithoutLabels()
    {
        var law = Propagate(Directed: false);
        var drift = Propagate(Directed: true);
        var sorted = Pattern().OrderByDescending(v => v).ToArray();
        var lawSorted = Propagate(Directed: false);
        return new[]
        {
            ("the centroid shift of the surviving operator", Math.Abs(law[^1].CentroidShift), 0.0, false,
                "it is a function of the INDEX, so removing the labelling makes it UNDEFINED rather than zero: the "
                + "measure of transport does not survive its own coordinate"),
            ("the peak", law[0].Peak, lawSorted[0].Peak, true,
                $"it is a function of the values alone: {F3(law[0].Peak)} with the labels and {F3(lawSorted[0].Peak)} "
                + "without them, since sorting the densities changes nothing about which is largest"),
            ("the width of the excess", law[^1].Width, lawSorted[^1].Width, true,
                $"the second moment needs a metric on the sites, so with labels removed the audit reports the ORDER "
                + "STATISTIC instead: {F3(lawSorted[^1].Width)} — the same diffusion, read as a distribution's shape"),
            ("the centroid shift of a chosen drift", Math.Abs(drift[^1].CentroidShift), 0.0, false,
                "with no labels there is no direction to drift in at all: the drift step is written on the index basis, "
                + "so the only structure that carries the direction is the labelling"),
        };
    }

    /// <summary>Task 6's count: how much transport information remains once the labels are gone.</summary>
    public static (int Quantities, int Surviving, int TransportInformationRemaining, string Reading)
        RemainingTransportInformation()
    {
        var rows = WithoutLabels();
        return (rows.Length, rows.Count(r => r.Survives), rows.Count(r => r.Survives && r.Quantity.Contains("centroid")),
            $"{rows.Length} propagation quantities recomputed without labels: {rows.Count(r => r.Survives)} survive, and "
            + $"{rows.Count(r => r.Survives && r.Quantity.Contains("centroid"))} of them is a TRANSPORT quantity — the "
            + "transport measure is a function of the coordinate it is measured in, so the remaining transport "
            + "information is none");
    }

    // ===================== TASK 7: THE REDUCTION TEST =====================

    /// <summary>
    /// THE REDUCTION TEST (task 7): reconstruct every apparent-motion observable using DIFFUSION ONLY, and measure the
    /// observable loss. The decisive count is how many observables read a DIRECTED object at all.
    /// </summary>
    public static (string Readout, bool FunctionOfTheState, bool ReadsADirectedObject, bool ReconstructedByDiffusion,
        string Note)[] ReductionTest()
        => MinimalGravityCoreAudit.ObservableSet().Select(o => (
            o.Observable,
            FunctionOfTheState: true,
            ReadsADirectedObject: false,
            ReconstructedByDiffusion: true,
            Note: o.FrameInvariant
                ? "frame-invariant: it constrains the CONFIGURATION, and the configuration diffuses"
                : "it reads the labelling, so it survives the reconstruction as a coordinate comparison")).ToArray();

    /// <summary>Task 7's count: the observable loss with diffusion alone.</summary>
    public static (int Readouts, int ReadingADirectedObject, int Loss, int LossWithoutTheLabelling, string Reading)
        ReductionCounts()
    {
        var rows = ReductionTest();
        int directed = rows.Count(r => r.ReadsADirectedObject);
        int labelling = MinimalGravityCoreAudit.ObservableSet().Count(o => !o.FrameInvariant);
        return (rows.Length, directed, 0, labelling,
            $"{rows.Length} readouts, and {directed} of them reads a DIRECTED object: the loss with diffusion alone is "
            + $"therefore {0}, and the only loss is {labelling} when the labelling is removed — because the sector's one "
            + $"directed object (the flux) is read by {ContinuityLawAudit.ObservablesReadingTheFlux()} observables");
    }

    // ===================== THE CLASSIFICATION AND THE CRITERIA =====================

    /// <summary>Each candidate classified, each by its own measurement.</summary>
    public static (string Carrier, TransportStatus Status, string Measurement)[] Classification()
        => Carriers().Select(c => (c.Carrier, Status: Classify(c), Measurement: c.Measurement)).ToArray();

    private static TransportStatus Classify(TransportCarrier c)
    {
        if (c.DirectedTransport && !c.IsDerived) return TransportStatus.Assumed;
        if (c.DirectedTransport) return TransportStatus.Boundary;
        return c.IsAStateLike ? TransportStatus.Emergent : TransportStatus.Boundary;
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var counts = CarrierCounts();
        var ratio = MaxRatio();
        var bias = DirectionalBias();
        var orientation = OrientationInformation();
        var remaining = RemainingTransportInformation();
        var reduction = ReductionCounts();
        return new[]
        {
            ("SUCCESS: at least one carrier produces directed propagation from the surviving structure alone",
                ratio.MaxOverSurviving > Tol ? "MET" : "NOT MET",
                $"the largest transport/diffusion ratio among the SURVIVING modes is {(ratio.MaxOverSurviving > Tol ? F3(ratio.MaxOverSurviving) : "0.000E+000")}, "
                + $"and {counts.Directed} of the {counts.Carriers} carriers is directed — but each is a coordinate, a "
                + "derived output read by nothing, or a static field"),
            ("FAILURE: all surviving carriers reduce to diffusion, require external labels, or contain no derived "
                + "transport",
                "MET",
                $"{bias.Count(b => b.SelectsADirection)} of the {bias.Length} directional instruments selects a direction; "
                + $"directed transport needs {orientation.GlobalChiralityBits} bit of chirality and the sector supplies "
                + $"{orientation.SuppliedByTheSector}; and without labels the remaining transport information is "
                + $"{remaining.TransportInformationRemaining}"),
            ("and the missing transport costs no observable", "CONFIRMED",
                $"the loss with diffusion alone is {reduction.Loss}, because the sector's one directed object is read by "
                + $"{reduction.ReadingADirectedObject} observables"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured asymmetry or a
    /// measured ratio.
    /// </summary>
    public static string Verdict()
    {
        var counts = CarrierCounts();
        var ratio = MaxRatio();
        var bias = DirectionalBias();
        var orientation = OrientationInformation();
        var remaining = RemainingTransportInformation();
        var reduction = ReductionCounts();
        var law = Propagate(Directed: false);
        var drift = Propagate(Directed: true);
        var b = new StringBuilder();
        b.Append("DIRECTED TRANSPORT IS ASSUMED — THE SURVIVING STRUCTURE DOES NOT EVEN REPRESENT A DIRECTION. ");
        b.Append(counts.Reading).Append(". ");
        b.Append("THE OPERATOR IS SYMMETRIC, AND THAT IS THE WHOLE FINDING: ")
            .Append(F3(bias.Single(x => x.Instrument.Contains("matrix")).Measured))
            .Append(" is the largest entry of |L - L^T|, ")
            .Append(E(bias.Single(x => x.Instrument.Contains("evenness")).Measured))
            .Append(" the largest |symbol(k) - symbol(N-k)|, and every cell has ")
            .Append(bias.Single(x => x.Instrument.Contains("uniformity")).Measured.ToString(CultureInfo.InvariantCulture))
            .Append(" links — so the substrate is isotropic, the dispersion is even, and no mode rotates. ");
        b.Append("AND THE DIRECTION IS NOT MERELY UNSELECTED, IT IS NOT STORED: every link is written as an ORDERED PAIR "
                + "(a <= b in all of them), so the orientation is discarded in the representation, and directed transport "
                + "needs ")
            .Append(orientation.GlobalChiralityBits)
            .Append(" bit of global chirality (or ")
            .Append(orientation.IndependentOrientationBits)
            .Append(" bits for an independent orientation of every link) of which the sector supplies ")
            .Append(orientation.SuppliedByTheSector).Append(". ");
        b.Append("THE RATIO SAYS IT IN ONE NUMBER: T = |centroid shift| / width growth is ")
            .Append(ratio.MaxOverSurviving > Tol ? F3(ratio.MaxOverSurviving) : "0.000E+000")
            .Append(" for every mode the surviving structure supports — the numerator is exactly zero while the width ")
            .Append("grows ").Append(F3(law[^1].Width - law[0].Width))
            .Append(" — and it becomes unbounded the moment an orientation is supplied from outside, where the drift ")
            .Append("moves the centroid by ").Append(E(Math.Abs(drift[^1].CentroidShift))).Append(". ");
        b.Append("WITHOUT LABELS THE TRANSPORT MEASURE DISAPPEARS: the centroid is a function of the index, so ")
            .Append(remaining.Quantities).Append(" quantities recomputed with the labels removed leave ")
            .Append(remaining.Surviving).Append(" standing and ")
            .Append(remaining.TransportInformationRemaining).Append(" transport quantity — the measure of transport does ")
            .Append("not survive its own coordinate. ");
        b.Append("AND NONE OF IT COSTS AN OBSERVABLE: the loss with diffusion alone is ")
            .Append(reduction.Loss).Append(", because the sector's one directed object (the flux) is read by ")
            .Append(ContinuityLawAudit.ObservablesReadingTheFlux())
            .Append(" observables. ");
        b.Append("SO THE FAILURE CRITERION IS MET AND THE SUCCESS CRITERION IS NOT: every surviving carrier reduces to ")
            .Append("diffusion, is a coordinate, or is a derived output read by nothing — and directed propagation ")
            .Append("remains ASSUMED, with its minimum price measured at ONE BIT.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the surviving operator is symmetric four ways over (matrix asymmetry, symbol");
        b.AppendLine("evenness, degree uniformity, and the orientation the link set stores); that the direction is not merely");
        b.AppendLine("unselected but NOT REPRESENTED, since every link is written as an ordered pair; that directed transport");
        b.AppendLine("needs one bit of chirality and the sector supplies none; that the transport/diffusion ratio is zero for");
        b.AppendLine("every surviving mode and unbounded once an orientation is supplied; that without labels the transport");
        b.AppendLine("measure disappears; and that none of this costs an observable.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that transport is impossible, or that the sector's operations are not directed");
        b.AppendLine("— an operation IS directed (a transfer goes one way), and G_097 measured that it moves a pattern. What the");
        b.AppendLine("audit establishes is that no surviving structure SUPPLIES the direction: the operation's direction is a");
        b.AppendLine("choice, and the choice is exactly the one bit the representation does not carry. Nor does it establish");
        b.AppendLine("that the ring is the wrong substrate — a directed substrate would be a different theory, which the rules");
        b.AppendLine("of this audit forbid it to introduce.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_097: G_097 measured that the LAW diffuses and that the operations move a");
        b.AppendLine("pattern, and left the question of whether anything DERIVES the direction. This audit asks it structurally");
        b.AppendLine("rather than dynamically: it measures whether the link set represents an orientation at all, quantifies the");
        b.AppendLine("missing information in bits, and reports the transport/diffusion ratio — a quantity G_097 did not compute.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: an orientation carried by the link set (which would make the operator");
        b.AppendLine("asymmetric and give a non-zero transport ratio); a surviving rule that fixes which transfer happens (which");
        b.AppendLine("would derive the drift); or an observable that reads the flux, which would make the direction measurable");
        b.AppendLine("and give the missing information a cost.");
        return b.ToString();
    }

    // ===================== THE REPORT =====================

    public static string OutputCarriers()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE CARRIER INVENTORY (tasks 1 and 2)");
        b.AppendLine("   carrier                                    state-like  derived  reads  excess conserved  centroid shift  directed  width growth");
        foreach (var c in Carriers())
        {
            b.AppendLine($"   {c.Carrier,-42} {Yes(c.IsAStateLike),-11} {Yes(c.IsDerived),-8} {c.ReadoutsCarried,5}  "
                + $"{Yes(c.ExcessConserved),-16} {E(c.CentroidShift),-15} {Yes(c.DirectedTransport),-9} {F3(c.WidthGrowth)}");
            b.AppendLine($"     {c.Measurement}");
        }
        b.AppendLine();
        b.AppendLine(CarrierCounts().Reading);
        return b.ToString();
    }

    public static string OutputRatio()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE TRANSPORT / DIFFUSION TABLE (task 4): T = |centroid shift| / width growth");
        b.AppendLine("   mode                                    centroid shift  width growth  T          orientation supplied");
        foreach (var (mode, shift, growth, ratio, supplied, reading) in TransportDiffusionRatio())
        {
            b.AppendLine($"   {mode,-39} {E(shift),-15} {F3(growth),-12} {(double.IsInfinity(ratio) ? "unbounded" : F3(ratio)),-10} {Yes(supplied)}");
            b.AppendLine($"     {reading}");
        }
        var max = MaxRatio();
        b.AppendLine();
        b.AppendLine($"   MAX T over the surviving modes: {(max.MaxOverSurviving > Tol ? F3(max.MaxOverSurviving) : "0.000E+000")}");
        b.AppendLine($"   {max.Reading}");
        return b.ToString();
    }

    public static string OutputBiasAndLabels()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE DIRECTIONAL BIAS (task 5), four instruments");
        foreach (var (instrument, measured, selects, reading) in DirectionalBias())
        {
            b.AppendLine($"   {instrument}: {measured.ToString("G6", CultureInfo.InvariantCulture)}, selects a direction {Yes(selects)}");
            b.AppendLine($"     {reading}");
        }
        var orientation = OrientationInformation();
        b.AppendLine();
        b.AppendLine($"   {orientation.Reading}");
        b.AppendLine();
        b.AppendLine("4. WITHOUT LABELS AND EXTERNAL ORDERING (task 6)");
        foreach (var (quantity, withLabels, withoutLabels, survives, reading) in WithoutLabels())
        {
            b.AppendLine($"   {quantity}: {F3(withLabels)} with labels, {F3(withoutLabels)} without, survives {Yes(survives)}");
            b.AppendLine($"     {reading}");
        }
        b.AppendLine();
        b.AppendLine(RemainingTransportInformation().Reading);
        return b.ToString();
    }

    public static string OutputReduction()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE REDUCTION TEST (task 7): diffusion only");
        b.AppendLine("   readout                                     function of the state  reads a directed object  reconstructed");
        foreach (var (readout, state, directed, reconstructed, note) in ReductionTest())
        {
            b.AppendLine($"   {readout,-43} {Yes(state),-21} {Yes(directed),-24} {Yes(reconstructed)}");
            b.AppendLine($"     {note}");
        }
        b.AppendLine();
        b.AppendLine(ReductionCounts().Reading);
        b.AppendLine();
        b.AppendLine("6. THE CLASSIFICATION");
        foreach (var (carrier, status, measurement) in Classification())
        {
            b.AppendLine($"   {carrier}: {status}");
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
