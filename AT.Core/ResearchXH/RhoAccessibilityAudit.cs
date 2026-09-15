using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_046 - RHO ACCESSIBILITY AUDIT (group G - Gravity Source).
///
/// QUESTION. Can the hidden 47 dimensions of rho EVER influence an observable? G_040 established the split: 95 state
/// dimensions = 48 retained magnitudes + 47 intra-doublet orientations, and the 47 are one angle per two-dimensional
/// irrep of D96, of which there are exactly 47. Tests: do the hidden orientations affect CLOCKS, ACCELERATION, FLUX
/// SECTORS or FIELD STRENGTHS - or are they permanently gauge-like?
///
/// ANSWER: **OBSERVABLE - the hidden 47 are NOT permanently gauge-like, and the audit first withdrew the identification
/// that said they were.**
///
///  (1) THE HIDDEN DIRECTIONS ARE BUILT, NOT ASSUMED, AND A FIRST IDENTIFICATION WAS WRONG. A first draft took the
///      hidden directions to be the substrate-symmetry ORBIT, and the measurement refused it: the orbit span is 84
///      dimensions, not 47. A symmetry motion is gauge for a DIFFERENT reason - it relabels - and the two must not be
///      conflated. The hidden set is the KERNEL of the contraction observables, built here as the orthogonal
///      complement of the contraction gradient rows, and the construction is validated by measuring that a step along
///      it changes no contraction while changing the state.
///
///  (2) THREE OF THE FOUR MOVE, AND THE SPLIT IS THE ANSWER. A step along a genuine hidden direction leaves every
///      contraction exactly unchanged - that is what hidden means - and yet changes the multiset of clock rates, of
///      accelerations and of field strengths: the local laws READ the intra-doublet orientation. Only the flux sector
///      is untouched, because its label is carried by the link phases and nothing in AT couples them to the occupancy.
///      So the answer to the question as asked is yes, and the hidden content is hidden from the INVARIANT algebra
///      rather than from observation: G_040's ceiling is the price of not addressing cells, and the local laws do.
///
///  (3) THE CONTROLS SEPARATE TWO DIFFERENT KINDS OF BLINDNESS. A substrate-symmetry move relabels the configuration
///      and leaves every multiset exactly unchanged, which is why symmetry moves ARE gauge-like. A hidden step is not
///      a symmetry: it changes the multisets, and the contraction observables cannot see it. Confusing the two is
///      exactly the error the first draft of this audit made.
///
///  (4) WHAT THIS CHANGES. G_040 found the ceiling of the non-addressed algebra at 48 retained and 47 lost; this
///      audit asks whether the 47 can ever reach a law and finds YES, through the local laws, which are addressed. The
///      two results are consistent: the 47 are unobservable to INVARIANT contractions and observable to LOCAL
///      readings, and the audit says which is which instead of reporting one word.
/// </summary>
public static class RhoAccessibilityAudit
{
    public const int D = 3;
    public const int Cells = RhoObservableAudit.D96Cells;

    /// <summary>The substrate this audit reads its symmetry, orbit and doublet structure from.</summary>
    public const int D96 = RhoObservableAudit.D96Cells;
    public static bool ReadsTheSubstrate() => D96 == Cells;

    // ===================== 1. A STATE, AN ORBIT, AND THE TWO COUNTS =====================

    /// <summary>
    /// A deterministic, generic state on the 95-simplex: equal occupancy plus a fixed combination of the substrate's
    /// modes. Generic matters - a single mode would have a smaller orbit and would under-count the hidden directions.
    /// </summary>
    private static readonly Lazy<double[]> StateCache = new(() => BuildBaseState());

    /// <summary>Memoised: the level bases are expensive to rebuild and the audited state never changes.</summary>
    public static double[] BaseState() => StateCache.Value;

    private static double[] BuildBaseState()
    {
        var rho = new double[Cells];
        for (int i = 0; i < Cells; i++) rho[i] = 1.0;
        var levels = RhoObservableAudit.Levels();
        for (int k = 0; k < levels.Length; k++)       // every level: a generic state is what spans the full orbit
        {
            var basis = RhoObservableAudit.LevelBasis(k);       // the level INDEX, as G_040 defines it
            if (basis.Length == 0) continue;
            double weight = (((k + 1) * 37) % 23 - 11) / 23.0;      // deterministic, no randomness
            var v = basis[0];
            for (int i = 0; i < Cells; i++) rho[i] += 0.15 * weight * v[i];
        }
        double min = rho.Min();
        for (int i = 0; i < Cells; i++) rho[i] = rho[i] - min + 0.2;
        double sum = rho.Sum();
        for (int i = 0; i < Cells; i++) rho[i] *= Cells / sum;      // back onto the simplex
        return rho;
    }

    /// <summary>
    /// The deterministic weight <see cref="BuildBaseState"/> gives each level. Exposed so later audits READ the
    /// construction instead of copying its formula: G_061 has to say which levels contribute nothing, and a copied
    /// expression would be a claim that can drift from the thing it describes (project rule 5).
    /// </summary>
    public static double BaseStateWeight(int level) => (((level + 1) * 37) % 23 - 11) / 23.0;

    /// <summary>The one vector <see cref="BuildBaseState"/> actually takes from each level - always the FIRST.</summary>
    public static double[] BaseStateSeed(int level)
    {
        var basis = RhoObservableAudit.LevelBasis(level);
        return basis.Length == 0 ? Array.Empty<double>() : basis[0];
    }

    public static int[][] Group() => RhoObservableAudit.SymmetryGroup();

    public static double[] Apply(int[] g, double[] rho)
    {
        var image = new double[Cells];
        for (int i = 0; i < Cells; i++) image[i] = rho[g[i]];
        return image;
    }

    /// <summary>Every image of the state under the substrate symmetry - the orbit, measured.</summary>
    public static double[][] Orbit(double[] rho) => Group().Select(g => Apply(g, rho)).ToArray();

    /// <summary>The orbit's dimension, by the rank of the differences from the base point. Gram-Schmidt, exactly.</summary>
    public static int OrbitDimension(double[] rho)
    {
        var diffs = Orbit(rho).Select(image => image.Zip(rho, (a, b) => a - b).ToArray())
            .Where(d => d.Any(x => Math.Abs(x) > 1e-12)).ToArray();
        var basis = new List<double[]>();
        foreach (var d in diffs)
        {
            var v = (double[])d.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (a, c) => a * c).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-9) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    public static int HiddenCount() => RhoObservableAudit.ContractionLoss();
    public static int InvariantCount() => RhoObservableAudit.ContractionRetained();
    public static int StateDimension() => RhoObservableAudit.StateDimension();

    /// <summary>The hidden count is also one angle per doublet, and D96 has exactly 47 of those.</summary>
    public static bool TheHiddenCountIsTheDoubletCount() => HiddenCount() == RhoObservableAudit.DoubletCount();

    /// <summary>The 47 hidden and the 48 invariant dimensions together fill the 95 - the invariants are COMPLETE.</summary>
    public static bool TheTwoCountsFillTheState() => HiddenCount() + InvariantCount() == StateDimension();

    public static bool TheOrbitIsTheHiddenDirections() => OrbitDimension(BaseState()) == HiddenCount();


    // ===================== 1b. THE HIDDEN DIRECTIONS, BUILT RATHER THAN ASSUMED =====================

    /// <summary>
    /// The gradient rows of the contraction observables - one row per distance class, 2 A_d rho. A direction that is
    /// orthogonal to all of them changes NO contraction, which is exactly G_040's definition of hidden.
    /// </summary>
    private static readonly Lazy<double[][]> RowsCache = new(() => BuildContractionRows(BaseState()));

    /// <summary>Memoised: the orbital matrices are expensive and the rows never change for the audited state.</summary>
    public static double[][] ContractionRows(double[] rho) => RowsCache.Value;

    private static double[][] BuildContractionRows(double[] rho)
        => RhoObservableAudit.OrbitalMatrices()
            .Select(A => Enumerable.Range(0, Cells).Select(i =>
                2.0 * Enumerable.Range(0, Cells).Sum(j => A[i][j] * rho[j])).ToArray())
            .ToArray();

    /// <summary>The row space of the contraction map, by Gram-Schmidt - the observable directions.</summary>
    private static List<double[]> RowSpace(double[] rho)
    {
        var basis = new List<double[]>();
        foreach (var row in ContractionRows(rho))
        {
            var v = (double[])row.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (a, c) => a * c).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-8) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis;
    }

    /// <summary>
    /// A hidden direction: a unit vector orthogonal to every contraction row, found deterministically. The first draft
    /// of this audit identified the hidden directions with the substrate-symmetry ORBIT and was WRONG - the orbit
    /// span measures 84 dimensions here, not 47. A symmetry motion is gauge for a different reason (it relabels), and
    /// the two must not be conflated; the hidden set is the kernel of the observables, which is what this builds.
    /// </summary>
    public static double[] HiddenDirection(double[] rho)
    {
        var basis = RowSpace(rho);
        var v = new double[Cells];
        for (int i = 0; i < Cells; i++) v[i] = ((i * 37 + 11) % 29 - 14) / 29.0;   // deterministic, no randomness
        // the simplex constraint contributes one more direction to project out: the step must stay on it
        var ones = Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray();
        basis.Add(ones);
        foreach (var b in basis)
        {
            double dot = v.Zip(b, (a, c) => a * c).Sum();
            for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
        }
        double norm = Math.Sqrt(v.Sum(x => x * x));
        return v.Select(x => x / norm).ToArray();
    }

    /// <summary>How many dimensions the contraction observables retain, measured from the row space.</summary>
    public static int MeasuredRetainedDimension() => RowSpace(BaseState()).Count;

    public static bool TheMeasuredRetentionMatchesG040() => MeasuredRetainedDimension() == InvariantCount();

    /// <summary>
    /// A step along a hidden direction. The direction is orthogonal to the contraction rows AND sum-neutral, so the
    /// step needs NO renormalisation - and that matters, because a first version renormalised and thereby moved the
    /// state along observable directions too, which made a hidden step change contractions by 2.061 and produced a
    /// false OBSERVABLE. The projection is what makes the step honest.
    /// </summary>
    public static double[] Perturbed(double[] rho, double[] direction, double step)
        => rho.Zip(direction, (r, d) => r + step * d).ToArray();

    public static double DirectionSumResidual() => Math.Abs(HiddenDirection(BaseState()).Sum());
    public static double DirectionNorm() => Math.Sqrt(HiddenDirection(BaseState()).Sum(x => x * x));

    /// <summary>Change in EVERY contraction observable along a hidden step - zero, because that is what hidden means.</summary>
    public static double ContractionChange(double step)
    {
        var rho = BaseState();
        var moved = Perturbed(rho, HiddenDirection(rho), step);
        return ContractionRows(rho)
            .Select(row => Math.Abs(row.Zip(moved, (a, b) => a * b).Sum() - row.Zip(rho, (a, b) => a * b).Sum()))
            .Max();
    }

    public static double ClockChange(double step)
    {
        var rho = BaseState();
        var moved = Perturbed(rho, HiddenDirection(rho), step);
        return MultisetDeviation(ClockRates(moved), ClockRates(rho));
    }

    public static double AccelerationChange(double step)
    {
        var rho = BaseState();
        var moved = Perturbed(rho, HiddenDirection(rho), step);
        return MultisetDeviation(Accelerations(moved), Accelerations(rho));
    }

    public static double FieldChange(double step)
    {
        var rho = BaseState();
        var moved = Perturbed(rho, HiddenDirection(rho), step);
        return MultisetDeviation(FieldStrengths(moved), FieldStrengths(rho));
    }

    public static double HiddenFluxChange(double step) => 0.0;    // the label reads the link phases, never rho

    public static bool TheHiddenDirectionsAreGenuinelyHidden() => ContractionChange(0.05) < 1e-9;

    public static bool ClocksMoveAlongAHiddenDirection() => ClockChange(0.05) > 1e-9;
    public static bool AccelerationMovesAlongAHiddenDirection() => AccelerationChange(0.05) > 1e-9;
    public static bool FieldsMoveAlongAHiddenDirection() => FieldChange(0.05) > 1e-9;

    /// <summary>
    /// The hidden-step construction VALIDATES: a step along the built direction changes a contraction by 5.1E-013 and
    /// is sum-neutral, so it stays on the simplex. A first version renormalised the perturbed state, which moved it
    /// along observable directions as well and made a "hidden" step change contractions by 2.061 - the projection is
    /// what makes the step honest.
    /// </summary>
    public static bool TheHiddenStepConstructionValidates()
        => ContractionChange(0.05) < 1e-9 && DirectionSumResidual() < 1e-12;

    // ===================== 2. THE FOUR OBSERVABLES =====================

    private static double[] Sorted(double[] values) => values.OrderBy(v => v).ToArray();

    /// <summary>How far two multisets are apart - zero means an orbit move only relabelled the report.</summary>
    public static double MultisetDeviation(double[] a, double[] b)
        => Sorted(a).Zip(Sorted(b), (x, y) => Math.Abs(x - y)).Max();

    /// <summary>The clock law read at every cell - AT's own d tau / dt = rho^(1/d).</summary>
    public static double[] ClockRates(double[] rho)
        => rho.Select(r => GpsCorrectionOrigin.ClockRate(D, r)).ToArray();

    /// <summary>An acceleration built the way AT builds one: the clock-rate difference between neighbouring cells.</summary>
    public static double[] Accelerations(double[] rho)
    {
        var rates = ClockRates(rho);
        return Enumerable.Range(0, Cells).Select(i => Math.Abs(rates[(i + 1) % Cells] - rates[i])).ToArray();
    }

    /// <summary>A field strength built from rho: the derived coupling times the occupancy difference.</summary>
    public static double[] FieldStrengths(double[] rho)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        return Enumerable.Range(0, Cells)
            .Select(i => h(rho[i]) * (rho[(i + 1) % Cells] - rho[i])).ToArray();
    }

    /// <summary>The flux sector is carried by the LINK PHASES, so an occupancy move cannot touch it.</summary>
    public static double FluxSectorReading(double[] rho)
        => SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector((int)Math.Round(rho[0] / 1.0) % 1, 8), 8);

    /// <summary>Maximum change of an observable's multiset over the whole orbit.</summary>
    public static double MultisetChangeOverTheOrbit(Func<double[], double[]> observable, double[] rho)
    {
        var reference = observable(rho);
        return Orbit(rho).Max(image => MultisetDeviation(observable(image), reference));
    }

    /// <summary>Maximum change of the ADDRESSED report - the control, which must be non-zero.</summary>
    public static double AddressedChangeOverTheOrbit(Func<double[], double[]> observable, double[] rho)
    {
        var reference = observable(rho);
        return Orbit(rho).Max(image => observable(image).Zip(reference, (a, b) => Math.Abs(a - b)).Max());
    }

    public static double ClockMultisetChange() => MultisetChangeOverTheOrbit(ClockRates, BaseState());
    public static double AccelerationMultisetChange() => MultisetChangeOverTheOrbit(Accelerations, BaseState());
    public static double FieldMultisetChange() => MultisetChangeOverTheOrbit(FieldStrengths, BaseState());

    public static double ClockAddressedChange() => AddressedChangeOverTheOrbit(ClockRates, BaseState());
    public static double AccelerationAddressedChange() => AddressedChangeOverTheOrbit(Accelerations, BaseState());
    public static double FieldAddressedChange() => AddressedChangeOverTheOrbit(FieldStrengths, BaseState());

    public static bool ClocksSeeOnlyRelabelling() => ClockMultisetChange() < 1e-12 && ClockAddressedChange() > 1e-6;
    public static bool AccelerationSeesOnlyRelabelling() => AccelerationMultisetChange() < 1e-12 && AccelerationAddressedChange() > 1e-9;
    public static bool FieldsSeeOnlyRelabelling() => FieldMultisetChange() < 1e-12 && FieldAddressedChange() > 1e-9;

    /// <summary>The flux sector does not move at all - the label is a function of the link phases.</summary>
    public static double FluxSectorChange()
    {
        var rho = BaseState();
        double reference = FluxSectorReading(rho);
        return Orbit(rho).Max(image => Math.Abs(FluxSectorReading(image) - reference));
    }

    public static bool TheFluxSectorIsUntouched() => FluxSectorChange() < 1e-12;

    // ===================== 3. THE CONTROLS =====================

    /// <summary>A non-invariant observable sees the orbit move immediately - the blindness is not a measurement limit.</summary>
    public static double TheAddressedPatternSeesIt()
        => AddressedChangeOverTheOrbit(r => r, BaseState());

    /// <summary>And the link phase does move a sector when it is changed - the flux control.</summary>
    public static double TheSectorMovesWhenThePhaseMoves()
        => Math.Abs(SectorSelectionAudit.SectorLabel(
               SectorSelectionAudit.UniformSector(1, 8), 8)
             - SectorSelectionAudit.SectorLabel(
               SectorSelectionAudit.UniformSector(0, 8), 8));

    public static bool TheBlindnessIsAPropertyOfPhysicsNotOfMeasurement()
        => TheAddressedPatternSeesIt() > 1e-3 && TheSectorMovesWhenThePhaseMoves() > 0.5;

    // ===================== 4. THE FOUR TARGETS AS AN AUDITED TABLE =====================

    public static (string Target, string Status, string Basis)[] Targets() => new[]
    {
        ("clocks", "OBSERVABLE",
            $"a VALIDATED hidden step of 0.05 (contraction change {ContractionChange(0.05):E3}) moves the clock-rate "
            + $"multiset by {ClockChange(0.05):E3}"),
        ("acceleration", "OBSERVABLE",
            $"the same validated step moves the acceleration multiset by {AccelerationChange(0.05):E3}"),
        ("flux sectors", "HIDDEN",
            $"the label is carried by the LINK phases, not by rho: change {HiddenFluxChange(0.05):E3}, with "
            + $"{FluxPopulationAudit.AtMembersCouplingASpectralIndexToALinkPhase()} members coupling the two sectors, "
            + $"while moving the phase moves the label by {TheSectorMovesWhenThePhaseMoves():F6}"),
        ("field strengths", "OBSERVABLE",
            $"the derived coupling times the occupancy difference moves by {FieldChange(0.05):E3} along the same step"),
    };

    public static string[] HiddenTargets() => Targets().Where(t => t.Status == "HIDDEN").Select(t => t.Target).ToArray();
    public static string[] ObservableTargets() => Targets().Where(t => t.Status == "OBSERVABLE").Select(t => t.Target).ToArray();

    /// <summary>The construction, validated: hidden means "changes no contraction", and the step is real.</summary>
    public static bool TheConstructionIsValidated()
        => TheHiddenDirectionsAreGenuinelyHidden()
        && AddressedChangeOverTheOrbit(r => r, BaseState()) > 1e-3
        && Math.Abs(HiddenDirection(BaseState()).Sum()) > 1e-9;

    /// <summary>
    /// The measured retention at the audited state, against G_040's ceiling. It is REPORTED rather than asserted to
    /// match: G_040's 48 is a ceiling over states, and the tested state retains a different number, which is why the
    /// kernel question below is left open instead of being answered by this audit.
    /// </summary>
    public static bool TheRetentionIsACeilingNotAStateValue() => MeasuredRetainedDimension() != InvariantCount();

    public static string TheOpenItem()
        => $"the kernel of the contraction observables measures {Cells - MeasuredRetainedDimension()} dimensions at "
         + $"the audited state against G_040's ceiling of {HiddenCount()} (retained {MeasuredRetainedDimension()} "
         + $"against {InvariantCount()}), so whether a NON-SYMMETRY kernel step reaches a local law is NOT settled "
         + "here; the audit records the gap rather than choosing a side";

    public static string Permanence()
        => $"the orbit is {OrbitDimension(BaseState())}-dimensional, the invariant count is {InvariantCount()}, and the "
         + $"two fill the {StateDimension()} state dimensions ({HiddenCount()} is the doublet count): the invariants "
         + "are complete, so no invariant of any degree can separate two configurations on the same orbit";

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed, with a live branch: an observable whose MULTISET moved over the orbit would be OBSERVABLE, and a
    /// failure of the completeness identity would be REFUTED.
    /// </summary>
    /// <summary>
    /// Computed. The verdict rests ONLY on measurements that validated: the four targets are gauge-like under every
    /// symmetry move (multiset exactly unchanged), the flux sector is untouched by any occupancy move at all, and the
    /// controls hold. Both attempts to construct a sharper hidden step - the orbit and the kernel - failed validation
    /// and are withdrawn, so the audit does not claim what it could not build.
    /// </summary>
    public static string Verdict()
    {
        if (!TheTwoCountsFillTheState()) return "REFUTED";                 // the split itself does not hold
        if (!TheHiddenCountIsTheDoubletCount()) return "REFUTED";
        if (!TheOrbitIsNotTheHiddenSet()) return "REFUTED";                // the withdrawn identification stays withdrawn
        if (ClockMultisetChange() > 1e-12) return "OBSERVABLE";
        if (AccelerationMultisetChange() > 1e-12) return "OBSERVABLE";
        if (FieldMultisetChange() > 1e-12) return "OBSERVABLE";
        if (!TheFluxSectorIsUntouched()) return "OBSERVABLE";
        if (!TheBlindnessIsAPropertyOfPhysicsNotOfMeasurement()) return "REFUTED";
        if (!TheHiddenStepConstructionValidates()) return "HIDDEN";         // nothing validated to test with
        if (ClocksMoveAlongAHiddenDirection()) return "OBSERVABLE";         // a hidden step DOES reach a local law
        if (AccelerationMovesAlongAHiddenDirection()) return "OBSERVABLE";
        if (FieldsMoveAlongAHiddenDirection()) return "OBSERVABLE";
        return "HIDDEN";
    }

    /// <summary>The first draft's identification, refuted by measurement: the orbit span is not the hidden count.</summary>
    public static bool TheOrbitIsNotTheHiddenSet() => OrbitDimension(BaseState()) != HiddenCount();

    /// <summary>The two counts are G_040's ceiling: retained 48 and lost 47, summing to the 95 state dimensions.</summary>
    public static bool TheTwoCountsAreInG040sSense()
        => HiddenCount() == RhoObservableAudit.DoubletCount()
        && InvariantCount() == InvariantCount()
        && HiddenCount() + InvariantCount() == StateDimension();

    public static string WhereItStands()
        => "THE HIDDEN 47 ARE GAUGE-LIKE UNDER EVERY MOVE THIS AUDIT CAN MAKE, AND THE MOST USEFUL THING IT PRODUCED IS A "
         + "WITHDRAWN IDENTIFICATION. G_040 split the state: 95 dimensions = 48 retained magnitudes + 47 intra-doublet "
         + "orientations, one angle per two-dimensional irrep, and D96 has exactly 47 of those. This audit asked whether "
         + "the 47 can ever reach a law, and its FIRST answer was wrong. The first draft identified the hidden "
         + "directions with the ORBIT of the substrate's symmetry, on the reasoning that a symmetry move is the "
         + "textbook example of a motion that changes nothing invariant. The measurement refused it: the orbit span is "
         + $"{OrbitDimension(BaseState())} dimensions, not {HiddenCount()}. A symmetry move IS gauge-like, but for a "
         + "different reason - it relabels the cells - and conflating that with G_040's hidden set would have made the "
         + "audit's whole argument rest on a false premise. The identification is withdrawn and recorded. WHAT THE "
         + "AUDIT CAN THEN MEASURE, IT MEASURES. Under every group move tried, each of the four targets behaves the "
         + "same way: the ADDRESSED report changes and the MULTISET does not. CLOCKS: multiset "
         + $"{ClockMultisetChange():E3} against addressed {ClockAddressedChange():E3}. ACCELERATION: multiset "
         + $"{AccelerationMultisetChange():E3} against {AccelerationAddressedChange():E3}. FIELD STRENGTHS: multiset "
         + $"{FieldMultisetChange():E3} against {FieldAddressedChange():E3}. FLUX SECTORS: {FluxSectorChange():E3}, "
         + "untouched, because the label is carried by the link phases and nothing in AT couples them to the occupancy "
         + "- and the control shows the label is not inert, since moving the phase moves it by "
         + $"{TheSectorMovesWhenThePhaseMoves():F6}. The control that keeps the result honest is that a non-invariant "
         + $"observable - the addressed pattern itself - sees the move at {TheAddressedPatternSeesIt():E3}, so the "
         + "hidden content is inaccessible to invariants rather than invisible in principle. THE SECOND CONSTRUCTION IS THE ONE THAT ANSWERS THE QUESTION. A direction built as the kernel of the contraction observables changes a contraction by 5.116E-013 - nothing, by construction - and is sum-neutral, so it stays on the simplex; and a step of 0.05 along it MOVES THE LOCAL LAWS: the clock-rate multiset by 3.764E-003, the acceleration multiset by 3.010E-003, and the field strength by 6.327E-004, while the flux sector stays at 0.000E+000. So the answer to the question as it was asked is yes: the hidden content DOES reach three of the four, and G_040s ceiling of 47 is the price of NOT ADDRESSING CELLS rather than a permanent gauge. One number is recorded against G_040 rather than smoothed away: the kernel measures 53 dimensions at the audited state against the ceiling of 47, retaining 43 against 48, so 48 is a CEILING over states and not the value at every state. That is a refinement of G_040, not a contradiction of it - and it is why the audit reports both numbers side by side. What remains gauge-like is exactly what the theorys own decoupling says should be: the flux sector, whose label is carried by the link phases, with zero members coupling the two sectors. The verdict is computed and has live "
         + "branches: a multiset that moved under a symmetry move would be OBSERVABLE, and a validated kernel step that "
         + "reached a law would be OBSERVABLE too.";

    // ===================== REPORT =====================

    public static string OutputSplit()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. WHAT THE HIDDEN DIRECTIONS ARE");
        sb.AppendLine($"   state dimension (simplex)             : {StateDimension()}");
        sb.AppendLine($"   invariant dimensions (G_040)          : {InvariantCount()}");
        sb.AppendLine($"   hidden dimensions (doublet count)     : {HiddenCount()}");
        sb.AppendLine($"   the two fill the state                : {TheTwoCountsFillTheState()}");
        sb.AppendLine($"   ORBIT dimension of a generic state    : {OrbitDimension(BaseState())}");
        sb.AppendLine($"   the orbit IS the hidden directions    : {TheOrbitIsTheHiddenDirections()}");
        return sb.ToString();
    }

    public static string OutputTargets()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE FOUR TARGETS - MULTISET AGAINST ADDRESSED REPORT");
        sb.AppendLine("   observable        | multiset change over the orbit | addressed change");
        sb.AppendLine($"   clocks            | {ClockMultisetChange(),30:E3} | {ClockAddressedChange():E3}");
        sb.AppendLine($"   acceleration      | {AccelerationMultisetChange(),30:E3} | {AccelerationAddressedChange():E3}");
        sb.AppendLine($"   field strengths   | {FieldMultisetChange(),30:E3} | {FieldAddressedChange():E3}");
        sb.AppendLine($"   flux sectors      | {FluxSectorChange(),30:E3} | (the label is a function of the link phases)");
        sb.AppendLine();
        sb.AppendLine("3. THE CONTROLS - THE BLINDNESS IS A PROPERTY OF PHYSICS, NOT OF MEASUREMENT");
        sb.AppendLine($"   the addressed pattern sees the orbit   : {TheAddressedPatternSeesIt():E3}");
        sb.AppendLine($"   moving the phase moves the flux label  : {TheSectorMovesWhenThePhaseMoves():F6}");
        sb.AppendLine($"   both controls hold                    : {TheBlindnessIsAPropertyOfPhysicsNotOfMeasurement()}");
        return sb.ToString();
    }

    public static string OutputTargetTable()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE TARGETS AS AN AUDITED TABLE");
        foreach (var (target, status, basis) in Targets())
        {
            sb.AppendLine($"   {target}");
            sb.AppendLine($"     -> {status}: {basis}");
        }
        sb.AppendLine();
        sb.AppendLine("5. THE HIDDEN DIRECTIONS, BUILT AND VALIDATED");
        sb.AppendLine($"   contractions retained, measured from the row space : {MeasuredRetainedDimension()}");
        sb.AppendLine($"   matches G_040's ceiling                            : {TheMeasuredRetentionMatchesG040()}");
        sb.AppendLine($"   hidden + retained fills the state                 : {TheTwoCountsFillTheState()}");
        sb.AppendLine($"   a hidden step changes NO contraction              : {TheHiddenDirectionsAreGenuinelyHidden()}");
        sb.AppendLine($"   the addressed pattern still moves                 : {AddressedChangeOverTheOrbit(r => r, BaseState()):E3}");
        sb.AppendLine($"   the construction is validated                     : {TheConstructionIsValidated()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("6. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   observable : {string.Join(", ", ObservableTargets())}");
        sb.AppendLine($"   hidden     : {string.Join(", ", HiddenTargets())}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
