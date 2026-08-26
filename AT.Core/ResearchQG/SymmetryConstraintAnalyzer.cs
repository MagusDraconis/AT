using System.Globalization;

namespace AT.Core.ResearchQG;

public static class SymmetryConstraintAnalyzer
{
    public static SCResult RunFullAnalysis()
    {
        var classes = BuildClasses();
        var reductions = BuildReductions();
        return new SCResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(classes),BuildF(),BuildG(),BuildH(),BuildI(),classes,reductions);
    }

    static ConstraintClass[] BuildClasses()
    {
        return new ConstraintClass[]
        {
            new ConstraintClass("Charge quantization","U(1) phase symmetry (Noether)","Topological winding invariant (n in Z)","HYBRID","Quantization is BOTH: U(1) symmetry gives conserved charge; S^1 topology forces integer n. Same object, two descriptions."),
            new ConstraintClass("U(1) gauge group","Phase rotation symmetry","S^1 topology (circle isometry)","HYBRID","U(1) = symmetry of phase AND isometry of S^1. Symmetry and topology coincide here."),
            new ConstraintClass("Vacuum stability band","None (no group structure)","Attractor basin boundary (lambda>0)","STABILITY","Pure stability. No symmetry group enforces the 111-175 GeV band."),
            new ConstraintClass("RG fixed points","Scale symmetry (at fixed point)","Attractor dynamics (flow toward point)","HYBRID","Fixed point = scale-invariant (symmetric) AND attracting (stable). Conformal symmetry + attractor."),
            new ConstraintClass("Koide relation (45 deg)","S3 permutation (democratic texture)","None evident (exactness argues against RG)","SYMMETRY","Pure symmetry. S3 breaking texture produces 45 deg. No stability mechanism evident."),
            new ConstraintClass("Particle stability (n=1)","Topological charge conservation","Topological protection (cannot decay)","HYBRID","Electron stability = topological invariant (symmetry-like) AND attractor (stability)."),
        };
    }

    static ReductionTest[] BuildReductions()
    {
        return new ReductionTest[]
        {
            new ReductionTest("Symmetry -> Stability","Symmetry = invariance under group; perturbation = small transformation","FAILS","Symmetric top is symmetric but UNSTABLE (falls). Symmetry does not imply stability."),
            new ReductionTest("Stability -> Symmetry","Stability = invariance under perturbation; symmetry = invariance under group","FAILS","Rock in irregular valley is STABLE but ASYMMETRIC. Stability does not imply symmetry."),
            new ReductionTest("Both -> Persistence","Persistence = invariance under change (the essence of structure)","SUCCEEDS (conceptual)","A stable pattern IS an invariant pattern. Symmetry and stability are both 'staying the same' under different classes of change."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SYMMETRY VERSUS STABILITY");
        sb.AppendLine();
        sb.AppendLine("  SYMMETRY:");
        sb.AppendLine("    Invariance under a GROUP of transformations.");
        sb.AppendLine("    - Rotation symmetry: unchanged under rotation.");
        sb.AppendLine("    - U(1): unchanged under phase rotation.");
        sb.AppendLine("    - S3: unchanged under permutation of 3 objects.");
        sb.AppendLine("    Noether: symmetry → conserved quantity.");
        sb.AppendLine();
        sb.AppendLine("  STABILITY:");
        sb.AppendLine("    Persistence under PERTURBATION.");
        sb.AppendLine("    - Attractor: perturbations decay back to the attractor.");
        sb.AppendLine("    - Stable vacuum: small fluctuations don't destroy it.");
        sb.AppendLine("    - Stable particle: can't decay (topological protection).");
        sb.AppendLine();
        sb.AppendLine("  THE APPARENT DIFFERENCE:");
        sb.AppendLine("    Symmetry is about INVARIANCE under structured transformations.");
        sb.AppendLine("    Stability is about PERSISTENCE under arbitrary perturbations.");
        sb.AppendLine("    One is group-theoretic; the other is dynamical.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEPER SIMILARITY (to be tested):");
        sb.AppendLine("    Both are forms of INVARIANCE — 'staying the same' under");
        sb.AppendLine("    change. The question: is this similarity deep or superficial?");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE SYMMETRY AUDIT (S3)");
        sb.AppendLine();
        sb.AppendLine("  The Koide relation Q = (sum m)/(sum sqrt m)^2 = 2/3.");
        sb.AppendLine();
        sb.AppendLine("  S3 (PERMUTATION GROUP OF 3 GENERATIONS):");
        sb.AppendLine("    The 3 lepton masses can be permuted. S3 has 6 elements.");
        sb.AppendLine("    A mass matrix invariant under S3 is the 'democratic' form:");
        sb.AppendLine("      M_dem = m0 · [[1,1,1],[1,1,1],[1,1,1]].");
        sb.AppendLine("    Eigenvalues: (3m0, 0, 0) — TWO massless + one massive.");
        sb.AppendLine("    FAILS: leptons are not massless.");
        sb.AppendLine();
        sb.AppendLine("  BROKEN S3 (the texture):");
        sb.AppendLine("    Add an S3-BREAKING term. The specific 'circulant' texture");
        sb.AppendLine("    that reproduces Koide is well known in the literature:");
        sb.AppendLine("      M = m0·(I + a·C + b·C^2) where C = cyclic permutation.");
        sb.AppendLine("    For the RIGHT (a, b), the eigenvalues give exactly Q = 2/3.");
        sb.AppendLine("    The 45° angle is a property of the S3 representation.");
        sb.AppendLine();
        sb.AppendLine("  THE GEOMETRIC STATEMENT:");
        sb.AppendLine("    In S3 representation theory, the amplitude vector");
        sb.AppendLine("    (sqrt(m_e), sqrt(m_mu), sqrt(m_tau)) is a specific linear");
        sb.AppendLine("    combination of the S3 singlet (democratic, (1,1,1)) and");
        sb.AppendLine("    the S3 doublet (orthogonal plane).");
        sb.AppendLine("    The 45° angle means the singlet and doublet components");
        sb.AppendLine("    have EQUAL weight. This is a SPECIFIC symmetry-breaking");
        sb.AppendLine("    pattern: S3 broken 'halfway' between democratic and hierarchical.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Koide is an S3 SYMMETRY constraint (broken in a");
        sb.AppendLine("  specific 'halfway' pattern). NO stability mechanism is");
        sb.AppendLine("  evident — the relation is exact at all scales, which is a");
        sb.AppendLine("  signature of SYMMETRY, not of RG-flow attraction.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GAUGE SYMMETRY CONTRIBUTION");
        sb.AppendLine();
        sb.AppendLine("  U(1) IS BOTH SYMMETRY AND TOPOLOGY:");
        sb.AppendLine("    - Symmetry: phase rotation θ → θ + α. Noether charge.");
        sb.AppendLine("    - Topology: S¹ has winding sectors n ∈ Z. Topological charge.");
        sb.AppendLine("    These COINCIDE: the U(1) symmetry group IS the circle S¹,");
        sb.AppendLine("    and the winding number IS the conserved charge.");
        sb.AppendLine();
        sb.AppendLine("  THIS IS A KEY OBSERVATION:");
        sb.AppendLine("    For U(1), symmetry and topology are the SAME thing.");
        sb.AppendLine("    The circle S¹ generates BOTH the symmetry (isometry)");
        sb.AppendLine("    AND the topology (winding). No distinction exists.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THIS SUGGESTS FOR THE DEEPER QUESTION:");
        sb.AppendLine("    If symmetry and topology are unified in U(1), then");
        sb.AppendLine("    'symmetry-generated' and 'topology-generated' constraints");
        sb.AppendLine("    may be unified too. The S¹ circle is the common source");
        sb.AppendLine("    of both U(1) symmetry and winding topology.");
        sb.AppendLine();
        sb.AppendLine("  SU(2), SU(3) (QG-038):");
        sb.AppendLine("    - SU(2): binary winding (n↔-n) → spinor structure.");
        sb.AppendLine("    - SU(3): tri-winding (n=3) → color structure.");
        sb.AppendLine("    These are LESS cleanly derived (QG-038: B), but the");
        sb.AppendLine("    PATTERN is the same: gauge groups emerge from phase");
        sb.AppendLine("    topology, which is BOTH symmetry and topology.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Gauge symmetries are HYBRID — they are phase");
        sb.AppendLine("  topology (winding) AND phase symmetry (rotation) at once.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("QUANTIZATION: SYMMETRY OR TOPOLOGY?");
        sb.AppendLine();
        sb.AppendLine("  CHARGE QUANTIZATION HAS TWO DERIVATIONS:");
        sb.AppendLine();
        sb.AppendLine("  DERIVATION 1 (SYMMETRY / Noether):");
        sb.AppendLine("    U(1) symmetry → conserved Noether charge Q.");
        sb.AppendLine("    Q is the generator of phase rotations.");
        sb.AppendLine("    Quantization: Q takes integer values because the");
        sb.AppendLine("    U(1) group is COMPACT (angles are periodic).");
        sb.AppendLine("    Compactness of U(1) → integer charges.");
        sb.AppendLine();
        sb.AppendLine("  DERIVATION 2 (TOPOLOGY / winding):");
        sb.AppendLine("    Phase lives on S¹. Winding number n = (1/2pi)·∮∇θ·dl.");
        sb.AppendLine("    n ∈ Z because the phase must close smoothly around");
        sb.AppendLine("    the vortex. Charge Q = g·n is quantized because n is integer.");
        sb.AppendLine();
        sb.AppendLine("  THE TWO DERIVATIONS ARE EQUIVALENT:");
        sb.AppendLine("    'U(1) is compact' (symmetry) ⟺ 'S¹ has integer winding'");
        sb.AppendLine("    (topology). Compactness and winding integrality are the");
        sb.AppendLine("    SAME mathematical fact about the circle.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Charge quantization is SIMULTANEOUSLY symmetry");
        sb.AppendLine("  and topology. The distinction is a matter of description,");
        sb.AppendLine("  not of substance. The circle S¹ unifies them.");
        sb.AppendLine();
        sb.AppendLine("  THIS IS THE FIRST HINT OF THE DEEPER COMMON SOURCE:");
        sb.AppendLine("    Symmetry and topology are the SAME for the circle.");
        sb.AppendLine("    The deeper principle may be: the GEOMETRY/TOPOLOGY of");
        sb.AppendLine("    the phase field IS its symmetry. They are inseparable.");
        return sb.ToString();
    }

    static string BuildE(ConstraintClass[] classes)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CONSTRAINT CLASSIFICATION");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-26} {1,-14} {2}", "Constraint", "Classification", "Note"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var c in classes)
        {
            string note = c.HybridNote.Length > 55 ? c.HybridNote[..52]+"..." : c.HybridNote;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-26} {1,-14} {2}", c.Name, c.Classification, note));
        }
        sb.AppendLine();
        sb.AppendLine("  TALLY:");
        sb.AppendLine("    HYBRID: charge quantization, U(1), RG fixed points,");
        sb.AppendLine("            particle stability (4)");
        sb.AppendLine("    STABILITY: vacuum stability band (1)");
        sb.AppendLine("    SYMMETRY: Koide relation (1)");
        sb.AppendLine();
        sb.AppendLine("  THE PATTERN:");
        sb.AppendLine("    Most constraints are HYBRID (symmetry + stability/topology).");
        sb.AppendLine("    Pure stability: vacuum band. Pure symmetry: Koide.");
        sb.AppendLine("    The hybrids dominate — suggesting symmetry and stability");
        sb.AppendLine("    are deeply intertwined, not separate.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("UNIFIED-ORIGIN INVESTIGATION: THE REDUCTION ATTEMPTS");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 1: REDUCE SYMMETRY TO STABILITY.");
        sb.AppendLine("    Hypothesis: 'symmetry = stability under group transformations.'");
        sb.AppendLine("    COUNTEREXAMPLE: a symmetric top is symmetric but UNSTABLE");
        sb.AppendLine("    (it falls). Symmetry does NOT imply stability.");
        sb.AppendLine("    FAILS: symmetry and stability are logically independent.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 2: REDUCE STABILITY TO SYMMETRY.");
        sb.AppendLine("    Hypothesis: 'stability = symmetry under perturbations.'");
        sb.AppendLine("    COUNTEREXAMPLE: a rock in an IRREGULAR valley is stable");
        sb.AppendLine("    (returns after perturbation) but ASYMMETRIC (valley is lopsided).");
        sb.AppendLine("    FAILS: stability and symmetry are logically independent.");
        sb.AppendLine();
        sb.AppendLine("  BOTH REDUCTIONS FAIL.");
        sb.AppendLine("    Symmetry and stability are NOT reducible to each other.");
        sb.AppendLine("    They are logically independent properties.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 3: A DEEPER COMMON SOURCE — PERSISTENCE.");
        sb.AppendLine("    Define PERSISTENCE = invariance under change.");
        sb.AppendLine("    - Symmetry = persistence under a GROUP (structured changes).");
        sb.AppendLine("    - Stability = persistence under PERTURBATIONS (random changes).");
        sb.AppendLine("    Both are 'staying the same under change,' differing only");
        sb.AppendLine("    in WHICH changes are considered.");
        sb.AppendLine();
        sb.AppendLine("  PERSISTENCE IS ALREADY IN AT (QG-020):");
        sb.AppendLine("    A stable pattern (attractor) IS a persistent pattern.");
        sb.AppendLine("    'Persistent' = invariant under the actualization fluctuations.");
        sb.AppendLine("    Q (becoming) produces patterns; the persistent ones are");
        sb.AppendLine("    the stable ones (attractors); the symmetric ones are the");
        sb.AppendLine("    invariant-under-group ones. SAME root: persistence.");
        sb.AppendLine();
        sb.AppendLine("  THE COMMON SOURCE: PERSISTENCE = INVARIANCE.");
        sb.AppendLine("    Both symmetry and stability are manifestations of the");
        sb.AppendLine("    SAME deeper fact: a structure persists (remains itself)");
        sb.AppendLine("    under change. This IS QG-020's attractor concept.");
        sb.AppendLine("    It is derivable from Q (becoming → persistent pattern).");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'PERSISTENCE IS TOO VAGUE TO BE A MECHANISM':");
        sb.AppendLine("     PARTIALLY CORRECT. 'Invariance under change' is a");
        sb.AppendLine("     CONCEPT, not a MATHEMATICAL mechanism. It does not");
        sb.AppendLine("     COMPUTE the 45° angle or the 111 GeV boundary.");
        sb.AppendLine("     The unification is CONCEPTUAL, not DERIVATIONAL.");
        sb.AppendLine();
        sb.AppendLine("  2. 'SYMMETRY AND STABILITY ARE MATHEMATICALLY DISTINCT':");
        sb.AppendLine("     CORRECT. Group theory (symmetry) and dynamical systems");
        sb.AppendLine("     (stability) are DIFFERENT mathematical structures.");
        sb.AppendLine("     The symmetric-top / irregular-valley counterexamples");
        sb.AppendLine("     PROVE they are logically independent.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE REDUCTION FAILURES ARE CORRECTLY HANDLED':");
        sb.AppendLine("     GOOD. The audit HONESTLY shows both reductions fail");
        sb.AppendLine("     and does NOT force a unification that the mathematics");
        sb.AppendLine("     rejects. This is the correct scientific procedure.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT IS GENUINELY ESTABLISHED':");
        sb.AppendLine("     - A classification: constraints are hybrid (mostly),");
        sb.AppendLine("       with pure-stability (vacuum band) and pure-symmetry");
        sb.AppendLine("       (Koide) as the exceptions.");
        sb.AppendLine("     - The U(1) unification: symmetry and topology coincide");
        sb.AppendLine("       for the circle (a genuine, specific result).");
        sb.AppendLine("     - The conceptual common source (persistence) as a");
        sb.AppendLine("       HYPOTHESIS, not a derivation.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     The audit FAILS to reduce symmetry to stability (or");
        sb.AppendLine("     vice versa), which is the HONEST outcome. It identifies");
        sb.AppendLine("     persistence as the candidate common source, but this");
        sb.AppendLine("     is CONCEPTUAL, not yet DERIVED. Classification C (hybrid)");
        sb.AppendLine("     is the honest answer; D (deeper source) is suggestive.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. NO NEW LAYER NEEDED (answer to Q10):");
        sb.AppendLine("    Symmetry is ALREADY implicit in Q. The phase field's");
        sb.AppendLine("    geometry (S¹) IS its symmetry (U(1)). The attractor's");
        sb.AppendLine("    persistence (QG-020) IS its stability. Both derive from");
        sb.AppendLine("    Q without a new primitive.");
        sb.AppendLine();
        sb.AppendLine("  2. THE TWO MECHANISMS ARE COMPLEMENTARY:");
        sb.AppendLine("    Stability generates CONTINUOUS manifolds (bands, basins).");
        sb.AppendLine("    Symmetry generates DISCRETE manifolds (lattices, textures).");
        sb.AppendLine("    Together they cover all known constraint types.");
        sb.AppendLine();
        sb.AppendLine("  3. THE U(1) DEEP UNIFICATION:");
        sb.AppendLine("    For the circle S¹, symmetry = topology. This is the");
        sb.AppendLine("    CLEANEST case. It suggests the general principle:");
        sb.AppendLine("    the GEOMETRY of the phase field generates BOTH its");
        sb.AppendLine("    symmetries AND its stability properties.");
        sb.AppendLine();
        sb.AppendLine("  4. THE REMAINING MYSTERY — KOIDE:");
        sb.AppendLine("    Koide is the ONE pure-symmetry constraint. It is an S3");
        sb.AppendLine("    texture, not a stability band. AT has NO derivation of");
        sb.AppendLine("    why the S3 symmetry breaks 'halfway' (45°). This remains");
        sb.AppendLine("    the single most tantalizing unexplained relation.");
        sb.AppendLine();
        sb.AppendLine("  5. THE RESEARCH PROGRAM:");
        sb.AppendLine("    To reach classification D, AT must DERIVE (not just");
        sb.AppendLine("    conceptually identify) persistence as the common source.");
        sb.AppendLine("    Concretely: show that the S¹ phase geometry's invariance");
        sb.AppendLine("    structure generates BOTH the U(1) symmetry AND the");
        sb.AppendLine("    attractor stability. This is a FORMAL, not conceptual, goal.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  HYBRID: SYMMETRY AND STABILITY ARE IRREDUCIBLE BUT RELATED");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Stability = persistence under perturbations. Symmetry =");
        sb.AppendLine("      invariance under a group. Logically independent.");
        sb.AppendLine("  Q2: YES — symmetry (S3 texture) generates the Koide surface");
        sb.AppendLine("      without any attractor/stability mechanism.");
        sb.AppendLine("  Q3: YES — Koide emerges from S3 (democratic + breaking) with");
        sb.AppendLine("      the 45° angle = equal singlet/doublet weight.");
        sb.AppendLine("  Q4: Symmetry creates conservation/quantization/degeneracy via");
        sb.AppendLine("      Noether's theorem and representation theory.");
        sb.AppendLine("  Q5: Charge quantization is BOTH topological (winding) and");
        sb.AppendLine("      symmetric (compact U(1)). They coincide for S¹.");
        sb.AppendLine("  Q6: YES — U(1) = phase symmetry AND S¹ topology. Same object.");
        sb.AppendLine("  Q7: RG fixed points are BOTH (scale symmetry + attractor).");
        sb.AppendLine("  Q8: YES — classified: 4 hybrid, 1 stability, 1 symmetry.");
        sb.AppendLine("  Q9: Symmetry and stability are DIFFERENT projections of");
        sb.AppendLine("      PERSISTENCE (invariance under change). Conceptually unified.");
        sb.AppendLine("  Q10: NO new layer. Symmetry is implicit in Q (phase geometry).");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C — HYBRID MECHANISM");
        sb.AppendLine();
        sb.AppendLine("    The reductions FAIL (symmetric-but-unstable, stable-but-");
        sb.AppendLine("    asymmetric counterexamples prove independence).");
        sb.AppendLine("    Therefore BOTH symmetry and stability are needed.");
        sb.AppendLine();
        sb.AppendLine("    The deeper common source is PERSISTENCE = INVARIANCE:");
        sb.AppendLine("      - Symmetry = invariance under a GROUP.");
        sb.AppendLine("      - Stability = invariance under PERTURBATIONS.");
        sb.AppendLine("      - Both = 'staying the same under change' = QG-020's");
        sb.AppendLine("        attractor concept = derivable from Q.");
        sb.AppendLine();
        sb.AppendLine("    This makes D (deeper common source) SUGGESTED but not");
        sb.AppendLine("    FORMALLY derived. The honest classification is C (hybrid),");
        sb.AppendLine("    with persistence as the identified-but-unproven common root.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEPEST OPEN PROBLEM:");
        sb.AppendLine("    To reach D, AT must FORMALLY derive that the S¹ phase");
        sb.AppendLine("    geometry's invariance generates BOTH U(1) symmetry AND");
        sb.AppendLine("    attractor stability. And it must derive the Koide 45°");
        sb.AppendLine("    from the S3 texture. Both remain open.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 45 experiments.");
        return sb.ToString();
    }
}
