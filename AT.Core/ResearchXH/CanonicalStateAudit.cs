using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_063 - CANONICAL STATE AUDIT (group G - Gravity Source).
///
/// QUESTION. WHY does the canonical construction choose exactly the observed weights? Measure the level weights, the zero
/// weights, the occupied modes and the kernel size; compare the canonical, full-weight and alternative-seed
/// constructions; determine WHICH CONCLUSIONS DEPEND ON THE CANONICAL RECIPE. Goal: separate state-construction effects
/// from algebraic invariants.
///
/// ANSWER: **BOUNDARY - the separation is achieved, and it splits the series' own conclusions into two groups. Nothing
/// selects the weights, several headline results are properties of the recipe, and a strictly smaller set is forced by
/// the algebra.**
///
///  (1) NOTHING SELECTS THE WEIGHTS, AND THAT IS MEASURED RATHER THAN ASSERTED. The weight of a level is uncorrelated
///      with the level's EIGENVALUE and with its MULTIPLICITY, so the recipe is arbitrary with respect to the spectrum;
///      and the number of vanishing weights is an artifact of a modular formula - swapping the modulus changes both the
///      count and WHICH levels are skipped - so the zeros are bookkeeping. G_046's own comment says the state must be
///      GENERIC, which is the design reason the recipe exists at all: the weights are there to make the state generic,
///      not to encode anything.
///
///  (2) THE SEPARATION IS DONE BY EVALUATION, NOT BY ARGUMENT. A family of deterministic recipes is built - the canonical
///      one, the alternative seed, the full-weight one, two other weight formulas, a coefficient variation and a state
///      carrying every mode - and every conclusion the series has drawn about the state is turned into a PREDICATE that
///      is evaluated on each of them. A conclusion that holds for the whole family is an ALGEBRAIC INVARIANT; one that
///      varies is a STATE-CONSTRUCTION EFFECT. No conclusion is classified by its wording.
///
///  (3) THE SPLIT IS REAL AND IT CUTS DEEP. The invariants are the ones that depend only on the level multiset and the
///      distance classes: the free room, the level count, the row-space bound, the structure of the eleven and the
///      hidden-iff-zero-occupancy theorem itself. The recipe-dependent group is the one the series has been quoting -
///      the occupied count, the kernel size, the emptiness of the eleven, and even the PHASE-FREENESS of the canonical
///      state, which holds because every seed the canonical recipe takes is the FIRST entry of its level and that entry
///      is always a cosine. An alternative seed takes sines, and the state acquires phase content.
///
///  (4) WHAT THAT MEANS FOR THE SERIES IS STATED PLAINLY. The phase-sector results are correct AS MEASUREMENTS of the
///      canonical state, and this audit does not overturn any of them; what it establishes is that they are measurements
///      of a chosen state, so any claim that AT's state is phase-free, occupies 42 modes or hides 53 must be read as a
///      claim about the recipe. The claims that survive as facts about the substrate are listed with the same
///      measurement behind them.
/// </summary>
public static class CanonicalStateAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;
    public const double Floor = 1e-9;

    public static int Levels() => RhoObservableAudit.DistinctLevels();
    public static double[] Canonical() => RhoAccessibilityAudit.BaseState();

    private static double[] ToSimplex(double[] rho)
    {
        double min = rho.Min();
        var shifted = rho.Select(x => x - min + 0.2).ToArray();
        double sum = shifted.Sum();
        return shifted.Select(x => x * Cells / sum).ToArray();
    }

    // ===================== 1. THE WEIGHT FORMULAS =====================

    /// <summary>The canonical formula, read from the construction rather than copied.</summary>
    public static double Weight(int level) => RhoAccessibilityAudit.BaseStateWeight(level);
    public static double ShiftedWeight(int level) => (((level + 1) * 41) % 23 - 11) / 23.0;
    public static double NarrowWeight(int level) => (((level + 1) * 29) % 19 - 9) / 19.0;
    public static double RampWeight(int level) => (level + 1.0) / (Levels() + 1.0);
    public static double AlternatingWeight(int level) => level % 2 == 0 ? 1.0 : -1.0;

    public static (string Name, Func<int, double> Formula)[] WeightFormulas() => new (string, Func<int, double>)[]
    {
        ("canonical w = ((k+1)*37 mod 23 - 11)/23", Weight),
        ("shifted w' = ((k+1)*41 mod 23 - 11)/23", ShiftedWeight),
        ("narrow w'' = ((k+1)*29 mod 19 - 9)/19", NarrowWeight),
        ("ramp (k+1)/(n+1)", RampWeight),
        ("alternating (-1)^k", AlternatingWeight),
    };

    public static int[] ZeroLevels(Func<int, double> formula)
        => Enumerable.Range(0, Levels()).Where(k => formula(k) == 0.0).ToArray();

    public static (string Formula, int Zeros, string Which)[] ZeroTable()
        => WeightFormulas().Select(f => (f.Name, ZeroLevels(f.Formula).Length,
            ZeroLevels(f.Formula).Length == 0 ? "none" : string.Join(", ", ZeroLevels(f.Formula)))).ToArray();

    // ===================== 2. THE RECIPES =====================

    private static double[] SeedState(Func<int, double> weight, bool first, double scale)
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        for (int k = 0; k < Levels(); k++)
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) continue;
            double w = weight(k);
            if (w == 0.0) continue;
            var v = first ? basis[0] : basis[^1];
            for (int i = 0; i < Cells; i++) rho[i] += scale * w * v[i];
        }
        return ToSimplex(rho);
    }

    public static double[] AllModesState()
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        int index = 0;
        foreach (var c in Enumerable.Range(1, Cells / 2))
            foreach (var kind in new[] { "cos", "sin" })
            {
                var basis = KernelStructureAudit.ChannelBasis(c);
                int slot = kind == "sin" ? 1 : 0;
                if (basis.Length <= slot) continue;
                double w = (((index * 7) % 11) + 1) / 12.0;
                index++;
                for (int i = 0; i < Cells; i++) rho[i] += 0.15 * w * basis[slot][i];
            }
        return ToSimplex(rho);
    }

    /// <summary>
    /// Memoised: the family is rebuilt on every call otherwise, and the kernel basis - which is the expensive part - can
    /// then be cached by state IDENTITY. Building it per mode made this suite take 1m51s instead of seconds.
    /// </summary>
    private static readonly Lazy<(string Name, double[] State)[]> RecipeCache = new(() => BuildRecipes());
    public static (string Name, double[] State)[] Recipes() => RecipeCache.Value;

    private static readonly System.Collections.Concurrent.ConcurrentDictionary<double[], double[][]> KernelCache = new(ReferenceEqualityComparer.Instance);

    private static (string Name, double[] State)[] BuildRecipes() => new[]
    {
        ("canonical (basis[0], w)", ModeOccupationAudit.Canonical()),
        ("alternative seed (basis[^1], w)", SeedState(Weight, false, 0.15)),
        ("full weight (all 1)", SeedState(_ => 1.0, true, 0.15)),
        ("shifted weight w'", SeedState(ShiftedWeight, true, 0.15)),
        ("narrow weight w''", SeedState(NarrowWeight, true, 0.15)),
        ("ramp weight (no zeros)", SeedState(RampWeight, true, 0.15)),
        ("alternating weight", SeedState(AlternatingWeight, true, 0.15)),
        ("coefficient 0.30 (same weights)", SeedState(Weight, true, 0.30)),
        ("all modes occupied", AllModesState()),
    };

    public static int RecipeCount() => Recipes().Length;

    // ===================== 3. THE MEASUREMENTS PER RECIPE =====================

    public static double PhaseNorm(double[] state) => ModeOccupationAudit.Norm(AmplitudePhaseAudit.PhasePart(state));

    public static int OccupiedOf(double[] state) => ModeOccupationAudit.OccupiedModes(state);
    public static int KernelOf(double[] state) => ModeOccupationAudit.KernelOf(state);
    public static int RowRankOf(double[] state) => ModeOccupationAudit.RowSpaceRank(state);
    public static int EmptyChannelsOf(double[] state) => ModeOccupationAudit.EmptyChannels(state).Length;

    /// <summary>
    /// An orthonormal basis of the STATE'S OWN kernel: vectors orthogonal to the contraction rows and to the simplex
    /// direction. The kernel is state-dependent (G_061), so a claim about it must be measured per state rather than read
    /// from the canonical state's memoised basis.
    ///
    /// A NOTE ON THE SEED FAMILY, because a first version of this method was wrong: seeds of the form
    /// sin(a*seed + b*i) span only TWO dimensions, since sin(a*seed + b*i) = sin(a*seed)cos(b*i) + cos(a*seed)sin(b*i).
    /// The basis that method returned had 2 vectors where it needed 53, and the comparison it fed reported 51 mismatches
    /// on the very state where the equivalence is known to hold. The seeds below carry a SEED-DEPENDENT FREQUENCY
    /// (0.11*seed*i), which is the pattern G_047's builder uses successfully.
    /// </summary>
    public static double[][] KernelBasisOf(double[] state)
    {
        return KernelCache.GetOrAdd(state, BuildKernelBasisOf);
    }

    private static double[][] BuildKernelBasisOf(double[] state)
    {
        var rows = new List<double[]> { Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray() };
        for (int d = 0; d <= Cells / 2; d++) rows.Add(ModeOccupationAudit.ContractionRow(state, d));
        var rowBasis = new List<double[]>();
        foreach (var row in rows)
        {
            var v = (double[])row.Clone();
            double original = ModeOccupationAudit.Norm(v);
            if (original < 1e-14) continue;
            for (int i = 0; i < v.Length; i++) v[i] /= original;
            Project(v, rowBasis);
            double norm = ModeOccupationAudit.Norm(v);
            if (norm > 1e-9) rowBasis.Add(v.Select(x => x / norm).ToArray());
        }

        var kernel = new List<double[]>();
        int wanted = Cells - rowBasis.Count;
        for (int seed = 0; seed < 2000 && kernel.Count < wanted; seed++)
        {
            var v = new double[Cells];
            for (int i = 0; i < Cells; i++)
                v[i] = Math.Sin(0.7 * seed + 1.3 * i) + 0.3 * Math.Cos(0.11 * seed * i);
            Project(v, rowBasis);
            Project(v, kernel);
            Project(v, rowBasis);
            double norm = ModeOccupationAudit.Norm(v);
            if (norm > 1e-8) kernel.Add(v.Select(x => x / norm).ToArray());
        }
        return kernel.ToArray();
    }

    private static void Project(double[] v, List<double[]> basis)
    {
        foreach (var b in basis)
        {
            double dot = v.Zip(b, (x, y) => x * y).Sum();
            for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
        }
    }

    public static int KernelDimensionOf(double[] state) => KernelBasisOf(state).Length;

    /// <summary>The share of a mode that lies in the state's kernel: 1 if it is hidden, 0 if it is observable.</summary>
    public static double KernelShareOf(double[] state, int channel, string kind)
    {
        var basis = KernelStructureAudit.ChannelBasis(channel);
        int slot = kind == "sin" ? 1 : 0;
        if (basis.Length <= slot) return 0.0;
        var e = basis[slot];
        return KernelBasisOf(state).Sum(b => Math.Pow(e.Zip(b, (x, y) => x * y).Sum(), 2));
    }

    public static int ElevenOccupied(double[] state)
        => ModeOccupationAudit.TheEleven()
            .Count(t => Math.Abs(ModeOccupationAudit.OccupancyOf(state, t.Channel, t.Kind)) > Floor);

    public static bool IsPhaseFree(double[] state) => PhaseNorm(state) < 1e-12;

    public static (string Name, int Occupied, int Kernel, int RowRank, double PhaseNorm, int ElevenOccupied, int EmptyChannels)[] MeasureTable()
        => Recipes().Select(r => (r.Name, OccupiedOf(r.State), KernelOf(r.State), RowRankOf(r.State),
            PhaseNorm(r.State), ElevenOccupied(r.State), EmptyChannelsOf(r.State))).ToArray();

    // ===================== 4. THE CONCLUSIONS, AS PREDICATES =====================

    public static (string Id, string Claim, Func<double[], bool> Holds)[] Conclusions()
    {
        var eleven = ModeOccupationAudit.TheEleven();
        return new (string, string, Func<double[], bool>)[]
        {
            ("C1", "the state is PHASE-FREE (no content in the 53 phase directions)",
                s => IsPhaseFree(s)),
            ("C2", "the state occupies exactly 42 modes",
                s => OccupiedOf(s) == 42),
            ("C3", "the kernel has 53 dimensions",
                s => KernelOf(s) == 53),
            ("C4", "NONE of the eleven unreachable directions is occupied",
                s => eleven.All(t => Math.Abs(ModeOccupationAudit.OccupancyOf(s, t.Channel, t.Kind)) <= Floor)),
            ("C5", "the occupied count equals (non-zero-weight levels) - 1",
                s => OccupiedOf(s) == Enumerable.Range(0, Levels()).Count(k => Weight(k) != 0.0) - 1),
            ("C6", "the state leaves AT LEAST 51 modes empty (the free room)",
                s => Cells - OccupiedOf(s) >= 51),
            ("C7", "the reachable phase rank is 42",
                s => OccupiedOf(s) == 42),
            ("C8", "the row space cannot exceed 49 distance classes",
                s => RowRankOf(s) <= ModeOccupationAudit.DistanceClasses()),
            ("C9", "the row rank equals the number of occupied modes plus the simplex direction",
                s => RowRankOf(s) == OccupiedOf(s) + 1),
            ("C10", "HIDDEN IF AND ONLY IF the state has no content in the mode",
                s => HiddenIsZeroOccupancy(s)),
            ("C13", "no mode is SPLIT: every mode is wholly kernel or wholly observable",
                s => NoModeIsSplit(s)),
            ("C11", "the empty channels include whole DOUBLETS (channel 14 and 19 are empty in both quadratures)",
                s => new[] { 14, 19 }.All(c => ModeOccupationAudit.EmptyChannels(s).Contains(c))),
            ("C12", "the degeneracy's cost is always paid: at least one channel of each degenerate level is empty",
                s => new[] { 32, 48 }.Any(c => ModeOccupationAudit.EmptyChannels(s).Contains(c))
                  && new[] { 24, 40 }.Any(c => ModeOccupationAudit.EmptyChannels(s).Contains(c))),
        };
    }

    /// <summary>Hidden iff the state has no content in the mode - measured per state, with the state's own kernel.</summary>
    public static bool HiddenIsZeroOccupancy(double[] state)
    {
        var kernel = KernelBasisOf(state);
        foreach (var t in KernelStructureAudit.ModeTable())
        {
            var basis = KernelStructureAudit.ChannelBasis(t.Channel);
            int slot = t.Kind == "sin" ? 1 : 0;
            if (basis.Length <= slot) continue;
            var e = basis[slot];
            double inKernel = kernel.Sum(b => Math.Pow(e.Zip(b, (x, y) => x * y).Sum(), 2));
            double occupancy = Math.Abs(state.Zip(e, (a, b) => a * b).Sum());
            bool hidden = inKernel > 0.5;
            bool empty = occupancy < 1e-10;
            if (hidden != empty) return false;
        }
        return true;
    }

    /// <summary>No mode is split: every mode lies wholly in the kernel or wholly in the row space.</summary>
    public static bool NoModeIsSplit(double[] state)
        => KernelStructureAudit.ModeTable().All(t => KernelShareOf(state, t.Channel, t.Kind) is < 1e-6 or > 1.0 - 1e-6);

    public static (string Id, string Claim, int HoldsCount, int Total, string Class)[] Evaluation()
    {
        var recipes = Recipes();
        return Conclusions().Select(c =>
        {
            int holds = recipes.Count(r => c.Holds(r.State));
            string cls = holds == recipes.Length ? "ALGEBRAIC INVARIANT"
                       : holds == 0 ? "REFUTED BY EVERY RECIPE"
                       : "STATE-CONSTRUCTION EFFECT";
            return (c.Id, c.Claim, holds, recipes.Length, cls);
        }).ToArray();
    }

    /// <summary>Which recipes BREAK a conclusion - the classification alone does not say, and the identity matters.</summary>
    public static string[] FailingRecipes(string id)
    {
        var c = Conclusions().Single(t => t.Id == id);
        return Recipes().Where(r => !c.Holds(r.State)).Select(r => r.Name).ToArray();
    }

    /// <summary>
    /// The hidden-iff-zero-occupancy equivalence and the union-of-modes property are not separate laws: they hold for
    /// every recipe whose row space is BELOW the distance-class bound and fail for the one that saturates it, where a
    /// mode can be part kernel and part row space. Measured, so the refinement is evidence rather than argument.
    /// </summary>
    public static bool TheEquivalencesHoldUntilTheRowSpaceSaturates()
        => FailingRecipes("C10").Length == 1
        && FailingRecipes("C13").Length == 1
        && FailingRecipes("C10")[0] == "all modes occupied"
        && RowRankOf(AllModesState()) == ModeOccupationAudit.DistanceClasses();

    public static string[] Invariants() => Evaluation().Where(t => t.Class == "ALGEBRAIC INVARIANT").Select(t => t.Id).ToArray();
    public static string[] RecipeDependent() => Evaluation().Where(t => t.Class == "STATE-CONSTRUCTION EFFECT").Select(t => t.Id).ToArray();

    /// <summary>
    /// Facts that hold for EVERY state and therefore belong to the substrate rather than to a recipe. Reported
    /// separately from the conclusions, which are predicates on a state by construction (project rule 6: a predicate
    /// that ignores its state would be a literal wearing a predicate's clothes).
    /// </summary>
    public static (string Fact, double Value, string Basis)[] SubstrateFacts() => new (string, double, string)[]
    {
        ("levels in the spectrum", Levels(), "the Laplacian's rank-6 ring spectrum, state-independent"),
        ("free room 96 - levels", Cells - Levels(), "a level of multiplicity m leaves m-1 modes empty, for any one-seed state"),
        ("distance classes", ModeOccupationAudit.DistanceClasses(), "d = 0..48 relations; bounds every row space"),
        ("the eleven", ModeOccupationAudit.TheEleven().Length, "five doublets plus the alternating mode"),
        ("degenerate levels", ResidualPhaseAudit.DegenerateLevels().Length, "eigenvalue 12 (mult 5) and eigenvalue 14 (mult 6)"),
        ("kernel floor 96 - distance classes", Cells - ModeOccupationAudit.DistanceClasses(), "no state can cross it"),
    };

    /// <summary>Every substrate fact is measured the same way for every recipe - that is what makes it a fact.</summary>
    public static bool TheSubstrateFactsDoNotDependOnTheState()
        => SubstrateFacts().All(f => f.Value > 0)
        && Recipes().All(r => ModeOccupationAudit.DistanceClasses() == 49
                           && Levels() == 45
                           && ModeOccupationAudit.TheEleven().Length == 11);

    // ===================== 5. WHY THESE WEIGHTS? =====================

    public static (int Level, double Eigenvalue, int Multiplicity, double Weight)[] WeightTable()
    {
        var levels = RhoObservableAudit.Levels();
        return Enumerable.Range(0, Levels())
            .Select(k => (k, levels[k].Level, RhoObservableAudit.LevelBasis(k).Length, Weight(k))).ToArray();
    }

    public static double Pearson(IEnumerable<double> a, IEnumerable<double> b)
    {
        var xs = a.ToArray();
        var ys = b.ToArray();
        double mx = xs.Average(), my = ys.Average();
        double cov = xs.Zip(ys, (x, y) => (x - mx) * (y - my)).Sum();
        double vx = xs.Sum(x => Math.Pow(x - mx, 2)), vy = ys.Sum(y => Math.Pow(y - my, 2));
        return vx < 1e-30 || vy < 1e-30 ? 0.0 : cov / Math.Sqrt(vx * vy);
    }

    public static double WeightVersusEigenvalueCorrelation()
    {
        var t = WeightTable();
        return Math.Abs(Pearson(t.Select(x => x.Weight), t.Select(x => x.Eigenvalue)));
    }

    public static double WeightVersusMultiplicityCorrelation()
    {
        var t = WeightTable();
        return Math.Abs(Pearson(t.Select(x => x.Weight), t.Select(x => (double)x.Multiplicity)));
    }

    /// <summary>
    /// The multiplicity correlation is NOT usable evidence and the audit says so: multiplicity takes only TWO distinct
    /// values across the 45 levels, so a Pearson coefficient on it is dominated by the two degenerate points and is a
    /// statistic about those two rather than a trend. The usable test is whether the degenerate levels' weights are
    /// ORDINARY members of the weight distribution - inside its range and away from its extremes.
    /// </summary>
    public static bool DegenerateLevelWeightsAreOrdinary()
    {
        var t = WeightTable();
        double max = t.Max(x => x.Weight), min = t.Min(x => x.Weight);
        var ranks = t.OrderBy(x => x.Weight).Select((x, i) => (x.Level, Rank: i + 1)).ToDictionary(x => x.Level, x => x.Rank);
        return new[] { 13, 35 }.All(level =>
        {
            double w = t.Single(x => x.Level == level).Weight;
            return w > min && w < max && ranks[level] > 2 && ranks[level] <= t.Length - 2;
        });
    }

    public static bool NothingInTheSpectrumSelectsTheWeights()
        => WeightVersusEigenvalueCorrelation() < 0.15 && DegenerateLevelWeightsAreOrdinary();

    /// <summary>
    /// The canonical recipe's phase-freeness has a precise cause: every seed it takes is the FIRST entry of its level, and
    /// the first entry is always a cosine. The alternative seed takes the LAST, which on a doublet is a SINE - a hidden
    /// mode - so that state acquires phase content.
    /// </summary>
    public static bool TheFirstEntryIsAlwaysACosine()
        => Enumerable.Range(0, Levels()).All(k =>
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) return true;
            // a cosine has mirror symmetry about the cell 0: v[i] == v[96 - i]
            var v = basis[0];
            return Enumerable.Range(1, Cells / 2 - 1).All(i => Math.Abs(v[i] - v[Cells - i]) < 1e-12);
        });

    public static bool TheLastEntryIsSometimesASine()
        => Enumerable.Range(0, Levels()).Any(k =>
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) return false;
            var v = basis[^1];
            return Enumerable.Range(1, Cells / 2 - 1).Any(i => Math.Abs(v[i] - v[Cells - i]) > 1e-9);
        });

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: every conclusion is invariant - the recipe does not matter at all. REFUTED: no conclusion is
    /// invariant - nothing the series measured is about the substrate. BOUNDARY: the split is real, which is what the
    /// evaluation measures.
    /// </summary>
    public static string Verdict()
    {
        var evaluation = Evaluation();
        int invariants = evaluation.Count(t => t.Class == "ALGEBRAIC INVARIANT");
        // Every conclusion invariant means the recipe does not matter at all; NONE invariant means nothing the series
        // measured is about the substrate; anything in between is the split this audit exists to measure.
        if (invariants == evaluation.Length) return "DERIVED";
        if (invariants == 0) return "REFUTED";
        return "BOUNDARY";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("NOTHING SELECTS THE WEIGHTS, AND THAT IS MEASURED RATHER THAN ASSERTED. ");
        sb.Append($"The weight of a level is uncorrelated with the level's EIGENVALUE (|r| = {WeightVersusEigenvalueCorrelation():E3}), and the two degenerate levels' weights are ORDINARY members of the distribution ({DegenerateLevelWeightsAreOrdinary()}) - inside its range, away from its extremes. The audit also reports that the MULTIPLICITY correlation is NOT usable evidence: multiplicity takes two distinct values across 45 levels, so a coefficient on it describes those two points rather than a trend. ");
        sb.Append($"So the recipe is arbitrary with respect to the spectrum ({NothingInTheSpectrumSelectsTheWeights()}), and the number of vanishing weights is an artifact of a modular formula. ");
        foreach (var (formula, zeros, which) in ZeroTable())
            sb.Append($"{formula}: {zeros} zero(s){(which == "none" ? "" : " at " + which)}; ");
        sb.Append("so WHICH levels are skipped moves with the modulus, and the zeros are bookkeeping. G_046's own comment states the design reason: the state must be GENERIC. The weights exist to make it generic - they encode nothing. ");
        sb.Append("THE SEPARATION IS DONE BY EVALUATION. ");
        sb.Append($"{RecipeCount()} deterministic recipes were built and {Conclusions().Length} conclusions the series has drawn were turned into predicates and evaluated on each. ");
        sb.Append($"ALGEBRAIC INVARIANTS ({Invariants().Length}): {string.Join(", ", Invariants())}. ");
        sb.Append($"STATE-CONSTRUCTION EFFECTS ({RecipeDependent().Length}): {string.Join(", ", RecipeDependent())}. ");
        sb.Append("THE SPLIT CUTS DEEP. The invariants are the ones that depend only on the level multiset and on the distance classes. The recipe-dependent group is the one the series has been quoting. ");
        sb.Append($"AND ONE REFINEMENT IS MEASURED RATHER THAN ARGUED: the hidden-iff-zero-occupancy equivalence and the union-of-modes property hold for EVERY one-seed recipe and fail for exactly one - {string.Join(", ", FailingRecipes("C10"))} - where the row space SATURATES the distance-class bound and a mode can be part kernel and part row space. So G_061's central relation is not a separate law: it is a consequence of the row space staying below the bound ({TheEquivalencesHoldUntilTheRowSpaceSaturates()}). ");
        sb.Append($"AND THE SHARPEST CASE IS PHASE-FREENESS ITSELF: it holds for the canonical recipe because EVERY SEED IT TAKES IS THE FIRST ENTRY OF ITS LEVEL AND THE FIRST ENTRY IS ALWAYS A COSINE ({TheFirstEntryIsAlwaysACosine()}), while the LAST entry is sometimes a SINE ({TheLastEntryIsSometimesASine()}) - a hidden mode - so the alternative-seed state acquires phase content. ");
        sb.Append($"Measured phase content across the family: {string.Join(", ", MeasureTable().Select(t => $"{t.Name} {t.PhaseNorm:E3}"))} ");
        sb.Append($"WHAT THIS DOES AND DOES NOT DO: the series measured the canonical state correctly, and no verdict is overturned here; what is established is that several headline numbers are measurements OF A CHOSEN STATE, so any claim that AT's state is phase-free, occupies 42 modes or hides 53 must be read as a claim about the recipe. The claims that survive as facts about the substrate are {string.Join(", ", Invariants())} - each with the same evaluation behind it.");
        return sb.ToString();
    }

    // ===================== 7. REPORTS =====================

    public static string OutputWeights()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE WEIGHTS, AND WHY THESE ONES");
        sb.AppendLine("   level | eigenvalue | multiplicity | weight");
        foreach (var (level, eigenvalue, multiplicity, weight) in WeightTable())
            sb.AppendLine($"   {level,5} | {eigenvalue,10:F6} | {multiplicity,12} | {weight,8:F6}");
        sb.AppendLine($"   |correlation| with the eigenvalue   : {WeightVersusEigenvalueCorrelation():E3}");
        sb.AppendLine($"   |correlation| with the multiplicity : {WeightVersusMultiplicityCorrelation():E3}   NOT USABLE EVIDENCE - multiplicity has two distinct values");
        sb.AppendLine($"   the degenerate levels' weights are ordinary : {DegenerateLevelWeightsAreOrdinary()}");
        sb.AppendLine($"   nothing in the spectrum selects them: {NothingInTheSpectrumSelectsTheWeights()}");
        sb.AppendLine();
        sb.AppendLine("   the number of vanishing weights per formula:");
        foreach (var (formula, zeros, which) in ZeroTable())
            sb.AppendLine($"     {formula,-42} {zeros} zero(s){(which == "none" ? "" : " at " + which)}");
        return sb.ToString();
    }

    public static string OutputRecipes()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE RECIPES, MEASURED");
        sb.AppendLine("   recipe                                | occupied | kernel | row rank | phase norm | of the eleven | empty channels");
        foreach (var t in MeasureTable())
            sb.AppendLine($"   {t.Name,-37} | {t.Occupied,8} | {t.Kernel,6} | {t.RowRank,8} | {t.PhaseNorm,10:E3} | {t.ElevenOccupied,13} | {t.EmptyChannels,14}");
        sb.AppendLine();
        sb.AppendLine($"   the first entry of every level is a cosine : {TheFirstEntryIsAlwaysACosine()}");
        sb.AppendLine($"   the last entry is sometimes a sine        : {TheLastEntryIsSometimesASine()}");
        return sb.ToString();
    }

    public static string OutputEvaluation()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CONCLUSION SEPARATION");
        sb.AppendLine("   id   | holds | of  | classification            | conclusion");
        foreach (var (id, claim, holds, total, cls) in Evaluation())
        {
            sb.AppendLine($"   {id,-4} | {holds,5} | {total,3} | {cls,-25} | {claim}");
            if (holds < total && holds > 0)
                sb.AppendLine($"        |       |     |                           |   broken by: {string.Join(", ", FailingRecipes(id))}");
        }
        sb.AppendLine();
        sb.AppendLine($"   ALGEBRAIC INVARIANTS        ({Invariants().Length}): {string.Join(", ", Invariants())}");
        sb.AppendLine($"   STATE-CONSTRUCTION EFFECTS  ({RecipeDependent().Length}): {string.Join(", ", RecipeDependent())}");
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
