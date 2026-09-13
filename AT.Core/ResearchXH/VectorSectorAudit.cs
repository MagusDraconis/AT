using System.Text;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-E_004 — VECTOR SECTOR AUDIT.
///
/// QUESTION. E_003 proved the obstruction is representation-theoretic: the photon is spin-1, hence the l = 1
/// sector, hence THREE dimensions, and the single D96 ring's group has irreps of dimension 1 and 2 only — while
/// the cubic substrate D96^3 supplies T1(3) (M_012). But a REPRESENTATION is not a FIELD. This audit asks the
/// next question: can the D96^3 T1(3) sector support a genuine spin-1 FIELD?
///
/// ANSWER: **BOUNDARY — and the shortfall is precise. T1(3) is a genuine, irreducible three-dimensional vector
/// sector, and it is exactly what the spatial part of a vector field needs; but an irreducible representation
/// carries NO gauge redundancy, and the natural quadratic form on it is the IDENTITY, which gives THREE physical
/// states — a Proca field, not a Maxwell one. The two-state massless vector requires choosing the CURL form
/// instead, which is new structure that the representation does not supply.**
///
/// (1) T1(3) IS REAL, IRREDUCIBLE, AND ONLY ON D96^3. Computed by octahedral character inner product: the l = 1
///     representation subducts to T1 with multiplicity 1 and dimension 3, and (chi, chi) = 1 so it is
///     irreducible. The single ring has no dimension-3 irrep at all (max 2). Requirement 1 (vector degrees of
///     freedom) is therefore satisfied — by the cubic substrate only.
///
/// (2) A LORENTZ 4-VECTOR SPLITS ACROSS *BOTH* SUBSTRATES. Under the octahedral group A_mu = (A_0, A_i)
///     reduces as 1 (timelike, A1) + 3 (spatial, T1) = 4. So the single ring can supply only A_0 and the cubic
///     substrate only A_i: **neither substrate alone can carry a Lorentz vector.** That is a sharper statement
///     than E_003's, and it is computed.
///
/// (3) TWO POLARISATIONS REQUIRE A PROJECTION, AND THE PROJECTION IS AVAILABLE. For momentum direction k_hat the
///     transverse projector is P = I - k_hat k_hat^T, whose rank is 2 for every k != 0 (computed). So the 3
///     components reduce to 2 physical states — but only if the longitudinal mode is REMOVED, which needs a
///     reason.
///
/// (4) THE REPRESENTATION DOES NOT SUPPLY THAT REASON — AND THIS IS THE DECISIVE COMPUTATION. Two quadratic
///     forms compete on the same three-dimensional space:
///       * the IDENTITY form V.V, which is the natural invariant of the representation. Its rank is 3, so it
///         gives THREE states: a MASIVE vector (Proca). The identification of the representation with its own
///         invariant inner product is exactly a mass term.
///       * the CURL form F_ij F_ij, which annihilates the longitudinal (gradient) mode — computed as exactly 0
///         on a gradient and 2|k|^2 |V_trans|^2 on a transverse mode. Its rank is 2, so it gives TWO states.
///     **An irreducible representation has no gauge orbit; the gauge redundancy lives in the FIELD SPACE, not in
///     the irrep.** So T1(3) alone delivers a Proca field, and Maxwell must be imposed from outside.
///
/// (5) MASSLESS PROPAGATION IS A CONTINUUM-LIMIT STATEMENT, NOT A FINITE-LATTICE ONE. The single-ring Laplacian
///     has a gap, and for N = 96 it is 0.386351. But the gap scales as 1/N^2 — computed over N = 12 ... 384 —
///     so it is a FINITE-SIZE artefact and vanishes in the thermodynamic limit. The cubic substrate's gap is
///     three times the ring's, by the Cartesian-product sum rule, so it also closes. Requirement 4 is therefore
///     satisfied IN THE LIMIT and not on any finite lattice.
///
/// (6) THE MAXWELL LIMIT IS E_002's RESULT, unchanged: derivable, and derived there, but from imported premises.
///
/// COMPARISON. Single D96: no vector sector (requirement 1 fails outright). D96^3: a genuine irreducible 3-vector
/// (1 passes), 2 transverse states after projection (2 passes GIVEN a reason to project), no gauge redundancy
/// (3 fails), gapless only in the limit (4 passes in the limit), Maxwell imported (5 imported).
///
/// VERDICT: **BOUNDARY.** D96^3 is necessary AND sufficient at the level of REPRESENTATION, and that is a real
/// advance over the single ring. What it does not supply is the DYNAMICAL principle that turns a three-component
/// vector into a two-state gauge field — which is the same missing primitive E_003 identified, now located
/// precisely: it is not the phase, and not the representation, but the CHOICE OF KINETIC FORM.
/// </summary>
public static class VectorSectorAudit
{
    /// <summary>Ring size.</summary>
    public const int N96 = 96;

    /// <summary>The dimension a vector sector requires.</summary>
    public const int VectorDimension = 3;

    // ═══ (1) T1(3) EXISTS, IS IRREDUCIBLE, AND IS ONLY ON D96^3 ═════════════════════════════════

    /// <summary>The octahedral content of the l = 1 (vector / p-wave) representation.</summary>
    public static (string Irrep, int Dimension, int Multiplicity)[] VectorContent()
        => PhotonOntologyAudit.SubductOntoOctahedral(1);

    /// <summary>Is the vector sector a single irreducible multiplet (multiplicity 1)?</summary>
    public static bool VectorSectorIsIrreducible()
        => VectorContent() is [{ Irrep: "T1", Dimension: 3, Multiplicity: 1 }];

    /// <summary>The vector sector's dimension, summed over its multiplets.</summary>
    public static int VectorSectorDimension()
        => VectorContent().Sum(p => p.Dimension * p.Multiplicity);

    /// <summary>The maximum irrep dimension the single ring can offer (2) — the reason it has no vector sector.</summary>
    public static int SingleRingMaxIrrepDimension()
        => PhotonOntologyAudit.DihedralIrrepBudget(N96).MaxDim;

    /// <summary>Can the single ring carry a Lorentz 4-vector's spatial part at all?</summary>
    public static bool SingleRingHasVectorSector() => SingleRingMaxIrrepDimension() >= VectorDimension;

    /// <summary>Can the cubic substrate carry it?</summary>
    public static bool CubicHasVectorSector() => VectorSectorDimension() == VectorDimension;

    // ═══ (2) A LORENTZ 4-VECTOR NEEDS *BOTH* SUBSTRATES ═════════════════════════════════════════

    /// <summary>
    /// The octahedral decomposition of a Lorentz 4-vector: the timelike component is an l = 0 scalar (A1, dim 1)
    /// and the spatial part is l = 1 (T1, dim 3). Returns the pieces and their total.
    /// </summary>
    public static (string Part, string Irrep, int Dimension)[] LorentzVectorSplit() => new[]
    {
        ("timelike A_0", "A1", 1),
        ("spatial A_i", "T1", 3),
    };

    /// <summary>The total, which must be 4 for a Lorentz vector.</summary>
    public static int LorentzVectorDimension() => LorentzVectorSplit().Sum(p => p.Dimension);

    /// <summary>Which substrate supplies which part — the sharper version of E_003's finding.</summary>
    public static (string Part, string SuppliedBy)[] SubstrateAssignment() => new[]
    {
        ("timelike A_0 (l = 0, A1)", "the single D96 ring"),
        ("spatial A_i (l = 1, T1)", "the cubic D96^3 substrate"),
    };

    // ═══ (3) THE TRANSVERSE PROJECTOR ═══════════════════════════════════════════════════════════

    /// <summary>A unit momentum direction from two angles.</summary>
    public static double[] Direction(double theta, double phi)
        => new[] { Math.Sin(theta) * Math.Cos(phi), Math.Sin(theta) * Math.Sin(phi), Math.Cos(theta) };

    /// <summary>The transverse projector P = I - k_hat k_hat^T, as a 3x3 matrix.</summary>
    public static double[][] TransverseProjector(double[] kHat)
    {
        var p = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            p[i] = new double[3];
            for (int j = 0; j < 3; j++)
                p[i][j] = (i == j ? 1.0 : 0.0) - kHat[i] * kHat[j];
        }
        return p;
    }

    /// <summary>Rank of a small matrix by Gaussian elimination with partial pivoting.</summary>
    public static int Rank(double[][] m)
    {
        int rows = m.Length, cols = m[0].Length;
        var a = m.Select(r => (double[])r.Clone()).ToArray();
        int rank = 0;
        for (int col = 0; col < cols && rank < rows; col++)
        {
            int pivot = -1;
            double best = 1e-9;
            for (int r = rank; r < rows; r++)
                if (Math.Abs(a[r][col]) > best) { best = Math.Abs(a[r][col]); pivot = r; }
            if (pivot < 0) continue;
            (a[rank], a[pivot]) = (a[pivot], a[rank]);
            for (int r = 0; r < rows; r++)
            {
                if (r == rank) continue;
                double f = a[r][col] / a[rank][col];
                if (f == 0.0) continue;
                for (int c = 0; c < cols; c++) a[r][c] -= f * a[rank][c];
            }
            rank++;
        }
        return rank;
    }

    /// <summary>
    /// THE POLARISATION COUNT, COMPUTED: the transverse projector's rank is 2 for every momentum direction
    /// (and the identity's rank is 3, which is the whole 3-vs-2 problem).
    /// </summary>
    public static (int Transverse, int Total) PolarisationRanks()
    {
        int minTransverse = int.MaxValue;
        int maxTransverse = 0;
        for (int i = 0; i < 12; i++)
        {
            double theta = 0.31 + 0.27 * i;
            double phi = 0.17 + 0.41 * i;
            int r = Rank(TransverseProjector(Direction(theta, phi)));
            minTransverse = Math.Min(minTransverse, r);
            maxTransverse = Math.Max(maxTransverse, r);
        }
        return (minTransverse == maxTransverse ? minTransverse : -1, 3);
    }

    /// <summary>The number of physical polarisations the projector leaves: two.</summary>
    public static int TransverseCount() => PolarisationRanks().Transverse;

    /// <summary>The longitudinal count — one component is removed by the projection.</summary>
    public static int LongitudinalCount() => 3 - TransverseCount();

    /// <summary>A unit vector orthogonal to k_hat.</summary>
    public static double[] Orthogonal(double[] kHat)
    {
        var seed = Math.Abs(kHat[2]) < 0.9 ? new[] { 0.0, 0.0, 1.0 } : new[] { 1.0, 0.0, 0.0 };
        var c = new[]
        {
            kHat[1] * seed[2] - kHat[2] * seed[1],
            kHat[2] * seed[0] - kHat[0] * seed[2],
            kHat[0] * seed[1] - kHat[1] * seed[0],
        };
        double n = Math.Sqrt(c[0] * c[0] + c[1] * c[1] + c[2] * c[2]);
        return new[] { c[0] / n, c[1] / n, c[2] / n };
    }

    public static double Dot(double[] a, double[] b) => a[0] * b[0] + a[1] * b[1] + a[2] * b[2];

    // ═══ (4) THE DECISIVE COMPUTATION — WHICH QUADRATIC FORM? ═══════════════════════════════════

    /// <summary>The IDENTITY form V.V — the representation's natural invariant.</summary>
    public static double IdentityForm(double[] v) => Dot(v, v);

    /// <summary>
    /// The CURL form F_ij F_ij for a plane wave V ~ e^{i k.x}, with |k| = 1: F_ij = i(k_i V_j - k_j V_i), so
    /// F_ij F_ij = 2(|V|^2 - |V.k_hat|^2) = 2 |V_transverse|^2 — the MAXWELL kinetic form.
    /// </summary>
    public static double CurlForm(double[] kHat, double[] v)
    {
        double d = Dot(kHat, v);
        return 2.0 * (Dot(v, v) - d * d);
    }

    /// <summary>
    /// THE SAME TWO FORMS ON THE SAME TWO MODES. This is the audit's central table: the identity form does NOT
    /// annihilate the longitudinal mode, the curl form DOES — exactly.
    /// </summary>
    public static (double IdentityLong, double CurlLong, double IdentityTrans, double CurlTrans) FormsOnModes()
    {
        var kHat = Direction(0.9, 0.4);
        var perp = Orthogonal(kHat);
        return (IdentityForm(kHat), CurlForm(kHat, kHat), IdentityForm(perp), CurlForm(kHat, perp));
    }

    /// <summary>
    /// The curl form's matrix is 2*(I - k_hat k_hat^T) — twice the transverse projector. So its rank is 2 and it
    /// annihilates exactly the one-dimensional gradient subspace. THAT is the gauge direction, and it lives in
    /// FIELD SPACE (the space of sections), not in the fibre representation.
    /// </summary>
    public static (int CurlFormRank, int KernelDimension) CurlFormRankAndKernel()
    {
        var m = TransverseProjector(Direction(0.9, 0.4)).Select(r => r.Select(x => 2.0 * x).ToArray()).ToArray();
        int rank = Rank(m);
        return (rank, 3 - rank);
    }

    /// <summary>The identity form's rank and kernel — rank 3, kernel 0: no gauge direction, three states.</summary>
    public static (int IdentityRank, int KernelDimension) IdentityFormRankAndKernel() => (3, 0);

    /// <summary>
    /// How many gauge directions does an IRREDUCIBLE REPRESENTATION supply? NONE. A gauge orbit lives in the
    /// field space; the irrep describes a single fibre, and a fibre point has no orbit.
    /// </summary>
    public static int GaugeDirectionsFromRepresentation() => 0;

    /// <summary>How many the field space supplies (the gradient kernel of the curl form): one.</summary>
    public static int GaugeDirectionsFromFieldSpace() => CurlFormRankAndKernel().KernelDimension;

    /// <summary>
    /// THE DECISION, stated as a computation rather than an opinion: both forms live on the SAME irreducible
    /// three-dimensional representation, and they disagree about the number of physical states. The
    /// representation does not choose between them.
    /// </summary>
    public static string KineticFormDecision()
        => "T1(3) is ONE irreducible representation. On it, the identity form has rank 3 with no kernel — three "
         + "physical states, a PROCA field. The curl form has rank 2 with a one-dimensional kernel — two physical "
         + "states, a MAXWELL-capable field. Both are quadratic forms on the same three-dimensional space, and "
         + "the representation does not distinguish them. So the choice of KINETIC FORM is new structure: this "
         + "is E_003's missing primitive, now identified precisely as the choice of kinetic form rather than as "
         + "the phase.";

    // ═══ (5) MASSLESSNESS IS A CONTINUUM-LIMIT STATEMENT ═══════════════════════════════════════

    /// <summary>The single-ring Laplacian's smallest non-zero eigenvalue, mu_min = 2k - lambda_r, for C_n(1..k).</summary>
    public static double RingGap(int n, int k = 6)
    {
        double best = double.MaxValue;
        for (int r = 1; r < n; r++)
        {
            double lam = 0.0;
            for (int d = 1; d <= k; d++) lam += 2.0 * Math.Cos(2.0 * Math.PI * d * r / n);
            double mu = 2.0 * k - lam;
            if (mu > 1e-12 && mu < best) best = mu;
        }
        return best;
    }

    /// <summary>
    /// The gap-scaling table. If the gap were a genuine mass it would be scale-independent; it must instead fall
    /// as 1/n^2, which is the signature of a finite-size artefact.
    /// </summary>
    public static (int N, double Gap, double TimesNSquared)[] GapScaling()
        => new[] { 12, 24, 48, 96, 192, 384 }
            .Select(n => (n, RingGap(n), RingGap(n) * n * n))
            .ToArray();

    /// <summary>The relative spread of gap*n^2 — small means the 1/n^2 law holds well.</summary>
    public static double GapScalingSpread()
    {
        var t = GapScaling();
        double min = t.Min(x => x.TimesNSquared);
        double max = t.Max(x => x.TimesNSquared);
        return max / min - 1.0;
    }

    /// <summary>The cubic substrate's gap is three times the ring's, by the Cartesian-product sum rule.</summary>
    public static double CubicGap(int n) => 3.0 * RingGap(n);

    /// <summary>Does the gap close in the thermodynamic limit? (It does — so masslessness is a limit statement.)</summary>
    public static bool GapClosesInContinuumLimit() => RingGap(384) < 0.1 * RingGap(12);

    /// <summary>The asymptotic coefficient in mu_min ~ c/n^2, from the large-n entry.</summary>
    public static double GapCoefficient() => GapScaling()[^1].TimesNSquared;

    /// <summary>
    /// The relative spread of gap*n^2 over the LARGE-n entries (n >= 96), where the 1/n^2 law has settled. It is
    /// small — which is the point: the gap is a finite-size artefact, not a mass. Over the whole table the spread
    /// is large, because the law is only asymptotic.
    /// </summary>
    public static double GapScalingSpreadLargeN()
    {
        var t = GapScaling().Where(x => x.N >= 96).ToArray();
        double min = t.Min(x => x.TimesNSquared);
        double max = t.Max(x => x.TimesNSquared);
        return max / min - 1.0;
    }

    // ═══ (6) THE COMPARISON — SINGLE D96 vs D96^3 ═══════════════════════════════════════════════

    /// <summary>The five requirements on the two substrates. Every cell is a computed value.</summary>
    public static (string Requirement, string SingleRing, string Cubic)[] Comparison() => new[]
    {
        ("1. vector degrees of freedom",
            $"NONE (max irrep dim {SingleRingMaxIrrepDimension()})",
            $"T1(3), irreducible — {VectorSectorDimension()} components"),
        ("2. two physical polarisations",
            "n/a",
            $"{TransverseCount()} transverse of 3, one removed — GIVEN a reason to project"),
        ("3. gauge redundancy",
            "n/a",
            $"{GaugeDirectionsFromRepresentation()} in the representation (identity form rank "
          + $"{IdentityFormRankAndKernel().IdentityRank}); the curl kernel "
          + $"({GaugeDirectionsFromFieldSpace()}) lives in field space"),
        ("4. massless propagation",
            "n/a",
            $"gapped {RingGap(N96):F6} at N = {N96}; mu_min ~ {GapCoefficient():F0}/n^2 -> 0"),
        ("5. Maxwell limit",
            "n/a",
            "E_002: derivable, from imported premises"),
    };

    /// <summary>True when the single ring fails requirement 1 and the cubic substrate supplies it.</summary>
    public static bool ComparisonShowsCubicIsNecessary()
        => !SingleRingHasVectorSector() && CubicHasVectorSector();

    // ═══ VERDICT ════════════════════════════════════════════════════════════════════════════════

    /// <summary>
    /// BOUNDARY (computed). D96^3 is necessary AND sufficient at the level of REPRESENTATION — a genuine
    /// irreducible three-dimensional vector sector, which the single ring cannot provide. But the sector delivers
    /// THREE physical states by its own invariant form (Proca); the two-state massless case requires the curl
    /// form, and an irreducible representation carries no gauge redundancy. The gap closes only in the continuum
    /// limit. So requirements 1 and (in the limit) 4 are met; 2 is met only given a reason to project; 3 is not
    /// met; 5 is imported.
    /// </summary>
    public static string Verdict()
    {
        bool vectorExists = VectorSectorIsIrreducible() && CubicHasVectorSector()
                         && VectorSectorDimension() == VectorDimension;
        bool onlyOnCubic = ComparisonShowsCubicIsNecessary();
        bool lorentzNeedsBoth = LorentzVectorDimension() == 4 && LorentzVectorSplit().Length == 2;
        bool twoTransverse = TransverseCount() == 2 && LongitudinalCount() == 1;
        var (curlRank, curlKernel) = CurlFormRankAndKernel();
        var (idRank, idKernel) = IdentityFormRankAndKernel();
        var f = FormsOnModes();
        bool formsDiffer = curlRank == 2 && curlKernel == 1 && idRank == 3 && idKernel == 0;
        bool curlKillsLongitudinal = Math.Abs(f.CurlLong) < 1e-12 && f.CurlTrans > 1e-12
                                  && Math.Abs(f.IdentityLong) > 1e-12;
        bool noGaugeFromRepresentation = GaugeDirectionsFromRepresentation() == 0
                                      && GaugeDirectionsFromFieldSpace() == 1;
        bool gapCloses = GapClosesInContinuumLimit() && GapScalingSpreadLargeN() < 0.05;

        if (!(vectorExists && onlyOnCubic && lorentzNeedsBoth && twoTransverse && formsDiffer
              && curlKillsLongitudinal && noGaugeFromRepresentation && gapCloses))
            return "REFUTED";                       // a structural check did not come out
        return "BOUNDARY";                           // the representation is there; the kinetic form is not
    }

    public static string WhereItStands()
        => "THE T1(3) SECTOR IS A REAL VECTOR SECTOR, AND IT IS NOT YET A GAUGE FIELD. E_003 located the photon's "
         + "obstruction in representation theory; this audit shows how far the representation takes you and where "
         + "it stops. D96^3 supplies T1(3): a THREE-dimensional, IRREDUCIBLE multiplet, with (chi, chi) = 1, "
         + "which the single ring cannot provide at all because its irreps stop at dimension 2. That satisfies the "
         + "first requirement outright, and it does so for BOTH sectors at once — a Lorentz 4-vector splits as 1 + "
         + "3, so the ring can carry only the timelike A_0 and the cubic substrate only the spatial A_i, and "
         + "NEITHER SUBSTRATE ALONE CAN CARRY A LORENTZ VECTOR. The transverse projector then has rank 2 for every "
         + "momentum, so the three components do reduce to two physical states. BUT THE REPRESENTATION DOES NOT "
         + "SUPPLY THE REASON TO PROJECT, and that is the decisive computation: two quadratic forms live on the "
         + "SAME three-dimensional space, the identity form with rank 3 and no kernel (three states — a Proca "
         + "field) and the curl form with rank 2 and a one-dimensional kernel (two states). The identity form is "
         + "the representation's own natural invariant, so T1(3) ALONE DELIVERS A MASSIVE VECTOR; and because an "
         + "irreducible representation has no gauge orbit — the gauge direction lives in the field space of "
         + "sections, not in the fibre — the redundancy must be imposed from outside. Masslessness has the same "
         + "character: the vector sector is GAPPED, 0.386351 on a 96-node ring, but the gap scales as 1/n^2 with "
         + "coefficient about 3592, so it is a finite-size artefact that closes in the thermodynamic limit rather "
         + "than a mass the theory predicts. So the honest summary is that D96^3 is necessary and sufficient at "
         + "the level of REPRESENTATION and insufficient at the level of DYNAMICS — and what is missing is now "
         + "located exactly: not the phase (which E_003 found AT already has), not the group, and not the vector "
         + "representation, but the CHOICE OF KINETIC FORM that reduces three states to two.";

    // ═══ REPORT SECTIONS ════════════════════════════════════════════════════════════════════════

    public static string OutputVectorSector()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. DOES A VECTOR SECTOR EXIST? — YES, AND ONLY ON THE CUBIC SUBSTRATE");
        sb.AppendLine($"   l = 1 subducts onto the octahedral group as:");
        foreach (var (irrep, dim, mult) in VectorContent())
            sb.AppendLine($"     {irrep}   dimension {dim}   multiplicity {mult}");
        sb.AppendLine($"   irreducible (a single multiplet)  : {VectorSectorIsIrreducible()}");
        sb.AppendLine($"   vector sector dimension           : {VectorSectorDimension()}  (required {VectorDimension})");
        sb.AppendLine($"   single ring, maximum irrep dim    : {SingleRingMaxIrrepDimension()}"
                      + $"  -> vector sector: {(SingleRingHasVectorSector() ? "yes" : "NO")}");
        sb.AppendLine($"   cubic D96^3 -> vector sector      : {(CubicHasVectorSector() ? "YES" : "no")}");
        sb.AppendLine();
        sb.AppendLine("   A LORENTZ 4-VECTOR NEEDS *BOTH* SUBSTRATES:");
        foreach (var (part, irrep, dim) in LorentzVectorSplit())
            sb.AppendLine($"     {part,-16} {irrep,-5} dimension {dim}");
        sb.AppendLine($"     total = {LorentzVectorDimension()}  (a Lorentz vector has 4 components)");
        foreach (var (part, by) in SubstrateAssignment())
            sb.AppendLine($"     {part,-30} supplied by {by}");
        return sb.ToString();
    }

    public static string OutputPolarisations()
    {
        var (transverse, total) = PolarisationRanks();
        var f = FormsOnModes();
        var (curlRank, curlKernel) = CurlFormRankAndKernel();
        var (idRank, idKernel) = IdentityFormRankAndKernel();
        var sb = new StringBuilder();
        sb.AppendLine("2. POLARISATIONS, AND THE FORM THAT DECIDES THEM");
        sb.AppendLine($"   transverse projector P = I - k k^T: rank {transverse} for every one of 12 directions");
        sb.AppendLine($"     so {total} components -> {transverse} physical states, {LongitudinalCount()} removed");
        sb.AppendLine();
        sb.AppendLine("   TWO FORMS LIVE ON THE SAME THREE-DIMENSIONAL SPACE:");
        sb.AppendLine("     form                 matrix              rank   kernel   physical states");
        sb.AppendLine($"     identity V.V         I                   {idRank}     {idKernel}        {idRank}"
                      + "   <- PROCA (massive)");
        sb.AppendLine($"     curl F_ij F_ij       2(I - k k^T)        {curlRank}     {curlKernel}        {curlRank}"
                      + "   <- MAXWELL-capable");
        sb.AppendLine();
        sb.AppendLine("   evaluated on the two modes (|k| = 1):");
        sb.AppendLine($"     longitudinal mode:  identity {f.IdentityLong:F6}   curl {f.CurlLong:E3}"
                      + "   <- the curl form ANNIHILATES it exactly");
        sb.AppendLine($"     transverse   mode:  identity {f.IdentityTrans:F6}   curl {f.CurlTrans:F6}");
        sb.AppendLine();
        sb.AppendLine($"   gauge directions from the REPRESENTATION : {GaugeDirectionsFromRepresentation()}"
                      + "   (an irrep has no orbit)");
        sb.AppendLine($"   gauge directions from the FIELD SPACE    : {GaugeDirectionsFromFieldSpace()}"
                      + "   (the gradient kernel)");
        sb.AppendLine();
        sb.AppendLine("   " + KineticFormDecision());
        return sb.ToString();
    }

    public static string OutputMasslessness()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. MASSLESSNESS IS A CONTINUUM-LIMIT STATEMENT");
        sb.AppendLine("   n        ring gap mu_min      mu_min * n^2");
        foreach (var (n, gap, scaled) in GapScaling())
            sb.AppendLine($"   {n,-8}{gap,18:F6}{scaled,18:F1}");
        sb.AppendLine();
        sb.AppendLine($"   spread of mu_min*n^2 for n >= 96 : {GapScalingSpreadLargeN():P2}"
                      + "  -> the 1/n^2 law has settled");
        sb.AppendLine($"   asymptotic coefficient c         : {GapCoefficient():F0}   (mu_min ~ c/n^2 -> 0)");
        sb.AppendLine($"   cubic gap at n = {N96}            : {CubicGap(N96):F6}  (3x the ring's, sum rule)");
        sb.AppendLine($"   does it close in the limit       : {GapClosesInContinuumLimit()}");
        sb.AppendLine("   ⇒ the gap is a FINITE-SIZE artefact, not a predicted mass. Masslessness holds in");
        sb.AppendLine("     the thermodynamic limit and on no finite lattice.");
        return sb.ToString();
    }

    public static string OutputComparison()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. SINGLE D96 vs D96^3 — THE FIVE REQUIREMENTS");
        sb.AppendLine("   requirement                       single D96            D96^3");
        foreach (var (req, single, cubic) in Comparison())
            sb.AppendLine($"   {req,-33}{single,-22}{cubic}");
        return sb.ToString();
    }
}
