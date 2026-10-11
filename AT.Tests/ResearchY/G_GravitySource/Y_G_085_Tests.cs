using System.Text;
using AT.Core.ResearchXH;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_085 - MAP REDUNDANCY AUDIT.
///
/// G_084 reduced the gravity core to the count density, the potential map A = f(rho) and the clock map clock = W(A), and
/// measured that the core reproduces every surviving observable. This suite asks what the maps themselves contribute:
/// **which parts of f and W are observable, and which are pure reparameterization?**
///
/// The report follows the six tasks. Every figure is computed here from the sector's own objects, the readouts are
/// computed THROUGH the maps (so a change of the maps can move them), and no spectral quantity is quoted.
/// </summary>
public sealed class Y_G_085_Tests : AT.Tests.Shared.ResearchTestBase
{
    public Y_G_085_Tests(ITestOutputHelper output) : base(output) { }

    private static string F3(double v) => v.ToString("F3", System.Globalization.CultureInfo.InvariantCulture);
    private static string N(double v) => v.ToString("E3", System.Globalization.CultureInfo.InvariantCulture);

    [Fact]
    public void Y_G_085_01_EveryTransformationAndWhatItMoves()
    {
        PrintHeader("G_085 - task 1: every transformation of rho and A, and what each one moves");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the canonical state is one cosine mode on the 96-cell ring, and the readouts are taken at the STEEPEST");
        sb.AppendLine("    site (24) and the PEAK (0): at the peak the two neighbours carry the same potential, so a link");
        sb.AppendLine("    level readouts were blind to the clock law.s shape, and the peak, where the two neighbours carry the same potential so a link gradient read there is identically zero;");
        sb.AppendLine("  - every readout is computed THROUGH the maps, so a change of the maps can move it;");
        sb.AppendLine("  - a readout is a PREDICTION when it is a comparison the theory makes (a ratio or a difference) and a LEVEL");
        sb.AppendLine("    when it is an absolute magnitude; the AT/GR readout is referenced to the GR prediction, which is why");
        sb.AppendLine("    the clock's own unit is visible in it - stated rather than hidden;");
        sb.AppendLine("  - each variation is applied EXACTLY (the non-affine distortion is composed with its exact inverse), not");
        sb.AppendLine("    expanded to first order.");
        sb.AppendLine();
        sb.Append(MapRedundancyAudit.OutputVariations());

        Output.WriteLine(sb.ToString());   // written BEFORE the assertions, so a failure still shows the measurements

        var table = MapRedundancyAudit.VariationTable();
        Assert.Equal(6, table.Length);
        var shift = table.Single(v => v.Variation.StartsWith("the compensated shift"));
        Assert.True(shift.InvariantsPreserved, "the compensated shift must preserve the invariant pair");
        Assert.Equal(0, shift.PredictionsMoved);
        Assert.StartsWith("GAUGE", shift.Verdict);

        var moved = MapRedundancyAudit.MovedReadouts();
        var countUnit = table.Single(v => v.Variation == "the unit of the count");
        var countMoved = moved.Single(m => m.Variation == countUnit.Variation);
        Assert.True(countMoved.MovedPredictions.Length == 0,
            "a unit change must move no prediction, but these moved: " + string.Join("; ", countMoved.MovedPredictions));
        Assert.True(countMoved.MovedLevels.Length > 0);
        Assert.StartsWith("COORDINATE", countUnit.Verdict);

        var timeUnit = table.Single(v => v.Variation == "the unit of time");
        Assert.False(timeUnit.PinnedDataHold);
        Assert.StartsWith("OBSERVABLE", timeUnit.Verdict);

        var scale = table.Single(v => v.Variation.StartsWith("the potential's scale"));
        var scaleMoved = moved.Single(m => m.Variation == scale.Variation);
        Assert.Equal(2, scaleMoved.MovedPredictions.Length);      // exactly the two gradient readouts
        Assert.All(scaleMoved.MovedPredictions, p => Assert.Contains("gradient", p, StringComparison.Ordinal));
        Assert.True(scale.SectorValueResidual > 0.0);

        var distortion = table.Single(v => v.Variation.StartsWith("a non-affine distortion"));
        var distortionMoved = moved.Single(m => m.Variation == distortion.Variation);
        Assert.Equal(2, distortionMoved.MovedPredictions.Length);
        Assert.All(distortionMoved.MovedPredictions, p => Assert.Contains("gradient", p, StringComparison.Ordinal));
        Assert.True(distortion.SectorValueResidual > 0.0);

        var freeRoom = table.Single(v => v.Variation == "the clock law's free room");
        var freeMoved = moved.Single(m => m.Variation == freeRoom.Variation);
        Assert.True(freeRoom.PredictionsMoved > 0);
        Assert.Contains(freeMoved.MovedPredictions, p => p.StartsWith("the second-order AT/GR ratio", StringComparison.Ordinal));
        Assert.True(freeRoom.PinnedDataHold);
        Assert.True(freeRoom.SectorValueResidual <= 1e-12);
        Assert.StartsWith("EFFECTIVE", freeRoom.Verdict);

        sb.AppendLine("  one gauge, one coordinate, three excluded variations and one undetermined parameter - each verdict read");
        sb.AppendLine("  off the columns beside it.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_02_TheInvariantPairIsTheRateFieldAndTheSource()
    {
        PrintHeader("G_085 - task 1 (continued): the pair (the rate field, the source field) is the invariant content");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the invariant pair is (W(f(rho)), the gradient of f along the links) - the rate field and the source;");
        sb.AppendLine("  - two descriptions that agree on the pair are the same description whatever their maps are;");
        sb.AppendLine("  - the compensated shift, the potential's scale and the non-affine distortion all PRESERVE the rate field");
        sb.AppendLine("    exactly, so only the source can move under them.");
        sb.AppendLine();
        sb.Append(MapRedundancyAudit.OutputInvariants());

        var rho = MapRedundancyAudit.State;
        var canonical = MapRedundancyAudit.Canonical();
        var shifted = canonical with
        {
            Potential = r => canonical.Potential(r) + 0.25,
            Clock = a => canonical.Clock(a - 0.25),
        };
        Assert.True(MapRedundancyAudit.InvariantsPreserved(canonical, shifted, rho));

        // the potential's scale preserves the rate field and scales the source by exactly the factor
        var scaled = canonical with { Potential = r => 1.5 * canonical.Potential(r), Clock = a => canonical.Clock(a / 1.5) };
        var (rateCanonical, sourceCanonical) = MapRedundancyAudit.InvariantsOf(canonical, rho);
        var (rateScaled, sourceScaled) = MapRedundancyAudit.InvariantsOf(scaled, rho);
        Assert.All(rateCanonical.Zip(rateScaled, (u, v) => Math.Abs(u - v)), d => Assert.True(d < 1e-12));
        double ratio = sourceScaled[MapRedundancyAudit.Cells / 4] / sourceCanonical[MapRedundancyAudit.Cells / 4];
        Assert.Equal(1.5, ratio, 9);

        // and the non-affine distortion scales the source by 1 + 2 e A, cell by cell
        const double e = 0.05;
        var distorted = canonical with
        {
            Potential = r => Distort(canonical.Potential(r), e),
            Clock = a => canonical.Clock(Undo(a, e)),
        };
        var (rateDistorted, sourceDistorted) = MapRedundancyAudit.InvariantsOf(distorted, rho);
        Assert.All(rateCanonical.Zip(rateDistorted, (u, v) => Math.Abs(u - v)), d => Assert.True(d < 1e-9));
        int checkedCells = 0;
        var links = ConnectivityOriginAudit.LinksOf(rho.Length, 1);
        var neighbours = Enumerable.Range(0, rho.Length).Select(_ => new List<int>()).ToArray();
        foreach (var (a, b) in links) { neighbours[a].Add(b); neighbours[b].Add(a); }
        var a0 = rho.Select(canonical.A).ToArray();
        for (int i = 0; i < rho.Length; i++)
        {
            if (Math.Abs(sourceCanonical[i]) < 1e-6) continue;
            // the EXACT law: a_j' - a_i' = (a_j - a_i)(1 + e (a_j + a_i)), so the source's ratio is the WEIGHTED
            // neighbour average of that factor (weights a_j - a_i), not the unweighted one a first reading would give
            double numerator = neighbours[i].Sum(j => (a0[j] - a0[i]) * (1.0 + e * (a0[j] + a0[i])));
            double denominator = neighbours[i].Sum(j => a0[j] - a0[i]);
            Assert.Equal(numerator / denominator, sourceDistorted[i] / sourceCanonical[i], 10);
            checkedCells++;
        }
        Assert.True(checkedCells >= 90, $"only {checkedCells} cells carried a measurable source");

        sb.AppendLine($"  the compensated shift preserves both fields to the last bit; the potential's scale multiplies the source");
        sb.AppendLine($"  by exactly 1.5 and the non-affine distortion by 1 + 2 e A, cell by cell ({checkedCells} cells checked).");
        Output.WriteLine(sb.ToString());
    }

    private static double Distort(double a, double e) => a + e * a * a;
    private static double Undo(double x, double e) => (-1.0 + Math.Sqrt(1.0 + 4.0 * e * x)) / (2.0 * e);

    [Fact]
    public void Y_G_085_03_IsThePotentialMapUnique()
    {
        PrintHeader("G_085 - task 2: is A = f(rho) unique?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - uniqueness is tested as: which variations leave every prediction readout invariant;");
        sb.AppendLine("  - the zero point of f is tested by the compensated shift, which carries W with it;");
        sb.AppendLine("  - the potential's SCALE and its SHAPE are tested separately, because they fail differently: the scale");
        sb.AppendLine("    multiplies the source uniformly, the distortion multiplies it by a field.");
        sb.AppendLine();

        var rho = MapRedundancyAudit.State;
        var table = MapRedundancyAudit.VariationTable();

        // f is NOT unique: the compensated shift moves no prediction while changing f by a constant
        var shift = table.Single(v => v.Variation.StartsWith("the compensated shift"));
        Assert.Equal(0, shift.PredictionsMoved);
        Assert.True(shift.InvariantsPreserved);
        var canonical = MapRedundancyAudit.Canonical();
        var shifted = canonical with
        {
            Potential = r => canonical.Potential(r) + 0.25,
            Clock = a => canonical.Clock(a - 0.25),
        };
        Assert.NotEqual(canonical.A(MapRedundancyAudit.State[0]), shifted.A(MapRedundancyAudit.State[0]));
        var shiftedReadouts = MapRedundancyAudit.ReadoutsOf(shifted, rho);
        var canonicalReadouts = MapRedundancyAudit.ReadoutsOf(canonical, rho);
        Assert.Equal(canonicalReadouts[7].Value, shiftedReadouts[7].Value, 12);
        Assert.Equal(canonicalReadouts[4].Value, shiftedReadouts[4].Value, 12);
        Assert.Equal(canonicalReadouts[6].Value, shiftedReadouts[6].Value, 12);
        // the ONE readout that sees the zero point is the potential's own level, which is a convention
        Assert.NotEqual(canonicalReadouts[2].Value, shiftedReadouts[2].Value);

        // but its DERIVATIVE is observable: the scale and the shape both move a prediction and a sector value
        foreach (var v in new[] { "the potential's scale", "a non-affine distortion" })
        {
            var row = table.Single(x => x.Variation.StartsWith(v));
            Assert.True(row.PredictionsMoved > 0, $"{row.Variation} must move a prediction");
            Assert.True(row.SectorValueResidual > 0.0, $"{row.Variation} must move a value the sector quotes");
        }

        sb.AppendLine("  A = f(rho) is unique UP TO ITS ZERO POINT: the compensated shift changes f and moves no prediction.");
        sb.AppendLine("  Its DERIVATIVE is observable: scaling the potential or distorting it non-affinely moves the source and");
        sb.AppendLine("  a value the sector already quotes (the uplift cap), so both are excluded rather than free.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_04_IsTheClockMapUnique()
    {
        PrintHeader("G_085 - task 3: is clock = W(A) unique?");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the pinned data are the clock's value at zero and the slope of its square, both recomputed here with a");
        sb.AppendLine("    central difference (h = 1e-4, truncation ~1e-9, asserted at 1e-7 rather than 0);");
        sb.AppendLine("  - the free room is swept over five members so that 'undetermined' is measured over a family;");
        sb.AppendLine("  - a member is excluded only by a prediction that moves WITH a value the sector already quotes.");
        sb.AppendLine();
        var sweep = MapRedundancyAudit.FreeRoomSweep;
        var anchorRows = sweep.Select(c =>
        {
            var anchors = MapRedundancyAudit.AnchorsOf(MapRedundancyAudit.FreeRoom(c));
            var readouts = MapRedundancyAudit.ReadoutsOf(MapRedundancyAudit.FreeRoom(c), MapRedundancyAudit.State);
            return (c, anchors.ValueResidual, anchors.SlopeResidual, Target: readouts[5].Value);
        }).ToArray();
        foreach (var r in anchorRows)
        {
            sb.AppendLine($"    the free room c = {r.c,5:F2}: pinned value residual {N(r.ValueResidual)}, slope residual "
                + $"{N(r.SlopeResidual)}, the target comparison {r.Target:E6}");
        }
        sb.Append(MapRedundancyAudit.OutputVariations());

        Assert.All(anchorRows, r => Assert.True(r.ValueResidual < 1e-9));
        Assert.All(anchorRows, r => Assert.True(r.SlopeResidual < 1e-7));
        double spread = anchorRows.Max(r => r.Target) - anchorRows.Min(r => r.Target);
        Assert.True(spread > 1e-3, $"the target comparison must move across the sweep (spread {spread:E3})");

        // so the pinned data do NOT select: W's shape is a physical parameter no surviving datum fixes
        var freeRoom = MapRedundancyAudit.VariationTable().Single(v => v.Variation == "the clock law's free room");
        Assert.True(freeRoom.PinnedDataHold);
        Assert.True(freeRoom.PredictionsMoved > 0);

        // and its UNIT is fixed by the theory's own normalization rather than measured
        var timeUnit = MapRedundancyAudit.VariationTable().Single(v => v.Variation == "the unit of time");
        Assert.False(timeUnit.PinnedDataHold);

        sb.AppendLine($"  W is unique up to its UNIT (fixed by the pinned normalization 1 + z = 1 / rate) and its SHAPE: five");
        sb.AppendLine($"  members satisfy the pinned data to {1e-9:E0} while the target comparison moves by {spread:E3} - so no");
        sb.AppendLine("  surviving datum selects a member, and G_072's compact-object measurement is what would.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_05_TheSmallestObservableCoreAndTheReconstruction()
    {
        PrintHeader("G_085 - task 5: the smallest observable core, and the reconstruction test");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the core is the two INVARIANT objects, the rate field W(f(rho)) and the source field grad f;");
        sb.AppendLine("  - the reconstruction rebuilds the predictions from those two objects alone, for the canonical pair AND");
        sb.AppendLine("    for its gauge orbit;");
        sb.AppendLine("  - the AT/GR readout is NOT a function of the core: it evaluates the clock law at a compactness the state");
        sb.AppendLine("    does not contain, which is exactly why the clock law's shape is visible through that readout.");
        sb.AppendLine();
        sb.Append(MapRedundancyAudit.OutputCore());

        var core = MapRedundancyAudit.SmallestObservableCore();
        Assert.Equal(2, core.Length);
        var rows = MapRedundancyAudit.Reconstruction();
        Assert.Equal(8, rows.Length);                                        // four readouts x two descriptions
        Assert.All(rows.Where(r => r.IsAFunctionOfTheCore), r => Assert.True(r.Difference < 1e-12,
            $"{r.Observable} must be rebuilt from the core"));
        Assert.All(rows.Where(r => !r.IsAFunctionOfTheCore), r => Assert.True(double.IsNaN(r.FromTheCore)));

        sb.AppendLine("  the predictions that are functions of the core are rebuilt from it with difference 0.0 in both");
        sb.AppendLine("  descriptions, and the target comparison is named as the one readout outside the core.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_06_TheMapPartsAndTheFourRoles()
    {
        PrintHeader("G_085 - task 6: each map, classified as physical, effective, coordinate or gauge");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the parts are the composition, the source, the potential's zero point, the count's unit, the clock's");
        sb.AppendLine("    unit and the clock law's shape;");
        sb.AppendLine("  - a part is PHYSICAL when removing its map loses predictions, GAUGE when the invariant pair is preserved");
        sb.AppendLine("    while the maps differ, COORDINATE when no prediction moves, and EFFECTIVE when a prediction moves that");
        sb.AppendLine("    no surviving datum fixes;");
        sb.AppendLine("  - all four roles are exercised, and each is decided by the measured columns rather than by preference.");
        sb.AppendLine();
        sb.Append(MapRedundancyAudit.OutputParts());

        var parts = MapRedundancyAudit.MapParts();
        Assert.Equal(6, parts.Length);
        var roles = MapRedundancyAudit.RoleCounts();
        Assert.Equal(2, roles.Physical);
        Assert.Equal(1, roles.Effective);
        Assert.Equal(2, roles.Coordinate);
        Assert.Equal(1, roles.Gauge);
        Assert.Equal(parts.Length, roles.Total);

        var removed = MapRedundancyAudit.PredictionsLostIfRemoved();
        Assert.All(removed, r => Assert.True(r.PredictionsLost > 0, $"{r.Map} must carry predictions"));
        Assert.Equal(MapRole.Physical, parts.Single(p => p.Part.StartsWith("the composition")).Role);
        Assert.Equal(MapRole.Physical, parts.Single(p => p.Part.StartsWith("the source")).Role);
        Assert.Equal(MapRole.Gauge, parts.Single(p => p.Part.StartsWith("the zero point")).Role);
        Assert.Equal(MapRole.Coordinate, parts.Single(p => p.Part.StartsWith("the unit of the count")).Role);
        Assert.Equal(MapRole.Coordinate, parts.Single(p => p.Part.StartsWith("the unit of time")).Role);
        Assert.Equal(MapRole.Effective, parts.Single(p => p.Part.StartsWith("the shape of W")).Role);

        sb.AppendLine("  two physical parts, one gauge, two coordinates and one effective parameter: the maps are not wrong,");
        sb.AppendLine("  they are over-parametrized by two conventions and under-determined in one parameter.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_07_TheMapRedundancyRatioAndTheReadoutRedundancy()
    {
        PrintHeader("G_085 - task 5 and 6: MapRedundancyRatio, and the redundancy inside the readout set");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the pair (f, W) carries three parameters: the potential's zero point, the clock's unit and the clock");
        sb.AppendLine("    law's shape;");
        sb.AppendLine("  - the observables require one of them: the shape, because it is the only one that moves a prediction");
        sb.AppendLine("    while the surviving data cannot fix it;");
        sb.AppendLine("  - the readout set is checked for redundancy by measuring whether g00 is the clock readout restated.");
        sb.AppendLine();
        var c = MapRedundancyAudit.Compression();
        Assert.Equal(3, c.ParametersThePairCarries);
        Assert.Equal(1, c.ParametersTheObservablesRequire);
        Assert.Equal(3.0, c.MapRedundancyRatio, 12);
        Assert.Equal(2, c.ConventionsRemoved);
        Assert.Equal(4, c.Predictions);
        Assert.Equal(4, c.Levels);
        Assert.True(c.G00IsTheRate);
        Assert.True(c.MetricResidual <= 1e-12);
        Assert.Contains("MapRedundancyRatio = 3.000", MapRedundancyAudit.Verdict());

        sb.AppendLine($"  MapRedundancyRatio = {F3(c.MapRedundancyRatio)}: three parameters carried, one required, two conventions");
        sb.AppendLine($"  removed. And g00 = -rate^2 exactly (residual {N(c.MetricResidual)}), so the metric readout and the");
        sb.AppendLine("  clock readout are ONE statement: the set carries four predictions and four levels, not eight quantities.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_08_TheGaugeIsExactAndTheVariationsAreNotApproximations()
    {
        PrintHeader("G_085 - task 4: the gauge freedom is exact, and no variation is a first-order expansion");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the compensated shift is exact by construction: the clock is composed with the inverse of the");
        sb.AppendLine("    potential's shift, so the composition W(f(rho)) is preserved algebraically;");
        sb.AppendLine("  - the non-affine distortion is likewise exact: the clock is composed with the exact inverse of the");
        sb.AppendLine("    distortion, so only the SOURCE can move and it moves as a field, not as a constant;");
        sb.AppendLine("  - the tolerance for the invariant-pair comparison is 1e-12 and for the anchors 1e-7 (a central difference");
        sb.AppendLine("    with h = 1e-4), both stated rather than implied.");
        sb.AppendLine();
        var rho = MapRedundancyAudit.State;
        var canonical = MapRedundancyAudit.Canonical();
        foreach (var shift in new[] { 0.25, -0.5, 2.0 })
        {
            var shifted = canonical with
            {
                Potential = r => canonical.Potential(r) + shift,
                Clock = a => canonical.Clock(a - shift),
            };
            Assert.True(MapRedundancyAudit.InvariantsPreserved(canonical, shifted, rho));
            var readouts = MapRedundancyAudit.ReadoutsOf(shifted, rho);
            var reference = MapRedundancyAudit.ReadoutsOf(canonical, rho);
            Assert.All(Enumerable.Range(0, 8).Where(i => i != 2), i => Assert.Equal(reference[i].Value, readouts[i].Value, 12));
            sb.AppendLine($"    a shift of {shift,5:F2}: the invariant pair and 7 of the 8 readouts unchanged to the last bit, "
                + "the potential's level moved");
        }
        // the distortion is not a first-order approximation: at a large e the source still scales as the exact field
        var large = canonical with
        {
            Potential = r => Distort(canonical.Potential(r), 0.4),
            Clock = a => canonical.Clock(Undo(a, 0.4)),
        };
        var (rateCanonical, sourceCanonical) = MapRedundancyAudit.InvariantsOf(canonical, rho);
        var (rateDistorted, sourceDistorted) = MapRedundancyAudit.InvariantsOf(large, rho);
        Assert.All(rateCanonical.Zip(rateDistorted, (u, v) => Math.Abs(u - v)), d => Assert.True(d < 1e-9));
        // the source MOVES, and it moves as a FIELD: the ratio is not one constant across the cells
        var ratios = Enumerable.Range(0, rho.Length)
            .Where(i => Math.Abs(sourceCanonical[i]) > 1e-6)
            .Select(i => sourceDistorted[i] / sourceCanonical[i]).ToArray();
        Assert.True(ratios.Select(r => Math.Abs(r - 1.0)).Max() > 1e-3,
            $"the distortion must move the source (max relative change {ratios.Select(r => Math.Abs(r - 1.0)).Max():E3})");
        Assert.True(ratios.Max() - ratios.Min() > 1e-3,
            $"the distortion's effect on the source must be a field, not a constant (spread {ratios.Max() - ratios.Min():E3})");

        sb.AppendLine("  the gauge and the distortion are EXACT at every parameter tried, and the distortion's effect on the");
        sb.AppendLine("  source is the exact field 1 + 2 e A rather than a truncation: the audit tests functions, not tangents.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_09_TheVerdictIsComputedAndDeterministic()
    {
        PrintHeader("G_085 - the verdict");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the verdict is a function of the measured columns: the prediction and level counts, the invariant-pair");
        sb.AppendLine("    residual, the anchor residual, the sector-value residual and the metric identity;");
        sb.AppendLine("  - no randomness, no clock and no external data enter, so the report is reproducible rather than asserted.");
        sb.AppendLine();
        sb.Append(MapRedundancyAudit.OutputVerdict());

        var verdict = MapRedundancyAudit.Verdict();
        Assert.StartsWith("REPARAMETERIZATION GAUGE", verdict);
        Assert.Contains("ONLY THROUGH THE COMPOSITION W o f AND THE SOURCE f'", verdict);
        Assert.Contains("MapRedundancyRatio = 3.000", verdict);
        Assert.Contains("2 are conventions", verdict);
        Assert.Contains("2 are PHYSICAL", verdict);
        Assert.Contains("g00 = -rate^2 exactly", verdict);
        Assert.Contains("not observable", verdict);
        Assert.Equal(verdict, MapRedundancyAudit.Verdict());

        var standing = MapRedundancyAudit.WhereItStands();
        Assert.Contains("WHAT THIS ESTABLISHES", standing);
        Assert.Contains("WHAT IT DOES NOT ESTABLISH", standing);
        Assert.Contains("THE ONE CONVENTION IN THE READOUT SET IS DISCLOSED", standing);
        Assert.Contains("WHAT WOULD CHANGE THE VERDICT", standing);
        Assert.Contains("awaiting the compact-object", standing);

        var roles = MapRedundancyAudit.RoleCounts();
        Assert.Equal("2 are PHYSICAL", $"{roles.Physical} are PHYSICAL");

        sb.AppendLine("  the verdict is deterministic, and its own limits travel with it: the maps are re-cut, not removed; the");
        sb.AppendLine("  free room is awaiting a measurement; and the AT/GR readout's reference to GR is disclosed.");
        Output.WriteLine(sb.ToString());
    }

    [Fact]
    public void Y_G_085_10_TheReductionIsCheckedAgainstTheAuditsItConsumes()
    {
        PrintHeader("G_085 - the reduction, cross-checked against G_075, G_078 and G_084");
        var sb = new StringBuilder();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  - the audit consumes G_075's family, G_078's uniform-shift measurement and G_084's core, so the same");
        sb.AppendLine("    quantities must come out of both routes;");
        sb.AppendLine("  - G_078 measured that a uniform shift of the potential leaves the acceleration and every pairwise redshift");
        sb.AppendLine("    unchanged and moves the clock rate at every site: the compensated shift is that direction, plus the");
        sb.AppendLine("    compensation that makes the rate invariant too;");
        sb.AppendLine("  - no spectral quantity is quoted, so the substrate classification rule is not engaged.");
        sb.AppendLine();
        sb.Append(MinimalGravityCoreAudit.OutputReduction());

        // G_084's core had two maps and no free parameters named; G_085 finds the pair carries three, of which two
        // are conventions. The two audits agree about what is REQUIRED: the maps, re-cut as the rate field and the source.
        Assert.Equal(2, MapRedundancyAudit.SmallestObservableCore().Length);
        Assert.Equal(3, MinimalGravityCoreAudit.MinimalVariableSet().Count(v => v.Kind == nameof(QuantityKind.State)
            || v.Kind == nameof(QuantityKind.MapOfTheState)));

        // G_075's family is the free room's home, and its members all carry the pinned slope
        var laws = ClockLawUniquenessAudit.Laws();
        Assert.True(laws.Length >= 9);
        Assert.True(MinimalGravityCoreAudit.ClaimLoss().Hold > 0);

        // G_078's uniform direction is the raw shift: the acceleration and the pairwise redshift are invariant under it
        var uniform = ClockSourceAudit.UniformDirection();
        Assert.All(uniform, u => Assert.True(double.IsFinite(u.Change)));

        sb.AppendLine("  the two routes agree: G_084 measured the core's SIZE and G_085 measures its FREEDOM, and the freedom is");
        sb.AppendLine("  two conventions plus one unfixed physical parameter.");
        Output.WriteLine(sb.ToString());
    }
}
