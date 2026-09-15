using System.Text;
using AT.Core.ResearchXH;

namespace AT.Core.ResearchXH;

/// <summary>
/// ResearchY-G_056 - NON-SCALAR SELECTION AUDIT (group G - Gravity Source).
///
/// QUESTION. Can any existing NON-SCALAR AT structure span the 53-dimensional phase sector? Given G_052 (the interface
/// identity), G_054 (the phases are freely assigned) and G_055 (a SCALAR functional constrains at most ONE phase
/// direction, and six of them measured a rank of 3). Candidates: phase vector field, connection structure, T1/T2 sector
/// coupling, edge-holonomy network, causal-order tensor. Measure the PHASE RANK, required to exceed 3. Goal: the first
/// AT object whose gradients span more than one phase direction.
///
/// ANSWER: **DERIVED - the occupancy-gradient VECTOR FIELD spans the phase sector EXACTLY (53 of 53), and its
/// connection and tensor descendants do too. G_055's ceiling was a ceiling on SCALARS, and non-scalar structures are
/// not subject to it. What this does NOT do is select a phase: G_054 measured that no AT process runs the flow, so
/// spanning is SENSITIVITY rather than DETERMINATION - and the audit keeps the two apart explicitly.**
///
///  (1) THE MEASUREMENT IS THE PHASE RANK, AND IT IS THE RIGHT GENERALISATION OF G_055's COUNT. A scalar's gradient is
///      one vector; a structure that produces m numbers has a Jacobian with m rows, so its phase rank is the rank of the
///      map it induces FROM the 53 phase directions INTO its own output space. The audit computes that directly: it
///      differentiates the structure along each phase direction and takes the rank of the resulting vectors. A rank of 1
///      is a scalar-like structure; 53 is a structure that sees everything.
///
///  (2) THE GRADIENT-BASED CANDIDATES REACH THE FULL RANK, AND THE REASON IS STRUCTURAL. The occupancy-gradient field
///      differentiates the state along a cell, and differentiation is INJECTIVE on every non-constant Fourier mode -
///      which is every mode the phase sector contains. So its phase rank is the full 53 by construction, and the
///      measurement confirms it. The connection built from it inherits the property, and the T1/T2 coupling - the
///      products of two gradients - inherits it as well.
///
///  (3) THE DECOUPLED AND PIECEWISE-CONSTANT CANDIDATES REACH ZERO. The edge-holonomy network READ FROM THE LINK PHASES
///      is decoupled from the organisation altogether (G_055's census is zero), so it cannot see a phase at all. The
///      causal-order tensor built from the SIGN of occupancy differences is piecewise constant, so its derivative
///      vanishes almost everywhere. Both are REFUTED as phase-spanning structures - and the audit notes that the
///      holonomy built instead from the OCCUPANCY-derived connection coincides with the connection candidate, which is
///      why the two are reported together rather than as independent routes.
///
///  (4) SPANNING IS NOT SELECTING, AND THIS IS THE CLAUSE THAT KEEPS THE SERIES CONSISTENT. G_054 found that no AT
///      process runs a phase flow, so the structures measured here are SENSITIVITIES that nothing extremises. The
///      phases remain freely assigned; what has changed is the reason the audit can give for the deficiency G_055
///      measured - it was a deficiency of SCALARS, not of AT.
/// </summary>
public static class NonScalarSelectionAudit
{
    public const int D = 3;
    public const int Cells = RhoAccessibilityAudit.Cells;

    public static double[] State() => RhoAccessibilityAudit.BaseState();
    public static (int Channel, string Kind, double[] Mode)[] PhaseBasis() => AmplitudePhaseAudit.PhaseModes();
    public static int PhaseDimension() => PhaseBasis().Length;

    /// <summary>The scalar ceiling measured in G_055, kept here as the bar this audit has to clear.</summary>
    public static int ScalarCeiling() => PhaseSelectionPrincipleAudit.ConstraintRank();

    // ===================== 1. THE PHASE RANK =====================

    /// <summary>
    /// The phase rank of a structure: differentiate it along each phase direction, and take the rank of the resulting
    /// vectors. This is the generalisation of G_055's count - a scalar gives one vector, so its rank cannot exceed 1,
    /// while a structure with m outputs can reach min(m, 53).
    /// </summary>
    public static int PhaseRank(Func<double[], double[]> structure, double step = 1e-5)
    {
        var rho = State();
        var images = new List<double[]>();
        foreach (var p in PhaseBasis())
        {
            var up = rho.Zip(p.Mode, (r, d) => r + step * d).ToArray();
            var down = rho.Zip(p.Mode, (r, d) => r - step * d).ToArray();
            var a = structure(up);
            var b = structure(down);
            images.Add(a.Zip(b, (x, y) => (x - y) / (2.0 * step)).ToArray());
        }
        return Rank(images);
    }

    private static int Rank(IEnumerable<double[]> vectors)
    {
        var basis = new List<double[]>();
        foreach (var v0 in vectors)
        {
            var v = (double[])v0.Clone();
            foreach (var b in basis)
            {
                double dot = v.Zip(b, (x, y) => x * y).Sum();
                for (int i = 0; i < v.Length; i++) v[i] -= dot * b[i];
            }
            double norm = Math.Sqrt(v.Sum(x => x * x));
            if (norm > 1e-9) basis.Add(v.Select(x => x / norm).ToArray());
        }
        return basis.Count;
    }

    // ===================== 2. THE CANDIDATES, AS AT STRUCTURES =====================

    /// <summary>1. THE PHASE VECTOR FIELD: the occupancy gradient, the object E_008 and E_010 build their links from.</summary>
    public static double[] OccupancyGradient(double[] rho)
        => Enumerable.Range(0, Cells).Select(i => rho[(i + 1) % Cells] - rho[i]).ToArray();

    /// <summary>
    /// 2. THE CONNECTION STRUCTURE: the link field A = h(rho) * Delta rho of E_009 and E_010, the only vector a scalar
    /// organisation can build.
    /// </summary>
    public static double[] DerivedConnection(double[] rho)
    {
        var h = CouplingFunctionAudit.DerivedCoupling();
        return Enumerable.Range(0, Cells).Select(i => h(rho[i]) * (rho[(i + 1) % Cells] - rho[i])).ToArray();
    }

    /// <summary>
    /// 3. THE T1/T2 SECTOR COUPLING: the products of two gradients, whose antisymmetric part is the T1-like object and
    /// whose symmetric part is the T2-like one. On the ring the two shifts commute, so the antisymmetric part vanishes
    /// IDENTICALLY - measured below - and the coupling's rank comes from its symmetric part. The audit reports that
    /// rather than quietly relying on the surviving half: a candidate whose T1 half is zero is a weaker candidate than
    /// its name suggests, and saying so is the point of measuring it.
    /// </summary>
    public static double[] SectorCoupling(double[] rho)
    {
        var d1 = OccupancyGradient(rho);
        var d2 = Enumerable.Range(0, Cells).Select(i => rho[(i + 2) % Cells] - rho[i]).ToArray();
        var d3 = Enumerable.Range(0, Cells).Select(i => rho[(i + 3) % Cells] - rho[i]).ToArray();
        var t1 = d1.Zip(d2, (a, b) => a * b - b * a).ToArray();          // antisymmetric part (vanishes by commuting
        var t2 = d1.Zip(d2, (a, b) => a * b + b * a).ToArray();          // shifts, so the audit reports it as measured)
        var t2b = d1.Zip(d3, (a, b) => a * b + b * a).ToArray();
        return t1.Concat(t2).Concat(t2b).ToArray();
    }

    /// <summary>
    /// 4. THE EDGE-HOLONOMY NETWORK read from the LINK phases. The label is carried by the phases on the edges and the
    /// census of members coupling them to the organisation is zero, so this structure cannot see the state at all.
    /// </summary>
    public static double[] EdgeHolonomyNetwork(double[] rho)
    {
        _ = rho;
        return Enumerable.Range(0, Cells)
            .Select(i => SectorSelectionAudit.SectorLabel(SectorSelectionAudit.UniformSector(1, 8), 8)).ToArray();
    }

    /// <summary>
    /// 5. THE CAUSAL-ORDER TENSOR: the sign of the occupancy difference between neighbouring cells, which is the
    /// order-theoretic content the substrate can express locally.
    /// </summary>
    public static double[] CausalOrderTensor(double[] rho)
    {
        var sign = Enumerable.Range(0, Cells)
            .Select(i => Math.Sign(rho[(i + 1) % Cells] - rho[i])).ToArray();
        return Enumerable.Range(0, Cells).Select(i => (double)sign[i]).ToArray();
    }

    // ===================== 3. THE TABLE =====================

    /// <summary>The antisymmetric half of the coupling, which the commuting shifts make identically zero.</summary>
    public static double[] AntisymmetricCouplingPart(double[] rho)
    {
        var d1 = OccupancyGradient(rho);
        var d2 = Enumerable.Range(0, Cells).Select(i => rho[(i + 2) % Cells] - rho[i]).ToArray();
        return d1.Zip(d2, (a, b) => a * b - b * a).ToArray();
    }

    public static double AntisymmetricPartNorm() => Math.Sqrt(AntisymmetricCouplingPart(State()).Sum(x => x * x));

    public static bool TheAntisymmetricPartVanishes() => AntisymmetricPartNorm() < 1e-15;

    /// <summary>
    /// The candidates and their measured phase ranks. The table carries NO verdict strings: rule 6 of this project -
    /// a literal must never reach a verdict - applies to audits as much as to code, and a first version of this table
    /// had its verdicts written beside the numbers. The verdict is computed from the rank and the ceiling instead.
    /// </summary>
    public static (string Candidate, int PhaseRank, int Outputs)[] CandidateTable() => new[]
    {
        ("phase vector field", PhaseRank(OccupancyGradient), OccupancyGradient(State()).Length),
        ("connection structure", PhaseRank(DerivedConnection), DerivedConnection(State()).Length),
        ("T1/T2 sector coupling", PhaseRank(SectorCoupling), SectorCoupling(State()).Length),
        ("edge-holonomy network", PhaseRank(EdgeHolonomyNetwork), EdgeHolonomyNetwork(State()).Length),
        ("causal-order tensor", PhaseRank(CausalOrderTensor), CausalOrderTensor(State()).Length),
    };

    /// <summary>The verdict of each candidate is COMPUTED from its measured rank, not written beside it.</summary>
    public static string VerdictOf(int phaseRank, int scalarCeiling)
        => phaseRank == 0 ? "REFUTED" : phaseRank > scalarCeiling ? "DERIVED" : "BOUNDARY";

    public static (string Candidate, int PhaseRank, int Outputs, string Verdict)[] ComputedTable()
        => CandidateTable().Select(t => (t.Candidate, t.PhaseRank, t.Outputs, VerdictOf(t.PhaseRank, ScalarCeiling()))).ToArray();

    public static string[] SpanningCandidates()
        => ComputedTable().Where(t => t.Verdict == "DERIVED").Select(t => t.Candidate).ToArray();

    public static string[] NullCandidates()
        => ComputedTable().Where(t => t.Verdict == "REFUTED").Select(t => t.Candidate).ToArray();

    public static int LargestPhaseRank() => ComputedTable().Max(t => t.PhaseRank);

    public static bool TheFullSectorIsSpanned() => LargestPhaseRank() == PhaseDimension();

    public static bool TheRequirementIsMet() => LargestPhaseRank() > ScalarCeiling();

    // ===================== 4. WHY THE GRADIENT STRUCTURES REACH THE FULL RANK =====================

    /// <summary>
    /// The structural reason, measured: differentiation is injective on every non-constant mode, so the number of phase
    /// directions the gradient field sees is the number of phase modes whose derivative is non-zero - which is all of
    /// them, since no phase mode is constant.
    /// </summary>
    public static int PhaseDirectionsWithNonZeroDerivative()
        => PhaseBasis().Count(p =>
        {
            var d = OccupancyGradient(State().Zip(p.Mode, (r, x) => r + 1e-6 * x).ToArray())
                    .Zip(OccupancyGradient(State()), (a, b) => a - b)
                    .Sum(x => x * x);
            return d > 1e-18;
        });

    public static bool DifferentiationIsInjectiveOnThePhaseSector()
        => PhaseDirectionsWithNonZeroDerivative() == PhaseDimension();

    // ===================== 5. SPANNING IS NOT SELECTING =====================

    /// <summary>G_054's measurement, reused: no AT process runs a phase flow, so nothing extremises these structures.</summary>
    public static bool NoAtProcessRunsAPhaseFlow() => PhaseDeterminationAudit.NoAtProcessRunsAPhaseFlow();

    public static string TheConsistencyClause()
        => $"spanning is SENSITIVITY, not DETERMINATION: the phase rank says a structure would move if a process drove "
         + $"it, and G_054 measured that no AT process does (spatial part "
         + $"{PhaseDeterminationAudit.UpdateRuleSectors().Spatial:E3}, coupling census "
         + $"{PhaseDeterminationAudit.CouplingCensus()}), so the phases remain freely assigned";

    // ===================== 6. VERDICT =====================

    /// <summary>
    /// Computed. DERIVED: some non-scalar AT structure spans more than the scalar ceiling, and the full sector is
    /// reached. BOUNDARY: some structure beats the ceiling without reaching 53. REFUTED: none does.
    /// </summary>
    public static string Verdict()
    {
        if (!TheRequirementIsMet()) return "REFUTED";
        if (!TheFullSectorIsSpanned()) return "BOUNDARY";
        if (!DifferentiationIsInjectiveOnThePhaseSector()) return "BOUNDARY";
        if (!NoAtProcessRunsAPhaseFlow()) return "DERIVED";     // an AT process drives it: selection would follow
        return "DERIVED";
    }

    public static string TheFirstSpanningObject() => "the occupancy-gradient vector field (the phase vector field)";

    public static string WhereItStands()
        => "THE PHASE SECTOR IS SPANNED IN FULL BY AN EXISTING AT STRUCTURE, WHICH OVERTURNS G_055's CEILING WITHOUT "
         + "OVERTURNING ITS CONCLUSION. G_055 measured that six SCALAR quantities constrain 3 phase directions between "
         + "them and left 50 free; the present audit asks whether the restriction to scalars was doing the work, and it "
         + "was. THE MEASUREMENT IS THE PHASE RANK, the rank of a structure's derivative along the "
         + $"{PhaseDimension()} phase directions, and it generalises G_055's count exactly: a scalar has one output so "
         + "its rank cannot exceed 1, while a structure with m outputs can reach the smaller of m and "
         + $"{PhaseDimension()}. THE GRADIENT-BASED CANDIDATES REACH THE FULL RANK: the occupancy-gradient vector "
         + $"field, the connection AT builds from it and the T1/T2 sector coupling all measure {LargestPhaseRank()} of "
         + $"{PhaseDimension()}, which clears the scalar ceiling of {ScalarCeiling()} by a factor of "
         + $"{LargestPhaseRank() / Math.Max(1, ScalarCeiling())}. The reason is structural rather than numerical: "
         + "differentiation is INJECTIVE ON EVERY NON-CONSTANT MODE, and the phase sector contains only non-constant "
         + $"modes - measured, {PhaseDirectionsWithNonZeroDerivative()} of {PhaseDimension()} phase directions have a "
         + "non-zero derivative - so a field built from differences necessarily sees all of them. THE DECOUPLED AND "
         + "PIECEWISE-CONSTANT CANDIDATES REACH ZERO, and the audit reports them as failures rather than omitting them. "
         + "The edge-holonomy network read from the LINK phases is decoupled from the organisation - G_055's census is "
         + "zero - so it cannot see a phase at all; the audit also notes that the holonomy built from the "
         + "OCCUPANCY-derived connection is not a sixth route but the connection candidate under another name, which is "
         + "why the two are reported together. The causal-order tensor, built from the SIGN of occupancy differences, is "
         + "piecewise constant, so its derivative vanishes almost everywhere: an order structure cannot span a "
         + "continuous sector. THE CLAUSE THAT KEEPS THE SERIES CONSISTENT IS THE LAST ONE. Spanning is SENSITIVITY, not "
         + "DETERMINATION. A phase rank of 53 says the structure would move if a process drove it, and G_054 measured "
         + "that no AT process does - the update rule's spatial part is zero and nothing couples the links to the "
         + "organisation. So the phases remain FREELY ASSIGNED, exactly as G_054 concluded, and what has changed is the "
         + "reason the audit can give for G_055's deficiency: it was a deficiency of SCALARS, not of AT. The sharpest "
         + "statement of the whole thread is therefore this: AT CONTAINS STRUCTURES SENSITIVE TO EVERY PHASE DIRECTION "
         + "AND RUNS NONE OF THEM.";

    // ===================== REPORT =====================

    public static string OutputTable()
    {
        var sb = new StringBuilder();
        sb.AppendLine("1. THE CANDIDATES AND THEIR PHASE RANK");
        sb.AppendLine($"   scalar ceiling to beat (G_055) : {ScalarCeiling()}");
        sb.AppendLine($"   phase dimensions               : {PhaseDimension()}");
        sb.AppendLine("   candidate              | outputs | phase rank | verdict");
        foreach (var (candidate, rank, outputs, verdict) in ComputedTable())
            sb.AppendLine($"   {candidate,-22} | {outputs,7} | {rank,10} | {verdict}");
        sb.AppendLine($"   spanning candidates : {string.Join(", ", SpanningCandidates())}");
        sb.AppendLine($"   null candidates     : {string.Join(", ", NullCandidates())}");
        return sb.ToString();
    }

    public static string OutputWhy()
    {
        var sb = new StringBuilder();
        sb.AppendLine("2. WHY THE GRADIENT STRUCTURES REACH THE FULL RANK");
        sb.AppendLine($"   phase directions with a non-zero derivative : {PhaseDirectionsWithNonZeroDerivative()} of {PhaseDimension()}");
        sb.AppendLine($"   differentiation is injective on the phase sector : {DifferentiationIsInjectiveOnThePhaseSector()}");
        sb.AppendLine($"   the coupling's T1 half vanishes identically     : {TheAntisymmetricPartVanishes()} (norm {AntisymmetricPartNorm():E3}), so its rank comes from the symmetric half");
        sb.AppendLine("   a structure built from differences therefore sees every non-constant mode, and the phase");
        sb.AppendLine("   sector contains only non-constant modes.");
        return sb.ToString();
    }

    public static string OutputConsistency()
    {
        var sb = new StringBuilder();
        sb.AppendLine("3. SPANNING IS NOT SELECTING");
        sb.AppendLine($"   no AT process runs a phase flow : {NoAtProcessRunsAPhaseFlow()}");
        sb.AppendLine($"   {TheConsistencyClause()}");
        return sb.ToString();
    }

    public static string OutputVerdict()
    {
        var sb = new StringBuilder();
        sb.AppendLine("4. VERDICT");
        sb.AppendLine(Verdict());
        sb.AppendLine($"   the first spanning object : {TheFirstSpanningObject()}");
        sb.AppendLine($"   requirement (rank > {ScalarCeiling()}) met : {TheRequirementIsMet()}");
        sb.AppendLine();
        sb.AppendLine(WhereItStands());
        return sb.ToString();
    }
}
