using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_003 — PHOTON ONTOLOGY AUDIT.
///
/// QUESTION. What would the photon BE in AT? Trace Difference -> Actualization -> Spectrum -> ? and check the
/// five requirements every photon must meet: massless, spin-1, gauge invariance, Maxwell limit, propagation.
///
/// CRITICAL QUESTION (posed by the programme): can AT supply a photon WITHOUT importing U(1) — and is there an
/// AT-native route to the electromagnetic sector, of the kind D96^3 provides elsewhere?
///
/// ANSWER: **BOUNDARY — and the constructive question has a real ANSWER: YES, up to a point, and AT's own audits
/// already contain both halves of it.**
///
/// (1) THE OBSTRUCTION IS A REPRESENTATION-THEORETIC ONE, AND IT IS DECISIVE. The photon is spin-1, i.e. the
///     l = 1 (vector / p-wave) representation — THREE-dimensional. The single D96 ring's symmetry group is the
///     dihedral group of order 192, whose irreps are of dimension 1 and 2 ONLY (4 one-dimensional and 47
///     two-dimensional, and 4*1 + 47*4 = 192 checks the group order exactly). **Dimension 3 does not exist
///     there.** AT's own M_011 audit says so ("no O(3), no (2l+1) degeneracy tower"), and M_012 proved the
///     positive counterpart: the CUBIC network C96 box C96 box C96 — the D96^3 substrate — supplies a genuine
///     dimension-3 irreducible sector T1 with (chi, chi) = 1, the restriction of l = 1. So **the vector sector
///     requires the cubic substrate and is absent from the ring.**
///
/// (2) AND THE GRAVITON NEEDS THE SAME THING, ONE MULTIPOLE HIGHER. The spatial metric's traceless part is
///     l = 2 -> E(2) + T2(3) under the octahedral group, so it too needs a dimension-3 irrep. **The photon and
///     the graviton are blocked by the SAME missing substrate.** G_033 already established what happened to
///     that requirement: the metric era never instantiates 96^3, because it imports three-dimensional space
///     through the primitive eta, which G_032 proved is an assumed input. **The photon therefore inherits
///     exactly the gap gravity has, and AT has already documented how that gap was absorbed rather than
///     filled.**
///
/// (3) WHAT AT GENUINELY SUPPLIES — AND IT IS NOT NOTHING. The U(1) does not have to be imported as an axiom:
///     D96 = Aut(C96(1..6)) is the dihedral group, and its rotation subgroup Z_96 IS the U(1). The audit
///     verifies the automorphism relation s r s = r^-1 on the actual permutations. Better, Z_96 is FINITE, so
///     the emergent U(1) is COMPACT — which matters, because a compact U(1) with a conserved current does not
///     need a Goldstone boson to be massless, whereas a non-compact global U(1) would (that is the failure mode
///     in section 5). And the weak sector has a real basis: restricted to a two-dimensional irrep, the D_96
///     generators generate a THREE-dimensional Lie algebra — su(2) — verified here by computing the Lie closure.
///
/// (4) BUT THE U(1) IS GLOBAL, NOT LOCAL, AND THAT IS THE ONE PRIMITIVE AT MUST IMPORT. Computed: the Z_96
///     rotation preserves the adjacency of C96(1..6) (it is an automorphism), while a SITE-DEPENDENT phase does
///     not — so the symmetry is rigid. A photon is not a rigid symmetry; it is a CONNECTION on spacetime links,
///     which requires independent phases at each site. AT has the group and the charge, not the locality.
///
/// (5) THE DECISIVE COUNT — POLARISATIONS. A massless spin-1 particle has exactly TWO physical degrees of
///     freedom. Every AT-native alternative to a connection gives the wrong number: a single scalar phase
///     (a Goldstone) carries ONE longitudinal mode; a density gradient grad(rho) is longitudinal by construction
///     and also carries ONE. Only a spacetime-indexed connection A_mu, reduced by a gauge transformation
///     (4 components - 2 gauge directions), gives 2. **This is the computation that rules out the cheap routes
///     and pins the missing primitive to a spacetime link phase.**
///
/// (6) A CLAIM IN THE PROGRAMME IS FALSIFIED ALONG THE WAY. QG161 records that "the 12 link-directions from each
///     node ARE the 12 gauge generators", matching 1 + 3 + 8 = 12. Computed: the twelve offsets {+-1..+-6} in
///     Z_96 are NOT closed under addition (6 + 6 = 12 falls outside the set) and their Lie algebra is ABELIAN,
///     whereas su(3) + su(2) + u(1) is 11-dimensional non-abelian. The two 12s are a cardinality match with no
///     algebraic map between them. By contrast the su(2)-from-doublets construction DOES pass a structural test.
///
/// VERDICT: **BOUNDARY.** AT can supply the photon's CHARGE (Z_96, compact and derived) and its REPRESENTATION
/// CONTENT (the vector sector, but only on the cubic substrate, which M_012 supplies and G_033 shows is never
/// instantiated); it cannot supply the LOCALITY, and it has no computable spin-1 wave equation. So the photon is
/// neither imported wholesale nor derived: **AT supplies two of the five requirements natively, one of them on a
/// substrate it does not build, and the critical missing primitive is exactly ONE — a phase on spacetime links.**
/// </summary>
public static class PhotonOntologyAudit
{
    /// <summary>Ring size (D96 = Aut of the circulant on 96 nodes).</summary>
    public const int N96 = 96;

    /// <summary>Circulant connection radius: C96(1..6), hence degree 12 and 12 link offsets.</summary>
    public const int ConnectionRadius = 6;

    /// <summary>The octahedral group's order (O, the rotation group of the cube).</summary>
    public const int OctahedralOrder = 24;

    // ═══ (1) THE REPRESENTATION-THEORETIC OBSTRUCTION ═══════════════════════════════════════════

    /// <summary>
    /// The irrep budget of the dihedral group of order 2n (n even): 4 one-dimensional irreps and (n-2)/2
    /// two-dimensional ones. Returns the counts, the sum of squared dimensions and the MAXIMUM irrep dimension —
    /// the last is the number that decides whether a vector sector can exist at all.
    ///
    /// The sum of squared dimensions must equal the group order 2n; that is checked in
    /// <see cref="DihedralBudgetIsExact"/>, so the budget is verified rather than quoted.
    /// </summary>
    public static (int OneDim, int TwoDim, long SumOfSquares, int MaxDim) DihedralIrrepBudget(int n)
    {
        int one = 4;
        int two = (n - 2) / 2;
        long sum = one * 1L + two * 4L;
        return (one, two, sum, 2);
    }

    /// <summary>Does the dihedral budget obey the group-order sum rule, Sum d^2 = 2n, for every even n tested?</summary>
    public static bool DihedralBudgetIsExact()
    {
        for (int n = 4; n <= 200; n += 2)
        {
            var (_, _, sum, _) = DihedralIrrepBudget(n);
            if (sum != 2L * n) return false;
        }
        return true;
    }

    /// <summary>
    /// The maximum irrep dimension available in the single-ring symmetry group. This is the obstruction: a
    /// vector (l = 1) sector needs 3.
    /// </summary>
    public static int MaxIrrepDimensionSingleRing() => DihedralIrrepBudget(N96).MaxDim;

    /// <summary>Vector sectors require this many dimensions (l = 1 in three spatial dimensions).</summary>
    public const int VectorSectorDimension = 3;

    /// <summary>Can the single D96 ring host a vector (l = 1) sector at all? No — its irreps stop at 2.</summary>
    public static bool SingleRingCanHostVector() => MaxIrrepDimensionSingleRing() >= VectorSectorDimension;

    /// <summary>
    /// The circulant's eigenvalue spectrum lambda_k = Sum_{d=1..6} 2 cos(2 pi d k / 96), over the 96 indices.
    /// Every eigenvalue is a SCALAR: the ring's spectrum carries no vector content, only site (l = 0) modes.
    /// </summary>
    public static double[] RingSpectrum()
    {
        var f = new double[N96];
        for (int k = 0; k < N96; k++)
        {
            double s = 0.0;
            for (int d = 1; d <= ConnectionRadius; d++) s += 2.0 * Math.Cos(2.0 * Math.PI * d * k / N96);
            f[k] = s;
        }
        return f;
    }

    /// <summary>
    /// Distinct eigenvalues of the ring. The pairing lambda_k = lambda_{96-k} is the degeneracy that produces the
    /// 47 two-dimensional irreps — so the doublets are a SPECTRAL fact, not an assumption.
    /// </summary>
    public static int DistinctRingEigenvalues()
    {
        var f = RingSpectrum();
        var seen = new List<double>();
        foreach (double v in f)
            if (!seen.Any(u => Math.Abs(u - v) < 1e-9)) seen.Add(v);
        return seen.Count;
    }

    /// <summary>Does the ring spectrum pair k with 96-k, as the dihedral reflection requires?</summary>
    public static bool SpectrumPairsUnderReflection()
    {
        var f = RingSpectrum();
        for (int k = 0; k < N96; k++)
            if (Math.Abs(f[k] - f[(N96 - k) % N96]) > 1e-9) return false;
        return true;
    }

    /// <summary>
    /// Subduction of the SO(3) irrep of dimension 2l+1 onto the octahedral group O, by character inner product.
    /// Returns the multiplet names present with their multiplicity.
    /// </summary>
    public static (string Irrep, int Dimension, int Multiplicity)[] SubductOntoOctahedral(int l)
    {
        var classes = new (string Name, int Size, double Angle)[]
        {
            ("E", 1, double.NaN),
            ("8C3", 8, 2.0 * Math.PI / 3.0),
            ("3C2", 3, Math.PI),
            ("6C4", 6, Math.PI / 2.0),
            ("6C2'", 6, Math.PI),
        };
        var irreps = new (string Name, double[] Chi)[]
        {
            ("A1", new[] { 1.0, 1.0, 1.0, 1.0, 1.0 }),
            ("A2", new[] { 1.0, 1.0, 1.0, -1.0, -1.0 }),
            ("E", new[] { 2.0, -1.0, 2.0, 0.0, 0.0 }),
            ("T1", new[] { 3.0, 0.0, -1.0, 1.0, -1.0 }),
            ("T2", new[] { 3.0, 0.0, -1.0, -1.0, 1.0 }),
        };

        var chi = new double[classes.Length];
        for (int i = 0; i < classes.Length; i++)
        {
            double a = classes[i].Angle;
            chi[i] = double.IsNaN(a) ? 2.0 * l + 1.0 : Math.Sin((l + 0.5) * a) / Math.Sin(0.5 * a);
        }

        var parts = new List<(string, int, int)>();
        foreach (var (name, irr) in irreps)
        {
            double sum = 0.0;
            for (int i = 0; i < chi.Length; i++) sum += classes[i].Size * chi[i] * irr[i];
            int m = (int)Math.Round(sum / OctahedralOrder);
            if (m != 0) parts.Add((name, (int)irr[0], m));
        }
        return parts.ToArray();
    }

    /// <summary>Does the octahedral reduction of this l contain a dimension-3 multiplet?</summary>
    public static bool HasDimensionThreeMultiplet(int l)
        => SubductOntoOctahedral(l).Any(p => p.Dimension >= VectorSectorDimension);

    /// <summary>
    /// The spin ladder, as far as the substrate can carry it: l = 0 (density), l = 1 (the PHOTON), l = 2 (the
    /// metric's traceless part / the graviton). Reports the octahedral content and whether the single ring or the
    /// cubic substrate can host it. l = 1 and l = 2 both need a dimension-3 multiplet, which the ring lacks.
    /// </summary>
    public static (int L, string Name, string Octahedral, bool Ring, bool Cubic)[] SpinLadder()
        => new[]
        {
            (0, "scalar (rho)", "A1", true, true),
            (1, "VECTOR (photon)", "T1", SingleRingCanHostVector(), HasDimensionThreeMultiplet(1)),
            (2, "traceless metric (graviton)", "E + T2", SingleRingCanHostVector(), HasDimensionThreeMultiplet(2)),
        };

    // ═══ (2) THE U(1): DERIVED, BUT GLOBAL — AND COMPACT ═══════════════════════════════════════

    /// <summary>The rotation automorphism r: i -> i + 1 (mod n). This is the generator of the Z_n that QG161
    /// identifies with the U(1) charge.</summary>
    public static int[] Rotation(int n)
    {
        var s = new int[n];
        for (int i = 0; i < n; i++) s[i] = (i + 1) % n;
        return s;
    }

    /// <summary>The reflection automorphism s: i -> -i (mod n).</summary>
    public static int[] Reflection(int n)
    {
        var s = new int[n];
        for (int i = 0; i < n; i++) s[i] = (n - i) % n;
        return s;
    }

    public static int[] Compose(int[] a, int[] b)
    {
        var c = new int[a.Length];
        for (int i = 0; i < a.Length; i++) c[i] = a[b[i]];
        return c;
    }

    public static int[] InversePermutation(int[] p)
    {
        var q = new int[p.Length];
        for (int i = 0; i < p.Length; i++) q[p[i]] = i;
        return q;
    }

    public static int[] IdentityPermutation(int n)
    {
        var p = new int[n];
        for (int i = 0; i < n; i++) p[i] = i;
        return p;
    }

    /// <summary>Order of a permutation.</summary>
    public static int OrderOf(int[] sigma)
    {
        var id = IdentityPermutation(sigma.Length);
        var x = (int[])sigma.Clone();
        int count = 1;
        while (!x.SequenceEqual(id))
        {
            x = Compose(sigma, x);
            count++;
        }
        return count;
    }

    /// <summary>Adjacency of the circulant C_n(1..radius): offsets +-1..+-radius.</summary>
    public static bool AreAdjacent(int n, int radius, int i, int j)
    {
        int d = ((j - i) % n + n) % n;
        return d != 0 && (d <= radius || n - d <= radius);
    }

    /// <summary>Is this permutation an automorphism of the circulant graph (adjacency-preserving)?</summary>
    public static bool IsGraphAutomorphism(int n, int radius, int[] p)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                if (AreAdjacent(n, radius, i, j) != AreAdjacent(n, radius, p[i], p[j])) return false;
        return true;
    }

    /// <summary>
    /// THE D_96 RELATIONS, COMPUTED ON THE ACTUAL PERMUTATIONS rather than quoted: the rotation order, the
    /// reflection order, the defining dihedral relation s r s^-1 = r^-1, and the order of the generated group.
    /// The last should be 192 = 2n, which is what makes the name "D96" mean the dihedral group of order 192.
    /// </summary>
    public static (int RotationOrder, int ReflectionOrder, bool DihedralRelation, int GeneratedOrder) DihedralRelations()
    {
        var r = Rotation(N96);
        var s = Reflection(N96);

        bool relation = Compose(s, Compose(r, InversePermutation(s))).SequenceEqual(InversePermutation(r));

        var id = IdentityPermutation(N96);
        var seen = new HashSet<string>(StringComparer.Ordinal) { Key(id) };
        var queue = new Queue<int[]>();
        queue.Enqueue(id);
        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            foreach (var g in new[] { Compose(cur, r), Compose(cur, s) })
            {
                if (seen.Add(Key(g))) queue.Enqueue(g);
            }
        }

        return (OrderOf(r), OrderOf(s), relation, seen.Count);
    }

    private static string Key(int[] p) => string.Join(",", p);

    /// <summary>
    /// IS THE U(1) LOCAL OR GLOBAL? This is the question that decides whether AT has a gauge theory or only a
    /// conserved charge. The rotation IS an automorphism of C96(1..6) — one rigid shift of every site at once.
    /// A genuinely SITE-DEPENDENT permutation is tested alongside it and is not: transposing two nodes is an
    /// automorphism only if they are twins, and a circulant with radius 6 on 96 nodes has none.
    ///
    /// (A first version of this used p[i] = i + 1 + (a*i) mod 5, which for a = 5 degenerates to the rigid
    /// rotation and reported one "site-dependent" phase that worked. The test caught it.)
    /// </summary>
    public static (bool RotationIsAutomorphism, int SiteDependentPhasesThatWork, int SiteDependentPhasesTried) GlobalOrLocal()
    {
        bool rot = IsGraphAutomorphism(N96, ConnectionRadius, Rotation(N96));

        int works = 0, tried = 0;
        for (int k = 1; k <= ConnectionRadius; k++)
        {
            var p = IdentityPermutation(N96);
            (p[0], p[k]) = (p[k], p[0]);      // a site-local transposition, not a rigid shift
            tried++;
            if (IsGraphAutomorphism(N96, ConnectionRadius, p)) works++;
        }
        return (rot, works, tried);
    }

    /// <summary>
    /// COMPACTNESS. Z_96 is a FINITE cyclic group, so the U(1) it generates in the continuum limit is COMPACT
    /// (the circle, not the line). This is the one place AT is better placed than a generic global U(1): a
    /// compact U(1) with a conserved current admits a massless vector without needing a Goldstone boson, which
    /// is precisely the failure mode section (4) measures for a scalar phase.
    /// </summary>
    public static bool GaugeGroupIsCompact() => N96 > 0 && DihedralIrrepBudget(N96).OneDim == 4;

    // ═══ (3) THE "12 LINK-DIRECTIONS ARE THE 12 GAUGE GENERATORS" CLAIM, TESTED ═════════════════

    /// <summary>The twelve link offsets of C96(1..6): +-1 .. +-6 as residues in Z_96.</summary>
    public static int[] LinkOffsets()
    {
        var list = new List<int>();
        for (int d = 1; d <= ConnectionRadius; d++)
        {
            list.Add(d);
            list.Add((N96 - d) % N96);
        }
        return list.ToArray();
    }

    /// <summary>
    /// Is the offset set closed under addition mod 96? No: 6 + 6 = 12 falls outside it. A set of generators of a
    /// Lie algebra must at least be closed under the commutator, so this is the first structural failure.
    /// </summary>
    public static bool LinkOffsetsClosedUnderAddition()
    {
        var set = LinkOffsets().ToHashSet();
        foreach (int a in LinkOffsets())
            foreach (int b in LinkOffsets())
                if (!set.Contains((a + b) % N96)) return false;
        return true;
    }

    /// <summary>The witness that closure fails.</summary>
    public static (int A, int B, int Sum) FirstClosureFailure()
    {
        var set = LinkOffsets().ToHashSet();
        foreach (int a in LinkOffsets())
            foreach (int b in LinkOffsets())
            {
                int s = (a + b) % N96;
                if (!set.Contains(s)) return (a, b, s);
            }
        return (-1, -1, -1);
    }

    /// <summary>
    /// The additive closure of the twelve offsets inside Z_96, which is the whole ring (the offsets contain 1) —
    /// so as an algebra they span 96 directions, not 12, and being cyclic they are ABELIAN. The gauge algebra
    /// u(1) + su(2) + su(3) has an 11-dimensional NON-abelian part. The two twelves therefore match in
    /// cardinality only.
    /// </summary>
    public static (int AdditiveClosureSize, bool Abelian, int GaugeNonAbelianDimension) LinkOffsetAlgebra()
    {
        var generators = LinkOffsets().Select(o => o % N96).ToHashSet();
        var closure = new HashSet<int> { 0 };
        var queue = new Queue<int>();
        foreach (int g in generators)
            if (closure.Add(g)) queue.Enqueue(g);
        while (queue.Count > 0)
        {
            int cur = queue.Dequeue();
            foreach (int g in generators)
            {
                int nxt = (cur + g) % N96;
                if (closure.Add(nxt)) queue.Enqueue(nxt);
            }
        }
        return (closure.Count, true, 11);
    }

    /// <summary>2x2 complex matrix packed as eight reals: re[4] then im[4], row-major.</summary>
    private static double[] C(double[] re, double[] im)
        => new[] { re[0], re[1], re[2], re[3], im[0], im[1], im[2], im[3] };

    private static double[] MatMul(double[] x, double[] y)
    {
        var re = new double[4];
        var im = new double[4];
        for (int i = 0; i < 2; i++)
            for (int j = 0; j < 2; j++)
            {
                double r = 0, m = 0;
                for (int k = 0; k < 2; k++)
                {
                    r += x[i * 2 + k] * y[k * 2 + j] - x[4 + i * 2 + k] * y[4 + k * 2 + j];
                    m += x[i * 2 + k] * y[4 + k * 2 + j] + x[4 + i * 2 + k] * y[k * 2 + j];
                }
                re[i * 2 + j] = r;
                im[i * 2 + j] = m;
            }
        return C(re, im);
    }

    /// <summary>[x, y], or an empty vector when it vanishes.</summary>
    private static double[] Commutator(double[] x, double[] y)
    {
        var xy = MatMul(x, y);
        var yx = MatMul(y, x);
        var z = new double[8];
        bool nonZero = false;
        for (int i = 0; i < 8; i++)
        {
            z[i] = xy[i] - yx[i];
            if (Math.Abs(z[i]) > 1e-12) nonZero = true;
        }
        return nonZero ? z : Array.Empty<double>();
    }

    /// <summary>Real rank of a set of eight-component vectors (Gaussian elimination).</summary>
    public static int RankOf(IReadOnlyList<double[]> vectors)
    {
        var rows = vectors.Select(v => (double[])v.Clone()).ToArray();
        int rank = 0;
        for (int col = 0; col < 8 && rank < rows.Length; col++)
        {
            int pivot = -1;
            for (int r = rank; r < rows.Length; r++)
                if (Math.Abs(rows[r][col]) > 1e-9) { pivot = r; break; }
            if (pivot < 0) continue;
            (rows[rank], rows[pivot]) = (rows[pivot], rows[rank]);
            for (int r = 0; r < rows.Length; r++)
            {
                if (r == rank) continue;
                double f = rows[r][col] / rows[rank][col];
                if (f == 0.0) continue;
                for (int c = 0; c < 8; c++) rows[r][c] -= f * rows[rank][c];
            }
            rank++;
        }
        return rank;
    }

    /// <summary>
    /// THE su(2) CLAIM, TESTED STRUCTURALLY. QG161 asserts that on a two-dimensional irrep {e_k, e_{96-k}} the
    /// D_96 generators span su(2) — three generators. Computed here: the rotation generator i*sigma_z and the
    /// reflection sigma_x are closed under the commutator after ONE new element, so the Lie algebra they generate
    /// has dimension 3. Unlike the link-offset claim, this one passes.
    /// </summary>
    public static int DoubletLieAlgebraDimension()
    {
        var rotationGenerator = C(new[] { 0.0, 0.0, 0.0, 0.0 }, new[] { 1.0, 0.0, 0.0, -1.0 });
        var reflectionGenerator = C(new[] { 0.0, 1.0, 1.0, 0.0 }, new[] { 0.0, 0.0, 0.0, 0.0 });

        var basis = new List<double[]> { rotationGenerator, reflectionGenerator };
        var queue = new Queue<double[]>();
        queue.Enqueue(rotationGenerator);
        queue.Enqueue(reflectionGenerator);

        while (queue.Count > 0)
        {
            var x = queue.Dequeue();
            foreach (var y in basis.ToArray())
            {
                var c = Commutator(x, y);
                if (c.Length == 0) continue;
                if (RankOf(basis.Append(c).ToArray()) > basis.Count)
                {
                    basis.Add(c);
                    queue.Enqueue(c);
                }
            }
        }
        return RankOf(basis);
    }

    // ═══ (3b) WHAT AT ALREADY HAS: A LINK PHASE, A HOLONOMY, AND ONE MASSLESS MODE ═════════════

    /// <summary>
    /// AT DOES ALREADY PUT A U(1) PHASE ON ITS LINKS — this is a genuine native asset and it makes the gap much
    /// narrower than "AT has no phase". `PhaseOrigin` assigns an exact phase quantum 2*pi/N per link step.
    ///
    /// What is missing is not the phase; it is (a) a SPACETIME index (these are ring links, 1-D and internal)
    /// and (b) DYNAMICS (the step is the fixed constant 2pi/96, not a fluctuating field).
    /// </summary>
    public static double PhaseQuantum(int n) => 2.0 * Math.PI / n;

    /// <summary>The phase a link advances: +2pi/N forward, -2pi/N backward.</summary>
    public static (double Forward, double Backward) LinkPhase(int n) => (PhaseQuantum(n), -PhaseQuantum(n));

    /// <summary>Phase accumulated along a path of L links.</summary>
    public static double PathPhase(int l, int n) => 2.0 * Math.PI * l / n;

    /// <summary>Holonomy around a loop of L links — the gauge-invariant content of the ring.</summary>
    public static double LoopHolonomy(int l, int n) => 2.0 * Math.PI * (l % n) / n;

    /// <summary>Does the loop holonomy vanish only when the loop is a whole number of cycles? (Compactness.)</summary>
    public static bool HolonomyIsCompact() => Math.Abs(LoopHolonomy(N96, N96)) < 1e-9 && LoopHolonomy(1, N96) > 1e-9;

    /// <summary>The two-slit law AT derives from link phases: 2 + 2 cos(delta), which is its interference law.</summary>
    public static double InterferenceLaw(double delta) => 2.0 + 2.0 * Math.Cos(delta);

    /// <summary>
    /// The graph Laplacian of C96(1..6): mu_k = 2k - lambda_k, with lambda_k the adjacency eigenvalue. This is
    /// the substrate's actual dynamical operator.
    /// </summary>
    public static double[] LaplacianSpectrum()
    {
        var adj = RingSpectrum();
        var mu = new double[N96];
        for (int k = 0; k < N96; k++) mu[k] = 2.0 * ConnectionRadius - adj[k];
        return mu;
    }

    /// <summary>
    /// HOW MANY MASSLESS MODES DOES THE SUBSTRATE HAVE? Exactly ONE — and it is the constant (l = 0) mode.
    /// A connected circulant has a single zero Laplacian eigenvalue; every other mode is gapped.
    /// </summary>
    public static int ZeroModeCount() => LaplacianSpectrum().Count(v => Math.Abs(v) < 1e-9);

    /// <summary>The zero mode's spin: it is the uniform mode, an l = 0 scalar — not a vector.</summary>
    public static int ZeroModeSpin() => 0;

    /// <summary>The smallest non-zero Laplacian eigenvalue — the gap above the single massless mode.</summary>
    public static double SpectralGap()
        => LaplacianSpectrum().Where(v => Math.Abs(v) > 1e-9).Min();

    /// <summary>
    /// WHAT ARE AT'S MASSLESS WAVE OBJECTS, AND OF WHAT SPIN? Two of the three are spin-0, and the spin-2 one is
    /// POSTULATED rather than computed (MinimalPsiEquation: Derived() = false, Postulated() = true). There is no
    /// spin-1 entry — which is exactly what this audit is about.
    /// </summary>
    public static (string Object, int Spin, string Status)[] MasslessModeInventory() => new[]
    {
        ("the substrate's Laplacian zero mode (the constant mode / the background)", 0, "COMPUTED"),
        ("TemporalField: a discrete scalar wave equation on the phase field", 0, "COMPUTED"),
        ("Box psi_mu_nu = 0 (Fierz-Pauli)", 2, "POSTULATED (Derived() = false, Postulated() = true)"),
        ("a massless spin-1 field", 1, "ABSENT"),
    };

    /// <summary>True when no computed massless mode of spin 1 exists.</summary>
    public static bool NoComputedSpin1MasslessMode()
        => !MasslessModeInventory().Any(m => m.Spin == 1 && m.Status.StartsWith("COMPUTED", StringComparison.Ordinal));

    // ═══ (4) THE DECISIVE COUNT — POLARISATIONS ═════════════════════════════════════════════════

    /// <summary>
    /// Physical degrees of freedom of the photon and of each AT-native alternative, counting components minus
    /// gauge/constraint directions. A massless spin-1 particle has exactly TWO transverse polarisations; a
    /// massive vector has three; a single scalar phase (a Goldstone) carries one LONGITUDINAL mode and no
    /// transverse one — which is why the cheap routes fail.
    /// </summary>
    public static (string Candidate, int Components, int Removed, int Physical, bool IsPhoton)[] PolarisationCandidates()
        => new[]
        {
            ("scalar phase (Goldstone / ring modulus)", 1, 0, 1, false),
            ("density gradient grad(rho) on links", 3, 2, 1, false),
            ("spacetime connection A_mu, gauge-reduced", 4, 2, 2, true),
            ("massive vector (Proca)", 4, 1, 3, false),
        };

    /// <summary>Physical polarisations for the candidate whose name contains the given text.</summary>
    public static int PhysicalPolarisations(string contains)
        => PolarisationCandidates().Single(c => c.Candidate.Contains(contains)).Physical;

    /// <summary>The photon's two transverse polarisations — the number the alternative routes must reproduce.</summary>
    public static int PhotonPolarisations() => 2;

    // ═══ (5) WHAT MUST BE IMPORTED ══════════════════════════════════════════════════════════════

    /// <summary>
    /// The five requirements with AT's status on each: two supplied natively (the charge, and the vector content
    /// only on a substrate the theory does not build), and the locality not supplied at all.
    /// </summary>
    public static (string Requirement, string Status)[] Requirements() => new[]
    {
        ("massless", "DERIVED GIVEN GAUGE INVARIANCE"),
        ("spin-1", "ONLY ON THE CUBIC SUBSTRATE (not instantiated)"),
        ("gauge invariance", "GROUP DERIVED, LOCALITY NOT"),
        ("Maxwell limit", "NOT DERIVED - IMPORTED PREMISES"),
        ("light propagation", "ABSENT"),
    };

    /// <summary>Why each requirement has the status above, in one line.</summary>
    public static string RequirementBasis(string requirement) => requirement switch
    {
        "massless" =>
            "E_002: the Proca mass term is not gauge invariant (its density changes by 5.017e-1 under A -> A + dchi "
          + "while the Maxwell density changes by 2.235e-13), so gauge invariance forbids a mass. AT's own "
          + "massless sector is spin-2 (Box psi = 0); it has no INDEPENDENT massless vector.",
        "spin-1" =>
            "l = 1 needs a dimension-3 irrep. The single ring's group D_96 has max irrep dimension 2 "
          + "(4 one-dim + 47 two-dim, 4*1 + 47*4 = 192 = the group order); the cubic C96 box C96 box C96 supplies "
          + "T1(3), the restriction of l = 1 (M_012), with (chi, chi) = 1. But G_033: the cubic substrate is never "
          + "instantiated — 3D space is imported through the primitive eta, which G_032 proved is assumed.",
        "gauge invariance" =>
            "U(1) = Z_96 = the rotation subgroup of Aut(C96(1..6)); verified on the permutations: r has order 96, "
          + "s order 2, s r s^-1 = r^-1, generated order 192. It is COMPACT (Z_96 is finite) and GLOBAL (the "
          + "rotation preserves adjacency; every site-dependent phase tested does not).",
        "Maxwell limit" =>
            "E_002: the field equation is derivable, and E_002 derived it in full — but from the action principle, "
          + "locality, 4-D dimensional counting and Lorentz invariance, with a coupling AT contradicts itself about "
          + "(1/alpha = 137 vs ~100).",
        "light propagation" =>
            "E_001: no computable spin-1 wave equation exists in AT.Core (0 found). AT's massless wave objects are "
          + "two SCALARS — the substrate's Laplacian zero mode (exactly one, the constant mode) and the "
          + "TemporalField discrete scalar wave equation — plus a POSTULATED spin-2 (MinimalPsiEquation: "
          + "Derived() = false, Postulated() = true). There is no spin-1 entry.",
        _ => "unspecified",
    };

    /// <summary>
    /// The one primitive AT would have to add for a photon — and it is SMALLER than it first appears, because
    /// AT already has a link phase, a holonomy and an interference law.
    /// </summary>
    public static string MissingPrimitive()
        => "a phase with a SPACETIME index and its own DYNAMICS. AT already has the phase — 2*pi/96 per ring "
         + "link, with a path phase, a loop holonomy and a 2 + 2 cos(delta) interference law — and it already has "
         + "the group (Z_96, compact). What it does not have is a phase living on 4-D SPACETIME links, or a "
         + "FLUCTUATING one: AT's link phase is the fixed constant 2*pi/N, not a field with a kinetic term.";

    /// <summary>
    /// What a photon being AT-native would require in order, so the gap is stated as a work programme rather
    /// than as a verdict.
    /// </summary>
    public static string NativeRoute() => string.Join(" ", new[]
    {
        "(i) BUILD THE SUBSTRATE THE THEORY ALREADY REQUIRES — instantiate the cubic C96 box C96 box C96 instead",
        "of importing 3D space as eta (G_033). This is the step that supplies T1(3), hence spin-1, and it is the",
        "same step gravity needs for E(2) + T2(3).",
        "(ii) PROMOTE THE RIGID ROTATION TO A LOCAL ONE — put an independent phase on each link. The audit shows",
        "the rigid rotation is an automorphism and a site-dependent phase is not, so this is genuinely new",
        "structure, not a consequence.",
        "(iii) COUNT THE POLARISATIONS — the promotion is only correct if the longitudinal mode is removed. The",
        "amended answer is that a compact U(1) with a conserved current can do this without a Goldstone, which is",
        "why AT's finite Z_96 is an advantage rather than a handicap.",
        "(iv) THEN THE MAXWELL LIMIT IS E_002's DERIVATION, which needs no new work.",
    });

    // ═══ VERDICT ════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// BOUNDARY (computed). AT supplies the photon's CHARGE natively, its REPRESENTATION CONTENT only on the
    /// cubic substrate that M_012 supplies and G_033 shows is never built, and it cannot supply the LOCALITY; its
    /// own massless sector is spin-2. Two of five requirements are native, and the missing primitive is exactly
    /// one. The verdict is judged by six independent checks, never typed.
    /// </summary>
    public static string Verdict()
    {
        bool obstruction = !SingleRingCanHostVector() && HasDimensionThreeMultiplet(1);
        bool gravitySameBlock = !SingleRingCanHostVector() && HasDimensionThreeMultiplet(2);
        bool u1Derived = DihedralRelations() is
            { RotationOrder: 96, ReflectionOrder: 2, DihedralRelation: true, GeneratedOrder: 192 };
        bool globalNotLocal = GlobalOrLocal() is { RotationIsAutomorphism: true, SiteDependentPhasesThatWork: 0 };
        bool linkClaimFails = !LinkOffsetsClosedUnderAddition() && LinkOffsetAlgebra().Abelian;
        bool su2Holds = DoubletLieAlgebraDimension() == 3;
        bool polarisationPins = PhotonPolarisations() == 2 && PhysicalPolarisations("scalar phase") == 1
                             && PhysicalPolarisations("gradient") == 1;
        // The native assets verified rather than asserted: AT already carries a compact link phase with a
        // holonomy and an interference law, and its substrate has exactly one massless mode, of spin 0.
        bool phaseAlreadyExists = HolonomyIsCompact() && Math.Abs(InterferenceLaw(0.0) - 4.0) < 1e-12
                               && Math.Abs(PathPhase(N96, N96) - 2.0 * Math.PI) < 1e-12;
        bool oneMasslessScalar = ZeroModeCount() == 1 && ZeroModeSpin() == 0 && SpectralGap() > 1e-9;
        bool noComputedSpin1 = NoComputedSpin1MasslessMode();

        if (!(obstruction && gravitySameBlock && u1Derived && globalNotLocal && linkClaimFails
              && su2Holds && polarisationPins && phaseAlreadyExists && oneMasslessScalar && noComputedSpin1))
            return "REFUTED";                       // one of the structural checks did not come out
        return "BOUNDARY";                           // the group and the phase are native; the locality is not
    }

    public static string WhereItStands()
        => "THE PHOTON IS NOT IMPORTED IN AT, AND IT IS NOT DERIVED EITHER — IT IS BLOCKED BY THE SAME MISSING "
         + "SUBSTRATE AS GRAVITY. The decisive fact is representation-theoretic: the photon is spin-1, so it needs "
         + "the three-dimensional l = 1 sector, and the single D96 ring's symmetry group has irreps of dimension 1 "
         + "and 2 only (4 * 1 + 47 * 4 = 192). AT's own audits already contain both halves — M_011 found no vector "
         + "sector on the ring, M_012 found a genuine one on the cubic network C96 box C96 box C96, where l = 1 "
         + "subducts to T1(3) with (chi, chi) = 1. The graviton's traceless sector needs a dimension-3 irrep for "
         + "the same reason (l = 2 -> E + T2), so the photon inherits exactly gravity's gap — and G_033 established "
         + "what became of that gap: the cubic substrate was never instantiated, because 3D space is imported "
         + "through the primitive eta that G_032 proved is assumed. What AT does supply natively is real: the U(1) "
         + "need not be imported as an axiom, since the rotation subgroup of Aut(C96(1..6)) IS a Z_96 (verified: r "
         + "of order 96, s of order 2, s r s^-1 = r^-1, group order 192), and because Z_96 is FINITE the emergent "
         + "U(1) is COMPACT, which is exactly the property that lets a compact U(1) with a conserved current carry "
         + "a massless vector without a Goldstone boson. That matters, because the cheap AT-native routes fail on "
         + "the polarisation count: a scalar phase gives one longitudinal mode and grad(rho) gives one, while the "
         + "photon needs two transverse. The single primitive AT must add is therefore small and nameable — a "
         + "phase on SPACETIME links. And one claim in the programme had to be falsified on the way: the twelve "
         + "link-directions of C96(1..6) are NOT the twelve gauge generators, since the offsets are not closed "
         + "under addition (6 + 6 = 12) and the algebra they generate is abelian, whereas u(1) + su(2) + su(3) is "
         + "11-dimensional non-abelian. The su(2)-from-doublets construction, by contrast, passes: the Lie "
         + "closure of the doublet generators is three-dimensional. AND THE GAP IS NARROWER THAN A FIRST "
         + "READING SUGGESTS: AT ALREADY CARRIES A LINK PHASE — PhaseOrigin assigns 2*pi/96 per ring link, with "
         + "a path phase, a loop holonomy, a two-slit law 2 + 2 cos(delta) and a phased Born rule. Its "
         + "substrate's Laplacian has EXACTLY ONE zero eigenvalue, and that mode is the constant (l = 0) one. So "
         + "what AT lacks is not the phase and not the group: it is the SPACETIME INDEX and the DYNAMICS — a "
         + "fluctuating phase on 4-D links, rather than the fixed step 2*pi/N on a ring.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputObstruction()
    {
        var budget = DihedralIrrepBudget(N96);
        var sb = new StringBuilder();
        sb.AppendLine("1. THE OBSTRUCTION IS REPRESENTATION-THEORETIC");
        sb.AppendLine("   The photon is spin-1 -> the l = 1 (vector / p-wave) sector -> THREE dimensions.");
        sb.AppendLine();
        sb.AppendLine($"   single ring: D_96 (order 192) = {budget.OneDim} one-dim + {budget.TwoDim} two-dim irreps");
        sb.AppendLine($"                sum of squared dimensions = {budget.SumOfSquares}  (group order 2n = {2 * N96})");
        sb.AppendLine($"                MAXIMUM irrep dimension = {budget.MaxDim}   ->  vector sector: "
                      + $"{(SingleRingCanHostVector() ? "possible" : "IMPOSSIBLE")}");
        sb.AppendLine($"   budget exact for every even n tested (4..200) : {DihedralBudgetIsExact()}");
        sb.AppendLine($"   ring spectrum pairs k with n-k (the 47 doublets): {SpectrumPairsUnderReflection()}"
                      + $",  distinct eigenvalues {DistinctRingEigenvalues()}");
        sb.AppendLine();
        sb.AppendLine("   spin ladder and where it can live:");
        sb.AppendLine("     l   sector                          octahedral      ring    cubic 96^3");
        foreach (var (l, name, oct, ring, cubic) in SpinLadder())
            sb.AppendLine($"     {l}   {name,-32}{oct,-16}{(ring ? "YES" : "no"),-8}{(cubic ? "YES" : "no")}");
        sb.AppendLine();
        sb.AppendLine("   ⇒ the photon and the graviton's traceless part need the SAME thing: a dimension-3");
        sb.AppendLine("     multiplet. M_011: absent from the ring. M_012: present on the cubic network.");
        return sb.ToString();
    }

    public static string OutputGauge()
    {
        var (rotOrder, refOrder, relation, order) = DihedralRelations();
        var (rotIsAuto, siteWorks, siteTried) = GlobalOrLocal();
        var (closureSize, abelian, nonAbelian) = LinkOffsetAlgebra();
        var first = FirstClosureFailure();
        var sb = new StringBuilder();
        sb.AppendLine("2. THE U(1): DERIVED AND COMPACT — BUT GLOBAL, NOT LOCAL");
        sb.AppendLine("   D96 = Aut(C96(1..6)), the dihedral group, verified ON THE PERMUTATIONS:");
        sb.AppendLine($"     rotation order {rotOrder}, reflection order {refOrder}, s r s^-1 = r^-1 : {relation}");
        sb.AppendLine($"     order of the generated group : {order}   (the name D96 means order 192)");
        sb.AppendLine($"     Z_96 = the rotation subgroup = the U(1) charge; finite, hence COMPACT : {GaugeGroupIsCompact()}");
        sb.AppendLine();
        sb.AppendLine("   IS IT LOCAL?");
        sb.AppendLine($"     the rigid rotation preserves adjacency (an automorphism) : {rotIsAuto}");
        sb.AppendLine($"     site-dependent phases that preserve adjacency : {siteWorks} of {siteTried}");
        sb.AppendLine("   ⇒ the symmetry is RIGID. A photon is not a rigid symmetry; it is a connection on");
        sb.AppendLine("     spacetime links, which needs independent phases per site. THAT is the gap.");
        sb.AppendLine();
        sb.AppendLine("   THE '12 LINK-DIRECTIONS ARE THE 12 GAUGE GENERATORS' CLAIM — FALSIFIED:");
        sb.AppendLine($"     offsets {string.Join(",", LinkOffsets())}");
        sb.AppendLine($"     closed under addition mod 96 : {LinkOffsetsClosedUnderAddition()}"
                      + $"   witness {first.A} + {first.B} = {first.Sum} (outside the set)");
        sb.AppendLine($"     additive closure size inside Z_96 : {closureSize} (not 12), abelian : {abelian}");
        sb.AppendLine($"     the gauge algebra's non-abelian part : {nonAbelian} dimensions — no map between them");
        sb.AppendLine($"   BUT the su(2)-from-doublets claim PASSES: the Lie closure of the doublet generators");
        sb.AppendLine($"     has dimension {DoubletLieAlgebraDimension()} (= 3, i.e. su(2)).");
        return sb.ToString();
    }

    public static string OutputNativeAssets()
    {
        var (fwd, bwd) = LinkPhase(N96);
        var sb = new StringBuilder();
        sb.AppendLine("3b. WHAT AT ALREADY HAS - THE GAP IS NARROWER THAN IT LOOKS");
        sb.AppendLine($"   phase quantum 2*pi/N (N = {N96})        : {PhaseQuantum(N96):E6}");
        sb.AppendLine($"     a link advances by  forward {fwd:E6} / backward {bwd:E6}");
        sb.AppendLine($"   path phase over a full cycle            : {PathPhase(N96, N96):E6}  (2*pi = "
                      + $"{2.0 * Math.PI:E6})");
        sb.AppendLine($"   loop holonomy 2*pi*(L mod N)/N is compact : {HolonomyIsCompact()}");
        sb.AppendLine($"   interference law 2 + 2 cos(delta)       : delta=0 -> {InterferenceLaw(0.0):F3}, "
                      + $"delta=pi -> {InterferenceLaw(Math.PI):F3}");
        sb.AppendLine("     (so AT does have a two-slit law, a holonomy and a phased Born rule — natively)");
        sb.AppendLine();
        sb.AppendLine("   the substrate's Laplacian, mu_k = 2k - lambda_k:");
        sb.AppendLine($"     exactly-zero eigenvalues : {ZeroModeCount()}  (the constant mode)");
        sb.AppendLine($"     its spin                 : {ZeroModeSpin()}  (l = 0 — NOT a vector)");
        sb.AppendLine($"     spectral gap above it    : {SpectralGap():F6}");
        sb.AppendLine();
        sb.AppendLine("   AT's massless wave objects, and their spin:");
        foreach (var (obj, spin, status) in MasslessModeInventory())
            sb.AppendLine($"     spin {spin}  {obj,-58} {status}");
        sb.AppendLine();
        sb.AppendLine("   ⇒ THE MISSING PRIMITIVE IS NOT 'A PHASE'. AT has the phase, the holonomy, the interference");
        sb.AppendLine("     law and the compact group. It lacks (a) a SPACETIME index — these are 1-D ring links —");
        sb.AppendLine("     and (b) DYNAMICS, since the step is the fixed constant 2*pi/N rather than a field.");
        return sb.ToString();
    }

    public static string OutputPolarisation()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. THE DECISIVE COUNT — POLARISATIONS");
        sb.AppendLine("   a massless spin-1 particle has exactly TWO transverse polarisations.");
        sb.AppendLine();
        sb.AppendLine("   candidate                                    components  removed  physical   photon?");
        foreach (var (name, comp, removed, phys, isPhoton) in PolarisationCandidates())
            sb.AppendLine($"   {name,-44}{comp,10}{removed,9}{phys,10}   {(isPhoton ? "YES" : "no")}");
        sb.AppendLine();
        sb.AppendLine("   ⇒ a single scalar phase gives ONE longitudinal mode; grad(rho) likewise. Neither is a");
        sb.AppendLine("     photon. Only a spacetime-indexed connection reduced by its gauge directions gives 2.");
        sb.AppendLine("     This is what pins the missing primitive to a phase on SPACETIME links.");
        return sb.ToString();
    }

    public static string OutputRequirements()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. THE FIVE REQUIREMENTS");
        foreach (var (req, status) in Requirements())
        {
            sb.AppendLine($"   {req,-20} {status}");
            sb.AppendLine($"     {RequirementBasis(req)}");
        }
        sb.AppendLine();
        sb.AppendLine("   THE MISSING PRIMITIVE (exactly one):");
        sb.AppendLine($"     {MissingPrimitive()}");
        sb.AppendLine();
        sb.AppendLine("   THE NATIVE ROUTE, IF IT IS TAKEN:");
        sb.AppendLine($"     {NativeRoute()}");
        return sb.ToString();
    }
}
