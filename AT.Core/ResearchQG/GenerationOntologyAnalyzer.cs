using System.Globalization;

namespace AT.Core.ResearchQG;

public static class GenerationOntologyAnalyzer
{
    public static GOResult RunFullAnalysis()
    {
        var ontologies = BuildOntologies();
        var eliminations = BuildEliminations();
        return new GOResult(BuildA(),BuildB(),BuildC(),BuildD(),BuildE(ontologies),BuildF(),BuildG(eliminations),BuildH(),BuildI(),ontologies,eliminations);
    }

    static GOntology[] BuildOntologies()
    {
        return new GOntology[]
        {
            new GOntology("Bookkeeping (labels)","Just an index i=1,2,3 distinguishing copies","None physical — labels have no dynamics","A: FAILS. Mixing (CKM/PMNS) is REAL rotation, not relabeling."),
            new GOntology("Excitation levels (QG-039)","Frequency bands of one n=1 vortex","Mass hierarchy, decay chains (de-excitation)","B: PARTIAL. Explains masses/dynamics but the COUNT (3) is external."),
            new GOntology("Attractor branches (QG-052)","Stable attractor families of one architecture","Stability, shared quantum numbers","B: PARTIAL. Branches are real, but they LIVE IN G (don't replace it)."),
            new GOntology("Actualization histories","Different random-actualization paths","Reproducibility, contingency","C: SPECULATIVE. Random Actualization could populate G, but no mechanism shown."),
            new GOntology("REAL generation space G","A 3D internal space; Yukawa matrix acts on it; mixing = rotation","CKM/PMNS mixing = REAL rotation in G (neutrino oscillation is physical)","C: STRONG. Mixing is the decisive evidence G is a real space."),
        };
    }

    static Elimination[] BuildEliminations()
    {
        return new Elimination[]
        {
            new Elimination("'Generations = just 3 copies'","The index (which copy) IS G. You cannot have copies without an index.","FAILS: the copy-index is exactly G."),
            new Elimination("'Generations = winding numbers'","e/mu/tau all have n=1. Winding does not distinguish them.","FAILS (QG-039)."),
            new Elimination("'Generations = spacetime states'","Generation is orthogonal to spacetime (same particle, different G-coordinate, same position).","FAILS: G is internal, not spatial."),
            new Elimination("'Generations = QCD/color'","Leptons have no color yet have 3 generations.","FAILS (QG-050)."),
            new Elimination("'Eliminate G entirely'","Mixing (CKM/PMNS) is a REAL rotation; rotation needs a space.","FAILS: mixing requires a real rotation space = G."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHAT IS G ONTOLOGICALLY?");
        sb.AppendLine();
        sb.AppendLine("  QG-052: G (generation space) is UNAVOIDABLE.");
        sb.AppendLine("  QG-053: dim(G)=3 is STRONGLY SELECTED.");
        sb.AppendLine("  QG-054: but WHAT IS G? This is the ontology question.");
        sb.AppendLine();
        sb.AppendLine("  THE CANDIDATE ANSWERS:");
        sb.AppendLine("    1. Bookkeeping: just labels (i=1,2,3).");
        sb.AppendLine("    2. Emergent: excitation levels / attractor branches.");
        sb.AppendLine("    3. Real space: a genuine internal space (like flavor space).");
        sb.AppendLine("    4. Fundamental: an irreducible ontological layer.");
        sb.AppendLine();
        sb.AppendLine("  THE HONEST PREVIEW:");
        sb.AppendLine("    The DECISIVE evidence is MIXING. CKM and PMNS are REAL");
        sb.AppendLine("    rotations (neutrino oscillation is a physical process).");
        sb.AppendLine("    You cannot 'rotate between labels' — rotation requires a");
        sb.AppendLine("    real space. So G is REAL, not bookkeeping.");
        sb.AppendLine("    Its ORIGIN (emergent vs fundamental) remains open.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHAT G STORES (AND WHAT IT DOESN'T)");
        sb.AppendLine();
        sb.AppendLine("  G STORES ONLY THE GENERATION INDEX:");
        sb.AppendLine("    - WHICH copy of the architecture a particle is (1st, 2nd, 3rd).");
        sb.AppendLine("    - The mass (eigenvalue of the Yukawa matrix on G).");
        sb.AppendLine("    - The mixing orientation (rotation angle in G).");
        sb.AppendLine();
        sb.AppendLine("  G DOES NOT STORE (these are G-INDEPENDENT):");
        sb.AppendLine("    - Charge: same across generations (e, mu, tau all -1).");
        sb.AppendLine("    - Spin: same across generations (all spin-1/2).");
        sb.AppendLine("    - Topology: same winding (all n=1, QG-039).");
        sb.AppendLine("    - Gauge quantum numbers: identical across generations.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MATTERS (Q2):");
        sb.AppendLine("    Charge/spin/topology live in the SPACETIME + PHASE sector.");
        sb.AppendLine("    The generation index lives in a SEPARATE internal sector G.");
        sb.AppendLine("    These are ORTHOGONAL: a particle is specified by (position,");
        sb.AppendLine("    spin, charge, GENERATION). The last coordinate is G.");
        sb.AppendLine();
        sb.AppendLine("  THE MINIMAL CONTENT OF G:");
        sb.AppendLine("    One integer (or one axis coordinate): 'which copy'. That's");
        sb.AppendLine("    ALL G stores. It is the SMALLEST possible additional");
        sb.AppendLine("    structure — but it is IRREDUCIBLE (cannot be absorbed).");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CAN G EMERGE FROM EXISTING AT PRIMITIVES?");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPTED EMERGENCE SOURCES (all fail, QG-051/052):");
        sb.AppendLine("    - Q (becoming): gives structure/persistence, not a 3D index.");
        sb.AppendLine("    - Actualization (randomness): gives contingency, not a fixed 3D space.");
        sb.AppendLine("    - Oscillation: gives frequency, not a generation index.");
        sb.AppendLine("    - Architecture: gives excitation levels, but NOT their count.");
        sb.AppendLine("    - S¹ topology: gives U(1)/winding, NOT S3 or '3' (QG-051).");
        sb.AppendLine();
        sb.AppendLine("  THE RESULT:");
        sb.AppendLine("    G does NOT emerge from Q, Random Actualization, oscillation,");
        sb.AppendLine("    architecture, or S¹ topology. It is an INDEPENDENT structure.");
        sb.AppendLine();
        sb.AppendLine("  THE ONE PARTIAL EXCEPTION — ARCHITECTURE:");
        sb.AppendLine("    The excitation-level picture (QG-039) gives the CONTENT of G");
        sb.AppendLine("    (3 frequency bands = 3 generations). But the NUMBER of bands");
        sb.AppendLine("    (3) is not derived — it is an input. So architecture populates");
        sb.AppendLine("    G but does not DERIVE G.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G is NOT emergent from existing primitives.");
        sb.AppendLine("  It is an independent layer. This pushes toward 'fundamental'.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("MIXING: THE DECISIVE EVIDENCE G IS REAL");
        sb.AppendLine();
        sb.AppendLine("  THE KEY ARGUMENT:");
        sb.AppendLine("    Neutrino oscillation is a REAL physical process:");
        sb.AppendLine("    a neutrino created as nu_mu genuinely becomes nu_e as it");
        sb.AppendLine("    propagates. This is MEASURED (Super-Kamiokande, SNO).");
        sb.AppendLine();
        sb.AppendLine("  WHAT OSCILLATION REQUIRES:");
        sb.AppendLine("    Flavor change = a ROTATION between the neutrino mass");
        sb.AppendLine("    eigenstates. Rotation is a GEOMETRIC operation that");
        sb.AppendLine("    requires a SPACE to rotate in.");
        sb.AppendLine("    You cannot 'rotate between labels'. Labels have no");
        sb.AppendLine("    geometry. Rotation requires a real space = G.");
        sb.AppendLine();
        sb.AppendLine("  THE SAME FOR QUARKS:");
        sb.AppendLine("    CKM mixing (quark flavor change in weak decay) is a real");
        sb.AppendLine("    rotation in the quark generation space. The CP-violating");
        sb.AppendLine("    phase is a real GEOMETRIC phase (a Berry-like phase in G).");
        sb.AppendLine();
        sb.AppendLine("  SO G IS REAL, NOT BOOKKEEPING:");
        sb.AppendLine("    Mixing (CKM/PMNS) is real physics. Real physics that");
        sb.AppendLine("    involves rotation requires a real rotation space.");
        sb.AppendLine("    G is that space. It is NOT a mere label — it has geometry");
        sb.AppendLine("    (angles, phases) that are physically observable.");
        sb.AppendLine();
        sb.AppendLine("  THE COST (honest):");
        sb.AppendLine("    G is real but its ORIGIN is unknown. It is an independent");
        sb.AppendLine("    internal space, analogous to (but distinct from) color space.");
        sb.AppendLine("    This is the 'flavor space' of the SM — unexplained there,");
        sb.AppendLine("    and now identified as a genuine AT ontological structure.");
        return sb.ToString();
    }

    static string BuildE(GOntology[] ontologies)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ONTOLOGY INTERPRETATIONS (EVALUATED)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-14} {2}", "Interpretation", "Status", "Evidence"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var o in ontologies)
        {
            string e = o.Evidence.Length > 50 ? o.Evidence[..47]+"..." : o.Evidence;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-14} {2}", o.Interpretation, o.Status, e));
        }
        sb.AppendLine();
        sb.AppendLine("  THE RANKING:");
        sb.AppendLine("    1. REAL GENERATION SPACE (C): best supported (mixing).");
        sb.AppendLine("    2. Attractor branches (B): coherent, but live IN G.");
        sb.AppendLine("    3. Excitation levels (B): coherent, but count underived.");
        sb.AppendLine("    4. Actualization histories (C): speculative.");
        sb.AppendLine("    5. Bookkeeping (A): REJECTED (mixing is real).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G is a REAL internal space. The excitation-level");
        sb.AppendLine("  and attractor-branch pictures are DESCRIPTIONS of G's");
        sb.AppendLine("  content, not alternatives to G. Mixing settles it.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ACTUALIZATION AND ARCHITECTURE INTERPRETATIONS");
        sb.AppendLine();
        sb.AppendLine("  ACTUALIZATION INTERPRETATION:");
        sb.AppendLine("    Could Random Actualization (QG-006) POPULATE G?");
        sb.AppendLine("    - The 3 generation 'slots' could be 3 distinct attractor");
        sb.AppendLine("      basins that Random Actualization fills differently.");
        sb.AppendLine("    - This is COHERENT with QG-020 (attractors) + QG-006");
        sb.AppendLine("      (randomness): the 3 slots exist as attractors, and");
        sb.AppendLine("      actualization realizes one configuration per slot.");
        sb.AppendLine("    - BUT: the NUMBER of slots (3) is not derived. The");
        sb.AppendLine("      actualization picture fills G, doesn't create it.");
        sb.AppendLine();
        sb.AppendLine("  ARCHITECTURE INTERPRETATION:");
        sb.AppendLine("    Could G be the 'mode space' of the n=1 vortex (QG-028)?");
        sb.AppendLine("    - The vortex has excitation modes (frequency bands).");
        sb.AppendLine("    - 3 stable modes = 3 generations = 3 axes of G.");
        sb.AppendLine("    - This is the MOST AT-NATURAL: G = the mode space of");
        sb.AppendLine("      the architecture. But the mode COUNT (3) is underived.");
        sb.AppendLine();
        sb.AppendLine("  THE SYNTHESIS:");
        sb.AppendLine("    G = the MODE SPACE of the architecture (excitation bands),");
        sb.AppendLine("    with dim(G)=3 (selected, QG-053). Random Actualization");
        sb.AppendLine("    fills the modes. This unifies architecture + actualization");
        sb.AppendLine("    + G into ONE picture. But dim(G)=3 remains selected.");
        return sb.ToString();
    }

    static string BuildG(Elimination[] eliminations)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE ELIMINATION AUDIT");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT: remove G entirely. Recover generations + mixing");
        sb.AppendLine("  + Koide + Yukawas without it.");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-34} {1}", "Elimination attempt", "Verdict"));
        sb.AppendLine("  " + new string('-', 80));
        foreach (var e in eliminations)
        {
            string v = e.Verdict.Length > 38 ? e.Verdict[..35]+"..." : e.Verdict;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-34} {1}", e.Attempt, v));
        }
        sb.AppendLine();
        sb.AppendLine("  THE DECISIVE FAILURE:");
        sb.AppendLine("    'Eliminate G entirely' fails because MIXING is a real");
        sb.AppendLine("    rotation, and rotation requires a real space. G cannot");
        sb.AppendLine("    be eliminated without eliminating neutrino oscillation");
        sb.AppendLine("    and quark flavor change — which are OBSERVED.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: G is IRREDUCIBLE. Every elimination attempt fails,");
        sb.AppendLine("  and the mixing argument makes elimination IMPOSSIBLE in");
        sb.AppendLine("  principle (not just in practice).");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. G IS A REAL ONTOLOGICAL STRUCTURE:");
        sb.AppendLine("    Mixing (CKM/PMNS) proves G is real, not bookkeeping.");
        sb.AppendLine("    AT's ontology now includes: Q, Random Actualization,");
        sb.AppendLine("    (ℓ, τ, ħ), S¹ phase topology, AND the generation space G.");
        sb.AppendLine();
        sb.AppendLine("  2. G = MODE SPACE (the AT-natural interpretation):");
        sb.AppendLine("    G is the space of excitation modes of the architecture");
        sb.AppendLine("    (QG-028). The 3 generations are 3 stable modes. This");
        sb.AppendLine("    connects G to the existing frequency-architecture picture.");
        sb.AppendLine();
        sb.AppendLine("  3. THE RELATIONSHIP TO OTHER SPACES:");
        sb.AppendLine("    - Spacetime (position, time): external motion.");
        sb.AppendLine("    - Phase S¹ (winding, charge): topology.");
        sb.AppendLine("    - Color SU(3): strong force.");
        sb.AppendLine("    - Generation G (flavor): the NEW internal space.");
        sb.AppendLine("    G is the 4th 'space' — a genuine addition.");
        sb.AppendLine();
        sb.AppendLine("  4. WHAT REMAINS OPEN:");
        sb.AppendLine("    - dim(G)=3: selected (QG-053), not derived.");
        sb.AppendLine("    - The Koide 45°: a direction in G, value unexplained.");
        sb.AppendLine("    - The Yukawa eigenvalues: masses, unexplained.");
        sb.AppendLine("    - Whether G is emergent (from unknown) or fundamental.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    G is real (mixing), irreducible (reductions fail), and");
        sb.AppendLine("    its origin is unknown. It is the 'flavor sector' of AT —");
        sb.AppendLine("    analogous to the SM's flavor space, now with a clearer");
        sb.AppendLine("    ontological status (real internal space).");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  G IS A REAL INTERNAL SPACE (ORIGIN UNKNOWN)");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: G = the generation space — a real internal space carrying");
        sb.AppendLine("      the generation index (which copy of the architecture).");
        sb.AppendLine("  Q2: Charge/spin/topology are G-INDEPENDENT (same across");
        sb.AppendLine("      generations). They live in spacetime+phase, not G.");
        sb.AppendLine("  Q3: G stores ONLY the generation index — nothing else.");
        sb.AppendLine("      It is the SMALLEST irreducible additional structure.");
        sb.AppendLine("  Q4: G does NOT emerge from Q/Actualization/Oscillation/");
        sb.AppendLine("      Architecture/S¹ (QG-051/052). It is independent.");
        sb.AppendLine("  Q5: G is a space of MODES (excitation levels) / ATTRACTOR");
        sb.AppendLine("      branches — these describe its content, not replace it.");
        sb.AppendLine("  Q6: Identical quantum numbers, different G-coordinate —");
        sb.AppendLine("      because G is ORTHOGONAL to charge/spin/topology.");
        sb.AppendLine("  Q7: YES — Yukawa matrices are operators on G (eigenvalues =");
        sb.AppendLine("      masses, eigenvectors = mass basis).");
        sb.AppendLine("  Q8: YES — CKM/PMNS mixing (real rotation) is the DECISIVE");
        sb.AppendLine("      evidence G is a real space, not bookkeeping.");
        sb.AppendLine("  Q9: No other internal space replaces G (color can't — leptons");
        sb.AppendLine("      have no color; spacetime can't — G is internal).");
        sb.AppendLine("  Q10: NO — G cannot be eliminated (mixing requires a rotation");
        sb.AppendLine("      space). Reductions all fail.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: C — REAL EMERGENT SPACE");
        sb.AppendLine();
        sb.AppendLine("    G is REAL (mixing = real rotation = needs a real space).");
        sb.AppendLine("    G is IRREDUCIBLE (elimination fails; mixing forbids it).");
        sb.AppendLine("    G does NOT emerge from existing primitives (QG-051/052).");
        sb.AppendLine();
        sb.AppendLine("    So G is a real internal space whose ORIGIN is unresolved:");
        sb.AppendLine("    - 'Real emergent space' (C): real, with emergence from");
        sb.AppendLine("      an unknown deeper mechanism still possible.");
        sb.AppendLine("    - 'Fundamental layer' (D): real, and irreducible —");
        sb.AppendLine("      leaning this way given all reductions failed.");
        sb.AppendLine("    The honest classification is C, bordering on D.");
        sb.AppendLine();
        sb.AppendLine("    THE DEEPEST INSIGHT:");
        sb.AppendLine("    The generation space G is the 'flavor sector' of reality.");
        sb.AppendLine("    It is real (mixing proves it), minimal (stores only an");
        sb.AppendLine("    index), and unexplained (no emergence from S¹/Q). It");
        sb.AppendLine("    stands alongside spacetime, phase, and color as a fourth");
        sb.AppendLine("    fundamental space of the AT ontology.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 54 experiments.");
        return sb.ToString();
    }
}
