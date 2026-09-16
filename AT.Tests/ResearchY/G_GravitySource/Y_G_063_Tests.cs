using System.Globalization;
using System.Text;
using AT.Core.ResearchXH;
using static AT.Core.ResearchXH.CanonicalStateAudit;
using AT.Tests.Shared;

namespace AT.Tests.ResearchY.G_GravitySource;

/// <summary>
/// ResearchY-G_063 - Canonical State Audit (group G - Gravity Source).
///
/// QUESTION. Why does the canonical construction choose exactly the observed weights? Measure the level weights, the zero
/// weights, the occupied modes and the kernel size; compare the canonical, full-weight and alternative-seed
/// constructions; determine which conclusions depend on the canonical recipe. Goal: separate state-construction effects
/// from algebraic invariants.
///
/// ANSWER: **BOUNDARY - the separation is achieved and it splits the series\' own conclusions: ONE algebraic invariant
/// survives, TWELVE are state-construction effects, and not even the canonical state\'s phase-freeness is forced.**
/// </summary>
public class Y_G_063_Tests : ResearchTestBase
{
    public Y_G_063_Tests(ITestOutputHelper o) : base(o) { }

    [Fact]
    public void Y_G_063_NothingInTheSpectrumSelectsTheWeights()
    {
        // the weight is uncorrelated with the eigenvalue, and the degenerate levels\' weights are ordinary members
        Assert.InRange(WeightVersusEigenvalueCorrelation(), 0.0, 0.15);
        Assert.True(DegenerateLevelWeightsAreOrdinary());
        Assert.True(NothingInTheSpectrumSelectsTheWeights());

        // both degenerate levels sit inside the weight distribution and away from its extremes
        var table = WeightTable();
        Assert.Equal(45, table.Length);
        double min = table.Min(t => t.Weight), max = table.Max(t => t.Weight);
        foreach (int level in new[] { 13, 35 })
        {
            double w = table.Single(t => t.Level == level).Weight;
            Assert.InRange(w, min + 1e-12, max - 1e-12);
        }

        // and the ZEROS are an artifact of the modulus: other formulas skip other levels
        Assert.Equal(new[] { 8, 31 }, ZeroLevels(Weight));
        Assert.Equal(new[] { 6, 29 }, ZeroLevels(ShiftedWeight));
        Assert.Empty(ZeroLevels(RampWeight));
        Assert.Equal(5, ZeroTable().Length);
    }

    [Fact]
    public void Y_G_063_TheRecipeFamilyIsMeasured()
    {
        var table = MeasureTable();
        Assert.Equal(9, table.Length);

        // the canonical state and the coefficient variation are the only phase-free recipes
        Assert.Equal(2, table.Count(t => t.PhaseNorm < 1e-12));
        Assert.True(IsPhaseFree(Canonical()));

        // the recipes span 42, 44 and 95 occupied modes, and kernels 47, 51 and 53
        Assert.Equal(new[] { 42, 44, 95 }, table.Select(t => t.Occupied).Distinct().OrderBy(x => x));
        Assert.Equal(new[] { 47, 51, 53 }, table.Select(t => t.Kernel).Distinct().OrderBy(x => x));

        // the row rank tracks the occupancy, except where the distance-class bound binds
        Assert.All(table, t => Assert.InRange(t.RowRank, 1, 49));
        Assert.Equal(3, table.Select(t => t.RowRank).Distinct().Count());
    }

    [Fact]
    public void Y_G_063_TheFirstEntryIsAlwaysACosineAndTheLastIsSometimesASine()
    {
        // the canonical recipe\'s phase-freeness has a cause: every seed it takes is a cosine
        Assert.True(TheFirstEntryIsAlwaysACosine());
        Assert.True(TheLastEntryIsSometimesASine());

        // so the alternative seed acquires phase content with nothing else changed
        Assert.True(IsPhaseFree(Canonical()));
        Assert.False(IsPhaseFree(ModeOccupationAudit.AlternativeSeed()));
        Assert.True(PhaseNorm(ModeOccupationAudit.AlternativeSeed()) > 1.0);

        // and the COEFFICIENT does not matter: same weights, larger scale, still phase-free
        Assert.True(IsPhaseFree(Recipes().Single(r => r.Name.StartsWith("coefficient")).State));
    }

    [Fact]
    public void Y_G_063_TheSeparationIsOneInvariantAndTwelveEffects()
    {
        var evaluation = Evaluation();
        Assert.Equal(13, evaluation.Length);

        // exactly one conclusion survives the whole family
        Assert.Equal(new[] { "C8" }, Invariants());
        Assert.Equal(12, RecipeDependent().Length);
        Assert.DoesNotContain("REFUTED BY EVERY RECIPE", evaluation.Select(t => t.Class));

        // and the invariant is the algebraic bound, which is what the goal asked to isolate
        var bound = evaluation.Single(t => t.Id == "C8");
        Assert.Equal("ALGEBRAIC INVARIANT", bound.Class);
        Assert.Equal(9, bound.HoldsCount);

        Assert.Equal("BOUNDARY", Verdict());
    }

    [Fact]
    public void Y_G_063_PhaseFreenessIsARecipeEffect()
    {
        Assert.Equal(2, Evaluation().Single(t => t.Id == "C1").HoldsCount);
        Assert.Equal(2, Evaluation().Single(t => t.Id == "C4").HoldsCount);
        Assert.Equal(5, Evaluation().Single(t => t.Id == "C2").HoldsCount);
        Assert.Equal(5, Evaluation().Single(t => t.Id == "C3").HoldsCount);
        Assert.Equal(5, Evaluation().Single(t => t.Id == "C7").HoldsCount);

        // the assertions name the recipes that break them, so the classification is not a bare count
        Assert.Contains("full weight (all 1)", FailingRecipes("C2"));
        Assert.Contains("alternating weight", FailingRecipes("C2"));
        Assert.Contains("all modes occupied", FailingRecipes("C1"));
        Assert.Contains("all modes occupied", FailingRecipes("C10"));
    }

    [Fact]
    public void Y_G_063_TheEquivalencesHoldUntilTheRowSpaceSaturates()
    {
        // G_061\'s central relation is not a separate law: it fails exactly where the row space hits the bound
        Assert.True(TheEquivalencesHoldUntilTheRowSpaceSaturates());
        Assert.Single(FailingRecipes("C10"));
        Assert.Single(FailingRecipes("C13"));
        Assert.Equal("all modes occupied", FailingRecipes("C10")[0]);
        Assert.Equal(ModeOccupationAudit.DistanceClasses(), RowRankOf(AllModesState()));
        Assert.Equal(8, Evaluation().Single(t => t.Id == "C10").HoldsCount);
    }

    [Fact]
    public void Y_G_063_TheSubstrateFactsAreMeasuredSeparately()
    {
        // facts that hold for every state are reported apart from the state conclusions (rule 6)
        Assert.Equal(6, SubstrateFacts().Length);
        Assert.True(TheSubstrateFactsDoNotDependOnTheState());
        Assert.Contains(SubstrateFacts(), f => f.Fact.Contains("free room") && f.Value == 51);
        Assert.Contains(SubstrateFacts(), f => f.Fact.Contains("distance classes") && f.Value == 49);
        Assert.Contains(SubstrateFacts(), f => f.Fact.Contains("kernel floor") && f.Value == 47);
        Assert.Contains(SubstrateFacts(), f => f.Fact.Contains("levels") && f.Value == 45);

        // the per-state kernel agrees with the canonical state\'s memoised one on the canonical state
        Assert.Equal(KernelStructureAudit.KernelDimension(), KernelDimensionOf(Canonical()));
        Assert.Equal(53, KernelDimensionOf(Canonical()));
    }

    [Fact]
    public void Y_G_063_Run()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var sb = new StringBuilder();
        PrintHeader("Y_G_063 - Canonical State Audit: why exactly these weights, and which conclusions depend on them?");

        sb.AppendLine("QUESTION. Why does the canonical construction choose exactly the observed weights?");
        sb.AppendLine("GIVEN        G_062 - the occupancy is one seed per level of non-zero weight, and the eleven can be");
        sb.AppendLine("             populated by a state alone");
        sb.AppendLine("MEASURE      level weights, zero weights, occupied modes, kernel size");
        sb.AppendLine("COMPARE      canonical | full weight | alternative seeds | other formulas | coefficient | all modes");
        sb.AppendLine("DETERMINE    which conclusions depend on the canonical recipe");
        sb.AppendLine("GOAL         separate state-construction effects from algebraic invariants");
        sb.AppendLine();
        sb.AppendLine("ASSUMPTIONS");
        sb.AppendLine("  1. A conclusion is classified by EVALUATION over the whole recipe family, never by its wording: each is a");
        sb.AppendLine("     predicate on a state, so a predicate that ignores its state would be a literal in a predicate\'s");
        sb.AppendLine("     clothes and is not admitted (project rule 6).");
        sb.AppendLine("  2. The kernel is state-dependent (G_061), so every kernel claim is measured against the STATE\'S OWN kernel");
        sb.AppendLine("     basis rather than the canonical state\'s memoised one.");
        sb.AppendLine("  3. \"Without changing the theory\" is read strictly: only the state varies; the levels, the distance");
        sb.AppendLine("     classes, the contraction rows and the eleven are asserted unchanged for every recipe.");
        sb.AppendLine("  4. Facts that hold for every state are reported SEPARATELY as substrate facts, with the measurement behind");
        sb.AppendLine("     each, rather than being counted as conclusions.");
        sb.AppendLine();

        PrintHeader(OutputWeights());
        PrintHeader(OutputRecipes());
        PrintHeader(OutputEvaluation());
        PrintHeader(OutputVerdict());

        Output.WriteLine(sb.ToString());
    }
}
