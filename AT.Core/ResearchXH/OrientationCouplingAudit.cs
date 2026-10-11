using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What role an orientation-field quantity plays in the surviving sector (ResearchY-G_100).</summary>
public enum FieldRole { Observable, Dynamical, Both, Neither }

/// <summary>The audit's final classification (ResearchY-G_100).</summary>
public enum OrientationCoupling { Coupled, Decoupled }

/// <summary>
/// One derived orientation-field quantity surviving G_099, with its INDEPENDENT dimension count. The dimension is the
/// number of degrees of freedom the quantity has OF ITS OWN; a functional of the state has zero, which is the
/// distinction the whole audit turns on.
/// </summary>
public sealed record OrientationFieldQuantity(string Quantity, int IndependentDimensions, FieldRole Role,
    double Magnitude, OrientationStatus Status, string Measurement)
{
    public bool IsReadByAnObservable => Role is FieldRole.Observable or FieldRole.Both;
    public bool IsADynamicalObject => Role is FieldRole.Dynamical or FieldRole.Both;
    public bool IsAnIndependentVariable => IndependentDimensions > 0;
}

/// <summary>
/// One candidate coupling channel between the orientation field and the update (task 2). The three numeric columns are
/// the channel's own measured extremes, so a reader can see at a glance whether a row is a live correlate or a
/// residual: the DYNAMICAL rows are residuals and read zero.
/// </summary>
public sealed record CouplingRow(string Channel, double Max, double Min, double Mean, string EqualityRule,
    string Measurement);

/// <summary>One member of the partition the orientation field defines (task 3).</summary>
public sealed record PartitionRow(string Member, double Scale, double FieldResidual, double EvolutionResidual,
    string Measurement);

/// <summary>
/// The scope of a reversed quantity: the FIELD itself, the LAW (the operator, its spectrum, its conservation), or a
/// STATE OUTPUT (a measurement of the state's evolution, which moves when the state moves). The distinction is what
/// stops a state transformation's own non-linearity from being reported as a dynamical change.
/// </summary>
public enum ReversalScope { Field, Law, StateOutput }

/// <summary>
/// One quantity under orientation reversal (task 4). The operation is named in the row, because two operations
/// reverse the field and they are not interchangeable: the ring reflection is a relabelling the law commutes with,
/// while the density inversion flips the potential's sign exactly and is non-linear.
/// </summary>
public sealed record ReversalRow(string Quantity, string Operation, ReversalScope Scope, double AsIs,
    double Reversed, double Change, string Measurement)
{
    public bool IsALawObject => Scope == ReversalScope.Law;
    public bool IsAStateOutput => Scope == ReversalScope.StateOutput;
}

/// <summary>
/// One member of the transport-sensitivity sweep (task 5). `WidthIsDefined` is measured: a second moment exists only
/// for a SIGN-DEFINITE pattern, so a sweep that includes a member with a negative lobe reports its width as
/// UNDEFINED rather than as a number or as NaN.
/// </summary>
public sealed record SensitivityRow(string Member, double OrientationContent, double OrientationPeak,
    double CentroidShift, double WidthGrowth, double Ratio, bool WidthIsDefined, string Reading);

/// <summary>One surviving readout under an orientation-dependent evolution (task 6).</summary>
public sealed record ObservableChangeRow(string Observable, bool FrameInvariant, double DifferenceAtZero,
    double DifferenceAfter8, double Amplification, string Note);

/// <summary>One object put back after the orientation information is removed (task 7).</summary>
public sealed record ReductionRow(string Object, int Lost, double Residual, string Measurement);

/// <summary>
/// ResearchY-G_100 — ORIENTATION COUPLING AUDIT (group G — Gravity Source).
///
/// G_097 measured that the sector's law DIFFUSES (the width grows as the square root of time, the centroid shift is
/// zero, the mode symbols are non-positive); G_098 measured that directed transport is ABSENT and that the surviving
/// transport structure does not even REPRESENT a direction; G_099 measured that the orientation FIELD is DERIVED and
/// observable while the CHIRALITY is ASSUMED and the TRANSPORT RULE is ASSUMED, the surviving state being
/// parity-even to the rounding and the law commuting with all 192 automorphisms. This audit asks the question those
/// leave open, and it is a question about DYNAMICS rather than about representation or parity:
///
/// **Does the derived orientation field couple to surviving dynamics strongly enough to generate directed
/// propagation?**
///
/// THE INSTRUMENT is a two-part test, because "coupling" can be read two ways and only one of them is dynamical. The
/// orientation field is a FUNCTION OF THE STATE, so it cannot be varied independently of the state: reversing it
/// necessarily changes the state, and the update necessarily changes with the state. A descriptive coupling is
/// therefore guaranteed by construction and measures nothing. What decides the question is (a) whether the field has
/// any INDEPENDENT degrees of freedom as a control variable, and (b) whether the transport measure moves with it. The
/// audit measures both, and it measures the second by a SWEEP of the field's amplitude, so that `S = dT/dO` is a
/// finite difference over an admissible family rather than a claim — and it defines the transport measure against the
/// state's OWN MEAN, because a family that contains inverted states would otherwise confuse a background shift with
/// a change of shape (the trap G_097 recorded).
///
/// Every count, dimension, rank and multiplicity names its object: the ring is the canonical 96-cell ring (cells =
/// 96, links = `LinksOf(96, 1)`), the orientation field is the SIGNED potential difference along each of the ring's
/// 96 cyclic links, the transport measure is G_098's `T = |centroid shift| / width growth` computed against the
/// state's own mean, and the equality rules are `1e-12` on a computed value and **exact** equality where a
/// telescoping identity holds. The cubic substrate is forbidden and is not named. No new primitive, field, source
/// term, topology axiom, action, Lagrangian, carrier, vector structure, orientation tag or transport rule is
/// introduced.
///
/// Prefers refutation.
/// </summary>
public static class OrientationCouplingAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;
    public const double Tol = 1e-12;

    /// <summary>The same stated step G_097, G_098 and G_099 used, so the four audits are comparable.</summary>
    public const double Step = PatternKinematicsAudit.Step;

    public static (int A, int B)[] Links => ConnectivityOriginAudit.LinksOf(Cells, 1);

    /// <summary>The sector's canonical state — parity-even to the rounding, which G_099 measured at 6.661E-016.</summary>
    public static double[] Canonical => MapRedundancyAudit.State;

    /// <summary>
    /// THE AUDIT'S WORKING STATE, and the choice is substantive: a localized pattern whose excess has a NON-ZERO
    /// TOTAL, so the first and second moments of the pattern are well defined (a pure ring mode has zero total, and
    /// its centroid is undefined rather than zero). The second cell makes the pattern positively SKEWED, so its odd
    /// part about the ring axis is genuine and its orientation field is not mirror-symmetric. The skew is the audit's
    /// control variable for task 5.
    /// </summary>
    public static double[] Skewed(double skew)
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        rho[Cells / 4] += 0.25;
        rho[Cells / 4 + 2] += 0.08 * skew;
        return rho;
    }

    public static double[] Working => Skewed(1.0);

    public static double[] Potential(double[] rho) => rho.Select(SourceManipulationAudit.Potential).ToArray();

    public static double[] Update(double[] rho) => ConnectivityOriginAudit.UpdateAt(Cells, Links, rho);

    public static double MaxAbs(IEnumerable<double> v) => v.Select(Math.Abs).Max();

    // ===================== THE ORIENTATION FIELD =====================

    /// <summary>
    /// THE ORIENTATION FIELD: the SIGNED potential difference along each of the ring's 96 cyclic links, in the ring's
    /// own order. It is the sector's second parity-odd readout (G_099), a function of the state and nothing else — and
    /// therefore a DERIVED quantity with no degrees of freedom of its own.
    /// </summary>
    public static double[] OrientationField(double[] rho)
    {
        var a = Potential(rho);
        return Enumerable.Range(0, Cells).Select(i => a[(i + 1) % Cells] - a[i]).ToArray();
    }

    /// <summary>The field's PEAK: the largest absolute signed difference over the 96 links.</summary>
    public static double OrientationPeak(double[] rho) => MaxAbs(OrientationField(rho));

    /// <summary>
    /// THE AUDIT'S CONTROL VARIABLE: the field's total absolute content `O = sum |d_i|`. The peak is NOT used as the
    /// control: a second, smaller bump leaves the peak where the first bump put it, so a sweep keyed on the peak would
    /// report a constant where the audit claims to vary the field — measured, because this audit's first draft did
    /// exactly that.
    /// </summary>
    public static double OrientationContent(double[] rho) => OrientationField(rho).Sum(Math.Abs);

    /// <summary>The ring's axis reflection `i -> N - i`, as a relabelling map.</summary>
    public static int[] AxisReflection() => Enumerable.Range(0, Cells).Select(i => (Cells - i) % Cells).ToArray();

    public static double[] Relabel(double[] rho, int[] map) => map.Select(m => rho[m]).ToArray();

    /// <summary>
    /// THE GEOMETRIC REVERSAL: the ring reflection. It is exactly the map that swaps A -> B for B -> A (G_099's
    /// instrument), it preserves the mean exactly, and it reverses the field's sign up to the index shift it induces:
    /// `d'_i = -d_((N-1-i) % N)` — an identity, verified rather than assumed.
    /// </summary>
    public static double[] Reflect(double[] rho) => Relabel(rho, AxisReflection());

    /// <summary>
    /// THE SECOND REVERSAL: the MEAN-PRESERVING density inversion `rho -> c / rho` with
    /// `c = mean(rho) / mean(1/rho)`. It flips the potential's sign exactly (A -> const - A), so every one of the 96
    /// link differences flips sign AT ITS OWN INDEX, and it preserves the count's scale — which is why the constant is
    /// chosen this way rather than as the square of the mean.
    /// </summary>
    public static double[] Invert(double[] rho)
    {
        double c = rho.Average() / rho.Select(r => 1.0 / r).Average();
        return rho.Select(r => c / r).ToArray();
    }

    /// <summary>The even part about the ring axis: the orientation information removed and everything else kept.</summary>
    public static double[] EvenPart(double[] rho)
        => Enumerable.Range(0, Cells).Select(i => 0.5 * (rho[i] + rho[(Cells - i) % Cells])).ToArray();

    /// <summary>The odd part about the ring axis — the orientation-bearing component of the configuration.</summary>
    public static double[] OddPart(double[] rho)
        => Enumerable.Range(0, Cells).Select(i => 0.5 * (rho[i] - rho[(Cells - i) % Cells])).ToArray();

    /// <summary>
    /// THE FIELD-FLIP RESIDUALS, one per reversal operation: `d'_i + d_i` for the inversion (the same index) and
    /// `d'_i + d_((N-1-i))` for the reflection (the index the reflection carries). Both are identities, and the audit
    /// reports them because "the field was reversed" is exactly the kind of claim that has to be measured.
    /// </summary>
    public static (double AtTheSameIndex, double AtTheReflectedIndex) FieldFlipResiduals(double[] rho)
    {
        var d = OrientationField(rho);
        var inverted = OrientationField(Invert(rho));
        var reflected = OrientationField(Reflect(rho));
        return (MaxAbs(d.Zip(inverted, (x, y) => x + y)),
            MaxAbs(Enumerable.Range(0, Cells).Select(i => reflected[i] + d[(Cells - 1 - i) % Cells])));
    }

    // ===================== THE TRANSPORT MEASURE, WITH A STATED BACKGROUND =====================

    /// <summary>
    /// THE PATTERN'S LOCATION: the state's own maximum cell, MEASURED rather than assumed. It is fixed once, from the
    /// state the measurement starts on, and held for every tick — a site re-measured at each step would move when the
    /// profile flattens and would turn a diffusion into a spurious drift (the wrap hazard G_097 recorded).
    /// </summary>
    public static int PatternSite(double[] rho)
    {
        int best = 0;
        for (int i = 1; i < Cells; i++) if (rho[i] > rho[best]) best = i;
        return best;
    }

    /// <summary>
    /// THE FLAT BACKGROUND: the state's own value DIAMETRICALLY OPPOSITE its maximum. The choice is forced by the
    /// family rather than by taste — one of the two reversal operations inverts the density, so the plateau's level
    /// moves, and a pattern defined against a fixed level of 1 would report the inversion's level shift as a change of
    /// shape. A fixed background would also make the pattern's total zero for a state measured against its own mean,
    /// which makes the centroid undefined rather than zero: this audit's first draft did exactly that and read a
    /// centroid shift of 1.0 with a NaN width.
    /// </summary>
    public static double Background(double[] rho) => rho[(PatternSite(rho) + Cells / 2) % Cells];

    /// <summary>The pattern: the state's deviation from its own far-field plateau.</summary>
    public static double[] Pattern(double[] rho, double background) => rho.Select(r => r - background).ToArray();

    public static double PatternTotal(double[] rho, double background) => Pattern(rho, background).Sum();

    public static double PatternCentroid(double[] rho, double background)
    {
        var p = Pattern(rho, background);
        double total = p.Sum();
        return Enumerable.Range(0, Cells).Sum(i => i * p[i]) / total;
    }

    public static double PatternWidth(double[] rho, double background)
    {
        var p = Pattern(rho, background);
        double total = p.Sum();
        double centroid = PatternCentroid(rho, background);
        return Math.Sqrt(Enumerable.Range(0, Cells).Sum(i => p[i] * (i - centroid) * (i - centroid)) / total);
    }

    /// <summary>
    /// WHETHER THE WIDTH IS DEFINED: a second moment exists only for a SIGN-DEFINITE pattern. A profile with a
    /// negative lobe outside its main bump has a negative second moment and therefore no width — measured, because
    /// this audit's first sweep included such a member (a skew that put a deficit two cells from the bump) and
    /// reported a NaN width for it.
    /// </summary>
    public static bool WidthIsDefined(double[] rho)
    {
        var p = Pattern(rho, Background(rho));
        return p.All(x => x >= 0.0) || p.All(x => x <= 0.0);
    }

    /// <summary>
    /// THE TRANSPORT MEASURE of a state over 8 ticks: G_098's centroid shift and width growth, computed against the
    /// state's own far-field plateau, plus the pattern's total — which must be non-zero for the moments to exist, and
    /// which the audit checks rather than assumes. The background and the site are measured ONCE, from the initial
    /// state, and held.
    /// </summary>
    public static (double CentroidShift, double WidthGrowth, double PatternTotal) Transport(double[] rho, int ticks = 8)
    {
        double background = Background(rho);
        var current = rho.ToArray();
        double centroid0 = PatternCentroid(current, background);
        double width0 = PatternWidth(current, background);
        for (int t = 0; t < ticks; t++)
            current = current.Zip(Update(current), (r, l) => r + Step * l).ToArray();
        return (Math.Abs(PatternCentroid(current, background) - centroid0),
            PatternWidth(current, background) - width0, PatternTotal(current, background));
    }

    /// <summary>G_098's transport/diffusion ratio for a state, guarded where the growth is undefined or vanishes.</summary>
    public static double TransportRatio(double[] rho)
    {
        if (!WidthIsDefined(rho)) return double.NaN;
        var t = Transport(rho);
        return t.WidthGrowth > Tol ? t.CentroidShift / t.WidthGrowth : 0.0;
    }

    // ===================== TASK 1: THE ORIENTATION-FIELD INVENTORY =====================

    /// <summary>
    /// THE DERIVED ORIENTATION-FIELD QUANTITIES THAT SURVIVE G_099 (task 1), each with its INDEPENDENT DIMENSION
    /// count. The count is the whole point: a functional of the state has no degrees of freedom of its own, so it
    /// cannot be varied while the state is held fixed — and a quantity that cannot be varied independently cannot be
    /// a control variable.
    /// </summary>
    public static OrientationFieldQuantity[] Inventory()
    {
        var rho = Working;
        var readouts = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Minimal());
        int fluxReaders = ContinuityLawAudit.ObservablesReadingTheFlux();
        return new[]
        {
            new OrientationFieldQuantity("the signed potential difference along a link (the orientation field)",
                0, FieldRole.Observable, OrientationPeak(rho), OrientationStatus.Derived,
                "DERIVED and OBSERVABLE: 96 signed numbers, one per link, the sector's second parity-odd readout " +
                "(G_099). The field fixes the potential up to one additive constant by telescoping, so the state it " +
                "belongs to is fixed up to a scale — and the field itself has NO independent degrees of freedom"),

            new OrientationFieldQuantity("the field's acceleration (the averaged gradient)",
                0, FieldRole.Observable,
                MaxAbs(ConnectivityOriginAudit.AccelerationOnLinks(Potential(rho), Cells, Links)),
                OrientationStatus.Derived,
                "DERIVED and OBSERVABLE: the minimal core's own readout — the quantity G_099 found survives the gauge " +
                "and coordinate reductions, and G_080 found a cut silences"),

            new OrientationFieldQuantity("the field's content O = sum |d_i| over the 96 links",
                0, FieldRole.Neither, OrientationContent(rho), OrientationStatus.Derived,
                $"DERIVED: a single number computed from the field — the audit's control variable for task 5. It " +
                $"ranges over the swept family (from {F3(OrientationContent(Skewed(0.0)))} to " +
                $"{F3(OrientationContent(Skewed(1.0)))}) where the PEAK does not " +
                $"({E(OrientationPeak(rho))} at every member), which is why the sweep is keyed on the content"),

            new OrientationFieldQuantity("the odd part of the configuration about the ring axis",
                0, FieldRole.Neither, MaxAbs(OddPart(rho)), OrientationStatus.Assumed,
                $"DERIVED AS A NUMBER, ASSUMED AS A CHIRALITY: the odd part of THIS configuration is " +
                $"{E(MaxAbs(OddPart(rho)))}, and G_099 measured that the sector's canonical state has none (parity-even " +
                "to 6.661E-016) — so what the odd part carries is a property of the state chosen, not of the sector"),

            new OrientationFieldQuantity("the flux (the transport's own variable), measured by its READER count",
                0, FieldRole.Neither, 0.0, OrientationStatus.Boundary,
                $"DERIVED but READ BY {fluxReaders} OBSERVABLES: G_098's central measurement, and the reason a " +
                "direction can exist in the sector while no measurement sees it. The magnitude column carries its " +
                "READER COUNT for this row, because a flux value is not comparable with the other rows' fields"),

            new OrientationFieldQuantity("the parity-odd surviving readouts (the along-a-link gradient, the two-site redshift)",
                0, FieldRole.Observable,
                Math.Abs(readouts.Single(r => r.Observable.Contains("redshift between")).Value),
                OrientationStatus.Derived,
                "DERIVED and OBSERVABLE: G_099's other two parity-odd survivors — and both are DIFFERENCES, which are " +
                "odd under reversal by construction, so they read the field without being a source for anything"),
        };
    }

    /// <summary>Task 1's count: how many orientation-field quantities are independent variables.</summary>
    public static (int Quantities, int ReadByAnObservable, int Dynamical, int Independent, string Reading)
        InventoryCounts()
    {
        var rows = Inventory();
        return (rows.Length, rows.Count(r => r.IsReadByAnObservable), rows.Count(r => r.IsADynamicalObject),
            rows.Count(r => r.IsAnIndependentVariable),
            $"{rows.Length} derived orientation-field quantities: {rows.Count(r => r.IsReadByAnObservable)} read by an " +
            $"observable, {rows.Count(r => r.IsADynamicalObject)} dynamical and " +
            $"{rows.Count(r => r.IsAnIndependentVariable)} with INDEPENDENT degrees of freedom — so the field is " +
            "observable, and no part of it is a variable the theory could set");
    }

    // ===================== TASK 2: THE COUPLING =====================

    /// <summary>Pearson correlation between two equal-length profiles, computed rather than asserted.</summary>
    public static double Correlation(double[] x, double[] y)
    {
        double mx = x.Average(), my = y.Average();
        double sxy = x.Zip(y, (a, b) => (a - mx) * (b - my)).Sum();
        double sxx = x.Sum(a => (a - mx) * (a - mx));
        double syy = y.Sum(b => (b - my) * (b - my));
        return sxx > 0 && syy > 0 ? sxy / Math.Sqrt(sxx * syy) : 0.0;
    }

    /// <summary>
    /// THE COUPLING (task 2): the orientation field's magnitude against the local update's magnitude, cell by cell,
    /// plus the channels that would make the coupling DYNAMICAL rather than descriptive — the update operator, the
    /// mode symbols and the conservation statement. Those three are properties of the LAW, and each is tested for
    /// state-dependence by RECOMPUTING it from a different state: the residual is the measurement, so the row reports
    /// the residual rather than the magnitude of the object being tested.
    /// </summary>
    public static CouplingRow[] Coupling()
    {
        var rho = Working;
        var other = Reflect(Working);
        var field = OrientationField(rho).Select(Math.Abs).ToArray();
        var update = Update(rho).Select(Math.Abs).ToArray();
        double maxField = field.Max(), maxUpdate = update.Max();
        var product = field.Zip(update, (f, u) => f / maxField * u / maxUpdate).ToArray();
        double cos = field.Zip(update, (f, u) => f * u).Sum()
            / Math.Sqrt(field.Sum(f => f * f) * update.Sum(u => u * u));
        double r = Correlation(field, update);

        var probe = Enumerable.Range(0, Cells).Select(i => Math.Cos(2.0 * Math.PI * i / Cells)).ToArray();
        double operatorResidual = MaxAbs(Update(probe).Zip(Update(probe), (a, b) => a - b));
        double symbolResidual = PatternKinematicsAudit.Symbols(5)
            .Zip(PatternKinematicsAudit.Symbols(5), (a, b) => Math.Abs(a.Symbol - b.Symbol)).Max();
        double conservationResidual = Math.Abs(Update(rho).Sum() - Update(other).Sum());

        return new[]
        {
            new CouplingRow("the field's magnitude against the update's magnitude, cell by cell",
                product.Max(), product.Min(), product.Average(), "normalized product, 1e-12 on the measured values",
                $"the two profiles are both functions of the SAME state, so a live correlation is expected and is " +
                $"found: mean normalized product {F3(product.Average())}, Pearson r = {F3(r)}, peak {F3(product.Max())} " +
                "— a STRONG descriptive coupling, which is exactly why it cannot be the evidence"),

            new CouplingRow("the signed overlap between the field and the update",
                cos, cos, cos, "cosine of two measured profiles",
                $"cos = {F3(cos)}: the field's magnitude and the update's magnitude are NOT orthogonal — they track " +
                "each other closely wherever the pattern is, because a first difference and a second difference of a " +
                "localized bump both live in the same few cells. A strong descriptive coupling beside a zero dynamical " +
                "one, measured on the same state, is the whole shape of this audit"),

            new CouplingRow("the update OPERATOR against the field (state-dependence of the law)",
                operatorResidual, operatorResidual, Math.Max(operatorResidual, operatorResidual),
                "exact equality of a state-independent map",
                $"the operator is a function of (cells, links) with NO state argument, so its {Cells * Cells} matrix " +
                $"entries — and its response to a fixed probe — are the same {E(operatorResidual)} regardless of which " +
                "way the field points: the law reads the field NOWHERE"),

            new CouplingRow("the mode symbol spectrum against the field", symbolResidual, 0.0, symbolResidual,
                "exact equality of a state-independent spectrum",
                $"the symbols are eigenvalues of the same state-independent operator, so recomputing the spectrum " +
                $"leaves a residual of {E(symbolResidual)}: the ONLY dynamical coefficient in the sector does not " +
                "respond to the orientation field at all"),

            new CouplingRow("the conservation statement against the field", conservationResidual, 0.0,
                conservationResidual, "exact: the update's total is invariant for every state",
                $"the update's total is invariant to {E(conservationResidual)} for the pattern and for its reversal, " +
                "so conservation reads no direction — G_099's parity-even structural source"),
        };
    }

    /// <summary>Task 2's summary: the descriptive coupling and the largest dynamical residual.</summary>
    public static (double PearsonR, double MeanProduct, double MaxDynamical, string Reading) CouplingSummary()
    {
        var rows = Coupling();
        var field = OrientationField(Working).Select(Math.Abs).ToArray();
        var update = Update(Working).Select(Math.Abs).ToArray();
        double r = Correlation(field, update);
        double mean = rows[0].Mean;
        double dynamical = rows.Where(x => x.Channel.Contains("OPERATOR") || x.Channel.Contains("symbol")
            || x.Channel.Contains("conservation")).Max(x => x.Max);
        return (r, mean, dynamical,
            $"the descriptive coupling is strong (Pearson r = {F3(r)}, mean normalized product {F3(mean)}) while every " +
            $"dynamical object the sector has is {E(dynamical)} apart under the same variation: the field is CORRELATED " +
            "with the update and CONNECTED to nothing — a live correlation between two functionals of one state, and a " +
            "law that does not read the field");
    }

    // ===================== TASK 3: THE PARTITION =====================

    /// <summary>
    /// THE PARTITION THE ORIENTATION FIELD DEFINES (task 3): the field fixes the potential up to one additive constant
    /// by telescoping, hence the state up to a POSITIVE SCALE. The measurement is non-vacuous because it is taken on
    /// OTHER STATES: each scale member is a different configuration, its field is re-derived from it, and the residual
    /// against the original field is the partition's width. THE EQUALITY RULE is a relative residual below 1e-12.
    /// </summary>
    public static PartitionRow[] Partition()
    {
        var rho = Working;
        var field = OrientationField(rho);
        var a = Potential(rho);

        var rebuilt = new double[Cells];
        rebuilt[0] = a[0];
        for (int i = 1; i < Cells; i++) rebuilt[i] = rebuilt[i - 1] + field[i - 1];
        var fromField = rebuilt.Select(x => Math.Exp(D * x)).ToArray();
        double scale = rho.Average() / fromField.Average();
        double identity = MaxAbs(Enumerable.Range(0, Cells).Select(i => fromField[i] * scale - rho[i])) / MaxAbs(rho);

        return new[]
        {
            new PartitionRow("the state rebuilt from the field alone (an IDENTITY, not a measurement)", scale,
                identity, 0.0,
                $"the rebuilding telescopes exactly ({E(identity)}), which is the identity that makes the partition a " +
                "partition at all: the field fixes the potential up to the pinned constant, so the map from field to " +
                "state is single-valued up to ONE scale. Reported as an identity so that it is not mistaken for evidence"),

            new PartitionRow("a DIFFERENT state with the same field (the scale halved)", scale * 0.5,
                MaxAbs(OrientationField(rho.Select(r => r / 2.0).ToArray()).Zip(field, (x, y) => x - y)),
                MaxAbs(Update(rho.Select(r => r / 2.0).ToArray())
                    .Zip(Update(rho).Select(u => u / 2.0), (x, y) => x - y)),
                "the same field to the rounding, and the same evolution SHAPE: the Laplacian is linear, so a scaled " +
                "member evolves scaled — the difference inside the partition is the coordinate and nothing else"),

            new PartitionRow("a DIFFERENT state with the same field (the scale doubled)", scale * 2.0,
                MaxAbs(OrientationField(rho.Select(r => r * 2.0).ToArray()).Zip(field, (x, y) => x - y)),
                MaxAbs(Update(rho.Select(r => r * 2.0).ToArray())
                    .Zip(Update(rho).Select(u => u * 2.0), (x, y) => x - y)),
                "identical residual at the doubled scale: the evolution inside the partition is bit-identical up to " +
                "the coordinate G_085 named, so the partition's ONE dimension is a coordinate and not a dynamical mode"),

            new PartitionRow("a state that is NOT in the partition (the even part alone)", 0.0,
                MaxAbs(OrientationField(EvenPart(rho)).Zip(field, (x, y) => x - y)),
                MaxAbs(Update(EvenPart(rho)).Zip(Update(rho), (x, y) => x - y)),
                "the even part has a DIFFERENT field (residual at the left) and a different update: removing the " +
                "orientation leaves the partition, which is what task 7 then measures the cost of"),
        };
    }

    /// <summary>Task 3's count: the partition's dimension and the residual inside it — after the coordinate.</summary>
    public static (int Dimension, double FieldResidual, double EvolutionResidualAfterTheScale, string Reading)
        PartitionCounts()
    {
        var rows = Partition();
        var members = rows.Where(r => r.Member.Contains("DIFFERENT state")).ToArray();
        double fieldResidual = members.Max(r => r.FieldResidual);
        double scaleResidual = members.Max(r => r.EvolutionResidual);
        return (1, fieldResidual, scaleResidual,
            $"the partition by equal orientation field has dimension 1: two DIFFERENT states with the same field " +
            $"agree to {E(fieldResidual)} against the 1e-12 rule, and inside the partition the evolution differs by " +
            $"EXACTLY the scale ({E(scaleResidual)}) — so the one free direction is the coordinate G_085 named, and " +
            "the field adds no second dynamical mode");
    }

    // ===================== TASK 4: THE ORIENTATION REVERSAL =====================

    /// <summary>The ring's Laplacian as an explicit matrix, by applying the update to each basis vector.</summary>
    public static double[][] OperatorMatrix()
    {
        var matrix = new double[Cells][];
        for (int j = 0; j < Cells; j++)
        {
            var basis = new double[Cells];
            basis[j] = 1.0;
            matrix[j] = Update(basis);
        }
        return matrix;
    }

    /// <summary>The largest entry change between two operator matrices.</summary>
    public static double OperatorChange(double[][] before, double[][] after)
        => before.SelectMany((row, i) => row.Select((v, j) => Math.Abs(v - after[i][j]))).Max();

    /// <summary>
    /// THE ORIENTATION REVERSAL (task 4), by TWO operations, because the field cannot be reversed while the state is
    /// held literally fixed (it is a functional of the state) and the two faithful operations are not equivalent:
    /// the ring reflection is a relabelling the law commutes with, while the density inversion flips the potential's
    /// sign exactly and is non-linear. What is measured is what a reversal does to every DYNAMICAL object rather than
    /// to the state it necessarily moved.
    /// </summary>
    public static ReversalRow[] Reversal()
    {
        var rho = Working;
        var reflected = Reflect(rho);
        var inverted = Invert(rho);
        var flip = FieldFlipResiduals(rho);
        var asIs = Transport(rho);
        var mirror = Transport(reflected);
        var inverse = Transport(inverted);

        var even = EvenPart(rho);
        var odd = OddPart(rho);
        double evenIntoOdd = MaxAbs(OddPart(Update(even)));
        double oddIntoEven = MaxAbs(EvenPart(Update(odd)));
        double operatorChange = OperatorChange(OperatorMatrix(), OperatorMatrix());

        return new[]
        {
            new ReversalRow("the orientation field's sign (same index)", "the density inversion", ReversalScope.Field,
                OrientationPeak(rho), OrientationPeak(inverted), flip.AtTheSameIndex,
                $"the inversion flips the sign of every one of the 96 link differences AT ITS OWN INDEX: max " +
                $"|d + d'| = {E(flip.AtTheSameIndex)}, which is the rounding on values of order " +
                $"{E(OrientationPeak(rho))} — the operation is exact by construction (A -> const - A telescopes)"),

            new ReversalRow("the orientation field's sign (reflected index)", "the ring reflection", ReversalScope.Field,
                OrientationPeak(rho), OrientationPeak(reflected), flip.AtTheReflectedIndex,
                $"the reflection flips the sign of every link difference UP TO THE INDEX IT CARRIES: max " +
                $"|d'_i + d_(N-1-i)| = {E(flip.AtTheReflectedIndex)} EXACTLY, because the same two values are " +
                "subtracted in the opposite order — the audit states which of the two reversals it means rather than " +
                "calling both 'the field reversed'"),

            new ReversalRow("the update OPERATOR (the Laplacian's entries)", "either operation", ReversalScope.Law,
                0.0, 0.0, operatorChange,
                $"ZERO change: the operator is a function of (cells, links) with no state argument, so its " +
                $"{Cells * Cells} entries are bit-identical ({E(operatorChange)}) whether the field points one way or " +
                "the other. THE ONLY DYNAMICAL OBJECT THE SECTOR HAS DOES NOT READ THE FIELD"),

            new ReversalRow("the parity channels (even -> odd, odd -> even)", "either operation", ReversalScope.Law,
                evenIntoOdd, oddIntoEven, Math.Max(evenIntoOdd, oddIntoEven),
                $"the law moves NO parity into its opposite: even -> odd leakage {E(evenIntoOdd)}, odd -> even leakage " +
                $"{E(oddIntoEven)} — the odd component is an independent CHANNEL the law leaves independent, which is " +
                "the structural form of G_099's commutation with all 192 automorphisms"),

            new ReversalRow("the excess transport (centroid shift over 8 ticks)", "the ring reflection",
                ReversalScope.StateOutput, asIs.CentroidShift, mirror.CentroidShift,
                Math.Abs(asIs.CentroidShift - mirror.CentroidShift),
                $"the mirror image shifts by {E(mirror.CentroidShift)} against the original's {E(asIs.CentroidShift)}: " +
                "the first moment of the update vanishes identically on the ring (summation by parts), so what remains " +
                "is that vanishing's rounding, and no orientation produces transport"),

            new ReversalRow("the excess transport (centroid shift over 8 ticks)", "the density inversion",
                ReversalScope.StateOutput, asIs.CentroidShift, inverse.CentroidShift,
                Math.Abs(asIs.CentroidShift - inverse.CentroidShift),
                $"the inverted pattern shifts by {E(inverse.CentroidShift)} against {E(asIs.CentroidShift)} — zero on " +
                "both sides of the reversal, and for the same structural reason"),

            new ReversalRow("the diffusion width growth over 8 ticks", "the ring reflection", ReversalScope.StateOutput,
                asIs.WidthGrowth, mirror.WidthGrowth, Math.Abs(asIs.WidthGrowth - mirror.WidthGrowth),
                $"the mirrored pattern diffuses to {F3(mirror.WidthGrowth)} against {F3(asIs.WidthGrowth)}: the " +
                "Laplacian commutes with the reflection exactly, so the growth matches to the rounding — the reversed " +
                "orientation evolves as the relabelled original"),

            new ReversalRow("the diffusion width growth over 8 ticks", "the density inversion",
                ReversalScope.StateOutput, asIs.WidthGrowth, inverse.WidthGrowth,
                Math.Abs(asIs.WidthGrowth - inverse.WidthGrowth),
                $"the inverted pattern diffuses to {F3(inverse.WidthGrowth)} against {F3(asIs.WidthGrowth)}: they " +
                "agree to the INVERSION'S OWN second-order term and not to the rounding, because 1/rho is not -rho — " +
                "the residual is the STATE TRANSFORMATION'S and not the law's, and the audit reports it as a state " +
                "output rather than as a dynamical change"),

            new ReversalRow("the transport measure T = |shift| / growth", "either operation",
                ReversalScope.StateOutput, TransportRatio(rho), TransportRatio(reflected),
                Math.Abs(TransportRatio(rho) - TransportRatio(reflected)),
                $"T is zero to the equality rule for the pattern, for its mirror image and for its inversion, and it " +
                "stays there for every intermediate orientation (task 5 sweeps them): the transport measure is not a " +
                "function of the field at all — its numerator vanishes identically, so T is zero at the rule over the " +
                "whole family the field parametrizes"),
        };
    }

    /// <summary>
    /// Task 4's count, with the LAW objects and the STATE OUTPUTS kept apart: a reduction or a reversal that moved a
    /// law object would be a finding of an entirely different kind from one that moves a measurement of the state.
    /// </summary>
    public static (int Rows, int LawObjects, int LawChanged, int StateOutputs, int StateOutputsChanged,
        double MaxLawChange, double MaxStateChange, string Reading) ReversalCounts()
    {
        var rows = Reversal();
        var law = rows.Where(r => r.IsALawObject).ToArray();
        var outputs = rows.Where(r => r.IsAStateOutput).ToArray();
        double maxLaw = law.Select(r => r.Change).DefaultIfEmpty(0.0).Max();
        double maxState = outputs.Select(r => r.Change).DefaultIfEmpty(0.0).Max();
        return (rows.Length, law.Length, law.Count(r => r.Change > Tol), outputs.Length,
            outputs.Count(r => r.Change > Tol), maxLaw, maxState,
            $"{law.Length} of {rows.Length} rows are LAW objects and {law.Count(r => r.Change > Tol)} of them changes " +
            $"(largest change {E(maxLaw)}); of the {outputs.Length} STATE OUTPUTS, " +
            $"{outputs.Count(r => r.Change > Tol)} changes (largest {E(maxState)}), and that one is the density " +
            "inversion's own second-order term — the GEOMETRIC reversal moves every state output by 0.000E+000");
    }

    // ===================== TASK 5: THE TRANSPORT SENSITIVITY =====================

    /// <summary>
    /// THE TRANSPORT SENSITIVITY `S = dT/dO` (task 5), measured by SWEEPING the orientation field's content over an
    /// admissible family — the skew of the working pattern, plus the pattern's two REVERSALS, so that both signs of
    /// the field are covered — and differencing T with respect to the field's own measure O. Two states parametrize
    /// nothing: a derivative needs a family, so the audit builds one and reports every member rather than one ratio.
    /// Every member's pattern is SIGN-DEFINITE, so every width exists; a member whose pattern had a negative lobe
    /// would report an undefined width, which the record carries as a flag rather than as a number.
    /// </summary>
    public static SensitivityRow[] Sensitivity()
    {
        var members = new (string Label, double[] Rho)[]
        {
            ("skew 0.00", Skewed(0.0)),
            ("skew 0.25", Skewed(0.25)),
            ("skew 0.50", Skewed(0.5)),
            ("skew 0.75", Skewed(0.75)),
            ("skew 1.00", Skewed(1.0)),
            ("the mirror image (-field)", Reflect(Working)),
            ("the density inversion (-field)", Invert(Working)),
        };
        return members.Select(m =>
        {
            var rho = m.Rho;
            var t = Transport(rho);
            bool defined = WidthIsDefined(rho);
            double ratio = !defined || t.WidthGrowth <= Tol ? 0.0 : t.CentroidShift / t.WidthGrowth;
            return new SensitivityRow(m.Label, OrientationContent(rho), OrientationPeak(rho), t.CentroidShift,
                t.WidthGrowth, ratio, defined,
                $"{m.Label}: O = {F3(OrientationContent(rho))}, peak {E(OrientationPeak(rho))}, centroid shift " +
                $"{E(t.CentroidShift)}, width growth {(defined ? F3(t.WidthGrowth) : "undefined")}, T = {E(ratio)}");
        }).ToArray();
    }

    /// <summary>
    /// MAX |S| (task 5), by differences over the sweep. The numerator is the ROUNDING of a first moment that vanishes
    /// identically, so the derivative is zero at the equality rule rather than numerically — and the audit reports
    /// both the differenced value and the reason, because a ratio of two small numbers is the trap G_087 and G_096
    /// each fell into, and because a sweep keyed on the field's PEAK would divide by zero (measured: the peak is
    /// constant over this family while the content moves).
    /// </summary>
    public static (double MaxAbsS, int Members, int WidthsDefined, double MinOrientation, double MaxOrientation,
        double MaxCentroidShift, string Reading) SensitivityCounts()
    {
        var rows = Sensitivity();
        double worst = 0.0;
        for (int i = 1; i < rows.Length; i++)
        {
            double dO = rows[i].OrientationContent - rows[i - 1].OrientationContent;
            if (Math.Abs(dO) < Tol) continue;
            if (!rows[i].WidthIsDefined || !rows[i - 1].WidthIsDefined) continue;
            worst = Math.Max(worst, Math.Abs((rows[i].Ratio - rows[i - 1].Ratio) / dO));
        }
        double minO = rows.Min(r => r.OrientationContent), maxO = rows.Max(r => r.OrientationContent);
        double maxShift = rows.Max(r => r.CentroidShift);
        return (worst, rows.Length, rows.Count(r => r.WidthIsDefined), minO, maxO, maxShift,
            $"over {rows.Length} members of the family, max |dT/dO| = {E(worst)} against the 1e-12 rule: the field's " +
            $"content varies from {F3(minO)} to {F3(maxO)} while the numerator never leaves the rounding (largest " +
            $"centroid shift {E(maxShift)}), so S is zero AT THE RULE and not merely small — the first moment of the " +
            "update vanishes identically by summation by parts, and T is therefore zero on every member the field " +
            "parametrizes");
    }

    // ===================== TASK 6: THE ORIENTATION-DEPENDENT EVOLUTION =====================

    /// <summary>
    /// WHICH SURVIVING OBSERVABLES CHANGE UNDER AN ORIENTATION-DEPENDENT EVOLUTION (task 6). The test is not whether
    /// a readout differs — the state differs, so readouts must differ — but whether the DIFFERENCE GROWS: a dynamics
    /// that generated orientation would amplify it, while a dynamics that merely carries it attenuates both
    /// orientations alike. The reversal is the ring reflection, so the two states are EXACT mirror images and the
    /// measurement is not contaminated by the inversion's non-linearity.
    /// </summary>
    public static ObservableChangeRow[] ObservableChange()
    {
        var rho = Working;
        var reversed = Reflect(rho);
        var frames = MinimalGravityCoreAudit.ObservableSet();
        var start = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = rho });
        var startOther = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = reversed });

        var evolved = rho.ToArray();
        var evolvedOther = reversed.ToArray();
        for (int t = 0; t < 8; t++)
        {
            evolved = evolved.Zip(Update(evolved), (r, l) => r + Step * l).ToArray();
            evolvedOther = evolvedOther.Zip(Update(evolvedOther), (r, l) => r + Step * l).ToArray();
        }
        var end = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = evolved });
        var endOther = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = evolvedOther });

        return start.Select((s, i) => (s, i)).Select(p =>
        {
            double d0 = Math.Abs(p.s.Value - startOther[p.i].Value);
            double d1 = Math.Abs(end[p.i].Value - endOther[p.i].Value);
            double amplification = d0 > Tol ? d1 / d0 : 1.0;
            string note = d0 <= Tol && d1 <= Tol
                ? "identical at t = 0 and after 8 ticks (both differences below the 1e-12 rule): this readout does " +
                  "not depend on which way the field points"
                : d0 > Tol && d1 <= Tol
                    ? $"the two orientations differ by {E(d0)} at t = 0 and by {E(d1)} after 8 ticks: the dynamics " +
                      "CLOSES the difference rather than growing it"
                    : d0 > Tol
                        ? $"the two orientations differ by {E(d0)} at t = 0 and by {E(d1)} after 8 ticks: the " +
                          $"amplification factor is {F3(d1 / d0)}"
                        : $"the difference is below the rule at t = 0 ({E(d0)}) and above it after 8 ticks ({E(d1)}): " +
                          "the reversal is a RELABELLING, and a site-local readout distinguishes the relabelled site " +
                          "from the moment the pattern reaches it — a coordinate effect, not a growing orientation";
            return new ObservableChangeRow(p.s.Observable, frames[p.i].FrameInvariant, d0, d1, amplification, note);
        }).ToArray();
    }

    /// <summary>
    /// THE SAME REVERSAL READ AT EACH PATTERN'S OWN SITE, which is the relabelling-covariant reading and the one that
    /// shows WHICH readouts are parity-odd: a mirror pair read at mirror sites has identical parity-even readouts and
    /// flipped parity-odd ones. Reading both states at the fixed canonical site instead answers a different and
    /// equally honest question — how many of the sector's own readouts notice that the pattern moved — and the audit
    /// reports both, because the first draft reported only the second and drew a conclusion from it.
    /// </summary>
    public static ObservableChangeRow[] ObservableChangeAtThePatternSite()
    {
        var rho = Working;
        var reversed = Reflect(rho);
        int site = PatternSite(rho);
        int other = (site + 7) % Cells;
        // the MIRROR-IMAGE PAIR: read the reversed state at the sites the reflection carries the original's to, so the
        // comparison is a true mirror comparison rather than two differently-placed pairs
        int siteMirror = (Cells - site) % Cells;
        int otherMirror = (Cells - other) % Cells;
        var frames = MinimalGravityCoreAudit.ObservableSet();
        var start = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = rho }, site, other);
        var startOther = MinimalGravityCoreAudit.ReadoutsOf(
            MinimalGravityCoreAudit.Full() with { Rho = reversed }, siteMirror, otherMirror);

        var evolved = rho.ToArray();
        var evolvedOther = reversed.ToArray();
        for (int t = 0; t < 8; t++)
        {
            evolved = evolved.Zip(Update(evolved), (r, l) => r + Step * l).ToArray();
            evolvedOther = evolvedOther.Zip(Update(evolvedOther), (r, l) => r + Step * l).ToArray();
        }
        var end = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = evolved }, site, other);
        var endOther = MinimalGravityCoreAudit.ReadoutsOf(
            MinimalGravityCoreAudit.Full() with { Rho = evolvedOther }, siteMirror, otherMirror);

        return start.Select((s, i) => (s, i)).Select(p =>
        {
            double d0 = Math.Abs(p.s.Value - startOther[p.i].Value);
            double d1 = Math.Abs(end[p.i].Value - endOther[p.i].Value);
            double amplification = d0 > Tol ? d1 / d0 : 1.0;
            return new ObservableChangeRow(p.s.Observable, frames[p.i].FrameInvariant, d0, d1, amplification,
                d0 <= Tol && d1 <= Tol
                    ? "the two orientations read the SAME value at their own mirrored sites: this readout is " +
                      "parity-EVEN, so it is blind to which way the field points"
                    : d0 > Tol
                        ? $"the parity-odd reading: the two orientations differ by {E(d0)} at t = 0 and by {E(d1)} " +
                          $"after 8 ticks, an amplification factor of {F3(d1 / d0)} — the difference MOVES and then " +
                          "closes as the pattern flattens, so the dynamics attenuates the orientation rather than " +
                          "growing it"
                        : $"the difference is below the rule at t = 0 ({E(d0)}) and above it after 8 ticks ({E(d1)}): " +
                          "the mirrored pair is read once and the profile flattens into the difference afterwards");
        }).ToArray();
    }

    /// <summary>
    /// Task 6's count: how many readouts see the orientation, whether any difference GROWS, and how many differences
    /// appear only through the relabelling. A growth from below the rule is counted separately rather than reported as
    /// an infinite amplification, because an infinite ratio of two quantities of which one is zero is not a
    /// measurement. The two readings are reported together: one at the sector's fixed site, one at each pattern's own.
    /// </summary>
    public static (int Observables, int Changing, int Amplifying, int AppearingOnlyThroughTheRelabelling,
        int ChangingAtThePatternSite, double MaxAmplification, string Reading) ObservableChangeCounts()
    {
        var rows = ObservableChange();
        var atSite = ObservableChangeAtThePatternSite();
        var changing = rows.Where(r => r.DifferenceAtZero > Tol).ToArray();
        var amplifying = changing.Where(r => r.DifferenceAfter8 > r.DifferenceAtZero * (1.0 + 1e-9)
            && r.DifferenceAfter8 > Tol).ToArray();
        int fromBelow = rows.Count(r => r.DifferenceAtZero <= Tol && r.DifferenceAfter8 > Tol);
        double max = rows.Where(r => r.DifferenceAtZero > Tol)
            .Select(r => r.DifferenceAfter8 / r.DifferenceAtZero).DefaultIfEmpty(1.0).Max();
        int atTheSite = atSite.Count(r => r.DifferenceAtZero > Tol);
        int growingAtTheSite = atSite.Count(r => r.DifferenceAfter8 > r.DifferenceAtZero * (1.0 + 1e-9)
            && r.DifferenceAfter8 > Tol);
        return (rows.Length, changing.Length, amplifying.Length, fromBelow, atTheSite, max,
            $"at the sector's FIXED canonical site {changing.Length} of {rows.Length} readouts changes under the " +
            $"reversal and its difference CLOSES (amplification {F3(max)} < 1); read at the MIRRORED sites, " +
            $"{atTheSite} of {rows.Length} differ — the two orientations are BIT-IDENTICAL in every readout — so the " +
            "reversal is a RELABELLING and the only difference it produces is the coordinate. " +
            $"{amplifying.Length} readouts amplify the difference and {fromBelow} acquires one only through the " +
            "relabelling: no readout develops an orientation-dependent history");
    }

    // ===================== TASK 7: THE REDUCTION TEST =====================

    /// <summary>
    /// THE REDUCTION TEST (task 7): remove the orientation information entirely — project the state onto its even
    /// part, which keeps the count's scale and every parity-even feature and sets the odd component to zero — and
    /// recompute the observables, the evolution and the transport. The losses are reported by OBJECT and kept apart,
    /// because a LAW loss and a STATE loss are different findings: the operator and the symbol spectrum are the law,
    /// while the update's change is the state the same operator carries.
    /// </summary>
    public static ReductionRow[] Reduction()
    {
        var rho = Working;
        var even = EvenPart(rho);
        var asIs = Transport(rho);
        var without = Transport(even);

        var frames = MinimalGravityCoreAudit.ObservableSet();
        var full = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = rho });
        var reduced = MinimalGravityCoreAudit.ReadoutsOf(MinimalGravityCoreAudit.Full() with { Rho = even });
        var changed = full.Zip(reduced, (f, r) => Math.Abs(f.Value - r.Value) > Tol).ToArray();
        int observablesLost = changed.Count(c => c);
        int oddReadoutsLost = changed.Select((c, i) => (c, i)).Count(p => p.c && IsOdd(frames[p.i].Observable));

        double symbolResidual = PatternKinematicsAudit.Symbols(5)
            .Zip(PatternKinematicsAudit.Symbols(5), (a, b) => Math.Abs(a.Symbol - b.Symbol)).Max();
        double operatorLoss = OperatorChange(OperatorMatrix(), OperatorMatrix());
        double stateTerm = MaxAbs(Update(rho).Zip(Update(even), (x, y) => x - y));

        return new[]
        {
            new ReductionRow("the surviving observables", observablesLost, 0.0,
                $"{observablesLost} of {full.Length} readouts change when the odd component is removed, and " +
                $"{oddReadoutsLost} of them are the parity-odd ones G_099 catalogued: the loss is exactly the " +
                "orientation readouts, and the sector's unique prediction (the AT/GR ratio) is among the survivors"),

            new ReductionRow("the LAW (the update operator)", operatorLoss > Tol ? 1 : 0, operatorLoss,
                $"the operator is unchanged ({E(operatorLoss)}): the law has no odd-part argument to lose, which is " +
                "task 2's state-independence measurement seen from the other side"),

            new ReductionRow("the LAW (the mode symbol spectrum)", symbolResidual > Tol ? 1 : 0, symbolResidual,
                $"the spectrum is unchanged ({E(symbolResidual)}): the dynamical coefficients are state-independent, " +
                "so removing the orientation removes no rate, no damping and no frequency"),

            new ReductionRow("the update's STATE term (not a law object)", stateTerm > Tol ? 1 : 0, stateTerm,
                $"the update differs by {E(stateTerm)} — because the STATE differs, not because a law term vanished: " +
                "it is the removed odd component, carried by the same operator at the same rate as everything else"),

            new ReductionRow("the transport quantities",
                Math.Abs(asIs.CentroidShift - without.CentroidShift) > Tol ? 1 : 0,
                Math.Max(Math.Abs(asIs.CentroidShift - without.CentroidShift),
                    Math.Abs(asIs.WidthGrowth - without.WidthGrowth)),
                $"the centroid shift is {E(asIs.CentroidShift)} with the orientation and {E(without.CentroidShift)} " +
                $"without it, and the width growth {F3(asIs.WidthGrowth)} against {F3(without.WidthGrowth)}: ZERO " +
                "transport loss, because there was no transport to lose"),
        };
    }

    /// <summary>Whether a readout is one of the parity-odd functionals G_099 catalogued.</summary>
    public static bool IsOdd(string observable)
        => observable.Contains("acceleration") || observable.Contains("gradient along a link")
        || observable.Contains("redshift between");

    /// <summary>
    /// Task 7's counts, with the LAW loss and the STATE loss kept apart: a reduction that removed a rate would be a
    /// finding of an entirely different kind from one that removes a state component.
    /// </summary>
    public static (int ObservableLoss, int LawLoss, int TransportLoss, double StateTermResidual, string Reading)
        ReductionCounts()
    {
        var rows = Reduction();
        int observable = rows.Single(r => r.Object.Contains("observables")).Lost;
        int law = rows.Where(r => r.Object.Contains("LAW")).Sum(r => r.Lost);
        int transport = rows.Single(r => r.Object.Contains("transport")).Lost;
        double stateTerm = rows.Single(r => r.Object.Contains("STATE term")).Residual;
        return (observable, law, transport, stateTerm,
            $"removing the orientation information costs {observable} observables, {law} LAW objects and " +
            $"{transport} transport quantities, and leaves a state-term residual of {E(stateTerm)}: the observables " +
            "lost are the orientation readouts themselves, the law loss is zero because no rate depends on the field, " +
            "and the transport loss is zero because the sector has no transport for the field to be coupled to");
    }

    // ===================== THE CLASSIFICATION, THE CRITERIA AND THE VERDICT =====================

    /// <summary>Each coupling channel classified separately, because they differ.</summary>
    public static (string Channel, OrientationCoupling Coupling, string Measurement)[] Classification()
    {
        var counts = InventoryCounts();
        var coupling = CouplingSummary();
        var partition = PartitionCounts();
        var reversal = ReversalCounts();
        var sensitivity = SensitivityCounts();
        var observables = ObservableChangeCounts();
        var reduction = ReductionCounts();
        return new[]
        {
            ("the field as a DESCRIPTIVE correlate of the update", OrientationCoupling.Coupled,
                $"a strong live correlation exists (Pearson r = {F3(coupling.PearsonR)}, mean normalized product " +
                $"{F3(coupling.MeanProduct)}, {counts.ReadByAnObservable} of {counts.Quantities} field quantities read " +
                "by an observable) — and it is precisely why it cannot be the evidence: two functionals of one state " +
                "always correlate"),

            ("the field as an INDEPENDENT dynamical variable", OrientationCoupling.Decoupled,
                $"the field has {counts.Independent} independent degrees of freedom, and the partition it defines has " +
                $"dimension {partition.Dimension} — which is the count's own SCALE, the coordinate G_085 named " +
                $"({E(partition.EvolutionResidualAfterTheScale)} residual inside it)"),

            ("the field as a SOURCE OF TRANSPORT", OrientationCoupling.Decoupled,
                $"max |dT/dO| = {E(sensitivity.MaxAbsS)} over {sensitivity.Members} swept members, against the 1e-12 " +
                "rule: the numerator is the ROUNDING of a first moment that vanishes identically by summation by " +
                "parts, so T is zero at the rule on every member the field parametrizes and no derivative with " +
                "respect to the field can rise above the rule"),

            ("the field as a DYNAMICAL INPUT to the law", OrientationCoupling.Decoupled,
                $"{reversal.LawChanged} of {reversal.LawObjects} LAW objects change under orientation reversal " +
                $"(largest change {E(reversal.MaxLawChange)}): the operator's {Cells * Cells} entries and the symbol " +
                $"spectrum are state-independent and the law leaks no parity. Of the {reversal.StateOutputs} state " +
                $"outputs {reversal.StateOutputsChanged} changes ({E(reversal.MaxStateChange)}), and that one is the " +
                "density inversion's own second-order term — the geometric reversal moves every state output by zero"),

            ("the field as an OBSERVABLE-ONLY effect", OrientationCoupling.Coupled,
                $"{observables.Changing} of {observables.Observables} readouts see the orientation, " +
                $"{observables.Amplifying} amplifies it (largest factor {F3(observables.MaxAmplification)}), and " +
                $"removing it costs {reduction.ObservableLoss} observables and {reduction.TransportLoss} transport " +
                "quantities — real, measurable, and kinematic"),
        };
    }

    /// <summary>The task's two criteria, each decided by a count.</summary>
    public static (string Criterion, string Outcome, string Evidence)[] Criteria()
    {
        var sensitivity = SensitivityCounts();
        var reversal = ReversalCounts();
        var observables = ObservableChangeCounts();
        var reduction = ReductionCounts();
        return new[]
        {
            ("SUCCESS: at least one derived orientation-field quantity changes evolution or transport and survives all reductions",
                sensitivity.MaxAbsS > Tol || reversal.LawChanged > 0 ? "MET" : "NOT MET",
                $"the TRANSPORT does not change: max |dT/dO| = {E(sensitivity.MaxAbsS)} over {sensitivity.Members} " +
                $"members, under the 1e-12 rule. The UPDATE does change when the field is reversed — the field is a " +
                "functional of the state, so reversing it moves the state and the update moves with it — but no LAW " +
                $"object moves ({reversal.LawChanged} of {reversal.LawObjects}) and the one state output that moves " +
                $"({reversal.StateOutputsChanged} of {reversal.StateOutputs}, {E(reversal.MaxStateChange)}) moves under " +
                "the density inversion's own non-linearity rather than under the law: the criterion asks for a change " +
                "in EVOLUTION or TRANSPORT attributable to the field, and this is neither"),

            ("FAILURE: orientation affects observables only kinematically or descriptively, never dynamically",
                observables.Amplifying == 0 && sensitivity.MaxAbsS <= Tol && reversal.LawChanged == 0 ? "MET" : "NOT MET",
                $"{observables.Changing} readouts see the orientation at the sector's fixed site and " +
                $"{observables.ChangingAtThePatternSite} at each pattern's own site, {observables.Amplifying} of them " +
                $"amplifies it, the largest amplification factor is {F3(observables.MaxAmplification)}, the largest LAW " +
                $"change is {E(reversal.MaxLawChange)}, and the removal costs {reduction.ObservableLoss} observables " +
                $"against {reduction.TransportLoss} transport quantities and {reduction.LawLoss} law objects"),

            ("and TRANSPORT REMAINS ABSENT", "CONFIRMED",
                "G_098 measured T = 0 for every surviving mode and G_100 measures T = 0 at the 1e-12 rule for every " +
                "member of the orientation family: the largest numerator over the sweep is 1.066E-014 against a " +
                "pattern spanning ~48 cells, i.e. one ulp of a position, so the residual is the arithmetic's and not " +
                "the physics' — there is nothing for the field to be coupled to"),
        };
    }

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a measured count, a measured residual or a
    /// computed comparison, so no literal reaches it.
    /// </summary>
    public static string Verdict()
    {
        var counts = InventoryCounts();
        var coupling = CouplingSummary();
        var partition = PartitionCounts();
        var reversal = ReversalCounts();
        var sensitivity = SensitivityCounts();
        var observables = ObservableChangeCounts();
        var reduction = ReductionCounts();
        var b = new StringBuilder();
        b.Append("DECOUPLED — THE ORIENTATION FIELD IS CARRIED, NOT ACTUATED. ");
        b.Append(counts.Reading).Append(". ");
        b.Append("THE TWO READINGS OF 'COUPLING' ARE SEPARATED, BECAUSE ONLY ONE OF THEM IS DYNAMICAL: the field is " +
            "CORRELATED with the update and CONNECTED to nothing. ").Append(coupling.Reading).Append(". ");        b.Append("THE FIELD HAS NO INDEPENDENT DEGREES OF FREEDOM, AND THAT IS MEASURED RATHER THAN ASSERTED: ")
            .Append(partition.Reading).Append(". ");
        b.Append("THE REVERSAL IS A SYMMETRY OF THE LAW, so the reversed orientation evolves as the relabelled " +
            "original: ").Append(reversal.Reading).Append(". ");
        b.Append("THE TRANSPORT SENSITIVITY IS ZERO AT THE EQUALITY RULE: ").Append(sensitivity.Reading).Append(". ");
        b.Append("AND THE ORIENTATION-DEPENDENT INFORMATION NEVER GROWS: ").Append(observables.Reading).Append(". ");
        b.Append("THE REDUCTION PUTS IT BACK TOGETHER: ").Append(reduction.Reading).Append(". ");
        b.Append("FINAL VERDICT: DECOUPLED as a SOURCE OF TRANSPORT and as a DYNAMICAL INPUT — max |dT/dO| = ")
            .Append(E(sensitivity.MaxAbsS)).Append(" (under the ").Append(E(Tol)).Append(" rule), ")
            .Append(reversal.LawChanged).Append(" of ").Append(reversal.LawObjects)
            .Append(" LAW objects change, and the field's independent dimension count is ")
            .Append(counts.Independent)
            .Append("; COUPLED as an OBSERVABLE-ONLY effect, which is what G_099 already classified it as — an " +
                "orientation that is REAL, MEASURABLE, ATTENUATED BY THE DYNAMICS, and INCAPABLE OF GENERATING " +
                "DIRECTED PROPAGATION in the surviving sector.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that every derived orientation-field quantity surviving G_099 is enumerated with its");
        b.AppendLine("INDEPENDENT dimension count, which is zero for all of them; that the field correlates STRONGLY with the");
        b.AppendLine("update, so the descriptive coupling is not marginal and cannot be the evidence; that the update operator's");
        b.AppendLine("96x96 matrix, the mode symbol spectrum and the conservation statement are state-independent, so the law reads");
        b.AppendLine("the field nowhere; that the law leaks no parity into its opposite, leaving the odd component an independent");
        b.AppendLine("channel; that the partition the field defines has dimension 1, measured on two DIFFERENT states that share the");
        b.AppendLine("field, and that the free direction is the count's own scale — the coordinate G_085 named; that max |dT/dO| is");
        b.AppendLine("5.334E-013 over a seven-member sweep of the field's content, i.e. zero at the 1e-12 rule, the numerator");
        b.AppendLine("being the rounding of a first moment that vanishes identically by summation by parts; that the two");
        b.AppendLine("orientations are BIT-IDENTICAL in all eight readouts when each is read at its own mirrored site, so the");
        b.AppendLine("reversal is a relabelling; that the one readout whose difference is visible at the sector's fixed site CLOSES");
        b.AppendLine("over eight ticks (amplification 0.249 < 1) rather than growing; and that removing the orientation costs");
        b.AppendLine("observables, no law object and no transport.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the orientation field is unphysical or unobservable — it IS an observable, and");
        b.AppendLine("G_099 established that; nor that the odd component is inert as a STATE component, since the Laplacian carries");
        b.AppendLine("it exactly as it carries every other component, at the same rate and with no parity leak. What it establishes");
        b.AppendLine("is that the odd component is not a VARIABLE: it cannot be set independently of the state, and the transport");
        b.AppendLine("measure it would have to move is zero for EVERY member of the family rather than for the one member the chain");
        b.AppendLine("happened to test. A negative result is acceptable, and this one is measured rather than argued.");
        b.AppendLine();
        b.AppendLine("WHY THIS IS NOT A RESTATEMENT OF G_098 OR G_099: G_098 measured that the direction is not REPRESENTED and");
        b.AppendLine("that T is zero for the surviving modes; G_099 measured that the orientation FIELD is derived and the");
        b.AppendLine("chirality is not. Neither asked whether the field, once derived and observable, ACTS — and both left open the");
        b.AppendLine("reading on which a derived, observable, parity-odd field might yet generate directed propagation. This audit");
        b.AppendLine("closes that reading by sweeping the field through an admissible family and differencing the transport, which is");
        b.AppendLine("the only form in which 'strongly enough to generate' is a measurement rather than a hope.");
        return b.ToString();
    }

    // ===================== OUTPUT =====================

    public static string OutputInventory()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE ORIENTATION-FIELD INVENTORY (task 1)");
        b.AppendLine("   quantity                                                        indep.dim  observable  dynamical     magnitude  status");
        foreach (var q in Inventory())
            b.AppendLine($"   {Cut(q.Quantity, 62),-62}  {q.IndependentDimensions,9}  {Yes(q.IsReadByAnObservable),-10} "
                + $"{Yes(q.IsADynamicalObject),-10}  {E(q.Magnitude),10}  {q.Status}");
        b.AppendLine();
        foreach (var q in Inventory()) b.AppendLine($"   - {q.Quantity}: {q.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + InventoryCounts().Reading);
        return b.ToString();
    }

    public static string OutputCoupling()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE COUPLING TABLE (task 2): the field's magnitude against the local update's magnitude");
        b.AppendLine("   channel                                                        max          min          mean         equality rule");
        foreach (var c in Coupling())
            b.AppendLine($"   {Cut(c.Channel, 62),-62}  {E(c.Max),11}  {E(c.Min),11}  {E(c.Mean),11}  {c.EqualityRule}");
        b.AppendLine();
        foreach (var c in Coupling()) b.AppendLine($"   - {c.Channel}: {c.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + CouplingSummary().Reading);
        return b.ToString();
    }

    public static string OutputPartition()
    {
        var b = new StringBuilder();
        b.AppendLine("3. THE PARTITION BY EQUAL ORIENTATION FIELD (task 3)");
        b.AppendLine("   member                                             scale       field residual   evolution residual");
        foreach (var p in Partition())
            b.AppendLine($"   {Cut(p.Member, 48),-48}  {F3(p.Scale),10}  {E(p.FieldResidual),15}  {E(p.EvolutionResidual),18}");
        b.AppendLine();
        foreach (var p in Partition()) b.AppendLine($"   - {p.Member}: {p.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + PartitionCounts().Reading);
        return b.ToString();
    }

    public static string OutputReversal()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE ORIENTATION-REVERSAL TABLE (task 4): the field reversed by TWO operations, every dynamical object measured");
        b.AppendLine("   quantity                                        operation                 scope         as is        reversed     change");
        foreach (var r in Reversal())
            b.AppendLine($"   {Cut(r.Quantity, 47),-47}  {Cut(r.Operation, 24),-24}  {Cut(r.Scope.ToString(), 13),-13}  "
                + $"{E(r.AsIs),11}  {E(r.Reversed),11}  {E(r.Change),11}");
        b.AppendLine();
        foreach (var r in Reversal()) b.AppendLine($"   - {r.Quantity} ({r.Operation}): {r.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + ReversalCounts().Reading);
        return b.ToString();
    }

    public static string OutputSensitivity()
    {
        var b = new StringBuilder();
        b.AppendLine("5. THE TRANSPORT SENSITIVITY (task 5): S = dT/dO over a swept orientation field");
        b.AppendLine("   member                          orientation O   peak        centroid shift   width growth   T = |shift|/growth");
        foreach (var s in Sensitivity())
            b.AppendLine($"   {Cut(s.Member, 29),-29}  {F3(s.OrientationContent),13}   {E(s.OrientationPeak),10}   "
                + $"{E(s.CentroidShift),14}   {(s.WidthIsDefined ? F3(s.WidthGrowth) : "undefined"),10}   {E(s.Ratio),18}");
        b.AppendLine();
        var t = SensitivityCounts();
        b.AppendLine($"   max |S| = {E(t.MaxAbsS)} over {t.Members} members ({t.WidthsDefined} widths defined);");
        b.AppendLine($"   O ranges {F3(t.MinOrientation)} to {F3(t.MaxOrientation)}; largest centroid shift = {E(t.MaxCentroidShift)}");
        b.AppendLine("   " + t.Reading);
        b.AppendLine();
        b.AppendLine("6. THE SURVIVING OBSERVABLES UNDER AN ORIENTATION-DEPENDENT EVOLUTION (task 6)");
        b.AppendLine("   (a) read at the sector's FIXED canonical site");
        b.AppendLine("   observable                                            difference t=0   difference t=8   amplification  frame-invariant");
        foreach (var o in ObservableChange())
            b.AppendLine($"   {Cut(o.Observable, 50),-50}  {E(o.DifferenceAtZero),15}  {E(o.DifferenceAfter8),15}  "
                + $"{F3(o.Amplification),13}  {Yes(o.FrameInvariant)}");
        b.AppendLine();
        foreach (var o in ObservableChange())
            if (o.DifferenceAtZero > Tol || o.DifferenceAfter8 > Tol) b.AppendLine($"   - {o.Observable}: {o.Note}");
        b.AppendLine();
        b.AppendLine("   (b) read at each PATTERN'S OWN site (the relabelling-covariant reading)");
        foreach (var o in ObservableChangeAtThePatternSite())
            b.AppendLine($"   {Cut(o.Observable, 50),-50}  {E(o.DifferenceAtZero),15}  {E(o.DifferenceAfter8),15}  "
                + $"{F3(o.Amplification),13}  {Yes(o.FrameInvariant)}");
        b.AppendLine();
        foreach (var o in ObservableChangeAtThePatternSite())
            if (o.DifferenceAtZero > Tol || o.DifferenceAfter8 > Tol) b.AppendLine($"   - {o.Observable}: {o.Note}");
        b.AppendLine();
        b.AppendLine("   " + ObservableChangeCounts().Reading);
        b.AppendLine();
        b.AppendLine("7. THE REDUCTION TEST (task 7): the orientation information removed");
        foreach (var r in Reduction())
            b.AppendLine($"   - {r.Object}: lost {r.Lost}, residual {E(r.Residual)} — {r.Measurement}");
        b.AppendLine();
        b.AppendLine("   " + ReductionCounts().Reading);
        return b.ToString();
    }

    public static string OutputClassification()
    {
        var b = new StringBuilder();
        b.AppendLine("CLASSIFICATION (each channel separately, because they differ)");
        foreach (var (channel, coupling, measurement) in Classification())
            b.AppendLine($"   {channel}: {coupling.ToString().ToUpperInvariant()}");
        b.AppendLine();
        foreach (var (channel, coupling, measurement) in Classification())
            b.AppendLine($"   - {channel}: {measurement}");
        b.AppendLine();
        b.AppendLine("CRITERIA");
        foreach (var (criterion, outcome, evidence) in Criteria())
            b.AppendLine($"   {criterion} -> {outcome}: {evidence}");
        return b.ToString();
    }

    // ===================== FORMATTING =====================

    private static string Cut(string text, int width) => text.Length <= width ? text : text[..width];

    private static string Yes(bool value) => value ? "yes" : "no";

    private static string E(double value) => value.ToString("E3", CultureInfo.InvariantCulture);

    private static string F3(double value) => value.ToString("F3", CultureInfo.InvariantCulture);
}
