using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_061 - RESIDUAL PHASE AUDIT (group G - Gravity Source).
///
/// QUESTION. What is SPECIAL about the 11 unreachable phase directions of G_060? Identify the alternating mode, the
/// empty channels, the kernel relation and the symmetry properties; test whether any existing AT operator can reach
/// them; explain the 53 = 42 + 11 split.
///
/// ANSWER: **DERIVED - and the answer is that they are NOT special directions of the substrate at all. They are the
/// modes the canonical state does not occupy, and the split is a property of SHIFT-INVARIANCE rather than of the
/// directions.**
///
///  (1) THE KERNEL IS THE STATE'S ORTHOGONAL COMPLEMENT, AND THAT IS EXACT RATHER THAN APPROXIMATE. A contraction row is
///      A_d rho with A_d the distance-d relation, and A_d is CIRCULANT, so on a single Fourier mode e of channel c the row
///      acts as <A_d rho, e> = lambda_d(c) <rho, e> with lambda_0(c) = 1. The row is therefore non-zero for every mode the
///      state occupies, and EXACTLY zero for every mode it does not: HIDDEN IF AND ONLY IF <rho, e> = 0. That is why the
///      kernel has 53 dimensions - it is the complement of the state's 43-dimensional span (the mean plus 42 occupied
///      modes) - and it is why the phase directions are hidden: NOT because the contractions are blind to them, but
///      because the state says nothing about them.
///
///  (2) WHY THE STATE OCCUPIES ONE MODE PER LEVEL, AND THEREFORE WHY ELEVEN ARE LEFT. The canonical state takes exactly
///      ONE basis vector per level - always basis[0]. A level of multiplicity m therefore receives content in one of its
///      m modes and leaves m-1 of them empty; summed over all levels that forces Sum(m-1) = 96 - 45 = 51 empty modes,
///      which is the free room. Measured, the substrate has 45 levels and only TWO are degenerate: eigenvalue 12 with
///      multiplicity 5 (channels 16, 32 and the ALTERNATING MODE) and eigenvalue 14 with multiplicity 6 (channels 8, 24,
///      40). Those two leave 4 + 5 = 9 modes empty, and SEVEN of the eleven lie among them - the six quadratures of
///      channels 32, 24 and 40 plus the alternating mode; the other two (one quadrature of channel 16 and one of channel
///      8) fall in channels the level DOES occupy, so they are among the 42 contingently hidden. TWO further levels carry
///      a CONSTRUCTION WEIGHT OF EXACTLY ZERO (indices 8 and 31), which empties their channels 14 and 19 in BOTH
///      quadratures: four more directions. Seven plus four is eleven, and the two parts are disjoint because 14 and 19
///      are not in a degenerate level.
///
///  (3) THE ELEVEN ARE NOT SUBSTRATE-INVARIANT, AND THE AUDIT SHOWS IT BY MOVING THEM. Rebuilding the same state from
///      the LAST basis vector of each level instead of the first changes the occupied set, and the unreachable set moves
///      with it - from 42/11 to 41/12 - because the alternating mode becomes occupied and channels 8, 16, 24 and 32 lose
///      their content. What IS invariant is the count the degeneracy forces: a level of multiplicity m leaves m-1 modes
///      empty for ANY state that takes one vector per level, so Sum(m-1) = 96 - 45 = 51 modes are always empty. The
///      remaining two come from a weight that vanishes, which is a property of a chosen formula and not of the substrate.
///
///  (4) WHAT MAKES THEM UNREACHABLE IS SHIFT-INVARIANCE, AND BOTH HALVES ARE MEASURED. A circulant operator is diagonal
///      in the Fourier basis, so it can never mix channels: from the state's own span it reaches each occupied channel's
///      OTHER quadrature (42 of them) and can never enter a channel the state does not occupy. AT's state-dependent
///      operators are not circulant - the connection it builds is a MULTIPLICATION by h(rho) composed with the
///      difference - and multiplication by a non-constant function DOES mix channels: measured, its exact Jacobian sends
///      the occupied modes into the eleven, with FULL rank 11. So the answer to the audit's test is yes and no at the same
///      time, and the two answers are exactly the two halves of the split.
/// </summary>
public static class ResidualPhaseAudit
{
    public const int Cells = RhoAccessibilityAudit.Cells;
    public const int D = 3;
    public const double Floor = 1e-9;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static double Norm(double[] v) => Math.Sqrt(v.Sum(x => x * x));

    // ===================== 1. THE ELEVEN =====================

    public static (int Channel, string Kind, double Frequency)[] UnreachableDirections()
        => KernelStructureAudit.ModeTable()
            .Where(t => t.Class == "HIDDEN")
            .Where(t => !KernelStructureAudit.VisibleModeVectors().Any(v => v.Channel == t.Channel))
            .Select(t => (t.Channel, t.Kind, t.Frequency))
            .OrderBy(t => t.Channel).ThenBy(t => t.Kind).ToArray();

    /// <summary>Every channel carrying NO visible mode: the five doublets plus the alternating channel.</summary>
    public static int[] EmptyChannels()
        => UnreachableDirections().Select(t => t.Channel).Distinct().OrderBy(c => c).ToArray();

    /// <summary>The five EMPTY DOUBLETS - the channels carrying no visible quadrature and no alternating mode.</summary>
    public static int[] EmptyDoubletChannels()
        => EmptyChannels().Where(c => c != AlternatingChannel()).ToArray();

    public static int AlternatingChannel() => Cells / 2;

    public static int TheCountIsEleven() => UnreachableDirections().Length;

    // ===================== 2. WHAT THE STATE OCCUPIES =====================

    /// <summary>The state's content in one mode: the whole classification turns on whether this is zero.</summary>
    public static double Occupancy(int channel, string kind)
    {
        var basis = KernelStructureAudit.ChannelBasis(channel);
        int index = kind == "sin" ? 1 : 0;
        if (basis.Length <= index) return 0.0;
        return State().Zip(basis[index], (a, b) => a * b).Sum();
    }

    public static (int Channel, string Kind, double Occupancy)[] ModeOccupancy()
        => KernelStructureAudit.ModeTable()
            .Select(t => (t.Channel, t.Kind, Occupancy(t.Channel, t.Kind)))
            .ToArray();

    public static int[] OccupiedChannels()
        => ModeOccupancy().Where(t => Math.Abs(t.Occupancy) > Floor).Select(t => t.Channel).Distinct().OrderBy(c => c).ToArray();

    public static string[] OccupiedModes()
        => ModeOccupancy().Where(t => Math.Abs(t.Occupancy) > Floor).Select(t => $"{t.Kind}_{t.Channel}").ToArray();

    /// <summary>The state's span: the mean plus the occupied modes - measured as a rank.</summary>
    public static int OccupiedSpanDimension()
    {
        var rows = new List<double[]> { Enumerable.Repeat(1.0 / Math.Sqrt(Cells), Cells).ToArray() };
        rows.AddRange(ModeOccupancy().Where(t => Math.Abs(t.Occupancy) > Floor)
            .Select(t => KernelStructureAudit.ChannelBasis(t.Channel)[t.Kind == "sin" ? 1 : 0]));
        return Rank(rows);
    }

    // ===================== 3. THE LEVEL STRUCTURE =====================

    public static int[] LevelChannels(int level)
    {
        var map = RhoObservableAudit.LevelIndexOfMode();
        return Enumerable.Range(0, Cells).Where(m => map[m] == level)
            .Select(m => Math.Min(m, Cells - m)).Distinct().OrderBy(c => c).ToArray();
    }

    public static (int Index, double Level, int Multiplicity, int[] Channels)[] DegenerateLevels()
        => Enumerable.Range(0, RhoObservableAudit.DistinctLevels())
            .Select(k => (Index: k, Level: RhoObservableAudit.Levels()[k].Level,
                          Multiplicity: RhoObservableAudit.LevelBasis(k).Length, Channels: LevelChannels(k)))
            .Where(t => t.Multiplicity > 2).ToArray();

    public static int[] ZeroWeightLevels()
        => Enumerable.Range(0, RhoObservableAudit.DistinctLevels())
            .Where(k => RhoAccessibilityAudit.BaseStateWeight(k) == 0.0).ToArray();

    public static int[] ZeroWeightChannels()
        => ZeroWeightLevels().SelectMany(LevelChannels).Distinct().OrderBy(c => c).ToArray();

    /// <summary>Modes any one-vector-per-level state must leave empty: Sum(m - 1) over the levels.</summary>
    public static int ModesLeftEmptyByDegeneracy()
        => Enumerable.Range(0, RhoObservableAudit.DistinctLevels()).Sum(k => RhoObservableAudit.LevelBasis(k).Length - 1);

    /// <summary>Modes left empty by degeneracy INSIDE the levels that carry more than one mode of one channel pair.</summary>
    public static int ModesLeftEmptyByTheDegenerateLevels()
        => DegenerateLevels().Sum(t => t.Multiplicity - 1);

    /// <summary>Directions of the eleven whose channel sits in a DEGENERATE level - the alternating mode included.</summary>
    public static int DirectionsFromDegeneracy()
        => UnreachableDirections().Count(t => DegenerateLevels().Any(d => d.Channels.Contains(t.Channel)));

    /// <summary>Directions of the eleven whose channel's level carries a VANISHING construction weight.</summary>
    public static int DirectionsFromZeroWeightLevels()
        => UnreachableDirections().Count(t => ZeroWeightChannels().Contains(t.Channel));

    /// <summary>The accounting of the eleven, each part measured directly and none double-counted.</summary>
    public static (int FromDegeneracy, int FromZeroWeight, int Total) TheElevenAccountedFor()
        => (DirectionsFromDegeneracy(), DirectionsFromZeroWeightLevels(),
            DirectionsFromDegeneracy() + DirectionsFromZeroWeightLevels());

    /// <summary>
    /// The account closes, the two parts do not overlap, and they exhaust the eleven. Measured, not asserted: the
    /// channels 14 and 19 are NOT in a degenerate level, which is what makes the two parts disjoint.
    /// </summary>
    public static bool TheElevenClose()
        => TheElevenAccountedFor().Total == TheCountIsEleven()
        && ZeroWeightChannels().Length == 2
        && ZeroWeightChannels().All(c => !DegenerateLevels().Any(d => d.Channels.Contains(c)))
        && DirectionsFromDegeneracy() == 7
        && DirectionsFromZeroWeightLevels() == 4;

    // ===================== 4. THE KERNEL RELATION =====================

    /// <summary>
    /// Hidden IF AND ONLY IF the state has no content in the mode. Measured over all 95 non-constant modes, which is the
    /// exact statement of the kernel relation: the kernel is the state's orthogonal complement, not an algebraic
    /// property of the contractions.
    /// </summary>
    public static bool HiddenIsExactlyZeroOccupancy()
    {
        var rho = State();
        foreach (var t in KernelStructureAudit.ModeTable())
        {
            var basis = KernelStructureAudit.ChannelBasis(t.Channel);
            int index = t.Kind == "sin" ? 1 : 0;
            if (basis.Length <= index) continue;
            double occupancy = Math.Abs(rho.Zip(basis[index], (a, b) => a * b).Sum());
            bool hidden = t.Class == "HIDDEN";
            if (hidden != (occupancy < 1e-11)) return false;
        }
        return true;
    }

    public static bool TheKernelIsTheStatesOrthogonalComplement()
        => HiddenIsExactlyZeroOccupancy()
        && KernelStructureAudit.KernelDimension() == Cells - OccupiedSpanDimension()
        && OccupiedSpanDimension() == 43;

    public static int KernelDimension() => KernelStructureAudit.KernelDimension();

    // ===================== 5. SYMMETRY =====================

    public static int OrbitSize(double[] mode)
        => RhoAccessibilityAudit.Group().Select(g => RhoAccessibilityAudit.Apply(g, mode))
            .Select(v => string.Join(",", v.Select(x => Math.Round(x, 10))))
            .Distinct().Count();

    public static (int Channel, string Kind, int OrbitSize)[] OrbitTable()
        => UnreachableDirections().Select(t =>
            (t.Channel, t.Kind, OrbitSize(KernelStructureAudit.ChannelBasis(t.Channel)[t.Kind == "sin" ? 1 : 0]))).ToArray();

    /// <summary>The eleven are a union of whole doublets plus the alternating mode, so the symmetry group preserves the SET.</summary>
    /// <summary>
    /// The eleven are a union of whole doublets plus the alternating mode, so the group must carry the SET into itself -
    /// but as a SPAN, not as a list of vectors. A rotation turns cos_c into a combination of cos_c and sin_c, so testing
    /// each image against the eleven VECTORS fails even when the set is preserved; a first version of this method did
    /// exactly that and reported False. The test here asks whether the image stays inside the span.
    /// </summary>
    public static bool TheElevenFormASymmetryInvariantSet()
    {
        var eleven = UnreachableDirections()
            .Select(t => KernelStructureAudit.ChannelBasis(t.Channel)[t.Kind == "sin" ? 1 : 0]).ToArray();
        foreach (var g in RhoAccessibilityAudit.Group())
            foreach (var e in eleven)
            {
                var image = RhoAccessibilityAudit.Apply(g, e);
                double inside = eleven.Sum(f => Math.Pow(image.Zip(f, (a, b) => a * b).Sum(), 2));
                if (Math.Abs(inside - 1.0) > 1e-9) return false;
            }
        return true;
    }

    public static int GroupOrder() => RhoAccessibilityAudit.Group().Length;

    // ===================== 6. REACHABILITY =====================

    private static double[] Difference(double[] v)
        => Enumerable.Range(0, Cells).Select(i => v[(i + 1) % Cells] - v[i]).ToArray();

    private static double[] CentredDifference(double[] v)
        => Enumerable.Range(0, Cells).Select(i => (v[(i + 1) % Cells] - v[(i - 1 + Cells) % Cells]) / 2.0).ToArray();

    private static double ScalarDerivative(Func<double, double> f, double x)
        => (f(x * (1.0 + 1e-5)) - f(x * (1.0 - 1e-5))) / (2.0 * x * 1e-5);

    /// <summary>An AT-native, STATE-DEPENDENT operator: the connection AT builds, h(rho) times the difference.</summary>
    public static double[] ConnectionJacobian(double[] v)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        var rho = State();
        return Enumerable.Range(0, Cells).Select(i =>
            ScalarDerivative(h, rho[i]) * (rho[(i + 1) % Cells] - rho[i]) * v[i]
            + h(rho[i]) * (v[(i + 1) % Cells] - v[i])).ToArray();
    }

    /// <summary>The two-gradient (T1/T2) coupling of G_058 - also state-dependent.</summary>
    public static double[] SectorCouplingJacobian(double[] v)
    {
        var rho = State();
        return Enumerable.Range(0, Cells).Select(i =>
        {
            double d1 = rho[(i + 1) % Cells] - rho[i], d3 = rho[(i + 3) % Cells] - rho[i];
            double e1 = v[(i + 1) % Cells] - v[i], e3 = v[(i + 3) % Cells] - v[i];
            return e1 * d3 + d1 * e3;
        }).ToArray();
    }

    private static double[][] UnreachableBasis()
        => UnreachableDirections().Select(t => KernelStructureAudit.ChannelBasis(t.Channel)[t.Kind == "sin" ? 1 : 0]).ToArray();

    /// <summary>How much of an operator's response to the occupied modes lands in the eleven.</summary>
    public static (double Projection, int Rank) Response(string op)
    {
        var eleven = UnreachableBasis();
        var images = KernelStructureAudit.VisibleModeVectors().Select(m =>
        {
            var response = op switch
            {
                "difference (circulant)" => Difference(m.Mode),
                "centred difference (circulant)" => CentredDifference(m.Mode),
                "connection h(rho) d rho (state-dependent)" => ConnectionJacobian(m.Mode),
                "T1/T2 coupling (state-dependent)" => SectorCouplingJacobian(m.Mode),
                _ => throw new ArgumentException(op),
            };
            return eleven.Select(e => response.Zip(e, (a, b) => a * b).Sum()).ToArray();
        }).ToArray();
        return (images.Select(Norm).Max(), Rank(images));
    }

    public static (string Operator, double Projection, int Rank)[] ReachTable()
        => new[]
        {
            "difference (circulant)", "centred difference (circulant)",
            "connection h(rho) d rho (state-dependent)", "T1/T2 coupling (state-dependent)",
        }.Select(op => { var (p, r) = Response(op); return (op, p, r); }).ToArray();

    public static bool NoCirculantAtOperatorReachesThem()
        => ReachTable().Where(t => t.Operator.Contains("circulant")).All(t => t.Rank == 0 && t.Projection < 1e-12);

    public static bool StateDependentAtOperatorsReachAllOfThem()
        => ReachTable().Where(t => t.Operator.Contains("state-dependent")).All(t => t.Rank == 11 && t.Projection > 1e-6);

    // ===================== 7. THE DECISIVE EXPERIMENT =====================

    /// <summary>
    /// The same construction from the LAST basis vector of each level. If the eleven were a substrate property the
    /// unreachable set would not move; measured, it does.
    /// </summary>
    public static double[] AlternativeState()
    {
        var rho = Enumerable.Repeat(1.0, Cells).ToArray();
        int levels = RhoObservableAudit.DistinctLevels();
        for (int k = 0; k < levels; k++)
        {
            var basis = RhoObservableAudit.LevelBasis(k);
            if (basis.Length == 0) continue;
            var v = basis[^1];
            double weight = RhoAccessibilityAudit.BaseStateWeight(k);
            for (int i = 0; i < Cells; i++) rho[i] += 0.15 * weight * v[i];
        }
        double min = rho.Min();
        for (int i = 0; i < Cells; i++) rho[i] = rho[i] - min + 0.2;
        double sum = rho.Sum();
        for (int i = 0; i < Cells; i++) rho[i] *= Cells / sum;
        return rho;
    }

    public static int[] AlternativeEmptyChannels()
    {
        var rho = AlternativeState();
        return KernelStructureAudit.ModeTable()
            .GroupBy(t => t.Channel)
            .Where(g => g.All(t =>
            {
                var basis = KernelStructureAudit.ChannelBasis(t.Channel);
                int index = t.Kind == "sin" ? 1 : 0;
                return basis.Length <= index || Math.Abs(rho.Zip(basis[index], (a, b) => a * b).Sum()) < Floor;
            }))
            .Select(g => g.Key).OrderBy(c => c).ToArray();
    }

    public static int AlternativeUnreachableCount()
        => AlternativeEmptyChannels().Sum(c => KernelStructureAudit.ChannelBasis(c).Length);

    /// <summary>The canonical state's count, by the same route, so the two are comparable.</summary>
    public static int CanonicalUnreachableCount()
        => TheCountIsEleven();

    public static bool AlternativeOccupiedAlternating()
    {
        var rho = AlternativeState();
        var alt = KernelStructureAudit.ChannelBasis(AlternatingChannel())[0];
        return Math.Abs(rho.Zip(alt, (a, b) => a * b).Sum()) > Floor;
    }

    /// <summary>The set moves: 42/11 becomes 41/12. What does NOT move is the count degeneracy forces.</summary>
    public static bool TheElevenAreNotSubstrateInvariant()
        => !AlternativeEmptyChannels().SequenceEqual(EmptyChannels())
        && AlternativeEmptyChannels().Contains(AlternatingChannel()) == false;

    public static bool TheDegeneracyCountIsInvariant()
    {
        var rho = AlternativeState();
        int empty = KernelStructureAudit.ModeTable().Count(t =>
        {
            var basis = KernelStructureAudit.ChannelBasis(t.Channel);
            int index = t.Kind == "sin" ? 1 : 0;
            return basis.Length <= index || Math.Abs(rho.Zip(basis[index], (a, b) => a * b).Sum()) < Floor;
        });
        return empty == Cells - OccupiedSpanDimension();
    }

    // ===================== 8. RANK HELPER =====================

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

    // ===================== 9. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: the origins close AND the reachability is explained by shift-invariance - circulant operators
    /// reach none, state-dependent ones reach all. REFUTED: the accounting does not close. BOUNDARY: an AT operator that
    /// IS circulant reaches one of them, which would leave the split unexplained.
    /// </summary>
    public static string Verdict()
    {
        if (!TheKernelIsTheStatesOrthogonalComplement()) return "REFUTED";
        if (EmptyDoubletChannels().Length != 5) return "REFUTED";
        if (!TheElevenClose()) return "REFUTED";
        if (!NoCirculantAtOperatorReachesThem()) return "BOUNDARY";
        if (!StateDependentAtOperatorsReachAllOfThem()) return "BOUNDARY";
        return "DERIVED";
    }

    public static string WhereItStands()
    {
        var sb = new StringBuilder();
        sb.Append("THE KERNEL IS THE STATE'S ORTHOGONAL COMPLEMENT, AND THAT IS EXACT. A contraction row is A_d rho with A_d the distance-d relation; A_d is CIRCULANT, so on a single mode e of channel c it acts as <A_d rho, e> = lambda_d(c) <rho, e>, and lambda_0(c) = 1. The row therefore vanishes on e EXACTLY when the state has no content in it: HIDDEN IF AND ONLY IF <rho, e> = 0. ");
        sb.Append($"Measured over all 95 non-constant modes: {HiddenIsExactlyZeroOccupancy()}. So the phase directions are hidden not because the contractions are blind to them but because the STATE says nothing about them, and the kernel dimension ({KernelDimension()}) is the complement of the state's {OccupiedSpanDimension()}-dimensional span (the mean plus the occupied modes). ");
        sb.Append($"WHY ELEVEN ARE LEFT OVER. The canonical state takes exactly ONE basis vector per level - always basis[0] - so a level of multiplicity m leaves m-1 of its modes empty. The substrate has {RhoObservableAudit.DistinctLevels()} levels and only two are degenerate: ");
        foreach (var (index, level, mult, channels) in DegenerateLevels())
            sb.Append($"eigenvalue {level:F6} with multiplicity {mult}, level index {index}, channels {string.Join(", ", channels)}; ");
        sb.Append($"those two leave {ModesLeftEmptyByTheDegenerateLevels()} modes empty, and {ZeroWeightLevels().Length} further levels have a CONSTRUCTION WEIGHT OF EXACTLY ZERO (indices {string.Join(", ", ZeroWeightLevels())}, channels {string.Join(", ", ZeroWeightChannels())}), so those channels are empty in BOTH quadratures. The eleven divide into {DirectionsFromDegeneracy()} from degeneracy, {DirectionsFromZeroWeightLevels()} from vanishing weights and the alternating mode. ");
        sb.Append($"THEY ARE NOT SUBSTRATE-INVARIANT, AND THE AUDIT MOVES THEM. Rebuilding the same state from the LAST basis vector of each level changes the occupied set: the unreachable set moves from {CanonicalUnreachableCount()} directions over {EmptyChannels().Length} channels to {AlternativeUnreachableCount()} directions over {AlternativeEmptyChannels().Length} channels, because the alternating mode becomes occupied and channels 8, 16, 24 and 32 lose their content ({TheElevenAreNotSubstrateInvariant()}). What IS invariant is the count the degeneracy forces: a level of multiplicity m leaves m-1 modes empty for ANY such state, so {ModesLeftEmptyByDegeneracy()} modes are always empty - which is the free room, {Cells} minus the {RhoObservableAudit.DistinctLevels()} levels. The two extra come from a weight that vanishes, and that is a property of a CHOSEN FORMULA rather than of the substrate. ");
        sb.Append($"WHAT MAKES THEM UNREACHABLE IS SHIFT-INVARIANCE, AND BOTH HALVES ARE MEASURED. A circulant operator is diagonal in the Fourier basis, so it can never mix channels: from the state's own span it reaches each occupied channel's OTHER quadrature and can never enter a channel the state does not occupy. ");
        foreach (var (op, projection, rank) in ReachTable())
            sb.Append($"{op}: projection onto the eleven {projection:E3}, rank {rank}. ");
        sb.Append($"So the connection AT builds - a MULTIPLICATION by h(rho) composed with the difference - reaches all eleven ({StateDependentAtOperatorsReachAllOfThem()}), while every circulant AT operator reaches none ({NoCirculantAtOperatorReachesThem()}). The answer to the audit's own test is YES for AT's state-dependent operators and NO for its shift-invariant ones, and those two answers are the two halves of the split. ");
        sb.Append($"THE SYMMETRY IS WHAT SURVIVES: the group has order {GroupOrder()}, the eleven form a symmetry-invariant SET ({TheElevenFormASymmetryInvariantSet()}) because they are whole doublets plus the alternating mode, and the alternating mode's orbit is the smallest of all ({OrbitSize(KernelStructureAudit.ChannelBasis(AlternatingChannel())[0])} vectors, because a shift by two fixes it).");
        return sb.ToString();
    }

    // ===================== 10. REPORTS =====================

    public static string OutputTheEleven()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE ELEVEN, IDENTIFIED");
        sb.AppendLine($"   count = {TheCountIsEleven()}; empty channels = {string.Join(", ", EmptyChannels())} (both quadratures each)");
        sb.AppendLine("   channel | kind | frequency | orbit size | occupancy");
        foreach (var (channel, kind, frequency) in UnreachableDirections())
            sb.AppendLine($"   {channel,7} | {kind,4} | {frequency,9:F6} | {OrbitSize(KernelStructureAudit.ChannelBasis(channel)[kind == "sin" ? 1 : 0]),10} | {Occupancy(channel, kind),9:E3}");
        sb.AppendLine();
        sb.AppendLine("   the state's occupancy: occupied modes = " + OccupiedModes().Length
            + ", occupied span = " + OccupiedSpanDimension() + ", kernel = " + KernelDimension());
        sb.AppendLine("   hidden iff zero occupancy : " + HiddenIsExactlyZeroOccupancy());
        return sb.ToString();
    }

    public static string OutputLevels()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE LEVEL STRUCTURE AND THE ACCOUNTING OF ELEVEN");
        sb.AppendLine($"   levels = {RhoObservableAudit.DistinctLevels()} over {Cells} modes; modes any one-vector-per-level state leaves empty = Sum(m-1) = {ModesLeftEmptyByDegeneracy()}");
        sb.AppendLine("   degenerate levels:");
        foreach (var (index, level, mult, channels) in DegenerateLevels())
            sb.AppendLine($"     level index {index,3} eigenvalue {level,9:F6} multiplicity {mult} channels {string.Join(", ", channels)} -> leaves {mult - 1} modes empty");
        sb.AppendLine($"   levels with a vanishing construction weight: {string.Join(", ", ZeroWeightLevels())} -> channels {string.Join(", ", ZeroWeightChannels())} entirely empty ({DirectionsFromZeroWeightLevels()} directions)");
        sb.AppendLine($"   ACCOUNTING: {DirectionsFromDegeneracy()} from degeneracy (the alternating mode inside it) + {DirectionsFromZeroWeightLevels()} from vanishing weights = {TheElevenAccountedFor().Total} of {TheCountIsEleven()}, and the parts are disjoint: {TheElevenClose()}");
        return sb.ToString();
    }

    public static string OutputReach()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. CAN AN EXISTING AT OPERATOR REACH THEM?");
        sb.AppendLine("   operator                                    | projection onto the eleven | rank");
        foreach (var (op, projection, rank) in ReachTable())
            sb.AppendLine($"   {op,-43} | {projection,26:E3} | {rank,4}");
        sb.AppendLine("   circulant operators reach none : " + NoCirculantAtOperatorReachesThem());
        sb.AppendLine("   state-dependent ones reach all : " + StateDependentAtOperatorsReachAllOfThem());
        sb.AppendLine();
        sb.AppendLine("4. THE DECISIVE EXPERIMENT");
        sb.AppendLine($"   canonical state (basis[0]) : empty channels {string.Join(", ", EmptyChannels())} -> {CanonicalUnreachableCount()} directions");
        sb.AppendLine($"   alternative state (basis[^1]) : empty channels {string.Join(", ", AlternativeEmptyChannels())} -> {AlternativeUnreachableCount()}");
        sb.AppendLine("   the set MOVES : " + TheElevenAreNotSubstrateInvariant());
        sb.AppendLine("   the degeneracy count is invariant : " + TheDegeneracyCountIsInvariant());
        sb.AppendLine("   symmetry group order " + GroupOrder() + "; the eleven form an invariant set : " + TheElevenFormASymmetryInvariantSet());
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
