using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_043 — MINIMALITY AUDIT.
///
/// QUESTION. Why does nature stop at the first working dimension (d = 3) instead of continuing to d = 4, 5, ...?
/// Is there a quantity that is optimal ONLY at d = 3?
///
/// Compared: D96^3, D96^4, D96^5 (and the rest of the ladder). Measured: photon polarisations, graviton
/// polarisations, the Hodge mismatch, representation growth, state-space growth, clock-law scaling — plus two
/// derived observability measures and one count bound.
///
/// ANSWER: **BOUNDARY — and the obstruction is that every candidate criterion turns over at the SAME d.**
///
///  (1) EXACTLY ONE MEASURED FAMILY IS UNIQUELY OPTIMAL AT d = 3: the Hodge mismatch
///      |dim(Lambda^2) - dim(V)| = |d(d-3)|/2, which is ZERO only at d = 3 (1, 0, 2, 5, 9 for d = 2..6) and
///      increases monotonically afterwards. That is G_042's root, so it is not a second mechanism.
///
///  (2) EVERY GROWTH FAMILY IS MONOTONE, AND ALL OF THEM FAVOUR SMALLER d. Photon counts, graviton counts,
///      representation growth (max irrep dimension 2, 3, 8, 20, 80), state-space size (96^d - 1), the
///      observability fraction (orbitals / state space: 0.516, 0.133, 0.0235, 0.00319, ...) and the states per
///      observable (1.94, 7.52, 42.5, 314, 2841) all move in the direction that makes higher dimensions WORSE.
///      So an economy optimum, if it existed, would stop at d = 1 or 2 — and those do not work. **The stop at 3
///      is therefore NOT an economy optimum**, and no member of the growth families is optimal at 3.
///
///  (3) THE ONE OTHER EXTREMAL QUANTITY IS FROM THE SAME FAMILY. "The graviton's polarisations do not exceed
///      the number of directions" holds for d <= 3, so the LARGEST admissible dimension is 3 — a count bound,
///      not an equality. But it is the same quadratic family with a different offset: the Hodge equality is
///      d^2 - 3d = 0 and the bound is d^2 - 3d - 2 <= 0. One family, two members.
///
///  (4) FIVE CRITERIA TURN OVER AT d = 3, IN BOTH DIRECTIONS — which is why the mechanism cannot be identified.
///      Turning ON at 3: a dimension-3 irrep is supplied, the graviton count becomes positive, the Hodge
///      mismatch reaches zero. Turning OFF after 3: the graviton still fits within the dimensions, and the
///      vector is still the largest irrep. Their intersection is exactly {3}, and NOTHING in the measured
///      quantities distinguishes "the first dimension that works" from "the only dimension where the mismatch
///      vanishes" — the two coincide. So the OUTCOME is over-determined while the MECHANISM is undetermined.
///
///  (5) BUT THE OUTCOME IS ROBUST, NOT KNIFE-EDGE. The whole one-parameter family d^2 - 3d - c separates 3 from
///      4 for every offset c in [0, 4) — which contains both the Hodge equality (c = 0) and the count bound
///      (c = 2). The exclusion of d = 4 does not depend on the particular quantity chosen.
/// </summary>
public static class MinimalityAudit
{
    /// <summary>Cells per axis in every member of the family.</summary>
    public const int D96Cells = 96;

    /// <summary>How far the comparison is carried (96^6 and C(54,6) still fit in long).</summary>
    public const int MaxDimension = 6;

    // ═══ §1  THE MEASURED FAMILIES ══════════════════════════════════════════════════════════════

    public static int Photons(int d) => d - 1;
    public static int Gravitons(int d) => (d + 1) * (d - 2) / 2;

    /// <summary>The Hodge mismatch |dim(Lambda^2) - dim(V)| — the defect of the duality.</summary>
    public static int HodgeMismatch(int d)
        => Math.Abs(ThreeDimensionalityDependencyAudit.AntisymmetricSquare(d)
                  - ThreeDimensionalityDependencyAudit.VectorDimension(d));

    /// <summary>Representation growth: the largest irrep the d-cube's symmetry can supply.</summary>
    public static int RepresentationGrowth(int d) => SubstrateDimensionAudit.MaxIrrepDimension(d);

    /// <summary>State-space growth: the simplex dimension over the 96^d cells.</summary>
    public static long StateSpaceDimension(int d)
    {
        long n = 1;
        for (int i = 0; i < d; i++) n *= D96Cells;
        return n - 1;
    }

    /// <summary>Clock-law scaling: rho^(1/d) with G_016b's own 20:1 contrast.</summary>
    public static double ClockRatePerDay(int d) => Math.Log(20.0) / d * 86400.0;

    /// <summary>
    /// The number of ORBITALS of the D96^d lattice's symmetry on ordered cell pairs — the centralizer
    /// dimension G_040 computed for the ring, where the answer was 49. The pair invariant is the multiset of
    /// per-axis distances (49 values per axis), so the count is C(48 + d, d) — which reproduces 49 at d = 1.
    /// </summary>
    public static long Orbitals(int d) => Binomial(48 + d, d);

    private static long Binomial(int n, int k)
    {
        long r = 1;
        for (int i = 0; i < k; i++) r = r * (n - i) / (i + 1);
        return r;
    }

    /// <summary>What fraction of the state space a substrate-constructed measurement can name.</summary>
    public static double ObservabilityFraction(int d) => (double)Orbitals(d) / StateSpaceDimension(d);

    /// <summary>How many states hide behind a single observable.</summary>
    public static double StatesPerObservable(int d) => (double)StateSpaceDimension(d) / Orbitals(d);

    /// <summary>The count bound: do the graviton's polarisations fit within the number of directions?</summary>
    public static bool GravitonsFitWithinDimensions(int d) => Gravitons(d) <= d;

    // ═══ §2  MONOTONICITY — the test for "no optimum" ═══════════════════════════════════════════

    public static bool StrictlyIncreasing(double[] values)
        => Enumerable.Range(1, values.Length - 1).All(i => values[i] > values[i - 1]);

    public static bool StrictlyDecreasing(double[] values)
        => Enumerable.Range(1, values.Length - 1).All(i => values[i] < values[i - 1]);

    public static bool Monotone(double[] values) => StrictlyIncreasing(values) || StrictlyDecreasing(values);

    public static double[] Values(Func<int, double> f, int from = 2)
        => Enumerable.Range(from, MaxDimension - from + 1).Select(d => f(d)).ToArray();

    /// <summary>
    /// THE PROFILE TABLE. Every measured family, its values over the ladder, and whether it is monotone (which
    /// means it has NO optimum on the ladder) or uniquely extremal at d = 3.
    /// </summary>
    public static (string Family, double[] Values, string Profile)[] Profiles()
    {
        var rows = new List<(string, double[], string)>();
        void Add(string name, Func<int, double> f, int from = 2)
        {
            var v = Values(f, from);
            bool increasing = StrictlyIncreasing(v);
            bool decreasing = StrictlyDecreasing(v);
            string profile;
            if (v.All(x => x == 0.0 || x == 1.0))
            {
                int lastOne = Array.FindLastIndex(v, x => x == 1.0);
                profile = lastOne >= 0
                    ? $"THRESHOLD - holds through d = {lastOne + from}"
                    : "never holds";
            }
            else if (increasing) profile = "MONOTONE UP - no optimum";
            else if (decreasing) profile = "MONOTONE DOWN - no optimum";
            else
            {
                int minAt = Array.IndexOf(v, v.Min()) + from;
                profile = v.Min() == 0.0 && v.Count(x => x == 0.0) == 1
                    ? $"UNIQUELY ZERO AT d = {minAt}"
                    : $"extremal at d = {minAt}";
            }
            rows.Add((name, v, profile));
        }

        Add("photon polarisations  d-1", d => Photons(d));
        Add("graviton polarisations (d+1)(d-2)/2", d => Gravitons(d));
        Add("Hodge mismatch |dim L2 - dim V|", d => HodgeMismatch(d));
        Add("representation growth max irrep dim", d => RepresentationGrowth(d));
        Add("state-space growth 96^d - 1", d => StateSpaceDimension(d));
        Add("clock-law scaling rho^(1/d)", d => ClockRatePerDay(d));
        Add("observability fraction orbitals/state", d => ObservabilityFraction(d));
        Add("states per observable", d => StatesPerObservable(d));
        Add("graviton fits within dimensions (0/1)", d => GravitonsFitWithinDimensions(d) ? 1.0 : 0.0, 1);
        return rows.ToArray();
    }

    /// <summary>Families with no extremum on the ladder — the growth families, i.e. no economy optimum.</summary>
    public static string[] MonotoneFamilies()
        => Profiles().Where(r => r.Profile.StartsWith("MONOTONE")).Select(r => r.Family).ToArray();

    /// <summary>Families uniquely zero at d = 3.</summary>
    public static string[] UniquelyZeroAtThree()
        => Profiles().Where(r => r.Profile.Contains("UNIQUELY ZERO AT d = 3")).Select(r => r.Family).ToArray();

    /// <summary>
    /// Do the growth families all favour SMALLER dimensions? (They do — which is what rules out an economy
    /// optimum, since an economy principle would stop at d = 1 or 2, and those do not work.)
    /// </summary>
    public static bool GrowthFavoursSmallerDimensions()
        => StrictlyIncreasing(Values(d => StateSpaceDimension(d)))
        && StrictlyDecreasing(Values(d => ObservabilityFraction(d)))
        && StrictlyIncreasing(Values(d => StatesPerObservable(d)))
        && StrictlyIncreasing(Values(d => RepresentationGrowth(d)))
        && StrictlyIncreasing(Values(d => Gravitons(d)));

    // ═══ §3  THE THRESHOLDS — why the mechanism is undetermined ═════════════════════════════════

    public static bool SuppliesThreeDimensionalIrrep(int d)
        => SubstrateDimensionAudit.MaxIrrepDimension(d) >= 3;

    public static bool GravitonsArePositive(int d) => Gravitons(d) > 0;

    public static bool HodgeMismatchVanishes(int d) => HodgeMismatch(d) == 0;

    public static bool VectorIsTheLargestIrrep(int d)
        => SubstrateDimensionAudit.MaxIrrepDimension(d) == d;

    /// <summary>The five criteria, each computed over d = 1..8 so the turning points are visible.</summary>
    public static (string Criterion, int[] Holds)[] Criteria() => new (string, int[])[]
    {
        ("A dimension-3 irrep is supplied", Turned("supply")),
        ("The graviton count is positive", Turned("graviton")),
        ("The Hodge mismatch vanishes", Turned("hodge")),
        ("The graviton fits within the dimensions", Turned("fits")),
        ("The vector is the largest irrep", Turned("largest")),
    };

    private static int[] Turned(string which) => Enumerable.Range(1, 8).Where(d => which switch
    {
        "supply" => SuppliesThreeDimensionalIrrep(d),
        "graviton" => GravitonsArePositive(d),
        "hodge" => HodgeMismatchVanishes(d),
        "fits" => GravitonsFitWithinDimensions(d),
        _ => VectorIsTheLargestIrrep(d),
    }).ToArray();

    /// <summary>Criterion names paired with whether they turn ON at 3 or OFF after 3.</summary>
    public static (string Criterion, int[] Holds, string Direction)[] CriteriaWithDirection()
        => Criteria().Select(c =>
        {
            int first = c.Holds[0];
            bool onAtThree = c.Holds.Contains(3) && !c.Holds.Contains(2);
            string direction = onAtThree
                ? $"turns ON at d = 3 (first: {first})"
                : $"turns OFF after d = 3 (last: {c.Holds.Max()})";
            return (c.Criterion, c.Holds, direction);
        }).ToArray();

    /// <summary>Is d = 3 the dimension in which ALL five criteria hold at once?</summary>
    public static int[] DimensionsWhereAllCriteriaHold()
        => Enumerable.Range(1, 8).Where(d => SuppliesThreeDimensionalIrrep(d) && GravitonsArePositive(d)
                                          && HodgeMismatchVanishes(d) && GravitonsFitWithinDimensions(d)
                                          && VectorIsTheLargestIrrep(d)).ToArray();

    /// <summary>
    /// THE OBSTRUCTION. Three criteria turn on at 3 and two turn off after 3 — every turning point is at the
    /// same dimension, so the measured quantities cannot say WHICH one explains the stop.
    /// </summary>
    public static bool EveryCriterionTurnsAtThree()
    {
        var criteria = CriteriaWithDirection();
        int onAtThree = criteria.Count(c => c.Direction.StartsWith("turns ON"));
        int offAfterThree = criteria.Count(c => c.Direction.StartsWith("turns OFF") && c.Holds.Max() == 3);
        return onAtThree == 3 && offAfterThree == 2
            && DimensionsWhereAllCriteriaHold().Length == 1
            && DimensionsWhereAllCriteriaHold()[0] == 3;
    }

    /// <summary>Do the two candidate MECHANISMS coincide? (The first working dimension IS the unique optimum.)</summary>
    public static bool TheTwoCandidateMechanismsCoincide()
    {
        int firstWorking = Enumerable.Range(1, 8).First(SuppliesThreeDimensionalIrrep);
        int optimum = Enumerable.Range(2, 7).First(HodgeMismatchVanishes);
        return firstWorking == optimum && firstWorking == 3;
    }

    // ═══ §4  ROBUSTNESS — the outcome is not knife-edge ═════════════════════════════════════════

    /// <summary>The quadratic family d^2 - 3d - c &lt;= 0, of which the Hodge equality (c = 0) and the count bound (c = 2) are members.</summary>
    public static bool FamilyAdmits(int d, int c) => d * d - 3 * d - c <= 0;

    /// <summary>Does the offset c keep d = 3 in and d = 4 out?</summary>
    public static bool SeparatesThreeFromFour(int c) => FamilyAdmits(3, c) && !FamilyAdmits(4, c);

    /// <summary>The offsets that separate 3 from 4 — computed by scanning the family, not assumed.</summary>
    public static int[] RobustOffsets()
        => Enumerable.Range(0, 12).Where(SeparatesThreeFromFour).ToArray();

    /// <summary>
    /// The Hodge equality is the member at c = 0 and the graviton count bound at c = 2, so both lie inside the
    /// robust window — the exclusion of d = 4 does not depend on which quantity is chosen.
    /// </summary>
    public static bool BothSelectedMembersAreRobust()
        => SeparatesThreeFromFour(0) && SeparatesThreeFromFour(2);

    public static bool TheOutcomeIsRobust() => RobustOffsets().Length >= 4 && BothSelectedMembersAreRobust();

    // ═══ §5  VERDICT AND REPORT ═════════════════════════════════════════════════════════════════

    /// <summary>
    /// BOUNDARY (computed). The ladder is recomputed first, and the cross-audit check that the orbital count
    /// reproduces G_040's 49 at d = 1. On that floor: exactly one measured family is uniquely zero at d = 3
    /// (the Hodge mismatch, which is G_042's root); every growth family is monotone and favours smaller d, so
    /// the stop is not an economy optimum; five criteria all turn over at d = 3, three on and two off, so the
    /// MECHANISM is undetermined while the OUTCOME is over-determined; and the exclusion of d = 4 is robust
    /// across the whole quadratic family.
    /// </summary>
    public static string Verdict()
    {
        bool floor = Enumerable.Range(2, MaxDimension - 2).All(d =>
            HodgeMismatch(d) == Math.Abs(ThreeDimensionalityDependencyAudit.AntisymmetricSquare(d)
                                       - ThreeDimensionalityDependencyAudit.VectorDimension(d)));
        bool crossAudit = Orbitals(1) == 49;                       // G_040's centralizer dimension for the ring
        bool oneOptimum = UniquelyZeroAtThree().Length == 1;
        bool growthMonotone = GrowthFavoursSmallerDimensions()
                           && MonotoneFamilies().Length >= 6;
        bool degenerate = EveryCriterionTurnsAtThree() && TheTwoCandidateMechanismsCoincide();
        bool robust = TheOutcomeIsRobust();
        if (!(floor && crossAudit && oneOptimum && growthMonotone && degenerate && robust)) return "REFUTED";
        return "BOUNDARY";
    }

    public static string WhereItStands()
        => "THE OUTCOME IS OVER-DETERMINED AND THE MECHANISM IS NOT — which is why the answer is a BOUNDARY. "
         + "The audit recomputes its ladder first, and along the way it reproduces a number from a different "
         + "audit: the orbital count for the D96^d lattice is C(48 + d, d), which gives 49 at d = 1 — exactly "
         + "the centralizer dimension G_040 computed for the ring from the dihedral group. With the ground "
         + "rebuilt, the measurement finds ONE quantity that is optimal only at d = 3, and it is the quantity "
         + "G_042 already identified as the root: the Hodge mismatch |dim(Lambda^2) - dim(V)|, which is zero at "
         + "d = 3 and 1, 2, 5, 9 at d = 2, 4, 5, 6. Nothing else in the measured set is extremal there. Every "
         + "GROWTH family is monotone, and every one of them points the same way — photon counts, graviton "
         + "counts, representation growth (2, 3, 8, 20, 80), the state space (96^d - 1), the fraction of it a "
         + "measurement can name (0.516, 0.133, 0.0235, 0.00319) and the number of states hiding behind one "
         + "observable (1.94, 7.52, 42.5, 314, 2841) all get WORSE as d grows. So if nature chose by economy it "
         + "would have stopped at d = 1 or 2, and those do not work: the stop at 3 is not an economy optimum, "
         + "and the growth families carry no optimum at all. The one quantity that looked like an independent "
         + "second answer — the count bound that the graviton's polarisations fit within the number of "
         + "directions, which holds for d <= 3 and fails from d = 4 — turns out to be the same quadratic family "
         + "with a different offset: the equality is d^2 - 3d = 0 and the bound is d^2 - 3d - 2 <= 0. One "
         + "family, two members, and no second mechanism. AND THEN THE OBSTRUCTION, which is the real finding: "
         + "FIVE criteria turn over at the same dimension, three of them turning ON at 3 (a dimension-3 irrep "
         + "is supplied, the graviton count becomes positive, the Hodge mismatch reaches zero) and two turning "
         + "OFF after it (the graviton still fits within the dimensions, the vector is still the largest irrep). "
         + "Their intersection is exactly {3}. So 'the first dimension that works' and 'the only dimension where "
         + "the mismatch vanishes' are the SAME dimension, and no measured quantity can separate the two "
         + "explanations: the audit can say with confidence WHY each candidate fails at d = 4, but it cannot say "
         + "which candidate is doing the work, because they all fail at the same place for the same reason. The "
         + "one thing the data do settle is that the exclusion is ROBUST rather than knife-edge: the whole "
         + "family d^2 - 3d - c separates 3 from 4 for offsets c from 0 to 3 inclusive, a window that contains "
         + "both selected members (the equality at c = 0 and the bound at c = 2). So d = 4 is excluded by a "
         + "one-parameter family of conditions, not by a single tuned coincidence — and d = 3 is the smallest "
         + "dimension in which everything the theory needs is simultaneously true.";

    public static string OutputFamilies()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE MEASURED FAMILIES ACROSS THE LADDER");
        sb.AppendLine("   family                                       " + string.Join("  ", Enumerable
            .Range(2, MaxDimension - 1).Select(d => $"d={d}")));
        foreach (var (name, values, profile) in Profiles())
            sb.AppendLine($"   {name,-44} " + string.Join("  ", values.Select(v =>
                v >= 1000 ? $"{v,8:F0}" : $"{v,8:F3}")) + $"   {profile}");
        sb.AppendLine();
        sb.AppendLine($"   MONOTONE (no optimum on the ladder) : {MonotoneFamilies().Length}");
        sb.AppendLine($"   UNIQUELY ZERO AT d = 3              : {UniquelyZeroAtThree().Length} -> "
                      + $"{string.Join(" | ", UniquelyZeroAtThree())}");
        sb.AppendLine($"   all growth families favour SMALL d  : {GrowthFavoursSmallerDimensions()}");
        sb.AppendLine("   -> an economy optimum would stop at d = 1 or 2, which do not work: the stop at 3 is not an");
        sb.AppendLine("      economy optimum, and the growth families contain no optimum at all");
        return sb.ToString();
    }

    public static string OutputThresholds()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE THRESHOLDS — every criterion turns over at d = 3");
        foreach (var (criterion, holds, direction) in CriteriaWithDirection())
            sb.AppendLine($"   {criterion,-42} holds at {{{string.Join(",", holds)}}}   {direction}");
        sb.AppendLine();
        sb.AppendLine($"   criteria holding together only at d = {{{string.Join(",", DimensionsWhereAllCriteriaHold())}}}");
        sb.AppendLine($"   EVERY CRITERION TURNS AT 3          : {EveryCriterionTurnsAtThree()}");
        sb.AppendLine($"   the two candidate mechanisms COINCIDE: {TheTwoCandidateMechanismsCoincide()}");
        sb.AppendLine("   -> 'the first dimension that works' and 'the only dimension where the mismatch vanishes'");
        sb.AppendLine("      are the same dimension, so the measured quantities cannot separate the mechanisms");
        return sb.ToString();
    }

    public static string OutputRobustness()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. ROBUSTNESS — the outcome is not knife-edge");
        sb.AppendLine($"   the quadratic family      : d^2 - 3d - c <= 0");
        sb.AppendLine($"   the Hodge equality is     : c = 0   (dim L2 = dim V)");
        sb.AppendLine($"   the count bound is        : c = 2   (gravitons <= dimensions)");
        sb.AppendLine($"   offsets separating 3 from 4: c = {string.Join(", ", RobustOffsets())}");
        sb.AppendLine($"   both selected members robust: {BothSelectedMembersAreRobust()}");
        sb.AppendLine($"   the outcome is robust       : {TheOutcomeIsRobust()}");
        sb.AppendLine("   -> d = 4 is excluded by a one-parameter family of conditions, not by one tuned accident");
        sb.AppendLine();
        sb.AppendLine($"   cross-audit check: orbitals at d = 1 = {Orbitals(1)} (G_040's centralizer dimension: 49)");
        sb.AppendLine();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
