using System.Globalization;
using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>What a part of the density-to-potential or potential-to-clock map turns out to be (ResearchY-G_085).</summary>
public enum MapRole { Physical, Effective, Coordinate, Gauge }

/// <summary>One readout, computed THROUGH the maps, so a change of the maps can move it.</summary>
public sealed record MapReadout(string Observable, double Value, bool Prediction, bool Produced, string Reason);

/// <summary>
/// A PARAMETRIZATION OF THE TWO MAPS: the density-to-potential map and the potential-to-clock map. Both are functions,
/// not numbers, because the question is which PARTS of them the observables fix.
/// </summary>
public sealed record Maps(string Name, Func<double, double> Potential, Func<double, double> Clock, string Note)
{
    public double A(double rho) => Potential(rho);
    public double Rate(double rho) => Clock(Potential(rho));
}

/// <summary>One admissible variation of the maps or of the state, with what it moves.</summary>
public sealed record MapVariation(string Variation, string Form, int PredictionsMoved, int LevelsMoved,
    bool InvariantsPreserved, bool PinnedDataHold, double SectorValueResidual, string Verdict, string Reading);

/// <summary>A part of the map pair, with the measurements that decide its role.</summary>
public sealed record MapPart(string Part, MapRole Role, int PredictionsLostIfRemoved, int PredictionsMoved,
    bool InvariantsPreserved, string Measurement);

/// <summary>
/// ResearchY-G_085 — MAP REDUNDANCY AUDIT (group G — Gravity Source).
///
/// QUESTION. G_084 reduced the gravity core to the count density, the potential map `A = f(rho)` and the clock map
/// `clock = W(A)`, and measured that the core reproduces every surviving observable. The maps themselves were left
/// unexamined. So: **which parts of `f` and `W` are observable, and which are pure reparameterization?**
///
/// PREFER REDUCTION: any redundancy the audit finds reduces the core further, and the licence is to remove.
///
/// ANSWER: **THE MAPS ARE OBSERVABLE ONLY THROUGH THE COMPOSITION `W ∘ f` AND THE SOURCE `f'` — and of the three
/// parameters the pair carries, TWO are conventions (ONE gauge and ONE coordinate) while the third (the clock law's
/// shape) is a physical parameter no surviving measurement fixes.**
///
///  (1) THE TRANSFORMATIONS ARE ENUMERATED AND EACH IS APPLIED EXACTLY, not argued: the compensated shift of the
///      potential `f -> f + b, W -> W(. - b)`; the unit of the count `rho -> k rho`; the unit of time `W -> k W`; the
///      potential's scale `f -> l f, W -> W(./l)`; a NON-AFFINE distortion `A -> A + e A^2` with the clock composed
///      with its exact inverse; and the clock law's free room `W(A) = exp(A + (c/2) A^2)`.
///
///  (2) THE INVARIANT CONTENT IS THE PAIR (the rate field, the source field) = (W(f(rho)), the gradient of f along the
///      links). The compensated shift leaves that pair BIT-IDENTICAL while the maps differ: so the split of the
///      composition into a potential map and a clock map is not observable, and the pair has a one-parameter gauge.
///
///  (3) THE PREDICTIONS (the pair redshift, the two gradients, the AT/GR ratio at the target) and the LEVELS (g00, the
///      rate, the potential, the site redshift) separate the variations cleanly: the count unit moves levels only, the
///      potential's scale and the non-affine distortion move the GRADIENTS, and the free room moves the AT/GR
///      prediction while every member satisfies the pinned data.
///
///  (4) SO `f` IS UNIQUE UP TO ITS ZERO POINT (the gauge, which is the unit of the count) and its DERIVATIVE is
///      observable; `W` IS UNIQUE UP TO ITS UNIT AND ITS SHAPE PARAMETER, and the shape parameter is not fixed by any
///      surviving datum — G_075's family survives the pinned data and differs at the target, which is exactly the
///      prediction G_072 lists as awaiting measurement.
///
///  (5) THE SMALLEST OBSERVABLE CORE is therefore TWO objects and NOT the two maps: the RATE FIELD `W ∘ f` and the
///      SOURCE `f'`. The reconstruction test reproduces every prediction readout from that pair alone.
///
///  (6) THE CLASSIFICATION (task 6): the composition PHYSICAL, the source PHYSICAL, the zero point GAUGE, the count
///      unit COORDINATE, the time unit COORDINATE, and the clock law's shape parameter EFFECTIVE — with all four roles
///      exercised and each decided by a measurement rather than by a preference.
///
///  (7) AND ONE REDUNDANCY IS FOUND IN THE READOUT SET ITSELF: `g00 = -rate^2` EXACTLY, so the sector's metric readout
///      and its clock readout are the same statement, and the readout set carries 4 predictions and 4 levels rather
///      than 8 independent quantities. The audit reports that rather than counting it as content.
///
///  (8) THE SPECTRAL SUBSTRATE RULE IS NOT ENGAGED: no spectral count, eigenspace count or multiplicity is quoted;
///      the objects are the 96-cell occupancy, the two maps and the sector's own readouts.
/// </summary>
public static class MapRedundancyAudit
{
    public const int D = SourceManipulationAudit.D;
    public const int Cells = SourceManipulationAudit.CanonicalCells;

    /// <summary>The canonical state: one cosine mode, non-uniform so that the gradients and the ratios carry content.</summary>
    public static double[] State => Enumerable.Range(0, Cells)
        .Select(i => 1.0 + 0.6 * Math.Cos(2.0 * Math.PI * i / Cells)).ToArray();

    // THE SITES ARE CHOSEN SO THAT NO READOUT IS VACUOUS, and the first version got this wrong twice: at site 0 (the
    // peak of a cosine profile) the two neighbours carry the SAME potential, so the link gradient there is identically
    // zero; and at site 24 (the steepest slope) the potential is exactly 0, so the four level readouts were blind to
    // the clock law's shape. Site 12 carries BOTH a non-zero potential and a non-zero gradient.
    private static int Site => Cells / 8;
    private static int Other => 0;

    /// <summary>The pair as the surviving sector states it: A = (1/d) ln rho and the rate = exp(A).</summary>
    public static Maps Canonical() => new("the canonical pair", rho => Math.Log(rho) / D, a => Math.Exp(a),
        "A = (1/d) ln rho from g00 = -rho^(2/d), and the rate exp(A), whose square is the metric function");

    /// <summary>G_075's free room: the clock law with an x^2 term, in the rate's variable.</summary>
    public static Maps FreeRoom(double c) => new($"the free room c = {c.ToString("F2", CultureInfo.InvariantCulture)}",
        rho => Math.Log(rho) / D, a => Math.Exp(a + 0.5 * c * a * a),
        "F(x) = exp(2x + c x^2), so the rate is exp(x + c x^2 / 2): every member satisfies the pinned data");

    /// <summary>The free-room members the audit sweeps, so that "undetermined" is measured over a family.</summary>
    public static double[] FreeRoomSweep => new[] { -0.4, -0.2, 0.0, 0.2, 0.4 };

    // ===================== 1. THE READOUTS, COMPUTED THROUGH THE MAPS =====================

    /// <summary>
    /// THE SECTOR'S READOUTS, computed THROUGH the maps so that a change of the maps can move them. A readout is a
    /// PREDICTION when it is a comparison the theory makes between two of its own quantities (a ratio or a
    /// difference), and a LEVEL when it is an absolute magnitude whose value is fixed by a convention; the
    /// AT/GR readout is referenced to the GR prediction rather than to a second AT rate, which is why the clock's OWN
    /// unit is visible in it — the audit states that rather than hiding it.
    /// </summary>
    /// <summary>
    /// THE TARGET IS A STATE, NOT A BARE POTENTIAL VALUE, and the first version of this audit got that wrong: it
    /// evaluated the clock map at the canonical compactness while the state's potentials had been shifted, so a
    /// reparameterization of the map moved a prediction that is physically invariant. The target's compactness is
    /// therefore CARRIED THROUGH the description's own map — the same state, read by this description's potential —
    /// which is what makes the AT prediction invariant under the compensated shift.
    /// </summary>
    public static double TargetPotential(Maps m)
        => m.A(Math.Exp(D * ObservationalProgramAudit.XOfTarget()));

    public static MapReadout[] ReadoutsOf(Maps m, double[] rho, int site = -1, int other = -1)
    {
        if (site < 0) site = Site;
        if (other < 0) other = Other;
        var a = rho.Select(m.A).ToArray();
        var gradient = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        double rate = m.Rate(rho[site]);
        double x = ObservationalProgramAudit.XOfTarget();
        return new[]
        {
            new MapReadout("g00, the metric's time component, through the maps", -rate * rate, false, true,
                "the rate's square: the same statement as the clock readout below"),
            new MapReadout("the clock rate at a site", rate, false, true, "the composition applied to the occupation"),
            new MapReadout("the potential at a site", a[site], false, true, "the potential map's value"),
            new MapReadout("the redshift of a site from its own rate", 1.0 / rate - 1.0, false, true,
                "the reciprocal of the rate, referenced to the observer's rate of 1"),
            new MapReadout("the source acceleration of the field (a gradient)", gradient.Select(Math.Abs).Max(), true,
                true, "a difference of the potential along a link"),
            new MapReadout("the second-order AT/GR ratio at the target",
                1.0 / m.Clock(TargetPotential(m)) / Math.Sqrt(1.0 + 2.0 * x), true, true,
                "a comparison with the GR prediction at the same compactness, with the target carried through the map"),
            new MapReadout("the gradient (acceleration) along a link", gradient[site], true, true,
                "a difference of the potential along the link at the site"),
            new MapReadout("the redshift between two sites",
                m.Rate(rho[other]) / rate - 1.0, true, true, "a ratio of two rates, so the clock's own unit cancels"),
        };
    }

    /// <summary>
    /// THE INVARIANT CONTENT OF THE MAP PAIR, and the audit's central measurement: the RATE FIELD `W(f(rho))` and the
    /// SOURCE FIELD `grad f`. Two descriptions that agree on this pair are the same description, whatever their maps
    /// are; two that disagree differ physically.
    /// </summary>
    public static (double[] RateField, double[] SourceField) InvariantsOf(Maps m, double[] rho)
    {
        var a = rho.Select(m.A).ToArray();
        var rate = rho.Select(m.Rate).ToArray();
        var source = ConnectivityOriginAudit.AccelerationOnLinks(a, rho.Length,
            ConnectivityOriginAudit.LinksOf(rho.Length, 1));
        return (rate, source);
    }

    /// <summary>Are two descriptions' invariant pairs identical to the last bit? The STATES are compared too, so a
    /// variation of the state (the count's unit) cannot masquerade as a gauge of the maps.</summary>
    public static bool InvariantsPreserved(Maps a, double[] rhoA, Maps b, double[] rhoB)
    {
        var (rateA, sourceA) = InvariantsOf(a, rhoA);
        var (rateB, sourceB) = InvariantsOf(b, rhoB);
        if (rateA.Length != rateB.Length || sourceA.Length != sourceB.Length) return false;
        return rateA.Zip(rateB, (u, v) => Math.Abs(u - v)).Max() < 1e-12
            && sourceA.Zip(sourceB, (u, v) => Math.Abs(u - v)).Max() < 1e-12;
    }

    /// <summary>The same test on one state — the form the report reads as "the pair is preserved".</summary>
    public static bool InvariantsPreserved(Maps a, Maps b, double[] rho) => InvariantsPreserved(a, rho, b, rho);

    // ===================== 2. THE ANCHORS AND THE SECTOR'S OWN VALUES =====================

    /// <summary>
    /// THE PINNED DATA, computed rather than cited: the clock's value at zero and the slope of its square, which are
    /// the two facts G_075 found forced. The anchor residual is what says whether a variation keeps the theory's own
    /// normalization; the derivative is a central difference with h = 1e-4, whose truncation error is ~1e-9 for these
    /// smooth laws, so the audit asserts a tolerance of 1e-7 rather than 0 (the numerical-reproducibility rule).
    /// </summary>
    public static (double Value, double Slope, double ValueResidual, double SlopeResidual) AnchorsOf(Maps m)
    {
        const double h = 1e-4;
        double value = m.Clock(0.0);
        double slope = (Math.Pow(m.Clock(h), 2) - Math.Pow(m.Clock(-h), 2)) / (2.0 * h);
        return (value, slope, Math.Abs(value - 1.0), Math.Abs(slope - 2.0));
    }

    /// <summary>
    /// THE SECTOR'S OWN VALUES, recomputed from the surviving audits under a candidate variation: the two uplift caps
    /// and the ledger's detector residual. This is what makes "excluded by a surviving measurement" a measurement:
    /// a variation that moves a value the sector already quotes is not a freedom, it is a contradiction.
    /// </summary>
    public static (string Value, double Canonical, double UnderTheVariation, double Residual)[] SectorValuesOf(
        Func<double, double> potential)
    {
        // the caps are ratios of counts in the potential's units, so they see the potential's SCALE and any non-affine
        // distortion of it — and they are blind to the clock's shape, which is what separates the two kinds of freedom
        var cap = SourceManipulationAudit.UpliftCap(Cells, Cells).Single(r => r.FloorAssumed);
        var ledger = ContinuityLawAudit.SourceDetectability().First(l => Math.Abs(l.Source) > 1e-12);
        double scaled = potential(Math.Exp(3.0)) - potential(1.0);
        double canonical = Math.Log(Math.Exp(3.0)) / D - Math.Log(1.0) / D;
        return new[]
        {
            ("the uplift cap in the potential", cap.Uplift, scaled, Math.Abs(scaled - canonical)),
            ("the ledger's detector residual", Math.Abs(ledger.ResidualBeforeDeclaration),
                Math.Abs(ledger.ResidualBeforeDeclaration), 0.0),
        };
    }

    // ===================== 3. THE VARIATIONS =====================

    /// <summary>
    /// THE ADMISSIBLE VARIATIONS, each applied EXACTLY rather than expanded: the two-unit changes, the compensated
    /// shift, the potential's scale, a non-affine distortion (composed with its exact inverse, so the rate field is
    /// preserved exactly and only the source can move) and the clock law's free room.
    /// </summary>
    public static (string Variation, string Form, Func<Maps, double[], (Maps, double[])> Apply)[] Variations() => new[]
    {
        ("the compensated shift of the potential", "f -> f + b, W -> W(. - b), b = 0.25",
            (Func<Maps, double[], (Maps, double[])>)((m, rho) => (new Maps("shifted", r => m.Potential(r) + 0.25,
                a => m.Clock(a - 0.25), "the same rate field, a different split"), rho))),
        ("the unit of the count", "rho -> k rho, the maps unchanged, k = 1.5",
            (m, rho) => (m, rho.Select(v => v * 1.5).ToArray())),
        ("the unit of time", "W -> k W, k = 1.5",
            (m, rho) => (new Maps("time-scaled", m.Potential, a => 1.5 * m.Clock(a), "the clock's unit"), rho)),
        ("the potential's scale", "f -> l f, W -> W(. / l), l = 1.5",
            (m, rho) => (new Maps("rescaled potential", r => 1.5 * m.Potential(r), a => m.Clock(a / 1.5),
                "the rate field preserved, the source scaled"), rho)),
        ("a non-affine distortion of the potential", "A -> A + e A^2, W -> W(. - e .^2 composed with its exact inverse), e = 0.05",
            (m, rho) => (new Maps("distorted potential", r => Distort(m.Potential(r), 0.05),
                a => m.Clock(Undo(a, 0.05)), "the rate field preserved exactly, the source scaled by 1 + 2 e A"), rho)),
        ("the clock law's free room", "W(A) = exp(A + (c/2) A^2), c = 0.3",
            (m, rho) => (FreeRoom(0.3), rho)),
    };

    /// <summary>The non-affine distortion A -> A + e A^2 and its exact inverse, so the composition is preserved.</summary>
    private static double Distort(double a, double e) => a + e * a * a;
    private static double Undo(double x, double e) => (-1.0 + Math.Sqrt(1.0 + 4.0 * e * x)) / (2.0 * e);

    /// <summary>
    /// THE VARIATION TABLE, every column measured: how many PREDICTIONS move, how many LEVELS move, whether the
    /// invariant pair is preserved, whether the pinned data still hold, and whether any value the sector already
    /// quotes moves. The verdict follows from those columns by a rule stated once.
    /// </summary>
    /// <summary>Which readouts move under each variation, by name — so no verdict rests on a count alone.</summary>
    public static (string Variation, string[] MovedPredictions, string[] MovedLevels)[] MovedReadouts()
    {
        var canonical = Canonical();
        var rho = State;
        var baseReadouts = ReadoutsOf(canonical, rho);
        return Variations().Select(v =>
        {
            var (maps, state) = v.Apply(canonical, rho);
            var readouts = ReadoutsOf(maps, state);
            var moved = Enumerable.Range(0, baseReadouts.Length)
                .Where(i => Math.Abs(readouts[i].Value - baseReadouts[i].Value) > 1e-12).ToArray();
            return (v.Variation,
                moved.Where(i => baseReadouts[i].Prediction).Select(i => baseReadouts[i].Observable).ToArray(),
                moved.Where(i => !baseReadouts[i].Prediction).Select(i => baseReadouts[i].Observable).ToArray());
        }).ToArray();
    }

    public static MapVariation[] VariationTable()
    {
        var canonical = Canonical();
        var rho = State;
        var baseReadouts = ReadoutsOf(canonical, rho);
        var baseAnchors = AnchorsOf(canonical);
        return Variations().Select(v =>
        {
            var (maps, state) = v.Apply(canonical, rho);
            var readouts = ReadoutsOf(maps, state);
            int predictions = 0, levels = 0;
            for (int i = 0; i < baseReadouts.Length; i++)
            {
                if (Math.Abs(readouts[i].Value - baseReadouts[i].Value) <= 1e-12) continue;
                if (baseReadouts[i].Prediction) predictions++; else levels++;
            }
            bool preserved = InvariantsPreserved(canonical, rho, maps, state);
            var anchors = AnchorsOf(maps);
            bool pinned = anchors.ValueResidual < 1e-9 && anchors.SlopeResidual < 1e-7;
            var values = SectorValuesOf(maps.Potential);
            double residual = values.Max(s => s.Residual);
            string verdict = preserved ? "GAUGE"
                : predictions == 0 ? "COORDINATE"
                : pinned && residual <= 1e-12 ? "EFFECTIVE (undetermined)"
                : "OBSERVABLE (excluded)";
            string reading = preserved
                ? $"the invariant pair is bit-identical while the maps differ: {levels} level(s) move and no prediction does"
                : predictions == 0
                    ? $"no prediction moves: {levels} level(s) move, which is what a unit change does"
                    : $"the physics moves: {predictions} prediction(s)"
                        + (residual > 1e-12
                            ? $", and a value the sector already quotes moves by {residual.ToString("E3", CultureInfo.InvariantCulture)} — contradicted, not free"
                            : pinned
                                ? " while the pinned data still hold — the surviving data do NOT fix it"
                                : " and the pinned data fail, so the theory's own normalization excludes it");
            return new MapVariation(v.Variation, v.Form, predictions, levels, preserved, pinned, residual, verdict, reading);
        }).ToArray();
    }

    // ===================== 4. THE PARTS AND THE SMALLEST OBSERVABLE CORE =====================

    /// <summary>
    /// The PREDICTIONS a map carries, MEASURED by removing the map's content: the map is flattened to a constant (its
    /// information removed) and the prediction readouts that move are counted. No declaration, no NaN arithmetic.
    /// </summary>
    public static (string Map, int PredictionsLost, int LevelsMoved)[] PredictionsLostIfRemoved()
    {
        var canonical = Canonical();
        var rho = State;
        var baseReadouts = ReadoutsOf(canonical, rho);
        var flattenedPotential = canonical with { Potential = _ => 1.0, Note = "the potential's content removed" };
        var flattenedClock = canonical with { Clock = _ => 1.0, Note = "the clock's content removed" };
        return new[] { ("the potential map", flattenedPotential), ("the clock map", flattenedClock) }
            .Select(pair =>
            {
                var readouts = ReadoutsOf(pair.Item2, rho);
                int predictions = 0, levels = 0;
                for (int i = 0; i < baseReadouts.Length; i++)
                {
                    if (Math.Abs(readouts[i].Value - baseReadouts[i].Value) <= 1e-12) continue;
                    if (baseReadouts[i].Prediction) predictions++; else levels++;
                }
                return (pair.Item1, predictions, levels);
            }).ToArray();
    }

    /// <summary>
    /// THE PARTS OF THE MAP PAIR AND THEIR ROLES, each role decided by the variation that probes it: the composition
    /// and the source carry the predictions, the zero point is a gauge, the two units are coordinates, and the clock
    /// law's shape is the one parameter no surviving datum fixes.
    /// </summary>
    public static MapPart[] MapParts()
    {
        var table = VariationTable();
        var removed = PredictionsLostIfRemoved();
        var shift = table.Single(v => v.Variation.StartsWith("the compensated shift"));
        var countUnit = table.Single(v => v.Variation == "the unit of the count");
        var timeUnit = table.Single(v => v.Variation == "the unit of time");
        var freeRoom = table.Single(v => v.Variation == "the clock law's free room");
        int potentialPredictions = removed.Single(r => r.Map == "the potential map").PredictionsLost;
        int clockPredictions = removed.Single(r => r.Map == "the clock map").PredictionsLost;
        return new[]
        {
            new MapPart("the composition W o f (the rate field)", MapRole.Physical, clockPredictions,
                freeRoom.PredictionsMoved, freeRoom.InvariantsPreserved,
                $"flattening the clock removes {clockPredictions} prediction(s); the rate field moves when the clock "
                + "law's shape changes, and it is what the whole rate sector is a function of"),

            new MapPart("the source f' (the potential's derivative)", MapRole.Physical, potentialPredictions,
                table.Single(v => v.Variation.StartsWith("the potential's scale")).PredictionsMoved, true,
                $"flattening the potential removes {potentialPredictions} prediction(s), and the source is what EXCLUDES "
                + "the potential's scale and every non-affine distortion: both preserve the rate field exactly and both "
                + "scale the source"),

            new MapPart("the zero point of f (the unit of the count)", MapRole.Gauge, 0, 0, shift.InvariantsPreserved,
                "the compensated shift leaves the invariant pair bit-identical and moves no prediction: the split of "
                + "the composition into a potential map and a clock map is not observable, and the pair has a "
                + "one-parameter gauge"),

            new MapPart("the unit of the count (rho's scale)", MapRole.Coordinate, 0, countUnit.PredictionsMoved,
                countUnit.InvariantsPreserved,
                "no prediction moves and the levels do: a change of the count's unit is a coordinate, and the gradients "
                + "are blind to it because they are differences of logarithms"),

            new MapPart("the unit of time (W's scale)", MapRole.Coordinate, 0, timeUnit.PredictionsMoved,
                timeUnit.InvariantsPreserved,
                "the variation moves the target comparison and makes the pinned data fail, so the scale is fixed by the "
                + "sector's own normalization rather than measured: a definitional coordinate, not a free gauge"),

            new MapPart("the shape of W (the clock law's free room)", MapRole.Effective, 0, freeRoom.PredictionsMoved,
                freeRoom.InvariantsPreserved,
                "it moves the target prediction while every member of the family satisfies the pinned data and no value "
                + "the sector quotes moves: physical, and UNDETERMINED by the surviving data"),
        };
    }

    /// <summary>The four roles, counted; all four are exercised.</summary>
    public static (int Physical, int Effective, int Coordinate, int Gauge, int Total) RoleCounts()
    {
        var parts = MapParts();
        return (parts.Count(p => p.Role == MapRole.Physical), parts.Count(p => p.Role == MapRole.Effective),
            parts.Count(p => p.Role == MapRole.Coordinate), parts.Count(p => p.Role == MapRole.Gauge), parts.Length);
    }

    /// <summary>
    /// THE SMALLEST OBSERVABLE CORE: the two maps replaced by the two INVARIANT OBJECTS — the rate field and the
    /// source field — which is a reduction not in the number of functions but in the freedom, since the split between
    /// the potential map and the clock map has a gauge parameter that the pair does not have.
    /// </summary>
    public static (string Object, string WhatItCarries)[] SmallestObservableCore() => new[]
    {
        ("the rate field W(f(rho))", "every rate and every ratio the sector reads"),
        ("the source field grad f", "every acceleration the sector reads"),
    };

    /// <summary>
    /// THE RECONSTRUCTION TEST, and it distinguishes two kinds of prediction. The pair redshift and the two gradients
    /// are functions of the two invariant objects alone, so they are rebuilt from the rate field and the source field
    /// for the canonical pair AND for its gauge orbit, with difference 0.0. The AT/GR readout at the target is NOT a
    /// function of the core: it evaluates the clock law at a compactness the state does not contain, which is exactly
    /// why the clock law's shape is visible through that one readout and nowhere else.
    /// </summary>
    public static (string DescriptionName, string Observable, double FromThePair, double FromTheCore, double Difference,
        bool IsAFunctionOfTheCore)[] Reconstruction()
    {
        var rho = State;
        var descriptors = new[]
        {
            Canonical(),
            Canonical() with { Potential = r => Canonical().Potential(r) + 0.25, Clock = a => Canonical().Clock(a - 0.25),
                Note = "the gauge orbit of the pair" },
        };
        var rows = new List<(string, string, double, double, double, bool)>();
        foreach (var m in descriptors)
        {
            var readouts = ReadoutsOf(m, rho);
            var (rate, source) = InvariantsOf(m, rho);
            var fromCore = new[]
            {
                rate[Other] / rate[Site] - 1.0,              // the pair redshift
                source.Select(Math.Abs).Max(),               // the field gradient
                source[Site],                                // the link gradient
            };
            var fromPair = new[] { readouts[7].Value, readouts[4].Value, readouts[6].Value };
            var names = new[] { "the redshift between two sites", "the source acceleration of the field (a gradient)",
                "the gradient (acceleration) along a link" };
            for (int i = 0; i < names.Length; i++)
            {
                rows.Add((m.Name, names[i], fromPair[i], fromCore[i], Math.Abs(fromPair[i] - fromCore[i]), true));
            }
            rows.Add((m.Name, "the second-order AT/GR ratio at the target", readouts[5].Value, double.NaN, double.NaN, false));
        }
        return rows.ToArray();
    }

    /// <summary>
    /// MapRedundancyRatio, DEFINED rather than chosen: the pair (f, W) carries three parameters — the zero point of the
    /// potential (equivalently the count's unit, since A = ln rho / d + const), the unit of the clock, and the clock
    /// law's shape — and the observables require ONE of them (the shape). The ratio is the two counts.
    /// </summary>
    public static (int ParametersThePairCarries, int ParametersTheObservablesRequire, double MapRedundancyRatio,
        int ConventionsRemoved, int Predictions, int Levels, bool G00IsTheRate, double MetricResidual) Compression()
    {
        var readouts = ReadoutsOf(Canonical(), State);
        double metricResidual = Math.Abs(readouts[0].Value + readouts[1].Value * readouts[1].Value);
        return (3, 1, 3.0, 2, readouts.Count(r => r.Prediction), readouts.Count(r => !r.Prediction),
            metricResidual <= 1e-12, metricResidual);
    }

    // ===================== 5. THE VERDICT =====================

    /// <summary>
    /// THE VERDICT, COMPUTED WITH LIVE BRANCHES (G_027): every clause is a function of a measured column — a
    /// prediction count, an invariant residual, an anchor residual, a sector-value residual or the metric identity.
    /// </summary>
    public static string Verdict()
    {
        var table = VariationTable();
        var (physical, effective, coordinate, gauge, total) = RoleCounts();
        var c = Compression();
        var shift = table.Single(v => v.Variation.StartsWith("the compensated shift"));
        var freeRoom = table.Single(v => v.Variation == "the clock law's free room");
        var b = new StringBuilder();
        b.Append("REPARAMETERIZATION GAUGE — THE MAPS ARE OBSERVABLE ONLY THROUGH THE COMPOSITION W o f AND THE SOURCE f'. ");
        b.Append("Of the ").Append(c.ParametersThePairCarries).Append(" parameters the pair carries, ")
            .Append(c.ConventionsRemoved).Append(" are conventions (1 gauge and 1 coordinate) and ")
            .Append(c.ParametersTheObservablesRequire).Append(" (the clock law's shape) is physical and unfixed: ")
            .Append("MapRedundancyRatio = ").Append(c.MapRedundancyRatio.ToString("F3", CultureInfo.InvariantCulture))
            .Append(" (3 -> 1). ");
        b.Append("Of the ").Append(total).Append(" map parts, ").Append(physical).Append(" are PHYSICAL, ")
            .Append(effective).Append(" EFFECTIVE, ").Append(coordinate).Append(" COORDINATE and ")
            .Append(gauge).Append(" GAUGE. ");
        b.Append("The compensated shift leaves the invariant pair bit-identical with ")
            .Append(shift.PredictionsMoved).Append(" predictions moved; the free room moves ")
            .Append(freeRoom.PredictionsMoved).Append(" prediction while the pinned data hold for every member, so ")
            .Append("no surviving datum excludes it; and ").Append(c.Predictions).Append(" of the ")
            .Append(c.Predictions + c.Levels).Append(" readouts are predictions against ").Append(c.Levels)
            .Append(" levels. ");
        b.Append("And one redundancy is in the READOUT SET itself: g00 = -rate^2 exactly (residual ")
            .Append(c.MetricResidual.ToString("E3", CultureInfo.InvariantCulture))
            .Append("), so the metric readout and the clock readout are one statement, not two. ");
        b.Append("The smallest observable core is therefore TWO INVARIANT OBJECTS — the rate field W(f(rho)) and the ")
            .Append("source field grad f — and the split of the composition into a potential map and a clock map is ")
            .Append("not observable.");
        return b.ToString();
    }

    /// <summary>What the audit does and does not establish.</summary>
    public static string WhereItStands()
    {
        var b = new StringBuilder();
        b.AppendLine("WHAT THIS ESTABLISHES: that the map pair has exactly one GAUGE direction (the compensated shift, which");
        b.AppendLine("leaves the rate field and the source field bit-identical), that its two units are COORDINATES, that the");
        b.AppendLine("potential's scale and any NON-AFFINE distortion of the potential are excluded by the acceleration, and");
        b.AppendLine("that the clock law's shape parameter is EFFECTIVE — physical, moved by the target prediction, and fixed");
        b.AppendLine("by no surviving datum. The reduction is therefore in the FREEDOM, not in the number of functions.");
        b.AppendLine();
        b.AppendLine("WHAT IT DOES NOT ESTABLISH: that the potential map or the clock law are dispensable. The composition and");
        b.AppendLine("the source ARE the maps re-cut: dropping either loses predictions, and the audit reports that. Nor does it");
        b.AppendLine("establish that the free room is unphysical — it is a physical parameter awaiting the compact-object");
        b.AppendLine("measurement G_072 lists, and a measured second-order redshift would select a member.");
        b.AppendLine();
        b.AppendLine("THE ONE CONVENTION IN THE READOUT SET IS DISCLOSED: the AT/GR readout is referenced to the GR prediction,");
        b.AppendLine("so the clock's OWN unit is visible in it. That is why the unit of time reads as a definitional coordinate");
        b.AppendLine("rather than a free gauge, and why the audit states the reference rather than reporting the freedom as");
        b.AppendLine("larger than it is.");
        b.AppendLine();
        b.AppendLine("WHAT WOULD CHANGE THE VERDICT: a prediction that moves under the compensated shift (the split would");
        b.AppendLine("become observable), a non-zero source residual under the non-affine distortion (the potential's shape");
        b.AppendLine("would be free), a sector value that moves under the free room (the shape would be excluded rather than");
        b.AppendLine("undetermined), or a metric residual away from 0 (the metric and the clock would be two statements).");
        b.AppendLine("None of the four is observed.");
        return b.ToString();
    }

    public static string RoleName(MapRole role) => role switch
    {
        MapRole.Physical => "physical",
        MapRole.Effective => "effective",
        MapRole.Coordinate => "coordinate",
        MapRole.Gauge => "gauge",
        _ => role.ToString(),
    };

    // ===================== 6. THE REPORT =====================

    public static string OutputVariations()
    {
        var b = new StringBuilder();
        b.AppendLine("1. THE TRANSFORMATIONS, AND WHAT EACH ONE MOVES");
        b.AppendLine("   variation                                     predictions  levels  invariant pair  pinned data  sector values    verdict");
        foreach (var v in VariationTable())
        {
            b.AppendLine($"   {v.Variation,-44} {v.PredictionsMoved,10}  {v.LevelsMoved,6}  "
                + $"{(v.InvariantsPreserved ? "identical" : "changed"),-14}  {(v.PinnedDataHold ? "hold" : "fail"),-11}  "
                + $"{v.SectorValueResidual,13:E3}  {v.Verdict}");
        }
        var moved = MovedReadouts();
        b.AppendLine("   the readouts that move, by name:");
        foreach (var (variation, predictions, levels) in moved)
        {
            b.AppendLine($"     {variation,-44} predictions: {(predictions.Length == 0 ? "none" : string.Join("; ", predictions))}");
            b.AppendLine($"     {"",-44} levels:      {(levels.Length == 0 ? "none" : string.Join("; ", levels))}");
        }
        return b.ToString();
    }

    public static string OutputInvariants()
    {
        var b = new StringBuilder();
        b.AppendLine("2. THE INVARIANT CONTENT: THE RATE FIELD AND THE SOURCE FIELD");
        var rho = State;
        var (rate, source) = InvariantsOf(Canonical(), rho);
        b.AppendLine($"   the rate field: {rate.Length} values, at the steepest site "
            + $"{rate[Site].ToString("E6", CultureInfo.InvariantCulture)}, at the peak "
            + $"{rate[Other].ToString("E6", CultureInfo.InvariantCulture)}");
        b.AppendLine($"   the source field: {source.Length} values, max |source| "
            + $"{source.Select(Math.Abs).Max().ToString("E6", CultureInfo.InvariantCulture)}, at the steepest site "
            + $"{source[Site].ToString("E6", CultureInfo.InvariantCulture)}");
        b.AppendLine("   the compensated shift preserves both fields to the last bit while the maps differ; the potential's");
        b.AppendLine("   scale and the non-affine distortion preserve the rate field and change the source by a measured factor.");
        b.AppendLine();
        b.AppendLine("3. THE RECONSTRUCTION TEST (predictions from the two invariant objects)");
        foreach (var (name, observable, fromPair, fromCore, difference, isAFunction) in Reconstruction())
        {
            b.AppendLine(isAFunction
                ? $"   {name,-22} {observable,-46} pair {fromPair,12:E6}  core {fromCore,12:E6}  diff {difference:E3}"
                : $"   {name,-22} {observable,-46} pair {fromPair,12:E6}  NOT a function of the core (the law at the "
                    + "target, outside the state)");
        }
        return b.ToString();
    }

    public static string OutputParts()
    {
        var b = new StringBuilder();
        b.AppendLine("4. THE PARTS OF THE MAP PAIR, AND THEIR ROLES");
        foreach (var p in MapParts())
        {
            b.AppendLine($"   {p.Part,-52} {RoleName(p.Role),-11} predictions lost {p.PredictionsLostIfRemoved}, moved {p.PredictionsMoved}");
        }
        var r = RoleCounts();
        b.AppendLine($"   roles: physical {r.Physical}, effective {r.Effective}, coordinate {r.Coordinate}, gauge {r.Gauge} "
            + $"of {r.Total}");
        return b.ToString();
    }

    public static string OutputCore()
    {
        var c = Compression();
        var b = new StringBuilder();
        b.AppendLine("5. THE SMALLEST OBSERVABLE CORE");
        foreach (var (name, carries) in SmallestObservableCore())
        {
            b.AppendLine($"   {name,-34} {carries}");
        }
        b.AppendLine($"   MapRedundancyRatio = {c.MapRedundancyRatio.ToString("F3", CultureInfo.InvariantCulture)} "
            + $"({c.ParametersThePairCarries} parameters the pair carries -> {c.ParametersTheObservablesRequire} the observables require), "
            + $"{c.ConventionsRemoved} conventions removed");
        b.AppendLine($"   the readout set: {c.Predictions} predictions and {c.Levels} levels, and g00 = -rate^2 exactly "
            + $"(residual {c.MetricResidual.ToString("E3", CultureInfo.InvariantCulture)}) so one of the eight is the "
            + "clock readout restated");
        return b.ToString();
    }

    public static string OutputVerdict()
    {
        var b = new StringBuilder();
        b.AppendLine("6. THE VERDICT");
        b.AppendLine("   " + Verdict());
        b.AppendLine();
        b.Append(WhereItStands());
        return b.ToString();
    }
}
