using System.Globalization;

namespace AT.Core.ResearchQG;

public static class S1ToS3GeometryAnalyzer
{
    public static S13Result RunFullAnalysis()
    {
        var facts = BuildFacts();
        var modes = BuildModes();
        return new S13Result(BuildA(),BuildB(facts),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(modes),BuildH(),BuildI(),facts,modes);
    }

    static S1Fact[] BuildFacts()
    {
        return new S1Fact[]
        {
            new S1Fact("Symmetry group","U(1) (continuous rotations)","QG-038: S¹ isometry = U(1).","S¹ gives U(1), NOT S3. U(1) is abelian/continuous; S3 is non-abelian/discrete."),
            new S1Fact("Winding numbers","n in Z (integers)","QG-034: ∮∇θ·dl = 2πn.","Integer winding, but NO preferred '3'. The circle has no 3-fold structure."),
            new S1Fact("Fourier modes","e^{inθ}, labeled by ONE integer n","Harmonic analysis of the circle.","Modes are labeled by a SINGLE integer, not a triplet. No natural 3 arises."),
            new S1Fact("Discrete subgroups","Z_N (rotations by 2π/N)","The circle has Z_N subgroups for any N.","Z_3 exists but is abelian, ≠ S3. Z_3 ≠ S3 (S3 is non-abelian, order 6)."),
            new S1Fact("S3 origin","Weyl group of SU(3) (color)","S3 permutes the 3 color axes.","S3 is a COLOR symmetry (SU(3) Weyl), NOT a phase (U(1)) symmetry."),
        };
    }

    static ModeCount[] BuildModes()
    {
        return new ModeCount[]
        {
            new ModeCount(2,"S2 = Z2 (trivial)","No 3-mass Koide (2-mass relation differs)","Possible but no CP violation (empty universe)","NO S¹ preference for 2"),
            new ModeCount(3,"S3 (non-abelian, order 6)","Koide 45° (balanced singlet/doublet)","OBSERVED (CP violation possible)","NO S¹ preference for 3"),
            new ModeCount(4,"S4 (order 24)","No 45° (different geometry)","Excluded (Z-width, Higgs production)","NO S¹ preference for 4"),
            new ModeCount(5,"S5 (order 120)","No 45° (different geometry)","Excluded","NO S¹ preference for 5"),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE S¹ → S3 PROBLEM");
        sb.AppendLine();
        sb.AppendLine("  THE CENTRAL UNRESOLVED QUESTION:");
        sb.AppendLine("    How does a simple S¹ phase topology (one circle)");
        sb.AppendLine("    become a THREE-generation S3 geometry?");
        sb.AppendLine();
        sb.AppendLine("  THE CHAIN TO EXPLAIN:");
        sb.AppendLine("    S¹ (phase circle) → ??? → 3 generations → S3 → Koide 45°.");
        sb.AppendLine();
        sb.AppendLine("  WHAT IS KNOWN:");
        sb.AppendLine("    - S¹ → U(1) symmetry (QG-038). SOLID.");
        sb.AppendLine("    - S¹ → integer winding/charge (QG-034/035). SOLID.");
        sb.AppendLine("    - S¹ → S3? UNKNOWN. This is the gap.");
        sb.AppendLine("    - S3 → Koide 45° (QG-046). Geometric, but S3's origin unknown.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    S¹ gives U(1), NOT S3. U(1) is abelian (commutative),");
        sb.AppendLine("    continuous; S3 is non-abelian, discrete (order 6).");
        sb.AppendLine("    There is NO natural '3' in the circle. The emergence");
        sb.AppendLine("    S¹ → S3 is NOT established and likely FAILS.");
        return sb.ToString();
    }

    static string BuildB(S1Fact[] facts)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("S¹ EXCITATION SPECTRUM: WHAT THE CIRCLE ACTUALLY GIVES");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-18} {1,-26} {2}", "What S¹ gives", "Group", "Relevance"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var f in facts)
        {
            string r = f.Relevance.Length > 40 ? f.Relevance[..37]+"..." : f.Relevance;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-18} {1,-26} {2}", f.WhatS1Gives, f.Group, r));
        }
        sb.AppendLine();
        sb.AppendLine("  THE CRUCIAL NEGATIVE RESULT:");
        sb.AppendLine("    1. S¹'s symmetry is U(1) — continuous, abelian.");
        sb.AppendLine("    2. S3 is discrete, non-abelian, order 6.");
        sb.AppendLine("    3. U(1) has NO 3-fold structure. The circle has no");
        sb.AppendLine("       preferred triple.");
        sb.AppendLine("    4. S¹'s Fourier modes are labeled by ONE integer n,");
        sb.AppendLine("       NOT a triplet. No natural '3' arises.");
        sb.AppendLine("    5. S3 is the Weyl group of SU(3) (color), NOT of U(1).");
        sb.AppendLine();
        sb.AppendLine("  CONCLUSION: S¹ does NOT contain S3. The circle gives");
        sb.AppendLine("  U(1) + integer winding + Fourier modes — but NO 3-fold");
        sb.AppendLine("  structure and NO S3 permutation symmetry.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION EMERGENCE: WHERE COULD '3' COME FROM?");
        sb.AppendLine();
        sb.AppendLine("  CANDIDATE SOURCES OF '3' (all FAIL to derive from S¹):");
        sb.AppendLine();
        sb.AppendLine("  1. S¹ EXCITATION BANDS (QG-039):");
        sb.AppendLine("     A vortex CAN have multiple excitation levels. But the");
        sb.AppendLine("     NUMBER of stable levels is set by the architectural");
        sb.AppendLine("     potential — which AT has NOT specified. S¹ does NOT");
        sb.AppendLine("     fix the count to 3 (could be 1, 3, 5, ...).");
        sb.AppendLine();
        sb.AppendLine("  2. SU(3) COLOR (QG-038):");
        sb.AppendLine("     3 colors. But S3 (color Weyl) acts on COLOR, not on");
        sb.AppendLine("     generations. And leptons (Koide) have NO color. So");
        sb.AppendLine("     the color-S3 cannot explain the generation-S3.");
        sb.AppendLine();
        sb.AppendLine("  3. 3+1 SPACETIME DIMENSIONS:");
        sb.AppendLine("     3 spatial dimensions → 3 polarization modes. But AT");
        sb.AppendLine("     does NOT derive 3+1 (QG-018 gap). The dimensionality");
        sb.AppendLine("     is an external input, not a S¹ result.");
        sb.AppendLine();
        sb.AppendLine("  4. CP VIOLATION MINIMUM (QG-046):");
        sb.AppendLine("     3 = minimum generations for a complex CKM phase.");
        sb.AppendLine("     This is SELECTION (anthropic), not derivation.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: NO candidate derives '3' from S¹. The generation");
        sb.AppendLine("  count is an EXTERNAL input (selection or empirical).");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("S3 EMERGENCE: DOES 3 MODES AUTOMATICALLY GIVE S3?");
        sb.AppendLine();
        sb.AppendLine("  PARTIAL YES: S3 IS AUTOMATIC FOR 3 OBJECTS.");
        sb.AppendLine("    If a system HAS 3 indistinguishable modes, their");
        sb.AppendLine("    permutation group IS S3. This is mathematical automatism");
        sb.AppendLine("    (QG-046). S3 does not need to be 'inserted' — it is the");
        sb.AppendLine("    symmetry of ANY 3-fold structure.");
        sb.AppendLine();
        sb.AppendLine("  BUT: THE '3' ITSELF IS NOT FROM S¹.");
        sb.AppendLine("    S3 is automatic GIVEN 3 objects. But S¹ does not SUPPLY");
        sb.AppendLine("    the 3 objects. The circle has no preferred 3-fold");
        sb.AppendLine("    decomposition. So S¹ → (3 modes) → S3 FAILS at the first");
        sb.AppendLine("    step: S¹ does not produce 3 modes.");
        sb.AppendLine();
        sb.AppendLine("  THE TWO DISTINCT S3's (CRUCIAL):");
        sb.AppendLine("    - COLOR-S3: Weyl group of SU(3), permutes 3 colors.");
        sb.AppendLine("      Acts on QUARKS. Not related to S¹.");
        sb.AppendLine("    - GENERATION-S3: permutes 3 generations (e, mu, tau).");
        sb.AppendLine("      Acts on LEPTONS. Its origin is UNKNOWN.");
        sb.AppendLine("    These are DIFFERENT S3's. The generation-S3 (Koide) is");
        sb.AppendLine("    NOT the color-S3. Neither comes from S¹.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: S3 is automatic for 3 modes, but S¹ does NOT");
        sb.AppendLine("  generate 3 modes. The S¹ → S3 chain is BROKEN at the");
        sb.AppendLine("  first link. S3 (generation) is an UNEXPLAINED structure.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("SINGLET-DOUBLET DECOMPOSITION: NORMALIZATION OR EMERGENCE?");
        sb.AppendLine();
        sb.AppendLine("  S3 representation theory: 3D → 1 singlet + 1 doublet.");
        sb.AppendLine("    - Singlet (1,1,1): the S3-invariant direction.");
        sb.AppendLine("    - Doublet (2D plane): the S3-breaking directions.");
        sb.AppendLine("    Any 3-vector decomposes uniquely into singlet + doublet.");
        sb.AppendLine();
        sb.AppendLine("  IS THIS NORMALIZATION OR EMERGENCE?");
        sb.AppendLine("    The DECOMPOSITION (3D = 1+2) is pure S3 representation");
        sb.AppendLine("    theory — automatic once S3 is present. It follows from");
        sb.AppendLine("    completeness/normalization of the irreducible reps.");
        sb.AppendLine("    So the singlet/doublet SPLIT is NOT 'inserted manually' —");
        sb.AppendLine("    it is the unique decomposition of the 3D space.");
        sb.AppendLine();
        sb.AppendLine("  BUT THE BALANCE (|singlet| = |doublet|) IS NOT AUTOMATIC:");
        sb.AppendLine("    The decomposition always exists, but the RELATIVE WEIGHTS");
        sb.AppendLine("    (how much singlet vs doublet) depend on the actual mass");
        sb.AppendLine("    values. The balance (45°) is a SPECIFIC, non-generic");
        sb.AppendLine("    configuration — NOT forced by the decomposition.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The singlet/doublet DECOMPOSITION is automatic");
        sb.AppendLine("  (S3 rep theory). The BALANCE (45°) is NOT (QG-047).");
        sb.AppendLine("  Neither is derived from S¹ — both require S3, which S¹");
        sb.AppendLine("  does not supply.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE-ANGLE DERIVATION ATTEMPT (MODE GEOMETRY)");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: derive theta = 45° from S¹ mode geometry.");
        sb.AppendLine();
        sb.AppendLine("  STEP 1: S¹ modes are e^{inθ}, n ∈ Z. No triplet, no angle.");
        sb.AppendLine("    FAILS: S¹ gives no 3-dimensional generation space.");
        sb.AppendLine();
        sb.AppendLine("  STEP 2: Even IF 3 modes exist, S3 gives the singlet/doublet");
        sb.AppendLine("    split, but NOT the balance. The angle is a free parameter.");
        sb.AppendLine("    FAILS: S3 gives the decomposition, not the 45°.");
        sb.AppendLine();
        sb.AppendLine("  STEP 3: Mode orthogonality (normalization) forces the");
        sb.AppendLine("    singlet ⟂ doublet, but NOT equal weight. Orthogonality");
        sb.AppendLine("    gives cos²θ + sin²θ = 1, NOT cos²θ = sin²θ = 1/2.");
        sb.AppendLine("    FAILS: orthogonality ≠ balance.");
        sb.AppendLine();
        sb.AppendLine("  STEP 4: 'Mode completeness' (sum over modes) gives a");
        sb.AppendLine("    normalization, but no preferred angle.");
        sb.AppendLine("    FAILS: completeness ≠ 45°.");
        sb.AppendLine();
        sb.AppendLine("  ALL ATTEMPTS FAIL. theta = 45° is NOT derivable from");
        sb.AppendLine("  S¹ topology, S3 decomposition, orthogonality, or");
        sb.AppendLine("  completeness. It requires an ADDITIONAL principle");
        sb.AppendLine("  (the 'balance' condition), which has no known origin.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The 45° is the geometric fingerprint of an");
        sb.AppendLine("  UNKNOWN selection principle, NOT of S¹ topology.");
        return sb.ToString();
    }

    static string BuildG(ModeCount[] modes)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ALTERNATIVE GENERATION STRUCTURES (2, 4, 5 modes)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,-24} {2,-30} {3}", "N", "Symmetry", "Koide analog", "S¹ preference?"));
        sb.AppendLine("  " + new string('-', 85));
        foreach (var m in modes)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,-24} {2,-30} {3}", m.N, m.SymmetryGroup, m.KoideAnalog, m.S1Preference));
        }
        sb.AppendLine();
        sb.AppendLine("  KEY RESULT: S¹ has NO preference for 3 modes.");
        sb.AppendLine("    - 2 modes: S2 (trivial). No Koide. No CP violation.");
        sb.AppendLine("    - 3 modes: S3. Koide 45°. OBSERVED.");
        sb.AppendLine("    - 4 modes: S4. No 45°. Excluded.");
        sb.AppendLine("    - 5 modes: S5. No 45°. Excluded.");
        sb.AppendLine("    The 45° Koide is SPECIFIC to 3 modes (S3). But S¹ does");
        sb.AppendLine("    not prefer 3 over 2, 4, or 5. The '3' is external.");
        sb.AppendLine();
        sb.AppendLine("  WHY 3 IS OBSERVED (NOT DERIVED):");
        sb.AppendLine("    - 2 modes: no CP violation → empty universe (anthropic).");
        sb.AppendLine("    - 4+ modes: excluded by Z-width and Higgs production.");
        sb.AppendLine("    - 3 modes: the minimum with CP violation → survives.");
        sb.AppendLine("    This is SELECTION, not S¹ topology.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE NEGATIVE RESULT IS CORRECT AND IMPORTANT':");
        sb.AppendLine("     AGREED. S¹ → U(1), not S3. U(1) is abelian/continuous;");
        sb.AppendLine("     S3 is non-abelian/discrete. No group-theoretic path");
        sb.AppendLine("     connects them. The '3' is NOT in the circle.");
        sb.AppendLine();
        sb.AppendLine("  2. 'BUT MAYBE THE EXCITATION SPECTRUM SUPPLIES 3':");
        sb.AppendLine("     PARTIAL OBJECTION. A vortex CAN have excitation bands,");
        sb.AppendLine("     but the NUMBER is set by the (unspecified) architectural");
        sb.AppendLine("     potential. S¹ does NOT force '3'. This is a GAP, not");
        sb.AppendLine("     a derivation. The objection confirms, not refutes.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE TWO-S3 DISTINCTION IS CRUCIAL AND CORRECT':");
        sb.AppendLine("     AGREED. Color-S3 (SU(3) Weyl) and generation-S3 are");
        sb.AppendLine("     DIFFERENT. Leptons have no color, so the generation-S3");
        sb.AppendLine("     cannot be the color-S3. Its origin is genuinely unknown.");
        sb.AppendLine();
        sb.AppendLine("  4. 'THE 45° DERIVATION ATTEMPT HONESTLY FAILS':");
        sb.AppendLine("     CORRECT. Orthogonality gives cos²+sin²=1, not the balance");
        sb.AppendLine("     cos²=sin²=1/2. The balance is an ADDITIONAL condition");
        sb.AppendLine("     with no known origin. This is honest and decisive.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE BOTTOM LINE':");
        sb.AppendLine("     S¹ → S3 emergence FAILS. S¹ gives U(1) + winding +");
        sb.AppendLine("     Fourier modes. S3 (generation) and '3' are EXTERNAL.");
        sb.AppendLine("     Classification: B (weak correspondence) — the excitation-");
        sb.AppendLine("     band picture is coherent, but the strict S¹→S3 derivation");
        sb.AppendLine("     is A (no connection).");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  S¹ DOES NOT GENERATE S3 (OR 45°) — THE CHAIN IS BROKEN");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: A S¹ vortex CAN have excitation bands, but S¹ does NOT");
        sb.AppendLine("      fix the number to 3 (could be 1, 3, 5, ...).");
        sb.AppendLine("  Q2: WHY 3 stable levels: NOT derived from S¹. Selection");
        sb.AppendLine("      (3 = minimum for CP violation; 4+ excluded).");
        sb.AppendLine("  Q3: e/mu/tau as eigenmodes of one vortex: COHERENT but the");
        sb.AppendLine("      eigenmode COUNT (3) and VALUES are not derived.");
        sb.AppendLine("  Q4: S¹ (1D) → 3D generation space: FAILS. S¹ gives 1D (angle),");
        sb.AppendLine("      not 3D. The 3D generation space is external.");
        sb.AppendLine("  Q5: From harmonics/modes/attractors: COHERENT pictures, but");
        sb.AppendLine("      none derives '3' or the mass values.");
        sb.AppendLine("  Q6: S3 automatic for 3 modes: YES (trivial). But S¹ does NOT");
        sb.AppendLine("      supply the 3 modes. Chain broken at first link.");
        sb.AppendLine("  Q7: |singlet|=|doublet| from orthogonality: NO. Orthogonality");
        sb.AppendLine("      gives cos²+sin²=1, not the balance cos²=sin²=1/2.");
        sb.AppendLine("  Q8: 45° from mode normalization: NO. All attempts FAIL.");
        sb.AppendLine("  Q9: 45° is NOT topology/geometry/symmetry/completeness. It is");
        sb.AppendLine("      an UNKNOWN selection principle (QG-047).");
        sb.AppendLine("  Q10: Other topologies give 2/4/5 generations. S¹ has NO");
        sb.AppendLine("      preference for 3. The '3' is external.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK CORRESPONDENCE");
        sb.AppendLine();
        sb.AppendLine("    STRICT RESULT: S¹ → S3 → 45° emergence FAILS (A for the");
        sb.AppendLine("    strict question). S¹ gives U(1), integer winding, Fourier");
        sb.AppendLine("    modes — NO 3-fold structure, NO S3, NO 45°.");
        sb.AppendLine();
        sb.AppendLine("    WEAK CORRESPONDENCE: the excitation-band picture (e/mu/tau");
        sb.AppendLine("    as modes of one vortex) is COHERENT, and S3 is automatic");
        sb.AppendLine("    GIVEN 3 modes. But the '3' and the 45° are EXTERNAL.");
        sb.AppendLine();
        sb.AppendLine("    THE CENTRAL RESULT (honest and decisive):");
        sb.AppendLine("    The generation structure (3, S3, 45°) is NOT derived from");
        sb.AppendLine("    S¹ topology. It is an INDEPENDENT structure whose origin");
        sb.AppendLine("    remains the deepest open problem in the AT program.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 51 experiments.");
        return sb.ToString();
    }
}
