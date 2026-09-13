using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_042 — THREE-DIMENSIONALITY DEPENDENCY AUDIT.
///
/// QUESTION. Are the known selectors of d = 3 INDEPENDENT, or are they the same structure viewed differently —
/// i.e. is three-dimensionality pinned by ONE root mechanism or by several?
///
/// Inputs, as given: (1) rotation self-duality d(d-1)/2 = d; (2) Hodge duality dim(Lambda^2) = dim(V);
/// (3) photon/graviton polarisation equality d - 1 = (d+1)(d-2)/2; (4) the D96^d representation structure
/// (T1(3), T2(3)); (5) the clock exponent dtau/dt = rho^(1/d).
///
/// ANSWER: **REDUNDANT — ONE ROOT MECHANISM, and the count of independent selectors is 1.**
///
///  (1) THREE OF THE FIVE ARE THE SAME EQUATION, and not merely at the shared root — identically in d.
///      Two identities settle it, both verified for every d on the ladder:
///        photon   polarisations = dim(V) - 1        = d - 1
///        graviton polarisations = dim(Lambda^2) - 1 = d(d-1)/2 - 1 = dim(so(d)) - 1
///      Subtracting 1 from both sides, "the polarisations are equal" IS "dim(Lambda^2) = dim(V)" — the Hodge
///      condition — for EVERY d, not only at d = 3. And dim(Lambda^2) is the number of independent rotations,
///      so the polarisation equality says: THE NUMBER OF DIRECTIONS EQUALS THE NUMBER OF INDEPENDENT ROTATIONS.
///      All three inputs reduce to the same polynomial, d(d-3) = 0, with roots {0, 3}.
///
///  (2) THE FOURTH INPUT IS STRICTLY WEAKER, in both of its readings, and its conjunction is not new.
///      "The cube supplies a 3-dimensional irrep" holds for every d >= 3; "the vector is the largest irrep"
///      holds for d <= 3. Neither selects 3 alone. Their conjunction does reproduce the root's solution set
///      exactly — which is why this argument has LOOKED like a second mechanism — but it is implied by the root
///      (d = 3 satisfies both halves) and therefore redundant per the audit's own rule.
///
///  (3) THE FIFTH IS NOT A SELECTOR AT ALL. The clock law holds at every d: it is the LAW THAT MAKES THE CHOICE
///      OBSERVABLE, not a condition that picks it. Its status is REDUNDANT as a selector and INDEPENDENT in
///      role — it is the only input on the list that a measurement could read.
///
///  (4) THE ACCIDENTS ARE NOT ALL THE SAME ACCIDENT — each dimension owns a different one, and only the d = 3
///      one selects d = 3. Computed: bivectors collapse to a SCALAR at d = 2 (dim 1), ARE the vectors at d = 3
///      (dim 3 = dim V), split self-dual / anti-self-dual at d = 4 (dim 6 = 3 + 3), and the cross product
///      reappears at d = 7 (Hurwitz, cited rather than computed). So the family of "special dimensions" is real
///      and each membership is different — but the membership that matters here is {3}, and it is one equation.
///
///  (5) THIS REFINES G_041. G_041 called the eps/Hodge accident and the polarisation match "two independent
///      accidents". They are one condition, reached by two derivations: the tensor-algebra route (Lambda^2 is
///      dual to V) and the little-group route (both massless counts are the same function of d shifted by 1).
///      G_041's BOUNDARY verdict is unchanged — d = 4 is still not excluded by the representations — but the
///      number of INDEPENDENT reasons for d = 3 is 1, not 2. Recorded, not smoothed over.
/// </summary>
public static class ThreeDimensionalityDependencyAudit
{
    /// <summary>How far the solution sets are computed (the irrep spectrum is cheap; the group is not).</summary>
    public const int MaxDimension = 12;

    // ═══ §1  THE INGREDIENTS ════════════════════════════════════════════════════════════════════

    public static int VectorDimension(int d) => d;
    public static int AntisymmetricSquare(int d) => d * (d - 1) / 2;
    public static int SymmetricSquare(int d) => d * (d + 1) / 2;
    public static int TracelessSymmetric(int d) => d * (d + 1) / 2 - 1;

    /// <summary>The number of independent rotations = dim so(d) = dim Lambda^2.</summary>
    public static int RotationGenerators(int d) => d * (d - 1) / 2;

    /// <summary>Massless spin-1 polarisations in d dimensions: D - 2 with D = d + 1.</summary>
    public static int PhotonPolarisations(int d) => d - 1;

    /// <summary>Massless spin-2 polarisations: D(D-3)/2 with D = d + 1.</summary>
    public static int GravitonPolarisations(int d) => (d + 1) * (d - 2) / 2;

    // ═══ §2  THE IDENTITIES THAT DECIDE REDUNDANCY ══════════════════════════════════════════════

    /// <summary>photon = dim(V) - 1, for every d.</summary>
    public static bool PhotonIsVectorMinusOne()
        => Enumerable.Range(1, MaxDimension).All(d => PhotonPolarisations(d) == VectorDimension(d) - 1);

    /// <summary>
    /// graviton = dim(Lambda^2) - 1 = dim(so(d)) - 1, for every d. THE KEY IDENTITY: it is what turns
    /// "the polarisations are equal" into "dim(Lambda^2) = dim(V)" identically rather than only at d = 3.
    /// </summary>
    public static bool GravitonIsRotationAlgebraMinusOne()
        => Enumerable.Range(1, MaxDimension).All(d =>
            GravitonPolarisations(d) == AntisymmetricSquare(d) - 1
            && AntisymmetricSquare(d) == RotationGenerators(d));

    /// <summary>dim so(d) = dim Lambda^2 — the rotations ARE the antisymmetric square, for every d.</summary>
    public static bool RotationsAreTheAntisymmetricSquare()
        => Enumerable.Range(1, MaxDimension).All(d => RotationGenerators(d) == AntisymmetricSquare(d));

    /// <summary>
    /// Do the three geometric conditions have the SAME solution set, at every d — i.e. are they one equation?
    /// </summary>
    public static bool TheGeometricConditionsAreOneEquation()
    {
        var a = SolutionSet(SelfDuality);
        var b = SolutionSet(Hodge);
        var c = SolutionSet(Polarisations);
        return SameSet(a, b) && SameSet(b, c);
    }

    /// <summary>The three reduce to one polynomial with one root set.</summary>
    public static string RootEquation() => "d(d-3) = 0";

    public static int[] RootSolutions() => new[] { 0, RequiredDimension };

    /// <summary>The dimension the audit is about.</summary>
    public const int RequiredDimension = 3;

    public static string RootStatement()
        => "THE NUMBER OF DIRECTIONS EQUALS THE NUMBER OF INDEPENDENT ROTATIONS — the existence of the "
         + "cross product / Hodge dual in three dimensions, written as dim(Lambda^2) = dim(V), equivalently "
         + "d(d-1)/2 = d, equivalently (via graviton = dim so(d) - 1) the equality of the photon and graviton "
         + "polarisation counts.";

    // ═══ §3  THE SELECTORS AND THEIR SOLUTION SETS ══════════════════════════════════════════════

    public static bool SelfDuality(int d) => RotationGenerators(d) == VectorDimension(d);
    public static bool Hodge(int d) => AntisymmetricSquare(d) == VectorDimension(d);
    public static bool Polarisations(int d) => PhotonPolarisations(d) == GravitonPolarisations(d);
    public static bool SuppliesThreeDimensionalIrrep(int d)
        => SubstrateDimensionAudit.MaxIrrepDimension(d) >= 3;
    public static bool VectorIsTheLargestIrrep(int d) => SubstrateDimensionAudit.MaxIrrepDimension(d) == d;
    public static bool ClockLawHolds(int d) => d >= 1;

    /// <summary>The five inputs as the audit tests them — input 4 splits into its two readings.</summary>
    public static (string Name, Func<int, bool> Holds)[] Selectors() => new (string, Func<int, bool>)[]
    {
        ("1  rotation self-duality  d(d-1)/2 = d", SelfDuality),
        ("2  Hodge duality          dim(L2) = dim V", Hodge),
        ("3  polarisation equality  d-1 = (d+1)(d-2)/2", Polarisations),
        ("4a D96^d supplies a 3-dim irrep", SuppliesThreeDimensionalIrrep),
        ("4b D96^d: the vector is the largest irrep", VectorIsTheLargestIrrep),
        ("5  clock law rho^(1/d)", ClockLawHolds),
    };

    public static int[] SolutionSet(Func<int, bool> holds)
        => Enumerable.Range(1, MaxDimension).Where(holds).ToArray();

    public static bool SameSet(int[] a, int[] b) => a.Length == b.Length && a.SequenceEqual(b);

    private static bool StrictSubset(int[] small, int[] big)
        => small.Length < big.Length && small.All(big.Contains);

    /// <summary>
    /// THE DEPENDENCY GRAPH, computed from the solution sets: A implies B exactly when S(A) is contained in
    /// S(B). Every ordered pair of distinct selectors is tested, so the graph is derived rather than drawn.
    /// </summary>
    public static (string A, string B, bool Implies, string Basis)[] DependencyGraph()
    {
        var sels = Selectors();
        var sets = sels.Select(s => SolutionSet(s.Holds)).ToArray();
        var edges = new List<(string, string, bool, string)>();
        for (int i = 0; i < sels.Length; i++)
            for (int j = 0; j < sels.Length; j++)
            {
                if (i == j) continue;
                bool implies = sets[i].All(sets[j].Contains);
                if (!implies) continue;
                string labelI = sels[i].Name.Split(' ')[0];
                string labelJ = sels[j].Name.Split(' ')[0];
                string basis = SameSet(sets[i], sets[j])
                    ? $"EQUIVALENT: both select {{{string.Join(",", sets[i])}}}"
                    : $"{labelI} selects {{{string.Join(",", sets[i])}}}, a subset of what {labelJ} admits "
                      + $"({sets[j].Length} dimensions) — a strict weakening";
                edges.Add((sels[i].Name, sels[j].Name, true, basis));
            }
        return edges.ToArray();
    }

    /// <summary>
    /// THE CLASSIFICATION. Equivalent selectors are one mechanism: the first of an equivalence class is the
    /// representative, the rest are RESTATEMENTS. A class that a stronger class contains is a CONSEQUENCE. A
    /// selector that holds everywhere is not a selector at all.
    /// </summary>
    public static (string Selector, int[] Solutions, string Status, string Basis)[] Classify()
    {
        var sels = Selectors();
        var sets = sels.Select(s => SolutionSet(s.Holds)).ToArray();
        var representative = new int[sels.Length];
        for (int i = 0; i < sels.Length; i++)
        {
            int first = 0;
            for (int j = 0; j < sels.Length; j++) if (SameSet(sets[j], sets[i])) { first = j; break; }
            representative[i] = first;
        }

        var rows = new List<(string, int[], string, string)>();
        for (int i = 0; i < sels.Length; i++)
        {
            bool stronger = Enumerable.Range(0, sels.Length)
                .Any(j => j != i && StrictSubset(sets[j], sets[i]));
            bool everywhere = sets[i].Length == MaxDimension;
            string status;
            string basis;
            if (i != representative[i])
            {
                status = "REDUNDANT";
                basis = $"same solution set as selector {representative[i] + 1} — one mechanism, stated twice";
            }
            else if (everywhere)
            {
                status = "REDUNDANT";
                basis = $"holds at every d = 1..{MaxDimension}: a law, not a selector";
            }
            else if (stronger)
            {
                status = "DERIVED FROM";
                basis = "implied by a strictly stronger selector; does not single out d = 3 alone";
            }
            else
            {
                status = "INDEPENDENT";
                basis = "no other selector implies it — this is the root";
            }
            rows.Add((sels[i].Name, sets[i], status, basis));
        }
        return rows.ToArray();
    }

    public static string[] IndependentSelectors()
        => Classify().Where(r => r.Status == "INDEPENDENT").Select(r => r.Selector).ToArray();

    public static string[] RedundantSelectors()
        => Classify().Where(r => r.Status == "REDUNDANT").Select(r => r.Selector).ToArray();

    public static string[] DerivedSelectors()
        => Classify().Where(r => r.Status == "DERIVED FROM").Select(r => r.Selector).ToArray();

    /// <summary>How many independent mechanisms select d = 3? The answer the audit exists to compute.</summary>
    public static int IndependentRootCount() => IndependentSelectors().Length;

    /// <summary>Does the conjunction of the two representation readings reproduce the root's solution set?</summary>
    public static bool TheRepresentationConjunctionReproducesTheRoot()
    {
        var conjunction = Enumerable.Range(1, MaxDimension)
            .Where(d => SuppliesThreeDimensionalIrrep(d) && VectorIsTheLargestIrrep(d)).ToArray();
        return SameSet(conjunction, SolutionSet(SelfDuality));
    }

    /// <summary>Is the third in the list (polarisations) implied by the root without being a new condition?</summary>
    public static bool ThePolarisationRouteIsTheHodgeRoute()
        => SameSet(SolutionSet(Polarisations), SolutionSet(Hodge));

    // ═══ §4  THE ACCIDENTS BELONG TO DIFFERENT DIMENSIONS ═══════════════════════════════════════

    public static bool BivectorsAreScalars(int d) => AntisymmetricSquare(d) == 1;
    public static bool BivectorsAreVectors(int d) => AntisymmetricSquare(d) == VectorDimension(d);
    public static bool BivectorDimensionIsEven(int d) => AntisymmetricSquare(d) % 2 == 0 && d >= 4;

    public static int[] DimensionsWhereBivectorsAreScalars()
        => Enumerable.Range(1, MaxDimension).Where(BivectorsAreScalars).ToArray();

    public static int[] DimensionsWhereBivectorsAreVectors()
        => Enumerable.Range(1, MaxDimension).Where(BivectorsAreVectors).ToArray();

    /// <summary>Dimensions where a self-dual / anti-self-dual split of Lambda^2 is even possible.</summary>
    public static int[] DimensionsAdmittingASelfDualSplit()
        => Enumerable.Range(1, MaxDimension).Where(BivectorDimensionIsEven).ToArray();

    // ═══ §5  VERDICT AND REPORT ═════════════════════════════════════════════════════════════════

    /// <summary>
    /// REDUNDANT (computed). The floor is recomputed first — the two polarisation identities for every d, the
    /// equality of dim so(d) with dim Lambda^2, the shared solution sets, and the representation conjunction.
    /// On that floor four of the five inputs carry no independent content: three are the same equation reached
    /// by two derivations, one is a strictly weaker consequence, and one is not a selector at all. One root
    /// mechanism remains.
    /// </summary>
    public static string Verdict()
    {
        bool identities = PhotonIsVectorMinusOne()
                       && GravitonIsRotationAlgebraMinusOne()
                       && RotationsAreTheAntisymmetricSquare();
        bool oneEquation = TheGeometricConditionsAreOneEquation()
                        && ThePolarisationRouteIsTheHodgeRoute();
        bool roots = SolutionSet(SelfDuality).Length == 1
                  && SolutionSet(SelfDuality)[0] == RequiredDimension
                  && TheRepresentationConjunctionReproducesTheRoot();
        bool single = IndependentRootCount() == 1;
        if (!(identities && oneEquation && roots)) return "INDEPENDENT";   // the audit is not on its own floor
        return single ? "REDUNDANT" : "INDEPENDENT";
    }

    public static string WhereItStands()
        => "ONE ROOT MECHANISM, AND THE FIVE INPUTS COLLAPSE TO IT. The audit first recomputes its floor: two "
         + "polarisation identities verified for every dimension on the ladder, the equality of the rotation "
         + "algebra's dimension with the antisymmetric square, and the solution sets of all six predicates "
         + "computed rather than argued. The decisive identity is the second one — the graviton carries "
         + "dim(Lambda^2) - 1 polarisations, which is dim(so(d)) - 1, for EVERY d. Subtract one from the photon's "
         + "count (dim(V) - 1) and 'the polarisations are equal' becomes 'dim(Lambda^2) = dim(V)' identically: "
         + "the third input is not merely a coincidence that happens to share the root, it is the SAME EQUATION, "
         + "reached by a different derivation (little-group counting instead of tensor algebra). The first and "
         + "second inputs are the same expression written twice, so all three reduce to one polynomial, "
         + "d(d-3) = 0, whose roots are 0 and 3 — and its physical reading is that the number of directions "
         + "equals the number of independent rotations, which is what the cross product IS. The fourth input is "
         + "genuinely different in kind — a group-theoretic availability condition rather than a dimension count "
         + "— but it is strictly weaker in both of its readings: a dimension-3 irrep exists for every d >= 3, "
         + "and the vector is the largest irrep for d <= 3, so neither half selects three alone. Their "
         + "CONJUNCTION does reproduce the root exactly, which is precisely why this argument has looked like a "
         + "second mechanism in the earlier audits; but the root implies both halves, so under the audit's own "
         + "rule the conjunction is redundant too. The fifth input is not a selector at all: the clock law holds "
         + "at every dimension, and its role is the opposite one — it is the law that makes the choice "
         + "OBSERVABLE, the only entry on the list a measurement could read. What the audit does NOT claim is "
         + "that all dimensional accidents are the same accident. It checks, and they are not: bivectors "
         + "collapse to a scalar at d = 2, become the vectors themselves at d = 3, split self-dual and "
         + "anti-self-dual at d = 4, and the cross product reappears at d = 7. Each special dimension owns a "
         + "different coincidence. But only the d = 3 membership singles out d = 3, and it does so once. So the "
         + "answer to the question as posed is that three-dimensionality rests on ONE root mechanism, not "
         + "several: d(d-3) = 0, seen three times and merely implied twice more. THAT REFINES G_041, which "
         + "called the Hodge accident and the polarisation match two independent accidents — they are one "
         + "condition with two derivations, so the number of independent reasons for d = 3 is one, not two. "
         + "G_041's BOUNDARY verdict is unchanged: d = 4 is still not excluded by the representations, only by "
         + "this single accident, and the count of independent reasons being one rather than two is the honest "
         + "correction.";

    public static string OutputIdentities()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE IDENTITIES THAT DECIDE REDUNDANCY — verified for every d");
        sb.AppendLine($"   photon   = dim(V) - 1                       : {PhotonIsVectorMinusOne()}");
        sb.AppendLine($"   graviton = dim(Lambda^2) - 1 = dim(so(d)) - 1: {GravitonIsRotationAlgebraMinusOne()}");
        sb.AppendLine($"   dim(so(d)) = dim(Lambda^2)                  : {RotationsAreTheAntisymmetricSquare()}");
        sb.AppendLine($"   the three geometric conditions are ONE equation: {TheGeometricConditionsAreOneEquation()}");
        sb.AppendLine($"   the polarisation route IS the Hodge route   : {ThePolarisationRouteIsTheHodgeRoute()}");
        sb.AppendLine();
        sb.AppendLine("   d | dim V | dim L2 = dim so(d) | photons | gravitons | difference");
        for (int d = 1; d <= 8; d++)
            sb.AppendLine($"   {d,2} | {VectorDimension(d),6} | {AntisymmetricSquare(d),16} | {PhotonPolarisations(d),7} | "
                          + $"{GravitonPolarisations(d),9} | {PhotonPolarisations(d) - GravitonPolarisations(d),10}");
        sb.AppendLine($"   the difference vanishes only where dim L2 = dim V, i.e. d = "
                      + $"{string.Join(", ", SolutionSet(Hodge))}");
        sb.AppendLine();
        sb.AppendLine($"   ROOT EQUATION  {RootEquation()}   roots {{{string.Join(", ", RootSolutions())}}}");
        sb.AppendLine($"   {RootStatement()}");
        return sb.ToString();
    }

    public static string OutputGraph()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE DEPENDENCY GRAPH — computed, not drawn");
        sb.AppendLine("   solution sets over d = 1.." + MaxDimension + ":");
        foreach (var (name, holds) in Selectors())
            sb.AppendLine($"     S({name,-46}) = {{{string.Join(",", SolutionSet(holds))}}}");
        sb.AppendLine();
        sb.AppendLine("   implications (A implies B when S(A) is contained in S(B)):");
        foreach (var (a, b, _, basis) in DependencyGraph())
            sb.AppendLine($"     {a,-46} ==> {b,-46} {basis}");
        return sb.ToString();
    }

    public static string OutputClassification()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE CLASSIFICATION");
        foreach (var (selector, solutions, status, basis) in Classify())
        {
            sb.AppendLine($"   {status,-12} {selector}");
            sb.AppendLine($"                S = {{{string.Join(",", solutions)}}}");
            sb.AppendLine($"                {basis}");
        }
        sb.AppendLine();
        sb.AppendLine($"   INDEPENDENT  ({IndependentSelectors().Length}) : {string.Join(" | ", IndependentSelectors())}");
        sb.AppendLine($"   DERIVED FROM ({DerivedSelectors().Length}) : {string.Join(" | ", DerivedSelectors())}");
        sb.AppendLine($"   REDUNDANT    ({RedundantSelectors().Length}) : {string.Join(" | ", RedundantSelectors())}");
        sb.AppendLine($"   INDEPENDENT ROOT MECHANISMS : {IndependentRootCount()}");
        sb.AppendLine();
        sb.AppendLine("4. THE ACCIDENTS BELONG TO DIFFERENT DIMENSIONS — checked, not assumed");
        sb.AppendLine($"   bivectors collapse to a SCALAR (dim L2 = 1)  : d = {string.Join(", ", DimensionsWhereBivectorsAreScalars())}");
        sb.AppendLine($"   bivectors ARE the vectors  (dim L2 = dim V)  : d = {string.Join(", ", DimensionsWhereBivectorsAreVectors())}");
        sb.AppendLine($"   dim L2 even, so a self-dual split is possible: d = {string.Join(", ", DimensionsAdmittingASelfDualSplit())}");
        sb.AppendLine("   the cross product reappears at d = 7 (octonions; Hurwitz) — cited, not computed here");
        sb.AppendLine("   so the family of special dimensions is real and each membership differs — but the");
        sb.AppendLine("   only membership that selects d = 3 is dim L2 = dim V, and it does so once");
        sb.AppendLine();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
