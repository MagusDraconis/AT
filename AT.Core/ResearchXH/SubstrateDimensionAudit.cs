using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_041 — SUBSTRATE DIMENSION AUDIT.
///
/// QUESTION. AT needs the ring (d = 1) and, as G_033 established, the CUBE (d = 3). What about d = 2 and
/// d = 4 — do they change anything the programme should reckon with, and is d = 3 SELECTED or assumed?
///
/// The family is D96^d: the d-fold tensor product of the ring, i.e. the d-dimensional torus built from the same
/// 96-cell circulant, whose symmetry group is the signed-permutation group B_d = C2^d : S_d of order 2^d * d!
/// (B_3 is the full cubic group O_h of order 48 — the group E_003/E_004/G_033 used). The d = 1 case is not B_1:
/// AT's ring is PERIODIC, so its symmetry is the dihedral group of order 192, whose irreps E_003 computed.
///
/// ANSWER: **BOUNDARY — and the two halves point opposite ways.**
///
///  (1) D96^2 FAILS EXACTLY AS THE RING FAILS — DERIVED, and it strengthens G_033. The maximum irrep dimension
///      is 2 for the ring (dihedral, order 192) and 2 for the square torus (B_2, order 8). A dimension-3 sector
///      needs a dimension-3 irrep, so d = 2 supplies none: the cube is the MINIMAL working dimension, not merely
///      the chosen one.
///
///  (2) BUT THE IRREP-SUPPLY ARGUMENT DOES NOT SELECT d = 3 — REFUTED AS A SELECTOR, and this is the part the
///      programme has been leaning on. Computed character inner products give the SAME signature for every
///      d >= 2: the vector is irreducible (<V,V> = 1), the traceless symmetric rank-2 always splits into exactly
///      TWO irreps (<W,W> = 2, which is E_003's E + T2 at d = 3), and the vector never sits inside the
///      traceless sector (<V,W> = 0). Nothing in that signature distinguishes d = 3 from d = 4. What breaks only
///      at d >= 4 is the property that the vector is the LARGEST irrep: max irrep dimension equals d for
///      d = 1, 2, 3 and then jumps (8 at d = 4, 20 at d = 5).
///
///  (3) WHAT DOES SELECT d = 3 — DERIVED, two independent accidents. The eps/Hodge accident: the antisymmetric
///      rank-2 has the same dimension as the vector ONLY at d = 3 (3 = 3; at d = 2 it is a SCALAR, at d = 4 six,
///      at d = 5 ten) — that is the axial structure T2(3) E_003 and G_033 need. And the polarisation match: the
///      massless photon carries d - 1 and the massless graviton (d+1)(d-2)/2, which are EQUAL only at d = 3
///      (2 and 2). At d = 2 they are 1 and ZERO — no propagating graviton at all — and at d = 4 they are 3 and 5.
///
///  (4) AND d IS OBSERVABLE, NOT BOOKKEEPING — the clock law is rho^(1/d), so the SAME density gives
///      129 415.634 s/day at d = 2, 86 277.089 at d = 3 (the figure G_016b/G_039/G_040 quote) and 64 707.817 at
///      d = 4. Every observable that inherits the clock law inherits the dimension.
///
/// So the value d = 3 is DERIVED by two independent accidents, the exclusion of d = 4 is NOT established by the
/// representation argument, and the choice is observable through the clock. That is a BOUNDARY: a derived value
/// with an open window — the same two-level structure the programme uses elsewhere.
/// </summary>
public static class SubstrateDimensionAudit
{
    /// <summary>Cells in the D96 ring every member of the family is built from.</summary>
    public const int D96Cells = 96;

    /// <summary>How far the ladder is computed (the group order grows as 2^d * d!).</summary>
    public const int MaxDimension = 6;

    /// <summary>The dimension G_033 requires and the clock law uses.</summary>
    public const int RequiredDimension = 3;

    // ═══ §1  THE FAMILY'S SYMMETRY ══════════════════════════════════════════════════════════════

    public static long Factorial(int n)
    {
        long f = 1;
        for (int i = 2; i <= n; i++) f *= i;
        return f;
    }

    public static long GroupOrder(int d) => (1L << d) * Factorial(d);

    /// <summary>Permutations of the d axes.</summary>
    private static int[][] AxisPermutations(int d)
    {
        var list = new List<int[]>();
        var current = Enumerable.Range(0, d).ToArray();
        void Recurse(int k)
        {
            if (k == d) { list.Add((int[])current.Clone()); return; }
            for (int i = k; i < d; i++)
            {
                (current[k], current[i]) = (current[i], current[k]);
                Recurse(k + 1);
                (current[k], current[i]) = (current[i], current[k]);
            }
        }
        Recurse(0);
        return list.ToArray();
    }

    private static int[][] SignVectors(int d)
    {
        var list = new List<int[]>();
        for (int mask = 0; mask < (1 << d); mask++)
        {
            var s = new int[d];
            for (int i = 0; i < d; i++) s[i] = ((mask >> i) & 1) == 0 ? 1 : -1;
            list.Add(s);
        }
        return list.ToArray();
    }

    /// <summary>
    /// The signed-permutation group B_d as monomial integer matrices, flattened row-major. This is the symmetry
    /// of the d-dimensional cubic lattice: reflections in the d coordinate planes composed with permutations of
    /// the axes. B_3 is O_h (order 48) — the group the photon and graviton audits used.
    /// </summary>
    public static int[][] Group(int d)
    {
        var elements = new List<int[]>();
        foreach (var p in AxisPermutations(d))
            foreach (var s in SignVectors(d))
            {
                var m = new int[d * d];
                for (int i = 0; i < d; i++) m[i * d + p[i]] = s[i];
                elements.Add(m);
            }
        return elements.ToArray();
    }

    /// <summary>Distinct elements — the construction must not duplicate.</summary>
    public static int DistinctElements(int d)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var m in Group(d)) seen.Add(string.Join(",", m));
        return seen.Count;
    }

    // ═══ §2  THE IRREP DIMENSION LADDER ═════════════════════════════════════════════════════════

    private static IEnumerable<int[]> Partitions(int n, int max)
    {
        if (n == 0) { yield return Array.Empty<int>(); yield break; }
        for (int k = Math.Min(n, max); k >= 1; k--)
            foreach (var rest in Partitions(n - k, k))
            {
                var part = new int[rest.Length + 1];
                part[0] = k;
                Array.Copy(rest, 0, part, 1, rest.Length);
                yield return part;
            }
    }

    /// <summary>Number of standard Young tableaux of shape lambda, by the hook-length formula.</summary>
    public static long HookLength(int[] shape)
    {
        int n = shape.Sum();
        if (n == 0) return 1;
        long hooks = 1;
        for (int i = 0; i < shape.Length; i++)
            for (int j = 0; j < shape[i]; j++)
            {
                int arm = shape[i] - j - 1;
                int leg = 0;
                for (int r = i + 1; r < shape.Length; r++) if (shape[r] > j) leg++;
                hooks *= arm + leg + 1;
            }
        return Factorial(n) / hooks;
    }

    /// <summary>
    /// The irrep dimensions of B_d, from the pair-of-partitions classification with
    /// dim(lambda, mu) = C(d, |lambda|) * f^lambda * f^mu. The sum of the squares must equal the group order —
    /// and that identity is what <see cref="SpectrumIsExact"/> verifies rather than assumes.
    /// </summary>
    public static long[] IrrepDimensionSpectrum(int d)
    {
        var dims = new List<long>();
        for (int k = 0; k <= d; k++)
            foreach (var lambda in Partitions(k, k))
                foreach (var mu in Partitions(d - k, d - k))
                    dims.Add(Binomial(d, k) * HookLength(lambda) * HookLength(mu));
        return dims.OrderByDescending(x => x).ToArray();
    }

    public static long Binomial(int n, int k)
    {
        long r = 1;
        for (int i = 0; i < k; i++) r = r * (n - i) / (i + 1);
        return r;
    }

    public static long SumOfSquares(int d) => IrrepDimensionSpectrum(d).Sum(x => x * x);

    public static int MaxIrrepDimension(int d) => (int)IrrepDimensionSpectrum(d).Max();

    /// <summary>Does the dimension spectrum satisfy Burnside's sum rule, Sum d_i^2 = |G|?</summary>
    public static bool SpectrumIsExact(int d) => SumOfSquares(d) == GroupOrder(d);

    /// <summary>
    /// The single-ring case. AT's ring is PERIODIC, so its symmetry is the dihedral group of order 192, not B_1:
    /// E_003's budget gives 4 one-dimensional and 47 two-dimensional irreps, i.e. a maximum of 2.
    /// </summary>
    public static int RingMaxIrrepDimension()
        => PhotonOntologyAudit.DihedralIrrepBudget(D96Cells).MaxDim;

    /// <summary>The largest irrep the substrate at dimension d can supply — the ring's own group for d = 1.</summary>
    public static int SuppliedIrrepDimension(int d)
        => d == 1 ? RingMaxIrrepDimension() : MaxIrrepDimension(d);

    /// <summary>Can this dimension supply a 3-dimensional sector at all?</summary>
    public static bool SuppliesThreeDimensionalSector(int d) => SuppliedIrrepDimension(d) >= 3;

    /// <summary>Is the vector the LARGEST irrep of the d-cube's symmetry? (True only for d = 1, 2, 3.)</summary>
    public static bool VectorIsTheLargestIrrep(int d) => d >= 2 && MaxIrrepDimension(d) == d;

    // ═══ §3  THE SECTOR SIGNATURE — character inner products over the group ════════════════════

    private static long Trace(int[] m, int d)
    {
        long t = 0;
        for (int i = 0; i < d; i++) t += m[i * d + i];
        return t;
    }

    private static int[] Multiply(int[] a, int[] b, int d)
    {
        var c = new int[d * d];
        for (int i = 0; i < d; i++)
            for (int j = 0; j < d; j++)
            {
                int s = 0;
                for (int k = 0; k < d; k++) s += a[i * d + k] * b[k * d + j];
                c[i * d + j] = s;
            }
        return c;
    }

    /// <summary>The vector character: the trace of the monomial matrix (= the sum of the signs).</summary>
    public static long VectorCharacter(int[] m, int d) => Trace(m, d);

    /// <summary>
    /// The trace of the action on the TRACELESS symmetric rank-2 tensors: the full symmetric trace is
    /// (chi(g)^2 + chi(g^2))/2 and the invariant trace one dimension is removed.
    /// </summary>
    public static long TracelessCharacter(int[] m, int d)
    {
        long chi = Trace(m, d);
        long chi2 = Trace(Multiply(m, m, d), d);
        return (chi * chi + chi2) / 2 - 1;
    }

    /// <summary>The antisymmetric rank-2 character.</summary>
    public static long AntisymmetricCharacter(int[] m, int d)
    {
        long chi = Trace(m, d);
        long chi2 = Trace(Multiply(m, m, d), d);
        return (chi * chi - chi2) / 2;
    }

    /// <summary>The normalised inner product (1/|G|) Sum_g f(g) g(g) over the group.</summary>
    public static long InnerProduct(Func<int[], int, long> f, Func<int[], int, long> g, int d)
    {
        long sum = 0;
        foreach (var m in Group(d)) sum += f(m, d) * g(m, d);
        return sum / GroupOrder(d);
    }

    /// <summary>Is the vector a SINGLE irrep? (1 everywhere the group acts irreducibly.)</summary>
    public static long VectorSelfInner(int d) => InnerProduct(VectorCharacter, VectorCharacter, d);

    /// <summary>How many irreducible pieces the traceless sector has (Sum m_i^2).</summary>
    public static long TracelessSelfInner(int d) => InnerProduct(TracelessCharacter, TracelessCharacter, d);

    /// <summary>How much of the vector sits inside the traceless sector (must be 0 for separation).</summary>
    public static long VectorInsideTraceless(int d) => InnerProduct(VectorCharacter, TracelessCharacter, d);

    /// <summary>How much of the vector sits inside the axial sector.</summary>
    public static long VectorInsideAntisymmetric(int d) => InnerProduct(VectorCharacter, AntisymmetricCharacter, d);

    /// <summary>
    /// THE NEGATIVE RESULT. The separation signature is DIMENSION-BLIND: for every d >= 2 the vector is
    /// irreducible, the traceless sector splits into exactly two pieces, and the two never mix. Whatever selects
    /// d = 3, it is not this.
    /// </summary>
    public static bool SeparationSignatureIsDimensionBlind()
        => Enumerable.Range(2, MaxDimension - 1).All(d =>
            VectorSelfInner(d) == 1 && TracelessSelfInner(d) == 2
            && VectorInsideTraceless(d) == 0 && VectorInsideAntisymmetric(d) == 0);

    // ═══ §4  THE SELECTORS ══════════════════════════════════════════════════════════════════════

    public static int VectorSpaceDimension(int d) => d;
    public static int TracelessDimension(int d) => d * (d + 1) / 2 - 1;
    public static int AntisymmetricDimension(int d) => d * (d - 1) / 2;

    /// <summary>The eps / Hodge accident: the antisymmetric rank-2 has the vector's dimension.</summary>
    public static bool HodgeDualityHolds(int d) => d >= 2 && AntisymmetricDimension(d) == VectorSpaceDimension(d);

    /// <summary>Massless spin-1 polarisations in d spatial dimensions (D = d + 1 spacetime): D - 2.</summary>
    public static int PhotonPolarisations(int d) => d - 1;

    /// <summary>Massless spin-2 polarisations: D(D - 3)/2 = (d+1)(d-2)/2.</summary>
    public static int GravitonPolarisations(int d) => (d + 1) * (d - 2) / 2;

    /// <summary>Do the photon and the graviton carry the SAME number of physical polarisations?</summary>
    public static bool PolarisationCountsMatch(int d)
        => PhotonPolarisations(d) > 0 && PhotonPolarisations(d) == GravitonPolarisations(d);

    /// <summary>The dimensions (2..MaxDimension) where the Hodge accident holds — computed, and length 1.</summary>
    public static int[] DimensionsWithHodgeDuality()
        => Enumerable.Range(2, MaxDimension - 1).Where(HodgeDualityHolds).ToArray();

    /// <summary>The dimensions where the polarisation counts match — computed, and length 1.</summary>
    public static int[] DimensionsWithMatchingPolarisations()
        => Enumerable.Range(1, MaxDimension).Where(PolarisationCountsMatch).ToArray();

    /// <summary>The dimensions where the vector is the largest irrep — computed, 1, 2 and 3.</summary>
    public static int[] DimensionsWhereVectorIsLargest()
        => Enumerable.Range(2, MaxDimension - 1).Where(VectorIsTheLargestIrrep).ToArray();

    // ═══ §5  THE CONSEQUENCES — this choice is observable ═══════════════════════════════════════

    /// <summary>The clock law of G_016b: dtau/dt = rho^(1/d), so the exponent is the substrate dimension.</summary>
    public static double ClockExponent(int d) => 1.0 / d;

    /// <summary>G_016b's own reference figure: a twenty-fold rho contrast at d = 3.</summary>
    public static double ClockRatePerDay(int d) => Math.Log(20.0) / d * 86400.0;

    public static long ModeCount(int d)
    {
        long n = 1;
        for (int i = 0; i < d; i++) n *= D96Cells;
        return n;
    }

    /// <summary>The published d = 3 clock rate, recomputed here rather than quoted.</summary>
    public static double RequiredDimensionClockRate() => ClockRatePerDay(RequiredDimension);

    /// <summary>Does the same density give a different clock rate at every dimension? (It must.)</summary>
    public static bool TheChoiceIsObservable()
    {
        var rates = new[] { 2, 3, 4 }.Select(ClockRatePerDay).ToArray();
        return Math.Abs(rates[0] - rates[1]) > 1.0 && Math.Abs(rates[1] - rates[2]) > 1.0;
    }

    // ═══ §6  VERDICT AND REPORT ═════════════════════════════════════════════════════════════════

    /// <summary>The candidate selectors of d = 3, with their computed status.</summary>
    public static (string Candidate, string Status, string Basis)[] Selectors() => new[]
    {
        ("irrep dimension 3 is supplied", "DERIVED",
            $"max irrep dim: ring {SuppliedIrrepDimension(1)}, d=2 {SuppliedIrrepDimension(2)}, "
            + $"d=3 {SuppliedIrrepDimension(3)}, d=4 {SuppliedIrrepDimension(4)} -> needs d >= 3"),
        ("...and that SELECTS d = 3", "REFUTED",
            $"it selects d >= 3 only; the separation signature is dimension-blind: "
            + $"<V,V>={VectorSelfInner(3)}, <W,W>={TracelessSelfInner(3)}, <V,W>={VectorInsideTraceless(3)} "
            + $"at d=3 and at d=4 alike ({SeparationSignatureIsDimensionBlind()})"),
        ("the vector is the largest irrep", "DERIVED",
            $"holds for d = {string.Join(", ", DimensionsWhereVectorIsLargest())}, then jumps to "
            + $"{MaxIrrepDimension(4)} at d=4 and {MaxIrrepDimension(5)} at d=5"),
        ("the eps / Hodge accident", "DERIVED",
            $"dim Lambda^2 = dim V at d = {string.Join(", ", DimensionsWithHodgeDuality())} only "
            + $"({AntisymmetricDimension(3)} = {VectorSpaceDimension(3)}); d=2 gives {AntisymmetricDimension(2)} "
            + "(a scalar), d=4 gives " + AntisymmetricDimension(4)),
        ("the polarisation match", "DERIVED",
            $"photon vs graviton equal at d = {string.Join(", ", DimensionsWithMatchingPolarisations())} only "
            + $"(2 and 2); d=2 gives 1 and {GravitonPolarisations(2)}, d=4 gives {PhotonPolarisations(4)} and "
            + $"{GravitonPolarisations(4)}"),
        ("the clock law fixes the exponent", "BOUNDARY",
            $"rho^(1/d) makes the dimension observable: {ClockRatePerDay(2):F3} / "
            + $"{ClockRatePerDay(3):F3} / {ClockRatePerDay(4):F3} s/day at d = 2/3/4"),
        ("d = 4 is excluded", "REFUTED",
            $"not by the representations: <V,V>={VectorSelfInner(4)}, <W,W>={TracelessSelfInner(4)}, "
            + $"<V,W>={VectorInsideTraceless(4)} at d = 4, and a {MaxIrrepDimension(4)}-dimensional irrep exists; "
            + $"the cost is {ModeCount(4):N0} modes against {ModeCount(3):N0}"),
    };

    public static string[] DerivedSelectors()
        => Selectors().Where(s => s.Status == "DERIVED").Select(s => s.Candidate).ToArray();

    public static string[] RefutedSelectors()
        => Selectors().Where(s => s.Status == "REFUTED").Select(s => s.Candidate).ToArray();

    /// <summary>
    /// THE REFINEMENT THIS AUDIT OWES THE NEXT ONE. G_041 called the eps/Hodge accident and the polarisation
    /// match "two independent accidents". G_042 computes them to be ONE condition reached by two derivations —
    /// the identity graviton = dim(so(d)) - 1 makes the polarisation equality identical to dim(Lambda^2) = dim(V)
    /// for EVERY d — so the number of INDEPENDENT selectors of d = 3 is 1, not 2.
    /// </summary>
    public static string RefinementFromG042()
        => "REFINEMENT (G_042 -> G_041): the two accidents below are ONE condition, reached by two derivations. "
         + "The identity graviton polarisations = dim(Lambda^2) - 1 = dim(so(d)) - 1 holds for every d, so the "
         + "polarisation equality IS dim(Lambda^2) = dim(V) identically rather than coincidentally, and the "
         + "number of INDEPENDENT reasons for d = 3 is 1, not 2. G_041's positive results stand (the ladder, the "
         + "minimality of the cube, the open window at d = 4, the BOUNDARY verdict); only the COUNT of "
         + "independent reasons is corrected. See ResearchY-G_042.";

    /// <summary>
    /// BOUNDARY (computed). The floor is recomputed first — Burnside's sum rule for every dimension on the
    /// ladder, the group construction's distinctness, the ring's own dihedral budget, and the character inner
    /// products. On that floor: d = 3 is DERIVED by two independent accidents, the exclusion of d = 4 is NOT
    /// established by the representation argument, and the choice is observable through the clock law.
    /// </summary>
    public static string Verdict()
    {
        bool floor = Enumerable.Range(1, MaxDimension).All(d => SpectrumIsExact(d))
                  && Enumerable.Range(1, MaxDimension).All(d => DistinctElements(d) == GroupOrder(d))
                  && RingMaxIrrepDimension() == 2;
        bool d2Fails = !SuppliesThreeDimensionalSector(2) && !SuppliesThreeDimensionalSector(1);
        bool unique = DimensionsWithHodgeDuality().Length == 1
                   && DimensionsWithHodgeDuality()[0] == RequiredDimension
                   && DimensionsWithMatchingPolarisations().Length == 1
                   && DimensionsWithMatchingPolarisations()[0] == RequiredDimension;
        bool blind = SeparationSignatureIsDimensionBlind();
        bool observable = TheChoiceIsObservable();
        if (!(floor && d2Fails && unique && blind && observable)) return "REFUTED";
        if (DerivedSelectors().Length > 0 && RefutedSelectors().Length > 0) return "BOUNDARY";
        return "DERIVED";
    }

    public static string WhereItStands()
        => "d = 3 IS DERIVED BY TWO INDEPENDENT ACCIDENTS, AND ITS EXCLUSIVITY IS NOT. The audit first "
         + "recomputes its floor: the signed-permutation group B_d is constructed for every d on the ladder and "
         + "checked for distinctness, and the irrep dimension spectrum is derived from the pair-of-partitions "
         + "classification with Burnside's sum rule Sum d_i^2 = |G| verified as an identity for every d rather "
         + "than assumed. The first result strengthens G_033: the maximum irrep dimension is 2 for the ring "
         + "(dihedral, order 192) and 2 for the square torus (B_2, order 8), so D96^2 fails for EXACTLY the "
         + "ring's reason — no dimension-3 irrep — and the cube is the MINIMAL working dimension rather than "
         + "merely the chosen one. The second result cuts the other way, and it is the part the programme has "
         + "been leaning on. The character inner products over the group give the SAME signature at every d >= 2: "
         + "the vector is irreducible (1), the traceless symmetric rank-2 always splits into exactly two pieces "
         + "(2 — E_003's E plus T2 at d = 3), and the vector never sits inside the traceless sector (0). Nothing "
         + "there distinguishes d = 3 from d = 4, so the claim that the irrep-supply argument SELECTS d = 3 is "
         + "refuted as a selector: it selects d >= 3. What breaks only later is that the vector stops being the "
         + "largest irrep — max irrep dimension equals d for d = 1, 2 and 3, then jumps to 8 at d = 4 and 20 at "
         + "d = 5. Two independent accidents do pin d = 3, and the audit computes both: the eps/Hodge accident, "
         + "where the antisymmetric rank-2 has the vector's own dimension only at d = 3 (at d = 2 it collapses "
         + "to a scalar and at d = 4 it becomes six), and the polarisation match, where the massless photon "
         + "carries d - 1 and the massless graviton (d+1)(d-2)/2 and the two are equal only at d = 3 — 2 and 2 — "
         + "against 1 and zero at d = 2, which is to say no propagating graviton at all, and 3 and 5 at d = 4. "
         + "AND THEN THE PART THAT MAKES THIS MORE THAN BOOKKEEPING: the clock law is rho^(1/d), so the same "
         + "density gives 129 415.634, 86 277.089 and 64 707.817 seconds per day at d = 2, 3 and 4. The figure "
         + "G_016b, G_039 and G_040 quote is a d = 3 number, and every observable that inherits the clock law "
         + "inherits the dimension. So the answer is a BOUNDARY of the same two-level shape the programme uses "
         + "elsewhere: a DERIVED value (d = 3, pinned by Hodge duality and by the polarisation match, with d = 2 "
         + "excluded outright because it cannot host the sector at all), an OPEN window (d = 4 is not excluded by "
         + "the representation theory, only by the two accidents and by cost — 84 934 656 modes against "
         + "884 736), and a consequence that is falsifiable rather than conventional.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputLadder()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE SUBSTRATE LADDER");
        sb.AppendLine("   d   group              order   #irreps   max irrep dim  3-dim sector?   vector is largest?");
        sb.AppendLine($"   1   D96 (ring, periodic)  {192,6}       {4 + 47,2}   {RingMaxIrrepDimension(),12}   "
                      + $"{"no",14}   {"n/a",18}");
        for (int d = 2; d <= MaxDimension; d++)
        {
            var spec = IrrepDimensionSpectrum(d);
            sb.AppendLine($"   {d,2}   B_{d} (signed perms)  {GroupOrder(d),6}   {spec.Length,7}   "
                          + $"{MaxIrrepDimension(d),12}   {(SuppliesThreeDimensionalSector(d) ? "yes" : "no"),14}   "
                          + $"{(VectorIsTheLargestIrrep(d) ? "yes" : "no"),18}");
        }
        sb.AppendLine($"   Burnside Sum d^2 = |G| verified for every d : "
                      + $"{Enumerable.Range(1, MaxDimension).All(SpectrumIsExact)}");
        sb.AppendLine($"   irrep dimension spectra : " + string.Join("; ", Enumerable.Range(2, MaxDimension - 1)
            .Select(d => $"d={d}: {{{string.Join(",", IrrepDimensionSpectrum(d).Distinct().OrderBy(x => x))}}}")));
        return sb.ToString();
    }

    public static string OutputSignature()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. THE SECTOR SIGNATURE — the NEGATIVE result");
        sb.AppendLine("   d   <V,V>   <W,W>   <V,W>   <V,A>   V dim   W dim   A dim");
        for (int d = 2; d <= MaxDimension; d++)
            sb.AppendLine($"   {d,2}   {VectorSelfInner(d),5}   {TracelessSelfInner(d),5}   "
                          + $"{VectorInsideTraceless(d),5}   {VectorInsideAntisymmetric(d),5}   "
                          + $"{VectorSpaceDimension(d),5}   {TracelessDimension(d),5}   {AntisymmetricDimension(d),5}");
        sb.AppendLine($"   DIMENSION-BLIND                        : {SeparationSignatureIsDimensionBlind()}");
        sb.AppendLine("   the vector is irreducible, the traceless sector always splits into exactly two pieces,");
        sb.AppendLine("   and the two never mix - at EVERY d >= 2. So this signature cannot select d = 3.");
        return sb.ToString();
    }

    public static string OutputSelectors()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE SELECTORS OF d = 3");
        foreach (var (candidate, status, basis) in Selectors())
        {
            sb.AppendLine($"   {status,-9} {candidate}");
            sb.AppendLine($"             {basis}");
        }
        sb.AppendLine($"   Hodge duality holds only at  : d = {string.Join(", ", DimensionsWithHodgeDuality())}");
        sb.AppendLine($"   polarisation match only at   : d = {string.Join(", ", DimensionsWithMatchingPolarisations())}");
        sb.AppendLine($"   vector the largest irrep     : d = {string.Join(", ", DimensionsWhereVectorIsLargest())}");
        return sb.ToString();
    }

    public static string OutputImpact()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE CONSEQUENCES — the choice is OBSERVABLE, not conventional");
        sb.AppendLine("   d   clock exponent   clock rate (s/day)   modes");
        foreach (int d in new[] { 2, 3, 4 })
            sb.AppendLine($"   {d,2}   {ClockExponent(d),14:F6}   {ClockRatePerDay(d),17:F3}   {ModeCount(d),12:N0}");
        sb.AppendLine($"   the published d = 3 figure recomputed : {RequiredDimensionClockRate():F3} s/day");
        sb.AppendLine($"   (G_016b/G_039/G_040 quote 86 277.089)  : "
                      + $"{Math.Abs(RequiredDimensionClockRate() - 86277.089) < 1e-3}");
        sb.AppendLine($"   the same density gives a different rate at every d : {TheChoiceIsObservable()}");
        sb.AppendLine();
        sb.AppendLine("5. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
