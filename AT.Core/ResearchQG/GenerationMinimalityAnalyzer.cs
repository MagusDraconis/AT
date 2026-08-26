using System.Globalization;

namespace AT.Core.ResearchQG;

public static class GenerationMinimalityAnalyzer
{
    public static GSMResult RunFullAnalysis()
    {
        var approaches = BuildApproaches();
        var dims = BuildDimensions();
        return new GSMResult(BuildA(),BuildB(approaches),BuildC(),BuildD(),BuildE(),BuildF(dims),BuildG(),BuildH(),BuildI(),approaches,dims);
    }

    static MinimalStructure[] BuildApproaches()
    {
        return new MinimalStructure[]
        {
            new MinimalStructure("Topology only (S¹)","U(1), integer winding, Fourier modes","NO 3-fold structure, NO S3 (QG-051)","FAILS: S¹ cannot generate generations."),
            new MinimalStructure("Symmetry only (S3 by hand)","Permutation of 3, singlet/doublet split","NO mass values, NO 'why 3'","INSUFFICIENT: S3 is automatic for 3, but 3 is assumed."),
            new MinimalStructure("Attractors only (QG-020)","Stable pattern families (branches)","NO count (branches not fixed)","INSUFFICIENT: attractor branches exist but number underived."),
            new MinimalStructure("Architecture only (QG-028)","Excitation levels of one vortex","NO count (levels not fixed)","INSUFFICIENT: excitation levels exist but 3 underived."),
            new MinimalStructure("Generation space G (3D internal)","3 copies of the architecture; S3 automatic; masses = eigenvalues; mixing = rotations","Dimension 3 and the 45° are STILL not derived","MINIMAL: 3D internal space is the SMALLEST structure that works."),
        };
    }

    static GenDim[] BuildDimensions()
    {
        return new GenDim[]
        {
            new GenDim(1,"Trivial","No mixing","No","FAILS: 1 generation, no atoms, empty universe."),
            new GenDim(2,"S2 = Z2","1 real angle","No (2x2 CKM real)","FAILS: no CP violation, no baryogenesis, empty universe."),
            new GenDim(3,"S3 (non-abelian)","CKM 3x3 (1 phase)","YES","OBSERVED: minimum with CP violation. Matter survives."),
            new GenDim(4,"S4","CKM 4x4 (3 phases)","YES","EXCLUDED: Z-width N_nu=3, Higgs production."),
            new GenDim(5,"S5","CKM 5x5","YES","EXCLUDED: no evidence."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHY GENERATIONS ARE STILL UNEXPLAINED");
        sb.AppendLine();
        sb.AppendLine("  QG-051 established: S¹ topology does NOT generate S3 or '3'.");
        sb.AppendLine("  The generation structure is an INDEPENDENT layer.");
        sb.AppendLine();
        sb.AppendLine("  THE REMAINING QUESTION:");
        sb.AppendLine("    What is the MINIMAL additional structure needed for");
        sb.AppendLine("    3 generations + S3 + Koide + mixing to exist?");
        sb.AppendLine();
        sb.AppendLine("  WHAT MUST BE EXPLAINED:");
        sb.AppendLine("    1. A generation index (which 'copy' a particle is).");
        sb.AppendLine("    2. Exactly 3 copies (e/mu/tau, u/c/t, d/s/b).");
        sb.AppendLine("    3. S3 permutation of the 3 copies.");
        sb.AppendLine("    4. The mass hierarchy (Koide 45°).");
        sb.AppendLine("    5. Mixing between sectors (CKM, PMNS).");
        sb.AppendLine();
        sb.AppendLine("  THE CANDIDATE ANSWER (to be tested):");
        sb.AppendLine("    A 3-DIMENSIONAL GENERATION SPACE G (internal, like flavor");
        sb.AppendLine("    space) is the minimal structure. S3 = its permutation group.");
        return sb.ToString();
    }

    static string BuildB(MinimalStructure[] approaches)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MINIMAL STRUCTURE ANALYSIS: WHAT FAILS");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-24} {2}", "Approach", "Gives", "Verdict"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var a in approaches)
        {
            string g = a.WhatItGives.Length > 24 ? a.WhatItGives[..21]+"..." : a.WhatItGives;
            string v = a.Verdict.Length > 45 ? a.Verdict[..42]+"..." : a.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-24} {2}", a.Approach, g, v));
        }
        sb.AppendLine();
        sb.AppendLine("  THE REDUCTION FAILS:");
        sb.AppendLine("    - Topology (S¹): no 3-fold (QG-051).");
        sb.AppendLine("    - Symmetry (S3): assumes 3, doesn't derive it.");
        sb.AppendLine("    - Attractors: branches exist, count underived.");
        sb.AppendLine("    - Architecture: levels exist, count underived.");
        sb.AppendLine("    - ONLY a 3D generation space G works — and even it");
        sb.AppendLine("      leaves the dimension (3) and the 45° underived.");
        sb.AppendLine();
        sb.AppendLine("  THE MINIMAL CONCLUSION:");
        sb.AppendLine("    Generations require a 3D INTERNAL GENERATION SPACE.");
        sb.AppendLine("    This is the SMALLEST structure that supports 3 copies,");
        sb.AppendLine("    S3 permutation, and mixing. It is an ADDITIONAL layer,");
        sb.AppendLine("    NOT derivable from S¹, spacetime, or Q.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("GENERATION-SPACE CONSTRUCTION");
        sb.AppendLine();
        sb.AppendLine("  THE GENERATION SPACE G:");
        sb.AppendLine("    - An abstract INTERNAL space (like color space, but for");
        sb.AppendLine("      generations, not color).");
        sb.AppendLine("    - Dimension: 3 (one axis per generation).");
        sb.AppendLine("    - Symmetry: S3 (permutation of the 3 axes).");
        sb.AppendLine();
        sb.AppendLine("  OBSERVABLES OF G:");
        sb.AppendLine("    - Masses = EIGENVALUES of the Yukawa matrix acting on G.");
        sb.AppendLine("      (The 3 generations are the 3 eigenvectors.)");
        sb.AppendLine("    - Mixing = ROTATIONS between the G-spaces of different");
        sb.AppendLine("      sectors (up vs down → CKM; lepton vs neutrino → PMNS).");
        sb.AppendLine("    - Koide 45° = the angle of the mass-eigenvector with the");
        sb.AppendLine("      S3-democratic direction (1,1,1) in G.");
        sb.AppendLine();
        sb.AppendLine("  WHY G IS THE MINIMAL STRUCTURE:");
        sb.AppendLine("    - To have 3 copies, you need a 3-dimensional space.");
        sb.AppendLine("    - To permute them, you need S3 (automatic for 3).");
        sb.AppendLine("    - To have mixing, you need rotations in G.");
        sb.AppendLine("    - Nothing LESS supports all three. G is minimal.");
        sb.AppendLine();
        sb.AppendLine("  THE COST (honest):");
        sb.AppendLine("    G is a NEW STRUCTURE — a 'generation space' that AT's");
        sb.AppendLine("    existing primitives (Q, oscillation, phase, S¹) do NOT");
        sb.AppendLine("    supply. It is an independent ontological layer.");
        sb.AppendLine("    But it is MINIMAL: just '3 copies of the architecture',");
        sb.AppendLine("    with no additional dynamics specified.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ATTRACTOR-FAMILY ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  Can generations be ATTRACTOR FAMILIES (branches) of one");
        sb.AppendLine("  architecture (QG-020/039)?");
        sb.AppendLine();
        sb.AppendLine("  THE PICTURE:");
        sb.AppendLine("    A single architecture (e.g., the n=1 vortex) has MULTIPLE");
        sb.AppendLine("    stable attractor branches in its phase space. Each branch");
        sb.AppendLine("    = one generation. The electron is the lowest branch;");
        sb.AppendLine("    muon the next; tau the next.");
        sb.AppendLine();
        sb.AppendLine("  WHAT THIS EXPLAINS:");
        sb.AppendLine("    - WHY generations share quantum numbers (same architecture).");
        sb.AppendLine("    - WHY they differ in mass (different branches/frequencies).");
        sb.AppendLine("    - WHY they decay (de-excitation: higher branch → lower).");
        sb.AppendLine();
        sb.AppendLine("  WHAT IT DOES NOT EXPLAIN:");
        sb.AppendLine("    - WHY exactly 3 branches (could be 1, 2, 4, ...).");
        sb.AppendLine("    - WHY the branch masses are 207x, 17x (no derivation).");
        sb.AppendLine("    - WHY the Koide 45° (the branches' geometric relation).");
        sb.AppendLine();
        sb.AppendLine("  THE RELATION TO G (generation space):");
        sb.AppendLine("    The attractor branches are POINTS in G (the 3 axes).");
        sb.AppendLine("    So the attractor-family picture is a DESCRIPTION of G's");
        sb.AppendLine("    content, not an ALTERNATIVE to G. G is the space;");
        sb.AppendLine("    attractor branches are its 3 populated points.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Attractor families are COHERENT (and AT-natural),");
        sb.AppendLine("  but they need G (the 3D space) to live in. G remains minimal.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MIXING INTERPRETATION: ROTATIONS IN GENERATION SPACE");
        sb.AppendLine();
        sb.AppendLine("  CKM and PMNS = rotations in generation space G.");
        sb.AppendLine();
        sb.AppendLine("  THE GEOMETRIC PICTURE:");
        sb.AppendLine("    Each sector has its OWN Yukawa matrix acting on G.");
        sb.AppendLine("    The matrix's eigenvectors define a 'mass basis' in G.");
        sb.AppendLine("    Two sectors (up vs down) have DIFFERENT mass bases.");
        sb.AppendLine("    The ROTATION between their bases = the mixing matrix.");
        sb.AppendLine();
        sb.AppendLine("    CKM = rotation between up and down mass bases (small).");
        sb.AppendLine("    PMNS = rotation between lepton and neutrino bases (large).");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS NATURAL:");
        sb.AppendLine("    If generations are axes of a 3D space G, then 'mixing'");
        sb.AppendLine("    is exactly a rotation in G. No new structure needed —");
        sb.AppendLine("    it is the natural geometric operation on G.");
        sb.AppendLine();
        sb.AppendLine("  WHY THE ANGLES ARE UNEXPLAINED:");
        sb.AppendLine("    The specific rotation angles (CKM 13°/2°/0.2°; PMNS");
        sb.AppendLine("    33°/45°/8°) are INPUTS — the relative orientation of");
        sb.AppendLine("    the two sectors' mass bases. AT does not derive them.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Mixing = rotation in G is a clean, minimal");
        sb.AppendLine("  geometric interpretation. The angles remain empirical.");
        return sb.ToString();
    }

    static string BuildF(GenDim[] dims)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DIMENSION SELECTION: WHY 3D?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,3} {1,-14} {2,-22} {3}", "dim", "Symmetry", "CP violation?", "Status"));
        sb.AppendLine("  " + new string('-', 75));
        foreach (var d in dims)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,3} {1,-14} {2,-22} {3}", d.Dim, d.Symmetry, d.CPViolation, d.Status));
        }
        sb.AppendLine();
        sb.AppendLine("  THE DIMENSION IS NOT DERIVED:");
        sb.AppendLine("    - dim < 3: no CP violation → empty universe (anthropic).");
        sb.AppendLine("    - dim = 3: minimum with CP violation → OBSERVED.");
        sb.AppendLine("    - dim > 3: excluded (Z-width, Higgs production).");
        sb.AppendLine("    The 3D generation space is SELECTED (minimum for CP),");
        sb.AppendLine("    not DERIVED from any AT principle.");
        sb.AppendLine();
        sb.AppendLine("  THE 'WHY 3' ANSWER (honest):");
        sb.AppendLine("    3 = the smallest dimension where a complex phase (CP");
        sb.AppendLine("    violation) is possible in the mixing matrix. This is a");
        sb.AppendLine("    MATHEMATICAL fact (2x2 real, 3x3 has 1 phase), but the");
        sb.AppendLine("    SELECTION of '3' is anthropic, not ontological.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REDUCTION REVIEW: ELIMINATE G?");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: recover 3 generations + S3 + Koide + mixing");
        sb.AppendLine("  WITHOUT a generation space G.");
        sb.AppendLine();
        sb.AppendLine("  REDUCTION 1: 'Generations are just excitation levels.'");
        sb.AppendLine("    FAILS: excitation levels need a COUNT (3) and a space to");
        sb.AppendLine("    index them. The index itself IS a generation space.");
        sb.AppendLine();
        sb.AppendLine("  REDUCTION 2: 'Generations are winding numbers.'");
        sb.AppendLine("    FAILS: e/mu/tau all have n=1 (QG-039). Winding does NOT");
        sb.AppendLine("    distinguish generations.");
        sb.AppendLine();
        sb.AppendLine("  REDUCTION 3: 'Generations are just repeated copies.'");
        sb.AppendLine("    PARTIALLY SUCCEEDS: 3 copies IS the minimal description.");
        sb.AppendLine("    But '3 copies' requires an index (which copy?) — that");
        sb.AppendLine("    index IS the generation space. You cannot avoid it.");
        sb.AppendLine();
        sb.AppendLine("  REDUCTION 4: 'Generations are emergent from QCD.'");
        sb.AppendLine("    FAILS: leptons have no QCD, yet have 3 generations.");
        sb.AppendLine();
        sb.AppendLine("  ALL REDUCTIONS FAIL. G (or its equivalent) is UNAVOIDABLE.");
        sb.AppendLine("    The generation index — a label distinguishing the 3");
        sb.AppendLine("    copies — is a MINIMAL, IRREDUCIBLE structure. AT must");
        sb.AppendLine("    accept it as an additional ontological layer.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G cannot be eliminated. It is the minimal cost.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. AT GAINS A NEW (MINIMAL) STRUCTURE:");
        sb.AppendLine("    The generation space G (3D internal, S3-symmetric) is a");
        sb.AppendLine("    necessary addition. It is NOT derivable from Q, S¹, or");
        sb.AppendLine("    spacetime. This is an HONEST extension of the ontology.");
        sb.AppendLine();
        sb.AppendLine("  2. THE PRIMITIVE COUNT GROWS (slightly):");
        sb.AppendLine("    AT's primitives: Q + Random Actualization + (ℓ, τ, ħ).");
        sb.AppendLine("    Add: the generation space G (dimension 3).");
        sb.AppendLine("    This is a MINIMAL addition — just '3 copies exist'.");
        sb.AppendLine("    But it is a genuine new structure, not a derived one.");
        sb.AppendLine();
        sb.AppendLine("  3. WHAT G EXPLAINS (once accepted):");
        sb.AppendLine("    - 3 generations (dimension).");
        sb.AppendLine("    - S3 (permutation symmetry).");
        sb.AppendLine("    - Mixing (rotations in G).");
        sb.AppendLine("    - Koide 45° (a specific direction in G, value unexplained).");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT G STILL LEAVES OPEN:");
        sb.AppendLine("    - WHY dimension = 3 (selection, not derivation).");
        sb.AppendLine("    - WHY 45° (the balanced direction).");
        sb.AppendLine("    - The specific Yukawa eigenvalues (masses).");
        sb.AppendLine();
        sb.AppendLine("  5. THE HONEST POSITION:");
        sb.AppendLine("    The generation space G is the MINIMAL additional layer");
        sb.AppendLine("    required by experiment. Accepting it is a COST (a new");
        sb.AppendLine("    structure), but refusing it is IMPOSSIBLE (reductions fail).");
        sb.AppendLine("    AT is more honest WITH G than without it.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  GENERATIONS REQUIRE A MINIMAL 3D GENERATION SPACE G");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: The generation index is carried by an axis of the 3D");
        sb.AppendLine("      generation space G (internal, like flavor space).");
        sb.AppendLine("  Q2: Generations emerge from G (dimension 3) — NOT from");
        sb.AppendLine("      topology/symmetry/attractors alone (all fail, QG-051).");
        sb.AppendLine("  Q3: Minimal structure = a 3D generation space G with S3");
        sb.AppendLine("      permutation symmetry. Nothing less works.");
        sb.AppendLine("  Q4: Generations preserve charge/spin/gauge numbers because");
        sb.AppendLine("      they are COPIES (same architecture) differing only in");
        sb.AppendLine("      the generation index (their axis in G).");
        sb.AppendLine("  Q5: YES — G is an internal space, independent of spacetime.");
        sb.AppendLine("  Q6: Generations = eigenvectors/attractor-branches IN G.");
        sb.AppendLine("  Q7: G is 3D because there are 3 generations. Why 3? Selection");
        sb.AppendLine("      (minimum for CP violation), not derivation.");
        sb.AppendLine("  Q8: YES — S3 is automatic once G is 3D (permutation of axes).");
        sb.AppendLine("  Q9: YES — CKM/PMNS = rotations between sectors' bases in G.");
        sb.AppendLine("  Q10: Topology-only, symmetry-only, attractor-only, QCD — all");
        sb.AppendLine("      FAIL (QG-051 + this audit). Only G works.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK GENERATION SPACE");
        sb.AppendLine();
        sb.AppendLine("    Generations REQUIRE an independent 3D generation space G.");
        sb.AppendLine("    This is a NEW ontological layer (not derivable from S¹,");
        sb.AppendLine("    spacetime, or Q). QG-051 ruled out the alternatives.");
        sb.AppendLine();
        sb.AppendLine("    G is MINIMAL: it explains 3 generations (dimension),");
        sb.AppendLine("    S3 (permutation), mixing (rotations), and hosts Koide");
        sb.AppendLine("    (a direction in G). But G's dimension (3) and the 45°");
        sb.AppendLine("    value remain UNDERIVED.");
        sb.AppendLine();
        sb.AppendLine("    THE HONEST CONCLUSION:");
        sb.AppendLine("    AT must ADD the generation space G as a minimal new");
        sb.AppendLine("    structure. This is a real COST, but refusing it is");
        sb.AppendLine("    impossible (all reductions fail). The generation");
        sb.AppendLine("    structure is an INDEPENDENT layer of reality.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 52 experiments.");
        return sb.ToString();
    }
}
