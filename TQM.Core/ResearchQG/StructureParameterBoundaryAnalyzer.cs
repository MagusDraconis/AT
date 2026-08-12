using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class StructureParameterBoundaryAnalyzer
{
    public static SPBResult RunFullAnalysis()
    {
        var constraints = BuildConstraints();
        var layers = BuildLayers();
        return new SPBResult(BuildA(),BuildB(),BuildC(),BuildD(constraints),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),constraints,layers);
    }

    static Constraint[] BuildConstraints()
    {
        return new Constraint[]
        {
            new Constraint("Vacuum stability band","m_H, m_t","1D INTERVAL (band)","m_H in [~111, ~175] GeV for stable/meta-stable vacuum (QG-040)","NO (125 GeV selected within band)","REAL: constrains m_H to a band, not a point."),
            new Constraint("Koide relation","m_e, m_mu, m_tau","2D SURFACE in 3D mass space","Q = (sum m)/(sum sqrt m)^2 = 2/3 exactly (QG-039a)","NO (masses free ON the surface)","REAL: lepton masses lie on a surface, not scattered in a volume."),
            new Constraint("Metastability near-criticality","m_H vs m_t","1D CURVE (critical line)","(m_H, m_t) near the phase boundary where lambda ~ 0 at MPl","NO (125 GeV is near but not on the line)","REAL: the pair (m_H,m_t) sits near the critical line."),
            new Constraint("Gauge coupling unification","alpha_s, alpha_W, alpha_EM","1D RELATION (RG flow)","Three couplings converge at ~10^16 GeV (SUSY-GUT)","NO (unified value alpha_GUT ~ 1/25 not derived)","PARTIAL: convergence constrains RELATIVE values; SUSY unconfirmed."),
            new Constraint("Charge quantization","electric charge","DISCRETE (integer)","Q = g·n, n in Z. Charges are integer multiples of g.","PARTIAL: g (elementary charge) not derived, but quantization IS","REAL: charge is quantized (structural). The unit g is contingent."),
        };
    }

    static Layer[] BuildLayers()
    {
        return new Layer[]
        {
            new Layer("STRUCTURE (derived)","WHAT exists, WHY","Ontology: Q, oscillation, phase, topology","U(1), charge quantization, gravity, inertia, particle stability"),
            new Layer("CONSTRAINTS (structural, partial)","WHAT is ALLOWED","Stability, consistency, relations","Vacuum stability band, Koide relation, coupling unification"),
            new Layer("PARAMETERS (contingent)","WHAT is SELECTED","Random Actualization (history)","alpha=1/137, alpha_s=0.118, Yukawas, lambda=0.13"),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("STRUCTURE VERSUS PARAMETER: RECAP (QG-042)");
        sb.AppendLine();
        sb.AppendLine("  QG-042 established:");
        sb.AppendLine("    STRUCTURE (form, symmetry, topology) = DERIVABLE.");
        sb.AppendLine("    PARAMETERS (dimensionless magnitudes) = CONTINGENT.");
        sb.AppendLine("    Boundary: derivability stops where dimensionless magnitude begins.");
        sb.AppendLine();
        sb.AppendLine("  QG-043's QUESTION:");
        sb.AppendLine("    Is the boundary SHARP (parameters completely free)");
        sb.AppendLine("    or is there a MIDDLE LAYER (structure constrains parameters");
        sb.AppendLine("    to allowed regions without fixing exact values)?");
        sb.AppendLine();
        sb.AppendLine("  THE HYPOTHESIS TO TEST:");
        sb.AppendLine("    Reality may contain STRUCTURAL MANIFOLDS — allowed");
        sb.AppendLine("    surfaces/bands on which parameters must lie — while");
        sb.AppendLine("    actualization selects the specific point.");
        sb.AppendLine();
        sb.AppendLine("  ANALOGY:");
        sb.AppendLine("    A gas molecule's POSITION is free (contingent),");
        sb.AppendLine("    but it must lie INSIDE the container (structure).");
        sb.AppendLine("    The container is derived (its walls); the position is not.");
        sb.AppendLine("    Question: does physics have 'containers' for parameters?");
        return sb.ToString();
    }

    static string BuildB()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("KOIDE AS A STRUCTURAL CONSTRAINT (NOT A DERIVATION)");
        sb.AppendLine();
        sb.AppendLine("  QG-039a result: Q = (sum m)/(sum sqrt m)^2 = 2/3 to 10^-5.");
        sb.AppendLine();
        sb.AppendLine("  WHAT KOIDE IS:");
        sb.AppendLine("    A CONSTRAINT on (m_e, m_mu, m_tau), NOT a derivation.");
        sb.AppendLine("    It says the 3 masses lie on a 2D SURFACE in 3D mass space.");
        sb.AppendLine("    (2 masses can be chosen freely; the 3rd is then determined");
        sb.AppendLine("    by the relation — 2 DOF, not 3.)");
        sb.AppendLine();
        sb.AppendLine("  WHAT KOIDE IS NOT:");
        sb.AppendLine("    - It does NOT predict m_e, m_mu, m_tau individually.");
        sb.AppendLine("    - It does NOT explain WHY 3 generations.");
        sb.AppendLine("    - It does NOT determine the mass scale (overall factor).");
        sb.AppendLine();
        sb.AppendLine("  THE KEY DISTINCTION:");
        sb.AppendLine("    Koide = STRUCTURE ACTING ON PARAMETERS.");
        sb.AppendLine("    It constrains the RELATION among masses while leaving");
        sb.AppendLine("    the masses themselves (2 DOF) free.");
        sb.AppendLine("    This is EXACTLY the middle layer: structural constraint");
        sb.AppendLine("    WITHOUT parameter determination.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS PROFOUND:");
        sb.AppendLine("    Koide shows that the boundary is NOT sharp.");
        sb.AppendLine("    Parameters are NOT completely free — they obey at least");
        sb.AppendLine("    one structural relation (the 45° surface).");
        sb.AppendLine("    But the relation does NOT determine the values.");
        sb.AppendLine("    STRUCTURE CONSTRAINS; ACTUALIZATION SELECTS.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("PARAMETER MANIFOLD ANALYSIS");
        sb.AppendLine();
        sb.AppendLine("  THE PARAMETER SPACE:");
        sb.AppendLine("    SM has ~19 free parameters. Think of them as coordinates");
        sb.AppendLine("    in a 19-dimensional parameter space P.");
        sb.AppendLine();
        sb.AppendLine("  STRUCTURAL CONSTRAINTS CARVE OUT SUBSPACES:");
        sb.AppendLine("    - Vacuum stability: excludes m_H < ~111 GeV (a forbidden");
        sb.AppendLine("      region). The allowed space is m_H >= 111 GeV.");
        sb.AppendLine("    - Koide: lepton masses restricted to a 2D surface.");
        sb.AppendLine("    - Unification: couplings constrained to a 1D RG trajectory.");
        sb.AppendLine();
        sb.AppendLine("  THE THREE TYPES OF CONSTRAINT:");
        sb.AppendLine("    1. FORBIDDEN REGIONS: parameters CANNOT take values here");
        sb.AppendLine("       (vacuum unstable, matter impossible). Stability excludes.");
        sb.AppendLine("    2. RELATION SURFACES: parameters must LIE ON a surface");
        sb.AppendLine("       (Koide). Symmetry/geometry forces.");
        sb.AppendLine("    3. RG TRAJECTORIES: parameters FLOW along a curve");
        sb.AppendLine("       (unification). Running determines.");
        sb.AppendLine();
        sb.AppendLine("  HOW MANY CONSTRAINTS?");
        sb.AppendLine("    Known: ~3-4 structural constraints on ~19 parameters.");
        sb.AppendLine("    (stability band, Koide, unification, metastability).");
        sb.AppendLine("    The constraints are REAL but SPARSE — they reduce the");
        sb.AppendLine("    parameter space dimension from ~19 to ~15-16.");
        sb.AppendLine("    Most parameters remain unconstrained.");
        sb.AppendLine();
        sb.AppendLine("  VERDICT: The parameter space has STRUCTURE (some forbidden");
        sb.AppendLine("  regions and relation surfaces), but the structure is NOT");
        sb.AppendLine("  dense enough to determine most values. Sparse constraints.");
        return sb.ToString();
    }

    static string BuildD(Constraint[] constraints)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("CONSTRAINT SURFACES (CATALOG)");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,-18} {2,-20} {3}", "Constraint", "Type", "Determines value?", "Status"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var c in constraints)
        {
            string val = c.FixedValue.Length > 28 ? c.FixedValue[..25]+"..." : c.FixedValue;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,-18} {2,-20} {3}", c.Name, c.Type, val, c.Status));
        }
        sb.AppendLine();
        sb.AppendLine("  PATTERN:");
        sb.AppendLine("    - Stability constraints give BANDS (intervals), not points.");
        sb.AppendLine("    - Relation constraints give SURFACES (Koide), not points.");
        sb.AppendLine("    - Unification gives TRAJECTORIES, not points.");
        sb.AppendLine("    - Quantization gives DISCRETE sets (integers), not points.");
        sb.AppendLine();
        sb.AppendLine("  NO CONSTRAINT DETERMINES AN EXACT VALUE.");
        sb.AppendLine("    Every known structural constraint REDUCES the allowed");
        sb.AppendLine("    space (removes a dimension or excludes a region) but");
        sb.AppendLine("    leaves a CONTINUUM of allowed values.");
        sb.AppendLine();
        sb.AppendLine("  THIS SUPPORTS THE MANIFOLD HYPOTHESIS:");
        sb.AppendLine("    Structure determines the MANIFOLD (allowed space).");
        sb.AppendLine("    Actualization determines the POINT (specific value).");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("ACTUALIZATION SELECTION MODEL");
        sb.AppendLine();
        sb.AppendLine("  THE TWO-STEP PICTURE:");
        sb.AppendLine();
        sb.AppendLine("  STEP 1: STRUCTURE DEFINES THE MANIFOLD.");
        sb.AppendLine("    Ontology (Q, oscillation, phase) → allowed parameter space.");
        sb.AppendLine("    Stability, consistency, symmetry → constraints on the space.");
        sb.AppendLine("    Result: a MANIFOLD M (the 'container').");
        sb.AppendLine();
        sb.AppendLine("  STEP 2: ACTUALIZATION SELECTS THE POINT.");
        sb.AppendLine("    Random Actualization (QG-006) → a specific realization.");
        sb.AppendLine("    The actualization HISTORY picks a point in M.");
        sb.AppendLine("    Result: the observed parameter values (a 'position').");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS FITS TQM PERFECTLY:");
        sb.AppendLine("    - Q (becoming) → structure → the manifold M.");
        sb.AppendLine("    - Random Actualization → contingency → the point in M.");
        sb.AppendLine("    - The triple (ℓ, τ, ħ) → the SCALE of M.");
        sb.AppendLine("    All three primitives play distinct roles.");
        sb.AppendLine();
        sb.AppendLine("  MULTIVERSE-FREE SELECTION:");
        sb.AppendLine("    Random Actualization provides the selection WITHOUT a");
        sb.AppendLine("    multiverse. The actualization history of THIS universe");
        sb.AppendLine("    picked its parameter point. No ensemble of universes needed.");
        sb.AppendLine();
        sb.AppendLine("  THE LIMITATION:");
        sb.AppendLine("    TQM can characterize M (the allowed space) but NOT the");
        sb.AppendLine("    actualization history that picked the point. The SELECTION");
        sb.AppendLine("    is fundamentally inaccessible (it's history, not law).");
        sb.AppendLine("    This is WHY parameter values resist derivation.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REVIEW");
        sb.AppendLine();
        sb.AppendLine("  1. 'THE CONSTRAINTS ARE FEW AND WEAK':");
        sb.AppendLine("     CORRECT. Only ~3-4 structural constraints on ~19 parameters.");
        sb.AppendLine("     The manifold M is mostly 'flat' (unconstrained).");
        sb.AppendLine("     Calling this 'strong structure' would overclaim.");
        sb.AppendLine();
        sb.AppendLine("  2. 'KOIDE MIGHT BE COINCIDENCE (QG-039a ~10^-4)':");
        sb.AppendLine("     PARTIALLY CORRECT. If Koide is accident, then the");
        sb.AppendLine("     'manifold' evidence weakens to just the stability band.");
        sb.AppendLine("     The manifold hypothesis is COHERENT but NOT yet proven.");
        sb.AppendLine();
        sb.AppendLine("  3. 'THE MANIFOLD/POINT PICTURE IS UNFALSIFIABLE':");
        sb.AppendLine("     PARTIALLY CORRECT. The claim 'structure defines M, and");
        sb.AppendLine("     actualization picks a point' is DIFFICULT to test directly.");
        sb.AppendLine("     It would be supported if: (a) more structural relations");
        sb.AppendLine("     are found (like Koide), or (b) TQM derives the FORM of M");
        sb.AppendLine("     even without the point. Currently: suggestive, not proven.");
        sb.AppendLine();
        sb.AppendLine("  4. 'WHAT GENUINELY EMERGES':");
        sb.AppendLine("     - A THREE-LAYER picture: structure → constraints → parameters.");
        sb.AppendLine("     - The recognition that constraints are BANDS/SURFACES,");
        sb.AppendLine("       not points. (Stability gives intervals; Koide gives surfaces.)");
        sb.AppendLine("     - A precise division of labor among TQM's primitives:");
        sb.AppendLine("       Q → structure, Randomness → selection, triple → scale.");
        sb.AppendLine();
        sb.AppendLine("  5. 'THE DEEPEST TEST':");
        sb.AppendLine("     Does TQM predict the EXISTENCE of more Koide-like relations?");
        sb.AppendLine("     If structure→manifold is real, there should be MORE");
        sb.AppendLine("     relation surfaces (e.g., among quark masses, mixing angles).");
        sb.AppendLine("     The quark Koide is only ~2% accurate — WEAKER evidence.");
        sb.AppendLine("     This is a FALSIFIABLE prediction: find more exact relations.");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE DERIVABILITY BOUNDARY: THREE LAYERS, NOT TWO");
        sb.AppendLine();
        sb.AppendLine("  QG-042 proposed TWO layers:");
        sb.AppendLine("    Structure (derived) | Parameters (contingent).");
        sb.AppendLine();
        sb.AppendLine("  QG-043 REFINES to THREE layers:");
        sb.AppendLine();
        sb.AppendLine("  ┌─────────────────────────────────────────────────────────┐");
        sb.AppendLine("  │ LAYER 1: STRUCTURE (derived from ontology)             │");
        sb.AppendLine("  │   WHAT exists, WHY. Q → oscillation → phase → topology │");
        sb.AppendLine("  │   U(1), charge quantization, gravity, inertia          │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────────┤");
        sb.AppendLine("  │ LAYER 2: CONSTRAINTS (structural, partial)             │");
        sb.AppendLine("  │   WHAT is ALLOWED. Stability bands, relations, flows   │");
        sb.AppendLine("  │   Koide surface, vacuum stability band, unification    │");
        sb.AppendLine("  ├─────────────────────────────────────────────────────────┤");
        sb.AppendLine("  │ LAYER 3: PARAMETERS (contingent)                       │");
        sb.AppendLine("  │   WHAT is SELECTED. Random Actualization (history)     │");
        sb.AppendLine("  │   alpha=1/137, alpha_s=0.118, Yukawas, lambda          │");
        sb.AppendLine("  └─────────────────────────────────────────────────────────┘");
        sb.AppendLine();
        sb.AppendLine("  THE BOUNDARY IS A GRADIENT, NOT A CLIFF:");
        sb.AppendLine("    Structure (layer 1) → fully derivable.");
        sb.AppendLine("    Constraints (layer 2) → partially derivable (the FORM");
        sb.AppendLine("    of the constraint is derived; the values are not).");
        sb.AppendLine("    Parameters (layer 3) → contingent.");
        sb.AppendLine();
        sb.AppendLine("  THIS RESOLVES THE QG-042 TENSION:");
        sb.AppendLine("    QG-042 said 'parameters are contingent' (true).");
        sb.AppendLine("    QG-043 adds 'but not completely free' (also true).");
        sb.AppendLine("    The middle layer (constraints) is the bridge.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. TQM's PREDICTIVE POWER IS REFINED:");
        sb.AppendLine("    TQM can predict STRUCTURE (layer 1) fully.");
        sb.AppendLine("    TQM can predict CONSTRAINTS (layer 2) partially —");
        sb.AppendLine("    the FORM of stability bands and relations.");
        sb.AppendLine("    TQM CANNOT predict PARAMETERS (layer 3) — these are history.");
        sb.AppendLine();
        sb.AppendLine("  2. THE RESEARCH PROGRAM BECOMES CLEAR:");
        sb.AppendLine("    PRIORITY: find and derive MORE layer-2 constraints.");
        sb.AppendLine("    (a) Derive the vacuum stability band from Q-event dynamics.");
        sb.AppendLine("    (b) Derive the Koide 45° from architectural symmetry.");
        sb.AppendLine("    (c) Derive the unification trajectory from phase topology.");
        sb.AppendLine("    Each would MOVE a quantity from layer 3 to layer 2.");
        sb.AppendLine();
        sb.AppendLine("  3. FALSIFIABLE PREDICTIONS:");
        sb.AppendLine("    - More Koide-like relations should exist (quark, neutrino).");
        sb.AppendLine("    - Stability bands should emerge from TQM dynamics.");
        sb.AppendLine("    - The manifold M should have SPECIFIC geometric form.");
        sb.AppendLine();
        sb.AppendLine("  4. THE DEEPEST CONSISTENCY:");
        sb.AppendLine("    Q → structure, Random Actualization → selection, ℓ/τ/ħ → scale.");
        sb.AppendLine("    The three primitives map to the three layers.");
        sb.AppendLine("    TQM's ontology ALREADY contains the three-layer structure.");
        sb.AppendLine("    This is a powerful internal consistency check.");
        sb.AppendLine();
        sb.AppendLine("  5. WHAT 'COMPLETE' WOULD MEAN:");
        sb.AppendLine("    A complete TQM would derive ALL of layer 1 and ALL of");
        sb.AppendLine("    layer 2 (every constraint), while correctly identifying");
        sb.AppendLine("    layer 3 (contingent values) as historical. Completeness");
        sb.AppendLine("    = deriving the MANIFOLD, not the POINT.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  PARAMETERS ARE FREE WITHIN STRUCTURAL BOUNDARIES");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: YES — parameters can be contingent while their RELATIONS");
        sb.AppendLine("      are fixed. Contingency of VALUES ≠ contingency of RELATIONS.");
        sb.AppendLine("  Q2: YES — Koide is structure acting on parameters: it constrains");
        sb.AppendLine("      the relation among masses (45° surface) without fixing masses.");
        sb.AppendLine("  Q3: PARTIALLY — couplings are free but constrained to stability");
        sb.AppendLine("      bands (m_H) and unification trajectories (gauge couplings).");
        sb.AppendLine("  Q4: YES — TQM can derive parameter RELATIONS (constraints)");
        sb.AppendLine("      without deriving parameter VALUES. This is layer 2.");
        sb.AppendLine("  Q5: YES — actualization selects a point INSIDE the allowed space M.");
        sb.AppendLine("  Q6: Manifolds, surfaces, bands, trajectories (the container).");
        sb.AppendLine("  Q7: YES — geometric constraints (45° surface) generate Koide-like");
        sb.AppendLine("      relations without fixing the masses.");
        sb.AppendLine("  Q8: YES — couplings reside on stability surfaces (vacuum stability).");
        sb.AppendLine("  Q9: POSSIBLY — parameter space may have resonance regions/attractors.");
        sb.AppendLine("      Speculative, not yet demonstrated.");
        sb.AppendLine("  Q10: YES — Nature selects LAWS (structure, layer 1) and VALUES");
        sb.AppendLine("      (parameters, layer 3) through DIFFERENT mechanisms:");
        sb.AppendLine("      ontology vs history.");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: B — WEAK STRUCTURAL CONSTRAINTS");
        sb.AppendLine();
        sb.AppendLine("    The THREE-LAYER picture is the key contribution:");
        sb.AppendLine("      Layer 1 (structure): derived.");
        sb.AppendLine("      Layer 2 (constraints): partially derived (form, not values).");
        sb.AppendLine("      Layer 3 (parameters): contingent.");
        sb.AppendLine();
        sb.AppendLine("    Parameters are NOT completely free (stability bands, Koide");
        sb.AppendLine("    surface, unification trajectory are real constraints).");
        sb.AppendLine("    But the constraints are SPARSE (~3-4 on ~19 parameters),");
        sb.AppendLine("    so most parameters remain unconstrained.");
        sb.AppendLine();
        sb.AppendLine("    The boundary is a GRADIENT, not a cliff. Reality determines");
        sb.AppendLine("    the MANIFOLD (allowed space); actualization selects the POINT.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 43 experiments.");
        return sb.ToString();
    }
}
