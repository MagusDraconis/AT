using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.CanonicalRecipeAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_064 - Canonical Recipe Origin Audit (group G - Gravity Source).
///
/// QUESTION. Why does the canonical state use these specific level weights? Test the basis[0] choice, the level-weight
/// formula and the zero-weight levels; measure which conclusions change when the recipe changes. Goal: separate the
/// canonical recipe from theory content.
///
/// ANSWER: **DERIVED - the construction is PINNED: it is the unique substance in the family that is phase-free, and its
/// headline counts follow arithmetically from its zero count (occupied = 44 - zero-weight non-constant levels). Its
/// magnitudes are irrelevant, its named justification (genericity) does not pin it, and the seed choice decides WHICH
/// SECTOR the state occupies rather than how much of it.**
/// </summary>
public class Y_G_064_Tests : ResearchTestBase
{
    public Y_G_064_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_064_TheCountLawIsExact()
    {
        var law = CountLaw();
        Assert.Equal(5, law.Length);
        Assert.True(TheCountIsFortyFourMinusTheZeros());
        Assert.All(law, t => Assert.Equal(t.Predicted, t.Measured));

        // the five formulas span zero counts 0, 3, 2, 6 and 8 (non-constant), giving 44, 41, 42, 38 and 36
        Assert.Equal(new[] { 44, 42, 41, 38, 36 }, law.Select(t => t.Measured).OrderByDescending(x => x).ToArray());
        Assert.Equal(0, law.Single(t => t.Formula == "ramp (no modulus)").ZerosTotal);
        Assert.Equal(2, law.Single(t => t.Formula == "canonical mod-23").ZerosTotal);

        // and the constant level is special: zeroing it does not remove an occupied MODE
        var mod15 = law.Single(t => t.Formula == "mod-15");
        Assert.NotEqual(mod15.ZerosTotal, mod15.ZerosNonConstant);
        Assert.Equal(mod15.Predicted, mod15.Measured);
    }

    [Fact]
    public void Y_G_064_TheSeedChoiceSwapsTheTwoSectors()
    {
        var seeds = SeedComparison();
        Assert.Equal(2, seeds.Length);

        Assert.Contains(seeds, t => t.Seed.StartsWith("basis[0]") && t.VisibleModes == 42 && t.HiddenModes == 0);
        Assert.Contains(seeds, t => t.Seed.StartsWith("basis[^1]") && t.VisibleModes == 0 && t.HiddenModes == 42);

        // the SAME totals in both cases, with the sectors swapped: 42 occupied and the same kernel
        var first = Build(CanonicalWeight, true);
        var last = Build(CanonicalWeight, false);
        Assert.Equal(ModeOccupationAudit.OccupiedModes(first), ModeOccupationAudit.OccupiedModes(last));
        Assert.Equal(42, ModeOccupationAudit.OccupiedModes(first));
        Assert.Equal(ModeOccupationAudit.KernelOf(first), ModeOccupationAudit.KernelOf(last));
        Assert.Equal(53, ModeOccupationAudit.KernelOf(first));

        Assert.True(TheBasisZeroChoiceIsWhatMakesTheStatePhaseFree());
        Assert.True(CanonicalStateAudit.TheFirstEntryIsAlwaysACosine());
        Assert.True(CanonicalStateAudit.TheLastEntryIsSometimesASine());
    }

    [Fact]
    public void Y_G_064_TheConstructionIsPinnedByPhaseFreeness()
    {
        var table = RequirementTable();
        Assert.Equal(5, table.Length);
        Assert.Equal(8, Substances().Length);

        // exactly one requirement is met by exactly one substance, and that substance is the canonical construction
        Assert.Equal(new[] { "R3" }, RequirementsThatPinTheCanonicalConstruction());
        Assert.True(SomeRequirementPinsTheConstruction());
        Assert.Equal(1, table.Single(t => t.Id == "R3").Holds);
        Assert.Equal(CanonicalRecipeName, table.Single(t => t.Id == "R3").OnlySatisfiedBy[0]);

        // the named justification is SHARED, so it does not pin anything - and that is why the pinning one is R3
        Assert.Equal(4, table.Single(t => t.Id == "R1").Holds);
        Assert.True(table.Single(t => t.Id == "R1").Holds > 1);
        Assert.False(NoRequirementPinsTheRecipeAlone());

        Assert.True(TheCanonicalRecipeSatisfiesEveryRequirementItIsJustifiedBy());
        Assert.True(TheCanonicalRecipeIsTheOnlyFullyOccupyingPhaseFreeOne());
        Assert.Equal("DERIVED", Verdict());
    }

    [Fact]
    public void Y_G_064_TheNamedJustificationIsNotWhatPinsItAndTheOrbitIsTraded()
    {
        // the canonical recipe's orbit is large but NOT maximal: the largest belongs to a state that is not phase-free
        int canonicalOrbit = RhoAccessibilityAudit.OrbitDimension(Build(CanonicalWeight, true));
        Assert.Equal(84, canonicalOrbit);
        Assert.True(MaxOrbitDimension() > canonicalOrbit);
        Assert.Equal(95, MaxOrbitDimension());

        // and one requirement pins a DIFFERENT construction: G_040's 47 is not the canonical kernel of 53
        Assert.Contains("R4", RequirementsThatPinAnotherConstruction());
        Assert.Contains("every mode occupied", RequirementsThatPinAnotherConstruction());
        Assert.NotEqual(RhoObservableAudit.DoubletCount(), ModeOccupationAudit.KernelOf(Build(CanonicalWeight, true)));
    }

    [Fact]
    public void Y_G_064_EveryRequirementIsMeasuredOnEveryRecipe()
    {
        var measured = MeasuredRecipes();
        Assert.Equal(Recipes().Length, measured.Length);
        Assert.All(measured, t => Assert.InRange(t.Orbit, 1, 95));
        Assert.All(measured, t => Assert.InRange(t.Kernel, 47, 95));
        // no mode is split in any one-seed recipe; the all-modes state is the known exception (G_063)
        Assert.All(measured.Where(t => !t.Name.StartsWith("every mode")), t => Assert.True(t.NoSplit));
        Assert.False(measured.Single(t => t.Name.StartsWith("every mode")).NoSplit);

        // the recipes occupy 36, 38, 41, 42 and 44 modes - the count law's arithmetic, on states
        Assert.Equal(new[] { 36, 38, 41, 42, 44 }, measured.Select(t => t.Occupied).Distinct().OrderBy(x => x).Take(5).ToArray());

        // only the canonical construction (and its scale variant) is phase-free
        Assert.Equal(2, measured.Count(t => t.PhaseFree));
    }

    [Fact]
    public void Y_G_064_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_064 - Canonical Recipe Origin Audit: why these weights, and what the recipe decides");

        sb.AppendLine("QUESTION. Why does the canonical state use these specific level weights?");
        sb.AppendLine("GIVEN        G_062 (one seed per level of non-zero weight) and G_063 (one invariant out of thirteen)");
        sb.AppendLine("TEST         the basis[0] choice, the level-weight formula, the zero-weight levels");
        sb.AppendLine("MEASURE      which conclusions change when the recipe changes");
        sb.AppendLine("GOAL         separate the canonical recipe from theory content");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A SUBSTANCE is a recipe up to the overall SCALE of its weights: the canonical recipe at twice the");
        sb.AppendLine("     amplitude is the same construction, so counting recipe names would understate what a requirement pins.");
        sb.AppendLine("  2. Requirements are the ones the construction\'s own justification names plus the properties the");
        sb.AppendLine("     phase-sector programme needed; each is a measurement on a state rather than a comment.");
        sb.AppendLine("  3. The count law is stated over the 44 NON-CONSTANT levels: the constant level carries the simplex");
        sb.AppendLine("     direction, which is not a mode, so zeroing it does not remove an occupied mode.");
        sb.AppendLine("  4. Deterministic throughout; every state is built from the level bases by the same seed policy.");
        sb.AppendLine();

        PrintHeader(OutputCountLaw());
        PrintHeader(OutputRequirements());
        PrintHeader(OutputSeed());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
