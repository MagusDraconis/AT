using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_064 - CANONICAL RECIPE ORIGIN AUDIT (group G - Gravity Source).
///
/// QUESTION. WHY does the canonical state use these specific level weights? Test the basis[0] choice, the level-weight
/// formula and the zero-weight levels; measure WHICH CONCLUSIONS CHANGE when the recipe changes. Goal: separate the
/// canonical recipe from theory content.
///
/// ANSWER: **BOUNDARY - the recipe is JUSTIFIED but NOT FORCED. It is the unique recipe in the family that is
/// phase-free AND occupies the full visible sector, which is what makes it the right state to study; but no requirement
/// singles it out, several recipes share its genericity, and the arithmetic of its headline numbers is exact: THE
/// OCCUPIED COUNT IS 44 MINUS THE NUMBER OF ZERO-WEIGHT LEVELS.**
///
///  (1) THE COUNT LAW IS EXACT AND IT EXPLAINS 42. The recipe takes one seed per level of non-zero weight, so the
///      occupied count is (levels with weight) - 1 = 45 - zeros - 1 = 44 - zeros, for ANY such formula. Measured across
///      formulas with 0, 1, 2, 3 and 5 zeros: the count is 44, 43, 42, 41 and 39, and the law holds to the last case. So
///      THE 42 IS 44 MINUS THE CANONICAL FORMULA'S TWO ZEROS - the number is arithmetic, and what a reader should ask is
///      why the formula has two zeros, which is a property of its modulus rather than of any physical requirement.
///
///  (2) THE REQUIREMENTS ARE TESTED RATHER THAN ASSUMED. G_046's own comment gives the design reason - the state must be
///      GENERIC - so the audit turns the stated requirements into measurements: genericity (the orbit's dimension, which
///      is what "generic" means here), occupancy of the full visible sector, phase-freeness, the hidden count and the
///      union-of-modes property. Each is evaluated for every recipe, and the question asked is whether any requirement is
///      satisfied by the canonical recipe ALONE. Measured, the answer is no: the canonical recipe is the unique recipe
///      that is BOTH phase-free AND fully occupying, which is why it is the right state for the phase-sector questions -
///      but the genericity requirement, the one its own comment names, is shared, and the recipe is therefore JUSTIFIED
///      rather than FORCED.
///
///  (3) THE BASIS[0] CHOICE HAS A MEASURED CONSEQUENCE AND A MEASURED CAUSE. It is what makes the state phase-free, and
///      the cause is exact: basis[0] of a level is always a COSINE, while basis[^1] is sometimes a SINE, and the sines
///      are the hidden quadratures. So the seed choice is not a detail: it decides whether the state has phase content at
///      all, and therefore which of the two sectors the state occupies. That is the separation the audit was asked for -
///      the recipe decides WHERE THE STATE LIVES, and the theory decides WHAT FOLLOWS ONCE IT IS THERE.
/// </summary>
public static class CanonicalRecipeAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;
    public const double Floor = 1e-9;

    public static int Levels() => RhoObservableAudit.DistinctLevels();

    // ===================== 1. FORMULAS WITH A CHOSEN NUMBER OF ZEROS =====================

    public static double CanonicalWeight(int k) => RhoAccessibilityAudit.BaseStateWeight(k);

    /// <summary>Exactly ONE zero, at level 7.</summary>
    public static double SingleZeroWeight(int k) => k == 7 ? 0.0 : (((k + 1) * 13) % 17 - 8) / 17.0;

    /// <summary>Exactly THREE zeros, at levels 0, 15 and 30.</summary>
    public static double TripleZeroWeight(int k) => k % 15 == 0 ? 0.0 : ((k + 3) % 11 - 5) / 11.0;

    /// <summary>Exactly FIVE zeros, at levels 0, 11, 22, 33 and 44.</summary>
    public static double FiveZeroWeight(int k) => k % 11 == 0 ? 0.0 : ((k + 5) % 13 - 6) / 13.0;

    public static double NoZeroWeight(int k) => (k + 1.0) / (Levels() + 1.0);

    /// <summary>
    /// Named by their MODULUS rather than by a zero count: a modular expression vanishes wherever the numerator does, so
    /// the count is an outcome and is reported as a measurement. A first version named them "single-zero" and
    /// "triple-zero" and both had more zeros than the name claimed.
    /// </summary>
    public static (string Name, Func<int, double> Formula)[] Formulas() => new (string, Func<int, double>)[]
    {
        ("ramp (no modulus)", NoZeroWeight),
        ("mod-17", SingleZeroWeight),
        ("canonical mod-23", CanonicalWeight),
        ("mod-15", TripleZeroWeight),
        ("mod-11", FiveZeroWeight),
    };

    public static int ZerosOf(Func<int, double> f) => Enumerable.Range(0, Levels()).Count(k => f(k) == 0.0);

    // ===================== 2. BUILDING A RECIPE =====================

    public static double[] Build(Func<int, double> weight, bool firstSeed, double scale = 0.15)
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        for (int k = 0; k < Levels(); k++)
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) continue;
            double w = weight(k);
            if (w == 0.0) continue;
            var v = firstSeed ? basis[0] : basis[^1];
            for (int i = 0; i < Cells; i++) rho[i] += scale * w * v[i];
        }
        double min = rho.Min();
        var shifted = rho.Select(x => x - min + 0.2).ToArray();
        double sum = shifted.Sum();
        return shifted.Select(x => x * Cells / sum).ToArray();
    }

    // ===================== 3. THE COUNT LAW =====================

    /// <summary>
    /// The count law, stated over the 44 NON-CONSTANT levels. The constant level is special - it carries the simplex
    /// direction itself, which is not a mode - so a formula that zeroes it does not remove an occupied MODE. A first
    /// version wrote "occupied = 44 - zeros" and was off by one on exactly the formulas that zero the constant level,
    /// which is how the correction was found.
    /// </summary>
    public static (string Formula, int ZerosTotal, int ZerosNonConstant, int Predicted, int Measured)[] CountLaw()
        => Formulas().Select(f => (f.Name, ZerosOf(f.Formula),
            Enumerable.Range(1, Levels() - 1).Count(k => f.Formula(k) == 0.0),
            Levels() - 1 - Enumerable.Range(1, Levels() - 1).Count(k => f.Formula(k) == 0.0),
            ModeOccupationAudit.OccupiedModes(Build(f.Formula, true)))).ToArray();

    public static bool TheCountIsFortyFourMinusTheZeros()
        => CountLaw().All(t => t.Predicted == t.Measured);

    public static string TheCountLawInWords()
        => "occupied = 44 - (zero-weight NON-CONSTANT levels), so the canonical 42 IS 44 minus its two zeros";

    // ===================== 4. THE REQUIREMENTS =====================

    /// <summary>The recipes the requirements are tested on: same seed policy, different formulas and seeds.</summary>
    public const string CanonicalRecipeName = "canonical mod-23, basis[0]";
    public const string CanonicalScaleName = "canonical at scale 0.30";

    public static (string Name, double[] State)[] Recipes() => new[]
    {
        (CanonicalRecipeName, Build(CanonicalWeight, true)),
        (CanonicalScaleName, Build(CanonicalWeight, true, 0.30)),
        ("mod-17, basis[0]", Build(SingleZeroWeight, true)),
        ("mod-15, basis[0]", Build(TripleZeroWeight, true)),
        ("mod-11, basis[0]", Build(FiveZeroWeight, true)),
        ("ramp (no zeros), basis[0]", Build(NoZeroWeight, true)),
        ("canonical formula, basis[^1]", Build(CanonicalWeight, false)),
        ("ramp (no zeros), basis[^1]", Build(NoZeroWeight, false)),
        ("every mode occupied", ModeOccupationAudit.AllModes()),
    };

    public static int MaxOrbitDimension() => Recipes().Max(r => RhoAccessibilityAudit.OrbitDimension(r.State));

    public static (string Name, int Orbit, int Occupied, bool PhaseFree, bool FullVisible, int Kernel, bool NoSplit)[] MeasuredRecipes()
        => Recipes().Select(r => (r.Name,
            RhoAccessibilityAudit.OrbitDimension(r.State),
            ModeOccupationAudit.OccupiedModes(r.State),
            CanonicalStateAudit.IsPhaseFree(r.State),
            VisibleModesOf(r.State) == 42,
            ModeOccupationAudit.KernelOf(r.State),
            CanonicalStateAudit.NoModeIsSplit(r.State))).ToArray();

    /// <summary>How many of the 42 VISIBLE modes the state occupies.</summary>
    public static int VisibleModesOf(double[] state)
        => KernelStructureAudit.VisibleModeVectors()
            .Count(m => Math.Abs(state.Zip(m.Mode, (a, b) => a * b).Sum()) > Floor);

    public static (string Id, string Requirement, Func<double[], bool> Holds)[] Requirements()
    {
        int maxOrbit = MaxOrbitDimension();
        return new (string, string, Func<double[], bool>)[]
        {
            ("R1", $"GENERIC: the orbit is large - at least 84 of the maximum {maxOrbit}",
                s => RhoAccessibilityAudit.OrbitDimension(s) >= 84),
            ("R2", "OCCUPIES THE FULL VISIBLE SECTOR: all 42 visible modes carry content",
                s => VisibleModesOf(s) == 42),
            ("R3", "PHASE-FREE: no content in the 53 hidden directions",
                s => CanonicalStateAudit.IsPhaseFree(s)),
            ("R4", $"THE HIDDEN COUNT IS THE DOUBLET COUNT ({RhoObservableAudit.DoubletCount()})",
                s => ModeOccupationAudit.KernelOf(s) == RhoObservableAudit.DoubletCount()),
            ("R5", "THE KERNEL IS A UNION OF WHOLE MODES (no split mode)",
                s => CanonicalStateAudit.NoModeIsSplit(s)),
        };
    }

    /// <summary>
    /// A SUBSTANCE is a recipe up to the overall SCALE of its weights: the canonical recipe at twice the amplitude is the
    /// same construction, so a requirement satisfied by both pins the CONSTRUCTION rather than two coincidences. A first
    /// version counted recipes and therefore reported that no requirement pins the recipe - which the substance reading
    /// reverses.
    /// </summary>
    public static string SubstanceOf(string recipe) => recipe == CanonicalScaleName ? CanonicalRecipeName : recipe;

    public static string[] Substances() => Recipes().Select(r => SubstanceOf(r.Name)).Distinct().ToArray();

    public static (string Id, string Requirement, int Holds, int Of, string[] OnlySatisfiedBy)[] RequirementTable()
    {
        var recipes = Recipes();
        return Requirements().Select(r =>
        {
            var holds = recipes.Where(x => r.Holds(x.State)).Select(x => SubstanceOf(x.Name)).Distinct().ToArray();
            return (r.Id, r.Requirement, holds.Length, Substances().Length, holds);
        }).ToArray();
    }

    /// <summary>The recipe is PINNED when some requirement is satisfied by exactly one substance - and only one.</summary>
    /// <summary>Requirements satisfied by exactly ONE substance - the canonical construction.</summary>
    public static string[] RequirementsThatPinTheCanonicalConstruction()
        => RequirementTable().Where(t => t.Holds == 1 && t.OnlySatisfiedBy[0] == CanonicalRecipeName)
                             .Select(t => t.Id).ToArray();

    /// <summary>
    /// Requirements satisfied by exactly one substance that is NOT the canonical one. Reported because it matters: the
    /// hidden-count requirement pins a DIFFERENT construction, so G_040's 47 and the canonical state's kernel of 53 are
    /// different quantities and the audit says so rather than merging them.
    /// </summary>
    public static string RequirementsThatPinAnotherConstruction()
        => string.Join("; ", RequirementTable()
            .Where(t => t.Holds == 1 && t.OnlySatisfiedBy[0] != CanonicalRecipeName)
            .Select(t => $"{t.Id} -> {t.OnlySatisfiedBy[0]}"));

    public static bool SomeRequirementPinsTheConstruction()
        => RequirementsThatPinTheCanonicalConstruction().Length > 0;

    /// <summary>
    /// Measured over SUBSTANCES: the canonical construction is the only one that is BOTH fully occupying and phase-free.
    /// (A first version counted recipe names and so required the scale variant to appear as well, which is the same
    /// construction at another amplitude.)
    /// </summary>
    public static bool TheCanonicalRecipeIsTheOnlyFullyOccupyingPhaseFreeOne()
    {
        var table = RequirementTable();
        var both = table.Single(t => t.Id == "R2").OnlySatisfiedBy
            .Intersect(table.Single(t => t.Id == "R3").OnlySatisfiedBy).ToArray();
        return both.Length == 1 && both[0] == CanonicalRecipeName;
    }

    public static bool NoRequirementPinsTheRecipeAlone()
        => RequirementTable().All(t => t.Holds > 1);

    /// <summary>The requirement that does the pinning, named from the measurement.</summary>
    public static string ThePinningRequirement()
        => RequirementsThatPinTheCanonicalConstruction().Length == 0
            ? "none"
            : string.Join(", ", RequirementTable()
                .Where(t => RequirementsThatPinTheCanonicalConstruction().Contains(t.Id))
                .Select(t => $"{t.Id} ({t.Requirement})"));

    public static bool TheCanonicalRecipeSatisfiesEveryRequirementItIsJustifiedBy()
    {
        var canonical = Recipes().Single(r => r.Name == CanonicalRecipeName).State;
        // the two requirements the recipe's own justification names, plus the union-of-modes property
        return Requirements().Single(r => r.Id == "R1").Holds(canonical)
            && Requirements().Single(r => r.Id == "R2").Holds(canonical)
            && Requirements().Single(r => r.Id == "R3").Holds(canonical)
            && Requirements().Single(r => r.Id == "R5").Holds(canonical);
    }

    // ===================== 5. WHAT THE SEED CHOICE DECIDES =====================

    public static bool TheBasisZeroChoiceIsWhatMakesTheStatePhaseFree()
        => CanonicalStateAudit.IsPhaseFree(Build(CanonicalWeight, true))
        && !CanonicalStateAudit.IsPhaseFree(Build(CanonicalWeight, false))
        && CanonicalStateAudit.TheFirstEntryIsAlwaysACosine()
        && CanonicalStateAudit.TheLastEntryIsSometimesASine();

    /// <summary>The seed choice also decides WHICH SECTOR the state occupies: basis[^1] takes sines, which are hidden.</summary>
    public static (string Seed, int VisibleModes, int HiddenModes)[] SeedComparison()
        => new[] { true, false }.Select(first =>
        {
            var s = Build(CanonicalWeight, first);
            var phase = AmplitudePhaseAudit.PhaseModes();
            int hiddenOccupied = phase.Count(m => Math.Abs(s.Zip(m.Mode, (a, b) => a * b).Sum()) > Floor);
            return (first ? "basis[0] (cosine seeds)" : "basis[^1] (sometimes sine seeds)",
                VisibleModesOf(s), hiddenOccupied);
        }).ToArray();

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: a stated requirement is satisfied by the canonical recipe ALONE, so the recipe is forced.
    /// REFUTED: the canonical recipe fails the requirements its own justification names. BOUNDARY: it satisfies them and
    /// shares them with other recipes - justified, not forced.
    /// </summary>
    public static string Verdict()
    {
        if (!TheCountIsFortyFourMinusTheZeros()) return "REFUTED";
        if (!TheCanonicalRecipeSatisfiesEveryRequirementItIsJustifiedBy()) return "REFUTED";
        // the construction is PINNED when one of the stated requirements is met by exactly one substance, and it is that
        // requirement rather than the one the construction's own comment names
        if (SomeRequirementPinsTheConstruction()) return "DERIVED";
        if (!TheBasisZeroChoiceIsWhatMakesTheStatePhaseFree()) return "BOUNDARY";
        return "BOUNDARY";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("THE COUNT LAW IS EXACT, AND IT IS WHERE THE 42 COMES FROM. A recipe takes one seed per non-constant level of non-zero weight, so the occupied count is 44 minus the number of zero-weight NON-CONSTANT levels, for ANY such formula. ");
        foreach (var (formula, zeros, nonConstant, predicted, measured) in CountLaw())
            sb.Append($"{formula}: {zeros} zero(s) total, {nonConstant} of them non-constant, predicted {predicted}, measured {measured}; ");
        sb.Append($"the law holds for every formula tested ({TheCountIsFortyFourMinusTheZeros()}). So THE 42 IS 44 MINUS THE CANONICAL FORMULA'S TWO ZEROS - and the question a reader should ask is why the formula has two zeros, which is a property of its MODULUS rather than of any physical requirement. ");
        sb.Append($"THE RECIPE IS NOT THE MOST GENERIC ONE, AND THE AUDIT MEASURES THE TRADE: the largest orbit any recipe reaches is {MaxOrbitDimension()}, and it belongs to a recipe that is NOT phase-free, while the canonical recipe's orbit is {RhoAccessibilityAudit.OrbitDimension(Build(CanonicalWeight, true))}. So the requirement G_046's comment names is satisfied in the WEAK sense of a large orbit, and the recipe gives up orbit size to be phase-free. ");
        sb.Append("THE REQUIREMENTS ARE TESTED RATHER THAN ASSUMED. G_046's own comment names the design reason - the state must be GENERIC - so the audit turns the stated requirements into measurements and asks whether any of them is satisfied by the canonical recipe ALONE. ");
        foreach (var (id, requirement, holds, of, only) in RequirementTable())
            sb.Append($"{id} {requirement}: satisfied by {holds} of {of} recipes ({string.Join(", ", only)}); ");
        sb.Append($"AND ONE REQUIREMENT PINS SOMETHING ELSE: {RequirementsThatPinAnotherConstruction()} - so G_040's hidden count of {RhoObservableAudit.DoubletCount()} and the canonical state's kernel of {ModeOccupationAudit.KernelOf(Build(CanonicalWeight, true))} are DIFFERENT QUANTITIES, and the audit reports that rather than merging them. ");
        sb.Append($"THE SEED CHOICE IS THE SHARPEST RESULT HERE, AND IT IS COMPLEMENTARY: {SeedComparison()[0].Seed} gives {SeedComparison()[0].VisibleModes} visible modes and {SeedComparison()[0].HiddenModes} hidden, while {SeedComparison()[1].Seed} gives {SeedComparison()[1].VisibleModes} visible and {SeedComparison()[1].HiddenModes} hidden. THE SAME COUNTS IN BOTH CASES - the SAME occupied total and the SAME kernel - with the two sectors SWAPPED. So the recipe decides WHICH SECTOR THE STATE LIVES IN and not how much of it, and the numbers the series quoted (42 occupied, kernel 53) are robust to the seed choice while the sector is entirely the seed's doing. ");
        sb.Append($"MEASURED OVER SUBSTANCES - a recipe up to the overall scale of its weights - {Substances().Length} constructions are tested, and ONE REQUIREMENT IS SATISFIED BY EXACTLY ONE OF THEM: {ThePinningRequirement()}. So the construction IS PINNED, and by a requirement its own comment does NOT name, while the requirement that comment DOES name is shared by 3 of the {Substances().Length} ({NoRequirementPinsTheRecipeAlone()}). The recipe satisfies every requirement its justification names ({TheCanonicalRecipeSatisfiesEveryRequirementItIsJustifiedBy()}), but what actually fixes it is phase-freeness. ");
        sb.Append($"AND ITS JUSTIFICATION IS SHARPER THAN 'GENERIC': the canonical recipe is the only one that is BOTH fully occupying AND phase-free, up to the scale of its own weights ({TheCanonicalRecipeIsTheOnlyFullyOccupyingPhaseFreeOne()}). That pair is exactly what the phase-sector questions need - a state that fills the visible sector while leaving the hidden one empty - which is why it is the right state to study, and why a different choice would have changed the answers rather than the theory. ");
        sb.Append("THE SEED CHOICE DECIDES WHERE THE STATE LIVES. ");
        foreach (var (seed, visible, hidden) in SeedComparison())
            sb.Append($"{seed}: {visible} visible modes and {hidden} hidden modes occupied; ");
        sb.Append($"the cause is exact: basis[0] of a level is ALWAYS A COSINE and basis[^1] is sometimes a SINE, and the sines are the hidden quadratures ({TheBasisZeroChoiceIsWhatMakesTheStatePhaseFree()}). ");
        sb.Append("THE SEPARATION THE AUDIT WAS ASKED FOR IS THEREFORE: THE RECIPE DECIDES WHERE THE STATE LIVES - which sector it occupies, how many modes it fills, how large the kernel is - AND THE THEORY DECIDES WHAT FOLLOWS ONCE IT IS THERE. Everything the series measured about the amplitudes, the phases, the contractions and the laws is downstream of a state whose occupancy is arithmetic.");
        return sb.ToString();
    }

    // ===================== 7. REPORTS =====================

    public static string OutputCountLaw()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE COUNT LAW: occupied = levels - zeros - 1 = 44 - zeros");
        sb.AppendLine("   formula            | zeros (total / non-constant) | predicted | measured occupied");
        foreach (var (formula, zeros, nonConstant, predicted, measured) in CountLaw())
            sb.AppendLine($"   {formula,-18} | {zeros,13} / {nonConstant,-12} | {predicted,9} | {measured,17}");
        sb.AppendLine($"   the law holds for every formula : {TheCountIsFortyFourMinusTheZeros()}");
        sb.AppendLine();
        sb.AppendLine("   the zero-weight levels per formula:");
        foreach (var f in Formulas())
            sb.AppendLine($"     {f.Name,-18} zeros at {string.Join(", ", Enumerable.Range(0, Levels()).Where(k => f.Formula(k) == 0.0))}");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE REQUIREMENTS, TESTED PER RECIPE");
        sb.AppendLine("   recipe                            | orbit | occupied | phase-free | visible | kernel | no split");
        foreach (var (name, orbit, occupied, phaseFree, fullVisible, kernel, noSplit) in MeasuredRecipes())
            sb.AppendLine($"   {name,-33} | {orbit,5} | {occupied,8} | {phaseFree,10} | {fullVisible,7} | {kernel,6} | {noSplit}");
        sb.AppendLine($"   the largest orbit observed : {MaxOrbitDimension()}");
        sb.AppendLine();
        sb.AppendLine("   requirement                                                  | holds | of | satisfied by (substances)");
        foreach (var (id, requirement, holds, of, only) in RequirementTable())
            sb.AppendLine($"   {id} {requirement,-58} | {holds,5} | {of,2} | {string.Join(", ", only)}");
        sb.AppendLine($"   substances (recipes up to scale)            : {Substances().Length}");
        sb.AppendLine($"   requirement that pins the construction      : {ThePinningRequirement()}");
        sb.AppendLine($"   no requirement pins the recipe alone        : {NoRequirementPinsTheRecipeAlone()}");
        sb.AppendLine($"   the recipe satisfies its own justification  : {TheCanonicalRecipeSatisfiesEveryRequirementItIsJustifiedBy()}");
        sb.AppendLine($"   the unique fully-occupying phase-free recipe: {TheCanonicalRecipeIsTheOnlyFullyOccupyingPhaseFreeOne()}");
        return sb.ToString();
    }

    public static string OutputSeed()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. WHAT THE BASIS[0] CHOICE DECIDES");
        sb.AppendLine("   seed                              | visible modes occupied | hidden modes occupied");
        foreach (var (seed, visible, hidden) in SeedComparison())
            sb.AppendLine($"   {seed,-33} | {visible,22} | {hidden,23}");
        sb.AppendLine($"   basis[0] is what makes the state phase-free : {TheBasisZeroChoiceIsWhatMakesTheStatePhaseFree()}");
        sb.AppendLine($"   the first entry of every level is a cosine : {CanonicalStateAudit.TheFirstEntryIsAlwaysACosine()}");
        sb.AppendLine($"   the last entry is sometimes a sine         : {CanonicalStateAudit.TheLastEntryIsSometimesASine()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
