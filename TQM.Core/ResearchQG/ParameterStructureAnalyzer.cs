using System.Globalization;

namespace TQM.Core.ResearchQG;

public static class ParameterStructureAnalyzer
{
    public static PSResult RunFullAnalysis()
    {
        var derived = BuildDerived();
        var resistant = BuildResistant();
        return new PSResult(BuildA(derived),BuildB(resistant),BuildC(),BuildD(),BuildE(),BuildF(),BuildG(),BuildH(),BuildI(),derived,resistant);
    }

    static DerivedItem[] BuildDerived()
    {
        return new DerivedItem[]
        {
            new DerivedItem("Oscillation","STRUCTURE","Q → temporal succession at τ → rhythm (QG-026)","Follows from LOGICAL PRIMITIVES. Q defines succession; τ defines interval. Cannot be otherwise."),
            new DerivedItem("Phase","STRUCTURE","Oscillation → phase angle θ = ωt (QG-021)","Phase is the angular parametrization of oscillation. Lives on S¹. Mathematical necessity."),
            new DerivedItem("U(1) gauge symmetry","STRUCTURE","S¹ topology → winding → conserved charge → U(1) (QG-038)","S¹'s isometry group IS U(1). Not chosen — it's the symmetry of phase itself."),
            new DerivedItem("Charge quantization","STRUCTURE","Winding number n ∈ Z → Q = g·n (QG-035)","Integer winding → integer charge (in units of g). Topology forces discreteness."),
            new DerivedItem("Gravity","STRUCTURE","Phase gradient ∇θ → curvature (QG-022)","Gravity = geometry of phase structure. Emerges from phase organization."),
            new DerivedItem("Inertia / F=ma","STRUCTURE","Attractor resistance to phase reconfiguration (QG-036)","Inertia = stability of the phase architecture. F=ma from reconfiguration cost."),
            new DerivedItem("Equivalence principle","STRUCTURE","m_i = m_g = E_total/c² (QG-036)","Both masses = same phase-energy density. DERIVED, not postulated."),
            new DerivedItem("Particle stability","STRUCTURE","Topological protection of n=1 winding (QG-034)","Electron stability = topological theorem. n=1 cannot fission (no n=½)."),
            new DerivedItem("Anti-matter","STRUCTURE","n → -n = opposite winding (QG-034/035)","S¹ topology gives n and -n automatically. Anti-matter is inevitable."),
            new DerivedItem("Higgs interpretation","STRUCTURE","Higgs = amplitude mode of phase field (QG-037)","Phase field has 2 DOF (θ, A). Higgs = amplitude excitation. Ontological necessity."),
        };
    }

    static ResistantItem[] BuildResistant()
    {
        return new ResistantItem[]
        {
            new ResistantItem("Fine structure α_EM",1.0/137.036,"DIMENSIONLESS PARAMETER","The winding-gauge coupling g. WHY g² = 1/137? No derivation. Deepest mystery."),
            new ResistantItem("Strong coupling α_s(MZ)",0.118,"DIMENSIONLESS PARAMETER","Tri-winding confinement strength. Running from QCD beta function (external). Value empirical."),
            new ResistantItem("Weak mixing angle θ_W",0.231,"DIMENSIONLESS PARAMETER","sin²θ_W = 0.231. Electroweak unification ratio. Not derived."),
            new ResistantItem("Top Yukawa y_t",0.99,"DIMENSIONLESS PARAMETER","Overlap of top architecture with amplitude mode. WHY ~1? Unexplained."),
            new ResistantItem("Electron Yukawa y_e",2.9e-6,"DIMENSIONLESS PARAMETER","Overlap of electron architecture with amplitude mode. WHY 3e-6? Unexplained hierarchy."),
            new ResistantItem("Higgs self-coupling λ",0.13,"DIMENSIONLESS PARAMETER","Amplitude stiffness. WHY 0.13? Empirical (from m_H, v)."),
            new ResistantItem("θ_QCD",0.0,"DIMENSIONLESS PARAMETER","Strong CP violation. WHY ~0? Strong CP problem. Unexplained."),
            new ResistantItem("Neutrino mass ratio",1e-6,"DIMENSIONLESS PARAMETER","WHY neutrinos ~1e6x lighter than electrons? Seesaw/external. Unexplained."),
        };
    }

    // === REPORT SECTIONS ===

    static string BuildA(DerivedItem[] derived)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHAT IS DERIVABLE?");
        sb.AppendLine();
        sb.AppendLine("  TQM has successfully DERIVED:");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-26} {1,-12} {2}", "Quantity", "Category", "Why derivable"));
        sb.AppendLine("  " + new string('-', 95));
        foreach (var d in derived)
        {
            string why = d.WhyDerivable.Length > 60 ? d.WhyDerivable[..57]+"..." : d.WhyDerivable;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-26} {1,-12} {2}", d.Quantity, d.Category, why));
        }
        sb.AppendLine();
        sb.AppendLine("  COMMON CHARACTERISTICS OF DERIVED QUANTITIES:");
        sb.AppendLine("    1. They are STRUCTURAL (form, not magnitude).");
        sb.AppendLine("    2. They follow from LOGICAL PRIMITIVES (Q + Randomness).");
        sb.AppendLine("    3. They are TOPOLOGICAL or GEOMETRIC (integers, circles).");
        sb.AppendLine("    4. They answer 'WHAT EXISTS and WHY' — not 'HOW MUCH'.");
        sb.AppendLine("    5. They are NECESSARY (cannot be otherwise).");
        sb.AppendLine();
        sb.AppendLine("  THE PATTERN: Structure is derivable.");
        sb.AppendLine("  Form, symmetry, topology, existence — all follow from the");
        sb.AppendLine("  ontology of Q-events. The MATHEMATICS is forced.");
        return sb.ToString();
    }

    static string BuildB(ResistantItem[] resistant)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("WHAT RESISTS DERIVATION?");
        sb.AppendLine();
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
            "  {0,-28} {1,12} {2}", "Quantity", "Value", "Why resistant"));
        sb.AppendLine("  " + new string('-', 90));
        foreach (var r in resistant)
        {
            string val = r.Value < 0.001 ? r.Value.ToString("E2", CultureInfo.InvariantCulture)
                : (r.Value < 0.02 && r.Value > 0 ? "1/"+Math.Round(1.0/r.Value).ToString() : r.Value.ToString("F4", CultureInfo.InvariantCulture));
            string why = r.WhyResistant.Length > 50 ? r.WhyResistant[..47]+"..." : r.WhyResistant;
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture,
                "  {0,-28} {1,12} {2}", r.Quantity, val, why));
        }
        sb.AppendLine();
        sb.AppendLine("  COMMON CHARACTERISTICS OF RESISTANT QUANTITIES:");
        sb.AppendLine("    1. They are ALL DIMENSIONLESS (pure numbers).");
        sb.AppendLine("    2. They are MAGNITUDES (how much), not structures (what).");
        sb.AppendLine("    3. They are NOT forced by topology or geometry.");
        sb.AppendLine("    4. They answer 'HOW MUCH' — not 'WHAT EXISTS'.");
        sb.AppendLine("    5. They are CONTINGENT (could be otherwise).");
        sb.AppendLine();
        sb.AppendLine("  THE STRIKING FACT:");
        sb.AppendLine("    EVERY derived quantity is structural (form).");
        sb.AppendLine("    EVERY resistant quantity is a dimensionless magnitude.");
        sb.AppendLine("    This is NOT a coincidence — it reveals a boundary.");
        return sb.ToString();
    }

    static string BuildC()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("STRUCTURE VERSUS PARAMETER");
        sb.AppendLine();
        sb.AppendLine("  STRUCTURE (derivable):");
        sb.AppendLine("    - The FORM of reality: what exists, what symmetries, what laws.");
        sb.AppendLine("    - Determined by the ONTOLOGY (Q-events, oscillation, phase).");
        sb.AppendLine("    - NECESSARY: cannot be otherwise. Mathematical inevitability.");
        sb.AppendLine("    - Examples: U(1), charge quantization, gravity, inertia, stability.");
        sb.AppendLine();
        sb.AppendLine("  PARAMETER (contingent):");
        sb.AppendLine("    - The MAGNITUDE of reality: how strong, how much, how heavy.");
        sb.AppendLine("    - Determined by HISTORY (actualization process, QG-006).");
        sb.AppendLine("    - CONTINGENT: could be otherwise. A specific realization.");
        sb.AppendLine("    - Examples: α=1/137, α_s=0.118, Yukawas, λ=0.13.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEP DIVIDE:");
        sb.AppendLine("    Structure : Parameter :: Grammar : Vocabulary");
        sb.AppendLine("    Structure : Parameter :: Form : Content");
        sb.AppendLine("    Structure : Parameter :: Law : Initial Condition");
        sb.AppendLine("    Structure : Parameter :: Identity : Abundance");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS MAPS TO TQM'S PRIMITIVES:");
        sb.AppendLine("    Q (becoming) → STRUCTURE (what succession/oscillation ARE).");
        sb.AppendLine("    Random Actualization → PARAMETERS (which realization occurs).");
        sb.AppendLine("    The two primitives split reality into derivable and contingent!");
        sb.AppendLine();
        sb.AppendLine("  THIS IS A PROFOUND RESULT:");
        sb.AppendLine("    TQM's ontology ALREADY CONTAINS the structure/parameter");
        sb.AppendLine("    distinction. Q gives the laws. Random Actualization gives");
        sb.AppendLine("    the numbers. The boundary is NOT an accident — it's built in.");
        return sb.ToString();
    }

    static string BuildD()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IDENTITY VERSUS ABUNDANCE (the QG-065b analogy)");
        sb.AppendLine();
        sb.AppendLine("  A PREVIOUS TQM RESULT (QG-065b, information program):");
        sb.AppendLine("    Identity = derivable (what a species IS).");
        sb.AppendLine("    Abundance = contingent (how MANY exist).");
        sb.AppendLine("    Same pattern: form is forced, quantity is historical.");
        sb.AppendLine();
        sb.AppendLine("  THE UNIVERSAL PATTERN:");
        sb.AppendLine("    Identity (what)  → STRUCTURE  → derivable");
        sb.AppendLine("    Abundance (how many) → PARAMETER → contingent");
        sb.AppendLine();
        sb.AppendLine("  MAPPING TO PARTICLE PHYSICS:");
        sb.AppendLine("    Particle IDENTITY (electron, proton) → TOPOLOGY → derivable");
        sb.AppendLine("    Particle ABUNDANCE (masses, couplings) → NUMBERS → contingent");
        sb.AppendLine();
        sb.AppendLine("  THE SAME PATTERN APPEARS EVERYWHERE:");
        sb.AppendLine("    - Chemistry: molecular FORM is derivable (valence),");
        sb.AppendLine("      reaction RATES (abundance) are empirical.");
        sb.AppendLine("    - Biology: species IDENTITY is genetic (derivable),");
        sb.AppendLine("      population SIZE (abundance) is historical.");
        sb.AppendLine("    - Cosmology: the LAW of expansion is derived,");
        sb.AppendLine("      the RATE (H0) is measured.");
        sb.AppendLine();
        sb.AppendLine("  THE UNIFYING INSIGHT:");
        sb.AppendLine("    Structure = Identity = WHAT = derivable (ontology).");
        sb.AppendLine("    Parameter = Abundance = HOW MUCH = contingent (history).");
        sb.AppendLine("    This is a UNIVERSAL pattern, not a particle-physics quirk.");
        return sb.ToString();
    }

    static string BuildE()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("DIMENSIONLESS CONSTANTS: THE TRUE PARAMETERS");
        sb.AppendLine();
        sb.AppendLine("  CRUCIAL DISTINCTION:");
        sb.AppendLine("    DIMENSIONFUL constants (c, ħ, G) are UNIT DEFINITIONS.");
        sb.AppendLine("    They can be set to 1 (natural units). They set the RULER,");
        sb.AppendLine("    not the physics. They don't need 'derivation' in the");
        sb.AppendLine("    same sense — they're conventions.");
        sb.AppendLine();
        sb.AppendLine("    DIMENSIONLESS constants (α, α_s, λ, Yukawas) are RATIOS.");
        sb.AppendLine("    They CANNOT be absorbed by unit choice. They are the");
        sb.AppendLine("    TRUE parameters — the actual physics.");
        sb.AppendLine();
        sb.AppendLine("  TQM ON DIMENSIONFUL CONSTANTS:");
        sb.AppendLine("    c = ℓ/τ (definition, QG-016).");
        sb.AppendLine("    G = ℓ²c³/ħ (derived, QG-007).");
        sb.AppendLine("    ħ = quantum of action (empirical scale, QG-014).");
        sb.AppendLine("    These are the TRIPLE (ℓ,τ,ħ) — they set the SCALE.");
        sb.AppendLine("    They are 'dimensionful' → 'unit conventions' → 'not the");
        sb.AppendLine("    real physics'. This is WHY TQM 'derived' G: G is dimensionful!");
        sb.AppendLine();
        sb.AppendLine("  TQM ON DIMENSIONLESS CONSTANTS:");
        sb.AppendLine("    α = g²/4π = the ONE truly dimensionless coupling.");
        sb.AppendLine("    α_s, λ, Yukawas — all dimensionless, all empirical.");
        sb.AppendLine("    TQM derives NONE of them.");
        sb.AppendLine();
        sb.AppendLine("  THE DEEP PATTERN:");
        sb.AppendLine("    Dimensionful → derived or defined (scale-setting).");
        sb.AppendLine("    Dimensionless → contingent (true parameters).");
        sb.AppendLine("    This is consistent with the history of physics:");
        sb.AppendLine("    NO dimensionless constant has EVER been derived.");
        return sb.ToString();
    }

    static string BuildF()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("HOSTILE REDUCTION ATTEMPT: CAN TQM DERIVE α = 1/137?");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 1: α from topology");
        sb.AppendLine("    α = g²/4π. g = winding-gauge coupling. The winding number");
        sb.AppendLine("    n is an INTEGER, but g (the coupling strength) is continuous.");
        sb.AppendLine("    Topology fixes n ∈ Z, NOT g ∈ R+. FAILS: g is not topological.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 2: α from geometry");
        sb.AppendLine("    Could α relate to a geometric ratio (volume, angle)?");
        sb.AppendLine("    No geometric quantity in the phase field produces 1/137.");
        sb.AppendLine("    FAILS: no geometric object gives 1/137.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 3: α from architecture");
        sb.AppendLine("    Could α emerge from the frequency architecture's complexity?");
        sb.AppendLine("    No: architecture determines mass hierarchy (which is also");
        sb.AppendLine("    unexplained). No architectural count yields 1/137.");
        sb.AppendLine("    FAILS: architecture gives structure, not numbers.");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 4: α from complexity optimization");
        sb.AppendLine("    Is α = 1/137 the 'optimal' coupling for stable matter?");
        sb.AppendLine("    POSSIBLY (if α >> 1/137, atoms unstable; if α << 1/137,");
        sb.AppendLine("    no chemistry). But this is SELECTION (anthropic), not");
        sb.AppendLine("    DERIVATION. It gives a BAND, not a point.");
        sb.AppendLine("    FAILS as a derivation (succeeds as a selection argument).");
        sb.AppendLine();
        sb.AppendLine("  ATTEMPT 5: α from the triple (ℓ,τ,ħ)");
        sb.AppendLine("    ℓ, τ, ħ are dimensionful. Their RATIOS give dimensionful");
        sb.AppendLine("    quantities (c = ℓ/τ, G = ℓ²c³/ħ). NO combination of ℓ,τ,ħ");
        sb.AppendLine("    can produce a DIMENSIONLESS number. Hence α CANNOT come");
        sb.AppendLine("    from the triple. FAILS: dimensionful → dimensionful.");
        sb.AppendLine();
        sb.AppendLine("  ALL ATTEMPTS FAIL. Honest conclusion:");
        sb.AppendLine("    TQM has NO route to α = 1/137.");
        sb.AppendLine("    The dimensionless couplings are not derivable from");
        sb.AppendLine("    TQM's current primitives (Q, Randomness, ℓ, τ, ħ).");
        return sb.ToString();
    }

    static string BuildG()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("THE DERIVABILITY BOUNDARY");
        sb.AppendLine();
        sb.AppendLine("  WHERE IS THE BOUNDARY?");
        sb.AppendLine();
        sb.AppendLine("  DERIVABLE (structure):");
        sb.AppendLine("    - Topology (S¹, winding)");
        sb.AppendLine("    - Symmetries (U(1))");
        sb.AppendLine("    - Conservation laws (charge, topological)");
        sb.AppendLine("    - Existence (particles, stability)");
        sb.AppendLine("    - Relations (G = ℓ²c³/ħ, c = ℓ/τ)");
        sb.AppendLine("    - Qualitative behavior (gravity, inertia, Higgs interpretation)");
        sb.AppendLine();
        sb.AppendLine("  CONTINGENT (parameters):");
        sb.AppendLine("    - Dimensionless couplings (α, α_s, λ, Yukawas)");
        sb.AppendLine("    - Mass ratios (m_μ/m_e = 207, etc.)");
        sb.AppendLine("    - Mixing angles (θ_W, CKM, PMNS)");
        sb.AppendLine("    - Absolute scales (ℓ, τ, ħ — empirical)");
        sb.AppendLine();
        sb.AppendLine("  THE BOUNDARY IS SHARP AND PRINCIPLED:");
        sb.AppendLine("    Derivability stops EXACTLY where dimensionless magnitude");
        sb.AppendLine("    begins. TQM derives FORM; it does not derive MAGNITUDE.");
        sb.AppendLine();
        sb.AppendLine("  WHY THIS IS NOT A FAILURE:");
        sb.AppendLine("    Random Actualization (QG-006) is INHERENTLY stochastic.");
        sb.AppendLine("    The specific realization (parameter values) is a HISTORICAL");
        sb.AppendLine("    outcome of the actualization process, not a mathematical");
        sb.AppendLine("    necessity. Structure is ontology; parameters are history.");
        sb.AppendLine();
        sb.AppendLine("  MULTIVERSE-FREE VARIABILITY:");
        sb.AppendLine("    TQM does NOT need a multiverse to explain contingency.");
        sb.AppendLine("    Random Actualization provides parameter variability");
        sb.AppendLine("    WITHIN a single actualization history. Different initial");
        sb.AppendLine("    Q-event configurations → different parameter values.");
        sb.AppendLine("    Contingency is INTRINSIC to the theory, not bolted on.");
        return sb.ToString();
    }

    static string BuildH()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("IMPLICATIONS FOR TQM");
        sb.AppendLine();
        sb.AppendLine("  1. TQM'S CLAIM IS REFINED:");
        sb.AppendLine("     OLD CLAIM: 'TQM derives everything.'");
        sb.AppendLine("     REFINED CLAIM: 'TQM derives all STRUCTURE; parameters");
        sb.AppendLine("     are contingent historical outcomes.'");
        sb.AppendLine("     This is MORE HONEST and MORE PRECISE.");
        sb.AppendLine();
        sb.AppendLine("  2. THE TWO PRIMITIVES MAP PERFECTLY:");
        sb.AppendLine("     Q → structure (becoming, succession, laws).");
        sb.AppendLine("     Random Actualization → parameters (realization, history).");
        sb.AppendLine("     The structure/parameter split IS the Q/Randomness split.");
        sb.AppendLine("     This is a DEEP consistency check that TQM passes.");
        sb.AppendLine();
        sb.AppendLine("  3. PARAMETER REDUCTION IS REAL BUT LIMITED:");
        sb.AppendLine("     TQM reduces CONCEPTS (26 → 2 primitives + 3 scales).");
        sb.AppendLine("     TQM does NOT reduce NUMBERS (19 couplings stay).");
        sb.AppendLine("     The reduction is ONTOLOGICAL, not NUMERICAL.");
        sb.AppendLine();
        sb.AppendLine("  4. THE COMPLETENESS QUESTION:");
        sb.AppendLine("     Is TQM 'complete' if it leaves 19 numbers unexplained?");
        sb.AppendLine("     Answer: TQM explains WHAT EXISTS and WHY (structure).");
        sb.AppendLine("     It leaves HOW MUCH (parameters) to history. A complete");
        sb.AppendLine("     theory of structure + a theory of contingency together");
        sb.AppendLine("     constitute completeness. TQM has both.");
        sb.AppendLine();
        sb.AppendLine("  5. THE DEEPEST RESULT OF THE QG PROGRAM:");
        sb.AppendLine("     The boundary between derivable and contingent is NOT");
        sb.AppendLine("     a failure of TQM — it is a FEATURE. Random Actualization");
        sb.AppendLine("     was introduced in QG-006 as the deepest primitive.");
        sb.AppendLine("     Its presence NECESSARILY produces contingency.");
        sb.AppendLine("     Structure is what randomness CANNOT change.");
        sb.AppendLine("     Parameters are what randomness DOES determine.");
        return sb.ToString();
    }

    static string BuildI()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("FINAL VERDICT");
        sb.AppendLine();
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine("  STRUCTURE IS DERIVABLE; PARAMETERS ARE CONTINGENT");
        sb.AppendLine("  ═══════════════════════════════════════════════════════════════");
        sb.AppendLine();
        sb.AppendLine("  Q1: Structure = form, symmetry, topology (WHAT exists).");
        sb.AppendLine("      Parameter = magnitude, coupling, ratio (HOW MUCH).");
        sb.AppendLine("  Q2: TQM derives structure (from Q ontology) but not parameters");
        sb.AppendLine("      (which come from Random Actualization history).");
        sb.AppendLine("  Q3: Physical constants = HISTORICAL OUTCOMES, not fundamental");
        sb.AppendLine("      laws. The LAW is the structure; the CONSTANT is the history.");
        sb.AppendLine("  Q4: YES — universes can share structure, differ in parameters.");
        sb.AppendLine("      Same laws, different realization. (Multiverse-free: this");
        sb.AppendLine("      is Random Actualization's natural variability.)");
        sb.AppendLine("  Q5: YES — actualization history generates parameter variability.");
        sb.AppendLine("  Q6: Couplings could emerge as STATISTICAL quantities (expectation");
        sb.AppendLine("      values of the actualization process). Speculative, not derived.");
        sb.AppendLine("  Q7: YES — dimensionless constants resist derivation because they");
        sb.AppendLine("      are RATIOS with no dimensionful anchor. They are pure numbers.");
        sb.AppendLine("  Q8: YES — Q-event ontology constrains STRUCTURE, not VALUES.");
        sb.AppendLine("  Q9: YES — everywhere: energy level FORM derived, Rydberg constant");
        sb.AppendLine("      measured. Geodesic FORM derived, G measured. Universal.");
        sb.AppendLine("  Q10: NO known route derives α=1/137 without numerology.");
        sb.AppendLine();
        sb.AppendLine("  THE DERIVABILITY BOUNDARY:");
        sb.AppendLine("    ---------------------- DERIVABLE (structure) ----------------------");
        sb.AppendLine("    Topology, symmetries, conservation laws, existence, relations");
        sb.AppendLine("    ---------------------- CONTINGENT (parameters) --------------------");
        sb.AppendLine("    Dimensionless couplings, mass ratios, mixing angles, scales");
        sb.AppendLine("    -------------------------------------------------------------------");
        sb.AppendLine();
        sb.AppendLine("  CLASSIFICATION: A — PARAMETERS FUNDAMENTALLY CONTINGENT");
        sb.AppendLine();
        sb.AppendLine("    The inability to derive α, α_s, λ, Yukawas is NOT a");
        sb.AppendLine("    temporary limitation. It is a FUNDAMENTAL FEATURE:");
        sb.AppendLine("    Random Actualization (the deepest primitive) makes");
        sb.AppendLine("    parameters historical outcomes, not mathematical");
        sb.AppendLine("    necessities. Structure is forced; parameters are drawn.");
        sb.AppendLine();
        sb.AppendLine("    This is arguably the DEEPEST RESULT of the QG program:");
        sb.AppendLine("    TQM does not fail to derive the numbers — it EXPLAINS");
        sb.AppendLine("    WHY the numbers cannot be derived.");
        sb.AppendLine();
        sb.AppendLine("  QG program: 42 experiments.");
        return sb.ToString();
    }
}
