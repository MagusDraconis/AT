using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_062 - MODE OCCUPATION AUDIT (group G - Gravity Source).
///
/// QUESTION. What DETERMINES which Fourier modes are occupied in the canonical state? Measure the occupied modes, the
/// empty modes, the state construction weights and the degeneracy structure. Test: can the empty 11 be POPULATED without
/// changing the theory? Goal: explain why the canonical state occupies 42 phase modes and leaves 11 empty.
///
/// ANSWER: **DERIVED - the occupancy is set by the CONSTRUCTION, not by any AT law, and the eleven CAN be populated by
/// changing only the state: no law, no operator and no coefficient has to move.**
///
///  (1) THE RULE, AND IT IS ARITHMETIC RATHER THAN PHYSICS. The canonical state takes ONE seed per level - always
///      basis[0] - and weights it by a deterministic factor. Its span is therefore the mean plus one mode per level of
///      NON-ZERO weight, so the occupied count is (levels with non-zero weight) - 1 and the whole number follows from two
///      integers: how many levels there are, and how many of the weights vanish. Measured, 45 levels and two vanishing
///      weights give 42 - and changing the weight formula to have NO zeros gives 44 without touching anything else. THE
///      NUMBER 42 IS A PROPERTY OF A CHOSEN FORMULA. The prompt's phrasing is corrected here as well: the canonical state
///      occupies 42 VISIBLE modes and leaves the whole 53-direction PHASE sector empty, and of those 53 the eleven are
///      the ones lying in channels it does not occupy at all.
///
///  (2) THE ELEVEN ARE POPULATED BY A STATE, WITH THE THEORY UNTOUCHED. A state carrying content in every channel empties
///      no channel, so every one of the eleven is occupied - measured - and the contractions, the laws and the coupling
///      are the same objects they were before. What this audit adds to G_061 is the bound on what a state can do: the row
///      space is spanned by the distance-class images A_d rho, so its rank is bounded by the number of distance classes,
///      and the kernel therefore cannot fall below a floor that no state can cross. Measured over five states, the
///      canonical 53 is NOT that floor: the floor is lower, so the canonical state's own construction leaves MORE hidden
///      than the algebra forces.
///
///  (3) WHAT SURVIVES IS THE ALGEBRA, NOT THE STATE. The eleven are coordinates: their emptiness is a choice, their
///      number is arithmetic, and the only thing the substrate contributes is the irreducible sector the centralizer's
///      size forces. That is the honest shape of the answer to the goal - 42 and 11 are where a particular deterministic
///      formula puts the boundary between what the state occupies and what it does not, and the invariant part of the
///      picture is smaller than the split suggests.
/// </summary>
public static class ModeOccupationAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;
    public const double Floor = 1e-9;

    public static int Levels() => RhoObservableAudit.DistinctLevels();
    public static int DistanceClasses() => Cells / 2 + 1;      // d = 0 .. 48

    // ===================== 1. THE STATES =====================

    private static double[] ToSimplex(double[] rho)
    {
        double min = rho.Min();
        var shifted = rho.Select(x => x - min + 0.2).ToArray();
        double sum = shifted.Sum();
        return shifted.Select(x => x * Cells / sum).ToArray();
    }

    /// <summary>G_046's canonical state: one seed per level, basis[0], weighted.</summary>
    public static double[] Canonical() => RhoAccessibilityAudit.BaseState();

    /// <summary>The same weights with the LAST basis vector of each level - G_061's alternative.</summary>
    public static double[] AlternativeSeed() => SeedState(k => RhoAccessibilityAudit.BaseStateWeight(k), false);

    /// <summary>The canonical seeds with EVERY weight non-zero: no level is skipped.</summary>
    public static double[] FullWeight() => SeedState(_ => 1.0, true);

    public static double[] FullWeightAlternativeSeed() => SeedState(_ => 1.0, false);

    /// <summary>
    /// A DIFFERENT weight formula whose zeros fall on OTHER levels. The canonical formula's two zeros are what empties
    /// channels 14 and 19 (G_061); moving the zeros must move the empty channels, which is the test of whether the
    /// emptiness is structure or bookkeeping.
    /// </summary>
    public static double ShiftedWeight(int level) => (((level + 1) * 41) % 23 - 11) / 23.0;
    public static int[] ShiftedZeroLevels() => Enumerable.Range(0, Levels()).Where(k => ShiftedWeight(k) == 0.0).ToArray();
    public static double[] ShiftedFormula() => SeedState(ShiftedWeight, true);

    private static double[] SeedState(Func<int, double> weightOf, bool first)
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        for (int k = 0; k < Levels(); k++)
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) continue;
            var v = first ? basis[0] : basis[^1];
            double weight = weightOf(k);
            if (weight == 0.0) continue;
            for (int i = 0; i < Cells; i++) rho[i] += 0.15 * weight * v[i];
        }
        return ToSimplex(rho);
    }

    /// <summary>
    /// A state carrying EVERY non-constant mode, so no channel is left empty at all. The weights must be NON-ZERO: a
    /// first version used ((index * 7) % 11 - 5) / 11, which is zero at index 7, 18, 29, ... - so the state quietly left
    /// eight modes empty and the audit reported that one of the eleven could not be occupied by any state, which was
    /// false and was a defect in the PROBE rather than a finding.
    /// </summary>
    public static double[] AllModes()
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        int index = 0;
        foreach (var c in Enumerable.Range(1, Cells / 2))
            foreach (var kind in new[] { "cos", "sin" })
            {
                var basis = KernelStructureAudit.ChannelBasis(c);
                int slot = kind == "sin" ? 1 : 0;
                if (basis.Length <= slot) continue;
                double weight = (((index * 7) % 11) + 1) / 12.0;
                index++;
                for (int i = 0; i < Cells; i++) rho[i] += 0.15 * weight * basis[slot][i];
            }
        return ToSimplex(rho);
    }

    public static (string Name, double[] State)[] States() => new[]
    {
        ("canonical (basis[0], weights w)", Canonical()),
        ("alternative seed (basis[^1], weights w)", AlternativeSeed()),
        ("full weight (all weights 1)", FullWeight()),
        ("shifted weight formula w'", ShiftedFormula()),
        ("full weight, alternative seed", FullWeightAlternativeSeed()),
        ("all modes occupied", AllModes()),
    };

    // ===================== 2. OCCUPANCY =====================

    public static double OccupancyOf(double[] state, int channel, string kind)
    {
        var basis = KernelStructureAudit.ChannelBasis(channel);
        int slot = kind == "sin" ? 1 : 0;
        return basis.Length <= slot ? 0.0 : state.Zip(basis[slot], (a, b) => a * b).Sum();
    }

    public static (int Channel, string Kind, double Occupancy)[] ModeOccupancy(double[] state)
        => KernelStructureAudit.ModeTable().Select(t => (t.Channel, t.Kind, OccupancyOf(state, t.Channel, t.Kind))).ToArray();

    public static int OccupiedModes(double[] state) => ModeOccupancy(state).Count(t => Math.Abs(t.Occupancy) > Floor);
    public static int EmptyModes(double[] state) => ModeOccupancy(state).Count(t => Math.Abs(t.Occupancy) <= Floor);

    /// <summary>The five empty doublets plus the alternating channel, if they are empty for this state.</summary>
    public static int[] EmptyChannels(double[] state)
        => ModeOccupancy(state).GroupBy(t => t.Channel)
            .Where(g => g.All(t => Math.Abs(t.Occupancy) <= Floor))
            .Select(g => g.Key).OrderBy(c => c).ToArray();

    public static int UnreachableDirections(double[] state)
        => EmptyChannels(state).Sum(c => KernelStructureAudit.ChannelBasis(c).Length);

    /// <summary>G_060's eleven, as the CANONICAL state sees them.</summary>
    public static (int Channel, string Kind)[] TheEleven()
        => ResidualPhaseAudit.UnreachableDirections().Select(t => (t.Channel, t.Kind)).ToArray();

    public static bool OccupiesTheEleven(double[] state)
        => TheEleven().All(t => Math.Abs(OccupancyOf(state, t.Channel, t.Kind)) > Floor);

    // ===================== 3. THE RULE =====================

    public static int[] NonZeroWeightLevels()
        => Enumerable.Range(0, Levels()).Where(k => RhoAccessibilityAudit.BaseStateWeight(k) != 0.0).ToArray();

    /// <summary>The occupied count the construction alone predicts: one mode per level of non-zero weight, minus the mean.</summary>
    public static int OccupiedModesFromTheRule() => NonZeroWeightLevels().Length - 1;

    public static bool TheRulePredictsTheCanonicalCount()
        => OccupiedModesFromTheRule() == OccupiedModes(Canonical())
        && NonZeroWeightLevels().Length == 43
        && OccupiedModes(FullWeight()) == Levels() - 1;

    public static string WhyFortyTwo()
        => $"{Levels()} levels, {Levels() - NonZeroWeightLevels().Length} with a vanishing weight, so "
         + $"{NonZeroWeightLevels().Length} seeds of which one is the mean: {OccupiedModesFromTheRule()} occupied modes";

    // ===================== 4. THE ALGEBRAIC FLOOR =====================

    /// <summary>The distance-d relation applied to a state: A_d rho, the contraction row.</summary>
    public static double[] ContractionRow(double[] state, int d)
    {
        var row = new double[Cells];
        for (int i = 0; i < Cells; i++)
        {
            if (d == 0) row[i] = state[i];
            else if (d == Cells / 2) row[i] = state[(i + d) % Cells];
            else row[i] = state[(i + d) % Cells] + state[(i - d + Cells) % Cells];
        }
        return row;
    }

    public static int RowSpaceRank(double[] state)
    {
        var rows = new List<double[]> { Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray() };
        for (int d = 0; d <= Cells / 2; d++) rows.Add(ContractionRow(state, d));
        return Rank(rows);
    }

    public static int KernelOf(double[] state) => Cells - RowSpaceRank(state);

    public static (string Name, int Occupied, int Empty, int Rows, int Kernel, bool ElevenOccupied, int EmptyChannels)[] Table()
        => States().Select(s => (s.Name, OccupiedModes(s.State), EmptyModes(s.State), RowSpaceRank(s.State),
            KernelOf(s.State), OccupiesTheEleven(s.State), EmptyChannels(s.State).Length)).ToArray();

    public static int MinimumKernel() => Table().Min(t => t.Kernel);
    public static string StateWithTheSmallestKernel()
        => Table().OrderBy(t => t.Kernel).First().Name;

    /// <summary>The floor the ALGEBRA forces: the row space is spanned by one row per distance class, so its rank cannot exceed their number.</summary>
    public static bool TheRowRankCannotExceedTheDistanceClasses()
        => Table().All(t => t.Rows <= DistanceClasses());

    public static bool TheKernelFloorIsTheDistanceClassBound()
        => MinimumKernel() == Cells - DistanceClasses();

    public static bool TheKernelFallsTowardsTheFloorAndTheCanonicalStateIsAboveIt()
        => MinimumKernel() < KernelOf(Canonical());

    public static bool EveryOneOfTheElevenIsOccupiedBySomeState()
        => TheEleven().All(t => States().Any(s => Math.Abs(OccupancyOf(s.State, t.Channel, t.Kind)) > Floor));

    // ===================== 5. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: the occupancy follows from the construction (rule and weights) AND every one of the eleven is
    /// occupied by some state with no change to any AT object. BOUNDARY: some of the eleven cannot be occupied by any
    /// state of this family. REFUTED: the occupancy is not explained by the construction.
    /// </summary>
    public static string Verdict()
    {
        if (!TheRulePredictsTheCanonicalCount()) return "REFUTED";
        if (!EveryOneOfTheElevenIsOccupiedBySomeState()) return "BOUNDARY";
        if (!TheRowRankCannotExceedTheDistanceClasses()) return "REFUTED";
        return "DERIVED";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("THE OCCUPANCY IS SET BY THE CONSTRUCTION, AND THE RULE IS ARITHMETIC. The canonical state takes ONE seed per level - always basis[0] - and weights it, so its span is the mean plus one mode per level of NON-ZERO weight: ");
        sb.Append($"{WhyFortyTwo()}. ");
        sb.Append($"Changing nothing but the weight formula removes the zeros and the count becomes {OccupiedModes(FullWeight())}, so the number 42 is a property of a CHOSEN FORMULA rather than of the substrate (rule confirms it: {TheRulePredictsTheCanonicalCount()}). ");
        sb.Append("THE PROMPT'S PHRASING IS CORRECTED HERE. The canonical state occupies 42 VISIBLE modes and leaves the whole 53-direction PHASE sector empty; of those 53, the eleven are the ones lying in channels it does not occupy at all. ");
        sb.Append("THE ELEVEN ARE POPULATED BY A STATE, WITH THE THEORY UNTOUCHED. ");
        foreach (var t in Table())
            sb.Append($"{t.Name}: occupied {t.Occupied}, empty {t.Empty}, empty channels {t.EmptyChannels}, row rank {t.Rows}, kernel {t.Kernel}, occupies the eleven {t.ElevenOccupied}. ");
        sb.Append($"A state carrying content in every channel empties NO channel and occupies every one of the eleven ({EveryOneOfTheElevenIsOccupiedBySomeState()}) - and no law, operator or coefficient moves to make that happen. ");
        sb.Append($"WHAT SURVIVES IS THE ALGEBRA. The row space is spanned by the distance-class images A_d rho plus the simplex direction, so its rank cannot exceed the {DistanceClasses()} distance classes ({TheRowRankCannotExceedTheDistanceClasses()}): measured, the smallest kernel any of these states reaches is {MinimumKernel()} ({StateWithTheSmallestKernel()}), against the canonical {KernelOf(Canonical())}. So the canonical state leaves MORE hidden than the algebra forces, and the excess is its own construction's doing. ");
        sb.Append($"WHERE THE SPLIT REALLY COMES FROM: 42 and 11 are where a deterministic formula puts the boundary between what the state occupies and what it does not. The invariant part of the picture is smaller than the split suggests - the eleven are coordinates, and only the floor is structure.");
        return sb.ToString();
    }

    // ===================== 6. REPORTS =====================

    public static string OutputOccupancy()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. OCCUPIED AND EMPTY MODES");
        sb.AppendLine("   state                                  | occupied | empty | empty channels | occupies the eleven");
        foreach (var t in Table())
            sb.AppendLine($"   {t.Name,-38} | {t.Occupied,8} | {t.Empty,5} | {t.EmptyChannels,14} | {t.ElevenOccupied,18}");
        sb.AppendLine();
        sb.AppendLine("   the eleven (G_060/G_061): " + string.Join(", ", TheEleven().Select(t => $"{t.Kind}_{t.Channel}")));
        sb.AppendLine("   occupied per state, restricted to the eleven:");
        foreach (var s in States())
            sb.AppendLine($"     {s.Name,-38} {TheEleven().Count(t => Math.Abs(OccupancyOf(s.State, t.Channel, t.Kind)) > Floor),2} of 11");
        return sb.ToString();
    }

    public static string OutputRule()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE CONSTRUCTION RULE AND THE WEIGHTS");
        sb.AppendLine($"   levels = {Levels()}; levels with a non-zero weight = {NonZeroWeightLevels().Length}; rule predicts {OccupiedModesFromTheRule()} occupied modes; measured {OccupiedModes(Canonical())}");
        sb.AppendLine($"   the rule is confirmed : {TheRulePredictsTheCanonicalCount()}");
        sb.AppendLine($"   with every weight non-zero the count becomes {OccupiedModes(FullWeight())} - nothing else changes");
        sb.AppendLine("   degenerate levels:");
        foreach (var (index, level, mult, channels) in ResidualPhaseAudit.DegenerateLevels())
            sb.AppendLine($"     index {index,3} eigenvalue {level,9:F6} multiplicity {mult} channels {string.Join(", ", channels)}");
        sb.AppendLine("   levels with a vanishing weight: " + string.Join(", ", Enumerable.Range(0, Levels()).Where(k => RhoAccessibilityAudit.BaseStateWeight(k) == 0.0)));
        sb.AppendLine("   the SHIFTED formula w' vanishes on levels: " + string.Join(", ", ShiftedZeroLevels()) + " -> empty channels " + string.Join(", ", EmptyChannels(ShiftedFormula())));
        sb.AppendLine("   so the empty channels FOLLOW THE FORMULA'S ZEROS, and channel 48 is empty in both because its level is degenerate");
        return sb.ToString();
    }

    public static string OutputFloor()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE ALGEBRAIC FLOOR");
        sb.AppendLine("   state                                  | row rank | kernel | distance classes");
        foreach (var t in Table())
            sb.AppendLine($"   {t.Name,-38} | {t.Rows,8} | {t.Kernel,6} | {DistanceClasses(),16}");
        sb.AppendLine($"   the row rank cannot exceed the distance classes : {TheRowRankCannotExceedTheDistanceClasses()}");
        sb.AppendLine($"   the smallest kernel reached : {MinimumKernel()} ({StateWithTheSmallestKernel()}); the canonical state has {KernelOf(Canonical())}");
        sb.AppendLine($"   the canonical state is ABOVE the floor : {TheKernelFallsTowardsTheFloorAndTheCanonicalStateIsAboveIt()}");
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

    // ===================== 7. RANK =====================

    private static int Rank(IEnumerable<double[]> vectors)
    {
        var basis = new List<double[]>();
        foreach (var v0 in vectors)
        {
            var v = (double[])v0.Clone();
            double original = Norm(v);
            if (original < Floor) continue;
            for (int i = 0; i < v.Length; i++) v[i] /= original;
            for (int pass = 0; pass < 3; pass++)
                foreach (var b in basis)
                {
                    double dot = v.Zip(b, (x, y) => x * y).Sum();
                    for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
                }
            double norm = Norm(v);
            if (norm > 1e-9) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));
}
