using System.Globalization;

namespace AT.Core.ResearchQG;

public static class OntologyCompletionAnalyzer
{
    public static OAResult RunFullAnalysis()
    {
        var layers = BuildLayers();
        var residues = BuildResidues();
        return new OAResult(BuildA(layers),BuildB(),BuildC(residues),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),layers,residues);
    }

    static OntologyLayer[] BuildLayers()
    {
        return new OntologyLayer[]
        {
            new OntologyLayer("Q (becoming)","IRREDUCIBLE","LOGICAL primitive: the minimal notion of 'something happens'."),
            new OntologyLayer("Random Actualization","IRREDUCIBLE","LOGICAL primitive: the minimal notion of 'without prior determination'."),
            new OntologyLayer("(l, tau, hbar)","IRREDUCIBLE (values empirical)","PHYSICAL triple: one process (actualization), three aspects (where/when/how much)."),
            new OntologyLayer("G (generation space)","REAL, possibly EMERGENT","Internal space (flavor). Origin unresolved (QG-054/064)."),
            new OntologyLayer("Everything else","DERIVED or CONTINGENT","All structure (flavor, gravity, QM) + all content (parameters)."),
        };
    }

    static Residue[] BuildResidues()
    {
        return new Residue[]
        {
            new Residue("Architecture shapes (landscape content)","UNDERIVED","CONTINGENT CONTENT","Correctly classified (QG-042): historical, not structural."),
            new Residue("Koide 45 deg","UNDERIVED","CONTINGENT CONTENT","Real but unexplained value (QG-057); classified as boundary condition (QG-060)."),
            new Residue("Coupling constants (alpha, etc.)","UNDERIVED","CONTINGENT CONTENT","Empirical (QG-041); contingent (QG-042)."),
            new Residue("Triple values (l, tau, hbar)","UNDERIVED","EMPIRICAL SCALE","The triple IS structure; its VALUES are empirical (QG-012/014)."),
            new Residue("Generation space G (origin)","UNDERIVED","STRUCTURE (possibly emergent)","The one unresolved STRUCTURE. Possibly emerges from landscape (QG-064)."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA(OntologyLayer[] layers)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ONTOLOGY RECAP: THE PRIMITIVES");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-28} {2}", "Layer", "Status", "Nature"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var l in layers)
        {
            string n = l.Nature.Length > 48 ? l.Nature[..45]+"..." : l.Nature;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-28} {2}", l.Layer, l.Status, n));
        }
        sb.AppendLine();
        sb.AppendLine("  THE ONTOLOGY (4 primitives):");
        sb.AppendLine("    1. Q (becoming) — LOGICAL.");
        sb.AppendLine("    2. Random Actualization — LOGICAL.");
        sb.AppendLine("    3. (l, tau, hbar) — PHYSICAL (irreducible triple).");
        sb.AppendLine("    4. G (generation space) — REAL (possibly emergent).");
        sb.AppendLine();
        sb.AppendLine("  THE QUESTION: is this COMPLETE?");
        sb.AppendLine("    Complete = all STRUCTURE derived; all CONTENT classified.");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("REDUCTION CHAIN ANALYSIS: WHAT HAS BEEN DERIVED");
        sb.AppendLine();
        sb.AppendLine("  THE COMPLETE REDUCTION CHAIN (64 experiments):");
        sb.AppendLine();
        sb.AppendLine("    FLAVOR:");
        sb.AppendLine("      masses → Y (overlap) → architecture shapes → attractor");
        sb.AppendLine("      landscape → actualization (bedrock).");
        sb.AppendLine("    GRAVITY:");
        sb.AppendLine("      curvature → phase gradients → oscillation → actualization.");
        sb.AppendLine("    QM:");
        sb.AppendLine("      Born/Hilbert/entanglement → interference → phase →");
        sb.AppendLine("      oscillation → actualization.");
        sb.AppendLine("    ALL THREE converge on ACTUALIZATION (Q + Randomness + triple).");
        sb.AppendLine();
        sb.AppendLine("  EVERYTHING DERIVED:");
        sb.AppendLine("    - Oscillation (QG-021/026): logical inevitability.");
        sb.AppendLine("    - Phase, U(1), charge (QG-033/038): S¹ topology.");
        sb.AppendLine("    - Gravity (QG-022): phase gradients.");
        sb.AppendLine("    - Inertia, equivalence (QG-036): attractor persistence.");
        sb.AppendLine("    - Particles (QG-034): topological winding.");
        sb.AppendLine("    - G, S3, mixing (QG-052-056): generation geometry.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The reduction chain is COMPLETE — every branch");
        sb.AppendLine("  terminates at actualization. No branch hangs.");
        return sb.ToString();
    }

    static string BuildC(Residue[] residues)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IRREDUCIBLE RESIDUES: WHAT REMAINS UNDERIVED");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-32} {1,-18} {2}", "Quantity", "Nature", "Classification"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var r in residues)
        {
            string c = r.Classification.Length > 48 ? r.Classification[..45]+"..." : r.Classification;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-32} {1,-18} {2}", r.Quantity, r.Nature, c));
        }
        sb.AppendLine();
        sb.AppendLine("  THE KEY DISTINCTION (QG-042):");
        sb.AppendLine("    - CONTINGENT CONTENT (shapes, Koide, couplings): historical");
        sb.AppendLine("      outcomes, NOT missing structure. Correctly classified.");
        sb.AppendLine("    - EMPIRICAL SCALE (triple values): the triple IS structure;");
        sb.AppendLine("      its VALUES are empirical (not derived).");
        sb.AppendLine("    - STRUCTURE (G): the ONE unresolved structure. Possibly");
        sb.AppendLine("      emergent from the landscape (QG-064, untested).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The residues are ALMOST ALL 'contingent content' or");
        sb.AppendLine("  'empirical scale' — NOT missing structure. The ONE exception");
        sb.AppendLine("  is G (the generation space), whose origin is unresolved.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("STRUCTURE VS CONTENT: THE UNIVERSAL SPLIT");
        sb.AppendLine();
        sb.AppendLine("  QG-042: Structure (form) is derivable; Parameters (content)");
        sb.AppendLine("  are contingent. QG-064: this split reaches the bedrock.");
        sb.AppendLine();
        sb.AppendLine("  THE SPLIT APPLIED UNIVERSALLY:");
        sb.AppendLine("    - STRUCTURE (derived): oscillation, phase, topology, U(1),");
        sb.AppendLine("      gravity, inertia, particles, G's geometry, S3, mixing.");
        sb.AppendLine("    - CONTENT (contingent): masses, couplings, architecture");
        sb.AppendLine("      shapes, Koide 45°, landscape minima.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS THE COMPLETENESS CRITERION:");
        sb.AppendLine("    A complete ontology must derive ALL structure and correctly");
        sb.AppendLine("    classify ALL content as contingent. AT does the first");
        sb.AppendLine("    fully, the second fully.");
        sb.AppendLine();
        sb.AppendLine("  THE RESULT:");
        sb.AppendLine("    AT derives all STRUCTURE (down to Q + Randomness + triple)");
        sb.AppendLine("    and classifies all CONTENT as contingent (Random Actualization).");
        sb.AppendLine("    The structure/content split is UNIVERSAL — it holds at");
        sb.AppendLine("    every level (flavor, gravity, QM, landscape).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The structure/content split is the COMPLETENESS");
        sb.AppendLine("  criterion, and AT SATISFIES it (structure derived, content");
        sb.AppendLine("  classified). The split is a genuine ontological principle.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ACTUALIZATION AS BEDROCK: LOGICAL, NOT PHYSICAL");
        sb.AppendLine();
        sb.AppendLine("  Why does the reduction stop at actualization?");
        sb.AppendLine();
        sb.AppendLine("  QG-006: Q + Random Actualization are LOGICAL primitives:");
        sb.AppendLine("    - Q = 'something happens' (the minimal notion of becoming).");
        sb.AppendLine("    - Randomness = 'without prior determination' (indeterminacy).");
        sb.AppendLine("    These are LOGICALLY NECESSARY — removing them destroys the");
        sb.AppendLine("    very concept of 'something exists and happens'.");
        sb.AppendLine();
        sb.AppendLine("  WHY LOGICAL BEDROCK AVOIDS REGRESS:");
        sb.AppendLine("    - A PHYSICAL primitive invites 'what explains it?'.");
        sb.AppendLine("    - A LOGICAL primitive does NOT: asking 'why does something");
        sb.AppendLine("      happen?' is a LOGICAL question, not a physical one.");
        sb.AppendLine("    - Q and Randomness are the minimal logical content of");
        sb.AppendLine("      'there is a world'. They are not physical assumptions.");
        sb.AppendLine();
        sb.AppendLine("  THE TRIPLE (l, tau, hbar):");
        sb.AppendLine("    - The triple is the ONE physical content: the SCALE of");
        sb.AppendLine("      actualization (where/when/how much).");
        sb.AppendLine("    - Its VALUES are empirical (QG-012/014), but the triple");
        sb.AppendLine("      IS the irreducible physical bedrock (QG-017).");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Actualization (Q + Randomness + triple) is the");
        sb.AppendLine("  LOGICAL+PHYSICAL bedrock. It is irreducible (QG-025). The");
        sb.AppendLine("  reduction stops here because there is nothing LOGICALLY");
        sb.AppendLine("  deeper than 'something happens, without prior determination'.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ALTERNATIVE DEEPER ONTOLOGIES: DO THEY HELP?");
        sb.AppendLine();
        sb.AppendLine("  Candidate deeper layers (all fail to add explanatory power):");
        sb.AppendLine();
        sb.AppendLine("  1. 'A creator / external cause of actualization':");
        sb.AppendLine("     FAILS: replaces 'random actualization' with 'external cause',");
        sb.AppendLine("     which then needs explanation (regress). No new predictions.");
        sb.AppendLine();
        sb.AppendLine("  2. 'A deterministic hidden-variable layer':");
        sb.AppendLine("     FAILS: contradicts Random Actualization (QG-006), which is");
        sb.AppendLine("     the LOGICAL primitive of indeterminacy. Removing randomness");
        sb.AppendLine("     removes the contingency that explains parameter freedom.");
        sb.AppendLine();
        sb.AppendLine("  3. 'A multiverse ensemble':");
        sb.AppendLine("     FAILS: replaces 'contingent content' with 'one universe in");
        sb.AppendLine("     an ensemble'. The ensemble's OWN parameters need explanation.");
        sb.AppendLine("     No new predictions (unfalsifiable).");
        sb.AppendLine();
        sb.AppendLine("  4. 'A deeper generation-space origin':");
        sb.AppendLine("     PARTIAL: G's origin is unresolved (QG-054). A deeper layer");
        sb.AppendLine("     MIGHT derive G. But no such layer is identified (QG-051/052).");
        sb.AppendLine("     This is the ONE legitimate open question.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: Alternative deeper ontologies RENAME the unknown");
        sb.AppendLine("  (creator, multiverse) or are unfalsifiable. The ONLY");
        sb.AppendLine("  legitimate deeper question is G's origin (possibly emergent).");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("INFINITE-REGRESSION ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  The regress threat: 'what explains X?' → 'Y' → 'what explains");
        sb.AppendLine("  Y?' → ... (never terminates).");
        sb.AppendLine();
        sb.AppendLine("  DOES AT AVOID IT?  YES — via LOGICAL primitives.");
        sb.AppendLine();
        sb.AppendLine("  THE TERMINATION POINT:");
        sb.AppendLine("    Q = 'something happens' (becoming).");
        sb.AppendLine("    Randomness = 'without prior determination'.");
        sb.AppendLine("    These are LOGICAL, not physical. Asking 'what explains");
        sb.AppendLine("    becoming?' is a LOGICAL question, not a physical one.");
        sb.AppendLine("    There is nothing LOGICALLY deeper than 'something happens'.");
        sb.AppendLine();
        sb.AppendLine("  CONTRAST WITH OTHER FRAMEWORKS:");
        sb.AppendLine("    - 'God of the gaps': stops at God (regress: who made God?).");
        sb.AppendLine("    - 'Multiverse': stops at the ensemble (regress: what set");
        sb.AppendLine("      the ensemble's parameters?).");
        sb.AppendLine("    - AT: stops at LOGICAL primitives (no regress — logic");
        sb.AppendLine("      has no 'deeper layer').");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: AT AVOIDS infinite regress by grounding in LOGICAL");
        sb.AppendLine("  primitives (Q, Randomness). This is a STRONG form of");
        sb.AppendLine("  completion: the bottom is LOGIC, not physics.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR AT");
        sb.AppendLine();
        sb.AppendLine("  1. AT IS (MOSTLY) ONTOLOGICALLY COMPLETE:");
        sb.AppendLine("    - Structure: fully derived (down to Q + Randomness + triple).");
        sb.AppendLine("    - Content: fully classified (contingent, Random Actualization).");
        sb.AppendLine("    - Bedrock: LOGICAL (avoids infinite regress).");
        sb.AppendLine();
        sb.AppendLine("  2. THE ONE OPEN QUESTION: G.");
        sb.AppendLine("    The generation space G is the single unresolved STRUCTURE.");
        sb.AppendLine("    Its origin is unresolved (QG-054), possibly emergent from");
        sb.AppendLine("    the landscape (QG-064, untested). Resolving G would make");
        sb.AppendLine("    the ontology fully complete (D).");
        sb.AppendLine();
        sb.AppendLine("  3. THE CONTINGENT CONTENT IS A FEATURE, NOT A BUG:");
        sb.AppendLine("    The underived parameters (masses, couplings, Koide, shapes)");
        sb.AppendLine("    are CONTINGENT CONTENT (QG-042). Their freedom is EXPLAINED");
        sb.AppendLine("    by Random Actualization — it is the MECHANISM of contingency.");
        sb.AppendLine("    A complete ontology does NOT need to derive them; it needs");
        sb.AppendLine("    to correctly CLASSIFY them. AT does.");
        sb.AppendLine();
        sb.AppendLine("  4. THE DEEPEST RESULT:");
        sb.AppendLine("    The structure/content split (QG-042) is the KEY to");
        sb.AppendLine("    completion. AT derives ALL structure and classifies ALL");
        sb.AppendLine("    content. This is the deepest ontological claim: reality");
        sb.AppendLine("    has a fully-derivable form and a contingent realization.");
        sb.AppendLine();
        sb.AppendLine("  5. HONEST POSITION:");
        sb.AppendLine("    AT is MOSTLY COMPLETE (B): structure derived, content");
        sb.AppendLine("    classified, bedrock logical. The ONE unresolved structure");
        sb.AppendLine("    (G) is the sole remaining ontological question.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  AT IS MOSTLY COMPLETE: STRUCTURE DERIVED, CONTENT CONTINGENT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: The underived quantities are: architecture shapes, Koide,");
        sb.AppendLine("      couplings (contingent content), triple values (empirical),");
        sb.AppendLine("      and G's origin (one unresolved structure).");
        sb.AppendLine("  Q2: Almost — everything reduces to Q + Randomness + triple + G.");
        sb.AppendLine("      The first three are irreducible; G is the one open structure.");
        sb.AppendLine("  Q3: Architecture shapes = contingent (QG-063/064). Koide =");
        sb.AppendLine("      boundary condition, value unexplained (QG-060). Flavor");
        sb.AppendLine("      structure = derived (G, S3, overlap, QG-055/056/062).");
        sb.AppendLine("  Q4: Random Actualization is GENUINELY irreducible (QG-025):");
        sb.AppendLine("      it is a LOGICAL primitive (indeterminacy), not unexplained.");
        sb.AppendLine("  Q5: YES — every remaining mystery reduces to CONTINGENT CONTENT");
        sb.AppendLine("      (values), NOT missing structure (except G).");
        sb.AppendLine("  Q6: YES — laws (structure, derived), constraints (manifolds,");
        sb.AppendLine("      QG-044), realized values (content, contingent).");
        sb.AppendLine("  Q7: A deeper theory could derive actualization OUTCOMES only if");
        sb.AppendLine("      it eliminated randomness — which would eliminate contingency.");
        sb.AppendLine("  Q8: YES — such a theory would eliminate genuine contingency");
        sb.AppendLine("      (and thus the explanation of parameter freedom).");
        sb.AppendLine("  Q9: YES — completeness is COMPATIBLE with free parameters:");
        sb.AppendLine("      the FREEDOM is explained (Randomness), the VALUES are not.");
        sb.AppendLine("  Q10: YES — the chain terminates at LOGICAL primitives (Q,");
        sb.AppendLine("      Randomness). No physical regress.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — MOSTLY COMPLETE");
        sb.AppendLine();
        sb.AppendLine("    AT has reduced reality to 4 primitives:");
        sb.AppendLine("      Q (logical), Randomness (logical), triple (physical),");
        sb.AppendLine("      G (real, possibly emergent).");
        sb.AppendLine();
        sb.AppendLine("    All STRUCTURE is derived; all CONTENT is classified as");
        sb.AppendLine("    contingent (Random Actualization). The bedrock is LOGICAL");
        sb.AppendLine("    (Q + Randomness), avoiding infinite regress.");
        sb.AppendLine();
        sb.AppendLine("    THE ONE OPEN QUESTION: G's origin (unresolved structure,");
        sb.AppendLine("    possibly emergent from the landscape, QG-064). Resolving");
        sb.AppendLine("    G would make the ontology STRONGLY/ONTOLOGICALLY complete.");
        sb.AppendLine();
        sb.AppendLine("    THE DEEPEST RESULT (after 65 experiments):");
        sb.AppendLine("    Reality has a fully-derivable FORM and a contingent");
        sb.AppendLine("    REALIZATION. Structure is derived; content is drawn. This");
        sb.AppendLine("    is the AT ontology: Q + Randomness at the bedrock,");
        sb.AppendLine("    with everything else either derived structure or");
        sb.AppendLine("    contingent content.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 65 experiments.");
        return sb.ToString();
    }
}
